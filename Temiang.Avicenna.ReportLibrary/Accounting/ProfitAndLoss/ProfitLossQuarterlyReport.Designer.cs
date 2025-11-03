namespace Temiang.Avicenna.ReportLibrary.Accounting.ProfitLoss
{
    partial class ProfitLossQuarterlyReport
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
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.txtProfitLossPageFooter = new Telerik.Reporting.TextBox();
            this.TotalProfitLossSectionPageFooter = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.SumFinalBalance = new Telerik.Reporting.TextBox();
            this.GeneralAccount = new Telerik.Reporting.TextBox();
            this.GeneralAccountName = new Telerik.Reporting.TextBox();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.txtReportTitle = new Telerik.Reporting.TextBox();
            this.txtCompanyName = new Telerik.Reporting.TextBox();
            this.txtFinalBalance = new Telerik.Reporting.TextBox();
            this.txtChartOfAccountName = new Telerik.Reporting.TextBox();
            this.txtChartOfAccountCode = new Telerik.Reporting.TextBox();
            this.txtPeriode = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.txtProfitLossSection = new Telerik.Reporting.TextBox();
            this.groupAccountGroup = new Telerik.Reporting.Group();
            this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();
            this.TotalAccountGroup_NameLVL3 = new Telerik.Reporting.TextBox();
            this.txtTotalAccountGroup_NameLVL3 = new Telerik.Reporting.TextBox();
            this.shape1 = new Telerik.Reporting.Shape();
            this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
            this.AccountGroup_NameLVL3 = new Telerik.Reporting.TextBox();
            this.groupGeneralAccount = new Telerik.Reporting.Group();
            this.groupHeaderSection2 = new Telerik.Reporting.GroupHeaderSection();
            this.groupReportPLSection = new Telerik.Reporting.Group();
            this.groupFooterPLSection = new Telerik.Reporting.GroupFooterSection();
            this.ProfitLossSection = new Telerik.Reporting.TextBox();
            this.txtPrintDateTime = new Telerik.Reporting.TextBox();
            this.txtPageNumber = new Telerik.Reporting.TextBox();
            groupFooterSection2 = new Telerik.Reporting.GroupFooterSection();
            groupHeaderSection3 = new Telerik.Reporting.GroupHeaderSection();
            reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // groupFooterSection2
            // 
            groupFooterSection2.Height = Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672);
            groupFooterSection2.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1});
            groupFooterSection2.Name = "groupFooterSection2";
            groupFooterSection2.Style.Font.Bold = true;
            groupFooterSection2.Style.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Format = "{0:#,##0.00;(#,##0.00)}";
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5000003576278687), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox1.Value = "=sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcherForDisplay(Ac" +
                "countGroup,NormalBalance,FinalBalance))";
            // 
            // groupHeaderSection3
            // 
            groupHeaderSection3.Height = Telerik.Reporting.Drawing.Unit.Inch(0.299999862909317);
            groupHeaderSection3.Name = "groupHeaderSection3";
            groupHeaderSection3.Style.Visible = false;
            // 
            // reportFooterSection1
            // 
            reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.39992094039916992);
            reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtProfitLossPageFooter,
            this.TotalProfitLossSectionPageFooter});
            reportFooterSection1.Name = "reportFooterSection1";
            reportFooterSection1.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            reportFooterSection1.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1);
            reportFooterSection1.Style.Font.Bold = true;
            reportFooterSection1.Style.Visible = true;
            // 
            // txtProfitLossPageFooter
            // 
            this.txtProfitLossPageFooter.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579), Telerik.Reporting.Drawing.Unit.Inch(0.099960006773471832));
            this.txtProfitLossPageFooter.Name = "txtProfitLossPageFooter";
            this.txtProfitLossPageFooter.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.3999218940734863), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtProfitLossPageFooter.Style.Font.Bold = true;
            this.txtProfitLossPageFooter.Value = "PROFIT LOSS";
            // 
            // TotalProfitLossSectionPageFooter
            // 
            this.TotalProfitLossSectionPageFooter.Format = "{0:#,##0.00;(#,##0.00)}";
            this.TotalProfitLossSectionPageFooter.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.5), Telerik.Reporting.Drawing.Unit.Inch(0.099960006773471832));
            this.TotalProfitLossSectionPageFooter.Name = "TotalProfitLossSectionPageFooter";
            this.TotalProfitLossSectionPageFooter.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4957541227340698), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.TotalProfitLossSectionPageFooter.Style.BorderColor.Bottom = System.Drawing.Color.Silver;
            this.TotalProfitLossSectionPageFooter.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.TotalProfitLossSectionPageFooter.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TotalProfitLossSectionPageFooter.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TotalProfitLossSectionPageFooter.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.TotalProfitLossSectionPageFooter.Value = "=sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcher(AccountGroup" +
                ",NormalBalance,FinalBalance))";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.10003942251205444);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.SumFinalBalance,
            this.GeneralAccount,
            this.GeneralAccountName});
            this.detail.Name = "detail";
            this.detail.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.detail.Style.Visible = true;
            // 
            // SumFinalBalance
            // 
            this.SumFinalBalance.Format = "{0:#,##0.00;(#,##0.00)}";
            this.SumFinalBalance.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.SumFinalBalance.Name = "SumFinalBalance";
            this.SumFinalBalance.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5000003576278687), Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612));
            this.SumFinalBalance.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.SumFinalBalance.Value = "=Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcherForDisplay(Accoun" +
                "tGroup,NormalBalance,FinalBalance)";
            // 
            // GeneralAccount
            // 
            this.GeneralAccount.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0041667381301522255), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.GeneralAccount.Name = "GeneralAccount";
            this.GeneralAccount.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0957150459289551), Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612));
            this.GeneralAccount.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(2);
            this.GeneralAccount.Value = "=ChartOfAccountCode";
            // 
            // GeneralAccountName
            // 
            this.GeneralAccountName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.GeneralAccountName.Name = "GeneralAccountName";
            this.GeneralAccountName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.8999207019805908), Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612));
            this.GeneralAccountName.TextWrap = false;
            this.GeneralAccountName.Value = "=ChartOfAccountName";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(0.80000019073486328);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtReportTitle,
            this.txtCompanyName,
            this.txtFinalBalance,
            this.txtChartOfAccountName,
            this.txtChartOfAccountCode,
            this.txtPeriode});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.pageHeader.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.pageHeader.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1);
            // 
            // txtReportTitle
            // 
            this.txtReportTitle.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.099999986588954926));
            this.txtReportTitle.Name = "txtReportTitle";
            this.txtReportTitle.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.9998817443847656), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtReportTitle.Style.BorderColor.Bottom = System.Drawing.Color.Blue;
            this.txtReportTitle.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(119)))), ((int)(((byte)(171)))));
            this.txtReportTitle.Style.Font.Bold = true;
            this.txtReportTitle.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.txtReportTitle.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtReportTitle.Value = "INCOME STATEMENT - QUARTERLY";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134));
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.9998817443847656), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtCompanyName.Value = "";
            // 
            // txtFinalBalance
            // 
            this.txtFinalBalance.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.5), Telerik.Reporting.Drawing.Unit.Inch(0.60000008344650269));
            this.txtFinalBalance.Name = "txtFinalBalance";
            this.txtFinalBalance.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4957542419433594), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtFinalBalance.Style.Font.Bold = true;
            this.txtFinalBalance.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtFinalBalance.Value = "Balance";
            // 
            // txtChartOfAccountName
            // 
            this.txtChartOfAccountName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579), Telerik.Reporting.Drawing.Unit.Inch(0.60000008344650269));
            this.txtChartOfAccountName.Name = "txtChartOfAccountName";
            this.txtChartOfAccountName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.8999214172363281), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtChartOfAccountName.Style.Font.Bold = true;
            this.txtChartOfAccountName.Value = "Account Name";
            // 
            // txtChartOfAccountCode
            // 
            this.txtChartOfAccountCode.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0041667381301522255), Telerik.Reporting.Drawing.Unit.Inch(0.60000008344650269));
            this.txtChartOfAccountCode.Name = "txtChartOfAccountCode";
            this.txtChartOfAccountCode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.095714807510376), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtChartOfAccountCode.Style.Font.Bold = true;
            this.txtChartOfAccountCode.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(2);
            this.txtChartOfAccountCode.Value = "Account Code";
            // 
            // txtPeriode
            // 
            this.txtPeriode.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5), Telerik.Reporting.Drawing.Unit.Inch(0.099999986588954926));
            this.txtPeriode.Name = "txtPeriode";
            this.txtPeriode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.9999210834503174), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtPeriode.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtPeriode.Value = "";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.33341202139854431);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtPrintDateTime,
            this.txtPageNumber});
            this.pageFooter.Name = "pageFooter";
            this.pageFooter.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.pageFooter.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.pageFooter.Style.Font.Bold = true;
            this.pageFooter.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            // 
            // txtProfitLossSection
            // 
            this.txtProfitLossSection.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579), Telerik.Reporting.Drawing.Unit.Inch(0.033333301544189453));
            this.txtProfitLossSection.Name = "txtProfitLossSection";
            this.txtProfitLossSection.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.3999218940734863), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtProfitLossSection.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtProfitLossSection.Style.Visible = true;
            this.txtProfitLossSection.TextWrap = false;
            this.txtProfitLossSection.Value = "=ReportSectionName";
            // 
            // groupAccountGroup
            // 
            this.groupAccountGroup.GroupFooter = this.groupFooterSection1;
            this.groupAccountGroup.GroupHeader = this.groupHeaderSection1;
            this.groupAccountGroup.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("=AccountGroupLVL3")});
            this.groupAccountGroup.Name = "groupAccountGroup";
            // 
            // groupFooterSection1
            // 
            this.groupFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.26666703820228577);
            this.groupFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.TotalAccountGroup_NameLVL3,
            this.txtTotalAccountGroup_NameLVL3,
            this.shape1});
            this.groupFooterSection1.Name = "groupFooterSection1";
            this.groupFooterSection1.Style.Font.Bold = true;
            this.groupFooterSection1.Style.Visible = true;
            // 
            // TotalAccountGroup_NameLVL3
            // 
            this.TotalAccountGroup_NameLVL3.Format = "{0:#,##0.00;(#,##0.00)}";
            this.TotalAccountGroup_NameLVL3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.5), Telerik.Reporting.Drawing.Unit.Inch(0.064583458006381989));
            this.TotalAccountGroup_NameLVL3.Name = "TotalAccountGroup_NameLVL3";
            this.TotalAccountGroup_NameLVL3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4957541227340698), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.TotalAccountGroup_NameLVL3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.TotalAccountGroup_NameLVL3.Value = "=sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcherForDisplay(Ac" +
                "countGroup,NormalBalance,FinalBalance))";
            // 
            // txtTotalAccountGroup_NameLVL3
            // 
            this.txtTotalAccountGroup_NameLVL3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579), Telerik.Reporting.Drawing.Unit.Inch(0.064583458006381989));
            this.txtTotalAccountGroup_NameLVL3.Name = "txtTotalAccountGroup_NameLVL3";
            this.txtTotalAccountGroup_NameLVL3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.8999216556549072), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtTotalAccountGroup_NameLVL3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtTotalAccountGroup_NameLVL3.Value = "=\"TOTAL \" + AccountGroup_NameLVL3";
            // 
            // shape1
            // 
            this.shape1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5), Telerik.Reporting.Drawing.Unit.Inch(1.5894572413799324E-07));
            this.shape1.Name = "shape1";
            this.shape1.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4999216794967651), Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609));
            this.shape1.Style.Color = System.Drawing.Color.Silver;
            this.shape1.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1);
            // 
            // groupHeaderSection1
            // 
            this.groupHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672);
            this.groupHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.AccountGroup_NameLVL3});
            this.groupHeaderSection1.Name = "groupHeaderSection1";
            this.groupHeaderSection1.PrintOnEveryPage = true;
            this.groupHeaderSection1.Style.Font.Bold = true;
            // 
            // AccountGroup_NameLVL3
            // 
            this.AccountGroup_NameLVL3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0041667618788778782), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.AccountGroup_NameLVL3.Name = "AccountGroup_NameLVL3";
            this.AccountGroup_NameLVL3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.9957547187805176), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.AccountGroup_NameLVL3.Value = "=AccountGroup_NameLVL3";
            // 
            // groupGeneralAccount
            // 
            this.groupGeneralAccount.GroupFooter = groupFooterSection2;
            this.groupGeneralAccount.GroupHeader = this.groupHeaderSection2;
            this.groupGeneralAccount.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("=GeneralAccount")});
            this.groupGeneralAccount.Name = "groupGeneralAccount";
            this.groupGeneralAccount.Sortings.AddRange(new Telerik.Reporting.Sorting[] {
            new Telerik.Reporting.Sorting("=GeneralAccount", Telerik.Reporting.SortDirection.Asc)});
            // 
            // groupHeaderSection2
            // 
            this.groupHeaderSection2.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179);
            this.groupHeaderSection2.Name = "groupHeaderSection2";
            this.groupHeaderSection2.Style.Visible = false;
            // 
            // groupReportPLSection
            // 
            this.groupReportPLSection.GroupFooter = this.groupFooterPLSection;
            this.groupReportPLSection.GroupHeader = groupHeaderSection3;
            this.groupReportPLSection.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("=ReportSectionINT")});
            this.groupReportPLSection.Name = "groupReportPLSection";
            // 
            // groupFooterPLSection
            // 
            this.groupFooterPLSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.299999862909317);
            this.groupFooterPLSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtProfitLossSection,
            this.ProfitLossSection});
            this.groupFooterPLSection.Name = "groupFooterPLSection";
            this.groupFooterPLSection.Style.Font.Bold = true;
            // 
            // ProfitLossSection
            // 
            this.ProfitLossSection.Format = "{0:#,##0.00;(#,##0.00)}";
            this.ProfitLossSection.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.5), Telerik.Reporting.Drawing.Unit.Inch(0.033333141356706619));
            this.ProfitLossSection.Name = "ProfitLossSection";
            this.ProfitLossSection.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4957541227340698), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.ProfitLossSection.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.ProfitLossSection.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.ProfitLossSection.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.ProfitLossSection.Value = "=RunningValue(\'\', sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwit" +
                "cher(AccountGroup,NormalBalance,FinalBalance)))";
            // 
            // txtPrintDateTime
            // 
            this.txtPrintDateTime.Format = "Avicenna HIS, Print Date : {0:dd-MM-yyyy HH:mm}";
            this.txtPrintDateTime.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.066666670143604279));
            this.txtPrintDateTime.Name = "txtPrintDateTime";
            this.txtPrintDateTime.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.9956374168396), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtPrintDateTime.Style.Font.Bold = false;
            this.txtPrintDateTime.Style.Font.Italic = true;
            this.txtPrintDateTime.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtPrintDateTime.Value = "= Now()";
            // 
            // txtPageNumber
            // 
            this.txtPageNumber.Format = "";
            this.txtPageNumber.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5), Telerik.Reporting.Drawing.Unit.Inch(0.066666670143604279));
            this.txtPageNumber.Name = "txtPageNumber";
            this.txtPageNumber.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.9998822212219238), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtPageNumber.Style.Font.Bold = false;
            this.txtPageNumber.Style.Font.Italic = true;
            this.txtPageNumber.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtPageNumber.Value = "= \"Page \" + PageNumber + \" Of \" + PageCount";
            // 
            // ProfitLossQuarterlyReport
            // 
            this.DocumentName = "ProfitLoss Report";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            this.groupReportPLSection,
            this.groupAccountGroup,
            this.groupGeneralAccount});
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
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Inch(0.25);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Inch(0.25);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Inch(0.25);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Inch(0.25);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Sortings.AddRange(new Telerik.Reporting.Sorting[] {
            new Telerik.Reporting.Sorting("=AccountGroup", Telerik.Reporting.SortDirection.Asc),
            new Telerik.Reporting.Sorting("=ChartOfAccountCode", Telerik.Reporting.SortDirection.Asc)});
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.9999213218688965);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox txtReportTitle;
        private Telerik.Reporting.TextBox txtCompanyName;
        private Telerik.Reporting.TextBox txtFinalBalance;
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
        private Telerik.Reporting.Shape shape1;
        private Telerik.Reporting.TextBox txtProfitLossPageFooter;
        private Telerik.Reporting.TextBox TotalProfitLossSectionPageFooter;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox txtPrintDateTime;
        private Telerik.Reporting.TextBox txtPageNumber;
    }
}