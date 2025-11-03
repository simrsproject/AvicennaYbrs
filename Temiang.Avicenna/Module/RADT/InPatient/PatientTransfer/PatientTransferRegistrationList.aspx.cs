using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class PatientTransferRegistrationList : BasePage
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

            ProgramID = AppConstant.Program.PatientTransfer;

            if (!IsPostBack)
            {
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");

                if (!this.IsUserCrossUnitAble)
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                    query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                }

                query.Where(
                    query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                    query.IsActive == true
                    );

                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                ParamedicCollection param = new ParamedicCollection();
                param.Query.Where(param.Query.IsActive == true);
                param.LoadAll();

                cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Paramedic medic in param)
                {
                    cboParamedicID.Items.Add(new RadComboBoxItem(medic.ParamedicName, medic.ParamedicID));
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
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

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit && this.IsUserCrossUnitAble)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = InpatientRegistrations;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        private DataTable InpatientRegistrations
        {
            get
            {
                var isEmptyFilter = this.IsUserCrossUnitAble && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(cboParamedicID.SelectedValue) && string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Registration")) return null;

                var query = new RegistrationQuery("a");
                var patient = new PatientQuery("b");
                var grr = new GuarantorQuery("c");
                var param = new ParamedicQuery("d");
                var room = new ServiceRoomQuery("e");
                var su = new ServiceUnitQuery("g");
                var smf = new SmfQuery("h");
                var sal = new AppStandardReferenceItemQuery("sal");

                esQueryItem group = new esQueryItem(query, "Group", esSystemType.String);
                group = room.RoomName;

                query.Select
                    (
                        query.RegistrationDate,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        grr.GuarantorName,
                        param.ParamedicName,
                        query.BedID,
                        query.RoomID,
                        group.As("Group"),
                        room.RoomName,
                        smf.SmfName,
                        query.ServiceUnitID,
                        su.ServiceUnitName,
                        sal.ItemName.As("SalutationName"),
                        @"<CAST(1 AS BIT) AS IsNewVisible>",
                        query.IsRoomIn
                    );

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.InnerJoin(param).On(query.ParamedicID == param.ParamedicID);
                query.InnerJoin(room).On(query.RoomID == room.RoomID);
                query.InnerJoin(su).On(query.ServiceUnitID == su.ServiceUnitID);
                query.LeftJoin(smf).On(query.SmfID == smf.SmfID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

                if (!this.IsUserCrossUnitAble)
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID && qusr.UserID == AppSession.UserLogin.UserID);
                }

                if (cboServiceUnitID.Text != string.Empty)
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    //query.Where(
                    //    query.Or(
                    //        query.RegistrationNo == searchReg,
                    //        patient.MedicalNo == searchReg,
                    //        string.Format("< OR REPLACE(b.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //        )
                    //    );
                    Helper.AddFilterMedNoOrRegNoOrPatName(query, patient, searchReg, "registration");
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
                                patient.Or(
                                    patient.FirstName.Like(searchLike),
                                    patient.LastName.Like(searchLike),
                                    patient.MiddleName.Like(searchLike)
                                    )
                                );
                        }
                    }
                    else
                    {
                        var searchLike = "%" + Helper.EscapeQuery(txtPatientName.Text).Trim() + "%";
                        query.Where(
                            patient.Or(
                                patient.FirstName.Like(searchLike),
                                patient.LastName.Like(searchLike),
                                patient.MiddleName.Like(searchLike)
                                )
                            );
                    }
                }
                
                query.Where
                    (
                        query.IsClosed == false,
                        query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                        query.DischargeDate.IsNull(),
                        query.IsVoid == false
                    );

                query.OrderBy(query.BedID.Ascending);

                DataTable dtbl = query.LoadDataTable();

                foreach (DataRow row in dtbl.Rows)
                {
                    if (Convert.ToBoolean(row["IsRoomIn"]))
                    {
                        var bric = new BedRoomInCollection();
                        bric.Query.Where(bric.Query.BedID == row["BedID"].ToString(),
                            bric.Query.RegistrationNo == row["RegistrationNo"].ToString(),
                            bric.Query.IsVoid == false,
                            bric.Query.SRBedStatus == AppSession.Parameter.BedStatusPending);
                        bric.LoadAll();
                        if (bric.Count > 0)
                            row["IsNewVisible"] = false;
                    }
                    else
                    {
                        var bedc = new BedCollection();
                        bedc.Query.Where(bedc.Query.BedID == row["BedID"].ToString(),
                           bedc.Query.RegistrationNo == row["RegistrationNo"].ToString(),
                           bedc.Query.SRBedStatus == AppSession.Parameter.BedStatusPending);
                        bedc.LoadAll();
                        if (bedc.Count > 0)
                            row["IsNewVisible"] = false;
                    }
                }
                dtbl.AcceptChanges();

                return dtbl;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var query = new PatientTransferQuery("a");

            var rq = new RegistrationQuery("b");
            query.InnerJoin(rq).On(query.RegistrationNo == rq.RegistrationNo);

            var patient = new PatientQuery("z");
            query.InnerJoin(patient).On(rq.PatientID == patient.PatientID);

            query.Where
                (
                    query.RegistrationNo == e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString()
                );

            //from
            var fsrq = new ServiceRoomQuery("d");
            var fbq = new BedQuery("f");
            var fasriq = new AppStandardReferenceItemQuery("m");
            query.InnerJoin(fsrq).On(query.FromRoomID == fsrq.RoomID);
            query.InnerJoin(fbq).On(query.FromBedID == fbq.BedID);
            query.LeftJoin(fasriq).On
                (
                    query.FromSpecialtyID == fasriq.ItemID &
                    fasriq.StandardReferenceID == "Specialty"
                );

            //to
            var tsrq = new ServiceRoomQuery("i");
            var tbq = new BedQuery("k");
            var tasriq = new AppStandardReferenceItemQuery("n");
            query.InnerJoin(tsrq).On(query.ToRoomID == tsrq.RoomID);
            query.InnerJoin(tbq).On(query.ToBedID == tbq.BedID);
            query.LeftJoin(tasriq).On
                (
                    query.ToSpecialtyID == tasriq.ItemID &
                    tasriq.StandardReferenceID == "Specialty"
                );

            query.Select
                (
                    query.TransferNo,
                    query.RegistrationNo,
                    query.TransferDate,
                    query.TransferTime,
                    fsrq.RoomName.As("FromRoomName"),
                    fbq.BedID.As("FromBedID"),
                    fasriq.ItemName.As("FromSpecialtyName"),
                    tsrq.RoomName.As("ToRoomName"),
                    tbq.BedID.As("ToBedID"),
                    tasriq.ItemName.As("ToSpecialtyName"),
                    query.IsApprove,
                    query.IsVoid,
                    query.LastUpdateByUserID,
                    rq.BedID,
                    @"<CAST(0 AS BIT) AS IsViewVisible>",
                    @"<CAST(0 AS BIT) AS IsEditAble>"
                );

            query.OrderBy(query.TransferNo.Ascending);

            DataTable dtb = query.LoadDataTable();

            foreach (DataRow row in dtb.Rows)
            {
                var bed = new Bed();
                if (bed.LoadByPrimaryKey(row["BedID"].ToString()))
                {
                    if (bed.IsNeedConfirmation == true && bed.SRBedStatus == AppSession.Parameter.BedStatusPending)
                        row["IsViewVisible"] = false;
                }
                row["IsEditAble"] = this.IsPowerUser;
            }
            dtb.AcceptChanges();

            e.DetailTableView.DataSource = dtb;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (!(source is RadGrid))
                return;

            if (eventArgument == "rebind")
                grdList.Rebind();
        }
    }
}
