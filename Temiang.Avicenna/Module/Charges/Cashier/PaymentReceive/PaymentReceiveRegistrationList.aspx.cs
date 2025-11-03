using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class PaymentReceiveRegistrationList : BasePage
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

            switch (Request.QueryString["pc"])
            {
                case "no":
                    ProgramID = AppConstant.Program.PaymentReceive;
                    break;
                case "yes":
                    ProgramID = AppConstant.Program.PaymentReceiveLinkToPettyCash;
                    break;
            }

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

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (AppSession.Parameter.HealthcareInitialAppsVersion != "RSSA")
                SaveValueToCookie();

            grdList.Rebind();
        }

        private DataTable Registrations
        {
            get
            {
                var isEmptyFilter = txtOrderDate1.IsEmpty && txtOrderDate2.IsEmpty && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) &&
                    string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientName.Text) && string.IsNullOrEmpty(txtPaymentNo.Text) && string.IsNullOrEmpty(cboGuarantorID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Registration")) return null;

                var qr = new RegistrationQuery("r");
                var qp = new PatientQuery("p");
                var qm = new ParamedicQuery("m");
                var unit = new ServiceUnitQuery("s");
                var room = new ServiceRoomQuery("d");
                var mrg = new MergeBillingQuery("b");
                var grr = new GuarantorQuery("c");
                var sal = new AppStandardReferenceItemQuery("sal");
                var sumInfo = new RegistrationInfoSumaryQuery("h");
                var gdc = new GuarantorDocumentChecklistQuery("gdc");
                var dc = new AppStandardReferenceItemQuery("dc");
                
                qr.es.Top = AppSession.Parameter.MaxResultRecord;
                qr.es.Distinct = true;

                qr.Select
                    (
                        qr.PatientID,
                        qr.RegistrationNo,
                        qr.RegistrationDate,
                        qr.RegistrationTime,
                        qp.MedicalNo,
                        //qp.PatientName,
                        "<(LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) + case when (r.IsNonPatient = 1 and ISNULL(r.DischargeNotes,'') <> '') then ' (['+r.DischargeMedicalNotes+'] ' + r.DischargeNotes + ')' else '' end) as PatientName>",
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
                        qr.DischargeNotes, qr.DischargeMedicalNotes,
                        qr.IsNonPatient,
                        @"<CAST(1 AS BIT) AS IsChasierCheckin>",
                        @"<'' AS CashManagementNo>",
                        @"<CASE WHEN h.NoteCount <= 0 THEN NULL ELSE h.NoteCount END AS NoteCount>",
                        @"<CASE WHEN dc.LineNumber IS NULL OR (dc.LineNumber - h.DocumentCheckListCount) <= 0 THEN NULL ELSE (dc.LineNumber - h.DocumentCheckListCount) END AS DocumentCheckListCountRemains>"
                        //,
                        //mrg.FromRegistrationNo
                    );

                if (AppSession.Parameter.IsShowArReceiptInVerificationAndPaymentList)
                    qr.Select(@"<CASE WHEN (SELECT TOP 1 tp.PaymentNo FROM TransPayment tp 
                                        INNER JOIN TransPaymentItem tpi ON tpi.PaymentNo = tp.PaymentNo
                                    WHERE tp.RegistrationNo = r.RegistrationNo AND tp.TransactionCode = '016' AND tp.IsVoid = 0 AND tp.IsApproved = 1
                                        AND tpi.SRPaymentType IN ('PaymentType-002', 'PaymentType-003', 'PaymentType-004')
                            ) IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsArReceipt'>");
                else
                    qr.Select(@"<CAST(0 AS BIT) AS 'IsArReceipt'>");

                qr.InnerJoin(qp).On(qr.PatientID == qp.PatientID);
                qr.LeftJoin(qm).On(qr.ParamedicID == qm.ParamedicID);
                qr.LeftJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);
                qr.LeftJoin(room).On(qr.RoomID == room.RoomID);
                qr.InnerJoin(mrg).On(qr.RegistrationNo == mrg.RegistrationNo);
                qr.InnerJoin(grr).On(qr.GuarantorID == grr.GuarantorID);
                qr.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qp.SRSalutation == sal.ItemID);
                qr.LeftJoin(sumInfo).On(qr.RegistrationNo == sumInfo.RegistrationNo);
                qr.LeftJoin(gdc).On(qr.GuarantorID == gdc.GuarantorID & qr.SRRegistrationType == gdc.SRRegistrationType);
                qr.LeftJoin(dc).On(dc.StandardReferenceID == "DocumentChecklist" & gdc.SRDocumentChecklist == dc.ItemID);

                if (!txtOrderDate1.IsEmpty && !txtOrderDate2.IsEmpty)
                    qr.Where(qr.RegistrationDate >= txtOrderDate1.SelectedDate, qr.RegistrationDate < txtOrderDate2.SelectedDate.Value.AddDays(1));
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    qr.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    //qr.Where(
                    //    qr.Or(
                    //        qp.MedicalNo == searchReg,
                    //        qp.OldMedicalNo == searchReg,
                    //        qr.RegistrationNo == searchReg,
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
                    //      string.Format("<(LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) + case when (r.IsNonPatient = 1 and ISNULL(r.DischargeNotes,'') <> '') then ' (['+r.DischargeMedicalNotes+'] ' + r.DischargeNotes + ')' else '' end) LIKE '{0}'>", searchPatient)
                    //    );
                }

                if (!string.IsNullOrEmpty(txtPaymentNo.Text))
                {
                    var py = new TransPaymentQuery("py");
                    qr.InnerJoin(py).On(qr.RegistrationNo == py.RegistrationNo);
                    qr.Where(py.PaymentNo == txtPaymentNo.Text, py.TransactionCode == TransactionCode.Payment);
                }

                if (cboGuarantorID.SelectedValue != string.Empty)
                    qr.Where(qr.GuarantorID == cboGuarantorID.SelectedValue);

                //qr.Where(qr.IsClosed == false, qr.IsVoid == false, 
                //    qr.ServiceUnitID != AppSession.Parameter.ServiceUnitIDForCafe);

                qr.Where(qr.IsClosed == false, qr.IsVoid == false);

                if (!AppSession.Parameter.IsSeparatePaymentForOpConsul)
                    qr.Where(qr.Or(qr.IsConsul == false, mrg.FromRegistrationNo == string.Empty));

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

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            string[] regno = Helper.MergeBilling.GetMergeRegistration(e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString());

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
                    patient.MedicalNo,
                    patient.PatientName,
                    unit.ServiceUnitName,
                    query.PaymentDate,
                    query.PaymentTime,
                    query.IsApproved,
                    query.IsVoid,
                    usr.UserName,
                    query.Notes,
                    @"<ISNULL(a.CashManagementNo, '') AS CashManagementNo>",
                    "<(SELECT ISNULL(SUM(f.[Amount]),0) FROM TransPaymentItem f WHERE a.[PaymentNo] = f.[PaymentNo]) AS 'TotalPaymentAmount'>"
                );

            if (Request.QueryString["pc"] == "yes")
                query.Select(
                    "<'PaymentReceiveDetail.aspx?md=view&id='+a.PaymentNo+'&regno='+a.RegistrationNo+'&pc=yes&cmno='+ISNULL(a.CashManagementNo, '')+'&utype=' as PaymentUrl>");
            else
                query.Select(
                    "<'PaymentReceiveDetail.aspx?md=view&id='+a.PaymentNo+'&regno='+a.RegistrationNo+'&pc=no&cmno='+ISNULL(a.CashManagementNo, '')+'&utype=' as PaymentUrl>");

            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            query.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            query.LeftJoin(usr).On(query.CreatedBy == usr.UserID);

            query.Where(query.RegistrationNo.In(regno), query.TransactionCode == TransactionCode.Payment);

            if (!string.IsNullOrEmpty(txtPaymentNo.Text))
                query.Where(query.PaymentNo == txtPaymentNo.Text);

            e.DetailTableView.DataSource = query.LoadDataTable();
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

                var payment = new TransPayment();
                if (payment.LoadByPrimaryKey(param[1]) && payment.IsVoid == true)
                {
                    var coll = new TransPaymentCollection();
                    coll.Query.Where(coll.Query.PaymentReferenceNo == param[1]);
                    coll.LoadAll();
                    foreach (var dp in coll)
                    {
                        dp.PaymentReferenceNo = string.Empty;
                        dp.LastUpdateDateTime = DateTime.Now;
                        dp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }

                    var collItem = new TransPaymentItemOrderCollection();
                    collItem.Query.Where(collItem.Query.PaymentNo == param[1]);
                    collItem.LoadAll();
                    foreach (var tpio in collItem)
                    {
                        tpio.IsPaymentProceed = false;
                        tpio.LastUpdateDateTime = DateTime.Now;
                        tpio.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }

                    var collIb = new TransPaymentItemIntermBillCollection();
                    collIb.Query.Where(collIb.Query.PaymentNo == param[1]);
                    collIb.LoadAll();
                    foreach (var ib in collIb)
                    {
                        ib.IsPaymentProceed = false;
                        ib.LastUpdateDateTime = DateTime.Now;
                        ib.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }

                    var collIbg = new TransPaymentItemIntermBillGuarantorCollection();
                    collIbg.Query.Where(collIbg.Query.PaymentNo == param[1]);
                    collIbg.LoadAll();
                    foreach (var ib in collIbg)
                    {
                        ib.IsPaymentProceed = false;
                        ib.LastUpdateDateTime = DateTime.Now;
                        ib.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }

                    using (var trans = new esTransactionScope())
                    {
                        coll.Save();
                        collItem.Save();
                        collIb.Save();
                        collIbg.Save();

                        trans.Complete();
                    }
                }

                grdList.Rebind();
            }
        }
    }
}
