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

namespace Temiang.Avicenna.Module.RADT.CardexMonitoring
{
    public partial class CardexMonitoringList : Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
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
                    dtb = RowOutPatient();
                    break;
                case AppConstant.RegistrationType.InPatient:
                    dtb = RowInPatient();
                    break;
                case AppConstant.RegistrationType.EmergencyPatient:
                    dtb = RowEmergencyPatient();
                    break;
                case AppConstant.RegistrationType.MedicalCheckUp:
                    dtb = RowMedicalCheckup();
                    break;
                default:
                    // All
                    dtb = RowInPatient();
                    dtb.Merge(RowOutPatient());
                    dtb.Merge(RowEmergencyPatient());
                    dtb.Merge(RowMedicalCheckup());
                    break;
            }

            return dtb;
        }

        private DataTable RowEmergencyPatient()
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
                unit.ServiceUnitName,
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
                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
            }
            else
                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);

            if (cboParamedicID.SelectedValue != string.Empty)
                query.Where(query.ParamedicID == cboParamedicID.SelectedValue);

            if (!string.IsNullOrWhiteSpace(txtBedID.Text))
                query.Where(query.BedID == txtBedID.Text);

            AddFilterRegOrMedicalNo(query, patient);

            AddFilterPatientName(query);

            AddFilterRegistrationDate(query);

            AddFilterRegistrationClosedStat(query);

            query.OrderBy
                (
                    query.RegistrationDate.Descending,
                    query.RegistrationNo.Descending
                );

            return query.LoadDataTable();
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

        private DataTable RowOutPatient()
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
                unit.ServiceUnitName,
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

            query.OrderBy
                (
                    query.RegistrationDate.Descending,
                    query.RegistrationNo.Descending
                );

            DataTable dtb = query.LoadDataTable();

            foreach (DataRow row in dtb.Rows)
            {
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

        private DataTable RowInPatient()
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
                unit.ServiceUnitName,
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

            if (!string.IsNullOrWhiteSpace(txtBedID.Text))
                query.Where(query.BedID == txtBedID.Text);

            AddFilterRegOrMedicalNo(query, patient);

            AddFilterPatientName(query);

            // Inpatient perlu difilter RegDate nya juga untuk tahu pasien mana aja yg baru di regis hari ini untuk keperluan UDD Setup (Info: Imel for RSYS, by: Handono 230316) 
            AddFilterRegistrationDate(query);

            AddFilterRegistrationClosedStat(query);

            query.OrderBy
                (
                    query.RegistrationDate.Descending,
                    query.RegistrationNo.Descending
                );

            var dtbl = query.LoadDataTable();

            return dtbl;
        }

        private DataTable RowMedicalCheckup() //khusus MCU unitnya diambil dari ToServiceUnitID di transcharges, selain itu unit diambil dari registrasi
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
            var sumInfo = new RegistrationInfoSumaryQuery("h");

            var file = new MedicalRecordFileStatusMovementQuery("z");
            query.LeftJoin(file).On(
                query.RegistrationNo == file.RegistrationNo &&
                query.ServiceUnitID == file.LastPositionServiceUnitID
                );

            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.es.Distinct = true;

            query.Select
                (
                unit.ServiceUnitName,
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
                    "<CAST(0 AS BIT) AS IsEpisodeSOAP>",
                    "<CAST (0 AS BIT) AS IsDiagnosis>",
                    query.BedID,
                    query.RegistrationTime,
                    "<'' AS SRTriage>",
                    query.RegistrationQue,
                    sumInfo.NoteCount,
                    query.FromRegistrationNo.Coalesce("''").As("FromRegistrationNo"),
                    patient.DateOfBirth,
                    rg.ItemName.As("ReferralGroupName"),
                    sal.ItemName.As("SalutationName")
                );

            query.InnerJoin(tc).On(query.RegistrationNo == tc.RegistrationNo);
            query.InnerJoin(tci).On(tc.TransactionNo == tci.TransactionNo);
            query.LeftJoin(room).On(query.RoomID == room.RoomID);
            query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
            query.LeftJoin(sumInfo).On(query.RegistrationNo == sumInfo.RegistrationNo & sumInfo.NoteCount > 0);
            query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
            query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
            query.InnerJoin(unit).On(tc.ToServiceUnitID == unit.ServiceUnitID);
            query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID && qusr.UserID == AppSession.UserLogin.UserID);
            query.LeftJoin(rg).On(query.SRReferralGroup == rg.ItemID & rg.StandardReferenceID == "ReferralGroup");
            query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);


            if (cboServiceUnitID.SelectedValue != string.Empty)
                query.Where(tc.ToServiceUnitID == cboServiceUnitID.SelectedValue);
            if (cboParamedicID.SelectedValue != string.Empty)
                query.Where(query.ParamedicID == cboParamedicID.SelectedValue);

            AddFilterRegOrMedicalNo(query, patient);

            AddFilterPatientName(query);

            AddFilterRegistrationDate(query);

            AddFilterRegistrationClosedStat(query);


            query.Where
                (
                    query.SRRegistrationType == AppConstant.RegistrationType.MedicalCheckUp,
                    query.IsVoid == false
                );

            query.OrderBy
                (
                    query.RegistrationDate.Descending,
                    query.RegistrationNo.Ascending
                );

            return query.LoadDataTable();
        }


        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.CurrentPageIndex = 0;
            grdList.Rebind();
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

        protected string CardexMonMenu(GridItem container, string cmType)
        {
            var tooltipText = string.Empty;
            switch (cmType)
            {
                case "mc3":
                    tooltipText = "Cardex Monitoring C3";
                    break;
                case "mc3n":
                    tooltipText = "Cardex Monitoring C3 Neonatus ";
                    break;
                case "mcctcu":
                    tooltipText = "Cardex Monitoring CTCU";
                    break;
                default:
                    break;
            }

            var script = string.Format("<a href=\"#\" title=\"{4}\" class=\"noti_homepres\" onclick=\"javascript:openCardexMonitoring('{0}','{1}','{2}','{3}'); return false;\"></a>",
                DataBinder.Eval(container.DataItem, "PatientID"), DataBinder.Eval(container.DataItem, "RegistrationNo"), DataBinder.Eval(container.DataItem, "FromRegistrationNo"), cmType, tooltipText);

            return script;
        }

    }
}
