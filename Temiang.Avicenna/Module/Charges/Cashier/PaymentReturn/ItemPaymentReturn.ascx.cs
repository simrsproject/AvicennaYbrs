using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Charges.Cashier
{
    public partial class ItemPaymentReturn : BaseUserControl
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
            pmColl.Query.Where(pmColl.Query.SRPaymentTypeID == AppSession.Parameter.PaymentTypeDownPayment,
                pmColl.Query.SRPaymentMethodID.In(AppSession.Parameter.PaymentMethodCash, AppSession.Parameter.PaymentMethodTransfer));
            pmColl.LoadAll();

            foreach (var pm in pmColl)
            {
                cboSRPaymentMethod.Items.Add(new RadComboBoxItem(pm.PaymentMethodName, pm.SRPaymentMethodID));
            }

            cboBank.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            
            var bankActive = new BankCollection();
            bankActive.Query.Where(bankActive.Query.IsActive == true, bankActive.Query.IsCashierFrontOfficeDpReturn == true);
            bankActive.LoadAll();

            foreach (Bank collection in bankActive)
            {
                cboBank.Items.Add(new RadComboBoxItem(collection.BankName, collection.BankID));
            }

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                TransPaymentItemCollection coll = (TransPaymentItemCollection)Session["DownPaymentReturnItems" + Request.UserHostName];
                if (!coll.HasData)
                    hdnSequenceNo.Value = "001";
                else
                {
                    int seqNo = int.Parse(coll[coll.Count - 1].SequenceNo) + 1;
                    hdnSequenceNo.Value = string.Format("{0:000}", seqNo);
                }

                txtAmount.Value = Convert.ToDouble(Helper.Payment.GetTotalDownPayment(Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regno"])));

                return;
            }
            ViewState["IsNewRecord"] = false;

            hdnSequenceNo.Value = (String)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.SequenceNo);
            hdnReferenceNo.Value = (String)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.ReferenceNo);
            cboSRPaymentMethod.SelectedValue = (String)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.SRPaymentMethod);
            cboBank.SelectedValue = (String)DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.BankID);
            txtAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransPaymentItemMetadata.ColumnNames.Amount));

            pnlBank.Visible = cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodTransfer;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            TransPaymentItemCollection coll = (TransPaymentItemCollection)Session["DownPaymentReturnItems" + Request.UserHostName];

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
                    return;
                }
            }
            if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodTransfer)
            {
                if (string.IsNullOrEmpty(cboBank.SelectedValue))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "Bank is required";
                    return;
                }
            }
        }

        protected void cboSRPaymentMethod_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            pnlBank.Visible = e.Value == AppSession.Parameter.PaymentMethodTransfer;
            cboBank.SelectedValue = string.Empty;
            cboBank.Text = string.Empty;
        }

        #region Properties for return entry value

        public String SequenceNo
        {
            get { return hdnSequenceNo.Value; }
        }

        public String SRPaymentType
        {
            get { return AppSession.Parameter.PaymentTypeDownPayment; }
        }

        public String PaymentTypeName
        {
            get
            {
                var std = new PaymentType();
                std.LoadByPrimaryKey(AppSession.Parameter.PaymentTypeDownPayment);
                return std.PaymentTypeName;
            }
        }

        public String SRPaymentMethod
        {
            get 
            {
                //return AppSession.Parameter.PaymentMethodCash;
                return cboSRPaymentMethod.SelectedValue; 
            }
        }

        public String PaymentMethodName
        {
            get
            {
                //var std = new PaymentMethod();
                //std.LoadByPrimaryKey(AppSession.Parameter.PaymentTypeDownPayment, AppSession.Parameter.PaymentMethodCash);
                //return std.PaymentMethodName;
                return cboSRPaymentMethod.Text;
            }
        }

        public String SRCardProvider
        {
            get { return string.Empty; }
        }

        public String SRCardType
        {
            get { return string.Empty; }
        }

        public String SRDiscountReason
        {
            get { return string.Empty; }
        }

        public String EDCMachineID
        {
            get { return string.Empty; }
        }

        public String CardHolderName
        {
            get { return string.Empty; }
        }

        public Decimal CardFeeAmount
        {
            get { return 0; }
        }

        public String BankID
        {
            get { return cboBank.SelectedValue; }
        }

        public String BankName
        {
            get { return cboBank.Text; }
        }

        public String ReferenceNo
        {
            get { return hdnReferenceNo.Value; }
        }

        public Decimal Amount
        {
            get { return Convert.ToDecimal(txtAmount.Value); }
        }

        #endregion
    }
}
