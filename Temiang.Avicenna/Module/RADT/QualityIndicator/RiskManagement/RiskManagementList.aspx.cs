using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class RiskManagementList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 300;

            UrlPageSearch = "RiskManagementSearch.aspx";
            UrlPageDetail = "RiskManagementDetail.aspx";
            ProgramID = AppConstant.Program.RiskManagement;
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
            string id = dataItem.GetDataKeyValue(PatientIncidentMetadata.ColumnNames.PatientIncidentNo).ToString();
            string url = string.Format("RiskManagementDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = RiskManagements;
        }

        private DataTable RiskManagements
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                PatientIncidentQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (PatientIncidentQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new PatientIncidentQuery("a");
                    var qsu = new ServiceUnitQuery("b");
                    var qrefgr = new AppStandardReferenceItemQuery("f");

                    query.InnerJoin(qsu).On(query.ServiceUnitIDInCharge == qsu.ServiceUnitID);
                    query.LeftJoin(qrefgr).On(query.SRIncidentGroup == qrefgr.ItemID & qrefgr.StandardReferenceID == AppEnum.StandardReference.IncidentGroup.ToString());

                    query.Where(query.IsRiskManagement == true);

                    query.OrderBy
                        (
                            query.IncidentDateTime.Descending
                        );

                    query.Select(
                        query.PatientIncidentNo,
                                query.IncidentDateTime,
                                query.ServiceUnitIDInCharge.As("ServiceUnitID"),
                                qsu.ServiceUnitName,

                                query.SRIncidentType,
                                query.SRIncidentGroup,
                                query.SRClinicalImpact,
                                query.SRIncidentFollowUp,
                                query.ReportingDateTime,
                                query.ReportedByUserID,
                                qrefgr.ItemName.As("IncidentGroupName"),
                                query.IsApproved,
                                query.IsVerified
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
