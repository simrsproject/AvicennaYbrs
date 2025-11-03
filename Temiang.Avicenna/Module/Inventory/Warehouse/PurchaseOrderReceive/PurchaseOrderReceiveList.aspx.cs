using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class PurchaseOrderReceiveList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        private bool IsGrantsReceiving {
            get {
                return (Request.QueryString["grants"] == "1");
            }
        }

        private bool IsDirectPurchase
        {
            get
            {
                return (Request.QueryString["grants"] == "2");
            }
        }
        protected string IsPoWithThreeTypesOfTaxes
        {
            get { return (string)ViewState["_poType" + Request.UserHostName]; }
            set { ViewState["_poType" + Request.UserHostName] = value; }
        }

        private void InitGrantsReceive() {
            trPoRef.Visible = !IsGrantsReceiving && !IsDirectPurchase;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = Request.QueryString["cons"] == "0"
                            ? AppConstant.Program.ReceivingOrder
                            : AppConstant.Program.ReceivingOrderConsignment;
            ProgramID = IsGrantsReceiving ? AppConstant.Program.GrantsReceive : (IsDirectPurchase ? AppConstant.Program.DirectPurchase : ProgramID);

            InitGrantsReceive();

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboPurchasingUnitID, TransactionCode.PurchaseOrderReceive, true);
                cboSearchStatus.Items.Add(new RadComboBoxItem("", ""));
                cboSearchStatus.Items.Add(new RadComboBoxItem("Not Approved Yet", "0"));
                cboSearchStatus.Items.Add(new RadComboBoxItem("Approved", "1"));
                cboSearchStatus.Items.Add(new RadComboBoxItem("Void", "4"));

                txtFromDate.SelectedDate = (new DateTime()).NowAtSqlServer(); //DateTime.Now;
                txtToDate.SelectedDate = (new DateTime()).NowAtSqlServer(); //DateTime.Now;

                grdListIm.Columns.FindByUniqueName("ReferenceNo").Visible = !IsGrantsReceiving && !IsDirectPurchase;
                grdListIm.Columns.FindByUniqueName("IsConsignment").Visible = !IsGrantsReceiving && !IsDirectPurchase;
                grdListInm.Columns.FindByUniqueName("ReferenceNo").Visible = !IsGrantsReceiving && !IsDirectPurchase;
                grdListIk.Columns.FindByUniqueName("ReferenceNo").Visible = !IsGrantsReceiving && !IsDirectPurchase;
                
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

        protected void grdListIm_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
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

        protected void grdListInm_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = ItemTransactionsInm;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
        }

        protected void grdListIk_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = ItemTransactionsIk;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string transNo = dataItem.GetDataKeyValue("TransactionNo").ToString();
            //Load record
            var query = new ItemTransactionItemQuery("a");
            var iq = new ItemQuery("b");
            var hdq = new ItemTransactionQuery("c");
            var vwItemQ = new VwItemProductMedicNonMedicQuery("d");

            query.LeftJoin(iq).On(query.ItemID == iq.ItemID);
            query.InnerJoin(hdq).On(query.TransactionNo == hdq.TransactionNo);
            query.LeftJoin(vwItemQ).On(query.ItemID == vwItemQ.ItemID);

            query.Where(query.TransactionNo == transNo);
            query.OrderBy(query.ItemID.Ascending);

            query.Select
                (
                    query.TransactionNo,
                    query.SequenceNo,
                    query.ItemID,
                    query.SRItemUnit,
                    query.Quantity,
                    query.IsClosed,
                    iq.ItemName.As("ItemName"),
                    query.BatchNumber,
                    query.ExpiredDate,
                    query.Price,
                    query.ConversionFactor,
                    query.IsBonusItem,
                    query.Description,
                    query.Discount1Percentage,
                    query.Discount2Percentage,
                    query.Discount,
                    hdq.SRItemType,
                    @"<ISNULL(d.IsControlExpired, 0) AS IsControlExpired>"
                );

            DataTable dtb = query.LoadDataTable();
            //Apply
            e.DetailTableView.DataSource = dtb;
        }

        private DataTable ItemTransactions
        {
            get
            {
                var isEmptyFilter = txtFromDate.IsEmpty && txtToDate.IsEmpty && string.IsNullOrEmpty(txtTransactionNo.Text) && string.IsNullOrEmpty(txtReferenceNo.Text) && string.IsNullOrEmpty(txtInvoice.Text) && txtInvoiceSupplierDate.IsEmpty && string.IsNullOrEmpty(cboPurchasingUnitID.SelectedValue) && string.IsNullOrEmpty(cboSearchSupplier.SelectedValue) && string.IsNullOrEmpty(cboSearchStatus.SelectedValue);

                if (Request.QueryString["cons"] == "0" && Request.QueryString["grants"] == "1")
                    if (!ValidateSearch(isEmptyFilter, "Grants Receive")) return null;
                if (Request.QueryString["cons"] == "0" && Request.QueryString["grants"] == "2")
                    if (!ValidateSearch(isEmptyFilter, "Direct Purchase")) return null;
                if (Request.QueryString["cons"] == "1")
                    if (!ValidateSearch(isEmptyFilter, "Purchase Order Receive (Consignment)")) return null;
                if (Request.QueryString["cons"] == "0")
                    if (!ValidateSearch(isEmptyFilter, "Purchase Order Receive")) return null;

                var query = new ItemTransactionQuery("a");
                var qryserviceunit = new ServiceUnitQuery("c");
                var sup = new SupplierQuery("b");
                var itemtype = new AppStandardReferenceItemQuery("d");
                var qusr = new AppUserServiceUnitQuery("u");

                query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.ToServiceUnitID);
                query.LeftJoin(sup).On(query.BusinessPartnerID == sup.SupplierID);
                query.LeftJoin(itemtype).On(itemtype.ItemID == query.SRItemType && itemtype.StandardReferenceID == "ItemType");
                query.InnerJoin(qusr).On(query.ToServiceUnitID == qusr.ServiceUnitID &&
                                qusr.UserID == AppSession.UserLogin.UserID);
                query.Where(query.TransactionCode == (IsGrantsReceiving ? TransactionCode.GrantsReceive : (IsDirectPurchase ? TransactionCode.DirectPurchase : TransactionCode.PurchaseOrderReceive)),
                            query.SRItemType == ItemType.Medical);

                query.Select(
                       query.TransactionNo,
                       query.TransactionDate,
                       query.ReferenceNo,
                       qryserviceunit.ServiceUnitName.As("TServiceUnitID"),
                       sup.SupplierName,
                       itemtype.ItemName,
                       query.IsApproved,
                       query.ReferenceNo,
                       query.Notes,
                       query.IsVoid,
                       query.InvoiceNo,
                       query.InvoiceSupplierDate,
                       query.DeliveryOrdersNo,
                       query.ChargesAmount,
                       query.IsConsignment
                   );

                   query.Select(Request.QueryString["cons"] == "0"
                               ? string.Format("<'PurchaseOrderReceiveDetail.aspx?md=view&id=' + a.TransactionNo + '&type=2&cons=0&grants={0}' AS PorUrl>", Request.QueryString["grants"])
                               : "<'PurchaseOrderReceiveDetail.aspx?md=view&id=' + a.TransactionNo + '&type=2&cons=1' AS PorUrl>");

                if (!txtFromDate.IsEmpty && !txtToDate.IsEmpty)
                    query.Where(query.TransactionDate.Between(txtFromDate.SelectedDate, txtToDate.SelectedDate));
                if (!string.IsNullOrEmpty(txtTransactionNo.Text))
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                if (!string.IsNullOrEmpty(txtReferenceNo.Text))
                    query.Where(query.ReferenceNo == txtReferenceNo.Text);
                if (!string.IsNullOrEmpty(txtInvoice.Text))
                    query.Where(query.InvoiceNo == txtInvoice.Text);
                if (!txtInvoiceSupplierDate.IsEmpty)
                    query.Where(query.InvoiceSupplierDate == txtInvoiceSupplierDate.SelectedDate);
                if (!string.IsNullOrEmpty(cboSearchSupplier.SelectedValue))
                    query.Where(query.BusinessPartnerID == cboSearchSupplier.SelectedValue);
                if (!string.IsNullOrEmpty(cboPurchasingUnitID.SelectedValue))
                    query.Where(query.ToServiceUnitID == cboPurchasingUnitID.SelectedValue);
                if (cboSearchStatus.SelectedValue == "0")
                    query.Where(query.IsApproved == false);
                if (cboSearchStatus.SelectedValue == "1")
                    query.Where(query.IsApproved == true);
                if (cboSearchStatus.SelectedValue == "4")
                    query.Where(query.IsVoid == true);
                if (Request.QueryString["cons"] == "1")
                    query.Where(query.IsConsignment == true);

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.OrderBy(query.ToServiceUnitID.Ascending, query.TransactionDate.Descending,
                              query.TransactionNo.Descending);

                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        private DataTable ItemTransactionsInm
        {
            get
            {
                var isEmptyFilter = txtFromDate.IsEmpty && txtToDate.IsEmpty && string.IsNullOrEmpty(txtTransactionNo.Text) && string.IsNullOrEmpty(txtReferenceNo.Text) && string.IsNullOrEmpty(txtInvoice.Text) && txtInvoiceSupplierDate.IsEmpty && string.IsNullOrEmpty(cboPurchasingUnitID.SelectedValue) && string.IsNullOrEmpty(cboSearchSupplier.SelectedValue) && string.IsNullOrEmpty(cboSearchStatus.SelectedValue);

                if (Request.QueryString["cons"] == "0" && Request.QueryString["grants"] == "1")
                    if (!ValidateSearch(isEmptyFilter, "Grants Receive")) return null;
                if (Request.QueryString["cons"] == "0" && Request.QueryString["grants"] == "2")
                    if (!ValidateSearch(isEmptyFilter, "Direct Purchase")) return null;
                if (Request.QueryString["cons"] == "1")
                    if (!ValidateSearch(isEmptyFilter, "Purchase Order Receive (Consignment)")) return null;
                if (Request.QueryString["cons"] == "0")
                    if (!ValidateSearch(isEmptyFilter, "Purchase Order Receive")) return null;

                var query = new ItemTransactionQuery("a");
                var qryserviceunit = new ServiceUnitQuery("c");
                var sup = new SupplierQuery("b");
                var itemtype = new AppStandardReferenceItemQuery("d");
                var qusr = new AppUserServiceUnitQuery("u");

                query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.ToServiceUnitID);
                query.LeftJoin(sup).On(query.BusinessPartnerID == sup.SupplierID);
                query.LeftJoin(itemtype).On(itemtype.ItemID == query.SRItemType && itemtype.StandardReferenceID == "ItemType");
                query.InnerJoin(qusr).On(query.ToServiceUnitID == qusr.ServiceUnitID &&
                                qusr.UserID == AppSession.UserLogin.UserID);
                query.Where(query.TransactionCode == (IsGrantsReceiving ? TransactionCode.GrantsReceive : (IsDirectPurchase ? TransactionCode.DirectPurchase : TransactionCode.PurchaseOrderReceive)),
                            query.SRItemType == ItemType.NonMedical);

                query.Select(
                       query.TransactionNo,
                       query.TransactionDate,
                       query.ReferenceNo,
                       qryserviceunit.ServiceUnitName.As("TServiceUnitID"),
                       sup.SupplierName,
                       itemtype.ItemName,
                       query.IsApproved,
                       query.ReferenceNo,
                       query.Notes,
                       query.IsVoid,
                       query.InvoiceNo,
                       query.InvoiceSupplierDate,
                       query.DeliveryOrdersNo,
                       query.ChargesAmount,
                       query.IsConsignment
                   );

                query.Select(Request.QueryString["cons"] == "0"
                        ? "<'PurchaseOrderReceiveDetail.aspx?md=view&id=' + a.TransactionNo + '&type=2&cons=0' AS PorUrl>"
                        : "<'PurchaseOrderReceiveDetail.aspx?md=view&id=' + a.TransactionNo + '&type=2&cons=1' AS PorUrl>");

                if (!txtFromDate.IsEmpty && !txtToDate.IsEmpty)
                    query.Where(query.TransactionDate.Between(txtFromDate.SelectedDate, txtToDate.SelectedDate));
                if (!string.IsNullOrEmpty(txtTransactionNo.Text))
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                if (!string.IsNullOrEmpty(txtReferenceNo.Text))
                    query.Where(query.ReferenceNo == txtReferenceNo.Text);
                if (!string.IsNullOrEmpty(txtInvoice.Text))
                    query.Where(query.InvoiceNo == txtInvoice.Text);
                if (!txtInvoiceSupplierDate.IsEmpty)
                    query.Where(query.InvoiceSupplierDate == txtInvoiceSupplierDate.SelectedDate);
                if (!string.IsNullOrEmpty(cboSearchSupplier.SelectedValue))
                    query.Where(query.BusinessPartnerID == cboSearchSupplier.SelectedValue);
                if (!string.IsNullOrEmpty(cboPurchasingUnitID.SelectedValue))
                    query.Where(query.ToServiceUnitID == cboPurchasingUnitID.SelectedValue);
                if (cboSearchStatus.SelectedValue == "0")
                    query.Where(query.IsApproved == false);
                if (cboSearchStatus.SelectedValue == "1")
                    query.Where(query.IsApproved == true);
                if (cboSearchStatus.SelectedValue == "4")
                    query.Where(query.IsVoid == true);
                if (Request.QueryString["cons"] == "1")
                    query.Where(query.IsConsignment == true);

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.OrderBy(query.ToServiceUnitID.Ascending, query.TransactionDate.Descending,
                              query.TransactionNo.Descending);

                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        private DataTable ItemTransactionsIk
        {
            get
            {
                var isEmptyFilter = txtFromDate.IsEmpty && txtToDate.IsEmpty && string.IsNullOrEmpty(txtTransactionNo.Text) && string.IsNullOrEmpty(txtReferenceNo.Text) && string.IsNullOrEmpty(txtInvoice.Text) && txtInvoiceSupplierDate.IsEmpty && string.IsNullOrEmpty(cboPurchasingUnitID.SelectedValue) && string.IsNullOrEmpty(cboSearchSupplier.SelectedValue) && string.IsNullOrEmpty(cboSearchStatus.SelectedValue);

                if (Request.QueryString["cons"] == "0" && Request.QueryString["grants"] == "1")
                    if (!ValidateSearch(isEmptyFilter, "Grants Receive")) return null;
                if (Request.QueryString["cons"] == "0" && Request.QueryString["grants"] == "2")
                    if (!ValidateSearch(isEmptyFilter, "Direct Purchase")) return null;
                if (Request.QueryString["cons"] == "1")
                    if (!ValidateSearch(isEmptyFilter, "Purchase Order Receive (Consignment)")) return null;
                if (Request.QueryString["cons"] == "0")
                    if (!ValidateSearch(isEmptyFilter, "Purchase Order Receive")) return null;

                var query = new ItemTransactionQuery("a");
                var qryserviceunit = new ServiceUnitQuery("c");
                var sup = new SupplierQuery("b");
                var itemtype = new AppStandardReferenceItemQuery("d");
                var qusr = new AppUserServiceUnitQuery("u");

                query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.ToServiceUnitID);
                query.LeftJoin(sup).On(query.BusinessPartnerID == sup.SupplierID);
                query.LeftJoin(itemtype).On(itemtype.ItemID == query.SRItemType && itemtype.StandardReferenceID == "ItemType");
                query.InnerJoin(qusr).On(query.ToServiceUnitID == qusr.ServiceUnitID &&
                                qusr.UserID == AppSession.UserLogin.UserID);
                query.Where(query.TransactionCode == (IsGrantsReceiving ? TransactionCode.GrantsReceive : (IsDirectPurchase ? TransactionCode.DirectPurchase : TransactionCode.PurchaseOrderReceive)),
                            query.SRItemType == ItemType.Kitchen);

                query.Select(
                       query.TransactionNo,
                       query.TransactionDate,
                       query.ReferenceNo,
                       qryserviceunit.ServiceUnitName.As("TServiceUnitID"),
                       sup.SupplierName,
                       itemtype.ItemName,
                       query.IsApproved,
                       query.ReferenceNo,
                       query.Notes,
                       query.IsVoid,
                       query.InvoiceNo,
                       query.InvoiceSupplierDate,
                       query.DeliveryOrdersNo,
                       query.ChargesAmount
                   );

                query.Select(Request.QueryString["cons"] == "0"
                        ? "<'PurchaseOrderReceiveDetail.aspx?md=view&id=' + a.TransactionNo + '&type=2&cons=0' AS PorUrl>"
                        : "<'PurchaseOrderReceiveDetail.aspx?md=view&id=' + a.TransactionNo + '&type=2&cons=1' AS PorUrl>");

                if (!txtFromDate.IsEmpty && !txtToDate.IsEmpty)
                    query.Where(query.TransactionDate.Between(txtFromDate.SelectedDate, txtToDate.SelectedDate));
                if (!string.IsNullOrEmpty(txtTransactionNo.Text))
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                if (!string.IsNullOrEmpty(txtReferenceNo.Text))
                    query.Where(query.ReferenceNo == txtReferenceNo.Text);
                if (!string.IsNullOrEmpty(txtInvoice.Text))
                    query.Where(query.InvoiceNo == txtInvoice.Text);
                if (!txtInvoiceSupplierDate.IsEmpty)
                    query.Where(query.InvoiceSupplierDate == txtInvoiceSupplierDate.SelectedDate);
                if (!string.IsNullOrEmpty(cboSearchSupplier.SelectedValue))
                    query.Where(query.BusinessPartnerID == cboSearchSupplier.SelectedValue);
                if (!string.IsNullOrEmpty(cboPurchasingUnitID.SelectedValue))
                    query.Where(query.ToServiceUnitID == cboPurchasingUnitID.SelectedValue);
                if (cboSearchStatus.SelectedValue == "0")
                    query.Where(query.IsApproved == false);
                if (cboSearchStatus.SelectedValue == "1")
                    query.Where(query.IsApproved == true);
                if (cboSearchStatus.SelectedValue == "4")
                    query.Where(query.IsVoid == true);
                if (Request.QueryString["cons"] == "1")
                    query.Where(query.IsConsignment == true);

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.OrderBy(query.ToServiceUnitID.Ascending, query.TransactionDate.Descending,
                              query.TransactionNo.Descending);

                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdListIm.Rebind();
            grdListInm.Rebind();
            grdListIk.Rebind();
        }

        protected void cboSearchSupplier_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var supp = new SupplierQuery("a");
            supp.es.Top = 10;
            supp.Where(
                supp.SupplierName.Like(searchTextContain),
                supp.IsActive == true
                );

            cboSearchSupplier.DataSource = supp.LoadDataTable();
            cboSearchSupplier.DataBind();
        }

        protected void cboSearchSupplier_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SupplierName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SupplierID"].ToString();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;

            if (eventArgument == "new")
            {
                string url = string.Format(
                    "PurchaseOrderReceiveDetail.aspx?md={0}&type=2&cons={1}", eventArgument, Request.QueryString["cons"]);
                Page.Response.Redirect(url, true);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }

        protected override void InitializeControlFromCookie(Control ctl, object value)
        {
            if (ctl.ID.ToLower().Equals(cboSearchSupplier.ID.ToLower()) && value != null)
            {
                var query = new SupplierQuery();
                query.es.Top = 1;
                query.Select
                    (
                        query.SupplierID,
                        query.SupplierName
                    );
                query.Where(query.SupplierID == value);

                cboSearchSupplier.DataSource = query.LoadDataTable();
                cboSearchSupplier.DataBind();
            }
        }
    }
}
