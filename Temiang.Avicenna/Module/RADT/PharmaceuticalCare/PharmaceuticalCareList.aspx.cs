using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Drawing;
using System.Web;
using Telerik.Web.UI;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Interfaces;
using AppStandardReferenceItemQuery = Temiang.Avicenna.BusinessObject.AppStandardReferenceItemQuery;

namespace Temiang.Avicenna.Module.RADT.PharmaceuticalCare
{
    public partial class PharmaceuticalCareList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PharmaceuticalCare;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboRegistrationType, AppEnum.StandardReference.RegistrationType);
                ComboBox.SelectedValue(cboRegistrationType, AppConstant.RegistrationType.InPatient);

                //service unit
                PopulateServiceUnit(cboRegistrationType.SelectedValue);

                if (!string.IsNullOrEmpty(AppSession.UserLogin.ServiceUnitID))
                {
                    ComboBox.SelectedValue(cboServiceUnitID, AppSession.UserLogin.ServiceUnitID);
                }

                txtRegistrationDate.SelectedDate = DateTime.Now.Date;


            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["medno"]))
                {
                    RestoreValueFromCookie();
                }

            }
            //PopulateMenuUdd();
        }
        protected string RegistrationNoteCount(GridItem container)
        {
            var regNo = DataBinder.Eval(container.DataItem, "RegistrationNo").ToString();
            int? noteCount = 0;
            var si = new RegistrationInfoSumary();
            if (si.LoadByPrimaryKey(regNo))
                noteCount = si.NoteCount;
            return (string.Format("<a href=\"#\" title=\"Note\" class=\"noti_Container\" onclick=\"openWinRegistrationInfo('{0}'); return false;\"><span id=\"noti_{0}\" class=\"noti_bubble\">{1}</span></a>",
                                                                    regNo, noteCount > 0 ? noteCount.ToString() : string.Empty));
        }

        private MedicationReceive _prevMrCheck;
        private bool IsMedicationReceiveExist(string regNo)
        {
            if (_prevMrCheck != null && _prevMrCheck.RegistrationNo == regNo)
                return true;

            var mr = new MedicationReceiveQuery("mr");
            mr.Where(mr.RegistrationNo == regNo, mr.IsVoid != true);
            mr.es.Top = 1;

            _prevMrCheck = new MedicationReceive();
            if (_prevMrCheck.Load(mr))
                return true;

            return false;
        }

        private MedicationReceive _prevMrContinueCheck;
        private bool IsMedicationReceiveContinueExist(string regNo)
        {
            if (_prevMrContinueCheck != null && _prevMrContinueCheck.RegistrationNo == regNo)
                return true;

            var mr = new MedicationReceiveQuery("mr");
            mr.Where(mr.RegistrationNo == regNo, mr.IsVoid != true, mr.IsContinue == true);
            mr.es.Top = 1;

            _prevMrContinueCheck = new MedicationReceive();
            if (_prevMrContinueCheck.Load(mr))
                return true;

            return false;
        }
        protected string MedicationConsume(GridItem container, string programID)
        {
            var regNo = DataBinder.Eval(container.DataItem, "RegistrationNo").ToString();
            if (!IsMedicationReceiveContinueExist(regNo))
                return string.Empty;

            // openMedicationConsume(progId, patid, regno, fregno)
            return string.Format("<a href=\"#\" onclick=\"javascript:openMedicationConsume('{0}','{1}','{2}','{3}'); return false;\"><img src=\"../../../Images/Toolbar/drugs16.png\" border=\"0\" alt=\"Confirmed\"  /></a>",
                                                    programID, DataBinder.Eval(container.DataItem, "PatientID"), DataBinder.Eval(container.DataItem, "RegistrationNo"), DataBinder.Eval(container.DataItem, "FromRegistrationNo"));

        }

        protected string DrugRecon(GridItem container, string reconType)
        {
            var regNo = DataBinder.Eval(container.DataItem, "RegistrationNo").ToString();
            if (reconType != "adm" && !IsMedicationReceiveExist(regNo))
                return string.Empty;

            if (reconType != "adm")
            {
                if (!IsMedicationReceiveContinueExist(regNo))
                    return string.Empty;
            }

            var reconName = "Admision";
            var isReconExist = false;
            var itemCount = 0;
            switch (reconType)
            {
                case "adm":
                    {
                        reconName = "Admision";
                        // Admisi bisa diakses walaupun tidak ada penerimaan obat dari pasien krn sudah ditambah menu utk add nya

                        //var mr = new MedicationReceiveQuery("mr");
                        //var hp = new MedicationReceiveFromPatientQuery("hp");
                        //mr.InnerJoin(hp).On(mr.MedicationReceiveNo == hp.MedicationReceiveNo);
                        //mr.Select(mr.AdmissionAppropriateDateTime);
                        //mr.Where(mr.RegistrationNo == regNo);
                        //var dtbCheck = mr.LoadDataTable();

                        //foreach (DataRow row in dtbCheck.Rows)
                        //{
                        //    itemCount++;
                        //    if (!isReconExist && row[0] != DBNull.Value)
                        //    {
                        //        isReconExist = true;
                        //    }
                        //}
                        break;
                    }
                case "trf":
                    {
                        reconName = "Transfer";

                        // Check yg Service Unit nya tidak sama dgn di Reg (posisi terakhir)
                        var mr = new MedicationReceiveQuery("mr");
                        mr.Select(mr.TransferAppropriateDateTime);
                        mr.Where(mr.RegistrationNo == regNo, mr.ServiceUnitID.IsNotNull(), mr.ServiceUnitID != DataBinder.Eval(container.DataItem, "ServiceUnitID").ToString());
                        var dtbCheck = mr.LoadDataTable();

                        itemCount = dtbCheck.Rows.Count;
                        if (itemCount > 0)
                        {
                            // Check sudah ada yg direcon transfer
                            mr = new MedicationReceiveQuery("mr");
                            mr.es.Top = 1;
                            mr.Where(mr.RegistrationNo == regNo, mr.ServiceUnitID == DataBinder.Eval(container.DataItem, "ServiceUnitID").ToString());
                            mr.Select(mr.TransferAppropriateDateTime);
                            dtbCheck = mr.LoadDataTable();
                            isReconExist = dtbCheck.Rows.Count > 0;
                        }

                        break;
                    }
                case "dcg":
                    {
                        reconName = "Discharge";
                        break;
                    }
            }

            var scriptNoti = string.Format("<span id=\"noti_{0}_{1}\" class=\"noti_bubble\">{2}</span>", reconType, regNo, isReconExist || itemCount == 0 ? string.Empty : itemCount.ToString());

            var script = string.Format("<a href=\"#\" class=\"noti_recon_{4}\" title=\"{5} Drug Reconciliation\" onclick=\"javascript:openReconciliation('{4}','{0}','{1}','{2}'); return false;\">{3}</a>",
                                DataBinder.Eval(container.DataItem, "PatientID"),
                                DataBinder.Eval(container.DataItem, "RegistrationNo"),
                                DataBinder.Eval(container.DataItem, "FromRegistrationNo"),
                                scriptNoti, reconType, reconName);


            return script;
        }

        protected string HomePrescription(GridItem container)
        {
            var regNo = DataBinder.Eval(container.DataItem, "RegistrationNo").ToString();

            // Data obay dari recon Discharge
            var lastRecon = new MedicationRecon();
            lastRecon.Query.Where(lastRecon.Query.RegistrationNo == regNo, lastRecon.Query.ReconType == "DCG");
            lastRecon.Query.es.Top = 1;
            lastRecon.Query.OrderBy(lastRecon.Query.ReconSeqNo.Descending);
            if (!lastRecon.Query.Load()) return String.Empty;

            //if (!IsMedicationReceiveExist(regNo))
            //    return string.Empty;

            //if (!IsMedicationReceiveContinueExist(regNo))
            //    return string.Empty;

            // Check apakah sudah ada discharge summary nya 
            var mds = new MedicalDischargeSummary();
            if (!mds.LoadByPrimaryKey(regNo))
            {
                // Tetap bisa reviewk alau2 Discharge Summary dokter belum jalan
                return string.Format("<a href=\"#\" title=\"Home prescription education form\" class=\"noti_homepres\" onclick=\"javascript:openHomePrescription('{0}','{1}','{2}'); return false;\"></a>",
                    DataBinder.Eval(container.DataItem, "PatientID"), DataBinder.Eval(container.DataItem, "RegistrationNo"), DataBinder.Eval(container.DataItem, "FromRegistrationNo"));

            }

            //// Check status review dan Hitung Jumlah obat yg belum direview
            //var mr = new MedicationReceiveQuery("mr");
            //var hp = new HomePrescriptionQuery("hp");
            //mr.LeftJoin(hp).On(mr.MedicationReceiveNo == hp.MedicationReceiveNo);
            //mr.Select(hp.MedicationReceiveNo);
            //mr.Where(mr.RegistrationNo == regNo, mr.IsBroughtHome == true, mr.BalanceQty > 0);
            //var dtbCheck = mr.LoadDataTable();

            //var isRevExist = false;
            //var notRevCount = 0;
            //foreach (DataRow row in dtbCheck.Rows)
            //{
            //    if (row["MedicationReceiveNo"] == DBNull.Value)
            //        notRevCount++;
            //    else if (!isRevExist)
            //        isRevExist = true;
            //}

            //var scriptNoti = string.Format("<span id=\"noti_hp_{0}\" class=\"noti_bubble\">{1}</span>", regNo, (isRevExist || notRevCount == 0 ? string.Empty : notRevCount.ToString()));
            //var script = string.Format("<a href=\"#\" title=\"Home Prescription\" class=\"noti_homepres\" onclick=\"javascript:openHomePrescription('{0}','{1}','{2}'); return false;\">{3}</a>",
            //                DataBinder.Eval(container.DataItem, "PatientID"), DataBinder.Eval(container.DataItem, "RegistrationNo"), DataBinder.Eval(container.DataItem, "FromRegistrationNo"), scriptNoti);

            var script = string.Format("<a href=\"#\" title=\"Home Prescription\" class=\"noti_homepres\" onclick=\"javascript:openHomePrescription('{0}','{1}','{2}'); return false;\"></a>",
                DataBinder.Eval(container.DataItem, "PatientID"), DataBinder.Eval(container.DataItem, "RegistrationNo"), DataBinder.Eval(container.DataItem, "FromRegistrationNo"));

            return script;
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (IsPostBack)
                grdList.DataSource = PatientRow;
            else
                grdList.DataSource = string.Empty;// Untuk mempercepat loading page, data tidak diload saat awal
        }

        private DataTable PatientRow
        {
            get
            {
                DataTable dtb = null;
                using (var tr = new esTransactionScope())
                {
                    dtb = PopulatePatientRow();
                }

                return dtb;

            }
        }

        private DataTable PopulatePatientRow()
        {
            var regType = string.Empty;
            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                var su = new ServiceUnit();
                su.LoadByPrimaryKey(cboServiceUnitID.SelectedValue);
                regType = su.SRRegistrationType;
            }
            else if (!string.IsNullOrEmpty(cboRegistrationType.SelectedValue))
                regType = cboRegistrationType.SelectedValue;

            DataTable dtb;

            switch (regType)
            {
                case AppConstant.RegistrationType.OutPatient:
                    dtb = RowOutPatient;
                    break;
                case AppConstant.RegistrationType.InPatient:
                    dtb = RowInPatient;
                    break;
                case AppConstant.RegistrationType.EmergencyPatient:
                    dtb = RowEmergencyPatient;
                    break;
                case AppConstant.RegistrationType.MedicalCheckUp:
                    dtb = RowMedicalCheckup;
                    break;
                default:
                    // All
                    dtb = RowInPatient;
                    dtb.Merge(RowOutPatient);
                    dtb.Merge(RowEmergencyPatient);
                    dtb.Merge(RowMedicalCheckup);
                    break;
            }

            return dtb;
        }

        private DataTable RowEmergencyPatient
        {
            get
            {
                var unit = new ServiceUnitQuery("b");
                var room = new ServiceRoomQuery("c");
                var medic = new ParamedicQuery("d");
                var query = new RegistrationQuery("e");
                var patient = new PatientQuery("f");
                var grr = new GuarantorQuery("g");
                var sumInfo = new RegistrationInfoSumaryQuery("h");
                var rg = new AppStandardReferenceItemQuery("rg");
                var sal = new AppStandardReferenceItemQuery("sal");
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        room.RoomName,
                        query.RegistrationDate,
                        "<0 AS QueNo>",
                        unit.ServiceUnitID,
                        query.ParamedicID,
                        medic.ParamedicName,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        grr.GuarantorName,
                        query.PatientID,
                        query.IsConsul,
                        query.SRRegistrationType,
                        query.RoomID,
                        "<CAST(0 AS BIT) AS IsEpisodeSOAP>",
                        "<CAST (0 AS BIT) AS IsDiagnosis>",
                        query.BedID,
                        "<'' AS ReferFrom>",
                        "<'' AS ReferTo>",
                        query.RegistrationTime,
                        query.SRTriage,
                        query.RegistrationQue,
                        sumInfo.NoteCount,
                        query.FromRegistrationNo.Coalesce("''").As("FromRegistrationNo"),
                        patient.DateOfBirth,
                        rg.ItemName.As("ReferralGroupName"),
                        sal.ItemName.As("SalutationName")
                    );

                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.LeftJoin(sumInfo).On(query.RegistrationNo == sumInfo.RegistrationNo & sumInfo.NoteCount > 0);
                query.LeftJoin(rg).On(query.SRReferralGroup == rg.ItemID & rg.StandardReferenceID == "ReferralGroup");
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
                query.Where(query.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient, query.IsVoid == false);
                if (cboServiceUnitID.SelectedValue != string.Empty)
                {
                    //var rooms = new ServiceRoomCollection();
                    //rooms.Query.Where(
                    //    rooms.Query.IsOperatingRoom == true,
                    //    rooms.Query.IsActive == true
                    //    );
                    //rooms.LoadAll();

                    //var r = (rooms.Where(i => i.ServiceUnitID == cboServiceUnitID.SelectedValue && i.IsOperatingRoom == true)
                    //              .Select(i => i.ServiceUnitID)).Distinct().SingleOrDefault();

                    //if (r != null)
                    //{
                    //    var booking = new ServiceUnitBookingQuery("x");

                    //    query.InnerJoin(booking).On(query.RegistrationNo == booking.RegistrationNo);
                    //    query.InnerJoin(unit).On(booking.ServiceUnitID == unit.ServiceUnitID);
                    //    query.Where(booking.IsApproved == true);
                    //    query.OrderBy(booking.BookingDateTimeFrom.Ascending);
                    //}
                    //else
                    //{
                    //    query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                    //    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                    //}
                    query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);

                }
                else
                    query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);

                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);

                AddFilterRegOrMedicalNo(query, patient);

                AddFilterPatientName(query);

                AddFilterRegistrationDate(query);

                AddFilterRegistrationClosedStat(query);

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unit.ServiceUnitName;

                query.Select(group.As("Group"));

                query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.RegistrationDate.Descending,
                        query.RegistrationTime.Ascending,
                        query.RegistrationNo.Descending
                    );

                return query.LoadDataTable();
            }
        }

        private void AddFilterRegOrMedicalNo(RegistrationQuery query, PatientQuery patient)
        {
            if (!string.IsNullOrEmpty(txtRegistrationNo.Text))
            {
                if (txtRegistrationNo.Text.Contains("REG"))
                    query.Where(query.RegistrationNo == txtRegistrationNo.Text);
                else
                    query.Where(patient.MedicalNo == txtRegistrationNo.Text);
            }
        }

        private void AddFilterPatientName(RegistrationQuery query)
        {
            if (txtPatientName.Text != string.Empty)
            {
                var searchPatient = "%" + txtPatientName.Text + "%";
                query.Where(string.Format("<RTRIM(f.FirstName+' '+f.MiddleName)+' '+f.LastName LIKE '{0}'>", searchPatient));
            }
        }

        private void AddFilterRegistrationClosedStat(RegistrationQuery query)
        {
            if (!chkIsClosed.Checked)
                query.Where(query.IsClosed == false);
        }

        private void AddFilterRegistrationDate(RegistrationQuery query)
        {
            if (!txtRegistrationDate.IsEmpty)
            {
                query.Where(query.RegistrationDate == txtRegistrationDate.SelectedDate.Value.Date);

                if (txtFromRegistrationTime.Text != "0000" || txtToRegistrationTime.Text != "0000")
                    query.Where(
                        query.RegistrationTime.Between(
                            txtFromRegistrationTime.Text.Substring(0, 2) + ":" +
                            txtFromRegistrationTime.Text.Substring(2, 2),
                            txtToRegistrationTime.Text.Substring(0, 2) + ":" +
                            txtToRegistrationTime.Text.Substring(2, 2)));
            }
        }

        private DataTable RowOutPatient
        {
            get
            {
                var unit = new ServiceUnitQuery("b");
                var room = new ServiceRoomQuery("c");
                var medic = new ParamedicQuery("d");
                var query = new RegistrationQuery("e");
                var patient = new PatientQuery("f");
                var grr = new GuarantorQuery("g");
                var rmb = new RegistrationQuery("rmb");
                var sumInfo = new RegistrationInfoSumaryQuery("h");
                var rg = new AppStandardReferenceItemQuery("rg");
                var sal = new AppStandardReferenceItemQuery("sal");

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        room.RoomName,
                        query.RegistrationDate,
                        query.RegistrationQue,
                        unit.ServiceUnitID,
                        query.ParamedicID,
                        medic.ParamedicName,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        grr.GuarantorName,
                        query.PatientID,
                        query.IsConsul,
                        query.SRRegistrationType,
                        query.RoomID,
                        "<CAST(0 AS BIT) AS IsEpisodeSOAP>",
                        "<CAST (0 AS BIT) AS IsDiagnosis>",
                        query.BedID,
                        @"<CASE WHEN rmb.SRRegistrationType='MCU' THEN 
 (SELECT TOP 1 ServiceUnitName FROM ServiceUnit rs WHERE rs.ServiceUnitID=rmb.ServiceUnitID)
ELSE 
 (SELECT TOP 1 ParamedicName FROM Paramedic rp WHERE rp.ParamedicID=rmb.ParamedicID)
END as ReferFrom>", // Jika refer dari MCU tampilkan nama serviceUnit nya
                    //pmb.ParamedicName.As("ReferFrom"),
                        "<'' AS ReferTo>",
                        query.RegistrationTime,
                        "<'' AS SRTriage>",
                        query.RegistrationQue,
                        sumInfo.NoteCount,
                        query.FromRegistrationNo.Coalesce("''").As("FromRegistrationNo"),
                        patient.DateOfBirth,
                        rg.ItemName.As("ReferralGroupName"),
                        sal.ItemName.As("SalutationName")
                    );

                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.LeftJoin(rmb).On(query.FromRegistrationNo == rmb.RegistrationNo);
                query.LeftJoin(sumInfo).On(query.RegistrationNo == sumInfo.RegistrationNo & sumInfo.NoteCount > 0);

                query.LeftJoin(rg).On(query.SRReferralGroup == rg.ItemID & rg.StandardReferenceID == "ReferralGroup");
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

                query.Where(query.SRRegistrationType == AppConstant.RegistrationType.OutPatient, query.IsVoid == false, query.IsFromDispensary == false);
                if (cboServiceUnitID.SelectedValue != string.Empty)
                {
                    query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                }
                else
                    query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);

                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);


                AddFilterRegOrMedicalNo(query, patient);

                AddFilterPatientName(query);

                AddFilterRegistrationDate(query);

                AddFilterRegistrationClosedStat(query);

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unit.ServiceUnitName;

                query.Select(group.As("Group"));

                query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.RegistrationDate.Descending,
                        query.RegistrationTime.Ascending,
                        query.RegistrationNo.Descending,
                        query.RegistrationQue.Ascending
                    );

                DataTable dtb = query.LoadDataTable();

                foreach (DataRow row in dtb.Rows)
                {
                    var referTo = string.Empty;
                    var mbcoll = new MergeBillingCollection();
                    mbcoll.Query.Where(mbcoll.Query.FromRegistrationNo == row["RegistrationNo"].ToString());
                    mbcoll.LoadAll();
                    foreach (var c in mbcoll)
                    {
                        var r = new Registration();
                        r.LoadByPrimaryKey(c.RegistrationNo);
                        if (r.IsVoid == false)
                        {
                            if (!string.IsNullOrEmpty(r.ParamedicID))
                            {
                                var p = new Paramedic();
                                p.LoadByPrimaryKey(r.ParamedicID);
                                referTo += p.ParamedicName + ";";
                            }
                        }
                    }

                    if (referTo != string.Empty)
                        referTo = referTo.Remove(referTo.Length - 1);
                    row["ReferTo"] = referTo;

                    var phrC = new PatientHealthRecordLineCollection();
                    var phr = new PatientHealthRecordLineQuery("phr");
                    var qf = new QuestionFormQuery("qf");
                    phr.InnerJoin(qf).On(phr.QuestionFormID == qf.QuestionFormID)
                        .Where(phr.RegistrationNo == row["RegistrationNo"].ToString(),
                                    qf.IsVSignForm == true);
                    phr.es.Top = 1;
                    phrC.Load(phr);
                    if (phrC.Count > 0)
                    {
                        row["SRTriage"] = "99";
                    }
                    else
                    {
                        var phrColl = new PatientHealthRecordLineCollection();
                        phrColl.Query.Where(phrColl.Query.RegistrationNo == row["RegistrationNo"].ToString(),
                                        phrColl.Query.QuestionFormID == "PHYEXAM");
                        phrColl.LoadAll();
                        if (phrColl.Count > 0)
                        {
                            row["SRTriage"] = "99";
                        }
                    }

                }
                dtb.AcceptChanges();

                return dtb;
            }
        }

        private DataTable RowInPatient
        {
            get
            {
                var unit = new ServiceUnitQuery("b");
                var room = new ServiceRoomQuery("c");
                var query = new RegistrationQuery("e");
                var patient = new PatientQuery("f");
                var grr = new GuarantorQuery("g");
                var sumInfo = new RegistrationInfoSumaryQuery("h");
                var rg = new AppStandardReferenceItemQuery("rg");
                var sal = new AppStandardReferenceItemQuery("sal");

                var parteam = new ParamedicTeamQuery("pt");
                query.InnerJoin(parteam).On(query.RegistrationNo == parteam.RegistrationNo);

                var medic = new ParamedicQuery("medic");
                query.InnerJoin(medic).On(parteam.ParamedicID == medic.ParamedicID);

                if (cboRegistrationType.SelectedValue == AppConstant.RegistrationType.InPatient &&
                    chkIsIncludeNotInBed.Checked == false)
                {
                    //Tampilkan semua jika hanya yg sedang dirawat yg difilter
                }
                else
                    query.es.Top = AppSession.Parameter.MaxResultRecord;

                if (!chkIsIncludeNotInBed.Checked)
                {
                    // And just patient in room
                    var bed = new BedQuery("bd");
                    query.InnerJoin(bed).On(query.RegistrationNo == bed.RegistrationNo);
                }

                query.Select
                    (
                        room.RoomName,
                        query.RegistrationDate,
                        "<0 AS QueNo>",
                        unit.ServiceUnitID,
                        query.ParamedicID,
                        medic.ParamedicName,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        grr.GuarantorName,
                        query.PatientID,
                        query.IsConsul,
                        query.SRRegistrationType,
                        query.RoomID,
                        "<CAST(0 AS BIT) AS IsEpisodeSOAP>",
                        "<CAST (0 AS BIT) AS IsDiagnosis>",
                        query.BedID,
                        "<'' AS ReferFrom>",
                        "<'' AS ReferTo>",
                        query.RegistrationTime,
                        "<'' AS SRTriage>",
                        query.RegistrationQue,
                        sumInfo.NoteCount,
                        query.FromRegistrationNo.Coalesce("''").As("FromRegistrationNo"),
                        patient.DateOfBirth,
                        rg.ItemName.As("ReferralGroupName"),
                        sal.ItemName.As("SalutationName")
                    );

                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.LeftJoin(sumInfo).On(query.RegistrationNo == sumInfo.RegistrationNo & sumInfo.NoteCount > 0);
                query.LeftJoin(rg).On(query.SRReferralGroup == rg.ItemID & rg.StandardReferenceID == "ReferralGroup");
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
                query.Where(query.SRRegistrationType == AppConstant.RegistrationType.InPatient, query.IsVoid == false);
                if (cboServiceUnitID.SelectedValue != string.Empty)
                {
                    query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                }
                else
                    query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);

                // Filter dokter dpjp nya saja
                query.Where(parteam.SRParamedicTeamStatus == AppParameter.GetParameterValue(AppParameter.ParameterItem.ParamedicTeamStatusDpjpID));

                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(parteam.ParamedicID == cboParamedicID.SelectedValue);

                AddFilterRegOrMedicalNo(query, patient);

                AddFilterPatientName(query);

                // Inpatient perlu difilter RegDate nya juga untuk tahu pasien mana aja yg baru di regis hari ini untuk keperluan UDD Setup (Info: Imel for RSYS, by: Handono 230316) 
                AddFilterRegistrationDate(query);

                AddFilterRegistrationClosedStat(query);

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unit.ServiceUnitName;

                query.Select(group.As("Group"));

                query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.RegistrationDate.Descending,
                        query.RegistrationTime.Ascending,
                        query.RegistrationNo.Descending
                    );

                var dtbl = query.LoadDataTable();

                return dtbl;
            }
        }

        private DataTable RowMedicalCheckup //khusus MCU unitnya diambil dari ToServiceUnitID di transcharges, selain itu unit diambil dari registrasi
        {
            get
            {
                var unit = new ServiceUnitQuery("b");
                var room = new ServiceRoomQuery("c");
                var medic = new ParamedicQuery("d");
                var query = new RegistrationQuery("e");
                var patient = new PatientQuery("f");
                var grr = new GuarantorQuery("g");
                var qusr = new AppUserServiceUnitQuery("u");
                var tc = new TransChargesQuery("h");
                var tci = new TransChargesItemQuery("i");
                var rg = new AppStandardReferenceItemQuery("rg");
                var sal = new AppStandardReferenceItemQuery("sal");

                var file = new MedicalRecordFileStatusMovementQuery("z");
                query.LeftJoin(file).On(
                    query.RegistrationNo == file.RegistrationNo &&
                    query.ServiceUnitID == file.LastPositionServiceUnitID
                    );

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.es.Distinct = true;

                query.Select
                    (
                        room.RoomName,
                        query.RegistrationDate,
                        "<1 AS QueNo>",
                        tc.ToServiceUnitID.As("ServiceUnitID"),
                        query.ParamedicID,
                        medic.ParamedicName,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        grr.GuarantorName,
                        query.PatientID,
                        query.IsConsul,
                        query.SRRegistrationType,
                        query.RoomID,
                        file.LastPositionUserID.Coalesce("''"),
                        query.IsNewPatient,
                    "<0 AS NoteCount>",
                        query.FromRegistrationNo.Coalesce("''").As("FromRegistrationNo"),
                        patient.DateOfBirth,
                        rg.ItemName.As("ReferralGroupName"),
                        sal.ItemName.As("SalutationName")
                    );

                query.InnerJoin(tc).On(query.RegistrationNo == tc.RegistrationNo);
                query.InnerJoin(tci).On(tc.TransactionNo == tci.TransactionNo);
                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.InnerJoin(unit).On(tc.ToServiceUnitID == unit.ServiceUnitID);
                query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                query.LeftJoin(rg).On(query.SRReferralGroup == rg.ItemID & rg.StandardReferenceID == "ReferralGroup");
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
                query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                //query.Where(tc.PackageReferenceNo.IsNotNull());

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(tc.ToServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);

                AddFilterRegOrMedicalNo(query, patient);

                AddFilterPatientName(query);

                AddFilterRegistrationDate(query);

                AddFilterRegistrationClosedStat(query);

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unit.ServiceUnitName;

                query.Select(group.As("Group"));
                query.Where
                    (
                        query.SRRegistrationType == AppConstant.RegistrationType.MedicalCheckUp,
                        query.IsVoid == false
                    );

                query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.RegistrationDate.Descending,
                        query.RegistrationNo.Ascending
                    );

                return query.LoadDataTable();
            }
        }


        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.CurrentPageIndex = 0;
            grdList.Rebind();
            SaveValueToCookie();
        }

        protected string ColorOfTriase(object SRTriage)
        {
            var color = "Transparant";
            switch (SRTriage.ToString())
            {
                case "01":
                    {
                        color = "Red";
                        break;
                    }
                case "02":
                    {
                        color = "Yellow";
                        break;
                    }
                case "03":
                    {
                        color = "Yellow";
                        break;
                    }
                case "04":
                    {
                        color = "Green";
                        break;
                    }
                case "05":
                    {
                        color = "Black";
                        break;
                    }
                case "99": // pasien rawat jalan yg sudah dilakukan PHYEXAM
                    {
                        color = "Blue";
                        break;
                    }
            }

            return color;
        }

        protected string SoapEntryStatuslHtml(GridItem container)
        {
            var regno = DataBinder.Eval(container.DataItem, "RegistrationNo").ToString();
            var parid = DataBinder.Eval(container.DataItem, "ParamedicID").ToString();
            var regtype = DataBinder.Eval(container.DataItem, "SRRegistrationType").ToString();

            // Cek di Integrated Note
            var rimQr = new RegistrationInfoMedicQuery();
            rimQr.es.Top = 1;
            rimQr.es.WithNoLock = true;

            rimQr.Where(rimQr.RegistrationNo == regno,
                rimQr.Or(rimQr.IsDeleted.IsNull(), rimQr.IsDeleted == false), rimQr.SRMedicalNotesInputType == "SOAP", rimQr.Info1 != string.Empty);

            if (regtype == AppConstant.RegistrationType.InPatient)
            {
                // Untuk in patient cek hari ini apakah sudah diisi soapnya
                rimQr.Where(rimQr.ParamedicID == parid, rimQr.DateTimeInfo > DateTime.Today);
            }

            var rim = new RegistrationInfoMedic();
            if (rim.Load(rimQr))
            {
                return string.Format("<img src='{0}/Images/Toolbar/post_yellow_16.png'/>", Helper.UrlRoot());
            }
            return string.Empty;
        }
        private void PopulateServiceUnit(string registrationType)
        {
            var units = new ServiceUnitCollection();
            var query = new ServiceUnitQuery("a");

            if (string.IsNullOrEmpty(registrationType))
                query.Where(
                    query.SRRegistrationType.In(
                        AppConstant.RegistrationType.InPatient,
                        AppConstant.RegistrationType.EmergencyPatient,
                        AppConstant.RegistrationType.OutPatient,
                        AppConstant.RegistrationType.MedicalCheckUp
                    ),
                    query.IsActive == true
                );
            else
                query.Where(
                    query.SRRegistrationType == registrationType,
                    query.IsActive == true
                );

            query.OrderBy(units.Query.ServiceUnitName.Ascending);
            units.Load(query);

            cboServiceUnitID.Items.Clear();
            cboServiceUnitID.Text = string.Empty;
            cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (ServiceUnit entity in units)
            {
                cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
            }

            if (!string.IsNullOrEmpty(AppSession.UserLogin.ServiceUnitID))
            {
                ComboBox.SelectedValue(cboServiceUnitID, AppSession.UserLogin.ServiceUnitID);
            }
        }

    }
}
