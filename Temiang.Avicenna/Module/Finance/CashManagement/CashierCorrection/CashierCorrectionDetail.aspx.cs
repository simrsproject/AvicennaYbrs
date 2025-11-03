using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI;

namespace Temiang.Avicenna.Module.Finance.CashManagement
{
    public partial class CashierCorrectionDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private void PopulateNewPaymentCorrectionNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New) return;

            _autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.PaymentCorrectionNo);
            txtPaymentCorrectionNo.Text = _autoNumber.LastCompleteNumber;
            
        }

        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "CashierCorrectionSearch.aspx";
            UrlPageList = "CashierCorrectionList.aspx";

            this.WindowSearch.Height = 400;
            ProgramID = AppConstant.Program.CASHIER_CORRECTION;


            if (!IsPostBack)
            {
                
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (sourceControl is RadGrid && eventArgument == "rebind")
            {
                ((RadGrid)sourceControl).Rebind();
            }
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID,
                                               PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("PaymentCorrectionNo", txtPaymentCorrectionNo.Text);
        }

        protected override void OnMenuEditClick()
        {

        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            TransPaymentCorrection entity = new TransPaymentCorrection();
            if (entity.LoadByPrimaryKey(txtPaymentCorrectionNo.Text))
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
            // do approve
            var entity = new TransPaymentCorrection();
            if (!entity.LoadByPrimaryKey(txtPaymentCorrectionNo.Text))
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

            // validation detail
            var tpicColl = new TransPaymentItemCorrectionCollection();
            tpicColl.Query.Where(tpicColl.Query.PaymentCorrectionNo == entity.PaymentCorrectionNo);
            if (!tpicColl.LoadAll()) {
                args.MessageText = "Empty detail can not be approved.";
                args.IsCancel = true;
                return;
            }

            foreach (var tpic in tpicColl) {
                if (string.IsNullOrEmpty(tpic.SRCardProvider)) {
                    args.MessageText = "Card provider must be set";
                    args.IsCancel = true;
                    return;
                }
                if (string.IsNullOrEmpty(tpic.SRCardType))
                {
                    args.MessageText = "Card type must be set";
                    args.IsCancel = true;
                    return;
                }
                if (string.IsNullOrEmpty(tpic.EDCMachineID))
                {
                    args.MessageText = "EDC machine must be set";
                    args.IsCancel = true;
                    return;
                }
            }

            entity.Approve(AppSession.UserLogin.UserID);
            Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.VoucherEntryDetail
                .JournalPaymentCashierCorrection(0, entity.PaymentCorrectionNo);
        }

        public override bool OnGetStatusMenuUnApprovalEnabled()
        {
            return !PostingStatus.IsUnApproveDisabledIfPerClosed(txtPaymentCorrectionDate.SelectedDate.Value);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new TransPaymentCorrection();
            if (!entity.LoadByPrimaryKey(txtPaymentCorrectionNo.Text))
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

            entity.UnApprove(AppSession.UserLogin.UserID);
            // jurnal

            var tpcItemsColl = new TransPaymentItemCorrectionCollection();
            tpcItemsColl.Query.Where(tpcItemsColl.Query.PaymentCorrectionNo == entity.PaymentCorrectionNo);
            tpcItemsColl.LoadAll();

            int? journalId = JournalTransactions.AddNewCashierCorrectionJournalUnApproval(
                                JournalType.CashierCorrection, entity, tpcItemsColl, AppSession.UserLogin.UserID, 0);
        }
        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            //(new Invoices()).Void(txtInvoiceNo.Text, AppSession.UserLogin.UserID);
            var entity = new TransPaymentCorrection();
            if (!entity.LoadByPrimaryKey(txtPaymentCorrectionNo.Text))
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

            entity.Void(AppSession.UserLogin.UserID);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            //(new Invoices()).UnVoid(txtInvoiceNo.Text, AppSession.UserLogin.UserID);
        }

        private bool IsApprovedOrVoid(TransPaymentCorrection entity, ValidateArgs args)
        {
            if (entity.IsApproved != null && entity.IsApproved.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            if (entity.IsVoid != null && entity.IsVoid.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }
            return true;
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new TransPaymentCorrection());
            PopulateNewPaymentCorrectionNo();

            txtPaymentCorrectionDate.SelectedDate = DateTime.Now;
            btnPick.Enabled = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            TransPaymentCorrection entity = new TransPaymentCorrection();
            if (entity.LoadByPrimaryKey(txtPaymentCorrectionNo.Text))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            PopulateNewPaymentCorrectionNo();
            var entity = new TransPaymentCorrection();
            if (entity.LoadByPrimaryKey(txtPaymentCorrectionNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new TransPaymentCorrection();
            entity.AddNew();
            SetEntityValue(entity);
            if (TransPaymentCorrectionItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            TransPaymentCorrection entity = new TransPaymentCorrection();
            if (entity.LoadByPrimaryKey(txtPaymentCorrectionNo.Text))
            {
                SetEntityValue(entity);
                if (TransPaymentCorrectionItems.Count == 0)
                {
                    args.MessageText = AppConstant.Message.RecordDetailEmpty;
                    args.IsCancel = true;
                    return;
                }
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
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
            auditLogFilter.PrimaryKeyData = string.Format("PaymentCorrectionNo='{0}'", txtPaymentCorrectionNo.Text.Trim());
            auditLogFilter.TableName = "TransPaymentCorrection";
        }


        #endregion

        #region ToolBar Menu Support
        public override bool OnGetStatusMenuEdit()
        {
            return txtPaymentCorrectionNo.Text != string.Empty;
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
            RefreshCommandItemGrid(oldVal, newVal);

            btnPick.Enabled = newVal != AppEnum.DataMode.Read;

            if (newVal != AppEnum.DataMode.New)
            {
                if (oldVal == AppEnum.DataMode.New)
                {

                }
            }
            else
            {

            }
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            TransPaymentCorrection entity = new TransPaymentCorrection();
            if (parameters.Length > 0)
            {
                String PaymentCorrectionNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(PaymentCorrectionNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtPaymentCorrectionNo.Text);
            }

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var tpCorrection = (TransPaymentCorrection)entity;
            txtPaymentCorrectionNo.Text = tpCorrection.PaymentCorrectionNo;
            txtPaymentCorrectionDate.SelectedDate = tpCorrection.PaymentCorrectionDate;

            ViewState["IsVoid"] = tpCorrection.IsVoid ?? false;
            ViewState["IsApproved"] = tpCorrection.IsApproved ?? false;

            //Display Data Detail
            PopulateGridDetail();
            btnPick.Enabled = false;
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(TransPaymentCorrection entity)
        {
            entity.PaymentCorrectionNo = txtPaymentCorrectionNo.Text;
            entity.PaymentCorrectionDate = txtPaymentCorrectionDate.SelectedDate;

            entity.IsApproved = false;
            entity.IsVoid = false;

            var d = (new DateTime()).NowAtSqlServer();

            if (entity.es.IsAdded) {
                entity.CreatedByUserID = AppSession.UserLogin.UserID;
                entity.CreatedDateTime = d;
            }

            //Last Update Status
            if (entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = d;
            }

            //Update Detil
            foreach (TransPaymentItemCorrection item in TransPaymentCorrectionItems)
            {
                item.PaymentCorrectionNo = entity.PaymentCorrectionNo;

                if (item.es.IsAdded)
                {
                    item.CreatedByUserID = AppSession.UserLogin.UserID;
                    item.CreatedDateTime = d;
                }

                //Last Update Status
                if (item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = d;
                }
            }
        }

        private void SaveEntity(TransPaymentCorrection entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                TransPaymentCorrectionItems.Save();

                if (DataModeCurrent == AppEnum.DataMode.New)
                    _autoNumber.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            TransPaymentItemCorrectionQuery que = new TransPaymentItemCorrectionQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.PaymentCorrectionNo > txtPaymentCorrectionNo.Text);
                que.OrderBy(que.PaymentCorrectionNo.Ascending);
            }
            else
            {
                que.Where(que.PaymentCorrectionNo < txtPaymentCorrectionNo.Text);
                que.OrderBy(que.PaymentCorrectionNo.Descending);
            }
            TransPaymentItemCorrection entity = new TransPaymentItemCorrection();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Record Detail Method Function

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPaymentCorrectionItem.Columns[0].Visible = isVisible;
            grdPaymentCorrectionItem.Columns[grdPaymentCorrectionItem.Columns.Count - 1].Visible = isVisible;

            //grdInvoicesItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                TransPaymentCorrectionItems = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdPaymentCorrectionItem.Rebind();
        }

        private TransPaymentItemCorrectionCollection TransPaymentCorrectionItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["colTransPaymentCorrectionItems" + Request.UserHostName];
                    if (obj != null)
                        return ((TransPaymentItemCorrectionCollection)(obj));
                }

                var coll = new TransPaymentItemCorrectionCollection();
                var tpic = new TransPaymentItemCorrectionQuery("tpic");
                var tpi = new TransPaymentItemQuery("tpi");
                var tp = new TransPaymentQuery("tp");
                var reg = new RegistrationQuery("reg");
                var pat = new PatientQuery("pat");

                var pm = new PaymentMethodQuery("pm");

                var cpo = new AppStandardReferenceItemQuery("cpo");
                var cto = new AppStandardReferenceItemQuery("cto");
                var edco = new EDCMachineQuery("edco");

                var cpc = new AppStandardReferenceItemQuery("cpc");
                var ctc = new AppStandardReferenceItemQuery("ctc");
                var edcc = new EDCMachineQuery("edcc");


                tpic.InnerJoin(tpi).On(tpic.PaymentNo == tpi.PaymentNo && tpic.SequenceNo == tpi.SequenceNo)
                    .InnerJoin(tp).On(tpic.PaymentNo == tp.PaymentNo)
                    .InnerJoin(reg).On(tp.RegistrationNo == reg.RegistrationNo)
                    .InnerJoin(pat).On(reg.PatientID == pat.PatientID)

                    .InnerJoin(pm).On(tpi.SRPaymentType == pm.SRPaymentTypeID && tpi.SRPaymentMethod == pm.SRPaymentMethodID)

                    .LeftJoin(cpo).On(cpo.StandardReferenceID == "CardProvider" && tpi.SRCardProvider == cpo.ItemID)
                    .LeftJoin(cto).On(cto.StandardReferenceID == "CardType" && tpi.SRCardType == cto.ItemID)
                    .LeftJoin(edco).On(tpi.EDCMachineID == edco.EDCMachineID)

                    .LeftJoin(cpc).On(cpc.StandardReferenceID == "CardProvider" && tpic.SRCardProvider == cpc.ItemID)
                    .LeftJoin(ctc).On(ctc.StandardReferenceID == "CardType" && tpic.SRCardType == ctc.ItemID)
                    .LeftJoin(edcc).On(tpic.EDCMachineID == edcc.EDCMachineID)

                    .Select(
                        tpic,
                        reg.RegistrationNo.As("refToRegistration_RegistrationNo"), 
                        pat.PatientName.As("refToPatient_PatientName"),
                        pm.PaymentMethodName.As("refToPM_PaymentMethodOName"),
                        cpo.ItemName.As("refToCP_CardProviderOName"),
                        cto.ItemName.As("refToCT_CardTypeOName"),
                        edco.EDCMachineName.As("refToEDC_EDCMachineOName"),
                        cpc.ItemName.As("refToCP_CardProviderCName"),
                        ctc.ItemName.As("refToCT_CardTypeCName"),
                        edcc.EDCMachineName.As("refToEDC_EDCMachineCName"),
                        tpi.Amount.As("refToPayment_Amount")
                    )

                    .Where(tpic.PaymentCorrectionNo == txtPaymentCorrectionNo.Text);

                coll.Load(tpic);

                Session["colTransPaymentCorrectionItems" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["colTransPaymentCorrectionItems" + Request.UserHostName] = value; }
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            TransPaymentCorrectionItems = null; //Reset Record Detail
            grdPaymentCorrectionItem.DataSource = TransPaymentCorrectionItems;
            grdPaymentCorrectionItem.MasterTableView.IsItemInserted = false;
            grdPaymentCorrectionItem.MasterTableView.ClearEditItems();
            grdPaymentCorrectionItem.DataBind();
        }

        protected void grdPaymentCorrectionItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPaymentCorrectionItem.DataSource = TransPaymentCorrectionItems;
        }

        protected void grdPaymentCorrectionItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            string PayCorrectionNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][TransPaymentItemCorrectionMetadata.ColumnNames.PaymentCorrectionNo]);
            string PayNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][TransPaymentItemCorrectionMetadata.ColumnNames.PaymentNo]);
            string SeqNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][TransPaymentItemCorrectionMetadata.ColumnNames.SequenceNo]);
            TransPaymentItemCorrection entity = FindItem(PayCorrectionNo, PayNo, SeqNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        private TransPaymentItemCorrection FindItem(string PayCorrectionNo, string PayNo, string SeqNo)
        {
            TransPaymentItemCorrectionCollection coll = TransPaymentCorrectionItems;
            return coll.Where(x => x.PaymentCorrectionNo == PayCorrectionNo &&
                x.PaymentNo == PayNo && x.SequenceNo == SeqNo).FirstOrDefault();
        }

        protected void grdPaymentCorrectionItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            string PayCorrectionNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][TransPaymentItemCorrectionMetadata.ColumnNames.PaymentCorrectionNo]);
            string PayNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][TransPaymentItemCorrectionMetadata.ColumnNames.PaymentNo]);
            string SeqNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][TransPaymentItemCorrectionMetadata.ColumnNames.SequenceNo]);
            TransPaymentItemCorrection entity = FindItem(PayCorrectionNo, PayNo, SeqNo);
            if (entity != null)
            {
                entity.MarkAsDeleted();
            }
        }

        private void SetEntityValue(TransPaymentItemCorrection entity, GridCommandEventArgs e)
        {
            CashCorrectionDetailEdit userControl = (CashCorrectionDetailEdit)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SRCardProvider = userControl.SRCardProvider;
                entity.CardProviderCName = userControl.SRCardProviderName;
                entity.SRCardType = userControl.SRCardType;
                entity.CardTypeCName = userControl.SRCardTypeName;
                entity.EDCMachineID = userControl.EDCMachineID;
                entity.EDCMachineCName = userControl.EDCMachineName;
                entity.CardFeeAmount = userControl.CardFeeAmount;
            }
        }

        #endregion

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
            ToolBarMenuEdit.Enabled = !(bool)ViewState["IsApproved"];
        }
    }
}
