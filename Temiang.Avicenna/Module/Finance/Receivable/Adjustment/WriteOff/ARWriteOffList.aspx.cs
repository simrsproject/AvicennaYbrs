using System;
using System.Data;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Receivable.Adjustment.WriteOff
{
    public partial class ARWriteOffList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 400;

            UrlPageSearch = "ARWriteOffSearch.aspx";
            UrlPageDetail = "ARWriteOffDetail.aspx";

            ProgramID = AppConstant.Program.AR_WRITEOFF;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

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
            string id = dataItem.GetDataKeyValue(InvoicesMetadata.ColumnNames.InvoiceNo).ToString();
            string url = string.Format("ARWriteOffDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Invoices;
        }


        private DataTable Invoices
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                InvoicesQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (InvoicesQuery)Session[SessionNameForQuery];
                else
                {
                    query = new InvoicesQuery("a");
                    var guar = new GuarantorQuery("b");

                    query.InnerJoin(guar).On(query.GuarantorID == guar.GuarantorID);

                    query.Select(
                           query.InvoiceNo,
                           query.InvoiceReferenceNo,
                           query.PaymentDate,
                           guar.GuarantorName,
                           query.IsApproved,
                           query.IsVoid
                       );

                    query.Where(query.IsInvoicePayment == true, query.IsWriteOff == true );
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
