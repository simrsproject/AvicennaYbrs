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
    public partial class NeurologyPeV2Ctl : BaseAssessmentCtl
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
            //if (string.IsNullOrWhiteSpace(CranialisStdRefId))
            //    StandardReference.InitializeIncludeSpace(ddlCranialis, AppEnum.StandardReference.NervusCranialis, true);
            //else
            //    StandardReference.InitializeIncludeSpace(ddlCranialis, CranialisStdRefId, true);
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



            gcsCtl.Condition = ent.Condition;
            gcsCtl.Gcs = ent.Consciousness;
            optNuchalRigidity.SelectedValue = ent.Meningeal.KakuKuduk == true ? "1" : "0";
            optKernig.SelectedValue = ent.Meningeal.Kernig == true ? "1" : "0";
            optLasgque.SelectedValue = ent.Meningeal.Lasgque == true ? "1" : "0";
            optPapiledema.SelectedValue = ent.Funduscopy.Papiledema == true ? "1" : "0";
            //ddlCranialis.SelectedText = (ent == null ||  ent.Cranialis == null ? string.Empty : ent.Cranialis.ToString());
            //optNervus.SelectedIndex = ent.Nervus.IsAbNormal ? 1 : 0;
            //txtNervus.Text = ent.Nervus.Notes;
            optNervusOlfaktoris.SelectedIndex = ent.NervusOlfaktoris.IsAbNormal.Value ? 1 : 0;
            txtNervusOlfaktoris.Text = ent.NervusOlfaktoris.Notes;
            optNervusOptikus.SelectedIndex = ent.NervusOptikus.IsAbNormal.Value ? 1 : 0;
            txtNervusOptikus.Text = ent.NervusOptikus.Notes;
            optNervusOkulomotoris.SelectedIndex = ent.NervusOkulomotoris.IsAbNormal.Value ? 1 : 0;
            txtNervusOkulomotoris.Text = ent.NervusOkulomotoris.Notes;
            optNervusTroklear.SelectedIndex = ent.NervusTroklear.IsAbNormal.Value ? 1 : 0;
            txtNervusTroklear.Text = ent.NervusTroklear.Notes;
            optNervusTrigeminus.SelectedIndex = ent.NervusTrigeminus.IsAbNormal.Value ? 1 : 0;
            txtNervusTrigeminus.Text = ent.NervusTrigeminus.Notes;
            optNervusAbducens.SelectedIndex = ent.NervusAbducens.IsAbNormal.Value ? 1 : 0;
            txtNervusAbducens.Text = ent.NervusAbducens.Notes;
            optNervusFasialis.SelectedIndex = ent.NervusFasialis.IsAbNormal.Value ? 1 : 0;
            txtNervusFasialis.Text = ent.NervusFasialis.Notes;
            optNervusVestibukokhlearis.SelectedIndex = ent.NervusVestibukokhlearis.IsAbNormal.Value ? 1 : 0;
            txtNervusVestibukokhlearis.Text = ent.NervusVestibukokhlearis.Notes;
            optNervusGlossofaringeal.SelectedIndex = ent.NervusGlossofaringeal.IsAbNormal.Value ? 1 : 0;
            txtNervusGlossofaringeal.Text = ent.NervusGlossofaringeal.Notes;
            optNervusVagus.SelectedIndex = ent.NervusVagus.IsAbNormal.Value ? 1 : 0;
            txtNervusVagus.Text = ent.NervusVagus.Notes;
            optNervusAsesoris.SelectedIndex = ent.NervusAsesoris.IsAbNormal.Value ? 1 : 0;
            txtNervusAsesoris.Text = ent.NervusAsesoris.Notes;
            optNervusHipoglossus.SelectedIndex = ent.NervusHipoglossus.IsAbNormal.Value ? 1 : 0;
            txtNervusHipoglossus.Text = ent.NervusHipoglossus.Notes;
            //optExtermintasSuperior.SelectedValue = (ent == null || ent.Motorik == null || ent.Motorik.Superior == null ? string.Empty : ent.Motorik.Superior.ToString());
            //optExtermintasInterior.SelectedValue = (ent == null || ent.Motorik == null || ent.Motorik.Interior == null ? string.Empty : ent.Motorik.Interior.ToString());
            txtRefleksFisiologis.Text = (ent == null || ent.Refleks == null || ent.Refleks.Fisiologis == null ? string.Empty : ent.Refleks.Fisiologis.ToString());
            txtRefleksPatologis.Text = (ent == null || ent.Refleks == null || ent.Refleks.Patologis == null ? string.Empty : ent.Refleks.Patologis.ToString());
            txtStatusOtonom.Text = ent.StatOtonom;
            txtPhysicalExamNotes.Text = ent.Notes;
            txtNeurologis.Text = ent.Neurologis;

            var left = ent.Left;
            optExtermintasSuperior.SelectedValue = (ent == null || ent.Left.Superior == null || ent.Left.Superior == null ? string.Empty : ent.Left.Superior.ToString());
            optExtermintasInterior.SelectedValue = (ent == null || ent.Left.Interior == null || ent.Left.Interior == null ? string.Empty : ent.Left.Interior.ToString());





            var right = ent.Right;
            optExtermintasSuperior2.SelectedValue = (ent == null || ent.Right.Superior == null || ent.Right.Superior == null ? string.Empty : ent.Right.Superior.ToString());
            optExtermintasInterior2.SelectedValue = (ent == null || ent.Right.Interior == null || ent.Right.Interior == null ? string.Empty : ent.Right.Interior.ToString());


        }


        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {

            var pExam = new NeurologiPe
            {
                Condition = gcsCtl.Condition,
                Consciousness = gcsCtl.Gcs,
                Neurologis = txtNeurologis.Text,
                Notes = txtPhysicalExamNotes.Text,
                StatOtonom = txtStatusOtonom.Text,
                NervusOlfaktoris = new AbNormalAndNotes2 { IsAbNormal = optNervusOlfaktoris.SelectedIndex == 1, Notes = txtNervusOlfaktoris.Text },
                NervusOptikus = new AbNormalAndNotes2 { IsAbNormal = optNervusOptikus.SelectedIndex == 1, Notes = txtNervusOptikus.Text },
                NervusOkulomotoris = new AbNormalAndNotes2 { IsAbNormal = optNervusOkulomotoris.SelectedIndex == 1, Notes = txtNervusOkulomotoris.Text },
                NervusTroklear = new AbNormalAndNotes2 { IsAbNormal = optNervusTroklear.SelectedIndex == 1, Notes = txtNervusTroklear.Text },
                NervusTrigeminus = new AbNormalAndNotes2 { IsAbNormal = optNervusTrigeminus.SelectedIndex == 1, Notes = txtNervusTrigeminus.Text },
                NervusAbducens = new AbNormalAndNotes2 { IsAbNormal = optNervusAbducens.SelectedIndex == 1, Notes = txtNervusAbducens.Text },
                NervusFasialis = new AbNormalAndNotes2 { IsAbNormal = optNervusFasialis.SelectedIndex == 1, Notes = txtNervusFasialis.Text },
                NervusVestibukokhlearis = new AbNormalAndNotes2 { IsAbNormal = optNervusVestibukokhlearis.SelectedIndex == 1, Notes = txtNervusVestibukokhlearis.Text },
                NervusGlossofaringeal = new AbNormalAndNotes2 { IsAbNormal = optNervusGlossofaringeal.SelectedIndex == 1, Notes = txtNervusGlossofaringeal.Text },
                NervusVagus = new AbNormalAndNotes2 { IsAbNormal = optNervusVagus.SelectedIndex == 1, Notes = txtNervusVagus.Text },
                NervusAsesoris = new AbNormalAndNotes2 { IsAbNormal = optNervusAsesoris.SelectedIndex == 1, Notes = txtNervusAsesoris.Text },
                NervusHipoglossus = new AbNormalAndNotes2 { IsAbNormal = optNervusHipoglossus.SelectedIndex == 1, Notes = txtNervusHipoglossus.Text },
                Left = new StatusMotorikTest
                {
                    Superior = optExtermintasSuperior.SelectedValue.ToInt(),
                    Interior = optExtermintasInterior.SelectedValue.ToInt()
                },
                Right = new StatusMotorikTest
                {
                    Superior = optExtermintasSuperior2.SelectedValue.ToInt(),
                    Interior = optExtermintasInterior2.SelectedValue.ToInt()
                }


            };
            pExam.Meningeal = new Meningeal { KakuKuduk = optNuchalRigidity.SelectedValue == "1", Kernig = optKernig.SelectedValue == "1", Lasgque = optLasgque.SelectedValue == "1" };
            pExam.Funduscopy = new Funduscopy { Papiledema = optPapiledema.SelectedValue == "1" };
            //pExam.Cranialis = ddlCranialis.SelectedText.ToString();
            //pExam.Motorik.Superior = optExtermintasSuperior.SelectedValue.ToInt();
            //pExam.Motorik.Interior = optExtermintasInterior.SelectedValue.ToInt();
            pExam.Refleks.Fisiologis = txtRefleksFisiologis.Text;
            pExam.Refleks.Patologis = txtRefleksPatologis.Text;


            assessment.PhysicalExam = JsonConvert.SerializeObject(pExam);

            // Objective
            rim.Info2 = GenerateSoapObjective(pExam);
        }

        private string GenerateSoapObjective(NeurologiPe pe)
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
            //    strBuilder.AppendFormat("Skala Nyeri: {0}", pe.Consciousness.PainScale);
            //    strBuilder.AppendLine(string.Empty);
            //}

            strBuilder.AppendLine(pe.Consciousness.GetSoapObjective(pe.Condition));

            var isIncludeNormal = AppParameter.IsYes(AppParameter.ParameterItem.IsSoapFromPysicalExamIncludeNormalValue);

            if (isIncludeNormal || pe.NervusOlfaktoris.IsAbNormal.Value)
            {
                strBuilder.AppendFormat("Nervus Olfaktoris / Olfactory: {1}: {0}", pe.NervusOlfaktoris.Notes, pe.NervusOlfaktoris.IsAbNormal.Value ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.NervusOptikus.IsAbNormal.Value)
            {
                strBuilder.AppendFormat("Nervus Optikus / Optic: {1}: {0}", pe.NervusOptikus.Notes, pe.NervusOptikus.IsAbNormal.Value ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.NervusOkulomotoris.IsAbNormal.Value)
            {
                strBuilder.AppendFormat("Nervus Okulomotoris / Oculomotor: {1}: {0}", pe.NervusOkulomotoris.Notes, pe.NervusOkulomotoris.IsAbNormal.Value ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }    
            if (isIncludeNormal || pe.NervusTroklear.IsAbNormal.Value)
            {
                strBuilder.AppendFormat("Nervus Troklear / Trochlear: {1}: {0}", pe.NervusTroklear.Notes, pe.NervusTroklear.IsAbNormal.Value ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.NervusTrigeminus.IsAbNormal.Value)
            {
                strBuilder.AppendFormat("Nervus Trigeminus / Trigeminal: {1}: {0}", pe.NervusTrigeminus.Notes, pe.NervusTrigeminus.IsAbNormal.Value ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.NervusAbducens.IsAbNormal.Value)
            {
                strBuilder.AppendFormat("Nervus Abducens / Abducens: {1}: {0}", pe.NervusAbducens.Notes, pe.NervusAbducens.IsAbNormal.Value ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.NervusFasialis.IsAbNormal.Value)
            {
                strBuilder.AppendFormat("Nervus Fasialis / Facial Nerve: {1}: {0}", pe.NervusFasialis.Notes, pe.NervusFasialis.IsAbNormal.Value ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.NervusVestibukokhlearis.IsAbNormal.Value)
            {
                strBuilder.AppendFormat("Nervus Vestibukokhlearis / Vestibulocochlear: {1}: {0}", pe.NervusVestibukokhlearis.Notes, pe.NervusVestibukokhlearis.IsAbNormal.Value ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.NervusGlossofaringeal.IsAbNormal.Value)
            {
                strBuilder.AppendFormat("Nervus Glossofaringeal / Glossopharyngeal: {1}: {0}", pe.NervusGlossofaringeal.Notes, pe.NervusGlossofaringeal.IsAbNormal.Value ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.NervusVagus.IsAbNormal.Value)
            {
                strBuilder.AppendFormat("Nervus Vagus / Vagus Nerve: {1}: {0}", pe.NervusVagus.Notes, pe.NervusVagus.IsAbNormal.Value ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.NervusAsesoris.IsAbNormal.Value)
            {
                strBuilder.AppendFormat("Nervus Asesoris / Spinal Accessory: {1}: {0}", pe.NervusAsesoris.Notes, pe.NervusAsesoris.IsAbNormal.Value ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.NervusHipoglossus.IsAbNormal.Value)
            {
                strBuilder.AppendFormat("Nervus Hipoglossus / Hypoglossal: {1}: {0}", pe.NervusHipoglossus.Notes, pe.NervusHipoglossus.IsAbNormal.Value ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (!string.IsNullOrEmpty(pe.Neurologis))
            {
                strBuilder.AppendFormat("Neurologi: {0}", pe.Neurologis);
                strBuilder.AppendLine(string.Empty);
            }

            if (!string.IsNullOrEmpty(pe.Notes))
            {
                strBuilder.AppendFormat("{0}", pe.Notes);
                strBuilder.AppendLine(string.Empty);
            }
            return strBuilder.ToString();
        }

        #endregion


    }
}