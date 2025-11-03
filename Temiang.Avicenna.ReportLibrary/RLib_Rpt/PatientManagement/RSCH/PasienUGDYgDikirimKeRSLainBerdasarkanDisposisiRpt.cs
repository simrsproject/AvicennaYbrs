using System;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSCH
{
    public partial class PasienUGDYgDikirimKeRSLainBerdasarkanDisposisiRpt : Report
    {
        public PasienUGDYgDikirimKeRSLainBerdasarkanDisposisiRpt(string programID,
                                                                 PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeNoLogoAlignLeft(pageHeaderSection1);

            String Year = printJobParameters.FindByParameterName("p_PeriodYear").ValueString;

            textBox2.Value = "Year : " + Year;

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;

            var healthCare = Healthcare.GetHealthcare();
            
            textBox18.Value = string.Format("{0}, {1:dd-MM-yyyy}", healthCare.City, DateTime.Now);

            var user = new AppUser();
            user.LoadByPrimaryKey(AppSession.UserLogin.UserID);
            textBox19.Value = user.UserName;

            textBox39.Value = string.Format("NAMA RUMAH SAKIT: {0}", healthCare.HealthcareName);
            textBox40.Value = "KODE RS: " + healthCare.HospitalCode;
        }
    }
}