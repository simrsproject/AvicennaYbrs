using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
namespace Temiang.Avicenna.Module.RADT.QualityIndicator.v2
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
            string id = dataItem.GetDataKeyValue(RiskManagementMetadata.ColumnNames.RiskManagementNo).ToString();
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

                RiskManagementQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (RiskManagementQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new RiskManagementQuery("a");
                    var qsu = new ServiceUnitQuery("b");

                    query.InnerJoin(qsu).On(query.ServiceUnitID == qsu.ServiceUnitID);
                    

                    query.OrderBy
                        (
                            query.RiskManagementNo.Descending
                        );

                    query.Select(
                        query.RiskManagementNo,
                                query.PeriodYear,
                                query.ServiceUnitID,
                                qsu.ServiceUnitName,
                                query.IsApproved
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