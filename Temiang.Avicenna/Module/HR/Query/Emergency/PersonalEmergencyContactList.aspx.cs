using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class PersonalEmergencyContactList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "PersonalEmergencyContactSearch.aspx";
            UrlPageDetail = "PersonalEmergencyContactDetail.aspx";
            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.QueryPersonalEmergency; //TODO: Isi ProgramID
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
            string id = dataItem.GetDataKeyValue(PersonalEmergencyContactMetadata.ColumnNames.PersonalEmergencyContactID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = PersonalEmergencyContacts;
        }

        private DataTable PersonalEmergencyContacts
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				PersonalEmergencyContactQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (PersonalEmergencyContactQuery)Session[SessionNameForQuery];
                }
                else
                {
                    AppStandardReferenceItemQuery city = new AppStandardReferenceItemQuery("f");
                    AppStandardReferenceItemQuery state = new AppStandardReferenceItemQuery("e");
                    AppStandardReferenceItemQuery addressType = new AppStandardReferenceItemQuery("d");
                    AppStandardReferenceItemQuery gender = new AppStandardReferenceItemQuery("c");
                    PersonalInfoQuery personal = new PersonalInfoQuery("b");
                    query = new PersonalEmergencyContactQuery("a");


                    query.es.Top = AppSession.Parameter.MaxResultRecord;     
                    query.Select( query.PersonalEmergencyContactID,
                                  query.PersonID,
                                  personal.EmployeeNumber,
                                  personal.EmployeeName,
                                  query.Address,
                                  state.ItemName.As("StateName"),
                                  city.ItemName.As("CityName"),
                                  query.ZipCode,
                                  query.Phone,
                                  query.Mobile,
                                  query.LastUpdateByUserID,
                                  query.LastUpdateDateTime
                                   );

                    query.InnerJoin(personal).On
                        (
                            query.PersonID == personal.PersonID 
                        );
                    
                    query.LeftJoin(state).On
                        (
                            query.SRState == state.ItemID &
                            state.StandardReferenceID == "Province"
                        );
                    query.LeftJoin(city).On
                        (
                            query.SRCity == city.ItemID &
                            city.StandardReferenceID == "City"
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

