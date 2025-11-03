using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;
//using Temiang.Avicenna.ReportLibrary.Accounting.BalanceSheet;
using System.Data;

namespace Temiang.Avicenna.Module.Inventory.Procurement
{
    public partial class RequestOrderEditorDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.RequestOrderApproval;

            if (!IsPostBack)
            {
                txtPRNo.Text = Request.QueryString["id"];

                var trans = new ItemTransaction();
                trans.LoadByPrimaryKey(txtPRNo.Text);

                txtFromDate.SelectedDate = trans.TransactionDate;

                txtFromServiceUnitID.Text = trans.FromServiceUnitID;
                ViewState["FromLocationID"] = trans.FromLocationID; // Untuk mengambil maximum & min qty di ItemBalance
                ViewState["SRItemType"] = trans.SRItemType;

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(txtFromServiceUnitID.Text);
                lblFromServiceUnitName.Text = unit.ServiceUnitName;

                txtServiceUnitCostID.Text = trans.ServiceUnitCostID;
                unit = new ServiceUnit();
                unit.LoadByPrimaryKey(txtServiceUnitCostID.Text);
                lblServiceUnitCostName.Text = unit.ServiceUnitName;

                txtToServiceUnitID.Text = trans.ToServiceUnitID;
                unit = new ServiceUnit();
                unit.LoadByPrimaryKey(txtToServiceUnitID.Text);
                lblToServiceUnitName.Text = unit.ServiceUnitName;

                txtNotes.Text = trans.Notes;
            }

            ProcurementUtils.HideColumnStockAndPriceInfo(grdList.MasterTableView);
        }

        public override bool OnButtonOkClicked()
        {
            // validasi sudah ada PO atau belum
            //var dt = new ItemTransactionItemQuery("a");
            //var hd = new ItemTransactionQuery("b");
            //dt.InnerJoin(hd).On(dt.TransactionNo == hd.TransactionNo);
            //dt.Where(dt.ReferenceNo == txtPRNo.Text, hd.IsVoid == false);

            //DataTable dtb = dt.LoadDataTable();
            //if (dtb.Rows.Count > 0)
            //{
            //    ShowInformationHeader("This transaction can't be edited, this data has been proceed to another process");
            //    return false;
            //}

            var items = new ItemTransactionItemCollection();
            items.Query.Where(items.Query.TransactionNo == Request.QueryString["id"]);
            items.LoadAll();

            bool isAutoClosed = AppSession.Parameter.IsAutoClosedOnPrApprovalZero;

            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                var item = items.FindByPrimaryKey(Request.QueryString["id"], dataItem["SequenceNo"].Text);
                if (!(item.IsClosed ?? false))
                {
                    item.RequestQty = item.Quantity;
                    item.Quantity = Convert.ToDecimal((dataItem.FindControl("txtQty") as RadNumericTextBox).Value ?? 0);
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    item.ApprovedByUserID = AppSession.UserLogin.UserID;
                    item.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                    if (isAutoClosed && item.Quantity == 0)
                        item.IsClosed = true;
                }
            }

            items.Save();

            var trans = new ItemTransaction();
            trans.LoadByPrimaryKey(txtPRNo.Text);
            trans.Notes = txtNotes.Text;
            trans.Save();

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
                    query.ItemID,
                    query.SRItemUnit,
                    query.ConversionFactor,
                    query.Quantity,
                    query.QuantityFinishInBaseUnit,
                    query.SequenceNo,
                    query.IsClosed,
                    query.Description,
                    iq.ItemName,
                    "<ISNULL(a.RequestQty, a.Quantity) AS RequestQty>",
                    query.Price,
                    query.Discount1Percentage,
                    query.Discount2Percentage,
                    query.Discount
                );
            query.LeftJoin(iq).On(query.ItemID == iq.ItemID);

            // Base Unit
            var ipnmq = new ItemProductNonMedicQuery("i2");
            var ikq = new ItemKitchenQuery("i2");
            var ipmq = new ItemProductMedicQuery("i2");
            var itemType = ViewState["SRItemType"].ToString();

            switch (itemType)
            {
                case ItemType.NonMedical:
                    query.LeftJoin(ipnmq).On(query.ItemID == ipnmq.ItemID);
                    break;
                case ItemType.Kitchen:
                    query.LeftJoin(ikq).On(query.ItemID == ikq.ItemID);
                    break;
                default:
                    query.LeftJoin(ipmq).On(query.ItemID == ipmq.ItemID);
                    break;
            }

            // Balance Min Max
            var locationID = ViewState["FromLocationID"].ToString();
            if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPorByStockGroup))
            {
                var stockGroup = string.Empty;
                var ibbsgq = new ItemBalanceByStockGroupQuery("c");
                var loc = new Location();
                loc.LoadByPrimaryKey(locationID);
                if (!string.IsNullOrEmpty(loc.SRStockGroup))
                    stockGroup = loc.SRStockGroup;
                query.LeftJoin(ibbsgq).On(query.ItemID == ibbsgq.ItemID && ibbsgq.SRStockGroup == stockGroup);


                var ibq = new ItemBalanceQuery("bl");
                query.LeftJoin(ibq).On(query.ItemID == ibq.ItemID && ibq.LocationID == locationID);


                query.Select(@"<CONVERT(decimal(10,2),COALESCE(c.Balance,0)) AS BalanceSG>",
                    @"<CONVERT(decimal(10,2),COALESCE(bl.Balance,0)) AS Balance>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Minimum,0)) AS Minimum>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Maximum,0)) AS Maximum>",
                    @"<i2.SRItemUnit AS SRMasterBaseUnit>"
                    );
            }
            else
            {
                locationID = ProcurementUtils.LocationIdByItemType(itemType);
                var ibq = new ItemBalanceQuery("c");
                if (string.IsNullOrEmpty(locationID))
                    locationID = "ABCD_EFG";
                query.LeftJoin(ibq).On(query.ItemID == ibq.ItemID && ibq.LocationID == locationID);

                query.Select(@"<CONVERT(decimal(10,2),0) AS BalanceSG>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Balance,0)) AS Balance>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Minimum,0)) AS Minimum>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Maximum,0)) AS Maximum>",
                    @"<i2.SRItemUnit AS SRMasterBaseUnit>"
                    );
            }
            // Sub Query BalanceTotal
            var itemBalTot = new ItemBalanceQuery("ibt");
            itemBalTot.Select((itemBalTot.Balance.Sum().As("BalanceTotal")));
            itemBalTot.Where(itemBalTot.ItemID == query.ItemID);
            query.Select(itemBalTot.Select().As("BalanceTotal"));

            query.Where(
                query.TransactionNo == Request.QueryString["id"],
                query.QuantityFinishInBaseUnit == 0,
                query.IsClosed == false
            );
            query.OrderBy(query.ItemID.Ascending);

            //Apply
            grdList.DataSource = query.LoadDataTable();
        }
    }
}
