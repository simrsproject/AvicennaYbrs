using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class EmployeePositionList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "EmployeePositionSearch.aspx";
            UrlPageDetail = "EmployeePositionDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.QueryEmployeePosition; //TODO: Isi ProgramID
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
            string id = dataItem.GetDataKeyValue(EmployeePositionMetadata.ColumnNames.EmployeePositionID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = EmployeePositions;
        }

        private DataTable EmployeePositions
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				EmployeePositionQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (EmployeePositionQuery)Session[SessionNameForQuery];
                }
                else
                {

                    PositionLevelQuery level = new PositionLevelQuery("e");
                    PositionGradeQuery grade = new PositionGradeQuery("d");
                    PositionQuery position = new PositionQuery("c");
                    PersonalInfoQuery personal = new PersonalInfoQuery("b");
                    query = new EmployeePositionQuery("a");

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select
                        (
                           query.EmployeePositionID,
                           query.PersonID,
                           personal.EmployeeNumber,
                           personal.EmployeeName,
                           query.PositionID,
                           position.PositionCode,
                           position.PositionName,
                           grade.PositionGradeName,
                           level.PositionLevelName,
                           query.ValidFrom,
                           query.ValidTo,
                           query.IsPrimaryPosition
                        );

                    query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                    query.InnerJoin(position).On(query.PositionID == position.PositionID);
                    query.LeftJoin(grade).On(position.PositionGradeID == grade.PositionGradeID);
                    query.LeftJoin(level).On(position.PositionLevelID == level.PositionLevelID);
                    query.OrderBy(query.PersonID.Ascending); //TODO: Betulkan ordernya
                }				
				
				DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

