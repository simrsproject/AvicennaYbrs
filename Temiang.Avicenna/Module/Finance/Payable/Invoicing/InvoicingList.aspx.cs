using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Data.Linq;
using Telerik.Web.UI;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Linq;
using Telerik.Web.Data.Extensions;

using System.Collections.Generic;
using System.Drawing;


namespace Temiang.Avicenna.Module.Finance.Payable
{
    public partial class InvoicingList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "InvoicingSearch.aspx?type=" + Request.QueryString["type"] + "&pg=0";
            UrlPageDetail = "InvoicingDetail.aspx?type=" + Request.QueryString["type"];

            this.WindowSearch.Height = 400;
            ProgramID = Request.QueryString["type"] == "1" ? AppConstant.Program.AP_INVOICING : AppConstant.Program.AP_INVOICING2;

            this.grdList.SortCommand += new GridSortCommandEventHandler(grdList_SortCommand);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!string.IsNullOrEmpty(Request.QueryString["pg"]))
                    grdList.CurrentPageIndex = int.Parse(Request.QueryString["pg"]);
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"gotoAddUrl('{0}', '{1}'); args.set_cancel(true);", Request.QueryString["type"], "0");
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
            string id = dataItem.GetDataKeyValue(InvoiceSupplierMetadata.ColumnNames.InvoiceNo).ToString();
            string url = string.Format("InvoicingDetail.aspx?md={0}&id={1}&type={2}&pg={3}", mode, id, Request.QueryString["type"], grdList.CurrentPageIndex);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //if (!IsPostBack && !IsListLoadRecordOnInit)
            //{
            //    grdList.DataSource = new String[] { };
            //    return;
            //}

            //if (!e.IsFromDetailTable)
            //{
            //    grdList.DataSource = InvoiceSuppliers;
            //}

            this.GenerateGrid();
        }

        protected void GenerateGrid()
        {
            string invoiceNo = string.Empty;
            DateTime? invoiceDate = DateTime.Now.Date;
            DateTime? invoiceDueDate = null;
            string supplierName = string.Empty;
            string invoiceSuppNo = string.Empty;
            string receivedNo = string.Empty;
            string purchaseOrderNo = string.Empty;

            if (Session[SessionNameForQuery] != null)
            {
                InvoicingSearch.SearchValue sv = (InvoicingSearch.SearchValue)Session[SessionNameForQuery];
                invoiceNo = sv.InvoiceNo;
                invoiceDate = sv.InvoiceDate;
                invoiceDueDate = sv.InvoiceDueDate;
                supplierName = sv.SupplierName;
                invoiceSuppNo = sv.InvoiceSuppNo;
                receivedNo = sv.ReceivedNo;
                purchaseOrderNo = sv.PurchaseOrderNo;
            }

            if (!this.IsPostBack)
            {
                GridSortExpression sortExpr3 = new GridSortExpression();
                sortExpr3.FieldName = InvoicesMetadata.ColumnNames.InvoiceNo;
                sortExpr3.SortOrder = GridSortOrder.Descending;

                grdList.MasterTableView.SortExpressions.AllowMultiColumnSorting = true;
                grdList.MasterTableView.SortExpressions.AddSortExpression(sortExpr3);
                grdList.MasterTableView.SortExpressions.AllowNaturalSort = false;

            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (GridSortExpression e in grdList.MasterTableView.SortExpressions)
            {
                sb.AppendFormat("{0}^{1}", e.FieldName, e.SortOrder);
                sb.Append(",");
            }

            int totalCount = InvoiceSupplier.TotalCount(invoiceNo, invoiceDate, invoiceDueDate, supplierName, invoiceSuppNo, receivedNo, purchaseOrderNo);
            grdList.VirtualItemCount = totalCount;

            List<GridItem> items = new List<GridItem>();
            foreach (InvoiceSupplier entity in InvoiceSupplier.GetAllWithPaging(this.grdList.CurrentPageIndex, this.grdList.PageSize,
                 invoiceNo, invoiceDate, invoiceDueDate, supplierName, invoiceSuppNo, receivedNo, purchaseOrderNo, sb.ToString()))

            {
                items.Add(new GridItem(entity));
            }
            grdList.DataSource = items;
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string invoiceNo = dataItem.GetDataKeyValue("InvoiceNo").ToString();
            //Load record
            var query = new InvoiceSupplierItemQuery("a");
            var queryRcp = new ItemTransactionQuery("b");
            var pphType = new AppStandardReferenceItemQuery("pphtype");
            query.LeftJoin(queryRcp).On(query.TransactionNo == queryRcp.TransactionNo);
            query.LeftJoin(pphType).On(pphType.StandardReferenceID == AppEnum.StandardReference.Pph.ToString() && pphType.ItemID == query.SRPph);
            query.Where(query.InvoiceNo == invoiceNo);
            query.OrderBy(query.TransactionNo.Ascending);

            query.Select
                (
                    query.InvoiceNo,
                    query.TransactionNo,
                    query.TransactionDate,
                    queryRcp.InvoiceNo.As("InvoiceSuppNo"),
                    query.Amount,
                    query.PPnAmount,
                    //query.PPh22Amount,
                    //query.PPh23Amount,
                    pphType.ItemName.As("PphTypeName"),
                    query.PphAmount,
                    query.StampAmount,
                    query.DownPaymentAmount,
                    query.OtherDeduction,
                    query.VerifyAmount,
                    query.PaymentAmount,
                    query.Notes,
                    query.VerifyDate,
                    query.PaymentDate,
                    query.IsPaymentApproved
                );

            DataTable dtb = query.LoadDataTable();
            //Apply
            e.DetailTableView.DataSource = dtb;
        }

        //private DataTable InvoiceSuppliers
        //{
        //    get
        //    {
        //        object obj = this.Session[SessionNameForList];
        //        if (obj != null)
        //        {
        //            return ((DataTable)(obj));
        //        }

        //        InvoiceSupplierQuery query;
        //        if (Session[SessionNameForQuery] != null)
        //        {
        //            query = (InvoiceSupplierQuery)Session[SessionNameForQuery];
        //        }
        //        else
        //        {
        //            query = new InvoiceSupplierQuery("a");
        //            var supp = new SupplierQuery("b");
        //            var sr = new AppStandardReferenceItemQuery("c");
        //            var det = new InvoiceSupplierItemQuery("d");
        //            var por = new ItemTransactionQuery("por");

        //            query.LeftJoin(supp).On(query.SupplierID == supp.SupplierID);
        //            query.LeftJoin(sr).On(query.SRPayableStatus == sr.ItemID &&
        //                                  sr.StandardReferenceID == AppEnum.StandardReference.PayableStatus);
        //            query.LeftJoin(det).On(query.InvoiceNo == det.InvoiceNo);
        //            query.LeftJoin(por).On(det.TransactionNo == por.TransactionNo);
        //            query.Select(
        //                   query.InvoiceNo,
        //                   query.InvoiceDate,
        //                   query.InvoiceDueDate,
        //                   supp.SupplierName,
        //                   query.InvoiceSuppNo,
        //                   query.IsApproved,
        //                   query.IsVoid,
        //                   sr.ItemName.As("refToAppStandardReference_PayableStatusName"),
        //                   "<SUM(ISNULL(d.Amount, 0) + ISNULL(d.PPnAmount, 0)  + ISNULL(d.StampAmount, 0) - ISNULL(d.DownPaymentAmount, 0) - ISNULL(d.OtherDeduction, 0) - ISNULL(d.PphAmount, 0)) AS Total>"
        //               );
        //            query.Where(query.IsInvoicePayment == false, query.IsConsignment == false);
        //            query.GroupBy(query.InvoiceNo, query.InvoiceDate, query.InvoiceDueDate, supp.SupplierName,
        //                          query.InvoiceSuppNo, query.IsApproved, query.IsVoid, sr.ItemName);
        //            query.OrderBy(query.InvoiceNo.Descending);
        //        }

        //        query.es.Top = AppSession.Parameter.MaxResultRecord;
        //        DataTable dtb = query.LoadDataTable();
        //        if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP")
        //        {
        //            foreach (DataRow row in dtb.AsEnumerable().Where(d => d.Field<bool>("IsApproved") && !d.Field<bool>("IsVoid")))
        //            {
        //                var count = (new InvoiceSupplierCollection()).ItemTransactionOutstandingByInvoiceNo(row["InvoiceNo"].ToString());
        //                if (count.Rows.Count == 0) row["refToAppStandardReference_PayableStatusName"] = "Paid";
        //            }
        //        }
        //        this.Session[SessionNameForList] = dtb;
        //        return dtb;
        //    }
        //}

        protected void grdList_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
        {
            if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
            {
                GridSortExpression sortExpr = new GridSortExpression();
                sortExpr.FieldName = e.SortExpression;
                sortExpr.SortOrder = e.NewSortOrder;

                grdList.MasterTableView.SortExpressions.Clear();
                grdList.MasterTableView.SortExpressions.AddSortExpression(sortExpr);

                grdList.Rebind();
            }
        }

        protected class GridItem
        {
            // Fields
            private readonly InvoiceSupplier Entity;

            // Methods
            public GridItem(InvoiceSupplier entity)
            {
                this.Entity = entity;
            }

            public string InvoiceNo
            {
                get
                {
                    return this.Entity.InvoiceNo;
                }
            }
            public DateTime InvoiceDate
            {
                get
                {
                    return this.Entity.InvoiceDate.Value;
                }
            }
            public DateTime InvoiceDueDate
            {
                get
                {
                    return this.Entity.InvoiceDueDate.Value;
                }
            }
            public string SupplierName
            {
                get
                {
                    return this.Entity.SupplierName;
                }
            }
            public string PayableStatus
            {
                get
                {
                    return this.Entity.PayableStatus;
                }
            }
            public decimal Total
            {
                get
                {
                    return this.Entity.Total;
                }
            }
            public bool IsApproved
            {
                get
                {
                    return this.Entity.IsApproved ?? false;
                }
            }
            public bool IsVoid
            {
                get
                {
                    return this.Entity.IsVoid ?? false;
                }
            }
        }
    }
}