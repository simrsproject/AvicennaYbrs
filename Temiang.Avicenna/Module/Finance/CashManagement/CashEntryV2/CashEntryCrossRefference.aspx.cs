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
    public partial class CashEntryCrossRefference : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CASH_ENTRY;
        }

        protected void grdListItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                int iRowCount; decimal Balance, ReconciledBalance;
                var cashDet = (new CashTransactionDetailCollection()).GetCashTransactionDetailRefByPage(
                    Request.QueryString["bankid"], ((grdListItem.CurrentPageIndex * grdListItem.PageSize) + 1),
                    ((grdListItem.CurrentPageIndex + 1) * grdListItem.PageSize),
                    txtFilterDateFrom.SelectedDate, txtFilterDateTo.SelectedDate, txtFilterDesc.Text,
                    out iRowCount, out Balance, out ReconciledBalance);

                grdListItem.VirtualItemCount = iRowCount;
                grdListItem.DataSource = cashDet;
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
                str = "'" + grdListItem.SelectedValue.ToString() + "'";
            return "oWnd.argument.init = " + str;
        }
    }
}
