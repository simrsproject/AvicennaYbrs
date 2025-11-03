namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSBHP
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
            this.reportNameTextBox = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.medicalNoCaptionTextBox = new Telerik.Reporting.TextBox();
            this.medicalNoDataTextBox = new Telerik.Reporting.TextBox();
            this.patientNameCaptionTextBox = new Telerik.Reporting.TextBox();
            this.patientNameDataTextBox = new Telerik.Reporting.TextBox();
            this.paramedicNameCaptionTextBox = new Telerik.Reporting.TextBox();
            this.paramedicNameDataTextBox = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699);
            this.pageHeader.Name = "pageHeader";
            // 
            // reportNameTextBox
            // 
            this.reportNameTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.099999986588954926), Telerik.Reporting.Drawing.Unit.Inch(3.9339065551757812E-05));
            this.reportNameTextBox.Name = "reportNameTextBox";
            this.reportNameTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7999998331069946), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.reportNameTextBox.Style.Font.Bold = true;
            this.reportNameTextBox.Style.Font.Name = "Tahoma";
            this.reportNameTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.reportNameTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.reportNameTextBox.Style.Visible = true;
            this.reportNameTextBox.StyleName = "PageInfo";
            this.reportNameTextBox.Value = "RSIA Buah Hati Pamulang";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(1.5479167699813843);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.currentTimeTextBox,
            this.medicalNoCaptionTextBox,
            this.medicalNoDataTextBox,
            this.patientNameCaptionTextBox,
            this.patientNameDataTextBox,
            this.paramedicNameCaptionTextBox,
            this.paramedicNameDataTextBox,
            this.textBox1,
            this.reportNameTextBox,
            this.textBox2,
            this.textBox3,
            this.textBox4,
            this.textBox5});
            this.detail.Name = "detail";
            // 
            // currentTimeTextBox
            // 
            this.currentTimeTextBox.Format = "{0:d MMMM yyyy HH:mm}";
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.75329089164733887), Telerik.Reporting.Drawing.Unit.Inch(0.25423455238342285));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2467092275619507), Telerik.Reporting.Drawing.Unit.Inch(0.1040378212928772));
            this.currentTimeTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(6);
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "=NOW()";
            // 
            // medicalNoCaptionTextBox
            // 
            this.medicalNoCaptionTextBox.CanGrow = true;
            this.medicalNoCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5894572413799324E-07), Telerik.Reporting.Drawing.Unit.Inch(0.43768024444580078));
            this.medicalNoCaptionTextBox.Name = "medicalNoCaptionTextBox";
            this.medicalNoCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.37291669845581055), Telerik.Reporting.Drawing.Unit.Inch(0.14999999105930328));
            this.medicalNoCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.medicalNoCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.medicalNoCaptionTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.medicalNoCaptionTextBox.StyleName = "Caption";
            this.medicalNoCaptionTextBox.Value = "No. RM";
            // 
            // medicalNoDataTextBox
            // 
            this.medicalNoDataTextBox.CanGrow = true;
            this.medicalNoDataTextBox.Format = "";
            this.medicalNoDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.42515763640403748), Telerik.Reporting.Drawing.Unit.Inch(0.43768024444580078));
            this.medicalNoDataTextBox.Name = "medicalNoDataTextBox";
            this.medicalNoDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5748424530029297), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.medicalNoDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.medicalNoDataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.medicalNoDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.medicalNoDataTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.medicalNoDataTextBox.StyleName = "Data";
            this.medicalNoDataTextBox.Value = "=Fields.MedicalNo";
            // 
            // patientNameCaptionTextBox
            // 
            this.patientNameCaptionTextBox.CanGrow = true;
            this.patientNameCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5894572413799324E-07), Telerik.Reporting.Drawing.Unit.Inch(0.58775883913040161));
            this.patientNameCaptionTextBox.Name = "patientNameCaptionTextBox";
            this.patientNameCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.37291669845581055), Telerik.Reporting.Drawing.Unit.Inch(0.20000003278255463));
            this.patientNameCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.patientNameCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.patientNameCaptionTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.patientNameCaptionTextBox.StyleName = "Caption";
            this.patientNameCaptionTextBox.Value = "Nama";
            // 
            // patientNameDataTextBox
            // 
            this.patientNameDataTextBox.CanGrow = false;
            this.patientNameDataTextBox.Format = "";
            this.patientNameDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.42515763640403748), Telerik.Reporting.Drawing.Unit.Inch(0.58775883913040161));
            this.patientNameDataTextBox.Name = "patientNameDataTextBox";
            this.patientNameDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5748425722122192), Telerik.Reporting.Drawing.Unit.Inch(0.2800000011920929));
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
            this.paramedicNameCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5894572413799324E-07), Telerik.Reporting.Drawing.Unit.Inch(0.86783772706985474));
            this.paramedicNameCaptionTextBox.Name = "paramedicNameCaptionTextBox";
            this.paramedicNameCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.37291669845581055), Telerik.Reporting.Drawing.Unit.Inch(0.20000003278255463));
            this.paramedicNameCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.paramedicNameCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.paramedicNameCaptionTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.paramedicNameCaptionTextBox.StyleName = "Caption";
            this.paramedicNameCaptionTextBox.Value = "Dokter";
            // 
            // paramedicNameDataTextBox
            // 
            this.paramedicNameDataTextBox.CanGrow = false;
            this.paramedicNameDataTextBox.Format = "";
            this.paramedicNameDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.42515763640403748), Telerik.Reporting.Drawing.Unit.Inch(0.86783772706985474));
            this.paramedicNameDataTextBox.Name = "paramedicNameDataTextBox";
            this.paramedicNameDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5748425722122192), Telerik.Reporting.Drawing.Unit.Inch(0.2800000011920929));
            this.paramedicNameDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.paramedicNameDataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.paramedicNameDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.paramedicNameDataTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.paramedicNameDataTextBox.StyleName = "Data";
            this.paramedicNameDataTextBox.Value = "=Fields.ParamedicName";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = false;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.384185791015625E-07), Telerik.Reporting.Drawing.Unit.Inch(1.1479166746139526));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.9999998807907105), Telerik.Reporting.Drawing.Unit.Inch(0.39825773239135742));
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Value = "=Fields.Notes";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.099999986588954926), Telerik.Reporting.Drawing.Unit.Inch(0.15011803805828095));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.8000001907348633), Telerik.Reporting.Drawing.Unit.Inch(0.1040378212928772));
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(6);
            this.textBox2.Style.Visible = true;
            this.textBox2.StyleName = "PageInfo";
            this.textBox2.Value = "Tracer Data Rekam Medik Pasien";
            // 
            // textBox3
            // 
            this.textBox3.CanGrow = true;
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.372995525598526), Telerik.Reporting.Drawing.Unit.Inch(0.43768015503883362));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699), Telerik.Reporting.Drawing.Unit.Inch(0.14999999105930328));
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox3.StyleName = "Caption";
            this.textBox3.Value = ":";
            // 
            // textBox4
            // 
            this.textBox4.CanGrow = true;
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.37299561500549316), Telerik.Reporting.Drawing.Unit.Inch(0.58775883913040161));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699), Telerik.Reporting.Drawing.Unit.Inch(0.14999999105930328));
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox4.StyleName = "Caption";
            this.textBox4.Value = ":";
            // 
            // textBox5
            // 
            this.textBox5.CanGrow = true;
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.37299561500549316), Telerik.Reporting.Drawing.Unit.Inch(0.86783772706985474));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699), Telerik.Reporting.Drawing.Unit.Inch(0.14999999105930328));
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox5.StyleName = "Caption";
            this.textBox5.Value = ":";
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
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(2.2440946102142334);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox reportNameTextBox;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.TextBox medicalNoCaptionTextBox;
        private Telerik.Reporting.TextBox medicalNoDataTextBox;
        private Telerik.Reporting.TextBox patientNameCaptionTextBox;
        private Telerik.Reporting.TextBox patientNameDataTextBox;
        private Telerik.Reporting.TextBox paramedicNameCaptionTextBox;
        private Telerik.Reporting.TextBox paramedicNameDataTextBox;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
    }
}