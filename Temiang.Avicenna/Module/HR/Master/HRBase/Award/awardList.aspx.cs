using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Master
{
    public partial class AwardList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "AwardSearch.aspx";
            UrlPageDetail = "AwardDetail.aspx";
			
			ProgramID = AppConstant.Program.Award; //TODO: Isi ProgramID
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
            string id = dataItem.GetDataKeyValue(AwardMetadata.ColumnNames.AwardID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Awards;
        }

        private DataTable Awards
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				AwardQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (AwardQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new AwardQuery("a");
                }

                AppStandardReferenceItemQuery Criteria = new AppStandardReferenceItemQuery("b");
                AppStandardReferenceItemQuery Type = new AppStandardReferenceItemQuery("c");

				query.es.Top = AppSession.Parameter.MaxResultRecord;
				query.Select(
                				query.AwardID,
                				query.AwardCode,
                				query.AwardName,
                                Criteria.ItemName.As("SRAwardCriteria"),
                                Type.ItemName.As("SRAwardType"),
                				query.ValidFrom,
                				query.ValidTo,
                				query.AwardPrize,
                				query.Note,
                				query.LastUpdateDateTime,
                				query.LastUpdateByUserID
							);
                query.InnerJoin(Criteria).On
                        (
                            query.SRAwardCriteria == Criteria.ItemID &
                            Criteria.StandardReferenceID == "AwardCriteria"
                        );
                query.InnerJoin(Type).On
                        (
                            query.SRAwardType == Type.ItemID &
                            Type.StandardReferenceID == "AwardType"
                        );
				DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

