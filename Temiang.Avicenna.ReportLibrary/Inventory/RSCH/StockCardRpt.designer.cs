namespace Temiang.Avicenna.ReportLibrary.Inventory.RSCH
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class StockCardRpt
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.detail = new Telerik.Reporting.DetailSection();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.ItemID = new Telerik.Reporting.Group();
            this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();
            this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox37 = new Telerik.Reporting.TextBox();
            this.textBox38 = new Telerik.Reporting.TextBox();
            this.textBox39 = new Telerik.Reporting.TextBox();
            this.textBox40 = new Telerik.Reporting.TextBox();
            this.textBox41 = new Telerik.Reporting.TextBox();
            this.textBox42 = new Telerik.Reporting.TextBox();
            this.textBox43 = new Telerik.Reporting.TextBox();
            this.textBox44 = new Telerik.Reporting.TextBox();
            this.textBox45 = new Telerik.Reporting.TextBox();
            this.textBox46 = new Telerik.Reporting.TextBox();
            this.textBox47 = new Telerik.Reporting.TextBox();
            this.textBox48 = new Telerik.Reporting.TextBox();
            this.textBox49 = new Telerik.Reporting.TextBox();
            this.textBox50 = new Telerik.Reporting.TextBox();
            this.textBox51 = new Telerik.Reporting.TextBox();
            this.textBox52 = new Telerik.Reporting.TextBox();
            this.textBox53 = new Telerik.Reporting.TextBox();
            this.textBox54 = new Telerik.Reporting.TextBox();
            this.textBox55 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699);
            this.detail.Name = "detail";
            this.detail.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699);
            this.pageFooter.Name = "pageFooter";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699);
            this.pageHeader.Name = "pageHeader";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2), Telerik.Reporting.Drawing.Unit.Inch(0.14791671931743622));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.9000002145767212), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox1.Style.Font.Name = "Tahoma";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "KARTU PERSEDIAAN GUDANG";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.1000003814697266), Telerik.Reporting.Drawing.Unit.Inch(0.34799551963806152));
            this.textBox3.Name = "textBox2";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.4999992847442627), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox3.Style.Font.Name = "Tahoma";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = "=ItemID";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.1000003814697266), Telerik.Reporting.Drawing.Unit.Inch(0.14791655540466309));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.4999997615814209), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.Style.Font.Name = "Tahoma";
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.Value = "=ItemName";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5), Telerik.Reporting.Drawing.Unit.Inch(0.14791655540466309));
            this.textBox5.Name = "textBox2";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Font.Name = "Tahoma";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox5.Value = ":";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4), Telerik.Reporting.Drawing.Unit.Inch(0.13968364894390106));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99992084503173828), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Name = "Tahoma";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "Nama Barang";
            // 
            // ItemID
            // 
            this.ItemID.GroupFooter = this.groupFooterSection1;
            this.ItemID.GroupHeader = this.groupHeaderSection1;
            this.ItemID.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("=ItemID")});
            this.ItemID.Name = "ItemID";
            // 
            // groupFooterSection1
            // 
            this.groupFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699);
            this.groupFooterSection1.Name = "groupFooterSection1";
            this.groupFooterSection1.PageBreak = Telerik.Reporting.PageBreak.After;
            this.groupFooterSection1.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            // 
            // groupHeaderSection1
            // 
            this.groupHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(1.3479167222976685);
            this.groupHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox5,
            this.textBox2,
            this.textBox7,
            this.textBox22,
            this.textBox24,
            this.textBox25,
            this.textBox26,
            this.textBox28,
            this.textBox29,
            this.textBox30,
            this.textBox31,
            this.textBox3,
            this.textBox14,
            this.textBox1,
            this.textBox37,
            this.textBox38,
            this.textBox39,
            this.textBox40,
            this.textBox41,
            this.textBox42,
            this.textBox43,
            this.textBox44,
            this.textBox45,
            this.textBox46,
            this.textBox47,
            this.textBox48,
            this.textBox49,
            this.textBox50,
            this.textBox51,
            this.textBox52,
            this.textBox53,
            this.textBox54,
            this.textBox55});
            this.groupHeaderSection1.Name = "groupHeaderSection1";
            this.groupHeaderSection1.PageBreak = Telerik.Reporting.PageBreak.None;
            this.groupHeaderSection1.PrintOnEveryPage = true;
            this.groupHeaderSection1.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            // 
            // textBox22
            // 
            this.textBox22.Format = "Min. {0:0}";
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.1000003814697266), Telerik.Reporting.Drawing.Unit.Inch(0.748153030872345));
            this.textBox22.Name = "textBox7";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.79107379913330078), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox22.Style.Font.Name = "Tahoma";
            this.textBox22.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox22.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox22.Value = "=Minimum";
            // 
            // textBox24
            // 
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1020447388291359), Telerik.Reporting.Drawing.Unit.Inch(1.0479955673217773));
            this.textBox24.Name = "textBox2";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.756092369556427), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox24.Style.Font.Name = "Tahoma";
            this.textBox24.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox24.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox24.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox24.Value = "Lokasi";
            // 
            // textBox25
            // 
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.85825586318969727), Telerik.Reporting.Drawing.Unit.Inch(1.0479955673217773));
            this.textBox25.Name = "textBox2";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox25.Style.Font.Name = "Tahoma";
            this.textBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox25.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox25.Value = ":";
            // 
            // textBox26
            // 
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.96026068925857544), Telerik.Reporting.Drawing.Unit.Inch(0.84791678190231323));
            this.textBox26.Name = "textBox7";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.939739465713501), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox26.Style.Font.Name = "Tahoma";
            this.textBox26.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox26.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox26.Value = "=ServiceUnitName";
            // 
            // textBox28
            // 
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.10003995895385742), Telerik.Reporting.Drawing.Unit.Inch(0.34799543023109436));
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7999601364135742), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox28.Style.Font.Name = "Tahoma";
            this.textBox28.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox28.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox28.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox28.Value = "=AddressLine1";
            // 
            // textBox29
            // 
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.85821598768234253), Telerik.Reporting.Drawing.Unit.Inch(0.84791678190231323));
            this.textBox29.Name = "textBox2";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox29.Style.Font.Name = "Tahoma";
            this.textBox29.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox29.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox29.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox29.Value = ":";
            // 
            // textBox30
            // 
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4), Telerik.Reporting.Drawing.Unit.Inch(0.34799537062644958));
            this.textBox30.Name = "textBox2";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99992084503173828), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox30.Style.Font.Name = "Tahoma";
            this.textBox30.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox30.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox30.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox30.Value = "Kode Barang";
            // 
            // textBox31
            // 
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5), Telerik.Reporting.Drawing.Unit.Inch(0.34799537062644958));
            this.textBox31.Name = "textBox2";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox31.Style.Font.Name = "Tahoma";
            this.textBox31.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox31.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox31.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox31.Value = ":";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.96026068925857544), Telerik.Reporting.Drawing.Unit.Inch(1.0479956865310669));
            this.textBox14.Name = "textBox7";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.939739465713501), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox14.Style.Font.Name = "Tahoma";
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox14.Value = "=LocationName";
            // 
            // textBox37
            // 
            this.textBox37.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1020447388291359), Telerik.Reporting.Drawing.Unit.Inch(0.14791662991046906));
            this.textBox37.Name = "textBox37";
            this.textBox37.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7979553937911987), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox37.Style.Font.Name = "Tahoma";
            this.textBox37.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox37.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox37.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox37.Value = "=HealthcareName";
            // 
            // textBox38
            // 
            this.textBox38.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1020447388291359), Telerik.Reporting.Drawing.Unit.Inch(0.54807424545288086));
            this.textBox38.Name = "textBox38";
            this.textBox38.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7979553937911987), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox38.Style.Font.Name = "Tahoma";
            this.textBox38.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox38.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox38.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox38.Value = "=AddressLine2";
            // 
            // textBox39
            // 
            this.textBox39.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1020447388291359), Telerik.Reporting.Drawing.Unit.Inch(0.84791678190231323));
            this.textBox39.Name = "textBox39";
            this.textBox39.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.756092369556427), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox39.Style.Font.Name = "Tahoma";
            this.textBox39.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox39.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox39.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox39.Value = "Service Unit";
            // 
            // textBox40
            // 
            this.textBox40.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2), Telerik.Reporting.Drawing.Unit.Inch(0.34799551963806152));
            this.textBox40.Name = "textBox40";
            this.textBox40.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.9000002145767212), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox40.Style.Font.Bold = true;
            this.textBox40.Style.Font.Name = "Tahoma";
            this.textBox40.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox40.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox40.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox40.Value = "=ItemID";
            // 
            // textBox41
            // 
            this.textBox41.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.1000003814697266), Telerik.Reporting.Drawing.Unit.Inch(0.54807424545288086));
            this.textBox41.Name = "textBox41";
            this.textBox41.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.49099478125572205), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox41.Style.Font.Name = "Tahoma";
            this.textBox41.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox41.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox41.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox41.Value = "=SRItemUnit";
            // 
            // textBox42
            // 
            this.textBox42.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5), Telerik.Reporting.Drawing.Unit.Inch(0.54807424545288086));
            this.textBox42.Name = "textBox42";
            this.textBox42.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox42.Style.Font.Name = "Tahoma";
            this.textBox42.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox42.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox42.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox42.Value = ":";
            // 
            // textBox43
            // 
            this.textBox43.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4), Telerik.Reporting.Drawing.Unit.Inch(0.54807424545288086));
            this.textBox43.Name = "textBox43";
            this.textBox43.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99992084503173828), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox43.Style.Font.Name = "Tahoma";
            this.textBox43.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox43.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox43.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox43.Value = "Satuan Kecil";
            // 
            // textBox44
            // 
            this.textBox44.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.5910739898681641), Telerik.Reporting.Drawing.Unit.Inch(0.54807430505752563));
            this.textBox44.Name = "textBox44";
            this.textBox44.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.30000081658363342), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox44.Style.Font.Name = "Tahoma";
            this.textBox44.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox44.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox44.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox44.Value = "Besar";
            // 
            // textBox45
            // 
            this.textBox45.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.8911538124084473), Telerik.Reporting.Drawing.Unit.Inch(0.54807430505752563));
            this.textBox45.Name = "textBox45";
            this.textBox45.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox45.Style.Font.Name = "Tahoma";
            this.textBox45.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox45.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox45.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox45.Value = ":";
            // 
            // textBox46
            // 
            this.textBox46.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.0000004768371582), Telerik.Reporting.Drawing.Unit.Inch(0.54807430505752563));
            this.textBox46.Name = "textBox46";
            this.textBox46.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.49099478125572205), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox46.Style.Font.Name = "Tahoma";
            this.textBox46.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox46.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox46.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox46.Value = "=SRPurchaseUnit";
            // 
            // textBox47
            // 
            this.textBox47.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.0000014305114746), Telerik.Reporting.Drawing.Unit.Inch(0.54807430505752563));
            this.textBox47.Name = "textBox47";
            this.textBox47.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.49099478125572205), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox47.Style.Font.Name = "Tahoma";
            this.textBox47.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox47.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox47.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox47.Value = "=ConversionFactor";
            // 
            // textBox48
            // 
            this.textBox48.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.9000015258789062), Telerik.Reporting.Drawing.Unit.Inch(0.54807430505752563));
            this.textBox48.Name = "textBox48";
            this.textBox48.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox48.Style.Font.Name = "Tahoma";
            this.textBox48.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox48.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox48.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox48.Value = ":";
            // 
            // textBox49
            // 
            this.textBox49.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.4910740852355957), Telerik.Reporting.Drawing.Unit.Inch(0.54807430505752563));
            this.textBox49.Name = "textBox49";
            this.textBox49.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.40884813666343689), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox49.Style.Font.Name = "Tahoma";
            this.textBox49.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox49.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox49.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox49.Value = "Faktor";
            // 
            // textBox50
            // 
            this.textBox50.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4), Telerik.Reporting.Drawing.Unit.Inch(0.748153030872345));
            this.textBox50.Name = "textBox50";
            this.textBox50.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99992084503173828), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox50.Style.Font.Name = "Tahoma";
            this.textBox50.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox50.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox50.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox50.Value = "Persediaan";
            // 
            // textBox51
            // 
            this.textBox51.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5), Telerik.Reporting.Drawing.Unit.Inch(0.748153030872345));
            this.textBox51.Name = "textBox51";
            this.textBox51.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.0999205932021141), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox51.Style.Font.Name = "Tahoma";
            this.textBox51.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox51.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox51.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox51.Value = ":";
            // 
            // textBox52
            // 
            this.textBox52.Format = "Max. {0:0}";
            this.textBox52.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.8911538124084473), Telerik.Reporting.Drawing.Unit.Inch(0.748153030872345));
            this.textBox52.Name = "textBox52";
            this.textBox52.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.79107379913330078), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox52.Style.Font.Name = "Tahoma";
            this.textBox52.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox52.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox52.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox52.Value = "=Maximum";
            // 
            // textBox53
            // 
            this.textBox53.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4), Telerik.Reporting.Drawing.Unit.Inch(0.94823199510574341));
            this.textBox53.Name = "textBox53";
            this.textBox53.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99992084503173828), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox53.Style.Font.Name = "Tahoma";
            this.textBox53.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox53.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox53.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox53.Value = "Saldo Awal";
            // 
            // textBox54
            // 
            this.textBox54.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5), Telerik.Reporting.Drawing.Unit.Inch(0.94823199510574341));
            this.textBox54.Name = "textBox54";
            this.textBox54.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.0999205932021141), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox54.Style.Font.Name = "Tahoma";
            this.textBox54.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox54.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox54.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox54.Value = ":";
            // 
            // textBox55
            // 
            this.textBox55.Format = "";
            this.textBox55.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.1000003814697266), Telerik.Reporting.Drawing.Unit.Inch(0.94823199510574341));
            this.textBox55.Name = "textBox55";
            this.textBox55.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.79107379913330078), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox55.Style.Font.Name = "Tahoma";
            this.textBox55.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox55.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox55.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox55.Value = "=Balance";
            // 
            // StockCardRpt
            // 
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            this.ItemID});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection1,
            this.groupFooterSection1,
            this.pageHeader,
            this.detail,
            this.pageFooter});
            this.Name = "StockCardRpt";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Inch(0.30000001192092896);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Inch(0.30000001192092896);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Inch(0.30000001192092896);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Inch(0.30000001192092896);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageSettings.PaperSize = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(21), Telerik.Reporting.Drawing.Unit.Cm(16.5));
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.6000003814697266);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private PageHeaderSection pageHeader;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox2;
        private Group ItemID;
        private GroupFooterSection groupFooterSection1;
        private GroupHeaderSection groupHeaderSection1;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox28;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox37;
        private Telerik.Reporting.TextBox textBox38;
        private Telerik.Reporting.TextBox textBox39;
        private Telerik.Reporting.TextBox textBox40;
        private Telerik.Reporting.TextBox textBox41;
        private Telerik.Reporting.TextBox textBox42;
        private Telerik.Reporting.TextBox textBox43;
        private Telerik.Reporting.TextBox textBox44;
        private Telerik.Reporting.TextBox textBox45;
        private Telerik.Reporting.TextBox textBox46;
        private Telerik.Reporting.TextBox textBox47;
        private Telerik.Reporting.TextBox textBox48;
        private Telerik.Reporting.TextBox textBox49;
        private Telerik.Reporting.TextBox textBox50;
        private Telerik.Reporting.TextBox textBox51;
        private Telerik.Reporting.TextBox textBox52;
        private Telerik.Reporting.TextBox textBox53;
        private Telerik.Reporting.TextBox textBox54;
        private Telerik.Reporting.TextBox textBox55;
    }
}