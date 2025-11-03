using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ProductAccountSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PRODUCTACCOUNT; //TODO: Isi ProgramID
            ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
        }

        public override bool OnButtonOkClicked()
        {
            var query = new ProductAccountQuery("a");
            var itype = new AppStandardReferenceItemQuery("b");
            query.InnerJoin(itype).On(query.SRItemType == itype.ItemID &
                                      itype.StandardReferenceID == AppEnum.StandardReference.ItemType);

            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(
                query.ProductAccountID,
                query.ProductAccountName,
                itype.ItemName.As("ItemTypeName"),
                query.IsActive
                );
            if (!string.IsNullOrEmpty(txtProductAccountID.Text))
            {
                if (cboFilterProductAccountID.SelectedIndex == 1)
                    query.Where(query.ProductAccountID == txtProductAccountID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtProductAccountID.Text);
                    query.Where(query.ProductAccountID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtProductAccountName.Text))
            {
                if (cboFilterProductAccountName.SelectedIndex == 1)
                    query.Where(query.ProductAccountName == txtProductAccountName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtProductAccountName.Text);
                    query.Where(query.ProductAccountName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboSRItemType.SelectedValue))
            {
                query.Where(query.SRItemType == cboSRItemType.SelectedValue);
            }
            query.Where(query.IsActive == chkIsActive.Checked);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
