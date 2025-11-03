using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class EditPhysicianDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            var btkOk = (Button)Helper.FindControlRecursive(Master, "btnOk");

            string regType = Request.QueryString["rt"];
            if (string.IsNullOrEmpty(regType))
                regType = AppConstant.RegistrationType.ClusterPatient;

            switch (regType)
            {
                case AppConstant.RegistrationType.InPatient:
                    ProgramID = AppConstant.Program.Admitting;
                    tblQue.Visible = false;
                    tblSenders.Visible = false;
                    btkOk.Visible = this.IsUserAddAble || this.IsUserEditAble;
                    break;
                case AppConstant.RegistrationType.OutPatient:
                    ProgramID = AppConstant.Program.OutPatientRegistration;
                    btkOk.Visible = this.IsUserAddAble || this.IsUserEditAble;
                    break;
                case AppConstant.RegistrationType.ClusterPatient:
                    ProgramID = AppConstant.Program.ClusterPatientRegistration;
                    tblSenders.Visible = false;
                    btkOk.Visible = this.IsUserAddAble || this.IsUserEditAble;
                    break;
                case AppConstant.RegistrationType.EmergencyPatient:
                    ProgramID = AppConstant.Program.EmergencyPatientRegistration;
                    tblSenders.Visible = false;
                    btkOk.Visible = this.IsUserAddAble || this.IsUserEditAble;
                    break;
                case AppConstant.RegistrationType.MedicalCheckUp:
                    ProgramID = AppConstant.Program.HealthScreeningRegistration;
                    tblSenders.Visible = false;
                    btkOk.Visible = this.IsUserAddAble || this.IsUserEditAble;
                    break;
                case "tr":
                    ProgramID = AppConstant.Program.ServiceUnitTransaction;
                    tblSenders.Visible = false;
                    btkOk.Visible = this.IsUserAddAble || this.IsUserEditAble;
                    break;
            }

            if (!IsPostBack)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regNo"]);

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                txtRegistrationNo.Text = reg.RegistrationNo;
                txtMedicalNo.Text = pat.MedicalNo;
                txtPatientName.Text = pat.PatientName;

                optSexMale.Checked = (pat.Sex == "M");
                optSexMale.Enabled = (pat.Sex == "M");
                optSexFemale.Checked = (pat.Sex == "F");
                optSexFemale.Enabled = (pat.Sex == "F");

                var room = new ServiceRoom();
                room.LoadByPrimaryKey(reg.RoomID);
                txtRoom.Text = room.RoomName;
                txtBed.Text = reg.BedID;

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(reg.ServiceUnitID);
                txtServiceUnitName.Text = unit.ServiceUnitName;

                if (!string.IsNullOrEmpty(reg.ParamedicID))
                {
                    var param = new Paramedic();
                    txtPhysicianName.Text = param.LoadByPrimaryKey(reg.ParamedicID) ? param.ParamedicName : string.Empty;
                }
                else
                    txtPhysicianName.Text = string.Empty;

                //Title = "Edit Physician for " + reg.RegistrationNo + " - " + pat.PatientName + " [" + pat.MedicalNo + "]";

                if (!string.IsNullOrEmpty(reg.ParamedicID))
                {
                    var query = new ParamedicQuery();
                    query.Where(query.ParamedicID == reg.ParamedicID);

                    cboPhysicianID.DataSource = query.LoadDataTable();
                    cboPhysicianID.DataBind();
                    cboPhysicianID.SelectedValue = reg.ParamedicID;

                    var par = new Paramedic();
                    par.LoadByPrimaryKey(cboPhysicianID.SelectedValue);

                    var sch = new ParamedicScheduleDate();
                    if (sch.LoadByPrimaryKey(reg.ServiceUnitID, cboPhysicianID.SelectedValue,
                                             reg.RegistrationDate.Value.Year.ToString(), reg.RegistrationDate.Value.Date))
                    {
                        var sp = new ServiceUnitParamedic();
                        if (sp.LoadByPrimaryKey(reg.ServiceUnitID, reg.ParamedicID))
                        {
                            if (sp.IsUsingQue ?? false)
                            {
                                cboQue.DataSource = AppointmentSlotTime(reg.ServiceUnitID, cboPhysicianID.SelectedValue, reg.RegistrationDate.Value.Date);
                                cboQue.DataTextField = "Subject";
                                cboQue.DataValueField = "Subject";
                                cboQue.DataBind();

                                foreach (RadComboBoxItem item in cboQue.Items)
                                {
                                    if (item.Text.Contains("[X]"))
                                        item.Attributes.Add("style", "color:red");
                                    else if (item.Text.Contains("[OK]"))
                                        item.Attributes.Add("style", "color:blue");
                                }

                                var ds = ((cboQue.DataSource as DataTable).AsEnumerable().Where(d => d.Field<DateTime>("Start").ToString("HH:mm") == reg.RegistrationTime)).SingleOrDefault();

                                if (ds != null)
                                    cboQue.SelectedValue = ds["Subject"].ToString();
                            }
                        }
                    }
                }

                txtPhysicianSenders.Text = reg.PhysicianSenders;
                txtPhysicianSenders.Enabled = (cboPhysicianID.SelectedValue == AppSession.Parameter.ParamedicIdDokterLuar);

                tblShift.Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH";
                StandardReference.Initialize(cboSRShift, AppEnum.StandardReference.Shift);
                cboSRShift.SelectedValue = reg.SRShift;
            }

        }

        protected void cboPhysicianID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboPhysicianID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ParamedicQuery("a");
            var medic = new ServiceUnitParamedicQuery("b");

            query.es.Top = 30;
            query.InnerJoin(medic).On(
                query.ParamedicID == medic.ParamedicID &&
                medic.ServiceUnitID == Request.QueryString["unitID"]
                );
            query.Where
                (
                    query.ParamedicName.Like(searchTextContain),
                    query.IsActive == true
                );
            query.OrderBy(query.ParamedicName.Ascending);

            cboPhysicianID.DataSource = query.LoadDataTable();
            cboPhysicianID.DataBind();
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind:'";
        }

        public override bool OnButtonOkClicked()
        {
            if (!Page.IsValid)
                return false;

            if (cboPhysicianID.SelectedValue == AppSession.Parameter.ParamedicIdDokterLuar && string.IsNullOrEmpty(txtPhysicianSenders.Text))
            {
                ShowInformationHeader("Physician Senders is required.");
                return false;
            }

            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regNo"]);

            var par = new Paramedic();
            par.LoadByPrimaryKey(cboPhysicianID.SelectedValue);

            if (tblQue.Visible)
            {
                if (reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient ||
                    reg.SRRegistrationType == AppConstant.RegistrationType.MedicalCheckUp)
                {
                    if (reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient)
                    {
                        var sp = new ServiceUnitParamedic();
                        sp.LoadByPrimaryKey(reg.ServiceUnitID, cboPhysicianID.SelectedValue);
                        if ((sp.IsUsingQue ?? false))
                        {
                            if (cboQue.SelectedValue == "0" || string.IsNullOrEmpty(cboQue.SelectedValue))
                            {
                                ShowInformationHeader("Que Slot Number is required.");
                                return false;
                            }
                        }
                    }

                    var appt = new AppointmentQuery();
                    appt.Where(
                        appt.ServiceUnitID == reg.ServiceUnitID,
                        appt.ParamedicID == cboPhysicianID.SelectedValue,
                        appt.AppointmentNo == cboQue.SelectedValue,
                        appt.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel
                        );
                    var tab = appt.LoadDataTable();

                    if (tab.Rows.Count > 0)
                    {
                        if (reg.PatientID != tab.Rows[0]["PatientID"].ToString())
                        {
                            ShowInformationHeader("Que Slot and Registration is invalid.");
                            return false;
                        }
                    }
                    else
                    {
                        var regt = new RegistrationQuery();
                        regt.Where(
                            regt.ServiceUnitID == reg.PatientID,
                            regt.ParamedicID == cboPhysicianID.SelectedValue,
                            regt.RegistrationNo == cboQue.SelectedValue
                            );
                        tab = regt.LoadDataTable();
                        if (tab.Rows.Count > 0)
                        {
                            if (reg.PatientID != tab.Rows[0]["PatientID"].ToString())
                            {
                                ShowInformationHeader("Que Slot and Registration is invalid.");
                                return false;
                            }
                        }
                    }
                }
            }

            bool allowEdit = true;
            string errorMsg = string.Empty;

            if (reg.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient &&
                     AppSession.Parameter.IsAllowRegistrationEmrChangePhysician)
            {
                // allow edit physician
            }
            else
            {
                // tetap bisa ganti dokter kalau belum ada transaksi manual yang menyangkut jasa medis
                var transCItem = new TransChargesItemQuery("a");
                var transC = new TransChargesQuery("b");
                var tcic = new TransChargesItemCompQuery("c");

                transCItem.Select(
                    transCItem.ItemID,
                    transCItem.ChargeQuantity.Sum()
                    );
                transCItem.InnerJoin(transC).On(transC.TransactionNo == transCItem.TransactionNo);
                transCItem.InnerJoin(tcic).On(tcic.TransactionNo == transCItem.TransactionNo &&
                    tcic.SequenceNo == transCItem.SequenceNo);
                transCItem.Where(
                    transC.RegistrationNo == Request.QueryString["regNo"],
                    transC.IsApproved == true,
                    transC.IsAutoBillTransaction == false,
                    tcic.ParamedicID != string.Empty
                    );
                transCItem.GroupBy(transCItem.ItemID);
                transCItem.Having(transCItem.ChargeQuantity.Sum() != 0);
                DataTable dtb = transCItem.LoadDataTable();

                if (dtb.Rows.Count > 0)
                {
                    allowEdit = false;
                    errorMsg = "Edit physician failed. This registration already have service unit transaction.";
                }
            }

            if (allowEdit)
            {
                #region -old-
                //using (var trans = new esTransactionScope())
                //{
                //    var charges = new TransCharges();
                //    var chargesQr = new TransChargesQuery();
                //    chargesQr.Where(chargesQr.RegistrationNo == Request.QueryString["regNo"], chargesQr.IsAutoBillTransaction == true);
                //    chargesQr.es.Top = 1;
                //    if (chargesQr.LoadDataTable().Rows.Count > 0)
                //    {
                //        charges.Load(chargesQr);
                //        var tciccoll = new TransChargesItemCompCollection();
                //        if (!string.IsNullOrEmpty(reg.ParamedicID))
                //        {
                //            tciccoll.Query.Where(
                //            tciccoll.Query.TransactionNo == charges.TransactionNo,
                //            tciccoll.Query.ParamedicID == reg.ParamedicID
                //            );
                //            tciccoll.LoadAll();

                //            foreach (var tcic in tciccoll)
                //            {
                //                tcic.ParamedicID = cboPhysicianID.SelectedValue;

                //                var tci = new TransChargesItem();
                //                tci.LoadByPrimaryKey(tcic.TransactionNo, tcic.SequenceNo);
                //                tci.ParamedicCollectionName = cboPhysicianID.Text;
                //                tci.LastUpdateByUserID = AppSession.UserLogin.UserID;
                //                tci.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                //                tci.Save();
                //            }
                //            tciccoll.Save();
                //        }
                //        else
                //        {
                //            tciccoll.Query.Where(
                //                    tciccoll.Query.TransactionNo == charges.TransactionNo,
                //                    tciccoll.Query.Or(tciccoll.Query.ParamedicID == string.Empty, tciccoll.Query.ParamedicID.IsNull())
                //                    );
                //            tciccoll.LoadAll();

                //            foreach (var tcic in tciccoll)
                //            {
                //                tcic.ParamedicID = cboPhysicianID.SelectedValue;

                //                var tci = new TransChargesItem();
                //                tci.LoadByPrimaryKey(tcic.TransactionNo, tcic.SequenceNo);
                //                tci.ParamedicCollectionName = cboPhysicianID.Text;
                //                tci.LastUpdateByUserID = AppSession.UserLogin.UserID;
                //                tci.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                //                tci.Save();
                //            }
                //            tciccoll.Save();
                //        }

                //        if (AppSession.Parameter.IsPhysicianFeeBasedOnPayment)
                //        {
                //            var x = ParamedicFeeTransChargesItemCompSettled.UpdateSettled(charges, tciccoll, AppSession.UserLogin.UserID);

                //            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                //            feeColl.SetFeeByTCIC(tciccoll, AppSession.UserLogin.UserID);
                //            feeColl.Save();
                //            feeColl.SetPaymentAndInvoicePaymentAfterSave(AppSession.UserLogin.UserID);
                //            feeColl.Save();
                //        }
                //    }

                //    if (reg.SRRegistrationType == AppConstant.RegistrationType.ClusterPatient ||
                //        reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient)
                //    {
                //        var query = new ServiceUnitQueQuery();
                //        if (string.IsNullOrEmpty(reg.ParamedicID))
                //        {
                //            query.Where(
                //            query.ServiceUnitID == reg.ServiceUnitID,
                //            query.ParamedicID == string.Empty,
                //            query.RegistrationNo == reg.RegistrationNo
                //            );
                //        }
                //        else
                //            query.Where(
                //            query.ServiceUnitID == reg.ServiceUnitID,
                //            query.ParamedicID == reg.ParamedicID,
                //            query.RegistrationNo == reg.RegistrationNo
                //            );

                //        var q = new ServiceUnitQue();
                //        if (q.Load(query))
                //        {
                //            q.MarkAsDeleted();
                //            q.Save();

                //            q = new ServiceUnitQue();
                //            q.QueDate = reg.RegistrationDate.Value + TimeSpan.Parse(reg.RegistrationTime);
                //            q.RegistrationNo = reg.RegistrationNo;
                //            q.ParamedicID = cboPhysicianID.SelectedValue;
                //            q.ServiceUnitID = reg.ServiceUnitID;

                //            var sch = new ParamedicScheduleDate();
                //            if (sch.LoadByPrimaryKey(reg.ServiceUnitID, cboPhysicianID.SelectedValue,
                //                                     reg.RegistrationDate.Value.Year.ToString(), reg.RegistrationDate.Value.Date))
                //            {
                //                var sp = new ServiceUnitParamedic();
                //                if (sp.LoadByPrimaryKey(reg.ServiceUnitID, cboPhysicianID.SelectedValue))
                //                {
                //                    q.QueNo = !string.IsNullOrEmpty(cboQue.SelectedValue) ? int.Parse(cboQue.Text.Split('-')[0].Trim()) :
                //                        ServiceUnitQue.GetNewQueNo(reg.ServiceUnitID, cboPhysicianID.SelectedValue, reg.RegistrationDate ?? (new DateTime()).NowAtSqlServer().Date);

                //                    if (!string.IsNullOrEmpty(cboQue.SelectedValue))
                //                    {
                //                        string value = cboQue.Text.Split('-')[1].Substring(1);
                //                        DateTime dt;
                //                        DateTime.TryParse(value, out dt);
                //                        reg.RegistrationTime = dt.ToString("HH:mm");

                //                        q.QueDate = reg.RegistrationDate.Value + TimeSpan.Parse(reg.RegistrationTime);
                //                    }
                //                }
                //                else
                //                    q.QueNo = ServiceUnitQue.GetNewQueNo(reg.ServiceUnitID, cboPhysicianID.SelectedValue, reg.RegistrationDate.Value.Date);
                //            }
                //            else
                //                q.QueNo = ServiceUnitQue.GetNewQueNo(reg.ServiceUnitID, cboPhysicianID.SelectedValue, reg.RegistrationDate.Value.Date);

                //            q.ServiceRoomID = reg.RoomID;
                //            q.IsFromReferProcess = false;
                //            q.StartTime = q.QueDate;
                //            q.IsStopped = false;
                //            q.LastUpdateByUserID = AppSession.UserLogin.UserID;
                //            q.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                //            q.Save();
                //        }

                //        reg.RegistrationQue = q.QueNo;
                //    }

                //    reg.ParamedicID = cboPhysicianID.SelectedValue;
                //    reg.PhysicianSenders = txtPhysicianSenders.Text;

                //    if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
                //        reg.SRShift = cboSRShift.SelectedValue;

                //    reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                //    reg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                //    reg.Save();

                //    if (GuarantorBPJS.Contains(reg.GuarantorID))
                //    {
                //        var bpjs = new BpjsSEP();
                //        bpjs.Query.Where(bpjs.Query.NoSEP == reg.BpjsSepNo);
                //        if (bpjs.Query.Load())
                //        {
                //            var pb = new ParamedicBridging();
                //            pb.Query.Where(pb.Query.ParamedicID == reg.ParamedicID && pb.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(), AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString(), AppEnum.BridgingType.BPJS_PASIEN_UMUM.ToString()));
                //            if (pb.Query.Load())
                //            {
                //                if (bpjs.KodeDpjpPelayanan != pb.BridgingID)
                //                {
                //                    bpjs.KodeDpjpPelayanan = pb.BridgingID;
                //                    bpjs.Save();
                //                }
                //            }
                //        }
                //    }

                //    trans.Complete();
                //}
                #endregion
                Helper.RegistrationOpenClose.EditPhysician(reg, cboPhysicianID.SelectedValue, cboPhysicianID.Text, cboQue.SelectedValue, cboQue.Text, txtPhysicianSenders.Text, cboSRShift.SelectedValue);
            }
            else
                ShowInformationHeader(errorMsg);

            return allowEdit;
        }

        private BusinessObject.AppointmentCollection AppointmentList(string serviceUnitID, string paramedicID)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regNo"]);

            var query = new AppointmentQuery("a");
            var unit = new ServiceUnitQuery("b");
            var medic = new ParamedicQuery("c");
            var patient = new PatientQuery("e");

            query.Select(
                query.AppointmentNo,
                query.AppointmentQue,
                query.AppointmentDate,
                query.AppointmentTime,
                query.PatientName,
                (medic.ParamedicName + "<br />" + unit.ServiceUnitName + "<br />" + query.Notes).As("Description"),
                query.SRAppointmentStatus
                );
            query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
            query.InnerJoin(medic).On(query.ParamedicID == medic.ParamedicID);
            query.LeftJoin(patient).On(query.PatientID == patient.PatientID);

            if (!string.IsNullOrEmpty(serviceUnitID))
                query.Where(query.ServiceUnitID == serviceUnitID);

            if (!string.IsNullOrEmpty(paramedicID))
                query.Where(query.ParamedicID == paramedicID);

            query.Where(
                query.AppointmentDate == reg.RegistrationDate.Value.Date,
                query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel
                );

            var coll = new BusinessObject.AppointmentCollection();
            coll.Load(query);

            return coll;
        }

        private DataTable AppointmentSlotTime(string serviceUnitID, string paramedicID, DateTime date)
        {
            var dtb = new DataTable("AppointmentSlotTime");

            //column
            var dc = new DataColumn("SlotNo", Type.GetType("System.String"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("Start", Type.GetType("System.DateTime"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("End", Type.GetType("System.DateTime"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("Subject", Type.GetType("System.String"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("Description", Type.GetType("System.String"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("OperationalStart", Type.GetType("System.DateTime"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("OperationalEnd", Type.GetType("System.DateTime"));
            dtb.Columns.Add(dc);

            if (!string.IsNullOrEmpty(serviceUnitID) && !string.IsNullOrEmpty(paramedicID))
            {
                DataRow r = dtb.NewRow();
                r[0] = 0;
                r[1] = (new DateTime()).NowAtSqlServer();
                r[2] = (new DateTime()).NowAtSqlServer();
                r[3] = string.Empty;
                r[4] = string.Empty;
                r[5] = (new DateTime()).NowAtSqlServer();
                r[6] = (new DateTime()).NowAtSqlServer();
                dtb.Rows.Add(r);

                var sch = new ParamedicScheduleDateQuery("a");
                var ot = new OperationalTimeQuery("b");
                var par = new ParamedicScheduleQuery("c");

                sch.Select(
                    sch.ScheduleDate,
                    ot.StartTime1,
                    ot.EndTime1,
                    ot.StartTime2,
                    ot.EndTime2,
                    ot.StartTime3,
                    ot.EndTime3,
                    ot.StartTime4,
                    ot.EndTime4,
                    ot.StartTime5,
                    ot.EndTime5,
                    par.ExamDuration
                    );
                sch.InnerJoin(ot).On(sch.OperationalTimeID == ot.OperationalTimeID);
                sch.InnerJoin(par).On(
                    sch.ServiceUnitID == par.ServiceUnitID &&
                    sch.ParamedicID == par.ParamedicID &&
                    sch.PeriodYear == par.PeriodYear
                    );
                sch.Where(
                    sch.ServiceUnitID == serviceUnitID,
                    sch.ParamedicID == paramedicID,
                    sch.PeriodYear == date.Year,
                    sch.ScheduleDate == date
                    );
                var list = sch.LoadDataTable();

                double duration = 0;
                if (list.Rows.Count > 0)
                    duration = Convert.ToDouble(list.Rows[0][11]);

                foreach (DataRow row in list.Rows)
                {
                    //time 1
                    if (row[1].ToString().Trim() != string.Empty && row[2].ToString().Trim() != string.Empty)
                    {
                        var i = 1;
                        var dt1 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[1].ToString().Trim());
                        var dt2 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[2].ToString().Trim());
                        while (dt1 < dt2)
                        {
                            DataRow dr = dtb.NewRow();
                            dr[0] = i;
                            dr[1] = dt1;
                            dr[2] = dt1.AddMinutes(duration);
                            dr[3] = i.ToString() + " - " + dt1.ToString("HH:mm");
                            dr[4] = string.Empty;
                            dr[5] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[1].ToString().Trim());
                            dr[6] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[2].ToString().Trim());
                            dtb.Rows.Add(dr);

                            dt1 = dt1.AddMinutes(duration);
                            i++;
                        }
                    }
                    //time 2
                    if (row[3].ToString().Trim() != string.Empty && row[4].ToString().Trim() != string.Empty)
                    {
                        var i = 1;
                        var dt1 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[3].ToString().Trim());
                        var dt2 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[4].ToString().Trim());
                        while (dt1 < dt2)
                        {
                            DataRow dr = dtb.NewRow();
                            dr[0] = i;
                            dr[1] = dt1;
                            dr[2] = dt1.AddMinutes(duration);
                            dr[3] = i.ToString() + " - " + dt1.ToString("HH:mm");
                            dr[4] = string.Empty;
                            dr[5] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[3].ToString().Trim());
                            dr[6] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[4].ToString().Trim());
                            dtb.Rows.Add(dr);

                            dt1 = dt1.AddMinutes(duration);
                            i++;
                        }
                    }
                    //time 3
                    if (row[5].ToString().Trim() != string.Empty && row[6].ToString().Trim() != string.Empty)
                    {
                        var i = 1;
                        var dt1 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[5].ToString().Trim());
                        var dt2 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[6].ToString().Trim());
                        while (dt1 < dt2)
                        {
                            DataRow dr = dtb.NewRow();
                            dr[0] = i;
                            dr[1] = dt1;
                            dr[2] = dt1.AddMinutes(duration);
                            dr[3] = i.ToString() + " - " + dt1.ToString("HH:mm");
                            dr[4] = string.Empty;
                            dr[5] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[5].ToString().Trim());
                            dr[6] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[6].ToString().Trim());
                            dtb.Rows.Add(dr);

                            dt1 = dt1.AddMinutes(duration);
                            i++;
                        }
                    }
                    //time 4
                    if (row[7].ToString().Trim() != string.Empty && row[8].ToString().Trim() != string.Empty)
                    {
                        var i = 1;
                        var dt1 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[7].ToString().Trim());
                        var dt2 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[8].ToString().Trim());
                        while (dt1 < dt2)
                        {
                            DataRow dr = dtb.NewRow();
                            dr[0] = i;
                            dr[1] = dt1;
                            dr[2] = dt1.AddMinutes(duration);
                            dr[3] = i.ToString() + " - " + dt1.ToString("HH:mm");
                            dr[4] = string.Empty;
                            dr[5] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[7].ToString().Trim());
                            dr[6] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[8].ToString().Trim());
                            dtb.Rows.Add(dr);

                            dt1 = dt1.AddMinutes(duration);
                            i++;
                        }
                    }
                    //time 5
                    if (row[9].ToString().Trim() != string.Empty && row[10].ToString().Trim() != string.Empty)
                    {
                        var i = 1;
                        var dt1 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[9].ToString().Trim());
                        var dt2 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[10].ToString().Trim());
                        while (dt1 < dt2)
                        {
                            DataRow dr = dtb.NewRow();
                            dr[0] = i;
                            dr[1] = dt1;
                            dr[2] = dt1.AddMinutes(duration);
                            dr[3] = i.ToString() + " - " + dt1.ToString("HH:mm");
                            dr[4] = string.Empty;
                            dr[5] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[9].ToString().Trim());
                            dr[6] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[10].ToString().Trim());
                            dtb.Rows.Add(dr);

                            dt1 = dt1.AddMinutes(duration);
                            i++;
                        }
                    }
                }

                var appt = AppointmentList(serviceUnitID, paramedicID);

                foreach (DataRow slot in dtb.Rows)
                {
                    foreach (var entity in from entity in appt let dateTime = entity.AppointmentDate.Value.Date + TimeSpan.Parse(entity.AppointmentTime) where Convert.ToDateTime(slot[1]) == dateTime select entity)
                    {
                        slot[0] = entity.AppointmentNo;
                        slot[3] = entity.AppointmentQue + " - " + entity.AppointmentTime + " - " + entity.GetColumn("PatientName").ToString() + " [A]";
                        break;
                    }
                }

                dtb.AcceptChanges();

                var regs = new RegistrationCollection();

                var query = new RegistrationQuery("a");
                var pq = new PatientQuery("b");

                query.Select(
                    query,
                    pq.PatientName
                    );
                query.InnerJoin(pq).On(query.PatientID == pq.PatientID);
                query.Where(
                    query.RegistrationDate == date,
                    query.ServiceUnitID == serviceUnitID,
                    query.ParamedicID == paramedicID//,
                    //query.IsVoid == false
                    );
                regs.Load(query);

                foreach (var reg in regs)
                {
                    DateTime dateTime = reg.RegistrationDate.Value.Date + TimeSpan.Parse(reg.RegistrationTime);

                    var slot = dtb.AsEnumerable().SingleOrDefault(d => d.Field<string>("SlotNo") == reg.RegistrationQue.ToString() &&
                                                                       d.Field<DateTime>("Start") == dateTime);

                    if (slot != null)
                    {
                        slot[0] = reg.RegistrationNo;
                        if (reg.IsVoid ?? false)
                            slot[3] = slot[3].ToString().Split('-')[0] + "- " + reg.RegistrationTime + " - " + reg.GetColumn("PatientName") + " [X]";
                        else if (reg.IsClosed ?? false)
                            slot[3] = slot[3].ToString().Split('-')[0] + "- " + reg.RegistrationTime + " - " + reg.GetColumn("PatientName") + " [OK]";
                        else
                            slot[3] = slot[3].ToString().Split('-')[0] + "- " + reg.RegistrationTime + " - " + reg.GetColumn("PatientName");
                    }
                }

                dtb.AcceptChanges();
            }

            return dtb;
        }

        protected void cboPhysicianID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //var p = new Paramedic();
            //p.LoadByPrimaryKey(e.Value);
            //cboQue.Enabled = p.IsUsingQue ?? false;

            cboQue.DataTextField = "Subject";
            cboQue.DataValueField = "Subject";

            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regNo"]);

            var sp = new ServiceUnitParamedic();
            if (sp.LoadByPrimaryKey(reg.ServiceUnitID, cboPhysicianID.SelectedValue))
            {
                if (sp.IsUsingQue == true)
                {
                    cboQue.DataSource = AppointmentSlotTime(reg.ServiceUnitID, cboPhysicianID.SelectedValue,
                        reg.RegistrationDate.Value.Date);
                    cboQue.DataBind();

                    foreach (RadComboBoxItem item in cboQue.Items)
                    {
                        if (item.Text.Contains("[X]"))
                            item.Attributes.Add("style", "color:red");
                        else if (item.Text.Contains("[OK]"))
                            item.Attributes.Add("style", "color:blue");
                    }
                }
                else
                {
                    cboQue.SelectedValue = string.Empty;
                    cboQue.Text = string.Empty;
                    cboQue.DataSource = null;
                }
            }
            else
            {
                cboQue.SelectedValue = string.Empty;
                cboQue.Text = string.Empty;
                cboQue.DataSource = null;
            }

            //if (p.IsUsingQue ?? false)
            //{
            //    var reg = new Registration();
            //    reg.LoadByPrimaryKey(Request.QueryString["regNo"]);

            //    cboQue.DataSource = AppointmentSlotTime(reg.ServiceUnitID, cboPhysicianID.SelectedValue,
            //        reg.RegistrationDate.Value.Date);
            //    cboQue.DataBind();

            //    foreach (RadComboBoxItem item in cboQue.Items)
            //    {
            //        if (item.Text.Contains("[X]"))
            //            item.Attributes.Add("style", "color:red");
            //        else if (item.Text.Contains("[OK]"))
            //            item.Attributes.Add("style", "color:blue");
            //    }
            //}
            //else
            //{
            //    cboQue.SelectedValue = string.Empty;
            //    cboQue.Text = string.Empty;
            //    cboQue.DataSource = null;
            //}

            cboQue.DataBind();

            txtPhysicianSenders.Enabled = (e.Value == AppSession.Parameter.ParamedicIdDokterLuar);
            txtPhysicianSenders.Text = string.Empty;
        }

        private static string[] GuarantorBPJS
        {
            get
            {
                var grr = new GuarantorBridgingCollection();
                grr.Query.es.Distinct = true;
                grr.Query.Where(grr.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(), AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString()));
                if (grr.Query.Load()) return grr.Select(g => g.GuarantorID).ToArray();
                else return new string[] { string.Empty };
            }
        }

    }
}
