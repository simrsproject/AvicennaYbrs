using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.MedicationStatus
{
    /// <summary>
    /// Drug consume realization step per Patient
    /// </summary>
    /// Create by: Handono
    /// 
    /// Modif Hist:
    /// ===========
    /// [2003-03-17 Handono]
    /// Add fitur Patient Sign:
    ///     - Untuk tandatangan pasien ketika selesai mengkonsumsi obat pada 1 jadwal
    /// [2003-09-06 Handono]
    /// - Add fitur Handovers

    public partial class MedicationStatusPerPatient : BasePageDialog
    {
        private string PatientID
        {
            get { return Request.QueryString["patid"]; }
        }

        private string RegistrationNo
        {
            get { return Request.QueryString["regno"]; }
        }
        private string FromRegistrationNo
        {
            get { return Request.QueryString["fregno"]; }
        }

        protected string MedicationStep
        {
            get
            {
                return Request.QueryString["stat"];
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            ButtonOk.Visible = false;
            ButtonCancel.Text = "Close";
            if (!IsPostBack)
            {
                txtStartDate.SelectedDate = (new DateTime()).NowAtSqlServer();

                if (AppConstant.Program.PharmaceuticalCare.Equals(Request.QueryString["prgid"]))
                    ProgramID = AppConstant.Program.PharmaceuticalCare;
                else
                    ProgramID = AppConstant.Program.ElectronicMedicalRecord;

                medicationStatusCtl.MedicationStep = MedicationStep;
                medicationStatusCtl.CurrentDate = txtStartDate.SelectedDate.Value;

                lnkPatientSign.Visible = MedicationStep.Equals("R"); //Hanya dihalaman Realisasi saja dimunculkan fitur patient sign
                switch (MedicationStep)
                {
                    case "S":
                        this.Title = "Medication Setup";
                        txtStartDate.Enabled = false;
                        break;
                    case "H":
                        this.Title = "Medication Handovers";
                        break;
                    case "V":
                        this.Title = "Medication Verification";
                        break;
                    case "R":
                        this.Title = "Medication Realization";
                        break;
                }

                // ServiceUnitID
                ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, Temiang.Avicenna.BusinessObject.Reference.TransactionCode.Prescription, false);
                // Remove for not in parameter
                var serviceUnitIdListForUdd = AppParameter.GetParameterValue(AppParameter.ParameterItem.ServiceUnitIdListForUdd);
                if (string.IsNullOrEmpty(serviceUnitIdListForUdd))
                    serviceUnitIdListForUdd = AppParameter.GetParameterValue(AppParameter.ParameterItem.ServiceUnitPharmacyID);

                if (!string.IsNullOrEmpty(serviceUnitIdListForUdd))
                {
                    serviceUnitIdListForUdd = String.Concat(serviceUnitIdListForUdd, ";");
                    var itemSelecteds = new List<RadComboBoxItem>();
                    foreach (RadComboBoxItem item in cboServiceUnitID.Items)
                    {
                        if (string.IsNullOrEmpty(item.Value) || serviceUnitIdListForUdd.Contains(String.Concat(item.Value, ";")))
                            itemSelecteds.Add(item);
                    }

                    cboServiceUnitID.Items.Clear();
                    cboServiceUnitID.Items.AddRange(itemSelecteds);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            medicationStatusCtl.Rebind(MergeRegistrations, cboServiceUnitID.SelectedValue, chkIsIncludeFinished.Checked, chkIsIncludeStopped.Checked, txtStartDate.SelectedDate, optDrugSource.SelectedValue);
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            // Hanya untuk keperluan postback
            // Fungsi sudah diload di event load
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            // Pindah ke aspx menggunakan RadAjaxManagerProxy
            //ajax.AddAjaxSetting(btnFilterSuId, medicationStatusCtl.GridMedicationStatus);
            //ajax.AddAjaxSetting(btnFilterDrugSource, medicationStatusCtl.GridMedicationStatus);
            //ajax.AddAjaxSetting(btnFilterStartDate, medicationStatusCtl.GridMedicationStatus);
            //ajax.AddAjaxSetting(btnFilterStatus, medicationStatusCtl.GridMedicationStatus);

        }
    }
}