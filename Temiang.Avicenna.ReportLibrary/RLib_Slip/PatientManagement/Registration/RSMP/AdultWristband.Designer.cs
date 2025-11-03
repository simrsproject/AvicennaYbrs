namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSMP
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class AdultWristband
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.barcode2 = new Telerik.Reporting.Barcode();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(9.1440000534057617);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox2,
            this.barcode2,
            this.textBox3,
            this.textBox4,
            this.textBox13,
            this.textBox5});
            this.detail.Name = "detail";
            // 
            // textBox1
            // 
            this.textBox1.Angle = 90;
            this.textBox1.CanGrow = false;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.49999997019767761), Telerik.Reporting.Drawing.Unit.Inch(-4.8656804280122756E-11));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.15267695486545563), Telerik.Reporting.Drawing.Unit.Inch(2.0999212265014648));
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox1.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox1.Value = "=PatientName";
            // 
            // textBox2
            // 
            this.textBox2.Angle = 90;
            this.textBox2.CanGrow = false;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.2000393271446228), Telerik.Reporting.Drawing.Unit.Inch(0.50003945827484131));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(3.8779945373535156), Telerik.Reporting.Drawing.Unit.Inch(0.84991806745529175));
            this.textBox2.Style.Font.Bold = false;
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox2.Value = "=MedicalNo";
            // 
            // barcode2
            // 
            this.barcode2.Angle = 90;
            this.barcode2.BarAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.barcode2.Checksum = false;
            this.barcode2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.044567201286554337), Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421));
            this.barcode2.Name = "barcode2";
            this.barcode2.ShowText = false;
            this.barcode2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1), Telerik.Reporting.Drawing.Unit.Cm(5.3299999237060547));
            this.barcode2.Stretch = true;
            this.barcode2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.barcode2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.barcode2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.barcode2.Symbology = Telerik.Reporting.Barcode.SymbologyType.Code39Extended;
            this.barcode2.Value = "=MedicalNo";
            // 
            // textBox3
            // 
            this.textBox3.Angle = 90;
            this.textBox3.CanGrow = false;
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.2000393271446228), Telerik.Reporting.Drawing.Unit.Inch(-4.8656804280122756E-11));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.15267695486545563), Telerik.Reporting.Drawing.Unit.Inch(0.49996057152748108));
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox3.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox3.Value = "No RM :";
            // 
            // textBox4
            // 
            this.textBox4.Angle = 90;
            this.textBox4.CanGrow = false;
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.35279503464698792), Telerik.Reporting.Drawing.Unit.Inch(-4.8656804280122756E-11));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.14712625741958618), Telerik.Reporting.Drawing.Unit.Inch(0.59988182783126831));
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox4.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox4.Value = "Tgl Lahir :";
            // 
            // textBox13
            // 
            this.textBox13.Angle = 90;
            this.textBox13.CanGrow = false;
            this.textBox13.Format = "{0:dd-MM-yyyy}";
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.35271614789962769), Telerik.Reporting.Drawing.Unit.Inch(0.59996062517166138));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.14720506966114044), Telerik.Reporting.Drawing.Unit.Inch(0.7499966025352478));
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox13.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox13.Value = "=DateOfBirth";
            // 
            // textBox5
            // 
            this.textBox5.Angle = 90;
            this.textBox5.CanGrow = false;
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.65275579690933228), Telerik.Reporting.Drawing.Unit.Inch(-4.8656804280122756E-11));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.15267695486545563), Telerik.Reporting.Drawing.Unit.Inch(2.0999212265014648));
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox5.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox5.Value = "RS MUHAMMADIYAH PALEMBANG";
            // 
            // AdultWristband
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detail});
            this.Name = "AdultWristband";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Mm(0);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Mm(0);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageSettings.PaperSize = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.89999997615814209), Telerik.Reporting.Drawing.Unit.Inch(5.5));
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.UnitOfMeasure = Telerik.Reporting.Drawing.UnitType.Inch;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(0.89999997615814209);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Barcode barcode2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox5;
    }
}