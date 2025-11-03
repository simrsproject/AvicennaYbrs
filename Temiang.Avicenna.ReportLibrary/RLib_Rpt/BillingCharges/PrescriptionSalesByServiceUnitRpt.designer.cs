namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class PrescriptionSalesByServiceUnitRpt
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
            this.textBox43 = new Telerik.Reporting.TextBox();
            this.textBox88 = new Telerik.Reporting.TextBox();
            this.textBox91 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
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
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox49 = new Telerik.Reporting.TextBox();
            this.txtPeriode = new Telerik.Reporting.TextBox();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.shape1 = new Telerik.Reporting.Shape();
            this.textBox53 = new Telerik.Reporting.TextBox();
            this.reportHeaderSection1 = new Telerik.Reporting.ReportHeaderSection();
            this.TxtCityRS = new Telerik.Reporting.TextBox();
            this.TxtTelp = new Telerik.Reporting.TextBox();
            this.TxtNameRS = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.group1 = new Telerik.Reporting.Group();
            this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();
            this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.group3 = new Telerik.Reporting.Group();
            this.groupFooterSection3 = new Telerik.Reporting.GroupFooterSection();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.shape2 = new Telerik.Reporting.Shape();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.groupHeaderSection3 = new Telerik.Reporting.GroupHeaderSection();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.group4 = new Telerik.Reporting.Group();
            this.groupFooterSection4 = new Telerik.Reporting.GroupFooterSection();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.groupHeaderSection4 = new Telerik.Reporting.GroupHeaderSection();
            this.textBox25 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20003955066204071);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox43,
            this.textBox88,
            this.textBox91,
            this.textBox17,
            this.textBox29,
            this.textBox31});
            this.detail.Name = "detail";
            this.detail.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.detail.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.detail.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.detail.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.detail.Style.Visible = true;
            // 
            // textBox43
            // 
            this.textBox43.Format = "{0:N0}";
            this.textBox43.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.7139315605163574), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox43.Name = "textBox9";
            this.textBox43.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0582345724105835), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox43.Style.Font.Name = "Tahoma";
            this.textBox43.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox43.Value = "=TotalGA";
            // 
            // textBox88
            // 
            this.textBox88.Format = "{0:N0}";
            this.textBox88.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.7390289306640625), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox88.Name = "textBox26";
            this.textBox88.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97482365369796753), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox88.Style.Font.Name = "Tahoma";
            this.textBox88.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox88.Value = "=TotalPA";
            // 
            // textBox91
            // 
            this.textBox91.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0114504499360919), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox91.Name = "textBox9";
            this.textBox91.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.4817638397216797), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox91.Style.Font.Name = "Tahoma";
            this.textBox91.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(3);
            this.textBox91.Value = "=ItemName";
            // 
            // textBox17
            // 
            this.textBox17.Format = "{0:N0}";
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.7084870338439941), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0304628610610962), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox17.Style.Font.Name = "Tahoma";
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox17.Value = "=TotalPA+TotalGA";
            // 
            // textBox29
            // 
            this.textBox29.Format = "{0:N0}";
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.4932930469512939), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.43071427941322327), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox29.Style.Font.Name = "Tahoma";
            this.textBox29.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox29.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox29.Value = "=ResultQty";
            // 
            // textBox31
            // 
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.924086332321167), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.78432208299636841), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox31.Style.Font.Name = "Tahoma";
            this.textBox31.Value = "=SRItemUnit";
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
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.21089808642864227);
            this.pageFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox13,
            this.textBox53});
            this.pageFooterSection1.Name = "pageFooterSection1";
            this.pageFooterSection1.Style.Visible = true;
            // 
            // textBox13
            // 
            this.textBox13.Format = "Avicenna HIS, Print Date : {0:dd-MM-yyyy HH:mm}";
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.54506254196167), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.2746651172637939), Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672));
            this.textBox13.Style.Font.Italic = true;
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox13.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox13.Value = "= PageNumber +\' / \'+ PageCount";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.77283763885498047));
            this.textBox2.Name = "TxtNameRS";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.7721657752990723), Telerik.Reporting.Drawing.Unit.Inch(0.2199999988079071));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Name = "Tahoma";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox2.Value = "Laporan Penjualan Resep Pasien (Detail)";
            // 
            // textBox49
            // 
            this.textBox49.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.084027804434299469));
            this.textBox49.Name = "textBox29";
            this.textBox49.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.7721657752990723), Telerik.Reporting.Drawing.Unit.Inch(0.20176728069782257));
            this.textBox49.Style.Font.Bold = true;
            this.textBox49.Style.Font.Name = "Tahoma";
            this.textBox49.Style.Font.Underline = false;
            this.textBox49.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox49.Value = "=GuarantorName";
            // 
            // txtPeriode
            // 
            this.txtPeriode.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(1.2298847436904907));
            this.txtPeriode.Name = "txtPeriode";
            this.txtPeriode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.7721657752990723), Telerik.Reporting.Drawing.Unit.Inch(0.20625019073486328));
            this.txtPeriode.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtPeriode.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.txtPeriode.Style.Font.Bold = false;
            this.txtPeriode.Style.Font.Name = "Tahoma";
            this.txtPeriode.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.txtPeriode.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtPeriode.Value = "=Period";
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.14791679382324219);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.shape1});
            this.reportFooterSection1.KeepTogether = false;
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // shape1
            // 
            this.shape1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9378803194267675E-05), Telerik.Reporting.Drawing.Unit.Inch(0.0416666679084301));
            this.shape1.Name = "shape1";
            this.shape1.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.8196883201599121), Telerik.Reporting.Drawing.Unit.Inch(0.05000000074505806));
            // 
            // textBox53
            // 
            this.textBox53.Format = "Avicenna HIS, Print Date : {0:dd-MM-yyyy HH:mm}";
            this.textBox53.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0114504499360919), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox53.Name = "textBox48";
            this.textBox53.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.3612403869628906), Telerik.Reporting.Drawing.Unit.Inch(0.19996070861816406));
            this.textBox53.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox53.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox53.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox53.Style.Font.Italic = true;
            this.textBox53.Style.Font.Name = "Tahoma";
            this.textBox53.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox53.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612);
            this.textBox53.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Cm(0);
            this.textBox53.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox53.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox53.Value = "=Now()";
            // 
            // reportHeaderSection1
            // 
            this.reportHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(1.5093511343002319);
            this.reportHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox2,
            this.txtPeriode,
            this.TxtCityRS,
            this.TxtTelp,
            this.TxtNameRS,
            this.textBox3});
            this.reportHeaderSection1.KeepTogether = false;
            this.reportHeaderSection1.Name = "reportHeaderSection1";
            // 
            // TxtCityRS
            // 
            this.TxtCityRS.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.25605905055999756));
            this.TxtCityRS.Name = "TxtCityRS";
            this.TxtCityRS.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.7721657752990723), Telerik.Reporting.Drawing.Unit.Inch(0.23400585353374481));
            this.TxtCityRS.Style.Font.Name = "Microsoft Sans Serif";
            this.TxtCityRS.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.TxtCityRS.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.TxtCityRS.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtCityRS.Value = "";
            // 
            // TxtTelp
            // 
            this.TxtTelp.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.4901435375213623));
            this.TxtTelp.Name = "TxtTelp";
            this.TxtTelp.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.7721657752990723), Telerik.Reporting.Drawing.Unit.Inch(0.23125012218952179));
            this.TxtTelp.Style.Font.Name = "Microsoft Sans Serif";
            this.TxtTelp.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.TxtTelp.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.TxtTelp.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtTelp.Value = "";
            // 
            // TxtNameRS
            // 
            this.TxtNameRS.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.01647573709487915));
            this.TxtNameRS.Name = "TxtNameRS";
            this.TxtNameRS.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.7721657752990723), Telerik.Reporting.Drawing.Unit.Inch(0.23950465023517609));
            this.TxtNameRS.Style.Font.Name = "Microsoft Sans Serif";
            this.TxtNameRS.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.TxtNameRS.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.TxtNameRS.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtNameRS.Value = "";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0114504499360919), Telerik.Reporting.Drawing.Unit.Inch(0.99291640520095825));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.7607154846191406), Telerik.Reporting.Drawing.Unit.Inch(0.18757915496826172));
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Name = "Tahoma";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.Value = "=TypePasien";
            // 
            // group1
            // 
            this.group1.GroupFooter = this.groupFooterSection1;
            this.group1.GroupHeader = this.groupHeaderSection1;
            this.group1.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("=GuarantorName")});
            this.group1.Name = "group1";
            this.group1.Sortings.AddRange(new Telerik.Reporting.Sorting[] {
            new Telerik.Reporting.Sorting("GuarantorName", Telerik.Reporting.SortDirection.Asc),
            new Telerik.Reporting.Sorting("PatientName", Telerik.Reporting.SortDirection.Asc)});
            // 
            // groupFooterSection1
            // 
            this.groupFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699);
            this.groupFooterSection1.Name = "groupFooterSection1";
            // 
            // groupHeaderSection1
            // 
            this.groupHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.28579509258270264);
            this.groupHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox49});
            this.groupHeaderSection1.Name = "groupHeaderSection1";
            // 
            // textBox27
            // 
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.7139315605163574), Telerik.Reporting.Drawing.Unit.Inch(0.30424562096595764));
            this.textBox27.Name = "textBox9";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0582345724105835), Telerik.Reporting.Drawing.Unit.Inch(0.3111114501953125));
            this.textBox27.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox27.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox27.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox27.Style.Font.Name = "Tahoma";
            this.textBox27.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox27.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox27.Value = "Total Instansi";
            // 
            // textBox26
            // 
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.7390289306640625), Telerik.Reporting.Drawing.Unit.Inch(0.30424532294273376));
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97482365369796753), Telerik.Reporting.Drawing.Unit.Inch(0.3111114501953125));
            this.textBox26.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox26.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox26.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox26.Style.Font.Name = "Tahoma";
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox26.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox26.Value = "Total Pasien";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0114504499360919), Telerik.Reporting.Drawing.Unit.Inch(0.30424562096595764));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.9125568866729736), Telerik.Reporting.Drawing.Unit.Inch(0.3111114501953125));
            this.textBox4.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox4.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox4.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox4.Style.Font.Name = "Tahoma";
            this.textBox4.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(3);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.Value = "Nama Item";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.7084870338439941), Telerik.Reporting.Drawing.Unit.Inch(0.30424562096595764));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0304628610610962), Telerik.Reporting.Drawing.Unit.Inch(0.3111114501953125));
            this.textBox14.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox14.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox14.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox14.Style.Font.Name = "Tahoma";
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox14.Value = "Total Biaya";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9240858554840088), Telerik.Reporting.Drawing.Unit.Inch(0.3042454719543457));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.78432244062423706), Telerik.Reporting.Drawing.Unit.Inch(0.3111114501953125));
            this.textBox10.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox10.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox10.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox10.Style.Font.Name = "Tahoma";
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox10.Value = "Qty";
            // 
            // group3
            // 
            this.group3.GroupFooter = this.groupFooterSection3;
            this.group3.GroupHeader = this.groupHeaderSection3;
            this.group3.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("=MedicalNo")});
            this.group3.Name = "group3";
            this.group3.Sortings.AddRange(new Telerik.Reporting.Sorting[] {
            new Telerik.Reporting.Sorting("PatientName", Telerik.Reporting.SortDirection.Asc)});
            // 
            // groupFooterSection3
            // 
            this.groupFooterSection3.Height = Telerik.Reporting.Drawing.Unit.Inch(0.35791650414466858);
            this.groupFooterSection3.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox11,
            this.textBox8,
            this.textBox12,
            this.shape2,
            this.textBox24});
            this.groupFooterSection3.Name = "groupFooterSection3";
            this.groupFooterSection3.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.groupFooterSection3.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            // 
            // textBox11
            // 
            this.textBox11.Format = "{0:N0}";
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.7139315605163574), Telerik.Reporting.Drawing.Unit.Inch(7.8837074397597462E-05));
            this.textBox11.Name = "textBox9";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0582345724105835), Telerik.Reporting.Drawing.Unit.Inch(0.23996067047119141));
            this.textBox11.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox11.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox11.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox11.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox11.Style.Font.Bold = true;
            this.textBox11.Style.Font.Name = "Tahoma";
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox11.Value = "=Sum(TotalGA)";
            // 
            // textBox8
            // 
            this.textBox8.Format = "{0:N0}";
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.7390289306640625), Telerik.Reporting.Drawing.Unit.Inch(7.8837074397597462E-05));
            this.textBox8.Name = "textBox26";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97482365369796753), Telerik.Reporting.Drawing.Unit.Inch(0.23996067047119141));
            this.textBox8.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox8.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox8.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox8.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox8.Style.Font.Bold = true;
            this.textBox8.Style.Font.Name = "Tahoma";
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Value = "=Sum(TotalPA)";
            // 
            // textBox12
            // 
            this.textBox12.Format = "{0:N0}";
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.7084870338439941), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0304628610610962), Telerik.Reporting.Drawing.Unit.Inch(0.24000008404254913));
            this.textBox12.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox12.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox12.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox12.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox12.Style.Font.Bold = true;
            this.textBox12.Style.Font.Name = "Tahoma";
            this.textBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.Value = "=Sum(TotalPA+TotalGA)";
            // 
            // shape2
            // 
            this.shape2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.24011844396591187));
            this.shape2.Name = "shape2";
            this.shape2.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.8196883201599121), Telerik.Reporting.Drawing.Unit.Inch(0.05000000074505806));
            this.shape2.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.shape2.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            // 
            // textBox24
            // 
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0114504499360919), Telerik.Reporting.Drawing.Unit.Inch(0.018131256103515625));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.696958065032959), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox24.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox24.Style.Font.Bold = true;
            this.textBox24.Style.Font.Name = "Tahoma";
            this.textBox24.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox24.Value = "=\'Total  Pasien a.n \' +PatientName + \' : \'";
            // 
            // groupHeaderSection3
            // 
            this.groupHeaderSection3.Height = Telerik.Reporting.Drawing.Unit.Inch(0.24003951251506805);
            this.groupHeaderSection3.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox9,
            this.textBox28,
            this.textBox1});
            this.groupHeaderSection3.Name = "groupHeaderSection3";
            this.groupHeaderSection3.PrintOnEveryPage = true;
            this.groupHeaderSection3.Style.Visible = true;
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.30340257287025452), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.5335612297058105), Telerik.Reporting.Drawing.Unit.Inch(0.24000008404254913));
            this.textBox9.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox9.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox9.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox9.Style.Font.Bold = true;
            this.textBox9.Style.Font.Name = "Tahoma";
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox9.Value = "=\'Nama Pasien : \' + PatientName + \' (\' +MedicalNo +\')\'";
            // 
            // textBox28
            // 
            this.textBox28.Format = "{0}.";
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.303323894739151), Telerik.Reporting.Drawing.Unit.Inch(0.23999999463558197));
            this.textBox28.Style.BackgroundColor = System.Drawing.Color.White;
            this.textBox28.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox28.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox28.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox28.Style.Font.Bold = true;
            this.textBox28.Style.Font.Name = "Tahoma";
            this.textBox28.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox28.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox28.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox28.Value = "=RowNumber()";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.8647370338439941), Telerik.Reporting.Drawing.Unit.Inch(0.01809183694422245));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.9074287414550781), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox1.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox1.Style.Color = System.Drawing.Color.DarkRed;
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Tahoma";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "=\'Dokter : \' + ParamedicName";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0114504499360919), Telerik.Reporting.Drawing.Unit.Inch(0.1041666641831398));
            this.textBox5.Name = "textBox9";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1457548141479492), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Font.Name = "Tahoma";
            this.textBox5.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox5.Value = "No. Resep";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.157284140586853), Telerik.Reporting.Drawing.Unit.Inch(0.1041666641831398));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.0547587871551514), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.Style.Font.Name = "Tahoma";
            this.textBox7.Value = "=\': \'  + TransactionNo";
            // 
            // group4
            // 
            this.group4.GroupFooter = this.groupFooterSection4;
            this.group4.GroupHeader = this.groupHeaderSection4;
            this.group4.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("=TransactionNo")});
            this.group4.Name = "group4";
            this.group4.Sortings.AddRange(new Telerik.Reporting.Sorting[] {
            new Telerik.Reporting.Sorting("TransactionNo", Telerik.Reporting.SortDirection.Asc),
            new Telerik.Reporting.Sorting("No", Telerik.Reporting.SortDirection.Asc)});
            // 
            // groupFooterSection4
            // 
            this.groupFooterSection4.Height = Telerik.Reporting.Drawing.Unit.Inch(0.26087284088134766);
            this.groupFooterSection4.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox6,
            this.textBox18,
            this.textBox19,
            this.textBox23});
            this.groupFooterSection4.Name = "groupFooterSection4";
            // 
            // textBox6
            // 
            this.textBox6.Format = "{0:N0}";
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.7084870338439941), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0304628610610962), Telerik.Reporting.Drawing.Unit.Inch(0.24000008404254913));
            this.textBox6.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox6.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox6.Style.Color = System.Drawing.Color.Black;
            this.textBox6.Style.Font.Bold = true;
            this.textBox6.Style.Font.Name = "Tahoma";
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "=Sum(TotalPA+TotalGA)";
            // 
            // textBox18
            // 
            this.textBox18.Format = "{0:N0}";
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.7390289306640625), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97482365369796753), Telerik.Reporting.Drawing.Unit.Inch(0.24000008404254913));
            this.textBox18.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox18.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox18.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox18.Style.Color = System.Drawing.Color.Black;
            this.textBox18.Style.Font.Bold = true;
            this.textBox18.Style.Font.Name = "Tahoma";
            this.textBox18.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox18.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox18.Value = "=Sum(TotalPA)";
            // 
            // textBox19
            // 
            this.textBox19.Format = "{0:N0}";
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.7139315605163574), Telerik.Reporting.Drawing.Unit.Inch(7.8837074397597462E-05));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0582345724105835), Telerik.Reporting.Drawing.Unit.Inch(0.23996067047119141));
            this.textBox19.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox19.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox19.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5);
            this.textBox19.Style.Color = System.Drawing.Color.Black;
            this.textBox19.Style.Font.Bold = true;
            this.textBox19.Style.Font.Name = "Tahoma";
            this.textBox19.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox19.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox19.Value = "=Sum(TotalGA)";
            // 
            // textBox23
            // 
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.060833614319562912));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.7084083557128906), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox23.Style.Font.Bold = true;
            this.textBox23.Style.Font.Name = "Tahoma";
            this.textBox23.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox23.Value = "=\'Total  No. Resep \' + TransactionNo + \' : \'";
            // 
            // groupHeaderSection4
            // 
            this.groupHeaderSection4.Height = Telerik.Reporting.Drawing.Unit.Inch(0.64882916212081909);
            this.groupHeaderSection4.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox7,
            this.textBox5,
            this.textBox25,
            this.textBox10,
            this.textBox14,
            this.textBox26,
            this.textBox27,
            this.textBox4});
            this.groupHeaderSection4.Name = "groupHeaderSection4";
            // 
            // textBox25
            // 
            formattingRule1.Filters.AddRange(new Telerik.Reporting.Filter[] {
            new Telerik.Reporting.Filter("Jenis", Telerik.Reporting.FilterOperator.NotEqual, "Retur")});
            formattingRule1.Style.Visible = false;
            this.textBox25.ConditionalFormatting.AddRange(new Telerik.Reporting.Drawing.FormattingRule[] {
            formattingRule1});
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.2121217250823975), Telerik.Reporting.Drawing.Unit.Inch(0.1041666641831398));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2915090322494507), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox25.Style.Font.Bold = true;
            this.textBox25.Style.Font.Name = "Tahoma";
            this.textBox25.Value = "=\'(\' + Jenis + \')\'";
            // 
            // PrescriptionSalesByServiceUnitRpt
            // 
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            this.group1,
            this.group3,
            this.group4});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection1,
            this.groupFooterSection1,
            this.groupHeaderSection3,
            this.groupFooterSection3,
            this.groupHeaderSection4,
            this.groupFooterSection4,
            this.detail,
            this.pageFooterSection1,
            this.reportFooterSection1,
            this.reportHeaderSection1});
            this.Name = "PrescriptionSalesPerGuarantor";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Cm(0.5);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Cm(0.550000011920929);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Cm(0.550000011920929);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Sortings.AddRange(new Telerik.Reporting.Sorting[] {
            new Telerik.Reporting.Sorting("=PatientName", Telerik.Reporting.SortDirection.Asc),
            new Telerik.Reporting.Sorting("=TransactionNo", Telerik.Reporting.SortDirection.Asc),
            new Telerik.Reporting.Sorting("=No", Telerik.Reporting.SortDirection.Asc)});
            this.UnitOfMeasure = Telerik.Reporting.Drawing.UnitType.Cm;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.8197274208068848);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.DetailSection detail;
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
        private ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.TextBox textBox2;
        private Shape shape1;
        private Telerik.Reporting.TextBox textBox53;
        private Telerik.Reporting.TextBox textBox49;
        private Telerik.Reporting.TextBox txtPeriode;
        private ReportHeaderSection reportHeaderSection1;
        private Group group1;
        private GroupFooterSection groupFooterSection1;
        private GroupHeaderSection groupHeaderSection1;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox43;
        private Telerik.Reporting.TextBox textBox88;
        private Telerik.Reporting.TextBox textBox91;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox TxtCityRS;
        private Telerik.Reporting.TextBox TxtTelp;
        private Telerik.Reporting.TextBox TxtNameRS;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox10;
        private Group group3;
        private GroupFooterSection groupFooterSection3;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox9;
        private GroupHeaderSection groupHeaderSection3;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox7;
        private Shape shape2;
        private Group group4;
        private GroupFooterSection groupFooterSection4;
        private GroupHeaderSection groupHeaderSection4;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox28;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox textBox1;
    }
}