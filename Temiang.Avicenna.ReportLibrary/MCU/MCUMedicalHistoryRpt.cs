using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.MCU
{
    /// <summary>
    /// Summary description for Medical History.
    /// </summary>
    public partial class MCUMedicalHistoryRpt : Report
    {
        public MCUMedicalHistoryRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            //            Helper.InitializeLogo(this.pageHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);


        }

    }
}