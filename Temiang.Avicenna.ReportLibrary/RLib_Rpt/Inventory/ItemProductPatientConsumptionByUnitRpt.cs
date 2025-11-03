using System.Linq;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Inventory
{
    using Telerik.Reporting;
    using BusinessObject;
    using System.Data;
    using System;

    /// <summary>
    /// Summary description for ItemProductPatientConsumptionByUnitRpt.
    /// </summary>
    public partial class ItemProductPatientConsumptionByUnitRpt : Report
    {
        public ItemProductPatientConsumptionByUnitRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeNoLogo(this.pageHeaderSection1);


            //string paymentNo = printJobParameters.FindByParameterName("PaymentNo").ValueString;
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}