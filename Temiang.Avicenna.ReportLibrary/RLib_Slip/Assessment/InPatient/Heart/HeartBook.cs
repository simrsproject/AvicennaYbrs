using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Assessment.InPatient.Heart
{
    public class HeartBook : Telerik.Reporting.ReportBook
    {
        public HeartBook(string programID, PrintJobParameterCollection printJobParameters)
        {
            this.Reports.Add(new Heart_P1(programID, printJobParameters));
            this.Reports.Add(new Heart_P2(programID, printJobParameters));
            this.Reports.Add(new Heart_P3(programID, printJobParameters));
        }
    }
}
