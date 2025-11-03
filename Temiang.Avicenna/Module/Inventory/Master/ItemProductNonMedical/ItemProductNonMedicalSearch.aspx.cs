using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ItemProductNonMedicalSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.ItemProductNonMedical;

            if (!IsPostBack)
            {
                var group = new ItemGroupCollection();
                group.Query.Where(group.Query.IsActive == true, group.Query.SRItemType == ItemType.NonMedical);
                group.Query.OrderBy(group.Query.ItemGroupID.Ascending);
                group.LoadAll();

                cboItemGroupID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ItemGroup entity in group)
                {
                    cboItemGroupID.Items.Add(new RadComboBoxItem(entity.ItemGroupName, entity.ItemGroupID));
                }

                pnlNewUpload.Visible = AppSession.Application.IsMenuMasterItemProductExportAble(ProgramID);
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new ItemQuery("a");
            var qs = new ItemProductNonMedicQuery("b");
            var group = new ItemGroupQuery("c");
            var pa = new ProductAccountQuery("d");

            query.Where(query.SRItemType == BusinessObject.Reference.ItemType.NonMedical);
            query.LeftJoin(qs).On(query.ItemID == qs.ItemID);
            query.LeftJoin(group).On(query.ItemGroupID == group.ItemGroupID);
            query.LeftJoin(pa).On(pa.ProductAccountID == query.ProductAccountID);
            query.Select
                (
                    query.ItemID,
                    group.ItemGroupName,
                    query.ItemName,
                    query.IsActive,
                    query.Notes,
                    qs.SRItemUnit,
                    qs.SRPurchaseUnit,
                    qs.ConversionFactor,
                    pa.ProductAccountName,
                    qs.IsInventoryItem,
                    qs.IsControlExpired
                );

            bool isEsTop = true;

            if (!string.IsNullOrEmpty(cboItemGroupID.SelectedValue))
            {
                query.Where(query.ItemGroupID == cboItemGroupID.SelectedValue);
            }

            if (!string.IsNullOrEmpty(txtItemID.Text))
            {
                if (cboFilterItemID.SelectedIndex == 1)
                    query.Where(query.ItemID == txtItemID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtItemID.Text);
                    query.Where(query.ItemID.Like(searchTextContain));
                }
                isEsTop = false;
            }
            if (!string.IsNullOrEmpty(txtItemName.Text))
            {
                if (cboFilterItemName.SelectedIndex == 1)
                    query.Where(query.ItemName == txtItemName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtItemName.Text);
                    query.Where(query.ItemName.Like(searchTextContain));
                }
                isEsTop = false;
            }
            if (pnlNewUpload.Visible)
            {
                query.Where(query.IsActive == chkIsActive.Checked);
                if (chkIsNewUpload.Checked)
                    query.Where(query.IsNewUpload == true);
                else
                    query.Where(query.Or(query.IsNewUpload.IsNull(), query.IsNewUpload == false));
            }

            if (isEsTop)
                query.es.Top = AppSession.Parameter.MaxResultRecord;
            
            query.OrderBy(query.ItemID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}