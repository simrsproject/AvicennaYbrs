using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class DownPaymentList : BasePage
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

            ProgramID = AppConstant.Program.DownPayment;

            if (!IsPostBack)
            {
                var coll = new ServiceUnitCollection();
                coll.Query.Where(
                    coll.Query.SRRegistrationType.In(
                            AppConstant.RegistrationType.EmergencyPatient,
                            AppConstant.RegistrationType.InPatient,
                            AppConstant.RegistrationType.OutPatient,
                            AppConstant.RegistrationType.MedicalCheckUp
                        ),
                    coll.Query.IsActive == true
                );
                coll.Query.OrderBy(coll.Query.DepartmentID.Ascending);
                coll.LoadAll();

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

            if (AppSession.Parameter.HealthcareInitialAppsVersion != "RSSA")
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

            var dataSource = Registrations;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }
        protected void grdListD_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = Patients;
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
            if (AppSession.Parameter.HealthcareInitialAppsVersion != "RSSA")
                SaveValueToCookie();

            grdList.Rebind();
        }

        protected void btnFilterD_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (AppSession.Parameter.HealthcareInitialAppsVersion != "RSSA")
                SaveValueToCookie();

            grdListD.Rebind();
        }

        private DataTable Registrations
        {
            get
            {
                var isEmptyFilter = txtOrderDate1.IsEmpty && txtOrderDate2.IsEmpty && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && 
                    string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientName.Text) && string.IsNullOrEmpty(txtPaymentNo.Text);
                if (!ValidateSearch(isEmptyFilter, "Registration")) return null;

                var qr = new RegistrationQuery("r");
                var qp = new PatientQuery("p");
                var qm = new ParamedicQuery("m");
                var unit = new ServiceUnitQuery("s");
                var room = new ServiceRoomQuery("d");
                //var mrg = new MergeBillingQuery("b");
                var grr = new GuarantorQuery("c");
                var sal = new AppStandardReferenceItemQuery("sal");

                qr.es.Top = AppSession.Parameter.MaxResultRecord;
                qr.es.Distinct = true;

                qr.Select
                    (
                        qr.RegistrationNo,
                        qr.RegistrationDate,
                        qr.RegistrationTime,
                        qp.PatientID,
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
                        qr.IsConsul,
                        qr.ServiceUnitID,
                        sal.ItemName.As("SalutationName"),
                        @"<CAST(1 AS BIT) AS IsChasierCheckin>",
                        @"<'' AS CashManagementNo>"
                    );

                qr.InnerJoin(qp).On(qr.PatientID == qp.PatientID);
                qr.LeftJoin(qm).On(qr.ParamedicID == qm.ParamedicID);
                qr.LeftJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);
                qr.LeftJoin(room).On(qr.RoomID == room.RoomID);
                //qr.InnerJoin(mrg).On(qr.RegistrationNo == mrg.RegistrationNo);
                qr.InnerJoin(grr).On(qr.GuarantorID == grr.GuarantorID);
                qr.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qp.SRSalutation == sal.ItemID);

                if (!txtOrderDate1.IsEmpty && !txtOrderDate2.IsEmpty)
                    qr.Where(qr.RegistrationDate >= txtOrderDate1.SelectedDate, qr.RegistrationDate < txtOrderDate2.SelectedDate.Value.AddDays(1));
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    qr.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    //qr.Where(
                    //    qr.Or(
                    //        qr.RegistrationNo == searchReg,
                    //        qp.MedicalNo == searchReg,
                    //        qp.OldMedicalNo == searchReg,
                    //        string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //        string.Format("< OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //        )
                    //    );
                    Helper.AddFilterMedNoOrRegNoOrPatName(qr, qp, searchReg, "registration");
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    qr.Where(qp.FullName.Like(searchPatient));
                    //qr.Where
                    //    (
                    //      string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient)
                    //    );
                }
                if (!string.IsNullOrEmpty(txtPaymentNo.Text))
                {
                    var py = new TransPaymentQuery("py");
                    qr.InnerJoin(py).On(qr.RegistrationNo == py.RegistrationNo);
                    qr.Where(py.PaymentNo == txtPaymentNo.Text,
                             py.TransactionCode == BusinessObject.Reference.TransactionCode.DownPayment);
                }

                qr.Where(qr.IsConsul == false, qr.IsVoid == false, qr.IsClosed == false);
                
                //if (!AppSession.Parameter.IsSeparatePaymentForOpConsul)
                //    qr.Where(qr.IsConsul == false);

                qr.OrderBy(qr.RegistrationDate.Descending, qr.RegistrationNo.Ascending);

                DataTable tbl = qr.LoadDataTable();

                if (AppSession.Parameter.IsUsingCashManagement)
                {
                    var usr = new AppUser();
                    usr.LoadByPrimaryKey(AppSession.UserLogin.UserID);

                    bool isCheckin = !string.IsNullOrEmpty(usr.CashManagementNo);
                    string cashManagNo = string.IsNullOrEmpty(usr.CashManagementNo) ? string.Empty : usr.CashManagementNo;

                    foreach (DataRow row in tbl.Rows)
                    {
                        row["IsChasierCheckin"] = isCheckin;
                        row["CashManagementNo"] = cashManagNo;
                    }

                    tbl.AcceptChanges();
                }

                return tbl;
            }
        }

        private DataTable Patients
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(txtSsnD.Text) && string.IsNullOrEmpty(txtPatientNameD.Text) && string.IsNullOrEmpty(txtMedicalNoD.Text) && string.IsNullOrEmpty(txtPaymentNoD.Text);
                if (!ValidateSearch(isEmptyFilter, "Patient")) return null;

                isEmptyFilter = true;

                var qp = new PatientQuery("p");
                var sal = new AppStandardReferenceItemQuery("sal");

                qp.es.Top = AppSession.Parameter.MaxResultRecord;
                qp.es.Distinct = true;

                qp.Select
                    (
                        qp.PatientID,
                        qp.MedicalNo,
                        qp.PatientName,
                        qp.Sex,
                        qp.Ssn,
                        sal.ItemName.As("SalutationName"),
                        @"<CAST(1 AS BIT) AS IsChasierCheckin>",
                        @"<'' AS CashManagementNo>"
                    );

                qp.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qp.SRSalutation == sal.ItemID);
                qp.Where(qp.IsActive == true, qp.IsNonPatient == false);

                if (txtSsnD.Text != string.Empty)
                {
                    isEmptyFilter = false;

                    string searchSsn = Helper.EscapeQuery(txtSsnD.Text);
                    qp.Where(qp.Ssn == searchSsn);
                }

                if (txtMedicalNoD.Text != string.Empty)
                {
                    isEmptyFilter = false;

                    string searchReg = Helper.EscapeQuery(txtMedicalNoD.Text);
                    if (AppSession.Parameter.IsMedicalNoContainStrip)
                        qp.Where(
                            qp.Or(
                                qp.MedicalNo == searchReg,
                                qp.OldMedicalNo == searchReg,
                                string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                                string.Format("< OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                                )
                            );
                    else
                        qp.Where(
                            qp.Or(
                                qp.MedicalNo == searchReg,
                                qp.OldMedicalNo == searchReg,
                                string.Format("< OR p.MedicalNo LIKE '%{0}%'>", searchReg),
                                string.Format("< OR p.OldMedicalNo LIKE '%{0}%'>", searchReg)
                                )
                            );
                }
                if (txtPatientNameD.Text != string.Empty)
                {
                    isEmptyFilter = false;

                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientNameD.Text) + "%";
                    qp.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient)
                        );
                }
                if (!string.IsNullOrEmpty(txtPaymentNoD.Text))
                {
                    isEmptyFilter = false;

                    var py = new TransPaymentQuery("py");
                    qp.InnerJoin(py).On(qp.PatientID == py.PatientID);
                    qp.Where(py.PaymentNo == txtPaymentNoD.Text,
                             py.TransactionCode == BusinessObject.Reference.TransactionCode.DownPayment, 
                             py.RegistrationNo == string.Empty);
                }
                if (isEmptyFilter) {
                    // tampilkan hanya yang ada deposit
                    var dp = new TransPaymentQuery("dp");
                    qp.InnerJoin(dp).On(qp.PatientID == dp.PatientID)
                        .Where(dp.PaymentReferenceNo == string.Empty);
                }

                qp.OrderBy(qp.MedicalNo.Ascending);

                DataTable tbl = qp.LoadDataTable();

                if (AppSession.Parameter.IsUsingCashManagement)
                {
                    var usr = new AppUser();
                    usr.LoadByPrimaryKey(AppSession.UserLogin.UserID);

                    bool isCheckin = !string.IsNullOrEmpty(usr.CashManagementNo);
                    string cashManagNo = string.IsNullOrEmpty(usr.CashManagementNo) ? string.Empty : usr.CashManagementNo;

                    foreach (DataRow row in tbl.Rows)
                    {
                        row["IsChasierCheckin"] = isCheckin;
                        row["CashManagementNo"] = cashManagNo;
                    }

                    tbl.AcceptChanges();
                }

                return tbl;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            string[] mrg = Helper.MergeBilling.GetMergeRegistration(e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString());

            var query = new TransPaymentQuery("a");
            var reg = new RegistrationQuery("b");
            var patient = new PatientQuery("c");
            var unit = new ServiceUnitQuery("d");
            var usr = new AppUserQuery("e");

            query.es.Top = AppSession.Parameter.MaxResultRecord;

            query.Select
                (
                    query.PaymentNo,
                    query.RegistrationNo,
                    patient.PatientID,
                    patient.MedicalNo,
                    patient.PatientName,
                    unit.ServiceUnitName,
                    query.PaymentDate,
                    query.PaymentTime,
                    query.IsApproved,
                    query.IsVoid,
                    usr.UserName,
                    query.PaymentReferenceNo,
                    @"<ISNULL(a.CashManagementNo, '') AS CashManagementNo>",
                    @"<(SELECT SUM(f.[Amount]) 
                        FROM TransPaymentItem f 
                        WHERE a.[PaymentNo] = f.[PaymentNo]) AS 'TotalPaymentAmount'>",
                    "<'DownPaymentDetail.aspx?md=view&id='+a.PaymentNo+'&regno='+a.RegistrationNo+'&cmno='+ISNULL(a.CashManagementNo, '')+'&utype=' as PaymentUrl>"
                );

            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            query.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            query.LeftJoin(usr).On(query.CreatedBy == usr.UserID);

            query.Where
                (
                    query.RegistrationNo.In(mrg),
                    query.TransactionCode == BusinessObject.Reference.TransactionCode.DownPayment
                );
            if (!string.IsNullOrEmpty(txtPaymentNo.Text))
                query.Where(query.PaymentNo == txtPaymentNo.Text);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }
        protected void grdListD_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            string patId = e.DetailTableView.ParentItem.GetDataKeyValue("PatientID").ToString();

            var query = new TransPaymentQuery("a");
            var patient = new PatientQuery("c");
            var usr = new AppUserQuery("e");

            query.es.Top = AppSession.Parameter.MaxResultRecord;

            query.Select
                (
                    query.PaymentNo,
                    query.RegistrationNo,
                    patient.PatientID,
                    patient.MedicalNo,
                    patient.PatientName,
                    query.PaymentDate,
                    query.PaymentTime,
                    query.IsApproved,
                    query.IsVoid,
                    usr.UserName,
                    query.PaymentReferenceNo,
                    query.IsVisiteDownPayment,
                    @"<ISNULL(a.CashManagementNo, '') AS CashManagementNo>",
                    @"<(SELECT SUM(f.[Amount]) 
                        FROM TransPaymentItem f 
                        WHERE a.[PaymentNo] = f.[PaymentNo]) AS 'TotalPaymentAmount'>",
                    "<'DownPaymentDetail.aspx?md=view&id='+a.PaymentNo+'&regno='+a.RegistrationNo+'&cmno='+ISNULL(a.CashManagementNo, '')+'&utype=' as PaymentUrl>"
                );

            query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
            query.LeftJoin(usr).On(query.CreatedBy == usr.UserID);

            query.Where
                (
                    query.TransactionCode == BusinessObject.Reference.TransactionCode.DownPayment,
                    query.PatientID == patId,
                    query.RegistrationNo == string.Empty
                );
            if (!string.IsNullOrEmpty(txtPaymentNo.Text))
                query.Where(query.PaymentNo == txtPaymentNo.Text);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }
    }
}
