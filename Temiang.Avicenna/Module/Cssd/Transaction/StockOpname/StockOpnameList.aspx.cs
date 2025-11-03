using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class StockOpnameList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "StockOpnameSearch.aspx";
            UrlPageDetail = "StockOpnameDetail.aspx";

            ProgramID = AppConstant.Program.CssdStockOpname;

            WindowSearch.Height = 400;
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"openWinStockOpnameAdd(); args.set_cancel(true);");
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
            string id = dataItem.GetDataKeyValue(CssdStockOpnameMetadata.ColumnNames.TransactionNo).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = CssdStockOpnames;
        }

        private DataTable CssdStockOpnames
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                CssdStockOpnameQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (CssdStockOpnameQuery)Session[SessionNameForQuery];
                else
                {
                    query = new CssdStockOpnameQuery("a");
                    var unit = new ServiceUnitQuery("b");
                    var user = new AppUserServiceUnitQuery("e");
                    
                    query.Select
                        (
                            query.TransactionNo,
                            query.TransactionDate,
                            unit.ServiceUnitName,
                            query.Notes,
                            "<CAST((CASE WHEN (SELECT COUNT(TransactionNo) FROM CssdStockOpnameApproval WHERE TransactionNo = a.TransactionNo AND IsApproved = 0) = 0 THEN 1 ELSE 0 END) AS BIT) AS IsApproved>",
                            query.IsVoid
                        );
                    query.InnerJoin(unit).On(unit.ServiceUnitID == query.ServiceUnitID);
                    query.InnerJoin(user).On(user.UserID == AppSession.UserLogin.UserID && user.ServiceUnitID == query.ServiceUnitID);
                    
                    query.OrderBy(query.TransactionNo.Descending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;

                return dtb;
            }
        }
    }
}