using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;


namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class MedicationReceiveOpt : BasePageDialog
    {

        public string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }

        protected string FromRegistrationNo
        {
            get
            {
                if (string.IsNullOrEmpty(Request.QueryString["fregno"]))
                {
                    if (ViewState["fregno"] == null)
                    {
                        var reg = new Registration();
                        reg.LoadByPrimaryKey(RegistrationNo);
                        ViewState["fregno"] = reg.FromRegistrationNo?? string.Empty;
                    }

                    return ViewState["fregno"].ToString();
                }
                else
                {
                    return Request.QueryString["fregno"];
                }
            }
        }

        public string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (AppConstant.Program.PharmaceuticalCare.Equals(Request.QueryString["prgid"]))
                ProgramID = AppConstant.Program.PharmaceuticalCare;
            else
                ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Medication Option of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }
        }
        

        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonCancel.Text = "Close";
            ButtonOk.Visible = false;

            if (!IsPostBack)
            {
                // Diremark dokter jadi bisa akses semua (dr. Elina 27 Jan 2019 )
                //tbarMedication.Items[0].Visible = AppSession.UserLogin.SRUserType == UserLogin.UserType.Doctor;
                //tbarMedication.Items[1].Visible = AppSession.UserLogin.SRUserType != UserLogin.UserType.Doctor;
                //tbarMedication.Items[2].Visible = AppSession.UserLogin.SRUserType != UserLogin.UserType.Doctor;
                //tbarMedication.Items[3].Visible = AppSession.UserLogin.SRUserType != UserLogin.UserType.Doctor;
                //tbarMedication.Items[4].Visible = AppSession.UserLogin.SRUserType != UserLogin.UserType.Doctor;
                //tbarMedication.Items[5].Visible = AppSession.UserLogin.SRUserType != UserLogin.UserType.Doctor;
                //tbarMedication.Items[6].Visible = AppSession.UserLogin.SRUserType != UserLogin.UserType.Doctor;

                // Dokter jadi bisa akses semua (dr. Elina 27 Jan 2019 )
                tbarMedication.Items[0].Visible = AppSession.UserLogin.SRUserType == UserLogin.UserType.Doctor;
            }
        }
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return string.Format("oWnd.argument.url = '{0}'", ViewState["url"]??string.Empty);
        }

        protected void tbarMedication_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            var url = string.Empty;
            var rectype = string.Empty;
            switch (e.Item.Value) {
                case "maintenance":
                    url = "MedicationReceiveMaintenance.aspx";
                    break;               
                case "prescription":
                    url = "MedicationReceiveFromPrescription.aspx";
                    break;
                case "patient":
                    //url = "MedicationReceiveEntry.aspx";
                    url = "MedicationReceiveFromPatientEntry.aspx";
                    break;
                case "serviceunit":
                    url = "MedicationReceiveFromServiceUnit.aspx";
                    break;

                case "adm_reconciliation":
                    url = "MedicationReceiveReconciliaton.aspx";
                    rectype = "adm";
                    break;
                case "trf_reconciliation":
                    url = "MedicationReceiveReconciliaton.aspx";
                    rectype = "trf";
                    break;
                case "dcg_reconciliation":
                    url = "MedicationReceiveReconciliaton.aspx";
                    rectype = "dcg";
                    break;
            }

            ViewState["url"] = string.Format("{0}/Module/RADT/EmrIp/EmrIpCommon/Medication/{1}?mod=view&patid={2}&regno={3}&fregno={5}&rectype={4}", Helper.UrlRoot(), url, PatientID, RegistrationNo, rectype, FromRegistrationNo);
            Helper.RegisterStartupScript(Page, "close", "window.CloseAndApply();");
        }

        protected void tbartbarMedicationSetup_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            var stat = string.Empty;
            switch (e.Item.Value)
            {
                case "udd_setup":
                    stat = "S";
                    break;
                case "udd_verification":
                    stat = "V";
                    break;
                case "udd_realization":
                    stat = "R";
                    break;
            }

            ViewState["url"] = string.Format("{0}/Module/RADT/MedicationStatus/MedicationStatusPerPatient.aspx?stat={5}&progid={1}&wintype=max&patid={2}&regno={3}&fregno={4}", Helper.UrlRoot(), string.Empty, PatientID, RegistrationNo, FromRegistrationNo, stat);
            Helper.RegisterStartupScript(Page, "close", "window.CloseAndApply();");
        }
    }
}
