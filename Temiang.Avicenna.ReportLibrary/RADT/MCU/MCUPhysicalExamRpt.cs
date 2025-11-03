using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.MCU
{
    /// <summary>
    /// Summary description for Physical Exam.
    /// </summary>
    public partial class MCUPhysicalExamRpt : Report
    {
        public MCUPhysicalExamRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            //            Helper.InitializeLogo(this.pageHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);


        }

    }
}