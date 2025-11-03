namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSMP
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
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.reportHeaderSection1 = new Telerik.Reporting.ReportHeaderSection();
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
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.60007864236831665), Telerik.Reporting.Drawing.Unit.Inch(0.20007865130901337));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.8998420238494873), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Font.Name = "Arial";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
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
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(1.3245476715439963E-08));
            this.textBox1.Name = "textBox5";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.4998815059661865), Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Arial";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
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
            this.barcode2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.40015730261802673));
            this.barcode2.Name = "barcode2";
            this.barcode2.ShowText = false;
            this.barcode2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.3299999237060547), Telerik.Reporting.Drawing.Unit.Cm(1));
            this.barcode2.Stretch = true;
            this.barcode2.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.barcode2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.barcode2.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2);
            this.barcode2.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.barcode2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.barcode2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.barcode2.Style.Visible = true;
            this.barcode2.Symbology = Telerik.Reporting.Barcode.SymbologyType.Code93;
            this.barcode2.Value = "=MedicalNo";
            // 
            // textBox2
            // 
            this.textBox2.Angle = 0;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.2000785619020462));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.5999605655670166), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Name = "Arial";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox2.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2);
            this.textBox2.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox2.Value = "No RM :";
            // 
            // reportHeaderSection1
            // 
            this.reportHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929);
            this.reportHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox2,
            this.textBox1,
            this.textBox5,
            this.barcode2});
            this.reportHeaderSection1.Name = "reportHeaderSection1";
            // 
            // PatientIdCard
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detail,
            this.reportHeaderSection1});
            this.Name = "GPIPatientIdCard";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Cm(0);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Cm(0);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Cm(0);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(0);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageSettings.PaperSize = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.5), Telerik.Reporting.Drawing.Unit.Inch(1));
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(3.4999604225158691);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox1;
        private Barcode barcode2;
        private Telerik.Reporting.TextBox textBox2;
        private ReportHeaderSection reportHeaderSection1;
    }
}