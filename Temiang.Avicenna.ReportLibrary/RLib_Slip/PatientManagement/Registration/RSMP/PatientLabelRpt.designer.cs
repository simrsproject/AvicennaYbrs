namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSMP
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class PatientLabelRpt
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.reportHeaderSection1 = new Telerik.Reporting.ReportHeaderSection();
            this.textBox2 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Mm(2.5399994850158691);
            this.detail.Name = "detail";
            this.detail.Style.Visible = false;
            // 
            // textBox13
            // 
            this.textBox13.CanGrow = false;
            this.textBox13.Format = "{0:dd-MM-yyyy}";
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.90011817216873169), Telerik.Reporting.Drawing.Unit.Inch(0.26007875800132751));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.79962730407714844), Telerik.Reporting.Drawing.Unit.Inch(0.16180235147476196));
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.5);
            this.textBox13.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox13.Value = "=DateOfBirth";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = false;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.90011817216873169), Telerik.Reporting.Drawing.Unit.Inch(0.099999986588954926));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.5998423099517822), Telerik.Reporting.Drawing.Unit.Inch(0.15999999642372131));
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.5);
            this.textBox1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox1.Value = "=PatientName";
            // 
            // textBox4
            // 
            this.textBox4.CanGrow = false;
            this.textBox4.Format = "{0:dd-MM-yyyy}";
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.26007875800132751));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.79996061325073242), Telerik.Reporting.Drawing.Unit.Inch(0.16180242598056793));
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.5);
            this.textBox4.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox4.Value = "Tanggal lahir";
            // 
            // textBox5
            // 
            this.textBox5.CanGrow = false;
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.42195996642112732));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929), Telerik.Reporting.Drawing.Unit.Inch(0.161802276968956));
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.5);
            this.textBox5.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox5.Value = "No.RM";
            // 
            // textBox6
            // 
            this.textBox6.CanGrow = false;
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.099999986588954926));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.79996061325073242), Telerik.Reporting.Drawing.Unit.Inch(0.16000007092952728));
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.5);
            this.textBox6.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox6.Value = "Nama Pasien";
            // 
            // textBox7
            // 
            this.textBox7.CanGrow = false;
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.80007869005203247), Telerik.Reporting.Drawing.Unit.Inch(0.10180243104696274));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099960647523403168), Telerik.Reporting.Drawing.Unit.Inch(0.15999998152256012));
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.5);
            this.textBox7.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.Value = ":";
            // 
            // textBox8
            // 
            this.textBox8.CanGrow = false;
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.80007869005203247), Telerik.Reporting.Drawing.Unit.Inch(0.421959787607193));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099960647523403168), Telerik.Reporting.Drawing.Unit.Inch(0.16180258989334106));
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.5);
            this.textBox8.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Value = ":";
            // 
            // textBox9
            // 
            this.textBox9.CanGrow = false;
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.80007869005203247), Telerik.Reporting.Drawing.Unit.Inch(0.26368379592895508));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099960647523403168), Telerik.Reporting.Drawing.Unit.Inch(0.15819729864597321));
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.5);
            this.textBox9.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox9.Value = ":";
            // 
            // reportHeaderSection1
            // 
            this.reportHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.60000008344650269);
            this.reportHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox13,
            this.textBox5,
            this.textBox6,
            this.textBox4,
            this.textBox8,
            this.textBox7,
            this.textBox9,
            this.textBox2});
            this.reportHeaderSection1.Name = "reportHeaderSection1";
            // 
            // textBox2
            // 
            this.textBox2.Angle = 0;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.90011817216873169), Telerik.Reporting.Drawing.Unit.Inch(0.421959787607193));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.5998423099517822), Telerik.Reporting.Drawing.Unit.Inch(0.16180248558521271));
            this.textBox2.Style.Font.Bold = false;
            this.textBox2.Style.Font.Name = "Arial";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.5);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox2.Style.Visible = true;
            this.textBox2.Value = "=MedicalNo";
            // 
            // PatientLabelRpt
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detail,
            this.reportHeaderSection1});
            this.Name = "PatientLabelRpt";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageSettings.PaperSize = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.5), Telerik.Reporting.Drawing.Unit.Inch(0.699999988079071));
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.UnitOfMeasure = Telerik.Reporting.Drawing.UnitType.Cm;
            this.Width = Telerik.Reporting.Drawing.Unit.Mm(88.899993896484375);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DetailSection detail;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox9;
        private ReportHeaderSection reportHeaderSection1;
        private Telerik.Reporting.TextBox textBox2;

    }
}