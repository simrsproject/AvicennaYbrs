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
    /// Summary description for Lung_P3.
    /// </summary>
    public partial class Lung_P3 : Telerik.Reporting.Report
    {
        public Lung_P3(string programID, PrintJobParameterCollection printJobParameters, BusinessObject.Registration reg, PatientAssessment asses, Patient pat)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            Helper.InitializeLogoAndTextBottom(this.pageHeader);

            PopulateTherapy(asses.RegistrationInfoMedicID);

            txtLamaRawat.Value = string.Format("{0:n0}", asses.EstimatedDayInPatient);
            txtPrognosis.Value = asses.Prognosis;
            txtDiagnosa.Value = asses.Diagnose;

        }


        private void PopulateTherapy(string registrationInfoMedicID)
        {
            var rim = new RegistrationInfoMedic();
            rim.LoadByPrimaryKey(registrationInfoMedicID);
            txtTherapy.Value = rim.Info4;
        }

    }
}