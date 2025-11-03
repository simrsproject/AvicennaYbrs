using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.AssetManagement
{
    public partial class WorkOrderDetail : BasePageDetail
    {
        private string getPageID
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize
        
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "WorkOrderSearch.aspx?type=" + getPageID;
            UrlPageList = "WorkOrderList.aspx?type=" + getPageID;

            ProgramID = getPageID == "" ? AppConstant.Program.AssetWorkOrder : AppConstant.Program.SanitationActivityWorkOrder;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, TransactionCode.AssetWorkOrder, true);
                if (getPageID == "")
                {
                    if (AppSession.Parameter.IsUsingSingleUnitIPSRS)
                        ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, TransactionCode.AssetWorkOrderRealization, false);
                    else
                        ComboBox.PopulateWithServiceUnitForTransactionWithException(cboToServiceUnitID, TransactionCode.AssetWorkOrderRealization, false, string.Empty, AppSession.Parameter.ServiceUnitSanitationId, getPageID != "");
                }
                else 
                    ComboBox.PopulateWithServiceUnitForTransactionWithException(cboToServiceUnitID, TransactionCode.AssetWorkOrderRealization, false, string.Empty, AppSession.Parameter.ServiceUnitSanitationId, getPageID != "");
                StandardReference.InitializeIncludeSpace(cboSRWorkPriority, AppEnum.StandardReference.WorkPriority);
                StandardReference.InitializeIncludeSpace(cboSRAssetsStatus, AppEnum.StandardReference.AssetsStatus);
                StandardReference.InitializeIncludeSpace(cboSRAssetsWarrantyContract, AppEnum.StandardReference.AssetsWarrantyContract);
                StandardReference.InitializeIncludeSpace(cboSRFailureCode, AppEnum.StandardReference.FailureCode);

                if (getPageID == "")
                {
                    string[] exc = AppSession.Parameter.WorkTradeSanitation.Split(',');
                    StandardReference.InitializeIncludeSpace(cboSRWorkTrade, AppEnum.StandardReference.WorkTrade, exc, false);
                }
                else
                {
                    StandardReference.InitializeIncludeSpace(cboSRWorkTrade, AppEnum.StandardReference.WorkTrade);

                    pnlAssetInfo.Visible = false;
                    tabStrip.Tabs[2].Visible = false;
                    trCostEstimation.Visible = false;
                }
            }

            if (!IsCallback)
                PopUpSearch.InitializeAssetByServiceUnit(Page, txtAssetID, cboFromServiceUnitID, cboToServiceUnitID);
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboFromServiceUnitID);
            ajax.AddAjaxSetting(cboFromServiceUnitID, txtOrderNo);
           
            if (getPageID == "")
            {
                ajax.AddAjaxSetting(cboFromServiceUnitID, txtAssetID);
                ajax.AddAjaxSetting(cboFromServiceUnitID, lblAssetName);
                ajax.AddAjaxSetting(cboFromServiceUnitID, txtBrandName);
                ajax.AddAjaxSetting(cboFromServiceUnitID, txtSerialNo);
                ajax.AddAjaxSetting(cboFromServiceUnitID, txtLocationName);
                ajax.AddAjaxSetting(cboFromServiceUnitID, cboSRAssetsStatus);
                ajax.AddAjaxSetting(cboFromServiceUnitID, txtAssetNotes);
                ajax.AddAjaxSetting(cboFromServiceUnitID, txtNotesToTechnician);
                ajax.AddAjaxSetting(cboFromServiceUnitID, cboSRAssetsWarrantyContract);
                ajax.AddAjaxSetting(cboFromServiceUnitID, txtGuaranteeExpiredDate);

                ajax.AddAjaxSetting(cboToServiceUnitID, cboToServiceUnitID);
                ajax.AddAjaxSetting(cboToServiceUnitID, txtAssetID);
                ajax.AddAjaxSetting(cboToServiceUnitID, lblAssetName);
                ajax.AddAjaxSetting(cboToServiceUnitID, txtBrandName);
                ajax.AddAjaxSetting(cboToServiceUnitID, txtSerialNo);
                ajax.AddAjaxSetting(cboToServiceUnitID, txtLocationName);
                ajax.AddAjaxSetting(cboToServiceUnitID, cboSRAssetsStatus);
                ajax.AddAjaxSetting(cboToServiceUnitID, txtAssetNotes);
                ajax.AddAjaxSetting(cboToServiceUnitID, txtNotesToTechnician);
                ajax.AddAjaxSetting(cboToServiceUnitID, cboSRAssetsWarrantyContract);
                ajax.AddAjaxSetting(cboToServiceUnitID, txtGuaranteeExpiredDate);

                ajax.AddAjaxSetting(txtAssetID, txtAssetID);
                ajax.AddAjaxSetting(txtAssetID, lblAssetName);
                ajax.AddAjaxSetting(txtAssetID, txtBrandName);
                ajax.AddAjaxSetting(txtAssetID, txtSerialNo);
                ajax.AddAjaxSetting(txtAssetID, txtLocationName);
                ajax.AddAjaxSetting(txtAssetID, cboSRAssetsStatus);
                ajax.AddAjaxSetting(txtAssetID, txtAssetNotes);
                ajax.AddAjaxSetting(txtAssetID, txtNotesToTechnician);
                ajax.AddAjaxSetting(txtAssetID, cboSRAssetsWarrantyContract);
                ajax.AddAjaxSetting(txtAssetID, txtGuaranteeExpiredDate);
                ajax.AddAjaxSetting(txtAssetID, cboItemID);

                ajax.AddAjaxSetting(grdItem, grdItem);
            }
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuEditClick()
        {
            cboFromServiceUnitID.Enabled = !chkIsPreventiveMaintenance.Checked;
            cboToServiceUnitID.Enabled = !chkIsPreventiveMaintenance.Checked;
            txtRequiredDate.Enabled = !chkIsPreventiveMaintenance.Checked;
            txtAssetID.Enabled = chkIsPreventiveMaintenance.Checked;
            cboSRWorkType.Enabled = !chkIsPreventiveMaintenance.Checked;
            cboSRWorkTrade.Enabled = getPageID == "";
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new AssetWorkOrder();
            if (entity.LoadByPrimaryKey(txtOrderNo.Text))
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

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new AssetWorkOrder();
            entity.LoadByPrimaryKey(txtOrderNo.Text);

            entity.IsApproved = true;
            entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                trans.Complete();
            } 
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new AssetWorkOrder();
            entity.LoadByPrimaryKey(txtOrderNo.Text);

            if (!string.IsNullOrEmpty(entity.FirstResponseByUserID))
            {
                args.MessageText = "Work Order already responded by maintenance unit.";
                args.IsCancel = true;
                return;
            }

            entity.IsApproved = false;
            entity.ApprovedDateTime = null;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                trans.Complete();
            } 
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            OnVoid(true);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            OnVoid(false);
        }

        private void OnVoid(bool isVoid)
        {
            var entity = new AssetWorkOrder();
            entity.LoadByPrimaryKey(txtOrderNo.Text);

            entity.IsVoid = isVoid;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                trans.Complete();
            }
        }

        private bool IsApprovedOrVoid(AssetWorkOrder entity, ValidateArgs args)
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
            OnPopulateEntryControl(new AssetWorkOrder());

            txtOrderDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            cboFromServiceUnitID.Text = string.Empty;
            cboToServiceUnitID.Text = string.Empty;
            txtRequiredDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            cboSRWorkPriority.SelectedValue = AppSession.Parameter.WorkPriorityNormal;
            txtQty.Value = 1;

            cboSRWorkType.Items.Clear();
            var query = new AppStandardReferenceItemQuery("a");
            query.Where
                (query.StandardReferenceID == AppEnum.StandardReference.WorkType.ToString(),
                 query.IsActive == true, query.IsUsedBySystem == true);
            query.OrderBy(query.ItemID.Ascending);
            DataTable dtb = query.LoadDataTable();
            if (dtb.Rows.Count == 1)
            {
                cboSRWorkType.Items.Add(new RadComboBoxItem(dtb.Rows[0]["ItemName"].ToString(), dtb.Rows[0]["ItemID"].ToString()));
                cboSRWorkType.SelectedValue = dtb.Rows[0]["ItemID"].ToString();
            }

            if (getPageID != "")
            {
                if (!string.IsNullOrEmpty(AppSession.Parameter.ServiceUnitSanitationId))
                {
                    cboToServiceUnitID.SelectedValue = AppSession.Parameter.ServiceUnitSanitationId.ToString();
                    cboToServiceUnitID.Enabled = false;
                }

                cboSRWorkTrade.Enabled = false;
                cboSRWorkTrade.SelectedValue = AppSession.Parameter.WorkTradeSanitation;
            }
            else
            {
                cboSRWorkTrade.SelectedValue = string.Empty;
                cboSRWorkTrade.Text = string.Empty;
            }

            ComboBox.PopulateWorkTradeItemList(cboSRWorkTradeItem, cboSRWorkTrade.SelectedValue, true);
            cboSRWorkTradeItem.SelectedValue = string.Empty;
            cboSRWorkTradeItem.Text = string.Empty;

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (getPageID == "" && cboSRWorkType.SelectedValue != AppSession.Parameter.WorkTypeProject)
            {
                if (string.IsNullOrEmpty(txtAssetID.Text))
                {
                    args.MessageText = "Asset required.";
                    args.IsCancel = true;
                    return;
                }
                var asset = new Asset();
                if (!asset.LoadByPrimaryKey(txtAssetID.Text))
                {
                    args.MessageText = "Invalid Asset.";
                    args.IsCancel = true;
                    return;
                }
            }
            if (cboSRWorkType.SelectedValue == AppSession.Parameter.WorkTypeProject)
            {
                if (string.IsNullOrEmpty(cboItemID.SelectedValue))
                {
                    args.MessageText = "Item required.";
                    args.IsCancel = true;
                    return;
                }
            }
            if (txtQty.Value < 1)
            {
                args.MessageText = "Qty required.";
                args.IsCancel = true;
                return;
            }

            PopulateNewNumber();
            _autoNumber.Save();

            var entity = new AssetWorkOrder();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (getPageID == "" && cboSRWorkType.SelectedValue != AppSession.Parameter.WorkTypeProject)
            {
                if (string.IsNullOrEmpty(txtAssetID.Text))
                {
                    args.MessageText = "Asset required.";
                    args.IsCancel = true;
                    return;
                }
                var asset = new Asset();
                if (!asset.LoadByPrimaryKey(txtAssetID.Text))
                {
                    args.MessageText = "Invalid Asset.";
                    args.IsCancel = true;
                    return;
                }
            }
            if (cboSRWorkType.SelectedValue == AppSession.Parameter.WorkTypeProject)
            {
                if (string.IsNullOrEmpty(cboItemID.SelectedValue))
                {
                    args.MessageText = "Item required.";
                    args.IsCancel = true;
                    return;
                }
            }
            if (txtQty.Value < 1)
            {
                args.MessageText = "Qty required.";
                args.IsCancel = true;
                return;
            }

            var entity = new AssetWorkOrder();
            if (entity.LoadByPrimaryKey(txtOrderNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_OrderNo", txtOrderNo.Text);
        }
        
        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("OrderNo='{0}'", txtOrderNo.Text.Trim());
            auditLogFilter.TableName = "AssetWorkOrder";
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuEdit()
        {
            return txtOrderNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !(bool)ViewState["IsApproved"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new AssetWorkOrder();
            if (parameters.Length > 0)
            {
                String orderNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(orderNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtOrderNo.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var wo = (AssetWorkOrder)entity;
            txtOrderNo.Text = wo.OrderNo;
            txtOrderDate.SelectedDate = wo.OrderDate;
            cboFromServiceUnitID.SelectedValue = wo.FromServiceUnitID;
            cboToServiceUnitID.SelectedValue = wo.ToServiceUnitID;
            txtRequiredDate.SelectedDate = wo.RequiredDate;
            if (!string.IsNullOrEmpty(wo.SRWorkStatus))
            {
                var ws = new AppStandardReferenceItemQuery();
                ws.Where(ws.StandardReferenceID == AppEnum.StandardReference.WorkStatus.ToString(),
                         ws.ItemID == wo.SRWorkStatus);
                cboSRWorkStatus.DataSource = ws.LoadDataTable();
                cboSRWorkStatus.DataBind();

                cboSRWorkStatus.SelectedValue = wo.SRWorkStatus;
            }
            else
            {
                var ws = new AppStandardReferenceItemQuery();
                ws.Where(ws.StandardReferenceID == AppEnum.StandardReference.WorkStatus.ToString(),
                         ws.ItemID == AppSession.Parameter.WorkStatusOpen);
                cboSRWorkStatus.DataSource = ws.LoadDataTable();
                cboSRWorkStatus.DataBind();

                cboSRWorkStatus.SelectedValue = AppSession.Parameter.WorkStatusOpen;
                var wsts = new AppStandardReferenceItem();
                if (wsts.LoadByPrimaryKey(AppEnum.StandardReference.WorkStatus.ToString(), cboSRWorkStatus.SelectedValue))
                    cboSRWorkStatus.Text = wsts.ItemName;
                else
                    cboSRWorkStatus.Text = "Open*";
            }
            if (!string.IsNullOrEmpty(wo.SRWorkType))
            {
                var ws = new AppStandardReferenceItemQuery();
                ws.Where(ws.StandardReferenceID == AppEnum.StandardReference.WorkType.ToString(),
                         ws.ItemID == wo.SRWorkType);
                cboSRWorkType.DataSource = ws.LoadDataTable();
                cboSRWorkType.DataBind();

                cboSRWorkType.SelectedValue = wo.SRWorkType;
            }
            else
            {
                cboSRWorkType.Items.Clear();
                cboSRWorkType.Text = string.Empty;
            }
            cboSRWorkPriority.SelectedValue = wo.SRWorkPriority;
            cboSRWorkTrade.SelectedValue = wo.SRWorkTrade;

            ComboBox.PopulateWorkTradeItemList(cboSRWorkTradeItem, cboSRWorkTrade.SelectedValue, false);
            cboSRWorkTradeItem.SelectedValue = wo.SRWorkTradeItem;

            txtProblemDescription.Text = wo.ProblemDescription;
            chkIsPreventiveMaintenance.Checked = wo.IsPreventiveMaintenance ?? false;
            txtPMNo.Text = wo.PMNo;
            
            string userId = !string.IsNullOrEmpty(wo.RequestByUserID) ? wo.RequestByUserID : AppSession.UserLogin.UserID;
            var usr = new AppUser();
            usr.LoadByPrimaryKey(userId);
            txtRequestByUserID.Text = usr.UserName;

            txtAssetID.Text = wo.AssetID;
            if (!string.IsNullOrEmpty(wo.ItemID))
            {
                var iq = new ItemQuery();
                iq.Where(iq.ItemID == wo.ItemID);
                cboItemID.DataSource = iq.LoadDataTable();
                cboItemID.DataBind();
                cboItemID.SelectedValue = wo.ItemID;
            }
            else
            {
                cboItemID.Items.Clear();
                cboItemID.Text = string.Empty;
            }
            txtQty.Value = Convert.ToDouble(wo.Qty);
            PopulateAssetInfo(txtAssetID.Text, false);

            if (!string.IsNullOrEmpty(wo.ReceivedByUserID))
            {
                txtReceivedDate.SelectedDate = wo.ReceivedDateTime.Value.Date;
                txtReceivedTime.Text = wo.ReceivedDateTime.Value.ToString("HH:mm");
                txtReceivedByUserID.Text = wo.ReceivedByUserID;
                usr = new AppUser();
                usr.LoadByPrimaryKey(wo.ReceivedByUserID);
                txtReceivedBy.Text = usr.UserName;
            }
            else
            {
                txtReceivedDate.SelectedDate = null;
                txtReceivedTime.Text = "00:00";
                txtReceivedByUserID.Text = string.Empty;
                txtReceivedBy.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(wo.FirstResponseByUserID))
            {
                txtFirstResponseDate.SelectedDate = wo.FirstResponseDateTime.Value.Date;
                txtFirstResponseTime.Text = wo.FirstResponseDateTime.Value.ToString("HH:mm");
                txtFirstResponseByUserID.Text = wo.FirstResponseByUserID;
                usr = new AppUser();
                usr.LoadByPrimaryKey(wo.FirstResponseByUserID);
                txtFirstResponseByUserName.Text = usr.UserName;
            }
            else
            {
                txtFirstResponseDate.SelectedDate = null;
                txtFirstResponseTime.Text = "00:00";
                txtFirstResponseByUserID.Text = string.Empty;
                txtFirstResponseByUserName.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(wo.LastRealizationByUserID))
            {
                txtLastRealizationDate.SelectedDate = wo.LastRealizationDateTime.Value.Date;
                txtLastRealizationTime.Text = wo.LastRealizationDateTime.Value.ToString("HH:mm");
                txtLastRealizationByUserID.Text = wo.LastRealizationByUserID;
                usr = new AppUser();
                usr.LoadByPrimaryKey(wo.LastRealizationByUserID);
                txtLastRealizationBy.Text = usr.UserName;
            }
            else
            {
                txtLastRealizationDate.SelectedDate = null;
                txtLastRealizationTime.Text = "00:00";
                txtLastRealizationByUserID.Text = string.Empty;
                txtLastRealizationBy.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(wo.AcceptedByUserID))
            {
                txtAcceptedDate.SelectedDate = wo.AcceptedDateTime.Value.Date;
                txtAcceptedTime.Text = wo.AcceptedDateTime.Value.ToString("HH:mm");
                usr = new AppUser();
                usr.LoadByPrimaryKey(wo.AcceptedByUserID);
                txtAcceptedByUserID.Text = usr.UserName;
            }
            else
            {
                txtAcceptedDate.SelectedDate = null;
                txtAcceptedTime.Text = "00:00";
                txtAcceptedByUserID.Text = string.Empty;
            }

            cboSRFailureCode.SelectedValue = wo.SRFailureCode;
            txtFailureCauseDescription.Text = wo.FailureCauseDescription;
            txtActionTaken.Text = wo.ActionTaken;
            txtPreventionTaken.Text = wo.PreventionTaken;
            txtCostEstimation.Value = Convert.ToDouble(wo.CostEstimation);

            if (wo.IsApproved ?? false)
            {
                txtAppovedDate.SelectedDate = wo.ApprovedDateTime.Value.Date;
                txtApprovedTime.Text = wo.ApprovedDateTime.Value.ToString("HH:mm");
            }
            else
            {
                txtAppovedDate.SelectedDate = null;
                txtApprovedTime.Text = "00:00";
            }

            txtAcceptedBy.Text = wo.AcceptedBy;

            //Display Data Detail
            PopulateGridDetail();
            PopulateGridImplementerDetail();

            ViewState["IsApproved"] = wo.IsApproved ?? false;
            ViewState["IsVoid"] = wo.IsVoid ?? false;
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(AssetWorkOrder entity)
        {
            entity.OrderNo = txtOrderNo.Text;
            entity.OrderDate = txtOrderDate.SelectedDate;
            entity.FromServiceUnitID = cboFromServiceUnitID.SelectedValue;
            entity.ToServiceUnitID = cboToServiceUnitID.SelectedValue;
            entity.AssetID = txtAssetID.Text;
            entity.ItemID = cboItemID.SelectedValue;
            entity.Qty = Convert.ToDecimal(txtQty.Value);
            entity.ProblemDescription = txtProblemDescription.Text;
            entity.SRWorkStatus = cboSRWorkStatus.SelectedValue;
            entity.SRWorkType = cboSRWorkType.SelectedValue;
            entity.SRWorkPriority = cboSRWorkPriority.SelectedValue;
            entity.SRWorkTrade = cboSRWorkTrade.SelectedValue;
            entity.SRWorkTradeItem = cboSRWorkTradeItem.SelectedValue;
            entity.RequiredDate = txtRequiredDate.SelectedDate;
            entity.RequestByUserID = AppSession.UserLogin.UserID;
            entity.IsVoid = false;
            entity.IsApproved = false;
            entity.IsProceed = false;
            entity.IsPreventiveMaintenance = chkIsPreventiveMaintenance.Checked;
            entity.PMNo = txtPMNo.Text;
            entity.ReferenceNo = string.Empty;
            entity.IsSanitation = getPageID != "";

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(AssetWorkOrder entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                ////AutoNumberLast
                //if (DataModeCurrent == DataMode.New)
                //    _autoNumber.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new AssetWorkOrderQuery("a");
            var user = new AppUserServiceUnitQuery("b");
            que.InnerJoin(user).On(que.FromServiceUnitID == user.ServiceUnitID && user.UserID == AppSession.UserLogin.UserID);

            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.OrderNo > txtOrderNo.Text);
                que.OrderBy(que.OrderNo.Ascending);
            }
            else
            {
                que.Where(que.OrderNo < txtOrderNo.Text);
                que.OrderBy(que.OrderNo.Descending);
            }
            if (getPageID == "")
                que.Where(que.IsSanitation == false);
            else
                que.Where(que.IsSanitation == true);

            var entity = new AssetWorkOrder();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        protected void cboFromServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateNewNumber();
            txtAssetID.Text = string.Empty;
            lblAssetName.Text = string.Empty;
            txtBrandName.Text = string.Empty;
            txtSerialNo.Text = string.Empty;
            txtLocationName.Text = string.Empty;
            cboSRAssetsStatus.SelectedValue = string.Empty;
            txtAssetNotes.Text = string.Empty;
            txtNotesToTechnician.Text = string.Empty;
            cboSRAssetsWarrantyContract.SelectedValue = string.Empty;
            txtGuaranteeExpiredDate.SelectedDate = null;
            lblAssetName.Text = string.Empty;

            txtLocationName.Text = string.Empty;
            cboSRAssetsStatus.SelectedValue = string.Empty;
            txtNotesToTechnician.Text = string.Empty;
            cboSRAssetsWarrantyContract.SelectedValue = string.Empty;
            txtGuaranteeExpiredDate.SelectedDate = null;
        }

        protected void cboToServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txtAssetID.Text = string.Empty;
            lblAssetName.Text = string.Empty;
            txtBrandName.Text = string.Empty;
            txtSerialNo.Text = string.Empty;
            txtLocationName.Text = string.Empty;
            cboSRAssetsStatus.SelectedValue = string.Empty;
            txtAssetNotes.Text = string.Empty;
            txtNotesToTechnician.Text = string.Empty;
            cboSRAssetsWarrantyContract.SelectedValue = string.Empty;
            txtGuaranteeExpiredDate.SelectedDate = null;
            lblAssetName.Text = string.Empty;
        }

        protected void cboSRWorkTrade_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ComboBox.PopulateWorkTradeItemList(cboSRWorkTradeItem, e.Value, true);
        }

        private void PopulateNewNumber()
        {
            txtOrderNo.Text = string.Empty;

            if (DataModeCurrent != AppEnum.DataMode.New)
                return;

            if (cboFromServiceUnitID.SelectedValue == string.Empty)
                return;

            var serv = new ServiceUnit();
            if (serv.LoadByPrimaryKey(cboFromServiceUnitID.SelectedValue))
            {
                _autoNumber = Helper.GetNewAutoNumber(txtOrderDate.SelectedDate.Value.Date, getPageID == "" ? TransactionCode.AssetWorkOrder : TransactionCode.SanitationWorkOrder, serv.DepartmentID);
                txtOrderNo.Text = _autoNumber.LastCompleteNumber;
            }
        }

        #region Record Detail Method Function

        private AssetWorkOrderItemCollection AssetWorkOrderItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collAssetWorkOrderItem" + Request.UserHostName];
                    if (obj != null)
                        return ((AssetWorkOrderItemCollection)(obj));
                }

                var coll = new AssetWorkOrderItemCollection();

                var query = new AssetWorkOrderItemQuery("a");
                var ipq = new VwItemProductMedicNonMedicQuery("b");
                query.LeftJoin(ipq).On(ipq.ItemID == query.ItemID);

                query.Select(query, @"<ISNULL(b.IsInventoryItem, 0) AS 'refToItemProductNonMedic_IsInventoryItem'>");

                query.Where(query.OrderNo == txtOrderNo.Text);

                query.OrderBy(query.SeqNo.Ascending);

                coll.Load(query);

                Session["collAssetWorkOrderItem" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collAssetWorkOrderItem" + Request.UserHostName] = value; }
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            AssetWorkOrderItems = null; //Reset Record Detail
            grdItem.DataSource = AssetWorkOrderItems;
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = AssetWorkOrderItems;
        }
       
        #endregion

        #region Work Order Implementer
        private AssetWorkOrderImplementerCollection AssetWorkOrderImplementers
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collAssetWorkOrderImplementer" + Request.UserHostName];
                    if (obj != null)
                        return ((AssetWorkOrderImplementerCollection)(obj));
                }

                var coll = new AssetWorkOrderImplementerCollection();

                var query = new AssetWorkOrderImplementerQuery("a");
                var usr = new AppUserQuery("b");
                query.InnerJoin(usr).On(query.UserID == usr.UserID);

                query.Select(
                    query,
                    usr.UserName.As("refToAppUser_UserName")
                    );

                query.Where(query.OrderNo == txtOrderNo.Text);

                query.OrderBy(query.UserID.Ascending);

                coll.Load(query);

                Session["collAssetWorkOrderImplementer" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collAssetWorkOrderImplementer" + Request.UserHostName] = value; }
        }

        private void PopulateGridImplementerDetail()
        {
            //Display Data Detail
            AssetWorkOrderImplementers = null; //Reset Record Detail
            grdImplementer.DataSource = AssetWorkOrderImplementers;
            grdImplementer.MasterTableView.IsItemInserted = false;
            grdImplementer.MasterTableView.ClearEditItems();
            grdImplementer.DataBind();
        }

        protected void grdImplementer_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdImplementer.DataSource = AssetWorkOrderImplementers;
        }

        #endregion

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
            ToolBarMenuEdit.Enabled = !(bool)ViewState["IsApproved"] && !(bool)ViewState["IsVoid"];
        }

        protected void txtAssetID_TextChanged(object sender, EventArgs e)
        {
            PopulateAssetInfo(txtAssetID.Text, true);
        }

        private void PopulateAssetInfo(string assetId, bool fromAsset)
        {
            var asset = new BusinessObject.Asset();
            if (asset.LoadByPrimaryKey(assetId))
            {
                txtBrandName.Text = asset.BrandName;
                txtSerialNo.Text = asset.SerialNumber;
                
                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(asset.ServiceUnitID);
                var room = new ServiceRoom();
                if (room.LoadByPrimaryKey(asset.AssetLocationID))
                {
                    if (!string.IsNullOrEmpty(room.RoomName))
                        txtLocationName.Text = unit.ServiceUnitName + " - " + room.RoomName;
                    else
                        txtLocationName.Text = unit.ServiceUnitName;
                }
                else
                    txtLocationName.Text = unit.ServiceUnitName;

                cboSRAssetsStatus.SelectedValue = asset.SRAssetsStatus;
                txtAssetNotes.Text = asset.Notes;
                txtNotesToTechnician.Text = asset.NotesToTechnician;
                cboSRAssetsWarrantyContract.SelectedValue = asset.SRAssetsWarrantyContract;
                txtGuaranteeExpiredDate.SelectedDate = asset.GuaranteeExpiredDate;
                lblAssetName.Text = asset.AssetName;
                if (fromAsset)
                {
                    if (!string.IsNullOrEmpty(asset.ItemID))
                    {
                        var iq = new ItemQuery();
                        iq.Where(iq.ItemID == asset.ItemID);
                        cboItemID.DataSource = iq.LoadDataTable();
                        cboItemID.DataBind();
                        cboItemID.SelectedValue = asset.ItemID;
                    }
                    else
                    {
                        cboItemID.Items.Clear();
                        cboItemID.Text = string.Empty;
                    }
                }
            }
            else
            {
                txtBrandName.Text = string.Empty;
                txtSerialNo.Text = string.Empty;
                txtLocationName.Text = string.Empty;
                cboSRAssetsStatus.SelectedValue = string.Empty;
                txtAssetNotes.Text = string.Empty;
                txtNotesToTechnician.Text = string.Empty;
                cboSRAssetsWarrantyContract.SelectedValue = string.Empty;
                txtGuaranteeExpiredDate.SelectedDate = null;
                lblAssetName.Text = string.Empty;

                if (fromAsset)
                {
                    cboItemID.Items.Clear();
                    cboItemID.Text = string.Empty;
                }
            }
        }

        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ItemQuery("a");
            var ipnm = new ItemProductNonMedicQuery("b");
            query.InnerJoin(ipnm).On(query.ItemID == ipnm.ItemID);
            query.es.Top = 20;
            query.Where
                (query.Or(query.ItemName.Like(searchTextContain),
                          query.ItemID.Like(searchTextContain)),
                 query.IsActive == true);
            query.OrderBy(query.ItemName.Ascending);
            DataTable dtb = query.LoadDataTable();

            cboItemID.DataSource = dtb;
            cboItemID.DataBind();
        }
        
        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"] + " [" +
                          ((DataRowView)e.Item.DataItem)["ItemID"]+ "]";
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRWorkStatus_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery("a");
            query.Where
                (query.StandardReferenceID == AppEnum.StandardReference.WorkStatus.ToString(),
                 query.Or(query.ItemName.Like(searchTextContain),
                          query.ItemID.Like(searchTextContain)),
                 query.IsActive == true, query.IsUsedBySystem == true);
            query.OrderBy(query.ItemID.Ascending);
            DataTable dtb = query.LoadDataTable();

            cboSRWorkStatus.DataSource = dtb;
            cboSRWorkStatus.DataBind();
        }

        protected void cboSRWorkStatus_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRWorkType_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery("a");
            query.Where
                (query.StandardReferenceID == AppEnum.StandardReference.WorkType.ToString(),
                 query.Or(query.ItemName.Like(searchTextContain),
                          query.ItemID.Like(searchTextContain)),
                 query.IsActive == true, query.IsUsedBySystem == true);
            query.OrderBy(query.ItemID.Ascending);
            DataTable dtb = query.LoadDataTable();

            cboSRWorkType.DataSource = dtb;
            cboSRWorkType.DataBind();
        }

        protected void cboSRWorkType_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }
    }
}
