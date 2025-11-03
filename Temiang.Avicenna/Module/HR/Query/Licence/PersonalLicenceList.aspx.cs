using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class PersonalLicenceList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "PersonalLicenceSearch.aspx";
            UrlPageDetail = "PersonalLicenceDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.QueryPersonalLicence; //TODO: Isi ProgramID
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
            string id = dataItem.GetDataKeyValue(PersonalLicenceMetadata.ColumnNames.PersonalLicenceID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = PersonalLicences;
        }

        private DataTable PersonalLicences
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				PersonalLicenceQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (PersonalLicenceQuery)Session[SessionNameForQuery];
                }
                else
                {
                    AppStandardReferenceItemQuery licence = new AppStandardReferenceItemQuery("c");
                    PersonalInfoQuery personal = new PersonalInfoQuery("b");
                    query = new PersonalLicenceQuery("a");

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select
                        (
                           query.PersonalLicenceID,
                           query.PersonID,
                           personal.EmployeeNumber,
                           personal.EmployeeName,
                           licence.ItemName.As("LicenceTypeName"),
                           licence.Note.As("LicenceTypeNote"),
                           query.ValidFrom,
                           query.ValidTo,
                           query.Note
                        );

                    query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                    query.LeftJoin(licence).On
                            (
                                query.SRLicenceType == licence.ItemID &
                                licence.StandardReferenceID == "LicenseType"
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

