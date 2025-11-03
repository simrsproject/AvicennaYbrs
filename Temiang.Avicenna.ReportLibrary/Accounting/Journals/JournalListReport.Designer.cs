namespace Temiang.Avicenna.ReportLibrary.Accounting.Journals
{
    partial class JournalListReport
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.txtReportTitle = new Telerik.Reporting.TextBox();
            this.txtCompanyName = new Telerik.Reporting.TextBox();
            this.txtTransactionDate = new Telerik.Reporting.TextBox();
            this.txtTransactionNumber = new Telerik.Reporting.TextBox();
            this.txtDescription = new Telerik.Reporting.TextBox();
            this.txtDebit = new Telerik.Reporting.TextBox();
            this.txtCredit = new Telerik.Reporting.TextBox();
            this.txtAccountCode = new Telerik.Reporting.TextBox();
            this.txtAccountName = new Telerik.Reporting.TextBox();
            this.txtSubLedger = new Telerik.Reporting.TextBox();
            this.txtDocumentNumber = new Telerik.Reporting.TextBox();
            this.txtPeriode = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.ChartOfAccountCode = new Telerik.Reporting.TextBox();
            this.ChartOfAccountName = new Telerik.Reporting.TextBox();
            this.SubLedgerName = new Telerik.Reporting.TextBox();
            this.Credit = new Telerik.Reporting.TextBox();
            this.Debit = new Telerik.Reporting.TextBox();
            this.DocumentNumber = new Telerik.Reporting.TextBox();
            this.TransactionNumber = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.txtPageNumber = new Telerik.Reporting.TextBox();
            this.txtPrintDateTime = new Telerik.Reporting.TextBox();
            this.Description = new Telerik.Reporting.TextBox();
            this.TransactionDate = new Telerik.Reporting.TextBox();
            this.JournalIdGroup = new Telerik.Reporting.Group();
            this.groupFooterJournalId = new Telerik.Reporting.GroupFooterSection();
            this.TotalDebit = new Telerik.Reporting.TextBox();
            this.TotalCredit = new Telerik.Reporting.TextBox();
            this.groupHeaderJournalId = new Telerik.Reporting.GroupHeaderSection();
            this.JournalTypeGroup = new Telerik.Reporting.Group();
            this.groupFooterJournalType = new Telerik.Reporting.GroupFooterSection();
            this.groupHeaderJournalType = new Telerik.Reporting.GroupHeaderSection();
            this.JournalType = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(1.4000000953674316);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtReportTitle,
            this.txtCompanyName,
            this.txtTransactionDate,
            this.txtTransactionNumber,
            this.txtDescription,
            this.txtDebit,
            this.txtCredit,
            this.txtAccountCode,
            this.txtAccountName,
            this.txtSubLedger,
            this.txtDocumentNumber,
            this.txtPeriode});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.pageHeader.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.pageHeader.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1);
            // 
            // txtReportTitle
            // 
            this.txtReportTitle.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.099999986588954926));
            this.txtReportTitle.Name = "txtReportTitle";
            this.txtReportTitle.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.9999213218688965), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtReportTitle.Style.BorderColor.Bottom = System.Drawing.Color.Blue;
            this.txtReportTitle.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(119)))), ((int)(((byte)(171)))));
            this.txtReportTitle.Style.Font.Bold = true;
            this.txtReportTitle.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.txtReportTitle.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtReportTitle.Value = "JOURNAL TRANSACTION REPORT";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134));
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.9998421669006348), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtCompanyName.Value = "";
            // 
            // txtTransactionDate
            // 
            this.txtTransactionDate.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929));
            this.txtTransactionDate.Name = "txtTransactionDate";
            this.txtTransactionDate.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.79992133378982544), Telerik.Reporting.Drawing.Unit.Inch(0.3999999463558197));
            this.txtTransactionDate.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(160)))));
            this.txtTransactionDate.Style.Font.Bold = true;
            this.txtTransactionDate.Value = "Date";
            // 
            // txtTransactionNumber
            // 
            this.txtTransactionNumber.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929), Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929));
            this.txtTransactionNumber.Name = "txtTransactionNumber";
            this.txtTransactionNumber.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.8999212384223938), Telerik.Reporting.Drawing.Unit.Inch(0.3999999463558197));
            this.txtTransactionNumber.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(160)))));
            this.txtTransactionNumber.Style.Font.Bold = true;
            this.txtTransactionNumber.Value = "Transaction Number";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.7000001668930054), Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929));
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.2998814582824707), Telerik.Reporting.Drawing.Unit.Inch(0.3999999463558197));
            this.txtDescription.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(160)))));
            this.txtDescription.Style.Font.Bold = true;
            this.txtDescription.Value = "Description";
            // 
            // txtDebit
            // 
            this.txtDebit.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.9999604225158691), Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929));
            this.txtDebit.Name = "txtDebit";
            this.txtDebit.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99992102384567261), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtDebit.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(160)))));
            this.txtDebit.Style.Font.Bold = true;
            this.txtDebit.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtDebit.Value = "Debit";
            // 
            // txtCredit
            // 
            this.txtCredit.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.0000395774841309), Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929));
            this.txtCredit.Name = "txtCredit";
            this.txtCredit.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99999982118606567), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtCredit.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(160)))));
            this.txtCredit.Style.Font.Bold = true;
            this.txtCredit.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtCredit.Value = "Credit";
            // 
            // txtAccountCode
            // 
            this.txtAccountCode.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(1.2000788450241089));
            this.txtAccountCode.Name = "txtAccountCode";
            this.txtAccountCode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.79992133378982544), Telerik.Reporting.Drawing.Unit.Inch(0.19992129504680634));
            this.txtAccountCode.Style.Color = System.Drawing.Color.Black;
            this.txtAccountCode.Style.Font.Bold = true;
            this.txtAccountCode.Value = "Account";
            // 
            // txtAccountName
            // 
            this.txtAccountName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929), Telerik.Reporting.Drawing.Unit.Inch(1.2000788450241089));
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.2000000476837158), Telerik.Reporting.Drawing.Unit.Inch(0.19992129504680634));
            this.txtAccountName.Style.Color = System.Drawing.Color.Black;
            this.txtAccountName.Style.Font.Bold = true;
            this.txtAccountName.Value = "Account Name";
            // 
            // txtSubLedger
            // 
            this.txtSubLedger.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.0000786781311035), Telerik.Reporting.Drawing.Unit.Inch(1.2000788450241089));
            this.txtSubLedger.Name = "txtSubLedger";
            this.txtSubLedger.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99984198808670044), Telerik.Reporting.Drawing.Unit.Inch(0.19992129504680634));
            this.txtSubLedger.Style.Color = System.Drawing.Color.Black;
            this.txtSubLedger.Style.Font.Bold = true;
            this.txtSubLedger.Value = "Subledger";
            // 
            // txtDocumentNumber
            // 
            this.txtDocumentNumber.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.0001182556152344), Telerik.Reporting.Drawing.Unit.Inch(1.2000788450241089));
            this.txtDocumentNumber.Name = "txtDocumentNumber";
            this.txtDocumentNumber.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99984198808670044), Telerik.Reporting.Drawing.Unit.Inch(0.19992129504680634));
            this.txtDocumentNumber.Style.Color = System.Drawing.Color.Black;
            this.txtDocumentNumber.Style.Font.Bold = true;
            this.txtDocumentNumber.Value = "Document#";
            // 
            // txtPeriode
            // 
            this.txtPeriode.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.9999213218688965), Telerik.Reporting.Drawing.Unit.Inch(0.099999986588954926));
            this.txtPeriode.Name = "txtPeriode";
            this.txtPeriode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.9999605417251587), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtPeriode.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtPeriode.Value = "";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.ChartOfAccountCode,
            this.ChartOfAccountName,
            this.SubLedgerName,
            this.Credit,
            this.Debit,
            this.DocumentNumber});
            this.detail.Name = "detail";
            // 
            // ChartOfAccountCode
            // 
            this.ChartOfAccountCode.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.ChartOfAccountCode.Name = "ChartOfAccountCode";
            this.ChartOfAccountCode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.79988193511962891), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.ChartOfAccountCode.TextWrap = true;
            this.ChartOfAccountCode.Value = "=ChartOfAccountCode";
            // 
            // ChartOfAccountName
            // 
            this.ChartOfAccountName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.80000019073486328), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.ChartOfAccountName.Name = "ChartOfAccountName";
            this.ChartOfAccountName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.1999998092651367), Telerik.Reporting.Drawing.Unit.Inch(0.19996070861816406));
            this.ChartOfAccountName.TextWrap = true;
            this.ChartOfAccountName.Value = "=ChartOfAccountName";
            // 
            // SubLedgerName
            // 
            this.SubLedgerName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.0000786781311035), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.SubLedgerName.Name = "SubLedgerName";
            this.SubLedgerName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.999842643737793), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.SubLedgerName.TextWrap = true;
            this.SubLedgerName.Value = "=SubLedgerName";
            // 
            // Credit
            // 
            this.Credit.Format = "{0:N2}";
            this.Credit.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.9999604225158691), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.Credit.Name = "Credit";
            this.Credit.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99996060132980347), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.Credit.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.Credit.TextWrap = true;
            this.Credit.Value = "=Credit";
            // 
            // Debit
            // 
            this.Debit.Format = "{0:N2}";
            this.Debit.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.9999604225158691), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.Debit.Name = "Debit";
            this.Debit.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99992114305496216), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.Debit.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.Debit.TextWrap = true;
            this.Debit.Value = "=Debit";
            // 
            // DocumentNumber
            // 
            this.DocumentNumber.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.DocumentNumber.Name = "DocumentNumber";
            this.DocumentNumber.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.9998820424079895), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.DocumentNumber.TextWrap = true;
            this.DocumentNumber.Value = "=DocumentNumber";
            // 
            // TransactionNumber
            // 
            this.TransactionNumber.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.80000019073486328), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.TransactionNumber.Name = "TransactionNumber";
            this.TransactionNumber.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.89992111921310425), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.TransactionNumber.Value = "=TransactionNumberDisplay";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.40007862448692322);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtPageNumber,
            this.txtPrintDateTime});
            this.pageFooter.Name = "pageFooter";
            this.pageFooter.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.pageFooter.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.pageFooter.Style.Font.Bold = true;
            this.pageFooter.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            // 
            // txtPageNumber
            // 
            this.txtPageNumber.Format = "";
            this.txtPageNumber.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5), Telerik.Reporting.Drawing.Unit.Inch(0.09984191507101059));
            this.txtPageNumber.Name = "txtPageNumber";
            this.txtPageNumber.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.9999995231628418), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtPageNumber.Style.Font.Bold = false;
            this.txtPageNumber.Style.Font.Italic = true;
            this.txtPageNumber.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtPageNumber.Value = "=\'Page : \' + PageNumber + \'/\' + PageCount";
            // 
            // txtPrintDateTime
            // 
            this.txtPrintDateTime.Format = "Avicenna HIS, Print Date : {0:dd-MM-yyyy HH:mm}";
            this.txtPrintDateTime.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.099841989576816559));
            this.txtPrintDateTime.Name = "txtPrintDateTime";
            this.txtPrintDateTime.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.9999217987060547), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtPrintDateTime.Style.Font.Bold = false;
            this.txtPrintDateTime.Style.Font.Italic = true;
            this.txtPrintDateTime.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtPrintDateTime.Value = "= Now()";
            // 
            // Description
            // 
            this.Description.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.7000001668930054), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.Description.Name = "Description";
            this.Description.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.2998814582824707), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.Description.TextWrap = false;
            this.Description.Value = "=Description";
            // 
            // TransactionDate
            // 
            this.TransactionDate.Format = "{0:dd-MM-yyyy}";
            this.TransactionDate.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.TransactionDate.Name = "TransactionDate";
            this.TransactionDate.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.79988193511962891), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.TransactionDate.TextWrap = false;
            this.TransactionDate.Value = "=TransactionDate";
            // 
            // JournalIdGroup
            // 
            this.JournalIdGroup.GroupFooter = this.groupFooterJournalId;
            this.JournalIdGroup.GroupHeader = this.groupHeaderJournalId;
            this.JournalIdGroup.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("=JournalId")});
            this.JournalIdGroup.Name = "group1";
            // 
            // groupFooterJournalId
            // 
            this.groupFooterJournalId.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20007896423339844);
            this.groupFooterJournalId.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.TotalDebit,
            this.TotalCredit});
            this.groupFooterJournalId.Name = "groupFooterJournalId";
            // 
            // TotalDebit
            // 
            this.TotalDebit.Format = "{0:N2}";
            this.TotalDebit.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.9999604225158691), Telerik.Reporting.Drawing.Unit.Inch(7.8837074397597462E-05));
            this.TotalDebit.Name = "TotalDebit";
            this.TotalDebit.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99992114305496216), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.TotalDebit.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.TotalDebit.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.TotalDebit.Style.Font.Bold = true;
            this.TotalDebit.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.TotalDebit.TextWrap = true;
            this.TotalDebit.Value = "=Sum(Debit)";
            // 
            // TotalCredit
            // 
            this.TotalCredit.Format = "{0:N2}";
            this.TotalCredit.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.0000395774841309), Telerik.Reporting.Drawing.Unit.Inch(7.8837074397597462E-05));
            this.TotalCredit.Name = "TotalCredit";
            this.TotalCredit.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99996060132980347), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.TotalCredit.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.TotalCredit.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.TotalCredit.Style.Font.Bold = true;
            this.TotalCredit.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.TotalCredit.TextWrap = true;
            this.TotalCredit.Value = "=sum(Credit)";
            // 
            // groupHeaderJournalId
            // 
            this.groupHeaderJournalId.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20003955066204071);
            this.groupHeaderJournalId.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.Description,
            this.TransactionNumber,
            this.TransactionDate});
            this.groupHeaderJournalId.Name = "groupHeaderJournalId";
            this.groupHeaderJournalId.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            // 
            // JournalTypeGroup
            // 
            this.JournalTypeGroup.GroupFooter = this.groupFooterJournalType;
            this.JournalTypeGroup.GroupHeader = this.groupHeaderJournalType;
            this.JournalTypeGroup.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("=JournalType")});
            this.JournalTypeGroup.Name = "JournalTypeGroup";
            // 
            // groupFooterJournalType
            // 
            this.groupFooterJournalType.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699);
            this.groupFooterJournalType.Name = "groupFooterJournalType";
            this.groupFooterJournalType.Style.Font.Bold = true;
            // 
            // groupHeaderJournalType
            // 
            this.groupHeaderJournalType.Height = Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985);
            this.groupHeaderJournalType.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.JournalType});
            this.groupHeaderJournalType.Name = "groupHeaderJournalType";
            this.groupHeaderJournalType.Style.Visible = false;
            // 
            // JournalType
            // 
            this.JournalType.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.JournalType.Name = "JournalType";
            this.JournalType.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.1999998092651367), Telerik.Reporting.Drawing.Unit.Inch(0.19992129504680634));
            this.JournalType.Style.Color = System.Drawing.Color.Black;
            this.JournalType.Style.Font.Bold = true;
            this.JournalType.Value = "=JournalType";
            // 
            // JournalListReport
            // 
            this.DocumentName = "Journal Report";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            this.JournalTypeGroup,
            this.JournalIdGroup});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderJournalType,
            this.groupFooterJournalType,
            this.groupHeaderJournalId,
            this.groupFooterJournalId,
            this.pageHeader,
            this.detail,
            this.pageFooter});
            this.Name = "JournalListReport";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Inch(0.25);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Inch(0.25);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Inch(0.25);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Inch(0.25);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(8.0000391006469727);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox txtReportTitle;
        private Telerik.Reporting.TextBox txtPrintDateTime;
        private Telerik.Reporting.TextBox txtPageNumber;
        private Telerik.Reporting.TextBox txtCompanyName;
        private Telerik.Reporting.TextBox txtTransactionDate;
        private Telerik.Reporting.TextBox txtTransactionNumber;
        private Telerik.Reporting.TextBox TransactionNumber;
        private Telerik.Reporting.TextBox txtDescription;
        private Telerik.Reporting.TextBox txtDebit;
        private Telerik.Reporting.TextBox txtCredit;
        private Telerik.Reporting.TextBox Description;
        private Telerik.Reporting.TextBox TransactionDate;
        private Telerik.Reporting.TextBox ChartOfAccountCode;
        private Telerik.Reporting.TextBox ChartOfAccountName;
        private Telerik.Reporting.TextBox SubLedgerName;
        private Telerik.Reporting.TextBox Credit;
        private Telerik.Reporting.TextBox Debit;
        private Telerik.Reporting.Group JournalIdGroup;
        private Telerik.Reporting.GroupFooterSection groupFooterJournalId;
        private Telerik.Reporting.GroupHeaderSection groupHeaderJournalId;
        private Telerik.Reporting.Group JournalTypeGroup;
        private Telerik.Reporting.GroupFooterSection groupFooterJournalType;
        private Telerik.Reporting.GroupHeaderSection groupHeaderJournalType;
        private Telerik.Reporting.TextBox TotalDebit;
        private Telerik.Reporting.TextBox TotalCredit;
        private Telerik.Reporting.TextBox DocumentNumber;
        private Telerik.Reporting.TextBox txtAccountCode;
        private Telerik.Reporting.TextBox txtAccountName;
        private Telerik.Reporting.TextBox txtSubLedger;
        private Telerik.Reporting.TextBox txtDocumentNumber;
        private Telerik.Reporting.TextBox JournalType;
        private Telerik.Reporting.TextBox txtPeriode;
    }
}