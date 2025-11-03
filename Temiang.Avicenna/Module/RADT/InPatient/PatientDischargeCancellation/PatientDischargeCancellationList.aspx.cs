using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class PatientDischargeCancellationList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected void Page_Init(object sender, EventArgs e)
        {
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.PatientDischargeCancellation;
            if (!IsPostBack)
            {
                txtDischargeDate.SelectedDate = (new DateTime()).NowAtSqlServer();
                ComboBox.PopulateWithServiceUnitFilterByUserID(cboServiceUnitID);
            }
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        private DataTable PatientDischarges
        {
            get
            {
                var isEmptyFilter = txtDischargeDate.IsEmpty && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Registration")) return null;

                var query = new RegistrationQuery("a");
                var pat = new PatientQuery("b");
                var su = new ServiceUnitQuery("c");
                var usr = new AppUserServiceUnitQuery("d");
                var par = new ParamedicQuery("e");
                var sr = new ServiceRoomQuery("f");
                var sal = new AppStandardReferenceItemQuery("sal");

                esQueryItem group = new esQueryItem(query, "Group", esSystemType.String);
                group = su.ServiceUnitName;

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select(query.RegistrationNo, query.DischargeDate, query.DischargeTime, query.RoomID, query.BedID,
                             pat.MedicalNo, pat.PatientName, pat.Sex, par.ParamedicName, query.DischargeOperatorID,
                             group.As("Group"), sr.RoomName, sal.ItemName.As("SalutationName"));

                query.InnerJoin(pat).On(query.PatientID == pat.PatientID);
                query.InnerJoin(su).On(query.ServiceUnitID == su.ServiceUnitID);
                query.InnerJoin(usr).On(query.ServiceUnitID == usr.ServiceUnitID && usr.UserID == AppSession.UserLogin.UserID);
                query.InnerJoin(par).On(query.ParamedicID == par.ParamedicID);
                query.InnerJoin(sr).On(query.RoomID == sr.RoomID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & pat.SRSalutation == sal.ItemID);

                if (!txtDischargeDate.IsEmpty)
                    query.Where(query.DischargeDate == txtDischargeDate.SelectedDate);
                if (cboServiceUnitID.Text != string.Empty)
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    //query.Where(
                    //    query.Or(
                    //        query.RegistrationNo == searchReg,
                    //        pat.MedicalNo == searchReg,
                    //        string.Format("< OR REPLACE(b.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //        )
                    //    );
                    Helper.AddFilterMedNoOrRegNoOrPatName(query, pat, searchReg, "registration");
                }
                if (!(string.IsNullOrEmpty(txtPatientName.Text)))
                {
                    if (txtPatientName.Text.Trim().Contains(" "))
                    {
                        var searchs = Helper.EscapeQuery(txtPatientName.Text).Trim().Split(' ');
                        foreach (var search in searchs)
                        {
                            var searchLike = "%" + search + "%";
                            query.Where(
                                pat.Or(
                                    pat.FirstName.Like(searchLike),
                                    pat.LastName.Like(searchLike),
                                    pat.MiddleName.Like(searchLike)
                                    )
                                );
                        }
                    }
                    else
                    {
                        var searchLike = "%" + Helper.EscapeQuery(txtPatientName.Text).Trim() + "%";
                        query.Where(
                            pat.Or(
                                pat.FirstName.Like(searchLike),
                                pat.LastName.Like(searchLike),
                                pat.MiddleName.Like(searchLike)
                                )
                            );
                    }
                }

                query.Where(query.IsVoid == false,
                    query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                    //query.DischargeDate.IsNotNull(),
                    "<ISNULL(a.SRDischargeMethod,'') <> ''>",
                    query.IsClosed == false);

                query.OrderBy(query.DischargeDate.Ascending);

                return query.LoadDataTable();
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit) {
                grd.DataSource = new String[] { };
                return; 
            } 
            
            var dataSource = PatientDischarges;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                grd.DataSource = dataSource;
            }
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            foreach (CheckBox chkBox in grdList.MasterTableView.Items.Cast<GridDataItem>().Select(dataItem => (CheckBox)dataItem.FindControl("detailChkbox"))
                                                                                          .Where(chkBox => chkBox.Visible))
            {
                chkBox.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument.Contains("|"))
            {
                var param = eventArgument.Split('|');
                string msg = string.Empty;

                var reg = new Registration();
                reg.LoadByPrimaryKey(param[1]);

                var isRoomIn = reg.IsRoomIn ?? false;

                var bed = new Bed();
                bed.LoadByPrimaryKey(reg.BedID);

                var history = new PatientDischargeHistory();
                history.AddNew();
                history.RegistrationNo = reg.RegistrationNo;
                history.BedID = reg.BedID;
                history.DischargeDate = reg.DischargeDate;
                history.DischargeTime = reg.DischargeTime;
                history.SRDischargeMethod = reg.SRDischargeMethod;
                history.SRDischargeCondition = reg.SRDischargeCondition;
                history.DischargeOperatorID = AppSession.UserLogin.UserID;
                history.IsCancel = true;
                history.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                var patient = new Patient();
                patient.LoadByPrimaryKey(reg.PatientID);
                var isUpdatePatient = false;
                if (reg.SRDischargeCondition == AppSession.Parameter.DischargeConditionDieLessThen48 || reg.SRDischargeCondition == AppSession.Parameter.DischargeConditionDieMoreThen48)
                {
                    isUpdatePatient = true;

                    patient.IsAlive = true;
                    patient.DeathCertificateNo = string.Empty;
                    patient.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    patient.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }

                if (!isRoomIn)
                {
                    if (string.IsNullOrEmpty(bed.RegistrationNo) && (bed.SRBedStatus == AppSession.Parameter.BedStatusUnoccupied || bed.SRBedStatus == AppSession.Parameter.BedStatusCleaning))
                    {
                        reg.DischargeDate = null;
                        reg.DischargeTime = null;
                        reg.DischargeMedicalNotes = string.Empty;
                        reg.DischargeNotes = string.Empty;
                        reg.SRDischargeCondition = string.Empty;
                        reg.SRDischargeMethod = string.Empty;
                        reg.DischargeOperatorID = string.Empty;
                        reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        reg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                        bed.RegistrationNo = reg.RegistrationNo;
                        var srBedStatusBefore = bed.SRBedStatus;
                        bed.SRBedStatus = AppSession.Parameter.BedStatusOccupied;

                        var birColl = new BedRoomInCollection();
                        birColl.Query.Where(birColl.Query.BedID == bed.BedID, birColl.Query.IsVoid == false,
                                            birColl.Query.DateOfExit.IsNull());
                        birColl.LoadAll();

                        bed.IsRoomIn = birColl.Count > 0;
                        bed.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        bed.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                        var thuColl = new PatientTransferHistoryCollection();
                        var thuQuery = new PatientTransferHistoryQuery();
                        thuQuery.Where(thuQuery.RegistrationNo == reg.RegistrationNo);
                        thuQuery.es.Top = 1;
                        thuQuery.OrderBy(thuQuery.TransferNo.Descending);
                        thuColl.Load(thuQuery);

                        foreach (var item in thuColl)
                        {
                            item.DateOfExit = null;
                            item.TimeOfExit = null;
                            item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        }

                        using (var trans = new esTransactionScope())
                        {
                            reg.Save();
                            bed.Save();
                            thuColl.Save();
                            history.Save();

                            var bshOld = new BedStatusHistoryCollection();
                            bshOld.Query.Where(
                                bshOld.Query.BedID == bed.BedID,
                                bshOld.Query.SRBedStatusFrom == AppSession.Parameter.BedStatusOccupied,
                                bshOld.Query.SRBedStatusTo == (AppSession.Parameter.IsBedNeedCleanedProcess ? AppSession.Parameter.BedStatusCleaning : AppSession.Parameter.BedStatusUnoccupied),
                                bshOld.Query.RegistrationNo == reg.RegistrationNo,
                                bshOld.Query.TransferNo == string.Empty);
                            bshOld.Query.es.Top = 1;
                            bshOld.Query.OrderBy(bshOld.Query.LastUpdateDateTime.Descending);
                            bshOld.LoadAll();
                            bshOld.MarkAllAsDeleted();
                            bshOld.Save();

                            if (isUpdatePatient)
                                patient.Save();

                            var apptColl = new BusinessObject.AppointmentCollection();
                            apptColl.Query.Where(apptColl.Query.FromRegistrationNo == reg.RegistrationNo,
                                apptColl.Query.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusOpen);
                            apptColl.LoadAll();
                            foreach (var appt in apptColl)
                            {
                                appt.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusCancel;
                            }
                            apptColl.Save();

                            var patDischargeAppt = new PatientDischargeAppointmentCollection();
                            patDischargeAppt.Query.Where(patDischargeAppt.Query.RegistrationNo == reg.RegistrationNo);
                            patDischargeAppt.LoadAll();
                            foreach (var appt in patDischargeAppt)
                            {
                                appt.IsProcessed = false;
                            }
                            patDischargeAppt.Save();

                            // update tanggal pulang di jasa medis
                            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                            feeColl.SetDischargeDate(reg, AppSession.Parameter.IsFeeCalculatedOnTransaction);
                            feeColl.Save();

                            trans.Complete();
                        }
                    }
                    else
                    {
                        msg = "Discharge cancellation for registration : " + reg.RegistrationNo + " failed. Bed is already registered to other patient.";
                    }
                }
                else
                {
                    DateTime regDate = reg.RegistrationDate ?? (new DateTime()).NowAtSqlServer();
                    string regTime = reg.RegistrationTime;

                    reg.DischargeDate = null;
                    reg.DischargeTime = null;
                    reg.DischargeMedicalNotes = string.Empty;
                    reg.DischargeNotes = string.Empty;
                    reg.SRDischargeCondition = string.Empty;
                    reg.SRDischargeMethod = string.Empty;
                    reg.DischargeOperatorID = string.Empty;
                    reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    reg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    bed.IsRoomIn = true;
                    bed.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    bed.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    var thuColl = new PatientTransferHistoryCollection();
                    var thuQuery = new PatientTransferHistoryQuery();
                    thuQuery.Where(thuQuery.RegistrationNo == reg.RegistrationNo);
                    thuQuery.es.Top = 1;
                    thuQuery.OrderBy(thuQuery.TransferNo.Descending);
                    thuColl.Load(thuQuery);

                    foreach (var item in thuColl)
                    {
                        item.DateOfExit = null;
                        item.TimeOfExit = null;
                        item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }

                    using (var trans = new esTransactionScope())
                    {
                        reg.Save();
                        bed.Save();
                        thuColl.Save();

                        if (!string.IsNullOrEmpty(bed.BedID))
                        {
                            var bir = new BedRoomIn();
                            if (bir.LoadByPrimaryKey(bed.BedID, reg.RegistrationNo, regDate, regTime))
                            {
                                bir.DateOfExit = null;
                                bir.TimeOfExit = null;
                                bir.SRBedStatus = AppSession.Parameter.BedStatusOccupied;
                                bir.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                bir.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                bir.Save();
                            }
                        }

                        history.Save();

                        if (isUpdatePatient)
                            patient.Save();

                        var apptColl = new BusinessObject.AppointmentCollection();
                        apptColl.Query.Where(apptColl.Query.FromRegistrationNo == reg.RegistrationNo,
                            apptColl.Query.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusOpen);
                        apptColl.LoadAll();
                        foreach (var appt in apptColl)
                        {
                            appt.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusCancel;
                        }
                        apptColl.Save();

                        var patDischargeAppt = new PatientDischargeAppointmentCollection();
                        patDischargeAppt.Query.Where(patDischargeAppt.Query.RegistrationNo == reg.RegistrationNo);
                        patDischargeAppt.LoadAll();
                        foreach (var appt in patDischargeAppt)
                        {
                            appt.IsProcessed = false;
                        }
                        patDischargeAppt.Save();

                        // update tanggal pulang di jasa medis
                        var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                        feeColl.SetDischargeDate(reg, AppSession.Parameter.IsFeeCalculatedOnTransaction);
                        feeColl.Save();

                        trans.Complete();
                    }
                }

                if (msg != string.Empty)
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = msg;
                }

                grdList.Rebind();
            }
        }
    }
}
