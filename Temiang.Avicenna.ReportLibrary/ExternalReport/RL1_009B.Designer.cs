namespace Temiang.Avicenna.ReportLibrary.ExternalReport
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class RL1_009B
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
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.txtkdrs = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.txtNamaRS = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.txtPeriode = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.txtRlMasterReportItemName = new Telerik.Reporting.TextBox();
            this.txtPasienAwal = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = new Telerik.Reporting.Drawing.Unit(1.8000000715255737, Telerik.Reporting.Drawing.UnitType.Inch);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtkdrs,
            this.textBox6,
            this.textBox1,
            this.textBox2,
            this.txtNamaRS,
            this.textBox3,
            this.textBox5,
            this.txtPeriode,
            this.textBox4,
            this.textBox17});
            this.pageHeader.Name = "pageHeader";
            // 
            // detail
            // 
            this.detail.Height = new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtRlMasterReportItemName,
            this.txtPasienAwal});
            this.detail.Name = "detail";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = new Telerik.Reporting.Drawing.Unit(0.0520833320915699, Telerik.Reporting.Drawing.UnitType.Inch);
            this.pageFooter.Name = "pageFooter";
            // 
            // txtkdrs
            // 
            this.txtkdrs.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.9041666984558105, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.0270832777023315, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtkdrs.Name = "txtkdrs";
            this.txtkdrs.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.7978769540786743, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.22514626383781433, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtkdrs.Style.Font.Bold = true;
            this.txtkdrs.Style.Font.Name = "Times New Roman";
            this.txtkdrs.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.txtkdrs.Style.Font.Strikeout = false;
            this.txtkdrs.Style.Font.Underline = false;
            this.txtkdrs.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtkdrs.Value = "=HealthcareSosialNumber";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.6958332061767578, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.0270832777023315, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox6.Name = "txtPeriode";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.1999607086181641, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.22514626383781433, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox6.Style.Font.Bold = true;
            this.textBox6.Style.Font.Name = "Times New Roman";
            this.textBox6.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox6.Style.Font.Strikeout = false;
            this.textBox6.Style.Font.Underline = false;
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox6.Value = "No.Kode RS:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.30000004172325134, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.78749996423721313, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox1.Name = "txtPeriode";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.39996075630188, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.22514626383781433, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Times New Roman";
            this.textBox1.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox1.Style.Font.Strikeout = false;
            this.textBox1.Style.Font.Underline = false;
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox1.Value = "Formulir RL1";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.30000004172325134, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.0270832777023315, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox2.Name = "txtPeriode";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.1999607086181641, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.22514626383781433, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Name = "Times New Roman";
            this.textBox2.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox2.Style.Font.Strikeout = false;
            this.textBox2.Style.Font.Underline = false;
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.Value = "Nama Rumah Sakit : ";
            // 
            // txtNamaRS
            // 
            this.txtNamaRS.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.4999607801437378, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.0270832777023315, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtNamaRS.Name = "txtNamaRS";
            this.txtNamaRS.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.39996075630188, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.22514626383781433, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtNamaRS.Style.Font.Bold = true;
            this.txtNamaRS.Style.Font.Name = "Times New Roman";
            this.txtNamaRS.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.txtNamaRS.Style.Font.Strikeout = false;
            this.txtNamaRS.Style.Font.Underline = false;
            this.txtNamaRS.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtNamaRS.Value = "=HealthcareName";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.30000004172325134, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.2562500238418579, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.400001049041748, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.15000000596046448, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Name = "Times New Roman";
            this.textBox3.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox3.Style.Font.Strikeout = false;
            this.textBox3.Style.Font.Underline = false;
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox3.Value = "9. KEGIATAN RADIOLOGI";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.30000004172325134, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.099999986588954926, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.400001049041748, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.27477502822875977, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Font.Name = "Times New Roman";
            this.textBox5.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(12, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox5.Style.Font.Strikeout = false;
            this.textBox5.Style.Font.Underline = false;
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.Value = "DATA KEGIATAN RUMAH SAKIT";
            // 
            // txtPeriode
            // 
            this.txtPeriode.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.30000004172325134, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.37477502226829529, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtPeriode.Name = "txtPeriode";
            this.txtPeriode.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.400001049041748, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.22514626383781433, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtPeriode.Style.Font.Bold = true;
            this.txtPeriode.Style.Font.Name = "Times New Roman";
            this.txtPeriode.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.txtPeriode.Style.Font.Strikeout = false;
            this.txtPeriode.Style.Font.Underline = false;
            this.txtPeriode.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtPeriode.Value = "=Periode";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.1707546710968018, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.6000000238418579, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox4.Name = "txtPeriode";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.4996057748794556, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox4.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox4.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox4.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox4.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Name = "Times New Roman";
            this.textBox4.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox4.Style.Font.Strikeout = false;
            this.textBox4.Style.Font.Underline = false;
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Value = "Jumlah";
            // 
            // textBox17
            // 
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.30000004172325134, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.6000000238418579, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox17.Name = "txtPeriode";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.8707544803619385, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox17.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox17.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox17.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox17.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox17.Style.Font.Bold = true;
            this.textBox17.Style.Font.Name = "Times New Roman";
            this.textBox17.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox17.Style.Font.Strikeout = false;
            this.textBox17.Style.Font.Underline = false;
            this.textBox17.Value = "B. RADIOTHERAPI";
            // 
            // txtRlMasterReportItemName
            // 
            this.txtRlMasterReportItemName.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.30000019073486328, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtRlMasterReportItemName.Name = "txtRlMasterReportItemName";
            this.txtRlMasterReportItemName.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.8707544803619385, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19992099702358246, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtRlMasterReportItemName.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtRlMasterReportItemName.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtRlMasterReportItemName.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.txtRlMasterReportItemName.Style.Font.Bold = false;
            this.txtRlMasterReportItemName.Style.Font.Name = "Times New Roman";
            this.txtRlMasterReportItemName.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.txtRlMasterReportItemName.Style.Font.Strikeout = false;
            this.txtRlMasterReportItemName.Style.Font.Underline = false;
            this.txtRlMasterReportItemName.Style.Padding.Left = new Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Mm);
            this.txtRlMasterReportItemName.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtRlMasterReportItemName.Value = "=RlMasterReportItemName";
            // 
            // txtPasienAwal
            // 
            this.txtPasienAwal.CanGrow = false;
            this.txtPasienAwal.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.1707546710968018, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtPasienAwal.Name = "txtPasienAwal";
            this.txtPasienAwal.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.499605655670166, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19992099702358246, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtPasienAwal.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtPasienAwal.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtPasienAwal.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtPasienAwal.Style.Font.Bold = false;
            this.txtPasienAwal.Style.Font.Name = "Times New Roman";
            this.txtPasienAwal.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.txtPasienAwal.Style.Font.Strikeout = false;
            this.txtPasienAwal.Style.Font.Underline = false;
            this.txtPasienAwal.Style.Padding.Right = new Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Mm);
            this.txtPasienAwal.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtPasienAwal.Value = "=Total";
            // 
            // RL1_009B
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail,
            this.pageFooter});
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Left = new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Right = new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Top = new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = new Telerik.Reporting.Drawing.Unit(7.8740158081054688, Telerik.Reporting.Drawing.UnitType.Inch);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox txtkdrs;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox txtNamaRS;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox txtPeriode;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox txtRlMasterReportItemName;
        private Telerik.Reporting.TextBox txtPasienAwal;
    }
}