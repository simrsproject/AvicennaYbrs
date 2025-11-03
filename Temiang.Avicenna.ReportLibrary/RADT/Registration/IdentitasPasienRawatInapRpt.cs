using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.Registration
{
    /// <summary>
    /// Summary description for IdentitasPasienRawatInap.
    /// </summary>
    public partial class IdentitasPasienRawatInapRpt : Report
    {
        public IdentitasPasienRawatInapRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);         


        }

    }
}