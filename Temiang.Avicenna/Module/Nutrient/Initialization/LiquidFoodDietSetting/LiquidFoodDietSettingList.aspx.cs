using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Nutrient.Initialization
{
    public partial class LiquidFoodDietSettingList : BasePageList
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

            UrlPageSearch = "LiquidFoodDietSettingSearch.aspx";
            UrlPageDetail = "LiquidFoodDietSettingDetail.aspx";

            ProgramID = AppConstant.Program.LiquidFoodDietSetting;
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string modus)
        {
            string id = dataItem.GetDataKeyValue(DietMetadata.ColumnNames.DietID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, modus, id);
            Page.Response.Redirect(url, true);
        }

        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Diets;
        }

        private DataTable Diets
        {
            get
            {
                object obj = Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                DietQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (DietQuery)Session[SessionNameForQuery];
                else
                {
                    query = new DietQuery();
                    query.Select
                        (
                            query.DietID,
                            query.DietName
                        );
                    query.OrderBy(query.DietID.Ascending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;

                return dtb;
            }
        }
    }
}
