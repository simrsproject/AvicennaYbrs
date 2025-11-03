using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class InventoryIssueOrderList : BasePage
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
            ProgramID = AppConstant.Program.InventoryIssue;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboSearchToUnit, BusinessObject.Reference.TransactionCode.InventoryIssueOut, true);
                ComboBox.PopulateWithServiceUnitForTransaction(cboSearchFromUnit, BusinessObject.Reference.TransactionCode.InventoryIssueRequestOut, false);

                ComboBox.PopulateWithServiceUnitForTransaction(cboSearchFromUnit2, BusinessObject.Reference.TransactionCode.InventoryIssueOut, true);

                ComboBox.PopulateWithItemTypeProduct(cboSRItemTypePr);
                ComboBox.PopulateWithItemTypeProduct(cboSRItemTypePr2);

                txtFromRequestDate.SelectedDate = DateTime.Now;
                txtFromRequestDate2.SelectedDate = DateTime.Now;
                txtToRequestDate.SelectedDate = DateTime.Now;
                txtToRequestDate2.SelectedDate = DateTime.Now;

                if (AppSession.Parameter.IsInventoryIssueListByTransactionDate)
                    rblFilterDateBy.SelectedIndex = 0;
                else
                    rblFilterDateBy.SelectedIndex = 1;
            }
        }

        protected void btnFilterPR_Click(object sender, ImageClickEventArgs e)
        {
            if (RadTabStrip1.SelectedIndex == 0) grdList.Rebind();
            else grdList2.Rebind();
        }

        protected void rblFilterDateBy_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            grdList.Rebind();
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

        protected void grdList2_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = ItemTransactions2;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            e.DetailTableView.DataSource = InventoryIssueRequestItemPendings(e.DetailTableView.ParentItem.GetDataKeyValue("TransactionNo").ToString());
        }

        private DataTable ItemTransactions
        {
            get
            {
                var isEmptyFilter = txtFromRequestDate.IsEmpty && txtToRequestDate.IsEmpty && string.IsNullOrEmpty(txtRequestNo.Text) && string.IsNullOrEmpty(cboSearchToUnit.SelectedValue) && string.IsNullOrEmpty(cboSearchFromUnit.SelectedValue) && string.IsNullOrEmpty(cboSRItemTypePr.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Inventory Issue")) return null;

                var query = new ItemTransactionQuery("a");
                var iti = new ItemTransactionItemQuery("b");
                var su = new ServiceUnitQuery("c");
                var itemtype = new AppStandardReferenceItemQuery("d");
                var user = new AppUserServiceUnitQuery("e");
                var tounit = new ServiceUnitQuery("f");

                query.InnerJoin(iti).On(iti.TransactionNo == query.TransactionNo);
                query.InnerJoin(su).On(su.ServiceUnitID == query.FromServiceUnitID);
                query.InnerJoin(tounit).On(query.ToServiceUnitID == tounit.ServiceUnitID);
                query.InnerJoin(user).On(query.ToServiceUnitID == user.ServiceUnitID &&
                                         user.UserID == AppSession.UserLogin.UserID);
                query.LeftJoin(itemtype).On
                    (
                        itemtype.ItemID == query.SRItemType &&
                        itemtype.StandardReferenceID == AppEnum.StandardReference.ItemType.ToString()
                    );
                query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.InventoryIssueRequestOut,
                            query.IsApproved == true, //query.ReferenceNo == string.Empty, 
                            iti.IsClosed == false);
                
                
                query.es.Distinct = true;
                query.Select
                    (
                        "<'InventoryIssueDetail.aspx?md=new&req=' + a.TransactionNo + '&rod=&list=y' AS PoUrl>",
                        query.TransactionNo,
                        query.TransactionDate,
                        su.ServiceUnitName,
                        tounit.ServiceUnitName.As("ToUnit"),
                        itemtype.ItemName,
                        query.IsApproved,
                        query.ApprovedByUserID,
                        query.ApprovedDate,
                        query.Notes,
                        query.IsVoid
                   );

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                if (!txtFromRequestDate.IsEmpty && !txtToRequestDate.IsEmpty)
                {
                    if (rblFilterDateBy.SelectedValue == "REQ")
                        query.Where(query.TransactionDate.Between(txtFromRequestDate.SelectedDate.Value.Date, txtToRequestDate.SelectedDate.Value.Date));
                    else
                        query.Where(query.ApprovedDate >= txtFromRequestDate.SelectedDate.Value.Date, query.ApprovedDate < txtToRequestDate.SelectedDate.Value.Date.AddDays(1));
                }

                if (!string.IsNullOrEmpty(txtRequestNo.Text)) query.Where(query.TransactionNo == txtRequestNo.Text);
                if (!string.IsNullOrEmpty(cboSearchToUnit.SelectedValue)) query.Where(query.ToServiceUnitID == cboSearchToUnit.SelectedValue);
                if (!string.IsNullOrEmpty(cboSearchFromUnit.SelectedValue)) query.Where(query.FromServiceUnitID == cboSearchFromUnit.SelectedValue);
                if (!string.IsNullOrEmpty(cboSRItemTypePr.SelectedValue)) query.Where(query.SRItemType == cboSRItemTypePr.SelectedValue);

                if (rblFilterDateBy.SelectedValue == "REQ")
                    query.OrderBy(query.TransactionDate.Descending);
                else
                    query.OrderBy(query.ApprovedDate.Descending);

                DataTable dtb = query.LoadDataTable();

                foreach (DataRow row in dtb.Rows.Cast<DataRow>().Where(row => InventoryIssueRequestItemPendings(row["TransactionNo"].ToString()).Rows.Count == 0))
                {
                    row.Delete();
                }

                return dtb;
            }
        }

        private DataTable InventoryIssueRequestItemPendings(string transactionNo)
        {
            var query = new ItemTransactionItemQuery("a");
            var iq = new ItemQuery("b");
            query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
            query.Where(query.TransactionNo == transactionNo);
            query.OrderBy(query.ItemID.Ascending);

            query.Select
                (
                    query.ItemID,
                    query.SRItemUnit,
                    query.ConversionFactor,
                    query.Quantity,
                    query.SequenceNo,
                    query.IsClosed,
                    iq.ItemName.As("ItemName"),
                    @"<(ISNULL((SELECT SUM(iti.Quantity * iti.ConversionFactor) AS Qty 
FROM ItemTransactionItem AS iti
INNER JOIN ItemTransaction AS it ON it.TransactionNo = iti.TransactionNo
WHERE it.TransactionCode = '082' AND it.IsVoid = 0 AND iti.ReferenceNo = a.TransactionNo AND iti.ReferenceSequenceNo = a.SequenceNo), 0))/a.ConversionFactor AS QtyFinished>"
                );
            query.Where(@"<(a.Quantity * a.ConversionFactor) - (ISNULL((SELECT SUM(iti.Quantity * iti.ConversionFactor) AS Qty 
FROM ItemTransactionItem AS iti
INNER JOIN ItemTransaction AS it ON it.TransactionNo = iti.TransactionNo
WHERE it.TransactionCode = '082' AND it.IsVoid = 0 AND iti.ReferenceNo = a.TransactionNo AND iti.ReferenceSequenceNo = a.SequenceNo), 0)) > 0 >");

            var dtb = query.LoadDataTable();
            return dtb;
        }

        private DataTable ItemTransactions2
        {
            get
            {
                var isEmptyFilter = txtFromRequestDate2.IsEmpty && txtToRequestDate2.IsEmpty && string.IsNullOrEmpty(txtIssueNo.Text) && string.IsNullOrEmpty(txtRequestNo2.Text) && string.IsNullOrEmpty(cboSearchFromUnit2.SelectedValue) && string.IsNullOrEmpty(cboSearchFromLocation2.SelectedValue) && string.IsNullOrEmpty(cboSRItemTypePr2.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Inventory Issue")) return null;

                ItemTransactionQuery query = new ItemTransactionQuery("a");
                var qryserviceunit = new ServiceUnitQuery("c");
                var itemtype = new AppStandardReferenceItemQuery("d");
                var user = new AppUserServiceUnitQuery("e");
                var tounit = new ServiceUnitQuery("f");
                var floc = new LocationQuery("g");

                query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
                query.InnerJoin(user).On(query.FromServiceUnitID == user.ServiceUnitID &&
                                         user.UserID == AppSession.UserLogin.UserID);
                query.LeftJoin(itemtype).On
                    (
                        itemtype.ItemID == query.SRItemType &&
                        itemtype.StandardReferenceID == AppEnum.StandardReference.ItemType.ToString()
                    );
                query.LeftJoin(tounit).On(query.ToServiceUnitID == tounit.ServiceUnitID);
                query.InnerJoin(floc).On(query.FromLocationID == floc.LocationID);
                query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.InventoryIssueOut);
                query.OrderBy(query.TransactionDate.Descending);

                query.Select
                    (
                        "<'InventoryIssueDetail.aspx?md=view&id=' + a.TransactionNo + '&req=&rod=&list=y' AS PoUrl>",
                        query.TransactionNo,
                        query.TransactionDate,
                        qryserviceunit.ServiceUnitName,
                        floc.LocationName,
                        tounit.ServiceUnitName.As("ToUnit"),
                        itemtype.ItemName,
                        query.IsApproved,
                        query.ApprovedByUserID,
                        query.ApprovedDate,
                        query.Notes,
                        query.IsVoid, query.ReferenceNo
                   );

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                if (!txtFromRequestDate2.IsEmpty && !txtToRequestDate2.IsEmpty) query.Where(query.TransactionDate.Between(txtFromRequestDate2.SelectedDate.Value.Date, txtToRequestDate2.SelectedDate.Value.Date));
                if (!string.IsNullOrEmpty(txtIssueNo.Text)) query.Where(query.TransactionNo == txtIssueNo.Text);
                if (!string.IsNullOrEmpty(txtRequestNo2.Text)) query.Where(query.ReferenceNo == txtRequestNo2.Text);
                if (!string.IsNullOrEmpty(cboSearchFromUnit2.SelectedValue)) query.Where(query.FromServiceUnitID == cboSearchFromUnit2.SelectedValue);
                if (!string.IsNullOrEmpty(cboSearchFromLocation2.SelectedValue)) query.Where(query.FromLocationID == cboSearchFromLocation2.SelectedValue);
                if (!string.IsNullOrEmpty(cboSRItemTypePr2.SelectedValue)) query.Where(query.SRItemType == cboSRItemTypePr2.SelectedValue);

                return query.LoadDataTable();
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (sourceControl is RadGrid && eventArgument == "rebind1") grdList.Rebind();
            else if (sourceControl is RadGrid && eventArgument == "rebind2") grdList2.Rebind();
        }

        protected void cboSearchFromUnit2_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ComboBox.PopulateWithServiceUnitForLocation(cboSearchFromLocation2, e.Value);
        }
    }
}
