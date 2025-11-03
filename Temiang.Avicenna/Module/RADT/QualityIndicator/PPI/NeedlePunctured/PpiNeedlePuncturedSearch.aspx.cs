using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class PpiNeedlePuncturedSearch : BasePageDialog
    {
        private string FormType
        {
            get
            {
                return Request.QueryString["type"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = FormType == "" ? AppConstant.Program.PpiNeedlePuncturedSurveillance : AppConstant.Program.PpiNeedlePuncturedSurveillanceVerified;

            if (!IsPostBack)
            {
                cboStatus.Items.Add(new RadComboBoxItem("", ""));
                cboStatus.Items.Add(new RadComboBoxItem("Not Approved Yet", "0"));
                cboStatus.Items.Add(new RadComboBoxItem("Approved", "1"));
                cboStatus.Items.Add(new RadComboBoxItem("Not Verified Yet", "2"));
                cboStatus.Items.Add(new RadComboBoxItem("Verified", "3"));
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new PpiNeedlePuncturedQuery("a");
            var usr = new AppUserQuery("b");
            query.LeftJoin(usr).On(query.VerifiedByUserID == usr.UserID);

            if (FormType == "")
                query.Where(query.CreatedByUserID == AppSession.UserLogin.UserID);
            else
                query.Where(query.IsApproved == true);

            query.OrderBy
                (
                    query.TransactionNo.Descending
                );

            query.Select(
                query.TransactionNo,
                query.TransactionDate,
                query.OfficerName,
                query.DatePunctured,
                query.PuncturedAreas,
                query.CausePunctured,
                query.FollowUp,
                query.FollowUpBy,
                query.IsApproved,
                query.IsVoid,
                query.IsVerified,
                query.VerifiedDateTime,
                usr.UserName.As("VerifiedBy")
                );

            if (!string.IsNullOrEmpty(txtTransactionNo.Text))
            {
                if (cboFilterTransactionNo.SelectedIndex == 1)
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                else
                    query.Where(query.TransactionNo.Like(string.Format("%{0}%", txtTransactionNo.Text)));
            }
            if (!txtFromDate.IsEmpty)
                query.Where(query.TransactionDate >= txtFromDate.SelectedDate);
            if (!txtToDate.IsEmpty)
                query.Where(query.TransactionDate <= txtToDate.SelectedDate);
            if (!string.IsNullOrEmpty(txtOfficerName.Text))
            {
                if (cboFilterOfficerName.SelectedIndex == 1)
                    query.Where(query.OfficerName == txtOfficerName.Text);
                else
                    query.Where(query.OfficerName.Like(string.Format("%{0}%", txtOfficerName.Text)));
            }
            if (!txtDatePuncturedFrom.IsEmpty)
                query.Where(query.DatePunctured >= txtDatePuncturedFrom.SelectedDate);
            if (!txtDatePuncturedTo.IsEmpty)
                query.Where(query.DatePunctured <= txtDatePuncturedTo.SelectedDate);
            if (!string.IsNullOrEmpty(txtPuncturedAreas.Text))
            {
                if (cboFilterPuncturedAreas.SelectedIndex == 1)
                    query.Where(query.PuncturedAreas == txtPuncturedAreas.Text);
                else
                    query.Where(query.PuncturedAreas.Like(string.Format("%{0}%", txtPuncturedAreas.Text)));
            }
            if (!string.IsNullOrEmpty(txtCausePunctured.Text))
            {
                if (cboFilterCausePunctured.SelectedIndex == 1)
                    query.Where(query.CausePunctured == txtCausePunctured.Text);
                else
                    query.Where(query.CausePunctured.Like(string.Format("%{0}%", txtCausePunctured.Text)));
            }
            if (!string.IsNullOrEmpty(cboStatus.SelectedValue))
            {
                switch (cboStatus.SelectedValue)
                {
                    case "0":
                        query.Where(query.Or(query.IsApproved.IsNull(), query.IsApproved == false));
                        break;
                    case "1":
                        query.Where(query.IsApproved == true);
                        break;
                    case "2":
                        query.Where(query.Or(query.IsVerified.IsNull(), query.IsVerified == false));
                        break;
                    case "3":
                        query.Where(query.IsVerified == true);
                        break;
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
