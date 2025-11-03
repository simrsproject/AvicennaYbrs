namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.RSBHP
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
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox59 = new Telerik.Reporting.TextBox();
            this.textBox58 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox51 = new Telerik.Reporting.TextBox();
            this.textBox53 = new Telerik.Reporting.TextBox();
            this.textBox54 = new Telerik.Reporting.TextBox();
            this.textBox55 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(0.70000004768371582);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtHealthcareName,
            this.txtAddress1,
            this.txtAddress2,
            this.textBox9});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.Font.Name = "Microsoft Sans Serif";
            this.pageHeader.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.5);
            // 
            // txtHealthcareName
            // 
            this.txtHealthcareName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.100000262260437), Telerik.Reporting.Drawing.Unit.Inch(3.9378803194267675E-05));
            this.txtHealthcareName.Name = "txtHealthcareName";
            this.txtHealthcareName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.3998823165893555), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtHealthcareName.Style.Font.Bold = true;
            this.txtHealthcareName.Style.Font.Name = "Tahoma";
            this.txtHealthcareName.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.txtHealthcareName.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.txtHealthcareName.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtHealthcareName.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.txtHealthcareName.Value = "";
            // 
            // txtAddress1
            // 
            this.txtAddress1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.100000262260437), Telerik.Reporting.Drawing.Unit.Inch(0.20011825859546661));
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.3998823165893555), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.txtAddress1.Style.Font.Bold = false;
            this.txtAddress1.Style.Font.Name = "Tahoma";
            this.txtAddress1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.txtAddress1.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.txtAddress1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtAddress1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.txtAddress1.Value = "";
            // 
            // txtAddress2
            // 
            this.txtAddress2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.100000262260437), Telerik.Reporting.Drawing.Unit.Inch(0.3501971960067749));
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.3998823165893555), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.txtAddress2.Style.Font.Bold = false;
            this.txtAddress2.Style.Font.Name = "Tahoma";
            this.txtAddress2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.txtAddress2.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.txtAddress2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtAddress2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.txtAddress2.Value = "";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.100000262260437), Telerik.Reporting.Drawing.Unit.Inch(0.50027596950531006));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.3998823165893555), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.textBox9.Style.Font.Bold = false;
            this.textBox9.Style.Font.Name = "Tahoma";
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox9.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox9.Value = "";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(2.8999998569488525);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox5,
            this.textBox59,
            this.textBox58,
            this.textBox4,
            this.textBox16,
            this.textBox51,
            this.textBox53,
            this.textBox54,
            this.textBox55,
            this.textBox1,
            this.textBox2,
            this.textBox12,
            this.textBox3,
            this.textBox7,
            this.textBox11,
            this.textBox14,
            this.textBox15,
            this.textBox17,
            this.textBox8});
            this.detail.Name = "detail";
            this.detail.Style.Font.Name = "Microsoft Sans Serif";
            this.detail.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.5);
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.7999999523162842), Telerik.Reporting.Drawing.Unit.Inch(2.5969061851501465));
            this.textBox5.Name = "textBox9";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.2999975681304932), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox5.Style.Font.Bold = false;
            this.textBox5.Style.Font.Name = "Tahoma";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox5.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox5.Value = "=\'( \' + ParamedicName + \' )\'";
            // 
            // textBox59
            // 
            this.textBox59.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.100000262260437), Telerik.Reporting.Drawing.Unit.Inch(1.5077023506164551));
            this.textBox59.Name = "textBox9";
            this.textBox59.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.69999945163726807), Telerik.Reporting.Drawing.Unit.Inch(0.19444401562213898));
            this.textBox59.Style.Font.Bold = false;
            this.textBox59.Style.Font.Name = "Tahoma";
            this.textBox59.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox59.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox59.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox59.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox59.Value = "Pekerjaan";
            // 
            // textBox58
            // 
            this.textBox58.CanGrow = false;
            this.textBox58.Format = ": {0}";
            this.textBox58.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.7999998331069946), Telerik.Reporting.Drawing.Unit.Inch(1.1075443029403687));
            this.textBox58.Name = "textBox9";
            this.textBox58.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.9000003337860107), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox58.Style.Font.Bold = false;
            this.textBox58.Style.Font.Name = "Tahoma";
            this.textBox58.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox58.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox58.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox58.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox58.Value = "=PatientName";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.9998823404312134), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox4.Name = "TxtRSU";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.5), Telerik.Reporting.Drawing.Unit.Inch(0.24174520373344421));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Name = "Tahoma";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox4.Style.Font.Underline = false;
            this.textBox4.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox4.Value = "SURAT KETERANGAN";
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.0000791549682617), Telerik.Reporting.Drawing.Unit.Inch(2.1023833751678467));
            this.textBox16.Name = "textBox9";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.8999999761581421), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox16.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox16.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox16.Style.Font.Bold = false;
            this.textBox16.Style.Font.Name = "Tahoma";
            this.textBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox16.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox16.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox16.Value = "Dokter yang memeriksa";
            // 
            // textBox51
            // 
            this.textBox51.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.4999613761901855), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox51.Name = "textBox9";
            this.textBox51.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7000387907028198), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox51.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox51.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox51.Style.Font.Bold = false;
            this.textBox51.Style.Font.Name = "Tahoma";
            this.textBox51.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox51.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox51.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox51.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox51.Value = "";
            // 
            // textBox53
            // 
            this.textBox53.CanGrow = false;
            this.textBox53.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.3999999463558197), Telerik.Reporting.Drawing.Unit.Inch(0.90508192777633667));
            this.textBox53.Name = "textBox53";
            this.textBox53.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5000002384185791), Telerik.Reporting.Drawing.Unit.Inch(0.20246219635009766));
            this.textBox53.Style.Font.Bold = false;
            this.textBox53.Style.Font.Name = "Tahoma";
            this.textBox53.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox53.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox53.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox53.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox53.Value = "menerangkan,  bahwa :";
            // 
            // textBox54
            // 
            this.textBox54.CanGrow = false;
            this.textBox54.Format = ": {0}";
            this.textBox54.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8000785112380981), Telerik.Reporting.Drawing.Unit.Inch(0.705003023147583));
            this.textBox54.Name = "textBox54";
            this.textBox54.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.8999223709106445), Telerik.Reporting.Drawing.Unit.Inch(0.20000022649765015));
            this.textBox54.Style.Font.Bold = false;
            this.textBox54.Style.Font.Name = "Tahoma";
            this.textBox54.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox54.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox54.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox54.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox54.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox54.Value = "=ParamedicName";
            // 
            // textBox55
            // 
            this.textBox55.CanGrow = false;
            this.textBox55.Format = ": {0}";
            this.textBox55.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8000787496566773), Telerik.Reporting.Drawing.Unit.Inch(1.5021462440490723));
            this.textBox55.Name = "textBox9";
            this.textBox55.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.8999223709106445), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox55.Style.Font.Bold = false;
            this.textBox55.Style.Font.Name = "Tahoma";
            this.textBox55.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox55.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox55.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox55.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox55.Value = "=Occupation";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = false;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.3999999463558197), Telerik.Reporting.Drawing.Unit.Inch(0.50000005960464478));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.6000003814697266), Telerik.Reporting.Drawing.Unit.Inch(0.20246219635009766));
            this.textBox1.Style.Font.Bold = false;
            this.textBox1.Style.Font.Name = "Tahoma";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox1.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "Yang bertanda tangan di bawah ini, ";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.40015745162963867), Telerik.Reporting.Drawing.Unit.Inch(1.9023045301437378));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox2.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox2.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox2.Style.Font.Bold = false;
            this.textBox2.Style.Font.Name = "Tahoma";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox2.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "mulai tanggal";
            // 
            // textBox12
            // 
            this.textBox12.Format = "{0:dd-MMM-yyyy}";
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.4002362489700317), Telerik.Reporting.Drawing.Unit.Inch(1.9023045301437378));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.2997636795043945), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox12.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox12.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox12.Style.Font.Bold = false;
            this.textBox12.Style.Font.Name = "Tahoma";
            this.textBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox12.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.Value = "=Format(\'{0:dd-MMM-yyyy}\',StartDate) + \' s/d tanggal \' + Format(\'{0:dd-MMM-yyyy}\'" +
                ", EndDate)";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.39999961853027344), Telerik.Reporting.Drawing.Unit.Inch(1.7022252082824707));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.3000006675720215), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox3.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox3.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox3.Style.Font.Bold = false;
            this.textBox3.Style.Font.Name = "Tahoma";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox3.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = "";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.39999976754188538), Telerik.Reporting.Drawing.Unit.Inch(2.1023833751678467));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.9000003337860107), Telerik.Reporting.Drawing.Unit.Inch(0.69452279806137085));
            this.textBox7.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox7.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox7.Style.Font.Bold = false;
            this.textBox7.Style.Font.Name = "Tahoma";
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox7.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox7.Value = "=\'Catatan : \' +Notes";
            // 
            // textBox11
            // 
            this.textBox11.CanGrow = false;
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579), Telerik.Reporting.Drawing.Unit.Inch(0.70254111289978027));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.69999986886978149), Telerik.Reporting.Drawing.Unit.Inch(0.20246219635009766));
            this.textBox11.Style.Font.Bold = false;
            this.textBox11.Style.Font.Name = "Tahoma";
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox11.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox11.Value = "Dokter";
            // 
            // textBox14
            // 
            this.textBox14.CanGrow = false;
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579), Telerik.Reporting.Drawing.Unit.Inch(1.1076229810714722));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.69999986886978149), Telerik.Reporting.Drawing.Unit.Inch(0.19992139935493469));
            this.textBox14.Style.Font.Bold = false;
            this.textBox14.Style.Font.Name = "Tahoma";
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox14.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox14.Value = "N a m a";
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579), Telerik.Reporting.Drawing.Unit.Inch(1.3076233863830566));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.69999945163726807), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox15.Style.Font.Bold = false;
            this.textBox15.Style.Font.Name = "Tahoma";
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox15.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox15.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox15.Value = "U m u r";
            // 
            // textBox17
            // 
            this.textBox17.Format = ": {0}";
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.7999998331069946), Telerik.Reporting.Drawing.Unit.Inch(1.3076233863830566));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.5000002384185791), Telerik.Reporting.Drawing.Unit.Inch(0.19444401562213898));
            this.textBox17.Style.Font.Bold = false;
            this.textBox17.Style.Font.Name = "Tahoma";
            this.textBox17.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox17.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox17.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox17.Value = "=Umur";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.3000788688659668), Telerik.Reporting.Drawing.Unit.Inch(1.3131794929504395));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4000005722045898), Telerik.Reporting.Drawing.Unit.Inch(0.18888790905475617));
            this.textBox8.Style.Font.Bold = false;
            this.textBox8.Style.Font.Name = "Tahoma";
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox8.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Value = "=JK";
            // 
            // SickLetter
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail});
            this.Name = "SickLetter";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Cm(0);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Cm(3);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageSettings.PaperSize = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(21.5), Telerik.Reporting.Drawing.Unit.Cm(11));
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(6.8000006675720215);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox txtAddress2;
        private Telerik.Reporting.TextBox txtHealthcareName;
        private Telerik.Reporting.TextBox textBox59;
        private Telerik.Reporting.TextBox textBox58;
        private Telerik.Reporting.TextBox textBox55;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox txtAddress1;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox51;
        private Telerik.Reporting.TextBox textBox54;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox53;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox8;
    }
}