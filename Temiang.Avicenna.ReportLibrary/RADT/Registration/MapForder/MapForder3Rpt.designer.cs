namespace Temiang.Avicenna.ReportLibrary.Registration
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class MapForder3Rpt
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.detail = new Telerik.Reporting.DetailSection();
            this.txtPage3a = new Telerik.Reporting.TextBox();
            this.txtPage3b = new Telerik.Reporting.Barcode();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.Height = new Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Cm);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtPage3a,
            this.txtPage3b});
            this.detail.Name = "detail";
            // 
            // txtPage3a
            // 
            this.txtPage3a.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.058333396911621094, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.15000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
            this.txtPage3a.Name = "txtPage3a";
            this.txtPage3a.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.3120832443237305, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.7089582681655884, Telerik.Reporting.Drawing.UnitType.Cm));
            this.txtPage3a.Style.Font.Bold = true;
            this.txtPage3a.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(16, Telerik.Reporting.Drawing.UnitType.Point);
            this.txtPage3a.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtPage3a.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.txtPage3a.Value = "= MedicalNo";
            // 
            // txtPage3b
            // 
            this.txtPage3b.BarAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtPage3b.Checksum = false;
            this.txtPage3b.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.7916666269302368, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2083333283662796, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtPage3b.Name = "txtPage3b";
            this.txtPage3b.ShowText = false;
            this.txtPage3b.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.8622915744781494, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.1004166603088379, Telerik.Reporting.Drawing.UnitType.Cm));
            this.txtPage3b.Stretch = true;
            this.txtPage3b.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(14, Telerik.Reporting.Drawing.UnitType.Point);
            this.txtPage3b.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtPage3b.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.txtPage3b.Style.Visible = true;
            this.txtPage3b.Symbology = Telerik.Reporting.Barcode.SymbologyType.Code39;
            this.txtPage3b.Value = "= BarcodeMedicalNo";
            // 
            // MapForder3Rpt
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detail});
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Inch);
            this.PageSettings.Margins.Left = new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Inch);
            this.PageSettings.Margins.Right = new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Inch);
            this.PageSettings.Margins.Top = new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Inch);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageSettings.PaperSize = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(8.8000001907348633, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Cm));
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = new Telerik.Reporting.Drawing.Unit(8.8000001907348633, Telerik.Reporting.Drawing.UnitType.Cm);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox txtPage3a;
        private Barcode txtPage3b;
    }
}