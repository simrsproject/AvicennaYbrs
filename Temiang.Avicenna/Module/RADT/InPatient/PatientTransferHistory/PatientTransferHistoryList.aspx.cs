using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class PatientTransferHistoryList : BasePage
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

            ProgramID = AppConstant.Program.PatientTransferHistory;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitFilterByUserID(cboServiceUnitID);

                var param = new ParamedicCollection();
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
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        private DataTable InpatientRegistrations
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(cboParamedicID.SelectedValue) && string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Registration")) return null;

                var query = new RegistrationQuery("a");
                var patient = new PatientQuery("b");
                var param = new ParamedicQuery("d");
                var su = new ServiceUnitQuery("g");
                var smf = new SmfQuery("h");
                var sal = new AppStandardReferenceItemQuery("sal");

                query.Select
                    (
                        query.RegistrationDate,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        param.ParamedicName,
                        query.BedID,
                        su.ServiceUnitName,
                        smf.SmfName,
                        @"<CAST(1 AS BIT) AS 'IsCheckinConfirmed'>",
                        sal.ItemName.As("SalutationName")
                    );

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(param).On(query.ParamedicID == param.ParamedicID);
                query.InnerJoin(su).On(query.ServiceUnitID == su.ServiceUnitID);
                query.LeftJoin(smf).On(query.SmfID == smf.SmfID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

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

                if (!chkIsIncludeDischarge.Checked || !chkIsIncludeClosed.Checked)
                {
                    query.Where
                    (
                        query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                        query.IsVoid == false,
                        query.IsClosed == false,
                        query.Or(query.DischargeDate.IsNull(), query.SRDischargeCondition == string.Empty)
                    );
                }
                else
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    query.Where(
                        query.Or(
                            query.RegistrationNo == searchReg,
                            patient.MedicalNo == searchReg,
                            string.Format("< OR REPLACE(b.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                            )
                        );

                    query.Where
                    (
                        query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                        query.IsVoid == false
                    );
                    if (!chkIsIncludeClosed.Checked)
                        query.Where(query.IsClosed == false);
                    if (!chkIsIncludeDischarge.Checked)
                        query.Where(query.Or(query.DischargeDate.IsNull(), query.SRDischargeCondition == string.Empty));
                }

                query.OrderBy(query.BedID.Ascending);

                DataTable dtbl = query.LoadDataTable();

                foreach (DataRow row in dtbl.Rows)
                {
                    var bed = new Bed();
                    if (bed.LoadByPrimaryKey(row["BedID"].ToString()))
                    {
                        if (bed.IsNeedConfirmation == true && bed.SRBedStatus == AppSession.Parameter.BedStatusPending)
                            row["IsCheckinConfirmed"] = false;
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
            var query = new PatientTransferHistoryQuery("a");
            var su = new ServiceUnitQuery("b");
            var sr = new ServiceRoomQuery("c");
            var cl = new ClassQuery("d");
            var smf = new SmfQuery("e");
            query.Where
                (
                    query.RegistrationNo == e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString()
                );
            query.InnerJoin(su).On(query.ServiceUnitID == su.ServiceUnitID);
            query.InnerJoin(sr).On(query.RoomID == sr.RoomID);
            query.InnerJoin(cl).On(query.ChargeClassID == cl.ClassID);
            query.InnerJoin(smf).On(query.SmfID == smf.SmfID);

            query.Select
                (
                    query.RegistrationNo,
                    query.TransferNo,
                    su.ServiceUnitName,
                    sr.RoomName,
                    query.BedID,
                    cl.ClassName,
                    smf.SmfName,
                    query.DateOfEntry,
                    query.TimeOfEntry,
                    query.DateOfExit,
                    query.TimeOfExit
                );

            query.OrderBy(query.TransferNo.Ascending);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }
    }
}
