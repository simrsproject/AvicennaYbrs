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
    public partial class PackagingItemDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber, _autoItemNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "PackagingItemSearch.aspx";
            UrlPageList = "PackagingItemList.aspx";

            ProgramID = AppConstant.Program.CssdPackagingItem;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                grdItem.Columns.FindByUniqueName("IsDtt").Visible = AppSession.Parameter.IsCssdUsingDttTerm;
                grdItem.Columns.FindByUniqueName("DttDescription").Visible = !AppSession.Parameter.IsCssdUsingDttTerm;
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new CssdPackaging());

            txtTransactionNo.Text = GetNewTransactionNo();
            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtTransactionTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            //var entity = new CssdPackaging();
            //if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            //{
            //    entity.MarkAsDeleted();

            //    SaveEntity(entity);
            //}
            //else
            //    args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (CssdPackagingItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new CssdPackaging();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (CssdPackagingItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new CssdPackaging();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text.Trim());
            auditLogFilter.TableName = "CssdPackaging";
        }

        #endregion

        #region ToolBar Menu Support
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new CssdPackaging();
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

        private bool IsApprovedOrVoid(CssdPackaging entity, ValidateArgs args)
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
            var entity = new CssdPackaging();
            if (parameters.Length > 0)
            {
                var tno = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(tno);
            }
            else
                entity.LoadByPrimaryKey(txtTransactionNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var packaging = (CssdPackaging)entity;
            txtTransactionNo.Text = packaging.TransactionNo;

            if (packaging.TransactionDate.HasValue)
            {
                txtTransactionDate.SelectedDate = packaging.TransactionDate;
                txtTransactionTime.Text = packaging.TransactionTime;
            }
            txtNotes.Text = packaging.Notes;

            PopulateItemGrid();

            ViewState["IsApproved"] = packaging.IsApproved ?? false;
            ViewState["IsVoid"] = packaging.IsVoid ?? false;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new CssdPackaging();
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

            DataTable dtb = (new CssdPackagingItemCollection()).GetItemIsPackagingItemStatus(txtTransactionNo.Text);
            if (dtb.Rows.Count > 0)
            {
                var msg = string.Empty;
                var msgPhase = "Packaging";

                foreach (DataRow row in dtb.Rows)
                {
                    if ((bool)row["IsPackaging"] == true)
                    {
                        if (msg == string.Empty)
                            msg = "R#: " + row["ReceivedNo"].ToString() + " (" + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + ")";
                        else
                            msg = msg + "; " + "R#: " + row["ReceivedNo"].ToString() + " (" + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + ")";
                    }
                }

                if (msg != string.Empty)
                {
                    args.MessageText = "The transaction " + msgPhase + " process for the following data has already been performed. Approval is not allowed. [" + msg + "]";
                    args.IsCancel = true;
                    return;
                }
                else
                {
                    if (AppSession.Application.IsMenuCssdFeasibilityTestActive)
                    {
                        msgPhase = "Feasibility Test";
                        foreach (DataRow row in dtb.Rows)
                        {
                            if ((bool)row["IsFeasibilityTest"] == false)
                            {
                                if (msg == string.Empty)
                                    msg = "R#: " + row["ReceivedNo"].ToString() + " (" + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + ")";
                                else
                                    msg = msg + "; R#: " + row["ReceivedNo"].ToString() + " (" + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + ")";
                            }
                        }
                    }
                    else if (AppSession.Application.IsMenuCssdDecontaminationActive)
                    {
                        msgPhase = "Decontamination - Drying";
                        foreach (DataRow row in dtb.Rows)
                        {
                            if (row["SRDecontaminationPhase"].ToString() != "3")
                            {
                                if (msg == string.Empty)
                                    msg = "R#: " + row["ReceivedNo"].ToString() + " (" + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + ")";
                                else
                                    msg = msg + "; R#: " + row["ReceivedNo"].ToString() + " (" + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + ")";
                            }
                        }
                    }

                    if (msg != string.Empty)
                    {
                        args.MessageText = "This following data has not yet passed the " + msgPhase + " phase. Approval is not allowed. [" + msg + "]";
                        args.IsCancel = true;
                        return;
                    }
                }
            }

            SetApproval(entity, true, args);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new CssdPackaging();
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

            DataTable dtb = (new CssdPackagingItemCollection()).GetItemIsPackagingItemStatus(txtTransactionNo.Text);
            if (dtb.Rows.Count > 0)
            {
                var msg1 = string.Empty; //Ultrasound
                var msg2 = string.Empty; //Sterilization

                foreach (DataRow row in dtb.Rows)
                {
                    if ((bool)row["IsSterilization"])
                    {
                        if (msg2 == string.Empty)
                            msg2 = "R#: " + row["ReceivedNo"].ToString() + " (" + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + ")";
                        else
                            msg2 = msg2 + "; " + "R#: " + row["ReceivedNo"].ToString() + " (" + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + ")";
                    }
                    else
                    {
                        DataTable dtb2 = (new CssdSterileItemsUltrasoundItemCollection()).GetItemProceed(row["ReceivedNo"].ToString(), row["ReceivedSeqNo"].ToString());
                        if (dtb2.Rows.Count > 0)
                        {
                            foreach (DataRow row2 in dtb2.Rows)
                            {
                                if (msg1 == string.Empty)
                                    msg1 = "R#: " + row2["ReceivedNo"].ToString() + " (" + row2["ItemID"].ToString() + " - " + row2["ItemName"].ToString() + ")";
                                else
                                    msg1 = msg1 + "; " + "R#: " + row2["ReceivedNo"].ToString() + " (" + row2["ItemID"].ToString() + " - " + row2["ItemName"].ToString() + ")";
                            }
                        }
                        else
                        {
                            DataTable dtb3 = (new CssdSterilizationProcessItemCollection()).GetItemProceed(row["ReceivedNo"].ToString(), row["ReceivedSeqNo"].ToString());
                            if (dtb3.Rows.Count > 0)
                            {
                                foreach (DataRow row3 in dtb3.Rows)
                                {
                                    if (msg2 == string.Empty)
                                        msg2 = "R#: " + row3["ReceivedNo"].ToString() + " (" + row3["ItemID"].ToString() + " - " + row3["ItemName"].ToString() + ")";
                                    else
                                        msg2 = msg2 + "; " + "R#: " + row3["ReceivedNo"].ToString() + " (" + row3["ItemID"].ToString() + " - " + row3["ItemName"].ToString() + ")";
                                }
                            }
                        }
                    }
                }

                if (msg1 != string.Empty)
                {
                    args.MessageText = "This transaction has entered into the next phase (Ultrasound). Unapproval is not allowed. [" + msg1 + "]";
                    args.IsCancel = true;
                    return;
                }

                if (msg2 != string.Empty)
                {
                    args.MessageText = "This transaction has entered into the next phase (Sterilization). Unapproval is not allowed. [" + msg2+ "]";
                    args.IsCancel = true;
                    return;
                }
            }

            SetApproval(entity, false, args);
        }

        private void SetApproval(CssdPackaging entity, bool isApproval, ValidateArgs args)
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

                if (isApproval)
                {
                    foreach (var item in CssdPackagingItems)
                    {
                        if (string.IsNullOrEmpty(item.CssdItemNo))
                        {
                            item.CssdItemNo = GetNewCssdItemNo();
                            // save autonumber immediately to decrease time gap between create and save
                            _autoItemNumber.Save();
                        }

                        var received = new CssdSterileItemsReceivedItem();
                        if (received.LoadByPrimaryKey(item.ReceivedNo, item.ReceivedSeqNo))
                        {
                            received.SRCssdPhase = "6";
                            received.IsPackaging = true;

                            received.ExpiredDate = item.ExpiredDate;
                            received.ReuseTo = item.ReuseTo;
                            received.IsNeedUltrasound = item.IsNeedUltrasound;
                            received.IsDtt = item.IsDtt;

                            received.Save();
                        }
                    }

                    CssdPackagingItems.Save();
                }
                else
                {
                    foreach (var item in CssdPackagingItems)
                    {
                        var received = new CssdSterileItemsReceivedItem();
                        if (received.LoadByPrimaryKey(item.ReceivedNo, item.ReceivedSeqNo))
                        {
                            if (AppSession.Application.IsMenuCssdFeasibilityTestActive)
                                received.SRCssdPhase = "5";
                            else if (AppSession.Application.IsMenuCssdDecontaminationActive)
                                received.SRCssdPhase = "4";
                            else received.SRCssdPhase = "1";

                            received.IsPackaging = null;
                            received.Save();
                        }
                    }
                }

                var balances = new CssdItemBalanceCollection();
                CssdItemBalance.PrepareItemBalancePackaging(entity.TransactionNo, AppSession.Parameter.ServiceUnitCssdID, AppSession.UserLogin.UserID, 
                    AppSession.Application.IsMenuCssdFeasibilityTestActive, AppSession.Application.IsMenuCssdDecontaminationActive, isApproval, ref balances);
                if (balances != null)
                    balances.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new CssdPackaging();
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
            var entity = new CssdPackaging();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private void SetVoid(CssdPackaging entity, bool isVoid)
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

        private void SetEntityValue(CssdPackaging entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtTransactionNo.Text = GetNewTransactionNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.TransactionTime = txtTransactionTime.TextWithLiterals;
            entity.Notes = txtNotes.Text;

            entity.IsApproved = false;
            entity.IsVoid = false;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (var item in CssdPackagingItems)
            {
                item.TransactionNo = txtTransactionNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(CssdPackaging entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                CssdPackagingItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new CssdPackagingQuery();
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

            var entity = new CssdPackaging();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function of CssdPackagingItem

        private CssdPackagingItemCollection CssdPackagingItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collCssdPackagingItem" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((CssdPackagingItemCollection)(obj));
                    }
                }

                var coll = new CssdPackagingItemCollection();
                var query = new CssdPackagingItemQuery("a");
                var received = new CssdSterileItemsReceivedItemQuery("b");
                var iq = new ItemQuery("c");
                var unitq = new AppStandardReferenceItemQuery("d");

                query.Select
                    (
                        query,
                        @"<CAST((CAST(a.CssdItemNo AS INT)) AS VARCHAR) AS 'refTo_CssdItemNo'>",
                        received.ItemID.As("refToCssdSterileItemsReceivedItem_ItemID"),
                        iq.ItemName.As("refToCssdItem_ItemName"),
                        received.Qty.As("refToCssdSterileItemsReceivedItem_Qty"),
                        unitq.ItemName.As("refToAppStandardReferenceItem_CssdItemUnit"),
                        received.Notes.As("refToCssdSterileItemsReceivedItem_Notes"),
                        @"<CASE WHEN a.IsDtt = 0 THEN 'Low' ELSE 'High' END AS 'refTo_IsDttDescription'>"
                    );
                query.InnerJoin(received).On(received.ReceivedNo == query.ReceivedNo && received.ReceivedSeqNo == query.ReceivedSeqNo);
                query.InnerJoin(iq).On(iq.ItemID == received.ItemID);
                query.InnerJoin(unitq).On(unitq.ItemID == received.SRCssdItemUnit && unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(query.SeqNo.Ascending);
                coll.Load(query);

                Session["collCssdPackagingItem" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collCssdPackagingItem" + Request.UserHostName] = value;
            }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            CssdPackagingItems = null; //Reset Record Detail
            grdItem.DataSource = CssdPackagingItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private CssdPackagingItem FindItem(String seqNo)
        {
            CssdPackagingItemCollection coll = CssdPackagingItems;
            CssdPackagingItem retEntity = null;
            foreach (CssdPackagingItem rec in coll)
            {
                if (rec.SeqNo.Equals(seqNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = CssdPackagingItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String seqNo =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][CssdPackagingItemMetadata.ColumnNames.SeqNo]);
            CssdPackagingItem entity = FindItem(seqNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String seqNo =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][CssdPackagingItemMetadata.ColumnNames.SeqNo]);
            CssdPackagingItem entity = FindItem(seqNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        private void SetEntityValue(CssdPackagingItem entity, GridCommandEventArgs e)
        {
            var userControl = (PackagingItemDetailItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                if (userControl.ExpiredDate == null)
                    entity.str.ExpiredDate = string.Empty;
                else
                    entity.ExpiredDate = userControl.ExpiredDate;
                entity.ReuseTo = userControl.ReuseTo;
                entity.IsNeedUltrasound = userControl.IsNeedUltrasound;
                entity.IsDtt = userControl.IsDtt;
                entity.DttDescription = userControl.IsDtt ? "High" : "Low";
            }
        }

        #endregion

        #region Combobox
        
        #endregion

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.CssdPackagingNo);

            return _autoNumber.LastCompleteNumber;
        }

        private string GetNewCssdItemNo()
        {
            _autoItemNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.CssdItemNo);

            return _autoItemNumber.LastCompleteNumber;
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
            if (CssdPackagingItems.Count > 0)
                CssdPackagingItems.MarkAllAsDeleted();
            grdItem.DataSource = CssdPackagingItems;
            grdItem.DataBind();
        }
    }
}