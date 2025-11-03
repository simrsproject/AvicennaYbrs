using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class SalaryScaleFactorDetail : BaseUserControl
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
                
                txtSalaryScaleFactorID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtValidFrom.Enabled = false;
            txtSalaryScaleFactorID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, SalaryScaleFactorMetadata.ColumnNames.SalaryScaleFactorID));
            txtValidFrom.SelectedDate = Convert.ToDateTime(DataBinder.Eval(DataItem, SalaryScaleFactorMetadata.ColumnNames.ValidFrom));
            txtAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, SalaryScaleFactorMetadata.ColumnNames.Amount));
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (SalaryScaleFactorCollection)Session["collSalaryScaleFactor"];

                //TODO: Betulkan cara pengecekannya
                DateTime id = txtValidFrom.SelectedDate ?? DateTime.Now;
                bool isExist = false;
                foreach (SalaryScaleFactor item in coll)
                {
                    if (item.ValidFrom.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Valid From: {0} has exist", id);
                }
            }
        }

        #region Properties for return entry value
        public Int32 SalaryScaleFactorID
        {
            get { return Convert.ToInt32(txtSalaryScaleFactorID.Text); }
        }
        public DateTime ValidFrom
        {
            get { return Convert.ToDateTime(txtValidFrom.SelectedDate); }
        }
        public Decimal Amount
        {
            get { return Convert.ToDecimal(txtAmount.Value); }
        }
        #endregion
    }
}