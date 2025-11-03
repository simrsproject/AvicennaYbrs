using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Master
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
            string id = dataItem.GetDataKeyValue(ReferralMetadata.ColumnNames.ReferralID).ToString();
            string url = string.Format("ReferralDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Referrals;
        }

        private DataTable Referrals
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ReferralQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (ReferralQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ReferralQuery("a");

                    AppStandardReferenceItemQuery asriq = new AppStandardReferenceItemQuery("b");
                    query.InnerJoin(asriq).On(query.SRReferralGroup == asriq.ItemID);
                    query.Where(asriq.StandardReferenceID == "ReferralGroup");

                    query.Select
                        (
                            query.ReferralID,
                            query.ReferralName,
                            query.DepartmentName,
                            asriq.ItemName.As("SRReferralGroup"),
                            query.StreetName,
                            query.City,
                            query.IsRefferalFrom,
                            query.IsRefferalTo,
                            query.IsActive
                        );
                    query.OrderBy(query.ReferralID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "ReferralName", "ReferralID");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}