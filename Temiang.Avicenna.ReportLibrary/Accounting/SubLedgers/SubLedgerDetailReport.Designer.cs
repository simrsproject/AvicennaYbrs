namespace Temiang.Avicenna.ReportLibrary.Accounting.SubLedgers
{
    partial class SubLedgerDetailReport
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.Drawing.FormattingRule formattingRule1 = new Telerik.Reporting.Drawing.FormattingRule();
            Telerik.Reporting.Drawing.FormattingRule formattingRule2 = new Telerik.Reporting.Drawing.FormattingRule();
            Telerik.Reporting.Drawing.FormattingRule formattingRule3 = new Telerik.Reporting.Drawing.FormattingRule();
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group2 = new Telerik.Reporting.Group();
            this.groupFooterChartOfAccount = new Telerik.Reporting.GroupFooterSection();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.groupHeaderChartOfAccount = new Telerik.Reporting.GroupHeaderSection();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.groupFooterSubLedger = new Telerik.Reporting.GroupFooterSection();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.groupHeaderSectionSubLedger = new Telerik.Reporting.GroupHeaderSection();
            this.SubLedgerDescription = new Telerik.Reporting.TextBox();
            this.SubLedgerName = new Telerik.Reporting.TextBox();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.txtReportTitle = new Telerik.Reporting.TextBox();
            this.txtCompanyName = new Telerik.Reporting.TextBox();
            this.txtTransactionDate = new Telerik.Reporting.TextBox();
            this.txtTransactionNumber = new Telerik.Reporting.TextBox();
            this.txtDescription = new Telerik.Reporting.TextBox();
            this.txtDebit = new Telerik.Reporting.TextBox();
            this.txtCredit = new Telerik.Reporting.TextBox();
            this.txtPeriode = new Telerik.Reporting.TextBox();
            this.txtJournalCode = new Telerik.Reporting.TextBox();
            this.txtBalance = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.ChartOfAccountCode = new Telerik.Reporting.TextBox();
            this.ChartOfAccountName = new Telerik.Reporting.TextBox();
            this.Balance = new Telerik.Reporting.TextBox();
            this.Credit = new Telerik.Reporting.TextBox();
            this.Debit = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.Description = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.txtPageNumber = new Telerik.Reporting.TextBox();
            this.txtPrintDateTime = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // groupFooterChartOfAccount
            // 
            this.groupFooterChartOfAccount.Height = Telerik.Reporting.Drawing.Unit.Inch(0.3D);
            this.groupFooterChartOfAccount.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox5,
            this.textBox6});
            this.groupFooterChartOfAccount.Name = "groupFooterChartOfAccount";
            this.groupFooterChartOfAccount.PageBreak = Telerik.Reporting.PageBreak.After;
            this.groupFooterChartOfAccount.Style.Font.Bold = true;
            // 
            // textBox5
            // 
            this.textBox5.Format = "{0:N2}";
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox5.TextWrap = true;
            this.textBox5.Value = "=Sum(IIF(DetailId >0, Debit,0))";
            // 
            // textBox6
            // 
            this.textBox6.Format = "{0:N2}";
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox6.TextWrap = true;
            this.textBox6.Value = "=Sum(IIF(Description_Detail = \'INITIAL BALANCE\', 0,Credit))";
            // 
            // groupHeaderChartOfAccount
            // 
            this.groupHeaderChartOfAccount.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2D);
            this.groupHeaderChartOfAccount.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox2,
            this.textBox3});
            this.groupHeaderChartOfAccount.Name = "groupHeaderChartOfAccount";
            this.groupHeaderChartOfAccount.PrintOnEveryPage = true;
            this.groupHeaderChartOfAccount.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.groupHeaderChartOfAccount.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.groupHeaderChartOfAccount.Style.Font.Bold = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox2.TextWrap = true;
            this.textBox2.Value = "=ChartOfAccountCode";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.8D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox3.TextWrap = true;
            this.textBox3.Value = "=ChartOfAccountName";
            // 
            // groupFooterSubLedger
            // 
            formattingRule1.Filters.Add(new Telerik.Reporting.Filter("=Sum(Credit)", Telerik.Reporting.FilterOperator.Equal, "0"));
            formattingRule1.Filters.Add(new Telerik.Reporting.Filter("=Sum(Debit)", Telerik.Reporting.FilterOperator.Equal, "0"));
            formattingRule1.Style.Visible = false;
            this.groupFooterSubLedger.ConditionalFormatting.AddRange(new Telerik.Reporting.Drawing.FormattingRule[] {
            formattingRule1});
            this.groupFooterSubLedger.Height = Telerik.Reporting.Drawing.Unit.Inch(0.3D);
            this.groupFooterSubLedger.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox4,
            this.textBox7});
            this.groupFooterSubLedger.Name = "groupFooterSubLedger";
            this.groupFooterSubLedger.Style.Visible = true;
            // 
            // textBox4
            // 
            this.textBox4.Format = "{0:N2}";
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox4.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.textBox4.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox4.TextWrap = true;
            this.textBox4.Value = "=Sum(IIF(Description_Detail = \'INITIAL BALANCE\', 0,Credit))";
            // 
            // textBox7
            // 
            this.textBox7.Format = "{0:N2}";
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox7.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.textBox7.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox7.TextWrap = true;
            this.textBox7.Value = "=Sum(IIF(DetailId >0, Debit,0))";
            // 
            // groupHeaderSectionSubLedger
            // 
            formattingRule2.Filters.Add(new Telerik.Reporting.Filter("=Sum(Credit)", Telerik.Reporting.FilterOperator.Equal, "0"));
            formattingRule2.Filters.Add(new Telerik.Reporting.Filter("=Sum(Debit)", Telerik.Reporting.FilterOperator.Equal, "0"));
            formattingRule2.Style.Visible = false;
            this.groupHeaderSectionSubLedger.ConditionalFormatting.AddRange(new Telerik.Reporting.Drawing.FormattingRule[] {
            formattingRule2});
            this.groupHeaderSectionSubLedger.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2D);
            this.groupHeaderSectionSubLedger.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.SubLedgerDescription,
            this.SubLedgerName});
            this.groupHeaderSectionSubLedger.Name = "groupHeaderSectionSubLedger";
            this.groupHeaderSectionSubLedger.Style.Font.Bold = true;
            // 
            // SubLedgerDescription
            // 
            this.SubLedgerDescription.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.4D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.SubLedgerDescription.Name = "SubLedgerDescription";
            this.SubLedgerDescription.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.6D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.SubLedgerDescription.TextWrap = true;
            this.SubLedgerDescription.Value = "=subledger_description";
            // 
            // SubLedgerName
            // 
            this.SubLedgerName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.8D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.SubLedgerName.Name = "SubLedgerName";
            this.SubLedgerName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.6D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.SubLedgerName.TextWrap = true;
            this.SubLedgerName.Value = "=SubLedgerName";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(1.2D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtReportTitle,
            this.txtCompanyName,
            this.txtTransactionDate,
            this.txtTransactionNumber,
            this.txtDescription,
            this.txtDebit,
            this.txtCredit,
            this.txtPeriode,
            this.txtJournalCode,
            this.txtBalance});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.pageHeader.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.pageHeader.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            // 
            // txtReportTitle
            // 
            this.txtReportTitle.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.txtReportTitle.Name = "txtReportTitle";
            this.txtReportTitle.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtReportTitle.Style.BorderColor.Bottom = System.Drawing.Color.Blue;
            this.txtReportTitle.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(119)))), ((int)(((byte)(171)))));
            this.txtReportTitle.Style.Font.Bold = true;
            this.txtReportTitle.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.txtReportTitle.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtReportTitle.Value = "SUB LEDGER REPORT";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.3D));
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtCompanyName.Value = "";
            // 
            // txtTransactionDate
            // 
            this.txtTransactionDate.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.8D));
            this.txtTransactionDate.Name = "txtTransactionDate";
            this.txtTransactionDate.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8D), Telerik.Reporting.Drawing.Unit.Inch(0.4D));
            this.txtTransactionDate.Style.Color = System.Drawing.Color.Black;
            this.txtTransactionDate.Style.Font.Bold = true;
            this.txtTransactionDate.Value = "Date";
            // 
            // txtTransactionNumber
            // 
            this.txtTransactionNumber.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5D), Telerik.Reporting.Drawing.Unit.Inch(0.8D));
            this.txtTransactionNumber.Name = "txtTransactionNumber";
            this.txtTransactionNumber.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.9D), Telerik.Reporting.Drawing.Unit.Inch(0.4D));
            this.txtTransactionNumber.Style.Color = System.Drawing.Color.Black;
            this.txtTransactionNumber.Style.Font.Bold = true;
            this.txtTransactionNumber.Value = "Transaction Number";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.4D), Telerik.Reporting.Drawing.Unit.Inch(0.8D));
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.6D), Telerik.Reporting.Drawing.Unit.Inch(0.4D));
            this.txtDescription.Style.Color = System.Drawing.Color.Black;
            this.txtDescription.Style.Font.Bold = true;
            this.txtDescription.Value = "Description";
            // 
            // txtDebit
            // 
            this.txtDebit.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5D), Telerik.Reporting.Drawing.Unit.Inch(0.8D));
            this.txtDebit.Name = "txtDebit";
            this.txtDebit.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtDebit.Style.Color = System.Drawing.Color.Black;
            this.txtDebit.Style.Font.Bold = true;
            this.txtDebit.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtDebit.Value = "Debit";
            // 
            // txtCredit
            // 
            this.txtCredit.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6D), Telerik.Reporting.Drawing.Unit.Inch(0.8D));
            this.txtCredit.Name = "txtCredit";
            this.txtCredit.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtCredit.Style.Color = System.Drawing.Color.Black;
            this.txtCredit.Style.Font.Bold = true;
            this.txtCredit.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtCredit.Value = "Credit";
            // 
            // txtPeriode
            // 
            this.txtPeriode.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.txtPeriode.Name = "txtPeriode";
            this.txtPeriode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtPeriode.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtPeriode.Value = "";
            // 
            // txtJournalCode
            // 
            this.txtJournalCode.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.8D), Telerik.Reporting.Drawing.Unit.Inch(0.8D));
            this.txtJournalCode.Name = "txtJournalCode";
            this.txtJournalCode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.7D), Telerik.Reporting.Drawing.Unit.Inch(0.4D));
            this.txtJournalCode.Style.Color = System.Drawing.Color.Black;
            this.txtJournalCode.Style.Font.Bold = true;
            this.txtJournalCode.Value = "Journal\r\nCode";
            // 
            // txtBalance
            // 
            this.txtBalance.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7D), Telerik.Reporting.Drawing.Unit.Inch(0.8D));
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtBalance.Style.Color = System.Drawing.Color.Black;
            this.txtBalance.Style.Font.Bold = true;
            this.txtBalance.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtBalance.Value = "Balance";
            // 
            // detail
            // 
            formattingRule3.Filters.Add(new Telerik.Reporting.Filter("=Sum(Credit)", Telerik.Reporting.FilterOperator.Equal, "0"));
            formattingRule3.Filters.Add(new Telerik.Reporting.Filter("=Sum(Debit)", Telerik.Reporting.FilterOperator.Equal, "0"));
            formattingRule3.Style.Visible = false;
            this.detail.ConditionalFormatting.AddRange(new Telerik.Reporting.Drawing.FormattingRule[] {
            formattingRule3});
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.ChartOfAccountCode,
            this.ChartOfAccountName,
            this.Balance,
            this.Credit,
            this.Debit,
            this.textBox1,
            this.Description});
            this.detail.Name = "detail";
            // 
            // ChartOfAccountCode
            // 
            this.ChartOfAccountCode.Format = "{0:dd-MM-yyyy}";
            this.ChartOfAccountCode.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.ChartOfAccountCode.Name = "ChartOfAccountCode";
            this.ChartOfAccountCode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.ChartOfAccountCode.TextWrap = true;
            this.ChartOfAccountCode.Value = "=TransactionDate";
            // 
            // ChartOfAccountName
            // 
            this.ChartOfAccountName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.8D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.ChartOfAccountName.Name = "ChartOfAccountName";
            this.ChartOfAccountName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.7D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.ChartOfAccountName.TextWrap = true;
            this.ChartOfAccountName.Value = "=JournalCode";
            // 
            // Balance
            // 
            this.Balance.Format = "{0:N2}";
            this.Balance.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.Balance.Name = "Balance";
            this.Balance.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.Balance.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.Balance.TextWrap = true;
            this.Balance.Value = "= RunningValue(\'groupSubLedger\', Sum(Temiang.Avicenna.ReportLibrary.Utility.Calcu" +
    "lateBalance(NormalBalance,Debit,Credit)))";
            // 
            // Credit
            // 
            this.Credit.Format = "{0:N2}";
            this.Credit.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.Credit.Name = "Credit";
            this.Credit.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.Credit.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.Credit.TextWrap = true;
            this.Credit.Value = "= IIF(Description_Detail = \'INITIAL BALANCE\', 0,Credit)";
            // 
            // Debit
            // 
            this.Debit.Format = "{0:N2}";
            this.Debit.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.Debit.Name = "Debit";
            this.Debit.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.Debit.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.Debit.TextWrap = true;
            this.Debit.Value = "= IIF(Description_Detail = \'INITIAL BALANCE\', 0,Debit)";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.9D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox1.TextWrap = true;
            this.textBox1.Value = "=TransactionNumber";
            // 
            // Description
            // 
            this.Description.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.4D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.Description.Name = "Description";
            this.Description.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.6D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.Description.Value = "=Description_Detail";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.4D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtPageNumber,
            this.txtPrintDateTime});
            this.pageFooter.Name = "pageFooter";
            this.pageFooter.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.pageFooter.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.pageFooter.Style.Font.Bold = true;
            this.pageFooter.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            // 
            // txtPageNumber
            // 
            this.txtPageNumber.Format = "";
            this.txtPageNumber.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.txtPageNumber.Name = "txtPageNumber";
            this.txtPageNumber.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtPageNumber.Style.Font.Bold = false;
            this.txtPageNumber.Style.Font.Italic = true;
            this.txtPageNumber.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtPageNumber.Value = "= \"Page \" + PageNumber + \" Of \" + PageCount";
            // 
            // txtPrintDateTime
            // 
            this.txtPrintDateTime.Format = "Avicenna HIS, Print Date : {0:dd-MM-yyyy HH:mm}";
            this.txtPrintDateTime.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.1D));
            this.txtPrintDateTime.Name = "txtPrintDateTime";
            this.txtPrintDateTime.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtPrintDateTime.Style.Font.Bold = false;
            this.txtPrintDateTime.Style.Font.Italic = true;
            this.txtPrintDateTime.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtPrintDateTime.Value = "= Now()";
            // 
            // SubLedgerDetailReport
            // 
            this.DocumentName = "Subledger Detail Report";
            group1.GroupFooter = this.groupFooterChartOfAccount;
            group1.GroupHeader = this.groupHeaderChartOfAccount;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("=ChartOfAccountCode"));
            group1.Name = "groupChartOfAccount";
            group2.GroupFooter = this.groupFooterSubLedger;
            group2.GroupHeader = this.groupHeaderSectionSubLedger;
            group2.Groupings.Add(new Telerik.Reporting.Grouping("=SubLedgerId"));
            group2.Name = "groupSubLedger";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderChartOfAccount,
            this.groupFooterChartOfAccount,
            this.groupHeaderSectionSubLedger,
            this.groupFooterSubLedger,
            this.pageHeader,
            this.detail,
            this.pageFooter});
            this.Name = "SubLedgerDetailReport";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(0.25D), Telerik.Reporting.Drawing.Unit.Inch(0.25D), Telerik.Reporting.Drawing.Unit.Inch(0.25D), Telerik.Reporting.Drawing.Unit.Inch(0.25D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Sortings.Add(new Telerik.Reporting.Sorting("=ChartOfAccountCode", Telerik.Reporting.SortDirection.Asc));
            this.Sortings.Add(new Telerik.Reporting.Sorting("=TransactionDate", Telerik.Reporting.SortDirection.Asc));
            this.Sortings.Add(new Telerik.Reporting.Sorting("=DetailID", Telerik.Reporting.SortDirection.Asc));
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(8D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox txtReportTitle;
        private Telerik.Reporting.TextBox txtCompanyName;
        private Telerik.Reporting.TextBox txtTransactionDate;
        private Telerik.Reporting.TextBox txtTransactionNumber;
        private Telerik.Reporting.TextBox txtDescription;
        private Telerik.Reporting.TextBox txtDebit;
        private Telerik.Reporting.TextBox txtCredit;
        private Telerik.Reporting.TextBox ChartOfAccountCode;
        private Telerik.Reporting.TextBox ChartOfAccountName;
        private Telerik.Reporting.TextBox Balance;
        private Telerik.Reporting.TextBox Credit;
        private Telerik.Reporting.TextBox Debit;
        private Telerik.Reporting.TextBox txtPeriode;
        private Telerik.Reporting.TextBox txtJournalCode;
        private Telerik.Reporting.TextBox txtBalance;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox Description;
        private Telerik.Reporting.Group groupChartOfAccount;
        private Telerik.Reporting.GroupFooterSection groupFooterChartOfAccount;
        private Telerik.Reporting.GroupHeaderSection groupHeaderChartOfAccount;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.Group groupSubLedger;
        private Telerik.Reporting.GroupFooterSection groupFooterSubLedger;
        private Telerik.Reporting.GroupHeaderSection groupHeaderSectionSubLedger;
        private Telerik.Reporting.TextBox SubLedgerDescription;
        private Telerik.Reporting.TextBox SubLedgerName;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox txtPageNumber;
        private Telerik.Reporting.TextBox txtPrintDateTime;
    }
}