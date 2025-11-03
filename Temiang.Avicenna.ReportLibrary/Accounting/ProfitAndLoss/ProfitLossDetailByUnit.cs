namespace Temiang.Avicenna.ReportLibrary.Accounting.ProfitAndLoss
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Processing = Telerik.Reporting.Processing;
    using Temiang.Avicenna.BusinessObject.Util;
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.ReportLibrary;

    /// <summary>
    /// Journal Report Document.
    /// </summary>
    public partial class ProfitLossDetailByUnit : Telerik.Reporting.Report
    {
        private ReportDataSource reportDataSource = new ReportDataSource();

        public ProfitLossDetailByUnit(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            this.groupFooterPLSection.ItemDataBound += new EventHandler(groupFooterPLSection_ItemDataBound);

            System.Data.DataTable tbl = reportDataSource.GetDataTable(programID, printJobParameters);
            tbl.Columns.Add("ReportSectionName", typeof(string));
            tbl.Columns.Add("ReportSectionINT", typeof(int));

            SetupReport(printJobParameters);

            foreach (System.Data.DataRow row in tbl.Rows)
            {
                int accountGroup = Convert.ToInt32(row["AccountGroupLVL3"]);
                
                row["ReportSectionINT"] = accountGroup;
                row["ReportSectionName"] = Utility.ProfitLossReportSection(accountGroup);

                row.AcceptChanges();
            }

            this.DataSource = tbl;
        }

        void groupFooterPLSection_ItemDataBound(object sender, EventArgs e)
        {
            Processing.ReportSection section = sender as Processing.ReportSection;
            Processing.ProcessingElement el = section.ChildElements.Find("txtProfitLossSection", false)[0];
            Processing.TextBox txt = el as Processing.TextBox;
            if (txt != null)
            {
                if (string.IsNullOrEmpty(txt.Value.ToString()))
                    section.Visible = false;
            }
        }

        private void SetupReport(PrintJobParameterCollection printJobParameters)
        {
            string month = printJobParameters.FindByParameterName("pSinglePostingPeriode_Month").ValueString;
            string year = printJobParameters.FindByParameterName("pSinglePostingPeriode_Year").ValueString;
            

            this.txtPeriode.Value = string.Format("Periode: {0} - {1}", Utility.MonthName(month), year);

            var healthcare = Healthcare.GetHealthcare();
            this.txtCompanyName.Value = healthcare.HealthcareName;
        }
    }
}