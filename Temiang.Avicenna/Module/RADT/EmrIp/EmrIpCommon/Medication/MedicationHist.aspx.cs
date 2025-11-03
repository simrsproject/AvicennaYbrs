using System;
using System.Data;
using System.Web.UI;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Telerik.Web.UI.Map;
using Temiang.Avicenna.BusinessObject;


namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class MedicationHist : BasePageDialog
    {

        public string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        public string FromRegistrationNo
        {
            get
            {
                return Request.QueryString["fregno"];
            }
        }

        public string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (AppConstant.Program.PharmaceuticalCare.Equals(Request.QueryString["prgid"]))
                ProgramID = AppConstant.Program.PharmaceuticalCare;
            else
                ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Service Unit Kardex of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }

                var reg = new Registration();
                reg.LoadByPrimaryKey(!string.IsNullOrEmpty(FromRegistrationNo) ? FromRegistrationNo : RegistrationNo);
                txtFromDate.SelectedDate = reg.RegistrationDate;

                lblPatientName.Text = pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                lblSex.Text = pat.Sex;
                if (pat.DateOfBirth != null)
                {
                    var birthDate = pat.DateOfBirth.Value;
                    lblBirthDate.Text = birthDate.ToString(AppConstant.DisplayFormat.Date);

                    lblAge.Text = string.Format("{0}Y, {1}M, {2}D",
                        Helper.GetAgeInYear(birthDate, DateTime.Today), Helper.GetAgeInMonth(birthDate, DateTime.Today),
                        Helper.GetAgeInDay(birthDate, DateTime.Today));
                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonCancel.Text = "Close";
            ButtonOk.Visible = false;

            if (!IsPostBack)
                medicationHistCtl.SetDataSource(MedicationReceiveDataTable(), txtFromDate.SelectedDate);
        }

        #region MedicationReceive

        private DataTable MedicationReceiveDataTable()
        {
            var query = new MedicationReceiveQuery("a");
            var reg = new RegistrationQuery("r");
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);

            var cm = new ConsumeMethodQuery("cm");
            query.LeftJoin(cm).On(query.SRConsumeMethod == cm.SRConsumeMethod);

            var item = new ItemProductMedicQuery("im");

            if (chkIsAntibiotic.Checked)
            {
                // Add filter just antibiotik
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.Where(item.IsAntibiotic==true);
            }
            else
                query.LeftJoin(item).On(query.ItemID == item.ItemID);

            var mc = new AppStandardReferenceItemQuery("mc");
            query.LeftJoin(mc).On(query.SRMedicationConsume == mc.ItemID &&
                                  mc.StandardReferenceID == "MedicationConsume");

            query.Select
            (
                query,
                reg.PatientID,
                cm.SRConsumeMethodName,
                item.IsAntibiotic,
                mc.ItemName.As("SRMedicationConsumeName")

            );
            //query.Where(query.IsVoid != true, query.Or(query.RegistrationNo == RegistrationNo, query.RegistrationNo == FromRegistrationNo));
            // Tampilkan 1 episodenya
            query.Where(query.IsVoid != true, query.RegistrationNo.In(MergeRegistrations));
            query.OrderBy(query.RefTransactionNo.Ascending, query.RefSequenceNo.Ascending, query.ItemDescription.Ascending);

            if (chkIsOnlyUsed.Checked)
            {
                // Add filter just consumed
                var qrUsed = new MedicationReceiveUsedQuery("usd");
                qrUsed.Select(qrUsed.MedicationReceiveNo);
                qrUsed.es.Distinct = true;
                qrUsed.Where(qrUsed.ScheduleDateTime >= txtFromDate.SelectedDate.Value.Date);

                query.Where(query.MedicationReceiveNo.In(qrUsed));
            }


            var dtb = query.LoadDataTable();



            return dtb;

        }



        #endregion




        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            medicationHistCtl.SetDataSource(MedicationReceiveDataTable(), txtFromDate.SelectedDate);
        }

        protected void btnStartFromRegistration_Click(object sender, EventArgs e)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(!string.IsNullOrEmpty(FromRegistrationNo) ? FromRegistrationNo : RegistrationNo);
            txtFromDate.SelectedDate = reg.RegistrationDate;

            medicationHistCtl.SetDataSource(MedicationReceiveDataTable(), txtFromDate.SelectedDate);
        }



        protected void btnStartFromLast3Day_Click(object sender, EventArgs e)
        {
            txtFromDate.SelectedDate = DateTime.Today.AddDays(-3);
            medicationHistCtl.SetDataSource(MedicationReceiveDataTable(), txtFromDate.SelectedDate);
        }

        protected void btnStartFromCurrentDay_Click(object sender, EventArgs e)
        {
            txtFromDate.SelectedDate = DateTime.Today;
            medicationHistCtl.SetDataSource(MedicationReceiveDataTable(), txtFromDate.SelectedDate);
        }

        protected void btnStartFromLast1Day_Click(object sender, EventArgs e)
        {
            txtFromDate.SelectedDate = DateTime.Today.AddDays(-1);
            medicationHistCtl.SetDataSource(MedicationReceiveDataTable(), txtFromDate.SelectedDate);
        }
    }
}
