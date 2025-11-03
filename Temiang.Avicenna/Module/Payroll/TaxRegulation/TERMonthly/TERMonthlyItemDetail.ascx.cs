using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.TaxRegulation
{
    public partial class TERMonthlyItemDetail : BaseUserControl
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

                txtTERMonthlyItemID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtTERMonthlyItemID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TERMonthlyItemMetadata.ColumnNames.TERMonthlyItemID));
            txtLowerLimit.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TERMonthlyItemMetadata.ColumnNames.LowerLimit));
            txtUpperLimit.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TERMonthlyItemMetadata.ColumnNames.UpperLimit));
            txtTaxRate.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TERMonthlyItemMetadata.ColumnNames.TaxRate));
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                TERMonthlyItemCollection coll = (TERMonthlyItemCollection)Session["collTERMonthlyItem" + Request.UserHostName];

                //TODO: Betulkan cara pengecekannya
                decimal id = Convert.ToDecimal(txtLowerLimit.Value);
                bool isExist = false;
                foreach (TERMonthlyItem item in coll)
                {
                    if (item.LowerLimit.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Lower Limit: {0} has exist", String.Format("{0:N2}", id));
                }
            }
        }

        #region Properties for return entry value
        public Int32 TERMonthlyItemID
        {
            get { return Convert.ToInt32(txtTERMonthlyItemID.Text); }
        }
        public decimal LowerLimit
        {
            get { return Convert.ToDecimal(txtLowerLimit.Value); }
        }
        public Decimal UpperLimit
        {
            get { return Convert.ToDecimal(txtUpperLimit.Value); }
        }
        public Decimal TaxRate
        {
            get { return Convert.ToDecimal(txtTaxRate.Value); }
        }
        #endregion
    }
}