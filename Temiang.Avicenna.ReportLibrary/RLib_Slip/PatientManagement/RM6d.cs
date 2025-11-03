using System;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Temiang.Avicenna.BusinessObject.Util;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement
{

    /// <summary>
    /// Summary description for RM6d.
    /// </summary>
    public partial class RM6d : Report
    {
        public RM6d(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogoOnlySizeModeNormal(this.pageHeader);
            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            this.DataSource = dtb;

            var healthcare = Healthcare.GetHealthcare();
            
            txtHealthcareName.Value = healthcare.HealthcareName.ToUpper();
            txtAddress1.Value = healthcare.City;
        }
    }
}