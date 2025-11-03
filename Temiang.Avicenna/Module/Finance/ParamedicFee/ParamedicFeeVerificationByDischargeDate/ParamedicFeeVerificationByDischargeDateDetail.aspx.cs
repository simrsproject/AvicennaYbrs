using System;
using System.Linq;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using System.Web.UI;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee
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
                UrlPageList = "../ParamedicFeeVerificationByDischargeDate/ParamedicFeeVerificationByDischargeDateList.aspx";
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

            ajax.AddAjaxSetting(grdAddDeduc, txtVerificationAmount);
            ajax.AddAjaxSetting(grdAddDeduc, txtVerificationTaxAmount);
            ajax.AddAjaxSetting(grdAddDeduc, grdAddDeduc);
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
            var query = new ParamedicQuery();
            query.Select(
                query.ParamedicID,
                (query.ParamedicName + " [" + query.ParamedicID + "]").As("ParamedicName")
                );
            query.Where
                (
                    query.Or
                    (
                       query.ParamedicID.Like(string.Format("%{0}%", e.Text)),
                       query.ParamedicName.Like(string.Format("%{0}%", e.Text))
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
            (new ParamedicFeeVerification()).Approv(txtVerificationNo.Text, AppSession.UserLogin.UserID);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            (new ParamedicFeeVerification()).UnApprov(txtVerificationNo.Text, AppSession.UserLogin.UserID);
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
                foreach (var item in ParamedicFeeTransChargesItemCompByDischargeDate)
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
            if ((ParamedicFeeTransChargesItemCompByDischargeDate.Count == 0) && (ParamedicFeeAddDeducs.Count == 0))
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
                if ((ParamedicFeeTransChargesItemCompByDischargeDate.Count == 0) && (ParamedicFeeAddDeducs.Count == 0))
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
            grdAddDeduc.Columns[grdAddDeduc.Columns.Count - 1].Visible = newVal != AppEnum.DataMode.Read;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
            {
                ParamedicFeeTransChargesItemCompByDischargeDate = null;
                ParamedicFeeAddDeducs = null;
            }

            //Perbaharui tampilan dan data
            if (IsPostBack)
            {
                grdFeeCalculation.Rebind();
                grdAddDeduc.Rebind();
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
            PopulateGridDetailAddDeduc();
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
            foreach (var item in ParamedicFeeTransChargesItemCompByDischargeDate)
            {
                if (item.VerificationNo != null)
                {
                    item.VerificationNo = txtVerificationNo.Text;
                    totalFee += (item.FeeAmount ?? 0);
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
                ParamedicFeeTransChargesItemCompByDischargeDate.Save();
                ParamedicFeeAddDeducs.Save();

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

            if (ParamedicFeeTransChargesItemCompByDischargeDate.HasData)
            {
                totalFee = ParamedicFeeTransChargesItemCompByDischargeDate.Where(item => item.VerificationNo != null)
                    .Aggregate(totalFee, (current, item) => current + item.FeeAmount);
                totalTax = ParamedicFeeTransChargesItemCompByDischargeDate.Where(item => item.VerificationNo != null && item.IsIncludeInTaxCalc == true)
                    .Aggregate(totalTax, (current, item) => current + ((BaseCalculateTax == "1" && (item.SRPhysicianFeeCategory == "01" || item.SRPhysicianFeeCategory == "04")) ? item.Price : item.FeeAmount));
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

        private ParamedicFeeTransChargesItemCompByDischargeDateCollection ParamedicFeeTransChargesItemCompByDischargeDate
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collParamedicFeeTransChargesItemCompByDischargeDate" + Request.UserHostName];
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

                //foreach (var c in coll)
                //{
                //    var table = new ParamedicFeeTransChargesItemCompByDischargeDateCollection().GetPaymentType(c.TransactionNo,c.SequenceNo);
                //    if (table.AsEnumerable().Any())
                //    {
                //        var payment = table.AsEnumerable().Aggregate(string.Empty, (current, t) => current + (t["PaymentMethodName"].ToString() + ", "));
                //        if (!string.IsNullOrEmpty(payment))
                //            c.PaymentMethod = payment;
                //    }
                //}

                Session["collParamedicFeeTransChargesItemCompByDischargeDate" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collParamedicFeeTransChargesItemCompByDischargeDate" + Request.UserHostName] = value; }
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            ParamedicFeeTransChargesItemCompByDischargeDate = null; //Reset Record Detail
            grdFeeCalculation.DataSource = ParamedicFeeTransChargesItemCompByDischargeDate;
            grdFeeCalculation.MasterTableView.IsItemInserted = false;
            grdFeeCalculation.MasterTableView.ClearEditItems();
            grdFeeCalculation.DataBind();
        }

        protected void grdFeeCalculation_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdFeeCalculation.DataSource = ParamedicFeeTransChargesItemCompByDischargeDate;
        }

        private ParamedicFeeTransChargesItemCompByDischargeDate FindTransChargesItemComp(String transactionNo, String sequenceNo, String componentID)
        {
            var coll = ParamedicFeeTransChargesItemCompByDischargeDate;
            return
                coll.FirstOrDefault(
                    rec =>
                    (rec.TransactionNo.Equals(transactionNo)) &&
                    (rec.SequenceNo.Equals(sequenceNo)) && (rec.TariffComponentID.Equals(componentID)));
        }

        protected void grdFeeCalculation_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var transactionNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.TransactionNo]);
            var sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SequenceNo]);
            var componentID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.TariffComponentID]);
            var entity = FindTransChargesItemComp(transactionNo, sequenceNo, componentID);
            if (entity != null)
                entity.VerificationNo = null;

            grdFeeCalculation.Rebind();

            CalculateAmountVerification();
        }

        #endregion

        #region Additional / Deduction

        private ParamedicFeeAddDeducCollection ParamedicFeeAddDeducs
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collParamedicFeeAddDeduc" + Request.UserHostName];
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

                Session["collParamedicFeeAddDeduc" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collParamedicFeeAddDeduc" + Request.UserHostName] = value; }
        }

        private void PopulateGridDetailAddDeduc()
        {
            //Display Data Detail
            ParamedicFeeAddDeducs = null; //Reset Record Detail
            grdAddDeduc.DataSource = ParamedicFeeAddDeducs;
            grdAddDeduc.MasterTableView.IsItemInserted = false;
            grdAddDeduc.MasterTableView.ClearEditItems();
            grdAddDeduc.DataBind();
        }

        protected void grdAddDeduc_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdAddDeduc.DataSource = ParamedicFeeAddDeducs;
        }

        private ParamedicFeeAddDeduc FindFeeAddDeduc(String transactionNo)
        {
            var coll = ParamedicFeeAddDeducs;
            return coll.FirstOrDefault(rec => rec.TransactionNo.Equals(transactionNo));
        }

        protected void grdAddDeduc_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var transactionNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ParamedicFeeAddDeducMetadata.ColumnNames.TransactionNo]);
            var entity = FindFeeAddDeduc(transactionNo);
            if (entity != null)
                entity.VerificationNo = null;

            grdAddDeduc.Rebind();

            CalculateAmountVerification();
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
                    foreach (var entity in ParamedicFeeTransChargesItemCompByDischargeDate)
                    {
                        var item = new Item();
                        item.LoadByPrimaryKey(entity.ItemID);
                        entity.ItemName = item.ItemName;

                        var transCharges = new TransCharges();
                        transCharges.LoadByPrimaryKey(entity.TransactionNo);
                        entity.RegistrationNo = transCharges.RegistrationNo;

                        var registration = new Registration();
                        registration.LoadByPrimaryKey(entity.RegistrationNo);

                        var patient = new Patient();
                        patient.LoadByPrimaryKey(registration.PatientID);

                        entity.MedicalNo = patient.MedicalNo;
                        entity.PatientName = patient.PatientName;

                        var guarantor = new Guarantor();
                        guarantor.LoadByPrimaryKey(registration.GuarantorID);
                        entity.GuarantorName = guarantor.GuarantorName;

                        var tc = new TariffComponent();
                        tc.LoadByPrimaryKey(entity.TariffComponentID);
                        entity.IsIncludeInTaxCalc = tc.IsIncludeInTaxCalc ?? false;
                    }

                    //RadTabStrip1.Tabs[0].Selected = true;
                    //RadMultiPage1.SelectedIndex = 0;
                    grdFeeCalculation.Rebind();
                }
                else if ((sourceControl as RadGrid).ID == grdAddDeduc.ID)
                {
                    foreach (var entity in ParamedicFeeAddDeducs)
                    {
                        var std = new AppStandardReferenceItem();
                        std.LoadByPrimaryKey("ParamedicFeeAdjustType", entity.SRParamedicFeeAdjustType);
                        entity.ParamedicFeeAdjustType = std.ItemName;
                    }

                    //RadTabStrip1.Tabs[1].Selected = true;
                    //RadMultiPage1.SelectedIndex = 1;
                    grdAddDeduc.Rebind();
                }

                CalculateAmountVerification();
            }
        }
    }
}
