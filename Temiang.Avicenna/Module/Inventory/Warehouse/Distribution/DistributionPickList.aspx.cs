using System;
using System.Linq;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class DistributionPickList : BasePageDialog
    {
        private DataTable DistributionRequests
        {
            get
            {
                string itemType = Request.QueryString["it"];
                string distReqNo = Request.QueryString["drn"];
                var query = new ItemTransactionQuery("a");
                var qryserviceunit = new ServiceUnitQuery("b");
                query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);

                var qrItem = new ItemTransactionItemQuery("d");
                query.InnerJoin(qrItem).On(query.TransactionNo == qrItem.TransactionNo);

                query.Select
                    (
                        query.TransactionNo,
                        query.TransactionDate,
                        query.FromServiceUnitID,
                        qryserviceunit.ServiceUnitName.As("FromServiceUnit"),
                        query.Notes, query.ApprovedByUserID, query.ApprovedDate
                    );

                query.Where
                    (
                        query.TransactionCode == BusinessObject.Reference.TransactionCode.DistributionRequest,
                        query.IsApproved == true,
                        qrItem.IsClosed == false,
                        query.SRItemType == itemType
                    );

                if (!txtDate.IsEmpty)
                    query.Where(query.TransactionDate == txtDate.SelectedDate);
                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    query.Where(query.FromServiceUnitID == cboServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(distReqNo))
                    query.Where(query.TransactionNo == distReqNo);
                query.OrderBy(query.TransactionDate.Descending, query.TransactionNo.Descending);

                query.es.Distinct = true;
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                var dtb = query.LoadDataTable();

                foreach (DataRow row in dtb.Rows.Cast<DataRow>().Where(row => DistributionRequestItemPendings(row["TransactionNo"].ToString()).Rows.Count == 0))
                {
                    row.Delete();
                }

                return dtb;
            }
        }

        private DataTable DistributionRequestItemPendings(string transactionNo)
        {
            var query = new ItemTransactionItemQuery("a");
            var hq = new ItemTransactionQuery("b");
            var bal = new ItemBalanceQuery("c");
            var iq = new ItemQuery("e");
            var ibq2 = new ItemBalanceQuery("f");

            query.InnerJoin(hq).On(query.TransactionNo == hq.TransactionNo);
            query.LeftJoin(bal).On(query.ItemID == bal.ItemID && hq.FromLocationID == bal.LocationID);
            query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
            query.LeftJoin(ibq2).On(query.ItemID == ibq2.ItemID && ibq2.LocationID == hq.ToLocationID);

            query.Where
                (
                query.TransactionNo == transactionNo,
                query.IsClosed == false);
            query.OrderBy
                (
                    query.ItemID.Ascending
                );

            query.Select
                (
                    query.TransactionNo,
                    query.SequenceNo,
                    hq.FromServiceUnitID,
                    hq.FromLocationID,
                    query.ItemID,
                    query.SRItemUnit,
                    query.Quantity,
                    query.QuantityFinishInBaseUnit,
                    ((query.Quantity * query.ConversionFactor) - query.QuantityFinishInBaseUnit).As("QtyInput"),
                    iq.ItemName,
                    hq.SRItemType,
                    query.CostPrice,
                    query.ConversionFactor,
                    @"<ISNULL(c.Balance, 0) AS 'Balance'>",
                    @"<ISNULL(c.Minimum, 0) AS 'Minimum'>",
                    @"<ISNULL(c.Maximum, 0) AS 'Maximum'>",
                    @"<ISNULL(f.Balance / a.ConversionFactor, 0) AS 'Balance2'>",
                    hq.ServiceUnitCostID
                );
            var dtb = query.LoadDataTable();
            return dtb;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //service unit
                var unit = new ServiceUnitCollection();
                unit.Query.Where(unit.Query.IsActive == true);
                unit.LoadAll();

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource = DistributionRequests;
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["DistributionDetail" + Request.UserHostName] != null)
                grdDetail.DataSource = ViewState["DistributionDetail" + Request.UserHostName];
        }
        protected void grdDetail_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateDataSourceDetail();
        }

        private void UpdateDataSourceDetail()
        {
            DataTable dtb = (DataTable)ViewState["DistributionDetail" + Request.UserHostName];
            foreach (GridDataItem dataItem in grdDetail.MasterTableView.Items)
            {
                string seqNo = dataItem.GetDataKeyValue("SequenceNo").ToString();
                double qty = ((RadNumericTextBox)dataItem.FindControl("txtQtyInput")).Value ?? 0;

                foreach (DataRow row in dtb.Rows)
                {
                    if (row["SequenceNo"].Equals(seqNo))
                    {
                        row["QtyInput"] = qty;
                        break;
                    }
                }

                ViewState["DistributionDetail" + Request.UserHostName] = dtb;
            }
        }
        private void InitializeDataDetail(string transactionNo)
        {
            var query = new ItemTransactionItemQuery("a");
            var hq = new ItemTransactionQuery("b");
            var ibq = new ItemBalanceQuery("d");
            var iq = new ItemQuery("e");
            var ibq2 = new ItemBalanceQuery("f");

            query.InnerJoin(hq).On(query.TransactionNo == hq.TransactionNo);
            query.LeftJoin(ibq).On(query.ItemID == ibq.ItemID && hq.FromLocationID == ibq.LocationID);
            query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
            query.LeftJoin(ibq2).On(query.ItemID == ibq2.ItemID && ibq2.LocationID == hq.ToLocationID);

            query.Where(query.TransactionNo == transactionNo, query.IsClosed == false);
            query.OrderBy
                (
                    query.ItemID.Ascending
                );

            query.Select
                (
                    query.TransactionNo,
                    query.SequenceNo,
                    hq.FromServiceUnitID,
                    hq.FromLocationID,
                    hq.ToServiceUnitID,
                    hq.ToLocationID,
                    query.ItemID,
                    query.SRItemUnit,
                    query.Quantity,
                    query.QuantityFinishInBaseUnit,
                    (((query.Quantity * query.ConversionFactor) - query.QuantityFinishInBaseUnit) / query.ConversionFactor).As("QtyInput"),
                    iq.ItemName,
                    hq.SRItemType,
                    hq.ItemGroupID,
                    query.CostPrice,
                    query.ConversionFactor,
                    ibq.Balance.Coalesce("0"),
                    ibq.Minimum.Coalesce("0"),
                    ibq.Maximum.Coalesce("0"),
                    ibq.Booking.Coalesce("0"),
                    @"<ISNULL(f.Balance / a.ConversionFactor, 0) AS 'Balance2'>",
                    hq.ServiceUnitCostID
                );
            var dtb = query.LoadDataTable();
            ViewState["DistributionDetail" + Request.UserHostName] = dtb;
            grdDetail.DataSource = dtb;
            grdDetail.DataBind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);
            if (!(source is RadGrid)) return;
            RadGrid grd = (RadGrid)source;
            switch (grd.ID.ToLower())
            {
                case "grddetail": // Populate Detail
                    string[] pars = eventArgument.Split('|');
                    string transNo = pars[0].Split(':')[1];
                    InitializeDataDetail(transNo);
                    break;
            }
        }
        public override bool OnButtonOkClicked()
        {
            UpdateDataSourceDetail();
            //Session["DistributionItemSelected" + Request.UserHostName] = ViewState["DistributionWithStockInfoDetail" + Request.UserHostName];
            Session["DistributionItemSelected" + Request.UserHostName] = ViewState["DistributionDetail" + Request.UserHostName];
            return true;
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
            InitializeDataDetail(string.Empty);
        }
    }
}
