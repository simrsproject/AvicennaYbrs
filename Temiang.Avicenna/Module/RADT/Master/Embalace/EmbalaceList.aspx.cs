using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class EmbalaceList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            UrlPageSearch = "EmbalaceSearch.aspx";
            UrlPageDetail = "EmbalaceDetail.aspx";

            ProgramID = AppConstant.Program.Embalace;

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
            string id = dataItem.GetDataKeyValue(EmbalaceMetadata.ColumnNames.EmbalaceID).ToString();
            Page.Response.Redirect("EmbalaceDetail.aspx?md=" + mode + "&id=" + id, true);
        }	
        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Embalaces;
        }

        private DataTable Embalaces
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                EmbalaceQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (EmbalaceQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new EmbalaceQuery();
                    query.OrderBy(query.EmbalaceID.Ascending);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

