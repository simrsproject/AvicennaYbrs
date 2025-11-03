using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Util;
namespace Temiang.Avicenna.ReportLibrary.RADT.RSCH
{
    using System;
    using System.Data;
    using BusinessObject;
    

    /// <summary>
    /// Summary description for HarianBayiLahir.
    /// </summary>
    public partial class HarianBayiLahirHidup : Telerik.Reporting.Report
    {
        public HarianBayiLahirHidup(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            var rptdata = new ReportDataSource();
            Helper.InitializeNoLogoAlignLeft(this.pageHeaderSection1);
            DataTable dtb = rptdata.GetDataTable(programID, printJobParameters);

            DataSource = dtb;

            table1.DataSource = dtb;


            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

           textBox30.Value = string.Format("Periode : {0:dd MMMM yyyy} s/d {1:dd MMMM yyyy}", fromDate, toDate);

        }
    }
}