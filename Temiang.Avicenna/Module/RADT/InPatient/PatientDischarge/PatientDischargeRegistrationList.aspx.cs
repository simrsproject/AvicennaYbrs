using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using System.Web.UI;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class PatientDischargeRegistrationList : BasePage
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

            ProgramID = AppConstant.Program.PatientDischarge;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitFilterByUserID(cboServiceUnitID);

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

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = InpatientRegistrations;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                grd.DataSource = dataSource;
            }
        }

        private DataTable InpatientRegistrations
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtBPJSSepNo.Text) && string.IsNullOrEmpty(cboParamedicID.SelectedValue) && string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Registration")) return null;

                var query = new RegistrationQuery("a");
                var patient = new PatientQuery("b");
                var grr = new GuarantorQuery("c");
                var param = new ParamedicQuery("d");
                var room = new ServiceRoomQuery("e");
                var qusr = new AppUserServiceUnitQuery("u");
                var su = new ServiceUnitQuery("g");
                var sal = new AppStandardReferenceItemQuery("sal");

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        query.RegistrationDate,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        grr.GuarantorName,
                       @"<ISNULL(a.BpjsSepNo, '') AS BpjsSepNo>",
                        param.ParamedicName,
                        query.BedID,
                        query.RoomID,
                        room.RoomName,
                        query.ServiceUnitID,
                        su.ServiceUnitName,
                        query.IsRoomIn,
                        @"<ISNULL(a.IsAllowPatientCheckOut, 0) AS IsAllowPatientCheckOut>",
                        @"<CAST (1 AS BIT) AS IsVisible>",
                        sal.ItemName.As("SalutationName")
                    );

                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.InnerJoin(param).On(query.ParamedicID == param.ParamedicID);
                query.InnerJoin(room).On(query.RoomID == room.RoomID);
                query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                query.InnerJoin(su).On(query.ServiceUnitID == su.ServiceUnitID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" && patient.SRSalutation == sal.ItemID);

                if (cboServiceUnitID.Text != string.Empty)
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    //query.Where(
                    //    query.Or(
                    //            query.RegistrationNo == searchReg,
                    //            patient.MedicalNo == searchReg,
                    //            string.Format("< OR REPLACE(b.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //        )
                    //    );
                    Helper.AddFilterMedNoOrRegNoOrPatName(query, patient, searchReg, "registration");
                }
                if (txtBPJSSepNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtBPJSSepNo.Text);
                    query.Where(
                        query.Or(
                                query.BpjsSepNo == searchReg,
                                patient.Ssn == searchReg
                            )
                        );
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
                        query.IsVoid == false,
                        query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                        //query.DischargeDate.IsNull(),
                        "<ISNULL(a.SRDischargeMethod,'') = ''>", // query.DischargeDate.IsNull() diganti karena bisa discharge otomatis
                        qusr.UserID == AppSession.UserLogin.UserID
                    );

                query.OrderBy(query.BedID.Ascending);

                DataTable dtb = query.LoadDataTable();

                bool isNeedAllowCheckout = (AppSession.Parameter.IsNeedAllowCheckoutConfirmedForDischarge);

                foreach (DataRow row in dtb.Rows)
                {
                    if (isNeedAllowCheckout)
                        row["IsVisible"] = Convert.ToBoolean(row["IsAllowPatientCheckOut"]);

                    if (Convert.ToBoolean(row["IsRoomIn"]) == false)
                    {
                        var bed = new Bed();
                        if (bed.LoadByPrimaryKey(row["BedID"].ToString()))
                        {
                            if (bed.IsNeedConfirmation == true && bed.SRBedStatus == AppSession.Parameter.BedStatusPending)
                                row.Delete();
                        }
                    }
                    else
                    {
                        var bedRoomingInColl = new BedRoomInCollection();
                        var bedRoomingInQ = new BedRoomInQuery("a");
                        var bedQ = new BedQuery("b");
                        bedRoomingInQ.InnerJoin(bedQ).On(bedRoomingInQ.BedID == bedQ.BedID && bedQ.IsNeedConfirmation == true);
                        bedRoomingInQ.Where(bedRoomingInQ.RegistrationNo == row["RegistrationNo"].ToString() &&
                                                 bedRoomingInQ.BedID == row["BedID"].ToString() &&
                                                 bedRoomingInQ.SRBedStatus == AppSession.Parameter.BedStatusPending);
                        bedRoomingInColl.Load(bedRoomingInQ);
                        if (bedRoomingInColl.Count > 0)
                            row.Delete();
                    }
                }
                dtb.AcceptChanges();

                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
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
