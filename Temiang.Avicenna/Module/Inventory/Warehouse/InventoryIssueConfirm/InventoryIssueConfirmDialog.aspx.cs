using System;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;
using System.Data;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class InventoryIssueConfirmDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.RequestOrderApproval;

            if (!IsPostBack)
            {
                txtIssueNo.Text = Request.QueryString["id"];

                var trans = new ItemTransaction();
                trans.LoadByPrimaryKey(txtIssueNo.Text);
                txtDate.SelectedDate = trans.TransactionDate;
                txtReferenceNo.Text = trans.ReferenceNo;
                
                txtFromServiceUnitID.Text = trans.FromServiceUnitID;
                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(txtFromServiceUnitID.Text);
                lblFromServiceUnitName.Text = unit.ServiceUnitName;
                
                txtFromLocationID.Text = trans.FromLocationID;
                var loc = new Location();
                loc.LoadByPrimaryKey(txtFromLocationID.Text);
                lblFromLocationName.Text = loc.LocationName;

                txtToServiceUnitID.Text = trans.ToServiceUnitID;
                unit = new ServiceUnit();
                unit.LoadByPrimaryKey(txtToServiceUnitID.Text);
                lblToServiceUnitName.Text = unit.ServiceUnitName;

                var itype = new AppStandardReferenceItem();
                itype.LoadByPrimaryKey(AppEnum.StandardReference.ItemType.ToString(), trans.SRItemType);
                txtSRItemType.Text = itype.ItemName;

                txtNotes.Text = trans.Notes;
            }
        }

        public override bool OnButtonOkClicked()
        {
            // validasi sudah di-confirm
            var dt = new ItemTransactionItemQuery("a");
            var usr = new AppUserQuery("u");
            dt.LeftJoin(usr).On(dt.ApprovedByUserID == usr.UserID);
            dt.Select(dt.TransactionNo, usr.UserName, dt.ApprovedDateTime);
            dt.Where(dt.TransactionNo == txtIssueNo.Text, dt.RequestQty.IsNotNull());
            dt.es.Top = 1;

            DataTable dtb = dt.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                var d = string.Format("{0:dd-MMM-yyyy HH:mm}", Convert.ToDateTime(dtb.Rows[0]["ApprovedDateTime"]));
                ShowInformationHeader("This transaction already confirmed by: " + dtb.Rows[0]["UserName"] + " [" + d + "]");
                return false;
            }

            var loc = new Location();
            if (loc.LoadByPrimaryKey(txtFromLocationID.Text) && loc.IsHoldForTransaction == true)
            {
                ShowInformationHeader("Location: " + loc.LocationName + " in Hold For Transaction status. Transaction is not allowed.");
                return false;
            }

            var entity = new ItemTransaction();
            entity.LoadByPrimaryKey(txtIssueNo.Text);

            var items = new ItemTransactionItemCollection();
            items.Query.Where(items.Query.TransactionNo == txtIssueNo.Text);
            items.LoadAll();

            string result = ItemTransaction.IsItemMinusProcess(txtIssueNo.Text, items);
            if (result != string.Empty)
            {
                ShowInformationHeader(result);
                return false;
            }

            using (var trans = new esTransactionScope())
            {
                // stock calculation
                var chargesBalances = new ItemBalanceCollection();
                var chargesDetailBalances = new ItemBalanceDetailCollection();
                var chargesMovements = new ItemMovementCollection();
                var itemBalanceDetailEds = new ItemBalanceDetailEdCollection();

                string itemNoStock;
                var itemTransactionItems = items;

                ItemBalance.PrepareItemBalances(entity, itemTransactionItems, entity.FromServiceUnitID, entity.FromLocationID, AppSession.UserLogin.UserID,
                   ref chargesBalances, ref chargesDetailBalances, ref chargesMovements, ref itemBalanceDetailEds, out itemNoStock, AppSession.Parameter.IsEnabledStockWithEdControl);

                if (!string.IsNullOrEmpty(itemNoStock))
                {
                    string msg;
                    if (itemNoStock.Length >= 5 && itemNoStock.Substring(0, 5) == "Zero|")
                        msg = "Zero cost price of item : " + itemNoStock.Replace("Zero|", "");
                    else
                        msg = "Insufficient balance of item : " + itemNoStock;

                    ShowInformationHeader(msg);
                    return false;
                }

                foreach (var item in itemTransactionItems)
                {
                    item.RequestQty = item.Quantity;
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    item.ApprovedByUserID = AppSession.UserLogin.UserID;
                    item.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                    item.IsClosed = true;
                }

                itemTransactionItems.Save();

                if (chargesBalances != null)
                    chargesBalances.Save();
                if (chargesDetailBalances != null)
                    chargesDetailBalances.Save();
                if (itemBalanceDetailEds != null)
                    itemBalanceDetailEds.Save();
                if (chargesMovements != null)
                    chargesMovements.Save();

                var app = new AppParameter();
                app.LoadByPrimaryKey("acc_IsAutoJournalInvIssue");
                if (app.ParameterValue == "Yes")
                {
                    var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.TransactionDate.Value);
                    if (isClosingPeriod)
                    {
                        ShowInformationHeader("Financial statements for period: " +
                                              string.Format("{0:MMMM-yyyy}", entity.TransactionDate) +
                                              " have been closed. Please contact the authorities.");
                        return false;
                    }

                    /* Automatic Journal Testing Start */

                    int? journalId = JournalTransactions.AddNewInventoryIssueJournal(entity, AppSession.UserLogin.UserID, 0);

                    /* Automatic Journal Testing End */
                }

                trans.Complete();
            }

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.rebind = 'rebind'";
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //Load record
            var query = new ItemTransactionItemQuery("a");
            var iq = new ItemQuery("b");

            query.Select
                (
                    query.TransactionNo,
                    query.SequenceNo,
                    query.ItemID,
                    iq.ItemName,
                    query.Quantity,
                    "<ISNULL(a.RequestQty, a.Quantity) AS RequestQty>",
                    query.SRItemUnit,
                    query.ConversionFactor,
                    query.IsClosed
                );
            query.InnerJoin(iq).On(query.ItemID == iq.ItemID);

            query.Where(
                query.TransactionNo == Request.QueryString["id"]
            );
            query.OrderBy(query.ItemID.Ascending);

            //Apply
            grdList.DataSource = query.LoadDataTable();
        }
    }
}
