using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.NursingCare
{
    public partial class ImplementationAndEvaluation : Telerik.Reporting.Report
    {
        public ImplementationAndEvaluation(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            var nh = new NursingTransHD();
            if (nh.LoadByPrimaryKey(printJobParameters[0].ValueString))
            {
                var reg = new Temiang.Avicenna.BusinessObject.Registration();
                if (reg.LoadByPrimaryKey(nh.RegistrationNo))
                {
                    textBox6.Value = reg.RegistrationNo;
                    textBox24.Value = reg.AgeInYear.ToString() + "yr " + reg.AgeInMonth.ToString() + "mth";
                    textBox22.Value = reg.BedID;
                    var su = new ServiceUnit();
                    if (su.LoadByPrimaryKey(reg.ServiceUnitID))
                    {
                        textBox21.Value = su.ServiceUnitName;
                    }
                    var pat = new Patient();
                    if (pat.LoadByPrimaryKey(reg.PatientID))
                    {
                        textBox8.Value = pat.PatientName;
                        textBox12.Value = pat.DateOfBirth.Value.ToString("dd-MM-yyyy");
                        textBox14.Value = pat.MedicalNo;
                        textBox19.Value = pat.Sex;
                    }
                }
            }

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}