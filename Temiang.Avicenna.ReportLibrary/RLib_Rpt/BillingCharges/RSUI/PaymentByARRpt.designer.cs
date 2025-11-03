namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges.RSUI
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class PaymentByARRpt
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.txtTanggal = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox66 = new Telerik.Reporting.TextBox();
            this.textBox37 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox63 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.textBox82 = new Telerik.Reporting.TextBox();
            this.group1 = new Telerik.Reporting.Group();
            this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox69 = new Telerik.Reporting.TextBox();
            this.textBox72 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.textBox60 = new Telerik.Reporting.TextBox();
            this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
            this.textBox86 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox65 = new Telerik.Reporting.TextBox();
            this.textBox36 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox35 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.textBox33 = new Telerik.Reporting.TextBox();
            this.textBox34 = new Telerik.Reporting.TextBox();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(2.89989972114563);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtTanggal,
            this.textBox5});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.Font.Name = "Tahoma";
            // 
            // txtTanggal
            // 
            this.txtTanggal.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.9548063278198242), Telerik.Reporting.Drawing.Unit.Inch(0.95208340883255));
            this.txtTanggal.Name = "txtPeriod";
            this.txtTanggal.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.0787396430969238), Telerik.Reporting.Drawing.Unit.Inch(0.13894350826740265));
            this.txtTanggal.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.txtTanggal.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.txtTanggal.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.txtTanggal.Style.Font.Bold = true;
            this.txtTanggal.Style.Font.Name = "Tahoma";
            this.txtTanggal.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(12);
            this.txtTanggal.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtTanggal.Value = "Tanggal :";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.9548063278198242), Telerik.Reporting.Drawing.Unit.Inch(0.71041673421859741));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.0787396430969238), Telerik.Reporting.Drawing.Unit.Inch(0.23843836784362793));
            this.textBox5.Style.Font.Bold = false;
            this.textBox5.Style.Font.Name = "Tahoma";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(18);
            this.textBox5.Style.Font.Strikeout = false;
            this.textBox5.Style.Font.Underline = false;
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.Value = "Laporan Pasien Jaminan";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.37048420310020447), Telerik.Reporting.Drawing.Unit.Inch(0.21703243255615234));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0989066362380981), Telerik.Reporting.Drawing.Unit.Inch(0.21628029644489288));
            this.textBox7.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox7.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox7.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox7.Style.Font.Bold = false;
            this.textBox7.Style.Font.Name = "Tahoma";
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.Value = "Layanan";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(1.0239378213882446);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox66,
            this.textBox37,
            this.textBox2,
            this.textBox63,
            this.textBox11,
            this.textBox8,
            this.textBox1,
            this.textBox3,
            this.textBox17,
            this.textBox18,
            this.textBox25,
            this.textBox24});
            this.detail.Name = "detail";
            this.detail.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.detail.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            // 
            // textBox66
            // 
            this.textBox66.Format = "{0:N0}";
            this.textBox66.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.0313363075256348), Telerik.Reporting.Drawing.Unit.Inch(0.21350224316120148));
            this.textBox66.Name = "textBox15";
            this.textBox66.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0154966115951538), Telerik.Reporting.Drawing.Unit.Inch(0.18958337604999542));
            this.textBox66.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox66.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox66.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox66.Style.Font.Bold = false;
            this.textBox66.Style.Font.Name = "Tahoma";
            this.textBox66.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox66.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox66.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox66.Value = "=Amount";
            // 
            // textBox37
            // 
            this.textBox37.Format = "{0:N0}";
            this.textBox37.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.0469117164611816), Telerik.Reporting.Drawing.Unit.Inch(0.2135416716337204));
            this.textBox37.Name = "textBox15";
            this.textBox37.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0154963731765747), Telerik.Reporting.Drawing.Unit.Inch(0.18958337604999542));
            this.textBox37.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox37.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox37.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox37.Style.Font.Bold = false;
            this.textBox37.Style.Font.Name = "Tahoma";
            this.textBox37.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox37.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox37.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox37.Value = "=Disc";
            // 
            // textBox2
            // 
            this.textBox2.Format = "{0:N0}";
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.0624871253967285), Telerik.Reporting.Drawing.Unit.Inch(0.21350224316120148));
            this.textBox2.Name = "textBox17";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0154963731765747), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox2.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox2.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox2.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox2.Style.Font.Bold = false;
            this.textBox2.Style.Font.Name = "Tahoma";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "=PatientAmount";
            // 
            // textBox63
            // 
            this.textBox63.Format = "{0:N0}";
            this.textBox63.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.0780625343322754), Telerik.Reporting.Drawing.Unit.Inch(0.2135416716337204));
            this.textBox63.Name = "textBox63";
            this.textBox63.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0154974460601807), Telerik.Reporting.Drawing.Unit.Inch(0.18954403698444367));
            this.textBox63.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox63.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox63.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox63.Style.Font.Bold = false;
            this.textBox63.Style.Font.Name = "Tahoma";
            this.textBox63.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox63.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox63.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox63.Value = "=GuarantorAmount";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.4694696664810181), Telerik.Reporting.Drawing.Unit.Inch(0.2135416716337204));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.5617880821228027), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox11.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox11.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox11.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox11.Style.Font.Bold = false;
            this.textBox11.Style.Font.Name = "Tahoma";
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox11.Value = "=PatientName";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.4694696664810181), Telerik.Reporting.Drawing.Unit.Inch(0.023879369720816612));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1242878437042236), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox8.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox8.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox8.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox8.Style.Font.Bold = false;
            this.textBox8.Style.Font.Name = "Tahoma";
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Value = "=RegistrationNo+\' (\'+MedicalNo+\')\'";
            // 
            // textBox1
            // 
            this.textBox1.Format = "{0}.";
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.9472862068996619E-08), Telerik.Reporting.Drawing.Unit.Inch(0.023879369720816612));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.34621071815490723), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
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
            // textBox3
            // 
            this.textBox3.Format = "{0: dd-MMMM-yyyy}";
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.34628948569297791), Telerik.Reporting.Drawing.Unit.Inch(0.21350224316120148));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1231013536453247), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox3.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox3.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox3.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox3.Style.Font.Bold = false;
            this.textBox3.Style.Font.Name = "Tahoma";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox3.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = "=TglPulang";
            // 
            // textBox17
            // 
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.5938365459442139), Telerik.Reporting.Drawing.Unit.Inch(0.023879369720816612));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.3350474834442139), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox17.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox17.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox17.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox17.Style.Font.Bold = false;
            this.textBox17.Style.Font.Name = "Tahoma";
            this.textBox17.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox17.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox17.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox17.Value = "=ServiceUnitName";
            // 
            // textBox18
            // 
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.9289627075195312), Telerik.Reporting.Drawing.Unit.Inch(0.023879369720816612));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1645970344543457), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox18.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox18.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox18.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox18.Style.Font.Bold = false;
            this.textBox18.Style.Font.Name = "Tahoma";
            this.textBox18.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox18.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox18.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox18.Value = "";
            // 
            // textBox25
            // 
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.37048420310020447), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0989066362380981), Telerik.Reporting.Drawing.Unit.Inch(0.21342341601848602));
            this.textBox25.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox25.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox25.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox25.Style.Font.Bold = false;
            this.textBox25.Style.Font.Name = "Tahoma";
            this.textBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox25.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox25.Value = "=Pasien";
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
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.4482226371765137), Telerik.Reporting.Drawing.Unit.Inch(0.19999916851520538));
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
            this.textBox82.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.024194717407226562), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox82.Name = "textBox82";
            this.textBox82.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.6366405487060547), Telerik.Reporting.Drawing.Unit.Inch(0.19999940693378449));
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
            new Telerik.Reporting.Grouping("=GuarantorName")});
            this.group1.Name = "group1";
            // 
            // groupFooterSection1
            // 
            this.groupFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.63669806718826294);
            this.groupFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox21,
            this.textBox69,
            this.textBox72,
            this.textBox32,
            this.textBox60,
            this.textBox26});
            this.groupFooterSection1.Name = "groupFooterSection1";
            this.groupFooterSection1.PageBreak = Telerik.Reporting.PageBreak.None;
            this.groupFooterSection1.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            // 
            // textBox21
            // 
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.643817126750946), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.3874399662017822), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox21.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox21.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox21.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox21.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox21.Style.Font.Bold = false;
            this.textBox21.Style.Font.Name = "Tahoma";
            this.textBox21.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox21.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox21.Value = "=GuarantorName";
            // 
            // textBox69
            // 
            this.textBox69.Format = "{0:N0}";
            this.textBox69.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.0313363075256348), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox69.Name = "textBox69";
            this.textBox69.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0154969692230225), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox69.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox69.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox69.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox69.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox69.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox69.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox69.Style.Font.Bold = false;
            this.textBox69.Style.Font.Name = "Tahoma";
            this.textBox69.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox69.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox69.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox69.Value = "=sum(Amount)";
            // 
            // textBox72
            // 
            this.textBox72.Format = "{0:N0}";
            this.textBox72.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.0469117164611816), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox72.Name = "textBox69";
            this.textBox72.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0050778388977051), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox72.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox72.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox72.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox72.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox72.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox72.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox72.Style.Font.Bold = false;
            this.textBox72.Style.Font.Name = "Tahoma";
            this.textBox72.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox72.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox72.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox72.Value = "=sum(Disc)";
            // 
            // textBox32
            // 
            this.textBox32.Format = "{0:N0}";
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.0520687103271484), Telerik.Reporting.Drawing.Unit.Inch(7.8837074397597462E-05));
            this.textBox32.Name = "textBox69";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0154943466186523), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox32.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox32.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox32.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox32.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox32.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox32.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox32.Style.Font.Bold = false;
            this.textBox32.Style.Font.Name = "Tahoma";
            this.textBox32.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox32.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox32.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox32.Value = "=sum(PatientAmount)";
            // 
            // textBox60
            // 
            this.textBox60.Format = "{0:N0}";
            this.textBox60.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.0780634880065918), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox60.Name = "textBox9";
            this.textBox60.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0154966115951538), Telerik.Reporting.Drawing.Unit.Inch(0.18966229259967804));
            this.textBox60.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox60.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox60.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox60.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox60.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox60.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox60.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox60.Style.Font.Bold = false;
            this.textBox60.Style.Font.Name = "Tahoma";
            this.textBox60.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox60.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox60.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox60.Value = "=Sum(GuarantorAmount)";
            // 
            // groupHeaderSection1
            // 
            this.groupHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(1.7629374265670776);
            this.groupHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox86,
            this.textBox7,
            this.textBox20,
            this.textBox65,
            this.textBox36,
            this.textBox14,
            this.textBox35,
            this.textBox9,
            this.textBox13,
            this.textBox15,
            this.textBox16,
            this.textBox4,
            this.textBox23});
            this.groupHeaderSection1.Name = "groupHeaderSection1";
            this.groupHeaderSection1.PrintOnEveryPage = true;
            // 
            // textBox86
            // 
            this.textBox86.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.092884860932827), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox86.Name = "textBox86";
            this.textBox86.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.3704562187194824), Telerik.Reporting.Drawing.Unit.Inch(0.2169535905122757));
            this.textBox86.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox86.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox86.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox86.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox86.Style.Font.Bold = false;
            this.textBox86.Style.Font.Name = "Tahoma";
            this.textBox86.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox86.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox86.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox86.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox86.Value = "=GuarantorName";
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.024194717407226562), Telerik.Reporting.Drawing.Unit.Inch(0.21703243255615234));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.34621071815490723), Telerik.Reporting.Drawing.Unit.Inch(0.43263944983482361));
            this.textBox20.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox20.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox20.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox20.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox20.Style.Font.Bold = false;
            this.textBox20.Style.Font.Name = "Tahoma";
            this.textBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox20.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox20.Value = "No.";
            // 
            // textBox65
            // 
            this.textBox65.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.0313363075256348), Telerik.Reporting.Drawing.Unit.Inch(0.43339157104492188));
            this.textBox65.Name = "textBox56";
            this.textBox65.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0154968500137329), Telerik.Reporting.Drawing.Unit.Inch(0.21628022193908691));
            this.textBox65.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox65.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox65.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox65.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox65.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox65.Style.Font.Bold = false;
            this.textBox65.Style.Font.Name = "Tahoma";
            this.textBox65.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox65.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox65.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox65.Style.Visible = true;
            this.textBox65.Value = "Tagihan";
            // 
            // textBox36
            // 
            this.textBox36.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.0469117164611816), Telerik.Reporting.Drawing.Unit.Inch(0.43339157104492188));
            this.textBox36.Name = "textBox56";
            this.textBox36.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0154968500137329), Telerik.Reporting.Drawing.Unit.Inch(0.21628022193908691));
            this.textBox36.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox36.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox36.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox36.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox36.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox36.Style.Font.Bold = false;
            this.textBox36.Style.Font.Name = "Tahoma";
            this.textBox36.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox36.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox36.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox36.Style.Visible = true;
            this.textBox36.Value = "Discount";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.0624871253967285), Telerik.Reporting.Drawing.Unit.Inch(0.43339157104492188));
            this.textBox14.Name = "textBox56";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0154968500137329), Telerik.Reporting.Drawing.Unit.Inch(0.21628022193908691));
            this.textBox14.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox14.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox14.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox14.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox14.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox14.Style.Font.Bold = false;
            this.textBox14.Style.Font.Name = "Tahoma";
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox14.Style.Visible = true;
            this.textBox14.Value = "Pasien";
            // 
            // textBox35
            // 
            this.textBox35.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.0780634880065918), Telerik.Reporting.Drawing.Unit.Inch(0.43339157104492188));
            this.textBox35.Name = "textBox56";
            this.textBox35.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0154968500137329), Telerik.Reporting.Drawing.Unit.Inch(0.21628022193908691));
            this.textBox35.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox35.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox35.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox35.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox35.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox35.Style.Font.Bold = false;
            this.textBox35.Style.Font.Name = "Tahoma";
            this.textBox35.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox35.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox35.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox35.Style.Visible = true;
            this.textBox35.Value = "Perusahaan";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.356046199798584), Telerik.Reporting.Drawing.Unit.Inch(0.21703243255615234));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.7530906200408936), Telerik.Reporting.Drawing.Unit.Inch(0.21628022193908691));
            this.textBox9.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox9.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox9.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox9.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox9.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox9.Style.Font.Bold = false;
            this.textBox9.Style.Font.Name = "Tahoma";
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox9.Style.Visible = true;
            this.textBox9.Value = "Kasir";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.5938365459442139), Telerik.Reporting.Drawing.Unit.Inch(0.21703243255615234));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.7621307373046875), Telerik.Reporting.Drawing.Unit.Inch(0.21628022193908691));
            this.textBox13.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox13.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox13.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox13.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox13.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox13.Style.Font.Bold = false;
            this.textBox13.Style.Font.Name = "Tahoma";
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.Style.Visible = true;
            this.textBox13.Value = "Service Unit";
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.4694696664810181), Telerik.Reporting.Drawing.Unit.Inch(0.43339157104492188));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.5617873668670654), Telerik.Reporting.Drawing.Unit.Inch(0.21628022193908691));
            this.textBox15.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox15.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox15.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox15.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox15.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox15.Style.Font.Bold = false;
            this.textBox15.Style.Font.Name = "Tahoma";
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox15.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox15.Style.Visible = true;
            this.textBox15.Value = "Nama Pasien";
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.4694696664810181), Telerik.Reporting.Drawing.Unit.Inch(0.21703243255615234));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1242878437042236), Telerik.Reporting.Drawing.Unit.Inch(0.21628022193908691));
            this.textBox16.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox16.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox16.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox16.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox16.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox16.Style.Font.Bold = false;
            this.textBox16.Style.Font.Name = "Tahoma";
            this.textBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox16.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox16.Style.Visible = true;
            this.textBox16.Value = "No. Reg / No. MR";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.37048420310020447), Telerik.Reporting.Drawing.Unit.Inch(0.43339157104492188));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0989066362380981), Telerik.Reporting.Drawing.Unit.Inch(0.21628029644489288));
            this.textBox4.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox4.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox4.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox4.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox4.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox4.Style.Font.Bold = false;
            this.textBox4.Style.Font.Name = "Tahoma";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.Value = "Tgl. Berobat";
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
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(1.0316715240478516);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox6,
            this.textBox10,
            this.textBox12,
            this.textBox19,
            this.textBox22,
            this.textBox27});
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.64381790161132812), Telerik.Reporting.Drawing.Unit.Inch(0.0625));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.3874399662017822), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox6.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox6.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox6.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox6.Style.Color = System.Drawing.Color.Black;
            this.textBox6.Style.Font.Bold = true;
            this.textBox6.Style.Font.Name = "Tahoma";
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "Grand Total";
            // 
            // textBox10
            // 
            this.textBox10.Format = "{0:N0}";
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.0313363075256348), Telerik.Reporting.Drawing.Unit.Inch(0.0625));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0154969692230225), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox10.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox10.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox10.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox10.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox10.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox10.Style.Color = System.Drawing.Color.Black;
            this.textBox10.Style.Font.Bold = true;
            this.textBox10.Style.Font.Name = "Tahoma";
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox10.Value = "=sum(Amount)";
            // 
            // textBox12
            // 
            this.textBox12.Format = "{0:N0}";
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.0469117164611816), Telerik.Reporting.Drawing.Unit.Inch(0.0625));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0050778388977051), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox12.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox12.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox12.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox12.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox12.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox12.Style.Color = System.Drawing.Color.Black;
            this.textBox12.Style.Font.Bold = true;
            this.textBox12.Style.Font.Name = "Tahoma";
            this.textBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.Value = "=sum(Disc)";
            // 
            // textBox19
            // 
            this.textBox19.Format = "{0:N0}";
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.0520687103271484), Telerik.Reporting.Drawing.Unit.Inch(0.0625));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0154943466186523), Telerik.Reporting.Drawing.Unit.Inch(0.18958346545696259));
            this.textBox19.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox19.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox19.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox19.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox19.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox19.Style.Color = System.Drawing.Color.Black;
            this.textBox19.Style.Font.Bold = true;
            this.textBox19.Style.Font.Name = "Tahoma";
            this.textBox19.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox19.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox19.Value = "=sum(PatientAmount)";
            // 
            // textBox22
            // 
            this.textBox22.Format = "{0:N0}";
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.0780625343322754), Telerik.Reporting.Drawing.Unit.Inch(0.0625));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.98053812980651855), Telerik.Reporting.Drawing.Unit.Inch(0.18966229259967804));
            this.textBox22.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox22.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox22.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox22.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox22.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox22.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox22.Style.Color = System.Drawing.Color.Black;
            this.textBox22.Style.Font.Bold = true;
            this.textBox22.Style.Font.Name = "Tahoma";
            this.textBox22.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox22.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox22.Value = "=Sum(GuarantorAmount)";
            // 
            // textBox23
            // 
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.0936393737792969), Telerik.Reporting.Drawing.Unit.Inch(0.43339157104492188));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0154968500137329), Telerik.Reporting.Drawing.Unit.Inch(0.21628022193908691));
            this.textBox23.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox23.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox23.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox23.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox23.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox23.Style.Font.Bold = false;
            this.textBox23.Style.Font.Name = "Tahoma";
            this.textBox23.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox23.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox23.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox23.Style.Visible = true;
            this.textBox23.Value = "Plafond";
            // 
            // textBox24
            // 
            this.textBox24.Format = "{0:N0}";
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.0936393737792969), Telerik.Reporting.Drawing.Unit.Inch(0.21358108520507813));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0154974460601807), Telerik.Reporting.Drawing.Unit.Inch(0.18954403698444367));
            this.textBox24.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox24.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox24.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox24.Style.Font.Bold = false;
            this.textBox24.Style.Font.Name = "Tahoma";
            this.textBox24.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox24.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox24.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox24.Value = "=PlafondAmount";
            // 
            // textBox26
            // 
            this.textBox26.Format = "{0:N0}";
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.0936393737792969), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0154966115951538), Telerik.Reporting.Drawing.Unit.Inch(0.18966229259967804));
            this.textBox26.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox26.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox26.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox26.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox26.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox26.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox26.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox26.Style.Font.Bold = false;
            this.textBox26.Style.Font.Name = "Tahoma";
            this.textBox26.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox26.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox26.Value = "=Sum(PlafondAmount)";
            // 
            // textBox27
            // 
            this.textBox27.Format = "{0:N0}";
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.0586795806884766), Telerik.Reporting.Drawing.Unit.Inch(0.0625));
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0504564046859741), Telerik.Reporting.Drawing.Unit.Inch(0.18966229259967804));
            this.textBox27.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox27.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox27.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox27.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox27.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox27.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox27.Style.Color = System.Drawing.Color.Black;
            this.textBox27.Style.Font.Bold = true;
            this.textBox27.Style.Font.Name = "Tahoma";
            this.textBox27.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox27.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox27.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox27.Value = "=Sum(PlafondAmount)";
            // 
            // PaymentByARRpt
            // 
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            this.group1});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection1,
            this.groupFooterSection1,
            this.pageHeader,
            this.detail,
            this.pageFooter,
            this.reportFooterSection1});
            this.Name = "PaymentByARRpt";
            this.PageSettings.Landscape = true;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Bold = true;
            this.Style.Font.Name = "Courier New";
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(23.362325668334961);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox txtTanggal;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox11;
        private Group group1;
        private GroupFooterSection groupFooterSection1;
        private GroupHeaderSection groupHeaderSection1;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox textBox33;
        private Telerik.Reporting.TextBox textBox34;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox66;
        private Telerik.Reporting.TextBox textBox69;
        private Telerik.Reporting.TextBox textBox65;
        private Telerik.Reporting.TextBox textBox37;
        private Telerik.Reporting.TextBox textBox72;
        private Telerik.Reporting.TextBox textBox36;
        private ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.TextBox textBox60;
        private Telerik.Reporting.TextBox textBox63;
        private Telerik.Reporting.TextBox textBox35;
        private Telerik.Reporting.TextBox textBox28;
        private Telerik.Reporting.TextBox textBox82;
        private Telerik.Reporting.TextBox textBox86;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox27;
    }
}