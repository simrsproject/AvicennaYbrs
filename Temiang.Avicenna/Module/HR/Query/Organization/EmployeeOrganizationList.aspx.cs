using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class EmployeeOrganizationList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "EmployeeOrganizationSearch.aspx";
            UrlPageDetail = "EmployeeOrganizationDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.QueryEmployeeOrganization; //TODO: Isi ProgramID

            if (!IsPostBack)
                grdList.Columns[6].Visible = AppSession.Parameter.IsUsingFourLevelOrganizationUnit;
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
            string id = dataItem.GetDataKeyValue(EmployeeOrganizationMetadata.ColumnNames.EmployeeOrganizationID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = EmployeeOrganizations;
        }

        private DataTable EmployeeOrganizations
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				EmployeeOrganizationQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (EmployeeOrganizationQuery)Session[SessionNameForQuery];
                }
                else
                {
                    
                    OrganizationUnitQuery organization = new OrganizationUnitQuery("c");                    
                    PersonalInfoQuery personal = new PersonalInfoQuery("b");
                    query = new EmployeeOrganizationQuery("a");
                    var division = new OrganizationUnitQuery("d"); 
                    var section = new OrganizationUnitQuery("e");
                    var subdivision = new OrganizationUnitQuery("f");

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select
                        (
                           query.EmployeeOrganizationID,
                           query.PersonID,
                           personal.EmployeeNumber,
                           personal.EmployeeName,
                           query.OrganizationID,
                           organization.OrganizationUnitCode,
                           organization.OrganizationUnitName.As("Department"),
                           query.ValidFrom,
                           query.ValidTo,
                           query.IsActive,
                           division.OrganizationUnitName.As("Division"),
                           subdivision.OrganizationUnitName.As("SubDivision"),
                           section.OrganizationUnitName.As("Section")
                        );

                    query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                    query.LeftJoin(organization).On(query.OrganizationID == organization.OrganizationUnitID);
                    query.LeftJoin(division).On(query.SubOrganizationID == division.OrganizationUnitID);
                    query.LeftJoin(section).On(query.ServiceUnitID == section.OrganizationUnitID);
                    query.LeftJoin(subdivision).On(query.SubDivisonID == subdivision.OrganizationUnitID);

                    query.OrderBy(query.PersonID.Ascending); //TODO: Betulkan ordernya
                }
				
				
				DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

