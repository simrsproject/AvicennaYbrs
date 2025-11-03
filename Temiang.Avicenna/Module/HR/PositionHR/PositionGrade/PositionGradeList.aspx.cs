using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.PositionInformation
{
    public partial class PositionGradeList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "PositionGradeSearch.aspx";
            UrlPageDetail = "PositionGradeDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.PositionGrade; //TODO: Isi ProgramID

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
            //TODO: Betulkan parameter nya
            string id = dataItem.GetDataKeyValue(PositionGradeMetadata.ColumnNames.PositionGradeID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = PositionGrades;
        }

        private DataTable PositionGrades
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				PositionGradeQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (PositionGradeQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new PositionGradeQuery("a");
                    var et = new AppStandardReferenceItemQuery("b");

                    query.LeftJoin(et).On(et.StandardReferenceID == "EmploymentType" && et.ItemID == query.SREmploymentType);

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(
                                    query.PositionGradeID,
                                    query.PositionGradeCode,
                                    query.PositionGradeName,
                                    query.Interval,
                                    query.Ranking,
                                    query.RankName,
                                    et.ItemName.As("EmploymentTypeName"),
                                    query.LastUpdateDateTime,
                                    query.LastUpdateByUserID
                                );
                    //Quick Search
                    ApplyQuickSearch(query, "PositionGradeName", "PositionGradeCode");
                }
				
				DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

