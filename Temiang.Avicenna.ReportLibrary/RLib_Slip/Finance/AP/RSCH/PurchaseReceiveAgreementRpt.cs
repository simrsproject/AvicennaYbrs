using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.ReportLibrary.Rlib_Slip.Finance.AP.RSCH
{
   

  
    /// <summary>
    /// Summary description for PurchaseReceiveAgreementRpt.
    /// </summary>
    public partial class PurchaseReceiveAgreementRpt : Telerik.Reporting.Report
    {
        public PurchaseReceiveAgreementRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            Helper.InitializeLogo(pageHeaderSection1);
            var reportDataSource = new ReportDataSource();
            System.Data.DataTable tbl = reportDataSource.GetDataTable(programID, printJobParameters);
            tbl.Columns.Add("Terbilang", typeof(string));
           

            foreach (DataRow row in tbl.Rows)
            {           
                row["Terbilang"] = (new Common.Convertion()).NumericToWords(Convert.ToDecimal(row["Total"]));

                row.AcceptChanges();
            }

            this.DataSource = tbl;
            textBox32.Value = AppSession.UserLogin.UserName;
           


        }
    }
}