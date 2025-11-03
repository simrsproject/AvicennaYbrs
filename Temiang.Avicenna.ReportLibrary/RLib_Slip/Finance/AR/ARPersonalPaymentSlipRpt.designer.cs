namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Finance.AR
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class ARPersonalPaymentSlipRpt
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.TextBox TxtAlamat1;
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox43 = new Telerik.Reporting.TextBox();
            this.textBox44 = new Telerik.Reporting.TextBox();
            this.textBox45 = new Telerik.Reporting.TextBox();
            this.textBox46 = new Telerik.Reporting.TextBox();
            this.textBox47 = new Telerik.Reporting.TextBox();
            this.textBox48 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.txtUserName = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.txtTotalAmountInWords = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            TxtAlamat1 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TxtAlamat1
            // 
            TxtAlamat1.CanGrow = false;
            TxtAlamat1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2000002861022949), Telerik.Reporting.Drawing.Unit.Inch(1.5000003576278687));
            TxtAlamat1.Name = "TxtAlamat1";
            TxtAlamat1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.1677167415618896), Telerik.Reporting.Drawing.Unit.Inch(0.18000014126300812));
            TxtAlamat1.Style.Font.Italic = true;
            TxtAlamat1.Style.Font.Name = "Microsoft Sans Serif";
            TxtAlamat1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            TxtAlamat1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            TxtAlamat1.Value = "=StreetName";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(1.8000000715255737);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox9,
            this.textBox16,
            this.textBox43,
            this.textBox44,
            this.textBox45,
            this.textBox46,
            this.textBox47,
            this.textBox48});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Double;
            this.pageHeader.Style.Font.Name = "Microsoft Sans Serif";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.012500126846134663), Telerik.Reporting.Drawing.Unit.Inch(0.90000009536743164));
            this.textBox9.Name = "TxtNameRS";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.467735767364502), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox9.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox9.Style.Font.Bold = true;
            this.textBox9.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox9.Style.Font.Underline = true;
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox9.Value = "Kwitansi Pembayaran";
            // 
            // textBox16
            // 
            this.textBox16.CanGrow = false;
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.012500126846134663), Telerik.Reporting.Drawing.Unit.Inch(1.2000789642333984));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.4677352905273438), Telerik.Reporting.Drawing.Unit.Inch(0.18000014126300812));
            this.textBox16.Style.Font.Bold = true;
            this.textBox16.Style.Font.Italic = true;
            this.textBox16.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox16.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox16.Value = "=InvoiceNo";
            // 
            // textBox43
            // 
            this.textBox43.CanGrow = false;
            this.textBox43.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.70007866621017456), Telerik.Reporting.Drawing.Unit.Inch(1.4704562425613403));
            this.textBox43.Name = "textBox43";
            this.textBox43.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5), Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737));
            this.textBox43.Style.Font.Bold = true;
            this.textBox43.Style.Font.Italic = true;
            this.textBox43.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox43.Value = "=InvoiceReferenceNo";
            // 
            // textBox44
            // 
            this.textBox44.CanGrow = false;
            this.textBox44.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(1.4704560041427612));
            this.textBox44.Name = "textBox44";
            this.textBox44.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.699999988079071), Telerik.Reporting.Drawing.Unit.Inch(0.18000046908855438));
            this.textBox44.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox44.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox44.Value = "No Invoice:";
            // 
            // textBox45
            // 
            this.textBox45.CanGrow = false;
            this.textBox45.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.200000524520874), Telerik.Reporting.Drawing.Unit.Inch(1.4704557657241821));
            this.textBox45.Name = "textBox45";
            this.textBox45.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099920749664306641), Telerik.Reporting.Drawing.Unit.Inch(0.18000046908855438));
            this.textBox45.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox45.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox45.Value = "-";
            // 
            // textBox46
            // 
            this.textBox46.CanGrow = false;
            this.textBox46.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.3000001907348633), Telerik.Reporting.Drawing.Unit.Inch(1.4704564809799194));
            this.textBox46.Name = "textBox46";
            this.textBox46.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.4999208450317383), Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737));
            this.textBox46.Style.Font.Bold = true;
            this.textBox46.Style.Font.Italic = true;
            this.textBox46.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox46.Value = "=GuarantorName";
            // 
            // textBox47
            // 
            this.textBox47.CanGrow = false;
            this.textBox47.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.7999997138977051), Telerik.Reporting.Drawing.Unit.Inch(1.4704557657241821));
            this.textBox47.Name = "textBox47";
            this.textBox47.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.187402606010437), Telerik.Reporting.Drawing.Unit.Inch(0.18000046908855438));
            this.textBox47.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox47.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox47.Value = "Tgl. Pembayaran:";
            // 
            // textBox48
            // 
            this.textBox48.CanGrow = false;
            this.textBox48.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.9874815940856934), Telerik.Reporting.Drawing.Unit.Inch(1.4704557657241821));
            this.textBox48.Name = "textBox48";
            this.textBox48.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5), Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737));
            this.textBox48.Style.Font.Bold = true;
            this.textBox48.Style.Font.Italic = true;
            this.textBox48.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox48.Value = "=PaymentDate";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(3.2290108203887939);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox8,
            this.textBox7,
            this.textBox2,
            this.txtUserName,
            this.textBox12,
            this.textBox6,
            this.textBox32,
            this.textBox4,
            this.txtTotalAmountInWords,
            this.textBox10,
            this.textBox13,
            this.textBox5,
            this.textBox30,
            this.textBox11,
            this.textBox15,
            this.textBox18,
            this.textBox21,
            TxtAlamat1,
            this.textBox22,
            this.textBox23,
            this.textBox25,
            this.textBox26,
            this.textBox27,
            this.textBox1});
            this.detail.Name = "detail";
            this.detail.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.detail.Style.Font.Name = "Microsoft Sans Serif";
            // 
            // textBox8
            // 
            this.textBox8.CanGrow = false;
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2000002861022949), Telerik.Reporting.Drawing.Unit.Inch(1.1181768178939819));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.160435676574707), Telerik.Reporting.Drawing.Unit.Inch(0.19083292782306671));
            this.textBox8.Style.Font.Italic = true;
            this.textBox8.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox8.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox8.Value = "=RegistrationNo";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.4000791311264038), Telerik.Reporting.Drawing.Unit.Inch(0.099466443061828613));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.18000003695487976));
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox7.Value = ":";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = false;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(1.1290098428726196));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0999212265014648), Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox2.Value = "No. Registrasi";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.0125980377197266), Telerik.Reporting.Drawing.Unit.Inch(3.0290107727050781));
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.0998392105102539), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtUserName.Style.Font.Name = "Microsoft Sans Serif";
            this.txtUserName.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.txtUserName.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtUserName.Value = "=UserName";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.0125980377197266), Telerik.Reporting.Drawing.Unit.Inch(2.0183334350585938));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.0998399257659912), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox12.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox12.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox12.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox12.Value = "Petugas";
            // 
            // textBox6
            // 
            this.textBox6.Format = "Rp. {0:N0}";
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609), Telerik.Reporting.Drawing.Unit.Inch(1.9415105581283569));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.787578821182251), Telerik.Reporting.Drawing.Unit.Inch(0.38749948143959045));
            this.textBox6.Style.Font.Bold = true;
            this.textBox6.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(16);
            this.textBox6.Value = "=PaymentAmount-OtherAmount";
            // 
            // textBox32
            // 
            this.textBox32.CanGrow = false;
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.099466443061828613));
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3999607563018799), Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737));
            this.textBox32.Style.Font.Bold = true;
            this.textBox32.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox32.Value = "Telah Terima Dari";
            // 
            // textBox4
            // 
            this.textBox4.CanGrow = false;
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.6785542368888855));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3999607563018799), Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox4.Value = "Keterangan";
            // 
            // txtTotalAmountInWords
            // 
            this.txtTotalAmountInWords.CanGrow = false;
            this.txtTotalAmountInWords.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5000790357589722), Telerik.Reporting.Drawing.Unit.Inch(0.27954530715942383));
            this.txtTotalAmountInWords.Name = "txtTotalAmountInWords";
            this.txtTotalAmountInWords.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.980196475982666), Telerik.Reporting.Drawing.Unit.Inch(0.17999999225139618));
            this.txtTotalAmountInWords.Style.Font.Italic = true;
            this.txtTotalAmountInWords.Style.Font.Name = "Microsoft Sans Serif";
            this.txtTotalAmountInWords.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.txtTotalAmountInWords.Value = "=TotalAmountInWords";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.4000002145767212), Telerik.Reporting.Drawing.Unit.Inch(0.45962429046630859));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.18000003695487976));
            this.textBox10.Style.Font.Bold = true;
            this.textBox10.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox10.Value = ":";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.4000791311264038), Telerik.Reporting.Drawing.Unit.Inch(0.27954533696174622));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.18000003695487976));
            this.textBox13.Style.Font.Bold = true;
            this.textBox13.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox13.Value = ":";
            // 
            // textBox5
            // 
            this.textBox5.CanGrow = false;
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.27954533696174622));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4000002145767212), Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737));
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox5.Value = "Uang Sejumlah";
            // 
            // textBox30
            // 
            this.textBox30.CanGrow = false;
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5000790357589722), Telerik.Reporting.Drawing.Unit.Inch(0.099466323852539062));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.699920654296875), Telerik.Reporting.Drawing.Unit.Inch(0.18000014126300812));
            this.textBox30.Style.Font.Italic = true;
            this.textBox30.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox30.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox30.Value = "=PrintReceiptAsName";
            // 
            // textBox11
            // 
            this.textBox11.CanGrow = false;
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5000004768371582), Telerik.Reporting.Drawing.Unit.Inch(0.67855405807495117));
            this.textBox11.Name = "textBox4";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.9803142547607422), Telerik.Reporting.Drawing.Unit.Inch(0.43954357504844666));
            this.textBox11.Style.Font.Italic = true;
            this.textBox11.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox11.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox11.Value = "=InvoiceNotes";
            // 
            // textBox15
            // 
            this.textBox15.CanGrow = false;
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.8837074397597462E-05), Telerik.Reporting.Drawing.Unit.Inch(0.47855439782142639));
            this.textBox15.Name = "textBox4";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3999214172363281), Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737));
            this.textBox15.Style.Font.Bold = true;
            this.textBox15.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox15.Value = "Cara Pembayaran";
            // 
            // textBox18
            // 
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.4000002145767212), Telerik.Reporting.Drawing.Unit.Inch(0.639703094959259));
            this.textBox18.Name = "textBox10";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.18000014126300812));
            this.textBox18.Style.Font.Bold = true;
            this.textBox18.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox18.Value = ":";
            // 
            // textBox21
            // 
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5000002384185791), Telerik.Reporting.Drawing.Unit.Inch(0.47855439782142639));
            this.textBox21.Name = "textBox10";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1000001430511475), Telerik.Reporting.Drawing.Unit.Inch(0.18000014126300812));
            this.textBox21.Style.Font.Italic = true;
            this.textBox21.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox21.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox21.Value = "=PaymentMethod";
            // 
            // textBox22
            // 
            this.textBox22.CanGrow = false;
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579), Telerik.Reporting.Drawing.Unit.Inch(1.1290102005004883));
            this.textBox22.Name = "textBox2";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699), Telerik.Reporting.Drawing.Unit.Inch(0.18000014126300812));
            this.textBox22.Style.Font.Bold = true;
            this.textBox22.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox22.Value = ":";
            // 
            // textBox23
            // 
            this.textBox23.CanGrow = false;
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(1.3090890645980835));
            this.textBox23.Name = "textBox3";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0999212265014648), Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737));
            this.textBox23.Style.Font.Bold = true;
            this.textBox23.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox23.Value = "Nama";
            // 
            // textBox25
            // 
            this.textBox25.CanGrow = false;
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579), Telerik.Reporting.Drawing.Unit.Inch(1.3090895414352417));
            this.textBox25.Name = "textBox16";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699), Telerik.Reporting.Drawing.Unit.Inch(0.17999982833862305));
            this.textBox25.Style.Font.Bold = true;
            this.textBox25.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox25.Value = ":";
            // 
            // textBox26
            // 
            this.textBox26.CanGrow = false;
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.012500126846134663), Telerik.Reporting.Drawing.Unit.Inch(1.4891678094863892));
            this.textBox26.Name = "textBox20";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0874210596084595), Telerik.Reporting.Drawing.Unit.Inch(0.18000014126300812));
            this.textBox26.Style.Font.Bold = true;
            this.textBox26.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox26.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox26.Value = "Alamat";
            // 
            // textBox27
            // 
            this.textBox27.CanGrow = false;
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2000001668930054), Telerik.Reporting.Drawing.Unit.Inch(1.3090887069702148));
            this.textBox27.Name = "textBox22";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1604359149932861), Telerik.Reporting.Drawing.Unit.Inch(0.18000046908855438));
            this.textBox27.Style.Font.Italic = true;
            this.textBox27.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox27.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox27.Value = "=PatientName";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = false;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579), Telerik.Reporting.Drawing.Unit.Inch(1.4891678094863892));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699), Telerik.Reporting.Drawing.Unit.Inch(0.16938638687133789));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox1.Value = ":";
            // 
            // ARPersonalPaymentSlipRpt
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail});
            this.Name = "DownPaymentReceipt";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Cm(0);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(0);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageSettings.PaperSize = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(8.5), Telerik.Reporting.Drawing.Unit.Inch(5.5));
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Name = "Microsoft Sans Serif";
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.4874815940856934);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox txtUserName;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox txtTotalAmountInWords;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox43;
        private Telerik.Reporting.TextBox textBox44;
        private Telerik.Reporting.TextBox textBox45;
        private Telerik.Reporting.TextBox textBox46;
        private Telerik.Reporting.TextBox textBox47;
        private Telerik.Reporting.TextBox textBox48;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox1;
    }
}