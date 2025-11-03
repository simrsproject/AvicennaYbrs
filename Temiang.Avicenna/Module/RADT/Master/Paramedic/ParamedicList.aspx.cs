using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ParamedicList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "ParamedicSearch.aspx";
            UrlPageDetail = "ParamedicDetail.aspx";

            ProgramID = AppConstant.Program.Paramedic;

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
            string id = dataItem.GetDataKeyValue(ParamedicMetadata.ColumnNames.ParamedicID).ToString();
            Page.Response.Redirect("ParamedicDetail.aspx?md=" + mode + "&id=" + id, true);
        }	

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Paramedics;
        }

        private DataTable Paramedics
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ParamedicQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (ParamedicQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ParamedicQuery();
                    query.OrderBy(query.ParamedicID.Ascending);
                }

                //Quick Search
                ApplyQuickSearch(query);

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}

