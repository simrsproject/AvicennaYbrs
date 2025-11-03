using System;
using System.Collections.Generic;
using System.Text;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class PersonalBloodTypeList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "PersonalBloodTypeSearch.aspx";
            UrlPageDetail = "PersonalBloodTypeDetail.aspx";

            ProgramID = AppConstant.Program.QueryPersonalBloodType; //TODO: Isi ProgramID
            WindowSearch.Height = 300;
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
                    query = new PersonalInfoQuery("a");
                    var std = new AppStandardReferenceItemQuery("b");

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(
                                query.PersonID,
                                query.EmployeeNumber,
                                query.EmployeeName,
                                query.SRReligion,
                                std.ItemName.As("BloodTypeName")
                            );
                    query.LeftJoin(std).On
                    (
                        query.SRBloodType == std.ItemID &
                        std.StandardReferenceID == "BloodType"
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