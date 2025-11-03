namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Inventory.Procurement.RSUTAMA
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class PurchaseOrderWithoutPrice
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.textBox34 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox33 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.shape5 = new Telerik.Reporting.Shape();
            this.shape2 = new Telerik.Reporting.Shape();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox40 = new Telerik.Reporting.TextBox();
            this.textBox35 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox38 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.textBox53 = new Telerik.Reporting.TextBox();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.txtKaSubBag = new Telerik.Reporting.TextBox();
            this.txtKaBag = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(2.2000000476837158);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox15,
            this.textBox25,
            this.textBox27,
            this.textBox28,
            this.textBox31,
            this.textBox34,
            this.textBox1,
            this.textBox33,
            this.textBox3,
            this.textBox32,
            this.shape5,
            this.shape2,
            this.textBox17,
            this.textBox20,
            this.textBox2,
            this.textBox40,
            this.textBox35});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.pageHeader.Style.Visible = true;
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.40000024437904358), Telerik.Reporting.Drawing.Unit.Inch(1.1999999284744263));
            this.textBox15.Name = "textBox37";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.8978385925292969), Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134));
            this.textBox15.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox15.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox15.Style.Font.Bold = true;
            this.textBox15.Style.Font.Name = "Tahoma";
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(13);
            this.textBox15.Value = "SURAT PESANAN";
            // 
            // textBox25
            // 
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.0979175567626953), Telerik.Reporting.Drawing.Unit.Inch(0.30007871985435486));
            this.textBox25.Name = "textBox3";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1999211311340332), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox25.Style.Font.Bold = false;
            this.textBox25.Style.Font.Name = "Tahoma";
            this.textBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox25.Value = "=SupplierName";
            // 
            // textBox27
            // 
            this.textBox27.Format = "Telp.";
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.1000003814697266), Telerik.Reporting.Drawing.Unit.Inch(0.70023602247238159));
            this.textBox27.Name = "textBox3";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1999211311340332), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox27.Style.Font.Bold = false;
            this.textBox27.Style.Font.Name = "Tahoma";
            this.textBox27.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox27.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox27.Value = "=PhoneNo";
            // 
            // textBox28
            // 
            this.textBox28.Format = "Fax.";
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.1000003814697266), Telerik.Reporting.Drawing.Unit.Inch(0.9003148078918457));
            this.textBox28.Name = "textBox3";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1999211311340332), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox28.Style.Font.Bold = false;
            this.textBox28.Style.Font.Name = "Tahoma";
            this.textBox28.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox28.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox28.Value = "=FaxNo";
            // 
            // textBox31
            // 
            this.textBox31.Format = "{0:dd-MMMM-yyyy}";
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.2000002861022949), Telerik.Reporting.Drawing.Unit.Inch(0.099999986588954926));
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0999213457107544), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox31.Style.Font.Bold = false;
            this.textBox31.Style.Font.Name = "Tahoma";
            this.textBox31.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox31.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox31.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox31.Value = "=now()";
            // 
            // textBox34
            // 
            this.textBox34.Format = "No. {0}";
            this.textBox34.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.0979166030883789), Telerik.Reporting.Drawing.Unit.Inch(1.5));
            this.textBox34.Name = "textBox3";
            this.textBox34.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1999211311340332), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox34.Style.Font.Bold = false;
            this.textBox34.Style.Font.Name = "Tahoma";
            this.textBox34.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox34.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox34.Value = "=TransactionNo";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = false;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.899998664855957), Telerik.Reporting.Drawing.Unit.Inch(1.8000000715255737));
            this.textBox1.Name = "textBox16";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.59783840179443359), Telerik.Reporting.Drawing.Unit.Inch(0.21089856326580048));
            this.textBox1.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox1.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox1.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox1.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Tahoma";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.TextWrap = false;
            this.textBox1.Value = "Satuan";
            // 
            // textBox33
            // 
            this.textBox33.CanGrow = false;
            this.textBox33.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(-1.6556846560433769E-08), Telerik.Reporting.Drawing.Unit.Inch(1.8000000715255737));
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.3000786304473877), Telerik.Reporting.Drawing.Unit.Inch(0.21089856326580048));
            this.textBox33.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox33.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox33.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox33.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox33.Style.Font.Bold = true;
            this.textBox33.Style.Font.Name = "Tahoma";
            this.textBox33.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox33.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox33.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox33.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox33.TextWrap = false;
            this.textBox33.Value = "No.";
            // 
            // textBox3
            // 
            this.textBox3.CanGrow = false;
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.3000001907348633), Telerik.Reporting.Drawing.Unit.Inch(1.8000000715255737));
            this.textBox3.Name = "textBox35";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.49297726154327393), Telerik.Reporting.Drawing.Unit.Inch(0.21089856326580048));
            this.textBox3.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox3.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox3.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox3.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Name = "Tahoma";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.TextWrap = false;
            this.textBox3.Value = "Jumlah";
            // 
            // textBox32
            // 
            this.textBox32.CanGrow = false;
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.39996048808097839), Telerik.Reporting.Drawing.Unit.Inch(1.7999602556228638));
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.9000396728515625), Telerik.Reporting.Drawing.Unit.Inch(0.21089856326580048));
            this.textBox32.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox32.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox32.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox32.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox32.Style.Font.Bold = true;
            this.textBox32.Style.Font.Name = "Tahoma";
            this.textBox32.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox32.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox32.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox32.TextWrap = false;
            this.textBox32.Value = "Nama Item";
            // 
            // shape5
            // 
            this.shape5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(1.6999999284744263));
            this.shape5.Name = "shape5";
            this.shape5.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(18.790304183959961), Telerik.Reporting.Drawing.Unit.Cm(0.25400015711784363));
            this.shape5.Style.Font.Name = "Times New Roman";
            this.shape5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            // 
            // shape2
            // 
            this.shape2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(2.1000001430511475));
            this.shape2.Name = "shape2";
            this.shape2.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(18.7958984375), Telerik.Reporting.Drawing.Unit.Cm(0.19999989867210388));
            this.shape2.Style.Font.Name = "Times New Roman";
            this.shape2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            // 
            // textBox17
            // 
            this.textBox17.Format = "";
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(1.5));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.6999602317810059), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox17.Style.Font.Bold = false;
            this.textBox17.Style.Font.Name = "Tahoma";
            this.textBox17.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox17.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox17.Value = "Mohon dikirimkan kepada kami barang-barang sebagai berikut :";
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.0979170799255371), Telerik.Reporting.Drawing.Unit.Inch(0.500157356262207));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1999211311340332), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox20.Style.Font.Bold = false;
            this.textBox20.Style.Font.Name = "Tahoma";
            this.textBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox20.Value = "=StreetName";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = false;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.4000000953674316), Telerik.Reporting.Drawing.Unit.Inch(1.7999602556228638));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.8000005483627319), Telerik.Reporting.Drawing.Unit.Inch(0.21089856326580048));
            this.textBox2.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox2.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox2.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox2.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Name = "Tahoma";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.TextWrap = false;
            this.textBox2.Value = "Spesifikasi";
            // 
            // textBox40
            // 
            this.textBox40.Format = "";
            this.textBox40.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.8699214458465576), Telerik.Reporting.Drawing.Unit.Inch(0.099999986588954926));
            this.textBox40.Name = "textBox40";
            this.textBox40.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.2999212741851807), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox40.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox40.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox40.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox40.Value = "";
            // 
            // textBox35
            // 
            this.textBox35.Format = "";
            this.textBox35.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.1699213981628418), Telerik.Reporting.Drawing.Unit.Inch(0.15000009536743164));
            this.textBox35.Name = "textBox35";
            this.textBox35.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.029999999329447746), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.textBox35.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox35.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox35.Value = ",";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.22499999403953552);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox38,
            this.textBox8,
            this.textBox10,
            this.textBox9,
            this.textBox5});
            this.detail.Name = "detail";
            // 
            // textBox38
            // 
            this.textBox38.CanGrow = false;
            this.textBox38.Format = "{0:N0}.";
            this.textBox38.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(-1.6556846560433769E-08), Telerik.Reporting.Drawing.Unit.Inch(0.00020345051598269492));
            this.textBox38.Name = "textBox38";
            this.textBox38.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.3000786304473877), Telerik.Reporting.Drawing.Unit.Inch(0.21003961563110352));
            this.textBox38.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox38.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox38.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox38.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox38.Style.Font.Name = "Tahoma";
            this.textBox38.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox38.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox38.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox38.TextWrap = false;
            this.textBox38.Value = "=RowNumber()";
            // 
            // textBox8
            // 
            this.textBox8.CanGrow = true;
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.39996048808097839), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox8.Name = "textBox37";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.9000396728515625), Telerik.Reporting.Drawing.Unit.Inch(0.22492118179798126));
            this.textBox8.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox8.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox8.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox8.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox8.Style.Font.Name = "Tahoma";
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox8.TextWrap = true;
            this.textBox8.Value = "=ItemName";
            // 
            // textBox10
            // 
            this.textBox10.CanGrow = true;
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.9001960754394531), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox10.Name = "textBox37";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.59783810377120972), Telerik.Reporting.Drawing.Unit.Inch(0.21000020205974579));
            this.textBox10.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox10.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox10.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox10.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox10.Style.Font.Name = "Tahoma";
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox10.TextWrap = false;
            this.textBox10.Value = "=SRItemUnit";
            // 
            // textBox9
            // 
            this.textBox9.CanGrow = false;
            this.textBox9.Format = "{0:N2}";
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.3000001907348633), Telerik.Reporting.Drawing.Unit.Inch(7.8837074397597462E-05));
            this.textBox9.Name = "textBox37";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.49305576086044312), Telerik.Reporting.Drawing.Unit.Inch(0.20996077358722687));
            this.textBox9.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox9.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox9.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox9.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox9.Style.Font.Name = "Tahoma";
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox9.TextWrap = false;
            this.textBox9.Value = "=Quantity1";
            // 
            // textBox5
            // 
            this.textBox5.CanGrow = true;
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.3999998569488525), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.8000003099441528), Telerik.Reporting.Drawing.Unit.Inch(0.22492118179798126));
            this.textBox5.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox5.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox5.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox5.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox5.Style.Font.Name = "Tahoma";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox5.TextWrap = true;
            this.textBox5.Value = "=Specification";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0520833358168602);
            this.pageFooter.Name = "pageFooter";
            this.pageFooter.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            // 
            // textBox53
            // 
            this.textBox53.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.1000003814697266), Telerik.Reporting.Drawing.Unit.Inch(1.2500399351119995));
            this.textBox53.Name = "textBox3";
            this.textBox53.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1021625995635986), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox53.Style.Font.Bold = false;
            this.textBox53.Style.Font.Name = "Tahoma";
            this.textBox53.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox53.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox53.Value = "Menyetujui,";
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(2.8750002384185791);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox53,
            this.textBox4,
            this.textBox16,
            this.txtKaSubBag,
            this.txtKaBag,
            this.textBox18,
            this.textBox19,
            this.textBox6});
            this.reportFooterSection1.Name = "reportFooterSection1";
            this.reportFooterSection1.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.975000262260437));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.00003981590271), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox4.Style.Font.Bold = false;
            this.textBox4.Style.Font.Name = "Tahoma";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox4.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox4.Value = "Atas bantuannya kami ucapkan terima kasih.";
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.15972220897674561), Telerik.Reporting.Drawing.Unit.Inch(1.2500402927398682));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.5402774810791016), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox16.Style.Font.Bold = false;
            this.textBox16.Style.Font.Name = "Tahoma";
            this.textBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox16.Value = "Pemesan,";
            // 
            // txtKaSubBag
            // 
            this.txtKaSubBag.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.15972204506397247), Telerik.Reporting.Drawing.Unit.Inch(2.1500394344329834));
            this.txtKaSubBag.Name = "txtKaSubBag";
            this.txtKaSubBag.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.5402779579162598), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.txtKaSubBag.Style.Font.Bold = false;
            this.txtKaSubBag.Style.Font.Name = "Tahoma";
            this.txtKaSubBag.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.txtKaSubBag.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtKaSubBag.Value = "=PharmacyHead";
            // 
            // txtKaBag
            // 
            this.txtKaBag.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.0979170799255371), Telerik.Reporting.Drawing.Unit.Inch(2.35003924369812));
            this.txtKaBag.Name = "txtKaBag";
            this.txtKaBag.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1021625995635986), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.txtKaBag.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtKaBag.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.txtKaBag.Style.Font.Bold = false;
            this.txtKaBag.Style.Font.Name = "Tahoma";
            this.txtKaBag.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.txtKaBag.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtKaBag.Value = "=PicPurchasing";
            // 
            // textBox18
            // 
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1597222238779068), Telerik.Reporting.Drawing.Unit.Inch(2.3500397205352783));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.5402779579162598), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox18.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox18.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox18.Style.Font.Bold = false;
            this.textBox18.Style.Font.Name = "Tahoma";
            this.textBox18.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox18.Value = "=PharmacyHeadLicenseNo";
            // 
            // textBox19
            // 
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.0750003382563591));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4), Telerik.Reporting.Drawing.Unit.Inch(0.70000004768371582));
            this.textBox19.Style.Font.Bold = false;
            this.textBox19.Style.Font.Name = "Tahoma";
            this.textBox19.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox19.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox19.Value = "=Notes";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(2.575000524520874));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.7000000476837158), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox6.Style.Font.Bold = false;
            this.textBox6.Style.Font.Italic = true;
            this.textBox6.Style.Font.Name = "Tahoma";
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox6.Style.Font.Underline = false;
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox6.Value = "=UserName";
            // 
            // PurchaseOrderWithoutPrice
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail,
            this.pageFooter,
            this.reportFooterSection1});
            this.Name = "PurchaseOrderWithoutPrice";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.3999996185302734);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox textBox38;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox28;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox textBox34;
        private Telerik.Reporting.TextBox textBox53;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox33;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox32;
        private Shape shape5;
        private Shape shape2;
        private ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox txtKaSubBag;
        private Telerik.Reporting.TextBox txtKaBag;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox40;
        private Telerik.Reporting.TextBox textBox35;
    }
}