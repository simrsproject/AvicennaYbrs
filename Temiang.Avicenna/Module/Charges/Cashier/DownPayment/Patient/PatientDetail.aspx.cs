using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using System.Data;
using Telerik.Web.UI;
using System.Text;

namespace Temiang.Avicenna.Module.Charges.DownPayment
{
    public partial class PatientDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;
        private bool _isPowerUser;
        private string CashManagementNo
        {
            get
            {
                var cmno = string.Empty;

                var usr = new AppUser();
                if (usr.LoadByPrimaryKey(AppSession.UserLogin.UserID))
                    cmno = string.IsNullOrEmpty(usr.CashManagementNo)? string.Empty : usr.CashManagementNo;
                
                return cmno;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "PatientSearch.aspx?type=" + Request.QueryString["type"];
            UrlPageList = "PatientList.aspx?type=" + Request.QueryString["type"];

            if (Request.QueryString["type"] == "deposit")
            {
                ProgramID = AppConstant.Program.PatientDepositReceive;
                lblNumber.Text = "Payment No";
                lblDateTime.Text = "Payment Date Time";
            }
            else
            {
                ProgramID = AppConstant.Program.PatientDepositReturn;
                lblNumber.Text = "Return No";
                lblDateTime.Text = "Return Date Time";
            }
            Session["ProgramID"] = ProgramID;
            WindowSearch.Height = 300;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRPatientDepositType, AppEnum.StandardReference.PatientDepositType);
                TransPaymentItems = null;

                //var cstext1 = new StringBuilder();
                //cstext1.Append("<script type=text/javascript> __doPostBack('ctl00$ContentPlaceHolder1$grdTransPaymentItem$ctl00$ctl02$ctl00$AddNewRecordButton','') </script>");

                //Page.ClientScript.RegisterStartupScript(GetType(), "OpenAddNewRecordGrid", cstext1.ToString());

            }

            var grUsr = new AppUserUserGroupQuery("a");
            var gr = new AppUserGroupQuery("b");
            grUsr.InnerJoin(gr).On(grUsr.UserGroupID == gr.UserGroupID && grUsr.UserID == AppSession.UserLogin.UserID &&
                                   gr.IsEditAble == true);
            _isPowerUser = grUsr.LoadDataTable().Rows.Count > 0 || AppSession.Parameter.IsBypassCashierAuthorization;
        }

        public override bool OnGetStatusMenuEdit()
        {
            return true;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !(bool)ViewState["IsApproved"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnMenuNewClick()
        {
            txtPaymentDate.SelectedDate = DateTime.Now.Date;
            txtPaymentTime.Text = DateTime.Now.ToString("HH:mm");

            ViewState["IsVoid"] = false;
            ViewState["IsApproved"] = false;
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new TransPaymentPatient();
            if (entity.LoadByPrimaryKey(txtPaymentNo.Text))
            {
                if (entity.LastUpdateByUserID != AppSession.UserLogin.UserID && !_isPowerUser)
                {
                    var usr = new AppUser();
                    usr.LoadByPrimaryKey(entity.LastUpdateByUserID);

                    args.MessageText = "You don't have authorization to edit this transaction. This data belong to: " +
                                       usr.UserName + ". Please contact your supervision.";
                    args.IsCancel = true;
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemTransPaymentItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new TransPaymentPatient();
            if (parameters.Length > 0)
            {
                String paymentNo = parameters[0];
                if (!parameters[0].Equals(string.Empty)) entity.LoadByPrimaryKey(paymentNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtPaymentNo.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(Temiang.Dal.Core.esEntity entity)
        {
            var transPayment = (TransPaymentPatient)entity;
            txtPaymentNo.Text = transPayment.PaymentNo;
            txtPaymentDate.SelectedDate = transPayment.PaymentDate;
            txtPaymentTime.Text = transPayment.PaymentTime;

            if (transPayment != null)
            {
                var patient = new Patient();
                patient.LoadByPrimaryKey(transPayment.PatientID);

                var dtbPatient = (new PatientCollection()).PatientRegisterAble(patient.MedicalNo, string.Empty, string.Empty, string.Empty, 5);
                cboPatient.DataSource = dtbPatient;
                cboPatient.DataBind();
            }

            ViewState["IsVoid"] = transPayment.IsVoid ?? false;
            ViewState["IsApproved"] = transPayment.IsApproved ?? false;

            txtNotes.Text = transPayment.Notes;
            cboSRPatientDepositType.SelectedValue = transPayment.SRPatientDepositType;

            PopulateTransPaymentItemGrid();
        }

        protected void cboPatient_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["MedicalNo"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["PatientName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PatientID"].ToString();
        }

        protected void cboPatient_ItemsRequested(object o, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Text)) return;

            var dtbPatient = (new PatientCollection()).PatientRegisterAble(e.Text, string.Empty, string.Empty, string.Empty, 20);
            cboPatient.DataSource = dtbPatient;
            cboPatient.DataBind();
        }

        private void SetEntityValue(TransPaymentPatient entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtPaymentNo.Text = GetNewPaymentNo();

                //save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }

            entity.PaymentNo = txtPaymentNo.Text;
            if (ProgramID == AppConstant.Program.PatientDepositReceive)
                entity.TransactionCode = TransactionCode.DownPayment;
            else
                entity.TransactionCode = TransactionCode.DownPaymentReturn;
            entity.PaymentDate = txtPaymentDate.SelectedDate;
            entity.PaymentTime = txtPaymentTime.Text;
            entity.PatientID = cboPatient.SelectedValue;
            entity.IsVoid = false;
            entity.IsApproved = false;
            entity.Notes = txtNotes.Text;
            entity.SRPatientDepositType = cboSRPatientDepositType.SelectedValue;

            if (entity.es.IsAdded)
            {
                entity.CashManagementNo = CashManagementNo;
            }

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            foreach (var item in TransPaymentItems)
            {
                item.PaymentNo = entity.PaymentNo;

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void SaveEntity(TransPaymentPatient entity)
        {
            using (var trans = new Temiang.Dal.Interfaces.esTransactionScope())
            {
                entity.Save();
                TransPaymentItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new TransPaymentPatientQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.PaymentNo > txtPaymentNo.Text);
                if (ProgramID == AppConstant.Program.PatientDepositReceive)
                    que.Where(que.TransactionCode == TransactionCode.DownPayment);
                else
                    que.Where(que.TransactionCode == TransactionCode.DownPaymentReturn);
                que.OrderBy(que.PaymentNo.Ascending);
            }
            else
            {
                que.Where(que.PaymentNo < txtPaymentNo.Text);
                if (ProgramID == AppConstant.Program.PatientDepositReceive)
                    que.Where(que.TransactionCode == TransactionCode.DownPayment);
                else
                    que.Where(que.TransactionCode == TransactionCode.DownPaymentReturn);
                que.OrderBy(que.PaymentNo.Descending);
            }

            var entity = new TransPaymentPatient();
            if (entity.Load(que)) OnPopulateEntryControl(entity);
        }

        private string GetNewPaymentNo()
        {
            _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.PatientDepositNo);
            return _autoNumber.LastCompleteNumber;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new TransPaymentPatient();
            if (entity.LoadByPrimaryKey(txtPaymentNo.Text))
            {
                TransPaymentItems.MarkAllAsDeleted();
                TransPaymentItems.Save();

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
            if (AppSession.Parameter.IsUsingCashManagement && string.IsNullOrEmpty(CashManagementNo))
            {
                args.MessageText = "Chasier checkin required.";
                args.IsCancel = true;
                return;
            }

            if (TransPaymentItems.Count == 0)
            {
                args.MessageText = "Down payment detail is not defined.";
                args.IsCancel = true;
                return;
            }

            var entity = new TransPaymentPatient();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (TransPaymentItems.Count == 0)
            {
                args.MessageText = "Down payment detail is not defined.";
                args.IsCancel = true;
                return;
            }

            var entity = new TransPaymentPatient();
            if (entity.LoadByPrimaryKey(txtPaymentNo.Text))
            {
                SetEntityValue(entity);
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
            auditLogFilter.PrimaryKeyData = string.Format("PaymentNo='{0}'", txtPaymentNo.Text.Trim());
            auditLogFilter.TableName = "TransPaymentPatient";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            if (TransPaymentItems.Count == 0)
            {
                args.MessageText = "Down payment detail is not defined.";
                args.IsCancel = true;
                return;
            }

            var entity = new TransPaymentPatient();
            if (!entity.LoadByPrimaryKey(txtPaymentNo.Text))
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

            if (entity.LastUpdateByUserID != AppSession.UserLogin.UserID && !_isPowerUser)
            {
                var usr = new AppUser();
                usr.LoadByPrimaryKey(entity.LastUpdateByUserID);

                args.MessageText = "You don't have authorization to approved this transaction. This data belong to: " +
                                   usr.UserName + ". Please contact your supervision.";
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, true);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new TransPaymentPatient();
            if (!entity.LoadByPrimaryKey(txtPaymentNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            //sering ada double journal untuk void
            if (entity.IsVoid ?? false)
            {
                args.MessageText = "Unapproval failed, payment has been voided";
                args.IsCancel = true;
                return;
            }

            if (!string.IsNullOrEmpty(entity.ReferenceNo))
            {
                args.MessageText = "Unapproval failed, deposit is already referenced";
                args.IsCancel = true;
                return;
            }

            if (entity.LastUpdateByUserID != AppSession.UserLogin.UserID && !_isPowerUser)
            {
                var usr = new AppUser();
                usr.LoadByPrimaryKey(entity.LastUpdateByUserID);

                args.MessageText = "You don't have authorization to Un-Approved this transaction. This data belong to: " +
                                   usr.UserName + ". Please contact your supervision.";
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, false);
        }

        private void SetApproval(TransPaymentPatient entity, bool isApprove)
        {
            entity.IsApproved = isApprove;
            entity.IsVoid = !isApprove;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;

            using (var trans = new Temiang.Dal.Interfaces.esTransactionScope())
            {
                entity.Save();

                if (ProgramID == AppConstant.Program.PatientDepositReturn)
                {
                    foreach (var item in TransPaymentItems)
                    {
                        if (string.IsNullOrEmpty(item.ReferenceNo) && string.IsNullOrEmpty(item.ReferenceSequenceNo)) continue;
                        var deposit = new TransPaymentPatient();
                        if (deposit.LoadByPrimaryKey(item.ReferenceNo))
                        {
                            if (isApprove) deposit.ReferenceNo = entity.PaymentNo;
                            else deposit.str.ReferenceNo = string.Empty;
                            deposit.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            deposit.LastUpdateDateTime = DateTime.Now;
                            deposit.Save();
                        }
                    }
                }

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new TransPaymentPatient();
            if (!entity.LoadByPrimaryKey(txtPaymentNo.Text))
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

            if (entity.LastUpdateByUserID != AppSession.UserLogin.UserID && !_isPowerUser)
            {
                var usr = new AppUser();
                usr.LoadByPrimaryKey(entity.LastUpdateByUserID);

                args.MessageText = "You don't have authorization to void this transaction. This data belong to: " +
                                   usr.UserName + ". Please contact your supervision.";
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, true);
        }

        private void SetVoid(TransPaymentPatient entity, bool isVoid)
        {
            entity.IsApproved = !isVoid;
            entity.IsVoid = isVoid;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;
            using (var trans = new Temiang.Dal.Interfaces.esTransactionScope())
            {
                entity.Save();

                foreach (var item in TransPaymentItems)
                {
                    if (string.IsNullOrEmpty(item.ReferenceNo) && string.IsNullOrEmpty(item.ReferenceSequenceNo)) continue;
                    var deposit = new TransPaymentPatient();
                    if (deposit.LoadByPrimaryKey(item.ReferenceNo))
                    {
                        deposit.str.ReferenceNo = string.Empty;
                        deposit.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        deposit.LastUpdateDateTime = DateTime.Now;
                        deposit.Save();
                    }
                }

                trans.Complete();
            }
        }

        private TransPaymentPatientItemCollection TransPaymentItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["DownPayment:collTransPaymentItem" + Request.UserHostName];
                    if (obj != null) return ((TransPaymentPatientItemCollection)(obj));
                }

                var coll = new TransPaymentPatientItemCollection();

                var query = new TransPaymentPatientItemQuery("a");
                var srQuery = new AppStandardReferenceItemQuery("b");
                var srQuery2 = new AppStandardReferenceItemQuery("c");

                query.Select
                    (
                    query,
                    srQuery2.ItemName.As("refToAppStandardReferenceItem_PaymentType"),
                    srQuery.ItemName.As("refToAppStandardReferenceItem_PaymentMethod"),
                    query.Amount
                    );
                query.InnerJoin(srQuery2).On(query.SRPaymentType == srQuery2.ItemID);
                query.LeftJoin(srQuery).On(query.SRPaymentMethod == srQuery.ItemID);
                query.Where(query.PaymentNo == txtPaymentNo.Text);
                query.OrderBy(query.SequenceNo.Ascending);

                coll.Load(query);

                Session["DownPayment:collTransPaymentItem" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["DownPayment:collTransPaymentItem" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemTransPaymentItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdTransPaymentItem.Columns[0].Visible = isVisible && ProgramID == AppConstant.Program.PatientDepositReceive;
            grdTransPaymentItem.Columns[grdTransPaymentItem.Columns.Count - 1].Visible = isVisible;

            grdTransPaymentItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            if (isVisible)
            {
                grdTransPaymentItem.DataSource = null;
                grdTransPaymentItem.Rebind();
            }
        }

        private void PopulateTransPaymentItemGrid()
        {
            //Display Data Detail
            TransPaymentItems = null; //Reset Record Detail
            grdTransPaymentItem.DataSource = TransPaymentItems; //Requery
            grdTransPaymentItem.MasterTableView.IsItemInserted = false;
            grdTransPaymentItem.MasterTableView.ClearEditItems();
            grdTransPaymentItem.DataBind();
        }

        protected void grdTransPaymentItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdTransPaymentItem.DataSource = TransPaymentItems;
        }

        protected void grdTransPaymentItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            var entity = FindTransPaymentItem((string)editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][TransPaymentPatientItemMetadata.ColumnNames.SequenceNo]);

            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdTransPaymentItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var sequenceNo = item.OwnerTableView.DataKeyValues[item.ItemIndex][TransPaymentPatientItemMetadata.ColumnNames.SequenceNo].ToString();
            var entity = FindTransPaymentItem(sequenceNo);

            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdTransPaymentItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = TransPaymentItems.AddNew();

            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdTransPaymentItem.Rebind();
        }

        private TransPaymentPatientItem FindTransPaymentItem(String sequenceNo)
        {
            var coll = TransPaymentItems;
            return coll.FirstOrDefault(rec => rec.SequenceNo.Equals(sequenceNo));
        }

        private void SetEntityValue(TransPaymentPatientItem entity, GridCommandEventArgs e)
        {
            var userControl = (ItemDownPayment)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.PaymentNo = txtPaymentNo.Text;
                entity.SequenceNo = userControl.SequenceNo;
                entity.SRPaymentType = userControl.SRPaymentType;
                entity.PaymentTypeName = userControl.PaymentTypeName;
                entity.SRPaymentMethod = userControl.SRPaymentMethod;
                entity.PaymentMethodName = userControl.PaymentMethodName;
                entity.str.SRCardProvider = userControl.SRCardProvider;
                entity.str.SRCardType = userControl.SRCardType;
                entity.str.EDCMachineID = userControl.EDCMachineID;
                entity.CardHolderName = userControl.CardHolderName;
                entity.CardFeeAmount = userControl.CardFeeAmount;
                entity.BankID = userControl.BankID;
                entity.Amount = userControl.Amount;
                entity.CardNo = userControl.CardNo;
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
            ToolBarMenuEdit.Enabled = !(bool)ViewState["IsApproved"];
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (!(sourceControl is RadGrid)) return;

            if (eventArgument == "deposit") grdTransPaymentItem.Rebind();
        }
    }
}
