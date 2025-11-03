using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.BloodBank
{
    public partial class BloodRequestList : BasePage
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

            ProgramID = AppConstant.Program.BloodBankRequest;

            if (!IsPostBack)
            {
                var query = new ServiceUnitQuery("a");
                var usrq = new AppUserServiceUnitQuery("b");
                query.InnerJoin(usrq).On(usrq.ServiceUnitID == query.ServiceUnitID &&
                                         usrq.UserID == AppSession.UserLogin.UserID);
                query.Where(
                    query.SRRegistrationType.In(AppConstant.RegistrationType.InPatient,
                                                AppConstant.RegistrationType.EmergencyPatient,
                                                AppConstant.RegistrationType.OutPatient), query.IsActive == true);
                query.OrderBy(query.ServiceUnitID.Ascending);

                var coll = new ServiceUnitCollection();
                coll.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in coll)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
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
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = AllRegistrations;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
        }

        private DataTable AllRegistrations
        {
            get
            {
                var isEmptyFilter = txtOrderDate1.IsEmpty && txtOrderDate2.IsEmpty && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue)
                    && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Blood Request")) return null;

                DataTable dtb = Registrations;
                dtb.Merge(RegistrationsOperatingRoom);
                return dtb;
            }
        }

        private DataTable Registrations
        {
            get
            {
                var qr = new RegistrationQuery("r");
                var qp = new PatientQuery("p");
                var qm = new ParamedicQuery("m");
                var unit = new ServiceUnitQuery("s");
                var room = new ServiceRoomQuery("d");
                var grr = new GuarantorQuery("c");
                var sal = new AppStandardReferenceItemQuery("sal");

                qr.es.Top = AppSession.Parameter.MaxResultRecord;

                qr.Select
                    (
                        qr.RegistrationNo,
                        qr.RegistrationDate,
                        qr.RegistrationTime,
                        qp.MedicalNo,
                        qp.PatientName,
                        qp.Sex,
                        qm.ParamedicName,
                        unit.ServiceUnitName,
                        room.RoomName,
                        qr.BedID,
                        qr.IsTransferedToInpatient,
                        qr.SRRegistrationType,
                        grr.GuarantorName,
                        sal.ItemName.As("SalutationName")
                    );

                qr.InnerJoin(qp).On(qp.PatientID == qr.PatientID);
                qr.LeftJoin(qm).On(qm.ParamedicID == qr.ParamedicID);
                qr.LeftJoin(unit).On(unit.ServiceUnitID == qr.ServiceUnitID);
                qr.LeftJoin(room).On(room.RoomID == qr.RoomID);
                qr.InnerJoin(grr).On(grr.GuarantorID == qr.GuarantorID);
                qr.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & sal.ItemID == qp.SRSalutation);

                var usr = new AppUserServiceUnitQuery("usr");
                qr.InnerJoin(usr).On(usr.UserID == AppSession.UserLogin.UserID && usr.ServiceUnitID == unit.ServiceUnitID);

                if (!txtOrderDate1.IsEmpty && !txtOrderDate2.IsEmpty)
                    qr.Where(qr.RegistrationDate >= txtOrderDate1.SelectedDate, qr.RegistrationDate <= txtOrderDate2.SelectedDate);
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    qr.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    if (AppSession.Parameter.IsMedicalNoContainStrip)
                        qr.Where
                            (qr.Or
                                 (
                                     string.Format("<r.RegistrationNo = '{0}' OR >", searchReg),
                                     string.Format("<p.MedicalNo = '{0}' OR >", searchReg),
                                     string.Format("<p.OldMedicalNo = '{0}'>", searchReg),
                                     string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                                     string.Format("< OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                                 )
                            );
                    else
                        qr.Where
                        (qr.Or
                             (
                                 string.Format("<r.RegistrationNo = '{0}' OR >", searchReg),
                                 string.Format("<p.MedicalNo = '{0}' OR >", searchReg),
                                 string.Format("<p.OldMedicalNo = '{0}'>", searchReg)
                             )
                        );
                }

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    qr.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient)
                        );
                }

                qr.Where(
                    qr.IsClosed == false,
                    qr.IsVoid == false,
                    qr.IsNonPatient == false,
                    qr.IsFromDispensary == false,
                    qr.IsDirectPrescriptionReturn == false,
                    qr.Or(qr.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient,
                          qr.SRRegistrationType == AppConstant.RegistrationType.OutPatient,
                          qr.And(qr.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                                 qr.SRDischargeMethod == string.Empty)));
                qr.OrderBy(qr.RegistrationNo.Descending);

                DataTable tbl = qr.LoadDataTable();

                return tbl;
            }
        }

        private DataTable RegistrationsOperatingRoom
        {
            get
            {
                var qr = new RegistrationQuery("r");
                var qp = new PatientQuery("p");
                var qm = new ParamedicQuery("m");
                var unit = new ServiceUnitQuery("s");
                var room = new ServiceRoomQuery("d");
                var grr = new GuarantorQuery("c");
                var sal = new AppStandardReferenceItemQuery("sal");
                var sub = new ServiceUnitBookingQuery("sub");

                qr.es.Top = AppSession.Parameter.MaxResultRecord;
                qr.es.Distinct = true;

                qr.Select
                    (
                        qr.RegistrationNo,
                        qr.RegistrationDate,
                        qr.RegistrationTime,
                        qp.MedicalNo,
                        qp.PatientName,
                        qp.Sex,
                        qm.ParamedicName,
                        unit.ServiceUnitName,
                        room.RoomName,
                        @"<'' AS BedID>",
                        qr.IsTransferedToInpatient,
                        qr.SRRegistrationType,
                        grr.GuarantorName,
                        sal.ItemName.As("SalutationName")
                    );
                qr.InnerJoin(sub).On(sub.RegistrationNo == qr.RegistrationNo && sub.IsApproved == true);
                qr.InnerJoin(qp).On(qp.PatientID == qr.PatientID);
                qr.LeftJoin(qm).On(qm.ParamedicID == sub.ParamedicID);
                qr.InnerJoin(room).On(room.RoomID == sub.RoomID);
                qr.InnerJoin(unit).On(unit.ServiceUnitID == sub.ServiceUnitID);
                qr.InnerJoin(grr).On(grr.GuarantorID == qr.GuarantorID);
                qr.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & sal.ItemID == qp.SRSalutation);

                var usr = new AppUserServiceUnitQuery("usr");
                qr.InnerJoin(usr).On(usr.UserID == AppSession.UserLogin.UserID && usr.ServiceUnitID == unit.ServiceUnitID);

                if (!txtOrderDate1.IsEmpty && !txtOrderDate2.IsEmpty)
                    qr.Where(qr.RegistrationDate >= txtOrderDate1.SelectedDate, qr.RegistrationDate <= txtOrderDate2.SelectedDate);
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    qr.Where(room.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    if (AppSession.Parameter.IsMedicalNoContainStrip)
                        qr.Where
                            (qr.Or
                                 (
                                     string.Format("<r.RegistrationNo = '{0}' OR >", searchReg),
                                     string.Format("<p.MedicalNo = '{0}' OR >", searchReg),
                                     string.Format("<p.OldMedicalNo = '{0}'>", searchReg),
                                     string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                                     string.Format("< OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                                 )
                            );
                    else
                        qr.Where
                        (qr.Or
                             (
                                 string.Format("<r.RegistrationNo = '{0}' OR >", searchReg),
                                 string.Format("<p.MedicalNo = '{0}' OR >", searchReg),
                                 string.Format("<p.OldMedicalNo = '{0}'>", searchReg)
                             )
                        );
                }

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    qr.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient)
                        );
                }

                qr.Where(
                    qr.IsClosed == false,
                    qr.IsVoid == false,
                    qr.Or(qr.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient,
                          qr.SRRegistrationType == AppConstant.RegistrationType.OutPatient,
                          qr.And(qr.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                                 qr.SRDischargeMethod == string.Empty)));
                qr.OrderBy(qr.RegistrationNo.Descending);

                DataTable tbl = qr.LoadDataTable();

                return tbl;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var query = new BloodBankTransactionQuery("a");
            var reg = new RegistrationQuery("b");
            var pat = new PatientQuery("c");
            var btype = new AppStandardReferenceItemQuery("d");
            var bgroup = new AppStandardReferenceItemQuery("e");
            var usr = new AppUserQuery("f");

            query.es.Top = AppSession.Parameter.MaxResultRecord;

            query.InnerJoin(reg).On(reg.RegistrationNo == query.RegistrationNo);
            query.InnerJoin(pat).On(pat.PatientID == reg.PatientID);
            query.LeftJoin(btype).On(btype.StandardReferenceID == AppEnum.StandardReference.BloodType && btype.ItemID == pat.SRBloodType);
            query.LeftJoin(bgroup).On(bgroup.StandardReferenceID == AppEnum.StandardReference.BloodGroup && bgroup.ItemID == query.SRBloodGroupRequest);
            query.InnerJoin(usr).On(usr.UserID == query.OfficerByUserID);
            query.Select
                (
                    query.RegistrationNo,
                    query.TransactionNo,
                    query.TransactionDate,
                    query.RequestDate,
                    query.RequestTime,
                    query.BloodBankNo,
                    query.PdutNo,
                    btype.ItemName.As("BloodType"),
                    pat.BloodRhesus,
                    bgroup.ItemName.As("BloodGroup"),
                    query.QtyBagRequest,
                    query.VolumeBag,
                    usr.UserName.As("Officer"),
                    query.IsApproved,
                    query.IsVoid,
                    "<'BloodRequestDetail.aspx?md=view&id='+a.TransactionNo+'&regno='+a.RegistrationNo AS RequestUrl>"
                );

            query.Where(query.RegistrationNo == e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString());

            e.DetailTableView.DataSource = query.LoadDataTable();
        }
    }
}
