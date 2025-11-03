using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class EmployeeWorkingInfoList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "EmployeeWorkingInfoSearch.aspx?status=" + Request.QueryString["status"];
            UrlPageDetail = "EmployeeWorkingInfoDetail.aspx";
            UrlPageDetailNew = string.Format("{0}?md={1}&status={2}", "EmployeeWorkingInfoDetail.aspx", "new", Request.QueryString["status"]);

            this.WindowSearch.Height = 400;

            if (Request.QueryString["status"] == "recruit")
            {
                ProgramID = AppConstant.Program.ApplicantWorkingInfo;
                grdList.Columns.FindByUniqueName("EmployeeNumber").HeaderText = "Applicant No";
                grdList.Columns.FindByUniqueName("EmployeeName").HeaderText = "Applicant Name";
            }
            else
                ProgramID = Request.QueryString["status"] == "trn"? AppConstant.Program.EmployeeOrientation : AppConstant.Program.EmployeeWorkingInfo; 

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;

            if (!IsPostBack)
            {
                tblDocUpload.Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSMM" || AppSession.Parameter.HealthcareInitialAppsVersion == "RSMP";
                CollapsePanel1.IsCollapsed = "true";
            }
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
            string id = dataItem.GetDataKeyValue(EmployeeWorkingInfoMetadata.ColumnNames.PersonID).ToString();
            string url = string.Format("{0}?md={1}&id={2}&status={3}", UrlPageDetail, mode, id, Request.QueryString["status"]);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = EmployeeWorkingInfos;
        }

        private DataTable EmployeeWorkingInfos
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                VwEmployeeTableQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (VwEmployeeTableQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new VwEmployeeTableQuery("a");
                    var emplGrade = new PositionGradeQuery("i");
                    var position = new PositionQuery("h");
                    var unit = new OrganizationUnitQuery("g");
                    var info = new EmployeeWorkingInfoQuery("f");
                    var remuneration = new AppStandardReferenceItemQuery("e");
                    var type = new AppStandardReferenceItemQuery("d");
                    var status = new AppStandardReferenceItemQuery("c");
                    //var supervisor = new VwEmployeeTableQuery("b");
                    var supervisor = new PersonalInfoQuery("b");
                    var org = new OrganizationUnitQuery("j");
                    var div = new OrganizationUnitQuery("k");
                    var sdiv = new OrganizationUnitQuery("l");

                    query.LeftJoin(supervisor).On(query.SupervisorId == supervisor.PersonID);
                    query.LeftJoin(status).On
                            (
                                query.SREmployeeStatus == status.ItemID &
                                status.StandardReferenceID == AppEnum.StandardReference.EmployeeStatus
                            );
                    query.LeftJoin(type).On
                            (
                                query.SREmployeeType == type.ItemID &
                                type.StandardReferenceID == AppEnum.StandardReference.EmployeeType
                            );
                    query.LeftJoin(remuneration).On
                            (
                                query.SRRemunerationType == remuneration.ItemID &
                                remuneration.StandardReferenceID == AppEnum.StandardReference.RemunerationType
                            );
                    query.LeftJoin(info).On(query.PersonID == info.PersonID);
                    query.LeftJoin(unit).On(query.ServiceUnitID == unit.OrganizationUnitID);
                    query.LeftJoin(position).On(query.PositionID == position.PositionID);
                    query.LeftJoin(emplGrade).On(query.PositionGradeID == emplGrade.PositionGradeID);
                    query.LeftJoin(org).On(org.OrganizationUnitID == query.OrganizationUnitID);
                    query.LeftJoin(div).On(div.OrganizationUnitID == query.SubOrganizationUnitID);
                    query.LeftJoin(sdiv).On(sdiv.OrganizationUnitID == query.SubDivisonID);

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(
                                   query.PersonID,
                                   query.EmployeeNumber.As("EmployeeNo"),
                                   query.EmployeeName,
                                   supervisor.EmployeeName.As("SupervisorName"),
                                   status.ItemName.As("EmployeeStatusName"),
                                   type.ItemName.As("EmployeeTypeName"),
                                   remuneration.ItemName.As("RemunerationTypeName"),
                                   info.AbsenceCardNo,
                                   query.JoinDate,
                                   position.PositionName,
                                   emplGrade.PositionGradeName,
                                   unit.OrganizationUnitName.As("ServiceUnitName"),
                                   org.OrganizationUnitName,
                                   info.LastUpdateDateTime,
                                   info.LastUpdateByUserID,
                                   @"<CASE WHEN k.OrganizationUnitName IS NULL THEN l.OrganizationUnitName ELSE (CASE WHEN l.OrganizationUnitName IS NULL THEN k.OrganizationUnitName ELSE k.OrganizationUnitName + ' - ' + l.OrganizationUnitName END) END AS 'Division'>"
                                );

                    if (Request.QueryString["status"] == "recruit")
                    {
                        var eepq = new EmployeeEmploymentPeriodQuery("eep");
                        query.LeftJoin(eepq).On(eepq.PersonID == query.PersonID && eepq.SREmploymentType == "0");
                        query.Select(@"<ISNULL(eep.EmployeeNumber, a.EmployeeNumber) AS EmployeeNumber>");
                        //query.Select(@"<ISNULL((SELECT TOP 1 x.EmployeeNumber FROM EmployeeEmploymentPeriod x WHERE x.PersonID = a.PersonID AND x.SREmploymentType = '0'), a.EmployeeNumber) AS EmployeeNumber>");
                        query.Where(query.SREmploymentType == "0");
                    }
                    else
                    {
                        query.Select(query.EmployeeNumber);
                        query.Where(query.SREmploymentType != "0");
                    }
                    query.OrderBy(query.PersonID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "EmployeeName", "EmployeeNumber");
                }
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

