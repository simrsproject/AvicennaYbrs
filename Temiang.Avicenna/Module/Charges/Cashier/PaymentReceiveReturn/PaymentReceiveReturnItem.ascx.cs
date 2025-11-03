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
    public partial class PaymentReceiveReturnItem : BaseUserControl
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

            BankCollection bank = new BankCollection();
            bank.LoadAll();

            foreach (Bank collection in bank)
            {
                cboBank.Items.Add(new RadComboBoxItem(collection.BankName, collection.BankID));
            }

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                TransPaymentItemCollection coll = (TransPaymentItemCollection)Session["PaymentReceiveReturnItems" + Request.UserHostName];
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
            PrevoiusAmount = Convert.ToDecimal(DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.Amount));

            SetVisiblePanel();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            TransPaymentItemCollection coll = (TransPaymentItemCollection)Session["PaymentReceiveReturnItems" + Request.UserHostName];

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                string id = hdnSequenceNo.Value;
                bool isExist = false;
                foreach (TransPaymentItem item in coll)
                {
                    if (item.SequenceNo.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", id);
                }
            }

            if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodCreditCard || cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodDebitCard)
            {
                if (cboSRCardProvider.SelectedIndex == -1)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "Card Provider is required";
                }

                if (cboSRCardType.SelectedIndex == -1)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "Card Type is required";
                }

                if (cboEDCMachineID.SelectedIndex == -1)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "EDC Machine is required";
                }
            }

            if (txtAmount.Value == 0 || txtAmount.Text.Length == 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Amount is required";
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
                var std = new PaymentType();
                std.LoadByPrimaryKey(AppSession.Parameter.PaymentTypePayment);
                return std.PaymentTypeName;
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

        public Decimal PrevoiusAmount
        {
            get { return (Decimal)ViewState["PrevoiusAmount"]; }
            set { ViewState["PrevoiusAmount"] = value; }
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
                txtAmount.Value = 0;
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
                pnlBank.Visible = false;
                pnlCardProvider.Visible = false;
                pnlCardDetail.Visible = false;
                pnlCardFeeAmount.Visible = false;
            }
            else if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodCreditCard)
            {
                pnlBank.Visible = false;
                pnlCardDetail.Visible = true;
                pnlCardProvider.Visible = true;
                pnlCardFeeAmount.Visible = true;
            }
            else if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodDebitCard)
            {
                pnlBank.Visible = false;
                pnlCardDetail.Visible = true;
                pnlCardProvider.Visible = true;
                pnlCardFeeAmount.Visible = true;
            }
            else if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodTransfer)
            {
                pnlBank.Visible = true;
                pnlCardProvider.Visible = false;
                pnlCardDetail.Visible = false;
                pnlCardFeeAmount.Visible = false;
            }
            else
            {
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
            txtAmount.Value = ((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtOrderAmount")).Value;
            cboSRCardProvider.SelectedValue = string.Empty;
            cboSRCardType.SelectedValue = string.Empty;
            cboEDCMachineID.SelectedValue = string.Empty;
            txtCardNo.Text = string.Empty;
            txtCardHolderName.Text = string.Empty;
            txtCardFeeAmount.Value = 0D;
        }
    }
}