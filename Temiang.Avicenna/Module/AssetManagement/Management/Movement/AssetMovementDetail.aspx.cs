using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.AssetManagement
{
    public partial class AssetMovementDetail : BasePageDetail
    {
        private string FormType
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            if (FormType == "appr")
            {
                UrlPageSearch = "##";
                UrlPageList = "AssetMovementApprovalList.aspx?type=" + FormType;
            }
            else
            {
                UrlPageSearch = "AssetMovementSearch.aspx?type=" + FormType;
                UrlPageList = "AssetMovementList.aspx?type=" + FormType;
            }
            
            if (FormType == "req")
                ProgramID = AppConstant.Program.ASSET_MOVEMENT_REQUEST;
            else
                ProgramID = AppConstant.Program.ASSET_MOVEMENT;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(cboFromServiceUnit, cboFromServiceUnit);
            ajax.AddAjaxSetting(cboFromServiceUnit, cboFromLocation);
            ajax.AddAjaxSetting(cboFromServiceUnit, cboAssetID);

            ajax.AddAjaxSetting(cboFromLocation, cboFromLocation);
            ajax.AddAjaxSetting(cboFromLocation, cboAssetID);

            ajax.AddAjaxSetting(cboAssetID, cboAssetID);
            ajax.AddAjaxSetting(cboAssetID, txtSerialNumber);
            ajax.AddAjaxSetting(cboAssetID, txtAssetGroup);
            ajax.AddAjaxSetting(cboAssetID, txtPurchaseDate2);
            ajax.AddAjaxSetting(cboAssetID, cboFromServiceUnit);
            ajax.AddAjaxSetting(cboAssetID, cboFromLocation);

            ajax.AddAjaxSetting(cboToServiceUnit, cboToServiceUnit);
            ajax.AddAjaxSetting(cboToServiceUnit, cboToLocation);

        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = AssetMovement.Get(txtTransactionNo.Text);
            if (entity == null)
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            if (FormType == "req")
            {
                if (entity.IsApproved.Value)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved + AppConstant.Message.RecordCanNotEdited;
                    args.IsCancel = true;
                    return;
                }
            }
            else
            {
                if (entity.IsPosted.Value)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved + AppConstant.Message.RecordCanNotEdited;
                    args.IsCancel = true;
                    return;
                }
            }
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new AssetMovement());
            txtTransactionNo.Text = GetNextAutoNumber(false);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = AssetMovement.Get(txtTransactionNo.Text);
            if (entity == null)
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsPosted.Value || entity.IsApproved.Value)
            {
                args.MessageText = "This data already approved.";
                args.IsCancel = true;
                return;
            }

            entity.MarkAsDeleted();
            SaveEntity(entity);

            if (FormType == "appr")
                Response.Redirect("AssetMovementApprovalList.aspx?type=" + FormType);
            else
                Response.Redirect("AssetMovementList.aspx?type=" + FormType);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboToServiceUnit.SelectedValue))
            {
                args.MessageText = "To Service Unit is required.";
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                args.MessageText = "Notes is required.";
                args.IsCancel = true;
                return;
            }

            var entity = new AssetMovement();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboToServiceUnit.SelectedValue))
            {
                args.MessageText = "To Service Unit is required.";
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                args.MessageText = "Notes is required.";
                args.IsCancel = true;
                return;
            }

            var entity = AssetMovement.Get(txtTransactionNo.Text);
            if (entity != null)
            {
                SetEntityValue(entity);
                SaveEntity(entity);
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

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_AssetMovementNo", txtTransactionNo.Text);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("AssetMovementNo='{0}'", txtTransactionNo.Text.Trim());
            auditLogFilter.TableName = "Asset";
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            bool isAutoJournal = false;
            var entity = AssetMovement.Get(txtTransactionNo.Text);
            if (entity == null)
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (FormType == "req")
            {
                if (entity.IsApproved.Value)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return;
                }

                entity.IsApproved = true;
                entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                entity.ApprovedDateTime = DateTime.Now;
            }
            else if (FormType == "appr")
            {
                if (entity.IsPosted.Value)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return;
                }
                
                if (AppSession.Parameter.acc_IsAutoJournalAssetMovement)
                {
                    var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.MovementDate ?? DateTime.Now);
                    if (isClosingPeriod)
                    {
                        args.MessageText = "Financial statements for period: " +
                                       string.Format("{0:MMMM-yyyy}", entity.MovementDate) +
                                       " have been closed. Please contact the authorities.";
                        args.IsCancel = true;
                        return;
                    }
                    isAutoJournal = true;
                }
                entity.IsPosted = true;
            }
            else
            {
                if (entity.IsPosted.Value)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return;
                }

                entity.IsApproved = true;
                entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                entity.ApprovedDateTime = DateTime.Now;
                entity.IsPosted = true;
            }
            
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;

            //SaveEntity(entity);
            using (var trans = new esTransactionScope())
            {
                if (entity.IsPosted == true)
                {
                    var asset = Asset.Get(entity.AssetID);
                    asset.ServiceUnitID = entity.ToServiceUnitID;
                    asset.AssetLocationID = entity.ToAssetLocationID;

                    asset.Save();

                    if (isAutoJournal)
                    {
                        if (asset.IsFixedAsset == true)
                        {
                            var adpQ = new AssetDepreciationPostQuery("adp");
                            adpQ.Where(adpQ.AssetID == entity.AssetID);
                            adpQ.Where(string.Format("<CAST(adp.[Month] AS INT) = {0} AND CAST(adp.[Year] AS INT) = {1}>", entity.MovementDate.Value.Month, entity.MovementDate.Value.Year));

                            DataTable dtb = adpQ.LoadDataTable();
                            entity.CurrentValue = dtb.Rows.Count > 0 ? Convert.ToDecimal(dtb.Rows[0]["AccumulationAmount"]) : 0;

                            int? journalId = JournalTransactions.AddNewAssetMovementJournal(entity, asset, AppSession.UserLogin.UserID, 0);
                        }
                        else
                        {
                            entity.CurrentValue = 0;
                        }
                    }
                }

                entity.Save();
                //Commit if success, Rollback if failed

                trans.Complete();
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            if (FormType == "appr")
                ToolBarMenuSearch.Enabled = false;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !chkIsApproved.Checked;
        }

        public override bool OnGetStatusMenuDelete()
        {
            return !chkIsApproved.Checked;
        }
        #endregion

        #region Private Method Standard
        private void SetEntityValue(AssetMovement entity)
        {
            entity.MovementDate = txtMovementDate.SelectedDate;
            entity.AssetID = cboAssetID.SelectedValue;
            entity.FromServiceUnitID = cboFromServiceUnit.SelectedValue;
            entity.FromAssetLocationID = cboFromLocation.SelectedValue;
            entity.ToServiceUnitID = cboToServiceUnit.SelectedValue;
            entity.ToAssetLocationID = cboToLocation.SelectedValue;
            entity.Notes = txtDescription.Text;

            entity.IsDeleted = false;
            entity.IsApproved = false;
            entity.IsPosted = false;
            entity.FromDepartmentID = string.Empty;
            entity.ToDepartmentID = string.Empty;

            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(AssetMovement entity)
        {
            using (var trans = new esTransactionScope())
            {
                if (entity.es.IsAdded)
                    entity.AssetMovementNo = GetNextAutoNumber(true);

                if (entity.IsPosted == true)
                {
                    var asset = Asset.Get(entity.AssetID);
                    asset.ServiceUnitID = entity.ToServiceUnitID;
                    asset.AssetLocationID = entity.ToAssetLocationID;

                    asset.Save();

                }

                entity.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new AssetMovementQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.AssetMovementNo > txtTransactionNo.Text);
                que.OrderBy(que.AssetMovementNo.Ascending);
            }
            else
            {
                que.Where(que.AssetMovementNo < txtTransactionNo.Text);
                que.OrderBy(que.AssetMovementNo.Descending);
            }

            var entity = new AssetMovement();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        private string GetNextAutoNumber(bool save)
        {
            var autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.AssetMovementNo);
            if (save)
                autoNumber.Save();
            return autoNumber.LastCompleteNumber;
        }

        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //var enabled = (newVal == AppEnum.DataMode.New);
            //cboAssetID.Enabled = enabled;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new AssetMovement();
            if (parameters.Length > 0)
            {
                var id = parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity = AssetMovement.Get(id);
            }
            else
            {
                entity = AssetMovement.Get(txtTransactionNo.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var am = (AssetMovement)entity;
            txtTransactionNo.Text = am.AssetMovementNo;
            txtMovementDate.SelectedDate = am.MovementDate;

            if (!string.IsNullOrEmpty(am.FromServiceUnitID))
            {
                var unitq = new ServiceUnitQuery();
                unitq.Where(unitq.ServiceUnitID == am.FromServiceUnitID);
                cboFromServiceUnit.DataSource = unitq.LoadDataTable();
                cboFromServiceUnit.DataBind();
                cboFromServiceUnit.SelectedValue = am.FromServiceUnitID;

                PopulateRoomList(cboFromServiceUnit.SelectedValue, false, cboFromLocation);
                cboFromLocation.SelectedValue = am.FromAssetLocationID;
            }
            else
            {
                cboFromServiceUnit.Items.Clear();
                cboFromServiceUnit.Text = string.Empty;
                cboFromLocation.Items.Clear();
                cboFromLocation.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(am.AssetID))
            {
                var assetq = new AssetQuery("a");
                var funitq = new ServiceUnitQuery("b");
                var tunitq = new ServiceUnitQuery("c");
                assetq.InnerJoin(funitq).On(assetq.ServiceUnitID == funitq.ServiceUnitID);
                assetq.LeftJoin(tunitq).On(assetq.MaintenanceServiceUnitID == tunitq.ServiceUnitID);
                assetq.Select(assetq.AssetID, assetq.AssetName, assetq.SerialNumber, funitq.ServiceUnitName,
                              tunitq.ServiceUnitName.As("MaintenanceServiceUnitName"));
                assetq.Where(assetq.AssetID == am.AssetID);
                cboAssetID.DataSource = assetq.LoadDataTable();
                cboAssetID.DataBind();
                cboAssetID.SelectedValue = am.AssetID;

                PopulateAssetInfo(am.AssetID, false);
            }
            else
            {
                cboAssetID.Items.Clear();
                cboAssetID.Text = string.Empty;
                txtBrandName.Text = string.Empty;
                txtSerialNumber.Text = string.Empty;
                txtAssetGroup.Text = string.Empty;
                txtPurchaseDate2.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(am.ToServiceUnitID))
            {
                var unitq = new ServiceUnitQuery();
                unitq.Where(unitq.ServiceUnitID == am.ToServiceUnitID);
                cboToServiceUnit.DataSource = unitq.LoadDataTable();
                cboToServiceUnit.DataBind();
                cboToServiceUnit.SelectedValue = am.ToServiceUnitID;

                PopulateRoomList(cboFromServiceUnit.SelectedValue, false, cboToLocation);
                cboToLocation.SelectedValue = am.ToAssetLocationID;
            }
            else
            {
                cboToServiceUnit.Items.Clear();
                cboToServiceUnit.Text = string.Empty;
                cboToLocation.Items.Clear();
                cboToLocation.Text = string.Empty;
            }

            txtDescription.Text = am.Notes;

            if (FormType == "req")
                chkIsApproved.Checked = am.IsApproved ?? false;
            else
                chkIsApproved.Checked = am.IsPosted ?? false;

            OnGetStatusMenuApproval();
        }

        private void PopulateAssetInfo(string assetId, bool isNew)
        {
            string unitId, locId;
            var asset = new Asset();
            if (asset.LoadByPrimaryKey(assetId))
            {
                txtBrandName.Text = asset.BrandName;
                txtSerialNumber.Text = asset.SerialNumber;
                var g = AssetGroup.Get(asset.AssetGroupID);
                txtAssetGroup.Text = string.Format("{0} - {1}", asset.AssetGroupID, g.GroupName);
                txtPurchaseDate2.Text = asset.PurchasedDate.Value.ToString("dd-MMMM-yyyy");
                unitId = asset.ServiceUnitID;
                locId = asset.AssetLocationID;
            }
            else
            {
                txtBrandName.Text = string.Empty;
                txtSerialNumber.Text = string.Empty;
                txtAssetGroup.Text = string.Empty;
                txtPurchaseDate2.Text = string.Empty;
                unitId = string.Empty;
                locId = string.Empty;
            }

            if (isNew)
            {
                if (string.IsNullOrEmpty(cboFromServiceUnit.SelectedValue))
                {
                    var unitq = new ServiceUnitQuery();
                    unitq.Where(unitq.ServiceUnitID == unitId);
                    cboFromServiceUnit.DataSource = unitq.LoadDataTable();
                    cboFromServiceUnit.DataBind();
                    cboFromServiceUnit.SelectedValue = unitId;
                }
                if (string.IsNullOrEmpty(cboFromLocation.SelectedValue))
                {
                    PopulateRoomList(cboFromServiceUnit.SelectedValue, false, cboFromLocation);
                    cboFromLocation.SelectedValue = locId;
                }
            }
        }

        #endregion

        #region Combobox
        protected void cboFromServiceUnit_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new ServiceUnitQuery("a");
            //var usr = new AppUserServiceUnitQuery("b");
            //query.InnerJoin(usr).On(query.ServiceUnitID == usr.ServiceUnitID &&
            //                        usr.UserID == AppSession.UserLogin.UserID);
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            query.es.Top = 20;
            query.Where
                (
                    query.ServiceUnitName.Like(searchText),
                    query.IsActive == true
                );
            query.OrderBy(query.ServiceUnitID.Ascending);

            cboFromServiceUnit.DataSource = query.LoadDataTable();
            cboFromServiceUnit.DataBind();
        }
        protected void cboFromServiceUnit_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboFromLocation.Items.Clear();
            cboFromLocation.Text = string.Empty;
            PopulateRoomList(cboFromServiceUnit.SelectedValue, true, cboFromLocation);
            cboAssetID.Items.Clear();
            cboAssetID.Text = string.Empty;
        }

        protected void cboFromLocation_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboAssetID.Items.Clear();
            cboAssetID.Text = string.Empty;
        }

        protected void cboAssetID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["AssetName"] + " (" + ((DataRowView)e.Item.DataItem)["AssetID"] + ")";
            e.Item.Value = ((DataRowView)e.Item.DataItem)["AssetID"].ToString();
        }

        protected void cboAssetID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new AssetQuery("a");
            var fromunit = new ServiceUnitQuery("b");
            var tounit = new ServiceUnitQuery("c");
            query.InnerJoin(fromunit).On(query.ServiceUnitID == fromunit.ServiceUnitID);
            query.LeftJoin(tounit).On(query.MaintenanceServiceUnitID == tounit.ServiceUnitID);
            query.es.Top = 20;
            query.Select(query.AssetID, query.AssetName, query.SerialNumber, fromunit.ServiceUnitName,
                         tounit.ServiceUnitName.As("MaintenanceServiceUnitName"));
            query.Where
                (
                    query.Or(query.AssetID.Like(searchText), 
                            query.AssetName.Like(searchText),
                            query.SerialNumber.Like(searchText)),
                    query.ServiceUnitID == cboFromServiceUnit.SelectedValue
                );
            if (!string.IsNullOrEmpty(cboFromLocation.SelectedValue))
            {
                query.Where(query.AssetLocationID == cboFromLocation.SelectedValue);
            }
            query.OrderBy(query.AssetName.Ascending);
            DataTable dtb = query.LoadDataTable();
            cboAssetID.DataSource = dtb;
            cboAssetID.DataBind();
        }

        protected void cboAssetID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateAssetInfo(e.Value, true);
        }

        protected void cboServiceUnit_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitID"] + @" - " + ((DataRowView)e.Item.DataItem)["ServiceUnitName"];
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }
        protected void cboToServiceUnit_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new ServiceUnitQuery();
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            query.es.Top = 20;
            query.Where
                (
                    query.ServiceUnitName.Like(searchText),
                    query.IsActive == true
                );
            query.OrderBy(query.ServiceUnitID.Ascending);

            cboToServiceUnit.DataSource = query.LoadDataTable();
            cboToServiceUnit.DataBind();
        }
        protected void cboToServiceUnit_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboToLocation.Items.Clear();
            cboToLocation.Text = string.Empty;
            PopulateRoomList(cboToServiceUnit.SelectedValue, true, cboToLocation);
        }

        #endregion

        #region Populate List
        private void PopulateRoomList(string serviceUnitId, bool isNew, RadComboBox comboBox)
        {
            if (serviceUnitId != string.Empty)
            {
                var sr = new ServiceRoomCollection();
                sr.Query.Where(sr.Query.ServiceUnitID == serviceUnitId);

                if (isNew)
                    sr.Query.Where(sr.Query.IsActive == true);

                sr.LoadAll();

                comboBox.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceRoom entity in sr)
                {
                    comboBox.Items.Add(new RadComboBoxItem(entity.RoomName, entity.RoomID));
                }
            }
        }
        #endregion
    }
}
