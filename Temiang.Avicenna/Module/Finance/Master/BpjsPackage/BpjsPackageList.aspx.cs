using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class BpjsPackageList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            UrlPageSearch = "BpjsPackageSearch.aspx";
            UrlPageDetail = "BpjsPackageDetail.aspx";

            ProgramID = AppConstant.Program.BpjsPackage;

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
            string id = dataItem.GetDataKeyValue(BpjsPackageMetadata.ColumnNames.PackageID).ToString();
            Page.Response.Redirect("BpjsPackageDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = BpjsPackages;
        }

        private DataTable BpjsPackages
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                BpjsPackageQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (BpjsPackageQuery)Session[SessionNameForQuery];
                else
                {
                    query = new BpjsPackageQuery("a");
                    query.Select(
                        query.PackageID,
                        query.PackageName,
                        query.IsActive
                        );

                    //Quick Search
                    ApplyQuickSearch(query, "PackageName", "PackageID");
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string id = dataItem.GetDataKeyValue("PackageID").ToString();

            //Load record
            var items = new BpjsPackageTariffQuery("a");
            var cls = new AppStandardReferenceItemQuery("b");
            items.Select(items.PackageID, items.StartingDate, items.ClassID, cls.ItemName.As("ClassName"), items.Price);
            items.InnerJoin(cls).On(items.ClassID == cls.ItemID &&
                                    cls.StandardReferenceID == AppEnum.StandardReference.ClassRL.ToString());
            items.Where(items.PackageID == id);
            items.OrderBy(items.ClassID.Ascending, items.StartingDate.Ascending);

            //Apply
            e.DetailTableView.DataSource = items.LoadDataTable();
        }
    }
}