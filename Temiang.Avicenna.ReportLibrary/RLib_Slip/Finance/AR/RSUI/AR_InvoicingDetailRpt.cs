using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Finance.AR.RSUI
{
    using BusinessObject;
    using System;
    using System.Data;
    using System.Linq;

    public partial class AR_InvoicingDetailRpt : Telerik.Reporting.Report
    {
        public AR_InvoicingDetailRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();


            PopulateHealthcareInfo();


            var query = new InvoicesQuery("a");
            var detail = new InvoicesItemQuery("b");
            var guar = new GuarantorQuery("c");
            var reg = new RegistrationQuery("d");
            var pat = new PatientQuery("e");
            var ib = new IntermBillQuery("f");
            var su = new ServiceUnitQuery("g");
            var type = new AppStandardReferenceItemQuery("h");
            var rrp = new RegistrationResponsiblePersonQuery("i");
            var rel = new AppStandardReferenceItemQuery("j");
            var tp = new TransPaymentQuery ("k");
            var tpi = new TransPaymentItemQuery ("l");

            query.Select
                (
                    //query.InvoiceNo,
                    query.InvoiceDate,
                    query.InvoiceDueDate,
                    query.GuarantorID,
                    type.ItemName.As("GuarantorType"),
                    guar.GuarantorName,
                    @"<RTRIM ( c.StreetName + ' ' + c.ZipCode + ' ' + c.District + ' ' + c.County + ' ' + c.City ) AS Addres>",
                    guar.FaxNo.Coalesce("'  '"),
                    guar.MobilePhoneNo.Coalesce("'  '"),
                    guar.ContactPerson,
                    guar.PhoneNo.Coalesce("'  '"),
                    su.ServiceUnitName,
                    @"<RTRIM(e.FirstName) + ' ' + RTRIM(e.MiddleName) + ' ' + RTRIM(e.LastName) AS PatientName>",
                    pat.MedicalNo.Coalesce("''"),
                    detail.RegistrationNo.Coalesce("''"),
                    pat.Company,
                    rrp.NameOfTheResponsible,
                    rel.ItemName.As("Relationship"),
                    reg.GuarantorCardNo,
                    reg.SRRegistrationType,
                    reg.InsuranceID,
                    reg.AdministrationAmount,
                    reg.PatientAdm,
                    reg.GuarantorAdm,
                    detail.PaymentNo,
                    detail.PaymentDate,
                    detail.Amount,
                    ib.PatientAmount,
                    ib.GuarantorAmount,
                    detail.LastUpdateByUserID,
                    @"<GETDATE() AS PrintedDate>"
                );

            query.InnerJoin(detail).On(query.InvoiceNo == detail.InvoiceNo);
            query.InnerJoin(guar).On(query.GuarantorID == guar.GuarantorID);
            query.InnerJoin(reg).On(detail.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            query.InnerJoin(ib).On(ib.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(su).On(su.ServiceUnitID == reg.ServiceUnitID);
            query.InnerJoin(tp).On(tp.PaymentNo == detail.PaymentNo);
            query.InnerJoin(tpi).On(tpi.PaymentNo == tp.PaymentNo);
            query.LeftJoin(type).On
                (
                    guar.SRGuarantorType == type.ItemID &
                    type.StandardReferenceID == "GuarantorType"
                );
            query.InnerJoin(rrp).On(rrp.RegistrationNo == reg.RegistrationNo);
            query.LeftJoin(rel).On
                 (
                     rrp.SRRelationship == rel.ItemID &
                     rel.StandardReferenceID == "Relationship"
                 );
            query.Where
                (
                    query.InvoiceNo == printJobParameters[0].ValueString,
                    query.IsVoid == false,
                    query.IsApproved == true,
                    ib.IsVoid == false,
                    tp.IsApproved == true
                );
            //DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            //DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            //textBox20.Value = string.Format(" {0:dd-MMM-yyyy} s/d {1:dd-MMM-yyyy}", fromDate, toDate);

            var dtb = query.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                this.DataSource = dtb;
                var user = new AppUser();
                user.LoadByPrimaryKey(dtb.Rows[0]["LastUpdateByUserID"].ToString());
            }

            //Helper.InitializeLogo(this.pageHeader);
            //Helper.InitializeDataSource(this, programID, printJobParameters);
        }

        private void PopulateHealthcareInfo()
        {
            var healthcare = Healthcare.GetHealthcare();
            
        }
    }
}