using System;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges.RSUI
{
    using Telerik.Reporting;
    using BusinessObject;
    using System;
    using System.Data;
    using System.Linq;

    public partial class PaymentReceiveReceiptDetailOutPatient2 : Telerik.Reporting.Report
    {
        public PaymentReceiveReceiptDetailOutPatient2(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            //Helper.InitializeLogo(this.pageHeaderSection1);

            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            this.DataSource = dtb;

            decimal total = 0;
            foreach (DataRow row in dtb.Rows)
            {
                total = Convert.ToDecimal(row["Amount"]);
            }
            textBox31.Value = (new Common.Convertion()).NumericToWords(total);
            //textBox45.Value = string.Format("{0:n0}", (total));
            var healthcare = Healthcare.GetHealthcare();
            
            //TxtNameRS.Value = healthcare.HealthcareName;
            //TxtCityRS.Value = healthcare.AddressLine1 + ' ' + healthcare.City;
            //TxtTelp.Value = "Telp " + healthcare.PhoneNo + " Fax " + healthcare.FaxNo;
            textBox14.Value = healthcare.City + ", " + string.Format("{0:dd-MMM-yyyy}", DateTime.Now);

            textBox6.Value = printJobParameters[1].ValueString;

            var qpay_i = new TransPayment();
            qpay_i.LoadByPrimaryKey(printJobParameters[0].ValueString);


            var qpay = new TransPaymentQuery("a");
            var qpayitem = new TransPaymentItemQuery("b");
            var qpaytype = new AppStandardReferenceItemQuery("c");
            var qpaymethod = new AppStandardReferenceItemQuery("d");

            qpay.InnerJoin(qpayitem).On(qpay.PaymentNo == qpayitem.PaymentNo);
            qpay.LeftJoin(qpaytype).On(qpaytype.StandardReferenceID == "PaymentType" & qpayitem.SRPaymentType == qpaytype.ItemID);
            qpay.LeftJoin(qpaymethod).On(qpaymethod.StandardReferenceID == "PaymentMethod" & qpayitem.SRPaymentMethod == qpaymethod.ItemID);

            qpay.Where(qpay.IsApproved == true, 
                qpay.IsVoid == false, 
                //qpay.RegistrationNo == qpay_i.RegistrationNo,
                qpay.PaymentNo == qpay_i.PaymentNo,
                qpay.TransactionCode == "016");

            qpay.Select(
                qpay.PaymentNo,
                "<c.ItemName + ' ' + ISNULL(d.ItemName, '') SRPaymentTypeMethodName>",
                qpayitem.CardNo,
                qpayitem.Amount
            );
            

        }
    }
}