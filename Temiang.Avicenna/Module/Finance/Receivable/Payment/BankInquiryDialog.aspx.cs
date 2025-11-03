using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using System.Data;
using System.IO;
using System.Configuration;
using Telerik.Web.UI;
using DevExpress.Data.Linq;
using System.Drawing;

namespace Temiang.Avicenna.Module.Finance.Receivable
{
    public partial class BankInquiryDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AR_PAYMENT;
        }

        public override bool OnButtonOkClicked()
        {
            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            if(grdInquiryDetail.SelectedItems.Count == 0)
                return "oWnd.argument.transId = '0';";
            else
                return "oWnd.argument.transId = '" + (grdInquiryDetail.SelectedItems[0] as GridDataItem).GetDataKeyValue("TransactionID") + "';";
        }

        protected void grdInquiryDetail_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid rg = sender as RadGrid;
            var bidColl = new BankInquiryDetailCollection();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (GridSortExpression ep in rg.MasterTableView.SortExpressions)
            {
                sb.AppendFormat("{0}^{1}", ep.FieldName, ep.SortOrder);
                sb.Append(",");
            }

            rg.VirtualItemCount = bidColl.GetCountByCashCodeByBankIdByPaging("1102");

            bidColl.LoadByCashCodeByUnlinkedByUnreconciledByPaging("1102", rg.CurrentPageIndex, rg.PageSize, sb.ToString());
            rg.DataSource = bidColl;
        }
    }
}
