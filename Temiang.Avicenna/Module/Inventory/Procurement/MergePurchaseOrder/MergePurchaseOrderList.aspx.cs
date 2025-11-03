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
    public partial class MergePurchaseOrderList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.MergePurchaseOrder;

            if (!IsPostBack)
            {
                txtDate.SelectedDate = DateTime.Now;
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            cboSupplierID.Items.Clear();
            cboSupplierID.Text = string.Empty;

            cboFromServiceUnitID.Items.Clear();
            cboFromServiceUnitID.Text = string.Empty;

            cboSRItemType.Items.Clear();
            cboSRItemType.Text = string.Empty;

            var po = new ItemTransaction();
            po.LoadByPrimaryKey(txtTransactionNo.Text);
            if (po.TransactionCode == TransactionCode.PurchaseOrder && po.IsApproved == false && po.IsVoid == false)
            {
                var supp = new SupplierQuery();
                supp.Where(supp.SupplierID == po.BusinessPartnerID);
                cboSupplierID.DataSource = supp.LoadDataTable();
                cboSupplierID.DataBind();
                cboSupplierID.SelectedValue = po.BusinessPartnerID;

                var su = new ServiceUnitQuery();
                su.Where(su.ServiceUnitID == po.FromServiceUnitID);
                cboFromServiceUnitID.DataSource = su.LoadDataTable();
                cboFromServiceUnitID.DataBind();
                cboFromServiceUnitID.SelectedValue = po.FromServiceUnitID;

                var itype = new AppStandardReferenceItemQuery();
                itype.Where(itype.StandardReferenceID == AppEnum.StandardReference.ItemType, itype.ItemID == po.SRItemType);
                cboSRItemType.DataSource = itype.LoadDataTable();
                cboSRItemType.DataBind();
                cboSRItemType.SelectedValue = po.SRItemType;

                grdList.Rebind();
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                grdList.DataSource = ItemTransactions;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
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
                query.InnerJoin(sup).On(query.BusinessPartnerID == sup.SupplierID);
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
                       query.FromServiceUnitID,
                       query.SRItemType
                       );

                query.Where(query.TransactionCode == TransactionCode.PurchaseOrder, query.IsBySystem == true,
                            query.BusinessPartnerID == cboSupplierID.SelectedValue);
                query.Where(query.FromServiceUnitID == cboFromServiceUnitID.SelectedValue,
                            query.SRItemType == cboSRItemType.SelectedValue,
                            query.TransactionNo != txtTransactionNo.Text, query.IsApproved == false, query.IsVoid == false);
                if (!txtDate.IsEmpty)
                    query.Where(query.TransactionDate == txtDate.SelectedDate);

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.OrderBy(query.TransactionNo.Descending);

                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }

        protected void cboSupplierID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.SupplierItemDataBound(e);
        }

        protected void cboFromServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ServiceUnitItemDataBound(e);
        }

        protected void cboSRItemType_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.StandardReferenceItemDataBound(e);
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

            if (eventArgument == "generate")
            {
                pnlInfo.Visible = false;
                bool isValid = true;
                if (string.IsNullOrEmpty(txtTransactionNo.Text))
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = "Transaction No required.";
                    isValid = false;
                }
                else if (string.IsNullOrEmpty(cboSupplierID.SelectedValue))
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = "Supplier required.";
                    isValid = false;
                }
                else if (string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = "Item Type required.";
                    isValid = false;
                }
                else if (string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = "Purchasing Unit required.";
                    isValid = false;
                }

                if (!isValid) return;

                using (var trans = new esTransactionScope())
                {
                    var dtbTemp = new DataTable();
                    dtbTemp.Columns.Add("ItemID", Type.GetType("System.String"));
                    dtbTemp.Columns.Add("Quantity", Type.GetType("System.Decimal"));

                    var mainColl = new ItemTransactionItemCollection();
                    mainColl.Query.Where(mainColl.Query.TransactionNo == txtTransactionNo.Text);
                    mainColl.LoadAll();

                    foreach (var x in mainColl)
                    {
                        DataRow rowAdd = dtbTemp.NewRow();
                        rowAdd["ItemID"] = x.ItemID;
                        rowAdd["Quantity"] = x.Quantity;

                        dtbTemp.Rows.Add(rowAdd);
                    }

                    mainColl.MarkAllAsDeleted();
                    mainColl.Save();

                    var entity = new ItemTransaction();
                    entity.LoadByPrimaryKey(txtTransactionNo.Text);

                    var itemTemps = grdList.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                                                                                  .Select(dataItem => new
                                                                                  {
                                                                                      TransactionNo = dataItem["TransactionNo"].Text
                                                                                  });
                    foreach (var item in itemTemps)
                    {
                        var temp = new ItemTransaction();
                        temp.LoadByPrimaryKey(item.TransactionNo);
                        temp.Notes = "Digabung ke : " + txtTransactionNo.Text;
                        temp.IsVoid = true;
                        temp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        temp.LastUpdateDateTime = DateTime.Now;

                        var tempItem = new ItemTransactionItemCollection();
                        tempItem.Query.Where(tempItem.Query.TransactionNo == item.TransactionNo);
                        tempItem.Query.OrderBy(tempItem.Query.SequenceNo.Ascending);
                        tempItem.LoadAll();

                        foreach (var t in tempItem)
                        {
                            DataRow rowAdd = dtbTemp.NewRow();
                            rowAdd["ItemID"] = t.ItemID;
                            rowAdd["Quantity"] = t.Quantity;

                            dtbTemp.Rows.Add(rowAdd);
                        }

                        temp.Save();
                    }
                    
                    var items = from item in dtbTemp.AsEnumerable()
                                select new
                                {
                                    ItemID = item.Field<string>("ItemID"),
                                    Quantity = item.Field<decimal>("Quantity")
                                };

                    var itemGroups = items.GroupBy(c => new
                    {
                        c.ItemID,
                        c.Quantity
                    }).Select(q => new
                    {
                        q.Key.ItemID,
                        Quantity = q.Sum(p => (p.Quantity))
                    });

                    var coll = new ItemTransactionItemCollection();
                    decimal chargeAmt = 0;
                    int seqNo = 0;

                    foreach (var i in itemGroups)
                    {
                        seqNo += 1;

                        var c = coll.AddNew();
                        c.TransactionNo = txtTransactionNo.Text;
                        c.ItemID = i.ItemID;
                        c.SequenceNo = string.Format("{0:000}", seqNo);
                        c.Quantity = i.Quantity;

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
                        if (suppItem.LoadByPrimaryKey(cboSupplierID.SelectedValue, c.ItemID))
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
                        c.LastUpdateDateTime = DateTime.Now;

                        chargeAmt += (c.Quantity ?? 0) * (c.Price ?? 0 - c.Discount ?? 0);
                    }

                    entity.ChargesAmount = chargeAmt;
                    entity.AmountTaxed = chargeAmt;
                    entity.TaxAmount = chargeAmt * entity.TaxPercentage / 100;
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = DateTime.Now;

                    entity.Save();
                    coll.Save();

                    trans.Complete();
                }

                pnlInfo.Visible = true;
                lblInfo.Text = "Merge Purchase Order Succeed.";

                grdList.Rebind();
            }
        }
    }
}
