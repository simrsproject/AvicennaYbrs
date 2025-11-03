namespace Temiang.Avicenna.ReportLibrary.Accounting.Cash
{
    partial class CashFlowIndirectDetailReport
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.ReportFooterSection reportFooterSection1;
            Telerik.Reporting.Drawing.FormattingRule formattingRule1 = new Telerik.Reporting.Drawing.FormattingRule();
            Telerik.Reporting.Drawing.FormattingRule formattingRule2 = new Telerik.Reporting.Drawing.FormattingRule();
            Telerik.Reporting.Drawing.FormattingRule formattingRule3 = new Telerik.Reporting.Drawing.FormattingRule();
            Telerik.Reporting.Drawing.FormattingRule formattingRule4 = new Telerik.Reporting.Drawing.FormattingRule();
            this.shape5 = new Telerik.Reporting.Shape();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.txtTotalBeginingBalance = new Telerik.Reporting.TextBox();
            this.txtTotalEndingBalance = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
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
            this.grpActivity = new Telerik.Reporting.Group();
            this.gfsActivities = new Telerik.Reporting.GroupFooterSection();
            this.txtTotalActivity = new Telerik.Reporting.TextBox();
            this.txtTotalOfActivity = new Telerik.Reporting.TextBox();
            this.ghsActivity = new Telerik.Reporting.GroupHeaderSection();
            this.txtActivity = new Telerik.Reporting.TextBox();
            this.grpNormalBalance = new Telerik.Reporting.Group();
            this.gfsNormalBalance = new Telerik.Reporting.GroupFooterSection();
            this.txtTotalNormalBalance = new Telerik.Reporting.TextBox();
            this.txtTotalOfNormalBalance = new Telerik.Reporting.TextBox();
            this.ghsNormalBalance = new Telerik.Reporting.GroupHeaderSection();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.grpCategory = new Telerik.Reporting.Group();
            this.gfsCategory = new Telerik.Reporting.GroupFooterSection();
            this.txtTotalCategory = new Telerik.Reporting.TextBox();
            this.txtTotalOfCategory = new Telerik.Reporting.TextBox();
            this.ghsCategory = new Telerik.Reporting.GroupHeaderSection();
            this.textBox6 = new Telerik.Reporting.TextBox();
            reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // reportFooterSection1
            // 
            reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.90000039339065552);
            reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.shape5,
            this.textBox1,
            this.textBox7,
            this.textBox8,
            this.textBox9,
            this.txtTotalBeginingBalance,
            this.txtTotalEndingBalance});
            reportFooterSection1.Name = "reportFooterSection1";
            reportFooterSection1.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            reportFooterSection1.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1);
            reportFooterSection1.Style.Font.Bold = true;
            reportFooterSection1.Style.Visible = true;
            // 
            // shape5
            // 
            this.shape5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.shape5.Name = "shape5";
            this.shape5.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(8), Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609));
            this.shape5.Style.Color = System.Drawing.Color.Silver;
            this.shape5.Style.LineStyle = Telerik.Reporting.Drawing.LineStyle.Dotted;
            this.shape5.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1);
            // 
            // textBox1
            // 
            this.textBox1.Format = "{0:#,##0.00;(#,##0.00)}";
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.5042462348937988), Telerik.Reporting.Drawing.Unit.Inch(0.10007879137992859));
            this.textBox1.Name = "txtTotalActivity";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4957536458969116), Telerik.Reporting.Drawing.Unit.Inch(0.19996070861816406));
            this.textBox1.Style.BorderColor.Top = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(160)))));
            this.textBox1.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox1.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(160)))));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox1.Value = "=sum(Balance)";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.10007879137992859));
            this.textBox7.Name = "txtTotalOfActivity";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.4998812675476074), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox7.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(160)))));
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox7.Value = "=\"Total Of  Net Cash Provide (Used) in This Periode:\"";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.300157755613327));
            this.textBox8.Name = "txtTotalOfActivity";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.4998812675476074), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox8.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(160)))));
            this.textBox8.Style.Font.Bold = true;
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox8.Value = "=\"Total Of  Cash & Cash Equivalent at Begining Of Period:\"";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.50023674964904785));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.4998812675476074), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox9.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(160)))));
            this.textBox9.Style.Font.Bold = true;
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox9.Value = "=\"Total Of  Cash & Cash Equivalent at End Of Period:\"";
            // 
            // txtTotalBeginingBalance
            // 
            this.txtTotalBeginingBalance.Format = "{0:#,##0.00;(#,##0.00)}";
            this.txtTotalBeginingBalance.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.5042462348937988), Telerik.Reporting.Drawing.Unit.Inch(0.30019718408584595));
            this.txtTotalBeginingBalance.Name = "txtTotalBeginingBalance";
            this.txtTotalBeginingBalance.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4957536458969116), Telerik.Reporting.Drawing.Unit.Inch(0.19996070861816406));
            this.txtTotalBeginingBalance.Style.BorderColor.Top = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(160)))));
            this.txtTotalBeginingBalance.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtTotalBeginingBalance.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(160)))));
            this.txtTotalBeginingBalance.Style.Font.Bold = true;
            this.txtTotalBeginingBalance.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtTotalBeginingBalance.Value = "";
            // 
            // txtTotalEndingBalance
            // 
            this.txtTotalEndingBalance.Format = "{0:#,##0.00;(#,##0.00)}";
            this.txtTotalEndingBalance.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.5042462348937988), Telerik.Reporting.Drawing.Unit.Inch(0.50027614831924438));
            this.txtTotalEndingBalance.Name = "txtTotalEndingBalance";
            this.txtTotalEndingBalance.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4957536458969116), Telerik.Reporting.Drawing.Unit.Inch(0.19996070861816406));
            this.txtTotalEndingBalance.Style.BorderColor.Top = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(160)))));
            this.txtTotalEndingBalance.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtTotalEndingBalance.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(160)))));
            this.txtTotalEndingBalance.Style.Font.Bold = true;
            this.txtTotalEndingBalance.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtTotalEndingBalance.Value = "";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox3,
            this.textBox4,
            this.textBox5});
            this.detail.Name = "detail";
            this.detail.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.detail.Style.Visible = true;
            // 
            // textBox3
            // 
            this.textBox3.Format = "{0:#,##0.00;(#,##0.00)}";
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.5), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4957541227340698), Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612));
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox3.Value = "=Balance";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.4000000953674316), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.0999207496643066), Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612));
            this.textBox4.TextWrap = false;
            this.textBox4.Value = "=ChartOfAccountName";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.951390681322664E-05), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3998818397521973), Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612));
            this.textBox5.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(4);
            this.textBox5.Value = "=ChartOfAccountCode";
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
            this.txtReportTitle.Value = "CASH FLOW REPORT DETAIL - (INDIRECT METHOD) ";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134));
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.9999604225158691), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtCompanyName.Value = "";
            // 
            // txtFinalBalance
            // 
            this.txtFinalBalance.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.5), Telerik.Reporting.Drawing.Unit.Inch(0.60000008344650269));
            this.txtFinalBalance.Name = "txtFinalBalance";
            this.txtFinalBalance.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4957542419433594), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtFinalBalance.Style.Font.Bold = true;
            this.txtFinalBalance.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtFinalBalance.Style.Visible = false;
            this.txtFinalBalance.Value = "Balance";
            // 
            // txtChartOfAccountName
            // 
            this.txtChartOfAccountName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.4000000953674316), Telerik.Reporting.Drawing.Unit.Inch(0.60000008344650269));
            this.txtChartOfAccountName.Name = "txtChartOfAccountName";
            this.txtChartOfAccountName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.0999212265014648), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtChartOfAccountName.Style.Font.Bold = true;
            this.txtChartOfAccountName.Value = "Account Name";
            // 
            // txtChartOfAccountCode
            // 
            this.txtChartOfAccountCode.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.60000008344650269));
            this.txtChartOfAccountCode.Name = "txtChartOfAccountCode";
            this.txtChartOfAccountCode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3999606370925903), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtChartOfAccountCode.Style.Font.Bold = true;
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
            this.txtPageNumber,
            this.txtPrintDateTime});
            this.pageFooter.Name = "pageFooter";
            this.pageFooter.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.pageFooter.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.pageFooter.Style.Font.Bold = true;
            this.pageFooter.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            // 
            // txtPageNumber
            // 
            this.txtPageNumber.Format = "";
            this.txtPageNumber.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.5), Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609));
            this.txtPageNumber.Name = "txtPageNumber";
            this.txtPageNumber.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4999215602874756), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtPageNumber.Style.Font.Bold = false;
            this.txtPageNumber.Style.Font.Italic = true;
            this.txtPageNumber.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtPageNumber.Value = "=\'Page : \' + PageNumber + \'/\' + PageCount";
            // 
            // txtPrintDateTime
            // 
            this.txtPrintDateTime.Format = "Avicenna HIS, Print Date : {0:dd-MM-yyyy HH:mm}";
            this.txtPrintDateTime.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.10000014305114746));
            this.txtPrintDateTime.Name = "txtPrintDateTime";
            this.txtPrintDateTime.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.499880313873291), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtPrintDateTime.Style.Font.Bold = false;
            this.txtPrintDateTime.Style.Font.Italic = true;
            this.txtPrintDateTime.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtPrintDateTime.Value = "= Now()";
            // 
            // grpActivity
            // 
            this.grpActivity.GroupFooter = this.gfsActivities;
            this.grpActivity.GroupHeader = this.ghsActivity;
            this.grpActivity.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("=ActivityId")});
            this.grpActivity.Name = "grpActivity";
            // 
            // gfsActivities
            // 
            this.gfsActivities.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2000393271446228);
            this.gfsActivities.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtTotalActivity,
            this.txtTotalOfActivity});
            this.gfsActivities.Name = "gfsActivities";
            // 
            // txtTotalActivity
            // 
            this.txtTotalActivity.Format = "{0:#,##0.00;(#,##0.00)}";
            this.txtTotalActivity.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.5), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.txtTotalActivity.Name = "txtTotalActivity";
            this.txtTotalActivity.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4957536458969116), Telerik.Reporting.Drawing.Unit.Inch(0.19996070861816406));
            this.txtTotalActivity.Style.BorderColor.Top = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(160)))));
            this.txtTotalActivity.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtTotalActivity.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(160)))));
            this.txtTotalActivity.Style.Font.Bold = true;
            this.txtTotalActivity.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtTotalActivity.Value = "=sum(Balance)";
            // 
            // txtTotalOfActivity
            // 
            this.txtTotalOfActivity.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.txtTotalOfActivity.Name = "txtTotalOfActivity";
            this.txtTotalOfActivity.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.4999613761901855), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtTotalOfActivity.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(160)))));
            this.txtTotalOfActivity.Style.Font.Bold = true;
            this.txtTotalOfActivity.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtTotalOfActivity.Value = "=\"Total Of \" + Activities";
            // 
            // ghsActivity
            // 
            this.ghsActivity.Height = Telerik.Reporting.Drawing.Unit.Inch(0.299999862909317);
            this.ghsActivity.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtActivity});
            this.ghsActivity.Name = "ghsActivity";
            // 
            // txtActivity
            // 
            this.txtActivity.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.099999904632568359));
            this.txtActivity.Name = "txtActivity";
            this.txtActivity.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.4999208450317383), Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612));
            this.txtActivity.Value = "=Activities";
            // 
            // grpNormalBalance
            // 
            this.grpNormalBalance.GroupFooter = this.gfsNormalBalance;
            this.grpNormalBalance.GroupHeader = this.ghsNormalBalance;
            this.grpNormalBalance.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("=NormalBalance")});
            this.grpNormalBalance.Name = "grpNormalBalance";
            // 
            // gfsNormalBalance
            // 
            formattingRule1.Filters.AddRange(new Telerik.Reporting.Filter[] {
            new Telerik.Reporting.Filter("=RowID", Telerik.Reporting.FilterOperator.Equal, "=1")});
            formattingRule1.Style.Visible = false;
            this.gfsNormalBalance.ConditionalFormatting.AddRange(new Telerik.Reporting.Drawing.FormattingRule[] {
            formattingRule1});
            this.gfsNormalBalance.Height = Telerik.Reporting.Drawing.Unit.Inch(0.19992096722126007);
            this.gfsNormalBalance.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtTotalNormalBalance,
            this.txtTotalOfNormalBalance});
            this.gfsNormalBalance.Name = "gfsNormalBalance";
            // 
            // txtTotalNormalBalance
            // 
            this.txtTotalNormalBalance.Format = "{0:#,##0.00;(#,##0.00)}";
            this.txtTotalNormalBalance.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.5), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.txtTotalNormalBalance.Name = "txtTotalNormalBalance";
            this.txtTotalNormalBalance.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4957536458969116), Telerik.Reporting.Drawing.Unit.Inch(0.19996070861816406));
            this.txtTotalNormalBalance.Style.BorderColor.Top = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(160)))));
            this.txtTotalNormalBalance.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtTotalNormalBalance.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(160)))));
            this.txtTotalNormalBalance.Style.Font.Bold = true;
            this.txtTotalNormalBalance.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtTotalNormalBalance.Value = "=sum(Balance)";
            // 
            // txtTotalOfNormalBalance
            // 
            this.txtTotalOfNormalBalance.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.txtTotalOfNormalBalance.Name = "txtTotalOfNormalBalance";
            this.txtTotalOfNormalBalance.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.4999217987060547), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtTotalOfNormalBalance.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(160)))));
            this.txtTotalOfNormalBalance.Style.Font.Bold = true;
            this.txtTotalOfNormalBalance.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(2);
            this.txtTotalOfNormalBalance.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtTotalOfNormalBalance.Value = "=\"Total Of \" + NormalBalance";
            // 
            // ghsNormalBalance
            // 
            formattingRule2.Filters.AddRange(new Telerik.Reporting.Filter[] {
            new Telerik.Reporting.Filter("=RowId", Telerik.Reporting.FilterOperator.Equal, "=1")});
            formattingRule2.Style.Visible = false;
            this.ghsNormalBalance.ConditionalFormatting.AddRange(new Telerik.Reporting.Drawing.FormattingRule[] {
            formattingRule2});
            this.ghsNormalBalance.Height = Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612);
            this.ghsNormalBalance.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox2});
            this.ghsNormalBalance.Name = "ghsNormalBalance";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(-1.5894572769070692E-08), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox2.Name = "textBox5";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.4999208450317383), Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612));
            this.textBox2.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(2);
            this.textBox2.Value = "=NormalBalance";
            // 
            // grpCategory
            // 
            this.grpCategory.GroupFooter = this.gfsCategory;
            this.grpCategory.GroupHeader = this.ghsCategory;
            this.grpCategory.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("=CategoryId")});
            this.grpCategory.Name = "grpCategory";
            // 
            // gfsCategory
            // 
            formattingRule3.Filters.AddRange(new Telerik.Reporting.Filter[] {
            new Telerik.Reporting.Filter("=RowID", Telerik.Reporting.FilterOperator.Equal, "=1")});
            formattingRule3.Style.Visible = false;
            this.gfsCategory.ConditionalFormatting.AddRange(new Telerik.Reporting.Drawing.FormattingRule[] {
            formattingRule3});
            this.gfsCategory.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179);
            this.gfsCategory.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtTotalCategory,
            this.txtTotalOfCategory});
            this.gfsCategory.Name = "gfsCategory";
            // 
            // txtTotalCategory
            // 
            this.txtTotalCategory.Format = "{0:#,##0.00;(#,##0.00)}";
            this.txtTotalCategory.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.5), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.txtTotalCategory.Name = "txtTotalCategory";
            this.txtTotalCategory.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4957536458969116), Telerik.Reporting.Drawing.Unit.Inch(0.19996070861816406));
            this.txtTotalCategory.Style.BorderColor.Top = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(160)))));
            this.txtTotalCategory.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtTotalCategory.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(160)))));
            this.txtTotalCategory.Style.Font.Bold = true;
            this.txtTotalCategory.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtTotalCategory.Value = "=sum(Balance)";
            // 
            // txtTotalOfCategory
            // 
            this.txtTotalOfCategory.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9386748539982364E-05), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.txtTotalOfCategory.Name = "txtTotalOfCategory";
            this.txtTotalOfCategory.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.499882698059082), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtTotalOfCategory.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(160)))));
            this.txtTotalOfCategory.Style.Font.Bold = true;
            this.txtTotalOfCategory.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(4);
            this.txtTotalOfCategory.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtTotalOfCategory.Value = "=\"Total Of \" + Category";
            // 
            // ghsCategory
            // 
            formattingRule4.Filters.AddRange(new Telerik.Reporting.Filter[] {
            new Telerik.Reporting.Filter("=RowID", Telerik.Reporting.FilterOperator.Equal, "=1")});
            formattingRule4.Style.Visible = false;
            this.ghsCategory.ConditionalFormatting.AddRange(new Telerik.Reporting.Drawing.FormattingRule[] {
            formattingRule4});
            this.ghsCategory.Height = Telerik.Reporting.Drawing.Unit.Inch(0.10003942251205444);
            this.ghsCategory.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox6});
            this.ghsCategory.Name = "ghsCategory";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9434431528206915E-05), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.4998812675476074), Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612));
            this.textBox6.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(4);
            this.textBox6.Value = "=Category";
            // 
            // CashFlowIndirectDetailReport
            // 
            this.DocumentName = "CashFlow Report";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            this.grpActivity,
            this.grpNormalBalance,
            this.grpCategory});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.ghsActivity,
            this.gfsActivities,
            this.ghsNormalBalance,
            this.gfsNormalBalance,
            this.ghsCategory,
            this.gfsCategory,
            this.pageHeader,
            this.detail,
            this.pageFooter,
            reportFooterSection1});
            this.Name = "CashFlowIndirectDetailReport";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Inch(0.25);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Inch(0.25);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Inch(0.25);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Inch(0.25);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Sortings.AddRange(new Telerik.Reporting.Sorting[] {
            new Telerik.Reporting.Sorting("=RowId", Telerik.Reporting.SortDirection.Asc)});
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(8);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox txtReportTitle;
        private Telerik.Reporting.TextBox txtPrintDateTime;
        private Telerik.Reporting.TextBox txtPageNumber;
        private Telerik.Reporting.TextBox txtCompanyName;
        private Telerik.Reporting.TextBox txtFinalBalance;
        private Telerik.Reporting.TextBox txtChartOfAccountName;
        private Telerik.Reporting.TextBox txtChartOfAccountCode;
        private Telerik.Reporting.TextBox txtPeriode;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.Shape shape5;
        private Telerik.Reporting.Group grpActivity;
        private Telerik.Reporting.GroupFooterSection gfsActivities;
        private Telerik.Reporting.GroupHeaderSection ghsActivity;
        private Telerik.Reporting.TextBox txtActivity;
        private Telerik.Reporting.Group grpNormalBalance;
        private Telerik.Reporting.GroupFooterSection gfsNormalBalance;
        private Telerik.Reporting.GroupHeaderSection ghsNormalBalance;
        private Telerik.Reporting.Group grpCategory;
        private Telerik.Reporting.GroupFooterSection gfsCategory;
        private Telerik.Reporting.GroupHeaderSection ghsCategory;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox txtTotalCategory;
        private Telerik.Reporting.TextBox txtTotalOfCategory;
        private Telerik.Reporting.TextBox txtTotalNormalBalance;
        private Telerik.Reporting.TextBox txtTotalOfNormalBalance;
        private Telerik.Reporting.TextBox txtTotalActivity;
        private Telerik.Reporting.TextBox txtTotalOfActivity;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox txtTotalBeginingBalance;
        private Telerik.Reporting.TextBox txtTotalEndingBalance;
    }
}