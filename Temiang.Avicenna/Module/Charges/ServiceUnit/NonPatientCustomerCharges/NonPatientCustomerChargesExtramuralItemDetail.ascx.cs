using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class NonPatientCustomerChargesExtramuralItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                txtQty.Value = 1;
                return;
            }
            ViewState["IsNewRecord"] = false;
            var exID = (String)DataBinder.Eval(DataItem, TransChargesExtramuralItemsMetadata.ColumnNames.SRExtramuralItem);
            var arghhh = new RadComboBoxItemsRequestedEventArgs();
            arghhh.Text = exID;
            cboExtramuralItems_ItemsRequested(cboExtramuralItems, arghhh);
            cboExtramuralItems.SelectedValue = exID;
            cboExtramuralItems_SelectedIndexChanged(cboExtramuralItems, new RadComboBoxSelectedIndexChangedEventArgs(cboExtramuralItems.Text, "", cboExtramuralItems.SelectedValue, ""));

            var xxx = DataBinder.Eval(DataItem, TransChargesExtramuralItemsMetadata.ColumnNames.LeasingPeriodInDays);
            if (xxx != null)
            {
                txtLeasingPeriod.Value = System.Convert.ToInt32(xxx);
            }
            txtQty.Value = System.Convert.ToDouble(DataBinder.Eval(DataItem, TransChargesExtramuralItemsMetadata.ColumnNames.Qty));
            txtGuaranty.Value = System.Convert.ToDouble(DataBinder.Eval(DataItem, TransChargesExtramuralItemsMetadata.ColumnNames.GuarantyAmount));
        }

        protected void cboExtramuralItems_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.StandardReferenceItemsRequested(cboExtramuralItems, "ExtramuralItem", e.Text);
        }
        protected void cboExtramuralItems_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.StandardReferenceItemDataBound(e);
        }
        protected void cboExtramuralItems_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var stdi = new AppStandardReferenceItem();
            if (stdi.LoadByPrimaryKey("ExtramuralItem", cboExtramuralItems.SelectedValue))
            {
                txtGuaranty.Value = System.Convert.ToDouble(stdi.NumericValue);
            }
            else
            {
                txtGuaranty.Value = 0;
            }
        }

        #region Properties for return entry value
        public String SRExtramuralItem
        {
            get { return cboExtramuralItems.SelectedValue; }
        }

        public String SRExtramuralItemName
        {
            get { return cboExtramuralItems.Text; }
        }

        public int LeasingPeriodInDays
        {
            get { return System.Convert.ToInt32(txtLeasingPeriod.Value ?? 0); }
        }

        public decimal Qty
        {
            get { return System.Convert.ToDecimal(txtQty.Value ?? 0); }
        }

        public decimal Guaranty
        {
            get { return System.Convert.ToDecimal(txtGuaranty.Value ?? 0); }
        }
        #endregion

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                TransChargesExtramuralItemsCollection coll = (TransChargesExtramuralItemsCollection)Session["collTransChargesExtramural" + Request.UserHostName];

                string sr = cboExtramuralItems.SelectedValue;
                if (coll.Where(x => x.SRExtramuralItem == sr).Any())
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Extramural item: {0} has exist", sr);
                }
            }
        }

        
    }
}
