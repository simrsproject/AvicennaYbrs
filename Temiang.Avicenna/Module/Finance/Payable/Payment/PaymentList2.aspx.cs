using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Linq;
using Telerik.Web.Data.Extensions;

using System.Collections.Generic;
using System.Drawing;

namespace Temiang.Avicenna.Module.Finance.Payable
{
    public partial class PaymentList2 : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AP_PAYMENT;

            UrlPageSearch = "PaymentSearch.aspx?pg=0";
            UrlPageDetail = "PaymentDetail2.aspx";

            this.WindowSearch.Height = 400;

            this.grdList.SortCommand += new GridSortCommandEventHandler(grdList_SortCommand);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!string.IsNullOrEmpty(Request.QueryString["pg"]))
                    grdList.CurrentPageIndex = int.Parse(Request.QueryString["pg"]);
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
            string url = string.Format("PaymentDetail2.aspx?md={0}&id={1}&pg={2}", mode, id, grdList.CurrentPageIndex);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //if (!IsPostBack && !IsListLoadRecordOnInit)
            //{
            //    grdList.DataSource = new String[] { };
            //    return;
            //}

            //grdList.DataSource = InvoicesSuppliers;

            this.GenerateGrid();
        }

        protected void GenerateGrid()
        {
            string invoiceNo = string.Empty;
            DateTime? invoiceDate = DateTime.Now.Date;
            DateTime? invoiceDateTo = DateTime.Now.Date;
            string invoiceReferenceNo = string.Empty;
            string supplierName = string.Empty;
            string invoiceSuppNo = string.Empty;
            string status = string.Empty;

            if (Session[SessionNameForQuery] != null)
            {
                PaymentSearch.SearchValue sv = (PaymentSearch.SearchValue)Session[SessionNameForQuery];
                invoiceNo = sv.InvoiceNo;
                invoiceDate = sv.InvoiceDate;
                invoiceDateTo = sv.InvoiceDateTo;
                invoiceReferenceNo = sv.InvoiceReferenceNo;
                supplierName = sv.SupplierName;
                invoiceSuppNo = sv.InvoiceSuppNo;
                status = sv.Status;
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

            int totalCount = InvoiceSupplier.TotalPaymentCount(invoiceNo, invoiceDate, invoiceDateTo, invoiceReferenceNo, supplierName, invoiceSuppNo, status);
            grdList.VirtualItemCount = totalCount;

            List<GridItem> items = new List<GridItem>();
            foreach (InvoiceSupplier entity in InvoiceSupplier.GetAllPaymentWithPaging(this.grdList.CurrentPageIndex, this.grdList.PageSize,
                 invoiceNo, invoiceDate, invoiceDateTo, invoiceReferenceNo, supplierName, invoiceSuppNo, status, sb.ToString()))

            {
                items.Add(new GridItem(entity));
            }
            grdList.DataSource = items;
        }


        //private DataTable InvoicesSuppliers
        //{
        //    get
        //    {
        //        object obj = this.Session[SessionNameForList];
        //        if (obj != null)
        //            return ((DataTable)(obj));

        //        InvoiceSupplierQuery query;
        //        if (Session[SessionNameForQuery] != null)
        //            query = (InvoiceSupplierQuery)Session[SessionNameForQuery];
        //        else
        //        {
        //            query = new InvoiceSupplierQuery("a");
        //            var supp = new SupplierQuery("b");
        //            var itm = new InvoiceSupplierItemQuery("c");

        //            query.InnerJoin(itm).On(query.InvoiceNo == itm.InvoiceNo);
        //            query.LeftJoin(supp).On(query.SupplierID == supp.SupplierID);

        //            query.Select(
        //                   query.InvoiceNo,
        //                   query.InvoiceReferenceNo,
        //                   query.InvoiceDate,
        //                   supp.SupplierName,
        //                   query.IsApproved,
        //                   query.IsVoid,
        //                   "<SUM(c.PaymentAmount) AS Total>"
        //               );

        //            query.Where(query.IsInvoicePayment == true, query.IsWriteOff == false, query.IsAddPayment.IsNull());
        //            query.GroupBy(query.InvoiceNo,
        //                   query.InvoiceReferenceNo,
        //                   query.InvoiceDate,
        //                   supp.SupplierName,
        //                   query.IsApproved,
        //                   query.IsVoid);
        //            query.OrderBy(query.InvoiceNo.Descending);
        //        }
        //        query.es.Top = AppSession.Parameter.MaxResultRecord;
        //        DataTable dtb = query.LoadDataTable();
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
            public string InvoiceReferenceNo
            {
                get
                {
                    return this.Entity.InvoiceReferenceNo;
                }
            }
            public string SupplierName
            {
                get
                {
                    return this.Entity.SupplierName;
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
