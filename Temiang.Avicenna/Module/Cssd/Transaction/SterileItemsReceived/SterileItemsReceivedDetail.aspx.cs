using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class SterileItemsReceivedDetail : BasePageDetail
    {
        private string getPageID
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        private AppAutoNumberLast _autoNumber, _autoItemNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "SterileItemsReceivedSearch.aspx";
            UrlPageList = getPageID == "ver" ? "SterileItemsReceivedVerificationList.aspx" :  "SterileItemsReceivedList.aspx";

            ProgramID = AppConstant.Program.CssdSterileItemsReceived;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSenderByID, AppEnum.StandardReference.CssdSender);

                bool isNotUsingPackagingProcess = !AppSession.Application.IsMenuCssdPackagingActive;

                grdItem.Columns.FindByUniqueName("ExpiredDate").Visible = isNotUsingPackagingProcess && AppSession.Parameter.IsCssdExpiredValidateInReceiveDetail;
                grdItem.Columns.FindByUniqueName("ReuseTo").Visible = isNotUsingPackagingProcess;
                grdItem.Columns.FindByUniqueName("IsNeedUltrasound").Visible = isNotUsingPackagingProcess;
                grdItem.Columns.FindByUniqueName("IsDtt").Visible = isNotUsingPackagingProcess && AppSession.Parameter.IsCssdUsingDttTerm;
                grdItem.Columns.FindByUniqueName("DttDescription").Visible = isNotUsingPackagingProcess && !AppSession.Parameter.IsCssdUsingDttTerm;

                trSRInstrumentType.Visible = AppSession.Parameter.IsCentralizedCssd;

                CssdSterileItemsReceivedItems = null;
                CssdSterileItemsReceivedItemDetails = null;
            }

            //AjaxManager.AjaxRequest += AjaxManager_AjaxRequest;
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            if (getPageID == "ver")
            {
                ToolBarMenuSearch.Visible = false;
                ToolBarMenuAdd.Visible = false;
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItem, grdItem);
            ajax.AddAjaxSetting(grdItem, cboFromServiceUnitID);
            ajax.AddAjaxSetting(grdItem, cboFromRoomID);
            ajax.AddAjaxSetting(grdItem, cboSenderByID);
            ajax.AddAjaxSetting(grdItem, txtSenderBy);

            ajax.AddAjaxSetting(cboFromServiceUnitID, cboFromServiceUnitID);
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboFromRoomID);

            ajax.AddAjaxSetting(cboSenderByID, cboSenderByID);
            ajax.AddAjaxSetting(cboSenderByID, txtSenderBy);

            //ajax.AddAjaxSetting(AjaxManager, grdItem);
        }

        private void PopulateGridDetailFromRequestNo()
        {
            var hd = new CssdSterileItemsRequest();
            hd.LoadByPrimaryKey(txtProductionNo.Text);

            if (!string.IsNullOrEmpty(hd.FromServiceUnitID))
            {
                var q = new ServiceUnitQuery();
                q.Where(q.ServiceUnitID == hd.FromServiceUnitID);
                cboFromServiceUnitID.DataSource = q.LoadDataTable();
                cboFromServiceUnitID.DataBind();
                cboFromServiceUnitID.SelectedValue = hd.FromServiceUnitID;

                PopulateRoomList(hd.FromServiceUnitID, false);
                cboFromRoomID.SelectedValue = hd.FromRoomID;
            }
            else
            {
                cboFromServiceUnitID.Items.Clear();
                cboFromServiceUnitID.Text = string.Empty;
                cboFromRoomID.Items.Clear();
                cboFromRoomID.Text = string.Empty;
            }

            cboSenderByID.SelectedValue = hd.SenderByID;
            txtSenderBy.Text = hd.SenderBy;

            cboFromServiceUnitID.Enabled = false;
            cboFromRoomID.Enabled = false;
            cboSenderByID.Enabled = false;
            txtSenderBy.ReadOnly = true;

            grdItem.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
            grdItem.Columns[grdItem.Columns.Count - 2].Visible = false;

            var query = new CssdSterileItemsRequestItemQuery("a");
            var iq = new ItemQuery("b");
            var unit = new AppStandardReferenceItemQuery("c");

            query.InnerJoin(iq).On(iq.ItemID == query.ItemID);
            query.InnerJoin(unit).On(unit.StandardReferenceID == "ItemUnit" && unit.ItemID == query.SRCssdItemUnit);
            
            query.Select
                (
                    query.RequestNo,
                    query.RequestSeqNo,
                    query.ItemID,
                    iq.ItemName,
                    query.SRCssdItemUnit,
                    unit.ItemName.As("CssdItemUnit"),
                    query.Qty,
                    query.Notes
                );

            query.Where(query.RequestNo == hd.RequestNo);

            query.OrderBy(query.RequestSeqNo.Ascending);

            var dtb = query.LoadDataTable();

            Session["CssdRequestItemSelected" + Request.UserHostName] = dtb;

            PopulateFromSelectedRequest();
        }

        private void PopulateFromSelectedRequest()
        {
            object obj = Session["CssdRequestItemSelected" + Request.UserHostName];
            if (obj == null)
                return;

            //delete previouse item
            if (CssdSterileItemsReceivedItems.Count > 0)
                CssdSterileItemsReceivedItems.MarkAllAsDeleted();

            if (CssdSterileItemsReceivedItemDetails.Count > 0)
                CssdSterileItemsReceivedItemDetails.MarkAllAsDeleted();

            DataTable dtbSelectedItem = (DataTable)obj;
            if (dtbSelectedItem.Rows.Count > 0)
            {
                txtProductionNo.Text = dtbSelectedItem.Rows[0]["RequestNo"].ToString();
            }

            int i = 0;
            foreach (DataRow row in dtbSelectedItem.Rows)
            {
                if (Convert.ToDecimal(row["Qty"]) <= 0)
                    continue;

                i++;
                string seqNo = string.Format("{0:000}", i);

                var entity = CssdSterileItemsReceivedItems.AddNew();
                entity.ReceivedNo = txtReceivedNo.Text;
                entity.ReceivedSeqNo = seqNo;
                entity.ItemID = row["ItemID"].ToString();
                entity.ItemName = row["ItemName"].ToString();
                entity.Qty = Convert.ToDecimal(row["Qty"]);
                entity.SRCssdItemUnit = row["SRCssdItemUnit"].ToString(); 
                entity.CssdItemUnit = row["CssdItemUnit"].ToString();
                entity.Notes = row["Notes"].ToString();
                entity.CssdItemNo = string.Empty;
                entity.str.ExpiredDate = string.Empty;
                entity.ReuseTo = 0;
                entity.IsNeedUltrasound = false;
                entity.IsDtt = false;

                var items = new CssdItemDetailCollection();
                items.Query.Where(items.Query.ItemID == entity.ItemID);
                items.LoadAll();
                
                foreach (var d in items)
                {
                    var det = CssdSterileItemsReceivedItemDetails.AddNew();
                    det.ReceivedNo = entity.ReceivedNo;
                    det.ReceivedSeqNo = entity.ReceivedSeqNo;
                    det.ItemID = entity.ItemID;
                    det.ItemDetailID = d.ItemDetailID;

                    var item = new Item();
                    if (item.LoadByPrimaryKey(det.ItemDetailID))
                        det.ItemName = item.ItemName;
                    else det.ItemName = string.Empty;

                    det.Qty = entity.Qty * d.Qty;
                    det.QtyReceived = det.Qty;

                    det.IsBrokenInstrument = false;
                    det.QtyReplacements = 0;

                    det.IsBrokenInstrumentX = false;
                    det.QtyReplacementsX = 0;

                    var itemDetQ = new VwItemProductMedicNonMedicQuery();
                    itemDetQ.Where(itemDetQ.ItemID == det.ItemDetailID);
                    var itemDet = new VwItemProductMedicNonMedic();
                    itemDet.Load(itemDetQ);

                    det.SRItemUnit = itemDet.SRItemUnit;
                }
            }

            grdItem.DataSource = CssdSterileItemsReceivedItems;
            grdItem.DataBind();

            //CssdSterileItemsReceivedItemDetails = null;
            var x = CssdSterileItemsReceivedItemDetails;

            //Remove session
            Session.Remove("CssdRequestItemSelected" + Request.UserHostName);
        }

        private void AjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            PopulateFromSelectedRequest();
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new CssdSterileItemsReceived());

            txtReceivedNo.Text = GetNewReceivedNo();

            if (!string.IsNullOrEmpty(Request.QueryString["rn"]))
            {
                txtProductionNo.Text = Request.QueryString["rn"];
                PopulateGridDetailFromRequestNo();
            }

            txtReceivedDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtReceivedTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");
            if (string.IsNullOrEmpty(txtProductionNo.Text))
            {
                cboSenderByID.SelectedValue = string.Empty;
                cboSenderByID.Text = string.Empty;
            }

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuEditClick()
        {
            if (!string.IsNullOrEmpty(txtProductionNo.Text))
            {
                cboFromServiceUnitID.Enabled = false;
                cboFromRoomID.Enabled = false;
                cboSenderByID.Enabled = false;
                txtSenderBy.ReadOnly = true;
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            //var entity = new CssdSterileItemsReceived();
            //if (entity.LoadByPrimaryKey(txtReceivedNo.Text))
            //{
            //    entity.MarkAsDeleted();

            //    SaveEntity(entity);
            //}
            //else
            //    args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
            {
                args.MessageText = "From Unit required.";
                args.IsCancel = true;
                return;
            }

            if (CssdSterileItemsReceivedItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new CssdSterileItemsReceived();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
            {
                args.MessageText = "From Unit required.";
                args.IsCancel = true;
                return;
            }

            if (CssdSterileItemsReceivedItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new CssdSterileItemsReceived();
            if (entity.LoadByPrimaryKey(txtReceivedNo.Text))
            {
                SetEntityValue(entity);
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
            auditLogFilter.PrimaryKeyData = string.Format("ReceivedNo='{0}'", txtReceivedNo.Text.Trim());
            auditLogFilter.TableName = "CssdSterileItemsReceived";
        }

        #endregion

        #region ToolBar Menu Support
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new CssdSterileItemsReceived();
            if (entity.LoadByPrimaryKey(txtReceivedNo.Text))
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

        private bool IsApprovedOrVoid(CssdSterileItemsReceived entity, ValidateArgs args)
        {
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_ReceivedNo", txtReceivedNo.Text);
            printJobParameters.AddNew("p_UserID", AppSession.UserLogin.UserID);
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return ViewState["IsApproved"] == null ? false : !(bool)ViewState["IsApproved"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new CssdSterileItemsReceived();
            if (parameters.Length > 0)
            {
                var receivedNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(receivedNo);
            }
            else
                entity.LoadByPrimaryKey(txtReceivedNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var received = (CssdSterileItemsReceived)entity;

            txtReceivedNo.Text = received.ReceivedNo;
            txtReceivedDate.SelectedDate = received.ReceivedDate;
            txtReceivedTime.Text = received.ReceivedTime;
            if (!string.IsNullOrEmpty(received.FromServiceUnitID))
            {
                var q = new ServiceUnitQuery();
                q.Where(q.ServiceUnitID == received.FromServiceUnitID);
                cboFromServiceUnitID.DataSource = q.LoadDataTable();
                cboFromServiceUnitID.DataBind();
                cboFromServiceUnitID.SelectedValue = received.FromServiceUnitID;

                PopulateRoomList(received.FromServiceUnitID, false);
                cboFromRoomID.SelectedValue = received.FromRoomID;
            }
            else
            {
                cboFromServiceUnitID.Items.Clear();
                cboFromServiceUnitID.Text = string.Empty;
                cboFromRoomID.Items.Clear();
                cboFromRoomID.Text = string.Empty;
            }

            cboSenderByID.SelectedValue = received.SenderByID;
            txtSenderBy.Text = received.SenderBy;

            if (!string.IsNullOrEmpty(received.ReceivedByUserID))
            {
                var usr = new AppUserQuery();
                usr.Where(usr.UserID == received.ReceivedByUserID);
                cboReceivedByUserID.DataSource = usr.LoadDataTable();
                cboReceivedByUserID.DataBind();
                cboReceivedByUserID.SelectedValue = received.ReceivedByUserID;
            }
            else
            {
                var usr = new AppUserQuery();
                usr.Where(usr.UserID == AppSession.UserLogin.UserID);
                cboReceivedByUserID.DataSource = usr.LoadDataTable();
                cboReceivedByUserID.DataBind();
                cboReceivedByUserID.SelectedValue = AppSession.UserLogin.UserID;
            }
            
            chkIsFromProductionOfGoods.Checked = received.IsFromProductionOfGoods ?? false;
            txtProductionNo.Text = received.ProductionNo;
            if (!string.IsNullOrEmpty(received.SRInstrumentType))
                rblSRInstrumentType.SelectedValue = received.SRInstrumentType;

            PopulateItemGrid();

            ViewState["IsApproved"] = received.IsApproved ?? false;
            ViewState["IsVoid"] = received.IsVoid ?? false;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new CssdSterileItemsReceived();
            if (!entity.LoadByPrimaryKey(txtReceivedNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return;
            }

            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            if (AppSession.Parameter.IsCssdExpiredValidateInReceiveDetail && !AppSession.Application.IsMenuCssdPackagingActive)
            {
                var valEd = string.Empty;
                foreach (var i in CssdSterileItemsReceivedItems)
                {
                    if (i.ExpiredDate == null)
                    {
                        if (valEd == string.Empty)
                            valEd = i.ItemName;
                        else
                            valEd += ", " + i.ItemName;

                    }
                }

                if (valEd != string.Empty)
                {
                    args.MessageText = "Expired Date for item : " + valEd + " not set up yet.";
                    args.IsCancel = true;
                    return;
                }
            }

            SetApproval(entity, true, args);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new CssdSterileItemsReceived();
            if (!entity.LoadByPrimaryKey(txtReceivedNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            if (entity.IsApproved == false)
            {
                args.MessageText = AppConstant.Message.RecordHasNotApproved;
                args.IsCancel = true;
                return;
            }

            if (entity.IsFromProductionOfGoods == true)
            {
                args.MessageText = "Data is the result of automatic transactions from the production of goods.";
                args.IsCancel = true;
                return;
            }

            if (AppSession.Application.IsMenuCssdDecontaminationActive)
            {
                DataTable dtb = (new CssdDecontaminationItemCollection()).GetItemProceed(txtReceivedNo.Text, string.Empty, "1");
                if (dtb.Rows.Count > 0)
                {
                    var msg = string.Empty;

                    foreach (DataRow row in dtb.Rows)
                    {
                        if (msg == string.Empty)
                            msg =  row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + " (" + row["DecontaminationNo"].ToString() + ")";
                        else
                            msg = msg + "; " + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + " (" + row["DecontaminationNo"].ToString() + ")";
                    }

                    if (msg != string.Empty)
                    {
                        args.MessageText = "This transaction has entered into the next phase (Decontamination - Immersion). Unapproval is not allowed. [" + msg + "]";
                        args.IsCancel = true;
                        return;
                    }
                }
            }
            else if (AppSession.Application.IsMenuCssdFeasibilityTestActive)
            {
                DataTable dtb = (new CssdFeasibilityTestItemCollection()).GetItemProceed(txtReceivedNo.Text, string.Empty);
                if (dtb.Rows.Count > 0)
                {
                    var msg = string.Empty;

                    foreach (DataRow row in dtb.Rows)
                    {
                        if (msg == string.Empty)
                            msg = row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + " (" + row["FeasibilityTestNo"].ToString() + ")";
                        else
                            msg = msg + "; " + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + " (" + row["FeasibilityTestNo"].ToString() + ")";
                    }

                    if (msg != string.Empty)
                    {
                        args.MessageText = "This transaction has entered into the next phase (Feasibility Test). Unapproval is not allowed. [" + msg + "]";
                        args.IsCancel = true;
                        return;
                    }
                }
            }
            else if (AppSession.Application.IsMenuCssdPackagingActive)
            {
                DataTable dtb = (new CssdPackagingItemCollection()).GetItemProceed(txtReceivedNo.Text, string.Empty);
                if (dtb.Rows.Count > 0)
                {
                    var msg = string.Empty;

                    foreach (DataRow row in dtb.Rows)
                    {
                        if (msg == string.Empty)
                            msg = row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + " (" + row["TransactionNo"].ToString() + ")";
                        else
                            msg = msg + "; " + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + " (" + row["TransactionNo"].ToString() + ")";
                    }

                    if (msg != string.Empty)
                    {
                        args.MessageText = "This transaction has entered into the next phase (Packaging). Unapproval is not allowed. [" + msg + "]";
                        args.IsCancel = true;
                        return;
                    }
                }
            }
            else
            {
                DataTable dtb = (new CssdSterileItemsUltrasoundItemCollection()).GetItemProceed(txtReceivedNo.Text, string.Empty);
                if (dtb.Rows.Count > 0)
                {
                    var msg = string.Empty;

                    foreach (DataRow row in dtb.Rows)
                    {
                        if (msg == string.Empty)
                            msg = row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + " (" + row["TransactionNo"].ToString() + ")";
                        else
                            msg = msg + "; " + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + " (" + row["TransactionNo"].ToString() + ")";
                    }

                    if (msg != string.Empty)
                    {
                        args.MessageText = "This transaction has entered into the next phase (Ultrasound). Unapproval is not allowed. [" + msg + "]";
                        args.IsCancel = true;
                        return;
                    }
                }

                DataTable dtb2 = (new CssdSterilizationProcessItemCollection()).GetItemProceed(txtReceivedNo.Text, string.Empty);
                if (dtb2.Rows.Count > 0)
                {
                    var msg = string.Empty;
                    
                    foreach (DataRow row in dtb.Rows)
                    {
                        if (msg == string.Empty)
                            msg = row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + " (" + row["ProcessNo"].ToString() + ")";
                        else
                            msg = msg + "; " + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + " (" + row["ProcessNo"].ToString() + ")";
                    }

                    if (msg != string.Empty)
                    {
                        args.MessageText = "This transaction has entered into the next phase (Sterilization). Unapproval is not allowed. [" + msg + "]";
                        args.IsCancel = true;
                        return;
                    }
                }
            }

            SetApproval(entity, false, args);
        }

        private void SetApproval(CssdSterileItemsReceived entity, bool isApproval, ValidateArgs args)
        {
            using (var trans = new esTransactionScope())
            {
                entity.IsApproved = isApproval;

                if (isApproval)
                {
                    entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                    entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                }
                else
                {
                    entity.ApprovedByUserID = null;
                    entity.ApprovedDateTime = null;
                }

                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.Save();

                if (isApproval && !AppSession.Application.IsMenuCssdPackagingActive)
                {
                    if (!AppSession.Application.IsMenuCssdPackagingActive)
                    {
                        foreach (var item in CssdSterileItemsReceivedItems)
                        {
                            if (string.IsNullOrEmpty(item.CssdItemNo))
                            {
                                item.CssdItemNo = GetNewCssdItemNo();
                                // save autonumber immediately to decrease time gap between create and save
                                _autoItemNumber.Save();
                            }

                            item.SRCssdPhase = "1";
                        }
                    }
                    else
                    {
                        foreach (var item in CssdSterileItemsReceivedItems)
                        {
                            item.SRCssdPhase = "1";
                        }
                    }

                    CssdSterileItemsReceivedItems.Save();
                }

                var balancesFrom = new CssdItemBalanceCollection();
                var balancesTo = new CssdItemBalanceCollection();

                CssdItemBalance.PrepareItemBalancesReceive(CssdSterileItemsReceivedItems, entity.FromServiceUnitID, AppSession.Parameter.ServiceUnitCssdID, AppSession.UserLogin.UserID, AppSession.Parameter.IsCentralizedCssd, isApproval, ref balancesFrom, ref balancesTo);
                
                if (balancesFrom != null) 
                    balancesFrom.Save();
                if (balancesTo != null)
                    balancesTo.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new CssdSterileItemsReceived();
            if (!entity.LoadByPrimaryKey(txtReceivedNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, true);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            var entity = new CssdSterileItemsReceived();
            if (!entity.LoadByPrimaryKey(txtReceivedNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private void SetVoid(CssdSterileItemsReceived entity, bool isVoid)
        {
            //header
            entity.IsVoid = isVoid;
            if (isVoid)
            {
                entity.VoidByUserID = AppSession.UserLogin.UserID;
                entity.VoidDateTime = (new DateTime()).NowAtSqlServer();
            }
            else
            {
                entity.VoidByUserID = null;
                entity.VoidDateTime = null;
            }
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.Save();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(CssdSterileItemsReceived entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtReceivedNo.Text = GetNewReceivedNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }
            entity.ReceivedNo = txtReceivedNo.Text;
            entity.ReceivedDate = txtReceivedDate.SelectedDate;
            entity.ReceivedTime = txtReceivedTime.TextWithLiterals;
            entity.FromServiceUnitID = cboFromServiceUnitID.SelectedValue;
            entity.FromRoomID = cboFromRoomID.SelectedValue;
            entity.SenderByID = cboSenderByID.SelectedValue;
            entity.SenderBy = txtSenderBy.Text;
            entity.ReceivedByUserID = cboReceivedByUserID.SelectedValue;
            entity.IsFromProductionOfGoods = chkIsFromProductionOfGoods.Checked;
            entity.ProductionNo = txtProductionNo.Text;
            if (AppSession.Parameter.IsCentralizedCssd)
                entity.SRInstrumentType = rblSRInstrumentType.SelectedValue;

            entity.IsApproved = false;
            entity.IsVoid = false;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (var item in CssdSterileItemsReceivedItems)
            {
                item.ReceivedNo = txtReceivedNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (var item in CssdSterileItemsReceivedItemDetails)
            {
                item.ReceivedNo = txtReceivedNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(CssdSterileItemsReceived entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                CssdSterileItemsReceivedItems.Save();
                CssdSterileItemsReceivedItemDetails.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new CssdSterileItemsReceivedQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.ReceivedNo > txtReceivedNo.Text
                    );
                que.OrderBy(que.ReceivedNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.ReceivedNo < txtReceivedNo.Text
                    );
                que.OrderBy(que.ReceivedNo.Descending);
            }

            var entity = new CssdSterileItemsReceived();
            if (entity.Load(que))
            {
                CssdSterileItemsReceivedItems = null;
                OnPopulateEntryControl(entity);
            }
        }

        #endregion

        #region Record Detail Method Function of SterileItemsReceived

        private CssdSterileItemsReceivedItemCollection CssdSterileItemsReceivedItems
        {
            get
            {
                object obj = Session["collCssdSterileItemsReceivedItem" + Request.UserHostName];
                if (obj != null)
                    return ((CssdSterileItemsReceivedItemCollection)(obj));

                var coll = new CssdSterileItemsReceivedItemCollection();
                var query = new CssdSterileItemsReceivedItemQuery("a");
                var iq = new ItemQuery("b");
                var unitq = new AppStandardReferenceItemQuery("c");

                query.Select
                    (
                        query,
                        @"<CAST(a.CssdItemNo AS VARCHAR) AS 'refTo_CssdItemNo'>",
                        iq.ItemName.As("refToCssdItem_ItemName"),
                        unitq.ItemName.As("refToAppStandardReferenceItem_CssdItemUnit"),
                        @"<CASE WHEN a.IsDtt = 0 THEN 'Low' ELSE 'High' END AS 'refTo_IsDttDescription'>"

                    );
                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
                query.InnerJoin(unitq).On(query.SRCssdItemUnit == unitq.ItemID &&
                                          unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                query.Where(query.ReceivedNo == txtReceivedNo.Text);
                query.OrderBy(query.ReceivedSeqNo.Ascending);
                coll.Load(query);

                Session["collCssdSterileItemsReceivedItem" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collCssdSterileItemsReceivedItem" + Request.UserHostName] = value;
            }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns.FindByUniqueName("listDetailEdit").Visible = isVisible;
            grdItem.Columns.FindByUniqueName("listDetailView").Visible = !isVisible;

            if (getPageID == "ver" && !string.IsNullOrEmpty(txtProductionNo.Text))
            {
                grdItem.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                grdItem.Columns[grdItem.Columns.Count - 1].Visible = false;
            }
            else
            {
                grdItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
                grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;
            }
            
            if (oldVal != AppEnum.DataMode.Read)
            {
                CssdSterileItemsReceivedItems = null;
                CssdSterileItemsReceivedItemDetails = null;
            }
                
            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdItem.Rebind();
            
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            CssdSterileItemsReceivedItems = null; //Reset Record Detail
            grdItem.DataSource = CssdSterileItemsReceivedItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();

            CssdSterileItemsReceivedItemDetails = null;
            var det = CssdSterileItemsReceivedItemDetails;
        }

        private CssdSterileItemsReceivedItem FindItem(String seqNo)
        {
            CssdSterileItemsReceivedItemCollection coll = CssdSterileItemsReceivedItems;
            CssdSterileItemsReceivedItem retEntity = null;
            foreach (CssdSterileItemsReceivedItem rec in coll)
            {
                if (rec.ReceivedSeqNo.Equals(seqNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = CssdSterileItemsReceivedItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String seqNo =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][CssdSterileItemsReceivedItemMetadata.ColumnNames.ReceivedSeqNo]);
            CssdSterileItemsReceivedItem entity = FindItem(seqNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String seqNo =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][CssdSterileItemsReceivedItemMetadata.ColumnNames.ReceivedSeqNo]);
            CssdSterileItemsReceivedItem entity = FindItem(seqNo);
            if (entity != null)
            {
                foreach (var det in CssdSterileItemsReceivedItemDetails.Where(det => det.ReceivedSeqNo == seqNo))
                {
                    det.MarkAsDeleted();
                }
                entity.MarkAsDeleted();
            }
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            CssdSterileItemsReceivedItem entity = CssdSterileItemsReceivedItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(CssdSterileItemsReceivedItem entity, GridCommandEventArgs e)
        {
            var userControl = (SterileItemsReceivedItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ReceivedSeqNo = userControl.ReceivedSeqNo;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.Qty = userControl.Qty;
                entity.SRCssdItemUnit = userControl.SRCssdItemUnit;
                entity.CssdItemUnit = userControl.CssdItemUnitName;
                entity.Notes = userControl.Notes;
                entity.CssdItemNo = string.Empty;
                if (userControl.ExpiredDate == null)
                    entity.str.ExpiredDate = string.Empty;
                else
                    entity.ExpiredDate = userControl.ExpiredDate;
                entity.ReuseTo = userControl.ReuseTo;
                entity.IsNeedUltrasound = userControl.IsNeedUltrasound;
                entity.IsDtt = userControl.IsDtt;
                entity.IsItemProduction = userControl.IsItemProduction;
                entity.DttDescription = userControl.IsDtt ? "High" : "Low";

                var items = new CssdItemDetailCollection();
                items.Query.Where(items.Query.ItemID == entity.ItemID);
                items.LoadAll();

                if (userControl.IsNewRecord)
                {
                    foreach (var d in items)
                    {
                        var det = CssdSterileItemsReceivedItemDetails.AddNew();
                        det.ReceivedNo = txtReceivedNo.Text;
                        det.ReceivedSeqNo = entity.ReceivedSeqNo;
                        det.ItemID = entity.ItemID;
                        det.ItemDetailID = d.ItemDetailID;

                        var item = new Item();
                        if (item.LoadByPrimaryKey(det.ItemDetailID))
                            det.ItemName = item.ItemName;
                        else det.ItemName = string.Empty;

                        det.Qty = entity.Qty * d.Qty;
                        det.QtyReceived = det.Qty;

                        det.IsBrokenInstrument = false;
                        det.QtyReplacements = 0;

                        det.IsBrokenInstrumentX = false;
                        det.QtyReplacementsX = 0;

                        var itemDetQ = new VwItemProductMedicNonMedicQuery();
                        itemDetQ.Where(itemDetQ.ItemID == det.ItemDetailID);
                        var itemDet = new VwItemProductMedicNonMedic();
                        itemDet.Load(itemDetQ);

                        det.SRItemUnit = itemDet.SRItemUnit;
                    }
                }
                else
                {
                    foreach (var d in items)
                    {
                        try 
                        {
                            var details = (CssdSterileItemsReceivedItemDetails.Where(b => b.ReceivedNo == txtReceivedNo.Text && b.ReceivedSeqNo == entity.ReceivedSeqNo
                                && b.ItemID == entity.ItemID && b.ItemDetailID == d.ItemDetailID)).Take(1).Single();
                            
                            details.Qty = entity.Qty * d.Qty;
                            details.QtyReceived = entity.Qty * d.Qty;
                        }
                        catch
                        {
                            var det = CssdSterileItemsReceivedItemDetails.AddNew();
                            det.ReceivedNo = txtReceivedNo.Text;
                            det.ReceivedSeqNo = entity.ReceivedSeqNo;
                            det.ItemID = entity.ItemID;
                            det.ItemDetailID = d.ItemDetailID;

                            var item = new Item();
                            if (item.LoadByPrimaryKey(det.ItemDetailID))
                                det.ItemName = item.ItemName;
                            else det.ItemName = string.Empty;

                            det.Qty = entity.Qty * d.Qty;
                            det.QtyReceived = det.Qty;

                            det.IsBrokenInstrument = false;
                            det.QtyReplacements = 0;

                            det.IsBrokenInstrumentX = false;
                            det.QtyReplacementsX = 0;

                            var itemDetQ = new VwItemProductMedicNonMedicQuery();
                            itemDetQ.Where(itemDetQ.ItemID == det.ItemDetailID);
                            var itemDet = new VwItemProductMedicNonMedic();
                            itemDet.Load(itemDetQ);

                            det.SRItemUnit = itemDet.SRItemUnit;
                        }
                    }
                       
                }
            }
        }

        #endregion

        #region Record Detail Method Function of SterileItemsReceivedItemDetails
        private CssdSterileItemsReceivedItemDetailCollection CssdSterileItemsReceivedItemDetails
        {
            get
            {
                //if (IsPostBack)
                {
                    var obj = Session["collCssdSterileItemsReceivedItemDetail" + Request.UserHostName];
                    if (obj != null)
                        return ((CssdSterileItemsReceivedItemDetailCollection)(obj));
                }

                var coll = new CssdSterileItemsReceivedItemDetailCollection();
                var query = new CssdSterileItemsReceivedItemDetailQuery("a");
                var item = new ItemQuery("b");
                var itemDetail = new VwItemProductMedicNonMedicQuery("c");

                query.Select(
                    query,
                    @"<ISNULL(a.IsBrokenInstrument, 0) AS 'refTo_IsBrokenInstrument'>",
                    @"<ISNULL(a.QtyReplacements, 0) AS 'refTo_QtyReplacements'>",
                    item.ItemName.As("refToItem_ItemName"),
                    itemDetail.SRItemUnit.As("refToAppStdRef_ItemUnit")
                    );
                query.InnerJoin(item).On(query.ItemDetailID == item.ItemID);
                query.InnerJoin(itemDetail).On(query.ItemDetailID == itemDetail.ItemID);

                query.Where(query.ReceivedNo == txtReceivedNo.Text);

                coll.Load(query);

                Session["collCssdSterileItemsReceivedItemDetail" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collCssdSterileItemsReceivedItemDetail" + Request.UserHostName] = value; }
        }
        #endregion

        #region Combobox
        protected void cboFromServiceUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ServiceUnitQuery("a");
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            query.Where
                (
                    query.Or
                        (
                            query.ServiceUnitID == e.Text,
                            query.ServiceUnitName.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );

            query.es.Top = 20;
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            DataTable dtb = query.LoadDataTable();

            cboFromServiceUnitID.DataSource = dtb;
            cboFromServiceUnitID.DataBind();
        }

        protected void cboFromServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void cboFromServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
                PopulateRoomList(e.Value, true);
            else
            {
                cboFromRoomID.Items.Clear();
                cboFromRoomID.Text = string.Empty;
            }
        }

        private void PopulateRoomList(string unitId, bool isNew)
        {
            cboFromRoomID.Items.Clear();

            var sr = new ServiceRoomCollection();
            var srQ = new ServiceRoomQuery("a");

            srQ.Select(srQ.RoomID, srQ.RoomName);
            srQ.Where(srQ.ServiceUnitID == unitId);
            if (isNew)
                srQ.Where(srQ.IsActive == true);
            srQ.OrderBy(srQ.RoomID.Ascending);
            srQ.es.Distinct = true;

            sr.Load(srQ);

            cboFromRoomID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (ServiceRoom entity in sr)
            {
                cboFromRoomID.Items.Add(new RadComboBoxItem(entity.RoomName, entity.RoomID));
            }
        }

        protected void cboSenderByID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                if (e.Value != AppSession.Parameter.CssdSenderBySelf && e.Value != AppSession.Parameter.CssdSenderByOtherUnit)
                {
                    var std = new AppStandardReferenceItem();
                    txtSenderBy.Text = std.LoadByPrimaryKey(AppEnum.StandardReference.CssdSender.ToString(), e.Value) ? std.ItemName : string.Empty;
                }
                else 
                    txtSenderBy.Text = AppSession.UserLogin.UserName;
            }
            else
                txtSenderBy.Text = string.Empty;
        }

        protected void cboReceivedByUserID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.UserByUnitItemRequested((RadComboBox)sender, AppSession.Parameter.ServiceUnitCssdID, e.Text);
        }

        protected void cboReceivedByUserID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.UserItemDataBound(e);
        }
        #endregion

        private string GetNewReceivedNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.CssdReceivedNo);

            return _autoNumber.LastCompleteNumber;
        }

        private string GetNewCssdItemNo()
        {
            _autoItemNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.CssdItemNo);

            return _autoItemNumber.LastCompleteNumber;
        }
    }
}
