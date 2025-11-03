namespace Temiang.Avicenna.ReportLibrary.Accounting.SubLedgers
{
    partial class SubLedgerRecapReport
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.DetailSection detail;
            Telerik.Reporting.TableGroup tableGroup19 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup20 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup21 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup22 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup23 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup24 = new Telerik.Reporting.TableGroup();
            this.ctSummary = new Telerik.Reporting.Crosstab();
            this.SumFinalBalance = new Telerik.Reporting.TextBox();
            this.Month = new Telerik.Reporting.TextBox();
            this.Year = new Telerik.Reporting.TextBox();
            this.txtChartOfAccountCode = new Telerik.Reporting.TextBox();
            this.ChartOfAccountName = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.txtReportTitle = new Telerik.Reporting.TextBox();
            this.txtCompanyName = new Telerik.Reporting.TextBox();
            this.txtPeriode = new Telerik.Reporting.TextBox();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.reportHeaderSection1 = new Telerik.Reporting.ReportHeaderSection();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.txtPrintDateTime = new Telerik.Reporting.TextBox();
            this.txtPageNumber = new Telerik.Reporting.TextBox();
            detail = new Telerik.Reporting.DetailSection();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.60003942251205444);
            detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.ctSummary});
            detail.KeepTogether = true;
            detail.Name = "detail";
            detail.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            detail.Style.Visible = true;
            // 
            // ctSummary
            // 
            this.ctSummary.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.1541669368743897)));
            this.ctSummary.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224)));
            this.ctSummary.Body.SetCellContent(0, 0, this.SumFinalBalance);
            tableGroup20.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("=Fields.Month")});
            tableGroup20.Name = "Month";
            tableGroup20.ReportItem = this.Month;
            tableGroup20.Sortings.AddRange(new Telerik.Reporting.Sorting[] {
            new Telerik.Reporting.Sorting("=Fields.Month", Telerik.Reporting.SortDirection.Asc)});
            tableGroup19.ChildGroups.Add(tableGroup20);
            tableGroup19.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("=Fields.Year")});
            tableGroup19.Name = "Year";
            tableGroup19.ReportItem = this.Year;
            tableGroup19.Sortings.AddRange(new Telerik.Reporting.Sorting[] {
            new Telerik.Reporting.Sorting("=Fields.Year", Telerik.Reporting.SortDirection.Asc)});
            this.ctSummary.ColumnGroups.Add(tableGroup19);
            this.ctSummary.ColumnHeadersPrintOnEveryPage = false;
            this.ctSummary.Corner.SetCellContent(0, 0, this.txtChartOfAccountCode, 2, 3);
            this.ctSummary.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.SumFinalBalance,
            this.Year,
            this.Month,
            this.txtChartOfAccountCode,
            this.ChartOfAccountName,
            this.textBox1,
            this.textBox3});
            this.ctSummary.KeepTogether = false;
            this.ctSummary.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.ctSummary.Name = "ctSummary";
            tableGroup24.Name = "Group1";
            tableGroup23.ChildGroups.Add(tableGroup24);
            tableGroup23.Name = "Group2";
            tableGroup23.ReportItem = this.textBox3;
            tableGroup22.ChildGroups.Add(tableGroup23);
            tableGroup22.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("=SubLedgerId")});
            tableGroup22.Name = "SubLedgerId";
            tableGroup22.ReportItem = this.textBox1;
            tableGroup22.Sortings.AddRange(new Telerik.Reporting.Sorting[] {
            new Telerik.Reporting.Sorting("=SubLedgerId", Telerik.Reporting.SortDirection.Asc)});
            tableGroup21.ChildGroups.Add(tableGroup22);
            tableGroup21.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("=ChartOfAccountCode")});
            tableGroup21.GroupKeepTogether = true;
            tableGroup21.Name = "ChartOfAccountCode";
            tableGroup21.ReportItem = this.ChartOfAccountName;
            tableGroup21.Sortings.AddRange(new Telerik.Reporting.Sorting[] {
            new Telerik.Reporting.Sorting("=Fields.ChartOfAccountCode", Telerik.Reporting.SortDirection.Asc)});
            this.ctSummary.RowGroups.Add(tableGroup21);
            this.ctSummary.RowHeadersPrintOnEveryPage = false;
            this.ctSummary.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.67291784286499), Telerik.Reporting.Drawing.Unit.Inch(0.60000002384185791));
            this.ctSummary.Sortings.AddRange(new Telerik.Reporting.Sorting[] {
            new Telerik.Reporting.Sorting("=Fields.ChartOfAccountCode", Telerik.Reporting.SortDirection.Asc)});
            this.ctSummary.Style.LineStyle = Telerik.Reporting.Drawing.LineStyle.Solid;
            this.ctSummary.StyleName = "";
            // 
            // SumFinalBalance
            // 
            this.SumFinalBalance.Format = "{0:N2}";
            this.SumFinalBalance.Name = "SumFinalBalance";
            this.SumFinalBalance.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1541671752929688), Telerik.Reporting.Drawing.Unit.Inch(0.20000001788139343));
            this.SumFinalBalance.Style.BorderColor.Default = System.Drawing.Color.Silver;
            this.SumFinalBalance.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.SumFinalBalance.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.SumFinalBalance.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2);
            this.SumFinalBalance.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.SumFinalBalance.StyleName = "";
            this.SumFinalBalance.Value = "=Sum(FinalBalance)";
            // 
            // Month
            // 
            this.Month.Name = "Month";
            this.Month.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1541671752929688), Telerik.Reporting.Drawing.Unit.Inch(0.20000001788139343));
            this.Month.Style.BorderColor.Default = System.Drawing.Color.Silver;
            this.Month.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.Month.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(0);
            this.Month.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.Month.StyleName = "";
            this.Month.Value = "=Temiang.Avicenna.ReportLibrary.Utility.MonthName(Month)";
            // 
            // Year
            // 
            this.Year.Name = "Year";
            this.Year.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1541671752929688), Telerik.Reporting.Drawing.Unit.Inch(0.20000001788139343));
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
            this.txtChartOfAccountCode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.5187506675720215), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448));
            this.txtChartOfAccountCode.Style.BorderColor.Default = System.Drawing.Color.Silver;
            this.txtChartOfAccountCode.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtChartOfAccountCode.Style.Font.Bold = true;
            this.txtChartOfAccountCode.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2);
            this.txtChartOfAccountCode.StyleName = "";
            this.txtChartOfAccountCode.Value = "Chart Of Account Code";
            // 
            // ChartOfAccountName
            // 
            this.ChartOfAccountName.Angle = 90;
            this.ChartOfAccountName.Name = "ChartOfAccountName";
            this.ChartOfAccountName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.34166780114173889), Telerik.Reporting.Drawing.Unit.Inch(0.20000001788139343));
            this.ChartOfAccountName.Style.BorderColor.Default = System.Drawing.Color.Silver;
            this.ChartOfAccountName.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.ChartOfAccountName.Style.Font.Bold = true;
            this.ChartOfAccountName.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.ChartOfAccountName.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2);
            this.ChartOfAccountName.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.ChartOfAccountName.StyleName = "";
            this.ChartOfAccountName.Value = "=ChartOfAccountName";
            // 
            // textBox1
            // 
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1), Telerik.Reporting.Drawing.Unit.Inch(0.20000001788139343));
            this.textBox1.Style.BorderColor.Default = System.Drawing.Color.Silver;
            this.textBox1.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox1.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox1.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox1.StyleName = "";
            this.textBox1.Value = "=SubLedgerName";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1770832538604736), Telerik.Reporting.Drawing.Unit.Inch(0.20000001788139343));
            this.textBox3.Style.BorderColor.Default = System.Drawing.Color.Silver;
            this.textBox3.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox3.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox3.StyleName = "";
            this.textBox3.Value = "=Description ";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(0.50000017881393433);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtReportTitle,
            this.txtCompanyName,
            this.txtPeriode});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.pageHeader.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.pageHeader.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1);
            // 
            // txtReportTitle
            // 
            this.txtReportTitle.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.099999986588954926));
            this.txtReportTitle.Name = "txtReportTitle";
            this.txtReportTitle.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.67291784286499), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtReportTitle.Style.BorderColor.Bottom = System.Drawing.Color.Blue;
            this.txtReportTitle.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(119)))), ((int)(((byte)(171)))));
            this.txtReportTitle.Style.Font.Bold = true;
            this.txtReportTitle.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.txtReportTitle.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtReportTitle.Value = "SUB LEDGER SUMMARY";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134));
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.67291784286499), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtCompanyName.Value = "";
            // 
            // txtPeriode
            // 
            this.txtPeriode.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.6730356216430664), Telerik.Reporting.Drawing.Unit.Inch(0.099999986588954926));
            this.txtPeriode.Name = "txtPeriode";
            this.txtPeriode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.3269643783569336), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtPeriode.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtPeriode.Style.Visible = false;
            this.txtPeriode.Value = "";
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699);
            this.reportFooterSection1.Name = "reportFooterSection1";
            this.reportFooterSection1.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.reportFooterSection1.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.reportFooterSection1.Style.Font.Bold = true;
            this.reportFooterSection1.Style.Visible = true;
            // 
            // reportHeaderSection1
            // 
            this.reportHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.099999904632568359);
            this.reportHeaderSection1.Name = "reportHeaderSection1";
            this.reportHeaderSection1.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.34787699580192566);
            this.pageFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtPrintDateTime,
            this.txtPageNumber});
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // txtPrintDateTime
            // 
            this.txtPrintDateTime.Format = "Avicenna HIS, Print Date : {0:dd-MM-yyyy HH:mm}";
            this.txtPrintDateTime.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.047877121716737747));
            this.txtPrintDateTime.Name = "txtPrintDateTime";
            this.txtPrintDateTime.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.6729574203491211), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtPrintDateTime.Style.Font.Bold = false;
            this.txtPrintDateTime.Style.Font.Italic = true;
            this.txtPrintDateTime.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtPrintDateTime.Value = "= Now()";
            // 
            // txtPageNumber
            // 
            this.txtPageNumber.Format = "";
            this.txtPageNumber.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.0000782012939453), Telerik.Reporting.Drawing.Unit.Inch(0.047877121716737747));
            this.txtPageNumber.Name = "txtPageNumber";
            this.txtPageNumber.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.4999215602874756), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtPageNumber.Style.Font.Bold = false;
            this.txtPageNumber.Style.Font.Italic = true;
            this.txtPageNumber.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtPageNumber.Value = "= \"Page \" + PageNumber + \" Of \" + PageCount";
            // 
            // SubLedgerRecapReport
            // 
            this.DocumentName = "Ledger Report";
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            detail,
            this.reportFooterSection1,
            this.reportHeaderSection1,
            this.pageFooterSection1});
            this.Name = "JournalListReport";
            this.PageSettings.Landscape = true;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Inch(1);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Inch(0.25);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Inch(0.25);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Inch(1);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(10.499960899353027);
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
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.TextBox txtPrintDateTime;
        private Telerik.Reporting.TextBox txtPageNumber;
    }
}