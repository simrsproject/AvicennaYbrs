using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Inventory.Procurement
{
    public partial class RequestOrderAssetApprovalList : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.RequestOrderAssetApproval;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboSearchFromUnit, BusinessObject.Reference.TransactionCode.PurchaseRequest, false);
                ComboBox.PopulateWithServiceUnitForTransaction(cboSearchToUnit, BusinessObject.Reference.TransactionCode.PurchaseOrder, false);

                txtRequestDate.SelectedDate = DateTime.Now.Date;
                txtRequestDate2.SelectedDate = DateTime.Now.Date;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = ItemTransactions;
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            ProcurementUtils.HideColumnPriceInfo(e.DetailTableView);

            //Load record
            GridDataItem dataItem = e.DetailTableView.ParentItem;

            var query = new ItemTransactionItemQuery("a");
            var iq = new ItemQuery("b");

            query.Select
                (
                    query,
                    iq.ItemName.As("ItemName"),
                    @"<CASE WHEN b.SRItemType <> '21' THEN '' ELSE 'Specification : '+ ISNULL(a.Specification, '') END AS 'AdditionalInfo'>"
                );

            query.LeftJoin(iq).On(query.ItemID == iq.ItemID);
            query.Where(query.TransactionNo == dataItem.GetDataKeyValue("TransactionNo").ToString());

            if (AppSession.Parameter.HealthcareInitial == "RSTJ")
                query.OrderBy(iq.ItemName.Ascending);
            else
                query.OrderBy(query.SequenceNo.Ascending);


            var dtb = query.LoadDataTable();

            e.DetailTableView.DataSource = dtb;
        }

        private DataTable ItemTransactions
        {
            get
            {
                var query = new ItemTransactionQuery("a");
                var qryserviceunit = new ServiceUnitQuery("b");
                var qryserviceunitto = new ServiceUnitQuery("c");
                var itemtype = new AppStandardReferenceItemQuery("d");
                var costunit = new ServiceUnitQuery("cu");

                query.Select
                    (
                        query.TransactionNo,
                        query.TransactionDate,
                        qryserviceunit.ServiceUnitName.As("FServiceUnitID"),
                        qryserviceunitto.ServiceUnitName.As("TServiceUnitID"),
                        costunit.ServiceUnitName.As("CostUnit"),
                        itemtype.ItemName,
                        query.IsInventoryItem,
                        query.IsApproved,
                        query.IsClosed,
                        query.Notes,
                        query.IsVoid,
                        query.FromLocationID,
                        query.SRItemType,
                        query.ApprovedDate,
                        query.ApprovedByUserID,
                        "<'RequestOrderDetail.aspx?md=view&id=' + a.TransactionNo + '&cons=0&itype=aa' AS PrUrl>"
                    );

                query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
                query.InnerJoin(qryserviceunitto).On(qryserviceunitto.ServiceUnitID == query.ToServiceUnitID);
                query.InnerJoin(costunit).On(costunit.ServiceUnitID == query.ServiceUnitCostID);
                query.LeftJoin(itemtype).On
                    (
                        itemtype.ItemID == query.SRItemType &&
                        itemtype.StandardReferenceID == "ItemType"
                    );
                query.Where
                    (
                        query.TransactionCode == TransactionCode.PurchaseRequest,
                        query.IsAssets == true,
                        query.IsApproved == true,
                        query.IsVoid == false,
                        query.Or(query.IsConsignmentAlreadyReceived.IsNull(), query.IsConsignmentAlreadyReceived == false)
                    );

                if (!string.IsNullOrEmpty(txtRequestNo.Text))
                {
                    string searchTextContain = string.Format("%{0}%", txtRequestNo.Text);
                    query.Where(query.TransactionNo.Like(searchTextContain));
                }
                if (!txtRequestDate.IsEmpty && !txtRequestDate2.IsEmpty)
                    query.Where(query.TransactionDate >= txtRequestDate.SelectedDate, query.TransactionDate <= txtRequestDate2.SelectedDate);
                if (!string.IsNullOrEmpty(cboSearchFromUnit.SelectedValue))
                    query.Where(query.FromServiceUnitID == cboSearchFromUnit.SelectedValue);
                if (!string.IsNullOrEmpty(cboSearchToUnit.SelectedValue))
                    query.Where(query.ToServiceUnitID == cboSearchToUnit.SelectedValue);

                query.OrderBy(query.TransactionDate.Ascending, query.TransactionNo.Ascending);

                var dtb = query.LoadDataTable();

                return dtb;
            }
        }

        protected void grdListPr_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdListPr.DataSource = PurchaseRequestPendings;
        }

        protected void grdListPr_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            ProcurementUtils.HideColumnPriceInfo(e.DetailTableView);

            GridDataItem dataItem = e.DetailTableView.ParentItem;

            var query = new ItemTransactionItemQuery("a");
            var iq = new ItemQuery("b");

            query.Select
                (
                    query,
                    iq.ItemName.As("ItemName"),
                    @"<CASE WHEN b.SRItemType <> '21' THEN '' ELSE 'Specification : '+ ISNULL(a.Specification, '') END AS 'AdditionalInfo'>"
                );

            query.LeftJoin(iq).On(query.ItemID == iq.ItemID);
            query.Where(query.TransactionNo == dataItem.GetDataKeyValue("TransactionNo").ToString());

            if (AppSession.Parameter.HealthcareInitial == "RSTJ")
                query.OrderBy(iq.ItemName.Ascending);
            else
                query.OrderBy(query.SequenceNo.Ascending);


            var dtb = query.LoadDataTable();

            e.DetailTableView.DataSource = dtb;
        }

        private DataTable PurchaseRequestPendings
        {
            get
            {
                var query = new ItemTransactionQuery("a");
                var qryserviceunit = new ServiceUnitQuery("b");
                var qryserviceunitto = new ServiceUnitQuery("c");
                var itemtype = new AppStandardReferenceItemQuery("d");
                var costunit = new ServiceUnitQuery("cu");

                query.Select
                    (
                        query.TransactionNo,
                        query.TransactionDate,
                        qryserviceunit.ServiceUnitName.As("FServiceUnitID"),
                        qryserviceunitto.ServiceUnitName.As("TServiceUnitID"),
                        costunit.ServiceUnitName.As("CostUnit"),
                        itemtype.ItemName,
                        query.IsInventoryItem,
                        query.IsApproved,
                        query.IsClosed,
                        query.Notes,
                        query.IsVoid,
                        query.FromLocationID,
                        query.SRItemType,
                        "<'RequestOrderDetail.aspx?md=view&id=' + a.TransactionNo + '&cons=0&itype=aa' AS PrUrl>"
                    );

                query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
                query.InnerJoin(qryserviceunitto).On(qryserviceunitto.ServiceUnitID == query.ToServiceUnitID);
                query.InnerJoin(costunit).On(costunit.ServiceUnitID == query.ServiceUnitCostID);
                query.LeftJoin(itemtype).On
                    (
                        itemtype.ItemID == query.SRItemType &&
                        itemtype.StandardReferenceID == "ItemType"
                    );

                query.Where
                    (
                        query.TransactionCode == TransactionCode.PurchaseRequest,
                        query.IsAssets == true,
                        query.IsApproved == false,
                        query.IsVoid == false,
                        query.Or(query.IsConsignmentAlreadyReceived.IsNull(), query.IsConsignmentAlreadyReceived == false)
                    );

                if (!string.IsNullOrEmpty(txtRequestNo.Text))
                {
                    string searchTextContain = string.Format("%{0}%", txtRequestNo.Text);
                    query.Where(query.TransactionNo.Like(searchTextContain));
                }
                if (!txtRequestDate.IsEmpty && !txtRequestDate2.IsEmpty)
                    query.Where(query.TransactionDate >= txtRequestDate.SelectedDate, query.TransactionDate <= txtRequestDate2.SelectedDate);
                if (!string.IsNullOrEmpty(cboSearchFromUnit.SelectedValue))
                    query.Where(query.FromServiceUnitID == cboSearchFromUnit.SelectedValue);
                if (!string.IsNullOrEmpty(cboSearchToUnit.SelectedValue))
                    query.Where(query.ToServiceUnitID == cboSearchToUnit.SelectedValue);

                query.OrderBy(query.TransactionDate.Ascending, query.TransactionNo.Ascending);


                var dtb = query.LoadDataTable();


                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            SaveValueToCookie();
            grdListPr.Rebind();
            grdList.Rebind();
        }
    }
}