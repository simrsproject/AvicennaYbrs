using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSCH
{
    public partial class PasienIGDBerdasarkanKeadaanDatangDetailRpt : Report
    {
        public PasienIGDBerdasarkanKeadaanDatangDetailRpt(string programID,
                                                          PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            var rptdata = new ReportDataSource();
            Helper.InitializeNoLogoAlignLeft(pageHeaderSection1);
            DataTable dtb = rptdata.GetDataTable(programID, printJobParameters);

            DataSource = dtb;

            table1.DataSource = dtb;

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;
            textBox18.Value = string.Format("Tanggal {0:dd-MMM-yyyy} s/d {1:dd-MMM-yyyy}", fromDate, toDate);

            var healthcare = Healthcare.GetHealthcare();
            
            textBox14.Value = string.Format(healthcare.City + ",{0:dd-MM-yyyy}", DateTime.Now.Date);

            var user = new AppUser();
            user.LoadByPrimaryKey(AppSession.UserLogin.UserID);
            textBox27.Value = user.UserName;
        }
    }
}