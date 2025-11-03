using System;
using System.Linq;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using System.Web.UI;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee.V2
{
    public partial class ParamedicFeeVerificationByDischargeDateDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private void PopulateNewVerificationNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New) return;

            _autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.FeeVerificationNo);
            txtVerificationNo.Text = _autoNumber.LastCompleteNumber;
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ParamedicFeeVerificationByPaymentSearch.aspx";

            if (Request.QueryString["type"] == "2")
            {
                ProgramID = AppConstant.Program.ParamedicFeeVerificationPerRegNo;
                UrlPageList = "../ParamedicFeeVerificationByPaymentPerRegistration/ParamedicFeeVerificationByPaymentPerRegistrationList.aspx";
            }
            else if (Request.QueryString["type"] == "3")
            {
                ProgramID = AppConstant.Program.ParamedicFeeVerificationPerFilter;
                UrlPageList = "../ParamedicFeeVerificationByPaymentPerFilter/ParamedicFeeVerificationByPaymentPerFilterList.aspx";
            }
            else if (Request.QueryString["type"] == "4")
            {
                ProgramID = AppConstant.Program.ParamedicFeeVerification;
                UrlPageList = "../ParamedicFeeVerificationByDischargeDateV2/ParamedicFeeVerificationByDischargeDateList.aspx";
            }
            else
            {
                ProgramID = AppConstant.Program.ParamedicFeeVerification;
                UrlPageList = "ParamedicFeeVerificationByPaymentList.aspx";
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdFeeCalculation, txtVerificationAmount);
            ajax.AddAjaxSetting(grdFeeCalculation, txtVerificationTaxAmount);
            ajax.AddAjaxSetting(grdFeeCalculation, grdFeeCalculation);
            //ajax.AddAjaxSetting(grdFeeCalculation, RadTabStrip1);
            //ajax.AddAjaxSetting(grdFeeCalculation, RadMultiPage1);

            //ajax.AddAjaxSetting(grdAddDeduc, RadTabStrip1);
            //ajax.AddAjaxSetting(grdAddDeduc, RadMultiPage1);
        }

        #endregion

        #region ComboBox ParamedicID

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ParamedicQuery();
            query.Select(
                query.ParamedicID,
                (query.ParamedicName + " [" + query.ParamedicID + "]").As("ParamedicName")
                );
            query.Where
                (
                    query.Or
                    (
                       query.ParamedicID.Like(searchTextContain),
                       query.ParamedicName.Like(searchTextContain)
                    ),
                    query.IsActive == true
                );
            query.OrderBy(query.ParamedicID.Ascending);

            cboParamedicID.DataSource = query.LoadDataTable();
            cboParamedicID.DataBind();
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_VerificationNo", txtVerificationNo.Text);
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new ParamedicFeeVerification();
            if (entity.LoadByPrimaryKey(txtVerificationNo.Text))
            {
                if (!IsApprovedOrVoid(entity, args))
                    return;
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var str = (new ParamedicFeeVerification()).Approv(txtVerificationNo.Text, AppSession.UserLogin.UserID);
            if (!string.IsNullOrEmpty(str)) {
                args.MessageText = str;
                //args.IsCancel = true;
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var msg = (new ParamedicFeeVerification()).UnApprov(txtVerificationNo.Text, AppSession.UserLogin.UserID);
            if (!string.IsNullOrEmpty(msg))
            {
                ShowInformationHeader(msg);
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            (new ParamedicFeeVerification()).Void(txtVerificationNo.Text, AppSession.UserLogin.UserID);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            (new ParamedicFeeVerification()).UnVoid(txtVerificationNo.Text, AppSession.UserLogin.UserID);
        }

        private static bool IsApprovedOrVoid(esParamedicFeeVerification entity, ValidateArgs args)
        {
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }
            return true;
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ParamedicFeeVerification());
            PopulateNewVerificationNo();

            cboParamedicID.Text = string.Empty;
            txtVerificationDate.SelectedDate = DateTime.Now;
            txtStartDate.SelectedDate = DateTime.Now;
            txtEndDate.SelectedDate = DateTime.Now;
            txtVerificationAmount.Value = 0;
            txtVerificationTaxAmount.Value = 0;
            txtTaxAmount.Value = 0;
            btnGetItem.Enabled = true;
            btnGetAddDeduc.Enabled = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ParamedicFeeVerification();
            if (entity.LoadByPrimaryKey(txtVerificationNo.Text))
            {
                entity.MarkAsDeleted();
                foreach (var item in FeeByAR.Union(Fee4Service))
                {
                    item.VerificationNo = null;
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                foreach (var item in Fee4ServiceByTeam)
                {
                    item.VerificationNo = null;
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                foreach (var item in ParamedicFeeAddDeducs)
                {
                    item.VerificationNo = null;
                    item.LastUpdatedByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                foreach (var item in FeeDeductions)
                {
                    item.VerificationNo = null;
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new ParamedicFeeVerification();
            if (entity.LoadByPrimaryKey(txtVerificationNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new ParamedicFeeVerification();
            entity.AddNew();
            SetEntityValue(entity);
            if ((FeeByAR.Union(Fee4Service).Count() == 0) && 
                (Fee4ServiceByTeam.Count() == 0) &&
                (ParamedicFeeAddDeducs.Count == 0))
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new ParamedicFeeVerification();
            if (entity.LoadByPrimaryKey(txtVerificationNo.Text))
            {
                SetEntityValue(entity);
                if ((FeeByAR.Union(Fee4Service).Count() == 0) && 
                    (Fee4ServiceByTeam.Count() == 0) &&
                    (ParamedicFeeAddDeducs.Count == 0))
                {
                    args.MessageText = AppConstant.Message.RecordDetailEmpty;
                    args.IsCancel = true;
                    return;
                }
                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("VerificationNo='{0}'", txtVerificationNo.Text.Trim());
            auditLogFilter.TableName = "ParamedicFeeVerification";
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuDelete()
        {
            return !chkIsApproved.Checked;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !chkIsApproved.Checked;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return true;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            btnGetItem.Enabled = newVal != AppEnum.DataMode.Read;
            btnGetAddDeduc.Enabled = newVal != AppEnum.DataMode.Read;

            grdFeeCalculation.Columns[grdFeeCalculation.Columns.Count - 1].Visible = newVal != AppEnum.DataMode.Read;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
            {
                Fee4Service = null;
                FeeByAR = null;
                Fee4ServiceByTeam = null;
                ParamedicFeeAddDeducs = null;
                FeeDeductions = null;
            }

            //Perbaharui tampilan dan data
            if (IsPostBack)
            {
                grdFeeCalculation.Rebind();
            }
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ParamedicFeeVerification();
            if (parameters.Length > 0)
            {
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(parameters[0]);
            }
            else
                entity.LoadByPrimaryKey(txtVerificationNo.Text);

            var par = new ParamedicQuery("a");
            par.Select(par.ParamedicID, par.ParamedicName);

            DataTable tbl = par.LoadDataTable();
            cboParamedicID.DataSource = tbl;
            cboParamedicID.DataBind();

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var ver = (ParamedicFeeVerification)entity;
            txtVerificationNo.Text = ver.VerificationNo;
            txtVerificationDate.SelectedDate = ver.VerificationDate;

            if (!string.IsNullOrEmpty(ver.ParamedicID))
            {
                var par = new ParamedicQuery("a");
                par.Select(
                    par.ParamedicID,
                    par.ParamedicName
                    );
                par.Where(par.ParamedicID == ver.str.ParamedicID);
                cboParamedicID.DataSource = null;
                cboParamedicID.Text = string.Empty;

                cboParamedicID.DataSource = par.LoadDataTable();
                cboParamedicID.DataBind();
                cboParamedicID.SelectedValue = ver.ParamedicID;
            }
            else
            {
                cboParamedicID.Items.Clear();
                cboParamedicID.Text = string.Empty;
            }

            txtStartDate.SelectedDate = ver.StartDate;
            txtEndDate.SelectedDate = ver.EndDate;
            txtVerificationAmount.Value = Convert.ToDouble(ver.VerificationAmount);
            txtVerificationTaxAmount.Value = Convert.ToDouble(ver.VerificationTaxAmount);
            txtTaxAmount.Value = Convert.ToDouble(ver.TaxAmount);

            chkIsVoid.Checked = ver.IsVoid ?? false;
            chkIsApproved.Checked = ver.IsApproved ?? false;

            //Display Data Detail
            PopulateGridDetail();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(ParamedicFeeVerification entity)
        {
            PopulateNewVerificationNo();

            entity.VerificationNo = txtVerificationNo.Text;
            entity.VerificationDate = txtVerificationDate.SelectedDate;
            entity.ParamedicID = cboParamedicID.SelectedValue;
            entity.StartDate = txtStartDate.SelectedDate;
            entity.EndDate = txtEndDate.SelectedDate;
            entity.TaxPeriod = Convert.ToInt16(txtEndDate.SelectedDate.Value.Year.ToString());
            entity.VerificationAmount = Convert.ToDecimal(txtVerificationAmount.Value);
            entity.VerificationTaxAmount = Convert.ToDecimal(txtVerificationTaxAmount.Value);
            entity.TaxAmount = Convert.ToDecimal(txtTaxAmount.Value);
            entity.IsVoid = false;
            entity.IsApproved = false;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            decimal totalFee = 0;
            //Update Detil
            foreach (var item in FeeByAR.Union(Fee4Service))
            {
                if (item.VerificationNo != null)
                {
                    item.VerificationNo = txtVerificationNo.Text;
                    totalFee += (item.FeeAmount ?? 0) - (item.SumDeductionAmount ?? 0);
                }

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }

            foreach (var item in Fee4ServiceByTeam)
            {
                if (item.VerificationNo != null)
                {
                    item.VerificationNo = txtVerificationNo.Text;
                    totalFee += (item.FeeAmount ?? 0);// - (item.SumDeductionAmount ?? 0);
                }

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }

            foreach (var item in ParamedicFeeAddDeducs)
            {
                if (item.VerificationNo != null)
                {
                    item.VerificationNo = txtVerificationNo.Text;

                    if (item.SRParamedicFeeAdjustType == AppConstant.ParamedicFeeAdjustType.ADD)
                        totalFee += (item.Amount ?? 0);
                    else
                        totalFee -= (item.Amount ?? 0);
                }

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdatedByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }

            entity.VerificationAmount = totalFee;
        }

        private void SaveEntity(ParamedicFeeVerification entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                Fee4Service.Save();
                FeeByAR.Save();
                Fee4ServiceByTeam.Save();
                ParamedicFeeAddDeducs.Save();
                FeeDeductions.Save();

                if (DataModeCurrent == AppEnum.DataMode.New)
                    _autoNumber.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ParamedicFeeVerificationQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.VerificationNo > txtVerificationNo.Text);
                que.OrderBy(que.VerificationNo.Ascending);
            }
            else
            {
                que.Where(que.VerificationNo < txtVerificationNo.Text);
                que.OrderBy(que.VerificationNo.Descending);
            }
            var entity = new ParamedicFeeVerification();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region Method & Event TextChanged

        private void CalculateAmountVerification()
        {
            decimal? totalFee = 0;
            decimal? totalTax = 0;

            /*(0:calculated fee, 1:tariff component fee)*/
            string BaseCalculateTax = AppParameter.GetParameterValue(AppParameter.ParameterItem.pphFeeBase);

            if (Fee4Service.HasData)
            {
                totalFee += Fee4Service.Where(item => item.VerificationNo != null)
                    .Aggregate(totalFee, (current, item) => current + item.FeeAmount - (item.SumDeductionAmount ?? 0));
                totalTax += Fee4Service.Where(item => item.VerificationNo != null && item.IsIncludeInTaxCalc == true)
                    .Aggregate(totalTax, (current, item) => current + ((BaseCalculateTax == "1" && (
                    item.SRPhysicianFeeCategory == "01" ||
                    System.Convert.ToInt32(string.IsNullOrEmpty(item.SRPhysicianFeeCategory) ? "0" : item.SRPhysicianFeeCategory) >= 4
                    )) ? item.Price: item.FeeAmount) /* pajak dihitung sebelum potongan bro!!- (item.SumDeductionAmount ?? 0) */ );
            } 
            if (FeeByAR.HasData)
            {
                totalFee += FeeByAR.Where(item => item.VerificationNo != null).Aggregate(totalFee, (current, item) => current + item.FeeAmount - (item.SumDeductionAmount ?? 0));
                totalTax += FeeByAR.Where(item => item.VerificationNo != null && item.IsIncludeInTaxCalc == true).Aggregate(totalTax, (current, item) => current + item.FeeAmount /* pajak dihitung sebelum potongan bro!!- (item.SumDeductionAmount ?? 0) */ );
            }
            if (Fee4ServiceByTeam.HasData)
            {
                totalFee += Fee4ServiceByTeam.Where(item => item.VerificationNo != null)
                    .Aggregate(totalFee, (current, item) => current + item.FeeAmount);// - (item.SumDeductionAmount ?? 0));
                totalTax += Fee4ServiceByTeam.Where(item => item.VerificationNo != null && item.IsIncludeInTaxCalc == true)
                    .Aggregate(totalTax, (current, item) => current + ((BaseCalculateTax == "1") ? item.Price : item.FeeAmount) /* pajak dihitung sebelum potongan bro!!- (item.SumDeductionAmount ?? 0) */ );
            }
            if (ParamedicFeeAddDeducs.HasData)
            {
                foreach (var item in ParamedicFeeAddDeducs.Where(item => item.VerificationNo != null))
                {
                    if (item.SRParamedicFeeAdjustType == AppConstant.ParamedicFeeAdjustType.ADD)
                    {
                        totalFee += (item.Amount);
                        if (item.IsIncludeInTaxCalc == true)
                            totalTax += (item.Amount);
                    }
                    else
                    {
                        totalFee -= (item.Amount);
                        if (item.IsIncludeInTaxCalc == true)
                            totalTax -= (item.Amount);
                    }
                }
            }

            txtVerificationAmount.Value = Convert.ToDouble(totalFee);
            txtVerificationTaxAmount.Value = Convert.ToDouble(totalTax);
        }

        #endregion

        #region Fee Calculation

        private ParamedicFeeTransChargesItemCompByDischargeDateCollection Fee4Service
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collParamedicFeeTransChargesItemCompByDischargeDate4Service"];
                    if (obj != null)
                        return ((ParamedicFeeTransChargesItemCompByDischargeDateCollection)(obj));
                }

                var coll = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();

                var query = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("a");
                var txhQ = new TransChargesQuery("c");
                var itemQ = new ItemQuery("d");
                var regQ = new RegistrationQuery("e");
                var patQ = new PatientQuery("f");
                var guarQ = new GuarantorQuery("j");
                var tcQ = new TariffComponentQuery("k");

                query.InnerJoin(txhQ).On(query.TransactionNo == txhQ.TransactionNo);
                query.InnerJoin(itemQ).On(query.ItemID == itemQ.ItemID);
                query.InnerJoin(regQ).On(txhQ.RegistrationNo == regQ.RegistrationNo);
                query.InnerJoin(patQ).On(regQ.PatientID == patQ.PatientID);
                query.InnerJoin(guarQ).On(regQ.GuarantorID == guarQ.GuarantorID);
                query.InnerJoin(tcQ).On(query.TariffComponentID == tcQ.TariffComponentID);

                query.Where(query.VerificationNo == txtVerificationNo.Text);
                query.OrderBy(regQ.RegistrationNo.Ascending, itemQ.ItemID.Ascending);

                query.Select
                    (
                        query,
                        itemQ.ItemName.As("refToItem_ItemName"),
                        txhQ.RegistrationNo.As("refToTransCharges_RegistrationNo"),
                        patQ.MedicalNo.As("refToPatient_MedicalNo"),
                        patQ.PatientName.As("refToPatient_PatientName"),
                        guarQ.GuarantorName.As("refToGuarantor_GuarantorName"),
                        "<'' AS refToVwClosedRegistration_PaymentMethod>",
                        tcQ.IsIncludeInTaxCalc.As("refToTariffComponent_IsIncludeInTaxCalc")
                    );

                coll.Load(query);

                Session["collParamedicFeeTransChargesItemCompByDischargeDate4Service"] = coll;
                return coll;
            }
            set { Session["collParamedicFeeTransChargesItemCompByDischargeDate4Service"] = value; }
        }
        private ParamedicFeeTransChargesItemCompByTeamCollection Fee4ServiceByTeam
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collParamedicFeeTransChargesItemCompByTeam4Service"];
                    if (obj != null)
                        return ((ParamedicFeeTransChargesItemCompByTeamCollection)(obj));
                }

                var coll = new ParamedicFeeTransChargesItemCompByTeamCollection();

                var query = new ParamedicFeeTransChargesItemCompByTeamQuery("a");
                var txhQ = new TransChargesQuery("c");
                var itemQ = new ItemQuery("d");
                var regQ = new RegistrationQuery("e");
                var patQ = new PatientQuery("f");
                var guarQ = new GuarantorQuery("j");
                var tcQ = new TariffComponentQuery("k");

                query.InnerJoin(txhQ).On(query.TransactionNo == txhQ.TransactionNo);
                query.InnerJoin(itemQ).On(query.ItemID == itemQ.ItemID);
                query.InnerJoin(regQ).On(txhQ.RegistrationNo == regQ.RegistrationNo);
                query.InnerJoin(patQ).On(regQ.PatientID == patQ.PatientID);
                query.InnerJoin(guarQ).On(regQ.GuarantorID == guarQ.GuarantorID);
                query.InnerJoin(tcQ).On(query.TariffComponentID == tcQ.TariffComponentID);

                query.Where(query.VerificationNo == txtVerificationNo.Text);
                query.OrderBy(regQ.RegistrationNo.Ascending, itemQ.ItemID.Ascending);

                query.Select
                    (
                        query,
                        itemQ.ItemName.As("refToItem_ItemName"),
                        txhQ.RegistrationNo.As("refToTransCharges_RegistrationNo"),
                        patQ.MedicalNo.As("refToPatient_MedicalNo"),
                        patQ.PatientName.As("refToPatient_PatientName"),
                        guarQ.GuarantorName.As("refToGuarantor_GuarantorName"),
                        "<'' AS refToVwClosedRegistration_PaymentMethod>",
                        tcQ.IsIncludeInTaxCalc.As("refToTariffComponent_IsIncludeInTaxCalc")
                    );

                coll.Load(query);

                Session["collParamedicFeeTransChargesItemCompByTeam4Service"] = coll;
                return coll;
            }
            set { Session["collParamedicFeeTransChargesItemCompByTeam4Service"] = value; }
        }
        private ParamedicFeeTransChargesItemCompByDischargeDateCollection FeeByAR
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collParamedicFeeTransChargesItemCompByDischargeDateByAR"];
                    if (obj != null)
                        return ((ParamedicFeeTransChargesItemCompByDischargeDateCollection)(obj));
                }

                var collAR = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();

                var queryAR = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("a");
                var guarQAR = new GuarantorQuery("b");
                var regQAR = new RegistrationQuery("c");
                var patQAR = new PatientQuery("d");

                queryAR.InnerJoin(regQAR).On(queryAR.RegistrationNo == regQAR.RegistrationNo);
                queryAR.InnerJoin(patQAR).On(regQAR.PatientID == patQAR.PatientID);
                queryAR.InnerJoin(guarQAR).On(queryAR.ItemID == guarQAR.GuarantorID);

                queryAR.Where(queryAR.VerificationNo == txtVerificationNo.Text);
                queryAR.OrderBy(regQAR.RegistrationNo.Ascending, guarQAR.GuarantorName.Ascending);

                queryAR.Select
                    (
                        queryAR,
                        guarQAR.GuarantorName.As("refToItem_ItemName"),
                    //txhQ.RegistrationNo.As("refToTransCharges_RegistrationNo"),
                        patQAR.MedicalNo.As("refToPatient_MedicalNo"),
                        patQAR.PatientName.As("refToPatient_PatientName"),
                        guarQAR.GuarantorName.As("refToGuarantor_GuarantorName"),
                        "<'' AS refToVwClosedRegistration_PaymentMethod>",
                        "<CAST(1 as bit) refToTariffComponent_IsIncludeInTaxCalc>"
                    );

                collAR.Load(queryAR);

                Session["collParamedicFeeTransChargesItemCompByDischargeDateByAR"] = collAR;
                return collAR;
            }
            set { Session["collParamedicFeeTransChargesItemCompByDischargeDateByAR"] = value; }
        }

        private ParamedicFeeDeductionsCollection FeeDeductions
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collFeeDeductions"];
                    if (obj != null)
                        return ((ParamedicFeeDeductionsCollection)(obj));
                }

                var coll = new ParamedicFeeDeductionsCollection();

                var query = new ParamedicFeeDeductionsQuery("a");
                query.Where(query.VerificationNo == txtVerificationNo.Text);
                query.Select(query);

                coll.Load(query);

                Session["collFeeDeductions"] = coll;
                return coll;
            }
            set { Session["collFeeDeductions"] = value; }
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            Fee4Service = null; //Reset Record Detail
            FeeByAR = null; //Reset Record Detail
            Fee4ServiceByTeam = null;
            ParamedicFeeAddDeducs = null; //Reset Record Detail
            FeeDeductions = null; //Reset deduction
            grdFeeCalculation.DataSource = FeeSource();
            grdFeeCalculation.MasterTableView.IsItemInserted = false;
            grdFeeCalculation.MasterTableView.ClearEditItems();
            grdFeeCalculation.DataBind();
        }

        protected void grdFeeCalculation_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdFeeCalculation.DataSource = FeeSource();
        }

        private System.Collections.Generic.IEnumerable<CustomFee> FeeSource(){
            var x1 = (FeeByAR.Union(Fee4Service).Select(fee => new CustomFee()
            {
                TransactionNo = fee.TransactionNo,
                SequenceNo = fee.SequenceNo,
                TariffComponentID = fee.TariffComponentID,
                ParamedicID = fee.ParamedicID,
                RegistrationNo = fee.RegistrationNo,
                MedicalNo = fee.MedicalNo,
                PatientName = fee.PatientName,
                GuarantorName = fee.GuarantorName,
                PaymentMethodName = fee.PaymentMethodName,
                ItemName = fee.ItemName,
                PriceItem = fee.PriceItem??0,
                Price = fee.Price??0,
                DiscountItem = fee.DiscountItem??0,
                Discount = fee.Discount??0,
                CalculatedAmount = fee.CalculatedAmount??0,
                FeeAmount = fee.FeeAmount??0,
                SumDeductionAmount = fee.SumDeductionAmount??0,
                Nett = fee.Nett,
                VerificationNo = fee.VerificationNo
            }));

            var x2 = (Fee4ServiceByTeam.Select(fee => new CustomFee()
            {
                TransactionNo = fee.TransactionNo,
                SequenceNo = fee.SequenceNo,
                TariffComponentID = fee.TariffComponentID,
                ParamedicID = fee.ParamedicID,
                RegistrationNo = fee.RegistrationNo,
                MedicalNo = fee.MedicalNo,
                PatientName = fee.PatientName,
                GuarantorName = fee.GuarantorName,
                PaymentMethodName = string.Empty, // fee.PaymentMethodName,
                ItemName = fee.ItemName,
                PriceItem = fee.PriceItem ?? 0,
                Price = fee.Price ?? 0,
                DiscountItem = fee.DiscountItem ?? 0,
                Discount = fee.Discount ?? 0,
                CalculatedAmount = fee.CalculatedAmount ?? 0,
                FeeAmount = fee.FeeAmount ?? 0,
                SumDeductionAmount = 0, //fee.SumDeductionAmount ?? 0,
                Nett = fee.FeeAmount ?? 0, //fee.Nett,
                VerificationNo = fee.VerificationNo
            }));

            var x3 = (ParamedicFeeAddDeducs.Select(fee => new CustomFee(){
                    TransactionNo = fee.TransactionNo,
                    SequenceNo = "00",
                    TariffComponentID = fee.TariffComponentID,
                    ParamedicID = fee.ParamedicID,
                    RegistrationNo = "AddDec",
                    MedicalNo = "AddDec",
                    PatientName = "Additional / Deduction",
                    GuarantorName = "",
                    PaymentMethodName = "",
                    ItemName = "Additional / Deduction" + fee.Notes,
                    PriceItem = (fee.Price ?? (fee.Amount ?? 0)) * (fee.SRParamedicFeeAdjustType.Equals("02") ? -1 : 1),
                    Price = (fee.Price ?? (fee.Amount ?? 0)) * (fee.SRParamedicFeeAdjustType.Equals("02") ? -1 : 1),
                    DiscountItem = (decimal)0,
                    Discount = (decimal)0,
                    CalculatedAmount = (fee.Amount ?? 0) * (fee.SRParamedicFeeAdjustType.Equals("02") ? -1 : 1),
                    FeeAmount = (fee.Amount ?? 0) * (fee.SRParamedicFeeAdjustType.Equals("02") ? -1 : 1),
                    SumDeductionAmount = (decimal)0,
                    Nett = (fee.Amount ?? 0) * (fee.SRParamedicFeeAdjustType.Equals("02") ? -1 : 1),
                    VerificationNo = fee.VerificationNo
                })
            );

            return(x1.Union(x2).Union(x3));
        }

        private class CustomFee { 
            public string TransactionNo { get; set; }
            public string SequenceNo { get; set; }
            public string TariffComponentID { get; set; }
            public string ParamedicID { get; set; }
            public string RegistrationNo { get; set; }
            public string MedicalNo { get; set; }
            public string PatientName { get; set; }
            public string GuarantorName { get; set; }
            public string PaymentMethodName { get; set; }
            public string ItemName { get; set; }
            public decimal PriceItem { get; set; }
            public decimal Price { get; set; }
            public decimal DiscountItem { get; set; }
            public decimal Discount { get; set; }
            public decimal CalculatedAmount { get; set; }
            public decimal FeeAmount { get; set; }
            public decimal SumDeductionAmount { get; set; }
            public decimal Nett { get; set; }
            public string VerificationNo { get; set; }
        }

        private ParamedicFeeTransChargesItemCompByDischargeDate FindFee(String transactionNo, String sequenceNo, String componentID, string paramedicID)
        {
            var coll = FeeByAR.Union(Fee4Service);
            return
                coll.FirstOrDefault(
                    rec =>
                    (rec.TransactionNo.Equals(transactionNo)) &&
                    (rec.SequenceNo.Equals(sequenceNo)) && 
                    (rec.TariffComponentID.Equals(componentID)) && 
                    (rec.ParamedicID.Equals(paramedicID)));
        }
        private ParamedicFeeTransChargesItemCompByTeam FindFeeByTeam(String transactionNo, String sequenceNo, String componentID, string paramedicID)
        {
            return
                Fee4ServiceByTeam.FirstOrDefault(
                    rec =>
                    (rec.TransactionNo.Equals(transactionNo)) &&
                    (rec.SequenceNo.Equals(sequenceNo)) &&
                    (rec.TariffComponentID.Equals(componentID)) &&
                    (rec.ParamedicID.Equals(paramedicID)));
        }

        private ParamedicFeeDeductions[] FindFeeDeductions(String transactionNo, String sequenceNo, String componentID)
        {
            return
                FeeDeductions.Where(rec =>
                    (rec.TransactionNo.Equals(transactionNo)) &&
                    (rec.SequenceNo.Equals(sequenceNo)) && 
                    (rec.TariffComponentID.Equals(componentID))).ToArray();
        }
        

        protected void grdFeeCalculation_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var transactionNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.TransactionNo]);
            var sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SequenceNo]);
            var componentID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.TariffComponentID]);
            var paramedicID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.ParamedicID]);

            var entity = FindFee(transactionNo, sequenceNo, componentID, paramedicID);
            if (entity != null)
                entity.VerificationNo = null;

            var feeBt = FindFeeByTeam(transactionNo, sequenceNo, componentID, paramedicID);
            if (entity != null)
                entity.VerificationNo = null;

            var adddec = FindFeeAddDeduc(transactionNo);
            if (adddec != null)
                adddec.VerificationNo = null;
            var feeDecs = FindFeeDeductions(transactionNo, sequenceNo, componentID);
            foreach (var feeDec in feeDecs) {
                feeDec.VerificationNo = null;
            }

            grdFeeCalculation.Rebind();

            CalculateAmountVerification();
        }

        #endregion

        #region Additional / Deduction
        private ParamedicFeeTransChargesItemCompByDischargeDateCollection AddDecToFeeByARForDisplayOnly(ParamedicFeeAddDeducCollection ads)
        {
            var coll = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            foreach(var ad in ads){
                var n = coll.AddNew();
                n.TransactionNo = ad.TransactionNo;
                n.SequenceNo = "00";
                n.TariffComponentID = ad.TariffComponentID;
                n.DischargeDate = ad.TransactionDate;
                n.ParamedicID = ad.ParamedicID;
                n.ItemID = "AddDec";
                n.Qty = 1;
                n.Price = ad.Amount;
                n.Discount = 0;
                n.FeeAmount = ad.Amount;
                n.IsCalcDeductionInPercent = false;
                n.CalculatedAmount = ad.Amount;
                n.VerificationNo = ad.VerificationNo;
                n.SumDeductionAmount = 0;

                n.ItemName = "Additional Deduction: " + ad.Notes;
                n.RegistrationNo = n.TransactionNo;
                n.MedicalNo = "AddDec";
                n.PatientName = "Additional Deduction";
                n.IsIncludeInTaxCalc = ad.IsIncludeInTaxCalc;
                coll.AttachEntity(n);
            }
            return coll;
        }

        private ParamedicFeeAddDeducCollection ParamedicFeeAddDeducs
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collParamedicFeeAddDeduc"];
                    if (obj != null)
                        return ((ParamedicFeeAddDeducCollection)(obj));
                }

                var coll = new ParamedicFeeAddDeducCollection();

                var query = new ParamedicFeeAddDeducQuery("a");
                var stdQuery = new AppStandardReferenceItemQuery("b");
                query.InnerJoin(stdQuery).On(query.SRParamedicFeeAdjustType == stdQuery.ItemID &&
                                             stdQuery.StandardReferenceID == "ParamedicFeeAdjustType");

                query.Where(query.VerificationNo == txtVerificationNo.Text);
                query.OrderBy(query.TransactionNo.Ascending);

                query.Select
                    (
                        query,
                        stdQuery.ItemName.As("refToAppStandardReferenceItem_ItemName")
                    );

                coll.Load(query);

                Session["collParamedicFeeAddDeduc"] = coll;
                return coll;
            }
            set { Session["collParamedicFeeAddDeduc"] = value; }
        }

        private ParamedicFeeAddDeduc FindFeeAddDeduc(String transactionNo)
        {
            var coll = ParamedicFeeAddDeducs;
            return coll.FirstOrDefault(rec => rec.TransactionNo.Equals(transactionNo));
        }

        #endregion

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument == "rebind")
            {
                if ((sourceControl as RadGrid).ID == grdFeeCalculation.ID)
                {
                    //foreach (var entity in FeeByAR.Union(Fee4Service))
                    //{
                    //    var item = new Item();
                    //    item.LoadByPrimaryKey(entity.ItemID);
                    //    entity.ItemName = item.ItemName;

                    //    var transCharges = new TransCharges();
                    //    transCharges.LoadByPrimaryKey(entity.TransactionNo);
                    //    entity.RegistrationNo = transCharges.RegistrationNo;

                    //    var registration = new Registration();
                    //    registration.LoadByPrimaryKey(entity.RegistrationNo);

                    //    var patient = new Patient();
                    //    patient.LoadByPrimaryKey(registration.PatientID);

                    //    entity.MedicalNo = patient.MedicalNo;
                    //    entity.PatientName = patient.PatientName;

                    //    var guarantor = new Guarantor();
                    //    guarantor.LoadByPrimaryKey(registration.GuarantorID);
                    //    entity.GuarantorName = guarantor.GuarantorName;

                    //    var tc = new TariffComponent();
                    //    tc.LoadByPrimaryKey(entity.TariffComponentID);
                    //    entity.IsIncludeInTaxCalc = tc.IsIncludeInTaxCalc ?? false;
                    //}

                    //RadTabStrip1.Tabs[0].Selected = true;
                    //RadMultiPage1.SelectedIndex = 0;
                    grdFeeCalculation.Rebind();
                }

                CalculateAmountVerification();
            }
        }
    }
}
