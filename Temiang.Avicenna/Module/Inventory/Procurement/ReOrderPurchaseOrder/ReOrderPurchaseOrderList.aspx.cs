using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Inventory.Procurement
{
    public partial class ReOrderPurchaseOrderList : BasePage
    {
        private AppAutoNumberLast _autoNumber;

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ReOrderPurchaseOrder;
            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, TransactionCode.PurchaseRequest, true);
                ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, TransactionCode.PurchaseOrder, false);

                if (!string.IsNullOrEmpty(Request.QueryString["su"]))
                    cboToServiceUnitID.SelectedValue = Request.QueryString["su"];
                else
                    cboToServiceUnitID.Text = string.Empty;

                ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboToServiceUnitID.SelectedValue, TransactionCode.PurchaseOrder);

                if (!string.IsNullOrEmpty(Request.QueryString["it"]))
                    cboSRItemType.SelectedValue = Request.QueryString["it"];
                else
                    cboSRItemType.Text = string.Empty;

                StandardReference.InitializeIncludeSpace(cboProductType, AppEnum.StandardReference.ProductType);

                txtDate.SelectedDate = (new DateTime()).NowAtSqlServer();
                cboFromServiceUnitID.Text = string.Empty;
                cboItemGroupID.Text = string.Empty;

                txtStartPrefix.Text = "A";
                txtEndPrefix.Text = "A";
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdListPo.Rebind();
        }

        protected void cboToServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRItemType.Items.Clear();
            cboSRItemType.Text = string.Empty;
            ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboToServiceUnitID.SelectedValue, TransactionCode.PurchaseOrder);
            cboItemGroupID.Items.Clear();
            cboItemGroupID.Text = string.Empty;
        }

        protected void cboSRItemType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboItemGroupID.Items.Clear();
            cboItemGroupID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            if (!string.IsNullOrEmpty(cboSRItemType.SelectedValue))
            {
                var coll = new ItemGroupCollection();
                coll.Query.Where(coll.Query.SRItemType == cboSRItemType.SelectedValue, coll.Query.IsActive == true);
                coll.LoadAll();
                foreach (var c in coll)
                {
                    cboItemGroupID.Items.Add(new RadComboBoxItem(c.ItemGroupName, c.ItemGroupID));
                }
            }
        }

        private void PopulateSupplier()
        {
            if (ViewState["supplier"] != null)
                return;

            var supp = new SupplierQuery("b");
            supp.Select(supp.SupplierID, supp.SupplierName, supp.IsPKP, supp.TaxPercentage);
            supp.Where(supp.IsActive == true);

            ViewState["supplier"] = supp.LoadDataTable();
        }

        private DataTable ItemBalances()
        {
            var poTransCode = TransactionCode.PurchaseOrder;
            var toUnit = cboToServiceUnitID.SelectedValue;

            var su = new ServiceUnit();
            var floc = su.GetMainLocationId(cboFromServiceUnitID.SelectedValue);
            var tloc = su.GetMainLocationId(cboToServiceUnitID.SelectedValue);
            var fmloc1 = su.GetMainLocationId(AppSession.Parameter.ServiceUnitPharmacyID);
            var fmloc2 = su.GetMainLocationId(AppSession.Parameter.ServiceUnitPharmacyIdOpr);

            var query = new ItemBalanceQuery("a");
            var qi = new ItemQuery("d");

            query.Select(
                query.ItemID,
                qi.ItemName,
                query.Balance,
                query.Minimum,
                query.Maximum,
                @"<ISNULL((SELECT SUM(ib.Balance) FROM ItemBalance ib WHERE ib.LocationID = '" + tloc + @"' AND ib.ItemID = a.ItemID), 0) AS PurcUnitBalance>",
                @"<ISNULL((SELECT SUM(ib.Balance) FROM ItemBalance ib WHERE ib.LocationID = '" + fmloc1 + @"' AND ib.ItemID = a.ItemID), 0) AS Fm1Balance>",
                @"<ISNULL((SELECT SUM(ib.Balance) FROM ItemBalance ib WHERE ib.LocationID = '" + fmloc2 + @"' AND ib.ItemID = a.ItemID), 0) AS Fm2Balance>",
                @"<ISNULL((SELECT SUM(ib.Balance) FROM ItemBalance ib WHERE ib.LocationID NOT IN ('" + tloc + @"','" + floc + @"','" + fmloc1 + @"','" + fmloc2 + @"') AND ib.ItemID = a.ItemID), 0) AS OtherBalance>",
                @"<ISNULL((SELECT SUM((iti.Quantity * iti.ConversionFactor) - iti.QuantityFinishInBaseUnit) 
                    FROM ItemTransactionItem iti 
                    INNER JOIN ItemTransaction it ON iti.TransactionNo = it.TransactionNo
                        AND iti.IsClosed = 0 
                        AND it.IsApproved = 1
                        AND it.TransactionCode = '" + poTransCode + @"' 
                        AND it. FromServiceUnitID = '" + toUnit + @"' 
                        AND iti.ItemID = a.ItemID
                    ), 0) AS QtyOnOrder>",
                @"<0 AS RoP>",
                @"<0 AS QtyOrder>",
                @"<'' AS Unit>",
                @"<ISNULL((SELECT TOP 1 si.SupplierID FROM SupplierItem si WHERE si.ItemID = a.ItemID ORDER BY si.LastUpdateDateTime DESC), '') AS SupplierID>"
                );
            query.InnerJoin(qi).On(query.ItemID == qi.ItemID & qi.IsActive == true &
                                    qi.SRItemType == cboSRItemType.SelectedValue & query.LocationID == floc);
            
            if (cboSRItemType.SelectedValue == ItemType.Medical)
            {
                var qipm = new ItemProductMedicQuery("e");
                query.InnerJoin(qipm).On(query.ItemID == qipm.ItemID);
                query.Select(qipm.SRItemUnit, qipm.SRPurchaseUnit, qipm.ConversionFactor);
                query.GroupBy(query.ItemID, qi.ItemName, query.Balance, query.Minimum, query.Maximum, qipm.SRItemUnit,
                              qipm.SRPurchaseUnit, qipm.ConversionFactor);
                if (!string.IsNullOrEmpty(cboProductType.SelectedValue))
                    query.Where(qipm.SRProductType == cboProductType.SelectedValue, qipm.IsConsignment == false);
            }
            else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
            {
                var qipm = new ItemProductNonMedicQuery("e");
                query.InnerJoin(qipm).On(query.ItemID == qipm.ItemID);
                query.Select(qipm.SRItemUnit, qipm.SRPurchaseUnit, qipm.ConversionFactor);
                query.GroupBy(query.ItemID, qi.ItemName, query.Balance, query.Minimum, query.Maximum, qipm.SRItemUnit,
                              qipm.SRPurchaseUnit, qipm.ConversionFactor);
                if (!string.IsNullOrEmpty(cboProductType.SelectedValue))
                    query.Where(qipm.SRProductType == cboProductType.SelectedValue, qipm.IsConsignment == false);
            }
            else
            {
                var qipm = new ItemKitchenQuery("e");
                query.InnerJoin(qipm).On(query.ItemID == qipm.ItemID);
                query.Select(qipm.SRItemUnit, qipm.SRPurchaseUnit, qipm.ConversionFactor);
                query.GroupBy(query.ItemID, qi.ItemName, query.Balance, query.Minimum, query.Maximum, qipm.SRItemUnit,
                              qipm.SRPurchaseUnit, qipm.ConversionFactor);
            }

            if (!string.IsNullOrEmpty(cboItemGroupID.SelectedValue))
            {
                query.Where(qi.ItemGroupID == cboItemGroupID.SelectedValue);
            }

            if (!string.IsNullOrEmpty(txtStartPrefix.Text) && !string.IsNullOrEmpty(txtEndPrefix.Text))
                query.Where(query.ItemID >= txtStartPrefix.Text, query.ItemID <= txtEndPrefix.Text + "ZZZZZZZZZZ");

            query.Where(query.Balance <= query.Minimum, query.Minimum != 0);
            query.OrderBy(query.ItemID.Ascending);

            var tbl = query.LoadDataTable();

            foreach (DataRow row in tbl.Rows)
            {
                row["RoP"] = Convert.ToDecimal(row["Maximum"]) - Convert.ToDecimal(row["Balance"]) - Convert.ToDecimal(row["QtyOnOrder"]);
                row["QtyOrder"] = Math.Round(Convert.ToDecimal(row["RoP"]) / Convert.ToDecimal(row["ConversionFactor"]), 0);
                row["Unit"] = row["SRPurchaseUnit"] + "/" + string.Format("{0:n0}", (Convert.ToDecimal(row["ConversionFactor"])));

                if (!string.IsNullOrEmpty(cboSupplierID.SelectedValue) && row["SupplierID"].ToString() != cboSupplierID.SelectedValue)
                    row.Delete();

                //if (Convert.ToDecimal(row["QtyOrder"]) < 0) row.Delete();
            }

            tbl.AcceptChanges();

            return tbl;
        }

        protected void grdList_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
                e.Item.PreRender += grdList_ItemPreRender;
        }

        private void grdList_ItemPreRender(object sender, EventArgs e)
        {
            var dataItem = sender as GridDataItem;
            if (dataItem == null)
                return;

            var suppId = dataItem["SupplierID"].Text;
            if (!string.IsNullOrEmpty(suppId))
            {
                var supplier = (dataItem["ItemID"].FindControl("cboSupplierID") as RadComboBox);

                if (!supplier.Items.Any())
                {
                    supplier.Items.Clear();
                    supplier.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

                    if (ViewState["supplier"] == null) PopulateSupplier();

                    var table = ((DataTable)ViewState["supplier"]);
                    foreach (DataRow row in table.Rows)
                    {
                        supplier.Items.Add(new RadComboBoxItem((string)row["SupplierName"],
                            string.Format("{0};{1};{2}", (string)row["SupplierID"], (bool)row["IsPKP"], (decimal)row["TaxPercentage"])));
                    }
                }

                var item = supplier.Items.Cast<RadComboBoxItem>().SingleOrDefault(s => s.Value.Split(';')[0] == suppId);
                if (item != null) supplier.SelectedValue = item.Value;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ItemBalances();
        }

        protected void grdListPo_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                grdListPo.DataSource = ItemTransactions;
            }
        }

        protected void grdListPo_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;

            var query = new ItemTransactionItemQuery("a");
            var iq = new ItemQuery("b");
            query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
            query.Where(query.TransactionNo == dataItem.GetDataKeyValue("TransactionNo").ToString());
            query.OrderBy(query.ItemID.Ascending);

            query.Select
                (
                    query.TransactionNo,
                    query.SequenceNo,
                    query.ItemID,
                    iq.ItemName,
                    query.Description,
                    query.SRItemUnit,
                    query.Quantity,
                    query.ConversionFactor,
                    query.Price,
                    query.Discount1Percentage,
                    query.Discount2Percentage,
                    query.Discount
                );
            var dtb = query.LoadDataTable();

            e.DetailTableView.DataSource = dtb;
        }

        private DataTable ItemTransactions
        {
            get
            {
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
                query.LeftJoin(sup).On(query.BusinessPartnerID == sup.SupplierID);
                query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
                query.LeftJoin(itemtype).On(
                    itemtype.ItemID == query.SRItemType &&
                    itemtype.StandardReferenceID == AppEnum.StandardReference.ItemType
                    );
                query.InnerJoin(user).On(query.FromServiceUnitID == user.ServiceUnitID &&
                                         user.UserID == AppSession.UserLogin.UserID);

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
                       "<'../PurchaseOrder/WithThreeTypesOfTaxes/PurchaseOrderDetail.aspx?md=view&id=' + a.TransactionNo + '&pr=&rop=1&su=' + a.FromServiceUnitID +'&it=' + a.SRItemType + '&cons=0' AS PoUrl>",
                       query.FromServiceUnitID,
                       query.SRItemType
                       );

                query.Where(query.TransactionCode == TransactionCode.PurchaseOrder, query.IsBySystem == true);
                query.Where(query.FromServiceUnitID == cboToServiceUnitID.SelectedValue,
                            query.SRItemType == cboSRItemType.SelectedValue);
                if (!txtDate.IsEmpty)
                    query.Where(query.TransactionDate == txtDate.SelectedDate);

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.OrderBy(query.TransactionNo.Descending);

                DataTable dtb = query.LoadDataTable();
                
                return dtb;
            }
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            foreach (CheckBox chkBox in grdList.MasterTableView.Items.Cast<GridDataItem>().Select(dataItem => (CheckBox)dataItem.FindControl("detailChkbox")).Where(chkBox => chkBox.Visible))
            {
                chkBox.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;
            if (eventArgument == "calc")
            {
                grdList.DataSource = null;
                grdList.Rebind();

                grdListPo.DataSource = null;
                grdListPo.Rebind();
            }
            else if (eventArgument == "generate")
            {
                pnlInfo.Visible = false;
                bool isValid = true;
                if (string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = "From Unit required.";
                    isValid = false;
                }
                else if (string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = "Purchasing Unit required.";
                    isValid = false;
                }
                else if (string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = "Item Type required.";
                    isValid = false;
                }

                if (!isValid) return;

                var su = new ServiceUnit();
                if (su.LoadByPrimaryKey(cboToServiceUnitID.SelectedValue))
                {
                    var transNos = string.Empty;

                    using (var trans = new esTransactionScope())
                    {
                        var coll = new ItemTransactionItemCollection();

                        var items = grdList.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked &&
                                                                                                         !string.IsNullOrEmpty(((RadComboBox)dataItem.FindControl("cboSupplierID")).SelectedValue))
                                                                                      .Select(dataItem => new
                                                                                      {
                                                                                          SupplierID = ((RadComboBox)dataItem.FindControl("cboSupplierID")).SelectedValue.Split(';')[0],
                                                                                          IsPkp = Convert.ToBoolean(((RadComboBox)dataItem.FindControl("cboSupplierID")).SelectedValue.Split(';')[1]),
                                                                                          TaxPercentage = Convert.ToDecimal(((RadComboBox)dataItem.FindControl("cboSupplierID")).SelectedValue.Split(';')[2]),
                                                                                          ItemID = dataItem["ItemID"].Text,
                                                                                          Quantity = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQty")).Value ?? 0)
                                                                                      });
                        foreach (var group in (from g in items
                                               group g by new
                                               {
                                                   g.SupplierID,
                                                   g.IsPkp,
                                                   g.TaxPercentage
                                               }
                                                   into grp
                                                   orderby grp.Key.SupplierID
                                                   select new
                                                   {
                                                       SupplierID = grp.Key.SupplierID,
                                                       IsPkp = grp.Key.IsPkp,
                                                       TaxPercentage = grp.Key.TaxPercentage
                                                   }))
                        {
                            _autoNumber = Helper.GetNewAutoNumber(txtDate.SelectedDate.Value.Date, TransactionCode.PurchaseOrder, su.DepartmentID);
                            decimal chargeAmt = 0;

                            foreach (var i in items.Where(i => i.SupplierID == group.SupplierID))
                            {
                                var c = coll.AddNew();

                                #region detail

                                c.TransactionNo = _autoNumber.LastCompleteNumber;
                                c.ItemID = i.ItemID;
                                c.SequenceNo = string.Format("{0:000}", coll.Count + 1);
                                c.Quantity = i.Quantity < 0 ? 0 : i.Quantity;

                                var item = new Item();
                                item.LoadByPrimaryKey(c.ItemID);
                                c.Description = item.ItemName;

                                if (cboSRItemType.SelectedValue == ItemType.Medical)
                                {
                                    var ipm = new ItemProductMedic();
                                    ipm.LoadByPrimaryKey(c.ItemID);
                                    c.SRItemUnit = ipm.SRPurchaseUnit;
                                    c.ConversionFactor = ipm.ConversionFactor;
                                    c.Price = ipm.PriceInPurchaseUnit ?? 0;
                                    c.IsDiscountInPercent = true;
                                    c.Discount1Percentage = c.Discount1Percentage ?? 0;
                                    c.Discount2Percentage = c.Discount2Percentage ?? 0;
                                }
                                else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
                                {
                                    var ipm = new ItemProductNonMedic();
                                    ipm.LoadByPrimaryKey(c.ItemID);
                                    c.SRItemUnit = ipm.SRPurchaseUnit;
                                    c.ConversionFactor = ipm.ConversionFactor;
                                    c.Price = ipm.PriceInPurchaseUnit ?? 0;
                                    c.IsDiscountInPercent = true;
                                    c.Discount1Percentage = c.Discount1Percentage ?? 0;
                                    c.Discount2Percentage = c.Discount2Percentage ?? 0;
                                }
                                else if (cboSRItemType.SelectedValue == ItemType.Kitchen)
                                {
                                    var ipm = new ItemKitchen();
                                    ipm.LoadByPrimaryKey(c.ItemID);
                                    c.SRItemUnit = ipm.SRPurchaseUnit;
                                    c.ConversionFactor = ipm.ConversionFactor;
                                    c.Price = ipm.PriceInPurchaseUnit ?? 0;
                                    c.IsDiscountInPercent = true;
                                    c.Discount1Percentage = c.Discount1Percentage ?? 0;
                                    c.Discount2Percentage = c.Discount2Percentage ?? 0;
                                }

                                var suppItem = new SupplierItem();
                                if (suppItem.LoadByPrimaryKey(i.SupplierID, c.ItemID))
                                {
                                    c.Price = suppItem.PriceInPurchaseUnit ?? 0;
                                    c.IsDiscountInPercent = true;
                                    c.Discount1Percentage = suppItem.PurchaseDiscount1 ?? 0;
                                    c.Discount2Percentage = suppItem.PurchaseDiscount2 ?? 0;
                                }

                                c.PriceInCurrency = c.Price;

                                decimal disc1 = Convert.ToDecimal(c.Price * c.Discount1Percentage / 100);
                                decimal disc2 = Convert.ToDecimal((c.Price - disc1) * c.Discount2Percentage / 100);
                                c.Discount = disc1 + disc2;
                                c.DiscountInCurrency = c.Discount;
                                c.IsBonusItem = false;
                                c.IsTaxable = true;
                                c.IsClosed = false;
                                c.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                c.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                                #endregion

                                chargeAmt += (c.Quantity ?? 0) * (c.Price ?? 0 - c.Discount ?? 0);
                            }

                            var entity = new ItemTransaction();

                            #region header

                            entity.TransactionNo = _autoNumber.LastCompleteNumber;
                            entity.TransactionCode = TransactionCode.PurchaseOrder;
                            entity.TransactionDate = txtDate.SelectedDate;
                            entity.BusinessPartnerID = group.SupplierID;
                            entity.CurrencyID = AppSession.Parameter.CurrencyRupiahID;
                            entity.CurrencyRate = 1;
                            entity.ReferenceNo = string.Empty;
                            entity.FromServiceUnitID = cboToServiceUnitID.SelectedValue;
                            entity.ToServiceUnitID = cboFromServiceUnitID.SelectedValue;
                            entity.ServiceUnitCostID = cboFromServiceUnitID.SelectedValue;
                            entity.TermID = AppSession.Parameter.DefaultTerm;
                            entity.SRPurchaseOrderType = AppSession.Parameter.DefaultPurchaseOrderType;
                            entity.SRItemType = cboSRItemType.SelectedValue;
                            entity.DiscountAmount = 0;
                            entity.ChargesAmount = chargeAmt;
                            entity.AmountTaxed = chargeAmt;
                            entity.DownPaymentAmount = 0;
                            entity.SRDownPaymentType = AppSession.Parameter.DefaultDownPaymentType;

                            entity.IsTaxable = Convert.ToInt16(group.IsPkp ? 1 : 0);
                            entity.TaxPercentage = group.IsPkp ? group.TaxPercentage : 0;
                            entity.TaxAmount = chargeAmt * entity.TaxPercentage / 100;

                            entity.Notes = string.Empty;
                            entity.IsNonMasterOrder = false;
                            entity.LeadTime = string.Empty;
                            entity.ContractNo = string.Empty;
                            entity.IsBySystem = true;
                            entity.IsInventoryItem = true;
                            entity.IsConsignment = false;
                            
                            var supp = new Supplier();
                            supp.LoadByPrimaryKey(group.SupplierID);

                            entity.TermOfPayment = supp.TermOfPayment;
                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                            #endregion

                            _autoNumber.Save();
                            entity.Save();
                            coll.Save();

                            transNos = string.IsNullOrEmpty(transNos) ? entity.TransactionNo : transNos + ", " + entity.TransactionNo;
                        }

                        trans.Complete();
                    }

                    pnlInfo.Visible = true;
                    lblInfo.Text = "Generate Purchase Order Succeed with No. : " + transNos;

                    grdList.Rebind();
                    grdListPo.Rebind();
                }
            }
        }

        protected void cboSupplier_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SupplierQuery("a");
            query.Where(
                query.Or(query.SupplierID == e.Text,
                query.SupplierName.Like(searchTextContain)),
                query.IsActive == true
                );
            query.Select(query.SupplierID, query.SupplierName);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSupplierID.DataSource = dtb;
            cboSupplierID.DataBind();
        }

        protected void cboSupplierID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.SupplierItemDataBound(e);
        }
    }
}
