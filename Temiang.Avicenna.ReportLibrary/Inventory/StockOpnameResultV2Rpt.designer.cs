namespace Temiang.Avicenna.ReportLibrary.Inventory
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class StockOpnameResultV2Rpt
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.titleTextBox = new Telerik.Reporting.TextBox();
            this.txtTransactionNo = new Telerik.Reporting.TextBox();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.txtLocationName = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.txtTransactionDate = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.txtPageNo = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.textBox36 = new Telerik.Reporting.TextBox();
            this.textBox37 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.textBox33 = new Telerik.Reporting.TextBox();
            this.textBox35 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox34 = new Telerik.Reporting.TextBox();
            this.textBox39 = new Telerik.Reporting.TextBox();
            this.textBox41 = new Telerik.Reporting.TextBox();
            this.textBox44 = new Telerik.Reporting.TextBox();
            this.textBox46 = new Telerik.Reporting.TextBox();
            this.textBox48 = new Telerik.Reporting.TextBox();
            this.textBox50 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.group1 = new Telerik.Reporting.Group();
            this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();
            this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox38 = new Telerik.Reporting.TextBox();
            this.textBox40 = new Telerik.Reporting.TextBox();
            this.textBox43 = new Telerik.Reporting.TextBox();
            this.textBox45 = new Telerik.Reporting.TextBox();
            this.textBox47 = new Telerik.Reporting.TextBox();
            this.textBox49 = new Telerik.Reporting.TextBox();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.textBox42 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(0.86033779382705688);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.titleTextBox});
            this.pageHeader.Name = "pageHeader";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.1211490631103516), Telerik.Reporting.Drawing.Unit.Inch(0.29091215133666992));
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.8353379964828491), Telerik.Reporting.Drawing.Unit.Inch(0.21867126226425171));
            this.titleTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14);
            this.titleTextBox.StyleName = "Title";
            this.titleTextBox.Value = "Hasil Stock Opname";
            // 
            // txtTransactionNo
            // 
            this.txtTransactionNo.KeepTogether = false;
            this.txtTransactionNo.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.50555419921875), Telerik.Reporting.Drawing.Unit.Inch(0.016745461151003838));
            this.txtTransactionNo.Name = "txtTransactionNo";
            this.txtTransactionNo.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.5770828723907471), Telerik.Reporting.Drawing.Unit.Inch(0.1666666716337204));
            this.txtTransactionNo.Style.Font.Bold = true;
            // 
            // textBox28
            // 
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.026389030739665031), Telerik.Reporting.Drawing.Unit.Inch(0.016745461151003838));
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.25), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox28.Style.Font.Bold = true;
            this.textBox28.Value = "No";
            // 
            // txtLocationName
            // 
            this.txtLocationName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.50555419921875), Telerik.Reporting.Drawing.Unit.Inch(0.6104167103767395));
            this.txtLocationName.Name = "txtLocationName";
            this.txtLocationName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.5770828723907471), Telerik.Reporting.Drawing.Unit.Inch(0.1666666716337204));
            this.txtLocationName.Style.Font.Bold = true;
            this.txtLocationName.Value = "=LocationName";
            // 
            // textBox27
            // 
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.026389030739665031), Telerik.Reporting.Drawing.Unit.Inch(0.6104167103767395));
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.25), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox27.Style.Font.Bold = true;
            this.textBox27.Value = "Lokasi";
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2591067552566528), Telerik.Reporting.Drawing.Unit.Inch(0.84652751684188843));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7690801620483398), Telerik.Reporting.Drawing.Unit.Inch(0.49194663763046265));
            this.textBox20.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox20.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox20.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox20.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox20.Style.Font.Bold = true;
            this.textBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox20.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox20.Value = "Nama";
            // 
            // textBox21
            // 
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.63202309608459473), Telerik.Reporting.Drawing.Unit.Inch(0.84652751684188843));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.62700515985488892), Telerik.Reporting.Drawing.Unit.Inch(0.49194663763046265));
            this.textBox21.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox21.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox21.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox21.Style.Font.Bold = true;
            this.textBox21.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox21.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox21.Value = "Kode";
            // 
            // textBox22
            // 
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0055554709397256374), Telerik.Reporting.Drawing.Unit.Inch(0.84652751684188843));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.27638885378837585), Telerik.Reporting.Drawing.Unit.Inch(0.49194663763046265));
            this.textBox22.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox22.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox22.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox22.Style.Font.Bold = true;
            this.textBox22.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox22.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox22.Value = "No";
            // 
            // textBox19
            // 
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.72711181640625), Telerik.Reporting.Drawing.Unit.Inch(0.84652751684188843));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.4305555522441864), Telerik.Reporting.Drawing.Unit.Inch(0.49194663763046265));
            this.textBox19.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox19.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox19.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox19.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox19.Style.Font.Bold = true;
            this.textBox19.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox19.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox19.Value = "Selisih (Fisik - Sblm Appr)";
            // 
            // textBox18
            // 
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.0282657146453857), Telerik.Reporting.Drawing.Unit.Inch(0.84652751684188843));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.40686458349227905), Telerik.Reporting.Drawing.Unit.Inch(0.49194663763046265));
            this.textBox18.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox18.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox18.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox18.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox18.Style.Font.Bold = true;
            this.textBox18.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox18.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox18.Value = "Unit";
            // 
            // txtTransactionDate
            // 
            this.txtTransactionDate.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5054754018783569), Telerik.Reporting.Drawing.Unit.Inch(0.21041668951511383));
            this.txtTransactionDate.Name = "txtTransactionDate";
            this.txtTransactionDate.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.28125), Telerik.Reporting.Drawing.Unit.Inch(0.1875));
            this.txtTransactionDate.Style.Font.Bold = true;
            // 
            // textBox30
            // 
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.026389069855213165), Telerik.Reporting.Drawing.Unit.Inch(0.20071305334568024));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.83333331346511841), Telerik.Reporting.Drawing.Unit.Inch(0.1875));
            this.textBox30.Style.Font.Bold = true;
            this.textBox30.Value = "Tanggal";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.026389030739665031), Telerik.Reporting.Drawing.Unit.Inch(0.41041672229766846));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.25), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Value = "Halaman";
            // 
            // txtPageNo
            // 
            this.txtPageNo.Format = ": {0}";
            this.txtPageNo.KeepTogether = false;
            this.txtPageNo.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.50555419921875), Telerik.Reporting.Drawing.Unit.Inch(0.41041672229766846));
            this.txtPageNo.Name = "txtPageNo";
            this.txtPageNo.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.5770828723907471), Telerik.Reporting.Drawing.Unit.Inch(0.1666666716337204));
            this.txtPageNo.Style.Font.Bold = true;
            this.txtPageNo.Value = "=PageNo";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.25333359837532043);
            this.pageFooter.Name = "pageFooter";
            // 
            // textBox36
            // 
            this.textBox36.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.1576752662658691), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox36.Name = "textBox19";
            this.textBox36.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4347577095031738), Telerik.Reporting.Drawing.Unit.Inch(0.23880992829799652));
            this.textBox36.Style.Font.Bold = true;
            this.textBox36.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox36.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox36.Value = "Grand Total  :  ";
            // 
            // textBox37
            // 
            this.textBox37.Format = "{0:n0}";
            this.textBox37.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.457085132598877), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox37.Name = "textBox23";
            this.textBox37.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.80665463209152222), Telerik.Reporting.Drawing.Unit.Inch(0.25321510434150696));
            this.textBox37.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox37.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox37.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox37.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox37.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox37.Value = "=sum(Amount)";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2532939612865448);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox31,
            this.textBox32,
            this.textBox33,
            this.textBox35,
            this.textBox23,
            this.textBox17,
            this.textBox25,
            this.textBox34,
            this.textBox39,
            this.textBox41,
            this.textBox44,
            this.textBox46,
            this.textBox48,
            this.textBox50});
            this.detail.Name = "detail";
            // 
            // textBox31
            // 
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.00555551890283823), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.27638885378837585), Telerik.Reporting.Drawing.Unit.Inch(0.25321516394615173));
            this.textBox31.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox31.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox31.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox31.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox31.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox31.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox31.Value = "= RowNumber()";
            // 
            // textBox32
            // 
            this.textBox32.CanGrow = false;
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.63202309608459473), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.62700515985488892), Telerik.Reporting.Drawing.Unit.Inch(0.25321510434150696));
            this.textBox32.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox32.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox32.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox32.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox32.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox32.Value = "=ItemID";
            // 
            // textBox33
            // 
            this.textBox33.CanGrow = false;
            this.textBox33.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.259105920791626), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7690809965133667), Telerik.Reporting.Drawing.Unit.Inch(0.25321510434150696));
            this.textBox33.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox33.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox33.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox33.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox33.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox33.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox33.Value = "=ItemName";
            // 
            // textBox35
            // 
            this.textBox35.CanGrow = false;
            this.textBox35.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.0282266139984131), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox35.Name = "textBox35";
            this.textBox35.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.40686458349227905), Telerik.Reporting.Drawing.Unit.Inch(0.25321510434150696));
            this.textBox35.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox35.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox35.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox35.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox35.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox35.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox35.Value = "=SRItemUnit";
            // 
            // textBox23
            // 
            this.textBox23.Format = "{0:n0}";
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.7270731925964355), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.4305555522441864), Telerik.Reporting.Drawing.Unit.Inch(0.25321510434150696));
            this.textBox23.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox23.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox23.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox23.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox23.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox23.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox23.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox23.Value = "=QtyDiff";
            // 
            // textBox17
            // 
            this.textBox17.Format = "{0:n0}";
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.4351694583892822), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.4305555522441864), Telerik.Reporting.Drawing.Unit.Inch(0.25321510434150696));
            this.textBox17.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox17.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox17.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox17.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox17.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox17.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox17.Value = "=QtySystem";
            // 
            // textBox25
            // 
            this.textBox25.Format = "{0:n0}";
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.2964382171630859), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.4305555522441864), Telerik.Reporting.Drawing.Unit.Inch(0.25321510434150696));
            this.textBox25.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox25.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox25.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox25.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox25.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox25.Value = "=QtyActual";
            // 
            // textBox34
            // 
            this.textBox34.Format = "{0:n0}";
            this.textBox34.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.45712423324585), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox34.Name = "textBox23";
            this.textBox34.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.80661559104919434), Telerik.Reporting.Drawing.Unit.Inch(0.25321510434150696));
            this.textBox34.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox34.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox34.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox34.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox34.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox34.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox34.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox34.Value = "=Amount";
            // 
            // textBox39
            // 
            this.textBox39.Format = "{0:n0}";
            this.textBox39.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.09829568862915), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox39.Name = "textBox23";
            this.textBox39.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.67819511890411377), Telerik.Reporting.Drawing.Unit.Inch(0.25321510434150696));
            this.textBox39.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox39.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox39.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox39.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox39.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox39.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox39.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox39.Value = "=CostPrice";
            // 
            // textBox41
            // 
            this.textBox41.Format = "{0:n0}";
            this.textBox41.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.7765693664550781), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox41.Name = "textBox23";
            this.textBox41.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.68047505617141724), Telerik.Reporting.Drawing.Unit.Inch(0.25321510434150696));
            this.textBox41.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox41.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox41.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox41.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox41.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox41.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox41.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox41.Value = "=AmountDiff";
            // 
            // textBox44
            // 
            this.textBox44.Format = "{0:n0}";
            this.textBox44.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.8658046722412109), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox44.Name = "textBox44";
            this.textBox44.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.4305555522441864), Telerik.Reporting.Drawing.Unit.Inch(0.25321510434150696));
            this.textBox44.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox44.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox44.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox44.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox44.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox44.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox44.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox44.Value = "=InitialStock";
            // 
            // textBox46
            // 
            this.textBox46.Format = "{0:n0}";
            this.textBox46.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.1577072143554688), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox46.Name = "textBox46";
            this.textBox46.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.4305555522441864), Telerik.Reporting.Drawing.Unit.Inch(0.25321510434150696));
            this.textBox46.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox46.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox46.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox46.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox46.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox46.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox46.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox46.Value = "=QtyDiff2";
            // 
            // textBox48
            // 
            this.textBox48.Format = "{0:n0}";
            this.textBox48.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.5883407592773438), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox48.Name = "textBox48";
            this.textBox48.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.50987619161605835), Telerik.Reporting.Drawing.Unit.Inch(0.25321510434150696));
            this.textBox48.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox48.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox48.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox48.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox48.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox48.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox48.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox48.Value = "=Alasan";
            // 
            // textBox50
            // 
            this.textBox50.CanGrow = false;
            this.textBox50.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.2820231020450592), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox50.Name = "textBox50";
            this.textBox50.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.34992128610610962), Telerik.Reporting.Drawing.Unit.Inch(0.25321510434150696));
            this.textBox50.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox50.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox50.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox50.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox50.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox50.Value = "=ItemBin";
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.3041667938232422), Telerik.Reporting.Drawing.Unit.Inch(1.3812500238418579));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.60000002384185791), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox16.Value = "Gudang";
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.5958333015441895), Telerik.Reporting.Drawing.Unit.Inch(1.3812500238418579));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.60000002384185791), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox15.Value = "Gudang";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.0020833015441895), Telerik.Reporting.Drawing.Unit.Inch(1.3812500238418579));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.5), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox14.Value = "Satuan";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.5020835399627686), Telerik.Reporting.Drawing.Unit.Inch(1.3812500238418579));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.44999998807907104), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox13.Value = "Qty";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.595833420753479), Telerik.Reporting.Drawing.Unit.Inch(1.3812500238418579));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7999999523162842), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox12.Value = "Nama Item";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.50208348035812378), Telerik.Reporting.Drawing.Unit.Inch(1.3812500238418579));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox11.Value = "Kode Item";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0020834605675190687), Telerik.Reporting.Drawing.Unit.Inch(1.3812500238418579));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.44999998807907104), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox10.Value = "No";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.0333335399627686), Telerik.Reporting.Drawing.Unit.Inch(1.1104167699813843));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.625), Telerik.Reporting.Drawing.Unit.Inch(0.1666666716337204));
            this.textBox9.Value = "textBox7";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.0333335399627686), Telerik.Reporting.Drawing.Unit.Inch(0.8604167103767395));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.625), Telerik.Reporting.Drawing.Unit.Inch(0.1666666716337204));
            this.textBox8.Value = "textBox7";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.0333335399627686), Telerik.Reporting.Drawing.Unit.Inch(0.62083339691162109));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.625), Telerik.Reporting.Drawing.Unit.Inch(0.1666666716337204));
            this.textBox7.Value = "textBox7";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.55416679382324219), Telerik.Reporting.Drawing.Unit.Inch(1.1104167699813843));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.25), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox6.Value = "Gudang Tujuan";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.55416679382324219), Telerik.Reporting.Drawing.Unit.Inch(0.8604167103767395));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.25), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox5.Value = "Gudang Asal";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.55416679382324219), Telerik.Reporting.Drawing.Unit.Inch(0.62083339691162109));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.25), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934));
            this.textBox4.Value = "No Distribusi";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.3958332538604736), Telerik.Reporting.Drawing.Unit.Inch(0.375));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.28125), Telerik.Reporting.Drawing.Unit.Inch(0.1875));
            this.textBox3.Value = "textBox3";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5520833730697632), Telerik.Reporting.Drawing.Unit.Inch(0.375));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.83333331346511841), Telerik.Reporting.Drawing.Unit.Inch(0.1979166716337204));
            this.textBox2.Value = "Tanggal :";
            // 
            // group1
            // 
            this.group1.GroupFooter = this.groupFooterSection1;
            this.group1.GroupHeader = this.groupHeaderSection1;
            this.group1.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("PageNo")});
            this.group1.Name = "group1";
            // 
            // groupFooterSection1
            // 
            this.groupFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.18520833551883698);
            this.groupFooterSection1.Name = "groupFooterSection1";
            this.groupFooterSection1.PageBreak = Telerik.Reporting.PageBreak.After;
            this.groupFooterSection1.Style.Visible = false;
            // 
            // groupHeaderSection1
            // 
            this.groupHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(3.3998234272003174);
            this.groupHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox20,
            this.textBox28,
            this.txtLocationName,
            this.textBox27,
            this.txtTransactionNo,
            this.textBox21,
            this.textBox22,
            this.textBox19,
            this.textBox18,
            this.txtTransactionDate,
            this.textBox30,
            this.textBox1,
            this.txtPageNo,
            this.textBox24,
            this.textBox26,
            this.textBox29,
            this.textBox38,
            this.textBox40,
            this.textBox43,
            this.textBox45,
            this.textBox47,
            this.textBox49});
            this.groupHeaderSection1.Name = "groupHeaderSection1";
            this.groupHeaderSection1.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.groupHeaderSection1.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0.5);
            // 
            // textBox24
            // 
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.4352085590362549), Telerik.Reporting.Drawing.Unit.Inch(0.84652751684188843));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.4305555522441864), Telerik.Reporting.Drawing.Unit.Inch(0.49194663763046265));
            this.textBox24.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox24.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox24.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox24.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox24.Style.Font.Bold = true;
            this.textBox24.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox24.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox24.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox24.Value = "K.Kerja";
            // 
            // textBox26
            // 
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.2964777946472168), Telerik.Reporting.Drawing.Unit.Inch(0.84652751684188843));
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.4305555522441864), Telerik.Reporting.Drawing.Unit.Inch(0.49194663763046265));
            this.textBox26.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox26.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox26.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox26.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox26.Style.Font.Bold = true;
            this.textBox26.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox26.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox26.Value = "Fisik";
            // 
            // textBox29
            // 
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.45712423324585), Telerik.Reporting.Drawing.Unit.Inch(0.846448540687561));
            this.textBox29.Name = "textBox19";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.80661559104919434), Telerik.Reporting.Drawing.Unit.Inch(0.49194663763046265));
            this.textBox29.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox29.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox29.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox29.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox29.Style.Font.Bold = true;
            this.textBox29.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox29.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox29.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox29.Value = "Total";
            // 
            // textBox38
            // 
            this.textBox38.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.0983366966247559), Telerik.Reporting.Drawing.Unit.Inch(0.846448540687561));
            this.textBox38.Name = "textBox19";
            this.textBox38.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.67819428443908691), Telerik.Reporting.Drawing.Unit.Inch(0.49194663763046265));
            this.textBox38.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox38.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox38.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox38.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox38.Style.Font.Bold = true;
            this.textBox38.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox38.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox38.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox38.Value = "Nilai";
            // 
            // textBox40
            // 
            this.textBox40.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.7765698432922363), Telerik.Reporting.Drawing.Unit.Inch(0.846448540687561));
            this.textBox40.Name = "textBox19";
            this.textBox40.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.68047499656677246), Telerik.Reporting.Drawing.Unit.Inch(0.49194663763046265));
            this.textBox40.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox40.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox40.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox40.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox40.Style.Font.Bold = true;
            this.textBox40.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox40.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox40.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox40.Value = "Selisih";
            // 
            // textBox43
            // 
            this.textBox43.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.8658435344696045), Telerik.Reporting.Drawing.Unit.Inch(0.846448540687561));
            this.textBox43.Name = "textBox43";
            this.textBox43.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.4305555522441864), Telerik.Reporting.Drawing.Unit.Inch(0.49194663763046265));
            this.textBox43.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox43.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox43.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox43.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox43.Style.Font.Bold = true;
            this.textBox43.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox43.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox43.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox43.Value = "Sblm Appr";
            // 
            // textBox45
            // 
            this.textBox45.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.1577458381652832), Telerik.Reporting.Drawing.Unit.Inch(0.84644836187362671));
            this.textBox45.Name = "textBox45";
            this.textBox45.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.4305555522441864), Telerik.Reporting.Drawing.Unit.Inch(0.49194663763046265));
            this.textBox45.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox45.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox45.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox45.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox45.Style.Font.Bold = true;
            this.textBox45.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox45.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox45.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox45.Value = "Selisih (Fisik - K.Kerja)";
            // 
            // textBox47
            // 
            this.textBox47.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.5883808135986328), Telerik.Reporting.Drawing.Unit.Inch(0.84656649827957153));
            this.textBox47.Name = "textBox47";
            this.textBox47.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.50987684726715088), Telerik.Reporting.Drawing.Unit.Inch(0.49194663763046265));
            this.textBox47.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox47.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox47.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox47.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox47.Style.Font.Bold = true;
            this.textBox47.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox47.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox47.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox47.Value = "Alasan Selisih";
            // 
            // textBox49
            // 
            this.textBox49.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.2820231020450592), Telerik.Reporting.Drawing.Unit.Inch(0.84656661748886108));
            this.textBox49.Name = "textBox49";
            this.textBox49.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.34992128610610962), Telerik.Reporting.Drawing.Unit.Inch(0.49194663763046265));
            this.textBox49.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox49.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox49.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox49.Style.Font.Bold = true;
            this.textBox49.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox49.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox49.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox49.Value = "Rak";
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2708333432674408);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox37,
            this.textBox36,
            this.textBox42});
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // textBox42
            // 
            this.textBox42.Format = "{0:n0}";
            this.textBox42.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.6445956230163574), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox42.Name = "textBox23";
            this.textBox42.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.80665522813797), Telerik.Reporting.Drawing.Unit.Inch(0.25321510434150696));
            this.textBox42.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox42.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox42.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox42.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox42.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox42.Value = "=sum(AmountDiff)";
            // 
            // StockOpnameResultV2Rpt
            // 
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            this.group1});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection1,
            this.groupFooterSection1,
            this.pageHeader,
            this.pageFooter,
            this.detail,
            this.reportFooterSection1});
            this.Name = "StockOpnameResultRpt";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Cm(0.30000001192092896);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Cm(0.30000001192092896);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Cm(0.30000001192092896);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(0.30000001192092896);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Title")});
            styleRule1.Style.Color = System.Drawing.Color.Black;
            styleRule1.Style.Font.Bold = false;
            styleRule1.Style.Font.Italic = false;
            styleRule1.Style.Font.Name = "Tahoma";
            styleRule1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(16);
            styleRule1.Style.Font.Strikeout = false;
            styleRule1.Style.Font.Underline = false;
            styleRule1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Caption")});
            styleRule2.Style.Color = System.Drawing.Color.Black;
            styleRule2.Style.Font.Name = "Tahoma";
            styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            styleRule2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Data")});
            styleRule3.Style.Font.Name = "Tahoma";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("PageInfo")});
            styleRule4.Style.Font.Name = "Tahoma";
            styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            styleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4});
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(8.2637796401977539);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private PageHeaderSection pageHeader;
        private PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox titleTextBox;
        private DetailSection detail;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox txtLocationName;
        private Telerik.Reporting.TextBox txtTransactionNo;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox28;
        private Telerik.Reporting.TextBox txtTransactionDate;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.TextBox textBox33;
        private Telerik.Reporting.TextBox textBox35;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox txtPageNo;
        private Telerik.Reporting.TextBox textBox23;
        private Group group1;
        private GroupFooterSection groupFooterSection1;
        private GroupHeaderSection groupHeaderSection1;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox34;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox36;
        private Telerik.Reporting.TextBox textBox37;
        private ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.TextBox textBox38;
        private Telerik.Reporting.TextBox textBox39;
        private Telerik.Reporting.TextBox textBox41;
        private Telerik.Reporting.TextBox textBox40;
        private Telerik.Reporting.TextBox textBox42;
        private Telerik.Reporting.TextBox textBox43;
        private Telerik.Reporting.TextBox textBox44;
        private Telerik.Reporting.TextBox textBox46;
        private Telerik.Reporting.TextBox textBox45;
        private Telerik.Reporting.TextBox textBox48;
        private Telerik.Reporting.TextBox textBox47;
        private Telerik.Reporting.TextBox textBox50;
        private Telerik.Reporting.TextBox textBox49;

    }
}