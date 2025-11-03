using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ServiceRoomList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "ServiceRoomSearch.aspx";
            UrlPageDetail = "ServiceRoomDetail.aspx";

            this.WindowSearch.Height = 400;
            ProgramID = AppConstant.Program.ServiceRoom;

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
            string id = dataItem.GetDataKeyValue(ServiceRoomMetadata.ColumnNames.RoomID).ToString();
            Page.Response.Redirect("ServiceRoomDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ServiceRooms;
        }

        private DataTable ServiceRooms
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ServiceRoomQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (ServiceRoomQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ServiceRoomQuery("a");
                    var suq = new ServiceUnitQuery("b");
                    var floor = new AppStandardReferenceItemQuery("c");

                    query.InnerJoin(suq).On(query.ServiceUnitID == suq.ServiceUnitID);
                    query.LeftJoin(floor).On(query.SRFloor == floor.ItemID & floor.StandardReferenceID == AppEnum.StandardReference.Floor);
                    query.Select(query.SelectAllExcept(), suq.ServiceUnitName, floor.ItemName.As("SRFloorName"));
                    query.OrderBy(query.RoomID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "RoomName", "RoomID");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}

