using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.KEHRS
{
    public partial class SafetyCultureIncidentReportsList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 400;

            UrlPageSearch = "SafetyCultureIncidentReportsSearch.aspx";
            UrlPageDetail = "SafetyCultureIncidentReportsDetail.aspx";
            ProgramID = AppConstant.Program.KEHRS_SafetyCultureIncidentReports;

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
            string id = dataItem.GetDataKeyValue(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.TransactionNo).ToString();
            string url = string.Format("SafetyCultureIncidentReportsDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = EmployeeSafetyCultureIncidentReportses;
        }

        private DataTable EmployeeSafetyCultureIncidentReportses
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                EmployeeSafetyCultureIncidentReportsQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (EmployeeSafetyCultureIncidentReportsQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new EmployeeSafetyCultureIncidentReportsQuery("a");
                    var qemp = new VwEmployeeTableQuery("b");
                    var qusrVic = new AppUserQuery("c");
                    var qusrSupervisor = new AppUserQuery("d");
                    var qusr = new AppUserQuery("e");
                    var qsubject = new EmployeeSafetyCultureIncidentReportsSubjectQuery("f");
                    var qic = new AppStandardReferenceItemQuery("g");

                    query.InnerJoin(qemp).On(qemp.PersonID == query.VictimPersonID);
                    query.LeftJoin(qusrVic).On(qusrVic.PersonID == query.VictimPersonID && qusrVic.PersonID != -1);
                    query.LeftJoin(qusrSupervisor).On(qusrSupervisor.PersonID == qemp.SupervisorId && qusrSupervisor.PersonID != -1);
                    query.InnerJoin(qusr).On(qusr.UserID == AppSession.UserLogin.UserID);
                    query.LeftJoin(qsubject).On(qsubject.TransactionNo == query.TransactionNo && qsubject.SubjectPersonID == qusr.PersonID && qusr.PersonID != -1);
                    query.LeftJoin(qic).On(qic.StandardReferenceID == AppEnum.StandardReference.EmployeeAccidentReportStatus && qic.ItemID == query.SRIncidentReportStatus);

                    query.es.Distinct = true;
                    query.Select(
                        query.TransactionNo,
                        query.ReportDate,
                        query.ReportDescription,
                        query.VictimPersonID,
                        qemp.EmployeeNumber,
                        qemp.EmployeeName,
                        query.IsApproved,
                        query.IsVoid,
                        query.IsVerified,
                        query.VerifiedDateTime,
                        qic.ItemName.As("IncidentReportStatus"),
                        query.IsClosed,
                        query.ClosedDateTime
                        );

                    query.Where
                        (
                        query.Or(
                            query.CreatedByUserID == AppSession.UserLogin.UserID, 
                            qusrVic.UserID == AppSession.UserLogin.UserID,
                            qusrSupervisor.UserID == AppSession.UserLogin.UserID
                            ),
                        qsubject.TransactionNo.IsNull()
                        );

                    query.OrderBy(query.TransactionNo.Descending);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}