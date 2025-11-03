using System;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class DistributionConfirmPickList : BasePageDialog
    {
        private DataTable DistributionConfirms
        {
            get
            {
                string itemType = Request.QueryString["it"];
                string toServiceUnitId = Request.QueryString["su"];
                string toLocationId = Request.QueryString["loc"];

                var query = new ItemTransactionQuery("a");
                var qryserviceunit = new ServiceUnitQuery("b");
                var queryreff = new ItemTransactionQuery("c");
                var floc = new LocationQuery("d");

                query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
                query.LeftJoin(queryreff).On(query.ReferenceNo == queryreff.TransactionNo);
                query.InnerJoin(floc).On(floc.LocationID == query.FromLocationID);
                query.Select(
                    query.TransactionNo,
                    query.TransactionDate,
                    query.FromServiceUnitID,
                    qryserviceunit.ServiceUnitName.As("FromServiceUnit"),
                    floc.LocationName.As("FromLocation"),
                    query.Notes,
                    query.ReferenceNo,
                    (@"<CASE WHEN a.ReferenceNo ='' THEN a.ReferenceDate ELSE c.ReferenceDate  END AS TransactionDateReff>")
                    //queryreff.TransactionDate.As("TransactionDateReff")
                    );

                query.Where(
                    query.IsClosed == false,
                    query.TransactionCode == BusinessObject.Reference.TransactionCode.Distribution,
                    query.SRItemType == itemType,
                    query.ToServiceUnitID == toServiceUnitId,
                    query.ToLocationID == toLocationId,
                    query.IsApproved == true
                    );
                query.Where(@"<a.TransactionNo IN 
(
	SELECT DISTINCT x.TransactionNo 
	FROM ItemTransactionItem AS x 
	INNER JOIN ItemTransaction AS y ON y.TransactionNo = x.TransactionNo AND y.TransactionCode = '050' AND x.IsClosed = 0
)>");
                if (!txtDate.IsEmpty)
                    query.Where(query.TransactionDate == txtDate.SelectedDate);
                if (!string.IsNullOrEmpty(txtDistributionNo.Text))
                    query.Where(query.TransactionNo == txtDistributionNo.Text);
                if (!string.IsNullOrEmpty(txtRequestNo.Text))
                    query.Where(query.ReferenceNo == txtRequestNo.Text);
                if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                    query.Where(query.FromServiceUnitID == cboFromServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(cboFromLocationID.SelectedValue))
                    query.Where(query.FromLocationID == cboFromLocationID.SelectedValue);

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource = DistributionConfirms;
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["DistributionConfirmDetail" + Request.UserHostName] != null)
                grdDetail.DataSource = ViewState["DistributionConfirmDetail" + Request.UserHostName];
        }

        protected void grdDetail_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateDataSourceDetail();
        }

        private void UpdateDataSourceDetail()
        {
            DataTable dtb = (DataTable)ViewState["DistributionConfirmDetail" + Request.UserHostName];
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

                ViewState["DistributionConfirmDetail" + Request.UserHostName] = dtb;
            }
        }

        private void InitializeDataDetail(string transactionNo)
        {
            var query = new ItemTransactionItemQuery("a");
            var hq = new ItemTransactionQuery("h");
            query.InnerJoin(hq).On(query.TransactionNo == hq.TransactionNo);

            var iq = new ItemQuery("b");
            query.InnerJoin(iq).On(query.ItemID == iq.ItemID);

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
                    query.ItemID,
                    query.SRItemUnit,
                    query.ConversionFactor,
                    query.Quantity,
                    //query.QuantityFinishInBaseUnit,
                    //(query.Quantity - query.QuantityFinishInBaseUnit).As("QtyInput"),
                    @"<ISNULL((SELECT SUM(x.Quantity)
                    FROM ItemTransactionItem x
                    INNER JOIN ItemTransaction y ON y.TransactionNo = x.TransactionNo
                    WHERE x.ReferenceNo = a.TransactionNo
                        AND x.ReferenceSequenceNo = a.SequenceNo
                        AND y.IsVoid = 0), 0) AS 'QuantityFinishInBaseUnit'>",

                    @"<(a.Quantity) - ISNULL((SELECT SUM(x.Quantity)
                    FROM ItemTransactionItem x
                    INNER JOIN ItemTransaction y ON y.TransactionNo = x.TransactionNo
                    WHERE x.ReferenceNo = a.TransactionNo
                        AND x.ReferenceSequenceNo = a.SequenceNo
                        AND y.IsVoid = 0), 0) AS 'QtyInput'>",
                    iq.ItemName,
                    hq.SRItemType,
                    query.CostPrice,
                    query.Price,
                    query.PriceInCurrency,
                    query.ConversionFactor
                );
            DataTable dtb = query.LoadDataTable();
            ViewState["DistributionConfirmDetail" + Request.UserHostName] = dtb;
            grdDetail.DataSource = dtb;
            grdDetail.DataBind();
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
            InitializeDataDetail(string.Empty);
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
            Session["DistributionConfirmItemSelected" + Request.UserHostName] = ViewState["DistributionConfirmDetail" + Request.UserHostName];
            return true;
        }

        protected void cboFromServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void cboFromServiceUnitID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ServiceUnitQuery("a");
            var tcquery = new ServiceUnitTransactionCodeQuery("b");

            query.InnerJoin(tcquery).On(query.ServiceUnitID == tcquery.ServiceUnitID);
            query.es.Top = 10;
            query.es.Distinct = true;

            query.Select(
                query.ServiceUnitID,
                query.ServiceUnitName
                );
            query.Where(
                query.ServiceUnitName.Like(searchTextContain),
                query.IsActive == true,
                tcquery.SRTransactionCode == BusinessObject.Reference.TransactionCode.Distribution
                );

            cboFromServiceUnitID.DataSource = query.LoadDataTable();
            cboFromServiceUnitID.DataBind();
        }

        protected void cboFromServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboFromLocationID.Items.Clear();
            cboFromLocationID.SelectedValue = string.Empty;
            cboFromLocationID.Text = string.Empty;
        }

        protected void cboFromLocationID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new LocationQuery("l");
            var sul = new ServiceUnitLocationQuery("sul");
            var tcode = new ServiceUnitTransactionCodeQuery("tcode");
            var qusr = new AppUserServiceUnitQuery("u");
            query.InnerJoin(sul).On(query.LocationID == sul.LocationID);
            query.InnerJoin(tcode).On(tcode.ServiceUnitID == sul.ServiceUnitID && tcode.SRTransactionCode == BusinessObject.Reference.TransactionCode.StockTaking);
            query.InnerJoin(qusr).On(sul.ServiceUnitID == qusr.ServiceUnitID && qusr.UserID == AppSession.UserLogin.UserID);

            query.es.Distinct = true;
            query.es.Top = 10;
            query.Select
                (
                    query.LocationID,
                    query.LocationName
                );
            query.Where
                (
                    query.LocationName.Like(searchTextContain),
                    query.IsActive == true
                );

            if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                query.Where(sul.ServiceUnitID == cboFromServiceUnitID.SelectedValue);

            cboFromLocationID.DataSource = query.LoadDataTable();
            cboFromLocationID.DataBind();
        }

        protected void cboFromLocationID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["LocationName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["LocationID"].ToString();
        }
    }
}