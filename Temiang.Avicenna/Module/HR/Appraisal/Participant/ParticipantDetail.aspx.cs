using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.HR.Appraisal
{
    public partial class ParticipantDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AppraisalParticipant;

            UrlPageSearch = "ParticipantSearch.aspx";
            UrlPageList = "ParticipantList.aspx";

            WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                trAppraisalType.Visible = false; //AppSession.Parameter.AppraisalVersionNo == "3";
                trScoringRecapitulation.Visible = false; //AppSession.Parameter.AppraisalVersionNo == "3";
                StandardReference.InitializeIncludeSpace(cboSREmployeeType, AppEnum.StandardReference.EmployeeType);
                StandardReference.InitializeIncludeSpace(cboSRAppraisalType, AppEnum.StandardReference.AppraisalType);
                StandardReference.InitializeIncludeSpace(cboSRQuarterPeriod, AppEnum.StandardReference.QuarterPeriod);
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdParticipantItem, grdParticipantItem);
            ajax.AddAjaxSetting(grdParticipantItem, cboSREmployeeType);
            ajax.AddAjaxSetting(grdParticipantItem, cboServiceUnitID);
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new AppraisalParticipant());

            ViewState["id"] = 0;
            cboSRQuarterPeriod.SelectedValue = string.Empty;
            cboSRQuarterPeriod.Text = string.Empty;
            cboSREmployeeType.SelectedValue = string.Empty;
            cboSREmployeeType.Text = string.Empty;
            chkIsScoringRecapitulation.Checked = true;
            cboSRAppraisalType.SelectedValue = "A";
        }

        protected override void OnMenuEditClick()
        {
            cboSREmployeeType.Enabled = ParticipantItems.Count == 0;
            cboServiceUnitID.Enabled = ParticipantItems.Count == 0;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new AppraisalParticipant();
            if (entity.LoadByPrimaryKey(ViewState["id"].ToInt()))
            {
                var as1 = new AppraisalScoresheetQuery("as1");
                var api = new AppraisalParticipantItemQuery("api");
                as1.InnerJoin(api).On(api).Or(api.ParticipantItemID == as1.ParticipantItemID);
                as1.Where(api.ParticipantID == ViewState["id"].ToInt());

                DataTable as1dt = as1.LoadDataTable();
                if (as1dt.Rows.Count > 0)
                {
                    args.MessageText = AppConstant.Message.RecordHasUsed;
                    return;
                }

                using (var trans = new esTransactionScope())
                {
                    var apis = new AppraisalParticipantItemCollection();
                    apis.Query.Where(apis.Query.ParticipantID == ViewState["id"].ToInt());
                    apis.LoadAll();

                    var apes = new AppraisalParticipantEvaluatorCollection();
                    if (apis.Any())
                        apes.Query.Where(apes.Query.ParticipantItemID.In(apis.Select(p => p.ParticipantItemID)));
                    else
                        apes.Query.Where(apes.Query.ParticipantItemID == -1);
                    apes.LoadAll();
                    apes.MarkAllAsDeleted();
                    apes.Save();

                    var apqs = new AppraisalParticipantQuestionerCollection();
                    if (apqs.Any())
                        apqs.Query.Where(apqs.Query.ParticipantItemID.In(apis.Select(p => p.ParticipantItemID)));
                    else
                        apqs.Query.Where(apqs.Query.ParticipantItemID == -1);
                    apqs.LoadAll();
                    apqs.MarkAllAsDeleted();
                    apqs.Save();

                    apis.MarkAllAsDeleted();
                    apis.Save();

                    entity.MarkAsDeleted();
                    entity.Save();

                    trans.Complete();
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new AppraisalParticipant();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new AppraisalParticipant();
            if (entity.LoadByPrimaryKey(ViewState["id"].ToInt()))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("ParticipantID='{0}'", ViewState["id"].ToInt());
            auditLogFilter.TableName = "AppraisalParticipant";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            RefreshCommandItemParticipantItem(oldVal, newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new AppraisalParticipant();
            if (parameters.Length > 0)
            {
                String id = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty)) entity.LoadByPrimaryKey(id.ToInt());
            }
            else
            {
                entity.LoadByPrimaryKey(ViewState["id"].ToInt());
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var aq = (AppraisalParticipant)entity;
            if (aq != null) ViewState["id"] = aq.ParticipantID.ToString();
            else ViewState["id"] = 0;
            txtParticipantName.Text = aq.ParticipantName;
            txtPeriodYear.Text = aq.PeriodYear;
            txtNotes.Text = aq.Notes;
            chkIsScoringRecapitulation.Checked = aq.IsScoringRecapitulation ?? false;
            cboSREmployeeType.SelectedValue = aq.SREmployeeType;
            if (!string.IsNullOrEmpty(aq.ServiceUnitID))
            {
                PopulateCboServiceUnitID2(cboServiceUnitID, aq.ServiceUnitID);
                cboServiceUnitID.SelectedValue = aq.ServiceUnitID;
            }
            cboSRAppraisalType.SelectedValue = aq.SRAppraisalType;
            cboSRQuarterPeriod.SelectedValue = aq.SRQuarterPeriod;
            
            ViewState["IsApproved"] = aq.IsApproved ?? false;
            ViewState["IsVoid"] = aq.IsVoid ?? false;

            PopulateParticipantItemGrid();
        }

        private void SetEntityValue(AppraisalParticipant entity)
        {
            entity.ParticipantName = txtParticipantName.Text;
            entity.PeriodYear = txtPeriodYear.Text;
            entity.SRQuarterPeriod = cboSRQuarterPeriod.SelectedValue;
            entity.Notes = txtNotes.Text;
            entity.IsScoringRecapitulation = chkIsScoringRecapitulation.Checked;
            entity.SREmployeeType = cboSREmployeeType.SelectedValue;
            entity.ServiceUnitID = cboServiceUnitID.SelectedValue;
            entity.SRAppraisalType = cboSRAppraisalType.SelectedValue;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(AppraisalParticipant entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                ViewState["id"] = entity.ParticipantID;

                foreach (var e in ParticipantItems)
                {
                    if (e.es.IsDeleted) continue;

                    int value = e.ParticipantItemID ?? 0;

                    var api = new AppraisalParticipantItem();
                    api.Query.Where(api.Query.ParticipantID == ViewState["id"].ToInt(), api.Query.EmployeeID == e.EmployeeID);
                    if (!api.Query.Load())
                        api = new AppraisalParticipantItem();
                    api.ParticipantID = ViewState["id"].ToInt();
                    api.EmployeeID = e.EmployeeID;

                    var emps = new VwEmployeeTable();
                    emps.Query.Where(emps.Query.PersonID == api.EmployeeID);
                    if (emps.Query.Load())
                    {
                        api.PositionID = emps.PositionID;
                        api.OrganizationUnitID = emps.OrganizationUnitID;
                        api.SubOrganizationUnitID = emps.SubOrganizationUnitID;
                        api.ServiceUnitID = emps.ServiceUnitID;
                        api.SubDivisonID = emps.SubDivisonID;
                    }

                    api.IsClosed = e.IsClosed;
                    api.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    api.LastUpdateDateTime = DateTime.Now;
                    api.Save();

                    var pqs = ParticipantQuestioners.Where(pq => pq.ParticipantItemID == (value != api.ParticipantItemID ? value : api.ParticipantItemID));
                    foreach (var f in pqs)
                    {
                        var apq = new AppraisalParticipantQuestioner();
                        apq.Query.Where(apq.Query.ParticipantItemID == api.ParticipantItemID, apq.Query.QuestionerID == f.QuestionerID, apq.Query.EvaluatorID == f.EvaluatorID);
                        if (!apq.Query.Load())
                            apq = new AppraisalParticipantQuestioner();
                        apq.ParticipantItemID = api.ParticipantItemID;
                        apq.QuestionerID = f.QuestionerID;
                        apq.EvaluatorID = f.EvaluatorID;

                        apq.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        apq.LastUpdateDateTime = DateTime.Now;
                        apq.Save();
                    }

                    var pvs = ParticipantEvaluators.Where(pq => pq.ParticipantItemID == (value != api.ParticipantItemID ? value : api.ParticipantItemID));
                    foreach (var g in pvs)
                    {
                        var ape = new AppraisalParticipantEvaluator();
                        ape.Query.Where(ape.Query.ParticipantItemID == api.ParticipantItemID, ape.Query.EvaluatorID == g.EvaluatorID);
                        if (!ape.Query.Load())
                            ape = new AppraisalParticipantEvaluator();
                        ape.ParticipantItemID = api.ParticipantItemID;
                        ape.EvaluatorID = g.EvaluatorID;

                        emps = new VwEmployeeTable();
                        emps.Query.Where(emps.Query.PersonID == ape.EvaluatorID);
                        if (emps.Query.Load())
                        {
                            ape.PositionID = emps.PositionID;
                            ape.OrganizationUnitID = emps.OrganizationUnitID;
                            ape.SubOrganizationUnitID = emps.SubOrganizationUnitID;
                            ape.ServiceUnitID = emps.ServiceUnitID;
                            ape.SubDivisonID = emps.SubDivisonID;
                        }

                        ape.SREvaluatorType = g.SREvaluatorType;
                        ape.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        ape.LastUpdateDateTime = DateTime.Now;
                        ape.Save();
                    }
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new AppraisalParticipantQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ParticipantID > ViewState["id"].ToInt());
                que.OrderBy(que.ParticipantID.Ascending);
            }
            else
            {
                que.Where(que.ParticipantID < ViewState["id"].ToInt());
                que.OrderBy(que.ParticipantID.Descending);
            }
            var entity = new AppraisalParticipant();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new AppraisalParticipant();
            if (!entity.LoadByPrimaryKey(ViewState["id"].ToInt()))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }
            entity.IsApproved = true;
            entity.ApprovedByUserID = AppSession.UserLogin.UserID;
            entity.ApprovedDateTime = DateTime.Now;
            SaveEntity(entity);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new AppraisalParticipant();
            if (!entity.LoadByPrimaryKey(ViewState["id"].ToInt()))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }
            entity.IsApproved = false;
            entity.str.ApprovedByUserID = string.Empty;
            entity.str.ApprovedDateTime = string.Empty;
            SaveEntity(entity);
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new AppraisalParticipant();
            if (!entity.LoadByPrimaryKey(ViewState["id"].ToInt()))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return;
            }

            entity.IsVoid = true;
            entity.VoidByUserID = AppSession.UserLogin.UserID;
            entity.VoidDateTime = DateTime.Now;
            SaveEntity(entity);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            var entity = new AppraisalParticipant();
            if (!entity.LoadByPrimaryKey(ViewState["id"].ToInt()))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            entity.IsVoid = false;
            entity.str.VoidByUserID = string.Empty;
            entity.str.VoidDateTime = string.Empty;
            SaveEntity(entity);
        }

        public override bool OnGetStatusMenuEdit()
        {
            return ViewState["id"].ToInt() > 0;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !(bool)ViewState["IsApproved"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        private AppraisalParticipantItemCollection ParticipantItems
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collAppraisalParticipantItem" + Request.UserHostName];
                    if (obj != null) return ((AppraisalParticipantItemCollection)(obj));
                }

                var coll = new AppraisalParticipantItemCollection();

                var query = new AppraisalParticipantItemQuery("a");
                var emp1 = new VwEmployeeTableQuery("b");

                query.Select(
                    query,
                    (emp1.EmployeeNumber + " - " + emp1.EmployeeName).As("refToVwEmployeeTableQuery_EmployeeName"),
                    "<[dbo].[fn_AppraisalParticipantQuestionerEvaluators](a.ParticipantItemID) AS refToAppraisalParticipantQuestionerQuery_Evaluators>",
                    "<[dbo].[fn_AppraisalParticipantQuestionerQuestioners](a.ParticipantItemID) AS refToAppraisalParticipantQuestionerQuery_Questioners>"
                    );
                query.Where(query.ParticipantID == ViewState["id"].ToInt());

                query.LeftJoin(emp1).On(query.EmployeeID == emp1.PersonID);

                query.OrderBy(query.ParticipantItemID.Ascending);

                coll.Load(query);

                Session["collAppraisalParticipantItem" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collAppraisalParticipantItem" + Request.UserHostName] = value; }
        }

        private AppraisalParticipantQuestionerCollection ParticipantQuestioners
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collAppraisalParticipantQuestioner" + Request.UserHostName];
                    if (obj != null) return ((AppraisalParticipantQuestionerCollection)(obj));
                }

                var coll = new AppraisalParticipantQuestionerCollection();

                var query = new AppraisalParticipantQuestionerQuery("a");

                var qq = new AppraisalQuestionQuery("b");
                var participant = new AppraisalParticipantItemQuery("c");
                var emp = new VwEmployeeTableQuery("d");
                var eval = new VwEmployeeTableQuery("e");

                query.Select(query,
                    qq.QuestionerNo.As("refToAppraisalQuestion_QuestionerNo"),
                    qq.QuestionerName.As("refToAppraisalQuestion_QuestionerName"),
                    emp.EmployeeName.As("refToVwEmployeeTableQuery_EmployeeName"),
                    eval.EmployeeName.As("refToVwEmployeeTableQuery_QuestionerEvaluatorName")
                    );
                if (ParticipantItems.Any())
                    query.Where(query.ParticipantItemID.In(ParticipantItems.Select(p => p.ParticipantItemID)));
                else
                    query.Where(query.ParticipantItemID == -1);

                query.LeftJoin(qq).On(query.QuestionerID == qq.QuestionerID);
                query.LeftJoin(participant).On(query.ParticipantItemID == participant.ParticipantItemID);
                query.LeftJoin(emp).On(participant.EmployeeID == emp.PersonID);
                query.LeftJoin(eval).On(eval.PersonID == query.EvaluatorID);

                query.OrderBy(query.ParticipantItemID.Ascending);

                DataTable dtb = query.LoadDataTable();

                coll.Load(query);

                Session["collAppraisalParticipantQuestioner" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collAppraisalParticipantQuestioner" + Request.UserHostName] = value; }
        }

        private AppraisalParticipantEvaluatorCollection ParticipantEvaluators
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collAppraisalParticipantEvaluator" + Request.UserHostName];
                    if (obj != null) return ((AppraisalParticipantEvaluatorCollection)(obj));
                }

                var coll = new AppraisalParticipantEvaluatorCollection();

                var query = new AppraisalParticipantEvaluatorQuery("a");
                var emp = new VwEmployeeTableQuery("b");
                var std = new AppStandardReferenceItemQuery("c");

                query.Select(query,
                    emp.EmployeeName.As("refToVwEmployeeTableQuery_EmployeeName"),
                    std.ItemName.As("refToAppStandardReferenceItem_ItemName")
                    );
                query.LeftJoin(emp).On(query.EvaluatorID == emp.PersonID);
                query.LeftJoin(std).On(query.SREvaluatorType == std.ItemID && std.StandardReferenceID == AppEnum.StandardReference.EvaluatorType.ToString());
                if (ParticipantItems.Any())
                    query.Where(query.ParticipantItemID.In(ParticipantItems.Select(p => p.ParticipantItemID)));
                else
                    query.Where(query.ParticipantItemID == -1);
                query.OrderBy(query.ParticipantItemID.Ascending);

                coll.Load(query);

                Session["collAppraisalParticipantEvaluator" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collAppraisalParticipantEvaluator" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemParticipantItem(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdParticipantItem.Columns[0].Visible = isVisible;
            grdParticipantItem.Columns[grdParticipantItem.Columns.Count - 1].Visible = isVisible;

            grdParticipantItem.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdParticipantItem.Rebind();
        }

        private void PopulateParticipantItemGrid()
        {
            //Display Data Detail
            ParticipantItems = null; //Reset Record Detail
            grdParticipantItem.DataSource = ParticipantItems; //Requery
            grdParticipantItem.MasterTableView.IsItemInserted = false;
            grdParticipantItem.MasterTableView.ClearEditItems();
            grdParticipantItem.DataBind();

            ParticipantQuestioners = null;
            var pq = ParticipantQuestioners;

            ParticipantEvaluators = null;
            var pe = ParticipantEvaluators;
        }

        protected void grdParticipantItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdParticipantItem.DataSource = ParticipantItems;
        }

        protected void grdParticipantItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            var participantItemId = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][AppraisalParticipantItemMetadata.ColumnNames.ParticipantItemID]);
            var entity = FindAppraisalParticipantItem(participantItemId);
            if (entity != null && entity.IsClosed == false)
            {
                SetEntityValue(entity, e, false);
            }
        }

        protected void grdParticipantItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var participantItemId = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][AppraisalParticipantItemMetadata.ColumnNames.ParticipantItemID]);

            var as1 = new AppraisalScoresheetQuery("as1");
            as1.Where(as1.ParticipantItemID == participantItemId, as1.IsVoid == false);
            DataTable as1dtb = as1.LoadDataTable();
            if (as1dtb.Rows.Count > 0) return;

            var entity = FindAppraisalParticipantItem(participantItemId);
            if (entity != null && entity.IsClosed == false)
            {
                var pqs = ParticipantQuestioners.Where(pq => pq.ParticipantItemID == entity.ParticipantItemID);
                foreach (var f in ParticipantQuestioners)
                {
                    f.MarkAsDeleted();
                }
                ParticipantQuestioners.Save();

                var pvs = ParticipantEvaluators.Where(pq => pq.ParticipantItemID == entity.ParticipantItemID);
                foreach (var g in ParticipantEvaluators)
                {
                    g.MarkAsDeleted();
                }
                ParticipantEvaluators.Save();

                entity.MarkAsDeleted();
                ParticipantItems.Save();
            }

            cboSREmployeeType.Enabled = ParticipantItems.Count == 0;
            cboServiceUnitID.Enabled = ParticipantItems.Count == 0;
        }

        protected void grdParticipantItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = ParticipantItems.AddNew();
            SetEntityValue(entity, e, true);

            e.Canceled = true;
            grdParticipantItem.Rebind();

            cboSREmployeeType.Enabled = ParticipantItems.Count == 0;
            cboServiceUnitID.Enabled = ParticipantItems.Count == 0;
        }

        private AppraisalParticipantItem FindAppraisalParticipantItem(int participantItemId)
        {
            return ParticipantItems.FirstOrDefault(rec => rec.ParticipantItemID.Equals(participantItemId));
        }

        private void SetEntityValue(AppraisalParticipantItem entity, GridCommandEventArgs e, bool isInsertCommand)
        {
            var userControl = (ParticipantItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ParticipantItemID = userControl.ParticipantItemID;
                entity.EmployeeID = userControl.EmployeeID.ToInt();
                entity.EmployeeName = userControl.EmployeeName;
                entity.Evaluators = userControl.Evaluators;
                entity.Questioners = userControl.Questioners;
                entity.IsClosed = userControl.IsClosed;

                var emps = new VwEmployeeTable();
                emps.Query.Where(emps.Query.PersonID == entity.EmployeeID);
                if (emps.Query.Load())
                {
                    entity.PositionID = emps.PositionID;
                    entity.PositionValidFromDate = emps.PositionValidFromDate;
                    entity.OrganizationUnitID = emps.OrganizationUnitID;
                    entity.SubOrganizationUnitID = emps.SubOrganizationUnitID;
                    entity.ServiceUnitID = emps.ServiceUnitID;
                }
            }
        }

        protected void cboServiceUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboServiceUnitID((RadComboBox)sender, e.Text);
        }

        private void PopulateCboServiceUnitID(RadComboBox comboBox, string textSearch)
        {
            DataTable dtb;
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new OrganizationUnitQuery("a");
            var sub = new OrganizationUnitQuery("b");
            query.LeftJoin(sub).On(sub.OrganizationUnitID == query.ParentOrganizationUnitID);
            query.Select(query.OrganizationUnitID.As("ServiceUnitID"), query.OrganizationUnitName.As("ServiceUnitName"));
            query.Where(query.OrganizationUnitName.Like(searchTextContain), query.SROrganizationLevel == "0");
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }

        private void PopulateCboServiceUnitID2(RadComboBox comboBox, string textSearch)
        {
            DataTable dtb;

            var query = new OrganizationUnitQuery();

            query.Where(query.OrganizationUnitID == textSearch);

            query.Select(query.OrganizationUnitID.As("ServiceUnitID"),
                         query.OrganizationUnitName.As("ServiceUnitName"));
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }

        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void cboSRAppraisalType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var appraisaltype = new AppStandardReferenceItem();
            if (appraisaltype.LoadByPrimaryKey(AppEnum.StandardReference.AppraisalType.ToString(), e.Value))
                chkIsScoringRecapitulation.Checked = appraisaltype.NumericValue.ToInt() == 1;
            else
                chkIsScoringRecapitulation.Checked = true;
        }
    }
}