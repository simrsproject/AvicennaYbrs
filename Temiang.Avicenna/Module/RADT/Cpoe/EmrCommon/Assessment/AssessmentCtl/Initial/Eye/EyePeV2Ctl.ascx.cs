using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    public partial class EyePeV2Ctl : BaseAssessmentCtl
    {
        public override EntryGroupEnum EntryGroup
        {
            get { return EntryGroupEnum.PhysicalExam; }
        }

        public override ColumnEnum Column
        {
            get { return ColumnEnum.Left; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #region override method

        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {

            var ent = new EyePe();

            // Get Education
            var asses = assessment;
            if (!string.IsNullOrEmpty(asses.PhysicalExam))
            {
                // Convert to class w json
                try
                {
                    ent = JsonConvert.DeserializeObject<EyePe>(asses.PhysicalExam);
                }
                catch (Exception)
                {
                    return;
                }
            }
            else
            {
                return;
            }



            //gcsCtl.Condition = ent.Condition;
            //gcsCtl.Gcs = ent.Consciousness;

            ////Eye Left
            //optLVisus.SelectedIndex = ent.LVisus.IsAbNormal.Value ? 1 : 0;
            //txtLVisus.Text = ent.LVisus.Notes;
            //optLRefractio.SelectedIndex = ent.LRefractio.IsAbNormal.Value ? 1 : 0;
            //txtLRefractio.Text = ent.LRefractio.Notes;
            //optLTension.SelectedIndex = ent.LTension.IsAbNormal.Value ? 1 : 0;
            //txtLTension.Text = ent.LTension.Notes;
            //optLCorrection.SelectedIndex = ent.LCorrection.IsAbNormal.Value ? 1 : 0;
            //txtLCorrection.Text = ent.LCorrection.Notes;
            //optLGlasses.SelectedIndex = ent.LGlasses.IsAbNormal.Value ? 1 : 0;
            //txtLGlasses.Text = ent.LGlasses.Notes;
            //optLOcular.SelectedIndex = ent.LOcular.IsAbNormal.Value ? 1 : 0;
            //txtLOcular.Text = ent.LOcular.Notes;
            //optLAnterior.SelectedIndex = ent.LAnterior.IsAbNormal.Value ? 1 : 0;
            //txtLAnterior.Text = ent.LAnterior.Notes;
            //optLPosterior.SelectedIndex = ent.LPosterior.IsAbNormal.Value ? 1 : 0;
            //txtLPosterior.Text = ent.LPosterior.Notes;
            optLEyeBallPosition.SelectedIndex = ent.LEyeBallPosition.IsAbNormal ? 1 : 0;
            txtLEyeBallPosition.Text = ent.LEyeBallPosition.Notes;
            //optLEyeBallMovement.SelectedIndex = ent.LEyeBallMovement.IsAbNormal.Value ? 1 : 0;
            //txtLEyeBallMovement.Text = ent.LEyeBallMovement.Notes;
            //optLConfrontation.SelectedIndex = ent.LConfrontation.IsAbNormal.Value ? 1 : 0;
            //txtLConfrontation.Text = ent.LConfrontation.Notes;
            //optLOrbitalBone.SelectedIndex = ent.LOrbitalBone.IsAbNormal.Value ? 1 : 0;
            //txtLOrbitalBone.Text = ent.LOrbitalBone.Notes;
            optLPalpebra.SelectedIndex = ent.LPalpebra.IsAbNormal ? 1 : 0;
            txtLPalpebra.Text = ent.LPalpebra.Notes;
            optLConjungtivaTars.SelectedIndex = ent.LConjungtivaTars.IsAbNormal ? 1 : 0;
            txtLConjungtivaTars.Text = ent.LConjungtivaTars.Notes;
            optLConjungtivaBulbi.SelectedIndex = ent.LConjungtivaBulbi.IsAbNormal ? 1 : 0;
            txtLConjungtivaBulbi.Text = ent.LConjungtivaBulbi.Notes;
            //optLSclera.SelectedIndex = ent.LSclera.IsAbNormal.Value ? 1 : 0;
            //txtLSclera.Text = ent.LSclera.Notes;
            //optLLimbCornea.SelectedIndex = ent.LLimbCornea.IsAbNormal.Value ? 1 : 0;
            //txtLLimbCornea.Text = ent.LLimbCornea.Notes;
            optLCornea.SelectedIndex = ent.LCornea.IsAbNormal ? 1 : 0;
            txtLCornea.Text = ent.LCornea.Notes;
            optLCameraOculiAnterior.SelectedIndex = ent.LCameraOculiAnterior.IsAbNormal ? 1 : 0;
            txtLCameraOculiAnterior.Text = ent.LCameraOculiAnterior.Notes;
            optLIris.SelectedIndex = ent.LIris.IsAbNormal ? 1 : 0;
            txtLIris.Text = ent.LIris.Notes;
            optLPupil.SelectedIndex = ent.LPupil.IsAbNormal ? 1 : 0;
            txtLPupil.Text = ent.LPupil.Notes;
            optLLens.SelectedIndex = ent.LLens.IsAbNormal ? 1 : 0;
            txtLLens.Text = ent.LLens.Notes;
            optLFundus.SelectedIndex = ent.LFundus.IsAbNormal ? 1 : 0;
            txtLFundus.Text = ent.LFundus.Notes;
            optLCorpusVitreum.SelectedIndex = ent.LCorpusVitreum.IsAbNormal ? 1 : 0;
            txtLCorpusVitreum.Text = ent.LCorpusVitreum.Notes;
            optLOther.SelectedIndex = ent.LOther.IsAbNormal ? 1 : 0;
            txtLOther.Text = ent.LOther.Notes;

            ////Eye Right
            //optRVisus.SelectedIndex = ent.RVisus.IsAbNormal.Value ? 1 : 0;
            //txtRVisus.Text = ent.RVisus.Notes;
            //optRRefractio.SelectedIndex = ent.RRefractio.IsAbNormal.Value ? 1 : 0;
            //txtRRefractio.Text = ent.RRefractio.Notes;
            //optRTension.SelectedIndex = ent.RTension.IsAbNormal.Value ? 1 : 0;
            //txtRTension.Text = ent.RTension.Notes;
            //optRCorrection.SelectedIndex = ent.RCorrection.IsAbNormal.Value ? 1 : 0;
            //txtRCorrection.Text = ent.RCorrection.Notes;
            //optRGlasses.SelectedIndex = ent.RGlasses.IsAbNormal.Value ? 1 : 0;
            //txtRGlasses.Text = ent.RGlasses.Notes;
            //optROcular.SelectedIndex = ent.ROcular.IsAbNormal.Value ? 1 : 0;
            //txtROcular.Text = ent.ROcular.Notes;
            //optRAnterior.SelectedIndex = ent.RAnterior.IsAbNormal.Value ? 1 : 0;
            //txtRAnterior.Text = ent.RAnterior.Notes;
            //optRPosterior.SelectedIndex = ent.RPosterior.IsAbNormal.Value ? 1 : 0;
            //txtRPosterior.Text = ent.RPosterior.Notes;
            optREyeBallPosition.SelectedIndex = ent.REyeBallPosition.IsAbNormal ? 1 : 0;
            txtREyeBallPosition.Text = ent.REyeBallPosition.Notes;
            //optREyeBallMovement.SelectedIndex = ent.REyeBallMovement.IsAbNormal.Value ? 1 : 0;
            //txtREyeBallMovement.Text = ent.REyeBallMovement.Notes;
            //optRConfrontation.SelectedIndex = ent.RConfrontation.IsAbNormal.Value ? 1 : 0;
            //txtRConfrontation.Text = ent.RConfrontation.Notes;
            //optROrbitalBone.SelectedIndex = ent.ROrbitalBone.IsAbNormal.Value ? 1 : 0;
            //txtROrbitalBone.Text = ent.ROrbitalBone.Notes;
            optRPalpebra.SelectedIndex = ent.RPalpebra.IsAbNormal ? 1 : 0;
            txtRPalpebra.Text = ent.RPalpebra.Notes;
            optRConjungtivaTars.SelectedIndex = ent.RConjungtivaTars.IsAbNormal ? 1 : 0;
            txtRConjungtivaTars.Text = ent.RConjungtivaTars.Notes;
            optRConjungtivaBulbi.SelectedIndex = ent.RConjungtivaBulbi.IsAbNormal ? 1 : 0;
            txtRConjungtivaBulbi.Text = ent.RConjungtivaBulbi.Notes;
            //optRSclera.SelectedIndex = ent.RSclera.IsAbNormal.Value ? 1 : 0;
            //txtRSclera.Text = ent.RSclera.Notes;
            //optRLimbCornea.SelectedIndex = ent.RLimbCornea.IsAbNormal.Value ? 1 : 0;
            //txtRLimbCornea.Text = ent.RLimbCornea.Notes;
            optRCornea.SelectedIndex = ent.RCornea.IsAbNormal ? 1 : 0;
            txtRCornea.Text = ent.RCornea.Notes;
            optRCameraOculiAnterior.SelectedIndex = ent.RCameraOculiAnterior.IsAbNormal ? 1 : 0;
            txtRCameraOculiAnterior.Text = ent.RCameraOculiAnterior.Notes;
            optRIris.SelectedIndex = ent.RIris.IsAbNormal ? 1 : 0;
            txtRIris.Text = ent.RIris.Notes;
            optRPupil.SelectedIndex = ent.RPupil.IsAbNormal ? 1 : 0;
            txtRPupil.Text = ent.RPupil.Notes;
            optRLens.SelectedIndex = ent.RLens.IsAbNormal ? 1 : 0;
            txtRLens.Text = ent.RLens.Notes;
            optRFundus.SelectedIndex = ent.RFundus.IsAbNormal ? 1 : 0;
            txtRFundus.Text = ent.RFundus.Notes;
            optRCorpusVitreum.SelectedIndex = ent.RCorpusVitreum.IsAbNormal ? 1 : 0;
            txtRCorpusVitreum.Text = ent.RCorpusVitreum.Notes;
            optROther.SelectedIndex = ent.ROther.IsAbNormal ? 1 : 0;
            txtROther.Text = ent.ROther.Notes;


            optIshihara.SelectedIndex = ent.Ishihara2.IsAbNormal == null ? 0 : ((bool)ent.Ishihara2.IsAbNormal ? 2 : 1);
            txtIshihara.Text = ent.Ishihara2.Notes;
            txtPhysicalExamNotes.Text = ent.Notes;
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var ent = new EyePe
            {
                //Condition = gcsCtl.Condition,
                Consciousness = new Gcs { Eye = new GcsItem(), Motor = new GcsItem(), Verbal = new GcsItem() }
            };

            //ent.Consciousness.Eye.SetValue(gcsCtl.Eye);
            //ent.Consciousness.Motor.SetValue(gcsCtl.Motor);
            //ent.Consciousness.Verbal.SetValue(gcsCtl.Verbal);

            ////Eye Left
            //ent.LVisus = new AbNormalAndNotes2 { IsAbNormal = optLVisus.SelectedIndex == 1, Notes = txtLVisus.Text };
            //ent.LRefractio = new AbNormalAndNotes2 { IsAbNormal = optLRefractio.SelectedIndex == 1, Notes = txtLRefractio.Text };
            //ent.LTension = new AbNormalAndNotes2 { IsAbNormal = optLTension.SelectedIndex == 1, Notes = txtLTension.Text };
            //ent.LCorrection = new AbNormalAndNotes2 { IsAbNormal = optLCorrection.SelectedIndex == 1, Notes = txtLCorrection.Text };
            //ent.LGlasses = new AbNormalAndNotes2 { IsAbNormal = optLGlasses.SelectedIndex == 1, Notes = txtLGlasses.Text };
            //ent.LOcular = new AbNormalAndNotes2 { IsAbNormal = optLOcular.SelectedIndex == 1, Notes = txtLOcular.Text };
            //ent.LAnterior = new AbNormalAndNotes2 { IsAbNormal = optLAnterior.SelectedIndex == 1, Notes = txtLAnterior.Text };
            //ent.LPosterior = new AbNormalAndNotes2 { IsAbNormal = optLPosterior.SelectedIndex == 1, Notes = txtLPosterior.Text };
            ent.LEyeBallPosition = new AbNormalAndNotes { IsAbNormal = optLEyeBallPosition.SelectedIndex == 1, Notes = txtLEyeBallPosition.Text };
            //ent.LEyeBallMovement = new AbNormalAndNotes2 { IsAbNormal = optLEyeBallMovement.SelectedIndex == 1, Notes = txtLEyeBallMovement.Text };
            //ent.LConfrontation = new AbNormalAndNotes2 { IsAbNormal = optLConfrontation.SelectedIndex == 1, Notes = txtLConfrontation.Text };
            //ent.LOrbitalBone = new AbNormalAndNotes2 { IsAbNormal = optLOrbitalBone.SelectedIndex == 1, Notes = txtLOrbitalBone.Text };
            ent.LPalpebra = new AbNormalAndNotes { IsAbNormal = optLPalpebra.SelectedIndex == 1, Notes = txtLPalpebra.Text };
            ent.LConjungtivaTars = new AbNormalAndNotes { IsAbNormal = optLConjungtivaTars.SelectedIndex == 1, Notes = txtLConjungtivaTars.Text };
            ent.LConjungtivaBulbi = new AbNormalAndNotes { IsAbNormal = optLConjungtivaBulbi.SelectedIndex == 1, Notes = txtLConjungtivaBulbi.Text };
            //ent.LSclera = new AbNormalAndNotes2 { IsAbNormal = optLSclera.SelectedIndex == 1, Notes = txtLSclera.Text };
            //ent.LLimbCornea = new AbNormalAndNotes2 { IsAbNormal = optLLimbCornea.SelectedIndex == 1, Notes = txtLLimbCornea.Text };
            ent.LCornea = new AbNormalAndNotes { IsAbNormal = optLCornea.SelectedIndex == 1, Notes = txtLCornea.Text };
            ent.LCameraOculiAnterior = new AbNormalAndNotes { IsAbNormal = optLCameraOculiAnterior.SelectedIndex == 1, Notes = txtLCameraOculiAnterior.Text };
            ent.LIris = new AbNormalAndNotes { IsAbNormal = optLIris.SelectedIndex == 1, Notes = txtLIris.Text };
            ent.LPupil = new AbNormalAndNotes { IsAbNormal = optLPupil.SelectedIndex == 1, Notes = txtLPupil.Text };
            ent.LLens = new AbNormalAndNotes { IsAbNormal = optLLens.SelectedIndex == 1, Notes = txtLLens.Text };
            ent.LFundus = new AbNormalAndNotes { IsAbNormal = optLFundus.SelectedIndex == 1, Notes = txtLFundus.Text };
            ent.LCorpusVitreum = new AbNormalAndNotes { IsAbNormal = optLCorpusVitreum.SelectedIndex == 1, Notes = txtLCorpusVitreum.Text };
            ent.LOther = new AbNormalAndNotes { IsAbNormal = optLOther.SelectedIndex == 1, Notes = txtLOther.Text };

            ////Eye Right
            //ent.RVisus = new AbNormalAndNotes2 { IsAbNormal = optRVisus.SelectedIndex == 1, Notes = txtRVisus.Text };
            //ent.RRefractio = new AbNormalAndNotes2 { IsAbNormal = optRRefractio.SelectedIndex == 1, Notes = txtRRefractio.Text };
            //ent.RTension = new AbNormalAndNotes2 { IsAbNormal = optRTension.SelectedIndex == 1, Notes = txtRTension.Text };
            //ent.RCorrection = new AbNormalAndNotes2 { IsAbNormal = optRCorrection.SelectedIndex == 1, Notes = txtRCorrection.Text };
            //ent.RGlasses = new AbNormalAndNotes2 { IsAbNormal = optRGlasses.SelectedIndex == 1, Notes = txtRGlasses.Text };
            //ent.ROcular = new AbNormalAndNotes2 { IsAbNormal = optROcular.SelectedIndex == 1, Notes = txtROcular.Text };
            //ent.RAnterior = new AbNormalAndNotes2 { IsAbNormal = optRAnterior.SelectedIndex == 1, Notes = txtRAnterior.Text };
            //ent.RPosterior = new AbNormalAndNotes2 { IsAbNormal = optRPosterior.SelectedIndex == 1, Notes = txtRPosterior.Text };
            ent.REyeBallPosition = new AbNormalAndNotes { IsAbNormal = optREyeBallPosition.SelectedIndex == 1, Notes = txtREyeBallPosition.Text };
            //ent.REyeBallMovement = new AbNormalAndNotes2 { IsAbNormal = optREyeBallMovement.SelectedIndex == 1, Notes = txtREyeBallMovement.Text };
            //ent.RConfrontation = new AbNormalAndNotes2 { IsAbNormal = optRConfrontation.SelectedIndex == 1, Notes = txtRConfrontation.Text };
            //ent.ROrbitalBone = new AbNormalAndNotes2 { IsAbNormal = optROrbitalBone.SelectedIndex == 1, Notes = txtROrbitalBone.Text };
            ent.RPalpebra = new AbNormalAndNotes { IsAbNormal = optRPalpebra.SelectedIndex == 1, Notes = txtRPalpebra.Text };
            ent.RConjungtivaTars = new AbNormalAndNotes { IsAbNormal = optRConjungtivaTars.SelectedIndex == 1, Notes = txtRConjungtivaTars.Text };
            ent.RConjungtivaBulbi = new AbNormalAndNotes { IsAbNormal = optRConjungtivaBulbi.SelectedIndex == 1, Notes = txtRConjungtivaBulbi.Text };
            //ent.RSclera = new AbNormalAndNotes2 { IsAbNormal = optRSclera.SelectedIndex == 1, Notes = txtRSclera.Text };
            //ent.RLimbCornea = new AbNormalAndNotes2 { IsAbNormal = optRLimbCornea.SelectedIndex == 1, Notes = txtRLimbCornea.Text };
            ent.RCornea = new AbNormalAndNotes { IsAbNormal = optRCornea.SelectedIndex == 1, Notes = txtRCornea.Text };
            ent.RCameraOculiAnterior = new AbNormalAndNotes { IsAbNormal = optRCameraOculiAnterior.SelectedIndex == 1, Notes = txtRCameraOculiAnterior.Text };
            ent.RIris = new AbNormalAndNotes { IsAbNormal = optRIris.SelectedIndex == 1, Notes = txtRIris.Text };
            ent.RPupil = new AbNormalAndNotes { IsAbNormal = optRPupil.SelectedIndex == 1, Notes = txtRPupil.Text };
            ent.RLens = new AbNormalAndNotes { IsAbNormal = optRLens.SelectedIndex == 1, Notes = txtRLens.Text };
            ent.RFundus = new AbNormalAndNotes { IsAbNormal = optRFundus.SelectedIndex == 1, Notes = txtRFundus.Text };
            ent.RCorpusVitreum = new AbNormalAndNotes { IsAbNormal = optRCorpusVitreum.SelectedIndex == 1, Notes = txtRCorpusVitreum.Text };
            ent.ROther = new AbNormalAndNotes { IsAbNormal = optROther.SelectedIndex == 1, Notes = txtROther.Text };


            if (optIshihara.SelectedIndex == 0)
            {
                ent.Ishihara2 = new AbNormalAndNotes2 { IsAbNormal = null, Notes = txtIshihara.Text };
            }
            else
            {
                ent.Ishihara2 = new AbNormalAndNotes2 { IsAbNormal = optIshihara.SelectedIndex == 2, Notes = txtIshihara.Text };
            }

            ent.Notes = txtPhysicalExamNotes.Text;

            assessment.PhysicalExam = JsonConvert.SerializeObject(ent);

            // Objective
            if (!string.IsNullOrWhiteSpace(rim.Info2))
                rim.Info2 = String.Concat(rim.Info2, Environment.NewLine, GenerateSoapObjective(ent));
            else
                rim.Info2 = GenerateSoapObjective(ent);
        }

        private string GenerateSoapObjective(EyePe pe)
        {
            var strBuilder = new StringBuilder();


            //if (string.IsNullOrEmpty(pe.Condition))
            //    strBuilder.Append("1. Keadaan Umum:");
            //else
            //    strBuilder.AppendFormat("1. Keadaan Umum: Sakit {0}", pe.Condition == "Mild" ? "Ringan" : pe.Condition == "Moderate" ? "Sedang" : "Berat");
            //strBuilder.AppendLine(string.Empty);
            //strBuilder.AppendFormat("2. Kesadaran: {0} GCS: E: {1} M: {2} V: {3}", pe.Consciousness.ConsciousnessDescription, pe.Consciousness.Eye.Score, pe.Consciousness.Motor.Score, pe.Consciousness.Verbal.Score);
            //strBuilder.AppendLine(string.Empty);

            strBuilder.AppendLine("3. Status Lokalis");

            //if (!string.IsNullOrEmpty(rightEye) || !string.IsNullOrEmpty(leftEye))
            //{
            //    strBuilder.AppendLine("3. Status Lokalis");
            //    if (!string.IsNullOrEmpty(rightEye))
            //    {
            //        strBuilder.AppendLine("   • Mata Kanan");
            //        strBuilder.AppendLine(rightEye);
            //    }

            //    if (!string.IsNullOrEmpty(leftEye))
            //    {
            //        strBuilder.AppendLine("   • Mata Kiri");
            //        strBuilder.AppendLine(leftEye);
            //    }

            //}

            //if (!string.IsNullOrEmpty(value))
            //    if (pe.LVisus.IsAbNormal.HasValue)
            //{
            //    strBuilder.AppendFormat("Left Visus: Abnormal: {0} : {1}", pe.LVisus.IsAbNormal.Value ? "AbNormal" : "Normal", pe.LVisus.Notes);
            //    strBuilder.AppendLine(string.Empty);
            //}

            //if (pe.LVisus.IsAbNormal.Value.ToString("null"))
            //{
            //    strBuilder.AppendFormat(" • Left Visus: Abnormal: {0}", pe.LVisus.Notes);
            //    strBuilder.AppendLine(string.Empty);
            //}
            //else if (pe.LVisus.IsAbNormal.Value==false)
            //{
            //    strBuilder.AppendFormat(" • Left Visus: Normal: {0}", pe.LVisus.Notes);
            //    strBuilder.AppendLine(string.Empty);
            //}



            //if (pe.LVisus.IsAbNormal.Value==false)
            //{

            //    strBuilder.AppendFormat(" • Left Visus: Normal: {0}", pe.LVisus.Notes);
            //    strBuilder.AppendLine(string.Empty);
            //}

            var isIncludeNormal = AppParameter.IsYes(AppParameter.ParameterItem.IsSoapFromPysicalExamIncludeNormalValue);

            //if (isIncludeNormal || pe.LVisus.IsAbNormal)
            //{
            //    strBuilder.AppendFormat(" • Left Visus: {1}: {0}", pe.LVisus.Notes, pe.RVisus.IsAbNormal ? "Abnormal" : "Normal");
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (isIncludeNormal || pe.RVisus.IsAbNormal)
            //{
            //    strBuilder.AppendFormat(" • Right Visus: {1}: {0}", pe.RVisus.Notes, pe.RVisus.IsAbNormal ? "Abnormal" : "Normal");
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (isIncludeNormal || pe.LRefractio.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Left Refractio: {0} : {1}", pe.LRefractio.Notes, pe.LRefractio.IsAbNormal ? "Abnormal" : "Normal");
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (isIncludeNormal || pe.RRefractio.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Right Refractio : {0} : {1}", pe.RRefractio.Notes, pe.RRefractio.IsAbNormal ? "Abnormal" : "Normal");
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (isIncludeNormal || pe.LTension.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Left Tension: {0} : {1}", pe.LTension.Notes, pe.LTension.IsAbNormal ? "Abnormal" : "Normal");
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (isIncludeNormal || pe.RTension.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Right Tension: {0} : {1}", pe.RTension.Notes, pe.RTension.IsAbNormal ? "Abnormal" : "Normal");
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (isIncludeNormal || pe.LCorrection.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Left Correction: {0} : {1}", pe.LCorrection.Notes, pe.LCorrection.IsAbNormal ? "Abnormal" : "Normal");
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (isIncludeNormal || pe.RCorrection.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Right Correction: {0} : {1}", pe.RCorrection.Notes, pe.RCorrection.IsAbNormal ? "Abnormal" : "Normal");
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (isIncludeNormal || pe.LGlasses.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Left Reading glasses:  {0} : {1}", pe.LGlasses.Notes, pe.LGlasses.IsAbNormal ? "Abnormal" : "Normal");
            //    strBuilder.AppendLine(string.Empty);
            //}

            //if (isIncludeNormal || pe.RGlasses.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Right Reading glasses: {0} : {1}", pe.RGlasses.Notes, pe.RGlasses.IsAbNormal ? "Abnormal" : "Normal");
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (isIncludeNormal || pe.LOcular.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Left Intra-ocular pressure: {0} : {1}", pe.LOcular.Notes, pe.LOcular.IsAbNormal ? "Abnormal" : "Normal");
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (isIncludeNormal || pe.ROcular.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Right Intra-ocular pressure: {0} : {1}", pe.ROcular.Notes, pe.ROcular.IsAbNormal ? "Abnormal" : "Normal");
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (isIncludeNormal || pe.LAnterior.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Left Segmen anterior: {0} : {1}", pe.LAnterior.Notes, pe.LAnterior.IsAbNormal ? "Abnormal" : "Normal");
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (isIncludeNormal || pe.RAnterior.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Right Segmen anterior: {0} : {1}", pe.RAnterior.Notes, pe.RAnterior.IsAbNormal ? "Abnormal" : "Normal");
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (isIncludeNormal || pe.LPosterior.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Left Segmen Posterior: {0} : {1}", pe.LPosterior.Notes, pe.LPosterior.IsAbNormal ? "Abnormal" : "Normal");
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (isIncludeNormal || pe.RPosterior.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Right Segmen Posterior: {0} : {1}", pe.RPosterior.Notes, pe.RPosterior.IsAbNormal ? "Abnormal" : "Normal");
            //    strBuilder.AppendLine(string.Empty);
            //}
            if (isIncludeNormal || pe.LEyeBallPosition.IsAbNormal)
            {
                strBuilder.AppendFormat("Left Eye Ball Position: {1}: {0}", pe.LEyeBallPosition.Notes, pe.LEyeBallPosition.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.REyeBallPosition.IsAbNormal)
            {
                strBuilder.AppendFormat("Right Eye Ball Position: {1}: {0}", pe.REyeBallPosition.Notes, pe.REyeBallPosition.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            //if (isIncludeNormal || pe.LEyeBallMovement.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Left Eye Ball Movement: {0} : {1}", pe.LEyeBallMovement.Notes, pe.LEyeBallMovement.IsAbNormal ? "Abnormal" : "Normal");
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (isIncludeNormal || pe.REyeBallMovement.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Right Eye Ball Movement: {0} : {1}", pe.REyeBallMovement.Notes, pe.REyeBallMovement.IsAbNormal ? "Abnormal" : "Normal");
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (isIncludeNormal || pe.LConfrontation.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Left Confrontation: {0} : {1}", pe.LConfrontation.Notes, pe.LConfrontation.IsAbNormal ? "Abnormal" : "Normal");
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (isIncludeNormal || pe.RConfrontation.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Right Confrontation: {0} : {1}", pe.RConfrontation.Notes, pe.RConfrontation.IsAbNormal ? "Abnormal" : "Normal");
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (isIncludeNormal || pe.LOrbitalBone.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Left Orbital Bone: {0} : {1}", pe.LOrbitalBone.Notes, pe.LOrbitalBone.IsAbNormal ? "Abnormal" : "Normal");
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (isIncludeNormal || pe.ROrbitalBone.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Right Orbital Bone: {0} : {1}", pe.ROrbitalBone.Notes, pe.ROrbitalBone.IsAbNormal ? "Abnormal" : "Normal");
            //    strBuilder.AppendLine(string.Empty);
            //}
            if (isIncludeNormal || pe.LPalpebra.IsAbNormal)
            {
                strBuilder.AppendFormat("Left Palpebrae: {1}: {0}", pe.LPalpebra.Notes, pe.LPalpebra.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.RPalpebra.IsAbNormal)
            {
                strBuilder.AppendFormat("Right Palpebrae: {1}: {0}", pe.RPalpebra.Notes, pe.RPalpebra.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.LConjungtivaTars.IsAbNormal)
            {
                strBuilder.AppendFormat("Left Conjungtiva Tars: {1}: {0}", pe.LConjungtivaTars.Notes, pe.LConjungtivaTars.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.RConjungtivaTars.IsAbNormal)
            {
                strBuilder.AppendFormat("Right Conjungtiva Tars: {1}: {0}", pe.RConjungtivaTars.Notes, pe.RConjungtivaTars.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.LConjungtivaBulbi.IsAbNormal)
            {
                strBuilder.AppendFormat("Left Conjungtiva Bulbi: {1}: {0}", pe.LConjungtivaBulbi.Notes, pe.LConjungtivaBulbi.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.RConjungtivaBulbi.IsAbNormal)
            {
                strBuilder.AppendFormat("Right Conjungtiva Bulbi: {1}: {0}", pe.RConjungtivaBulbi.Notes, pe.RConjungtivaBulbi.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            //if (isIncludeNormal || pe.LSclera.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Left Sclera: {0} : {1}", pe.LSclera.Notes, pe.LSclera.IsAbNormal ? "Abnormal" : "Normal");
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (isIncludeNormal || pe.RSclera.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Right Sclera : {0} : {1}", pe.RSclera.Notes, pe.RSclera.IsAbNormal ? "Abnormal" : "Normal");
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (isIncludeNormal || pe.LLimbCornea.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Left Limb Cornea: {0} : {1}", pe.LLimbCornea.Notes, pe.LLimbCornea.IsAbNormal ? "Abnormal" : "Normal");
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (isIncludeNormal || pe.RLimbCornea.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Right Limb Cornea: {0} : {1}", pe.RLimbCornea.Notes, pe.RLimbCornea.IsAbNormal ? "Abnormal" : "Normal");
            //    strBuilder.AppendLine(string.Empty);
            //}
            if (isIncludeNormal || pe.LCornea.IsAbNormal)
            {
                strBuilder.AppendFormat("Left Cornea: {1}: {0}", pe.LCornea.Notes, pe.LCornea.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.RCornea.IsAbNormal)
            {
                strBuilder.AppendFormat("Right Cornea: {1}: {0}", pe.RCornea.Notes, pe.RCornea.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.LCameraOculiAnterior.IsAbNormal)
            {
                strBuilder.AppendFormat("Left Camera Oculi Anterior: {1}: {0}", pe.LCameraOculiAnterior.Notes, pe.LCameraOculiAnterior.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.RCameraOculiAnterior.IsAbNormal)
            {
                strBuilder.AppendFormat("Right Camera Oculi Anterior: {1}: {0}", pe.RCameraOculiAnterior.Notes, pe.RCameraOculiAnterior.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.LIris.IsAbNormal)
            {
                strBuilder.AppendFormat("Left Iris: {1}: {0}", pe.LIris.Notes, pe.LIris.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.RIris.IsAbNormal)
            {
                strBuilder.AppendFormat("Right Iris: {1}: {0}", pe.RIris.Notes, pe.RIris.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.LPupil.IsAbNormal)
            {
                strBuilder.AppendFormat("left Pupil: {1}: {0}", pe.LPupil.Notes, pe.LPupil.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.RPupil.IsAbNormal)
            {
                strBuilder.AppendFormat("Right Pupil:  {1}: {0}", pe.RPupil.Notes, pe.RPupil.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.LLens.IsAbNormal)
            {
                strBuilder.AppendFormat("Left Lens: {1}: {0}", pe.LLens.Notes, pe.LLens.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.RLens.IsAbNormal)
            {
                strBuilder.AppendFormat("Right Lens: {1}: {0}", pe.RLens.Notes, pe.RLens.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.LCorpusVitreum.IsAbNormal)
            {
                strBuilder.AppendFormat("Left Corpus Vitreum: {1}: {0}", pe.LCorpusVitreum.Notes, pe.LCorpusVitreum.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.RCorpusVitreum.IsAbNormal)
            {
                strBuilder.AppendFormat("Right Corpus Vitreum: {1}: {0}", pe.RCorpusVitreum.Notes, pe.RCorpusVitreum.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.LFundus.IsAbNormal)
            {
                strBuilder.AppendFormat("Left Fundus: {1}: {0}", pe.LFundus.Notes, pe.LFundus.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.RFundus.IsAbNormal)
            {
                strBuilder.AppendFormat("Right Fundus: {1}: {0} ", pe.RFundus.Notes, pe.RFundus.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            
            if (isIncludeNormal || pe.LOther.IsAbNormal)
            {
                strBuilder.AppendFormat("Left Other: {1}: {0} ", pe.LOther.Notes, pe.LOther.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.ROther.IsAbNormal)
            {
                strBuilder.AppendFormat("Right Other : {1}: {0} ", pe.ROther.Notes, pe.LOther.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            strBuilder.AppendFormat("• Hasil Tes Ishihara: {0} : {1}", pe.Ishihara.IsAbNormal == null ? "Tidak diperiksa" : pe.Ishihara.IsAbNormal == true ? "Buta Warna" : "Normal", pe.Ishihara.Notes);
            strBuilder.AppendLine(string.Empty);

            if (!string.IsNullOrEmpty(pe.Notes))
            {
                strBuilder.AppendFormat("{0}", pe.Notes);
                strBuilder.AppendLine(string.Empty);
            }

            return strBuilder.ToString();
        }

        //private string LocalistStatus(EyeTest eyeTest)
        //{
        //    var strb = new StringBuilder();
        //    Append(strb, "Visus", eyeTest.Visus);
        //    Append(strb, "Refractio", eyeTest.Refractio);
        //    Append(strb, "Tension culi", eyeTest.Tension);
        //    Append(strb, "Koreksi", eyeTest.Correction);
        //    Append(strb, "Kacamata baca", eyeTest.Glasses);
        //    Append(strb, "Tekanan intra ocular", eyeTest.Ocular);
        //    Append(strb, "Segmen Anterior", eyeTest.Anterior);
        //    Append(strb, "Segmen Posterior", eyeTest.Posterior);
        //    Append(strb, "Kedudukan bola mata", eyeTest.EyeBallPosition);
        //    Append(strb, "Gerak bola mata", eyeTest.EyeBallMovement);
        //    Append(strb, "Konfrontasi", eyeTest.Confrontation);
        //    Append(strb, "Tulang orbita", eyeTest.OrbitalBone);
        //    Append(strb, "Palpebra", eyeTest.Palpebra);
        //    Append(strb, "Conjungtiva tars", eyeTest.ConjungtivaTars);
        //    Append(strb, "Conjungtiva bulbi", eyeTest.ConjungtivaBulbi);
        //    Append(strb, "Sklera", eyeTest.Sclera);
        //    Append(strb, "Tungkai Kornea", eyeTest.LimbCornea);
        //    Append(strb, "Kornea", eyeTest.Cornea);
        //    Append(strb, "Camera oculi anterior", eyeTest.CameraOculiAnterior);
        //    Append(strb, "Iris", eyeTest.Iris);
        //    Append(strb, "Pupil", eyeTest.Pupil);
        //    Append(strb, "Lensa", eyeTest.Lens);
        //    Append(strb, "Fundus", eyeTest.Fundus);
        //    Append(strb, "Corpus vitreum", eyeTest.CorpusVitreum);
        //    Append(strb, "Lain-lain", eyeTest.Other);
        //    return strb.ToString();
        //}

        private void Append(StringBuilder str, string label, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                str.AppendFormat("      - {0}: {1}", label, value);
                str.AppendLine(string.Empty);
            }
        }

        #endregion


    }
}