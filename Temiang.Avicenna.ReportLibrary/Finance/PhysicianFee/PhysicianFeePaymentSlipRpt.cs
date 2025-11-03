using System;
using Temiang.Avicenna.BusinessObject;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.Finance.PhysicianFee
{
    public partial class PhysicianFeePaymentSlipRpt : Report
    {
        public PhysicianFeePaymentSlipRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            var reportDataSource = new ReportDataSource();
            DataSource = reportDataSource.GetDataTable(programID, printJobParameters);

            var query = new ParamedicFeePaymentHdQuery("a");
            query.Select
                (
                    query.PaymentAmount
                );
            query.Where
                (
                    query.PaymentNo == printJobParameters[0].ValueString,
                    query.IsApproved == true
                );

            var dtb = query.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords(Convert.ToDecimal(dtb.Rows[0]["PaymentAmount"]));

                var rs = Healthcare.GetHealthcare();
                
                txtHealtcareName.Value = rs.HealthcareName;
            }

        }
    }
}