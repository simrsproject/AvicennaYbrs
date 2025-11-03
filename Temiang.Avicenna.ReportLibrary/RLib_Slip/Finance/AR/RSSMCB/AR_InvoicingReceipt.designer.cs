namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Finance.AR.RSSMCB
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class AR_InvoicingReceipt
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
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.TxtAmount = new Telerik.Reporting.TextBox();
            this.textBox42 = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.textBox36 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.txtFinance = new Telerik.Reporting.TextBox();
            this.reportHeaderSection1 = new Telerik.Reporting.ReportHeaderSection();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.group1 = new Telerik.Reporting.Group();
            this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();
            this.txtTotalAmountInWords = new Telerik.Reporting.TextBox();
            this.textBox43 = new Telerik.Reporting.TextBox();
            this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox46 = new Telerik.Reporting.TextBox();
            this.group2 = new Telerik.Reporting.Group();
            this.groupFooterSection2 = new Telerik.Reporting.GroupFooterSection();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.groupHeaderSection2 = new Telerik.Reporting.GroupHeaderSection();
            this.group3 = new Telerik.Reporting.Group();
            this.groupFooterSection3 = new Telerik.Reporting.GroupFooterSection();
            this.groupHeaderSection3 = new Telerik.Reporting.GroupHeaderSection();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699);
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.10003934055566788), Telerik.Reporting.Drawing.Unit.Inch(0.84791660308837891));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.10007905960083), Telerik.Reporting.Drawing.Unit.Inch(0.30000010132789612));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Tahoma";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(13);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Value = "KWITANSI";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8000788688659668), Telerik.Reporting.Drawing.Unit.Inch(1.5479167699813843));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.8998818397521973), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Name = "Tahoma";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox2.Value = "=GuarantorName";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.099999964237213135), Telerik.Reporting.Drawing.Unit.Inch(1.5479167699813843));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.6000000238418579), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox7.Style.Font.Name = "Tahoma";
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox7.Value = "Sudah diterima dari ";
            // 
            // textBox21
            // 
            this.textBox21.Format = "{0}";
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.7999600172042847), Telerik.Reporting.Drawing.Unit.Inch(1.3478385210037231));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.90000057220459), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox21.Style.Font.Name = "Tahoma";
            this.textBox21.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox21.Value = "=NoKW";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699);
            this.detail.Name = "detail";
            this.detail.Style.Visible = false;
            // 
            // textBox24
            // 
            this.textBox24.CanGrow = false;
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0993700698018074), Telerik.Reporting.Drawing.Unit.Inch(0.75));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5992504358291626), Telerik.Reporting.Drawing.Unit.Inch(0.19996070861816406));
            this.textBox24.Style.Font.Name = "Tahoma";
            this.textBox24.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox24.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox24.Value = "Jumlah Uang (Rp)";
            // 
            // TxtAmount
            // 
            this.TxtAmount.CanGrow = false;
            this.TxtAmount.Format = "{0:N0}";
            this.TxtAmount.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3), Telerik.Reporting.Drawing.Unit.Inch(0.75));
            this.TxtAmount.Name = "TxtAmount";
            this.TxtAmount.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.2999999523162842), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.TxtAmount.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.TxtAmount.Style.Font.Bold = true;
            this.TxtAmount.Style.Font.Name = "Tahoma";
            this.TxtAmount.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.TxtAmount.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.TxtAmount.Value = "=TAmount";
            // 
            // textBox42
            // 
            this.textBox42.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.6990543603897095), Telerik.Reporting.Drawing.Unit.Inch(0.74996083974838257));
            this.textBox42.Name = "textBox42";
            this.textBox42.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.0998026505112648), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox42.Style.Font.Name = "Tahoma";
            this.textBox42.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox42.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox42.Value = ":";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.28764298558235168);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox36,
            this.textBox11});
            this.pageFooter.Name = "pageFooter";
            // 
            // textBox36
            // 
            this.textBox36.Format = "Avicenna HIS, Print Date : {0:dd-MM-yyyy HH:mm}";
            this.textBox36.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9354959881165996E-05), Telerik.Reporting.Drawing.Unit.Inch(3.9164224290288985E-05));
            this.textBox36.Name = "textBox36";
            this.textBox36.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.0999612808227539), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox36.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox36.Style.Font.Italic = true;
            this.textBox36.Style.Font.Name = "Tahoma";
            this.textBox36.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox36.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox36.Value = "=PrintedDate";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.10007905960083), Telerik.Reporting.Drawing.Unit.Inch(3.9164224290288985E-05));
            this.textBox11.Name = "textBox36";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.7998819351196289), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox11.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox11.Style.Font.Italic = true;
            this.textBox11.Style.Font.Name = "Tahoma";
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox11.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(2);
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox11.Value = "= PageNumber +\' / \'+ PageCount";
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.13229165971279144);
            this.reportFooterSection1.KeepTogether = false;
            this.reportFooterSection1.Name = "reportFooterSection1";
            this.reportFooterSection1.Style.Visible = false;
            // 
            // textBox13
            // 
            this.textBox13.CanGrow = false;
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.099644787609577179), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox13.Name = "textBox35";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2998433113098145), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox13.Style.Font.Name = "Tahoma";
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox13.Value = "Terbilang";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0993700698018074), Telerik.Reporting.Drawing.Unit.Inch(0.43117114901542664));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.1001186370849609), Telerik.Reporting.Drawing.Unit.Inch(0.23113149404525757));
            this.textBox14.Style.Font.Name = "Tahoma";
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox14.Value = "=Pmbyaran";
            // 
            // textBox17
            // 
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.10059293359518051), Telerik.Reporting.Drawing.Unit.Inch(1.3478380441665649));
            this.textBox17.Name = "textBox16";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3000000715255737), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox17.Style.Font.Name = "Tahoma";
            this.textBox17.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox17.Value = "No.";
            // 
            // textBox25
            // 
            this.textBox25.Format = "NPWP : {0}";
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.1999998092651367), Telerik.Reporting.Drawing.Unit.Inch(0.4479166567325592));
            this.textBox25.Name = "textBox16";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.6001186370849609), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox25.Style.Font.Name = "Tahoma";
            this.textBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox25.Value = "=NPWP";
            // 
            // txtFinance
            // 
            this.txtFinance.CanShrink = false;
            this.txtFinance.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0993700698018074), Telerik.Reporting.Drawing.Unit.Inch(1.2000001668930054));
            this.txtFinance.Name = "txtFinance";
            this.txtFinance.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.3006300926208496), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtFinance.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.txtFinance.Style.Font.Name = "Tahoma";
            this.txtFinance.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.txtFinance.Style.Font.Underline = true;
            this.txtFinance.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtFinance.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.txtFinance.Value = "=FinanceHead";
            // 
            // reportHeaderSection1
            // 
            this.reportHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(1.8479167222976685);
            this.reportHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox2,
            this.textBox1,
            this.textBox7,
            this.textBox21,
            this.textBox25,
            this.textBox17,
            this.textBox5,
            this.textBox8,
            this.textBox4});
            this.reportHeaderSection1.Name = "reportHeaderSection1";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.7001973390579224), Telerik.Reporting.Drawing.Unit.Inch(1.347838282585144));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.0998026505112648), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox5.Style.Font.Name = "Tahoma";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.Value = ":";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.7000788450241089), Telerik.Reporting.Drawing.Unit.Inch(1.5479167699813843));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.0998026505112648), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox8.Style.Font.Name = "Tahoma";
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox8.Value = ":";
            // 
            // textBox4
            // 
            this.textBox4.Format = "{0}";
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.1999998092651367), Telerik.Reporting.Drawing.Unit.Inch(0.64783781766891479));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.6001183986663818), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox4.Style.Font.Name = "Tahoma";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox4.Value = "=InvoiceNo";
            // 
            // textBox6
            // 
            this.textBox6.Format = "{0:dd MMMM yyyy}";
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.10059306025505066), Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.0000393390655518), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox6.Style.Font.Bold = false;
            this.textBox6.Style.Font.Name = "Tahoma";
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "=AddressLine2 +\', \' + Format(\'{0:dd-MMM-yyyy}\' ,InvoiceDate)";
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
            this.groupFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(1.0499999523162842);
            this.groupFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox13,
            this.textBox14,
            this.txtTotalAmountInWords,
            this.textBox43,
            this.TxtAmount,
            this.textBox24,
            this.textBox42});
            this.groupFooterSection1.Name = "groupFooterSection1";
            // 
            // txtTotalAmountInWords
            // 
            this.txtTotalAmountInWords.CanGrow = false;
            this.txtTotalAmountInWords.Format = "# {0} #";
            this.txtTotalAmountInWords.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8003153800964356), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.txtTotalAmountInWords.Name = "txtTotalAmountInWords";
            this.txtTotalAmountInWords.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.9998030662536621), Telerik.Reporting.Drawing.Unit.Inch(0.43109226226806641));
            this.txtTotalAmountInWords.Style.Font.Name = "Tahoma";
            this.txtTotalAmountInWords.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.txtTotalAmountInWords.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtTotalAmountInWords.Value = "=tebilang";
            // 
            // textBox43
            // 
            this.textBox43.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.7001973390579224), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox43.Name = "textBox43";
            this.textBox43.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.0998026505112648), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox43.Style.Font.Name = "Tahoma";
            this.textBox43.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox43.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox43.Value = ":";
            // 
            // groupHeaderSection1
            // 
            this.groupHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699);
            this.groupHeaderSection1.Name = "groupHeaderSection1";
            this.groupHeaderSection1.Style.Visible = false;
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.0999999046325684), Telerik.Reporting.Drawing.Unit.Inch(1.4609851837158203));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.2999615669250488), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.textBox20.Style.Font.Name = "Tahoma";
            this.textBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox20.Value = "Pembayaran paling lambat sesuai dengan perjanjian.";
            // 
            // textBox26
            // 
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.0999999046325684), Telerik.Reporting.Drawing.Unit.Inch(1.1826404333114624));
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.3000001907348633), Telerik.Reporting.Drawing.Unit.Inch(0.13909365236759186));
            this.textBox26.Style.Font.Name = "Tahoma";
            this.textBox26.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox26.Value = "Kwitansi ini dianggap lunas bila dilampirkan bukti transfer ke rekening kami.";
            // 
            // textBox46
            // 
            this.textBox46.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.1000006198883057), Telerik.Reporting.Drawing.Unit.Inch(1.3218127489089966));
            this.textBox46.Name = "textBox46";
            this.textBox46.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.2999606132507324), Telerik.Reporting.Drawing.Unit.Inch(0.13909365236759186));
            this.textBox46.Style.Font.Name = "Tahoma";
            this.textBox46.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox46.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox46.Value = "=BankNm +\' \' +  Cabang  +\' AC \' + NoRek + \' an \' + NamaRek";
            // 
            // group2
            // 
            this.group2.GroupFooter = this.groupFooterSection2;
            this.group2.GroupHeader = this.groupHeaderSection2;
            this.group2.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("=InvoiceNo")});
            this.group2.Name = "group2";
            // 
            // groupFooterSection2
            // 
            this.groupFooterSection2.Height = Telerik.Reporting.Drawing.Unit.Inch(1.7000001668930054);
            this.groupFooterSection2.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtFinance,
            this.textBox20,
            this.textBox26,
            this.textBox46,
            this.textBox6,
            this.textBox3});
            this.groupFooterSection2.Name = "groupFooterSection2";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.10059306025505066), Telerik.Reporting.Drawing.Unit.Inch(1.4000788927078247));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.7990520000457764), Telerik.Reporting.Drawing.Unit.Inch(0.19992145895957947));
            this.textBox3.Style.Font.Name = "Tahoma";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = "Direktur";
            // 
            // groupHeaderSection2
            // 
            this.groupHeaderSection2.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699);
            this.groupHeaderSection2.Name = "groupHeaderSection2";
            this.groupHeaderSection2.Style.Visible = false;
            // 
            // group3
            // 
            this.group3.GroupFooter = this.groupFooterSection3;
            this.group3.GroupHeader = this.groupHeaderSection3;
            this.group3.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("=GuarantorName")});
            this.group3.Name = "group3";
            // 
            // groupFooterSection3
            // 
            this.groupFooterSection3.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0416666679084301);
            this.groupFooterSection3.Name = "groupFooterSection3";
            this.groupFooterSection3.Style.Visible = true;
            // 
            // groupHeaderSection3
            // 
            this.groupHeaderSection3.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699);
            this.groupHeaderSection3.Name = "groupHeaderSection3";
            this.groupHeaderSection3.Style.Visible = false;
            // 
            // AR_InvoicingReceipt
            // 
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            this.group2,
            this.group1,
            this.group3});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection2,
            this.groupFooterSection2,
            this.groupHeaderSection1,
            this.groupFooterSection1,
            this.groupHeaderSection3,
            this.groupFooterSection3,
            this.pageHeader,
            this.detail,
            this.pageFooter,
            this.reportFooterSection1,
            this.reportHeaderSection1});
            this.Name = "AR_Invoicing";
            this.PageSettings.Landscape = true;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Inch(0.25);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Inch(0.25);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Inch(0.25);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(0.5);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageSettings.PaperSize = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(8.5), Telerik.Reporting.Drawing.Unit.Inch(5.5));
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(8);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox36;
        private ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox txtFinance;
        private ReportHeaderSection reportHeaderSection1;
        private Group group1;
        private GroupFooterSection groupFooterSection1;
        private GroupHeaderSection groupHeaderSection1;
        private Group group2;
        private GroupFooterSection groupFooterSection2;
        private GroupHeaderSection groupHeaderSection2;
        private Group group3;
        private GroupFooterSection groupFooterSection3;
        private GroupHeaderSection groupHeaderSection3;
        private Telerik.Reporting.TextBox txtTotalAmountInWords;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox TxtAmount;
        private Telerik.Reporting.TextBox textBox42;
        private Telerik.Reporting.TextBox textBox43;
        private Telerik.Reporting.TextBox textBox46;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
    }
}