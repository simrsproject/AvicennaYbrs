namespace Temiang.Avicenna.ReportLibrary.Accounting.SubLedgers
{
    partial class SubLedgerListReport
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
            this.txtGroupName = new Telerik.Reporting.TextBox();
            this.txtDescription = new Telerik.Reporting.TextBox();
            this.txtGroupCode = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.SubLedgerName = new Telerik.Reporting.TextBox();
            this.SubLedgerDescription = new Telerik.Reporting.TextBox();
            this.GroupCode = new Telerik.Reporting.TextBox();
            this.GroupName = new Telerik.Reporting.TextBox();
            this.Description = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.group1 = new Telerik.Reporting.Group();
            this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();
            this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
            this.txtPageNumber = new Telerik.Reporting.TextBox();
            this.txtPrintDateTime = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(1.0999213457107544);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtReportTitle,
            this.txtCompanyName,
            this.txtGroupName,
            this.txtDescription,
            this.txtGroupCode});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.pageHeader.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1);
            this.pageHeader.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1);
            // 
            // txtReportTitle
            // 
            this.txtReportTitle.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.099999986588954926));
            this.txtReportTitle.Name = "txtReportTitle";
            this.txtReportTitle.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.9999608993530273), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtReportTitle.Style.BorderColor.Bottom = System.Drawing.Color.Blue;
            this.txtReportTitle.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(119)))), ((int)(((byte)(171)))));
            this.txtReportTitle.Style.Font.Bold = true;
            this.txtReportTitle.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.txtReportTitle.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtReportTitle.Value = "SUB LEDGER LIST";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134));
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.9999606609344482), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtCompanyName.Value = "";
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.0000787973403931), Telerik.Reporting.Drawing.Unit.Inch(0.80003935098648071));
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.9999213218688965), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtGroupName.Style.Font.Bold = true;
            this.txtGroupName.Value = "Group Name";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4), Telerik.Reporting.Drawing.Unit.Inch(0.80003947019577026));
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.4999604225158691), Telerik.Reporting.Drawing.Unit.Inch(0.29988193511962891));
            this.txtDescription.Style.Font.Bold = true;
            this.txtDescription.Value = "Description";
            // 
            // txtGroupCode
            // 
            this.txtGroupCode.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.80003935098648071));
            this.txtGroupCode.Name = "txtGroupCode";
            this.txtGroupCode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0000001192092896), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtGroupCode.Style.Font.Bold = true;
            this.txtGroupCode.Value = "Group Code";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.SubLedgerName,
            this.SubLedgerDescription});
            this.detail.Name = "detail";
            this.detail.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            // 
            // SubLedgerName
            // 
            this.SubLedgerName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.0000787973403931), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.SubLedgerName.Name = "SubLedgerName";
            this.SubLedgerName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.9999213218688965), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.SubLedgerName.TextWrap = false;
            this.SubLedgerName.Value = "=SubLedgerName";
            // 
            // SubLedgerDescription
            // 
            this.SubLedgerDescription.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.0000786781311035), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.SubLedgerDescription.Name = "SubLedgerDescription";
            this.SubLedgerDescription.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.4999606609344482), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.SubLedgerDescription.Value = "=SubLedgerDescription";
            // 
            // GroupCode
            // 
            this.GroupCode.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(-7.9472862068996619E-08));
            this.GroupCode.Name = "GroupCode";
            this.GroupCode.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.999960720539093), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.GroupCode.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.GroupCode.Value = "=GroupCode";
            // 
            // GroupName
            // 
            this.GroupName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.0000787973403931), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.GroupName.Name = "GroupName";
            this.GroupName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.9999213218688965), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.GroupName.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.GroupName.TextWrap = false;
            this.GroupName.Value = "=GroupName";
            // 
            // Description
            // 
            this.Description.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.Description.Name = "Description";
            this.Description.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.4999606609344482), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.Description.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.Description.Value = "=Description";
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
            // group1
            // 
            this.group1.GroupFooter = this.groupFooterSection1;
            this.group1.GroupHeader = this.groupHeaderSection1;
            this.group1.Groupings.AddRange(new Telerik.Reporting.Grouping[] {
            new Telerik.Reporting.Grouping("=GroupCode"),
            new Telerik.Reporting.Grouping("")});
            this.group1.Name = "group1";
            // 
            // groupFooterSection1
            // 
            this.groupFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20007848739624023);
            this.groupFooterSection1.Name = "groupFooterSection1";
            // 
            // groupHeaderSection1
            // 
            this.groupHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.30007871985435486);
            this.groupHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.GroupCode,
            this.GroupName,
            this.Description});
            this.groupHeaderSection1.Name = "groupHeaderSection1";
            this.groupHeaderSection1.Style.Font.Bold = true;
            // 
            // txtPageNumber
            // 
            this.txtPageNumber.Format = "";
            this.txtPageNumber.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.0000782012939453), Telerik.Reporting.Drawing.Unit.Inch(0.099921420216560364));
            this.txtPageNumber.Name = "txtPageNumber";
            this.txtPageNumber.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.4999215602874756), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtPageNumber.Style.Font.Bold = false;
            this.txtPageNumber.Style.Font.Italic = true;
            this.txtPageNumber.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtPageNumber.Value = "= \"Page \" + PageNumber + \" Of \" + PageCount";
            // 
            // txtPrintDateTime
            // 
            this.txtPrintDateTime.Format = "Avicenna HIS, Print Date : {0:dd-MM-yyyy HH:mm}";
            this.txtPrintDateTime.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0042449953034520149), Telerik.Reporting.Drawing.Unit.Inch(0.099921420216560364));
            this.txtPrintDateTime.Name = "txtPrintDateTime";
            this.txtPrintDateTime.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.9957549571990967), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtPrintDateTime.Style.Font.Bold = false;
            this.txtPrintDateTime.Style.Font.Italic = true;
            this.txtPrintDateTime.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtPrintDateTime.Value = "= Now()";
            // 
            // SubLedgerListReport
            // 
            this.DocumentName = "Subledger Report";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            this.group1});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection1,
            this.groupFooterSection1,
            this.pageHeader,
            this.detail,
            this.pageFooter});
            this.Name = "SubLedgerListReport";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Inch(0.5);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Inch(0.5);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Inch(0.5);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Inch(0.5);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.5);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox GroupCode;
        private Telerik.Reporting.TextBox GroupName;
        private Telerik.Reporting.TextBox txtReportTitle;
        private Telerik.Reporting.TextBox txtCompanyName;
        private Telerik.Reporting.TextBox txtGroupCode;
        private Telerik.Reporting.TextBox txtGroupName;
        private Telerik.Reporting.TextBox txtDescription;
        private Telerik.Reporting.TextBox Description;
        private Telerik.Reporting.TextBox SubLedgerName;
        private Telerik.Reporting.TextBox SubLedgerDescription;
        private Telerik.Reporting.Group group1;
        private Telerik.Reporting.GroupFooterSection groupFooterSection1;
        private Telerik.Reporting.GroupHeaderSection groupHeaderSection1;
        private Telerik.Reporting.TextBox txtPageNumber;
        private Telerik.Reporting.TextBox txtPrintDateTime;
    }
}