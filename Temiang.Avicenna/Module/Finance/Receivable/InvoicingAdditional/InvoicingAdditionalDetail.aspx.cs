using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Receivable
{
    public partial class InvoicingAdditionalDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private void PopulateNewInvoiceNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New) return;

            _autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.InvoiceARNo);
            txtInvoiceNo.Text = _autoNumber.LastCompleteNumber;
            
        }

        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            if (AppSession.Parameter.IsNeedVoidReasonOnArInvoicing)
            {
                IsUsingBeforeUnapprovalValidation = true;
            }

            // Url Search & List
            UrlPageSearch = "InvoicingAdditionalSearch.aspx";
            UrlPageList = "InvoicingAdditionalList.aspx";

            this.WindowSearch.Height = 400;
            ProgramID = AppConstant.Program.AR_INVOICING_ADDITIONAL;


            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRReceivableStatus, AppEnum.StandardReference.ReceivableStatus);
                StandardReference.InitializeIncludeSpace(cboSRReceivableType, AppEnum.StandardReference.ReceivableType);

                pnlDiscount.Visible = AppSession.Parameter.IsAllowDiscountInvoice;
            }

        }


        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdInvoicesItem, grdInvoicesItem);

            ajax.AddAjaxSetting(AjaxManager, txtInvoiceNo);
            ajax.AddAjaxSetting(AjaxManager, grdInvoicesItem);
            
            ajax.AddAjaxSetting(grdInvoicesItem, txtTotal);

            if (AppSession.Parameter.IsAllowDiscountInvoice)
            {
                ajax.AddAjaxSetting(grdInvoicesItem, txtTransactionAmount);
                ajax.AddAjaxSetting(grdInvoicesItem, txtDiscountAmount);

                ajax.AddAjaxSetting(AjaxManager, chkIsDiscountInPercentage);
                ajax.AddAjaxSetting(chkIsDiscountInPercentage, txtDiscountPercentage);
                ajax.AddAjaxSetting(chkIsDiscountInPercentage, txtDiscountAmount);
                ajax.AddAjaxSetting(chkIsDiscountInPercentage, txtTotal);

                ajax.AddAjaxSetting(AjaxManager, txtDiscountPercentage);
                ajax.AddAjaxSetting(txtDiscountPercentage, txtDiscountAmount);
                ajax.AddAjaxSetting(txtDiscountPercentage, txtTotal);

                ajax.AddAjaxSetting(AjaxManager, txtDiscountAmount);
                ajax.AddAjaxSetting(txtDiscountAmount, txtTotal);
            }
        }
        #endregion

        #region ComboBox GuarantorID

        protected void cboGuarantorID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboGuarantorID((RadComboBox)sender, e.Text);
        }

        private void PopulateCboGuarantorID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new GuarantorQuery("a");
            var query2 = new GuarantorQuery("b");
            query.InnerJoin(query2).On(query.GuarantorHeaderID == query2.GuarantorID);

            query.Select(query2.GuarantorID, query2.GuarantorName,
                         (query2.StreetName + " " + query2.City + " " + query2.ZipCode).Trim().As("Address"));

            query.Where
                (
                    query.Or
                        (
                            query2.GuarantorID.Like(searchTextContain),
                            query2.GuarantorName.Like(searchTextContain)
                        )
                );

            query.es.Distinct = true;
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID,
                                               PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("InvoiceNo", txtInvoiceNo.Text);
        }

        protected override void OnMenuEditClick()
        {
            txtDiscountPercentage.ReadOnly = !chkIsDiscountInPercentage.Checked;
            txtDiscountAmount.ReadOnly = chkIsDiscountInPercentage.Checked;
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            Invoices entity = new Invoices();
            if (entity.LoadByPrimaryKey(txtInvoiceNo.Text))
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
            var retval = (new Invoices()).Approv(txtInvoiceNo.Text, AppSession.UserLogin.UserID, AppSession.Parameter.ReceivableStatusProcess);
            if (retval != string.Empty)
            {
                args.MessageText = retval;
                args.IsCancel = true;
            }
        }
        public override bool OnGetStatusMenuUnApprovalEnabled()
        {
            return !PostingStatus.IsUnApproveDisabledIfPerClosed(txtInvoiceDate.SelectedDate.Value);
        }
        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var reason = args.ReasonText;

            var entity = new Invoices();
            if (!entity.LoadByPrimaryKey(txtInvoiceNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            //Sekarang dibuat jika sekalipun sudah verifikasi, selama belum dibayar tetap boleh diunapprove lalu divoid.
            //if (!string.IsNullOrEmpty(entity.VerifyByUserID))
            //{
            //    args.MessageText = AppConstant.Message.RecordHasVerified;
            //    args.IsCancel = true;
            //    return;
            //}
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            //var inv = new InvoicesCollection();
            //inv.Query.Where(inv.Query.InvoiceReferenceNo == txtInvoiceNo.Text, inv.Query.IsVoid == false);
            //inv.LoadAll();
            //if (inv.Count > 0)
            //{
            //    args.MessageText = "The invoice has been processed to payment transaction.";
            //    args.IsCancel = true;
            //    return;
            //}

            var ivip = new InvoicesItemQuery("ivi");
            var ivp = new InvoicesQuery("iv");
            ivip.InnerJoin(ivp).On(ivip.InvoiceNo == ivp.InvoiceNo)
                .Where(
                    ivip.InvoiceReferenceNo == txtInvoiceNo.Text,
                    ivp.IsVoid == false
                ).Select(ivip.InvoiceNo);
            if ((new InvoicesItemCollection()).Load(ivip)) {
                args.MessageText = "The invoice has been processed to payment transaction.";
                args.IsCancel = true;
                return;
            }

            var retval = (new Invoices()).UnApprov(txtInvoiceNo.Text, AppSession.UserLogin.UserID, AppSession.Parameter.ReceivableStatusProcess, reason);
            if (retval != string.Empty)
            {
                args.MessageText = retval;
                args.IsCancel = true;
            }
        }
        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            (new Invoices()).Void(txtInvoiceNo.Text, AppSession.UserLogin.UserID);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            (new Invoices()).UnVoid(txtInvoiceNo.Text, AppSession.UserLogin.UserID);
        }

        private bool IsApprovedOrVoid(Invoices entity, ValidateArgs args)
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
            OnPopulateEntryControl(new Invoices());
            PopulateNewInvoiceNo();

            txtInvoiceDate.SelectedDate = DateTime.Now;
            cboGuarantorID.Text = string.Empty;
            cboSRReceivableType.Text = string.Empty;
            txtTotal.Value = 0;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            Invoices entity = new Invoices();
            if (entity.LoadByPrimaryKey(txtInvoiceNo.Text))
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
            PopulateNewInvoiceNo();
            Invoices entity = new Invoices();
            if (entity.LoadByPrimaryKey(txtInvoiceNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new Invoices();
            entity.AddNew();
            SetEntityValue(entity);
            if (InvoicesItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Invoices entity = new Invoices();
            if (entity.LoadByPrimaryKey(txtInvoiceNo.Text))
            {
                SetEntityValue(entity);
                if (InvoicesItems.Count == 0)
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
            auditLogFilter.PrimaryKeyData = string.Format("InvoiceNo='{0}'", txtInvoiceNo.Text.Trim());
            auditLogFilter.TableName = "Invoices";
        }


        #endregion

        #region ToolBar Menu Support
        public override bool OnGetStatusMenuEdit()
        {
            return txtInvoiceNo.Text != string.Empty;
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

            if (newVal != AppEnum.DataMode.New)
            {
                txtInvoiceDate.Enabled = false;
                cboSRReceivableType.Enabled = false;
                cboGuarantorID.Enabled = false;

                if (oldVal == AppEnum.DataMode.New)
                {
                    cboSRReceivableType.Text = string.Empty;
                    cboGuarantorID.Text = string.Empty;
                }
            }
            else
            {
                cboSRReceivableType.Text = string.Empty;
                cboGuarantorID.Text = string.Empty;
                txtInvoiceDate.Enabled = true;
                cboSRReceivableType.Enabled = true;
                cboGuarantorID.Enabled = true;
            }
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            Invoices entity = new Invoices();
            if (parameters.Length > 0)
            {
                String invoiceNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(invoiceNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtInvoiceNo.Text);
            }

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var invoices = (Invoices)entity;
            txtInvoiceNo.Text = invoices.InvoiceNo;
            cboSRReceivableType.SelectedValue = invoices.SRReceivableType;

            if (!string.IsNullOrEmpty(invoices.GuarantorID))
            {
                var query = new GuarantorQuery("a");
                var query2 = new GuarantorQuery("b");
                query.InnerJoin(query2).On(query.GuarantorHeaderID == query2.GuarantorID);
                query.Select(query2.GuarantorID, query2.GuarantorName,
                             (query2.StreetName + " " + query2.City + " " + query2.ZipCode).Trim().As("Address"));
                query.Where(query2.GuarantorID == invoices.GuarantorID);
                query.es.Distinct = true;
                DataTable dtb = query.LoadDataTable();

                if (dtb.Rows.Count == 0)
                {
                    var query3 = new GuarantorQuery("a");
                    query3.Select(query3.GuarantorID, query3.GuarantorName,
                                 (query3.StreetName + " " + query3.City + " " + query3.ZipCode).Trim().As("Address"));
                    query3.Where(query3.GuarantorID == invoices.GuarantorID);
                    query3.es.Distinct = true;
                    dtb = query3.LoadDataTable();
                }

                cboGuarantorID.DataSource = dtb;
                cboGuarantorID.DataBind();
                cboGuarantorID.SelectedValue = invoices.GuarantorID;
                cboGuarantorID.Text = dtb.Rows[0]["GuarantorName"].ToString();
            }
            else
            {
                cboGuarantorID.Items.Clear();
                cboGuarantorID.Text = string.Empty;
            }
            
            txtInvoiceDate.SelectedDate = invoices.InvoiceDate;
            txtInvoiceDueDate.SelectedDate = invoices.InvoiceDueDate;
            txtTermOfPayment.Value = Convert.ToDouble(invoices.InvoiceTOP);
            txtNotes.Text = invoices.InvoiceNotes;
            cboSRReceivableStatus.SelectedValue = invoices.SRReceivableStatus;
            chkIsDiscountInPercentage.Checked = invoices.IsDiscountInPercantege ?? false;
            txtDiscountPercentage.Value = Convert.ToDouble(invoices.DiscountPercentage);
            txtDiscountAmount.Value = Convert.ToDouble(invoices.DiscountAmount);

            ViewState["IsVoid"] = invoices.IsVoid ?? false;
            ViewState["IsApproved"] = invoices.IsApproved ?? false;

            //Display Data Detail
            PopulateGridDetail();

            CalculateDetailTransaction();
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(Invoices entity)
        {
            entity.InvoiceNo = txtInvoiceNo.Text;
            entity.SRReceivableType = cboSRReceivableType.SelectedValue;
            entity.GuarantorID = cboGuarantorID.SelectedValue;
            entity.InvoiceDate = txtInvoiceDate.SelectedDate;
            entity.InvoiceDueDate = txtInvoiceDueDate.SelectedDate;
            entity.InvoiceTOP = (decimal?)(txtTermOfPayment.Value ?? 0);
            entity.InvoiceNotes = txtNotes.Text;
            entity.IsInvoicePayment = false;
            entity.IsDiscountInPercantege = chkIsDiscountInPercentage.Checked;
            entity.DiscountPercentage = (decimal?) txtDiscountPercentage.Value;
            entity.DiscountAmount = (decimal?)txtDiscountAmount.Value;
            entity.IsAdditionalInvoice = true;


            //Update Detil
            foreach (InvoicesItem item in InvoicesItems)
            {
                item.InvoiceNo = txtInvoiceNo.Text;
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void SaveEntity(Invoices entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                InvoicesItems.Save();

                if (DataModeCurrent == AppEnum.DataMode.New)
                    _autoNumber.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            InvoicesQuery que = new InvoicesQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.InvoiceNo > txtInvoiceNo.Text,que.IsAdditionalInvoice==true );
                que.OrderBy(que.InvoiceNo.Ascending);
            }
            else
            {
                que.Where(que.InvoiceNo < txtInvoiceNo.Text, que.IsAdditionalInvoice == true);
                que.OrderBy(que.InvoiceNo.Descending);
            }
            Invoices entity = new Invoices();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged
        protected void cboGuarantorID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Guarantor guarantor = new Guarantor();
            guarantor.LoadByPrimaryKey(cboGuarantorID.SelectedValue);
            // if top guarantor is 0 then use value from appparameter
            txtTermOfPayment.Value = Convert.ToDouble(((guarantor.TermOfPayment ?? 0) == 0) ? int.Parse(AppSession.Parameter.InvoiceTermOfPayment) : (guarantor.TermOfPayment.Value));
            //txtTermOfPayment.Value = int.Parse(AppSession.Parameter.InvoiceTermOfPayment);

            DateTime date = Convert.ToDateTime(txtInvoiceDate.SelectedDate);
            txtInvoiceDueDate.SelectedDate = date.AddDays((int)txtTermOfPayment.Value);

            if (cboGuarantorID.SelectedValue == AppSession.Parameter.SelfGuarantor)
                cboSRReceivableType.SelectedValue = AppSession.Parameter.ReceivableTypePersonal;
            else
                cboSRReceivableType.SelectedValue = AppSession.Parameter.ReceivableTypeCorporate;
        }

        protected void txtTermOfPayment_TextChanged(object sender, EventArgs e)
        {
            DateTime date = Convert.ToDateTime(txtInvoiceDate.SelectedDate);
            txtInvoiceDueDate.SelectedDate = date.AddDays((int)txtTermOfPayment.Value);
        }

        #endregion

        #region Record Detail Method Function

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdInvoicesItem.Columns[0].Visible = isVisible;
            grdInvoicesItem.Columns[grdInvoicesItem.Columns.Count - 1].Visible = isVisible;

            grdInvoicesItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                InvoicesItems = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdInvoicesItem.Rebind();
        }

        private InvoicesItemCollection InvoicesItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collInvoicesItem" + Request.UserHostName];
                    if (obj != null)
                        return ((InvoicesItemCollection)(obj));
                }

                var coll = new InvoicesItemCollection();
                var query = new InvoicesItemQuery("a");
                var coa = new ChartOfAccountsQuery("coa");
                var pph = new AppStandardReferenceItemQuery("pph");
                var pat = new PatientQuery("d");
                query.LeftJoin(coa).On(query.ChartOfAccountId == coa.ChartOfAccountId);
                query.LeftJoin(pph).On(query.SRPph == pph.ItemID && pph.StandardReferenceID == "Pph");
                query.LeftJoin(pat).On(query.PatientID == pat.PatientID);

                query.Where(query.InvoiceNo == txtInvoiceNo.Text);
                query.OrderBy
                    (
                        query.PaymentNo.Ascending
                    );

                query.Select
                    (
                        query,
                        pat.PatientID,
                        pat.MedicalNo.As("refPatientID_MedicalNo"),
                        coa.ChartOfAccountCode.As("refToChartOfAccounts_ChartOfAccountCode"),
                        coa.ChartOfAccountName.As("refToChartOfAccounts_ChartOfAccountName"),
                        pph.ItemName.As("refToAppStandardReferenceItem_Pph")
                    );

                coll.Load(query);

                Session["collInvoicesItem" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collInvoicesItem" + Request.UserHostName] = value; }
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            InvoicesItems = null; //Reset Record Detail
            grdInvoicesItem.DataSource = InvoicesItems;
            grdInvoicesItem.MasterTableView.IsItemInserted = false;
            grdInvoicesItem.MasterTableView.ClearEditItems();
            grdInvoicesItem.DataBind();
        }

        protected void grdInvoicesItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdInvoicesItem.DataSource = InvoicesItems;
        }

        protected void grdInvoicesItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String paymentNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][InvoicesItemMetadata.ColumnNames.PaymentNo]);
            InvoicesItem entity = FindInvoicesItem(paymentNo);
            if (entity != null)
                SetEntityValue(entity, e);

            CalculateDetailTransaction();
        }

        private InvoicesItem FindInvoicesItem(String paymentNo)
        {
            InvoicesItemCollection coll = InvoicesItems;
            InvoicesItem retEntity = null;
            foreach (InvoicesItem rec in coll)
            {
                if (rec.PaymentNo.Equals(paymentNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdInvoicesItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String paymentNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][InvoicesItemMetadata.ColumnNames.PaymentNo]);
            InvoicesItem entity = FindInvoicesItem(paymentNo);
            if (entity != null)
            {
                entity.MarkAsDeleted();
            }

            CalculateDetailTransaction();
        }

        protected void grdInvoicesItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            InvoicesItem entity = InvoicesItems.AddNew();
            SetEntityValue(entity, e);
        }

        private void SetEntityValue(InvoicesItem entity, GridCommandEventArgs e)
        {
            var userControl = (InvoicingAdditionalItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.PaymentDate = (new DateTime()).NowAtSqlServer().Date;
                entity.Amount = userControl.Amount;
                entity.IsPpn = userControl.IsPpn;
                entity.IsPph = userControl.IsPph;
                entity.PpnPercentage = userControl.PpnPercentage;
                entity.PphPercentage = userControl.PphPercentage;
                entity.PpnAmount = userControl.PpnAmount;
                entity.PphAmount = userControl.PphAmount;
                entity.SRPph = userControl.SRPph;
                entity.PphName = userControl.PphName;
                entity.VerifyAmount = userControl.Amount + userControl.PpnAmount + userControl.PphAmount;
                entity.Notes = userControl.Notes;
                if (string.IsNullOrEmpty(entity.PaymentNo))
                {
                    //var maxSeqNo = string.Empty;
                    //foreach (var item in InvoicesItems)
                    //{

                    //        if (maxSeqNo.ToInt() < item.PaymentNo.ToInt())
                    //            maxSeqNo = item.PaymentNo;

                    //}
                    //entity.PaymentNo = string.Format("{0:000}", maxSeqNo.ToInt() + 1);
                    entity.PaymentNo = Common.Helper.Get8DigitsUnique();
                }
                entity.ChartOfAccountId = userControl.ChartOfAccountId;
                var coa = new ChartOfAccounts();
                if(coa.LoadByPrimaryKey(entity.ChartOfAccountId.Value)){
                    entity.ChartOfAccountCode = coa.ChartOfAccountCode;
                    entity.ChartOfAccountName = coa.ChartOfAccountName;
                }

                entity.SubLedgerId = userControl.SubLedgerId;



                entity.PatientID = userControl.PatientID;
                entity.MedicalNo = userControl.MedicalNo;
                entity.PatientName = userControl.PatientName;
            }
        }

        #endregion



        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
            ToolBarMenuEdit.Enabled = !(bool)ViewState["IsApproved"];
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument == "rebind")
            {
                CalculateDetailTransaction();
                grdInvoicesItem.Rebind();
            }
        }


        #region TextChanged
        protected void chkIsDiscountInPercentage_CheckedChanged(object sender, EventArgs e)
        {
            txtDiscountPercentage.ReadOnly = !chkIsDiscountInPercentage.Checked;
            txtDiscountAmount.ReadOnly = chkIsDiscountInPercentage.Checked;

            CalculateDiscount();
        }

        protected void txtDiscountPercentage_TextChanged(object sender, EventArgs e)
        {
            CalculateDiscount();
        }

        protected void txtDiscountAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateDiscount();
        }

        private void CalculateDetailTransaction()
        {
            if (InvoicesItems.Count > 0)
            {
                decimal? totaltransaction = InvoicesItems.Aggregate<InvoicesItem, decimal?>(0, (current, item) => current + (item.Amount + item.PpnAmount + item.PphAmount));

                txtTransactionAmount.Value = Convert.ToDouble(totaltransaction);
                
                CalculateDiscount();
            }
        }

        private void CalculateDiscount()
        {
            if (chkIsDiscountInPercentage.Checked)
                txtDiscountAmount.Value = ((txtTransactionAmount.Value * txtDiscountPercentage.Value) / Convert.ToDouble(100));
            else
                txtDiscountPercentage.Value = 0;
            
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            txtTotal.Value = txtTransactionAmount.Value - txtDiscountAmount.Value;
        }

        #endregion

    }
}
