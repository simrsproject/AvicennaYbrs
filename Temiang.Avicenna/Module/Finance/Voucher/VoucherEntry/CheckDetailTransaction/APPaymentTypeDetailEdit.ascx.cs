using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.CheckDetailTransaction
{
    public partial class APPaymentTypeDetailEdit : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            //PopUpSearch.InitializeOnButtonClick(AppEnum.PopUpSearch.Accounts, txtDocumentNumber);

            if (DataItem is GridInsertionObject)
            {
                return;
            }
            txtPPh22Amount.Value = System.Convert.ToDouble(DataBinder.Eval(DataItem, "PPh22Amount"));
            txtPPh23Amount.Value = System.Convert.ToDouble(DataBinder.Eval(DataItem, "PPh23Amount"));
            txtStampAmount.Value = System.Convert.ToDouble(DataBinder.Eval(DataItem, "StampAmount"));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {

        }

        #region Method & Event TextChanged
        protected void Page_Init(object sender, EventArgs e)
        {   
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        #endregion

        #region Properties for return entry value
        
        public decimal PPh22Amount
        {
            get { return System.Convert.ToDecimal(txtPPh22Amount.Value.Value); }
        }
        public decimal PPh23Amount
        {
            get { return System.Convert.ToDecimal(txtPPh23Amount.Value.Value); }
        }
        public decimal StampAmount
        {
            get { return System.Convert.ToDecimal(txtStampAmount.Value.Value); }
        }
        #endregion
    }
}