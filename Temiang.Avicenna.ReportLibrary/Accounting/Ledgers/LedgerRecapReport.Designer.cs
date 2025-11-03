namespace Temiang.Avicenna.ReportLibrary.Accounting.Ledgers
{
    partial class LedgerRecapReport
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.DetailSection detail;
            Telerik.Reporting.TableGroup tableGroup1 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup2 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup3 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.PageFooterSection pageFooter;
            this.ctSummary = new Telerik.Reporting.Crosstab();
            this.SumFinalBalance = new Telerik.Reporting.TextBox();
            this.Month = new Telerik.Reporting.TextBox();
            this.Year = new Telerik.Reporting.TextBox();
            this.txtChartOfAccountCode = new Telerik.Reporting.TextBox();
            this.ChartOfAccountName = new Telerik.Reporting.TextBox();
            this.txtPrintDateTime = new Telerik.Reporting.TextBox();
            this.txtPageNumber = new Telerik.Reporting.TextBox();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.txtReportTitle = new Telerik.Reporting.TextBox();
            this.txtCompanyName = new Telerik.Reporting.TextBox();
            this.txtPeriode = new Telerik.Reporting.TextBox();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.reportHeaderSection1 = new Telerik.Reporting.ReportHeaderSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            detail = new Telerik.Reporting.DetailSection();
            pageFooter = new Telerik.Reporting.PageFooterSection();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.6D);
            detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.ctSummary});
            detail.KeepTogether = true;
            detail.Name = "detail";
            detail.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            detail.Style.Visible = true;
            // 
            // ctSummary
            // 
            this.ctSummary.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.996D)));
            this.ctSummary.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Inch(0.2D)));
            this.ctSummary.Body.SetCellContent(0, 0, this.SumFinalBalance);
            tableGroup2.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.Month"));
            tableGroup2.Name = "Month";
            tableGroup2.ReportItem = this.Month;
            tableGroup2.Sortings.Add(new Telerik.Reporting.Sorting("=Fields.Month", Telerik.Reporting.SortDirection.Asc));
            tableGroup1.ChildGroups.Add(tableGroup2);
            tableGroup1.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.Year"));
            tableGroup1.Name = "Year";
            tableGroup1.ReportItem = this.Year;
            tableGroup1.Sortings.Add(new Telerik.Reporting.Sorting("=Fields.Year", Telerik.Reporting.SortDirection.Asc));
            this.ctSummary.ColumnGroups.Add(tableGroup1);
            this.ctSummary.ColumnHeadersPrintOnEveryPage = false;
            this.ctSummary.Corner.SetCellContent(0, 0, this.txtChartOfAccountCode, 2, 1);
            this.ctSummary.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtChartOfAccountCode,
            this.SumFinalBalance,
            this.Year,
            this.Month,
            this.ChartOfAccountName});
            this.ctSummary.KeepTogether = false;
            this.ctSummary.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.ctSummary.Name = "ctSummary";
            tableGroup3.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.ChartOfAccountCode"));
            tableGroup3.GroupKeepTogether = true;
            tableGroup3.Name = "ChartOfAccountCode";
            tableGroup3.ReportItem = this.ChartOfAccountName;
            tableGroup3.Sortings.Add(new Telerik.Reporting.Sorting("=Fields.ChartOfAccountCode", Telerik.Reporting.SortDirection.Asc));
            this.ctSummary.RowGroups.Add(tableGroup3);
            this.ctSummary.RowHeadersPrintOnEveryPage = false;
            this.ctSummary.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.804D), Telerik.Reporting.Drawing.Unit.Inch(0.6D));
            this.ctSummary.Sortings.Add(new Telerik.Reporting.Sorting("=Fields.ChartOfAccountCode", Telerik.Reporting.SortDirection.Asc));
            this.ctSummary.Style.LineStyle = Telerik.Reporting.Drawing.LineStyle.Solid;
            this.ctSummary.StyleName = "";
            // 
            // SumFinalBalance
            // 
            this.SumFinalBalance.Format = "{0:N2}";
            this.SumFinalBalance.Name = "SumFinalBalance";
            this.SumFinalBalance.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.996D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.SumFinalBalance.Style.BorderColor.Default = System.Drawing.Color.Silver;
            this.SumFinalBalance.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.SumFinalBalance.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.SumFinalBalance.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.SumFinalBalance.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.SumFinalBalance.StyleName = "";
            this.SumFinalBalance.Value = "=Sum(FinalBalance)";
            // 
            // Month
            // 
            this.Month.Name = "Month";
            this.Month.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.996D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.Month.Style.BorderColor.Default = System.Drawing.Color.Silver;
            this.Month.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.Month.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.Month.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.Month.StyleName = "";
            this.Month.Value = "=Fields.Month +\' \' +ItemName";
            // 
            // Year
            // 
            this.Year.Name = "Year";
            this.Year.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.996D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.Year.Style.BorderColor.Default = System.Drawing.Color.Silver;
            this.Year.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.Year.Style.Font.Bold = true;
            this.Year.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.Year.StyleName = "";
            this.Year.Value = "=Year";
            // 
            // txtChartOfAccountCode
            // 
            this.txtChartOfAccountCode.Name = "txtChartOfAccountCode";
            this.txtChartOfAccountCode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.808D), Telerik.Reporting.Drawing.Unit.Inch(0.4D));
            this.txtChartOfAccountCode.Style.BorderColor.Default = System.Drawing.Color.Silver;
            this.txtChartOfAccountCode.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtChartOfAccountCode.Style.Font.Bold = true;
            this.txtChartOfAccountCode.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.txtChartOfAccountCode.StyleName = "";
            this.txtChartOfAccountCode.Value = "Chart Of Account Code";
            // 
            // ChartOfAccountName
            // 
            this.ChartOfAccountName.Name = "ChartOfAccountName";
            this.ChartOfAccountName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.808D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.ChartOfAccountName.Style.BorderColor.Default = System.Drawing.Color.Silver;
            this.ChartOfAccountName.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.ChartOfAccountName.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.ChartOfAccountName.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.ChartOfAccountName.StyleName = "";
            this.ChartOfAccountName.Value = "=ChartOfAccountName";
            // 
            // pageFooter
            // 
            pageFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.3D);
            pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtPrintDateTime,
            this.txtPageNumber});
            pageFooter.Name = "pageFooter";
            pageFooter.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            pageFooter.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            pageFooter.Style.Font.Bold = true;
            pageFooter.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            pageFooter.Style.Visible = false;
            // 
            // txtPrintDateTime
            // 
            this.txtPrintDateTime.Format = "Avicenna HIS, Print Date : {0:dd-MM-yyyy HH:mm}";
            this.txtPrintDateTime.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.txtPrintDateTime.Name = "txtPrintDateTime";
            this.txtPrintDateTime.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.8D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtPrintDateTime.Style.Font.Bold = false;
            this.txtPrintDateTime.Style.Font.Italic = true;
            this.txtPrintDateTime.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtPrintDateTime.Value = "= Now()";
            // 
            // txtPageNumber
            // 
            this.txtPageNumber.Format = "";
            this.txtPageNumber.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.8D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.txtPageNumber.Name = "txtPageNumber";
            this.txtPageNumber.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.7D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtPageNumber.Style.Font.Bold = false;
            this.txtPageNumber.Style.Font.Italic = true;
            this.txtPageNumber.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtPageNumber.Value = "= \"Page \" + PageNumber + \" Of \" + PageCount";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(0.5D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtReportTitle,
            this.txtCompanyName,
            this.txtPeriode,
            this.textBox1});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.pageHeader.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.pageHeader.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            // 
            // txtReportTitle
            // 
            this.txtReportTitle.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.txtReportTitle.Name = "txtReportTitle";
            this.txtReportTitle.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.8D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtReportTitle.Style.BorderColor.Bottom = System.Drawing.Color.Blue;
            this.txtReportTitle.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(119)))), ((int)(((byte)(171)))));
            this.txtReportTitle.Style.Font.Bold = true;
            this.txtReportTitle.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.txtReportTitle.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtReportTitle.Value = "LEDGER SUMMARY";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.3D));
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.8D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtCompanyName.Value = "";
            // 
            // txtPeriode
            // 
            this.txtPeriode.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.8D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.txtPeriode.Name = "txtPeriode";
            this.txtPeriode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.7D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtPeriode.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtPeriode.Style.Visible = false;
            this.txtPeriode.Value = "";
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.1D);
            this.reportFooterSection1.Name = "reportFooterSection1";
            this.reportFooterSection1.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.reportFooterSection1.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.reportFooterSection1.Style.Font.Bold = true;
            this.reportFooterSection1.Style.Visible = true;
            // 
            // reportHeaderSection1
            // 
            this.reportHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.1D);
            this.reportHeaderSection1.Name = "reportHeaderSection1";
            this.reportHeaderSection1.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.8D), Telerik.Reporting.Drawing.Unit.Inch(0.3D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.7D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox1.Style.Visible = false;
            this.textBox1.Value = "";
            // 
            // LedgerRecapReport
            // 
            this.DocumentName = "Ledger Report";
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            detail,
            pageFooter,
            this.reportFooterSection1,
            this.reportHeaderSection1});
            this.Name = "JournalListReport";
            this.PageSettings.Landscape = true;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(0.25D), Telerik.Reporting.Drawing.Unit.Inch(0.25D), Telerik.Reporting.Drawing.Unit.Inch(0.25D), Telerik.Reporting.Drawing.Unit.Inch(0.25D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(10.5D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.TextBox txtReportTitle;
        private Telerik.Reporting.TextBox txtCompanyName;
        private Telerik.Reporting.TextBox txtPeriode;
        private Telerik.Reporting.ReportHeaderSection reportHeaderSection1;
        private Telerik.Reporting.Crosstab ctSummary;
        private Telerik.Reporting.TextBox txtChartOfAccountCode;
        private Telerik.Reporting.TextBox ChartOfAccountName;
        private Telerik.Reporting.TextBox SumFinalBalance;
        private Telerik.Reporting.TextBox Month;
        private Telerik.Reporting.TextBox Year;
        private Telerik.Reporting.ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.TextBox txtPrintDateTime;
        private Telerik.Reporting.TextBox txtPageNumber;
        private Telerik.Reporting.TextBox textBox1;
    }
}