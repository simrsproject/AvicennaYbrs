using System;
using System.Collections.Generic;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.Core;
using System.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Receivable.Adjustment.WriteOff
{
    public partial class ARWriteOffDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ARWriteOffSearch.aspx";
            UrlPageList = "ARWriteOffList.aspx";

            ProgramID = AppConstant.Program.AR_WRITEOFF;
            WindowSearch.Height = 400;
            if (!IsPostBack)
            {              
                ViewState["IsApproved"] = false;
                StandardReference.InitializeIncludeSpace(cboSRARWriteOffType, AppEnum.StandardReference.ARWriteOffReason);
            }
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new Invoices();
            if (entity.LoadByPrimaryKey(txtInvoicePaymentNo.Text))
            {
                if (entity.IsPaymentApproved != null && entity.IsPaymentApproved.Value)
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
            var retval = (new Invoices()).WriteOffApproved(txtInvoicePaymentNo.Text, InvoicesItems, AppSession.UserLogin.UserID);
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
            var retval = (new Invoices()).PaymentUnApproved(txtInvoicePaymentNo.Text, InvoicesItems, AppSession.UserLogin.UserID);
            if (retval != string.Empty)
            {
                args.MessageText = retval;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            (new Invoices()).PaymentVoid(txtInvoicePaymentNo.Text, AppSession.UserLogin.UserID);
        }

        protected override void OnMenuNewClick()
        {

            cboGuarantorID.DataSource = null;
            cboGuarantorID.DataBind();
            cboGuarantorID.SelectedValue = string.Empty;
            cboGuarantorID.Text = string.Empty;


            OnPopulateEntryControl(new Invoices());


            PopulateNewInvoiceNo();
            txtInvoiceNo.ShowButton = true;
            txtPaymentDate.SelectedDate = DateTime.Now.Date;
            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        private void PopulateNewInvoiceNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New) return;

            _autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.InvoiceARNo);
            txtInvoicePaymentNo.Text = _autoNumber.LastCompleteNumber;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            PopulateNewInvoiceNo();
            var entity = new Invoices();
            if (entity.LoadByPrimaryKey(txtInvoicePaymentNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }


            entity = new Invoices();
            entity.AddNew();
            SetEntityValue(entity);
            if (InvoicesItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new Invoices();
            if (entity.LoadByPrimaryKey(txtInvoicePaymentNo.Text))
            {
               
              
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
            auditLogFilter.TableName = "Invoices";
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
            var entity = new Invoices();
            if (parameters.Length > 0)
            {
                var invoiceNo = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(invoiceNo);
            }
            else
                entity.LoadByPrimaryKey(txtInvoicePaymentNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var invoices = (Invoices)entity;
            txtInvoicePaymentNo.Text = invoices.InvoiceNo;

            var query = new GuarantorQuery();
            query.Select(
                query.GuarantorID,
                query.GuarantorName,
                (query.StreetName + " " + query.City + " " + query.ZipCode).Trim().As("Address")
                );
            query.Where(query.GuarantorID == invoices.str.GuarantorID);


            cboGuarantorID.DataSource = query.LoadDataTable();
            cboGuarantorID.DataBind();
            cboGuarantorID.SelectedValue = invoices.GuarantorID;


            txtInvoiceNo.Text = invoices.InvoiceReferenceNo;
            txtInvoiceDate.SelectedDate = invoices.InvoiceDate;
            txtNotes.Text = invoices.InvoiceNotes;
            cboSRARWriteOffType.SelectedValue = invoices.Reason;

            txtPaymentDate.SelectedDate = invoices.PaymentDate;
          

            ViewState["IsApproved"] = invoices.IsApproved ?? false;
            ViewState["IsVoid"] = invoices.IsVoid ?? false;

            PopulateInvoicesItemGrid();
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new InvoicesQuery();
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

            var entity = new Invoices();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = InvoicesItems;
        }

        private InvoicesItemCollection InvoicesItems
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["InvoicesItems"];
                    if (obj != null)
                        return ((InvoicesItemCollection)(obj));
                }

                var coll = new InvoicesItemCollection();
                var query = new InvoicesItemQuery("a");
                var patQ = new PatientQuery("b");

                query.Select(
                    query,
                    (query.VerifyAmount - (query.PaymentAmount.Coalesce("'0'") + query.OtherAmount.Coalesce("'0'"))).As("refToInvoicesItem_BalanceAmount"),
                    patQ.MedicalNo.As("refPatientID_MedicalNo")
                    );

                query.LeftJoin(patQ).On(query.PatientID == patQ.PatientID);
                query.Where(query.InvoiceNo == txtInvoicePaymentNo.Text);
                query.OrderBy(query.PaymentNo.Ascending);

                coll.Load(query);
                Session["InvoicesItems"] = coll;
                return coll;
            }
            set { Session["InvoicesItems"] = value; }
        }

        protected void cboGuarantorID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new GuarantorQuery();

            query.Select(
                query.GuarantorID,
                query.GuarantorName,
                (query.StreetName + " " + query.City + " " + query.ZipCode).Trim().As("Address")
                );

            query.Where(
                query.Or(
                    query.GuarantorID.Like(searchText),
                    query.GuarantorName.Like(searchText)
                    )
                );

            query.es.Top = 20;
            cboGuarantorID.DataSource = query.LoadDataTable();
            cboGuarantorID.DataBind();
        }

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
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
                    var inv = new Invoices();
                    if (inv.LoadByPrimaryKey(txtInvoiceNo.Text))
                        txtInvoiceDate.SelectedDate = inv.InvoiceDate;

                    grdItem.Rebind();
                    break;
            }
        }

        protected void txtInvoiceNo_TextChanged(object sender, EventArgs e)
        {
            var inv = new Invoices();
            txtInvoiceDate.SelectedDate = inv.LoadByPrimaryKey(txtInvoiceNo.Text) ? inv.InvoiceDate : null;
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            var invoiceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][InvoicesItemMetadata.ColumnNames.InvoiceNo]);
            var paymentNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][InvoicesItemMetadata.ColumnNames.PaymentNo]);
            var invoiceReferenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][InvoicesItemMetadata.ColumnNames.InvoiceReferenceNo]);

            var entity = InvoicesItems.FindByPrimaryKey(invoiceNo, paymentNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        private void RefreshCommandItemInvoicesItem(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = (newVal != AppEnum.DataMode.Read);
        }

        private void PopulateInvoicesItemGrid()
        {
            //Display Data Detail
            InvoicesItems = null; //Reset Record Detail
            grdItem.DataSource = InvoicesItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemInvoicesItem(oldVal, newVal);
        }

        private void SetEntityValue(esInvoices entity)
        {
            if (entity.es.IsAdded)
                PopulateNewInvoiceNo();
            entity.InvoiceNo = txtInvoicePaymentNo.Text;

            var en = new Invoices();
            en.LoadByPrimaryKey(txtInvoiceNo.Text);

            entity.SRReceivableType = en.SRReceivableType;
            entity.InvoiceDate = txtPaymentDate.SelectedDate.Value;
            entity.str.InvoiceDueDate = string.Empty;
            entity.InvoiceTOP = 0;
            entity.GuarantorID = cboGuarantorID.SelectedValue;
            entity.InvoiceNotes = txtNotes.Text;
            entity.PaymentDate = txtPaymentDate.SelectedDate.Value;
            entity.SRReceivableStatus = AppSession.Parameter.ReceivableStatusPaid;
            entity.IsInvoicePayment = true;
            entity.IsWriteOff = true;
            entity.Reason = cboSRARWriteOffType.SelectedValue;
            entity.InvoiceReferenceNo = txtInvoiceNo.Text;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;

            //Update Detil
            foreach (var item in InvoicesItems)
            {
                item.InvoiceNo = txtInvoicePaymentNo.Text;
                item.InvoiceReferenceNo = txtInvoiceNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(esEntity entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                InvoicesItems.Save();

                if (DataModeCurrent == AppEnum.DataMode.New)
                    _autoNumber.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
     
        }
    }
}
