namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSIAMTP
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
            this.txtPage3b = new Telerik.Reporting.Barcode();
            this.textBox69 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(2);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtPage3b,
            this.textBox69,
            this.textBox1,
            this.textBox2,
            this.textBox3});
            this.detail.Name = "detail";
            // 
            // txtPage3b
            // 
            this.txtPage3b.BarAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtPage3b.Checksum = false;
            this.txtPage3b.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.099999904632568359), Telerik.Reporting.Drawing.Unit.Inch(0.099999986588954926));
            this.txtPage3b.Name = "txtPage3b";
            this.txtPage3b.ShowText = false;
            this.txtPage3b.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.0480003356933594), Telerik.Reporting.Drawing.Unit.Cm(1.5240001678466797));
            this.txtPage3b.Stretch = true;
            this.txtPage3b.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14);
            this.txtPage3b.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtPage3b.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.txtPage3b.Style.Visible = true;
            this.txtPage3b.Symbology = Telerik.Reporting.Barcode.SymbologyType.Code39;
            this.txtPage3b.Value = "=MedicalNo";
            // 
            // textBox69
            // 
            this.textBox69.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3000788688659668), Telerik.Reporting.Drawing.Unit.Inch(0.099999986588954926));
            this.textBox69.Name = "textBox69";
            this.textBox69.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.0999212265014648), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox69.Style.Font.Bold = true;
            this.textBox69.Style.Font.Name = "Tahoma";
            this.textBox69.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox69.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox69.Value = "=MedicalNo";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3000789880752564), Telerik.Reporting.Drawing.Unit.Inch(0.3000786304473877));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.0999212265014648), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Tahoma";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox1.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox1.Value = "=PatientName";
            // 
            // textBox2
            // 
            this.textBox2.Format = "{0:dd-MM-yyyy}";
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3000789880752564), Telerik.Reporting.Drawing.Unit.Inch(0.50000005960464478));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.89992111921310425), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox2.Style.Font.Name = "Tahoma";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox2.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox2.Value = "=RegistrationDate";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.2000789642333984), Telerik.Reporting.Drawing.Unit.Inch(0.50015729665756226));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.89992111921310425), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox3.Style.Font.Name = "Tahoma";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox3.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox3.Value = "=Age";
            // 
            // AdultWristband
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detail});
            this.Name = "AdultWristband";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageSettings.PaperSize = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.8000001907348633), Telerik.Reporting.Drawing.Unit.Cm(2));
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(8.8000001907348633);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.DetailSection detail;
        private Barcode txtPage3b;
        private Telerik.Reporting.TextBox textBox69;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
    }
}