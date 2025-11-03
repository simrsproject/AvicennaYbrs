using System;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Finance.Receivable.Customer
{
    public partial class InvoicingPickList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AR_CUSTOMER_INVOICING;

            if (!IsPostBack)
            {
                ViewState["InvoiceNo"] = string.Empty;
                if (Request.QueryString["all"] == "false")
                {
                    txtTransactionFromDate.SelectedDate = Convert.ToDateTime(Request.QueryString["sd"]);
                    txtTransactionToDate.SelectedDate = Convert.ToDateTime(Request.QueryString["ed"]);
                }
            }
        }

        private DataTable ItemTransactions
        {
            get
            {
                DataTable dtb =
                    (new InvoiceCustomerCollection()).ItemTransactionOutstandingWithParameter(Request.QueryString["gid"],
                                                                                    txtTransactionFromDate.SelectedDate,
                                                                                    txtTransactionToDate.SelectedDate);

                return dtb;
            }
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdItem.DataSource = ItemTransactions;
        }

        protected void grdItem_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string transNo = dataItem.GetDataKeyValue("TransactionNo").ToString();

            //Load record
            var query = new ItemTransactionItemQuery("a");
            var iq = new ItemQuery("b");
            query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
            query.Where(query.TransactionNo == transNo);
            query.OrderBy(query.ItemID.Ascending);

            query.Select
                (
                    query,
                    iq.ItemName.As("ItemName")
                );

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            bool selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in grdItem.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.command = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            InvoiceCustomerItemCollection invoice = (InvoiceCustomerItemCollection)Session["collInvoiceCustomerItem" + Request.UserHostName];

            foreach (GridDataItem dataItem in grdItem.MasterTableView.Items)
            {
                if (((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                {
                    //invoice
                    {
                        var source = new ItemTransaction();
                        source.LoadByPrimaryKey(dataItem["TransactionNo"].Text);

                        var entity = invoice.AddNew();

                        entity.InvoiceNo = Request.QueryString["inv"].ToString();
                        entity.TransactionNo = source.TransactionNo;
                        entity.TransactionDate = source.TransactionDate;
                        entity.Amount = Convert.ToDecimal(dataItem["Amount"].Text);
                        entity.PaymentTypeName = dataItem["PaymentTypeName"].Text;
                        entity.LastUpdateDateTime = DateTime.Now;
                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                }
            }

            return true;
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            grdItem.Rebind();
        }
    }
}
