using System;
using System.Linq;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Assessment.InPatient
{
    /// <summary>
    /// Summary description for Tht_P2.
    /// </summary>
    public partial class Tht_P3 : Telerik.Reporting.Report
    {
        public Tht_P3(string programID, PrintJobParameterCollection printJobParameters, Patient pat, BusinessObject.Registration reg, PatientAssessment asses)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            Helper.InitializeLogoOnly(this.pageHeader);

            PopulatePhysicalExam(asses);

            PopulateLocalistStatus(asses);            

           
        }

        
        

        private void PopulateLocalistStatus(PatientAssessment asses)
        {
            //// Reset Image
            //picLocalistStatus01.Value = null;
            //picLocalistStatus02.Value = null;

            //// Update Image
            //var loc = new RegistrationInfoMedicBodyDiagramCollection();
            //loc.Query.Where(loc.Query.RegistrationInfoMedicID == asses.RegistrationInfoMedicID);
            //if (loc.LoadAll())
            //{
            //    if (loc.Count > 0 && loc[0] != null)
            //    {
            //        picLocalistStatus01.Value = ImageHelper.ConvertByteArrayToImage(loc[0].BodyImage);
            //    }

            //    if (loc.Count > 1 && loc[1] != null)
            //    {
            //        picLocalistStatus02.Value = ImageHelper.ConvertByteArrayToImage(loc[1].BodyImage);
            //    }
            //}
        }

        private void PopulatePhysicalExam(PatientAssessment asses)
        {
            if (string.IsNullOrEmpty(asses.PhysicalExam)) return;
            try
            {
                // Convert to class w json
                var pexam = JsonConvert.DeserializeObject<ThtPe>(asses.PhysicalExam);



            }
            catch (Exception)
            {
                //Nothing
            }
        }

        
    
    }
}