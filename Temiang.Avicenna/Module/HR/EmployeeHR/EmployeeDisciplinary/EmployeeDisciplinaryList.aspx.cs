using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Employee
{
    public partial class EmployeeDisciplinaryList : BasePageList
    {
        private string FormType
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "EmployeeDisciplinarySearch.aspx?type=" + FormType;
            UrlPageDetail = "EmployeeDisciplinaryDetail.aspxtype=" + FormType;

            ProgramID = FormType == string.Empty ? AppConstant.Program.EmployeeDisciplinaryHistory : AppConstant.Program.EmployeeDisciplinary; 
        }
        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"gotoAddUrl('{0}'); args.set_cancel(true);", FormType);
            return script;
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
            string id = dataItem.GetDataKeyValue(EmployeeDisciplinaryMetadata.ColumnNames.EmployeeDisciplinaryID).ToString();
            string url = string.Format("EmployeeDisciplinaryDetail.aspx?md={0}&id={1}&type={2}", mode, id, FormType);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = EmployeeDisciplinarys;
        }

        private DataTable EmployeeDisciplinarys
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				EmployeeDisciplinaryQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (EmployeeDisciplinaryQuery)Session[SessionNameForQuery];
                }
                else
                {
                    AppStandardReferenceItemQuery vType = new AppStandardReferenceItemQuery("e");
                    AppStandardReferenceItemQuery vDegree = new AppStandardReferenceItemQuery("d");
                    AppStandardReferenceItemQuery warning = new AppStandardReferenceItemQuery("c");
                    var personal = new VwEmployeeTableQuery("b");
                    query = new EmployeeDisciplinaryQuery("a");

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(
                                    query.EmployeeDisciplinaryID,
                                    query.PersonID,
                                    personal.EmployeeNumber,
                                    personal.EmployeeName,
                                    query.SRWarningLevel,
                                    warning.ItemName.As("WarningLevelName"),
                                    query.IncidentDate,
                                    query.DateIssue,
                                    query.Violation,
                                    query.AdviceGiven,
                                    query.SRViolationDegree,
                                    vDegree.ItemName.As("ViolationDegreeName"),
                                    query.SRViolationType,
                                    vType.ItemName.As("ViolationTypeName"),
                                    query.LastUpdateDateTime,
                                    query.LastUpdateByUserID
                                );

                    query.LeftJoin(personal).On
                        (
                            query.PersonID == personal.PersonID
                        );
                    query.LeftJoin(warning).On
                            (
                                query.SRWarningLevel == warning.ItemID &
                                warning.StandardReferenceID == "WarningLevel"
                            );
                    query.LeftJoin(vDegree).On
                            (
                                query.SRViolationDegree == vDegree.ItemID &
                                vDegree.StandardReferenceID == "ViolationDegree"
                            );
                    query.LeftJoin(vType).On
                            (
                                query.SRViolationType == vType.ItemID &
                                vType.StandardReferenceID == "ViolationType"
                            );
                    if (FormType == "sv")
                    {
                        query.Where(personal.SupervisorId == AppSession.UserLogin.PersonID);
                    }
                    query.OrderBy(query.PersonID.Ascending);
                }
				
				DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

