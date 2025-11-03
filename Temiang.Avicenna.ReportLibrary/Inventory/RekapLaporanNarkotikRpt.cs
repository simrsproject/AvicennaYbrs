using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.Inventory
{
    /// <summary>
    /// Summary description for PoliklinikDailyRpt.
    /// </summary>
    public partial class RekapLaporanNarkotikRpt : Report
    {
        public RekapLaporanNarkotikRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            Helper.InitializeDataSource(this, programID, printJobParameters);
            InitializeComponent();
        }

    }
}