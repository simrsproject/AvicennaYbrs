using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class PaymentReceiveCashierList : BasePage
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

            ProgramID = AppConstant.Program.PaymentReceiveCashier;

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
            ComboBox.PopulateWithGuarantor(cboGuarantorID);
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

            var dataSource = OutStandings;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
        }

        protected void grdListPayment_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = Payments;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();
            grdList.Rebind();
            grdListPayment.Rebind();
        }

        private DataTable OutStandings
        {
            get
            {
                var isEmptyFilter = txtPaymentDate1.IsEmpty && txtPaymentDate2.IsEmpty && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPaymentNo.Text) && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtPatientName.Text) && string.IsNullOrEmpty(cboGuarantorID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Payment Received (Cashier)")) return null;

                var qpy = new TransPaymentQuery("py");
                var qpyi = new TransPaymentItemQuery("pyi");
                var qreg = new RegistrationQuery("r");
                var qpat = new PatientQuery("p");
                var qpar = new ParamedicQuery("m");
                var qunit = new ServiceUnitQuery("s");
                var qgrr = new GuarantorQuery("c");
                var qsal = new AppStandardReferenceItemQuery("sal");
                var qsumInfo = new RegistrationInfoSumaryQuery("h");

                qpy.es.Top = AppSession.Parameter.MaxResultRecord;
                qpy.es.Distinct = true;

                qpy.Select
                (
                    qpy.PaymentNo,
                    qpy.PaymentDate,
                    qpy.PaymentDate.Date().As("Group"),
                    qpy.PaymentTime,
                    qpy.RegistrationNo,
                    qreg.PatientID,
                    qreg.RegistrationDate,
                    qreg.RegistrationTime,
                    qpat.MedicalNo,
                    "<(LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) + case when (r.IsNonPatient = 1 and ISNULL(r.DischargeNotes,'') <> '') then ' (['+r.DischargeMedicalNotes+'] ' + r.DischargeNotes + ')' else '' end) as PatientName>",
                    qpat.Sex,
                    qpar.ParamedicName,
                    qunit.ServiceUnitName,
                    qreg.IsTransferedToInpatient,
                    qreg.SRRegistrationType,
                    qgrr.GuarantorName,
                    qreg.IsConsul,
                    qreg.ServiceUnitID,
                    qsal.ItemName.As("SalutationName"),
                    qreg.DischargeNotes, qreg.DischargeMedicalNotes,
                    qreg.IsNonPatient,
                    @"<CAST(1 AS BIT) AS IsChasierCheckin>",
                    @"<'' AS CashManagementNo>",
                    @"<CASE WHEN h.NoteCount <= 0 THEN NULL ELSE h.NoteCount END AS NoteCount>",
                    "<(SELECT ISNULL(SUM(f.[Amount]),0) FROM TransPaymentItem f WHERE py.[PaymentNo] = f.[PaymentNo]) AS 'TotalPaymentAmount'>",
                    qpy.IsApproved,
                    qpy.IsVoid,
                    "<'PaymentReceiveDetail.aspx?md=view&id='+py.PaymentNo+'&regno='+r.RegistrationNo+'&pc=no&utype=c&cmno='+ISNULL(py.CashManagementNo, '') as PaymentUrl>"
                );

                qpy.InnerJoin(qpyi).On(qpyi.PaymentNo == qpy.PaymentNo &&
                                       qpyi.SRPaymentType != AppSession.Parameter.PaymentTypeCorporateAR);
                qpy.InnerJoin(qreg).On(qreg.RegistrationNo == qpy.RegistrationNo);
                qpy.InnerJoin(qpat).On(qpat.PatientID == qreg.PatientID);
                qpy.LeftJoin(qpar).On(qpar.ParamedicID == qreg.ParamedicID);
                qpy.LeftJoin(qunit).On(qunit.ServiceUnitID == qreg.ServiceUnitID);
                qpy.InnerJoin(qgrr).On(qgrr.GuarantorID == qreg.GuarantorID);
                qpy.LeftJoin(qsal).On(qsal.StandardReferenceID == "Salutation" & qsal.ItemID == qpat.SRSalutation);
                qpy.LeftJoin(qsumInfo).On(qsumInfo.RegistrationNo == qpy.RegistrationNo);

                if (!txtPaymentDate1.IsEmpty && !txtPaymentDate2.IsEmpty)
                    qpy.Where(qpy.PaymentDate >= txtPaymentDate1.SelectedDate, qpy.PaymentDate < txtPaymentDate2.SelectedDate.Value.AddDays(1));
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    qpy.Where(qreg.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    if (searchReg.ToUpper().Contains("REG"))
                    {
                        qpy.Where(qpy.RegistrationNo == searchReg);
                    }
                    else
                    {
                        string reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());
                        qpy.Where(
                            qpy.Or(
                                qpat.ReverseMedicalNo.Like(reverseMedNoSearch),
                                qpat.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                                )
                            );
                    }
                    //if (AppSession.Parameter.IsMedicalNoContainStrip)
                    //    qpy.Where(
                    //        qpy.Or(
                    //            qpat.MedicalNo == searchReg,
                    //            qpat.OldMedicalNo == searchReg,
                    //            qpy.RegistrationNo == searchReg,
                    //            string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //            string.Format("< OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //            )
                    //        );
                    //else
                    //    qpy.Where(
                    //        qpy.Or(
                    //            qpat.MedicalNo == searchReg,
                    //            qpat.OldMedicalNo == searchReg,
                    //            qpy.RegistrationNo == searchReg,
                    //            string.Format("< OR p.MedicalNo LIKE '%{0}%'>", searchReg),
                    //            string.Format("< OR p.OldMedicalNo LIKE '%{0}%'>", searchReg)
                    //            )
                    //        );
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    qpy.Where
                        (
                          string.Format("<(LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) + case when (r.IsNonPatient = 1 and ISNULL(r.DischargeNotes,'') <> '') then ' (['+r.DischargeMedicalNotes+'] ' + r.DischargeNotes + ')' else '' end) LIKE '{0}'>", searchPatient)
                        );
                }

                if (!string.IsNullOrEmpty(txtPaymentNo.Text))
                {
                    qpy.Where(qpy.PaymentNo == txtPaymentNo.Text);
                }

                if (cboGuarantorID.SelectedValue != string.Empty)
                    qpy.Where(qreg.GuarantorID == cboGuarantorID.SelectedValue);

                qpy.Where(qpy.TransactionCode == TransactionCode.Payment, qpy.IsApproved == false, 
                    qpy.IsVoid == false, qreg.IsClosed == false, qreg.IsVoid == false,
                    qreg.ServiceUnitID != AppSession.Parameter.ServiceUnitIDForCafe);
                qpy.OrderBy(qpy.PaymentDate.Ascending, qpy.PaymentNo.Ascending);

                DataTable tbl = qpy.LoadDataTable();

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
            var query = new TransPaymentItemQuery("a");
            var srQuery = new PaymentMethodQuery("b");
            var srQuery2 = new PaymentTypeQuery("c");

            query.Select
                (
                    query,
                    srQuery2.PaymentTypeName.As("PaymentTypeName"),
                    srQuery.PaymentMethodName.As("PaymentMethodName")
                );
            query.InnerJoin(srQuery2).On(query.SRPaymentType == srQuery2.SRPaymentTypeID);
            query.LeftJoin(srQuery).On
                (
                    srQuery2.SRPaymentTypeID == srQuery.SRPaymentTypeID &
                    query.SRPaymentMethod == srQuery.SRPaymentMethodID
                );
            query.Where(query.PaymentNo == e.DetailTableView.ParentItem.GetDataKeyValue("PaymentNo").ToString());
            query.OrderBy(query.SequenceNo.Ascending);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        private DataTable Payments
        {
            get
            {
                var isEmptyFilter = txtPaymentDate1.IsEmpty && txtPaymentDate2.IsEmpty && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPaymentNo.Text) && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtPatientName.Text) && string.IsNullOrEmpty(cboGuarantorID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Payment Received (Cashier)")) return null;

                var qpy = new TransPaymentQuery("py");
                var qpyi = new TransPaymentItemQuery("pyi");
                var qreg = new RegistrationQuery("r");
                var qpat = new PatientQuery("p");
                var qpar = new ParamedicQuery("m");
                var qunit = new ServiceUnitQuery("s");
                var qgrr = new GuarantorQuery("c");
                var qsal = new AppStandardReferenceItemQuery("sal");
                var qsumInfo = new RegistrationInfoSumaryQuery("h");

                qpy.es.Top = AppSession.Parameter.MaxResultRecord;
                qpy.es.Distinct = true;

                qpy.Select
                (
                    qpy.PaymentNo,
                    qpy.PaymentDate,
                    qpy.ApproveDate,
                    qpy.ApproveDate.Date().As("Group"),
                    @"<LEFT(CONVERT(VARCHAR, py.ApproveDate, 14),5) AS ApprovedTime>",
                    qpy.RegistrationNo,
                    qreg.PatientID,
                    qreg.RegistrationDate,
                    qreg.RegistrationTime,
                    qpat.MedicalNo,
                    "<(LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) + case when (r.IsNonPatient = 1 and ISNULL(r.DischargeNotes,'') <> '') then ' (['+r.DischargeMedicalNotes+'] ' + r.DischargeNotes + ')' else '' end) as PatientName>",
                    qpat.Sex,
                    qpar.ParamedicName,
                    qunit.ServiceUnitName,
                    qreg.IsTransferedToInpatient,
                    qreg.SRRegistrationType,
                    qgrr.GuarantorName,
                    qreg.IsConsul,
                    qreg.ServiceUnitID,
                    qsal.ItemName.As("SalutationName"),
                    qreg.DischargeNotes, qreg.DischargeMedicalNotes,
                    qreg.IsNonPatient,
                    @"<CAST(1 AS BIT) AS IsChasierCheckin>",
                    @"<'' AS CashManagementNo>",
                    @"<CASE WHEN h.NoteCount <= 0 THEN NULL ELSE h.NoteCount END AS NoteCount>",
                    "<(SELECT ISNULL(SUM(f.[Amount]),0) FROM TransPaymentItem f WHERE py.[PaymentNo] = f.[PaymentNo]) AS 'TotalPaymentAmount'>",
                    qpy.IsApproved,
                    qpy.IsVoid,
                    "<'PaymentReceiveDetail.aspx?md=view&id='+py.PaymentNo+'&regno='+r.RegistrationNo+'&pc=no&utype=c&cmno='+ISNULL(py.CashManagementNo, '') as PaymentUrl>"
                );

                qpy.InnerJoin(qpyi).On(qpyi.PaymentNo == qpy.PaymentNo &&
                                       qpyi.SRPaymentType != AppSession.Parameter.PaymentTypeCorporateAR);
                qpy.InnerJoin(qreg).On(qreg.RegistrationNo == qpy.RegistrationNo);
                qpy.InnerJoin(qpat).On(qpat.PatientID == qreg.PatientID);
                qpy.LeftJoin(qpar).On(qpar.ParamedicID == qreg.ParamedicID);
                qpy.LeftJoin(qunit).On(qunit.ServiceUnitID == qreg.ServiceUnitID);
                qpy.InnerJoin(qgrr).On(qgrr.GuarantorID == qreg.GuarantorID);
                qpy.LeftJoin(qsal).On(qsal.StandardReferenceID == "Salutation" & qsal.ItemID == qpat.SRSalutation);
                qpy.LeftJoin(qsumInfo).On(qsumInfo.RegistrationNo == qpy.RegistrationNo);

                if (!txtPaymentDate1.IsEmpty && !txtPaymentDate2.IsEmpty)
                    qpy.Where(qpy.ApproveDate >= txtPaymentDate1.SelectedDate, qpy.ApproveDate < txtPaymentDate2.SelectedDate.Value.AddDays(1));
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    qpy.Where(qreg.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    if (searchReg.ToUpper().Contains("REG"))
                    {
                        qpy.Where(qpy.RegistrationNo == searchReg);
                    }
                    else {
                        string reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());
                        qpy.Where(
                            qpy.Or(
                                qpat.ReverseMedicalNo.Like(reverseMedNoSearch),
                                qpat.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                                )
                            );
                    }
                    //if (AppSession.Parameter.IsMedicalNoContainStrip)
                    //    qpy.Where(
                    //        qpy.Or(
                    //            qpat.MedicalNo == searchReg,
                    //            qpat.OldMedicalNo == searchReg,
                    //            qpy.RegistrationNo == searchReg,
                    //            string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //            string.Format("< OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //            )
                    //        );
                    //else
                    //    qpy.Where(
                    //        qpy.Or(
                    //            qpat.MedicalNo == searchReg,
                    //            qpat.OldMedicalNo == searchReg,
                    //            qpy.RegistrationNo == searchReg,
                    //            string.Format("< OR p.MedicalNo LIKE '%{0}%'>", searchReg),
                    //            string.Format("< OR p.OldMedicalNo LIKE '%{0}%'>", searchReg)
                    //            )
                    //        );
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    qpy.Where
                        (
                          string.Format("<(LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) + case when (r.IsNonPatient = 1 and ISNULL(r.DischargeNotes,'') <> '') then ' (['+r.DischargeMedicalNotes+'] ' + r.DischargeNotes + ')' else '' end) LIKE '{0}'>", searchPatient)
                        );
                }

                if (!string.IsNullOrEmpty(txtPaymentNo.Text))
                {
                    qpy.Where(qpy.PaymentNo == txtPaymentNo.Text);
                }

                if (cboGuarantorID.SelectedValue != string.Empty)
                    qpy.Where(qreg.GuarantorID == cboGuarantorID.SelectedValue);

                qpy.Where(qpy.TransactionCode == TransactionCode.Payment, qpy.IsApproved == true, 
                    qpy.ApproveByUserID == AppSession.UserLogin.UserID,
                    qreg.ServiceUnitID != AppSession.Parameter.ServiceUnitIDForCafe);
                qpy.OrderBy(qpy.ApproveDate.Descending, qpy.PaymentNo.Ascending);

                DataTable tbl = qpy.LoadDataTable();

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

        protected void grdListPayment_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var query = new TransPaymentItemQuery("a");
            var srQuery = new PaymentMethodQuery("b");
            var srQuery2 = new PaymentTypeQuery("c");

            query.Select
                (
                    query,
                    srQuery2.PaymentTypeName.As("PaymentTypeName"),
                    srQuery.PaymentMethodName.As("PaymentMethodName")
                );
            query.InnerJoin(srQuery2).On(query.SRPaymentType == srQuery2.SRPaymentTypeID);
            query.LeftJoin(srQuery).On
                (
                    srQuery2.SRPaymentTypeID == srQuery.SRPaymentTypeID &
                    query.SRPaymentMethod == srQuery.SRPaymentMethodID
                );
            query.Where(query.PaymentNo == e.DetailTableView.ParentItem.GetDataKeyValue("PaymentNo").ToString());
            query.OrderBy(query.SequenceNo.Ascending);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;
        }
    }
}