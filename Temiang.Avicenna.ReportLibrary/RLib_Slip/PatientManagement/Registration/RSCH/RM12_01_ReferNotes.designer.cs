namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSCH
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class RM12_01_ReferNotes
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RM12_01_ReferNotes));
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.txtMedicalNo = new Telerik.Reporting.TextBox();
            this.txtPatientName = new Telerik.Reporting.TextBox();
            this.txtBirthDateAge = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.txtToSpecialty = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.txtDiagnosa = new Telerik.Reporting.TextBox();
            this.txtToParamedicName = new Telerik.Reporting.TextBox();
            this.textBox66 = new Telerik.Reporting.TextBox();
            this.textBox65 = new Telerik.Reporting.TextBox();
            this.textBox57 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.txtNotes = new Telerik.Reporting.TextBox();
            this.chkParamedicConsultType01 = new Telerik.Reporting.CheckBox();
            this.chkParamedicConsultType02 = new Telerik.Reporting.CheckBox();
            this.chkParamedicConsultType03 = new Telerik.Reporting.CheckBox();
            this.txtFromParamedicName = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.txtFromSpecialty = new Telerik.Reporting.TextBox();
            this.txtReferTime = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.txtReferDate = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Mm(267D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox4,
            this.textBox7,
            this.txtMedicalNo,
            this.txtPatientName,
            this.txtBirthDateAge,
            this.textBox10,
            this.textBox11,
            this.txtToSpecialty,
            this.textBox23,
            this.textBox21,
            this.txtDiagnosa,
            this.txtToParamedicName,
            this.textBox66,
            this.textBox65,
            this.textBox57,
            this.textBox12,
            this.txtNotes,
            this.chkParamedicConsultType01,
            this.chkParamedicConsultType02,
            this.chkParamedicConsultType03,
            this.txtFromParamedicName,
            this.textBox14,
            this.textBox15,
            this.txtFromSpecialty,
            this.txtReferTime,
            this.textBox3,
            this.textBox6,
            this.txtReferDate,
            this.textBox1});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.Font.Name = "Microsoft Sans Serif";
            this.pageHeader.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.5D);
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.9D), Telerik.Reporting.Drawing.Unit.Inch(1.058D));
            this.textBox4.Name = "TxtRSU";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.5D), Telerik.Reporting.Drawing.Unit.Inch(0.242D));
            this.textBox4.Style.Font.Bold = false;
            this.textBox4.Style.Font.Name = "Tahoma";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox4.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox4.Value = "SURAT PERMINTAAN KONSULTASI";
            // 
            // textBox7
            // 
            this.textBox7.Format = "{0}";
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.3D), Telerik.Reporting.Drawing.Unit.Inch(0.7D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox7.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox7.Style.Font.Name = "Tahoma";
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox7.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1D);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.Value = "RM/RD";
            // 
            // txtMedicalNo
            // 
            this.txtMedicalNo.Format = " : {0}";
            this.txtMedicalNo.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.3D), Telerik.Reporting.Drawing.Unit.Inch(0.7D));
            this.txtMedicalNo.Name = "txtMedicalNo";
            this.txtMedicalNo.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.095D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtMedicalNo.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtMedicalNo.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.txtMedicalNo.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtMedicalNo.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.txtMedicalNo.Style.Font.Name = "Tahoma";
            this.txtMedicalNo.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.txtMedicalNo.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtMedicalNo.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.txtMedicalNo.Value = "";
            // 
            // txtPatientName
            // 
            this.txtPatientName.Format = " : {0}";
            this.txtPatientName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.3D), Telerik.Reporting.Drawing.Unit.Inch(0.3D));
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.095D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtPatientName.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.txtPatientName.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtPatientName.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtPatientName.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.txtPatientName.Style.Font.Name = "Tahoma";
            this.txtPatientName.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.txtPatientName.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtPatientName.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.txtPatientName.Value = "";
            // 
            // txtBirthDateAge
            // 
            this.txtBirthDateAge.Format = ": {0}";
            this.txtBirthDateAge.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.3D), Telerik.Reporting.Drawing.Unit.Inch(0.5D));
            this.txtBirthDateAge.Name = "txtBirthDateAge";
            this.txtBirthDateAge.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.095D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtBirthDateAge.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.txtBirthDateAge.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtBirthDateAge.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.txtBirthDateAge.Style.Font.Name = "Tahoma";
            this.txtBirthDateAge.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.txtBirthDateAge.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtBirthDateAge.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.txtBirthDateAge.Value = "";
            // 
            // textBox10
            // 
            this.textBox10.Format = "{0}";
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.3D), Telerik.Reporting.Drawing.Unit.Inch(0.5D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox10.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox10.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox10.Style.Font.Name = "Tahoma";
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox10.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1D);
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox10.Value = "Tgl Lahir/Umur ";
            // 
            // textBox11
            // 
            this.textBox11.Format = "{0}";
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.3D), Telerik.Reporting.Drawing.Unit.Inch(0.3D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox11.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox11.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox11.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox11.Style.Font.Name = "Tahoma";
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox11.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1D);
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox11.Value = "Nama Pasien";
            // 
            // txtToSpecialty
            // 
            this.txtToSpecialty.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.795D), Telerik.Reporting.Drawing.Unit.Inch(1.8D));
            this.txtToSpecialty.Name = "txtToSpecialty";
            this.txtToSpecialty.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.6D), Telerik.Reporting.Drawing.Unit.Inch(0.198D));
            this.txtToSpecialty.Style.Font.Bold = false;
            this.txtToSpecialty.Style.Font.Name = "Tahoma";
            this.txtToSpecialty.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.txtToSpecialty.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.txtToSpecialty.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtToSpecialty.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.txtToSpecialty.Value = "";
            // 
            // textBox23
            // 
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.105D), Telerik.Reporting.Drawing.Unit.Inch(3.5D));
            this.textBox23.Name = "textBox9";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.7D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox23.Style.Font.Bold = false;
            this.textBox23.Style.Font.Name = "Tahoma";
            this.textBox23.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox23.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox23.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox23.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox23.Value = "Keterangan klinik terpenting adalah:";
            // 
            // textBox21
            // 
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.105D), Telerik.Reporting.Drawing.Unit.Inch(9.3D));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.292D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox21.Style.Font.Bold = false;
            this.textBox21.Style.Font.Name = "Tahoma";
            this.textBox21.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox21.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox21.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox21.Value = "Diagnosa Kerja";
            // 
            // txtDiagnosa
            // 
            this.txtDiagnosa.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.405D), Telerik.Reporting.Drawing.Unit.Inch(9.3D));
            this.txtDiagnosa.Name = "txtDiagnosa";
            this.txtDiagnosa.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.3D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtDiagnosa.Style.Font.Bold = false;
            this.txtDiagnosa.Style.Font.Name = "Tahoma";
            this.txtDiagnosa.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.txtDiagnosa.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.txtDiagnosa.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtDiagnosa.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.txtDiagnosa.Value = "";
            // 
            // txtToParamedicName
            // 
            this.txtToParamedicName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.086D), Telerik.Reporting.Drawing.Unit.Inch(1.8D));
            this.txtToParamedicName.Name = "txtToParamedicName";
            this.txtToParamedicName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.914D), Telerik.Reporting.Drawing.Unit.Inch(0.198D));
            this.txtToParamedicName.Style.Font.Bold = false;
            this.txtToParamedicName.Style.Font.Name = "Tahoma";
            this.txtToParamedicName.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.txtToParamedicName.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.txtToParamedicName.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtToParamedicName.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.txtToParamedicName.Value = "";
            // 
            // textBox66
            // 
            this.textBox66.CanGrow = false;
            this.textBox66.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.205D), Telerik.Reporting.Drawing.Unit.Inch(1.8D));
            this.textBox66.Name = "textBox66";
            this.textBox66.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox66.Style.Font.Bold = false;
            this.textBox66.Style.Font.Name = "Tahoma";
            this.textBox66.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox66.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox66.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox66.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox66.Value = "Kepada Yth:";
            // 
            // textBox65
            // 
            this.textBox65.CanGrow = false;
            this.textBox65.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.205D), Telerik.Reporting.Drawing.Unit.Inch(2.4D));
            this.textBox65.Name = "textBox65";
            this.textBox65.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox65.Style.Font.Bold = false;
            this.textBox65.Style.Font.Name = "Tahoma";
            this.textBox65.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox65.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox65.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox65.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox65.Value = "Mohon bantuan sejawat atas pasien ini untuk";
            // 
            // textBox57
            // 
            this.textBox57.CanGrow = false;
            this.textBox57.Format = "";
            this.textBox57.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.205D), Telerik.Reporting.Drawing.Unit.Inch(2.1D));
            this.textBox57.Name = "textBox9";
            this.textBox57.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.192D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox57.Style.Font.Bold = false;
            this.textBox57.Style.Font.Name = "Tahoma";
            this.textBox57.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox57.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox57.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox57.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox57.Value = "Dengan hormat,";
            // 
            // textBox12
            // 
            this.textBox12.CanGrow = false;
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.195D), Telerik.Reporting.Drawing.Unit.Inch(1.798D));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.6D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox12.Style.Font.Bold = false;
            this.textBox12.Style.Font.Name = "Tahoma";
            this.textBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox12.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox12.Value = "Spesialis:";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.105D), Telerik.Reporting.Drawing.Unit.Inch(3.7D));
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.29D), Telerik.Reporting.Drawing.Unit.Inch(5.3D));
            this.txtNotes.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.txtNotes.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.txtNotes.Style.Font.Bold = false;
            this.txtNotes.Style.Font.Name = "Tahoma";
            this.txtNotes.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.txtNotes.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.txtNotes.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtNotes.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.txtNotes.Value = "";
            // 
            // chkParamedicConsultType01
            // 
            this.chkParamedicConsultType01.CheckedImage = ((object)(resources.GetObject("chkParamedicConsultType01.CheckedImage")));
            this.chkParamedicConsultType01.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.405D), Telerik.Reporting.Drawing.Unit.Inch(2.6D));
            this.chkParamedicConsultType01.Name = "chkParamedicConsultType01";
            this.chkParamedicConsultType01.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.192D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.chkParamedicConsultType01.Style.Font.Name = "Tahoma";
            this.chkParamedicConsultType01.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.chkParamedicConsultType01.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.chkParamedicConsultType01.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.chkParamedicConsultType01.Text = "Konsultasi / tindakan masalah medik saat ini";
            this.chkParamedicConsultType01.UncheckedImage = ((object)(resources.GetObject("chkParamedicConsultType01.UncheckedImage")));
            this.chkParamedicConsultType01.Value = "False";
            // 
            // chkParamedicConsultType02
            // 
            this.chkParamedicConsultType02.CheckedImage = ((object)(resources.GetObject("chkParamedicConsultType02.CheckedImage")));
            this.chkParamedicConsultType02.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.405D), Telerik.Reporting.Drawing.Unit.Inch(2.8D));
            this.chkParamedicConsultType02.Name = "chkParamedicConsultType02";
            this.chkParamedicConsultType02.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.192D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.chkParamedicConsultType02.Style.Font.Name = "Tahoma";
            this.chkParamedicConsultType02.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.chkParamedicConsultType02.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.chkParamedicConsultType02.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.chkParamedicConsultType02.Text = "Pengambil alihan kasus ini untuk selanjutnya";
            this.chkParamedicConsultType02.UncheckedImage = ((object)(resources.GetObject("chkParamedicConsultType02.UncheckedImage")));
            this.chkParamedicConsultType02.Value = false;
            // 
            // chkParamedicConsultType03
            // 
            this.chkParamedicConsultType03.CheckedImage = ((object)(resources.GetObject("chkParamedicConsultType03.CheckedImage")));
            this.chkParamedicConsultType03.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.405D), Telerik.Reporting.Drawing.Unit.Inch(3D));
            this.chkParamedicConsultType03.Name = "chkParamedicConsultType03";
            this.chkParamedicConsultType03.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.192D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.chkParamedicConsultType03.Style.Font.Name = "Tahoma";
            this.chkParamedicConsultType03.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.chkParamedicConsultType03.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.chkParamedicConsultType03.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.chkParamedicConsultType03.Text = "Rawat bersama untuk selanjutnya";
            this.chkParamedicConsultType03.UncheckedImage = ((object)(resources.GetObject("chkParamedicConsultType03.UncheckedImage")));
            this.chkParamedicConsultType03.Value = false;
            // 
            // txtFromParamedicName
            // 
            this.txtFromParamedicName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.405D), Telerik.Reporting.Drawing.Unit.Inch(9.5D));
            this.txtFromParamedicName.Name = "txtFromParamedicName";
            this.txtFromParamedicName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.9D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtFromParamedicName.Style.Font.Bold = false;
            this.txtFromParamedicName.Style.Font.Name = "Tahoma";
            this.txtFromParamedicName.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.txtFromParamedicName.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.txtFromParamedicName.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtFromParamedicName.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.txtFromParamedicName.Value = "";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.105D), Telerik.Reporting.Drawing.Unit.Inch(9.5D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.292D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox14.Style.Font.Bold = false;
            this.textBox14.Style.Font.Name = "Tahoma";
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox14.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox14.Value = "Nama Dokter";
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.105D), Telerik.Reporting.Drawing.Unit.Inch(9.7D));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.292D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox15.Style.Font.Bold = false;
            this.textBox15.Style.Font.Name = "Tahoma";
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox15.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox15.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox15.Value = "Spesialis";
            // 
            // txtFromSpecialty
            // 
            this.txtFromSpecialty.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.405D), Telerik.Reporting.Drawing.Unit.Inch(9.7D));
            this.txtFromSpecialty.Name = "txtFromSpecialty";
            this.txtFromSpecialty.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.9D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtFromSpecialty.Style.Font.Bold = false;
            this.txtFromSpecialty.Style.Font.Name = "Tahoma";
            this.txtFromSpecialty.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.txtFromSpecialty.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.txtFromSpecialty.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtFromSpecialty.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.txtFromSpecialty.Value = "";
            // 
            // txtReferTime
            // 
            this.txtReferTime.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.005D), Telerik.Reporting.Drawing.Unit.Inch(9.7D));
            this.txtReferTime.Name = "txtReferTime";
            this.txtReferTime.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtReferTime.Style.Font.Bold = false;
            this.txtReferTime.Style.Font.Name = "Tahoma";
            this.txtReferTime.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.txtReferTime.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.txtReferTime.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtReferTime.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.txtReferTime.Value = "";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.405D), Telerik.Reporting.Drawing.Unit.Inch(9.7D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.592D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox3.Style.Font.Bold = false;
            this.textBox3.Style.Font.Name = "Tahoma";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox3.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox3.Value = "Pukul";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.405D), Telerik.Reporting.Drawing.Unit.Inch(9.5D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.592D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox6.Style.Font.Bold = false;
            this.textBox6.Style.Font.Name = "Tahoma";
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox6.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox6.Value = "Tanggal";
            // 
            // txtReferDate
            // 
            this.txtReferDate.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.005D), Telerik.Reporting.Drawing.Unit.Inch(9.5D));
            this.txtReferDate.Name = "txtReferDate";
            this.txtReferDate.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtReferDate.Style.Font.Bold = false;
            this.txtReferDate.Style.Font.Name = "Tahoma";
            this.txtReferDate.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.txtReferDate.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.txtReferDate.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtReferDate.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.txtReferDate.Value = "";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.105D), Telerik.Reporting.Drawing.Unit.Inch(10.1D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.695D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox1.Style.Font.Bold = false;
            this.textBox1.Style.Font.Name = "Tahoma";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox1.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox1.Value = "Rev.3/01/10/2018";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.052D);
            this.detail.Name = "detail";
            this.detail.Style.Font.Name = "Microsoft Sans Serif";
            this.detail.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.5D);
            // 
            // RM12_01_ReferNotes
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail});
            this.Name = "ReferNotes";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Cm(1D), Telerik.Reporting.Drawing.Unit.Cm(1D), Telerik.Reporting.Drawing.Unit.Cm(1D), Telerik.Reporting.Drawing.Unit.Cm(1D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.UnitOfMeasure = Telerik.Reporting.Drawing.UnitType.Mm;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.4D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox txtNotes;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox txtDiagnosa;
        private Telerik.Reporting.TextBox textBox66;
        private Telerik.Reporting.TextBox textBox65;
        private Telerik.Reporting.TextBox textBox57;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox txtToParamedicName;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox txtMedicalNo;
        private Telerik.Reporting.TextBox txtPatientName;
        private Telerik.Reporting.TextBox txtBirthDateAge;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox txtToSpecialty;
        private Telerik.Reporting.CheckBox chkParamedicConsultType01;
        private Telerik.Reporting.CheckBox chkParamedicConsultType02;
        private Telerik.Reporting.CheckBox chkParamedicConsultType03;
        private Telerik.Reporting.TextBox txtFromParamedicName;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox txtFromSpecialty;
        private Telerik.Reporting.TextBox txtReferTime;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox txtReferDate;
        private Telerik.Reporting.TextBox textBox1;
    }
}