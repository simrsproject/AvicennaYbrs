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
    public partial class EecpPeCtl : BaseAssessmentCtl
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
        protected override void OnDataModeChanged(bool isEdited)
        {
            base.OnDataModeChanged(isEdited);
            medHistCtl.DataModeChanged(isEdited);
        }
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {

            EecpPe pExam;

            // Get Education
            var asses = assessment;
            if (!string.IsNullOrEmpty(asses.PhysicalExam))
            {
                // Convert to class w json
                try
                {
                    pExam = JsonConvert.DeserializeObject<EecpPe>(asses.PhysicalExam);
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

            // Checkingtype
            chkEkg.Checked = pExam.CheckingType.Ekg;
            chkEchocardiography.Checked = pExam.CheckingType.Echocardiography;
            chkDuplex.Checked = pExam.CheckingType.Duplex;
            chkUsgAbdomen.Checked = pExam.CheckingType.UsgAbdomen;
            chkTreadmill.Checked = pExam.CheckingType.Treadmill;
            chkAngiografi.Checked = pExam.CheckingType.Angiografi;
            chkCTScan.Checked = pExam.CheckingType.CTScan;
            txtEchocardiographyEF.Value = pExam.CheckingType.EchocardiographyEF;

            // EECPIndication
            chkRefractory.Checked = pExam.Indication.Refractory;
            chkHeartFailure.Checked = pExam.Indication.HeartFailure;
            txtEtcIndication.Text = pExam.Indication.EtcIndication;

            // EECPContraindications
            chkRegurgitation.Checked = pExam.Contraindication.Regurgitation;
            chkAortic.Checked = pExam.Contraindication.Aortic;
            chkHypertension.Checked = pExam.Contraindication.Hypertension;
            chkThromboflebitis.Checked = pExam.Contraindication.Thromboflebitis;
            chkPeripheral.Checked = pExam.Contraindication.Peripheral;
            chkArrhythmia.Checked = pExam.Contraindication.Arrhythmia;
            chkHemorrhagic.Checked = pExam.Contraindication.Hemorrhagic;
            chkPregnancy.Checked = pExam.Contraindication.Pregnancy;
            chkAbdominal.Checked = pExam.Contraindication.TumorAbdominal;

            txtDrugCurrentConsumed.Text = pExam.DrugCurrentConsumed;

            medHistCtl.PopulateEntryControl(assessment, rim);
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var pExam = new EecpPe();
            // Checkingtype
            pExam.CheckingType.Ekg = chkEkg.Checked;
            pExam.CheckingType.Echocardiography = chkEchocardiography.Checked;
            pExam.CheckingType.Duplex = chkDuplex.Checked;
            pExam.CheckingType.UsgAbdomen = chkUsgAbdomen.Checked;
            pExam.CheckingType.Treadmill = chkTreadmill.Checked;
            pExam.CheckingType.Angiografi = chkAngiografi.Checked;
            pExam.CheckingType.CTScan = chkCTScan.Checked;
            pExam.CheckingType.EchocardiographyEF = txtEchocardiographyEF.Value.ToInt();

            // EECPIndication
            pExam.Indication.Refractory = chkRefractory.Checked;
            pExam.Indication.HeartFailure = chkHeartFailure.Checked;
            pExam.Indication.EtcIndication = txtEtcIndication.Text;

            // EECPContraindications
            pExam.Contraindication.Regurgitation = chkRegurgitation.Checked;
            pExam.Contraindication.Aortic = chkAortic.Checked;
            pExam.Contraindication.Hypertension = chkHypertension.Checked;
            pExam.Contraindication.Thromboflebitis = chkThromboflebitis.Checked;
            pExam.Contraindication.Peripheral = chkPeripheral.Checked;
            pExam.Contraindication.Arrhythmia = chkArrhythmia.Checked;
            pExam.Contraindication.Hemorrhagic = chkHemorrhagic.Checked;
            pExam.Contraindication.Pregnancy = chkPregnancy.Checked;
            pExam.Contraindication.TumorAbdominal = chkAbdominal.Checked;

            pExam.DrugCurrentConsumed = txtDrugCurrentConsumed.Text;

            assessment.PhysicalExam = JsonConvert.SerializeObject(pExam);

            // Objective
            if (!string.IsNullOrWhiteSpace(rim.Info2))
                rim.Info2 = String.Concat(rim.Info2, Environment.NewLine, GenerateSoapObjective(pExam));
            else
                rim.Info2 = GenerateSoapObjective(pExam);

            medHistCtl.SetEntityValue(args, assessment, rim);
        }

        private string GenerateSoapObjective(EecpPe pe)
        {
            var strBuilder = new StringBuilder();
            // Jenis Pemeriksaan
            var checkingType = pe.CheckingType;
            if (checkingType.Angiografi || checkingType.CTScan || checkingType.Duplex || checkingType.Echocardiography ||
                checkingType.Ekg || checkingType.Treadmill || checkingType.UsgAbdomen)
            {
                strBuilder.AppendLine("Jenis Pemeriksaan:");
                if (checkingType.Ekg)
                    strBuilder.AppendLine(" - EKG");

                if (checkingType.Echocardiography)
                {
                    strBuilder.AppendFormat(" - Echocardiografi (EF = {0}%)", checkingType.EchocardiographyEF);
                    strBuilder.AppendLine(string.Empty);
                }

                if (checkingType.Duplex)
                    strBuilder.AppendLine(" - Dupleks Extremitas Bawah");

                if (checkingType.UsgAbdomen)
                    strBuilder.AppendLine(" - USG Abdomen");

                if (checkingType.Treadmill)
                    strBuilder.AppendLine(" - Treadmill Test");

                if (checkingType.Angiografi)
                    strBuilder.AppendLine(" - Angiografi");

                if (checkingType.CTScan)
                    strBuilder.AppendLine(" - CT-Scan");
                strBuilder.AppendLine(string.Empty);
            }

            //Indikasi EECP
            var indication = pe.Indication;
            if (indication.Refractory || indication.HeartFailure || !string.IsNullOrEmpty(indication.EtcIndication))
            {
                strBuilder.AppendLine("Indikasi EECP:");
                if (indication.Refractory)
                    strBuilder.AppendLine(" - Angina refrakter");
                if (indication.HeartFailure)
                    strBuilder.AppendLine(" - Gagal jantung kronik");

                if (!string.IsNullOrEmpty(indication.EtcIndication))
                {
                    strBuilder.AppendFormat(" - Lain-lain : {0}", indication.EtcIndication);
                    strBuilder.AppendLine(string.Empty);
                }
                strBuilder.AppendLine(string.Empty);
            }

            // Kontraindikasi EECP
            var contra = pe.Contraindication;
            if (contra.TumorAbdominal || contra.Aortic || contra.Arrhythmia
                || contra.Hemorrhagic || contra.Hypertension || contra.Peripheral
                || contra.Pregnancy || contra.Regurgitation || contra.Thromboflebitis)
            {
                strBuilder.AppendLine("Kontraindikasi EECP:");
                if (contra.Regurgitation)
                    strBuilder.AppendLine(" - Regurgitasi aorta berat");
                if (contra.Aortic)
                    strBuilder.AppendLine(" - Aneurisma aorta (>5 cm)");
                if (contra.Hypertension)
                    strBuilder.AppendLine(" - Hipertensi berat (≥180/110 mmHg)");
                if (contra.Thromboflebitis)
                    strBuilder.AppendLine(" - Thromboflebitis/DVT ext bawah");
                if (contra.Peripheral)
                    strBuilder.AppendLine(" - Peripheral artery disease (PAD)");
                if (contra.Arrhythmia)
                    strBuilder.AppendLine(" - Aritmia berat (AF, extrasistole)");
                if (contra.Hemorrhagic)
                    strBuilder.AppendLine(" - Diastesis hemoragik, hemofilia");
                if (contra.Pregnancy)
                    strBuilder.AppendLine(" - Kehamilan");
                if (contra.TumorAbdominal)
                    strBuilder.AppendLine(" - Tumor Abdomen");
                strBuilder.AppendLine(string.Empty);
            }

            return strBuilder.ToString();
        }



        #endregion


    }
}