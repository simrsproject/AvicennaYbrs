using System;
using System.Data;
using Telerik.Reporting;
using Telerik.Reporting.Drawing;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using Temiang.Avicenna.ReportLibrary.Properties;

namespace Temiang.Avicenna.ReportLibrary.Inventory
{
    public partial class PoReturRpt : Report
    {
        public PoReturRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
 
            // ATTENTION : DESIGN report ini digunakan untuk 2 laporan yaitu: 
            // 1. Laporan Retur Farmasi Ke Supplier 
            // 2. Retur Barang Belum diproses

            InitializeComponent();

            Helper.InitializeLogo(pageHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            txtPeriod.Value = string.Format("Periode : {0:dd MMMM yyyy} s/d {1:dd MMMM yyyy}", fromDate, toDate);
        }

    }
}