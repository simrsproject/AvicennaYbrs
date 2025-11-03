using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Assessment.InPatient
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Psychiatry_P3.
    /// </summary>
    public partial class Psychiatry_P3 : Telerik.Reporting.Report
    {
        public Psychiatry_P3(string programID, PrintJobParameterCollection printJobParameters, BusinessObject.Registration reg, PatientAssessment asses)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            Helper.InitializeLogoAndTextBottom(this.pageHeader);
           
            PopulatePhysicalExam(asses);
                        
            txtLamaRawat.Value = string.Format("{0:n0}", asses.EstimatedDayInPatient);
            txtParamedicName.Value = ParamedicTeam.DPJP(reg.RegistrationNo).ParamedicName; 

        }


        private void PopulatePhysicalExam(PatientAssessment asses)
        {
            if (string.IsNullOrEmpty(asses.PhysicalExam)) return;
            try
            {
                // Convert to class w json
                var pe = JsonConvert.DeserializeObject<PsychiatryPe>(asses.PhysicalExam);

                txtPsikodinamik.Value = pe.Psikodinamik;
                txtHalLain.Value = pe.OtherThing;
                txtAksis1.Value = pe.Aksis1;
                txtAksis2.Value = pe.Aksis2;
                txtAksis3.Value = pe.Aksis3;
                txtAksis4.Value = pe.Aksis4;
                txtAksis5.Value = pe.Aksis5;
                txtPsikofarmaka.Value = pe.Psikofarmaka;
                txtPsikoterapi.Value = pe.Psikoterapi;
                txtPsikoedukasi.Value = pe.Psikoedukasi;
                txtPsikososial.Value = pe.Psikososial;

            }
            catch (Exception)
            {
                //Nothing
            }
        }

     
  
    }
}