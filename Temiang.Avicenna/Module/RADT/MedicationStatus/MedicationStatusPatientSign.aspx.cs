using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.MedicationStatus
{
    public partial class MedicationStatusPatientSign : BasePageDialog
    {
        protected bool IsModeViewHist => (Request.QueryString["md"] == "view");
        protected int SignId => Request.QueryString["signid"].ToInt();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (AppConstant.Program.PharmaceuticalCare.Equals(Request.QueryString["prgid"]))
                    ProgramID = AppConstant.Program.PharmaceuticalCare;
                else
                    ProgramID = AppConstant.Program.ElectronicMedicalRecord;


                this.Title = "Medication Realization Patient Sign";
            }


            if (IsModeViewHist)
            {
                ButtonOk.Visible = false;
                ButtonCancel.Text = "Close";
                btnPatientSign.Enabled = false;
                grdMedication.Columns[0].Visible = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (IsModeViewHist)
                {
                    var psign = new SignPool();
                    if (psign.LoadByPrimaryKey(SignId))
                    {
                        PopulateSignImage(imgPatientSign, hdnPatientSign, psign.SignImg);
                        if (psign.SignDateTime != null)
                            lblSignDate.Text =
                                psign.SignDateTime.Value.ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute);
                    }
                }
                else
                    lblSignDate.Text = DateTime.Now.ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute);
            }
        }


        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        private void PopulateSignImage(RadBinaryImage rbImage, HiddenField hdnImage, Byte[] val)
        {
            rbImage.DataValue = val;
            if (val == null)
                hdnImage.Value = String.Empty;
            else
            {
                var mstream = new System.IO.MemoryStream(val);
                var img = new Telerik.Web.UI.ImageEditor.EditableImage(mstream);
                var imgHelper = new ImageHelper();
                hdnImage.Value = imgHelper.ToBase64String(img.Image, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private DataTable MedicationPendingSign()
        {
            var query = new MedicationReceiveQuery("a");
            var used = new MedicationReceiveUsedQuery("usd");
            query.InnerJoin(used).On(query.MedicationReceiveNo == used.MedicationReceiveNo);

            var cm = new ConsumeMethodQuery("cm");
            query.LeftJoin(cm).On(query.SRConsumeMethod == cm.SRConsumeMethod);

            var mc = new AppStandardReferenceItemQuery("mc");
            query.LeftJoin(mc).On(query.SRMedicationConsume == mc.ItemID &&
                                  mc.StandardReferenceID == "MedicationConsume");

            var tp = new TransPrescriptionQuery("tp");
            query.LeftJoin(tp).On(query.RefTransactionNo == tp.PrescriptionNo);

            var ipm = new ItemProductMedicQuery("ipm");
            query.LeftJoin(ipm).On(query.ItemID == ipm.ItemID);

            var realize = new AppUserQuery("u3");
            query.LeftJoin(realize).On(used.RealizedByUserID == realize.UserID);

            query.Select
            (
                query.MedicationReceiveNo,
                query.ItemID,
                query.ConsumeQty,
                query.SRConsumeUnit,
                query.ItemDescription,
                used.SequenceNo,
                used.ScheduleDateTime,
                used.RealizedDateTime,
                used.IsNotConsume,
                used.IsReSchedule,
                used.Qty,
                used.Note,
                cm.SRConsumeMethodName,
                mc.ItemName.As("SRMedicationConsumeName"),
                realize.UserName.As("RealizedByUserName"),
                ipm.IsAntibiotic.Coalesce("1").As("IsAntibiotic"),
                "<CONVERT(BIT,1) as IsSelect>"
            );

            query.Where(query.RegistrationNo.In(MergeRegistrations));

            // Filter hanya yg sudah RealizedDateTime sampai 12 jam sebelumnya dan belum di TTD
            query.Where(used.RealizedDateTime.IsNotNull(), used.RealizedDateTime > DateTime.Now.AddHours(-12),
                used.PatientSignID.IsNull());

            query.OrderBy(query.ItemDescription.Ascending, used.RealizedDateTime.Ascending);

            var dtbMedRec = query.LoadDataTable();
            return dtbMedRec;
        }

        private DataTable MedicationPendingSigned(int signId)
        {
            var query = new MedicationReceiveQuery("a");
            var used = new MedicationReceiveUsedQuery("usd");
            query.InnerJoin(used).On(query.MedicationReceiveNo == used.MedicationReceiveNo);

            var cm = new ConsumeMethodQuery("cm");
            query.LeftJoin(cm).On(query.SRConsumeMethod == cm.SRConsumeMethod);

            var mc = new AppStandardReferenceItemQuery("mc");
            query.LeftJoin(mc).On(query.SRMedicationConsume == mc.ItemID &&
                                  mc.StandardReferenceID == "MedicationConsume");

            var tp = new TransPrescriptionQuery("tp");
            query.LeftJoin(tp).On(query.RefTransactionNo == tp.PrescriptionNo);

            var ipm = new ItemProductMedicQuery("ipm");
            query.LeftJoin(ipm).On(query.ItemID == ipm.ItemID);

            var realize = new AppUserQuery("u3");
            query.LeftJoin(realize).On(used.RealizedByUserID == realize.UserID);

            query.Select
            (
                query.MedicationReceiveNo,
                query.ItemID,
                query.ConsumeQty,
                query.SRConsumeUnit,
                query.ItemDescription,
                used.SequenceNo,
                used.ScheduleDateTime,
                used.RealizedDateTime,
                used.IsNotConsume,
                used.IsReSchedule,
                used.Qty,
                used.Note,
                cm.SRConsumeMethodName,
                mc.ItemName.As("SRMedicationConsumeName"),
                realize.UserName.As("RealizedByUserName"),
                ipm.IsAntibiotic.Coalesce("1").As("IsAntibiotic"),
                "<CONVERT(BIT,1) as IsSelect>"
            );

            query.Where(query.RegistrationNo.In(MergeRegistrations));

            // Hist TT
            query.Where(used.PatientSignID == signId);

            query.OrderBy(query.ItemDescription.Ascending, used.RealizedDateTime.Ascending);

            var dtbMedRec = query.LoadDataTable();
            return dtbMedRec;
        }

        protected void grdMedication_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdMedication.DataSource = IsModeViewHist ? MedicationPendingSigned(SignId) : MedicationPendingSign();
        }

        protected override void OnButtonOkClicked(ValidateArgs args)
        {

            // Check ada yg perlu di TTD
            var isContinue = false;
            foreach (GridDataItem item in grdMedication.MasterTableView.Items)
            {
                var mrno = item.GetDataKeyValue("MedicationReceiveNo").ToInt();
                var seqno = item.GetDataKeyValue("SequenceNo").ToInt();
                var reqForm = Request.Form[string.Format("chkOnOff_{0}_{1}", mrno, seqno)];
                var isSelect = reqForm != null && "on".Equals(reqForm.ToLower());
                if (isSelect)
                {
                    isContinue = true;
                    break;
                }
            }

            if (!isContinue)
            {
                args.IsCancel = true;
                args.MessageText = "Please select meditaion item first";
                return;
            }

            var imgHelper = new ImageHelper();

            // Patient / Family Sign
            if (!string.IsNullOrWhiteSpace(hdnPatientSign.Value))
            {
                // Add new Sign
                var sp = new SignPool();
                sp.SignByID = PatientID;
                sp.SignDateTime = DateTime.Now;

                var resized = imgHelper.ResizeImage(imgHelper.ToImage(hdnPatientSign.Value), new Size(332, 185));
                sp.SignImg = imgHelper.ToByteArray(resized, ImageFormat.Png);
                sp.Save();

                var signID = sp.SignID; //Otomatis terisi setelah save, SignID IDENTITY -> true

                // Update SignID in MedicationUsed
                foreach (GridDataItem item in grdMedication.MasterTableView.Items)
                {
                    var mrno = item.GetDataKeyValue("MedicationReceiveNo").ToInt();
                    var seqno = item.GetDataKeyValue("SequenceNo").ToInt();
                    var reqForm = Request.Form[string.Format("chkOnOff_{0}_{1}", mrno, seqno)];
                    var isSelect = reqForm != null && "on".Equals(reqForm.ToLower());
                    if (isSelect)
                    {
                        var medUsed = new MedicationReceiveUsed();
                        if (medUsed.LoadByPrimaryKey(mrno, seqno))
                        {
                            medUsed.PatientSignID = signID;
                            medUsed.Save();
                        }
                    }
                }

            }
        }
    }
}