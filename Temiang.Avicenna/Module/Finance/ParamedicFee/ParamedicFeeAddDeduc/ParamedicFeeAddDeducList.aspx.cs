using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee
{
    public partial class ParamedicFeeAddDeducList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "ParamedicFeeAddDeducSearch.aspx";
            UrlPageDetail = "ParamedicFeeAddDeducDetail.aspx";

            this.WindowSearch.Height = 400;
            ProgramID = AppConstant.Program.ParamedicFeeAddDeduc;
        }
        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }
        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }
        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(ParamedicFeeAddDeducMetadata.ColumnNames.TransactionNo).ToString();
            string url = string.Format("ParamedicFeeAddDeducDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                grdList.DataSource = ParamedicFeeAddDeducs;
            }
        }
        private DataTable ParamedicFeeAddDeducs
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                ParamedicFeeAddDeducQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (ParamedicFeeAddDeducQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new ParamedicFeeAddDeducQuery("a");
                    var parQ = new ParamedicQuery("b");
                    var stdQ = new AppStandardReferenceItemQuery("c");

                    query.InnerJoin(parQ).On(query.ParamedicID == parQ.ParamedicID);
                    query.InnerJoin(stdQ).On(query.SRParamedicFeeAdjustType == stdQ.ItemID &&
                                             stdQ.StandardReferenceID == "ParamedicFeeAdjustType");

                    query.Select(
                           query.TransactionNo,
                           query.TransactionDate,
                           parQ.ParamedicName,
                           stdQ.ItemName,
                           query.Amount,
                           query.Notes,
                           query.IsIncludeInTaxCalc,
                           query.IsApproved
                       );
                    query.OrderBy(query.VerificationNo.Descending);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }
    }
}
