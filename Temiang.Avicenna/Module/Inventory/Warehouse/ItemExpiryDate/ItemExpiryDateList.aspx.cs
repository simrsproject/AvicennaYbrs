using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class ItemExpiryDateList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            var btnOk = (Button)Helper.FindControlRecursive(Master, "btnOk");
            btnOk.Visible = false;
            var btnCancel = (Button)Helper.FindControlRecursive(Master, "btnCancel");
            btnCancel.Text = "Close";

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRItemUnit, AppEnum.StandardReference.ItemUnit);
                StandardReference.InitializeIncludeSpace(cboSRItemType, AppEnum.StandardReference.ItemType);

                txtItemID.Text = Request.QueryString["id"];
                var item = new Item();
                if (item.LoadByPrimaryKey(txtItemID.Text))
                {
                    txtItemName.Text = item.ItemName;
                    cboSRItemType.SelectedValue = item.SRItemType;

                    switch (cboSRItemType.SelectedValue)
                    {
                        case ItemType.Medical:
                            var im = new ItemProductMedic();
                            if (im.LoadByPrimaryKey(txtItemID.Text))
                                cboSRItemUnit.SelectedValue = im.SRItemUnit;
                            else
                                cboSRItemUnit.Text = string.Empty;
                            break;
                        case ItemType.NonMedical:
                            var inm = new ItemProductNonMedic();
                            if (inm.LoadByPrimaryKey(txtItemID.Text))
                                cboSRItemUnit.SelectedValue = inm.SRItemUnit;
                            else
                                cboSRItemUnit.Text = string.Empty;
                            break;
                        case ItemType.Kitchen:
                            var ik = new ItemKitchen();
                            if (ik.LoadByPrimaryKey(txtItemID.Text))
                                cboSRItemUnit.SelectedValue = ik.SRItemUnit;
                            else
                                cboSRItemUnit.Text = string.Empty;

                            break;
                    }

                    var ib = new ItemBalanceQuery("a");
                    ib.Select(ib.ItemID, @"<ISNULL(SUM(a.Balance), 0) AS Balance>");
                    ib.Where(ib.ItemID == txtItemID.Text);
                    ib.GroupBy(ib.ItemID);
                    ib.es.WithNoLock = true;
                    DataTable dtb = ib.LoadDataTable();
                    if (dtb.Rows.Count > 0)
                        txtQuantity.Value = Convert.ToDouble(dtb.Rows[0]["Balance"]);
                    else
                        txtQuantity.Value = 0;
                }
                else {
                    txtItemName.Text = "Item not found";
                    cboSRItemType.Text = string.Empty;
                    cboSRItemUnit.Text = string.Empty;
                    txtQuantity.Value = -1;
                }

                grdEd.Columns.FindByUniqueName("btnIsClosed").Visible = Request.QueryString["t"].ToString() == "8";
            }
        }

        private DataTable ItemTransactionItemEds
        {
            get
            {
                var itemEdQ = new ItemTransactionItemEdQuery("a");
                var itQ = new ItemTransactionQuery("b");
                var vwQ = new VwItemProductMedicNonMedicQuery("c");
                itemEdQ.InnerJoin(itQ).On(itQ.TransactionNo == itemEdQ.TransactionNo);
                itemEdQ.InnerJoin(vwQ).On(vwQ.ItemID == itemEdQ.ItemID);
                itemEdQ.Select
                (
                    itemEdQ.TransactionNo,
                    itemEdQ.SequenceNo,
                    itemEdQ.ExpiredDate,
                    itemEdQ.BatchNumber,
                    @"<a.Quantity * a.ConversionFactor AS QuantityInBaseUnit>",
                    vwQ.SRItemUnit,
                    @"<ISNULL(a.IsClosed, 0) AS IsClosed>"
                );
                itemEdQ.Where(itQ.TransactionCode == TransactionCode.PurchaseOrderReceive, itQ.IsApproved == true, itemEdQ.ItemID == txtItemID.Text);
                if (chkIsClosed.Checked)
                {
                    itemEdQ.Where(itemEdQ.IsClosed == true);
                    itemEdQ.OrderBy(itemEdQ.ExpiredDate.Descending, itemEdQ.TransactionNo.Descending);
                }
                else
                {
                    itemEdQ.Where(itemEdQ.Or(itemEdQ.IsClosed.IsNull(), itemEdQ.IsClosed == false));
                    itemEdQ.OrderBy(itemEdQ.ExpiredDate.Ascending, itemEdQ.TransactionNo.Ascending);
                }
                    
                itemEdQ.es.WithNoLock = true;
                
                return itemEdQ.LoadDataTable();
            }
        }

        protected void grdEd_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEd.DataSource = ItemTransactionItemEds;
        }

        protected void chkIsClosed_CheckedChanged(object sender, EventArgs e)
        {
            grdEd.Rebind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument == "rebind")
                grdEd.Rebind();

            else if (eventArgument.Contains("|"))
            {
                var param = eventArgument.Split('|');
                var transNo = param[1];
                var seqNo = param[2];
                var ed = Convert.ToDateTime(param[3]);
                var batchNo = param[4];

                var itemEd = new ItemTransactionItemEd();
                if (itemEd.LoadByPrimaryKey(transNo, seqNo, ed, batchNo))
                {
                    itemEd.IsClosed = true;
                    itemEd.ClosedDateTime = (new DateTime()).NowAtSqlServer();
                    itemEd.ClosedByUserID = AppSession.UserLogin.UserID;
                    itemEd.Save();
                }

                grdEd.DataSource = ItemTransactionItemEds;
                grdEd.DataBind();
            }
        }
    }
}