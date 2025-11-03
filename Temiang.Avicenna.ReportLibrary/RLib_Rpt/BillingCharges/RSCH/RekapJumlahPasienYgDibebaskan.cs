namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges.RSCH
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Temiang.Avicenna.BusinessObject.Util;
    using Temiang.Avicenna.BusinessObject;

    /// <summary>
    /// Summary description for Report1.
    /// </summary>
    public partial class RekapJumlahPasienYgDibebaskan : Telerik.Reporting.Report
    {
        public RekapJumlahPasienYgDibebaskan(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
           
            InitializeComponent();
            Helper.InitializeNoLogoAlignLeft(pageHeaderSection1);
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}