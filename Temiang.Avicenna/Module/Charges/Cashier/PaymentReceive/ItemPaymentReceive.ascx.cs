using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Charges.Cashier
{
    public partial class ItemPaymentReceive : BaseUserControl
    {
        public object DataItem { get; set; }

        private string GetRegNo() {
            //return ((RadTextBox)Helper.FindControlRecursive(Page, "txtRegistrationNo")).Text;
            return Request.QueryString["regno"];
        }

        protected override void OnDataBinding(EventArgs e)
        {
            cboSRPaymentType.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            var reg = new Registration();
            reg.LoadByPrimaryKey(GetRegNo());

            var ptColl = new PaymentTypeCollection();
            if (reg.IsDirectPrescriptionReturn == true)
            {
                cboSRPaymentType.Items.Add(new RadComboBoxItem(AppSession.Parameter.PaymentTypePaymentName, AppSession.Parameter.PaymentTypePayment));
            }
            else
            {
                ptColl.Query.Where(ptColl.Query.SRPaymentTypeID != AppSession.Parameter.PaymentTypeDownPayment && ptColl.Query.IsCashierFrontOffice == true);
                ptColl.LoadAll();

                foreach (var pt in ptColl)
                {
                    cboSRPaymentType.Items.Add(new RadComboBoxItem(pt.PaymentTypeName, pt.SRPaymentTypeID));
                }
            }
            StandardReference.InitializeIncludeSpace(cboSRCardProvider, AppEnum.StandardReference.CardProvider);
            StandardReference.InitializeIncludeSpace(cboSRCardType, AppEnum.StandardReference.CardType);
            StandardReference.InitializeIncludeSpace(cboSRDiscountReason, AppEnum.StandardReference.DiscountReason);

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

                var coll = (TransPaymentItemCollection)Session["PaymentReceive:collTransPaymentItem" + GetRegNo()];
                if (!coll.HasData)
                    hdnSequenceNo.Value = "001";
                else
                {
                    int seqNo = 0;
                    foreach (TransPaymentItem item in coll)
                    {
                        if (int.Parse(item.SequenceNo) > seqNo)
                            seqNo = int.Parse(item.SequenceNo);
                    }
                    hdnSequenceNo.Value = string.Format("{0:000}", seqNo + 1);

                    //if (coll.Count == 0)
                    //    hdnSequenceNo.Value = "001";
                    //else
                    //{
                    //    var seqNo = int.Parse(coll[coll.Count - 1].SequenceNo) + 1;
                    //    hdnSequenceNo.Value = string.Format("{0:000}", seqNo);
                    //}
                }

                txtAmount.Value = 0D;
                PrevoiusAmount = 0;
                return;
            }

            var bank = new BankCollection();
            bank.LoadAll();

            foreach (var collection in bank)
            {
                cboBank.Items.Add(new RadComboBoxItem(collection.BankName, collection.BankID));
            }

            ViewState["IsNewRecord"] = false;

            ResetValue(true);

            hdnSequenceNo.Value = (String)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.SequenceNo);
            cboSRPaymentType.SelectedValue = (String)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.SRPaymentType);

            cboSRPaymentMethod.Items.Clear();
            cboSRPaymentMethod.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            var pmColl = new PaymentMethodCollection();
            pmColl.Query.Where(pmColl.Query.SRPaymentTypeID == cboSRPaymentType.SelectedValue);
            pmColl.LoadAll();

            foreach (var pm in pmColl)
            {
                cboSRPaymentMethod.Items.Add(new RadComboBoxItem(pm.PaymentMethodName, pm.SRPaymentMethodID));
            }

            cboSRPaymentMethod.SelectedValue = (String)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.SRPaymentMethod);

            cboSRCardProvider.SelectedValue = (String)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.SRCardProvider);
            PopulateEDCMachine();

            cboSRCardType.SelectedValue = (String)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.SRCardType);
            cboSRDiscountReason.SelectedValue = (String)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.SRDiscountReason);
            cboEDCMachineID.SelectedValue = (String)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.EDCMachineID);
            txtCardNo.Text = (String)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.CardNo);
            txtCardHolderName.Text = (String)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.CardHolderName);
            txtCardFeeAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.CardFeeAmount));
            cboBank.SelectedValue = (String)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.BankID);

            txtAmount.Value = Convert.ToDouble((decimal)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.Amount));
            txtRoundingAmount.Value = Convert.ToDouble((decimal)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.RoundingAmount));
            txtPaymentAmount.Value = Convert.ToDouble((decimal)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.AmountReceived));
            txtReturnAmount.Value = txtPaymentAmount.Value > 0 ? txtPaymentAmount.Value - txtAmount.Value : 0;

            PrevoiusAmount = Convert.ToDecimal(DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.Amount));

            if (cboSRPaymentType.SelectedValue == AppSession.Parameter.PaymentTypeCorporateAR ||
                cboSRPaymentType.SelectedValue == AppSession.Parameter.PaymentTypePersonalAR ||
                cboSRPaymentType.SelectedValue == AppSession.Parameter.PaymentTypeSaldoAR)
            {
                pnlPaymentMethod.Visible = false;
                pnlDiscountReason.Visible = false;
                pnlBank.Visible = false;
                pnlCardProvider.Visible = false;
                pnlCardDetail.Visible = false;
                pnlCardFeeAmount.Visible = false;
            }
            else
            {
                pnlPaymentMethod.Visible = true;
                SetVisiblePanel();
            }

            if ((bool)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.IsFromDownPayment))
            {
                cboSRPaymentType.Enabled = false;
                cboSRPaymentMethod.Enabled = false;
                txtAmount.ReadOnly = true;
                txtPaymentAmount.ReadOnly = true;
                txtReturnAmount.ReadOnly = true;
                txtRoundingAmount.ReadOnly = true;
            }
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var coll = (TransPaymentItemCollection)Session["PaymentReceive:collTransPaymentItem" + GetRegNo()];

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var id = hdnSequenceNo.Value;
                bool isExist = false;
                bool isDouble = false;
                foreach (var item in coll)
                {
                    //if (cboSRPaymentType.SelectedValue == AppSession.Parameter.PaymentTypeCorporateAR ||
                    //    cboSRPaymentType.SelectedValue == AppSession.Parameter.PaymentTypePersonalAR ||
                    //    cboSRPaymentType.SelectedValue == AppSession.Parameter.PaymentTypeSaldoAR)
                    //{
                    //    if (item.IsFromDownPayment == false)
                    //    {
                    //        isDouble = true;
                    //        break;
                    //    }
                    //}
                    //if (item.SRPaymentType.Equals(AppSession.Parameter.PaymentTypeCorporateAR) ||
                    //    item.SRPaymentType.Equals(AppSession.Parameter.PaymentTypePersonalAR) ||
                    //    item.SRPaymentType.Equals(AppSession.Parameter.PaymentTypeSaldoAR))
                    //{
                    //    if (item.IsFromDownPayment == false)
                    //    {
                    //        isDouble = true;
                    //        break;
                    //    }

                    //}

                    if (cboSRPaymentType.SelectedValue == AppSession.Parameter.PaymentTypePayment && cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodCash)
                    {
                        if (item.IsFromDownPayment == false && item.SRPaymentType == cboSRPaymentType.SelectedValue && item.SRPaymentMethod == cboSRPaymentMethod.SelectedValue)
                        {
                            isDouble = true;
                            break;
                        }
                    }

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
                    return;
                }
                if (isDouble)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Payment with Cash has exist");
                    return;
                }

                //if (isDouble)
                //{
                //    args.IsValid = false;
                //    ((CustomValidator)source).ErrorMessage = string.Format("Payment Type for A/R can't be combined with other payment type in one transaction");
                //    return;
                //}
            }

            double? guarRemaining =
                ((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtTransGuarantorAmount")).Value == 0
                    ? ((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtRemainingAmountGuarantor")).Value
                    : ((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtTransGuarantorAmount")).Value;

            double? patRemaining =
                            ((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtTransPatientAmount")).Value == 0
                                ? ((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtRemainingAmountPatient")).Value
                                : ((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtTransPatientAmount")).Value;

            string labelMsg = ((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtTransPatientAmount")).Value == 0
                                ? "Remaining Amount"
                                : "Transaction / IntermBill Amount";

            //if (cboSRPaymentType.SelectedValue == AppSession.Parameter.PaymentTypeCorporateAR &&
            //    txtAmount.Value > guarRemaining)
            //{
            //    args.IsValid = false;
            //    ((CustomValidator)source).ErrorMessage = "Amount value can't be greater than Guarantor " + labelMsg + ".";
            //    return;
            //}

            //---dicek lagi
            //if ((cboSRPaymentType.SelectedValue == AppSession.Parameter.PaymentTypePersonalAR || cboSRPaymentType.SelectedValue == AppSession.Parameter.PaymentTypePayment) && txtAmount.Value > patRemaining)
            //{
            //    //if (cboSRPaymentMethod.SelectedValue != AppSession.Parameter.PaymentMethodCash)
            //    //{
            //    args.IsValid = false;
            //    ((CustomValidator)source).ErrorMessage = "Amount value can't be greater than Patient " + labelMsg + ".";
            //    return;
            //    //}
            //}

            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSA")
            {
                if (cboSRPaymentType.SelectedValue == AppSession.Parameter.PaymentTypePayment && txtAmount.Value > patRemaining)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "Amount value can't be greater than Patient " + labelMsg + ".";
                    return;
                }
            }

            if (cboSRPaymentType.SelectedValue == AppSession.Parameter.PaymentTypeDiscount)
            {
                //var valll = txtAmount.Value - (patRemaining - (((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtDownPaymentAmount")).Value));
                //if (txtAmount.Value - (patRemaining - (((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtDownPaymentAmount")).Value)) > 1)
                //{
                //    args.IsValid = false;
                //    ((CustomValidator)source).ErrorMessage = "Discount Amount value is invalid";
                //    return;
                //}
            }

            //validated paymentmethod discount
            if (cboSRPaymentType.SelectedValue == AppSession.Parameter.PaymentTypeDiscount && cboSRDiscountReason.SelectedValue == "")
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Discount Reason is required";
                return;
            }

            if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodCreditCard ||
                cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodDebitCard)
            {
                if (string.IsNullOrEmpty(cboSRCardProvider.SelectedValue))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "Card Provider is required";
                    return;
                }

                if (string.IsNullOrEmpty(cboSRCardType.SelectedValue))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "Card Type is required";
                    return;
                }

                if (string.IsNullOrEmpty(cboEDCMachineID.SelectedValue))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "EDC Machine is required";
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
            else if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodPackageBalance)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey((Helper.FindControlRecursive(Page, "txtRegistrationNo") as RadTextBox).Text);

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                if (txtAmount.Value > Convert.ToDouble(pat.PackageBalance ?? 0))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "Package Balance amount is invalid, max : " + string.Format("{0:n2}", pat.PackageBalance ?? 0);
                    return;
                }
            }

            if (txtAmount.Value == 0 || txtAmount.Text.Length == 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Amount is required";
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
            get { return cboSRPaymentType.SelectedValue; }
        }

        public String PaymentTypeName
        {
            get { return cboSRPaymentType.Text; }
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

        public String SRDiscountReason
        {
            get { return cboSRDiscountReason.SelectedValue; }
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
        public bool IsClosed
        {
            get { return chkIsClosed.Checked; }
            set { chkIsClosed.Checked = value; }
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
            var entity = Helper.EDCMachineTariff.GetEDCMachineTariff(cboEDCMachineID.SelectedValue, cboSRCardType.SelectedValue);
            txtCardFeeAmount.Value = ((double)txtAmount.Value * ((entity != null ? (double)entity.EDCMachineTariff : 0) / 100)) + (entity != null ? (double)(entity.AddFeeAmount ?? 0) : 0);
        }

        protected void cboSRPaymentType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRPaymentMethod.Items.Clear();
            cboSRPaymentMethod.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            var reg = new Registration();
            reg.LoadByPrimaryKey(((RadTextBox)Helper.FindControlRecursive(Page, "txtRegistrationNo")).Text);

            var pmColl = new PaymentMethodCollection();

            if (reg.IsDirectPrescriptionReturn == true)
            {
                cboSRPaymentMethod.Items.Add(new RadComboBoxItem(AppSession.Parameter.PaymentMethodCashName, AppSession.Parameter.PaymentMethodCash));
                cboSRPaymentMethod.Items.Add(new RadComboBoxItem(AppSession.Parameter.PaymentMethodTransferName, AppSession.Parameter.PaymentMethodTransfer));
            }
            else
            {
                pmColl.Query.Where(pmColl.Query.SRPaymentTypeID == e.Value, pmColl.Query.SRPaymentMethodID != "PaymentMethod-006");
                pmColl.LoadAll();

                foreach (var pm in pmColl)
                {
                    cboSRPaymentMethod.Items.Add(new RadComboBoxItem(pm.PaymentMethodName, pm.SRPaymentMethodID));
                }

            }
            if (e.Value == AppSession.Parameter.PaymentTypeCorporateAR || e.Value == AppSession.Parameter.PaymentTypePersonalAR || e.Value == AppSession.Parameter.PaymentTypeSaldoAR)
            {
                pnlPaymentMethod.Visible = false;
                pnlDiscountReason.Visible = false;
                pnlBank.Visible = false;
                pnlCardProvider.Visible = false;
                pnlCardDetail.Visible = false;
                pnlCardFeeAmount.Visible = false;
            }
            else if (e.Value == AppSession.Parameter.PaymentTypeDiscount)
            {
                pnlDiscountReason.Visible = true;
                pnlPaymentMethod.Visible = false;
                pnlBank.Visible = false;
                pnlCardProvider.Visible = false;
                pnlCardDetail.Visible = false;
                pnlCardFeeAmount.Visible = false;
            }
            else
            {
                pnlPaymentMethod.Visible = true;
                SetVisiblePanel();
            }

            ResetValue(true);
        }

        private void SetVisiblePanel()
        {
            if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodCash)
            {
                pnlPayment.Visible = true;
                pnlDiscountReason.Visible = false;
                pnlBank.Visible = false;
                pnlCardProvider.Visible = false;
                pnlCardDetail.Visible = false;
                pnlCardFeeAmount.Visible = false;
                pnlClosed.Visible = false;
            }
            else if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodCreditCard)
            {
                pnlPayment.Visible = false;
                pnlDiscountReason.Visible = false;
                pnlBank.Visible = false;
                pnlCardDetail.Visible = true;
                pnlCardProvider.Visible = true;
                pnlCardFeeAmount.Visible = true;
                pnlClosed.Visible = false;
            }
            else if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodDebitCard)
            {
                pnlPayment.Visible = false;
                pnlDiscountReason.Visible = false;
                pnlBank.Visible = false;
                pnlCardDetail.Visible = true;
                pnlCardProvider.Visible = true;
                pnlCardFeeAmount.Visible = true;
                pnlClosed.Visible = false;
            }
            else if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodTransfer)
            {
                pnlPayment.Visible = false;
                pnlDiscountReason.Visible = false;
                pnlBank.Visible = true;
                pnlCardProvider.Visible = false;
                pnlCardDetail.Visible = false;
                pnlCardFeeAmount.Visible = false;
                pnlClosed.Visible = false;
            }    
            else if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodPackageBalance)
            {
                pnlPayment.Visible = false;
                pnlDiscountReason.Visible = false;
                pnlBank.Visible = false;
                pnlCardProvider.Visible = false;
                pnlCardDetail.Visible = false;
                pnlCardFeeAmount.Visible = false;
                pnlClosed.Visible = true;
            }
            else if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodQris)
            {
                pnlPayment.Visible = false;
                pnlDiscountReason.Visible = false;
                pnlBank.Visible = false;
                pnlCardDetail.Visible = true;
                pnlCardProvider.Visible = true;
                pnlCardFeeAmount.Visible = true;
                pnlClosed.Visible = false;
            }
            else
            {
                pnlPayment.Visible = true;
                pnlDiscountReason.Visible = true;
                pnlBank.Visible = true;
                pnlCardProvider.Visible = true;
                pnlCardDetail.Visible = true;
                pnlCardFeeAmount.Visible = true;
                pnlClosed.Visible = false;
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

        private void ResetValue(bool includePaymentMethod)
        {
            if (includePaymentMethod)
                cboSRPaymentMethod.SelectedValue = string.Empty;

            cboSRDiscountReason.SelectedValue = string.Empty;
            cboBank.SelectedValue = string.Empty;

            if (cboSRPaymentType.SelectedValue == AppSession.Parameter.PaymentTypePayment ||
                cboSRPaymentType.SelectedValue == AppSession.Parameter.PaymentTypeDiscount)
            {
                if ((((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtTransPatientAmount")).Value) == 0)
                    txtAmount.Value =
                        (((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtRemainingAmountPatient")).Value);
                else
                {
                    decimal? totAmount = 0;
                    var coll = (TransPaymentItemCollection)Session["PaymentReceive:collTransPaymentItem" + GetRegNo()];
                    if (coll.HasData)
                    {
                        totAmount = ViewState["IsNewRecord"].Equals(true) ? coll.Aggregate(totAmount, (current, item) => current + item.Amount) : coll.Where(item => item.SequenceNo != hdnSequenceNo.Value).Aggregate(totAmount, (current, item) => current + item.Amount);
                    }

                    txtAmount.Value = ((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtTransPatientAmount")).Value - (double)totAmount;
                }
            }
            else if (cboSRPaymentType.SelectedValue == AppSession.Parameter.PaymentTypePersonalAR)
            {
                if ((((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtTransPatientAmount")).Value) == 0)
                    txtAmount.Value =
                        (((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtRemainingAmountPatient")).Value);
                else
                {
                    decimal? totAmount = 0;
                    var coll = (TransPaymentItemCollection)Session["PaymentReceive:collTransPaymentItem" + GetRegNo()];
                    if (coll.HasData)
                    {
                        totAmount = ViewState["IsNewRecord"].Equals(true) ? coll.Aggregate(totAmount, (current, item) => current + item.Amount) : coll.Where(item => item.SequenceNo != hdnSequenceNo.Value).Aggregate(totAmount, (current, item) => current + item.Amount);
                    }

                    txtAmount.Value = ((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtTransPatientAmount")).Value - (double)totAmount;
                }
            }
            else if (cboSRPaymentType.SelectedValue == AppSession.Parameter.PaymentTypeCorporateAR || cboSRPaymentType.SelectedValue == AppSession.Parameter.PaymentTypeSaldoAR)
                txtAmount.Value = (((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtTransGuarantorAmount")).Value) == 0
                                      ? (((RadNumericTextBox)
                                          Helper.FindControlRecursive(Page, "txtRemainingAmountGuarantor")).Value)
                                      : (((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtTransGuarantorAmount")).Value);
            else
                txtAmount.Value = 0D;

            txtRoundingAmount.Value = 0D;

            //if (!includePaymentMethod && cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodCash)
            //{
            //    var amount = Convert.ToDecimal(txtAmount.Value ?? 0);
            //    txtAmount.Value = Convert.ToDouble(Helper.Rounding(amount, AppEnum.RoundingType.Payment));
            //    txtRoundingAmount.Value = txtAmount.Value - Convert.ToDouble(amount);
            //}
            //else
            //{
            //    // RSUI, payment kartu ikut di rounding
            //    if (AppSession.Parameter.HealthcareID == "RSUI" || AppSession.Parameter.HealthcareID == "RSPM")
            //    {
            //        if (!includePaymentMethod && (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodCreditCard || cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodCreditCard))
            //        {
            //            var amount = Convert.ToDecimal(txtAmount.Value ?? 0);
            //            txtAmount.Value = Convert.ToDouble(Helper.Rounding(amount, AppEnum.RoundingType.Payment));
            //            txtRoundingAmount.Value = txtAmount.Value - Convert.ToDouble(amount);
            //        }
            //    }
            //}

            //if (!includePaymentMethod)
            //{
            if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodCash)
            {
                var amount = Convert.ToDecimal(txtAmount.Value ?? 0);
                if (amount >= 0)
                {
                    txtAmount.Value = Convert.ToDouble(Helper.Rounding(amount, AppEnum.RoundingType.Payment));
                    txtRoundingAmount.Value = txtAmount.Value - Convert.ToDouble(amount);
                }
                else
                {
                    amount = Math.Abs(amount);
                    txtAmount.Value = Convert.ToDouble(Helper.Rounding(amount, AppEnum.RoundingType.Payment));
                    txtRoundingAmount.Value = txtAmount.Value - Convert.ToDouble(amount);
                    txtAmount.Value = (-1) * txtAmount.Value;
                    txtRoundingAmount.Value = (-1) * txtRoundingAmount.Value;
                }
            }
            else if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodDebitCard || cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodCreditCard || cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodTransfer)
            {
                var amount = Convert.ToDecimal(txtAmount.Value ?? 0);
                if (amount >= 0)
                {
                    txtAmount.Value = Convert.ToDouble(Helper.Rounding(amount, AppEnum.RoundingType.PaymentWithCard));
                    txtRoundingAmount.Value = txtAmount.Value - Convert.ToDouble(amount);
                }
                else
                {
                    amount = Math.Abs(amount);
                    txtAmount.Value = Convert.ToDouble(Helper.Rounding(amount, AppEnum.RoundingType.PaymentWithCard));
                    txtRoundingAmount.Value = txtAmount.Value - Convert.ToDouble(amount);
                    txtAmount.Value = (-1) * txtAmount.Value;
                    txtRoundingAmount.Value = (-1) * txtRoundingAmount.Value;
                }
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
            chkIsClosed.Checked = false;
        }

        protected void txtPaymentAmount_TextChanged(object sender, EventArgs e)
        {
            txtReturnAmount.Value = txtPaymentAmount.Value > 0 ? txtPaymentAmount.Value - txtAmount.Value : 0;
        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodCash)
            {
                //var amount = Convert.ToDecimal(txtAmount.Value ?? 0);               
                //txtAmount.Value = Convert.ToDouble(Helper.Rounding(amount, AppEnum.RoundingType.Payment));
                //txtRoundingAmount.Value = txtAmount.Value - Convert.ToDouble(amount);

                //if ((((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtTransPatientAmount")).Value) == 0)
                //{
                //    decimal? totAmount = 0;
                //    var coll = (TransPaymentItemCollection)Session["PaymentReceive:collTransPaymentItem" + GetRegNo()];
                //    if (coll.HasData)
                //    {
                //        totAmount = ViewState["IsNewRecord"].Equals(true) ? coll.Aggregate(totAmount, (current, item) => current + item.Amount) : coll.Where(item => item.SequenceNo != hdnSequenceNo.Value).Aggregate(totAmount, (current, item) => current + item.Amount);
                //    }
                //    var amount = Convert.ToDecimal(txtAmount.Value ?? 0);
                //    var amountReal =
                //        (((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtRemainingAmountPatient")).Value);
                //    txtAmount.Value = Convert.ToDouble(Helper.Rounding(amount, AppEnum.RoundingType.Payment));
                //    txtRoundingAmount.Value = txtAmount.Value - Convert.ToDouble(amount);
                //}
                //else
                //{
                //    decimal? totAmount = 0;
                //    var coll = (TransPaymentItemCollection)Session["PaymentReceive:collTransPaymentItem" + GetRegNo()];
                //    if (coll.HasData)
                //    {
                //        totAmount = ViewState["IsNewRecord"].Equals(true) ? coll.Aggregate(totAmount, (current, item) => current + item.Amount) : coll.Where(item => item.SequenceNo != hdnSequenceNo.Value).Aggregate(totAmount, (current, item) => current + item.Amount);
                //    }
                //    var amount = Convert.ToDecimal(txtAmount.Value ?? 0);
                //    var amountReal = ((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtTransPatientAmount")).Value - (double)totAmount;
                //    txtAmount.Value = Convert.ToDouble(Helper.Rounding(amount, AppEnum.RoundingType.Payment));
                //    txtRoundingAmount.Value = txtAmount.Value - Convert.ToDouble(amount);
                //}

                var rounding = AppSession.Parameter.RoundingPayment;
                if (rounding > 0)
                {
                    var amount = Convert.ToDecimal(txtAmount.Value ?? 0);
                    decimal remainingAmount = 0;
                    if ((((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtTransPatientAmount")).Value) == 0)
                    {
                        remainingAmount = Convert.ToDecimal(((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtRemainingAmountPatient")).Value);
                    }
                    else
                    {
                        decimal totAmount = 0;
                        var coll = (TransPaymentItemCollection)Session["PaymentReceive:collTransPaymentItem" + GetRegNo()];
                        if (coll.HasData)
                        {
                            totAmount = ViewState["IsNewRecord"].Equals(true) ? coll.Aggregate(totAmount, (current, item) => current + (item.Amount ?? 0)) : coll.Where(item => item.SequenceNo != hdnSequenceNo.Value).Aggregate(totAmount, (current, item) => current + (item.Amount ?? 0));
                        }

                        remainingAmount = Convert.ToDecimal(((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtTransPatientAmount")).Value) - totAmount;
                    }

                    if (amount >= remainingAmount)
                    {
                        txtAmount.Value = (double)remainingAmount;
                        amount = Convert.ToDecimal(txtAmount.Value ?? 0);

                        txtAmount.Value = Convert.ToDouble(Helper.Rounding(amount, AppEnum.RoundingType.Payment));
                        txtRoundingAmount.Value = txtAmount.Value - Convert.ToDouble(amount);
                    }
                    else
                    {
                        txtAmount.Value = Convert.ToDouble(Helper.Rounding(amount, AppEnum.RoundingType.Payment));
                        if (remainingAmount - amount > rounding)
                        {
                            txtRoundingAmount.Value = 0;
                        }
                        else
                        {
                            txtRoundingAmount.Value = txtAmount.Value - Convert.ToDouble(amount);
                        }
                    }
                }
                else
                    txtRoundingAmount.Value = 0;
            }
            else if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodDebitCard || cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodCreditCard || cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodTransfer)
            {
                //var amount = Convert.ToDecimal(txtAmount.Value ?? 0);               
                //txtAmount.Value = Convert.ToDouble(Helper.Rounding(amount, AppEnum.RoundingType.PaymentWithCard));
                //txtRoundingAmount.Value = txtAmount.Value - Convert.ToDouble(amount);

                //if ((((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtTransPatientAmount")).Value) == 0)
                //{
                //    decimal? totAmount = 0;
                //    var coll = (TransPaymentItemCollection)Session["PaymentReceive:collTransPaymentItem" + GetRegNo()];
                //    if (coll.HasData)
                //    {
                //        totAmount = ViewState["IsNewRecord"].Equals(true) ? coll.Aggregate(totAmount, (current, item) => current + item.Amount) : coll.Where(item => item.SequenceNo != hdnSequenceNo.Value).Aggregate(totAmount, (current, item) => current + item.Amount);
                //    }
                //    var amount = Convert.ToDecimal(txtAmount.Value ?? 0);
                //    var amountReal =
                //        (((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtRemainingAmountPatient")).Value);
                //    txtAmount.Value = Convert.ToDouble(Helper.Rounding(amount, AppEnum.RoundingType.PaymentWithCard));
                //    txtRoundingAmount.Value = txtAmount.Value - Convert.ToDouble(amountReal);
                //}
                //else
                //{
                //    decimal? totAmount = 0;
                //    var coll = (TransPaymentItemCollection)Session["PaymentReceive:collTransPaymentItem" + GetRegNo()];
                //    if (coll.HasData)
                //    {
                //        totAmount = ViewState["IsNewRecord"].Equals(true) ? coll.Aggregate(totAmount, (current, item) => current + item.Amount) : coll.Where(item => item.SequenceNo != hdnSequenceNo.Value).Aggregate(totAmount, (current, item) => current + item.Amount);
                //    }
                //    var amount = Convert.ToDecimal(txtAmount.Value ?? 0);
                //    var amountReal = ((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtTransPatientAmount")).Value - (double)totAmount;
                //    txtAmount.Value = Convert.ToDouble(Helper.Rounding(amount, AppEnum.RoundingType.PaymentWithCard));
                //    txtRoundingAmount.Value = txtAmount.Value - Convert.ToDouble(amountReal);
                //}

                var rounding = AppSession.Parameter.RoundingPaymentWithCard;
                if (rounding > 0)
                {
                    var amount = Convert.ToDecimal(txtAmount.Value ?? 0);
                    decimal remainingAmount = 0;
                    if ((((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtTransPatientAmount")).Value) == 0)
                    {
                        remainingAmount = Convert.ToDecimal(((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtRemainingAmountPatient")).Value);
                    }
                    else
                    {
                        decimal totAmount = 0;
                        var coll = (TransPaymentItemCollection)Session["PaymentReceive:collTransPaymentItem" + GetRegNo()];
                        if (coll.HasData)
                        {
                            totAmount = ViewState["IsNewRecord"].Equals(true) ? coll.Aggregate(totAmount, (current, item) => current + (item.Amount ?? 0)) : coll.Where(item => item.SequenceNo != hdnSequenceNo.Value).Aggregate(totAmount, (current, item) => current + (item.Amount ?? 0));
                        }

                        remainingAmount = Convert.ToDecimal(((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtTransPatientAmount")).Value) - totAmount;
                    }

                    if (amount >= remainingAmount)
                    {
                        txtAmount.Value = (double)remainingAmount;
                        amount = Convert.ToDecimal(txtAmount.Value ?? 0);

                        txtAmount.Value = Convert.ToDouble(Helper.Rounding(amount, AppEnum.RoundingType.PaymentWithCard));
                        txtRoundingAmount.Value = txtAmount.Value - Convert.ToDouble(amount);
                    }
                    else
                    {
                        txtAmount.Value = Convert.ToDouble(Helper.Rounding(amount, AppEnum.RoundingType.PaymentWithCard));
                        if (remainingAmount - amount > rounding)
                        {
                            txtRoundingAmount.Value = 0;
                        }
                        else
                        {
                            txtRoundingAmount.Value = txtAmount.Value - Convert.ToDouble(amount);
                        }
                    }
                }
                else
                    txtRoundingAmount.Value = 0;
            }
            else 
                txtRoundingAmount.Value = 0;
        }
    }
}
