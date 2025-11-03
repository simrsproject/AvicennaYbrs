using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class PrescriptionSalesDelivery : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["rt"] == "opr")
            {
                if (Request.QueryString["type"] == "udd")
                    ProgramID = AppConstant.Program.PrescriptionUddOpr;
                else
                    ProgramID = Request.QueryString["type"] == "sales" ? AppConstant.Program.PrescriptionSalesOpr : AppConstant.Program.PrescriptionRealizationOpr;
            }
            else
            {
                if (Request.QueryString["type"] == "udd")
                    ProgramID = AppConstant.Program.PrescriptionUddIpr;
                else
                    ProgramID = Request.QueryString["type"] == "sales" ? AppConstant.Program.PrescriptionSales : AppConstant.Program.PrescriptionRealization;
            }

            if (IsPostBack) return;

            (Helper.FindControlRecursive(Page, "btnOk") as Button).Attributes["onClick"] = "if (!confirm('Would you approval this transaction?')) return false;";
            (Helper.FindControlRecursive(Page, "btnOk") as Button).Text = "Approve";
        }

        private DataTable TransPrescriptionItems
        {
            get
            {
                var query = new TransPrescriptionItemQuery("a");
                var item1 = new ItemQuery("b");
                var item2 = new ItemQuery("c");

                var hd = new TransPrescriptionQuery("d");

                query.Select
                    (
                        hd.ApprovalDateTime,
                        query,
                        "<CAST(ISNULL(a.DeliveryQty, 0) AS VARCHAR(MAX)) + '/' + CAST(ISNULL(a.ResultQty, 0) AS VARCHAR(MAX)) AS DeliveredQty>",
                        "<a.ResultQty - ISNULL(a.DeliveryQty, 0) AS QtyDelivery>",
                        query.LineAmount.As("Total"),
                        "<CASE WHEN c.ItemName IS NOT NULL THEN c.ItemName ELSE b.ItemName END AS ItemName>",
                        "<(a.ParentNo + a.SequenceNo) as ORDERKEY>"
                    );
                query.InnerJoin(hd).On(query.PrescriptionNo == hd.PrescriptionNo);
                query.InnerJoin(item1).On(query.ItemID == item1.ItemID);
                query.LeftJoin(item2).On(query.ItemInterventionID == item2.ItemID);
                query.Where
                    (
                        hd.RegistrationNo == Request.QueryString["regno"],
                        hd.IsPrescriptionReturn == false,
                        query.IsApprove == true,
                        query.IsPendingDelivery == true,
                        query.IsReturned == false
                    );
                if (!string.IsNullOrEmpty(Request.QueryString["pno"]))
                    query.Where(query.PrescriptionNo == Request.QueryString["pno"]);
                
                query.Where("<a.[ResultQty] > ISNULL(a.[DeliveryQty], 0)>");
                query.OrderBy("ORDERKEY", esOrderByDirection.Ascending);

                return query.LoadDataTable();
            }
        }

        protected void grdTransPrescriptionItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdTransPrescriptionItem.DataSource = TransPrescriptionItems;
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            bool selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in grdTransPrescriptionItem.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }

        protected string GetItemName(object isRFlag, object itemName)
        {
            if (isRFlag.Equals(true))
                return itemName.ToString();
            else
                return "&nbsp;&nbsp;&nbsp;" + itemName.ToString();
        }

        public override bool OnButtonOkClicked()
        {
            if (!grdTransPrescriptionItem.Items.Cast<GridDataItem>().Any(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked))
            {
                this.ShowInformationHeader("No data selected.");
                return false;
            }

            using (var trans = new esTransactionScope())
            {
                foreach (GridDataItem item in grdTransPrescriptionItem.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked))
                {
                    var qty = Convert.ToDecimal((item.FindControl("txtQty") as RadNumericTextBox).Value);
                    if (qty == 0) continue;

                    var header = new TransPrescription();
                    header.LoadByPrimaryKey(item["PrescriptionNo"].Text);

                    var reg = new Registration();
                    reg.LoadByPrimaryKey(header.RegistrationNo);

                    var detail = new TransPrescriptionItem();
                    detail.LoadByPrimaryKey(item["PrescriptionNo"].Text, item["SequenceNo"].Text);
                    detail.DeliveryQty = (detail.DeliveryQty ?? 0) + qty;
                    detail.Save();

                    var unit = new ServiceUnit();
                    unit.LoadByPrimaryKey(header.ServiceUnitID);

                    var chargesBalances = new ItemBalanceCollection();
                    var chargesDetailBalances = new ItemBalanceDetailCollection();
                    var chargesDetailBalanceEds = new ItemBalanceDetailEdCollection();
                    var chargesMovements = new ItemMovementCollection();

                    var transPrescriptionItems = new TransPrescriptionItemCollection();
                    transPrescriptionItems.AttachEntity(detail);

                    string itemNoStock;
                    ItemBalance.PrepareItemBalancesForDelivery(detail.PrescriptionNo, transPrescriptionItems, BusinessObject.Reference.TransactionCode.Prescription, unit.ServiceUnitID,
                        unit.GetMainLocationId(unit.ServiceUnitID), AppSession.UserLogin.UserID, true, ref chargesBalances, ref chargesDetailBalances, ref chargesMovements,
                        ref chargesDetailBalanceEds, AppSession.Parameter.IsEnabledStockWithEdControl, out itemNoStock, qty);

                    if (!string.IsNullOrEmpty(itemNoStock))
                    {
                        this.ShowInformationHeader("Insufficient balance of item : " + itemNoStock);
                        return false;
                    }

                    transPrescriptionItems.Save();

                    if (chargesBalances != null) chargesBalances.Save();
                    if (chargesDetailBalances != null) chargesDetailBalances.Save();
                    if (chargesDetailBalanceEds != null) chargesDetailBalanceEds.Save();
                    if (chargesMovements != null) chargesMovements.Save();

                    /* Automatic Journal Testing Start */
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                    {
                        if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                        {
                            // delivery harusnya cuma masalah stok, pendapatannya sudah dijurnal saat approve resep
                        }
                        else {
                            int? journalId = JournalTransactions.AddNewPrescriptionJournal(header, reg, unit, detail, "RS", AppSession.UserLogin.UserID, 0);
                        }   
                    }
                    /* Automatic Journal Testing End */
                }
                trans.Complete();
            }

            return true;
        }
    }
}
