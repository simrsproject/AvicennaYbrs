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
using Temiang.Avicenna.Module.RADT.Cpoe;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    public partial class EyePe2Ctl : BaseAssessmentCtl
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



            gcsCtl.Condition = ent.Condition;
            gcsCtl.Gcs = ent.Consciousness;

            var leftEye = ent.LeftEye;
            txtLVisus.Text = leftEye.Visus;
            //txtLRefractio.Text = leftEye.Refractio;
            //txtLTension.Text = leftEye.Tension;
            txtLCorrection.Text = leftEye.Correction;
            txtLGlasses.Text = leftEye.Glasses;
            txtLOcular.Text = leftEye.Ocular;
            //txtLAnterior.Text = leftEye.Anterior;
            //txtLPosterior.Text = leftEye.Posterior;
            txtLEyeBallPosition.Text = leftEye.EyeBallPosition;
            //txtLEyeBallMovement.Text = leftEye.EyeBallMovement;
            //txtLConfrontation.Text = leftEye.Confrontation;
            //txtLOrbitalBone.Text = leftEye.OrbitalBone;
            txtLPalpebra.Text = leftEye.Palpebra;
            txtLConjungtivaTars.Text = leftEye.ConjungtivaTars;
            txtLConjungtivaBulbi.Text = leftEye.ConjungtivaBulbi;
            //txtLSclera.Text = leftEye.Sclera;
            //txtLLimbCornea.Text = leftEye.LimbCornea;
            txtLCornea.Text = leftEye.Cornea;
            txtLCameraOculiAnterior.Text = leftEye.CameraOculiAnterior;
            txtLIris.Text = leftEye.Iris;
            txtLPupil.Text = leftEye.Pupil;
            txtLLens.Text = leftEye.Lens;
            txtLFundus.Text = leftEye.Fundus;
            //txtLCorpusVitreum.Text = leftEye.CorpusVitreum;
            txtLOther.Text = leftEye.Other;

            var rightEye = ent.RightEye;
            txtRVisus.Text = rightEye.Visus;
            //txtRRefractio.Text = rightEye.Refractio;
            //txtRTension.Text = rightEye.Tension;
            txtRCorrection.Text = rightEye.Correction;
            txtRGlasses.Text = rightEye.Glasses;
            txtROcular.Text = rightEye.Ocular;
            //txtRAnterior.Text = rightEye.Anterior;
            //txtRPosterior.Text = rightEye.Posterior;
            txtREyeBallPosition.Text = rightEye.EyeBallPosition;
            //txtREyeBallMovement.Text = rightEye.EyeBallMovement;
            //txtRConfrontation.Text = rightEye.Confrontation;
            //txtROrbitalBone.Text = rightEye.OrbitalBone;
            txtRPalpebra.Text = rightEye.Palpebra;
            txtRConjungtivaTars.Text = rightEye.ConjungtivaTars;
            txtRConjungtivaBulbi.Text = rightEye.ConjungtivaBulbi;
            //txtRSclera.Text = rightEye.Sclera;
            //txtRLimbCornea.Text = rightEye.LimbCornea;
            txtRCornea.Text = rightEye.Cornea;
            txtRCameraOculiAnterior.Text = rightEye.CameraOculiAnterior;
            txtRIris.Text = rightEye.Iris;
            txtRPupil.Text = rightEye.Pupil;
            txtRLens.Text = rightEye.Lens;
            txtRFundus.Text = rightEye.Fundus;
            //txtRCorpusVitreum.Text = rightEye.CorpusVitreum;
            txtROther.Text = rightEye.Other;


            optIshihara.SelectedIndex = ent.Ishihara.IsAbNormal == null ? 0 : ((bool)ent.Ishihara.IsAbNormal ? 2 : 1);
            txtIshihara.Text = ent.Ishihara.Notes;
            txtPhysicalExamNotes.Text = ent.Notes;
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var ent = new EyePe
            {
                Condition = gcsCtl.Condition,
                Consciousness = gcsCtl.Gcs,
                LeftEye = new EyeTest
                {
                    Visus = txtLVisus.Text,
                    //Refractio = txtLRefractio.Text,
                    //Tension = txtLTension.Text,
                    Correction = txtLCorrection.Text,
                    Glasses = txtLGlasses.Text,
                    Ocular = txtLOcular.Text,
                    //Anterior = txtLAnterior.Text,
                    //Posterior = txtLPosterior.Text,
                    EyeBallPosition = txtLEyeBallPosition.Text,
                    //EyeBallMovement = txtLEyeBallMovement.Text,
                    //Confrontation = txtLConfrontation.Text,
                    //OrbitalBone = txtLOrbitalBone.Text,
                    Palpebra = txtLPalpebra.Text,
                    ConjungtivaTars = txtLConjungtivaTars.Text,
                    ConjungtivaBulbi = txtLConjungtivaBulbi.Text,
                    //Sclera = txtLSclera.Text,
                    //LimbCornea = txtLLimbCornea.Text,
                    Cornea = txtLCornea.Text,
                    CameraOculiAnterior = txtLCameraOculiAnterior.Text,
                    Iris = txtLIris.Text,
                    Pupil = txtLPupil.Text,
                    Lens = txtLLens.Text,
                    Fundus = txtLFundus.Text,
                    //CorpusVitreum = txtLCorpusVitreum.Text,
                    Other = txtLOther.Text
                },
                RightEye = new EyeTest
                {
                    Visus = txtRVisus.Text,
                    //Refractio = txtRRefractio.Text,
                    //Tension = txtRTension.Text,
                    Correction = txtRCorrection.Text,
                    Glasses = txtRGlasses.Text,
                    Ocular = txtROcular.Text,
                    //Anterior = txtRAnterior.Text,
                    //Posterior = txtRPosterior.Text,
                    EyeBallPosition = txtREyeBallPosition.Text,
                    //EyeBallMovement = txtREyeBallMovement.Text,
                    //Confrontation = txtRConfrontation.Text,
                    //OrbitalBone = txtROrbitalBone.Text,
                    Palpebra = txtRPalpebra.Text,
                    ConjungtivaTars = txtRConjungtivaTars.Text,
                    ConjungtivaBulbi = txtRConjungtivaBulbi.Text,
                    //Sclera = txtRSclera.Text,
                    //LimbCornea = txtRLimbCornea.Text,
                    Cornea = txtRCornea.Text,
                    CameraOculiAnterior = txtRCameraOculiAnterior.Text,
                    Iris = txtRIris.Text,
                    Pupil = txtRPupil.Text,
                    Lens = txtRLens.Text,
                    Fundus = txtRFundus.Text,
                    //CorpusVitreum = txtRCorpusVitreum.Text,
                    Other = txtROther.Text
                },
                Notes = txtPhysicalExamNotes.Text
            };

            if (optIshihara.SelectedIndex == 0)
            {
                ent.Ishihara = new AbNormalAndNotes2 { IsAbNormal = null, Notes = txtIshihara.Text };
            }
            else
            {
                ent.Ishihara = new AbNormalAndNotes2 { IsAbNormal = optIshihara.SelectedIndex == 2, Notes = txtIshihara.Text };
            }

            assessment.PhysicalExam = JsonConvert.SerializeObject(ent);

            // Objective
            if (!string.IsNullOrWhiteSpace(rim.Info2))
                rim.Info2 = String.Concat(rim.Info2, Environment.NewLine, GenerateSoapObjective(ent));
            else
                rim.Info2 = GenerateSoapObjective(ent);

            // Save Localist / Body Image 
            var dtbSession = (DataTable)Session["rimBodyImage"];

            var i = 0;
            foreach (DataRow row in dtbSession.Rows)
            {
                var txtNotes = (RadTextBox)lvLocalistStatus.Items[i].FindControl("txtNotes");
                i++;
                if (true.Equals(row["IsModified"]) || !string.IsNullOrWhiteSpace(txtNotes.Text))
                {
                    SaveLocalistStatus(RegistrationInfoMedicID, row["BodyID"].ToString(),
                        (byte[])row["BodyImage"], txtNotes.Text);
                }
            }
        }

        private string GenerateSoapObjective(EyePe pe)
        {
            var strBuilder = new StringBuilder();
            var rightEye = LocalistStatus(pe.RightEye);
            var leftEye = LocalistStatus(pe.LeftEye);
            if (!string.IsNullOrEmpty(pe.Consciousness.PainScale))
            {
                Gcs gcs = new Gcs();
                strBuilder.AppendFormat("Skala Nyeri: ({0}) {1}",
                    pe.Consciousness.PainScale,
                    gcs.PainScaleDesc(pe.Consciousness.PainScale.ToInt()));
                strBuilder.AppendLine(string.Empty);
            }
            if (string.IsNullOrEmpty(pe.Condition))
                strBuilder.Append("1. Keadaan Umum:");
            else
                strBuilder.AppendFormat("1. Keadaan Umum: Sakit {0}", pe.Condition == "Mild" ? "Ringan" : pe.Condition == "Moderate" ? "Sedang" : "Berat");
            strBuilder.AppendLine(string.Empty);
            strBuilder.AppendFormat("2. Kesadaran: {0} GCS: E: {1} M: {2} V: {3}", pe.Consciousness.ConsciousnessDescription, pe.Consciousness.Eye.Score, pe.Consciousness.Motor.Score, pe.Consciousness.Verbal.Score);
            strBuilder.AppendLine(string.Empty);

            if (!string.IsNullOrEmpty(rightEye) || !string.IsNullOrEmpty(leftEye))
            {
                strBuilder.AppendLine("3. Status Lokalis");
                if (!string.IsNullOrEmpty(rightEye))
                {
                    strBuilder.AppendLine("   • Mata Kanan");
                    strBuilder.AppendLine(rightEye);
                }

                if (!string.IsNullOrEmpty(leftEye))
                {
                    strBuilder.AppendLine("   • Mata Kiri");
                    strBuilder.AppendLine(leftEye);
                }

            }
            strBuilder.AppendFormat("• Hasil Tes Ishihara: {0} : {1}", pe.Ishihara.IsAbNormal == null ? "Tidak diperiksa": pe.Ishihara.IsAbNormal == true ? "Buta Warna" : "Normal", pe.Ishihara.Notes);
            strBuilder.AppendLine(string.Empty);

            if (!string.IsNullOrEmpty(pe.Notes))
            {
                strBuilder.AppendFormat("{0}", pe.Notes);
                strBuilder.AppendLine(string.Empty);
            }

            return strBuilder.ToString();
        }

        private string LocalistStatus(EyeTest eyeTest)
        {
            var strb = new StringBuilder();
            Append(strb, "Visus", eyeTest.Visus);
            //Append(strb, "Refractio", eyeTest.Refractio);
            //Append(strb, "Tension culi", eyeTest.Tension);
            Append(strb, "Koreksi", eyeTest.Correction);
            Append(strb, "Kacamata baca", eyeTest.Glasses);
            Append(strb, "Tekanan intra ocular", eyeTest.Ocular);
            //Append(strb, "Segmen Anterior", eyeTest.Anterior);
            //Append(strb, "Segmen Posterior", eyeTest.Posterior);
            Append(strb, "Kedudukan bola mata", eyeTest.EyeBallPosition);
            //Append(strb, "Gerak bola mata", eyeTest.EyeBallMovement);
            //Append(strb, "Konfrontasi", eyeTest.Confrontation);
            //Append(strb, "Tulang orbita", eyeTest.OrbitalBone);
            Append(strb, "Palpebra", eyeTest.Palpebra);
            Append(strb, "Conjungtiva tars", eyeTest.ConjungtivaTars);
            Append(strb, "Conjungtiva bulbi", eyeTest.ConjungtivaBulbi);
            //Append(strb, "Sklera", eyeTest.Sclera);
            //Append(strb, "Tungkai Kornea", eyeTest.LimbCornea);
            Append(strb, "Kornea", eyeTest.Cornea);
            Append(strb, "Camera oculi anterior", eyeTest.CameraOculiAnterior);
            Append(strb, "Iris", eyeTest.Iris);
            Append(strb, "Pupil", eyeTest.Pupil);
            Append(strb, "Lensa", eyeTest.Lens);
            Append(strb, "Fundus", eyeTest.Fundus);
            //Append(strb, "Corpus vitreum", eyeTest.CorpusVitreum);
            Append(strb, "Lain-lain", eyeTest.Other);
            return strb.ToString();
        }

        private void Append(StringBuilder str, string label, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                str.AppendFormat("      - {0}: {1}", label, value);
                str.AppendLine(string.Empty);
            }
        }


        private void SaveLocalistStatus(string regInfoMedicID, string bodyId, byte[] bodyImage, string notes)
        {
            var es = new RegistrationInfoMedicBodyDiagram();
            if (!es.LoadByPrimaryKey(regInfoMedicID, bodyId))
            {
                es = new RegistrationInfoMedicBodyDiagram
                {
                    RegistrationInfoMedicID = regInfoMedicID,
                    IsDeleted = false,
                    ServiceUnitID = Request.QueryString["unit"],
                    ParamedicID = ParamedicID,
                    BodyID = bodyId,
                    CreatedDateTime = DateTime.Now,
                    CreatedByUserID = AppSession.UserLogin.UserID
                };
            }

            es.BodyImage = bodyImage;
            es.Notes = notes;
            es.Save();
        }


        protected void lvLocalistStatus_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            if (!IsPostBack || !RegistrationInfoMedicID.Equals(Session["rimBodyImage_id"]))
            {
                DataTable dtb;
                if (Request.QueryString["rt"] == "IPR")
                    dtb = BodyDiagramInPatient();
                else
                {
                    dtb = BodyDiagramOutPatient();
                    if (dtb.Rows.Count == 0)
                        dtb = BodyDiagramInPatient(); // Ambil dari AssessmentType Matrix
                }

                Session["rimBodyImage_id"] = RegistrationInfoMedicID;
                LocalistStatusEntry.BodyImages = dtb;
            }

            var dtbSession = LocalistStatusEntry.BodyImages;
            lvLocalistStatus.DataSource = dtbSession;
        }

        private DataTable BodyDiagramOutPatient()
        {
            var qr = new RegistrationInfoMedicBodyDiagramQuery("rim");
            var qrSubd = new BodyDiagramServiceUnitQuery("bsu");
            qr.RightJoin(qrSubd)
                .On(qr.BodyID == qrSubd.BodyID && qr.RegistrationInfoMedicID == RegistrationInfoMedicID);

            var qrBody = new BodyDiagramQuery("bd");
            qr.InnerJoin(qrBody).On(qrSubd.BodyID == qrBody.BodyID);

            qr.Select(qr.RegistrationInfoMedicID,
                "<CASE WHEN rim.RegistrationInfoMedicID IS NULL THEN bd.BodyImage ELSE rim.BodyImage END as BodyImage>",
                qr.LastUpdateByUserID, qr.CreatedDateTime, qr.LastUpdateDateTime, qr.Notes,
                "<CASE WHEN rim.RegistrationInfoMedicID IS NULL THEN 'new' ELSE 'edit' END as EntryMode>",
                qrBody.BodyID, qrBody.BodyName, "<CONVERT(BIT,0) IsModified>");

            qr.Where(qrSubd.ServiceUnitID == ServiceUnitID);

            var dtb = qr.LoadDataTable();
            return dtb;
        }
        private DataTable BodyDiagramInPatient()
        {
            var qr = new RegistrationInfoMedicBodyDiagramQuery("rim");
            var qrSubd = new AssessmentTypeBodyDiagramQuery("bsu");
            qr.RightJoin(qrSubd)
                .On(qr.BodyID == qrSubd.BodyID && qr.RegistrationInfoMedicID == RegistrationInfoMedicID);

            var qrBody = new BodyDiagramQuery("bd");
            qr.InnerJoin(qrBody).On(qrSubd.BodyID == qrBody.BodyID);

            qr.Select(qr.RegistrationInfoMedicID,
                "<CASE WHEN rim.RegistrationInfoMedicID IS NULL THEN bd.BodyImage ELSE rim.BodyImage END as BodyImage>",
                qr.LastUpdateByUserID, qr.CreatedDateTime, qr.LastUpdateDateTime, qr.Notes,
                "<CASE WHEN rim.RegistrationInfoMedicID IS NULL THEN 'new' ELSE 'edit' END as EntryMode>",
                qrBody.BodyID, qrBody.BodyName, "<CONVERT(BIT,0) IsModified>");

            qr.Where(qrSubd.SRAssessmentType == AssessmentType);

            var dtb = qr.LoadDataTable();
            return dtb;
            #endregion
        }
    }
}