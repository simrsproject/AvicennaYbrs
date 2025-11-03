namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSSMCB
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class Tracer
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
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.medicalNoCaptionTextBox = new Telerik.Reporting.TextBox();
            this.medicalNoDataTextBox = new Telerik.Reporting.TextBox();
            this.patientNameCaptionTextBox = new Telerik.Reporting.TextBox();
            this.patientNameDataTextBox = new Telerik.Reporting.TextBox();
            this.paramedicNameCaptionTextBox = new Telerik.Reporting.TextBox();
            this.paramedicNameDataTextBox = new Telerik.Reporting.TextBox();
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
            this.textBox1 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699);
            this.pageHeader.Name = "pageHeader";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.947916567325592);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.currentTimeTextBox,
            this.medicalNoCaptionTextBox,
            this.medicalNoDataTextBox,
            this.patientNameCaptionTextBox,
            this.patientNameDataTextBox,
            this.paramedicNameCaptionTextBox,
            this.paramedicNameDataTextBox,
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
            this.textBox1});
            this.detail.Name = "detail";
            // 
            // currentTimeTextBox
            // 
            this.currentTimeTextBox.Format = "{0:dd/MM/yyyy}";
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1999999284744263), Telerik.Reporting.Drawing.Unit.Inch(3.9365557313431054E-05));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1468666791915894), Telerik.Reporting.Drawing.Unit.Inch(0.15191513299942017));
            this.currentTimeTextBox.Style.Font.Name = "Arial";
            this.currentTimeTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.currentTimeTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "=Tanggal";
            // 
            // medicalNoCaptionTextBox
            // 
            this.medicalNoCaptionTextBox.CanGrow = true;
            this.medicalNoCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.15203322470188141));
            this.medicalNoCaptionTextBox.Name = "medicalNoCaptionTextBox";
            this.medicalNoCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.49996054172515869), Telerik.Reporting.Drawing.Unit.Inch(0.14999999105930328));
            this.medicalNoCaptionTextBox.Style.Font.Name = "Arial";
            this.medicalNoCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.medicalNoCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.medicalNoCaptionTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.medicalNoCaptionTextBox.StyleName = "Caption";
            this.medicalNoCaptionTextBox.Value = "No. RM";
            // 
            // medicalNoDataTextBox
            // 
            this.medicalNoDataTextBox.CanGrow = true;
            this.medicalNoDataTextBox.Format = "";
            this.medicalNoDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.55231952667236328), Telerik.Reporting.Drawing.Unit.Inch(0.15203322470188141));
            this.medicalNoDataTextBox.Name = "medicalNoDataTextBox";
            this.medicalNoDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1476802825927734), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.medicalNoDataTextBox.Style.Font.Name = "Arial";
            this.medicalNoDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.medicalNoDataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.medicalNoDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.medicalNoDataTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.medicalNoDataTextBox.StyleName = "Data";
            this.medicalNoDataTextBox.Value = "=Fields.MedicalNo";
            // 
            // patientNameCaptionTextBox
            // 
            this.patientNameCaptionTextBox.CanGrow = true;
            this.patientNameCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.30211198329925537));
            this.patientNameCaptionTextBox.Name = "patientNameCaptionTextBox";
            this.patientNameCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.50003916025161743), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.patientNameCaptionTextBox.Style.Font.Name = "Arial";
            this.patientNameCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.patientNameCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.patientNameCaptionTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.patientNameCaptionTextBox.StyleName = "Caption";
            this.patientNameCaptionTextBox.Value = "Nama";
            // 
            // patientNameDataTextBox
            // 
            this.patientNameDataTextBox.CanGrow = false;
            this.patientNameDataTextBox.Format = "";
            this.patientNameDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.55231952667236328), Telerik.Reporting.Drawing.Unit.Inch(0.30211198329925537));
            this.patientNameDataTextBox.Name = "patientNameDataTextBox";
            this.patientNameDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7945472002029419), Telerik.Reporting.Drawing.Unit.Inch(0.14999999105930328));
            this.patientNameDataTextBox.Style.Font.Name = "Arial";
            this.patientNameDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.patientNameDataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.patientNameDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.patientNameDataTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.patientNameDataTextBox.StyleName = "Data";
            this.patientNameDataTextBox.Value = "=Fields.PatientName";
            // 
            // paramedicNameCaptionTextBox
            // 
            this.paramedicNameCaptionTextBox.CanGrow = true;
            this.paramedicNameCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.60226958990097046));
            this.paramedicNameCaptionTextBox.Name = "paramedicNameCaptionTextBox";
            this.paramedicNameCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.49999994039535522), Telerik.Reporting.Drawing.Unit.Inch(0.14564695954322815));
            this.paramedicNameCaptionTextBox.Style.Font.Name = "Arial";
            this.paramedicNameCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.paramedicNameCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.paramedicNameCaptionTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.paramedicNameCaptionTextBox.StyleName = "Caption";
            this.paramedicNameCaptionTextBox.Value = "Dokter";
            // 
            // paramedicNameDataTextBox
            // 
            this.paramedicNameDataTextBox.CanGrow = false;
            this.paramedicNameDataTextBox.Format = "";
            this.paramedicNameDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.55231952667236328), Telerik.Reporting.Drawing.Unit.Inch(0.60226947069168091));
            this.paramedicNameDataTextBox.Name = "paramedicNameDataTextBox";
            this.paramedicNameDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7945472002029419), Telerik.Reporting.Drawing.Unit.Inch(0.14564698934555054));
            this.paramedicNameDataTextBox.Style.Font.Name = "Arial";
            this.paramedicNameDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.paramedicNameDataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.paramedicNameDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.paramedicNameDataTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.paramedicNameDataTextBox.StyleName = "Data";
            this.paramedicNameDataTextBox.Value = "=Fields.ParamedicName";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.8625147580169141E-05), Telerik.Reporting.Drawing.Unit.Inch(3.9365557313431054E-05));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0999213457107544), Telerik.Reporting.Drawing.Unit.Inch(0.15191513299942017));
            this.textBox2.Style.Color = System.Drawing.Color.Black;
            this.textBox2.Style.Font.Name = "Arial";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.Style.Visible = true;
            this.textBox2.StyleName = "PageInfo";
            this.textBox2.Value = "=BookingNonBooking";
            // 
            // textBox3
            // 
            this.textBox3.CanGrow = true;
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.500157356262207), Telerik.Reporting.Drawing.Unit.Inch(0.15203322470188141));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699), Telerik.Reporting.Drawing.Unit.Inch(0.14999999105930328));
            this.textBox3.Style.Font.Name = "Arial";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox3.StyleName = "Caption";
            this.textBox3.Value = ":";
            // 
            // textBox4
            // 
            this.textBox4.CanGrow = true;
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.500157356262207), Telerik.Reporting.Drawing.Unit.Inch(0.30211198329925537));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699), Telerik.Reporting.Drawing.Unit.Inch(0.14999999105930328));
            this.textBox4.Style.Font.Name = "Arial";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox4.StyleName = "Caption";
            this.textBox4.Value = ":";
            // 
            // textBox5
            // 
            this.textBox5.CanGrow = true;
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.500157356262207), Telerik.Reporting.Drawing.Unit.Inch(0.60226958990097046));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699), Telerik.Reporting.Drawing.Unit.Inch(0.14564698934555054));
            this.textBox5.Style.Font.Name = "Arial";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox5.StyleName = "Caption";
            this.textBox5.Value = ":";
            // 
            // textBox6
            // 
            this.textBox6.CanGrow = true;
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.500157356262207), Telerik.Reporting.Drawing.Unit.Inch(0.45219075679779053));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699), Telerik.Reporting.Drawing.Unit.Inch(0.14999993145465851));
            this.textBox6.Style.Font.Name = "Arial";
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox6.StyleName = "Caption";
            this.textBox6.Value = ":";
            // 
            // textBox7
            // 
            this.textBox7.CanGrow = false;
            this.textBox7.Format = "";
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.55231952667236328), Telerik.Reporting.Drawing.Unit.Inch(0.45219060778617859));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7945472002029419), Telerik.Reporting.Drawing.Unit.Inch(0.14999999105930328));
            this.textBox7.Style.Font.Name = "Arial";
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox7.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox7.StyleName = "Data";
            this.textBox7.Value = "=Fields.ServiceUnitName";
            // 
            // textBox8
            // 
            this.textBox8.CanGrow = true;
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.45219075679779053));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.50003916025161743), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.textBox8.Style.Font.Name = "Arial";
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox8.StyleName = "Caption";
            this.textBox8.Value = "Poli";
            // 
            // textBox9
            // 
            this.textBox9.CanGrow = true;
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.500157356262207), Telerik.Reporting.Drawing.Unit.Inch(0.74799537658691406));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699), Telerik.Reporting.Drawing.Unit.Inch(0.14564698934555054));
            this.textBox9.Style.Font.Name = "Arial";
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox9.StyleName = "Caption";
            this.textBox9.Value = ":";
            // 
            // textBox10
            // 
            this.textBox10.CanGrow = false;
            this.textBox10.Format = "";
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.55231952667236328), Telerik.Reporting.Drawing.Unit.Inch(0.74799537658691406));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7945472002029419), Telerik.Reporting.Drawing.Unit.Inch(0.14564698934555054));
            this.textBox10.Style.Font.Name = "Arial";
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox10.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox10.StyleName = "Data";
            this.textBox10.Value = "=Fields.GuarantorName";
            // 
            // textBox11
            // 
            this.textBox11.CanGrow = true;
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.8625147580169141E-05), Telerik.Reporting.Drawing.Unit.Inch(0.74799537658691406));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.49999994039535522), Telerik.Reporting.Drawing.Unit.Inch(0.14564695954322815));
            this.textBox11.Style.Font.Name = "Arial";
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox11.StyleName = "Caption";
            this.textBox11.Value = "Penjamin";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Format = "";
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.7000787258148193), Telerik.Reporting.Drawing.Unit.Inch(0.152033269405365));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.64678788185119629), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.textBox1.Style.Font.Name = "Arial";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox1.StyleName = "Data";
            this.textBox1.Value = "=Fields.BaruLama";
            // 
            // Tracer
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail});
            this.Name = "Tracer";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Mm(0);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Mm(0);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageSettings.PaperSize = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.6999998092651367), Telerik.Reporting.Drawing.Unit.Cm(6));
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(2.3468666076660156);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.TextBox medicalNoCaptionTextBox;
        private Telerik.Reporting.TextBox medicalNoDataTextBox;
        private Telerik.Reporting.TextBox patientNameCaptionTextBox;
        private Telerik.Reporting.TextBox patientNameDataTextBox;
        private Telerik.Reporting.TextBox paramedicNameCaptionTextBox;
        private Telerik.Reporting.TextBox paramedicNameDataTextBox;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox1;
    }
}