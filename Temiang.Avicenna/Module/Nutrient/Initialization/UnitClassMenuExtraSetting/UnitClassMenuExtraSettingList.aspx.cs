using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Nutrient.Initialization
{
    public partial class UnitClassMenuExtraSettingList : BasePageList
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ToolBarMenuQuickSearch.Enabled = true;
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 140;

            UrlPageSearch = "UnitClassMenuExtraSettingSearch.aspx";
            UrlPageDetail = "UnitClassMenuExtraSettingDetail.aspx";

            ProgramID = AppConstant.Program.UnitClassMenuExtraSetting;
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string modus)
        {
            string id = dataItem.GetDataKeyValue(ServiceUnitMetadata.ColumnNames.ServiceUnitID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, modus, id);
            Page.Response.Redirect(url, true);
        }

        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ServiceUnits;
        }

        private DataTable ServiceUnits
        {
            get
            {
                object obj = Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ServiceUnitQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (ServiceUnitQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ServiceUnitQuery("a");
                    var std = new AppStandardReferenceItemQuery("b");
                    var x = new ServiceUnitClassMenuExtraSettingQuery("c");
                    var c = new ClassQuery("d");
                    var m = new MenuQuery("e");
                    query.InnerJoin(std).On(query.ServiceUnitID == std.ItemID &&
                                            std.StandardReferenceID == AppEnum.StandardReference.UnitForExtraMealOrder);
                    query.LeftJoin(x).On(query.ServiceUnitID == x.ServiceUnitID);
                    query.LeftJoin(c).On(x.ClassID == c.ClassID);
                    query.LeftJoin(m).On(x.MenuID == m.MenuID);
                    query.Select
                        (
                            query.ServiceUnitID,
                            query.ServiceUnitName,
                            c.ClassName,
                            m.MenuName
                        );
                    query.Where(query.IsActive == true);
                    query.OrderBy(query.ServiceUnitID.Ascending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;

                return dtb;
            }
        }
    }
}
