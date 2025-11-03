using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class PurchaseOrderReturnPickList : BasePageDialog
    {
        private DataTable PurchaseOrders
        {
            get
            {
                string itemType = Request.QueryString["it"];
                string supplierID = Request.QueryString["supid"];
                string serviceUnitID = Request.QueryString["suid"];

                var query = new ItemTransactionQuery("a");
                var qryserviceunit = new ServiceUnitQuery("b");
                query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.ToServiceUnitID);

                var qrItem = new ItemTransactionItemQuery("d");
                query.InnerJoin(qrItem).On(query.TransactionNo == qrItem.TransactionNo);

                var supp = new SupplierQuery("supp");
                query.InnerJoin(supp).On(query.BusinessPartnerID == supp.SupplierID);

                var po = new ItemTransactionQuery("po");
                query.InnerJoin(po).On(po.TransactionNo == query.ReferenceNo);

                query.Select(
                    query.TransactionNo,
                    query.ReferenceNo,
                    query.InvoiceNo,
                    query.TransactionDate,
                    query.ToServiceUnitID,
                    qryserviceunit.ServiceUnitName,
                    query.Notes,
                    supp.SupplierName
                    );

                query.Where(
                    query.TransactionCode == TransactionCode.PurchaseOrderReceive,
                    //query.BusinessPartnerID == supplierID,
                    query.SRItemType == itemType,
                    query.Or(query.ToServiceUnitID == serviceUnitID, po.FromServiceUnitID == serviceUnitID),
                    query.IsApproved == true
                    );

                if (Request.QueryString["cons"] == "1")
                    query.Where(query.IsConsignment == true);
                else
                    query.Where(query.IsConsignment == false);

                if (!string.IsNullOrEmpty(supplierID))
                    query.Where(query.BusinessPartnerID == supplierID);
                if (!string.IsNullOrEmpty(cboItemID.SelectedValue))
                    query.Where(qrItem.ItemID == cboItemID.SelectedValue);
                if (!string.IsNullOrEmpty(txtReceivedNo.Text))
                    query.Where(query.TransactionNo == txtReceivedNo.Text);
                if (!string.IsNullOrEmpty(txtInvoiceNo.Text))
                    query.Where(query.InvoiceNo == txtInvoiceNo.Text);
                if (!string.IsNullOrEmpty(txtBatchNo.Text) || !txtExpiredDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                {
                    if (!AppSession.Parameter.IsTxUsingEdDetail)
                    {
                        if (!string.IsNullOrEmpty(txtBatchNo.Text))
                            query.Where(qrItem.BatchNumber == txtBatchNo.Text);
                        if (!txtExpiredDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                            query.Where(qrItem.ExpiredDate == txtExpiredDate.SelectedDate);
                    }
                    else
                    {
                        var ed = new ItemTransactionItemEdQuery("ed");
                        query.InnerJoin(ed).On(query.TransactionNo == ed.TransactionNo &&
                                               qrItem.SequenceNo == ed.SequenceNo);
                        if (!string.IsNullOrEmpty(txtBatchNo.Text))
                            query.Where(ed.BatchNumber == txtBatchNo.Text);
                        if (!txtExpiredDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                            query.Where(ed.ExpiredDate == txtExpiredDate.SelectedDate);
                    }
                }
                

                query.es.Distinct = true;
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (AppSession.Parameter.IsPurcReturnWithPrice)
            {
                if (AppSession.Parameter.IsPurcReturnCanChangePrice)
                {
                    grdDetail.Columns[7].Visible = true; // txtprice
                    grdDetail.Columns[9].Visible = true; // discinpercent txt
                    grdDetail.Columns[11].Visible = true; // disc1 txt
                    grdDetail.Columns[13].Visible = true; // disc2 txt
                    grdDetail.Columns[15].Visible = true; // discamt txt
                }
                else
                {
                    grdDetail.Columns[6].Visible = true; // lblprice
                    grdDetail.Columns[8].Visible = true; // discinpercent lbl
                    grdDetail.Columns[10].Visible = true; // disc1 lbl
                    grdDetail.Columns[12].Visible = true; // disc2 lbl
                    grdDetail.Columns[14].Visible = true; // discamt lbl
                }
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource = PurchaseOrders;
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["POReturn:Detail" + Request.UserHostName + Request.QueryString["pageId"]] != null)
                grdDetail.DataSource = ViewState["POReturn:Detail" + Request.UserHostName + Request.QueryString["pageId"]];
        }
        
        protected void grdDetail_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateDataSourceDetail();
        }

        private void UpdateDataSourceDetail()
        {
            var dtb = (DataTable)ViewState["POReturn:Detail" + Request.UserHostName + Request.QueryString["pageId"]];
            foreach (GridDataItem dataItem in grdDetail.MasterTableView.Items)
            {
                string seqNo = dataItem.GetDataKeyValue("SequenceNo").ToString();
                string itemId = dataItem.GetDataKeyValue("ItemID").ToString();
                string batchNo = dataItem.GetDataKeyValue("BatchNumber").ToString();
                double qty = ((RadNumericTextBox)dataItem.FindControl("txtQtyInput")).Value ?? 0;
                double price = ((RadNumericTextBox)dataItem.FindControl("txtPrice")).Value ?? 0;
                double disc1 = ((RadNumericTextBox)dataItem.FindControl("txtDisc1")).Value ?? 0;
                double disc2 = ((RadNumericTextBox)dataItem.FindControl("txtDisc2")).Value ?? 0;
                double discAmt = ((RadNumericTextBox)dataItem.FindControl("txtDiscAmt")).Value ?? 0;
                bool isDiscInPercent = ((CheckBox)dataItem.FindControl("chkIsDiscInPercent")).Checked;

                foreach (DataRow row in dtb.Rows)
                {
                    if (row["SequenceNo"].Equals(seqNo) && row["ItemID"].Equals(itemId) && row["BatchNumber"].Equals(batchNo))
                    {
                        row["QtyInput"] = qty;
                        row["QtyPending"] = qty;
                        row["Price"] = price;
                        row["IsDiscountInPercent"] = isDiscInPercent;
                        if (isDiscInPercent)
                        {
                            row["Discount1Percentage"] = disc1;
                            row["Discount2Percentage"] = disc2;
                            row["Discount"] = discAmt;//(price * disc1 / 100) + ((price - (price * disc1 / 100)) * disc2 / 100);
                        }
                        else
                        {
                            row["Discount1Percentage"] = 0;
                            row["Discount2Percentage"] = 0;
                            row["Discount"] = discAmt;
                        }

                        break;
                    }
                }

                ViewState["POReturn:Detail" + Request.UserHostName + Request.QueryString["pageId"]] = dtb;
            }
        }

        private void InitializeDataDetail(string transactionNo)
        {
            DataTable dtb;
            using (new esTransactionScope())
            {
                var itemTransaction = new ItemTransaction();
                itemTransaction.LoadByPrimaryKey(transactionNo);

                if (!AppSession.Parameter.IsTxUsingEdDetail)
                {
                    var query = new ItemTransactionItemQuery("a");
                    var iq = new ItemQuery("b");
                    if (itemTransaction.SRItemType == ItemType.Medical)
                    {
                        var itemProductMedic = new ItemProductMedicQuery("p");
                        query.LeftJoin(itemProductMedic).On(query.ItemID == itemProductMedic.ItemID);
                    }
                    else if (itemTransaction.SRItemType == ItemType.NonMedical)
                    {
                        var itemNonProductMedic = new ItemProductNonMedicQuery("p");
                        query.LeftJoin(itemNonProductMedic).On(query.ItemID == itemNonProductMedic.ItemID);
                    }
                    else if (itemTransaction.SRItemType == ItemType.Kitchen)
                    {
                        var itemKitchen = new ItemKitchenQuery("p");
                        query.LeftJoin(itemKitchen).On(query.ItemID == itemKitchen.ItemID);
                    }

                    query.LeftJoin(iq).On(query.ItemID == iq.ItemID);
                    query.Where(query.TransactionNo == transactionNo, query.IsClosed == false); //, query.IsBonusItem == false);

                    if (!string.IsNullOrEmpty(txtBatchNo.Text))
                        query.Where(query.BatchNumber == txtBatchNo.Text);
                    if (!txtExpiredDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                        query.Where(query.ExpiredDate == txtExpiredDate.SelectedDate);

                    query.OrderBy(query.ItemID.Ascending);

                    query.Select(
                        query.TransactionNo,
                        query.SequenceNo,
                        string.Format("<'{0}' as ToServiceUnitID>", itemTransaction.ToServiceUnitID),
                        query.ItemID,
                        "<CASE WHEN a.SRItemUnit = '' OR a.SRItemUnit IS NULL THEN CASE WHEN a.ConversionFactor > 1 THEN p.SRPurchaseUnit ELSE p.SRItemUnit END ELSE a.SRItemUnit END AS SRItemUnit>",
                        query.Quantity,
                        "<CASE WHEN a.ConversionFactor = 0 THEN a.QuantityFinishInBaseUnit ELSE (a.QuantityFinishInBaseUnit / a.ConversionFactor) END  AS QuantityFinishInBaseUnit>",
                        "<CASE WHEN a.ConversionFactor=0 THEN (a.Quantity - a.QuantityFinishInBaseUnit) ELSE (a.Quantity - (a.QuantityFinishInBaseUnit / a.ConversionFactor)) END AS QtyInput>",
                        "<CASE WHEN a.ConversionFactor=0 THEN (a.Quantity - a.QuantityFinishInBaseUnit) ELSE (a.Quantity - (a.QuantityFinishInBaseUnit / a.ConversionFactor)) END AS QtyPending>",
                        query.ConversionFactor,
                        query.Description,
                        string.Format("<'{0}' as SRItemType>", itemTransaction.SRItemType),
                        query.Price,
                        query.Discount1Percentage,
                        query.Discount2Percentage,
                        query.IsDiscountInPercent,
                        query.Discount,
                        query.PriceInCurrency,
                        query.DiscountInCurrency,
                        query.BatchNumber.Coalesce("''"),
                        query.ExpiredDate,
                        @"<ISNULL(p.IsControlExpired, 0) AS IsControlExpired>"
                        );
                    dtb = query.LoadDataTable();
                }
                else
                {
                    var query = new ItemTransactionItemQuery("a");
                    var iq = new ItemQuery("b");
                    var edq = new ItemTransactionItemEdQuery("c");
                    if (itemTransaction.SRItemType == ItemType.Medical)
                    {
                        var itemProductMedic = new ItemProductMedicQuery("p");
                        query.LeftJoin(itemProductMedic).On(query.ItemID == itemProductMedic.ItemID);
                    }
                    else if (itemTransaction.SRItemType == ItemType.NonMedical)
                    {
                        var itemNonProductMedic = new ItemProductNonMedicQuery("p");
                        query.LeftJoin(itemNonProductMedic).On(query.ItemID == itemNonProductMedic.ItemID);
                    }
                    else if (itemTransaction.SRItemType == ItemType.Kitchen)
                    {
                        var itemKitchen = new ItemKitchenQuery("p");
                        query.LeftJoin(itemKitchen).On(query.ItemID == itemKitchen.ItemID);
                    }

                    query.LeftJoin(iq).On(query.ItemID == iq.ItemID);
                    query.LeftJoin(edq).On(query.TransactionNo == edq.TransactionNo &&
                                           query.SequenceNo == edq.SequenceNo);
                    query.Where(query.TransactionNo == transactionNo, query.IsClosed == false);//, query.IsBonusItem == false);
                    if (!string.IsNullOrEmpty(txtBatchNo.Text))
                        query.Where(edq.BatchNumber == txtBatchNo.Text);
                    if (!txtExpiredDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                        query.Where(edq.ExpiredDate == txtExpiredDate.SelectedDate);

                    query.OrderBy(query.ItemID.Ascending);

                    query.Select(
                        query.TransactionNo,
                        query.SequenceNo,
                        string.Format("<'{0}' as ToServiceUnitID>", itemTransaction.ToServiceUnitID),
                        query.ItemID,
                        "<CASE WHEN ISNULL(c.SRItemUnit, a.SRItemUnit) = '' THEN CASE WHEN ISNULL(c.ConversionFactor, a.ConversionFactor) > 1 THEN p.SRPurchaseUnit ELSE p.SRItemUnit END ELSE ISNULL(c.SRItemUnit, a.SRItemUnit) END AS SRItemUnit>",
                        query.Quantity,
                        @"<CASE WHEN ISNULL(c.ConversionFactor, a.ConversionFactor) = 0 THEN ISNULL(c.QuantityFinishInBaseUnit, a.QuantityFinishInBaseUnit) 
		ELSE (ISNULL(c.QuantityFinishInBaseUnit, a.QuantityFinishInBaseUnit) / ISNULL(c.ConversionFactor, a.ConversionFactor)) END  AS QuantityFinishInBaseUnit>",
                        @"<CASE WHEN ISNULL(c.ConversionFactor, a.ConversionFactor) = 0 THEN (ISNULL(c.Quantity, a.Quantity) - ISNULL(c.QuantityFinishInBaseUnit, a.QuantityFinishInBaseUnit)) 
		ELSE (ISNULL(c.Quantity, a.Quantity) - (ISNULL(c.QuantityFinishInBaseUnit, a.QuantityFinishInBaseUnit) / ISNULL(c.ConversionFactor, a.ConversionFactor))) END AS QtyInput>",
                        @"<CASE WHEN ISNULL(c.ConversionFactor, a.ConversionFactor) = 0 THEN (ISNULL(c.Quantity, a.Quantity) - ISNULL(c.QuantityFinishInBaseUnit, a.QuantityFinishInBaseUnit)) 
		ELSE (ISNULL(c.Quantity, a.Quantity) - (ISNULL(c.QuantityFinishInBaseUnit, a.QuantityFinishInBaseUnit) / ISNULL(c.ConversionFactor, a.ConversionFactor))) END AS QtyPending>",
                        "<ISNULL(c.ConversionFactor, a.ConversionFactor) AS ConversionFactor>",
                        query.Description,
                        string.Format("<'{0}' as SRItemType>", itemTransaction.SRItemType),
                        query.Price,
                        query.Discount1Percentage,
                        query.Discount2Percentage,
                        query.IsDiscountInPercent,
                        query.Discount,
                        query.PriceInCurrency,
                        query.DiscountInCurrency,
                        edq.BatchNumber.Coalesce("''"),
                        edq.ExpiredDate,
                        @"<ISNULL(p.IsControlExpired, 0) AS IsControlExpired>"
                        );
                    dtb = query.LoadDataTable(); 
                }
            }
            ViewState["POReturn:Detail" + Request.UserHostName + Request.QueryString["pageId"]] = dtb;
            grdDetail.DataSource = dtb;
            grdDetail.DataBind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);
            if (!(source is RadGrid)) return;
            RadGrid grd = (RadGrid)source;
            switch (grd.ID.ToLower())
            {
                case "grddetail": // Populate Detail
                    string[] pars = eventArgument.Split('|');
                    string transNo = pars[0].Split(':')[1];
                    InitializeDataDetail(transNo);
                    break;
            }
        }

        public override bool OnButtonOkClicked()
        {
            UpdateDataSourceDetail();

            //Check data
            DataTable dtb = (DataTable)ViewState["POReturn:Detail" + Request.UserHostName + Request.QueryString["pageId"]];
            using (new esTransactionScope())
            {
                foreach (DataRow row in dtb.Rows)
                {
                    if (Convert.ToDouble(row["QtyInput"]) > Convert.ToDouble(row["QtyPending"]))
                    {
                        ShowMessage(string.Format("Return Qty for item {0} can not be greather than {1}", row["Description"], Convert.ToDouble(row["QtyPending"]).ToString()));
                        return false;
                    }
                }
            }

            Session["POReturn:ItemSelected" + Request.UserHostName + Request.QueryString["pageId"]] = ViewState["POReturn:Detail" + Request.UserHostName + Request.QueryString["pageId"]];
            return true;
        }

        #region Search Item
        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();

            grdDetail.DataSource = null;
            grdDetail.DataBind();
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var tbl = PopulateItem(e.Text);
            cboItemID.DataSource = tbl.Rows.Count == 0 ? PopulateItem(e.Text) : tbl;
            cboItemID.DataBind();
        }

        private DataTable PopulateItem(string parameter)
        {
            //var query = new ItemQuery("a");
            //var itiq = new ItemTransactionItemQuery("b");
            //var itq = new ItemTransactionQuery("c");

            //query.InnerJoin(itiq).On(query.ItemID == itiq.ItemID);
            //query.InnerJoin(itq).On(itiq.TransactionNo == itq.TransactionNo &&
            //                        itq.TransactionCode == TransactionCode.PurchaseOrderReceive &&
            //                        itq.BusinessPartnerID == Request.QueryString["supid"]);

            //query.es.Top = 30;
            //query.es.Distinct = true;
            //query.Select
            //    (
            //        query.ItemID,
            //        query.ItemName
            //    );

            //query.Where
            //    (
            //        query.Or
            //            (
            //                query.ItemName.Like(string.Format("%.{0}%", parameter)),
            //                query.ItemID.Like(string.Format("%.{0}%", parameter))
            //            )
            //    );
            //query.OrderBy(query.ItemName.Ascending);

            string searchTextContain = string.Format("%{0}%", parameter);

            var query = new ItemQuery("a");
            query.Where(query.SRItemType == Request.QueryString["it"], query.IsActive == true);
            
            query.es.Top = 30;
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );

            query.Where
                (
                    query.Or
                        (
                            query.ItemName.Like(searchTextContain),
                            query.ItemID.Like(searchTextContain)
                        )
                );
            query.OrderBy(query.ItemName.Ascending);

            var tbl = query.LoadDataTable();

            return tbl;
        }

        #endregion
    }
}