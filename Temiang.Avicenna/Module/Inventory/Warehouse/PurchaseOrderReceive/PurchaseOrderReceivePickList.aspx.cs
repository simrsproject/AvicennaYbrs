using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class PurchaseOrderReceivePickList : BasePageDialog
    {
        bool _isTxUsingEdDetail;
        protected void Page_Init(object sender, EventArgs e)
        {
            _isTxUsingEdDetail = AppSession.Parameter.IsTxUsingEdDetail;
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["unitId"]) && AppSession.Parameter.IsPoAndPorInTheSameUnit)
                {
                    var q = new ServiceUnitQuery();
                    q.Where(q.ServiceUnitID == Request.QueryString["unitId"]);
                    cboServiceUnitName.DataSource = q.LoadDataTable();
                    cboServiceUnitName.DataBind();

                    cboServiceUnitName.SelectedValue = Request.QueryString["unitId"];
                    cboServiceUnitName.Enabled = false;
                }

                //txtOrderDate.SelectedDate = DateTime.Today.AddMonths(-1).AddDays(-1);
                //txtOrderDateTo.SelectedDate = DateTime.Today;
            }
        }

        private DataTable PurchaseOrderPendings
        {
            get
            {
                var query = new ItemTransactionQuery("a");
                var qryserviceunit = new ServiceUnitQuery("b");
                var qrItem = new ItemTransactionItemQuery("d");
                var qrSup = new SupplierQuery("s");
                var qrref = new ItemTransactionQuery("ref");
                var qrsuref = new ServiceUnitQuery("suref");
                var qrStandr = new AppStandardReferenceItemQuery("e");
                var sureq = new ServiceUnitQuery("f"); //tambah pemanggilan serviceunit - Apip 20240508

                query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
                query.InnerJoin(qrItem).On(query.TransactionNo == qrItem.TransactionNo);
                query.InnerJoin(qrSup).On(query.BusinessPartnerID == qrSup.SupplierID);
                query.LeftJoin(qrref).On(query.ReferenceNo == qrref.TransactionNo &&
                                         qrref.TransactionCode == TransactionCode.PurchaseRequest);
                query.LeftJoin(qrsuref).On(qrref.FromServiceUnitID == qrsuref.ServiceUnitID);
                query.LeftJoin(qrStandr).On
                                (
                                    query.SRPurchaseCategorization == qrStandr.ItemID &
                                    qrStandr.StandardReferenceID == "PurchaseCategorization"
                                );
                query.LeftJoin(sureq).On(query.ToServiceUnitID == sureq.ServiceUnitID);

                query.Select(
                    query.TransactionNo,
                    query.TransactionDate,
                    qryserviceunit.ServiceUnitName.As("FromServiceUnit"),
                    qrSup.SupplierName,
                    query.SRItemType,
                    query.IsInventoryItem,
                    query.Notes,
                @"<ISNULL(e.ItemName, '') AS InventoryCategory>",
                    //qrStandr.ItemName.As("InventoryCategory"),
                    query.IsConsignment,
                    query.ReferenceNo,
                @"<ISNULL(suref.ServiceUnitName, f.ServiceUnitName) AS ServiceUnitRequest>"
                    //qrsuref.ServiceUnitName.As("ServiceUnitRequest")
                    );

                query.Where
                    (
                    query.TransactionCode == TransactionCode.PurchaseOrder,
                    query.IsApproved == true,
                    qrItem.IsClosed == false
                    );
                if (rblTypesOfTaxes.SelectedIndex == 0)
                    query.Where(query.IsTaxable == 1);
                else if (rblTypesOfTaxes.SelectedIndex == 1)
                    query.Where(query.IsTaxable == 0);
                else 
                    query.Where(query.IsTaxable == 2);

                bool isFilter = false;

                if (!txtOrderDate.IsEmpty)
                {
                    query.Where(query.TransactionDate == txtOrderDate.SelectedDate);
                    isFilter = true;
                }
                if (!string.IsNullOrEmpty(cboSupplierName.SelectedValue))
                {
                    query.Where(query.BusinessPartnerID == cboSupplierName.SelectedValue);
                    isFilter = true;
                }
                if (!string.IsNullOrEmpty(cboServiceUnitName.SelectedValue))
                    query.Where(query.FromServiceUnitID == cboServiceUnitName.SelectedValue);
                if (!string.IsNullOrEmpty(txtOrderNo.Text))
                {
                    query.Where(query.TransactionNo == txtOrderNo.Text);
                    isFilter = true;
                }

                if (!isFilter)
                {
                    query.Where(query.TransactionDate >= DateTime.Today.AddMonths(-1).Date, query.TransactionDate <= DateTime.Today.Date);
                }
                    
                if (!string.IsNullOrEmpty(cboServiceUnitReqName.SelectedValue))
                    query.Where(qrref.FromServiceUnitID == cboServiceUnitReqName.SelectedValue);
                if (Request.QueryString["cons"] == "1")
                    query.Where(query.IsConsignment == true);
                else if (Request.QueryString["cons"] == "0" && AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB")
                    query.Where(query.IsConsignment == false);

                if (!string.IsNullOrEmpty(Request.QueryString["unitId"]))
                {
                    var q = new ServiceUnitQuery();
                    q.Where(q.ServiceUnitPorID == Request.QueryString["unitId"]);
                    DataTable qdtb = q.LoadDataTable();
                    if (qdtb.Rows.Count > 0)
                        query.Where(qryserviceunit.ServiceUnitPorID == Request.QueryString["unitId"]);
                }

                bool itemMedic = false, itemNonMedic = false, itemKitchen = false;
                var it = new ServiceUnitTransactionCode();
                if (it.LoadByPrimaryKey(Request.QueryString["unitId"], TransactionCode.PurchaseOrderReceive))
                {
                    itemMedic = it.IsItemProductMedic ?? false;
                    itemNonMedic = it.IsItemProductNonMedic ?? false;
                    itemKitchen = it.IsItemKitchen ?? false;
                }

                var xx = new List<Temiang.Dal.DynamicQuery.esComparison>();

                if (itemMedic)
                    xx.Add(query.SRItemType == ItemType.Medical);

                if (itemNonMedic)
                    xx.Add(query.SRItemType == ItemType.NonMedical);

                if (itemKitchen)
                    xx.Add(query.SRItemType == ItemType.Kitchen);

                if (xx.Count > 0)
                    query.Where(query.Or(xx.ToArray()));

                query.OrderBy(query.TransactionNo.Descending);
                query.es.Distinct = true;
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();

                //foreach (DataRow row in dtb.Rows)
                //{
                //    switch (row["SRItemType"].ToString())
                //    {
                //        case ItemType.Medical:
                //            if (itemMedic == false)
                //                row.Delete();
                //            break;
                //        case ItemType.NonMedical:
                //            if (itemNonMedic == false)
                //                row.Delete();
                //            break;
                //        case ItemType.Kitchen:
                //            if (itemKitchen == false)
                //                row.Delete();
                //            break;
                //    }
                //}
                //dtb.AcceptChanges();

                return dtb;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource = PurchaseOrderPendings;
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["POR_W3TOT:Detail" + Request.UserHostName] != null)
                grdDetail.DataSource = ViewState["POR_W3TOT:Detail" + Request.UserHostName];
        }

        protected void grdDetail_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateDataSourceDetail();
        }

        private void UpdateDataSourceDetail()
        {
            var isNeedEdControlOnPor = AppSession.Parameter.IsNeedEdControlOnPOR;
            var isNeedPriceControlOnPor = AppSession.Parameter.IsNeedPriceControlOnPOR;
            var isNeedQtyControlOnPor = AppSession.Parameter.IsNeedQtyControlOnPOR;
            var theMinimumTimeLimitEdControlOnPor = AppSession.Parameter.TheMinimumTimeLimitEdControlOnPOR;

            var dtb = (DataTable)ViewState["POR_W3TOT:Detail" + Request.UserHostName];
            foreach (GridDataItem dataItem in grdDetail.MasterTableView.Items)
            {
                string seqNo = dataItem.GetDataKeyValue("SequenceNo").ToString();
                double price = ((RadNumericTextBox)dataItem.FindControl("txtPrice")).Value ?? 0;
                double disc1 = ((RadNumericTextBox)dataItem.FindControl("txtDisc1")).Value ?? 0;
                double disc2 = ((RadNumericTextBox)dataItem.FindControl("txtDisc2")).Value ?? 0;
                double discAmt = ((RadNumericTextBox)dataItem.FindControl("txtDiscAmt")).Value ?? 0;
                double qty = ((RadNumericTextBox)dataItem.FindControl("txtQtyRecv")).Value ?? 0;
                string batchNumber = ((RadTextBox)dataItem.FindControl("txtBatchNumber")).Text ?? string.Empty;
                DateTime? expiredDate = ((RadDatePicker)dataItem.FindControl("txtExpiredDate")).SelectedDate;
                bool isDiscInPercent = ((CheckBox)dataItem.FindControl("chkIsDiscInPercent")).Checked;
                bool isTaxable = ((CheckBox)dataItem.FindControl("chkIsTaxable")).Checked;

                bool isSelect = AppSession.Parameter.IsPorUsingChecklistItem ? ((CheckBox)dataItem.FindControl("chkIsSelect")).Checked : true;

                double qtyPending = ((RadNumericTextBox)dataItem.FindControl("txtQtyPending")).Value ?? 0;
                double priceOri = ((RadNumericTextBox)dataItem.FindControl("txtPriceOri")).Value ?? 0;

                bool isConsignment = ((CheckBox)dataItem.FindControl("chkIsConsignment")).Checked;
                if (isConsignment)
                {
                    qty = ((RadNumericTextBox)dataItem.FindControl("txtQtyRecv2")).Value ?? 0;
                }

                foreach (DataRow row in dtb.Rows)
                {
                    if (row["SequenceNo"].Equals(seqNo))
                    {
                        row["Price"] = price;
                        row["IsDiscountInPercent"] = isDiscInPercent;
                        if (isDiscInPercent)
                        {
                            row["Discount1Percentage"] = disc1;
                            row["Discount2Percentage"] = disc2;
                            var disc = Math.Round((price * disc1 / 100),2);
                            row["Discount"] = disc + Math.Round(((price - disc) * disc2 / 100),2);
                            //row["Discount"] = (price * disc1 / 100) + ((price - (price * disc1 / 100)) * disc2 / 100);
                        }
                        else
                        {
                            row["Discount1Percentage"] = 0;
                            row["Discount2Percentage"] = 0;
                            row["Discount"] = discAmt;
                        }

                        row["QtyRecv"] = qty;
                        row["BatchNumber"] = batchNumber;
                        if (expiredDate != null)
                        {
                            row["ExpiredDate"] = expiredDate;
                            if (isNeedEdControlOnPor && theMinimumTimeLimitEdControlOnPor != "0" && (new DateTime()).NowAtSqlServer().Date.AddMonths(theMinimumTimeLimitEdControlOnPor.ToInt()) > expiredDate.Value && _isTxUsingEdDetail == false)
                            {
                                row["IsAccEd"] = false;
                            }
                        }
                        if (isNeedQtyControlOnPor && qty > qtyPending)
                            row["IsAccQty"] = false;
                        if (isNeedPriceControlOnPor && price != priceOri)
                            row["IsAccPrice"] = false;
                        row["IsTaxable"] = isTaxable;
                        row["IsSelect"] = isSelect;
                     
                        break;
                    }
                }

                ViewState["POR_W3TOT:Detail" + Request.UserHostName] = dtb;
            }
        }

        private void InitializeDataDetail(string transactionNo)
        {
            DataTable dtbTemp;
            
            var itemTransaction = new ItemTransaction();
            itemTransaction.LoadByPrimaryKey(transactionNo);
            
            string srItemType = itemTransaction.SRItemType;
            decimal taxPercentage = itemTransaction.TaxPercentage ?? 0;
            string contractNo = itemTransaction.ContractNo;

            var query = new ItemTransactionItemQuery("a");
            var iq = new ItemQuery("b");
            
            query.LeftJoin(iq).On(query.ItemID == iq.ItemID);
            if (itemTransaction.SRItemType == ItemType.Medical)
            {
                var itemDetil = new ItemProductMedicQuery("c");
                query.LeftJoin(itemDetil).On(query.ItemID == itemDetil.ItemID);
            }
            else if (itemTransaction.SRItemType == ItemType.NonMedical)
            {
                var itemDetil = new ItemProductNonMedicQuery("c");
                query.LeftJoin(itemDetil).On(query.ItemID == itemDetil.ItemID);
            }
            else if (itemTransaction.SRItemType == ItemType.Kitchen)
            {
                var itemDetil = new ItemKitchenQuery("c");
                query.LeftJoin(itemDetil).On(query.ItemID == itemDetil.ItemID);
            }

            var fq = new FabricQuery("f");
            query.LeftJoin(fq).On(fq.FabricID == query.FabricID);

            query.Where(query.TransactionNo == transactionNo, query.IsClosed == false);
            query.OrderBy(query.ItemID.Ascending);

            query.Select
                (
                query.TransactionNo,
                query.SequenceNo,
                query.ItemID,
                query.SRItemUnit.As("PurchaseUnitID"),
                query.Quantity.As("QtyPO"),
                query.QuantityFinishInBaseUnit,
                ((query.Quantity * query.ConversionFactor) - query.QuantityFinishInBaseUnit).As("QtyPending"),
                @"<(ISNULL((SELECT SUM(iti.Quantity * iti.ConversionFactor) 
FROM ItemTransactionItem AS iti 
INNER JOIN ItemTransaction AS it ON it.TransactionNo = iti.TransactionNo
WHERE it.TransactionCode = '040' AND iti.ReferenceNo = a.TransactionNo AND iti.ReferenceSequenceNo = a.SequenceNo AND it.IsVoid = 0 AND it.IsApproved = 0), 0)) AS 'QtyPendingX'>",
                query.BatchNumber,
                query.ExpiredDate,
                query.Price.As("PricePurchase"),
                query.PriceInCurrency,
                query.Discount1Percentage,
                query.Discount2Percentage,
                query.Discount,
                query.IsDiscountInPercent,
                query.ConversionFactor,
                query.Description,
                query.FabricID,
                fq.FabricName,
                "<c.SRItemUnit as BaseUnitID>",
                query.IsBonusItem,
                @"<CAST(1 AS BIT) AS IsAccEd>",
                @"<CAST(1 AS BIT) AS IsAccPrice>",
                @"<CAST(1 AS BIT) AS IsAccQty>",
                @"<ISNULL(c.IsControlExpired, 0) AS IsControlExpired>",
                @"<ISNULL(a.IsTaxable, 1) AS IsTaxable>",
                @"<CAST(0 AS BIT) AS IsNotCompleteED>",
                @"<CAST(1 AS BIT) AS IsSelect>"
                );
            dtbTemp = query.LoadDataTable();

            // Loop, bila sisa dikonversi ke PurchaseUnit tidak ada pecahan maka pakai unit Purchase bila ada
            // maka pakai base unit
            var dtb = new DataTable();
            dtb.Columns.Add("TransactionNo", Type.GetType("System.String"));
            dtb.Columns.Add("SequenceNo", Type.GetType("System.String"));
            dtb.Columns.Add("ItemID", Type.GetType("System.String"));
            dtb.Columns.Add("Description", Type.GetType("System.String"));
            dtb.Columns.Add("FabricID", Type.GetType("System.String"));
            dtb.Columns.Add("FabricName", Type.GetType("System.String"));
            dtb.Columns.Add("SRItemType", Type.GetType("System.String"));
            dtb.Columns.Add("SRItemUnit", Type.GetType("System.String"));
            dtb.Columns.Add("QuantityFinish", Type.GetType("System.Decimal"));
            dtb.Columns.Add("QtyPO", Type.GetType("System.Decimal"));
            dtb.Columns.Add("QtyRecv", Type.GetType("System.Decimal"));
            dtb.Columns.Add("QtyPending", Type.GetType("System.Decimal"));
            dtb.Columns.Add("QtyPendingX", Type.GetType("System.Decimal"));
            dtb.Columns.Add("BatchNumber", Type.GetType("System.String"));
            dtb.Columns.Add("ExpiredDate", Type.GetType("System.DateTime"));
            dtb.Columns.Add("Price", Type.GetType("System.Decimal"));
            dtb.Columns.Add("Discount1Percentage", Type.GetType("System.Decimal"));
            dtb.Columns.Add("Discount2Percentage", Type.GetType("System.Decimal"));
            dtb.Columns.Add("Discount", Type.GetType("System.Decimal"));
            dtb.Columns.Add("IsDiscountInPercent", Type.GetType("System.Boolean"));
            dtb.Columns.Add("ConversionFactor", Type.GetType("System.Decimal"));
            dtb.Columns.Add("TaxPercentage", Type.GetType("System.Decimal"));
            dtb.Columns.Add("CostPrice", Type.GetType("System.Decimal"));
            dtb.Columns.Add("IsBonusItem", Type.GetType("System.Boolean"));
            dtb.Columns.Add("IsAccEd", Type.GetType("System.Boolean"));
            dtb.Columns.Add("IsAccPrice", Type.GetType("System.Boolean"));
            dtb.Columns.Add("IsAccQty", Type.GetType("System.Boolean"));
            dtb.Columns.Add("IsControlExpired", Type.GetType("System.Boolean"));
            dtb.Columns.Add("IsTaxable", Type.GetType("System.Boolean"));
            dtb.Columns.Add("IsConsignment", Type.GetType("System.Boolean"));
            dtb.Columns.Add("IsNotCompleteED", Type.GetType("System.Boolean"));
            dtb.Columns.Add("IsSelect", Type.GetType("System.Boolean"));

            foreach (DataRow row in dtbTemp.Rows)
            {
                DataRow rowAdd = dtb.NewRow();
                rowAdd["TransactionNo"] = row["TransactionNo"];
                rowAdd["SequenceNo"] = row["SequenceNo"];
                rowAdd["ItemID"] = row["ItemID"];
                rowAdd["Description"] = row["Description"];
                rowAdd["FabricID"] = row["FabricID"];
                rowAdd["FabricName"] = row["FabricName"];
                rowAdd["SRItemType"] = srItemType;
                rowAdd["TaxPercentage"] = taxPercentage;
                rowAdd["BatchNumber"] = row["BatchNumber"];
                rowAdd["ExpiredDate"] = row["ExpiredDate"];
                rowAdd["IsBonusItem"] = row["IsBonusItem"];
                rowAdd["IsAccEd"] = row["IsAccEd"];
                rowAdd["IsAccPrice"] = row["IsAccPrice"];
                rowAdd["IsAccQty"] = row["IsAccQty"];
                rowAdd["IsControlExpired"] = row["IsControlExpired"];
                rowAdd["IsTaxable"] = row["IsTaxable"];
                rowAdd["IsConsignment"] = itemTransaction.IsConsignmentAlreadyReceived ?? false;
                rowAdd["IsNotCompleteED"] = row["IsNotCompleteED"];
                rowAdd["IsSelect"] = row["IsSelect"];
                rowAdd["IsDiscountInPercent"] = row["IsDiscountInPercent"];

                if ((decimal)row["QtyPending"] % (decimal)row["ConversionFactor"] == 0) // Tidak Ada pecahan
                {
                    rowAdd["SRItemUnit"] = row["PurchaseUnitID"];
                    rowAdd["QuantityFinish"] = (decimal)row["QuantityFinishInBaseUnit"] / (decimal)row["ConversionFactor"];
                    rowAdd["Price"] = row["PricePurchase"];
                    rowAdd["Discount1Percentage"] = row["Discount1Percentage"];
                    rowAdd["Discount2Percentage"] = row["Discount2Percentage"];
                    rowAdd["Discount"] = row["Discount"];
                    rowAdd["QtyRecv"] = (decimal)row["QtyPending"] / (decimal)row["ConversionFactor"];
                    rowAdd["QtyPending"] = (decimal)row["QtyPending"] / (decimal)row["ConversionFactor"];
                    rowAdd["QtyPendingX"] = (decimal)row["QtyPendingX"] / (decimal)row["ConversionFactor"];
                    rowAdd["QtyPO"] = row["QtyPO"];
                    rowAdd["ConversionFactor"] = row["ConversionFactor"];
                }
                else
                {
                    rowAdd["SRItemUnit"] = row["BaseUnitID"];
                    rowAdd["QuantityFinish"] = row["QuantityFinishInBaseUnit"];
                    rowAdd["QtyPO"] = (decimal)row["QtyPO"] * (decimal)row["ConversionFactor"];
                    rowAdd["QtyRecv"] = row["QtyPending"];
                    rowAdd["QtyPending"] = row["QtyPending"];
                    rowAdd["QtyPendingX"] = row["QtyPendingX"];
                    rowAdd["Price"] = (decimal)row["PricePurchase"] / (decimal)row["ConversionFactor"];
                    rowAdd["Discount1Percentage"] = (decimal)row["Discount1Percentage"];
                    rowAdd["Discount2Percentage"] = (decimal)row["Discount2Percentage"];
                    rowAdd["Discount"] = (decimal)row["Discount"];
                    rowAdd["ConversionFactor"] = 1;
                }

                string parCostType = AppParameter.GetParameterValue(AppParameter.ParameterItem.ItemProductCostPriceType);

                if (srItemType == ItemType.Medical)
                {
                    var med = new ItemProductMedic();
                    if (med.LoadByPrimaryKey(row["ItemID"].ToString()))
                    {
                        if (parCostType == "AVG")
                            rowAdd["CostPrice"] = med.CostPrice;
                        else
                        {
                            rowAdd["CostPrice"] = (((decimal)row["PriceInCurrency"] - (decimal)row["Discount"]) / (decimal)row["ConversionFactor"]) * (1 + (taxPercentage / 100));
                        }
                    }
                    else
                    {
                        rowAdd["CostPrice"] = rowAdd["Price"];
                    }

                }
                else if (srItemType == ItemType.NonMedical)
                {
                    var nonMed = new ItemProductNonMedic();
                    if (nonMed.LoadByPrimaryKey(row["ItemID"].ToString()))
                    {
                        if (parCostType == "AVG")
                            rowAdd["CostPrice"] = nonMed.CostPrice;
                        else
                        {
                            rowAdd["CostPrice"] = (((decimal)row["PriceInCurrency"] - (decimal)row["Discount"]) / (decimal)row["ConversionFactor"]) * (1 + (taxPercentage / 100));
                        }
                    }
                    else
                    {
                        rowAdd["CostPrice"] = rowAdd["Price"];
                    }

                }
                else if (srItemType == ItemType.Kitchen)
                {
                    var kitchen = new ItemKitchen();
                    if (kitchen.LoadByPrimaryKey(row["ItemID"].ToString()))
                    {
                        if (parCostType == "AVG")
                            rowAdd["CostPrice"] = kitchen.CostPrice;
                        else
                        {
                            rowAdd["CostPrice"] = (((decimal)row["PriceInCurrency"] - (decimal)row["Discount"]) / (decimal)row["ConversionFactor"]) * (1 + (taxPercentage / 100));
                        }
                    }
                    else
                    {
                        rowAdd["CostPrice"] = rowAdd["Price"];
                    }

                }

                dtb.Rows.Add(rowAdd);
            }

            ViewState["POR_W3TOT:Detail" + Request.UserHostName] = dtb;

            grdDetail.Columns[0].Visible = AppSession.Parameter.IsPorUsingChecklistItem;

            if (!string.IsNullOrEmpty(contractNo) || (!AppSession.Parameter.IsPorCanChangeThePrice))
            {
                grdDetail.Columns.FindByUniqueName("Price").Visible = true; // price lbl
                grdDetail.Columns.FindByUniqueName("txtPrice").Visible = false; // price txt

                grdDetail.Columns.FindByUniqueName("IsDiscountInPercent").Visible = true; // discinpercent lbl
                grdDetail.Columns.FindByUniqueName("chkIsDiscInPercent").Visible = false; // discinpercent txt

                grdDetail.Columns.FindByUniqueName("Discount1Percentage").Visible = true; // disc1 lbl
                grdDetail.Columns.FindByUniqueName("txtDisc1").Visible = false; // disc1 txt

                grdDetail.Columns.FindByUniqueName("Discount2Percentage").Visible = true; // disc2 lbl
                grdDetail.Columns.FindByUniqueName("txtDisc2").Visible = false; // disc2 txt

                grdDetail.Columns.FindByUniqueName("Discount").Visible = true; // discamt lbl
                grdDetail.Columns.FindByUniqueName("txtDiscAmt").Visible = false; // discamt txt
            }

            if (itemTransaction.IsConsignmentAlreadyReceived != null && itemTransaction.IsConsignmentAlreadyReceived == true)
            {
                grdDetail.Columns.FindByUniqueName("txtQtyRecv").Visible = false; // qty
                grdDetail.Columns.FindByUniqueName("txtQtyRecv2").Visible = true; // qty 2 --> u/ konsinyasi, tidak boleh ganti qty
            }

            if (_isTxUsingEdDetail)
            {
                grdDetail.Columns.FindByUniqueName("txtBatchNumber").Visible = false; // batch no txt
                grdDetail.Columns.FindByUniqueName("txtExpiredDate").Visible = false; // ed txt
            }

            grdDetail.Columns.FindByUniqueName("FabricName").Visible = AppSession.Parameter.IsUsingFactoryInTheItemProcurementProcess;

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
            DataTable dtb = (DataTable)ViewState["POR_W3TOT:Detail" + Request.UserHostName];

            if (dtb != null)
            {
                using (new esTransactionScope())
                {
                    foreach (DataRow row in dtb.Rows)
                    {
                        if (_isTxUsingEdDetail == false)
                        {
                            bool isControlExpired = false;
                            var item = new Item();
                            if (item.LoadByPrimaryKey(row["ItemID"].ToString()))
                            {
                                if (item.SRItemType == ItemType.Medical)
                                {
                                    var product = new ItemProductMedic();
                                    if (product.LoadByPrimaryKey(item.ItemID))
                                        isControlExpired = product.IsControlExpired ?? false;
                                }
                                else if (item.SRItemType == ItemType.NonMedical)
                                {
                                    var product = new ItemProductNonMedic();
                                    if (product.LoadByPrimaryKey(item.ItemID))
                                        isControlExpired = product.IsControlExpired ?? false;
                                }
                                else if (item.SRItemType == ItemType.Kitchen)
                                {
                                    var product = new ItemKitchen();
                                    if (product.LoadByPrimaryKey(item.ItemID))
                                        isControlExpired = product.IsControlExpired ?? false;
                                }
                            }

                            if (isControlExpired && Convert.ToDouble(row["QtyRecv"]) > 0 && Convert.ToBoolean(row["IsSelect"]) == true)
                            {
                                if (Convert.ToDateTime(row["ExpiredDate"]) < (new DateTime()).NowAtSqlServer())
                                {
                                    ShowMessage(string.Format("Expired date for item {0} must greather than transaction date.", row["Description"]));
                                    return false;
                                }
                            }
                        }
                        if (Convert.ToBoolean(row["IsSelect"]) == true && (!AppSession.Parameter.IsNeedQtyControlOnPOR) && Convert.ToBoolean(row["IsBonusItem"]) == false && Convert.ToDouble(row["QtyRecv"]) + Convert.ToDouble(row["QtyPendingX"]) > Convert.ToDouble(row["QtyPending"]) )
                        {
                            ShowMessage(string.Format("Receive Qty for item {0} can not be greather than {1}.", row["Description"], (Convert.ToDouble(row["QtyPending"]) - Convert.ToDouble(row["QtyPendingX"])).ToString()));
                            return false;
                        }
                        if (Convert.ToBoolean(row["IsBonusItem"]) == false && (Convert.ToDouble(row["Price"]) == 0) && Convert.ToDouble(row["QtyRecv"]) > 0 && Convert.ToBoolean(row["IsSelect"]) == true)
                        {
                            ShowMessage(string.Format("Price for item {0} must greather than 0.", row["Description"]));
                            return false;
                        }
                    }
                }
            }
            Session["POR_W3TOT:ItemSelected" + Request.UserHostName] = ViewState["POR_W3TOT:Detail" + Request.UserHostName];
            return true;
        }

        protected void btnFilterOrder_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();

            grdDetail.DataSource = null;
            grdDetail.DataBind();
        }

        protected void cboSupplierName_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SupplierName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SupplierID"].ToString();
        }

        protected void cboSupplierName_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SupplierQuery();
            query.es.Top = 20;
            query.Select(
                query.SupplierID,
                query.SupplierName
                );
            query.Where(
                query.SupplierName.Like(searchTextContain),
                query.IsActive == true
                );
            cboSupplierName.DataSource = query.LoadDataTable();
            cboSupplierName.DataBind();
        }

        protected void cboServiceUnitName_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void cboServiceUnitName_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ServiceUnitQuery("a");
            var tcquery = new ServiceUnitTransactionCodeQuery("b");
            var usr = new AppUserServiceUnitQuery("c");

            query.InnerJoin(tcquery).On(query.ServiceUnitID == tcquery.ServiceUnitID);
            query.InnerJoin(usr).On(query.ServiceUnitID == usr.ServiceUnitID & usr.UserID == AppSession.UserLogin.UserID);
            query.es.Top = 20;
            query.es.Distinct = true;

            query.Select(
                query.ServiceUnitID,
                query.ServiceUnitName
                );
            query.Where(
                query.ServiceUnitName.Like(searchTextContain),
                query.IsActive == true,
                tcquery.SRTransactionCode == TransactionCode.PurchaseOrder
                );

            if (!string.IsNullOrEmpty(Request.QueryString["unitId"]))
            {
                var q = new ServiceUnitQuery();
                q.Where(q.ServiceUnitPorID == Request.QueryString["unitId"]);
                DataTable dtb = q.LoadDataTable();
                if (dtb.Rows.Count > 0)
                    query.Where(query.ServiceUnitPorID == Request.QueryString["unitId"]);
            }

            cboServiceUnitName.DataSource = query.LoadDataTable();
            cboServiceUnitName.DataBind();
        }

        protected void cboServiceUnitReqName_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ServiceUnitQuery("a");
            var tcquery = new ServiceUnitTransactionCodeQuery("b");
            var usr = new AppUserServiceUnitQuery("c");

            query.InnerJoin(tcquery).On(query.ServiceUnitID == tcquery.ServiceUnitID);
            query.InnerJoin(usr).On(query.ServiceUnitID == usr.ServiceUnitID & usr.UserID == AppSession.UserLogin.UserID);
            query.es.Top = 20;
            query.es.Distinct = true;

            query.Select(
                query.ServiceUnitID,
                query.ServiceUnitName
                );
            query.Where(
                query.ServiceUnitName.Like(searchTextContain),
                query.IsActive == true,
                tcquery.SRTransactionCode == TransactionCode.PurchaseRequest
                );

            if (!string.IsNullOrEmpty(Request.QueryString["unitId"]))
            {
                var q = new ServiceUnitQuery();
                q.Where(q.ServiceUnitPorID == Request.QueryString["unitId"]);
                DataTable dtb = q.LoadDataTable();
                if (dtb.Rows.Count > 0)
                    query.Where(query.ServiceUnitPorID == Request.QueryString["unitId"]);
            }

            cboServiceUnitReqName.DataSource = query.LoadDataTable();
            cboServiceUnitReqName.DataBind();
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            bool selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in grdDetail.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("chkIsSelect")).Checked = selected;
            }
        }
    }
}
