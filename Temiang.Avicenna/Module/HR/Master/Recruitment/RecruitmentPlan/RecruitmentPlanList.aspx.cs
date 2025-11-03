using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Recruitment.Master
{
    public partial class RecruitmentPlanList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "RecruitmentPlanSearch.aspx";
            UrlPageDetail = "RecruitmentPlanDetail.aspx";

            ProgramID = AppConstant.Program.RecruitmentPlan; //TODO: Isi ProgramID
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
            string id = dataItem.GetDataKeyValue(RecruitmentPlanMetadata.ColumnNames.RecruitmentPlanID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = RecruitmentPlans;
        }

        private DataTable RecruitmentPlans
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				RecruitmentPlanQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (RecruitmentPlanQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new RecruitmentPlanQuery("a");
                    var depart = new OrganizationUnitQuery("b");
                    var divi = new OrganizationUnitQuery("c");
                    var subdiv = new OrganizationUnitQuery("d");
                    var unit = new OrganizationUnitQuery("e");
                    var posisi = new PositionQuery("f");
                    query.LeftJoin(depart).On(query.OrganizationUnitID == depart.OrganizationUnitID);
                    query.LeftJoin(divi).On(query.DivisionID == divi.OrganizationUnitID);
                    query.LeftJoin(subdiv).On(query.SubDivisionID == subdiv.OrganizationUnitID);
                    query.LeftJoin(unit).On(query.SectionID == unit.OrganizationUnitID);
                    query.LeftJoin(posisi).On(query.PositionID == posisi.PositionID);
                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(
                                    query.RecruitmentPlanID,
                                    query.RecruitmentPlanName,
                                    query.OrganizationUnitID,
                                    depart.OrganizationUnitName.As("DepartmentName"),
                                    query.DivisionID,
                                    divi.OrganizationUnitName.As("DivisionName"),
                                    query.SubDivisionID,
                                    subdiv.OrganizationUnitName.As("SubDivisionName"),
                                    query.SectionID,
                                    unit.OrganizationUnitName.As("SectionName"),
                                    posisi.PositionName,
                                    query.PositionID,

                                    query.ValidFrom,
                                    query.ValidTo,
                                    //query.NumberOfRequestedEmployees,
                                    "<CAST(a.NumberOfRequestedEmployees AS VARCHAR(MAX)) + '/' + CAST(ISNULL((SELECT COUNT(*) FROM EmployeeEmploymentPeriod AS eep WHERE eep.RecruitmentPlanID = a.RecruitmentPlanID), 0) AS VARCHAR(MAX)) AS NumberOfRequestedEmployees>",
                                    query.LastUpdateDateTime,
                                    query.LastUpdateByUserID
                                );
                }
				
				DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

