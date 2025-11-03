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
    public partial class ContinuedCtl : BaseAssessmentCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            txtPhysicalExam.Text = assessment.PhysicalExam;
            txtOtherExam.Text = assessment.OtherExam;
        }
        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment asses, RegistrationInfoMedic rim)
        {
            asses.PhysicalExam = txtPhysicalExam.Text;
            asses.OtherExam = txtOtherExam.Text;

            // Dipindah ke OnMenuSave AssessmentEntry (Parent Page)
            //// Objective
            //if (!string.IsNullOrWhiteSpace(rim.Info2))
            //    rim.Info2 = String.Concat(rim.Info2, Environment.NewLine, GenerateSoapObjective());
            //else
            //    rim.Info2 = GenerateSoapObjective();
        }

        //private string GenerateSoapObjective()
        //{
        //    var strBuilder = new StringBuilder();

        //    if (!string.IsNullOrEmpty(txtPhysicalExam.Text))
        //    {
        //        strBuilder.AppendFormat("{0}", txtPhysicalExam.Text);
        //        strBuilder.AppendLine(string.Empty);
        //    }

        //    if (!string.IsNullOrEmpty(txtOtherExam.Text))
        //    {
        //        strBuilder.AppendFormat("Pemeriksaan Penunjang : {0}", txtOtherExam.Text);
        //        strBuilder.AppendLine(string.Empty);
        //    }

        //    return strBuilder.ToString();
        //}

        #endregion


    }
}