using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI;
namespace Temiang.Avicenna.Module.Charges.Cashier
{
    public partial class PaymentReceiptDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private void PopulateNewPaymentReceiptNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New) return;

            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.PaymentReceiptNo);
            txtPaymentReceiptNo.Text = _autoNumber.LastCompleteNumber;
        }

        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "PaymentReceiptSearch.aspx";
            UrlPageList = "PaymentReceiptList.aspx";

            ProgramID = AppConstant.Program.PaymentReceipt;


            if (!IsPostBack)
            {
                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
                {
                    trCboRegNo.Visible = false;
                    rfvCboRegNo.Visible = false;
                    btnGetPayment.Visible = false;
                }
                else
                {
                    trTxtRegNo.Visible = false;
                    rfvTxtRegNo.Visible = false;
                    btnGetPayment2.Visible = false;
                }
            }

            //Add Event for Request Order Selection
            this.AjaxManager.AjaxRequest += new RadAjaxControl.AjaxRequestDelegate(AjaxManager_AjaxRequest);
        }

        void AjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdTransPaymentReceiptItem, grdTransPaymentReceiptItem);
            ajax.AddAjaxSetting(grdTransPaymentReceiptItem, txtTotalReceipt);

            ajax.AddAjaxSetting(AjaxManager, txtPaymentReceiptNo);
            ajax.AddAjaxSetting(AjaxManager, grdTransPaymentReceiptItem);

            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
            {
                ajax.AddAjaxSetting(AjaxManager, txtRegistrationNo);
                ajax.AddAjaxSetting(txtRegistrationNo, txtMedicalNo);
                ajax.AddAjaxSetting(txtRegistrationNo, txtPatientName);
                ajax.AddAjaxSetting(txtRegistrationNo, txtSalutation);
                ajax.AddAjaxSetting(txtRegistrationNo, txtPrintReceiptAsName);
                ajax.AddAjaxSetting(txtRegistrationNo, txtServiceUnitName);
            }
            else
            {
                ajax.AddAjaxSetting(AjaxManager, cboRegistrationNo);
                ajax.AddAjaxSetting(cboRegistrationNo, txtMedicalNo);
                ajax.AddAjaxSetting(cboRegistrationNo, txtPatientName);
                ajax.AddAjaxSetting(txtRegistrationNo, txtSalutation);
                ajax.AddAjaxSetting(cboRegistrationNo, txtPrintReceiptAsName);
                ajax.AddAjaxSetting(cboRegistrationNo, txtServiceUnitName);
            }
        }
        #endregion

        #region ComboBox RegistrationNo

        protected void cboRegistrationNo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboRegistrationNo((RadComboBox)sender, e.Text);
        }

        private void PopulateCboRegistrationNo(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new RegistrationQuery("a");
            var pat = new PatientQuery("b");
            var su = new ServiceUnitQuery("c");

            query.InnerJoin(pat).On(query.PatientID == pat.PatientID);
            query.InnerJoin(su).On(query.ServiceUnitID == su.ServiceUnitID);

            query.Select
                (
                    query.RegistrationNo,
                    query.PatientID,
                    pat.MedicalNo,
                    su.ServiceUnitName,
                    @"<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) AS 'PatientName'>"
                    );

            query.Where(query.IsVoid == false, query.IsClosed == false);
            query.Where
                (
                    query.Or
                        (
                            query.RegistrationNo.Like(searchTextContain),
                            pat.MedicalNo.Like(searchTextContain),
                            pat.FirstName.Like(searchTextContain),
                            pat.MiddleName.Like(searchTextContain),
                            pat.LastName.Like(searchTextContain)
                        )
                );

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }

        protected void cboRegistrationNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["RegistrationNo"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["RegistrationNo"].ToString();
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID,
                                                 PrintJobParameterCollection printJobParameters)
        {
            var regNo = string.IsNullOrEmpty(cboRegistrationNo.SelectedValue) ? txtRegistrationNo.Text : cboRegistrationNo.SelectedValue;
            switch (programID)
            {
                case AppConstant.Report.PaymentReceiptAllDirect:
                    printJobParameters.AddNew("PaymentReceiptNo", txtPaymentReceiptNo.Text);
                    printJobParameters.AddNew("RegNo", regNo);
                    printJobParameters.AddNew("UserID", AppSession.UserLogin.UserID);
                    break;
                case AppConstant.Report.PaymentReceiptAllDirectDetail:
                    printJobParameters.AddNew("PaymentReceiptNo", txtPaymentReceiptNo.Text);
                    printJobParameters.AddNew("RegNo", regNo);
                    printJobParameters.AddNew("UserID", AppSession.UserLogin.UserID);
                    break;
            }
        }

        protected override void OnMenuEditClick()
        {

        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new TransPaymentReceipt();
            if (entity.LoadByPrimaryKey(txtPaymentReceiptNo.Text))
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
            (new TransPaymentReceipt()).Approv(txtPaymentReceiptNo.Text, AppSession.UserLogin.UserID);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            (new TransPaymentReceipt()).Void(txtPaymentReceiptNo.Text, AppSession.UserLogin.UserID);
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            (new TransPaymentReceipt()).Void(txtPaymentReceiptNo.Text, AppSession.UserLogin.UserID);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            (new TransPaymentReceipt()).UnVoid(txtPaymentReceiptNo.Text, AppSession.UserLogin.UserID);
        }

        private bool IsApprovedOrVoid(TransPaymentReceipt entity, ValidateArgs args)
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
            OnPopulateEntryControl(new TransPaymentReceipt());
            PopulateNewPaymentReceiptNo();

            txtPaymentReceiptDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtPaymentReceiptTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");

            btnGetPayment.Enabled = true;
            txtTotalReceipt.Value = 0;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            TransPaymentReceipt entity = new TransPaymentReceipt();
            if (entity.LoadByPrimaryKey(txtPaymentReceiptNo.Text))
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
            PopulateNewPaymentReceiptNo();
            // save autonumber immediately to decrease time gap between create and save
            _autoNumber.Save();

            TransPaymentReceipt entity = new TransPaymentReceipt();
            if (entity.LoadByPrimaryKey(txtPaymentReceiptNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new TransPaymentReceipt();
            entity.AddNew();
            SetEntityValue(entity);
            if (TransPaymentReceiptItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            TransPaymentReceipt entity = new TransPaymentReceipt();
            if (entity.LoadByPrimaryKey(txtPaymentReceiptNo.Text))
            {
                SetEntityValue(entity);
                if (TransPaymentReceiptItems.Count == 0)
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
            auditLogFilter.PrimaryKeyData = string.Format("PaymentReceiptNo='{0}'", txtPaymentReceiptNo.Text.Trim());
            auditLogFilter.TableName = "TransPaymentReceipt";
        }

        #endregion

        #region ToolBar Menu Support
        public override bool OnGetStatusMenuEdit()
        {
            return txtPaymentReceiptNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !chkIsApproved.Checked;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !chkIsVoid.Checked;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemGrid(oldVal, newVal);

            btnGetPayment.Enabled = newVal != AppEnum.DataMode.Read;

            if (newVal != AppEnum.DataMode.New)
            {
                txtPaymentReceiptDate.Enabled = false;
                cboRegistrationNo.Enabled = false;

                if (oldVal == AppEnum.DataMode.New)
                {
                    cboRegistrationNo.Text = string.Empty;
                }
            }
            else
            {
                cboRegistrationNo.Text = string.Empty;
                txtPaymentReceiptDate.Enabled = true;
                cboRegistrationNo.Enabled = true;
            }
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new TransPaymentReceipt();
            if (parameters.Length > 0)
            {
                String paymentReceiptNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(paymentReceiptNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtPaymentReceiptNo.Text);
            }

            var reg = new RegistrationQuery("a");
            var pat = new PatientQuery("b");
            var su = new ServiceUnitQuery("c");

            reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            reg.InnerJoin(su).On(reg.ServiceUnitID == su.ServiceUnitID);

            reg.Select
                (
                    reg.RegistrationNo,
                    reg.PatientID,
                    pat.MedicalNo,
                    su.ServiceUnitName,
                    @"<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) AS 'PatientName'>");
            reg.Where(reg.RegistrationNo == entity.RegistrationNo);

            DataTable tbl = reg.LoadDataTable();
            cboRegistrationNo.DataSource = tbl;
            cboRegistrationNo.DataBind();

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var transPaymentReceipt = (TransPaymentReceipt)entity;
            txtPaymentReceiptNo.Text = transPaymentReceipt.PaymentReceiptNo;
            txtPaymentReceiptDate.SelectedDate = transPaymentReceipt.PaymentReceiptDate;
            txtPaymentReceiptTime.Text = transPaymentReceipt.PaymentReceiptTime;

            if (!string.IsNullOrEmpty(transPaymentReceipt.RegistrationNo))
            {
                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
                    txtRegistrationNo.Text = transPaymentReceipt.RegistrationNo;
                else
                {
                    var query = new RegistrationQuery("a");
                    var patq = new PatientQuery("b");
                    var suq = new ServiceUnitQuery("c");
                    query.InnerJoin(patq).On(query.PatientID == patq.PatientID);
                    query.InnerJoin(suq).On(query.ServiceUnitID == suq.ServiceUnitID);
                    query.Select
                        (
                            query.RegistrationNo,
                            query.PatientID,
                            patq.MedicalNo,
                            suq.ServiceUnitName,
                            @"<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) AS 'PatientName'>"
                            );
                    query.Where(query.RegistrationNo == transPaymentReceipt.RegistrationNo);
                    cboRegistrationNo.DataSource = query.LoadDataTable();
                    cboRegistrationNo.DataBind();
                    cboRegistrationNo.SelectedValue = transPaymentReceipt.RegistrationNo;
                }

                var reg = new Registration();
                reg.LoadByPrimaryKey(transPaymentReceipt.RegistrationNo);

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                txtMedicalNo.Text = pat.MedicalNo;
                txtPatientName.Text = pat.PatientName;
                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;

                var su = new ServiceUnit();
                su.LoadByPrimaryKey(reg.ServiceUnitID);
                txtServiceUnitName.Text = su.ServiceUnitName;
            }
            else
            {
                cboRegistrationNo.Items.Clear();
                cboRegistrationNo.Text = string.Empty;

                txtRegistrationNo.Text = string.Empty;
                txtMedicalNo.Text = string.Empty;
                txtPatientName.Text = string.Empty;
                txtSalutation.Text = string.Empty;
                txtServiceUnitName.Text = string.Empty;
            }

            txtPrintReceiptAsName.Text = transPaymentReceipt.PrintReceiptAsName;
            txtNotes.Text = transPaymentReceipt.Notes;

            chkIsVoid.Checked = transPaymentReceipt.IsVoid ?? false;
            chkIsPrinted.Checked = transPaymentReceipt.IsPrinted ?? false;
            chkIsApproved.Checked = transPaymentReceipt.IsApproved ?? false;

            //Display Data Detail
            PopulateGridDetail();
            CalculateDetailReceipt();
            btnGetPayment.Enabled = false;
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(TransPaymentReceipt entity)
        {
            entity.PaymentReceiptNo = txtPaymentReceiptNo.Text;
            entity.PaymentReceiptDate = txtPaymentReceiptDate.SelectedDate;
            entity.PaymentReceiptTime = txtPaymentReceiptTime.Text;
            entity.RegistrationNo = AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH"
                                        ? txtRegistrationNo.Text
                                        : cboRegistrationNo.SelectedValue;
            entity.PrintReceiptAsName = txtPrintReceiptAsName.Text;
            entity.Notes = txtNotes.Text;


            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            //Update Detil
            foreach (TransPaymentReceiptItem item in TransPaymentReceiptItems)
            {
                item.PaymentReceiptNo = txtPaymentReceiptNo.Text;
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }
        }

        private void SaveEntity(TransPaymentReceipt entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                TransPaymentReceiptItems.Save();

                //autonumber has been saved on SetEntity
                //if (DataModeCurrent == DataMode.New)
                //    _autoNumber.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new TransPaymentReceiptQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.PaymentReceiptNo > txtPaymentReceiptNo.Text);
                que.OrderBy(que.PaymentReceiptNo.Ascending);
            }
            else
            {
                que.Where(que.PaymentReceiptNo < txtPaymentReceiptNo.Text);
                que.OrderBy(que.PaymentReceiptNo.Descending);
            }
            var entity = new TransPaymentReceipt();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged
        protected void cboRegistrationNo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(cboRegistrationNo.SelectedValue))
            {
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                txtMedicalNo.Text = pat.MedicalNo;
                txtPatientName.Text = pat.PatientName;
                txtPrintReceiptAsName.Text = pat.PatientName;
                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;

                var su = new ServiceUnit();
                su.LoadByPrimaryKey(reg.ServiceUnitID);
                txtServiceUnitName.Text = su.ServiceUnitName;
            }
            else
            {
                txtMedicalNo.Text = string.Empty;
                txtPatientName.Text = string.Empty;
                txtPrintReceiptAsName.Text = string.Empty;
                txtSalutation.Text = string.Empty;
                txtServiceUnitName.Text = string.Empty;  
            }
        }
        protected void txtRegistrationNo_TextChanged(object sender, EventArgs e)
        {
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                txtMedicalNo.Text = pat.MedicalNo;
                txtPatientName.Text = pat.PatientName;
                txtPrintReceiptAsName.Text = pat.PatientName;
                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;

                var su = new ServiceUnit();
                su.LoadByPrimaryKey(reg.ServiceUnitID);
                txtServiceUnitName.Text = su.ServiceUnitName;
            }
            else
            {
                txtMedicalNo.Text = string.Empty;
                txtPatientName.Text = string.Empty;
                txtPrintReceiptAsName.Text = string.Empty;
                txtSalutation.Text = string.Empty;
                txtServiceUnitName.Text = string.Empty;
            }
        }
        #endregion

        #region Record Detail Method Function

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdTransPaymentReceiptItem.Columns[0].Visible = isVisible;
            grdTransPaymentReceiptItem.Columns[grdTransPaymentReceiptItem.Columns.Count - 1].Visible = isVisible;

            //grdTransPaymentReceiptItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                TransPaymentReceiptItems = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdTransPaymentReceiptItem.Rebind();
        }

        private TransPaymentReceiptItemCollection TransPaymentReceiptItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collTransPaymentReceiptItem" + Request.UserHostName];
                    if (obj != null)
                        return ((TransPaymentReceiptItemCollection)(obj));
                }

                var coll = new TransPaymentReceiptItemCollection();
                var query = new TransPaymentReceiptItemQuery("a");
                var py = new TransPaymentQuery("b");
                
                query.InnerJoin(py).On(query.PaymentNo == py.PaymentNo);
                query.Where(query.PaymentReceiptNo == txtPaymentReceiptNo.Text);
                query.OrderBy
                    (
                        query.PaymentNo.Ascending
                    );

                query.Select
                    (
                        query.PaymentReceiptNo,
                        query.PaymentNo,
                        py.PaymentDate.As("refToTransPayment_PaymentDate"),
                        py.PaymentTime.As("refToTransPayment_PaymentTime"),
                        py.PrintReceiptAsName.As("refToTransPayment_PrintReceiptAsName"),
                        py.Notes.As("refToTransPayment_Notes"),
                        query.Amount,
                        query.LastUpdateByUserID,
                        query.LastUpdateDateTime
                    );

                coll.Load(query);

                Session["collTransPaymentReceiptItem" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collTransPaymentReceiptItem" + Request.UserHostName] = value; }
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            TransPaymentReceiptItems = null; //Reset Record Detail
            grdTransPaymentReceiptItem.DataSource = TransPaymentReceiptItems;
            grdTransPaymentReceiptItem.MasterTableView.IsItemInserted = false;
            grdTransPaymentReceiptItem.MasterTableView.ClearEditItems();
            grdTransPaymentReceiptItem.DataBind();
        }

        protected void grdTransPaymentReceiptItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdTransPaymentReceiptItem.DataSource = TransPaymentReceiptItems;
        }

        private TransPaymentReceiptItem FindTransPaymentReceiptItem(String paymentNo)
        {
            TransPaymentReceiptItemCollection coll = TransPaymentReceiptItems;
            TransPaymentReceiptItem retEntity = null;
            foreach (TransPaymentReceiptItem rec in coll)
            {
                if (rec.PaymentNo.Equals(paymentNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdTransPaymentReceiptItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String paymentNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][TransPaymentReceiptItemMetadata.ColumnNames.PaymentNo]);
            TransPaymentReceiptItem entity = FindTransPaymentReceiptItem(paymentNo);
            if (entity != null)
            {
                entity.MarkAsDeleted();
            }

            CalculateDetailReceipt();
        }

        #endregion

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument == "rebind")
            {
                CalculateDetailReceipt();
                grdTransPaymentReceiptItem.Rebind();
            }
            
        }

        private void CalculateDetailReceipt()
        {
            if (TransPaymentReceiptItems.Count > 0)
            {
                decimal? total = 0;

                total = TransPaymentReceiptItems.Aggregate(total, (current, c) => current + c.Amount);

                txtTotalReceipt.Value = Convert.ToDouble(total);
            }
        }
    }
}
