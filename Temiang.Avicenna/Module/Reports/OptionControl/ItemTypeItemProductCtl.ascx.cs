using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class ItemTypeItemProductCtl : BaseOptionCtl
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
            cboItemProduct.Items.Clear();
            cboItemProduct.Text = string.Empty;
        }

        
        protected void cboItemProduct_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ItemItemsRequested((RadComboBox)sender, e.Text, cboItemType.SelectedValue);
        }
        protected void cboItemProduct_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ItemItemDataBound(e);
        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_ItemType", cboItemType.SelectedValue);
            parameters.AddNew("p_ItemID", cboItemProduct.SelectedValue);

            //Retun List
            return parameters;
        }
    }
}