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

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class InventoryIssueConfirmList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.InventoryIssueConfirm;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, TransactionCode.InventoryIssueOut, false);
                ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, TransactionCode.InventoryIssueRequestOut, true);

                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = ItemTransactions;
        }

        private DataTable ItemTransactions
        {
            get
            {
                var query = new ItemTransactionQuery("a");
                var qryserviceunit = new ServiceUnitQuery("b");
                var qryserviceunitto = new ServiceUnitQuery("c");
                var itemtype = new AppStandardReferenceItemQuery("d");
                var itiq = new ItemTransactionItemQuery("e");
                var usrq = new AppUserServiceUnitQuery("f");
                var fromlocq = new LocationQuery("fl");

                query.Select
                    (
                        query.TransactionNo,
                        query.TransactionDate,
                        query.ReferenceNo,
                        qryserviceunit.ServiceUnitName.As("FServiceUnitID"),
                        fromlocq.LocationName.As("FLocationID"),
                        qryserviceunitto.ServiceUnitName.As("TServiceUnitID"),
                        itemtype.ItemName,
                        query.IsApproved,
                        query.IsClosed,
                        query.Notes,
                        query.IsVoid,
                        query.FromLocationID,
                        query.SRItemType
                    );

                query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
                query.InnerJoin(fromlocq).On(query.FromLocationID == fromlocq.LocationID);
                query.InnerJoin(qryserviceunitto).On(qryserviceunitto.ServiceUnitID == query.ToServiceUnitID);
                query.LeftJoin(itemtype).On
                    (
                        itemtype.ItemID == query.SRItemType &&
                        itemtype.StandardReferenceID == AppEnum.StandardReference.ItemType
                    );
                query.InnerJoin(itiq).On(query.TransactionNo == itiq.TransactionNo);
                query.InnerJoin(usrq).On(query.ToServiceUnitID == usrq.ServiceUnitID &&
                                         usrq.UserID == AppSession.UserLogin.UserID);

                if (!txtFromDate.IsEmpty && !txtToDate.IsEmpty)
                    query.Where(query.TransactionDate.Between(txtFromDate.SelectedDate.Value.Date, txtToDate.SelectedDate.Value.Date));

                if (!string.IsNullOrEmpty(txtIssueNo.Text))
                    query.Where(query.TransactionNo == txtIssueNo.Text);

                if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                    query.Where(query.FromServiceUnitID == cboFromServiceUnitID.SelectedValue);

                if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
                    query.Where(query.ToServiceUnitID == cboToServiceUnitID.SelectedValue);

                if (!string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                    query.Where(query.SRItemType == cboSRItemType.SelectedValue);

                if (chkIncludeConfirmed.Checked)
                {
                    //
                }
                else
                {
                    query.Where(query.Or(itiq.RequestQty.IsNull(), itiq.RequestQty == 0));
                }

                query.Where(
                    query.TransactionCode == TransactionCode.InventoryIssueOut,
                    query.ReferenceNo != string.Empty,
                    query.IsApproved == true
                    );
                query.es.Distinct = true;
                query.OrderBy(query.TransactionDate.Descending);

                return query.LoadDataTable();
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var query = new ItemTransactionItemQuery("a");
            var iq = new ItemQuery("b");
            query.Select
                (
                    query.TransactionNo,
                    query.SequenceNo,
                    query.ItemID,
                    iq.ItemName.As("ItemName"),
                    "<ISNULL(a.RequestQty, a.Quantity) AS RequestQty>",
                    query.SRItemUnit,
                    query.ConversionFactor,
                    "<CASE WHEN a.RequestQty IS NULL THEN CAST(0 AS NUMERIC(10, 2)) ELSE a.Quantity END AS Quantity>",
                    query.QuantityFinishInBaseUnit,
                    query.IsClosed
                );
            query.InnerJoin(iq).On(query.ItemID == iq.ItemID);

            query.Where(
                query.TransactionNo == e.DetailTableView.ParentItem.GetDataKeyValue("TransactionNo").ToString()
                );
            query.OrderBy(query.ItemID.Ascending);

            //Apply
            e.DetailTableView.DataSource = query.LoadDataTable();
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
