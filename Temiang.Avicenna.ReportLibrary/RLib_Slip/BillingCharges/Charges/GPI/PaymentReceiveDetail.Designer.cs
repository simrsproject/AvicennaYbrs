namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges.GPI
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class PaymentReceiveDetail
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.Drawing.FormattingRule formattingRule1 = new Telerik.Reporting.Drawing.FormattingRule();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox37 = new Telerik.Reporting.TextBox();
            this.textBox41 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox38 = new Telerik.Reporting.TextBox();
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
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox94 = new Telerik.Reporting.TextBox();
            this.textBox95 = new Telerik.Reporting.TextBox();
            this.textBox96 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.txtTerimaOleh = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.textBox35 = new Telerik.Reporting.TextBox();
            this.txtTotalAmountInWords = new Telerik.Reporting.TextBox();
            this.textBox40 = new Telerik.Reporting.TextBox();
            this.textBox44 = new Telerik.Reporting.TextBox();
            this.txtPaymentNo = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.textBox50 = new Telerik.Reporting.TextBox();
            this.textBox49 = new Telerik.Reporting.TextBox();
            this.textBox48 = new Telerik.Reporting.TextBox();
            this.textBox47 = new Telerik.Reporting.TextBox();
            this.txtTotalG = new Telerik.Reporting.TextBox();
            this.shape7 = new Telerik.Reporting.Shape();
            this.txtAdministrationAmount = new Telerik.Reporting.TextBox();
            this.textBox45 = new Telerik.Reporting.TextBox();
            this.TxtCityRS = new Telerik.Reporting.TextBox();
            this.txtUserName = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.txtPaymentMethod = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox36 = new Telerik.Reporting.TextBox();
            this.textBox39 = new Telerik.Reporting.TextBox();
            this.txtDownpayment = new Telerik.Reporting.TextBox();
            this.txtKembalian = new Telerik.Reporting.TextBox();
            this.textBox81 = new Telerik.Reporting.TextBox();
            this.textBox82 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.shape1 = new Telerik.Reporting.Shape();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.txtTotalP = new Telerik.Reporting.TextBox();
            this.reportHeaderSection1 = new Telerik.Reporting.ReportHeaderSection();
            this.textBox34 = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox33 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.29999998211860657);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox37,
            this.textBox41,
            this.textBox12,
            this.textBox31,
            this.textBox14,
            this.textBox38});
            this.detail.Name = "detail";
            this.detail.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.detail.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            // 
            // textBox37
            // 
            this.textBox37.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.099999986588954926), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox37.Name = "textBox37";
            this.textBox37.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.1024417877197266), Telerik.Reporting.Drawing.Unit.Inch(0.30000001192092896));
            this.textBox37.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox37.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox37.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox37.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox37.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox37.Value = "=ItemName";
            // 
            // textBox41
            // 
            this.textBox41.Format = "{0:N2}";
            this.textBox41.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.5725994110107422), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox41.Name = "textBox41";
            this.textBox41.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.88480573892593384), Telerik.Reporting.Drawing.Unit.Inch(0.30000001192092896));
            this.textBox41.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox41.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox41.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox41.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox41.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox41.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox41.Value = "=DiscountAmount";
            // 
            // textBox12
            // 
            this.textBox12.Format = "{0:N2}";
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.2025206089019775), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox12.Name = "textBox8";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.39992094039916992), Telerik.Reporting.Drawing.Unit.Inch(0.30000001192092896));
            this.textBox12.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox12.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox12.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.Value = "=ChargeQuantity";
            // 
            // textBox31
            // 
            this.textBox31.Format = "{0:N2}";
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.4599990844726562), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox31.Name = "textBox34";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97000002861022949), Telerik.Reporting.Drawing.Unit.Inch(0.30000001192092896));
            this.textBox31.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox31.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox31.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox31.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox31.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox31.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox31.Value = "=PatientAmount";
            // 
            // textBox14
            // 
            this.textBox14.Format = "{0:N2}";
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.4300780296325684), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97000002861022949), Telerik.Reporting.Drawing.Unit.Inch(0.30000001192092896));
            this.textBox14.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox14.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox14.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox14.Value = "=GuarantorAmount";
            // 
            // textBox38
            // 
            this.textBox38.Format = "{0:N2}";
            this.textBox38.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.6025207042694092), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox38.Name = "textBox38";
            this.textBox38.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97000002861022949), Telerik.Reporting.Drawing.Unit.Inch(0.30000001192092896));
            this.textBox38.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox38.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox38.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox38.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox38.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox38.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox38.Value = "=Total";
            // 
            // textBox55
            // 
            this.textBox55.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.60416668653488159));
            this.textBox55.Name = "textBox55";
            this.textBox55.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2999210357666016), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox55.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox55.Value = "Stamp";
            // 
            // textBox56
            // 
            this.textBox56.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3020833730697632), Telerik.Reporting.Drawing.Unit.Inch(0.60416668653488159));
            this.textBox56.Name = "textBox56";
            this.textBox56.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox56.Value = ":";
            // 
            // textBox57
            // 
            this.textBox57.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.40625));
            this.textBox57.Name = "textBox57";
            this.textBox57.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2999210357666016), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox57.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox57.Value = "Service";
            // 
            // textBox58
            // 
            this.textBox58.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3020833730697632), Telerik.Reporting.Drawing.Unit.Inch(0.40625));
            this.textBox58.Name = "textBox58";
            this.textBox58.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox58.Value = ":";
            // 
            // textBox59
            // 
            this.textBox59.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.1979166716337204));
            this.textBox59.Name = "textBox59";
            this.textBox59.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2999210357666016), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox59.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox59.Value = "Administration";
            // 
            // textBox60
            // 
            this.textBox60.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3020833730697632), Telerik.Reporting.Drawing.Unit.Inch(0.1979166716337204));
            this.textBox60.Name = "textBox60";
            this.textBox60.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox60.Value = ":";
            // 
            // textBox61
            // 
            this.textBox61.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3020833730697632), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox61.Name = "textBox61";
            this.textBox61.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox61.Value = ":";
            // 
            // textBox62
            // 
            this.textBox62.Name = "textBox62";
            this.textBox62.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2999210357666016), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox62.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox62.Value = "Total";
            // 
            // textBox63
            // 
            this.textBox63.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.40625), Telerik.Reporting.Drawing.Unit.Inch(0.91666668653488159));
            this.textBox63.Name = "textBox63";
            this.textBox63.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2951974868774414), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox63.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox63.Value = "textBox21";
            // 
            // shape8
            // 
            this.shape8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.40625), Telerik.Reporting.Drawing.Unit.Inch(0.80208331346511841));
            this.shape8.Name = "shape8";
            this.shape8.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2897999286651611), Telerik.Reporting.Drawing.Unit.Cm(0.28148153424263));
            // 
            // textBox64
            // 
            this.textBox64.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.40625), Telerik.Reporting.Drawing.Unit.Inch(0.60416668653488159));
            this.textBox64.Name = "textBox64";
            this.textBox64.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2951974868774414), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox64.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox64.Value = "textBox21";
            // 
            // textBox65
            // 
            this.textBox65.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.40625), Telerik.Reporting.Drawing.Unit.Inch(0.40625));
            this.textBox65.Name = "textBox65";
            this.textBox65.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2951974868774414), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox65.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox65.Value = "textBox21";
            // 
            // textBox66
            // 
            this.textBox66.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.40625), Telerik.Reporting.Drawing.Unit.Inch(0.1979166716337204));
            this.textBox66.Name = "textBox66";
            this.textBox66.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2951974868774414), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox66.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox66.Value = "textBox21";
            // 
            // textBox67
            // 
            this.textBox67.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8958333730697632), Telerik.Reporting.Drawing.Unit.Inch(1.28125));
            this.textBox67.Name = "textBox67";
            this.textBox67.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2999210357666016), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox67.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox67.Value = "Stamp";
            // 
            // textBox68
            // 
            this.textBox68.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.1979167461395264), Telerik.Reporting.Drawing.Unit.Inch(1.28125));
            this.textBox68.Name = "textBox68";
            this.textBox68.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox68.Value = ":";
            // 
            // textBox69
            // 
            this.textBox69.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8958333730697632), Telerik.Reporting.Drawing.Unit.Inch(1.0833333730697632));
            this.textBox69.Name = "textBox69";
            this.textBox69.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2999210357666016), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox69.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox69.Value = "Service";
            // 
            // textBox70
            // 
            this.textBox70.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.1979167461395264), Telerik.Reporting.Drawing.Unit.Inch(1.0833333730697632));
            this.textBox70.Name = "textBox70";
            this.textBox70.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox70.Value = ":";
            // 
            // textBox71
            // 
            this.textBox71.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8958333730697632), Telerik.Reporting.Drawing.Unit.Inch(0.875));
            this.textBox71.Name = "textBox71";
            this.textBox71.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2999210357666016), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox71.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox71.Value = "Administration";
            // 
            // textBox72
            // 
            this.textBox72.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.1979167461395264), Telerik.Reporting.Drawing.Unit.Inch(0.875));
            this.textBox72.Name = "textBox72";
            this.textBox72.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox72.Value = ":";
            // 
            // textBox73
            // 
            this.textBox73.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.1979167461395264), Telerik.Reporting.Drawing.Unit.Inch(0.67708331346511841));
            this.textBox73.Name = "textBox73";
            this.textBox73.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox73.Value = ":";
            // 
            // textBox74
            // 
            this.textBox74.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8958333730697632), Telerik.Reporting.Drawing.Unit.Inch(0.67708331346511841));
            this.textBox74.Name = "textBox74";
            this.textBox74.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2999210357666016), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox74.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox74.Value = "Total";
            // 
            // textBox75
            // 
            this.textBox75.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.3020832538604736), Telerik.Reporting.Drawing.Unit.Inch(1.59375));
            this.textBox75.Name = "textBox75";
            this.textBox75.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2951974868774414), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox75.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox75.Value = "textBox21";
            // 
            // shape9
            // 
            this.shape9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.3020832538604736), Telerik.Reporting.Drawing.Unit.Inch(1.4791666269302368));
            this.shape9.Name = "shape9";
            this.shape9.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2897999286651611), Telerik.Reporting.Drawing.Unit.Cm(0.28148153424263));
            // 
            // textBox76
            // 
            this.textBox76.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.3020832538604736), Telerik.Reporting.Drawing.Unit.Inch(1.28125));
            this.textBox76.Name = "textBox76";
            this.textBox76.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2951974868774414), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox76.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox76.Value = "textBox21";
            // 
            // textBox77
            // 
            this.textBox77.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.3020832538604736), Telerik.Reporting.Drawing.Unit.Inch(1.0833333730697632));
            this.textBox77.Name = "textBox77";
            this.textBox77.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2951974868774414), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox77.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox77.Value = "textBox21";
            // 
            // textBox78
            // 
            this.textBox78.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.3020832538604736), Telerik.Reporting.Drawing.Unit.Inch(0.875));
            this.textBox78.Name = "textBox78";
            this.textBox78.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2951974868774414), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox78.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox78.Value = "textBox21";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.39999961853027344);
            this.pageFooterSection1.Name = "pageFooterSection1";
            this.pageFooterSection1.Style.Visible = true;
            // 
            // pageHeader
            // 
            formattingRule1.Filters.AddRange(new Telerik.Reporting.Filter[] {
            new Telerik.Reporting.Filter("= PageNumber", Telerik.Reporting.FilterOperator.GreaterThan, "1")});
            formattingRule1.Style.Visible = false;
            this.pageHeader.ConditionalFormatting.AddRange(new Telerik.Reporting.Drawing.FormattingRule[] {
            formattingRule1});
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(1.7003159523010254);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox28,
            this.textBox9,
            this.textBox15,
            this.textBox16,
            this.textBox18,
            this.textBox19,
            this.textBox20,
            this.textBox21,
            this.textBox25,
            this.textBox26,
            this.textBox29,
            this.textBox94,
            this.textBox95,
            this.textBox96,
            this.textBox4,
            this.txtTerimaOleh,
            this.textBox32,
            this.textBox35,
            this.txtTotalAmountInWords,
            this.textBox40,
            this.textBox44,
            this.txtPaymentNo,
            this.textBox10,
            this.textBox22,
            this.textBox3,
            this.textBox2,
            this.textBox6});
            this.pageHeader.Name = "pageHeader";
            // 
            // textBox28
            // 
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.5725998878479), Telerik.Reporting.Drawing.Unit.Inch(0.90000009536743164));
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2274004220962524), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox28.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox28.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox28.Value = "No Registrasi";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.5725998878479), Telerik.Reporting.Drawing.Unit.Inch(0.68737930059432983));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2274004220962524), Telerik.Reporting.Drawing.Unit.Inch(0.21266047656536102));
            this.textBox9.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox9.Value = "Tgl Registrasi";
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0029919941443949938), Telerik.Reporting.Drawing.Unit.Inch(1.5));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2000789642333984), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox15.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox15.Value = "Penjamin";
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2031106948852539), Telerik.Reporting.Drawing.Unit.Inch(1.5));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox16.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox16.Value = ":";
            // 
            // textBox18
            // 
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2030315399169922), Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.19964472949504852));
            this.textBox18.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox18.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox18.Value = ":";
            // 
            // textBox19
            // 
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.8000006675720215), Telerik.Reporting.Drawing.Unit.Inch(0.90000009536743164));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox19.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox19.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox19.Value = ":";
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.8837074397597462E-05), Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2000002861022949), Telerik.Reporting.Drawing.Unit.Inch(0.19964472949504852));
            this.textBox20.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox20.Value = "Ruangan";
            // 
            // textBox21
            // 
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.9000792503356934), Telerik.Reporting.Drawing.Unit.Inch(0.90000009536743164));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4507156610488892), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox21.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox21.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox21.Value = "=RegistrationNo";
            // 
            // textBox25
            // 
            this.textBox25.Format = "{0:dd/MM/yyyy}";
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.9000792503356934), Telerik.Reporting.Drawing.Unit.Inch(0.68737930059432983));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4507156610488892), Telerik.Reporting.Drawing.Unit.Inch(0.21266023814678192));
            this.textBox25.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox25.Value = "=RegistrationDate";
            // 
            // textBox26
            // 
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.30303156375885), Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579));
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.269489049911499), Telerik.Reporting.Drawing.Unit.Inch(0.19964472949504852));
            this.textBox26.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox26.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox26.Value = "=RoomName + \" / \" + ClassName + \" / \" + BedID";
            // 
            // textBox29
            // 
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.30303156375885), Telerik.Reporting.Drawing.Unit.Inch(1.5));
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.0725188255310059), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox29.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox29.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox29.Value = "=GuarantorName";
            // 
            // textBox94
            // 
            this.textBox94.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.5725998878479), Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579));
            this.textBox94.Name = "textBox94";
            this.textBox94.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2274004220962524), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox94.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox94.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox94.Value = "No Rekam Medik";
            // 
            // textBox95
            // 
            this.textBox95.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.8000788688659668), Telerik.Reporting.Drawing.Unit.Inch(0.68737930059432983));
            this.textBox95.Name = "textBox95";
            this.textBox95.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.21266031265258789));
            this.textBox95.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox95.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox95.Value = ":";
            // 
            // textBox96
            // 
            this.textBox96.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.9000792503356934), Telerik.Reporting.Drawing.Unit.Inch(1.100000262260437));
            this.textBox96.Name = "textBox96";
            this.textBox96.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4508339166641235), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox96.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox96.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox96.Value = "=MedicalNo";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.7999997138977051), Telerik.Reporting.Drawing.Unit.Inch(1.1000003814697266));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox4.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox4.Value = ":";
            // 
            // txtTerimaOleh
            // 
            this.txtTerimaOleh.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3000396490097046), Telerik.Reporting.Drawing.Unit.Inch(0.69996070861816406));
            this.txtTerimaOleh.Name = "txtTerimaOleh";
            this.txtTerimaOleh.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.2724809646606445), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.txtTerimaOleh.Style.Font.Name = "Microsoft Sans Serif";
            this.txtTerimaOleh.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.txtTerimaOleh.Value = "";
            // 
            // textBox32
            // 
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.69996070861816406));
            this.textBox32.Name = "textBox3";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1999212503433228), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox32.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox32.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox32.Value = "Sudah Terima Dari";
            // 
            // textBox35
            // 
            this.textBox35.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2000001668930054), Telerik.Reporting.Drawing.Unit.Inch(0.69996070861816406));
            this.textBox35.Name = "textBox2";
            this.textBox35.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox35.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox35.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox35.Value = ":";
            // 
            // txtTotalAmountInWords
            // 
            this.txtTotalAmountInWords.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.30303156375885), Telerik.Reporting.Drawing.Unit.Inch(1.2999210357666016));
            this.txtTotalAmountInWords.Name = "txtTotalAmountInWords";
            this.txtTotalAmountInWords.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.4969687461853027), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtTotalAmountInWords.Style.Font.Name = "Microsoft Sans Serif";
            this.txtTotalAmountInWords.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.txtTotalAmountInWords.Value = "=TotalAmountInWords";
            // 
            // textBox40
            // 
            this.textBox40.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2000396251678467), Telerik.Reporting.Drawing.Unit.Inch(1.2999210357666016));
            this.textBox40.Name = "textBox13";
            this.textBox40.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox40.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox40.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox40.Value = ":";
            // 
            // textBox44
            // 
            this.textBox44.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(1.2999210357666016));
            this.textBox44.Name = "textBox5";
            this.textBox44.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2000001668930054), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox44.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox44.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox44.Value = "Uang Sebesar";
            // 
            // txtPaymentNo
            // 
            this.txtPaymentNo.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.6725635528564453), Telerik.Reporting.Drawing.Unit.Inch(0.30306228995323181));
            this.txtPaymentNo.Name = "txtPaymentNo";
            this.txtPaymentNo.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.200000524520874), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtPaymentNo.Style.Font.Name = "Tahoma";
            this.txtPaymentNo.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.txtPaymentNo.Value = "=PaymentNo";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.6725635528564453), Telerik.Reporting.Drawing.Unit.Inch(0.013400156982243061));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.297917366027832), Telerik.Reporting.Drawing.Unit.Inch(0.28958341479301453));
            this.textBox10.Style.Font.Bold = true;
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14);
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox10.Value = "Bukti Pembayaran";
            // 
            // textBox22
            // 
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.30303156375885), Telerik.Reporting.Drawing.Unit.Inch(0.89992111921310425));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.269489049911499), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox22.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox22.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox22.Value = "=PatientName";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.89992111921310425));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2000789642333984), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox3.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox3.Value = "Nama Pasien";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2030315399169922), Telerik.Reporting.Drawing.Unit.Inch(0.89992111921310425));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox2.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox2.Value = ":";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.7189064025878906), Telerik.Reporting.Drawing.Unit.Inch(1.5177861452102661));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.6811712384223938), Telerik.Reporting.Drawing.Unit.Inch(0.1825297623872757));
            this.textBox6.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox6.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox6.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox6.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "= \"Page \" + PageNumber + \"/\" + PageCount";
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(1.4777294397354126);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox50,
            this.textBox49,
            this.textBox48,
            this.textBox47,
            this.txtTotalG,
            this.shape7,
            this.txtAdministrationAmount,
            this.textBox45,
            this.TxtCityRS,
            this.txtUserName,
            this.textBox13,
            this.textBox17,
            this.textBox23,
            this.txtPaymentMethod,
            this.textBox11,
            this.textBox27,
            this.textBox36,
            this.textBox39,
            this.txtDownpayment,
            this.txtKembalian,
            this.textBox81,
            this.textBox82,
            this.textBox5,
            this.shape1,
            this.textBox7,
            this.txtTotalP});
            this.reportFooterSection1.Name = "reportFooterSection1";
            this.reportFooterSection1.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            // 
            // textBox50
            // 
            this.textBox50.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.2000002861022949), Telerik.Reporting.Drawing.Unit.Inch(0.25231996178627014));
            this.textBox50.Name = "textBox50";
            this.textBox50.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0640230178833008), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox50.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox50.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox50.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox50.Style.Visible = true;
            this.textBox50.Value = "Administrasi (+) ";
            // 
            // textBox49
            // 
            this.textBox49.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.264103889465332), Telerik.Reporting.Drawing.Unit.Inch(0.25231996178627014));
            this.textBox49.Name = "textBox49";
            this.textBox49.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox49.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox49.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox49.Style.Visible = true;
            this.textBox49.Value = ":";
            // 
            // textBox48
            // 
            this.textBox48.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.2643771171569824), Telerik.Reporting.Drawing.Unit.Inch(0.0522410087287426));
            this.textBox48.Name = "textBox48";
            this.textBox48.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox48.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox48.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox48.Style.Visible = true;
            this.textBox48.Value = ":";
            // 
            // textBox47
            // 
            this.textBox47.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.2002739906311035), Telerik.Reporting.Drawing.Unit.Inch(0.0522410087287426));
            this.textBox47.Name = "textBox47";
            this.textBox47.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0640230178833008), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox47.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox47.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox47.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox47.Style.Visible = true;
            this.textBox47.Value = "Total ";
            // 
            // txtTotalG
            // 
            this.txtTotalG.Format = "{0:N2}";
            this.txtTotalG.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.4000000953674316), Telerik.Reporting.Drawing.Unit.Inch(0.55216282606124878));
            this.txtTotalG.Name = "txtTotal";
            this.txtTotalG.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97712278366088867), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.txtTotalG.Style.Font.Name = "Microsoft Sans Serif";
            this.txtTotalG.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.txtTotalG.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtTotalG.Style.Visible = true;
            // 
            // shape7
            // 
            this.shape7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.6724362373352051), Telerik.Reporting.Drawing.Unit.Inch(0.50000029802322388));
            this.shape7.Name = "shape7";
            this.shape7.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.7047662734985352), Telerik.Reporting.Drawing.Unit.Cm(0.13229165971279144));
            this.shape7.Style.Font.Name = "Microsoft Sans Serif";
            this.shape7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.shape7.Style.Visible = true;
            // 
            // txtAdministrationAmount
            // 
            this.txtAdministrationAmount.Format = "{0:N2}";
            this.txtAdministrationAmount.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.399726390838623), Telerik.Reporting.Drawing.Unit.Inch(0.25231996178627014));
            this.txtAdministrationAmount.Name = "txtAdministrationAmount";
            this.txtAdministrationAmount.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97724038362503052), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.txtAdministrationAmount.Style.Font.Name = "Microsoft Sans Serif";
            this.txtAdministrationAmount.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.txtAdministrationAmount.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtAdministrationAmount.Style.Visible = true;
            // 
            // textBox45
            // 
            this.textBox45.Format = "{0:N2}";
            this.textBox45.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.4000000953674316), Telerik.Reporting.Drawing.Unit.Inch(0.0522410087287426));
            this.textBox45.Name = "textBox45";
            this.textBox45.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97724038362503052), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox45.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox45.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox45.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox45.Style.Visible = true;
            this.textBox45.Value = "=SUM(GuarantorAmount)";
            // 
            // TxtCityRS
            // 
            this.TxtCityRS.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.48921966552734375));
            this.TxtCityRS.Name = "TxtCityRS";
            this.TxtCityRS.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.TxtCityRS.Style.Font.Name = "Microsoft Sans Serif";
            this.TxtCityRS.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.TxtCityRS.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0029919941443949938), Telerik.Reporting.Drawing.Unit.Inch(1.2777293920516968));
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.8748822212219238), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtUserName.Style.Font.Name = "Microsoft Sans Serif";
            this.txtUserName.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.txtUserName.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtUserName.Value = "User Name";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.68914061784744263));
            this.textBox13.Name = "textBox12";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.8748822212219238), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox13.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox13.Value = "Kasir";
            // 
            // textBox17
            // 
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.5002744197845459), Telerik.Reporting.Drawing.Unit.Inch(0.55216246843338013));
            this.textBox17.Name = "textBox47";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7640234231948853), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox17.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox17.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox17.Style.Visible = true;
            this.textBox17.Value = "Total Tagihan ";
            // 
            // textBox23
            // 
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.2643771171569824), Telerik.Reporting.Drawing.Unit.Inch(0.55216246843338013));
            this.textBox23.Name = "textBox48";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox23.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox23.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox23.Style.Visible = true;
            this.textBox23.Value = ":";
            // 
            // txtPaymentMethod
            // 
            this.txtPaymentMethod.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.25231996178627014));
            this.txtPaymentMethod.Name = "txtPaymentMethod";
            this.txtPaymentMethod.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.5000002384185791), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtPaymentMethod.Style.Font.Name = "Microsoft Sans Serif";
            this.txtPaymentMethod.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.txtPaymentMethod.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtPaymentMethod.Value = "";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3031107187271118), Telerik.Reporting.Drawing.Unit.Inch(0.0522410087287426));
            this.textBox11.Name = "textBox48";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox11.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox11.Style.Visible = true;
            this.textBox11.Value = ":";
            // 
            // textBox27
            // 
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0029919941443949938), Telerik.Reporting.Drawing.Unit.Inch(0.0522410087287426));
            this.textBox27.Name = "textBox47";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3000398874282837), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox27.Style.Font.Bold = true;
            this.textBox27.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox27.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox27.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox27.Style.Visible = true;
            this.textBox27.Value = "Cara Pembayaran";
            // 
            // textBox36
            // 
            this.textBox36.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.2643771171569824), Telerik.Reporting.Drawing.Unit.Inch(0.75224143266677856));
            this.textBox36.Name = "textBox48";
            this.textBox36.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox36.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox36.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox36.Style.Visible = false;
            this.textBox36.Value = ":";
            // 
            // textBox39
            // 
            this.textBox39.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.5002737045288086), Telerik.Reporting.Drawing.Unit.Inch(0.75224143266677856));
            this.textBox39.Name = "textBox47";
            this.textBox39.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7640234231948853), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox39.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox39.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox39.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox39.Style.Visible = false;
            this.textBox39.Value = "Uang Muka ";
            // 
            // txtDownpayment
            // 
            this.txtDownpayment.Format = "{0:N2}";
            this.txtDownpayment.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.4000000953674316), Telerik.Reporting.Drawing.Unit.Inch(0.75224143266677856));
            this.txtDownpayment.Name = "txtDownpayment";
            this.txtDownpayment.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97712278366088867), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.txtDownpayment.Style.Font.Name = "Microsoft Sans Serif";
            this.txtDownpayment.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.txtDownpayment.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtDownpayment.Style.Visible = false;
            // 
            // txtKembalian
            // 
            this.txtKembalian.Format = "{0:N2}";
            this.txtKembalian.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.399843692779541), Telerik.Reporting.Drawing.Unit.Inch(0.952320396900177));
            this.txtKembalian.Name = "txtKembalian";
            this.txtKembalian.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97712278366088867), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.txtKembalian.Style.Font.Name = "Microsoft Sans Serif";
            this.txtKembalian.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.txtKembalian.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtKembalian.Style.Visible = false;
            // 
            // textBox81
            // 
            this.textBox81.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.5002737045288086), Telerik.Reporting.Drawing.Unit.Inch(0.952320396900177));
            this.textBox81.Name = "textBox47";
            this.textBox81.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7640234231948853), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox81.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox81.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox81.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox81.Style.Visible = false;
            this.textBox81.Value = "Total Uang Kembalian(-)";
            // 
            // textBox82
            // 
            this.textBox82.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.2642984390258789), Telerik.Reporting.Drawing.Unit.Inch(0.952320396900177));
            this.textBox82.Name = "textBox48";
            this.textBox82.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox82.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox82.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox82.Style.Visible = false;
            this.textBox82.Value = ":";
            // 
            // textBox5
            // 
            this.textBox5.Format = ",{0:dd-MM-yyyy}";
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1), Telerik.Reporting.Drawing.Unit.Inch(0.48921966552734375));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.87488222122192383), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox5.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.Value = "= Now()";
            // 
            // shape1
            // 
            this.shape1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332));
            this.shape1.Name = "shape1";
            this.shape1.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(18.796197891235352), Telerik.Reporting.Drawing.Unit.Cm(0.13229165971279144));
            // 
            // textBox7
            // 
            this.textBox7.Format = "{0:N2}";
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.4224071502685547), Telerik.Reporting.Drawing.Unit.Inch(0.0522410087287426));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97724038362503052), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox7.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox7.Style.Visible = true;
            this.textBox7.Value = "=SUM(PatientAmount)";
            // 
            // txtTotalP
            // 
            this.txtTotalP.Format = "{0:N2}";
            this.txtTotalP.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.4224071502685547), Telerik.Reporting.Drawing.Unit.Inch(0.55216246843338013));
            this.txtTotalP.Name = "txtTotalP";
            this.txtTotalP.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97712278366088867), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.txtTotalP.Style.Font.Name = "Microsoft Sans Serif";
            this.txtTotalP.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.txtTotalP.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtTotalP.Style.Visible = true;
            // 
            // reportHeaderSection1
            // 
            this.reportHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.29999998211860657);
            this.reportHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox34,
            this.textBox24,
            this.textBox8,
            this.textBox30,
            this.textBox1,
            this.textBox33});
            this.reportHeaderSection1.Name = "reportHeaderSection1";
            this.reportHeaderSection1.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.reportHeaderSection1.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            // 
            // textBox34
            // 
            this.textBox34.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.5725994110107422), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox34.Name = "textBox34";
            this.textBox34.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.88480573892593384), Telerik.Reporting.Drawing.Unit.Inch(0.30000001192092896));
            this.textBox34.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox34.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox34.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox34.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox34.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox34.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox34.Value = "Diskon";
            // 
            // textBox24
            // 
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0029919941443949938), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox24.Name = "textBox35";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.1994497776031494), Telerik.Reporting.Drawing.Unit.Inch(0.29948696494102478));
            this.textBox24.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox24.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox24.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox24.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox24.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox24.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox24.Value = "Detail Kegiatan";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.2025210857391357), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.39992094039916992), Telerik.Reporting.Drawing.Unit.Inch(0.29948696494102478));
            this.textBox8.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox8.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox8.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Value = "Qty";
            // 
            // textBox30
            // 
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.4574837684631348), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox30.Name = "textBox34";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.9725152850151062), Telerik.Reporting.Drawing.Unit.Inch(0.29948696494102478));
            this.textBox30.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox30.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox30.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox30.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox30.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox30.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox30.Value = "Pasien";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.4300780296325684), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97000002861022949), Telerik.Reporting.Drawing.Unit.Inch(0.30000001192092896));
            this.textBox1.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox1.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox1.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "Jaminan";
            // 
            // textBox33
            // 
            this.textBox33.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.6025207042694092), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97000002861022949), Telerik.Reporting.Drawing.Unit.Inch(0.30000001192092896));
            this.textBox33.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox33.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox33.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox33.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox33.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox33.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox33.Value = "Total";
            // 
            // PaymentReceiveDetail
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail,
            this.pageFooterSection1,
            this.reportFooterSection1,
            this.reportHeaderSection1});
            this.Name = "PaymentReceiveDetail";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Inch(0.75);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Inch(0.550000011920929);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Inch(0.550000011920929);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(0.5);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Style.Font.Name = "Microsoft Sans Serif";
            this.UnitOfMeasure = Telerik.Reporting.Drawing.UnitType.Cm;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.4000778198242188);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox37;
        private Telerik.Reporting.TextBox textBox41;
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
        private PageFooterSection pageFooterSection1;
        private PageHeaderSection pageHeader;
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
        private Telerik.Reporting.TextBox textBox94;
        private Telerik.Reporting.TextBox textBox95;
        private Telerik.Reporting.TextBox textBox96;
        private Telerik.Reporting.TextBox textBox4;
        private ReportFooterSection reportFooterSection1;
        private ReportHeaderSection reportHeaderSection1;
        private Telerik.Reporting.TextBox textBox34;
        private Telerik.Reporting.TextBox textBox50;
        private Telerik.Reporting.TextBox textBox49;
        private Telerik.Reporting.TextBox textBox48;
        private Telerik.Reporting.TextBox textBox47;
        private Telerik.Reporting.TextBox txtTotalG;
        private Shape shape7;
        private Telerik.Reporting.TextBox txtAdministrationAmount;
        private Telerik.Reporting.TextBox textBox45;
        private Telerik.Reporting.TextBox TxtCityRS;
        private Telerik.Reporting.TextBox txtUserName;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox txtPaymentMethod;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox txtTerimaOleh;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.TextBox textBox35;
        private Telerik.Reporting.TextBox textBox36;
        private Telerik.Reporting.TextBox textBox39;
        private Telerik.Reporting.TextBox txtDownpayment;
        private Telerik.Reporting.TextBox txtTotalAmountInWords;
        private Telerik.Reporting.TextBox textBox40;
        private Telerik.Reporting.TextBox textBox44;
        private Telerik.Reporting.TextBox txtKembalian;
        private Telerik.Reporting.TextBox textBox81;
        private Telerik.Reporting.TextBox textBox82;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox textBox30;
        private Shape shape1;
        private Telerik.Reporting.TextBox txtPaymentNo;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox txtTotalP;
        private Telerik.Reporting.TextBox textBox38;
        private Telerik.Reporting.TextBox textBox33;
    }
}