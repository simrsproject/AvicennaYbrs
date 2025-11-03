using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;


namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class SupplierContractList : BasePageList
    {
        private DataTable SupplierContracts
        {
            get
            {
                object obj = Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                SupplierContractQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (SupplierContractQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new SupplierContractQuery("a");
                    var supp = new SupplierQuery("b");
                    query.InnerJoin(supp).On(query.SupplierID == supp.SupplierID);
                    query.Select(query, supp.SupplierName);
                    query.OrderBy(query.ContractStart.Descending);

                    //Quick Search
                    ApplyQuickSearch(query);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "SupplierContractSearch.aspx";
            UrlPageDetail = "SupplierContractDetail.aspx";

            ProgramID = AppConstant.Program.SupplierContract;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
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
            string id = dataItem.GetDataKeyValue(SupplierContractMetadata.ColumnNames.TransactionNo).ToString();
            string url = string.Format("SupplierContractDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = SupplierContracts;
        }
    }
}
