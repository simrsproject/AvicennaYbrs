using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class FeasibilityTestDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "FeasibilityTestSearch.aspx";
            UrlPageList = "FeasibilityTestList.aspx";

            ProgramID = AppConstant.Program.CssdFeasibilityTest;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                CssdSterileItemsReceivedItemDetails = null;
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
            OnPopulateEntryControl(new CssdFeasibilityTest());

            txtFeasibilityTestNo.Text = GetNewTransactionNo();
            txtFeasibilityTestDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtFeasibilityTestTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");

            btnGetPickList.Enabled = true;
            btnResetItem.Enabled = true;

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            //var entity = new CssdFeasibilityTest();
            //if (entity.LoadByPrimaryKey(txtFeasibilityTestNo.Text))
            //{
            //    entity.MarkAsDeleted();

            //    SaveEntity(entity);
            //}
            //else
            //    args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (CssdFeasibilityTestItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new CssdFeasibilityTest();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (CssdFeasibilityTestItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new CssdFeasibilityTest();
            if (entity.LoadByPrimaryKey(txtFeasibilityTestNo.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("FeasibilityTestNo='{0}'", txtFeasibilityTestNo.Text.Trim());
            auditLogFilter.TableName = "CssdFeasibilityTest";
        }

        #endregion

        #region ToolBar Menu Support
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new CssdFeasibilityTest();
            if (entity.LoadByPrimaryKey(txtFeasibilityTestNo.Text))
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

        private bool IsApprovedOrVoid(CssdFeasibilityTest entity, ValidateArgs args)
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
            printJobParameters.AddNew("p_FeasibilityTestNo", txtFeasibilityTestNo.Text);
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
            var entity = new CssdFeasibilityTest();
            if (parameters.Length > 0)
            {
                var tno = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(tno);
            }
            else
                entity.LoadByPrimaryKey(txtFeasibilityTestNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var e = (CssdFeasibilityTest)entity;

            txtFeasibilityTestNo.Text = e.FeasibilityTestNo;
            txtFeasibilityTestDate.SelectedDate = e.FeasibilityTestDate;
            txtFeasibilityTestTime.Text = e.FeasibilityTestTime;
            txtNotes.Text = e.Notes;

            btnGetPickList.Enabled = false;
            btnResetItem.Enabled = false;

            PopulateItemGrid();

            ViewState["IsApproved"] = e.IsApproved ?? false;
            ViewState["IsVoid"] = e.IsVoid ?? false;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new CssdFeasibilityTest();
            if (!entity.LoadByPrimaryKey(txtFeasibilityTestNo.Text))
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

            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return;
            }

            DataTable dtb = (new CssdFeasibilityTestItemCollection()).GetItemIsFeasibilityTestStatus(txtFeasibilityTestNo.Text);
            if (dtb.Rows.Count > 0)
            {
                var msg = string.Empty;
                var msgPhase = "Feasibility Test";

                foreach (DataRow row in dtb.Rows)
                {
                    if ((bool)row["IsFeasibilityTest"] == true)
                    {
                        if (msg == string.Empty)
                            msg = "R#: " + row["ReceivedNo"].ToString() + " *" + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + ")";
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
            var entity = new CssdFeasibilityTest();
            if (!entity.LoadByPrimaryKey(txtFeasibilityTestNo.Text))
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

            DataTable dtb = (new CssdFeasibilityTestItemCollection()).GetItemIsFeasibilityTestStatus(txtFeasibilityTestNo.Text);
            if (dtb.Rows.Count > 0)
            {
                if (AppSession.Application.IsMenuCssdPackagingActive)
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
                        else
                        {
                            DataTable dtb2 = (new CssdPackagingItemCollection()).GetItemProceed(row["ReceivedNo"].ToString(), row["ReceivedSeqNo"].ToString());
                            if (dtb2.Rows.Count > 0)
                            {
                                foreach (DataRow row2 in dtb2.Rows)
                                {
                                    if (msg == string.Empty)
                                        msg = "R#: " + row2["ReceivedNo"].ToString() + " (" + row2["ItemID"].ToString() + " - " + row2["ItemName"].ToString() + ")";
                                    else
                                        msg = msg + "; " + "R#: " + row2["ReceivedNo"].ToString() + " (" + row2["ItemID"].ToString() + " - " + row2["ItemName"].ToString() + ")";
                                }
                            }
                        }
                    }

                    if (msg != string.Empty)
                    {
                        args.MessageText = "This transaction has entered into the next phase (" + msgPhase + "). Unapproval is not allowed. [" + msg + "]";
                        args.IsCancel = true;
                        return;
                    }
                }
                else
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
                        args.MessageText = "This transaction has entered into the next phase (Sterilization). Unapproval is not allowed. [" + msg2 + "]";
                        args.IsCancel = true;
                        return;
                    }
                }
            }

            SetApproval(entity, false, args);
        }

        private void SetApproval(CssdFeasibilityTest entity, bool isApproval, ValidateArgs args)
        {
            using (var trans = new esTransactionScope())
            {
                entity.IsApproved = isApproval;
                if (isApproval)
                {
                    entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                    entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();

                    foreach (var i in CssdFeasibilityTestItems)
                    {
                        var received = new CssdSterileItemsReceivedItem();
                        if (received.LoadByPrimaryKey(i.ReceivedNo, i.ReceivedSeqNo))
                        {
                            var item = new Item();
                            item.LoadByPrimaryKey(received.ItemID);

                            received.SRCssdPhase = "5";
                            received.IsFeasibilityTest = true;
                            received.IsFeasibilityTestPassed = (i.IsFeasibilityTestPassed ?? false);
                            received.IsBrokenInstrument = (i.IsBrokenInstrument ?? false);
                            received.QtyReplacements = (i.QtyReplacements ?? 0);

                            var receivedDts = new CssdSterileItemsReceivedItemDetailCollection();
                            receivedDts.Query.Where(receivedDts.Query.ReceivedNo == received.ReceivedNo, receivedDts.Query.ReceivedSeqNo == received.ReceivedSeqNo);
                            receivedDts.LoadAll();
                            if (receivedDts.Count > 0)
                            {
                                var isBrokenInstrument = false;
                                decimal qtyReplacements = 0;
                                foreach (var dt in receivedDts)
                                {
                                    if (dt.IsBrokenInstrument == true)
                                        isBrokenInstrument = true;
                                    qtyReplacements += (dt.QtyReplacements ?? 0);
                                }

                                received.IsBrokenInstrumentDetail = isBrokenInstrument;
                                received.QtyReplacementsDetail = qtyReplacements;
                            }
                            else
                            {
                                received.IsBrokenInstrumentDetail = false;
                                received.QtyReplacementsDetail = 0;
                            }

                            received.Save();
                        }
                    }
                }
                else
                {
                    entity.ApprovedByUserID = null;
                    entity.ApprovedDateTime = null;

                    foreach (var i in CssdFeasibilityTestItems)
                    {
                        var received = new CssdSterileItemsReceivedItem();
                        if (received.LoadByPrimaryKey(i.ReceivedNo, i.ReceivedSeqNo))
                        {
                            if (AppSession.Application.IsMenuCssdDecontaminationActive)
                                received.SRCssdPhase = "4";
                            else
                                received.SRCssdPhase = "1";
                            received.IsFeasibilityTest = null;
                            received.IsFeasibilityTestPassed = null;
                            received.IsBrokenInstrument = null;
                            received.QtyReplacements = null;
                            received.IsBrokenInstrumentDetail = null;
                            received.QtyReplacementsDetail = null;

                            received.Save();
                        }
                    }
                }

                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.Save();

                var balances = new CssdItemBalanceCollection();
                CssdItemBalance.PrepareItemBalanceFeasibilityTest(entity.FeasibilityTestNo, AppSession.Parameter.ServiceUnitCssdID, AppSession.UserLogin.UserID, AppSession.Application.IsMenuCssdDecontaminationActive, isApproval, ref balances);
                if (balances != null)
                    balances.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new CssdFeasibilityTest();
            if (!entity.LoadByPrimaryKey(txtFeasibilityTestNo.Text))
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
            //var entity = new CssdFeasibilityTest();
            //if (!entity.LoadByPrimaryKey(txtDecontaminationNo.Text))
            //{
            //    args.MessageText = AppConstant.Message.RecordNotExist;
            //    args.IsCancel = true;
            //    return;
            //}

            //SetVoid(entity, false);
        }

        private void SetVoid(CssdFeasibilityTest entity, bool isVoid)
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

        private void SetEntityValue(CssdFeasibilityTest entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtFeasibilityTestNo.Text = GetNewTransactionNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }

            entity.FeasibilityTestNo = txtFeasibilityTestNo.Text;
            entity.FeasibilityTestDate = txtFeasibilityTestDate.SelectedDate;
            entity.FeasibilityTestTime = txtFeasibilityTestTime.TextWithLiterals;
            entity.Notes = txtNotes.Text;
            entity.IsApproved = false;
            entity.IsVoid = false;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (var item in CssdFeasibilityTestItems)
            {
                item.FeasibilityTestNo = txtFeasibilityTestNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(CssdFeasibilityTest entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                CssdFeasibilityTestItems.Save();
                CssdSterileItemsReceivedItemDetails.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new CssdFeasibilityTestQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.FeasibilityTestNo > txtFeasibilityTestNo.Text);
                que.OrderBy(que.FeasibilityTestNo.Ascending);
            }
            else
            {
                que.Where(que.FeasibilityTestNo < txtFeasibilityTestNo.Text);
                que.OrderBy(que.FeasibilityTestNo.Descending);
            }

            var entity = new CssdFeasibilityTest();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Method & Event TextChanged

        #endregion

        #region Record Detail Method Function of CssdFeasibilityTestItems

        private CssdFeasibilityTestItemCollection CssdFeasibilityTestItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collCssdFeasibilityTestItem" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((CssdFeasibilityTestItemCollection)(obj));
                    }
                }

                var coll = new CssdFeasibilityTestItemCollection();

                var query = new CssdFeasibilityTestItemQuery("a");
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
                        received.Qty.As("refToCssdSterileItemsReceivedItem_Qty"),

                        unitq.ItemName.As("refToAppStandardReferenceItem_CssdItemUnit"),
                        received.Notes.As("refToCssdSterileItemsReceivedItem_Notes"),

                        @"<ISNULL((SELECT SUM(x.QtyReplacements) AS QtyReplacementsDetail FROM CssdSterileItemsReceivedItemDetail AS x WHERE x.ReceivedNo = a.ReceivedNo AND x.ReceivedSeqNo = a.ReceivedSeqNo), 0) AS 'refToCssdSterileItemsReceivedItemDetail_QtyReplacements'>",
                        @"<ISNULL((SELECT TOP 1 CAST(1 AS BIT) AS IsBrokenInstrumentDetail FROM CssdSterileItemsReceivedItemDetail AS x WHERE x.ReceivedNo = a.ReceivedNo AND x.ReceivedSeqNo = a.ReceivedSeqNo AND x.IsBrokenInstrument = 1), CAST(0 AS BIT)) AS 'refToCssdSterileItemsReceivedItemDetail_IsBrokenInstrument'>"

                    );
                query.InnerJoin(received).On(received.ReceivedNo == query.ReceivedNo && received.ReceivedSeqNo == query.ReceivedSeqNo);
                query.InnerJoin(iq).On(iq.ItemID == received.ItemID);
                query.InnerJoin(unitq).On(unitq.ItemID == received.SRCssdItemUnit && unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                query.Where(query.FeasibilityTestNo == txtFeasibilityTestNo.Text);
                query.OrderBy(query.FeasibilityTestSeqNo.Ascending);
                coll.Load(query);

                Session["collCssdFeasibilityTestItem" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collCssdFeasibilityTestItem" + Request.UserHostName] = value;
            }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);

            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns.FindByUniqueName("listDetailEdit").Visible = isVisible;
            grdItem.Columns.FindByUniqueName("listDetailView").Visible = !isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            CssdFeasibilityTestItems = null; //Reset Record Detail
            grdItem.DataSource = CssdFeasibilityTestItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();

            CssdSterileItemsReceivedItemDetails = null;
            var det = CssdSterileItemsReceivedItemDetails;
        }

        private CssdFeasibilityTestItem FindItem(String seqNo)
        {
            CssdFeasibilityTestItemCollection coll = CssdFeasibilityTestItems;
            CssdFeasibilityTestItem retEntity = null;
            foreach (CssdFeasibilityTestItem rec in coll)
            {
                if (rec.ReceivedNo.Equals(seqNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = CssdFeasibilityTestItems;
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String seqNo =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][CssdFeasibilityTestItemMetadata.ColumnNames.FeasibilityTestSeqNo]);
            CssdFeasibilityTestItem entity = FindItem(seqNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String seqNo =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][CssdFeasibilityTestItemMetadata.ColumnNames.FeasibilityTestSeqNo]);
            CssdFeasibilityTestItem entity = FindItem(seqNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        private void SetEntityValue(CssdFeasibilityTestItem entity, GridCommandEventArgs e)
        {
            var userControl = (FeasibilityTestItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.IsFeasibilityTestPassed = userControl.IsFeasibilityTestPassed;
                entity.IsBrokenInstrument = userControl.IsBrokenInstrument;
                entity.QtyReplacements = userControl.QtyReplacements;
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
                    var obj = Session["collCssdSterileItemsReceivedItemDetail" + Request.UserHostName + "_fts"];
                    if (obj != null)
                        return ((CssdSterileItemsReceivedItemDetailCollection)(obj));
                }

                var coll = new CssdSterileItemsReceivedItemDetailCollection();
                var query = new CssdSterileItemsReceivedItemDetailQuery("a");
                var fts = new CssdFeasibilityTestItemQuery("aa");
                var item = new ItemQuery("b");
                var itemDetail = new VwItemProductMedicNonMedicQuery("c");

                query.Select(
                    query,
                    @"<ISNULL(a.IsBrokenInstrument, 0) AS 'refTo_IsBrokenInstrument'>",
                    @"<ISNULL(a.QtyReplacements, 0) AS 'refTo_QtyReplacements'>",
                    item.ItemName.As("refToItem_ItemName"),
                    itemDetail.SRItemUnit.As("refToAppStdRef_ItemUnit")
                    );
                query.InnerJoin(fts).On(fts.ReceivedNo == query.ReceivedNo && fts.ReceivedSeqNo == query.ReceivedSeqNo);
                query.InnerJoin(item).On(query.ItemDetailID == item.ItemID);
                query.InnerJoin(itemDetail).On(query.ItemDetailID == itemDetail.ItemID);

                query.Where(fts.FeasibilityTestNo == txtFeasibilityTestNo.Text);

                coll.Load(query);

                Session["collCssdSterileItemsReceivedItemDetail" + Request.UserHostName + "_fts"] = coll;
                return coll;
            }
            set { Session["collCssdSterileItemsReceivedItemDetail" + Request.UserHostName + "_fts"] = value; }
        }
        #endregion

        #region Combobox

        #endregion

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.CssdFeasibilityTestNo);

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
            if (CssdFeasibilityTestItems.Count > 0)
                CssdFeasibilityTestItems.MarkAllAsDeleted();
            grdItem.DataSource = CssdFeasibilityTestItems;
            grdItem.DataBind();
        }
    }
}