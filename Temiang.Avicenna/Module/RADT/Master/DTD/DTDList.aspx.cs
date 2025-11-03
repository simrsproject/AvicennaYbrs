using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Master
{
    public partial class DtdList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "DtdSearch.aspx";
            UrlPageDetail = "DtdDetail.aspx";

            ProgramID = AppConstant.Program.DTD;

            this.WindowSearch.Height = 400;

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
            string id = dataItem.GetDataKeyValue(DtdMetadata.ColumnNames.DtdNo).ToString();
            Page.Response.Redirect("DtdDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = Dtds;
        }
        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string id = dataItem.GetDataKeyValue("DtdNo").ToString();
            //Load record
            DiagnoseQuery query = new DiagnoseQuery();
            query.Where(query.DtdNo == id);
            DataTable dtb = query.LoadDataTable();
            //Apply
            e.DetailTableView.DataSource = dtb;
        }
        private DataTable Dtds
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                DtdQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (DtdQuery)Session[SessionNameForQuery];
                else
                {
                    query = new DtdQuery("a");
                    var std = new AppStandardReferenceItemQuery("b");
                    query.LeftJoin(std).On(std.StandardReferenceID == AppEnum.StandardReference.ChronicDisease && std.ItemID == query.SRChronicDisease);
                    query.Select(query.DtdNo, query.DtdName, query.DtdLabel, query.SRChronicDisease, std.ItemName.As("ChronicDisease"), query.IsActive);

                    query.OrderBy(query.DtdNo.Ascending);

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

