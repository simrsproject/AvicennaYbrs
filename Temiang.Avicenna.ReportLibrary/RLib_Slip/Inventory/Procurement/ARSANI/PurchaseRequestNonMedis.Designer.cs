namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Inventory.Procurement.ARSANI
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class PurchaseRequestNonMedis
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.textBox36 = new Telerik.Reporting.TextBox();
            this.textBox47 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox35 = new Telerik.Reporting.TextBox();
            this.textBox34 = new Telerik.Reporting.TextBox();
            this.textBox33 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.reportHeaderSection1 = new Telerik.Reporting.ReportHeaderSection();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.group1 = new Telerik.Reporting.Group();
            this.labelsGroupFooter = new Telerik.Reporting.GroupFooterSection();
            this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(1.2999211549758911);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox36,
            this.textBox47,
            this.textBox25,
            this.textBox29,
            this.textBox28,
            this.textBox30,
            this.textBox13,
            this.textBox1,
            this.textBox4,
            this.textBox8,
            this.textBox14,
            this.textBox15,
            this.textBox16});
            this.pageHeader.Name = "pageHeader";
            // 
            // textBox36
            // 
            this.textBox36.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.4196467399597168), Telerik.Reporting.Drawing.Unit.Inch(0.05646967887878418));
            this.textBox36.Name = "textBox36";
            this.textBox36.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.7999998927116394), Telerik.Reporting.Drawing.Unit.Inch(0.24349093437194824));
            this.textBox36.Style.Font.Name = "Tahoma";
            this.textBox36.Style.Visible = false;
            this.textBox36.Value = "REVISI : 01";
            // 
            // textBox47
            // 
            this.textBox47.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.4000000953674316), Telerik.Reporting.Drawing.Unit.Inch(0.30003929138183594));
            this.textBox47.Name = "textBox47";
            this.textBox47.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.59999942779541), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox47.Style.Font.Bold = true;
            this.textBox47.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox47.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox47.Value = "PERMINTAAN PEMBELIAN (PR) - NON MEDIS";
            // 
            // textBox25
            // 
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.3799991607666016), Telerik.Reporting.Drawing.Unit.Inch(0.77007859945297241));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4999217987060547), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox25.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox25.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox25.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox25.Style.Font.Bold = true;
            this.textBox25.Style.Font.Name = "Tahoma";
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox25.Value = "=TransactionNo";
            // 
            // textBox29
            // 
            this.textBox29.Format = "{0:dd/MM/yyyy}";
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.87999963760376), Telerik.Reporting.Drawing.Unit.Inch(0.77007865905761719));
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1200003623962402), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox29.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox29.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox29.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox29.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox29.Style.Font.Bold = true;
            this.textBox29.Style.Font.Name = "Tahoma";
            this.textBox29.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox29.Value = "=TransactionDate";
            // 
            // textBox28
            // 
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.3799991607666016), Telerik.Reporting.Drawing.Unit.Inch(0.56999999284744263));
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4999209642410278), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox28.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox28.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox28.Style.Font.Bold = true;
            this.textBox28.Style.Font.Name = "Tahoma";
            this.textBox28.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox28.Value = "Nomor";
            // 
            // textBox30
            // 
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.87999963760376), Telerik.Reporting.Drawing.Unit.Inch(0.56999999284744263));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1200000047683716), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox30.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox30.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox30.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox30.Style.Font.Bold = true;
            this.textBox30.Style.Font.Name = "Tahoma";
            this.textBox30.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox30.Value = "Tanggal";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.2197251319885254), Telerik.Reporting.Drawing.Unit.Inch(0.05646967887878418));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.78027456998825073), Telerik.Reporting.Drawing.Unit.Inch(0.24349093437194824));
            this.textBox13.Style.Font.Bold = true;
            this.textBox13.Style.Font.Name = "Tahoma";
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14);
            this.textBox13.Style.Font.Underline = false;
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox13.Value = "=Ket";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.17003941535949707);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox35,
            this.textBox34,
            this.textBox33,
            this.textBox31,
            this.textBox7});
            this.detail.Name = "detail";
            // 
            // textBox35
            // 
            this.textBox35.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.1189866065979), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox35.Name = "textBox35";
            this.textBox35.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.88101321458816528), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox35.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox35.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox35.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox35.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox35.Style.Font.Name = "Tahoma";
            this.textBox35.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox35.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox35.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox35.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox35.Value = "=SRItemUnit";
            // 
            // textBox34
            // 
            this.textBox34.Format = "{0:N2}";
            this.textBox34.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.21882963180542), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox34.Name = "textBox34";
            this.textBox34.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.89662724733352661), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox34.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox34.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox34.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox34.Style.Font.Name = "Tahoma";
            this.textBox34.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox34.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox34.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox34.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox34.Value = "=Quantity";
            // 
            // textBox33
            // 
            this.textBox33.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.700039267539978), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.5187115669250488), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox33.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox33.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox33.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox33.Style.Font.Name = "Tahoma";
            this.textBox33.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox33.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox33.Value = "=ItemName";
            // 
            // textBox31
            // 
            this.textBox31.Format = "{0:#\".\"}";
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.34999990463256836), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox31.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox31.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox31.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox31.Style.Font.Name = "Tahoma";
            this.textBox31.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox31.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox31.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox31.Value = "= RowNumber()";
            // 
            // textBox7
            // 
            this.textBox7.Format = "{0:N0}";
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.55007839202880859), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox7.Name = "textBox34";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1498819589614868), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox7.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox7.Style.Font.Name = "Tahoma";
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox7.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox7.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox7.Value = "=ItemId";
            // 
            // reportHeaderSection1
            // 
            this.reportHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.4399210512638092);
            this.reportHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox22,
            this.textBox20,
            this.textBox19,
            this.textBox18,
            this.textBox3});
            this.reportHeaderSection1.Name = "reportHeaderSection1";
            this.reportHeaderSection1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.reportHeaderSection1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            // 
            // textBox22
            // 
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985), Telerik.Reporting.Drawing.Unit.Inch(0.099999904632568359));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.3500000536441803), Telerik.Reporting.Drawing.Unit.Inch(0.339921236038208));
            this.textBox22.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox22.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox22.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox22.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox22.Style.Font.Bold = true;
            this.textBox22.Style.Font.Name = "Tahoma";
            this.textBox22.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox22.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox22.Value = "No";
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.700039267539978), Telerik.Reporting.Drawing.Unit.Inch(0.099999904632568359));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.5187110900878906), Telerik.Reporting.Drawing.Unit.Inch(0.339921236038208));
            this.textBox20.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox20.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox20.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox20.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox20.Style.Font.Bold = true;
            this.textBox20.Style.Font.Name = "Tahoma";
            this.textBox20.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox20.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox20.Value = "Nama & Spesifikasi Barang";
            // 
            // textBox19
            // 
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.21882963180542), Telerik.Reporting.Drawing.Unit.Inch(0.099999904632568359));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.89662724733352661), Telerik.Reporting.Drawing.Unit.Inch(0.339921236038208));
            this.textBox19.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox19.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox19.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox19.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox19.Style.Font.Bold = true;
            this.textBox19.Style.Font.Name = "Tahoma";
            this.textBox19.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox19.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox19.Value = "Jumlah";
            // 
            // textBox18
            // 
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.1189866065979), Telerik.Reporting.Drawing.Unit.Inch(0.099999904632568359));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.88101321458816528), Telerik.Reporting.Drawing.Unit.Inch(0.339921236038208));
            this.textBox18.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox18.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox18.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox18.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox18.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox18.Style.Font.Bold = true;
            this.textBox18.Style.Font.Name = "Tahoma";
            this.textBox18.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox18.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox18.Value = "Satuan";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.55007869005203247), Telerik.Reporting.Drawing.Unit.Inch(0.099999904632568359));
            this.textBox3.Name = "textBox19";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1498817205429077), Telerik.Reporting.Drawing.Unit.Inch(0.339921236038208));
            this.textBox3.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox3.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox3.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox3.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Name = "Tahoma";
            this.textBox3.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = "Kd Barang";
            // 
            // group1
            // 
            this.group1.GroupFooter = this.labelsGroupFooter;
            this.group1.GroupHeader = this.groupHeaderSection1;
            this.group1.Name = "group1";
            // 
            // labelsGroupFooter
            // 
            this.labelsGroupFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699);
            this.labelsGroupFooter.Name = "labelsGroupFooter";
            this.labelsGroupFooter.Style.Font.Name = "Tahoma";
            this.labelsGroupFooter.Style.Visible = false;
            // 
            // groupHeaderSection1
            // 
            this.groupHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.groupHeaderSection1.Name = "groupHeaderSection1";
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
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(1.4379562139511108);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox2,
            this.textBox5,
            this.textBox12});
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.21882963180542), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5965479612350464), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox2.Style.Font.Name = "Tahoma";
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox2.Value = "Mengetahui,";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985), Telerik.Reporting.Drawing.Unit.Inch(1.1379560232162476));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.696627140045166), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox5.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox5.Style.Font.Name = "Tahoma";
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.Value = "Bagian Logistik";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.21882963180542), Telerik.Reporting.Drawing.Unit.Inch(1.1379560232162476));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.696627140045166), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox12.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox12.Style.Font.Name = "Tahoma";
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox12.Value = "Direktur Umum";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.702083170413971), Telerik.Reporting.Drawing.Unit.Inch(1));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921382963657379), Telerik.Reporting.Drawing.Unit.Inch(0.19999988377094269));
            this.textBox1.Style.Font.Bold = false;
            this.textBox1.Style.Font.Name = "Tahoma";
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox1.Value = ":";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.23188622295856476), Telerik.Reporting.Drawing.Unit.Inch(1));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.4701181948184967), Telerik.Reporting.Drawing.Unit.Inch(0.19999988377094269));
            this.textBox4.Style.Font.Bold = false;
            this.textBox4.Style.Font.Name = "Tahoma";
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox4.Value = "From";
            // 
            // textBox8
            // 
            this.textBox8.Format = "{0:dd/MM/yyyy}";
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.80208331346511841), Telerik.Reporting.Drawing.Unit.Inch(1));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.8056144714355469), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox8.Style.Font.Bold = false;
            this.textBox8.Style.Font.Name = "Tahoma";
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox8.Value = "=FromServiceUnitName";
            // 
            // textBox14
            // 
            this.textBox14.Format = "{0:dd/MM/yyyy}";
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.80208331346511841), Telerik.Reporting.Drawing.Unit.Inch(0.79992121458053589));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.8056144714355469), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox14.Style.Font.Bold = false;
            this.textBox14.Style.Font.Name = "Tahoma";
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox14.Value = "=ServiceUnitName";
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.23188622295856476), Telerik.Reporting.Drawing.Unit.Inch(0.79992121458053589));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.4701181948184967), Telerik.Reporting.Drawing.Unit.Inch(0.19999988377094269));
            this.textBox15.Style.Font.Bold = false;
            this.textBox15.Style.Font.Name = "Tahoma";
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox15.Value = "To";
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.702083170413971), Telerik.Reporting.Drawing.Unit.Inch(0.79992121458053589));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921382963657379), Telerik.Reporting.Drawing.Unit.Inch(0.19999988377094269));
            this.textBox16.Style.Font.Bold = false;
            this.textBox16.Style.Font.Name = "Tahoma";
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox16.Value = ":";
            // 
            // PurchaseRequestNonMedis
            // 
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            this.group1});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection1,
            this.labelsGroupFooter,
            this.pageHeader,
            this.detail,
            this.reportHeaderSection1,
            this.pageFooterSection1,
            this.reportFooterSection1});
            this.Name = "PurchaseRequest";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Cm(0.5);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(0.5);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Name = "Tahoma";
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(8.11886978149414);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox36;
        private ReportHeaderSection reportHeaderSection1;
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
        private Group group1;
        private GroupFooterSection labelsGroupFooter;
        private GroupHeaderSection groupHeaderSection1;
        private PageFooterSection pageFooterSection1;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox3;
        private ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox47;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox16;
    }
}