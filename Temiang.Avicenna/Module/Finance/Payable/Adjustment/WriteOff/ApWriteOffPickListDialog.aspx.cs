using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Globalization;

namespace Temiang.Avicenna.Module.Finance.Payable
{
    public partial class ApWriteOffPickListDialog : BasePageDialog
    {
        private double _sum = 0;

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AP_WRITEOFF;

            if (!IsPostBack)
            {
                var supp = new Supplier();
                supp.LoadByPrimaryKey(Request.QueryString["supp"]);
                Title = "Invoice List : " + supp.SupplierName;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var query = new InvoiceSupplierQuery("a");
            var detail = new InvoiceSupplierItemQuery("b");

            detail.es.Distinct = true;
            detail.Select(detail.InvoiceNo);
            detail.InnerJoin(query).On(detail.InvoiceNo == query.InvoiceNo);
            detail.Where(
                    query.SupplierID == Request.QueryString["supp"],
                    query.IsPaymentApproved.IsNull(),
                    query.IsInvoicePayment == false
                 
                );
            detail.Where("<ABS(b.VerifyAmount) > ABS(ISNULL(b.PaymentAmount, 0))>");
            //detail.Where("<b.VerifyAmount > ISNULL(b.PaymentAmount, 0)>");
            
            var coll = new InvoiceSupplierItemCollection();
            coll.Load(detail);

            //detail
            detail = new InvoiceSupplierItemQuery("a");
            detail.Select(detail.InvoiceNo);
            detail.Where(detail.VerifyDate.IsNotNull());
            detail.Where("<ABS(a.VerifyAmount) > ABS(ISNULL(a.PaymentAmount, 0))>");
            //detail.Where("<(VerifyAmount - ISNULL(PaymentAmount, 0)) > 0>");
            var dtb = detail.LoadDataTable();
            
            grdList.DataSource = coll.Where(c => dtb.AsEnumerable().Select(d => d.Field<string>("InvoiceNo")).Contains(c.InvoiceNo));
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (!(source is RadGrid) || string.IsNullOrEmpty(eventArgument))
                return;

            switch (((RadGrid)source).ID)
            {
                case "grdInvoicesDetail":
                    var query = new InvoiceSupplierItemQuery("a");
                    
                    query.Select(
                        query.InvoiceNo,
                        query.TransactionNo,
                        query.InvoiceReferenceNo,
                        query.TransactionDate,
                        (query.VerifyAmount - query.PaymentAmount.Coalesce("'0'")).As("BalanceAmount"),
                        query.CurrencyID,
                        query.CurrencyRate
                        );
                    query.Where(query.InvoiceNo == eventArgument, query.VerifyDate.IsNotNull());
                    query.Where("<(ABS(a.VerifyAmount) - ABS(ISNULL(a.PaymentAmount, 0))) > 0>");
                    //query.Where("<(VerifyAmount - ISNULL(PaymentAmount, 0)) > 0>");
                    query.OrderBy(query.TransactionNo.Ascending);

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
            var coll = (InvoiceSupplierItemCollection)Session["InvoiceSupplierItems"];
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
                        entity.TransactionDate = DateTime.ParseExact(dataItem["TransactionDate"].Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);//DateTime.Parse(dataItem["TransactionDate"].Text);
                        entity.PaymentAmount = Convert.ToDecimal((dataItem.FindControl("txtPaymentAmount") as RadNumericTextBox).Value);
                        entity.Amount = Convert.ToDecimal(dataItem["BalanceAmount"].Text);
                        entity.VerifyAmount = Convert.ToDecimal(dataItem["BalanceAmount"].Text);
                        entity.CurrencyID = dataItem["CurrencyID"].Text;
                        entity.CurrencyRate = Convert.ToDecimal((dataItem.FindControl("txtCurrencyRate") as RadNumericTextBox).Value);
                        entity.PPnAmount = 0;
                        entity.PPh22Amount = 0;
                        entity.PPh23Amount = 0;
                        entity.StampAmount = 0;
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

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            var selected = ((CheckBox)sender).Checked;

            foreach (var dataItem in grdInvoicesDetail.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Enabled))
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
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
