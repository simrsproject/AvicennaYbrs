namespace Temiang.Avicenna.ReportLibrary.Accounting.ProfitLoss
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
    public partial class ProfitLossHighLightReportV2 : Telerik.Reporting.Report
    {
        private ReportDataSource reportDataSource = new ReportDataSource();

        public ProfitLossHighLightReportV2(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            this.groupFooterAccountGroup.ItemDataBound += new EventHandler(groupFooterAccountGroup_ItemDataBound);

            SetupReport(printJobParameters);

            this.DataSource = reportDataSource.GetDataTable(programID, printJobParameters);
        }

        void groupFooterAccountGroup_ItemDataBound(object sender, EventArgs e)
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
            string monthStart = printJobParameters.FindByParameterName("pYMSMEPostingPeriode_MonthStart").ValueString;
            string year = printJobParameters.FindByParameterName("pYMSMEPostingPeriode_Year").ValueString;
            string monthEnd = printJobParameters.FindByParameterName("pYMSMEPostingPeriode_MonthEnd").ValueString;


           /* this.txtPeriode.Value = string.Format("Periode: {0} - {1}", year, string.Format("{0} To {1}", Utility.MonthName(monthStart), Utility.MonthName(monthEnd)));*/
            this.txtPeriode.Value = string.Format("Year: {0}", year);
            txtMonthToDate.Value = Utility.MonthName(monthEnd);
            txtYearToDate.Value = Utility.MonthName(monthStart) + " - " + Utility.MonthName(monthEnd);
            var healthcare = Healthcare.GetHealthcare();
            this.txtCompanyName.Value = healthcare.HealthcareName;
        }
    }
}