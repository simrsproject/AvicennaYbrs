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
    public partial class RequestOrderUnApprovalList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.RequestOrderUnApproval;
            if (!IsPostBack)
            {
                txtFromDate.SelectedDate = (new DateTime()).NowAtSqlServer();
                txtToDate.SelectedDate = (new DateTime()).NowAtSqlServer();

                ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, BusinessObject.Reference.TransactionCode.PurchaseRequest, true);
                ComboBox.PopulateWithServiceUnitForTransaction(cboPurchasingUnitID, BusinessObject.Reference.TransactionCode.PurchaseOrder, false);
                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        private DataTable ItemTransactions()
        {
            var hd = new ItemTransactionQuery("a");
            var supp = new SupplierQuery("b");
            var usr = new AppUserServiceUnitQuery("c");
            var su = new ServiceUnitQuery("d");
            var std = new AppStandardReferenceItemQuery("e");
            var dt = new ItemTransactionItemQuery("f");
            var fsu = new ServiceUnitQuery("g");

            hd.es.Top = AppSession.Parameter.MaxResultRecord;
            hd.es.Distinct = true;

            hd.Select(
                hd.TransactionNo,
                hd.TransactionDate,
                supp.SupplierName,
                fsu.ServiceUnitName.As("FromServiceUnitName"),
                su.ServiceUnitName,
                std.ItemName.As("ItemType")
                );
            hd.LeftJoin(supp).On(hd.BusinessPartnerID == supp.SupplierID);
            hd.InnerJoin(usr).On(hd.FromServiceUnitID == usr.ServiceUnitID && usr.UserID == AppSession.UserLogin.UserID);
            hd.InnerJoin(fsu).On(hd.FromServiceUnitID == fsu.ServiceUnitID);
            hd.InnerJoin(su).On(hd.ToServiceUnitID == su.ServiceUnitID);
            hd.InnerJoin(std).On(hd.SRItemType == std.ItemID && std.StandardReferenceID == "ItemType");
            hd.InnerJoin(dt).On(hd.TransactionNo == dt.TransactionNo);

            if (!txtFromDate.IsEmpty && !txtToDate.IsEmpty)
                hd.Where(hd.TransactionDate.Between(txtFromDate.SelectedDate.Value.Date, txtToDate.SelectedDate.Value.Date));
            if (!string.IsNullOrEmpty(txtRONo.Text))
                hd.Where(hd.TransactionNo == txtRONo.Text);
            if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                hd.Where(hd.FromServiceUnitID == cboFromServiceUnitID.SelectedValue);
            if (!string.IsNullOrEmpty(cboPurchasingUnitID.SelectedValue))
                hd.Where(hd.ToServiceUnitID == cboPurchasingUnitID.SelectedValue);
            if (!string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                hd.Where(hd.SRItemType == cboSRItemType.SelectedValue);

            hd.Where(
                hd.TransactionCode == BusinessObject.Reference.TransactionCode.PurchaseRequest,
                hd.IsApproved == true,
                hd.IsClosed == false,
                dt.RequestQty.IsNotNull()
                );

            hd.OrderBy(hd.TransactionDate.Ascending, hd.TransactionNo.Ascending);

            var tab = hd.LoadDataTable();

            return tab;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = ItemTransactions();
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

                    var porDtQ = new ItemTransactionItemQuery("a");
                    var porQ = new ItemTransactionQuery("b");
                    porDtQ.InnerJoin(porQ).On(porQ.TransactionNo == porDtQ.TransactionNo);
                    porDtQ.Where(porDtQ.ReferenceNo == transNo, porQ.IsVoid == false);
                    DataTable dtb = porDtQ.LoadDataTable();
                    if (dtb.Rows.Count > 0)
                    {
                        pnlInfo.Visible = true;
                        lblInfo.Text = "This transaction can't be canceled, this data has been proceed to PO.";
                        grdList.Rebind();
                        return;
                    }

                    var entity = new ItemTransaction();
                    entity.LoadByPrimaryKey(transNo);
                    entity.IsApproved = false;
                    entity.ApprovedDate = (new DateTime()).NowAtSqlServer();
                    entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                    entity.IsVoid = true;
                    entity.VoidDate = (new DateTime()).NowAtSqlServer();
                    entity.VoidByUserID = AppSession.UserLogin.UserID;
                    entity.Notes += " VOID : " + eventArgument.Split('|')[2];
                    entity.Save();

                    //rebind data
                    grdList.Rebind();
                }
            }
        }
    }
}
