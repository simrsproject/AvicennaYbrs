namespace Temiang.Avicenna.ReportLibrary.Charges
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class DailyReceiptPerCashierRpt
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
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.txtPeriod = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox57 = new Telerik.Reporting.TextBox();
            this.textBox58 = new Telerik.Reporting.TextBox();
            this.textBox64 = new Telerik.Reporting.TextBox();
            this.textBox66 = new Telerik.Reporting.TextBox();
            this.textBox37 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox35 = new Telerik.Reporting.TextBox();
            this.txtCard = new Telerik.Reporting.TextBox();
            this.textBox48 = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox38 = new Telerik.Reporting.TextBox();
            this.textBox42 = new Telerik.Reporting.TextBox();
            this.textBox43 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox69 = new Telerik.Reporting.TextBox();
            this.textBox72 = new Telerik.Reporting.TextBox();
            this.textBox44 = new Telerik.Reporting.TextBox();
            this.textBox45 = new Telerik.Reporting.TextBox();
            this.textBox46 = new Telerik.Reporting.TextBox();
            this.textBox47 = new Telerik.Reporting.TextBox();
            this.textBox41 = new Telerik.Reporting.TextBox();
            this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.txtUserID = new Telerik.Reporting.TextBox();
            this.textBox40 = new Telerik.Reporting.TextBox();
            this.groupFooterSection2 = new Telerik.Reporting.GroupFooterSection();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox68 = new Telerik.Reporting.TextBox();
            this.textBox71 = new Telerik.Reporting.TextBox();
            this.groupHeaderSection2 = new Telerik.Reporting.GroupHeaderSection();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox39 = new Telerik.Reporting.TextBox();
            this.textBox53 = new Telerik.Reporting.TextBox();
            this.textBox54 = new Telerik.Reporting.TextBox();
            this.textBox55 = new Telerik.Reporting.TextBox();
            this.textBox56 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox63 = new Telerik.Reporting.TextBox();
            this.textBox65 = new Telerik.Reporting.TextBox();
            this.textBox36 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.textBox33 = new Telerik.Reporting.TextBox();
            this.textBox34 = new Telerik.Reporting.TextBox();
            this.groupFooterSection4 = new Telerik.Reporting.GroupFooterSection();
            this.textBox50 = new Telerik.Reporting.TextBox();
            this.textBox51 = new Telerik.Reporting.TextBox();
            this.textBox52 = new Telerik.Reporting.TextBox();
            this.textBox59 = new Telerik.Reporting.TextBox();
            this.textBox60 = new Telerik.Reporting.TextBox();
            this.textBox61 = new Telerik.Reporting.TextBox();
            this.textBox62 = new Telerik.Reporting.TextBox();
            this.textBox67 = new Telerik.Reporting.TextBox();
            this.textBox70 = new Telerik.Reporting.TextBox();
            this.groupHeaderSection4 = new Telerik.Reporting.GroupHeaderSection();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox49 = new Telerik.Reporting.TextBox();
            this.textBox73 = new Telerik.Reporting.TextBox();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.textBox74 = new Telerik.Reporting.TextBox();
            this.textBox75 = new Telerik.Reporting.TextBox();
            this.textBox76 = new Telerik.Reporting.TextBox();
            this.textBox77 = new Telerik.Reporting.TextBox();
            this.textBox78 = new Telerik.Reporting.TextBox();
            this.textBox79 = new Telerik.Reporting.TextBox();
            this.textBox80 = new Telerik.Reporting.TextBox();
            this.textBox81 = new Telerik.Reporting.TextBox();
            this.textBox82 = new Telerik.Reporting.TextBox();
            this.textBox83 = new Telerik.Reporting.TextBox();
            this.textBox84 = new Telerik.Reporting.TextBox();
            this.textBox85 = new Telerik.Reporting.TextBox();
            this.textBox86 = new Telerik.Reporting.TextBox();
            this.textBox87 = new Telerik.Reporting.TextBox();
            this.textBox88 = new Telerik.Reporting.TextBox();
            this.textBox89 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(2.9D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtPeriod,
            this.textBox5});
            this.pageHeader.Name = "pageHeader";
            // 
            // txtPeriod
            // 
            this.txtPeriod.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.532D), Telerik.Reporting.Drawing.Unit.Inch(0.952D));
            this.txtPeriod.Name = "txtPeriod";
            this.txtPeriod.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.862D), Telerik.Reporting.Drawing.Unit.Inch(0.139D));
            this.txtPeriod.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.txtPeriod.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.txtPeriod.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.txtPeriod.Style.Font.Bold = true;
            this.txtPeriod.Style.Font.Name = "Courier New";
            this.txtPeriod.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(12D);
            this.txtPeriod.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtPeriod.Value = "Periode:";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.532D), Telerik.Reporting.Drawing.Unit.Inch(0.71D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.862D), Telerik.Reporting.Drawing.Unit.Inch(0.238D));
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Font.Name = "Courier New";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(18D);
            this.textBox5.Style.Font.Strikeout = false;
            this.textBox5.Style.Font.Underline = false;
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.Value = "Laporan Penerimaan Harian Per Kasir";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.719D), Telerik.Reporting.Drawing.Unit.Inch(0.196D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.828D), Telerik.Reporting.Drawing.Unit.Inch(0.268D));
            this.textBox7.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox7.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox7.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.Style.Font.Name = "Courier New";
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.Value = "Nama";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.346D), Telerik.Reporting.Drawing.Unit.Inch(0.196D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.373D), Telerik.Reporting.Drawing.Unit.Inch(0.268D));
            this.textBox6.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox6.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox6.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.Font.Bold = true;
            this.textBox6.Style.Font.Name = "Courier New";
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "No. Kwitansi";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.984D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox15,
            this.textBox17,
            this.textBox19,
            this.textBox29,
            this.textBox57,
            this.textBox58,
            this.textBox64,
            this.textBox66,
            this.textBox37,
            this.textBox1,
            this.textBox4,
            this.textBox8,
            this.textBox11,
            this.textBox35,
            this.txtCard});
            this.detail.Name = "detail";
            this.detail.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Dashed;
            // 
            // textBox15
            // 
            this.textBox15.Format = "{0:N0}";
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.568D), Telerik.Reporting.Drawing.Unit.Inch(0.198D));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.758D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox15.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox15.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox15.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox15.Style.Font.Bold = false;
            this.textBox15.Style.Font.Name = "Courier New";
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox15.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox15.Value = "=AR";
            // 
            // textBox17
            // 
            this.textBox17.Format = "{0:N0}";
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.558D), Telerik.Reporting.Drawing.Unit.Inch(0.198D));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.831D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox17.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox17.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox17.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox17.Style.Font.Bold = false;
            this.textBox17.Style.Font.Name = "Courier New";
            this.textBox17.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox17.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox17.Value = "=Cash";
            // 
            // textBox19
            // 
            this.textBox19.Format = "{0:N0}";
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.41D), Telerik.Reporting.Drawing.Unit.Inch(0.198D));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.821D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox19.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox19.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox19.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox19.Style.Font.Bold = false;
            this.textBox19.Style.Font.Name = "Courier New";
            this.textBox19.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox19.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox19.Value = "=CreditCard";
            // 
            // textBox29
            // 
            this.textBox29.Format = "{0:N0}";
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.231D), Telerik.Reporting.Drawing.Unit.Inch(0.198D));
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox29.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox29.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox29.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox29.Style.Font.Bold = false;
            this.textBox29.Style.Font.Name = "Courier New";
            this.textBox29.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox29.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox29.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox29.Value = "=DebitCard";
            // 
            // textBox57
            // 
            this.textBox57.Format = "{0:N0}";
            this.textBox57.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.81D), Telerik.Reporting.Drawing.Unit.Inch(0.198D));
            this.textBox57.Name = "textBox57";
            this.textBox57.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.758D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox57.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox57.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox57.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox57.Style.Font.Bold = false;
            this.textBox57.Style.Font.Name = "Courier New";
            this.textBox57.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox57.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox57.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox57.Value = "=Discount";
            // 
            // textBox58
            // 
            this.textBox58.Format = "{0:N0}";
            this.textBox58.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.041D), Telerik.Reporting.Drawing.Unit.Inch(0.198D));
            this.textBox58.Name = "textBox58";
            this.textBox58.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.758D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox58.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox58.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox58.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox58.Style.Font.Bold = false;
            this.textBox58.Style.Font.Name = "Courier New";
            this.textBox58.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox58.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox58.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox58.Value = "=Transfer";
            // 
            // textBox64
            // 
            this.textBox64.Format = "{0:N0}";
            this.textBox64.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(10.052D), Telerik.Reporting.Drawing.Unit.Inch(0.198D));
            this.textBox64.Name = "textBox15";
            this.textBox64.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.87D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox64.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox64.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox64.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox64.Style.Font.Bold = false;
            this.textBox64.Style.Font.Name = "Courier New";
            this.textBox64.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox64.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox64.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox64.Value = "=TDP";
            // 
            // textBox66
            // 
            this.textBox66.Format = "{0:N0}";
            this.textBox66.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.327D), Telerik.Reporting.Drawing.Unit.Inch(0.198D));
            this.textBox66.Name = "textBox15";
            this.textBox66.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.81D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox66.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox66.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox66.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox66.Style.Font.Bold = false;
            this.textBox66.Style.Font.Name = "Courier New";
            this.textBox66.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox66.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox66.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox66.Value = "=DPOffset";
            // 
            // textBox37
            // 
            this.textBox37.Format = "{0:N0}";
            this.textBox37.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(9.147D), Telerik.Reporting.Drawing.Unit.Inch(0.198D));
            this.textBox37.Name = "textBox15";
            this.textBox37.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.894D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox37.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox37.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox37.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox37.Style.Font.Bold = false;
            this.textBox37.Style.Font.Name = "Courier New";
            this.textBox37.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox37.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox37.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox37.Value = "=DPReturn";
            // 
            // textBox1
            // 
            this.textBox1.Format = "{0}.";
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.346D), Telerik.Reporting.Drawing.Unit.Inch(0.198D));
            this.textBox1.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox1.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox1.Style.Font.Bold = false;
            this.textBox1.Style.Font.Name = "Courier New";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "= RowNumber()";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.346D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.373D), Telerik.Reporting.Drawing.Unit.Inch(0.198D));
            this.textBox4.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox4.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox4.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox4.Style.Font.Bold = false;
            this.textBox4.Style.Font.Name = "Courier New";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox4.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.1D);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.Value = "=PaymentNo";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.719D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.618D), Telerik.Reporting.Drawing.Unit.Inch(0.198D));
            this.textBox8.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox8.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox8.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox8.Style.Font.Bold = false;
            this.textBox8.Style.Font.Name = "Courier New";
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Value = "=RegistrationNo";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.344D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.199D), Telerik.Reporting.Drawing.Unit.Inch(0.198D));
            this.textBox11.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox11.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox11.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox11.Style.Font.Bold = false;
            this.textBox11.Style.Font.Name = "Courier New";
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox11.Value = "=PatientName";
            // 
            // textBox35
            // 
            this.textBox35.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.344D), Telerik.Reporting.Drawing.Unit.Inch(0.198D));
            this.textBox35.Name = "textBox35";
            this.textBox35.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.795D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox35.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox35.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox35.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox35.Style.Color = System.Drawing.Color.DarkGreen;
            this.textBox35.Style.Font.Bold = true;
            this.textBox35.Style.Font.Name = "Courier New";
            this.textBox35.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox35.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.1D);
            this.textBox35.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox35.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox35.Value = "=GuarantorName";
            // 
            // txtCard
            // 
            this.txtCard.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.543D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.txtCard.Name = "txtCard";
            this.txtCard.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.199D), Telerik.Reporting.Drawing.Unit.Inch(0.198D));
            this.txtCard.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.txtCard.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.txtCard.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.txtCard.Style.Font.Bold = false;
            this.txtCard.Style.Font.Name = "Courier New";
            this.txtCard.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.txtCard.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtCard.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.txtCard.Value = "=card";
            // 
            // textBox48
            // 
            this.textBox48.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.848D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox48.Name = "textBox48";
            this.textBox48.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.682D), Telerik.Reporting.Drawing.Unit.Inch(0.157D));
            this.textBox48.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox48.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox48.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox48.Style.Color = System.Drawing.Color.Black;
            this.textBox48.Style.Font.Bold = true;
            this.textBox48.Style.Font.Name = "Courier New";
            this.textBox48.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox48.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox48.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox48.Value = "=PaymentTypeName";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.482D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox28});
            this.pageFooter.Name = "pageFooter";
            // 
            // textBox28
            // 
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.897D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.973D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox28.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox28.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox28.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox28.Style.Font.Bold = false;
            this.textBox28.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox28.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox28.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox28.Value = "= PageNumber";
            // 
            // groupFooterSection1
            // 
            this.groupFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(4.115D);
            this.groupFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox21,
            this.textBox38,
            this.textBox42,
            this.textBox43,
            this.textBox25,
            this.textBox26,
            this.textBox69,
            this.textBox72,
            this.textBox44,
            this.textBox45,
            this.textBox46,
            this.textBox47,
            this.textBox41});
            this.groupFooterSection1.Name = "groupFooterSection1";
            this.groupFooterSection1.PageBreak = Telerik.Reporting.PageBreak.After;
            // 
            // textBox21
            // 
            this.textBox21.Format = "Total Kasir {0} :";
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.063D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.099D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox21.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox21.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox21.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox21.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox21.Style.Font.Bold = true;
            this.textBox21.Style.Font.Name = "Courier New";
            this.textBox21.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox21.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox21.Value = "=CashierName";
            // 
            // textBox38
            // 
            this.textBox38.Format = "{0:N0}";
            this.textBox38.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.198D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox38.Name = "textBox38";
            this.textBox38.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.18D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox38.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox38.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox38.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox38.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox38.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox38.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox38.Style.Font.Bold = true;
            this.textBox38.Style.Font.Name = "Courier New";
            this.textBox38.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox38.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox38.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox38.Value = "=sum(Cash)";
            // 
            // textBox42
            // 
            this.textBox42.Format = "{0:N0}";
            this.textBox42.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.231D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox42.Name = "textBox42";
            this.textBox42.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox42.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox42.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox42.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox42.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox42.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox42.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox42.Style.Font.Bold = true;
            this.textBox42.Style.Font.Name = "Courier New";
            this.textBox42.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox42.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox42.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox42.Value = "=sum(DebitCard)";
            // 
            // textBox43
            // 
            this.textBox43.Format = "{0:N0}";
            this.textBox43.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.568D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox43.Name = "textBox43";
            this.textBox43.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.758D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox43.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox43.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox43.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox43.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox43.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox43.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox43.Style.Font.Bold = true;
            this.textBox43.Style.Font.Name = "Courier New";
            this.textBox43.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox43.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox43.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox43.Value = "=sum(AR)";
            // 
            // textBox25
            // 
            this.textBox25.Format = "{0:N0}";
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.041D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.758D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox25.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox25.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox25.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox25.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox25.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox25.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox25.Style.Font.Bold = true;
            this.textBox25.Style.Font.Name = "Courier New";
            this.textBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox25.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox25.Value = "=sum(Transfer)";
            // 
            // textBox26
            // 
            this.textBox26.Format = "{0:N0}";
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.81D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.758D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox26.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox26.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox26.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox26.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox26.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox26.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox26.Style.Font.Bold = true;
            this.textBox26.Style.Font.Name = "Courier New";
            this.textBox26.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox26.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox26.Value = "=sum(Discount)";
            // 
            // textBox69
            // 
            this.textBox69.Format = "{0:N0}";
            this.textBox69.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.327D), Telerik.Reporting.Drawing.Unit.Inch(0.004D));
            this.textBox69.Name = "textBox69";
            this.textBox69.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.81D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox69.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox69.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox69.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox69.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox69.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox69.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox69.Style.Font.Bold = true;
            this.textBox69.Style.Font.Name = "Courier New";
            this.textBox69.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox69.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox69.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox69.Value = "=sum(DPOffset)";
            // 
            // textBox72
            // 
            this.textBox72.Format = "{0:N0}";
            this.textBox72.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(9.147D), Telerik.Reporting.Drawing.Unit.Inch(0.004D));
            this.textBox72.Name = "textBox69";
            this.textBox72.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.894D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox72.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox72.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox72.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox72.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox72.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox72.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox72.Style.Font.Bold = true;
            this.textBox72.Style.Font.Name = "Courier New";
            this.textBox72.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox72.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox72.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox72.Value = "=sum(DPReturn)";
            // 
            // textBox44
            // 
            this.textBox44.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.411D), Telerik.Reporting.Drawing.Unit.Inch(1.431D));
            this.textBox44.Name = "textBox44";
            this.textBox44.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.778D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox44.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox44.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox44.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox44.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox44.Style.Font.Bold = true;
            this.textBox44.Style.Font.Name = "Courier New";
            this.textBox44.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox44.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox44.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox44.Value = "=CashierName";
            // 
            // textBox45
            // 
            this.textBox45.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.65D), Telerik.Reporting.Drawing.Unit.Inch(0.643D));
            this.textBox45.Name = "textBox45";
            this.textBox45.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.331D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox45.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox45.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox45.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox45.Style.Font.Bold = true;
            this.textBox45.Style.Font.Name = "Courier New";
            this.textBox45.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox45.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox45.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox45.Value = "Kasir";
            // 
            // textBox46
            // 
            this.textBox46.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.103D), Telerik.Reporting.Drawing.Unit.Inch(1.431D));
            this.textBox46.Name = "textBox46";
            this.textBox46.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.308D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox46.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox46.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox46.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox46.Style.Font.Bold = true;
            this.textBox46.Style.Font.Name = "Times New Roman";
            this.textBox46.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox46.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox46.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox46.Value = "(";
            // 
            // textBox47
            // 
            this.textBox47.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(9.189D), Telerik.Reporting.Drawing.Unit.Inch(1.431D));
            this.textBox47.Name = "textBox47";
            this.textBox47.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.308D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox47.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox47.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox47.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox47.Style.Font.Bold = true;
            this.textBox47.Style.Font.Name = "Times New Roman";
            this.textBox47.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox47.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox47.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox47.Value = ")";
            // 
            // textBox41
            // 
            this.textBox41.Format = "{0:N0}";
            this.textBox41.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.41D), Telerik.Reporting.Drawing.Unit.Inch(0.004D));
            this.textBox41.Name = "textBox41";
            this.textBox41.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.821D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox41.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox41.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox41.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox41.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox41.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox41.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox41.Style.Font.Bold = true;
            this.textBox41.Style.Font.Name = "Courier New";
            this.textBox41.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox41.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox41.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox41.Value = "=sum(CreditCard)";
            // 
            // groupHeaderSection1
            // 
            this.groupHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.547D);
            this.groupHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox16,
            this.txtUserID,
            this.textBox40});
            this.groupHeaderSection1.Name = "groupHeaderSection1";
            this.groupHeaderSection1.PrintOnEveryPage = true;
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.058D));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.659D), Telerik.Reporting.Drawing.Unit.Inch(0.157D));
            this.textBox16.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox16.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox16.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox16.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox16.Style.Font.Bold = true;
            this.textBox16.Style.Font.Name = "Courier New";
            this.textBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox16.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox16.Value = "Kasir          ";
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.848D), Telerik.Reporting.Drawing.Unit.Inch(0.058D));
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.134D), Telerik.Reporting.Drawing.Unit.Inch(0.157D));
            this.txtUserID.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.txtUserID.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.txtUserID.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.txtUserID.Style.Color = System.Drawing.Color.DarkRed;
            this.txtUserID.Style.Font.Bold = true;
            this.txtUserID.Style.Font.Name = "Courier New";
            this.txtUserID.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.txtUserID.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtUserID.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.txtUserID.Value = "=CashierName";
            // 
            // textBox40
            // 
            this.textBox40.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.69D), Telerik.Reporting.Drawing.Unit.Inch(0.058D));
            this.textBox40.Name = "textBox40";
            this.textBox40.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.133D), Telerik.Reporting.Drawing.Unit.Inch(0.157D));
            this.textBox40.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox40.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox40.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox40.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox40.Style.Font.Bold = true;
            this.textBox40.Style.Font.Name = "Times New Roman";
            this.textBox40.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox40.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox40.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox40.Value = ":";
            // 
            // groupFooterSection2
            // 
            this.groupFooterSection2.Height = Telerik.Reporting.Drawing.Unit.Cm(0.49D);
            this.groupFooterSection2.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox24,
            this.textBox18,
            this.textBox27,
            this.textBox30,
            this.textBox32,
            this.textBox12,
            this.textBox14,
            this.textBox68,
            this.textBox71});
            this.groupFooterSection2.Name = "groupFooterSection2";
            // 
            // textBox24
            // 
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.063D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.099D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox24.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox24.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox24.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox24.Style.Color = System.Drawing.Color.DarkSlateBlue;
            this.textBox24.Style.Font.Bold = true;
            this.textBox24.Style.Font.Name = "Courier New";
            this.textBox24.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox24.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox24.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox24.Value = "Total Per Tanggal : ";
            // 
            // textBox18
            // 
            this.textBox18.Format = "{0:N0}";
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.198D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.18D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox18.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox18.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox18.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox18.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox18.Style.Color = System.Drawing.Color.DarkSlateBlue;
            this.textBox18.Style.Font.Bold = true;
            this.textBox18.Style.Font.Name = "Courier New";
            this.textBox18.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox18.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox18.Value = "=sum(Cash)";
            // 
            // textBox27
            // 
            this.textBox27.Format = "{0:N0}";
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.41D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.821D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox27.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox27.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox27.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox27.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox27.Style.Color = System.Drawing.Color.DarkSlateBlue;
            this.textBox27.Style.Font.Bold = true;
            this.textBox27.Style.Font.Name = "Courier New";
            this.textBox27.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox27.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox27.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox27.Value = "=sum(CreditCard)";
            // 
            // textBox30
            // 
            this.textBox30.Format = "{0:N0}";
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.231D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox30.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox30.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox30.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox30.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox30.Style.Color = System.Drawing.Color.DarkSlateBlue;
            this.textBox30.Style.Font.Bold = true;
            this.textBox30.Style.Font.Name = "Courier New";
            this.textBox30.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox30.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox30.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox30.Value = "=sum(DebitCard)";
            // 
            // textBox32
            // 
            this.textBox32.Format = "{0:N0}";
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.568D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.758D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox32.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox32.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox32.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox32.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox32.Style.Color = System.Drawing.Color.DarkSlateBlue;
            this.textBox32.Style.Font.Bold = true;
            this.textBox32.Style.Font.Name = "Courier New";
            this.textBox32.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox32.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox32.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox32.Value = "=sum(AR)";
            // 
            // textBox12
            // 
            this.textBox12.Format = "{0:N0}";
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.041D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.758D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox12.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox12.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox12.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox12.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox12.Style.Color = System.Drawing.Color.DarkSlateBlue;
            this.textBox12.Style.Font.Bold = true;
            this.textBox12.Style.Font.Name = "Courier New";
            this.textBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.Value = "=sum(Transfer)";
            // 
            // textBox14
            // 
            this.textBox14.Format = "{0:N0}";
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.81D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.758D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox14.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox14.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox14.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox14.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox14.Style.Color = System.Drawing.Color.DarkSlateBlue;
            this.textBox14.Style.Font.Bold = true;
            this.textBox14.Style.Font.Name = "Courier New";
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox14.Value = "=sum(Discount)";
            // 
            // textBox68
            // 
            this.textBox68.Format = "{0:N0}";
            this.textBox68.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.327D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox68.Name = "textBox32";
            this.textBox68.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.81D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox68.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox68.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox68.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox68.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox68.Style.Color = System.Drawing.Color.DarkSlateBlue;
            this.textBox68.Style.Font.Bold = true;
            this.textBox68.Style.Font.Name = "Courier New";
            this.textBox68.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox68.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox68.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox68.Value = "=sum(DPOffset)";
            // 
            // textBox71
            // 
            this.textBox71.Format = "{0:N0}";
            this.textBox71.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(9.147D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox71.Name = "textBox71";
            this.textBox71.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.894D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox71.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox71.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox71.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox71.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox71.Style.Color = System.Drawing.Color.DarkSlateBlue;
            this.textBox71.Style.Font.Bold = true;
            this.textBox71.Style.Font.Name = "Courier New";
            this.textBox71.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox71.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox71.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox71.Value = "=sum(DPReturn)";
            // 
            // groupHeaderSection2
            // 
            this.groupHeaderSection2.Height = Telerik.Reporting.Drawing.Unit.Cm(1.179D);
            this.groupHeaderSection2.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox22,
            this.textBox23,
            this.textBox39,
            this.textBox7,
            this.textBox6,
            this.textBox53,
            this.textBox54,
            this.textBox55,
            this.textBox56,
            this.textBox2,
            this.textBox10,
            this.textBox20,
            this.textBox63,
            this.textBox65,
            this.textBox36});
            this.groupHeaderSection2.Name = "groupHeaderSection2";
            this.groupHeaderSection2.PrintOnEveryPage = true;
            // 
            // textBox22
            // 
            this.textBox22.Format = "{0: dd-MMMM-yyyy}";
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.848D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.134D), Telerik.Reporting.Drawing.Unit.Inch(0.157D));
            this.textBox22.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox22.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox22.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox22.Style.Color = System.Drawing.Color.DarkSlateBlue;
            this.textBox22.Style.Font.Bold = true;
            this.textBox22.Style.Font.Name = "Courier New";
            this.textBox22.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox22.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox22.Value = "=PaymentDate";
            // 
            // textBox23
            // 
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.659D), Telerik.Reporting.Drawing.Unit.Inch(0.157D));
            this.textBox23.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox23.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox23.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox23.Style.Color = System.Drawing.Color.DarkSlateBlue;
            this.textBox23.Style.Font.Bold = true;
            this.textBox23.Style.Font.Name = "Courier New";
            this.textBox23.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox23.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox23.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox23.Value = "Tanggal     ";
            // 
            // textBox39
            // 
            this.textBox39.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.69D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox39.Name = "textBox39";
            this.textBox39.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.133D), Telerik.Reporting.Drawing.Unit.Inch(0.157D));
            this.textBox39.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox39.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox39.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox39.Style.Color = System.Drawing.Color.DarkBlue;
            this.textBox39.Style.Font.Bold = true;
            this.textBox39.Style.Font.Name = "Times New Roman";
            this.textBox39.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox39.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox39.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox39.Value = ":";
            // 
            // textBox53
            // 
            this.textBox53.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.231D), Telerik.Reporting.Drawing.Unit.Inch(0.196D));
            this.textBox53.Name = "textBox53";
            this.textBox53.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8D), Telerik.Reporting.Drawing.Unit.Inch(0.268D));
            this.textBox53.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox53.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox53.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox53.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox53.Style.Font.Bold = true;
            this.textBox53.Style.Font.Name = "Courier New";
            this.textBox53.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox53.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox53.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox53.Style.Visible = true;
            this.textBox53.Value = "K.Debit";
            // 
            // textBox54
            // 
            this.textBox54.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.41D), Telerik.Reporting.Drawing.Unit.Inch(0.196D));
            this.textBox54.Name = "textBox54";
            this.textBox54.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.821D), Telerik.Reporting.Drawing.Unit.Inch(0.268D));
            this.textBox54.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox54.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox54.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox54.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox54.Style.Font.Bold = true;
            this.textBox54.Style.Font.Name = "Courier New";
            this.textBox54.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox54.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox54.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox54.Style.Visible = true;
            this.textBox54.Value = "K.Kredit";
            // 
            // textBox55
            // 
            this.textBox55.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.558D), Telerik.Reporting.Drawing.Unit.Inch(0.196D));
            this.textBox55.Name = "textBox55";
            this.textBox55.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.842D), Telerik.Reporting.Drawing.Unit.Inch(0.268D));
            this.textBox55.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox55.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox55.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox55.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox55.Style.Font.Bold = true;
            this.textBox55.Style.Font.Name = "Courier New";
            this.textBox55.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox55.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox55.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox55.Style.Visible = true;
            this.textBox55.Value = "Tunai";
            // 
            // textBox56
            // 
            this.textBox56.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.568D), Telerik.Reporting.Drawing.Unit.Inch(0.196D));
            this.textBox56.Name = "textBox56";
            this.textBox56.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.758D), Telerik.Reporting.Drawing.Unit.Inch(0.268D));
            this.textBox56.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox56.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox56.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox56.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox56.Style.Font.Bold = true;
            this.textBox56.Style.Font.Name = "Courier New";
            this.textBox56.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox56.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox56.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox56.Style.Visible = true;
            this.textBox56.Value = "Piutang";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.81D), Telerik.Reporting.Drawing.Unit.Inch(0.196D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.758D), Telerik.Reporting.Drawing.Unit.Inch(0.268D));
            this.textBox2.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox2.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox2.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox2.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Name = "Courier New";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Style.Visible = true;
            this.textBox2.Value = "Diskon";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.041D), Telerik.Reporting.Drawing.Unit.Inch(0.196D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.758D), Telerik.Reporting.Drawing.Unit.Inch(0.268D));
            this.textBox10.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox10.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox10.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox10.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox10.Style.Font.Bold = true;
            this.textBox10.Style.Font.Name = "Courier New";
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox10.Style.Visible = true;
            this.textBox10.Value = "Transfer";
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.196D));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.346D), Telerik.Reporting.Drawing.Unit.Inch(0.268D));
            this.textBox20.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox20.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox20.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox20.Style.Font.Bold = true;
            this.textBox20.Style.Font.Name = "Courier New";
            this.textBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox20.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox20.Value = "No.";
            // 
            // textBox63
            // 
            this.textBox63.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(10.052D), Telerik.Reporting.Drawing.Unit.Inch(0.196D));
            this.textBox63.Name = "textBox56";
            this.textBox63.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.87D), Telerik.Reporting.Drawing.Unit.Inch(0.268D));
            this.textBox63.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox63.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox63.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox63.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox63.Style.Font.Bold = true;
            this.textBox63.Style.Font.Name = "Courier New";
            this.textBox63.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox63.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox63.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox63.Style.Visible = true;
            this.textBox63.Value = "Total DP";
            // 
            // textBox65
            // 
            this.textBox65.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.327D), Telerik.Reporting.Drawing.Unit.Inch(0.196D));
            this.textBox65.Name = "textBox56";
            this.textBox65.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.81D), Telerik.Reporting.Drawing.Unit.Inch(0.268D));
            this.textBox65.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox65.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox65.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox65.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox65.Style.Font.Bold = true;
            this.textBox65.Style.Font.Name = "Courier New";
            this.textBox65.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox65.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox65.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox65.Style.Visible = true;
            this.textBox65.Value = "DP Offset";
            // 
            // textBox36
            // 
            this.textBox36.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(9.147D), Telerik.Reporting.Drawing.Unit.Inch(0.196D));
            this.textBox36.Name = "textBox56";
            this.textBox36.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.894D), Telerik.Reporting.Drawing.Unit.Inch(0.268D));
            this.textBox36.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox36.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox36.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox36.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox36.Style.Font.Bold = true;
            this.textBox36.Style.Font.Name = "Courier New";
            this.textBox36.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox36.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox36.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox36.Style.Visible = true;
            this.textBox36.Value = "DP Kembali";
            // 
            // textBox31
            // 
            this.textBox31.Format = "{0:N2}";
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.112D), Telerik.Reporting.Drawing.Unit.Inch(0.079D));
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.492D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox31.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox31.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox31.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox31.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox31.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox31.Style.Color = System.Drawing.Color.DarkMagenta;
            this.textBox31.Style.Font.Bold = true;
            this.textBox31.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox31.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox31.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox31.Value = "=sum(netto)";
            // 
            // textBox33
            // 
            this.textBox33.Format = "{0:N2}";
            this.textBox33.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.831D), Telerik.Reporting.Drawing.Unit.Inch(0.079D));
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.153D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox33.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox33.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox33.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox33.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox33.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox33.Style.Color = System.Drawing.Color.DarkMagenta;
            this.textBox33.Style.Font.Bold = true;
            this.textBox33.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox33.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox33.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox33.Value = "=sum(totaltagihan)";
            // 
            // textBox34
            // 
            this.textBox34.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.425D), Telerik.Reporting.Drawing.Unit.Inch(0.079D));
            this.textBox34.Name = "textBox34";
            this.textBox34.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.299D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox34.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox34.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox34.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox34.Style.Color = System.Drawing.Color.DarkMagenta;
            this.textBox34.Style.Font.Bold = true;
            this.textBox34.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox34.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox34.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox34.Value = "Total : ";
            // 
            // groupFooterSection4
            // 
            this.groupFooterSection4.Height = Telerik.Reporting.Drawing.Unit.Cm(0.482D);
            this.groupFooterSection4.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox50,
            this.textBox51,
            this.textBox52,
            this.textBox59,
            this.textBox60,
            this.textBox61,
            this.textBox62,
            this.textBox67,
            this.textBox70});
            this.groupFooterSection4.Name = "groupFooterSection4";
            // 
            // textBox50
            // 
            this.textBox50.Format = "{0:N0}";
            this.textBox50.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(9.147D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox50.Name = "textBox69";
            this.textBox50.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.894D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox50.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox50.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox50.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox50.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox50.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox50.Style.Color = System.Drawing.Color.Black;
            this.textBox50.Style.Font.Bold = true;
            this.textBox50.Style.Font.Name = "Courier New";
            this.textBox50.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox50.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox50.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox50.Value = "=sum(DPReturn)";
            // 
            // textBox51
            // 
            this.textBox51.Format = "{0:N0}";
            this.textBox51.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.327D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox51.Name = "textBox69";
            this.textBox51.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.81D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox51.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox51.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox51.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox51.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox51.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox51.Style.Color = System.Drawing.Color.Black;
            this.textBox51.Style.Font.Bold = true;
            this.textBox51.Style.Font.Name = "Courier New";
            this.textBox51.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox51.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox51.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox51.Value = "=sum(DPOffset)";
            // 
            // textBox52
            // 
            this.textBox52.Format = "{0:N0}";
            this.textBox52.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.81D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox52.Name = "textBox26";
            this.textBox52.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.758D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox52.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox52.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox52.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox52.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox52.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox52.Style.Color = System.Drawing.Color.Black;
            this.textBox52.Style.Font.Bold = true;
            this.textBox52.Style.Font.Name = "Courier New";
            this.textBox52.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox52.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox52.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox52.Value = "=sum(Discount)";
            // 
            // textBox59
            // 
            this.textBox59.Format = "{0:N0}";
            this.textBox59.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.041D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox59.Name = "textBox25";
            this.textBox59.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.758D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox59.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox59.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox59.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox59.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox59.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox59.Style.Color = System.Drawing.Color.Black;
            this.textBox59.Style.Font.Bold = true;
            this.textBox59.Style.Font.Name = "Courier New";
            this.textBox59.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox59.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox59.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox59.Value = "=sum(Transfer)";
            // 
            // textBox60
            // 
            this.textBox60.Format = "{0:N0}";
            this.textBox60.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.568D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox60.Name = "textBox43";
            this.textBox60.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.758D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox60.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox60.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox60.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox60.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox60.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox60.Style.Color = System.Drawing.Color.Black;
            this.textBox60.Style.Font.Bold = true;
            this.textBox60.Style.Font.Name = "Courier New";
            this.textBox60.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox60.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox60.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox60.Value = "=sum(AR)";
            // 
            // textBox61
            // 
            this.textBox61.Format = "{0:N0}";
            this.textBox61.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.231D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox61.Name = "textBox42";
            this.textBox61.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox61.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox61.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox61.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox61.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox61.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox61.Style.Color = System.Drawing.Color.Black;
            this.textBox61.Style.Font.Bold = true;
            this.textBox61.Style.Font.Name = "Courier New";
            this.textBox61.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox61.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox61.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox61.Value = "=sum(DebitCard)";
            // 
            // textBox62
            // 
            this.textBox62.Format = "{0:N0}";
            this.textBox62.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.41D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox62.Name = "textBox41";
            this.textBox62.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.821D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox62.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox62.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox62.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox62.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox62.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox62.Style.Color = System.Drawing.Color.Black;
            this.textBox62.Style.Font.Bold = true;
            this.textBox62.Style.Font.Name = "Courier New";
            this.textBox62.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox62.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox62.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox62.Value = "=sum(CreditCard)";
            // 
            // textBox67
            // 
            this.textBox67.Format = "{0:N0}";
            this.textBox67.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.198D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox67.Name = "textBox38";
            this.textBox67.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.18D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox67.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox67.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox67.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox67.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox67.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox67.Style.Color = System.Drawing.Color.Black;
            this.textBox67.Style.Font.Bold = true;
            this.textBox67.Style.Font.Name = "Courier New";
            this.textBox67.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox67.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox67.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox67.Value = "=sum(Cash)";
            // 
            // textBox70
            // 
            this.textBox70.Format = "Total Tipe {0} :";
            this.textBox70.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.063D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox70.Name = "textBox21";
            this.textBox70.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.099D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox70.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox70.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox70.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox70.Style.Color = System.Drawing.Color.Black;
            this.textBox70.Style.Font.Bold = true;
            this.textBox70.Style.Font.Name = "Courier New";
            this.textBox70.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox70.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox70.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox70.Value = "=PaymentTypeName";
            // 
            // groupHeaderSection4
            // 
            this.groupHeaderSection4.Height = Telerik.Reporting.Drawing.Unit.Cm(0.4D);
            this.groupHeaderSection4.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox48,
            this.textBox3,
            this.textBox49});
            this.groupHeaderSection4.Name = "groupHeaderSection4";
            this.groupHeaderSection4.PrintOnEveryPage = true;
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.69D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox3.Name = "textBox40";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.133D), Telerik.Reporting.Drawing.Unit.Inch(0.157D));
            this.textBox3.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox3.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox3.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox3.Style.Color = System.Drawing.Color.Black;
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Name = "Times New Roman";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = ":";
            // 
            // textBox49
            // 
            this.textBox49.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox49.Name = "textBox49";
            this.textBox49.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.659D), Telerik.Reporting.Drawing.Unit.Inch(0.157D));
            this.textBox49.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox49.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox49.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox49.Style.Color = System.Drawing.Color.Black;
            this.textBox49.Style.Font.Bold = true;
            this.textBox49.Style.Font.Name = "Courier New";
            this.textBox49.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox49.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox49.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox49.Value = "Tipe";
            // 
            // textBox73
            // 
            this.textBox73.Format = "{0:N0}";
            this.textBox73.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.131D), Telerik.Reporting.Drawing.Unit.Inch(0.431D));
            this.textBox73.Name = "textBox38";
            this.textBox73.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.914D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox73.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox73.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.textBox73.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox73.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox73.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox73.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox73.Style.Font.Bold = true;
            this.textBox73.Style.Font.Name = "Courier New";
            this.textBox73.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.textBox73.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox73.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox73.Value = "=sum(Cash)";
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(1.813D);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox73,
            this.textBox74,
            this.textBox75,
            this.textBox76,
            this.textBox77,
            this.textBox78,
            this.textBox79,
            this.textBox80,
            this.textBox81,
            this.textBox82,
            this.textBox83,
            this.textBox84,
            this.textBox85,
            this.textBox86,
            this.textBox87,
            this.textBox88,
            this.textBox89,
            this.textBox9,
            this.textBox13});
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // textBox74
            // 
            this.textBox74.Format = "{0:N0}";
            this.textBox74.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.045D), Telerik.Reporting.Drawing.Unit.Inch(0.431D));
            this.textBox74.Name = "textBox41";
            this.textBox74.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.758D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox74.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox74.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.textBox74.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox74.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox74.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox74.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox74.Style.Font.Bold = true;
            this.textBox74.Style.Font.Name = "Courier New";
            this.textBox74.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.textBox74.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox74.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox74.Value = "=sum(CreditCard)";
            // 
            // textBox75
            // 
            this.textBox75.Format = "{0:N0}";
            this.textBox75.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.814D), Telerik.Reporting.Drawing.Unit.Inch(0.431D));
            this.textBox75.Name = "textBox42";
            this.textBox75.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox75.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox75.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.textBox75.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox75.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox75.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox75.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox75.Style.Font.Bold = true;
            this.textBox75.Style.Font.Name = "Courier New";
            this.textBox75.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.textBox75.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox75.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox75.Value = "=sum(DebitCard)";
            // 
            // textBox76
            // 
            this.textBox76.Format = "Total Kasir {0} :";
            this.textBox76.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.24D), Telerik.Reporting.Drawing.Unit.Inch(0.42D));
            this.textBox76.Name = "textBox21";
            this.textBox76.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.741D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox76.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox76.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox76.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox76.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox76.Style.Font.Bold = true;
            this.textBox76.Style.Font.Name = "Courier New";
            this.textBox76.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox76.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox76.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox76.Value = "Grand Total";
            // 
            // textBox77
            // 
            this.textBox77.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.127D), Telerik.Reporting.Drawing.Unit.Inch(0.162D));
            this.textBox77.Name = "textBox55";
            this.textBox77.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.914D), Telerik.Reporting.Drawing.Unit.Inch(0.268D));
            this.textBox77.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox77.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox77.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox77.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox77.Style.Font.Bold = true;
            this.textBox77.Style.Font.Name = "Courier New";
            this.textBox77.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox77.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox77.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox77.Style.Visible = true;
            this.textBox77.Value = "Tunai";
            // 
            // textBox78
            // 
            this.textBox78.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.042D), Telerik.Reporting.Drawing.Unit.Inch(0.162D));
            this.textBox78.Name = "textBox54";
            this.textBox78.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.758D), Telerik.Reporting.Drawing.Unit.Inch(0.268D));
            this.textBox78.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox78.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox78.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox78.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox78.Style.Font.Bold = true;
            this.textBox78.Style.Font.Name = "Courier New";
            this.textBox78.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox78.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox78.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox78.Style.Visible = true;
            this.textBox78.Value = "K.Kredit";
            // 
            // textBox79
            // 
            this.textBox79.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.814D), Telerik.Reporting.Drawing.Unit.Inch(0.162D));
            this.textBox79.Name = "textBox53";
            this.textBox79.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8D), Telerik.Reporting.Drawing.Unit.Inch(0.268D));
            this.textBox79.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox79.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox79.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox79.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox79.Style.Font.Bold = true;
            this.textBox79.Style.Font.Name = "Courier New";
            this.textBox79.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox79.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox79.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox79.Style.Visible = true;
            this.textBox79.Value = "K.Debit";
            // 
            // textBox80
            // 
            this.textBox80.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.624D), Telerik.Reporting.Drawing.Unit.Inch(0.162D));
            this.textBox80.Name = "textBox10";
            this.textBox80.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.758D), Telerik.Reporting.Drawing.Unit.Inch(0.268D));
            this.textBox80.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox80.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox80.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox80.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox80.Style.Font.Bold = true;
            this.textBox80.Style.Font.Name = "Courier New";
            this.textBox80.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox80.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox80.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox80.Style.Visible = true;
            this.textBox80.Value = "Transfer";
            // 
            // textBox81
            // 
            this.textBox81.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.383D), Telerik.Reporting.Drawing.Unit.Inch(0.162D));
            this.textBox81.Name = "textBox2";
            this.textBox81.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.758D), Telerik.Reporting.Drawing.Unit.Inch(0.268D));
            this.textBox81.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox81.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox81.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox81.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox81.Style.Font.Bold = true;
            this.textBox81.Style.Font.Name = "Courier New";
            this.textBox81.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox81.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox81.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox81.Style.Visible = true;
            this.textBox81.Value = "Discount";
            // 
            // textBox82
            // 
            this.textBox82.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.141D), Telerik.Reporting.Drawing.Unit.Inch(0.162D));
            this.textBox82.Name = "textBox56";
            this.textBox82.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.758D), Telerik.Reporting.Drawing.Unit.Inch(0.268D));
            this.textBox82.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox82.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox82.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox82.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox82.Style.Font.Bold = true;
            this.textBox82.Style.Font.Name = "Courier New";
            this.textBox82.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox82.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox82.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox82.Style.Visible = true;
            this.textBox82.Value = "Piutang";
            // 
            // textBox83
            // 
            this.textBox83.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.91D), Telerik.Reporting.Drawing.Unit.Inch(0.162D));
            this.textBox83.Name = "textBox56";
            this.textBox83.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.821D), Telerik.Reporting.Drawing.Unit.Inch(0.268D));
            this.textBox83.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox83.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox83.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox83.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox83.Style.Font.Bold = true;
            this.textBox83.Style.Font.Name = "Courier New";
            this.textBox83.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox83.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox83.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox83.Style.Visible = true;
            this.textBox83.Value = "DP Offset";
            // 
            // textBox84
            // 
            this.textBox84.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.741D), Telerik.Reporting.Drawing.Unit.Inch(0.162D));
            this.textBox84.Name = "textBox56";
            this.textBox84.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.904D), Telerik.Reporting.Drawing.Unit.Inch(0.268D));
            this.textBox84.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox84.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox84.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox84.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox84.Style.Font.Bold = true;
            this.textBox84.Style.Font.Name = "Courier New";
            this.textBox84.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox84.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox84.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox84.Style.Visible = true;
            this.textBox84.Value = "DP Kembali";
            // 
            // textBox85
            // 
            this.textBox85.Format = "{0:N0}";
            this.textBox85.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.624D), Telerik.Reporting.Drawing.Unit.Inch(0.431D));
            this.textBox85.Name = "textBox25";
            this.textBox85.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.758D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox85.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox85.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.textBox85.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox85.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox85.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox85.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox85.Style.Font.Bold = true;
            this.textBox85.Style.Font.Name = "Courier New";
            this.textBox85.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.textBox85.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox85.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox85.Value = "=sum(Transfer)";
            // 
            // textBox86
            // 
            this.textBox86.Format = "{0:N0}";
            this.textBox86.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.383D), Telerik.Reporting.Drawing.Unit.Inch(0.431D));
            this.textBox86.Name = "textBox26";
            this.textBox86.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.758D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox86.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox86.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.textBox86.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox86.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox86.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox86.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox86.Style.Font.Bold = true;
            this.textBox86.Style.Font.Name = "Courier New";
            this.textBox86.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.textBox86.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox86.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox86.Value = "=sum(Discount)";
            // 
            // textBox87
            // 
            this.textBox87.Format = "{0:N0}";
            this.textBox87.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.141D), Telerik.Reporting.Drawing.Unit.Inch(0.431D));
            this.textBox87.Name = "textBox43";
            this.textBox87.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.758D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox87.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox87.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.textBox87.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox87.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox87.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox87.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox87.Style.Font.Bold = true;
            this.textBox87.Style.Font.Name = "Courier New";
            this.textBox87.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.textBox87.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox87.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox87.Value = "=sum(AR)";
            // 
            // textBox88
            // 
            this.textBox88.Format = "{0:N0}";
            this.textBox88.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.91D), Telerik.Reporting.Drawing.Unit.Inch(0.431D));
            this.textBox88.Name = "textBox69";
            this.textBox88.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.821D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox88.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox88.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.textBox88.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox88.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox88.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox88.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox88.Style.Font.Bold = true;
            this.textBox88.Style.Font.Name = "Courier New";
            this.textBox88.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.textBox88.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox88.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox88.Value = "=sum(DPOffset)";
            // 
            // textBox89
            // 
            this.textBox89.Format = "{0:N0}";
            this.textBox89.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.741D), Telerik.Reporting.Drawing.Unit.Inch(0.431D));
            this.textBox89.Name = "textBox69";
            this.textBox89.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.904D), Telerik.Reporting.Drawing.Unit.Inch(0.19D));
            this.textBox89.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox89.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.textBox89.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox89.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox89.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox89.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox89.Style.Font.Bold = true;
            this.textBox89.Style.Font.Name = "Courier New";
            this.textBox89.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.textBox89.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox89.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox89.Value = "=sum(DPReturn)";
            // 
            // textBox9
            // 
            this.textBox9.Format = "{0:N0}";
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(9.645D), Telerik.Reporting.Drawing.Unit.Inch(0.433D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.219D), Telerik.Reporting.Drawing.Unit.Inch(0.188D));
            this.textBox9.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Double;
            this.textBox9.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.textBox9.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.textBox9.Style.Font.Bold = true;
            this.textBox9.Style.Font.Name = "Courier New";
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox9.Value = "=sum(Cash - DPReturn)";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(9.656D), Telerik.Reporting.Drawing.Unit.Inch(0.162D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.208D), Telerik.Reporting.Drawing.Unit.Inch(0.271D));
            this.textBox13.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox13.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.Value = "Uang Disetor";
            // 
            // DailyReceiptPerCashierRpt
            // 
            group1.GroupFooter = this.groupFooterSection1;
            group1.GroupHeader = this.groupHeaderSection1;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("=CashierName"));
            group1.Name = "group1";
            group2.GroupFooter = this.groupFooterSection4;
            group2.GroupHeader = this.groupHeaderSection4;
            group2.Groupings.Add(new Telerik.Reporting.Grouping("=PaymentTypeName"));
            group2.Name = "group4";
            group3.GroupFooter = this.groupFooterSection2;
            group3.GroupHeader = this.groupHeaderSection2;
            group3.Groupings.Add(new Telerik.Reporting.Grouping("=PaymentDate"));
            group3.Name = "group2";
            group3.Sortings.Add(new Telerik.Reporting.Sorting("=PaymentNo", Telerik.Reporting.SortDirection.Asc));
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2,
            group3});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection1,
            this.groupFooterSection1,
            this.groupHeaderSection4,
            this.groupFooterSection4,
            this.groupHeaderSection2,
            this.groupFooterSection2,
            this.pageHeader,
            this.detail,
            this.pageFooter,
            this.reportFooterSection1});
            this.Name = "DailyReceiptPerCashierRpt";
            this.PageSettings.Landscape = true;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Cm(0.1D), Telerik.Reporting.Drawing.Unit.Cm(0.1D), Telerik.Reporting.Drawing.Unit.Cm(1D), Telerik.Reporting.Drawing.Unit.Cm(1D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Sortings.Add(new Telerik.Reporting.Sorting("=PaymentNo", Telerik.Reporting.SortDirection.Asc));
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Bold = true;
            this.Style.Font.Name = "Courier New";
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(27.74D);
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
        private Telerik.Reporting.TextBox textBox15;
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
        private Telerik.Reporting.TextBox textBox28;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox textBox33;
        private Telerik.Reporting.TextBox textBox34;
        private Telerik.Reporting.TextBox textBox35;
        private Telerik.Reporting.TextBox textBox40;
        private Telerik.Reporting.TextBox textBox39;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.TextBox textBox38;
        private Telerik.Reporting.TextBox textBox41;
        private Telerik.Reporting.TextBox textBox42;
        private Telerik.Reporting.TextBox textBox43;
        private Telerik.Reporting.TextBox textBox44;
        private Telerik.Reporting.TextBox textBox45;
        private Telerik.Reporting.TextBox textBox46;
        private Telerik.Reporting.TextBox textBox47;
        private Telerik.Reporting.TextBox textBox48;
        private Telerik.Reporting.TextBox textBox53;
        private Telerik.Reporting.TextBox textBox54;
        private Telerik.Reporting.TextBox textBox55;
        private Telerik.Reporting.TextBox textBox56;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox57;
        private Telerik.Reporting.TextBox textBox58;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox64;
        private Telerik.Reporting.TextBox textBox63;
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
        private Telerik.Reporting.TextBox textBox52;
        private Telerik.Reporting.TextBox textBox59;
        private Telerik.Reporting.TextBox textBox60;
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
        private Telerik.Reporting.TextBox textBox81;
        private Telerik.Reporting.TextBox textBox82;
        private Telerik.Reporting.TextBox textBox83;
        private Telerik.Reporting.TextBox textBox84;
        private Telerik.Reporting.TextBox textBox85;
        private Telerik.Reporting.TextBox textBox86;
        private Telerik.Reporting.TextBox textBox87;
        private Telerik.Reporting.TextBox textBox88;
        private Telerik.Reporting.TextBox textBox89;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox txtCard;
    }
}