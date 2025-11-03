using Temiang.Avicenna.Common;
namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges.RSUI
{
    using BusinessObject;
    using System;
    using Temiang.Dal.DynamicQuery;
    using System.Data;

    public partial class DownPaymentReturnReceipt : Telerik.Reporting.Report
    {
        public DownPaymentReturnReceipt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeNoLogo(this.pageHeader);
            TransPaymentQuery query = new TransPaymentQuery("a");
            TransPaymentItemQuery item = new TransPaymentItemQuery("b");
            RegistrationQuery reg = new RegistrationQuery("c");
            PatientQuery patient = new PatientQuery("d");
            GuarantorQuery guar = new GuarantorQuery("e");
            AppStandardReferenceItemQuery type = new AppStandardReferenceItemQuery("f");
            AppStandardReferenceItemQuery method = new AppStandardReferenceItemQuery("g");
            ClassQuery clss = new ClassQuery("h");

            query.Select
                (
                    "<CASE WHEN a.IsPrinted > 1 THEN a.PaymentNo + ' (D)' ELSE a.PaymentNo END AS PaymentNo>",
                    query.PaymentDate,
                    query.Notes,
                    query.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName,
                    patient.StreetName,
                    clss.ClassName,
                    query.PrintReceiptAsName,
                    guar.GuarantorName,
                    (item.Amount + item.CardFeeAmount).As("Amount")
                );

            query.InnerJoin(item).On(query.PaymentNo == item.PaymentNo);
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.LeftJoin(clss).On(reg.ClassID == clss.ClassID);
            query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            query.InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID);
            query.Where(query.PaymentNo == printJobParameters[0].ValueString);
            query.OrderBy(item.SequenceNo, esOrderByDirection.Ascending);

            DataTable dtb = query.LoadDataTable();

            foreach (DataRow row in dtb.Rows)
            {
                if (!(bool)row["IsApproved"])
                {
                    dtb.Rows.Clear(); // can not print
                    break;
                }
            }


            decimal total = 0;
            foreach (DataRow row in dtb.Rows)
            {
                total += Convert.ToDecimal(row["Amount"]);
            }
            txtTotalAmountInWords.Value = (total == 0) ? "" : (new Common.Convertion()).NumericToWords(total);
            textBox6.Value = string.Format("{0:n0}", (total));
            DataSource = dtb;
            var healthcare = Healthcare.GetHealthcare();
            
            textBox16.Value = healthcare.AddressLine2 + ",";
            txtHealthcareName.Value = healthcare.HealthcareName;
            textBox21.Value = healthcare.HealthcareName;

            var user = new AppUser();
            user.LoadByPrimaryKey(query.LastUpdateByUserID);
            txtUserName.Value = user.UserName;

        }
    }
}