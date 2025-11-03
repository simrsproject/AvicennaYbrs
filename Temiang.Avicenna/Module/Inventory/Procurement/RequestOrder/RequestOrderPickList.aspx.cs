using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Procurement
{
    public partial class RequestOrderPickList : BasePageDialog
    {
        private bool _isNeedValidatedMax;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isPorByStockGroup =
                    AppParameter.IsYes(AppParameter.ParameterItem.IsPorByStockGroup);

                pnlFilterStockGroup.Visible = isPorByStockGroup;

                //if (!isPorByStockGroup)
                //    InitializeData("StockGroup");
                //else
                //{
                //    StandardReference.InitializeIncludeSpace(cboSRStockGroup, AppEnum.StandardReference.StockGroup);

                //    var locationID = Request.QueryString["lid"];
                //    var loc = new Location();
                //    loc.LoadByPrimaryKey(locationID);
                //    if (!string.IsNullOrEmpty(loc.SRStockGroup))
                //    {g
                //        ComboBox.SelectedValue(cboSRStockGroup, loc.SRStockGroup);
                //        cboSRStockGroup.Enabled = false;

                //        InitializeData("StockGroup");
                //    }
                //}

                txtPorBaseSalesDay.Value =
                    AppParameter.GetParameterValue(AppParameter.ParameterItem.PorBaseSalesDay).ToInt();

                txtPorForStockDay.Value =
                    AppParameter.GetParameterValue(AppParameter.ParameterItem.PorForStockDay).ToInt();

                var locationID = Request.QueryString["lid"];
                var loc = new Location();
                loc.LoadByPrimaryKey(locationID);

                if (isPorByStockGroup)
                {
                    StandardReference.InitializeIncludeSpace(cboSRStockGroup, AppEnum.StandardReference.StockGroup);

                    if (!string.IsNullOrEmpty(loc.SRStockGroup))
                    {
                        ComboBox.SelectedValue(cboSRStockGroup, loc.SRStockGroup);
                        //cboSRStockGroup.Enabled = false;
                    }
                }

                switch (Request.QueryString["it"])
                {
                    case BusinessObject.Reference.ItemType.Medical:
                        _isNeedValidatedMax = loc.IsValidateMaxValueOnPurcReqForIpm ?? false;
                        break;
                    case BusinessObject.Reference.ItemType.NonMedical:
                        _isNeedValidatedMax = loc.IsValidateMaxValueOnPurcReqForIpnm ?? false;
                        break;
                    case BusinessObject.Reference.ItemType.Kitchen:
                        _isNeedValidatedMax = loc.IsValidateMaxValueOnPurcReqForIk ?? false;
                        break;
                }

                grdDetail.Columns[10].Visible = !_isNeedValidatedMax;
                grdDetail.Columns[11].Visible = _isNeedValidatedMax;

                btnPrint.Visible = AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsShowPriceInPurchaseRequest);
                grdDetail.Columns[grdDetail.Columns.Count - 1].Visible = btnPrint.Visible; //disc2
                grdDetail.Columns[grdDetail.Columns.Count - 2].Visible = btnPrint.Visible; //disc1
                grdDetail.Columns[grdDetail.Columns.Count - 3].Visible = btnPrint.Visible; //price
            }
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["RequestOrderDetail" + Request.UserHostName] != null)
                grdDetail.DataSource = ViewState["RequestOrderDetail" + Request.UserHostName];
        }

        protected void grdDetail_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateDataSource();
        }

        private void UpdateDataSource()
        {
            DataTable dtb = (DataTable)ViewState["RequestOrderDetail" + Request.UserHostName];
            foreach (GridDataItem dataItem in grdDetail.MasterTableView.Items)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    if (row["ItemID"].Equals(dataItem.GetDataKeyValue("ItemID").ToString()))
                    {
                        row["QtyOrder"] = ((RadNumericTextBox)dataItem.FindControl("txtQtyOrder")).Value ?? 0;
                        break;
                    }
                }

                ViewState["RequestOrderDetail" + Request.UserHostName] = dtb;
            }
        }

        private void InitializeData(string byType)
        {
            //var dtb = AppParameter.IsYes(AppParameter.ParameterItem.IsPorByStockGroup) ?  InitializeDataFromItemBalanceByStockGroup(): InitializeDataFromItemBalance();
            var colMin = grdDetail.Columns.FindByDataField("Minimum");
            colMin.HeaderText = "Minimum";

            var colMax = grdDetail.Columns.FindByDataField("Maximum");
            colMax.HeaderText = "Maximum";

            DataTable dtb;
            switch (byType)
            {
                case "StockGroup":
                    dtb = InitializeDataFromItemBalanceByStockGroup();
                    break;
                case "StockTotal":
                    dtb = InitializeDataFromItemBalance();
                    break;
                default:
                    colMin.HeaderText = "Avg Sales Per Day";
                    colMax.HeaderText = string.Format("Stock for {0} days", txtPorForStockDay.Value.ToInt());
                    dtb = InitializeDataFromSalesHist();
                    break;
            }

            grdDetail.DataSource = dtb;
            grdDetail.DataBind();
        }

        private DataTable InitializeDataFromItemBalance()
        {
            var query = new ItemBalanceQuery("a");

            if (Request.QueryString["it"] == ItemType.Medical)
            {
                var qrItemMed = new ItemProductMedicQuery("b");
                query.Select
                    (
                        qrItemMed.SRItemUnit,
                        qrItemMed.SRPurchaseUnit,
                        qrItemMed.ConversionFactor,
                        qrItemMed.PriceInBaseUnit,
                        qrItemMed.PriceInPurchaseUnit,
                        qrItemMed.PriceInPurchaseUnit.As("Price"),
                        qrItemMed.PurchaseDiscount1.As("Discount1Percentage"),
                        qrItemMed.PurchaseDiscount2.As("Discount2Percentage")
                    );
                query.InnerJoin(qrItemMed).On(query.ItemID == qrItemMed.ItemID &&
                                              qrItemMed.IsInventoryItem ==
                                              bool.Parse(Request.QueryString["inv"]) &&
                                              qrItemMed.IsConsignment == false);
            }
            else if (Request.QueryString["it"] == ItemType.NonMedical)
            {
                var qrItemMed = new ItemProductNonMedicQuery("b");
                query.Select
                    (
                        qrItemMed.SRItemUnit,
                        qrItemMed.SRPurchaseUnit,
                        qrItemMed.ConversionFactor,
                        qrItemMed.PriceInBaseUnit,
                        qrItemMed.PriceInPurchaseUnit,
                        qrItemMed.PriceInPurchaseUnit.As("Price"),
                        qrItemMed.PurchaseDiscount1.As("Discount1Percentage"),
                        qrItemMed.PurchaseDiscount2.As("Discount2Percentage")
                    );
                query.InnerJoin(qrItemMed).On(query.ItemID == qrItemMed.ItemID &&
                                              qrItemMed.IsInventoryItem ==
                                              bool.Parse(Request.QueryString["inv"]) &&
                                              qrItemMed.IsConsignment == false);
            }
            else
            {
                var qrItemMed = new ItemKitchenQuery("b");
                query.Select
                    (
                        qrItemMed.SRItemUnit,
                        qrItemMed.SRPurchaseUnit,
                        qrItemMed.ConversionFactor,
                        qrItemMed.PriceInBaseUnit,
                        qrItemMed.PriceInPurchaseUnit,
                        qrItemMed.PriceInPurchaseUnit.As("Price"),
                        qrItemMed.PurchaseDiscount1.As("Discount1Percentage"),
                        qrItemMed.PurchaseDiscount2.As("Discount2Percentage")
                    );
                query.InnerJoin(qrItemMed).On(query.ItemID == qrItemMed.ItemID &&
                                              qrItemMed.IsInventoryItem ==
                                              bool.Parse(Request.QueryString["inv"]));
            }

            var qrItem = new ItemQuery("d");
            query.InnerJoin(qrItem).On(query.ItemID == qrItem.ItemID);

            query.Where
                (
                    query.LocationID == Request.QueryString["lid"],
                    qrItem.IsActive == true
                );

            if (AppSession.Parameter.IsPurchaseRequestBasedOnItemCategory && !string.IsNullOrEmpty(Request.QueryString["itmcat"]))
            {
                query.Where(qrItem.SRItemCategory == Request.QueryString["itmcat"]);
            }

            query.Where(query.Balance <= query.Minimum, query.Maximum > 0);

            if (AppParameter.IsYes(AppParameter.ParameterItem.IsPorBySupplierItem) &&
                !string.IsNullOrEmpty(Request.QueryString["suppid"]))
            {
                var itemSupp = new SupplierItemQuery("siq");
                query.InnerJoin(itemSupp).On(query.ItemID == itemSupp.ItemID &&
                                             itemSupp.SupplierID == Request.QueryString["suppid"]);
            }
            if (AppParameter.IsYes(AppParameter.ParameterItem.IsPorByProductAccount) &&
                !string.IsNullOrEmpty(Request.QueryString["paid"]))
                query.Where(qrItem.ProductAccountID == Request.QueryString["paid"]);

            var su = new ServiceUnit();
            var tloc = su.GetMainLocationId(Request.QueryString["tu"]);

            // Sub Query BalanceTotal
            var itemBalTot = new ItemBalanceQuery("ibt");
            itemBalTot.Select((itemBalTot.Balance.Sum().As("BalanceTotal")));
            itemBalTot.Where(itemBalTot.ItemID == query.ItemID);

            query.Select
                (
                    query.ItemID,
                    query.Minimum,
                    query.Maximum,
                    query.Balance,
                    itemBalTot.Select().As("BalanceTotal"),
                    //@"<ISNULL((SELECT SUM(ib.Balance) FROM ItemBalance ib WHERE ib.LocationID = '" + tloc +
                    //@"' AND ib.ItemID = a.ItemID), 0) AS PurcUnitBalance>",
                    "<CASE WHEN b.ConversionFactor = 0 THEN a.Maximum - a.Balance ELSE (a.Maximum-a.Balance) END as PurcUnitBalance>",
                    //"<CASE WHEN b.ConversionFactor = 0 THEN a.Maximum - a.Balance ELSE CEILING((a.Maximum-a.Balance) / b.ConversionFactor) END as QtyOrder>",
                    "<CASE WHEN b.ConversionFactor = 0 THEN a.Maximum - a.Balance ELSE (a.Maximum-a.Balance) END as QtyOrder>",
                    qrItem.ItemName.As("ItemName"),
                    @"<'' AS Unit>"
                );

            query.OrderBy(qrItem.ItemName.Ascending);
            DataTable dtb = query.LoadDataTable();

            var isUsingItemUnit = AppParameter.IsYes(AppParameter.ParameterItem.IsPurchaseRequestsUsingItemUnit);
            if (isUsingItemUnit)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    row["ConversionFactor"] = 1;
                    row["SRPurchaseUnit"] = row["SRItemUnit"];
                    row["Unit"] = row["SRItemUnit"] + "/1";
                    row["Price"] = row["PriceInBaseUnit"];
                }
            }
            else
            {
                foreach (DataRow row in dtb.Rows)
                {
                    row["QtyOrder"] =
                        Math.Ceiling(((Convert.ToDecimal(row["Maximum"])) -
                                      (Convert.ToDecimal(row["Balance"]))) /
                                     (Convert.ToDecimal(row["ConversionFactor"])));
                    row["Unit"] = row["SRPurchaseUnit"] + "/" +
                                  string.Format("{0:n0}", (Convert.ToDecimal(row["ConversionFactor"])));
                }
            }

            dtb.AcceptChanges();

            ViewState["RequestOrderDetail" + Request.UserHostName] = dtb;
            return dtb;
        }

        private DataTable InitializeDataFromSalesHist()
        {
            var isInventoryItem = bool.Parse(Request.QueryString["inv"]);

            // Avg Sales
            var dtbAvgSales = LoadAvgSales(isInventoryItem);

            // Balance MW 
            var dtbBalanceMW = LoadBalance(isInventoryItem, false);

            // Balance Total
            var dtbBalanceTot = LoadBalance(isInventoryItem, true);

            // Lengkapi Field
            dtbAvgSales.Columns.Add("QtyOrder", typeof(Int32));
            dtbAvgSales.Columns.Add("Balance", typeof(Int32));
            dtbAvgSales.Columns.Add("Minimum", typeof(Decimal));
            dtbAvgSales.Columns.Add("Maximum", typeof(Decimal));
            dtbAvgSales.Columns.Add("BalanceTotal", typeof(Int32));
            dtbAvgSales.Columns.Add("PurcUnitBalance", typeof(Int32));
            //dtbAvgSales.Columns.Add("ItemName", typeof(String));
            dtbAvgSales.Columns.Add("Unit", typeof(String));
            dtbAvgSales.Columns.Add("ConversionFactor", typeof(Int32));
            dtbAvgSales.Columns.Add("SRPurchaseUnit", typeof(String));
            dtbAvgSales.Columns.Add("SRItemUnit", typeof(String));
            dtbAvgSales.Columns.Add("PriceInBaseUnit", typeof(Decimal));
            dtbAvgSales.Columns.Add("PriceInPurchaseUnit", typeof(Decimal));
            dtbAvgSales.Columns.Add("Price", typeof(Decimal));
            dtbAvgSales.Columns.Add("Discount1Percentage", typeof(Decimal));
            dtbAvgSales.Columns.Add("Discount2Percentage", typeof(Decimal));

            // Lengkapi 
            foreach (DataRow row in dtbAvgSales.Rows)
            {
                var rowBalance = dtbBalanceMW.Rows.Find(row["ItemID"]);
                var balanceQty = rowBalance == null ? 0 : rowBalance["Balance"].ToInt();
                balanceQty = balanceQty < 0 ? 0 : balanceQty;

                var qtyNeed = (row["AvgSales"].ToDecimal() * txtPorForStockDay.Value.ToDecimal()).ToInt();
                row["QtyOrder"] = qtyNeed - (chkIsIgnoreBalance.Checked ? 0 : balanceQty); // Isi dalam satuan kecil dahulu dan dibawah diconvert jika dalam PU
                row["Maximum"] = qtyNeed; // Isi dgn total kebutuhan dalam satuan kecil krn nilai Max tidak bisa diambil dari setingan max per lokasi (balance nya ditotal)
                row["Balance"] = balanceQty;
                row["Minimum"] = row["AvgSales"].ToDecimal(); // Isi dgn Avg Sales //row["AvgSales"].ToInt()

                var rowBalanceTot = dtbBalanceTot.Rows.Find(row["ItemID"]);
                var balanceQtyTot = rowBalanceTot == null ? 0 : rowBalanceTot["Balance"].ToInt();
                //var balanceQtyTot = dtbBalanceTot == null ? 0 : rowBalanceTot["Balance"].ToInt();

                row["BalanceTotal"] = balanceQtyTot < 0 ? 0 : balanceQtyTot;

                //var item = new Item();
                //item.LoadByPrimaryKey(row["ItemID"].ToString());
                //row["ItemName"] = item.ItemName;

                if (Request.QueryString["it"] == ItemType.Medical)
                {
                    var idt = new ItemProductMedic();
                    idt.LoadByPrimaryKey(row["ItemID"].ToString());
                    row["ConversionFactor"] = idt.ConversionFactor;
                    row["SRPurchaseUnit"] = idt.SRPurchaseUnit;
                    row["SRItemUnit"] = idt.SRItemUnit;
                    row["PriceInBaseUnit"] = idt.PriceInBaseUnit;
                    row["PriceInPurchaseUnit"] = idt.PriceInPurchaseUnit;
                    row["Price"] = idt.PriceInPurchaseUnit;
                    row["Discount1Percentage"] = idt.PurchaseDiscount1;
                    row["Discount2Percentage"] = idt.PurchaseDiscount2;
                }
                else if (Request.QueryString["it"] == ItemType.NonMedical)
                {
                    var idt = new ItemProductNonMedic();
                    idt.LoadByPrimaryKey(row["ItemID"].ToString());
                    row["ConversionFactor"] = idt.ConversionFactor;
                    row["SRPurchaseUnit"] = idt.SRPurchaseUnit;
                    row["SRItemUnit"] = idt.SRItemUnit;
                    row["PriceInBaseUnit"] = idt.PriceInBaseUnit;
                    row["PriceInPurchaseUnit"] = idt.PriceInPurchaseUnit;
                    row["Price"] = idt.PriceInPurchaseUnit;
                    row["Discount1Percentage"] = idt.PurchaseDiscount1;
                    row["Discount2Percentage"] = idt.PurchaseDiscount2;
                }
                else
                {
                    var idt = new ItemKitchen();
                    idt.LoadByPrimaryKey(row["ItemID"].ToString());
                    row["ConversionFactor"] = idt.ConversionFactor;
                    row["SRPurchaseUnit"] = idt.SRPurchaseUnit;
                    row["SRItemUnit"] = idt.SRItemUnit;
                    row["PriceInBaseUnit"] = idt.PriceInBaseUnit;
                    row["PriceInPurchaseUnit"] = idt.PriceInPurchaseUnit;
                    row["Price"] = idt.PriceInPurchaseUnit;
                    row["Discount1Percentage"] = idt.PurchaseDiscount1;
                    row["Discount2Percentage"] = idt.PurchaseDiscount2;
                }

                //if (row["ConversionFactor"].ToInt() > 0)
                //    row["PurcUnitBalance"] = Math.Ceiling(balanceQty / row["ConversionFactor"].ToDecimal());
                //else
                //    row["PurcUnitBalance"] = balanceQty;
            }


            // Convert qty order dalam satuan terpilih
            var isUsingItemUnit = AppParameter.IsYes(AppParameter.ParameterItem.IsPurchaseRequestsUsingItemUnit);
            if (isUsingItemUnit)
            {
                // Menggunakan satuan kecil
                foreach (DataRow row in dtbAvgSales.Rows)
                {
                    row["ConversionFactor"] = 1;
                    row["SRPurchaseUnit"] = row["SRItemUnit"];
                    row["Unit"] = row["SRItemUnit"] + "/1";
                    row["Price"] = row["PriceInBaseUnit"];
                }
            }
            else
            {
                // Convert ke Satuan Purchase Unit
                foreach (DataRow row in dtbAvgSales.Rows)
                {
                    row["QtyOrder"] =
                        Math.Ceiling(((Convert.ToDecimal(row["Maximum"])) - (chkIsIgnoreBalance.Checked ? 0 : (Convert.ToDecimal(row["Balance"])))) /
                                     (Convert.ToDecimal(row["ConversionFactor"])));

                    row["Unit"] = row["SRPurchaseUnit"] + "/" +
                                  string.Format("{0:n0}", (Convert.ToDecimal(row["ConversionFactor"])));

                    row["PurcUnitBalance"] = row["QtyOrder"];
                }
            }

            // Hapus yg QtyOrder == 0
            foreach (DataRow row in dtbAvgSales.Rows)
            {
                if (row["QtyOrder"].ToInt() < 1)
                    row.Delete();
            }
            dtbAvgSales.AcceptChanges();

            ViewState["RequestOrderDetail" + Request.UserHostName] = dtbAvgSales;
            return dtbAvgSales;
        }

        private DataTable LoadAvgSales(bool isInventoryItem)
        {
            var sales = new ItemSalesPerDateQuery("s");

            var qrItem = new ItemQuery("d");
            sales.InnerJoin(qrItem).On(sales.ItemID == qrItem.ItemID); 
            sales.Where
            (
                qrItem.IsActive == true
            );
            if (!string.IsNullOrEmpty(cboSRStockGroup.SelectedValue))
                sales.Where(sales.SRStockGroup == cboSRStockGroup.SelectedValue);

            if (AppSession.Parameter.IsPurchaseRequestBasedOnItemCategory && !string.IsNullOrEmpty(Request.QueryString["itmcat"]))
            {
                sales.Where(qrItem.SRItemCategory == Request.QueryString["itmcat"]);
            }

            if (!string.IsNullOrWhiteSpace(cboItemGroupID.SelectedValue))
                sales.Where(qrItem.ItemGroupID == cboItemGroupID.SelectedValue);


            if (AppParameter.IsYes(AppParameter.ParameterItem.IsPorBySupplierItem) &&
                !string.IsNullOrEmpty(Request.QueryString["suppid"]))
            {
                var itemSupp = new SupplierItemQuery("siq");
                sales.InnerJoin(itemSupp).On(sales.ItemID == itemSupp.ItemID &&
                                             itemSupp.SupplierID == Request.QueryString["suppid"]);

            }

            if (AppParameter.IsYes(AppParameter.ParameterItem.IsPorByProductAccount) &&
                !string.IsNullOrEmpty(Request.QueryString["paid"]))
            {
                sales.Where(qrItem.ProductAccountID == Request.QueryString["paid"]);
            }

            if (Request.QueryString["it"] == ItemType.Medical)
            {
                var qrItemMed = new ItemProductMedicQuery("b");
                sales.InnerJoin(qrItemMed).On(sales.ItemID == qrItemMed.ItemID &&
                                              qrItemMed.IsInventoryItem == isInventoryItem &&
                                              qrItemMed.IsConsignment == false);
            }
            else if (Request.QueryString["it"] == ItemType.NonMedical)
            {
                var qrItemMed = new ItemProductNonMedicQuery("b");
                sales.InnerJoin(qrItemMed).On(sales.ItemID == qrItemMed.ItemID &&
                                              qrItemMed.IsInventoryItem == isInventoryItem &&
                                              qrItemMed.IsConsignment == false);
            }
            else
            {
                var qrItemMed = new ItemKitchenQuery("b");
                sales.InnerJoin(qrItemMed).On(sales.ItemID == qrItemMed.ItemID &&
                                              qrItemMed.IsInventoryItem == isInventoryItem);
            }

            var dayCount = txtPorBaseSalesDay.Value.ToInt();
            sales.Select(sales.ItemID, qrItem.ItemName, (sales.QuantityOut.Sum() / dayCount).As("AvgSales"));
            sales.Where(sales.MovementDate > DateTime.Today.AddDays(0 - dayCount));
            sales.GroupBy(sales.ItemID, qrItem.ItemName);
            sales.OrderBy(qrItem.ItemName.Ascending);

            var dtbAvgSales = sales.LoadDataTable();
            return dtbAvgSales;
        }

        //private DataTable LoadBalanceTotal(bool isInventoryItem)
        //{
        //    var balance = new ItemBalanceQuery("a");
        //    if (Request.QueryString["it"] == ItemType.Medical)
        //    {
        //        var qrItemMed = new ItemProductMedicQuery("b");
        //        balance.InnerJoin(qrItemMed).On(balance.ItemID == qrItemMed.ItemID &&
        //                                        qrItemMed.IsInventoryItem == isInventoryItem
        //                                        &&
        //                                        qrItemMed.IsConsignment == false);
        //    }
        //    else if (Request.QueryString["it"] == ItemType.NonMedical)
        //    {
        //        var qrItemMed = new ItemProductNonMedicQuery("b");
        //        balance.InnerJoin(qrItemMed).On(balance.ItemID == qrItemMed.ItemID &&
        //                                        qrItemMed.IsInventoryItem == isInventoryItem &&
        //                                        qrItemMed.IsConsignment == false);
        //    }
        //    else
        //    {
        //        var qrItemMed = new ItemKitchenQuery("b");
        //        balance.InnerJoin(qrItemMed).On(balance.ItemID == qrItemMed.ItemID &&
        //                                        qrItemMed.IsInventoryItem == isInventoryItem);
        //    }


        //    balance.Select(balance.ItemID, (balance.Balance + balance.Booking).Sum().As("Balance"));
        //    balance.GroupBy(balance.ItemID);
        //    var dtbBalanceTot = balance.LoadDataTable();
        //    dtbBalanceTot.PrimaryKey = new[] { dtbBalanceTot.Columns["ItemID"] };
        //    return dtbBalanceTot;
        //}

        private DataTable LoadBalance(bool isInventoryItem, bool isTotal)
        {
            var balance = new ItemBalanceQuery("a");
            if (!isTotal)
            {
                var loc = new LocationQuery("l");
                balance.InnerJoin(loc).On(balance.LocationID == loc.LocationID);
                //if (string.IsNullOrEmpty(cboSRStockGroup.SelectedValue))
                //    balance.Where(loc.SRStockGroup == "MW");
                //else
                //    balance.Where(loc.SRStockGroup == cboSRStockGroup.SelectedValue);
                if (!string.IsNullOrEmpty(cboSRStockGroup.SelectedValue))
                   balance.Where(loc.SRStockGroup == cboSRStockGroup.SelectedValue);
            }
            if (Request.QueryString["it"] == ItemType.Medical)
            {
                var qrItemMed = new ItemProductMedicQuery("b");
                balance.InnerJoin(qrItemMed).On(balance.ItemID == qrItemMed.ItemID &&
                                              qrItemMed.IsInventoryItem == isInventoryItem
                                              &&
                                              qrItemMed.IsConsignment == false);
            }
            else if (Request.QueryString["it"] == ItemType.NonMedical)
            {
                var qrItemMed = new ItemProductNonMedicQuery("b");
                balance.InnerJoin(qrItemMed).On(balance.ItemID == qrItemMed.ItemID &&
                                              qrItemMed.IsInventoryItem == isInventoryItem &&
                                              qrItemMed.IsConsignment == false);
            }
            else
            {
                var qrItemMed = new ItemKitchenQuery("b");
                balance.InnerJoin(qrItemMed).On(balance.ItemID == qrItemMed.ItemID &&
                                              qrItemMed.IsInventoryItem == isInventoryItem);
            }

            // + qty booking (qty distribusi yg belum di confirm)
            balance.Select(balance.ItemID, (balance.Balance + balance.Booking).Sum().As("Balance"));
            balance.GroupBy(balance.ItemID);
            var dtbBal = balance.LoadDataTable();
            dtbBal.PrimaryKey = new[] { dtbBal.Columns["ItemID"] };
            return dtbBal;
        }

        private DataTable InitializeDataFromItemBalanceByStockGroup()
        {
            var query = new ItemBalanceByStockGroupQuery("a");

            if (Request.QueryString["it"] == ItemType.Medical)
            {
                var qrItemMed = new ItemProductMedicQuery("b");
                query.Select
                    (
                        qrItemMed.SRItemUnit,
                        qrItemMed.SRPurchaseUnit,
                        qrItemMed.ConversionFactor,
                        qrItemMed.PriceInBaseUnit,
                        qrItemMed.PriceInPurchaseUnit,
                        qrItemMed.PriceInPurchaseUnit.As("Price"),
                        qrItemMed.PurchaseDiscount1.As("Discount1Percentage"),
                        qrItemMed.PurchaseDiscount2.As("Discount2Percentage")
                    );
                query.InnerJoin(qrItemMed).On(query.ItemID == qrItemMed.ItemID &&
                                              qrItemMed.IsInventoryItem ==
                                              bool.Parse(Request.QueryString["inv"]) &&
                                              qrItemMed.IsConsignment == false);
            }
            else if (Request.QueryString["it"] == ItemType.NonMedical)
            {
                var qrItemMed = new ItemProductNonMedicQuery("b");
                query.Select
                    (
                        qrItemMed.SRItemUnit,
                        qrItemMed.SRPurchaseUnit,
                        qrItemMed.ConversionFactor,
                        qrItemMed.PriceInBaseUnit,
                        qrItemMed.PriceInPurchaseUnit,
                        qrItemMed.PriceInPurchaseUnit.As("Price"),
                        qrItemMed.PurchaseDiscount1.As("Discount1Percentage"),
                        qrItemMed.PurchaseDiscount2.As("Discount2Percentage")
                    );
                query.InnerJoin(qrItemMed).On(query.ItemID == qrItemMed.ItemID &&
                                              qrItemMed.IsInventoryItem ==
                                              bool.Parse(Request.QueryString["inv"]) &&
                                              qrItemMed.IsConsignment == false);
            }
            else
            {
                var qrItemMed = new ItemKitchenQuery("b");
                query.Select
                    (
                        qrItemMed.SRItemUnit,
                        qrItemMed.SRPurchaseUnit,
                        qrItemMed.ConversionFactor,
                        qrItemMed.PriceInBaseUnit,
                        qrItemMed.PriceInPurchaseUnit,
                        qrItemMed.PriceInPurchaseUnit.As("Price"),
                        qrItemMed.PurchaseDiscount1.As("Discount1Percentage"),
                        qrItemMed.PurchaseDiscount2.As("Discount2Percentage")
                    );
                query.InnerJoin(qrItemMed).On(query.ItemID == qrItemMed.ItemID &&
                                              qrItemMed.IsInventoryItem ==
                                              bool.Parse(Request.QueryString["inv"]));
            }

            var qrItem = new ItemQuery("d");
            query.InnerJoin(qrItem).On(query.ItemID == qrItem.ItemID);

            query.Where
                (
                    qrItem.IsActive == true
                );

            if (AppSession.Parameter.IsPurchaseRequestBasedOnItemCategory && !string.IsNullOrEmpty(Request.QueryString["itmcat"]))
            {
                query.Where(qrItem.SRItemCategory == Request.QueryString["itmcat"]);
            }

            query.Where(query.SRStockGroup == cboSRStockGroup.SelectedValue, query.Balance <= query.Minimum, query.Maximum > 0);

            if (AppParameter.IsYes(AppParameter.ParameterItem.IsPorBySupplierItem) &&
                !string.IsNullOrEmpty(Request.QueryString["suppid"]))
            {
                var itemSupp = new SupplierItemQuery("siq");
                query.InnerJoin(itemSupp).On(query.ItemID == itemSupp.ItemID &&
                                             itemSupp.SupplierID == Request.QueryString["suppid"]);
            }
            if (AppParameter.IsYes(AppParameter.ParameterItem.IsPorByProductAccount) &&
                !string.IsNullOrEmpty(Request.QueryString["paid"]))
                query.Where(qrItem.ProductAccountID == Request.QueryString["paid"]);

            // Sub Query BalanceTotal
            var itemBalTot = new ItemBalanceQuery("ibt");
            itemBalTot.Select((itemBalTot.Balance.Sum().As("BalanceTotal")));
            itemBalTot.Where(itemBalTot.ItemID == query.ItemID);

            // balance loc
            //var itemBal = new ItemBalanceQuery("iBal");
            //query.LeftJoin(itemBal).On(query.ItemID == itemBal.ItemID && itemBal.LocationID == Request.QueryString["lid"]);

            query.Select
                (
                    query.ItemID,
                    query.Minimum,
                    query.Maximum,
                    query.Balance,
                    //itemBal.Balance.As("BalanceLoc"),
                    itemBalTot.Select().As("BalanceTotal"),
                    //"<CASE WHEN b.ConversionFactor = 0 THEN a.Balance ELSE CEILING(a.Balance / b.ConversionFactor) END as PurcUnitBalance>",
                    "<CASE WHEN b.ConversionFactor = 0 THEN a.Maximum - a.Balance ELSE (a.Maximum-a.Balance) END as PurcUnitBalance>",
                    //"<CASE WHEN b.ConversionFactor = 0 THEN a.Maximum - a.Balance ELSE CEILING((a.Maximum-a.Balance) / b.ConversionFactor) END as QtyOrder>",
                    "<CASE WHEN b.ConversionFactor = 0 THEN a.Maximum - a.Balance ELSE (a.Maximum-a.Balance) END as QtyOrder>",
                    qrItem.ItemName.As("ItemName"),
                    @"<'' AS Unit>"
                );

            query.OrderBy(qrItem.ItemName.Ascending);
            DataTable dtb = query.LoadDataTable();

            var isUsingItemUnit = AppParameter.IsYes(AppParameter.ParameterItem.IsPurchaseRequestsUsingItemUnit);
            if (isUsingItemUnit)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    row["ConversionFactor"] = 1;
                    row["SRPurchaseUnit"] = row["SRItemUnit"];
                    row["Unit"] = row["SRItemUnit"] + "/1";
                    row["Price"] = row["PriceInBaseUnit"];
                }
            }
            else
            {
                foreach (DataRow row in dtb.Rows)
                {
                    row["QtyOrder"] =
                        Math.Ceiling(((Convert.ToDecimal(row["Maximum"])) -
                                      (Convert.ToDecimal(row["Balance"]))) /
                                     (Convert.ToDecimal(row["ConversionFactor"])));
                    row["Unit"] = row["SRPurchaseUnit"] + "/" +
                                  string.Format("{0:n0}", (Convert.ToDecimal(row["ConversionFactor"])));
                }
            }

            dtb.AcceptChanges();

            ViewState["RequestOrderDetail" + Request.UserHostName] = dtb;
            return dtb;
        }

        private ItemTransactionItem FindItemTransactionItem(ItemTransactionItemCollection coll, string itemID)
        {
            foreach (ItemTransactionItem entity in coll)
            {
                if (entity.ItemID == itemID)
                    return entity;
            }
            return null;
        }

        private ItemTransactionItemCollection ItemTransactionItems
        {
            get
            {
                //if (IsPostBack)
                //{
                object obj = Session["RequestOrderItems" + Request.UserHostName];
                if (obj != null)
                    return ((ItemTransactionItemCollection)(obj));
                //}

                var coll = new ItemTransactionItemCollection();

                var query = new ItemTransactionItemQuery("a");
                var iq = new ItemQuery("b");

                query.Select
                    (
                        query,
                        iq.ItemName.As("refToItem_ItemName"),
                        @"<CASE WHEN b.SRItemType <> '21' THEN '' ELSE 'Specification : '+ ISNULL(a.Specification, '') END AS 'refToAdditionalInfo'>"
                    );

                query.LeftJoin(iq).On(query.ItemID == iq.ItemID);
                query.Where(query.TransactionNo == ((Request.QueryString["md"] == "new") ? string.Empty: Request.QueryString["tno"]));

                query.OrderBy(query.SequenceNo.Ascending);

                if (bool.Parse(Request.QueryString["inv"]) && AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPOWithStockInfo))
                {
                    InitializeQueryWithStockInfo(query);
                }
                else
                {
                    query.Select(@"<CONVERT(decimal(10,2), 0) AS refToItemBalance_Balance>",
                    @"<CONVERT(decimal(10,2), 0) AS refToItemBalance_BalanceSG>",
                    @"<CONVERT(decimal(10,2), 0) AS refToItemBalance_Minimum>",
                    @"<CONVERT(decimal(10,2), 0) AS refToItemBalance_Maximum>",
                    @"<'' AS refToItemProduct_SRItemUnit>",
                    @"<CONVERT(decimal(10,2), 0) AS refToItemBalance_BalanceTotal>"
                    );
                }

                coll.Load(query);

                Session["RequestOrderItems" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["RequestOrderItems" + Request.UserHostName] = value; }
        }

        private void InitializeQueryWithStockInfo(ItemTransactionItemQuery query)
        {
            // Base Unit
            var ipnmq = new ItemProductNonMedicQuery("i2");
            var ikq = new ItemKitchenQuery("i2");
            var ipmq = new ItemProductMedicQuery("i2");

            switch (Request.QueryString["it"])
            {
                case ItemType.NonMedical:
                    query.LeftJoin(ipnmq).On(query.ItemID == ipnmq.ItemID);
                    break;
                case ItemType.Kitchen:
                    query.LeftJoin(ikq).On(query.ItemID == ikq.ItemID);
                    break;
                default:
                    query.LeftJoin(ipmq).On(query.ItemID == ipmq.ItemID);
                    break;
            }

            // Balance Min Max
            if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPorByStockGroup))
            {
                var stockGroup = "ABCD_EFG";
                var ibbsgq = new ItemBalanceByStockGroupQuery("c");
                var loc = new Location();
                loc.LoadByPrimaryKey(Request.QueryString["lid"]);
                if (!string.IsNullOrEmpty(loc.SRStockGroup))
                    stockGroup = loc.SRStockGroup;
                query.LeftJoin(ibbsgq).On(query.ItemID == ibbsgq.ItemID && ibbsgq.SRStockGroup == stockGroup);

                var ibq = new ItemBalanceQuery("bl");
                var locationID = Request.QueryString["lid"] ?? string.Empty;
                query.LeftJoin(ibq).On(query.ItemID == ibq.ItemID && ibq.LocationID == locationID);

                query.Select(@"<CONVERT(decimal(10,2),COALESCE(c.Balance,0)) AS refToItemBalance_BalanceSG>",
                    @"<CONVERT(decimal(10,2),COALESCE(bl.Balance,0)) AS refToItemBalance_Balance>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Minimum,0)) AS refToItemBalance_Minimum>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Maximum,0)) AS refToItemBalance_Maximum>",
                    @"<i2.SRItemUnit AS refToItemProduct_SRItemUnit>"
                    );
            }
            else
            {
                var locationID = ProcurementUtils.LocationIdByItemType(Request.QueryString["it"]);
                var ibq = new ItemBalanceQuery("c");
                if (string.IsNullOrEmpty(locationID))
                    locationID = "ABCD_EFG";
                query.LeftJoin(ibq).On(query.ItemID == ibq.ItemID && ibq.LocationID == locationID);

                query.Select(@"<CONVERT(decimal(10,2),0) AS refToItemBalance_BalanceSG>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Balance,0)) AS refToItemBalance_Balance>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Minimum,0)) AS refToItemBalance_Minimum>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Maximum,0)) AS refToItemBalance_Maximum>",
                    @"<i2.SRItemUnit AS refToItemProduct_SRItemUnit>"
                    );
            }

            // Sub Query BalanceTotal
            var itemBalTot = new ItemBalanceQuery("ibt");
            itemBalTot.Select((itemBalTot.Balance.Sum().As("BalanceTotal")));
            itemBalTot.Where(itemBalTot.ItemID == query.ItemID);
            query.Select(itemBalTot.Select().As("refToItemBalance_BalanceTotal"));
        }

        public override bool OnButtonOkClicked()
        {
            //UpdateDataSource();

            DataTable tbl = (DataTable)ViewState["RequestOrderDetail" + Request.UserHostName];

            //var coll = (ItemTransactionItemCollection)Session["RequestOrderItems" + Request.UserHostName];
            var coll = ItemTransactionItems;
            string seqNo = coll != null && coll.HasData ? coll[coll.Count - 1].SequenceNo : "000";

            foreach (GridDataItem dataItem in grdDetail.MasterTableView.Items.Cast<GridDataItem>().Where(g => ((CheckBox)g.FindControl("detailChkbox")).Checked))
            {
                //if (!((CheckBox)dataItem.FindControl("detailChkbox")).Checked) continue;

                //foreach (DataRow row in tbl.Rows)
                //{
                decimal qtyOrder = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQtyOrder")).Value);
                if (qtyOrder <= 0) continue;

                ItemTransactionItem entity = FindItemTransactionItem(coll, dataItem["ItemID"].Text);
                if (entity == null)
                {
                    entity = coll.AddNew();

                    entity.SequenceNo = string.Format("{0:000}", int.Parse(seqNo) + 1);
                    seqNo = entity.SequenceNo;
                }
                entity.ItemID = dataItem["ItemID"].Text;
                entity.ItemName = dataItem["ItemName"].Text;
                entity.ReferenceNo = string.Empty;
                entity.ReferenceSequenceNo = string.Empty;
                entity.Quantity = qtyOrder;
                entity.SRItemUnit = dataItem["SRPurchaseUnit"].Text;
                entity.ConversionFactor = decimal.Parse(dataItem["ConversionFactor"].Text);
                if (entity.ConversionFactor == 0) entity.ConversionFactor = 1;
                entity.QuantityFinishInBaseUnit = 0;
                entity.PageNo = 0;
                entity.CostPrice = 0;

                entity.BatchNumber = string.Empty;
                entity.str.ExpiredDate = string.Empty;
                entity.IsPackage = false;
                entity.IsBonusItem = false;
                entity.IsClosed = false;
                entity.Description = dataItem["ItemName"].Text;
                entity.Minimum = dataItem["Minimum"].Text.ToDecimal();
                entity.Maximum = dataItem["Maximum"].Text.ToDecimal();
                entity.Balance = dataItem["Balance"].Text.ToDecimal();
                entity.SRMasterBaseUnit = dataItem["SRItemUnit"].Text;

                if (AppSession.Parameter.IsShowPriceInPurchaseRequest)
                    ProcurementUtils.PopulateWithHistPrice(entity, Request.QueryString["it"], Request.QueryString["suppid"]);
                else
                {
                    entity.Discount1Percentage = 0;
                    entity.Discount2Percentage = 0;
                    entity.Price = 0;
                    entity.Discount = 0;
                }


                //}
            }

            ViewState["RequestOrderDetail" + Request.UserHostName] = null;
            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.command = 'rebind'";
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            bool selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in grdDetail.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }

        protected void btnByStockGroup_Click(object sender, ImageClickEventArgs e)
        {
            InitializeData("StockGroup");
            grdDetail.Rebind();
        }

        protected void btnByAvgSales_Click(object sender, ImageClickEventArgs e)
        {
            InitializeData("SalesHist");
            grdDetail.Rebind();
        }

        protected void btnByStockTotal_Click(object sender, ImageClickEventArgs e)
        {
            InitializeData("StockTotal");
            grdDetail.Rebind();
        }

        protected void cboItemGroupID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            ItemGroupQuery query = new ItemGroupQuery();
            query.es.Top = 10;
            query.Select
            (
                query.ItemGroupID,
                query.ItemGroupName
            );
            query.Where
            (
                query.Or
                (
                    query.ItemGroupID.Like(searchTextContain),
                    query.ItemGroupName.Like(searchTextContain)
                ),
                query.IsActive == true,
                query.SRItemType == Request.QueryString["it"]
            );

            cboItemGroupID.DataSource = query.LoadDataTable();
            cboItemGroupID.DataBind();
        }

        protected void cboItemGroupID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemGroupName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemGroupID"].ToString();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            grdDetail.MasterTableView.ExportToExcel();
        }
    }
}