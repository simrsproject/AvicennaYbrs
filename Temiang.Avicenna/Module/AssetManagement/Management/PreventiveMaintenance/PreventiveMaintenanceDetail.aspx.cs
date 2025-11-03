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
    public partial class PreventiveMaintenanceDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "#";
            UrlPageList = "PreventiveMaintenanceList.aspx";

            ProgramID = AppConstant.Program.AssetPreventiveMaintenance;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, TransactionCode.AssetWorkOrderRealization, false);
                StandardReference.InitializeIncludeSpace(cboSRWorkTrade, AppEnum.StandardReference.WorkTrade);

                StandardReference.InitializeIncludeSpace(cboSRAssetsStatus, AppEnum.StandardReference.AssetsStatus);
                StandardReference.InitializeIncludeSpace(cboSRAssetsWarrantyContract, AppEnum.StandardReference.AssetsWarrantyContract);
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuEditClick()
        {
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new AssetPreventiveMaintenance();
            entity.LoadByPrimaryKey(txtPMNo.Text);

            entity.IsApproved = true;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;

            var su = new ServiceUnit();
            su.LoadByPrimaryKey(txtServiceUnitID.Text);

            _autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, TransactionCode.AssetWorkOrder, su.DepartmentID);
            
            var wo = new AssetWorkOrder();
            wo.AddNew();
            wo.OrderNo = _autoNumber.LastCompleteNumber;
            _autoNumber.Save();

            wo.OrderDate = DateTime.Now.Date;
            wo.FromServiceUnitID = txtServiceUnitID.Text;
            wo.ToServiceUnitID = entity.ServiceUnitID;
            wo.AssetID = entity.AssetID;
            wo.ProblemDescription = "Maintenance";
            wo.SRWorkStatus = AppSession.Parameter.WorkStatusOpen;
            wo.SRWorkType = AppSession.Parameter.WorkTypePreventive;
            wo.SRWorkPriority = AppSession.Parameter.WorkPriorityRoutine;
            wo.SRWorkTrade = entity.SRWorkTrade;
            wo.RequiredDate = entity.TargetDate;
            wo.RequestByUserID = AppSession.UserLogin.UserID;
            wo.IsVoid = false;
            wo.IsApproved = true;
            wo.ApprovedDateTime = DateTime.Now;
            wo.IsProceed = false;
            wo.IsPreventiveMaintenance = true;
            wo.PMNo = entity.PMNo;
            wo.IsSanitation = false;
            wo.LastUpdateByUserID = AppSession.UserLogin.UserID;
            wo.LastUpdateDateTime = DateTime.Now;

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                
                wo.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
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
            var entity = new AssetPreventiveMaintenance();
            entity.LoadByPrimaryKey(txtPMNo.Text);

            entity.IsVoid = isVoid;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                trans.Complete();
            }
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
            var entity = new AssetPreventiveMaintenance();
            if (entity.LoadByPrimaryKey(txtPMNo.Text))
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
            var qwo = new AssetWorkOrderQuery();
            qwo.Where(qwo.PMNo == txtPMNo.Text);
            var wo = new AssetWorkOrder();
            wo.Load(qwo);

            var orderNo = string.Empty;
            if (wo != null)
                orderNo = wo.OrderNo;

            printJobParameters.AddNew("p_OrderNo", orderNo);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("OrderNo='{0}'", txtPMNo.Text.Trim());
            auditLogFilter.TableName = "AssetPreventiveMaintenance";
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuEdit()
        {
            return txtPMNo.Text != string.Empty;
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
            var entity = new AssetPreventiveMaintenance();
            if (parameters.Length > 0)
            {
                String orderNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(orderNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtPMNo.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var pm = (AssetPreventiveMaintenance)entity;
            txtPMNo.Text = pm.PMNo;
            txtPMDate.SelectedDate = pm.PMDate;
            cboServiceUnitID.SelectedValue = pm.ServiceUnitID;
            cboSRWorkTrade.SelectedValue = pm.SRWorkTrade;
            txtTargetDate.SelectedDate = pm.TargetDate;

            txtAssetID.Text = pm.AssetID;
            PopulateAssetInfo(txtAssetID.Text);

            ViewState["IsApproved"] = pm.IsApproved ?? false;
            ViewState["IsVoid"] = pm.IsVoid ?? false;
        }

        #endregion

        #region Private Method Standard


        private void SetEntityValue(AssetPreventiveMaintenance entity)
        {
            entity.TargetDate = txtTargetDate.SelectedDate;
            entity.SRWorkTrade = cboSRWorkTrade.SelectedValue;
            
            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(AssetPreventiveMaintenance entity)
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
            var que = new AssetPreventiveMaintenanceQuery("a");
            var user = new AppUserServiceUnitQuery("b");
            que.InnerJoin(user).On(que.ServiceUnitID == user.ServiceUnitID && user.UserID == AppSession.UserLogin.UserID);

            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.PMNo > txtPMNo.Text);
                que.OrderBy(que.PMNo.Ascending);
            }
            else
            {
                que.Where(que.PMNo < txtPMNo.Text);
                que.OrderBy(que.PMNo.Descending);
            }

            var entity = new AssetPreventiveMaintenance();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
            ToolBarMenuEdit.Enabled = !(bool)ViewState["IsApproved"] && !(bool)ViewState["IsVoid"];
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
                
                txtServiceUnitID.Text = asset.ServiceUnitID;
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
    }
}
