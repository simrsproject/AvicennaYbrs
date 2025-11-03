namespace Temiang.Avicenna.ReportLibrary.Finance.Payable.RSUSKY
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class APMutationRecapRpt
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.shape3 = new Telerik.Reporting.Shape();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.txtPeriode = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(2.2404921054840088);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.shape3,
            this.textBox6,
            this.textBox9,
            this.textBox10,
            this.textBox28,
            this.textBox5,
            this.textBox4,
            this.textBox1,
            this.txtPeriode});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            // 
            // shape3
            // 
            this.shape3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.62992125749588013));
            this.shape3.Name = "shape3";
            this.shape3.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.4999213218688965), Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699));
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.7953548431396484), Telerik.Reporting.Drawing.Unit.Inch(0.6820833683013916));
            this.textBox6.Name = "textBox23";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82669258117675781), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "Saldo Awal";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.6221263408660889), Telerik.Reporting.Drawing.Unit.Inch(0.68208342790603638));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.90543299913406372), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox9.Value = "Debet";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.5276379585266113), Telerik.Reporting.Drawing.Unit.Inch(0.68208342790603638));
            this.textBox10.Name = "textBox9";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97271060943603516), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox10.Value = "Kredit";
            // 
            // textBox28
            // 
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.50042724609375), Telerik.Reporting.Drawing.Unit.Inch(0.68208342790603638));
            this.textBox28.Name = "textBox23";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99953299760818481), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox28.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox28.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox28.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox28.Value = "Saldo Akhir";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.68208330869674683));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.62988185882568359), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox5.Value = "ID";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.62999993562698364), Telerik.Reporting.Drawing.Unit.Inch(0.6820833683013916));
            this.textBox4.Name = "textBox7";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1652758121490479), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.Value = "Supplier";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.0472440719604492), Telerik.Reporting.Drawing.Unit.Inch(0.11811026185750961));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.45271635055542), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "Rekap Mutasi Hutang";
            // 
            // txtPeriode
            // 
            this.txtPeriode.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.0472440719604492), Telerik.Reporting.Drawing.Unit.Inch(0.31818905472755432));
            this.txtPeriode.Name = "txtPeriode";
            this.txtPeriode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.45271635055542), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.txtPeriode.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.txtPeriode.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtPeriode.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.txtPeriode.Value = "";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.55950796604156494);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox2,
            this.textBox7,
            this.textBox8,
            this.textBox15,
            this.textBox16,
            this.textBox26});
            this.detail.Name = "detail";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(7.8519187809433788E-05));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.62988185882568359), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox2.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "=SupplierID";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.62999993562698364), Telerik.Reporting.Drawing.Unit.Inch(7.8678131103515625E-05));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1652758121490479), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox7.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox7.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0.039999999105930328);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.Value = "=SupplierName";
            // 
            // textBox8
            // 
            this.textBox8.Format = "{0:N2}";
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.7953548431396484), Telerik.Reporting.Drawing.Unit.Inch(0.0035627682227641344));
            this.textBox8.Name = "textBox13";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82669258117675781), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox8.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox8.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0.05000000074505806);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Value = "=SaldoAwal";
            // 
            // textBox15
            // 
            this.textBox15.Format = "{0:N2}";
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.6221263408660889), Telerik.Reporting.Drawing.Unit.Inch(0.0035627682227641344));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.90543299913406372), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox15.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox15.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0.05000000074505806);
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox15.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox15.Value = "=Debit";
            // 
            // textBox16
            // 
            this.textBox16.Format = "{0:N2}";
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.5276379585266113), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox16.Name = "textBox13";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97271060943603516), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox16.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox16.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0.05000000074505806);
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox16.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox16.Value = "=Credit";
            // 
            // textBox26
            // 
            this.textBox26.Format = "{0:N2}";
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.5004668235778809), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox26.Name = "textBox13";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99953365325927734), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox26.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox26.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox26.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0.05000000074505806);
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox26.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox26.Value = "=(saldoawal-debit+credit)";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.50000017881393433);
            this.pageFooter.Name = "pageFooter";
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.599999725818634);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox11,
            this.textBox12,
            this.textBox13,
            this.textBox14,
            this.textBox3});
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // textBox11
            // 
            this.textBox11.Format = "{0:N2}";
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.4803149700164795), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox11.Name = "textBox13";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1417325735092163), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox11.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.textBox11.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox11.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0.05000000074505806);
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox11.Value = "=Sum(SaldoAwal)";
            // 
            // textBox12
            // 
            this.textBox12.Format = "{0:N2}";
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.6221263408660889), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.90543299913406372), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox12.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.textBox12.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.textBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox12.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0.05000000074505806);
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.Value = "=Sum(Debit)";
            // 
            // textBox13
            // 
            this.textBox13.Format = "{0:N2}";
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.5276379585266113), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97271060943603516), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox13.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.textBox13.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox13.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0.05000000074505806);
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.Value = "=Sum(Credit)";
            // 
            // textBox14
            // 
            this.textBox14.Format = "{0:N2}";
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.5004668235778809), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox14.Name = "textBox13";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99953365325927734), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox14.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.textBox14.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox14.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0.05000000074505806);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox14.Value = "=sum(saldoawal-debit+credit)";
            // 
            // textBox3
            // 
            this.textBox3.Format = "{0:N2}";
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5748032331466675), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox3.Name = "textBox13";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.82669258117675781), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.textBox3.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox3.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            this.textBox3.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0.05000000074505806);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = "Grand Total:";
            // 
            // APMutationRecapRpt
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail,
            this.pageFooter,
            this.reportFooterSection1});
            this.Name = "APMutationRecapRpt";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Cm(2.5399999618530273);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Cm(2.5399999618530273);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Cm(2.5399999618530273);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(2.5399999618530273);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(16.510000228881836);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Shape shape3;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox28;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox txtPeriode;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox26;
        private ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox3;
    }
}