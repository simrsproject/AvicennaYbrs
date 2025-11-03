using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class LocationTemplateList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "LocationTemplateSearch.aspx";
            UrlPageDetail = "LocationTemplateDetail.aspx";

            ProgramID = AppConstant.Program.LocationTemplate;

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
            string id = dataItem.GetDataKeyValue(LocationTemplateMetadata.ColumnNames.TemplateNo).ToString();
            Page.Response.Redirect("LocationTemplateDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = LocationTemplates;
        }

        private DataTable LocationTemplates
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                LocationTemplateQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (LocationTemplateQuery)Session[SessionNameForQuery];
                else
                {
                    query = new LocationTemplateQuery("a");
                    var location = new LocationQuery("b");
                    var unitLocation = new ServiceUnitLocationQuery("c");
                    var usrUnit = new AppUserServiceUnitQuery("d");
                    
                    query.InnerJoin(location).On(location.LocationID == query.LocationID);
                    query.InnerJoin(unitLocation).On(unitLocation.LocationID == query.LocationID);
                    query.InnerJoin(usrUnit).On(usrUnit.UserID == AppSession.UserLogin.UserID &&
                                                usrUnit.ServiceUnitID == unitLocation.ServiceUnitID);

                    query.Select(query.TemplateNo, query.TemplateName, query.LocationID, location.LocationName,
                        query.IsActive);

                    query.OrderBy(query.TemplateNo.Ascending);

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