using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;

namespace Temiang.Avicenna.Module.Finance.CashManagement.CashEntryV2
{
    public partial class CashEntryRefferenceToAPPayment : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CASH_ENTRY;
        }

        protected void grdListItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                var tp = (new InvoiceSupplierCollection())
                    .GetPaymentWithPaging(this.grdListItem.CurrentPageIndex, this.grdListItem.PageSize,
                    txtInvoicePaymentNo.Text, txtInvoiceNo.Text, cboSupplierID.SelectedValue,
                    txtInvoicePaymentDateFrom.SelectedDate, txtInvoicePaymentDateTo.SelectedDate);

                var iCount = (new InvoiceSupplierCollection())
                    .GetPaymentWithPagingCount(txtInvoicePaymentNo.Text, txtInvoiceNo.Text, cboSupplierID.SelectedValue,
                    txtInvoicePaymentDateFrom.SelectedDate, txtInvoicePaymentDateTo.SelectedDate);

                grdListItem.VirtualItemCount = iCount;
                grdListItem.DataSource = tp;
            }
        }

        protected void grdListItem_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            int ParentDetailId = (int)dataItem.GetDataKeyValue("DetailId");

            DataTable dtb = (new CashTransactionDetailCollection()).GetCashTransactionRealizationDetail(ParentDetailId);
            //Apply
            e.DetailTableView.DataSource = dtb;
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdListItem.Rebind();
        }

        public override bool OnButtonOkClicked()
        {
            if (grdListItem.SelectedValue == null)
                return false;
            else
                return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            string str = "";
            if (grdListItem.SelectedValue == null)
                str = "'rebind'";
            else
                str = "'" + grdListItem.SelectedValue.ToString() + "||PaymentAP'";
            return "oWnd.argument.init = " + str;
        }

        protected void cboSupplierID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboSupplierID((RadComboBox)sender, e.Text);
        }

        private void PopulateCboSupplierID(RadComboBox comboBox, string textSearch)
        {
            string searchText = string.Format("%{0}%", textSearch);
            var query = new SupplierQuery("a");

            query.Select(query.SupplierID, query.SupplierName,
                         (query.StreetName + " " + query.City + " " + query.ZipCode).Trim().As("Address"));

            query.Where
                (
                    query.Or
                        (
                            query.SupplierID.Like(searchText),
                            query.SupplierName.Like(searchText)
                        )
                );

            query.es.Distinct = true;
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
    }
}
