using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ItemDeductionList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "##";
            UrlPageDetail = "ItemDeductionDetail.aspx";
            
            ProgramID = AppConstant.Program.ItemProductDeductionDetail;
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            ToolBarMenuSearch.Visible = false;
        }

        public override void OnMenuEditClick(Telerik.Web.UI.GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(Telerik.Web.UI.GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(ItemProductDeductionDetailMetadata.ColumnNames.DeductionID).ToString();
            Page.Response.Redirect("ItemDeductionDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ItemProductDeductionDetails;
        }

        private DataTable ItemProductDeductionDetails
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ItemProductDeductionDetailQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (ItemProductDeductionDetailQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ItemProductDeductionDetailQuery("a");
                    query.Select
                        (
                        query.DeductionID,
                        query.MinAmount,
                        query.MaxAmount,
                        query.DeductionAmount
                        );

                    query.OrderBy(query.DeductionID.Ascending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
