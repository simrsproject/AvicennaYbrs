using System;
using System.Data;
using System.Web.UI;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Telerik.Web.UI.Map;
using Temiang.Avicenna.BusinessObject;


namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class MedicationAnesthesiaHist : BasePageDialog
    {
        public string BookingNo
        {
            get
            {
                return Request.QueryString["bookingno"];
            }
        }
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
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Monitoring Intra-Operative Anesthesia Drug of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }

                var bk = new ServiceUnitBooking();
                bk.LoadByPrimaryKey(BookingNo);
                txtFromDate.SelectedDate = bk.RealizationDateTimeFrom;

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

            if (!IsPostBack && !txtFromDate.IsEmpty)
                medicationHistCtl.SetDataSource(MedicationReceiveDataTable(),txtFromDate.SelectedDate);
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
            query.Where(query.IsVoid != true, query.RegistrationNo.In(MergeRegistrations));
            query.OrderBy(query.RefTransactionNo.Ascending, query.RefSequenceNo.Ascending, query.ItemDescription.Ascending);

            //if (chkIsOnlyUsed.Checked)
            //{
            //    // Add filter just consumed
            //    var qrUsed = new MedicationReceiveUsedQuery("usd");
            //    qrUsed.Select(qrUsed.MedicationReceiveNo);
            //    qrUsed.es.Distinct = true;
            //    qrUsed.Where(qrUsed.ScheduleDateTime >= txtFromDate.SelectedDate.Value.Date);

            //    query.Where(query.MedicationReceiveNo.In(qrUsed));
            //}

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
            var bk = new ServiceUnitBooking();
            bk.LoadByPrimaryKey(BookingNo);
            txtFromDate.SelectedDate = bk.RealizationDateTimeFrom;

            if (!txtFromDate.IsEmpty)
                medicationHistCtl.SetDataSource(MedicationReceiveDataTable(), txtFromDate.SelectedDate);
        }

    }
}
