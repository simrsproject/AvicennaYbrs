using System;
using System.Linq;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Assessment
{

    /// <summary>
    /// Summary description for Report1.
    /// </summary>
    public partial class RehabilitasiMedik : Telerik.Reporting.Report
    {
        public RehabilitasiMedik(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            Helper.InitializeLogoAndTextBottom(this.pageHeader);

            var patientID = printJobParameters.FindByParameterName("PatientID").ValueString;
            var rimid = printJobParameters.FindByParameterName("RegistrationInfoMedicID").ValueString;

            var pat = new Patient();
            pat.LoadByPrimaryKey(patientID);
            txtPatientName.Value = pat.PatientName;
            txtMedicalNo.Value = pat.MedicalNo;

            var asses = new PatientAssessment();
            asses.LoadByPrimaryKey(rimid);

            var reg = new BusinessObject.Registration();
            reg.LoadByPrimaryKey(asses.RegistrationNo);
            txtBirthDateAge.Value = string.Format("{0} - {1}Y {2}M",
                Convert.ToDateTime(pat.DateOfBirth).ToString(AppConstant.DisplayFormat.Date), reg.AgeInYear,
                reg.AgeInMonth);

            txtRegistrationDate.Value = Convert.ToDateTime(reg.RegistrationDate).ToString(AppConstant.DisplayFormat.Date);
            txtRegistrationTime.Value = reg.RegistrationTime;
            txtHandleTime.Value = Convert.ToDateTime(asses.AssessmentDateTime).ToString("HH:mm");
            chkAlloanamnesis.Value = !asses.IsAutoAnamnesis ?? false;
            chkIsAutoanamnesis.Value = asses.IsAutoAnamnesis ?? false;
            txtHpi.Value = asses.Hpi;
            txtChiefComplaint.Value = reg.Complaint;

            PopulateRiwayatPenyakitDahulu(patientID);

            PopulateRiwayatPenyakitKeluarga(patientID);

            txtMedikamentosa.Value = asses.Medikamentosa;

            PopulatePhysicalExam(asses);

            var par = new Paramedic();
            if (par.LoadByPrimaryKey(reg.ParamedicID))
            {
                txtParamedicName.Value = par.ParamedicName;
            }
        }
        private void PopulatePhysicalExam(PatientAssessment asses)
        {
            if (string.IsNullOrEmpty(asses.PhysicalExam)) return;
            try
            {
                // Convert to class w json
                var pexam = JsonConvert.DeserializeObject<RehabilitationPe>(asses.PhysicalExam);

                //chkKulitNormal.Value = !pexam.Kulit.IsAbNormal;
                //chkKulitAbnormal.Value = pexam.Kulit.IsAbNormal;
                //txtKulit.Value = pexam.Kulit.Notes;

                txtGeneral.Value = pexam.GeneralCondition;
                txtNeuro.Value = pexam.Neuromuskuloskeletal;
                txtCardio.Value = pexam.Cardiorespiratory;

                chkPhysical.Value = pexam.FunctionalProblem.PhysicalActivity;
                chkSwallowing.Value = pexam.FunctionalProblem.Swallowing;
                chkGait.Value = pexam.FunctionalProblem.Gait;
                chkCardio.Value = pexam.FunctionalProblem.Cardiorespiratory;
                chkDefecation.Value = pexam.FunctionalProblem.Defecation;
                chkMicturituin.Value = pexam.FunctionalProblem.Micturition;
                chkNoble.Value = pexam.FunctionalProblem.Noble;
                chkExecution.Value = pexam.FunctionalProblem.Execution;
                chkSensory.Value = pexam.FunctionalProblem.Sensory;
                chkCommunication.Value = pexam.FunctionalProblem.Communication;
                chkBalance.Value = pexam.FunctionalProblem.Balance;
                chkPosture.Value = pexam.FunctionalProblem.Posture;
                chkMuscle.Value = pexam.FunctionalProblem.Muscle;
                chkJoint.Value = pexam.FunctionalProblem.Joint;
                chkLocomotor.Value = pexam.FunctionalProblem.Locomotor;



                chkFim_A.Value = pexam.SpecialExamination.Fim=="A";
                chkFim_I.Value = pexam.SpecialExamination.Fim=="I";
                txtFimNotes.Value = pexam.SpecialExamination.FimDesc;

                chkBarthel_A.Value = pexam.SpecialExamination.BarthelIndex == "A";
                chkBarthel_I.Value = pexam.SpecialExamination.BarthelIndex == "I";
                txtBarthelNotes.Value = pexam.SpecialExamination.BarthelIndexDesc;

                chkDisphagya_A.Value = pexam.SpecialExamination.Disphagya == "A";
                chkDisphagya_I.Value = pexam.SpecialExamination.Disphagya == "I";
                txtDisphagyaNotes.Value = pexam.SpecialExamination.DisphagyaDesc;

                chkMmse_A.Value = pexam.SpecialExamination.Mmse == "A";
                chkMmse_I.Value = pexam.SpecialExamination.Mmse == "I";
                txtMmseNotes.Value = pexam.SpecialExamination.MmseDesc;

                chkToken_A.Value = pexam.SpecialExamination.Token == "A";
                chkToken_I.Value = pexam.SpecialExamination.Token == "I";
                txtTokenNotes.Value = pexam.SpecialExamination.TokenDesc;

                chkTadir_A.Value = pexam.SpecialExamination.Tadir == "A";
                chkTadir_I.Value = pexam.SpecialExamination.Tadir == "I";
                txtTadirNotes.Value = pexam.SpecialExamination.TadirDesc;

                chkBerg_A.Value = pexam.SpecialExamination.BergBalance == "A";
                chkBerg_I.Value = pexam.SpecialExamination.BergBalance == "I";
                txtBergNotes.Value = pexam.SpecialExamination.BergBalanceDesc;

                chkSchober_A.Value = pexam.SpecialExamination.Schober == "A";
                chkSchober_I.Value = pexam.SpecialExamination.Schober == "I";
                txtSchoberNotes.Value = pexam.SpecialExamination.SchoberDesc;

                chkGonioMeter_A.Value = pexam.SpecialExamination.Goniometer == "A";
                chkGonioMeter_I.Value = pexam.SpecialExamination.Goniometer == "I";
                txtGonioMeterNotes.Value = pexam.SpecialExamination.GoniometerDesc;

                chkNoRisk.Value = pexam.SpecialExamination.TimeUpGoTest=="NR";
                chkLowRisk.Value = pexam.SpecialExamination.TimeUpGoTest=="LR";
                chkHighRisk.Value = pexam.SpecialExamination.TimeUpGoTest=="HR";

                txtWongBaker.Value = pexam.SpecialExamination.WongBaker;
                txtVisual.Value = pexam.SpecialExamination.Vas;
                txtNumericRating.Value = pexam.SpecialExamination.Nrs;

                txtSummary.Value = pexam.Summary;
                txtRecomendation.Value = pexam.Recomendation;
                chkBack.Value = pexam.Evaluation == "Back";
                chkContinued.Value = pexam.Evaluation == "Continued";
                txtReason.Value = pexam.EvaluationReason;
            }
            catch (Exception)
            {
                //Nothing
            }
        }

        private void PopulateRiwayatPenyakitKeluarga(string patientID)
        {
            // Update value
            var famhColl = new BusinessObject.FamilyMedicalHistoryCollection();
            famhColl.Query.Where(famhColl.Query.PatientID == patientID);
            famhColl.LoadAll();
            foreach (var famh in famhColl)
            {
                switch (famh.SRMedicalDisease)
                {
                    case "001":
                        chk5Hypertensi.Value = true;
                        break;
                    case "002":
                        chk5Jantung.Value = true;
                        break;
                    case "003":
                        chk5Tumor.Value = true;
                        break;
                    case "004":
                        chk5Hepatitis.Value = true;
                        break;
                    case "005":
                        chk5TBParu.Value = true;
                        break;
                    case "006":
                        chk5Struma.Value = true;
                        break;
                    case "007":
                        chk5DM.Value = true;
                        break;
                    case "008":
                        chk5TBParu.Value = true;
                        break;
                    case "009":
                        chk5KelainanDarah.Value = true;
                        break;
                    case "010":
                        chk5Ginjal.Value = true;
                        break;
                    case "011":
                        chk5Stroke.Value = true;
                        break;
                    case "014":
                        chk5Lain.Value = true;
                        txt5Lain.Value = famh.Notes;
                        break;
                }
            }
        }

        private void PopulateRiwayatPenyakitDahulu(string patientID)
        {
            var pmhColl = new BusinessObject.PastMedicalHistoryCollection();
            pmhColl.Query.Where(pmhColl.Query.PatientID == patientID);
            pmhColl.LoadAll();
            foreach (var pmh in pmhColl)
            {
                switch (pmh.SRMedicalDisease)
                {
                    case "001":
                        chkRpdHypertensi.Value = true;
                        break;
                    case "002":
                        chkRpdJantung.Value = true;
                        break;
                    case "003":
                        chkRpdTumor.Value = true;
                        break;
                    case "004":
                        chkRpdHepatitis.Value = true;
                        break;
                    case "005":
                        chkRpdTBParu.Value = true;
                        break;
                    case "006":
                        chkRpdStruma.Value = true;
                        break;
                    case "007":
                        chkRpdDM.Value = true;
                        break;
                    case "008":
                        chkRpdAsma.Value = true;
                        break;
                    case "009":
                        chkRpdKelainanDarah.Value = true;
                        break;
                    case "010":
                        chkRpdGinjal.Value = true;
                        break;
                    case "011":
                        chkRpdStroke.Value = true;
                        break;
                    case "012":
                        chkRpdTrauma.Value = true;
                        break;
                    case "014":
                        chkRpdLain.Value = true;
                        txtRpdLain.Value = pmh.Notes;
                        break;
                }
            }
        }
    }
}