using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Core;

namespace Temiang.Avicenna.Module.Finance.Payable
{
    public partial class APWriteOffDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "APWriteOffSearch.aspx";
            UrlPageList = "APWriteOffList.aspx";

            ProgramID = AppConstant.Program.AP_WRITEOFF;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                ViewState["IsApproved"] = false;

                var coll = new PaymentMethodCollection();
                coll.Query.Where(coll.Query.SRPaymentTypeID == "PaymentType-006");
                coll.Query.OrderBy(coll.Query.SRPaymentMethodID.Ascending);
                coll.LoadAll();
                cboSRInvoicePayment.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var row in coll)
                {
                    cboSRInvoicePayment.Items.Add(new RadComboBoxItem(row.PaymentMethodName, row.SRPaymentMethodID));
                }

                var banks = new BankCollection();
                banks.Query.OrderBy(banks.Query.BankID.Ascending);
                banks.LoadAll();
                cboBankID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var row in banks)
                {
                    cboBankID.Items.Add(new RadComboBoxItem(row.BankName, row.BankID));
                }

                StandardReference.InitializeIncludeSpace(cboSRARWriteOffType, AppEnum.StandardReference.APWriteOffReason);
            }

        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new InvoiceSupplier();
            if (entity.LoadByPrimaryKey(txtInvoicePaymentNo.Text))
            {
                if (entity.IsApproved != null && entity.IsApproved.Value)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var retval = (new InvoiceSupplier()).APWriteOffApproved(txtInvoicePaymentNo.Text, InvoiceSupplierItems, AppSession.UserLogin.UserID);
            if (retval != string.Empty)
            {
                args.MessageText = retval;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var retval = (new InvoiceSupplier()).PaymentUnApproved(txtInvoicePaymentNo.Text, InvoiceSupplierItems, AppSession.UserLogin.UserID);
            if (retval != string.Empty)
            {
                args.MessageText = retval;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            (new InvoiceSupplier()).PaymentVoid(txtInvoicePaymentNo.Text, AppSession.UserLogin.UserID);
        }

        protected override void OnMenuNewClick()
        {

            cboSupplierID.DataSource = null;
            cboSupplierID.DataBind();
            cboSupplierID.SelectedValue = string.Empty;
            cboSupplierID.Text = string.Empty;

            OnPopulateEntryControl(new InvoiceSupplier());

            PopulateNewInvoiceNo();
            txtInvoiceNo.ShowButton = true;
            txtInvoiceDate.SelectedDate = null;
            txtPaymentDate.SelectedDate = DateTime.Now.Date;
            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        private void PopulateNewInvoiceNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New) return;

            _autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.InvoiceAPNo);
            txtInvoicePaymentNo.Text = _autoNumber.LastCompleteNumber;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            PopulateNewInvoiceNo();

            var entity = new InvoiceSupplier();
            if (entity.LoadByPrimaryKey(txtInvoicePaymentNo.Text))
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
            if (entity.LoadByPrimaryKey(txtInvoicePaymentNo.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("InvoiceNo='{0}'", txtInvoicePaymentNo.Text.Trim());
            auditLogFilter.TableName = "InvoiceSupplier";
        }

        public override bool OnGetStatusMenuEdit()
        {
            return txtInvoicePaymentNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !(bool)ViewState["IsApproved"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new InvoiceSupplier();
            if (parameters.Length > 0)
            {
                String invoiceNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(invoiceNo);
            }
            else
                entity.LoadByPrimaryKey(txtInvoicePaymentNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var invoiceSupplier = (InvoiceSupplier)entity;
            txtInvoicePaymentNo.Text = invoiceSupplier.InvoiceNo;

            if (!string.IsNullOrEmpty(invoiceSupplier.SupplierID))
            {
                var query = new SupplierQuery();
                query.Select(
                    query.SupplierID,
                    query.SupplierName);
                query.Where(query.SupplierID == invoiceSupplier.str.SupplierID);
                DataTable dtbg = query.LoadDataTable();
                cboSupplierID.DataSource = dtbg;
                cboSupplierID.DataBind();
                cboSupplierID.SelectedValue = invoiceSupplier.SupplierID;
                cboSupplierID.Text = dtbg.Rows[0]["SupplierName"].ToString();
            }
            else
            {
                cboSupplierID.Items.Clear();
                cboSupplierID.Text = string.Empty;
            }

            txtInvoiceNo.Text = invoiceSupplier.InvoiceReferenceNo;
            if (!string.IsNullOrEmpty(invoiceSupplier.InvoiceReferenceNo))
            {
                var invReff = new InvoiceSupplier();
                if (invReff.LoadByPrimaryKey(invoiceSupplier.InvoiceReferenceNo))
                    txtInvoiceDate.SelectedDate = invReff.InvoiceDate;
            }

            txtPaymentDate.SelectedDate = invoiceSupplier.InvoiceDate;
            txtNotes.Text = invoiceSupplier.InvoiceNotes;
            cboSRInvoicePayment.SelectedValue = invoiceSupplier.SRInvoicePayment;
            //pnlBank.Visible = invoiceSupplier.SRInvoicePayment != AppSession.Parameter.InvoicePaymentCash;
            cboBankID.SelectedValue = invoiceSupplier.BankID;
            txtBankAccountNo.Text = invoiceSupplier.BankAccountNo;
            cboSRARWriteOffType.SelectedValue = invoiceSupplier.Reason;

            ViewState["IsApproved"] = invoiceSupplier.IsApproved ?? false;
            ViewState["IsVoid"] = invoiceSupplier.IsVoid ?? false;

            PopulateInvoiceSupplierItemGrid();
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new InvoiceSupplierQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(
                    que.InvoiceNo > txtInvoicePaymentNo.Text,
                    que.IsInvoicePayment == true
                    );
                que.OrderBy(que.InvoiceNo.Ascending);
            }
            else
            {
                que.Where(
                    que.InvoiceNo < txtInvoicePaymentNo.Text,
                    que.IsInvoicePayment == true
                    );
                que.OrderBy(que.InvoiceNo.Descending);
            }
            var entity = new InvoiceSupplier();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = InvoiceSupplierItems;
        }

        private InvoiceSupplierItemCollection InvoiceSupplierItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["InvoiceSupplierItems"];
                    if (obj != null)
                        return ((InvoiceSupplierItemCollection)(obj));
                }

                var coll = new InvoiceSupplierItemCollection();
                var query = new InvoiceSupplierItemQuery("a");

                query.Select(
                    query,
                    (query.VerifyAmount - query.PaymentAmount.Coalesce("'0'")).As("refToInvoiceSupplierItem_BalanceAmount")
                    );

                query.Where(query.InvoiceNo == txtInvoicePaymentNo.Text);
                query.OrderBy(query.TransactionNo.Ascending);

                coll.Load(query);
                Session["InvoiceSupplierItems"] = coll;
                return coll;
            }
            set { Session["InvoiceSupplierItems"] = value; }
        }

        protected void cboSupplierID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new SupplierQuery();

            query.Select(
                query.SupplierID,
                query.SupplierName
                );

            query.Where(
                query.Or(
                    query.SupplierID.Like(searchText),
                    query.SupplierName.Like(searchText)
                    )
                );

            query.es.Top = 20;
            cboSupplierID.DataSource = query.LoadDataTable();
            cboSupplierID.DataBind();
        }

        protected void cboSupplierID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SupplierName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SupplierID"].ToString();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (!(sourceControl is RadGrid) || string.IsNullOrEmpty(eventArgument))
                return;

            switch (((RadGrid)sourceControl).ID)
            {
                case "grdItem":
                    txtInvoiceNo.Text = eventArgument;
                    var inv = new InvoiceSupplier();
                    if (inv.LoadByPrimaryKey(txtInvoiceNo.Text))
                    {
                        txtInvoiceDate.SelectedDate = inv.InvoiceDate;
                        
                        if (!string.IsNullOrEmpty(inv.SRInvoicePayment))
                        {
                            cboSRInvoicePayment.SelectedValue = inv.SRInvoicePayment;
                            //pnlBank.Visible = cboSRInvoicePayment.SelectedValue != AppSession.Parameter.InvoicePaymentCash;
                            cboBankID.SelectedValue = inv.BankID;
                        }
                    }

                    grdItem.Rebind();
                    break;
            }
        }

        protected void txtInvoiceNo_TextChanged(object sender, EventArgs e)
        {
            var inv = new InvoiceSupplier();
            if (inv.LoadByPrimaryKey(txtInvoiceNo.Text))
            {
                txtInvoiceDate.SelectedDate = inv.InvoiceDate;
                
                if (!string.IsNullOrEmpty(inv.SRInvoicePayment))
                {
                    cboSRInvoicePayment.SelectedValue = inv.SRInvoicePayment;
                    //pnlBank.Visible = cboSRInvoicePayment.SelectedValue != AppSession.Parameter.InvoicePaymentCash;
                    cboBankID.SelectedValue = inv.BankID;
                }
                else
                {
                    cboSRInvoicePayment.SelectedValue = string.Empty;
                    cboSRInvoicePayment.Text = string.Empty;
                    //pnlBank.Visible = true;
                    cboBankID.SelectedValue = string.Empty;
                    cboBankID.Text = string.Empty;
                }
            }
            else
            {
                txtInvoiceDate.SelectedDate = null;
                
                cboSRInvoicePayment.SelectedValue = string.Empty;
                cboSRInvoicePayment.Text = string.Empty;
                //pnlBank.Visible = true;
                cboBankID.SelectedValue = string.Empty;
                cboBankID.Text = string.Empty;
            }
        }

        protected void cboSRInvoicePayment_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //pnlBank.Visible = e.Value != AppSession.Parameter.InvoicePaymentCash;
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            String invoiceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][InvoiceSupplierItemMetadata.ColumnNames.InvoiceNo]);
            String transNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][InvoiceSupplierItemMetadata.ColumnNames.TransactionNo]);

            InvoiceSupplierItem entity = InvoiceSupplierItems.FindByPrimaryKey(invoiceNo, transNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        private void RefreshCommandItemInvoiceSupplierItem(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = (newVal != AppEnum.DataMode.Read);
        }

        private void PopulateInvoiceSupplierItemGrid()
        {
            //Display Data Detail
            InvoiceSupplierItems = null; //Reset Record Detail
            grdItem.DataSource = InvoiceSupplierItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemInvoiceSupplierItem(oldVal, newVal);
        }

        private void SetEntityValue(InvoiceSupplier entity)
        {
            if (entity.es.IsAdded)
                PopulateNewInvoiceNo();
            entity.InvoiceNo = txtInvoicePaymentNo.Text;

            entity.SRInvoicePayment = cboSRInvoicePayment.SelectedValue;
            entity.InvoiceDate = txtPaymentDate.SelectedDate.Value;
            entity.str.InvoiceDueDate = string.Empty;
            entity.InvoiceTOP = 0;
            entity.SupplierID = cboSupplierID.SelectedValue;
            entity.BankID = cboBankID.SelectedValue;
            entity.BankAccountNo = txtBankAccountNo.Text;
            entity.InvoiceNotes = txtNotes.Text;

            entity.SRPayableStatus = AppSession.Parameter.PayableStatusPaid;
            entity.IsInvoicePayment = true;
            entity.InvoiceReferenceNo = txtInvoiceNo.Text;
            entity.IsWriteOff = true;
            entity.Reason = cboSRARWriteOffType.SelectedValue;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //Update Detil
            foreach (InvoiceSupplierItem item in InvoiceSupplierItems)
            {
                item.InvoiceNo = txtInvoicePaymentNo.Text;
                item.InvoiceReferenceNo = txtInvoiceNo.Text;

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
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

    }
}
