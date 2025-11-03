using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Inventory.Procurement
{
    public partial class RequestOrderConsignmentList : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["md"]))
            {
                // Redirect to entry page
                // Program entry PO ada beberapa macam shg jika dipanggil dari program lain 
                // hanya bisa memakai url listnya yg disimpan di AppProgram yg kemudian harus di redirect
                Response.Redirect(string.Format("RequestOrderDetail.aspx?{0}", Request.QueryString));
                return;
            }

            base.OnInit(e);
            
            ProgramID = AppConstant.Program.RequestOrderConsignment; 
            
            if (!IsPostBack)
            {
                /*Consignment Transfer List*/
                ComboBox.PopulateWithServiceUnitForTransaction(cboSearchToServiceUnitIDCt, TransactionCode.PurchaseRequest, true);
                ComboBox.PopulateWithItemTypeProduct(cboSRItemTypeCt);

                /*Purchase Request List*/
                txtFromRequestDate.SelectedDate = DateTime.Now;
                txtToRequestDate.SelectedDate = DateTime.Now;

                cboSearchStatus.Items.Add(new RadComboBoxItem("", ""));
                cboSearchStatus.Items.Add(new RadComboBoxItem("Not Approved Yet", "0"));
                cboSearchStatus.Items.Add(new RadComboBoxItem("Approved", "1"));
                cboSearchStatus.Items.Add(new RadComboBoxItem("Still Open", "2"));
                cboSearchStatus.Items.Add(new RadComboBoxItem("Closed", "3"));
                cboSearchStatus.Items.Add(new RadComboBoxItem("Void", "4"));

                ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, TransactionCode.PurchaseOrder, false);
                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
            }
        }

        protected void grdListCt_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdListCt.DataSource = ConsignmnetTransferPendings;
        }

        protected void grdListCt_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            ProcurementUtils.HideColumnStockAndPriceInfo(e.DetailTableView);
            GridDataItem dataItem = e.DetailTableView.ParentItem;

            var query = new ItemTransactionItemQuery("a");
            var iq = new ItemQuery("b");
            query.LeftJoin(iq).On(query.ItemID == iq.ItemID);
            query.Where(query.TransactionNo == dataItem.GetDataKeyValue("TransactionNo").ToString());

            query.OrderBy(query.ItemID.Ascending);

            query.Select
                (
                    query.TransactionNo,
                    query.SequenceNo,
                    query.ItemID,
                    "<ISNULL(b.ItemName, a.Description) AS ItemName>",
                    query.SRItemUnit,
                    query.Quantity,
                    query.Description,
                    query.ConversionFactor,
                    @"<(a.Quantity*a.ConversionFactor) AS QtyTransfer>",
                    query.Price,
                    query.Discount1Percentage,
                    query.Discount2Percentage,
                    query.Discount,
                    query.IsBonusItem,
                    query.IsClosed,
                    @"<(a.QuantityFinishInBaseUnit/a.ConversionFactor) AS QtyFinish>"
                );

            query.Where(query.IsClosed == false);
            var dtb = query.LoadDataTable();

            foreach (DataRow row in dtb.Rows)
            {
                var iti = new ItemTransactionItemQuery("a");
                var it = new ItemTransactionQuery("b");
                iti.InnerJoin(it).On(it.TransactionNo == iti.TransactionNo &&
                                     it.TransactionCode == TransactionCode.PurchaseRequest && it.IsVoid == false);
                iti.Select(iti.ReferenceNo, iti.ReferenceSequenceNo, (iti.Quantity * iti.ConversionFactor).Sum().As("QtyFinished"));
                iti.Where(iti.ReferenceNo == row["TransactionNo"].ToString(), iti.ReferenceSequenceNo == row["SequenceNo"].ToString());
                iti.GroupBy(iti.ReferenceNo, iti.ReferenceSequenceNo);
                DataTable dtbd = iti.LoadDataTable();
                if (dtbd.Rows.Count > 0)
                {
                    if (Convert.ToDouble(row["QtyTransfer"]) <= Convert.ToDouble(dtbd.Rows[0]["QtyFinished"]))
                        row.Delete();
                    else
                        row["QtyFinish"] = Convert.ToDouble(dtbd.Rows[0]["QtyFinished"]) /
                                           Convert.ToDouble(row["ConversionFactor"]);
                }
            }
            dtb.AcceptChanges();

            e.DetailTableView.DataSource = dtb;
        }

        private DataTable ConsignmnetTransferPendings
        {
            get
            {
                var query = new ItemTransactionQuery("a");
                var itype = new AppStandardReferenceItemQuery("d");
                var supp = new SupplierQuery("s");
                var floc = new LocationQuery("floc");
                var tsu = new ServiceUnitQuery("c");
                var tloc = new LocationQuery("tloc");
                var usr = new AppUserServiceUnitQuery("usr");

                query.LeftJoin(itype).On(itype.ItemID == query.SRItemType && itype.StandardReferenceID == "ItemType");
                query.InnerJoin(supp).On(query.BusinessPartnerID == supp.SupplierID);
                query.InnerJoin(floc).On(query.FromLocationID == floc.LocationID);
                query.InnerJoin(tsu).On(tsu.ServiceUnitID == query.ToServiceUnitID);
                query.InnerJoin(tloc).On(query.ToLocationID == tloc.LocationID);
                query.InnerJoin(usr).On(query.ToServiceUnitID == usr.ServiceUnitID &&
                                        usr.UserID == AppSession.UserLogin.UserID);

                query.Select(
                       query.TransactionNo,
                       query.TransactionDate,
                       itype.ItemName.As("ItemType"),
                       supp.SupplierName,
                       floc.LocationName.As("FromLocationName"),
                       tsu.ServiceUnitName.As("ToServiceUnitName"),
                       tloc.LocationName.As("ToLocationName"),
                       query.Notes,
                       query.IsApproved,
                       query.IsVoid,
                       "<'RequestOrderDetail.aspx?md=new&id=&pr=' + a.TransactionNo + '&cons=1' AS PrUrl>"
                   );

                query.Where(query.TransactionCode == TransactionCode.ConsignmentTransfer, query.IsApproved == true,
                            query.IsClosed == false);

                if (!txtTransferDate.IsEmpty)
                    query.Where(query.TransactionDate == txtTransferDate.SelectedDate);
                if (!string.IsNullOrEmpty(txtTransferNo.Text))
                {
                    string searchTextContain = string.Format("%{0}%", txtTransferNo.Text);
                    query.Where(query.TransactionNo.Like(searchTextContain));
                }
                if (!string.IsNullOrEmpty(cboSearchSupplierIDCt.SelectedValue))
                    query.Where(query.BusinessPartnerID == cboSearchSupplierIDCt.SelectedValue);
                if (!string.IsNullOrEmpty(cboSearchToServiceUnitIDCt.SelectedValue))
                    query.Where(query.ToServiceUnitID == cboSearchToServiceUnitIDCt.SelectedValue);
                if (!string.IsNullOrEmpty(cboSRItemTypeCt.SelectedValue))
                    query.Where(query.SRItemType == cboSRItemTypeCt.SelectedValue);

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.OrderBy(query.TransactionDate.Ascending, query.TransactionNo.Ascending);

                var dtb = query.LoadDataTable();
                foreach (DataRow row in dtb.Rows)
                {
                    bool isRowDelete = false;
                    var iti = new ItemTransactionItemQuery("a");
                    var itiR = new ItemTransactionItemQuery("ar");
                    var itR = new ItemTransactionQuery("br");
                    iti.LeftJoin(itiR).On(
                        iti.TransactionNo == itiR.ReferenceNo && iti.SequenceNo == itiR.ReferenceSequenceNo);
                    iti.LeftJoin(itR).On(itR.TransactionNo == itiR.TransactionNo);
                    iti.Select(iti.TransactionNo, iti.SequenceNo, iti.IsClosed,
                               (iti.Quantity * iti.ConversionFactor).As("QtyTransfer"),
                               "<ISNULL(SUM(CASE ISNULL(br.IsVoid, 0) WHEN 0 THEN (ar.[Quantity]*ar.[ConversionFactor]) ELSE 0 END), 0) AS 'QtyFinished'>");
                    iti.Where(iti.TransactionNo == row["TransactionNo"].ToString());
                    iti.GroupBy(iti.TransactionNo, iti.SequenceNo, iti.IsClosed, iti.Quantity, iti.ConversionFactor);
                    DataTable dtbd = iti.LoadDataTable();

                    if (dtbd.Rows.Count > 0)
                    {
                        if (!dtbd.Rows.Cast<DataRow>().Any(d => Convert.ToBoolean(d["IsClosed"]) == false && Convert.ToDouble(d["QtyTransfer"]) > Convert.ToDouble(d["QtyFinished"])))
                        {
                            isRowDelete = true;
                        }
                    }

                    if (isRowDelete)
                        row.Delete();
                }
                dtb.AcceptChanges();

                return dtb;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = ItemTransactions;
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            ProcurementUtils.HideColumnStockAndPriceInfo(e.DetailTableView);

            //Load record
            var query = new ItemTransactionItemQuery("a");
            var iq = new ItemQuery("b");

            query.Select
                (
                    query.TransactionNo,
                    query.SequenceNo,
                    query.ItemID,
                    query.SRItemUnit,
                    @"<ISNULL((SELECT SUM(iti.Quantity*iti.ConversionFactor) AS QuantityFinishInBaseUnit 
                        FROM ItemTransactionItem iti
                        INNER JOIN ItemTransaction it ON it.TransactionNo = iti.TransactionNo AND it.TransactionCode = '034' AND it.IsVoid = 0
                        WHERE iti.ReferenceNo = a.TransactionNo AND iti.ReferenceSequenceNo = a.SequenceNo 
                        GROUP BY iti.ReferenceNo, iti.ReferenceSequenceNo
                    ), 0) / a.ConversionFactor AS QuantityFinishInBaseUnit>",
                    @"<ISNULL((SELECT SUM(iti.QuantityFinishInBaseUnit) AS QuantityFinishInBaseUnit 
                        FROM ItemTransactionItem iti
                        INNER JOIN ItemTransaction it ON it.TransactionNo = iti.TransactionNo AND it.TransactionCode = '034' AND it.IsVoid = 0
                        WHERE iti.ReferenceNo = a.TransactionNo AND iti.ReferenceSequenceNo = a.SequenceNo
                        GROUP BY iti.ReferenceNo, iti.ReferenceSequenceNo
                    ), 0) / a.ConversionFactor AS QuantityReceived>",
                    query.ConversionFactor,
                    query.IsClosed,
                    query.Description,
                    iq.ItemName.As("ItemName"),
                    query.Price,
                    query.Discount1Percentage,
                    query.Discount2Percentage,
                    query.Discount
                );

            if (AppSession.Parameter.IsUsingApprovalPurchaseRequest)
            {
                query.Select(
                    @"<CASE WHEN a.RequestQty IS NULL THEN a.Quantity ELSE a.RequestQty END AS Quantity>",
                    @"<CASE WHEN a.RequestQty IS NULL THEN 0 ELSE a.Quantity END AS QtyApproved>");
            }
            else
            {
                query.Select(query.Quantity, query.Quantity.As("QtyApproved"));
            }

            // Base Unit
            var ipnmq = new ItemProductNonMedicQuery("i2");
            var ikq = new ItemKitchenQuery("i2");
            var ipmq = new ItemProductMedicQuery("i2");
            var itemType = e.DetailTableView.ParentItem.GetDataKeyValue("SRItemType").ToString();

            switch (itemType)
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
            var locationID = e.DetailTableView.ParentItem.GetDataKeyValue("FromLocationID").ToString();
            if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPorByStockGroup))
            {
                var stockGroup = string.Empty;
                var ibbsgq = new ItemBalanceByStockGroupQuery("c");
                var loc = new Location();
                loc.LoadByPrimaryKey(locationID);
                if (!string.IsNullOrEmpty(loc.SRStockGroup))
                    stockGroup = loc.SRStockGroup;
                query.LeftJoin(ibbsgq).On(query.ItemID == ibbsgq.ItemID && ibbsgq.SRStockGroup == stockGroup);


                var ibq = new ItemBalanceQuery("bl");
                query.LeftJoin(ibq).On(query.ItemID == ibq.ItemID && ibq.LocationID == locationID);


                query.Select(@"<CONVERT(decimal(10,2),COALESCE(c.Balance,0)) AS BalanceSG>",
                    @"<CONVERT(decimal(10,2),COALESCE(bl.Balance,0)) AS Balance>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Minimum,0)) AS Minimum>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Maximum,0)) AS Maximum>",
                    @"<i2.SRItemUnit AS SRMasterBaseUnit>"
                    );
            }
            else
            {
                locationID = ProcurementUtils.LocationIdByItemType(itemType);
                var ibq = new ItemBalanceQuery("c");
                if (string.IsNullOrEmpty(locationID))
                    locationID = "ABCD_EFG";
                query.LeftJoin(ibq).On(query.ItemID == ibq.ItemID && ibq.LocationID == locationID);

                query.Select(@"<CONVERT(decimal(10,2),0) AS BalanceSG>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Balance,0)) AS Balance>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Minimum,0)) AS Minimum>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Maximum,0)) AS Maximum>",
                    @"<i2.SRItemUnit AS SRMasterBaseUnit>"
                    );
            }
            // Sub Query BalanceTotal
            var itemBalTot = new ItemBalanceQuery("ibt");
            itemBalTot.Select((itemBalTot.Balance.Sum().As("BalanceTotal")));
            itemBalTot.Where(itemBalTot.ItemID == query.ItemID);
            query.Select(itemBalTot.Select().As("BalanceTotal"));

            query.LeftJoin(iq).On(query.ItemID == iq.ItemID);
            var trNo = e.DetailTableView.ParentItem.GetDataKeyValue("TransactionNo").ToString();
            query.Where(query.TransactionNo == trNo);
            query.OrderBy(query.ItemID.Ascending);

            //Apply
            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        private DataTable ItemTransactions
        {
            get
            {
                var query = new ItemTransactionQuery("a");
                var qryserviceunit = new ServiceUnitQuery("b");
                var qryserviceunitto = new ServiceUnitQuery("c");
                var itemtype = new AppStandardReferenceItemQuery("d");
                var user = new AppUserServiceUnitQuery("e");
                var costunit = new ServiceUnitQuery("cu");

                query.Select
                    (
                        query.TransactionNo,
                        query.TransactionDate,
                        qryserviceunit.ServiceUnitName.As("FServiceUnitID"),
                        qryserviceunitto.ServiceUnitName.As("TServiceUnitID"),
                        costunit.ServiceUnitName.As("CostUnit"),
                        itemtype.ItemName,
                        query.IsInventoryItem,
                        query.IsApproved,
                        query.IsClosed,
                        query.Notes,
                        query.IsVoid,
                        query.FromLocationID,
                        query.SRItemType,
                        "<'RequestOrderDetail.aspx?md=view&id=' + a.TransactionNo + '&cons=1' AS PrUrl>"
                    );

                query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
                query.InnerJoin(qryserviceunitto).On(qryserviceunitto.ServiceUnitID == query.ToServiceUnitID);
                query.InnerJoin(costunit).On(costunit.ServiceUnitID == query.ServiceUnitCostID);
                query.LeftJoin(itemtype).On
                    (
                        itemtype.ItemID == query.SRItemType &&
                        itemtype.StandardReferenceID == "ItemType"
                    );
                query.InnerJoin(user).On(user.ServiceUnitID == query.FromServiceUnitID & user.UserID == AppSession.UserLogin.UserID);
                query.Where
                    (
                        query.TransactionCode == TransactionCode.PurchaseRequest,
                        query.IsConsignmentAlreadyReceived.IsNotNull(),
                        query.IsConsignmentAlreadyReceived == true
                    );

                if (!txtFromRequestDate.IsEmpty)
                    query.Where(query.TransactionDate >= txtFromRequestDate.SelectedDate);
                if (!txtToRequestDate.IsEmpty)
                    query.Where(query.TransactionDate <= txtToRequestDate.SelectedDate);
                if (!string.IsNullOrEmpty(txtRequestNo.Text))
                {
                    string searchTextContain = string.Format("%{0}%", txtRequestNo.Text);
                    query.Where(query.TransactionNo.Like(searchTextContain));
                }
                if (!string.IsNullOrEmpty(txtReferenceNo.Text))
                {
                    string searchTextContain = string.Format("%{0}%", txtReferenceNo.Text);
                    query.Where(query.ReferenceNo.Like(searchTextContain));
                }
                if (cboSearchStatus.SelectedValue == "0")
                    query.Where(query.IsApproved == false);
                if (cboSearchStatus.SelectedValue == "1")
                    query.Where(query.IsApproved == true);
                if (cboSearchStatus.SelectedValue == "2")
                    query.Where(query.IsClosed == false);
                if (cboSearchStatus.SelectedValue == "3")
                    query.Where(query.IsClosed == true);
                if (cboSearchStatus.SelectedValue == "4")
                    query.Where(query.IsVoid == true);
                if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
                    query.Where(query.ToServiceUnitID == cboToServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(cboSearchSupplierID.SelectedValue))
                    query.Where(query.BusinessPartnerID == cboSearchSupplierID.SelectedValue);
                if (!string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                    query.Where(query.SRItemType == cboSRItemType.SelectedValue);

                query.OrderBy(query.TransactionDate.Descending, query.TransactionNo.Descending);

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }

        protected void btnFilterCT_Click(object sender, ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdListCt.Rebind();
        }

        protected void btnFilterPR_Click(object sender, ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
        }

        protected void cboSearchSupplierID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var supp = new SupplierQuery("a");
            supp.es.Top = 10;
            supp.Where(
                supp.SupplierName.Like(searchTextContain),
                supp.IsActive == true
                );

            cboSearchSupplierID.DataSource = supp.LoadDataTable();
            cboSearchSupplierID.DataBind();
        }

        protected void cboSearchSupplierID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SupplierName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SupplierID"].ToString();
        }

        protected void cboSearchSupplierIDCt_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var supp = new SupplierQuery("a");
            supp.es.Top = 10;
            supp.Where(
                supp.SupplierName.Like(searchTextContain),
                supp.IsActive == true
                );

            cboSearchSupplierIDCt.DataSource = supp.LoadDataTable();
            cboSearchSupplierIDCt.DataBind();
        }

        protected void cboSearchSupplierIDCt_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SupplierName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SupplierID"].ToString();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }

        protected override void InitializeControlFromCookie(Control ctl, object value)
        {
            if (ctl.ID.ToLower().Equals(cboSearchSupplierID.ID.ToLower()) && value != null)
            {
                var query = new SupplierQuery();
                query.es.Top = 1;
                query.Select
                    (
                        query.SupplierID,
                        query.SupplierName
                    );
                query.Where(query.SupplierID == value);

                cboSearchSupplierID.DataSource = query.LoadDataTable();
                cboSearchSupplierID.DataBind();
            }

            if (ctl.ID.ToLower().Equals(cboSearchSupplierIDCt.ID.ToLower()) && value != null)
            {
                var query = new SupplierQuery();
                query.es.Top = 1;
                query.Select
                    (
                        query.SupplierID,
                        query.SupplierName
                    );
                query.Where(query.SupplierID == value);

                cboSearchSupplierIDCt.DataSource = query.LoadDataTable();
                cboSearchSupplierIDCt.DataBind();
            }
        }
    }
}
