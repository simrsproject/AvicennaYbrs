using System;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using System.Collections.Generic;
using System.Linq;

namespace Temiang.Avicenna.Module.Charges.Cashier
{
    public partial class ClosingVisiteDownPaymentDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "##";
            UrlPageList = "ClosingVisiteDownPaymentList.aspx";

            ProgramID = AppConstant.Program.ClosingVisiteDownPayment;

            if (!IsPostBack)
            {
                ClosingVisiteDownPaymentItems = null;
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItem, grdItem);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("ClosingNo", txtClosingNo.Text);
            printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new ClosingVisiteDownPayment();
            if (entity.LoadByPrimaryKey(txtClosingNo.Text))
            {
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ClosingVisiteDownPayment());

            txtClosingNo.Text = GetNewClosingNo();
            txtClosingDate.SelectedDate = (new DateTime()).NowAtSqlServer();

            Patient patient = new Patient();
            patient.LoadByPrimaryKey(Request.QueryString["patid"]);

            txtMedicalNo.Text = patient.MedicalNo;
            txtPatientName.Text = patient.PatientName;
            
            var std = new AppStandardReferenceItem();
            txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.str.SRSalutation) ? std.ItemName : string.Empty;

            ViewState["IsVoid"] = false;
            ViewState["IsApproved"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            ClosingVisiteDownPayment entity = new ClosingVisiteDownPayment();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            ClosingVisiteDownPayment entity = new ClosingVisiteDownPayment();
            if (entity.LoadByPrimaryKey(txtClosingNo.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("ClosingNo='{0}'", txtClosingNo.Text.Trim());
            auditLogFilter.TableName = "ClosingVisiteDownPayment";
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            ClosingVisiteDownPayment entity = new ClosingVisiteDownPayment();
            if (!entity.LoadByPrimaryKey(txtClosingNo.Text))
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

            if (ClosingVisiteDownPaymentItems.Count == 0)
            {
                args.MessageText = "Down Payment detail is not defined.";
                args.IsCancel = true;
                return;
            }

            // cek DP retur atau tidak
            var htp = new TransPaymentQuery("a");
            var dtp = new TransPaymentItemQuery("b");
            htp.InnerJoin(dtp).On(
                htp.PaymentNo == dtp.PaymentNo &&
                htp.PatientID == entity.PatientID &&
                htp.IsApproved == true &&
                htp.TransactionCode == BusinessObject.Reference.TransactionCode.DownPaymentReturn
              );
            htp.Where(htp.RegistrationNo == string.Empty);
            htp.Select(dtp.ReferenceNo);

            DataTable dtb = htp.LoadDataTable();

            var visitDpItem = new ClosingVisiteDownPaymentItemQuery();
            visitDpItem.Where(visitDpItem.ClosingNo == entity.ClosingNo);
            visitDpItem.Select(visitDpItem.PaymentNo);
            if (dtb.AsEnumerable().Any())
            {
                visitDpItem.Where(visitDpItem.PaymentNo.In(dtb.AsEnumerable().Select(d => d.Field<string>("ReferenceNo"))));

                DataTable visitDtb = visitDpItem.LoadDataTable();
                if (visitDtb.Rows.Count > 0)
                {
                    var paymentNos = string.Empty;
                    foreach (DataRow row in visitDtb.Rows)
                    {
                        if (paymentNos == string.Empty)
                            paymentNos = row["PaymentNo"].ToString();
                        else
                            paymentNos += ", " + row["PaymentNo"].ToString();
                    }

                    args.MessageText = string.Format("Down Payment reference: {0} already returned.", paymentNos);
                    args.IsCancel = true;
                    return;
                }
            }
                
            // cek Dp udah di closing atau belum
            visitDpItem = new ClosingVisiteDownPaymentItemQuery("a");
            var closingDp = new TransPaymentQuery("b");
            visitDpItem.InnerJoin(closingDp).On(closingDp.PaymentNo == visitDpItem.PaymentNo);
            visitDpItem.Where(visitDpItem.ClosingNo == entity.ClosingNo, closingDp.IsClosedVisiteDownPayment == true);
            visitDpItem.Select(visitDpItem.PaymentNo);

            DataTable closingDtb = visitDpItem.LoadDataTable();
            if (closingDtb.Rows.Count > 0)
            {
                var paymentNos = string.Empty;
                foreach (DataRow row in closingDtb.Rows)
                {
                    if (paymentNos == string.Empty)
                        paymentNos = row["PaymentNo"].ToString();
                    else
                        paymentNos += ", " + row["PaymentNo"].ToString();
                }

                args.MessageText = string.Format("Down Payment reference: {0} already closed.", paymentNos);
                args.IsCancel = true;
                return;
            }

            var closingperiod = entity.ClosingDate.Value.Date;
            var isClosingPeriod = PostingStatus.IsPeriodeClosed(closingperiod);
            if (isClosingPeriod)
            {
                args.MessageText = "Financial statements for period: " +
                                   string.Format("{0:MMMM-yyyy}", closingperiod) +
                                   " have been closed. Please contact the authorities.";
                args.IsCancel = true;
                return;
            }

            entity.IsApproved = true;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.ApprovedByUserID = AppSession.UserLogin.UserID;
            entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();


            // update status IsClosedVisiteDownPayment
            var dp = new ClosingVisiteDownPaymentItemQuery("a");
            dp.Where(dp.ClosingNo == entity.ClosingNo);
            dp.Select(dp.PaymentNo);
            DataTable dtbdp = dp.LoadDataTable();

            var payments = new TransPaymentCollection();
            var paymentq = new TransPaymentQuery();
            if (dtbdp.AsEnumerable().Any())
                paymentq.Where(paymentq.PaymentNo.In(dtbdp.AsEnumerable().Select(d => d.Field<string>("PaymentNo"))));
            else
                paymentq.Where(paymentq.PaymentNo == "-N/A-");

            payments.Load(paymentq);
            foreach (var p in payments)
            {
                p.IsClosedVisiteDownPayment = true;
            }

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                payments.Save();

                int? journalId =
                        JournalTransactions.AddNewClosingVisitDownPaymentJournal(BusinessObject.JournalType.ClosingvisitDownPayment,
                                                                           entity, this.ClosingVisiteDownPaymentItems, "CVD",
                                                                           AppSession.UserLogin.UserID, 0);

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new ClosingVisiteDownPayment();
            if (!entity.LoadByPrimaryKey(txtClosingNo.Text))
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

            entity.IsVoid = true;
            entity.VoidDateTime = (new DateTime()).NowAtSqlServer();
            entity.VoidByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                trans.Complete();
            }
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuEdit()
        {
            return txtClosingNo.Text != string.Empty;
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
            RefreshCommandItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ClosingVisiteDownPayment();
            if (parameters.Length > 0)
            {
                String closingNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(closingNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtClosingNo.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            ClosingVisiteDownPayment transPayment = (ClosingVisiteDownPayment)entity;
            txtClosingNo.Text = transPayment.ClosingNo;
            txtClosingDate.SelectedDate = transPayment.ClosingDate;

            Patient pat = new Patient();
            if (pat.LoadByPrimaryKey(transPayment.PatientID ?? Request.QueryString["patid"]))
            {
                txtMedicalNo.Text = pat.MedicalNo;
                txtPatientName.Text = pat.PatientName;
                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.str.SRSalutation) ? std.ItemName : string.Empty;
            }
            else
            {
                txtMedicalNo.Text = string.Empty;
                txtPatientName.Text = string.Empty;
                txtSalutation.Text = string.Empty;
            }
            txtNotes.Text = transPayment.Notes;

            ViewState["IsVoid"] = transPayment.IsVoid ?? false;
            ViewState["IsApproved"] = transPayment.IsApproved ?? false;

            //Display Data Detail
            PopulateItemGrid();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(ClosingVisiteDownPayment entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtClosingNo.Text = GetNewClosingNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }

            entity.ClosingNo = txtClosingNo.Text;
            entity.ClosingDate = txtClosingDate.SelectedDate;
            entity.PatientID = Request.QueryString["patid"];
            entity.Notes = txtNotes.Text;
            entity.IsVoid = false;
            entity.IsApproved = false;
            entity.Notes = txtNotes.Text;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (ClosingVisiteDownPaymentItem item in ClosingVisiteDownPaymentItems)
            {
                item.ClosingNo = entity.ClosingNo;
            }
        }

        private void SaveEntity(ClosingVisiteDownPayment entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {

                entity.Save();
                ClosingVisiteDownPaymentItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ClosingVisiteDownPaymentQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.ClosingNo > txtClosingNo.Text
                    );
                que.OrderBy(que.ClosingNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.ClosingNo < txtClosingNo.Text
                    );
                que.OrderBy(que.ClosingNo.Descending);
            }
            ClosingVisiteDownPayment entity = new ClosingVisiteDownPayment();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        private string GetNewClosingNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer(), AppEnum.AutoNumber.ClosingVisitDpNo);
            return _autoNumber.LastCompleteNumber;
        }

        #endregion

        #region Record Detail Method Function ClosingVisiteDownPaymentItem

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;
            btnDownPayment.Enabled = isVisible;

            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private ClosingVisiteDownPaymentItemCollection ClosingVisiteDownPaymentItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["ClosingVisiteDownPaymentItems" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((ClosingVisiteDownPaymentItemCollection)(obj));
                    }
                }

                var coll = new ClosingVisiteDownPaymentItemCollection();
                var query = new ClosingVisiteDownPaymentItemQuery("a");
                var payment = new TransPaymentQuery("b");

                query.Select
                    (
                        query,
                        payment.PaymentDate.As("refToTransPayment_PaymentDate"),
                        payment.PaymentTime.As("refToTransPayment_PaymentTime")
                    );
                query.InnerJoin(payment).On(payment.PaymentNo == query.PaymentNo);
                query.Where(query.ClosingNo == txtClosingNo.Text);
                query.OrderBy(query.PaymentNo.Ascending);

                coll.Load(query);

                Session["ClosingVisiteDownPaymentItems" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["ClosingVisiteDownPaymentItems" + Request.UserHostName] = value; }
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            ClosingVisiteDownPaymentItems = null; //Reset Record Detail
            grdItem.DataSource = ClosingVisiteDownPaymentItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = ClosingVisiteDownPaymentItems;
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String paymentNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ClosingVisiteDownPaymentItemMetadata.ColumnNames.PaymentNo]);
            ClosingVisiteDownPaymentItem entity = FindItem(paymentNo);

            if (entity != null)
                entity.MarkAsDeleted();
        }

        private ClosingVisiteDownPaymentItem FindItem(String paymentNo)
        {
            ClosingVisiteDownPaymentItemCollection coll = ClosingVisiteDownPaymentItems;
            ClosingVisiteDownPaymentItem retEntity = null;
            foreach (ClosingVisiteDownPaymentItem rec in coll)
            {
                if (rec.PaymentNo.Equals(paymentNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        #endregion

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
            ToolBarMenuEdit.Enabled = !(bool)ViewState["IsApproved"] && !(bool)ViewState["IsVoid"];
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler source, string argument)
        {
            base.RaisePostBackEvent(source, argument);

            if (string.IsNullOrEmpty(argument) || !(source is RadGrid))
                return;

            if (argument.Contains("rebind"))
            {
                grdItem.Rebind();
            }
        }
    }
}