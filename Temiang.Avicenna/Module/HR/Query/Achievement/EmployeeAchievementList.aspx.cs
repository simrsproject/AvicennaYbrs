using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class EmployeeAchievementList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "EmployeeAchievementSearch.aspx";
            UrlPageDetail = "EmployeeAchievementDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.QueryEmployeeAchievement; //TODO: Isi ProgramID
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
            string id = dataItem.GetDataKeyValue(EmployeeAchievementMetadata.ColumnNames.EmployeeAchievementID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = EmployeeAchievements;
        }

        private DataTable EmployeeAchievements
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				EmployeeAchievementQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (EmployeeAchievementQuery)Session[SessionNameForQuery];
                }
                else
                {
                    AwardQuery award = new AwardQuery("c");
                    PersonalInfoQuery personal = new PersonalInfoQuery("b");
                    query = new EmployeeAchievementQuery("a");

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select
                        (
                           query.EmployeeAchievementID,
                           query.PersonID,
                           personal.EmployeeNumber,
                           personal.EmployeeName,
                           query.AwardID,
                           award.AwardName,
                           award.AwardPrize,
                           query.AwardDate,
                           query.Achievement,
                           query.FinancialValue,
                           query.Note
                        );

                    query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                    query.InnerJoin(award).On(query.AwardID == award.AwardID);

                    query.OrderBy(query.PersonID.Ascending); //TODO: Betulkan ordernya
                }				
				
				DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

