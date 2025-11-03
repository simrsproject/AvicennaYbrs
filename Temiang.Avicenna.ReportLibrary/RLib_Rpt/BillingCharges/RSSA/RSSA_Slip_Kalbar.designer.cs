namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges.RSSA
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class RSSA_Slip_Kalbar
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.txtTotalAmountInWords = new Telerik.Reporting.TextBox();
            this.txtBankAccName = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.TxtAmount = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.txt1 = new Telerik.Reporting.TextBox();
            this.txtBankName = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(5.1083335876464844);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtTotalAmountInWords,
            this.txtBankAccName,
            this.textBox30,
            this.TxtAmount,
            this.textBox22,
            this.txt1,
            this.txtBankName,
            this.textBox3,
            this.textBox1,
            this.textBox2,
            this.textBox4,
            this.textBox5,
            this.textBox6,
            this.textBox7,
            this.textBox8,
            this.textBox9,
            this.textBox10,
            this.textBox11,
            this.textBox12,
            this.textBox13,
            this.textBox14,
            this.textBox15});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.Font.Name = "Microsoft Sans Serif";
            // 
            // txtTotalAmountInWords
            // 
            this.txtTotalAmountInWords.CanGrow = true;
            this.txtTotalAmountInWords.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.92000001668930054), Telerik.Reporting.Drawing.Unit.Inch(3.1000001430511475));
            this.txtTotalAmountInWords.Name = "txtTotalAmountInWords";
            this.txtTotalAmountInWords.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.1603546142578125), Telerik.Reporting.Drawing.Unit.Inch(0.81984233856201172));
            this.txtTotalAmountInWords.Style.Font.Name = "Microsoft Sans Serif";
            this.txtTotalAmountInWords.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.txtTotalAmountInWords.Value = "=TotalAmountInWords";
            // 
            // txtBankAccName
            // 
            this.txtBankAccName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.75), Telerik.Reporting.Drawing.Unit.Inch(2.1600000858306885));
            this.txtBankAccName.Name = "txtBankAccName";
            this.txtBankAccName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.130354642868042), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtBankAccName.Style.Font.Bold = false;
            this.txtBankAccName.Style.Font.Name = "Microsoft Sans Serif";
            this.txtBankAccName.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.txtBankAccName.Value = "";
            // 
            // textBox30
            // 
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.75), Telerik.Reporting.Drawing.Unit.Inch(1.1100001335144043));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.0803530216217041), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox30.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox30.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            this.textBox30.Value = "";
            // 
            // TxtAmount
            // 
            this.TxtAmount.CanGrow = false;
            this.TxtAmount.Format = "# Rp. {0:N0} #";
            this.TxtAmount.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579), Telerik.Reporting.Drawing.Unit.Inch(2.9198424816131592));
            this.TxtAmount.Name = "TxtAmount";
            this.TxtAmount.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7041662931442261), Telerik.Reporting.Drawing.Unit.Inch(0.18007878959178925));
            this.TxtAmount.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.TxtAmount.Style.Font.Bold = true;
            this.TxtAmount.Style.Font.Name = "Microsoft Sans Serif";
            this.TxtAmount.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.TxtAmount.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.TxtAmount.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.TxtAmount.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.TxtAmount.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.TxtAmount.Value = "=Amount";
            // 
            // textBox22
            // 
            this.textBox22.Format = "{0:dd-MMM-yyyy}";
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.0999999046325684), Telerik.Reporting.Drawing.Unit.Inch(0.92000007629394531));
            this.textBox22.Name = "TxtUserName";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.9999217987060547), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox22.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox22.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox22.Value = "=Now()";
            // 
            // txt1
            // 
            this.txt1.CanGrow = false;
            this.txt1.Format = "";
            this.txt1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.75), Telerik.Reporting.Drawing.Unit.Inch(2.5799217224121094));
            this.txt1.Name = "txt1";
            this.txt1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737), Telerik.Reporting.Drawing.Unit.Inch(0.19999973475933075));
            this.txt1.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.txt1.Style.Font.Bold = false;
            this.txt1.Style.Font.Name = "Microsoft Sans Serif";
            this.txt1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.txt1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(0);
            this.txt1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.txt1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txt1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.txt1.Value = "";
            // 
            // txtBankName
            // 
            this.txtBankName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.75), Telerik.Reporting.Drawing.Unit.Inch(2.7800004482269287));
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.1303532123565674), Telerik.Reporting.Drawing.Unit.Inch(0.19322797656059265));
            this.txtBankName.Style.Font.Bold = false;
            this.txtBankName.Style.Font.Name = "Microsoft Sans Serif";
            this.txtBankName.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.txtBankName.Value = "";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.75), Telerik.Reporting.Drawing.Unit.Inch(2.3798427581787109));
            this.textBox3.Name = "textBox32";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.1303536891937256), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox3.Style.Font.Bold = false;
            this.textBox3.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox3.Value = "";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.92000001668930054), Telerik.Reporting.Drawing.Unit.Inch(1.130000114440918));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox1.Value = "x";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = false;
            this.textBox2.Format = "";
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.9300789833068848), Telerik.Reporting.Drawing.Unit.Inch(2.5799217224121094));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.16992098093032837), Telerik.Reporting.Drawing.Unit.Inch(0.19999973475933075));
            this.textBox2.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox2.Style.Font.Bold = false;
            this.textBox2.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox2.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(0);
            this.textBox2.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox2.Value = "";
            // 
            // textBox4
            // 
            this.textBox4.CanGrow = false;
            this.textBox4.Format = "";
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.10015869140625), Telerik.Reporting.Drawing.Unit.Inch(2.5799217224121094));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737), Telerik.Reporting.Drawing.Unit.Inch(0.19999973475933075));
            this.textBox4.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox4.Style.Font.Bold = false;
            this.textBox4.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox4.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(0);
            this.textBox4.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox4.Value = "";
            // 
            // textBox5
            // 
            this.textBox5.CanGrow = false;
            this.textBox5.Format = "";
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.2802376747131348), Telerik.Reporting.Drawing.Unit.Inch(2.5799217224121094));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737), Telerik.Reporting.Drawing.Unit.Inch(0.19999973475933075));
            this.textBox5.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox5.Style.Font.Bold = false;
            this.textBox5.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox5.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(0);
            this.textBox5.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox5.Value = "";
            // 
            // textBox6
            // 
            this.textBox6.CanGrow = false;
            this.textBox6.Format = "";
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.4603171348571777), Telerik.Reporting.Drawing.Unit.Inch(2.5799217224121094));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737), Telerik.Reporting.Drawing.Unit.Inch(0.19999973475933075));
            this.textBox6.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox6.Style.Font.Bold = false;
            this.textBox6.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox6.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(0);
            this.textBox6.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox6.Value = "";
            // 
            // textBox7
            // 
            this.textBox7.CanGrow = false;
            this.textBox7.Format = "";
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.6403961181640625), Telerik.Reporting.Drawing.Unit.Inch(2.5799217224121094));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737), Telerik.Reporting.Drawing.Unit.Inch(0.19999973475933075));
            this.textBox7.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox7.Style.Font.Bold = false;
            this.textBox7.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox7.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(0);
            this.textBox7.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox7.Value = "";
            // 
            // textBox8
            // 
            this.textBox8.CanGrow = false;
            this.textBox8.Format = "";
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.8204751014709473), Telerik.Reporting.Drawing.Unit.Inch(2.5799217224121094));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737), Telerik.Reporting.Drawing.Unit.Inch(0.19999973475933075));
            this.textBox8.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox8.Style.Font.Bold = false;
            this.textBox8.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox8.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(0);
            this.textBox8.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox8.Value = "";
            // 
            // textBox9
            // 
            this.textBox9.CanGrow = false;
            this.textBox9.Format = "";
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.00055456161499), Telerik.Reporting.Drawing.Unit.Inch(2.5799214839935303));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737), Telerik.Reporting.Drawing.Unit.Inch(0.19999973475933075));
            this.textBox9.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox9.Style.Font.Bold = false;
            this.textBox9.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox9.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(0);
            this.textBox9.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox9.Value = "";
            // 
            // textBox10
            // 
            this.textBox10.CanGrow = false;
            this.textBox10.Format = "";
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.180633544921875), Telerik.Reporting.Drawing.Unit.Inch(2.5799217224121094));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737), Telerik.Reporting.Drawing.Unit.Inch(0.19999973475933075));
            this.textBox10.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox10.Style.Font.Bold = false;
            this.textBox10.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox10.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(0);
            this.textBox10.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox10.Value = "";
            // 
            // textBox11
            // 
            this.textBox11.CanGrow = false;
            this.textBox11.Format = "";
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.36071252822876), Telerik.Reporting.Drawing.Unit.Inch(2.5799214839935303));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737), Telerik.Reporting.Drawing.Unit.Inch(0.19999973475933075));
            this.textBox11.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox11.Style.Font.Bold = false;
            this.textBox11.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox11.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(0);
            this.textBox11.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox11.Value = "";
            // 
            // textBox12
            // 
            this.textBox12.CanGrow = false;
            this.textBox12.Format = "";
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.75), Telerik.Reporting.Drawing.Unit.Inch(3.2799999713897705));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7907127141952515), Telerik.Reporting.Drawing.Unit.Inch(0.19999973475933075));
            this.textBox12.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox12.Style.Font.Bold = false;
            this.textBox12.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox12.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(0);
            this.textBox12.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox12.Style.Visible = false;
            this.textBox12.Value = "";
            // 
            // textBox13
            // 
            this.textBox13.CanGrow = false;
            this.textBox13.Format = "";
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.5407919883728027), Telerik.Reporting.Drawing.Unit.Inch(2.5799214839935303));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737), Telerik.Reporting.Drawing.Unit.Inch(0.19999973475933075));
            this.textBox13.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox13.Style.Font.Bold = false;
            this.textBox13.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox13.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(0);
            this.textBox13.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox13.Value = "";
            // 
            // textBox14
            // 
            this.textBox14.CanGrow = false;
            this.textBox14.Format = "";
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.7208709716796875), Telerik.Reporting.Drawing.Unit.Inch(2.5799214839935303));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737), Telerik.Reporting.Drawing.Unit.Inch(0.19999973475933075));
            this.textBox14.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox14.Style.Font.Bold = false;
            this.textBox14.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox14.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(0);
            this.textBox14.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox14.Value = "";
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.75), Telerik.Reporting.Drawing.Unit.Inch(2.9733073711395264));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.1303532123565674), Telerik.Reporting.Drawing.Unit.Inch(0.19322797656059265));
            this.textBox15.Style.Font.Bold = false;
            this.textBox15.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            this.textBox15.Value = "";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699);
            this.detail.Name = "detail";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699);
            this.pageFooter.Name = "pageFooter";
            // 
            // RSSA_Slip_Kalbar
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail,
            this.pageFooter});
            this.Name = "RSSA_Slip_Mandiri";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Cm(0.699999988079071);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(0.699999988079071);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageSettings.PaperSize = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(215), Telerik.Reporting.Drawing.Unit.Mm(155));
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(8.0314950942993164);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox txtTotalAmountInWords;
        private Telerik.Reporting.TextBox txtBankAccName;
        private Telerik.Reporting.TextBox TxtAmount;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox txt1;
        private Telerik.Reporting.TextBox txtBankName;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox15;
    }
}