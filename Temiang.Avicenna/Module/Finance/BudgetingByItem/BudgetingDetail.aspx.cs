using System;
using System.Data;
using System.Linq;
//using System.Web.UI;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using DevExpress.XtraSplashScreen;
using Telerik.Web.Data.Extensions;

namespace Temiang.Avicenna.Module.Finance.BudgetingByItem
{
    public partial class BudgetingDetail : BasePageDetail
    {
        private bool isApprovalModule {
            get {
                return Request.QueryString["Approval"] == null ? false : Request.QueryString["Approval"].Equals("1");
            }
        }

        private BudgetingDetailItemCollection BudgetingDetailItems {
            get {
                if (Session["BudgetingDetailItems"] == null) {
                    var bdColl = new BudgetingDetailItemCollection();
                    var bdi = new BudgetingDetailItemQuery("bdi");
                    var i = new ItemQuery("i");
                    bdi.InnerJoin(i).On(bdi.ItemID == i.ItemID)
                        .Select(
                            bdi,
                            i.ItemName.As("refTo_ItemName")
                        ).Where(bdi.BudgetingNo == txtBudgetingNo.Text, bdi.Revision == txtRev.Text);
                    bdColl.Load(bdi);
                    Session["BudgetingDetailItems"] = bdColl;
                }
                return Session["BudgetingDetailItems"] as BudgetingDetailItemCollection;
            }
            set {
                Session["BudgetingDetailItems"] = value;
            }
        }

        private AppAutoNumberLast GetAutoNumber()
        {
            if (cboServiceUnitID.SelectedValue == string.Empty)
                return null;
            if (cboYear.Text == string.Empty)
                return null;

            var serv = new ServiceUnit();
            if (serv.LoadByPrimaryKey(cboServiceUnitID.SelectedValue))
            {
                return Helper.GetNewAutoNumber(new DateTime(Convert.ToInt32(cboYear.Text), 12, 31), BusinessObject.Reference.TransactionCode.BudgetPlan, serv.DepartmentID);
            }
            else {
                return null;
            }
        }

        private void PopulateYearForComboYear()
        {
            
            var tc = new AppAutoNumberTransactionCode();
            if(tc.LoadByPrimaryKey(BusinessObject.Reference.TransactionCode.BudgetPlan))
            {
                var au = new AppAutoNumber();
                var cAu = new AppAutoNumberCollection();
                var qAu = new AppAutoNumberQuery();
                qAu.Where(qAu.SRAutoNumber == tc.SRAutoNumber);
                cAu.Load(qAu);

                // start from year of efective date of budgetplan
                int year = cAu[0].EffectiveDate.Value.Year;

                var lYear = new System.Collections.Generic.List<string>();
                for (var i = DateTime.Now.Year + 1; i >= year; i--)
                {
                    lYear.Add(i.ToString());
                    cboYear.Items.Add(new RadComboBoxItem(i.ToString()));
                }
            }
            
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "BudgetingSearch.aspx";
            UrlPageList = string.Format("BudgetingList.aspx?Approval={0}", Request.QueryString["Approval"]);

            IsUsingBeforeVoidValidation = true;

            if (isApprovalModule)
            {
                ProgramID = AppConstant.Program.BUDGETING_APPROVAL;
            }
            else
                ProgramID = AppConstant.Program.BUDGETING;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, BusinessObject.Reference.TransactionCode.BudgetPlan, !isApprovalModule);
                StandardReference.InitializeIncludeSpace(cboSRBudgetingGroup, AppEnum.StandardReference.BudgetingGroup);

                var bq = new BusinessObject.BudgetingQuery();
                bq.Select(bq.Periode).OrderBy(bq.Periode.Descending);
                bq.es.Distinct = true;
                var dtb = bq.LoadDataTable();
                foreach (DataRow row in dtb.Rows) {
                    cboYear.Items.Add((new RadComboBoxItem(row["Periode"].ToString(), row["Periode"].ToString())));
                }
                var thisYear = DateTime.Now.Year.ToString();
                if (cboYear.Items.FindItemByValue(thisYear) == null) {
                    cboYear.Items.Insert(0,(new RadComboBoxItem(thisYear, thisYear)));
                }
                var nextYear = (DateTime.Now.Year + 1).ToString();
                if (cboYear.Items.FindItemByValue(nextYear) == null)
                {
                    cboYear.Items.Insert(0,(new RadComboBoxItem(nextYear, nextYear)));
                }

                txtRev.Text = "1";

                // reset detailitem
                BudgetingDetailItems = null;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

                UpdateToolbarMenu();
        }

        //protected override void OnLoadComplete(EventArgs e)
        //{
        //    if (!Page.IsPostBack) {
        //        base.OnLoadComplete(e);
        //    }
        //}

        private void UpdateToolbarMenu() {
            if (isApprovalModule)
            {
                var bgt = new BusinessObject.Budgeting();
                if (bgt.LoadByPrimaryKey(txtBudgetingNo.Text))
                {
                    if (bgt.SRBudgetingVerifyStatus == "02")
                    {
                        ToolBarMenuEdit.Enabled = false;
                        ToolBarMenuApproval.Enabled = false;
                        ToolBarMenuVoid.Enabled = false;
                    }
                }
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            //ajax.AddAjaxSetting(cboFromServiceUnitID, txtTransactionNo);
            //ajax.AddAjaxSetting(cboFromServiceUnitID, cboSRItemType);

            //ajax.AddAjaxSetting(grdItemTransactionItem, grdItemTransactionItem);
            //ajax.AddAjaxSetting(grdItemTransactionItem, cboFromServiceUnitID);
            //ajax.AddAjaxSetting(grdItemTransactionItem, cboSRItemType);
        }

        //protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        //{
        //    base.RaisePostBackEvent(sourceControl, eventArgument);

        //    if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
        //        return;

        //    if (eventArgument == "edit")
        //    {
        //        //RedirectToPageDetail();
        //    }
        //}

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuEditClick()
        {

        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            // cek budgetnya sudah approve atau belum
            var bg = new BusinessObject.Budgeting();
            if (!bg.LoadByPrimaryKey(txtBudgetingNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (bg.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            if (isApprovalModule) {
                if (!(bg.IsApprove ?? false))
                {
                    args.MessageText = AppConstant.Message.RecordHasNotApproved + " (Previous Step)";
                    args.IsCancel = true;
                    return;
                }

                if (bg.SRBudgetingVerifyStatus == "03")
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return;
                }

                if (bg.SRBudgetingVerifyStatus == "02")
                {
                    args.MessageText = "This data has been rejected for correction.";
                    args.IsCancel = true;
                    return;
                }

                bg.SRBudgetingVerifyStatus = "03";
                bg.VerifiedDateTime = DateTime.Now;
                bg.VerifiedByUserID = AppSession.UserLogin.UserID;
            }
            else
            {
                if (bg.IsApprove ?? false)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return;
                }

                bg.IsApprove = true;
                //bg.SRBudgetingVerifyStatus = "01"; // Submission
                bg.ApprovedDateTime = DateTime.Now;
                bg.ApprovedByUserID = AppSession.UserLogin.UserID;
                bg.LastUpdateDateTime = DateTime.Now;
                bg.LastUpdateByUserID = AppSession.UserLogin.UserID;   
            }

            using (var trans = new esTransactionScope())
            {
                bg.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            // cek budgetnya sudah approve atau belum
            var bg = new BusinessObject.Budgeting();
            if (!bg.LoadByPrimaryKey(txtBudgetingNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (!(bg.IsApprove ?? false))
            {
                args.MessageText = AppConstant.Message.RecordHasNotApproved;
                args.IsCancel = true;
                return;
            }
            if (bg.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            if (isApprovalModule)
            {
                if (bg.SRBudgetingVerifyStatus != "03")
                {
                    args.MessageText = AppConstant.Message.RecordHasNotApproved;
                    args.IsCancel = true;
                    return;
                }

                bg.SRBudgetingVerifyStatus = "01";
                bg.LastUpdateDateTime = DateTime.Now;
                bg.LastUpdateByUserID = AppSession.UserLogin.UserID;

                using (var trans = new esTransactionScope())
                {
                    bg.Save();
                    trans.Complete();
                }
            }
            else
            {
                if (bg.SRBudgetingVerifyStatus == "03")
                {
                    args.MessageText = AppConstant.Message.RecordHasVerified;
                    args.IsCancel = true;
                    return;
                }

                bg.IsApprove = false;
                bg.LastUpdateDateTime = DateTime.Now;
                bg.LastUpdateByUserID = AppSession.UserLogin.UserID;

                var bgDetailColl = new BudgetingDetailCollection();
                bgDetailColl.Query.Where(bgDetailColl.Query.BudgetingNo == bg.BudgetingNo,
                    bgDetailColl.Query.Revision == bg.Revision);
                bgDetailColl.LoadAll();

                var bgDetailItemColl = new BudgetingDetailItemCollection();
                bgDetailItemColl.Query.Where(bgDetailItemColl.Query.BudgetingNo == bg.BudgetingNo,
                    bgDetailItemColl.Query.Revision == bg.Revision);
                bgDetailItemColl.LoadAll();


                if (bg.SRBudgetingVerifyStatus == "02")
                {
                    bg.Revision = bg.Revision + 1;
                    bg.SRBudgetingVerifyStatus = "01"; // set kembali sebagai submission

                    foreach (var bgd in bgDetailColl)
                    {
                        bgd.MarkAllColumnsAsDirty(DataRowState.Added);
                        bgd.Revision = bg.Revision;
                        bgd.CreatedDateTime = DateTime.Now;
                        bgd.CreatedByUserID = AppSession.UserLogin.UserID;
                        bgd.LastUpdateDateTime = DateTime.Now;
                        bgd.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }

                    foreach (var bgdi in bgDetailItemColl)
                    {
                        bgdi.MarkAllColumnsAsDirty(DataRowState.Added);
                        bgdi.Revision = bg.Revision;
                        bgdi.CreatedDateTime = DateTime.Now;
                        bgdi.CreatedByUserID = AppSession.UserLogin.UserID;
                        bgdi.LastUpdateDateTime = DateTime.Now;
                        bgdi.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                }

                using (var trans = new esTransactionScope())
                {
                    bg.Save();
                    bgDetailColl.Save();
                    bgDetailItemColl.Save();

                    trans.Complete();
                }

                BudgetingDetailItems = null;
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            // cek budgetnya sudah approve atau belum
            var bg = new BusinessObject.Budgeting();
            if (!bg.LoadByPrimaryKey(txtBudgetingNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            if (bg.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            if (isApprovalModule)
            {
                if (!(bg.IsApprove ?? false))
                {
                    args.MessageText = AppConstant.Message.RecordHasNotApproved + " (Previous Step)";
                    args.IsCancel = true;
                    return;
                }

                if (bg.SRBudgetingVerifyStatus == "02")
                {
                    args.MessageText = "This data has been rejected for correction.";
                    args.IsCancel = true;
                    return;
                }

                if (bg.SRBudgetingVerifyStatus == "03")
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved; ;
                    args.IsCancel = true;
                    return;
                }

                bg.SRBudgetingVerifyStatus = "02";
                bg.CorrectionNotes = args.ReasonText;
                bg.LastUpdateDateTime = DateTime.Now;
                bg.LastUpdateByUserID = AppSession.UserLogin.UserID; 
            }
            else
            {
                if (bg.IsApprove ?? false)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return;
                }
                
                if (bg.SRBudgetingVerifyStatus == "03")
                {
                    args.MessageText = AppConstant.Message.RecordHasVerified;
                    args.IsCancel = true;
                    return;
                }

                bg.IsVoid = true;
                bg.VoidByUserID = AppSession.UserLogin.UserID;
                bg.VerifiedDateTime = DateTime.Now;
                bg.VoidNotes = args.ReasonText;
                bg.LastUpdateByUserID = AppSession.UserLogin.UserID; 
                bg.LastUpdateDateTime = DateTime.Now;
            }

            using (var trans = new esTransactionScope())
            {
                bg.Save();

                trans.Complete();
            }

            UpdateToolbarMenu();
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            // cek budgetnya sudah approve atau belum
            var bg = new BusinessObject.Budgeting();
            if (!bg.LoadByPrimaryKey(txtBudgetingNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (!(bg.IsVoid ?? false))
            {
                args.MessageText = "This data has not been voided";
                args.IsCancel = true;
                return;
            }

            var dtime = DateTime.Now;

            bg.IsVoid = false;
            bg.LastUpdateDateTime = dtime;
            bg.LastUpdateByUserID = AppSession.UserLogin.UserID;

            using (var trans = new esTransactionScope())
            {
                bg.Save();

                trans.Complete();
            }
        }

        private bool IsApprovedOrVoid(ItemTransaction entity, ValidateArgs args)
        {
            if (entity.IsApproved.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            if (entity.IsVoid.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        protected override void OnMenuNewClick()
        {

        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            //ItemTransaction entity = new ItemTransaction();
            //if (entity.LoadByPrimaryKey(txtApprovalNo.Text))
            //{
            //    ItemTransactionItems.MarkAllAsDeleted();

            //    entity.MarkAsDeleted();
            //    //SaveEntity(entity);

            //    using (esTransactionScope trans = new esTransactionScope())
            //    {
            //        ItemTransactionItems.Save();
            //        entity.Save();
                    
            //        trans.Complete();
            //    }
            //}
            //else
            //    args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            OnMenuSaveEditClick(args);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            // validate save
            if (cboServiceUnitID.SelectedValue == string.Empty) {
                args.MessageText = "Invalid service unit";
                args.IsCancel = true;
                return;
            }
            if (cboYear.SelectedValue == string.Empty)
            {
                args.MessageText = "Invalid period";
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(txtBudgetingNo.Text)) {
                var bColl = new BudgetingCollection();
                bColl.Query.Where(bColl.Query.ServiceUnitID == cboServiceUnitID.SelectedValue,
                    bColl.Query.Periode == cboYear.SelectedValue, bColl.Query.IsVoid == false,
                    bColl.Query.SRBudgetingGroup == cboSRBudgetingGroup.SelectedValue,
                    bColl.Query.BudgetingNo != txtBudgetingNo.Text);
                if (bColl.LoadAll()) {
                    args.MessageText = "Selected period and group has exist";
                    args.IsCancel = true;
                    return;
                }
            }
            

            if (grdItemTransactionItem.MasterTableView.Items.Count == 0) {
                args.MessageText = "Detail can not be empty";
                args.IsCancel = true;
                return;
            }

            string ret = SaveEntity();
            if (ret != string.Empty) {
                args.MessageText = ret;
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
            auditLogFilter.PrimaryKeyData = string.Format("BudgetingNo='{0}'", txtBudgetingNo.Text.Trim());
            auditLogFilter.TableName = "Budgeting";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_BudgetingNo", txtBudgetingNo.Text);
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuEdit()
        {
            return txtBudgetingNo.Text != string.Empty;
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
            UpdateToolbarMenu();

            //cboSRBudgetingGroup.Enabled = newVal == AppEnum.DataMode.New;

            RefreshCommandItemGrid(oldVal, newVal);

            grdItemTransactionItem.Rebind();

            //if (oldVal == DataMode.New && newVal == DataMode.Read)
            //{ 
            //    // redirect to list
            //    string url = string.Format("BudgetPlanApprovalList.aspx");
            //    Page.Response.Redirect(url, true);
            //}
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemTransactionItem.Columns[0].Visible = isVisible;
            grdItemTransactionItem.Columns[grdItemTransactionItem.Columns.Count - 1].Visible = isVisible;
            grdItemTransactionItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            BusinessObject.Budgeting entity = new BusinessObject.Budgeting();
            if (parameters.Length > 0)
            {
                String BudgetingNo = (String)parameters[0];

                entity.LoadByPrimaryKey(BudgetingNo);
            }
            else
            {
                if (!txtBudgetingNo.Text.Equals(string.Empty))
                {
                    entity.LoadByPrimaryKey(txtBudgetingNo.Text);
                }
                else
                {
                    entity.LoadByPrimaryKey(txtBudgetingNo.Text);
                }
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            BusinessObject.Budgeting bgt = (BusinessObject.Budgeting)entity;

            if (isApprovalModule)
            {
                //
                bgt.IsApprove = (bgt.IsApprove ?? false) && (bgt.SRBudgetingVerifyStatus == "03");
                var bar = ToolBarMenuVoid;
                bar.Text = "Reject";
            }

            txtBudgetingNo.Text = bgt.BudgetingNo;

            cboYear.SelectedValue = bgt.Periode.HasValue ? bgt.Periode.Value.ToString() : "";
            cboServiceUnitID.SelectedValue = bgt.ServiceUnitID;
            //ComboBox.PopulateWithOneServiceUnit(cboFromServiceUnitID, itemTransaction.FromServiceUnitID ?? string.Empty);
            //cboToServiceUnitID.SelectedValue = itemTransaction.ToServiceUnitID;
            //ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboFromServiceUnitID.SelectedValue, BusinessObject.Reference.TransactionCode.BudgetPlan);
            chkIsVoid.Checked = bgt.IsVoid ?? false;
            chkIsApproved.Checked = bgt.IsApprove ?? false;
            txtNotes.Text = bgt.Notes;
            ComboBox.SetValue(cboSRBudgetingGroup, bgt.SRBudgetingGroup);
            txtRev.Text = (bgt.Revision ?? 1).ToString();

            //Status
            if (!string.IsNullOrEmpty(bgt.BudgetingNo))
            {
                var appstd = new AppStandardReferenceItemCollection();
                appstd.Query.Where(appstd.Query.StandardReferenceID == "BudgetingVerifyStatus");
                if (appstd.LoadAll())
                {
                    lblStatus.Text = appstd.Where(x => x.ItemID == bgt.SRBudgetingVerifyStatus).FirstOrDefault().ItemName;

                    tblStatus.Attributes.Remove("Class");
                    tblStatus.Attributes.Add("Class", 
                        string.Format(
                            "info {0}", bgt.SRBudgetingVerifyStatus == "01" ? "warning" : (
                                bgt.SRBudgetingVerifyStatus == "02" ? "error" : "success"
                             )
                        )
                    );
                }
            }

            //Display Data Detail
            grdItemTransactionItem.DataSource = null;
            grdItemTransactionItem.Rebind();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(BusinessObject.Budgeting entity, BusinessObject.BudgetingDetailCollection itemColl)
        {
            entity.BudgetingNo = txtBudgetingNo.Text;
            entity.ServiceUnitID = cboServiceUnitID.SelectedValue;
            entity.Periode = Convert.ToInt32(cboYear.SelectedValue);
            entity.Revision = Convert.ToInt32(txtRev.Text);
            entity.Notes = txtNotes.Text;
            entity.SRBudgetingGroup = cboSRBudgetingGroup.SelectedValue;
            
            if (entity.es.IsAdded)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;// DateTime.Now;
            }
            //Last Update Status
            if (entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;// DateTime.Now;
            }
        }

        private string SaveEntity()
        {
            BusinessObject.Budgeting entity = new Temiang.Avicenna.BusinessObject.Budgeting();

            int Revision = System.Convert.ToInt32(txtRev.Text);

            if (txtBudgetingNo.Text == string.Empty)
            {
                entity.AddNew();
                entity.CreatedByUserID = AppSession.UserLogin.UserID;
                entity.CreatedDateTime = DateTime.Now;
                entity.IsByItem = true;
            }
            else {
                if (!entity.LoadByPrimaryKey(txtBudgetingNo.Text)) {
                    return AppConstant.Message.RecordNotExist;
                }

                if (isApprovalModule && entity.SRBudgetingVerifyStatus == "03") {
                    return AppConstant.Message.RecordHasApproved;
                }
                if ((!isApprovalModule) && (entity.IsApprove ?? false))
                {
                    return AppConstant.Message.RecordHasApproved;
                }

                if (isApprovalModule && entity.SRBudgetingVerifyStatus == "02")
                {
                    return "This data has been rejected for correction.";
                }
                if ((!isApprovalModule) && (entity.IsVoid ?? false))
                {
                    return AppConstant.Message.RecordHasVoided;
                }

                //if (entity.ServiceUnitID != cboServiceUnitID.SelectedValue) {
                //    return "Service unit can not be changed";
                //}
                //if (entity.Periode.ToString() != cboYear.SelectedValue)
                //{
                //    return "Period can not be changed";
                //}
            }

            if (!isApprovalModule) {
                entity.SRBudgetingVerifyStatus = "01"; //Submission
                entity.Revision = Revision;
                entity.IsApprove = false;
                entity.IsVoid = false;
            }

            entity.ServiceUnitID = cboServiceUnitID.SelectedValue;
            entity.Periode = System.Convert.ToInt32(cboYear.SelectedValue);
            entity.Notes = txtNotes.Text;
            entity.SRBudgetingGroup = cboSRBudgetingGroup.SelectedValue;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;

            entity.SumLimit = BudgetingDetailItems.Where(x => x.BudgetingNo == txtBudgetingNo.Text).Sum(x => x.Qty * x.Price);

            using (esTransactionScope trans = new esTransactionScope())
            {
                if(txtBudgetingNo.Text == string.Empty){
                    var autono = GetAutoNumber();
                    if(autono == null) {
                        return "Generating transaction number failed";
                    }
                    txtBudgetingNo.Text = autono.LastCompleteNumber;
                    entity.BudgetingNo = txtBudgetingNo.Text;
                    autono.Save();
                }

                // detail item
                foreach (var i in BudgetingDetailItems.Where(d => d.es.IsAdded || d.es.IsModified)){
                    if (i.es.IsAdded)
                    {
                        i.BudgetingNo = txtBudgetingNo.Text;
                        i.CreatedByUserID = AppSession.UserLogin.UserID;
                        i.CreatedDateTime = DateTime.Now;
                        i.LastUpdateByUserID = i.CreatedByUserID;
                        i.LastUpdateDateTime = i.CreatedDateTime;
                    }
                    if (i.es.IsModified) {
                        i.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        i.LastUpdateDateTime = DateTime.Now;
                    }
                }

                var bdColl = new BudgetingDetailCollection();
                bdColl.SetByBudgetingDetailItem(entity, BudgetingDetailItems);

                entity.Save();
                BudgetingDetailItems.Save();
                bdColl.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            return string.Empty;
        }

        private void MoveRecord(bool isNextRecord)
        {
            BusinessObject.BudgetingQuery que = new BusinessObject.BudgetingQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.BudgetingNo > txtBudgetingNo.Text
                    );
                que.OrderBy(que.BudgetingNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.BudgetingNo < txtBudgetingNo.Text
                    );
                que.OrderBy(que.BudgetingNo.Descending);
            }

            BusinessObject.Budgeting entity = new BusinessObject.Budgeting();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function
        
        protected void grdItemTransactionItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemTransactionItem.DataSource = BudgetingDetailItems;
            var bdiColl = BudgetingDetailItems;

            lblTotalItem.Text = BudgetingDetailItems.Count.ToString("n2");
            lblSumBudgetAmount.Text = (BudgetingDetailItems.Sum(x => x.Qty * x.Price) ?? 0).ToString("n2");
        }

        protected void grdItemTransactionItem_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;
            String itemID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][BudgetingDetailItemMetadata.ColumnNames.ItemID]);
            var entity = FindItemGrid(itemID);
            if (entity != null)
                SetEntityDetailItemValue(entity, e);
        }

        protected void grdItemTransactionItem_InsertCommand(object sender, GridCommandEventArgs e)
        {
            var entity = BudgetingDetailItems.AddNew();
            SetEntityDetailItemValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItemTransactionItem.Rebind();
        }

        protected void grdItemTransactionItem_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][BudgetingDetailItemMetadata.ColumnNames.ItemID]);
            var entity = FindItemGrid(itemID);
            if (entity != null)
            {
                entity.MarkAsDeleted();
            }
        }

        private void SetEntityDetailItemValue(BusinessObject.BudgetingDetailItem entity, GridCommandEventArgs e)
        {
            var userControl = (BudgetingDetailItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;

                entity.QtyMonth01 = userControl.QtyMonth01;
                entity.QtyMonth02 = userControl.QtyMonth02;
                entity.QtyMonth03 = userControl.QtyMonth03;
                entity.QtyMonth04 = userControl.QtyMonth04;
                entity.QtyMonth05 = userControl.QtyMonth05;
                entity.QtyMonth06 = userControl.QtyMonth06;
                entity.QtyMonth07 = userControl.QtyMonth07;
                entity.QtyMonth08 = userControl.QtyMonth08;
                entity.QtyMonth09 = userControl.QtyMonth09;
                entity.QtyMonth10 = userControl.QtyMonth10;
                entity.QtyMonth11 = userControl.QtyMonth11;
                entity.QtyMonth12 = userControl.QtyMonth12;

                entity.Qty = userControl.Qty;
                entity.SRItemUnit = userControl.SRItemUnit;
                entity.ConversionFactor = userControl.ConversionFactor;

                entity.Price = userControl.Price;

                entity.ChartOfAccountID = 0;
                entity.Revision = System.Convert.ToInt32(txtRev.Text);
                entity.BudgetingNo = txtBudgetingNo.Text;

                entity.IsAsset = false;
                entity.IsAssetApproved = false;
                entity.IsAssetRejected = false;
            }
        }

        private BusinessObject.BudgetingDetailItem FindItemGrid(string ItemID)
        {
            return BudgetingDetailItems.Where(x => x.BudgetingNo == txtBudgetingNo.Text && x.Revision.ToString() == txtRev.Text && x.ItemID == ItemID).FirstOrDefault();
        }

        #endregion

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //PopulateNewTransactionNo();
            //ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboServiceUnitID.SelectedValue, BusinessObject.Reference.TransactionCode.BudgetPlan);
            //if (cboSRItemType.Items.Count == 2) cboSRItemType.SelectedIndex = 1;
        }
    }
}