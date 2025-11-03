using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Assessment.InPatient
{

    /// <summary>
    /// Summary description for Surgery_P3.
    /// </summary>
    public partial class Surgery_P3 : Telerik.Reporting.Report
    {
        public Surgery_P3(string programID, PrintJobParameterCollection printJobParameters, BusinessObject.Registration reg, PatientAssessment asses)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            txtAncillaryExam.Value = asses.OtherExam;
            txtDiagnosa.Value = asses.Diagnose;

            PopulateTherapy(asses.RegistrationInfoMedicID);

            txtLamaRawat.Value = string.Format("{0:n0} Hari", asses.EstimatedDayInPatient);
            txtPrognosis.Value = asses.Prognosis;

            txtParamedicName.Value = ParamedicTeam.DPJP(reg.RegistrationNo).ParamedicName;

        }

        private void PopulateTherapy(string registrationInfoMedicID)
        {
            var rim = new RegistrationInfoMedic();
            rim.LoadByPrimaryKey(registrationInfoMedicID);
            txtTherapy.Value = rim.Info4;
        }

    }
}