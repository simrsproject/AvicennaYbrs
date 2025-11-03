namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSBHP
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class RegistrationTicket
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.detail = new Telerik.Reporting.DetailSection();
            this.reportHeaderSection1 = new Telerik.Reporting.ReportHeaderSection();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.medicalNoCaptionTextBox = new Telerik.Reporting.TextBox();
            this.medicalNoDataTextBox = new Telerik.Reporting.TextBox();
            this.patientNameCaptionTextBox = new Telerik.Reporting.TextBox();
            this.patientNameDataTextBox = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.checkBox1 = new Telerik.Reporting.CheckBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.052083175629377365);
            this.detail.Name = "detail";
            this.detail.Style.Font.Name = "Microsoft Sans Serif";
            this.detail.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.5);
            // 
            // reportHeaderSection1
            // 
            this.reportHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(4.3999996185302734);
            this.reportHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox10,
            this.textBox8,
            this.medicalNoCaptionTextBox,
            this.medicalNoDataTextBox,
            this.patientNameCaptionTextBox,
            this.patientNameDataTextBox,
            this.textBox2,
            this.textBox1,
            this.textBox4,
            this.textBox3,
            this.textBox5,
            this.textBox11,
            this.textBox14,
            this.textBox12,
            this.textBox15,
            this.textBox13,
            this.textBox16,
            this.textBox17,
            this.checkBox1,
            this.textBox18,
            this.textBox20,
            this.textBox19,
            this.textBox21,
            this.textBox24,
            this.textBox23,
            this.textBox25,
            this.textBox22,
            this.textBox26,
            this.textBox27,
            this.textBox7});
            this.reportHeaderSection1.Name = "reportHeaderSection1";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612), Telerik.Reporting.Drawing.Unit.Inch(0.90000009536743164));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.4999210834503174), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox10.Style.Font.Underline = true;
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox10.Value = "NOTA PEMBAYARAN RAWAT JALAN";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.099999904632568359), Telerik.Reporting.Drawing.Unit.Inch(1.10007905960083));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.4999210834503174), Telerik.Reporting.Drawing.Unit.Inch(0.19936943054199219));
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox8.Value = "=Fields.RegistrationNo";
            // 
            // medicalNoCaptionTextBox
            // 
            this.medicalNoCaptionTextBox.CanGrow = true;
            this.medicalNoCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.099999986588954926), Telerik.Reporting.Drawing.Unit.Inch(1.2995274066925049));
            this.medicalNoCaptionTextBox.Name = "medicalNoCaptionTextBox";
            this.medicalNoCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.59992122650146484), Telerik.Reporting.Drawing.Unit.Inch(0.14999999105930328));
            this.medicalNoCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.medicalNoCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.medicalNoCaptionTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.medicalNoCaptionTextBox.StyleName = "Caption";
            this.medicalNoCaptionTextBox.Value = "No. RM";
            // 
            // medicalNoDataTextBox
            // 
            this.medicalNoDataTextBox.CanGrow = true;
            this.medicalNoDataTextBox.Format = ": {0}";
            this.medicalNoDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.70000004768371582), Telerik.Reporting.Drawing.Unit.Inch(1.2995274066925049));
            this.medicalNoDataTextBox.Name = "medicalNoDataTextBox";
            this.medicalNoDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.3270044326782227), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.medicalNoDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.medicalNoDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.medicalNoDataTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.medicalNoDataTextBox.StyleName = "Data";
            this.medicalNoDataTextBox.Value = "=Fields.MedicalNo";
            // 
            // patientNameCaptionTextBox
            // 
            this.patientNameCaptionTextBox.CanGrow = true;
            this.patientNameCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.099999986588954926), Telerik.Reporting.Drawing.Unit.Inch(1.4496062994003296));
            this.patientNameCaptionTextBox.Name = "patientNameCaptionTextBox";
            this.patientNameCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.59992122650146484), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.patientNameCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.patientNameCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.patientNameCaptionTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.patientNameCaptionTextBox.StyleName = "Caption";
            this.patientNameCaptionTextBox.Value = "Nama";
            // 
            // patientNameDataTextBox
            // 
            this.patientNameDataTextBox.CanGrow = true;
            this.patientNameDataTextBox.Format = ": {0}";
            this.patientNameDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.70000004768371582), Telerik.Reporting.Drawing.Unit.Inch(1.4496062994003296));
            this.patientNameDataTextBox.Name = "patientNameDataTextBox";
            this.patientNameDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.0270040035247803), Telerik.Reporting.Drawing.Unit.Inch(0.14999999105930328));
            this.patientNameDataTextBox.Style.Font.Name = "Tahoma";
            this.patientNameDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.patientNameDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.patientNameDataTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.patientNameDataTextBox.StyleName = "Data";
            this.patientNameDataTextBox.Value = "=Fields.PatientName";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.099999986588954926), Telerik.Reporting.Drawing.Unit.Inch(1.5996850728988648));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.59992122650146484), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox2.StyleName = "Caption";
            this.textBox2.Value = "Usia/tgl";
            // 
            // textBox1
            // 
            this.textBox1.Format = ": {0}";
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.70000004768371582), Telerik.Reporting.Drawing.Unit.Inch(1.5996850728988648));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1270040273666382), Telerik.Reporting.Drawing.Unit.Inch(0.14999993145465851));
            this.textBox1.Style.Font.Name = "Tahoma";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox1.Value = "=Fields.Age";
            // 
            // textBox4
            // 
            this.textBox4.Format = "( {0:dd-MMM-yyyy} )";
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.9000000953674316), Telerik.Reporting.Drawing.Unit.Inch(1.5996850728988648));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1999214887619019), Telerik.Reporting.Drawing.Unit.Inch(0.14999993145465851));
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox4.Value = "= Fields.DateOfBirth";
            // 
            // textBox3
            // 
            this.textBox3.CanGrow = true;
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0999998226761818), Telerik.Reporting.Drawing.Unit.Inch(1.7497638463974));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.59992122650146484), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox3.StyleName = "Caption";
            this.textBox3.Value = "Jaminan";
            // 
            // textBox5
            // 
            this.textBox5.Format = ": {0}";
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.7000003457069397), Telerik.Reporting.Drawing.Unit.Inch(1.7497638463974));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.3270041942596436), Telerik.Reporting.Drawing.Unit.Inch(0.14999993145465851));
            this.textBox5.Style.Font.Name = "Tahoma";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox5.Value = "=Fields.GuarantorName";
            // 
            // textBox11
            // 
            this.textBox11.CanGrow = true;
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0999998226761818), Telerik.Reporting.Drawing.Unit.Inch(1.8997637033462524));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.59992122650146484), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox11.StyleName = "Caption";
            this.textBox11.Value = "Poli";
            // 
            // textBox14
            // 
            this.textBox14.Format = ": {0}";
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.7000003457069397), Telerik.Reporting.Drawing.Unit.Inch(1.8998426198959351));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.3270044326782227), Telerik.Reporting.Drawing.Unit.Inch(0.149921253323555));
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox14.Value = "=Fields.ServiceUnitName";
            // 
            // textBox12
            // 
            this.textBox12.CanGrow = true;
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0999998226761818), Telerik.Reporting.Drawing.Unit.Inch(2.0498425960540771));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.59992122650146484), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.textBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox12.StyleName = "Caption";
            this.textBox12.Value = "Dokter";
            // 
            // textBox15
            // 
            this.textBox15.Format = ": {0}";
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.69999986886978149), Telerik.Reporting.Drawing.Unit.Inch(2.0498425960540771));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.32692551612854), Telerik.Reporting.Drawing.Unit.Inch(0.14999993145465851));
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox15.Value = "=Fields.ParamedicName";
            // 
            // textBox13
            // 
            this.textBox13.CanGrow = true;
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.099999986588954926), Telerik.Reporting.Drawing.Unit.Inch(2.1999213695526123));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.59992122650146484), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox13.StyleName = "Caption";
            this.textBox13.Value = "Tanggal";
            // 
            // textBox16
            // 
            this.textBox16.Format = ": {0:dd MMMM yyyy}";
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.699921190738678), Telerik.Reporting.Drawing.Unit.Inch(2.1999213695526123));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.3270041942596436), Telerik.Reporting.Drawing.Unit.Inch(0.14999993145465851));
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox16.Value = "=Fields.RegistrationDate";
            // 
            // textBox17
            // 
            this.textBox17.CanGrow = true;
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609), Telerik.Reporting.Drawing.Unit.Inch(2.5000002384185791));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.726925253868103), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.textBox17.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox17.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox17.StyleName = "Caption";
            this.textBox17.Value = "Pemeriksaan/Tindakan";
            // 
            // checkBox1
            // 
            this.checkBox1.FalseValue = "False";
            this.checkBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.2000000476837158), Telerik.Reporting.Drawing.Unit.Inch(2.5000002384185791));
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3999996185302734), Telerik.Reporting.Drawing.Unit.Inch(0.14999993145465851));
            this.checkBox1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.checkBox1.Text = "  Konsultasi Dokter";
            this.checkBox1.Value = "False";
            // 
            // textBox18
            // 
            this.textBox18.CanGrow = true;
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609), Telerik.Reporting.Drawing.Unit.Inch(2.7000000476837158));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.4999208450317383), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.textBox18.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.textBox18.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.textBox18.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox18.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox18.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox18.StyleName = "Caption";
            this.textBox18.Value = "";
            // 
            // textBox20
            // 
            this.textBox20.CanGrow = true;
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612), Telerik.Reporting.Drawing.Unit.Inch(2.9000003337860107));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.726925253868103), Telerik.Reporting.Drawing.Unit.Inch(0.20000003278255463));
            this.textBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox20.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox20.StyleName = "Caption";
            this.textBox20.Value = "Obat dan Alkes";
            // 
            // textBox19
            // 
            this.textBox19.CanGrow = true;
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612), Telerik.Reporting.Drawing.Unit.Inch(3.1000792980194092));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.4999208450317383), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.textBox19.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.textBox19.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.textBox19.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox19.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox19.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox19.StyleName = "Caption";
            this.textBox19.Value = "";
            // 
            // textBox21
            // 
            this.textBox21.CanGrow = true;
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.099999748170375824), Telerik.Reporting.Drawing.Unit.Inch(3.2501580715179443));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.4999208450317383), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.textBox21.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.textBox21.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox21.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox21.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox21.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox21.StyleName = "Caption";
            this.textBox21.Value = "";
            // 
            // textBox24
            // 
            this.textBox24.CanGrow = true;
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134), Telerik.Reporting.Drawing.Unit.Inch(3.5000002384185791));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.59992122650146484), Telerik.Reporting.Drawing.Unit.Inch(0.20000003278255463));
            this.textBox24.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox24.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox24.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox24.StyleName = "Caption";
            this.textBox24.Value = "Dokter";
            // 
            // textBox23
            // 
            this.textBox23.CanGrow = true;
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5), Telerik.Reporting.Drawing.Unit.Inch(3.5000002384185791));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.5), Telerik.Reporting.Drawing.Unit.Inch(0.20000003278255463));
            this.textBox23.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox23.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox23.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox23.StyleName = "Caption";
            this.textBox23.Value = "Unit";
            // 
            // textBox25
            // 
            this.textBox25.CanGrow = true;
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.3999998569488525), Telerik.Reporting.Drawing.Unit.Inch(3.5000002384185791));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.199920654296875), Telerik.Reporting.Drawing.Unit.Inch(0.20000003278255463));
            this.textBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox25.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox25.StyleName = "Caption";
            this.textBox25.Value = "Pendaftaran";
            // 
            // textBox22
            // 
            this.textBox22.CanGrow = false;
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.4000790119171143), Telerik.Reporting.Drawing.Unit.Inch(4.2000002861022949));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.199920654296875), Telerik.Reporting.Drawing.Unit.Inch(0.19999949634075165));
            this.textBox22.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox22.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox22.Style.Font.Name = "Tahoma";
            this.textBox22.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox22.Value = "=Fields.UserName";
            // 
            // textBox26
            // 
            this.textBox26.CanGrow = false;
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.0992521047592163), Telerik.Reporting.Drawing.Unit.Inch(4.2000002861022949));
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0999997854232788), Telerik.Reporting.Drawing.Unit.Inch(0.19999949634075165));
            this.textBox26.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox26.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox26.Style.Font.Name = "Tahoma";
            this.textBox26.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox26.Value = "";
            // 
            // textBox27
            // 
            this.textBox27.CanGrow = false;
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(4.1999993324279785));
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.89992111921310425), Telerik.Reporting.Drawing.Unit.Inch(0.20000003278255463));
            this.textBox27.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox27.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox27.Style.Font.Name = "Tahoma";
            this.textBox27.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox27.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox27.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox27.StyleName = "Caption";
            this.textBox27.Value = "";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20000003278255463);
            this.pageFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox6});
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // textBox6
            // 
            this.textBox6.CanGrow = true;
            this.textBox6.Format = "Avicenna HIS, Print Date : {0:dd-MM-yyyy HH:mm}";
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.5999605655670166), Telerik.Reporting.Drawing.Unit.Inch(0.14791637659072876));
            this.textBox6.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox6.Style.Font.Bold = false;
            this.textBox6.Style.Font.Italic = true;
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox6.StyleName = "Caption";
            this.textBox6.Value = "=Now()";
            // 
            // textBox7
            // 
            this.textBox7.CanGrow = true;
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.2000000476837158), Telerik.Reporting.Drawing.Unit.Inch(1.3378806114196777));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.3999999463558197), Telerik.Reporting.Drawing.Unit.Inch(0.350078821182251));
            this.textBox7.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.Font.Name = "Tahoma";
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(15);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.StyleName = "Caption";
            this.textBox7.Value = "=Antrian";
            // 
            // RegistrationTicket
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detail,
            this.reportHeaderSection1,
            this.pageFooterSection1});
            this.Name = "RegistrationTicket";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageSettings.PaperSize = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(14), Telerik.Reporting.Drawing.Unit.Cm(14));
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(3.9000003337860107);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.DetailSection detail;
        private ReportHeaderSection reportHeaderSection1;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox medicalNoCaptionTextBox;
        private Telerik.Reporting.TextBox medicalNoDataTextBox;
        private Telerik.Reporting.TextBox patientNameCaptionTextBox;
        private Telerik.Reporting.TextBox patientNameDataTextBox;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.CheckBox checkBox1;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox27;
        private PageFooterSection pageFooterSection1;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox7;
    }
}