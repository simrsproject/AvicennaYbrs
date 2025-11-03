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
    public partial class NeedleStickInjuryList : BasePage
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

            ProgramID = AppConstant.Program.K3RS_EmployeeNeedleStickInjury;

            if (!IsPostBack)
            {
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
            var dataSource = EmployeeIncidentOutstandings;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        private DataTable EmployeeIncidentOutstandings
        {
            get
            {
                var isEmptyFilter = txtIncidentFromDate.IsEmpty && txtIncidentToDate.IsEmpty && string.IsNullOrEmpty(txtTransactionNo.Text) 
                    && string.IsNullOrEmpty(txtEmployeeName.Text) && string.IsNullOrEmpty(txtEmployeeNumber.Text);
                if (!ValidateSearch(isEmptyFilter, "Employee Needle Stick Injury ")) return null;

                var query = new EmployeeAccidentReportsQuery("a");
                var qemp = new VwEmployeeTableQuery("b");
                var qpos = new PositionQuery("c");
                var qic = new AppStandardReferenceItemQuery("d");
                var qis = new AppStandardReferenceItemQuery("e");
                var qit = new AppStandardReferenceItemQuery("f");
                var qin = new AppStandardReferenceItemQuery("g");
                var qnis = new EmployeeNeedleStickInjuryQuery("h");

                query.InnerJoin(qemp).On(qemp.PersonID == query.PersonID);
                query.LeftJoin(qpos).On(qpos.PositionID == qemp.PositionID);
                query.LeftJoin(qic).On(qic.StandardReferenceID == AppEnum.StandardReference.EmployeeInjuryCategory && qic.ItemID == query.SREmployeeInjuryCategory);
                query.LeftJoin(qis).On(qis.StandardReferenceID == AppEnum.StandardReference.EmployeeIncidentStatus && qis.ItemID == query.SREmployeeIncidentStatus);
                query.LeftJoin(qit).On(qit.StandardReferenceID == AppEnum.StandardReference.EmployeeIncidentType && qit.ItemID == query.SREmployeeIncidentType);
                query.LeftJoin(qin).On(qin.StandardReferenceID == AppEnum.StandardReference.NeedleType && qin.ItemID == query.SRNeedleType);
                query.LeftJoin(qnis).On(qnis.ReferenceNo == query.TransactionNo && qnis.IsVoid == false);

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
                    "<'NeedleStickInjuryDetail.aspx?md=new&id=&rno=' + a.TransactionNo as NewUrl>"
                    );

                query.Where(query.IsVerified == true, 
                    query.Or(
                        query.SREmployeeIncidentType.In(AppSession.Parameter.EmployeeIncidentTypeEBF), 
                        query.SRNeedleType.In(AppSession.Parameter.NeedleTypeNSI)), 
                    qnis.TransactionDate.IsNull());

                if (!txtIncidentFromDate.IsEmpty && !txtIncidentToDate.IsEmpty)
                    query.Where(query.IncidentDateTime >= txtIncidentFromDate.SelectedDate, query.IncidentDateTime < txtIncidentToDate.SelectedDate.Value.AddDays(1));
                if (!string.IsNullOrEmpty(txtTransactionNo.Text))
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                if (!string.IsNullOrEmpty(txtEmployeeName.Text))
                {
                    string searchEmpName = string.Format("%{0}%", txtEmployeeName.Text);
                    query.Where(qemp.EmployeeName.Like(searchEmpName));
                }
                //if (txtEmployeeName.Text != string.Empty)
                //{
                //    string searchEmpName = "%" + txtEmployeeName.Text + "%";
                //    query.Where(string.Format("<b.EmployeeName LIKE '{0}' OR >", searchEmpName));
                //}
                if (txtEmployeeNumber.Text != string.Empty)
                {
                    string searchEmpNo = "%" + txtEmployeeNumber.Text + "%";
                    query.Where(string.Format("<b.EmployeeNumber LIKE '{0}'>", searchEmpNo));
                }

                return query.LoadDataTable();
            }
        }

        protected void grdVerification_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdVerification.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = EmployeeNeedleStickInjurys;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        private DataTable EmployeeNeedleStickInjurys
        {
            get
            {
                var isEmptyFilter = txtIncidentFromDate.IsEmpty && txtIncidentToDate.IsEmpty && string.IsNullOrEmpty(txtTransactionNo.Text)
                    && string.IsNullOrEmpty(txtEmployeeName.Text) && string.IsNullOrEmpty(txtEmployeeNumber.Text);
                if (!ValidateSearch(isEmptyFilter, "Employee Needle Stick Injury ")) return null;

                var query = new EmployeeNeedleStickInjuryQuery("a");
                var qei = new EmployeeAccidentReportsQuery("b");
                var qemp = new VwEmployeeTableQuery("c");
                var qpos = new PositionQuery("d");

                query.InnerJoin(qei).On(qei.TransactionNo == query.ReferenceNo);
                query.InnerJoin(qemp).On(qemp.PersonID == qei.PersonID);
                query.LeftJoin(qpos).On(qpos.PositionID == qemp.PositionID);

                query.OrderBy
                    (
                        query.TransactionNo.Descending
                    );

                query.Select(
                    query.TransactionNo,
                    query.TransactionDate,
                    qei.IncidentDateTime,
                    qei.PersonID,
                    qemp.EmployeeNumber,
                    qemp.EmployeeName,
                    qpos.PositionName,
                    query.ExposedArea,
                    query.Reason,
                    
                    query.FollowUpDate,
                    query.FollowUpBy,
                    query.IsApproved,
                    query.IsVerified,
                    query.IsVoid,
                    "<'NeedleStickInjuryDetail.aspx?md=view&id=' + a.TransactionNo + '&rno=' as ViewUrl>"
                    );

                if (!txtIncidentFromDate.IsEmpty && !txtIncidentToDate.IsEmpty)
                    query.Where(qei.IncidentDateTime >= txtIncidentFromDate.SelectedDate, qei.IncidentDateTime < txtIncidentToDate.SelectedDate.Value.AddDays(1));
                if (!string.IsNullOrEmpty(txtTransactionNo.Text))
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                if (!string.IsNullOrEmpty(txtEmployeeName.Text))
                {
                    string searchEmpName = string.Format("%{0}%", txtEmployeeName.Text);
                    query.Where(qemp.EmployeeName.Like(searchEmpName));
                }
                //if (txtEmployeeName.Text != string.Empty)
                //{
                //    string searchEmpName = "%" + txtEmployeeName.Text + "%";
                //    query.Where(string.Format("<c.EmployeeName LIKE '{0}' OR >", searchEmpName));
                //}
                if (txtEmployeeNumber.Text != string.Empty)
                {
                    string searchEmpNo = "%" + txtEmployeeNumber.Text + "%";
                    query.Where(string.Format("<c.EmployeeNumber LIKE '{0}'>", searchEmpNo));
                }

                return query.LoadDataTable();
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdOutstanding.Rebind();
            grdVerification.Rebind();
        }
    }
}