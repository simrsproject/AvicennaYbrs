using System;
using System.Drawing;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class FinalizeBillingList : BasePage
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

            ProgramID = AppConstant.Program.VerificationFinalizeBilling;

            if (!IsPostBack)
            {
                //RADT.Cpoe.EmrList.GuarantorBpjs = null; // Reset list Guarantor Bpjs (u/ perhitungan plafond) ///dipindah ke webservice

                var coll = new ServiceUnitCollection();

                if (AppSession.Parameter.HealthcareInitial != "RSYS")
                {
                    var unit = new ServiceUnitQuery("a");
                    if (!this.IsUserCrossUnitAble)
                    {
                        var usr = new AppUserServiceUnitQuery("usr");
                        unit.InnerJoin(usr).On(unit.ServiceUnitID == usr.ServiceUnitID &&
                                               usr.UserID == AppSession.UserLogin.UserID);
                    }
                    unit.Where
                        (
                            unit.SRRegistrationType.In(
                                AppConstant.RegistrationType.EmergencyPatient,
                                AppConstant.RegistrationType.InPatient,
                                AppConstant.RegistrationType.OutPatient,
                                AppConstant.RegistrationType.MedicalCheckUp
                            ),
                            unit.IsActive == true
                        );
                    unit.OrderBy(unit.DepartmentID.Ascending);

                    coll.Load(unit);

                    cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    foreach (ServiceUnit entity in coll)
                    {
                        cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                    }
                }
                else
                {
                    //service unit
                    var query = new ServiceUnitQuery("a");

                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                    query.Where(qusr.UserID == AppSession.UserLogin.UserID);

                    query.Where(
                        query.SRRegistrationType.In(
                            AppConstant.RegistrationType.ClusterPatient,
                            AppConstant.RegistrationType.EmergencyPatient,
                            AppConstant.RegistrationType.InPatient,
                            AppConstant.RegistrationType.OutPatient,
                            AppConstant.RegistrationType.MedicalCheckUp
                            ),
                        query.IsActive == true
                        );

                    query.OrderBy(query.DepartmentID.Ascending);
                    coll.Load(query);

                    cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    foreach (ServiceUnit entity in coll)
                    {
                        cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                    }
                }

                // pasien consul tetep muncul, tp billing tetap harus lihat registrasi utama
                grdRegisterOpenList.Columns[1].Visible = !AppSession.Parameter.IsSeparatePaymentForOpConsul && AppSession.Parameter.IsShowRegConsulOnVerificationBilling;
                // kebalikan dari kondisi di atas
                grdRegisterOpenList.Columns[0].Visible = !grdRegisterOpenList.Columns[1].Visible;

                // sembunyikan tombol close untuk user biasa
                grdRegisterOpenList.Columns.FindByUniqueName("templateProcessClosed").Visible = this.IsPowerUser && !(AppSession.Parameter.IsHideOpenCloseOnVerificationForUser);
                // tombol refer hanya u/ group sentra
                grdRegisterOpenList.Columns.FindByUniqueName("openWinTransfer").Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB";

                grdRegisterOpenList.Columns.FindByUniqueName("RealizationStatus").Visible = AppSession.Parameter.IsShowRealizationOrderTransactionStatus;

                grdRegisterOpenList.Columns.FindByUniqueName("PlafondProgress").Visible = AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsEmrListShowPlafondProgress);

                StandardReference.InitializeIncludeSpace(cboRegistrationType, AppEnum.StandardReference.RegistrationType);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                RestoreValueFromCookie();
            }
        }

        protected void grdRegisterOpenList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
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


        private DataTable Registrations
        {
            get
            {
                var isEmptyFilter = txtOrderDate1.IsEmpty && txtOrderDate2.IsEmpty && txtDischargePlanDate.IsEmpty && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) && 
                    string.IsNullOrEmpty(cboRegistrationType.SelectedValue) && string.IsNullOrEmpty(txtPatientName.Text) && string.IsNullOrEmpty(cboGuarantorID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Registration")) return null;

                var qr = new RegistrationQuery("r");
                var qp = new PatientQuery("p");
                var qm = new ParamedicQuery("m");
                var unit = new ServiceUnitQuery("s");
                var room = new ServiceRoomQuery("d");
                var mrg = new MergeBillingQuery("b");
                var guar = new GuarantorQuery("g");
                var infoSum = new RegistrationInfoSumaryQuery("h");
                var sal = new AppStandardReferenceItemQuery("sal");
                var gdc = new GuarantorDocumentChecklistQuery("gdc");
                var dc = new AppStandardReferenceItemQuery("dc");
                var bd = new BedQuery("bd");
                var cl = new ClassQuery("cl");
                var cl2 = new ClassQuery("cl2");

                qr.es.Top = AppSession.Parameter.MaxResultRecord;

                qr.Select
                    (
                    qr.PatientID,
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
                        @"<cl2.ClassName +'/'+ cl.ClassName AS ChargeClassID2>",
                        //@"<cl2.ClassName AS CoverageClassID2>",
                        qr.IsTransferedToInpatient,
                        qr.SRRegistrationType,
                        qr.IsHoldTransactionEntry,
                        guar.GuarantorName,
                        qr.DischargePlanDate,
                        qr.IsConsul,
                        qr.SRRegistrationType,
                        @"<CASE WHEN h.NoteCount <= 0 THEN NULL ELSE h.NoteCount END AS NoteCount>",
                        @"<CASE WHEN dc.LineNumber IS NULL OR (dc.LineNumber - h.DocumentCheckListCount) <= 0 THEN NULL ELSE (dc.LineNumber - h.DocumentCheckListCount) END AS DocumentCheckListCountRemains>",
                        qr.DischargeDate,
                        "<CASE WHEN r.DischargeDate IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsDischarge>",
                        qr.ChargeClassID, qr.CoverageClassID, qr.ClassID, @"<ISNULL(bd.DefaultChargeClassID, r.ChargeClassID) AS DefaultClassID>",
                        qr.PlavonAmount, sal.ItemName.As("SalutationName"),
                        "<CASE WHEN (r.ParamedicID IS NULL OR r.IsConsul = 1) THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsNewVisible>",
                        "<CASE WHEN b.FromRegistrationNo = '' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsMergeBilling>",
                        @"<CAST(0 AS BIT) AS IsOrderRealization>",
                        qr.IsClosed,
                        @"<CASE WHEN r.SRRegistrationType = 'IPR' AND (r.DischargeDate IS NULL OR r.SRDischargeMethod = '') THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsAllowClosed'>",
                        @"<CAST(cl.ClassSeq AS VARCHAR) AS ClassSeq1>", 
                        @"<CAST(cl2.ClassSeq AS VARCHAR) AS ClassSeq2>"
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
                qr.InnerJoin(guar).On(qr.GuarantorID == guar.GuarantorID);
                qr.LeftJoin(infoSum).On(qr.RegistrationNo == infoSum.RegistrationNo);
                qr.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qp.SRSalutation == sal.ItemID);
                qr.LeftJoin(gdc).On(qr.GuarantorID == gdc.GuarantorID & qr.SRRegistrationType == gdc.SRRegistrationType);
                qr.LeftJoin(dc).On(dc.StandardReferenceID == "DocumentChecklist" & gdc.SRDocumentChecklist == dc.ItemID);
                qr.LeftJoin(bd).On(bd.BedID == qr.BedID);
                qr.LeftJoin(cl).On(cl.ClassID == qr.ChargeClassID);
                qr.LeftJoin(cl2).On(cl2.ClassID == qr.CoverageClassID);

                if (!txtOrderDate1.IsEmpty && !txtOrderDate2.IsEmpty)
                    qr.Where(qr.RegistrationDate >= txtOrderDate1.SelectedDate, qr.RegistrationDate <= txtOrderDate2.SelectedDate);
                if (!txtDischargePlanDate.IsEmpty)
                    qr.Where(qr.DischargePlanDate >= txtDischargePlanDate.SelectedDate, qr.DischargePlanDate < txtDischargePlanDate.SelectedDate.Value.AddDays(1));
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    qr.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    Helper.AddFilterMedNoOrRegNoOrPatName(qr, qp, searchReg, "registration");
                }

                if (!string.IsNullOrEmpty(cboRegistrationType.SelectedValue))
                {
                    qr.Where(qr.SRRegistrationType == cboRegistrationType.SelectedValue);
                    //isFilter = true;
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

                if (!string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
                    qr.Where(qr.GuarantorID == cboGuarantorID.SelectedValue);

                qr.Where(
                    qr.IsClosed == false,
                    qr.IsVoid == false
                    );

                if (!AppSession.Parameter.IsSeparatePaymentForOpConsul && !AppSession.Parameter.IsShowRegConsulOnVerificationBilling)
                    qr.Where(qr.Or(qr.IsConsul == false, mrg.FromRegistrationNo == string.Empty));

                if (!this.IsUserCrossUnitAble)
                {
                    var usr = new AppUserServiceUnitQuery("usr");
                    qr.InnerJoin(usr).On(unit.ServiceUnitID == usr.ServiceUnitID &&
                                         usr.UserID == AppSession.UserLogin.UserID);
                }

                if (chkReadyToDischarge.Checked)
                {
                    qr.Where(qr.DischargeDate.IsNull(), qr.DischargePlanDate.IsNotNull());
                }

                qr.OrderBy(qr.RegistrationDate.Descending, qr.ServiceUnitID.Ascending, qr.RegistrationNo.Descending);

                DataTable tbl = qr.LoadDataTable();

                if (AppSession.Parameter.IsShowRealizationOrderTransactionStatus)
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        var tcq = new TransChargesQuery("a");
                        var tciq = new TransChargesItemQuery("b");
                        tcq.InnerJoin(tciq).On(tciq.TransactionNo == tcq.TransactionNo && tcq.IsOrder == true && tciq.IsOrderRealization == true);
                        tcq.Where(tcq.RegistrationNo == row["RegistrationNo"].ToString());
                        if (tcq.LoadDataTable().Rows.Count > 0)
                            row["IsOrderRealization"] = true;
                        else
                        {
                            var tpq = new TransPrescriptionQuery("a");
                            tpq.Where(tpq.RegistrationNo == row["RegistrationNo"].ToString(), tpq.IsFromSOAP == true, tpq.IsApproval == true);
                            if (tpq.LoadDataTable().Rows.Count > 0)
                                row["IsOrderRealization"] = true;
                        }
                    }
                    tbl.AcceptChanges();
                }

                return tbl;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (AppSession.Parameter.HealthcareInitialAppsVersion != "RSSA")
                SaveValueToCookie();

            grdRegisterOpenList.DataSource = Registrations;
            grdRegisterOpenList.DataBind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (!(source is RadGrid)) return;

            if (eventArgument == "rebind")
                grdRegisterOpenList.Rebind();

            else if (eventArgument.Contains("processClosed"))
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(eventArgument.Split('|')[1]);

                var lanjut = true;
                if (AppSession.Parameter.IsVerificationBillingAuthorizationActivated)
                {
                    var programId = ProgramID;
                    switch (reg.SRRegistrationType)
                    {
                        case AppConstant.RegistrationType.InPatient:
                            programId = AppConstant.Program.InPatientCloseOpenRegistration;
                            break;
                        case AppConstant.RegistrationType.OutPatient:
                            programId = AppConstant.Program.OutPatientCloseOpenRegistration;
                            break;
                        case AppConstant.RegistrationType.EmergencyPatient:
                            programId = AppConstant.Program.EmergencyPatientCloseOpenRegistration;
                            break;
                        case AppConstant.RegistrationType.MedicalCheckUp:
                            programId = AppConstant.Program.HealthScreeningCloseOpenRegistration;
                            break;
                    }
                    var userAccess = new UserAccess(AppSession.UserLogin.UserID, programId);
                    if (!userAccess.IsEditAble)
                        lanjut = false;
                }

                if (!lanjut)
                    return;

                bool isClosed = reg.IsClosed ?? false;
                if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient && reg.DischargeDate == null && !isClosed)
                    return;

                Helper.RegistrationOpenClose.SetClosed(eventArgument.Split('|')[1], "Verification & Finalize Billing");
                grdRegisterOpenList.Rebind();
            }
        }

        protected void grdRegisterOpenList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var tooltip = string.Empty;
                var dataItem = e.Item as GridDataItem;
                if (dataItem["ChargeClassID"].Text != dataItem["CoverageClassID"].Text)
                {
                    // Beri warna merah jika CoverageClassID berbeda dg ChargeClassID Up, 
                    // Beri warna biru jika CoverageClassID berbeda dg ChargeClassID Down, 
                    var classSeq1 = dataItem["ClassSeq1"].Text.ToInt();
                    var classSeq2 = dataItem["ClassSeq2"].Text.ToInt();

                    dataItem.ForeColor = classSeq1 < classSeq2 ? Color.Red : Color.Blue;
                    dataItem.Font.Bold = true;
                    tooltip = "Charge class is different from coverage class.";
                }
                if (dataItem["ChargeClassID"].Text != dataItem["DefaultClassID"].Text)
                {
                    var c = new Class();
                    c.LoadByPrimaryKey(dataItem["DefaultClassID"].Text);
                    if (c.IsTariffClass ?? false)
                    {
                        dataItem.Font.Bold = true;
                        dataItem.Font.Italic = true;
                        tooltip = tooltip == string.Empty ? "Charge class is different from bed class." : "Charge class is different from coverage and bed class.";
                    }
                }
                dataItem.ToolTip = tooltip;
            }
        }

        #region Plafond Balance

        private string[] GuarantorBPJS
        {
            get
            {
                if (ViewState["bpjs"] != null) return (string[])ViewState["bpjs"];
                var grr = new GuarantorBridgingCollection();
                grr.Query.es.Distinct = true;
                grr.Query.Where(grr.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(),
                                                            AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString(),
                                                            AppEnum.BridgingType.BPJS_PASIEN_UMUM.ToString()));
                if (grr.Query.Load()) ViewState["bpjs"] = grr.Select(g => g.GuarantorID).ToArray();
                else ViewState["bpjs"] = new string[] { string.Empty };

                return (string[])ViewState["bpjs"];
            }
        }

        // Dipindah ke webservice (Handono 230330)
        //public string PlafondProgress(string regNo)
        //{
        //    return Temiang.Avicenna.Module.RADT.Cpoe.EmrList.PlafondProgress(regNo);

        //    //decimal tpatient=0;
        //    //decimal tguarantor=0;
        //    //decimal totalPlafond=0;

        //    //var usedInPercent = PlafondValueUsedInPercent(regNo, ref tguarantor, ref tpatient, ref  totalPlafond);
        //    //if (usedInPercent == 0) return string.Empty;
        //    //return string.Format(@"<div title='G: [{3:N2}] P: [{4:N2}] F: [{5:N2}]' style='background:black;width: 100%; padding: 1px'>
        //    //                <div style='background:{0};color:Black;width: {1}%'>{2:n2}%</div>
        //    //            </div>", usedInPercent > 100 ? "red" : usedInPercent > 75 ? "yellow" : "green", usedInPercent > 100 ? 100 : usedInPercent, usedInPercent, tguarantor,  tpatient,   totalPlafond);

        //}

        //private decimal PlafondValueUsedInPercent(string regNo)
        //{
        //    var reg = Registration(regNo);
        //    var additionalPlafond = AdditionalPlafond(regNo);
        //    //decimal additionalPlafond = 0;

        //    var plafondAmt = (decimal)(reg.PlavonAmount == null ? 0 : reg.PlavonAmount);
        //    if (AppSession.Parameter.GuarantorAskesID.Contains(reg.GuarantorID) && plafondAmt == 0)
        //        plafondAmt = (decimal)(reg.ApproximatePlafondAmount == null ? 0 : reg.ApproximatePlafondAmount);

        //    var totalPlafond = plafondAmt + additionalPlafond;
        //    if (totalPlafond == 0) return 0;

        //    var mergeRegistrationNos = Helper.MergeBilling.GetMergeRegistration(regNo);

        //    var plafonUsedPercent = (TotalGuarantorAndRemainingPatientAmount(reg, mergeRegistrationNos, additionalPlafond) / totalPlafond) * (decimal)100;
        //    return plafonUsedPercent;
        //}

        //private decimal PlafondValueUsedInPercent(string regno, ref decimal tguarantor, ref decimal tpatient, ref decimal totalPlafond)
        //{
        //    var reg = Registration(regno);
        //    if (reg.SRRegistrationType != AppConstant.RegistrationType.InPatient)
        //        return 0;

        //    if (!GuarantorBPJS.Contains(reg.GuarantorID))
        //        return 0;

        //    decimal cobPlafond = AdditionalPlafond(regno);
        //    totalPlafond = TotalPlafond(reg, cobPlafond);
        //    //if (totalPlafond == 0) return 0;
        //    if (totalPlafond == 0)
        //        totalPlafond = 1;

        //    var regnos = Helper.MergeBilling.GetMergeRegistration(regno);

        //    var guarantor = new Guarantor();
        //    guarantor.LoadByPrimaryKey(reg.GuarantorID);

        //    //Helper.CostCalculation.GetBillingTotal2(regnos, reg.SRBussinesMethod, (reg.PlavonAmount ?? 0) + cobPlafond, out tpatient, out tguarantor,
        //    //                                       guarantor, reg.IsGlobalPlafond ?? false);

        //    Helper.CostCalculation.GetBillingTotal2(regnos, reg.SRBussinesMethod, 0, out tpatient, out tguarantor,
        //                                           guarantor, reg.IsGlobalPlafond ?? false);

        //    var totalRemain = tguarantor + tpatient;

        //    var plafonUsedPercent = (totalRemain / totalPlafond) * (decimal)100;
        //    return plafonUsedPercent;

        //}
        //private decimal TotalPlafond(Registration reg, decimal additionalPlafond)
        //{
        //    decimal plafondAmt = reg.PlavonAmount ?? 0;
        //    if (GuarantorBPJS.Contains(reg.GuarantorID) && plafondAmt == 0)
        //        plafondAmt = (decimal)(reg.ApproximatePlafondAmount == null ? 0 : reg.ApproximatePlafondAmount);

        //    return plafondAmt + additionalPlafond;
        //}
        //private Registration Registration(string regNo)
        //{
        //    var reg = new Registration();
        //    reg.LoadByPrimaryKey(regNo);
        //    return reg;
        //}

        //private decimal AdditionalPlafond(string regno)
        //{
        //    decimal cobPlafond = 0;
        //    var cob = new RegistrationGuarantorCollection();
        //    cob.Query.Where(cob.Query.RegistrationNo == regno);
        //    cob.LoadAll();
        //    cobPlafond = cob.Sum(c => (c.PlafondAmount ?? 0));
        //    return cobPlafond;
        //}
        //private decimal TotalGuarantorAndRemainingPatientAmount(Registration reg, string[] mergeRegistrationNos, decimal additionalPlafond)
        //{
        //    decimal tpatient;
        //    decimal tguarantor;
        //    var guarantor = new Guarantor();
        //    guarantor.LoadByPrimaryKey(reg.GuarantorID);

        //    decimal plafondAmt = reg.PlavonAmount ?? 0;
        //    if (AppSession.Parameter.GuarantorAskesID.Contains(reg.GuarantorID) && plafondAmt == 0)
        //        plafondAmt = reg.ApproximatePlafondAmount ?? 0;

        //    //Helper.CostCalculation.GetBillingTotal(mergeRegistrationNos, reg.SRBussinesMethod, plafondAmt + additionalPlafond, out tpatient, out tguarantor,
        //    //                                       guarantor, reg.IsGlobalPlafond ?? false);

        //    //var discGuarantor = (decimal)Helper.Payment.GetPaymentDiscount(mergeRegistrationNos, true);
        //    //var totalAmountGuarantor = tguarantor - discGuarantor;
        //    //var remainingPatientAmt = RemainingPatientAmount(reg, mergeRegistrationNos, tpatient);
        //    //return totalAmountGuarantor + remainingPatientAmt;

        //    Helper.CostCalculation.GetBillingTotal2(mergeRegistrationNos, reg.SRBussinesMethod, plafondAmt + additionalPlafond, out tpatient, out tguarantor,
        //                                           guarantor, reg.IsGlobalPlafond ?? false);
        //    return tpatient + tguarantor;
        //}

        //private decimal RemainingPatientAmount(Registration reg, string[] mergeRegistrationNos, decimal tpatient)
        //{
        //    var discPatient = (decimal)Helper.Payment.GetPaymentDiscount(mergeRegistrationNos, false);

        //    string[] patientParam = new string[2];
        //    patientParam.SetValue(AppSession.Parameter.PaymentTypePayment, 0);
        //    patientParam.SetValue(AppSession.Parameter.PaymentTypePersonalAR, 1);

        //    decimal tpayment = Helper.Payment.GetTotalPayment(mergeRegistrationNos, true, patientParam);
        //    decimal treturn = Helper.Payment.GetTotalPayment(mergeRegistrationNos, false);
        //    var totalPaymentAmountPatient = (decimal)tpayment + (decimal)treturn;

        //    var remainingAmountPatient = tpatient - totalPaymentAmountPatient - discPatient;

        //    decimal selisih = 0;
        //    //selisih kelas untuk bpjs
        //    if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
        //    {
        //        if (AppSession.Parameter.GuarantorAskesID.Contains(reg.GuarantorID))
        //        {
        //            if (reg.CoverageClassID != reg.ChargeClassID)
        //            {
        //                //if ((reg.PlavonAmount2 ?? 0) > 0)
        //                //{
        //                //    var class1 = new Class();
        //                //    class1.LoadByPrimaryKey(reg.CoverageClassID);

        //                //    var asri1 = new AppStandardReferenceItem();
        //                //    asri1.LoadByPrimaryKey(AppEnum.StandardReference.ClassRL.ToString(), class1.SRClassRL);

        //                //    var class2 = new Class();
        //                //    class2.LoadByPrimaryKey(reg.ChargeClassID);

        //                //    var asri2 = new AppStandardReferenceItem();
        //                //    asri2.LoadByPrimaryKey(AppEnum.StandardReference.ClassRL.ToString(), class2.SRClassRL);

        //                //    if (asri2.Note.ToInt() < asri1.Note.ToInt())
        //                //        selisih = (reg.PlavonAmount2 ?? 0) - (reg.PlavonAmount ?? 0);
        //                //}

        //                var cov = new RegistrationCoverageDetail();
        //                cov.Query.Select(cov.Query.CalculatedAmount.Sum());
        //                cov.Query.Where(cov.Query.RegistrationNo == reg.RegistrationNo);
        //                if (cov.Query.Load()) selisih = cov.CalculatedAmount ?? 0;
        //                else
        //                {
        //                    var acov = new RegistrationApproximateCoverageDetail();
        //                    acov.Query.Select(acov.Query.CalculatedAmount.Sum());
        //                    acov.Query.Where(acov.Query.RegistrationNo == reg.RegistrationNo);
        //                    if (acov.Query.Load()) selisih = acov.CalculatedAmount ?? 0;
        //                }
        //            }
        //        }
        //    }

        //    if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
        //    {
        //        if (AppSession.Parameter.GuarantorAskesID.Contains(reg.GuarantorID))
        //            if (reg.CoverageClassID != reg.ChargeClassID)
        //                //if ((reg.PlavonAmount2 ?? 0) > 0)
        //                //    remainingAmountPatient = (((reg.PlavonAmount2 ?? 0) == 0) ? (decimal)tpatient : (decimal)selisih) - totalPaymentAmountPatient - discPatient;
        //                remainingAmountPatient = ((decimal)selisih > (decimal)tpatient ? ((decimal)tpatient == 0 ? (decimal)selisih : (decimal)tpatient) : (decimal)selisih) - totalPaymentAmountPatient - discPatient;
        //    }
        //    return remainingAmountPatient;
        //}
        #endregion

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        protected void cboGuarantorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new GuarantorQuery();
            query.es.Top = 30;
            query.Where
                (
                    query.GuarantorName.Like(searchTextContain),
                    query.SRGuarantorType != AppSession.Parameter.GuarantorTypeMemberID,
                    query.IsActive == true
                );
            query.OrderBy(query.GuarantorName.Ascending);

            cboGuarantorID.DataSource = query.LoadDataTable();
            cboGuarantorID.DataBind();
        }

        private StringBuilder StrbResponseScripts { get; } = new StringBuilder();
        protected string UpdateStatScript(string statType, object regNo, object fregNo, object regType, object patId, object dob, object parId)
        {
            if (statType == "plafond" && !AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsEmrListShowPlafondProgress))
                return String.Empty;

            var script = string.Format("UpdateState(\"{6}\",\"{6}{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{7}\");", regNo.ToString().Replace("/", "_"),
                regNo, fregNo, regType, patId, dob, statType, parId);

            // Tampung script untuk diregistrasi pada AjaxManager.ResponseScripts karena AJAX defaultnya tidak akan menjalankan script yg ada di page
            StrbResponseScripts.AppendLine(script);
            return script;
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            if (IsPostBack)
                AjaxManager.ResponseScripts.Add(StrbResponseScripts.ToString());
        }
    }
}
