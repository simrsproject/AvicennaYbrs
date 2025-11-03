namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Inventory.Procurement.RSIAMTP
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class PurchaseRequest
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox35 = new Telerik.Reporting.TextBox();
            this.textBox34 = new Telerik.Reporting.TextBox();
            this.textBox33 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.reportHeaderSection1 = new Telerik.Reporting.ReportHeaderSection();
            this.titleTextBox = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.TxtKetua = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.TxtSekretaris = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(0.3999999463558197);
            this.pageHeader.Name = "pageHeader";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.17003941535949707);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox35,
            this.textBox34,
            this.textBox33,
            this.textBox31,
            this.textBox7,
            this.textBox1});
            this.detail.Name = "detail";
            // 
            // textBox35
            // 
            this.textBox35.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.5000004768371582), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox35.Name = "textBox35";
            this.textBox35.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.59984207153320312), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox35.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox35.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox35.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox35.Style.Font.Name = "Tahoma";
            this.textBox35.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox35.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox35.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox35.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox35.Value = "=SRItemUnit";
            // 
            // textBox34
            // 
            this.textBox34.Format = "{0:N0}";
            this.textBox34.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.7811713218688965), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox34.Name = "textBox34";
            this.textBox34.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.71890878677368164), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox34.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox34.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox34.Style.Font.Name = "Tahoma";
            this.textBox34.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox34.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox34.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox34.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox34.Value = "=Quantity";
            // 
            // textBox33
            // 
            this.textBox33.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.225275993347168), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.5558164119720459), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox33.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox33.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox33.Style.Font.Name = "Tahoma";
            this.textBox33.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox33.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox33.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox33.Value = "=ItemName";
            // 
            // textBox31
            // 
            this.textBox31.Format = "{0:#\".\"}";
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0033728282433003187), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.34666648507118225), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox31.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox31.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox31.Style.Font.Name = "Tahoma";
            this.textBox31.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox31.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox31.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox31.Value = "= RowNumber()";
            // 
            // textBox7
            // 
            this.textBox7.Format = "{0:N0}";
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.3501180112361908), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox7.Name = "textBox34";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.87000036239624023), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox7.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.Font.Name = "Tahoma";
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox7.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox7.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox7.Value = "=ItemId";
            // 
            // textBox1
            // 
            this.textBox1.Format = "{0}";
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.1000003814697266), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox1.Name = "textBox46";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5340086221694946), Telerik.Reporting.Drawing.Unit.Inch(0.17000007629394531));
            this.textBox1.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox1.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox1.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox1.Style.Font.Name = "Tahoma";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox1.Value = "=FromServiceUnitName";
            // 
            // reportHeaderSection1
            // 
            this.reportHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(1.9399213790893555);
            this.reportHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.titleTextBox,
            this.textBox27,
            this.textBox26,
            this.textBox21,
            this.textBox24,
            this.textBox23,
            this.textBox30,
            this.textBox29,
            this.textBox28,
            this.textBox25,
            this.textBox22,
            this.textBox20,
            this.textBox19,
            this.textBox18,
            this.textBox2,
            this.textBox3,
            this.textBox8});
            this.reportHeaderSection1.Name = "reportHeaderSection1";
            this.reportHeaderSection1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.reportHeaderSection1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609));
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.4802360534667969), Telerik.Reporting.Drawing.Unit.Inch(0.22500006854534149));
            this.titleTextBox.Style.Font.Bold = true;
            this.titleTextBox.Style.Font.Name = "Tahoma";
            this.titleTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14);
            this.titleTextBox.Style.Font.Underline = true;
            this.titleTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.titleTextBox.StyleName = "Title";
            this.titleTextBox.Value = "PENGAJUAN PEMBELIAN BARANG";
            // 
            // textBox27
            // 
            this.textBox27.Format = "{0:dd/MM/yyyy}";
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0033728282433003187), Telerik.Reporting.Drawing.Unit.Inch(1.2700790166854858));
            this.textBox27.Name = "textBox29";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.3000006675720215), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox27.Style.Font.Bold = true;
            this.textBox27.Style.Font.Name = "Tahoma";
            this.textBox27.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox27.Value = "=ServiceUnitName";
            // 
            // textBox26
            // 
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.120118260383606), Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929));
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox26.Style.Font.Bold = true;
            this.textBox26.Style.Font.Name = "Tahoma";
            this.textBox26.Value = ":";
            // 
            // textBox21
            // 
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579));
            this.textBox21.Name = "textBox30";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1200000047683716), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox21.Style.Font.Bold = true;
            this.textBox21.Style.Font.Name = "Tahoma";
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox21.Value = "Kepada Yth.";
            // 
            // textBox24
            // 
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.120118260383606), Telerik.Reporting.Drawing.Unit.Inch(0.62992095947265625));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox24.Style.Font.Bold = true;
            this.textBox24.Style.Font.Name = "Tahoma";
            this.textBox24.Value = ":";
            // 
            // textBox23
            // 
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.120118260383606), Telerik.Reporting.Drawing.Unit.Inch(0.45984235405921936));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609), Telerik.Reporting.Drawing.Unit.Inch(0.17000007629394531));
            this.textBox23.Style.Font.Bold = true;
            this.textBox23.Style.Font.Name = "Tahoma";
            this.textBox23.Value = ":";
            // 
            // textBox30
            // 
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.62992113828659058));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1200000047683716), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox30.Style.Font.Bold = true;
            this.textBox30.Style.Font.Name = "Tahoma";
            this.textBox30.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox30.Value = "Tanggal";
            // 
            // textBox29
            // 
            this.textBox29.Format = "{0:dd/MM/yyyy}";
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2201972007751465), Telerik.Reporting.Drawing.Unit.Inch(0.62658786773681641));
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.6299999952316284), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox29.Style.Font.Bold = true;
            this.textBox29.Style.Font.Name = "Tahoma";
            this.textBox29.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox29.Value = "=TransactionDate";
            // 
            // textBox28
            // 
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0033728282433003187), Telerik.Reporting.Drawing.Unit.Inch(0.45984235405921936));
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1166666746139526), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox28.Style.Font.Bold = true;
            this.textBox28.Style.Font.Name = "Tahoma";
            this.textBox28.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox28.Value = "Nomor";
            // 
            // textBox25
            // 
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2251969575881958), Telerik.Reporting.Drawing.Unit.Inch(0.45984235405921936));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.625), Telerik.Reporting.Drawing.Unit.Inch(0.1666666716337204));
            this.textBox25.Style.Font.Bold = true;
            this.textBox25.Style.Font.Name = "Tahoma";
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox25.Value = "=TransactionNo";
            // 
            // textBox22
            // 
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(1.6000000238418579));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.3500000536441803), Telerik.Reporting.Drawing.Unit.Inch(0.339921236038208));
            this.textBox22.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox22.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox22.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox22.Style.Font.Bold = true;
            this.textBox22.Style.Font.Name = "Tahoma";
            this.textBox22.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox22.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox22.Value = "No";
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2251969575881958), Telerik.Reporting.Drawing.Unit.Inch(1.6000000238418579));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.5558164119720459), Telerik.Reporting.Drawing.Unit.Inch(0.339921236038208));
            this.textBox20.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox20.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox20.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox20.Style.Font.Bold = true;
            this.textBox20.Style.Font.Name = "Tahoma";
            this.textBox20.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox20.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox20.Value = "Nama & Spesifikasi Barang";
            // 
            // textBox19
            // 
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.7810916900634766), Telerik.Reporting.Drawing.Unit.Inch(1.6000000238418579));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.71890878677368164), Telerik.Reporting.Drawing.Unit.Inch(0.339921236038208));
            this.textBox19.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox19.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox19.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox19.Style.Font.Bold = true;
            this.textBox19.Style.Font.Name = "Tahoma";
            this.textBox19.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox19.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox19.Value = "Jumlah";
            // 
            // textBox18
            // 
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.5000004768371582), Telerik.Reporting.Drawing.Unit.Inch(1.6000000238418579));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.59984207153320312), Telerik.Reporting.Drawing.Unit.Inch(0.339921236038208));
            this.textBox18.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox18.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox18.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox18.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox18.Style.Font.Bold = true;
            this.textBox18.Style.Font.Name = "Tahoma";
            this.textBox18.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox18.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox18.Value = "Satuan";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929));
            this.textBox2.Name = "textBox30";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1200394630432129), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Name = "Tahoma";
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.Value = "Hal";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.35011830925941467), Telerik.Reporting.Drawing.Unit.Inch(1.6000000238418579));
            this.textBox3.Name = "textBox19";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.87000006437301636), Telerik.Reporting.Drawing.Unit.Inch(0.339921236038208));
            this.textBox3.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox3.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox3.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Name = "Tahoma";
            this.textBox3.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = "Kd Barang";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.1000003814697266), Telerik.Reporting.Drawing.Unit.Inch(1.5999212265014648));
            this.textBox8.Name = "textBox18";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5340086221694946), Telerik.Reporting.Drawing.Unit.Inch(0.339921236038208));
            this.textBox8.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox8.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox8.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox8.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox8.Style.Font.Bold = true;
            this.textBox8.Style.Font.Name = "Tahoma";
            this.textBox8.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Value = "Unit pemohon";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20003938674926758);
            this.pageFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageInfoTextBox,
            this.currentTimeTextBox});
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Format = "Hal : {0}";
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.0737099647521973), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.5602989196777344), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.pageInfoTextBox.Style.Font.Name = "Tahoma";
            this.pageInfoTextBox.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(2);
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "=PageNumber";
            // 
            // currentTimeTextBox
            // 
            this.currentTimeTextBox.Format = "Print : {0}";
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.1979167461395264), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.currentTimeTextBox.Style.Font.Name = "Tahoma";
            this.currentTimeTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "=NOW()";
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(2.3941271305084229);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox6,
            this.TxtKetua,
            this.textBox10,
            this.TxtSekretaris,
            this.textBox9,
            this.textBox11,
            this.textBox12,
            this.textBox4,
            this.textBox13});
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0033728282433003187), Telerik.Reporting.Drawing.Unit.Inch(1.0899604558944702));
            this.textBox6.Name = "textBox42";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.696627140045166), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox6.Style.Font.Name = "Tahoma";
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox6.Value = "Mengetahui, Menyetujui";
            // 
            // TxtKetua
            // 
            this.TxtKetua.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0033728282433003187), Telerik.Reporting.Drawing.Unit.Inch(2.1899607181549072));
            this.TxtKetua.Name = "TxtKetua";
            this.TxtKetua.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.696627140045166), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.TxtKetua.Style.Font.Name = "Tahoma";
            this.TxtKetua.Style.Font.Underline = true;
            this.TxtKetua.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtKetua.Value = "(   Manager Keuangan   )";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.0374608039855957), Telerik.Reporting.Drawing.Unit.Inch(1.089999794960022));
            this.textBox10.Name = "textBox42";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5965479612350464), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox10.Style.Font.Name = "Tahoma";
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox10.Value = "Pemohon,";
            // 
            // TxtSekretaris
            // 
            this.TxtSekretaris.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.9373817443847656), Telerik.Reporting.Drawing.Unit.Inch(2.1899607181549072));
            this.TxtSekretaris.Name = "TxtSekretaris";
            this.TxtSekretaris.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.696627140045166), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.TxtSekretaris.Style.Font.Name = "Tahoma";
            this.TxtSekretaris.Style.Font.Underline = true;
            this.TxtSekretaris.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtSekretaris.Value = "( Rumah Tangga )";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.7000000476837158), Telerik.Reporting.Drawing.Unit.Inch(2.194127082824707));
            this.textBox9.Name = "TxtKetua";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.899999737739563), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox9.Style.Font.Name = "Tahoma";
            this.textBox9.Style.Font.Underline = true;
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox9.Value = "( Logistik / Unit Purchasing )";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.7000000476837158), Telerik.Reporting.Drawing.Unit.Inch(1.0899604558944702));
            this.textBox11.Name = "textBox42";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.696627140045166), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox11.Style.Font.Name = "Tahoma";
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox11.Value = "Yang Mengajukan,";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.35011830925941467), Telerik.Reporting.Drawing.Unit.Inch(0.08996041864156723));
            this.textBox12.Name = "textBox42";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.2838907241821289), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox12.Style.Font.Name = "Tahoma";
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox12.Value = "*) Harga yang tertera adalah harga lama.";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0033728282433003187), Telerik.Reporting.Drawing.Unit.Inch(0.39003944396972656));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.49662718176841736), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Name = "Tahoma";
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox4.Value = "Notes :";
            // 
            // textBox13
            // 
            this.textBox13.Format = "{0:dd/MM/yyyy}";
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.60000008344650269), Telerik.Reporting.Drawing.Unit.Inch(0.39003944396972656));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.3748030662536621), Telerik.Reporting.Drawing.Unit.Inch(0.49999991059303284));
            this.textBox13.Style.Font.Bold = false;
            this.textBox13.Style.Font.Name = "Tahoma";
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox13.Value = "=Notes";
            // 
            // PurchaseRequest
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail,
            this.reportHeaderSection1,
            this.pageFooterSection1,
            this.reportFooterSection1});
            this.Name = "PurchaseRequest";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Cm(0.699999988079071);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Cm(0.699999988079071);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Name = "Tahoma";
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.7165355682373047);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private ReportHeaderSection reportHeaderSection1;
        private Telerik.Reporting.TextBox titleTextBox;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox28;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox35;
        private Telerik.Reporting.TextBox textBox34;
        private Telerik.Reporting.TextBox textBox33;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox textBox1;
        private PageFooterSection pageFooterSection1;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox3;
        private ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox TxtKetua;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox TxtSekretaris;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox13;
    }
}