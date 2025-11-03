namespace Temiang.Avicenna.ReportLibrary.Accounting.ProfitLoss
{
    partial class ProfitLossHighLightReportV2
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
            Telerik.Reporting.Group group3 = new Telerik.Reporting.Group();
            this.groupFooterAccountGroup = new Telerik.Reporting.GroupFooterSection();
            this.txtProfitLossSection = new Telerik.Reporting.TextBox();
            this.txtRunningValueMTD = new Telerik.Reporting.TextBox();
            this.txtRunningValueYTD = new Telerik.Reporting.TextBox();
            this.groupHeaderAccountGroup = new Telerik.Reporting.GroupHeaderSection();
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
            this.groupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.groupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
            this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();
            this.AccountGroup_NameLVL3 = new Telerik.Reporting.TextBox();
            this.GeneralAccount = new Telerik.Reporting.TextBox();
            this.GeneralAccountName = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.txtTotalAccountGroup_NameLVL3 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.shape2 = new Telerik.Reporting.Shape();
            this.shape1 = new Telerik.Reporting.Shape();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // groupFooterAccountGroup
            // 
            this.groupFooterAccountGroup.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2D);
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
            this.groupHeaderAccountGroup.Name = "groupHeaderAccountGroup";
            this.groupHeaderAccountGroup.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
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
            this.txtReportTitle.Value = "INCOME STATEMENT - SUMMARY";
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
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.052D);
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
            // groupHeaderSection
            // 
            this.groupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2D);
            this.groupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.AccountGroup_NameLVL3});
            this.groupHeaderSection.Name = "groupHeaderSection";
            // 
            // groupFooterSection
            // 
            this.groupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.396D);
            this.groupFooterSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtTotalAccountGroup_NameLVL3,
            this.textBox3,
            this.textBox4,
            this.shape1,
            this.shape2});
            this.groupFooterSection.Name = "groupFooterSection";
            // 
            // groupHeaderSection1
            // 
            this.groupHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2D);
            this.groupHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.GeneralAccountName,
            this.GeneralAccount,
            this.textBox1,
            this.textBox2});
            this.groupHeaderSection1.Name = "groupHeaderSection1";
            // 
            // groupFooterSection1
            // 
            this.groupFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.052D);
            this.groupFooterSection1.Name = "groupFooterSection1";
            this.groupFooterSection1.Style.Visible = false;
            // 
            // AccountGroup_NameLVL3
            // 
            this.AccountGroup_NameLVL3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.006D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.AccountGroup_NameLVL3.Name = "AccountGroup_NameLVL3";
            this.AccountGroup_NameLVL3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.996D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.AccountGroup_NameLVL3.Value = "=AccountGroup_NameLVL3";
            // 
            // GeneralAccount
            // 
            this.GeneralAccount.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.006D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.GeneralAccount.Name = "GeneralAccount";
            this.GeneralAccount.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.GeneralAccount.Value = "=ChartOfAccountCode";
            // 
            // GeneralAccountName
            // 
            this.GeneralAccountName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.006D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.GeneralAccountName.Name = "GeneralAccountName";
            this.GeneralAccountName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.994D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.GeneralAccountName.TextWrap = false;
            this.GeneralAccountName.Value = "=ChartOfAccountName";
            // 
            // textBox1
            // 
            this.textBox1.Format = "{0:N2}";
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox1.Value = "=sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcherForDisplay(Ac" +
    "countGroup,NormalBalance,TotalYTD))";
            // 
            // textBox2
            // 
            this.textBox2.Format = "{0:N2}";
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox2.Value = "=sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcherForDisplay(Ac" +
    "countGroup,NormalBalance,Total))";
            // 
            // txtTotalAccountGroup_NameLVL3
            // 
            this.txtTotalAccountGroup_NameLVL3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.096D));
            this.txtTotalAccountGroup_NameLVL3.Name = "txtTotalAccountGroup_NameLVL3";
            this.txtTotalAccountGroup_NameLVL3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtTotalAccountGroup_NameLVL3.Style.Font.Bold = true;
            this.txtTotalAccountGroup_NameLVL3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtTotalAccountGroup_NameLVL3.Value = "=\"TOTAL \" + AccountGroup_NameLVL3";
            // 
            // textBox3
            // 
            this.textBox3.Format = "{0:N2}";
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6D), Telerik.Reporting.Drawing.Unit.Inch(0.096D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox3.Value = "=sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcherForDisplay(Ac" +
    "countGroup,NormalBalance,TotalYTD))";
            // 
            // textBox4
            // 
            this.textBox4.Format = "{0:N2}";
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4D), Telerik.Reporting.Drawing.Unit.Inch(0.096D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox4.Value = "=sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcherForDisplay(Ac" +
    "countGroup,NormalBalance,Total))";
            // 
            // shape2
            // 
            this.shape2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.shape2.Name = "shape2";
            this.shape2.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.9D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.shape2.Style.Color = System.Drawing.Color.Silver;
            this.shape2.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            // 
            // shape1
            // 
            this.shape1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.shape1.Name = "shape1";
            this.shape1.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.shape1.Style.Color = System.Drawing.Color.Silver;
            this.shape1.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            // 
            // ProfitLossHighLightReportV2
            // 
            this.DocumentName = "Ledger Report";
            group1.GroupFooter = this.groupFooterAccountGroup;
            group1.GroupHeader = this.groupHeaderAccountGroup;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("=AccountGroup"));
            group1.Name = "groupAccountGroup";
            group1.Sortings.Add(new Telerik.Reporting.Sorting("=AccountGroup", Telerik.Reporting.SortDirection.Asc));
            group2.GroupFooter = this.groupFooterSection;
            group2.GroupHeader = this.groupHeaderSection;
            group2.Name = "group";
            group3.GroupFooter = this.groupFooterSection1;
            group3.GroupHeader = this.groupHeaderSection1;
            group3.Groupings.Add(new Telerik.Reporting.Grouping("=ChartOfAccountCode"));
            group3.Name = "group1";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2,
            group3});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderAccountGroup,
            this.groupFooterAccountGroup,
            this.pageHeader,
            this.detail,
            this.pageFooter,
            this.reportFooterSection1,
            this.groupHeaderSection,
            this.groupFooterSection,
            this.groupHeaderSection1,
            this.groupFooterSection1});
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
        private Telerik.Reporting.GroupHeaderSection groupHeaderSection;
        private Telerik.Reporting.GroupFooterSection groupFooterSection;
        private Telerik.Reporting.GroupHeaderSection groupHeaderSection1;
        private Telerik.Reporting.GroupFooterSection groupFooterSection1;
        private Telerik.Reporting.TextBox AccountGroup_NameLVL3;
        private Telerik.Reporting.TextBox GeneralAccountName;
        private Telerik.Reporting.TextBox GeneralAccount;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox txtTotalAccountGroup_NameLVL3;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.Shape shape1;
        private Telerik.Reporting.Shape shape2;
    }
}