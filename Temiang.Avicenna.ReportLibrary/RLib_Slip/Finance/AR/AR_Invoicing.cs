namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Finance.AR
{
    using BusinessObject;
    using System;
    using System.Data;
    using Temiang.Avicenna.Common;

    public partial class AR_Invoicing : Telerik.Reporting.Report
    {
        public AR_Invoicing(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            PopulateHealthcareInfo();
            
            var query = new InvoicesQuery("a");
            var detail = new InvoicesItemQuery("b");
            var guar = new GuarantorQuery("c");
            var reg = new RegistrationQuery("d");
            var pat = new PatientQuery("e");
            var guargroup = new GuarantorQuery("f");

            query.Select
                (
                    query.InvoiceNo,
                    query.InvoiceDate,
                    query.InvoiceDueDate,
                    query.GuarantorID,
                    guar.GuarantorName,
                    guargroup.GuarantorName.As("GuarantorNm"),
                    @"<RTRIM(c.StreetName) + ' ' + RTRIM(c.District) + ' ' + RTRIM(c.County) AS Address1>",
                    @"<RTRIM(c.State) + ' ' + RTRIM(c.ZipCode) AS Address2>",
                    guar.ContactPerson,
                    guar.PhoneNo,
                    guar.FaxNo,
                    detail.RegistrationNo,
                    @"<RTRIM(e.FirstName) + ' ' + RTRIM(e.MiddleName) + ' ' + RTRIM(e.LastName) AS PatientName>",
                    detail.PaymentNo,
                    detail.PaymentDate,
                    detail.Amount,
                    detail.LastUpdateByUserID,
                    @"<GETDATE() AS PrintedDate>",
                    @"<CASE WHEN d.SRRegistrationType = 'IPR' THEN d.DischargeDate ELSE d.RegistrationDate END AS DischargeDate>"
                );

            query.InnerJoin(detail).On(query.InvoiceNo == detail.InvoiceNo);
            query.InnerJoin(guar).On(query.GuarantorID == guar.GuarantorID);
            query.InnerJoin(reg).On(detail.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            query.LeftJoin(guargroup).On(reg.GuarantorID == guargroup.GuarantorID);
            query.Where
                (
                    query.InvoiceNo == printJobParameters[0].ValueString,
                    query.IsVoid == false,
                    query.IsApproved == true
                );

            var dtb = query.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                this.DataSource = dtb;
                textBox38.Value = string.Format("{0:n0}", dtb.Rows.Count);
            }
            else
                textBox38.Value = "0";
        }

        private void PopulateHealthcareInfo()
        {
            var healthcare = Healthcare.GetHealthcare();
            
            TxtHealthcareName.Value = healthcare.HealthcareName;
            txtHealthcare.Value = healthcare.HealthcareName;
            txtAddr.Value = healthcare.AddressLine1 + ' ' + healthcare.AddressLine2 + ' ' + healthcare.City;
            txtCity.Value = " Telp. " + healthcare.PhoneNo + "(3 lines) Fax. " + healthcare.FaxNo;
            textBox29.Value = healthcare.City + ',';
            string financeNoFax = AppParameter.GetParameterValue(AppParameter.ParameterItem.FinanceFaxNoDirect);
            if (string.IsNullOrEmpty(financeNoFax))
                financeNoFax = healthcare.FaxNo;
            
            string finance = AppParameter.GetParameterValue(AppParameter.ParameterItem.PicManagingDirector);
            string emailAddr = AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareFinanceEmailAddr);
            txtFinance.Value = '(' + finance + ')';
            txtFaxNo.Value = financeNoFax;
            txtEmailAddr.Value = emailAddr;
        }
    }
}