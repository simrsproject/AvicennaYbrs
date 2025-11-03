namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.AssetManagement
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class WorkOrderToThirdPartySlp
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
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
            this.titleTextBox = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.textBox33 = new Telerik.Reporting.TextBox();
            this.textBox34 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(0.89999991655349731);
            this.pageHeader.Name = "pageHeader";
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
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.406944751739502), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.pageInfoTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.pageInfoTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "=RealizationBy";
            // 
            // reportHeader
            // 
            this.reportHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(2.1197917461395264);
            this.reportHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.titleTextBox,
            this.textBox5,
            this.textBox6,
            this.textBox7,
            this.textBox8,
            this.textBox24,
            this.textBox25,
            this.textBox27,
            this.textBox28,
            this.textBox17,
            this.textBox32,
            this.textBox22,
            this.textBox30,
            this.textBox4,
            this.textBox9,
            this.textBox10,
            this.textBox11,
            this.textBox14,
            this.textBox15});
            this.reportHeader.Name = "reportHeader";
            this.reportHeader.Style.Font.Name = "Tahoma";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.10000000894069672));
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.602083683013916), Telerik.Reporting.Drawing.Unit.Inch(0.30000010132789612));
            this.titleTextBox.Style.Font.Name = "Tahoma";
            this.titleTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14);
            this.titleTextBox.Style.Font.Underline = false;
            this.titleTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.titleTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.titleTextBox.StyleName = "Title";
            this.titleTextBox.Value = "Form Permintaan Perbaikan Pihak Ke-3";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.86057436466217041));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2000000476837158), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
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
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(1.0606532096862793));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2000000476837158), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
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
            this.textBox6.Value = "Estimasi Biaya";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0043247011490166187), Telerik.Reporting.Drawing.Unit.Inch(1.2607319355010986));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1956753730773926), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
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
            this.textBox7.Value = "Kondisi Awal";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(1.6784749031066895));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1999999284744263), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
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
            this.textBox8.Value = "Keterangan";
            // 
            // textBox24
            // 
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0020834605675190687), Telerik.Reporting.Drawing.Unit.Inch(0.66049563884735107));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1979166269302368), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
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
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3021630048751831), Telerik.Reporting.Drawing.Unit.Inch(0.66049563884735107));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.1068658828735352), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox25.Style.Font.Bold = false;
            this.textBox25.Style.Font.Name = "Tahoma";
            this.textBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox25.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox25.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox25.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox25.Value = "=AssetName";
            // 
            // textBox27
            // 
            this.textBox27.Format = "";
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3021630048751831), Telerik.Reporting.Drawing.Unit.Inch(0.46041673421859741));
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.1068658828735352), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox27.Style.Font.Bold = false;
            this.textBox27.Style.Font.Name = "Tahoma";
            this.textBox27.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox27.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox27.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox27.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox27.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox27.Value = "=OrderNo";
            // 
            // textBox28
            // 
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0020834605675190687), Telerik.Reporting.Drawing.Unit.Inch(0.46041673421859741));
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1979166269302368), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox28.Style.Font.Bold = false;
            this.textBox28.Style.Font.Name = "Tahoma";
            this.textBox28.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox28.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox28.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox28.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox28.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox28.Value = "No. WO";
            // 
            // textBox17
            // 
            this.textBox17.Format = "";
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3021631240844727), Telerik.Reporting.Drawing.Unit.Inch(0.86057436466217041));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.1090288162231445), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
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
            this.textBox32.Format = "Rp. {0:N2}";
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3021637201309204), Telerik.Reporting.Drawing.Unit.Inch(1.0606532096862793));
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.1090283393859863), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox32.Style.Font.Bold = false;
            this.textBox32.Style.Font.Name = "Tahoma";
            this.textBox32.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox32.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox32.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox32.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox32.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox32.Value = "=CostEstimation";
            // 
            // textBox22
            // 
            this.textBox22.Format = "";
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3021637201309204), Telerik.Reporting.Drawing.Unit.Inch(1.2607319355010986));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.1090288162231445), Telerik.Reporting.Drawing.Unit.Inch(0.41766399145126343));
            this.textBox22.Style.Font.Bold = false;
            this.textBox22.Style.Font.Name = "Tahoma";
            this.textBox22.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox22.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox22.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox22.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox22.Value = "=InitialConditions";
            // 
            // textBox30
            // 
            this.textBox30.Format = "";
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3021644353866577), Telerik.Reporting.Drawing.Unit.Inch(1.6784749031066895));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.1090283393859863), Telerik.Reporting.Drawing.Unit.Inch(0.39750289916992188));
            this.textBox30.Style.Font.Bold = false;
            this.textBox30.Style.Font.Name = "Tahoma";
            this.textBox30.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox30.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox30.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox30.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox30.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox30.Value = "=Notes";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0520833395421505);
            this.detail.Name = "detail";
            this.detail.Style.Font.Name = "Tahoma";
            this.detail.Style.Visible = false;
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(1.6479165554046631);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox12,
            this.textBox13,
            this.textBox20,
            this.textBox21,
            this.textBox29,
            this.textBox31,
            this.textBox33,
            this.textBox34,
            this.pageInfoTextBox,
            this.textBox1,
            this.textBox2,
            this.textBox3});
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.1000001430511475), Telerik.Reporting.Drawing.Unit.Inch(0.23893314599990845));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4069443941116333), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox12.Style.Font.Name = "Tahoma";
            this.textBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.Value = "Mengetahui,";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9000000953674316), Telerik.Reporting.Drawing.Unit.Inch(1.3400396108627319));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4069453477859497), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox13.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox13.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox13.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox13.Style.Font.Name = "Tahoma";
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.Value = "Direktorat";
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9000000953674316), Telerik.Reporting.Drawing.Unit.Inch(1.1399607658386231));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4069453477859497), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox20.Style.Font.Name = "Tahoma";
            this.textBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox20.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox20.Value = "";
            // 
            // textBox21
            // 
            this.textBox21.Format = "{0:dd-MMM-yyyy}";
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.1000001430511475), Telerik.Reporting.Drawing.Unit.Inch(1.3400396108627319));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4069443941116333), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox21.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox21.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox21.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox21.Style.Font.Name = "Tahoma";
            this.textBox21.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox21.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox21.Value = "Kabag Pemeliharaan";
            // 
            // textBox29
            // 
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.8999998569488525), Telerik.Reporting.Drawing.Unit.Inch(0.23893314599990845));
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4069443941116333), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox29.Style.Font.Name = "Tahoma";
            this.textBox29.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox29.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox29.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox29.Value = "Menyetujui,";
            // 
            // textBox31
            // 
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.29305574297904968), Telerik.Reporting.Drawing.Unit.Inch(0.2389330267906189));
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4069443941116333), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox31.Style.Font.Name = "Tahoma";
            this.textBox31.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox31.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox31.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox31.Value = "Pemohon,";
            // 
            // textBox33
            // 
            this.textBox33.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.1000001430511475), Telerik.Reporting.Drawing.Unit.Inch(1.1399607658386231));
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4069443941116333), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
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
            this.textBox34.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4068658351898193), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox34.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox34.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox34.Style.Font.Name = "Tahoma";
            this.textBox34.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox34.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox34.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox34.Value = "=ToServiceUnit";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.7000002861022949), Telerik.Reporting.Drawing.Unit.Inch(0.23893314599990845));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4069443941116333), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox1.Style.Font.Name = "Tahoma";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "Pelaksana,";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.6999993324279785), Telerik.Reporting.Drawing.Unit.Inch(1.1399607658386231));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4069453477859497), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox2.Style.Font.Name = "Tahoma";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.6999993324279785), Telerik.Reporting.Drawing.Unit.Inch(1.3400396108627319));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4069453477859497), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox3.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox3.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox3.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox3.Style.Font.Name = "Tahoma";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = "Logistik";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2000789642333984), Telerik.Reporting.Drawing.Unit.Inch(0.46041667461395264));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.10000017285346985), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox4.Style.Font.Bold = false;
            this.textBox4.Style.Font.Name = "Tahoma";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox4.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox4.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox4.Value = ":";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2000789642333984), Telerik.Reporting.Drawing.Unit.Inch(0.66049563884735107));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.10000017285346985), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox9.Style.Font.Bold = false;
            this.textBox9.Style.Font.Name = "Tahoma";
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox9.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox9.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox9.Value = ":";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2000788450241089), Telerik.Reporting.Drawing.Unit.Inch(0.86057442426681519));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.10000017285346985), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox10.Style.Font.Bold = false;
            this.textBox10.Style.Font.Name = "Tahoma";
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox10.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox10.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox10.Value = ":";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2000789642333984), Telerik.Reporting.Drawing.Unit.Inch(1.0606532096862793));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.10000017285346985), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox11.Style.Font.Bold = false;
            this.textBox11.Style.Font.Name = "Tahoma";
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox11.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox11.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox11.Value = ":";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2000787258148193), Telerik.Reporting.Drawing.Unit.Inch(1.2607319355010986));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.10000017285346985), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox14.Style.Font.Bold = false;
            this.textBox14.Style.Font.Name = "Tahoma";
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox14.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox14.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox14.Value = ":";
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2000787258148193), Telerik.Reporting.Drawing.Unit.Inch(1.6784747838974));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.10000017285346985), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox15.Style.Font.Bold = false;
            this.textBox15.Style.Font.Name = "Tahoma";
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox15.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox15.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox15.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox15.Value = ":";
            // 
            // WorkOrderToThirdPartySlp
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.pageFooter,
            this.reportHeader,
            this.detail,
            this.reportFooterSection1});
            this.Name = "WorkOrderToThirdPartySlp";
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

        private PageHeaderSection pageHeader;
        private PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private ReportHeaderSection reportHeader;
        private Telerik.Reporting.TextBox titleTextBox;
        private DetailSection detail;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox22;
        private ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox28;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox textBox33;
        private Telerik.Reporting.TextBox textBox34;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox15;

    }
}