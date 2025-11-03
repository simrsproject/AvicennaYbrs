using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class IndicationList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "IndicationSearch.aspx";
            UrlPageDetail = "IndicationDetail.aspx";

            ProgramID = AppConstant.Program.Indication;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
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
            string id = dataItem.GetDataKeyValue(IndicationMetadata.ColumnNames.IndicationID).ToString();
            Page.Response.Redirect("IndicationDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Indications;
        }

        private DataTable Indications
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                IndicationQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (IndicationQuery)Session[SessionNameForQuery];
                else
                {
                    query = new IndicationQuery("a");
                    query.OrderBy(query.IndicationID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query);
                }
                
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
