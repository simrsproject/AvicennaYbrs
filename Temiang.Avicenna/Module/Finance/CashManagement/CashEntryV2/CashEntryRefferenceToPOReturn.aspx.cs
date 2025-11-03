using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.CashManagement.CashEntryV2
{
    public partial class CashEntryRefferenceToPOReturn : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CASH_ENTRY;

            if (!IsPostBack)
            {
                ViewState["InvoiceNo"] = string.Empty;

                var sups = new SupplierCollection();
                sups.Query.Where(sups.Query.IsActive == true);
                sups.Query.Load();

                cboSupplierID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var sup in sups)
                {
                    cboSupplierID.Items.Add(new RadComboBoxItem(sup.SupplierName, sup.SupplierID));
                }

                txtReturnDate.SelectedDate = DateTime.Now;
            }
        }

        private DataTable ItemTransactions
        {
            get
            {
                if (Request.QueryString["type"] == "1")
                    return (new InvoiceSupplierCollection()).ItemTransactionOutstandingWithParameter(txtReturnDate.SelectedDate, txtTransactionNo.Text, cboSupplierID.SelectedValue, BusinessObject.Reference.TransactionCode.PurchaseOrderReturn, AppSession.Parameter.HealthcareInitialAppsVersion);
                else if (Request.QueryString["type"] == "2")
                    return (new InvoiceSupplierCollection()).ItemTransactionOutstandingWithParameter(txtReturnDate.SelectedDate, txtTransactionNo.Text, cboSupplierID.SelectedValue, BusinessObject.Reference.TransactionCode.PurchaseOrderReceive, AppSession.Parameter.HealthcareInitialAppsVersion);
                else
                    return (new InvoiceSupplierCollection()).ItemTransactionOutstandingWithParameter(txtReturnDate.SelectedDate, txtTransactionNo.Text, cboSupplierID.SelectedValue, BusinessObject.Reference.TransactionCode.PurchaseOrder, AppSession.Parameter.HealthcareInitialAppsVersion);
            }
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = ItemTransactions;
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["Detail"] != null) grdDetail.DataSource = ViewState["Detail"];
        }

        protected void grdDetail_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateDataSourceDetail();
        }

        private void UpdateDataSourceDetail()
        {
            DataTable dtb = (DataTable)ViewState["Detail"];
            foreach (Telerik.Web.UI.GridDataItem dataItem in grdDetail.MasterTableView.Items)
            {
                string seqNo = dataItem.GetDataKeyValue("SequenceNo").ToString();
                string batchNumber = ((Telerik.Web.UI.RadTextBox)dataItem.FindControl("txtBatchNumber")).Text ?? string.Empty;
                DateTime? expiredDate = ((Telerik.Web.UI.RadDatePicker)dataItem.FindControl("txtExpiredDate")).SelectedDate;

                foreach (DataRow row in dtb.Rows)
                {
                    if (row["SequenceNo"].Equals(seqNo))
                    {
                        row["BatchNumber"] = batchNumber;
                        if (expiredDate != null) row["ExpiredDate"] = expiredDate;
                        break;
                    }
                }

                ViewState["Detail"] = dtb;
            }
        }

        private void InitializeDataDetail(string transactionNo)
        {
            DataTable dtb;

            using (new Temiang.Dal.Interfaces.esTransactionScope())
            {
                var itemTransaction = new ItemTransaction();
                itemTransaction.LoadByPrimaryKey(transactionNo);

                var query = new ItemTransactionItemQuery("a");
                var iq = new ItemQuery("b");
                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
                if (itemTransaction.SRItemType == BusinessObject.Reference.ItemType.Medical)
                {
                    var itemDetil = new ItemProductMedicQuery("c");
                    query.LeftJoin(itemDetil).On(query.ItemID == itemDetil.ItemID);
                }
                else if (itemTransaction.SRItemType == BusinessObject.Reference.ItemType.NonMedical)
                {
                    var itemDetil = new ItemProductNonMedicQuery("c");
                    query.LeftJoin(itemDetil).On(query.ItemID == itemDetil.ItemID);
                }
                else
                {
                    var itemDetil = new ItemKitchenQuery("c");
                    query.LeftJoin(itemDetil).On(query.ItemID == itemDetil.ItemID);
                }

                query.Where(query.TransactionNo == transactionNo);
                query.OrderBy(query.ItemID.Ascending);

                query.Select
                    (
                    query.TransactionNo,
                    query.SequenceNo,
                    query.ItemID,
                    query.SRItemUnit,
                    query.Quantity,
                    query.BatchNumber,
                    query.ExpiredDate,
                    query.Price,
                    query.Discount1Percentage,
                    query.Discount2Percentage,
                    query.Discount,
                    query.ConversionFactor,
                    iq.ItemName,
                    "<ISNULL(c.SRItemUnit, a.SRItemUnit) AS BaseUnitID>"
                    );
                dtb = query.LoadDataTable();
            }

            ViewState["Detail"] = dtb;
            grdDetail.DataSource = dtb;
            grdDetail.DataBind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);
            if (!(source is Telerik.Web.UI.RadGrid)) return;
            Telerik.Web.UI.RadGrid grd = (Telerik.Web.UI.RadGrid)source;
            switch (grd.ID.ToLower())
            {
                case "grddetail": // Populate Detail
                    string[] pars = eventArgument.Split('|');
                    //string transNo = pars[0].Split(':')[1];
                    string transNo = pars[0].ToString();
                    InitializeDataDetail(transNo);
                    break;
                case "grditem": // 
                    var command = eventArgument.Split(':')[0];
                    switch (command)
                    {
                        case "rebind":
                            grdItem.Rebind();

                            ViewState["Detail"] = null;
                            grdDetail.DataSource = null;
                            grdDetail.DataBind();
                            break;
                    }
                    break;
            }
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            bool selected = ((CheckBox)sender).Checked;

            foreach (Telerik.Web.UI.GridDataItem dataItem in grdItem.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            string str = "";
            if (grdItem.SelectedValue == null) str = "'rebind'";
            else
            {
                if (Request.QueryString["type"] == "1") str = "'" + grdItem.SelectedValue.ToString() + "||ReturnPO'";
                else if (Request.QueryString["type"] == "2") str = "'" + grdItem.SelectedValue.ToString() + "||ReceivePO'";
                else str = "'" + grdItem.SelectedValue.ToString() + "||PO'";
            }
            return "oWnd.argument.init = " + str;
        }

        public override bool OnButtonOkClicked()
        {
            if (grdItem.SelectedValue == null)
                return false;
            else
                return true;
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdItem.Rebind();

            ViewState["Detail"] = null;
            grdDetail.DataSource = null;
            grdDetail.DataBind();
        }
    }
}
