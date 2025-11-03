namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSBHP
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class PatientIdCard
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.barcode2 = new Telerik.Reporting.Barcode();
            this.reportHeaderSection1 = new Telerik.Reporting.ReportHeaderSection();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.25399935245513916);
            this.detail.Name = "detail";
            this.detail.Style.Visible = false;
            // 
            // textBox5
            // 
            this.textBox5.Angle = 0;
            this.textBox5.CanGrow = false;
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609), Telerik.Reporting.Drawing.Unit.Inch(0.99992108345031738));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.8999998569488525), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox5.Style.Font.Bold = false;
            this.textBox5.Style.Font.Name = "Tahoma";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox5.Style.Visible = true;
            this.textBox5.Value = "=MedicalNo";
            // 
            // textBox1
            // 
            this.textBox1.Angle = 0;
            this.textBox1.CanGrow = false;
            this.textBox1.CanShrink = false;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612), Telerik.Reporting.Drawing.Unit.Inch(1.2000000476837158));
            this.textBox1.Name = "textBox5";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.0495274066925049), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Tahoma";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2);
            this.textBox1.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.textBox1.Value = "=PatientName";
            // 
            // barcode2
            // 
            this.barcode2.Angle = 0;
            this.barcode2.BarAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.barcode2.Checksum = false;
            this.barcode2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612), Telerik.Reporting.Drawing.Unit.Inch(1.7500787973403931));
            this.barcode2.Name = "barcode2";
            this.barcode2.ShowText = false;
            this.barcode2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.4000000953674316), Telerik.Reporting.Drawing.Unit.Inch(0.25));
            this.barcode2.Stretch = true;
            this.barcode2.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.barcode2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.barcode2.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2);
            this.barcode2.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.barcode2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.barcode2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.barcode2.Style.Visible = true;
            this.barcode2.Symbology = Telerik.Reporting.Barcode.SymbologyType.Code128;
            this.barcode2.Value = "=BarcodeMedicalNo";
            // 
            // reportHeaderSection1
            // 
            this.reportHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(2.0000789165496826);
            this.reportHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox5,
            this.barcode2,
            this.textBox2,
            this.textBox3});
            this.reportHeaderSection1.Name = "reportHeaderSection1";
            // 
            // textBox2
            // 
            this.textBox2.Angle = 0;
            this.textBox2.CanGrow = false;
            this.textBox2.CanShrink = false;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609), Telerik.Reporting.Drawing.Unit.Inch(1.4499212503433228));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.4000000953674316), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.textBox2.Style.Font.Bold = false;
            this.textBox2.Style.Font.Name = "Tahoma";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox2.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2);
            this.textBox2.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.textBox2.Value = "=Fields.StreetName";
            // 
            // textBox3
            // 
            this.textBox3.Angle = 0;
            this.textBox3.CanGrow = false;
            this.textBox3.CanShrink = false;
            this.textBox3.Format = "{0:dd MMMM yyyy}";
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612), Telerik.Reporting.Drawing.Unit.Inch(1.6000000238418579));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.4000000953674316), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.textBox3.Style.Font.Bold = false;
            this.textBox3.Style.Font.Name = "Tahoma";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox3.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2);
            this.textBox3.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.textBox3.Value = "=Fields.DateOfBirth";
            // 
            // PatientIdCard
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detail,
            this.reportHeaderSection1});
            this.Name = "GPIPatientIdCard";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Cm(0);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Mm(5);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Cm(0);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(0);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageSettings.PaperSize = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.5), Telerik.Reporting.Drawing.Unit.Cm(5.3000001907348633));
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(3.1495668888092041);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox1;
        private Barcode barcode2;
        private ReportHeaderSection reportHeaderSection1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
    }
}