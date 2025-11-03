using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Inventory.Procurement
{
    public partial class PurchaseOrderUnApprovalList : BasePage
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
            ProgramID = AppConstant.Program.PurchaseOrderUnApproval;
            if (!IsPostBack)
            {
                txtFromDate.SelectedDate = (new DateTime()).NowAtSqlServer();
                txtToDate.SelectedDate = (new DateTime()).NowAtSqlServer();

                ComboBox.PopulateWithServiceUnitForTransaction(cboPurchasingUnitID, BusinessObject.Reference.TransactionCode.PurchaseOrder, true);
                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void cboSupplier_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.SupplierItemsRequested((RadComboBox)sender, e.Text);
        }

        protected void cboSupplierID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.SupplierItemDataBound(e);
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

        private DataTable ItemTransactions()
        {
            var isEmptyFilter = txtFromDate.IsEmpty && txtToDate.IsEmpty && string.IsNullOrEmpty(txtPONo.Text) && string.IsNullOrEmpty(cboPurchasingUnitID.SelectedValue) && string.IsNullOrEmpty(cboSupplierID.SelectedValue) && string.IsNullOrEmpty(cboSRItemType.SelectedValue);
            if (!ValidateSearch(isEmptyFilter, "Purchase Order Cancellation")) return null;

            var hd = new ItemTransactionQuery("a");
            var supp = new SupplierQuery("b");
            var usr = new AppUserServiceUnitQuery("c");
            var su = new ServiceUnitQuery("d");
            var std = new AppStandardReferenceItemQuery("e");
            var hdreff = new ItemTransactionQuery("hdreff");

            hd.es.Top = AppSession.Parameter.MaxResultRecord;

            hd.Select(
                hd.TransactionNo,
                hd.TransactionDate,
                supp.SupplierName,
                su.ServiceUnitName,
                std.ItemName.As("ItemType")
                );
            hd.InnerJoin(supp).On(hd.BusinessPartnerID == supp.SupplierID);
            hd.InnerJoin(usr).On(hd.FromServiceUnitID == usr.ServiceUnitID && usr.UserID == AppSession.UserLogin.UserID);
            hd.InnerJoin(su).On(hd.FromServiceUnitID == su.ServiceUnitID);
            hd.InnerJoin(std).On(hd.SRItemType == std.ItemID && std.StandardReferenceID == "ItemType");
            hd.LeftJoin(hdreff).On(hd.TransactionNo == hdreff.ReferenceNo && hdreff.IsVoid == false);

            if (!txtFromDate.IsEmpty && !txtToDate.IsEmpty)
                hd.Where(hd.TransactionDate.Between(txtFromDate.SelectedDate.Value.Date, txtToDate.SelectedDate.Value.Date));
            if (!string.IsNullOrEmpty(txtPONo.Text))
                hd.Where(hd.TransactionNo == txtPONo.Text);
            if (!string.IsNullOrEmpty(cboSupplierID.SelectedValue))
                hd.Where(hd.BusinessPartnerID == cboSupplierID.SelectedValue);
            if (!string.IsNullOrEmpty(cboPurchasingUnitID.SelectedValue))
                hd.Where(hd.FromServiceUnitID == cboPurchasingUnitID.SelectedValue);
            if (!string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                hd.Where(hd.SRItemType == cboSRItemType.SelectedValue);

            hd.Where(
                hd.TransactionCode == BusinessObject.Reference.TransactionCode.PurchaseOrder,
                hd.IsApproved == true,
                hd.IsClosed == false,
                hdreff.TransactionNo.IsNull()
                );

            hd.OrderBy(hd.TransactionDate.Ascending, hd.TransactionNo.Ascending);

            var tab = hd.LoadDataTable();

            return tab;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = ItemTransactions();
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "validate")
            {
                GridItem item = e.Item as GridItem;
                if (item == null) return;

                var transNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemTransactionMetadata.ColumnNames.TransactionNo]);

                var entity = new ItemTransaction();
                entity.LoadByPrimaryKey(transNo);

                if (entity.IsNonMasterOrder == true)
                {
                    entity.IsApproved = false;
                    entity.ApprovedDate = (new DateTime()).NowAtSqlServer();
                    entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                    entity.IsVoid = true;
                    entity.VoidDate = (new DateTime()).NowAtSqlServer();
                    entity.VoidByUserID = AppSession.UserLogin.UserID;
                    entity.Save();
                }
                else
                {
                    var coll = new ItemTransactionItemCollection();
                    coll.Query.Where(coll.Query.TransactionNo == transNo);
                    coll.LoadAll();

                    (new ItemTransaction()).UnApprove(transNo, coll, AppSession.UserLogin.UserID);

                    entity.IsVoid = true;
                    entity.VoidDate = (new DateTime()).NowAtSqlServer();
                    entity.VoidByUserID = AppSession.UserLogin.UserID;
                    entity.Save();
                }

                //var atc = new ApprovalTransactionCollection();
                //atc.Query.Where(atc.Query.TransactionNo == transNo);
                //atc.LoadAll();
                //atc.MarkAllAsDeleted();
                //atc.Save();
                
                //rebind data
                grdList.Rebind();
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            e.DetailTableView.DataSource = ItemTransactionItems(e.DetailTableView.ParentItem.GetDataKeyValue("TransactionNo").ToString());
        }

        private DataTable ItemTransactionItems(string transactionNo)
        {
            var query = new ItemTransactionItemQuery("a");
            var item = new ItemQuery("b");

            query.Select
                (
                    query.TransactionNo,
                    query.SequenceNo,
                    query.ItemID,
                    @"<ISNULL(b.ItemName, a.[Description]) AS ItemName>",
                    query.Quantity,
                    query.SRItemUnit
                );
            query.LeftJoin(item).On(query.ItemID == item.ItemID);
            query.Where(
                query.TransactionNo == transactionNo
                );

            var tab = query.LoadDataTable();

            return tab;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (sourceControl is RadGrid)
            {
                if (eventArgument.Split('|')[0] == "void")
                {
                    var transNo = eventArgument.Split('|')[1];

                    var entity = new ItemTransaction();
                    entity.LoadByPrimaryKey(transNo);

                    if (entity.IsNonMasterOrder == true)
                    {
                        entity.IsApproved = false;
                        entity.ApprovedDate = (new DateTime()).NowAtSqlServer();
                        entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                        entity.IsVoid = true;
                        entity.VoidDate = (new DateTime()).NowAtSqlServer();
                        entity.VoidByUserID = AppSession.UserLogin.UserID;
                        entity.Notes += " VOID : " + eventArgument.Split('|')[2]; 
                        entity.Save();
                    }
                    else
                    {
                        var coll = new ItemTransactionItemCollection();
                        coll.Query.Where(coll.Query.TransactionNo == transNo);
                        coll.LoadAll();

                        (new ItemTransaction()).UnApprove(transNo, coll, AppSession.UserLogin.UserID);

                        entity.IsVoid = true;
                        entity.VoidDate = (new DateTime()).NowAtSqlServer();
                        entity.VoidByUserID = AppSession.UserLogin.UserID;
                        entity.Notes += " VOID : " + eventArgument.Split('|')[2];
                        entity.Save();
                    }

                    //rebind data
                    grdList.Rebind();
                }
            }
        }
    }
}
