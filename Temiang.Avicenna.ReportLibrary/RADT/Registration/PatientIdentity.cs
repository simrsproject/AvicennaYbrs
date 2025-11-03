using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.Registration.GPI
{
    /// <summary>
    /// Summary description for PatientIdentity.
    /// </summary>
    public partial class PatientIdentity : Report
    {
        public PatientIdentity(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //Helper.InitializeLogo(this.pageHeader);

            var rptData = new ReportDataSource();
            this.DataSource = rptData.GetDataTable(programID, printJobParameters);

        }
    }
}