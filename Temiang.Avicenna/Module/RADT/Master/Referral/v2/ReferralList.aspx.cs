using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Master.Referralv2
{
    public partial class ReferralList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "ReferralSearch.aspx";
            UrlPageDetail = "ReferralDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.Referral;

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
            string id = dataItem.GetDataKeyValue(AppStandardReferenceItemMetadata.ColumnNames.ItemID).ToString();
            string url = string.Format("ReferralDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ReferralGroups;
        }

        private DataTable ReferralGroups
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                AppStandardReferenceItemQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (AppStandardReferenceItemQuery)Session[SessionNameForQuery];
                else
                {
                    query = new AppStandardReferenceItemQuery("a");
                    query.Where(query.StandardReferenceID == "ReferralGroup");

                    query.Select
                        (
                            query.ItemID,
                            query.ItemName,
                            query.Note,
                            query.ReferenceID,
                            query.IsActive
                        );

                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsCreateReferralGroupIdAutomatic) == "Yes")
                        query.OrderBy(query.ItemID.Ascending);
                    else
                        query.OrderBy(query.ItemID.Descending);

                    //Quick Search
                    ApplyQuickSearch(query, "ItemName", "ItemID");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}