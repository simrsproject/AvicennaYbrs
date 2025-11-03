using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class ItemMedicalFromToCtl : BaseOptionCtl
    {

        #region ComboBox 
        protected void cboItemProductMedicFrom_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ItemProductMedicItemsRequested((RadComboBox)sender, e.Text);
        }
        protected void cboItemProductMedicFrom_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ItemProductMedicItemDataBound(e);
        }
        protected void cboItemProductMedicTo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ItemProductMedicItemsRequested((RadComboBox)sender, e.Text);
        }
        protected void cboItemProductMedicTo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ItemProductMedicItemDataBound(e);
        }
 
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_ItemMedicFrom", cboItemProductMedicFrom.SelectedValue);
            parameters.AddNew("p_ItemMedicTo", cboItemProductMedicTo.SelectedValue);

            //Retun List
            return parameters;
        }

        public override string ParameterCaption
        {
            get { return lblCaption.Text; }
            set { lblCaption.Text = value; }
        }
        //public override string ReportSubTitle
        //{
        //    get
        //    {
        //        return string.Format("Item Inventory : {0} to {1}", cboItemProductMedicFrom.SelectedValue, cboItemProductMedicTo.SelectedValue);
        //    }
        //}

    }
}