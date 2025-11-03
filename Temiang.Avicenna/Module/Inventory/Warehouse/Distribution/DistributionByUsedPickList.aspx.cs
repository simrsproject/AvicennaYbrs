using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class DistributionByUsedPickList : BasePageDialog
    {
        private bool _isNeedValidatedMax;

        public int GetInt(object o)
        {
            return o.ToInt();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.Distribution;

            var loc = new Location();
            if (loc.LoadByPrimaryKey(Request.QueryString["tloc"]))
            {
                switch (Request.QueryString["it"])
                {
                    case BusinessObject.Reference.ItemType.Medical:
                        _isNeedValidatedMax = loc.IsValidateMaxValueOnDistReqForIpm ?? false;
                        break;
                    case BusinessObject.Reference.ItemType.NonMedical:
                        _isNeedValidatedMax = loc.IsValidateMaxValueOnDistReqForIpnm ?? false;
                        break;
                    case BusinessObject.Reference.ItemType.Kitchen:
                        _isNeedValidatedMax = loc.IsValidateMaxValueOnDistReqForIk ?? false;
                        break;
                }
            }
            else
                _isNeedValidatedMax = false;

            grdDetail.Columns[8].Visible = !_isNeedValidatedMax;
            grdDetail.Columns[9].Visible = _isNeedValidatedMax;
            trHist1.Visible = !AppSession.Parameter.IsDistributionRequestUsingPickFromUsedHistoryV2;
            trHist2.Visible = AppSession.Parameter.IsDistributionRequestUsingPickFromUsedHistoryV2;

            if (!string.IsNullOrEmpty(Page.Request.QueryString["ig"]))
            {
                var ig = new ItemGroupQuery();
                ig.Select
                (
                    ig.ItemGroupID,
                    ig.ItemGroupName
                );
                ig.Where(ig.ItemGroupID == Page.Request.QueryString["ig"]);

                cboItemGroupID.DataSource = ig.LoadDataTable();
                cboItemGroupID.DataBind();
                cboItemGroupID.SelectedValue = Page.Request.QueryString["ig"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                StoreEntryValue();
            }
            else
            {
                Session.Remove("DistributionPicklist:Selection");
                Session.Remove("DistributionPicklist:ItemSelected");
                var toDay = DateTime.Today;
                txtFromDate.SelectedDate = new DateTime(toDay.Year, 1, 1);
                txtToDate.SelectedDate = toDay;
                txtDivideBy.Value = 1;
            }
        }

        private void StoreEntryValue()
        {
            var dtb = RequestItemSelectionDataTable;
            if (dtb == null) return;
            foreach (GridDataItem dataItem in grdDetail.MasterTableView.Items)
            {
                var itemID = dataItem["ItemID"].Text;
                var row = dtb.Rows.Find(itemID);
                row["IsSelect"] = ((CheckBox)dataItem.FindControl("detailChkbox")).Checked;
                if (_isNeedValidatedMax)
                    row["QtyInput"] = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQtyInput2")).Value ?? 0);
                else
                    row["QtyInput"] = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQtyInput")).Value ?? 0);

                row["FromServiceUnitID"] = Request.QueryString["fsu"].ToString();
                row["FromLocationID"] = Request.QueryString["floc"].ToString();
                row["ToServiceUnitID"] = Request.QueryString["tsu"].ToString();
                row["ToLocationID"] = Request.QueryString["tloc"].ToString();
                row["SRItemType"] = Request.QueryString["it"].ToString();
                row["ItemGroupID"] = Request.QueryString["ig"].ToString();
            }
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDetail.DataSource = RequestItemSelectionDataTable;
        }

        private DataTable LoadRequestItemSelectionDataTable()
        {
            var im = new ItemMovementPerDateQuery("a");
            im.Select(im.ItemID, im.QuantityOut.Sum().As("QtyUse"));
            im.Where(im.MovementDate >= txtFromDate.SelectedDate.Value.Date, im.MovementDate <= txtToDate.SelectedDate.Value.Date,
                im.LocationID == Request.QueryString["tloc"]);
            im.GroupBy(im.ItemID);

            var sqItemId = new esQueryItem(im, "ItemID", esSystemType.String);
            var sqQtyUse = new esQueryItem(im, "QtyUse", esSystemType.Decimal);

            var query = new ItemBalanceQuery("bal");
            query.InnerJoin(query.From(im).As("sq")).On(query.ItemID == sqItemId);

            var qrRef = new AppStandardReferenceItemQuery("c");

            if (Request.QueryString["it"] == BusinessObject.Reference.ItemType.Medical)
            {
                var qrItemMed = new ItemProductMedicQuery("b");
                query.InnerJoin(qrItemMed).On(query.ItemID == qrItemMed.ItemID && qrItemMed.IsConsignment == false);
                query.LeftJoin(qrRef).On(
                    qrItemMed.SRItemUnit == qrRef.ItemID &&
                    qrRef.StandardReferenceID == AppEnum.StandardReference.ItemUnit
                    );

                query.Select(
                    qrItemMed.SRItemUnit,
                    qrItemMed.SRPurchaseUnit,
                    qrItemMed.ConversionFactor
                    );
            }
            else if (Request.QueryString["it"] == BusinessObject.Reference.ItemType.NonMedical)
            {
                var qrItemMed = new ItemProductNonMedicQuery("b");
                query.InnerJoin(qrItemMed).On(query.ItemID == qrItemMed.ItemID && qrItemMed.IsConsignment == false);
                query.LeftJoin(qrRef).On(
                    qrItemMed.SRItemUnit == qrRef.ItemID &&
                    qrRef.StandardReferenceID == AppEnum.StandardReference.ItemUnit
                    );

                query.Select(
                    qrItemMed.SRItemUnit,
                    qrItemMed.SRPurchaseUnit,
                    qrItemMed.ConversionFactor
                    );
            }
            else
            {
                var qrItemMed = new ItemKitchenQuery("b");
                query.InnerJoin(qrItemMed).On(query.ItemID == qrItemMed.ItemID);
                query.LeftJoin(qrRef).On(
                    qrItemMed.SRItemUnit == qrRef.ItemID &&
                    qrRef.StandardReferenceID == AppEnum.StandardReference.ItemUnit
                    );

                query.Select(
                    qrItemMed.SRItemUnit,
                    qrItemMed.SRPurchaseUnit,
                    qrItemMed.ConversionFactor
                    );
            }

            var qrItem = new ItemQuery("d");
            query.InnerJoin(qrItem).On(query.ItemID == qrItem.ItemID && qrItem.IsActive == true);

            var qrBalanceWh = new ItemBalanceQuery("cwb");
            query.LeftJoin(qrBalanceWh).On(qrBalanceWh.LocationID == Request.QueryString["floc"] && qrBalanceWh.ItemID == query.ItemID);
            query.Select(@"<ISNULL(cwb.Balance, 0) AS BalanceFrom>");

            query.Where(
                query.LocationID == Request.QueryString["tloc"]
                );

            var itemGroupID = cboItemGroupID.SelectedValue;
            if (!string.IsNullOrEmpty(itemGroupID))
                query.Where(qrItem.ItemGroupID == itemGroupID);

            var markUp = (txtMarkUp.Value + 100) / 100;
            if (txtDivideBy.Value == 0)
                txtDivideBy.Value = 1;

            query.Select(
                @"<0 AS IsSelect>",
                query.ItemID,
                query.Minimum,
                query.Maximum,
                query.Balance,
                sqQtyUse,
                (query.Maximum - query.Balance).As("QtyMax"),
                (query.Minimum + ((sqQtyUse / txtDivideBy.Value) * markUp) - query.Balance).As("QtyInput"),
                qrItem.ItemName
                );

            if (AppSession.Parameter.IsDistributionRequestOnlyForUnderMinValue)
                query.Where(query.Balance < query.Minimum);

            query.OrderBy(qrItem.ItemName.Ascending);
            var dtb = query.LoadDataTable();

            // Return QtyInput>0
            var dtbSelect = dtb.Clone();
            dtbSelect.PrimaryKey = new[] { dtbSelect.Columns["ItemID"] };
            foreach (DataRow row in dtb.Rows)
            {
                if (Convert.ToDecimal(row["QtyInput"]) > 0)
                {
                    row["QtyInput"] = Convert.ToInt32(row["QtyInput"]);
                    if (_isNeedValidatedMax && Convert.ToDecimal(row["QtyInput"]) > Convert.ToDecimal(row["QtyInput"]))
                        row["QtyInput"] = Convert.ToInt32(row["QtyMax"]);

                    dtbSelect.Rows.Add(row.ItemArray);
                }
            }

            return dtbSelect;
        }

        public override bool OnButtonOkClicked()
        {
            try
            {
                RequestItemSelectedDataTable = RequestItemSelectionDataTable.Select().Where(row => (1.Equals(row["IsSelect"]) &&
                                                  Convert.ToDecimal(row["QtyInput"]) > 0)).CopyToDataTable();
            }
            catch {
                RequestItemSelectedDataTable = null;
            }
            return true;
        }
        private DataTable RequestItemSelectedDataTable
        {
            set
            {
                Session["DistributionPicklist:ItemSelected"] = value;
            }
        }
        private DataTable RequestItemSelectionDataTable
        {
            get
            {
                var result = Session["DistributionPicklist:Selection"];
                if (result == null)
                {
                    return null;
                }
                return (DataTable)result;
            }
            set
            {
                Session["DistributionPicklist:Selection"] = value;
            }
        }
        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            //StoreEntryValue();
            var dtb = RequestItemSelectionDataTable;
            var value = ((CheckBox)sender).Checked;
            foreach (DataRow row in dtb.Rows)
            {
                row["IsSelect"] = value;
            }

            grdDetail.Rebind();
        }

        protected void btnProccess_Click(object sender, EventArgs e)
        {
            RequestItemSelectionDataTable = LoadRequestItemSelectionDataTable();
            grdDetail.Rebind();
        }

        protected void btnByAvgSales_Click(object sender, ImageClickEventArgs e)
        {
            RequestItemSelectionDataTable = InitializeDataFromSalesHist();
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

        private DataTable InitializeDataFromSalesHist()
        {
            //var sales = new ItemSalesPerDateQuery("s");
            var sales = new ItemMovementQuery("s");
            var qrItem = new ItemQuery("d");
            var qrBal = new ItemBalanceQuery("bal");
            var qrRef = new AppStandardReferenceItemQuery("c");
            var qrItemMed = new VwItemProductMedicNonMedicQuery("b");

            sales.InnerJoin(qrItem).On(sales.ItemID == qrItem.ItemID && qrItem.IsActive == true);
            sales.InnerJoin(qrBal).On(sales.ItemID == qrBal.ItemID && sales.LocationID == qrBal.LocationID);
            sales.InnerJoin(qrItemMed).On(sales.ItemID == qrItemMed.ItemID);
            sales.LeftJoin(qrRef).On(
                    qrItemMed.SRItemUnit == qrRef.ItemID &&
                    qrRef.StandardReferenceID == AppEnum.StandardReference.ItemUnit
                    );

            sales.Where(
                sales.LocationID == Request.QueryString["tloc"]
                );

            var itemGroupID = Page.Request.QueryString["ig"];
            if (!string.IsNullOrEmpty(itemGroupID))
                sales.Where(qrItem.ItemGroupID == itemGroupID);

            var dayCount = txtBaseSalesDay.Value.ToInt();
            sales.Select(
                @"<0 AS IsSelect>",
                sales.ItemID,
                qrItem.ItemName,
                qrBal.Minimum,
                qrBal.Maximum,
                qrBal.Balance,
                @"<0 AS QtyMax>",
                @"<0 AS QtyInput>",

                qrItemMed.SRItemUnit,
                qrItemMed.SRPurchaseUnit,
                qrItemMed.ConversionFactor,
                @"<SUM(s.QuantityOut - s.QuantityIn) AS 'AvgSales'>"
                //(sales.QuantityOut.Sum() / dayCount).As("AvgSales")
                );

            var qrBalanceWh = new ItemBalanceQuery("cwb");
            sales.LeftJoin(qrBalanceWh).On(qrBalanceWh.LocationID == Request.QueryString["floc"] && qrBalanceWh.ItemID == sales.ItemID);
            sales.Select(@"<ISNULL(cwb.Balance, 0) AS BalanceFrom>");

            sales.Where(sales.MovementDate > DateTime.Today.AddDays(0 - dayCount), sales.TransactionCode.In("091", "094", "003"));
            sales.Where(qrItem.IsActive == true);

            if (!string.IsNullOrWhiteSpace(cboItemGroupID.SelectedValue))
                sales.Where(qrItem.ItemGroupID == cboItemGroupID.SelectedValue);


            sales.GroupBy(
                @"<s.ItemID>",
                @"<d.ItemName>",
                @"<bal.Minimum>",
                @"<bal.Maximum>",
                @"<bal.Balance>",
                @"<b.SRItemUnit>",
                @"<b.SRPurchaseUnit>",
                @"<b.ConversionFactor>",
                @"<ISNULL(cwb.Balance, 0)>"
                );

            sales.OrderBy(qrItem.ItemName.Ascending);
            var dtb = sales.LoadDataTable();

            // Return QtyInput>0

            var dtbSelect = dtb.Clone();
            dtbSelect.PrimaryKey = new[] { dtbSelect.Columns["ItemID"] };

            foreach (DataRow row in dtb.Rows)
            {
                var qtyUsed = row["AvgSales"].ToDecimal() / dayCount;
                var balanceQty = row["Balance"].ToDecimal() < 0 ? 0 : row["Balance"].ToDecimal();

                var qtyNeed = (qtyUsed * txtForStockDay.Value.ToDecimal()).ToDecimal();
                row["QtyInput"] = qtyNeed - (chkIsIgnoreBalance.Checked ? 0 : balanceQty); // Isi dalam satuan kecil dahulu dan dibawah diconvert jika dalam PU
                row["Maximum"] = qtyNeed; // Isi dgn total kebutuhan dalam satuan kecil krn nilai Max tidak bisa diambil dari setingan max per lokasi (balance nya ditotal)
                row["Balance"] = balanceQty;
                row["Minimum"] = qtyUsed; // Isi dgn Avg Sales //row["AvgSales"].ToInt()
                row["QtyMax"] = qtyNeed - (chkIsIgnoreBalance.Checked ? 0 : balanceQty);

                row["QtyInput"] = Convert.ToInt32(row["QtyInput"]);
                if (_isNeedValidatedMax && Convert.ToDecimal(row["QtyInput"]) > Convert.ToDecimal(row["QtyMax"]))
                    row["QtyInput"] = Convert.ToInt32(row["QtyMax"]);

                if (Convert.ToDecimal(row["QtyInput"]) > 0)
                    dtbSelect.Rows.Add(row.ItemArray);
            }

            return dtbSelect;
        }
    }
}