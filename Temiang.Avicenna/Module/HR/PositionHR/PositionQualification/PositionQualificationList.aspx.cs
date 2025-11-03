using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.PositionInformation
{
    public partial class PositionQualificationList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "PositionQualificationSearch.aspx";
            UrlPageDetail = "PositionQualificationDetail.aspx";

            ProgramID = AppConstant.Program.PositionQualification; //TODO: Isi ProgramID

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
            string id = dataItem.GetDataKeyValue(PositionMetadata.ColumnNames.PositionID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Positions;
        }

        private DataTable Positions
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                PositionQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (PositionQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new PositionQuery("a");
                    PositionLevelQuery PsLevel = new PositionLevelQuery("d");
                    PositionGradeQuery PsGrade = new PositionGradeQuery("c");

                    query.es.Top = AppSession.Parameter.MaxResultRecord;

                    query.LeftJoin(PsLevel).On(query.PositionLevelID == PsLevel.PositionLevelID);
                    query.LeftJoin(PsGrade).On(query.PositionGradeID == PsGrade.PositionGradeID);
                    query.Select
                        (
                            query.PositionID,
                                    query.PositionCode,
                                    query.PositionName,
                                    query.Summary,
                                    query.PositionGradeID,
                                    PsGrade.PositionGradeName,
                                    query.PositionLevelID,
                                    PsLevel.PositionLevelName,
                                    query.ValidFrom,
                                    query.ValidTo,
                                    query.LastUpdateDateTime,
                                    query.LastUpdateByUserID
                        );

                    //Quick Search
                    ApplyQuickSearch(query, "PositionName", "PositionCode");
                }
                
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}

