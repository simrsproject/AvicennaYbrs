using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.MCU
{
    /// <summary>
    /// Summary description for IdentitasPasienMCU.
    /// </summary>
    public partial class MCUPatientIdentityRpt : Report
    {
        public MCUPatientIdentityRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

//            Helper.InitializeLogo(this.pageHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);         


        }

    }
}