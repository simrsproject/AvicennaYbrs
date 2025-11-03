using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;
using System.Web.UI.HtmlControls;


namespace Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry
{
    public partial class VoucherEntryDetail : BasePageDetail
    {
        protected int JournalId
        {
            get
            {
                string tmpVal = this.lblJournalId.Text; //Request.QueryString["ivd"];
                int ret = 0;
                int.TryParse(tmpVal, out ret);
                return (ret == 0) ? System.Convert.ToInt32(Request.QueryString["ivd"]) : ret;
            }
            set
            {
                this.lblJournalId.Text = value.ToString();
            }
        }
        protected void btnDetailTransView_Click(object sender, ImageClickEventArgs e)
        {

        }

        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            //ToolBarMenuSearch.Enabled = false;
            UrlPageSearch = "VoucherEntrySearch.aspx";
            this.WindowSearch.Height = 470;

            ToolBarMenuMovePrev.Enabled = false;
            ToolBarMenuMoveNext.Enabled = false;

            if (Request.QueryString["source"] == "je")
            {
                ProgramID = AppConstant.Program.VOUCHER_MEMORIAL;
                UrlPageList = "VoucherEntryList.aspx?pg=" + Request.QueryString["pg"];
                UrlPageSearch = "VoucherEntrySearch.aspx?pg=0";
            }
            else if (Request.QueryString["source"] == "jcv")
            {
                ProgramID = AppConstant.Program.VOUCHER_VERIFY_CASHIER;
                UrlPageList = "VoucherVerifyCashierList.aspx?kasir=" + Request.QueryString["kasir"] + "&sdate=" + Request.QueryString["sdate"] + "&edate=" + Request.QueryString["edate"];
                UrlPageSearch = "#";
            }
            else if (Request.QueryString["source"] == "jav")
            {
                ProgramID = AppConstant.Program.VOUCHER_VERIFY_AR;
                UrlPageList = "VoucherVerifyArList.aspx";
                UrlPageSearch = "#";
            }
            else if (Request.QueryString["source"] == "ap")
            {
                ProgramID = AppConstant.Program.VOUCHER_VERIFY_AP;
                UrlPageList = "VoucherVerifyApList.aspx";
                UrlPageSearch = "#";
            }
            else if (Request.QueryString["source"] == "jcev")
            {
                ProgramID = AppConstant.Program.VOUCHER_VERIFY_CASH_ENTRY;
                UrlPageList = "VoucherVerifyCashEntryList.aspx";
                UrlPageSearch = "#";
            }
            else if (Request.QueryString["source"] == "jii")
            {
                ProgramID = AppConstant.Program.VOUCHER_VERIFY_INV_ISSUE;
                UrlPageList = "VoucherVerifyInventoryIssueList.aspx";
                UrlPageSearch = "#";
            }
            else if (Request.QueryString["source"] == "pv")
            {
                ProgramID = AppConstant.Program.VOUCHER_VERIFY_FEE;
                UrlPageList = "VoucherVerifyFeeList.aspx";
                UrlPageSearch = "#";
            }

            Helper.SetupComboBox(txtJournalCode);
            Helper.SetupGrid(grdVoucherEntryItem);

            this.grdVoucherEntryItem.SortCommand += new GridSortCommandEventHandler(grdVoucherEntryItem_SortCommand);
            this.grdVoucherEntryItem.ItemCommand += new GridCommandEventHandler(grdVoucherEntryItem_ItemCommand);
            this.grdVoucherEntryItem.ItemDataBound += new GridItemEventHandler(grdVoucherEntryItem_ItemDataBound);

            this.txtJournalCode.ItemsRequested += new RadComboBoxItemsRequestedEventHandler(txtJournalCode_ItemsRequested);
            this.txtJournalCode.ItemDataBound += new RadComboBoxItemEventHandler(txtJournalCode_ItemDataBound);
            this.txtJournalCode.TextChanged += new EventHandler(txtJournalCode_TextChanged);

            if (!IsPostBack)
            {
                trBudgettingCode.Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB";

                switch (Request.QueryString["jt"])
                {
                    case "02": //Income (Registrasi, Service Unit Entry & JO Realization)
                        tabDetail.Tabs[2].Visible = true;
                        mainiFrame.Attributes["src"] = "CheckDetailTransaction/IncomeTypeDetail.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        mainiFrame2.Attributes["src"] = "CheckCoaSetting/PaymentTypeCoaSetting.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        break;
                    case "03": //Prescription
                        mainiFrame.Attributes["src"] = "CheckDetailTransaction/IncomeTypeDetail.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        break;
                    case "04": //Prescription return
                        mainiFrame.Attributes["src"] = "CheckDetailTransaction/IncomeTypeDetail.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        break;
                    case "07": //Payment Return
                        mainiFrame.Attributes["src"] = "CheckDetailTransaction/PaymentTypeDetail.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        break;
                    case "08": //Down Payment Return
                        mainiFrame.Attributes["src"] = "CheckDetailTransaction/PaymentTypeDetail.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        break;
                    case "09": //Down Payment
                        mainiFrame.Attributes["src"] = "CheckDetailTransaction/PaymentTypeDetail.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        break;
                    case "13":
                    case "10": //Payment
                        tabDetail.Tabs[2].Visible = true;
                        mainiFrame.Attributes["src"] = "CheckDetailTransaction/PaymentTypeDetail.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        mainiFrame2.Attributes["src"] = "CheckCoaSetting/PaymentTypeCoaSetting.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        break;
                    case "11": //AR Payment
                        mainiFrame.Attributes["src"] = "CheckDetailTransaction/ARPaymentTypeDetail.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        break;
                    case "15": //PO Received 
                        tabDetail.Tabs[2].Visible = true;
                        mainiFrame.Attributes["src"] = "CheckDetailTransaction/POReceivedTypeDetail.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        mainiFrame2.Attributes["src"] = "CheckCoaSetting/PoReceivedTypeCoaSetting.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        break;
                    case "16": //PO Returned 
                        tabDetail.Tabs[2].Visible = true;
                        mainiFrame.Attributes["src"] = "CheckDetailTransaction/POReturnedTypeDetail.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        mainiFrame2.Attributes["src"] = "CheckCoaSetting/PoReturnedTypeCoaSetting.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        break;
                    case "20": //Ditribution
                        tabDetail.Tabs[2].Visible = true;
                        mainiFrame.Attributes["src"] = "CheckDetailTransaction/DistributionTypeDetail.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        mainiFrame2.Attributes["src"] = "CheckCoaSetting/DistributionTypeCoaSetting.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        break;
                    case "22": //Inventory Issue
                        tabDetail.Tabs[2].Visible = true;
                        mainiFrame.Attributes["src"] = "CheckDetailTransaction/InventoryIssueTypeDetail.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        mainiFrame2.Attributes["src"] = "CheckCoaSetting/InventoryIssueTypeCoaSetting.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        break;
                    case "25": //AP 
                    case "26": //AP Payment
                        mainiFrame.Attributes["src"] = "CheckDetailTransaction/APPaymentTypeDetail.aspx?src=tab&ivd=" + Request.QueryString["ivd"] + "&md=" + Request.QueryString["md"];
                        break;
                    case "27": //Paramedic Fee Payment
                        mainiFrame.Attributes["src"] = "CheckDetailTransaction/ParamedicFeePaymentTypeDetail.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        break;
                    case "28": //Paramedic Fee Verification 
                        tabDetail.Tabs[2].Visible = true;
                        mainiFrame.Attributes["src"] = "CheckDetailTransaction/ParamedicFeeVerificationTypeDetail.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        mainiFrame2.Attributes["src"] = "CheckCoaSetting/ParamedicFeeCoaSetting.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        break;
                    case "32": //Inventory Stock Adjustment
                    case "33": //Inventory Stock Opname
                        // SAMA DENGAN INVENTORY ISSUE
                        tabDetail.Tabs[2].Visible = true;
                        mainiFrame.Attributes["src"] = "CheckDetailTransaction/InventoryIssueTypeDetail.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        mainiFrame2.Attributes["src"] = "CheckCoaSetting/InventoryIssueTypeCoaSetting.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        break;
                    case "34": //Inventory Production
                        tabDetail.Tabs[2].Visible = true;
                        mainiFrame.Attributes["src"] = "CheckDetailTransaction/ProductionTypeDetail.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        mainiFrame2.Attributes["src"] = "CheckCoaSetting/ProductionTypeCoaSetting.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        break;
                    case "35": //Payment
                        //tabDetail.Tabs[1].Visible = false;
                        tabDetail.Tabs[2].Visible = true;
                        //tabDetail.Tabs[2].Selected = true;
                        //mainiFrame.Attributes["src"] = "CheckDetailTransaction/PaymentTypeDetail.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        mainiFrame2.Attributes["src"] = "CheckCoaSetting/PaymentTypeCoaSetting.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        break;
                    case "38": //Payroll
                    case "39": // THR
                    case "40": // sales to customer
                    case "41": // sales to customer return
                    case "42": // customer invoicing
                    case "43": // customer payment
                    case "44": // AssetAuction
                    case "45": // AssetDestruction
                        tabDetail.Tabs[1].Visible = false;
                        tabDetail.Tabs[2].Visible = false;
                        break;
                    case "48": //Paramedic Fee Payable 
                        tabDetail.Tabs[2].Visible = true;
                        //mainiFrame.Attributes["src"] = "CheckDetailTransaction/ParamedicFeeVerificationTypeDetail.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        tabDetail.Tabs[1].Visible = false;
                        mainiFrame2.Attributes["src"] = "CheckCoaSetting/ParamedicFeeCoaSetting.aspx?src=tab&ivd=" + Request.QueryString["ivd"];
                        break;
                }
            }
        }

        void grdVoucherEntryItem_ItemDataBound(object sender, GridItemEventArgs e)
        {
        }

        void grdVoucherEntryItem_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "InitInsert")
            {
                this.Validate();
                if (!this.IsValid)
                {
                    e.Canceled = true;
                    return;
                }

                Session["JournalTransaction::Description"] = txtDescription.Text;
                Session["JournalTransaction::RefferenceNo"] = txtRefferenceNumber.Text;
            }
        }

        void txtJournalCode_TextChanged(object sender, EventArgs e)
        {
            string code = txtJournalCode.Text;
            JournalCodes entity = JournalCodes.Get(code);
            if (entity != null)
            {
                if (entity.IsAutoNumber ?? false)
                {
                    txtTransactionNumber.Text = "{auto}";
                    txtTransactionNumber.ReadOnly = true;
                }
                else
                {
                    txtTransactionNumber.Text = "";
                    txtTransactionNumber.ReadOnly = false;
                }
            }
        }

        void txtJournalCode_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            //e.Item.Attributes["IsAutoNumber"] = ((JournalCodes)e.Item.DataItem).IsAutoNumber.Value ? "1" : "0";
        }

        void txtJournalCode_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox box = o as RadComboBox;
            string val = e.Text;
            if (val.Length != 0)
            {
                box.Items.Clear();
                JournalCodesCollection coll = JournalCodes.GetLike(val, true);

                box.DataSource = coll;
                box.DataBind();
            }
        }

        void grdVoucherEntryItem_SortCommand(object source, GridSortCommandEventArgs e)
        {
            if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
            {
                GridSortExpression sortExpr = new GridSortExpression();
                sortExpr.FieldName = e.SortExpression;
                sortExpr.SortOrder = e.NewSortOrder;

                this.grdVoucherEntryItem.MasterTableView.SortExpressions.Clear();
                this.grdVoucherEntryItem.MasterTableView.SortExpressions.AddSortExpression(sortExpr);

                this.grdVoucherEntryItem.Rebind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 3000;

            if (!this.IsPostBack)
            {

                string q = Request.QueryString["ivd"];
                int val = 0;
                if (int.TryParse(q, out val))
                    this.JournalId = val;
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdVoucherEntryItem, btnRecalculate, RadAjaxLoadingPanel1);
            ajax.AddAjaxSetting(grdVoucherEntryItem, grdVoucherEntryItem, RadAjaxLoadingPanel1);
            ajax.AddAjaxSetting(grdVoucherEntryItem, txtTotalAmountDebit);
            ajax.AddAjaxSetting(grdVoucherEntryItem, txtTotalAmountCredit);
            ajax.AddAjaxSetting(grdVoucherEntryItem, txtSelisih);
            ajax.AddAjaxSetting(grdVoucherEntryItem, chkIsApproved);
            ajax.AddAjaxSetting(grdVoucherEntryItem, chkIsVoid);
            ajax.AddAjaxSetting(grdVoucherEntryItem, lblMessage);
            var info = (System.Web.UI.WebControls.Panel)Helper.FindControlRecursive(Master, "fw_PanelStatus");
            ajax.AddAjaxSetting(grdVoucherEntryItem, info);
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            if (this.JournalId == 0)
            {
                args.MessageText = AppConstant.Message.RecordCanNotEdited;
                args.IsCancel = true;
                return;
            }

            JournalTransactions entity = JournalTransactions.Get(this.JournalId);
            if (entity == null)
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            if (entity.IsPosted.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved + AppConstant.Message.RecordCanNotEdited;
                args.IsCancel = true;
                return;
            }

            if (entity.IsVoid.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided + AppConstant.Message.RecordCanNotEdited;
                args.IsCancel = true;
                return;
            }
        }

        protected override void OnMenuNewClick()
        {
            this.JournalId = 0;
            this.txtTransactionDate.SelectedDate = DateTime.Now;
            Session.Remove("JournalTransaction::Description");
            Session.Remove("JournalTransaction::RefferenceNo");
            OnPopulateEntryControl(new JournalTransactions());
            btnDetailTransView.Enabled = false;
            this.grdVoucherEntryItem.Rebind();
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            JournalTransactions entity = JournalTransactions.Get(this.JournalId);
            if (entity != null)
            {
                if (entity.IsPosted.Value)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved + AppConstant.Message.RecordCanNotDeleted;
                    args.IsCancel = true;
                    return;
                }

                entity.MarkAsDeleted();

                JournalTransactionDetailsCollection coll = new JournalTransactionDetailsCollection();
                coll.Query.Where(coll.Query.JournalId == JournalId);
                coll.LoadAll();
                coll.MarkAllAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    coll.Save();
                    entity.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            //if (this.JournalId == 0 && (true == (args.IsCancel = true)))
            //    return;

            bool isValid = false;

            JournalCodes journalCode = JournalCodes.Get(txtJournalCode.Text);
            isValid = ((journalCode != null) && (journalCode.IsEnabled ?? false));
            if (!isValid)
            {
                args.MessageText = "Invalid Journal Code";
                args.IsCancel = true;
                return;
            }

            if (!Helper.ValidatePeriode(txtTransactionDate.SelectedDate.Value))
            {
                args.MessageText = AppConstant.Message.TRANSACTION_DATE_HAS_CLOSED;
                args.IsCancel = true;
                return;
            }

            JournalTransactions entity = null;
            if (this.JournalId == 0)
            {
                entity = new JournalTransactions();
                entity.AddNew();
            }
            else
            {
                entity = JournalTransactions.Get(this.JournalId);
                if (entity.IsPosted.Value)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return;
                }
            }

            SetEntityValue(entity);
            if (!this.SaveEntity(entity))
            {
                args.MessageText = "Unable to create new transaction please try again.";
                args.IsCancel = true;
                return;
            }

            this.GenerateGrid();
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            JournalTransactions entity = JournalTransactions.Get(this.JournalId);
            if (entity == null)
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            else
            {
                if (entity.IsPosted.Value)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return;
                }

                if (!Helper.ValidatePeriode(txtTransactionDate.SelectedDate.Value))
                {
                    args.MessageText = AppConstant.Message.TRANSACTION_DATE_HAS_CLOSED;
                    args.IsCancel = true;
                    return;
                }
            }

            entity.TransactionDate = this.txtTransactionDate.SelectedDate;
            entity.RefferenceNumber = this.txtRefferenceNumber.Text;
            entity.Description = this.txtDescription.Text;
            entity.BudgetingCode = this.txtBudgeting.Text;
            entity.LastUpdateDateTime = DateTime.Now;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.Save();

            grdVoucherEntryItem.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
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
            auditLogFilter.PrimaryKeyData = string.Format("JournalId='{0}'", this.JournalId);
            auditLogFilter.TableName = "JournalTransactions";
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            if (this.JournalId == 0 && (true == (args.IsCancel = true)))
                return;

            JournalTransactions entity = JournalTransactions.Get(this.JournalId);
            if (entity == null)
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            else
            {
                if (entity.IsPosted.Value)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return;
                }

                if (!Helper.ValidatePeriode(entity.TransactionDate.Value))
                {
                    args.MessageText = AppConstant.Message.TRANSACTION_DATE_HAS_CLOSED;
                    args.IsCancel = true;
                    return;
                }
            }

            // auto cash entry
            if (entity.JournalType == JournalType.Payment)
            {
                try
                {
                    var tp = new TransPayment();
                    if (!tp.LoadByPrimaryKey(entity.RefferenceNumber))
                        throw new Exception("Payment number of " + entity.RefferenceNumber + " not found");
                    var tpItems = new TransPaymentItemCollection();
                    tpItems.Query.Where(tpItems.Query.PaymentNo == tp.PaymentNo);
                    tpItems.LoadAll();

                    var cashID = JournalTransactions.AutoCashEntryOnPaymentReceive(tp, tpItems, entity, AppSession.UserLogin.UserID);
                }
                catch (Exception ex)
                {
                    args.MessageText = ex.Message;
                    args.IsCancel = true;
                }
            }

            string trNumber = string.Empty;
            bool ret = JournalTransactions.MarkStatusAsPosting(this.JournalId, AppSession.UserLogin.UserID, out trNumber);
            if (!ret)
            {
                args.MessageText = string.Format("Approval failed for this transaction number: {0}. Please check again your journal.", trNumber);
                args.IsCancel = true;
            }

            BkuJournalTransactions.AddBkuJournalByJournalTransactions(this.JournalId, AppSession.UserLogin.UserID);
        }

        public override bool OnGetStatusMenuUnApprovalEnabled()
        {
            return Helper.ValidatePeriode(txtTransactionDate.SelectedDate.Value);
        }
        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            if (this.JournalId == 0 && (true == (args.IsCancel = true)))
                return;

            JournalTransactions entity = JournalTransactions.Get(this.JournalId);
            if (entity == null)
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            else
            {
                if (!entity.IsPosted.Value)
                {
                    //args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return;
                }

                if (!Helper.ValidatePeriode(entity.TransactionDate.Value))
                {
                    args.MessageText = AppConstant.Message.TRANSACTION_DATE_HAS_CLOSED;
                    args.IsCancel = true;
                    return;
                }
            }


            string trNumber = string.Empty;
            bool ret = JournalTransactions.RemovePostingStatus(this.JournalId, AppSession.UserLogin.UserID, out trNumber);
            if (!ret)
            {
                args.MessageText = string.Format("UnApproval failed for this transaction number: {0}. Please check again your journal.", trNumber);
                args.IsCancel = true;
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            if (this.JournalId == 0 && (true == (args.IsCancel = true)))
                return;

            JournalTransactions entity = JournalTransactions.Get(this.JournalId);
            if (entity == null)
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            if (entity.IsVoid.Value || entity.IsPosted.Value)
            {
                args.MessageText = "Unable to void this transaction.";
                args.IsCancel = true;
                return;
            }

            entity.IsVoid = true;
            entity.VoidDate = DateTime.Now.Date;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;
            entity.Save();
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            if (this.JournalId == 0 && (true == (args.IsCancel = true)))
                return;

            JournalTransactions entity = JournalTransactions.Get(this.JournalId);
            if (entity == null)
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            if (!entity.IsVoid.Value || entity.IsPosted.Value)
            {
                args.MessageText = "Unable to unvoid this transaction.";
                args.IsCancel = true;
                return;
            }

            entity.IsVoid = false;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;
            entity.Save();
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            if (oldVal == AppEnum.DataMode.New && this.JournalId == 0)
                Response.Redirect("VoucherEntryList.aspx");

            RefreshCommandItemGrid(oldVal, newVal);
            txtRefferenceNumber.Visible = false;
            txtJournalCode.Visible = txtTransactionNumber.Visible = (newVal == AppEnum.DataMode.New);
            lblJournalCode.Visible = lblTransactionNumber.Visible = (newVal != AppEnum.DataMode.New);
            btnRecalculate.Visible = newVal == AppEnum.DataMode.Read && !chkIsApproved.Checked && !chkIsVoid.Checked && !string.IsNullOrEmpty(txtRefferenceNumber.Text);

            switch (Request.QueryString["jt"])
            {
                case "25": //AP 
                case "26": //AP Payment
                    mainiFrame.Attributes["src"] = "CheckDetailTransaction/APPaymentTypeDetail.aspx?src=tab&ivd=" + Request.QueryString["ivd"] + "&md=" + newVal;
                    break;
            }
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            JournalTransactions entity = new JournalTransactions();
            if (parameters.Length > 0)
            {
                string id = parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity = JournalTransactions.Get(Convert.ToInt32(id));
            }
            else
            {
                entity = JournalTransactions.Get(this.JournalId);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            JournalTransactions e = (JournalTransactions)entity;
            txtJournalCode.Text = e.JournalCode;
            lblJournalCode.Text = e.JournalCode;
            txtTransactionNumber.Text = e.TransactionNumber;
            lblTransactionNumber.Text = e.TransactionNumber;
            txtTransactionDate.SelectedDate = (e.TransactionDate.HasValue ? e.TransactionDate : DateTime.Now);
            txtRefferenceNumber.Text = e.RefferenceNumber;
            lblRefferenceNumber.Text = e.RefferenceNumber;

            if (!string.IsNullOrEmpty(e.RefferenceNumber) && e.RefferenceNumber.Contains("POR"))
            {
                // create link
                divRefferenceNumber.InnerHtml = string.Format(
                    "<a href=\"javascript:void();\" onclick=\"javascript:ShowDialogFullReference('{0}')\">{0}</a>",
                    e.RefferenceNumber
                );
            }

            lblJournalType.Text = e.JournalType;
            txtDescription.Text = e.Description;
            txtBudgeting.Text = e.BudgetingCode;
            chkIsApproved.Checked = e.IsPosted ?? false;
            chkIsVoid.Checked = e.IsVoid ?? false;

            lblMessage.Text = string.Empty;
            if ((e.JournalId ?? 0) != 0)
            {
                var msg = new JournalMessage();
                if (msg.LoadByPrimaryKey(e.JournalId ?? 0))
                {
                    lblMessage.Text = msg.Message.Replace(System.Environment.NewLine, "<br />");
                }
            }
            OnGetStatusMenuApproval();

            OnGetStatusMenuVoid();

            this.GenerateGrid();
        }

        public override bool OnGetStatusMenuVoid()
        {
            btnRecalculate.Visible = !chkIsApproved.Checked && !chkIsVoid.Checked && !string.IsNullOrEmpty(txtRefferenceNumber.Text);
            return !chkIsVoid.Checked;
        }

        public override bool OnGetStatusMenuEdit()
        {
            btnRecalculate.Visible = false;
            //return !this.chkIsApproved.Checked;
            return true;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            btnRecalculate.Visible = !chkIsApproved.Checked && !chkIsVoid.Checked && !string.IsNullOrEmpty(txtRefferenceNumber.Text);
            return !chkIsApproved.Checked;
        }

        public override bool OnGetStatusMenuDelete()
        {
            return !this.chkIsApproved.Checked;
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            switch (programID)
            {
                case AppConstant.Report.JournalVoucher:
                    printJobParameters.AddNew("pEntityId", this.JournalId.ToString());
                    break;
            }
        }
        #endregion

        #region Private Method Standard
        private void SetEntityValue(JournalTransactions entity)
        {
            entity.JournalCode = this.txtJournalCode.Text;
            entity.JournalType = JournalType.General;
            entity.TransactionNumber = this.txtTransactionNumber.Text;
            entity.TransactionDate = this.txtTransactionDate.SelectedDate;
            entity.RefferenceNumber = this.txtRefferenceNumber.Text;
            entity.Description = this.txtDescription.Text;
            entity.BudgetingCode = this.txtBudgeting.Text;
            entity.IsPosted = false;
            entity.DateCreated = entity.LastUpdateDateTime = DateTime.Now;
            entity.CreatedBy = entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
        }

        private bool SaveEntity(JournalTransactions entity)
        {
            bool ret = JournalTransactions.AddNew(entity);
            this.JournalId = entity.JournalId.Value; // to reset the screen
            return ret;
        }

        private void MoveRecord(bool isNextRecord)
        {
            JournalTransactionsQuery q = new JournalTransactionsQuery();
            q.es.Top = 1;
            if (isNextRecord)
            {
                q.Where(q.JournalCode == this.txtJournalCode.Text, q.TransactionNumber > txtTransactionNumber.Text);
                q.OrderBy(q.TransactionNumber.Ascending);
            }
            else
            {
                q.Where(q.JournalCode == this.txtJournalCode.Text, q.TransactionNumber < txtTransactionNumber.Text);
                q.OrderBy(q.TransactionNumber.Descending);
            }

            JournalTransactions entity = new JournalTransactions();
            if (entity.Load(q))
            {
                this.JournalId = entity.JournalId.Value;
                this.OnPopulateEntryControl(entity);
                this.grdVoucherEntryItem.Rebind();
            }
        }
        #endregion

        #region Record Detail Method Function
        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdVoucherEntryItem.Columns[0].Visible = isVisible;
            grdVoucherEntryItem.Columns[grdVoucherEntryItem.Columns.Count - 1].Visible = isVisible;
            grdVoucherEntryItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            if (!isVisible)
            {
                foreach (var item in grdVoucherEntryItem.MasterTableView.GetItems())
                {
                    item.Visible = false;
                }
            }
            grdVoucherEntryItem.Rebind();
        }

        protected void GenerateGrid()
        {
            if (!this.IsPostBack)
            {
                GridSortExpression sortExpr = new GridSortExpression();
                sortExpr.FieldName = JournalTransactionDetailsMetadata.ColumnNames.DetailId;
                sortExpr.SortOrder = GridSortOrder.Ascending;

                grdVoucherEntryItem.MasterTableView.SortExpressions.AddSortExpression(sortExpr);
                grdVoucherEntryItem.MasterTableView.SortExpressions.AllowNaturalSort = false;
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (GridSortExpression e in grdVoucherEntryItem.MasterTableView.SortExpressions)
            {
                sb.AppendFormat("{0}^{1}", this.grdVoucherEntryItem.MasterTableView.SortExpressions[0].FieldName, this.grdVoucherEntryItem.MasterTableView.SortExpressions[0].SortOrder);
                sb.Append(",");
            }

            double totalDebit = 0;
            double totalCredit = 0;
            int totalCount = JournalTransactionDetails.TotalCount(this.JournalId);

            grdVoucherEntryItem.VirtualItemCount = totalCount;

            JournalTransactionDetailsCollection en = JournalTransactionDetails.GetAllForTransactionWithPaging(this.JournalId, this.grdVoucherEntryItem.CurrentPageIndex, this.grdVoucherEntryItem.PageSize);
            List<GridItem> items = new List<GridItem>();

            foreach (JournalTransactionDetails e in en)
                items.Add(new GridItem(e));

            this.grdVoucherEntryItem.DataSource = items;

            JournalTransactionDetails.GetTotal(this.JournalId, out totalDebit, out totalCredit);
            txtTotalAmountCredit.Value = totalCredit;
            txtTotalAmountDebit.Value = totalDebit;
            txtSelisih.Value = Math.Abs(totalCredit - totalDebit);
        }

        protected void grdVoucherEntryItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            this.GenerateGrid();
        }

        protected void grdVoucherEntryItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            VoucherEntryItemDetail ctl = (VoucherEntryItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (ctl == null) return;

            if (!this.IsEnableEdit(this.JournalId))
            {
                e.Canceled = true;
                return;
            }

            JournalTransactionDetails entity = JournalTransactionDetails.Get(this.JournalId, ctl.DetailId);
            if (entity != null)
            {
                entity.ChartOfAccountId = ctl.ChartOfAccountId;
                entity.Debit = ctl.Debit;
                entity.Credit = ctl.Credit;
                entity.Description = ctl.Description;
                entity.SubLedgerId = ctl.SubLedgerId;
                entity.RegistrationNo = ctl.RegistrationNo;
                entity.DocumentNumber = ctl.DocumentNumber;
                entity.Save();
            }
            this.GenerateGrid();

        }

        protected void grdVoucherEntryItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            if (!this.IsEnableEdit(this.JournalId))
            {
                e.Canceled = true;
                return;
            }

            int id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][JournalTransactionDetailsMetadata.ColumnNames.DetailId]);
            JournalTransactionDetails entity = JournalTransactionDetails.Get(this.JournalId, id);
            if (entity != null)
            {
                entity.MarkAsDeleted();
                entity.Save();
            }
            this.GenerateGrid();
        }

        protected void grdVoucherEntryItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
            {
                e.Canceled = true;
                return;
            }

            VoucherEntryItemDetail ctl = (VoucherEntryItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (ctl == null) return;

            int accId = this.validateChartOfAccount(ctl);
            if (accId == 0)
            {
                e.Canceled = true;
                return;
            }

            bool newlyCreated = false;
            if (this.JournalId == 0)
            {
                bool isValid = false;

                JournalCodes journalCode = JournalCodes.Get(txtJournalCode.Text);
                isValid = ((journalCode != null) && (journalCode.IsEnabled ?? false));
                if (!isValid)
                {
                    //args.MessageText = "Invalid Journal Code";
                    e.Canceled = true;
                    return;
                }



                if (!Helper.ValidatePeriode(txtTransactionDate.SelectedDate.Value))
                {
                    //args.MessageText = "Invalid Transaction Date";
                    e.Canceled = true;
                    return;
                }

                JournalTransactions entity = new JournalTransactions();
                entity.AddNew();
                SetEntityValue(entity);
                if (!this.SaveEntity(entity))
                {
                    //args.MessageText = "Unable to create new transaction please try again.";
                    e.Canceled = true;
                    return;
                }
                OnPopulateEntryControl(entity);
                OnDataModeChanged(AppEnum.DataMode.New, AppEnum.DataMode.Edit);

                newlyCreated = true;
            }

            if (!newlyCreated && !this.IsEnableEdit(this.JournalId))
            {
                e.Canceled = true;
                return;
            }

            JournalTransactionDetails detail = new JournalTransactionDetails();
            detail.JournalId = this.JournalId;
            detail.ChartOfAccountId = accId;
            detail.Debit = ctl.Debit;
            detail.Credit = ctl.Credit;
            detail.Description = ctl.Description;
            //edit
            detail.RegistrationNo = ctl.RegistrationNo;
            detail.SubLedgerId = ctl.SubLedgerId;
            detail.DocumentNumber = ctl.DocumentNumber;

            detail.AddNew();
            detail.Save();

            e.Canceled = true;
            //grdVoucherEntryItem.Rebind();
            this.GenerateGrid();
            grdVoucherEntryItem.Rebind();
        }

        private int validateChartOfAccount(VoucherEntryItemDetail ctl)
        {
            try
            {
                ChartOfAccounts entity = null;
                // try to extract selected value from radcombo box first
                int selectedValue = ctl.ChartOfAccountId;
                if (selectedValue != 0)
                    entity = ChartOfAccounts.Get(selectedValue);
                else
                    entity = ChartOfAccounts.Get(ctl.ChartOfAccountCode);

                if (entity != null && ((entity.IsDetail.Value) /*&& (entity.AccountLevel == 4)*/))
                    return entity.ChartOfAccountId.Value;
            }
            catch
            {
            }

            return 0;
        }

        private bool IsEnableEdit(int journalId)
        {
            if (journalId > 0)
            {
                JournalTransactions entity = JournalTransactions.Get(journalId);
                return (Helper.ValidatePeriode(entity.TransactionDate.Value) && !entity.IsPosted.Value && !entity.IsVoid.Value);
            }
            return false;
        }
        #endregion

        public class GridItem
        {
            private readonly JournalTransactionDetails entity;

            public GridItem(JournalTransactionDetails Entity)
            {
                this.entity = Entity;
            }

            public int DetailId
            {
                get { return this.entity.DetailId.Value; }
            }
            public string ChartOfAccountCode
            {
                get { return this.entity.ChartOfAccountCode; }
            }
            public string ChartOfAccountName
            {
                get { return this.entity.ChartOfAccountName; }
            }
            public int SubLedgerId
            {
                get { return this.entity.SubLedgerId.Value; }
            }
            public string SubLedgerName
            {
                get { return this.entity.SubLedgerName; }
            }
            public string DocumentNumber
            {
                get { return this.entity.DocumentNumber; }
            }
            public decimal Debit
            {
                get { return this.entity.Debit.Value; }
            }
            public decimal Credit
            {
                get { return this.entity.Credit.Value; }
            }
            public string Description
            {
                get { return this.entity.Description; }
            }
            public string RegistrationNo
            {
                get { return this.entity.RegistrationNo; }
            }

            public string JournalType
            {
                get { return this.entity.JournalType; }
            }

            public string HeaderDescription
            {
                get { return this.entity.HeaderDescription; }
            }

            public string JournalGrouping
            {
                get { return this.entity.JournalGrouping; }
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(source is RadGrid))
                return;

            switch (eventArgument)
            {
                case "36":
                    Validate();
                    if (!IsValid)
                        return;
                    JournalIncomeFromIPRTransfer();
                    break;
                case "02": //Income (Registrasi, Service Unit Entry & JO Realization)
                    Validate();
                    if (!IsValid)
                        return;
                    JournalIncome();
                    break;
                case "03": //Prescription
                    Validate();
                    if (!IsValid)
                        return;
                    JournalPrescription();
                    break;
                case "04": //Prescription return
                    Validate();
                    if (!IsValid)
                        return;
                    JournalPrescriptionReturn();
                    break;
                case "05": //Spectacle Prescription
                    break;
                case "07": //Payment Return
                    Validate();
                    if (!IsValid)
                        return;
                    JournalPaymentReturn();
                    break;
                case "08": //Down Payment Return
                    Validate();
                    if (!IsValid)
                        return;
                    JournalDownPaymentReturn();
                    break;
                case "09": //Down Payment
                    Validate();
                    if (!IsValid)
                        return;
                    JournalDownPayment();
                    break;
                case "13":
                case "10": //Payment
                    Validate();
                    if (!IsValid)
                        return;
                    JournalPayment();
                    break;
                case "11": //AR Payment
                    Validate();
                    if (!IsValid)
                        return;
                    JournalArPayment();
                    break;
                case "12": //Cash Transaction
                    break;
                case "14":
                    Validate();
                    if (!IsValid)
                        return;
                    JournalInvoice();
                    break;
                case "15": //PO Received 
                    Validate();
                    if (!IsValid)
                        return;
                    JournalPoReceived();
                    break;
                case "16": //PO Returned 
                    Validate();
                    if (!IsValid)
                        return;
                    JournalPoReturned();
                    break;
                case "17": //Consignment Received
                    break;
                case "19": //Consignment Invoicing
                    break;
                case "20": //Distribution
                    Validate();
                    if (!IsValid)
                        return;
                    JournalDistribution();
                    break;
                case "22": //Inventory Issue
                    Validate();
                    if (!IsValid)
                        return;
                    JournalInventoryIssue();
                    break;
                case "24": //Grant Receive
                    Validate();
                    if (!IsValid)
                        return;
                    JournalGrantReceived();
                    break;
                case "25": //AP
                    Validate();
                    if (!IsValid)
                        return;
                    JournalAp();
                    break;
                case "26": //AP Payment
                    Validate();
                    if (!IsValid)
                        return;
                    JournalApPayment();
                    break;
                case "27": //Paramedic Fee Payment
                    Validate();
                    if (!IsValid)
                        return;
                    JournalParamedicFeePayment();
                    break;
                case "28": //Paramedic Fee Verification
                    Validate();
                    if (!IsValid)
                        return;
                    JournalParamedicFeeVerification();
                    break;
                case "32": //Inventory Stock Adjustment
                    Validate();
                    if (!IsValid)
                        return;
                    JournalInventoryStockAdjustment();
                    break;
                case "33": //Inventory Stock Opname
                    Validate();
                    if (!IsValid)
                        return;
                    JournalInventoryStockOpname();
                    break;
                case "34": //Inventory Production
                    Validate();
                    if (!IsValid)
                        return;
                    JournalInventoryProduction();
                    break;
                case "35": //Patient Receivable
                    Validate();
                    if (!IsValid)
                        return;
                    JournalPatientReceivable();
                    break;
                case "37": //Cashier Correction
                    Validate();
                    if (!IsValid)
                        return;
                    JournalPaymentCashierCorrection();
                    break;
                case "38": //Payroll
                    Validate();
                    if (!IsValid)
                        return;
                    JournalPayroll(1);
                    break;
                case "39": //THR
                    Validate();
                    if (!IsValid)
                        return;
                    JournalPayroll(2);
                    break;
                case "40": //Sales
                    Validate();
                    if (!IsValid)
                        return;
                    JournalSales();
                    break;
                case "41": //Sales return
                    Validate();
                    if (!IsValid)
                        return;
                    JournalSalesReturned();
                    break;
                case "42": //AR Customer
                    Validate();
                    if (!IsValid)
                        return;
                    JournalARCustomer();
                    break;
                case "43": //AR Payment Customer
                    Validate();
                    if (!IsValid)
                        return;
                    JournalARCustomerPayment();
                    break;
                case "44": //AssetAuction
                    Validate();
                    if (!IsValid)
                        return;
                    JournalAssets("44");
                    break;
                case "45": //AssetDestruction
                    Validate();
                    if (!IsValid)
                        return;
                    JournalAssets("45");
                    break;
                case "46": //AssetMovement
                    Validate();
                    if (!IsValid)
                        return;
                    JournalAssets("46");
                    break;
                case "47": //Closing Visit DP
                    Validate();
                    if (!IsValid)
                        return;
                    JournalClosingVisitDp("47");
                    break;
                case "48": //Patient Receivable
                    Validate();
                    if (!IsValid)
                        return;
                    JournalParamedicFeePayable();
                    break;
            }
            var entity = new JournalTransactions();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(lblJournalId.Text)))
            {
                OnPopulateEntryControl(entity);
                if (chkIsApproved.Checked) {
                    RefreshMenuStatus();
                }
            }
                
            grdVoucherEntryItem.Rebind();
        }

        private void JournalIncomeFromIPRTransfer()
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRefferenceNumber.Text);

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(reg.ServiceUnitID);

            var costs = new CostCalculationCollection();
            costs.Query.Where(costs.Query.RegistrationNo == reg.RegistrationNo);
            costs.LoadAll();

            int? journalId1 = JournalTransactions.AddNewInpatentTransferJournalTemporary(reg, unit, costs, "SU", AppSession.UserLogin.UserID, Convert.ToInt32(Request.QueryString["ivd"]));
            lblJournalId.Text = journalId1.ToString();

        }

        private void JournalIncome()
        {
            int? journalId = 0;
            if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary)) {
                var JournalID = JournalIncome(Convert.ToInt32(Request.QueryString["ivd"]), txtRefferenceNumber.Text);
                if (JournalId > 0) lblJournalId.Text = JournalId.ToString();
            }
            else {
                var chargesHd = new TransCharges();
                if (chargesHd.LoadByPrimaryKey(txtRefferenceNumber.Text))
                {
                    var compDts = new TransChargesItemCompCollection();
                    compDts.Query.Where(compDts.Query.TransactionNo == chargesHd.TransactionNo);
                    compDts.LoadAll();

                    var reg = new Registration();
                    reg.LoadByPrimaryKey(chargesHd.RegistrationNo);

                    var costs = new CostCalculationCollection();
                    costs.Query.Where(costs.Query.TransactionNo == chargesHd.TransactionNo);
                    costs.LoadAll();

                    var unit = new ServiceUnit();
                    unit.LoadByPrimaryKey(chargesHd.ToServiceUnitID);

                    
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                    {
                        if (chargesHd.IsCorrection == false)
                            journalId = JournalTransactions.AddNewIncomeJournalTemporary(chargesHd, compDts, reg, unit, costs, "SU", AppSession.UserLogin.UserID, Convert.ToInt32(Request.QueryString["ivd"]));
                        else
                            journalId = JournalTransactions.AddNewIncomeCorrectionJournalTemporary(chargesHd, compDts, reg, unit, costs, "SC", chargesHd.LastUpdateByUserID, Convert.ToInt32(Request.QueryString["ivd"]));
                    }
                    else
                    {
                        if (chargesHd.IsCorrection == false)
                            journalId = JournalTransactions.AddNewIncomeJournal(chargesHd, compDts, reg, unit, costs, "SU", AppSession.UserLogin.UserID, Convert.ToInt32(Request.QueryString["ivd"]));
                        else
                            journalId = JournalTransactions.AddNewIncomeCorrectionJournal(chargesHd, compDts, reg, unit, costs, "SC", chargesHd.LastUpdateByUserID, false, Convert.ToInt32(Request.QueryString["ivd"]));
                    }
                    lblJournalId.Text = journalId.ToString();
                }
            }
        }

        public static int? JournalIncome(int JournalID, string RefNo)
        {
            if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
            {
                if (RefNo.ToUpper().Contains("REG"))
                {
                    if (JournalID != 0)
                    {
                        var jr = new JournalTransactions();
                        if (jr.LoadByPrimaryKey(JournalID))
                        {
                            return JournalTransactions.AddNewPatientBillingRecalculation(BusinessObject.JournalType.Income, RefNo, jr.TransactionDate.Value, AppSession.UserLogin.UserID, JournalID);
                        }
                        else
                        {
                            return JournalTransactions.AddNewPatientBillingRecalculation(BusinessObject.JournalType.Income, RefNo, DateTime.Now, AppSession.UserLogin.UserID, JournalID);
                        }
                    }
                    else
                    {
                        return JournalTransactions.AddNewPatientBillingRecalculation(BusinessObject.JournalType.Income, RefNo, DateTime.Now, AppSession.UserLogin.UserID, JournalID);
                    }
                }
                else {
                    // accru tanpa temp
                    var jid = JournalTransactions.AddNewPatientIncomeAccrual(BusinessObject.JournalType.Income, RefNo, AppSession.UserLogin.UserID, JournalID);

                    
                    var tc = new TransCharges();
                    if (tc.LoadByPrimaryKey(RefNo)) {
                        // kalau reprocessing jurnal paket, jurnal detail paket juga reprocessing
                        if (tc.IsPackage ?? false) {
                            var tcpColl = new TransChargesCollection();
                            tcpColl.Query.Where(tcpColl.Query.PackageReferenceNo == tc.TransactionNo);
                            if (tcpColl.LoadAll())
                            {
                                foreach (var tcp in tcpColl) {
                                    var jtcpColl = new JournalTransactionsCollection();
                                    jtcpColl.Query.Where(jtcpColl.Query.RefferenceNumber == tcp.TransactionNo, jtcpColl.Query.IsVoid == 0);
                                    if (jtcpColl.LoadAll()) {
                                        // harusnya cuma satu row

                                        // unapprove dulu
                                        jtcpColl.First().IsPosted = false;
                                        jtcpColl.Save();

                                        JournalTransactions.AddNewPatientIncomeAccrual(BusinessObject.JournalType.Income, tcp.TransactionNo, AppSession.UserLogin.UserID, jtcpColl.First().JournalId.Value);
                                    }
                                }
                            }
                        }

                        // jurnal recalculate billing juga harus direjournal, untuk pasien reg ini dan tgl jurnal recalculate >= tgl jurnal jid
                        if ((jid ?? 0) > 0)
                        {
                            var jr = new JournalTransactions();
                            if (jr.LoadByPrimaryKey(jid.Value))
                            {
                                if (!string.IsNullOrEmpty(jr.RefferenceNumber))
                                {
                                    var regs = MergeBilling.GetFullMergeRegistration(tc.RegistrationNo);
                                    if (regs.Length > 0)
                                    {
                                        // cari jurnal recalculate
                                        var jrrColl = new JournalTransactionsCollection();
                                        jrrColl.Query.Where(
                                            jrrColl.Query.RefferenceNumber.In(regs),
                                            jrrColl.Query.JournalType == BusinessObject.JournalType.Income,
                                            jrrColl.Query.TransactionDate >= jr.TransactionDate,
                                            jrrColl.Query.IsVoid == false
                                        );
                                        jrrColl.Query.OrderBy(jrrColl.Query.TransactionDate.Ascending);
                                        if (jrrColl.LoadAll())
                                        {
                                            foreach (var jrr in jrrColl)
                                            {
                                                jrr.IsPosted = false;
                                            }
                                            jrrColl.Save();

                                            // rejournal rekal billing
                                            foreach (var jrr in jrrColl)
                                            {
                                                JournalIncome(jrr.JournalId.Value, jrr.RefferenceNumber);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    return jid;
                }
            }
            else {
                var chargesHd = new TransCharges();
                if (chargesHd.LoadByPrimaryKey(RefNo))
                {
                    var compDts = new TransChargesItemCompCollection();
                    compDts.Query.Where(compDts.Query.TransactionNo == chargesHd.TransactionNo);
                    compDts.LoadAll();

                    var reg = new Registration();
                    reg.LoadByPrimaryKey(chargesHd.RegistrationNo);

                    var costs = new CostCalculationCollection();
                    costs.Query.Where(costs.Query.TransactionNo == chargesHd.TransactionNo);
                    costs.LoadAll();

                    var unit = new ServiceUnit();
                    unit.LoadByPrimaryKey(chargesHd.ToServiceUnitID);

                    int? journalId = 0;
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                    {
                        if (chargesHd.IsCorrection == false)
                            return JournalTransactions.AddNewIncomeJournalTemporary(chargesHd, compDts, reg, unit, costs, "SU", AppSession.UserLogin.UserID, JournalID);
                        else
                            return JournalTransactions.AddNewIncomeCorrectionJournalTemporary(chargesHd, compDts, reg, unit, costs, "SC", chargesHd.LastUpdateByUserID, JournalID);
                    }
                    else
                    {
                        if (chargesHd.IsCorrection == false)
                            return JournalTransactions.AddNewIncomeJournal(chargesHd, compDts, reg, unit, costs, "SU", AppSession.UserLogin.UserID, JournalID);
                        else
                            return JournalTransactions.AddNewIncomeCorrectionJournal(chargesHd, compDts, reg, unit, costs, "SC", chargesHd.LastUpdateByUserID, false, JournalID);
                    }
                }
            }
            return 0;
        }

        private void JournalPrescription()
        {
            var entity = new TransPrescription();
            if (entity.LoadByPrimaryKey(txtRefferenceNumber.Text))
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(entity.RegistrationNo);

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(entity.ServiceUnitID);

                var costs = new CostCalculationCollection();
                costs.Query.Where(costs.Query.TransactionNo == entity.PrescriptionNo);
                costs.LoadAll();

                int? journalId = 0;
                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                {
                    if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                    {
                        journalId = JournalIncome(Convert.ToInt32(Request.QueryString["ivd"]), txtRefferenceNumber.Text);
                    }
                    else {
                        journalId = JournalTransactions.AddNewPrescriptionJournalTemporaryNetto(entity, reg, unit, costs, "RS",
                                                                                      AppSession.UserLogin.UserID,
                                                                                      Convert.ToInt32(Request.QueryString["ivd"]));
                    }
                }
                else
                {
                    journalId = JournalTransactions.AddNewPrescriptionJournal(entity, reg, unit, costs, "RS",
                                                               AppSession.UserLogin.UserID,
                                                               Convert.ToInt32(Request.QueryString["ivd"]));
                }
                lblJournalId.Text = journalId.ToString();
            }
        }

        public static int? JournalPrescription(int JournalID, string RefNo)
        {
            var entity = new TransPrescription();
            if (entity.LoadByPrimaryKey(RefNo))
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(entity.RegistrationNo);

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(entity.ServiceUnitID);

                var costs = new CostCalculationCollection();
                costs.Query.Where(costs.Query.TransactionNo == entity.PrescriptionNo);
                costs.LoadAll();

                int? journalId = 0;
                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                {
                    if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                    {
                        return JournalIncome(JournalID, RefNo);
                    }
                    else {
                        return JournalTransactions.AddNewPrescriptionJournalTemporaryNetto(entity, reg, unit, costs, "RS",
                                                                                       AppSession.UserLogin.UserID,
                                                                                       JournalID);
                    }
                }
                else
                {
                    return JournalTransactions.AddNewPrescriptionJournal(entity, reg, unit, costs, "RS",
                                                               AppSession.UserLogin.UserID,
                                                               JournalID);
                }
            }
            return 0;
        }

        private void JournalPrescriptionReturn()
        {
            var entity = new TransPrescription();
            if (entity.LoadByPrimaryKey(txtRefferenceNumber.Text))
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(entity.RegistrationNo);

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(entity.ServiceUnitID);

                var costs = new CostCalculationCollection();
                costs.Query.Where(costs.Query.TransactionNo == entity.PrescriptionNo);
                costs.LoadAll();

                if (!AppSession.Parameter.IsUsingIntermBill)
                {
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsPemisahanCOAUangRacikan) == "1")
                    {
                        int? journalId =
                            JournalTransactions.AddNewPrescriptionReturnJournalWithSeparationPersonalizedRecipeMoney(
                                entity, reg, unit, costs, "RS", AppSession.UserLogin.UserID,
                                Convert.ToInt32(Request.QueryString["ivd"]));
                        lblJournalId.Text = journalId.ToString();
                    }
                    else
                    {
                        int? journalId = JournalTransactions.AddNewPrescriptionReturnJournal(entity, reg, unit, costs, "RS",
                                                                                             AppSession.UserLogin.UserID,
                                                                                             Convert.ToInt32(Request.QueryString["ivd"]));
                        lblJournalId.Text = journalId.ToString();
                    }
                }
                else
                {
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                    {
                        if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                        {
                            JournalIncome(Convert.ToInt32(Request.QueryString["ivd"]), txtRefferenceNumber.Text);
                        }
                        else {
                            var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');
                            if (type.Contains(reg.SRRegistrationType))
                            {
                                int? journalId = JournalTransactions.AddNewPrescriptionReturnJournalTemporaryNetto(entity, reg, unit,
                                    costs.Where(c => c.TransactionNo == entity.PrescriptionNo), "RS", AppSession.UserLogin.UserID, Convert.ToInt32(Request.QueryString["ivd"]));
                            }
                        }
                    }
                }
            }
        }

        public static int? JournalPrescriptionReturn(int JournalID, string RefNo)
        {
            var entity = new TransPrescription();
            if (entity.LoadByPrimaryKey(RefNo))
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(entity.RegistrationNo);

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(entity.ServiceUnitID);

                var costs = new CostCalculationCollection();
                costs.Query.Where(costs.Query.TransactionNo == entity.PrescriptionNo);
                costs.LoadAll();
                if (!AppSession.Parameter.IsUsingIntermBill)
                {
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsPemisahanCOAUangRacikan) == "1")
                    {
                        return
                            JournalTransactions.AddNewPrescriptionReturnJournalWithSeparationPersonalizedRecipeMoney(
                                entity, reg, unit, costs, "RS", AppSession.UserLogin.UserID,
                                JournalID);
                        //lblJournalId.Text = journalId.ToString();
                    }
                    else
                    {
                        return JournalTransactions.AddNewPrescriptionReturnJournal(entity, reg, unit, costs, "RS",
                                                                                             AppSession.UserLogin.UserID,
                                                                                             JournalID);
                        //lblJournalId.Text = journalId.ToString();
                    }
                }
                else
                {
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                    {
                        if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                        {
                            return JournalIncome(JournalID, RefNo);
                        }
                        else {
                            var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');
                            if (type.Contains(reg.SRRegistrationType))
                            {
                                return JournalTransactions.AddNewPrescriptionReturnJournalTemporaryNetto(entity, reg, unit,
                                    costs.Where(c => c.TransactionNo == entity.PrescriptionNo), "RS", AppSession.UserLogin.UserID, JournalID);
                            }
                        }
                    }
                }
            }
            return 0;
        }

        private void JournalPaymentReturn()
        {
            var JournalID = JournalPaymentReturn(Convert.ToInt32(Request.QueryString["ivd"]), txtRefferenceNumber.Text);
            if (JournalId > 0) lblJournalId.Text = JournalId.ToString();
        }
        public static int? JournalPaymentReturn(int JournalID, string RefNo)
        {
            var entity = new TransPayment();
            if (entity.LoadByPrimaryKey(RefNo))
            {
                var registration = new Registration();
                registration.LoadByPrimaryKey(entity.RegistrationNo);

                var transPaymentItems = new TransPaymentItemCollection();
                transPaymentItems.Query.Where(transPaymentItems.Query.PaymentNo == entity.PaymentNo);
                transPaymentItems.LoadAll();

                var IsJournalVoid = false;
                if (JournalID == 0)
                {
                    IsJournalVoid = !(entity.IsApproved ?? false);
                }
                else
                {
                    // lihat dulu ini reproses journal payment return atau journal void payment return
                    var j = new JournalTransactions();
                    if (j.LoadByPrimaryKey(JournalID))
                    {
                        IsJournalVoid = j.Description.ToUpper().IndexOf("VOID") >= 0;
                    }
                }

                //if (entity.IsApproved == true)
                if (!IsJournalVoid)
                {
                    // function ini utk retur payment biasa
                    int? journalId =
                        JournalTransactions.AddNewPaymentCorrectionJournalCashBased(JournalType.PaymentReturn,
                                                                                    entity, registration,
                                                                                    transPaymentItems, "TP",
                                                                                    entity.PaymentReferenceNo,
                                                                                    AppSession.UserLogin.UserID,
                                                                                    JournalID);
                    return journalId;
                }
                else
                {
                    // function ini utk retur payment biasa
                    return JournalTransactions.AddNewPaymentCorrectionJournalVoidCashBased(
                        entity, "TP", AppSession.UserLogin.UserID, JournalID);
                }
            }
            return 0;
        }

        private void JournalDownPaymentReturn()
        {
            var JournalID = JournalDownPaymentReturn(Convert.ToInt32(Request.QueryString["ivd"]), txtRefferenceNumber.Text);
            if (JournalId > 0) lblJournalId.Text = JournalId.ToString();
        }
        public static int? JournalDownPaymentReturn(int JournalID, string RefNo)
        {
            var entity = new TransPayment();
            if (entity.LoadByPrimaryKey(RefNo))
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(entity.RegistrationNo);

                var transPaymentItems = new TransPaymentItemCollection();
                transPaymentItems.Query.Where(transPaymentItems.Query.PaymentNo == entity.PaymentNo);
                transPaymentItems.LoadAll();

                var IsJournalVoid = false;
                if (JournalID == 0)
                {
                    IsJournalVoid = !(entity.IsApproved ?? false);
                }
                else
                {
                    // lihat dulu ini reproses journal dp return atau journal void dp return
                    var j = new JournalTransactions();
                    if (j.LoadByPrimaryKey(JournalID))
                    {
                        IsJournalVoid = j.JournalIdRefference.HasValue;
                    }
                }

                if (!IsJournalVoid)
                {
                    int? journalId = JournalTransactions.AddNewDownPaymentReturnJournal(BusinessObject.JournalType.DownPayment, entity,
                                                                       reg, transPaymentItems, "DR",
                                                                       AppSession.UserLogin.UserID,
                                                                       JournalID);
                    return journalId;
                }
                else
                {
                    int? journalId =
                            JournalTransactions.AddNewDownPaymentReturnCorrectionJournal(BusinessObject.JournalType.DownPayment, entity,
                                                                       reg, transPaymentItems, "DR",
                                                                       AppSession.UserLogin.UserID,
                                                                       JournalID);
                    return journalId;
                }
            }
            return 0;


            //var entity = new TransPayment();
            //if (entity.LoadByPrimaryKey(RefNo))
            //{
            //    var reg = new Registration();
            //    reg.LoadByPrimaryKey(entity.RegistrationNo);

            //    var transPaymentItems = new TransPaymentItemCollection();
            //    transPaymentItems.Query.Where(transPaymentItems.Query.PaymentNo == entity.PaymentNo);
            //    transPaymentItems.LoadAll();

            //    int? journalId =
            //        JournalTransactions.AddNewDownPaymentReturnJournal(BusinessObject.JournalType.DownPayment, entity,
            //                                                           reg, transPaymentItems, "DR",
            //                                                           AppSession.UserLogin.UserID,
            //                                                           JournalID);
            //    return journalId;
            //}
            //return 0;
        }

        public static int? DownPaymentReturnAddCashEntry(string PaymentNo, int JournalID)
        {

            var tp = new TransPayment();
            if (tp.LoadByPrimaryKey(PaymentNo))
            {
                var tpItems = new TransPaymentItemCollection();
                tpItems.Query.Where(tpItems.Query.PaymentNo == tp.PaymentNo);
                if (tpItems.LoadAll())
                {
                    var jt = new JournalTransactions();
                    if (jt.LoadByPrimaryKey(JournalID))
                    {
                        // pastikan belum pernah ada cash entry
                        var ctColl = new CashTransactionCollection();
                        ctColl.Query.Where(ctColl.Query.DocumentNumber == PaymentNo, ctColl.Query.IsVoid == false);
                        if (!ctColl.LoadAll())
                        {
                            return JournalTransactions.DownPaymentReturnAddCashEntry(tp, tpItems, jt, tp.CreatedBy);
                        }
                    }
                }
            }
            return 0;
        }

        private void JournalDownPayment()
        {
            var JournalID = JournalDownPayment(Convert.ToInt32(Request.QueryString["ivd"]), txtRefferenceNumber.Text, false);
            if (JournalId > 0) lblJournalId.Text = JournalId.ToString();
        }
        public static int? JournalDownPayment(int JournalID, string RefNo, bool isReverse)
        {
            var entity = new TransPayment();
            if (entity.LoadByPrimaryKey(RefNo))
            {
                var registration = new Registration();
                registration.LoadByPrimaryKey(entity.RegistrationNo);

                var transPaymentItems = new TransPaymentItemCollection();
                transPaymentItems.Query.Where(transPaymentItems.Query.PaymentNo == entity.PaymentNo);
                transPaymentItems.LoadAll();

                var IsJournalVoid = false;
                if (JournalID == 0)
                {
                    IsJournalVoid = isReverse;// !(entity.IsApproved ?? false);
                }
                else
                {
                    // lihat dulu ini reproses journal dp atau journal void dp
                    var j = new JournalTransactions();
                    if (j.LoadByPrimaryKey(JournalID))
                    {
                        IsJournalVoid = j.JournalIdRefference.HasValue;
                    }
                }

                if (!IsJournalVoid)
                {
                    int? journalId = JournalTransactions.AddNewPaymentJournal(BusinessObject.JournalType.DownPayment,
                                                                                  entity, registration, transPaymentItems,
                                                                                  "DP", entity.CreatedBy,
                                                                                  JournalID);
                    return journalId;
                }
                else
                {
                    int? journalId =
                            JournalTransactions.AddNewPaymentCorrectionJournal(BusinessObject.JournalType.DownPayment,
                                                                               entity, registration, transPaymentItems, "DP",
                                                                               entity.CreatedBy,
                                                                               JournalID);
                    return journalId;
                }
            }
            return 0;
        }

        public static int? DownPaymentAddCashEntry(string PaymentNo, int JournalID)
        {
            
            var tp = new TransPayment();
            if (tp.LoadByPrimaryKey(PaymentNo)) {
                var tpItems = new TransPaymentItemCollection();
                tpItems.Query.Where(tpItems.Query.PaymentNo == tp.PaymentNo);
                if (tpItems.LoadAll()) {
                    var jt = new JournalTransactions();
                    if (jt.LoadByPrimaryKey(JournalID))
                    {
                        // pastikan belum pernah ada cash entry
                        var ctColl = new CashTransactionCollection();
                        ctColl.Query.Where(ctColl.Query.DocumentNumber == PaymentNo, ctColl.Query.IsVoid == false);
                        if (!ctColl.LoadAll())
                        {
                            return JournalTransactions.DownPaymentAddCashEntry(tp, tpItems, jt, tp.CreatedBy);
                        }
                    }
                }
            }
            return 0;
        }

        private void JournalPayment()
        {
            var JournalID = JournalPayment(Convert.ToInt32(Request.QueryString["ivd"]), txtRefferenceNumber.Text, false);
            if (JournalId > 0) lblJournalId.Text = JournalId.ToString();
        }
        public static int? JournalPayment(int JournalID, string RefNo, bool isReverse)
        {
            var entity = new TransPayment();
            if (entity.LoadByPrimaryKey(RefNo))
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(entity.RegistrationNo);

                var transPaymentItems = new TransPaymentItemCollection();
                transPaymentItems.Query.Where(transPaymentItems.Query.PaymentNo == entity.PaymentNo);
                transPaymentItems.LoadAll();

                var IsJournalVoid = false;
                if (JournalID == 0)
                {
                    IsJournalVoid = isReverse;//!(entity.IsApproved ?? false);
                }
                else
                {
                    // lihat dulu ini reproses journal payment atau journal void payment
                    var j = new JournalTransactions();
                    if (j.LoadByPrimaryKey(JournalID))
                    {
                        IsJournalVoid = j.Description.ToUpper().IndexOf("VOID") >= 0;
                    }
                }

                if (!IsJournalVoid)
                {
                    if (entity.TransactionCode == TransactionCode.PaymentReturn)
                    {
                        //int? journalId =
                        //JournalTransactions.AddNewPaymentReturnJournal(BusinessObject.JournalType.PaymentReturn,
                        //                                               entity, reg, transPaymentItems, "TP",
                        //                                               AppSession.UserLogin.UserID,
                        //                                               JournalID);

                        int? journalId = JournalPaymentReturn(JournalID, entity.PaymentNo);

                        return journalId;
                    }
                    else if (entity.TransactionCode == TransactionCode.Payment)
                    {
                        var x = (from tpi in transPaymentItems select tpi.SRPaymentType).Distinct();
                        string JournalType = BusinessObject.JournalType.Payment;
                        if (x.Contains(AppSession.Parameter.PaymentTypeCorporateAR))
                        {
                            JournalType = BusinessObject.JournalType.ARCreating;
                        }
                        else if (x.Contains(AppSession.Parameter.PaymentTypePersonalAR))
                        {
                            JournalType = BusinessObject.JournalType.ARCreating;
                        }

                        if (AppSession.Parameter.IsUsingIntermBill)
                        {
                            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                            {
                                var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');
                                if (type.Contains(reg.SRRegistrationType))
                                {

                                    int? journalId = JournalTransactions.AddNewPaymentCashBasedJournalTemporary(JournalType,
                                        entity, reg, transPaymentItems,
                                        "TP", entity.PaymentNo, AppSession.UserLogin.UserID, JournalID);
                                }
                                else
                                {
                                    int? journalId = JournalTransactions.AddNewPaymentCashBasedJournal(JournalType,
            entity, reg, transPaymentItems,
            "TP", entity.PaymentNo, AppSession.UserLogin.UserID, JournalID);
                                }

                                //int? journalId =
                                //    JournalTransactions.AddNewPaymentCashBasedJournalTemporary(JournalType,
                                //                                                      entity, reg, transPaymentItems, "TP",
                                //                                                      entity.PaymentNo,
                                //                                                      AppSession.UserLogin.UserID,
                                //                                                      JournalID);
                                //return journalId;
                            }
                            else
                            {
                                int? journalId =
                                    JournalTransactions.AddNewPaymentCashBasedJournal(JournalType,
                                                                                      entity, reg, transPaymentItems, "TP",
                                                                                      entity.PaymentNo,
                                                                                      AppSession.UserLogin.UserID,
                                                                                      JournalID);
                                return journalId;
                            }
                        }
                        else
                        {
                            int? journalId = JournalTransactions.AddNewPaymentJournal(
                                JournalType, entity, reg, transPaymentItems, "TP",
                                AppSession.UserLogin.UserID, JournalID);
                            return journalId;
                        }
                    }
                }
                else
                {
                    if (entity.TransactionCode == TransactionCode.PaymentReturn)
                    {
                        //int? journalId =
                        //    JournalTransactions.AddNewPaymentReturnCorrectionJournal(
                        //        BusinessObject.JournalType.PaymentReturn, entity, reg, transPaymentItems, "TP",
                        //        AppSession.UserLogin.UserID, JournalID);
                        //return journalId;
                        int? journalId = JournalPaymentReturn(JournalID, entity.PaymentNo);

                    }
                    else if (entity.TransactionCode == TransactionCode.Payment)
                    {
                        var x = (from tpi in transPaymentItems select tpi.SRPaymentType).Distinct();
                        string JournalType = BusinessObject.JournalType.Payment;
                        if (x.Contains(AppSession.Parameter.PaymentTypeCorporateAR))
                        {
                            JournalType = BusinessObject.JournalType.ARCreating;
                        }
                        else if (x.Contains(AppSession.Parameter.PaymentTypePersonalAR))
                        {
                            JournalType = BusinessObject.JournalType.ARCreating;
                        }

                        if (AppSession.Parameter.IsUsingIntermBill)
                        {
                            //int? journalId =
                            //    JournalTransactions.AddNewPaymentCashBasedVoidJournal(BusinessObject.JournalType.Payment, entity,
                            //                                                                              reg, this.TransPaymentItems, "TP",
                            //                                                                              entity.CreatedBy);
                            int? journalId = JournalTransactions.AddNewPaymentCorrectionJournalCashBased(JournalType,
                                entity, reg, transPaymentItems, "TP", entity.PaymentNo, AppSession.UserLogin.UserID,
                                JournalID);
                            return journalId;
                        }
                        else
                        {
                            int? journalId =
                                JournalTransactions.AddNewPaymentCorrectionJournal(JournalType, entity,
                                                                                   reg, transPaymentItems, "TP",
                                                                                   entity.CreatedBy, JournalID);
                            return journalId;
                        }

                        //int? journalId =
                        //    JournalTransactions.AddNewPaymentCorrectionJournal(BusinessObject.JournalType.Payment,
                        //                                                       entity, reg, transPaymentItems, "TP",
                        //                                                       AppSession.UserLogin.UserID,
                        //                                                       Convert.ToInt32(Request.QueryString["ivd"]));
                        //lblJournalId.Text = journalId.ToString();
                    }
                }
            }
            return 0;
        }

        private void JournalPaymentCashierCorrection()
        {
            var JournalID = JournalPaymentCashierCorrection(Convert.ToInt32(Request.QueryString["ivd"]), txtRefferenceNumber.Text);
            if (JournalId > 0) lblJournalId.Text = JournalId.ToString();
        }
        public static int? JournalPaymentCashierCorrection(int JournalID, string RefNo)
        {
            var entity = new TransPaymentCorrection();
            if (entity.LoadByPrimaryKey(RefNo))
            {
                var tpcItemsColl = new TransPaymentItemCorrectionCollection();
                tpcItemsColl.Query.Where(tpcItemsColl.Query.PaymentCorrectionNo == entity.PaymentCorrectionNo);
                if (tpcItemsColl.LoadAll())
                {
                    var jn = new JournalTransactions();
                    if (jn.LoadByPrimaryKey(JournalID))
                    {
                        if (jn.JournalIdRefference.HasValue)
                        {
                            int? journalId = JournalTransactions.AddNewCashierCorrectionJournalUnApproval(
                                JournalType.CashierCorrection, entity, tpcItemsColl, AppSession.UserLogin.UserID, JournalID);
                        }
                        else
                        {
                            int? journalId = JournalTransactions.AddNewCashierCorrectionJournal(
                                JournalType.CashierCorrection, entity, tpcItemsColl, AppSession.UserLogin.UserID, JournalID);
                        }
                    }
                    else
                    {
                        int? journalId = JournalTransactions.AddNewCashierCorrectionJournal(
                            JournalType.CashierCorrection, entity, tpcItemsColl, AppSession.UserLogin.UserID, JournalID);
                    }
                }
            }
            return 0;
        }

        private void JournalArPayment()
        {
            var journalId = JournalArPayment(Convert.ToInt32(Request.QueryString["ivd"]), txtRefferenceNumber.Text);
            if (journalId > 0) lblJournalId.Text = journalId.ToString();
        }
        public static int? JournalArPayment(int JournalID, string RefNo)
        {
            var app = new AppParameter();
            app.LoadByPrimaryKey("acc_IsAutoJournalARPayment");

            if (app.ParameterValue == "Yes")
            {
                var entity = new Invoices();
                if (entity.LoadByPrimaryKey(RefNo))
                {
                    var PrmUsingInvoicing = new AppParameter();
                    if (!PrmUsingInvoicing.LoadByPrimaryKey("acc_IsJournalArUsingInvoicing"))
                    {
                        throw new Exception("Parameter acc_IsJournalArUsingInvoicing not yet configured!");
                    }
                    if (PrmUsingInvoicing.ParameterValue == "Yes")
                    {
                        // approve or unapprove?
                        JournalTransactions jn = new JournalTransactions();
                        jn.LoadByPrimaryKey(JournalID);
                        if (jn.JournalIdRefference.HasValue)
                        {
                            int? journalId = JournalTransactions.AddNewARPaymentJournal2Unapproval(entity, AppSession.UserLogin.UserID, JournalID);
                            return journalId;
                        }
                        else
                        {
                            int? journalId = JournalTransactions.AddNewARPaymentJournal2(entity, AppSession.UserLogin.UserID, JournalID);
                            return journalId;
                        }
                    }
                    else
                    {
                        int? journalId = JournalTransactions.AddNewARPaymentJournal(entity, AppSession.UserLogin.UserID, JournalID);
                        return journalId;
                    }
                }
            }
            return 0;
        }

        private void JournalGrantReceived()
        {
            var journalId = JournalGrantReceived(Convert.ToInt32(Request.QueryString["ivd"]), txtRefferenceNumber.Text);
            if (journalId > 0) lblJournalId.Text = journalId.ToString();
        }
        public static int? JournalGrantReceived(int JournalID, string RefNo)
        {
            var app = new AppParameter();
            app.LoadByPrimaryKey("acc_IsAutoJournalPOReceived");
            if (app.ParameterValue == "Yes")
            {
                var entity = new ItemTransaction();
                if (entity.LoadByPrimaryKey(RefNo))
                {
                    int? journalId = 0;
                    journalId = JournalTransactions.AddNewGrantReceivedJournal(entity, AppSession.UserLogin.UserID, JournalID);
                    return journalId;
                }
            }
            return 0;
        }

        private void JournalPoReceived()
        {
            var journalId = JournalPoReceived(Convert.ToInt32(Request.QueryString["ivd"]), txtRefferenceNumber.Text);
            if (journalId > 0) lblJournalId.Text = journalId.ToString();
        }
        public static int? JournalPoReceived(int JournalID, string RefNo)
        {
            var app = new AppParameter();
            app.LoadByPrimaryKey("acc_IsAutoJournalPOReceived");
            if (app.ParameterValue == "Yes")
            {
                var entity = new ItemTransaction();
                if (entity.LoadByPrimaryKey(RefNo))
                {
                    int? journalId = 0;
                    //if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSIAMTP")
                    //    journalId = JournalTransactions.AddNewPurchaseOrderReceivedJournalNonVat(entity, AppSession.UserLogin.UserID, 0);
                    //else
                        journalId = JournalTransactions.AddNewPurchaseOrderReceivedJournal(entity, AppSession.UserLogin.UserID, JournalID);
                    return journalId;
                }
            }
            return 0;
        }

        private void JournalPoReturned()
        {
            var journalId = JournalPoReturned(Convert.ToInt32(Request.QueryString["ivd"]), txtRefferenceNumber.Text);
            if (journalId > 0) lblJournalId.Text = journalId.ToString();
        }
        public static int? JournalPoReturned(int JournalID, string RefNo)
        {
            var app = new AppParameter();
            app.LoadByPrimaryKey("acc_IsAutoJournalPOReturn");
            if (app.ParameterValue == "Yes")
            {
                var entity = new ItemTransaction();
                if (entity.LoadByPrimaryKey(RefNo))
                {
                    int? journalId = 0;
                    journalId = JournalTransactions.AddNewPurchaseOrderReturnedJournal(entity, AppSession.UserLogin.UserID, JournalID);
                    return journalId;
                }
            }
            return 0;
        }

        private void JournalDistribution()
        {
            var journalId = JournalDistribution(Convert.ToInt32(Request.QueryString["ivd"]), txtRefferenceNumber.Text);
            if (journalId > 0) lblJournalId.Text = journalId.ToString();
        }
        public static int? JournalDistribution(int JournalID, string RefNo)
        {
            var appDis = new AppParameter();
            if (appDis.LoadByPrimaryKey("acc_IsAutoJournalDistribution"))
            {
                if (appDis.ParameterValue == "Yes")
                {
                    if (AppParameter.IsNo(AppParameter.ParameterItem.IsDistributionAutoConfirm))
                    {
                        var entity = new ItemTransaction();
                        if (entity.LoadByPrimaryKey(RefNo))
                        {
                            if (entity.TransactionCode == TransactionCode.DistributionConfirm)
                            {
                                int? journalId = JournalTransactions.AddNewDistributionConfirmJournal(entity, AppSession.UserLogin.UserID, JournalID);
                                return journalId;
                            }
                        }
                    }
                    else {
                        var entity = new ItemTransaction();
                        if (entity.LoadByPrimaryKey(RefNo))
                        {
                            if (entity.TransactionCode == TransactionCode.Distribution)
                            {
                                int? journalId = JournalTransactions.AddNewDistributionConfirmJournal(entity, AppSession.UserLogin.UserID, JournalID);
                                return journalId;
                            }
                        }
                    }

                    ///*
                    // distribusi hanya bisa dijurnal jika Product Account --> Unit Base
                    //*/
                    //var app = new AppParameter();
                    //app.LoadByPrimaryKey("acc_IsUnitBasedProductAccount");
                    //if (app.ParameterValue == "Yes")
                    //{
                    //    var entity = new ItemTransaction();
                    //    if (entity.LoadByPrimaryKey(RefNo))
                    //    {
                    //        int? journalId = JournalTransactions.AddNewDistributionLocationBasedJournal(entity, AppSession.UserLogin.UserID, JournalID);
                    //        return journalId;
                    //    }
                    //}
                }
            }
            return 0;
        }

        private void JournalInventoryIssue()
        {
            var journalId = JournalInventoryIssue(Convert.ToInt32(Request.QueryString["ivd"]), txtRefferenceNumber.Text);
            if (journalId > 0) lblJournalId.Text = journalId.ToString();
        }
        public static int? JournalInventoryIssue(int JournalID, string RefNo)
        {
            var app = new AppParameter();
            app.LoadByPrimaryKey("acc_IsAutoJournalInvIssue");
            if (app.ParameterValue == "Yes")
            {
                var entity = new ItemTransaction();
                if (entity.LoadByPrimaryKey(RefNo))
                {
                    int? journalId = JournalTransactions.AddNewInventoryIssueJournal(entity, AppSession.UserLogin.UserID,
                                                                                     JournalID);
                    return journalId;
                }
                else
                {
                    var entityLaundry = new LaunderedProcess();
                    if (entityLaundry.LoadByPrimaryKey(RefNo))
                    {
                        int? journalId = JournalTransactions.AddNewInventoryIssueFromLaundryWashingProcessJournal(entityLaundry, AppSession.Parameter.ServiceUnitLaundryID, AppSession.UserLogin.UserID, JournalID);
                        return journalId;
                    }
                    else
                    {
                        var entityWO = new AssetWorkOrder();
                        if (entityWO.LoadByPrimaryKey(RefNo))
                        {
                            int? journalId = JournalTransactions.AddNewInventoryIssueFromWorkOrderJournal(entityWO, AppSession.UserLogin.UserID, JournalID);
                            return journalId;
                        }
                    }
                }
            }
            return 0;
        }

        private void JournalInventoryStockAdjustment()
        {
            var journalId = JournalInventoryStockAdjustment(Convert.ToInt32(Request.QueryString["ivd"]), txtRefferenceNumber.Text);
            if (journalId > 0) lblJournalId.Text = journalId.ToString();
        }
        public static int? JournalInventoryStockAdjustment(int JournalID, string RefNo)
        {
            var app = new AppParameter();
            app.LoadByPrimaryKey("acc_IsAutoJournalStockAdjustment");
            if (app.ParameterValue == "Yes")
            {
                int? journalId = 0;
                var entity = new ItemTransaction();
                if (entity.LoadByPrimaryKey(RefNo))
                {
                    switch (entity.TransactionCode)
                    {
                        case TransactionCode.ConsignmentTransfer:
                            journalId = JournalTransactions.AddNewInventoryStockAdjustmentJournal(entity, AppSession.UserLogin.UserID, JournalID, "SA", 0, true);
                            break;
                        case TransactionCode.PurchaseOrderReceive:
                            journalId = JournalTransactions.AddNewInventoryStockAdjustmentReverseJournal(entity, AppSession.UserLogin.UserID, JournalID, "SA", 0);
                            break;
                        default:
                            journalId = JournalTransactions.AddNewInventoryStockAdjustmentJournal(entity, AppSession.UserLogin.UserID, JournalID, "SA", 0, false);
                            break;
                    }
                }
                return journalId;
            }
            return 0;
        }

        private void JournalInventoryStockOpname()
        {
            var journalId = JournalInventoryStockOpname(Convert.ToInt32(Request.QueryString["ivd"]), txtRefferenceNumber.Text);
            if (journalId > 0) lblJournalId.Text = journalId.ToString();
        }
        public static int? JournalInventoryStockOpname(int JournalID, string RefNo)
        {
            var app = new AppParameter();
            app.LoadByPrimaryKey("acc_IsAutoJournalStockOpname");
            if (app.ParameterValue == "Yes")
            {
                var entity = new ItemTransaction();
                if (entity.LoadByPrimaryKey(RefNo))
                {
                    int? journalId = JournalTransactions.AddNewInventoryStockAdjustmentJournal(entity, AppSession.UserLogin.UserID,
                                                                                     JournalID, "ST", 0, false);
                    return journalId;
                }
            }
            return 0;
        }

        private void JournalInventoryProduction()
        {
            var journalId = JournalInventoryProduction(Convert.ToInt32(Request.QueryString["ivd"]), txtRefferenceNumber.Text);
            if (journalId > 0) lblJournalId.Text = journalId.ToString();
        }
        public static int? JournalInventoryProduction(int JournalID, string RefNo)
        {
            var app = new AppParameter();
            app.LoadByPrimaryKey("acc_IsAutoJournalInventoryProduction");
            if (app.ParameterValue == "Yes")
            {
                var entity = new ProductionOfGoods();
                if (entity.LoadByPrimaryKey(RefNo))
                {
                    int? journalId = JournalTransactions.AddNewInventoryProductionJournal(entity, AppSession.UserLogin.UserID, JournalID);
                    return journalId;
                }
            }
            return 0;
        }

        private void JournalSales()
        {
            var journalId = JournalSales(Convert.ToInt32(Request.QueryString["ivd"]), txtRefferenceNumber.Text);
            if (journalId > 0) lblJournalId.Text = journalId.ToString();
        }
        public static int? JournalSales(int JournalID, string RefNo)
        {
            var app = new AppParameter();
            app.LoadByPrimaryKey("acc_IsAutoJournalSales");
            if (app.ParameterValue == "Yes")
            {
                var entity = new ItemTransaction();
                if (entity.LoadByPrimaryKey(RefNo))
                {
                    int? journalId = JournalTransactions.AddNewSalesJournal(entity, AppSession.UserLogin.UserID, JournalID);
                    return journalId;
                }
            }
            return 0;
        }

        private void JournalSalesReturned()
        {
            var journalId = JournalSalesReturned(Convert.ToInt32(Request.QueryString["ivd"]), txtRefferenceNumber.Text);
            if (journalId > 0) lblJournalId.Text = journalId.ToString();
        }
        public static int? JournalSalesReturned(int JournalID, string RefNo)
        {
            var app = new AppParameter();
            app.LoadByPrimaryKey("acc_IsAutoJournalSales");
            if (app.ParameterValue == "Yes")
            {
                var entity = new ItemTransaction();
                if (entity.LoadByPrimaryKey(RefNo))
                {
                    int? journalId = JournalTransactions.AddNewSalesReturnedJournal(entity, AppSession.UserLogin.UserID, JournalID);
                    return journalId;
                }
            }
            return 0;
        }

        private void JournalARCustomer()
        {
            var journalId = JournalARCustomer(Convert.ToInt32(Request.QueryString["ivd"]), txtRefferenceNumber.Text);
            if (journalId > 0) lblJournalId.Text = journalId.ToString();
        }
        public static int? JournalARCustomer(int JournalID, string RefNo)
        {
            var app = new AppParameter();
            app.LoadByPrimaryKey("acc_IsAutoJournalSales");
            if (app.ParameterValue == "Yes")
            {
                var entity = new InvoiceCustomer();
                entity.LoadByPrimaryKey(RefNo);

                // approve or unapprove?
                JournalTransactions jn = new JournalTransactions();
                jn.LoadByPrimaryKey(JournalID);
                if (jn.JournalIdRefference.HasValue)
                {
                    int? journalId = JournalTransactions.AddNewARCustomerInvoicingJournal2Unapproval(entity, AppSession.UserLogin.UserID, JournalID);
                    return journalId;
                }
                else
                {
                    int? journalId = JournalTransactions.AddNewARCustomerInvoicingJournal2(entity, AppSession.UserLogin.UserID, JournalID);
                    return journalId;
                }
            }
            return 0;
        }

        private void JournalARCustomerPayment()
        {
            var journalId = JournalARCustomerPayment(Convert.ToInt32(Request.QueryString["ivd"]), txtRefferenceNumber.Text);
            if (journalId > 0) lblJournalId.Text = journalId.ToString();
        }

        public static int? JournalARCustomerPayment(int JournalID, string RefNo)
        {
            var app = new AppParameter();
            app.LoadByPrimaryKey("acc_IsAutoJournalSales");
            if (app.ParameterValue == "Yes")
            {
                var entity = new InvoiceCustomer();
                if (entity.LoadByPrimaryKey(RefNo))
                {
                    int? journalId = JournalTransactions.AddNewARCustomerPaymentJournal(entity, AppSession.UserLogin.UserID, JournalID);
                    return journalId;
                }
            }
            return 0;
        }


        private void JournalAp()
        {
            var journalId = JournalAp(Convert.ToInt32(Request.QueryString["ivd"]), txtRefferenceNumber.Text);
            if (journalId > 0) lblJournalId.Text = journalId.ToString();
        }
        public static int? JournalAp(int JournalID, string RefNo)
        {
            var app = new AppParameter();
            app.LoadByPrimaryKey("acc_IsAutoJournalAPPayment");

            if (app.ParameterValue == "Yes")
            {
                var entity = new InvoiceSupplier();
                if (entity.LoadByPrimaryKey(RefNo))
                {
                    //{
                    //    int? journalId = JournalTransactions.AddNewAPPaymentJournal(entity, AppSession.UserLogin.UserID,
                    //                                                                Convert.ToInt32(Request.QueryString["ivd"]));
                    //    lblJournalId.Text = journalId.ToString();
                    //}

                    app = new AppParameter();
                    app.LoadByPrimaryKey("HealthcareInitialAppsVersion");
                    if (app.ParameterValue == "RSSA")
                    {
                        int? journalId = JournalTransactions.AddNewAPInvoicingJournal(entity, AppSession.UserLogin.UserID);
                        return journalId;

                    }
                    else
                    {
                        var PrmApUsingInvoicing = new AppParameter();
                        if (!PrmApUsingInvoicing.LoadByPrimaryKey("acc_IsJournalApUsingInvoicing"))
                        {
                            throw new Exception("Parameter acc_IsJournalApUsingInvoicing not yet configured!");
                        }
                        if (PrmApUsingInvoicing.ParameterValue == "Yes")
                        {
                            // approve or unapprove?
                            var jn = new JournalTransactions();
                            if (jn.LoadByPrimaryKey(JournalID))
                            {
                                if (jn.JournalIdRefference.HasValue)
                                {
                                    int? journalId = JournalTransactions.AddNewAPInvoicingJournal2Unapproval(entity, AppSession.UserLogin.UserID,
                                        JournalID);
                                    return journalId;
                                }
                                else
                                {
                                    int? journalId = JournalTransactions.AddNewAPInvoicingJournal2(entity, AppSession.UserLogin.UserID,
                                        JournalID);
                                    return journalId;
                                }
                            }
                            else
                            {
                                // new journal
                                int? journalId = JournalTransactions.AddNewAPInvoicingJournal2(entity, AppSession.UserLogin.UserID,
                                        JournalID);
                                return journalId;
                            }
                        }
                    }
                }
                else
                {
                    // cek invoice adjustment
                    var adj = new InvoiceAdjusment();
                    if (adj.LoadByPrimaryKey(RefNo))
                    {
                        int? journalId = JournalTransactions.AddNewAPJournal(adj, AppSession.UserLogin.UserID, JournalID);
                        return journalId;
                    }
                }
            }
            return 0;
        }

        private void JournalApPayment()
        {
            var journalId = JournalApPayment(Convert.ToInt32(Request.QueryString["ivd"]), txtRefferenceNumber.Text);
            if (journalId > 0) lblJournalId.Text = journalId.ToString();
        }
        public static int? JournalApPayment(int JournalID, string RefNo)
        {
            var app = new AppParameter();
            app.LoadByPrimaryKey("acc_IsAutoJournalAPPayment");

            if (app.ParameterValue == "Yes")
            {
                var entity = new InvoiceSupplier();
                if (entity.LoadByPrimaryKey(RefNo))
                {
                    if (entity.IsWriteOff ?? false)
                    {
                        var invoicesItems = new InvoiceSupplierItemCollection();
                        invoicesItems.Query.Where(invoicesItems.Query.InvoiceNo == entity.InvoiceNo);
                        invoicesItems.LoadAll();

                        return JournalTransactions.AddNewWriteOffAPJournal(entity, invoicesItems, AppSession.UserLogin.UserID, JournalID);
                    }
                    else {
                        var PrmApUsingInvoicing = new AppParameter();
                        if (!PrmApUsingInvoicing.LoadByPrimaryKey("acc_IsJournalApUsingInvoicing"))
                        {
                            throw new Exception("Parameter acc_IsJournalApUsingInvoicing not yet configured!");
                        }
                        if (PrmApUsingInvoicing.ParameterValue == "Yes")
                        {
                            int? journalId;
                            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSMM")
                            {
                                journalId = JournalTransactions.AddNewAPPaymentJournal2RSMM(entity, AppSession.UserLogin.UserID, JournalID);
                            }
                            else
                            {
                                journalId = JournalTransactions.AddNewAPPaymentJournal2(entity, AppSession.UserLogin.UserID, JournalID);
                            }
                            return journalId;
                        }
                        else
                        {
                            int? journalId = JournalTransactions.AddNewAPPaymentJournal(entity, AppSession.UserLogin.UserID, JournalID);
                            return journalId;
                        }
                    }
                }
            }
            return 0;
        }

        private void JournalParamedicFeePayment()
        {
            var journalId = JournalParamedicFeePayment(Convert.ToInt32(Request.QueryString["ivd"]), txtRefferenceNumber.Text);
            if (journalId > 0) lblJournalId.Text = journalId.ToString();
        }
        public static int? JournalParamedicFeePayment(int JournalID, string RefNo)
        {
            var app = new AppParameter();
            app.LoadByPrimaryKey("acc_IsAutoJournalFeePayment");

            if (app.ParameterValue == "Yes")
            {
                var entity = new ParamedicFeePaymentHd();
                if (entity.LoadByPrimaryKey(RefNo))
                {
                    var appprg = new AppProgram();
                    if (appprg.LoadByPrimaryKey("05.07.03"))
                    {
                        if (appprg.NavigateUrl.Trim().IndexOf("ParamedicFeeVerificationByDischargeDateList.aspx") > 0)
                        {
                            // paramedic fee based on discharge date
                            int? journalId = JournalTransactions.AddNewPhysicianPaymentJournalByDischargeDate(
                                BusinessObject.JournalType.PhysicianPayment, entity, AppSession.UserLogin.UserID,
                                    JournalID);
                            return journalId;
                        }
                    }
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSSA")
                    {
                        int? journalId =
                            JournalTransactions.AddNewPhysicianPaymentJournal2(
                                BusinessObject.JournalType.PhysicianPayment, entity, AppSession.UserLogin.UserID,
                                JournalID);
                        return journalId;
                    }
                    else
                    {
                        int? journalId =
                            JournalTransactions.AddNewPhysicianPaymentJournal(
                                BusinessObject.JournalType.PhysicianPayment, entity, AppSession.UserLogin.UserID,
                                JournalID);
                        return journalId;
                    }
                }
                else
                {
                    var entityPG = new ParamedicFeePaymentGroup();
                    if (entityPG.LoadByPrimaryKey(RefNo))
                    {
                        var jn = new JournalTransactions();
                        if (jn.LoadByPrimaryKey(JournalID))
                        {
                            if (jn.JournalIdRefference.HasValue)
                            {
                                int? journalId = JournalTransactions.AddNewPhysicianPaymentJournalByDischargeDateByPaymentGroupUnapproval(
                                BusinessObject.JournalType.PhysicianPayment, entityPG, AppSession.UserLogin.UserID,
                                    JournalID);
                                return journalId;
                            }
                            else
                            {
                                int? journalId = JournalTransactions.AddNewPhysicianPaymentJournalByDischargeDateByPaymentGroup(
                                BusinessObject.JournalType.PhysicianPayment, entityPG, AppSession.UserLogin.UserID,
                                    JournalID);
                                return journalId;
                            }
                        }
                        else
                        {
                            // new journal
                            int? journalId = JournalTransactions.AddNewPhysicianPaymentJournalByDischargeDateByPaymentGroup(
                                BusinessObject.JournalType.PhysicianPayment, entityPG, AppSession.UserLogin.UserID,
                                    JournalID);
                            return journalId;
                        }
                    }
                }
            }
            return 0;
        }

        private void JournalParamedicFeeVerification()
        {
            var journalId = JournalParamedicFeeVerification(Convert.ToInt32(Request.QueryString["ivd"]), txtRefferenceNumber.Text);
            if (journalId > 0) lblJournalId.Text = journalId.ToString();
        }
        public static int? JournalParamedicFeeVerification(int JournalID, string RefNo)
        {
            var app = new AppParameter();
            app.LoadByPrimaryKey("acc_IsAutoJournalFeeVerification");

            if (app.ParameterValue == "Yes")
            {
                var entity = new ParamedicFeeVerification();
                if (entity.LoadByPrimaryKey(RefNo))
                {
                    var appprg = new AppProgram();
                    if (appprg.LoadByPrimaryKey("05.07.03"))
                    {
                        if (appprg.NavigateUrl.Trim().IndexOf("ParamedicFeeVerificationByDischargeDateList.aspx") > 0)
                        {
                            // paramedic fee based on discharge date
                            int? journalId = JournalTransactions.AddNewPhysicianVerificationJournalByDischargeDate(
                                BusinessObject.JournalType.PhysicianFeeVerification, entity, AppSession.UserLogin.UserID,
                                JournalID);
                            return journalId;
                        }
                    }
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSSA")
                    {
                        int? journalId =
                            JournalTransactions.AddNewPhysicianVerificationJournal2(
                                BusinessObject.JournalType.PhysicianFeeVerification, entity, AppSession.UserLogin.UserID,
                                JournalID);
                        return journalId;
                    }
                    else
                    {
                        int? journalId =
                            JournalTransactions.AddNewPhysicianVerificationJournal(
                                BusinessObject.JournalType.PhysicianFeeVerification, entity, AppSession.UserLogin.UserID,
                                JournalID);
                        return journalId;
                    }
                }
            }
            return 0;
        }

        private void JournalParamedicFeePayable()
        {
            // format ref no jurnal PRV-yyyy/mm/dd
            var refno = txtRefferenceNumber.Text.Split('-')[1];
            var dPart = refno.Split('/');
            var date = new DateTime(System.Convert.ToInt32(dPart[0]), System.Convert.ToInt32(dPart[1]), System.Convert.ToInt32(dPart[2]));

            JournalTransactions entity = new JournalTransactions();
            entity.LoadByPrimaryKey(Convert.ToInt32(Request.QueryString["ivd"]));
            if (entity.JournalIdRefference.HasValue)
            {
                // ini reprosesing journal reverse biaya dan hutang dokter
                var journalId = JournalParamedicFeePayableReversed(Convert.ToInt32(Request.QueryString["ivd"]), date/*reprocessing will ignore date*/, date/*reprocessing will ignore dateRef*/);
                if (journalId > 0) lblJournalId.Text = journalId.ToString();
            }
            else
            {
                // ini reprosesing journal biaya dan hutang dokter
                var journalId = JournalParamedicFeePayable(Convert.ToInt32(Request.QueryString["ivd"]), date);
                if (journalId > 0) lblJournalId.Text = journalId.ToString();
            }
        }

        public static int? JournalParamedicFeePayable(int JournalID, DateTime date)
        {

            int? journalId = JournalTransactions.AddNewParamedicFeePayable(
                BusinessObject.JournalType.PhysicianFeePayable, date, AppSession.UserLogin.UserID, JournalID);
            return journalId;
        }

        public static int? JournalParamedicFeePayableReversed(int JournalID, DateTime date, DateTime referenceDate)
        {
            int? journalId = JournalTransactions.AddNewParamedicFeePayableReversed(
                BusinessObject.JournalType.PhysicianFeePayable, date, referenceDate, AppSession.UserLogin.UserID, JournalID);
            return journalId;
        }

        private void JournalInvoice()
        {
            var journalId = JournalInvoice(Convert.ToInt32(Request.QueryString["ivd"]), txtRefferenceNumber.Text);
            if (journalId > 0) lblJournalId.Text = journalId.ToString();
        }
        public static int? JournalInvoice(int JournalID, string RefNo)
        {
            var PrmUsingInvoicing = new AppParameter();
            if (!PrmUsingInvoicing.LoadByPrimaryKey("acc_IsJournalArUsingInvoicing"))
            {
                throw new Exception("Parameter acc_IsJournalArUsingInvoicing not yet configured!");
            }
            if (PrmUsingInvoicing.ParameterValue == "Yes")
            {
                var entity = new Invoices();
                entity.LoadByPrimaryKey(RefNo);

                // approve or unapprove?
                JournalTransactions jn = new JournalTransactions();
                jn.LoadByPrimaryKey(JournalID);
                if (jn.JournalIdRefference.HasValue)
                {
                    int? journalId = JournalTransactions.AddNewARInvoicingJournal2Unapproval(entity, AppSession.UserLogin.UserID,
                                JournalID);
                    return journalId;
                }
                else
                {
                    int? journalId = JournalTransactions.AddNewARInvoicingJournal2(entity, AppSession.UserLogin.UserID,
                                JournalID);
                    return journalId;
                }
            }
            return 0;
        }

        private void JournalPatientReceivable()
        {
            // format ref no jurnal PRV-yyyy/mm/dd
            var refno = txtRefferenceNumber.Text.Split('-')[1];
            var dPart = refno.Split('/');
            var date = new DateTime(System.Convert.ToInt32(dPart[0]), System.Convert.ToInt32(dPart[1]), System.Convert.ToInt32(dPart[2]));

            JournalTransactions entity = new JournalTransactions();
            entity.LoadByPrimaryKey(Convert.ToInt32(Request.QueryString["ivd"]));
            if (entity.JournalIdRefference.HasValue)
            {
                // ini reprosesing journal reverse piutang dalam perawatan
                var journalId = JournalPatientReceivableReversed(Convert.ToInt32(Request.QueryString["ivd"]), date/*reprocessing will ignore date*/, date/*reprocessing will ignore dateRef*/);
                if (journalId > 0) lblJournalId.Text = journalId.ToString();
            }
            else
            {
                // ini reprosesing journal piutang dalam perawatan
                var journalId = JournalPatientReceivable(Convert.ToInt32(Request.QueryString["ivd"]), date);
                if (journalId > 0) lblJournalId.Text = journalId.ToString();
            }
        }
        public static int? JournalPatientReceivable(int JournalID, DateTime date)
        {

            int? journalId = JournalTransactions.AddNewPatientReceivableJournal(
                BusinessObject.JournalType.PatientReceivable, date, AppSession.UserLogin.UserID, JournalID, AppSession.Parameter.IsAccountReceivableByDischargeDate);
            return journalId;
        }

        //private void JournalPatientReceivableReversed()
        //{
        //    // format ref no jurnal PRV-yyyy/mm/dd
        //    var refno = txtRefferenceNumber.Text.Split('-')[1];
        //    var dPart = refno.Split('/');
        //    var date = new DateTime(System.Convert.ToInt32(dPart[0]), System.Convert.ToInt32(dPart[1]), System.Convert.ToInt32(dPart[2]));
        //    var journalId = JournalPatientReceivableReversed(Convert.ToInt32(Request.QueryString["ivd"]), date/*reprocessing will ignore date*/, date/*reprocessing will ignore dateRef*/);
        //    if (journalId > 0) lblJournalId.Text = journalId.ToString();
        //}
        public static int? JournalPatientReceivableReversed(int JournalID, DateTime date, DateTime referenceDate)
        {
            int? journalId = JournalTransactions.AddNewPatientReceivableJournalReversed(
                BusinessObject.JournalType.PatientReceivable, date, referenceDate, AppSession.UserLogin.UserID, JournalID);
            return journalId;
        }

        private void JournalPayroll(int typeId)
        {
            // format ref no jurnal PRV-yyyy/mm/dd
            //var refno = txtRefferenceNumber.Text.Split('-')[1];
            //var dPart = refno.Split('/');
            //var date = new DateTime(System.Convert.ToInt32(dPart[0]), System.Convert.ToInt32(dPart[1]), System.Convert.ToInt32(dPart[2]));

            JournalTransactions entity = new JournalTransactions();
            entity.LoadByPrimaryKey(Convert.ToInt32(Request.QueryString["ivd"]));

            var journalId = JournalPayroll(Convert.ToInt32(Request.QueryString["ivd"]), typeId, entity.RefferenceNumber.Substring(3,8));
            if (journalId > 0) lblJournalId.Text = journalId.ToString();

        }
        public static int? JournalPayroll(int JournalID, int typeId, string reffNo)
        {
            var x = new PayrollPeriod();
            var ppQuery = new PayrollPeriodQuery();
            ppQuery.Where(ppQuery.PayrollPeriodCode.Trim() == reffNo);
            x.Load(ppQuery);

            int? journalId;
            if (typeId == 1)
            {
                journalId = JournalTransactions.AddNewPayrollJournal(x.PayrollPeriodID.ToInt(), AppSession.UserLogin.UserID, JournalID);
            }
            else
            {
                journalId = JournalTransactions.AddNewThrJournal(x.PayrollPeriodID.ToInt(), AppSession.UserLogin.UserID, JournalID);
            }

            return journalId;
        }
        private void JournalAssets(string journalType)
        {
            JournalTransactions entity = new JournalTransactions();
            entity.LoadByPrimaryKey(Convert.ToInt32(Request.QueryString["ivd"]));

            var journalId = JournalAssets(Convert.ToInt32(Request.QueryString["ivd"]), journalType, entity.RefferenceNumber);
            if (journalId > 0) lblJournalId.Text = journalId.ToString();

        }
        public static int? JournalAssets(int JournalID, string journalType, string reffNo)
        {
            int? journalId;

            if (journalType == JournalType.AssetMovement)
            {
                var am = new AssetMovement();
                if (!am.LoadByPrimaryKey(reffNo))
                    return -1;

                var asset = new Asset();
                asset.LoadByPrimaryKey(am.AssetID);

                journalId = JournalTransactions.AddNewAssetMovementJournal(am, asset, AppSession.UserLogin.UserID, JournalID);
            }
            else
            {
                var sh = new AssetStatusHistory();
                if (!sh.LoadByPrimaryKey(reffNo.ToInt()))
                    return -1;

                var asset = new Asset();
                asset.LoadByPrimaryKey(sh.AssetID);

                if (journalType == JournalType.AssetAuction)
                {
                    journalId = JournalTransactions.AddNewAssetAuctionJournal(sh, asset, AppSession.UserLogin.UserID, JournalID);
                }
                else if (journalType == JournalType.AssetDestruction)
                {
                    journalId = JournalTransactions.AddNewAssetDestructionJournal(sh, asset, AppSession.UserLogin.UserID, JournalID);
                }
                else
                    return -1;
            }

            return journalId;
        }

        private void JournalClosingVisitDp(string journalType)
        {
            JournalTransactions entity = new JournalTransactions();
            entity.LoadByPrimaryKey(Convert.ToInt32(Request.QueryString["ivd"]));

            var journalId = JournalClosingVisitDp(Convert.ToInt32(Request.QueryString["ivd"]), journalType, entity.RefferenceNumber);
            if (journalId > 0) lblJournalId.Text = journalId.ToString();

        }
        public static int? JournalClosingVisitDp(int JournalID, string journalType, string reffNo)
        {
            int? journalId;

            var hd = new ClosingVisiteDownPayment();
            if (!hd.LoadByPrimaryKey(reffNo))
                return -1;

            var dts = new ClosingVisiteDownPaymentItemCollection();
            dts.Query.Where(dts.Query.ClosingNo == reffNo);
            dts.LoadAll();

            journalId = JournalTransactions.AddNewClosingVisitDownPaymentJournal(journalType, hd, dts, "CVD", AppSession.UserLogin.UserID, JournalID);

            return journalId;
        }
    }
}