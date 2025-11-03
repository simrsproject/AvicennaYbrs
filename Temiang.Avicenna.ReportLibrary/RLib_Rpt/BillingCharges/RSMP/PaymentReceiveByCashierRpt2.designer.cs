namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges.RSMP
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class PaymentReceiveByCashierRpt2
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.txtPeriod = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox66 = new Telerik.Reporting.TextBox();
            this.textBox37 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox63 = new Telerik.Reporting.TextBox();
            this.textBox81 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox48 = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.textBox82 = new Telerik.Reporting.TextBox();
            this.group1 = new Telerik.Reporting.Group();
            this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox38 = new Telerik.Reporting.TextBox();
            this.textBox42 = new Telerik.Reporting.TextBox();
            this.textBox69 = new Telerik.Reporting.TextBox();
            this.textBox72 = new Telerik.Reporting.TextBox();
            this.textBox44 = new Telerik.Reporting.TextBox();
            this.textBox45 = new Telerik.Reporting.TextBox();
            this.textBox46 = new Telerik.Reporting.TextBox();
            this.textBox47 = new Telerik.Reporting.TextBox();
            this.textBox41 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.textBox60 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox74 = new Telerik.Reporting.TextBox();
            this.textBox76 = new Telerik.Reporting.TextBox();
            this.textBox77 = new Telerik.Reporting.TextBox();
            this.textBox78 = new Telerik.Reporting.TextBox();
            this.textBox83 = new Telerik.Reporting.TextBox();
            this.textBox84 = new Telerik.Reporting.TextBox();
            this.textBox88 = new Telerik.Reporting.TextBox();
            this.textBox89 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox73 = new Telerik.Reporting.TextBox();
            this.textBox75 = new Telerik.Reporting.TextBox();
            this.textBox79 = new Telerik.Reporting.TextBox();
            this.textBox43 = new Telerik.Reporting.TextBox();
            this.textBox52 = new Telerik.Reporting.TextBox();
            this.textBox87 = new Telerik.Reporting.TextBox();
            this.textBox90 = new Telerik.Reporting.TextBox();
            this.textBox91 = new Telerik.Reporting.TextBox();
            this.textBox92 = new Telerik.Reporting.TextBox();
            this.textBox94 = new Telerik.Reporting.TextBox();
            this.textBox95 = new Telerik.Reporting.TextBox();
            this.textBox98 = new Telerik.Reporting.TextBox();
            this.textBox99 = new Telerik.Reporting.TextBox();
            this.textBox100 = new Telerik.Reporting.TextBox();
            this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox40 = new Telerik.Reporting.TextBox();
            this.txtUserID = new Telerik.Reporting.TextBox();
            this.group2 = new Telerik.Reporting.Group();
            this.groupFooterSection2 = new Telerik.Reporting.GroupFooterSection();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox68 = new Telerik.Reporting.TextBox();
            this.textBox71 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox56 = new Telerik.Reporting.TextBox();
            this.groupHeaderSection2 = new Telerik.Reporting.GroupHeaderSection();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox39 = new Telerik.Reporting.TextBox();
            this.textBox53 = new Telerik.Reporting.TextBox();
            this.textBox54 = new Telerik.Reporting.TextBox();
            this.textBox55 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox65 = new Telerik.Reporting.TextBox();
            this.textBox36 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox35 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.textBox33 = new Telerik.Reporting.TextBox();
            this.textBox34 = new Telerik.Reporting.TextBox();
            this.group4 = new Telerik.Reporting.Group();
            this.groupFooterSection4 = new Telerik.Reporting.GroupFooterSection();
            this.textBox50 = new Telerik.Reporting.TextBox();
            this.textBox51 = new Telerik.Reporting.TextBox();
            this.textBox61 = new Telerik.Reporting.TextBox();
            this.textBox62 = new Telerik.Reporting.TextBox();
            this.textBox67 = new Telerik.Reporting.TextBox();
            this.textBox70 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox57 = new Telerik.Reporting.TextBox();
            this.groupHeaderSection4 = new Telerik.Reporting.GroupHeaderSection();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox49 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox101 = new Telerik.Reporting.TextBox();
            this.textBox102 = new Telerik.Reporting.TextBox();
            this.textBox103 = new Telerik.Reporting.TextBox();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.group3 = new Telerik.Reporting.Group();
            this.groupFooterSection3 = new Telerik.Reporting.GroupFooterSection();
            this.groupHeaderSection3 = new Telerik.Reporting.GroupHeaderSection();
            this.textBox86 = new Telerik.Reporting.TextBox();
            this.group5 = new Telerik.Reporting.Group();
            this.groupFooterSection5 = new Telerik.Reporting.GroupFooterSection();
            this.textBox58 = new Telerik.Reporting.TextBox();
            this.textBox59 = new Telerik.Reporting.TextBox();
            this.textBox64 = new Telerik.Reporting.TextBox();
            this.textBox80 = new Telerik.Reporting.TextBox();
            this.textBox85 = new Telerik.Reporting.TextBox();
            this.textBox93 = new Telerik.Reporting.TextBox();
            this.textBox96 = new Telerik.Reporting.TextBox();
            this.textBox97 = new Telerik.Reporting.TextBox();
            this.groupHeaderSection5 = new Telerik.Reporting.GroupHeaderSection();
            this.group6 = new Telerik.Reporting.Group();
            this.groupFooterSection6 = new Telerik.Reporting.GroupFooterSection();
            this.textBox104 = new Telerik.Reporting.TextBox();
            this.textBox105 = new Telerik.Reporting.TextBox();
            this.textBox106 = new Telerik.Reporting.TextBox();
            this.textBox107 = new Telerik.Reporting.TextBox();
            this.textBox108 = new Telerik.Reporting.TextBox();
            this.textBox109 = new Telerik.Reporting.TextBox();
            this.textBox110 = new Telerik.Reporting.TextBox();
            this.textBox111 = new Telerik.Reporting.TextBox();
            this.groupHeaderSection6 = new Telerik.Reporting.GroupHeaderSection();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(2.89989972114563);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtPeriod,
            this.textBox5});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.Font.Name = "Tahoma";
            // 
            // txtPeriod
            // 
            this.txtPeriod.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.9548063278198242), Telerik.Reporting.Drawing.Unit.Inch(0.95208340883255));
            this.txtPeriod.Name = "txtPeriod";
            this.txtPeriod.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.0787396430969238), Telerik.Reporting.Drawing.Unit.Inch(0.13894350826740265));
            this.txtPeriod.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.txtPeriod.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.txtPeriod.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.txtPeriod.Style.Color = System.Drawing.Color.Black;
            this.txtPeriod.Style.Font.Bold = true;
            this.txtPeriod.Style.Font.Name = "Tahoma";
            this.txtPeriod.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(12);
            this.txtPeriod.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtPeriod.Value = "Periode:";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.9548063278198242), Telerik.Reporting.Drawing.Unit.Inch(0.71041673421859741));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.0787396430969238), Telerik.Reporting.Drawing.Unit.Inch(0.23843836784362793));
            this.textBox5.Style.Color = System.Drawing.Color.Black;
            this.textBox5.Style.Font.Bold = false;
            this.textBox5.Style.Font.Name = "Tahoma";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(18);
            this.textBox5.Style.Font.Strikeout = false;
            this.textBox5.Style.Font.Underline = false;
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.Value = "Laporan Penerimaan Per Kasir";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3444696664810181), Telerik.Reporting.Drawing.Unit.Inch(0.61654883623123169));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2082551717758179), Telerik.Reporting.Drawing.Unit.Inch(0.33086362481117249));
            this.textBox7.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox7.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox7.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox7.Style.Color = System.Drawing.Color.Black;
            this.textBox7.Style.Font.Bold = false;
            this.textBox7.Style.Font.Name = "Tahoma";
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.Value = "No. Register";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.34628948569297791), Telerik.Reporting.Drawing.Unit.Inch(0.61654883623123169));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99810141324996948), Telerik.Reporting.Drawing.Unit.Inch(0.33086362481117249));
            this.textBox6.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox6.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox6.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox6.Style.Color = System.Drawing.Color.Black;
            this.textBox6.Style.Font.Bold = false;
            this.textBox6.Style.Font.Name = "Tahoma";
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "No. Kwitansi";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.55532461404800415);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox17,
            this.textBox19,
            this.textBox29,
            this.textBox66,
            this.textBox37,
            this.textBox2,
            this.textBox63,
            this.textBox81});
            this.detail.Name = "detail";
            this.detail.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.detail.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            // 
            // textBox17
            // 
            this.textBox17.Format = "{0:N2}";
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.2292823791503906), Telerik.Reporting.Drawing.Unit.Inch(0.0155855817720294));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82493323087692261), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox17.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox17.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox17.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox17.Style.Font.Bold = false;
            this.textBox17.Style.Font.Name = "Tahoma";
            this.textBox17.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox17.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox17.Value = "=Cash";
            // 
            // textBox19
            // 
            this.textBox19.Format = "{0:N2}";
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.0542945861816406), Telerik.Reporting.Drawing.Unit.Inch(0.015625));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.86695724725723267), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox19.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox19.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox19.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox19.Style.Font.Bold = false;
            this.textBox19.Style.Font.Name = "Tahoma";
            this.textBox19.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox19.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox19.Value = "=CreditCard";
            // 
            // textBox29
            // 
            this.textBox29.Format = "{0:N2}";
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9213306903839111), Telerik.Reporting.Drawing.Unit.Inch(0.015625));
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82753777503967285), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox29.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox29.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox29.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox29.Style.Font.Bold = false;
            this.textBox29.Style.Font.Name = "Tahoma";
            this.textBox29.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox29.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox29.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox29.Value = "=DebitCard + Transfer";
            // 
            // textBox66
            // 
            this.textBox66.Format = "{0:N2}";
            this.textBox66.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.7536411285400391), Telerik.Reporting.Drawing.Unit.Inch(0.0155855817720294));
            this.textBox66.Name = "textBox15";
            this.textBox66.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82768756151199341), Telerik.Reporting.Drawing.Unit.Inch(0.18958337604999542));
            this.textBox66.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox66.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox66.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox66.Style.Font.Bold = false;
            this.textBox66.Style.Font.Name = "Tahoma";
            this.textBox66.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox66.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox66.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox66.Value = "=DPOffset";
            // 
            // textBox37
            // 
            this.textBox37.Format = "{0:N2}";
            this.textBox37.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.5814061164855957), Telerik.Reporting.Drawing.Unit.Inch(0.015625));
            this.textBox37.Name = "textBox15";
            this.textBox37.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.83463209867477417), Telerik.Reporting.Drawing.Unit.Inch(0.18958337604999542));
            this.textBox37.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox37.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox37.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox37.Style.Font.Bold = false;
            this.textBox37.Style.Font.Name = "Tahoma";
            this.textBox37.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox37.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox37.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox37.Value = "=DPReturn";
            // 
            // textBox2
            // 
            this.textBox2.Format = "{0:N2}";
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.4161152839660645), Telerik.Reporting.Drawing.Unit.Inch(0.0155855817720294));
            this.textBox2.Name = "textBox17";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82569551467895508), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox2.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox2.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox2.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox2.Style.Font.Bold = false;
            this.textBox2.Style.Font.Name = "Tahoma";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "=Retur";
            // 
            // textBox63
            // 
            this.textBox63.Format = "{0:N2}";
            this.textBox63.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.24188756942749), Telerik.Reporting.Drawing.Unit.Inch(0.015625));
            this.textBox63.Name = "textBox63";
            this.textBox63.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82713025808334351), Telerik.Reporting.Drawing.Unit.Inch(0.18954403698444367));
            this.textBox63.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox63.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox63.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox63.Style.Font.Bold = false;
            this.textBox63.Style.Font.Name = "Tahoma";
            this.textBox63.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox63.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox63.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox63.Value = "=TotalAmount";
            // 
            // textBox81
            // 
            this.textBox81.Format = "{0:N0}";
            this.textBox81.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.34628948569297791), Telerik.Reporting.Drawing.Unit.Inch(7.8678131103515625E-05));
            this.textBox81.Name = "textBox81";
            this.textBox81.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.8732796907424927), Telerik.Reporting.Drawing.Unit.Inch(0.18954403698444367));
            this.textBox81.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox81.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox81.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox81.Style.Font.Bold = false;
            this.textBox81.Style.Font.Name = "Tahoma";
            this.textBox81.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox81.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox81.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox81.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox81.Value = "=Ket";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.8132197856903076), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.6382598876953125), Telerik.Reporting.Drawing.Unit.Inch(0.16011793911457062));
            this.textBox11.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox11.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox11.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox11.Style.Font.Bold = false;
            this.textBox11.Style.Font.Name = "Tahoma";
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox11.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox11.Value = "=PatientName";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.4694696664810181), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3436712026596069), Telerik.Reporting.Drawing.Unit.Inch(0.15999999642372131));
            this.textBox8.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox8.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox8.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox8.Style.Font.Bold = false;
            this.textBox8.Style.Font.Name = "Tahoma";
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Value = "=RegistrationNo";
            // 
            // textBox1
            // 
            this.textBox1.Format = "{0}.";
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.9472862068996619E-08), Telerik.Reporting.Drawing.Unit.Inch(0.020951351150870323));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.34621071815490723), Telerik.Reporting.Drawing.Unit.Inch(0.15999999642372131));
            this.textBox1.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox1.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox1.Style.Font.Bold = false;
            this.textBox1.Style.Font.Name = "Tahoma";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "= RowNumber()";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.34628948569297791), Telerik.Reporting.Drawing.Unit.Inch(0.020912012085318565));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1231013536453247), Telerik.Reporting.Drawing.Unit.Inch(0.15999999642372131));
            this.textBox4.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox4.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox4.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox4.Style.Font.Bold = false;
            this.textBox4.Style.Font.Name = "Tahoma";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox4.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.Value = "=PaymentNo";
            // 
            // textBox48
            // 
            this.textBox48.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.024767279624939), Telerik.Reporting.Drawing.Unit.Inch(0.38546499609947205));
            this.textBox48.Name = "textBox48";
            this.textBox48.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.6823768615722656), Telerik.Reporting.Drawing.Unit.Inch(0.15748023986816406));
            this.textBox48.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox48.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox48.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox48.Style.Color = System.Drawing.Color.Black;
            this.textBox48.Style.Font.Bold = false;
            this.textBox48.Style.Font.Name = "Tahoma";
            this.textBox48.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox48.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox48.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox48.Value = "=PaymentTypeName";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.5978589653968811);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox28,
            this.textBox82});
            this.pageFooter.Name = "pageFooter";
            // 
            // textBox28
            // 
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.6609139442443848), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.3984057903289795), Telerik.Reporting.Drawing.Unit.Inch(0.19999916851520538));
            this.textBox28.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox28.Style.Font.Bold = false;
            this.textBox28.Style.Font.Italic = true;
            this.textBox28.Style.Font.Name = "Arial";
            this.textBox28.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox28.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox28.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox28.Value = "=\'Page : \' + PageNumber +\' / \'+ PageCount";
            // 
            // textBox82
            // 
            this.textBox82.Format = "Avicenna HIS, Print Date : {0:dd-MM-yyyy HH:mm}";
            this.textBox82.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1701248437166214), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox82.Name = "textBox82";
            this.textBox82.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.4907107353210449), Telerik.Reporting.Drawing.Unit.Inch(0.19999940693378449));
            this.textBox82.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox82.Style.Font.Bold = false;
            this.textBox82.Style.Font.Italic = true;
            this.textBox82.Style.Font.Name = "Arial";
            this.textBox82.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox82.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox82.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox82.Value = "=Now()";
            // 
            // group1
            // 
            this.group1.GroupFooter = this.groupFooterSection1;
            this.group1.GroupHeader = this.groupHeaderSection1;
            this.group1.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("=CashierName")});
            this.group1.Name = "group1";
            // 
            // groupFooterSection1
            // 
            this.groupFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(5.6373229026794434);
            this.groupFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox21,
            this.textBox38,
            this.textBox42,
            this.textBox69,
            this.textBox72,
            this.textBox44,
            this.textBox45,
            this.textBox46,
            this.textBox47,
            this.textBox41,
            this.textBox32,
            this.textBox60,
            this.textBox13,
            this.textBox74,
            this.textBox76,
            this.textBox77,
            this.textBox78,
            this.textBox83,
            this.textBox84,
            this.textBox88,
            this.textBox89,
            this.textBox9,
            this.textBox73,
            this.textBox75,
            this.textBox79,
            this.textBox43,
            this.textBox52,
            this.textBox87,
            this.textBox90,
            this.textBox91,
            this.textBox92,
            this.textBox94,
            this.textBox95,
            this.textBox98,
            this.textBox99,
            this.textBox100});
            this.groupFooterSection1.Name = "groupFooterSection1";
            this.groupFooterSection1.PageBreak = Telerik.Reporting.PageBreak.None;
            this.groupFooterSection1.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            // 
            // textBox21
            // 
            this.textBox21.Format = "Total Kasir {0} :";
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.6483728289604187), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5666404962539673), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox21.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox21.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox21.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox21.Style.Color = System.Drawing.Color.Black;
            this.textBox21.Style.Font.Bold = false;
            this.textBox21.Style.Font.Name = "Tahoma";
            this.textBox21.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox21.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox21.Value = "=CashierName";
            // 
            // textBox38
            // 
            this.textBox38.Format = "{0:N2}";
            this.textBox38.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.2188656330108643), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox38.Name = "textBox38";
            this.textBox38.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82493335008621216), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox38.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox38.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox38.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox38.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox38.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox38.Style.Color = System.Drawing.Color.Black;
            this.textBox38.Style.Font.Bold = false;
            this.textBox38.Style.Font.Name = "Tahoma";
            this.textBox38.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox38.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox38.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox38.Value = "=sum(Cash)";
            // 
            // textBox42
            // 
            this.textBox42.Format = "{0:N2}";
            this.textBox42.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9075920581817627), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox42.Name = "textBox42";
            this.textBox42.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8308594822883606), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox42.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox42.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox42.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox42.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox42.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox42.Style.Color = System.Drawing.Color.Black;
            this.textBox42.Style.Font.Bold = false;
            this.textBox42.Style.Font.Name = "Tahoma";
            this.textBox42.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox42.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox42.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox42.Value = "=sum(DebitCard + Transfer)";
            // 
            // textBox69
            // 
            this.textBox69.Format = "{0:N2}";
            this.textBox69.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.74322509765625), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox69.Name = "textBox69";
            this.textBox69.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82768672704696655), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox69.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox69.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox69.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox69.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox69.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox69.Style.Color = System.Drawing.Color.Black;
            this.textBox69.Style.Font.Bold = false;
            this.textBox69.Style.Font.Name = "Tahoma";
            this.textBox69.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox69.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox69.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox69.Value = "=sum(DPOffset)";
            // 
            // textBox72
            // 
            this.textBox72.Format = "{0:N2}";
            this.textBox72.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.5709896087646484), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox72.Name = "textBox69";
            this.textBox72.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.83463209867477417), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox72.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox72.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox72.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox72.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox72.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox72.Style.Color = System.Drawing.Color.Black;
            this.textBox72.Style.Font.Bold = false;
            this.textBox72.Style.Font.Name = "Tahoma";
            this.textBox72.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox72.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox72.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox72.Value = "=sum(DPReturn)";
            // 
            // textBox44
            // 
            this.textBox44.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.33587265014648438), Telerik.Reporting.Drawing.Unit.Inch(1.9568790197372437));
            this.textBox44.Name = "textBox44";
            this.textBox44.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7780011892318726), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox44.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox44.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox44.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox44.Style.Color = System.Drawing.Color.Black;
            this.textBox44.Style.Font.Bold = false;
            this.textBox44.Style.Font.Name = "Tahoma";
            this.textBox44.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox44.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox44.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox44.Value = "=CashierName";
            // 
            // textBox45
            // 
            this.textBox45.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.5749053955078125), Telerik.Reporting.Drawing.Unit.Inch(1.2289527654647827));
            this.textBox45.Name = "textBox45";
            this.textBox45.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.331395149230957), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox45.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox45.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox45.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox45.Style.Color = System.Drawing.Color.Black;
            this.textBox45.Style.Font.Bold = false;
            this.textBox45.Style.Font.Name = "Tahoma";
            this.textBox45.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox45.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox45.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox45.Value = "Dibuat Oleh";
            // 
            // textBox46
            // 
            this.textBox46.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.20045614242553711), Telerik.Reporting.Drawing.Unit.Inch(1.9568790197372437));
            this.textBox46.Name = "textBox46";
            this.textBox46.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.13533735275268555), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox46.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox46.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox46.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox46.Style.Color = System.Drawing.Color.Black;
            this.textBox46.Style.Font.Bold = false;
            this.textBox46.Style.Font.Name = "Tahoma";
            this.textBox46.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox46.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox46.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox46.Value = "(";
            // 
            // textBox47
            // 
            this.textBox47.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.11395263671875), Telerik.Reporting.Drawing.Unit.Inch(1.9568790197372437));
            this.textBox47.Name = "textBox47";
            this.textBox47.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.12027326971292496), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox47.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox47.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox47.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox47.Style.Color = System.Drawing.Color.Black;
            this.textBox47.Style.Font.Bold = false;
            this.textBox47.Style.Font.Name = "Tahoma";
            this.textBox47.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox47.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox47.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox47.Value = ")";
            // 
            // textBox41
            // 
            this.textBox41.Format = "{0:N2}";
            this.textBox41.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.0438783168792725), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox41.Name = "textBox41";
            this.textBox41.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.86363506317138672), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox41.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox41.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox41.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox41.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox41.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox41.Style.Color = System.Drawing.Color.Black;
            this.textBox41.Style.Font.Bold = false;
            this.textBox41.Style.Font.Name = "Tahoma";
            this.textBox41.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox41.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox41.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox41.Value = "=sum(CreditCard)";
            // 
            // textBox32
            // 
            this.textBox32.Format = "{0:N2}";
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.4056992530822754), Telerik.Reporting.Drawing.Unit.Inch(7.8837074397597462E-05));
            this.textBox32.Name = "textBox69";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.825694739818573), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox32.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox32.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox32.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox32.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox32.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox32.Style.Color = System.Drawing.Color.Black;
            this.textBox32.Style.Font.Bold = false;
            this.textBox32.Style.Font.Name = "Tahoma";
            this.textBox32.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox32.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox32.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox32.Value = "=sum(Retur)";
            // 
            // textBox60
            // 
            this.textBox60.Format = "{0:N2}";
            this.textBox60.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.2314720153808594), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox60.Name = "textBox9";
            this.textBox60.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82712942361831665), Telerik.Reporting.Drawing.Unit.Inch(0.18966229259967804));
            this.textBox60.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox60.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox60.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox60.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox60.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox60.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox60.Style.Color = System.Drawing.Color.Black;
            this.textBox60.Style.Font.Bold = false;
            this.textBox60.Style.Font.Name = "Tahoma";
            this.textBox60.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox60.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox60.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox60.Value = "=Sum(TotalAmount)";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1770833283662796), Telerik.Reporting.Drawing.Unit.Inch(0.29813608527183533));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3931522369384766), Telerik.Reporting.Drawing.Unit.Inch(0.28333356976509094));
            this.textBox13.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox13.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox13.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox13.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox13.Style.Color = System.Drawing.Color.Black;
            this.textBox13.Style.Font.Bold = false;
            this.textBox13.Style.Font.Name = "Tahoma";
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.Value = "Uang Disetor";
            // 
            // textBox74
            // 
            this.textBox74.Format = "{0:N2}";
            this.textBox74.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.6882197856903076), Telerik.Reporting.Drawing.Unit.Inch(0.91263419389724731));
            this.textBox74.Name = "textBox41";
            this.textBox74.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97699230909347534), Telerik.Reporting.Drawing.Unit.Inch(0.18934249877929688));
            this.textBox74.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox74.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox74.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox74.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox74.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox74.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox74.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox74.Style.Color = System.Drawing.Color.Black;
            this.textBox74.Style.Font.Bold = false;
            this.textBox74.Style.Font.Name = "Tahoma";
            this.textBox74.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox74.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox74.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox74.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox74.Value = "=sum(CreditCard)";
            // 
            // textBox76
            // 
            this.textBox76.Format = "";
            this.textBox76.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1770833283662796), Telerik.Reporting.Drawing.Unit.Inch(0.64419269561767578));
            this.textBox76.Name = "textBox21";
            this.textBox76.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3931522369384766), Telerik.Reporting.Drawing.Unit.Inch(0.45778465270996094));
            this.textBox76.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox76.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox76.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox76.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox76.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox76.Style.Color = System.Drawing.Color.Black;
            this.textBox76.Style.Font.Bold = false;
            this.textBox76.Style.Font.Name = "Tahoma";
            this.textBox76.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox76.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox76.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox76.Value = "Grand Total";
            // 
            // textBox77
            // 
            this.textBox77.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5703144073486328), Telerik.Reporting.Drawing.Unit.Inch(0.6441914439201355));
            this.textBox77.Name = "textBox55";
            this.textBox77.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1178264617919922), Telerik.Reporting.Drawing.Unit.Inch(0.26589298248291016));
            this.textBox77.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox77.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox77.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox77.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox77.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox77.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox77.Style.Color = System.Drawing.Color.Black;
            this.textBox77.Style.Font.Bold = false;
            this.textBox77.Style.Font.Name = "Tahoma";
            this.textBox77.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox77.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox77.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox77.Style.Visible = true;
            this.textBox77.Value = "Tunai";
            // 
            // textBox78
            // 
            this.textBox78.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.6882197856903076), Telerik.Reporting.Drawing.Unit.Inch(0.6441914439201355));
            this.textBox78.Name = "textBox54";
            this.textBox78.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97699230909347534), Telerik.Reporting.Drawing.Unit.Inch(0.2658936083316803));
            this.textBox78.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox78.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox78.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox78.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox78.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox78.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox78.Style.Color = System.Drawing.Color.Black;
            this.textBox78.Style.Font.Bold = false;
            this.textBox78.Style.Font.Name = "Tahoma";
            this.textBox78.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox78.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox78.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox78.Style.Visible = true;
            this.textBox78.Value = "K.Kredit";
            // 
            // textBox83
            // 
            this.textBox83.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.6350741386413574), Telerik.Reporting.Drawing.Unit.Inch(0.64419269561767578));
            this.textBox83.Name = "textBox56";
            this.textBox83.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97825878858566284), Telerik.Reporting.Drawing.Unit.Inch(0.265892893075943));
            this.textBox83.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox83.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox83.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox83.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox83.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox83.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox83.Style.Color = System.Drawing.Color.Black;
            this.textBox83.Style.Font.Bold = false;
            this.textBox83.Style.Font.Name = "Tahoma";
            this.textBox83.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox83.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox83.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox83.Style.Visible = true;
            this.textBox83.Value = "DP Offset";
            // 
            // textBox84
            // 
            this.textBox84.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.6134114265441895), Telerik.Reporting.Drawing.Unit.Inch(0.64395302534103394));
            this.textBox84.Name = "textBox56";
            this.textBox84.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.98740893602371216), Telerik.Reporting.Drawing.Unit.Inch(0.26613259315490723));
            this.textBox84.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox84.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox84.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox84.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox84.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox84.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox84.Style.Color = System.Drawing.Color.Black;
            this.textBox84.Style.Font.Bold = false;
            this.textBox84.Style.Font.Name = "Tahoma";
            this.textBox84.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox84.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox84.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox84.Style.Visible = true;
            this.textBox84.Value = "DP Kembali";
            // 
            // textBox88
            // 
            this.textBox88.Format = "{0:N2}";
            this.textBox88.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.6350741386413574), Telerik.Reporting.Drawing.Unit.Inch(0.91263419389724731));
            this.textBox88.Name = "textBox69";
            this.textBox88.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97825878858566284), Telerik.Reporting.Drawing.Unit.Inch(0.18934249877929688));
            this.textBox88.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox88.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox88.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox88.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox88.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox88.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox88.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox88.Style.Color = System.Drawing.Color.Black;
            this.textBox88.Style.Font.Bold = false;
            this.textBox88.Style.Font.Name = "Tahoma";
            this.textBox88.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox88.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox88.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox88.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox88.Value = "=sum(DPOffset)";
            // 
            // textBox89
            // 
            this.textBox89.Format = "{0:N2}";
            this.textBox89.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.6134114265441895), Telerik.Reporting.Drawing.Unit.Inch(0.91239386796951294));
            this.textBox89.Name = "textBox69";
            this.textBox89.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.98740893602371216), Telerik.Reporting.Drawing.Unit.Inch(0.18958282470703125));
            this.textBox89.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox89.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox89.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox89.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox89.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox89.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox89.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox89.Style.Color = System.Drawing.Color.Black;
            this.textBox89.Style.Font.Bold = false;
            this.textBox89.Style.Font.Name = "Tahoma";
            this.textBox89.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox89.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox89.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox89.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox89.Value = "=sum(DPReturn)";
            // 
            // textBox9
            // 
            this.textBox9.Format = "{0:N0}";
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5703144073486328), Telerik.Reporting.Drawing.Unit.Inch(0.29813608527183533));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1178264617919922), Telerik.Reporting.Drawing.Unit.Inch(0.28333345055580139));
            this.textBox9.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox9.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox9.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox9.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox9.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox9.Style.Color = System.Drawing.Color.Black;
            this.textBox9.Style.Font.Bold = false;
            this.textBox9.Style.Font.Name = "Tahoma";
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox9.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox9.Value = "=sum(Cash - DPReturn+Retur)";
            // 
            // textBox73
            // 
            this.textBox73.Format = "{0:N2}";
            this.textBox73.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5703144073486328), Telerik.Reporting.Drawing.Unit.Inch(0.91263419389724731));
            this.textBox73.Name = "textBox38";
            this.textBox73.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.11782705783844), Telerik.Reporting.Drawing.Unit.Inch(0.18934249877929688));
            this.textBox73.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox73.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox73.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox73.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox73.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox73.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox73.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox73.Style.Color = System.Drawing.Color.Black;
            this.textBox73.Style.Font.Bold = false;
            this.textBox73.Style.Font.Name = "Tahoma";
            this.textBox73.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox73.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox73.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox73.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox73.Value = "=sum(Cash)";
            // 
            // textBox75
            // 
            this.textBox75.Format = "{0:N2}";
            this.textBox75.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.6686127185821533), Telerik.Reporting.Drawing.Unit.Inch(0.91486519575119019));
            this.textBox75.Name = "textBox42";
            this.textBox75.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.962954044342041), Telerik.Reporting.Drawing.Unit.Inch(0.18711216747760773));
            this.textBox75.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox75.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox75.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox75.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox75.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox75.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox75.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox75.Style.Color = System.Drawing.Color.Black;
            this.textBox75.Style.Font.Bold = false;
            this.textBox75.Style.Font.Name = "Tahoma";
            this.textBox75.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox75.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox75.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox75.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox75.Value = "=sum(DebitCard + Transfer)";
            // 
            // textBox79
            // 
            this.textBox79.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.6652908325195312), Telerik.Reporting.Drawing.Unit.Inch(0.64395302534103394));
            this.textBox79.Name = "textBox53";
            this.textBox79.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.96627599000930786), Telerik.Reporting.Drawing.Unit.Inch(0.26613268256187439));
            this.textBox79.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox79.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox79.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox79.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox79.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox79.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox79.Style.Color = System.Drawing.Color.Black;
            this.textBox79.Style.Font.Bold = false;
            this.textBox79.Style.Font.Name = "Tahoma";
            this.textBox79.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox79.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox79.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox79.Style.Visible = true;
            this.textBox79.Value = "K.Debit / Transf";
            // 
            // textBox43
            // 
            this.textBox43.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.6008992195129395), Telerik.Reporting.Drawing.Unit.Inch(0.64419203996658325));
            this.textBox43.Name = "textBox56";
            this.textBox43.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.89575451612472534), Telerik.Reporting.Drawing.Unit.Inch(0.26589334011077881));
            this.textBox43.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox43.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox43.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox43.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox43.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox43.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox43.Style.Color = System.Drawing.Color.Black;
            this.textBox43.Style.Font.Bold = false;
            this.textBox43.Style.Font.Name = "Tahoma";
            this.textBox43.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox43.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox43.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox43.Style.Visible = true;
            this.textBox43.Value = "Retur";
            // 
            // textBox52
            // 
            this.textBox52.Format = "{0:N2}";
            this.textBox52.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.600898265838623), Telerik.Reporting.Drawing.Unit.Inch(0.91040325164794922));
            this.textBox52.Name = "textBox69";
            this.textBox52.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8957553505897522), Telerik.Reporting.Drawing.Unit.Inch(0.19157432019710541));
            this.textBox52.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox52.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox52.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox52.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox52.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox52.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox52.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox52.Style.Color = System.Drawing.Color.Black;
            this.textBox52.Style.Font.Bold = false;
            this.textBox52.Style.Font.Name = "Tahoma";
            this.textBox52.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox52.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox52.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox52.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox52.Value = "=sum(Retur)";
            // 
            // textBox87
            // 
            this.textBox87.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9676449298858643), Telerik.Reporting.Drawing.Unit.Inch(1.956878662109375));
            this.textBox87.Name = "textBox87";
            this.textBox87.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.12027326971292496), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox87.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox87.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox87.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox87.Style.Color = System.Drawing.Color.Black;
            this.textBox87.Style.Font.Bold = false;
            this.textBox87.Style.Font.Name = "Tahoma";
            this.textBox87.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox87.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox87.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox87.Value = ")";
            // 
            // textBox90
            // 
            this.textBox90.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.2507197856903076), Telerik.Reporting.Drawing.Unit.Inch(1.956878662109375));
            this.textBox90.Name = "textBox90";
            this.textBox90.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.13533735275268555), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox90.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox90.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox90.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox90.Style.Color = System.Drawing.Color.Black;
            this.textBox90.Style.Font.Bold = false;
            this.textBox90.Style.Font.Name = "Tahoma";
            this.textBox90.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox90.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox90.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox90.Value = "(";
            // 
            // textBox91
            // 
            this.textBox91.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.5528030395507812), Telerik.Reporting.Drawing.Unit.Inch(1.2277120351791382));
            this.textBox91.Name = "textBox91";
            this.textBox91.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.331395149230957), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox91.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox91.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox91.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox91.Style.Color = System.Drawing.Color.Black;
            this.textBox91.Style.Font.Bold = false;
            this.textBox91.Style.Font.Name = "Tahoma";
            this.textBox91.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox91.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox91.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox91.Value = "Diperiksa Oleh";
            // 
            // textBox92
            // 
            this.textBox92.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.4152584075927734), Telerik.Reporting.Drawing.Unit.Inch(1.2289530038833618));
            this.textBox92.Name = "textBox92";
            this.textBox92.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.331395149230957), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox92.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox92.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox92.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox92.Style.Color = System.Drawing.Color.Black;
            this.textBox92.Style.Font.Bold = false;
            this.textBox92.Style.Font.Name = "Tahoma";
            this.textBox92.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox92.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox92.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox92.Value = "Disetujui Oleh";
            // 
            // textBox94
            // 
            this.textBox94.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.1131749153137207), Telerik.Reporting.Drawing.Unit.Inch(1.9581197500228882));
            this.textBox94.Name = "textBox94";
            this.textBox94.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.13533735275268555), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox94.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox94.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox94.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox94.Style.Color = System.Drawing.Color.Black;
            this.textBox94.Style.Font.Bold = false;
            this.textBox94.Style.Font.Name = "Tahoma";
            this.textBox94.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox94.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox94.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox94.Value = "(";
            // 
            // textBox95
            // 
            this.textBox95.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.8215084075927734), Telerik.Reporting.Drawing.Unit.Inch(1.9581197500228882));
            this.textBox95.Name = "textBox95";
            this.textBox95.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.12027326971292496), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox95.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox95.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox95.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox95.Style.Color = System.Drawing.Color.Black;
            this.textBox95.Style.Font.Bold = false;
            this.textBox95.Style.Font.Name = "Tahoma";
            this.textBox95.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox95.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox95.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox95.Value = ")";
            // 
            // textBox98
            // 
            this.textBox98.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.6432089805603027), Telerik.Reporting.Drawing.Unit.Inch(1.9581197500228882));
            this.textBox98.Name = "textBox98";
            this.textBox98.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.12027326971292496), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox98.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox98.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox98.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox98.Style.Color = System.Drawing.Color.Black;
            this.textBox98.Style.Font.Bold = false;
            this.textBox98.Style.Font.Name = "Tahoma";
            this.textBox98.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox98.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox98.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox98.Value = ")";
            // 
            // textBox99
            // 
            this.textBox99.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.9626936912536621), Telerik.Reporting.Drawing.Unit.Inch(1.9581197500228882));
            this.textBox99.Name = "textBox99";
            this.textBox99.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.13533735275268555), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox99.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox99.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox99.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox99.Style.Color = System.Drawing.Color.Black;
            this.textBox99.Style.Font.Bold = false;
            this.textBox99.Style.Font.Name = "Tahoma";
            this.textBox99.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox99.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox99.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox99.Value = "(";
            // 
            // textBox100
            // 
            this.textBox100.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.1756744384765625), Telerik.Reporting.Drawing.Unit.Inch(1.2289530038833618));
            this.textBox100.Name = "textBox100";
            this.textBox100.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.331395149230957), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox100.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox100.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox100.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox100.Style.Color = System.Drawing.Color.Black;
            this.textBox100.Style.Font.Bold = false;
            this.textBox100.Style.Font.Name = "Tahoma";
            this.textBox100.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox100.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox100.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox100.Value = "Disetujui Oleh";
            // 
            // groupHeaderSection1
            // 
            this.groupHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.39999979734420776);
            this.groupHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox16,
            this.textBox40,
            this.txtUserID});
            this.groupHeaderSection1.Name = "groupHeaderSection1";
            this.groupHeaderSection1.PrintOnEveryPage = false;
            this.groupHeaderSection1.Style.Visible = true;
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1631663590669632), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.65887469053268433), Telerik.Reporting.Drawing.Unit.Inch(0.15748023986816406));
            this.textBox16.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox16.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox16.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox16.Style.Color = System.Drawing.Color.Black;
            this.textBox16.Style.Font.Bold = false;
            this.textBox16.Style.Font.Name = "Tahoma";
            this.textBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox16.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox16.Value = "Kasir          ";
            // 
            // textBox40
            // 
            this.textBox40.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.867286741733551), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox40.Name = "textBox40";
            this.textBox40.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.13295619189739227), Telerik.Reporting.Drawing.Unit.Inch(0.15748023986816406));
            this.textBox40.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox40.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox40.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox40.Style.Color = System.Drawing.Color.Black;
            this.textBox40.Style.Font.Bold = false;
            this.textBox40.Style.Font.Name = "Tahoma";
            this.textBox40.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox40.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox40.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox40.Value = ":";
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.024767279624939), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.1336147785186768), Telerik.Reporting.Drawing.Unit.Inch(0.15748023986816406));
            this.txtUserID.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.txtUserID.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.txtUserID.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.txtUserID.Style.Color = System.Drawing.Color.Black;
            this.txtUserID.Style.Font.Bold = false;
            this.txtUserID.Style.Font.Name = "Tahoma";
            this.txtUserID.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.txtUserID.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtUserID.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.txtUserID.Value = "=CashierName";
            // 
            // group2
            // 
            this.group2.GroupFooter = this.groupFooterSection2;
            this.group2.GroupHeader = this.groupHeaderSection2;
            this.group2.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("=PaymentDate")});
            this.group2.Name = "group2";
            this.group2.Sortings.AddRange(new Telerik.Reporting.Sorting[] {
            new Telerik.Reporting.Sorting("=PaymentNo", Telerik.Reporting.SortDirection.Asc)});
            // 
            // groupFooterSection2
            // 
            this.groupFooterSection2.Height = Telerik.Reporting.Drawing.Unit.Cm(0.49990808963775635);
            this.groupFooterSection2.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox24,
            this.textBox18,
            this.textBox27,
            this.textBox30,
            this.textBox68,
            this.textBox71,
            this.textBox15,
            this.textBox56});
            this.groupFooterSection2.Name = "groupFooterSection2";
            // 
            // textBox24
            // 
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.6483728289604187), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.571196436882019), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox24.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox24.Style.Color = System.Drawing.Color.Black;
            this.textBox24.Style.Font.Bold = false;
            this.textBox24.Style.Font.Name = "Tahoma";
            this.textBox24.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox24.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox24.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox24.Value = "Total Per Tanggal : ";
            // 
            // textBox18
            // 
            this.textBox18.Format = "{0:N2}";
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.2292823791503906), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82493335008621216), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox18.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox18.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox18.Style.Color = System.Drawing.Color.Black;
            this.textBox18.Style.Font.Bold = false;
            this.textBox18.Style.Font.Name = "Tahoma";
            this.textBox18.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox18.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox18.Value = "=sum(Cash)";
            // 
            // textBox27
            // 
            this.textBox27.Format = "{0:N2}";
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.0542948246002197), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.86363506317138672), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox27.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox27.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox27.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox27.Style.Color = System.Drawing.Color.Black;
            this.textBox27.Style.Font.Bold = false;
            this.textBox27.Style.Font.Name = "Tahoma";
            this.textBox27.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox27.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox27.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox27.Value = "=sum(CreditCard)";
            // 
            // textBox30
            // 
            this.textBox30.Format = "{0:N2}";
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9180088043212891), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8308594822883606), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox30.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox30.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox30.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox30.Style.Color = System.Drawing.Color.Black;
            this.textBox30.Style.Font.Bold = false;
            this.textBox30.Style.Font.Name = "Tahoma";
            this.textBox30.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox30.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox30.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox30.Value = "=sum(DebitCard + Transfer)";
            // 
            // textBox68
            // 
            this.textBox68.Format = "{0:N2}";
            this.textBox68.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.7536416053771973), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox68.Name = "textBox32";
            this.textBox68.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82768672704696655), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox68.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox68.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox68.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox68.Style.Color = System.Drawing.Color.Black;
            this.textBox68.Style.Font.Bold = false;
            this.textBox68.Style.Font.Name = "Tahoma";
            this.textBox68.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox68.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox68.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox68.Value = "=sum(DPOffset)";
            // 
            // textBox71
            // 
            this.textBox71.Format = "{0:N2}";
            this.textBox71.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.5814061164855957), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox71.Name = "textBox71";
            this.textBox71.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.83463209867477417), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox71.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox71.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox71.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox71.Style.Color = System.Drawing.Color.Black;
            this.textBox71.Style.Font.Bold = false;
            this.textBox71.Style.Font.Name = "Tahoma";
            this.textBox71.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox71.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox71.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox71.Value = "=sum(DPReturn)";
            // 
            // textBox15
            // 
            this.textBox15.Format = "{0:N2}";
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.4161162376403809), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox15.Name = "textBox71";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.825694739818573), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox15.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox15.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox15.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox15.Style.Color = System.Drawing.Color.Black;
            this.textBox15.Style.Font.Bold = false;
            this.textBox15.Style.Font.Name = "Tahoma";
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox15.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox15.Value = "=sum(Retur)";
            // 
            // textBox56
            // 
            this.textBox56.Format = "{0:N2}";
            this.textBox56.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.24231481552124), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox56.Name = "textBox9";
            this.textBox56.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82712942361831665), Telerik.Reporting.Drawing.Unit.Inch(0.1875));
            this.textBox56.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox56.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox56.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox56.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox56.Style.Color = System.Drawing.Color.Black;
            this.textBox56.Style.Font.Bold = false;
            this.textBox56.Style.Font.Name = "Tahoma";
            this.textBox56.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox56.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox56.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox56.Value = "=Sum(TotalAmount)";
            // 
            // groupHeaderSection2
            // 
            this.groupHeaderSection2.Height = Telerik.Reporting.Drawing.Unit.Cm(0.4529164731502533);
            this.groupHeaderSection2.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox22,
            this.textBox23,
            this.textBox39});
            this.groupHeaderSection2.Name = "groupHeaderSection2";
            this.groupHeaderSection2.PrintOnEveryPage = false;
            // 
            // textBox22
            // 
            this.textBox22.Format = "{0: dd-MMMM-yyyy}";
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.024767279624939), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.1336147785186768), Telerik.Reporting.Drawing.Unit.Inch(0.15748023986816406));
            this.textBox22.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox22.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox22.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox22.Style.Color = System.Drawing.Color.Black;
            this.textBox22.Style.Font.Bold = false;
            this.textBox22.Style.Font.Name = "Tahoma";
            this.textBox22.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox22.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox22.Value = "=PaymentDate";
            // 
            // textBox23
            // 
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1770833283662796), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.65887469053268433), Telerik.Reporting.Drawing.Unit.Inch(0.15748023986816406));
            this.textBox23.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox23.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox23.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox23.Style.Color = System.Drawing.Color.Black;
            this.textBox23.Style.Font.Bold = false;
            this.textBox23.Style.Font.Name = "Tahoma";
            this.textBox23.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox23.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox23.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox23.Value = "Tanggal     ";
            // 
            // textBox39
            // 
            this.textBox39.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.867286741733551), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505));
            this.textBox39.Name = "textBox39";
            this.textBox39.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.13295619189739227), Telerik.Reporting.Drawing.Unit.Inch(0.15748023986816406));
            this.textBox39.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox39.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox39.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox39.Style.Color = System.Drawing.Color.Black;
            this.textBox39.Style.Font.Bold = false;
            this.textBox39.Style.Font.Name = "Tahoma";
            this.textBox39.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox39.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox39.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox39.Value = ":";
            // 
            // textBox53
            // 
            this.textBox53.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9075920581817627), Telerik.Reporting.Drawing.Unit.Inch(0.61654883623123169));
            this.textBox53.Name = "textBox53";
            this.textBox53.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8308594822883606), Telerik.Reporting.Drawing.Unit.Inch(0.33086362481117249));
            this.textBox53.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox53.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox53.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox53.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox53.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox53.Style.Color = System.Drawing.Color.Black;
            this.textBox53.Style.Font.Bold = false;
            this.textBox53.Style.Font.Name = "Tahoma";
            this.textBox53.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox53.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox53.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox53.Style.Visible = true;
            this.textBox53.Value = "K.Debit / Transf";
            // 
            // textBox54
            // 
            this.textBox54.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.0438783168792725), Telerik.Reporting.Drawing.Unit.Inch(0.61654883623123169));
            this.textBox54.Name = "textBox54";
            this.textBox54.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.85321837663650513), Telerik.Reporting.Drawing.Unit.Inch(0.33086362481117249));
            this.textBox54.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox54.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox54.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox54.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox54.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox54.Style.Color = System.Drawing.Color.Black;
            this.textBox54.Style.Font.Bold = false;
            this.textBox54.Style.Font.Name = "Tahoma";
            this.textBox54.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox54.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox54.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox54.Style.Visible = true;
            this.textBox54.Value = "K.Kredit";
            // 
            // textBox55
            // 
            this.textBox55.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.5528037548065186), Telerik.Reporting.Drawing.Unit.Inch(0.61654883623123169));
            this.textBox55.Name = "textBox55";
            this.textBox55.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.49099540710449219), Telerik.Reporting.Drawing.Unit.Inch(0.33086362481117249));
            this.textBox55.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox55.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox55.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox55.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox55.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox55.Style.Color = System.Drawing.Color.Black;
            this.textBox55.Style.Font.Bold = false;
            this.textBox55.Style.Font.Name = "Tahoma";
            this.textBox55.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox55.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox55.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox55.Style.Visible = true;
            this.textBox55.Value = "Tunai";
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.61654883623123169));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.34621071815490723), Telerik.Reporting.Drawing.Unit.Inch(0.33086362481117249));
            this.textBox20.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox20.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox20.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox20.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox20.Style.Color = System.Drawing.Color.Black;
            this.textBox20.Style.Font.Bold = false;
            this.textBox20.Style.Font.Name = "Tahoma";
            this.textBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox20.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox20.Value = "No.";
            // 
            // textBox65
            // 
            this.textBox65.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.74322509765625), Telerik.Reporting.Drawing.Unit.Inch(0.61654883623123169));
            this.textBox65.Name = "textBox56";
            this.textBox65.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82768672704696655), Telerik.Reporting.Drawing.Unit.Inch(0.33086356520652771));
            this.textBox65.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox65.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox65.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox65.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox65.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox65.Style.Color = System.Drawing.Color.Black;
            this.textBox65.Style.Font.Bold = false;
            this.textBox65.Style.Font.Name = "Tahoma";
            this.textBox65.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox65.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox65.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox65.Style.Visible = true;
            this.textBox65.Value = "DP Offset";
            // 
            // textBox36
            // 
            this.textBox36.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.570991039276123), Telerik.Reporting.Drawing.Unit.Inch(0.61654883623123169));
            this.textBox36.Name = "textBox56";
            this.textBox36.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.83463078737258911), Telerik.Reporting.Drawing.Unit.Inch(0.33086356520652771));
            this.textBox36.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox36.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox36.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox36.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox36.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox36.Style.Color = System.Drawing.Color.Black;
            this.textBox36.Style.Font.Bold = false;
            this.textBox36.Style.Font.Name = "Tahoma";
            this.textBox36.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox36.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox36.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox36.Style.Visible = true;
            this.textBox36.Value = "DP Kembali";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.40570068359375), Telerik.Reporting.Drawing.Unit.Inch(0.61654883623123169));
            this.textBox14.Name = "textBox56";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.81527680158615112), Telerik.Reporting.Drawing.Unit.Inch(0.33086356520652771));
            this.textBox14.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox14.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox14.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox14.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox14.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox14.Style.Color = System.Drawing.Color.Black;
            this.textBox14.Style.Font.Bold = false;
            this.textBox14.Style.Font.Name = "Tahoma";
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox14.Style.Visible = true;
            this.textBox14.Value = "Retur";
            // 
            // textBox35
            // 
            this.textBox35.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.2210555076599121), Telerik.Reporting.Drawing.Unit.Inch(0.61654883623123169));
            this.textBox35.Name = "textBox56";
            this.textBox35.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.84838873147964478), Telerik.Reporting.Drawing.Unit.Inch(0.33086356520652771));
            this.textBox35.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox35.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox35.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox35.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox35.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox35.Style.Color = System.Drawing.Color.Black;
            this.textBox35.Style.Font.Bold = false;
            this.textBox35.Style.Font.Name = "Tahoma";
            this.textBox35.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox35.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox35.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox35.Style.Visible = true;
            this.textBox35.Value = "Total";
            // 
            // textBox31
            // 
            this.textBox31.Format = "{0:N2}";
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.1119585037231445), Telerik.Reporting.Drawing.Unit.Inch(0.078740119934082031));
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.491879940032959), Telerik.Reporting.Drawing.Unit.Inch(0.18958337604999542));
            this.textBox31.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox31.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox31.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox31.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox31.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox31.Style.Color = System.Drawing.Color.DarkMagenta;
            this.textBox31.Style.Font.Bold = true;
            this.textBox31.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox31.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox31.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox31.Value = "=sum(netto)";
            // 
            // textBox33
            // 
            this.textBox33.Format = "{0:N2}";
            this.textBox33.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.8313684463500977), Telerik.Reporting.Drawing.Unit.Inch(0.078740119934082031));
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1528054475784302), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox33.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox33.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox33.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox33.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox33.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox33.Style.Color = System.Drawing.Color.DarkMagenta;
            this.textBox33.Style.Font.Bold = true;
            this.textBox33.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox33.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox33.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox33.Value = "=sum(totaltagihan)";
            // 
            // textBox34
            // 
            this.textBox34.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.4251184463500977), Telerik.Reporting.Drawing.Unit.Inch(0.078740119934082031));
            this.textBox34.Name = "textBox34";
            this.textBox34.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2991341352462769), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox34.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox34.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox34.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox34.Style.Color = System.Drawing.Color.DarkMagenta;
            this.textBox34.Style.Font.Bold = true;
            this.textBox34.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox34.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox34.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox34.Value = "Total : ";
            // 
            // group4
            // 
            this.group4.GroupFooter = this.groupFooterSection4;
            this.group4.GroupHeader = this.groupHeaderSection4;
            this.group4.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("=PaymentTypeName")});
            this.group4.Name = "group4";
            // 
            // groupFooterSection4
            // 
            this.groupFooterSection4.Height = Telerik.Reporting.Drawing.Unit.Cm(0.53175854682922363);
            this.groupFooterSection4.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox50,
            this.textBox51,
            this.textBox61,
            this.textBox62,
            this.textBox67,
            this.textBox70,
            this.textBox26,
            this.textBox57});
            this.groupFooterSection4.Name = "groupFooterSection4";
            // 
            // textBox50
            // 
            this.textBox50.Format = "{0:N2}";
            this.textBox50.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.5814061164855957), Telerik.Reporting.Drawing.Unit.Inch(7.8837074397597462E-05));
            this.textBox50.Name = "textBox69";
            this.textBox50.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.83463209867477417), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox50.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox50.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox50.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox50.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox50.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox50.Style.Color = System.Drawing.Color.Black;
            this.textBox50.Style.Font.Bold = false;
            this.textBox50.Style.Font.Name = "Tahoma";
            this.textBox50.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox50.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox50.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox50.Value = "=sum(DPReturn)";
            // 
            // textBox51
            // 
            this.textBox51.Format = "{0:N2}";
            this.textBox51.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.7536416053771973), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox51.Name = "textBox69";
            this.textBox51.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82768672704696655), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox51.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox51.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox51.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox51.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox51.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox51.Style.Color = System.Drawing.Color.Black;
            this.textBox51.Style.Font.Bold = false;
            this.textBox51.Style.Font.Name = "Tahoma";
            this.textBox51.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox51.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox51.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox51.Value = "=sum(DPOffset)";
            // 
            // textBox61
            // 
            this.textBox61.Format = "{0:N2}";
            this.textBox61.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9180088043212891), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox61.Name = "textBox42";
            this.textBox61.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8308594822883606), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox61.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox61.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox61.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox61.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox61.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox61.Style.Color = System.Drawing.Color.Black;
            this.textBox61.Style.Font.Bold = false;
            this.textBox61.Style.Font.Name = "Tahoma";
            this.textBox61.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox61.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox61.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox61.Value = "=sum(DebitCard + Transfer)";
            // 
            // textBox62
            // 
            this.textBox62.Format = "{0:N2}";
            this.textBox62.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.0542948246002197), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox62.Name = "textBox41";
            this.textBox62.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.86363506317138672), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox62.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox62.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox62.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox62.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox62.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox62.Style.Color = System.Drawing.Color.Black;
            this.textBox62.Style.Font.Bold = false;
            this.textBox62.Style.Font.Name = "Tahoma";
            this.textBox62.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox62.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox62.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox62.Value = "=sum(CreditCard)";
            // 
            // textBox67
            // 
            this.textBox67.Format = "{0:N2}";
            this.textBox67.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.2292823791503906), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox67.Name = "textBox38";
            this.textBox67.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82493335008621216), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox67.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox67.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox67.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox67.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox67.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox67.Style.Color = System.Drawing.Color.Black;
            this.textBox67.Style.Font.Bold = false;
            this.textBox67.Style.Font.Name = "Tahoma";
            this.textBox67.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox67.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox67.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox67.Value = "=sum(Cash)";
            // 
            // textBox70
            // 
            this.textBox70.Format = "Total {0} :";
            this.textBox70.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.6483728289604187), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox70.Name = "textBox21";
            this.textBox70.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.571196436882019), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox70.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox70.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox70.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox70.Style.Color = System.Drawing.Color.Black;
            this.textBox70.Style.Font.Bold = false;
            this.textBox70.Style.Font.Name = "Tahoma";
            this.textBox70.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox70.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox70.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox70.Value = "=PaymentTypeName";
            // 
            // textBox26
            // 
            this.textBox26.Format = "{0:N2}";
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.4161152839660645), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox26.Name = "textBox69";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82569551467895508), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox26.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox26.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox26.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox26.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox26.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox26.Style.Color = System.Drawing.Color.Black;
            this.textBox26.Style.Font.Bold = false;
            this.textBox26.Style.Font.Name = "Tahoma";
            this.textBox26.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox26.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox26.Value = "=sum(Retur)";
            // 
            // textBox57
            // 
            this.textBox57.Format = "{0:N2}";
            this.textBox57.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.2418885231018066), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox57.Name = "textBox57";
            this.textBox57.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82712942361831665), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox57.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox57.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox57.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox57.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox57.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox57.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox57.Style.Color = System.Drawing.Color.Black;
            this.textBox57.Style.Font.Bold = false;
            this.textBox57.Style.Font.Name = "Tahoma";
            this.textBox57.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox57.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox57.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox57.Value = "=Sum(TotalAmount)";
            // 
            // groupHeaderSection4
            // 
            this.groupHeaderSection4.Height = Telerik.Reporting.Drawing.Unit.Cm(2.4509332180023193);
            this.groupHeaderSection4.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox6,
            this.textBox7,
            this.textBox53,
            this.textBox54,
            this.textBox55,
            this.textBox20,
            this.textBox65,
            this.textBox36,
            this.textBox14,
            this.textBox35,
            this.textBox25,
            this.textBox3,
            this.textBox49,
            this.textBox10,
            this.textBox12,
            this.textBox48,
            this.textBox101,
            this.textBox102,
            this.textBox103});
            this.groupHeaderSection4.Name = "groupHeaderSection4";
            this.groupHeaderSection4.PrintOnEveryPage = true;
            this.groupHeaderSection4.Style.Visible = true;
            // 
            // textBox25
            // 
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.024767279624939), Telerik.Reporting.Drawing.Unit.Inch(0.22790591418743134));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7201098203659058), Telerik.Reporting.Drawing.Unit.Inch(0.15748023986816406));
            this.textBox25.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox25.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox25.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox25.Style.Color = System.Drawing.Color.Black;
            this.textBox25.Style.Font.Bold = false;
            this.textBox25.Style.Font.Name = "Tahoma";
            this.textBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox25.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox25.Value = "=Layanan";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.86728668212890625), Telerik.Reporting.Drawing.Unit.Inch(0.38546499609947205));
            this.textBox3.Name = "textBox40";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.13295619189739227), Telerik.Reporting.Drawing.Unit.Inch(0.15748023986816406));
            this.textBox3.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox3.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox3.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox3.Style.Color = System.Drawing.Color.Black;
            this.textBox3.Style.Font.Bold = false;
            this.textBox3.Style.Font.Name = "Tahoma";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = ":";
            // 
            // textBox49
            // 
            this.textBox49.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.156207874417305), Telerik.Reporting.Drawing.Unit.Inch(0.38546499609947205));
            this.textBox49.Name = "textBox49";
            this.textBox49.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.65887469053268433), Telerik.Reporting.Drawing.Unit.Inch(0.15748023986816406));
            this.textBox49.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox49.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox49.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox49.Style.Color = System.Drawing.Color.Black;
            this.textBox49.Style.Font.Bold = false;
            this.textBox49.Style.Font.Name = "Tahoma";
            this.textBox49.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox49.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox49.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox49.Value = "Tipe";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.156207874417305), Telerik.Reporting.Drawing.Unit.Inch(0.22790591418743134));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.66583317518234253), Telerik.Reporting.Drawing.Unit.Inch(0.15748023986816406));
            this.textBox10.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox10.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox10.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox10.Style.Color = System.Drawing.Color.Black;
            this.textBox10.Style.Font.Bold = false;
            this.textBox10.Style.Font.Name = "Tahoma";
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox10.Value = "Layanan";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.86728668212890625), Telerik.Reporting.Drawing.Unit.Inch(0.22790591418743134));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.13295619189739227), Telerik.Reporting.Drawing.Unit.Inch(0.15748023986816406));
            this.textBox12.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox12.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox12.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox12.Style.Color = System.Drawing.Color.Black;
            this.textBox12.Style.Font.Bold = false;
            this.textBox12.Style.Font.Name = "Tahoma";
            this.textBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.Value = ":";
            // 
            // textBox101
            // 
            this.textBox101.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.156207874417305), Telerik.Reporting.Drawing.Unit.Inch(0.063857398927211761));
            this.textBox101.Name = "textBox101";
            this.textBox101.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.66583317518234253), Telerik.Reporting.Drawing.Unit.Inch(0.15748023986816406));
            this.textBox101.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox101.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox101.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox101.Style.Color = System.Drawing.Color.Black;
            this.textBox101.Style.Font.Bold = false;
            this.textBox101.Style.Font.Name = "Tahoma";
            this.textBox101.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox101.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox101.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox101.Value = "Jenis";
            // 
            // textBox102
            // 
            this.textBox102.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.86728668212890625), Telerik.Reporting.Drawing.Unit.Inch(0.063857398927211761));
            this.textBox102.Name = "textBox102";
            this.textBox102.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.13295619189739227), Telerik.Reporting.Drawing.Unit.Inch(0.15748023986816406));
            this.textBox102.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox102.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox102.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox102.Style.Color = System.Drawing.Color.Black;
            this.textBox102.Style.Font.Bold = false;
            this.textBox102.Style.Font.Name = "Tahoma";
            this.textBox102.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox102.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox102.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox102.Value = ":";
            // 
            // textBox103
            // 
            this.textBox103.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.024767279624939), Telerik.Reporting.Drawing.Unit.Inch(0.070346832275390625));
            this.textBox103.Name = "textBox103";
            this.textBox103.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7201098203659058), Telerik.Reporting.Drawing.Unit.Inch(0.15748023986816406));
            this.textBox103.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox103.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox103.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox103.Style.Color = System.Drawing.Color.Black;
            this.textBox103.Style.Font.Bold = false;
            this.textBox103.Style.Font.Name = "Tahoma";
            this.textBox103.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox103.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox103.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox103.Value = "=PaymentMethodName";
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.13229165971279144);
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // group3
            // 
            this.group3.GroupFooter = this.groupFooterSection3;
            this.group3.GroupHeader = this.groupHeaderSection3;
            this.group3.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("=PaymentNo")});
            this.group3.Name = "group3";
            // 
            // groupFooterSection3
            // 
            this.groupFooterSection3.Height = Telerik.Reporting.Drawing.Unit.Inch(0.23958349227905273);
            this.groupFooterSection3.Name = "groupFooterSection3";
            this.groupFooterSection3.Style.Visible = false;
            // 
            // groupHeaderSection3
            // 
            this.groupHeaderSection3.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20675651729106903);
            this.groupHeaderSection3.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox4,
            this.textBox1,
            this.textBox8,
            this.textBox86,
            this.textBox11});
            this.groupHeaderSection3.Name = "groupHeaderSection3";
            // 
            // textBox86
            // 
            this.textBox86.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.4515585899353027), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505));
            this.textBox86.Name = "textBox86";
            this.textBox86.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.6070430278778076), Telerik.Reporting.Drawing.Unit.Inch(0.16000001132488251));
            this.textBox86.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox86.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox86.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox86.Style.Font.Bold = false;
            this.textBox86.Style.Font.Name = "Tahoma";
            this.textBox86.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox86.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox86.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox86.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox86.Value = "=GuarantorName";
            // 
            // group5
            // 
            this.group5.GroupFooter = this.groupFooterSection5;
            this.group5.GroupHeader = this.groupHeaderSection5;
            this.group5.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("=Layanan")});
            this.group5.Name = "group5";
            // 
            // groupFooterSection5
            // 
            this.groupFooterSection5.Height = Telerik.Reporting.Drawing.Unit.Inch(0.18962287902832031);
            this.groupFooterSection5.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox58,
            this.textBox59,
            this.textBox64,
            this.textBox80,
            this.textBox85,
            this.textBox93,
            this.textBox96,
            this.textBox97});
            this.groupFooterSection5.Name = "groupFooterSection5";
            // 
            // textBox58
            // 
            this.textBox58.Format = "Total {0} :";
            this.textBox58.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.6438172459602356), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox58.Name = "textBox58";
            this.textBox58.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.571196436882019), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox58.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox58.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox58.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox58.Style.Color = System.Drawing.Color.Black;
            this.textBox58.Style.Font.Bold = false;
            this.textBox58.Style.Font.Name = "Tahoma";
            this.textBox58.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox58.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox58.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox58.Value = "=Layanan";
            // 
            // textBox59
            // 
            this.textBox59.Format = "{0:N2}";
            this.textBox59.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.2292823791503906), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox59.Name = "textBox59";
            this.textBox59.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82493335008621216), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox59.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox59.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox59.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox59.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox59.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox59.Style.Color = System.Drawing.Color.Black;
            this.textBox59.Style.Font.Bold = false;
            this.textBox59.Style.Font.Name = "Tahoma";
            this.textBox59.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox59.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox59.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox59.Value = "=sum(Cash)";
            // 
            // textBox64
            // 
            this.textBox64.Format = "{0:N2}";
            this.textBox64.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.0542945861816406), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox64.Name = "textBox64";
            this.textBox64.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.86363506317138672), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox64.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox64.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox64.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox64.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox64.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox64.Style.Color = System.Drawing.Color.Black;
            this.textBox64.Style.Font.Bold = false;
            this.textBox64.Style.Font.Name = "Tahoma";
            this.textBox64.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox64.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox64.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox64.Value = "=sum(CreditCard)";
            // 
            // textBox80
            // 
            this.textBox80.Format = "{0:N2}";
            this.textBox80.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9213306903839111), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox80.Name = "textBox80";
            this.textBox80.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8308594822883606), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox80.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox80.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox80.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox80.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox80.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox80.Style.Color = System.Drawing.Color.Black;
            this.textBox80.Style.Font.Bold = false;
            this.textBox80.Style.Font.Name = "Tahoma";
            this.textBox80.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox80.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox80.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox80.Value = "=sum(DebitCard + Transfer)";
            // 
            // textBox85
            // 
            this.textBox85.Format = "{0:N2}";
            this.textBox85.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.7536425590515137), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox85.Name = "textBox85";
            this.textBox85.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82768672704696655), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox85.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox85.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox85.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox85.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox85.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox85.Style.Color = System.Drawing.Color.Black;
            this.textBox85.Style.Font.Bold = false;
            this.textBox85.Style.Font.Name = "Tahoma";
            this.textBox85.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox85.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox85.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox85.Value = "=sum(DPOffset)";
            // 
            // textBox93
            // 
            this.textBox93.Format = "{0:N2}";
            this.textBox93.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.5814080238342285), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox93.Name = "textBox93";
            this.textBox93.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.83463209867477417), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox93.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox93.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox93.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox93.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox93.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox93.Style.Color = System.Drawing.Color.Black;
            this.textBox93.Style.Font.Bold = false;
            this.textBox93.Style.Font.Name = "Tahoma";
            this.textBox93.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox93.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox93.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox93.Value = "=sum(DPReturn)";
            // 
            // textBox96
            // 
            this.textBox96.Format = "{0:N2}";
            this.textBox96.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.41611909866333), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox96.Name = "textBox96";
            this.textBox96.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82569551467895508), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox96.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox96.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox96.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox96.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox96.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox96.Style.Color = System.Drawing.Color.Black;
            this.textBox96.Style.Font.Bold = false;
            this.textBox96.Style.Font.Name = "Tahoma";
            this.textBox96.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox96.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox96.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox96.Value = "=sum(Retur)";
            // 
            // textBox97
            // 
            this.textBox97.Format = "{0:N2}";
            this.textBox97.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.24231481552124), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox97.Name = "textBox97";
            this.textBox97.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82712942361831665), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox97.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox97.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox97.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox97.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox97.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox97.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox97.Style.Color = System.Drawing.Color.Black;
            this.textBox97.Style.Font.Bold = false;
            this.textBox97.Style.Font.Name = "Tahoma";
            this.textBox97.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox97.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox97.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox97.Value = "=Sum(TotalAmount)";
            // 
            // groupHeaderSection5
            // 
            this.groupHeaderSection5.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699);
            this.groupHeaderSection5.Name = "groupHeaderSection5";
            this.groupHeaderSection5.PrintOnEveryPage = true;
            this.groupHeaderSection5.Style.Visible = false;
            // 
            // group6
            // 
            this.group6.GroupFooter = this.groupFooterSection6;
            this.group6.GroupHeader = this.groupHeaderSection6;
            this.group6.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("PaymentMethodName")});
            this.group6.Name = "group6";
            // 
            // groupFooterSection6
            // 
            this.groupFooterSection6.Height = Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259);
            this.groupFooterSection6.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox104,
            this.textBox105,
            this.textBox106,
            this.textBox107,
            this.textBox108,
            this.textBox109,
            this.textBox110,
            this.textBox111});
            this.groupFooterSection6.Name = "groupFooterSection6";
            // 
            // textBox104
            // 
            this.textBox104.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.6438172459602356), Telerik.Reporting.Drawing.Unit.Inch(0.021686553955078125));
            this.textBox104.Name = "textBox104";
            this.textBox104.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5904084444046021), Telerik.Reporting.Drawing.Unit.Inch(0.16789691150188446));
            this.textBox104.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox104.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox104.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox104.Style.Color = System.Drawing.Color.Black;
            this.textBox104.Style.Font.Bold = false;
            this.textBox104.Style.Font.Name = "Tahoma";
            this.textBox104.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox104.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox104.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox104.Value = "=PaymentMethodName";
            // 
            // textBox105
            // 
            this.textBox105.Format = "{0:N2}";
            this.textBox105.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.2343044281005859), Telerik.Reporting.Drawing.Unit.Inch(0.021686553955078125));
            this.textBox105.Name = "textBox105";
            this.textBox105.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82493335008621216), Telerik.Reporting.Drawing.Unit.Inch(0.16789691150188446));
            this.textBox105.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox105.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox105.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox105.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox105.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox105.Style.Color = System.Drawing.Color.Black;
            this.textBox105.Style.Font.Bold = false;
            this.textBox105.Style.Font.Name = "Tahoma";
            this.textBox105.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox105.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox105.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox105.Value = "=sum(Cash)";
            // 
            // textBox106
            // 
            this.textBox106.Format = "{0:N2}";
            this.textBox106.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.0593166351318359), Telerik.Reporting.Drawing.Unit.Inch(0.021686553955078125));
            this.textBox106.Name = "textBox106";
            this.textBox106.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.86363506317138672), Telerik.Reporting.Drawing.Unit.Inch(0.16789691150188446));
            this.textBox106.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox106.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox106.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox106.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox106.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox106.Style.Color = System.Drawing.Color.Black;
            this.textBox106.Style.Font.Bold = false;
            this.textBox106.Style.Font.Name = "Tahoma";
            this.textBox106.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox106.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox106.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox106.Value = "=sum(CreditCard)";
            // 
            // textBox107
            // 
            this.textBox107.Format = "{0:N2}";
            this.textBox107.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9230306148529053), Telerik.Reporting.Drawing.Unit.Inch(0.021686553955078125));
            this.textBox107.Name = "textBox107";
            this.textBox107.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8308594822883606), Telerik.Reporting.Drawing.Unit.Inch(0.16789691150188446));
            this.textBox107.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox107.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox107.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox107.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox107.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox107.Style.Color = System.Drawing.Color.Black;
            this.textBox107.Style.Font.Bold = false;
            this.textBox107.Style.Font.Name = "Tahoma";
            this.textBox107.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox107.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox107.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox107.Value = "=sum(DebitCard + Transfer)";
            // 
            // textBox108
            // 
            this.textBox108.Format = "{0:N2}";
            this.textBox108.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.7539691925048828), Telerik.Reporting.Drawing.Unit.Inch(0.021686553955078125));
            this.textBox108.Name = "textBox108";
            this.textBox108.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82768672704696655), Telerik.Reporting.Drawing.Unit.Inch(0.16789691150188446));
            this.textBox108.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox108.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox108.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox108.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox108.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox108.Style.Color = System.Drawing.Color.Black;
            this.textBox108.Style.Font.Bold = false;
            this.textBox108.Style.Font.Name = "Tahoma";
            this.textBox108.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox108.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox108.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox108.Value = "=sum(DPOffset)";
            // 
            // textBox109
            // 
            this.textBox109.Format = "{0:N2}";
            this.textBox109.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.5817351341247559), Telerik.Reporting.Drawing.Unit.Inch(0.021686553955078125));
            this.textBox109.Name = "textBox109";
            this.textBox109.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.83463209867477417), Telerik.Reporting.Drawing.Unit.Inch(0.16789691150188446));
            this.textBox109.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox109.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox109.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox109.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox109.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox109.Style.Color = System.Drawing.Color.Black;
            this.textBox109.Style.Font.Bold = false;
            this.textBox109.Style.Font.Name = "Tahoma";
            this.textBox109.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox109.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox109.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox109.Value = "=sum(DPReturn)";
            // 
            // textBox110
            // 
            this.textBox110.Format = "{0:N2}";
            this.textBox110.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.4165401458740234), Telerik.Reporting.Drawing.Unit.Inch(0.021686553955078125));
            this.textBox110.Name = "textBox110";
            this.textBox110.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82569551467895508), Telerik.Reporting.Drawing.Unit.Inch(0.16789691150188446));
            this.textBox110.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox110.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox110.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox110.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox110.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox110.Style.Color = System.Drawing.Color.Black;
            this.textBox110.Style.Font.Bold = false;
            this.textBox110.Style.Font.Name = "Tahoma";
            this.textBox110.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox110.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox110.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox110.Value = "=sum(Retur)";
            // 
            // textBox111
            // 
            this.textBox111.Format = "{0:N2}";
            this.textBox111.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.24231481552124), Telerik.Reporting.Drawing.Unit.Inch(0.021686553955078125));
            this.textBox111.Name = "textBox111";
            this.textBox111.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82712942361831665), Telerik.Reporting.Drawing.Unit.Inch(0.16789691150188446));
            this.textBox111.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox111.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox111.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox111.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox111.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox111.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox111.Style.Color = System.Drawing.Color.Black;
            this.textBox111.Style.Font.Bold = false;
            this.textBox111.Style.Font.Name = "Tahoma";
            this.textBox111.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox111.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox111.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox111.Value = "=Sum(TotalAmount)";
            // 
            // groupHeaderSection6
            // 
            this.groupHeaderSection6.Height = Telerik.Reporting.Drawing.Unit.Inch(0.084563575685024261);
            this.groupHeaderSection6.Name = "groupHeaderSection6";
            this.groupHeaderSection6.PrintOnEveryPage = true;
            this.groupHeaderSection6.Style.Visible = false;
            // 
            // PaymentReceiveByCashierRpt2
            // 
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            this.group1,
            this.group2,
            this.group6,
            this.group5,
            this.group4,
            this.group3});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection1,
            this.groupFooterSection1,
            this.groupHeaderSection2,
            this.groupFooterSection2,
            this.groupHeaderSection6,
            this.groupFooterSection6,
            this.groupHeaderSection5,
            this.groupFooterSection5,
            this.groupHeaderSection4,
            this.groupFooterSection4,
            this.groupHeaderSection3,
            this.groupFooterSection3,
            this.pageHeader,
            this.detail,
            this.pageFooter,
            this.reportFooterSection1});
            this.Name = "PaymentReceiveByCashierRpt";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Sortings.AddRange(new Telerik.Reporting.Sorting[] {
            new Telerik.Reporting.Sorting("=PaymentNo", Telerik.Reporting.SortDirection.Asc)});
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Bold = true;
            this.Style.Font.Name = "Courier New";
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(20.496387481689453);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox txtPeriod;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox11;
        private Group group1;
        private GroupFooterSection groupFooterSection1;
        private GroupHeaderSection groupHeaderSection1;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox txtUserID;
        private Telerik.Reporting.TextBox textBox21;
        private Group group2;
        private GroupFooterSection groupFooterSection2;
        private GroupHeaderSection groupHeaderSection2;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox textBox33;
        private Telerik.Reporting.TextBox textBox34;
        private Telerik.Reporting.TextBox textBox40;
        private Telerik.Reporting.TextBox textBox39;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox textBox38;
        private Telerik.Reporting.TextBox textBox41;
        private Telerik.Reporting.TextBox textBox42;
        private Telerik.Reporting.TextBox textBox44;
        private Telerik.Reporting.TextBox textBox45;
        private Telerik.Reporting.TextBox textBox46;
        private Telerik.Reporting.TextBox textBox47;
        private Telerik.Reporting.TextBox textBox48;
        private Telerik.Reporting.TextBox textBox53;
        private Telerik.Reporting.TextBox textBox54;
        private Telerik.Reporting.TextBox textBox55;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox66;
        private Telerik.Reporting.TextBox textBox69;
        private Telerik.Reporting.TextBox textBox68;
        private Telerik.Reporting.TextBox textBox65;
        private Telerik.Reporting.TextBox textBox37;
        private Telerik.Reporting.TextBox textBox72;
        private Telerik.Reporting.TextBox textBox71;
        private Telerik.Reporting.TextBox textBox36;
        private Group group4;
        private GroupFooterSection groupFooterSection4;
        private GroupHeaderSection groupHeaderSection4;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox49;
        private Telerik.Reporting.TextBox textBox50;
        private Telerik.Reporting.TextBox textBox51;
        private Telerik.Reporting.TextBox textBox61;
        private Telerik.Reporting.TextBox textBox62;
        private Telerik.Reporting.TextBox textBox67;
        private Telerik.Reporting.TextBox textBox70;
        private Telerik.Reporting.TextBox textBox73;
        private ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.TextBox textBox74;
        private Telerik.Reporting.TextBox textBox75;
        private Telerik.Reporting.TextBox textBox76;
        private Telerik.Reporting.TextBox textBox77;
        private Telerik.Reporting.TextBox textBox78;
        private Telerik.Reporting.TextBox textBox79;
        private Telerik.Reporting.TextBox textBox83;
        private Telerik.Reporting.TextBox textBox84;
        private Telerik.Reporting.TextBox textBox88;
        private Telerik.Reporting.TextBox textBox89;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox43;
        private Telerik.Reporting.TextBox textBox52;
        private Telerik.Reporting.TextBox textBox60;
        private Telerik.Reporting.TextBox textBox56;
        private Telerik.Reporting.TextBox textBox57;
        private Telerik.Reporting.TextBox textBox63;
        private Telerik.Reporting.TextBox textBox81;
        private Telerik.Reporting.TextBox textBox35;
        private Group group3;
        private GroupFooterSection groupFooterSection3;
        private GroupHeaderSection groupHeaderSection3;
        private Telerik.Reporting.TextBox textBox28;
        private Telerik.Reporting.TextBox textBox82;
        private Telerik.Reporting.TextBox textBox86;
        private Group group5;
        private GroupFooterSection groupFooterSection5;
        private GroupHeaderSection groupHeaderSection5;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox58;
        private Telerik.Reporting.TextBox textBox59;
        private Telerik.Reporting.TextBox textBox64;
        private Telerik.Reporting.TextBox textBox80;
        private Telerik.Reporting.TextBox textBox85;
        private Telerik.Reporting.TextBox textBox93;
        private Telerik.Reporting.TextBox textBox96;
        private Telerik.Reporting.TextBox textBox97;
        private Telerik.Reporting.TextBox textBox87;
        private Telerik.Reporting.TextBox textBox90;
        private Telerik.Reporting.TextBox textBox91;
        private Telerik.Reporting.TextBox textBox92;
        private Telerik.Reporting.TextBox textBox94;
        private Telerik.Reporting.TextBox textBox95;
        private Telerik.Reporting.TextBox textBox98;
        private Telerik.Reporting.TextBox textBox99;
        private Telerik.Reporting.TextBox textBox100;
        private Group group6;
        private GroupFooterSection groupFooterSection6;
        private GroupHeaderSection groupHeaderSection6;
        private Telerik.Reporting.TextBox textBox101;
        private Telerik.Reporting.TextBox textBox102;
        private Telerik.Reporting.TextBox textBox103;
        private Telerik.Reporting.TextBox textBox104;
        private Telerik.Reporting.TextBox textBox105;
        private Telerik.Reporting.TextBox textBox106;
        private Telerik.Reporting.TextBox textBox107;
        private Telerik.Reporting.TextBox textBox108;
        private Telerik.Reporting.TextBox textBox109;
        private Telerik.Reporting.TextBox textBox110;
        private Telerik.Reporting.TextBox textBox111;
    }
}