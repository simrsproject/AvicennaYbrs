using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI;
using System.Linq;
namespace Temiang.Avicenna.Module.Finance.Payable
{
    public partial class InvoicingDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private void PopulateNewInvoiceNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New) return;

            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.InvoiceAPNo);
            txtInvoiceNo.Text = _autoNumber.LastCompleteNumber;
        }

        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "InvoicingSearch.aspx?type=" + Request.QueryString["type"] + "&pg=0";
            UrlPageList = "InvoicingList.aspx?type=" + Request.QueryString["type"] + "&pg=" + Request.QueryString["pg"];

            this.WindowSearch.Height = 400;

            ProgramID = Request.QueryString["type"] == "1"
                            ? AppConstant.Program.AP_INVOICING
                            : AppConstant.Program.AP_INVOICING2;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRPayableStatus, AppEnum.StandardReference.PayableStatus);

                if (AppSession.Parameter.IsPphUsesAfixedValue)
                {
                    grdInvoiceSupplierItem.Columns[10].Visible = true;
                }
                else
                    grdInvoiceSupplierItem.Columns[11].Visible = true;
            }

            //Add Event for Request Order Selection
            this.AjaxManager.AjaxRequest += new RadAjaxControl.AjaxRequestDelegate(AjaxManager_AjaxRequest);
        }

        void AjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdInvoiceSupplierItem, grdInvoiceSupplierItem);
            ajax.AddAjaxSetting(grdInvoiceSupplierItem, cboSupplierID);
            ajax.AddAjaxSetting(cboSupplierID, txtInvoiceDueDate);

            ajax.AddAjaxSetting(AjaxManager, txtInvoiceNo);
            ajax.AddAjaxSetting(AjaxManager, grdInvoiceSupplierItem);
        }
        #endregion

        #region ComboBox SupplierID

        protected void cboSupplierID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboSupplierID((RadComboBox)sender, e.Text);
        }

        private void PopulateCboSupplierID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new SupplierQuery();
            query.Select(query.SupplierID, query.SupplierName,
                         (query.StreetName + " " + query.City + " " + query.ZipCode).Trim().As("Address"));
            query.Where
                (
                    query.Or
                        (
                            query.SupplierID.Like(searchTextContain),
                            query.SupplierName.Like(searchTextContain)
                        ), query.IsActive == true
                );

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }

        protected void cboSupplierID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SupplierName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SupplierID"].ToString();
        }

        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID,
                                                PrintJobParameterCollection printJobParameters)
        {
            switch (programID)
            {
                case AppConstant.Report.AP_Invoicing:
                    printJobParameters.AddNew("InvoiceNo", txtInvoiceNo.Text);
                    break;
                case AppConstant.Report.AP_Invoicing2:
                    printJobParameters.AddNew("InvoiceNo", txtInvoiceNo.Text);
                    break;
                case AppConstant.Report.PurchaseReceiveAgreement:
                    printJobParameters.AddNew("p_InvoiceNo", txtInvoiceNo.Text);
                    break;
                case AppConstant.Report.PurchaseReceiveAgreementVer2:
                    printJobParameters.AddNew("p_InvoiceNo", txtInvoiceNo.Text);
                    break;
                default:
                    printJobParameters.AddNew("p_InvoiceNo", txtInvoiceNo.Text);
                    break;
            }

            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSUI")
                printJobParameters.AddNew("p_UserID", AppSession.UserLogin.UserID);
        }

        protected override void OnMenuEditClick()
        {

        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new InvoiceSupplier();
            if (entity.LoadByPrimaryKey(txtInvoiceNo.Text))
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
            var retval = (new InvoiceSupplier()).Approv(txtInvoiceNo.Text, AppSession.UserLogin.UserID, AppSession.Parameter.PayableStatusProcess, false);
            if (retval != string.Empty)
            {
                args.MessageText = retval;
                args.IsCancel = true;
            }
        }

        public override bool OnGetStatusMenuUnApprovalEnabled()
        {
            return !PostingStatus.IsUnApproveDisabledIfPerClosed(txtInvoiceDate.SelectedDate.Value);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var inv = new InvoiceSupplier();
            inv.LoadByPrimaryKey(txtInvoiceNo.Text);
            if (inv.SRPayableStatus == AppSession.Parameter.PayableStatusProcess)
            {
                var retval = (new InvoiceSupplier()).UnApprov(txtInvoiceNo.Text, AppSession.UserLogin.UserID,
                                                              AppSession.Parameter.PayableStatusProcess, false);

                if (retval != string.Empty)
                {
                    args.MessageText = retval;
                    args.IsCancel = true;
                }
                // jika sudah ada journal maka jurnalnya harus void dulu ya
                //if (!(JournalTransactions.IsExistJournalByRef(inv.InvoiceNo)))
                //{
                //    (new InvoiceSupplier()).UnApprov(txtInvoiceNo.Text, AppSession.UserLogin.UserID,
                //                                     AppSession.Parameter.PayableStatusProcess, false);
                //}
                //else
                //{
                //    args.MessageText = "This data has been proceed to journal. Please void the journal first!";
                //}
            }
            else
            {
                args.MessageText = "This data has been proceed to next process.";
                args.IsCancel = true;
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            (new InvoiceSupplier()).Void(txtInvoiceNo.Text, AppSession.UserLogin.UserID);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            (new InvoiceSupplier()).UnVoid(txtInvoiceNo.Text, AppSession.UserLogin.UserID);
        }

        private bool IsApprovedOrVoid(InvoiceSupplier entity, ValidateArgs args)
        {
            if (entity.IsApproved != null && entity.IsApproved.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            if (entity.IsVoid != null && entity.IsVoid.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }
            return true;
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new InvoiceSupplier());
            PopulateNewInvoiceNo();

            txtInvoiceDate.SelectedDate = (new DateTime()).NowAtSqlServer(); //DateTime.Now;
            txtInvoiceReceivedDate.SelectedDate = (new DateTime()).NowAtSqlServer();

            btnGetItem.Enabled = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new InvoiceSupplier();
            if (entity.LoadByPrimaryKey(txtInvoiceNo.Text))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            PopulateNewInvoiceNo();
            var entity = new InvoiceSupplier();
            if (entity.LoadByPrimaryKey(txtInvoiceNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new InvoiceSupplier();
            entity.AddNew();
            SetEntityValue(entity);
            if (InvoiceSupplierItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new InvoiceSupplier();
            if (entity.LoadByPrimaryKey(txtInvoiceNo.Text))
            {
                SetEntityValue(entity);
                if (InvoiceSupplierItems.Count == 0)
                {
                    args.MessageText = AppConstant.Message.RecordDetailEmpty;
                    args.IsCancel = true;
                    return;
                }
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
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
            auditLogFilter.PrimaryKeyData = string.Format("InvoiceNo='{0}'", txtInvoiceNo.Text.Trim());
            auditLogFilter.TableName = "InvoiceSupplier";
        }


        #endregion

        #region ToolBar Menu Support
        public override bool OnGetStatusMenuEdit()
        {
            return txtInvoiceNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !chkIsApproved.Checked;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !chkIsVoid.Checked;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemGrid(oldVal, newVal);

            btnGetItem.Enabled = newVal != AppEnum.DataMode.Read;

            if (newVal != AppEnum.DataMode.New)
            {
                txtInvoiceDate.Enabled = false;
                txtInvoiceReceivedDate.Enabled = false;
                cboSupplierID.Enabled = false;

                if (oldVal == AppEnum.DataMode.New)
                {
                    cboSupplierID.Text = string.Empty;
                }
            }
            else
            {
                cboSupplierID.Text = string.Empty;
                txtInvoiceDate.Enabled = true;
                txtInvoiceReceivedDate.Enabled = true;
                cboSupplierID.Enabled = true;
            }
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            InvoiceSupplier entity = new InvoiceSupplier();
            if (parameters.Length > 0)
            {
                String invoiceNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(invoiceNo);

            }
            else
            {
                entity.LoadByPrimaryKey(txtInvoiceNo.Text);
            }

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var invoiceSupplier = (InvoiceSupplier)entity;
            txtInvoiceNo.Text = invoiceSupplier.InvoiceNo;
            txtInvoiceSuppNo.Text = invoiceSupplier.InvoiceSuppNo;

            if (!string.IsNullOrEmpty(invoiceSupplier.SupplierID))
            {
                var supp = new SupplierQuery("a");
                supp.Select(supp.SupplierID, supp.SupplierName,
                             (supp.StreetName + " " + supp.City + " " + supp.ZipCode).Trim().As("Address"));
                supp.Where(supp.SupplierID == invoiceSupplier.SupplierID);
                DataTable tbl = supp.LoadDataTable();
                cboSupplierID.DataSource = tbl;
                cboSupplierID.DataBind();
                cboSupplierID.SelectedValue = invoiceSupplier.SupplierID;
                cboSupplierID.Text = tbl.Rows[0]["SupplierName"].ToString();
            }
            else
            {
                //cboSupplierID.Items.Clear();
                //cboSupplierID.Text = string.Empty;
                cboSupplierID.SelectedValue = string.Empty;
            }

            txtInvoiceDate.SelectedDate = invoiceSupplier.InvoiceDate;
            txtInvoiceReceivedDate.SelectedDate = invoiceSupplier.InvoiceReceivedDate;
            txtInvoiceDueDate.SelectedDate = invoiceSupplier.InvoiceDueDate;
            txtNotes.Text = invoiceSupplier.InvoiceNotes;
            cboSRPayableStatus.SelectedValue = invoiceSupplier.SRPayableStatus;
            chkIsVoid.Checked = invoiceSupplier.IsVoid ?? false;
            chkIsApproved.Checked = invoiceSupplier.IsApproved ?? false;

            //Display Data Detail
            PopulateGridDetail();
            btnGetItem.Enabled = false;
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(InvoiceSupplier entity)
        {
            entity.InvoiceNo = txtInvoiceNo.Text;
            entity.InvoiceSuppNo = txtInvoiceSuppNo.Text;
            entity.SupplierID = cboSupplierID.SelectedValue;
            entity.InvoiceDate = txtInvoiceDate.SelectedDate;
            entity.InvoiceReceivedDate = txtInvoiceReceivedDate.SelectedDate;
            entity.InvoiceDueDate = txtInvoiceDueDate.SelectedDate;
            entity.InvoiceTOP = (txtInvoiceDueDate.SelectedDate.Value - txtInvoiceDate.SelectedDate.Value).Days;
            entity.InvoiceNotes = txtNotes.Text;
            entity.IsInvoicePayment = false;
            entity.IsConsignment = false;

            //Update Detil
            foreach (InvoiceSupplierItem item in InvoiceSupplierItems)
            {
                item.InvoiceNo = txtInvoiceNo.Text;
                item.AgingDate = txtInvoiceDueDate.SelectedDate;
            }
        }

        private void SaveEntity(InvoiceSupplier entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                InvoiceSupplierItems.Save();

                if (DataModeCurrent == AppEnum.DataMode.New)
                    _autoNumber.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new InvoiceSupplierQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            que.Where(que.IsInvoicePayment == false);
            if (isNextRecord)
            {
                que.Where(que.InvoiceNo > txtInvoiceNo.Text);
                que.OrderBy(que.InvoiceNo.Ascending);
            }
            else
            {
                que.Where(que.InvoiceNo < txtInvoiceNo.Text);
                que.OrderBy(que.InvoiceNo.Descending);
            }
            var entity = new InvoiceSupplier();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged
        protected void cboSupplierID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var supplier = new Supplier();
            supplier.LoadByPrimaryKey(cboSupplierID.SelectedValue);

            double top = Convert.ToDouble(supplier.TermOfPayment);

            DateTime date = Convert.ToDateTime(txtInvoiceDate.SelectedDate);
            txtInvoiceDueDate.SelectedDate = date.AddDays((int)top);
        }

        #endregion

        #region Record Detail Method Function

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdInvoiceSupplierItem.Columns[0].Visible = isVisible && Request.QueryString["type"] == "1";
            grdInvoiceSupplierItem.Columns[1].Visible = isVisible && Request.QueryString["type"] == "1";
            grdInvoiceSupplierItem.Columns[grdInvoiceSupplierItem.Columns.Count - 1].Visible = isVisible;

            // Bisa tambah additional AP Juni 2017 (by hand)
            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsAllowAdditionalAP).ToLower() == "yes" && Request.QueryString["type"] == "1")
                grdInvoiceSupplierItem.MasterTableView.CommandItemDisplay = isVisible
                                                                     ? GridCommandItemDisplay.Top
                                                                     : GridCommandItemDisplay.None;
            else
                grdInvoiceSupplierItem.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                InvoiceSupplierItems = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdInvoiceSupplierItem.Rebind();
        }

        private InvoiceSupplierItemCollection InvoiceSupplierItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collInvoiceSupplierItem" + Request.UserHostName];
                    if (obj != null)
                        return ((InvoiceSupplierItemCollection)(obj));
                }

                var coll = new InvoiceSupplierItemCollection();
                var query = new InvoiceSupplierItemQuery("a");
                var que = new ItemTransactionQuery("b");
                var coa = new ChartOfAccountsQuery("coa");
                query.LeftJoin(que).On(que.TransactionNo == query.TransactionNo);
                query.LeftJoin(coa).On(query.ChartOfAccountId == coa.ChartOfAccountId);

                query.Where(query.InvoiceNo == txtInvoiceNo.Text);
                query.OrderBy
                    (
                        query.TransactionNo.Ascending
                    );

                query.Select
                    (
                        query, query.PPh22Amount, query.PPh23Amount,
                        que.InvoiceNo.As("refToItemTransaction_InvoiceSupplierNo"),
                        @"<CASE WHEN b.InvoiceSupplierDate IS NULL THEN ISNULL(b.TransactionDate, a.TransactionDate) ELSE b.InvoiceSupplierDate END AS 'refItemTransaction_InvoiceSupplierDate'>",
                        coa.ChartOfAccountCode.As("refToChartOfAccounts_ChartOfAccountCode"),
                        coa.ChartOfAccountName.As("refToChartOfAccounts_ChartOfAccountName"),
                        @"<CASE WHEN b.TransactionNo IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'refItemTransaction_IsAllowEdit'>"//,
                        //@"<CASE ISNULL(IsPpnExcluded, 0) WHEN 0 THEN PPnAmount ELSE 0 END refTo_PpnBilled>"
                    );
                if (AppSession.Parameter.IsPphUsesAfixedValue)
                {
                    query.Where(@"<CASE WHEN a.PPh22Amount > 0 THEN 'PPh 22' ELSE CASE WHEN a.PPh23Amount > 0 THEN 'PPh 23' ELSE '' END END AS 'refToStd_Pph' >");
                }
                else 
                {
                    var pphType = new AppStandardReferenceItemQuery("pphtype");
                    query.LeftJoin(pphType).On(pphType.StandardReferenceID == AppEnum.StandardReference.Pph.ToString() && pphType.ItemID == query.SRPph);
                    query.Select(pphType.ItemName.As("refToStd_Pph"));
                }

                coll.Load(query);

                Session["collInvoiceSupplierItem" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collInvoiceSupplierItem" + Request.UserHostName] = value; }
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            InvoiceSupplierItems = null; //Reset Record Detail
            grdInvoiceSupplierItem.DataSource = InvoiceSupplierItems;
            grdInvoiceSupplierItem.MasterTableView.IsItemInserted = false;
            grdInvoiceSupplierItem.MasterTableView.ClearEditItems();
            grdInvoiceSupplierItem.DataBind();
        }

        protected void grdInvoiceSupplierItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdInvoiceSupplierItem.DataSource = InvoiceSupplierItems;
        }

        protected void grdInvoiceSupplierItem_DataBound(object sender, EventArgs e)
        {
            var txt = ((GridFooterItem)((RadGrid)sender).MasterTableView.GetItems(GridItemType.Footer)[0]).FindControl("txtSumTotalTemplate") as RadNumericTextBox;
            txt.Value = System.Convert.ToDouble(InvoiceSupplierItems.Sum(x => x.Amount + x.PpnBilled + x.StampAmount - x.OtherDeduction - x.DownPaymentAmount - x.PphAmount));
            //txt.Value = System.Convert.ToDouble(InvoiceSupplierItems.Sum(x => x.Amount + x.PpnBilled - x.PPh22Amount - x.PPh23Amount + x.StampAmount - x.OtherDeduction - x.DownPaymentAmount - x.PphAmount));
        }

        protected void grdInvoiceSupplierItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String transactionNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][InvoiceSupplierItemMetadata.ColumnNames.TransactionNo]);
            InvoiceSupplierItem entity = FindInvoiceSupplierItem(transactionNo);
            if (entity != null)
                SetEntityValue(entity, e, false);
        }

        private InvoiceSupplierItem FindInvoiceSupplierItem(String transactionNo)
        {
            InvoiceSupplierItemCollection coll = InvoiceSupplierItems;
            InvoiceSupplierItem retEntity = null;
            foreach (InvoiceSupplierItem rec in coll)
            {
                if (rec.TransactionNo.Equals(transactionNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdInvoiceSupplierItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String transactionNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][InvoiceSupplierItemMetadata.ColumnNames.TransactionNo]);
            InvoiceSupplierItem entity = FindInvoiceSupplierItem(transactionNo);
            if (entity != null)
            {
                entity.MarkAsDeleted();
            }
            cboSupplierID.Enabled = !(InvoiceSupplierItems.Count > 0);
        }

        protected void grdInvoiceSupplierItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            InvoiceSupplierItem entity = InvoiceSupplierItems.AddNew();
            SetEntityValue(entity, e, true);
            cboSupplierID.Enabled = !(InvoiceSupplierItems.Count > 0);
        }

        private void SetEntityValue(InvoiceSupplierItem entity, GridCommandEventArgs e, bool isInsert)
        {
            var userControl = (InvoicingItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.TransactionNo = userControl.TransactionNo;
                entity.TransactionDate = userControl.TransactionDate;
                entity.Amount = userControl.Amount;
                entity.PPnAmount = userControl.PPnAmount;
                entity.IsPpnExcluded = userControl.IsPpnExcluded;
                entity.PPh22Amount = userControl.PPh22Amount;
                entity.PPh23Amount = userControl.PPh23Amount;
                entity.SRPph = userControl.SRPph;
                if (AppSession.Parameter.IsPphUsesAfixedValue)
                {
                    if (entity.PPh22Amount > 0)
                        entity.PphTypeName = "PPh 22";
                    else if (entity.PPh23Amount > 0)
                    {
                        entity.PphTypeName = "PPh 23";
                    }
                    else
                        entity.PphTypeName = "PPh";
                }
                else 
                {
                    entity.PphTypeName = userControl.PphTypeName;
                }
                entity.PphPercentage = userControl.PphPercentage;
                //if (entity.SRPph =="00")
                //{
                //    entity.PPh22Amount = userControl.PphAmount;
                //    entity.PPh23Amount = 0;
                //    entity.PphAmount = 0;
                //}
                //else if (entity.SRPph == "01" || entity.SRPph == "02")
                //{
                //    entity.PPh23Amount = userControl.PphAmount;
                //    entity.PPh22Amount = 0;
                //    entity.PphAmount = 0;
                //}
                //else
                //{
                //    entity.PPh22Amount = 0;
                //    entity.PPh23Amount = 0;
                //    entity.PphAmount = userControl.PphAmount;
                //}
                entity.PphAmount = userControl.PphAmount;
                entity.StampAmount = userControl.StampAmount;
                entity.OtherDeduction = userControl.OtherDeduction;
                entity.DownPaymentAmount = userControl.DownPaymentAmount;
                entity.Notes = userControl.Notes;
                entity.InvoiceSN = userControl.InvoiceSN;
                if (userControl.TaxInvoiceDate == null)
                    entity.str.TaxInvoiceDate = string.Empty;
                else
                    entity.TaxInvoiceDate = userControl.TaxInvoiceDate;

                if (isInsert)
                {
                    entity.IsAdditionalInvoice = true; // Additional Invoice
                    //var maxSeqNo = string.Empty;
                    //foreach (var item in InvoiceSupplierItems)
                    //{
                    //    if (item.IsAdditionalInvoice == true)
                    //    {
                    //        if (maxSeqNo.ToInt() < item.TransactionNo.ToInt())
                    //            maxSeqNo = item.TransactionNo;
                    //    }
                    //}
                    //entity.TransactionNo = string.Format("{0:000}", maxSeqNo.ToInt() + 1);

                    entity.TransactionNo = Common.Helper.Get8DigitsUnique();

                    entity.InvoiceSupplierDate = userControl.TransactionDate;
                }
                entity.ChartOfAccountId = entity.IsAdditionalInvoice == true ? userControl.ChartOfAccountId : 0;
                entity.ChartOfAccountCode = entity.IsAdditionalInvoice == true ? userControl.ChartOfAccountCodeText.Split('-')[0].Trim() : string.Empty;
                entity.ChartOfAccountName = entity.IsAdditionalInvoice == true ? userControl.ChartOfAccountCodeText.Split('-')[1].Trim() : string.Empty;
                entity.SubLedgerId = entity.IsAdditionalInvoice == true ? userControl.SubLedgerId : 0;
                entity.CurrencyID = userControl.CurrencyID;
                entity.CurrencyRate = userControl.CurrencyRate;
                entity.IsAllowEdit = userControl.TransactionNo.Length > 3;
                entity.SRItemType = userControl.SRItemType;
            }
        }

        #endregion

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument == "rebind")
            {
                grdInvoiceSupplierItem.Rebind();
                cboSupplierID.Enabled = InvoiceSupplierItems.Count == 0;
            }
        }
    }
}
