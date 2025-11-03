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
    public partial class CashEntryRefferenceToARPayment : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CASH_ENTRY;
        }

        protected void grdListItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                var tp = (new InvoicesCollection())
                    .GetPaymentWithPaging(this.grdListItem.CurrentPageIndex, this.grdListItem.PageSize,
                    txtInvoicePaymentNo.Text, txtInvoiceNo.Text, cboGuarantorID.SelectedValue,
                    txtInvoicePaymentDateFrom.SelectedDate, txtInvoicePaymentDateTo.SelectedDate);

                var iCount = (new InvoicesCollection())
                    .GetPaymentWithPagingCount(txtInvoicePaymentNo.Text, txtInvoiceNo.Text, cboGuarantorID.Text,
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
                str = "'" + grdListItem.SelectedValue.ToString() + "||PaymentAR'";
            return "oWnd.argument.init = " + str;
        }

        protected void cboGuarantorID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboGuarantorID((RadComboBox)sender, e.Text);
        }

        private void PopulateCboGuarantorID(RadComboBox comboBox, string textSearch)
        {
            string searchText = string.Format("%{0}%", textSearch);
            var query = new GuarantorQuery("a");
            var query2 = new GuarantorQuery("b");
            query.InnerJoin(query2).On(query.GuarantorHeaderID == query2.GuarantorID);

            query.Select(query2.GuarantorID, query2.GuarantorName,
                         (query2.StreetName + " " + query2.City + " " + query2.ZipCode).Trim().As("Address"));

            query.Where
                (
                    query.Or
                        (
                            query2.GuarantorID.Like(searchText),
                            query2.GuarantorName.Like(searchText)
                        )
                );

            query.es.Distinct = true;
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }
    }
}
