using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Inventory.StockOpname.RSCH
{
    public partial class StockOpnameLineQty : BasePageDialog
    {
        private string TransactionNo
        {
            get { return Request.QueryString["trno"]; }
        }
        private string SeqNo
        {
            get { return Request.QueryString["seqno"]; }
        }
        private string LocationID
        {
            get { return Request.QueryString["loc"]; }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var line = new ItemTransactionItem();
                line.LoadByPrimaryKey(TransactionNo, SeqNo);

                var item = new Item();
                item.LoadByPrimaryKey(line.ItemID);

                txtItemID.Text = item.ItemID;
                txtItemName.Text = item.ItemName;
                txtNote.Text = line.Note;

                txtQuantity.Value = line.Quantity.ToDouble();
                txtQuantity.Focus();
            }
        }

        public override bool OnButtonOkClicked()
        {
            var detail = new ItemTransactionItem();
            detail.LoadByPrimaryKey(TransactionNo, SeqNo);

            var item = new Item();
            item.LoadByPrimaryKey(detail.ItemID);

            // Update ItemStockOpnamePrevBalance
            var prevBalance = new ItemStockOpnamePrevBalance();
            if (!prevBalance.LoadByPrimaryKey(TransactionNo, detail.ItemID))
            {
                prevBalance.AddNew();
                prevBalance.TransactionNo = TransactionNo;
                prevBalance.ItemID = detail.ItemID;
            }

            // Cost Price
            decimal costPrice = 0;
            if (item.SRItemType == BusinessObject.Reference.ItemType.Medical)
            {
                var itemMedic = new ItemProductMedic();
                if (itemMedic.LoadByPrimaryKey(detail.ItemID))
                {
                    costPrice = itemMedic.CostPrice ?? 0;
                }
            }
            else if (item.SRItemType == BusinessObject.Reference.ItemType.NonMedical)
            {
                var itemNonMedic = new ItemProductNonMedic();
                if (itemNonMedic.LoadByPrimaryKey(detail.ItemID))
                {
                    costPrice = itemNonMedic.CostPrice ?? 0;
                }
            }
            else
            {
                var itemKitchen = new ItemKitchen();
                if (itemKitchen.LoadByPrimaryKey(detail.ItemID))
                {
                    costPrice = itemKitchen.CostPrice ?? 0;
                }
            }

            prevBalance.CostPrice = costPrice;
            detail.CostPrice = costPrice;

            // Ambil ulang info Balance for live count (Remark by Handono 202002)
            //prevBalance.Quantity = Convert.ToDecimal(detail.GetColumn("PrevQty"));
            var ib = new ItemBalance();
            if (ib.LoadByPrimaryKey(LocationID, detail.ItemID))
            {
                prevBalance.Quantity = ib.Balance;
            }

            prevBalance.SRItemUnit = detail.SRItemUnit;
            prevBalance.Save();

            detail.Quantity = txtQuantity.Value.ToDecimal();
            detail.Note = txtNote.Text;
            detail.Save();

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.command = 'ok'";
        }
    }
}