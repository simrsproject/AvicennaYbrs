using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class TariffComponentList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "TariffComponentSearch.aspx";
            UrlPageDetail = "TariffComponentDetail.aspx";

            WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.TariffComponent;

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
            string id = dataItem.GetDataKeyValue(TariffComponentMetadata.ColumnNames.TariffComponentID).ToString();
            Page.Response.Redirect("TariffComponentDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = TariffComponents;
        }

        private DataTable TariffComponents
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                TariffComponentQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (TariffComponentQuery)Session[SessionNameForQuery];
                else
                {
                    query = new TariffComponentQuery("a");
                    var type = new AppStandardReferenceItemQuery("b");
                    var PphType = new AppStandardReferenceItemQuery("pph");
                    query.InnerJoin(type).On(query.SRTariffComponentType == type.ItemID &&
                        type.StandardReferenceID == AppEnum.StandardReference.TariffComponentType)
                        .LeftJoin(PphType).On(query.SRPphType == PphType.ItemID &&
                                             PphType.StandardReferenceID == AppEnum.StandardReference.PphType);                    

                    query.Select
                        (
                        query,
                        type.ItemName.As("TariffComponentType"), 
                        PphType.ItemName.As("SRPphTypeName")
                        );

                    query.OrderBy(query.TariffComponentID.Ascending);

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
