using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
//using Temiang.Avicenna.Module.Finance.Payable.Adjustment.Adding;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Inventory.StockOpname
{
    public partial class AddItemEd : BasePageDialog
    {
        private int _pageNo 
        {
            get { return Request.QueryString["pageNo"].ToInt(); }
        }
        private string ItemType
        {
            get { return Request.QueryString["it"]; }
        }
        private string LocationID
        {
            get { return Request.QueryString["loc"]; }
        }
        private string TransactionNo
        {
            get { return Request.QueryString["trno"]; }
        }
        private bool IsNewBatchNo
        {
            get { return Request.QueryString["type"] == "new"; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            this.ButtonOk.ValidationGroup = "ok";
            this.ButtonCancel.Text = "Close";

            if (!IsPostBack)
            {
                if (IsNewBatchNo)
                {
                    cboBatchNumber.Visible = false;
                    rfvCboBatchNumber.Visible = false;
                }
                else
                {
                    txtBatchNumber.Visible = false;
                    rfvTxtBatchNumber.Visible = false;
                    txtExpiredDate.Enabled = false;
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            string script = string.Format("var parentPage = GetRadWindow().BrowserWindow;parentPage.RebindGridItem('rebind_{0}');return;", _pageNo);
            return script;
        }

        private void PopulateWithBarcode(string bc, ValidateArgs args)
        {
            HideInformationHeader();
            txtSRItemUnit.Text = string.Empty;
            cboItemID.SelectedIndex = -1;
            cboItemID.Text = string.Empty;
            txtQuantity.Value = 0;
            txtNote.Text = string.Empty;
            txtBatchNumber.Text = string.Empty;
            txtExpiredDate.Clear();

            if (string.IsNullOrEmpty(bc))
                return;
        }

        private bool CheckInCurrentStockOpname(ValidateArgs args, string itemID, string itemName)
        {
            var qr = new ItemTransactionItemQuery();
            qr.Where(qr.TransactionNo == TransactionNo, qr.ItemID == itemID, qr.PageNo == _pageNo);
            qr.es.Top = 1;
            var it = new ItemTransactionItem();
            if (!it.Load(qr))
            {
                args.MessageText = string.Format("Item {0} not listed in this stock taking on the selected page ({1}).", itemName, _pageNo.ToString());
                return false;
            }

            // Cek approve Stat
            var appr = new ItemStockOpnameApproval();
            appr.LoadByPrimaryKey(TransactionNo, _pageNo);

            if (appr.IsApproved ?? false)
            {
                args.MessageText = "Stock taking for this item has been approved.";
                return false;
            }

            return true;
        }

        #region ComboBox ItemID
        protected void csvItemID_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !string.IsNullOrEmpty(cboItemID.SelectedValue) && !string.IsNullOrEmpty(cboItemID.Text);
        }
        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboItemID((RadComboBox)sender, e.Text, false);
        }

        private void PopulateCboItemID(RadComboBox comboBox, string textSearch, bool isUseItemID)
        {
            string searchText = string.Format("%{0}%", textSearch);

            var query = new ItemTransactionItemQuery("ti");
            var item = new ItemQuery("a");
            query.InnerJoin(item).On(query.ItemID == item.ItemID);
            
            if (ItemType == BusinessObject.Reference.ItemType.Medical)
            {
                var medic = new ItemProductMedicQuery("c");
                query.InnerJoin(medic).On(query.ItemID == medic.ItemID && medic.IsInventoryItem == true && medic.IsControlExpired == true);
            }
            else if (ItemType == BusinessObject.Reference.ItemType.NonMedical)
            {
                var nonmedic = new ItemProductNonMedicQuery("d");
                query.InnerJoin(nonmedic).On(query.ItemID == nonmedic.ItemID && nonmedic.IsInventoryItem == true && nonmedic.IsControlExpired == true);
            }
            else if (ItemType == BusinessObject.Reference.ItemType.Kitchen)
            {
                var kitchen = new ItemKitchenQuery("d");
                query.InnerJoin(kitchen).On(query.ItemID == kitchen.ItemID && kitchen.IsInventoryItem == true && kitchen.IsControlExpired == true);
            }

            query.Select(query.ItemID, item.ItemName);
            query.Where(query.TransactionNo == TransactionNo, query.PageNo == _pageNo, 
                query.Or(
                    item.ItemName.Like(searchText), query.ItemID.Like(searchText)
                    ));

            query.es.Top = 20;
            query.es.Distinct = true;

            var dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }
        protected void cboItemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            HideInformationHeader();
            txtQuantity.MaxLength = 10;
            txtQuantity.MinValue = 0;
            txtQuantity.MaxValue = 99999999.99;
            txtSRItemUnit.Text = string.Empty;
            txtQuantity.Value = 0;

            var args = new ValidateArgs();
            CheckInCurrentStockOpname(args, e.Value, e.Text);

            if (!string.IsNullOrEmpty(args.MessageText))
                ShowInformationHeader(args.MessageText);

            PopulateItemUnit(e.Value);
        }

        private void PopulateItemUnit(string itemID)
        {
            if (ItemType == BusinessObject.Reference.ItemType.Medical)
            {
                var medic = new ItemProductMedic();
                medic.LoadByPrimaryKey(itemID);
                txtSRItemUnit.Text = medic.SRItemUnit;
            }
            else if (ItemType == BusinessObject.Reference.ItemType.NonMedical)
            {
                var medic = new ItemProductNonMedic();
                medic.LoadByPrimaryKey(itemID);
                txtSRItemUnit.Text = medic.SRItemUnit;
            }
            else if (ItemType == BusinessObject.Reference.ItemType.Kitchen)
            {
                var medic = new ItemKitchen();
                medic.LoadByPrimaryKey(itemID);
                txtSRItemUnit.Text = medic.SRItemUnit;
            }
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
            query.Where(query.LocationID == LocationID, query.ItemID == cboItemID.SelectedValue, query.BatchNumber != "-N/A-", query.IsActive == true);
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

        #endregion

        protected override void OnButtonOkClicked(ValidateArgs args)
        {
            if (!IsValid(args))
            {
                return;
            }

            int lastSeqNo = 0;

            // Cari di detail
            var itemID = cboItemID.SelectedValue;
            var note = txtNote.Text;
            decimal cossPrice = 0, conversionFactor = 0;
            var srItemUnit = string.Empty;

            // Last SequenceNo
            var itiq = new ItemTransactionItemQuery();
            var iti = new ItemTransactionItem();
            itiq.Where(itiq.TransactionNo == TransactionNo, itiq.SequenceNo.Like("E%"));
            itiq.OrderBy(itiq.SequenceNo.Descending);
            itiq.es.Top = 1;
            if (iti.Load(itiq))
            {
                lastSeqNo = iti.SequenceNo.Replace("E", "").ToInt();
            }

            itiq = new ItemTransactionItemQuery();
            iti = new ItemTransactionItem();
            itiq.Where(itiq.TransactionNo == TransactionNo, itiq.SequenceNo.NotLike("E%"), itiq.ItemID == itemID, itiq.PageNo == _pageNo);
            itiq.es.Top = 1;
            if (iti.Load(itiq))
            {
                cossPrice = iti.CostPrice ?? 0;
                conversionFactor = iti.ConversionFactor ?? 1;
                srItemUnit = iti.SRItemUnit;
            }

            var detail = new ItemTransactionItem();
            detail.AddNew();
            detail.TransactionNo = TransactionNo;
            detail.PageNo = _pageNo;
            detail.SequenceNo = string.Format("E{0:0000}", lastSeqNo + 1);

            detail.ItemID = itemID;
            detail.ExpiredDate = txtExpiredDate.SelectedDate;

            var batchNo = string.Empty;
            if (IsNewBatchNo)
            {
                batchNo = txtBatchNumber.Text.Trim();
            }
            else
            {
                var val = cboBatchNumber.SelectedValue.Split('|');
                batchNo = Convert.ToString(val[0]);
            }

            detail.BatchNumber = batchNo;
            detail.Quantity = Convert.ToDecimal(txtQuantity.Value);
            detail.ConversionFactor = conversionFactor;
            detail.CostPrice = cossPrice;
            detail.SRItemUnit = srItemUnit;
            detail.Note = note;
            detail.IsAccEd = true;

            detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
            detail.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            detail.Save();

            var prevBalanceEd = new ItemStockOpnamePrevBalanceEd();
            prevBalanceEd.AddNew();
            prevBalanceEd.TransactionNo = detail.TransactionNo;
            prevBalanceEd.SequenceNo = detail.SequenceNo;
            prevBalanceEd.ItemID = detail.ItemID;
            prevBalanceEd.ExpiredDate = detail.ExpiredDate;
            prevBalanceEd.BatchNumber = detail.BatchNumber;
            prevBalanceEd.Quantity = 0;
            prevBalanceEd.SRItemUnit = detail.SRItemUnit;

            prevBalanceEd.Save();

            PopulateWithBarcode(string.Empty, new ValidateArgs());
            cboItemID.Focus();

            //return;
        }

        private bool IsValid(ValidateArgs args)
        {
            //Check Entry ItemID
            var item = new Item();
            if (!item.LoadByPrimaryKey(cboItemID.SelectedValue))
            {
                args.IsCancel = true;
                args.MessageText = "Selected item not valid, please select exist item.";
                return false;
            }

            var batchNo = string.Empty;

            if (IsNewBatchNo)
            {
                if (string.IsNullOrEmpty(txtBatchNumber.Text))
                {
                    args.IsCancel = true;
                    args.MessageText = "Batch Number is required.";
                    return false;
                }

                if (txtExpiredDate.IsEmpty)
                {
                    args.IsCancel = true;
                    args.MessageText = "Expired Date is required.";
                    return false;
                }

                batchNo = txtBatchNumber.Text.Trim();

                var balance = new ItemBalanceDetailEd();
                if (balance.LoadByPrimaryKey(LocationID, cboItemID.SelectedValue, txtExpiredDate.SelectedDate.Value, batchNo))
                {
                    args.IsCancel = true;
                    args.MessageText = "The Batch Number and Expired Date entered already exists.";
                    return false;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(cboBatchNumber.SelectedValue))
                {
                    args.IsCancel = true;
                    args.MessageText = "Batch Number is required.";
                    return false;
                }

                if (txtExpiredDate.IsEmpty)
                {
                    args.IsCancel = true;
                    args.MessageText = "Expired Date is required.";
                    return false;
                }

                var val = cboBatchNumber.SelectedValue.Split('|');
                batchNo = Convert.ToString(val[0]);
            }

            var qr = new ItemTransactionItemQuery();
            qr.Where(qr.TransactionNo == TransactionNo, qr.ItemID == cboItemID.SelectedValue, qr.PageNo == _pageNo, qr.BatchNumber == batchNo, qr.ExpiredDate == txtExpiredDate.SelectedDate.Value);
            qr.es.Top = 1;
            var it = new ItemTransactionItem();
            if (it.Load(qr))
            {
                args.IsCancel = true;
                args.MessageText = string.Format("Item {0} with the selected Batch No and Expired Date have been registered in the stock taking.", cboItemID.Text);
                return false;
            }

            // Cek approve Stat
            var appr = new ItemStockOpnameApproval();
            appr.LoadByPrimaryKey(TransactionNo, _pageNo);

            if (appr.IsApproved ?? false)
            {
                args.IsCancel = true;
                args.MessageText = "Stock taking for this item has been approved.";
                return false;
            }

            return true;
        }
    }
}