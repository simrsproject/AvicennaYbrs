using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class ItemMedicalCtl : BaseOptionCtl
    {

        #region ComboBox 
        protected void cboItemProductMedic_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ItemProductMedicItemsRequested((RadComboBox)sender, e.Text);
        }
        protected void cboItemProductMedic_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ItemProductMedicItemDataBound(e);
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_ItemProductMedic", cboItemProductMedic.SelectedValue);

            //Retun List
            return parameters;
        }

        public override string ParameterCaption
        {
            get { return lblCaption.Text; }
            set { lblCaption.Text = value; }
        }
        public override string ReportSubTitle
        {
            get
            {
                return string.Format("Item Inventory : {0}", cboItemProductMedic.SelectedValue);
            }
        }

    }
}