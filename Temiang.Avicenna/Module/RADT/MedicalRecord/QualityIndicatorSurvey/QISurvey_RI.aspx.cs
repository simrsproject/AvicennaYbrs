using System;
using System.Web.Util;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using System.Globalization;
using System.Linq;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class QISurvey_RI : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "#";
            UrlPageList = "QualityIndicatorSurveyList.aspx";

            ProgramID = AppConstant.Program.QualityIndicatorSurvey;

            if (!IsPostBack)
            {
                txtStandardReferenceID.Text = Request.QueryString["srid"];
                var sr = new AppStandardReference();
                sr.LoadByPrimaryKey(txtStandardReferenceID.Text);
                txtStandardReferenceName.Text = sr.StandardReferenceName;
                ComboBox.PopulateWithServiceUnit(cboServiceUnitID, true);

                grdQISurvey_RI.Columns.FindByUniqueName("Denum").Visible = AppSession.Parameter.KPI_IsShowDenum;
            }
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new QualityIndicatorSurvey());
            txtPeriodDate.SelectedDate = (new DateTime()).NowAtSqlServer();
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            QualityIndicatorSurvey entity = new QualityIndicatorSurvey();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtSurveyID.Text)))
            {
                entity.MarkAsDeleted();

                var qisd = new QualityIndicatorSurveyDetailCollection();
                qisd.Query.Where(qisd.Query.SurveyID == txtSurveyID.Text);
                qisd.LoadAll();
                qisd.MarkAllAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    qisd.Save();

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
            QualityIndicatorSurvey entity = new QualityIndicatorSurvey();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtSurveyID.Text)))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            var coll = new QualityIndicatorSurveyCollection();
            coll.Query.Where(coll.Query.StandardReferenceID == txtStandardReferenceID.Text,
                             coll.Query.PeriodDate == txtPeriodDate.SelectedDate);
            coll.LoadAll();
            if (coll.Count > 0)
            {
                args.MessageText = "Record with this period has registered.";
                args.IsCancel = true;
                return;
            }

            entity = new QualityIndicatorSurvey();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);

            txtSurveyID.Text = entity.SurveyID.ToString();
            
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new QualityIndicatorSurvey();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtSurveyID.Text)))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var bg = new QualityIndicatorSurvey();
            if (!bg.LoadByPrimaryKey(Convert.ToInt32(txtSurveyID.Text)))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (bg.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }
            if (bg.IsApprove ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return;
            }

            bg.IsApprove = true;
            bg.ApprovedByUserID = AppSession.UserLogin.UserID;
            bg.ApprovedDateTime = DateTime.Now;
            
            using (var trans = new esTransactionScope())
            {
                bg.Save();
                trans.Complete();
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var bg = new QualityIndicatorSurvey();
            if (!bg.LoadByPrimaryKey(Convert.ToInt32(txtSurveyID.Text)))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (!(bg.IsApprove ?? false))
            {
                args.MessageText = AppConstant.Message.RecordHasNotApproved;
                args.IsCancel = true;
                return;
            }
            if (bg.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            bg.IsApprove = false;
            bg.ApprovedByUserID = AppSession.UserLogin.UserID;
            bg.ApprovedDateTime = DateTime.Now;

            using (var trans = new esTransactionScope())
            {
                bg.Save();
                trans.Complete();
            }
        }

        //protected override void OnMenuVoidClick(ValidateArgs args)
        //{
        //    (new QualityIndicatorSurvey()).Void(txtSurveyID.Text, AppSession.UserLogin.UserID);
        //}

        //private bool IsApprovedOrVoid(QualityIndicatorSurvey entity, ValidateArgs args)
        //{
        //    if (entity.IsApprove != null && entity.IsApprove.Value)
        //    {
        //        args.MessageText = AppConstant.Message.RecordHasApproved;
        //        args.IsCancel = true;
        //        return false;
        //    }
        //    if (entity.IsVoid != null && entity.IsVoid.Value)
        //    {
        //        args.MessageText = AppConstant.Message.RecordHasVoided;
        //        args.IsCancel = true;
        //        return false;
        //    }
        //    return true;
        //}

        public override bool? OnGetStatusMenuApproval()
        {
            return !chkIsApproved.Checked;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !chkIsVoid.Checked;
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
            auditLogFilter.PrimaryKeyData = string.Format("SurveyID='{0}'", txtSurveyID.Text.Trim());
            auditLogFilter.TableName = "QualityIndicatorSurvey";
        }


        #endregion

        #region Toolbar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtSurveyID.Enabled = (newVal == AppEnum.DataMode.New);
            txtPeriodDate.Enabled = (newVal == AppEnum.DataMode.New);

            //pnlPrint.Visible = newVal == AppEnum.DataMode.Read;
            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new QualityIndicatorSurvey();
            if (parameters.Length > 0)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["surid"]))
                    entity.LoadByPrimaryKey(Request.QueryString["surid"].ToInt());
            }
            else
            {
                entity.LoadByPrimaryKey(txtSurveyID.Text.ToInt());
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var qis = (QualityIndicatorSurvey)entity;
            txtCreateByUserID.Text = qis.CreateByUserID ?? AppSession.UserLogin.UserID;
            if (!string.IsNullOrEmpty(qis.ServiceUnitID))
                cboServiceUnitID.SelectedValue = qis.ServiceUnitID;
            else
            {
                cboServiceUnitID.SelectedValue = string.Empty;
                cboServiceUnitID.Text = string.Empty;
            }
            txtSurveyID.Value = Convert.ToInt32(qis.SurveyID);
            txtPeriodDate.SelectedDate = qis.PeriodDate;
            chkIsApproved.Checked = qis.IsApprove ?? false;
            chkIsVoid.Checked = qis.IsVoid ?? false;
            
            PopulateGridDetail();
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuEdit.Enabled = !chkIsApproved.Checked;
            ToolBarMenuDelete.Enabled = !chkIsApproved.Checked;
        }
        #endregion

        #region Private Method Standard
        private void SetEntityValue(QualityIndicatorSurvey entity)
        {
            entity.SurveyID = Convert.ToInt32(txtSurveyID.Value);
            entity.StandardReferenceID = txtStandardReferenceID.Text;
            entity.ServiceUnitID = cboServiceUnitID.SelectedValue;
            entity.PeriodDate = txtPeriodDate.SelectedDate;
            entity.IsApprove = chkIsApproved.Checked;
            entity.IsVoid = chkIsVoid.Checked;
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(QualityIndicatorSurvey entity)
        {
            var coll = new QualityIndicatorSurveyDetailCollection();
            coll.Query.Where(coll.Query.SurveyID == entity.SurveyID);
            coll.LoadAll();
            coll.MarkAllAsDeleted();
            coll.Save();

            entity.Save();
            var newcoll = new QualityIndicatorSurveyDetailCollection();
            foreach (GridDataItem dataItem in grdQISurvey_RI.MasterTableView.Items)
            {
                var itemId = dataItem.GetDataKeyValue("ItemID").ToString();
                var txtValue = dataItem.FindControl("txtNumerator") as RadNumericTextBox;
                var txtInputQueryNumer = dataItem.FindControl("txtInputQueryNumer") as RadTextBox;
                var txtValue2 = dataItem.FindControl("txtValueDemun") as RadNumericTextBox;
                var txtInput2 = dataItem.FindControl("txtInputQDenum") as RadTextBox;

                var qis = newcoll.AddNew();
                qis.SurveyID = entity.SurveyID;
                qis.ItemID = itemId;
                qis.Numerator = txtValue.Value.ToInt();
                qis.InputQueryNumer = txtInputQueryNumer.Text;
                qis.Denumerator = txtValue2.Value.ToInt();
                qis.InputQueryDenum = txtInput2.Text;
                qis.LastUpdateByUserID = AppSession.UserLogin.UserID;
                qis.LastUpdateDateTime = DateTime.Now;
            }
            
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                newcoll.Save();
                //Commit id success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new QualityIndicatorSurveyQuery();
            que.es.Top = 1; 
            if (isNextRecord)
            {
                que.Where(que.SurveyID > txtSurveyID.Text);
                que.OrderBy(que.SurveyID.Ascending);
            }
            else
            {
                que.Where(que.SurveyID < txtSurveyID.Text);
                que.OrderBy(que.SurveyID.Descending);
            }
            var entity = new QualityIndicatorSurvey();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        protected void grdQISurvey_RI_PreRender(object sender, EventArgs e)
        {
            for (int rowIndex = grdQISurvey_RI.Items.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridDataItem row = grdQISurvey_RI.Items[rowIndex];
                GridDataItem previousRow = grdQISurvey_RI.Items[rowIndex + 1];

                if (row["LineNumber"].Text == previousRow["LineNumber"].Text)
                {
                    row["LineNumber"].RowSpan = previousRow["LineNumber"].RowSpan < 2 ?
                        2 : previousRow["LineNumber"].RowSpan + 1;
                    previousRow["LineNumber"].Visible = false;
                }

                if (row["ItemName"].Text == previousRow["ItemName"].Text)
                {
                    row["ItemName"].RowSpan = previousRow["ItemName"].RowSpan < 2 ?
                        2 : previousRow["ItemName"].RowSpan + 1;
                    previousRow["ItemName"].Visible = false;
                }
            }
        }

        protected void grdQISurvey_RI_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdQISurvey_RI.DataSource = QualityIndicatorSurveyDetails;
        }

        private QualityIndicatorSurveyDetailCollection QualityIndicatorSurveyDetails
        {
            get
            {
                var obj = ViewState["collQualityIndicatorSurveyDetail"];
                if (obj != null)
                    return ((QualityIndicatorSurveyDetailCollection)(obj));

                var collection = new QualityIndicatorSurveyDetailCollection();

                var query = new QualityIndicatorSurveyDetailQuery("a");
                var qis = new QualityIndicatorSurveyQuery("qis");
                var sriq = new AppStandardReferenceItemQuery("b");


                query.InnerJoin(qis).On(query.SurveyID == qis.SurveyID)
                    .InnerJoin(sriq).On(query.ItemID == sriq.ItemID && qis.StandardReferenceID == sriq.StandardReferenceID);
                query.Select(
                        query,
                        sriq.ItemName.As("refToAppStandardReferenceItem_ItemName"),
                        sriq.Note.As("refToAppStandardReferenceItem_Note"),
                        sriq.LineNumber.As("refToAppStandardReferenceItem_LineNumber")
                    );

                query.Where(query.SurveyID == txtSurveyID.Text);

                query.OrderBy(sriq.ItemID.Ascending);

                collection.Load(query);

                if (collection.Count == 0)
                {
                    var sric = new AppStandardReferenceItemCollection();
                    sric.Query.Where(sric.Query.StandardReferenceID == Request.QueryString["srid"]);
                    sric.Query.Where(sric.Query.IsActive == true);
                    sric.Query.OrderBy(sric.Query.ItemID.Ascending);
                    sric.LoadAll();
                    foreach (var item in sric)
                    {
                        var coll = collection.AddNew();
                        coll.SurveyID = txtSurveyID.Text.ToInt();
                        coll.ItemID = item.ItemID;
                        coll.ItemName = item.ItemName;
                        coll.Note = item.Note;
                        coll.Numerator = 0;
                        coll.InputQueryNumer = item.CustomField;
                        coll.Denumerator = 0;
                        coll.InputQueryDenum = item.CustomField2;
                        coll.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        coll.LastUpdateDateTime = DateTime.Now;
                        coll.LineNumber = item.LineNumber ?? 0;
                    }
                }

                ViewState["collQualityIndicatorSurveyDetail"] = collection;

                return collection;
            }
            set
            {
                ViewState["collQualityIndicatorSurveyDetail"] = value;
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(source is RadGrid))
            {
                return;
            }

            switch (eventArgument)
            {
                case "process":
                    Process();
                    
                    //grdQISurvey_RI.Rebind();
                    break;
                case "print":
                    Print(AppConstant.Report.RL3_8);
                    break;
            }
        }

        private void Process()
        {
            foreach (GridDataItem item in grdQISurvey_RI.Items) 
            {
                string q1 = (item.FindControl("txtInputQueryNumer") as RadTextBox).Text;
                var val1 = (item.FindControl("txtNumerator") as RadNumericTextBox);
                string q2 = (item.FindControl("txtInputQDenum") as RadTextBox).Text;
                var val2 = (item.FindControl("txtValueDemun") as RadNumericTextBox);
                var pdate = txtPeriodDate.SelectedDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                var unit = Convert.ToString(cboServiceUnitID.SelectedValue);

                var sric = new AppStandardReferenceItemCollection();
                sric.Query.Where(sric.Query.StandardReferenceID == Request.QueryString["srid"], sric.Query.ItemID == item.GetDataKeyValue("ItemID").ToString());
                sric.Query.Where(sric.Query.IsActive == true);
                sric.Query.OrderBy(sric.Query.ItemID.Ascending);
                sric.LoadAll();
                foreach (var coll in sric)
                {
                    if (string.IsNullOrEmpty(coll.CustomField)) continue;

                    q1 = coll.CustomField.Replace("@p_unit", "'" + unit + "'").Replace("@p_date", "'" + pdate + "'");

                    if (string.IsNullOrEmpty(q1)) continue;

                    try
                    {
                        (item.FindControl("txtInputQueryNumer") as RadTextBox).Text = q1;
                        var ret = (new QualityIndicatorSurveyCollection()).ExecuteQuery(q1);
                        val1.Value = Convert.ToDouble(ret.Rows[0][0]);
                    }
                    catch (Exception ex)
                    {
                        ((System.Web.UI.WebControls.Label)item.FindControl("lblErrorMessage")).Text = "Error Num: " + ex.Message;
                        continue;
                    }
                    ((System.Web.UI.WebControls.Label)item.FindControl("lblErrorMessage")).Text = string.Empty;
                }

                foreach (var coll2 in sric)
                {
                    if (string.IsNullOrEmpty(coll2.CustomField2)) continue;

                    q2 = coll2.CustomField2.Replace("@p_date", "'" + pdate + "'").Replace("@p_date", "'" + pdate + "'");

                    if (string.IsNullOrEmpty(q2)) continue;

                    try
                    {
                        (item.FindControl("txtInputQDenum") as RadTextBox).Text = q2;
                        var ret2 = (new QualityIndicatorSurveyCollection()).ExecuteQuery(q2); 
                        val2.Value = Convert.ToDouble(ret2.Rows[0][0]);
                    }
                    catch (Exception ex2)
                    {
                        ((System.Web.UI.WebControls.Label)item.FindControl("lblErrorMessage2")).Text = "Error Denum: " + ex2.Message;
                        continue;
                    }
                    ((System.Web.UI.WebControls.Label)item.FindControl("lblErrorMessage2")).Text = string.Empty;
                }
            }
        }

        private void Print(string reportName)
        {
            var jobParameters = new PrintJobParameterCollection();

            var jobParameter = jobParameters.AddNew();
            jobParameter.Name = "SurveyID";
            jobParameter.ValueString = txtSurveyID.Text;

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = reportName;

            string script = @"var oWnd = $find('" + winPrint.ClientID + "');" + "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" + "oWnd.Show();" + "oWnd.Maximize();";
            AjaxPanel.ResponseScripts.Add(script);
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);

            grdQISurvey_RI.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                QualityIndicatorSurveyDetails = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdQISurvey_RI.Rebind();
        }

        private void PopulateGridDetail()
        {
            QualityIndicatorSurveyDetails = null;
            grdQISurvey_RI.DataSource = QualityIndicatorSurveyDetails;
            grdQISurvey_RI.MasterTableView.IsItemInserted = false;
            grdQISurvey_RI.MasterTableView.ClearEditItems();
            grdQISurvey_RI.DataBind();
        }
    }
}