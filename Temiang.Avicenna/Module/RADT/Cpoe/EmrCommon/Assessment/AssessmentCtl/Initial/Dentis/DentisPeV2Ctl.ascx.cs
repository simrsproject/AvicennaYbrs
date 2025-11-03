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
    public partial class DentisPeV2Ctl : BaseAssessmentCtl
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

            var ent = new DentisPe();

            // Get Education
            var asses = assessment;
            if (!string.IsNullOrEmpty(asses.PhysicalExam))
            {
                // Convert to class w json
                try
                {
                    ent = JsonConvert.DeserializeObject<DentisPe>(asses.PhysicalExam);
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

            txtExtraOral.Text = ent.ExtraOral;
            txtBibir.Text = ent.IntraOral.Bibir;
            txtPalatum.Text = ent.IntraOral.Palatum;
            txtLidah.Text = ent.IntraOral.Lidah;
            txtDasarMulut.Text = ent.IntraOral.DasarMulut;
            txtVestibulum.Text = ent.IntraOral.Vestibulum;
            txtGinggiva.Text = ent.IntraOral.Ginggiva;
            txtMukosaBukal.Text = ent.IntraOral.MukosaBukal;
            txtMukosaLingual.Text = ent.IntraOral.MukosaLingual;
            txtTonsil.Text = ent.IntraOral.Tonsil;
            txtTeeth.Text = ent.IntraOral.Teeth;
            txtIntraOralOther.Text = ent.IntraOral.Other;
            //txtPhysicalExamNotes.Text = ent.Notes;

            //if (!string.IsNullOrWhiteSpace(rim.Info4) && !string.IsNullOrWhiteSpace(ent.ActionAndTherapy) &&
            //    rim.Info4.Length > (ent.ActionAndTherapy.Length + 2))
            //    txtPlanning.Text =
            //        rim.Info4.Substring(ent.ActionAndTherapy
            //            .Length + 2); // rim.Info4 berisi gabungan ActionAndTherapy + "\r\n" + Planning
            //else
            //    txtPlanning.Text = rim.Info4;

            //txtActionAndTherapy.Text = ent.ActionAndTherapy;
        }


        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var ent = new DentisPe();
            ent.ExtraOral = txtExtraOral.Text;
            //ent.Notes = txtPhysicalExamNotes.Text;
            //ent.ActionAndTherapy = txtActionAndTherapy.Text;
            ent.IntraOral = new IntraOral
            {
                Bibir = txtBibir.Text,
                Palatum = txtPalatum.Text,
                Lidah = txtLidah.Text,
                DasarMulut = txtDasarMulut.Text,
                Vestibulum = txtVestibulum.Text,
                Ginggiva = txtGinggiva.Text,
                MukosaBukal = txtMukosaBukal.Text,
                MukosaLingual = txtMukosaLingual.Text,
                Tonsil = txtTonsil.Text,
                Teeth = txtTeeth.Text,
                Other = txtIntraOralOther.Text
            };

            assessment.PhysicalExam = JsonConvert.SerializeObject(ent);

            // Objective
            if (!string.IsNullOrWhiteSpace(rim.Info2))
                rim.Info2 = String.Concat(rim.Info2, Environment.NewLine, GenerateSoapObjective(ent));
            else
                rim.Info2 = GenerateSoapObjective(ent);

            // Planning
            // Kolom Action & therapy masuk ke kolom P
            var strBuilder = new StringBuilder();
            //if (!string.IsNullOrWhiteSpace(txtActionAndTherapy.Text))
            //    strBuilder.Append(txtActionAndTherapy.Text);

            //if (!string.IsNullOrWhiteSpace(rim.Info4) && !string.IsNullOrWhiteSpace(txtPlanning.Text))
            //    strBuilder.AppendLine(string.Empty);

            //if (!string.IsNullOrWhiteSpace(txtPlanning.Text))
            //    strBuilder.Append(txtPlanning.Text);

            rim.Info4 = strBuilder.ToString();
        }

        private string GenerateSoapObjective(DentisPe pe)
        {
            var strBuilder = new StringBuilder();
            var isExtraExist = false;
            if (!string.IsNullOrEmpty(pe.ExtraOral))
            {
                isExtraExist = true;
                strBuilder.AppendLine("Extra Oral:");
                strBuilder.AppendLine(pe.ExtraOral);
            }

            var isIntraExist = false;
            var strIntra = new StringBuilder();
            if (!string.IsNullOrEmpty(pe.IntraOral.Bibir))
            {
                isIntraExist = true;
                strIntra.AppendLine(string.Empty);
                strIntra.AppendFormat("• Bibir: {0}", pe.IntraOral.Bibir);

            }
            if (!string.IsNullOrEmpty(pe.IntraOral.Palatum))
            {
                if (isIntraExist)
                    strIntra.AppendLine(string.Empty);
                isIntraExist = true;
                strIntra.AppendFormat("• Palatum: {0}", pe.IntraOral.Palatum);
            }

            if (!string.IsNullOrEmpty(pe.IntraOral.Lidah))
            {
                if (isIntraExist)
                    strIntra.AppendLine(string.Empty);
                isIntraExist = true;
                strIntra.AppendFormat("• Lidah: {0}", pe.IntraOral.Lidah);
            }
            if (!string.IsNullOrEmpty(pe.IntraOral.DasarMulut))
            {
                if (isIntraExist)
                    strIntra.AppendLine(string.Empty);
                isIntraExist = true;
                strIntra.AppendFormat("• Dasar Mulut: {0}", pe.IntraOral.DasarMulut);
            }
            if (!string.IsNullOrEmpty(pe.IntraOral.Vestibulum))
            {
                if (isIntraExist)
                    strIntra.AppendLine(string.Empty);
                isIntraExist = true;
                strIntra.AppendFormat("• Vestibulum: {0}", pe.IntraOral.Vestibulum);
            }
            if (!string.IsNullOrEmpty(pe.IntraOral.Ginggiva))
            {
                if (isIntraExist)
                    strIntra.AppendLine(string.Empty);
                isIntraExist = true;
                strIntra.AppendFormat("• Ginggiva: {0}", pe.IntraOral.Ginggiva);
            }
            if (!string.IsNullOrEmpty(pe.IntraOral.MukosaBukal))
            {
                if (isIntraExist)
                    strIntra.AppendLine(string.Empty);
                isIntraExist = true;
                strIntra.AppendFormat("• Mukosa Bukal: {0}", pe.IntraOral.MukosaBukal);
            }

            if (!string.IsNullOrEmpty(pe.IntraOral.Tonsil))
            {
                if (isIntraExist)
                    strIntra.AppendLine(string.Empty);
                isIntraExist = true;
                strIntra.AppendFormat("• Tonsil: {0}", pe.IntraOral.Tonsil);
            }

            if (!string.IsNullOrEmpty(pe.IntraOral.Teeth))
            {
                if (isIntraExist)
                    strIntra.AppendLine(string.Empty);
                isIntraExist = true;
                strIntra.AppendFormat("• Gigi: {0}", pe.IntraOral.Teeth);
            }

            if (isIntraExist)
            {
                if (isExtraExist)
                {
                    strBuilder.AppendLine(string.Empty);
                }
                strBuilder.AppendFormat("Intra Oral:");
                strBuilder.AppendLine(strIntra.ToString());
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