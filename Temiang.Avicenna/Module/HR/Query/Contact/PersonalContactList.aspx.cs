using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class PersonalContactList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "PersonalContactSearch.aspx";
            UrlPageDetail = "PersonalContactDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.QueryPersonalContact; //TODO: Isi ProgramID
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
            string id = dataItem.GetDataKeyValue(PersonalContactMetadata.ColumnNames.PersonalContactID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = PersonalContacts;
        }

        private DataTable PersonalContacts
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				PersonalContactQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (PersonalContactQuery)Session[SessionNameForQuery];
                }
                else
                {
                    AppStandardReferenceItemQuery contactType = new AppStandardReferenceItemQuery("d");
                    AppStandardReferenceItemQuery gender = new AppStandardReferenceItemQuery("c");
                    PersonalInfoQuery personal = new PersonalInfoQuery("b");
                    query = new PersonalContactQuery("a");

                    query.Select(
                                query.PersonalContactID,
                                query.PersonID,
                                personal.EmployeeNumber,
                                personal.EmployeeName,
                                gender.ItemName.As("GenderTypeName"),
                                contactType.ItemName.As("ContactTypeName"),
                                query.SRContactType,
                                query.ContactValue,
                                query.LastUpdateDateTime,
                                query.LastUpdateByUserID
                            );
                    query.InnerJoin(personal).On
                        (
                            query.PersonID == personal.PersonID
                        );
                    query.InnerJoin(gender).On
                        (
                            personal.SRGenderType == gender.ItemID &
                            gender.StandardReferenceID == "GenderType"
                        );
                    query.InnerJoin(contactType).On
                        (
                            query.SRContactType == contactType.ItemID &
                            contactType.StandardReferenceID == "ContactType"
                        );
                    query.OrderBy(query.PersonID.Ascending); //TODO: Betulkan ordernya
                }
				query.es.Top = AppSession.Parameter.MaxResultRecord;				
				DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

