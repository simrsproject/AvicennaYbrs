namespace Temiang.Avicenna.ReportLibrary.Charges
{
    using Telerik.Reporting;
    using BusinessObject;
    using System.Data;

    /// <summary>
    /// Summary description for BillingSummaryShort.
    /// </summary>
    public partial class BillingInformationRpt : Report
    {
        public BillingInformationRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            string registrationNo = printJobParameters.FindByParameterName("RegistrationNoList").ValueString;
            string[] registrationNoList = new string[1];
            if (registrationNo.Contains(","))
                registrationNoList = registrationNo.Split(',');

            decimal? downPayment = printJobParameters.FindByParameterName("DownPayment").ValueNumeric;
            decimal? paymentAmount = printJobParameters.FindByParameterName("PaymentAmount").ValueNumeric;
            if (paymentAmount > 0)
                paymentAmount -= downPayment;

            #region charges
            CostCalculationQuery cost = new CostCalculationQuery("a");
            RegistrationQuery reg = new RegistrationQuery("b");
            PatientQuery patient = new PatientQuery("c");
            ServiceRoomQuery room = new ServiceRoomQuery("f");
            AppParameterQuery param= new AppParameterQuery("d");

            cost.Select
                (
                // header
                    reg.RegistrationNo,
                    patient.SRTitle,
                    patient.PatientName,
                    room.RoomName.Coalesce("''"),

                //detail
                      reg.AdministrationAmount,
                    "<" + downPayment + " AS DownPayment>",
                    "<" + paymentAmount + " AS 'PaymentAmount'>",
                    (cost.PatientAmount + cost.GuarantorAmount).As("Total")

               // footer
                );

            // header
            cost.InnerJoin(reg).On(cost.RegistrationNo == reg.RegistrationNo);
            cost.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            cost.LeftJoin(room).On(reg.RoomID == room.RoomID);
            
            if (registrationNo.Contains(","))
                cost.Where(cost.RegistrationNo.In(registrationNoList));
            else
                cost.Where(cost.RegistrationNo == registrationNo);

            cost.Where(
                cost.Or(
                    cost.ParentNo == string.Empty,
                    cost.ParentNo.IsNull()
                    )
                );

            #endregion

            this.DataSource = cost.LoadDataTable();

            textBox18.Value = printJobParameters.FindByParameterName("FinanceHeadJob").ValueString;
            NamaKaSubKeuangan.Value = printJobParameters.FindByParameterName("FinanceHead").ValueString;
            textBox24.Value = printJobParameters.FindByParameterName("FinanceHeadID").ValueString;          
        }
    }
}