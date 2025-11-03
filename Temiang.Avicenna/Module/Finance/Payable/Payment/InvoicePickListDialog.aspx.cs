using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
namespace Temiang.Avicenna.Module.Finance.Payable
{
    public partial class InvoicePickListDialog : BasePageDialog
    {
        private double _sum = 0;

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AP_PAYMENT;

            if (!IsPostBack)
            {
                var supp = new Supplier();
                supp.LoadByPrimaryKey(Request.QueryString["supp"]);
                Title = "Invoice List : " + supp.SupplierName;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var query = new InvoiceSupplierItemQuery("b");

            var hdr = new InvoiceSupplierQuery("a");
            query.InnerJoin(hdr).On(query.InvoiceNo == hdr.InvoiceNo);

            var bank = new BankQuery("c");
            query.LeftJoin(bank).On(hdr.BankID == bank.BankID);

            query.es.Distinct = true;
            query.Select(query.InvoiceNo, hdr.BankID, hdr.BankAccountNo, bank.BankName);
            query.Where(
                    hdr.SupplierID == Request.QueryString["supp"],
                    hdr.IsPaymentApproved.IsNull(),
                    hdr.IsInvoicePayment == false,
                    hdr.IsVoid == false, hdr.IsApproved == true 
                );

            if (AppSession.Parameter.IsClosingApAdvanceWithPayment)
                query.Where("<(ABS(b.VerifyAmount) > ABS(ISNULL(b.PaymentAmount, 0))) OR (b.VerifyAmount = 0 AND b.PaymentAmount IS NULL AND b.DownPaymentAmount > 0)>");
            else
            {
                if (AppSession.Parameter.IsClosingApZeroWithPayment)
                    query.Where("<(ABS(b.VerifyAmount) > ABS(ISNULL(b.PaymentAmount, 0))) OR (b.VerifyAmount = 0 AND b.PaymentAmount IS NULL)>");
                else
                    query.Where("<ABS(b.VerifyAmount) > ABS(ISNULL(b.PaymentAmount, 0))>");
            }
                
            DataTable dtb = query.LoadDataTable();

            grdList.DataSource = dtb;
        }

        //protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        //{
        //    base.RaisePostBackEvent(source, eventArgument);

        //    if (!(source is RadGrid) || string.IsNullOrEmpty(eventArgument))
        //        return;

        //    switch (((RadGrid)source).ID)
        //    {
        //        case "grdInvoicesDetail":

        //            break;
        //    }
        //}

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            var selected = false;

            foreach (GridDataItem dataItem in grdInvoicesDetail.MasterTableView.Items)
            {
                selected = ((CheckBox)dataItem.FindControl("itemChkbox")).Checked;
                if (selected)
                    break;
            }

            if (selected)
            {
                // Return first selected InvoiceNo
                foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
                {
                    selected = ((CheckBox)dataItem.FindControl("itemChkbox")).Checked;
                    if (selected)
                        return string.Format("oWnd.argument.id = '{0}'", dataItem["InvoiceNo"].Text);
                }
            }
                
            return string.Empty;
        }

        public override bool OnButtonOkClicked()
        {
            var coll = (InvoiceSupplierItemCollection)Session["InvoiceSupplierItemsPayment" + Request.UserHostName];
            coll.MarkAllAsDeleted();

            foreach (GridDataItem dataItem in grdInvoicesDetail.MasterTableView.Items)
            {
                if (((CheckBox)dataItem.FindControl("itemChkbox")).Checked)
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

                        var source = new InvoiceSupplierItem();
                        source.Query.Where(source.Query.InvoiceNo == dataItem["InvoiceNo"].Text, 
                            source.Query.TransactionNo == dataItem["TransactionNo"].Text);
                        source.Load(source.Query);

                        entity.TransactionDate = entity.PaymentDate = DateTime.ParseExact(dataItem["TransactionDate"].Text, "dd/MM/yyyy", CultureInfo.InvariantCulture); //DateTime.Parse(dataItem["TransactionDate"].Text);
                        entity.PaymentAmount = Convert.ToDecimal((dataItem.FindControl("txtPaymentAmount") as RadNumericTextBox).Value);

                        var balAmt = dataItem["BalanceAmount"].Text.ToDecimal();
                        entity.Amount = balAmt;
                        entity.VerifyAmount = balAmt;
                        entity.CurrencyID = dataItem["CurrencyID"].Text;
                        entity.CurrencyRate = Convert.ToDecimal((dataItem.FindControl("txtCurrencyRate") as RadNumericTextBox).Value);
                        entity.PPnAmount = 0;
                        entity.PPh22Amount = 0;
                        entity.PPh23Amount = 0;
                        entity.PphAmount = 0;
                        entity.StampAmount = 0;
                        entity.OtherDeduction = 0;
                        entity.DownPaymentAmount = 0;
                        entity.Notes = dataItem["Notes"].Text;
                        entity.SRItemType = source.SRItemType;
                        entity.RoundingAmount = dataItem["RoundingAmount"].Text.ToDecimal();

                        var por = new ItemTransaction();
                        if(por.LoadByPrimaryKey(entity.TransactionNo)){
                            entity.InvoiceSupplierNo = por.InvoiceNo;
                        }
                    }
                    else
                    {
                        entity.PaymentAmount = Convert.ToDecimal((dataItem.FindControl("txtPaymentAmount") as RadNumericTextBox).Value);
                        entity.CurrencyRate = Convert.ToDecimal((dataItem.FindControl("txtCurrencyRate") as RadNumericTextBox).Value);
                    }
                }
            }

            return true;
        }

        protected void grdInvoicesDetail_HeaderChkBoxCheckedChanged(object sender, EventArgs e)
        {
            var selected = ((CheckBox)sender).Checked;

            foreach (var dataItem in grdInvoicesDetail.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("itemChkbox")).Enabled))
            {
                ((CheckBox)dataItem.FindControl("itemChkbox")).Checked = selected;
            }
        }

        protected void grdInvoicesDetail_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                _sum += (dataItem["PaymentAmount"].FindControl("txtPaymentAmount") as RadNumericTextBox).Value.ToDouble();
            }
            else if (e.Item is GridFooterItem)
            {
                GridFooterItem footer = (GridFooterItem)e.Item;
                //footer["ShipCity"].Controls.Add(new LiteralControl("<span style='color: Black; font-weight: bold;'>Total freight on this page is:</span> "));
                (footer["PaymentAmount"].FindControl("txtSumPaymentAmount") as RadNumericTextBox).Value = _sum;
            }
        }


        protected void grdList_HeaderChkBoxCheckedChanged(object sender, EventArgs e)
        {
            var selected = ((CheckBox)sender).Checked;

            foreach (var dataItem in grdList.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("itemChkbox")).Enabled))
            {
                ((CheckBox)dataItem.FindControl("itemChkbox")).Checked = selected;
            }
            PopulateInvoiceDetailGrid();
        }

        protected void grdList_ItemChkBoxCheckedChanged(object sender, EventArgs e)
        {
            PopulateInvoiceDetailGrid();
        }

        private void PopulateInvoiceDetailGrid()
        {
            var invoices = new List<string>();
            foreach (var dataItem in grdList.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("itemChkbox")).Enabled))
            {
                if (((CheckBox) dataItem.FindControl("itemChkbox")).Checked == true)
                {
                    invoices.Add(dataItem["InvoiceNo"].Text);
                }
            }

            if (invoices.Count == 0) invoices.Add("xxxxx"); // dummy, prevent query error

            var query = new InvoiceSupplierItemQuery("a");

            query.Select(
                query.InvoiceNo,
                query.TransactionNo,
                query.InvoiceReferenceNo,
                query.TransactionDate,
                (query.VerifyAmount - query.PaymentAmount.Coalesce("'0'")).As("BalanceAmount"),
                query.RoundingAmount,
                query.CurrencyID,
                query.CurrencyRate,
                @"<CASE WHEN a.CurrencyID = 'IDR' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsCurrencyRate>",
                query.Notes
                );
            query.Where(query.InvoiceNo.In(invoices), query.VerifyDate.IsNotNull());

            if (AppSession.Parameter.IsClosingApAdvanceWithPayment)
                query.Where("<((ABS(a.VerifyAmount) - ABS(ISNULL(a.PaymentAmount, 0))) > 0) OR (a.VerifyAmount = 0 AND a.PaymentAmount IS NULL AND a.DownPaymentAmount > 0)>");
            else
            {
                if (AppSession.Parameter.IsClosingApZeroWithPayment)
                    query.Where("<((ABS(a.VerifyAmount) - ABS(ISNULL(a.PaymentAmount, 0))) > 0) OR (a.VerifyAmount = 0 AND a.PaymentAmount IS NULL)>");
                else
                    query.Where("<(ABS(a.VerifyAmount) - ABS(ISNULL(a.PaymentAmount, 0))) > 0>");
            }
                
            query.OrderBy(query.TransactionNo.Ascending);

            grdInvoicesDetail.DataSource = query.LoadDataTable();
            grdInvoicesDetail.DataBind();
        }
    }
}
