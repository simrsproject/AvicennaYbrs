using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Assessment.InPatient
{
    public class KidBook : Telerik.Reporting.ReportBook
    {
        public KidBook(string programID, PrintJobParameterCollection printJobParameters)
        {
            var patientID = printJobParameters.FindByParameterName("PatientID").ValueString;
            var rimid = printJobParameters.FindByParameterName("RegistrationInfoMedicID").ValueString;

            var asses = new PatientAssessment();
            asses.LoadByPrimaryKey(rimid);

            var reg = new BusinessObject.Registration();
            reg.LoadByPrimaryKey(asses.RegistrationNo);

            this.Reports.Add(new Kid_P1(programID, printJobParameters, asses, reg));
            this.Reports.Add(new Kid_P2(programID, printJobParameters, asses, reg));
            this.Reports.Add(new Kid_P3(programID, printJobParameters, asses, reg));
            this.Reports.Add(new Kid_P4(programID, printJobParameters, asses, reg));
        }
    }
}
