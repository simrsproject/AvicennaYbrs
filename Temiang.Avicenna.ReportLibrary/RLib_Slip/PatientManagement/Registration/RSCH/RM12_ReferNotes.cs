using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSCH
{
    public class RM12_ReferNotes : Telerik.Reporting.ReportBook
    {
        public RM12_ReferNotes(string programID, PrintJobParameterCollection printJobParameters)
        {
            this.Reports.Add(new RM12_01_ReferNotes(programID, printJobParameters));
            this.Reports.Add(new RM12_02_ReferNotes(programID, printJobParameters));
        }
    }
}
