using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.PositionInformation
{
    public partial class PositionLevelList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "PositionLevelSearch.aspx";
            UrlPageDetail = "PositionLevelDetail.aspx";

            ProgramID = AppConstant.Program.PositionLevel; //TODO: Isi ProgramID
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
            //TODO: Betulkan parameter nya
            string id = dataItem.GetDataKeyValue(PositionLevelMetadata.ColumnNames.PositionLevelID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = PositionLevels;
        }

        private DataTable PositionLevels
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				PositionLevelQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (PositionLevelQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new PositionLevelQuery();
                }
				query.es.Top = AppSession.Parameter.MaxResultRecord;
				query.Select(
                				query.PositionLevelID,
                				query.PositionLevelCode,
                				query.PositionLevelName,
                				query.Ranking,
                				query.LastUpdateDateTime,
                				query.LastUpdateByUserID
							);
				DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

