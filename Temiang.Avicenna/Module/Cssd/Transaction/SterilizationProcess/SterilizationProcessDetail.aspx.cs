using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class SterilizationProcessDetail : BasePageDetail
    {
        private string IsDtt
        {
            get
            {
                return Request.QueryString["dtt"];
            }
        }

        private AppAutoNumberLast _autoNumber, _autoNumber2;
        
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "SterilizationProcessSearch.aspx?dtt=" + IsDtt;
            UrlPageList = "SterilizationProcessList.aspx?dtt=" + IsDtt;

            ProgramID = IsDtt == "0" ? AppConstant.Program.CssdSterilizationProcess : AppConstant.Program.CssdDttProcess;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRCssdProcessType, AppEnum.StandardReference.CssdProcessType);

                pnlProcessType.Visible = IsDtt == "0";
                pnlProcessTo.Visible = IsDtt == "0";

                trExpiredDate.Visible = !AppSession.Parameter.IsCssdExpiredValidateInReceiveDetail;
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            //ToolBarMenuSearch.Visible = false;
            //ToolBarMenuAdd.Visible = false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            if (IsDtt == "0")
            {
                ajax.AddAjaxSetting(cboSRCssdProcessType, cboSRCssdProcessType);
                ajax.AddAjaxSetting(cboSRCssdProcessType, cboMachineID);

                ajax.AddAjaxSetting(cboMachineID, cboMachineID);
                ajax.AddAjaxSetting(cboMachineID, txtProcessEndTime);
            }
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new CssdSterilizationProcess());

            txtProcessNo.Text = GetNewProcessNo();
            txtProcessDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtProcessStartTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");

            btnGetPickList.Enabled = true;
            btnResetItem.Enabled = true;

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            //var entity = new CssdSterilizationProcess();
            //if (entity.LoadByPrimaryKey(txtProcessNo.Text))
            //{
            //    entity.MarkAsDeleted();

            //    SaveEntity(entity);
            //}
            //else
            //    args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (CssdSterilizationProcessItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new CssdSterilizationProcess();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (CssdSterilizationProcessItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new CssdSterilizationProcess();
            if (entity.LoadByPrimaryKey(txtProcessNo.Text))
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

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("ProcessNo='{0}'", txtProcessNo.Text.Trim());
            auditLogFilter.TableName = "CssdSterilizationProcess";
        }

        #endregion

        #region ToolBar Menu Support
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new CssdSterilizationProcess();
            if (entity.LoadByPrimaryKey(txtProcessNo.Text))
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

        private bool IsApprovedOrVoid(CssdSterilizationProcess entity, ValidateArgs args)
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
            printJobParameters.AddNew("p_ProcessNo", txtProcessNo.Text);
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
            RefreshCommandItem(newVal);
            btnGetPickList.Enabled = newVal != AppEnum.DataMode.Read;
            btnResetItem.Enabled = newVal != AppEnum.DataMode.Read;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new CssdSterilizationProcess();
            if (parameters.Length > 0)
            {
                var receivedNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(receivedNo);
            }
            else
                entity.LoadByPrimaryKey(txtProcessNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var process = (CssdSterilizationProcess)entity;

            txtProcessNo.Text = process.ProcessNo;
            txtProcessDate.SelectedDate = process.ProcessDate;
            txtProcessStartTime.Text = process.ProcessStartTime;
            txtProcessEndTime.Text = process.ProcessEndTime;
            if (!string.IsNullOrEmpty(process.SRCssdProcessType))
                cboSRCssdProcessType.SelectedValue = process.SRCssdProcessType;
            else
            {
                cboSRCssdProcessType.Text = string.Empty;
                cboSRCssdProcessType.SelectedValue = string.Empty;
            }
            if (!string.IsNullOrEmpty(process.MachineID))
            {
                var m = new CssdMachineQuery();
                m.Where(m.MachineID == process.MachineID);
                cboMachineID.DataSource = m.LoadDataTable();
                cboMachineID.DataBind();
                cboMachineID.SelectedValue = process.MachineID;
            }
            else
            {
                cboMachineID.Items.Clear();
                cboMachineID.Text = string.Empty;
            }
            if (!string.IsNullOrEmpty(process.OperatorByUserID))
            {
                var usr = new AppUserQuery();
                usr.Where(usr.UserID == process.OperatorByUserID);
                cboOperatorByUserID.DataSource = usr.LoadDataTable();
                cboOperatorByUserID.DataBind();
                cboOperatorByUserID.SelectedValue = process.OperatorByUserID;
            }
            else
            {
                var usr = new AppUserQuery();
                usr.Where(usr.UserID == AppSession.UserLogin.UserID);
                cboOperatorByUserID.DataSource = usr.LoadDataTable();
                cboOperatorByUserID.DataBind();
                cboOperatorByUserID.SelectedValue = AppSession.UserLogin.UserID;
            }

            txtProcessTo.Text = process.ProcessTo;
            btnProcessTo.Text = !string.IsNullOrEmpty(process.ProcessTo) ? txtProcessTo.Text.Substring(7, 4) : string.Empty;
            if (process.ExpiredDate != null)
                txtExpiredDate.SelectedDate = process.ExpiredDate;
            else
                txtExpiredDate.Clear();

            btnGetPickList.Enabled = false;
            btnResetItem.Enabled = false;

            PopulateItemGrid();

            ViewState["IsApproved"] = process.IsApproved ?? false;
            ViewState["IsVoid"] = process.IsVoid ?? false;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new CssdSterilizationProcess();
            if (!entity.LoadByPrimaryKey(txtProcessNo.Text))
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
            if (IsDtt == "0" && entity.ProcessEndTime == "00:00")
            {
                args.MessageText = "Process End Time required.";
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, true, args);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new CssdSterilizationProcess();
            if (!entity.LoadByPrimaryKey(txtProcessNo.Text))
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

            DataTable dtb = (new CssdSterileItemsReturnedItemCollection()).GetItemReturned(txtProcessNo.Text);
            if (dtb.Rows.Count > 0)
            {
                args.MessageText = "Data has been processed.";
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, false, args);
        }

        private void SetApproval(CssdSterilizationProcess entity, bool isApproval, ValidateArgs args)
        {
            using (var trans = new esTransactionScope())
            {
                entity.IsApproved = isApproval;
                if (isApproval)
                {
                    if (IsDtt == "0")
                    {
                        if (string.IsNullOrEmpty(txtProcessTo.Text))
                        {
                            txtProcessTo.Text = GetNewProcessTo();
                            _autoNumber2.Save();

                            btnProcessTo.Text = txtProcessTo.Text.Substring(7, 4);
                            entity.ProcessTo = txtProcessTo.Text;
                        }
                    }

                    entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                    entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();

                    foreach (var d in CssdSterilizationProcessItems)
                    {
                        var i = new Item();
                        if (i.LoadByPrimaryKey(d.ItemID))
                            d.CostAmount = i.CssdPackagingCostAmount ?? 0;

                        var received = new CssdSterileItemsReceivedItem();
                        if (received.LoadByPrimaryKey(d.ReceivedNo, d.ReceivedSeqNo))
                        {
                            received.SRCssdPhase = "8";
                            received.IsSterilization = true;

                            received.Save();
                        }
                    }
                    CssdSterilizationProcessItems.Save();
                }
                else
                {
                    entity.ApprovedByUserID = null;
                    entity.ApprovedDateTime = null;

                    foreach (var d in CssdSterilizationProcessItems)
                    {
                        var i = new Item();
                        if (i.LoadByPrimaryKey(d.ItemID))
                            d.CostAmount = i.CssdPackagingCostAmount ?? 0;

                        var received = new CssdSterileItemsReceivedItem();
                        if (received.LoadByPrimaryKey(d.ReceivedNo, d.ReceivedSeqNo))
                        {
                            if (received.IsUltrasound ?? false)
                                received.SRCssdPhase = "7";
                            else if (AppSession.Application.IsMenuCssdPackagingActive)
                                received.SRCssdPhase = "6";
                            else if (AppSession.Application.IsMenuCssdFeasibilityTestActive)
                                received.SRCssdPhase = "5";
                            else if (AppSession.Application.IsMenuCssdDecontaminationActive)
                                received.SRCssdPhase = "4";
                            else received.SRCssdPhase = "1";

                            received.IsSterilization = null;

                            received.Save();
                        }
                    }
                }

                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.Save();

                var balances = new CssdItemBalanceCollection();
                CssdItemBalance.PrepareItemBalanceSterilization(entity.ProcessNo, AppSession.Parameter.ServiceUnitCssdID, AppSession.UserLogin.UserID,
                    AppSession.Application.IsMenuCssdPackagingActive, AppSession.Application.IsMenuCssdFeasibilityTestActive, AppSession.Application.IsMenuCssdDecontaminationActive, isApproval, ref balances);
                if (balances != null)
                    balances.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new CssdSterilizationProcess();
            if (!entity.LoadByPrimaryKey(txtProcessNo.Text))
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
            var entity = new CssdSterilizationProcess();
            if (!entity.LoadByPrimaryKey(txtProcessNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private void SetVoid(CssdSterilizationProcess entity, bool isVoid)
        {
            using (var trans = new esTransactionScope())
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

                trans.Complete();
            }
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(CssdSterilizationProcess entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtProcessNo.Text = GetNewProcessNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }
            entity.ProcessNo = txtProcessNo.Text;
            entity.ProcessDate = txtProcessDate.SelectedDate;
            entity.ProcessStartTime = txtProcessStartTime.TextWithLiterals;
            entity.ProcessEndTime = txtProcessEndTime.TextWithLiterals;
            entity.SRCssdProcessType = cboSRCssdProcessType.SelectedValue;
            entity.MachineID = cboMachineID.SelectedValue;
            entity.OperatorByUserID = cboOperatorByUserID.SelectedValue;
            entity.ProcessTo = txtProcessTo.Text;
            entity.IsDtt = IsDtt == "1";
            if (txtExpiredDate.IsEmpty)
                entity.str.ExpiredDate = string.Empty;
            else
                entity.ExpiredDate = txtExpiredDate.SelectedDate;

            entity.IsApproved = false;
            entity.IsVoid = false;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (var item in CssdSterilizationProcessItems)
            {
                item.ProcessNo = txtProcessNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(CssdSterilizationProcess entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                CssdSterilizationProcessItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new CssdSterilizationProcessQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (IsDtt == "0")
                que.Where(que.IsDtt == false);
            else 
                que.Where(que.IsDtt == true);
            if (isNextRecord)
            {
                que.Where
                    (
                        que.ProcessNo > txtProcessNo.Text
                    );
                que.OrderBy(que.ProcessNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.ProcessNo < txtProcessNo.Text
                    );
                que.OrderBy(que.ProcessNo.Descending);
            }

            var entity = new CssdSterilizationProcess();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Method & Event TextChanged
        protected void cboSRCssdProcessType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboMachineID.Items.Clear();
            cboMachineID.Text = string.Empty;
        }

        protected void cboMachineID_SelectedIndexChanged(object sender, EventArgs e)
        {
            var p = new CssdMachineItem();
            if (p.LoadByPrimaryKey(cboMachineID.SelectedValue, cboSRCssdProcessType.SelectedValue))
            {
                var x = p.Duration ?? 0;
                txtProcessEndTime.Text = (new DateTime()).NowAtSqlServer().AddMinutes(x).ToString("HH:mm");
            }
        }
        #endregion

        #region Record Detail Method Function of CssdSterilizationProcessItems

        private CssdSterilizationProcessItemCollection CssdSterilizationProcessItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collCssdSterilizationProcessItem" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((CssdSterilizationProcessItemCollection)(obj));
                    }
                }

                var coll = new CssdSterilizationProcessItemCollection();
                var query = new CssdSterilizationProcessItemQuery("a");
                var received = new CssdSterileItemsReceivedItemQuery("b");
                var iq = new ItemQuery("c");
                var unitq = new AppStandardReferenceItemQuery("d");

                query.Select
                    (
                        query,

                        received.CssdItemNo.As("refToCssdSterileItemsReceivedItem_CssdItemNo"),
                        @"<CAST(b.CssdItemNo  AS VARCHAR) AS 'refTo_CssdItemNo'>",

                        received.ItemID.As("refToCssdSterileItemsReceivedItem_ItemID"),
                        iq.ItemName.As("refToCssdItem_ItemName"),

                        unitq.ItemName.As("refToAppStandardReferenceItem_CssdItemUnit"),
                        received.Notes.As("refToCssdSterileItemsReceivedItem_Notes")

                    );
                query.InnerJoin(received).On(received.ReceivedNo == query.ReceivedNo &&
                                         received.ReceivedSeqNo == query.ReceivedSeqNo);
                query.InnerJoin(iq).On(iq.ItemID == received.ItemID);
                query.InnerJoin(unitq).On(unitq.ItemID == received.SRCssdItemUnit &&
                                          unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                query.Where(query.ProcessNo == txtProcessNo.Text);
                query.OrderBy(query.ProcessSeqNo.Ascending);
                coll.Load(query);

                Session["collCssdSterilizationProcessItem" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collCssdSterilizationProcessItem" + Request.UserHostName] = value;
            }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            CssdSterilizationProcessItems = null; //Reset Record Detail
            grdItem.DataSource = CssdSterilizationProcessItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private CssdSterilizationProcessItem FindItem(String seqNo)
        {
            CssdSterilizationProcessItemCollection coll = CssdSterilizationProcessItems;
            CssdSterilizationProcessItem retEntity = null;
            foreach (CssdSterilizationProcessItem rec in coll)
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
            grdItem.DataSource = CssdSterilizationProcessItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String seqNo =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][CssdSterilizationProcessItemMetadata.ColumnNames.ProcessSeqNo]);
            CssdSterilizationProcessItem entity = FindItem(seqNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String seqNo =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][CssdSterilizationProcessItemMetadata.ColumnNames.ProcessSeqNo]);
            CssdSterilizationProcessItem entity = FindItem(seqNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        private void SetEntityValue(CssdSterilizationProcessItem entity, GridCommandEventArgs e)
        {
            var userControl = (SterilizationProcessItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.Qty = userControl.Qty;
                entity.Weight = userControl.Weight;
            }
        }

        #endregion

        #region Combobox
        protected void cboOperatorByUserID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.UserByUnitItemRequested((RadComboBox)sender, AppSession.Parameter.ServiceUnitCssdID, e.Text);
        }

        protected void cboOperatorByUserID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.UserItemDataBound(e);
        }

        protected void cboMachineID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new CssdMachineQuery("a");
            var itemq = new CssdMachineItemQuery("b");
            query.InnerJoin(itemq).On(itemq.MachineID == query.MachineID);
            query.Where(
                itemq.SRCssdProcessType == cboSRCssdProcessType.SelectedValue,
                query.Or(query.MachineID == e.Text,
                query.MachineName.Like(searchTextContain)),
                query.IsActive == true
                );
            query.Select(query.MachineID, query.MachineName);

            query.es.Top = 20;
            query.es.Distinct = true;
            DataTable dtb = query.LoadDataTable();
            cboMachineID.DataSource = dtb;
            cboMachineID.DataBind();
        }

        protected void cboMachineID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["MachineName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["MachineID"].ToString();
        }
        #endregion

        private string GetNewProcessNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date,
                                                  IsDtt == "0"
                                                      ? AppEnum.AutoNumber.CssdProcessNo
                                                      : AppEnum.AutoNumber.CssdDttNo);

            return _autoNumber.LastCompleteNumber;
        }

        private string GetNewProcessTo()
        {
            _autoNumber2 = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.CssdProcessTo);

            return _autoNumber2.LastCompleteNumber;
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (!(sourceControl is RadGrid))
                return;

            RadGrid grd = (RadGrid)sourceControl;
            switch (grd.ID)
            {
                case "grdItem":
                    grdItem.Rebind();
                    break;
            }
        }

        protected void btnResetItem_Click(object sender, EventArgs e)
        {
            if (CssdSterilizationProcessItems.Count > 0)
                CssdSterilizationProcessItems.MarkAllAsDeleted();
            grdItem.DataSource = CssdSterilizationProcessItems;
            grdItem.DataBind();
        }
    }
}
