using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI;
using static Temiang.Avicenna.Common.AppEnum;

namespace Temiang.Avicenna.Module.Finance.Receivable.Customer
{
    public partial class InvoicingDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private void PopulateNewInvoiceNo()
        {
            if (DataModeCurrent != DataMode.New) return;

            _autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.InvoiceARCustomerNo);
            txtInvoiceNo.Text = _autoNumber.LastCompleteNumber;
            
        }

        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "InvoicingSearch.aspx";
            UrlPageList = "InvoicingList.aspx";

            this.WindowSearch.Height = 400;
            ProgramID = AppConstant.Program.AR_CUSTOMER_INVOICING;


            if (!IsPostBack)
            {
                Common.StandardReference.InitializeIncludeSpace(cboSRReceivableStatus, AppEnum.StandardReference.ReceivableStatus);

                txtTransactionFromDate.SelectedDate = DateTime.Now;
                txtTransactionToDate.SelectedDate = DateTime.Now;
                chkIsAll.Checked = true;
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
            ajax.AddAjaxSetting(grdInvoicesItem, grdInvoicesItem);

            ajax.AddAjaxSetting(AjaxManager, txtInvoiceNo);
            ajax.AddAjaxSetting(AjaxManager, grdInvoicesItem);
            ajax.AddAjaxSetting(AjaxManager, btnGetAllItem);
            ajax.AddAjaxSetting(AjaxManager, btnClear);
            
            ajax.AddAjaxSetting(grdInvoicesItem, txtTotal);
            ajax.AddAjaxSetting(btnGetAllItem, grdInvoicesItem);
            ajax.AddAjaxSetting(btnGetAllItem, txtTotal);
            ajax.AddAjaxSetting(btnClear, grdInvoicesItem);
            ajax.AddAjaxSetting(btnClear, txtTotal);
        }
        #endregion

        #region ComboBox CustomerID

        protected void cboCustomerID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboCustomerID((RadComboBox)sender, e.Text);
        }

        private void PopulateCboCustomerID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new CustomerQuery("a");
            query.Select(query.CustomerID, query.CustomerName,
                         (query.StreetName + " " + query.City + " " + query.ZipCode).Trim().As("Address"));
            query.Where
                (
                    query.Or
                        (
                            query.CustomerID.Like(searchTextContain),
                            query.CustomerName.Like(searchTextContain)
                        )
                );

            query.es.Distinct = true;
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }

        protected void cboCustomerID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["CustomerName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["CustomerID"].ToString();
        }

        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID,
                                               PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("InvoiceNo", txtInvoiceNo.Text);
        }

        protected override void OnMenuEditClick()
        {
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            InvoiceCustomer entity = new InvoiceCustomer();
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
            (new InvoiceCustomer()).Approv(txtInvoiceNo.Text, AppSession.UserLogin.UserID, AppSession.Parameter.ReceivableStatusProcess);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new InvoiceCustomer();
            if (!entity.LoadByPrimaryKey(txtInvoiceNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            var inv = new InvoiceCustomerCollection();
            inv.Query.Where(inv.Query.InvoiceReferenceNo == txtInvoiceNo.Text, inv.Query.IsVoid == false);
            inv.LoadAll();
            if (inv.Count > 0)
            {
                args.MessageText = "The invoice has been processed to payment transaction.";
                args.IsCancel = true;
                return;
            }

            (new InvoiceCustomer()).UnApprov(txtInvoiceNo.Text, AppSession.UserLogin.UserID, AppSession.Parameter.ReceivableStatusProcess);
        }
        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            (new InvoiceCustomer()).Void(txtInvoiceNo.Text, AppSession.UserLogin.UserID);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            (new InvoiceCustomer()).UnVoid(txtInvoiceNo.Text, AppSession.UserLogin.UserID);
        }

        private bool IsApprovedOrVoid(InvoiceCustomer entity, ValidateArgs args)
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
            OnPopulateEntryControl(new InvoiceCustomer());
            PopulateNewInvoiceNo();

            txtInvoiceDate.SelectedDate = DateTime.Now;
            cboCustomerID.Text = string.Empty;
            txtTotal.Value = 0;

            btnGetItem.Enabled = true;
            btnGetAllItem.Enabled = true;
            btnClear.Enabled = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            InvoiceCustomer entity = new InvoiceCustomer();
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
            InvoiceCustomer entity = new InvoiceCustomer();
            if (entity.LoadByPrimaryKey(txtInvoiceNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new InvoiceCustomer();
            entity.AddNew();
            SetEntityValue(entity);
            if (InvoiceCustomerItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            InvoiceCustomer entity = new InvoiceCustomer();
            if (entity.LoadByPrimaryKey(txtInvoiceNo.Text))
            {
                SetEntityValue(entity);
                if (InvoiceCustomerItems.Count == 0)
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
            auditLogFilter.TableName = "InvoiceCustomer";
        }


        #endregion

        #region ToolBar Menu Support
        public override bool OnGetStatusMenuEdit()
        {
            return txtInvoiceNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !(bool)ViewState["IsApproved"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnDataModeChanged(DataMode oldVal, DataMode newVal)
        {
            RefreshCommandItemGrid(oldVal, newVal);

            btnGetItem.Enabled = newVal != DataMode.Read;
            btnGetAllItem.Enabled = newVal != DataMode.Read;
            btnClear.Enabled = newVal != DataMode.Read;

            if (newVal != DataMode.New)
            {
                txtInvoiceDate.Enabled = false;
                cboCustomerID.Enabled = false;

                if (oldVal == DataMode.New)
                {
                    cboCustomerID.Text = string.Empty;
                }
            }
            else
            {
                cboCustomerID.Text = string.Empty;
                txtInvoiceDate.Enabled = true;
                cboCustomerID.Enabled = true;
            }
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            InvoiceCustomer entity = new InvoiceCustomer();
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
            var invoices = (InvoiceCustomer)entity;
            txtInvoiceNo.Text = invoices.InvoiceNo;

            if (!string.IsNullOrEmpty(invoices.CustomerID))
            {
                var query = new CustomerQuery("a");
                query.Select(query.CustomerID, query.CustomerName,
                             (query.StreetName + " " + query.City + " " + query.ZipCode).Trim().As("Address"));
                query.Where(query.CustomerID == invoices.CustomerID);
                query.es.Distinct = true;
                DataTable dtb = query.LoadDataTable();

                cboCustomerID.DataSource = dtb;
                cboCustomerID.DataBind();
                cboCustomerID.SelectedValue = invoices.CustomerID;
                cboCustomerID.Text = dtb.Rows[0]["CustomerName"].ToString();
            }
            else
            {
                cboCustomerID.Items.Clear();
                cboCustomerID.Text = string.Empty;
            }
            
            txtInvoiceDate.SelectedDate = invoices.InvoiceDate;
            txtInvoiceDueDate.SelectedDate = invoices.InvoiceDueDate;
            txtTermOfPayment.Value = Convert.ToDouble(invoices.InvoiceTOP);
            txtNotes.Text = invoices.InvoiceNotes;
            cboSRReceivableStatus.SelectedValue = invoices.SRReceivableStatus;

            ViewState["IsVoid"] = invoices.IsVoid ?? false;
            ViewState["IsApproved"] = invoices.IsApproved ?? false;

            //Display Data Detail
            PopulateGridDetail();
            btnGetItem.Enabled = false;
            btnGetAllItem.Enabled = false;
            btnClear.Enabled = false;

            CalculateDetailTransaction();
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(InvoiceCustomer entity)
        {
            entity.InvoiceNo = txtInvoiceNo.Text;
            entity.CustomerID = cboCustomerID.SelectedValue;
            entity.InvoiceDate = txtInvoiceDate.SelectedDate;
            entity.InvoiceDueDate = txtInvoiceDueDate.SelectedDate;
            entity.InvoiceTOP = (decimal?)(txtTermOfPayment.Value ?? 0);
            entity.InvoiceNotes = txtNotes.Text;
            entity.IsInvoicePayment = false;
            entity.IsVoid = false;
            entity.IsApproved = false;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //Update Detil
            foreach (InvoiceCustomerItem item in InvoiceCustomerItems)
            {
                item.InvoiceNo = txtInvoiceNo.Text;
                item.VerifyAmount = item.Amount;
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void SaveEntity(InvoiceCustomer entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                InvoiceCustomerItems.Save();

                if (DataModeCurrent == DataMode.New)
                    _autoNumber.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            InvoiceCustomerQuery que = new InvoiceCustomerQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
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
            InvoiceCustomer entity = new InvoiceCustomer();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged
        protected void cboCustomerID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txtTermOfPayment.Value = int.Parse(AppSession.Parameter.InvoiceTermOfPayment);

            DateTime date = Convert.ToDateTime(txtInvoiceDate.SelectedDate);
            txtInvoiceDueDate.SelectedDate = date.AddDays((int)txtTermOfPayment.Value);
        }

        protected void txtTermOfPayment_TextChanged(object sender, EventArgs e)
        {
            DateTime date = Convert.ToDateTime(txtInvoiceDate.SelectedDate);
            txtInvoiceDueDate.SelectedDate = date.AddDays((int)txtTermOfPayment.Value);
        }

        #endregion

        #region Record Detail Method Function

        private void RefreshCommandItemGrid(DataMode oldVal, DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != DataMode.Read);
            grdInvoicesItem.Columns[0].Visible = isVisible;
            grdInvoicesItem.Columns[grdInvoicesItem.Columns.Count - 1].Visible = isVisible;

            //grdInvoicesItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != DataMode.Read)
                InvoiceCustomerItems = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdInvoicesItem.Rebind();
        }

        private InvoiceCustomerItemCollection InvoiceCustomerItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collInvoiceCustomerItem" + Request.UserHostName];
                    if (obj != null)
                        return ((InvoiceCustomerItemCollection)(obj));
                }

                var coll = new InvoiceCustomerItemCollection();
                var query = new InvoiceCustomerItemQuery("a");
                var itq = new ItemTransactionQuery("b");
                var ptq = new AppStandardReferenceItemQuery("c");

                query.InnerJoin(itq).On(itq.TransactionNo == query.TransactionNo);
                query.LeftJoin(ptq).On(ptq.StandardReferenceID == AppEnum.StandardReference.STBPaymentType.ToString() && ptq.ItemID == itq.SRPaymentType);
                query.Where(query.InvoiceNo == txtInvoiceNo.Text);
                query.OrderBy(query.TransactionDate.Ascending, query.TransactionNo.Ascending);

                query.Select
                    (
                        query.InvoiceNo,
                        query.TransactionNo,
                        query.TransactionDate,
                        ptq.ItemName.As("refToAppStd_STBPaymentType"),
                        query.Amount,
                        query.VerifyAmount,
                        query.Notes,
                        query.LastUpdateByUserID,
                        query.LastUpdateDateTime
                    );

                coll.Load(query);

                Session["collInvoiceCustomerItem" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collInvoiceCustomerItem" + Request.UserHostName] = value; }
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            InvoiceCustomerItems = null; //Reset Record Detail
            grdInvoicesItem.DataSource = InvoiceCustomerItems;
            grdInvoicesItem.MasterTableView.IsItemInserted = false;
            grdInvoicesItem.MasterTableView.ClearEditItems();
            grdInvoicesItem.DataBind();
        }

        protected void grdInvoicesItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdInvoicesItem.DataSource = InvoiceCustomerItems;
        }

        protected void grdInvoicesItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String transactionNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][InvoiceCustomerItemMetadata.ColumnNames.TransactionNo]);
            InvoiceCustomerItem entity = FindInvoicesItem(transactionNo);
            if (entity != null)
                SetEntityValue(entity, e);

            CalculateDetailTransaction();
        }

        private InvoiceCustomerItem FindInvoicesItem(String transactionNo)
        {
            InvoiceCustomerItemCollection coll = InvoiceCustomerItems;
            InvoiceCustomerItem retEntity = null;
            foreach (InvoiceCustomerItem rec in coll)
            {
                if (rec.TransactionNo.Equals(transactionNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdInvoicesItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String transactionNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][InvoiceCustomerItemMetadata.ColumnNames.TransactionNo]);
            InvoiceCustomerItem entity = FindInvoicesItem(transactionNo);
            if (entity != null)
            {
                entity.MarkAsDeleted();
            }

            CalculateDetailTransaction();
        }

        protected void grdInvoicesItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            InvoiceCustomerItem entity = InvoiceCustomerItems.AddNew();
            SetEntityValue(entity, e);
        }

        private void SetEntityValue(InvoiceCustomerItem entity, GridCommandEventArgs e)
        {
            InvoicingItemDetail userControl = (InvoicingItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.TransactionNo = userControl.TransactionNo;
                entity.TransactionDate = userControl.TransactionDate;
                entity.Amount = userControl.Amount;
                entity.VerifyAmount = userControl.Amount;
                entity.Notes = userControl.Notes;
            }
        }

        #endregion

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
            ToolBarMenuEdit.Enabled = !(bool)ViewState["IsApproved"];
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument == "rebind")
            {
                CalculateDetailTransaction();
                grdInvoicesItem.Rebind();
            }
        }

        protected void btnGetAllItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cboCustomerID.SelectedValue))
            {
                DataTable dtb;
                if (chkIsAll.Checked)
                {
                    dtb =
                    (new InvoiceCustomerCollection()).ItemTransactionOutstandingWithParameter(cboCustomerID.SelectedValue, null, null);
                }
                else
                {
                    dtb =
                    (new InvoiceCustomerCollection()).ItemTransactionOutstandingWithParameter(cboCustomerID.SelectedValue, txtTransactionFromDate.SelectedDate, txtTransactionToDate.SelectedDate);
                }

                var invoice = (InvoiceCustomerItemCollection)Session["collInvoiceCustomerItem" + Request.UserHostName];

                foreach (DataRow row in dtb.Rows)
                {
                    var entity = invoice.AddNew();

                    entity.InvoiceNo = txtInvoiceNo.Text;
                    entity.TransactionNo = row["TransactionNo"].ToString();
                    entity.TransactionDate = Convert.ToDateTime(row["TransactionDate"]);
                    entity.Amount = Convert.ToDecimal(row["Amount"]);
                    entity.PaymentTypeName = row["PaymentTypeName"].ToString();
                    entity.LastUpdateDateTime = DateTime.Now;
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }
                CalculateDetailTransaction();
                grdInvoicesItem.Rebind();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            if (InvoiceCustomerItems.Count > 0)
                InvoiceCustomerItems.MarkAllAsDeleted();

            grdInvoicesItem.DataSource = InvoiceCustomerItems;
            grdInvoicesItem.DataBind();

            txtTotal.Value = 0;
        }

        private void CalculateDetailTransaction()
        {
            if (InvoiceCustomerItems.Count > 0)
            {
                decimal? totaltransaction = 0;

                foreach (var item in InvoiceCustomerItems)
                {
                    totaltransaction += item.Amount;
                } 

                txtTotal.Value = Convert.ToDouble(totaltransaction);
            }
        }
    }
}
