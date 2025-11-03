using System;
using System.Collections.Generic;
using System.Text;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class PersonalReligionList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "PersonalReligionSearch.aspx";
            UrlPageDetail = "PersonalReligionDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.QueryPersonalReligion; //TODO: Isi ProgramID
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
            string id = dataItem.GetDataKeyValue(PersonalInfoMetadata.ColumnNames.PersonID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = PersonalInfos;
        }

        private DataTable PersonalInfos
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                PersonalInfoQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (PersonalInfoQuery)Session[SessionNameForQuery];
                }
                else
                {
                    AppStandardReferenceItemQuery religion = new AppStandardReferenceItemQuery("b");
                    query = new PersonalInfoQuery("a");

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(
                                query.PersonID,
                                query.EmployeeNumber,
                                query.EmployeeName,
                                query.SRReligion,
                                religion.ItemName.As("ReligionName")
                            );
                    query.LeftJoin(religion).On
                    (
                        query.SRReligion == religion.ItemID &
                        religion.StandardReferenceID == "Religion"
                    );
                    query.OrderBy(query.PersonID.Ascending);
                }
                
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }        

    }
}

