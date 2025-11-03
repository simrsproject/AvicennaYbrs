namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class TestResultNative
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
            this.shape2 = new Telerik.Reporting.Shape();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.shape1 = new Telerik.Reporting.Shape();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox33 = new Telerik.Reporting.TextBox();
            this.txtItemID = new Telerik.Reporting.TextBox();
            this.htmlTextBox1 = new Telerik.Reporting.HtmlTextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.txtDokter = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(2.2999999523162842);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox12,
            this.shape2,
            this.textBox2,
            this.textBox3,
            this.textBox4,
            this.textBox5,
            this.textBox11,
            this.shape1,
            this.textBox13,
            this.textBox14,
            this.textBox15,
            this.textBox16,
            this.textBox22,
            this.textBox23,
            this.textBox29,
            this.textBox30,
            this.textBox31,
            this.textBox32});
            this.pageHeader.Name = "pageHeader";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582), Telerik.Reporting.Drawing.Unit.Inch(0.71041649580001831));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.8000004291534424), Telerik.Reporting.Drawing.Unit.Inch(0.19999992847442627));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "HASIL PEMERIKSAAN RADIOLOGI";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582), Telerik.Reporting.Drawing.Unit.Inch(0.91041666269302368));
            this.textBox12.Name = "textBox3";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3916668891906738), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox12.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.Value = "Examination(s):";
            // 
            // shape2
            // 
            this.shape2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.19999988377094269), Telerik.Reporting.Drawing.Unit.Inch(1.1104166507720947));
            this.shape2.Name = "shape2";
            this.shape2.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.1000008583068848), Telerik.Reporting.Drawing.Unit.Inch(0.11250000447034836));
            this.shape2.Style.Font.Bold = false;
            this.shape2.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Point(0.699999988079071);
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582), Telerik.Reporting.Drawing.Unit.Inch(1.222995400428772));
            this.textBox2.Name = "textBox3";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3916667699813843), Telerik.Reporting.Drawing.Unit.Inch(0.18742132186889648));
            this.textBox2.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "Nama";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179), Telerik.Reporting.Drawing.Unit.Inch(1.410495400428772));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3916667699813843), Telerik.Reporting.Drawing.Unit.Inch(0.18742132186889648));
            this.textBox3.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = "Medical No.";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179), Telerik.Reporting.Drawing.Unit.Inch(1.5979957580566406));
            this.textBox4.Name = "textBox3";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3916667699813843), Telerik.Reporting.Drawing.Unit.Inch(0.18742132186889648));
            this.textBox4.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.Value = "Tanggal Lahir ";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179), Telerik.Reporting.Drawing.Unit.Inch(1.7854958772659302));
            this.textBox5.Name = "textBox3";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3916667699813843), Telerik.Reporting.Drawing.Unit.Inch(0.18742132186889648));
            this.textBox5.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox5.Value = "Jenis Kelamin ";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179), Telerik.Reporting.Drawing.Unit.Inch(1.9729958772659302));
            this.textBox11.Name = "textBox3";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3917456865310669), Telerik.Reporting.Drawing.Unit.Inch(0.18742132186889648));
            this.textBox11.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox11.Value = "Tanggal ";
            // 
            // shape1
            // 
            this.shape1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672), Telerik.Reporting.Drawing.Unit.Inch(2.1604957580566406));
            this.shape1.Name = "shape1";
            this.shape1.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.1000008583068848), Telerik.Reporting.Drawing.Unit.Inch(0.11250000447034836));
            this.shape1.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Point(0.699999988079071);
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.6999999284744263), Telerik.Reporting.Drawing.Unit.Inch(1.222995400428772));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.5000004768371582), Telerik.Reporting.Drawing.Unit.Inch(0.18742132186889648));
            this.textBox13.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.Value = "=PatientName";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.6999999284744263), Telerik.Reporting.Drawing.Unit.Inch(1.4104957580566406));
            this.textBox14.Name = "textBox3";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.5000014305114746), Telerik.Reporting.Drawing.Unit.Inch(0.18742132186889648));
            this.textBox14.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox14.Value = "=MedicalNo";
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.6999999284744263), Telerik.Reporting.Drawing.Unit.Inch(1.5979958772659302));
            this.textBox15.Name = "textBox3";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.5000004768371582), Telerik.Reporting.Drawing.Unit.Inch(0.18742132186889648));
            this.textBox15.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox15.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox15.Value = "=DateOfBirth";
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.6999999284744263), Telerik.Reporting.Drawing.Unit.Inch(1.7854964733123779));
            this.textBox16.Name = "textBox3";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.5000004768371582), Telerik.Reporting.Drawing.Unit.Inch(0.18742132186889648));
            this.textBox16.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox16.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox16.Value = "=Sex";
            // 
            // textBox22
            // 
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.6999999284744263), Telerik.Reporting.Drawing.Unit.Inch(1.9729958772659302));
            this.textBox22.Name = "textBox3";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.5000004768371582), Telerik.Reporting.Drawing.Unit.Inch(0.18742132186889648));
            this.textBox22.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox22.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox22.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox22.Value = "=TestResultDateTime";
            // 
            // textBox23
            // 
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.6000794172286987), Telerik.Reporting.Drawing.Unit.Inch(1.9729958772659302));
            this.textBox23.Name = "textBox9";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099920906126499176), Telerik.Reporting.Drawing.Unit.Inch(0.18742115795612335));
            this.textBox23.Style.Font.Bold = true;
            this.textBox23.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox23.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox23.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox23.Value = ":";
            // 
            // textBox29
            // 
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.600000262260437), Telerik.Reporting.Drawing.Unit.Inch(1.7854958772659302));
            this.textBox29.Name = "textBox9";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099920906126499176), Telerik.Reporting.Drawing.Unit.Inch(0.18742115795612335));
            this.textBox29.Style.Font.Bold = true;
            this.textBox29.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox29.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox29.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox29.Value = ":";
            // 
            // textBox30
            // 
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.600000262260437), Telerik.Reporting.Drawing.Unit.Inch(1.5979957580566406));
            this.textBox30.Name = "textBox9";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099920906126499176), Telerik.Reporting.Drawing.Unit.Inch(0.18742115795612335));
            this.textBox30.Style.Font.Bold = true;
            this.textBox30.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox30.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox30.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox30.Value = ":";
            // 
            // textBox31
            // 
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.600000262260437), Telerik.Reporting.Drawing.Unit.Inch(1.4104957580566406));
            this.textBox31.Name = "textBox9";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099920906126499176), Telerik.Reporting.Drawing.Unit.Inch(0.18742115795612335));
            this.textBox31.Style.Font.Bold = true;
            this.textBox31.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox31.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox31.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox31.Value = ":";
            // 
            // textBox32
            // 
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.600000262260437), Telerik.Reporting.Drawing.Unit.Inch(1.222995400428772));
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099920906126499176), Telerik.Reporting.Drawing.Unit.Inch(0.18742115795612335));
            this.textBox32.Style.Font.Bold = true;
            this.textBox32.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox32.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox32.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox32.Value = ":";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(1.0770829916000366);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox33,
            this.txtItemID,
            this.htmlTextBox1});
            this.detail.Name = "detail";
            // 
            // textBox33
            // 
            this.textBox33.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985), Telerik.Reporting.Drawing.Unit.Inch(7.8837074397597462E-05));
            this.textBox33.Name = "textBox1";
            this.textBox33.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.1000008583068848), Telerik.Reporting.Drawing.Unit.Inch(0.29976251721382141));
            this.textBox33.Style.Font.Bold = true;
            this.textBox33.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox33.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox33.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox33.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox33.Style.Visible = false;
            this.textBox33.Value = "Hasil, Kesan dan Saran:";
            // 
            // txtItemID
            // 
            this.txtItemID.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.59999996423721313), Telerik.Reporting.Drawing.Unit.Inch(0.29992008209228516));
            this.txtItemID.Name = "txtItemID";
            this.txtItemID.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.8020839691162109), Telerik.Reporting.Drawing.Unit.Inch(0.29992148280143738));
            this.txtItemID.Style.Font.Bold = true;
            this.txtItemID.Style.Font.Name = "Microsoft Sans Serif";
            this.txtItemID.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(13);
            this.txtItemID.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtItemID.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.txtItemID.Value = "=itemName";
            // 
            // htmlTextBox1
            // 
            this.htmlTextBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.60000008344650269), Telerik.Reporting.Drawing.Unit.Inch(0.77708292007446289));
            this.htmlTextBox1.Name = "htmlTextBox1";
            this.htmlTextBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.400001049041748), Telerik.Reporting.Drawing.Unit.Inch(0.29999923706054688));
            this.htmlTextBox1.Value = "<p>{Fields.Result}</p><p></p>";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(1.5229960680007935);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtDokter,
            this.textBox6,
            this.textBox7});
            this.pageFooter.Name = "pageFooter";
            // 
            // txtDokter
            // 
            this.txtDokter.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4), Telerik.Reporting.Drawing.Unit.Inch(1.1229175329208374));
            this.txtDokter.Name = "txtDokter";
            this.txtDokter.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.0000007152557373), Telerik.Reporting.Drawing.Unit.Inch(0.19999992847442627));
            this.txtDokter.Style.Font.Bold = false;
            this.txtDokter.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.txtDokter.Style.Font.Underline = true;
            this.txtDokter.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtDokter.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.txtDokter.Value = "=ParamedicName";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4), Telerik.Reporting.Drawing.Unit.Inch(1.3229961395263672));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.0000007152557373), Telerik.Reporting.Drawing.Unit.Inch(0.19999992847442627));
            this.textBox6.Style.Font.Bold = false;
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "=LicenseNo";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4), Telerik.Reporting.Drawing.Unit.Inch(0.22291724383831024));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.0000007152557373), Telerik.Reporting.Drawing.Unit.Inch(0.19999992847442627));
            this.textBox7.Style.Font.Bold = false;
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.Value = "Hormat Kami,";
            // 
            // TestResultNative
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
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.2000007629394531);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox12;
        private Shape shape2;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox11;
        private Shape shape1;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.TextBox textBox33;
        private Telerik.Reporting.TextBox txtItemID;
        private Telerik.Reporting.TextBox txtDokter;
        private HtmlTextBox htmlTextBox1;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox7;
    }
}