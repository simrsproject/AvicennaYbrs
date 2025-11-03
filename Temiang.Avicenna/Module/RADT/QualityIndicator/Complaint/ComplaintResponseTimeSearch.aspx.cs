using System;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator.Complaint
{
    public partial class ComplaintResponseTimeSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.ComplaintResponseTime;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
            }
        }

        public override bool OnButtonOkClicked()
        {
            OnButtonOkClick();

            return true;
        }

        private void OnButtonOkClick()
        {
            var query = new ComplaintResponseTimeQuery("a");
            var qpat = new PatientQuery("b");

            query.LeftJoin(qpat).On(qpat.PatientID == query.PatientID);

            query.OrderBy
                (
                    query.ComplaintDate.Descending, query.ComplaintNo.Descending
                );

            query.Select(
                query.ComplaintNo,
                query.ComplaintDate,
                query.CustomerName,
                query.PatientID,
                qpat.MedicalNo,
                qpat.PatientName,
                query.CustomerAddress,
                query.PhoneNo,
                query.IsApproved,
                query.IsVoid
                );

            if (!string.IsNullOrEmpty(txtComplaintNo.Text))
            {
                if (cboFilterComplaintNo.SelectedIndex == 1)
                    query.Where(query.ComplaintNo == txtComplaintNo.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtComplaintNo.Text);
                    query.Where(query.ComplaintNo.Like(searchText));
                }
            }
            if (!txtComplaintDate.IsEmpty)
            {
                query.Where(query.ComplaintDate == txtComplaintDate.SelectedDate);
            }
            if (!string.IsNullOrEmpty(txtCustomerName.Text))
            {
                if (cboFilterCustomerName.SelectedIndex == 1)
                    query.Where(query.CustomerName == txtCustomerName.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtCustomerName.Text);
                    query.Where(query.CustomerName.Like(searchText));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset
        }
    }
}