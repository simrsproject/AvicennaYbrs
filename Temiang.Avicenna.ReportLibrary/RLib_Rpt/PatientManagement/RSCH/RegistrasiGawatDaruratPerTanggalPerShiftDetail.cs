using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSCH
{
    /// <summary>
    /// Summary description for PasienGawatDaruratBerdasarkanPengirimDetail.
    /// </summary>
    public partial class RegistrasiGawatDaruratPerTanggalPerShiftDetail : Report
    {
        public RegistrasiGawatDaruratPerTanggalPerShiftDetail(string programID,
                                                              PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogo(pageHeaderSection1);
            var rptdata = new ReportDataSource();

            DataTable dtb = rptdata.GetDataTable(programID, printJobParameters);

            DataSource = dtb;

            table1.DataSource = dtb;

            var healthcare = Healthcare.GetHealthcare();
            
            textBox275.Value = string.Format("{0}, {1:dd-MM-yyyy}", healthcare.AddressLine2, DateTime.Now.Date);

            var user = new AppUser();
            user.LoadByPrimaryKey(AppSession.UserLogin.UserID);
            textBox358.Value = user.UserName;
        }
    }
}