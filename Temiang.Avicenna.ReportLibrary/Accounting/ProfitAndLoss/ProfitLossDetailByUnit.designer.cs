namespace Temiang.Avicenna.ReportLibrary.Accounting.ProfitAndLoss
{
    partial class ProfitLossDetailByUnit
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.DetailSection detail;
            Telerik.Reporting.GroupFooterSection groupFooterSection2;
            Telerik.Reporting.GroupHeaderSection groupHeaderSection3;
            Telerik.Reporting.ReportFooterSection reportFooterSection1;
            this.GeneralAccountName = new Telerik.Reporting.TextBox();
            this.GeneralAccount = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.SumFinalBalance = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.txtProfitLossPageFooter = new Telerik.Reporting.TextBox();
            this.TotalProfitLossSectionPageFooter = new Telerik.Reporting.TextBox();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.txtReportTitle = new Telerik.Reporting.TextBox();
            this.txtCompanyName = new Telerik.Reporting.TextBox();
            this.txtPeriode = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.txtPrintDateTime = new Telerik.Reporting.TextBox();
            this.txtPageNumber = new Telerik.Reporting.TextBox();
            this.txtProfitLossSection = new Telerik.Reporting.TextBox();
            this.groupAccountGroup = new Telerik.Reporting.Group();
            this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();
            this.TotalAccountGroup_NameLVL3 = new Telerik.Reporting.TextBox();
            this.txtTotalAccountGroup_NameLVL3 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
            this.AccountGroup_NameLVL3 = new Telerik.Reporting.TextBox();
            this.groupGeneralAccount = new Telerik.Reporting.Group();
            this.groupHeaderSection2 = new Telerik.Reporting.GroupHeaderSection();
            this.groupReportPLSection = new Telerik.Reporting.Group();
            this.groupFooterPLSection = new Telerik.Reporting.GroupFooterSection();
            this.ProfitLossSection = new Telerik.Reporting.TextBox();
            this.group1 = new Telerik.Reporting.Group();
            this.groupFooterSection3 = new Telerik.Reporting.GroupFooterSection();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.groupHeaderSection4 = new Telerik.Reporting.GroupHeaderSection();
            this.txtChartOfAccountCode = new Telerik.Reporting.TextBox();
            this.txtChartOfAccountName = new Telerik.Reporting.TextBox();
            this.txtFinalBalance = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            detail = new Telerik.Reporting.DetailSection();
            groupFooterSection2 = new Telerik.Reporting.GroupFooterSection();
            groupHeaderSection3 = new Telerik.Reporting.GroupHeaderSection();
            reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612);
            detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.GeneralAccountName,
            this.GeneralAccount,
            this.textBox6});
            detail.Name = "detail";
            detail.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            detail.Style.Visible = true;
            // 
            // GeneralAccountName
            // 
            this.GeneralAccountName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1001187562942505), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.GeneralAccountName.Name = "GeneralAccountName";
            this.GeneralAccountName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.8999207019805908), Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612));
            this.GeneralAccountName.TextWrap = false;
            this.GeneralAccountName.Value = "=ChartOfAccountName";
            // 
            // GeneralAccount
            // 
            this.GeneralAccount.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0042856005020439625), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.GeneralAccount.Name = "GeneralAccount";
            this.GeneralAccount.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0957543849945068), Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612));
            this.GeneralAccount.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(2);
            this.GeneralAccount.Value = "=ChartOfAccountCode";
            // 
            // textBox6
            // 
            this.textBox6.Format = "{0:#,##0.00;(#,##0.00)}";
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.0044021606445312), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.299842119216919), Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612));
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox6.Value = "=(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcherForDisplay(Accou" +
                "ntGroup,NormalBalance,FinalBalance))";
            // 
            // groupFooterSection2
            // 
            groupFooterSection2.Height = Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672);
            groupFooterSection2.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.SumFinalBalance,
            this.textBox7,
            this.textBox8});
            groupFooterSection2.Name = "groupFooterSection2";
            groupFooterSection2.Style.Visible = true;
            // 
            // SumFinalBalance
            // 
            this.SumFinalBalance.Format = "{0:#,##0.00;(#,##0.00)}";
            this.SumFinalBalance.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.0044021606445312), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.SumFinalBalance.Name = "SumFinalBalance";
            this.SumFinalBalance.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.299842119216919), Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612));
            this.SumFinalBalance.Style.Font.Bold = true;
            this.SumFinalBalance.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.SumFinalBalance.Value = "=sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcherForDisplay(Ac" +
                "countGroup,NormalBalance,FinalBalance))";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000001430511475), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.8999207019805908), Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612));
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.TextWrap = false;
            this.textBox7.Value = "=GeneralAccountName";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0999999046325684), Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612));
            this.textBox8.Style.Font.Bold = true;
            this.textBox8.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(2);
            this.textBox8.TextWrap = false;
            this.textBox8.Value = "=GeneralAccount";
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
            this.txtProfitLossPageFooter.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.8999207019805908), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtProfitLossPageFooter.Style.Font.Bold = true;
            this.txtProfitLossPageFooter.Value = "PROFIT LOSS";
            // 
            // TotalProfitLossSectionPageFooter
            // 
            this.TotalProfitLossSectionPageFooter.Format = "{0:#,##0.00;(#,##0.00)}";
            this.TotalProfitLossSectionPageFooter.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.3000001907348633), Telerik.Reporting.Drawing.Unit.Inch(0.099960006773471832));
            this.TotalProfitLossSectionPageFooter.Name = "TotalProfitLossSectionPageFooter";
            this.TotalProfitLossSectionPageFooter.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.6957541704177856), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.TotalProfitLossSectionPageFooter.Style.BorderColor.Bottom = System.Drawing.Color.Silver;
            this.TotalProfitLossSectionPageFooter.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.TotalProfitLossSectionPageFooter.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TotalProfitLossSectionPageFooter.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TotalProfitLossSectionPageFooter.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.TotalProfitLossSectionPageFooter.Value = "=sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcher(AccountGroup" +
                ",NormalBalance,FinalBalance))";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(0.59999996423721313);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtReportTitle,
            this.txtCompanyName,
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
            this.txtReportTitle.Value = "=\'INCOME STATEMENT  \' + ReportName";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134));
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.9998817443847656), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtCompanyName.Value = "";
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
            // txtPrintDateTime
            // 
            this.txtPrintDateTime.Format = "Avicenna HIS, Print Date : {0:dd-MM-yyyy HH:mm}";
            this.txtPrintDateTime.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.00440216064453125), Telerik.Reporting.Drawing.Unit.Inch(0.10007908940315247));
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
            this.txtPageNumber.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.0044021606445312), Telerik.Reporting.Drawing.Unit.Inch(0.10007908940315247));
            this.txtPageNumber.Name = "txtPageNumber";
            this.txtPageNumber.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.9998822212219238), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtPageNumber.Style.Font.Bold = false;
            this.txtPageNumber.Style.Font.Italic = true;
            this.txtPageNumber.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtPageNumber.Value = "= \"Page \" + PageNumber + \" Of \" + PageCount";
            // 
            // txtProfitLossSection
            // 
            this.txtProfitLossSection.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579), Telerik.Reporting.Drawing.Unit.Inch(0.033333301544189453));
            this.txtProfitLossSection.Name = "txtProfitLossSection";
            this.txtProfitLossSection.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.1998419761657715), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
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
            this.textBox5});
            this.groupFooterSection1.Name = "groupFooterSection1";
            this.groupFooterSection1.Style.Font.Bold = true;
            this.groupFooterSection1.Style.Visible = true;
            // 
            // TotalAccountGroup_NameLVL3
            // 
            this.TotalAccountGroup_NameLVL3.Format = "{0:#,##0.00;(#,##0.00)}";
            this.TotalAccountGroup_NameLVL3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.3000001907348633), Telerik.Reporting.Drawing.Unit.Inch(0.064583458006381989));
            this.TotalAccountGroup_NameLVL3.Name = "TotalAccountGroup_NameLVL3";
            this.TotalAccountGroup_NameLVL3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.6957541704177856), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.TotalAccountGroup_NameLVL3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.TotalAccountGroup_NameLVL3.Value = "=sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcherForDisplay(Ac" +
                "countGroup,NormalBalance,FinalBalance))";
            // 
            // txtTotalAccountGroup_NameLVL3
            // 
            this.txtTotalAccountGroup_NameLVL3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579), Telerik.Reporting.Drawing.Unit.Inch(0.064583458006381989));
            this.txtTotalAccountGroup_NameLVL3.Name = "txtTotalAccountGroup_NameLVL3";
            this.txtTotalAccountGroup_NameLVL3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.9000000953674316), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtTotalAccountGroup_NameLVL3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtTotalAccountGroup_NameLVL3.Value = "=\"TOTAL \" + AccountGroup_NameLVL3";
            // 
            // textBox5
            // 
            this.textBox5.Format = "{0:#,##0.00;(#,##0.00)}";
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5), Telerik.Reporting.Drawing.Unit.Inch(0.064583331346511841));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3000000715255737), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox5.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.textBox5.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox5.Value = "";
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
            this.groupHeaderSection2.Height = Telerik.Reporting.Drawing.Unit.Inch(0.10003942251205444);
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
            this.ProfitLossSection.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.3000001907348633), Telerik.Reporting.Drawing.Unit.Inch(0.033333141356706619));
            this.ProfitLossSection.Name = "ProfitLossSection";
            this.ProfitLossSection.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.6957541704177856), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.ProfitLossSection.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.ProfitLossSection.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.ProfitLossSection.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.ProfitLossSection.Value = "=RunningValue(\'\', sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwit" +
                "cher(AccountGroup,NormalBalance,FinalBalance)))";
            // 
            // group1
            // 
            this.group1.GroupFooter = this.groupFooterSection3;
            this.group1.GroupHeader = this.groupHeaderSection4;
            this.group1.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("=SubledgerName")});
            this.group1.Name = "group1";
            this.group1.Sortings.AddRange(new Telerik.Reporting.Sorting[] {
            new Telerik.Reporting.Sorting("=SubledgerName", Telerik.Reporting.SortDirection.Asc)});
            // 
            // groupFooterSection3
            // 
            this.groupFooterSection3.Height = Telerik.Reporting.Drawing.Unit.Inch(0.23329417407512665);
            this.groupFooterSection3.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox3,
            this.textBox4});
            this.groupFooterSection3.Name = "groupFooterSection3";
            // 
            // textBox3
            // 
            this.textBox3.Format = "{0:#,##0.00;(#,##0.00)}";
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.3042459487915039), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox3.Name = "TotalProfitLossSectionPageFooter";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.6957541704177856), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox3.Style.BorderColor.Bottom = System.Drawing.Color.Silver;
            this.textBox3.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.textBox3.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox3.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox3.Style.Color = System.Drawing.Color.DarkBlue;
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox3.Value = "=sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcher(AccountGroup" +
                ",NormalBalance,FinalBalance))";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000009775161743), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox4.Name = "txtProfitLossPageFooter";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.8999207019805908), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox4.Style.Color = System.Drawing.Color.DarkBlue;
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Value = "= \'PROFIT LOSS PER \' + SubledgerName";
            // 
            // groupHeaderSection4
            // 
            this.groupHeaderSection4.Height = Telerik.Reporting.Drawing.Unit.Inch(0.3999999463558197);
            this.groupHeaderSection4.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtChartOfAccountCode,
            this.txtChartOfAccountName,
            this.txtFinalBalance,
            this.textBox2});
            this.groupHeaderSection4.Name = "groupHeaderSection4";
            this.groupHeaderSection4.PrintOnEveryPage = true;
            // 
            // txtChartOfAccountCode
            // 
            this.txtChartOfAccountCode.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.20011837780475617));
            this.txtChartOfAccountCode.Name = "txtChartOfAccountCode";
            this.txtChartOfAccountCode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1000001430511475), Telerik.Reporting.Drawing.Unit.Inch(0.19988155364990234));
            this.txtChartOfAccountCode.Style.Font.Bold = true;
            this.txtChartOfAccountCode.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(2);
            this.txtChartOfAccountCode.Value = "Account Code";
            // 
            // txtChartOfAccountName
            // 
            this.txtChartOfAccountName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000406742095947), Telerik.Reporting.Drawing.Unit.Inch(0.20011837780475617));
            this.txtChartOfAccountName.Name = "txtChartOfAccountName";
            this.txtChartOfAccountName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.899998664855957), Telerik.Reporting.Drawing.Unit.Inch(0.19988155364990234));
            this.txtChartOfAccountName.Style.Font.Bold = true;
            this.txtChartOfAccountName.Value = "Account Name";
            // 
            // txtFinalBalance
            // 
            this.txtFinalBalance.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.5), Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672));
            this.txtFinalBalance.Name = "txtFinalBalance";
            this.txtFinalBalance.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5000002384185791), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtFinalBalance.Style.Font.Bold = true;
            this.txtFinalBalance.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtFinalBalance.Value = "Balance";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.00440216064453125), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox2.Name = "txtChartOfAccountName";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.99135160446167), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox2.Style.Color = System.Drawing.Color.DarkBlue;
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "=\'Unit : \' + SubledgerName";
            // 
            // ProfitLossDetailByUnit
            // 
            this.DocumentName = "Profit Loss Detail By Unit Report";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            this.group1,
            this.groupReportPLSection,
            this.groupAccountGroup,
            this.groupGeneralAccount});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection4,
            this.groupFooterSection3,
            groupHeaderSection3,
            this.groupFooterPLSection,
            this.groupHeaderSection1,
            this.groupFooterSection1,
            this.groupHeaderSection2,
            groupFooterSection2,
            this.pageHeader,
            detail,
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
            new Telerik.Reporting.Sorting("=AccountGroup", Telerik.Reporting.SortDirection.Asc)});
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(8.0042848587036133);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox txtReportTitle;
        private Telerik.Reporting.TextBox txtCompanyName;
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
        private Telerik.Reporting.Group group1;
        private Telerik.Reporting.GroupFooterSection groupFooterSection3;
        private Telerik.Reporting.GroupHeaderSection groupHeaderSection4;
        private Telerik.Reporting.TextBox txtChartOfAccountCode;
        private Telerik.Reporting.TextBox txtChartOfAccountName;
        private Telerik.Reporting.TextBox txtFinalBalance;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox txtPrintDateTime;
        private Telerik.Reporting.TextBox txtPageNumber;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
    }
}