using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class DecontaminationDetail : BasePageDetail
    {
        private string DPhase
        {
            get
            {
                return Request.QueryString["p"];
            }
        }

        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "DecontaminationSearch.aspx?p=" + DPhase;
            UrlPageList = "DecontaminationList.aspx?p=" + DPhase;

            ProgramID = DPhase == "1" ? AppConstant.Program.CssdDecontaminationImmersion : (DPhase == "2" ? AppConstant.Program.CssdDecontaminationAbstersion : AppConstant.Program.CssdDecontaminationDrying);

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRAbstersionType, AppEnum.StandardReference.AbstersionType);
                StandardReference.InitializeIncludeSpace(cboSRDecontaminationPhase, AppEnum.StandardReference.DecontaminationPhase);

                trSRAbstersionType.Visible = DPhase == "2";
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
            OnPopulateEntryControl(new CssdDecontamination());

            txtDecontaminationNo.Text = GetNewDecontaminationNo();
            txtDecontaminationDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtDecontaminationTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");

            btnGetPickList.Enabled = true;
            btnResetItem.Enabled = true;

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            //var entity = new CssdDecontamination();
            //if (entity.LoadByPrimaryKey(txtDecontaminationNo.Text))
            //{
            //    entity.MarkAsDeleted();

            //    SaveEntity(entity);
            //}
            //else
            //    args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (CssdDecontaminationItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new CssdDecontamination();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (CssdDecontaminationItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new CssdDecontamination();
            if (entity.LoadByPrimaryKey(txtDecontaminationNo.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("DecontaminationNo='{0}'", txtDecontaminationNo.Text.Trim());
            auditLogFilter.TableName = "CssdDecontamination";
        }

        #endregion

        #region ToolBar Menu Support
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new CssdDecontamination();
            if (entity.LoadByPrimaryKey(txtDecontaminationNo.Text))
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

        private bool IsApprovedOrVoid(CssdDecontamination entity, ValidateArgs args)
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
            printJobParameters.AddNew("p_DecontaminationNo", txtDecontaminationNo.Text);
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
            var entity = new CssdDecontamination();
            if (parameters.Length > 0)
            {
                var tno = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(tno);
            }
            else
                entity.LoadByPrimaryKey(txtDecontaminationNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var decontamination = (CssdDecontamination)entity;

            txtDecontaminationNo.Text = decontamination.DecontaminationNo;
            txtDecontaminationDate.SelectedDate = decontamination.DecontaminationDate;
            txtDecontaminationTime.Text = decontamination.DecontaminationTime;

            if (!string.IsNullOrEmpty(decontamination.SRDecontaminationPhase))
                cboSRDecontaminationPhase.SelectedValue = decontamination.SRDecontaminationPhase;
            else
            {
                cboSRDecontaminationPhase.SelectedValue = DPhase;
            }
            if (!string.IsNullOrEmpty(decontamination.SRAbstersionType))
                cboSRAbstersionType.SelectedValue = decontamination.SRAbstersionType;
            else
            {
                cboSRAbstersionType.Text = string.Empty;
                cboSRAbstersionType.SelectedValue = string.Empty;
            }
            txtNotes.Text = decontamination.Notes;

            btnGetPickList.Enabled = false;
            btnResetItem.Enabled = false;

            PopulateItemGrid();

            ViewState["IsApproved"] = decontamination.IsApproved ?? false;
            ViewState["IsVoid"] = decontamination.IsVoid ?? false;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new CssdDecontamination();
            if (!entity.LoadByPrimaryKey(txtDecontaminationNo.Text))
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

            DataTable dtb = (new CssdDecontaminationItemCollection()).GetItemDecontaminationStatus(txtDecontaminationNo.Text);
            if (dtb.Rows.Count > 0)
            {
                var msg = string.Empty;
                var msgPhase = string.Empty;

                if (DPhase == "1")
                {
                    msgPhase = "Immersion";
                    foreach (DataRow row in dtb.Rows)
                    {
                        if (row["SRDecontaminationPhase"].ToString() != string.Empty)
                        {
                            if (msg == string.Empty)
                                msg = "R#: " + row["ReceivedNo"].ToString() + " (" + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + ")";
                            else
                                msg = msg + "; " + "R#: " + row["ReceivedNo"].ToString() + " (" + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + ")";
                        }
                    }
                }
                else if (DPhase == "2")
                {
                    msgPhase = "Abstersion";
                    foreach (DataRow row in dtb.Rows)
                    {
                        if (row["SRDecontaminationPhase"].ToString() != "1")
                        {
                            if (msg == string.Empty)
                                msg = "R#: " + row["ReceivedNo"].ToString() + " (" + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + ")";
                            else
                                msg = msg + "; " + "R#: " + row["ReceivedNo"].ToString() + " (" + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + ")";
                        }
                    }
                }
                else if (DPhase == "3")
                {
                    msgPhase = "Drying";
                    foreach (DataRow row in dtb.Rows)
                    {
                        if (row["SRDecontaminationPhase"].ToString() != "2")
                        {
                            if (msg == string.Empty)
                                msg = "R#: " + row["ReceivedNo"].ToString() + " (" + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + ")";
                            else
                                msg = msg + "; " + "R#: " + row["ReceivedNo"].ToString() + ": " + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + ")";
                        }
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
                    if (DPhase == "2")
                    {
                        msgPhase = "Immersion";
                        foreach (DataRow row in dtb.Rows)
                        {
                            if (row["SRDecontaminationPhase"].ToString() == string.Empty)
                            {
                                if (msg == string.Empty)
                                    msg = "R#: " + row["ReceivedNo"].ToString() + " (" + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + ")";
                                else
                                    msg = msg + "; R#: " + row["ReceivedNo"].ToString() + " (" + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + ")";
                            }
                        }
                    }
                    else if (DPhase == "3")
                    {
                        msgPhase = "Abstersion";
                        foreach (DataRow row in dtb.Rows)
                        {
                            if (row["SRDecontaminationPhase"].ToString() == "1")
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
            var entity = new CssdDecontamination();
            if (!entity.LoadByPrimaryKey(txtDecontaminationNo.Text))
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

            DataTable dtb = (new CssdDecontaminationItemCollection()).GetItemDecontaminationStatus(txtDecontaminationNo.Text);
            if (dtb.Rows.Count > 0)
            {
                var msg = string.Empty;
                var msgPhase = string.Empty;

                if (DPhase == "1")
                {
                    msgPhase = "Abstersion";
                    foreach (DataRow row in dtb.Rows)
                    {
                        if (row["SRDecontaminationPhase"].ToString() != "1" )
                        {
                            if (msg == string.Empty)
                                msg = "R#: " + row["ReceivedNo"].ToString() + " (" + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + ")";
                            else
                                msg = msg + "; " + "R#: " + row["ReceivedNo"].ToString() + " (" + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + ")";
                        }
                        else
                        {
                            DataTable dtb2 = (new CssdDecontaminationItemCollection()).GetItemProceed(row["ReceivedNo"].ToString(), row["ReceivedSeqNo"].ToString(), "2");
                            if (dtb2.Rows.Count > 0)
                            {
                                foreach (DataRow row2 in dtb2.Rows)
                                {
                                    if (msg == string.Empty)
                                        msg = "R#: " + row2["ReceivedNo"].ToString() + " (" + row2["ItemID"].ToString() + " - " + row2["ItemName"].ToString() + ")";
                                    else
                                        msg = msg + "; " + "R#: " + row2["ReceivedNo"].ToString() + " (" + row2["ItemID"].ToString() + " - " + row2["ItemName"].ToString() +")";
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
                else if (DPhase == "2")
                {
                    msgPhase = "Drying";
                    foreach (DataRow row in dtb.Rows)
                    {
                        if (row["SRDecontaminationPhase"].ToString() != "2")
                        {
                            if (msg == string.Empty)
                                msg = "R#: " + row["ReceivedNo"].ToString() + " (" + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + ")";
                            else
                                msg = msg + "; " + "R#: " + row["ReceivedNo"].ToString() + " (" + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + ")";
                        }
                        else
                        {
                            DataTable dtb2 = (new CssdDecontaminationItemCollection()).GetItemProceed(row["ReceivedNo"].ToString(), row["ReceivedSeqNo"].ToString(), "3");
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
                else if (DPhase == "3")
                {
                    if (AppSession.Application.IsMenuCssdFeasibilityTestActive)
                    {
                        msgPhase = "Feasibility Test";

                        foreach (DataRow row in dtb.Rows)
                        {
                            if ((bool)row["IsFeasibilityTest"])
                            {
                                if (msg == string.Empty)
                                    msg = "R#: " + row["ReceivedNo"].ToString() + " (" + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + ")";
                                else
                                    msg = msg + "; " + "R#: " + row["ReceivedNo"].ToString() + " (" + row["ItemID"].ToString() + " - " + row["ItemName"].ToString() + ")";
                            }
                            else
                            {
                                DataTable dtb2 = (new CssdFeasibilityTestItemCollection()).GetItemProceed(row["ReceivedNo"].ToString(), row["ReceivedSeqNo"].ToString());
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
                    else if (AppSession.Application.IsMenuCssdPackagingActive)
                    {
                        msgPhase = "Packaging";

                        foreach (DataRow row in dtb.Rows)
                        {
                            if ((bool)row["IsPackaging"])
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
                        msgPhase = "Ultrasound";

                        var msg2 = string.Empty;
                        var msgPhase2 = "Sterilization";

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
                                        if (msg == string.Empty)
                                            msg = "R#: " + row2["ReceivedNo"].ToString() + " (" + row2["ItemID"].ToString() + " - " + row2["ItemName"].ToString() + ")";
                                        else
                                            msg = msg + "; " + "R#: " + row2["ReceivedNo"].ToString() + " (" + row2["ItemID"].ToString() + " - " + row2["ItemName"].ToString() + ")";
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

                        if (msg != string.Empty)
                        {
                            args.MessageText = "This transaction has entered into the next phase (" + msgPhase + "). Unapproval is not allowed. [" + msg + "]";
                            args.IsCancel = true;
                            return;
                        }

                        if (msg2 != string.Empty)
                        {
                            args.MessageText = "This transaction has entered into the next phase (" + msgPhase2 + "). Unapproval is not allowed. [" + msg2 + "]";
                            args.IsCancel = true;
                            return;
                        }
                    }
                }
            }

            SetApproval(entity, false, args);
        }

        private void SetApproval(CssdDecontamination entity, bool isApproval, ValidateArgs args)
        {
            using (var trans = new esTransactionScope())
            {
                entity.IsApproved = isApproval;
                if (isApproval)
                {
                    entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                    entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();

                    foreach (var i in CssdDecontaminationItems)
                    {
                        var received = new CssdSterileItemsReceivedItem();
                        if (received.LoadByPrimaryKey(i.ReceivedNo, i.ReceivedSeqNo))
                        {
                            received.SRCssdPhase = (DPhase == "1" ? "2" : (DPhase == "2" ? "3" : "4"));
                            if (DPhase == "1")
                                received.IsDecontamination = true;

                            received.SRDecontaminationPhase = DPhase;
                            received.Save();
                        }
                    }
                }
                else
                {
                    entity.ApprovedByUserID = null;
                    entity.ApprovedDateTime = null;

                    foreach (var i in CssdDecontaminationItems)
                    {
                        var received = new CssdSterileItemsReceivedItem();
                        if (received.LoadByPrimaryKey(i.ReceivedNo, i.ReceivedSeqNo))
                        {
                            if (DPhase == "1")
                            {
                                received.SRCssdPhase = "1";
                                received.IsDecontamination = false;
                                received.SRDecontaminationPhase = string.Empty;
                            }
                            else if (DPhase == "2")
                            {
                                received.SRCssdPhase = "2";
                                received.SRDecontaminationPhase = "1";
                            }
                            else if (DPhase == "3")
                            {
                                received.SRCssdPhase = "3";
                                received.SRDecontaminationPhase = "2";
                            }
                            received.Save();
                        }
                    }
                }

                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.Save();

                var balances = new CssdItemBalanceCollection();
                CssdItemBalance.PrepareItemBalanceDecontamination(entity.DecontaminationNo, AppSession.Parameter.ServiceUnitCssdID, AppSession.UserLogin.UserID, DPhase, isApproval, ref balances);
                if (balances != null)
                    balances.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new CssdDecontamination();
            if (!entity.LoadByPrimaryKey(txtDecontaminationNo.Text))
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
            //var entity = new CssdDecontamination();
            //if (!entity.LoadByPrimaryKey(txtDecontaminationNo.Text))
            //{
            //    args.MessageText = AppConstant.Message.RecordNotExist;
            //    args.IsCancel = true;
            //    return;
            //}

            //SetVoid(entity, false);
        }

        private void SetVoid(CssdDecontamination entity, bool isVoid)
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

        private void SetEntityValue(CssdDecontamination entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtDecontaminationNo.Text = GetNewDecontaminationNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }

            entity.DecontaminationNo = txtDecontaminationNo.Text;
            entity.DecontaminationDate = txtDecontaminationDate.SelectedDate;
            entity.DecontaminationTime = txtDecontaminationTime.TextWithLiterals;
            entity.SRDecontaminationPhase = DPhase;
            entity.SRAbstersionType = cboSRAbstersionType.SelectedValue;
            entity.Notes = txtNotes.Text;
            entity.IsApproved = false;
            entity.IsVoid = false;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (var item in CssdDecontaminationItems)
            {
                item.DecontaminationNo = txtDecontaminationNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(CssdDecontamination entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                CssdDecontaminationItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new CssdDecontaminationQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            que.Where(que.SRDecontaminationPhase == DPhase);
            if (isNextRecord)
            {
                que.Where(que.DecontaminationNo > txtDecontaminationNo.Text);
                que.OrderBy(que.DecontaminationNo.Ascending);
            }
            else
            {
                que.Where(que.DecontaminationNo < txtDecontaminationNo.Text);
                que.OrderBy(que.DecontaminationNo.Descending);
            }

            var entity = new CssdDecontamination();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Method & Event TextChanged

        #endregion

        #region Record Detail Method Function of CssdDecontaminationItems

        private CssdDecontaminationItemCollection CssdDecontaminationItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collCssdDecontaminationItem" + Request.UserHostName + "_" + DPhase];
                    if (obj != null)
                    {
                        return ((CssdDecontaminationItemCollection)(obj));
                    }
                }

                var coll = new CssdDecontaminationItemCollection();

                var query = new CssdDecontaminationItemQuery("a");
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
                        received.Notes.As("refToCssdSterileItemsReceivedItem_Notes")

                    );
                query.InnerJoin(received).On(received.ReceivedNo == query.ReceivedNo &&
                                         received.ReceivedSeqNo == query.ReceivedSeqNo);
                query.InnerJoin(iq).On(iq.ItemID == received.ItemID);
                query.InnerJoin(unitq).On(unitq.ItemID == received.SRCssdItemUnit &&
                                          unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                query.Where(query.DecontaminationNo == txtDecontaminationNo.Text);
                query.OrderBy(query.DecontaminationSeqNo.Ascending);
                coll.Load(query);

                Session["collCssdDecontaminationItem" + Request.UserHostName + "_" + DPhase] = coll;
                return coll;
            }
            set
            {
                Session["collCssdDecontaminationItem" + Request.UserHostName + "_" + DPhase] = value;
            }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns.FindByUniqueName("listDetailView").Visible = !isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            CssdDecontaminationItems = null; //Reset Record Detail
            grdItem.DataSource = CssdDecontaminationItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private CssdDecontaminationItem FindItem(String seqNo)
        {
            CssdDecontaminationItemCollection coll = CssdDecontaminationItems;
            CssdDecontaminationItem retEntity = null;
            foreach (CssdDecontaminationItem rec in coll)
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
            grdItem.DataSource = CssdDecontaminationItems;
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String seqNo =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][CssdDecontaminationItemMetadata.ColumnNames.DecontaminationSeqNo]);
            CssdDecontaminationItem entity = FindItem(seqNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        #endregion

        #region Combobox

        #endregion

        private string GetNewDecontaminationNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date,
                DPhase == "1" ? AppEnum.AutoNumber.CssdDeconImmersionNo : (DPhase == "2" ? AppEnum.AutoNumber.CssdDeconAbstersionNo : AppEnum.AutoNumber.CssdDeconDryingNo));

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
            if (CssdDecontaminationItems.Count > 0)
                CssdDecontaminationItems.MarkAllAsDeleted();
            grdItem.DataSource = CssdDecontaminationItems;
            grdItem.DataBind();
        }
    }
}