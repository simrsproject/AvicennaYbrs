using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class PatientFallRiskPreventionList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 400;

            UrlPageSearch = "PatientFallRiskPreventionSearch.aspx";
            UrlPageDetail = "PatientFallRiskPreventionDetail.aspx";

            ProgramID = AppConstant.Program.ComplianceWithEffortstoPreventTheRiskofPatientFalls;
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
            string id = dataItem.GetDataKeyValue(CompliancePatientFallRiskPreventionMetadata.ColumnNames.TransactionNo).ToString();
            Page.Response.Redirect("PatientFallRiskPreventionDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = CompliancePatientFallRiskPreventions;
        }

        private DataTable CompliancePatientFallRiskPreventions
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                CompliancePatientFallRiskPreventionQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (CompliancePatientFallRiskPreventionQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new CompliancePatientFallRiskPreventionQuery("a");
                    var qemp = new VwEmployeeTableQuery("b");
                    var qip = new AppStandardReferenceItemQuery("c");
                    var qdep = new OrganizationUnitQuery("d");
                    var qdiv = new OrganizationUnitQuery("e");
                    var qsub = new OrganizationUnitQuery("f");
                    var qunit = new OrganizationUnitQuery("g");
                    var qobs = new VwEmployeeTableQuery("h");

                    query.InnerJoin(qemp).On(qemp.PersonID == query.EmployeeID);
                    query.LeftJoin(qip).On(qip.StandardReferenceID == AppEnum.StandardReference.ProfessionType && qip.ItemID == qemp.SRProfessionType);
                    query.LeftJoin(qdep).On(qdep.OrganizationUnitID == query.DepartmentID);
                    query.LeftJoin(qdiv).On(qdiv.OrganizationUnitID == query.DivisionID);
                    query.LeftJoin(qsub).On(qsub.OrganizationUnitID == query.SubDivisionID);
                    query.LeftJoin(qunit).On(qunit.OrganizationUnitID == query.ServiceUnitID);
                    query.InnerJoin(qobs).On(qobs.PersonID == query.ObserverID);

                    query.OrderBy
                        (
                            query.TransactionDate.Descending, query.TransactionNo.Descending
                        );

                    query.Select(
                        query.TransactionNo,
                        query.TransactionDate,
                        query.EmployeeID,
                        qemp.EmployeeNumber,
                        qemp.EmployeeName,
                        qip.ItemName.As("ProfessionType"),
                        qdep.OrganizationUnitName.As("DepartmentName"),
                        qdiv.OrganizationUnitName.As("DivisionName"),
                        qsub.OrganizationUnitName.As("SubDivisionName"),
                        qunit.OrganizationUnitName.As("ServiceUnitName"),
                        qobs.EmployeeName.As("ObserverName"),
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