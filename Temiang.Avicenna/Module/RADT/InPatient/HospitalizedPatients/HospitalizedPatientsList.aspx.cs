using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class HospitalizedPatientsList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.HospitalizedPatients;

            if (!IsPostBack)
            {
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");
                query.Where(query.SRRegistrationType == AppConstant.RegistrationType.InPatient, query.IsActive == true);

                if (!this.IsUserCrossUnitAble)
                {
                    var usr = new AppUserServiceUnitQuery("b");
                    query.InnerJoin(usr).On(query.ServiceUnitID == usr.ServiceUnitID &&
                                            usr.UserID == AppSession.UserLogin.UserID);
                }

                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                var coll = new ParamedicCollection();
                coll.Query.Where(coll.Query.IsActive == true);
                coll.LoadAll();

                cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Paramedic entity in coll)
                {
                    cboParamedicID.Items.Add(new RadComboBoxItem(entity.ParamedicName, entity.ParamedicID));
                }

                var smfs = new SmfCollection();
                smfs.LoadAll();

                cboSmfID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Smf entity in smfs)
                {
                    cboSmfID.Items.Add(new RadComboBoxItem(entity.SmfName, entity.SmfID));
                }

                StandardReference.InitializeIncludeSpace(cboReligion, AppEnum.StandardReference.Religion);
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

            if (!IsPostBack && !IsListLoadRecordOnInit && this.IsUserCrossUnitAble)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = Registrations;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                grd.DataSource = dataSource;
            }

            var recCount = dataSource == null ? 0 : dataSource.Rows.Count;
            lblRegistrationCount.Text = string.Format("{0}{1}", recCount, AppSession.Parameter.MaxResultRecord == recCount ? "+" : "");
        }

        private DataTable Registrations
        {
            get
            {
                var isEmptyFilter = this.IsUserCrossUnitAble && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboParamedicID.SelectedValue) && string.IsNullOrEmpty(cboSmfID.SelectedValue) && 
                    string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientName.Text) && string.IsNullOrEmpty(cboReligion.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Registration")) return null;

                var query = new RegistrationQuery("a");
                var patQ = new PatientQuery("b");
                var parQ = new ParamedicQuery("c");
                var unitQ = new ServiceUnitQuery("d");
                var smfQ = new SmfQuery("e");
                var guarQ = new GuarantorQuery("f");
                var transferHistQ = new PatientTransferHistoryQuery("g");
                var smfHistQ = new SmfQuery("h");
                var roomQ = new ServiceRoomQuery("i");
                var sal = new AppStandardReferenceItemQuery("sal");
                var rel = new AppStandardReferenceItemQuery("rel");

                query.InnerJoin(patQ).On(query.PatientID == patQ.PatientID && query.IsVoid == false);
                query.InnerJoin(parQ).On(query.ParamedicID == parQ.ParamedicID);
                query.InnerJoin(unitQ).On(query.ServiceUnitID == unitQ.ServiceUnitID);
                query.LeftJoin(smfQ).On(query.SmfID == smfQ.SmfID);
                query.InnerJoin(guarQ).On(query.GuarantorID == guarQ.GuarantorID);
                query.InnerJoin(transferHistQ).On(query.RegistrationNo == transferHistQ.RegistrationNo &&
                                                  transferHistQ.TransferNo == string.Empty);
                query.LeftJoin(smfHistQ).On(transferHistQ.SmfID == smfHistQ.SmfID);
                query.InnerJoin(roomQ).On(query.RoomID == roomQ.RoomID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patQ.SRSalutation == sal.ItemID);
                query.LeftJoin(rel).On(rel.StandardReferenceID == "Religion" & patQ.SRReligion == rel.ItemID);

                if (!this.IsUserCrossUnitAble)
                {
                    var usr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(usr).On(query.ServiceUnitID == usr.ServiceUnitID &&
                                            usr.UserID == AppSession.UserLogin.UserID);
                }

                query.Select
                    (
                        query.RegistrationDate,
                        query.RegistrationNo,
                        patQ.MedicalNo.Coalesce("''"),
                        patQ.PatientName.Coalesce("''"),
                        patQ.Sex,
                        guarQ.GuarantorName,
                        unitQ.ServiceUnitName,
                        query.BedID,
                        parQ.ParamedicName,
                        smfQ.SmfName,
                        smfHistQ.SmfName.As("EarlySmfName"),
                        roomQ.RoomName,
                        sal.ItemName.As("SalutationName"),
                        @"<CASE WHEN DATEDIFF(DAY, a.RegistrationDate, ISNULL(a.DischargeDate, GETDATE())) = 0 THEN 1 ELSE DATEDIFF(DAY, a.RegistrationDate, ISNULL(a.DischargeDate, GETDATE())) END AS 'LoS'>",
                        @"<CAST(0 AS BIT) AS 'IsWarning'>",
                         rel.ItemName.As("Religion")
                    );
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.es.Distinct = true;

                query.Where(query.DischargeDate.IsNull(),
                            query.SRRegistrationType == AppConstant.RegistrationType.InPatient);
                query.OrderBy(query.BedID.Ascending);

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboSmfID.SelectedValue != string.Empty)
                    query.Where(query.SmfID == cboSmfID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    //query.Where(
                    //    query.Or(
                    //        query.RegistrationNo == searchReg,
                    //        patQ.MedicalNo == searchReg,
                    //        string.Format("< OR REPLACE(b.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //        )
                    //    );
                    Helper.AddFilterMedNoOrRegNoOrPatName(query, patQ, searchReg, "registration");
                }
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) LIKE '{0}'>", searchPatient)
                        );
                }

                if (!string.IsNullOrEmpty(cboReligion.SelectedValue))
                    query.Where(patQ.SRReligion == cboReligion.SelectedValue);

                DataTable dtbl = query.LoadDataTable();

                foreach (DataRow row in dtbl.Rows)
                {
                    var bed = new Bed();
                    if (bed.LoadByPrimaryKey(row["BedID"].ToString()))
                    {
                        if (bed.IsNeedConfirmation == true & bed.SRBedStatus == AppSession.Parameter.BedStatusPending)
                            row["IsWarning"] = true;
                    }
                }
                dtbl.AcceptChanges();

                return dtbl;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }
    }
}