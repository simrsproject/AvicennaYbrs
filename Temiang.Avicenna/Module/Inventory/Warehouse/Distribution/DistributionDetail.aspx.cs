using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;


namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class DistributionDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private void PopulateNewTransactionNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New) return;
            if (cboFromServiceUnitID.SelectedValue == string.Empty) return;
            var serv = new ServiceUnit();
            if (serv.LoadByPrimaryKey(cboFromServiceUnitID.SelectedValue))
            {
                _autoNumber = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value.Date,
                    BusinessObject.Reference.TransactionCode.Distribution, serv.DepartmentID);
                txtTransactionNo.Text = _autoNumber.LastCompleteNumber;
            }
        }

        private void PopulateGridDetailFromReferenceNo()
        {
            var query = new ItemTransactionItemQuery("a");
            var hq = new ItemTransactionQuery("b");
            var iq = new ItemQuery("c");
            var ibq = new ItemBalanceQuery("e");
            var ibq2 = new ItemBalanceQuery("f");

            query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
            query.InnerJoin(hq).On(query.TransactionNo == hq.TransactionNo);
            query.LeftJoin(ibq).On(query.ItemID == ibq.ItemID && ibq.LocationID == hq.FromLocationID);
            query.LeftJoin(ibq2).On(query.ItemID == ibq2.ItemID && ibq2.LocationID == hq.ToLocationID);

            query.Where(query.TransactionNo == txtReferenceNo.Text, query.IsClosed == false);
            query.OrderBy
                (
                    query.ItemID.Ascending
                );

            query.Select
                (
                    query.TransactionNo,
                    query.SequenceNo,
                    hq.ToServiceUnitID,
                    hq.ToLocationID,
                    hq.FromServiceUnitID,
                    hq.FromLocationID,
                    query.ItemID,
                    query.SRItemUnit,
                    query.Quantity,
                    query.QuantityFinishInBaseUnit,
                    (((query.Quantity * query.ConversionFactor) - query.QuantityFinishInBaseUnit) / query.ConversionFactor)
                        .As("QtyInput"),
                    iq.ItemName,
                    hq.SRItemType,
                    hq.ItemGroupID,
                    query.CostPrice,
                    query.ConversionFactor,
                    @"<ISNULL(e.Balance / a.ConversionFactor, 0) AS 'Balance'>",
                    @"<ISNULL(e.Booking / a.ConversionFactor, 0) AS 'Booking'>",
                    @"<ISNULL(e.Minimum / a.ConversionFactor, 0) AS 'Minimum'>",
                    @"<ISNULL(e.Maximum / a.ConversionFactor, 0) AS 'Maximum'>",
                    @"<ISNULL(f.Balance / a.ConversionFactor, 0) AS 'Balance2'>",
                    hq.ServiceUnitCostID
                );
            var dtb = query.LoadDataTable();

            Session["DistributionItemSelected" + Request.UserHostName] = dtb;

            PopulateFromSelectedRequestDistribution();
        }

        private void PopulateFromSelectedRequestDistribution()
        {
            object obj = Session["DistributionItemSelected" + Request.UserHostName];
            if (obj == null) return;

            //delete previouse item
            if (ItemTransactionItems.Count > 0)
                ItemTransactionItems.MarkAllAsDeleted();

            DataTable dtbSelectedItem = (DataTable)obj;
            if (dtbSelectedItem.Rows.Count > 0)
            {
                txtReferenceNo.Text = dtbSelectedItem.Rows[0]["TransactionNo"].ToString();
                cboFromServiceUnitID.SelectedValue = dtbSelectedItem.Rows[0]["ToServiceUnitID"].ToString();
                cboFromServiceUnitID.Enabled = false;
                ComboBox.PopulateWithServiceUnitForLocation(cboFromLocationID, cboFromServiceUnitID.SelectedValue);
                cboFromLocationID.SelectedValue = dtbSelectedItem.Rows[0]["ToLocationID"].ToString();
                cboFromLocationID.Enabled = false;
                cboToServiceUnitID.SelectedValue = dtbSelectedItem.Rows[0]["FromServiceUnitID"].ToString();
                cboToServiceUnitID.Enabled = false;
                ComboBox.PopulateWithServiceUnitForLocation(cboToLocationID, cboToServiceUnitID.SelectedValue);
                cboToLocationID.SelectedValue = dtbSelectedItem.Rows[0]["FromLocationID"].ToString();
                cboToLocationID.Enabled = false;
                ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboFromServiceUnitID.SelectedValue,
                    BusinessObject.Reference.TransactionCode.Distribution);
                cboSRItemType.SelectedValue = dtbSelectedItem.Rows[0]["SRItemType"].ToString();
                ComboBox.PopulateWithOneItemGroup(cboItemGroupID, dtbSelectedItem.Rows[0]["ItemGroupID"].ToString());
            }

            string seqNo;

            int i = 0;
            foreach (DataRow row in dtbSelectedItem.Rows)
            {
                if (Convert.ToDecimal(row["QtyInput"]) <= 0) continue;
                i++;
                seqNo = string.Format("{0:000}", i);
                var entity = ItemTransactionItems.AddNew();

                entity.ItemID = row["ItemID"].ToString();
                entity.SequenceNo = seqNo;
                entity.ReferenceNo = row["TransactionNo"].ToString();
                entity.ReferenceSequenceNo = row["SequenceNo"].ToString();
                entity.ItemName = row["ItemName"].ToString();
                entity.Quantity = Convert.ToDecimal(row["QtyInput"]);
                entity.SRItemUnit = row["SRItemUnit"].ToString();
                entity.ConversionFactor = Convert.ToDecimal(row["ConversionFactor"]);

                var it = new Item();
                it.LoadByPrimaryKey(entity.ItemID);
                if (it.SRItemType == BusinessObject.Reference.ItemType.Medical)
                {
                    var ipm = new ItemProductMedic();
                    if (ipm.LoadByPrimaryKey(entity.ItemID))
                    {
                        entity.CostPrice = ipm.CostPrice;
                        entity.Price = ipm.PriceInBaseUnit * entity.ConversionFactor;
                        entity.PriceInCurrency = ipm.PriceInBaseUnit * entity.ConversionFactor;
                        entity.IsControlExpired = ipm.IsControlExpired ?? false;
                    }
                    else
                    {
                        entity.CostPrice = 0;
                        entity.Price = 0;
                        entity.PriceInCurrency = 0;
                        entity.IsControlExpired = false;
                    }
                }
                else if (it.SRItemType == BusinessObject.Reference.ItemType.NonMedical)
                {
                    var ipm = new ItemProductNonMedic();
                    if (ipm.LoadByPrimaryKey(entity.ItemID))
                    {
                        entity.CostPrice = ipm.CostPrice;
                        entity.Price = ipm.PriceInBaseUnit * entity.ConversionFactor;
                        entity.PriceInCurrency = ipm.PriceInBaseUnit * entity.ConversionFactor;
                        entity.IsControlExpired = ipm.IsControlExpired ?? false;
                    }
                    else
                    {
                        entity.CostPrice = 0;
                        entity.Price = 0;
                        entity.PriceInCurrency = 0;
                        entity.IsControlExpired = false;
                    }
                }
                else
                {
                    var ipm = new ItemKitchen();
                    if (ipm.LoadByPrimaryKey(entity.ItemID))
                    {
                        entity.CostPrice = ipm.CostPrice;
                        entity.Price = ipm.PriceInBaseUnit * entity.ConversionFactor;
                        entity.PriceInCurrency = ipm.PriceInBaseUnit * entity.ConversionFactor;
                        entity.IsControlExpired = ipm.IsControlExpired ?? false;
                    }
                    else
                    {
                        entity.CostPrice = 0;
                        entity.Price = 0;
                        entity.PriceInCurrency = 0;
                        entity.IsControlExpired = false;
                    }
                }
                cboSRItemType.SelectedValue = it.SRItemType;
                entity.QuantityFinishInBaseUnit = Convert.ToDecimal(row["QuantityFinishInBaseUnit"]);
                entity.Balance = Convert.ToDecimal(row["Balance"]);
                entity.Booking = Convert.ToDecimal(row["Booking"]);
                entity.Minimum = Convert.ToDecimal(row["Minimum"]);
                entity.Maximum = Convert.ToDecimal(row["Maximum"]);
                entity.Balance2 = Convert.ToDecimal(row["Balance2"]);
            }

            grdItemTransactionItem.DataSource = ItemTransactionItems;
            grdItemTransactionItem.DataBind();

            cboSRItemType.Enabled = !(ItemTransactionItems.Count > 0);
            cboItemGroupID.Enabled = cboSRItemType.Enabled;
            cboFromServiceUnitID.Enabled = cboSRItemType.Enabled;
            cboFromLocationID.Enabled = cboSRItemType.Enabled;
            cboToServiceUnitID.Enabled = cboSRItemType.Enabled;
            cboToLocationID.Enabled = cboSRItemType.Enabled;

            //Remove session
            Session.Remove("DistributionItemSelected" + Request.UserHostName);
        }

        private void PopulateFromPickList()
        {
            Session.Remove("DistributionPicklist:Selection");

            object obj = Session["DistributionPicklist:ItemSelected"];
            if (obj == null) return;

            //delete previouse item
            if (ItemTransactionItems.Count > 0)
                ItemTransactionItems.MarkAllAsDeleted();

            DataTable dtbSelectedItem = (DataTable)obj;
            if (dtbSelectedItem.Rows.Count == 0) return;

            txtReferenceNo.Text = string.Empty;
            cboFromServiceUnitID.SelectedValue = dtbSelectedItem.Rows[0]["FromServiceUnitID"].ToString();
            cboFromServiceUnitID.Enabled = false;
            ComboBox.PopulateWithServiceUnitForLocation(cboFromLocationID, cboFromServiceUnitID.SelectedValue);
            cboFromLocationID.SelectedValue = dtbSelectedItem.Rows[0]["FromLocationID"].ToString();
            cboFromLocationID.Enabled = false;
            cboToServiceUnitID.SelectedValue = dtbSelectedItem.Rows[0]["ToServiceUnitID"].ToString();
            cboToServiceUnitID.Enabled = false;
            ComboBox.PopulateWithServiceUnitForLocation(cboToLocationID, cboToServiceUnitID.SelectedValue);
            cboToLocationID.SelectedValue = dtbSelectedItem.Rows[0]["ToLocationID"].ToString();
            cboToLocationID.Enabled = false;
            ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboFromServiceUnitID.SelectedValue,
                BusinessObject.Reference.TransactionCode.Distribution);
            cboSRItemType.SelectedValue = dtbSelectedItem.Rows[0]["SRItemType"].ToString();
            ComboBox.PopulateWithOneItemGroup(cboItemGroupID, dtbSelectedItem.Rows[0]["ItemGroupID"].ToString());

            int i = 0;
            foreach (DataRow row in dtbSelectedItem.Rows)
            {
                if (Convert.ToDecimal(row["QtyInput"]) < 1) continue;
                i++;

                ItemTransactionItem entity = ItemTransactionItems.AddNew();
                entity.ItemID = row["ItemID"].ToString();
                entity.SequenceNo = string.Format("{0:000}", i);
                entity.ReferenceNo = string.Empty;
                entity.ReferenceSequenceNo = string.Empty;
                entity.ItemName = row["ItemName"].ToString();
                entity.Quantity = Convert.ToDecimal(row["QtyInput"]);
                entity.SRItemUnit = row["SRItemUnit"].ToString();
                entity.ConversionFactor = 1;

                var it = new Item();
                it.LoadByPrimaryKey(entity.ItemID);
                if (it.SRItemType == BusinessObject.Reference.ItemType.Medical)
                {
                    var ipm = new ItemProductMedic();
                    if (ipm.LoadByPrimaryKey(entity.ItemID))
                    {
                        entity.CostPrice = ipm.CostPrice;
                        entity.Price = ipm.PriceInBaseUnit;
                        entity.PriceInCurrency = ipm.PriceInBaseUnit;
                        entity.IsControlExpired = ipm.IsControlExpired ?? false;
                    }
                    else
                    {
                        entity.CostPrice = 0;
                        entity.Price = 0;
                        entity.PriceInCurrency = 0;
                        entity.IsControlExpired = false;
                    }
                }
                else if (it.SRItemType == BusinessObject.Reference.ItemType.NonMedical)
                {
                    var ipm = new ItemProductNonMedic();
                    if (ipm.LoadByPrimaryKey(entity.ItemID))
                    {
                        entity.CostPrice = ipm.CostPrice;
                        entity.Price = ipm.PriceInBaseUnit;
                        entity.PriceInCurrency = ipm.PriceInBaseUnit;
                        entity.IsControlExpired = ipm.IsControlExpired ?? false;
                    }
                    else
                    {
                        entity.CostPrice = 0;
                        entity.Price = 0;
                        entity.PriceInCurrency = 0;
                        entity.IsControlExpired = false;
                    }
                }
                else
                {
                    var ipm = new ItemKitchen();
                    if (ipm.LoadByPrimaryKey(entity.ItemID))
                    {
                        entity.CostPrice = ipm.CostPrice;
                        entity.Price = ipm.PriceInBaseUnit;
                        entity.PriceInCurrency = ipm.PriceInBaseUnit;
                        entity.IsControlExpired = ipm.IsControlExpired ?? false;
                    }
                    else
                    {
                        entity.CostPrice = 0;
                        entity.Price = 0;
                        entity.PriceInCurrency = 0;
                        entity.IsControlExpired = false;
                    }
                }
                entity.QuantityFinishInBaseUnit = 0;
                entity.Balance = Convert.ToDecimal(row["Balance"]); // balance to
                entity.Booking = 0;
                entity.Minimum = Convert.ToDecimal(row["Minimum"]);
                entity.Maximum = Convert.ToDecimal(row["Maximum"]);
                entity.Balance2 = Convert.ToDecimal(row["BalanceFrom"]); //balance from
                
                //var bal = new ItemBalance();
                //if (bal.LoadByPrimaryKey(cboFromLocationID.SelectedValue, entity.ItemID))
                //    entity.Balance = bal.Balance;
                //else entity.Balance = 0;

                //bal = new ItemBalance();
                //if (bal.LoadByPrimaryKey(cboToLocationID.SelectedValue, entity.ItemID))
                //    entity.Balance2 = bal.Balance;
                //else entity.Balance2 = 0;
            }
            grdItemTransactionItem.DataSource = ItemTransactionItems;
            grdItemTransactionItem.DataBind();

            //Remove session
            Session.Remove("DistributionPicklist:ItemSelected");
            cboSRItemType.Enabled = ItemTransactionItems.Count == 0;
            cboItemGroupID.Enabled = cboSRItemType.Enabled;
            cboFromServiceUnitID.Enabled = cboSRItemType.Enabled;
            cboFromLocationID.Enabled = cboSRItemType.Enabled;
            cboToServiceUnitID.Enabled = cboSRItemType.Enabled;
            cboToLocationID.Enabled = cboSRItemType.Enabled;
        }

        protected void btnResetItem_Click(object sender, EventArgs e)
        {
            if (txtReferenceNo.Text != string.Empty)
            {
                txtReferenceNo.Text = string.Empty;
                cboSRItemType.Enabled = true;
                cboItemGroupID.Enabled = cboSRItemType.Enabled;
                cboFromServiceUnitID.Enabled = true;
                cboFromLocationID.Enabled = true;
                cboToServiceUnitID.Enabled = true;
                cboToLocationID.Enabled = true;
                if (ItemTransactionItems.Count > 0)
                    ItemTransactionItems.MarkAllAsDeleted();
                grdItemTransactionItem.DataSource = ItemTransactionItems;
                grdItemTransactionItem.DataBind();
            }
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "";

            if (string.IsNullOrEmpty(Request.QueryString["rod"]))
                UrlPageList = "DistributionList.aspx";
            else if (Request.QueryString["rod"] == "1")
                UrlPageList = "../ReOrderDistribution/ReOrderDistributionList.aspx?su=" + Request.QueryString["su"] +
                              "&it=" + Request.QueryString["it"];
            else
                UrlPageList = "../ItemRequestMaintenance/ItemRequestMaintenanceList.aspx?su=" +
                              Request.QueryString["su"] +
                              "&it=" + Request.QueryString["it"];

            ProgramID = AppConstant.Program.Distribution;

            this.WindowSearch.Height = 400;

            boxApprovalProgress.Visible = AppSession.Parameter.IsDistributionUseApprovalLevel;
            trProsesPr.Visible = AppSession.Parameter.IsVisibleBtnPurcReqOnDistribution;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                //  Reset Session
                ItemTransactionItems = null;

                grdItemTransactionItem.Columns.FindByUniqueName("Booking").Visible = (!AppSession.Parameter.IsDistributionAutoConfirm);
                if (!AppSession.Parameter.IsTxUsingEdDetail)
                    grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = false; //ico ed

                StandardReference.InitializeIncludeSpace(cboSRDistributionType, AppEnum.StandardReference.DistributionType);
            }

            //Add Event for Request Order Selection
            AjaxManager.AjaxRequest += new RadAjaxControl.AjaxRequestDelegate(AjaxManager_AjaxRequest);
        }

        private void AjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            PopulateFromSelectedRequestDistribution();
            PopulateFromPickList();
        }

        //protected override void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);
        //    PopulateApprovalGrid();
        //}

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;

            if (!string.IsNullOrEmpty(Request.QueryString["drn"]))
            {
                ToolBarMenuAdd.Enabled = false;
            }


            // ToolBarMenuApproval
            if (AppSession.Parameter.IsDistributionUseApprovalLevel)
            {
                ViewState["apprLevel"] = 0;
                ViewState["apprCount"] = 0;
                var dtbApproval = (DataTable)Session["ds_grdApproval"];
                ViewState["apprCount"] = dtbApproval.Rows.Count;
                if (ViewState["apprCount"].ToInt() > 0)
                {
                    var disableToolbarApproval = true;
                    foreach (DataRow row in dtbApproval.Rows)
                    {
                        if (true.Equals(row["IsApproveAble"]))
                        {
                            ViewState["apprLevel"] = row["ApprovalLevel"];
                            disableToolbarApproval = false;
                            break;
                        }
                    }
                    if (disableToolbarApproval)
                    {
                        ToolBarMenuApproval.Enabled = false;
                        ToolBarMenuUnApproval.Enabled = false;
                    }
                }
            }

        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItemTransactionItem, grdItemTransactionItem);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboFromServiceUnitID);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboFromLocationID);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboSRItemType);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboItemGroupID);

            ajax.AddAjaxSetting(cboFromServiceUnitID, cboFromServiceUnitID);
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboSRItemType);
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboFromLocationID);
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboItemGroupID);

            ajax.AddAjaxSetting(cboSRItemType, cboItemGroupID);
            ajax.AddAjaxSetting(cboSRItemType, cboToServiceUnitID);
            ajax.AddAjaxSetting(cboSRItemType, cboToLocationID);

            ajax.AddAjaxSetting(cboToServiceUnitID, cboToServiceUnitID);
            ajax.AddAjaxSetting(cboToServiceUnitID, cboToLocationID);

            //Distribution Request Selection
            ajax.AddAjaxSetting(AjaxManager, txtReferenceNo);
            ajax.AddAjaxSetting(AjaxManager, grdItemTransactionItem);
            ajax.AddAjaxSetting(AjaxManager, cboSRItemType);
            ajax.AddAjaxSetting(AjaxManager, cboToServiceUnitID);
            ajax.AddAjaxSetting(AjaxManager, cboToLocationID);
            ajax.AddAjaxSetting(AjaxManager, cboFromServiceUnitID);
            ajax.AddAjaxSetting(AjaxManager, cboFromLocationID);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuEditClick()
        {
            string toServiceUnitID = cboToServiceUnitID.SelectedValue;
            ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID,
                BusinessObject.Reference.TransactionCode.DistributionRequest, false, string.Empty, cboSRItemType.SelectedValue);
            cboToServiceUnitID.SelectedValue = toServiceUnitID;

            string serviceUnitID = cboFromServiceUnitID.SelectedValue;
            ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID,
                BusinessObject.Reference.TransactionCode.Distribution, true);
            cboFromServiceUnitID.SelectedValue = serviceUnitID;

            cboSRItemType.Enabled = ItemTransactionItems.Count == 0;
            cboItemGroupID.Enabled = cboSRItemType.Enabled;
            cboFromServiceUnitID.Enabled = ItemTransactionItems.Count == 0;
            cboFromLocationID.Enabled = ItemTransactionItems.Count == 0;
            if (!string.IsNullOrEmpty(txtReferenceNo.Text))
            {
                cboToServiceUnitID.Enabled = ItemTransactionItems.Count == 0;
                cboToLocationID.Enabled = ItemTransactionItems.Count == 0;
            }
            if (AppSession.Parameter.IsTxUsingEdDetail)
                grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = false; // ed ico
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new ItemTransaction();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                if (!IsApprovedOrVoid(entity, args))
                    return;
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID,
            PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            if (ItemTransactionItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }
            else
            {
                if (!AppSession.Parameter.IsDistributionAutoConfirm && AppSession.Parameter.IsUsingValidationPendingBalance)
                {
                    if (ItemBalance.IsHasPendingBalance(cboToLocationID.SelectedValue))
                    {
                        args.IsCancel = true;
                        args.MessageText = "Destination location has pending balance. Distribution cannot be continued.";
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(txtReferenceNo.Text))
                {
                    var refs = (ItemTransactionItems.OrderBy(b => b.ReferenceNo));

                    var msg = string.Empty;
                    foreach (var item in refs)
                    {
                        var iRef = new ItemTransactionItem();
                        if (iRef.LoadByPrimaryKey(item.ReferenceNo, item.ReferenceSequenceNo))
                        {
                            if (iRef.IsClosed == true || iRef.QuantityFinishInBaseUnit >= iRef.Quantity * iRef.ConversionFactor)
                            {
                                if (msg == string.Empty)
                                    msg = item.ItemID;
                                else
                                    msg += ", " + item.ItemID;
                            }
                        }
                    }
                    if (msg != string.Empty)
                    {
                        var refNos = string.Empty;
                        var refq = new ItemTransactionQuery();
                        refq.Select(refq.TransactionNo);
                        refq.Where(refq.TransactionCode == TransactionCode.Distribution,
                                   refq.ReferenceNo == txtReferenceNo.Text,
                                   refq.TransactionNo != txtTransactionNo.Text, refq.IsApproved == true);
                        DataTable refdtb = refq.LoadDataTable();
                        if (refdtb.Rows.Count > 0)
                        {
                            foreach (DataRow row in refdtb.Rows)
                            {
                                if (refNos == string.Empty)
                                    refNos = row["TransactionNo"].ToString();
                                else
                                    refNos += ", "+ row["TransactionNo"].ToString();
                            }
                        }

                        args.MessageText = "Data can't be approved. Item: " + msg + " has been closed. Check back your distribution transaction for this Dist. Request Number (" + refNos + ").";
                        args.IsCancel = true;
                        return;
                    }
                }
            }

            var it = new ItemTransaction();
            if (it.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                if (it.IsApproved ?? false)
                {
                    args.MessageText = "This Distributioan already approved";
                    args.IsCancel = true;
                    return;
                }

                if (string.IsNullOrEmpty(it.ToLocationID))
                {
                    args.MessageText = "To Location required.";
                    args.IsCancel = true;
                    return;
                }
            }

            //if (!string.IsNullOrEmpty(txtReferenceNo.Text))
            //{
            //    var refq = new ItemTransactionQuery();
            //    refq.Select(refq.TransactionNo);
            //    refq.Where(refq.TransactionCode == TransactionCode.Distribution,
            //               refq.ReferenceNo == txtReferenceNo.Text,
            //               refq.TransactionNo != txtTransactionNo.Text, refq.IsApproved == true);
            //    refq.es.Top = 1;
            //    DataTable refdtb = refq.LoadDataTable();
            //    if (refdtb.Rows.Count > 0)
            //    {
            //        args.MessageText = "This Dist. Request # has already been processed with transaction number: " + refdtb.Rows[0]["TransactionNo"] + ".";
            //        args.IsCancel = true;
            //        return;
            //    }
            //}

            if (AppSession.Parameter.IsValidateEdOnDistribution && !AppSession.Parameter.IsEnabledStockWithEdControl)
            {
                var c = ItemTransactionItems;
                var msg = string.Empty;
                foreach (var item in c)
                {
                    if (item.IsControlExpired)
                    {
                        decimal qty = (item.Quantity ?? 0) * (item.ConversionFactor ?? 0);
                        var ed = new ItemTransactionItemEdCollection();
                        ed.Query.Where(ed.Query.TransactionNo == item.TransactionNo,
                                       ed.Query.SequenceNo == item.SequenceNo);
                        ed.LoadAll();
                        decimal qtyDt = ed.Sum(i => (i.Quantity ?? 0) * (i.ConversionFactor ?? 0));

                        if (qty != qtyDt)
                        {
                            if (msg == string.Empty)
                                msg = item.ItemID;
                            else
                                msg += ", " + item.ItemID;
                        }
                    }
                }
                if (msg != string.Empty)
                {
                    args.MessageText = "Data can't be approved. Quantity detail Expiry Date for item: " + msg + " does not match the total quantity distributed.";
                    args.IsCancel = true;
                    return;
                }
            }

            if (AppSession.Parameter.IsDistributionUseApprovalLevel && ViewState["apprCount"].ToInt() > 0)
            {
                using (var trans = new esTransactionScope())
                {
                    var apprLevel = ViewState["apprLevel"].ToInt(); // Viewstate diisi saat loadcomplete
                    Util.ApprovalLevelUtil.Approve(args, TransactionCode.Distribution, txtTransactionNo.Text, apprLevel, AppSession.UserLogin.UserID);
                    if (!args.IsCancel)
                        trans.Complete();
                }
            }
            else
            {
                ApproveDistribution(args, it);
                if (args.IsCancel) return;

                if (AppSession.Parameter.IsAutoPrintDistributionReceipt)
                {
                    var printJobParameters = new PrintJobParameterCollection();
                    printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);

                    PrintManager.CreatePrintJob(AppSession.Parameter.ProgramIdPrintDistributionReceipt, printJobParameters);
                }
            }

            if (AppSession.Parameter.IsTxUsingEdDetail)
                grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = false; // ed ico
        }

        public static bool ApproveDistribution(ValidateArgs args, ItemTransaction it)
        {
            using (var trans = new esTransactionScope())
            {
                var loc = new Location();
                if (loc.LoadByPrimaryKey(it.FromLocationID) && loc.IsHoldForTransaction == true)
                {
                    args.MessageText = "Location: " + loc.LocationName +
                                       " in Hold For Transaction status. Transaction is not allowed.";
                    args.IsCancel = true;
                    return false;
                }

                bool valid = true;

                string itemId = string.Empty;

                var detailItems = new ItemTransactionItemCollection();
                detailItems.Query.Where(detailItems.Query.TransactionNo == it.TransactionNo);
                detailItems.LoadAll();

                foreach (var dt in detailItems)
                {
                    var ipm = new ItemProductMedic();
                    if (ipm.LoadByPrimaryKey(dt.ItemID))
                    {
                        if (!(ipm.IsInventoryItem ?? false)) continue;
                    }
                    else
                    {
                        var ipnm = new ItemProductNonMedic();
                        ipnm.LoadByPrimaryKey(dt.ItemID);
                        if (!(ipnm.IsInventoryItem ?? false)) continue;
                    }

                    var bal = new ItemBalance();
                    if (bal.LoadByPrimaryKey(it.FromLocationID, dt.ItemID))
                    {
                        if ((bal.Balance) <= 0)
                        {
                            valid = false;
                            itemId += dt.ItemID + ", ";
                        }
                    }
                    else
                    {
                        valid = false;
                        itemId += dt.ItemID + ", ";
                    }
                }

                if (!valid)
                {
                    args.MessageText = itemId + "has no balance available";
                    args.IsCancel = true;
                    return false;
                }


                it.IsApproved = true;
                it.ApprovedDate = (new DateTime()).NowAtSqlServer(); //DateTime.Now.Date;
                it.ApprovedByUserID = AppSession.UserLogin.UserID;
                it.LastUpdateDateTime = (new DateTime()).NowAtSqlServer(); //DateTime.Now;
                it.LastUpdateByUserID = AppSession.UserLogin.UserID;
                it.Save();

                // cek budget plan
                var str = ItemTransaction.CekBudgetPlan(it, detailItems);
                if (str != string.Empty)
                {
                    args.MessageText = str;
                    args.IsCancel = true;
                    return false;
                }
                // end cek budget plan

                var reference = new ItemTransaction();
                var referenceItems = new ItemTransactionItemCollection();
                var chargesBalances = new ItemBalanceCollection();
                var chargesBalancesTo = new ItemBalanceCollection();
                var chargesMovements= new ItemMovementCollection();
                var chargesDetailBalances = new ItemBalanceDetailCollection();
                var itemHistory = new ItemTransactionItemHistoryCollection();
                var itemBalanceDetailEd =  new ItemBalanceDetailEdCollection();

                string itemNoStock;

                ItemBalance.PrepareItemBalancesForDistribution(detailItems, it.FromServiceUnitID,
                                                               it.FromLocationID, it.ToLocationID,
                                                               AppSession.UserLogin.UserID, it.ReferenceNo,
                                                               ref reference, ref referenceItems,
                                                               ref chargesBalances, ref chargesBalancesTo,
                                                               ref chargesDetailBalances, ref chargesMovements,
                                                               ref itemHistory, ref itemBalanceDetailEd,
                                                               out itemNoStock, AppSession.Parameter.IsEnabledStockWithEdControl);

                if (!string.IsNullOrEmpty(itemNoStock))
                {
                    if (itemNoStock.Substring(0, 2) == "x|")
                        args.MessageText = itemNoStock.Replace("x|", "");
                    else if (itemNoStock.Length >= 5 && itemNoStock.Substring(0, 5) == "Zero|")
                        args.MessageText = "Zero cost price of item : " + itemNoStock.Replace("Zero|", "");
                    else
                        args.MessageText = "Insufficient balance of item : " + itemNoStock;

                    args.IsCancel = true;
                    return false;
                }

                detailItems.Save();

                //if (reference != null)
                //    reference.Save();
                if (referenceItems != null)
                    referenceItems.Save();
                if (chargesBalances != null)
                    chargesBalances.Save();
                if (chargesBalancesTo != null)
                    chargesBalancesTo.Save();
                if (chargesDetailBalances != null)
                    chargesDetailBalances.Save();
                if (itemBalanceDetailEd != null)
                    itemBalanceDetailEd.Save();
                if (chargesMovements != null)
                    chargesMovements.Save();
                if (itemHistory != null)
                    itemHistory.Save();

                //jurnal ini dijalankan jika inventory dicatat per lokasi, jika coa inventory tidak masuk ke farmasi semua
                //var app = new AppParameter();
                //app.LoadByPrimaryKey("acc_IsUnitBasedProductAccount");
                //if (app.ParameterValue == "Yes")
                //{
                //    var isClosingPeriod = PostingStatus.IsPeriodeClosed(it.TransactionDate.Value);
                //    if (isClosingPeriod)
                //    {
                //        args.MessageText = "Financial statements for period: " +
                //                           string.Format("{0:MMMM-yyyy}", it.TransactionDate) +
                //                           " have been closed. Please contact the authorities.";
                //        args.IsCancel = true;
                //        return false;
                //    }

                //    /* Automatic Journal Testing Start */

                //    int? journalId = JournalTransactions.AddNewDistributionLocationBasedJournal(it,
                //        AppSession.UserLogin.UserID, 0);

                //    /* Automatic Journal Testing End */
                //}

                var isDistributionAutoConfirm = AppSession.Parameter.IsDistributionAutoConfirm;
                if (!isDistributionAutoConfirm)
                {
                    var x = new LocationExceptionDistributionConfirm();
                    if (x.LoadByPrimaryKey(it.FromLocationID, it.ToLocationID))
                        isDistributionAutoConfirm = true;
                }

                //if (AppSession.Parameter.IsDistributionAutoConfirm)
                if (isDistributionAutoConfirm)
                {
                    loc = new Location();
                    if (loc.LoadByPrimaryKey(it.ToLocationID) && loc.IsHoldForTransaction == true)
                    {
                        args.MessageText = "Location: " + loc.LocationName +
                                           " in Hold For Transaction status. Transaction is not allowed.";
                        args.IsCancel = true;
                        return false;
                    }

                    var chargesMovementsTo = new ItemMovementCollection();
                    var chargesDetailBalancesTo = new ItemBalanceDetailCollection();
                    var itemBalanceDetailEdTo = new ItemBalanceDetailEdCollection();

                    ItemBalance.PrepareItemBalancesForAutoDistribution(detailItems, it, AppSession.UserLogin.UserID,
                        ref chargesBalancesTo, ref chargesDetailBalancesTo, ref chargesMovementsTo, ref chargesMovements, ref itemBalanceDetailEdTo, AppSession.Parameter.IsEnabledStockWithEdControl);

                    if (chargesBalancesTo != null)
                        chargesBalancesTo.Save();
                    if (chargesDetailBalancesTo != null)
                        chargesDetailBalancesTo.Save();
                    if (itemBalanceDetailEdTo != null)
                        itemBalanceDetailEdTo.Save();
                    if (chargesMovementsTo != null)
                        chargesMovementsTo.Save();
                    
                    //closing transaksi
                    foreach (var item in detailItems)
                    {
                        item.QuantityFinishInBaseUnit = item.Quantity * item.ConversionFactor;
                        item.IsClosed = true;
                    }
                    detailItems.Save();

                    it.IsClosed = true;
                    it.Save();
                    //end closing transaksi
                }

                trans.Complete();
            }

            Finance.Voucher.VoucherEntry.VoucherEntryDetail.JournalDistribution(0, it.TransactionNo);

            return true;
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            UnApproveDistribution(args, txtTransactionNo.Text);
        }

        private void UnApproveDistribution(ValidateArgs args, string transactionNo)
        {
            var entity = new ItemTransaction();
            entity.LoadByPrimaryKey(transactionNo);
            UnApproveDistribution(args, entity);
        }

        public static void UnApproveDistribution(ValidateArgs args, ItemTransaction it)
        {
            var movement = new ItemMovementCollection();
            movement.Query.Where(movement.Query.TransactionNo == it.TransactionNo);
            movement.LoadAll();
            if (movement.Count == 0)
            {
                var itemTransactionItems = new ItemTransactionItemCollection();
                itemTransactionItems.Query.Where(itemTransactionItems.Query.TransactionNo == it.TransactionNo);
                itemTransactionItems.LoadAll();
                (new ItemTransaction()).UnApproved(it, itemTransactionItems, AppSession.UserLogin.UserID);
            }
            else
            {
                args.MessageText = "Un-Approved process is not allowed.";
                args.IsCancel = true;
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsDistributionUseApprovalLevel))
                if (Util.ApprovalLevelUtil.IsApprovalLevelInProgress(args, txtTransactionNo.Text)) return;

            (new ItemTransaction()).Void(txtTransactionNo.Text, AppSession.UserLogin.UserID);

            if (AppSession.Parameter.IsTxUsingEdDetail)
                grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = false; // ed ico
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            (new ItemTransaction()).UnVoid(txtTransactionNo.Text, AppSession.UserLogin.UserID);
            if (AppSession.Parameter.IsTxUsingEdDetail)
                grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = !AppSession.Parameter.IsEnabledStockWithEdControl; // ed ico
        }

        private bool IsApprovedOrVoid(ItemTransaction entity, ValidateArgs args)
        {
            if (entity.IsApproved.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            if (entity.IsVoid.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }
            return true;
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ItemTransaction());

            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer(); //DateTime.Now;

            PopulateNewTransactionNo();
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ItemTransaction();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (cboFromServiceUnitID.SelectedValue == cboToServiceUnitID.SelectedValue &&
                cboFromLocationID.SelectedValue == cboToLocationID.SelectedValue)
            {
                args.MessageText = "Unit from and to are the same unit.";
                args.IsCancel = true;
                return;
            }

            var exception = new LocationException();
            if (exception.LoadByPrimaryKey(cboFromLocationID.SelectedValue, cboToLocationID.SelectedValue))
            {
                args.MessageText = "The selected location is an exception for distribution.";
                args.IsCancel = true;
                return;
            }

            PopulateNewTransactionNo();
            // save autonumber immediately to decrease time gap between create and save
            _autoNumber.Save();

            var entity = new ItemTransaction();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new ItemTransaction();
            entity.AddNew();
            SetEntityValue(entity);

            if (string.IsNullOrEmpty(entity.ToLocationID))
            {
                args.MessageText = "To Location required.";
                args.IsCancel = true;
                return;
            }

            if (ItemTransactionItems.Where(x => x.TransactionNo == txtTransactionNo.Text).Count() == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(entity.ReferenceNo))
            {
                if (AppSession.Parameter.IsMandatoryDistributionTypeOnDirectDistribution && string.IsNullOrEmpty(entity.SRDistributionType))
                {
                    args.MessageText = "Distribution Type required.";
                    args.IsCancel = true;
                    return;
                }
            }

            SaveEntity(entity);

            // Email to user
            if (boxApprovalProgress.Visible)
            {
                Util.ApprovalLevelUtil.EmailToApprover(entity);
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (cboFromServiceUnitID.SelectedValue == cboToServiceUnitID.SelectedValue &&
                cboFromLocationID.SelectedValue == cboToLocationID.SelectedValue)
            {
                args.MessageText = "Unit from and to are the same unit.";
                args.IsCancel = true;
                return;
            }

            var exception = new LocationException();
            if (exception.LoadByPrimaryKey(cboFromLocationID.SelectedValue, cboToLocationID.SelectedValue))
            {
                args.MessageText = "The selected location is an exception for distribution.";
                args.IsCancel = true;
                return;
            }

            var entity = new ItemTransaction();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                SetEntityValue(entity);

                if (string.IsNullOrEmpty(entity.ToLocationID))
                {
                    args.MessageText = "To Location required.";
                    args.IsCancel = true;
                    return;
                }

                if (ItemTransactionItems.Where(x => x.TransactionNo == txtTransactionNo.Text).Count() == 0)
                {
                    args.MessageText = AppConstant.Message.RecordDetailEmpty;
                    args.IsCancel = true;
                    return;
                }

                if (string.IsNullOrEmpty(entity.ReferenceNo))
                {
                    if (AppSession.Parameter.IsMandatoryDistributionTypeOnDirectDistribution && string.IsNullOrEmpty(entity.SRDistributionType))
                    {
                        args.MessageText = "Distribution Type required.";
                        args.IsCancel = true;
                        return;
                    }
                }

                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text.Trim());
            auditLogFilter.TableName = "ItemTransaction";
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuEdit()
        {
            return txtTransactionNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !chkIsApproved.Checked;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !chkIsVoid.Checked;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemGrid(oldVal, newVal);
            btnGetPickList.Enabled = newVal != AppEnum.DataMode.Read;
            btnResetItem.Enabled = newVal != AppEnum.DataMode.Read;
            btnProcess.Enabled = (newVal == AppEnum.DataMode.Read && !chkIsApproved.Checked && !chkIsVoid.Checked);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ItemTransaction();
            if (parameters.Length > 0)
            {
                String transactionNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(transactionNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtTransactionNo.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var itemTransaction = (ItemTransaction)entity;
            txtTransactionNo.Text = itemTransaction.TransactionNo;
            txtTransactionDate.SelectedDate = itemTransaction.TransactionDate;

            ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID,
                BusinessObject.Reference.TransactionCode.Distribution, true);
            ComboBox.SelectedValue(cboFromServiceUnitID, itemTransaction.FromServiceUnitID);
            cboFromServiceUnitID.SelectedValue = itemTransaction.FromServiceUnitID;
            ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID,
                BusinessObject.Reference.TransactionCode.DistributionRequest, false, string.Empty,
                string.IsNullOrEmpty(itemTransaction.SRItemType) ? string.Empty : itemTransaction.SRItemType);

            if ((!string.IsNullOrEmpty(Request.QueryString["drn"])) && DataModeCurrent == AppEnum.DataMode.New)
            {
                txtReferenceNo.Text = Request.QueryString["drn"];
                PopulateGridDetailFromReferenceNo();
            }
            else
            {
                txtReferenceNo.Text = itemTransaction.ReferenceNo;
                cboFromServiceUnitID.SelectedValue = itemTransaction.FromServiceUnitID;
                if (!string.IsNullOrEmpty(itemTransaction.FromServiceUnitID))
                {
                    ComboBox.PopulateWithServiceUnitForLocation(cboFromLocationID, itemTransaction.FromServiceUnitID);
                    if (!string.IsNullOrEmpty(itemTransaction.FromLocationID))
                        cboFromLocationID.SelectedValue = itemTransaction.FromLocationID;
                    else
                        cboFromLocationID.SelectedIndex = 1;
                }
                else
                {
                    cboFromLocationID.Items.Clear();
                    cboFromLocationID.Text = string.Empty;
                }
                cboToServiceUnitID.SelectedValue = itemTransaction.ToServiceUnitID;
                if (!string.IsNullOrEmpty(itemTransaction.ToServiceUnitID))
                {
                    ComboBox.PopulateWithServiceUnitForLocation(cboToLocationID, itemTransaction.ToServiceUnitID);
                    if (!string.IsNullOrEmpty(itemTransaction.ToLocationID))
                        cboToLocationID.SelectedValue = itemTransaction.ToLocationID;
                    else
                        cboToLocationID.SelectedIndex = 1;
                }
                else
                {
                    cboToLocationID.Items.Clear();
                    cboToLocationID.Text = string.Empty;
                }

                ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboFromServiceUnitID.SelectedValue,
                    BusinessObject.Reference.TransactionCode.Distribution);
                cboSRItemType.SelectedValue = itemTransaction.SRItemType;
                ComboBox.PopulateWithOneItemGroup(cboItemGroupID, itemTransaction.ItemGroupID);
                PopulateGridDetail();

                if (string.IsNullOrEmpty(itemTransaction.FromServiceUnitID))
                {
                    cboFromServiceUnitID.SelectedValue = string.Empty;
                    cboFromServiceUnitID.Text = string.Empty;
                }

                if (string.IsNullOrEmpty(itemTransaction.ToServiceUnitID))
                {
                    cboToServiceUnitID.SelectedValue = string.Empty;
                    cboToServiceUnitID.Text = string.Empty;
                }
            }
            chkIsVoid.Checked = itemTransaction.IsVoid ?? false;
            chkIsApproved.Checked = itemTransaction.IsApproved ?? false;
            txtNotes.Text = itemTransaction.Notes;
            cboSRDistributionType.SelectedValue = itemTransaction.SRDistributionType;

            btnProcess.Enabled = (!chkIsApproved.Checked && !chkIsVoid.Checked);

            grdApproval.Rebind();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(ItemTransaction entity)
        {
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionCode = BusinessObject.Reference.TransactionCode.Distribution;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.ReferenceNo = txtReferenceNo.Text;

            entity.FromServiceUnitID = cboFromServiceUnitID.SelectedValue;
            entity.FromLocationID = cboFromLocationID.SelectedValue;

            entity.ToServiceUnitID = cboToServiceUnitID.SelectedValue;
            entity.ToLocationID = cboToLocationID.SelectedValue;

            entity.SRItemType = cboSRItemType.SelectedValue;
            entity.ItemGroupID = cboItemGroupID.SelectedValue;
            entity.Notes = txtNotes.Text;
            entity.SRDistributionType = cboSRDistributionType.SelectedValue;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer(); //DateTime.Now;
            }

            //Update Detil
            foreach (ItemTransactionItem item in ItemTransactionItems)
            {
                if (item.es.IsAdded)
                {
                    item.TransactionNo = txtTransactionNo.Text;
                }
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer(); // DateTime.Now;
                }
            }
        }

        private void SaveEntity(ItemTransaction entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                ItemTransactionItems.Save();

                //autonumber has been saved on SetEntity
                //if (DataModeCurrent == AppEnum.DataMode.New)
                //    _autoNumber.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ItemTransactionQuery("a");
            var qusr = new AppUserServiceUnitQuery("u");
            que.InnerJoin(qusr).On(que.FromServiceUnitID == qusr.ServiceUnitID &&
                                   qusr.UserID == AppSession.UserLogin.UserID);

            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.TransactionNo > txtTransactionNo.Text &&
                          que.TransactionCode == BusinessObject.Reference.TransactionCode.Distribution);
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where(que.TransactionNo < txtTransactionNo.Text &&
                          que.TransactionCode == BusinessObject.Reference.TransactionCode.Distribution);
                que.OrderBy(que.TransactionNo.Descending);
            }
            var entity = new ItemTransaction();
            entity.Load(que);
            ItemTransactionItems = null;
            OnPopulateEntryControl(entity);

            if (AppSession.Parameter.IsTxUsingEdDetail)
                grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible =
                    !chkIsApproved.Checked && !chkIsVoid.Checked && !AppSession.Parameter.IsEnabledStockWithEdControl; // ed ico
        }

        #endregion

        #region Record Detail Method Function

        private ItemTransactionItemCollection ItemTransactionItems
        {
            get
            {
                object obj = Session["DistributionItems" + Request.UserHostName];
                if (obj != null)
                    return ((ItemTransactionItemCollection)(obj));

                var coll = LoadItemTransactionItem();
                Session["DistributionItems" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["DistributionItems" + Request.UserHostName] = value; }
        }

        private ItemTransactionItemCollection LoadItemTransactionItem()
        {
            var coll = new ItemTransactionItemCollection();
            var query = new ItemTransactionItemQuery("a");
            var itq = new ItemTransactionQuery("b");
            var suq = new ServiceUnitQuery("c");
            var iq = new ItemQuery("d");
            var ibq = new ItemBalanceQuery("e");
            var ibq2 = new ItemBalanceQuery("g");

            query.InnerJoin(itq).On(query.TransactionNo == itq.TransactionNo);
            query.InnerJoin(suq).On(itq.ToServiceUnitID == suq.ServiceUnitID);
            query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
            query.LeftJoin(ibq).On(query.ItemID == ibq.ItemID && ibq.LocationID == itq.ToLocationID);
            query.LeftJoin(ibq2).On(query.ItemID == ibq2.ItemID && ibq2.LocationID == itq.FromLocationID);

            query.Where(query.TransactionNo == txtTransactionNo.Text);
            query.OrderBy(query.ItemID.Ascending);

            query.Select(
                query,
                iq.ItemName.As("refToItem_ItemName"),
                @"<ISNULL(e.Balance, 0)/a.ConversionFactor AS 'refToItemBalance_Balance'>",
                @"<ISNULL(e.Booking, 0)/a.ConversionFactor AS 'refToItemBalance_Booking'>",
                @"<ISNULL(e.Minimum, 0)/a.ConversionFactor AS 'refToItemBalance_Minimum'>",
                @"<ISNULL(e.Maximum, 0)/a.ConversionFactor AS 'refToItemBalance_Maximum'>",
                @"<ISNULL(g.Balance, 0)/a.ConversionFactor AS 'refToItemBalance_Balance2'>"
                );

            if (cboSRItemType.SelectedValue == ItemType.Medical)
            {
                var ipq = new ItemProductMedicQuery("f");
                query.LeftJoin(ipq).On(query.ItemID == ipq.ItemID);
                query.Select(@"<ISNULL(f.IsControlExpired, 0) AS 'refToItemProduct_IsControlExpired'>");
            }
            else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
            {
                var ipq = new ItemProductNonMedicQuery("f");
                query.LeftJoin(ipq).On(query.ItemID == ipq.ItemID);
                query.Select(@"<ISNULL(f.IsControlExpired, 0) AS 'refToItemProduct_IsControlExpired'>");
            }
            else
            {
                var ipq = new ItemKitchenQuery("f");
                query.LeftJoin(ipq).On(query.ItemID == ipq.ItemID);
                query.Select(@"<ISNULL(f.IsControlExpired, 0) AS 'refToItemProduct_IsControlExpired'>");
            }

            coll.Load(query);
            return coll;
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemTransactionItem.Columns[0].Visible = isVisible;
            grdItemTransactionItem.Columns[grdItemTransactionItem.Columns.Count - 1].Visible = isVisible;

            if (AppSession.Parameter.IsTxUsingEdDetail)
                grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = !isVisible && !chkIsApproved.Checked && !AppSession.Parameter.IsEnabledStockWithEdControl;

            // ed ico

            if (AppSession.Parameter.IsDistributionOnlyBasedOnRequest)
                grdItemTransactionItem.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
            else
                grdItemTransactionItem.MasterTableView.CommandItemDisplay = isVisible
                    ? GridCommandItemDisplay.Top
                    : GridCommandItemDisplay.None;


            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                ItemTransactionItems = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdItemTransactionItem.Rebind();

            btnProcess.Enabled = !isVisible && !chkIsApproved.Checked && !chkIsVoid.Checked;
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            ItemTransactionItems = null; //Reset Record Detail
            grdItemTransactionItem.DataSource = ItemTransactionItems;
            grdItemTransactionItem.MasterTableView.IsItemInserted = false;
            grdItemTransactionItem.MasterTableView.ClearEditItems();
            grdItemTransactionItem.DataBind();
        }

        protected void grdItemTransactionItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemTransactionItem.DataSource = ItemTransactionItems;
        }

        protected void grdItemTransactionItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String sequenceNo =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        ItemTransactionItemMetadata.ColumnNames.SequenceNo]);
            ItemTransactionItem entity = FindItemTransactionItem(sequenceNo);
            if (entity != null)
            {
                var userControl =
                    (DistributionItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                if (userControl != null)
                {
                    SetEntityValue(entity, userControl);
                }
            }
        }

        private ItemTransactionItem FindItemTransactionItem(String sequenceNo)
        {
            return ItemTransactionItems.Where(x => x.SequenceNo == sequenceNo &&
                (x.TransactionNo ?? string.Empty) == (x.es.IsAdded ? string.Empty : txtTransactionNo.Text)).First();
        }

        protected void grdItemTransactionItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String sequenceNo =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemTransactionItemMetadata.ColumnNames.SequenceNo
                        ]);
            ItemTransactionItem entity = FindItemTransactionItem(sequenceNo);
            if (entity != null)
                entity.MarkAsDeleted();

            cboSRItemType.Enabled = !(ItemTransactionItems.Count > 0);
            cboItemGroupID.Enabled = cboSRItemType.Enabled;
            cboFromServiceUnitID.Enabled = cboSRItemType.Enabled;
            cboFromLocationID.Enabled = cboSRItemType.Enabled;
            cboToServiceUnitID.Enabled = cboSRItemType.Enabled;
            cboToLocationID.Enabled = cboSRItemType.Enabled;
        }

        protected void grdItemTransactionItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            ItemTransactionItem entity = ItemTransactionItems.AddNew();
            var userControl =
                (DistributionItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                SetEntityValue(entity, userControl);
            }

            //grid not close first
            e.Canceled = true;
            grdItemTransactionItem.Rebind();
        }

        private void SetEntityValue(ItemTransactionItem entity, DistributionItemDetail userControl)
        {
            entity.ItemID = userControl.ItemID;
            entity.SequenceNo = userControl.SequenceNo;
            entity.Quantity = userControl.Quantity;
            entity.ItemName = userControl.ItemName;
            entity.SRItemUnit = userControl.SRItemUnit;
            entity.ConversionFactor = userControl.ConversionFactor;

            var it = new Item();
            it.LoadByPrimaryKey(entity.ItemID);
            if (it.SRItemType == BusinessObject.Reference.ItemType.Medical)
            {
                var ipm = new ItemProductMedic();
                if (ipm.LoadByPrimaryKey(entity.ItemID))
                {
                    entity.CostPrice = ipm.CostPrice;
                    entity.Price = ipm.PriceInBaseUnit * entity.ConversionFactor;
                    entity.PriceInCurrency = ipm.PriceInBaseUnit * entity.ConversionFactor;
                    entity.IsControlExpired = ipm.IsControlExpired ?? false;
                }
                else
                {
                    entity.CostPrice = 0;
                    entity.Price = 0;
                    entity.PriceInCurrency = 0;
                    entity.IsControlExpired = false;
                }
            }
            else if (it.SRItemType == BusinessObject.Reference.ItemType.NonMedical)
            {
                var ipm = new ItemProductNonMedic();
                if (ipm.LoadByPrimaryKey(entity.ItemID))
                {
                    entity.CostPrice = ipm.CostPrice;
                    entity.Price = ipm.PriceInBaseUnit * entity.ConversionFactor;
                    entity.PriceInCurrency = ipm.PriceInBaseUnit * entity.ConversionFactor;
                    entity.IsControlExpired = ipm.IsControlExpired ?? false;
                }
                else
                {
                    entity.CostPrice = 0;
                    entity.Price = 0;
                    entity.PriceInCurrency = 0;
                    entity.IsControlExpired = false;
                }
            }
            else
            {
                var ipm = new ItemKitchen();
                if (ipm.LoadByPrimaryKey(entity.ItemID))
                {
                    entity.CostPrice = ipm.CostPrice;
                    entity.Price = ipm.PriceInBaseUnit * entity.ConversionFactor;
                    entity.PriceInCurrency = ipm.PriceInBaseUnit * entity.ConversionFactor;
                    entity.IsControlExpired = ipm.IsControlExpired ?? false;
                }
                else
                {
                    entity.CostPrice = 0;
                    entity.Price = 0;
                    entity.PriceInCurrency = 0;
                    entity.IsControlExpired = false;
                }
            }
            entity.Balance = userControl.Balance / userControl.ConversionFactor;
            entity.Booking = userControl.Booking / userControl.ConversionFactor;
            entity.Minimum = userControl.Minimum / userControl.ConversionFactor;
            entity.Maximum = userControl.Maximum / userControl.ConversionFactor;
            // 
            var ibl = new ItemBalance();
            if (ibl.LoadByPrimaryKey(cboFromLocationID.SelectedValue, entity.ItemID))
            {
                entity.Balance2 = ibl.Balance / userControl.ConversionFactor;
            }
            else
            {
                entity.Balance2 = 0;
            }
        }

        #endregion

        protected void cboFromServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateNewTransactionNo();
            ComboBox.PopulateWithServiceUnitForLocation(cboFromLocationID, e.Value);
            cboFromLocationID.SelectedIndex = 1;
            ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboFromServiceUnitID.SelectedValue,
                BusinessObject.Reference.TransactionCode.Distribution);
            cboSRItemType.Text = string.Empty;
            cboSRItemType.SelectedValue = string.Empty;
        }

        protected void cboToServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ComboBox.PopulateWithServiceUnitForLocation(cboToLocationID, e.Value);
            cboToLocationID.SelectedIndex = 1;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            if (string.IsNullOrEmpty(eventArgument))
                eventArgument = string.Empty;

            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (sourceControl is RadGrid)
            {
                if (eventArgument.Contains("_approv|"))
                {
                    AproveUnApproveViaApproveLevel(eventArgument, true);
                }
                else if (eventArgument.Contains("_unapprov|"))
                {
                    AproveUnApproveViaApproveLevel(eventArgument, false);
                }
            }
            else if (eventArgument.Contains("addwithbarcode"))
            {
                var barcode = eventArgument.Split('|')[1];
                if (AddItemDetailWithBarcode(barcode))
                {
                    grdItemTransactionItem.Rebind();
                }
                var txtBarcode = (RadTextBox)sourceControl;
                txtBarcode.Text = string.Empty;
                txtBarcode.Focus();
            }

        }

        private void AproveUnApproveViaApproveLevel(string eventArgument, bool isApproved)
        {
            var args = new ValidateArgs();
            var param = eventArgument.Split('|');
            var apprLevel = param[1].ToInt();

            using (var trans = new esTransactionScope())
            {
                if (isApproved)
                    Util.ApprovalLevelUtil.Approve(args, TransactionCode.Distribution, txtTransactionNo.Text,
                        apprLevel, AppSession.UserLogin.UserID);
                else
                    Util.ApprovalLevelUtil.UnApprove(args, TransactionCode.Distribution, txtTransactionNo.Text,
                    apprLevel);

                if (!args.IsCancel)
                {
                    trans.Complete();
                    Helper.ShowMessageAfterPostback(Page, "Process success.");
                }
                else
                    Helper.ShowMessageAfterPostback(Page,
                        string.Format("{0}. Process failed.", args.MessageText));
            }
            grdApproval.Rebind();

            //Approval Status Information
            var it = new ItemTransaction();
            it.LoadByPrimaryKey(txtTransactionNo.Text);

            var info = (Panel)Helper.FindControlRecursive(Master, "fw_PanelStatus");
            info.Visible = it.IsApproved ?? false;
            //((RadBinaryImage)Helper.FindControlRecursive(Master, "fw_StampStatus")).Visible = (it.IsApproved ?? false);
        }

        protected void cboItemGroupID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            ItemGroupQuery query = new ItemGroupQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.ItemGroupID,
                    query.ItemGroupName
                );
            query.Where
                (
                    query.Or
                        (
                            query.ItemGroupID.Like(searchTextContain),
                            query.ItemGroupName.Like(searchTextContain)
                        ),
                    query.IsActive == true,
                    query.SRItemType == cboSRItemType.SelectedValue
                );

            cboItemGroupID.DataSource = query.LoadDataTable();
            cboItemGroupID.DataBind();
        }

        protected void cboItemGroupID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemGroupName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemGroupID"].ToString();
        }

        protected void cboSRItemType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboItemGroupID.Text = string.Empty;
            cboItemGroupID.SelectedValue = null;

            ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, BusinessObject.Reference.TransactionCode.DistributionRequest, false, string.Empty, e.Value);
            cboToLocationID.SelectedValue = string.Empty;
            cboToLocationID.Text = string.Empty;
        }

        protected void grdApproval_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!boxApprovalProgress.Visible) return;

            var dtbApprov = Util.ApprovalLevelUtil.ApprovalLevelQue(txtTransactionNo.Text);
            Session["ds_grdApproval"] = dtbApprov;
            grdApproval.DataSource = dtbApprov;
        }

        #region Barcode entry

        private bool AddItemDetailWithBarcode(string barcode)
        {
            //Check hanya untuk type item 11 Medical & 21 Non Medical
            var item = new Item();
            if (!item.LoadByBarcode(barcode))
            {
                // Barcode bisa sbg ItemID
                if (!item.LoadByPrimaryKey(barcode))
                    return false;
            }

            var itemID = item.ItemID;

            if (item.SRItemType != cboSRItemType.SelectedValue)
                return false;

            //Check bila sudah ada maka tambah di qty nya saja
            //foreach (var transactionItem in ItemTransactionItems)
            //{
            //    if (transactionItem.ItemID == itemID)
            //    {
            //        entity = transactionItem;
            //        break;
            //    }
            //}
            ItemTransactionItem entity = ItemTransactionItems.FirstOrDefault(transactionItem => transactionItem.ItemID == itemID);
            if (entity != null)
            {
                entity.Quantity += 1;
            }
            else
            {
                var sequenceNo = ItemTransactionItems.Count > 0
                   ? string.Format("{0:000}", int.Parse(ItemTransactionItems[ItemTransactionItems.Count - 1].SequenceNo) + 1)
                   : "001";
                entity = ItemTransactionItems.AddNew();
                var itemEntry = (DistributionItemDetail)LoadControl("DistributionItemDetail.ascx");
                itemEntry.PopulateWithItemID(item.ItemID, item.SRItemType);
                SetEntityValue(entity, itemEntry);

                entity.SequenceNo = sequenceNo;
                entity.ItemName = item.ItemName;
                entity.ItemID = itemID;
                entity.Quantity = 1;
            }

            return true;
        }

        #endregion
    }
}
