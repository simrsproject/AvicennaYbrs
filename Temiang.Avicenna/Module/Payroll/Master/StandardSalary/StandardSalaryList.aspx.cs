using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class StandardSalaryList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "StandardSalarySearch.aspx";
            UrlPageDetail = "StandardSalaryDetail.aspx";

            ProgramID = AppConstant.Program.StandardSalary; //TODO: Isi ProgramID
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
            string id = dataItem.GetDataKeyValue(StandardSalaryMetadata.ColumnNames.StandardSalaryID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = StandardSalarys;
        }

        private DataTable StandardSalarys
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                StandardSalaryQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (StandardSalaryQuery)Session[SessionNameForQuery];
                }
                else
                {
                    PositionGradeQuery grade = new PositionGradeQuery("b");
                    query = new StandardSalaryQuery("a");

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(
                                    query.StandardSalaryID,
                                    query.PositionGradeID,
                                    grade.PositionGradeCode,
                                    grade.PositionGradeName,
                                    query.ValidFrom,
                                    query.ValidTo,
                                    query.LastUpdateDateTime,
                                    query.LastUpdateByUserID
                                );
                    query.InnerJoin(grade).On
                           (
                               query.PositionGradeID == grade.PositionGradeID
                           );
                    query.OrderBy(query.ValidFrom.Descending, query.PositionGradeID.Ascending);

                }

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

