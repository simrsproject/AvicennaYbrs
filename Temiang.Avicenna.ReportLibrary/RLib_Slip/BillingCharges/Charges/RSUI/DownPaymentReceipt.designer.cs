namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges.RSUI
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
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.txtUserName = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.txtTotalAmountInWords = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.textBox41 = new Telerik.Reporting.TextBox();
            this.textBox42 = new Telerik.Reporting.TextBox();
            this.TxtCityRS = new Telerik.Reporting.TextBox();
            this.textBox40 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.reportHeaderSection1 = new Telerik.Reporting.ReportHeaderSection();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2D);
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.Font.Name = "Tahoma";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.3D), Telerik.Reporting.Drawing.Unit.Inch(1.5D));
            this.textBox9.Name = "TxtNameRS";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.118D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox9.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox9.Style.Font.Bold = true;
            this.textBox9.Style.Font.Name = "Tahoma";
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox9.Style.Font.Underline = false;
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox9.Value = "BUKTI PEMBAYARAN SEMENTARA";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2D);
            this.detail.Name = "detail";
            this.detail.Style.Font.Name = "Tahoma";
            this.detail.Style.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = false;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.713D), Telerik.Reporting.Drawing.Unit.Inch(1.7D));
            this.textBox1.Name = "textBox32";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.787D), Telerik.Reporting.Drawing.Unit.Inch(0.18D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Tahoma";
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox1.Value = "No. Bukti";
            // 
            // textBox8
            // 
            this.textBox8.CanGrow = false;
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8D), Telerik.Reporting.Drawing.Unit.Inch(2.8D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4D), Telerik.Reporting.Drawing.Unit.Inch(0.18D));
            this.textBox8.Style.Font.Name = "Tahoma";
            this.textBox8.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox8.Value = "=RegistrationNo";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.7D), Telerik.Reporting.Drawing.Unit.Inch(2.052D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.18D));
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.Style.Font.Name = "Tahoma";
            this.textBox7.Value = ":";
            // 
            // textBox3
            // 
            this.textBox3.Format = "{0:, dd MMMM yyyy}";
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9D), Telerik.Reporting.Drawing.Unit.Inch(4.872D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox3.Style.Font.Name = "Tahoma";
            this.textBox3.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1D);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox3.Value = "=PaymentDate";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.9D), Telerik.Reporting.Drawing.Unit.Inch(5.87D));
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtUserName.Style.Font.Name = "Tahoma";
            this.txtUserName.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1D);
            this.txtUserName.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtUserName.Value = "";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.813D), Telerik.Reporting.Drawing.Unit.Inch(5.2D));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox12.Style.Font.Name = "Tahoma";
            this.textBox12.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1D);
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox12.Value = "Kasir";
            // 
            // textBox6
            // 
            this.textBox6.Format = "Rp. {0:N0}";
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8D), Telerik.Reporting.Drawing.Unit.Inch(3.2D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.788D), Telerik.Reporting.Drawing.Unit.Inch(0.3D));
            this.textBox6.Style.Font.Bold = true;
            this.textBox6.Style.Font.Name = "Tahoma";
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(16D);
            this.textBox6.Value = "=Amount";
            // 
            // textBox32
            // 
            this.textBox32.CanGrow = false;
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.3D), Telerik.Reporting.Drawing.Unit.Inch(2.052D));
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4D), Telerik.Reporting.Drawing.Unit.Inch(0.18D));
            this.textBox32.Style.Font.Bold = true;
            this.textBox32.Style.Font.Name = "Tahoma";
            this.textBox32.Value = "Telah Terima Dari";
            // 
            // textBox31
            // 
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.5D), Telerik.Reporting.Drawing.Unit.Inch(1.7D));
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.18D));
            this.textBox31.Style.Font.Bold = true;
            this.textBox31.Style.Font.Name = "Tahoma";
            this.textBox31.Value = ":";
            // 
            // textBox4
            // 
            this.textBox4.CanGrow = false;
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.3D), Telerik.Reporting.Drawing.Unit.Inch(2.272D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4D), Telerik.Reporting.Drawing.Unit.Inch(0.18D));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Name = "Tahoma";
            this.textBox4.Value = "Keterangan";
            // 
            // txtTotalAmountInWords
            // 
            this.txtTotalAmountInWords.CanGrow = false;
            this.txtTotalAmountInWords.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8D), Telerik.Reporting.Drawing.Unit.Inch(3.6D));
            this.txtTotalAmountInWords.Name = "txtTotalAmountInWords";
            this.txtTotalAmountInWords.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.618D), Telerik.Reporting.Drawing.Unit.Inch(0.5D));
            this.txtTotalAmountInWords.Style.Font.Name = "Tahoma";
            this.txtTotalAmountInWords.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1D);
            this.txtTotalAmountInWords.Value = "<TotalAmountInWords>";
            // 
            // textBox17
            // 
            this.textBox17.CanGrow = false;
            this.textBox17.Format = "{0}";
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.2D), Telerik.Reporting.Drawing.Unit.Inch(2.8D));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.218D), Telerik.Reporting.Drawing.Unit.Inch(0.18D));
            this.textBox17.Style.Font.Name = "Tahoma";
            this.textBox17.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox17.Value = "=\' / \' + MedicalNo";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.7D), Telerik.Reporting.Drawing.Unit.Inch(2.272D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.18D));
            this.textBox10.Style.Font.Bold = true;
            this.textBox10.Style.Font.Name = "Tahoma";
            this.textBox10.Value = ":";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.7D), Telerik.Reporting.Drawing.Unit.Inch(3.6D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.18D));
            this.textBox13.Style.Font.Bold = true;
            this.textBox13.Style.Font.Name = "Tahoma";
            this.textBox13.Value = ":";
            // 
            // textBox5
            // 
            this.textBox5.CanGrow = false;
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.3D), Telerik.Reporting.Drawing.Unit.Inch(3.6D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4D), Telerik.Reporting.Drawing.Unit.Inch(0.18D));
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Font.Name = "Tahoma";
            this.textBox5.Value = "Terbilang";
            // 
            // textBox30
            // 
            this.textBox30.CanGrow = false;
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8D), Telerik.Reporting.Drawing.Unit.Inch(2.052D));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.618D), Telerik.Reporting.Drawing.Unit.Inch(0.18D));
            this.textBox30.Style.Font.Name = "Tahoma";
            this.textBox30.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1D);
            this.textBox30.Value = "=PrintReceiptAsName";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.6D), Telerik.Reporting.Drawing.Unit.Inch(1.7D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.613D), Telerik.Reporting.Drawing.Unit.Inch(0.18D));
            this.textBox14.Style.Font.Name = "Tahoma";
            this.textBox14.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1D);
            this.textBox14.Value = "=PaymentNo";
            // 
            // textBox11
            // 
            this.textBox11.CanGrow = false;
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8D), Telerik.Reporting.Drawing.Unit.Inch(2.272D));
            this.textBox11.Name = "textBox4";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.618D), Telerik.Reporting.Drawing.Unit.Inch(0.337D));
            this.textBox11.Style.Font.Name = "Tahoma";
            this.textBox11.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1D);
            this.textBox11.Value = "=Notes";
            // 
            // textBox22
            // 
            this.textBox22.CanGrow = false;
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.7D), Telerik.Reporting.Drawing.Unit.Inch(2.609D));
            this.textBox22.Name = "textBox2";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.18D));
            this.textBox22.Style.Font.Bold = true;
            this.textBox22.Style.Font.Name = "Tahoma";
            this.textBox22.Value = ":";
            // 
            // textBox23
            // 
            this.textBox23.CanGrow = false;
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.3D), Telerik.Reporting.Drawing.Unit.Inch(2.609D));
            this.textBox23.Name = "textBox3";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4D), Telerik.Reporting.Drawing.Unit.Inch(0.18D));
            this.textBox23.Style.Font.Bold = true;
            this.textBox23.Style.Font.Name = "Tahoma";
            this.textBox23.Value = "Nama Pasien";
            // 
            // textBox25
            // 
            this.textBox25.CanGrow = false;
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.7D), Telerik.Reporting.Drawing.Unit.Inch(2.8D));
            this.textBox25.Name = "textBox16";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.18D));
            this.textBox25.Style.Font.Bold = true;
            this.textBox25.Style.Font.Name = "Tahoma";
            this.textBox25.Value = ":";
            // 
            // textBox27
            // 
            this.textBox27.CanGrow = false;
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8D), Telerik.Reporting.Drawing.Unit.Inch(2.609D));
            this.textBox27.Name = "textBox22";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.618D), Telerik.Reporting.Drawing.Unit.Inch(0.18D));
            this.textBox27.Style.Font.Name = "Tahoma";
            this.textBox27.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox27.Value = "=PatientName";
            // 
            // textBox28
            // 
            this.textBox28.CanGrow = false;
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.3D), Telerik.Reporting.Drawing.Unit.Inch(2.8D));
            this.textBox28.Name = "textBox2";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4D), Telerik.Reporting.Drawing.Unit.Inch(0.18D));
            this.textBox28.Style.Font.Bold = true;
            this.textBox28.Style.Font.Name = "Tahoma";
            this.textBox28.Value = "No. Reg/ No. RM";
            // 
            // textBox41
            // 
            this.textBox41.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.9D), Telerik.Reporting.Drawing.Unit.Inch(6.07D));
            this.textBox41.Name = "textBox19";
            this.textBox41.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox41.Style.Font.Bold = true;
            this.textBox41.Style.Font.Name = "Tahoma";
            this.textBox41.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox41.Value = "Penerima Deposit";
            // 
            // textBox42
            // 
            this.textBox42.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.3D), Telerik.Reporting.Drawing.Unit.Inch(4.424D));
            this.textBox42.Name = "textBox10";
            this.textBox42.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.6D), Telerik.Reporting.Drawing.Unit.Inch(0.411D));
            this.textBox42.Style.Font.Name = "Tahoma";
            this.textBox42.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1D);
            this.textBox42.Value = "Note : Jangan sampai hilang, Kembalikan ke kasir pada saat pembayaran Pulang !";
            // 
            // TxtCityRS
            // 
            this.TxtCityRS.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.8D), Telerik.Reporting.Drawing.Unit.Inch(4.872D));
            this.TxtCityRS.Name = "TxtCityRS";
            this.TxtCityRS.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.TxtCityRS.Style.Font.Name = "Tahoma";
            this.TxtCityRS.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            // 
            // textBox40
            // 
            this.textBox40.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.3D), Telerik.Reporting.Drawing.Unit.Inch(4.1D));
            this.textBox40.Name = "textBox40";
            this.textBox40.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.118D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox40.Style.Font.Name = "Tahoma";
            this.textBox40.Value = "";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = false;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.3D), Telerik.Reporting.Drawing.Unit.Inch(2.98D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4D), Telerik.Reporting.Drawing.Unit.Inch(0.18D));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Name = "Tahoma";
            this.textBox2.Value = "Ruang ";
            // 
            // textBox15
            // 
            this.textBox15.CanGrow = false;
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.7D), Telerik.Reporting.Drawing.Unit.Inch(2.98D));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.18D));
            this.textBox15.Style.Font.Bold = true;
            this.textBox15.Style.Font.Name = "Tahoma";
            this.textBox15.Value = ":";
            // 
            // reportHeaderSection1
            // 
            this.reportHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(6.5D);
            this.reportHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox9,
            this.textBox1,
            this.textBox8,
            this.textBox7,
            this.textBox3,
            this.txtUserName,
            this.textBox12,
            this.textBox6,
            this.textBox32,
            this.textBox31,
            this.textBox4,
            this.txtTotalAmountInWords,
            this.textBox17,
            this.textBox10,
            this.textBox13,
            this.textBox5,
            this.textBox30,
            this.textBox14,
            this.textBox11,
            this.textBox22,
            this.textBox23,
            this.textBox25,
            this.textBox27,
            this.textBox28,
            this.textBox41,
            this.textBox42,
            this.TxtCityRS,
            this.textBox40,
            this.textBox2,
            this.textBox15,
            this.textBox16,
            this.textBox18,
            this.textBox19,
            this.textBox20,
            this.textBox21});
            this.reportHeaderSection1.Name = "reportHeaderSection1";
            this.reportHeaderSection1.Style.Font.Name = "Tahoma";
            // 
            // textBox16
            // 
            this.textBox16.CanGrow = false;
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8D), Telerik.Reporting.Drawing.Unit.Inch(2.98D));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.618D), Telerik.Reporting.Drawing.Unit.Inch(0.18D));
            this.textBox16.Style.Font.Bold = false;
            this.textBox16.Style.Font.Name = "Tahoma";
            this.textBox16.Value = "=ServiceUnitName";
            // 
            // textBox18
            // 
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.3D), Telerik.Reporting.Drawing.Unit.Inch(4.372D));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.118D), Telerik.Reporting.Drawing.Unit.Inch(0.052D));
            this.textBox18.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox18.Style.Font.Name = "Tahoma";
            this.textBox18.Value = "";
            // 
            // textBox19
            // 
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.3D), Telerik.Reporting.Drawing.Unit.Inch(2D));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.118D), Telerik.Reporting.Drawing.Unit.Inch(0.052D));
            this.textBox19.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox19.Style.Font.Name = "Tahoma";
            this.textBox19.Value = "";
            // 
            // textBox20
            // 
            this.textBox20.CanGrow = false;
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.3D), Telerik.Reporting.Drawing.Unit.Inch(3.283D));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4D), Telerik.Reporting.Drawing.Unit.Inch(0.18D));
            this.textBox20.Style.Font.Bold = true;
            this.textBox20.Style.Font.Name = "Tahoma";
            this.textBox20.Value = "Uang Sejumlah";
            // 
            // textBox21
            // 
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.7D), Telerik.Reporting.Drawing.Unit.Inch(3.283D));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.18D));
            this.textBox21.Style.Font.Bold = true;
            this.textBox21.Style.Font.Name = "Tahoma";
            this.textBox21.Value = ":";
            // 
            // DownPaymentReceipt
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail,
            this.reportHeaderSection1});
            this.Name = "DownPaymentReceipt";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(1D), Telerik.Reporting.Drawing.Unit.Mm(1D), Telerik.Reporting.Drawing.Unit.Mm(1D), Telerik.Reporting.Drawing.Unit.Mm(1D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageSettings.PaperSize = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(14D), Telerik.Reporting.Drawing.Unit.Cm(20.5D));
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Name = "Microsoft Sans Serif";
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(5.433D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox txtUserName;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox txtTotalAmountInWords;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox28;
        private Telerik.Reporting.TextBox textBox41;
        private Telerik.Reporting.TextBox textBox42;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox TxtCityRS;
        private Telerik.Reporting.TextBox textBox40;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox15;
        private ReportHeaderSection reportHeaderSection1;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox21;
    }
}