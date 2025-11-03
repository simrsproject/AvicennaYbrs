using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Assessment.InPatient
{
    public class LungBook : Telerik.Reporting.ReportBook
    {
        public LungBook(string programID, PrintJobParameterCollection printJobParameters)
        {
            var patientID = printJobParameters.FindByParameterName("PatientID").ValueString;
            var rimid = printJobParameters.FindByParameterName("RegistrationInfoMedicID").ValueString;
            var pat = new Patient();
            pat.LoadByPrimaryKey(patientID);

            var asses = new PatientAssessment();
            asses.LoadByPrimaryKey(rimid);

            var reg = new BusinessObject.Registration();
            reg.LoadByPrimaryKey(asses.RegistrationNo);

            this.Reports.Add(new Lung_P1(programID, printJobParameters, reg, asses, pat));
            this.Reports.Add(new Lung_P2(programID, printJobParameters, reg, asses, pat));
            this.Reports.Add(new Lung_P3(programID, printJobParameters, reg, asses, pat));
        }
    }
}
