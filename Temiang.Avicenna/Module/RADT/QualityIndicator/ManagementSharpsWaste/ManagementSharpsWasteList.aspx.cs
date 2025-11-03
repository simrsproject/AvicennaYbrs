using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class ManagementSharpsWasteList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 400;

            UrlPageSearch = "ManagementSharpsWasteSearch.aspx";
            UrlPageDetail = "ManagementSharpsWasteDetail.aspx";

            ProgramID = AppConstant.Program.ManagementSharpsWaste;
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
            string id = dataItem.GetDataKeyValue(ManagementSharpsWasteMetadata.ColumnNames.TransactionNo).ToString();
            Page.Response.Redirect("ManagementSharpsWasteDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ManagementSharpsWastes;
        }

        private DataTable ManagementSharpsWastes
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                ManagementSharpsWasteQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (ManagementSharpsWasteQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new ManagementSharpsWasteQuery("a");
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