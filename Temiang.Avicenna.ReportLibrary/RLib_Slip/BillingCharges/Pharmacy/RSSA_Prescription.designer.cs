namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Pharmacy
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class RSSA_Prescription
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.shape4 = new Telerik.Reporting.Shape();
            this.reportHeaderSection1 = new Telerik.Reporting.ReportHeaderSection();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.textBox56 = new Telerik.Reporting.TextBox();
            this.textBox69 = new Telerik.Reporting.TextBox();
            this.textBox65 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.shape6 = new Telerik.Reporting.Shape();
            this.shape1 = new Telerik.Reporting.Shape();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.group1 = new Telerik.Reporting.Group();
            this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();
            this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox25,
            this.textBox17,
            this.textBox8,
            this.textBox18,
            this.textBox20});
            this.detail.Name = "detail";
            this.detail.Style.Font.Name = "Times New Roman";
            this.detail.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.detail.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            // 
            // textBox25
            // 
            this.textBox25.CanGrow = false;
            this.textBox25.Format = "  {0:0}";
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox25.Name = "textBox16";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.23984237015247345), Telerik.Reporting.Drawing.Unit.Inch(0.1999211311340332));
            this.textBox25.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox25.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox25.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox25.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox25.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox25.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox25.Value = "=IsRFlag";
            // 
            // textBox17
            // 
            this.textBox17.CanGrow = false;
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.23992109298706055), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox17.Name = "textBox1";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7303543090820313), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox17.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox17.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox17.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox17.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox17.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox17.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox17.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox17.Value = "=ItemName";
            // 
            // textBox8
            // 
            this.textBox8.CanGrow = false;
            this.textBox8.Format = "{0:N0}";
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.4000003337860107), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox8.Name = "textBox1";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.39583444595336914), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox8.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox8.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox8.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox8.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox8.Style.Font.Name = "Tahoma";
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox8.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox8.Value = "=SRItemUnit";
            // 
            // textBox18
            // 
            this.textBox18.CanGrow = false;
            this.textBox18.Format = "{0:N0}";
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.9703543186187744), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox18.Name = "textBox1";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.42952990531921387), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox18.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox18.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox18.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox18.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox18.Style.Font.Name = "Tahoma";
            this.textBox18.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox18.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox18.Value = "=PrescriptionQty";
            // 
            // textBox20
            // 
            this.textBox20.CanGrow = true;
            this.textBox20.Format = "";
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.7959134578704834), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.90400755405426025), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox20.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox20.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox20.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox20.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox20.Style.Font.Name = "Tahoma";
            this.textBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox20.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox20.Value = "=Note";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.10007882118225098));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.6065604686737061), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox12.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.Value = "Terima Kasih Atas Kunjungan Anda";
            // 
            // shape4
            // 
            this.shape4.Angle = 0;
            this.shape4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.010000016540288925), Telerik.Reporting.Drawing.Unit.Inch(0.047916624695062637));
            this.shape4.Name = "shape4";
            this.shape4.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.6065604686737061), Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699));
            this.shape4.Style.Font.Name = "Microsoft Sans Serif";
            this.shape4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.shape4.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Point(0.699999988079071);
            this.shape4.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Mm(1);
            // 
            // reportHeaderSection1
            // 
            this.reportHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(2.5);
            this.reportHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox29,
            this.textBox3,
            this.textBox2,
            this.textBox22,
            this.textBox5,
            this.textBox21,
            this.textBox10,
            this.textBox4,
            this.textBox23,
            this.textBox6,
            this.textBox24,
            this.textBox7,
            this.textBox26,
            this.textBox27,
            this.textBox28,
            this.textBox31,
            this.textBox56,
            this.textBox69,
            this.textBox65,
            this.textBox32,
            this.textBox1,
            this.textBox9,
            this.textBox11,
            this.shape6,
            this.shape1});
            this.reportHeaderSection1.Name = "reportHeaderSection1";
            this.reportHeaderSection1.Style.Font.Name = "Calibri";
            // 
            // textBox29
            // 
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.5225951671600342), Telerik.Reporting.Drawing.Unit.Inch(1.5));
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1773656606674194), Telerik.Reporting.Drawing.Unit.Inch(0.19992129504680634));
            this.textBox29.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox29.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox29.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox29.Value = "=PrescriptionNo";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.41011810302734375), Telerik.Reporting.Drawing.Unit.Inch(1.5));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.059999998658895493), Telerik.Reporting.Drawing.Unit.Inch(0.19992129504680634));
            this.textBox3.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = ":";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.970354437828064), Telerik.Reporting.Drawing.Unit.Inch(1.5));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.5), Telerik.Reporting.Drawing.Unit.Inch(0.19992129504680634));
            this.textBox2.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "Resep";
            // 
            // textBox22
            // 
            this.textBox22.Format = "{0:dd/MM/yy hh:mm}";
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.5225951671600342), Telerik.Reporting.Drawing.Unit.Inch(1.6999212503433228));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1773656606674194), Telerik.Reporting.Drawing.Unit.Inch(0.1999211311340332));
            this.textBox22.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox22.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox22.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox22.Value = "=ApprovalDateTime";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.970354437828064), Telerik.Reporting.Drawing.Unit.Inch(1.6999212503433228));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.5), Telerik.Reporting.Drawing.Unit.Inch(0.1999211311340332));
            this.textBox5.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox5.Value = "Tanggal";
            // 
            // textBox21
            // 
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.47019681334495544), Telerik.Reporting.Drawing.Unit.Inch(2.0999999046325684));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.500078558921814), Telerik.Reporting.Drawing.Unit.Inch(0.1999211311340332));
            this.textBox21.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox21.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox21.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox21.Value = "=ParamedicName";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.41011810302734375), Telerik.Reporting.Drawing.Unit.Inch(2.0999999046325684));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.059999998658895493), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox10.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox10.Value = ":";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0099999997764825821), Telerik.Reporting.Drawing.Unit.Inch(2.0999999046325684));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox4.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.Value = "Dokter";
            // 
            // textBox23
            // 
            this.textBox23.CanGrow = false;
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.5225951671600342), Telerik.Reporting.Drawing.Unit.Inch(1.899999737739563));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1773656606674194), Telerik.Reporting.Drawing.Unit.Inch(0.19992129504680634));
            this.textBox23.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox23.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox23.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox23.Value = "=RegistrationNo";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.970354437828064), Telerik.Reporting.Drawing.Unit.Inch(1.899999737739563));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.5), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox6.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "No. Reg";
            // 
            // textBox24
            // 
            this.textBox24.CanGrow = false;
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.47027555108070374), Telerik.Reporting.Drawing.Unit.Inch(1.5));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5), Telerik.Reporting.Drawing.Unit.Inch(0.19992129504680634));
            this.textBox24.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox24.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox24.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox24.Value = "=PatientName";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0099999997764825821), Telerik.Reporting.Drawing.Unit.Inch(1.4999213218688965));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448), Telerik.Reporting.Drawing.Unit.Inch(0.1999211311340332));
            this.textBox7.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.Value = "Pasien";
            // 
            // textBox26
            // 
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579));
            this.textBox26.Name = "textBox2";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.6065604686737061), Telerik.Reporting.Drawing.Unit.Inch(0.19992129504680634));
            this.textBox26.Style.Font.Bold = true;
            this.textBox26.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox26.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14);
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox26.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox26.Value = "COPY RESEP";
            // 
            // textBox27
            // 
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.470433235168457), Telerik.Reporting.Drawing.Unit.Inch(1.5000001192092896));
            this.textBox27.Name = "textBox14";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699), Telerik.Reporting.Drawing.Unit.Inch(0.1999211311340332));
            this.textBox27.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox27.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox27.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox27.Value = ":";
            // 
            // textBox28
            // 
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.41011810302734375), Telerik.Reporting.Drawing.Unit.Inch(1.899999737739563));
            this.textBox28.Name = "textBox10";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.059999998658895493), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox28.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox28.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox28.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox28.Value = ":";
            // 
            // textBox31
            // 
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0099999997764825821), Telerik.Reporting.Drawing.Unit.Inch(1.8974703550338745));
            this.textBox31.Name = "textBox7";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox31.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox31.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox31.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox31.Value = "Ruang";
            // 
            // textBox56
            // 
            this.textBox56.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.47019681334495544), Telerik.Reporting.Drawing.Unit.Inch(1.6999999284744263));
            this.textBox56.Name = "textBox9";
            this.textBox56.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5000786781311035), Telerik.Reporting.Drawing.Unit.Inch(0.1999211311340332));
            this.textBox56.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox56.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox56.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox56.Value = "=GuarantorName";
            // 
            // textBox69
            // 
            this.textBox69.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.5225954055786133), Telerik.Reporting.Drawing.Unit.Inch(2.0999999046325684));
            this.textBox69.Name = "textBox16";
            this.textBox69.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1773654222488403), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox69.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox69.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox69.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox69.Value = "=MedicalNo";
            // 
            // textBox65
            // 
            this.textBox65.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.970354437828064), Telerik.Reporting.Drawing.Unit.Inch(2.0999999046325684));
            this.textBox65.Name = "textBox65";
            this.textBox65.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.5), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox65.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox65.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox65.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox65.Value = "No. MR";
            // 
            // textBox32
            // 
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.47019687294960022), Telerik.Reporting.Drawing.Unit.Inch(1.899999737739563));
            this.textBox32.Name = "textBox9";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.500078558921814), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox32.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox32.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox32.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox32.Value = "=ServiceUnitName";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.470433235168457), Telerik.Reporting.Drawing.Unit.Inch(1.6999999284744263));
            this.textBox1.Name = "textBox14";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699), Telerik.Reporting.Drawing.Unit.Inch(0.1999211311340332));
            this.textBox1.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = ":";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.470433235168457), Telerik.Reporting.Drawing.Unit.Inch(1.899999737739563));
            this.textBox9.Name = "textBox14";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699), Telerik.Reporting.Drawing.Unit.Inch(0.1999211311340332));
            this.textBox9.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox9.Value = ":";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.470433235168457), Telerik.Reporting.Drawing.Unit.Inch(2.0999999046325684));
            this.textBox11.Name = "textBox14";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699), Telerik.Reporting.Drawing.Unit.Inch(0.1999211311340332));
            this.textBox11.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox11.Value = ":";
            // 
            // shape6
            // 
            this.shape6.Angle = 0;
            this.shape6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(2.4000000953674316));
            this.shape6.Name = "shape2";
            this.shape6.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.6065604686737061), Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699));
            this.shape6.Style.Font.Name = "Microsoft Sans Serif";
            this.shape6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.shape6.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Point(0.699999988079071);
            this.shape6.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Mm(1);
            // 
            // shape1
            // 
            this.shape1.Angle = 0;
            this.shape1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0416666679084301), Telerik.Reporting.Drawing.Unit.Inch(0.89999991655349731));
            this.shape1.Name = "shape1";
            this.shape1.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.6065208911895752), Telerik.Reporting.Drawing.Unit.Inch(0.15208323299884796));
            this.shape1.Style.Font.Name = "Microsoft Sans Serif";
            this.shape1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.8000001907348633);
            this.shape1.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.shape1.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Mm(1);
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699);
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // group1
            // 
            this.group1.GroupFooter = this.groupFooterSection1;
            this.group1.GroupHeader = this.groupHeaderSection1;
            this.group1.Name = "group1";
            // 
            // groupFooterSection1
            // 
            this.groupFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.3000788688659668);
            this.groupFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.shape4,
            this.textBox12});
            this.groupFooterSection1.Name = "groupFooterSection1";
            // 
            // groupHeaderSection1
            // 
            this.groupHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.052083492279052734);
            this.groupHeaderSection1.Name = "groupHeaderSection1";
            // 
            // RSSA_Prescription
            // 
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            this.group1});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection1,
            this.groupFooterSection1,
            this.detail,
            this.reportHeaderSection1,
            this.reportFooterSection1});
            this.Name = "RSSA_Prescription";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Mm(5);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Mm(2);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Mm(0);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageSettings.PaperSize = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4), Telerik.Reporting.Drawing.Unit.Inch(6));
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.UnitOfMeasure = Telerik.Reporting.Drawing.UnitType.Mm;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(3.6999602317810059);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.DetailSection detail;
        private ReportHeaderSection reportHeaderSection1;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox28;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox textBox56;
        private Telerik.Reporting.TextBox textBox69;
        private Telerik.Reporting.TextBox textBox65;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox12;
        private Shape shape4;
        private Shape shape6;
        private ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox18;
        private Group group1;
        private GroupFooterSection groupFooterSection1;
        private GroupHeaderSection groupHeaderSection1;
        private Shape shape1;
    }
}