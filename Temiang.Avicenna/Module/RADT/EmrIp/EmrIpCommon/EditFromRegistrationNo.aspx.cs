using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class EditFromRegistrationNo : BasePageDialog
    {
        private string PatientID
        {
            get { return Request.QueryString["patid"]; }
        }
        private string RegistrationNo
        {
            get { return Request.QueryString["regno"]; }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var pat = new Patient();
                pat.LoadByPrimaryKey(PatientID);
                this.Title = string.Format("Registration No: {0}, Name: {1}", RegistrationNo, pat.PatientName);

                var regCurr = new Registration();
                regCurr.LoadByPrimaryKey(RegistrationNo);

                var regColl = new RegistrationCollection();
                regColl.Query.Where(regColl.Query.PatientID == PatientID,regColl.Query.SRRegistrationType!=AppConstant.RegistrationType.InPatient,
                    regColl.Query.LastCreateDateTime < regCurr.LastCreateDateTime);
                regColl.Query.OrderBy(regColl.Query.LastCreateDateTime.Descending);
                regColl.LoadAll();

                cboFromRegistrationNo.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Registration reg in regColl)
                {
                    var su = new ServiceUnit();
                    su.LoadByPrimaryKey(reg.ServiceUnitID);
                    cboFromRegistrationNo.Items.Add(new RadComboBoxItem(
                        string.Format("{0} - {1}", reg.RegistrationNo, su.ServiceUnitName), reg.RegistrationNo));
                }

                if (!string.IsNullOrWhiteSpace(regCurr.FromRegistrationNo))
                    ComboBox.SelectedValue(cboFromRegistrationNo,regCurr.FromRegistrationNo);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override bool OnButtonOkClicked()
        {
            var regCurr = new Registration();
            regCurr.LoadByPrimaryKey(RegistrationNo);

            if (string.IsNullOrWhiteSpace(cboFromRegistrationNo.SelectedValue))
                regCurr.str.FromRegistrationNo = string.Empty;
            else
                regCurr.FromRegistrationNo = cboFromRegistrationNo.SelectedValue;
            regCurr.Save();

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.command = 'ok'";
        }
    }
}