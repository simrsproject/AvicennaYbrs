namespace Temiang.Avicenna.ReportLibrary.Accounting.Ledgers.RSMM
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class LedgerDetailByDateReport
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.txtBalance = new Telerik.Reporting.TextBox();
            this.txtJournalCode = new Telerik.Reporting.TextBox();
            this.txtPeriode = new Telerik.Reporting.TextBox();
            this.txtCredit = new Telerik.Reporting.TextBox();
            this.txtDebit = new Telerik.Reporting.TextBox();
            this.txtDescription = new Telerik.Reporting.TextBox();
            this.txtTransactionDate = new Telerik.Reporting.TextBox();
            this.txtCompanyName = new Telerik.Reporting.TextBox();
            this.txtReportTitle = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.Description = new Telerik.Reporting.TextBox();
            this.Debit = new Telerik.Reporting.TextBox();
            this.Credit = new Telerik.Reporting.TextBox();
            this.Balance = new Telerik.Reporting.TextBox();
            this.ChartOfAccountName = new Telerik.Reporting.TextBox();
            this.ChartOfAccountCode = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.txtPrintDateTime = new Telerik.Reporting.TextBox();
            this.txtPageNumber = new Telerik.Reporting.TextBox();
            this.groupChartOfAccount = new Telerik.Reporting.Group();
            this.groupFooterChartOfAccount = new Telerik.Reporting.GroupFooterSection();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.groupHeaderChartOfAccount = new Telerik.Reporting.GroupHeaderSection();
            this.textBox2 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(1.6999999284744263);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtBalance,
            this.txtJournalCode,
            this.txtPeriode,
            this.txtCredit,
            this.txtDebit,
            this.txtDescription,
            this.txtTransactionDate,
            this.txtCompanyName,
            this.txtReportTitle});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.pageHeader.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1);
            // 
            // txtBalance
            // 
            this.txtBalance.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.9000000953674316), Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421));
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.89976263046264648), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtBalance.Style.Color = System.Drawing.Color.Black;
            this.txtBalance.Style.Font.Bold = true;
            this.txtBalance.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.txtBalance.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtBalance.Value = "Balance";
            // 
            // txtJournalCode
            // 
            this.txtJournalCode.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.80003958940505981), Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421));
            this.txtJournalCode.Name = "txtJournalCode";
            this.txtJournalCode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1998815536499023), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtJournalCode.Style.Color = System.Drawing.Color.Black;
            this.txtJournalCode.Style.Font.Bold = true;
            this.txtJournalCode.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.txtJournalCode.Value = "Journal Code";
            // 
            // txtPeriode
            // 
            this.txtPeriode.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2), Telerik.Reporting.Drawing.Unit.Inch(1.0001574754714966));
            this.txtPeriode.Name = "txtPeriode";
            this.txtPeriode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.9001576900482178), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtPeriode.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtPeriode.Value = "";
            // 
            // txtCredit
            // 
            this.txtCredit.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.9000000953674316), Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421));
            this.txtCredit.Name = "txtCredit";
            this.txtCredit.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99991989135742188), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtCredit.Style.Color = System.Drawing.Color.Black;
            this.txtCredit.Style.Font.Bold = true;
            this.txtCredit.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.txtCredit.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtCredit.Value = "Credit";
            // 
            // txtDebit
            // 
            this.txtDebit.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.8000006675720215), Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421));
            this.txtDebit.Name = "txtDebit";
            this.txtDebit.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0999997854232788), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtDebit.Style.Color = System.Drawing.Color.Black;
            this.txtDebit.Style.Font.Bold = true;
            this.txtDebit.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.txtDebit.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtDebit.Value = "Debit";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2), Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421));
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.8000004291534424), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtDescription.Style.Color = System.Drawing.Color.Black;
            this.txtDescription.Style.Font.Bold = true;
            this.txtDescription.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.txtDescription.Value = "Description";
            // 
            // txtTransactionDate
            // 
            this.txtTransactionDate.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9339065551757812E-05), Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421));
            this.txtTransactionDate.Name = "txtTransactionDate";
            this.txtTransactionDate.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.79992133378982544), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtTransactionDate.Style.Color = System.Drawing.Color.Black;
            this.txtTransactionDate.Style.Font.Bold = true;
            this.txtTransactionDate.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.txtTransactionDate.Value = "Date";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.800078809261322));
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.7999191284179688), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtCompanyName.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtCompanyName.Value = "";
            // 
            // txtReportTitle
            // 
            this.txtReportTitle.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.59999996423721313));
            this.txtReportTitle.Name = "txtReportTitle";
            this.txtReportTitle.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.7999978065490723), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtReportTitle.Style.BorderColor.Bottom = System.Drawing.Color.Blue;
            this.txtReportTitle.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(119)))), ((int)(((byte)(171)))));
            this.txtReportTitle.Style.Font.Bold = true;
            this.txtReportTitle.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.txtReportTitle.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtReportTitle.Value = "LEDGER DETAIL REPORT";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20011837780475617);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.Description,
            this.Debit,
            this.Credit,
            this.Balance,
            this.ChartOfAccountName,
            this.ChartOfAccountCode});
            this.detail.Name = "detail";
            // 
            // Description
            // 
            this.Description.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.0001180171966553), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.Description.Name = "Description";
            this.Description.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.8000004291534424), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.Description.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.Description.Value = "=Description_Detail";
            // 
            // Debit
            // 
            this.Debit.Format = "{0:N2}";
            this.Debit.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.8001580238342285), Telerik.Reporting.Drawing.Unit.Inch(0.000118255615234375));
            this.Debit.Name = "Debit";
            this.Debit.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0999608039855957), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.Debit.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.Debit.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.Debit.TextWrap = true;
            this.Debit.Value = "=Debit";
            // 
            // Credit
            // 
            this.Credit.Format = "{0:N2}";
            this.Credit.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.9000401496887207), Telerik.Reporting.Drawing.Unit.Inch(0.000118255615234375));
            this.Credit.Name = "Credit";
            this.Credit.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99984121322631836), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.Credit.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.Credit.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.Credit.TextWrap = true;
            this.Credit.Value = "=Credit";
            // 
            // Balance
            // 
            this.Balance.Format = "{0:N2}";
            this.Balance.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.9000382423400879), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.Balance.Name = "Balance";
            this.Balance.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8999599814414978), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.Balance.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.Balance.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.Balance.TextWrap = true;
            this.Balance.Value = "= RunningValue(\'group1\', Sum(Temiang.Avicenna.ReportLibrary.Utility.CalculateBala" +
                "nce(NormalBalance,Debit,Credit)))";
            // 
            // ChartOfAccountName
            // 
            this.ChartOfAccountName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.80003947019577026), Telerik.Reporting.Drawing.Unit.Inch(0.000118255615234375));
            this.ChartOfAccountName.Name = "ChartOfAccountName";
            this.ChartOfAccountName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1999996900558472), Telerik.Reporting.Drawing.Unit.Inch(0.19996070861816406));
            this.ChartOfAccountName.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.ChartOfAccountName.TextWrap = true;
            this.ChartOfAccountName.Value = "=JournalCode";
            // 
            // ChartOfAccountCode
            // 
            this.ChartOfAccountCode.Format = "{0:dd-MM-yyyy}";
            this.ChartOfAccountCode.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(7.8837074397597462E-05));
            this.ChartOfAccountCode.Name = "ChartOfAccountCode";
            this.ChartOfAccountCode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.79988193511962891), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.ChartOfAccountCode.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.ChartOfAccountCode.TextWrap = true;
            this.ChartOfAccountCode.Value = "=TransactionDate";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.3999999463558197);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtPrintDateTime,
            this.txtPageNumber});
            this.pageFooter.Name = "pageFooter";
            this.pageFooter.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.pageFooter.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1);
            // 
            // txtPrintDateTime
            // 
            this.txtPrintDateTime.Format = "Avicenna HIS, Print Date : {0:dd-MM-yyyy HH:mm}";
            this.txtPrintDateTime.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609));
            this.txtPrintDateTime.Name = "txtPrintDateTime";
            this.txtPrintDateTime.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.7999997138977051), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtPrintDateTime.Style.Font.Italic = true;
            this.txtPrintDateTime.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.txtPrintDateTime.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtPrintDateTime.Value = "= Now()";
            // 
            // txtPageNumber
            // 
            this.txtPageNumber.Format = "";
            this.txtPageNumber.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.8001971244812012), Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609));
            this.txtPageNumber.Name = "txtPageNumber";
            this.txtPageNumber.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.9995660781860352), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtPageNumber.Style.Font.Italic = true;
            this.txtPageNumber.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.txtPageNumber.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtPageNumber.Value = "= \"Page \" + PageNumber + \" Of \" + PageCount";
            // 
            // groupChartOfAccount
            // 
            this.groupChartOfAccount.GroupFooter = this.groupFooterChartOfAccount;
            this.groupChartOfAccount.GroupHeader = this.groupHeaderChartOfAccount;
            this.groupChartOfAccount.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("=ChartOfAccountCode")});
            this.groupChartOfAccount.Name = "group1";
            // 
            // groupFooterChartOfAccount
            // 
            this.groupFooterChartOfAccount.Height = Telerik.Reporting.Drawing.Unit.Inch(0.29984202980995178);
            this.groupFooterChartOfAccount.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox6,
            this.textBox5});
            this.groupFooterChartOfAccount.Name = "groupFooterChartOfAccount";
            this.groupFooterChartOfAccount.PageBreak = Telerik.Reporting.PageBreak.After;
            // 
            // textBox6
            // 
            this.textBox6.Format = "{0:N2}";
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.900118350982666), Telerik.Reporting.Drawing.Unit.Inch(7.8837074397597462E-05));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99984121322631836), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox6.TextWrap = true;
            this.textBox6.Value = "=Sum(Credit)";
            // 
            // textBox5
            // 
            this.textBox5.Format = "{0:N2}";
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.8001976013183594), Telerik.Reporting.Drawing.Unit.Inch(7.8837074397597462E-05));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0999608039855957), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox5.TextWrap = true;
            this.textBox5.Value = "=Sum(Debit)";
            // 
            // groupHeaderChartOfAccount
            // 
            this.groupHeaderChartOfAccount.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20003955066204071);
            this.groupHeaderChartOfAccount.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox2});
            this.groupHeaderChartOfAccount.Name = "groupHeaderChartOfAccount";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.8000001907348633), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox2.TextWrap = true;
            this.textBox2.Value = "=ChartOfAccountCode + \' \' + ChartOfAccountName";
            // 
            // LedgerDetailByDateReport
            // 
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            this.groupChartOfAccount});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderChartOfAccount,
            this.groupFooterChartOfAccount,
            this.pageHeader,
            this.detail,
            this.pageFooter});
            this.Name = "LedgerDetailByDateReport";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Sortings.AddRange(new Telerik.Reporting.Sorting[] {
            new Telerik.Reporting.Sorting("=ChartOfAccountCode", Telerik.Reporting.SortDirection.Asc),
            new Telerik.Reporting.Sorting("=TransactionDate", Telerik.Reporting.SortDirection.Asc),
            new Telerik.Reporting.Sorting("=DetailID", Telerik.Reporting.SortDirection.Asc)});
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.7999978065490723);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Group groupChartOfAccount;
        private GroupFooterSection groupFooterChartOfAccount;
        private GroupHeaderSection groupHeaderChartOfAccount;
        private Telerik.Reporting.TextBox txtBalance;
        private Telerik.Reporting.TextBox txtJournalCode;
        private Telerik.Reporting.TextBox txtPeriode;
        private Telerik.Reporting.TextBox txtCredit;
        private Telerik.Reporting.TextBox txtDebit;
        private Telerik.Reporting.TextBox txtDescription;
        private Telerik.Reporting.TextBox txtTransactionDate;
        private Telerik.Reporting.TextBox txtCompanyName;
        private Telerik.Reporting.TextBox txtReportTitle;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox Description;
        private Telerik.Reporting.TextBox Debit;
        private Telerik.Reporting.TextBox Credit;
        private Telerik.Reporting.TextBox Balance;
        private Telerik.Reporting.TextBox ChartOfAccountName;
        private Telerik.Reporting.TextBox ChartOfAccountCode;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox txtPrintDateTime;
        private Telerik.Reporting.TextBox txtPageNumber;
    }
}