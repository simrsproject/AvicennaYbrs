using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Master
{
    public partial class OrganizationUnitList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "OrganizationUnitSearch.aspx";
            UrlPageDetail = "OrganizationUnitDetail.aspx";

            WindowSearch.Height = 300;

            ProgramID = AppConstant.Program.OrganizationUnit; //TODO: Isi ProgramID
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
            string id = dataItem.GetDataKeyValue(OrganizationUnitMetadata.ColumnNames.OrganizationUnitID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = OrganizationUnits;
        }
        private DataTable OrganizationUnits
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                OrganizationUnitQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (OrganizationUnitQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new OrganizationUnitQuery("a");
                    var level = new AppStandardReferenceItemQuery("b");
                    var parent = new OrganizationUnitQuery("c");
                    var vet = new VwEmployeeTableQuery("d");
                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(
                                    query.OrganizationUnitID,
                                    query.OrganizationUnitCode,
                                    query.OrganizationUnitName,
                                    query.ParentOrganizationUnitID,
                                    parent.OrganizationUnitName.As("ParentOrganizationUnitName"),
                                    query.SROrganizationLevel,
                                    level.ItemName.As("OrganizationLevelName"),
                                    vet.EmployeeName,
                                    query.IsActive,
                                    query.LastUpdateDateTime,
                                    query.LastUpdateByUserID
                                );
                    query.LeftJoin(level).On
                            (
                                query.SROrganizationLevel == level.ItemID &&
                                level.StandardReferenceID == AppEnum.StandardReference.OrganizationLevel
                            );
                    query.LeftJoin(parent).On(query.ParentOrganizationUnitID == parent.OrganizationUnitID);
                    query.LeftJoin(vet).On(vet.PersonID == query.PersonID);
                    query.OrderBy(query.OrganizationUnitCode.Ascending);
                }

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
        protected string GetOrganizationUnitName(object srOrganizationLevel, object organizationUnitName)
        {
            if (srOrganizationLevel.Equals("3"))
                return organizationUnitName.ToString();
            if (srOrganizationLevel.Equals("2"))
                return "&nbsp;&nbsp;&nbsp;&nbsp;" + organizationUnitName.ToString();
            if (srOrganizationLevel.Equals("1"))
                return "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + organizationUnitName.ToString();
            
            return "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + organizationUnitName.ToString();
        }
    }
}

