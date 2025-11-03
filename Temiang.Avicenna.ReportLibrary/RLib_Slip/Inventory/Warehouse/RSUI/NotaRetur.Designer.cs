namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Inventory.Warehouse.RSUI
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class NotaRetur
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelsGroupHeader = new Telerik.Reporting.GroupHeaderSection();
            this.labelsGroupFooter = new Telerik.Reporting.GroupFooterSection();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox47 = new Telerik.Reporting.TextBox();
            this.txtFinance = new Telerik.Reporting.TextBox();
            this.textBox44 = new Telerik.Reporting.TextBox();
            this.textBox54 = new Telerik.Reporting.TextBox();
            this.textBox56 = new Telerik.Reporting.TextBox();
            this.textBox57 = new Telerik.Reporting.TextBox();
            this.textBox58 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.labelsGroup = new Telerik.Reporting.Group();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.titleTextBox = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox37 = new Telerik.Reporting.TextBox();
            this.textBox38 = new Telerik.Reporting.TextBox();
            this.textBox39 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.textBox33 = new Telerik.Reporting.TextBox();
            this.textBox34 = new Telerik.Reporting.TextBox();
            this.textBox35 = new Telerik.Reporting.TextBox();
            this.textBox36 = new Telerik.Reporting.TextBox();
            this.textBox40 = new Telerik.Reporting.TextBox();
            this.textBox41 = new Telerik.Reporting.TextBox();
            this.textBox42 = new Telerik.Reporting.TextBox();
            this.textBox43 = new Telerik.Reporting.TextBox();
            this.textBox45 = new Telerik.Reporting.TextBox();
            this.textBox46 = new Telerik.Reporting.TextBox();
            this.textBox48 = new Telerik.Reporting.TextBox();
            this.textBox51 = new Telerik.Reporting.TextBox();
            this.textBox52 = new Telerik.Reporting.TextBox();
            this.textBox53 = new Telerik.Reporting.TextBox();
            this.textBox49 = new Telerik.Reporting.TextBox();
            this.textBox50 = new Telerik.Reporting.TextBox();
            this.textBox55 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // labelsGroupHeader
            // 
            this.labelsGroupHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699);
            this.labelsGroupHeader.Name = "labelsGroupHeader";
            this.labelsGroupHeader.PrintOnEveryPage = true;
            this.labelsGroupHeader.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.labelsGroupHeader.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.labelsGroupHeader.Style.Visible = false;
            // 
            // labelsGroupFooter
            // 
            this.labelsGroupFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(2.895951509475708);
            this.labelsGroupFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox20,
            this.textBox21,
            this.textBox28,
            this.textBox29,
            this.textBox10,
            this.textBox17,
            this.textBox47,
            this.txtFinance,
            this.textBox44,
            this.textBox54,
            this.textBox56,
            this.textBox57,
            this.textBox58,
            this.textBox12});
            this.labelsGroupFooter.Name = "labelsGroupFooter";
            this.labelsGroupFooter.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.labelsGroupFooter.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.labelsGroupFooter.Style.Font.Name = "Tahoma";
            this.labelsGroupFooter.Style.Visible = true;
            // 
            // textBox20
            // 
            this.textBox20.Format = "{0:N2}";
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.631645679473877), Telerik.Reporting.Drawing.Unit.Inch(0.10000022500753403));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1415514945983887), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox20.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox20.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox20.Style.Visible = true;
            this.textBox20.Value = "=sum(SubTotal)";
            // 
            // textBox21
            // 
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.099999904632568359));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.6315269470214844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox21.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox21.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox21.Style.Visible = true;
            this.textBox21.Value = "Jumlah harga jual BKP yang diKembalikan :";
            // 
            // textBox28
            // 
            this.textBox28.Format = "{0:N2}";
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.631645679473877), Telerik.Reporting.Drawing.Unit.Inch(0.30007919669151306));
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1415514945983887), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox28.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox28.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox28.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox28.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox28.Style.Visible = true;
            this.textBox28.Value = "=-TaxAmount";
            // 
            // textBox29
            // 
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.30007919669151306));
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.6315269470214844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox29.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox29.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox29.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox29.Style.Visible = true;
            this.textBox29.Value = "PPN yang diminta kembali :";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.50015813112258911));
            this.textBox10.Name = "textBox21";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.6315269470214844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox10.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox10.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox10.Style.Visible = true;
            this.textBox10.Value = "PPnMB yang diminta kembalikan :";
            // 
            // textBox17
            // 
            this.textBox17.Format = "{0:N2}";
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.631645679473877), Telerik.Reporting.Drawing.Unit.Inch(0.50015813112258911));
            this.textBox17.Name = "textBox20";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1415514945983887), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox17.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox17.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox17.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox17.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox17.Style.Visible = true;
            this.textBox17.Value = "=sum(SubTotal) - TaxAmount";
            // 
            // textBox47
            // 
            this.textBox47.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.8000001907348633), Telerik.Reporting.Drawing.Unit.Inch(1.1479562520980835));
            this.textBox47.Name = "textBox42";
            this.textBox47.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.8999990224838257), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox47.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox47.Value = "Pembeli";
            // 
            // txtFinance
            // 
            this.txtFinance.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.7999987602233887), Telerik.Reporting.Drawing.Unit.Inch(1.8479562997817993));
            this.txtFinance.Name = "txtFinance";
            this.txtFinance.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.8999999761581421), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.txtFinance.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtFinance.Value = "";
            // 
            // textBox44
            // 
            this.textBox44.Format = "{0}, ";
            this.textBox44.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.01690149307251), Telerik.Reporting.Drawing.Unit.Inch(0.84795635938644409));
            this.textBox44.Name = "textBox44";
            this.textBox44.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.6409878730773926), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox44.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox44.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox44.Value = "=FoundationCity";
            // 
            // textBox54
            // 
            this.textBox54.Format = "{0:dd MMM yyyy}";
            this.textBox54.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.6579680442810059), Telerik.Reporting.Drawing.Unit.Inch(0.84795635938644409));
            this.textBox54.Name = "textBox54";
            this.textBox54.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1001561880111694), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox54.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox54.Value = "=NOW()";
            // 
            // textBox56
            // 
            this.textBox56.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.026964029297232628), Telerik.Reporting.Drawing.Unit.Inch(2.5960304737091064));
            this.textBox56.Name = "textBox56";
            this.textBox56.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.7461934089660645), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox56.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox56.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox56.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox56.Value = "Lembar Ke 3 : untuk KPP tempat Pembeli terdaftar ( dalam hal pembelian bukan PKP " +
                ")";
            // 
            // textBox57
            // 
            this.textBox57.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.026964029297232628), Telerik.Reporting.Drawing.Unit.Inch(2.395951509475708));
            this.textBox57.Name = "textBox57";
            this.textBox57.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.7461934089660645), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox57.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox57.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox57.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox57.Value = "Lembar Ke 2 : untuk Pembeli";
            // 
            // textBox58
            // 
            this.textBox58.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.026964029297232628), Telerik.Reporting.Drawing.Unit.Inch(2.1958725452423096));
            this.textBox58.Name = "textBox58";
            this.textBox58.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.7461934089660645), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox58.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox58.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox58.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox58.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox58.Value = "Lembar Ke 1 : untuk PKP Penjual";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.6252808570861816), Telerik.Reporting.Drawing.Unit.Inch(0.700236976146698));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.14791648089885712), Telerik.Reporting.Drawing.Unit.Inch(1.4955569505691528));
            this.textBox12.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox12.Value = "";
            // 
            // labelsGroup
            // 
            this.labelsGroup.GroupFooter = this.labelsGroupFooter;
            this.labelsGroup.GroupHeader = this.labelsGroupHeader;
            this.labelsGroup.Name = "labelsGroup";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.28125);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.currentTimeTextBox,
            this.pageInfoTextBox});
            this.pageFooter.Name = "pageFooter";
            this.pageFooter.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.pageFooter.Style.Font.Name = "Tahoma";
            // 
            // currentTimeTextBox
            // 
            this.currentTimeTextBox.Format = "Print : {0}";
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.10000038146972656), Telerik.Reporting.Drawing.Unit.Inch(0.0812106654047966));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.1979167461395264), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.currentTimeTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.currentTimeTextBox.Value = "=NOW()";
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Format = "Hal : {0}";
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.41077184677124), Telerik.Reporting.Drawing.Unit.Inch(0.0812106654047966));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3624254465103149), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.pageInfoTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pageInfoTextBox.Value = "=PageNumber";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20003943145275116);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox18,
            this.textBox23,
            this.textBox24,
            this.textBox31,
            this.textBox9,
            this.textBox14,
            this.textBox16});
            this.detail.Name = "detail";
            this.detail.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.detail.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.detail.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.detail.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.detail.Style.Font.Name = "Tahoma";
            // 
            // textBox18
            // 
            this.textBox18.Format = "{0:N2}";
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.9899382591247559), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.92688471078872681), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox18.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox18.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox18.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox18.Style.Font.Bold = false;
            this.textBox18.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox18.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox18.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox18.Value = "=Price";
            // 
            // textBox23
            // 
            this.textBox23.Format = "{0:N2}";
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.01690149307251), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.972957193851471), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox23.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox23.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox23.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox23.Style.Font.Bold = false;
            this.textBox23.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox23.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox23.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox23.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox23.Value = "=Quantity";
            // 
            // textBox24
            // 
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.9168226718902588), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox24.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox24.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox24.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox24.Style.Font.Bold = false;
            this.textBox24.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox24.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox24.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox24.Value = "=ItemName";
            // 
            // textBox31
            // 
            this.textBox31.Format = "{0:#\".\"}";
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox31.Name = "textBox23";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox31.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox31.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox31.Style.Font.Bold = false;
            this.textBox31.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox31.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox31.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox31.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox31.Value = "= RowNumber()";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.30007877945899963), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.79984229803085327), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox9.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox9.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox9.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox9.Style.Font.Bold = false;
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox9.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox9.Value = "=ItemID";
            // 
            // textBox14
            // 
            this.textBox14.Format = "{0:N2}";
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.9169020652771), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.71466463804245), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox14.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox14.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox14.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox14.Style.Font.Bold = false;
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox14.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox14.Value = "=Discount";
            // 
            // textBox16
            // 
            this.textBox16.Format = "{0:N2}";
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.631645679473877), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1415512561798096), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox16.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox16.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox16.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox16.Style.Font.Bold = false;
            this.textBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox16.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox16.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox16.Value = "=SubTotal";
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(11.054091453552246);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.titleTextBox,
            this.textBox2,
            this.textBox1,
            this.textBox6,
            this.textBox19,
            this.textBox37,
            this.textBox38,
            this.textBox39,
            this.textBox26,
            this.textBox3,
            this.textBox4,
            this.textBox5,
            this.textBox7,
            this.textBox8,
            this.textBox11,
            this.textBox15,
            this.textBox22,
            this.textBox27,
            this.textBox25,
            this.textBox30,
            this.textBox32,
            this.textBox33,
            this.textBox34,
            this.textBox35,
            this.textBox36,
            this.textBox40,
            this.textBox41,
            this.textBox42,
            this.textBox43,
            this.textBox45,
            this.textBox46,
            this.textBox48,
            this.textBox51,
            this.textBox52,
            this.textBox53,
            this.textBox49,
            this.textBox50,
            this.textBox55});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            this.pageHeaderSection1.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.pageHeaderSection1.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.pageHeaderSection1.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.pageHeaderSection1.Style.Font.Name = "Tahoma";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(-1.4901161193847656E-08), Telerik.Reporting.Drawing.Unit.Inch(0.59999996423721313));
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.5626583099365234), Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134));
            this.titleTextBox.Style.Font.Bold = true;
            this.titleTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14);
            this.titleTextBox.Style.Font.Underline = false;
            this.titleTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.titleTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.titleTextBox.Value = "Nota Retur";
            // 
            // textBox2
            // 
            this.textBox2.Format = "";
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.5752720832824707), Telerik.Reporting.Drawing.Unit.Inch(0.90007877349853516));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.9832597970962524), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox2.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox2.Style.Visible = true;
            this.textBox2.Value = "=TransactionNo";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.2625746726989746), Telerik.Reporting.Drawing.Unit.Inch(0.90007877349853516));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1958742141723633), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox1.Style.Visible = true;
            this.textBox1.Value = "No. Retur";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000785827636719), Telerik.Reporting.Drawing.Unit.Inch(3.8000001907348633));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.9167442321777344), Telerik.Reporting.Drawing.Unit.Inch(0.29996064305305481));
            this.textBox6.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox6.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.Font.Bold = true;
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "Nama Barang";
            // 
            // textBox19
            // 
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.041668254882097244), Telerik.Reporting.Drawing.Unit.Inch(3.1000792980194092));
            this.textBox19.Name = "textBox1";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1999989748001099), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox19.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox19.Value = "Status Retur";
            // 
            // textBox37
            // 
            this.textBox37.Format = "";
            this.textBox37.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3584914207458496), Telerik.Reporting.Drawing.Unit.Inch(3.1000003814697266));
            this.textBox37.Name = "textBox2";
            this.textBox37.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.3000006675720215), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox37.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox37.Value = "=StreetName";
            // 
            // textBox38
            // 
            this.textBox38.Format = "";
            this.textBox38.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3584914207458496), Telerik.Reporting.Drawing.Unit.Inch(2.9000003337860107));
            this.textBox38.Name = "textBox38";
            this.textBox38.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.6583316326141357), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox38.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox38.Value = "=SupplierName";
            // 
            // textBox39
            // 
            this.textBox39.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.041668254882097244), Telerik.Reporting.Drawing.Unit.Inch(2.9000003337860107));
            this.textBox39.Name = "textBox3";
            this.textBox39.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1999989748001099), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox39.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox39.Value = "Nama Supplier";
            // 
            // textBox26
            // 
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(3.8000001907348633));
            this.textBox26.Name = "textBox7";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134), Telerik.Reporting.Drawing.Unit.Inch(0.29996064305305481));
            this.textBox26.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox26.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox26.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox26.Style.Font.Bold = true;
            this.textBox26.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox26.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox26.Value = "No";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.30007871985435486), Telerik.Reporting.Drawing.Unit.Inch(3.8000001907348633));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.79992133378982544), Telerik.Reporting.Drawing.Unit.Inch(0.29996064305305481));
            this.textBox3.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox3.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox3.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox3.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = "Kode Item";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.4899382591247559), Telerik.Reporting.Drawing.Unit.Inch(3.8000004291534424));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.499920517206192), Telerik.Reporting.Drawing.Unit.Inch(0.29996064305305481));
            this.textBox4.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox4.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox4.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox4.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox4.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.Value = "Satuan";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.01690149307251), Telerik.Reporting.Drawing.Unit.Inch(3.8000001907348633));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.47295761108398438), Telerik.Reporting.Drawing.Unit.Inch(0.29996064305305481));
            this.textBox5.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox5.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox5.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox5.Value = "Qty";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.9899382591247559), Telerik.Reporting.Drawing.Unit.Inch(3.8000001907348633));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.92688494920730591), Telerik.Reporting.Drawing.Unit.Inch(0.29996064305305481));
            this.textBox7.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox7.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.Value = "Harga";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.9169020652771), Telerik.Reporting.Drawing.Unit.Inch(3.7999999523162842));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.71466511487960815), Telerik.Reporting.Drawing.Unit.Inch(0.29996064305305481));
            this.textBox8.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox8.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox8.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox8.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox8.Style.Font.Bold = true;
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Value = "Diskon";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.631645679473877), Telerik.Reporting.Drawing.Unit.Inch(3.7999999523162842));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1415119171142578), Telerik.Reporting.Drawing.Unit.Inch(0.29996064305305481));
            this.textBox11.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox11.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox11.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox11.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox11.Style.Font.Bold = true;
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox11.Value = "Sub Total";
            // 
            // textBox15
            // 
            this.textBox15.Format = "{0:dd-MMM-yy}";
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.5793986320495605), Telerik.Reporting.Drawing.Unit.Inch(1.100157618522644));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.9832597970962524), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox15.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox15.Style.Visible = true;
            this.textBox15.Value = "=TransactionDate";
            // 
            // textBox22
            // 
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.2625737190246582), Telerik.Reporting.Drawing.Unit.Inch(1.100157618522644));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1958742141723633), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox22.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox22.Style.Visible = true;
            this.textBox22.Value = "Tanggal";
            // 
            // textBox27
            // 
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.462653636932373), Telerik.Reporting.Drawing.Unit.Inch(1.100157618522644));
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.11666647344827652), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox27.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox27.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox27.Style.Visible = true;
            this.textBox27.Value = ":";
            // 
            // textBox25
            // 
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.4585270881652832), Telerik.Reporting.Drawing.Unit.Inch(0.90007877349853516));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.11666647344827652), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox25.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox25.Style.Visible = true;
            this.textBox25.Value = ":";
            // 
            // textBox30
            // 
            this.textBox30.Format = "";
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(1.4000000953674316));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.7731575965881348), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox30.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox30.Style.Font.Bold = true;
            this.textBox30.Style.Font.Underline = true;
            this.textBox30.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox30.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox30.Value = "Pembelian Barang Kena Pajak";
            // 
            // textBox32
            // 
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.026964029297232628), Telerik.Reporting.Drawing.Unit.Inch(1.7000001668930054));
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1999989748001099), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox32.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox32.Value = "Nama";
            // 
            // textBox33
            // 
            this.textBox33.Format = "";
            this.textBox33.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9378803194267675E-05), Telerik.Reporting.Drawing.Unit.Inch(2.5));
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.7731184959411621), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox33.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox33.Style.Font.Bold = true;
            this.textBox33.Style.Font.Underline = true;
            this.textBox33.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox33.Value = "Kepada Penjual";
            // 
            // textBox34
            // 
            this.textBox34.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2270418405532837), Telerik.Reporting.Drawing.Unit.Inch(1.7000001668930054));
            this.textBox34.Name = "textBox34";
            this.textBox34.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.11666647344827652), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox34.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox34.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox34.Style.Visible = true;
            this.textBox34.Value = ":";
            // 
            // textBox35
            // 
            this.textBox35.Format = "";
            this.textBox35.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.026964029297232628), Telerik.Reporting.Drawing.Unit.Inch(1.9000788927078247));
            this.textBox35.Name = "textBox35";
            this.textBox35.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1999989748001099), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox35.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox35.Value = "Alamat";
            // 
            // textBox36
            // 
            this.textBox36.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2270418405532837), Telerik.Reporting.Drawing.Unit.Inch(1.9000788927078247));
            this.textBox36.Name = "textBox36";
            this.textBox36.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.11666647344827652), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox36.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox36.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox36.Style.Visible = true;
            this.textBox36.Value = ":";
            // 
            // textBox40
            // 
            this.textBox40.Format = "";
            this.textBox40.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.026964029297232628), Telerik.Reporting.Drawing.Unit.Inch(2.1001577377319336));
            this.textBox40.Name = "textBox40";
            this.textBox40.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1999989748001099), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox40.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox40.Value = "NPWP";
            // 
            // textBox41
            // 
            this.textBox41.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2270418405532837), Telerik.Reporting.Drawing.Unit.Inch(2.1001574993133545));
            this.textBox41.Name = "textBox41";
            this.textBox41.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.11666647344827652), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox41.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox41.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox41.Style.Visible = true;
            this.textBox41.Value = ":";
            // 
            // textBox42
            // 
            this.textBox42.Format = "";
            this.textBox42.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3437871932983398), Telerik.Reporting.Drawing.Unit.Inch(1.7000001668930054));
            this.textBox42.Name = "textBox42";
            this.textBox42.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.3000006675720215), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox42.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox42.Value = "=FoundationName";
            // 
            // textBox43
            // 
            this.textBox43.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.041668254882097244), Telerik.Reporting.Drawing.Unit.Inch(3.3001582622528076));
            this.textBox43.Name = "textBox43";
            this.textBox43.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1999989748001099), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox43.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox43.Value = "NPWP";
            // 
            // textBox45
            // 
            this.textBox45.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2417460680007935), Telerik.Reporting.Drawing.Unit.Inch(2.9000003337860107));
            this.textBox45.Name = "textBox45";
            this.textBox45.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.11666647344827652), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox45.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox45.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox45.Style.Visible = true;
            this.textBox45.Value = ":";
            // 
            // textBox46
            // 
            this.textBox46.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2417460680007935), Telerik.Reporting.Drawing.Unit.Inch(3.1000792980194092));
            this.textBox46.Name = "textBox46";
            this.textBox46.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.11666647344827652), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox46.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox46.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox46.Style.Visible = true;
            this.textBox46.Value = ":";
            // 
            // textBox48
            // 
            this.textBox48.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2417460680007935), Telerik.Reporting.Drawing.Unit.Inch(3.3001582622528076));
            this.textBox48.Name = "textBox48";
            this.textBox48.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.11666647344827652), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox48.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox48.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox48.Style.Visible = true;
            this.textBox48.Value = ":";
            // 
            // textBox51
            // 
            this.textBox51.Format = "";
            this.textBox51.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3437868356704712), Telerik.Reporting.Drawing.Unit.Inch(1.9000786542892456));
            this.textBox51.Name = "textBox51";
            this.textBox51.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.214745044708252), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox51.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox51.Value = "=FoundationAddr";
            // 
            // textBox52
            // 
            this.textBox52.Format = "";
            this.textBox52.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3437871932983398), Telerik.Reporting.Drawing.Unit.Inch(2.1001574993133545));
            this.textBox52.Name = "textBox52";
            this.textBox52.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.3000006675720215), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox52.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox52.Value = "=NPWP";
            // 
            // textBox53
            // 
            this.textBox53.Format = "";
            this.textBox53.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.35849130153656), Telerik.Reporting.Drawing.Unit.Inch(3.3001582622528076));
            this.textBox53.Name = "textBox53";
            this.textBox53.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.6583316326141357), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox53.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox53.Style.Visible = true;
            this.textBox53.Value = "=TaxRegistrationNo";
            // 
            // textBox49
            // 
            this.textBox49.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.026964029297232628), Telerik.Reporting.Drawing.Unit.Inch(1.100157618522644));
            this.textBox49.Name = "textBox49";
            this.textBox49.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5729571580886841), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox49.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox49.Style.Visible = true;
            this.textBox49.Value = "Atas Faktur Pajak Nomor";
            // 
            // textBox50
            // 
            this.textBox50.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.6000000238418579), Telerik.Reporting.Drawing.Unit.Inch(1.100157618522644));
            this.textBox50.Name = "textBox50";
            this.textBox50.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.11666647344827652), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224));
            this.textBox50.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox50.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox50.Style.Visible = true;
            this.textBox50.Value = ":";
            // 
            // textBox55
            // 
            this.textBox55.Format = "{0:dd-MMM-yy}";
            this.textBox55.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.7167453765869141), Telerik.Reporting.Drawing.Unit.Inch(1.100157618522644));
            this.textBox55.Name = "textBox55";
            this.textBox55.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.5415089130401611), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox55.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.textBox55.Style.Visible = true;
            this.textBox55.Value = "=InvoiceNo";
            // 
            // NotaRetur
            // 
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            this.labelsGroup});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.labelsGroupHeader,
            this.labelsGroupFooter,
            this.pageFooter,
            this.detail,
            this.pageHeaderSection1});
            this.Name = "PurchaseReturn";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Cm(0.5);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Cm(0.5);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Cm(0);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(0);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Underline = false;
            this.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(1);
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.7731971740722656);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private GroupHeaderSection labelsGroupHeader;
        private GroupFooterSection labelsGroupFooter;
        private Group labelsGroup;
        private PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private DetailSection detail;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox28;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox textBox47;
        private Telerik.Reporting.TextBox txtFinance;
        private PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.TextBox titleTextBox;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox37;
        private Telerik.Reporting.TextBox textBox38;
        private Telerik.Reporting.TextBox textBox39;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.TextBox textBox33;
        private Telerik.Reporting.TextBox textBox34;
        private Telerik.Reporting.TextBox textBox35;
        private Telerik.Reporting.TextBox textBox36;
        private Telerik.Reporting.TextBox textBox40;
        private Telerik.Reporting.TextBox textBox41;
        private Telerik.Reporting.TextBox textBox42;
        private Telerik.Reporting.TextBox textBox43;
        private Telerik.Reporting.TextBox textBox45;
        private Telerik.Reporting.TextBox textBox46;
        private Telerik.Reporting.TextBox textBox48;
        private Telerik.Reporting.TextBox textBox51;
        private Telerik.Reporting.TextBox textBox52;
        private Telerik.Reporting.TextBox textBox53;
        private Telerik.Reporting.TextBox textBox44;
        private Telerik.Reporting.TextBox textBox54;
        private Telerik.Reporting.TextBox textBox49;
        private Telerik.Reporting.TextBox textBox50;
        private Telerik.Reporting.TextBox textBox55;
        private Telerik.Reporting.TextBox textBox56;
        private Telerik.Reporting.TextBox textBox57;
        private Telerik.Reporting.TextBox textBox58;
        private Telerik.Reporting.TextBox textBox12;

    }
}