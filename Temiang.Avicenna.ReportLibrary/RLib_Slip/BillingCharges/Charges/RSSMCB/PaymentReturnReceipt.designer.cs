namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges.RSSMCB
    
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class PaymentReturnReceipt
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.txtTotalAmountInWords = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.TxtAmount = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.TxtUserName = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.TxtCityRS = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.TxtTelp = new Telerik.Reporting.TextBox();
            this.TxtNameRS = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(3.7999608516693115);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox14,
            this.textBox13,
            this.textBox12,
            this.textBox26,
            this.textBox27,
            this.textBox21,
            this.txtTotalAmountInWords,
            this.textBox10,
            this.textBox6,
            this.textBox4,
            this.textBox3,
            this.textBox9,
            this.textBox15,
            this.textBox30,
            this.textBox32,
            this.TxtAmount,
            this.textBox11,
            this.textBox19,
            this.TxtUserName,
            this.textBox22,
            this.textBox23,
            this.TxtCityRS,
            this.textBox2,
            this.textBox5,
            this.textBox7,
            this.textBox8,
            this.TxtTelp,
            this.TxtNameRS});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.Font.Name = "Microsoft Sans Serif";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.5), Telerik.Reporting.Drawing.Unit.Inch(1));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.4801950454711914), Telerik.Reporting.Drawing.Unit.Inch(0.28958341479301453));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14);
            this.textBox1.Style.Font.Underline = true;
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "=Title";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.8888797760009766), Telerik.Reporting.Drawing.Unit.Inch(1.28966224193573));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.200000524520874), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox14.Style.Font.Name = "Tahoma";
            this.textBox14.Value = "=\'No. Bukti : \' +PaymentNo";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.4083347320556641), Telerik.Reporting.Drawing.Unit.Inch(1.5));
            this.textBox13.Name = "textBox2";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.091586321592330933), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox13.Style.Font.Bold = true;
            this.textBox13.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox13.Value = ":";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(1.4977985620498657));
            this.textBox12.Name = "textBox3";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3957538604736328), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox12.Style.Font.Bold = false;
            this.textBox12.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox12.Value = "Nama Pasien";
            // 
            // textBox26
            // 
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(2.0930557250976562));
            this.textBox26.Name = "textBox20";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3999210596084595), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox26.Style.Font.Bold = false;
            this.textBox26.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox26.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox26.Value = "Uang Sebesar";
            // 
            // textBox27
            // 
            this.textBox27.CanGrow = false;
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5083346366882324), Telerik.Reporting.Drawing.Unit.Inch(1.5));
            this.textBox27.Name = "textBox22";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.6041667461395264), Telerik.Reporting.Drawing.Unit.Inch(0.19779862463474274));
            this.textBox27.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox27.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox27.Value = "=PatientName";
            // 
            // textBox21
            // 
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5020847320556641), Telerik.Reporting.Drawing.Unit.Inch(1.8971443176269531));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.7554459571838379), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox21.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox21.Value = "=Notes";
            // 
            // txtTotalAmountInWords
            // 
            this.txtTotalAmountInWords.CanGrow = false;
            this.txtTotalAmountInWords.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5041681528091431), Telerik.Reporting.Drawing.Unit.Inch(2.3903806209564209));
            this.txtTotalAmountInWords.Name = "txtTotalAmountInWords";
            this.txtTotalAmountInWords.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.2957534790039062), Telerik.Reporting.Drawing.Unit.Inch(0.40000021457672119));
            this.txtTotalAmountInWords.Style.Font.Name = "Tahoma";
            this.txtTotalAmountInWords.Style.Font.Strikeout = false;
            this.txtTotalAmountInWords.Value = "=TotalAmountInWords";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(1.8929767608642578));
            this.textBox10.Name = "textBox32";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3957544565200806), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox10.Style.Font.Bold = false;
            this.textBox10.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox10.Value = "Keterangan";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(2.3903806209564209));
            this.textBox6.Name = "textBox32";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox6.Style.Font.Bold = false;
            this.textBox6.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox6.Value = "Terbilang";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.4083347320556641), Telerik.Reporting.Drawing.Unit.Inch(1.8949416875839233));
            this.textBox4.Name = "textBox31";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.093671165406703949), Telerik.Reporting.Drawing.Unit.Inch(0.20220279693603516));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox4.Value = ":";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(1.2977195978164673));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3979164361953735), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox3.Style.Font.Bold = false;
            this.textBox3.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox3.Value = "No. Registrasi";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421), Telerik.Reporting.Drawing.Unit.Inch(1.2999211549758911));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox9.Style.Font.Bold = true;
            this.textBox9.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox9.Value = ":";
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5), Telerik.Reporting.Drawing.Unit.Inch(1.2977195978164673));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.200000524520874), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox15.Style.Font.Bold = false;
            this.textBox15.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox15.Value = "=RegistrationNo";
            // 
            // textBox30
            // 
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5083346366882324), Telerik.Reporting.Drawing.Unit.Inch(1.7000788450241089));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.4958324432373047), Telerik.Reporting.Drawing.Unit.Inch(0.192021906375885));
            this.textBox30.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox30.Value = "";
            // 
            // textBox32
            // 
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(1.7000788450241089));
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421), Telerik.Reporting.Drawing.Unit.Inch(0.192021906375885));
            this.textBox32.Style.Font.Bold = false;
            this.textBox32.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox32.Value = "Sudah Terima Dari";
            // 
            // TxtAmount
            // 
            this.TxtAmount.CanGrow = false;
            this.TxtAmount.Format = "{0:N2}";
            this.TxtAmount.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5020847320556641), Telerik.Reporting.Drawing.Unit.Inch(2.0972232818603516));
            this.TxtAmount.Name = "TxtAmount";
            this.TxtAmount.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.7979154586791992), Telerik.Reporting.Drawing.Unit.Inch(0.19996023178100586));
            this.TxtAmount.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.TxtAmount.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.TxtAmount.Style.Font.Bold = true;
            this.TxtAmount.Style.Font.Name = "Microsoft Sans Serif";
            this.TxtAmount.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.TxtAmount.Style.Font.Underline = true;
            this.TxtAmount.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(5);
            this.TxtAmount.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.TxtAmount.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.TxtAmount.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.TxtAmount.Value = "=Amount";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.4083347320556641), Telerik.Reporting.Drawing.Unit.Inch(1.7000788450241089));
            this.textBox11.Name = "textBox9";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.091586537659168243), Telerik.Reporting.Drawing.Unit.Inch(0.192021906375885));
            this.textBox11.Style.Font.Bold = true;
            this.textBox11.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox11.Value = ":";
            // 
            // textBox19
            // 
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.4083347320556641), Telerik.Reporting.Drawing.Unit.Inch(2.3903806209564209));
            this.textBox19.Name = "textBox9";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.095754623413085938), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox19.Style.Font.Bold = true;
            this.textBox19.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox19.Value = ":";
            // 
            // TxtUserName
            // 
            this.TxtUserName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.0575308799743652), Telerik.Reporting.Drawing.Unit.Inch(3.5999605655670166));
            this.TxtUserName.Name = "TxtUserName";
            this.TxtUserName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.0313489437103271), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.TxtUserName.Style.Font.Name = "Microsoft Sans Serif";
            this.TxtUserName.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtUserName.Value = "=PrintReceiptAsName";
            // 
            // textBox22
            // 
            this.textBox22.Format = ", {0:dd-MMM-yyyy}";
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.0575308799743652), Telerik.Reporting.Drawing.Unit.Inch(2.7897412776947021));
            this.textBox22.Name = "TxtUserName";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2000002861022949), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox22.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox22.Value = "=PaymentDate";
            // 
            // textBox23
            // 
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421), Telerik.Reporting.Drawing.Unit.Inch(2.0971834659576416));
            this.textBox23.Name = "textBox9";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.09992130845785141), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox23.Style.Font.Bold = true;
            this.textBox23.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox23.Value = ":";
            // 
            // TxtCityRS
            // 
            this.TxtCityRS.Format = "{0:dd-MMM-yyyy}";
            this.TxtCityRS.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.8575310707092285), Telerik.Reporting.Drawing.Unit.Inch(2.7897412776947021));
            this.TxtCityRS.Name = "TxtCityRS";
            this.TxtCityRS.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.142390251159668), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.TxtCityRS.Style.Font.Name = "Microsoft Sans Serif";
            this.TxtCityRS.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.TxtCityRS.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            // 
            // textBox2
            // 
            this.textBox2.Format = "";
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(2.8999998569488525));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.142390251159668), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox2.Style.Font.Italic = true;
            this.textBox2.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox2.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.Value = "=Initial";
            // 
            // textBox5
            // 
            this.textBox5.CanGrow = false;
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.55507576465606689));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0999212265014648), Telerik.Reporting.Drawing.Unit.Inch(0.15628950297832489));
            this.textBox5.Style.Font.Bold = false;
            this.textBox5.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox5.Value = "NPWP";
            // 
            // textBox7
            // 
            this.textBox7.Format = ": {0}";
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000396013259888), Telerik.Reporting.Drawing.Unit.Inch(0.55507576465606689));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.6391158103942871), Telerik.Reporting.Drawing.Unit.Inch(0.1562894880771637));
            this.textBox7.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox7.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox7.Value = "";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.23958338797092438));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.7391157150268555), Telerik.Reporting.Drawing.Unit.Inch(0.15904520452022553));
            this.textBox8.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox8.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox8.Value = "";
            // 
            // TxtTelp
            // 
            this.TxtTelp.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.39870738983154297));
            this.TxtTelp.Name = "TxtTelp";
            this.TxtTelp.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.7391157150268555), Telerik.Reporting.Drawing.Unit.Inch(0.1562894880771637));
            this.TxtTelp.Style.Font.Name = "Microsoft Sans Serif";
            this.TxtTelp.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.TxtTelp.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.TxtTelp.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.TxtTelp.Value = "";
            // 
            // TxtNameRS
            // 
            this.TxtNameRS.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.TxtNameRS.Name = "TxtNameRS";
            this.TxtNameRS.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.7391157150268555), Telerik.Reporting.Drawing.Unit.Inch(0.23950465023517609));
            this.TxtNameRS.Style.Font.Bold = true;
            this.TxtNameRS.Style.Font.Name = "Microsoft Sans Serif";
            this.TxtNameRS.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.TxtNameRS.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.TxtNameRS.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.TxtNameRS.Value = "";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699);
            this.detail.Name = "detail";
            // 
            // PaymentReturnReceipt
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail});
            this.Name = "PaymentReturnReceipt";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Cm(0.699999988079071);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Cm(0.5);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Cm(0);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(2.2999999523162842);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageSettings.PaperSize = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(8.5), Telerik.Reporting.Drawing.Unit.Inch(5.5));
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(8.3031501770019531);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox txtTotalAmountInWords;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.TextBox TxtAmount;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox TxtUserName;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox TxtCityRS;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox TxtTelp;
        private Telerik.Reporting.TextBox TxtNameRS;
    }
}