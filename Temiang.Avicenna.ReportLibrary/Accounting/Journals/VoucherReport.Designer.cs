namespace Temiang.Avicenna.ReportLibrary.Accounting.Journals
{
    partial class VoucherReport
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.GroupHeaderSection groupHeaderJournalType;
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.txtReportTitle = new Telerik.Reporting.TextBox();
            this.txtCompanyName = new Telerik.Reporting.TextBox();
            this.txtDescription = new Telerik.Reporting.TextBox();
            this.txtDebit = new Telerik.Reporting.TextBox();
            this.txtCredit = new Telerik.Reporting.TextBox();
            this.txtAccountCode = new Telerik.Reporting.TextBox();
            this.txtAccountName = new Telerik.Reporting.TextBox();
            this.TransactionNumber = new Telerik.Reporting.TextBox();
            this.Description = new Telerik.Reporting.TextBox();
            this.txtTransactionNumber = new Telerik.Reporting.TextBox();
            this.txtDescriptionHeader = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.ChartOfAccountCode = new Telerik.Reporting.TextBox();
            this.ChartOfAccountName = new Telerik.Reporting.TextBox();
            this.Credit = new Telerik.Reporting.TextBox();
            this.Debit = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.txtPageNumber = new Telerik.Reporting.TextBox();
            this.txtPrintDateTime = new Telerik.Reporting.TextBox();
            this.TransactionDate = new Telerik.Reporting.TextBox();
            this.JournalIdGroup = new Telerik.Reporting.Group();
            this.groupFooterJournalId = new Telerik.Reporting.GroupFooterSection();
            this.TotalCredit = new Telerik.Reporting.TextBox();
            this.TotalDebit = new Telerik.Reporting.TextBox();
            this.groupHeaderJournalId = new Telerik.Reporting.GroupHeaderSection();
            this.JournalTypeGroup = new Telerik.Reporting.Group();
            this.groupFooterJournalType = new Telerik.Reporting.GroupFooterSection();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.txtReceivedBy = new Telerik.Reporting.TextBox();
            this.txtApprovedBy = new Telerik.Reporting.TextBox();
            this.txtCheckedBy = new Telerik.Reporting.TextBox();
            this.txtPreparedBy = new Telerik.Reporting.TextBox();
            groupHeaderJournalType = new Telerik.Reporting.GroupHeaderSection();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // groupHeaderJournalType
            // 
            groupHeaderJournalType.Height = Telerik.Reporting.Drawing.Unit.Inch(0.10000001639127731);
            groupHeaderJournalType.Name = "groupHeaderJournalType";
            groupHeaderJournalType.Style.Visible = false;
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(1.2000001668930054);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtReportTitle,
            this.txtCompanyName,
            this.txtDescription,
            this.txtDebit,
            this.txtCredit,
            this.txtAccountCode,
            this.txtAccountName,
            this.TransactionNumber,
            this.Description,
            this.txtTransactionNumber,
            this.txtDescriptionHeader,
            this.textBox2,
            this.textBox3});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.pageHeader.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.pageHeader.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1);
            // 
            // txtReportTitle
            // 
            this.txtReportTitle.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.099999986588954926));
            this.txtReportTitle.Name = "txtReportTitle";
            this.txtReportTitle.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.9999606609344482), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtReportTitle.Style.BorderColor.Bottom = System.Drawing.Color.Blue;
            this.txtReportTitle.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(119)))), ((int)(((byte)(171)))));
            this.txtReportTitle.Style.Font.Bold = true;
            this.txtReportTitle.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.txtReportTitle.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtReportTitle.Style.Visible = true;
            this.txtReportTitle.Value = "JOURNAL VOUCHER";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.0000786781311035), Telerik.Reporting.Drawing.Unit.Inch(0.099999994039535522));
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.9999606609344482), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtCompanyName.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtCompanyName.Style.Visible = true;
            this.txtCompanyName.Value = "";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.9000787734985352), Telerik.Reporting.Drawing.Unit.Inch(1));
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.4999215602874756), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtDescription.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtDescription.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.txtDescription.Style.Color = System.Drawing.Color.Black;
            this.txtDescription.Style.Font.Bold = true;
            this.txtDescription.Value = "Description";
            // 
            // txtDebit
            // 
            this.txtDebit.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.4000792503356934), Telerik.Reporting.Drawing.Unit.Inch(1));
            this.txtDebit.Name = "txtDebit";
            this.txtDebit.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.79980278015136719), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtDebit.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtDebit.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.txtDebit.Style.Color = System.Drawing.Color.Black;
            this.txtDebit.Style.Font.Bold = true;
            this.txtDebit.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtDebit.Value = "Debit";
            // 
            // txtCredit
            // 
            this.txtCredit.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.1999607086181641), Telerik.Reporting.Drawing.Unit.Inch(1));
            this.txtCredit.Name = "txtCredit";
            this.txtCredit.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.80007892847061157), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtCredit.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtCredit.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.txtCredit.Style.Color = System.Drawing.Color.Black;
            this.txtCredit.Style.Font.Bold = true;
            this.txtCredit.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtCredit.Value = "Credit";
            // 
            // txtAccountCode
            // 
            this.txtAccountCode.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(1));
            this.txtAccountCode.Name = "txtAccountCode";
            this.txtAccountCode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.5999605655670166), Telerik.Reporting.Drawing.Unit.Inch(0.19992129504680634));
            this.txtAccountCode.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtAccountCode.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.txtAccountCode.Style.Color = System.Drawing.Color.Black;
            this.txtAccountCode.Style.Font.Bold = true;
            this.txtAccountCode.Value = "Account";
            // 
            // txtAccountName
            // 
            this.txtAccountName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.60000008344650269), Telerik.Reporting.Drawing.Unit.Inch(1));
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.2999999523162842), Telerik.Reporting.Drawing.Unit.Inch(0.19992129504680634));
            this.txtAccountName.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtAccountName.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.txtAccountName.Style.Color = System.Drawing.Color.Black;
            this.txtAccountName.Style.Font.Bold = true;
            this.txtAccountName.Value = "Account Name";
            // 
            // TransactionNumber
            // 
            this.TransactionNumber.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2000001668930054), Telerik.Reporting.Drawing.Unit.Inch(0.50000017881393433));
            this.TransactionNumber.Name = "TransactionNumber";
            this.TransactionNumber.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.7999999523162842), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.TransactionNumber.Value = "=TransactionNumberDisplay";
            // 
            // Description
            // 
            this.Description.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2000001668930054), Telerik.Reporting.Drawing.Unit.Inch(0.70007914304733276));
            this.Description.Name = "Description";
            this.Description.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.7000002861022949), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.Description.TextWrap = false;
            this.Description.Value = "=description_detail";
            // 
            // txtTransactionNumber
            // 
            this.txtTransactionNumber.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.5));
            this.txtTransactionNumber.Name = "txtTransactionNumber";
            this.txtTransactionNumber.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.199881911277771), Telerik.Reporting.Drawing.Unit.Inch(0.19992129504680634));
            this.txtTransactionNumber.Style.Color = System.Drawing.Color.Black;
            this.txtTransactionNumber.Style.Font.Bold = true;
            this.txtTransactionNumber.Value = "Voucher Number";
            // 
            // txtDescriptionHeader
            // 
            this.txtDescriptionHeader.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.70015794038772583));
            this.txtDescriptionHeader.Name = "txtDescriptionHeader";
            this.txtDescriptionHeader.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.199881911277771), Telerik.Reporting.Drawing.Unit.Inch(0.19992129504680634));
            this.txtDescriptionHeader.Style.Color = System.Drawing.Color.Black;
            this.txtDescriptionHeader.Style.Font.Bold = true;
            this.txtDescriptionHeader.Value = "Description";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.0000786781311035), Telerik.Reporting.Drawing.Unit.Inch(0.5));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99996060132980347), Telerik.Reporting.Drawing.Unit.Inch(0.19992129504680634));
            this.textBox2.Style.Color = System.Drawing.Color.Black;
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Value = "Voucher Date";
            // 
            // textBox3
            // 
            this.textBox3.Format = "{0:d}";
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.1000003814697266), Telerik.Reporting.Drawing.Unit.Inch(0.5));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.295872688293457), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox3.Value = "=TransactionDate";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.19999949634075165);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.ChartOfAccountCode,
            this.ChartOfAccountName,
            this.Credit,
            this.Debit,
            this.textBox1,
            this.textBox4});
            this.detail.Name = "detail";
            this.detail.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            // 
            // ChartOfAccountCode
            // 
            this.ChartOfAccountCode.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.ChartOfAccountCode.Name = "ChartOfAccountCode";
            this.ChartOfAccountCode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.5999605655670166), Telerik.Reporting.Drawing.Unit.Inch(0.0999603271484375));
            this.ChartOfAccountCode.TextWrap = true;
            this.ChartOfAccountCode.Value = "=ChartOfAccountCode";
            // 
            // ChartOfAccountName
            // 
            this.ChartOfAccountName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.60007864236831665), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.ChartOfAccountName.Name = "ChartOfAccountName";
            this.ChartOfAccountName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.2999212741851807), Telerik.Reporting.Drawing.Unit.Inch(0.0999603271484375));
            this.ChartOfAccountName.TextWrap = true;
            this.ChartOfAccountName.Value = "=ChartOfAccountName";
            // 
            // Credit
            // 
            this.Credit.Format = "{0:N2}";
            this.Credit.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.1999607086181641), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.Credit.Name = "Credit";
            this.Credit.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.79996085166931152), Telerik.Reporting.Drawing.Unit.Inch(0.0999603271484375));
            this.Credit.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.Credit.TextWrap = true;
            this.Credit.Value = "=Credit";
            // 
            // Debit
            // 
            this.Debit.Format = "{0:N2}";
            this.Debit.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.4000792503356934), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.Debit.Name = "Debit";
            this.Debit.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.79980260133743286), Telerik.Reporting.Drawing.Unit.Inch(0.0999603271484375));
            this.Debit.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.Debit.TextWrap = true;
            this.Debit.Value = "=Debit";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.9000787734985352), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.4999215602874756), Telerik.Reporting.Drawing.Unit.Inch(0.0999603271484375));
            this.textBox1.TextWrap = true;
            this.textBox1.Value = "=Description";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.60007858276367188), Telerik.Reporting.Drawing.Unit.Inch(0.10003916174173355));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.2999212741851807), Telerik.Reporting.Drawing.Unit.Inch(0.0999603271484375));
            this.textBox4.TextWrap = true;
            this.textBox4.Value = "=SubLedgerName";
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
            this.txtPageNumber.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.0000782012939453), Telerik.Reporting.Drawing.Unit.Inch(0.09984191507101059));
            this.txtPageNumber.Name = "txtPageNumber";
            this.txtPageNumber.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.9999210834503174), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
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
            this.txtPrintDateTime.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.0000004768371582), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtPrintDateTime.Style.Font.Bold = false;
            this.txtPrintDateTime.Style.Font.Italic = true;
            this.txtPrintDateTime.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtPrintDateTime.Value = "= Now()";
            // 
            // TransactionDate
            // 
            this.TransactionDate.Format = "{0:D}";
            this.TransactionDate.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.0998821258544922), Telerik.Reporting.Drawing.Unit.Inch(0.24783770740032196));
            this.TransactionDate.Name = "TransactionDate";
            this.TransactionDate.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.900118350982666), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.TransactionDate.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
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
            this.TotalCredit,
            this.TotalDebit});
            this.groupFooterJournalId.Name = "groupFooterJournalId";
            this.groupFooterJournalId.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.groupFooterJournalId.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Dotted;
            this.groupFooterJournalId.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.groupFooterJournalId.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.groupFooterJournalId.Style.Font.Bold = true;
            this.groupFooterJournalId.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7);
            // 
            // TotalCredit
            // 
            this.TotalCredit.Format = "{0:N2}";
            this.TotalCredit.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.1999611854553223), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.TotalCredit.Name = "TotalCredit";
            this.TotalCredit.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.80003839731216431), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.TotalCredit.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.TotalCredit.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.TotalCredit.Style.Font.Bold = true;
            this.TotalCredit.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.TotalCredit.TextWrap = true;
            this.TotalCredit.Value = "=sum(Credit)";
            // 
            // TotalDebit
            // 
            this.TotalDebit.Format = "{0:N2}";
            this.TotalDebit.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.4000792503356934), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.TotalDebit.Name = "TotalDebit";
            this.TotalDebit.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.79980343580245972), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.TotalDebit.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.TotalDebit.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.TotalDebit.Style.Font.Bold = true;
            this.TotalDebit.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.TotalDebit.TextWrap = true;
            this.TotalDebit.Value = "=Sum(Debit)";
            // 
            // groupHeaderJournalId
            // 
            this.groupHeaderJournalId.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20003955066204071);
            this.groupHeaderJournalId.Name = "groupHeaderJournalId";
            this.groupHeaderJournalId.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            // 
            // JournalTypeGroup
            // 
            this.JournalTypeGroup.GroupFooter = this.groupFooterJournalType;
            this.JournalTypeGroup.GroupHeader = groupHeaderJournalType;
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
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(2.0477983951568604);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.TransactionDate,
            this.txtReceivedBy,
            this.txtApprovedBy,
            this.txtCheckedBy,
            this.txtPreparedBy});
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // txtReceivedBy
            // 
            this.txtReceivedBy.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.0998821258544922), Telerik.Reporting.Drawing.Unit.Inch(0.44783782958984375));
            this.txtReceivedBy.Name = "txtReceivedBy";
            this.txtReceivedBy.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.9000393152236939), Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421));
            this.txtReceivedBy.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtReceivedBy.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.txtReceivedBy.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.txtReceivedBy.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.txtReceivedBy.Style.Color = System.Drawing.Color.Black;
            this.txtReceivedBy.Style.Font.Bold = true;
            this.txtReceivedBy.Value = "Received By";
            // 
            // txtApprovedBy
            // 
            this.txtApprovedBy.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.0000786781311035), Telerik.Reporting.Drawing.Unit.Inch(0.44783782958984375));
            this.txtApprovedBy.Name = "txtApprovedBy";
            this.txtApprovedBy.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.9000393152236939), Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421));
            this.txtApprovedBy.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtApprovedBy.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.txtApprovedBy.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.txtApprovedBy.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.txtApprovedBy.Style.Color = System.Drawing.Color.Black;
            this.txtApprovedBy.Style.Font.Bold = true;
            this.txtApprovedBy.Value = "Approved By";
            // 
            // txtCheckedBy
            // 
            this.txtCheckedBy.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2), Telerik.Reporting.Drawing.Unit.Inch(0.44783782958984375));
            this.txtCheckedBy.Name = "txtCheckedBy";
            this.txtCheckedBy.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.9000393152236939), Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421));
            this.txtCheckedBy.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtCheckedBy.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.txtCheckedBy.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.txtCheckedBy.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.txtCheckedBy.Style.Color = System.Drawing.Color.Black;
            this.txtCheckedBy.Style.Font.Bold = true;
            this.txtCheckedBy.Value = "Checked By";
            // 
            // txtPreparedBy
            // 
            this.txtPreparedBy.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.44783782958984375));
            this.txtPreparedBy.Name = "txtPreparedBy";
            this.txtPreparedBy.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.9000393152236939), Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421));
            this.txtPreparedBy.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtPreparedBy.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.txtPreparedBy.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.txtPreparedBy.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.txtPreparedBy.Style.Color = System.Drawing.Color.Black;
            this.txtPreparedBy.Style.Font.Bold = true;
            this.txtPreparedBy.Value = "Prepared By";
            // 
            // VoucherReport
            // 
            this.DocumentName = "Journal Report";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            this.JournalTypeGroup,
            this.JournalIdGroup});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            groupHeaderJournalType,
            this.groupFooterJournalType,
            this.groupHeaderJournalId,
            this.groupFooterJournalId,
            this.pageHeader,
            this.detail,
            this.pageFooter,
            this.reportFooterSection1});
            this.Name = "VoucherReport";
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
        private Telerik.Reporting.TextBox TransactionNumber;
        private Telerik.Reporting.TextBox txtDebit;
        private Telerik.Reporting.TextBox txtCredit;
        private Telerik.Reporting.TextBox Description;
        private Telerik.Reporting.TextBox TransactionDate;
        private Telerik.Reporting.TextBox ChartOfAccountCode;
        private Telerik.Reporting.TextBox ChartOfAccountName;
        private Telerik.Reporting.TextBox Credit;
        private Telerik.Reporting.TextBox Debit;
        private Telerik.Reporting.Group JournalIdGroup;
        private Telerik.Reporting.GroupFooterSection groupFooterJournalId;
        private Telerik.Reporting.GroupHeaderSection groupHeaderJournalId;
        private Telerik.Reporting.Group JournalTypeGroup;
        private Telerik.Reporting.GroupFooterSection groupFooterJournalType;
        private Telerik.Reporting.TextBox TotalDebit;
        private Telerik.Reporting.TextBox TotalCredit;
        private Telerik.Reporting.TextBox txtAccountCode;
        private Telerik.Reporting.TextBox txtAccountName;
        private Telerik.Reporting.ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.TextBox txtDescription;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox txtTransactionNumber;
        private Telerik.Reporting.TextBox txtDescriptionHeader;
        private Telerik.Reporting.TextBox txtReceivedBy;
        private Telerik.Reporting.TextBox txtApprovedBy;
        private Telerik.Reporting.TextBox txtCheckedBy;
        private Telerik.Reporting.TextBox txtPreparedBy;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
    }
}