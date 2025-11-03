namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSBHP
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class RegistrationLabelRpt
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.detail = new Telerik.Reporting.DetailSection();
            this.medicalNoCaptionTextBox = new Telerik.Reporting.TextBox();
            this.patientNameCaptionTextBox = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.medicalNoDataTextBox = new Telerik.Reporting.TextBox();
            this.patientNameDataTextBox = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Mm(25.659999847412109);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.medicalNoCaptionTextBox,
            this.patientNameCaptionTextBox,
            this.textBox2,
            this.textBox3,
            this.medicalNoDataTextBox,
            this.patientNameDataTextBox,
            this.textBox1,
            this.textBox4,
            this.textBox5,
            this.textBox6});
            this.detail.Name = "detail";
            // 
            // medicalNoCaptionTextBox
            // 
            this.medicalNoCaptionTextBox.CanGrow = false;
            this.medicalNoCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.11462271213531494), Telerik.Reporting.Drawing.Unit.Inch(0.20837266743183136));
            this.medicalNoCaptionTextBox.Name = "medicalNoCaptionTextBox";
            this.medicalNoCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.44999998807907104), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.medicalNoCaptionTextBox.Style.Font.Name = "Tahoma";
            this.medicalNoCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.medicalNoCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.medicalNoCaptionTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.medicalNoCaptionTextBox.StyleName = "Caption";
            this.medicalNoCaptionTextBox.Value = "No. RM";
            // 
            // patientNameCaptionTextBox
            // 
            this.patientNameCaptionTextBox.CanGrow = false;
            this.patientNameCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.11462271213531494), Telerik.Reporting.Drawing.Unit.Inch(0.35845136642456055));
            this.patientNameCaptionTextBox.Name = "patientNameCaptionTextBox";
            this.patientNameCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.44999998807907104), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.patientNameCaptionTextBox.Style.Font.Name = "Tahoma";
            this.patientNameCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.patientNameCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.patientNameCaptionTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.patientNameCaptionTextBox.StyleName = "Caption";
            this.patientNameCaptionTextBox.Value = "Nama";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = false;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.11462271213531494), Telerik.Reporting.Drawing.Unit.Inch(0.50853008031845093));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.44999998807907104), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.textBox2.Style.Font.Name = "Tahoma";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox2.StyleName = "Caption";
            this.textBox2.Value = "Usia/tgl";
            // 
            // textBox3
            // 
            this.textBox3.CanGrow = false;
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.11462271213531494), Telerik.Reporting.Drawing.Unit.Inch(0.65860873460769653));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.44999998807907104), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448));
            this.textBox3.Style.Font.Name = "Tahoma";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox3.StyleName = "Caption";
            this.textBox3.Value = "Bayar";
            // 
            // medicalNoDataTextBox
            // 
            this.medicalNoDataTextBox.CanGrow = false;
            this.medicalNoDataTextBox.Format = ": {0}";
            this.medicalNoDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.56470078229904175), Telerik.Reporting.Drawing.Unit.Inch(0.20837266743183136));
            this.medicalNoDataTextBox.Name = "medicalNoDataTextBox";
            this.medicalNoDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.4806995391845703), Telerik.Reporting.Drawing.Unit.Inch(0.14999999105930328));
            this.medicalNoDataTextBox.Style.Font.Name = "Tahoma";
            this.medicalNoDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.medicalNoDataTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.medicalNoDataTextBox.StyleName = "Data";
            this.medicalNoDataTextBox.Value = "=Fields.MedicalNo +\'[\' + Fields.RegistrationNo +\']\'";
            // 
            // patientNameDataTextBox
            // 
            this.patientNameDataTextBox.CanGrow = false;
            this.patientNameDataTextBox.Format = ": {0}";
            this.patientNameDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.56470078229904175), Telerik.Reporting.Drawing.Unit.Inch(0.35845136642456055));
            this.patientNameDataTextBox.Name = "patientNameDataTextBox";
            this.patientNameDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1806991100311279), Telerik.Reporting.Drawing.Unit.Inch(0.14999999105930328));
            this.patientNameDataTextBox.Style.Font.Name = "Tahoma";
            this.patientNameDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.patientNameDataTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.patientNameDataTextBox.StyleName = "Data";
            this.patientNameDataTextBox.Value = "=Fields.PatientName";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = false;
            this.textBox1.Format = ": {0}";
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.56470078229904175), Telerik.Reporting.Drawing.Unit.Inch(0.50853008031845093));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0498819351196289), Telerik.Reporting.Drawing.Unit.Inch(0.15000002086162567));
            this.textBox1.Style.Font.Name = "Tahoma";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox1.Value = "=Fields.Age";
            // 
            // textBox4
            // 
            this.textBox4.CanGrow = false;
            this.textBox4.Format = "( {0:dd-MMM-yyyy} )";
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.718828558921814), Telerik.Reporting.Drawing.Unit.Inch(0.50853008031845093));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3265719413757324), Telerik.Reporting.Drawing.Unit.Inch(0.15000002086162567));
            this.textBox4.Style.Font.Name = "Tahoma";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox4.Value = "= Fields.DateOfBirth";
            // 
            // textBox5
            // 
            this.textBox5.CanGrow = false;
            this.textBox5.Format = ": {0}";
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.5647013783454895), Telerik.Reporting.Drawing.Unit.Inch(0.65860873460769653));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.4806993007659912), Telerik.Reporting.Drawing.Unit.Inch(0.15000002086162567));
            this.textBox5.Style.Font.Name = "Tahoma";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox5.Value = "=Fields.GuarantorName";
            // 
            // textBox6
            // 
            this.textBox6.CanGrow = false;
            this.textBox6.Format = "( {0} )";
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.745478630065918), Telerik.Reporting.Drawing.Unit.Inch(0.35845136642456055));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.29992198944091797), Telerik.Reporting.Drawing.Unit.Inch(0.15000002086162567));
            this.textBox6.Style.Font.Name = "Tahoma";
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox6.Value = "=Fields.Sex";
            // 
            // RegistrationLabelRpt
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detail});
            this.Name = "RegistrationLabelIpRpt";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Mm(0);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Mm(0);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Mm(0);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Mm(0);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageSettings.PaperSize = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8), Telerik.Reporting.Drawing.Unit.Cm(2.5659999847412109));
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.UnitOfMeasure = Telerik.Reporting.Drawing.UnitType.Cm;
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(8);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DetailSection detail;
        private Telerik.Reporting.TextBox medicalNoCaptionTextBox;
        private Telerik.Reporting.TextBox patientNameCaptionTextBox;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox medicalNoDataTextBox;
        private Telerik.Reporting.TextBox patientNameDataTextBox;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox6;

    }
}