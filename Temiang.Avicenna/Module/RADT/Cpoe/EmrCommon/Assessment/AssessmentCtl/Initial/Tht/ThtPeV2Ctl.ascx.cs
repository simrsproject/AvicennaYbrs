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
    public partial class ThtPeV2Ctl : BaseAssessmentCtl
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
            optDaun.SelectedIndex = ent.Telinga.Daun.IsAbNormal ? 1 : 0;
            txtDaun.Text = ent.Telinga.Daun.Notes;
            optLiang.SelectedIndex = ent.Telinga.Liang.IsAbNormal ? 1 : 0;
            txtLiang.Text = ent.Telinga.Liang.Notes;
            optDischargeEARS.SelectedIndex = ent.Telinga.Discharge.IsAbNormal ? 1 : 0;
            txtDischargeEARS.Text = ent.Telinga.Discharge.Notes;
            optSerumen.SelectedIndex = ent.Telinga.Serumen.IsAbNormal ? 1 : 0;
            txtSerumen.Text = ent.Telinga.Serumen.Notes;
            optTympani.SelectedIndex = ent.Telinga.Tympani.IsAbNormal ? 1 : 0;
            txtTympani.Text = ent.Telinga.Tympani.Notes;
            optTumorEARS.SelectedIndex = ent.Telinga.Tumor.IsAbNormal ? 1 : 0;
            txtTumorEARS.Text = ent.Telinga.Tumor.Notes;
            optMastoid.SelectedIndex = ent.Telinga.Mastoid.IsAbNormal ? 1 : 0;
            txtMastoid.Text = ent.Telinga.Mastoid.Notes;
            OptPreAurikula.SelectedIndex = ent.Telinga.PreAurikula.IsAbNormal ? 1 : 0;
            txtPreAurikula.Text = ent.Telinga.PreAurikula.Notes;
            optPostAurikula.SelectedIndex = ent.Telinga.PostAurikula.IsAbNormal ? 1 : 0;
            txtPostAurikula.Text = ent.Telinga.PostAurikula.Notes;
            optHearingTests.SelectedIndex = ent.Telinga.Hearing.IsAbNormal ? 1 : 0;
            txtHearingTests.Text = ent.Telinga.Hearing.Notes;
            optAudiometri.SelectedIndex = ent.Telinga.Audiometri.IsAbNormal ? 1 : 0;
            txtAudiometri.Text = ent.Telinga.Audiometri.Notes;
            optOAE.SelectedIndex = ent.Telinga.Oae.IsAbNormal ? 1 : 0;
            txtOAE.Text = ent.Telinga.Oae.Notes;
            OptBalanceTests.SelectedIndex = ent.Telinga.Keseimbangan.IsAbNormal ? 1 : 0;
            txtBalanceTests.Text = ent.Telinga.Keseimbangan.Notes;

            #endregion

            #region Hidung
            optExternalNose.SelectedIndex = ent.Hidung.Luar.IsAbNormal ? 1 : 0;
            txtExternalNose.Text = ent.Hidung.Luar.Notes;
            optKavumNasi.SelectedIndex = ent.Hidung.Kavum.IsAbNormal ? 1 : 0;
            txtKavumNasi.Text = ent.Hidung.Kavum.Notes;
            optSeptum.SelectedIndex = ent.Hidung.Septum.IsAbNormal ? 1 : 0;
            txtSeptum.Text = ent.Hidung.Septum.Notes;
            optDischargeNOSE.SelectedIndex = ent.Hidung.DischargeNOSE.IsAbNormal ? 1 : 0;
            txtDischargeNOSE.Text = ent.Hidung.DischargeNOSE.Notes;
            optMukosa.SelectedIndex = ent.Hidung.Mukosa.IsAbNormal ? 1 : 0;
            txtMukosa.Text = ent.Hidung.Mukosa.Notes;
            optTumorNOSE.SelectedIndex = ent.Hidung.TumorNOSE.IsAbNormal ? 1 : 0;
            txtTumorNOSE.Text = ent.Hidung.TumorNOSE.Notes;
            optKonka.SelectedIndex = ent.Hidung.Konka.IsAbNormal ? 1 : 0;
            txtKonka.Text = ent.Hidung.Konka.Notes;
            optSinus.SelectedIndex = ent.Hidung.Sinus.IsAbNormal ? 1 : 0;
            txtSinus.Text = ent.Hidung.Sinus.Notes;
            optKoana.SelectedIndex = ent.Hidung.Koana.IsAbNormal ? 1 : 0;
            txtKoana.Text = ent.Hidung.Koana.Notes;

            #endregion

            #region Larinx
            optEpiglotis.SelectedIndex = ent.Larinx.Epiglotis2.IsAbNormal ? 1 : 0;
            txtEpiglotis.Text = ent.Larinx.Epiglotis2.Notes;
            optPVokalis.SelectedIndex = ent.Larinx.PVokal2.IsAbNormal ? 1 : 0;
            txtPVokalis.Text = ent.Larinx.PVokal2.Notes;
            optPVentrikulosis.SelectedIndex = ent.Larinx.PVentri2.IsAbNormal ? 1 : 0;
            txtPVentrikulosis.Text = ent.Larinx.PVentri2.Notes;
            optAritenoid.SelectedIndex = ent.Larinx.Aritenoid2.IsAbNormal ? 1 : 0;
            txtAritenoid.Text = ent.Larinx.Aritenoid2.Notes;
            optRimaglotis.SelectedIndex = ent.Larinx.Rimaglotis2.IsAbNormal ? 1 : 0;
            txtRimaglotis.Text = ent.Larinx.Rimaglotis2.Notes;
            optEndoskopi.SelectedIndex = ent.Larinx.Endoskopi2.IsAbNormal ? 1 : 0;
            txtEndoskopi.Text = ent.Larinx.Endoskopi2.Notes;
            #endregion

            #region Tenggorok
            optOcavity.SelectedIndex = ent.Tenggorok.Rongga.IsAbNormal ? 1 : 0;
            txtOcavity.Text = ent.Tenggorok.Rongga.Notes;
            optTonsils.SelectedIndex = ent.Tenggorok.Tonsil.IsAbNormal ? 1 : 0;
            txtTonsils.Text = ent.Tenggorok.Tonsil.Notes;
            optFaring.SelectedIndex = ent.Tenggorok.Faring.IsAbNormal ? 1 : 0;
            txtFaring.Text = ent.Tenggorok.Faring.Notes;
            optMucosa.SelectedIndex = ent.Tenggorok.Mucosa.IsAbNormal ? 1 : 0;
            txtMucosa.Text = ent.Tenggorok.Mucosa.Notes;
            optSound.SelectedIndex = ent.Tenggorok.Sound.IsAbNormal ? 1 : 0;
            txtSound.Text = ent.Tenggorok.Sound.Notes;


            #endregion

            optHNeck.SelectedIndex = ent.KepalaLeher2.IsAbNormal ? 1 : 0;
            txtHNeck.Text = ent.KepalaLeher2.Notes;
            optTrakea.SelectedIndex = ent.Trakea2.IsAbNormal ? 1 : 0;
            txtTrakea.Text = ent.Trakea2.Notes;
            optNlglands.SelectedIndex = ent.Nlglands.IsAbNormal ? 1 : 0;
            txtNlglands.Text = ent.Nlglands.Notes;
            optEsofagus.SelectedIndex = ent.Esofagus2.IsAbNormal ? 1 : 0;
            txtEsofagus.Text = ent.Esofagus2.Notes;
            optBronkus.SelectedIndex = ent.Bronkus2.IsAbNormal ? 1 : 0;
            txtBronkus.Text = ent.Bronkus2.Notes;
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var ent = new ThtPe
            {
                Condition = gcsCtl.Condition,
                Consciousness = new Gcs { Eye = new GcsItem(), Motor = new GcsItem(), Verbal = new GcsItem() }
            };

            ent.Consciousness.Eye.SetValue(gcsCtl.Eye);
            ent.Consciousness.Motor.SetValue(gcsCtl.Motor);
            ent.Consciousness.Verbal.SetValue(gcsCtl.Verbal);

            #region Telinga

            ent.Telinga.Daun = new AbNormalAndNotes { IsAbNormal = optDaun.SelectedIndex == 1, Notes = txtDaun.Text };
            ent.Telinga.Liang = new AbNormalAndNotes { IsAbNormal = optLiang.SelectedIndex == 1, Notes = txtLiang.Text };
            ent.Telinga.Discharge = new AbNormalAndNotes { IsAbNormal = optDischargeEARS.SelectedIndex == 1, Notes = txtDischargeEARS.Text };
            ent.Telinga.Serumen = new AbNormalAndNotes { IsAbNormal = optSerumen.SelectedIndex == 1, Notes = txtSerumen.Text };
            ent.Telinga.Tympani = new AbNormalAndNotes { IsAbNormal = optTympani.SelectedIndex == 1, Notes = txtTympani.Text };
            ent.Telinga.Tumor = new AbNormalAndNotes { IsAbNormal = optTumorEARS.SelectedIndex == 1, Notes = txtTumorEARS.Text };
            ent.Telinga.Mastoid = new AbNormalAndNotes { IsAbNormal = optMastoid.SelectedIndex == 1, Notes = txtMastoid.Text };
            ent.Telinga.PreAurikula = new AbNormalAndNotes { IsAbNormal = OptPreAurikula.SelectedIndex == 1, Notes = txtPreAurikula.Text };
            ent.Telinga.PostAurikula = new AbNormalAndNotes { IsAbNormal = optPostAurikula.SelectedIndex == 1, Notes = txtPostAurikula.Text };
            ent.Telinga.Hearing = new AbNormalAndNotes { IsAbNormal = optHearingTests.SelectedIndex == 1, Notes = txtHearingTests.Text };
            ent.Telinga.Audiometri = new AbNormalAndNotes { IsAbNormal = optAudiometri.SelectedIndex == 1, Notes = txtAudiometri.Text };
            ent.Telinga.Oae = new AbNormalAndNotes { IsAbNormal = optOAE.SelectedIndex == 1, Notes = txtOAE.Text };
            ent.Telinga.Keseimbangan = new AbNormalAndNotes { IsAbNormal = OptBalanceTests.SelectedIndex == 1, Notes = txtBalanceTests.Text };

            #endregion
            

            #region Hidung
            ent.Hidung.Luar = new AbNormalAndNotes { IsAbNormal = optExternalNose.SelectedIndex == 1, Notes = txtExternalNose.Text };
            ent.Hidung.Kavum = new AbNormalAndNotes { IsAbNormal = optKavumNasi.SelectedIndex == 1, Notes = txtKavumNasi.Text };
            ent.Hidung.Septum = new AbNormalAndNotes { IsAbNormal = optSeptum.SelectedIndex == 1, Notes = txtSeptum.Text };
            ent.Hidung.DischargeNOSE = new AbNormalAndNotes { IsAbNormal = optDischargeNOSE.SelectedIndex == 1, Notes = txtDischargeNOSE.Text };
            ent.Hidung.Mukosa = new AbNormalAndNotes { IsAbNormal = optMukosa.SelectedIndex == 1, Notes = txtMukosa.Text };
            ent.Hidung.TumorNOSE = new AbNormalAndNotes { IsAbNormal = optTumorNOSE.SelectedIndex == 1, Notes = txtTumorNOSE.Text };
            ent.Hidung.Konka = new AbNormalAndNotes { IsAbNormal = optKonka.SelectedIndex == 1, Notes = txtKonka.Text };
            ent.Hidung.Sinus = new AbNormalAndNotes { IsAbNormal = optSinus.SelectedIndex == 1, Notes = txtSinus.Text };
            ent.Hidung.Koana = new AbNormalAndNotes { IsAbNormal = optKoana.SelectedIndex == 1, Notes = txtKoana.Text };


            #endregion

            #region Tenggorok
            ent.Tenggorok.Rongga = new AbNormalAndNotes { IsAbNormal = optOcavity.SelectedIndex == 1, Notes = txtOcavity.Text };
            ent.Tenggorok.Tonsil = new AbNormalAndNotes { IsAbNormal = optTonsils.SelectedIndex == 1, Notes = txtTonsils.Text };
            ent.Tenggorok.Faring = new AbNormalAndNotes { IsAbNormal = optFaring.SelectedIndex == 1, Notes = txtFaring.Text };
            ent.Tenggorok.Mucosa = new AbNormalAndNotes { IsAbNormal = optMucosa.SelectedIndex == 1, Notes = txtMucosa.Text };
            ent.Tenggorok.Sound = new AbNormalAndNotes { IsAbNormal = optSound.SelectedIndex == 1, Notes = txtSound.Text };


            #endregion

            #region Larinx
            ent.Larinx.Epiglotis2 = new AbNormalAndNotes { IsAbNormal = optEpiglotis.SelectedIndex == 1, Notes = txtEpiglotis.Text };
            ent.Larinx.PVokal2 = new AbNormalAndNotes { IsAbNormal = optPVokalis.SelectedIndex == 1, Notes = txtPVokalis.Text };
            ent.Larinx.PVentri2 = new AbNormalAndNotes { IsAbNormal = optPVentrikulosis.SelectedIndex == 1, Notes = txtPVentrikulosis.Text };
            ent.Larinx.Aritenoid2 = new AbNormalAndNotes { IsAbNormal = optAritenoid.SelectedIndex == 1, Notes = txtAritenoid.Text };
            ent.Larinx.Rimaglotis2 = new AbNormalAndNotes { IsAbNormal = optRimaglotis.SelectedIndex == 1, Notes = txtRimaglotis.Text };
            ent.Larinx.Endoskopi2 = new AbNormalAndNotes { IsAbNormal = optEndoskopi.SelectedIndex == 1, Notes = txtEndoskopi.Text };

            #endregion

            ent.KepalaLeher2 = new AbNormalAndNotes { IsAbNormal = optHNeck.SelectedIndex == 1, Notes = txtHNeck.Text };
            ent.Trakea2 = new AbNormalAndNotes { IsAbNormal = optTrakea.SelectedIndex == 1, Notes = txtTrakea.Text };
            ent.Nlglands = new AbNormalAndNotes { IsAbNormal = optNlglands.SelectedIndex == 1, Notes = txtNlglands.Text };
            ent.Esofagus2 = new AbNormalAndNotes { IsAbNormal = optEsofagus.SelectedIndex == 1, Notes = txtEsofagus.Text };
            ent.Bronkus2 = new AbNormalAndNotes{ IsAbNormal = optBronkus.SelectedIndex == 1, Notes = txtBronkus.Text };

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
            //strBuilder.AppendFormat("Pain Scale: {0}", pe.Consciousness.PainScale);
            //strBuilder.AppendLine(string.Empty);
            strBuilder.AppendLine(pe.Consciousness.GetSoapObjective(pe.Condition));
            var isIncludeNormal = AppParameter.IsYes(AppParameter.ParameterItem.IsSoapFromPysicalExamIncludeNormalValue);

            #region Telinga
            if (isIncludeNormal || pe.Telinga.Daun.IsAbNormal)
            {
                strBuilder.AppendFormat("Daun: {1}: {0}", pe.Telinga.Daun.Notes, pe.Telinga.Daun.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Telinga.Liang.IsAbNormal)
            {
                strBuilder.AppendFormat("Liang: {1}: {0}", pe.Telinga.Liang.Notes, pe.Telinga.Liang.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Telinga.Discharge.IsAbNormal)
            {
                strBuilder.AppendFormat("Discharge: {1}: {0}", pe.Telinga.Discharge.Notes, pe.Telinga.Discharge.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }


            if (isIncludeNormal || pe.Telinga.Serumen.IsAbNormal)
            {
                strBuilder.AppendFormat("Serumen: {1}: {0}", pe.Telinga.Serumen.Notes, pe.Telinga.Serumen.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Telinga.Tympani.IsAbNormal)
            {
                strBuilder.AppendFormat("Tympani: {1}: {0}", pe.Telinga.Tympani.Notes, pe.Telinga.Tympani.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Telinga.Tumor.IsAbNormal)
            {
                strBuilder.AppendFormat("Tumor: {1}: {0}", pe.Telinga.Tumor.Notes, pe.Telinga.Tumor.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Telinga.Mastoid.IsAbNormal)
            {
                strBuilder.AppendFormat("Mastoid: {1}: {0}", pe.Telinga.Mastoid.Notes, pe.Telinga.Mastoid.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Telinga.PreAurikula.IsAbNormal)
            {
                strBuilder.AppendFormat("PreAurikula: {1}: {0}", pe.Telinga.PreAurikula.Notes, pe.Telinga.PreAurikula.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Telinga.PostAurikula.IsAbNormal)
            {
                strBuilder.AppendFormat("PostAurikula: {1}: {0}", pe.Telinga.PostAurikula.Notes, pe.Telinga.PostAurikula.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Telinga.Hearing.IsAbNormal)
            {
                strBuilder.AppendFormat("Hearing: {1}: {0}", pe.Telinga.Hearing.Notes, pe.Telinga.Hearing.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }


            if (isIncludeNormal || pe.Telinga.Audiometri.IsAbNormal)
            {
                strBuilder.AppendFormat("Audiometri: {1}: {0}", pe.Telinga.Audiometri.Notes, pe.Telinga.Audiometri.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Telinga.Oae.IsAbNormal)
            {
                strBuilder.AppendFormat("Oae: {1}: {0}", pe.Telinga.Oae.Notes, pe.Telinga.Oae.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Telinga.Keseimbangan.IsAbNormal)
            {
                strBuilder.AppendFormat("Keseimbangan: {1}: {0}", pe.Telinga.Keseimbangan.Notes, pe.Telinga.Keseimbangan.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }


            #endregion

            #region Hidung
            if (isIncludeNormal || pe.Hidung.Luar.IsAbNormal)
            {
                strBuilder.AppendFormat("Luar: {1}: {0}", pe.Hidung.Luar.Notes, pe.Hidung.Luar.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Hidung.Kavum.IsAbNormal)
            {
                strBuilder.AppendFormat("Kavum: {1}: {0}", pe.Hidung.Kavum.Notes, pe.Hidung.Kavum.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Hidung.Septum.IsAbNormal)
            {
                strBuilder.AppendFormat("Septum: {1}: {0}", pe.Hidung.Septum.Notes, pe.Hidung.Septum.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Hidung.DischargeNOSE.IsAbNormal)
            {
                strBuilder.AppendFormat("DischargeNOSE: {1}: {0}", pe.Hidung.DischargeNOSE.Notes, pe.Hidung.DischargeNOSE.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Hidung.Mukosa.IsAbNormal)
            {
                strBuilder.AppendFormat("Mukosa: {1}: {0}", pe.Hidung.Mukosa.Notes, pe.Hidung.Mukosa.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Hidung.TumorNOSE.IsAbNormal)
            {
                strBuilder.AppendFormat("TumorNOSE: {1}: {0}", pe.Hidung.TumorNOSE.Notes, pe.Hidung.TumorNOSE.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Hidung.Konka.IsAbNormal)
            {
                strBuilder.AppendFormat("Konka: {1}: {0}", pe.Hidung.Konka.Notes, pe.Hidung.Konka.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }


            if (isIncludeNormal || pe.Hidung.Sinus.IsAbNormal)
            {
                strBuilder.AppendFormat("Sinus: {1}: {0}", pe.Hidung.Sinus.Notes, pe.Hidung.Sinus.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Hidung.Koana.IsAbNormal)
            {
                strBuilder.AppendFormat("Koana: {1}: {0}", pe.Hidung.Koana.Notes, pe.Hidung.Koana.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            #endregion

            #region Tenggorok
            if (isIncludeNormal || pe.Tenggorok.Rongga.IsAbNormal)
            {
                strBuilder.AppendFormat("Rongga: {1}: {0}", pe.Tenggorok.Rongga.Notes, pe.Tenggorok.Rongga.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Tenggorok.Tonsil.IsAbNormal)
            {
                strBuilder.AppendFormat("Tonsil: {1}: {0}", pe.Tenggorok.Tonsil.Notes, pe.Tenggorok.Tonsil.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Tenggorok.Faring.IsAbNormal)
            {
                strBuilder.AppendFormat("Faring: {1}: {0}", pe.Tenggorok.Faring.Notes, pe.Tenggorok.Faring.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Tenggorok.Mucosa.IsAbNormal)
            {
                strBuilder.AppendFormat("Mucosa: {1}: {0}", pe.Tenggorok.Mucosa.Notes, pe.Tenggorok.Mucosa.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Tenggorok.Mucosa.IsAbNormal)
            {
                strBuilder.AppendFormat("Mucosa: {1}: {0}", pe.Tenggorok.Mucosa.Notes, pe.Tenggorok.Mucosa.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Tenggorok.Sound.IsAbNormal)
            {
                strBuilder.AppendFormat("Sound: {1}: {0}", pe.Tenggorok.Sound.Notes, pe.Tenggorok.Sound.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }



            #endregion

            #region Larinx
            if (isIncludeNormal || pe.Larinx.Epiglotis2.IsAbNormal)
            {
                strBuilder.AppendFormat("Epiglotis: {1}: {0}", pe.Larinx.Epiglotis2.Notes, pe.Larinx.Epiglotis2.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Larinx.PVokal2.IsAbNormal)
            {
                strBuilder.AppendFormat("PVokal: {1}: {0}", pe.Larinx.PVokal2.Notes, pe.Larinx.PVokal2.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Larinx.PVentri2.IsAbNormal)
            {
                strBuilder.AppendFormat("PVentri: {1}: {0}", pe.Larinx.PVentri2.Notes, pe.Larinx.PVentri2.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Larinx.Aritenoid2.IsAbNormal)
            {
                strBuilder.AppendFormat("Aritenoid: {1}: {0}", pe.Larinx.Aritenoid2.Notes, pe.Larinx.Aritenoid2.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Larinx.Rimaglotis2.IsAbNormal)
            {
                strBuilder.AppendFormat("Rimaglotis: {1}: {0}", pe.Larinx.Rimaglotis2.Notes, pe.Larinx.Rimaglotis2.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Larinx.Endoskopi2.IsAbNormal)
            {
                strBuilder.AppendFormat("Endoskopi: {1}: {0}", pe.Larinx.Endoskopi2.Notes, pe.Larinx.Endoskopi2.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            #endregion

            if (isIncludeNormal || pe.KepalaLeher2.IsAbNormal)
            {
                strBuilder.AppendFormat("KepalaLeher: {1}: {0}", pe.KepalaLeher2.Notes, pe.KepalaLeher2.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Trakea2.IsAbNormal)
            {
                strBuilder.AppendFormat("Trakea: {1}: {0}", pe.Trakea2.Notes, pe.Trakea2.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Nlglands.IsAbNormal)
            {
                strBuilder.AppendFormat("Nlglands: {1}: {0}", pe.Nlglands.Notes, pe.Nlglands.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }


            if (isIncludeNormal || pe.Esofagus2.IsAbNormal)
            {
                strBuilder.AppendFormat("Esofagus: {1}: {0}", pe.Esofagus2.Notes, pe.Esofagus2.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Bronkus2.IsAbNormal)
            {
                strBuilder.AppendFormat("Bronkus: {1}: {0}", pe.Bronkus2.Notes, pe.Bronkus2.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (!string.IsNullOrEmpty(pe.Notes))
            {
                strBuilder.AppendFormat("{0}", pe.Notes);
                strBuilder.AppendLine(string.Empty);
            }

            return strBuilder.ToString();
            #endregion


        }
    }
}