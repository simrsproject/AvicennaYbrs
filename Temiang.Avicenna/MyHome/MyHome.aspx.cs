using System;
using System.Data;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.Module.Inventory;
using Temiang.Avicenna.Module.Inventory.Procurement;
using Temiang.Avicenna.Module.Inventory.Warehouse;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna
{
    public partial class MyHome : BasePage
    {
        private bool _isPoRebind = false;
        private bool _isDistOrderRebind = false;

        public string BaseURL()
        {
            return Helper.UrlRoot2();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.MyHome;

            tabDetail.Tabs[1].Visible = AppSession.Parameter.IsUseApprovalLevel;
            tabDetail.Tabs[2].Visible = AppSession.Parameter.IsDistributionUseApprovalLevel;
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            if (!IsPostBack)
            {
                if (Request.QueryString["tb"] == "appr")
                {
                    var activeTab = 0; // Tab Home

                    if (AppSession.Parameter.IsUseApprovalLevel)
                    {
                        var dtbPoQue = (DataTable)grdList.DataSource;
                        if (dtbPoQue == null || dtbPoQue.Rows.Count == 0)
                        {
                            if (AppSession.Parameter.IsDistributionUseApprovalLevel)
                            {
                                var dtbDistQue = (DataTable)grdDistributionOrderAppQue.DataSource;
                                if (dtbDistQue != null && dtbDistQue.Rows.Count > 0)
                                    activeTab = 2;// Tab Distribution
                            }
                        }
                        else activeTab = 1; // Tab PO
                    }
                    else
                    {
                        if (AppSession.Parameter.IsDistributionUseApprovalLevel)
                        {
                            var dtbDistQue = (DataTable)grdDistributionOrderAppQue.DataSource;
                            if (dtbDistQue != null && dtbDistQue.Rows.Count > 0)
                                activeTab = 2;// Tab Distribution
                        }
                    }

                    tabDetail.Tabs[activeTab].Selected = true;
                    mpgDetail.PageViews[activeTab].Selected = true;
                }
            }
        }

        public void Page_Load()
        {
            if (!IsPostBack)
            {
                tmrAutoRefreshList.Enabled = true;
            }
            AjaxManager.AjaxSettings.AddAjaxSetting(grdList, grdList, AjaxLoadingPanel);
            AjaxManager.AjaxSettings.AddAjaxSetting(grdList, pnlPurchaseOrderInfo, AjaxLoadingPanel);
            AjaxManager.AjaxSettings.AddAjaxSetting(grdDistributionOrderAppQue, grdDistributionOrderAppQue, AjaxLoadingPanel);
            AjaxManager.AjaxSettings.AddAjaxSetting(tmrAutoRefreshList, grdList, AjaxLoadingPanel);
            AjaxManager.AjaxSettings.AddAjaxSetting(tmrAutoRefreshList, grdDistributionOrderAppQue, AjaxLoadingPanel);

            Helper.RegisterStartupScript(this.Page, "loadHomepage", "LoadHomePage();");
        }

        public static DataTable ApprovalTransactionPending(string transactionCode, string UserID)
        {
            var atq = new ApprovalTransactionQuery("at");
            atq.Select(atq.TransactionNo, atq.ApprovalLevel);
            atq.Where(atq.TransactionCode == transactionCode && atq.UserID == UserID &&
                      atq.IsApproved == 0);

            var dtbAppTrans = atq.LoadDataTable();
            DataTable dtbSelected;

            if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsHigherApprovalLevelCanBypass))
            {
                // Ambil semua
                dtbSelected = dtbAppTrans.Copy();
            }
            else
            {
                dtbSelected = dtbAppTrans.Clone();
                // Ambil Approval Queuing
                foreach (DataRow row in dtbAppTrans.Rows)
                {
                    var at = new ApprovalTransaction();
                    atq = new ApprovalTransactionQuery("at");
                    atq.Where(atq.TransactionNo == row["TransactionNo"].ToString() &&
                              atq.ApprovalLevel < row["ApprovalLevel"].ToInt());
                    atq.es.Top = 1;
                    atq.OrderBy(atq.ApprovalLevel.Descending);


                    var isApprovalQueuing = false;
                    if (at.Load(atq))
                    {
                        if (at.IsApproved == true)
                        {
                            // Jika user level dibawahnya sudah approve 
                            isApprovalQueuing = true;
                        }
                    }
                    else
                    {
                        // Jika tidak ada user level dibawahnya
                        isApprovalQueuing = true;
                    }

                    if (isApprovalQueuing)
                    {
                        var newRow = dtbSelected.NewRow();
                        newRow["TransactionNo"] = row["TransactionNo"];
                        newRow["ApprovalLevel"] = row["ApprovalLevel"];
                        dtbSelected.Rows.Add(newRow);
                    }
                }
            }
            return dtbSelected;
        }


        #region Purchase Order
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            _isPoRebind = true;
            if (!e.IsFromDetailTable)
                grdList.DataSource = ApprovalPoQueu(AppSession.UserLogin.UserID);
            _isPoRebind = false;
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            ProcurementUtils.HideColumnStockInfo(e.DetailTableView);
            GridDataItem parentItem = e.DetailTableView.ParentItem;

            var transactionNo = parentItem.GetDataKeyValue("TransactionNo").ToString();
            var itemType = parentItem.GetDataKeyValue("SRItemType").ToString();
            var porLocationID = parentItem.GetDataKeyValue("PorLocationID").ToString();

            var dtb = Util.ApprovalLevelUtil.PurchaseOrderItem(transactionNo, itemType, porLocationID);

            //Apply
            e.DetailTableView.DataSource = dtb;
        }



        public static DataTable ApprovalPoQueu(string UserID)
        {

            var dtbApprovalQueu = new DataTable();
            dtbApprovalQueu.Columns.Add("TransactionNo", typeof(System.String));
            dtbApprovalQueu.Columns.Add("SRItemType", typeof(System.String));
            dtbApprovalQueu.Columns.Add("ApprovalLevel", typeof(System.Int16));
            dtbApprovalQueu.Columns.Add("TransactionDate", typeof(System.DateTime));
            dtbApprovalQueu.Columns.Add("SupplierName", typeof(System.String));
            dtbApprovalQueu.Columns.Add("ReferenceNo", typeof(System.String));
            dtbApprovalQueu.Columns.Add("ServiceUnitID", typeof(System.String));
            dtbApprovalQueu.Columns.Add("ServiceUnitName", typeof(System.String));
            dtbApprovalQueu.Columns.Add("ItemName", typeof(System.String));
            dtbApprovalQueu.Columns.Add("Notes", typeof(System.String));
            dtbApprovalQueu.Columns.Add("ChargesAmount", typeof(System.Decimal));
            dtbApprovalQueu.Columns.Add("PurchaseOrderType", typeof(System.String));
            dtbApprovalQueu.Columns.Add("PoUrl", typeof(System.String));
            dtbApprovalQueu.Columns.Add("PrUrl", typeof(System.String));
            dtbApprovalQueu.Columns.Add("PorLocationID", typeof(System.String));

            //urlPurchaseOrder
            var prog = new AppProgram();
            prog.LoadByPrimaryKey(AppConstant.Program.PurchaseOrder);
            var urlPurchaseOrder = prog.NavigateUrl;

            //urlRequestOrder
            prog = new AppProgram();
            prog.LoadByPrimaryKey(AppConstant.Program.RequestOrder);
            var urlRequestOrder = prog.NavigateUrl;

            var approvalTransactionPending = ApprovalTransactionPending(TransactionCode.PurchaseOrder, UserID);
            var transactionNo = string.Empty;
            DataRow rowTran = null;
            foreach (DataRow row in approvalTransactionPending.Rows)
            {
                if (transactionNo != row["TransactionNo"].ToString())
                {
                    transactionNo = row["TransactionNo"].ToString();

                    var query = new ItemTransactionQuery("a");
                    var sup = new SupplierQuery("b");
                    var qryserviceunit = new ServiceUnitQuery("c");
                    var itemtype = new AppStandardReferenceItemQuery("d");
                    var poType = new AppStandardReferenceItemQuery("s");

                    query.LeftJoin(poType).On(
                        query.SRPurchaseOrderType == poType.ItemID &&
                        poType.StandardReferenceID == AppEnum.StandardReference.PurchaseOrderType
                        );
                    query.LeftJoin(sup).On(query.BusinessPartnerID == sup.SupplierID);
                    query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
                    query.LeftJoin(itemtype).On(
                        itemtype.ItemID == query.SRItemType &&
                        itemtype.StandardReferenceID == AppEnum.StandardReference.ItemType
                        );

                    if (AppSession.Parameter.IsUseApprovalLevelforPOWithUserRestriction)
                    {
                        var usr = new AppUserServiceUnitQuery("usr");
                        query.InnerJoin(usr).On(usr.UserID == AppSession.UserLogin.UserID && usr.ServiceUnitID == query.FromServiceUnitID);
                    }

                    query.Select(
                        query.TransactionNo,
                        query.SRItemType,
                        query.TransactionDate,
                        sup.SupplierName,
                        query.ReferenceNo,
                        qryserviceunit.ServiceUnitName,
                        query.FromServiceUnitID,
                        itemtype.ItemName,
                        query.Notes,
                        query.IsVoid,
                        query.ChargesAmount,
                        poType.ItemName.As("PurchaseOrderType")
                        );

                    // Sub Query PorLocationID
                    var por = new ItemTransactionQuery("por");
                    por.Select(por.FromLocationID);
                    por.Where(por.TransactionNo == query.ReferenceNo);
                    query.Select(por.Select().As("PorLocationID"));

                    query.Where(query.TransactionNo == transactionNo, query.IsApproved == 0, query.IsVoid == 0);

                    var dtbTran = query.LoadDataTable();
                    if (dtbTran == null || dtbTran.Rows == null || dtbTran.Rows.Count == 0) continue;

                    rowTran = dtbTran.Rows[0];
                }


                var newRow = dtbApprovalQueu.NewRow();
                newRow["TransactionNo"] = rowTran["TransactionNo"];
                newRow["SRItemType"] = rowTran["SRItemType"];
                newRow["ApprovalLevel"] = row["ApprovalLevel"].ToInt();
                newRow["TransactionDate"] = rowTran["TransactionDate"];
                newRow["SupplierName"] = rowTran["SupplierName"];
                newRow["ReferenceNo"] = rowTran["ReferenceNo"];
                newRow["ServiceUnitID"] = rowTran["FromServiceUnitID"];
                newRow["ServiceUnitName"] = rowTran["ServiceUnitName"];
                newRow["ItemName"] = rowTran["ItemName"];
                newRow["Notes"] = rowTran["Notes"];
                newRow["ChargesAmount"] = rowTran["ChargesAmount"];
                newRow["PurchaseOrderType"] = rowTran["PurchaseOrderType"];
                newRow["PorLocationID"] = rowTran["PorLocationID"];

                if (urlPurchaseOrder.Contains("?"))
                {
                    var urls = urlPurchaseOrder.Split('?');
                    newRow["PoUrl"] = string.Format("{0}?md=view&id={1}&pr=&rop=0&{2}", urls[0], rowTran["TransactionNo"], urls[1]);
                }
                else
                {
                    newRow["PoUrl"] = string.Format("{0}?md=view&id={1}&pr=&rop=0", urlPurchaseOrder, rowTran["TransactionNo"]);
                }
                if (urlRequestOrder.Contains("?"))
                {
                    var urls = urlRequestOrder.Split('?');
                    newRow["PrUrl"] = string.Format("{0}?md=view&id={1}&{2}", urls[0], rowTran["ReferenceNo"], urls[1]);

                }
                else
                {
                    newRow["PrUrl"] = string.Format("{0}?md=view&id={1}", urlRequestOrder, rowTran["ReferenceNo"]);

                }
                dtbApprovalQueu.Rows.Add(newRow);
            }

            return dtbApprovalQueu;
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Approv")
            {
                var item = (GridDataItem)e.Item;
                var trNo = item.GetDataKeyValue("TransactionNo").ToString();
                var apprLevel = item.GetDataKeyValue("ApprovalLevel").ToInt();

                var msg = Approve(trNo, apprLevel, AppSession.UserLogin.UserID);
                if (string.IsNullOrEmpty(msg))
                {
                    Helper.ShowMessageAfterPostback(Page, "Approve Success.");
                }
                else {
                    pnlPurchaseOrderInfo.Visible = true;
                    lblPurchaseOrderInfo.Text = msg;
                    Helper.ShowMessageAfterPostback(Page, msg);
                }

                grdList.Rebind();
            }
        }

        public static string Approve(string TransactionNo, int ApprovalLevel, string UserID)
        {

            bool isValid = true;

            #region validation
            var msg = string.Empty;

            var it = new ItemTransaction();
            it.LoadByPrimaryKey(TransactionNo);

            //if (string.IsNullOrEmpty(it.SRDownPaymentType))
            //{
            //    msg = "Shipping Charges Type for Order#: " + TransactionNo + " required. Approve failed.";
            //    isValid = false;
            //}
            //else 
            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSMM" & string.IsNullOrEmpty(it.SRPaymentType))
            {
                msg = "Payment Type for Order#: " + TransactionNo + " required. Approve failed.";
                isValid = false;
            }
            else if (it.IsNonMasterOrder == true & string.IsNullOrEmpty(it.ProductAccountID))
            {
                msg = "Product Account for Order#: " + TransactionNo + " required. Approve failed.";
                isValid = false;
            }
            #endregion

            if (isValid)
            {
                var args = new ValidateArgs();
                using (var trans = new esTransactionScope())
                {
                    Util.ApprovalLevelUtil.Approve(args, TransactionCode.PurchaseOrder, TransactionNo, ApprovalLevel, UserID);
                    if (!args.IsCancel)
                    {
                        trans.Complete();
                    }
                    else
                        msg = string.Format("{0}. Approve failed.", args.MessageText);
                }
            }
            return msg;
        }


        #endregion

        #region Distribution
        protected void grdDistributionOrderAppQue_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            _isDistOrderRebind = true;
            if (!e.IsFromDetailTable)
                grdDistributionOrderAppQue.DataSource = ApprovalDistributionQueu();
            _isDistOrderRebind = false;
        }

        protected void grdDistributionOrderAppQue_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem parentItem = e.DetailTableView.ParentItem;
            var transactionNo = parentItem.GetDataKeyValue("TransactionNo").ToString();
            var itemType = parentItem.GetDataKeyValue("SRItemType").ToString();
            var fromLocationID = parentItem.GetDataKeyValue("FromLocationID").ToString();
            var toLocationID = parentItem.GetDataKeyValue("ToLocationID").ToString();

            var dtb = DistributionOrderItem(transactionNo, itemType, fromLocationID, toLocationID);

            //Apply
            e.DetailTableView.DataSource = dtb;
        }
        protected void grdDistributionOrderAppQue_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Approv")
            {
                var item = (GridDataItem)e.Item;
                var trNo = item.GetDataKeyValue("TransactionNo").ToString();
                var apprLevel = item.GetDataKeyValue("ApprovalLevel").ToInt();

                var args = new ValidateArgs();
                using (var trans = new esTransactionScope())
                {
                    Util.ApprovalLevelUtil.Approve(args, TransactionCode.Distribution, trNo, apprLevel, AppSession.UserLogin.UserID);
                    if (!args.IsCancel)
                    {
                        trans.Complete();
                        Helper.ShowMessageAfterPostback(Page, "Approve success.");
                    }
                    else
                        Helper.ShowMessageAfterPostback(Page, string.Format("{0}. Approve failed.", args.MessageText));
                }

                grdDistributionOrderAppQue.Rebind();
            }

        }
        private static DataTable DistributionOrderItem(string transactionNo, string itemType, string fromLocationID, string toLocationID)
        {
            //Load record
            var query = new ItemTransactionItemQuery("a");
            var iq = new ItemQuery("b");
            var itiref = new ItemTransactionItemQuery("itr");
            query.Select(
                query.TransactionNo,
                query.ItemID,
                iq.ItemName,
                query.SRItemUnit,
                query.Quantity,
                query.QuantityFinishInBaseUnit,
                query.SequenceNo,
                query.Price,
                query.Discount1Percentage,
                query.Discount2Percentage,
                query.Discount,
                query.Discount,
                query.IsBonusItem,
                query.IsClosed,
                query.Description,
                query.ConversionFactor,
                @"<(ISNULL(itr.Quantity, 0) * ISNULL(itr.ConversionFactor, 0))/a.ConversionFactor AS QtyRequest>"
            );
            query.LeftJoin(iq).On(query.ItemID == iq.ItemID);
            query.LeftJoin(itiref).On(itiref.TransactionNo == query.ReferenceNo &&
                                      itiref.SequenceNo == query.ReferenceSequenceNo);

            // Base Unit
            var ipnmq = new ItemProductNonMedicQuery("i2");
            var ikq = new ItemKitchenQuery("i2");
            var ipmq = new ItemProductMedicQuery("i2");

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
            query.Select(@"<i2.SRItemUnit AS SRMasterBaseUnit>");

            // Min qty, Balance
            var reqLoc = new ItemBalanceQuery("reqLoc");
            query.LeftJoin(reqLoc).On(query.ItemID == reqLoc.ItemID && reqLoc.LocationID == toLocationID);

            query.Select(
                @"<CONVERT(decimal(10,2),COALESCE(reqLoc.Balance,0)) AS BalAtReqLoc>",
                @"<CONVERT(decimal(10,2),COALESCE(reqLoc.Minimum,0)) AS MinAtReqLoc>",
                @"<CONVERT(decimal(10,2),COALESCE(reqLoc.Maximum,0)) AS MaxAtReqLoc>"
                );

            var sourceLoc = new ItemBalanceQuery("sourceLoc");
            query.LeftJoin(sourceLoc).On(query.ItemID == sourceLoc.ItemID && sourceLoc.LocationID == fromLocationID);

            query.Select(
                @"<CONVERT(decimal(10,2),COALESCE(sourceLoc.Balance,0)) AS BalAtSourceLoc>",
                @"<CONVERT(decimal(10,2),COALESCE(sourceLoc.Minimum,0)) AS MinAtSourceLoc>",
                @"<CONVERT(decimal(10,2),COALESCE(sourceLoc.Maximum,0)) AS MaxAtSourceLoc>"

                );

            query.Where(query.TransactionNo == transactionNo);
            query.OrderBy(query.ItemID.Ascending);

            DataTable dtb = query.LoadDataTable();

            return dtb;
        }
        private DataTable ApprovalDistributionQueu()
        {

            var dtbApprovalQueu = new DataTable();
            dtbApprovalQueu.Columns.Add("TransactionNo", typeof(System.String));
            dtbApprovalQueu.Columns.Add("ReferenceNo", typeof(System.String));
            dtbApprovalQueu.Columns.Add("SRItemType", typeof(System.String));
            dtbApprovalQueu.Columns.Add("ItemTypeName", typeof(System.String));
            dtbApprovalQueu.Columns.Add("ItemGroupName", typeof(System.String));

            dtbApprovalQueu.Columns.Add("ItemGroupID", typeof(System.String));
            dtbApprovalQueu.Columns.Add("ApprovalLevel", typeof(System.Int16));
            dtbApprovalQueu.Columns.Add("TransactionDate", typeof(System.DateTime));
            dtbApprovalQueu.Columns.Add("FromServiceUnitName", typeof(System.String));
            dtbApprovalQueu.Columns.Add("ToServiceUnitName", typeof(System.String));
            dtbApprovalQueu.Columns.Add("FromLocationID", typeof(System.String));
            dtbApprovalQueu.Columns.Add("ToLocationID", typeof(System.String));

            dtbApprovalQueu.Columns.Add("Notes", typeof(System.String));
            dtbApprovalQueu.Columns.Add("DistUrl", typeof(System.String));
            dtbApprovalQueu.Columns.Add("DistReqUrl", typeof(System.String));

            //urlDistribution
            var prog = new AppProgram();
            prog.LoadByPrimaryKey(AppConstant.Program.Distribution);
            var urlDistribution = prog.NavigateUrl;

            //urlDistributionRequest
            prog = new AppProgram();
            prog.LoadByPrimaryKey(AppConstant.Program.DistributionRequest);
            var urlDistributionRequest = prog.NavigateUrl;

            var approvalTransactionPending = ApprovalTransactionPending(TransactionCode.Distribution, AppSession.UserLogin.UserID);
            var transactionNo = string.Empty;
            DataRow rowTran = null;
            foreach (DataRow row in approvalTransactionPending.Rows)
            {
                if (transactionNo != row["TransactionNo"].ToString())
                {
                    transactionNo = row["TransactionNo"].ToString();
                    var query = new ItemTransactionQuery("a");
                    var qryserviceunit = new ServiceUnitQuery("c");
                    var qryserviceunitto = new ServiceUnitQuery("e");
                    var itemtype = new AppStandardReferenceItemQuery("d");
                    var itemGroup = new ItemGroupQuery("ig");

                    query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
                    query.InnerJoin(qryserviceunitto).On(qryserviceunitto.ServiceUnitID == query.ToServiceUnitID);
                    query.LeftJoin(itemtype)
                        .On(itemtype.ItemID == query.SRItemType && itemtype.StandardReferenceID == "ItemType");
                    query.LeftJoin(itemGroup).On(query.ItemGroupID == itemGroup.ItemGroupID);

                    query.Where(query.TransactionNo == transactionNo, query.IsApproved == 0, query.IsVoid == 0);

                    query.Select(
                        query.TransactionNo,
                        query.SRItemType,
                        query.TransactionDate,
                        query.ReferenceNo,
                        query.FromLocationID,
                        query.ToLocationID,
                        qryserviceunit.ServiceUnitName.As("FromServiceUnitName"),
                        qryserviceunitto.ServiceUnitName.As("ToServiceUnitName"),
                        itemtype.ItemName.As("ItemTypeName"),
                        itemGroup.ItemGroupName,
                        query.Notes
                        );


                    var dtbTran = query.LoadDataTable();
                    rowTran = dtbTran.Rows[0];
                }

                var newRow = dtbApprovalQueu.NewRow();
                newRow["TransactionNo"] = rowTran["TransactionNo"];
                newRow["SRItemType"] = rowTran["SRItemType"];
                newRow["ItemTypeName"] = rowTran["ItemTypeName"];
                newRow["ItemGroupName"] = rowTran["ItemGroupName"];

                newRow["ApprovalLevel"] = row["ApprovalLevel"].ToInt();
                newRow["TransactionDate"] = rowTran["TransactionDate"];
                newRow["ReferenceNo"] = rowTran["ReferenceNo"];
                newRow["FromLocationID"] = rowTran["FromLocationID"];
                newRow["ToLocationID"] = rowTran["ToLocationID"];
                newRow["FromServiceUnitName"] = rowTran["FromServiceUnitName"];
                newRow["ToServiceUnitName"] = rowTran["ToServiceUnitName"];
                newRow["Notes"] = rowTran["Notes"];


                if (urlDistribution.Contains("?"))
                {
                    var urls = urlDistribution.Split('?');
                    newRow["DistUrl"] = string.Format("{0}?md=view&id={1}&pr=&rop=0&{2}", urls[0], rowTran["TransactionNo"], urls[1]);
                }
                else
                {
                    newRow["DistUrl"] = string.Format("{0}?md=view&id={1}&pr=&rop=0", urlDistribution, rowTran["TransactionNo"]);
                }
                if (urlDistributionRequest.Contains("?"))
                {
                    var urls = urlDistributionRequest.Split('?');
                    newRow["DistReqUrl"] = string.Format("{0}?md=view&id={1}&{2}", urls[0], rowTran["ReferenceNo"], urls[1]);

                }
                else
                {
                    newRow["DistReqUrl"] = string.Format("{0}?md=view&id={1}", urlDistributionRequest, rowTran["ReferenceNo"]);

                }
                dtbApprovalQueu.Rows.Add(newRow);
            }

            return dtbApprovalQueu;
        }


        #endregion


        protected void tmrAutoRefreshList_Tick(object sender, EventArgs e)
        {
            if (_isPoRebind || _isDistOrderRebind) return;

            tmrAutoRefreshList.Enabled = false;

            grdList.Rebind();
            grdDistributionOrderAppQue.Rebind();

            tmrAutoRefreshList.Enabled = true;
        }
    }
}