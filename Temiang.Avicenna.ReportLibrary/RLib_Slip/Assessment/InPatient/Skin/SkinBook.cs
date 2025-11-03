using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Assessment.InPatient
{
    public class SkinBook : Telerik.Reporting.ReportBook
    {
        public SkinBook(string programID, PrintJobParameterCollection printJobParameters)
        {
            var rimid = printJobParameters.FindByParameterName("RegistrationInfoMedicID").ValueString;

            var asses = new PatientAssessment();
            asses.LoadByPrimaryKey(rimid);

            var reg = new BusinessObject.Registration();
            reg.LoadByPrimaryKey(asses.RegistrationNo);

            this.Reports.Add(new Skin_P1(programID, printJobParameters, reg, asses));
            this.Reports.Add(new Skin_P2(programID, printJobParameters, reg, asses));
            this.Reports.Add(new Skin_P3(programID, printJobParameters, reg, asses));
        }
    }
}
