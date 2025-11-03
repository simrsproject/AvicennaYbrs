using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Linq;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class RegistrationGuarantorDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadComboBox CboGuarantorId
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboGuarantorID"); }
        }

        private RadTextBox TxtRegistrationNo
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtRegistrationNo"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                txtPlafondAmount.Value = 0D;
                cboGuarantorID.Enabled = true;
                txtNotes.Text = string.Empty;
                
                return;
            }
            ViewState["IsNewRecord"] = false;
            cboGuarantorID.Enabled = false;

            ComboBox.PopulateWithOneRoom(cboGuarantorID, (String)DataBinder.Eval(DataItem, RegistrationGuarantorMetadata.ColumnNames.GuarantorID));

            txtPlafondAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, RegistrationGuarantorMetadata.ColumnNames.PlafondAmount));
            txtNotes.Text = (String)DataBinder.Eval(DataItem, RegistrationGuarantorMetadata.ColumnNames.Notes);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (RegistrationGuarantorCollection)Session["collRegistrationGuarantor" + Request.UserHostName];

                string id = cboGuarantorID.SelectedValue;
                bool isExist = false;
                foreach (RegistrationGuarantor item in coll)
                {
                    if (item.GuarantorID.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Guarantor : {0} has exist", cboGuarantorID.Text);
                }
            }
        }

        #region Properties for return entry value

        public String GuarantorID
        {
            get { return cboGuarantorID.SelectedValue; }
        }

        public String GuarantorName
        {
            get { return cboGuarantorID.Text; }
        }

        public Decimal PlafondAmount
        {
            get { return Convert.ToDecimal(txtPlafondAmount.Value); }
        }

        public String Notes
        {
            get { return txtNotes.Text; }
        }
        #endregion

        protected void cboGuarantorID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var r = new Registration();
            if (r.LoadByPrimaryKey(TxtRegistrationNo.Text))
            {
                decimal? total = 0;

                var collection = IntermBillGuarantors;

                total = collection.Aggregate(total, (current, item) => current + (item.GuarantorAmount ?? 0) + (item.PatientAmount ?? 0) + (item.AdministrationAmount ?? 0) + (item.GuarantorAdministrationAmount ?? 0) - (item.DiscAdmPatient ?? 0) - (item.DiscAdmGuarantor ?? 0));
                if (total > Convert.ToDecimal(r.PlavonAmount))
                    txtPlafondAmount.Value = Convert.ToDouble(total) - Convert.ToDouble(r.PlavonAmount);
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