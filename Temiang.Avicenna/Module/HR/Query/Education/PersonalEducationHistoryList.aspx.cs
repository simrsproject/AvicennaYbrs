using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class PersonalEducationHistoryList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "PersonalEducationHistorySearch.aspx";
            UrlPageDetail = "PersonalEducationHistoryDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.QueryPersonalEducationHistory; //TODO: Isi ProgramID
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
            string id = dataItem.GetDataKeyValue(PersonalEducationHistoryMetadata.ColumnNames.PersonalEducationHistoryID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = PersonalEducationHistorys;
        }

        private DataTable PersonalEducationHistorys
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				PersonalEducationHistoryQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (PersonalEducationHistoryQuery)Session[SessionNameForQuery];
                }
                else
                {
                    AppStandardReferenceItemQuery education = new AppStandardReferenceItemQuery("c");
                    PersonalInfoQuery personal = new PersonalInfoQuery("b");
                    query = new PersonalEducationHistoryQuery("a");

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select
                        (
                           query.PersonalEducationHistoryID,
                           query.PersonID,
                           personal.EmployeeNumber,
                           personal.EmployeeName,
                           query.SREducationLevel,
                           education.ItemName.As("EducationLevelName"),
                           query.InstitutionName,
                           query.Location,
                           query.StartYear,
                           query.EndYear,
                           query.Gpa,
                           query.Achievement,
                           query.Note,
                           query.LastUpdateByUserID,
                           query.LastUpdateDateTime
                        );

                    query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                    query.LeftJoin(education).On
                            (
                                query.SREducationLevel == education.ItemID &
                                education.StandardReferenceID == "EducationLevel"
                            );

                    query.OrderBy(query.PersonID.Ascending); //TODO: Betulkan ordernya
                }				
				
				DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

