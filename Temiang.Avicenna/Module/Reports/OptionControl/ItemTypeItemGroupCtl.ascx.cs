using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class ItemTypeItemGroupCtl : BaseOptionCtl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //StandardReference Initialize
            if (!IsPostBack)
            {
                var coll = new AppStandardReferenceItemCollection();
                coll.Query.Where(coll.Query.StandardReferenceID == AppEnum.StandardReference.ItemType,
                                 coll.Query.ReferenceID == "Product", coll.Query.IsActive == true);
                coll.LoadAll();
                foreach (var item in coll)
                {
                    cboItemType.Items.Add(new RadComboBoxItem(item.ItemName, item.ItemID));
                }
            }
        }

        protected void cboItemType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboItemGroup.Items.Clear();
            cboItemGroup.Text = string.Empty;
        }


        protected void cboItemGroup_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ItemGroupItemsRequested((RadComboBox)sender, e.Text, cboItemType.SelectedValue);
        }
        protected void cboItemGroup_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ItemGroupItemDataBound(e);
        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_ItemType", cboItemType.SelectedValue);
            parameters.AddNew("p_ItemGroupID", cboItemGroup.SelectedValue);

            //Retun List
            return parameters;
        }
    }
}