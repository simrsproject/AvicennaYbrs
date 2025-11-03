using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Master
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for ItemPackageInDetailRpt.
    /// </summary>
    public partial class ItemPackageInDetailRpt : Report
    {
        public ItemPackageInDetailRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            Helper.InitializeLogo(this.pageHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);  

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}