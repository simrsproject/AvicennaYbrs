using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Finance.Receivable
{
    public partial class PaymentList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AR_PAYMENT;

            UrlPageSearch = "PaymentSearch.aspx?pg=0";
            UrlPageDetail = "PaymentDetail.aspx";

            this.grdList.SortCommand += new GridSortCommandEventHandler(grdList_SortCommand);

            this.WindowSearch.Height = 400;
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
            string id = dataItem.GetDataKeyValue(InvoicesMetadata.ColumnNames.InvoiceNo).ToString();
            string url = string.Format("PaymentDetail.aspx?md={0}&id={1}&pg={2}", mode, id, grdList.CurrentPageIndex);
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
            string invoiceReferenceNo = string.Empty;
            DateTime? invoiceDate = null;
            DateTime? invoiceDateTo = null;
            DateTime? paymentDate = DateTime.Now.Date;
            DateTime? paymentDateTo = DateTime.Now.Date;
            string guarantorName = string.Empty;
            string registrationNo = string.Empty;
            string medicalNo = string.Empty;
            string patientName = string.Empty;

            if (Session[SessionNameForQuery] != null)
            {
                PaymentSearch.SearchValue sv = (PaymentSearch.SearchValue)Session[SessionNameForQuery];
                invoiceNo = sv.InvoiceNo;
                invoiceReferenceNo = sv.InvoiceReferenceNo;
                invoiceDate = sv.InvoiceDate;
                invoiceDateTo = sv.InvoiceDateTo;
                paymentDate = sv.PaymentDate;
                paymentDateTo = sv.PaymentDateTo;
                guarantorName = sv.GuarantorName;
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

            int totalCount = Invoices.TotalPaymentCount(invoiceNo, invoiceReferenceNo, invoiceDate, invoiceDateTo, paymentDate, paymentDateTo, guarantorName, registrationNo, medicalNo, patientName);
            grdList.VirtualItemCount = totalCount;

            List<GridItem> items = new List<GridItem>();
            foreach (Invoices entity in Invoices.GetAllPaymentWithPaging(this.grdList.CurrentPageIndex, this.grdList.PageSize,
                 invoiceNo, invoiceReferenceNo, invoiceDate, invoiceDateTo, paymentDate, paymentDateTo, guarantorName, registrationNo, medicalNo, patientName, sb.ToString()))

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
            var patQ = new PatientQuery("b");
            var drQ = new AppStandardReferenceItemQuery("c");

            query.LeftJoin(patQ).On(query.PatientID == patQ.PatientID);
            query.LeftJoin(drQ).On(query.SRDiscountReason == drQ.ItemID &&
                                   drQ.StandardReferenceID == AppEnum.StandardReference.DiscountReason.ToString());
            query.Where(query.InvoiceNo == invoiceNo);
            query.OrderBy(query.PaymentNo.Ascending);

            query.Select(
                query,
                patQ.MedicalNo,
                drQ.ItemName.As("DiscountReason")
                );

            DataTable dtb = query.LoadDataTable();
            //Apply
            e.DetailTableView.DataSource = dtb;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            pnlInfo.Visible = false;

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument.Contains("recalFeePerc"))
            {
                var param = eventArgument.Split('|');
                switch (param[0])
                {
                    case "recalFeePerc":
                        {
                            if (param[1].Equals("page"))
                            {
                                // looping grid
                                foreach (GridDataItem r in grdList.MasterTableView.Items)
                                {
                                    var invPayNo = r.GetDataKeyValue("InvoiceNo").ToString();
                                    RecalculatePercentagePaymentInvoiceOfParamedicFee(invPayNo);

                                    pnlInfo.Visible = false;
                                    lblInfo.Text = "Recalculation has completed";
                                }
                            }
                            else
                            {
                                RecalculatePercentagePaymentInvoiceOfParamedicFee(param[1]);
                                //var msg = (new ParamedicFeeVerification()).Approv(param[1], AppSession.UserLogin.UserID);
                                //if (!string.IsNullOrEmpty(msg))
                                //{
                                //    pnlInfo.Visible = true;
                                //    lblInfo.Text = string.Format("Recalculation of payment number {0} has completed", param[1]);
                                //}
                            }
                            break;
                        }
                }

                //grdList.Rebind();
            }
        }

        private void RecalculatePercentagePaymentInvoiceOfParamedicFee(string InvoicePaymentNo)
        {
            //using (esTransactionScope trans = new esTransactionScope())
            //{
            //    var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            //    feeColl.SetInvoicePayment(InvoicePaymentNo, AppSession.UserLogin.UserID);
            //    feeColl.Save();

            //    //Commit if success, Rollback if failed
            //    trans.Complete();
            //}

            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            feeColl.SetInvoicePayment(InvoicePaymentNo, AppSession.UserLogin.UserID);
            feeColl.Save();
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

        //            query.LeftJoin(itm).On(query.InvoiceNo == itm.InvoiceNo);
        //            query.LeftJoin(guar).On(query.GuarantorID == guar.GuarantorID);
        //            query.LeftJoin(sr).On(
        //                query.SRReceivableStatus == sr.ItemID &&
        //                sr.StandardReferenceID == AppEnum.StandardReference.ReceivableStatus
        //                );

        //            query.Select(
        //                   query.InvoiceNo,
        //                   query.InvoiceReferenceNo,
        //                   query.PaymentDate,
        //                   guar.GuarantorName,
        //                   query.IsApproved,
        //                   query.IsVoid,
        //                   sr.ItemName.As("ReceivableStatus"),
        //                   "<SUM(ISNULL(d.PaymentAmount, 0)) + (ISNULL(a.RoundingAmount, 0)) AS TotalAmount>",
        //                   query.SRPhysicianFeeProportionalStatus,
        //                   query.PhysicianFeeProportionalPercentage
        //               );

        //            query.Where(query.IsInvoicePayment == true, query.IsWriteOff == false, query.IsAddPayment.IsNull());
        //            query.GroupBy(query.InvoiceNo, query.InvoiceReferenceNo, query.PaymentDate, guar.GuarantorName,
        //                          query.IsApproved, query.IsVoid, sr.ItemName,
        //                          query.SRPhysicianFeeProportionalStatus,
        //                          query.PhysicianFeeProportionalPercentage,
        //                          query.RoundingAmount);
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
            public DateTime PaymentDate
            {
                get
                {
                    return this.Entity.PaymentDate.Value;
                }
            }
            public string InvoiceReferenceNo
            {
                get
                {
                    return this.Entity.InvoiceReferenceNo;
                }
            }
            public string GuarantorName
            {
                get
                {
                    return this.Entity.GuarantorName;
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
            public string SRPhysicianFeeProportionalStatus
            {
                get
                {
                    return this.Entity.SRPhysicianFeeProportionalStatus;
                }
            }
            public int PhysicianFeeProportionalPercentage
            {
                get
                {
                    return this.Entity.PhysicianFeeProportionalPercentage ?? 0;
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

        private UserAccess UserAccess
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["_UA"];
                    if (obj != null)
                        return ((UserAccess)(obj));
                }

                var UA = new UserAccess(AppSession.UserLogin.UserID, AppConstant.Program.ParamedicFeeVerification);


                Session["_UA"] = UA;
                return UA;
            }
            set { Session["_UA"] = value; }
        }

        public bool HasAccessToPhysicianFee()
        {
            return (UserAccess.IsExist);
        }
    }
}
