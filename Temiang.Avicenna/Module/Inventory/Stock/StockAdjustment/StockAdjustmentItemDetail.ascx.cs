using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Inventory.Stock
{
    public partial class StockAdjustmentItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadComboBox cboFromServiceUnitID
        {
            get
            {
                return ((RadComboBox)Helper.FindControlRecursive(this.Page, "cboFromServiceUnitID"));
            }
        }

        private RadComboBox cboFromLocationID
        {
            get
            {
                return ((RadComboBox)Helper.FindControlRecursive(this.Page, "cboFromLocationID"));
            }
        }

        private RadComboBox cboSRItemType
        {
            get
            {
                return ((RadComboBox)Helper.FindControlRecursive(this.Page, "cboSRItemType"));
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            trQtyPending.Visible = !AppSession.Parameter.IsDistributionAutoConfirm;
            cboFromServiceUnitID.Enabled = false;
            cboFromLocationID.Enabled = false;
            cboSRItemType.Enabled = false;
            pnlEd.Visible = AppSession.Parameter.IsEnabledStockWithEdControl;

            if (!string.IsNullOrEmpty(Request.QueryString["type"]))
            {
                lblFillInStock.Text = Request.QueryString["type"] == "p" ? "Fill In Stock (+)" : "Fill In Stock (-)";
                txtQuantity.MinValue = Request.QueryString["type"] == "p" ? 0.01 : -99999.99;
                txtQuantity.MaxValue = Request.QueryString["type"] == "p" ? 99999.99 : -0.01;
            }

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                ItemTransactionItemCollection coll = (ItemTransactionItemCollection)Session["StockAdjustmentItems" + Request.UserHostName];
                if (!coll.HasData)
                    ViewState["SequenceNo"] = "001";
                else
                {
                    int seqNo = int.Parse(coll[coll.Count - 1].SequenceNo) + 1;
                    ViewState["SequenceNo"] = string.Format("{0:000}", seqNo);
                }

                //txtQuantity.Value = 0;
                
                return;
            }

            ViewState["IsNewRecord"] = false;
            cboBatchNumber.Enabled = false;

            ViewState["SequenceNo"] = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.SequenceNo);

            ItemQuery query = new ItemQuery("a");
            ItemBalanceQuery bal = new ItemBalanceQuery("b");

            query.Select
                (
                    query.ItemID,
                    query.ItemName,
                    bal.Balance.Coalesce("0")
                );
            query.InnerJoin(bal).On
                (
                    query.ItemID == bal.ItemID &
                    bal.LocationID == cboFromLocationID.SelectedValue
                );
            query.Where(query.ItemID == (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ItemID));

            cboItemID.DataSource = query.LoadDataTable();
            cboItemID.DataBind();

            cboItemID.SelectedValue = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ItemID);

            txtQuantity.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Quantity));
            txtSRItemUnit.Text = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.SRItemUnit);

            var batchNo = (string)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.BatchNumber);
            if (!string.IsNullOrEmpty(batchNo))
            {
                var ibdeq = new ItemBalanceDetailEdQuery("a");
                ibdeq.Select
                    (
                        @"<a.BatchNumber + '|' + CONVERT(VARCHAR(10), a.ExpiredDate, 101) AS ListKey>",
                        @"<CONVERT(VARCHAR(11), a.ExpiredDate, 13) AS ExpiredDate>",
                        ibdeq.BatchNumber,
                        ibdeq.Balance
                    );
                ibdeq.Where(ibdeq.LocationID == cboFromLocationID.SelectedValue, ibdeq.ItemID == cboItemID.SelectedValue);
                ibdeq.Where(ibdeq.BatchNumber == batchNo);

                object expiredDate = DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ExpiredDate);
                if (expiredDate != null)
                {
                    txtExpiredDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ExpiredDate);
                    ibdeq.Where(ibdeq.ExpiredDate == txtExpiredDate.SelectedDate.Value);
                }
                else
                {
                    txtExpiredDate.Clear();
                    ibdeq.es.Top = 1;
                }
                    
                cboBatchNumber.DataSource = ibdeq.LoadDataTable();
                cboBatchNumber.DataBind();

                cboBatchNumber.Text = batchNo;
            }
            else
            {
                cboBatchNumber.Items.Clear();
                cboBatchNumber.SelectedValue = string.Empty;
                cboBatchNumber.Text = string.Empty;

                object expiredDate = DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ExpiredDate);
                if (expiredDate != null)
                    txtExpiredDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ExpiredDate);
                else
                    txtExpiredDate.Clear();
            }

            chkIsControlExpired.Checked = (bool)DataBinder.Eval(DataItem, "IsControlExpired");

            var balance = new ItemBalance();
            if (balance.LoadByPrimaryKey(cboFromLocationID.SelectedValue, cboItemID.SelectedValue))
            {
                txtBalace.Value = Convert.ToDouble(balance.Balance);
                txtPending.Value = Convert.ToDouble(balance.Booking);
            }
            else
            {
                txtBalace.Value = 0;
                txtPending.Value = 0;
            }
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtQuantity.Value == 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Quantity should not be equal to zero.";
            }

            ItemTransactionItemCollection coll = (ItemTransactionItemCollection)Session["StockAdjustmentItems" + Request.UserHostName];

            if (!AppSession.Parameter.IsEnabledStockWithEdControl)
            {
                //Check duplicate key
                if (ViewState["IsNewRecord"].Equals(true))
                {
                    bool isExist = false;
                    foreach (ItemTransactionItem item in coll)
                    {
                        if (item.ItemID.Equals(cboItemID.SelectedValue))
                        {
                            isExist = true;
                            break;
                        }
                    }

                    if (isExist)
                    {
                        args.IsValid = false;
                        ((CustomValidator)source).ErrorMessage = string.Format("Item: {0} - {1} has exist", cboItemID.SelectedValue, cboItemID.Text);
                        return;
                    }
                }
            }
            else
            {
                string batchNo;

                if (chkIsControlExpired.Checked && txtQuantity.Value > 0 && (string.IsNullOrEmpty(cboBatchNumber.SelectedValue) || txtExpiredDate.IsEmpty))
                {
                    if (string.IsNullOrEmpty(cboBatchNumber.SelectedValue))
                    {
                        args.IsValid = false;
                        ((CustomValidator)source).ErrorMessage = string.Format("Batch Number is required.");
                        return;
                    }
                    if (txtExpiredDate.IsEmpty)
                    {
                        args.IsValid = false;
                        ((CustomValidator)source).ErrorMessage = string.Format("Expired Date is required.");
                        return;
                    }
                }

                if (ViewState["IsNewRecord"].Equals(true))
                {
                    var val = cboBatchNumber.SelectedValue.Split('|');
                    batchNo = Convert.ToString(val[0]);

                    bool isExist = false;
                    var msg = string.Empty;
                    foreach (ItemTransactionItem item in coll)
                    {
                        if (txtQuantity.Value > 0)
                        {
                            if (item.ItemID.Equals(cboItemID.SelectedValue) && item.Quantity < 0)
                            {
                                isExist = true;
                                msg = string.Format("Item: {0} - {1} has exist with different sign (-)", cboItemID.SelectedValue, cboItemID.Text);
                                break;
                            }
                        }
                        else
                        {
                            if (item.ItemID.Equals(cboItemID.SelectedValue) && item.Quantity > 0)
                            {
                                isExist = true;
                                msg = string.Format("Item: {0} - {1} has exist with different sign (+)", cboItemID.SelectedValue, cboItemID.Text);
                                break;
                            }
                        }
                        
                        if (item.ItemID.Equals(cboItemID.SelectedValue) && item.BatchNumber.Equals(batchNo) && item.ExpiredDate.Equals(txtExpiredDate.SelectedDate))
                        {
                            isExist = true;
                            msg = string.Format("Item: {0} - {1} with selected expired date and batch number has exist", cboItemID.SelectedValue, cboItemID.Text);
                            break;
                        }
                    }

                    if (isExist)
                    {
                        args.IsValid = false;
                        ((CustomValidator)source).ErrorMessage = msg;
                        return;
                    }
                }
                else
                {
                    batchNo = cboBatchNumber.Text;

                    bool isExist = false;
                    var msg = string.Empty;
                    foreach (ItemTransactionItem item in coll)
                    {
                        if (txtQuantity.Value > 0)
                        {
                            if (item.SequenceNo != ViewState["SequenceNo"].ToString() && item.ItemID.Equals(cboItemID.SelectedValue) && item.Quantity < 0)
                            {
                                isExist = true;
                                msg = string.Format("Item: {0} - {1} has exist with different sign (-)", cboItemID.SelectedValue, cboItemID.Text);
                                break;
                            }
                        }
                        else
                        {
                            if (item.SequenceNo != ViewState["SequenceNo"].ToString() && item.ItemID.Equals(cboItemID.SelectedValue) && item.Quantity > 0)
                            {
                                isExist = true;
                                msg = string.Format("Item: {0} - {1} has exist with different sign (+)", cboItemID.SelectedValue, cboItemID.Text);
                                break;
                            }
                        }
                        if (item.SequenceNo != ViewState["SequenceNo"].ToString() && item.ItemID.Equals(cboItemID.SelectedValue) &&
                            item.BatchNumber.Equals(batchNo) && item.ExpiredDate.Equals(txtExpiredDate.SelectedDate))
                        {
                            isExist = true;
                            msg = string.Format("Item: {0} - {1} with selected expired date and batch number has exist", cboItemID.SelectedValue, cboItemID.Text);
                            break;
                        }
                    }

                    if (isExist)
                    {
                        args.IsValid = false;
                        ((CustomValidator)source).ErrorMessage = msg;
                        return;
                    }
                }

                //if (chkIsControlExpired.Checked && txtQuantity.Value > 0)
                //{
                //    if (txtExpiredDate.IsEmpty)
                //    {
                //        args.IsValid = false;
                //        ((CustomValidator)source).ErrorMessage = string.Format("Expired Date is required.");
                //        return;
                //    }
                //    if (txtQuantity.Value < 0)
                //    {
                //        var ib = new ItemBalanceDetailEd();
                //        if (ib.LoadByPrimaryKey(cboFromLocationID.SelectedValue, cboItemID.SelectedValue, txtExpiredDate.SelectedDate.Value, batchNo))
                //        {
                //            if (ib.Balance < Math.Abs(Convert.ToDecimal(txtQuantity.Value)))
                //            {
                //                args.IsValid = false;
                //                ((CustomValidator)source).ErrorMessage = string.Format("Insufficient balance ({0:N2})", ib.Balance);
                //                return;
                //            }
                //        }
                //        else
                //        {
                //            args.IsValid = false;
                //            ((CustomValidator)source).ErrorMessage = string.Format("Unlisted selected expired date & batch number.");
                //            return;
                //        }
                //    }
                //}
            }
        }

        #region Properties for return entry value

        public String SequenceNo
        {
            get { return (string)ViewState["SequenceNo"]; }
        }

        public String ItemID
        {
            get { return cboItemID.SelectedValue; }
        }

        public String ItemName
        {
            get { return cboItemID.Text; }
        }

        public Decimal Quantity
        {
            get { return Convert.ToDecimal(txtQuantity.Value); }
        }

        public String SRItemUnit
        {
            get { return txtSRItemUnit.Text; }
        }

        public String BatchNumber
        {
            get
            {
                if (chkIsControlExpired.Checked)
                {
                    if (cboBatchNumber.SelectedValue.Contains("|"))
                    {
                        var val = cboBatchNumber.SelectedValue.Split('|');
                        return Convert.ToString(val[0]);
                    }
                    
                    return cboBatchNumber.Text;
                }
                return "-N/A-";
            }
        }

        public DateTime? ExpiredDate
        {
            get 
            {
                if (chkIsControlExpired.Checked)
                    return txtExpiredDate.SelectedDate;
                return null;
            }
        }

        #endregion

        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new ItemQuery("a");
            var bal = new ItemBalanceQuery("b");

            query.es.Top = 10;
            query.Select
                (
                    query.ItemID,
                    query.ItemName,
                    bal.Balance.Coalesce("0")
                );
            query.InnerJoin(bal).On
                (
                    query.ItemID == bal.ItemID &
                    bal.LocationID == cboFromLocationID.SelectedValue
                );

            if (cboSRItemType.SelectedValue == ItemType.Medical)
            {
                var ip = new ItemProductMedicQuery("c");
                query.InnerJoin(ip).On(query.ItemID == ip.ItemID);
                query.Where(ip.IsInventoryItem == true);
            }
            else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
            {
                var ip = new ItemProductNonMedicQuery("c");
                query.InnerJoin(ip).On(query.ItemID == ip.ItemID);
                query.Where(ip.IsInventoryItem == true);
            }
            else if (cboSRItemType.SelectedValue == ItemType.Kitchen)
            {
                var ip = new ItemKitchenQuery("c");
                query.InnerJoin(ip).On(query.ItemID == ip.ItemID);
                query.Where(ip.IsInventoryItem == true);
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            query.Where
                (
                    query.IsActive == true,
                    query.Or
                        (
                            query.ItemName.Like(searchTextContain),
                            query.ItemID == e.Text
                        )
                );

            cboItemID.DataSource = query.LoadDataTable();
            cboItemID.DataBind();
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboItemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string type = cboSRItemType.SelectedValue;
            var isControlExpired = false;
            if (type == ItemType.Medical)
            {
                var medic = new ItemProductMedic();
                medic.LoadByPrimaryKey(e.Value);
                txtSRItemUnit.Text = medic.SRItemUnit;
                isControlExpired = medic.IsControlExpired ?? false;
            }
            else if (type == ItemType.NonMedical)
            {
                var non = new ItemProductNonMedic();
                non.LoadByPrimaryKey(e.Value);
                txtSRItemUnit.Text = non.SRItemUnit;
                isControlExpired = non.IsControlExpired ?? false;
            }
            else if (type == ItemType.Kitchen)
            {
                var kitchen = new ItemKitchen();
                kitchen.LoadByPrimaryKey(e.Value);
                txtSRItemUnit.Text = kitchen.SRItemUnit;
                isControlExpired = kitchen.IsControlExpired ?? false;
            }

            chkIsControlExpired.Checked = isControlExpired;

            var balance = new ItemBalance();
            if (balance.LoadByPrimaryKey(cboFromLocationID.SelectedValue, e.Value))
            {
                txtBalace.Value = Convert.ToDouble(balance.Balance);
                txtPending.Value = Convert.ToDouble(balance.Booking);
            }
            else
            {
                txtBalace.Value = 0;
                txtPending.Value = 0;
            }
            if (!isControlExpired)
                cboBatchNumber.Text = "-N/A-";
        }

        protected void cboBatchNumber_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new ItemBalanceDetailEdQuery("a");
            query.Select
                (
                    @"<a.BatchNumber + '|' + CONVERT(VARCHAR(10), a.ExpiredDate, 101) AS ListKey>",
                    @"<CONVERT(VARCHAR(11), a.ExpiredDate, 13) AS ExpiredDate>",
                    query.BatchNumber,
                    query.Balance
                );
            query.Where(query.LocationID == cboFromLocationID.SelectedValue, query.ItemID == cboItemID.SelectedValue, query.IsActive == true);
            if (txtQuantity.Value < 0)
            {
                query.Where(query.Balance >= Convert.ToDecimal(txtQuantity.Value));
                query.OrderBy(query.ExpiredDate.Ascending, query.CreatedDateTime.Ascending);
            }
            else
                query.OrderBy(query.ExpiredDate.Descending, query.CreatedDateTime.Descending);

            string searchTextContain = string.Format("%{0}%", e.Text);
            query.Where(query.BatchNumber.Like(searchTextContain));

            cboBatchNumber.DataSource = query.LoadDataTable();
            cboBatchNumber.DataBind();
        }

        protected void cboBatchNumber_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["BatchNumber"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ListKey"].ToString();
        }

        protected void cboBatchNumber_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(cboBatchNumber.SelectedValue) || !cboBatchNumber.SelectedValue.Contains("|"))
            {
                txtExpiredDate.SelectedDate = null;

                return;
            }
            var val = cboBatchNumber.SelectedValue.Split('|');
            txtExpiredDate.SelectedDate = Convert.ToDateTime(val[1]);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (((ItemTransactionItemCollection)Session["StockAdjustmentItems" + Request.UserHostName]).Count == 0)
            {
                cboFromServiceUnitID.Enabled = true;
                cboFromLocationID.Enabled = true;
                cboSRItemType.Enabled = true;
            }
        }
    }
}