using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Payable
{
    public partial class APWriteOffList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AP_WRITEOFF;

            UrlPageSearch = "APWriteOffSearch.aspx";
            UrlPageDetail = "APWriteOffDetail.aspx";

           this.WindowSearch.Height = 400;
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
            string id = dataItem.GetDataKeyValue(InvoiceSupplierMetadata.ColumnNames.InvoiceNo).ToString();
            string url = string.Format("APWriteOffDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList.DataSource = new String[] { };
                return;
            }

            grdList.DataSource = InvoicesSuppliers;
        }

        private DataTable InvoicesSuppliers
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                InvoiceSupplierQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (InvoiceSupplierQuery)Session[SessionNameForQuery];
                else
                {
                    query = new InvoiceSupplierQuery("a");
                    var supp = new SupplierQuery("b");

                    query.InnerJoin(supp).On(query.SupplierID == supp.SupplierID);

                    query.Select(
                           query.InvoiceNo,
                           query.InvoiceReferenceNo,
                           query.InvoiceDate.As("PaymentDate"),
                           supp.SupplierName,
                           query.IsApproved,
                           query.IsVoid
                       );

                    query.Where(query.IsInvoicePayment == true, query.IsWriteOff == true);
                    query.OrderBy(query.InvoiceNo.Descending);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }

           }
}