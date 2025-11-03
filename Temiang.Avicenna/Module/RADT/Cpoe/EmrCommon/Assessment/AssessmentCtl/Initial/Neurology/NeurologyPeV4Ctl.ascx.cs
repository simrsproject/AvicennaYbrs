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


/// <summary>
/// Status Motorik di buat kanan kiri
/// Dipakai di RSISB 
/// Created : 2023-08-16 by dk
/// </summary>

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    public partial class NeurologyPeV4Ctl : BaseAssessmentCtl
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



            gcsCtl.Condition = ent.Condition;
            gcsCtl.Gcs = ent.Consciousness;
            optNuchalRigidity.SelectedValue = ent.Meningeal.KakuKuduk == true ? "1" : "0";
            optKernig.SelectedValue = ent.Meningeal.Kernig == true ? "1" : "0";
            optLasgque.SelectedValue = ent.Meningeal.Lasgque == true ? "1" : "0";
            optPapiledema.SelectedValue = ent.Funduscopy.Papiledema == true ? "1" : "0";
            ddlCranialis.SelectedText = (ent == null ||  ent.Cranialis == null ? string.Empty : ent.Cranialis.ToString());
            optRExtermintasSuperior.SelectedValue = (ent == null || ent.Motorik == null || ent.Motorik.RSuperior == null ? string.Empty : ent.Motorik.RSuperior.ToString());
            optLExtermintasSuperior.SelectedValue = (ent == null || ent.Motorik == null || ent.Motorik.LSuperior == null ? string.Empty : ent.Motorik.LSuperior.ToString());
            optRExtermintasInterior.SelectedValue = (ent == null || ent.Motorik == null || ent.Motorik.RInterior == null ? string.Empty : ent.Motorik.RInterior.ToString());
            optLExtermintasInterior.SelectedValue = (ent == null || ent.Motorik == null || ent.Motorik.LInterior == null ? string.Empty : ent.Motorik.LInterior.ToString());
            txtRefleksFisiologis.Text = (ent == null || ent.Refleks == null || ent.Refleks.Fisiologis == null ? string.Empty : ent.Refleks.Fisiologis.ToString());
            txtRefleksPatologis.Text = (ent == null || ent.Refleks == null || ent.Refleks.Patologis == null ? string.Empty : ent.Refleks.Patologis.ToString());
            txtStatusOtonom.Text = ent.StatOtonom;
            txtPhysicalExamNotes.Text = ent.Notes;
            txtNeurologis.Text = ent.Neurologis;



        }


        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {

            var pExam = new NeurologiPe
            {
                Condition = gcsCtl.Condition,
                Consciousness = gcsCtl.Gcs,
                Neurologis = txtNeurologis.Text,
                Notes = txtPhysicalExamNotes.Text,
                StatOtonom = txtStatusOtonom.Text
                
             };
            pExam.Meningeal = new Meningeal { KakuKuduk = optNuchalRigidity.SelectedValue == "1", Kernig = optKernig.SelectedValue == "1", Lasgque = optLasgque.SelectedValue == "1" };
            pExam.Funduscopy = new Funduscopy { Papiledema = optPapiledema.SelectedValue == "1" };
            pExam.Cranialis = ddlCranialis.SelectedText.ToString();
            pExam.Motorik.RSuperior = optRExtermintasSuperior.SelectedValue.ToInt();
            pExam.Motorik.LSuperior = optLExtermintasSuperior.SelectedValue.ToInt();
            pExam.Motorik.RInterior = optRExtermintasInterior.SelectedValue.ToInt();
            pExam.Motorik.LInterior = optLExtermintasInterior.SelectedValue.ToInt();
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



            if (!string.IsNullOrEmpty(pe.Neurologis))
            {
                strBuilder.AppendFormat("Neurologi: {0}", pe.Neurologis);
                strBuilder.AppendLine(string.Empty);
            }
            //if (!string.IsNullOrEmpty(pe.Motorik.RSuperior.ToString()))
            //{
            //    strBuilder.AppendFormat("Right Superior: {0}", pe.Motorik.RSuperior.ToString());
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (!string.IsNullOrEmpty(pe.Motorik.LSuperior.ToString()))
            //{
            //    strBuilder.AppendFormat("Left Superior: {0}", pe.Motorik.LSuperior.ToString());
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (!string.IsNullOrEmpty(pe.Motorik.RInterior.ToString()))
            //{
            //    strBuilder.AppendFormat("Right Interior: {0}", pe.Motorik.RInterior.ToString());
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (!string.IsNullOrEmpty(pe.Motorik.LInterior.ToString()))
            //{
            //    strBuilder.AppendFormat("Left Interior: {0}", pe.Motorik.LInterior.ToString());
            //    strBuilder.AppendLine(string.Empty);
            //}

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