namespace Temiang.Avicenna.ReportLibrary.Accounting.ProfitLoss
{
    partial class ProfitLossHighLightByUnitReport
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group2 = new Telerik.Reporting.Group();
            this.groupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.groupHeaderSection2 = new Telerik.Reporting.GroupHeaderSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.groupFooterAccountGroup = new Telerik.Reporting.GroupFooterSection();
            this.txtProfitLossSection = new Telerik.Reporting.TextBox();
            this.txtRunningValueMTD = new Telerik.Reporting.TextBox();
            this.txtRunningValueYTD = new Telerik.Reporting.TextBox();
            this.groupHeaderAccountGroup = new Telerik.Reporting.GroupHeaderSection();
            this.AccountGroupName = new Telerik.Reporting.TextBox();
            this.AccountGroupTotalMTD = new Telerik.Reporting.TextBox();
            this.AccountGroupTotalYTD = new Telerik.Reporting.TextBox();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.txtReportTitle = new Telerik.Reporting.TextBox();
            this.txtCompanyName = new Telerik.Reporting.TextBox();
            this.txtYearToDate = new Telerik.Reporting.TextBox();
            this.txtMonthToDate = new Telerik.Reporting.TextBox();
            this.txtChartOfAccountName = new Telerik.Reporting.TextBox();
            this.txtPeriode = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.txtPageNumber = new Telerik.Reporting.TextBox();
            this.txtPrintDateTime = new Telerik.Reporting.TextBox();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.SumTotalYTD = new Telerik.Reporting.TextBox();
            this.SumTotalMTD = new Telerik.Reporting.TextBox();
            this.txtTotalProfitLoss = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // groupFooterSection
            // 
            this.groupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.052D);
            this.groupFooterSection.Name = "groupFooterSection";
            this.groupFooterSection.Style.Visible = false;
            // 
            // groupHeaderSection2
            // 
            this.groupHeaderSection2.Height = Telerik.Reporting.Drawing.Unit.Inch(0.3D);
            this.groupHeaderSection2.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1});
            this.groupHeaderSection2.Name = "groupHeaderSection2";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.05D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(8D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox1.TextWrap = false;
            this.textBox1.Value = "=Description";
            // 
            // groupFooterAccountGroup
            // 
            this.groupFooterAccountGroup.Height = Telerik.Reporting.Drawing.Unit.Inch(0.5D);
            this.groupFooterAccountGroup.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtProfitLossSection,
            this.txtRunningValueMTD,
            this.txtRunningValueYTD});
            this.groupFooterAccountGroup.Name = "groupFooterAccountGroup";
            this.groupFooterAccountGroup.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.groupFooterAccountGroup.Style.Visible = true;
            // 
            // txtProfitLossSection
            // 
            this.txtProfitLossSection.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.txtProfitLossSection.Name = "txtProfitLossSection";
            this.txtProfitLossSection.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtProfitLossSection.TextWrap = true;
            this.txtProfitLossSection.Value = "=Temiang.Avicenna.ReportLibrary.Utility.ProfitLossReportSection(AccountGroup)";
            // 
            // txtRunningValueMTD
            // 
            this.txtRunningValueMTD.Format = "{0:#,##0.00;(#,##0.00)}";
            this.txtRunningValueMTD.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.txtRunningValueMTD.Name = "txtRunningValueMTD";
            this.txtRunningValueMTD.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtRunningValueMTD.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.txtRunningValueMTD.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtRunningValueMTD.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtRunningValueMTD.Value = "=RunningValue(\'\', sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwit" +
    "cher(AccountGroup,NormalBalance,Total)))";
            // 
            // txtRunningValueYTD
            // 
            this.txtRunningValueYTD.Format = "{0:#,##0.00;(#,##0.00)}";
            this.txtRunningValueYTD.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.txtRunningValueYTD.Name = "txtRunningValueYTD";
            this.txtRunningValueYTD.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtRunningValueYTD.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.txtRunningValueYTD.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtRunningValueYTD.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtRunningValueYTD.Value = "=RunningValue(\'\', sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwit" +
    "cher(AccountGroup,NormalBalance,TotalYTD)))";
            // 
            // groupHeaderAccountGroup
            // 
            this.groupHeaderAccountGroup.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2D);
            this.groupHeaderAccountGroup.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.AccountGroupName,
            this.AccountGroupTotalMTD,
            this.AccountGroupTotalYTD});
            this.groupHeaderAccountGroup.Name = "groupHeaderAccountGroup";
            this.groupHeaderAccountGroup.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            // 
            // AccountGroupName
            // 
            this.AccountGroupName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.AccountGroupName.Name = "AccountGroupName";
            this.AccountGroupName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.AccountGroupName.TextWrap = false;
            this.AccountGroupName.Value = "=AccountGroup_Name";
            // 
            // AccountGroupTotalMTD
            // 
            this.AccountGroupTotalMTD.Format = "{0:N2}";
            this.AccountGroupTotalMTD.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.AccountGroupTotalMTD.Name = "AccountGroupTotalMTD";
            this.AccountGroupTotalMTD.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.AccountGroupTotalMTD.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.AccountGroupTotalMTD.Value = "=sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcherForDisplay(Ac" +
    "countGroup,NormalBalance,Total))";
            // 
            // AccountGroupTotalYTD
            // 
            this.AccountGroupTotalYTD.Format = "{0:N2}";
            this.AccountGroupTotalYTD.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.AccountGroupTotalYTD.Name = "AccountGroupTotalYTD";
            this.AccountGroupTotalYTD.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.AccountGroupTotalYTD.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.AccountGroupTotalYTD.Value = "=sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcherForDisplay(Ac" +
    "countGroup,NormalBalance,TotalYTD))";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(1.1D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtReportTitle,
            this.txtCompanyName,
            this.txtYearToDate,
            this.txtMonthToDate,
            this.txtChartOfAccountName,
            this.txtPeriode});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.pageHeader.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.pageHeader.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.pageHeader.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            // 
            // txtReportTitle
            // 
            this.txtReportTitle.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.txtReportTitle.Name = "txtReportTitle";
            this.txtReportTitle.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtReportTitle.Style.BorderColor.Bottom = System.Drawing.Color.Blue;
            this.txtReportTitle.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(119)))), ((int)(((byte)(171)))));
            this.txtReportTitle.Style.Font.Bold = true;
            this.txtReportTitle.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.txtReportTitle.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtReportTitle.Value = "INCOME STATEMENT - SUMMARY BY UNIT";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.3D));
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtCompanyName.Value = "";
            // 
            // txtYearToDate
            // 
            this.txtYearToDate.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6D), Telerik.Reporting.Drawing.Unit.Inch(0.8D));
            this.txtYearToDate.Name = "txtYearToDate";
            this.txtYearToDate.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtYearToDate.Style.Font.Bold = true;
            this.txtYearToDate.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtYearToDate.Value = "";
            // 
            // txtMonthToDate
            // 
            this.txtMonthToDate.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4D), Telerik.Reporting.Drawing.Unit.Inch(0.8D));
            this.txtMonthToDate.Name = "txtMonthToDate";
            this.txtMonthToDate.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtMonthToDate.Style.Font.Bold = true;
            this.txtMonthToDate.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtMonthToDate.Value = "";
            // 
            // txtChartOfAccountName
            // 
            this.txtChartOfAccountName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.8D));
            this.txtChartOfAccountName.Name = "txtChartOfAccountName";
            this.txtChartOfAccountName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtChartOfAccountName.Style.Font.Bold = true;
            this.txtChartOfAccountName.Value = "Account Name";
            // 
            // txtPeriode
            // 
            this.txtPeriode.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.006D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.txtPeriode.Name = "txtPeriode";
            this.txtPeriode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.994D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtPeriode.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtPeriode.Value = "";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2D);
            this.detail.Name = "detail";
            this.detail.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.detail.Style.Visible = false;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtPageNumber,
            this.txtPrintDateTime});
            this.pageFooter.Name = "pageFooter";
            this.pageFooter.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.pageFooter.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.pageFooter.Style.Font.Bold = true;
            this.pageFooter.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.pageFooter.Style.Visible = false;
            // 
            // txtPageNumber
            // 
            this.txtPageNumber.Format = "";
            this.txtPageNumber.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.txtPageNumber.Name = "txtPageNumber";
            this.txtPageNumber.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtPageNumber.Style.Font.Bold = false;
            this.txtPageNumber.Style.Font.Italic = true;
            this.txtPageNumber.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtPageNumber.Value = "= \"Page \" + PageNumber + \" Of \" + PageCount";
            // 
            // txtPrintDateTime
            // 
            this.txtPrintDateTime.Format = "Avicenna HIS, Print Date : {0:dd-MM-yyyy HH:mm}";
            this.txtPrintDateTime.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.txtPrintDateTime.Name = "txtPrintDateTime";
            this.txtPrintDateTime.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtPrintDateTime.Style.Font.Bold = false;
            this.txtPrintDateTime.Style.Font.Italic = true;
            this.txtPrintDateTime.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtPrintDateTime.Value = "= Now()";
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.4D);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.SumTotalYTD,
            this.SumTotalMTD,
            this.txtTotalProfitLoss});
            this.reportFooterSection1.Name = "reportFooterSection1";
            this.reportFooterSection1.Style.Font.Bold = true;
            this.reportFooterSection1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            // 
            // SumTotalYTD
            // 
            this.SumTotalYTD.Format = "{0:#,##0.00;(#,##0.00)}";
            this.SumTotalYTD.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.SumTotalYTD.Name = "SumTotalYTD";
            this.SumTotalYTD.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.SumTotalYTD.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.SumTotalYTD.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Double;
            this.SumTotalYTD.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.SumTotalYTD.Value = "=sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcher(AccountGroup" +
    ",NormalBalance,TotalYTD))";
            // 
            // SumTotalMTD
            // 
            this.SumTotalMTD.Format = "{0:#,##0.00;(#,##0.00)}";
            this.SumTotalMTD.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.006D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.SumTotalMTD.Name = "SumTotalMTD";
            this.SumTotalMTD.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.994D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.SumTotalMTD.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.SumTotalMTD.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Double;
            this.SumTotalMTD.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.SumTotalMTD.Value = "=sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcher(AccountGroup" +
    ",NormalBalance,Total))";
            // 
            // txtTotalProfitLoss
            // 
            this.txtTotalProfitLoss.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.006D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.txtTotalProfitLoss.Name = "txtTotalProfitLoss";
            this.txtTotalProfitLoss.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.994D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtTotalProfitLoss.Style.Font.Bold = true;
            this.txtTotalProfitLoss.Value = "Profit Loss";
            // 
            // ProfitLossHighLightByUnitReport
            // 
            this.DocumentName = "Ledger Report";
            group1.GroupFooter = this.groupFooterSection;
            group1.GroupHeader = this.groupHeaderSection2;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("=SubLedgerId"));
            group1.Name = "SubLedgerId";
            group1.Sortings.Add(new Telerik.Reporting.Sorting("=SubLedgerId", Telerik.Reporting.SortDirection.Asc));
            group2.GroupFooter = this.groupFooterAccountGroup;
            group2.GroupHeader = this.groupHeaderAccountGroup;
            group2.Groupings.Add(new Telerik.Reporting.Grouping("=AccountGroup"));
            group2.Name = "groupAccountGroup";
            group2.Sortings.Add(new Telerik.Reporting.Sorting("=AccountGroup", Telerik.Reporting.SortDirection.Asc));
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection2,
            this.groupFooterSection,
            this.groupHeaderAccountGroup,
            this.groupFooterAccountGroup,
            this.pageHeader,
            this.detail,
            this.pageFooter,
            this.reportFooterSection1});
            this.Name = "JournalListReport";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(0.25D), Telerik.Reporting.Drawing.Unit.Inch(0.25D), Telerik.Reporting.Drawing.Unit.Inch(0.25D), Telerik.Reporting.Drawing.Unit.Inch(0.25D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Sortings.Add(new Telerik.Reporting.Sorting("=AccountGroup", Telerik.Reporting.SortDirection.Asc));
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(8D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox txtReportTitle;
        private Telerik.Reporting.TextBox txtCompanyName;
        private Telerik.Reporting.TextBox txtYearToDate;
        private Telerik.Reporting.TextBox txtMonthToDate;
        private Telerik.Reporting.TextBox txtChartOfAccountName;
        private Telerik.Reporting.TextBox AccountGroupName;
        private Telerik.Reporting.TextBox AccountGroupTotalMTD;
        private Telerik.Reporting.TextBox AccountGroupTotalYTD;
        private Telerik.Reporting.TextBox txtPeriode;
        private Telerik.Reporting.Group groupAccountGroup;
        private Telerik.Reporting.GroupFooterSection groupFooterAccountGroup;
        private Telerik.Reporting.GroupHeaderSection groupHeaderAccountGroup;
        private Telerik.Reporting.TextBox txtProfitLossSection;
        private Telerik.Reporting.TextBox txtRunningValueMTD;
        private Telerik.Reporting.TextBox txtRunningValueYTD;
        private Telerik.Reporting.ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.TextBox SumTotalYTD;
        private Telerik.Reporting.TextBox SumTotalMTD;
        private Telerik.Reporting.TextBox txtTotalProfitLoss;
        private Telerik.Reporting.TextBox txtPageNumber;
        private Telerik.Reporting.TextBox txtPrintDateTime;
        private Telerik.Reporting.GroupHeaderSection groupHeaderSection2;
        private Telerik.Reporting.GroupFooterSection groupFooterSection;
        private Telerik.Reporting.TextBox textBox1;
    }
}