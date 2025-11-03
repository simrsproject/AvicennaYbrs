namespace Temiang.Avicenna.ReportLibrary.Accounting.ProfitLoss
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Data;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Processing = Telerik.Reporting.Processing;
    using Temiang.Avicenna.BusinessObject.Util;
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.ReportLibrary;

    /// <summary>
    /// Journal Report Document.
    /// </summary>
    public partial class ProfitLossDetailReportV2 : Telerik.Reporting.Report
    {
        //private ReportDataSource reportDataSource = new ReportDataSource();

        public ProfitLossDetailReportV2(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);


            //System.Data.DataTable tbl = reportDataSource.GetDataTable(programID, printJobParameters);
            dtb.Columns.Add("ReportSectionName", typeof(string));
            dtb.Columns.Add("ReportSectionINT", typeof(int));

            //SetupReport(printJobParameters);

            foreach (System.Data.DataRow row in dtb.Rows)
            {
                int accountGroup = Convert.ToInt32(row["AccountGroupLVL3"]);

                row["ReportSectionINT"] = accountGroup;
                row["ReportSectionName"] = Utility.ProfitLossReportSection(accountGroup);

                row.AcceptChanges();
        }

        crosstab1.DataSource = dtb;
        }

        //void groupFooterPLSection_ItemDataBound(object sender, EventArgs e)
        //{
        //    Processing.ReportSection section = sender as Processing.ReportSection;
        //    Processing.ProcessingElement el = section.ChildElements.Find("txtProfitLossSection", false)[0];
        //    Processing.TextBox txt = el as Processing.TextBox;
        //    if (txt != null)
        //    {
        //        if (string.IsNullOrEmpty(txt.Value.ToString()))
        //            section.Visible = false;
        //    }
        //}

        private void SetupReport(PrintJobParameterCollection printJobParameters)
        {
            //string month = printJobParameters.FindByParameterName("pSinglePostingPeriode_Month").ValueString;
            string year = printJobParameters.FindByParameterName("p_PeriodYear").ValueString;

            this.txtPeriode.Value = string.Format("Periode: {0} ",  year);

            var healthcare = Healthcare.GetHealthcare();
            this.txtCompanyName.Value = healthcare.HealthcareName;
        }
    }
}