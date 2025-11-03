using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Procurement
{
    public partial class RequestOrderApprovalList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected void Page_Init(object sender, EventArgs e)
        {
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.RequestOrderApproval;
            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, TransactionCode.PurchaseRequest, true);
                ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, TransactionCode.PurchaseOrder, false);

                var costUnit = new ServiceUnitCollection();
                costUnit.Query.Where(costUnit.Query.IsActive == true);
                costUnit.LoadAll();
                cboServiceUnitCostID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var u in costUnit)
                {
                    cboServiceUnitCostID.Items.Add(new RadComboBoxItem(u.ServiceUnitName, u.ServiceUnitID));
                }

                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
            }
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = ItemTransactions;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            ProcurementUtils.HideColumnStockAndPriceInfo(e.DetailTableView);

            //Load record
            var query = new ItemTransactionItemQuery("a");
            var iq = new ItemQuery("b");
            query.Select
                (
                    query.TransactionNo,
                    query.ItemID,
                    query.SRItemUnit,
                    "<CASE WHEN a.RequestQty IS NULL THEN CAST(0 AS NUMERIC(10, 2)) ELSE a.Quantity END AS Quantity>",
                    query.QuantityFinishInBaseUnit,
                    query.SequenceNo,
                    query.IsClosed,
                    query.Description,
                    iq.ItemName.As("ItemName"),
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
            var itemType = e.DetailTableView.ParentItem.GetDataKeyValue("SRItemType").ToString();

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
            var locationID = e.DetailTableView.ParentItem.GetDataKeyValue("FromLocationID").ToString();
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
                query.TransactionNo == e.DetailTableView.ParentItem.GetDataKeyValue("TransactionNo").ToString(),
                query.IsClosed == false
                );
            query.OrderBy(query.ItemID.Ascending);

            //Apply
            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        private DataTable ItemTransactions
        {
            get
            {
                var isEmptyFilter = txtFromDate.IsEmpty && txtToDate.IsEmpty && string.IsNullOrEmpty(txtPRNo.Text) && chkIncludeApproved.Checked == false && string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboServiceUnitCostID.SelectedValue) && string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboSRItemType.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Purchase Request Approval")) return null;

                var query = new ItemTransactionQuery("a");
                var qryserviceunit = new ServiceUnitQuery("b");
                var qryserviceunitto = new ServiceUnitQuery("c");
                var itemtype = new AppStandardReferenceItemQuery("d");
                var itiq = new ItemTransactionItemQuery("e");
                var usrq = new AppUserServiceUnitQuery("f");
                var fromlocq = new LocationQuery("fl");
                var costunit = new ServiceUnitQuery("cu");

                query.Select
                    (
                        query.TransactionNo,
                        query.TransactionDate,
                        qryserviceunit.ServiceUnitName.As("FServiceUnitID"),
                        fromlocq.LocationName.As("FLocationID"),
                        qryserviceunitto.ServiceUnitName.As("TServiceUnitID"),
                        itemtype.ItemName,
                        query.IsApproved,
                        query.IsClosed,
                        query.Notes,
                        query.IsVoid,
                        query.FromLocationID,
                        query.SRItemType,
                        costunit.ServiceUnitName.As("CostUnit")
                    );

                query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
                query.InnerJoin(fromlocq).On(query.FromLocationID == fromlocq.LocationID);
                query.InnerJoin(qryserviceunitto).On(qryserviceunitto.ServiceUnitID == query.ToServiceUnitID);
                query.InnerJoin(costunit).On(costunit.ServiceUnitID == query.ServiceUnitCostID);
                query.LeftJoin(itemtype).On
                    (
                        itemtype.ItemID == query.SRItemType &&
                        itemtype.StandardReferenceID == AppEnum.StandardReference.ItemType
                    );
                query.InnerJoin(itiq).On(query.TransactionNo == itiq.TransactionNo);
                query.InnerJoin(usrq).On(query.FromServiceUnitID == usrq.ServiceUnitID &&
                                         usrq.UserID == AppSession.UserLogin.UserID);

                if (!txtFromDate.IsEmpty && !txtToDate.IsEmpty)
                    query.Where(query.TransactionDate.Between(txtFromDate.SelectedDate.Value.Date, txtToDate.SelectedDate.Value.Date));

                if (!string.IsNullOrEmpty(txtPRNo.Text))
                    query.Where(query.TransactionNo == txtPRNo.Text);

                if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                    query.Where(query.FromServiceUnitID == cboFromServiceUnitID.SelectedValue);

                if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
                    query.Where(query.ToServiceUnitID == cboToServiceUnitID.SelectedValue);

                if (!string.IsNullOrEmpty(cboServiceUnitCostID.SelectedValue))
                    query.Where(query.ServiceUnitCostID == cboServiceUnitCostID.SelectedValue);

                if (!string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                    query.Where(query.SRItemType == cboSRItemType.SelectedValue);

                if (chkIncludeApproved.Checked)
                {
                    //
                }
                else
                {
                    query.Where(query.Or(itiq.RequestQty.IsNull(), itiq.RequestQty == 0));
                }

                query.Where(
                    query.TransactionCode == TransactionCode.PurchaseRequest,
                    query.IsApproved == true,
                    query.IsClosed == false
                    );
                query.es.Distinct = true;
                query.OrderBy(query.TransactionDate.Descending);

                return query.LoadDataTable();
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (!(source is RadGrid))
                return;

            ((RadGrid)source).Rebind();
        }
    }
}
