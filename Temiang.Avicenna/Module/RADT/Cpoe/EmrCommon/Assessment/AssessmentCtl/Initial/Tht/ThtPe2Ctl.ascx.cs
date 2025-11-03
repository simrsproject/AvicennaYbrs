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
    // Versi Tenggorokannya ada 1
    public partial class ThtPe2Ctl : BaseAssessmentCtl
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

            var ent = new ThtPe();

            // Get Education
            var asses = assessment;
            if (!string.IsNullOrEmpty(asses.PhysicalExam))
            {
                // Convert to class w json
                try
                {
                    ent = JsonConvert.DeserializeObject<ThtPe>(asses.PhysicalExam);
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
            //gcsCtl.Eye = ent.Consciousness.Eye.Code;
            //gcsCtl.Motor = ent.Consciousness.Motor.Code;
            //gcsCtl.Verbal = ent.Consciousness.Verbal.Code;
            //gcsCtl.Consciousness = string.Format("{0} [{1}]", ent.Consciousness.ConsciousnessDescription, ent.Consciousness.ConsciousnessValue);

            gcsCtl.Condition = ent.Condition;
            gcsCtl.Gcs = ent.Consciousness;

            #region Telinga
            var rightTelinga = ent.Telinga.Right;
            txtRDaun.Text = rightTelinga.Daun;
            txtRLiang.Text = rightTelinga.Liang;
            txtRTympani.Text = rightTelinga.Tympani;
            txtRPreAurikula.Text = rightTelinga.PreAurikula;
            txtRPostAurikula.Text = rightTelinga.PostAurikula;
            txtRPendengaran.Text = rightTelinga.Pendengaran;
            txtRDischarge.Text = rightTelinga.Discharge;
            txtRTumor.Text = rightTelinga.Tumor;
            txtRMastoid.Text = rightTelinga.Mastoid;
            txtRAudiometri.Text = rightTelinga.Audiometri;
            txtROae.Text = rightTelinga.Oae;
            txtRKeseimbangan.Text = rightTelinga.Pendengaran;

            var leftTelinga = ent.Telinga.Left;
            txtLDaun.Text = leftTelinga.Daun;
            txtLLiang.Text = leftTelinga.Liang;
            txtLTympani.Text = leftTelinga.Tympani;
            txtLPreAurikula.Text = leftTelinga.PreAurikula;
            txtLPostAurikula.Text = leftTelinga.PostAurikula;
            txtLPendengaran.Text = leftTelinga.Pendengaran;
            txtLDischarge.Text = leftTelinga.Discharge;
            txtLTumor.Text = leftTelinga.Tumor;
            txtLMastoid.Text = leftTelinga.Mastoid;
            txtLAudiometri.Text = leftTelinga.Audiometri;
            txtLOae.Text = leftTelinga.Oae;
            txtLKeseimbangan.Text = leftTelinga.Pendengaran;

            #endregion

            #region Hidung
            var rightHidung = ent.Hidung.Right;
            txtRTestHidung.Text = rightHidung.Test;
            txtRHidungLuar.Text = rightHidung.Luar;
            txtRAnterior.Text = rightHidung.Anterior;
            txtRPosterior.Text = rightHidung.Posterior;
            txtRSinus.Text = rightHidung.Sinus;
            txtRKavum.Text = rightHidung.Kavum;
            txtRSeptum.Text = rightHidung.Septum;
            txtRNoseDischarge.Text = rightHidung.Discharge;
            txtRMukosa.Text = rightHidung.Mukosa;
            txtRNoseTumor.Text = rightHidung.Tumor;
            txtRKonka.Text = rightHidung.Konka;
            txtRKoana.Text = rightHidung.Koana;
            txtRNaso.Text = rightHidung.Naso;

            var leftHidung = ent.Hidung.Left;
            txtLTestHidung.Text = leftHidung.Test;
            txtLHidungLuar.Text = leftHidung.Luar;
            txtLAnterior.Text = leftHidung.Anterior;
            txtLPosterior.Text = leftHidung.Posterior;
            txtLSinus.Text = leftHidung.Sinus;
            txtLKavum.Text = leftHidung.Kavum;
            txtLSeptum.Text = leftHidung.Septum;
            txtLNoseDischarge.Text = leftHidung.Discharge;
            txtLMukosa.Text = leftHidung.Mukosa;
            txtLNoseTumor.Text = leftHidung.Tumor;
            txtLKonka.Text = leftHidung.Konka;
            txtLKoana.Text = leftHidung.Koana;
            txtLNaso.Text = leftHidung.Naso;
            #endregion

            #region Larinx
            var lrx = ent.Larinx;
            txtEpiglotis.Text = lrx.Epiglotis;
            txtPVokal.Text = lrx.PVokal;
            txtPVentri.Text = lrx.PVentri;
            txtAritenoid.Text = lrx.Aritenoid;
            txtRimaglotis.Text = lrx.Rimaglotis;
            txtEndoskopi.Text = lrx.Endoskopi;
            #endregion

            #region Tenggorok
            var rightTengorok = ent.Tenggorok.Right;
            txtRRonggaMulut.Text = rightTengorok.Rongga;
            txtRGigi.Text = rightTengorok.Gigi;
            txtRTonsil.Text = rightTengorok.Tonsil;
            txtRFaring.Text = rightTengorok.Faring;
            txtRLaring.Text = rightTengorok.Laring;
            txtDypneu.Text = rightTengorok.Dypneu;
            txtSianosis.Text = rightTengorok.Sianosis;
            txtMucosa.Text = rightTengorok.Mucosa;
            txtStridor.Text = rightTengorok.Stridor;
            txtSound.Text = rightTengorok.Sound;
            #endregion

            txtKepalaLeher.Text = ent.KepalaLeher;
            txtNeckLymph.Text = ent.NeckLymph;
            txtTrakea.Text = ent.Trakea;
            txtEsofagus.Text = ent.Esofagus;
            txtBronkus.Text = ent.Bronkus;
            txtPhysicalExamNotes.Text = ent.Notes;
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var ent = new ThtPe
            {
                Condition = gcsCtl.Condition,
                Consciousness = new Gcs { Eye = new GcsItem(), Motor = new GcsItem(), Verbal = new GcsItem(), PainScale = "0" }
            };

            ent.Consciousness.Eye.SetValue(gcsCtl.Eye);
            ent.Consciousness.Motor.SetValue(gcsCtl.Motor);
            ent.Consciousness.Verbal.SetValue(gcsCtl.Verbal);
            ent.Consciousness.PainScale = gcsCtl.PainScale;

            #region Telinga

            var rightTelinga = new ThtTelingaItem
            {
                Daun = txtRDaun.Text,
                Liang = txtRLiang.Text,
                Tympani = txtRTympani.Text,
                PreAurikula = txtRPreAurikula.Text,
                PostAurikula = txtRPostAurikula.Text,
                Pendengaran = txtRPendengaran.Text,
                Discharge = txtRDischarge.Text,
                Tumor = txtRTumor.Text,
                Mastoid = txtRMastoid.Text,
                Audiometri = txtRAudiometri.Text,
                Oae = txtROae.Text,
                Keseimbangan = txtRKeseimbangan.Text

            };
            ent.Telinga.Right = rightTelinga;

            var leftTelinga = new ThtTelingaItem
            {
                Daun = txtLDaun.Text,
                Liang = txtLLiang.Text,
                Tympani = txtLTympani.Text,
                PreAurikula = txtLPreAurikula.Text,
                PostAurikula = txtLPostAurikula.Text,
                Pendengaran = txtLPendengaran.Text,
                Discharge = txtLDischarge.Text,
                Tumor = txtLTumor.Text,
                Mastoid = txtLMastoid.Text,
                Audiometri = txtLAudiometri.Text,
                Oae = txtLOae.Text,
                Keseimbangan = txtLKeseimbangan.Text
            };
            ent.Telinga.Left = leftTelinga;
            #endregion

            #region Hidung

            var rightHidung = new ThtHidungItem
            {
                Test = txtRTestHidung.Text,
                Luar = txtRHidungLuar.Text,
                Anterior = txtRAnterior.Text,
                Posterior = txtRPosterior.Text,
                Sinus = txtRSinus.Text,
                Kavum = txtRKavum.Text,
                Septum = txtRSeptum.Text,
                Discharge = txtRNoseDischarge.Text,
                Mukosa = txtRMukosa.Text,
                Tumor = txtRNoseTumor.Text,
                Konka = txtRKonka.Text,
                Koana = txtRKoana.Text,
                Naso = txtRNaso.Text
            };
            ent.Hidung.Right = rightHidung;

            var leftHidung = new ThtHidungItem
            {
                Test = txtLTestHidung.Text,
                Luar = txtLHidungLuar.Text,
                Anterior = txtLAnterior.Text,
                Posterior = txtLPosterior.Text,
                Sinus = txtLSinus.Text,
                Kavum = txtLKavum.Text,
                Septum = txtLSeptum.Text,
                Discharge = txtLNoseDischarge.Text,
                Mukosa = txtLMukosa.Text,
                Tumor = txtLNoseTumor.Text,
                Konka = txtLKonka.Text,
                Koana = txtLKoana.Text,
                Naso = txtLNaso.Text
            };
            ent.Hidung.Left = leftHidung;

            #endregion

            #region Tenggorok

            var rightTengorok = new ThtTenggorokItem
            {
                Rongga = txtRRonggaMulut.Text,
                Gigi = txtRGigi.Text,
                Tonsil = txtRTonsil.Text,
                Faring = txtRFaring.Text,
                Laring = txtRLaring.Text,
                Dypneu = txtDypneu.Text,
                Sianosis = txtSianosis.Text,
                Mucosa = txtMucosa.Text,
                Stridor = txtStridor.Text,
                Sound = txtSound.Text
            };
            ent.Tenggorok.Right = rightTengorok;

            #endregion

            var larinx = new ThtLarinxItem
            {
                Epiglotis = txtEpiglotis.Text,
                PVokal = txtPVokal.Text,
                PVentri = txtPVentri.Text,
                Aritenoid = txtAritenoid.Text,
                Rimaglotis = txtRimaglotis.Text,
                Endoskopi = txtEndoskopi.Text
            };
            ent.Larinx = larinx;

            ent.KepalaLeher = txtKepalaLeher.Text;
            ent.NeckLymph = txtNeckLymph.Text;
            ent.Trakea = txtTrakea.Text;
            ent.Esofagus = txtEsofagus.Text;
            ent.Bronkus = txtBronkus.Text;
            ent.Notes = txtPhysicalExamNotes.Text;

            assessment.PhysicalExam = JsonConvert.SerializeObject(ent);

            // Objective
            if (!string.IsNullOrWhiteSpace(rim.Info2))
                rim.Info2 = String.Concat(rim.Info2, Environment.NewLine, GenerateSoapObjective(ent));
            else
                rim.Info2 = GenerateSoapObjective(ent);
        }
        #endregion

        #region SoapObjective
        private string GenerateSoapObjective(ThtPe pe)
        {
            var strBuilder = new StringBuilder();
            //if (!string.IsNullOrEmpty(pe.Condition))
            //{
            //    strBuilder.AppendFormat("Keadaan Umum: Sakit {0}", pe.Condition == "Mild" ? "Ringan" : pe.Condition == "Moderate" ? "Sedang" : "Berat");
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (!string.IsNullOrEmpty(pe.Consciousness.ConsciousnessDescription))
            //{
            //    strBuilder.AppendFormat("Kesadaran: {0} GCS: E: {1} M: {2} V: {3}",
            //        pe.Consciousness.ConsciousnessDescription, pe.Consciousness.Eye.Score, pe.Consciousness.Motor.Score,
            //        pe.Consciousness.Verbal.Score);
            //    strBuilder.AppendLine(string.Empty);
            //}

            strBuilder.AppendLine(pe.Consciousness.GetSoapObjective(pe.Condition));

            if (AppendEars(strBuilder, pe.Telinga) | AppendNose(strBuilder, pe.Hidung) | AppendThroat(strBuilder, pe.Tenggorok))
            {
                strBuilder.AppendLine(string.Empty);
            }

            Append(strBuilder, "Head-Neck", pe.KepalaLeher);
            Append(strBuilder, "Trakea", pe.Trakea);
            Append(strBuilder, "Esofagus", pe.Esofagus);
            Append(strBuilder, "Bronkus", pe.Bronkus);
            Append(strBuilder, "Notes", pe.Notes);
            return strBuilder.ToString();
        }

        private bool AppendEars(StringBuilder strBuilder, ThtTelinga value)
        {
            var lastIndex = strBuilder.Length;
            var exist1 = AppendRightLeft(strBuilder, "• Auricle", value.Right.Daun, value.Left.Daun)
                | AppendRightLeft(strBuilder, "• External acoustic Meatus", value.Right.Liang, value.Left.Liang)
                | AppendRightLeft(strBuilder, "• Discharge", value.Right.Discharge, value.Left.Discharge)
                | AppendRightLeft(strBuilder, "• Tympanic Membrane", value.Right.Tympani, value.Left.Tympani)
                | AppendRightLeft(strBuilder, "• Tumor", value.Right.Tumor, value.Left.Tumor)
                | AppendRightLeft(strBuilder, "• Mastoid", value.Right.Mastoid, value.Left.Mastoid)
                | AppendRightLeft(strBuilder, "• Audiometri", value.Right.Audiometri, value.Left.Audiometri)
                | AppendRightLeft(strBuilder, "• OAE", value.Right.Oae, value.Left.Oae)
                ;

            if (exist1)
            {
                strBuilder.Insert(lastIndex, "Outer Ear :" + Environment.NewLine);
            }

            var exist2 = AppendRightLeft(strBuilder, "Pre Aurikula", value.Right.PreAurikula, value.Left.PreAurikula)
                         | AppendRightLeft(strBuilder, "Post Aurikula", value.Right.PostAurikula, value.Left.PostAurikula)
                         | AppendRightLeft(strBuilder, "Hearing tests", value.Right.Pendengaran, value.Left.Pendengaran)
                         | AppendRightLeft(strBuilder, "Balance tests", value.Right.Keseimbangan, value.Left.Keseimbangan);

            if (exist1 || exist2)
            {
                strBuilder.Insert(lastIndex, string.Format("{0}EARS (Right/Left) :{0}", Environment.NewLine));
                return true;
            }
            return false;
        }


        private bool AppendNose(StringBuilder strBuilder, ThtHidung value)
        {
            var lastIndex = strBuilder.Length;
            var exist1 = (AppendRightLeft(strBuilder, "Tests Nose", value.Right.Test, value.Left.Test)
                          | AppendRightLeft(strBuilder, "External Nose", value.Right.Luar, value.Left.Luar)

                          | AppendRightLeft(strBuilder, "Anterior", value.Right.Luar, value.Left.Anterior)
                          | AppendRightLeft(strBuilder, "Posterior", value.Right.Luar, value.Left.Posterior)
                          | AppendRightLeft(strBuilder, "Sinus", value.Right.Luar, value.Left.Sinus)

                          | AppendRightLeft(strBuilder, "Kavum Nasi", value.Right.Kavum, value.Left.Kavum)
                          | AppendRightLeft(strBuilder, "Septum", value.Right.Septum, value.Left.Septum)
                          | AppendRightLeft(strBuilder, "Discharge", value.Right.Discharge, value.Left.Discharge)
                          | AppendRightLeft(strBuilder, "Mukosa", value.Right.Mukosa, value.Left.Mukosa)
                          | AppendRightLeft(strBuilder, "Tumor", value.Right.Tumor, value.Left.Tumor)
                          | AppendRightLeft(strBuilder, "Konka", value.Right.Konka, value.Left.Konka)
                          | AppendRightLeft(strBuilder, "Koana", value.Right.Koana, value.Left.Koana)
                          | AppendRightLeft(strBuilder, "Naso", value.Right.Koana, value.Left.Naso)
                          );

            var lastIndex2 = strBuilder.Length;
            var exist2 = AppendRightLeft(strBuilder, "• Rhinoscopi Anterior", value.Right.Anterior, value.Left.Anterior)
            | AppendRightLeft(strBuilder, "• Rhinoscopi Posterior", value.Right.Posterior, value.Left.Posterior);
            if (exist2)
                strBuilder.Insert(lastIndex2, "Internal Nose :" + Environment.NewLine);


            if (AppendRightLeft(strBuilder, "Sinus Paranasal", value.Right.Sinus, value.Left.Sinus) | exist1 | exist2)
            {
                strBuilder.Insert(lastIndex, string.Format("{0}NOSE (Right/Left) :{0}", Environment.NewLine));
                return true;
            }
            return false;
        }
        private bool AppendThroat(StringBuilder strBuilder, ThtTenggorok value)
        {
            var lastIndex = strBuilder.Length;

            if (Append(strBuilder, "Oral cavity", value.Right.Rongga)
            | Append(strBuilder, "Teeth", value.Right.Gigi)
            | Append(strBuilder, "Faring", value.Right.Faring)
            | Append(strBuilder, "Laring", value.Right.Laring)
            | Append(strBuilder, "Dypneu", value.Right.Dypneu)
            | Append(strBuilder, "Sianosis", value.Right.Sianosis)
            | Append(strBuilder, "Mucosa", value.Right.Mucosa)
            | Append(strBuilder, "Stridor", value.Right.Stridor)
            | Append(strBuilder, "Sound", value.Right.Sound)
            | Append(strBuilder, "Tonsils", value.Right.Tonsil)
            )
            {
                strBuilder.Insert(lastIndex, string.Format("{0}THROAT :{0}", Environment.NewLine));
                return true;
            }
            return false;
        }
        private bool Append(StringBuilder strBuilder, string label, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                strBuilder.AppendFormat("{0} : {1}", label, value);
                strBuilder.AppendLine(string.Empty);
                return true;
            }
            return false;
        }

        private bool AppendRightLeft(StringBuilder strBuilder, string label, string rightValue, string leftValue)
        {
            if (!string.IsNullOrEmpty(rightValue) || !string.IsNullOrEmpty(leftValue))
            {
                strBuilder.AppendFormat("{0} : {1} / {2}", label, string.IsNullOrEmpty(rightValue) ? "-" : rightValue, string.IsNullOrEmpty(leftValue) ? "-" : leftValue);
                strBuilder.AppendLine(string.Empty);
                return true;
            }
            return false;
        }

        #endregion


    }
}