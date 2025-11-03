using System;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class ItemBinLocationCtl : BaseOptionCtl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void cboLocationID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.LocationItemsRequested((RadComboBox)sender, e.Text);
        }

        protected void cboLocationID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.LocationItemDataBound(e);
        }

        protected void cboLocationID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ComboBox.PopulateWithItemBinByLocation(cboItemBin, e.Value);
        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_LocationID", cboLocationID.SelectedValue);
            parameters.AddNew("p_ItemBin", cboItemBin.SelectedValue);

            //Retun List
            return parameters;
        }
    }
}