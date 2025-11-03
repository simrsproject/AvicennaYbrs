namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class RSSA_Slip_Mandiri
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
            this.txtBankAccNo = new Telerik.Reporting.TextBox();
            this.txtBankName = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
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
            this.txtBankAccNo,
            this.txtBankName,
            this.textBox3});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.Font.Name = "Microsoft Sans Serif";
            // 
            // txtTotalAmountInWords
            // 
            this.txtTotalAmountInWords.CanGrow = true;
            this.txtTotalAmountInWords.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.4000000953674316), Telerik.Reporting.Drawing.Unit.Inch(4.1500000953674316));
            this.txtTotalAmountInWords.Name = "txtTotalAmountInWords";
            this.txtTotalAmountInWords.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.348820686340332), Telerik.Reporting.Drawing.Unit.Inch(0.5));
            this.txtTotalAmountInWords.Style.Font.Name = "Microsoft Sans Serif";
            this.txtTotalAmountInWords.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.txtTotalAmountInWords.Value = "=TotalAmountInWords";
            // 
            // txtBankAccName
            // 
            this.txtBankAccName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1), Telerik.Reporting.Drawing.Unit.Inch(2.369999885559082));
            this.txtBankAccName.Name = "txtBankAccName";
            this.txtBankAccName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.7999997138977051), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtBankAccName.Style.Font.Bold = true;
            this.txtBankAccName.Style.Font.Name = "Microsoft Sans Serif";
            this.txtBankAccName.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.txtBankAccName.Value = "";
            // 
            // textBox30
            // 
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.1700000762939453), Telerik.Reporting.Drawing.Unit.Inch(1.5499999523162842));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.5999612808227539), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox30.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox30.Value = "=PrintReceiptAsName";
            // 
            // TxtAmount
            // 
            this.TxtAmount.CanGrow = false;
            this.TxtAmount.Format = "# Rp. {0:N0} #";
            this.TxtAmount.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.46999979019165), Telerik.Reporting.Drawing.Unit.Inch(3.869999885559082));
            this.TxtAmount.Name = "TxtAmount";
            this.TxtAmount.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1000003814697266), Telerik.Reporting.Drawing.Unit.Inch(0.2800000011920929));
            this.TxtAmount.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.TxtAmount.Style.Font.Bold = true;
            this.TxtAmount.Style.Font.Name = "Microsoft Sans Serif";
            this.TxtAmount.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(15);
            this.TxtAmount.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.TxtAmount.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.TxtAmount.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.TxtAmount.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.TxtAmount.Value = "=Amount";
            // 
            // textBox22
            // 
            this.textBox22.Format = "{0:dd-MMM-yyyy}";
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.4200000762939453), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448));
            this.textBox22.Name = "TxtUserName";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0314966440200806), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox22.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox22.Value = "=PaymentDate";
            // 
            // txtBankAccNo
            // 
            this.txtBankAccNo.CanGrow = false;
            this.txtBankAccNo.Format = "";
            this.txtBankAccNo.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1), Telerik.Reporting.Drawing.Unit.Inch(2.5699999332427979));
            this.txtBankAccNo.Name = "txtBankAccNo";
            this.txtBankAccNo.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.8000001907348633), Telerik.Reporting.Drawing.Unit.Inch(0.29999938607215881));
            this.txtBankAccNo.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.txtBankAccNo.Style.Font.Bold = true;
            this.txtBankAccNo.Style.Font.Name = "Microsoft Sans Serif";
            this.txtBankAccNo.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(17);
            this.txtBankAccNo.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(0);
            this.txtBankAccNo.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.txtBankAccNo.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtBankAccNo.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.txtBankAccNo.Value = "";
            // 
            // txtBankName
            // 
            this.txtBankName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1), Telerik.Reporting.Drawing.Unit.Inch(3));
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.7999997138977051), Telerik.Reporting.Drawing.Unit.Inch(0.19322797656059265));
            this.txtBankName.Style.Font.Bold = true;
            this.txtBankName.Style.Font.Name = "Microsoft Sans Serif";
            this.txtBankName.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(13);
            this.txtBankName.Value = "";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1), Telerik.Reporting.Drawing.Unit.Inch(3.1933069229125977));
            this.textBox3.Name = "textBox32";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(13);
            this.textBox3.Value = "RSSA";
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
            // RSSA_Slip_Mandiri
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
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(8.03149700164795);
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
        private Telerik.Reporting.TextBox txtBankAccNo;
        private Telerik.Reporting.TextBox txtBankName;
        private Telerik.Reporting.TextBox textBox3;
    }
}