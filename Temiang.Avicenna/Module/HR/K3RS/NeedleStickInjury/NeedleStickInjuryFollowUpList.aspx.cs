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
    public partial class NeedleStickInjuryFollowUpList : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e); // !!Jika tidak dipanggil, tampilan jadi tidak rapih

            ProgramID = AppConstant.Program.K3RS_EmployeeNeedleStickInjuryFollowUp;

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

        protected void grdOutstanding_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdOutstanding.DataSource = EmployeeIncidentOutstandings;
        }

        private DataTable EmployeeIncidentOutstandings
        {
            get
            {
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
                    "<'NeedleStickInjuryDetail.aspx?md=view&id=' + a.TransactionNo + '&rno=&type=ver' as ViewUrl>"
                    );

                query.Where(query.IsApproved == true, query.FollowUpDate.IsNull());

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
                //    query.Where(string.Format("<b.EmployeeName LIKE '{0}' OR >", searchEmpName));
                //}
                if (txtEmployeeNumber.Text != string.Empty)
                {
                    string searchEmpNo = "%" + txtEmployeeNumber.Text + "%";
                    query.Where(string.Format("<b.EmployeeName LIKE '{0}'>", searchEmpNo));
                }

                return query.LoadDataTable();
            }
        }

        protected void grdVerification_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdVerification.DataSource = EmployeeNeedleStickInjurys;
        }

        private DataTable EmployeeNeedleStickInjurys
        {
            get
            {
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
                    "<'NeedleStickInjuryDetail.aspx?md=view&id=' + a.TransactionNo + '&rno=&type=ver' as ViewUrl>"
                    );

                query.Where(query.IsApproved == true, query.FollowUpDate.IsNotNull());

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
                //    query.Where(string.Format("<b.EmployeeName LIKE '{0}' OR >", searchEmpName));
                //}
                if (txtEmployeeNumber.Text != string.Empty)
                {
                    string searchEmpNo = "%" + txtEmployeeNumber.Text + "%";
                    query.Where(string.Format("<c.EmployeeName LIKE '{0}'>", searchEmpNo));
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