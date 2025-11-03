namespace Temiang.Avicenna.ReportLibrary.Charges
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class BillingSummaryShortRpt
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group2 = new Telerik.Reporting.Group();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.shape3 = new Telerik.Reporting.Shape();
            this.textBox35 = new Telerik.Reporting.TextBox();
            this.textBox34 = new Telerik.Reporting.TextBox();
            this.textBox94 = new Telerik.Reporting.TextBox();
            this.textBox95 = new Telerik.Reporting.TextBox();
            this.textBox96 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox37 = new Telerik.Reporting.TextBox();
            this.textBox39 = new Telerik.Reporting.TextBox();
            this.textBox41 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.shape6 = new Telerik.Reporting.Shape();
            this.textBox45 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.shape7 = new Telerik.Reporting.Shape();
            this.textBox46 = new Telerik.Reporting.TextBox();
            this.textBox47 = new Telerik.Reporting.TextBox();
            this.textBox48 = new Telerik.Reporting.TextBox();
            this.textBox49 = new Telerik.Reporting.TextBox();
            this.textBox50 = new Telerik.Reporting.TextBox();
            this.textBox79 = new Telerik.Reporting.TextBox();
            this.textBox82 = new Telerik.Reporting.TextBox();
            this.shape10 = new Telerik.Reporting.Shape();
            this.textBox83 = new Telerik.Reporting.TextBox();
            this.textBox84 = new Telerik.Reporting.TextBox();
            this.textBox85 = new Telerik.Reporting.TextBox();
            this.textBox88 = new Telerik.Reporting.TextBox();
            this.textBox91 = new Telerik.Reporting.TextBox();
            this.textBox92 = new Telerik.Reporting.TextBox();
            this.textBox93 = new Telerik.Reporting.TextBox();
            this.textBox80 = new Telerik.Reporting.TextBox();
            this.textBox81 = new Telerik.Reporting.TextBox();
            this.textBox86 = new Telerik.Reporting.TextBox();
            this.textBox87 = new Telerik.Reporting.TextBox();
            this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();
            this.shape5 = new Telerik.Reporting.Shape();
            this.textBox44 = new Telerik.Reporting.TextBox();
            this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
            this.textBox42 = new Telerik.Reporting.TextBox();
            this.textBox55 = new Telerik.Reporting.TextBox();
            this.textBox56 = new Telerik.Reporting.TextBox();
            this.textBox57 = new Telerik.Reporting.TextBox();
            this.textBox58 = new Telerik.Reporting.TextBox();
            this.textBox59 = new Telerik.Reporting.TextBox();
            this.textBox60 = new Telerik.Reporting.TextBox();
            this.textBox61 = new Telerik.Reporting.TextBox();
            this.textBox62 = new Telerik.Reporting.TextBox();
            this.textBox63 = new Telerik.Reporting.TextBox();
            this.shape8 = new Telerik.Reporting.Shape();
            this.textBox64 = new Telerik.Reporting.TextBox();
            this.textBox65 = new Telerik.Reporting.TextBox();
            this.textBox66 = new Telerik.Reporting.TextBox();
            this.textBox67 = new Telerik.Reporting.TextBox();
            this.textBox68 = new Telerik.Reporting.TextBox();
            this.textBox69 = new Telerik.Reporting.TextBox();
            this.textBox70 = new Telerik.Reporting.TextBox();
            this.textBox71 = new Telerik.Reporting.TextBox();
            this.textBox72 = new Telerik.Reporting.TextBox();
            this.textBox73 = new Telerik.Reporting.TextBox();
            this.textBox74 = new Telerik.Reporting.TextBox();
            this.textBox75 = new Telerik.Reporting.TextBox();
            this.shape9 = new Telerik.Reporting.Shape();
            this.textBox76 = new Telerik.Reporting.TextBox();
            this.textBox77 = new Telerik.Reporting.TextBox();
            this.textBox78 = new Telerik.Reporting.TextBox();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.groupFooterSection2 = new Telerik.Reporting.GroupFooterSection();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.groupHeaderSection2 = new Telerik.Reporting.GroupHeaderSection();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(2.244D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox28,
            this.textBox2,
            this.textBox3,
            this.textBox9,
            this.textBox15,
            this.textBox16,
            this.textBox18,
            this.textBox19,
            this.textBox20,
            this.textBox21,
            this.textBox22,
            this.textBox25,
            this.textBox26,
            this.textBox29,
            this.textBox32,
            this.shape3,
            this.textBox35,
            this.textBox34,
            this.textBox94,
            this.textBox95,
            this.textBox96,
            this.textBox4,
            this.textBox7});
            this.pageHeader.Name = "pageHeader";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.781D), Telerik.Reporting.Drawing.Unit.Inch(1.5D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.019D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Arial";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Value = "Billing Summary";
            // 
            // textBox28
            // 
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.004D));
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox28.Style.Font.Name = "Arial";
            this.textBox28.Value = "Registration No";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2D), Telerik.Reporting.Drawing.Unit.Inch(0.604D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox2.Style.Font.Name = "Arial";
            this.textBox2.Value = ":";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.604D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox3.Style.Font.Name = "Arial";
            this.textBox3.Value = "Patient Name";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.204D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox9.Style.Font.Name = "Arial";
            this.textBox9.Value = "Registration Date";
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(1.004D));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox15.Style.Font.Name = "Arial";
            this.textBox15.Value = "Guarantor Name";
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2D), Telerik.Reporting.Drawing.Unit.Inch(1.004D));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox16.Style.Font.Name = "Arial";
            this.textBox16.Value = ":";
            // 
            // textBox18
            // 
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2D), Telerik.Reporting.Drawing.Unit.Inch(0.804D));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox18.Style.Font.Name = "Arial";
            this.textBox18.Value = ":";
            // 
            // textBox19
            // 
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2D), Telerik.Reporting.Drawing.Unit.Inch(0.004D));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox19.Style.Font.Name = "Arial";
            this.textBox19.Value = ":";
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.804D));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox20.Style.Font.Name = "Arial";
            this.textBox20.Value = "Room Name";
            // 
            // textBox21
            // 
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3D), Telerik.Reporting.Drawing.Unit.Inch(0.004D));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.2D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox21.Style.Font.Name = "Arial";
            this.textBox21.Value = "=RegistrationNo";
            // 
            // textBox22
            // 
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3D), Telerik.Reporting.Drawing.Unit.Inch(0.604D));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.2D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox22.Style.Font.Name = "Arial";
            this.textBox22.Value = "=PatientName";
            // 
            // textBox25
            // 
            this.textBox25.Format = "{0:dd/MM/yyyy}";
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3D), Telerik.Reporting.Drawing.Unit.Inch(0.204D));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.2D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox25.Style.Font.Name = "Arial";
            this.textBox25.Value = "=RegistrationDate";
            // 
            // textBox26
            // 
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3D), Telerik.Reporting.Drawing.Unit.Inch(0.804D));
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.2D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox26.Style.Font.Name = "Arial";
            this.textBox26.Value = "=RoomName + \" / \" + ClassName + \" / \" + BedID";
            // 
            // textBox29
            // 
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3D), Telerik.Reporting.Drawing.Unit.Inch(1.004D));
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.2D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox29.Style.Font.Name = "Arial";
            this.textBox29.Value = "=GuarantorName";
            // 
            // textBox32
            // 
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(1.911D));
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.777D), Telerik.Reporting.Drawing.Unit.Inch(0.211D));
            this.textBox32.Style.Font.Name = "Arial";
            this.textBox32.Value = "Detail Transaction";
            // 
            // shape3
            // 
            this.shape3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(2.122D));
            this.shape3.Name = "shape3";
            this.shape3.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.195D), Telerik.Reporting.Drawing.Unit.Cm(0.309D));
            this.shape3.Style.Font.Name = "Arial";
            // 
            // textBox35
            // 
            this.textBox35.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9D), Telerik.Reporting.Drawing.Unit.Inch(1.922D));
            this.textBox35.Name = "textBox35";
            this.textBox35.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.9D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox35.Style.Font.Name = "Arial";
            this.textBox35.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox35.Value = "Amount";
            // 
            // textBox34
            // 
            this.textBox34.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.9D), Telerik.Reporting.Drawing.Unit.Inch(1.922D));
            this.textBox34.Name = "textBox34";
            this.textBox34.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox34.Style.Font.Name = "Arial";
            this.textBox34.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox34.Value = "Total";
            // 
            // textBox94
            // 
            this.textBox94.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.404D));
            this.textBox94.Name = "textBox94";
            this.textBox94.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox94.Style.Font.Name = "Arial";
            this.textBox94.Value = "Medical No";
            // 
            // textBox95
            // 
            this.textBox95.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2D), Telerik.Reporting.Drawing.Unit.Inch(0.404D));
            this.textBox95.Name = "textBox95";
            this.textBox95.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox95.Style.Font.Name = "Arial";
            this.textBox95.Value = ":";
            // 
            // textBox96
            // 
            this.textBox96.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3D), Telerik.Reporting.Drawing.Unit.Inch(0.404D));
            this.textBox96.Name = "textBox96";
            this.textBox96.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.2D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox96.Style.Font.Name = "Arial";
            this.textBox96.Value = "=MedicalNo";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2D), Telerik.Reporting.Drawing.Unit.Inch(0.204D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox4.Style.Font.Name = "Arial";
            this.textBox4.Value = ":";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.895D), Telerik.Reporting.Drawing.Unit.Inch(1.922D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.9D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox7.Style.Font.Name = "Arial";
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox7.Value = "Discount";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox37,
            this.textBox39,
            this.textBox41,
            this.textBox5});
            this.detail.Name = "detail";
            this.detail.Style.Visible = false;
            // 
            // textBox37
            // 
            this.textBox37.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.015D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox37.Name = "textBox37";
            this.textBox37.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.762D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox37.Style.Font.Name = "Arial";
            this.textBox37.Value = "=ItemName";
            // 
            // textBox39
            // 
            this.textBox39.Format = "{0:N2}";
            this.textBox39.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox39.Name = "textBox39";
            this.textBox39.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.9D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox39.Style.Font.Name = "Arial";
            this.textBox39.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox39.Value = "=Price";
            // 
            // textBox41
            // 
            this.textBox41.Format = "{0:N2}";
            this.textBox41.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.9D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox41.Name = "textBox41";
            this.textBox41.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox41.Style.Font.Name = "Arial";
            this.textBox41.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox41.Value = "=Total";
            // 
            // textBox5
            // 
            this.textBox5.Format = "{0:N2}";
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.895D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.9D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox5.Style.Font.Name = "Arial";
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox5.Value = "=DiscountAmount";
            // 
            // shape6
            // 
            this.shape6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(-0.012D), Telerik.Reporting.Drawing.Unit.Cm(0.226D));
            this.shape6.Name = "shape6";
            this.shape6.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.205D), Telerik.Reporting.Drawing.Unit.Cm(0.281D));
            this.shape6.Style.Font.Name = "Arial";
            // 
            // textBox45
            // 
            this.textBox45.Format = "{0:N2}";
            this.textBox45.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.89D), Telerik.Reporting.Drawing.Unit.Inch(0.289D));
            this.textBox45.Name = "textBox45";
            this.textBox45.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.31D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox45.Style.Font.Name = "Arial";
            this.textBox45.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox45.Value = "= Sum(Total)";
            // 
            // textBox10
            // 
            this.textBox10.Format = "{0:N2}";
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.89D), Telerik.Reporting.Drawing.Unit.Inch(0.489D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.31D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox10.Style.Font.Name = "Arial";
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox10.Value = "=AdministrationAmount";
            // 
            // shape7
            // 
            this.shape7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.49D), Telerik.Reporting.Drawing.Unit.Inch(0.689D));
            this.shape7.Name = "shape7";
            this.shape7.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.705D), Telerik.Reporting.Drawing.Unit.Cm(0.281D));
            this.shape7.Style.Font.Name = "Arial";
            // 
            // textBox46
            // 
            this.textBox46.Format = "{0:N2}";
            this.textBox46.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.89D), Telerik.Reporting.Drawing.Unit.Inch(0.8D));
            this.textBox46.Name = "textBox46";
            this.textBox46.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.305D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox46.Style.Font.Name = "Arial";
            this.textBox46.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox46.Value = "= Sum(Total) + AdministrationAmount";
            // 
            // textBox47
            // 
            this.textBox47.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.485D), Telerik.Reporting.Drawing.Unit.Inch(0.289D));
            this.textBox47.Name = "textBox47";
            this.textBox47.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox47.Style.Font.Name = "Arial";
            this.textBox47.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox47.Value = "Total";
            // 
            // textBox48
            // 
            this.textBox48.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.79D), Telerik.Reporting.Drawing.Unit.Inch(0.289D));
            this.textBox48.Name = "textBox48";
            this.textBox48.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox48.Style.Font.Name = "Arial";
            this.textBox48.Value = ":";
            // 
            // textBox49
            // 
            this.textBox49.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.79D), Telerik.Reporting.Drawing.Unit.Inch(0.489D));
            this.textBox49.Name = "textBox49";
            this.textBox49.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox49.Style.Font.Name = "Arial";
            this.textBox49.Value = ":";
            // 
            // textBox50
            // 
            this.textBox50.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.485D), Telerik.Reporting.Drawing.Unit.Inch(0.489D));
            this.textBox50.Name = "textBox50";
            this.textBox50.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox50.Style.Font.Name = "Arial";
            this.textBox50.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox50.Value = "Administration (+)";
            // 
            // textBox79
            // 
            this.textBox79.Format = "{0:N2}";
            this.textBox79.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.89D), Telerik.Reporting.Drawing.Unit.Inch(1.089D));
            this.textBox79.Name = "textBox79";
            this.textBox79.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.31D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox79.Style.Font.Name = "Arial";
            this.textBox79.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox79.Value = "=DownPayment";
            // 
            // textBox82
            // 
            this.textBox82.Format = "{0:N2}";
            this.textBox82.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.89D), Telerik.Reporting.Drawing.Unit.Inch(1.289D));
            this.textBox82.Name = "textBox82";
            this.textBox82.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.31D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox82.Style.Font.Name = "Arial";
            this.textBox82.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox82.Value = "=PlavonAmount";
            // 
            // shape10
            // 
            this.shape10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.49D), Telerik.Reporting.Drawing.Unit.Inch(1.689D));
            this.shape10.Name = "shape10";
            this.shape10.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.705D), Telerik.Reporting.Drawing.Unit.Cm(0.281D));
            this.shape10.Style.Font.Name = "Arial";
            // 
            // textBox83
            // 
            this.textBox83.Format = "{0:N2}";
            this.textBox83.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.9D), Telerik.Reporting.Drawing.Unit.Inch(1.8D));
            this.textBox83.Name = "textBox83";
            this.textBox83.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox83.Style.Font.Name = "Arial";
            this.textBox83.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox83.Value = "=DownPayment + PlavonAmount + PaymentAmount";
            // 
            // textBox84
            // 
            this.textBox84.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.79D), Telerik.Reporting.Drawing.Unit.Inch(1.089D));
            this.textBox84.Name = "textBox84";
            this.textBox84.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox84.Style.Font.Name = "Arial";
            this.textBox84.Value = ":";
            // 
            // textBox85
            // 
            this.textBox85.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.49D), Telerik.Reporting.Drawing.Unit.Inch(1.089D));
            this.textBox85.Name = "textBox85";
            this.textBox85.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox85.Style.Font.Name = "Arial";
            this.textBox85.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox85.Value = "Down Payment (-)";
            // 
            // textBox88
            // 
            this.textBox88.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.79D), Telerik.Reporting.Drawing.Unit.Inch(1.289D));
            this.textBox88.Name = "textBox88";
            this.textBox88.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox88.Style.Font.Name = "Arial";
            this.textBox88.Value = ":";
            // 
            // textBox91
            // 
            this.textBox91.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.49D), Telerik.Reporting.Drawing.Unit.Inch(1.289D));
            this.textBox91.Name = "textBox91";
            this.textBox91.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox91.Style.Font.Name = "Arial";
            this.textBox91.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox91.Value = "Plafon (-)";
            // 
            // textBox92
            // 
            this.textBox92.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.485D), Telerik.Reporting.Drawing.Unit.Inch(2.089D));
            this.textBox92.Name = "textBox92";
            this.textBox92.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.305D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox92.Style.Font.Name = "Arial";
            this.textBox92.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox92.Value = "Remaining Amount";
            // 
            // textBox93
            // 
            this.textBox93.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.79D), Telerik.Reporting.Drawing.Unit.Inch(2.089D));
            this.textBox93.Name = "textBox93";
            this.textBox93.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox93.Style.Font.Name = "Arial";
            this.textBox93.Value = ":";
            // 
            // textBox80
            // 
            this.textBox80.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.49D), Telerik.Reporting.Drawing.Unit.Inch(1.489D));
            this.textBox80.Name = "textBox80";
            this.textBox80.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox80.Style.Font.Name = "Arial";
            this.textBox80.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox80.Value = "Payment (-)";
            // 
            // textBox81
            // 
            this.textBox81.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.79D), Telerik.Reporting.Drawing.Unit.Inch(1.489D));
            this.textBox81.Name = "textBox81";
            this.textBox81.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox81.Style.Font.Name = "Arial";
            this.textBox81.Value = ":";
            // 
            // textBox86
            // 
            this.textBox86.Format = "{0:N2}";
            this.textBox86.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.89D), Telerik.Reporting.Drawing.Unit.Inch(1.489D));
            this.textBox86.Name = "textBox86";
            this.textBox86.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.31D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox86.Style.Font.Name = "Arial";
            this.textBox86.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox86.Value = "=PaymentAmount";
            // 
            // textBox87
            // 
            this.textBox87.Format = "{0:N2}";
            this.textBox87.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.89D), Telerik.Reporting.Drawing.Unit.Inch(2.089D));
            this.textBox87.Name = "textBox87";
            this.textBox87.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.31D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox87.Style.Font.Name = "Arial";
            this.textBox87.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox87.Value = "=(Sum(Total) + AdministrationAmount) - (DownPayment + PlavonAmount + PaymentAmoun" +
    "t)";
            // 
            // groupFooterSection1
            // 
            this.groupFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.311D);
            this.groupFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.shape5,
            this.textBox44});
            this.groupFooterSection1.Name = "groupFooterSection1";
            // 
            // shape5
            // 
            this.shape5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.9D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.shape5.Name = "shape5";
            this.shape5.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3D), Telerik.Reporting.Drawing.Unit.Cm(0.281D));
            this.shape5.Style.Font.Name = "Arial";
            // 
            // textBox44
            // 
            this.textBox44.Format = "{0:N2}";
            this.textBox44.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.9D), Telerik.Reporting.Drawing.Unit.Inch(0.111D));
            this.textBox44.Name = "textBox44";
            this.textBox44.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox44.Style.Font.Name = "Arial";
            this.textBox44.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox44.Value = "= Sum(Total)";
            // 
            // groupHeaderSection1
            // 
            this.groupHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2D);
            this.groupHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox42});
            this.groupHeaderSection1.Name = "groupHeaderSection1";
            this.groupHeaderSection1.PrintOnEveryPage = true;
            this.groupHeaderSection1.Style.Font.Bold = true;
            this.groupHeaderSection1.Style.Font.Italic = true;
            // 
            // textBox42
            // 
            this.textBox42.Name = "textBox42";
            this.textBox42.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox42.Style.Font.Name = "Arial";
            this.textBox42.Value = "=BillingGroup";
            // 
            // textBox55
            // 
            this.textBox55.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.604D));
            this.textBox55.Name = "textBox55";
            this.textBox55.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox55.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox55.Value = "Stamp";
            // 
            // textBox56
            // 
            this.textBox56.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.302D), Telerik.Reporting.Drawing.Unit.Inch(0.604D));
            this.textBox56.Name = "textBox56";
            this.textBox56.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox56.Value = ":";
            // 
            // textBox57
            // 
            this.textBox57.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.406D));
            this.textBox57.Name = "textBox57";
            this.textBox57.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox57.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox57.Value = "Service";
            // 
            // textBox58
            // 
            this.textBox58.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.302D), Telerik.Reporting.Drawing.Unit.Inch(0.406D));
            this.textBox58.Name = "textBox58";
            this.textBox58.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox58.Value = ":";
            // 
            // textBox59
            // 
            this.textBox59.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.198D));
            this.textBox59.Name = "textBox59";
            this.textBox59.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox59.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox59.Value = "Administration";
            // 
            // textBox60
            // 
            this.textBox60.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.302D), Telerik.Reporting.Drawing.Unit.Inch(0.198D));
            this.textBox60.Name = "textBox60";
            this.textBox60.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox60.Value = ":";
            // 
            // textBox61
            // 
            this.textBox61.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.302D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox61.Name = "textBox61";
            this.textBox61.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox61.Value = ":";
            // 
            // textBox62
            // 
            this.textBox62.Name = "textBox62";
            this.textBox62.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox62.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox62.Value = "Total";
            // 
            // textBox63
            // 
            this.textBox63.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.406D), Telerik.Reporting.Drawing.Unit.Inch(0.917D));
            this.textBox63.Name = "textBox63";
            this.textBox63.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.295D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox63.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox63.Value = "textBox21";
            // 
            // shape8
            // 
            this.shape8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.406D), Telerik.Reporting.Drawing.Unit.Inch(0.802D));
            this.shape8.Name = "shape8";
            this.shape8.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.29D), Telerik.Reporting.Drawing.Unit.Cm(0.281D));
            // 
            // textBox64
            // 
            this.textBox64.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.406D), Telerik.Reporting.Drawing.Unit.Inch(0.604D));
            this.textBox64.Name = "textBox64";
            this.textBox64.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.295D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox64.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox64.Value = "textBox21";
            // 
            // textBox65
            // 
            this.textBox65.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.406D), Telerik.Reporting.Drawing.Unit.Inch(0.406D));
            this.textBox65.Name = "textBox65";
            this.textBox65.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.295D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox65.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox65.Value = "textBox21";
            // 
            // textBox66
            // 
            this.textBox66.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.406D), Telerik.Reporting.Drawing.Unit.Inch(0.198D));
            this.textBox66.Name = "textBox66";
            this.textBox66.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.295D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox66.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox66.Value = "textBox21";
            // 
            // textBox67
            // 
            this.textBox67.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.896D), Telerik.Reporting.Drawing.Unit.Inch(1.281D));
            this.textBox67.Name = "textBox67";
            this.textBox67.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox67.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox67.Value = "Stamp";
            // 
            // textBox68
            // 
            this.textBox68.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.198D), Telerik.Reporting.Drawing.Unit.Inch(1.281D));
            this.textBox68.Name = "textBox68";
            this.textBox68.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox68.Value = ":";
            // 
            // textBox69
            // 
            this.textBox69.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.896D), Telerik.Reporting.Drawing.Unit.Inch(1.083D));
            this.textBox69.Name = "textBox69";
            this.textBox69.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox69.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox69.Value = "Service";
            // 
            // textBox70
            // 
            this.textBox70.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.198D), Telerik.Reporting.Drawing.Unit.Inch(1.083D));
            this.textBox70.Name = "textBox70";
            this.textBox70.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox70.Value = ":";
            // 
            // textBox71
            // 
            this.textBox71.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.896D), Telerik.Reporting.Drawing.Unit.Inch(0.875D));
            this.textBox71.Name = "textBox71";
            this.textBox71.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox71.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox71.Value = "Administration";
            // 
            // textBox72
            // 
            this.textBox72.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.198D), Telerik.Reporting.Drawing.Unit.Inch(0.875D));
            this.textBox72.Name = "textBox72";
            this.textBox72.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox72.Value = ":";
            // 
            // textBox73
            // 
            this.textBox73.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.198D), Telerik.Reporting.Drawing.Unit.Inch(0.677D));
            this.textBox73.Name = "textBox73";
            this.textBox73.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox73.Value = ":";
            // 
            // textBox74
            // 
            this.textBox74.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.896D), Telerik.Reporting.Drawing.Unit.Inch(0.677D));
            this.textBox74.Name = "textBox74";
            this.textBox74.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox74.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox74.Value = "Total";
            // 
            // textBox75
            // 
            this.textBox75.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.302D), Telerik.Reporting.Drawing.Unit.Inch(1.594D));
            this.textBox75.Name = "textBox75";
            this.textBox75.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.295D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox75.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox75.Value = "textBox21";
            // 
            // shape9
            // 
            this.shape9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.302D), Telerik.Reporting.Drawing.Unit.Inch(1.479D));
            this.shape9.Name = "shape9";
            this.shape9.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.29D), Telerik.Reporting.Drawing.Unit.Cm(0.281D));
            // 
            // textBox76
            // 
            this.textBox76.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.302D), Telerik.Reporting.Drawing.Unit.Inch(1.281D));
            this.textBox76.Name = "textBox76";
            this.textBox76.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.295D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox76.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox76.Value = "textBox21";
            // 
            // textBox77
            // 
            this.textBox77.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.302D), Telerik.Reporting.Drawing.Unit.Inch(1.083D));
            this.textBox77.Name = "textBox77";
            this.textBox77.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.295D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox77.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox77.Value = "textBox21";
            // 
            // textBox78
            // 
            this.textBox78.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.302D), Telerik.Reporting.Drawing.Unit.Inch(0.875D));
            this.textBox78.Name = "textBox78";
            this.textBox78.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.295D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox78.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox78.Value = "textBox21";
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(6.068D);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox45,
            this.shape6,
            this.textBox10,
            this.shape7,
            this.textBox46,
            this.textBox47,
            this.textBox48,
            this.textBox49,
            this.textBox50,
            this.textBox79,
            this.textBox82,
            this.shape10,
            this.textBox83,
            this.textBox84,
            this.textBox85,
            this.textBox88,
            this.textBox91,
            this.textBox92,
            this.textBox93,
            this.textBox80,
            this.textBox81,
            this.textBox86,
            this.textBox87});
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.211D);
            this.pageFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox6,
            this.textBox8});
            this.pageFooterSection1.Name = "pageFooterSection1";
            this.pageFooterSection1.Style.Visible = true;
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox6.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox6.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox6.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "= \"Page \" + PageNumber + \"/\" + PageCount";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.015D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox8.Name = "textBox36";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.916D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox8.Style.Font.Name = "Arial";
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox8.Value = "Short Type";
            // 
            // groupFooterSection2
            // 
            this.groupFooterSection2.Height = Telerik.Reporting.Drawing.Unit.Cm(0.508D);
            this.groupFooterSection2.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox11,
            this.textBox12,
            this.textBox14,
            this.textBox17});
            this.groupFooterSection2.Name = "groupFooterSection2";
            // 
            // textBox11
            // 
            this.textBox11.Format = "{0:N2}";
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.895D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox11.Name = "textBox5";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.9D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox11.Style.Font.Name = "Arial";
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox11.Value = "=sum(DiscountAmount)";
            // 
            // textBox12
            // 
            this.textBox12.Format = "{0:N2}";
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.9D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox12.Name = "textBox41";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox12.Style.Font.Name = "Arial";
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox12.Value = "=sum(Total)";
            // 
            // textBox14
            // 
            this.textBox14.Format = "{0:N2}";
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox14.Name = "textBox39";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.9D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox14.Style.Font.Name = "Arial";
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox14.Value = "=sum(Price)";
            // 
            // textBox17
            // 
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.2D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox17.Name = "textBox37";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.762D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox17.Style.Font.Name = "Arial";
            this.textBox17.Value = "=ServiceUnitName";
            // 
            // groupHeaderSection2
            // 
            this.groupHeaderSection2.Height = Telerik.Reporting.Drawing.Unit.Cm(0.37D);
            this.groupHeaderSection2.Name = "groupHeaderSection2";
            this.groupHeaderSection2.Style.Visible = false;
            // 
            // BillingSummaryShortRpt
            // 
            group1.GroupFooter = this.groupFooterSection1;
            group1.GroupHeader = this.groupHeaderSection1;
            group1.Name = "group1";
            group2.GroupFooter = this.groupFooterSection2;
            group2.GroupHeader = this.groupHeaderSection2;
            group2.Name = "group2";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection1,
            this.groupFooterSection1,
            this.groupHeaderSection2,
            this.groupFooterSection2,
            this.pageHeader,
            this.detail,
            this.reportFooterSection1,
            this.pageFooterSection1});
            this.Name = "BillingSummaryShortRpt";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(0.55D), Telerik.Reporting.Drawing.Unit.Inch(0.75D), Telerik.Reporting.Drawing.Unit.Inch(0.3D), Telerik.Reporting.Drawing.Unit.Inch(0.75D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.2D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox28;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox32;
        private Shape shape3;
        private Telerik.Reporting.TextBox textBox35;
        private Telerik.Reporting.TextBox textBox34;
        private Telerik.Reporting.TextBox textBox37;
        private Telerik.Reporting.TextBox textBox39;
        private Telerik.Reporting.TextBox textBox41;
        private Group group1;
        private GroupFooterSection groupFooterSection1;
        private GroupHeaderSection groupHeaderSection1;
        private Telerik.Reporting.TextBox textBox42;
        private Shape shape5;
        private Telerik.Reporting.TextBox textBox44;
        private Shape shape6;
        private Telerik.Reporting.TextBox textBox45;
        private Telerik.Reporting.TextBox textBox10;
        private Shape shape7;
        private Telerik.Reporting.TextBox textBox46;
        private Telerik.Reporting.TextBox textBox47;
        private Telerik.Reporting.TextBox textBox48;
        private Telerik.Reporting.TextBox textBox49;
        private Telerik.Reporting.TextBox textBox50;
        private Telerik.Reporting.TextBox textBox55;
        private Telerik.Reporting.TextBox textBox56;
        private Telerik.Reporting.TextBox textBox57;
        private Telerik.Reporting.TextBox textBox58;
        private Telerik.Reporting.TextBox textBox59;
        private Telerik.Reporting.TextBox textBox60;
        private Telerik.Reporting.TextBox textBox61;
        private Telerik.Reporting.TextBox textBox62;
        private Telerik.Reporting.TextBox textBox63;
        private Shape shape8;
        private Telerik.Reporting.TextBox textBox64;
        private Telerik.Reporting.TextBox textBox65;
        private Telerik.Reporting.TextBox textBox66;
        private Telerik.Reporting.TextBox textBox67;
        private Telerik.Reporting.TextBox textBox68;
        private Telerik.Reporting.TextBox textBox69;
        private Telerik.Reporting.TextBox textBox70;
        private Telerik.Reporting.TextBox textBox71;
        private Telerik.Reporting.TextBox textBox72;
        private Telerik.Reporting.TextBox textBox73;
        private Telerik.Reporting.TextBox textBox74;
        private Telerik.Reporting.TextBox textBox75;
        private Shape shape9;
        private Telerik.Reporting.TextBox textBox76;
        private Telerik.Reporting.TextBox textBox77;
        private Telerik.Reporting.TextBox textBox78;
        private Telerik.Reporting.TextBox textBox82;
        private Shape shape10;
        private Telerik.Reporting.TextBox textBox83;
        private Telerik.Reporting.TextBox textBox84;
        private Telerik.Reporting.TextBox textBox85;
        private Telerik.Reporting.TextBox textBox88;
        private Telerik.Reporting.TextBox textBox91;
        private Telerik.Reporting.TextBox textBox92;
        private Telerik.Reporting.TextBox textBox93;
        private Telerik.Reporting.TextBox textBox94;
        private Telerik.Reporting.TextBox textBox95;
        private Telerik.Reporting.TextBox textBox96;
        private Telerik.Reporting.TextBox textBox79;
        private Telerik.Reporting.TextBox textBox80;
        private Telerik.Reporting.TextBox textBox81;
        private Telerik.Reporting.TextBox textBox86;
        private Telerik.Reporting.TextBox textBox87;
        private ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.TextBox textBox4;
        private PageFooterSection pageFooterSection1;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Group group2;
        private GroupFooterSection groupFooterSection2;
        private GroupHeaderSection groupHeaderSection2;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox17;
    }
}