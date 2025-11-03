namespace Temiang.Avicenna.ReportLibrary.Accounting.ProfitLoss
{
    partial class ProfitLossAccumulationReport
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.GroupFooterSection groupFooterSection2;
            Telerik.Reporting.GroupHeaderSection groupHeaderSection3;
            Telerik.Reporting.ReportFooterSection reportFooterSection1;
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group2 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group3 = new Telerik.Reporting.Group();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.txtProfitLossPageFooter = new Telerik.Reporting.TextBox();
            this.TotalProfitLossSectionPageFooter = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.SumFinalBalance = new Telerik.Reporting.TextBox();
            this.GeneralAccount = new Telerik.Reporting.TextBox();
            this.GeneralAccountName = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.txtReportTitle = new Telerik.Reporting.TextBox();
            this.txtCompanyName = new Telerik.Reporting.TextBox();
            this.txtBalanceMTD = new Telerik.Reporting.TextBox();
            this.txtChartOfAccountName = new Telerik.Reporting.TextBox();
            this.txtChartOfAccountCode = new Telerik.Reporting.TextBox();
            this.txtPeriode = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.txtPageNumber = new Telerik.Reporting.TextBox();
            this.txtPrintDateTime = new Telerik.Reporting.TextBox();
            this.txtProfitLossSection = new Telerik.Reporting.TextBox();
            this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();
            this.TotalAccountGroup_NameLVL3 = new Telerik.Reporting.TextBox();
            this.txtTotalAccountGroup_NameLVL3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
            this.AccountGroup_NameLVL3 = new Telerik.Reporting.TextBox();
            this.groupHeaderSection2 = new Telerik.Reporting.GroupHeaderSection();
            this.groupFooterPLSection = new Telerik.Reporting.GroupFooterSection();
            this.ProfitLossSection = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            groupFooterSection2 = new Telerik.Reporting.GroupFooterSection();
            groupHeaderSection3 = new Telerik.Reporting.GroupHeaderSection();
            reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // groupFooterSection2
            // 
            groupFooterSection2.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2D);
            groupFooterSection2.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox3});
            groupFooterSection2.Name = "groupFooterSection2";
            groupFooterSection2.Style.Font.Bold = true;
            groupFooterSection2.Style.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Format = "{0:#,##0.00;(#,##0.00)}";
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox1.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.textBox1.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox1.Value = "=sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcherForDisplay(Ac" +
    "countGroup,NormalBalance,FinalBalance))";
            // 
            // textBox3
            // 
            this.textBox3.Format = "{0:#,##0.00;(#,##0.00)}";
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.3D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox3.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.textBox3.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox3.Value = "=sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcherForDisplay(Ac" +
    "countGroup,NormalBalance,FinalBalanceYTD))";
            // 
            // groupHeaderSection3
            // 
            groupHeaderSection3.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2D);
            groupHeaderSection3.Name = "groupHeaderSection3";
            groupHeaderSection3.Style.Visible = false;
            // 
            // reportFooterSection1
            // 
            reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.4D);
            reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtProfitLossPageFooter,
            this.TotalProfitLossSectionPageFooter,
            this.textBox6});
            reportFooterSection1.Name = "reportFooterSection1";
            reportFooterSection1.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            reportFooterSection1.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            reportFooterSection1.Style.Font.Bold = true;
            reportFooterSection1.Style.Visible = true;
            // 
            // txtProfitLossPageFooter
            // 
            this.txtProfitLossPageFooter.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.txtProfitLossPageFooter.Name = "txtProfitLossPageFooter";
            this.txtProfitLossPageFooter.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtProfitLossPageFooter.Style.Font.Bold = true;
            this.txtProfitLossPageFooter.Value = "PROFIT LOSS";
            // 
            // TotalProfitLossSectionPageFooter
            // 
            this.TotalProfitLossSectionPageFooter.Format = "{0:#,##0.00;(#,##0.00)}";
            this.TotalProfitLossSectionPageFooter.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.TotalProfitLossSectionPageFooter.Name = "TotalProfitLossSectionPageFooter";
            this.TotalProfitLossSectionPageFooter.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.196D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.TotalProfitLossSectionPageFooter.Style.BorderColor.Bottom = System.Drawing.Color.Silver;
            this.TotalProfitLossSectionPageFooter.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.TotalProfitLossSectionPageFooter.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TotalProfitLossSectionPageFooter.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TotalProfitLossSectionPageFooter.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.TotalProfitLossSectionPageFooter.Value = "=sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcher(AccountGroup" +
    ",NormalBalance,FinalBalance))";
            // 
            // textBox6
            // 
            this.textBox6.Format = "{0:#,##0.00;(#,##0.00)}";
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.304D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.196D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox6.Style.BorderColor.Bottom = System.Drawing.Color.Silver;
            this.textBox6.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.textBox6.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox6.Value = "=sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcher(AccountGroup" +
    ",NormalBalance,FinalBalanceYTD))";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.1D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.SumFinalBalance,
            this.GeneralAccount,
            this.GeneralAccountName,
            this.textBox2});
            this.detail.Name = "detail";
            this.detail.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.detail.Style.Visible = true;
            // 
            // SumFinalBalance
            // 
            this.SumFinalBalance.Format = "{0:#,##0.00;(#,##0.00)}";
            this.SumFinalBalance.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.SumFinalBalance.Name = "SumFinalBalance";
            this.SumFinalBalance.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.SumFinalBalance.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.SumFinalBalance.Value = "=Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcherForDisplay(Accoun" +
    "tGroup,NormalBalance,FinalBalance)";
            // 
            // GeneralAccount
            // 
            this.GeneralAccount.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.GeneralAccount.Name = "GeneralAccount";
            this.GeneralAccount.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.GeneralAccount.Value = "=ChartOfAccountCode";
            // 
            // GeneralAccountName
            // 
            this.GeneralAccountName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.GeneralAccountName.Name = "GeneralAccountName";
            this.GeneralAccountName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.GeneralAccountName.TextWrap = false;
            this.GeneralAccountName.Value = "=ChartOfAccountName";
            // 
            // textBox2
            // 
            this.textBox2.Format = "{0:#,##0.00;(#,##0.00)}";
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.3D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox2.Value = "=Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcherForDisplay(Accoun" +
    "tGroup,NormalBalance,FinalBalanceYTD)";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(0.8D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtReportTitle,
            this.txtCompanyName,
            this.txtBalanceMTD,
            this.txtChartOfAccountName,
            this.txtChartOfAccountCode,
            this.txtPeriode,
            this.textBox7});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.pageHeader.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.pageHeader.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            // 
            // txtReportTitle
            // 
            this.txtReportTitle.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.txtReportTitle.Name = "txtReportTitle";
            this.txtReportTitle.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtReportTitle.Style.BorderColor.Bottom = System.Drawing.Color.Blue;
            this.txtReportTitle.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(119)))), ((int)(((byte)(171)))));
            this.txtReportTitle.Style.Font.Bold = true;
            this.txtReportTitle.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.txtReportTitle.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtReportTitle.Value = "INCOME STATEMENT ";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.3D));
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtCompanyName.Value = "";
            // 
            // txtBalanceMTD
            // 
            this.txtBalanceMTD.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5D), Telerik.Reporting.Drawing.Unit.Inch(0.6D));
            this.txtBalanceMTD.Name = "txtBalanceMTD";
            this.txtBalanceMTD.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.196D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtBalanceMTD.Style.Font.Bold = true;
            this.txtBalanceMTD.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtBalanceMTD.Value = "";
            // 
            // txtChartOfAccountName
            // 
            this.txtChartOfAccountName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.6D));
            this.txtChartOfAccountName.Name = "txtChartOfAccountName";
            this.txtChartOfAccountName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtChartOfAccountName.Style.Font.Bold = true;
            this.txtChartOfAccountName.Value = "Account Name";
            // 
            // txtChartOfAccountCode
            // 
            this.txtChartOfAccountCode.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.6D));
            this.txtChartOfAccountCode.Name = "txtChartOfAccountCode";
            this.txtChartOfAccountCode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtChartOfAccountCode.Style.Font.Bold = true;
            this.txtChartOfAccountCode.Value = "Account Code";
            // 
            // txtPeriode
            // 
            this.txtPeriode.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.txtPeriode.Name = "txtPeriode";
            this.txtPeriode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtPeriode.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtPeriode.Value = "";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.304D), Telerik.Reporting.Drawing.Unit.Inch(0.6D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.196D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox7.Value = "";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.333D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtPageNumber,
            this.txtPrintDateTime});
            this.pageFooter.Name = "pageFooter";
            this.pageFooter.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.pageFooter.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.pageFooter.Style.Font.Bold = true;
            this.pageFooter.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            // 
            // txtPageNumber
            // 
            this.txtPageNumber.Format = "";
            this.txtPageNumber.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5D), Telerik.Reporting.Drawing.Unit.Inch(0.033D));
            this.txtPageNumber.Name = "txtPageNumber";
            this.txtPageNumber.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtPageNumber.Style.Font.Bold = false;
            this.txtPageNumber.Style.Font.Italic = true;
            this.txtPageNumber.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtPageNumber.Value = "= \"Page \" + PageNumber + \" Of \" + PageCount";
            // 
            // txtPrintDateTime
            // 
            this.txtPrintDateTime.Format = "Avicenna HIS, Print Date : {0:dd-MM-yyyy HH:mm}";
            this.txtPrintDateTime.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.004D), Telerik.Reporting.Drawing.Unit.Inch(0.033D));
            this.txtPrintDateTime.Name = "txtPrintDateTime";
            this.txtPrintDateTime.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.996D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtPrintDateTime.Style.Font.Bold = false;
            this.txtPrintDateTime.Style.Font.Italic = true;
            this.txtPrintDateTime.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtPrintDateTime.Value = "= Now()";
            // 
            // txtProfitLossSection
            // 
            this.txtProfitLossSection.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.033D));
            this.txtProfitLossSection.Name = "txtProfitLossSection";
            this.txtProfitLossSection.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5D), Telerik.Reporting.Drawing.Unit.Inch(0.267D));
            this.txtProfitLossSection.Style.Color = System.Drawing.Color.OrangeRed;
            this.txtProfitLossSection.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtProfitLossSection.Style.Visible = true;
            this.txtProfitLossSection.TextWrap = false;
            this.txtProfitLossSection.Value = "=ReportSectionName";
            // 
            // groupFooterSection1
            // 
            this.groupFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.267D);
            this.groupFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.TotalAccountGroup_NameLVL3,
            this.txtTotalAccountGroup_NameLVL3,
            this.textBox4});
            this.groupFooterSection1.Name = "groupFooterSection1";
            this.groupFooterSection1.Style.Font.Bold = true;
            this.groupFooterSection1.Style.Visible = true;
            // 
            // TotalAccountGroup_NameLVL3
            // 
            this.TotalAccountGroup_NameLVL3.Format = "{0:#,##0.00;(#,##0.00)}";
            this.TotalAccountGroup_NameLVL3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.TotalAccountGroup_NameLVL3.Name = "TotalAccountGroup_NameLVL3";
            this.TotalAccountGroup_NameLVL3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2D), Telerik.Reporting.Drawing.Unit.Inch(0.265D));
            this.TotalAccountGroup_NameLVL3.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.TotalAccountGroup_NameLVL3.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.TotalAccountGroup_NameLVL3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.TotalAccountGroup_NameLVL3.Value = "=sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcherForDisplay(Ac" +
    "countGroup,NormalBalance,FinalBalance))";
            // 
            // txtTotalAccountGroup_NameLVL3
            // 
            this.txtTotalAccountGroup_NameLVL3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.txtTotalAccountGroup_NameLVL3.Name = "txtTotalAccountGroup_NameLVL3";
            this.txtTotalAccountGroup_NameLVL3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5D), Telerik.Reporting.Drawing.Unit.Inch(0.265D));
            this.txtTotalAccountGroup_NameLVL3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtTotalAccountGroup_NameLVL3.Value = "=\"TOTAL \" + AccountGroup_NameLVL3";
            // 
            // textBox4
            // 
            this.textBox4.Format = "{0:#,##0.00;(#,##0.00)}";
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.3D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2D), Telerik.Reporting.Drawing.Unit.Inch(0.265D));
            this.textBox4.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.textBox4.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox4.Value = "=sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcherForDisplay(Ac" +
    "countGroup,NormalBalance,FinalBalanceYTD))";
            // 
            // groupHeaderSection1
            // 
            this.groupHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2D);
            this.groupHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.AccountGroup_NameLVL3});
            this.groupHeaderSection1.Name = "groupHeaderSection1";
            this.groupHeaderSection1.PrintOnEveryPage = true;
            this.groupHeaderSection1.Style.Font.Bold = true;
            // 
            // AccountGroup_NameLVL3
            // 
            this.AccountGroup_NameLVL3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.004D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.AccountGroup_NameLVL3.Name = "AccountGroup_NameLVL3";
            this.AccountGroup_NameLVL3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.996D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.AccountGroup_NameLVL3.Value = "=AccountGroup_NameLVL3";
            // 
            // groupHeaderSection2
            // 
            this.groupHeaderSection2.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2D);
            this.groupHeaderSection2.Name = "groupHeaderSection2";
            this.groupHeaderSection2.Style.Visible = false;
            // 
            // groupFooterPLSection
            // 
            this.groupFooterPLSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.3D);
            this.groupFooterPLSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtProfitLossSection,
            this.ProfitLossSection,
            this.textBox5});
            this.groupFooterPLSection.Name = "groupFooterPLSection";
            this.groupFooterPLSection.Style.Font.Bold = true;
            // 
            // ProfitLossSection
            // 
            this.ProfitLossSection.Format = "{0:#,##0.00;(#,##0.00)}";
            this.ProfitLossSection.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5D), Telerik.Reporting.Drawing.Unit.Inch(0.033D));
            this.ProfitLossSection.Name = "ProfitLossSection";
            this.ProfitLossSection.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.196D), Telerik.Reporting.Drawing.Unit.Inch(0.267D));
            this.ProfitLossSection.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.ProfitLossSection.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.ProfitLossSection.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.ProfitLossSection.Value = "=RunningValue(\'\', sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwit" +
    "cher(AccountGroup,NormalBalance,FinalBalance)))";
            // 
            // textBox5
            // 
            this.textBox5.Format = "{0:#,##0.00;(#,##0.00)}";
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.3D), Telerik.Reporting.Drawing.Unit.Inch(0.033D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2D), Telerik.Reporting.Drawing.Unit.Inch(0.267D));
            this.textBox5.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.textBox5.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox5.Value = "=RunningValue(\'\', sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwit" +
    "cher(AccountGroup,NormalBalance,FinalBalanceYTD)))";
            // 
            // ProfitLossAccumulationReport
            // 
            this.DocumentName = "ProfitLoss Report";
            group1.GroupFooter = this.groupFooterPLSection;
            group1.GroupHeader = groupHeaderSection3;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("=ReportSectionINT"));
            group1.Name = "groupReportPLSection";
            group2.GroupFooter = this.groupFooterSection1;
            group2.GroupHeader = this.groupHeaderSection1;
            group2.Groupings.Add(new Telerik.Reporting.Grouping("=AccountGroupLVL3"));
            group2.Name = "groupAccountGroup";
            group3.GroupFooter = groupFooterSection2;
            group3.GroupHeader = this.groupHeaderSection2;
            group3.Groupings.Add(new Telerik.Reporting.Grouping("=GeneralAccount"));
            group3.Name = "groupGeneralAccount";
            group3.Sortings.Add(new Telerik.Reporting.Sorting("=GeneralAccount", Telerik.Reporting.SortDirection.Asc));
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2,
            group3});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            groupHeaderSection3,
            this.groupFooterPLSection,
            this.groupHeaderSection1,
            this.groupFooterSection1,
            this.groupHeaderSection2,
            groupFooterSection2,
            this.pageHeader,
            this.detail,
            this.pageFooter,
            reportFooterSection1});
            this.Name = "JournalListReport";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(0.25D), Telerik.Reporting.Drawing.Unit.Inch(0.25D), Telerik.Reporting.Drawing.Unit.Inch(0.25D), Telerik.Reporting.Drawing.Unit.Inch(0.25D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Sortings.Add(new Telerik.Reporting.Sorting("=AccountGroup", Telerik.Reporting.SortDirection.Asc));
            this.Sortings.Add(new Telerik.Reporting.Sorting("=ChartOfAccountCode", Telerik.Reporting.SortDirection.Asc));
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(8D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox txtReportTitle;
        private Telerik.Reporting.TextBox txtCompanyName;
        private Telerik.Reporting.TextBox txtBalanceMTD;
        private Telerik.Reporting.TextBox txtChartOfAccountName;
        private Telerik.Reporting.TextBox txtChartOfAccountCode;
        private Telerik.Reporting.TextBox SumFinalBalance;
        private Telerik.Reporting.TextBox txtProfitLossSection;
        private Telerik.Reporting.TextBox txtPeriode;
        private Telerik.Reporting.Group groupAccountGroup;
        private Telerik.Reporting.GroupFooterSection groupFooterSection1;
        private Telerik.Reporting.GroupHeaderSection groupHeaderSection1;
        private Telerik.Reporting.TextBox GeneralAccount;
        private Telerik.Reporting.TextBox AccountGroup_NameLVL3;
        private Telerik.Reporting.Group groupGeneralAccount;
        private Telerik.Reporting.GroupHeaderSection groupHeaderSection2;
        private Telerik.Reporting.TextBox GeneralAccountName;
        private Telerik.Reporting.TextBox TotalAccountGroup_NameLVL3;
        private Telerik.Reporting.TextBox txtTotalAccountGroup_NameLVL3;
        private Telerik.Reporting.Group groupReportPLSection;
        private Telerik.Reporting.GroupFooterSection groupFooterPLSection;
        private Telerik.Reporting.TextBox ProfitLossSection;
        private Telerik.Reporting.TextBox txtProfitLossPageFooter;
        private Telerik.Reporting.TextBox TotalProfitLossSectionPageFooter;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox txtPageNumber;
        private Telerik.Reporting.TextBox txtPrintDateTime;
    }
}