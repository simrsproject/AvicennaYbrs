using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;
using System.Data;
using System.Drawing;   
using Temiang.Dal.DynamicQuery;
using DevExpress.PivotGrid.OLAP.AdoWrappers;
using DevExpress.Pdf.Native.BouncyCastle.Ocsp;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class CasemixList : BasePage
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

            ProgramID = AppConstant.Program.CasemixApproval;

            if (!IsPostBack)
            {
                txtTransactionDate.SelectedDate = DateTime.Now;

                //service unit
                var unit = new ServiceUnitCollection();

                var query = new ServiceUnitQuery("a");

                query.Where(
                    query.SRRegistrationType != string.Empty,
                    query.IsActive == true
                    );

                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                //paramedic
                var param = new ParamedicCollection();
                param.Query.Where
                    (
                        param.Query.IsActive == true,
                        param.Query.IsAvailable == true
                    );
                param.Query.OrderBy(param.Query.ParamedicName.Ascending);
                param.LoadAll();

                cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Paramedic entity in param)
                {
                    cboParamedicID.Items.Add(new RadComboBoxItem(entity.ParamedicName, entity.ParamedicID));
                }

                var guar = new GuarantorCollection();
                guar.Query.Where(
                    guar.Query.SRGuarantorType != AppSession.Parameter.GuarantorTypeMemberID,
                    guar.Query.IsActive == true
                    );
                guar.Query.OrderBy(guar.Query.GuarantorName.Ascending);
                guar.LoadAll();

                cboGuarantorID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach(Guarantor entity in guar)
                {
                    cboGuarantorID.Items.Add(new RadComboBoxItem(entity.GuarantorName, entity.GuarantorID));
                }

                StandardReference.InitializeIncludeSpace(cboRegistrationType, AppEnum.StandardReference.RegistrationType);
                grdList2.MasterTableView.GetColumn("BpjsSepNo").Visible = IsNewDisplayCasemixCenterSetting;
                grdList2.MasterTableView.GetColumn("document").Visible = IsNewDisplayCasemixCenterSetting;
                grdList2.MasterTableView.GetColumn("print").Visible = IsNewDisplayCasemixCenterSetting;
                grdList2.MasterTableView.GetColumn("RoomName").Visible = !IsNewDisplayCasemixCenterSetting;
                grdList2.MasterTableView.GetColumn("BedNo").Visible = !IsNewDisplayCasemixCenterSetting;
                grdList2.MasterTableView.GetColumn("PathwayName").Visible = !IsNewDisplayCasemixCenterSetting;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string tab = Request.QueryString["tab"];
                if (tab == "history")
                {
                    RadTabStrip1.Tabs[1].Selected = true;
                    RadMultiPage1.PageViews[1].Selected = true;
                }
                else // default outstanding
                {
                    RadTabStrip1.Tabs[0].Selected = true;
                    RadMultiPage1.PageViews[0].Selected = true;
                }                
            }
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }

        public bool IsNewDisplayCasemixCenterSetting
        {
            get
            {
                return AppParameter.GetParameterValue(AppParameter.ParameterItem.IsNewDisplayCasemixCenter) == "Yes";
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
            grdList.DataSource = TransCharges(false);
            //grdList.DataSource = new String[] { };
        }

        protected void grdList2_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit && !IsNewDisplayCasemixCenterSetting)
            {
                grd.DataSource = new String[] { };
                return;
            }

            if (IsNewDisplayCasemixCenterSetting)
            {
                // pastikan kontrol isi ulang filter dari Session
                RestoreStateFromSession();

                if (ViewState["IsFilterClicked"] != null && (bool)ViewState["IsFilterClicked"])
                {
                    grd.CurrentPageIndex = 0;
                    grd.MasterTableView.CurrentPageIndex = 0;
                    ViewState["IsFilterClicked"] = false; // reset flag
                }
            }
            string tab = Request.QueryString["tab"];
            if (!IsPostBack && !IsListLoadRecordOnInit && IsNewDisplayCasemixCenterSetting && (tab == null)) 
            {               
                grd.DataSource = new String[] { };
                return;               
            }

            var dataSource = TransCharges(true);
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                grd.DataSource = dataSource;
            }

            //grdList2.DataSource = TransCharges(true);
        }

        //private DataTable TransCharges(bool isHistory)
        //{
        //    DataTable dtbl;

        //    if (isHistory)
        //    {
        //        var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(cboParamedicID.SelectedValue) &&
        //            string.IsNullOrEmpty(txtPatientName.Text) && txtFromDate.IsEmpty && txtDischareFromDate.IsEmpty && txtDischareToDate.IsEmpty && txtToDate.IsEmpty && string.IsNullOrEmpty(cboRegistrationType.SelectedValue) && string.IsNullOrEmpty(cboGuarantorID.SelectedValue) &&
        //            string.IsNullOrEmpty(txtNoSep.Text) && string.IsNullOrEmpty(cboGuarantorID.SelectedValue);
        //        if (!ValidateSearch(isEmptyFilter, "Registration")) return null;

        //        //--select registration active
        //        var regQ = new RegistrationQuery("reg");
        //        regQ.es.WithNoLock = true;

        //        regQ.Select(regQ.RegistrationNo);
        //        regQ.Where(
        //            regQ.SRRegistrationType.In(AppSession.Parameter.CasemixValidationRegistrationType),
        //            regQ.IsVoid == false,
        //            regQ.IsNonPatient == false);

        //        var patQ = new PatientQuery("pat");
        //        patQ.es.WithNoLock = true;
        //        regQ.InnerJoin(patQ).On(patQ.PatientID == regQ.PatientID);

        //        var cob = new RegistrationGuarantorQuery("cob");
        //        cob.es.WithNoLock = true;

        //        regQ.LeftJoin(cob).On(cob.RegistrationNo == regQ.RegistrationNo);
        //        regQ.Where(regQ.Or(regQ.GuarantorID.In(Helper.GuarantorBpjsCasemix), cob.GuarantorID.In(Helper.GuarantorBpjsCasemix)));

        //        if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(cboParamedicID.SelectedValue) &&
        //            string.IsNullOrEmpty(txtPatientName.Text) && txtFromDate.IsEmpty && txtToDate.IsEmpty && txtDischareFromDate.IsEmpty && txtDischareToDate.IsEmpty && string.IsNullOrEmpty(cboRegistrationType.SelectedValue) && string.IsNullOrEmpty(cboGuarantorID.SelectedValue) &&
        //            string.IsNullOrEmpty(txtNoSep.Text) && string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
        //        {
        //            regQ.Where(regQ.IsClosed == false);
        //        }
        //        if (!txtFromDate.IsEmpty && !txtToDate.IsEmpty)
        //            regQ.Where(regQ.RegistrationDate >= txtFromDate.SelectedDate, regQ.RegistrationDate < txtToDate.SelectedDate.Value.AddDays(1));
        //        if (!txtDischareFromDate.IsEmpty && !txtDischareToDate.IsEmpty)
        //            regQ.Where(regQ.DischargeDate >= txtDischareFromDate.SelectedDate, regQ.DischargeDate < txtDischareToDate.SelectedDate.Value.AddDays(1));
        //        if (cboRegistrationType.SelectedValue != string.Empty)
        //            regQ.Where(regQ.SRRegistrationType == cboRegistrationType.SelectedValue);
        //        if (cboServiceUnitID.SelectedValue != string.Empty)
        //            regQ.Where(regQ.ServiceUnitID == cboServiceUnitID.SelectedValue);
        //        if (cboParamedicID.SelectedValue != string.Empty)
        //            regQ.Where(regQ.ParamedicID == cboParamedicID.SelectedValue);
        //        if (txtRegistrationNo.Text != string.Empty)
        //        {
        //            string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
        //            Helper.AddFilterMedNoOrRegNoOrPatName(regQ, patQ, searchReg, "registration");
        //        }
        //        if (txtPatientName.Text != string.Empty)
        //        {
        //            string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
        //            regQ.Where(string.Format("<LTRIM(RTRIM(LTRIM(pat.FirstName + ' ' + pat.MiddleName)) + ' ' + pat.LastName) LIKE '{0}'>", searchPatient));
        //        }
        //        if (cboGuarantorID.SelectedValue != string.Empty)
        //            regQ.Where(regQ.GuarantorID == cboGuarantorID.SelectedValue);
        //        if (!string.IsNullOrEmpty(txtNoSep.Text))
        //            regQ.Where($"<reg.BpjsSepNo LIKE '%{txtNoSep.Text}%'>");

        //        regQ.es.Top = AppSession.Parameter.MaxResultRecord;

        //        //--display
        //        var unit = new ServiceUnitQuery("b");
        //        unit.es.WithNoLock = true;

        //        var room = new ServiceRoomQuery("c");
        //        room.es.WithNoLock = true;

        //        var medic = new ParamedicQuery("d");
        //        medic.es.WithNoLock = true;

        //        var query = new RegistrationQuery("e");
        //        query.es.WithNoLock = true;

        //        var patient = new PatientQuery("f");
        //        patient.es.WithNoLock = true;

        //        var grr = new GuarantorQuery("g");
        //        grr.es.WithNoLock = true;

        //        var sal = new AppStandardReferenceItemQuery("sal");
        //        sal.es.WithNoLock = true;

        //        var ed = new EpisodeDiagnoseQuery("j");
        //        ed.es.WithNoLock = true;

        //        var diag = new DiagnoseQuery("i");
        //        diag.es.WithNoLock = true;

        //        var regpath = new RegistrationPathwayQuery("rp");
        //        regpath.es.WithNoLock = true;

        //        var path = new PathwayQuery("pt");
        //        path.es.WithNoLock = true;

        //        query.Select
        //        (
        //            room.RoomName,
        //            query.RegistrationDate,
        //            unit.ServiceUnitID,
        //            query.ParamedicID,
        //            medic.ParamedicName,
        //            query.RegistrationNo,
        //            query.BpjsSepNo,
        //            patient.MedicalNo,
        //            patient.PatientName,
        //            patient.Sex,
        //            grr.GuarantorName,
        //            query.BedID,
        //            query.ChargeClassID,
        //            query.CoverageClassID,
        //            query.ClassID,
        //            sal.ItemName.As("SalutationName"),
        //            diag.DiagnoseID.Coalesce("''"),
        //            @"<ISNULL(i.DiagnoseName, e.InitialDiagnose) AS DiagnoseName>",
        //            path.PathwayID.Coalesce("''"),
        //            path.PathwayName.Coalesce("''"),
        //            @"<DATEDIFF(DAY, e.RegistrationDate, ISNULL(e.DischargeDate, GETDATE())) + 1 AS LoS>"
        //        );

        //        query.LeftJoin(room).On(query.RoomID == room.RoomID);
        //        query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
        //        query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
        //        query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
        //        query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
        //        query.LeftJoin(sal).On(sal.StandardReferenceID == AppEnum.StandardReference.Salutation.ToString() && patient.SRSalutation == sal.ItemID);
        //        query.LeftJoin(ed).On(query.RegistrationNo == ed.RegistrationNo && ed.SRDiagnoseType == AppSession.Parameter.DiagnoseTypeMain && ed.IsVoid == false);
        //        query.LeftJoin(diag).On(ed.DiagnoseID == diag.DiagnoseID);
        //        query.LeftJoin(regpath).On(query.RegistrationNo == regpath.RegistrationNo);
        //        query.LeftJoin(path).On(regpath.PathwayID == path.PathwayID && regpath.PathwayStatus != string.Empty);

        //        var group = new esQueryItem(query, "Group", esSystemType.String);
        //        group = unit.ServiceUnitName;

        //        query.Select(group.As("Group"));
        //        query.Where
        //        (
        //            query.RegistrationNo.In(regQ)
        //        );

        //        query.OrderBy(query.RegistrationDate.Descending, query.RegistrationNo.Descending);

        //        dtbl = query.LoadDataTable();
        //    }
        //    else
        //    {
        //        //--trans charges
        //        var tcq = new TransChargesQuery("a");
        //        tcq.es.WithNoLock = true;

        //        var tciq = new TransChargesItemQuery("b");
        //        tciq.es.WithNoLock = true;

        //        tcq.InnerJoin(tciq).On(tcq.TransactionNo == tciq.TransactionNo);

        //        tcq.es.Distinct = true;
        //        tcq.Select(tcq.RegistrationNo);

        //        tcq.Where(
        //            tcq.TransactionDate >= txtTransactionDate.SelectedDate, tcq.TransactionDate < txtTransactionDate.SelectedDate.Value.AddDays(1),
        //            tcq.Or(
        //                tcq.PackageReferenceNo == string.Empty,
        //                tcq.PackageReferenceNo.IsNull()
        //                ),
        //            tcq.IsVoid == false,
        //            tcq.Or(tciq.IsVoid == false, tciq.CasemixApprovedByUserID.IsNotNull()),
        //            tcq.Or(tciq.ParentNo == string.Empty, tciq.ParentNo.IsNull(),
        //            tcq.Or(tciq.IsCasemixApproved.IsNull(), tciq.IsCasemixApproved == false),
        //            tciq.CasemixApprovedByUserID.IsNull(),
        //            tciq.CasemixApprovedDateTime.IsNull()
        //            )
        //        );
        //        DataTable tcx = tcq.LoadDataTable();

        //        //--trans prescription
        //        var tpq = new TransPrescriptionQuery("a");
        //        tpq.es.WithNoLock = true;

        //        var tpiq = new TransPrescriptionItemQuery("b");
        //        tpiq.es.WithNoLock = true;

        //        tpq.InnerJoin(tpiq).On(tpq.PrescriptionNo == tpiq.PrescriptionNo);

        //        tpq.es.Distinct = true;
        //        tpq.Select(tpq.RegistrationNo);

        //        tpq.Where(
        //            tpq.PrescriptionDate >= txtTransactionDate.SelectedDate, tpq.PrescriptionDate < txtTransactionDate.SelectedDate.Value.AddDays(1),
        //            tpq.IsVoid == false,
        //            tpq.Or(tpiq.IsVoid == false, tpiq.CasemixApprovedByUserID.IsNotNull()),
        //            tpq.Or(tpiq.IsCasemixApproved.IsNull(), tpiq.IsCasemixApproved == false),
        //            tpiq.CasemixApprovedByUserID.IsNull(), 
        //            tpiq.CasemixApprovedDateTime.IsNull()
        //        );
        //        DataTable tpx = tpq.LoadDataTable();

        //        //--blood bank
        //        var bbq = new BloodBankTransactionQuery("a");
        //        bbq.es.WithNoLock = true;

        //        bbq.es.Distinct = true;
        //        bbq.Select(bbq.RegistrationNo);

        //        bbq.Where(bbq.TransactionDate >= txtTransactionDate.SelectedDate, bbq.TransactionDate < txtTransactionDate.SelectedDate.Value.AddDays(1), bbq.IsApproved == true);
        //        bbq.Where(bbq.IsValidatedByCasemix.IsNotNull(), bbq.IsValidatedByCasemix == false);
        //        DataTable bbx = bbq.LoadDataTable();

        //        //--display
        //        var unit = new ServiceUnitQuery("b");
        //        unit.es.WithNoLock = true;

        //        var room = new ServiceRoomQuery("c");
        //        room.es.WithNoLock = true;

        //        var medic = new ParamedicQuery("d");
        //        medic.es.WithNoLock = true;

        //        var query = new RegistrationQuery("e");
        //        query.es.WithNoLock = true;

        //        var patient = new PatientQuery("f");
        //        patient.es.WithNoLock = true;

        //        var grr = new GuarantorQuery("g");
        //        grr.es.WithNoLock = true;

        //        var sal = new AppStandardReferenceItemQuery("sal");
        //        sal.es.WithNoLock = true;

        //        var ed = new EpisodeDiagnoseQuery("j");
        //        ed.es.WithNoLock = true;

        //        var diag = new DiagnoseQuery("i");
        //        diag.es.WithNoLock = true;

        //        var regpath = new RegistrationPathwayQuery("rp");
        //        regpath.es.WithNoLock = true;

        //        var path = new PathwayQuery("pt");
        //        path.es.WithNoLock = true;

        //        query.Select
        //        (
        //            room.RoomName,
        //            query.RegistrationDate,
        //            unit.ServiceUnitID,
        //            query.ParamedicID,
        //            medic.ParamedicName,
        //            query.RegistrationNo,
        //            query.BpjsSepNo,
        //            patient.MedicalNo,
        //            patient.PatientName,
        //            patient.Sex,
        //            grr.GuarantorName,
        //            query.BedID,
        //            query.ChargeClassID,
        //            query.CoverageClassID,
        //            query.ClassID,
        //            sal.ItemName.As("SalutationName"),
        //            diag.DiagnoseID.Coalesce("''"),
        //            @"<ISNULL(i.DiagnoseName, e.InitialDiagnose) AS DiagnoseName>",
        //            path.PathwayID.Coalesce("''"),
        //            path.PathwayName.Coalesce("''"),
        //            @"<DATEDIFF(DAY, e.RegistrationDate, ISNULL(e.DischargeDate, GETDATE())) + 1 AS LoS>"
        //        );

        //        query.LeftJoin(room).On(query.RoomID == room.RoomID);
        //        query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
        //        query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
        //        query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
        //        query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
        //        query.LeftJoin(sal).On(sal.StandardReferenceID == AppEnum.StandardReference.Salutation.ToString() && patient.SRSalutation == sal.ItemID);
        //        query.LeftJoin(ed).On(query.RegistrationNo == ed.RegistrationNo && ed.SRDiagnoseType == AppSession.Parameter.DiagnoseTypeMain && ed.IsVoid == false);
        //        query.LeftJoin(diag).On(ed.DiagnoseID == diag.DiagnoseID);
        //        query.LeftJoin(regpath).On(query.RegistrationNo == regpath.RegistrationNo);
        //        query.LeftJoin(path).On(regpath.PathwayID == path.PathwayID && regpath.PathwayStatus != string.Empty);

        //        var group = new esQueryItem(query, "Group", esSystemType.String);
        //        group = unit.ServiceUnitName;

        //        query.Select(group.As("Group"));

        //        var xx = new List<Temiang.Dal.DynamicQuery.esComparison>();
        //        if (tcx.Rows.Count > 0)
        //            xx.Add(query.RegistrationNo.In(tcq));
        //        if (tpx.Rows.Count > 0)
        //            xx.Add(query.RegistrationNo.In(tpq));
        //        if (bbx.Rows.Count > 0)
        //            xx.Add(query.RegistrationNo.In(bbq));

        //        if (xx.Count > 0)
        //            query.Where(query.Or(xx.ToArray()));
        //        else
        //            query.Where(query.RegistrationNo == "XXX");

        //        query.Where(
        //            query.SRRegistrationType.In(AppSession.Parameter.CasemixValidationRegistrationType),
        //            query.GuarantorID.In(Helper.GuarantorBpjsCasemix),
        //            query.IsVoid == false,
        //            query.IsClosed == false,
        //            query.IsNonPatient == false);

        //        query.OrderBy(query.RegistrationDate.Descending, query.RegistrationNo.Descending);

        //        query.es.Top = AppSession.Parameter.MaxResultRecord;
        //        dtbl = query.LoadDataTable();
        //    }
        //    return dtbl;
        //}


        private DataTable TransCharges(bool isHistory)
        {
            if (isHistory)
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(cboParamedicID.SelectedValue) &&
                    string.IsNullOrEmpty(txtPatientName.Text) && txtFromDate.IsEmpty && txtDischareFromDate.IsEmpty && txtDischareToDate.IsEmpty && txtToDate.IsEmpty && string.IsNullOrEmpty(cboRegistrationType.SelectedValue) && string.IsNullOrEmpty(cboGuarantorID.SelectedValue) &&
                    string.IsNullOrEmpty(txtNoSep.Text) && string.IsNullOrEmpty(cboGuarantorID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Registration")) return null;
            }
            else
            {
                if (txtTransactionDate.IsEmpty)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('{0} required.');", "Transaction Date"), true);

                    return null;
                }
            }

            //--select registration active
            var regQ = new RegistrationQuery("reg");
            regQ.es.WithNoLock = true;

            regQ.Select(regQ.RegistrationNo);
            regQ.Where(
                regQ.SRRegistrationType.In(AppSession.Parameter.CasemixValidationRegistrationType),
                regQ.IsVoid == false,
                regQ.IsNonPatient == false);

            if (isHistory)
            {
                var patQ = new PatientQuery("pat");
                patQ.es.WithNoLock = true;
                regQ.InnerJoin(patQ).On(patQ.PatientID == regQ.PatientID);

                var cob = new RegistrationGuarantorQuery("cob");
                cob.es.WithNoLock = true;

                regQ.LeftJoin(cob).On(cob.RegistrationNo == regQ.RegistrationNo);
                regQ.Where(regQ.Or(regQ.GuarantorID.In(Helper.GuarantorBpjsCasemix), cob.GuarantorID.In(Helper.GuarantorBpjsCasemix)));

                if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(cboParamedicID.SelectedValue) &&
                    string.IsNullOrEmpty(txtPatientName.Text) && txtFromDate.IsEmpty && txtToDate.IsEmpty && txtDischareFromDate.IsEmpty && txtDischareToDate.IsEmpty && string.IsNullOrEmpty(cboRegistrationType.SelectedValue) && string.IsNullOrEmpty(cboGuarantorID.SelectedValue) &&
                    string.IsNullOrEmpty(txtNoSep.Text) && string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
                {
                    regQ.Where(regQ.IsClosed == false, 
                        regQ.Or(
                            regQ.And(regQ.SRRegistrationType == AppConstant.RegistrationType.InPatient, regQ.DischargeDate.IsNull()), 
                            regQ.And(regQ.SRRegistrationType != AppConstant.RegistrationType.InPatient, regQ.RegistrationDate >= DateTime.Now.Date.AddDays(-1))
                            )
                        );
                }
                if (!txtFromDate.IsEmpty && !txtToDate.IsEmpty)
                    regQ.Where(regQ.RegistrationDate >= txtFromDate.SelectedDate, regQ.RegistrationDate < txtToDate.SelectedDate.Value.AddDays(1));
                if (!txtDischareFromDate.IsEmpty && !txtDischareToDate.IsEmpty)
                    regQ.Where(regQ.DischargeDate >= txtDischareFromDate.SelectedDate, regQ.DischargeDate < txtDischareToDate.SelectedDate.Value.AddDays(1));
                if (cboRegistrationType.SelectedValue != string.Empty)
                    regQ.Where(regQ.SRRegistrationType == cboRegistrationType.SelectedValue);
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    regQ.Where(regQ.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    regQ.Where(regQ.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    Helper.AddFilterMedNoOrRegNoOrPatName(regQ, patQ, searchReg, "registration");
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    regQ.Where(patQ.FullName.Like(searchPatient));
                    //regQ.Where(string.Format("<LTRIM(RTRIM(LTRIM(pat.FirstName + ' ' + pat.MiddleName)) + ' ' + pat.LastName) LIKE '{0}'>", searchPatient));
                }
                if (cboGuarantorID.SelectedValue != string.Empty)
                    regQ.Where(regQ.GuarantorID == cboGuarantorID.SelectedValue);
                if (!string.IsNullOrEmpty(txtNoSep.Text))
                    regQ.Where($"<reg.BpjsSepNo LIKE '%{txtNoSep.Text}%'>");

                regQ.es.Top = AppSession.Parameter.MaxResultRecord;
            }
            else
            {
                regQ.Where(regQ.GuarantorID.In(Helper.GuarantorBpjsCasemix), regQ.IsClosed == false,
                    regQ.Or(
                        regQ.And(regQ.SRRegistrationType == AppConstant.RegistrationType.InPatient, regQ.DischargeDate.IsNull()), 
                        regQ.And(regQ.SRRegistrationType != AppConstant.RegistrationType.InPatient, regQ.RegistrationDate >= DateTime.Now.Date.AddDays(-1))
                        )
                    );
            }

            //--display
            var unit = new ServiceUnitQuery("b");
            unit.es.WithNoLock = true;

            var room = new ServiceRoomQuery("c");
            room.es.WithNoLock = true;

            var medic = new ParamedicQuery("d");
            medic.es.WithNoLock = true;

            var query = new RegistrationQuery("e");
            query.es.WithNoLock = true;

            var patient = new PatientQuery("f");
            patient.es.WithNoLock = true;

            var grr = new GuarantorQuery("g");
            grr.es.WithNoLock = true;

            var sal = new AppStandardReferenceItemQuery("sal");
            sal.es.WithNoLock = true;

            var ed = new EpisodeDiagnoseQuery("j");
            ed.es.WithNoLock = true;

            var diag = new DiagnoseQuery("i");
            diag.es.WithNoLock = true;

            var regpath = new RegistrationPathwayQuery("rp");
            regpath.es.WithNoLock = true;

            var path = new PathwayQuery("pt");
            path.es.WithNoLock = true;

            query.Select
            (
                room.RoomName,
                query.RegistrationDate,
                unit.ServiceUnitID,
                query.ParamedicID,
                medic.ParamedicName,
                query.RegistrationNo,
                query.BpjsSepNo,
                patient.MedicalNo,
                patient.PatientName,
                patient.Sex,
                grr.GuarantorName,
                query.BedID,
                query.ChargeClassID,
                query.CoverageClassID,
                query.ClassID,
                sal.ItemName.As("SalutationName"),
                diag.DiagnoseID.Coalesce("''"),
                @"<ISNULL(i.DiagnoseName, e.InitialDiagnose) AS DiagnoseName>",
                path.PathwayID.Coalesce("''"),
                path.PathwayName.Coalesce("''"),
                @"<DATEDIFF(DAY, e.RegistrationDate, ISNULL(e.DischargeDate, GETDATE())) + 1 AS LoS>"
            );

            query.LeftJoin(room).On(query.RoomID == room.RoomID);
            query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
            query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
            query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
            query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
            query.LeftJoin(sal).On(sal.StandardReferenceID == AppEnum.StandardReference.Salutation.ToString() && patient.SRSalutation == sal.ItemID);
            query.LeftJoin(ed).On(query.RegistrationNo == ed.RegistrationNo && ed.SRDiagnoseType == AppSession.Parameter.DiagnoseTypeMain && ed.IsVoid == false);
            query.LeftJoin(diag).On(ed.DiagnoseID == diag.DiagnoseID);
            query.LeftJoin(regpath).On(query.RegistrationNo == regpath.RegistrationNo);
            query.LeftJoin(path).On(regpath.PathwayID == path.PathwayID && regpath.PathwayStatus != string.Empty);

            var group = new esQueryItem(query, "Group", esSystemType.String);
            group = unit.ServiceUnitName;

            query.Select(group.As("Group"));
            query.Where
            (
                query.RegistrationNo.In(regQ)
            );

            if (isHistory && IsNewDisplayCasemixCenterSetting)
            {
                query.OrderBy(query.BpjsSepNo.Descending,query.RegistrationDate.Descending, query.RegistrationNo.Descending);
            }
            else
            {
                query.OrderBy(query.RegistrationDate.Descending, query.RegistrationNo.Descending);
            }
            
            DataTable dtbl;
            if (isHistory)
            {
                dtbl = query.LoadDataTable();

                return dtbl;
            }
            else
            {
                //--trans charges
                var tcq = new TransChargesQuery("a");
                tcq.es.WithNoLock = true;

                var tciq = new TransChargesItemQuery("b");
                tciq.es.WithNoLock = true;

                tcq.InnerJoin(tciq).On(tcq.TransactionNo == tciq.TransactionNo);

                tcq.es.Distinct = true;
                tcq.Select(tcq.RegistrationNo);

                tcq.Where(tcq.RegistrationNo.In(regQ),
                    tcq.TransactionDate >= txtTransactionDate.SelectedDate, tcq.TransactionDate < txtTransactionDate.SelectedDate.Value.AddDays(1),
                      tcq.Or(
                         tcq.PackageReferenceNo == string.Empty,
                         tcq.PackageReferenceNo.IsNull()
                         ),
                     tcq.IsVoid == false,
                     tcq.Or(tciq.IsVoid == false, tciq.CasemixApprovedByUserID.IsNotNull()),
                     tcq.Or(tciq.ParentNo == string.Empty, tciq.ParentNo.IsNull()
                     )
                 );

                //tcq.Where("<ISNULL(b.IsCasemixApproved, CAST(0 AS BIT)) = 0>");
                tcq.Where(tcq.Or(tciq.IsCasemixApproved.IsNull(), tciq.IsCasemixApproved == false));
                tcq.Where(tciq.CasemixApprovedByUserID.IsNull(), tciq.CasemixApprovedDateTime.IsNull());

                DataTable tcx = tcq.LoadDataTable();

                //--trans prescription
                var tpq = new TransPrescriptionQuery("a");
                tpq.es.WithNoLock = true;

                var tpiq = new TransPrescriptionItemQuery("b");
                tpiq.es.WithNoLock = true;

                tpq.InnerJoin(tpiq).On(tpq.PrescriptionNo == tpiq.PrescriptionNo);

                tpq.es.Distinct = true;
                tpq.Select(tpq.RegistrationNo);

                tpq.Where(tpq.RegistrationNo.In(regQ),
                    tpq.PrescriptionDate >= txtTransactionDate.SelectedDate, tpq.PrescriptionDate < txtTransactionDate.SelectedDate.Value.AddDays(1),
                    tpq.IsVoid == false,
                    tpq.Or(tpiq.IsVoid == false, tpiq.CasemixApprovedByUserID.IsNotNull())
                );
                //tpq.Where("<ISNULL(b.IsCasemixApproved, CAST(0 AS BIT)) = 0>");
                tpq.Where(tpq.Or(tpiq.IsCasemixApproved.IsNull(), tpiq.IsCasemixApproved == false));
                tpq.Where(tpiq.CasemixApprovedByUserID.IsNull(), tpiq.CasemixApprovedDateTime.IsNull());

                DataTable tpx = tpq.LoadDataTable();

                //--blood bank
                var bbq = new BloodBankTransactionQuery("a");
                bbq.es.WithNoLock = true;

                bbq.es.Distinct = true;
                bbq.Select(bbq.RegistrationNo);

                bbq.Where(
                    bbq.RegistrationNo.In(regQ),
                    bbq.TransactionDate >= txtTransactionDate.SelectedDate, bbq.TransactionDate < txtTransactionDate.SelectedDate.Value.AddDays(1),
                    bbq.IsApproved == true);
                //bbq.Where("<ISNULL(a.IsValidatedByCasemix, 1) = 0>");
                bbq.Where(bbq.IsValidatedByCasemix.IsNotNull(), bbq.IsValidatedByCasemix == false);

                DataTable bbx = bbq.LoadDataTable();

                //--select registration yg butuh validasi casemix
                var xx = new List<Temiang.Dal.DynamicQuery.esComparison>();
                if (tcx.Rows.Count > 0)
                    xx.Add(query.RegistrationNo.In(tcq));
                if (tpx.Rows.Count > 0)
                    xx.Add(query.RegistrationNo.In(tpq));
                if (bbx.Rows.Count > 0)
                    xx.Add(query.RegistrationNo.In(bbq));

                if (xx.Count > 0)
                {
                    query.Where(query.Or(xx.ToArray()));

                    query.es.Top = AppSession.Parameter.MaxResultRecord;

                    dtbl = query.LoadDataTable();

                    return dtbl;
                }
                else return null;
            }
        }

        protected void btnFilterOutstanding_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();
            grdList.Rebind();
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (IsNewDisplayCasemixCenterSetting)
            {
                SaveStateToSession();
                ViewState["IsFilterClicked"] = true;
            }
            else
                SaveValueToCookie();            
            grdList2.Rebind();
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var tooltip = string.Empty;
                var dataItem = e.Item as GridDataItem;
                if (dataItem.OwnerTableView.Name == "master")
                {
                    if (dataItem["ChargeClassID"].Text != dataItem["CoverageClassID"].Text)
                    {
                        // Beri warna merah jika CoverageClassID berbeda dg ChargeClassID
                        dataItem.ForeColor = Color.Red;
                        dataItem.Font.Bold = true;
                        tooltip = "Charge class is different from coverage class.";
                    }
                    if (dataItem["ChargeClassID"].Text != dataItem["ClassID"].Text)
                    {
                        var c = new Class();
                        c.LoadByPrimaryKey(dataItem["ClassID"].Text);
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

        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            grdList.Rebind();
        }

        private void SaveStateToSession()
        {
            Session["FilterFromDate"] = txtFromDate.SelectedDate;
            Session["FilterToDate"] = txtToDate.SelectedDate;
            Session["FilterDischareFromDate"] = txtDischareFromDate.SelectedDate;
            Session["FilterDischareToDate"] = txtDischareToDate.SelectedDate;
            Session["FilterRegistrationType"] = cboRegistrationType.SelectedValue;
            Session["FilterServiceUnit"] = cboServiceUnitID.SelectedValue;
            Session["FilterRegistrationNo"] = txtRegistrationNo.Text;
            Session["FilterParamedicID"] = cboParamedicID.SelectedValue;
            Session["FilterGuarantorID"] = cboGuarantorID.SelectedValue;
            Session["FilterNoSep"] = txtNoSep.Text;
            Session["FilterPatientName"] = txtPatientName.Text;
            Session["FilterPageIndex"] = grdList2.CurrentPageIndex;
        }

        private void RestoreStateFromSession()
        {
            if (Session["FilterFromDate"] != null)
                txtFromDate.SelectedDate = Session["FilterFromDate"] as DateTime?;
            if (Session["FilterToDate"] != null)
                txtToDate.SelectedDate = Session["FilterToDate"] as DateTime?;
            if (Session["FilterDischareFromDate"] != null)
                txtDischareFromDate.SelectedDate = Session["FilterDischareFromDate"] as DateTime?;
            if (Session["FilterDischareToDate"] != null)
                txtDischareToDate.SelectedDate = Session["FilterDischareToDate"] as DateTime?;
            if (Session["FilterRegistrationType"] != null)
                cboRegistrationType.SelectedValue = Session["FilterRegistrationType"].ToString();
            if (Session["FilterServiceUnit"] != null)
                cboServiceUnitID.SelectedValue = Session["FilterServiceUnit"].ToString();
            if (Session["FilterRegistrationNo"] != null)
                txtRegistrationNo.Text = Session["FilterRegistrationNo"].ToString();
            if (Session["FilterParamedicID"] != null)
                cboParamedicID.SelectedValue = Session["FilterParamedicID"].ToString();
            if (Session["FilterGuarantorID"] != null)
                cboGuarantorID.SelectedValue = Session["FilterGuarantorID"].ToString();
            if (Session["FilterNoSep"] != null)
                txtNoSep.Text = Session["FilterNoSep"].ToString();
            if (Session["FilterPatientName"] != null)
                txtPatientName.Text = Session["FilterPatientName"].ToString();
            if (Session["FilterPageIndex"] != null)
                grdList2.CurrentPageIndex = (int)Session["FilterPageIndex"];
        }

        protected void grdList2_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            grdList2.CurrentPageIndex = e.NewPageIndex;
            SaveStateToSession();
        }
    }
}
