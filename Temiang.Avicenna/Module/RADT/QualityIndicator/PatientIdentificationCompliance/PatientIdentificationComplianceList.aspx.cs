using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class PatientIdentificationComplianceList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 400;

            UrlPageSearch = "PatientIdentificationComplianceSearch.aspx";
            UrlPageDetail = "PatientIdentificationComplianceDetail.aspx";

            ProgramID = AppConstant.Program.PatientIdentificationCompliance;
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
            string id = dataItem.GetDataKeyValue(PatientIdentificationComplianceMetadata.ColumnNames.TransactionNo).ToString();
            Page.Response.Redirect("PatientIdentificationComplianceDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = PatientIdentificationCompliances;
        }

        private DataTable PatientIdentificationCompliances
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                PatientIdentificationComplianceQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (PatientIdentificationComplianceQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new PatientIdentificationComplianceQuery("a");
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
                    query.LeftJoin(qunit).On(qunit.OrganizationUnitID.ToString() == query.ServiceUnitID);
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