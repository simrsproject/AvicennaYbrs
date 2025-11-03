using System;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.Medical_Record.RSSA
{
    /// <summary>
    /// Summary description for JumlahPasienPenyakitMenularDanTidakMenular.
    /// </summary>
    public partial class JumlahPasienPenyakitMenularDanTidakMenular : Report
    {
        public JumlahPasienPenyakitMenularDanTidakMenular(string programID,
                                                          PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            var rptdata = new ReportDataSource();

            //DataTable dtb = rptdata.GetDataTable(programID, printJobParameters);
            //DataSource = dtb;
            Helper.InitializeDataSource(this, programID, printJobParameters);
            Helper.InitializeLogo(pageHeaderSection1);
            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            textBox15.Value = string.Format("Periode : {0:dd-MMM-yyyy} s/d {1:dd-MMM-yyyy}", fromDate, toDate);
        }
    }
}