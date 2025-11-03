using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Inventory.StockOpname.RSCH
{
    public partial class StockOpnameAdd : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //StandardReference Initialize
            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, TransactionCode.StockTaking, true);
                StandardReference.InitializeIncludeSpace(cboSRProductType, AppEnum.StandardReference.ProductType);
                rbtStockStatus.SelectedValue = "0";

                trUsingBarcode.Visible = !AppSession.Parameter.IsEnabledStockWithEdControl;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();
        }

        public override bool OnButtonOkClicked()
        {
            rfvFromServiceUnitID.Validate();
            rfvSRItemType.Validate();

            if (!rfvFromServiceUnitID.IsValid || !rfvSRItemType.IsValid)
                return false;

            using (esTransactionScope trans = new esTransactionScope())
            {
                string userID = AppSession.UserLogin.UserID;

                var transactionNo = PopulateHeader(userID);
                Session["NewSO" + Request.UserHostName] = transactionNo;

                if (rbtStockStatus.SelectedValue == "3") // bukan barcode
                {
                    // Tambah 1 hal 
                    ItemStockOpnameApproval approval = new ItemStockOpnameApproval();
                    approval.TransactionNo = transactionNo;
                    approval.PageNo = 1;
                    approval.IsApproved = false;
                    approval.Save();
                }
                else
                {
                    if (!PopulateDetail(transactionNo, userID)) return false;
                }

                trans.Complete();
            }

            return true;
        }

        private bool PopulateDetail(string transactionNo, string userID)
        {
            //Detil Item
            var query = new ItemBalanceQuery("a");
            var medic = new ItemProductMedicQuery("c");
            var nonmedic = new ItemProductNonMedicQuery("d");
            var item = new ItemQuery("e");
            var kitchen = new ItemKitchenQuery("d");

            query.Select
                (
                    query.ItemID,
                    query.Balance,
                    "<ISNULL(a.SRItemBin,'') SRItemBin>"
                //query.SRItemBin
                );
            query.InnerJoin(item).On(query.ItemID == item.ItemID);

            if (cboSRItemType.SelectedValue == ItemType.Medical)
            {
                query.Select(
                    medic.SRItemUnit,
                    medic.CostPrice,
                    medic.IsControlExpired
                    );
                query.InnerJoin(medic).On(
                    query.ItemID == medic.ItemID &&
                    medic.IsInventoryItem == true
                    );

                if (!string.IsNullOrEmpty(cboSRProductType.SelectedValue))
                    query.Where(medic.SRProductType == cboSRProductType.SelectedValue);
                if (!string.IsNullOrEmpty(cboSRTherapyGroupID.SelectedValue))
                    query.Where(medic.SRTherapyGroup == cboSRTherapyGroupID.SelectedValue);
            }
            else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
            {
                query.Select(
                    nonmedic.SRItemUnit,
                    nonmedic.CostPrice,
                    nonmedic.IsControlExpired
                    );
                query.InnerJoin(nonmedic).On(
                    query.ItemID == nonmedic.ItemID &&
                    nonmedic.IsInventoryItem == true
                    );

                if (!string.IsNullOrEmpty(cboSRProductType.SelectedValue))
                    query.Where(nonmedic.SRProductType == cboSRProductType.SelectedValue);
            }
            else if (cboSRItemType.SelectedValue == ItemType.Kitchen)
            {
                query.Select(
                    kitchen.SRItemUnit,
                    kitchen.CostPrice,
                    kitchen.IsControlExpired
                    );
                query.InnerJoin(kitchen).On(
                    query.ItemID == kitchen.ItemID &&
                    kitchen.IsInventoryItem == true
                    );
            }

            query.Where(
                query.LocationID == cboFromLocationID.SelectedValue,
                item.IsActive == true
                );
            if (!string.IsNullOrEmpty(cboItemBin.SelectedValue))
                query.Where(query.SRItemBin == cboItemBin.SelectedValue);
            if (!string.IsNullOrEmpty(cboItemGroupID.SelectedValue))
                query.Where(item.ItemGroupID == cboItemGroupID.SelectedValue);
            if (rbtStockStatus.SelectedValue == "1")
                query.Where(query.Balance > 0);

            //db: 2023-07-06 jika modif order di sini, modif juga di StockOpnameDetail.aspx.cs 
            query.OrderBy(query.SRItemBin.Ascending, item.ItemName.Ascending);

            DataTable tbl = query.LoadDataTable();

            if (tbl.Rows.Count == 0)
            {
                pnlInfo.Visible = true;
                lblInfo.Text = "No records to process.";
                return false;
            }

            int index = 1;
            int index2 = 1;
            int counter = 0;
            int maxPageSize = 30;
            try
            {
                maxPageSize = System.Convert.ToInt32(
                    AppParameter.GetParameterValue(AppParameter.ParameterItem.StockOpnameRowPerPage)
                );
            }
            catch
            {

            }

            int pageNo = 1;

            var transactionItems = new ItemTransactionItemCollection();
            var prevBalances = new ItemStockOpnamePrevBalanceCollection();
            var prevBalanceEds = new ItemStockOpnamePrevBalanceEdCollection();

            var isStockOpnameFormPerBin = AppParameter.IsYes(AppParameter.ParameterItem.IsStockOpnameFormPerBin);
            var itemBin = string.Empty;
            var serverDateNow = (new DateTime()).NowAtSqlServer();

            var itemId = string.Empty;

            foreach (DataRow row in tbl.Rows)
            {
                ItemTransactionItem detail = transactionItems.AddNew();
                detail.TransactionNo = transactionNo;

                if (chkIsUsingBarcode.Checked != true)
                {
                    if (index == 1)
                    {
                        itemBin = (string)row["SRItemBin"];
                    }

                    detail.SequenceNo = string.Format("{0:00000}", index++);

                    counter++;
                    if ((isStockOpnameFormPerBin && itemBin != (string)row["SRItemBin"]) || counter > maxPageSize)
                    {
                        pageNo++;
                        counter = 0;
                        itemBin = (string)row["SRItemBin"];
                        counter++;
                    }

                    detail.PageNo = pageNo;
                }
                else
                {
                    // Using Barcode Method apply page no later
                    detail.PageNo = 0;
                    detail.SequenceNo = string.Format("A{0:0000}", index++);
                }
                detail.ItemID = (string)row["ItemID"];
                detail.Quantity = 0;
                detail.ConversionFactor = 1;
                detail.CostPrice = (decimal)row["CostPrice"];
                detail.SRItemUnit = (string)row["SRItemUnit"];
                detail.LastUpdateByUserID = userID;
                detail.LastUpdateDateTime = serverDateNow;

                if (AppSession.Parameter.IsEnabledStockWithEdControl )
                {
                    if (Convert.ToBoolean(row["IsControlExpired"]))
                    {
                        var itemBalanceDetailEds = new ItemBalanceDetailEdCollection();
                        itemBalanceDetailEds.Query.Where(itemBalanceDetailEds.Query.LocationID == cboFromLocationID.SelectedValue, itemBalanceDetailEds.Query.ItemID == detail.ItemID, itemBalanceDetailEds.Query.IsActive == true);
                        itemBalanceDetailEds.Query.OrderBy(itemBalanceDetailEds.Query.ExpiredDate.Descending, itemBalanceDetailEds.Query.CreatedDateTime.Descending);
                        itemBalanceDetailEds.LoadAll();

                        if (itemBalanceDetailEds.Count > 0)
                        {
                            var i = 0;
                            decimal balance = (decimal)row["Balance"];
                            foreach (var ibd in itemBalanceDetailEds)
                            {
                                if (i == 0)
                                {
                                    if (ibd.ExpiredDate == Convert.ToDateTime("1/1/2999"))
                                    {
                                        detail.str.ExpiredDate = string.Empty;
                                        detail.BatchNumber = string.Empty;
                                    }
                                    else
                                    {
                                        detail.ExpiredDate = ibd.ExpiredDate;
                                        detail.BatchNumber = ibd.BatchNumber;
                                    }

                                    ItemStockOpnamePrevBalanceEd prevBalanceEd = prevBalanceEds.AddNew();
                                    prevBalanceEd.TransactionNo = transactionNo;
                                    prevBalanceEd.SequenceNo = detail.SequenceNo;
                                    prevBalanceEd.ItemID = detail.ItemID;
                                    prevBalanceEd.ExpiredDate = detail.ExpiredDate;
                                    prevBalanceEd.BatchNumber = detail.BatchNumber;
                                    prevBalanceEd.Quantity = ibd.Balance;
                                    prevBalanceEd.SRItemUnit = detail.SRItemUnit;
                                }
                                else
                                {
                                    if (balance == 0) // --> asumsi baru pertama kali SO dimana stok ED msh kosong semua, jd ditampilkan semua ED yg aktif
                                    {
                                        ItemTransactionItem detail2 = transactionItems.AddNew();
                                        detail2.TransactionNo = transactionNo;
                                        detail2.PageNo = detail.PageNo;
                                        detail2.SequenceNo = string.Format("E{0:0000}", index2++);
                                        detail2.ItemID = (string)row["ItemID"];
                                        detail2.Quantity = 0;
                                        detail2.ConversionFactor = 1;
                                        detail2.CostPrice = (decimal)row["CostPrice"];
                                        detail2.SRItemUnit = (string)row["SRItemUnit"];
                                        detail2.LastUpdateByUserID = userID;
                                        detail2.LastUpdateDateTime = serverDateNow;

                                        if (ibd.ExpiredDate == Convert.ToDateTime("1/1/2999"))
                                        {
                                            detail2.str.ExpiredDate = string.Empty;
                                            detail2.BatchNumber = string.Empty;
                                        }
                                        else
                                        {
                                            detail2.ExpiredDate = ibd.ExpiredDate;
                                            detail2.BatchNumber = ibd.BatchNumber;
                                        }

                                        detail2.IsAccEd = true;

                                        ItemStockOpnamePrevBalanceEd prevBalanceEd = prevBalanceEds.AddNew();
                                        prevBalanceEd.TransactionNo = transactionNo;
                                        prevBalanceEd.SequenceNo = detail2.SequenceNo;
                                        prevBalanceEd.ItemID = detail2.ItemID;
                                        prevBalanceEd.ExpiredDate = detail2.ExpiredDate;
                                        prevBalanceEd.BatchNumber = detail2.BatchNumber;
                                        prevBalanceEd.Quantity = 0;
                                        prevBalanceEd.SRItemUnit = detail2.SRItemUnit;
                                    }
                                    else // balance > 0 --> hanya menampilkan ED yg ada balance-nya saja
                                    {
                                        if (ibd.Balance > 0)
                                        {
                                            ItemTransactionItem detail2 = transactionItems.AddNew();
                                            detail2.TransactionNo = transactionNo;
                                            detail2.PageNo = detail.PageNo;
                                            detail2.SequenceNo = string.Format("E{0:0000}", index2++);
                                            detail2.ItemID = (string)row["ItemID"];
                                            detail2.Quantity = 0;
                                            detail2.ConversionFactor = 1;
                                            detail2.CostPrice = (decimal)row["CostPrice"];
                                            detail2.SRItemUnit = (string)row["SRItemUnit"];
                                            detail2.LastUpdateByUserID = userID;
                                            detail2.LastUpdateDateTime = serverDateNow;

                                            if (ibd.ExpiredDate == Convert.ToDateTime("1/1/2999"))
                                            {
                                                detail2.str.ExpiredDate = string.Empty;
                                                detail2.BatchNumber = string.Empty;
                                            }
                                            else
                                            {
                                                detail2.ExpiredDate = ibd.ExpiredDate;
                                                detail2.BatchNumber = ibd.BatchNumber;
                                            }

                                            detail2.IsAccEd = true;

                                            ItemStockOpnamePrevBalanceEd prevBalanceEd = prevBalanceEds.AddNew();
                                            prevBalanceEd.TransactionNo = transactionNo;
                                            prevBalanceEd.SequenceNo = detail2.SequenceNo;
                                            prevBalanceEd.ItemID = detail2.ItemID;
                                            prevBalanceEd.ExpiredDate = detail2.ExpiredDate;
                                            prevBalanceEd.BatchNumber = detail2.BatchNumber;
                                            prevBalanceEd.Quantity = ibd.Balance;
                                            prevBalanceEd.SRItemUnit = detail2.SRItemUnit;
                                        }
                                    }
                                }
                                i++;
                            }
                        }
                        else
                        {
                            detail.str.ExpiredDate = string.Empty;
                            detail.BatchNumber = string.Empty;

                            ItemStockOpnamePrevBalanceEd prevBalanceEd = prevBalanceEds.AddNew();
                            prevBalanceEd.TransactionNo = transactionNo;
                            prevBalanceEd.SequenceNo = detail.SequenceNo;
                            prevBalanceEd.ItemID = detail.ItemID;
                            prevBalanceEd.ExpiredDate = detail.ExpiredDate;
                            prevBalanceEd.BatchNumber = detail.BatchNumber;
                            prevBalanceEd.Quantity = (decimal)row["Balance"];
                            prevBalanceEd.SRItemUnit = detail.SRItemUnit;
                        }
                    }
                    else
                    {
                        detail.str.ExpiredDate = string.Empty;
                        detail.BatchNumber = string.Empty;

                        ItemStockOpnamePrevBalanceEd prevBalanceEd = prevBalanceEds.AddNew();
                        prevBalanceEd.TransactionNo = transactionNo;
                        prevBalanceEd.SequenceNo = detail.SequenceNo;
                        prevBalanceEd.ItemID = detail.ItemID;
                        prevBalanceEd.ExpiredDate = detail.ExpiredDate;
                        prevBalanceEd.BatchNumber = detail.BatchNumber;
                        prevBalanceEd.Quantity = (decimal)row["Balance"];
                        prevBalanceEd.SRItemUnit = detail.SRItemUnit;
                    }
                }
                else
                {
                    detail.str.ExpiredDate = string.Empty;
                    detail.BatchNumber = string.Empty;
                }

                ItemStockOpnamePrevBalance prevBalance = prevBalances.AddNew();
                prevBalance.TransactionNo = transactionNo;
                prevBalance.ItemID = (string)row["ItemID"];
                prevBalance.Quantity = (decimal)row["Balance"];
                prevBalance.SRItemUnit = (string)row["SRItemUnit"];
            }

            //Approval table
            if (chkIsUsingBarcode.Checked == true)
            {
                var approval = new ItemStockOpnameApproval();
                approval.TransactionNo = transactionNo;
                approval.PageNo = 0;
                approval.IsApproved = false;
                approval.Save();
            }
            else
            {
                var approvals = new ItemStockOpnameApprovalCollection();
                for (int i = 0; i < pageNo; i++)
                {
                    ItemStockOpnameApproval approval = approvals.AddNew();
                    approval.TransactionNo = transactionNo;
                    approval.PageNo = i + 1;
                    approval.IsApproved = false;
                }
                approvals.Save();
            }
            transactionItems.Save();

            prevBalances.Save();

            if (AppSession.Parameter.IsEnabledStockWithEdControl)
                prevBalanceEds.Save();

            if (AppSession.Parameter.IsAutoFreezeLocationOnStockOpnameAdd)
            {
                var loc = new Location();
                if (loc.LoadByPrimaryKey(cboFromLocationID.SelectedValue))
                {
                    loc.IsHoldForTransaction = true;
                    loc.LastHoldForTransactionDateTime = (new DateTime()).NowAtSqlServer();
                    loc.LastHoldForTransactionByUserID = userID;

                    loc.Save();
                }
            }
            return true;
        }

        private string PopulateHeader(string userID)
        {
            //Item Transaction
            AppAutoNumberLast autoNumberLast = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value.Date,
                AppEnum.AutoNumber.StockTaking);

            autoNumberLast.Save();

            var itemTransaction = new ItemTransaction();
            itemTransaction.TransactionNo = autoNumberLast.LastCompleteNumber;
            itemTransaction.TransactionCode = TransactionCode.StockTaking;
            itemTransaction.TransactionDate = txtTransactionDate.SelectedDate;
            itemTransaction.FromServiceUnitID = cboFromServiceUnitID.SelectedValue;
            itemTransaction.FromLocationID = cboFromLocationID.SelectedValue;
            itemTransaction.SRItemType = cboSRItemType.SelectedValue;

            if (cboSRItemType.SelectedValue == ItemType.Medical)
                itemTransaction.Notes = ((cboItemGroupID.Text + " ").Trim() + cboSRProductType.Text + " ").Trim() +
                                        (cboSRTherapyGroupID.Text + " ").Trim() + txtNotes.Text;
            else
                itemTransaction.Notes = ((cboItemGroupID.Text + " ").Trim() + cboSRProductType.Text + " ").Trim() +
                                        txtNotes.Text;

            itemTransaction.LastUpdateByUserID = userID;
            itemTransaction.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            itemTransaction.Save();
            return itemTransaction.TransactionNo;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.command = 'ok';oWnd.argument.trno = '" + Session["NewSO" + Request.UserHostName] + "';oWnd.argument.initial = '" + AppSession.Parameter.IsStockOpnamePerGroupItem + "'";
        }

        protected void cboFromServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            // khusus poli anak rs sentra cikarang, kenapa pada saat pilih lokasi
            // jadi nembak ke event ini???????? DAFUG!!
            if (e.OldValue == e.Value) return;
            // temporary solved pake jurus ini. DAFUGGGGG

            ComboBox.PopulateWithServiceUnitForLocation(cboFromLocationID, e.Value);
            cboFromLocationID.SelectedIndex = 1;

            ComboBox.PopulateWithItemBinByLocation(cboItemBin, cboFromLocationID.SelectedValue);

            if (AppSession.Parameter.HealthcareInitialAppsVersion != "RSSA")
            {
                ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, e.Value, TransactionCode.StockTaking);

                cboSRTherapyGroupID.Items.Clear();
                cboSRTherapyGroupID.Text = string.Empty;
                cboSRTherapyGroupID.SelectedValue = string.Empty;
            }
            else if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSA" && string.IsNullOrEmpty(cboSRItemType.SelectedValue))
            {
                ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, e.Value, TransactionCode.StockTaking);

                cboSRTherapyGroupID.Items.Clear();
                cboSRTherapyGroupID.Text = string.Empty;
                cboSRTherapyGroupID.SelectedValue = string.Empty;
            }
        }

        protected void cboFromLocationID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ComboBox.PopulateWithItemBinByLocation(cboItemBin, e.Value);
        }

        protected void cboSRItemType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var query = new ItemGroupQuery("a");
            query.Select(query.ItemGroupID, query.ItemGroupName);
            query.Where(query.IsActive == true, query.SRItemType == e.Value);

            DataTable dtb = query.LoadDataTable();

            cboItemGroupID.Items.Clear();
            cboItemGroupID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow item in dtb.Rows)
            {
                cboItemGroupID.Items.Add(new RadComboBoxItem(item["ItemGroupName"].ToString(), item["ItemGroupID"].ToString()));
            }

            cboSRTherapyGroupID.Items.Clear();
            cboSRTherapyGroupID.Text = string.Empty;
            cboSRTherapyGroupID.SelectedValue = string.Empty;
        }

        protected void cboSRTherapyGroupID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.StandardReferenceItemsRequested(cboSRTherapyGroupID, "TherapyGroup", e.Text);
        }

        protected void cboSRTherapyGroupID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.StandardReferenceItemDataBound(e);
        }
    }
}