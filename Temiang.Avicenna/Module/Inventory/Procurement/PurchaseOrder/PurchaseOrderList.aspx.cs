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
    public partial class PurchaseOrderList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected override void OnInit(EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["md"]))
            {
                // Redirect to entry page
                // Program entry PO ada beberapa macam shg jika dipanggil dari program lain 
                // hanya bisa memakai url listnya yg disimpan di AppProgram yg kemudian harus di redirect
                Response.Redirect(string.Format("PurchaseOrderDetail.aspx?{0}", Request.QueryString));
                return;
            }

            base.OnInit(e); 
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = string.IsNullOrEmpty(Request.QueryString["cons"]) || Request.QueryString["cons"] == "0"
                            ? AppConstant.Program.PurchaseOrder
                            : AppConstant.Program.PurchaseOrderConsignment;
            if (!string.IsNullOrEmpty(Request.QueryString["suptype"]))
            {
                ProgramID = AppConstant.Program.PurchaseOrderFilteredBySupplierType;
            }

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboSearchFromUnit, BusinessObject.Reference.TransactionCode.PurchaseRequest, false);
                ComboBox.PopulateWithServiceUnitForTransaction(cboSearchToUnit, BusinessObject.Reference.TransactionCode.PurchaseOrder, true);

                ComboBox.PopulateWithServiceUnitForTransaction(cboPurchasingUnitID, BusinessObject.Reference.TransactionCode.PurchaseOrder, true);

                cboSearchStatus.Items.Add(new RadComboBoxItem("", ""));
                cboSearchStatus.Items.Add(new RadComboBoxItem("Not Approved Yet", "0"));
                cboSearchStatus.Items.Add(new RadComboBoxItem("Approved", "1"));
                cboSearchStatus.Items.Add(new RadComboBoxItem("Still Open", "2"));
                cboSearchStatus.Items.Add(new RadComboBoxItem("Closed", "3"));
                cboSearchStatus.Items.Add(new RadComboBoxItem("Void", "4"));

                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
                ComboBox.PopulateWithItemTypeProduct(cboSRItemTypePr);
                ComboBox.PopulateWithServiceUnitForTransaction(cboPurchasingUnitID, BusinessObject.Reference.TransactionCode.PurchaseOrder, true);

                txtFromDate.SelectedDate = DateTime.Now;
                txtToDate.SelectedDate = DateTime.Now;

                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB")
                {
                    txtRequestDate.SelectedDate = DateTime.Now.Date;
                }
            }
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = ItemTransactions;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            ProcurementUtils.HideColumnStockInfo(e.DetailTableView);
            GridDataItem dataItem = e.DetailTableView.ParentItem;

            //Load record
            var query = new ItemTransactionItemQuery("a");
            var iq = new ItemQuery("b");
            var itiref = new ItemTransactionItemQuery("itr");
            query.Select(
                query.TransactionNo,
                query.ItemID,
                query.SRItemUnit,
                query.Quantity,
                query.QuantityFinishInBaseUnit,
                query.SequenceNo,
                query.Price,
                query.Discount1Percentage,
                query.Discount2Percentage,
                query.Discount,
                query.Discount,
                query.IsBonusItem,
                query.IsClosed,
                query.Description,
                query.ConversionFactor,
                @"<(ISNULL(itr.Quantity, 0) * ISNULL(itr.ConversionFactor, 0))/a.ConversionFactor AS QtyRequest>",
                @"<(a.QuantityFinishInBaseUnit/a.ConversionFactor) AS QuantityFinish>"
            );
            query.LeftJoin(iq).On(query.ItemID == iq.ItemID);
            query.LeftJoin(itiref).On(itiref.TransactionNo == query.ReferenceNo &&
                                      itiref.SequenceNo == query.ReferenceSequenceNo);

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
            var locationID = string.Empty;
            if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPorByStockGroup))
            {
                locationID = e.DetailTableView.ParentItem.GetDataKeyValue("PorLocationID").ToString();
                if (string.IsNullOrEmpty(locationID))
                {
                    query.Select(@"<CONVERT(decimal(10,2),0) AS BalanceSG>",
                        @"<CONVERT(decimal(10,2),0) AS Balance>",
                        @"<CONVERT(decimal(10,2),0) AS Minimum>",
                        @"<CONVERT(decimal(10,2),0) AS Maximum>",
                        @"<i2.SRItemUnit AS SRMasterBaseUnit>"
                        );
                }
                else
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

            query.Where(query.TransactionNo == dataItem.GetDataKeyValue("TransactionNo").ToString());
            if (!string.IsNullOrEmpty(cboItemGroup.SelectedValue))
                query.Where(iq.ItemGroupID == cboItemGroup.SelectedValue);

            query.OrderBy(query.ItemID.Ascending);

            DataTable dtb = query.LoadDataTable();

            //Apply
            e.DetailTableView.DataSource = dtb;
        }

        private DataTable ItemTransactions
        {
            get
            {
                var isEmptyFilter = txtFromDate.IsEmpty && txtToDate.IsEmpty && string.IsNullOrEmpty(txtPo.Text) && string.IsNullOrEmpty(txtRo.Text) && string.IsNullOrEmpty(cboSearchStatus.SelectedValue) && string.IsNullOrEmpty(cboPurchasingUnitID.SelectedValue) && string.IsNullOrEmpty(cboSearchSupplier.SelectedValue) && string.IsNullOrEmpty(cboSRItemType.SelectedValue) && string.IsNullOrEmpty(cboItemGroup.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Purchase Order")) return null;

                var query = new ItemTransactionQuery("a");
                var sup = new SupplierQuery("b");
                var qryserviceunit = new ServiceUnitQuery("c");
                var itemtype = new AppStandardReferenceItemQuery("d");
                var poType = new AppStandardReferenceItemQuery("s");
                var user = new AppUserServiceUnitQuery("u");

                query.InnerJoin(poType).On(
                    query.SRPurchaseOrderType == poType.ItemID &&
                    poType.StandardReferenceID == AppEnum.StandardReference.PurchaseOrderType
                    );
                query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
                query.LeftJoin(itemtype).On(
                    itemtype.ItemID == query.SRItemType &&
                    itemtype.StandardReferenceID == AppEnum.StandardReference.ItemType
                    );
                query.InnerJoin(user).On(query.FromServiceUnitID == user.ServiceUnitID &&
                                         user.UserID == AppSession.UserLogin.UserID);
                query.Where(query.TransactionCode == TransactionCode.PurchaseOrder);
                query.Select(
                       query.TransactionNo,
                       query.TransactionDate,
                       sup.SupplierName,
                       query.ReferenceNo,
                       qryserviceunit.ServiceUnitName.As("FServiceUnitID"),
                       itemtype.ItemName,
                       query.IsApproved,
                       query.IsClosed,
                       query.Notes,
                       query.IsVoid,
                       poType.ItemName.As("PurchaseOrderType"),
                       query.SRItemType,
                       query.DeliveryOrdersDate,
                       //Request.QueryString["cons"] == "0"
                       //     ? "<'PurchaseOrderDetail.aspx?md=view&id=' + a.TransactionNo + '&pr=&rop=0&cons=0' AS PoUrl>"
                       //     : "<'PurchaseOrderDetail.aspx?md=view&id=' + a.TransactionNo + '&pr=&rop=0&cons=1' AS PoUrl>",
                       "<'PurchaseOrderDetail.aspx?md=view&id=' + a.TransactionNo + '&pr=&rop=0&" + Request.QueryString + "' AS PoUrl>",
                       "<'../RequestOrder/RequestOrderDetail.aspx?md=view&id=' + a.ReferenceNo AS PrUrl>"
                       );

                // Sub Query
                var por = new ItemTransactionQuery("por");
                por.Select(por.FromLocationID);
                por.Where(por.TransactionNo == query.ReferenceNo);
                query.Select(por.Select().As("PorLocationID"));

                if (!txtFromDate.IsEmpty && !txtToDate.IsEmpty)
                {
                    if (rblFilterDate.SelectedValue == "OD")
                        query.Where(query.TransactionDate.Between(txtFromDate.SelectedDate, txtToDate.SelectedDate));
                    else
                        query.Where(query.DeliveryOrdersDate.Between(txtFromDate.SelectedDate, txtToDate.SelectedDate));
                }
                if (!string.IsNullOrEmpty(cboSearchSupplier.SelectedValue))
                    query.Where(query.BusinessPartnerID == cboSearchSupplier.SelectedValue);
                if (!string.IsNullOrEmpty(cboPurchasingUnitID.SelectedValue))
                    query.Where(query.FromServiceUnitID == cboPurchasingUnitID.SelectedValue);
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
                if (cboSRItemType.SelectedValue != string.Empty)
                    query.Where(query.SRItemType == cboSRItemType.SelectedValue);
                if (!string.IsNullOrEmpty(txtPo.Text))
                {
                    string searchTextContain = string.Format("%{0}%", txtPo.Text);
                    query.Where(query.TransactionNo.Like(searchTextContain));
                }
                if (!string.IsNullOrEmpty(txtRo.Text))
                {
                    string searchTextContain = string.Format("%{0}%", txtRo.Text);
                    query.Where(query.ReferenceNo.Like(searchTextContain));
                }
                if (Request.QueryString["cons"] == "1")
                    query.Where(query.IsConsignment == true);

                if (!string.IsNullOrEmpty(cboItemGroup.SelectedValue))
                {
                    var itiq = new ItemTransactionItemQuery("iti");
                    var iq = new ItemQuery("i");
                    query.InnerJoin(itiq).On(itiq.TransactionNo == query.TransactionNo);
                    query.InnerJoin(iq).On(iq.ItemID == itiq.ItemID);
                    query.Where(iq.ItemGroupID == cboItemGroup.SelectedValue);
                    query.GroupBy(query.TransactionNo,
                                  query.TransactionDate,
                                  sup.SupplierName,
                                  query.ReferenceNo,
                                  qryserviceunit.ServiceUnitName,
                                  itemtype.ItemName,
                                  query.IsApproved,
                                  query.IsClosed,
                                  query.Notes,
                                  query.IsVoid,
                                  poType.ItemName,
                                  query.SRItemType,
                                  query.DeliveryOrdersDate);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.OrderBy(query.TransactionDate.Descending, query.TransactionNo.Descending);

                // Filter supplier type
                if (!string.IsNullOrEmpty(Request.QueryString["suptype"]))
                {
                    query.InnerJoin(sup)
                        .On(query.BusinessPartnerID == sup.SupplierID &&
                            sup.SRSupplierType == Request.QueryString["suptype"]);
                }
                else
                {
                    query.LeftJoin(sup).On(query.BusinessPartnerID == sup.SupplierID);
                }

                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        protected void grdListPr_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = PurchaseRequestPendings;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
        }

        protected void grdListPr_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            ProcurementUtils.HideColumnStockAndPriceInfo(e.DetailTableView);
            GridDataItem dataItem = e.DetailTableView.ParentItem;

            var query = new ItemTransactionItemQuery("a");
            var iq = new ItemQuery("b");
            query.LeftJoin(iq).On(query.ItemID == iq.ItemID);
            query.Where(query.TransactionNo == dataItem.GetDataKeyValue("TransactionNo").ToString());

            if (AppSession.Parameter.IsUsingApprovalPurchaseRequest)
                query.Where(query.RequestQty.IsNotNull(), query.Quantity > 0);

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
                    @"<(a.Quantity*a.ConversionFactor) AS QtyRequest>",
                    query.Price,
                    query.Discount1Percentage,
                    query.Discount2Percentage,
                    query.Discount,
                    query.IsBonusItem,
                    query.IsClosed,
                    @"<(a.QuantityFinishInBaseUnit/a.ConversionFactor) AS QtyFinish>"
                );

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

            query.Where(query.IsClosed == false);
            var dtb = query.LoadDataTable();

            if (AppSession.Parameter.IsPrOutstandingListBasedOnCalcQtyOrder)
            {
                foreach (DataRow row in dtb.Rows)
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
                            row["QtyFinish"] = Convert.ToDouble(dtbd.Rows[0]["QtyFinished"]) /
                                               Convert.ToDouble(row["ConversionFactor"]);
                    }
                }
                dtb.AcceptChanges();
            }

            e.DetailTableView.DataSource = dtb;
        }

        private DataTable PurchaseRequestPendings
        {
            get
            {
                var isEmptyFilter = txtRequestDate.IsEmpty && string.IsNullOrEmpty(txtRequestNo.Text) && string.IsNullOrEmpty(cboSearchFromUnit.SelectedValue) && string.IsNullOrEmpty(cboSearchToUnit.SelectedValue) && string.IsNullOrEmpty(cboSRItemTypePr.SelectedValue) && string.IsNullOrEmpty(cboItemGroupPr.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Purchase Order")) return null;

                var query = new ItemTransactionQuery("a");
                var qryserviceunit = new ServiceUnitQuery("b");
                var qrItem = new ItemTransactionItemQuery("c");
                var itemtype = new AppStandardReferenceItemQuery("e");
                var tsu = new ServiceUnitQuery("d");
                var user = new AppUserServiceUnitQuery("u");
                //var itiq = new ItemTransactionItemQuery("iti");
                var sup = new SupplierQuery("sup");

                query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
                query.InnerJoin(qrItem).On(query.TransactionNo == qrItem.TransactionNo);
                query.LeftJoin(itemtype).On(
                    itemtype.ItemID == query.SRItemType &&
                    itemtype.StandardReferenceID == AppEnum.StandardReference.ItemType
                    );
                query.InnerJoin(tsu).On(tsu.ServiceUnitID == query.ToServiceUnitID);
                query.InnerJoin(user).On(query.ToServiceUnitID == user.ServiceUnitID &&
                                         user.UserID == AppSession.UserLogin.UserID);
                //query.InnerJoin(itiq).On(itiq.TransactionNo == query.TransactionNo);
                query.LeftJoin(sup).On(query.BusinessPartnerID == sup.SupplierID);

                query.Select
                    (
                        query.TransactionNo,
                        query.TransactionDate,
                        query.FromLocationID,
                        query.SRItemType,
                        itemtype.ItemName,
                        qryserviceunit.ServiceUnitName.As("FromServiceUnit"),
                        tsu.ServiceUnitName.As("ToServiceUnit"),
                        query.Notes,
                        //(itiq.Quantity * itiq.ConversionFactor).Sum().As("QtyRequest"),
                        (qrItem.Quantity * qrItem.ConversionFactor).Sum().As("QtyRequest"),
                        Request.QueryString["cons"] == "0"
                            ? "<'PurchaseOrderDetail.aspx?md=new&id=&pr=' + a.TransactionNo + '&cons=0' AS PoUrl>"
                            : "<'PurchaseOrderDetail.aspx?md=new&id=&pr=' + a.TransactionNo + '&cons=1' AS PoUrl>",
                        sup.SupplierName, query.ApprovedByUserID, query.ApprovedDate
                    );

                query.Where
                (
                    query.TransactionCode == TransactionCode.PurchaseRequest,
                    query.IsApproved == true,
                    qrItem.IsClosed == false
                );

                query.GroupBy(query.TransactionNo,
                              query.TransactionDate,
                              query.FromLocationID,
                              query.SRItemType,
                              itemtype.ItemName,
                              qryserviceunit.ServiceUnitName,
                              tsu.ServiceUnitName,
                              query.Notes,
                              sup.SupplierName,
                              query.ApprovedByUserID,
                              query.ApprovedDate);

                if (AppSession.Parameter.IsUsingApprovalPurchaseRequest)
                    query.Where(qrItem.RequestQty.IsNotNull(), qrItem.Quantity > 0);

                if (!string.IsNullOrEmpty(txtRequestNo.Text))
                {
                    string searchTextContain = string.Format("%{0}%", txtRequestNo.Text);
                    query.Where(query.TransactionNo.Like(searchTextContain));
                }
                if (!txtRequestDate.IsEmpty)
                    query.Where(query.TransactionDate == txtRequestDate.SelectedDate);
                if (!string.IsNullOrEmpty(cboSearchFromUnit.SelectedValue))
                    query.Where(query.FromServiceUnitID == cboSearchFromUnit.SelectedValue);
                if (!string.IsNullOrEmpty(cboSearchToUnit.SelectedValue))
                    query.Where(query.ToServiceUnitID == cboSearchToUnit.SelectedValue);
                if (!string.IsNullOrEmpty(cboSRItemTypePr.SelectedValue))
                    query.Where(query.SRItemType == cboSRItemTypePr.SelectedValue);
                if (!string.IsNullOrEmpty(cboItemGroupPr.SelectedValue))
                {
                    var itm = new ItemQuery("itm");
                    query.InnerJoin(itm).On(itm.ItemID == qrItem.ItemID);
                    query.Where(itm.ItemGroupID == cboItemGroupPr.SelectedValue);
                }
                    
                if (Request.QueryString["cons"] == "1")
                    query.Where(query.IsConsignment == true);

                //query.es.Distinct = true;

                //query.es.Top = AppSession.Parameter.MaxResultRecord;
                var orderBy = AppSession.Parameter.PurchaseRequestOutstandingListOrderBy[0].ToLower();
                var sortingBy = AppSession.Parameter.PurchaseRequestOutstandingListOrderBy[1].ToLower();

                if (orderBy == "transdate")
                {
                    if (sortingBy == "asc")
                        query.OrderBy(query.TransactionDate.Ascending, query.TransactionNo.Ascending);
                    else
                        query.OrderBy(query.TransactionDate.Descending, query.TransactionNo.Descending);
                }
                else if (orderBy == "apprdate")
                {
                    if (sortingBy == "asc")
                        query.OrderBy(query.ApprovedDate.Ascending, query.TransactionNo.Ascending);
                    else
                        query.OrderBy(query.ApprovedDate.Descending, query.TransactionNo.Descending);
                }
                else //default
                    query.OrderBy(query.TransactionDate.Ascending, query.TransactionNo.Ascending);

                //if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSPP")
                //{
                //    query.OrderBy(query.TransactionDate.Descending, query.TransactionNo.Descending);
                //}
                //else
                //{
                //    query.OrderBy(query.TransactionDate.Ascending, query.TransactionNo.Ascending);
                //}

                // Filter supplier type
                if (!string.IsNullOrEmpty(Request.QueryString["suptype"]))
                {
                    var sup2 = new SupplierQuery("sup2");
                    query.InnerJoin(sup2)
                        .On(query.BusinessPartnerID == sup2.SupplierID &&
                            sup2.SRSupplierType == Request.QueryString["suptype"]);
                }

                var dtb = query.LoadDataTable();
                if (AppSession.Parameter.IsPrOutstandingListBasedOnCalcQtyOrder)
                {
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
                                   (iti.Quantity * iti.ConversionFactor).As("QtyRequest"),
                                   //(itiR.Quantity * itiR.ConversionFactor).Sum().As("QtyFinished"));
                                   "<ISNULL(SUM(CASE ISNULL(br.IsVoid, 0) WHEN 0 THEN (ar.[Quantity]*ar.[ConversionFactor]) ELSE 0 END), 0) AS 'QtyFinished'>");
                        iti.Where(iti.TransactionNo == row["TransactionNo"].ToString());
                        iti.GroupBy(iti.TransactionNo, iti.SequenceNo, iti.IsClosed, iti.Quantity, iti.ConversionFactor);
                        DataTable dtbd = iti.LoadDataTable();

                        if (dtbd.Rows.Count > 0)
                        {
                            if (!dtbd.Rows.Cast<DataRow>().Any(d => Convert.ToBoolean(d["IsClosed"]) == false && Convert.ToDouble(d["QtyRequest"]) > Convert.ToDouble(d["QtyFinished"])))
                            {
                                isRowDelete = true;
                            }
                        }

                        //bool isRowDelete = true;
                        //var iti = new ItemTransactionItemQuery("a");
                        //var itiR = new ItemTransactionItemQuery("ar");
                        //var itR = new ItemTransactionQuery("br");
                        //iti.InnerJoin(itiR).On(
                        //    iti.TransactionNo == itiR.ReferenceNo && iti.SequenceNo == itiR.ReferenceSequenceNo);
                        //iti.InnerJoin(itR).On(itR.TransactionNo == itiR.TransactionNo && itR.IsVoid == false);
                        //iti.Select(iti.TransactionNo, iti.SequenceNo, iti.IsClosed,
                        //           (iti.Quantity * iti.ConversionFactor).As("QtyRequest"),
                        //           (itiR.Quantity * itiR.ConversionFactor).Sum().As("QtyFinished"));
                        //iti.Where(iti.TransactionNo == row["TransactionNo"].ToString());
                        //iti.GroupBy(iti.TransactionNo, iti.SequenceNo, iti.IsClosed, iti.Quantity, iti.ConversionFactor);
                        //DataTable dtbd = iti.LoadDataTable();

                        //if (dtbd.Rows.Count > 0)
                        //{
                        //    foreach (DataRow d in dtbd.Rows)
                        //    {
                        //        if (Convert.ToBoolean(d["IsClosed"]) == false && Convert.ToDouble(d["QtyRequest"]) > Convert.ToDouble(d["QtyFinished"]))
                        //        {
                        //            isRowDelete = false;
                        //            break;
                        //        }
                        //    }
                        //}
                        //else isRowDelete = false;

                        if (isRowDelete)
                            row.Delete();
                    }
                    dtb.AcceptChanges();
                }

                return dtb;
            }
        }

        protected void btnFilterPO_Click(object sender, ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
        }

        protected void btnFilterPR_Click(object sender, ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdListPr.Rebind();
        }

        protected void cboSearchSupplier_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var supp = new SupplierQuery("a");
            supp.es.Top = 10;
            supp.Where(
                supp.SupplierName.Like(searchTextContain),
                supp.IsActive == true
                );

            cboSearchSupplier.DataSource = supp.LoadDataTable();
            cboSearchSupplier.DataBind();
        }

        protected void cboSearchSupplier_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SupplierName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SupplierID"].ToString();
        }

        protected void cboSRItemType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboItemGroup.Items.Clear();
            cboItemGroup.Text = string.Empty;
            cboItemGroup.SelectedValue = string.Empty;
        }

        protected void cboItemGroup_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ItemGroupQuery("a");
            query.Where(
                query.SRItemType == cboSRItemType.SelectedValue,
                query.ItemGroupName.Like(searchTextContain),
                query.IsActive == true
                );

            cboItemGroup.DataSource = query.LoadDataTable();
            cboItemGroup.DataBind();
        }

        protected void cboItemGroup_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemGroupName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemGroupID"].ToString();
        }

        protected void cboSRItemTypePr_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboItemGroupPr.Items.Clear();
            cboItemGroupPr.Text = string.Empty;
            cboItemGroupPr.SelectedValue = string.Empty;
        }

        protected void cboItemGroupPr_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ItemGroupQuery("a");
            query.Where(
                query.SRItemType == cboSRItemTypePr.SelectedValue,
                query.ItemGroupName.Like(searchTextContain),
                query.IsActive == true
                );

            cboItemGroupPr.DataSource = query.LoadDataTable();
            cboItemGroupPr.DataBind();
        }


        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;

            if (eventArgument == "new")
            {
                //string url = string.Format("PurchaseOrderDetail.aspx?md={0}&pr=&cons={1}", eventArgument, Request.QueryString["cons"]);

                string url = string.Format("PurchaseOrderDetail.aspx?md={0}&pr=&{1}", eventArgument, Request.QueryString);
                Page.Response.Redirect(url);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }

        protected override void InitializeControlFromCookie(Control ctl, object value)
        {
            if (ctl.ID.ToLower().Equals(cboSearchSupplier.ID.ToLower()) && value != null)
            {
                var query = new SupplierQuery();
                query.es.Top = 1;
                query.Select
                    (
                        query.SupplierID,
                        query.SupplierName
                    );
                query.Where(query.SupplierID == value);

                cboSearchSupplier.DataSource = query.LoadDataTable();
                cboSearchSupplier.DataBind();
            }

            if (ctl.ID.ToLower().Equals(cboItemGroup.ID.ToLower()) && value != null)
            {
                var query = new ItemGroupQuery();
                query.es.Top = 1;
                query.Select
                    (
                        query.ItemGroupID,
                        query.ItemGroupName
                    );
                query.Where(query.ItemGroupID == value);

                cboItemGroup.DataSource = query.LoadDataTable();
                cboItemGroup.DataBind();
            }
        }
    }
}
