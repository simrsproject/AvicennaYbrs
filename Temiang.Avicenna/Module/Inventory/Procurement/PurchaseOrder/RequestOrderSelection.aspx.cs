using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Inventory.Procurement.PurchaseOrder
{
    public partial class RequestOrderSelection : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, TransactionCode.PurchaseRequest, false);
        }

        private DataTable PurchaseRequestPendings
        {
            get
            {
                string requestNo = Request.QueryString["pr"];
                string itemType = Request.QueryString["it"];
                string purchaseUnit = Request.QueryString["pu"];

                var query = new ItemTransactionQuery("a");
                var qryserviceunit = new ServiceUnitQuery("b");
                query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);

                var qrItem = new ItemTransactionItemQuery("d");
                query.InnerJoin(qrItem).On(query.TransactionNo == qrItem.TransactionNo);

                query.Select(
                    query.TransactionNo,
                    query.TransactionDate,
                    qryserviceunit.ServiceUnitName.As("FromServiceUnit"),
                    query.Notes,
                    (qrItem.Quantity * qrItem.ConversionFactor).Sum().As("QtyRequest")
                    );

                query.Where(
                    query.TransactionCode == TransactionCode.PurchaseRequest,
                    query.IsApproved == true,
                    qrItem.IsClosed == false,
                    query.ToServiceUnitID == purchaseUnit
                    );
                query.GroupBy(query.TransactionNo,
                              query.TransactionDate,
                              qryserviceunit.ServiceUnitName,
                              query.Notes);

                if (!string.IsNullOrEmpty(requestNo))
                    query.Where(query.TransactionNo == requestNo);
                if (!string.IsNullOrEmpty(itemType))
                    query.Where(query.SRItemType == itemType);
                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    query.Where(query.FromServiceUnitID == cboServiceUnitID.SelectedValue);
                if (!txtTransactionDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                    query.Where(query.TransactionDate == txtTransactionDate.SelectedDate);
                if (Request.QueryString["cons"] == "1")
                    query.Where(query.IsConsignment == true);
                if (AppSession.Parameter.IsUsingApprovalPurchaseRequest)
                    query.Where(qrItem.RequestQty.IsNotNull());
                
                query.OrderBy(query.TransactionNo.Ascending);
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                var dtb = query.LoadDataTable();

                if (AppSession.Parameter.IsPrOutstandingListBasedOnCalcQtyOrder)
                {
                    foreach (DataRow row in dtb.Rows)
                    {
                        var iti = new ItemTransactionItemQuery("a");
                        var it = new ItemTransactionQuery("b");
                        iti.InnerJoin(it).On(it.TransactionNo == iti.TransactionNo &&
                                             it.TransactionCode == TransactionCode.PurchaseOrder && it.IsVoid == false);
                        iti.Select(iti.ReferenceNo, (iti.Quantity * iti.ConversionFactor).Sum().As("QtyFinished"));
                        iti.Where(iti.ReferenceNo == row["TransactionNo"].ToString());
                        iti.GroupBy(iti.ReferenceNo);
                        DataTable dtbd = iti.LoadDataTable();
                        if (dtbd.Rows.Count > 0 && (Convert.ToDouble(row["QtyRequest"]) <= Convert.ToDouble(dtbd.Rows[0]["QtyFinished"])))
                            row.Delete();
                    }
                    dtb.AcceptChanges();
                }

                return dtb;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource = PurchaseRequestPendings;
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["Detail" + Request.UserHostName] != null)
                grdDetail.DataSource = ViewState["Detail" + Request.UserHostName];
        }

        protected void grdDetail_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateDataSourceDetail();
        }

        private void UpdateDataSourceDetail()
        {
            var dtb = (DataTable)ViewState["Detail" + Request.UserHostName];
            var tmp = dtb.Clone();
            tmp.Rows.Clear();

            foreach (GridDataItem dataItem in grdDetail.MasterTableView.Items)
            {
                if (!((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                    continue;

                var seqNo = dataItem.GetDataKeyValue("SequenceNo").ToString();
                var row = dtb.AsEnumerable().SingleOrDefault(r => r.Field<string>("SequenceNo").Equals(seqNo));
                if (row != null)
                {
                    row["QtyInput"] = ((RadNumericTextBox)dataItem.FindControl("txtQtyInput")).Value ?? 0;
                    tmp.ImportRow(row);
                }
            }

            ViewState["Detail" + Request.UserHostName] = tmp;
        }

        private void InitializeDataDetail(string transactionNo)
        {
            var parSuppId = Request.QueryString["su"];

            var suppItems = new SupplierItemCollection();
            suppItems.Query.Where(suppItems.Query.SupplierID != parSuppId);
            suppItems.LoadAll();

            var itemTransaction = new ItemTransaction();
            itemTransaction.LoadByPrimaryKey(transactionNo);
            string srItemType = itemTransaction.SRItemType;

            var query = new ItemTransactionItemQuery("a");

            var iq = new ItemQuery("b");
            query.LeftJoin(iq).On(query.ItemID == iq.ItemID);

            if (srItemType == ItemType.Medical)
            {
                var itemDetil = new ItemProductMedicQuery("c");
                query.LeftJoin(itemDetil).On(query.ItemID == itemDetil.ItemID);

                query.Select(
                        itemDetil.PriceInBaseUnit.Coalesce("'0'"),
                        itemDetil.PriceInPurchaseUnit.Coalesce("'0'"),
                        itemDetil.PurchaseDiscount1.Coalesce("'0'"),
                        itemDetil.PurchaseDiscount2.Coalesce("'0'"),
                        itemDetil.SRPurchaseUnit,
                        itemDetil.ConversionFactor.Coalesce("'1'").As("ItemConversionFactor")
                    );
            }
            else if (srItemType == ItemType.NonMedical)
            {
                var itemDetil = new ItemProductNonMedicQuery("c");
                query.LeftJoin(itemDetil).On(query.ItemID == itemDetil.ItemID);

                query.Select(
                        itemDetil.PriceInBaseUnit.Coalesce("'0'"),
                        itemDetil.PriceInPurchaseUnit.Coalesce("'0'"),
                        itemDetil.PurchaseDiscount1.Coalesce("'0'"),
                        itemDetil.PurchaseDiscount2.Coalesce("'0'"),
                        itemDetil.SRPurchaseUnit,
                        itemDetil.ConversionFactor.Coalesce("'1'").As("ItemConversionFactor")
                    );
            }
            else if (srItemType == ItemType.Kitchen)
            {
                var itemDetil = new ItemKitchenQuery("c");
                query.LeftJoin(itemDetil).On(query.ItemID == itemDetil.ItemID);

                query.Select(
                        itemDetil.PriceInBaseUnit.Coalesce("'0'"),
                        itemDetil.PriceInPurchaseUnit.Coalesce("'0'"),
                        itemDetil.PurchaseDiscount1.Coalesce("'0'"),
                        itemDetil.PurchaseDiscount2.Coalesce("'0'"),
                        itemDetil.SRPurchaseUnit,
                        itemDetil.ConversionFactor.Coalesce("'1'").As("ItemConversionFactor")
                    );
            }

            query.Where(
                query.TransactionNo == transactionNo,
                query.IsClosed == false
                );
            query.OrderBy(query.ItemID.Ascending);

            if (AppSession.Parameter.IsUsingApprovalPurchaseRequest)
                query.Where(query.RequestQty.IsNotNull());

            query.Select
                (
                    query.TransactionNo,
                    query.SequenceNo,
                    query.ItemID,
                    iq.ItemName,
                    query.SRItemUnit,
                    query.Quantity,
                    @"<CASE WHEN a.ConversionFactor = 0 THEN a.QuantityFinishInBaseUnit ELSE a.QuantityFinishInBaseUnit / a.ConversionFactor END AS QtyFinish>",
                    @"<CASE WHEN a.ConversionFactor = 0 THEN a.Quantity - a.QuantityFinishInBaseUnit ELSE a.Quantity - (a.QuantityFinishInBaseUnit / a.ConversionFactor) END AS QtyInput>",
                    @"<CASE WHEN a.ItemID = '' THEN a.Description ELSE b.ItemName END AS Description>",
                    string.Format("<{0} as SRItemType>", srItemType),
                    query.ConversionFactor,
                    query.Price,
                    query.PriceInCurrency,
                    @"<ISNULL((SELECT si.SupplierID FROM SupplierItem si 
                        WHERE si.ItemID = a.ItemID AND si.SupplierID = '" + parSuppId + @"'), '') AS SupplierID>",
                    @"<ISNULL((SELECT si.PurchaseDiscount1 FROM SupplierItem si 
                        WHERE si.ItemID = a.ItemID AND si.SupplierID = '" + parSuppId + @"'), 0) AS SuppDisc1>",
                    @"<ISNULL((SELECT si.PurchaseDiscount2 FROM SupplierItem si 
                        WHERE si.ItemID = a.ItemID AND si.SupplierID = '" + parSuppId + @"'), 0) AS SuppDisc2>",
                    @"<ISNULL((SELECT si.PriceInPurchaseUnit FROM SupplierItem si 
                        WHERE si.ItemID = a.ItemID AND si.SupplierID = '" + parSuppId + @"'), 0) AS SuppPriceInPurchaseUnit>",
                    @"<(a.Quantity*a.ConversionFactor) AS QtyRequest>",
                    query.Specification
                );

            var dtb = query.LoadDataTable();

            foreach (DataRow row in dtb.Rows)
            {
                if (!string.IsNullOrEmpty(parSuppId))
                {
                    if (row["SupplierID"].ToString() == string.Empty)
                    {
                        string itemId = row["ItemID"].ToString();
                        var x = suppItems.Where(i => i.ItemID == itemId);
                        if (x.Any())
                        {
                            row.Delete();
                            continue;
                        }
                        else
                        {
                            row["Price"] = row["SRItemUnit"].ToString() == row["SRPurchaseUnit"].ToString()
                                               ? Convert.ToDecimal(row["PriceInPurchaseUnit"])
                                               : (Convert.ToDecimal(row["PriceInPurchaseUnit"]) /
                                                  Convert.ToDecimal(row["ItemConversionFactor"])) *
                                                 Convert.ToDecimal(row["ConversionFactor"]);
                            row["PriceInCurrency"] = row["Price"];
                        }
                    }
                    else
                    {
                        row["Price"] = row["SRItemUnit"].ToString() == row["SRPurchaseUnit"].ToString()
                                           ? Convert.ToDecimal(row["SuppPriceInPurchaseUnit"])
                                           : (Convert.ToDecimal(row["SuppPriceInPurchaseUnit"]) /
                                              Convert.ToDecimal(row["ItemConversionFactor"])) *
                                             Convert.ToDecimal(row["ConversionFactor"]);
                        row["PurchaseDiscount1"] = Convert.ToDecimal(row["SuppDisc1"]);
                        row["PurchaseDiscount2"] = Convert.ToDecimal(row["SuppDisc2"]);
                        row["PriceInCurrency"] = row["Price"];
                    }
                }
                else
                {
                    row["Price"] = row["SRItemUnit"].ToString() == row["SRPurchaseUnit"].ToString()
                                           ? Convert.ToDecimal(row["PriceInPurchaseUnit"])
                                           : (Convert.ToDecimal(row["PriceInPurchaseUnit"]) /
                                              Convert.ToDecimal(row["ItemConversionFactor"])) *
                                             Convert.ToDecimal(row["ConversionFactor"]);
                    row["PriceInCurrency"] = row["Price"];
                }

                if (AppSession.Parameter.IsPrOutstandingListBasedOnCalcQtyOrder)
                {
                    var iti = new ItemTransactionItemQuery("a");
                    var it = new ItemTransactionQuery("b");
                    iti.InnerJoin(it).On(it.TransactionNo == iti.TransactionNo &&
                                         it.TransactionCode == TransactionCode.PurchaseOrder && it.IsVoid == false);
                    iti.Select(iti.ReferenceNo, iti.ReferenceSequenceNo, (iti.Quantity * iti.ConversionFactor).Sum().As("QtyFinished"));
                    iti.Where(iti.ReferenceNo == row["TransactionNo"].ToString(), iti.ReferenceSequenceNo == row["SequenceNo"].ToString());
                    iti.GroupBy(iti.ReferenceNo, iti.ReferenceSequenceNo);
                    DataTable dtbd = iti.LoadDataTable();
                    if (dtbd.Rows.Count > 0)
                    {
                        if (Convert.ToDouble(row["QtyRequest"]) <= Convert.ToDouble(dtbd.Rows[0]["QtyFinished"]))
                            row.Delete();
                        else
                        {
                            row["QtyInput"] = (Convert.ToDouble(row["QtyRequest"]) -
                                               Convert.ToDouble(dtbd.Rows[0]["QtyFinished"])) /
                                              Convert.ToDouble(row["ConversionFactor"]);
                        }
                    }
                }
            }

            dtb.AcceptChanges();

            ViewState["Detail" + Request.UserHostName] = dtb;
            grdDetail.DataSource = dtb;
            grdDetail.DataBind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);
            if (!(source is RadGrid))
                return;

            var grd = (RadGrid)source;
            switch (grd.ID.ToLower())
            {
                case "grddetail": // Populate Detail
                    string[] pars = eventArgument.Split('|');
                    InitializeDataDetail(pars[0].Split(':')[1]);
                    break;
            }
        }

        public override bool OnButtonOkClicked()
        {
            UpdateDataSourceDetail();
            Session["ROItemSelected" + Request.UserHostName] = ViewState["Detail" + Request.UserHostName];
            return true;
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            var selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in grdDetail.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
            grdDetail.Rebind();
        }
    }
}