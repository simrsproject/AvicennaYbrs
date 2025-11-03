using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using System.Drawing;

namespace Temiang.Avicenna.Module.HR.Appraisal.Recapitulation
{
    public partial class RecapitulationDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = Request.QueryString["role"] == "svr" ? AppConstant.Program.AppraisalRecapitulation : AppConstant.Program.AppraisalRecapitulationAdmin;

            UrlPageSearch = "##";
            UrlPageList = "RecapitulationList.aspx?role=" + Request.QueryString["role"];

            txtParticipantItemID.Text = string.IsNullOrEmpty(Request.QueryString["pid"]) ? "0" : Request.QueryString["pid"].ToString();

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRQuarterPeriod, AppEnum.StandardReference.QuarterPeriod);

                var std = new AppStandardReferenceItem();
                grdListScores.Columns.FindByUniqueName("SupervisorScoreX").Visible = std.LoadByPrimaryKey(AppEnum.StandardReference.EvaluatorType.ToString(), "001") && std.IsActive == true;
                std = new AppStandardReferenceItem();
                grdListScores.Columns.FindByUniqueName("PartnerScoreX").Visible = std.LoadByPrimaryKey(AppEnum.StandardReference.EvaluatorType.ToString(), "002") && std.IsActive == true;
                std = new AppStandardReferenceItem();
                grdListScores.Columns.FindByUniqueName("SubordinateScoreX").Visible = std.LoadByPrimaryKey(AppEnum.StandardReference.EvaluatorType.ToString(), "003") && std.IsActive == true;
                std = new AppStandardReferenceItem();
                grdListScores.Columns.FindByUniqueName("SelfScoreX").Visible = std.LoadByPrimaryKey(AppEnum.StandardReference.EvaluatorType.ToString(), "004") && std.IsActive == true;
                std = new AppStandardReferenceItem();
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdListScores, grdListScores);
            ajax.AddAjaxSetting(grdListScores, grdListRecap);
            ajax.AddAjaxSetting(grdListScores, txtTotal);
            ajax.AddAjaxSetting(grdListScores, lblConclusion);
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new AppraisalParticipantItem());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new AppraisalParticipantItem();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new AppraisalParticipantItem();
            entity.Query.Where(entity.Query.ParticipantItemID == txtParticipantItemID.Value.ToInt());
            if (entity.Query.Load())
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
            auditLogFilter.PrimaryKeyData = string.Format("ParticipantItemID='{0}'", txtParticipantItemID.Value.ToInt());
            auditLogFilter.TableName = "AppraisalParticipantItem";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Refresh Grid Detail
            bool isVisible = (newVal != AppEnum.DataMode.Read);

            grdListScores.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            grdListNotes.Columns[2].Visible = !isVisible;
            grdListNotes.Columns[3].Visible = !isVisible;
            grdListNotes.Columns[4].Visible = isVisible;
            grdListNotes.Columns[5].Visible = isVisible;

            RefreshGrid();
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new AppraisalParticipantItem();
            if (parameters.Length > 0)
            {
                String id = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                {
                    entity.Query.Where(entity.Query.ParticipantItemID == id.ToInt());
                    entity.Query.Load();
                }
            }
            else
            {
                entity.Query.Where(entity.Query.ParticipantItemID == txtParticipantItemID.Value.ToInt());
                entity.Query.Load();
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var api = (AppraisalParticipantItem)entity;

            txtParticipantItemID.Value = api.ParticipantItemID ?? 0;

            var ap = new AppraisalParticipant();
            ap.LoadByPrimaryKey(api.ParticipantID.ToInt());

            txtPeriodYear.Text = ap.PeriodYear;
            cboSRQuarterPeriod.SelectedValue = ap.SRQuarterPeriod;

            var emp = new VwEmployeeTable();
            emp.Query.Where(emp.Query.PersonID == api.EmployeeID.ToInt());
            emp.Query.Load();

            txtEmployeeName.Text = emp.EmployeeName;
            txtEmployeeNo.Text = emp.EmployeeNumber;

            string organizationUnit = string.Empty;
            var ou = new OrganizationUnit();
            if (ou.LoadByPrimaryKey(api.OrganizationUnitID.ToInt()))
                organizationUnit = ou.OrganizationUnitName;

            ou = new OrganizationUnit();
            if (ou.LoadByPrimaryKey(api.ServiceUnitID.ToInt()))
            {
                if (organizationUnit == string.Empty)
                    organizationUnit = ou.OrganizationUnitName;
                else
                    organizationUnit += " - " + ou.OrganizationUnitName;
            }

            txtOrganizationUnitName.Text = organizationUnit;

            var pos = new Position();
            if (pos.LoadByPrimaryKey(api.PositionID.ToInt()))
                txtPositionName.Text = pos.PositionName;
            else txtPositionName.Text = string.Empty;

            ViewState["IsApproved"] = api.IsClosed ?? false;

            if (IsPostBack)
            {
                RefreshGrid();
            }

            CalcultedScore();
        }

        private void SetEntityValue(AppraisalParticipantItem entity)
        {
            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(AppraisalParticipantItem entity)
        {
            using (var trans = new esTransactionScope())
            {
                foreach (GridDataItem dataItem in grdListNotes.MasterTableView.Items)
                {
                    string questionerId = dataItem.GetDataKeyValue("QuestionerID").ToString();

                    var recap = new AppraisalScoringRecapitulation();
                    recap.Query.Where(recap.Query.ParticipantItemID == txtParticipantItemID.Value.ToInt(), recap.Query.QuestionerID == questionerId);
                    if (!recap.Query.Load())
                        recap = new AppraisalScoringRecapitulation();

                    recap.ParticipantItemID = txtParticipantItemID.Value.ToInt();
                    recap.QuestionerID = questionerId.ToInt();

                    string capacity = ((RadTextBox)dataItem.FindControl("txtCapacity")).Text;
                    string developed = ((RadTextBox)dataItem.FindControl("txtNeedsToBeDeveloped")).Text;

                    recap.Capacity = capacity;
                    recap.NeedsToBeDeveloped = developed;

                    recap.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    recap.LastUpdateDateTime = DateTime.Now;

                    recap.Save();
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new AppraisalParticipantItemQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ParticipantItemID > txtParticipantItemID.Value.ToInt());
                que.OrderBy(que.ParticipantItemID.Ascending);
            }
            else
            {
                que.Where(que.ParticipantItemID < txtParticipantItemID.Value.ToInt());
                que.OrderBy(que.ParticipantItemID.Descending);
            }
            var entity = new AppraisalParticipantItem();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_ParticipantItemID", txtParticipantItemID.Text);
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new AppraisalParticipantItem();
            entity.Query.Where(entity.Query.ParticipantItemID == txtParticipantItemID.Value.ToInt());
            if (!entity.Query.Load())
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            var asas = new AppraisalScoringAccumulationCollection();
            asas.Query.Where(asas.Query.ParticipantItemID == txtParticipantItemID.Value.ToInt());
            asas.LoadAll();
            if (asas.Count == 0)
            {
                args.MessageText = "This data has not been recapitulated. Approved not allowed.";
                args.IsCancel = true;
                return;
            }

            using (var trans = new esTransactionScope())
            {
                entity.IsClosed = true;
                entity.ClosedByUserID = AppSession.UserLogin.UserID;
                entity.ClosedDateTime = DateTime.Now;
                
                entity.Save();

                /*--------------------------------------------------------*/

                var yearPeriod = string.Empty;
                var quarter = string.Empty;
                var notes = string.Empty;
                var ap = new AppraisalParticipant();
                if (ap.LoadByPrimaryKey(entity.ParticipantID.ToInt()))
                {
                    yearPeriod = ap.PeriodYear;
                    quarter = ap.SRQuarterPeriod;
                    notes = ap.ParticipantName;
                }

                var epa = new EmployeePerformanceAppraisal();
                epa.AddNew();
                epa.PersonID = entity.EmployeeID;
                epa.ParticipantItemID = entity.ParticipantItemID;
                epa.YearPeriod = yearPeriod;
                epa.SRQuarterPeriod = quarter;
                epa.Score = Convert.ToDecimal(txtTotal.Value);
                epa.ScoreText = lblConclusion.Text;
                epa.Notes = notes;
                epa.LastUpdateDateTime = DateTime.Now;
                epa.LastUpdateByUserID = AppSession.UserLogin.UserID;
                
                epa.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
        }

        public override bool OnGetStatusMenuEdit()
        {
            return txtParticipantItemID.Value.ToInt() > 0;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !(bool)ViewState["IsApproved"];
        }

        #region Detail

        protected void grdListScores_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdListScores.DataSource = AppraisalScoringAccumulationScoress;
        }

        protected void grdListScores_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    var dataItem = e.Item as GridDataItem;
                    if (dataItem["IsScoringEnabled"].Text == "0")
                    {
                        //dataItem.ForeColor = Color.DarkBlue;
                        dataItem.Font.Bold = true;
                        dataItem.Font.Italic = true;
                    }
                }
            }
            catch
            { }
        }

        protected void grdListRecap_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdListRecap.DataSource = AppraisalScoringAccumulationRecaps;
        }

        protected void grdListNotes_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdListNotes.DataSource = AppraisalScoringRecapitulations;
        }

        protected void grdListConclusion_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdListConclusion.DataSource = AppraisalConclusions;
        }

        private DataTable AppraisalScoringAccumulationScoress
        {
            get
            {
                object obj = this.Session["AppraisalScoringAccumulationScoress" + Request.UserHostName];
                if (obj != null)
                    return ((DataTable)(obj));

                AppraisalParticipantItemCollection coll = new AppraisalParticipantItemCollection();

                DataTable dtb = coll.GetAccumulationScores(txtParticipantItemID.Value.ToString());

                Session["AppraisalScoringAccumulationScoress" + Request.UserHostName] = dtb;
                return dtb;
            }
        }

        private DataTable AppraisalScoringAccumulationRecaps
        {
            get
            {
                object obj = this.Session["AppraisalScoringAccumulationRecaps" + Request.UserHostName];
                if (obj != null)
                    return ((DataTable)(obj));

                AppraisalParticipantItemCollection coll = new AppraisalParticipantItemCollection();
                DataTable dtb = coll.GetAccumulationRecap(txtParticipantItemID.Value.ToString());

                Session["AppraisalScoringAccumulationRecaps" + Request.UserHostName] = dtb;
                return dtb;
            }
        }

        private DataTable AppraisalScoringRecapitulations
        {
            get
            {
                object obj = this.Session["AppraisalScoringRecapitulations" + Request.UserHostName];
                if (obj != null)
                    return ((DataTable)(obj));

                AppraisalParticipantItemCollection coll = new AppraisalParticipantItemCollection();
                DataTable dtb = coll.GetScoringRecapitulations(txtParticipantItemID.Value.ToString());

                Session["AppraisalScoringRecapitulations" + Request.UserHostName] = dtb;
                return dtb;
            }
        }

        private DataTable AppraisalConclusions
        {
            get
            {
                object obj = this.Session["AppraisalConclusions" + Request.UserHostName];
                if (obj != null)
                    return ((DataTable)(obj));

                var query = new AppraisalConclusionQuery("a");
                query.Select
                    (
                        query.ConclusionID,
                        query.ConclusionName,
                        query.MinValue,
                        query.MaxValue
                    );
                query.OrderBy(query.MinValue.Ascending);
                DataTable dtb = query.LoadDataTable();

                Session["AppraisalConclusions" + Request.UserHostName] = dtb;
                return dtb;
            }
        }

        private void RefreshGrid()
        {
            Session["AppraisalScoringAccumulationScoress" + Request.UserHostName] = null;
            grdListScores.Rebind();

            Session["AppraisalScoringAccumulationRecaps" + Request.UserHostName] = null;
            grdListRecap.Rebind();

            Session["AppraisalScoringRecapitulations" + Request.UserHostName] = null;
            grdListNotes.Rebind();

            Session["AppraisalConclusions" + Request.UserHostName] = null;
            grdListConclusion.Rebind();
        }

        private void CalcultedScore()
        {
            decimal total = -1;

            if (AppSession.Parameter.AppraisalVersionNo == "3")
            {
                AppraisalParticipantItemCollection coll = new AppraisalParticipantItemCollection();
                DataTable dtb = coll.GetAccumulationRecap(txtParticipantItemID.Value.ToString());

                //object obj = Session["AppraisalScoringAccumulationRecaps" + Request.UserHostName];
                //if (obj == null)
                //    return;

                //DataTable dtb = (DataTable)obj;

                if (dtb.Rows.Count > 0)
                {
                    total = 0;
                    foreach (DataRow row in dtb.Rows)
                    {
                        total += Convert.ToDecimal(row["TotalScore"]);
                    }
                }
            }
            else
            {
                var coll = new AppraisalScoringAccumulationCollection();
                coll.Query.Where(coll.Query.ParticipantItemID == txtParticipantItemID.Value.ToInt());
                coll.LoadAll();
                if (coll.Count > 0)
                {
                    total = 0;
                    foreach (var i in coll)
                    {
                        total += i.AverageScore ?? 0;
                    }
                }
            }

            txtTotal.Value = Convert.ToDouble(total);

            var con = new AppraisalConclusion();
            con.Query.Where(con.Query.MinValue <= total, con.Query.MaxValue >= total);
            if (con.Query.Load())
                lblConclusion.Text = con.ConclusionName;
            else
                lblConclusion.Text = string.Empty;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(source is RadGrid))
                return;

            switch (eventArgument)
            {
                case "calculation":
                    Validate();
                    if (!IsValid)
                        return;

                    //Process();

                    if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsUsingMultipleScoringSupervisor))
                        ProcessWithMultipleSupervisor();
                    else
                        Process();

                    RefreshGrid();
                    CalcultedScore();

                    break;
            }
        }

        private void Process()
        {
            var apeColl = new AppraisalParticipantEvaluatorCollection();
            apeColl.Query.Where(apeColl.Query.ParticipantItemID == txtParticipantItemID.Value.ToInt());
            apeColl.LoadAll();
            decimal i = apeColl.Count;
            if (i == 0)
                i = 1;

            var aqi = new AppraisalQuestionItemQuery("aqi");
            var apq = new AppraisalParticipantQuestionerQuery("apq");
            aqi.InnerJoin(apq).On(aqi.QuestionerID == aqi.QuestionerID);
            aqi.Select(aqi.QuestionerItemID);
            aqi.Where(apq.ParticipantItemID == txtParticipantItemID.Value.ToInt(), aqi.Or(aqi.Rating > 0, aqi.MaxValue > 0));

            var coll = new AppraisalQuestionItemCollection();
            coll.Load(aqi);

            foreach (var item in coll)
            {
                var asa = new AppraisalScoringAccumulation();
                asa.Query.Where(asa.Query.ParticipantItemID == txtParticipantItemID.Value.ToInt(), asa.Query.QuestionerItemID == item.QuestionerItemID);
                if (!asa.Query.Load())
                    asa = new AppraisalScoringAccumulation();

                asa.ParticipantItemID = txtParticipantItemID.Value.ToInt();
                asa.QuestionerItemID = item.QuestionerItemID;

                var x = new AppraisalScoresheetQuery("x");
                var xx = new AppraisalScoresheetItemQuery("xx");
                x.InnerJoin(xx).On(xx.ScoresheetID == x.ScoresheetID);
                x.Where(x.ParticipantItemID == txtParticipantItemID.Value.ToInt(), x.ReferenceID.IsNull(), x.SREvaluatorType == "001", x.IsApproved == true, xx.QuestionerItemID == item.QuestionerItemID);
                if (AppSession.Parameter.AppraisalVersionNo == "2")
                    x.Select(xx.TotalScore.Coalesce("0"));
                else
                    x.Select(xx.Score.Coalesce("0").As("TotalScore"));

                DataTable dtb = x.LoadDataTable();
                if (dtb.Rows.Count > 0)
                    asa.SupervisorScore = Convert.ToDecimal(dtb.Rows[0]["TotalScore"]);
                else
                    asa.SupervisorScore = 0;

                x = new AppraisalScoresheetQuery("x");
                xx = new AppraisalScoresheetItemQuery("xx");
                x.InnerJoin(xx).On(xx.ScoresheetID == x.ScoresheetID);
                x.Where(x.ParticipantItemID == txtParticipantItemID.Value.ToInt(), x.ReferenceID.IsNull(), x.SREvaluatorType == "002", x.IsApproved == true, xx.QuestionerItemID == item.QuestionerItemID);
                if (AppSession.Parameter.AppraisalVersionNo == "2")
                    x.Select(xx.TotalScore.Coalesce("0"));
                else
                    x.Select(xx.Score.Coalesce("0").As("TotalScore"));
                dtb = x.LoadDataTable();
                if (dtb.Rows.Count > 0)
                    asa.PartnerScore = Convert.ToDecimal(dtb.Rows[0]["TotalScore"]);
                else
                    asa.PartnerScore = 0;

                x = new AppraisalScoresheetQuery("x");
                xx = new AppraisalScoresheetItemQuery("xx");
                x.InnerJoin(xx).On(xx.ScoresheetID == x.ScoresheetID);
                x.Where(x.ParticipantItemID == txtParticipantItemID.Value.ToInt(), x.ReferenceID.IsNull(), x.SREvaluatorType == "003", x.IsApproved == true, xx.QuestionerItemID == item.QuestionerItemID);
                if (AppSession.Parameter.AppraisalVersionNo == "2")
                    x.Select(xx.TotalScore.Coalesce("0"));
                else
                    x.Select(xx.Score.Coalesce("0").As("TotalScore"));
                dtb = x.LoadDataTable();
                if (dtb.Rows.Count > 0)
                    asa.SubordinateScore = Convert.ToDecimal(dtb.Rows[0]["TotalScore"]);
                else
                    asa.SubordinateScore = 0;

                x = new AppraisalScoresheetQuery("x");
                xx = new AppraisalScoresheetItemQuery("xx");
                x.InnerJoin(xx).On(xx.ScoresheetID == x.ScoresheetID);
                x.Where(x.ParticipantItemID == txtParticipantItemID.Value.ToInt(), x.ReferenceID.IsNull(), x.SREvaluatorType == "004", x.IsApproved == true, xx.QuestionerItemID == item.QuestionerItemID);
                if (AppSession.Parameter.AppraisalVersionNo == "2")
                    x.Select(xx.TotalScore.Coalesce("0"));
                else
                    x.Select(xx.Score.Coalesce("0").As("TotalScore"));
                dtb = x.LoadDataTable();
                if (dtb.Rows.Count > 0)
                    asa.SelfScore = Convert.ToDecimal(dtb.Rows[0]["TotalScore"]);
                else
                    asa.SelfScore = 0;

                x = new AppraisalScoresheetQuery("x");
                xx = new AppraisalScoresheetItemQuery("xx");
                x.InnerJoin(xx).On(xx.ScoresheetID == x.ScoresheetID);
                x.Where(x.ParticipantItemID == txtParticipantItemID.Value.ToInt(), x.ReferenceID.IsNotNull(), x.SREvaluatorType == "001", x.IsApproved == true, xx.QuestionerItemID == item.QuestionerItemID);
                if (AppSession.Parameter.AppraisalVersionNo == "2")
                    x.Select(xx.TotalScore.Coalesce("0"));
                else
                    x.Select(xx.Score.Coalesce("0").As("TotalScore"));
                dtb = x.LoadDataTable();
                if (dtb.Rows.Count > 0)
                    asa.SupervisorScoreIntervention = Convert.ToDecimal(dtb.Rows[0]["TotalScore"]);
                else
                    asa.SupervisorScoreIntervention = 0;

                x = new AppraisalScoresheetQuery("x");
                xx = new AppraisalScoresheetItemQuery("xx");
                x.InnerJoin(xx).On(xx.ScoresheetID == x.ScoresheetID);
                x.Where(x.ParticipantItemID == txtParticipantItemID.Value.ToInt(), x.ReferenceID.IsNotNull(), x.SREvaluatorType == "002", x.IsApproved == true, xx.QuestionerItemID == item.QuestionerItemID);
                if (AppSession.Parameter.AppraisalVersionNo == "2")
                    x.Select(xx.TotalScore.Coalesce("0"));
                else
                    x.Select(xx.Score.Coalesce("0").As("TotalScore"));
                dtb = x.LoadDataTable();
                if (dtb.Rows.Count > 0)
                    asa.PartnerScoreIntervention = Convert.ToDecimal(dtb.Rows[0]["TotalScore"]);
                else
                    asa.PartnerScoreIntervention = 0;

                x = new AppraisalScoresheetQuery("x");
                xx = new AppraisalScoresheetItemQuery("xx");
                x.InnerJoin(xx).On(xx.ScoresheetID == x.ScoresheetID);
                x.Where(x.ParticipantItemID == txtParticipantItemID.Value.ToInt(), x.ReferenceID.IsNotNull(), x.SREvaluatorType == "003", x.IsApproved == true, xx.QuestionerItemID == item.QuestionerItemID);
                if (AppSession.Parameter.AppraisalVersionNo == "2")
                    x.Select(xx.TotalScore.Coalesce("0"));
                else
                    x.Select(xx.Score.Coalesce("0").As("TotalScore"));
                dtb = x.LoadDataTable();
                if (dtb.Rows.Count > 0)
                    asa.SubordinateScoreIntervention = Convert.ToDecimal(dtb.Rows[0]["TotalScore"]);
                else
                    asa.SubordinateScoreIntervention = 0;

                x = new AppraisalScoresheetQuery("x");
                xx = new AppraisalScoresheetItemQuery("xx");
                x.InnerJoin(xx).On(xx.ScoresheetID == x.ScoresheetID);
                x.Where(x.ParticipantItemID == txtParticipantItemID.Value.ToInt(), x.ReferenceID.IsNotNull(), x.SREvaluatorType == "004", x.IsApproved == true, xx.QuestionerItemID == item.QuestionerItemID);
                if (AppSession.Parameter.AppraisalVersionNo == "2")
                    x.Select(xx.TotalScore.Coalesce("0"));
                else
                    x.Select(xx.Score.Coalesce("0").As("TotalScore"));
                dtb = x.LoadDataTable();
                if (dtb.Rows.Count > 0)
                    asa.SelfScoreIntervention = Convert.ToDecimal(dtb.Rows[0]["TotalScore"]);
                else
                    asa.SelfScoreIntervention = 0;

                var supervisorScore = asa.SupervisorScoreIntervention == 0 ? asa.SupervisorScore : asa.SupervisorScoreIntervention;
                var partnerScore = asa.PartnerScoreIntervention == 0 ? asa.PartnerScore : asa.PartnerScoreIntervention;
                var subordinateScore = asa.SubordinateScoreIntervention == 0 ? asa.SubordinateScore : asa.SubordinateScoreIntervention;
                var selfScore = asa.SelfScoreIntervention == 0 ? asa.SelfScore : asa.SelfScoreIntervention;

                asa.AverageScore = (supervisorScore + partnerScore + subordinateScore + selfScore) / i;

                asa.LastUpdateByUserID = AppSession.UserLogin.UserID;
                asa.LastUpdateDateTime = DateTime.Now;
                asa.Save();
            }
        }

        private void ProcessWithMultipleSupervisor()
        {
            decimal i = 1;

            var aqi = new AppraisalQuestionItemQuery("aqi");
            var apq = new AppraisalParticipantQuestionerQuery("apq");
            aqi.InnerJoin(apq).On(aqi.QuestionerID == aqi.QuestionerID);
            aqi.Select(aqi.QuestionerItemID);
            aqi.Where(apq.ParticipantItemID == txtParticipantItemID.Value.ToInt(), aqi.Or(aqi.Rating > 0, aqi.MaxValue > 0));

            var coll = new AppraisalQuestionItemCollection();
            coll.Load(aqi);

            foreach (var item in coll)
            {
                var questioner = new AppraisalQuestionItem();
                questioner.LoadByPrimaryKey(item.QuestionerItemID.ToInt());

                AppraisalParticipantItemCollection divColl = new AppraisalParticipantItemCollection();
                DataTable divDtb = divColl.GetQuestionerEvalCount(txtParticipantItemID.Value.ToString(), questioner.QuestionerID.ToString());
                if (divDtb.Rows.Count > 0)
                    i = Convert.ToDecimal(divDtb.Rows[0]["EvalCount"]);

                var asa = new AppraisalScoringAccumulation();
                asa.Query.Where(asa.Query.ParticipantItemID == txtParticipantItemID.Value.ToInt(), asa.Query.QuestionerItemID == item.QuestionerItemID);
                if (!asa.Query.Load())
                    asa = new AppraisalScoringAccumulation();

                asa.ParticipantItemID = txtParticipantItemID.Value.ToInt();
                asa.QuestionerItemID = item.QuestionerItemID;

                var x = new AppraisalScoresheetQuery("x");
                var xx = new AppraisalScoresheetItemQuery("xx");
                x.InnerJoin(xx).On(xx.ScoresheetID == x.ScoresheetID);
                x.Where(x.ParticipantItemID == txtParticipantItemID.Value.ToInt(), x.ReferenceID.IsNull(), x.SREvaluatorType == "001", x.IsApproved == true, xx.QuestionerItemID == item.QuestionerItemID);
                if (AppSession.Parameter.AppraisalVersionNo == "2") //Appraisal Version No. (2: RSMM; 3:YBRSGKP)
                    x.Select(xx.TotalScore.Coalesce("0"));
                else
                    x.Select(xx.Score.Coalesce("0").As("TotalScore"));

                DataTable dtb = x.LoadDataTable();
                if (dtb.Rows.Count > 0)
                    asa.SupervisorScore = dtb.AsEnumerable().Sum(s => s.Field<decimal>("TotalScore")); //Convert.ToDecimal(dtb.Rows[0]["TotalScore"]);
                else
                    asa.SupervisorScore = 0;

                x = new AppraisalScoresheetQuery("x");
                xx = new AppraisalScoresheetItemQuery("xx");
                x.InnerJoin(xx).On(xx.ScoresheetID == x.ScoresheetID);
                x.Where(x.ParticipantItemID == txtParticipantItemID.Value.ToInt(), x.ReferenceID.IsNull(), x.SREvaluatorType == "002", x.IsApproved == true, xx.QuestionerItemID == item.QuestionerItemID);
                if (AppSession.Parameter.AppraisalVersionNo == "2")
                    x.Select(xx.TotalScore.Coalesce("0"));
                else
                    x.Select(xx.Score.Coalesce("0").As("TotalScore"));
                dtb = x.LoadDataTable();
                if (dtb.Rows.Count > 0)
                    asa.PartnerScore = dtb.AsEnumerable().Sum(s => s.Field<decimal>("TotalScore")); //Convert.ToDecimal(dtb.Rows[0]["TotalScore"]);
                else
                    asa.PartnerScore = 0;

                x = new AppraisalScoresheetQuery("x");
                xx = new AppraisalScoresheetItemQuery("xx");
                x.InnerJoin(xx).On(xx.ScoresheetID == x.ScoresheetID);
                x.Where(x.ParticipantItemID == txtParticipantItemID.Value.ToInt(), x.ReferenceID.IsNull(), x.SREvaluatorType == "003", x.IsApproved == true, xx.QuestionerItemID == item.QuestionerItemID);
                if (AppSession.Parameter.AppraisalVersionNo == "2")
                    x.Select(xx.TotalScore.Coalesce("0"));
                else
                    x.Select(xx.Score.Coalesce("0").As("TotalScore"));
                dtb = x.LoadDataTable();
                if (dtb.Rows.Count > 0)
                    asa.SubordinateScore = dtb.AsEnumerable().Sum(s => s.Field<decimal>("TotalScore")); //Convert.ToDecimal(dtb.Rows[0]["TotalScore"]);
                else
                    asa.SubordinateScore = 0;

                x = new AppraisalScoresheetQuery("x");
                xx = new AppraisalScoresheetItemQuery("xx");
                x.InnerJoin(xx).On(xx.ScoresheetID == x.ScoresheetID);
                x.Where(x.ParticipantItemID == txtParticipantItemID.Value.ToInt(), x.ReferenceID.IsNull(), x.SREvaluatorType == "004", x.IsApproved == true, xx.QuestionerItemID == item.QuestionerItemID);
                if (AppSession.Parameter.AppraisalVersionNo == "2")
                    x.Select(xx.TotalScore.Coalesce("0"));
                else
                    x.Select(xx.Score.Coalesce("0").As("TotalScore"));
                dtb = x.LoadDataTable();
                if (dtb.Rows.Count > 0)
                    asa.SelfScore = dtb.AsEnumerable().Sum(s => s.Field<decimal>("TotalScore")); //Convert.ToDecimal(dtb.Rows[0]["TotalScore"]);
                else
                    asa.SelfScore = 0;

                x = new AppraisalScoresheetQuery("x");
                xx = new AppraisalScoresheetItemQuery("xx");
                x.InnerJoin(xx).On(xx.ScoresheetID == x.ScoresheetID);
                x.Where(x.ParticipantItemID == txtParticipantItemID.Value.ToInt(), x.ReferenceID.IsNotNull(), x.SREvaluatorType == "001", x.IsApproved == true, xx.QuestionerItemID == item.QuestionerItemID);
                if (AppSession.Parameter.AppraisalVersionNo == "2")
                    x.Select(xx.TotalScore.Coalesce("0"));
                else
                    x.Select(xx.Score.Coalesce("0").As("TotalScore"));
                dtb = x.LoadDataTable();
                if (dtb.Rows.Count > 0)
                    asa.SupervisorScoreIntervention = dtb.AsEnumerable().Sum(s => s.Field<decimal>("TotalScore")); //Convert.ToDecimal(dtb.Rows[0]["TotalScore"]);
                else
                    asa.SupervisorScoreIntervention = 0;

                x = new AppraisalScoresheetQuery("x");
                xx = new AppraisalScoresheetItemQuery("xx");
                x.InnerJoin(xx).On(xx.ScoresheetID == x.ScoresheetID);
                x.Where(x.ParticipantItemID == txtParticipantItemID.Value.ToInt(), x.ReferenceID.IsNotNull(), x.SREvaluatorType == "002", x.IsApproved == true, xx.QuestionerItemID == item.QuestionerItemID);
                if (AppSession.Parameter.AppraisalVersionNo == "2")
                    x.Select(xx.TotalScore.Coalesce("0"));
                else
                    x.Select(xx.Score.Coalesce("0").As("TotalScore"));
                dtb = x.LoadDataTable();
                if (dtb.Rows.Count > 0)
                    asa.PartnerScoreIntervention = dtb.AsEnumerable().Sum(s => s.Field<decimal>("TotalScore")); //Convert.ToDecimal(dtb.Rows[0]["TotalScore"]);
                else
                    asa.PartnerScoreIntervention = 0;

                x = new AppraisalScoresheetQuery("x");
                xx = new AppraisalScoresheetItemQuery("xx");
                x.InnerJoin(xx).On(xx.ScoresheetID == x.ScoresheetID);
                x.Where(x.ParticipantItemID == txtParticipantItemID.Value.ToInt(), x.ReferenceID.IsNotNull(), x.SREvaluatorType == "003", x.IsApproved == true, xx.QuestionerItemID == item.QuestionerItemID);
                if (AppSession.Parameter.AppraisalVersionNo == "2")
                    x.Select(xx.TotalScore.Coalesce("0"));
                else
                    x.Select(xx.Score.Coalesce("0").As("TotalScore"));
                dtb = x.LoadDataTable();
                if (dtb.Rows.Count > 0)
                    asa.SubordinateScoreIntervention = dtb.AsEnumerable().Sum(s => s.Field<decimal>("TotalScore")); //Convert.ToDecimal(dtb.Rows[0]["TotalScore"]);
                else
                    asa.SubordinateScoreIntervention = 0;

                x = new AppraisalScoresheetQuery("x");
                xx = new AppraisalScoresheetItemQuery("xx");
                x.InnerJoin(xx).On(xx.ScoresheetID == x.ScoresheetID);
                x.Where(x.ParticipantItemID == txtParticipantItemID.Value.ToInt(), x.ReferenceID.IsNotNull(), x.SREvaluatorType == "004", x.IsApproved == true, xx.QuestionerItemID == item.QuestionerItemID);
                if (AppSession.Parameter.AppraisalVersionNo == "2")
                    x.Select(xx.TotalScore.Coalesce("0"));
                else
                    x.Select(xx.Score.Coalesce("0").As("TotalScore"));
                dtb = x.LoadDataTable();
                if (dtb.Rows.Count > 0)
                    asa.SelfScoreIntervention = dtb.AsEnumerable().Sum(s => s.Field<decimal>("TotalScore")); //Convert.ToDecimal(dtb.Rows[0]["TotalScore"]);
                else
                    asa.SelfScoreIntervention = 0;

                var supervisorScore = asa.SupervisorScoreIntervention == 0 ? asa.SupervisorScore : asa.SupervisorScoreIntervention;
                var partnerScore = asa.PartnerScoreIntervention == 0 ? asa.PartnerScore : asa.PartnerScoreIntervention;
                var subordinateScore = asa.SubordinateScoreIntervention == 0 ? asa.SubordinateScore : asa.SubordinateScoreIntervention;
                var selfScore = asa.SelfScoreIntervention == 0 ? asa.SelfScore : asa.SelfScoreIntervention;

                asa.AverageScore = (supervisorScore + partnerScore + subordinateScore + selfScore) / i;

                asa.LastUpdateByUserID = AppSession.UserLogin.UserID;
                asa.LastUpdateDateTime = DateTime.Now;
                asa.Save();
            }
        }

        #endregion

        protected string GetQuestionName(object questionGroupName, object questionName)
        {
            if (questionGroupName.ToString().Equals(string.Empty))
                return questionName.ToString();
            else
                return "&nbsp;&nbsp;&nbsp;" + questionName.ToString();
        }
    }
}