using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class CoorporateGradeList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "CoorporateGradeSearch.aspx";
            UrlPageDetail = "CoorporateGradeDetail.aspx";

            //WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.CoorporateGrade;
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
            string id = dataItem.GetDataKeyValue(CoorporateGradeMetadata.ColumnNames.CoorporateGradeID).ToString();
            Page.Response.Redirect("CoorporateGradeDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = CoorporateGrades;
        }

        private DataTable CoorporateGrades
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                CoorporateGradeQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (CoorporateGradeQuery)Session[SessionNameForQuery];
                else
                {
                    query = new CoorporateGradeQuery("a");
                    //query.Select(query.CoorporateGradeID,
                    //             query.CoorporateGradeLevel,
                    //             query.CoorporateGradeMin,
                    //             query.CoorporateGradeMax,
                    //             query.CoorporateGradeInterval,
                    //             query.CreatedDateTime,
                    //             query.CreatedByUserID,
                    //             query.LastUpdateDateTime,
                    //             query.LastUpdateByUserID);
                    query.OrderBy(query.CoorporateGradeLevel.Descending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}