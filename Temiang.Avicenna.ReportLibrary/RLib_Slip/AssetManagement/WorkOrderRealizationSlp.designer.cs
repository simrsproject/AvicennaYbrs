namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.AssetManagement
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class WorkOrderRealizationSlp
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
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
            this.titleTextBox = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox35 = new Telerik.Reporting.TextBox();
            this.textBox36 = new Telerik.Reporting.TextBox();
            this.textBox37 = new Telerik.Reporting.TextBox();
            this.textBox38 = new Telerik.Reporting.TextBox();
            this.textBox39 = new Telerik.Reporting.TextBox();
            this.textBox40 = new Telerik.Reporting.TextBox();
            this.textBox41 = new Telerik.Reporting.TextBox();
            this.textBox42 = new Telerik.Reporting.TextBox();
            this.textBox43 = new Telerik.Reporting.TextBox();
            this.textBox44 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.textBox33 = new Telerik.Reporting.TextBox();
            this.textBox34 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.33750152587890625);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.currentTimeTextBox});
            this.pageFooter.Name = "pageFooter";
            this.pageFooter.Style.Font.Name = "Tahoma";
            // 
            // currentTimeTextBox
            // 
            this.currentTimeTextBox.Format = "Print : {0}";
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.081210456788539886));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.21875), Telerik.Reporting.Drawing.Unit.Inch(0.15999999642372131));
            this.currentTimeTextBox.Style.Font.Italic = true;
            this.currentTimeTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "=NOW()";
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Format = "";
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.29305553436279297), Telerik.Reporting.Drawing.Unit.Inch(1.1399607658386231));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.0000002384185791), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.pageInfoTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.pageInfoTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "=RequestBy";
            // 
            // reportHeader
            // 
            this.reportHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(3.2604169845581055);
            this.reportHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.titleTextBox,
            this.textBox1,
            this.textBox2,
            this.textBox3,
            this.textBox4,
            this.textBox5,
            this.textBox6,
            this.textBox7,
            this.textBox8,
            this.textBox9,
            this.textBox10,
            this.textBox11,
            this.textBox23,
            this.textBox24,
            this.textBox25,
            this.textBox26,
            this.textBox27,
            this.textBox28,
            this.textBox17,
            this.textBox32,
            this.textBox22,
            this.textBox30,
            this.textBox19,
            this.textBox35,
            this.textBox36,
            this.textBox37,
            this.textBox38,
            this.textBox39,
            this.textBox40,
            this.textBox41,
            this.textBox42,
            this.textBox43,
            this.textBox44});
            this.reportHeader.Name = "reportHeader";
            this.reportHeader.Style.Font.Name = "Tahoma";
            // 
            // titleTextBox
            // 
            this.titleTextBox.CanGrow = false;
            this.titleTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.0344259738922119), Telerik.Reporting.Drawing.Unit.Inch(0.62083321809768677));
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.0499999523162842), Telerik.Reporting.Drawing.Unit.Inch(0.60000002384185791));
            this.titleTextBox.Style.Font.Name = "Tahoma";
            this.titleTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14);
            this.titleTextBox.Style.Font.Underline = false;
            this.titleTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.titleTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.titleTextBox.StyleName = "Title";
            this.titleTextBox.Value = "=ReportName";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.1345047950744629), Telerik.Reporting.Drawing.Unit.Inch(0.22083313763141632));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.86541628837585449), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Tahoma";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "Tanggal";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = false;
            this.textBox2.Format = ":    {0:dd-MMM-yyyy}";
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6), Telerik.Reporting.Drawing.Unit.Inch(0.22083313763141632));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5770838260650635), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Name = "Tahoma";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "=LastRealizationDateTime";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.1345047950744629), Telerik.Reporting.Drawing.Unit.Inch(0.42083325982093811));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.86541628837585449), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Name = "Tahoma";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = "No. WO";
            // 
            // textBox4
            // 
            this.textBox4.CanGrow = false;
            this.textBox4.Format = ":    {0}";
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6), Telerik.Reporting.Drawing.Unit.Inch(0.42083325982093811));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5770838260650635), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Name = "Tahoma";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.Value = "=OrderNo";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0020834605675190687), Telerik.Reporting.Drawing.Unit.Inch(1.821069598197937));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2979165315628052), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox5.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox5.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox5.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox5.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox5.Style.Font.Bold = false;
            this.textBox5.Style.Font.Name = "Tahoma";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox5.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox5.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox5.Value = "No. Seri";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(2.0211484432220459));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2978377342224121), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox6.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox6.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox6.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox6.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox6.Style.Font.Bold = false;
            this.textBox6.Style.Font.Name = "Tahoma";
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox6.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox6.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox6.Value = "Unit Servis";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0021622974891215563), Telerik.Reporting.Drawing.Unit.Inch(2.2212274074554443));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2956753969192505), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox7.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox7.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox7.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox7.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox7.Style.Font.Bold = false;
            this.textBox7.Style.Font.Name = "Tahoma";
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox7.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox7.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox7.Value = "Keterangan";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0043247011490166187), Telerik.Reporting.Drawing.Unit.Inch(2.6389701366424561));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2956753969192505), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox8.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox8.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox8.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox8.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox8.Style.Font.Bold = false;
            this.textBox8.Style.Font.Name = "Tahoma";
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox8.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox8.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox8.Value = "Hasil";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(3.0365517139434814));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.8000000715255737), Telerik.Reporting.Drawing.Unit.Inch(0.22386519610881805));
            this.textBox9.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox9.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox9.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox9.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox9.Style.Font.Bold = false;
            this.textBox9.Style.Font.Name = "Tahoma";
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox9.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox9.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox9.Value = "Pemakaian Suku Cadang :";
            // 
            // textBox10
            // 
            this.textBox10.CanGrow = false;
            this.textBox10.Format = ":    {0}";
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6), Telerik.Reporting.Drawing.Unit.Inch(0.62083321809768677));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5792450904846191), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox10.Style.Font.Bold = true;
            this.textBox10.Style.Font.Name = "Tahoma";
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox10.Value = "=WorkTypeCode";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.1345047950744629), Telerik.Reporting.Drawing.Unit.Inch(0.62083321809768677));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.86541628837585449), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox11.Style.Font.Bold = true;
            this.textBox11.Style.Font.Name = "Tahoma";
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox11.Value = "Kode WO";
            // 
            // textBox23
            // 
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0020834605675190687), Telerik.Reporting.Drawing.Unit.Inch(1.4209119081497192));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2979167699813843), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox23.Style.Font.Bold = false;
            this.textBox23.Style.Font.Name = "Tahoma";
            this.textBox23.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox23.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox23.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox23.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox23.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox23.Value = "Jenis Pekerjaan";
            // 
            // textBox24
            // 
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0020834605675190687), Telerik.Reporting.Drawing.Unit.Inch(1.6209907531738281));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2979167699813843), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox24.Style.Font.Bold = false;
            this.textBox24.Style.Font.Name = "Tahoma";
            this.textBox24.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox24.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox24.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox24.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox24.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox24.Value = "Nama Alat";
            // 
            // textBox25
            // 
            this.textBox25.Format = "";
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421), Telerik.Reporting.Drawing.Unit.Inch(1.6209907531738281));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.7344264984130859), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox25.Style.Font.Bold = false;
            this.textBox25.Style.Font.Name = "Tahoma";
            this.textBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox25.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox25.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox25.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox25.Value = "=AssetName";
            // 
            // textBox26
            // 
            this.textBox26.Format = "";
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421), Telerik.Reporting.Drawing.Unit.Inch(1.4209119081497192));
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.7344264984130859), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox26.Style.Font.Bold = false;
            this.textBox26.Style.Font.Name = "Tahoma";
            this.textBox26.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox26.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox26.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox26.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox26.Value = "=WorkType";
            // 
            // textBox27
            // 
            this.textBox27.Format = "";
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421), Telerik.Reporting.Drawing.Unit.Inch(1.2208333015441895));
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.7344264984130859), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox27.Style.Font.Bold = false;
            this.textBox27.Style.Font.Name = "Tahoma";
            this.textBox27.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox27.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox27.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox27.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox27.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox27.Value = "=FromServiceUnit";
            // 
            // textBox28
            // 
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0020834605675190687), Telerik.Reporting.Drawing.Unit.Inch(1.2208333015441895));
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2979167699813843), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox28.Style.Font.Bold = false;
            this.textBox28.Style.Font.Name = "Tahoma";
            this.textBox28.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox28.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox28.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox28.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox28.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox28.Value = "Bagian Pemohon";
            // 
            // textBox17
            // 
            this.textBox17.Format = "";
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421), Telerik.Reporting.Drawing.Unit.Inch(1.8210697174072266));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.2999997138977051), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox17.Style.Font.Bold = false;
            this.textBox17.Style.Font.Name = "Tahoma";
            this.textBox17.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox17.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox17.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox17.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox17.Value = "=SerialNumber";
            // 
            // textBox32
            // 
            this.textBox32.Format = "";
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3978376388549805), Telerik.Reporting.Drawing.Unit.Inch(2.0211484432220459));
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.3021626472473145), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox32.Style.Font.Bold = false;
            this.textBox32.Style.Font.Name = "Tahoma";
            this.textBox32.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox32.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox32.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox32.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox32.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox32.Value = "=ToServiceUnit";
            // 
            // textBox22
            // 
            this.textBox22.Format = "";
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3978370428085327), Telerik.Reporting.Drawing.Unit.Inch(2.2212274074554443));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.3021631240844727), Telerik.Reporting.Drawing.Unit.Inch(0.41766399145126343));
            this.textBox22.Style.Font.Bold = false;
            this.textBox22.Style.Font.Name = "Tahoma";
            this.textBox22.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox22.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox22.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox22.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox22.Value = "=ProblemDescription";
            // 
            // textBox30
            // 
            this.textBox30.Format = "";
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421), Telerik.Reporting.Drawing.Unit.Inch(2.6389701366424561));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.3000001907348633), Telerik.Reporting.Drawing.Unit.Inch(0.39750289916992188));
            this.textBox30.Style.Font.Bold = false;
            this.textBox30.Style.Font.Name = "Tahoma";
            this.textBox30.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox30.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox30.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox30.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox30.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox30.Value = "=ActionTaken";
            // 
            // textBox19
            // 
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3000000715255737), Telerik.Reporting.Drawing.Unit.Inch(1.6209907531738281));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.083254911005496979), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox19.Style.Font.Bold = false;
            this.textBox19.Style.Font.Name = "Tahoma";
            this.textBox19.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox19.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox19.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox19.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox19.Value = ":";
            // 
            // textBox35
            // 
            this.textBox35.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3000000715255737), Telerik.Reporting.Drawing.Unit.Inch(1.4209120273590088));
            this.textBox35.Name = "textBox35";
            this.textBox35.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.083254911005496979), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox35.Style.Font.Bold = false;
            this.textBox35.Style.Font.Name = "Tahoma";
            this.textBox35.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox35.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox35.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox35.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox35.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox35.Value = ":";
            // 
            // textBox36
            // 
            this.textBox36.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3000000715255737), Telerik.Reporting.Drawing.Unit.Inch(1.8210697174072266));
            this.textBox36.Name = "textBox36";
            this.textBox36.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.083254911005496979), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox36.Style.Font.Bold = false;
            this.textBox36.Style.Font.Name = "Tahoma";
            this.textBox36.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox36.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox36.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox36.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox36.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox36.Value = ":";
            // 
            // textBox37
            // 
            this.textBox37.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3000005483627319), Telerik.Reporting.Drawing.Unit.Inch(2.0211484432220459));
            this.textBox37.Name = "textBox37";
            this.textBox37.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.083254911005496979), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox37.Style.Font.Bold = false;
            this.textBox37.Style.Font.Name = "Tahoma";
            this.textBox37.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox37.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox37.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox37.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox37.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox37.Value = ":";
            // 
            // textBox38
            // 
            this.textBox38.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3000000715255737), Telerik.Reporting.Drawing.Unit.Inch(2.2212274074554443));
            this.textBox38.Name = "textBox38";
            this.textBox38.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.083254911005496979), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox38.Style.Font.Bold = false;
            this.textBox38.Style.Font.Name = "Tahoma";
            this.textBox38.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox38.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox38.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox38.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox38.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox38.Value = ":";
            // 
            // textBox39
            // 
            this.textBox39.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3000000715255737), Telerik.Reporting.Drawing.Unit.Inch(2.6389703750610352));
            this.textBox39.Name = "textBox39";
            this.textBox39.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.083254911005496979), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox39.Style.Font.Bold = false;
            this.textBox39.Style.Font.Name = "Tahoma";
            this.textBox39.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox39.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox39.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox39.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox39.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox39.Value = ":";
            // 
            // textBox40
            // 
            this.textBox40.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3000000715255737), Telerik.Reporting.Drawing.Unit.Inch(1.2208333015441895));
            this.textBox40.Name = "textBox40";
            this.textBox40.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.083254911005496979), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox40.Style.Font.Bold = false;
            this.textBox40.Style.Font.Name = "Tahoma";
            this.textBox40.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox40.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox40.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox40.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox40.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox40.Value = ":";
            // 
            // textBox41
            // 
            this.textBox41.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.1345047950744629), Telerik.Reporting.Drawing.Unit.Inch(0.82091212272644043));
            this.textBox41.Name = "textBox41";
            this.textBox41.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.86549556255340576), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox41.Style.Font.Bold = true;
            this.textBox41.Style.Font.Name = "Tahoma";
            this.textBox41.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox41.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox41.Value = "Waktu Terima";
            // 
            // textBox42
            // 
            this.textBox42.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.1345047950744629), Telerik.Reporting.Drawing.Unit.Inch(1.0209908485412598));
            this.textBox42.Name = "textBox42";
            this.textBox42.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.86549556255340576), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox42.Style.Font.Bold = true;
            this.textBox42.Style.Font.Name = "Tahoma";
            this.textBox42.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox42.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox42.Value = "Waktu Respon";
            // 
            // textBox43
            // 
            this.textBox43.CanGrow = false;
            this.textBox43.Format = ":    {0:dd-MMM-yyyy HH:mm}";
            this.textBox43.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6), Telerik.Reporting.Drawing.Unit.Inch(0.82091206312179565));
            this.textBox43.Name = "textBox43";
            this.textBox43.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5770838260650635), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox43.Style.Font.Bold = true;
            this.textBox43.Style.Font.Name = "Tahoma";
            this.textBox43.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox43.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox43.Value = "=ReceivedDateTime";
            // 
            // textBox44
            // 
            this.textBox44.CanGrow = false;
            this.textBox44.Format = ":    {0:dd-MMM-yyyy HH:mm}";
            this.textBox44.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.00007963180542), Telerik.Reporting.Drawing.Unit.Inch(1.0209908485412598));
            this.textBox44.Name = "textBox44";
            this.textBox44.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5792450904846191), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox44.Style.Font.Bold = true;
            this.textBox44.Style.Font.Name = "Tahoma";
            this.textBox44.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox44.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox44.Value = "=FirstResponseDateTime";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.16003940999507904);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox14,
            this.textBox15,
            this.textBox16,
            this.textBox18});
            this.detail.Name = "detail";
            this.detail.Style.Font.Name = "Tahoma";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.0930557250976562), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.59783762693405151), Telerik.Reporting.Drawing.Unit.Inch(0.15999999642372131));
            this.textBox14.Style.Font.Name = "Tahoma";
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox14.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox14.Value = "=SRItemUnit";
            // 
            // textBox15
            // 
            this.textBox15.Format = "{0:N2}";
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.42638897895813), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.66666668653488159), Telerik.Reporting.Drawing.Unit.Inch(0.15999999642372131));
            this.textBox15.Style.Font.Name = "Tahoma";
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox15.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox15.Value = "=Quantity";
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.52646762132644653), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.8999214172363281), Telerik.Reporting.Drawing.Unit.Inch(0.15999999642372131));
            this.textBox16.Style.Font.Name = "Tahoma";
            this.textBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox16.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox16.Value = "=ItemName";
            // 
            // textBox18
            // 
            this.textBox18.Format = "{0:N0}.";
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.32638883590698242), Telerik.Reporting.Drawing.Unit.Inch(0.15999999642372131));
            this.textBox18.Style.Font.Name = "Tahoma";
            this.textBox18.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox18.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox18.Value = "= RowNumber()";
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(1.7399603128433228);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox12,
            this.textBox13,
            this.textBox20,
            this.textBox21,
            this.textBox29,
            this.textBox31,
            this.textBox33,
            this.textBox34,
            this.pageInfoTextBox});
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.8000001907348633), Telerik.Reporting.Drawing.Unit.Inch(0.2125438004732132));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox12.Style.Font.Name = "Tahoma";
            this.textBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.Value = "Mengetahui,";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.3000006675720215), Telerik.Reporting.Drawing.Unit.Inch(1.3400396108627319));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.0000009536743164), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox13.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox13.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox13.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox13.Style.Font.Name = "Tahoma";
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.Value = "=\'Unit Servis \' + ToServiceUnit";
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.3000006675720215), Telerik.Reporting.Drawing.Unit.Inch(1.1399607658386231));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.0000009536743164), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox20.Style.Font.Name = "Tahoma";
            this.textBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox20.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox20.Value = "=RealizationBy";
            // 
            // textBox21
            // 
            this.textBox21.Format = "{0:dd-MMM-yyyy}";
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.8000001907348633), Telerik.Reporting.Drawing.Unit.Inch(1.3400396108627319));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox21.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox21.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox21.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox21.Style.Font.Name = "Tahoma";
            this.textBox21.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox21.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox21.Value = "=\'Kabag \' + FromServiceUnit";
            // 
            // textBox29
            // 
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.3000006675720215), Telerik.Reporting.Drawing.Unit.Inch(0.21254390478134155));
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox29.Style.Font.Name = "Tahoma";
            this.textBox29.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox29.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox29.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox29.Value = "Pelaksana,";
            // 
            // textBox31
            // 
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.29305574297904968), Telerik.Reporting.Drawing.Unit.Inch(0.2389330267906189));
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox31.Style.Font.Name = "Tahoma";
            this.textBox31.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox31.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox31.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox31.Value = "Pemohon,";
            // 
            // textBox33
            // 
            this.textBox33.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.8000001907348633), Telerik.Reporting.Drawing.Unit.Inch(1.1399607658386231));
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox33.Style.Font.Name = "Tahoma";
            this.textBox33.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox33.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox33.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox33.Value = "";
            // 
            // textBox34
            // 
            this.textBox34.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.29313406348228455), Telerik.Reporting.Drawing.Unit.Inch(1.3400396108627319));
            this.textBox34.Name = "textBox34";
            this.textBox34.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.9999215602874756), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox34.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox34.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox34.Style.Font.Name = "Tahoma";
            this.textBox34.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox34.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox34.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox34.Value = "=\'Petugas bagian \' + FromServiceUnit";
            // 
            // WorkOrderRealizationSlp
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageFooter,
            this.reportHeader,
            this.detail,
            this.reportFooterSection1});
            this.Name = "WorkOrderRealizationSlp";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Title")});
            styleRule1.Style.Color = System.Drawing.Color.Black;
            styleRule1.Style.Font.Bold = true;
            styleRule1.Style.Font.Italic = false;
            styleRule1.Style.Font.Name = "Tahoma";
            styleRule1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(20);
            styleRule1.Style.Font.Strikeout = false;
            styleRule1.Style.Font.Underline = false;
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
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.6292452812194824);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private ReportHeaderSection reportHeader;
        private Telerik.Reporting.TextBox titleTextBox;
        private DetailSection detail;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox11;
        private ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox28;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox textBox33;
        private Telerik.Reporting.TextBox textBox34;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox35;
        private Telerik.Reporting.TextBox textBox36;
        private Telerik.Reporting.TextBox textBox37;
        private Telerik.Reporting.TextBox textBox38;
        private Telerik.Reporting.TextBox textBox39;
        private Telerik.Reporting.TextBox textBox40;
        private Telerik.Reporting.TextBox textBox41;
        private Telerik.Reporting.TextBox textBox42;
        private Telerik.Reporting.TextBox textBox43;
        private Telerik.Reporting.TextBox textBox44;

    }
}