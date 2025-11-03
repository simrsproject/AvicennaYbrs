namespace Temiang.Avicenna.ReportLibrary.Accounting.ProfitLoss
{
    partial class ProfitLossDetailReport
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
            this.txtProfitLossPageFooter = new Telerik.Reporting.TextBox();
            this.TotalProfitLossSectionPageFooter = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
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
            this.txtPageNumber = new Telerik.Reporting.TextBox();
            this.txtPrintDateTime = new Telerik.Reporting.TextBox();
            this.txtProfitLossSection = new Telerik.Reporting.TextBox();
            this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();
            this.TotalAccountGroup_NameLVL3 = new Telerik.Reporting.TextBox();
            this.txtTotalAccountGroup_NameLVL3 = new Telerik.Reporting.TextBox();
            this.shape1 = new Telerik.Reporting.Shape();
            this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
            this.AccountGroup_NameLVL3 = new Telerik.Reporting.TextBox();
            this.groupHeaderSection2 = new Telerik.Reporting.GroupHeaderSection();
            this.groupFooterPLSection = new Telerik.Reporting.GroupFooterSection();
            this.ProfitLossSection = new Telerik.Reporting.TextBox();
            groupFooterSection2 = new Telerik.Reporting.GroupFooterSection();
            groupHeaderSection3 = new Telerik.Reporting.GroupHeaderSection();
            reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // groupFooterSection2
            // 
            groupFooterSection2.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2D);
            groupFooterSection2.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1});
            groupFooterSection2.Name = "groupFooterSection2";
            groupFooterSection2.Style.Font.Bold = true;
            groupFooterSection2.Style.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Format = "{0:#,##0.00;(#,##0.00)}";
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox1.Value = "=sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcherForDisplay(Ac" +
    "countGroup,NormalBalance,FinalBalance))";
            // 
            // groupHeaderSection3
            // 
            groupHeaderSection3.Height = Telerik.Reporting.Drawing.Unit.Inch(0.3D);
            groupHeaderSection3.Name = "groupHeaderSection3";
            groupHeaderSection3.Style.Visible = false;
            // 
            // reportFooterSection1
            // 
            reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(1.923D);
            reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtProfitLossPageFooter,
            this.TotalProfitLossSectionPageFooter,
            this.textBox23,
            this.textBox22,
            this.textBox21,
            this.textBox20,
            this.textBox19,
            this.textBox18,
            this.textBox17,
            this.textBox16,
            this.textBox15});
            reportFooterSection1.Name = "reportFooterSection1";
            reportFooterSection1.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            reportFooterSection1.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            reportFooterSection1.Style.Font.Bold = true;
            reportFooterSection1.Style.Visible = true;
            // 
            // txtProfitLossPageFooter
            // 
            this.txtProfitLossPageFooter.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.txtProfitLossPageFooter.Name = "txtProfitLossPageFooter";
            this.txtProfitLossPageFooter.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.9D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtProfitLossPageFooter.Style.Font.Bold = true;
            this.txtProfitLossPageFooter.Style.Visible = true;
            this.txtProfitLossPageFooter.Value = "PROFIT LOSS";
            // 
            // TotalProfitLossSectionPageFooter
            // 
            this.TotalProfitLossSectionPageFooter.Format = "{0:#,##0.00;(#,##0.00)}";
            this.TotalProfitLossSectionPageFooter.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.5D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.TotalProfitLossSectionPageFooter.Name = "TotalProfitLossSectionPageFooter";
            this.TotalProfitLossSectionPageFooter.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.496D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.TotalProfitLossSectionPageFooter.Style.BorderColor.Bottom = System.Drawing.Color.Silver;
            this.TotalProfitLossSectionPageFooter.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.TotalProfitLossSectionPageFooter.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TotalProfitLossSectionPageFooter.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TotalProfitLossSectionPageFooter.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.TotalProfitLossSectionPageFooter.Style.Visible = true;
            this.TotalProfitLossSectionPageFooter.Value = "=sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcher(AccountGroup" +
    ",NormalBalance,FinalBalance))";
            // 
            // textBox23
            // 
            this.textBox23.Format = "{0:#,##0.00;(#,##0.00)}";
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.433D));
            this.textBox23.Name = "textBox1";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3D), Telerik.Reporting.Drawing.Unit.Inch(0.187D));
            this.textBox23.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.textBox23.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox23.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox23.Value = "Mengetahui,";
            // 
            // textBox22
            // 
            this.textBox22.Format = "{0:#,##0.00;(#,##0.00)}";
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(1.735D));
            this.textBox22.Name = "textBox1";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3D), Telerik.Reporting.Drawing.Unit.Inch(0.187D));
            this.textBox22.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.textBox22.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox22.Value = "=tdt1";
            // 
            // textBox21
            // 
            this.textBox21.Format = "{0:#,##0.00;(#,##0.00)}";
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(1.537D));
            this.textBox21.Name = "textBox1";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3D), Telerik.Reporting.Drawing.Unit.Inch(0.187D));
            this.textBox21.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.textBox21.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox21.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox21.Value = "=jbt1";
            // 
            // textBox20
            // 
            this.textBox20.Format = "{0:#,##0.00;(#,##0.00)}";
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3D), Telerik.Reporting.Drawing.Unit.Inch(0.433D));
            this.textBox20.Name = "textBox1";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.996D), Telerik.Reporting.Drawing.Unit.Inch(0.187D));
            this.textBox20.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.textBox20.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox20.Value = "Mengetahui,";
            // 
            // textBox19
            // 
            this.textBox19.Format = "{0:#,##0.00;(#,##0.00)}";
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3D), Telerik.Reporting.Drawing.Unit.Inch(1.735D));
            this.textBox19.Name = "textBox1";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.996D), Telerik.Reporting.Drawing.Unit.Inch(0.187D));
            this.textBox19.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.textBox19.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox19.Value = "=tdt2";
            // 
            // textBox18
            // 
            this.textBox18.Format = "{0:#,##0.00;(#,##0.00)}";
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3D), Telerik.Reporting.Drawing.Unit.Inch(1.537D));
            this.textBox18.Name = "textBox1";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.996D), Telerik.Reporting.Drawing.Unit.Inch(0.187D));
            this.textBox18.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.textBox18.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox18.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox18.Value = "=jbt2";
            // 
            // textBox17
            // 
            this.textBox17.Format = "{0:#,##0.00;(#,##0.00)}";
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.5D), Telerik.Reporting.Drawing.Unit.Inch(0.433D));
            this.textBox17.Name = "textBox1";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.496D), Telerik.Reporting.Drawing.Unit.Inch(0.187D));
            this.textBox17.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.textBox17.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox17.Value = "Penyusun,";
            // 
            // textBox16
            // 
            this.textBox16.Format = "{0:#,##0.00;(#,##0.00)}";
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.5D), Telerik.Reporting.Drawing.Unit.Inch(1.735D));
            this.textBox16.Name = "textBox1";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.496D), Telerik.Reporting.Drawing.Unit.Inch(0.187D));
            this.textBox16.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.textBox16.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox16.Value = "=tdt3";
            // 
            // textBox15
            // 
            this.textBox15.Format = "{0:#,##0.00;(#,##0.00)}";
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.5D), Telerik.Reporting.Drawing.Unit.Inch(1.537D));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.496D), Telerik.Reporting.Drawing.Unit.Inch(0.187D));
            this.textBox15.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.textBox15.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox15.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox15.Value = "=jbt3";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.1D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.SumFinalBalance,
            this.GeneralAccount,
            this.GeneralAccountName});
            this.detail.Name = "detail";
            this.detail.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.detail.Style.Visible = true;
            // 
            // SumFinalBalance
            // 
            this.SumFinalBalance.Format = "{0:#,##0.00;(#,##0.00)}";
            this.SumFinalBalance.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.SumFinalBalance.Name = "SumFinalBalance";
            this.SumFinalBalance.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.SumFinalBalance.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.SumFinalBalance.Value = "=Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcherForDisplay(Accoun" +
    "tGroup,NormalBalance,FinalBalance)";
            // 
            // GeneralAccount
            // 
            this.GeneralAccount.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.GeneralAccount.Name = "GeneralAccount";
            this.GeneralAccount.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.GeneralAccount.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(2D);
            this.GeneralAccount.Value = "=ChartOfAccountCode";
            // 
            // GeneralAccountName
            // 
            this.GeneralAccountName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.GeneralAccountName.Name = "GeneralAccountName";
            this.GeneralAccountName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.9D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.GeneralAccountName.TextWrap = false;
            this.GeneralAccountName.Value = "=ChartOfAccountName";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(0.8D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtReportTitle,
            this.txtCompanyName,
            this.txtFinalBalance,
            this.txtChartOfAccountName,
            this.txtChartOfAccountCode,
            this.txtPeriode});
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
            this.txtReportTitle.Value = "INCOME STATEMENT";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.3D));
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtCompanyName.Value = "";
            // 
            // txtFinalBalance
            // 
            this.txtFinalBalance.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.5D), Telerik.Reporting.Drawing.Unit.Inch(0.6D));
            this.txtFinalBalance.Name = "txtFinalBalance";
            this.txtFinalBalance.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.496D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtFinalBalance.Style.Font.Bold = true;
            this.txtFinalBalance.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtFinalBalance.Value = "Balance";
            // 
            // txtChartOfAccountName
            // 
            this.txtChartOfAccountName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1D), Telerik.Reporting.Drawing.Unit.Inch(0.6D));
            this.txtChartOfAccountName.Name = "txtChartOfAccountName";
            this.txtChartOfAccountName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.9D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtChartOfAccountName.Style.Font.Bold = true;
            this.txtChartOfAccountName.Value = "Account Name";
            // 
            // txtChartOfAccountCode
            // 
            this.txtChartOfAccountCode.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.6D));
            this.txtChartOfAccountCode.Name = "txtChartOfAccountCode";
            this.txtChartOfAccountCode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtChartOfAccountCode.Style.Font.Bold = true;
            this.txtChartOfAccountCode.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(2D);
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
            this.txtPageNumber.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5D), Telerik.Reporting.Drawing.Unit.Inch(0.067D));
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
            this.txtPrintDateTime.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.067D));
            this.txtPrintDateTime.Name = "txtPrintDateTime";
            this.txtPrintDateTime.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.996D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtPrintDateTime.Style.Font.Bold = false;
            this.txtPrintDateTime.Style.Font.Italic = true;
            this.txtPrintDateTime.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtPrintDateTime.Value = "= Now()";
            // 
            // txtProfitLossSection
            // 
            this.txtProfitLossSection.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1D), Telerik.Reporting.Drawing.Unit.Inch(0.033D));
            this.txtProfitLossSection.Name = "txtProfitLossSection";
            this.txtProfitLossSection.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.4D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
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
            this.shape1});
            this.groupFooterSection1.Name = "groupFooterSection1";
            this.groupFooterSection1.Style.Font.Bold = true;
            this.groupFooterSection1.Style.Visible = true;
            // 
            // TotalAccountGroup_NameLVL3
            // 
            this.TotalAccountGroup_NameLVL3.Format = "{0:#,##0.00;(#,##0.00)}";
            this.TotalAccountGroup_NameLVL3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.5D), Telerik.Reporting.Drawing.Unit.Inch(0.065D));
            this.TotalAccountGroup_NameLVL3.Name = "TotalAccountGroup_NameLVL3";
            this.TotalAccountGroup_NameLVL3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.496D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.TotalAccountGroup_NameLVL3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.TotalAccountGroup_NameLVL3.Value = "=sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwitcherForDisplay(Ac" +
    "countGroup,NormalBalance,FinalBalance))";
            // 
            // txtTotalAccountGroup_NameLVL3
            // 
            this.txtTotalAccountGroup_NameLVL3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1D), Telerik.Reporting.Drawing.Unit.Inch(0.065D));
            this.txtTotalAccountGroup_NameLVL3.Name = "txtTotalAccountGroup_NameLVL3";
            this.txtTotalAccountGroup_NameLVL3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.9D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtTotalAccountGroup_NameLVL3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtTotalAccountGroup_NameLVL3.Value = "=\"TOTAL \" + AccountGroup_NameLVL3";
            // 
            // shape1
            // 
            this.shape1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.shape1.Name = "shape1";
            this.shape1.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.shape1.Style.Color = System.Drawing.Color.Silver;
            this.shape1.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
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
            this.ProfitLossSection});
            this.groupFooterPLSection.Name = "groupFooterPLSection";
            this.groupFooterPLSection.Style.Font.Bold = true;
            // 
            // ProfitLossSection
            // 
            this.ProfitLossSection.Format = "{0:#,##0.00;(#,##0.00)}";
            this.ProfitLossSection.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.5D), Telerik.Reporting.Drawing.Unit.Inch(0.033D));
            this.ProfitLossSection.Name = "ProfitLossSection";
            this.ProfitLossSection.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.496D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.ProfitLossSection.Style.BorderColor.Top = System.Drawing.Color.Silver;
            this.ProfitLossSection.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.ProfitLossSection.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.ProfitLossSection.Value = "=RunningValue(\'\', sum(Temiang.Avicenna.ReportLibrary.Utility.ProfitLossAmountSwit" +
    "cher(AccountGroup,NormalBalance,FinalBalance)))";
            // 
            // ProfitLossDetailReport
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
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox txtPageNumber;
        private Telerik.Reporting.TextBox txtPrintDateTime;
    }
}