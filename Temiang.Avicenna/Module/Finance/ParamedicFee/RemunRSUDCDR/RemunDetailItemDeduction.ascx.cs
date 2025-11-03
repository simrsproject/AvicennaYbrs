using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee
{
    public partial class RemunDetailItemDeduction : BaseUserControl
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
                return;
            }

            this.SRRemunDeduction = (string)DataBinder.Eval(DataItem, "SRRemunDeduction");
            this.DeductionName = (string)DataBinder.Eval(DataItem, "DeductionName");
            this.Amount = System.Convert.ToDecimal(DataBinder.Eval(DataItem, "Amount"));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {

        }

        #region Method & Event TextChanged

        protected void Page_Init(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region Properties for return entry value
        public string SRRemunDeduction {
            get {
                return hfID.Value;
            }
            set {
                hfID.Value = value;
            }
        }
        public string DeductionName {
            set {
                txtDeductionName.Text = value;
            }
        }
        public decimal Amount
        {
            get
            {
                return System.Convert.ToDecimal(txtAmount.Value ?? 0);
            }
            set
            {
                txtAmount.Value = System.Convert.ToDouble(value);
            }
        }
        #endregion


    }
}