using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class EmployeeGradeList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "EmployeeGradeSearch.aspx";
            UrlPageDetail = "EmployeeGradeDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.QueryEmployeeGrade; //TODO: Isi ProgramID
        }
        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            //RedirectToPageDetail(dataItems[0], "edit");
        }
        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            //RedirectToPageDetail(dataItems[0], "view");
        }
        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            //TODO: Betulkan parameter nya
            string id = dataItem.GetDataKeyValue(EmployeeGradeMetadata.ColumnNames.EmployeeGradeID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = EmployeeGrades;
        }

        private DataTable EmployeeGrades
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				EmployeePositionGradeQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (EmployeePositionGradeQuery)Session[SessionNameForQuery];
                }
                else
                {
                    PositionGradeQuery grade = new PositionGradeQuery("c");
                    PersonalInfoQuery personal = new PersonalInfoQuery("b");
                    query = new EmployeePositionGradeQuery("a");

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select
                        (
                           query.EmployeePositionGradeID,
                           query.PersonID,
                           personal.EmployeeNumber,
                           personal.EmployeeName,
                           grade.PositionGradeCode,
                           grade.PositionGradeName,
                           grade.RankName,
                           query.GradeYear,
                           query.ValidFrom,
                           query.LastUpdateByUserID,
                           query.LastUpdateDateTime
                        );

                    query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                    query.InnerJoin(grade).On(query.PositionGradeID == grade.PositionGradeID);
                    query.OrderBy(query.PersonID.Ascending); //TODO: Betulkan ordernya
                }				
				
				DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

