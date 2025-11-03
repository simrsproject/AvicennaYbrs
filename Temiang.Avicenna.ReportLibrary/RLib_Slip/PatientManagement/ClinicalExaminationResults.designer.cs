namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class ClinicalExaminationResults
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.txtItemID = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox33 = new Telerik.Reporting.TextBox();
            this.htmlTextBox1 = new Telerik.Reporting.HtmlTextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.txtDokter = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(1.9187885522842407);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox12,
            this.textBox8,
            this.textBox9,
            this.txtItemID});
            this.pageHeader.Name = "pageHeader";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.19992208480835), Telerik.Reporting.Drawing.Unit.Inch(0.299999862909317));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Tahoma";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14);
            this.textBox1.Style.Font.Underline = true;
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "=\'HASIL PEMERIKSAAN \' + Title";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.2000000476837158), Telerik.Reporting.Drawing.Unit.Inch(0.099999986588954926));
            this.textBox12.Name = "textBox3";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.1000018119812012), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox12.Style.Font.Bold = true;
            this.textBox12.Style.Font.Name = "Times New Roman";
            this.textBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14);
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.Value = "=HealthcareName";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.2000000476837158), Telerik.Reporting.Drawing.Unit.Inch(0.3000788688659668));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.1000018119812012), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox8.Style.Font.Name = "Times New Roman";
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Value = "=AddressLine1 +\' \' +City + \' \' +ZipCode";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.2000000476837158), Telerik.Reporting.Drawing.Unit.Inch(0.50015777349472046));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.1000018119812012), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox9.Style.Font.Name = "Times New Roman";
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox9.Value = "=\'Telp. \' + PhoneNo +\'  Fax. \' +FaxNo";
            // 
            // txtItemID
            // 
            this.txtItemID.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609), Telerik.Reporting.Drawing.Unit.Inch(1.5));
            this.txtItemID.Name = "txtItemID";
            this.txtItemID.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1000001430511475), Telerik.Reporting.Drawing.Unit.Inch(0.29992148280143738));
            this.txtItemID.Style.Font.Bold = true;
            this.txtItemID.Style.Font.Name = "Microsoft Sans Serif";
            this.txtItemID.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.txtItemID.Style.Font.Underline = true;
            this.txtItemID.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtItemID.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.txtItemID.Value = "Identitas";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609), Telerik.Reporting.Drawing.Unit.Inch(0.099999748170375824));
            this.textBox2.Name = "textBox3";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3916667699813843), Telerik.Reporting.Drawing.Unit.Inch(0.29999977350234985));
            this.textBox2.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox2.Value = "Nama Pasien";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609), Telerik.Reporting.Drawing.Unit.Inch(0.700078547000885));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3916667699813843), Telerik.Reporting.Drawing.Unit.Inch(0.30000108480453491));
            this.textBox3.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox3.Value = "No. Rekam Medis";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4), Telerik.Reporting.Drawing.Unit.Inch(0.42299556732177734));
            this.textBox4.Name = "textBox3";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3916667699813843), Telerik.Reporting.Drawing.Unit.Inch(0.18742132186889648));
            this.textBox4.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.Value = "Dokter Pemeriksa";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609), Telerik.Reporting.Drawing.Unit.Inch(0.40007823705673218));
            this.textBox5.Name = "textBox3";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3916667699813843), Telerik.Reporting.Drawing.Unit.Inch(0.29992139339447021));
            this.textBox5.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox5.Value = "Umur";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8000003099441528), Telerik.Reporting.Drawing.Unit.Inch(0.099999748170375824));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.5000004768371582), Telerik.Reporting.Drawing.Unit.Inch(0.29999959468841553));
            this.textBox13.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox13.Value = "=PatientName";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8000003099441528), Telerik.Reporting.Drawing.Unit.Inch(0.700078547000885));
            this.textBox14.Name = "textBox3";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.5000014305114746), Telerik.Reporting.Drawing.Unit.Inch(0.30000108480453491));
            this.textBox14.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox14.Value = "=MedicalNo";
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8000003099441528), Telerik.Reporting.Drawing.Unit.Inch(0.40007823705673218));
            this.textBox15.Name = "textBox3";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.5000004768371582), Telerik.Reporting.Drawing.Unit.Inch(0.29992139339447021));
            this.textBox15.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox15.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox15.Value = "=Umur";
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.3999996185302734), Telerik.Reporting.Drawing.Unit.Inch(0.099999427795410156));
            this.textBox16.Name = "textBox3";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.60000103712081909), Telerik.Reporting.Drawing.Unit.Inch(0.18742132186889648));
            this.textBox16.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox16.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox16.Value = "=Sex";
            // 
            // textBox29
            // 
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5918252468109131), Telerik.Reporting.Drawing.Unit.Inch(0.400078147649765));
            this.textBox29.Name = "textBox9";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099920906126499176), Telerik.Reporting.Drawing.Unit.Inch(0.29992124438285828));
            this.textBox29.Style.Font.Bold = true;
            this.textBox29.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox29.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox29.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox29.Value = ":";
            // 
            // textBox31
            // 
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5918252468109131), Telerik.Reporting.Drawing.Unit.Inch(0.70007866621017456));
            this.textBox31.Name = "textBox9";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099920906126499176), Telerik.Reporting.Drawing.Unit.Inch(0.30000114440917969));
            this.textBox31.Style.Font.Bold = true;
            this.textBox31.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox31.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox31.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox31.Value = ":";
            // 
            // textBox32
            // 
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5918252468109131), Telerik.Reporting.Drawing.Unit.Inch(0.099999748170375824));
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099920906126499176), Telerik.Reporting.Drawing.Unit.Inch(0.2999996542930603));
            this.textBox32.Style.Font.Bold = true;
            this.textBox32.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox32.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox32.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox32.Value = ":";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(2.7999999523162842);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox33,
            this.htmlTextBox1,
            this.textBox16,
            this.textBox13,
            this.textBox14,
            this.textBox15,
            this.textBox2,
            this.textBox29,
            this.textBox31,
            this.textBox3,
            this.textBox32,
            this.textBox5,
            this.textBox10});
            this.detail.Name = "detail";
            // 
            // textBox33
            // 
            this.textBox33.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609), Telerik.Reporting.Drawing.Unit.Inch(1.2125799655914307));
            this.textBox33.Name = "textBox1";
            this.textBox33.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5), Telerik.Reporting.Drawing.Unit.Inch(0.29976251721382141));
            this.textBox33.Style.Font.Bold = true;
            this.textBox33.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox33.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox33.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox33.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox33.Style.Visible = true;
            this.textBox33.Value = "Hasil Pemeriksaan";
            // 
            // htmlTextBox1
            // 
            this.htmlTextBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8000003099441528), Telerik.Reporting.Drawing.Unit.Inch(1.2125800848007202));
            this.htmlTextBox1.Name = "htmlTextBox1";
            this.htmlTextBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.0000786781311035), Telerik.Reporting.Drawing.Unit.Inch(1.468631386756897));
            this.htmlTextBox1.Style.Font.Name = "Microsoft Sans Serif";
            this.htmlTextBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.htmlTextBox1.Value = "=Result";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.6000789403915405), Telerik.Reporting.Drawing.Unit.Inch(1.2123415470123291));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099920906126499176), Telerik.Reporting.Drawing.Unit.Inch(0.30000114440917969));
            this.textBox10.Style.Font.Bold = true;
            this.textBox10.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox10.Value = ":";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(1.899999737739563);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtDokter,
            this.textBox7,
            this.textBox4,
            this.textBox6});
            this.pageFooter.Name = "pageFooter";
            // 
            // txtDokter
            // 
            this.txtDokter.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4), Telerik.Reporting.Drawing.Unit.Inch(1.5));
            this.txtDokter.Name = "txtDokter";
            this.txtDokter.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.0000007152557373), Telerik.Reporting.Drawing.Unit.Inch(0.19999992847442627));
            this.txtDokter.Style.Font.Bold = false;
            this.txtDokter.Style.Font.Name = "Microsoft Sans Serif";
            this.txtDokter.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.txtDokter.Style.Font.Underline = true;
            this.txtDokter.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtDokter.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.txtDokter.Value = "=\'( \' +ParamedicName +\' )\'";
            // 
            // textBox7
            // 
            this.textBox7.Format = "{0:dd-MMM-yyyy}";
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.0000004768371582), Telerik.Reporting.Drawing.Unit.Inch(0.22291724383831024));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.0000002384185791), Telerik.Reporting.Drawing.Unit.Inch(0.19999992847442627));
            this.textBox7.Style.Font.Bold = false;
            this.textBox7.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.Value = "=NOW()";
            // 
            // textBox6
            // 
            this.textBox6.Format = "{0}, ";
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9999995231628418), Telerik.Reporting.Drawing.Unit.Inch(0.22291660308837891));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.999921977519989), Telerik.Reporting.Drawing.Unit.Inch(0.19999992847442627));
            this.textBox6.Style.Font.Bold = false;
            this.textBox6.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "=City";
            // 
            // ClinicalExaminationResults
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail,
            this.pageFooter});
            this.Name = "TestResultNative";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Cm(2);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Inch(0.5);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Cm(0);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(1.5);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.2000007629394531);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.TextBox textBox33;
        private Telerik.Reporting.TextBox txtItemID;
        private Telerik.Reporting.TextBox txtDokter;
        private HtmlTextBox htmlTextBox1;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox10;
    }
}