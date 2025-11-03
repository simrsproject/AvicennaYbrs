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

namespace Temiang.Avicenna.Module.HR.K3RS
{
    public partial class AccidentsAndIncidentsReportsVerificationList : BasePage
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

            ProgramID = AppConstant.Program.K3RS_EmployeeIncidentVerification;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSREmployeeIncidentType, AppEnum.StandardReference.EmployeeIncidentType);
                StandardReference.InitializeIncludeSpace(cboSREmployeeInjuryCategory, AppEnum.StandardReference.EmployeeInjuryCategory);
                StandardReference.InitializeIncludeSpace(cboSREmployeeIncidentStatus, AppEnum.StandardReference.EmployeeIncidentStatus);
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
            var dataSource = EmployeeAccidentReportsOutstandings;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        private DataTable EmployeeAccidentReportsOutstandings
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(txtTransactionNo.Text) && txtIncidentFromDate.IsEmpty && txtIncidentToDate.IsEmpty 
                    && string.IsNullOrEmpty(txtEmployeeNo.Text) && string.IsNullOrEmpty(cboSREmployeeIncidentType.SelectedValue) 
                    && string.IsNullOrEmpty(cboSREmployeeInjuryCategory.SelectedValue) && string.IsNullOrEmpty(cboSREmployeeIncidentStatus.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Accidents and Incidents Reports")) return null;

                var query = new EmployeeAccidentReportsQuery("a");
                var qemp = new VwEmployeeTableQuery("b");
                var qpos = new PositionQuery("c");
                var qic = new AppStandardReferenceItemQuery("d");
                var qis = new AppStandardReferenceItemQuery("e");
                var qit = new AppStandardReferenceItemQuery("f");
                var qin = new AppStandardReferenceItemQuery("g");

                query.InnerJoin(qemp).On(qemp.PersonID == query.PersonID);
                query.LeftJoin(qpos).On(qpos.PositionID == qemp.PositionID);
                query.LeftJoin(qic).On(qic.StandardReferenceID == AppEnum.StandardReference.EmployeeInjuryCategory && qic.ItemID == query.SREmployeeInjuryCategory);
                query.LeftJoin(qis).On(qis.StandardReferenceID == AppEnum.StandardReference.EmployeeIncidentStatus && qis.ItemID == query.SREmployeeIncidentStatus);
                query.LeftJoin(qit).On(qit.StandardReferenceID == AppEnum.StandardReference.EmployeeIncidentType && qit.ItemID == query.SREmployeeIncidentType);
                query.LeftJoin(qin).On(qin.StandardReferenceID == AppEnum.StandardReference.NeedleType && qin.ItemID == query.SRNeedleType);

                query.OrderBy
                    (
                        query.IncidentDateTime.Descending
                    );

                query.Select(
                    query.TransactionNo,
                    query.ReportingDateTime,
                    query.IncidentDateTime,
                    query.PersonID,
                    qemp.EmployeeNumber,
                    qemp.EmployeeName,
                    qpos.PositionName,
                    qic.ItemName.As("EmployeeInjuryCategory"),
                    qis.ItemName.As("EmployeeIncidentStatus"),
                    qit.ItemName.As("EmployeeIncidentType"),
                    qin.ItemName.As("NeedleType"),
                    query.IsApproved,
                    query.IsVoid,
                    "<'AccidentsAndIncidentsReportsDetail.aspx?md=edit&id=' + a.TransactionNo + '&type=ver' as EditUrl>"
                    );
                query.Where(query.IsApproved == true, query.IsVerified.IsNull());

                if (!string.IsNullOrEmpty(txtTransactionNo.Text))
                {
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                }
                if (!txtIncidentFromDate.IsEmpty && !txtIncidentToDate.IsEmpty)
                {
                    query.Where(query.IncidentDateTime >= txtIncidentFromDate.SelectedDate, query.IncidentDateTime < txtIncidentToDate.SelectedDate.Value.AddDays(1));
                }
                if (!string.IsNullOrEmpty(txtEmployeeNo.Text))
                {
                    string searchText = string.Format("%{0}%", txtEmployeeNo.Text);
                    query.Where(query.Or(qemp.EmployeeNumber == txtEmployeeNo.Text, 
                        qemp.EmployeeName.Like(searchText)));
                }
                if (!string.IsNullOrEmpty(cboSREmployeeIncidentType.SelectedValue))
                {
                    query.Where(query.SREmployeeIncidentType == cboSREmployeeIncidentType.SelectedValue);
                }
                if (!string.IsNullOrEmpty(cboSREmployeeInjuryCategory.SelectedValue))
                {
                    query.Where(query.SREmployeeInjuryCategory == cboSREmployeeInjuryCategory.SelectedValue);
                }
                if (!string.IsNullOrEmpty(cboSREmployeeIncidentStatus.SelectedValue))
                {
                    query.Where(query.SREmployeeIncidentStatus == cboSREmployeeIncidentStatus.SelectedValue);
                }

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
            var dataSource = EmployeeAccidentReportsVerifications;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        private DataTable EmployeeAccidentReportsVerifications
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(txtTransactionNo.Text) && txtIncidentFromDate.IsEmpty && txtIncidentToDate.IsEmpty 
                    && string.IsNullOrEmpty(txtEmployeeNo.Text) && string.IsNullOrEmpty(cboSREmployeeIncidentType.SelectedValue) 
                    && string.IsNullOrEmpty(cboSREmployeeInjuryCategory.SelectedValue) && string.IsNullOrEmpty(cboSREmployeeIncidentStatus.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Accidents and Incidents Reports")) return null;

                var query = new EmployeeAccidentReportsQuery("a");
                var qemp = new VwEmployeeTableQuery("b");
                var qpos = new PositionQuery("c");
                var qic = new AppStandardReferenceItemQuery("d");
                var qis = new AppStandardReferenceItemQuery("e");
                var qit = new AppStandardReferenceItemQuery("f");
                var qin = new AppStandardReferenceItemQuery("g");

                query.InnerJoin(qemp).On(qemp.PersonID == query.PersonID);
                query.LeftJoin(qpos).On(qpos.PositionID == qemp.PositionID);
                query.LeftJoin(qic).On(qic.StandardReferenceID == AppEnum.StandardReference.EmployeeInjuryCategory && qic.ItemID == query.SREmployeeInjuryCategory);
                query.LeftJoin(qis).On(qis.StandardReferenceID == AppEnum.StandardReference.EmployeeIncidentStatus && qis.ItemID == query.SREmployeeIncidentStatus);
                query.LeftJoin(qit).On(qit.StandardReferenceID == AppEnum.StandardReference.EmployeeIncidentType && qit.ItemID == query.SREmployeeIncidentType);
                query.LeftJoin(qin).On(qin.StandardReferenceID == AppEnum.StandardReference.NeedleType && qin.ItemID == query.SRNeedleType);

                query.OrderBy
                    (
                        query.IncidentDateTime.Descending
                    );

                query.Select(
                    query.TransactionNo,
                    query.ReportingDateTime,
                    query.IncidentDateTime,
                    query.PersonID,
                    qemp.EmployeeNumber,
                    qemp.EmployeeName,
                    qpos.PositionName,
                    qic.ItemName.As("EmployeeInjuryCategory"),
                    qis.ItemName.As("EmployeeIncidentStatus"),
                    qit.ItemName.As("EmployeeIncidentType"),
                    qin.ItemName.As("NeedleType"),
                    query.IsApproved,
                    query.IsVerified,
                    query.IsVoid,
                    "<'AccidentsAndIncidentsReportsDetail.aspx?md=view&id=' + a.TransactionNo + '&type=ver' as ViewUrl>"
                    );
                query.Where(query.IsApproved == true, query.IsVerified.IsNotNull());

                if (!string.IsNullOrEmpty(txtTransactionNo.Text))
                {
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                }
                if (!txtIncidentFromDate.IsEmpty && !txtIncidentToDate.IsEmpty)
                {
                    query.Where(query.IncidentDateTime >= txtIncidentFromDate.SelectedDate, query.IncidentDateTime < txtIncidentToDate.SelectedDate.Value.AddDays(1));
                }
                if (!string.IsNullOrEmpty(txtEmployeeNo.Text))
                {
                    string searchText = string.Format("%{0}%", txtEmployeeNo.Text);
                    query.Where(query.Or(qemp.EmployeeNumber == txtEmployeeNo.Text, 
                        qemp.EmployeeName.Like(searchText)));
                }
                if (!string.IsNullOrEmpty(cboSREmployeeIncidentType.SelectedValue))
                {
                    query.Where(query.SREmployeeIncidentType == cboSREmployeeIncidentType.SelectedValue);
                }
                if (!string.IsNullOrEmpty(cboSREmployeeInjuryCategory.SelectedValue))
                {
                    query.Where(query.SREmployeeInjuryCategory == cboSREmployeeInjuryCategory.SelectedValue);
                }
                if (!string.IsNullOrEmpty(cboSREmployeeIncidentStatus.SelectedValue))
                {
                    query.Where(query.SREmployeeIncidentStatus == cboSREmployeeIncidentStatus.SelectedValue);
                }

                return query.LoadDataTable();
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();
            grdOutstanding.Rebind();
            grdList.Rebind();
        }
    }
}