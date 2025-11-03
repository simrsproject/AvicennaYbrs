using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Nutrient.Initialization
{
    public partial class LiquidFoodSettingList : BasePageList
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

            UrlPageSearch = "LiquidFoodSettingSearch.aspx";
            UrlPageDetail = "LiquidFoodSettingDetail.aspx";

            ProgramID = AppConstant.Program.LiquidFoodSetting;
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string modus)
        {
            string id = dataItem.GetDataKeyValue(AppStandardReferenceMetadata.ColumnNames.StandardReferenceID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, modus, id);
            Page.Response.Redirect(url, true);
        }

        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = AppStandardReferences;
        }

        private DataTable AppStandardReferences
        {
            get
            {
                object obj = Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                AppStandardReferenceQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (AppStandardReferenceQuery)Session[SessionNameForQuery];
                else
                {
                    query = new AppStandardReferenceQuery();
                    query.Select
                        (
                            query.StandardReferenceID,
                            query.StandardReferenceName
                        );
                    query.Where(query.StandardReferenceID.In("LQ-Unit", "LQ-Class"));
                    query.OrderBy(query.StandardReferenceID.Ascending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;

                return dtb;
            }
        }
    }
}
