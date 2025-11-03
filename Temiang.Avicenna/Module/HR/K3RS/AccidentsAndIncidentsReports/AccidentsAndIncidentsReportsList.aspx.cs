using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.K3RS
{
    public partial class AccidentsAndIncidentsReportsList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 400;

            UrlPageSearch = "AccidentsAndIncidentsReportsSearch.aspx";
            UrlPageDetail = "AccidentsAndIncidentsReportsDetail.aspx";
            ProgramID = AppConstant.Program.K3RS_EmployeeIncident;

            if (!IsPostBack)
            {
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
            string id = dataItem.GetDataKeyValue(EmployeeAccidentReportsMetadata.ColumnNames.TransactionNo).ToString();
            string url = string.Format("AccidentsAndIncidentsReportsDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = EmployeeAccidentReportses;
        }

        private DataTable EmployeeAccidentReportses
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                EmployeeAccidentReportsQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (EmployeeAccidentReportsQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new EmployeeAccidentReportsQuery("a");
                    var qemp = new VwEmployeeTableQuery("b");
                    var qpos = new PositionQuery("c");
                    var qic = new AppStandardReferenceItemQuery("d");
                    var qis = new AppStandardReferenceItemQuery("e");
                    var qit = new AppStandardReferenceItemQuery("f");

                    query.InnerJoin(qemp).On(qemp.PersonID == query.PersonID);
                    query.LeftJoin(qpos).On(qpos.PositionID == qemp.PositionID);
                    query.LeftJoin(qic).On(qic.StandardReferenceID == AppEnum.StandardReference.EmployeeInjuryCategory && qic.ItemID == query.SREmployeeInjuryCategory);
                    query.LeftJoin(qis).On(qis.StandardReferenceID == AppEnum.StandardReference.EmployeeIncidentStatus && qis.ItemID == query.SREmployeeIncidentStatus);
                    query.LeftJoin(qit).On(qit.StandardReferenceID == AppEnum.StandardReference.EmployeeIncidentType && qit.ItemID == query.SREmployeeIncidentType);

                    query.OrderBy
                        (
                            query.IncidentDateTime.Descending
                        );

                    query.Select(
                        query.TransactionNo,
                        query.ReportingDateTime,
                        query.IncidentDateTime,
                        query.PersonID,
                        qemp.EmployeeNumber,
                        qemp.EmployeeName,
                        qpos.PositionName,
                        qic.ItemName.As("EmployeeInjuryCategory"),
                        qis.ItemName.As("EmployeeIncidentStatus"),
                        qit.ItemName.As("EmployeeIncidentType"),
                        query.IsApproved,
                        query.IsVoid
                        );
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}