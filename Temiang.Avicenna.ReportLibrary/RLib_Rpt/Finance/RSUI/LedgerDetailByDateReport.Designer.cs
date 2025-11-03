namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Finance.RSUI
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
            this.txtTransactionNumber = new Telerik.Reporting.TextBox();
            this.txtTransactionDate = new Telerik.Reporting.TextBox();
            this.txtCompanyName = new Telerik.Reporting.TextBox();
            this.txtReportTitle = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.Description = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
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
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtBalance,
            this.txtJournalCode,
            this.txtPeriode,
            this.txtCredit,
            this.txtDebit,
            this.txtDescription,
            this.txtTransactionNumber,
            this.txtTransactionDate,
            this.txtCompanyName,
            this.txtReportTitle,
            this.textBox4});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.pageHeader.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1);
            // 
            // txtBalance
            // 
            this.txtBalance.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.0000786781311035), Telerik.Reporting.Drawing.Unit.Inch(0.70000004768371582));
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99976330995559692), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtBalance.Style.Color = System.Drawing.Color.Black;
            this.txtBalance.Style.Font.Bold = true;
            this.txtBalance.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.txtBalance.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtBalance.Value = "Balance";
            // 
            // txtJournalCode
            // 
            this.txtJournalCode.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.80003947019577026), Telerik.Reporting.Drawing.Unit.Inch(0.70000004768371582));
            this.txtJournalCode.Name = "txtJournalCode";
            this.txtJournalCode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.69988173246383667), Telerik.Reporting.Drawing.Unit.Inch(0.3999999463558197));
            this.txtJournalCode.Style.Color = System.Drawing.Color.Black;
            this.txtJournalCode.Style.Font.Bold = true;
            this.txtJournalCode.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.txtJournalCode.Value = "Journal\r\nCode";
            // 
            // txtPeriode
            // 
            this.txtPeriode.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6), Telerik.Reporting.Drawing.Unit.Inch(0.14791671931743622));
            this.txtPeriode.Name = "txtPeriode";
            this.txtPeriode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.9998422861099243), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtPeriode.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtPeriode.Value = "";
            // 
            // txtCredit
            // 
            this.txtCredit.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6), Telerik.Reporting.Drawing.Unit.Inch(0.70000004768371582));
            this.txtCredit.Name = "txtCredit";
            this.txtCredit.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99999982118606567), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtCredit.Style.Color = System.Drawing.Color.Black;
            this.txtCredit.Style.Font.Bold = true;
            this.txtCredit.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.txtCredit.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtCredit.Value = "Credit";
            // 
            // txtDebit
            // 
            this.txtDebit.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.9999213218688965), Telerik.Reporting.Drawing.Unit.Inch(0.70000004768371582));
            this.txtDebit.Name = "txtDebit";
            this.txtDebit.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99992102384567261), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtDebit.Style.Color = System.Drawing.Color.Black;
            this.txtDebit.Style.Font.Bold = true;
            this.txtDebit.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.txtDebit.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtDebit.Value = "Debit";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.4000003337860107), Telerik.Reporting.Drawing.Unit.Inch(0.70000004768371582));
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.5998420715332031), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtDescription.Style.Color = System.Drawing.Color.Black;
            this.txtDescription.Style.Font.Bold = true;
            this.txtDescription.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.txtDescription.Value = "Description";
            // 
            // txtTransactionNumber
            // 
            this.txtTransactionNumber.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5), Telerik.Reporting.Drawing.Unit.Inch(0.70000004768371582));
            this.txtTransactionNumber.Name = "txtTransactionNumber";
            this.txtTransactionNumber.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8999212384223938), Telerik.Reporting.Drawing.Unit.Inch(0.3999999463558197));
            this.txtTransactionNumber.Style.Color = System.Drawing.Color.Black;
            this.txtTransactionNumber.Style.Font.Bold = true;
            this.txtTransactionNumber.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.txtTransactionNumber.Value = "Transaction Number";
            // 
            // txtTransactionDate
            // 
            this.txtTransactionDate.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9339065551757812E-05), Telerik.Reporting.Drawing.Unit.Inch(0.70000004768371582));
            this.txtTransactionDate.Name = "txtTransactionDate";
            this.txtTransactionDate.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.79992133378982544), Telerik.Reporting.Drawing.Unit.Inch(0.3999999463558197));
            this.txtTransactionDate.Style.Color = System.Drawing.Color.Black;
            this.txtTransactionDate.Style.Font.Bold = true;
            this.txtTransactionDate.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.txtTransactionDate.Value = "Date";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.4000003337860107), Telerik.Reporting.Drawing.Unit.Inch(0.34799551963806152));
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.5998420715332031), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtCompanyName.Value = "";
            // 
            // txtReportTitle
            // 
            this.txtReportTitle.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.4000003337860107), Telerik.Reporting.Drawing.Unit.Inch(0.14791671931743622));
            this.txtReportTitle.Name = "txtReportTitle";
            this.txtReportTitle.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.5999209880828857), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtReportTitle.Style.BorderColor.Bottom = System.Drawing.Color.Blue;
            this.txtReportTitle.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(119)))), ((int)(((byte)(171)))));
            this.txtReportTitle.Style.Font.Bold = true;
            this.txtReportTitle.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.txtReportTitle.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtReportTitle.Value = "LEDGER DETAIL REPORT";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20011837780475617);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.Description,
            this.textBox1,
            this.Debit,
            this.Credit,
            this.Balance,
            this.ChartOfAccountName,
            this.ChartOfAccountCode,
            this.textBox7});
            this.detail.Name = "detail";
            // 
            // Description
            // 
            this.Description.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.4001181125640869), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.Description.Name = "Description";
            this.Description.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.5998420715332031), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.Description.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.Description.Value = "=Description_Detail";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5001181364059448), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8999212384223938), Telerik.Reporting.Drawing.Unit.Inch(0.19996070861816406));
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox1.TextWrap = true;
            this.textBox1.Value = "=TransactionNumber";
            // 
            // Debit
            // 
            this.Debit.Format = "{0:N2}";
            this.Debit.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.0000395774841309), Telerik.Reporting.Drawing.Unit.Inch(0.000118255615234375));
            this.Debit.Name = "Debit";
            this.Debit.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.9998820424079895), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.Debit.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.Debit.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.Debit.TextWrap = true;
            this.Debit.Value = "=Debit";
            // 
            // Credit
            // 
            this.Credit.Format = "{0:N2}";
            this.Credit.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6), Telerik.Reporting.Drawing.Unit.Inch(0.000118255615234375));
            this.Credit.Name = "Credit";
            this.Credit.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99992114305496216), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.Credit.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.Credit.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.Credit.TextWrap = true;
            this.Credit.Value = "=Credit";
            // 
            // Balance
            // 
            this.Balance.Format = "{0:N2}";
            this.Balance.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.0000786781311035), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.Balance.Name = "Balance";
            this.Balance.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99996060132980347), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
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
            this.ChartOfAccountName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.69999980926513672), Telerik.Reporting.Drawing.Unit.Inch(0.19996070861816406));
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
            this.txtPrintDateTime.Format = "{0:F}";
            this.txtPrintDateTime.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609));
            this.txtPrintDateTime.Name = "txtPrintDateTime";
            this.txtPrintDateTime.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.3000001907348633), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtPrintDateTime.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.txtPrintDateTime.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtPrintDateTime.Value = "= Now()";
            // 
            // txtPageNumber
            // 
            this.txtPageNumber.Format = "";
            this.txtPageNumber.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.5999999046325684), Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609));
            this.txtPageNumber.Name = "txtPageNumber";
            this.txtPageNumber.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.2999608516693115), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
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
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.0000786781311035), Telerik.Reporting.Drawing.Unit.Inch(7.8837074397597462E-05));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99992114305496216), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox6.TextWrap = true;
            this.textBox6.Value = "=Sum(Credit)";
            // 
            // textBox5
            // 
            this.textBox5.Format = "{0:N2}";
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.0000786781311035), Telerik.Reporting.Drawing.Unit.Inch(7.8837074397597462E-05));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.9998820424079895), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox5.TextWrap = true;
            this.textBox5.Value = "=Sum(Debit)";
            // 
            // groupHeaderChartOfAccount
            // 
            this.groupHeaderChartOfAccount.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20003955066204071);
            this.groupHeaderChartOfAccount.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox3,
            this.textBox2});
            this.groupHeaderChartOfAccount.Name = "groupHeaderChartOfAccount";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.9998424053192139), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox3.TextWrap = true;
            this.textBox3.Value = "=ChartOfAccountName";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99996060132980347), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox2.TextWrap = true;
            this.textBox2.Value = "=ChartOfAccountCode";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8), Telerik.Reporting.Drawing.Unit.Inch(0.7000001072883606));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99976330995559692), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox4.Style.Color = System.Drawing.Color.Black;
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox4.Value = "Unit";
            // 
            // textBox7
            // 
            this.textBox7.Format = "{0:N2}";
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.0001182556152344), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.9998820424079895), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox7.TextWrap = true;
            this.textBox7.Value = "=subledger_description";
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
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Inch(0.25);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Inch(0.25);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Inch(0.25);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Inch(0.25);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageSettings.PaperSize = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(9.5), Telerik.Reporting.Drawing.Unit.Inch(11));
            this.Sortings.AddRange(new Telerik.Reporting.Sorting[] {
            new Telerik.Reporting.Sorting("=ChartOfAccountCode", Telerik.Reporting.SortDirection.Asc),
            new Telerik.Reporting.Sorting("=TransactionDate", Telerik.Reporting.SortDirection.Asc),
            new Telerik.Reporting.Sorting("=DetailID", Telerik.Reporting.SortDirection.Asc)});
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(9.0000009536743164);
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
        private Telerik.Reporting.TextBox txtTransactionNumber;
        private Telerik.Reporting.TextBox txtTransactionDate;
        private Telerik.Reporting.TextBox txtCompanyName;
        private Telerik.Reporting.TextBox txtReportTitle;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox Description;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox Debit;
        private Telerik.Reporting.TextBox Credit;
        private Telerik.Reporting.TextBox Balance;
        private Telerik.Reporting.TextBox ChartOfAccountName;
        private Telerik.Reporting.TextBox ChartOfAccountCode;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox txtPrintDateTime;
        private Telerik.Reporting.TextBox txtPageNumber;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox7;
    }
}