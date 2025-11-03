using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class DistributionProcessToPr : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //StandardReference Initialize
            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, BusinessObject.Reference.TransactionCode.PurchaseRequest, true);
                ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, BusinessObject.Reference.TransactionCode.PurchaseOrder, false);
                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);

                var ent = new ItemTransaction();
                if (ent.LoadByPrimaryKey(Request.QueryString["drn"]))
                {
                    cboFromServiceUnitID.SelectedValue = ent.FromServiceUnitID;
                    cboSRItemType.SelectedValue = ent.SRItemType;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            txtTransactionDate.SelectedDate = DateTime.Now;
        }

        public override bool OnButtonOkClicked()
        {
            rfvFromServiceUnitID.Validate();
            rfvToServiceUnitID.Validate();
            rfvSRItemType.Validate();

            if (!rfvFromServiceUnitID.IsValid || !rfvToServiceUnitID.IsValid || !rfvSRItemType.IsValid)
                return false;

            using (esTransactionScope trans = new esTransactionScope())
            {
                string userId = AppSession.UserLogin.UserID;
                var fromLocationId = Request.QueryString["floc"];
                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(cboFromServiceUnitID.SelectedValue);

                //Item Transaction
                AppAutoNumberLast autoNumberLast = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value.Date,
                                                                           BusinessObject.Reference.TransactionCode.PurchaseRequest, unit.DepartmentID);

                var entity = new ItemTransaction();
                entity.TransactionNo = autoNumberLast.LastCompleteNumber;
                entity.TransactionCode = BusinessObject.Reference.TransactionCode.PurchaseRequest;
                entity.TransactionDate = txtTransactionDate.SelectedDate;
                entity.BusinessPartnerID = string.Empty;
                entity.ReferenceNo = string.Empty;
                entity.ReferenceDate = DateTime.Parse("01/01/1900");
                entity.FromServiceUnitID = cboFromServiceUnitID.SelectedValue;
                entity.FromLocationID = fromLocationId;
                entity.ToServiceUnitID = cboToServiceUnitID.SelectedValue;
                entity.ToLocationID = string.Empty;
                entity.ServiceUnitCostID = cboFromServiceUnitID.SelectedValue;
                entity.TermID = string.Empty;
                entity.SRItemType = cboSRItemType.SelectedValue;
                entity.DiscountAmount = 0;
                entity.ChargesAmount = 0;
                entity.StampAmount = 0;
                entity.DownPaymentAmount = 0;
                entity.DownPaymentReferenceNo = string.Empty;
                entity.SRDownPaymentType = string.Empty;
                entity.SRAdjustmentType = string.Empty;
                entity.SRDistributionType = string.Empty;
                entity.SRPurchaseReturnType = string.Empty;
                entity.TaxPercentage = 0;
                entity.TaxAmount = 0;
                entity.IsTaxable = 0;
                entity.IsVoid = false;
                entity.VoidDate = DateTime.Parse("01/01/1900");
                entity.VoidByUserID = string.Empty;
                entity.IsApproved = false;
                entity.ApprovedDate = DateTime.Now;
                entity.ApprovedByUserID = userId;
                entity.IsClosed = false;
                entity.IsBySystem = false;
                entity.Notes = txtNotes.Text;
                entity.IsNonMasterOrder = false;
                entity.SRPurchaseCategorization = string.Empty;
                entity.ProductAccountID = string.Empty;
                entity.IsInventoryItem = true;
                entity.IsAssets = false;
                entity.IsConsignment = false;
                entity.IsConsignmentAlreadyReceived = false;
                entity.LastUpdateByUserID = userId;
                entity.LastUpdateDateTime = DateTime.Now;

                //Item Transaction Item
                var query = new ItemTransactionItemQuery("a");
                var balance = new ItemBalanceQuery("b");
                var item = new ItemQuery("c");

                query.Select
                    (
                        query.ItemID,
                        item.ItemName,
                        query.Quantity, 
                        "<CASE WHEN a.ConversionFactor=0 THEN 1 ELSE a.ConversionFactor END as ConversionFactor>",
                        query.SRItemUnit,
                        "<ISNULL(b.Balance, 0) as Balance>",
                        "<ISNULL(b.Maximum, 0) as Maximum>",
                        "<CASE WHEN a.ConversionFactor=0 THEN ISNULL(b.Maximum,0)-ISNULL(b.Balance,0) ELSE (ISNULL(b.Maximum,0)-ISNULL(b.Balance,0)) / a.ConversionFactor END as QtyOrder>"
                    );
                query.LeftJoin(balance).On(query.ItemID == balance.ItemID & balance.LocationID == fromLocationId);
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.Where(query.TransactionNo == Request.QueryString["drn"],
                    "<ISNULL(b.Balance, 0) < a.ConversionFactor * a.Quantity>");
                DataTable tbl = query.LoadDataTable();

                foreach (DataRow row in tbl.Rows)
                {
                    if (!(Convert.ToDouble(row["Maximum"]) > 0))
                    {
                        row["QtyOrder"] = ((Convert.ToDouble(row["Quantity"])*Convert.ToDouble(row["ConversionFactor"])) -
                                           Convert.ToDouble(row["Balance"]))/Convert.ToDouble(row["ConversionFactor"]);
                    }
                }

                tbl.AcceptChanges();

                string seqNo = "000";

                var transactionItems = new ItemTransactionItemCollection();
                
                foreach (DataRow row in tbl.Rows)
                {
                    ItemTransactionItem detail = transactionItems.AddNew();
                    detail.TransactionNo = entity.TransactionNo;

                    detail.SequenceNo = string.Format("{0:000}", int.Parse(seqNo) + 1);
                    seqNo = detail.SequenceNo;

                    detail.ItemID = row["ItemID"].ToString();
                    detail.ItemName = row["ItemName"].ToString();
                    detail.ReferenceNo = string.Empty;
                    detail.ReferenceSequenceNo = string.Empty;
                    detail.Quantity = (decimal)row["QtyOrder"];
                    detail.SRItemUnit = row["SRItemUnit"].ToString();
                    detail.ConversionFactor = (decimal)row["ConversionFactor"];
                    detail.QuantityFinishInBaseUnit = 0;
                    detail.PageNo = 0;
                    detail.CostPrice = 0;
                    detail.Price = 0;
                    detail.IsDiscountInPercent = true;
                    detail.Discount1Percentage = 0;
                    detail.Discount2Percentage = 0;
                    detail.Discount = 0;
                    detail.BatchNumber = string.Empty;
                    detail.str.ExpiredDate = string.Empty;
                    detail.IsPackage = false;
                    detail.IsBonusItem = false;
                    detail.IsClosed = false;
                    detail.Description = row["ItemName"].ToString();
                    detail.LastUpdateByUserID = userId;
                    detail.LastUpdateDateTime = DateTime.Now;
                }

                if (transactionItems.Count > 0)
                {
                    entity.Save();
                    transactionItems.Save();
                    autoNumberLast.Save();
                }
                
                trans.Complete();
            }

            return true;
        }
    }
}
