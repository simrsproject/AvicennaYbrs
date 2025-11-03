using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class GeneralCleanlinessList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 400;

            UrlPageSearch = "GeneralCleanlinessSearch.aspx";
            UrlPageDetail = "GeneralCleanlinessDetail.aspx";

            ProgramID = AppConstant.Program.GeneralCleanliness;
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
            string id = dataItem.GetDataKeyValue(GeneralCleanlinessMetadata.ColumnNames.TransactionNo).ToString();
            Page.Response.Redirect("GeneralCleanlinessDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = GeneralCleanlinesss;
        }

        private DataTable GeneralCleanlinesss
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                GeneralCleanlinessQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (GeneralCleanlinessQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new GeneralCleanlinessQuery("a");                          
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
