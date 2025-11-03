using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using System.Data;
using System;

namespace Temiang.Avicenna.ReportLibrary.Charges
{

    public partial class ServiceRpt : Telerik.Reporting.Report
    {
        public ServiceRpt(string programID, PrintJobParameterCollection printJobParameters)
        {

            InitializeComponent();
            Helper.InitializeLogo(pageHeader);
            Helper.InitializeDataSource(this, programID, printJobParameters);
            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;
            String Unit = printJobParameters.FindByParameterName("p_ServiceUnitID").ValueString;

            ServiceUnit a = new ServiceUnit();
            if (a.LoadByPrimaryKey(Unit))
                textBox5.Value = "LAPORAN LAYANAN "+a.ServiceUnitName;
            else textBox5.Value = "LAPORAN LAYANAN";

            textBox1.Value = string.Format("Tanggal : {0:dd-MMMM-yyyy} s/d {1:dd-MMMM-yyyy}", fromDate, toDate);
            textBox5.Value = a.ServiceUnitName;

        }
    }
}