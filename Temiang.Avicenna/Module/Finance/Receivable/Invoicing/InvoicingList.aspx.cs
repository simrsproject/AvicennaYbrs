using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Receivable
{
    public partial class InvoicingList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "InvoicingSearch.aspx?pg=0";
            UrlPageDetail = "InvoicingDetail.aspx";

            this.WindowSearch.Height = 400;
            ProgramID = AppConstant.Program.AR_INVOICING;

            this.grdList.SortCommand += new GridSortCommandEventHandler(grdList_SortCommand);

            if (!IsPostBack)
            {
                if (!AppSession.Parameter.IsAllowDiscountInvoice)
                {
                    grdList.Columns.FindByUniqueName("TotalTransaction").Visible = false; // kolom total transaksi
                    grdList.Columns.FindByUniqueName("DiscountAmount").Visible = false; // kolom diskon
                }
            }
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
            string url = string.Format("InvoicingDetail.aspx?md={0}&id={1}&pg={2}", mode, id, grdList.CurrentPageIndex);
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
            //    grdList.DataSource = Invoicess;

            this.GenerateGrid();
        }

        protected void GenerateGrid()
        {
            string invoiceNo = string.Empty;
            DateTime? invoiceDate = DateTime.Now.Date;
            DateTime? invoiceDateTo = DateTime.Now.Date;
            DateTime? invoiceDueDate = null;
            string guarantorName = string.Empty;
            string paymentNo = string.Empty;
            string registrationNo = string.Empty;
            string medicalNo = string.Empty;
            string patientName = string.Empty;

            if (Session[SessionNameForQuery] != null)
            {
                InvoicingSearch.SearchValue sv = (InvoicingSearch.SearchValue)Session[SessionNameForQuery];
                invoiceNo = sv.InvoiceNo;
                invoiceDate = sv.InvoiceDate;
                invoiceDateTo = sv.InvoiceDateTo;
                invoiceDueDate = sv.InvoiceDueDate;
                guarantorName = sv.GuarantorName;
                paymentNo = sv.PaymentNo;
                registrationNo = sv.RegistrationNo;
                medicalNo = sv.MedicalNo;
                patientName = sv.PatientName;
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

            int totalCount = Invoices.TotalCount(invoiceNo, invoiceDate, invoiceDateTo, invoiceDueDate, guarantorName, paymentNo, registrationNo, medicalNo, patientName);
            grdList.VirtualItemCount = totalCount;

            List<GridItem> items = new List<GridItem>();
            foreach (Invoices entity in Invoices.GetAllWithPaging(this.grdList.CurrentPageIndex, this.grdList.PageSize,
                 invoiceNo, invoiceDate, invoiceDateTo, invoiceDueDate, guarantorName, paymentNo, registrationNo, medicalNo, patientName, sb.ToString()))

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
            var query = new InvoicesItemQuery("a");
            //var regQ = new RegistrationQuery("b");
            var guarQ = new GuarantorQuery("c");
            //var payQ = new TransPaymentQuery("d");
            var queryH = new InvoicesQuery("h");
            var patQ = new PatientQuery("pat");

            //query.InnerJoin(regQ).On(query.RegistrationNo == regQ.RegistrationNo);
            //query.LeftJoin(payQ).On(query.PaymentNo == payQ.PaymentNo);
            //query.LeftJoin(guarQ).On(payQ.GuarantorID == guarQ.GuarantorID);
            query.InnerJoin(queryH).On(query.InvoiceNo == queryH.InvoiceNo);
            query.InnerJoin(guarQ).On(queryH.GuarantorID == guarQ.GuarantorID);
            query.LeftJoin(patQ).On(patQ.PatientID == query.PatientID);
            query.Where(query.InvoiceNo == invoiceNo, query.InvoiceReferenceNo == string.Empty);
            query.OrderBy(query.PaymentNo.Ascending);

            query.Select
                (
                    query.InvoiceNo,
                    query.PaymentNo,
                    query.PaymentDate,
                    query.RegistrationNo,
                    patQ.MedicalNo,
                    query.PatientName,
                    query.Amount,
                    query.VerifyAmount,
                    query.PaymentAmount.Coalesce("0"),
                    query.OtherAmount.Coalesce("0"),
                    query.Notes,
                    guarQ.GuarantorName
                );

            DataTable dtb = query.LoadDataTable();
            //Apply
            e.DetailTableView.DataSource = dtb;
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    var dataItem = e.Item as GridDataItem;
                    if (dataItem["ReceivableStatus"].Text == "Verify")
                    {
                        if (dataItem["Aging"].Text.ToInt() <= 0)
                        {
                            // Beri warna merah jika sudah jatuh tempo
                            dataItem.ForeColor = Color.Red;
                            dataItem.Font.Bold = true;
                            dataItem.ToolTip = "The invoice is due.";
                        }
                        else if (dataItem["Aging"].Text.ToInt() > 0 && dataItem["Aging"].Text.ToInt() <= 7)
                        {
                            dataItem.ForeColor = Color.Orange;
                            dataItem.Font.Bold = true;
                            dataItem.ToolTip = "Aging between 1 to 7 day(s).";
                        }
                    }
                }
            }
            catch
            { }
        }

        //private DataTable Invoicess
        //{
        //    get
        //    {
        //        object obj = this.Session[SessionNameForList];
        //        if (obj != null)
        //            return ((DataTable)(obj));

        //        InvoicesQuery query;
        //        if (Session[SessionNameForQuery] != null)
        //            query = (InvoicesQuery)Session[SessionNameForQuery];
        //        else
        //        {
        //            query = new InvoicesQuery("a");
        //            var guar = new GuarantorQuery("b");
        //            var sr = new AppStandardReferenceItemQuery("c");
        //            var itm = new InvoicesItemQuery("d");

        //            query.LeftJoin(itm).On(itm.InvoiceNo == query.InvoiceNo);
        //            query.LeftJoin(guar).On(query.GuarantorID == guar.GuarantorID);
        //            query.LeftJoin(sr).On(
        //                query.SRReceivableStatus == sr.ItemID && 
        //                sr.StandardReferenceID == AppEnum.StandardReference.ReceivableStatus
        //                );

        //            query.Select(
        //                   query.InvoiceNo,
        //                   query.InvoiceDate,
        //                   query.InvoiceDueDate,
        //                   guar.GuarantorName,
        //                   query.IsApproved,
        //                   query.IsVoid,
        //                   sr.ItemName.As("ReceivableStatusName"),
        //                   query.AgingDate,
        //                   @"<CASE WHEN a.AgingDate IS NULL THEN '' ELSE CONVERT(VARCHAR, a.AgingDate, 103) END AS 'AgingDateStr'>",
        //                   "<SUM(ISNULL(d.Amount, 0)) AS TotalTransaction>",
        //                   "<ISNULL(a.DiscountAmount, 0) AS DiscountAmount>",
        //                   "<SUM(ISNULL(d.Amount, 0)) - ISNULL(a.DiscountAmount, 0) AS TotalAmount>",
        //                   @"<DATEDIFF(DAY, GETDATE(), a.InvoiceDueDate) AS Aging>"                           
        //               );

        //            query.Where(query.IsInvoicePayment == false, query.InvoiceReferenceNo.IsNull(), query.Or(query.IsAdditionalInvoice.IsNull(), query.IsAdditionalInvoice == false));
        //            query.GroupBy(query.InvoiceNo, query.InvoiceDate, query.InvoiceDueDate, guar.GuarantorName,
        //                          query.IsApproved, query.IsVoid, sr.ItemName, query.DiscountAmount, query.AgingDate);
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
            private readonly Invoices Entity;

            // Methods
            public GridItem(Invoices entity)
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
            public string GuarantorName
            {
                get
                {
                    return this.Entity.GuarantorName;
                }
            }
            public decimal TotalTransaction
            {
                get
                {
                    return this.Entity.TotalTransaction;
                }
            }
            public decimal TotalAmount
            {
                get
                {
                    return this.Entity.TotalAmount;
                }
            }

            public string ReceivableStatus
            {
                get
                {
                    return this.Entity.ReceivableStatus;
                }
            }
            public string AgingDateStr
            {
                get
                {
                    return this.Entity.AgingDateStr;
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

            public int Aging
            {
                get
                {
                    return this.Entity.Aging;
                }
            }
        }
    }
}
