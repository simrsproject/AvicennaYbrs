using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Receivable.Customer
{
    public partial class InvoicePickListDialog : BasePageDialog
    {
        private double _sum = 0;

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AR_CUSTOMER_PAYMENT;

            if (!IsPostBack)
            {
                var cust = new BusinessObject.Customer();
                cust.LoadByPrimaryKey(Request.QueryString["grr"]);
                Title = "Invoice List : " + cust.CustomerName;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var query = new InvoiceCustomerQuery("a");
            var detail = new InvoiceCustomerItemQuery("b");
            var cust = new CustomerQuery("c");

            detail.es.Distinct = true;
            detail.Select(detail.InvoiceNo);
            detail.InnerJoin(query).On(detail.InvoiceNo == query.InvoiceNo);
            detail.InnerJoin(cust).On(query.CustomerID == cust.CustomerID);
            detail.Where(
                    cust.CustomerID == Request.QueryString["grr"],
                    query.SRReceivableStatus == AppSession.Parameter.ReceivableStatusVerify,
                    query.IsPaymentApproved.IsNull(),
                    query.IsInvoicePayment == false
                );
            detail.Where("< b.VerifyAmount <> (ISNULL(b.PaymentAmount, 0) + ISNULL(b.OtherAmount,0) + ISNULL(b.BankCost,0)) >");

            var coll = new InvoiceCustomerItemCollection();
            coll.Load(detail);

            grdList.DataSource = coll;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (!(source is RadGrid) || string.IsNullOrEmpty(eventArgument))
                return;

            switch (((RadGrid)source).ID)
            {
                case "grdInvoicesDetail":
                    string isChecked = AppSession.Parameter.DefaultChecklistArPayment;

                    var query = new InvoiceCustomerItemQuery("a");
                    
                    query.Select(
                        query.InvoiceNo,
                        query.TransactionNo,
                        query.InvoiceReferenceNo,
                        query.TransactionDate,
                        query.VerifyAmount,
                        (query.VerifyAmount - (query.PaymentAmount.Coalesce("'0'") + query.OtherAmount.Coalesce("'0'") + query.BankCost.Coalesce("'0'"))).As("BalanceAmount"),
                        (query.VerifyAmount - (query.PaymentAmount.Coalesce("'0'") + query.OtherAmount.Coalesce("'0'") + query.BankCost.Coalesce("'0'"))).As("PaymentAmount"),
                        query.OtherAmount.Coalesce("'0'"),
                        query.BankCost.Coalesce("'0'"),
                        @"<CAST('" + isChecked + "' AS BIT) AS 'IsChecked'>"
                        );
                    query.Where(query.InvoiceNo == eventArgument);
                    query.Where("<(a.VerifyAmount - (ISNULL(a.PaymentAmount, 0) + ISNULL(a.OtherAmount, 0) + ISNULL(a.BankCost,0))) <> 0>");
                    query.OrderBy(query.TransactionDate.Ascending, query.TransactionNo.Ascending);

                    grdInvoicesDetail.DataSource = query.LoadDataTable();
                    grdInvoicesDetail.DataBind();
                    break;
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            var selected = false;

            foreach (GridDataItem dataItem in grdInvoicesDetail.MasterTableView.Items)
            {
                selected = ((CheckBox)dataItem.FindControl("detailChkbox")).Checked;
                if (selected)
                    break;
            }

            if (selected)
                return "oWnd.argument.id = '" + grdList.SelectedValue + "'";

            return string.Empty;
        }

        public override bool OnButtonOkClicked()
        {
            var coll = (InvoiceCustomerItemCollection)Session["InvoiceCustomerItemPayments" + Request.UserHostName];
            coll.MarkAllAsDeleted();

            foreach (GridDataItem dataItem in grdInvoicesDetail.MasterTableView.Items)
            {
                if (((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                {
                    var entity = (coll.Where(i => i.InvoiceNo == dataItem["InvoiceNo"].Text &&
                                                  i.TransactionNo == dataItem["TransactionNo"].Text &&
                                                  i.InvoiceNo == (dataItem["InvoiceReferenceNo"].Text == "&nbsp;" ? string.Empty : dataItem["InvoiceReferenceNo"].Text))).SingleOrDefault();
                    if (entity == null)
                    {
                        entity = coll.AddNew();
                        entity.InvoiceNo = string.Empty;
                        entity.TransactionNo = dataItem["TransactionNo"].Text;
                        entity.InvoiceReferenceNo = dataItem["InvoiceNo"].Text;

                        var source = new ItemTransaction();
                        source.LoadByPrimaryKey(dataItem["TransactionNo"].Text);

                        entity.TransactionDate = source.TransactionDate;
                        entity.PaymentAmount = Convert.ToDecimal((dataItem.FindControl("txtPaymentAmount") as RadNumericTextBox).Value);
                        entity.BalanceAmount = entity.PaymentAmount;
                        entity.Amount = Convert.ToDecimal(dataItem["BalanceAmount"].Text);
                        entity.VerifyAmount = Convert.ToDecimal(dataItem["BalanceAmount"].Text);
                        entity.OtherAmount = Convert.ToDecimal((dataItem.FindControl("txtOtherAmount") as RadNumericTextBox).Value);
                        entity.BankCost = Convert.ToDecimal((dataItem.FindControl("txtBankCost") as RadNumericTextBox).Value);
                        if (entity.PaymentAmount + entity.OtherAmount + entity.BankCost > entity.Amount)
                        {
                            entity.PaymentAmount = entity.Amount - entity.OtherAmount - entity.BankCost;
                        }
                    }
                    else
                    {
                        entity.PaymentAmount = Convert.ToDecimal((dataItem.FindControl("txtPaymentAmount") as RadNumericTextBox).Value);
                        entity.OtherAmount = Convert.ToDecimal((dataItem.FindControl("txtOtherAmount") as RadNumericTextBox).Value);
                        entity.BankCost = Convert.ToDecimal((dataItem.FindControl("txtBankCost") as RadNumericTextBox).Value);
                        if (entity.PaymentAmount + entity.OtherAmount + entity.BankCost > entity.Amount)
                        {
                            entity.PaymentAmount = entity.Amount - entity.OtherAmount - entity.BankCost;
                        }
                    }
                }
            }

            return true;
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            var selected = ((CheckBox)sender).Checked;

            foreach (var dataItem in grdInvoicesDetail.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Enabled))
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }

        protected void btnProcessGlobalDiscount_Click(object sender, ImageClickEventArgs e)
        {
            string isChecked = AppSession.Parameter.DefaultChecklistArPayment;

            var query = new InvoiceCustomerItemQuery("a");
            
            query.Select(
                query.InvoiceNo,
                query.TransactionNo,
                query.InvoiceReferenceNo,
                query.TransactionDate,
                query.VerifyAmount,
                (query.VerifyAmount - (query.PaymentAmount.Coalesce("'0'") + query.OtherAmount.Coalesce("'0'") + query.BankCost.Coalesce("'0'"))).As("BalanceAmount"),
                (query.VerifyAmount - (query.PaymentAmount.Coalesce("'0'") + query.OtherAmount.Coalesce("'0'") + query.BankCost.Coalesce("'0'"))).As("PaymentAmount"),
                query.OtherAmount.Coalesce("'0'"),
                query.BankCost.Coalesce("'0'"),
                @"<CAST('" + isChecked + "' AS BIT) AS 'IsChecked'>"
                );
            query.Where(query.InvoiceNo == grdList.SelectedValue);
            query.Where("<(VerifyAmount - (ISNULL(PaymentAmount, 0) + ISNULL(OtherAmount, 0) + ISNULL(BankCost,0))) <> 0>");
            query.OrderBy(query.TransactionDate.Ascending, query.TransactionNo.Ascending);

            var dtb = query.LoadDataTable();

            foreach (DataRow row in dtb.Rows)
            {
                row["BankCost"] = txtBankCost.Value;
                row["PaymentAmount"] = Convert.ToDouble(row["BalanceAmount"]) - Convert.ToDouble(row["OtherAmount"]) - Convert.ToDouble(row["BankCost"]);
            }

            dtb.AcceptChanges();

            grdInvoicesDetail.DataSource = dtb;
            grdInvoicesDetail.DataBind();
        }

        protected void grdInvoicesDetail_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                _sum += double.Parse((dataItem["PaymentAmount"].FindControl("txtPaymentAmount") as RadNumericTextBox).Value.ToString());
            }
            else if (e.Item is GridFooterItem)
            {
                GridFooterItem footer = (GridFooterItem)e.Item;
                //footer["ShipCity"].Controls.Add(new LiteralControl("<span style='color: Black; font-weight: bold;'>Total freight on this page is:</span> "));
                (footer["PaymentAmount"].FindControl("txtSumPaymentAmount") as RadNumericTextBox).Value = Double.Parse(_sum.ToString());
            }
        }
    }
}
