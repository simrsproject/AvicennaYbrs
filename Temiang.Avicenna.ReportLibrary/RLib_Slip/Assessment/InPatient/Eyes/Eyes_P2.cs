using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Assessment.InPatient
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Eyes_P2.
    /// </summary>
    public partial class Eyes_P2 : Telerik.Reporting.Report
    {
        public Eyes_P2(string programID, PrintJobParameterCollection printJobParameters, Patient pat, BusinessObject.Registration reg, PatientAssessment asses)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            PopulatePhysicalExam(asses, reg);
            PopulateLocalistStatus(asses);

            txtAncillaryExam.Value = asses.OtherExam;
            txtDiagnosa.Value = asses.Diagnose;

            PopulateTherapy(asses.RegistrationInfoMedicID);

            txtLamaRawat.Value = string.Format("{0:n0}", asses.EstimatedDayInPatient);
            txtPrognosis.Value = asses.Prognosis;
            txtParamedicName.Value = ParamedicTeam.DPJP(reg.RegistrationNo).ParamedicName; 
        }
                   

        private void PopulateTherapy(string registrationInfoMedicID)
        {
            var rim = new RegistrationInfoMedic();
            rim.LoadByPrimaryKey(registrationInfoMedicID);
            txtTherapy.Value = rim.Info4;
        }

        private void PopulateLocalistStatus(PatientAssessment asses)
        {
            // Reset Image
            picLocalistStatus01.Value = null;

            // Update Image
            var loc = new RegistrationInfoMedicBodyDiagramCollection();
            loc.Query.Where(loc.Query.RegistrationInfoMedicID == asses.RegistrationInfoMedicID);
            if (loc.LoadAll())
            {
                if (loc.Count > 0 && loc[0] != null)
                {
                    picLocalistStatus01.Value = (new ImageHelper()).ConvertByteArrayToImage(loc[0].BodyImage);
                }

            }
        }
        private void PopulatePhysicalExam(PatientAssessment asses, BusinessObject.Registration reg)
        {

            if (string.IsNullOrEmpty(asses.PhysicalExam)) return;
            try
            {
                // Convert to class w json
                var pexam = JsonConvert.DeserializeObject<EyePe>(asses.PhysicalExam);

                //Mata Kiri
                var left = pexam.LeftEye;
                txtLVisus.Value = left.Visus;
                txtLCorrection.Value = left.Correction;
                txtLGlasses.Value = left.Glasses;
                txtLOcular.Value = left.Ocular;
                txtLAnterior.Value = left.Anterior;
                txtLPosterior.Value = left.Posterior;

                chkIshiharaNormal.Value = pexam.Ishihara.IsAbNormal == false;
                chkIshiharaAbnormal.Value = pexam.Ishihara.IsAbNormal;
                txtIshihara.Value = pexam.Ishihara.Notes;
            }
            catch (Exception)
            {
                //Nothing
            }
        }

       
    }
}