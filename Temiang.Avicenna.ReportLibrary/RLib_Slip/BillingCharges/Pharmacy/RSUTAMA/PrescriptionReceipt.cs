using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Pharmacy.RSUTAMA
{
    /// <summary>
    /// Summary description for PrescriptionReceipt.
    /// </summary>
    public partial class PrescriptionReceipt : Report
    {
        public PrescriptionReceipt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //Helper.InitializeNoLogo(reportHeaderSection1);
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);

            var tp = new TransPrescription();
            tp.LoadByPrimaryKey(printJobParameters.FindByParameterName("p_PrescriptionNo").ValueString);
            var regno = tp.RegistrationNo;

            var reg = new BusinessObject.Registration();
            reg.LoadByPrimaryKey(regno);
            var patid = reg.PatientID;

            //decimal h = 0, w = 0;
            string alergies = string.Empty;
            ////tinggi badan
            //var phrcoll = new PatientHealthRecordLineCollection();
            //phrcoll.Query.Where(phrcoll.Query.RegistrationNo == regno, phrcoll.Query.QuestionFormID == "PHYEXAM",
            //                phrcoll.Query.QuestionID == "GEN.SGN.01");
            //phrcoll.Query.OrderBy(phrcoll.Query.LastUpdateDateTime.Descending);
            //phrcoll.Query.es.Top = 1;
            //phrcoll.LoadAll();
            //foreach (var p in phrcoll)
            //{
            //    h = p.QuestionAnswerNum ?? 0;
            //}

            ////berat badan
            //phrcoll = new PatientHealthRecordLineCollection();
            //phrcoll.Query.Where(phrcoll.Query.RegistrationNo == regno, phrcoll.Query.QuestionFormID == "PHYEXAM",
            //                phrcoll.Query.QuestionID == "GEN.SGN.02");
            //phrcoll.Query.OrderBy(phrcoll.Query.LastUpdateDateTime.Descending);
            //phrcoll.Query.es.Top = 1;
            //phrcoll.LoadAll();
            //foreach (var p in phrcoll)
            //{
            //    w = p.QuestionAnswerNum ?? 0;
            //}

            //textBox50.Value = "TB/BB : " + string.Format("{0:n0}", h) + " cm  / " + string.Format("{0:n0}", w) + " kg";

            //alergi
            var pacoll = new PatientAllergyCollection();
            pacoll.Query.Where(pacoll.Query.PatientID == patid);
            pacoll.Query.OrderBy(pacoll.Query.AllergenName.Ascending);
            pacoll.LoadAll();
            foreach (var pa in pacoll)
            {
                if (alergies == string.Empty)
                    alergies += string.Format(@"{0}", pa.DescAndReaction);
                else
                    alergies += string.Format(@"{0} {1}", ", ", pa.DescAndReaction);
            }

            textBox51.Value = alergies;

            //if (!string.IsNullOrEmpty(tp.NoTelp)) {
            //    textBox69.Value = tp.NoTelp;
            //}
            //if (!string.IsNullOrEmpty(tp.FullAddress))
            //{
            //    textBox43.Value = tp.FullAddress;
            //}
            //if (reg.SRRegistrationType == Temiang.Avicenna.Common.AppConstant.RegistrationType.OutPatient)
            //{
            //    textBox52.Value = tp.AdditionalNote;

            //    if (!string.IsNullOrEmpty(tp.SRFloor))
            //    {
            //        var std = new AppStandardReferenceItem();
            //        if (std.LoadByPrimaryKey("Floor", tp.SRFloor))
            //        {
            //            textBox57.Value = std.ItemName;
            //        }
            //    }
            //}
            //else {
            //    textBox52.Visible = textBox54.Visible = textBox53.Visible = false;
            //    textBox55.Visible = textBox57.Visible = textBox58.Visible = false;
            //    textBox21.Width = textBox56.Width;
            //}
        }
    }
}