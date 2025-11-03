using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class PaymentReceiveDirectItem : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            PaymentMethodCollection pmColl = new PaymentMethodCollection();
            pmColl.Query.Where(pmColl.Query.SRPaymentTypeID == AppSession.Parameter.PaymentTypePayment);
            pmColl.LoadAll();

            cboSRPaymentMethod.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            foreach (PaymentMethod pm in pmColl)
            {
                cboSRPaymentMethod.Items.Add(new RadComboBoxItem(pm.PaymentMethodName, pm.SRPaymentMethodID));
            }

            StandardReference.InitializeIncludeSpace(cboSRCardProvider, AppEnum.StandardReference.CardProvider);
            StandardReference.InitializeIncludeSpace(cboSRCardType, AppEnum.StandardReference.CardType);

            cboBank.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var bankActive = new BankCollection();
                bankActive.Query.Where(bankActive.Query.IsActive == true, bankActive.Query.IsCashierFrontOffice == true);
                bankActive.LoadAll();

                foreach (Bank collection in bankActive)
                {
                    cboBank.Items.Add(new RadComboBoxItem(collection.BankName, collection.BankID));
                }

                var coll = (TransPaymentItemCollection)Session["PaymentReceiveDirect:collTransPaymentItem" + Request.UserHostName];
                if (!coll.HasData)
                    hdnSequenceNo.Value = "001";
                else
                {
                    int seqNo = int.Parse(coll[coll.Count - 1].SequenceNo) + 1;
                    hdnSequenceNo.Value = string.Format("{0:000}", seqNo);
                }

                txtAmount.Value = 0D;
                PrevoiusAmount = 0;
                return;
            }

            var bank = new BankCollection();
            bank.LoadAll();

            foreach (Bank collection in bank)
            {
                cboBank.Items.Add(new RadComboBoxItem(collection.BankName, collection.BankID));
            }

            ViewState["IsNewRecord"] = false;

            ResetValue(true);

            hdnSequenceNo.Value = (String)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.SequenceNo);
            cboSRPaymentMethod.SelectedValue = (String)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.SRPaymentMethod);

            cboSRCardProvider.SelectedValue = (String)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.SRCardProvider);
            PopulateEDCMachine();

            cboSRCardType.SelectedValue = (String)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.SRCardType);
            cboEDCMachineID.SelectedValue = (String)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.EDCMachineID);
            txtCardNo.Text = (String)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.CardNo);
            txtCardHolderName.Text = (String)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.CardHolderName);
            txtCardFeeAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.CardFeeAmount));
            cboBank.SelectedValue = (String)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.BankID);

            txtAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.Amount));
            txtRoundingAmount.Value = Convert.ToDouble((decimal)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.RoundingAmount));
            txtPaymentAmount.Value = Convert.ToDouble((decimal)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.AmountReceived));
            txtReturnAmount.Value = txtPaymentAmount.Value > 0 ? txtPaymentAmount.Value - txtAmount.Value : 0;

            PrevoiusAmount = Convert.ToDecimal(DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.Amount));

            SetVisiblePanel();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var coll = (TransPaymentItemCollection)Session["PaymentReceiveDirect:collTransPaymentItem" + Request.UserHostName];

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                string id = hdnSequenceNo.Value;
                bool isExist = false;
                bool isDouble = false;
                foreach (TransPaymentItem item in coll)
                {
                    if (item.SequenceNo.Equals(id))
                    {
                        isExist = true;
                        break;
                    }

                    if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodCash)
                    {
                        if (item.SRPaymentMethod == cboSRPaymentMethod.SelectedValue)
                        {
                            isDouble = true;
                            break;
                        }
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", id);
                }
                if (isDouble)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Payment with Cash has exist");
                    return;
                }
            }

            if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodCreditCard || cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodDebitCard)
            {
                if (string.IsNullOrEmpty(cboEDCMachineID.SelectedValue))//(cboEDCMachineID.SelectedIndex == -1)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "EDC Machine is required";
                    return;
                }

                if (string.IsNullOrEmpty(cboSRCardType.SelectedValue))  //(cboSRCardType.SelectedIndex == -1)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "Card Type is required";
                    return;
                }

                if (string.IsNullOrEmpty(cboSRCardProvider.SelectedValue)) //(cboSRCardProvider.SelectedIndex == -1)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "Card Provider is required";
                    return;
                }
            }
            else if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodTransfer)
            {
                if (string.IsNullOrEmpty(cboBank.SelectedValue))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "Bank is required";
                    return;
                }
            }

            if (txtAmount.Value == 0 || txtAmount.Text.Length == 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Amount is required";
                return;
            }

            if (txtAmount.Value > ((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtOrderAmount")).Value)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Amount value can't be greater than transaction amount. ";
                return;
            }

            if (txtPaymentAmount.Value > 0 && txtAmount.Value > txtPaymentAmount.Value)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Amount received can't be less than amount value. ";
            }
        }

        #region Properties for return entry value

        public String SequenceNo
        {
            get { return hdnSequenceNo.Value; }
        }

        public String SRPaymentType
        {
            get { return AppSession.Parameter.PaymentTypePayment; }
        }

        public String PaymentTypeName
        {
            get
            {
                AppStandardReferenceItem std = new AppStandardReferenceItem();
                std.LoadByPrimaryKey("PaymentType", AppSession.Parameter.PaymentTypePayment);
                return std.ItemName;
            }
        }

        public String SRPaymentMethod
        {
            get { return cboSRPaymentMethod.SelectedValue; }
        }

        public String PaymentMethodName
        {
            get { return cboSRPaymentMethod.Text; }
        }

        public String SRCardProvider
        {
            get { return cboSRCardProvider.SelectedValue; }
        }

        public String SRCardType
        {
            get { return cboSRCardType.SelectedValue; }
        }

        public String EDCMachineID
        {
            get { return cboEDCMachineID.SelectedValue; }
        }

        public String CardNo
        {
            get { return txtCardNo.Text; }
        }

        public String CardHolderName
        {
            get { return txtCardHolderName.Text; }
        }

        public Decimal CardFeeAmount
        {
            get { return Convert.ToDecimal(txtCardFeeAmount.Value); }
        }

        public String BankID
        {
            get { return cboBank.SelectedValue; }
        }

        public Decimal Amount
        {
            get { return Convert.ToDecimal(txtAmount.Value); }
        }

        public Decimal RoundingAmount
        {
            get { return Convert.ToDecimal(txtRoundingAmount.Value); }
        }

        public Decimal PrevoiusAmount
        {
            get { return (Decimal)ViewState["PrevoiusAmount"]; }
            set { ViewState["PrevoiusAmount"] = value; }
        }

        public Decimal AmountReceived
        {
            get { return Convert.ToDecimal(txtPaymentAmount.Value); }
        }

        public Decimal Change
        {
            get { return Convert.ToDecimal(txtPaymentAmount.Value) > 0 ? Convert.ToDecimal(txtPaymentAmount.Value) - Convert.ToDecimal(txtAmount.Value) : 0; }
        }

        #endregion

        protected void cboSRCardProvider_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateEDCMachine();
        }

        protected void cboSRPaymentMethod_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetVisiblePanel();
            ResetValue(false);

            if (string.IsNullOrEmpty(e.Value))
            {
                txtAmount.Value = 0;
                txtRoundingAmount.Value = 0;
            }
        }

        protected void cboEDCMachineID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            EDCMachineTariff entity = Helper.EDCMachineTariff.GetEDCMachineTariff(cboEDCMachineID.SelectedValue, cboSRCardType.SelectedValue);
            txtCardFeeAmount.Value = (double)txtAmount.Value * ((entity != null ? (double)entity.EDCMachineTariff : 0) / 100);
        }

        private void SetVisiblePanel()
        {
            if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodCash)
            {
                pnlPayment.Visible = true;
                pnlBank.Visible = false;
                pnlCardProvider.Visible = false;
                pnlCardDetail.Visible = false;
                pnlCardFeeAmount.Visible = false;
            }
            else if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodCreditCard)
            {
                pnlPayment.Visible = false;
                pnlBank.Visible = false;
                pnlCardDetail.Visible = true;
                pnlCardProvider.Visible = true;
                pnlCardFeeAmount.Visible = true;
            }
            else if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodDebitCard)
            {
                pnlPayment.Visible = false;
                pnlBank.Visible = false;
                pnlCardDetail.Visible = true;
                pnlCardProvider.Visible = true;
                pnlCardFeeAmount.Visible = true;
            }
            else if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodTransfer)
            {
                pnlPayment.Visible = false;
                pnlBank.Visible = true;
                pnlCardProvider.Visible = false;
                pnlCardDetail.Visible = false;
                pnlCardFeeAmount.Visible = false;
            }
            else
            {
                pnlPayment.Visible = false;
                pnlBank.Visible = true;
                pnlCardProvider.Visible = true;
                pnlCardDetail.Visible = true;
                pnlCardFeeAmount.Visible = true;
            }
        }

        private void PopulateEDCMachine()
        {
            if (cboSRCardProvider.SelectedIndex == -1)
                cboEDCMachineID.Items.Clear();
            else
            {
                cboEDCMachineID.Items.Clear();
                EDCMachineQuery query = new EDCMachineQuery();
                query.Where
                    (
                        query.SRCardProvider == cboSRCardProvider.SelectedValue,
                        query.IsActive == true
                    );

                DataTable edc = query.LoadDataTable();
                cboEDCMachineID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                for (int i = 0; i < edc.Rows.Count; i++)
                {
                    cboEDCMachineID.Items.Add(new RadComboBoxItem((string)edc.Rows[i][EDCMachineMetadata.ColumnNames.EDCMachineName],
                        (string)edc.Rows[i][EDCMachineMetadata.ColumnNames.EDCMachineID]));
                }
            }
        }

        private void ResetValue(bool includePaymentMethod)
        {
            if (includePaymentMethod)
                cboSRPaymentMethod.SelectedValue = string.Empty;

            cboBank.SelectedValue = string.Empty;

            decimal? totAmount = 0;

            var coll = (TransPaymentItemCollection)Session["PaymentReceiveDirect:collTransPaymentItem" + Request.UserHostName];
            if (coll.HasData)
            {
                totAmount = ViewState["IsNewRecord"].Equals(true) ? coll.Aggregate(totAmount, (current, item) => current + item.Amount) : coll.Where(item => item.SequenceNo != hdnSequenceNo.Value).Aggregate(totAmount, (current, item) => current + item.Amount);
            }

            txtAmount.Value = ((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtOrderAmount")).Value - (double)totAmount;

            //var amount = Convert.ToDecimal(txtAmount.Value ?? 0);
            //txtAmount.Value = Convert.ToDouble(Helper.Rounding(amount, AppEnum.RoundingType.Payment));
            //txtRoundingAmount.Value = txtAmount.Value - Convert.ToDouble(amount);

            //if (!includePaymentMethod)
            //{
            if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodCash)
            {
                var amount = Convert.ToDecimal(txtAmount.Value ?? 0);
                txtAmount.Value = Convert.ToDouble(Helper.Rounding(amount, AppEnum.RoundingType.Payment));
                txtRoundingAmount.Value = txtAmount.Value - Convert.ToDouble(amount);
            }
            else if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodDebitCard || cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodCreditCard || cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodTransfer)
            {
                var amount = Convert.ToDecimal(txtAmount.Value ?? 0);
                txtAmount.Value = Convert.ToDouble(Helper.Rounding(amount, AppEnum.RoundingType.PaymentWithCard));
                txtRoundingAmount.Value = txtAmount.Value - Convert.ToDouble(amount);
            }
            //}
            
            txtPaymentAmount.Value = 0;
            txtReturnAmount.Value = 0;
            cboSRCardProvider.SelectedValue = string.Empty;
            cboSRCardType.SelectedValue = string.Empty;
            cboEDCMachineID.SelectedValue = string.Empty;
            txtCardNo.Text = string.Empty;
            txtCardHolderName.Text = string.Empty;
            txtCardFeeAmount.Value = 0D;
        }

        protected void txtPaymentAmount_TextChanged(object sender, EventArgs e)
        {
            txtReturnAmount.Value = txtPaymentAmount.Value > 0 ? txtPaymentAmount.Value - txtAmount.Value : 0;
        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            txtRoundingAmount.Value = 0;
        }
    }
}