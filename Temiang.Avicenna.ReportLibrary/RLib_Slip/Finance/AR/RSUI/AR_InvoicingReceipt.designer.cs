namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Finance.AR.RSUI
    
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
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.txtTotalAmountInWords = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.TxtAmount = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.TxtUserName = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.TxtCityRS = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox23 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(16.763999938964844);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox14,
            this.textBox26,
            this.txtTotalAmountInWords,
            this.textBox6,
            this.textBox30,
            this.textBox32,
            this.TxtAmount,
            this.textBox8,
            this.textBox19,
            this.TxtUserName,
            this.textBox22,
            this.TxtCityRS,
            this.textBox2,
            this.textBox3,
            this.textBox4,
            this.textBox23});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.Font.Name = "Microsoft Sans Serif";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.89999991655349731), Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.3084161281585693), Telerik.Reporting.Drawing.Unit.Inch(0.28958341479301453));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14);
            this.textBox1.Style.Font.Underline = true;
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "KWITANSI";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609), Telerik.Reporting.Drawing.Unit.Inch(1.8896621465682983));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.2000000476837158), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox14.Style.Font.Name = "Tahoma";
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox14.Value = "=\'No. Kwitansi : \' +InvoiceNo";
            // 
            // textBox26
            // 
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.099999986588954926), Telerik.Reporting.Drawing.Unit.Inch(3.7000000476837158));
            this.textBox26.Name = "textBox20";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0000001192092896), Telerik.Reporting.Drawing.Unit.Inch(0.299999862909317));
            this.textBox26.Style.Font.Bold = false;
            this.textBox26.Style.Font.Italic = true;
            this.textBox26.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox26.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox26.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox26.Value = "Jumlah Rp.";
            // 
            // txtTotalAmountInWords
            // 
            this.txtTotalAmountInWords.CanGrow = false;
            this.txtTotalAmountInWords.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.506251335144043), Telerik.Reporting.Drawing.Unit.Inch(2.4063303470611572));
            this.txtTotalAmountInWords.Name = "txtTotalAmountInWords";
            this.txtTotalAmountInWords.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.6937491893768311), Telerik.Reporting.Drawing.Unit.Inch(0.40000021457672119));
            this.txtTotalAmountInWords.Style.Font.Bold = true;
            this.txtTotalAmountInWords.Style.Font.Italic = true;
            this.txtTotalAmountInWords.Style.Font.Name = "Tahoma";
            this.txtTotalAmountInWords.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.txtTotalAmountInWords.Style.Font.Strikeout = false;
            this.txtTotalAmountInWords.Value = "=TotalAmountInWords";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.099999986588954926), Telerik.Reporting.Drawing.Unit.Inch(2.3999998569488525));
            this.textBox6.Name = "textBox32";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2000001668930054), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox6.Style.Font.Bold = false;
            this.textBox6.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox6.Value = "Banyaknya Uang";
            // 
            // textBox30
            // 
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5), Telerik.Reporting.Drawing.Unit.Inch(2.1870658397674561));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.7000000476837158), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox30.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox30.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox30.Value = "=GuarantorName";
            // 
            // textBox32
            // 
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.099999986588954926), Telerik.Reporting.Drawing.Unit.Inch(2.1870658397674561));
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2000001668930054), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox32.Style.Font.Bold = false;
            this.textBox32.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox32.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox32.Value = "Sudah Terima Dari";
            // 
            // TxtAmount
            // 
            this.TxtAmount.CanGrow = false;
            this.TxtAmount.Format = "{0:N2}";
            this.TxtAmount.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000789403915405), Telerik.Reporting.Drawing.Unit.Inch(3.7000000476837158));
            this.TxtAmount.Name = "TxtAmount";
            this.TxtAmount.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2), Telerik.Reporting.Drawing.Unit.Inch(0.29999971389770508));
            this.TxtAmount.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.TxtAmount.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.TxtAmount.Style.Font.Italic = true;
            this.TxtAmount.Style.Font.Name = "Microsoft Sans Serif";
            this.TxtAmount.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.TxtAmount.Style.Font.Underline = true;
            this.TxtAmount.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(5);
            this.TxtAmount.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.TxtAmount.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.TxtAmount.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.TxtAmount.Value = "=Amount";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.39992094039917), Telerik.Reporting.Drawing.Unit.Inch(2.1870658397674561));
            this.textBox8.Name = "textBox9";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox8.Style.Font.Bold = true;
            this.textBox8.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox8.Value = ":";
            // 
            // textBox19
            // 
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421), Telerik.Reporting.Drawing.Unit.Inch(2.3999998569488525));
            this.textBox19.Name = "textBox9";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.10617256164550781), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox19.Style.Font.Bold = true;
            this.textBox19.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox19.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox19.Value = ":";
            // 
            // TxtUserName
            // 
            this.TxtUserName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.5000011920928955), Telerik.Reporting.Drawing.Unit.Inch(5.9000000953674316));
            this.TxtUserName.Name = "TxtUserName";
            this.TxtUserName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.3999979496002197), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.TxtUserName.Style.Font.Name = "Microsoft Sans Serif";
            this.TxtUserName.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtUserName.Value = "";
            // 
            // textBox22
            // 
            this.textBox22.Format = ", {0:dd-MMM-yyyy}";
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.7000000476837158), Telerik.Reporting.Drawing.Unit.Inch(4.4000000953674316));
            this.textBox22.Name = "TxtUserName";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2000002861022949), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox22.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox22.Value = "=PrintedDate";
            // 
            // TxtCityRS
            // 
            this.TxtCityRS.Format = "{0:dd-MMM-yyyy}";
            this.TxtCityRS.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.5000011920928955), Telerik.Reporting.Drawing.Unit.Inch(4.4000000953674316));
            this.TxtCityRS.Name = "TxtCityRS";
            this.TxtCityRS.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.142390251159668), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.TxtCityRS.Style.Font.Name = "Microsoft Sans Serif";
            this.TxtCityRS.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.TxtCityRS.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609), Telerik.Reporting.Drawing.Unit.Inch(2.8999998569488525));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2935906648635864), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox2.Style.Font.Bold = false;
            this.textBox2.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox2.Value = "Untuk Pembayaran";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.393669605255127), Telerik.Reporting.Drawing.Unit.Inch(2.8999998569488525));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.10617256164550781), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox3.Value = ":";
            // 
            // textBox4
            // 
            this.textBox4.CanGrow = false;
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.506251335144043), Telerik.Reporting.Drawing.Unit.Inch(2.8999998569488525));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.6937491893768311), Telerik.Reporting.Drawing.Unit.Inch(0.60000002384185791));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Italic = true;
            this.textBox4.Style.Font.Name = "Tahoma";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox4.Style.Font.Strikeout = false;
            this.textBox4.Value = "=InvoiceNotes";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.099999748170375824);
            this.detail.Name = "detail";
            // 
            // textBox23
            // 
            this.textBox23.Format = "Avicenna HIS, Print Date : {0:dd-MM-yyyy HH:mm}";
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(6.399960994720459));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.3149604797363281), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox23.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox23.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox23.Style.Font.Italic = true;
            this.textBox23.Style.Font.Name = "Tahoma";
            this.textBox23.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox23.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox23.Value = "=Now()";
            // 
            // AR_InvoicingReceipt
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail});
            this.Name = "PaymentReceiveReceiptIP";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Cm(0.5);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Cm(0);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageSettings.PaperSize = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(14), Telerik.Reporting.Drawing.Unit.Cm(21.5));
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(5.3149604797363281);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox txtTotalAmountInWords;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.TextBox TxtAmount;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox TxtUserName;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox TxtCityRS;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox23;
    }
}