using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Linq;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class GuarantorReceiptDetail : UserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadTextBox TxtRegistrationNo
        {
            get
            {
                return (RadTextBox)Helper.FindControlRecursive(Page, "txtRegistrationNo");
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                var reg = new Registration();
                if (reg.LoadByPrimaryKey(TxtRegistrationNo.Text))
                    cboGuarantorID.Enabled = (reg.SRBussinesMethod == AppSession.Parameter.BusinessMethodFlavon);
                else
                    cboGuarantorID.Enabled = false;

                ViewState["IsNewRecord"] = true;
                txtPaymentDate.SelectedDate = DateTime.Now.Date;
                txtPaymentTime.Text = DateTime.Now.ToString("HH:mm");
                txtTotalPaymentAmount.Value = 0D;
                txtNotes.Text = string.Empty;
                return;
            }
            ViewState["IsNewRecord"] = false;

            var guarantor = new GuarantorQuery();
            guarantor.Select
                (
                    guarantor.GuarantorID,
                    guarantor.GuarantorName
                );
            guarantor.Where(guarantor.GuarantorID == (String)DataBinder.Eval(DataItem, TransPaymentMetadata.ColumnNames.GuarantorID));

            cboGuarantorID.DataSource = guarantor.LoadDataTable();
            cboGuarantorID.DataBind();

            cboGuarantorID.SelectedValue = (String)DataBinder.Eval(DataItem, TransPaymentMetadata.ColumnNames.GuarantorID);
            object paymentDate = DataBinder.Eval(DataItem, TransPaymentMetadata.ColumnNames.PaymentDate);
            if (paymentDate != null)
                txtPaymentDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, TransPaymentMetadata.ColumnNames.PaymentDate);
            else
                txtPaymentDate.Clear();
            txtPaymentTime.Text = (String)DataBinder.Eval(DataItem, TransPaymentMetadata.ColumnNames.PaymentTime);
            txtTotalPaymentAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransPaymentMetadata.ColumnNames.TotalPaymentAmount));
            txtNotes.Text = (String)DataBinder.Eval(DataItem, TransPaymentMetadata.ColumnNames.Notes);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                if (txtTotalPaymentAmount.Value == 0)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Payment Amount must be greater than zero");
                    return;
                }

                string id = cboGuarantorID.SelectedValue;
                var grr = new Guarantor();
                if (!grr.LoadByPrimaryKey(id))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Selected guarantor does not exist");
                    return;
                }

                bool isExist = false;
                var tpq = new TransPaymentQuery("a");
                var tpiq = new TransPaymentItemQuery("b");
                tpq.InnerJoin(tpiq).On(tpiq.PaymentNo == tpq.PaymentNo && tpq.TransactionCode == TransactionCode.Payment);
                tpq.Where(tpq.RegistrationNo == TxtRegistrationNo.Text, tpq.IsVoid == false,
                          tpq.GuarantorID == id,
                          tpq.Or(tpiq.SRPaymentType == AppSession.Parameter.PaymentTypePersonalAR,
                                 tpiq.SRPaymentType == AppSession.Parameter.PaymentTypeCorporateAR,
                                 tpiq.SRPaymentType == AppSession.Parameter.PaymentTypeSaldoAR));
                          
                DataTable tpdt = tpq.LoadDataTable();
                if (tpdt.Rows.Count > 0)
                    isExist = true;
                
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Guarantor: {0} has exist", cboGuarantorID.Text);
                }
            }
        }

        #region Properties for return entry value

        public String GuarantorId
        {
            get { return cboGuarantorID.SelectedValue; }
        }

        public String GuarantorName
        {
            get { return cboGuarantorID.Text; }
        }

        public DateTime? PaymentDate
        {
            get { return txtPaymentDate.SelectedDate; }
        }

        public String PaymentTime
        {
            get { return txtPaymentTime.TextWithLiterals; }
        }

        public Decimal PaymentAmount
        {
            get { return Convert.ToDecimal(txtTotalPaymentAmount.Value); }
        }

        public String Notes
        {
            get { return txtNotes.Text; }
        }

        #endregion

        protected void cboGuarantorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var rgs = new RegistrationGuarantorCollection();
            rgs.Query.Where(rgs.Query.RegistrationNo == TxtRegistrationNo.Text);
            rgs.LoadAll();

            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new GuarantorQuery("a");
            
            query.es.Distinct = true;
            query.Select
                (
                    query.GuarantorID,
                    query.GuarantorName
                );
            query.Where(query.Or(query.GuarantorID.Like(searchTextContain),
                                 query.GuarantorName.Like(searchTextContain)));
                        
            query.OrderBy(query.GuarantorID.Ascending);

            if (rgs.Count > 0)
            {
                var rgq  = new RegistrationGuarantorQuery("b");
                var tpq = new TransPaymentQuery("c");

                query.InnerJoin(rgq).On(query.GuarantorID == rgq.GuarantorID);
                query.LeftJoin(tpq).On(query.GuarantorID == tpq.GuarantorID && rgq.RegistrationNo == tpq.RegistrationNo && tpq.TransactionCode == "016" && tpq.IsVoid == false);
                query.Where(rgq.RegistrationNo == TxtRegistrationNo.Text);
                query.Where
                    (
                        rgq.RegistrationNo == TxtRegistrationNo.Text,
                        tpq.PaymentNo.IsNull()
                    );
            }
            else
            {
                var r = new Registration();
                r.LoadByPrimaryKey(TxtRegistrationNo.Text);

                query.Where
                    (
                        query.IsActive == true,
                        query.GuarantorID != r.GuarantorID
                    );
                query.es.Top = 10;
            }
            
            DataTable dtb = query.LoadDataTable();

            cboGuarantorID.DataSource = dtb;
            cboGuarantorID.DataBind();
        }

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        protected void cboGuarantorID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var rg = new RegistrationGuarantor();
            if (rg.LoadByPrimaryKey(TxtRegistrationNo.Text, e.Value))
            {
                txtTotalPaymentAmount.Value = Convert.ToDouble(rg.PlafondAmount);
                txtNotes.Text = rg.Notes;
            }
            else
            {
                txtTotalPaymentAmount.Value = 0;
                txtNotes.Text = string.Empty;
            }
            if (txtTotalPaymentAmount.Value == 0)
            {
                var r = new Registration();
                r.LoadByPrimaryKey(TxtRegistrationNo.Text);

                decimal? total = 0;

                var collection = IntermBillGuarantors;

                total = collection.Aggregate(total, (current, item) => current + (item.GuarantorAmount ?? 0) + (item.PatientAmount ?? 0) + (item.AdministrationAmount ?? 0)  + (item.GuarantorAdministrationAmount ?? 0) - (item.DiscAdmPatient ?? 0) - (item.DiscAdmGuarantor ?? 0));
                if (total > Convert.ToDecimal(r.PlavonAmount))
                {
                    // cek payment yang sudah terjadi
                    var ibNos = collection.Select(y => y.IntermBillNo.ToString()).ToArray();
                    var totalPay = Common.Helper.Payment.GetTotalPaymentByIntermbill(ibNos, true, true, r.PlavonAmount.Value);
                    if (totalPay > 0)
                    {
                        txtTotalPaymentAmount.Value = Convert.ToDouble(total) - Convert.ToDouble(totalPay);
                    }
                    else
                    {
                        txtTotalPaymentAmount.Value = Convert.ToDouble(total) - Convert.ToDouble(r.PlavonAmount);
                    }
                }
            }
        }

        private IntermBillCollection IntermBillGuarantors
        {
            get
            {
                var obj = ViewState["VerificationBilling:IntermBillGuarantorsAr" + Request.UserHostName];
                if (obj != null)
                    return ((IntermBillCollection)(obj));

                var registrationNoList = MergeRegistrationList();

                var collection = new IntermBillCollection();

                var query = new IntermBillQuery("a");
                var cc = new CostCalculationQuery("c");

                query.Select(query);
                query.es.Distinct = true;
                query.InnerJoin(cc).On(query.IntermBillNo == cc.IntermBillNo);
                query.Where(
                    query.RegistrationNo.In(registrationNoList),
                    query.IsVoid == false
                    );

                collection.Load(query);

                ViewState["VerificationBilling:IntermBillGuarantorsAr" + Request.UserHostName] = collection;

                return collection;
            }
            set { ViewState["VerificationBilling:IntermBillGuarantorsAr" + Request.UserHostName] = value; }
        }

        private string[] MergeRegistrationList()
        {
            if (ViewState["BillingVerification:MergeRegistration" + Request.UserHostName] == null)
                ViewState["BillingVerification:MergeRegistration" + Request.UserHostName] = Helper.MergeBilling.GetMergeRegistration(TxtRegistrationNo.Text);

            return (string[])ViewState["BillingVerification:MergeRegistration" + Request.UserHostName];
        }
    }
}