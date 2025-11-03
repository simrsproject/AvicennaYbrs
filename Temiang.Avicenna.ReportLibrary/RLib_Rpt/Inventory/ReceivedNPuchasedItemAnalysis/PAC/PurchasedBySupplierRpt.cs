using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Inventory.ReceivedNPuchasedItemAnalysis.PAC
{
    public partial class PurchasedBySupplierRpt : Report
    {
        public PurchasedBySupplierRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogo(pageHeader);
            Helper.InitializeDataSource(this, programID, printJobParameters);


            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            //var type = new AppStandardReferenceItem();
            //type.LoadByPrimaryKey("ItemType", printJobParameters.FindByParameterName("p_ItemType").ValueString);
            //textBox46.Value = type.ItemName;
            txtPeriod.Value = string.Format("Tanggal {0:dd-MMM-yyyy} s/d {1:dd-MMM-yyyy}", fromDate, toDate);
        }
    }
}