using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Assessment.InPatient.Stroke
{
    public class StrokeBook : Telerik.Reporting.ReportBook
    {
        public StrokeBook(string programID, PrintJobParameterCollection printJobParameters)
        {
            var rimid = printJobParameters.FindByParameterName("RegistrationInfoMedicID").ValueString;

            var asses = new PatientAssessment();
            asses.LoadByPrimaryKey(rimid);

            var reg = new BusinessObject.Registration();
            reg.LoadByPrimaryKey(asses.RegistrationNo);

            this.Reports.Add(new Stroke_P1(programID, printJobParameters, reg, asses));
            this.Reports.Add(new Stroke_P2(programID, printJobParameters, reg, asses));
            this.Reports.Add(new Stroke_P3(programID, printJobParameters, reg, asses));
            this.Reports.Add(new Stroke_P4(programID, printJobParameters, reg, asses));
        }
    }
}
