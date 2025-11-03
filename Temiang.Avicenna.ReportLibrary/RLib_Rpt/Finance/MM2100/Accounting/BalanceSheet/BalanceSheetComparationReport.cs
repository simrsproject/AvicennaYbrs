namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Finance.MM2100.Accounting.BalanceSheet
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
    public partial class BalanceSheetComparationReport : Telerik.Reporting.Report
    {
        private ReportDataSource reportDataSource  = new ReportDataSource();

        public BalanceSheetComparationReport(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //this.groupFooterPLSection.ItemDataBound += new EventHandler(groupFooterPLSection_ItemDataBound);
            this.groupFooterBLSection.ItemDataBound += new EventHandler(groupFooterBLSection_ItemDataBound);
            this.groupFooterSectionLVL3.ItemDataBound += new EventHandler(groupFooterSectionLVL3_ItemDataBound);

            System.Data.DataTable tbl = reportDataSource.GetDataTable(programID, printJobParameters);
            tbl.Columns.Add("ReportSectionName", typeof(string));
            tbl.Columns.Add("ReportSectionINT", typeof(int));
            tbl.Columns.Add("BalanceSheetTypeINT", typeof(int));

            SetupReport(printJobParameters);

            foreach (System.Data.DataRow row in tbl.Rows)
            {
                int accountGroup = Convert.ToInt32(row["AccountGroupLVL3"]);

                row["BalanceSheetTypeINT"] = Utility.BalanceSheetReportSectionINT(accountGroup); // 1:aktiva/2:passiva
                row["ReportSectionName"] = Utility.BalanceSheetReportSectionName(accountGroup); // Aktiva/Pasiva
                row["ReportSectionINT"] = accountGroup; // aktiva lancar, aktiva tetap ...

                row.AcceptChanges();
            }

            this.DataSource = tbl;
        }

        void groupFooterBLSection_ItemDataBound(object sender, EventArgs e)
        {
            Processing.ReportSection section = sender as Processing.ReportSection;
            Processing.ProcessingElement el = section.ChildElements.Find("txtBalanceSheetTypeSection", false)[0];
            Processing.ProcessingElement elMonth1 = section.ChildElements.Find("ProfitLossSection", false)[0];
            Processing.ProcessingElement elMonth2 = section.ChildElements.Find("ProfitLossSection_2", false)[0];
            Processing.ProcessingElement elCompare = section.ChildElements.Find("ProfitLossSection_Compare", false)[0];
            Processing.ProcessingElement elPerc = section.ChildElements.Find("ProfitLossSection_Perc", false)[0];

            Processing.TextBox txtBox = el as Processing.TextBox;
            if (txtBox != null)
            {
                if (string.IsNullOrEmpty(txtBox.Value.ToString()))
                {
                    section.Visible = false;
                }
                else
                {
                    Processing.TextBox x = elMonth1 as Processing.TextBox;
                    Processing.TextBox y = elMonth2 as Processing.TextBox;
                    if (y != null)
                    {
                        Processing.TextBox txtPerc = elPerc as Processing.TextBox;

                        decimal valFirstMonth = Convert.ToDecimal(x.Value);
                        decimal valSecondMonth = Convert.ToDecimal(y.Value);
                        decimal perc = 0;

                        if (valFirstMonth == 0)
                            perc = 100;
                        else
                            perc = ((valSecondMonth - valFirstMonth) / valFirstMonth * 100);

                        txtPerc.Value = perc;
                    }
                }
            }
        }

        void groupFooterSectionLVL3_ItemDataBound(object sender, EventArgs e)
        {
            Processing.ReportSection section = sender as Processing.ReportSection;
            Processing.ProcessingElement elMonth1 = section.ChildElements.Find("TotalAccountGroup_NameLVL3", false)[0];
            Processing.ProcessingElement elMonth2 = section.ChildElements.Find("TotalAccountGroup_NameLVL3_2", false)[0];
            Processing.ProcessingElement elCompare = section.ChildElements.Find("TotalAccountGroup_NameLVL3_Compare", false)[0];
            Processing.ProcessingElement elPerc = section.ChildElements.Find("TotalAccountGroup_NameLVL3_Perc", false)[0];

            Processing.TextBox txtBox = elMonth1 as Processing.TextBox;
            if (txtBox != null)
            {
                Processing.TextBox x = elMonth1 as Processing.TextBox;
                Processing.TextBox y = elMonth2 as Processing.TextBox;
                if (y != null && x != null)
                {
                    Processing.TextBox txtPerc = elPerc as Processing.TextBox;

                    decimal valFirstMonth = Convert.ToDecimal(x.Value);
                    decimal valSecondMonth = Convert.ToDecimal(y.Value);
                    decimal perc = 0;

                    if (valFirstMonth == 0)
                        perc = 100;
                    else
                        perc = ((valSecondMonth - valFirstMonth) / valFirstMonth * 100);

                    txtPerc.Value = perc;
                }
            }
        }

        void groupFooterPLSection_ItemDataBound(object sender, EventArgs e)
        {
            
        }

        private void SetupReport(PrintJobParameterCollection printJobParameters)
        {
            string monthStart = printJobParameters.FindByParameterName("pYMSMEPostingPeriode_MonthStart").ValueString;
            string year = printJobParameters.FindByParameterName("pYMSMEPostingPeriode_Year").ValueString;
            string monthEnd = printJobParameters.FindByParameterName("pYMSMEPostingPeriode_MonthEnd").ValueString;

            this.txtFinalBalance1.Value = string.Format("{1} - {0}", year, Utility.MonthName(monthStart));
            this.txtFinalBalance2.Value = string.Format("{1} - {0}", year, Utility.MonthName(monthEnd));

            this.txtPeriode.Value = string.Format("Periode: {0}", year);

            var healthcare = Healthcare.GetHealthcare();
            
            this.txtCompanyName.Value = healthcare.HealthcareName;
        }
    }
}