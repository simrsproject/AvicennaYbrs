using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Newtonsoft.Json;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{

    /// <summary>
    /// Entry control untuk asesmen Neorolgy
    /// Ditambah dari GeneralPeCtl dari Head sampai Other notes
    /// </summary>
    /// Create By: Fajri
    /// Create Date: 2023-March-21
    /// Client Req: RSYS
    /// ----------------------------------------------------
    public partial class NeurologyPeV3Ctl : BaseAssessmentCtl
    {
        public override EntryGroupEnum EntryGroup
        {
            get { return EntryGroupEnum.PhysicalExam; }
        }

        public override ColumnEnum Column
        {
            get { return ColumnEnum.Left; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            var CranialisStdRefId = AppParameter.GetParameterValue(AppParameter.ParameterItem.CranialisStdRefId);
            if (string.IsNullOrWhiteSpace(CranialisStdRefId))
                StandardReference.InitializeIncludeSpace(ddlCranialis, AppEnum.StandardReference.NervusCranialis, true);
            else
                StandardReference.InitializeIncludeSpace(ddlCranialis, CranialisStdRefId, true);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {

            var ent = new NeurologiPe();

            // Get Education
            var asses = assessment;
            if (!string.IsNullOrEmpty(asses.PhysicalExam))
            {
                // Convert to class w json
                try
                {
                    ent = JsonConvert.DeserializeObject<NeurologiPe>(asses.PhysicalExam);

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

            // Pyscal Exam
            var pExam = ent;
            optKepala.SelectedIndex = pExam.Kepala.IsAbNormal ? 1 : 0;
            txtKepala.Text = pExam.Kepala.Notes;

            optMata.SelectedIndex = pExam.Mata.IsAbNormal ? 1 : 0;
            txtMata.Text = pExam.Mata.Notes;

            optColorBlind.SelectedIndex = pExam.ColorBlind.IsExist ? 1 : 0;
            txtColorBlind.Text = pExam.ColorBlind.Notes;

            txtVisus.Text = pExam.Visus;

            optTht.SelectedIndex = pExam.Tht.IsAbNormal ? 1 : 0;
            txtTht.Text = pExam.Tht.Notes;

            optMulut.SelectedIndex = pExam.Mulut.IsAbNormal ? 1 : 0;
            txtMulut.Text = pExam.Mulut.Notes;

            optLeher.SelectedIndex = pExam.Leher.IsAbNormal ? 1 : 0;
            txtLeher.Text = pExam.Leher.Notes;

            optThorax.SelectedIndex = pExam.Thorax.IsAbNormal ? 1 : 0;
            txtThorax.Text = pExam.Thorax.Notes;

            optJantung.SelectedIndex = pExam.Jantung.IsAbNormal ? 1 : 0;
            txtJantung.Text = pExam.Jantung.Notes;

            optParu.SelectedIndex = pExam.Paru.IsAbNormal ? 1 : 0;
            txtParu.Text = pExam.Paru.Notes;

            optAbdomen.SelectedIndex = pExam.Abdomen.IsAbNormal ? 1 : 0;
            txtAbdomen.Text = pExam.Abdomen.Notes;

            optHepar.SelectedIndex = pExam.Hepar.IsAbNormal ? 1 : 0;
            txtHepar.Text = pExam.Hepar.Notes;

            optLien.SelectedIndex = pExam.Lien.IsAbNormal ? 1 : 0;
            txtLien.Text = pExam.Lien.Notes;

            optReflexFis.SelectedIndex = pExam.ReflexFis.IsAbNormal ? 1 : 0;
            txtReflexFis.Text = pExam.ReflexFis.Notes;

            optReflexPat.SelectedIndex = pExam.ReflexPat.IsAbNormal ? 1 : 0;
            txtReflexPat.Text = pExam.ReflexPat.Notes;

            optTumor.SelectedIndex = pExam.IsTumor ? 0 : 1;
            optHernia.SelectedIndex = pExam.IsHernia ? 0 : 1;
            optHemorrhoids.SelectedIndex = pExam.IsHemorrhoids ? 0 : 1;

            optAuskulatasi.SelectedIndex = pExam.Auskulatasi.IsAbNormal ? 1 : 0;
            txtAuskulatasi.Text = pExam.Auskulatasi.Notes;

            optGenitaliaAndAnus.SelectedIndex = pExam.GenitaliaAndAnus.IsAbNormal ? 1 : 0;
            txtGenitaliaAndAnus.Text = pExam.GenitaliaAndAnus.Notes;

            optEkstremitas.SelectedIndex = pExam.Ekstremitas.IsAbNormal ? 1 : 0;
            txtEkstremitas.Text = pExam.Ekstremitas.Notes;

            optKulit.SelectedIndex = pExam.Kulit.IsAbNormal ? 1 : 0;
            txtKulit.Text = pExam.Kulit.Notes;

            txtOtherNotes.Text = pExam.OtherNotes;

            //Gcs
            gcsCtl.Condition = ent.Condition;
            gcsCtl.Gcs = ent.Consciousness;

            optNuchalRigidity.SelectedValue = ent.Meningeal.KakuKuduk == true ? "1" : "0";
            optKernig.SelectedValue = ent.Meningeal.Kernig == true ? "1" : "0";
            optLasgque.SelectedValue = ent.Meningeal.Lasgque == true ? "1" : "0";
            optPapiledema.SelectedValue = ent.Funduscopy.Papiledema == true ? "1" : "0";
            ddlCranialis.SelectedText = ent.Cranialis == null ? string.Empty : ent.Cranialis.ToString();
            
            // Left
            optExtermintasSuperior.SelectedValue = ent.Motorik == null || ent.Motorik.Superior == null ? string.Empty : ent.Motorik.Superior.ToString();
            optExtermintasInterior.SelectedValue = ent.Motorik == null || ent.Motorik.Interior == null ? string.Empty : ent.Motorik.Interior.ToString();
            
            // Right
            optExtermintasSuperiorR.SelectedValue = ent.MotorikRight == null || ent.MotorikRight.Superior == null ? string.Empty : ent.MotorikRight.Superior.ToString();
            optExtermintasInteriorR.SelectedValue = ent.MotorikRight == null || ent.MotorikRight.Interior == null ? string.Empty : ent.MotorikRight.Interior.ToString();

            txtPupilLeft.Text = ent.Pupils == null || ent.Pupils.PupilLeft == null ? string.Empty : ent.Pupils.PupilLeft;
            txtPupilRight.Text = ent.Pupils == null || ent.Pupils.PupilRight == null ? string.Empty : ent.Pupils.PupilRight;

            txtPupilReflexLeft.Text = ent.PupilRefleks == null || ent.PupilRefleks.PupilRefleksLeft == null ? string.Empty : ent.PupilRefleks.PupilRefleksLeft;
            txtPupilReflexRight.Text = ent.PupilRefleks == null || ent.PupilRefleks.PupilRefleksRight == null ? string.Empty : ent.PupilRefleks.PupilRefleksRight;

            txtRefleksFisiologis.Text = ent.Refleks == null || ent.Refleks.Fisiologis == null ? string.Empty : ent.Refleks.Fisiologis;
            txtRefleksPatologis.Text = ent.Refleks == null || ent.Refleks.Patologis == null ? string.Empty : ent.Refleks.Patologis;
            txtStatusOtonom.Text = ent.StatOtonom;
            txtPhysicalExamNotes.Text = ent.Notes;
            txtNeurologis.Text = ent.Neurologis;
        }


        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var pExam = new NeurologiPe
            {
                Kepala = new AbNormalAndNotes { IsAbNormal = optKepala.SelectedIndex == 1, Notes = txtKepala.Text },
                Mata = new AbNormalAndNotes { IsAbNormal = optMata.SelectedIndex == 1, Notes = txtMata.Text },
                Tht = new AbNormalAndNotes { IsAbNormal = optTht.SelectedIndex == 1, Notes = txtTht.Text },
                Mulut = new AbNormalAndNotes { IsAbNormal = optMulut.SelectedIndex == 1, Notes = txtMulut.Text },
                Leher = new AbNormalAndNotes { IsAbNormal = optLeher.SelectedIndex == 1, Notes = txtLeher.Text },
                Thorax = new AbNormalAndNotes { IsAbNormal = optThorax.SelectedIndex == 1, Notes = txtThorax.Text },
                Jantung = new AbNormalAndNotes { IsAbNormal = optJantung.SelectedIndex == 1, Notes = txtJantung.Text },
                Paru = new AbNormalAndNotes { IsAbNormal = optParu.SelectedIndex == 1, Notes = txtParu.Text },
                Abdomen = new AbNormalAndNotes { IsAbNormal = optAbdomen.SelectedIndex == 1, Notes = txtAbdomen.Text },
                Auskulatasi = new AbNormalAndNotes { IsAbNormal = optAuskulatasi.SelectedIndex == 1, Notes = txtAuskulatasi.Text },
                ColorBlind = new ExistAndNotes() { IsExist = optColorBlind.SelectedIndex == 1, Notes = txtColorBlind.Text },
                Hepar = new AbNormalAndNotes { IsAbNormal = optHepar.SelectedIndex == 1, Notes = txtHepar.Text },
                Lien = new AbNormalAndNotes { IsAbNormal = optLien.SelectedIndex == 1, Notes = txtLien.Text },
                ReflexFis = new AbNormalAndNotes { IsAbNormal = optReflexFis.SelectedIndex == 1, Notes = txtReflexFis.Text },
                ReflexPat = new AbNormalAndNotes { IsAbNormal = optReflexPat.SelectedIndex == 1, Notes = txtReflexPat.Text },
                Visus = txtVisus.Text,
                IsTumor = optTumor.SelectedIndex == 0,
                IsHernia = optHernia.SelectedIndex == 0,
                IsHemorrhoids = optHemorrhoids.SelectedIndex == 0,
                GenitaliaAndAnus = new AbNormalAndNotes
                {
                    IsAbNormal = optGenitaliaAndAnus.SelectedIndex == 1,
                    Notes = txtGenitaliaAndAnus.Text
                },
                Ekstremitas = new AbNormalAndNotes
                {
                    IsAbNormal = optEkstremitas.SelectedIndex == 1,
                    Notes = txtEkstremitas.Text
                },
                Kulit = new AbNormalAndNotes { IsAbNormal = optKulit.SelectedIndex == 1, Notes = txtKulit.Text },
                OtherNotes = txtOtherNotes.Text,
                Neurologis = txtNeurologis.Text,
                Condition = gcsCtl.Condition,
                Consciousness = gcsCtl.Gcs,
                Notes = txtPhysicalExamNotes.Text,
                StatOtonom = txtStatusOtonom.Text
             };
            pExam.Meningeal = new Meningeal { KakuKuduk = optNuchalRigidity.SelectedValue == "1", Kernig = optKernig.SelectedValue == "1", Lasgque = optLasgque.SelectedValue == "1" };
            pExam.Funduscopy = new Funduscopy { Papiledema = optPapiledema.SelectedValue == "1" };
            pExam.Cranialis = ddlCranialis.SelectedText;

            // Left
            pExam.Motorik.Superior = optExtermintasSuperior.SelectedValue.ToInt();
            pExam.Motorik.Interior = optExtermintasInterior.SelectedValue.ToInt();

            // Right
            pExam.MotorikRight.Superior = optExtermintasSuperiorR.SelectedValue.ToInt();
            pExam.MotorikRight.Interior = optExtermintasInteriorR.SelectedValue.ToInt();

            pExam.Pupils.PupilLeft = txtPupilLeft.Text;
            pExam.Pupils.PupilRight = txtPupilRight.Text;

            pExam.PupilRefleks.PupilRefleksLeft = txtPupilReflexLeft.Text;
            pExam.PupilRefleks.PupilRefleksRight = txtPupilReflexRight.Text;

            pExam.Refleks.Fisiologis = txtRefleksFisiologis.Text;
            pExam.Refleks.Patologis = txtRefleksPatologis.Text;


            assessment.PhysicalExam = JsonConvert.SerializeObject(pExam);

            // Objective
            rim.Info2 = GenerateSoapObjective(pExam);
        }

        private string GenerateSoapObjective(NeurologiPe pe)
        {
            var strBuilder = new StringBuilder();

            var isIncludeNormal = AppParameter.IsYes(AppParameter.ParameterItem.IsSoapFromPysicalExamIncludeNormalValue);

            if (isIncludeNormal || pe.Kepala.IsAbNormal)
            {
                strBuilder.AppendFormat("Kepala: {1}: {0}", pe.Kepala.Notes, pe.Kepala.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Mata.IsAbNormal)
            {
                strBuilder.AppendFormat("Mata: {1}: {0}", pe.Mata.Notes, pe.Mata.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (pe.ColorBlind.IsExist)
            {
                strBuilder.AppendFormat("Buta Warna: Ya: {0}", pe.ColorBlind.Notes);
                strBuilder.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(pe.Visus))
            {
                strBuilder.AppendFormat("Visus: {0}", pe.Visus);
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Tht.IsAbNormal)
            {
                strBuilder.AppendFormat("THT: {1}: {0}", pe.Tht.Notes, pe.Tht.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Mulut.IsAbNormal)
            {
                strBuilder.AppendFormat("Mulut: {1}: {0}", pe.Mulut.Notes, pe.Mulut.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Leher.IsAbNormal)
            {
                strBuilder.AppendFormat("Leher: {1}: {0}", pe.Leher.Notes, pe.Leher.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Thorax.IsAbNormal)
            {
                strBuilder.AppendFormat("Thorax: {1}: {0}", pe.Thorax.Notes, pe.Thorax.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Jantung.IsAbNormal)
            {
                strBuilder.AppendFormat("Jantung: {1}: {0}", pe.Jantung.Notes, pe.Jantung.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Paru.IsAbNormal)
            {
                strBuilder.AppendFormat("Paru: {1}: {0}", pe.Paru.Notes, pe.Paru.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Abdomen.IsAbNormal)
            {
                strBuilder.AppendFormat("Abdomen: {1}: {0}", pe.Abdomen.Notes, pe.Abdomen.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Hepar.IsAbNormal)
            {
                strBuilder.AppendFormat("Hepar: {1}: {0}", pe.Hepar.Notes, pe.Hepar.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Lien.IsAbNormal)
            {
                strBuilder.AppendFormat("Lien: {1}: {0}", pe.Lien.Notes, pe.Lien.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.ReflexFis.IsAbNormal)
            {
                strBuilder.AppendFormat("Reflex Fisiologis: {1}: {0}", pe.ReflexFis.Notes, pe.ReflexFis.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.ReflexPat.IsAbNormal)
            {
                strBuilder.AppendFormat("Reflex Patologis: {1}: {0}", pe.ReflexPat.Notes, pe.ReflexPat.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (pe.IsTumor == true)
            {
                strBuilder.AppendFormat("Tumor: Ya");
                strBuilder.AppendLine(string.Empty);
            }
            if (pe.IsHernia == true)
            {
                strBuilder.AppendFormat("Hernia: Ya");
                strBuilder.AppendLine(string.Empty);
            }
            if (pe.IsHemorrhoids == true)
            {
                strBuilder.AppendFormat("Hemorrhoids: Ya");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Auskulatasi.IsAbNormal)
            {
                strBuilder.AppendFormat("Auskulatasi: {1}: {0}", pe.Auskulatasi.Notes, pe.Auskulatasi.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.GenitaliaAndAnus.IsAbNormal)
            {
                strBuilder.AppendFormat("Genitalia & Anus: {1}: {0}", pe.GenitaliaAndAnus.Notes, pe.GenitaliaAndAnus.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Ekstremitas.IsAbNormal)
            {
                strBuilder.AppendFormat("Ekstremitas: {1}: {0}", pe.Ekstremitas.Notes, pe.Ekstremitas.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Kulit.IsAbNormal)
            {
                strBuilder.AppendFormat("Kulit: {1}: {0}", pe.Kulit.Notes, pe.Kulit.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(pe.OtherNotes))
            {
                strBuilder.AppendFormat("Catatan Lainnya: {0}", pe.OtherNotes);
                strBuilder.AppendLine(string.Empty);
            }

            //Gcs
            strBuilder.AppendLine(pe.Consciousness.GetSoapObjective(pe.Condition));

            if (pe.Funduscopy.Papiledema == true)
            {
                strBuilder.AppendFormat("Papiledema: Ya");
                strBuilder.AppendLine(string.Empty);
            }
            if (pe.Meningeal.KakuKuduk == true)
                strBuilder.AppendFormat("Stimulus Sign Meningeal: Nuchal Rigidity: Ya, ");
            if (pe.Meningeal.Kernig == true)
                strBuilder.AppendFormat("Kernig: Ya, ");
            if (pe.Meningeal.Lasgque == true)
            {
                strBuilder.AppendFormat("Lasgque: Ya");
                strBuilder.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(pe.Cranialis))
            {
                strBuilder.AppendFormat("Cranialis: {0}", pe.Cranialis);
                strBuilder.AppendLine(string.Empty);
            }
            strBuilder.AppendFormat("Extermintas Superior: Left: {0}, Right: {1}", pe.Motorik.Superior, pe.MotorikRight.Superior);
            strBuilder.AppendLine(string.Empty);
            strBuilder.AppendFormat("Extermintas Interior: Left: {0}, Right: {1}", pe.Motorik.Interior, pe.MotorikRight.Interior);
            strBuilder.AppendLine(string.Empty);
            if (!string.IsNullOrEmpty(pe.Pupils.PupilLeft) || !string.IsNullOrEmpty(pe.Pupils.PupilRight))
            {
                strBuilder.AppendFormat("Pupil Kiri: {0}, Pupil Kanan {1}", pe.Pupils.PupilLeft, pe.Pupils.PupilRight);
                strBuilder.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(pe.PupilRefleks.PupilRefleksLeft) || !string.IsNullOrEmpty(pe.PupilRefleks.PupilRefleksRight))
            {
                strBuilder.AppendFormat("Pupil Refleks Kiri: {0}, Pupil Refleks Kanan: {1}", pe.PupilRefleks.PupilRefleksLeft, pe.PupilRefleks.PupilRefleksRight);
                strBuilder.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(pe.Refleks.Fisiologis))
            {
                strBuilder.AppendFormat("Refleks Fisiologis: {0}", pe.Refleks.Fisiologis);
                strBuilder.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(pe.Refleks.Patologis))
            {
                strBuilder.AppendFormat("Refleks Patologis: {0}", pe.Refleks.Patologis);
                strBuilder.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(pe.StatOtonom))
            {
                strBuilder.AppendFormat("Status Otonom: {0}", pe.StatOtonom);
                strBuilder.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(pe.Neurologis))
            {
                strBuilder.AppendFormat("Neurologi: {0}", pe.Neurologis);
                strBuilder.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(pe.Notes))
            {
                strBuilder.AppendFormat("Pemeriksaan Fisik: {0}", pe.Notes);
                strBuilder.AppendLine(string.Empty);
            }
            return strBuilder.ToString();
        }

        #endregion
    }
}