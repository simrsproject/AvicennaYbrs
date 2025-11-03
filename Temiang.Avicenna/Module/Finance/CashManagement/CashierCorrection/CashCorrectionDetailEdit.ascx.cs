using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Finance.CashManagement
{
    public partial class CashCorrectionDetailEdit : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            hfPaymentNo.Value = (String)DataBinder.Eval(DataItem, TransPaymentItemCorrectionMetadata.ColumnNames.PaymentNo);
            hfSequenceNo.Value = (String)DataBinder.Eval(DataItem, TransPaymentItemCorrectionMetadata.ColumnNames.SequenceNo);

            // card provider
            // card type
            StandardReference.InitializeIncludeSpace(cboSRCardProvider, AppEnum.StandardReference.CardProvider);
            StandardReference.InitializeIncludeSpace(cboSRCardType, AppEnum.StandardReference.CardType);

            if (DataItem is GridInsertionObject)
            {
                
                return;
            }

            cboSRCardProvider.SelectedValue = (String)DataBinder.Eval(DataItem, TransPaymentItemCorrectionMetadata.ColumnNames.SRCardProvider);
            cboSRCardType.SelectedValue = (String)DataBinder.Eval(DataItem, TransPaymentItemCorrectionMetadata.ColumnNames.SRCardType);

            // edc
            PopulateEDCMachine();
            cboEDCMachineID.SelectedValue = (String)DataBinder.Eval(DataItem, TransPaymentItemCorrectionMetadata.ColumnNames.EDCMachineID);

        }

        protected void cboSRCardProvider_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateEDCMachine();
        }

        protected void cboEDCMachineID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var entity = Helper.EDCMachineTariff.GetEDCMachineTariff(cboEDCMachineID.SelectedValue, cboSRCardType.SelectedValue);

            var payNo = hfPaymentNo.Value;
            var seqNo = hfSequenceNo.Value;

            var payItem = new TransPaymentItem();
            if(payItem.LoadByPrimaryKey(payNo, seqNo)){
                txtCardFeeAmount.Value = System.Convert.ToDouble(payItem.Amount) * ((entity != null ? (double)entity.EDCMachineTariff : 0) / 100);
            }
        }

        private void PopulateEDCMachine()
        {
            if (cboSRCardProvider.SelectedIndex == -1)
                cboEDCMachineID.Items.Clear();
            else
            {
                cboEDCMachineID.Items.Clear();
                var query = new EDCMachineQuery();
                query.Where(
                        query.SRCardProvider == cboSRCardProvider.SelectedValue,
                        query.IsActive == true
                    );

                var edc = query.LoadDataTable();
                cboEDCMachineID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                for (var i = 0; i < edc.Rows.Count; i++)
                {
                    cboEDCMachineID.Items.Add(new RadComboBoxItem((string)edc.Rows[i][EDCMachineMetadata.ColumnNames.EDCMachineName],
                        (string)edc.Rows[i][EDCMachineMetadata.ColumnNames.EDCMachineID]));
                }
            }
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(cboSRCardProvider.SelectedValue)) {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Card Provider required.";
                return;
            }
            if (string.IsNullOrEmpty(cboSRCardType.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Card Type required.";
                return;
            }
            if (string.IsNullOrEmpty(cboEDCMachineID.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "EDC Machine required.";
                return;
            }
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
        public string SRCardProvider {
            get {
                return cboSRCardProvider.SelectedValue;
            }
        }
        public string SRCardProviderName
        {
            get
            {
                return cboSRCardProvider.Text;
            }
        }
        public string SRCardType
        {
            get
            {
                return cboSRCardType.SelectedValue;
            }
        }
        public string SRCardTypeName
        {
            get
            {
                return cboSRCardType.Text;
            }
        }
        public string EDCMachineID
        {
            get
            {
                return cboEDCMachineID.SelectedValue;
            }
        }
        public string EDCMachineName
        {
            get
            {
                return cboEDCMachineID.Text;
            }
        }
        public decimal CardFeeAmount
        {
            get { return decimal.Parse(this.txtCardFeeAmount.Text); }
        }
        #endregion
    }
}