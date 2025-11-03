namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.LAMARAN
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class SickLetter
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.txtHealthcareName = new Telerik.Reporting.TextBox();
            this.txtAddress1 = new Telerik.Reporting.TextBox();
            this.txtAddress2 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox64 = new Telerik.Reporting.TextBox();
            this.textBox59 = new Telerik.Reporting.TextBox();
            this.textBox58 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox51 = new Telerik.Reporting.TextBox();
            this.textBox53 = new Telerik.Reporting.TextBox();
            this.textBox55 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(0.70000004768371582);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtHealthcareName,
            this.txtAddress1,
            this.txtAddress2});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.Font.Name = "Times New Roman";
            this.pageHeader.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.5);
            // 
            // txtHealthcareName
            // 
            this.txtHealthcareName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421), Telerik.Reporting.Drawing.Unit.Inch(0.026388874277472496));
            this.txtHealthcareName.Name = "txtHealthcareName";
            this.txtHealthcareName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.5), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtHealthcareName.Style.Font.Bold = false;
            this.txtHealthcareName.Style.Font.Name = "Times New Roman";
            this.txtHealthcareName.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.txtHealthcareName.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.txtHealthcareName.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtHealthcareName.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.txtHealthcareName.Value = "";
            // 
            // txtAddress1
            // 
            this.txtAddress1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421), Telerik.Reporting.Drawing.Unit.Inch(0.22646774351596832));
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.5), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtAddress1.Style.Font.Bold = false;
            this.txtAddress1.Style.Font.Name = "Times New Roman";
            this.txtAddress1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.txtAddress1.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.txtAddress1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtAddress1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.txtAddress1.Value = "";
            // 
            // txtAddress2
            // 
            this.txtAddress2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421), Telerik.Reporting.Drawing.Unit.Inch(0.42654666304588318));
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.5), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtAddress2.Style.Font.Bold = false;
            this.txtAddress2.Style.Font.Name = "Times New Roman";
            this.txtAddress2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.txtAddress2.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.txtAddress2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtAddress2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.txtAddress2.Value = "";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(3.7000000476837158);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox5,
            this.textBox64,
            this.textBox59,
            this.textBox58,
            this.textBox4,
            this.textBox8,
            this.textBox16,
            this.textBox51,
            this.textBox53,
            this.textBox55,
            this.textBox1,
            this.textBox2,
            this.textBox6,
            this.textBox10,
            this.textBox12,
            this.textBox13,
            this.textBox3,
            this.textBox7,
            this.textBox9,
            this.textBox11,
            this.textBox14,
            this.textBox15});
            this.detail.Name = "detail";
            this.detail.Style.Font.Name = "Times New Roman";
            this.detail.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.5);
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3), Telerik.Reporting.Drawing.Unit.Inch(3.3999996185302734));
            this.textBox5.Name = "textBox9";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.2999975681304932), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox5.Style.Font.Bold = false;
            this.textBox5.Style.Font.Name = "Times New Roman";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox5.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox5.Value = "=\'( \' + ParamedicName + \' )\'";
            // 
            // textBox64
            // 
            this.textBox64.CanGrow = false;
            this.textBox64.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(1.105161190032959));
            this.textBox64.Name = "textBox64";
            this.textBox64.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.90000057220459), Telerik.Reporting.Drawing.Unit.Inch(0.39483878016471863));
            this.textBox64.Style.Font.Bold = false;
            this.textBox64.Style.Font.Name = "Times New Roman";
            this.textBox64.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox64.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox64.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox64.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox64.Value = "Pada pemeriksaan saat ini ternyata dalam keadaan SAKIT, sehingga perlu beristirah" +
                "at selama:";
            // 
            // textBox59
            // 
            this.textBox59.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.9868215517249155E-08), Telerik.Reporting.Drawing.Unit.Inch(0.90508222579956055));
            this.textBox59.Name = "textBox9";
            this.textBox59.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox59.Style.Font.Bold = false;
            this.textBox59.Style.Font.Name = "Times New Roman";
            this.textBox59.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox59.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox59.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox59.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox59.Value = "Pekerjaan";
            // 
            // textBox58
            // 
            this.textBox58.CanGrow = false;
            this.textBox58.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.80007892847061157), Telerik.Reporting.Drawing.Unit.Inch(0.70500332117080688));
            this.textBox58.Name = "textBox9";
            this.textBox58.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.5999212265014648), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox58.Style.Font.Bold = false;
            this.textBox58.Style.Font.Name = "Times New Roman";
            this.textBox58.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox58.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox58.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox58.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox58.Value = "=PatientName";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(-7.4505805969238281E-09), Telerik.Reporting.Drawing.Unit.Inch(0.10000001639127731));
            this.textBox4.Name = "TxtRSU";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.3000006675720215), Telerik.Reporting.Drawing.Unit.Inch(0.24174520373344421));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Name = "Times New Roman";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox4.Style.Font.Underline = true;
            this.textBox4.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox4.Value = "SURAT KETERANGAN";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.00015746222925372422), Telerik.Reporting.Drawing.Unit.Inch(1.5000002384185791));
            this.textBox8.Name = "textBox28";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.39999982714653015), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox8.Style.Font.Bold = false;
            this.textBox8.Style.Font.Name = "Times New Roman";
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox8.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Value = "=Days";
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.99999737739563), Telerik.Reporting.Drawing.Unit.Inch(2.5000791549682617));
            this.textBox16.Name = "textBox9";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0667060613632202), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox16.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox16.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox16.Style.Font.Bold = false;
            this.textBox16.Style.Font.Name = "Times New Roman";
            this.textBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox16.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox16.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox16.Value = "Dokter,";
            // 
            // textBox51
            // 
            this.textBox51.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.9999980926513672), Telerik.Reporting.Drawing.Unit.Inch(2.3000001907348633));
            this.textBox51.Name = "textBox9";
            this.textBox51.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.2999997138977051), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox51.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox51.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox51.Style.Font.Bold = false;
            this.textBox51.Style.Font.Name = "Times New Roman";
            this.textBox51.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox51.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox51.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox51.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox51.Value = "";
            // 
            // textBox53
            // 
            this.textBox53.CanGrow = false;
            this.textBox53.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.70254135131835938));
            this.textBox53.Name = "textBox53";
            this.textBox53.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.80000013113021851), Telerik.Reporting.Drawing.Unit.Inch(0.20246219635009766));
            this.textBox53.Style.Font.Bold = false;
            this.textBox53.Style.Font.Name = "Times New Roman";
            this.textBox53.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox53.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox53.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox53.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox53.Value = "Nama";
            // 
            // textBox55
            // 
            this.textBox55.CanGrow = false;
            this.textBox55.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.80007892847061157), Telerik.Reporting.Drawing.Unit.Inch(0.90508228540420532));
            this.textBox55.Name = "textBox9";
            this.textBox55.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.9999995231628418), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox55.Style.Font.Bold = false;
            this.textBox55.Style.Font.Name = "Times New Roman";
            this.textBox55.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox55.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox55.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox55.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox55.Value = "=Occupation";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = false;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.00015767414879519492), Telerik.Reporting.Drawing.Unit.Inch(0.5000002384185791));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.5000777244567871), Telerik.Reporting.Drawing.Unit.Inch(0.20246219635009766));
            this.textBox1.Style.Font.Bold = false;
            this.textBox1.Style.Font.Name = "Times New Roman";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox1.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "Yang bertanda tangan di bawah ini menerangkan bahwa :";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(1.7000792026519775));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox2.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox2.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox2.Style.Font.Bold = false;
            this.textBox2.Style.Font.Name = "Times New Roman";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox2.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "terhitung tanggal";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.4000000953674316), Telerik.Reporting.Drawing.Unit.Inch(1.7000792026519775));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.39992141723632812), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox6.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox6.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox6.Style.Font.Bold = false;
            this.textBox6.Style.Font.Name = "Times New Roman";
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox6.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "s.d";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.40023612976074219), Telerik.Reporting.Drawing.Unit.Inch(1.5000004768371582));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.89992111921310425), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox10.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox10.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox10.Style.Font.Bold = false;
            this.textBox10.Style.Font.Name = "Times New Roman";
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox10.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox10.Value = "hari,";
            // 
            // textBox12
            // 
            this.textBox12.Format = "{0:dd-MMM-yyyy}";
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1002360582351685), Telerik.Reporting.Drawing.Unit.Inch(1.7000792026519775));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2996844053268433), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox12.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox12.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox12.Style.Font.Bold = false;
            this.textBox12.Style.Font.Name = "Times New Roman";
            this.textBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox12.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.Value = "=StartDate";
            // 
            // textBox13
            // 
            this.textBox13.Format = "{0:dd-MMM-yyyy}";
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.7999210357666016), Telerik.Reporting.Drawing.Unit.Inch(1.7000792026519775));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2000000476837158), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox13.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox13.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox13.Style.Font.Bold = false;
            this.textBox13.Style.Font.Name = "Times New Roman";
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox13.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.Value = "=EndDate";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3002362251281738), Telerik.Reporting.Drawing.Unit.Inch(1.5000004768371582));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.9000778198242188), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox3.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox3.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox3.Style.Font.Bold = false;
            this.textBox3.Style.Font.Name = "Times New Roman";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox3.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = "";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(1.9001584053039551));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.9000003337860107), Telerik.Reporting.Drawing.Unit.Inch(0.9999995231628418));
            this.textBox7.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox7.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox7.Style.Font.Bold = false;
            this.textBox7.Style.Font.Name = "Times New Roman";
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox7.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox7.Value = "=\'Catatan : \' +Notes";
            // 
            // textBox9
            // 
            this.textBox9.CanGrow = false;
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.5), Telerik.Reporting.Drawing.Unit.Inch(0.70500344038009644));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.29992127418518066), Telerik.Reporting.Drawing.Unit.Inch(0.20000001788139343));
            this.textBox9.Style.Font.Bold = false;
            this.textBox9.Style.Font.Name = "Times New Roman";
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox9.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox9.Value = "=Sex";
            // 
            // textBox11
            // 
            this.textBox11.CanGrow = false;
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9000003337860107), Telerik.Reporting.Drawing.Unit.Inch(0.70500361919403076));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.40000027418136597), Telerik.Reporting.Drawing.Unit.Inch(0.20000001788139343));
            this.textBox11.Style.Font.Bold = false;
            this.textBox11.Style.Font.Name = "Times New Roman";
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox11.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox11.Value = "Umur";
            // 
            // textBox14
            // 
            this.textBox14.CanGrow = false;
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.300079345703125), Telerik.Reporting.Drawing.Unit.Inch(0.70254123210906982));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.29999837279319763), Telerik.Reporting.Drawing.Unit.Inch(0.20000001788139343));
            this.textBox14.Style.Font.Bold = false;
            this.textBox14.Style.Font.Name = "Times New Roman";
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox14.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox14.Value = "=age";
            // 
            // textBox15
            // 
            this.textBox15.CanGrow = false;
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.6001567840576172), Telerik.Reporting.Drawing.Unit.Inch(0.70500361919403076));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.19992107152938843), Telerik.Reporting.Drawing.Unit.Inch(0.20000001788139343));
            this.textBox15.Style.Font.Bold = false;
            this.textBox15.Style.Font.Name = "Times New Roman";
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox15.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox15.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox15.Value = "Th";
            // 
            // SickLetter
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail});
            this.Name = "SickLetter";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(3);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageSettings.PaperSize = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.4000000953674316), Telerik.Reporting.Drawing.Unit.Inch(8.3999996185302734));
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(5.3000001907348633);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox txtAddress2;
        private Telerik.Reporting.TextBox txtHealthcareName;
        private Telerik.Reporting.TextBox textBox64;
        private Telerik.Reporting.TextBox textBox59;
        private Telerik.Reporting.TextBox textBox58;
        private Telerik.Reporting.TextBox textBox55;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox txtAddress1;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox51;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox53;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox15;
    }
}