namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges.RSSMCB
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class DownPaymentReceipt
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.TxtNameRS = new Telerik.Reporting.TextBox();
            this.TxtTelp = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.txtUserName = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.txtTotalAmountInWords = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox33 = new Telerik.Reporting.TextBox();
            this.textBox42 = new Telerik.Reporting.TextBox();
            this.TxtCityRS = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(1.1999999284744263);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox9,
            this.textBox16,
            this.TxtNameRS,
            this.TxtTelp,
            this.textBox4,
            this.textBox6,
            this.textBox11});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.Font.Name = "Microsoft Sans Serif";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.7000001668930054), Telerik.Reporting.Drawing.Unit.Inch(0.99996042251586914));
            this.textBox9.Name = "TxtNameRS";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.3124599456787109), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox9.Style.Font.Bold = true;
            this.textBox9.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox9.Value = "BUKTI PEMBAYARAN DEPOSIT";
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.4000000953674316), Telerik.Reporting.Drawing.Unit.Inch(0.99996042251586914));
            this.textBox16.Name = "TxtNameRS";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0802364349365234), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox16.Style.Font.Bold = true;
            this.textBox16.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox16.Style.Visible = false;
            this.textBox16.Value = "=Status";
            // 
            // TxtNameRS
            // 
            this.TxtNameRS.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.9600016761105508E-05), Telerik.Reporting.Drawing.Unit.Inch(3.9354959881165996E-05));
            this.TxtNameRS.Name = "TxtNameRS";
            this.TxtNameRS.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.7391157150268555), Telerik.Reporting.Drawing.Unit.Inch(0.23950465023517609));
            this.TxtNameRS.Style.Font.Bold = true;
            this.TxtNameRS.Style.Font.Name = "Microsoft Sans Serif";
            this.TxtNameRS.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.TxtNameRS.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.TxtNameRS.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.TxtNameRS.Value = "";
            // 
            // TxtTelp
            // 
            this.TxtTelp.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.9600016761105508E-05), Telerik.Reporting.Drawing.Unit.Inch(0.40007871389389038));
            this.TxtTelp.Name = "TxtTelp";
            this.TxtTelp.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.7391157150268555), Telerik.Reporting.Drawing.Unit.Inch(0.1562894880771637));
            this.TxtTelp.Style.Font.Name = "Microsoft Sans Serif";
            this.TxtTelp.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.TxtTelp.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.TxtTelp.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.TxtTelp.Value = "";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.9600016761105508E-05), Telerik.Reporting.Drawing.Unit.Inch(0.24095478653907776));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.7391157150268555), Telerik.Reporting.Drawing.Unit.Inch(0.15904520452022553));
            this.textBox4.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox4.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox4.Value = "";
            // 
            // textBox6
            // 
            this.textBox6.Format = ": {0}";
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579), Telerik.Reporting.Drawing.Unit.Inch(0.55644696950912476));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.6391158103942871), Telerik.Reporting.Drawing.Unit.Inch(0.1562894880771637));
            this.textBox6.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox6.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox6.Value = "";
            // 
            // textBox11
            // 
            this.textBox11.CanGrow = false;
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.55644696950912476));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0999212265014648), Telerik.Reporting.Drawing.Unit.Inch(0.15628950297832489));
            this.textBox11.Style.Font.Bold = false;
            this.textBox11.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox11.Value = "NPWP";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(3.8478372097015381);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox8,
            this.textBox7,
            this.textBox2,
            this.textBox3,
            this.txtUserName,
            this.textBox12,
            this.textBox32,
            this.txtTotalAmountInWords,
            this.textBox10,
            this.textBox13,
            this.textBox5,
            this.textBox30,
            this.textBox22,
            this.textBox23,
            this.textBox24,
            this.textBox27,
            this.textBox29,
            this.textBox33,
            this.textBox42,
            this.TxtCityRS,
            this.textBox14,
            this.textBox1,
            this.textBox31});
            this.detail.Name = "detail";
            this.detail.Style.Font.Name = "Microsoft Sans Serif";
            // 
            // textBox8
            // 
            this.textBox8.CanGrow = false;
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5000790357589722), Telerik.Reporting.Drawing.Unit.Inch(0.7000001072883606));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.160435676574707), Telerik.Reporting.Drawing.Unit.Inch(0.19083292782306671));
            this.textBox8.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox8.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox8.Value = "=RegistrationNo +\' [ \' + MedicalNo +\']\'";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.4000791311264038), Telerik.Reporting.Drawing.Unit.Inch(1.0709913969039917));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.18000003695487976));
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox7.Value = ":";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = false;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.7000001072883606));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0999212265014648), Telerik.Reporting.Drawing.Unit.Inch(0.19083285331726074));
            this.textBox2.Style.Font.Bold = false;
            this.textBox2.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox2.Value = "No. Registrasi";
            // 
            // textBox3
            // 
            this.textBox3.Format = ", {0:dd MMM yyyy}";
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.3000001907348633), Telerik.Reporting.Drawing.Unit.Inch(1.8274604082107544));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0124368667602539), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox3.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox3.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.Value = "=PaymentDate";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.2000393867492676), Telerik.Reporting.Drawing.Unit.Inch(3.3274602890014648));
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.0998392105102539), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtUserName.Style.Font.Name = "Microsoft Sans Serif";
            this.txtUserName.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.txtUserName.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.txtUserName.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtUserName.Value = "=PrintedBy";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.2000393867492676), Telerik.Reporting.Drawing.Unit.Inch(2.02954363822937));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.0998399257659912), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox12.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox12.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox12.Value = "Kasir";
            // 
            // textBox32
            // 
            this.textBox32.CanGrow = false;
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(1.2510697841644287));
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3999607563018799), Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737));
            this.textBox32.Style.Font.Bold = false;
            this.textBox32.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox32.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox32.Value = "Jumlah (Rp)";
            // 
            // txtTotalAmountInWords
            // 
            this.txtTotalAmountInWords.CanGrow = false;
            this.txtTotalAmountInWords.Format = "# {0} #";
            this.txtTotalAmountInWords.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5000790357589722), Telerik.Reporting.Drawing.Unit.Inch(1.4311486482620239));
            this.txtTotalAmountInWords.Name = "txtTotalAmountInWords";
            this.txtTotalAmountInWords.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.980196475982666), Telerik.Reporting.Drawing.Unit.Inch(0.396232932806015));
            this.txtTotalAmountInWords.Style.Font.Name = "Microsoft Sans Serif";
            this.txtTotalAmountInWords.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.txtTotalAmountInWords.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.txtTotalAmountInWords.Value = "<TotalAmountInWords>";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.4000002145767212), Telerik.Reporting.Drawing.Unit.Inch(1.4311486482620239));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.18000003695487976));
            this.textBox10.Style.Font.Bold = true;
            this.textBox10.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox10.Value = ":";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.4000791311264038), Telerik.Reporting.Drawing.Unit.Inch(1.2510697841644287));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.18000003695487976));
            this.textBox13.Style.Font.Bold = true;
            this.textBox13.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox13.Value = ":";
            // 
            // textBox5
            // 
            this.textBox5.CanGrow = false;
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(1.4311487674713135));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4000002145767212), Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737));
            this.textBox5.Style.Font.Bold = false;
            this.textBox5.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox5.Value = "Terbilang";
            // 
            // textBox30
            // 
            this.textBox30.CanGrow = false;
            this.textBox30.Format = "{0:N0}";
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5000790357589722), Telerik.Reporting.Drawing.Unit.Inch(1.2510697841644287));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.699920654296875), Telerik.Reporting.Drawing.Unit.Inch(0.18000014126300812));
            this.textBox30.Style.Font.Bold = true;
            this.textBox30.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox30.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox30.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox30.Value = "=Amount";
            // 
            // textBox22
            // 
            this.textBox22.CanGrow = false;
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.4000002145767212), Telerik.Reporting.Drawing.Unit.Inch(0.89091199636459351));
            this.textBox22.Name = "textBox2";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099999748170375824), Telerik.Reporting.Drawing.Unit.Inch(0.18000014126300812));
            this.textBox22.Style.Font.Bold = true;
            this.textBox22.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox22.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox22.Value = ":";
            // 
            // textBox23
            // 
            this.textBox23.CanGrow = false;
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.8837074397597462E-05), Telerik.Reporting.Drawing.Unit.Inch(0.89091199636459351));
            this.textBox23.Name = "textBox3";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.399921178817749), Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737));
            this.textBox23.Style.Font.Bold = false;
            this.textBox23.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox23.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox23.Value = "Nama";
            // 
            // textBox24
            // 
            this.textBox24.CanGrow = false;
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.9600016761105508E-05), Telerik.Reporting.Drawing.Unit.Inch(1.0709911584854126));
            this.textBox24.Name = "textBox15";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3999203443527222), Telerik.Reporting.Drawing.Unit.Inch(0.17999982833862305));
            this.textBox24.Style.Font.Bold = false;
            this.textBox24.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox24.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox24.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox24.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox24.Value = "Penanggung";
            // 
            // textBox27
            // 
            this.textBox27.CanGrow = false;
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5000002384185791), Telerik.Reporting.Drawing.Unit.Inch(0.89091199636459351));
            this.textBox27.Name = "textBox22";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.6999993324279785), Telerik.Reporting.Drawing.Unit.Inch(0.18000046908855438));
            this.textBox27.Style.Font.Bold = true;
            this.textBox27.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox27.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox27.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox27.Value = "=PatientName";
            // 
            // textBox29
            // 
            this.textBox29.CanGrow = false;
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5000790357589722), Telerik.Reporting.Drawing.Unit.Inch(1.0709911584854126));
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.6999201774597168), Telerik.Reporting.Drawing.Unit.Inch(0.17999982833862305));
            this.textBox29.Style.Font.Bold = true;
            this.textBox29.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox29.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox29.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox29.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox29.Value = "=Company";
            // 
            // textBox33
            // 
            this.textBox33.CanGrow = false;
            this.textBox33.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.4000002145767212), Telerik.Reporting.Drawing.Unit.Inch(0.7000001072883606));
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099999748170375824), Telerik.Reporting.Drawing.Unit.Inch(0.19083300232887268));
            this.textBox33.Style.Font.Bold = true;
            this.textBox33.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox33.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox33.Value = ":";
            // 
            // textBox42
            // 
            this.textBox42.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(3.3979160785675049));
            this.textBox42.Name = "textBox10";
            this.textBox42.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.0999612808227539), Telerik.Reporting.Drawing.Unit.Inch(0.44992128014564514));
            this.textBox42.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox42.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox42.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox42.Value = "Ket: Tanda bukti Penerimaan uang deposit perawatan ini harus dibawa ketika pasien" +
                " pulang (keluar dari rumah sakit)";
            // 
            // TxtCityRS
            // 
            this.TxtCityRS.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.2000393867492676), Telerik.Reporting.Drawing.Unit.Inch(1.8295441865921021));
            this.TxtCityRS.Name = "TxtCityRS";
            this.TxtCityRS.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0999608039855957), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.TxtCityRS.Style.Font.Name = "Microsoft Sans Serif";
            this.TxtCityRS.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.TxtCityRS.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5000790357589722), Telerik.Reporting.Drawing.Unit.Inch(0.51992154121398926));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.9999213218688965), Telerik.Reporting.Drawing.Unit.Inch(0.18000003695487976));
            this.textBox14.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox14.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox14.Value = "=PaymentNo";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = false;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.8837074397597462E-05), Telerik.Reporting.Drawing.Unit.Inch(0.51992154121398926));
            this.textBox1.Name = "textBox32";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.399921178817749), Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737));
            this.textBox1.Style.Font.Bold = false;
            this.textBox1.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox1.Value = "Nomor";
            // 
            // textBox31
            // 
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.4000790119171143), Telerik.Reporting.Drawing.Unit.Inch(0.51992154121398926));
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.18000003695487976));
            this.textBox31.Style.Font.Bold = true;
            this.textBox31.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox31.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox31.Value = ":";
            // 
            // DownPaymentReceipt
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail});
            this.Name = "DownPaymentReceipt";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Cm(0);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Name = "Microsoft Sans Serif";
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.7125587463378906);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox txtUserName;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox txtTotalAmountInWords;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox33;
        private Telerik.Reporting.TextBox textBox42;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox TxtCityRS;
        private Telerik.Reporting.TextBox TxtNameRS;
        private Telerik.Reporting.TextBox TxtTelp;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox11;
    }
}