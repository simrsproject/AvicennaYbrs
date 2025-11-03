using System;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges.RSCH
{
    using Telerik.Reporting;
    using BusinessObject;
    using System;
    using System.Data;
    using System.Linq;

    /// <summary>
    /// Summary description for BillingSummary.
    /// </summary>
    public partial class PaymentReceiveReceiptDetailOutPatientV2 : Report
    {
        public PaymentReceiveReceiptDetailOutPatientV2(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            this.DataSource = dtb;

            decimal total = 0;
            foreach (DataRow row in dtb.Rows)
            {
                total = Convert.ToDecimal(row["Amount"]);
            }
            txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords(total);
            textBox45.Value = string.Format("{0:n0}", (total));
            var healthcare = Healthcare.GetHealthcare();
            
            TxtNameRS.Value = healthcare.HealthcareName;
            TxtCityRS.Value = healthcare.AddressLine1 + ' ' + healthcare.City;
            TxtTelp.Value = "Telp " + healthcare.PhoneNo + " Fax " + healthcare.FaxNo;

            txtUserName.Value = printJobParameters[1].ValueString;
        }
    }
}
