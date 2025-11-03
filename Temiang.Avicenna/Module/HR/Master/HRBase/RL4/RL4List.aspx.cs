using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Master.HRBase.RL4
{
    public partial class RL4List : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "RL4Search.aspx";
            UrlPageDetail = "RL4Detail.aspx";

            ProgramID = AppConstant.Program.RL4; //TODO: Isi ProgramID
            this.WindowSearch.Height = 400;
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
            string id = dataItem.GetDataKeyValue(AppStandardReferenceItemMetadata.ColumnNames.ItemID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = LaborStatuses;
        }

        private DataTable LaborStatuses
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                AppStandardReferenceItemQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (AppStandardReferenceItemQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new AppStandardReferenceItemQuery("a");
                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(
                        query.ItemID,
                        query.ItemName,
                        query.LastUpdateDateTime,
                        query.LastUpdateByUserID
                        );
                    query.Where(query.StandardReferenceID == AppEnum.StandardReference.RL4Type.ToString());
                    query.OrderBy(query.ItemID.Ascending);
                }

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }
    }
}