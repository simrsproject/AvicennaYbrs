using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class PersonalWorkExperienceList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "PersonalWorkExperienceSearch.aspx";
            UrlPageDetail = "PersonalWorkExperienceDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.QueryPersonalWorkExperience; //TODO: Isi ProgramID
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
            string id = dataItem.GetDataKeyValue(PersonalWorkExperienceMetadata.ColumnNames.PersonalWorkExperienceID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = PersonalWorkExperiences;
        }

        private DataTable PersonalWorkExperiences
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				PersonalWorkExperienceQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (PersonalWorkExperienceQuery)Session[SessionNameForQuery];
                }
                else
                {
                    PersonalWorkExperienceCollection coll = new PersonalWorkExperienceCollection();
                    AppStandardReferenceItemQuery address = new AppStandardReferenceItemQuery("c");
                    PersonalInfoQuery posquery = new PersonalInfoQuery("b");
                    query = new PersonalWorkExperienceQuery("a");

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select
                        (
                           query.PersonalWorkExperienceID,
                           posquery.EmployeeNumber,
                           posquery.EmployeeName,
                           address.ItemName.As("LineBisnisName"),
                           query.StartDate,
                           query.EndDate,
                           query.Company,
                           query.Division,
                           query.Location,
                           query.JobDesc,
                           query.SupervisorName,
                           query.LastSalary,
                           query.ReasonOfLeaving
                        );

                    query.InnerJoin(posquery).On(query.PersonID == posquery.PersonID);
                    query.LeftJoin(address).On
                            (
                                query.SRLineBisnis == address.ItemID &
                                address.StandardReferenceID == "LineBusiness"
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

