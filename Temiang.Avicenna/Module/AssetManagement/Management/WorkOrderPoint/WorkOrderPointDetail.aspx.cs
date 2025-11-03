using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.AssetManagement.Management
{
    public partial class WorkOrderPointDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "#";
            UrlPageList = "WorkOrderPointList.aspx";

            ProgramID = AppConstant.Program.AssetWorkOrderPoint;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, TransactionCode.AssetWorkOrder, false);
                ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, TransactionCode.AssetWorkOrderRealization, false);

                StandardReference.InitializeIncludeSpace(cboSRWorkPriority, AppEnum.StandardReference.WorkPriority);
                StandardReference.InitializeIncludeSpace(cboSRWorkTrade, AppEnum.StandardReference.WorkTrade);

                StandardReference.InitializeIncludeSpace(cboSRAssetsStatus, AppEnum.StandardReference.AssetsStatus);
                StandardReference.InitializeIncludeSpace(cboSRAssetsWarrantyContract, AppEnum.StandardReference.AssetsWarrantyContract);

                StandardReference.InitializeIncludeSpace(cboSRFailureCode, AppEnum.StandardReference.FailureCode);
                StandardReference.InitializeIncludeSpace(cboSRWorkOrderPoint, AppEnum.StandardReference.WorkOrderPoint);
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new AssetWorkOrder();
            if (entity.LoadByPrimaryKey(txtOrderNo.Text))
            {
                if (entity.SRWorkStatus == AppSession.Parameter.WorkStatusClosed && entity.IsProceed == true)
                {
                    args.MessageText = "Work Order has been closed. The data can not be changed.";
                    args.IsCancel = true;
                    return;
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuEditClick()
        {
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
        }

        protected override void OnMenuNewClick()
        {
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
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

            var usr = new AppUser();
            usr.LoadByPrimaryKey(wo.RequestByUserID);
            txtRequestByUserID.Text = usr.UserName;
            txtAssetID.Text = wo.AssetID;
            PopulateAssetInfo(txtAssetID.Text);

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
                txtReceivedDate.SelectedDate = (new DateTime()).NowAtSqlServer().Date;
                txtReceivedTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");
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

            txtAppovedDate.SelectedDate = wo.ApprovedDateTime.Value.Date;
            txtApprovedTime.Text = wo.ApprovedDateTime.Value.ToString("HH:mm");

            txtAcceptedBy.Text = wo.AcceptedBy;
            txtReferenceNo.Text = wo.ReferenceNo;

            cboSRWorkOrderPoint.SelectedValue = wo.SRWorkOrderPoint;
            txtWorkOrderPoint.Value = Convert.ToDouble(wo.WorkOrderPoint);

            PopulateGridImplementerDetail();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(AssetWorkOrder entity)
        {
            entity.OrderNo = txtOrderNo.Text;
            entity.SRWorkOrderPoint = cboSRWorkOrderPoint.SelectedValue;
            entity.WorkOrderPoint = Convert.ToDecimal(txtWorkOrderPoint.Value);

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

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new AssetWorkOrderQuery("a");
            var user = new AppUserServiceUnitQuery("b");
            que.InnerJoin(user).On(que.ToServiceUnitID == user.ServiceUnitID && user.UserID == AppSession.UserLogin.UserID);

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
            que.Where(que.IsProceed == true, que.IsSanitation == false, que.SRWorkOrderPoint.IsNotNull());

            var entity = new AssetWorkOrder();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
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
            ToolBarMenuAdd.Enabled = false;
        }

        private void PopulateAssetInfo(string assetId)
        {
            var asset = new BusinessObject.Asset();
            if (asset.LoadByPrimaryKey(assetId))
            {
                lblAssetName.Text = asset.AssetName;
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
            }
            else
            {
                lblAssetName.Text = string.Empty;
                txtBrandName.Text = string.Empty;
                txtSerialNo.Text = string.Empty;
                txtLocationName.Text = string.Empty;
                cboSRAssetsStatus.SelectedValue = string.Empty;
                txtAssetNotes.Text = string.Empty;
                txtNotesToTechnician.Text = string.Empty;
                cboSRAssetsWarrantyContract.SelectedValue = string.Empty;
                txtGuaranteeExpiredDate.SelectedDate = null;
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
                          ((DataRowView)e.Item.DataItem)["ItemID"] + "]";
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

        protected void cboSRWorkOrderPoint_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value))
            {
                txtWorkOrderPoint.Value = 0;
                return;
            }

            var ute = new AppStandardReferenceItem();
            if (ute.LoadByPrimaryKey(AppEnum.StandardReference.WorkOrderPoint.ToString(), e.Value))
            {
                txtWorkOrderPoint.Value = Convert.ToDouble(ute.NumericValue);
            }
            else
                txtWorkOrderPoint.Value = 0;
        }
    }
}