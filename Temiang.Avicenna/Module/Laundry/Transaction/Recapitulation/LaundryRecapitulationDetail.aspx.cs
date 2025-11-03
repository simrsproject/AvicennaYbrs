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

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class LaundryRecapitulationDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "LaundryRecapitulationSearch.aspx";
            UrlPageList = "LaundryRecapitulationList.aspx";

            ProgramID = AppConstant.Program.LaundryRecapitulation;

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
            //ajax.AddAjaxSetting(grdItem, grdItem);
            //ajax.AddAjaxSetting(AjaxManager, grdItem);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new LaundryRecapitulationProcess());

            txtTransactionNo.Text = GetNewProcessNo();
            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();

            btnGetPickList.Enabled = true;
            btnResetItem.Enabled = true;

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (LaundryRecapitulationProcessItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new LaundryRecapitulationProcess();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (LaundryRecapitulationProcessItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new LaundryRecapitulationProcess();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
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
            auditLogFilter.TableName = "LaundryRecapitulationProcess";
        }

        #endregion

        #region ToolBar Menu Support
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new LaundryRecapitulationProcess();
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

        private bool IsApprovedOrVoid(LaundryRecapitulationProcess entity, ValidateArgs args)
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
            var entity = new LaundryRecapitulationProcess();
            if (parameters.Length > 0)
            {
                var transNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(transNo);
            }
            else
                entity.LoadByPrimaryKey(txtTransactionNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var process = (LaundryRecapitulationProcess)entity;

            txtTransactionNo.Text = process.TransactionNo;
            txtTransactionDate.SelectedDate = process.TransactionDate;
            txtNotes.Text = process.Notes;

            btnGetPickList.Enabled = false;
            btnResetItem.Enabled = false;

            PopulateItemGrid();

            ViewState["IsApproved"] = process.IsApproved ?? false;
            ViewState["IsVoid"] = process.IsVoid ?? false;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new LaundryRecapitulationProcess();
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
            var entity = new LaundryRecapitulationProcess();
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

        private void SetApproval(LaundryRecapitulationProcess entity, bool isApproval, ValidateArgs args)
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

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new LaundryRecapitulationProcess();
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
            var entity = new LaundryRecapitulationProcess();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private void SetVoid(LaundryRecapitulationProcess entity, bool isVoid)
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

        private void SetEntityValue(LaundryRecapitulationProcess entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtTransactionNo.Text = GetNewProcessNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.Notes = txtNotes.Text;
            entity.IsApproved = false;
            entity.IsVoid = false;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (var item in LaundryRecapitulationProcessItems)
            {
                item.TransactionNo = txtTransactionNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(LaundryRecapitulationProcess entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                LaundryRecapitulationProcessItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new LaundryRecapitulationProcessQuery();
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

            var entity = new LaundryRecapitulationProcess();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Method & Event TextChanged
        #endregion

        #region Record Detail Method Function of LaundryRecapitulationProcessItem

        private LaundryRecapitulationProcessItemCollection LaundryRecapitulationProcessItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collLaundryRecapitulationProcessItem" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((LaundryRecapitulationProcessItemCollection)(obj));
                    }
                }

                var coll = new LaundryRecapitulationProcessItemCollection();
                var query = new LaundryRecapitulationProcessItemQuery("a");
                var iq = new ItemQuery("b");
                var unitq = new AppStandardReferenceItemQuery("c");
                var inmq = new ItemProductNonMedicQuery("d");

                query.Select
                    (
                        query,
                        iq.ItemName.As("refToItem_ItemName"),
                        unitq.ItemName.As("refToAppStandardReferenceItem_ItemUnit")
                    );
                query.InnerJoin(iq).On(iq.ItemID == query.ItemID);
                query.InnerJoin(unitq).On(unitq.ItemID == query.SRItemUnit &&
                                          unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                query.InnerJoin(inmq).On(inmq.ItemID == query.ItemID);
                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(iq.ItemName.Ascending);
                coll.Load(query);

                Session["collLaundryRecapitulationProcessItem" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collLaundryRecapitulationProcessItem" + Request.UserHostName] = value;
            }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible
                                                             ? GridCommandItemDisplay.Top
                                                             : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            LaundryRecapitulationProcessItems = null; //Reset Record Detail
            grdItem.DataSource = LaundryRecapitulationProcessItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private LaundryRecapitulationProcessItem FindItem(String itemId)
        {
            LaundryRecapitulationProcessItemCollection coll = LaundryRecapitulationProcessItems;
            LaundryRecapitulationProcessItem retEntity = null;
            foreach (LaundryRecapitulationProcessItem rec in coll)
            {
                if (rec.ItemID.Equals(itemId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = LaundryRecapitulationProcessItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemId =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][LaundryRecapitulationProcessItemMetadata.ColumnNames.ItemID]);
            LaundryRecapitulationProcessItem entity = FindItem(itemId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String itemId =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][LaundryRecapitulationProcessItemMetadata.ColumnNames.ItemID]);
            LaundryRecapitulationProcessItem entity = FindItem(itemId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            LaundryRecapitulationProcessItem entity = LaundryRecapitulationProcessItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(LaundryRecapitulationProcessItem entity, GridCommandEventArgs e)
        {
            var userControl = (LaundryRecapitulationItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.Qty = userControl.Qty;
                entity.QtyRewashing = userControl.QtyRewashing;
                entity.SRItemUnit = userControl.SRItemUnit;
                entity.ItemUnit = userControl.ItemUnitName;
            }
        }

        #endregion

        private string GetNewProcessNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.LaundryRecapNo);

            return _autoNumber.LastCompleteNumber;
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (sourceControl is RadGrid)
            {
                if (eventArgument == "rebind")
                    grdItem.Rebind();
            }
        }

        protected void btnResetItem_Click(object sender, EventArgs e)
        {
            if (LaundryRecapitulationProcessItems.Count > 0)
                LaundryRecapitulationProcessItems.MarkAllAsDeleted();
            grdItem.DataSource = LaundryRecapitulationProcessItems;
            grdItem.DataBind();
        }
    }
}