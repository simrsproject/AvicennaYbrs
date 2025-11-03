using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class SupplierSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.SUPPLIER;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new SupplierQuery("a");
            var asriq = new AppStandardReferenceItemQuery("b");

            query.LeftJoin(asriq).On(query.SRSupplierType == asriq.ItemID &&
                                      asriq.StandardReferenceID == AppEnum.StandardReference.SupplierType.ToString());
            query.Select
                        (
                            query.SupplierID,
                            query.SupplierName,
                            asriq.ItemName.As("SRSupplierType"),
                            query.ContactPerson,
                            query.StreetName,
                            query.City,
                            query.PhoneNo,
                            query.Email,
                            query.IsActive
                        );
            
            if (!string.IsNullOrEmpty(txtSupplierID.Text))
            {
                if (cboFilterSupplierID.SelectedIndex == 1)
                    query.Where(query.SupplierID == txtSupplierID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtSupplierID.Text);
                    query.Where(query.SupplierID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtSupplierName.Text))
            {
                if (cboFilterSupplierName.SelectedIndex == 1)
                    query.Where(query.SupplierName == txtSupplierName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtSupplierName.Text);
                    query.Where(query.SupplierName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtShortName.Text))
            {
                if (cboFilterShortName.SelectedIndex == 1)
                    query.Where(query.ShortName == txtShortName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtShortName.Text);
                    query.Where(query.ShortName.Like(searchTextContain));
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
            if (!string.IsNullOrEmpty(txtCity.Text))
            {
                if (cboFilterCity.SelectedIndex == 1)
                    query.Where(query.City == txtCity.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtCity.Text);
                    query.Where(query.City.Like(searchTextContain));
                }
            }
            query.OrderBy(query.SupplierID.Descending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
