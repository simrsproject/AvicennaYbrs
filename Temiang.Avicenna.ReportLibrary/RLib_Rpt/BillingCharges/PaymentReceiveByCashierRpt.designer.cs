namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class PaymentReceiveByCashierRpt
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
            this.textBox93 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox58 = new Telerik.Reporting.TextBox();
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
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox69 = new Telerik.Reporting.TextBox();
            this.textBox72 = new Telerik.Reporting.TextBox();
            this.textBox44 = new Telerik.Reporting.TextBox();
            this.textBox45 = new Telerik.Reporting.TextBox();
            this.textBox46 = new Telerik.Reporting.TextBox();
            this.textBox47 = new Telerik.Reporting.TextBox();
            this.textBox41 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.textBox60 = new Telerik.Reporting.TextBox();
            this.textBox87 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox74 = new Telerik.Reporting.TextBox();
            this.textBox76 = new Telerik.Reporting.TextBox();
            this.textBox77 = new Telerik.Reporting.TextBox();
            this.textBox78 = new Telerik.Reporting.TextBox();
            this.textBox80 = new Telerik.Reporting.TextBox();
            this.textBox83 = new Telerik.Reporting.TextBox();
            this.textBox84 = new Telerik.Reporting.TextBox();
            this.textBox85 = new Telerik.Reporting.TextBox();
            this.textBox88 = new Telerik.Reporting.TextBox();
            this.textBox89 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox73 = new Telerik.Reporting.TextBox();
            this.textBox75 = new Telerik.Reporting.TextBox();
            this.textBox79 = new Telerik.Reporting.TextBox();
            this.textBox43 = new Telerik.Reporting.TextBox();
            this.textBox52 = new Telerik.Reporting.TextBox();
            this.textBox90 = new Telerik.Reporting.TextBox();
            this.textBox91 = new Telerik.Reporting.TextBox();
            this.textBox92 = new Telerik.Reporting.TextBox();
            this.textBox94 = new Telerik.Reporting.TextBox();
            this.textBox95 = new Telerik.Reporting.TextBox();
            this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.txtUserID = new Telerik.Reporting.TextBox();
            this.textBox40 = new Telerik.Reporting.TextBox();
            this.group2 = new Telerik.Reporting.Group();
            this.groupFooterSection2 = new Telerik.Reporting.GroupFooterSection();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
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
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox65 = new Telerik.Reporting.TextBox();
            this.textBox36 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox35 = new Telerik.Reporting.TextBox();
            this.textBox64 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.textBox33 = new Telerik.Reporting.TextBox();
            this.textBox34 = new Telerik.Reporting.TextBox();
            this.group4 = new Telerik.Reporting.Group();
            this.groupFooterSection4 = new Telerik.Reporting.GroupFooterSection();
            this.textBox50 = new Telerik.Reporting.TextBox();
            this.textBox51 = new Telerik.Reporting.TextBox();
            this.textBox59 = new Telerik.Reporting.TextBox();
            this.textBox61 = new Telerik.Reporting.TextBox();
            this.textBox62 = new Telerik.Reporting.TextBox();
            this.textBox67 = new Telerik.Reporting.TextBox();
            this.textBox70 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox57 = new Telerik.Reporting.TextBox();
            this.groupHeaderSection4 = new Telerik.Reporting.GroupHeaderSection();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox49 = new Telerik.Reporting.TextBox();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.group3 = new Telerik.Reporting.Group();
            this.groupFooterSection3 = new Telerik.Reporting.GroupFooterSection();
            this.groupHeaderSection3 = new Telerik.Reporting.GroupHeaderSection();
            this.textBox86 = new Telerik.Reporting.TextBox();
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
            this.txtPeriod.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.8818895816802979), Telerik.Reporting.Drawing.Unit.Inch(0.95208340883255));
            this.txtPeriod.Name = "txtPeriod";
            this.txtPeriod.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.0787396430969238), Telerik.Reporting.Drawing.Unit.Inch(0.13894350826740265));
            this.txtPeriod.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.txtPeriod.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.txtPeriod.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.txtPeriod.Style.Font.Bold = true;
            this.txtPeriod.Style.Font.Name = "Tahoma";
            this.txtPeriod.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(12);
            this.txtPeriod.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtPeriod.Value = "Periode:";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.8818895816802979), Telerik.Reporting.Drawing.Unit.Inch(0.71041673421859741));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.0787396430969238), Telerik.Reporting.Drawing.Unit.Inch(0.23843836784362793));
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Font.Name = "Tahoma";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(18);
            this.textBox5.Style.Font.Strikeout = false;
            this.textBox5.Style.Font.Underline = false;
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.Value = "Laporan Penerimaan Per Kasir";
            // 
            // textBox93
            // 
            this.textBox93.Format = "";
            this.textBox93.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox93.Name = "textBox93";
            this.textBox93.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.15624737739563), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox93.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox93.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox93.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox93.Style.Font.Bold = false;
            this.textBox93.Style.Font.Name = "Tahoma";
            this.textBox93.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox93.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox93.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox93.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox93.Value = "=Notes";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5736364126205444), Telerik.Reporting.Drawing.Unit.Inch(0.195901557803154));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5826505422592163), Telerik.Reporting.Drawing.Unit.Inch(0.26836362481117249));
            this.textBox7.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox7.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox7.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.Style.Font.Name = "Tahoma";
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.Value = "Nama";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.34628948569297791), Telerik.Reporting.Drawing.Unit.Inch(0.195901557803154));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2272680997848511), Telerik.Reporting.Drawing.Unit.Inch(0.26836362481117249));
            this.textBox6.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox6.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox6.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox6.Style.Font.Bold = true;
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
            this.textBox58,
            this.textBox66,
            this.textBox37,
            this.textBox2,
            this.textBox63,
            this.textBox81,
            this.textBox93});
            this.detail.Name = "detail";
            this.detail.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.detail.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            // 
            // textBox17
            // 
            this.textBox17.Format = "{0:N0}";
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.1563656330108643), Telerik.Reporting.Drawing.Unit.Inch(0.0155855817720294));
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
            this.textBox19.Format = "{0:N0}";
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9813778400421143), Telerik.Reporting.Drawing.Unit.Inch(0.015625));
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
            this.textBox29.Format = "{0:N0}";
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.8484139442443848), Telerik.Reporting.Drawing.Unit.Inch(0.015625));
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
            this.textBox29.Value = "=DebitCard";
            // 
            // textBox58
            // 
            this.textBox58.Format = "{0:N0}";
            this.textBox58.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.67603063583374), Telerik.Reporting.Drawing.Unit.Inch(0.015625));
            this.textBox58.Name = "textBox58";
            this.textBox58.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.81711578369140625), Telerik.Reporting.Drawing.Unit.Inch(0.18958337604999542));
            this.textBox58.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox58.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox58.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox58.Style.Font.Bold = false;
            this.textBox58.Style.Font.Name = "Tahoma";
            this.textBox58.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox58.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox58.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox58.Value = "=Transfer";
            // 
            // textBox66
            // 
            this.textBox66.Format = "{0:N0}";
            this.textBox66.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.4932246208190918), Telerik.Reporting.Drawing.Unit.Inch(0.0155855817720294));
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
            this.textBox37.Format = "{0:N0}";
            this.textBox37.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.3209896087646484), Telerik.Reporting.Drawing.Unit.Inch(0.015625));
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
            this.textBox2.Format = "{0:N0}";
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.1556987762451172), Telerik.Reporting.Drawing.Unit.Inch(0.0155855817720294));
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
            this.textBox63.Format = "{0:N0}";
            this.textBox63.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.981471061706543), Telerik.Reporting.Drawing.Unit.Inch(0.015625));
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
            this.textBox63.Value = "=Cash+CreditCard+DebitCard+Transfer-DPReturn-Retur";
            // 
            // textBox81
            // 
            this.textBox81.Format = "{0:N0}";
            this.textBox81.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(9.80910587310791), Telerik.Reporting.Drawing.Unit.Inch(0.014583375304937363));
            this.textBox81.Name = "textBox81";
            this.textBox81.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0161755084991455), Telerik.Reporting.Drawing.Unit.Inch(0.18954403698444367));
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
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.96177077293396), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.8029024600982666), Telerik.Reporting.Drawing.Unit.Inch(0.15999999642372131));
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
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5736364126205444), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3880554437637329), Telerik.Reporting.Drawing.Unit.Inch(0.15999999642372131));
            this.textBox8.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox8.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox8.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox8.Style.Font.Bold = false;
            this.textBox8.Style.Font.Name = "Tahoma";
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox8.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Value = "=RegistrationNo";
            // 
            // textBox1
            // 
            this.textBox1.Format = "{0}.";
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.9472862068996619E-08), Telerik.Reporting.Drawing.Unit.Inch(0.00011801719665527344));
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
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.34628948569297791), Telerik.Reporting.Drawing.Unit.Inch(7.8678131103515625E-05));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2272680997848511), Telerik.Reporting.Drawing.Unit.Inch(0.15999999642372131));
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
            this.textBox48.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.84768390655517578), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox48.Name = "textBox48";
            this.textBox48.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.6823768615722656), Telerik.Reporting.Drawing.Unit.Inch(0.15748023986816406));
            this.textBox48.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox48.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox48.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox48.Style.Color = System.Drawing.Color.Black;
            this.textBox48.Style.Font.Bold = true;
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
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.8959527015686035), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.9292902946472168), Telerik.Reporting.Drawing.Unit.Inch(0.19999916851520538));
            this.textBox28.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
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
            this.textBox82.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox82.Name = "textBox82";
            this.textBox82.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.8958344459533691), Telerik.Reporting.Drawing.Unit.Inch(0.19999940693378449));
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
            this.groupFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(6.2987813949584961);
            this.groupFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox21,
            this.textBox38,
            this.textBox42,
            this.textBox25,
            this.textBox69,
            this.textBox72,
            this.textBox44,
            this.textBox45,
            this.textBox46,
            this.textBox47,
            this.textBox41,
            this.textBox32,
            this.textBox60,
            this.textBox87,
            this.textBox13,
            this.textBox74,
            this.textBox76,
            this.textBox77,
            this.textBox78,
            this.textBox80,
            this.textBox83,
            this.textBox84,
            this.textBox85,
            this.textBox88,
            this.textBox89,
            this.textBox9,
            this.textBox73,
            this.textBox75,
            this.textBox79,
            this.textBox43,
            this.textBox52,
            this.textBox90,
            this.textBox91,
            this.textBox92,
            this.textBox94,
            this.textBox95});
            this.groupFooterSection1.Name = "groupFooterSection1";
            this.groupFooterSection1.PageBreak = Telerik.Reporting.PageBreak.None;
            this.groupFooterSection1.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            // 
            // textBox21
            // 
            this.textBox21.Format = "Total Kasir {0} :";
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9378803194267675E-05), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.15624737739563), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox21.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox21.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox21.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox21.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox21.Style.Font.Bold = true;
            this.textBox21.Style.Font.Name = "Tahoma";
            this.textBox21.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox21.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox21.Value = "=CashierName";
            // 
            // textBox38
            // 
            this.textBox38.Format = "{0:N0}";
            this.textBox38.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.1563656330108643), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox38.Name = "textBox38";
            this.textBox38.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82493335008621216), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox38.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox38.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox38.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox38.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox38.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox38.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox38.Style.Font.Bold = true;
            this.textBox38.Style.Font.Name = "Tahoma";
            this.textBox38.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox38.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox38.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox38.Value = "=sum(Cash)";
            // 
            // textBox42
            // 
            this.textBox42.Format = "{0:N0}";
            this.textBox42.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.8450922966003418), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox42.Name = "textBox42";
            this.textBox42.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8308594822883606), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox42.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox42.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox42.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox42.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox42.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox42.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox42.Style.Font.Bold = true;
            this.textBox42.Style.Font.Name = "Tahoma";
            this.textBox42.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox42.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox42.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox42.Value = "=sum(DebitCard)";
            // 
            // textBox25
            // 
            this.textBox25.Format = "{0:N0}";
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.67603063583374), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.81711578369140625), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox25.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox25.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox25.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox25.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox25.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox25.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox25.Style.Font.Bold = true;
            this.textBox25.Style.Font.Name = "Tahoma";
            this.textBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox25.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox25.Value = "=sum(Transfer)";
            // 
            // textBox69
            // 
            this.textBox69.Format = "{0:N0}";
            this.textBox69.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.49322509765625), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox69.Name = "textBox69";
            this.textBox69.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82768672704696655), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox69.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox69.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox69.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox69.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox69.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox69.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox69.Style.Font.Bold = true;
            this.textBox69.Style.Font.Name = "Tahoma";
            this.textBox69.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox69.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox69.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox69.Value = "=sum(DPOffset)";
            // 
            // textBox72
            // 
            this.textBox72.Format = "{0:N0}";
            this.textBox72.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.3209896087646484), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox72.Name = "textBox69";
            this.textBox72.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.83463209867477417), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox72.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox72.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox72.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox72.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox72.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox72.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox72.Style.Font.Bold = true;
            this.textBox72.Style.Font.Name = "Tahoma";
            this.textBox72.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox72.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox72.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox72.Value = "=sum(DPReturn)";
            // 
            // textBox44
            // 
            this.textBox44.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.4109663963317871), Telerik.Reporting.Drawing.Unit.Inch(1.4305280447006226));
            this.textBox44.Name = "textBox44";
            this.textBox44.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7780011892318726), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox44.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox44.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox44.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox44.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox44.Style.Font.Bold = true;
            this.textBox44.Style.Font.Name = "Tahoma";
            this.textBox44.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox44.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox44.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox44.Value = "=CashierName";
            // 
            // textBox45
            // 
            this.textBox45.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.6499991416931152), Telerik.Reporting.Drawing.Unit.Inch(0.64312648773193359));
            this.textBox45.Name = "textBox45";
            this.textBox45.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.331395149230957), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox45.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox45.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox45.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox45.Style.Font.Bold = true;
            this.textBox45.Style.Font.Name = "Tahoma";
            this.textBox45.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox45.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox45.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox45.Value = "Kasir";
            // 
            // textBox46
            // 
            this.textBox46.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.1031136512756348), Telerik.Reporting.Drawing.Unit.Inch(1.4305280447006226));
            this.textBox46.Name = "textBox46";
            this.textBox46.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.30777326226234436), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox46.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox46.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox46.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox46.Style.Font.Bold = true;
            this.textBox46.Style.Font.Name = "Tahoma";
            this.textBox46.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox46.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox46.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox46.Value = "(";
            // 
            // textBox47
            // 
            this.textBox47.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(9.1890459060668945), Telerik.Reporting.Drawing.Unit.Inch(1.4305280447006226));
            this.textBox47.Name = "textBox47";
            this.textBox47.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.30777326226234436), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox47.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox47.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox47.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox47.Style.Font.Bold = true;
            this.textBox47.Style.Font.Name = "Tahoma";
            this.textBox47.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox47.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox47.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox47.Value = ")";
            // 
            // textBox41
            // 
            this.textBox41.Format = "{0:N0}";
            this.textBox41.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9813783168792725), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox41.Name = "textBox41";
            this.textBox41.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.86363506317138672), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox41.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox41.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox41.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox41.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox41.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox41.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox41.Style.Font.Bold = true;
            this.textBox41.Style.Font.Name = "Tahoma";
            this.textBox41.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox41.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox41.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox41.Value = "=sum(CreditCard)";
            // 
            // textBox32
            // 
            this.textBox32.Format = "{0:N0}";
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.1556997299194336), Telerik.Reporting.Drawing.Unit.Inch(7.8837074397597462E-05));
            this.textBox32.Name = "textBox69";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.825694739818573), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox32.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox32.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox32.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox32.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox32.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox32.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox32.Style.Font.Bold = true;
            this.textBox32.Style.Font.Name = "Tahoma";
            this.textBox32.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox32.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox32.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox32.Value = "=sum(Retur)";
            // 
            // textBox60
            // 
            this.textBox60.Format = "{0:N0}";
            this.textBox60.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.98147201538086), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox60.Name = "textBox9";
            this.textBox60.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82712942361831665), Telerik.Reporting.Drawing.Unit.Inch(0.18966229259967804));
            this.textBox60.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox60.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox60.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox60.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox60.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox60.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox60.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox60.Style.Font.Bold = true;
            this.textBox60.Style.Font.Name = "Tahoma";
            this.textBox60.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox60.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox60.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox60.Value = "=Sum(Cash+CreditCard+DebitCard+Transfer-DPReturn-Retur)";
            // 
            // textBox87
            // 
            this.textBox87.Format = "{0:N0}";
            this.textBox87.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(9.28860092163086), Telerik.Reporting.Drawing.Unit.Inch(2.1127817630767822));
            this.textBox87.Name = "textBox87";
            this.textBox87.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0144603252410889), Telerik.Reporting.Drawing.Unit.Inch(0.18711216747760773));
            this.textBox87.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Double;
            this.textBox87.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox87.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox87.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox87.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox87.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox87.Style.Color = System.Drawing.Color.Navy;
            this.textBox87.Style.Font.Bold = true;
            this.textBox87.Style.Font.Name = "Tahoma";
            this.textBox87.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox87.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox87.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox87.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox87.Value = "=Sum(TotalAmount)";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.3508996963500977), Telerik.Reporting.Drawing.Unit.Inch(1.8421087265014648));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.93804931640625), Telerik.Reporting.Drawing.Unit.Inch(0.26589310169219971));
            this.textBox13.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox13.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox13.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox13.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox13.Style.Color = System.Drawing.Color.Navy;
            this.textBox13.Style.Font.Name = "Tahoma";
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.Value = "Uang Disetor";
            // 
            // textBox74
            // 
            this.textBox74.Format = "{0:N0}";
            this.textBox74.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.6882197856903076), Telerik.Reporting.Drawing.Unit.Inch(2.1105508804321289));
            this.textBox74.Name = "textBox41";
            this.textBox74.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97699230909347534), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox74.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox74.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox74.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox74.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox74.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox74.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox74.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox74.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox74.Style.Font.Bold = true;
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
            this.textBox76.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1701248437166214), Telerik.Reporting.Drawing.Unit.Inch(1.8421093225479126));
            this.textBox76.Name = "textBox21";
            this.textBox76.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4001107215881348), Telerik.Reporting.Drawing.Unit.Inch(0.45778465270996094));
            this.textBox76.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox76.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox76.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox76.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox76.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox76.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox76.Style.Font.Bold = true;
            this.textBox76.Style.Font.Name = "Tahoma";
            this.textBox76.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox76.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox76.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox76.Value = "Grand Total";
            // 
            // textBox77
            // 
            this.textBox77.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5703144073486328), Telerik.Reporting.Drawing.Unit.Inch(1.8421081304550171));
            this.textBox77.Name = "textBox55";
            this.textBox77.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1178264617919922), Telerik.Reporting.Drawing.Unit.Inch(0.26589298248291016));
            this.textBox77.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox77.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox77.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox77.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox77.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox77.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox77.Style.Font.Bold = true;
            this.textBox77.Style.Font.Name = "Tahoma";
            this.textBox77.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox77.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox77.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox77.Style.Visible = true;
            this.textBox77.Value = "Tunai";
            // 
            // textBox78
            // 
            this.textBox78.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.6882197856903076), Telerik.Reporting.Drawing.Unit.Inch(1.8421081304550171));
            this.textBox78.Name = "textBox54";
            this.textBox78.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97699230909347534), Telerik.Reporting.Drawing.Unit.Inch(0.2658936083316803));
            this.textBox78.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox78.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox78.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox78.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox78.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox78.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox78.Style.Font.Bold = true;
            this.textBox78.Style.Font.Name = "Tahoma";
            this.textBox78.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox78.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox78.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox78.Style.Visible = true;
            this.textBox78.Value = "K.Kredit";
            // 
            // textBox80
            // 
            this.textBox80.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.631645679473877), Telerik.Reporting.Drawing.Unit.Inch(1.8421093225479126));
            this.textBox80.Name = "textBox10";
            this.textBox80.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.85751587152481079), Telerik.Reporting.Drawing.Unit.Inch(0.26589339971542358));
            this.textBox80.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox80.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox80.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox80.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox80.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox80.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox80.Style.Font.Bold = true;
            this.textBox80.Style.Font.Name = "Tahoma";
            this.textBox80.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox80.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox80.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox80.Style.Visible = true;
            this.textBox80.Value = "Transfer";
            // 
            // textBox83
            // 
            this.textBox83.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.4892406463623047), Telerik.Reporting.Drawing.Unit.Inch(1.8421093225479126));
            this.textBox83.Name = "textBox56";
            this.textBox83.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97825878858566284), Telerik.Reporting.Drawing.Unit.Inch(0.265892893075943));
            this.textBox83.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox83.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox83.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox83.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox83.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox83.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox83.Style.Font.Bold = true;
            this.textBox83.Style.Font.Name = "Tahoma";
            this.textBox83.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox83.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox83.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox83.Style.Visible = true;
            this.textBox83.Value = "DP Offset";
            // 
            // textBox84
            // 
            this.textBox84.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.4675784111022949), Telerik.Reporting.Drawing.Unit.Inch(1.8418697118759155));
            this.textBox84.Name = "textBox56";
            this.textBox84.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.98740893602371216), Telerik.Reporting.Drawing.Unit.Inch(0.26613259315490723));
            this.textBox84.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox84.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox84.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox84.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox84.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox84.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox84.Style.Font.Bold = true;
            this.textBox84.Style.Font.Name = "Tahoma";
            this.textBox84.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox84.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox84.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox84.Style.Visible = true;
            this.textBox84.Value = "DP Kembali";
            // 
            // textBox85
            // 
            this.textBox85.Format = "{0:N0}";
            this.textBox85.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.631645679473877), Telerik.Reporting.Drawing.Unit.Inch(2.1127817630767822));
            this.textBox85.Name = "textBox25";
            this.textBox85.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.85751587152481079), Telerik.Reporting.Drawing.Unit.Inch(0.18711216747760773));
            this.textBox85.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox85.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox85.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox85.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox85.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox85.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox85.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox85.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox85.Style.Font.Bold = true;
            this.textBox85.Style.Font.Name = "Tahoma";
            this.textBox85.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox85.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox85.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox85.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox85.Value = "=sum(Transfer)";
            // 
            // textBox88
            // 
            this.textBox88.Format = "{0:N0}";
            this.textBox88.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.4892406463623047), Telerik.Reporting.Drawing.Unit.Inch(2.1105508804321289));
            this.textBox88.Name = "textBox69";
            this.textBox88.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97825878858566284), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox88.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox88.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox88.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox88.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox88.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox88.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox88.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox88.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox88.Style.Font.Bold = true;
            this.textBox88.Style.Font.Name = "Tahoma";
            this.textBox88.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox88.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox88.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox88.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox88.Value = "=sum(DPOffset)";
            // 
            // textBox89
            // 
            this.textBox89.Format = "{0:N0}";
            this.textBox89.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.4675784111022949), Telerik.Reporting.Drawing.Unit.Inch(2.1103105545043945));
            this.textBox89.Name = "textBox69";
            this.textBox89.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.98740893602371216), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox89.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox89.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox89.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox89.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox89.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox89.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox89.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox89.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox89.Style.Font.Bold = true;
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
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.3508996963500977), Telerik.Reporting.Drawing.Unit.Inch(2.1149442195892334));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.93762332201004028), Telerik.Reporting.Drawing.Unit.Inch(0.18494987487792969));
            this.textBox9.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Double;
            this.textBox9.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox9.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox9.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox9.Style.Color = System.Drawing.Color.Navy;
            this.textBox9.Style.Font.Bold = true;
            this.textBox9.Style.Font.Name = "Tahoma";
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox9.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox9.Value = "=sum(Cash - DPReturn+Retur)";
            // 
            // textBox73
            // 
            this.textBox73.Format = "{0:N0}";
            this.textBox73.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5703144073486328), Telerik.Reporting.Drawing.Unit.Inch(2.1105508804321289));
            this.textBox73.Name = "textBox38";
            this.textBox73.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.11782705783844), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox73.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox73.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox73.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox73.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox73.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox73.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox73.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox73.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox73.Style.Font.Bold = true;
            this.textBox73.Style.Font.Name = "Tahoma";
            this.textBox73.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox73.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox73.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox73.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox73.Value = "=sum(Cash)";
            // 
            // textBox75
            // 
            this.textBox75.Format = "{0:N0}";
            this.textBox75.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.6686127185821533), Telerik.Reporting.Drawing.Unit.Inch(2.1127817630767822));
            this.textBox75.Name = "textBox42";
            this.textBox75.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.962954044342041), Telerik.Reporting.Drawing.Unit.Inch(0.18711216747760773));
            this.textBox75.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox75.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox75.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox75.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox75.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox75.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox75.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox75.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox75.Style.Font.Bold = true;
            this.textBox75.Style.Font.Name = "Tahoma";
            this.textBox75.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox75.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox75.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox75.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox75.Value = "=sum(DebitCard)";
            // 
            // textBox79
            // 
            this.textBox79.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.6652908325195312), Telerik.Reporting.Drawing.Unit.Inch(1.8418697118759155));
            this.textBox79.Name = "textBox53";
            this.textBox79.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.96627599000930786), Telerik.Reporting.Drawing.Unit.Inch(0.26613268256187439));
            this.textBox79.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox79.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox79.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox79.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox79.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox79.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox79.Style.Font.Bold = true;
            this.textBox79.Style.Font.Name = "Tahoma";
            this.textBox79.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox79.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox79.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox79.Style.Visible = true;
            this.textBox79.Value = "K.Debit";
            // 
            // textBox43
            // 
            this.textBox43.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.4550662040710449), Telerik.Reporting.Drawing.Unit.Inch(1.8421087265014648));
            this.textBox43.Name = "textBox56";
            this.textBox43.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.89575451612472534), Telerik.Reporting.Drawing.Unit.Inch(0.26589334011077881));
            this.textBox43.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox43.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox43.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox43.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox43.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox43.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox43.Style.Font.Bold = true;
            this.textBox43.Style.Font.Name = "Tahoma";
            this.textBox43.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox43.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox43.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox43.Style.Visible = true;
            this.textBox43.Value = "Retur";
            // 
            // textBox52
            // 
            this.textBox52.Format = "{0:N0}";
            this.textBox52.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.45506477355957), Telerik.Reporting.Drawing.Unit.Inch(2.1083199977874756));
            this.textBox52.Name = "textBox69";
            this.textBox52.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8957553505897522), Telerik.Reporting.Drawing.Unit.Inch(0.19157432019710541));
            this.textBox52.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox52.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox52.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox52.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox52.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox52.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox52.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox52.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox52.Style.Font.Bold = true;
            this.textBox52.Style.Font.Name = "Tahoma";
            this.textBox52.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox52.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox52.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox52.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox52.Value = "=sum(Retur)";
            // 
            // textBox90
            // 
            this.textBox90.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(9.289027214050293), Telerik.Reporting.Drawing.Unit.Inch(1.8396387100219727));
            this.textBox90.Name = "textBox90";
            this.textBox90.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0140334367752075), Telerik.Reporting.Drawing.Unit.Inch(0.26836311817169189));
            this.textBox90.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox90.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox90.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox90.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox90.Style.Color = System.Drawing.Color.Navy;
            this.textBox90.Style.Font.Name = "Tahoma";
            this.textBox90.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox90.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox90.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox90.Value = "Pendapatan";
            // 
            // textBox91
            // 
            this.textBox91.Format = "{0:N0}";
            this.textBox91.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5703144073486328), Telerik.Reporting.Drawing.Unit.Inch(0.64312648773193359));
            this.textBox91.Name = "textBox91";
            this.textBox91.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.11782705783844), Telerik.Reporting.Drawing.Unit.Inch(0.28333345055580139));
            this.textBox91.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox91.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox91.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox91.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox91.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox91.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox91.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox91.Style.Color = System.Drawing.Color.Indigo;
            this.textBox91.Style.Font.Bold = true;
            this.textBox91.Style.Font.Name = "Tahoma";
            this.textBox91.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox91.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox91.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox91.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox91.Value = "=sum(Farmasi)";
            // 
            // textBox92
            // 
            this.textBox92.Format = "{0:N0}";
            this.textBox92.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5703144073486328), Telerik.Reporting.Drawing.Unit.Inch(0.92653876543045044));
            this.textBox92.Name = "textBox92";
            this.textBox92.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.11782705783844), Telerik.Reporting.Drawing.Unit.Inch(0.262500137090683));
            this.textBox92.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox92.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox92.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox92.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox92.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox92.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox92.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox92.Style.Color = System.Drawing.Color.Indigo;
            this.textBox92.Style.Font.Bold = true;
            this.textBox92.Style.Font.Name = "Tahoma";
            this.textBox92.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox92.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox92.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox92.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox92.Value = "=sum(Perawatan)";
            // 
            // textBox94
            // 
            this.textBox94.Format = "{0:N0}";
            this.textBox94.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1701248437166214), Telerik.Reporting.Drawing.Unit.Inch(0.92653876543045044));
            this.textBox94.Name = "textBox94";
            this.textBox94.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4001107215881348), Telerik.Reporting.Drawing.Unit.Inch(0.262500137090683));
            this.textBox94.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox94.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox94.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox94.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox94.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox94.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox94.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox94.Style.Color = System.Drawing.Color.Indigo;
            this.textBox94.Style.Font.Bold = true;
            this.textBox94.Style.Font.Name = "Tahoma";
            this.textBox94.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox94.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox94.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox94.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox94.Value = "Total Perawatan :";
            // 
            // textBox95
            // 
            this.textBox95.Format = "{0:N0}";
            this.textBox95.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1701248437166214), Telerik.Reporting.Drawing.Unit.Inch(0.64312648773193359));
            this.textBox95.Name = "textBox95";
            this.textBox95.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4001107215881348), Telerik.Reporting.Drawing.Unit.Inch(0.28333345055580139));
            this.textBox95.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox95.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox95.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox95.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox95.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox95.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox95.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox95.Style.Color = System.Drawing.Color.Indigo;
            this.textBox95.Style.Font.Bold = true;
            this.textBox95.Style.Font.Name = "Tahoma";
            this.textBox95.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox95.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox95.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox95.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox95.Value = "Total Farmasi :";
            // 
            // groupHeaderSection1
            // 
            this.groupHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.54708302021026611);
            this.groupHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox16,
            this.txtUserID,
            this.textBox40});
            this.groupHeaderSection1.Name = "groupHeaderSection1";
            this.groupHeaderSection1.PrintOnEveryPage = true;
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.057906787842512131));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.65887469053268433), Telerik.Reporting.Drawing.Unit.Inch(0.15748023986816406));
            this.textBox16.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox16.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox16.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox16.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox16.Style.Font.Bold = true;
            this.textBox16.Style.Font.Name = "Tahoma";
            this.textBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox16.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox16.Value = "Kasir          ";
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.84768390655517578), Telerik.Reporting.Drawing.Unit.Inch(0.057906787842512131));
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.1336147785186768), Telerik.Reporting.Drawing.Unit.Inch(0.15748023986816406));
            this.txtUserID.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.txtUserID.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.txtUserID.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.txtUserID.Style.Color = System.Drawing.Color.DarkRed;
            this.txtUserID.Style.Font.Bold = true;
            this.txtUserID.Style.Font.Name = "Tahoma";
            this.txtUserID.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.txtUserID.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtUserID.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.txtUserID.Value = "=CashierName";
            // 
            // textBox40
            // 
            this.textBox40.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.69020342826843262), Telerik.Reporting.Drawing.Unit.Inch(0.057867366820573807));
            this.textBox40.Name = "textBox40";
            this.textBox40.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.13295619189739227), Telerik.Reporting.Drawing.Unit.Inch(0.15748023986816406));
            this.textBox40.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox40.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox40.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox40.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox40.Style.Font.Bold = true;
            this.textBox40.Style.Font.Name = "Tahoma";
            this.textBox40.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox40.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox40.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox40.Value = ":";
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
            this.textBox12,
            this.textBox68,
            this.textBox71,
            this.textBox15,
            this.textBox56});
            this.groupFooterSection2.Name = "groupFooterSection2";
            // 
            // textBox24
            // 
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9378803194267675E-05), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.15624737739563), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox24.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox24.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox24.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox24.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox24.Style.Color = System.Drawing.Color.DarkSlateBlue;
            this.textBox24.Style.Font.Bold = true;
            this.textBox24.Style.Font.Name = "Tahoma";
            this.textBox24.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox24.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox24.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox24.Value = "Total Per Tanggal : ";
            // 
            // textBox18
            // 
            this.textBox18.Format = "{0:N0}";
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.1563656330108643), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82493335008621216), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox18.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox18.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox18.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox18.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox18.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox18.Style.Color = System.Drawing.Color.DarkSlateBlue;
            this.textBox18.Style.Font.Bold = true;
            this.textBox18.Style.Font.Name = "Tahoma";
            this.textBox18.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox18.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox18.Value = "=sum(Cash)";
            // 
            // textBox27
            // 
            this.textBox27.Format = "{0:N0}";
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9813783168792725), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.86363506317138672), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox27.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox27.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox27.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox27.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox27.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox27.Style.Color = System.Drawing.Color.DarkSlateBlue;
            this.textBox27.Style.Font.Bold = true;
            this.textBox27.Style.Font.Name = "Tahoma";
            this.textBox27.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox27.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox27.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox27.Value = "=sum(CreditCard)";
            // 
            // textBox30
            // 
            this.textBox30.Format = "{0:N0}";
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.8450922966003418), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8308594822883606), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox30.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox30.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox30.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox30.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox30.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox30.Style.Color = System.Drawing.Color.DarkSlateBlue;
            this.textBox30.Style.Font.Bold = true;
            this.textBox30.Style.Font.Name = "Tahoma";
            this.textBox30.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox30.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox30.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox30.Value = "=sum(DebitCard)";
            // 
            // textBox12
            // 
            this.textBox12.Format = "{0:N0}";
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.67603063583374), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.81711578369140625), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox12.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox12.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox12.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox12.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox12.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox12.Style.Color = System.Drawing.Color.DarkSlateBlue;
            this.textBox12.Style.Font.Bold = true;
            this.textBox12.Style.Font.Name = "Tahoma";
            this.textBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.Value = "=sum(Transfer)";
            // 
            // textBox68
            // 
            this.textBox68.Format = "{0:N0}";
            this.textBox68.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.49322509765625), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox68.Name = "textBox32";
            this.textBox68.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82768672704696655), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox68.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox68.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox68.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox68.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox68.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox68.Style.Color = System.Drawing.Color.DarkSlateBlue;
            this.textBox68.Style.Font.Bold = true;
            this.textBox68.Style.Font.Name = "Tahoma";
            this.textBox68.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox68.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox68.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox68.Value = "=sum(DPOffset)";
            // 
            // textBox71
            // 
            this.textBox71.Format = "{0:N0}";
            this.textBox71.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.3209896087646484), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox71.Name = "textBox71";
            this.textBox71.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.83463209867477417), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox71.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox71.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox71.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox71.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox71.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox71.Style.Color = System.Drawing.Color.DarkSlateBlue;
            this.textBox71.Style.Font.Bold = true;
            this.textBox71.Style.Font.Name = "Tahoma";
            this.textBox71.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox71.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox71.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox71.Value = "=sum(DPReturn)";
            // 
            // textBox15
            // 
            this.textBox15.Format = "{0:N0}";
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.1556997299194336), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox15.Name = "textBox71";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.825694739818573), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox15.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox15.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox15.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox15.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox15.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox15.Style.Color = System.Drawing.Color.DarkSlateBlue;
            this.textBox15.Style.Font.Bold = true;
            this.textBox15.Style.Font.Name = "Tahoma";
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox15.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox15.Value = "=sum(Retur)";
            // 
            // textBox56
            // 
            this.textBox56.Format = "{0:N0}";
            this.textBox56.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.981898307800293), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox56.Name = "textBox9";
            this.textBox56.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82712942361831665), Telerik.Reporting.Drawing.Unit.Inch(0.1875));
            this.textBox56.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox56.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox56.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox56.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox56.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox56.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox56.Style.Color = System.Drawing.Color.DarkSlateBlue;
            this.textBox56.Style.Font.Bold = true;
            this.textBox56.Style.Font.Name = "Tahoma";
            this.textBox56.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox56.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox56.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox56.Value = "=Sum(Cash+CreditCard+DebitCard+Transfer-DPReturn-Retur)";
            // 
            // groupHeaderSection2
            // 
            this.groupHeaderSection2.Height = Telerik.Reporting.Drawing.Unit.Cm(1.1792335510253906);
            this.groupHeaderSection2.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox22,
            this.textBox23,
            this.textBox39,
            this.textBox7,
            this.textBox6,
            this.textBox53,
            this.textBox54,
            this.textBox55,
            this.textBox10,
            this.textBox20,
            this.textBox65,
            this.textBox36,
            this.textBox14,
            this.textBox35,
            this.textBox64});
            this.groupHeaderSection2.Name = "groupHeaderSection2";
            this.groupHeaderSection2.PrintOnEveryPage = true;
            // 
            // textBox22
            // 
            this.textBox22.Format = "{0: dd-MMMM-yyyy}";
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.84768390655517578), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.1336147785186768), Telerik.Reporting.Drawing.Unit.Inch(0.15748023986816406));
            this.textBox22.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox22.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox22.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox22.Style.Color = System.Drawing.Color.DarkSlateBlue;
            this.textBox22.Style.Font.Bold = true;
            this.textBox22.Style.Font.Name = "Tahoma";
            this.textBox22.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox22.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox22.Value = "=PaymentDate";
            // 
            // textBox23
            // 
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.65887469053268433), Telerik.Reporting.Drawing.Unit.Inch(0.15748023986816406));
            this.textBox23.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox23.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox23.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox23.Style.Color = System.Drawing.Color.DarkSlateBlue;
            this.textBox23.Style.Font.Bold = true;
            this.textBox23.Style.Font.Name = "Tahoma";
            this.textBox23.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox23.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox23.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox23.Value = "Tanggal     ";
            // 
            // textBox39
            // 
            this.textBox39.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.69020342826843262), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox39.Name = "textBox39";
            this.textBox39.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.13295619189739227), Telerik.Reporting.Drawing.Unit.Inch(0.15748023986816406));
            this.textBox39.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox39.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox39.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox39.Style.Color = System.Drawing.Color.DarkBlue;
            this.textBox39.Style.Font.Bold = true;
            this.textBox39.Style.Font.Name = "Tahoma";
            this.textBox39.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox39.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox39.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox39.Value = ":";
            // 
            // textBox53
            // 
            this.textBox53.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.8450922966003418), Telerik.Reporting.Drawing.Unit.Inch(0.195901557803154));
            this.textBox53.Name = "textBox53";
            this.textBox53.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8308594822883606), Telerik.Reporting.Drawing.Unit.Inch(0.26836362481117249));
            this.textBox53.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox53.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox53.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox53.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox53.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox53.Style.Font.Bold = true;
            this.textBox53.Style.Font.Name = "Tahoma";
            this.textBox53.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox53.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox53.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox53.Style.Visible = true;
            this.textBox53.Value = "K.Debit";
            // 
            // textBox54
            // 
            this.textBox54.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9813776016235352), Telerik.Reporting.Drawing.Unit.Inch(0.195901557803154));
            this.textBox54.Name = "textBox54";
            this.textBox54.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8636353611946106), Telerik.Reporting.Drawing.Unit.Inch(0.26836362481117249));
            this.textBox54.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox54.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox54.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox54.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox54.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox54.Style.Font.Bold = true;
            this.textBox54.Style.Font.Name = "Tahoma";
            this.textBox54.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox54.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox54.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox54.Style.Visible = true;
            this.textBox54.Value = "K.Kredit";
            // 
            // textBox55
            // 
            this.textBox55.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.1563656330108643), Telerik.Reporting.Drawing.Unit.Inch(0.195901557803154));
            this.textBox55.Name = "textBox55";
            this.textBox55.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82493305206298828), Telerik.Reporting.Drawing.Unit.Inch(0.26836362481117249));
            this.textBox55.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox55.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox55.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox55.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox55.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox55.Style.Font.Bold = true;
            this.textBox55.Style.Font.Name = "Tahoma";
            this.textBox55.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox55.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox55.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox55.Style.Visible = true;
            this.textBox55.Value = "Tunai";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.67603063583374), Telerik.Reporting.Drawing.Unit.Inch(0.195901557803154));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.81711578369140625), Telerik.Reporting.Drawing.Unit.Inch(0.26836362481117249));
            this.textBox10.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox10.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox10.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox10.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox10.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox10.Style.Font.Bold = true;
            this.textBox10.Style.Font.Name = "Tahoma";
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox10.Style.Visible = true;
            this.textBox10.Value = "Transfer";
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.195901557803154));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.34621071815490723), Telerik.Reporting.Drawing.Unit.Inch(0.26836362481117249));
            this.textBox20.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox20.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox20.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox20.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox20.Style.Font.Bold = true;
            this.textBox20.Style.Font.Name = "Tahoma";
            this.textBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox20.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox20.Value = "No.";
            // 
            // textBox65
            // 
            this.textBox65.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.49322509765625), Telerik.Reporting.Drawing.Unit.Inch(0.195901557803154));
            this.textBox65.Name = "textBox56";
            this.textBox65.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82768672704696655), Telerik.Reporting.Drawing.Unit.Inch(0.26836356520652771));
            this.textBox65.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox65.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox65.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox65.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox65.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox65.Style.Font.Bold = true;
            this.textBox65.Style.Font.Name = "Tahoma";
            this.textBox65.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox65.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox65.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox65.Style.Visible = true;
            this.textBox65.Value = "DP Offset";
            // 
            // textBox36
            // 
            this.textBox36.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.3209896087646484), Telerik.Reporting.Drawing.Unit.Inch(0.195901557803154));
            this.textBox36.Name = "textBox56";
            this.textBox36.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.83463209867477417), Telerik.Reporting.Drawing.Unit.Inch(0.26836356520652771));
            this.textBox36.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox36.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox36.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox36.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox36.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox36.Style.Font.Bold = true;
            this.textBox36.Style.Font.Name = "Tahoma";
            this.textBox36.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox36.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox36.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox36.Style.Visible = true;
            this.textBox36.Value = "DP Kembali";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.1556997299194336), Telerik.Reporting.Drawing.Unit.Inch(0.19586212933063507));
            this.textBox14.Name = "textBox56";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.825694739818573), Telerik.Reporting.Drawing.Unit.Inch(0.26836356520652771));
            this.textBox14.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox14.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox14.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox14.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox14.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox14.Style.Font.Bold = true;
            this.textBox14.Style.Font.Name = "Tahoma";
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox14.Style.Visible = true;
            this.textBox14.Value = "Retur";
            // 
            // textBox35
            // 
            this.textBox35.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.98147201538086), Telerik.Reporting.Drawing.Unit.Inch(0.195901557803154));
            this.textBox35.Name = "textBox56";
            this.textBox35.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82755541801452637), Telerik.Reporting.Drawing.Unit.Inch(0.26836356520652771));
            this.textBox35.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox35.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox35.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox35.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox35.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox35.Style.Font.Bold = true;
            this.textBox35.Style.Font.Name = "Tahoma";
            this.textBox35.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox35.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox35.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox35.Style.Visible = true;
            this.textBox35.Value = "Total";
            // 
            // textBox64
            // 
            this.textBox64.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(9.80910587310791), Telerik.Reporting.Drawing.Unit.Inch(0.19590187072753906));
            this.textBox64.Name = "textBox64";
            this.textBox64.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.01617431640625), Telerik.Reporting.Drawing.Unit.Inch(0.26836356520652771));
            this.textBox64.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox64.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox64.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox64.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox64.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox64.Style.Font.Bold = true;
            this.textBox64.Style.Font.Name = "Tahoma";
            this.textBox64.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox64.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox64.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox64.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox64.Style.Visible = true;
            this.textBox64.Value = "Ket";
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
            this.groupFooterSection4.Height = Telerik.Reporting.Drawing.Unit.Cm(1.5900918245315552);
            this.groupFooterSection4.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox50,
            this.textBox51,
            this.textBox59,
            this.textBox61,
            this.textBox62,
            this.textBox67,
            this.textBox70,
            this.textBox26,
            this.textBox57});
            this.groupFooterSection4.Name = "groupFooterSection4";
            this.groupFooterSection4.PageBreak = Telerik.Reporting.PageBreak.After;
            // 
            // textBox50
            // 
            this.textBox50.Format = "{0:N0}";
            this.textBox50.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.3209896087646484), Telerik.Reporting.Drawing.Unit.Inch(7.8837074397597462E-05));
            this.textBox50.Name = "textBox69";
            this.textBox50.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.83463209867477417), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox50.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox50.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox50.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox50.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox50.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox50.Style.Color = System.Drawing.Color.Black;
            this.textBox50.Style.Font.Bold = true;
            this.textBox50.Style.Font.Name = "Tahoma";
            this.textBox50.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox50.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox50.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox50.Value = "=sum(DPReturn)";
            // 
            // textBox51
            // 
            this.textBox51.Format = "{0:N0}";
            this.textBox51.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.49322509765625), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox51.Name = "textBox69";
            this.textBox51.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82768672704696655), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox51.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox51.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox51.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox51.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox51.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox51.Style.Color = System.Drawing.Color.Black;
            this.textBox51.Style.Font.Bold = true;
            this.textBox51.Style.Font.Name = "Tahoma";
            this.textBox51.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox51.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox51.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox51.Value = "=sum(DPOffset)";
            // 
            // textBox59
            // 
            this.textBox59.Format = "{0:N0}";
            this.textBox59.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.67603063583374), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox59.Name = "textBox25";
            this.textBox59.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.81711578369140625), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox59.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox59.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox59.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox59.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox59.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox59.Style.Color = System.Drawing.Color.Black;
            this.textBox59.Style.Font.Bold = true;
            this.textBox59.Style.Font.Name = "Tahoma";
            this.textBox59.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox59.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox59.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox59.Value = "=sum(Transfer)";
            // 
            // textBox61
            // 
            this.textBox61.Format = "{0:N0}";
            this.textBox61.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.8450922966003418), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox61.Name = "textBox42";
            this.textBox61.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8308594822883606), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox61.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox61.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox61.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox61.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox61.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox61.Style.Color = System.Drawing.Color.Black;
            this.textBox61.Style.Font.Bold = true;
            this.textBox61.Style.Font.Name = "Tahoma";
            this.textBox61.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox61.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox61.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox61.Value = "=sum(DebitCard)";
            // 
            // textBox62
            // 
            this.textBox62.Format = "{0:N0}";
            this.textBox62.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9813783168792725), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox62.Name = "textBox41";
            this.textBox62.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.86363506317138672), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox62.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox62.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox62.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox62.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox62.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox62.Style.Color = System.Drawing.Color.Black;
            this.textBox62.Style.Font.Bold = true;
            this.textBox62.Style.Font.Name = "Tahoma";
            this.textBox62.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox62.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox62.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox62.Value = "=sum(CreditCard)";
            // 
            // textBox67
            // 
            this.textBox67.Format = "{0:N0}";
            this.textBox67.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.1563656330108643), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox67.Name = "textBox38";
            this.textBox67.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82493335008621216), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox67.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox67.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox67.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox67.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox67.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox67.Style.Color = System.Drawing.Color.Black;
            this.textBox67.Style.Font.Bold = true;
            this.textBox67.Style.Font.Name = "Tahoma";
            this.textBox67.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox67.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox67.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox67.Value = "=sum(Cash)";
            // 
            // textBox70
            // 
            this.textBox70.Format = "Total {0} :";
            this.textBox70.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9378803194267675E-05), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox70.Name = "textBox21";
            this.textBox70.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.15624737739563), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox70.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox70.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox70.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox70.Style.Color = System.Drawing.Color.Black;
            this.textBox70.Style.Font.Bold = true;
            this.textBox70.Style.Font.Name = "Tahoma";
            this.textBox70.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox70.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox70.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox70.Value = "=PaymentTypeName";
            // 
            // textBox26
            // 
            this.textBox26.Format = "{0:N0}";
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.1556987762451172), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox26.Name = "textBox69";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82569551467895508), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox26.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox26.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox26.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox26.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox26.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox26.Style.Color = System.Drawing.Color.Black;
            this.textBox26.Style.Font.Bold = true;
            this.textBox26.Style.Font.Name = "Tahoma";
            this.textBox26.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox26.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox26.Value = "=sum(Retur)";
            // 
            // textBox57
            // 
            this.textBox57.Format = "{0:N0}";
            this.textBox57.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.98147201538086), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox57.Name = "textBox57";
            this.textBox57.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82712942361831665), Telerik.Reporting.Drawing.Unit.Inch(0.19681422412395477));
            this.textBox57.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox57.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox57.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox57.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox57.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox57.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox57.Style.Font.Bold = true;
            this.textBox57.Style.Font.Name = "Tahoma";
            this.textBox57.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox57.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox57.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox57.Value = "=Sum(Cash+CreditCard+DebitCard+Transfer-DPReturn-Retur)";
            // 
            // groupHeaderSection4
            // 
            this.groupHeaderSection4.Height = Telerik.Reporting.Drawing.Unit.Cm(0.40009993314743042);
            this.groupHeaderSection4.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox48,
            this.textBox3,
            this.textBox49});
            this.groupHeaderSection4.Name = "groupHeaderSection4";
            this.groupHeaderSection4.PrintOnEveryPage = true;
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.69020348787307739), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox3.Name = "textBox40";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.13295619189739227), Telerik.Reporting.Drawing.Unit.Inch(0.15748023986816406));
            this.textBox3.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox3.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox3.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox3.Style.Color = System.Drawing.Color.Black;
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Name = "Tahoma";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = ":";
            // 
            // textBox49
            // 
            this.textBox49.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox49.Name = "textBox49";
            this.textBox49.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.65887469053268433), Telerik.Reporting.Drawing.Unit.Inch(0.15748023986816406));
            this.textBox49.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox49.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox49.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox49.Style.Color = System.Drawing.Color.Black;
            this.textBox49.Style.Font.Bold = true;
            this.textBox49.Style.Font.Name = "Tahoma";
            this.textBox49.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox49.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox49.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox49.Value = "Tipe";
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
            this.groupFooterSection3.Height = Telerik.Reporting.Drawing.Unit.Inch(0.052083492279052734);
            this.groupFooterSection3.Name = "groupFooterSection3";
            // 
            // groupHeaderSection3
            // 
            this.groupHeaderSection3.Height = Telerik.Reporting.Drawing.Unit.Inch(0.17566442489624023);
            this.groupHeaderSection3.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox4,
            this.textBox1,
            this.textBox8,
            this.textBox11,
            this.textBox86});
            this.groupHeaderSection3.Name = "groupHeaderSection3";
            // 
            // textBox86
            // 
            this.textBox86.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.76475191116333), Telerik.Reporting.Drawing.Unit.Inch(0.015664419159293175));
            this.textBox86.Name = "textBox86";
            this.textBox86.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.06052827835083), Telerik.Reporting.Drawing.Unit.Inch(0.14433558285236359));
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
            // PaymentReceiveByCashierRpt
            // 
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            this.group1,
            this.group4,
            this.group2,
            this.group3});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection1,
            this.groupFooterSection1,
            this.groupHeaderSection4,
            this.groupFooterSection4,
            this.groupHeaderSection2,
            this.groupFooterSection2,
            this.groupHeaderSection3,
            this.groupFooterSection3,
            this.pageHeader,
            this.detail,
            this.pageFooter,
            this.reportFooterSection1});
            this.Name = "PaymentReceiveByCashierRpt";
            this.PageSettings.Landscape = true;
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
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(27.4962158203125);
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
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox58;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox12;
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
        private Telerik.Reporting.TextBox textBox59;
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
        private Telerik.Reporting.TextBox textBox80;
        private Telerik.Reporting.TextBox textBox83;
        private Telerik.Reporting.TextBox textBox84;
        private Telerik.Reporting.TextBox textBox85;
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
        private Telerik.Reporting.TextBox textBox64;
        private Group group3;
        private GroupFooterSection groupFooterSection3;
        private GroupHeaderSection groupHeaderSection3;
        private Telerik.Reporting.TextBox textBox28;
        private Telerik.Reporting.TextBox textBox82;
        private Telerik.Reporting.TextBox textBox86;
        private Telerik.Reporting.TextBox textBox87;
        private Telerik.Reporting.TextBox textBox90;
        private Telerik.Reporting.TextBox textBox91;
        private Telerik.Reporting.TextBox textBox92;
        private Telerik.Reporting.TextBox textBox93;
        private Telerik.Reporting.TextBox textBox94;
        private Telerik.Reporting.TextBox textBox95;
    }
}