using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class PersonalAddressList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "PersonalAddressSearch.aspx";
            UrlPageDetail = "PersonalAddressDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.QueryPersonalAddress; //TODO: Isi ProgramID
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
            string id = dataItem.GetDataKeyValue(PersonalAddressMetadata.ColumnNames.PersonalAddressID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = PersonalAddresss;
        }

        private DataTable PersonalAddresss
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                PersonalAddressQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (PersonalAddressQuery)Session[SessionNameForQuery];
                }
                else
                {
                    AppStandardReferenceItemQuery state = new AppStandardReferenceItemQuery("e");
                    AppStandardReferenceItemQuery addressType = new AppStandardReferenceItemQuery("d");
                    AppStandardReferenceItemQuery gender = new AppStandardReferenceItemQuery("c");
                    PersonalInfoQuery personal = new PersonalInfoQuery("b");
                    query = new PersonalAddressQuery("a");


                    query.es.Top = AppSession.Parameter.MaxResultRecord;     
                    query.Select( query.PersonalAddressID,
                                  query.PersonID,
                                  personal.EmployeeNumber,
                                  personal.EmployeeName,
                                  gender.ItemName.As("GenderTypeName"),
                                  addressType.ItemName.As("AddressTypeName"),
                                  query.Address,
                                  query.District,
                                  query.County,
                                  query.City,
                                  state.ItemName.As("StateName"),
                                  query.ZipCode,
                                  query.LastUpdateByUserID,
                                  query.LastUpdateDateTime
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
                    query.InnerJoin(addressType).On
                        (
                            query.SRAddressType == addressType.ItemID &
                            addressType.StandardReferenceID == "AddressType"
                        );
                    query.LeftJoin(state).On
                        (
                            query.SRState == state.ItemID &
                            state.StandardReferenceID == "Province"
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

