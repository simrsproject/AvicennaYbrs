using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class MonitoringToiletList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 400;

            UrlPageSearch = "MonitoringToiletSearch.aspx";
            UrlPageDetail = "MonitoringToiletDetail.aspx";

            ProgramID = AppConstant.Program.MonitoringToilet;
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
            string id = dataItem.GetDataKeyValue(MonitoringToiletMetadata.ColumnNames.TransactionNo).ToString();
            Page.Response.Redirect("MonitoringToiletDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = MonitoringToilets;
        }

        private DataTable MonitoringToilets
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                MonitoringToiletQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (MonitoringToiletQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new MonitoringToiletQuery("a");
                    var su = new ServiceUnitQuery("b");
                    var user = new AppUserServiceUnitQuery("c");

                    query.InnerJoin(su).On(query.ServiceUnitID == su.ServiceUnitID);
                    query.InnerJoin(user).On(user.ServiceUnitID == query.ServiceUnitID & user.UserID == AppSession.UserLogin.UserID);

                    query.OrderBy
                        (
                            query.TransactionDate.Descending, query.TransactionNo.Descending
                        );

                    query.Select(
                        query.TransactionNo,
                        query.TransactionDate,
                        query.UserName,
                        su.ServiceUnitName.As("ServiceUnitName"),
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