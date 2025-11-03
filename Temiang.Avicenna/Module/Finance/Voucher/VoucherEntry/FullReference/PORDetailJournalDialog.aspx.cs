using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry
{
    public partial class PORDetailJournalDialog : BasePageDialog
    {
        private string RefNo {
            get { return Request.QueryString["RefNo"]; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.VOUCHER_MEMORIAL;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                grdVoucherEntryItem.Columns[0].Visible = false;
                grdVoucherEntryItem.Columns[grdVoucherEntryItem.Columns.Count - 1].Visible = false;
                grdVoucherEntryItem.MasterTableView.CommandItemDisplay = false ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            
            }
        }

        public override bool OnButtonOkClicked()
        {
            return true;   
        }

        protected void GenerateGrid()
        {
            
        }

        protected void grdVoucherEntryItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            // POR
            var jPORColl = new JournalTransactionDetailsCollection();
            jPORColl.LoadByRefferenceNo(RefNo);

            List<VoucherEntryDetail.GridItem> items = new List<VoucherEntryDetail.GridItem>();
            foreach (JournalTransactionDetails jtd in jPORColl)
                items.Add(new VoucherEntryDetail.GridItem(jtd));

            // Invoice
            var ipColl = new InvoiceSupplierCollection();
            var ip = new InvoiceSupplierQuery("ip");
            var ipi = new InvoiceSupplierItemQuery("ipi");
            ip.InnerJoin(ipi).On(ip.InvoiceNo == ipi.InvoiceNo)
                .Where(ipi.TransactionNo == RefNo, "<ISNULL(ip.IsInvoicePayment, 0) = 0>")
                .Select(ip);
            ipColl.Load(ip);

            foreach (var iv in ipColl)
            {
                var jIvColl = new JournalTransactionDetailsCollection();
                jIvColl.LoadByRefferenceNo(iv.InvoiceNo);
                foreach (JournalTransactionDetails jtd in jIvColl)
                    items.Add(new VoucherEntryDetail.GridItem(jtd));
            }

            // Invoice Payment
            var ipayColl = new InvoiceSupplierCollection();
            var ipay = new InvoiceSupplierQuery("ipay");
            var ipayi = new InvoiceSupplierItemQuery("ipayi");
            ipay.InnerJoin(ipayi).On(ipay.InvoiceNo == ipayi.InvoiceNo)
                .Where(ipayi.TransactionNo == RefNo, ipay.IsInvoicePayment == true)
                .Select(ipay);
            ipayColl.Load(ipay);

            foreach (var iv in ipayColl)
            {
                var jIvColl = new JournalTransactionDetailsCollection();
                jIvColl.LoadByRefferenceNo(iv.InvoiceNo);
                foreach (JournalTransactionDetails jtd in jIvColl)
                    items.Add(new VoucherEntryDetail.GridItem(jtd));
            }

            this.grdVoucherEntryItem.DataSource = items;

            //grdVoucherEntryItem.DataSource = jtdColl;
        }

        protected void grdVoucherEntryItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
        //    VoucherEntryItemDetail ctl = (VoucherEntryItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
        //    if (ctl == null) return;

        //    if (!this.IsEnableEdit(this.JournalId))
        //    {
        //        e.Canceled = true;
        //        return;
        //    }

        //    JournalTransactionDetails entity = JournalTransactionDetails.Get(this.JournalId, ctl.DetailId);
        //    if (entity != null)
        //    {
        //        entity.ChartOfAccountId = ctl.ChartOfAccountId;
        //        entity.Debit = ctl.Debit;
        //        entity.Credit = ctl.Credit;
        //        entity.Description = ctl.Description;
        //        entity.SubLedgerId = ctl.SubLedgerId;
        //        entity.DocumentNumber = ctl.DocumentNumber;
        //        entity.Save();
        //    }
        //    grdVoucherEntryItem.Rebind();
        }

        protected void grdVoucherEntryItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            //GridDataItem item = e.Item as GridDataItem;
            //if (item == null) return;

            //if (!this.IsEnableEdit(this.JournalId))
            //{
            //    e.Canceled = true;
            //    return;
            //}

            //int id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][JournalTransactionDetailsMetadata.ColumnNames.DetailId]);
            //JournalTransactionDetails entity = JournalTransactionDetails.Get(this.JournalId, id);
            //if (entity != null)
            //{
            //    entity.MarkAsDeleted();
            //    entity.Save();
            //}
            //grdVoucherEntryItem.Rebind();
        }

        protected void grdVoucherEntryItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
            {
                e.Canceled = true;
                return;
            }

            //VoucherEntryItemDetail ctl = (VoucherEntryItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            //if (ctl == null) return;

            //int accId = this.validateChartOfAccount(ctl);
            //if (accId == 0)
            //{
            //    e.Canceled = true;
            //    return;
            //}

            //bool newlyCreated = false;
            //if (this.JournalId == 0)
            //{
            //    bool isValid = false;

            //    JournalCodes journalCode = JournalCodes.Get(txtJournalCode.Text);
            //    isValid = ((journalCode != null) && (journalCode.IsEnabled ?? false));
            //    if (!isValid)
            //    {
            //        //args.MessageText = "Invalid Journal Code";
            //        e.Canceled = true;
            //        return;
            //    }

            //    if (!Helper.ValidatePeriode(txtTransactionDate.SelectedDate.Value))
            //    {
            //        //args.MessageText = "Invalid Transaction Date";
            //        e.Canceled = true;
            //        return;
            //    }

            //    JournalTransactions entity = new JournalTransactions();
            //    entity.AddNew();
            //    SetEntityValue(entity);
            //    if (!this.SaveEntity(entity))
            //    {
            //        //args.MessageText = "Unable to create new transaction please try again.";
            //        e.Canceled = true;
            //        return;
            //    }
            //    OnPopulateEntryControl(entity);
            //    OnDataModeChanged(DataMode.New, DataMode.Edit);

            //    newlyCreated = true;
            //}

            //if (!newlyCreated && !this.IsEnableEdit(this.JournalId))
            //{
            //    e.Canceled = true;
            //    return;
            //}

            //JournalTransactionDetails detail = new JournalTransactionDetails();
            //detail.JournalId = this.JournalId;
            //detail.ChartOfAccountId = accId;
            //detail.Debit = ctl.Debit;
            //detail.Credit = ctl.Credit;
            //detail.Description = ctl.Description;
            //detail.SubLedgerId = ctl.SubLedgerId;
            //detail.DocumentNumber = ctl.DocumentNumber;

            //detail.AddNew();
            //detail.Save();

            //e.Canceled = true;

            //grdVoucherEntryItem.Rebind();
        }
    }
}
