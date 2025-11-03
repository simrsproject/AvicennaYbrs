using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class GuarantorSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.GUARANTOR;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new GuarantorQuery("a");
            var asriq = new AppStandardReferenceItemQuery("b");
            var coaq = new ChartOfAccountsQuery("c");
            
            query.LeftJoin(asriq).On(query.SRTariffType == asriq.ItemID && asriq.StandardReferenceID == AppEnum.StandardReference.TariffType);
            query.LeftJoin(coaq).On(query.ChartOfAccountId == coaq.ChartOfAccountId);

            query.Select
                (
                    query.GuarantorID,
                    query.GuarantorName,
                    query.ContractStart,
                    query.ContractEnd,
                    query.ContactPerson,
                    asriq.ItemName.As("SRTariffType"),
                    coaq.ChartOfAccountCode,
                    query.PhoneNo,
                    query.IsActive,
                    query.ContractNumber
                );

            if (!string.IsNullOrEmpty(txtGuarantorID.Text))
            {
                if (cboFilterGuarantorID.SelectedIndex == 1)
                    query.Where(query.GuarantorID == txtGuarantorID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtGuarantorID.Text);
                    query.Where(query.GuarantorID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtGuarantorName.Text))
            {
                if (cboFilterGuarantorName.SelectedIndex == 1)
                    query.Where(query.GuarantorName == txtGuarantorName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtGuarantorName.Text);
                    query.Where(query.GuarantorName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtContactPerson.Text))
            {
                if (cboFilterContactPerson.SelectedIndex == 1)
                    query.Where(query.ContactPerson == txtContactPerson.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtContactPerson.Text);
                    query.Where(query.ContactPerson.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtStreetName.Text))
            {
                if (cboFilterStreetName.SelectedIndex == 1)
                    query.Where(query.StreetName == txtStreetName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtStreetName.Text);
                    query.Where(query.StreetName.Like(searchTextContain));
                }
            }
            if (!txtPhoneNo.Text.Trim().Equals(string.Empty))
            {
                if (cboFilterPhoneNo.SelectedIndex == 1)
                    query.Where(query.PhoneNo == txtPhoneNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtPhoneNo.Text);
                    query.Where(query.PhoneNo.Like(searchTextContain));
                }
            }
            query.OrderBy(query.GuarantorID.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
