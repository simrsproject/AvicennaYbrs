using System;
using System.Data;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class PatientFallRiskPreventionItem : BaseUserControl
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
                return;
            }
            ViewState["IsNewRecord"] = false;

            PopulateWithOneRegistrationByServiceUnitUser(cboRegistrationNo, (String)DataBinder.Eval(DataItem, CompliancePatientFallRiskPreventionDetailMetadata.ColumnNames.RegistrationNo));
            cboSRFallRiskStatus.SelectedValue = (String)DataBinder.Eval(DataItem, CompliancePatientFallRiskPreventionDetailMetadata.ColumnNames.SRFallRiskStatus);
            cboSRFallRiskPreventionEffort.SelectedValue = (String)DataBinder.Eval(DataItem, CompliancePatientFallRiskPreventionDetailMetadata.ColumnNames.SRFallRiskPreventionEffort);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (CompliancePatientFallRiskPreventionDetailCollection)Session["collCompliancePatientFallRiskPreventionDetail"];

                bool isExist = false;
                foreach (CompliancePatientFallRiskPreventionDetail item in coll)
                {
                    if (item.RegistrationNo.Equals(cboRegistrationNo.SelectedValue))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Registration No: {0} has exist", cboRegistrationNo.Text);
                }
            }
        }

        public static void PopulateWithOneRegistrationByServiceUnitUser(RadComboBox cbo, string regNo)
        {
            cbo.Items.Clear();
            if (string.IsNullOrEmpty(regNo))
                return;

            var coll = new RegistrationCollection();
            var qr = new RegistrationQuery("a");
            var pat = new PatientQuery("b");

            qr.InnerJoin(pat).On(qr.PatientID == pat.PatientID);
            qr.Select(qr.RegistrationNo, @"<'(' + b.MedicalNo + ') ' + RTRIM(LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName)) AS 'MedicalNo'>");
            qr.Where(qr.RegistrationNo == regNo);
            coll.Load(qr);

            foreach (Registration item in coll)
            {
                cbo.Items.Add(new RadComboBoxItem(item.RegistrationNo, item.RegistrationNo));
            }

            cbo.SelectedValue = regNo;
        }

        #region Properties for return entry value
        public String RegistrationNo
        {
            get { return cboRegistrationNo.SelectedValue; }
        }
        public String MedicalNo
        {
            get { return cboRegistrationNo.Text; }
        }
        public String SRFallRiskStatus
        {
            get { return cboSRFallRiskStatus.SelectedValue; }
        }
        public String SRFallRiskPreventionEffort
        {
            get { return cboSRFallRiskPreventionEffort.SelectedValue; }
        }
        #endregion
    }
}