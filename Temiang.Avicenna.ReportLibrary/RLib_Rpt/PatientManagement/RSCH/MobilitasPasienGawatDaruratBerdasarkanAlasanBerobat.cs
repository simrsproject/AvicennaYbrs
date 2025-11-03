namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSCH
{
    using System;
    using System.Data;
    using Telerik.Reporting;
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;
    using Temiang.Avicenna.Common;

    /// <summary>
    /// Summary description for Report1.
    /// </summary>
    public partial class MobilitasPasienGawatDaruratBerdasarkanAlasanBerobat : Telerik.Reporting.Report
    {
        public MobilitasPasienGawatDaruratBerdasarkanAlasanBerobat(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            var healthcare = Healthcare.GetHealthcare();
            textBox11.Value = string.Format("NAMA RUMAH SAKIT: {0}", healthcare.HealthcareName);
            textBox12.Value = string.Format("KODE RS: {0}", healthcare.HospitalCode);

            var rptdata = new ReportDataSource();
            DataTable dtb = rptdata.GetDataTable(programID, printJobParameters);

            DataSource = dtb;

            table1.DataSource = dtb;
        }
    }
}