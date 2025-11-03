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
    public partial class AddItemUsingBarcode : BasePageDialog
    {
        private int _pageNo = 0;
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
        protected void Page_Init(object sender, EventArgs e)
        {
            this.ButtonOk.ValidationGroup = "ok";
            this.ButtonCancel.Text = "Close";

            if (!IsPostBack)
            {
                trPrevBalance.Visible = AppParameter.IsYes(AppParameter.ParameterItem.IsShowSystemQtyInStockTackingOnBarcode);
                //if (AppParameter.IsYes(AppParameter.ParameterItem.IsShowSystemQtyInStockTackingOnBarcode))
                //{
                //    lblPrevBal.Visible = true;
                //    txtPrevBal.Visible = true;
                //    txtSRItemUnitPrev.Visible = true;
                //}
                //else
                //{
                //    lblPrevBal.Visible = false;
                //    txtPrevBal.Visible = false;
                //    txtSRItemUnitPrev.Visible = false;
                //}
            }
        }

        private void PopulateWithBarcode(string bc, ValidateArgs args)
        {
            HideInformationHeader();
            txtBarcodeEntry.Text = string.Empty;
            txtSRItemUnit.Text = string.Empty;
            cboItemID.SelectedIndex = -1;
            cboItemID.Text = string.Empty;
            txtQuantity.Value = 0;
            txtPrevBal.Value = 0;
            txtNote.Text = string.Empty;

            if (string.IsNullOrEmpty(bc))
                return;

            var item = new Item();
            if (!item.LoadByBarcode(bc))
            {
                args.MessageText = string.Format("Can't find barcode {0}", bc);
                return;
            }

            if (item.SRItemType != ItemType)
            {
                args.MessageText = string.Format("Item {0} not valid for this item type", item.ItemName);
                return;
            }


            if (txtPrevBal.Visible == true)
            {
                var prev = new ItemBalance();
                if (prev.LoadByPrimaryKey(LocationID, item.ItemID))
                {
                    txtPrevBal.Value = (double)prev.Balance;
                }
            }

            txtBarcodeEntry.Text = bc;
            PopulateCboItemID(cboItemID, item.ItemID, true);
            PopulateItemUnit(item.ItemID);

            CheckInCurrentStockOpname(args, item.ItemID, item.ItemName);

        }

        private bool CheckInCurrentStockOpname(ValidateArgs args, string itemID, string itemName)
        {
            var qr = new ItemTransactionItemQuery();
            qr.Where(qr.TransactionNo == TransactionNo, qr.ItemID == itemID);
            qr.es.Top = 1;
            var it = new ItemTransactionItem();
            if (!it.Load(qr))
            {
                args.MessageText = string.Format("Item {0} not registered in this stock opname", itemName);
                return false;
            }
            else if (!it.SequenceNo.Contains("A"))
            {
                args.MessageText = string.Format("This item has entry with stock qty: {0}", it.Quantity)
                                    + " " + string.Format("By User : {0}", it.LastUpdateByUserID);
                txtQuantity.Value = it.Quantity.ToDouble();
            }

            // Cek approve Stat
            var appr = new ItemStockOpnameApproval();
            appr.LoadByPrimaryKey(TransactionNo, it.PageNo ?? 0);

            if (appr.IsApproved ?? false)
            {
                args.MessageText = "Stock opname this item has approved";
                return false;
            }

            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                txtBarcodeEntry.Focus();
        }
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            string script = string.Format("var parentPage = GetRadWindow().BrowserWindow;parentPage.RebindGridItem('rebind_{0}');return;", _pageNo);
            return script;
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
            var std = new AppStandardReferenceItemQuery("d");
            query.LeftJoin(std).On(query.SRItemUnit == std.ItemID & std.StandardReferenceID == "ItemUnit");
            var bal = new ItemBalanceQuery("b");
            query.InnerJoin(bal).On(query.ItemID == bal.ItemID & bal.LocationID == LocationID);

            query.Select(query.ItemID, item.ItemName, std.ItemName.As("Unit"), bal.Balance);
            query.Where(query.TransactionNo == TransactionNo, query.SequenceNo.Like("A%") , query.Or
                            (
                                item.ItemName.Like(searchText),
                                query.ItemID.Like(searchText)
                            )
                        );

            query.es.Top = 20;
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

            if (txtPrevBal.Visible == true)
            {
                var prev = new ItemBalance();
                if (prev.LoadByPrimaryKey(LocationID, e.Value))
                {
                    txtPrevBal.Value = (double)prev.Balance;
                }
            }
        }

        private void PopulateItemUnit(string itemID)
        {
            if (ItemType == BusinessObject.Reference.ItemType.Medical)
            {
                var medic = new ItemProductMedic();
                medic.LoadByPrimaryKey(itemID);
                txtSRItemUnit.Text = medic.SRItemUnit;
                txtSRItemUnitPrev.Text = medic.SRItemUnit;
            }
            else if (ItemType == BusinessObject.Reference.ItemType.NonMedical)
            {
                var medic = new ItemProductNonMedic();
                medic.LoadByPrimaryKey(itemID);
                txtSRItemUnit.Text = medic.SRItemUnit;
                txtSRItemUnitPrev.Text = medic.SRItemUnit;
            }
            else if (ItemType == BusinessObject.Reference.ItemType.Kitchen)
            {
                var medic = new ItemKitchen();
                medic.LoadByPrimaryKey(itemID);
                txtSRItemUnit.Text = medic.SRItemUnit;
                txtSRItemUnitPrev.Text = medic.SRItemUnit;
            }
        }

        #endregion

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            if (string.IsNullOrEmpty(eventArgument))
                eventArgument = string.Empty;

            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (eventArgument.Contains("entrybarcode"))
            {
                HideInformationHeader();
                var barcode = eventArgument.Split('|')[1];
                if (!string.IsNullOrEmpty(barcode))
                {
                    ValidateArgs args = new ValidateArgs();
                    PopulateWithBarcode(barcode, args);
                    if (!string.IsNullOrEmpty(args.MessageText))
                        ShowInformationHeader(args.MessageText);

                    if (!string.IsNullOrEmpty(cboItemID.SelectedValue))
                        txtQuantity.Focus();
                }

            }

        }

        protected override void OnButtonOkClicked(ValidateArgs args)
        {
            if (!IsValid(args))
            {
                return;
            }

            using (var trans = new esTransactionScope())
            {
                int lastSeqNo = 0;

                // Cari di detail
                var itemID = cboItemID.SelectedValue;
                var note = txtNote.Text;
                var itiq = new ItemTransactionItemQuery();
                itiq.Where(itiq.TransactionNo == TransactionNo, itiq.ItemID == itemID);
                itiq.es.Top = 1;

                var lineItemInDB = new ItemTransactionItem();
                if (lineItemInDB.Load(itiq))
                {
                    if (lineItemInDB.SequenceNo.Contains("A")) // Belum di scan barcode
                    {
                        _pageNo = InitializedPageNo(TransactionNo, ref lastSeqNo);

                        // Create new
                        var newLineItem = new ItemTransactionItem();
                        newLineItem.TransactionNo = lineItemInDB.TransactionNo;
                        newLineItem.SequenceNo = string.Format("{0:00000}", lastSeqNo + 1);
                        newLineItem.PageNo = _pageNo;

                        newLineItem.ItemID = lineItemInDB.ItemID;
                        newLineItem.SRItemUnit = lineItemInDB.SRItemUnit;
                        newLineItem.ConversionFactor = lineItemInDB.ConversionFactor;
                        newLineItem.Note = note;
                        SaveQty(newLineItem);


                        // Delete
                        lineItemInDB.MarkAsDeleted();
                        lineItemInDB.Save();
                    }
                    else
                    {
                        _pageNo = lineItemInDB.PageNo.ToInt();

                        // Cek kalau sudah di approve jangan diupdate
                        var approval = new ItemStockOpnameApproval();
                        if (approval.LoadByPrimaryKey(lineItemInDB.TransactionNo, _pageNo) && approval.IsApproved == true)
                        {
                            args.MessageText = string.Format("Stock Opname item: {0} has approved, update quantity canceled", cboItemID.Text);
                            args.IsCancel = true;
                        }
                        else
                        {
                            // Update Qty
                            SaveQty(lineItemInDB);
                        }
                    }

                    // Check status item terakhir
                    itiq = new ItemTransactionItemQuery();
                    var iti = new ItemTransactionItem();
                    itiq.Where(itiq.TransactionNo == TransactionNo, itiq.SequenceNo.Like("A%"));
                    itiq.OrderBy(itiq.PageNo.Descending);
                    itiq.es.Top = 1;
                    if (!iti.Load(itiq))
                    {
                        // Jika sudah tidak ada SequenceNo.Like("A%")) berarti sudah selesai dan hapus Page dummy nya
                        var apprDummy = new ItemStockOpnameApproval();
                        if (apprDummy.LoadByPrimaryKey(TransactionNo, 0))
                        {
                            apprDummy.MarkAsDeleted();
                            apprDummy.Save();
                        }
                    }
                    trans.Complete();
                }
                else
                {
                    args.MessageText = string.Format("Item {0} not registered in this stock opname", cboItemID.Text);
                    args.IsCancel = true;
                }
            }

            PopulateWithBarcode(string.Empty, new ValidateArgs());
            txtBarcodeEntry.Focus();
        }

        private void SaveQty(ItemTransactionItem lineItem)
        {
            lineItem.Quantity = Convert.ToDecimal(txtQuantity.Value ?? 0);

            var item = new Item();
            item.LoadByPrimaryKey(lineItem.ItemID);

            // Update ItemStockOpnamePrevBalance
            var prevBalance = new ItemStockOpnamePrevBalance();
            if (!prevBalance.LoadByPrimaryKey(TransactionNo, lineItem.ItemID))
            {
                prevBalance.AddNew();
                prevBalance.TransactionNo = TransactionNo;
                prevBalance.ItemID = lineItem.ItemID;
            }

            // Cost Price
            decimal costPrice = 0;
            if (item.SRItemType == BusinessObject.Reference.ItemType.Medical)
            {
                var itemMedic = new ItemProductMedic();
                if (itemMedic.LoadByPrimaryKey(lineItem.ItemID))
                {
                    costPrice = itemMedic.CostPrice ?? 0;
                }
            }
            else if (item.SRItemType == BusinessObject.Reference.ItemType.NonMedical)
            {
                var itemNonMedic = new ItemProductNonMedic();
                if (itemNonMedic.LoadByPrimaryKey(lineItem.ItemID))
                {
                    costPrice = itemNonMedic.CostPrice ?? 0;
                }
            }
            else
            {
                var itemKitchen = new ItemKitchen();
                if (itemKitchen.LoadByPrimaryKey(lineItem.ItemID))
                {
                    costPrice = itemKitchen.CostPrice ?? 0;
                }
            }

            prevBalance.CostPrice = costPrice;
            lineItem.CostPrice = costPrice;

            // Ambil ulang info Balance for live count (Remark by Handono 202002)
            //prevBalance.Quantity = Convert.ToDecimal(detail.GetColumn("PrevQty"));
            var ib = new ItemBalance();
            if (ib.LoadByPrimaryKey(LocationID, lineItem.ItemID))
            {
                prevBalance.Quantity = ib.Balance;
            }

            prevBalance.SRItemUnit = lineItem.SRItemUnit;
            prevBalance.Save();

            lineItem.Save();
        }

        private int InitializedPageNo(string transactionNo, ref int lastSeqNo)
        {
            const int maxPageSize = 30;
            var newPage = 0;
            // Get last seq no current user
            var lastPageNoForCurrentUser = 1;
            var approval = new ItemStockOpnameApproval();
            var apprQr = new ItemStockOpnameApprovalQuery();
            apprQr.Where(apprQr.TransactionNo == transactionNo, apprQr.CreatedByUserID == AppSession.UserLogin.UserID,
                apprQr.Or(apprQr.IsApproved.IsNull(), apprQr.IsApproved == false));
            apprQr.es.Top = 1;
            apprQr.OrderBy(apprQr.PageNo.Descending);
            if (approval.Load(apprQr))
            {
                lastPageNoForCurrentUser = approval.PageNo ?? 0;

                // Last SequenceNo
                var itiq = new ItemTransactionItemQuery();
                var iti = new ItemTransactionItem();
                itiq.Where(itiq.TransactionNo == transactionNo, itiq.SequenceNo.NotLike("A%"), itiq.PageNo == lastPageNoForCurrentUser);
                itiq.OrderBy(itiq.SequenceNo.Descending);
                itiq.es.Top = 1;
                if (iti.Load(itiq))
                {
                    lastSeqNo = iti.SequenceNo.ToInt();
                }
            }

            // Create new page jika belum ada page dan no urut sudah mencapai kelipatan maxPageSize
            if (lastSeqNo == 0 || lastSeqNo % maxPageSize == 0)
            {
                // Get last Page no 
                var itiq = new ItemTransactionItemQuery();
                var iti = new ItemTransactionItem();
                itiq.Where(itiq.TransactionNo == transactionNo, itiq.SequenceNo.NotLike("A%"));
                itiq.OrderBy(itiq.PageNo.Descending);
                itiq.es.Top = 1;
                if (iti.Load(itiq))
                {
                    newPage = iti.PageNo ?? 1;
                }

                newPage = newPage + 1;

                // Create new page
                approval = new ItemStockOpnameApproval();
                approval.TransactionNo = transactionNo;
                approval.PageNo = newPage;
                approval.IsApproved = false;
                approval.Save();

                lastSeqNo = (maxPageSize * newPage) - maxPageSize;
            }
            else
            {
                newPage = lastPageNoForCurrentUser;
            }

            return newPage;
        }

        private bool IsValid(ValidateArgs args)
        {
            //Check Entry ItemID
            var qrItem = new ItemQuery();
            qrItem.es.Top = 1;
            qrItem.Where(qrItem.ItemName == cboItemID.Text);
            var item = new Item();
            if (!item.Load(qrItem))
            {
                args.IsCancel = true;
                args.MessageText = "Selected item not valid, please select exist item";
                return false;
            }

            // Chect exist
            var itemID = cboItemID.SelectedValue;
            var itiq = new ItemTransactionItemQuery();
            var iti = new ItemTransactionItem();
            itiq.Where(itiq.TransactionNo == TransactionNo, itiq.ItemID == itemID);
            itiq.es.Top = 1;
            if (!iti.Load(itiq))
            {
                args.IsCancel = true;
                args.MessageText = string.Format("Item {0} not registered in this stock opname", cboItemID.Text);
                return false;

            }
            return true;

        }

    }
}