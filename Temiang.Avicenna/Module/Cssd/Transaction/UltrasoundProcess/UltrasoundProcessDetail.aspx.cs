using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class UltrasoundProcessDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "UltrasoundProcessSearch.aspx";
            UrlPageList = "UltrasoundProcessList.aspx";

            ProgramID = AppConstant.Program.CssdUltrasoundProcess;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
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
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new CssdSterileItemsUltrasound());

            txtTransactionNo.Text = GetNewProcessNo();
            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtTransactionTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");

            btnGetPickList.Enabled = true;
            btnResetItem.Enabled = true;

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            //var entity = new CssdUltrasoundProcess();
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
            if (CssdSterileItemsUltrasoundItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new CssdSterileItemsUltrasound();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (CssdSterileItemsUltrasoundItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new CssdSterileItemsUltrasound();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text.Trim());
            auditLogFilter.TableName = "CssdSterileItemsUltrasound";
        }

        #endregion

        #region ToolBar Menu Support
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new CssdSterileItemsUltrasound();
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

        private bool IsApprovedOrVoid(CssdSterileItemsUltrasound entity, ValidateArgs args)
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
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
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
            var entity = new CssdSterileItemsUltrasound();
            if (parameters.Length > 0)
            {
                var receivedNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(receivedNo);
            }
            else
                entity.LoadByPrimaryKey(txtTransactionNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var process = (CssdSterileItemsUltrasound)entity;

            txtTransactionNo.Text = process.TransactionNo;
            txtTransactionDate.SelectedDate = process.TransactionDate;
            txtTransactionTime.Text = process.TransactionTime;
            
            if (!string.IsNullOrEmpty(process.TransactionByUserID))
            {
                var usr = new AppUserQuery();
                usr.Where(usr.UserID == process.TransactionByUserID);
                cboTransactionByUserID.DataSource = usr.LoadDataTable();
                cboTransactionByUserID.DataBind();
                cboTransactionByUserID.SelectedValue = process.TransactionByUserID;
            }
            else
            {
                var usr = new AppUserQuery();
                usr.Where(usr.UserID == AppSession.UserLogin.UserID);
                cboTransactionByUserID.DataSource = usr.LoadDataTable();
                cboTransactionByUserID.DataBind();
                cboTransactionByUserID.SelectedValue = AppSession.UserLogin.UserID;
            }

            btnGetPickList.Enabled = false;
            btnResetItem.Enabled = false;


            PopulateItemGrid();

            ViewState["IsApproved"] = process.IsApproved ?? false;
            ViewState["IsVoid"] = process.IsVoid ?? false;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new CssdSterileItemsUltrasound();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
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

            SetApproval(entity, true, args);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new CssdSterileItemsUltrasound();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
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

            SetApproval(entity, false, args);
        }

        private void SetApproval(CssdSterileItemsUltrasound entity, bool isApproval, ValidateArgs args)
        {
            using (var trans = new esTransactionScope())
            {
                entity.IsApproved = isApproval;
                if (isApproval)
                {
                    entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                    entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();

                    foreach (var item in CssdSterileItemsUltrasoundItems)
                    {
                        var received = new CssdSterileItemsReceivedItem();
                        if (received.LoadByPrimaryKey(item.ReceivedNo, item.ReceivedSeqNo))
                        {
                            received.SRCssdPhase = "7";
                            received.IsUltrasound = true;

                            received.Save();
                        }
                    }
                }
                else
                {
                    entity.ApprovedByUserID = null;
                    entity.ApprovedDateTime = null;

                    foreach (var item in CssdSterileItemsUltrasoundItems)
                    {
                        var received = new CssdSterileItemsReceivedItem();
                        if (received.LoadByPrimaryKey(item.ReceivedNo, item.ReceivedSeqNo))
                        {
                            if (AppSession.Application.IsMenuCssdPackagingActive)
                                received.SRCssdPhase = "6";
                            else if (AppSession.Application.IsMenuCssdFeasibilityTestActive)
                                received.SRCssdPhase = "5";
                            else if (AppSession.Application.IsMenuCssdDecontaminationActive)
                                received.SRCssdPhase = "4";
                            else received.SRCssdPhase = "1";

                            received.IsUltrasound = null;

                            received.Save();
                        }
                    }
                }

                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.Save();

                var balances = new CssdItemBalanceCollection();
                CssdItemBalance.PrepareItemBalanceUltrasound(entity.TransactionNo, AppSession.Parameter.ServiceUnitCssdID, AppSession.UserLogin.UserID,
                    AppSession.Application.IsMenuCssdPackagingActive, AppSession.Application.IsMenuCssdFeasibilityTestActive, AppSession.Application.IsMenuCssdDecontaminationActive, isApproval, ref balances);
                if (balances != null)
                    balances.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new CssdSterileItemsUltrasound();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
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
            var entity = new CssdSterileItemsUltrasound();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private void SetVoid(CssdSterileItemsUltrasound entity, bool isVoid)
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

        private void SetEntityValue(CssdSterileItemsUltrasound entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtTransactionNo.Text = GetNewProcessNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.TransactionTime = txtTransactionTime.TextWithLiterals;
            entity.TransactionByUserID = cboTransactionByUserID.SelectedValue;
            
            entity.IsApproved = false;
            entity.IsVoid = false;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (var item in CssdSterileItemsUltrasoundItems)
            {
                item.TransactionNo = txtTransactionNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(CssdSterileItemsUltrasound entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                CssdSterileItemsUltrasoundItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new CssdSterileItemsUltrasoundQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.TransactionNo > txtTransactionNo.Text
                    );
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.TransactionNo < txtTransactionNo.Text
                    );
                que.OrderBy(que.TransactionNo.Descending);
            }

            var entity = new CssdSterileItemsUltrasound();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Method & Event TextChanged
        #endregion

        #region Record Detail Method Function of CssdSterileItemsUltrasoundItem

        private CssdSterileItemsUltrasoundItemCollection CssdSterileItemsUltrasoundItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collCssdSterileItemsUltrasoundItem" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((CssdSterileItemsUltrasoundItemCollection)(obj));
                    }
                }

                var coll = new CssdSterileItemsUltrasoundItemCollection();
                var query = new CssdSterileItemsUltrasoundItemQuery("a");
                var received = new CssdSterileItemsReceivedItemQuery("b");
                var iq = new ItemQuery("c");
                var unitq = new AppStandardReferenceItemQuery("d");

                query.Select
                    (
                        query,

                        received.Qty.As("refToCssdSterileItemsReceivedItem_Qty"),
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
                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(query.TransactionSeqNo.Ascending);
                coll.Load(query);

                Session["collCssdSterileItemsUltrasoundItem" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collCssdSterileItemsUltrasoundItem" + Request.UserHostName] = value;
            }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            CssdSterileItemsUltrasoundItems = null; //Reset Record Detail
            grdItem.DataSource = CssdSterileItemsUltrasoundItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private CssdSterileItemsUltrasoundItem FindItem(String seqNo)
        {
            CssdSterileItemsUltrasoundItemCollection coll = CssdSterileItemsUltrasoundItems;
            CssdSterileItemsUltrasoundItem retEntity = null;
            foreach (CssdSterileItemsUltrasoundItem rec in coll)
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
            grdItem.DataSource = CssdSterileItemsUltrasoundItems;
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String seqNo =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][CssdSterileItemsUltrasoundItemMetadata.ColumnNames.TransactionSeqNo]);
            CssdSterileItemsUltrasoundItem entity = FindItem(seqNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        #endregion

        #region Combobox
        protected void cboTransactionByUserID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.UserByUnitItemRequested((RadComboBox)sender, AppSession.Parameter.ServiceUnitCssdID, e.Text);
        }

        protected void cboTransactionByUserID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.UserItemDataBound(e);
        }

        #endregion

        private string GetNewProcessNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.CssdUltrasoundNo);

            return _autoNumber.LastCompleteNumber;
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
            if (CssdSterileItemsUltrasoundItems.Count > 0)
                CssdSterileItemsUltrasoundItems.MarkAllAsDeleted();
            grdItem.DataSource = CssdSterileItemsUltrasoundItems;
            grdItem.DataBind();
        }
    }
}
