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

namespace Temiang.Avicenna.Module.HR.Appraisal.Recapitulation
{
    public partial class RecapitulationList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;
        protected void Page_Init(object sender, EventArgs e)
        {
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = Request.QueryString["role"] == "svr" ? AppConstant.Program.AppraisalRecapitulation : AppConstant.Program.AppraisalRecapitulationAdmin;

            if (!IsPostBack)
            {
                txtPeriodYear.Text = DateTime.Now.Year.ToString();
                StandardReference.InitializeIncludeSpace(cboSRQuarterPeriod, AppEnum.StandardReference.QuarterPeriod);
            }
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = AppraisalParticipants;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        private DataTable AppraisalParticipants
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboEmployeeID.SelectedValue) && string.IsNullOrEmpty(txtParticipantName.Text) 
                    && string.IsNullOrEmpty(txtPeriodYear.Text) && string.IsNullOrEmpty(cboSRQuarterPeriod.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Scoring Recapitulation")) return null;

                var employeeId = string.IsNullOrEmpty(cboEmployeeID.SelectedValue) ? -1 : cboEmployeeID.SelectedValue.ToInt();
                var coll = new AppraisalParticipantItemCollection();
                DataTable dtb = coll.GetScoringRecapitulationsList(employeeId, txtParticipantName.Text, txtPeriodYear.Text, cboSRQuarterPeriod.SelectedValue, rblStatus.SelectedIndex, Request.QueryString["role"].ToString(), AppSession.UserLogin.PersonID.ToInt());

                return dtb;
            }
        }

        protected void cboEmployeeID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboEmployeeID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var vw = new VwEmployeeTableQuery();
            vw.es.Top = 20;
            vw.Where(vw.EmployeeName.Like(searchTextContain));
            (o as RadComboBox).DataSource = vw.LoadDataTable();
            (o as RadComboBox).DataBind();
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (!(source is RadGrid)) return;

            if (eventArgument == "rebind")
                grdList.Rebind();

            else if (eventArgument.Contains("print"))
            {
                var pid = eventArgument.Split('|')[1].ToString();

                var jobParameters = new PrintJobParameterCollection();

                var jobParameter = jobParameters.AddNew();
                jobParameter.Name = "p_ParticipantItemID";
                jobParameter.ValueString = pid;

                var parUserID = jobParameters.AddNew();
                parUserID.Name = "p_UserID";
                parUserID.ValueString = AppSession.UserLogin.UserID;

                AppSession.PrintJobReportID = AppConstant.Report.Appraisal1;

                AppSession.PrintJobParameters = jobParameters;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                                "oWnd.Show();" +
                                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
        }
    }
}