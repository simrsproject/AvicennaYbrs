using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ItemServiceProcedureList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "ItemServiceProcedureSearch.aspx";
            UrlPageDetail = "ItemServiceProcedureDetail.aspx";

            WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.ItemServiceProcedure;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
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
            string id = dataItem.GetDataKeyValue(AppStandardReferenceItemMetadata.ColumnNames.ItemID).ToString();
            Page.Response.Redirect("ItemServiceProcedureDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ItemServiceProcedures;
        }

        private DataTable ItemServiceProcedures
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                AppStandardReferenceItemQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (AppStandardReferenceItemQuery)Session[SessionNameForQuery];
                else
                {
                    query = new AppStandardReferenceItemQuery("a");
                    query.Select(query);
                    query.Where(query.StandardReferenceID == AppEnum.StandardReference.Procedure.ToString());
                    query.OrderBy(query.ItemID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}