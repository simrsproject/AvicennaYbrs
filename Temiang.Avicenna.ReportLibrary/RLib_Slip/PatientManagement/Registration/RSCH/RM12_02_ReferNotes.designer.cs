namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSCH
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class RM12_02_ReferNotes
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.textBox33 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.txtMedicalNo = new Telerik.Reporting.TextBox();
            this.txtPatientName = new Telerik.Reporting.TextBox();
            this.txtBirthDateAge = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox57 = new Telerik.Reporting.TextBox();
            this.txtNotes = new Telerik.Reporting.TextBox();
            this.txtToParamedicName = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.txtToSpecialty = new Telerik.Reporting.TextBox();
            this.txtAnswerTime = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.txtAnswerDate = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Mm(267D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox33,
            this.textBox4,
            this.textBox7,
            this.txtMedicalNo,
            this.txtPatientName,
            this.txtBirthDateAge,
            this.textBox10,
            this.textBox11,
            this.textBox23,
            this.textBox57,
            this.txtNotes,
            this.txtToParamedicName,
            this.textBox15,
            this.txtToSpecialty,
            this.txtAnswerTime,
            this.textBox3,
            this.textBox6,
            this.txtAnswerDate,
            this.textBox2});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.Font.Name = "Microsoft Sans Serif";
            this.pageHeader.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.5D);
            // 
            // textBox33
            // 
            this.textBox33.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.9D), Telerik.Reporting.Drawing.Unit.Inch(0.026D));
            this.textBox33.Name = "textBox9";
            this.textBox33.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.5D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox33.Style.Font.Bold = false;
            this.textBox33.Style.Font.Name = "Tahoma";
            this.textBox33.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox33.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox33.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox33.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox33.Value = "RM 12";
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
            this.textBox4.Value = "JAWABAN KONSUL";
            // 
            // textBox7
            // 
            this.textBox7.Format = "{0}";
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.2D), Telerik.Reporting.Drawing.Unit.Inch(0.7D));
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
            this.txtMedicalNo.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.2D), Telerik.Reporting.Drawing.Unit.Inch(0.7D));
            this.txtMedicalNo.Name = "txtMedicalNo";
            this.txtMedicalNo.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.195D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
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
            this.txtPatientName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.2D), Telerik.Reporting.Drawing.Unit.Inch(0.3D));
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.195D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
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
            this.txtBirthDateAge.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.2D), Telerik.Reporting.Drawing.Unit.Inch(0.5D));
            this.txtBirthDateAge.Name = "txtBirthDateAge";
            this.txtBirthDateAge.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.195D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
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
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.2D), Telerik.Reporting.Drawing.Unit.Inch(0.5D));
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
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.2D), Telerik.Reporting.Drawing.Unit.Inch(0.3D));
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
            // textBox23
            // 
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.11D), Telerik.Reporting.Drawing.Unit.Inch(2.4D));
            this.textBox23.Name = "textBox9";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.7D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox23.Style.Font.Bold = false;
            this.textBox23.Style.Font.Name = "Tahoma";
            this.textBox23.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox23.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox23.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox23.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox23.Value = "Sesuai permohonan konsultasi, pada kasus ini dijumpai :";
            // 
            // textBox57
            // 
            this.textBox57.CanGrow = false;
            this.textBox57.Format = "";
            this.textBox57.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.11D), Telerik.Reporting.Drawing.Unit.Inch(2.1D));
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
            // txtNotes
            // 
            this.txtNotes.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.11D), Telerik.Reporting.Drawing.Unit.Inch(2.7D));
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.29D), Telerik.Reporting.Drawing.Unit.Inch(6.6D));
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
            // txtToParamedicName
            // 
            this.txtToParamedicName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.4D), Telerik.Reporting.Drawing.Unit.Inch(9.4D));
            this.txtToParamedicName.Name = "txtToParamedicName";
            this.txtToParamedicName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.9D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtToParamedicName.Style.Font.Bold = false;
            this.txtToParamedicName.Style.Font.Name = "Tahoma";
            this.txtToParamedicName.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.txtToParamedicName.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.txtToParamedicName.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtToParamedicName.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.txtToParamedicName.Value = "";
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(9.6D));
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
            // txtToSpecialty
            // 
            this.txtToSpecialty.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.4D), Telerik.Reporting.Drawing.Unit.Inch(9.6D));
            this.txtToSpecialty.Name = "txtToSpecialty";
            this.txtToSpecialty.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.9D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtToSpecialty.Style.Font.Bold = false;
            this.txtToSpecialty.Style.Font.Name = "Tahoma";
            this.txtToSpecialty.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.txtToSpecialty.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.txtToSpecialty.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtToSpecialty.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.txtToSpecialty.Value = "";
            // 
            // txtAnswerTime
            // 
            this.txtAnswerTime.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5D), Telerik.Reporting.Drawing.Unit.Inch(9.6D));
            this.txtAnswerTime.Name = "txtAnswerTime";
            this.txtAnswerTime.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtAnswerTime.Style.Font.Bold = false;
            this.txtAnswerTime.Style.Font.Name = "Tahoma";
            this.txtAnswerTime.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.txtAnswerTime.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.txtAnswerTime.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtAnswerTime.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.txtAnswerTime.Value = "";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.4D), Telerik.Reporting.Drawing.Unit.Inch(9.6D));
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
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.4D), Telerik.Reporting.Drawing.Unit.Inch(9.4D));
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
            // txtAnswerDate
            // 
            this.txtAnswerDate.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5D), Telerik.Reporting.Drawing.Unit.Inch(9.4D));
            this.txtAnswerDate.Name = "txtAnswerDate";
            this.txtAnswerDate.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtAnswerDate.Style.Font.Bold = false;
            this.txtAnswerDate.Style.Font.Name = "Tahoma";
            this.txtAnswerDate.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.txtAnswerDate.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.txtAnswerDate.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtAnswerDate.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.txtAnswerDate.Value = "";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(9.4D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.292D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox2.Style.Font.Bold = false;
            this.textBox2.Style.Font.Name = "Tahoma";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox2.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox2.Value = "Nama Dokter";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.052D);
            this.detail.Name = "detail";
            this.detail.Style.Font.Name = "Microsoft Sans Serif";
            this.detail.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.5D);
            // 
            // RM12_02_ReferNotes
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
        private Telerik.Reporting.TextBox textBox33;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox57;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox txtMedicalNo;
        private Telerik.Reporting.TextBox txtPatientName;
        private Telerik.Reporting.TextBox txtBirthDateAge;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox txtToParamedicName;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox txtToSpecialty;
        private Telerik.Reporting.TextBox txtAnswerTime;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox txtAnswerDate;
        private Telerik.Reporting.TextBox textBox2;
    }
}