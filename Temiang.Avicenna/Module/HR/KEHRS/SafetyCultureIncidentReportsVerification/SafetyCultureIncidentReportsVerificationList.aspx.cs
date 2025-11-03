using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.DynamicQuery;
using System.Collections.Generic;
using System.Web;

namespace Temiang.Avicenna.Module.HR.KEHRS
{
    public partial class SafetyCultureIncidentReportsVerificationList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e); // !!Jika tidak dipanggil, tampilan jadi tidak rapih

            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.KEHRS_SafetyCultureIncidentReportsVerification;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRIncidentReportStatus, AppEnum.StandardReference.IncidentReportStatus);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsPostBack) return;

            RestoreValueFromCookie();
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

        protected void grdOutstanding_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdOutstanding.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = EmployeeSafetyCultureIncidentReportsOutstandings;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        private DataTable EmployeeSafetyCultureIncidentReportsOutstandings
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(txtTransactionNo.Text) && txtReportFromDate.IsEmpty && txtReportToDate.IsEmpty && string.IsNullOrEmpty(txtEmployeeNo.Text);
                if (!ValidateSearch(isEmptyFilter, "Asset Status")) return null;

                var query = new EmployeeSafetyCultureIncidentReportsQuery("a");
                var qemp = new VwEmployeeTableQuery("b");

                query.InnerJoin(qemp).On(qemp.PersonID == query.VictimPersonID);

                query.OrderBy
                    (
                        query.ReportDate.Descending
                    );

                query.Select(
                    query.TransactionNo,
                    query.ReportDate,
                    query.ReportDescription,
                    qemp.EmployeeNumber,
                    qemp.EmployeeName,
                    query.IsApproved,
                    query.IsVoid,
                    "<'../SafetyCultureIncidentReports/SafetyCultureIncidentReportsDetail.aspx?md=edit&id=' + a.TransactionNo + '&type=ver' as EditUrl>"
                    );
                query.Where(query.IsApproved == true, query.IsVerified.IsNull());

                if (!string.IsNullOrEmpty(txtTransactionNo.Text))
                {
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                }
                if (!txtReportFromDate.IsEmpty && !txtReportToDate.IsEmpty)
                {
                    query.Where(query.ReportDate >= txtReportFromDate.SelectedDate, query.ReportDate < txtReportToDate.SelectedDate.Value.AddDays(1));
                }
                if (!string.IsNullOrEmpty(txtEmployeeNo.Text))
                {
                    string searchTextContain = string.Format("%{0}%", txtEmployeeNo.Text);
                    query.Where(query.Or(qemp.EmployeeNumber == txtEmployeeNo.Text, 
                        qemp.EmployeeName.Like(searchTextContain)));
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                return query.LoadDataTable();
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = EmployeeSafetyCultureIncidentReportsVerifications;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        private DataTable EmployeeSafetyCultureIncidentReportsVerifications
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(txtTransactionNo.Text) && txtReportFromDate.IsEmpty && txtReportToDate.IsEmpty && string.IsNullOrEmpty(txtEmployeeNo.Text) && txtVerfiedFromDate.IsEmpty && txtVerfiedToDate.IsEmpty && string.IsNullOrEmpty(cboSRIncidentReportStatus.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Safety Culture Verification")) return null;

                var query = new EmployeeSafetyCultureIncidentReportsQuery("a");
                var qemp = new VwEmployeeTableQuery("b");
                var qic = new AppStandardReferenceItemQuery("c");

                query.InnerJoin(qemp).On(qemp.PersonID == query.VictimPersonID);
                query.LeftJoin(qic).On(qic.StandardReferenceID == AppEnum.StandardReference.EmployeeAccidentReportStatus && qic.ItemID == query.SRIncidentReportStatus);

                query.OrderBy
                    (
                        query.ReportDate.Descending
                    );

                query.Select(
                    query.TransactionNo,
                    query.ReportDate,
                    query.ReportDescription,
                    qemp.EmployeeNumber,
                    qemp.EmployeeName,
                    qic.ItemName.As("IncidentReportStatus"),
                    query.IsVerified,
                    query.VerifiedDateTime,
                    "<'../SafetyCultureIncidentReports/SafetyCultureIncidentReportsDetail.aspx?md=view&id=' + a.TransactionNo + '&type=ver' as ViewUrl>"
                    );
                query.Where(query.IsApproved == true, query.IsVerified.IsNotNull());

                if (!string.IsNullOrEmpty(txtTransactionNo.Text))
                {
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                }
                if (!txtReportFromDate.IsEmpty && !txtReportToDate.IsEmpty)
                {
                    query.Where(query.ReportDate >= txtReportFromDate.SelectedDate, query.ReportDate < txtReportToDate.SelectedDate.Value.AddDays(1));
                }
                if (!string.IsNullOrEmpty(txtEmployeeNo.Text))
                {
                    string searchTextContain = string.Format("%{0}%", txtEmployeeNo.Text);
                    query.Where(query.Or(qemp.EmployeeNumber == txtEmployeeNo.Text, 
                        qemp.EmployeeName.Like(searchTextContain)));
                }
                if (!txtVerfiedFromDate.IsEmpty && !txtVerfiedToDate.IsEmpty)
                {
                    query.Where(query.VerifiedDateTime >= txtVerfiedFromDate.SelectedDate, query.VerifiedDateTime < txtVerfiedToDate.SelectedDate.Value.AddDays(1));
                }
                if (!string.IsNullOrEmpty(cboSRIncidentReportStatus.SelectedValue))
                {
                    query.Where(query.SRIncidentReportStatus == cboSRIncidentReportStatus.SelectedValue);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                return query.LoadDataTable();
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();
            grdOutstanding.Rebind();
            grdList.Rebind();
        }

        protected void btnFilterVerif_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();
            grdList.Rebind();
        }
    }
}