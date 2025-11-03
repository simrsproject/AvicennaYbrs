using Temiang.Avicenna.BusinessObject;
using System.Data;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.Registration
{
    public partial class MapForderBook : Telerik.Reporting.ReportBook
    {
        public MapForderBook(string programID, PrintJobParameterCollection printJobParameters)
        {
            // ----Test Parameter--------
            //programID = "MapForderBook";
            //printJobParameters.AddNew("p_RegistrationNo", "REG/PM2/100705-0023");
            // --------------------------
            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            this.Reports.Add(new MapForder1Rpt(dtb));
            this.Reports.Add(new MapForder2Rpt(dtb));
            this.Reports.Add(new MapForder3Rpt(dtb));
            this.Reports.Add(new MapForder4Rpt(dtb));
        }
    }
}
