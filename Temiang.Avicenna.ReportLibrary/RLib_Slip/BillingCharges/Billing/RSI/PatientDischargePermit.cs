namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.RSI
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.Common;
    using Helper = Helper;

    public partial class PatientDischargePermit : Telerik.Reporting.Report
    {
        public PatientDischargePermit(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeDataSource(this,programID,printJobParameters);

            //var reg = new RegistrationQuery("a");
            //var pat = new PatientQuery("b");
            //var room = new ServiceRoomQuery("c");
            //pat.InnerJoin(reg).On(pat.PatientID == reg.PatientID);
            //pat.InnerJoin(room).On(reg.RoomID == room.RoomID);
            //pat.Select
            //    (
            //    pat.MedicalNo,
            //    reg.RegistrationNo,
            //    pat.PatientName,
            //    room.RoomName
            //    );
            //pat.Where
            //    (
            //        reg.RegistrationNo == printJobParameters[0].ValueString, --> ini klo pake parameter ini kykny gak ketemu, krn klo liat 
            //        reg.IsVoid == false
            //    );

            //var healthcare = Healthcare.GetHealthcare();
            //
            //textBox1.Value = healthcare.HealthcareName;
            //textBox2.Value = healthcare.AddressLine1;

            var user = new AppUser();
            user.LoadByPrimaryKey(AppSession.UserLogin.UserID);
            textBox14.Value = user.UserName;


            //            DataTable dtb = reg.LoadDataTable();
            //            DataSource = dtb;

        }
    }
}