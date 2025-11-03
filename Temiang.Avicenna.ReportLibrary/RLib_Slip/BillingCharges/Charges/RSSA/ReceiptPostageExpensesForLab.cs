using System.Linq;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges.RSSA
{
    using BusinessObject;
    using System.Data;
    using System;
    public partial class ReceiptPostageExpensesForLab : Telerik.Reporting.Report
    {
        public ReceiptPostageExpensesForLab(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeNoLogoBigFont(this.pageHeader);

            var query = new PettyCashQuery("a");
            var queryitem = new PettyCashItemQuery("b");
            
            query.Select
                (
                    query.TransactionNo,
                    query.TransactionDate,
                    query.Notes,
                    queryitem.SequenceNo,
                    queryitem.Description,
                    queryitem.Credit
                );
            query.InnerJoin(queryitem).On(query.TransactionNo == queryitem.TransactionNo);
            query.Where(query.TransactionNo == printJobParameters[0].ValueString);

            DataTable dtb = query.LoadDataTable();

            decimal total = dtb.Rows.Cast<DataRow>().Sum(row => Convert.ToDecimal(row["Credit"]));

            txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords(total);
            TxtAmount.Value = string.Format("{0:n0}", (total));

            DataSource = dtb;
            this.table1.DataSource = dtb;
            var healthcare = Healthcare.GetHealthcare();
            
            TxtCityRS.Value = healthcare.AddressLine2;

            TxtUserName.Value = healthcare.HealthcareName;
        }
    }
}
