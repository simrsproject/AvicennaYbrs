namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.Cencus
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class DailyCensusOfPatientsInCareRpt
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.TableGroup tableGroup15 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup16 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup17 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup18 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup19 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup20 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup21 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup22 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup23 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup24 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup25 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup26 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup27 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup28 = new Telerik.Reporting.TableGroup();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox33 = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox37 = new Telerik.Reporting.TextBox();
            this.textBox41 = new Telerik.Reporting.TextBox();
            this.textBox45 = new Telerik.Reporting.TextBox();
            this.textBox49 = new Telerik.Reporting.TextBox();
            this.textBox54 = new Telerik.Reporting.TextBox();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.txtdate = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox48 = new Telerik.Reporting.TextBox();
            this.textBox55 = new Telerik.Reporting.TextBox();
            this.textBox62 = new Telerik.Reporting.TextBox();
            this.textBox64 = new Telerik.Reporting.TextBox();
            this.textBox69 = new Telerik.Reporting.TextBox();
            this.textBox63 = new Telerik.Reporting.TextBox();
            this.textBox71 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.shape6 = new Telerik.Reporting.Shape();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.table1 = new Telerik.Reporting.Table();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.textBox35 = new Telerik.Reporting.TextBox();
            this.textBox39 = new Telerik.Reporting.TextBox();
            this.textBox43 = new Telerik.Reporting.TextBox();
            this.textBox47 = new Telerik.Reporting.TextBox();
            this.textBox52 = new Telerik.Reporting.TextBox();
            this.textBox56 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // textBox1
            // 
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.29792815446853638), Telerik.Reporting.Drawing.Unit.Inch(0.36145830154418945));
            this.textBox1.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox1.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox1.Value = "No.";
            // 
            // textBox2
            // 
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0375117063522339), Telerik.Reporting.Drawing.Unit.Inch(0.36145830154418945));
            this.textBox2.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox2.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "No. Reg / No.RM";
            // 
            // textBox11
            // 
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0479273796081543), Telerik.Reporting.Drawing.Unit.Inch(0.36145830154418945));
            this.textBox11.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox11.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox11.Style.Font.Bold = true;
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox11.StyleName = "";
            this.textBox11.Value = "Nama / Service Unit";
            // 
            // textBox16
            // 
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1041666269302368), Telerik.Reporting.Drawing.Unit.Inch(0.36145830154418945));
            this.textBox16.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox16.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox16.Style.Font.Bold = true;
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox16.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox16.StyleName = "";
            this.textBox16.Value = "No. Reg / No.RM";
            // 
            // textBox20
            // 
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0729153156280518), Telerik.Reporting.Drawing.Unit.Inch(0.36145830154418945));
            this.textBox20.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox20.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox20.Style.Font.Bold = true;
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox20.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox20.StyleName = "";
            this.textBox20.Value = "Nama / Service Unit";
            // 
            // textBox33
            // 
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0520838499069214), Telerik.Reporting.Drawing.Unit.Inch(0.36145830154418945));
            this.textBox33.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox33.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox33.Style.Font.Bold = true;
            this.textBox33.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox33.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox33.StyleName = "";
            this.textBox33.Value = "No. Reg / No.RM";
            // 
            // textBox29
            // 
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0729160308837891), Telerik.Reporting.Drawing.Unit.Inch(0.36145830154418945));
            this.textBox29.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox29.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox29.Style.Font.Bold = true;
            this.textBox29.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox29.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox29.StyleName = "";
            this.textBox29.Value = "Nama / Service Unit";
            // 
            // textBox24
            // 
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0208326578140259), Telerik.Reporting.Drawing.Unit.Inch(0.36145830154418945));
            this.textBox24.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox24.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox24.Style.Font.Bold = true;
            this.textBox24.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox24.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox24.StyleName = "";
            this.textBox24.Value = "No. Reg / No.RM";
            // 
            // textBox37
            // 
            this.textBox37.Name = "textBox37";
            this.textBox37.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.96874982118606567), Telerik.Reporting.Drawing.Unit.Inch(0.36145830154418945));
            this.textBox37.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox37.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox37.Style.Font.Bold = true;
            this.textBox37.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox37.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox37.StyleName = "";
            this.textBox37.Value = "Nama / Service Unit";
            // 
            // textBox41
            // 
            this.textBox41.Name = "textBox41";
            this.textBox41.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.84374970197677612), Telerik.Reporting.Drawing.Unit.Inch(0.36145830154418945));
            this.textBox41.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox41.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox41.Style.Font.Bold = true;
            this.textBox41.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox41.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox41.StyleName = "";
            this.textBox41.Value = "No. Reg / No.RM";
            // 
            // textBox45
            // 
            this.textBox45.Name = "textBox45";
            this.textBox45.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.91666573286056519), Telerik.Reporting.Drawing.Unit.Inch(0.36145830154418945));
            this.textBox45.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox45.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox45.Style.Font.Bold = true;
            this.textBox45.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox45.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox45.StyleName = "";
            this.textBox45.Value = "Nama / Service Unit";
            // 
            // textBox49
            // 
            this.textBox49.Name = "textBox49";
            this.textBox49.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.37499940395355225), Telerik.Reporting.Drawing.Unit.Inch(0.36145830154418945));
            this.textBox49.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox49.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox49.Style.Font.Bold = true;
            this.textBox49.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox49.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox49.StyleName = "";
            this.textBox49.Value = "< 48";
            // 
            // textBox54
            // 
            this.textBox54.Name = "textBox54";
            this.textBox54.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.30208185315132141), Telerik.Reporting.Drawing.Unit.Inch(0.36145830154418945));
            this.textBox54.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox54.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox54.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox54.Style.Font.Bold = true;
            this.textBox54.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox54.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox54.StyleName = "";
            this.textBox54.Value = "> 48";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(3.8412492275238037);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtdate,
            this.textBox5,
            this.textBox48,
            this.textBox55,
            this.textBox62,
            this.textBox64,
            this.textBox69,
            this.textBox63,
            this.textBox71});
            this.pageHeader.Name = "pageHeader";
            // 
            // txtdate
            // 
            this.txtdate.Format = "Tanggal : {0:dd-MMMM-yyyy}";
            this.txtdate.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.078740119934082031), Telerik.Reporting.Drawing.Unit.Inch(0.90000009536743164));
            this.txtdate.Name = "txtdate";
            this.txtdate.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(11.132134437561035), Telerik.Reporting.Drawing.Unit.Inch(0.19367106258869171));
            this.txtdate.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.txtdate.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.txtdate.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.txtdate.Style.Font.Bold = true;
            this.txtdate.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(11);
            this.txtdate.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtdate.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.txtdate.Style.Visible = true;
            this.txtdate.Value = "=Date1";
            // 
            // textBox5
            // 
            this.textBox5.Format = "";
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.078740157186985016), Telerik.Reporting.Drawing.Unit.Inch(0.70000004768371582));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(11.127541542053223), Telerik.Reporting.Drawing.Unit.Inch(0.19992120563983917));
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(16);
            this.textBox5.Style.Font.Strikeout = false;
            this.textBox5.Style.Font.Underline = false;
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox5.Value = "Sensus Harian Penderita Dirawat";
            // 
            // textBox48
            // 
            this.textBox48.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.7792754173278809), Telerik.Reporting.Drawing.Unit.Inch(1.2916666269302368));
            this.textBox48.Name = "textBox30";
            this.textBox48.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.9894243478775024), Telerik.Reporting.Drawing.Unit.Inch(0.18938620388507843));
            this.textBox48.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox48.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox48.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox48.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox48.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox48.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox48.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox48.Style.BorderWidth.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox48.Style.BorderWidth.Right = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox48.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox48.Style.Font.Bold = true;
            this.textBox48.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox48.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox48.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox48.Style.Visible = true;
            this.textBox48.Value = "Pasien Dipindahkan";
            // 
            // textBox55
            // 
            this.textBox55.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.4771163463592529), Telerik.Reporting.Drawing.Unit.Inch(1.2916666269302368));
            this.textBox55.Name = "textBox30";
            this.textBox55.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1770811080932617), Telerik.Reporting.Drawing.Unit.Inch(0.18938620388507843));
            this.textBox55.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox55.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox55.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox55.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox55.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox55.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox55.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox55.Style.BorderWidth.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox55.Style.BorderWidth.Right = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox55.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox55.Style.Font.Bold = true;
            this.textBox55.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox55.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox55.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox55.Style.Visible = true;
            this.textBox55.Value = "Pasien Pindahan";
            // 
            // textBox62
            // 
            this.textBox62.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.09375), Telerik.Reporting.Drawing.Unit.Inch(1.2916666269302368));
            this.textBox62.Name = "textBox30";
            this.textBox62.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.2979280948638916), Telerik.Reporting.Drawing.Unit.Inch(0.18938620388507843));
            this.textBox62.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox62.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox62.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox62.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox62.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox62.Style.BorderWidth.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox62.Style.BorderWidth.Right = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox62.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox62.Style.Font.Bold = true;
            this.textBox62.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox62.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox62.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.textBox62.Style.Visible = true;
            this.textBox62.Value = "";
            // 
            // textBox64
            // 
            this.textBox64.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.6542763710021973), Telerik.Reporting.Drawing.Unit.Inch(1.2916666269302368));
            this.textBox64.Name = "textBox30";
            this.textBox64.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1249198913574219), Telerik.Reporting.Drawing.Unit.Inch(0.18938620388507843));
            this.textBox64.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox64.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox64.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox64.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox64.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox64.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox64.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox64.Style.BorderWidth.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox64.Style.BorderWidth.Right = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox64.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox64.Style.Font.Bold = true;
            this.textBox64.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox64.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox64.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox64.Style.Visible = true;
            this.textBox64.Value = "Pasien Keluar Hidup";
            // 
            // textBox69
            // 
            this.textBox69.Angle = 0;
            this.textBox69.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.7687788009643555), Telerik.Reporting.Drawing.Unit.Inch(1.2916666269302368));
            this.textBox69.Name = "textBox30";
            this.textBox69.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.4375038146972656), Telerik.Reporting.Drawing.Unit.Inch(0.18938620388507843));
            this.textBox69.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox69.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox69.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox69.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox69.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox69.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox69.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox69.Style.BorderWidth.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox69.Style.BorderWidth.Right = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox69.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox69.Style.Font.Bold = true;
            this.textBox69.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox69.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox69.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox69.Style.Visible = true;
            this.textBox69.Value = "Pasien Meninggal";
            // 
            // textBox63
            // 
            this.textBox63.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.39175677299499512), Telerik.Reporting.Drawing.Unit.Inch(1.2916666269302368));
            this.textBox63.Name = "textBox30";
            this.textBox63.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.0749442577362061), Telerik.Reporting.Drawing.Unit.Inch(0.18938620388507843));
            this.textBox63.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox63.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox63.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox63.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox63.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox63.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox63.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox63.Style.BorderWidth.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox63.Style.BorderWidth.Right = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox63.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox63.Style.Font.Bold = true;
            this.textBox63.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox63.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox63.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox63.Style.Visible = true;
            this.textBox63.Value = "Pasien Masuk";
            // 
            // textBox71
            // 
            this.textBox71.Format = "Ruang    : {0}";
            this.textBox71.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0833333358168602), Telerik.Reporting.Drawing.Unit.Inch(1.0999999046325684));
            this.textBox71.Name = "txtPeriod";
            this.textBox71.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(11.127541542053223), Telerik.Reporting.Drawing.Unit.Inch(0.18938620388507843));
            this.textBox71.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox71.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox71.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox71.Style.Font.Bold = true;
            this.textBox71.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(11);
            this.textBox71.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox71.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox71.Style.Visible = true;
            this.textBox71.Value = "=Roomtmp";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.13229165971279144);
            this.detail.Name = "detail";
            this.detail.Style.Visible = false;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.799142062664032);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox28,
            this.shape6,
            this.textBox10});
            this.pageFooter.Name = "pageFooter";
            // 
            // textBox28
            // 
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(9.1220474243164062), Telerik.Reporting.Drawing.Unit.Inch(0.12503941357135773));
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.0738189220428467), Telerik.Reporting.Drawing.Unit.Inch(0.18958337604999542));
            this.textBox28.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox28.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox28.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox28.Style.Font.Bold = false;
            this.textBox28.Style.Font.Italic = true;
            this.textBox28.Style.Font.Name = "Tahoma";
            this.textBox28.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox28.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox28.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox28.Value = "=\'Page : \' + PageNumber +\' / \'+ PageCount";
            // 
            // shape6
            // 
            this.shape6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.23812499642372131), Telerik.Reporting.Drawing.Unit.Cm(0.10583332926034927));
            this.shape6.Name = "shape6";
            this.shape6.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(28.225831985473633), Telerik.Reporting.Drawing.Unit.Cm(0.19999989867210388));
            // 
            // textBox10
            // 
            this.textBox10.Format = "Avicenna HIS, Print Date : {0:dd-MM-yyyy HH:mm}";
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0895833969116211), Telerik.Reporting.Drawing.Unit.Inch(0.12048562616109848));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.13753080368042), Telerik.Reporting.Drawing.Unit.Inch(0.18958337604999542));
            this.textBox10.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox10.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox10.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox10.Style.Font.Bold = false;
            this.textBox10.Style.Font.Italic = true;
            this.textBox10.Style.Font.Name = "Tahoma";
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox10.Value = "= Now()";
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.65624982118606567);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.table1});
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // table1
            // 
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.29792803525924683)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.0375115871429443)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.047926664352417)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.10416579246521)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.0729154348373413)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.0520833730697632)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.0729151964187622)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.0208326578140259)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.96874934434890747)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.843749463558197)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.91666507720947266)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.37499919533729553)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.30208185315132141)));
            this.table1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Inch(0.22395825386047363)));
            this.table1.Body.SetCellContent(0, 0, this.textBox4);
            this.table1.Body.SetCellContent(0, 1, this.textBox6);
            this.table1.Body.SetCellContent(0, 2, this.textBox7);
            this.table1.Body.SetCellContent(0, 3, this.textBox18);
            this.table1.Body.SetCellContent(0, 4, this.textBox22);
            this.table1.Body.SetCellContent(0, 7, this.textBox26);
            this.table1.Body.SetCellContent(0, 6, this.textBox31);
            this.table1.Body.SetCellContent(0, 5, this.textBox35);
            this.table1.Body.SetCellContent(0, 8, this.textBox39);
            this.table1.Body.SetCellContent(0, 9, this.textBox43);
            this.table1.Body.SetCellContent(0, 10, this.textBox47);
            this.table1.Body.SetCellContent(0, 11, this.textBox52);
            this.table1.Body.SetCellContent(0, 12, this.textBox56);
            tableGroup15.Name = "Group4";
            tableGroup15.ReportItem = this.textBox1;
            tableGroup16.Name = "Group5";
            tableGroup16.ReportItem = this.textBox2;
            tableGroup17.Name = "Group6";
            tableGroup17.ReportItem = this.textBox11;
            tableGroup18.Name = "Group8";
            tableGroup18.ReportItem = this.textBox16;
            tableGroup19.Name = "Group11";
            tableGroup19.ReportItem = this.textBox20;
            tableGroup20.Name = "Group20";
            tableGroup20.ReportItem = this.textBox33;
            tableGroup21.Name = "Group17";
            tableGroup21.ReportItem = this.textBox29;
            tableGroup22.Name = "Group14";
            tableGroup22.ReportItem = this.textBox24;
            tableGroup23.Name = "Group23";
            tableGroup23.ReportItem = this.textBox37;
            tableGroup24.Name = "Group26";
            tableGroup24.ReportItem = this.textBox41;
            tableGroup25.Name = "Group29";
            tableGroup25.ReportItem = this.textBox45;
            tableGroup26.Name = "Group32";
            tableGroup26.ReportItem = this.textBox49;
            tableGroup27.Name = "Group35";
            tableGroup27.ReportItem = this.textBox54;
            this.table1.ColumnGroups.Add(tableGroup15);
            this.table1.ColumnGroups.Add(tableGroup16);
            this.table1.ColumnGroups.Add(tableGroup17);
            this.table1.ColumnGroups.Add(tableGroup18);
            this.table1.ColumnGroups.Add(tableGroup19);
            this.table1.ColumnGroups.Add(tableGroup20);
            this.table1.ColumnGroups.Add(tableGroup21);
            this.table1.ColumnGroups.Add(tableGroup22);
            this.table1.ColumnGroups.Add(tableGroup23);
            this.table1.ColumnGroups.Add(tableGroup24);
            this.table1.ColumnGroups.Add(tableGroup25);
            this.table1.ColumnGroups.Add(tableGroup26);
            this.table1.ColumnGroups.Add(tableGroup27);
            this.table1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox4,
            this.textBox6,
            this.textBox7,
            this.textBox18,
            this.textBox22,
            this.textBox26,
            this.textBox31,
            this.textBox35,
            this.textBox39,
            this.textBox43,
            this.textBox47,
            this.textBox52,
            this.textBox56,
            this.textBox1,
            this.textBox2,
            this.textBox11,
            this.textBox16,
            this.textBox20,
            this.textBox33,
            this.textBox29,
            this.textBox24,
            this.textBox37,
            this.textBox41,
            this.textBox45,
            this.textBox49,
            this.textBox54});
            this.table1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.09375), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.table1.Name = "table1";
            tableGroup28.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("")});
            tableGroup28.Name = "DetailGroup";
            this.table1.RowGroups.Add(tableGroup28);
            this.table1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(11.112523078918457), Telerik.Reporting.Drawing.Unit.Inch(0.58541655540466309));
            this.table1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            // 
            // textBox4
            // 
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.297928124666214), Telerik.Reporting.Drawing.Unit.Inch(0.22395825386047363));
            this.textBox4.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox4.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox4.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.Value = "= RowNumber()";
            // 
            // textBox6
            // 
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0375117063522339), Telerik.Reporting.Drawing.Unit.Inch(0.22395825386047363));
            this.textBox6.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "=PatientIden";
            // 
            // textBox7
            // 
            this.textBox7.Format = "{0:0}";
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0479273796081543), Telerik.Reporting.Drawing.Unit.Inch(0.22395825386047363));
            this.textBox7.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.Value = "=PatientNameUnit";
            // 
            // textBox18
            // 
            this.textBox18.Format = "{0:0}";
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1041666269302368), Telerik.Reporting.Drawing.Unit.Inch(0.22395825386047363));
            this.textBox18.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox18.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox18.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox18.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox18.StyleName = "";
            this.textBox18.Value = "=PatientIdenTransferIn";
            // 
            // textBox22
            // 
            this.textBox22.Format = "{0:0}";
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0729153156280518), Telerik.Reporting.Drawing.Unit.Inch(0.22395825386047363));
            this.textBox22.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox22.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox22.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox22.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox22.StyleName = "";
            this.textBox22.Value = "=PatientNameUnitTransferIn";
            // 
            // textBox26
            // 
            this.textBox26.Format = "{0:0}";
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0208326578140259), Telerik.Reporting.Drawing.Unit.Inch(0.22395825386047363));
            this.textBox26.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox26.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox26.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox26.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox26.StyleName = "";
            this.textBox26.Value = "=PatientIdenTransferOut";
            // 
            // textBox31
            // 
            this.textBox31.Format = "{0:0}";
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0729160308837891), Telerik.Reporting.Drawing.Unit.Inch(0.22395825386047363));
            this.textBox31.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox31.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox31.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox31.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox31.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox31.StyleName = "";
            this.textBox31.Value = "=PatientNameUnitLife";
            // 
            // textBox35
            // 
            this.textBox35.Format = "{0:0}";
            this.textBox35.Name = "textBox35";
            this.textBox35.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0520838499069214), Telerik.Reporting.Drawing.Unit.Inch(0.22395825386047363));
            this.textBox35.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox35.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox35.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox35.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox35.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox35.StyleName = "";
            this.textBox35.Value = "=PatientIdenLife";
            // 
            // textBox39
            // 
            this.textBox39.Format = "{0:0}";
            this.textBox39.Name = "textBox39";
            this.textBox39.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.96874982118606567), Telerik.Reporting.Drawing.Unit.Inch(0.22395825386047363));
            this.textBox39.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox39.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox39.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox39.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox39.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox39.StyleName = "";
            this.textBox39.Value = "=PatientNameUnitTransferOut";
            // 
            // textBox43
            // 
            this.textBox43.Format = "{0:0}";
            this.textBox43.Name = "textBox43";
            this.textBox43.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.84374970197677612), Telerik.Reporting.Drawing.Unit.Inch(0.22395825386047363));
            this.textBox43.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox43.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox43.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox43.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox43.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox43.StyleName = "";
            this.textBox43.Value = "=PatientIdenDead";
            // 
            // textBox47
            // 
            this.textBox47.Format = "{0:0}";
            this.textBox47.Name = "textBox47";
            this.textBox47.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.91666573286056519), Telerik.Reporting.Drawing.Unit.Inch(0.22395825386047363));
            this.textBox47.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox47.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox47.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox47.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox47.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox47.StyleName = "";
            this.textBox47.Value = "=PatientNameUnitDead";
            // 
            // textBox52
            // 
            this.textBox52.Format = "{0:0}";
            this.textBox52.Name = "textBox52";
            this.textBox52.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.37499940395355225), Telerik.Reporting.Drawing.Unit.Inch(0.22395825386047363));
            this.textBox52.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox52.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox52.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox52.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox52.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox52.StyleName = "";
            this.textBox52.Value = "=[<48]";
            // 
            // textBox56
            // 
            this.textBox56.Format = "{0:0}";
            this.textBox56.Name = "textBox56";
            this.textBox56.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.30208185315132141), Telerik.Reporting.Drawing.Unit.Inch(0.22395825386047363));
            this.textBox56.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox56.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox56.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox56.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox56.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox56.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox56.StyleName = "";
            this.textBox56.Value = "=[>48]";
            // 
            // DailyCensusOfPatientsInCareRpt
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail,
            this.pageFooter,
            this.reportFooterSection1});
            this.Name = "DailyCensusOfPatientsInCareRpt";
            this.PageSettings.Landscape = true;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Cm(0.5);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Cm(0.5);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Cm(0.5);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(0.5);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(28.699901580810547);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox txtdate;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox28;
        private ReportFooterSection reportFooterSection1;
        private Shape shape6;
        private Telerik.Reporting.TextBox textBox10;
        private Table table1;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox textBox35;
        private Telerik.Reporting.TextBox textBox39;
        private Telerik.Reporting.TextBox textBox43;
        private Telerik.Reporting.TextBox textBox33;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox37;
        private Telerik.Reporting.TextBox textBox41;
        private Telerik.Reporting.TextBox textBox47;
        private Telerik.Reporting.TextBox textBox52;
        private Telerik.Reporting.TextBox textBox56;
        private Telerik.Reporting.TextBox textBox45;
        private Telerik.Reporting.TextBox textBox49;
        private Telerik.Reporting.TextBox textBox54;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox48;
        private Telerik.Reporting.TextBox textBox55;
        private Telerik.Reporting.TextBox textBox62;
        private Telerik.Reporting.TextBox textBox64;
        private Telerik.Reporting.TextBox textBox69;
        private Telerik.Reporting.TextBox textBox63;
        private Telerik.Reporting.TextBox textBox71;
        private Telerik.Reporting.TextBox textBox16;
    }
}