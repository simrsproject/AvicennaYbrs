using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Assessment.InPatient
{
    public class InternalBook : Telerik.Reporting.ReportBook
    {
        public InternalBook(string programID, PrintJobParameterCollection printJobParameters)
        {
            var rimid = printJobParameters.FindByParameterName("RegistrationInfoMedicID").ValueString;

            var asses = new PatientAssessment();
            asses.LoadByPrimaryKey(rimid);

            var reg = new BusinessObject.Registration();
            reg.LoadByPrimaryKey(asses.RegistrationNo);

            this.Reports.Add(new Internal_P1(programID, printJobParameters, reg, asses));
            this.Reports.Add(new Internal_P2(programID, printJobParameters, reg, asses));
            this.Reports.Add(new Internal_P3(programID, printJobParameters, reg, asses));
        }
    }
}
