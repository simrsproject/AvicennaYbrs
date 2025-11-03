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
using System.Text;
using System.Web.UI;
using System.Configuration;
using System.IO;

namespace Temiang.Avicenna.Module.Finance.Budgeting
{
    public partial class BudgetingDetail : BasePageDetail
    {
        private bool isApprovalModule
        {
            get
            {
                return Request.QueryString["Approval"] == null ? false : Request.QueryString["Approval"].Equals("1");
            }
        }

        private BudgetingDetailItemCollection BudgetingDetailItems
        {
            get
            {
                if (Session["BudgetingDetailItems"] == null)
                {
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
            set
            {
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
            else
            {
                return null;
            }
        }

        private void PopulateYearForComboYear()
        {

            var tc = new AppAutoNumberTransactionCode();
            if (tc.LoadByPrimaryKey(BusinessObject.Reference.TransactionCode.BudgetPlan))
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

                var bq = new BusinessObject.BudgetingQuery();
                bq.Select(bq.Periode).OrderBy(bq.Periode.Descending);
                bq.es.Distinct = true;
                var dtb = bq.LoadDataTable();
                foreach (DataRow row in dtb.Rows)
                {
                    cboYear.Items.Add((new RadComboBoxItem(row["Periode"].ToString(), row["Periode"].ToString())));
                }
                var thisYear = DateTime.Now.Year.ToString();
                if (cboYear.Items.FindItemByValue(thisYear) == null)
                {
                    cboYear.Items.Insert(0, (new RadComboBoxItem(thisYear, thisYear)));
                }
                var nextYear = (DateTime.Now.Year + 1).ToString();
                if (cboYear.Items.FindItemByValue(nextYear) == null)
                {
                    cboYear.Items.Insert(0, (new RadComboBoxItem(nextYear, nextYear)));
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

        private void UpdateToolbarMenu()
        {
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

            // EmailNotif
            bool IsSendEmail = false;

            if (isApprovalModule)
            {
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


                IsSendEmail = true;
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

            if (IsSendEmail) {
                // send the email here
                var emailAddrs = AppParameter.GetParameterValue(AppParameter.ParameterItem.EmailAddressHO);
                if (!(bg.IsByItem ?? false)) {
                    var bgdiColl = new BudgetingDetailItemCollection();
                    bgdiColl.Query.Where(
                        bgdiColl.Query.BudgetingNo == bg.BudgetingNo,
                        bgdiColl.Query.Revision == bg.Revision,
                        bgdiColl.Query.IsAsset == true
                    );
                    if (bgdiColl.LoadAll()) {
                        var bgdiToApprove = bgdiColl.Where(b =>
                            (b.Qty * b.Price) > AppSession.Parameter.BudgetOfAssetNeedExtraApprovalLimit &&
                            AppSession.Parameter.BudgetOfAssetNeedExtraApprovalLimit > 0);

                        var bodyEmail = new StringBuilder();
                        var itemColl = new ItemCollection();
                        if (bgdiToApprove.Any()) {
                            itemColl.Query.Where(itemColl.Query.ItemID.In(bgdiToApprove.Select(s => s.ItemID)));
                            itemColl.LoadAll();
                        }

                        var ctr = 1;
                        foreach (var bgdi in bgdiToApprove) {
                            var i = itemColl.Where(x => x.ItemID == bgdi.ItemID).FirstOrDefault();
                            if (i != null) {
                                bodyEmail.Append(string.Format("{0}. {1} senilai {2}", ctr.ToString(), i.ItemName, ((bgdi.Qty ?? 0) * (bgdi.Price ?? 0)).ToString("N2")));
                                //bodyEmail.Append(Environment.NewLine);
                                bodyEmail.AppendLine();
                                ctr++;
                            }
                        }

                        if (bodyEmail.Length > 0) {
                            bodyEmail.Insert(0, string.Format("{0}{1}{2}", "Mohon untuk di-approve pengajuan untuk item berikut: ", Environment.NewLine, Environment.NewLine));
                            bodyEmail.AppendLine();
                            bodyEmail.Append("Anda bisa membuka aplikasi di satuan kerja terkait untuk melakukan approval");
                            bodyEmail.AppendLine();
                            bodyEmail.Append("Ini adalah email otomatis dan tidak perlu untuk dibalas.");

                            Mail.SendMailUseOtherThread(emailAddrs, "Persetujuan pengajuan barang", bodyEmail.ToString());
                        }
                    }  
                }
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

                var bHist = new BudgetingHistory();

                if (bg.SRBudgetingVerifyStatus == "02")
                {
                    bHist.AddNew();
                    bHist.BudgetingNo = bg.BudgetingNo;
                    bHist.ServiceUnitID = bg.ServiceUnitID;
                    bHist.Periode = bg.Periode;
                    bHist.Revision = bg.Revision;
                    bHist.SumLimit = bg.SumLimit;
                    bHist.CreatedByUserID = bg.CreatedByUserID;
                    bHist.CreatedDateTime = bg.CreatedDateTime;
                    bHist.LastUpdateByUserID = bg.LastUpdateByUserID;
                    bHist.LastUpdateDateTime = bg.LastUpdateDateTime;
                    bHist.IsApprove = bg.IsApprove;
                    bHist.ApprovedByUserID = bg.ApprovedByUserID;
                    bHist.ApprovedDateTime = bg.ApprovedDateTime;
                    bHist.IsVoid = bg.IsVoid;
                    bHist.VoidByUserID = bg.VoidByUserID;
                    bHist.VoidDateTime = bg.VoidDateTime;
                    bHist.VoidNotes = bg.VoidNotes;
                    bHist.SRBudgetingVerifyStatus = bg.SRBudgetingVerifyStatus;
                    bHist.VerifiedByUserID = bg.VerifiedByUserID;
                    bHist.VerifiedDateTime = bg.VerifiedDateTime;
                    bHist.Notes = bg.Notes;
                    bHist.CorrectionNotes = bg.CorrectionNotes;
                    bHist.IsByItem = bg.IsByItem;

                    bg.Revision = bg.Revision + 1;
                    bg.SRBudgetingVerifyStatus = "01"; // set kembali sebagai submission
                    bg.CorrectionNotes = string.Empty;

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
                    if (!string.IsNullOrEmpty( bHist.BudgetingNo))
                        bHist.Save();

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
            if (cboServiceUnitID.SelectedValue == string.Empty)
            {
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

            if (string.IsNullOrEmpty(txtBudgetingNo.Text))
            {
                var bColl = new BudgetingCollection();
                bColl.Query.Where(bColl.Query.ServiceUnitID == cboServiceUnitID.SelectedValue,
                    bColl.Query.Periode == cboYear.SelectedValue, bColl.Query.IsVoid == false);
                if (bColl.LoadAll())
                {
                    args.MessageText = "Selected period has exist";
                    args.IsCancel = true;
                    return;
                }
            }


            if (grdItemTransactionItem.MasterTableView.Items.Count == 0)
            {
                args.MessageText = "Detail can not be empty";
                args.IsCancel = true;
                return;
            }

            string ret = SaveEntity();
            if (ret != string.Empty)
            {
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

            grdItemTransactionItem.Rebind();

            btnCopy.Visible = newVal != AppEnum.DataMode.Read;
            btnReset.Visible = newVal != AppEnum.DataMode.Read;

            //RefreshCommandItemGrid(oldVal, newVal);

            //if (oldVal == DataMode.New && newVal == DataMode.Read)
            //{ 
            //    // redirect to list
            //    string url = string.Format("BudgetPlanApprovalList.aspx");
            //    Page.Response.Redirect(url, true);
            //}
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

                    var RejectNotes = "";
                    if (!string.IsNullOrEmpty(bgt.CorrectionNotes)) RejectNotes = string.Format("Rev {0}: {1}{2}", bgt.Revision.ToString(), bgt.CorrectionNotes, Environment.NewLine);

                    var bHistColl = new BudgetingHistoryCollection();
                    bHistColl.Query.Where(bHistColl.Query.BudgetingNo == bgt.BudgetingNo).OrderBy(bHistColl.Query.Revision.Descending);
                    bHistColl.LoadAll();
                    foreach (var bHist in bHistColl)
                    {
                        if (!string.IsNullOrEmpty(bHist.CorrectionNotes))
                        {
                            RejectNotes += string.Format("Rev {0}: {1}{2}", bHist.Revision.ToString(), bHist.CorrectionNotes, Environment.NewLine);
                        }
                    }
                    if (!string.IsNullOrEmpty(RejectNotes))
                    {
                        lblStatus.ToolTip = RejectNotes;
                    }
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
            BusinessObject.BudgetingDetailCollection itemColl = new BudgetingDetailCollection();

            int Revision = System.Convert.ToInt32(txtRev.Text);

            if (txtBudgetingNo.Text == string.Empty)
            {
                entity.ServiceUnitID = cboServiceUnitID.SelectedValue;
                entity.Periode = System.Convert.ToInt32(cboYear.SelectedValue);
                entity.CreatedByUserID = AppSession.UserLogin.UserID;
                entity.CreatedDateTime = DateTime.Now;
                entity.IsByItem = false;
            }
            else
            {
                if (!entity.LoadByPrimaryKey(txtBudgetingNo.Text))
                {
                    return AppConstant.Message.RecordNotExist;
                }

                if (isApprovalModule && entity.SRBudgetingVerifyStatus == "03")
                {
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

                if (entity.ServiceUnitID != cboServiceUnitID.SelectedValue)
                {
                    return "Service unit can not be changed";
                }
                if (entity.Periode.ToString() != cboYear.SelectedValue)
                {
                    return "Period can not be changed";
                }

                itemColl.Query.Where(itemColl.Query.BudgetingNo == txtBudgetingNo.Text,
                    itemColl.Query.Revision == Revision);
                itemColl.LoadAll();
            }

            if (!isApprovalModule)
            {
                entity.SRBudgetingVerifyStatus = "01"; //Submission
                entity.Revision = Revision;
                entity.IsApprove = false;
                entity.IsVoid = false;
            }

            entity.Notes = txtNotes.Text;
            entity.SRBudgetingGroup = "01";
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;

            System.Collections.Generic.List<int> coaList = new System.Collections.Generic.List<int>();

            // detail
            foreach (GridDataItem row in grdItemTransactionItem.MasterTableView.Items)
            {
                int coaid = System.Convert.ToInt32(row.GetDataKeyValue("ChartOfAccountID"));
                decimal limit01 = System.Convert.ToDecimal(((RadNumericTextBox)row.FindControl("txtLimit01")).Value);
                decimal limit02 = System.Convert.ToDecimal(((RadNumericTextBox)row.FindControl("txtLimit02")).Value);
                decimal limit03 = System.Convert.ToDecimal(((RadNumericTextBox)row.FindControl("txtLimit03")).Value);
                decimal limit04 = System.Convert.ToDecimal(((RadNumericTextBox)row.FindControl("txtLimit04")).Value);
                decimal limit05 = System.Convert.ToDecimal(((RadNumericTextBox)row.FindControl("txtLimit05")).Value);
                decimal limit06 = System.Convert.ToDecimal(((RadNumericTextBox)row.FindControl("txtLimit06")).Value);
                decimal limit07 = System.Convert.ToDecimal(((RadNumericTextBox)row.FindControl("txtLimit07")).Value);
                decimal limit08 = System.Convert.ToDecimal(((RadNumericTextBox)row.FindControl("txtLimit08")).Value);
                decimal limit09 = System.Convert.ToDecimal(((RadNumericTextBox)row.FindControl("txtLimit09")).Value);
                decimal limit10 = System.Convert.ToDecimal(((RadNumericTextBox)row.FindControl("txtLimit10")).Value);
                decimal limit11 = System.Convert.ToDecimal(((RadNumericTextBox)row.FindControl("txtLimit11")).Value);
                decimal limit12 = System.Convert.ToDecimal(((RadNumericTextBox)row.FindControl("txtLimit12")).Value);

                //if (limit == 0) continue;

                var itemBudget = itemColl.Where(x => x.ChartOfAccountID == coaid).FirstOrDefault();
                if (itemBudget == null)
                {
                    itemBudget = itemColl.AddNew();
                    itemBudget.ChartOfAccountID = coaid;
                    itemBudget.Revision = Revision;
                    itemBudget.Limit01 = limit01;
                    itemBudget.Limit02 = limit02;
                    itemBudget.Limit03 = limit03;
                    itemBudget.Limit04 = limit04;
                    itemBudget.Limit05 = limit05;
                    itemBudget.Limit06 = limit06;
                    itemBudget.Limit07 = limit07;
                    itemBudget.Limit08 = limit08;
                    itemBudget.Limit09 = limit09;
                    itemBudget.Limit10 = limit10;
                    itemBudget.Limit11 = limit11;
                    itemBudget.Limit12 = limit12;
                    itemBudget.CreatedByUserID = AppSession.UserLogin.UserID;
                    itemBudget.CreatedDateTime = DateTime.Now;
                    itemBudget.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    itemBudget.LastUpdateDateTime = DateTime.Now;
                }
                else
                {
                    if ((itemBudget.Limit01 +
                        itemBudget.Limit02 +
                        itemBudget.Limit03 +
                        itemBudget.Limit04 +
                        itemBudget.Limit05 +
                        itemBudget.Limit06 +
                        itemBudget.Limit07 +
                        itemBudget.Limit08 +
                        itemBudget.Limit09 +
                        itemBudget.Limit10 +
                        itemBudget.Limit11 +
                        itemBudget.Limit12) !=
                        (limit01 + limit02 + limit03 + limit04 + limit05 + limit06 +
                        limit07 + limit08 + limit09 + limit10 + limit11 + limit12))
                    {
                        itemBudget.Limit01 = limit01;
                        itemBudget.Limit02 = limit02;
                        itemBudget.Limit03 = limit03;
                        itemBudget.Limit04 = limit04;
                        itemBudget.Limit05 = limit05;
                        itemBudget.Limit06 = limit06;
                        itemBudget.Limit07 = limit07;
                        itemBudget.Limit08 = limit08;
                        itemBudget.Limit09 = limit09;
                        itemBudget.Limit10 = limit10;
                        itemBudget.Limit11 = limit11;
                        itemBudget.Limit12 = limit12;
                        itemBudget.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        itemBudget.LastUpdateDateTime = DateTime.Now;
                    }
                }

                coaList.Add(coaid);
            }
            // remove unused
            var itemBudgetToRemove = itemColl.Where(x => !coaList.Contains(x.ChartOfAccountID ?? 0));
            foreach (var iToRemove in itemBudgetToRemove)
            {
                iToRemove.MarkAsDeleted();
            }

            entity.SumLimit = itemColl.Sum(x => (x.Limit01 + x.Limit02 + x.Limit03 + x.Limit04 + x.Limit05 + x.Limit06 + x.Limit07 + x.Limit08 + x.Limit09 + x.Limit10 + x.Limit11 + x.Limit12));

            using (esTransactionScope trans = new esTransactionScope())
            {
                if (txtBudgetingNo.Text == string.Empty)
                {
                    var autono = GetAutoNumber();
                    if (autono == null)
                    {
                        return "Generating transaction number failed";
                    }
                    txtBudgetingNo.Text = autono.LastCompleteNumber;
                    entity.BudgetingNo = txtBudgetingNo.Text;
                    autono.Save();

                    foreach (var ib in itemColl)
                    {
                        ib.BudgetingNo = entity.BudgetingNo;
                    }
                }

                // detail item
                foreach (var i in BudgetingDetailItems.Where(d => d.es.IsAdded || d.es.IsModified))
                {
                    if (i.es.IsAdded)
                    {
                        i.BudgetingNo = txtBudgetingNo.Text;
                        i.CreatedByUserID = AppSession.UserLogin.UserID;
                        i.CreatedDateTime = DateTime.Now;
                        i.LastUpdateByUserID = i.CreatedByUserID;
                        i.LastUpdateDateTime = i.CreatedDateTime;
                    }
                    if (i.es.IsModified)
                    {
                        i.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        i.LastUpdateDateTime = DateTime.Now;
                    }
                }

                entity.Save();
                itemColl.Save();
                BudgetingDetailItems.Save();

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
        private DataTable BudgetingDetails
        {
            get
            {
                AppStandardReferenceItemCollection coaPrefixColl = new AppStandardReferenceItemCollection();
                coaPrefixColl.Query.Where(coaPrefixColl.Query.StandardReferenceID == "BudgetingCoaCodePrefix");
                coaPrefixColl.LoadAll();

                ChartOfAccountsQuery coaq = new ChartOfAccountsQuery("coaq");

                int ctr = (txtRev.Text == "" ? 1 : System.Convert.ToInt32(txtRev.Text));

                if (this.DataModeCurrent == AppEnum.DataMode.Read)
                {
                    BudgetingDetailQuery bdq = new BudgetingDetailQuery("bdq");
                    coaq.InnerJoin(bdq).On(bdq.ChartOfAccountID == coaq.ChartOfAccountId);

                    coaq.Where(bdq.BudgetingNo == txtBudgetingNo.Text, bdq.Revision == ctr);

                    coaq.Where("<(bdq.Limit01 + bdq.Limit02 + bdq.Limit03 + bdq.Limit04 + bdq.Limit05 + bdq.Limit06 + bdq.Limit07 + bdq.Limit08 + bdq.Limit09 + bdq.Limit10 + bdq.Limit11 + bdq.Limit12) > 0>");

                    coaq.Select
                    (
                        bdq.BudgetingNo,
                        bdq.Revision,
                        bdq.ChartOfAccountID,
                        bdq.Limit01,
                        bdq.Limit02,
                        bdq.Limit03,
                        bdq.Limit04,
                        bdq.Limit05,
                        bdq.Limit06,
                        bdq.Limit07,
                        bdq.Limit08,
                        bdq.Limit09,
                        bdq.Limit10,
                        bdq.Limit11,
                        bdq.Limit12,

                        coaq.ChartOfAccountCode,
                        coaq.ChartOfAccountName,
                        coaq.NormalBalance,
                        "<'read' as Mode>"
                    );
                }
                else
                {
                    if ((ctr == 1) && (txtBudgetingNo.Text == string.Empty))
                    {
                        coaq.Select
                        (
                            "<'' BudgetingNo>",
                            "<1 Revision>",
                            coaq.ChartOfAccountId.As("ChartOfAccountID"),
                            "<cast(0 as decimal) Limit01>",
                            "<cast(0 as decimal) Limit02>",
                            "<cast(0 as decimal) Limit03>",
                            "<cast(0 as decimal) Limit04>",
                            "<cast(0 as decimal) Limit05>",
                            "<cast(0 as decimal) Limit06>",
                            "<cast(0 as decimal) Limit07>",
                            "<cast(0 as decimal) Limit08>",
                            "<cast(0 as decimal) Limit09>",
                            "<cast(0 as decimal) Limit10>",
                            "<cast(0 as decimal) Limit11>",
                            "<cast(0 as decimal) Limit12>",

                            coaq.ChartOfAccountCode,
                            coaq.ChartOfAccountName,
                            coaq.NormalBalance,
                            "<'new' as Mode>"
                        );
                    }
                    else
                    {
                        var bgt = new BusinessObject.Budgeting();
                        bgt.LoadByPrimaryKey(txtBudgetingNo.Text);

                        BudgetingDetailQuery bdq = new BudgetingDetailQuery("bdq");
                        coaq.InnerJoin(bdq).On(bdq.ChartOfAccountID == coaq.ChartOfAccountId);

                        coaq.Where(bdq.BudgetingNo == txtBudgetingNo.Text);
                        if (bgt.SRBudgetingVerifyStatus == "01")
                        {
                            coaq.Where(bdq.Revision == ctr);
                        }
                        else
                        {
                            coaq.Where(bdq.Revision == (ctr - 1));
                        }

                        coaq.Select
                        (
                            bdq.BudgetingNo,
                            bdq.Revision,
                            bdq.ChartOfAccountID,
                            bdq.Limit01,
                            bdq.Limit02,
                            bdq.Limit03,
                            bdq.Limit04,
                            bdq.Limit05,
                            bdq.Limit06,
                            bdq.Limit07,
                            bdq.Limit08,
                            bdq.Limit09,
                            bdq.Limit10,
                            bdq.Limit11,
                            bdq.Limit12,

                            coaq.ChartOfAccountCode,
                            coaq.ChartOfAccountName,
                            coaq.NormalBalance,
                            "<'edit' as Mode>"
                        );
                    }
                }

                coaq.Where(coaq.IsDetail == true);

                string whereor = string.Empty;
                foreach (var coapref in coaPrefixColl)
                {
                    whereor += (whereor == string.Empty ? "" : " OR ")
                        + string.Format("coaq.ChartOfAccountCode like '{0}%'", coapref.ItemName);
                }
                if (whereor != string.Empty)
                {
                    coaq.Where("<" + whereor + ">");
                }

                coaq.OrderBy(coaq.ChartOfAccountCode.Ascending);

                DataTable dt = coaq.LoadDataTable();

                return dt;
            }
        }

        protected void grdItemTransactionItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var dtb = BudgetingDetails;
            dtb.Columns.Add("ItemCount", typeof(int));

            var bdiColl = BudgetingDetailItems;

            var dItems = bdiColl.GroupBy(b => b.ChartOfAccountID).Select(g => new { ChartOfAccountID = g.Key, Count = g.Count() });

            foreach (var dItem in dItems)
            {
                var row = dtb.AsEnumerable().Where(r => (int)r["ChartOfAccountID"] == dItem.ChartOfAccountID).FirstOrDefault();
                if (row != null)
                {
                    row["ItemCount"] = dItem.Count;
                }
            }

            grdItemTransactionItem.DataSource = dtb;

            // summary
            lblCountDebit.Text = dtb.AsEnumerable().Where(f => f["NormalBalance"].ToString() == "D").Count().ToString();
            lblSumBudgetAmountDebit.Text = dtb.AsEnumerable().Where(f => f["NormalBalance"].ToString() == "D").Sum(x =>
                (x.Field<decimal>("Limit01") +
                x.Field<decimal>("Limit02") +
                x.Field<decimal>("Limit03") +
                x.Field<decimal>("Limit04") +
                x.Field<decimal>("Limit05") +
                x.Field<decimal>("Limit06") +
                x.Field<decimal>("Limit07") +
                x.Field<decimal>("Limit08") +
                x.Field<decimal>("Limit09") +
                x.Field<decimal>("Limit10") +
                x.Field<decimal>("Limit11") +
                x.Field<decimal>("Limit12")
            )).ToString("n2");

            lblCountCredit.Text = dtb.AsEnumerable().Where(f => f["NormalBalance"].ToString() == "K").Count().ToString();
            lblSumBudgetAmountCredit.Text = dtb.AsEnumerable().Where(f => f["NormalBalance"].ToString() == "K").Sum(x =>
                (x.Field<decimal>("Limit01") +
                x.Field<decimal>("Limit02") +
                x.Field<decimal>("Limit03") +
                x.Field<decimal>("Limit04") +
                x.Field<decimal>("Limit05") +
                x.Field<decimal>("Limit06") +
                x.Field<decimal>("Limit07") +
                x.Field<decimal>("Limit08") +
                x.Field<decimal>("Limit09") +
                x.Field<decimal>("Limit10") +
                x.Field<decimal>("Limit11") +
                x.Field<decimal>("Limit12")
            )).ToString("n2");
        }

        protected void grdItemTransactionItem_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                if (DataModeCurrent == AppEnum.DataMode.Read)
                {
                    HideNumShowLabel((RadNumericTextBox)dataItem.FindControl("txtLimit01"));
                    HideNumShowLabel((RadNumericTextBox)dataItem.FindControl("txtLimit02"));
                    HideNumShowLabel((RadNumericTextBox)dataItem.FindControl("txtLimit03"));
                    HideNumShowLabel((RadNumericTextBox)dataItem.FindControl("txtLimit04"));
                    HideNumShowLabel((RadNumericTextBox)dataItem.FindControl("txtLimit05"));
                    HideNumShowLabel((RadNumericTextBox)dataItem.FindControl("txtLimit06"));
                    HideNumShowLabel((RadNumericTextBox)dataItem.FindControl("txtLimit07"));
                    HideNumShowLabel((RadNumericTextBox)dataItem.FindControl("txtLimit08"));
                    HideNumShowLabel((RadNumericTextBox)dataItem.FindControl("txtLimit09"));
                    HideNumShowLabel((RadNumericTextBox)dataItem.FindControl("txtLimit10"));
                    HideNumShowLabel((RadNumericTextBox)dataItem.FindControl("txtLimit11"));
                    HideNumShowLabel((RadNumericTextBox)dataItem.FindControl("txtLimit12"));
                }
            }
        }

        private void HideNumShowLabel(RadNumericTextBox txt)
        {
            txt.Visible = false;
            // add label
            var lbl = new System.Web.UI.WebControls.Label();
            //lbl.ID = "lblLimit01";
            lbl.Text = txt.Value.Value.ToString("n0"); //(((DataRowView)dataItem.DataItem).Row.Field<decimal>("Limit01")).ToString("n0");
            txt.Parent.Controls.Add(lbl);
        }

        //protected void grdItemTransactionItem_ItemCommand(object source, GridCommandEventArgs e)
        //{
        //    //if (e.CommandName == "copy")
        //    //{
        //    //    if(CopyToVerified((GridDataItem)e.Item)){
        //    //        grdItemTransactionItem.Rebind();
        //    //    }
        //    //}
        //    //if (e.CommandName == "copyAll")
        //    //{
        //    //    foreach (GridDataItem item in grdItemTransactionItem.MasterTableView.Items) {
        //    //        if (CopyToVerified(item))
        //    //        {

        //    //        }
        //    //    }

        //    //    grdItemTransactionItem.Rebind();
        //    //}

        //    //if (e.CommandName == "remove")
        //    //{
        //    //    if (RemoveVerified((GridDataItem)e.Item))
        //    //    {
        //    //        grdItemTransactionItem.Rebind();
        //    //    }
        //    //}
        //    //if (e.CommandName == "removeAll")
        //    //{
        //    //    foreach (GridDataItem item in grdItemTransactionItem.MasterTableView.Items)
        //    //    {
        //    //        if (RemoveVerified(item))
        //    //        {

        //    //        }
        //    //    }

        //    //    grdItemTransactionItem.Rebind();
        //    //}
        //}

        //private bool CopyToVerified(GridDataItem gdi) {
        //    var CoaID = (Int32)gdi.GetDataKeyValue("ChartOfAccountID");
        //    var bgtD = new BusinessObject.BudgetingDetail();
        //    bgtD.Query.Where(bgtD.Query.BudgetingNo == txtBudgetingNo.Text, bgtD.Query.ChartOfAccountID == CoaID);
        //    if (bgtD.Load(bgtD.Query))
        //    {
        //        bgtD.CopyToVerified(AppSession.UserLogin.UserID);
        //        bgtD.Save();
        //        return true;
        //    }
        //    return false;
        //}
        //private bool RemoveVerified(GridDataItem gdi)
        //{
        //    var CoaID = (Int32)gdi.GetDataKeyValue("ChartOfAccountID");
        //    var bgtD = new BusinessObject.BudgetingDetail();
        //    bgtD.Query.Where(bgtD.Query.BudgetingNo == txtBudgetingNo.Text, bgtD.Query.ChartOfAccountID == CoaID);
        //    if (bgtD.Load(bgtD.Query))
        //    {
        //        bgtD.RemoveVerified(AppSession.UserLogin.UserID);
        //        bgtD.Save();
        //        return true;
        //    }
        //    return false;
        //}

        #endregion

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //PopulateNewTransactionNo();
            //ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboServiceUnitID.SelectedValue, BusinessObject.Reference.TransactionCode.BudgetPlan);
            //if (cboSRItemType.Items.Count == 2) cboSRItemType.SelectedIndex = 1;
        }

        protected void cboPeriodYear_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            switch (cboCopyFrom.SelectedValue)
            {
                case "1":
                    {
                        var bg = new BudgetingQuery();
                        bg.Where(
                            bg.Periode.Cast(Dal.DynamicQuery.esCastType.String).Like(searchText),
                            bg.IsVoid == false,
                            bg.ServiceUnitID == cboServiceUnitID.SelectedValue)
                            .OrderBy(bg.Periode.Descending)
                            .Select(bg.Periode.Cast(Dal.DynamicQuery.esCastType.String));
                        var dtb = bg.LoadDataTable();

                        cboPeriodYear.DataSource = dtb;
                        cboPeriodYear.DataBind();
                        break;
                    }
                case "2":
                    {
                        //var b = new SubLedgerBalancesQuery("bg");
                        //var sl = new SubLedgersQuery("sl");
                        //b.InnerJoin(sl).On(b.SubLedgerId == sl.SubLedgerId)
                        //    .Where(b.Year.Cast(Dal.DynamicQuery.esCastType.String).Like(searchText),
                        //        sl.SubLedgerName == cboServiceUnitID.SelectedValue)
                        //    .OrderBy(b.Year.Descending)
                        //    .Select(b.Year.Cast(Dal.DynamicQuery.esCastType.String));
                        var ps = new PostingStatusQuery();
                        ps.Select(ps.Year.Cast(Dal.DynamicQuery.esCastType.String).As("Periode"), ps.Year)
                            .OrderBy(ps.Year.Descending);
                        ps.es.Distinct = true;
                        var dtb = ps.LoadDataTable();

                        cboPeriodYear.DataSource = dtb;
                        cboPeriodYear.DataBind();
                        break;
                    }
            }

        }

        protected void cboPeriodYear_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["Periode"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["Periode"].ToString();
            //e.Item.Text = ((Temiang.Avicenna.BusinessObject.Budgeting)e.Item.DataItem).Periode.ToString();
            //e.Item.Value = ((Temiang.Avicenna.BusinessObject.Budgeting)e.Item.DataItem).Periode.ToString();
        }

        protected void btnCopy_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (this.DataModeCurrent == AppEnum.DataMode.Read) return;
            if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue)) return;
            if (string.IsNullOrEmpty(cboYear.SelectedValue)) return;
            if (string.IsNullOrEmpty(cboPeriodYear.SelectedValue)) return;

            switch (cboCopyFrom.SelectedValue)
            {
                case "1":
                    {
                        var bColl = new BudgetingCollection();
                        bColl.Query.Where(bColl.Query.ServiceUnitID == cboServiceUnitID.SelectedValue,
                            bColl.Query.IsVoid == false, bColl.Query.Periode.Cast(Dal.DynamicQuery.esCastType.String) == cboPeriodYear.SelectedValue);
                        if (bColl.LoadAll())
                        {
                            var biColl = new BudgetingDetailCollection();
                            var biq = new BudgetingDetailQuery("bi");
                            var coaq = new ChartOfAccountsQuery("coa");
                            biq.InnerJoin(coaq).On(biq.ChartOfAccountID == coaq.ChartOfAccountId)
                                .Where(biq.BudgetingNo == bColl.First().BudgetingNo, coaq.ChartOfAccountCode.Like(string.Format("{0}%", txtCoaPrefix.Text)));

                            if (biColl.Load(biq))
                            {
                                foreach (GridDataItem row in grdItemTransactionItem.MasterTableView.Items)
                                {
                                    int coaid = System.Convert.ToInt32(row.GetDataKeyValue("ChartOfAccountID"));
                                    var bi = biColl.Where(x => x.ChartOfAccountID == coaid).FirstOrDefault();
                                    if (bi == null) continue;

                                    if (cboPeriodMonth.SelectedValue == "" || cboPeriodMonth.SelectedValue == "01") ((RadNumericTextBox)row.FindControl("txtLimit01")).Value = Convert.ToDouble(bi.Limit01 ?? 0) * (txtMultiplyBy.Value ?? 1);
                                    if (cboPeriodMonth.SelectedValue == "" || cboPeriodMonth.SelectedValue == "02") ((RadNumericTextBox)row.FindControl("txtLimit02")).Value = Convert.ToDouble(bi.Limit02 ?? 0) * (txtMultiplyBy.Value ?? 1);
                                    if (cboPeriodMonth.SelectedValue == "" || cboPeriodMonth.SelectedValue == "03") ((RadNumericTextBox)row.FindControl("txtLimit03")).Value = Convert.ToDouble(bi.Limit03 ?? 0) * (txtMultiplyBy.Value ?? 1);
                                    if (cboPeriodMonth.SelectedValue == "" || cboPeriodMonth.SelectedValue == "04") ((RadNumericTextBox)row.FindControl("txtLimit04")).Value = Convert.ToDouble(bi.Limit04 ?? 0) * (txtMultiplyBy.Value ?? 1);
                                    if (cboPeriodMonth.SelectedValue == "" || cboPeriodMonth.SelectedValue == "05") ((RadNumericTextBox)row.FindControl("txtLimit05")).Value = Convert.ToDouble(bi.Limit05 ?? 0) * (txtMultiplyBy.Value ?? 1);
                                    if (cboPeriodMonth.SelectedValue == "" || cboPeriodMonth.SelectedValue == "06") ((RadNumericTextBox)row.FindControl("txtLimit06")).Value = Convert.ToDouble(bi.Limit06 ?? 0) * (txtMultiplyBy.Value ?? 1);
                                    if (cboPeriodMonth.SelectedValue == "" || cboPeriodMonth.SelectedValue == "07") ((RadNumericTextBox)row.FindControl("txtLimit07")).Value = Convert.ToDouble(bi.Limit07 ?? 0) * (txtMultiplyBy.Value ?? 1);
                                    if (cboPeriodMonth.SelectedValue == "" || cboPeriodMonth.SelectedValue == "08") ((RadNumericTextBox)row.FindControl("txtLimit08")).Value = Convert.ToDouble(bi.Limit08 ?? 0) * (txtMultiplyBy.Value ?? 1);
                                    if (cboPeriodMonth.SelectedValue == "" || cboPeriodMonth.SelectedValue == "09") ((RadNumericTextBox)row.FindControl("txtLimit09")).Value = Convert.ToDouble(bi.Limit09 ?? 0) * (txtMultiplyBy.Value ?? 1);
                                    if (cboPeriodMonth.SelectedValue == "" || cboPeriodMonth.SelectedValue == "10") ((RadNumericTextBox)row.FindControl("txtLimit10")).Value = Convert.ToDouble(bi.Limit10 ?? 0) * (txtMultiplyBy.Value ?? 1);
                                    if (cboPeriodMonth.SelectedValue == "" || cboPeriodMonth.SelectedValue == "11") ((RadNumericTextBox)row.FindControl("txtLimit11")).Value = Convert.ToDouble(bi.Limit11 ?? 0) * (txtMultiplyBy.Value ?? 1);
                                    if (cboPeriodMonth.SelectedValue == "" || cboPeriodMonth.SelectedValue == "12") ((RadNumericTextBox)row.FindControl("txtLimit12")).Value = Convert.ToDouble(bi.Limit12 ?? 0) * (txtMultiplyBy.Value ?? 1);
                                }
                            }
                        }
                        break;
                    }
                case "2":
                    {
                        var sbColl = new SubLedgerBalancesCollection();
                        var sb = new SubLedgerBalancesQuery("sb");
                        var sl = new SubLedgersQuery("sl");
                        var coaq = new ChartOfAccountsQuery("coa");
                        sb.InnerJoin(sl).On(sb.SubLedgerId == sl.SubLedgerId)
                            .InnerJoin(coaq).On(sb.ChartOfAccountId == coaq.ChartOfAccountId)
                            .Where(
                                sb.Year == cboPeriodYear.SelectedValue,
                                sl.SubLedgerName == cboServiceUnitID.SelectedValue,
                                coaq.ChartOfAccountCode.Like(string.Format("{0}%", txtCoaPrefix.Text)));
                        if (sbColl.Load(sb))
                        {
                            foreach (GridDataItem row in grdItemTransactionItem.MasterTableView.Items)
                            {
                                int coaid = System.Convert.ToInt32(row.GetDataKeyValue("ChartOfAccountID"));
                                var bi = sbColl.Where(x => x.ChartOfAccountId == coaid).FirstOrDefault();
                                if (bi == null) continue;

                                for (var i = 1; i <= 12; i++)
                                {
                                    var strPad = "0" + i.ToString();
                                    var month = strPad.Substring(strPad.Length - 2, 2);

                                    var sbMonth = sbColl.Where(x => x.ChartOfAccountId == coaid && x.Month == month).FirstOrDefault();
                                    if (sbMonth == null) continue;
                                    if ((!string.IsNullOrEmpty(cboPeriodMonth.SelectedValue)) && cboPeriodMonth.SelectedValue != month) continue;

                                    ((RadNumericTextBox)row.FindControl("txtLimit" + month)).Value =
                                        Convert.ToDouble((sbMonth.FinalBalance ?? 0) - (sbMonth.InitialBalance ?? 0)) * (txtMultiplyBy.Value ?? 1);
                                }
                            }
                        }
                        break;
                    }
            }
        }

        protected void btnReset_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (this.DataModeCurrent == AppEnum.DataMode.Read) return;
            if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue)) return;
            if (string.IsNullOrEmpty(cboYear.SelectedValue)) return;

            foreach (GridDataItem row in grdItemTransactionItem.MasterTableView.Items)
            {

                for (var i = 1; i <= 12; i++)
                {
                    var strPad = "0" + i.ToString();
                    var month = strPad.Substring(strPad.Length - 2, 2);

                    ((RadNumericTextBox)row.FindControl("txtLimit" + month)).Value = 0;
                }
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtb = new DataTable();
                dtb.Columns.Add("ChartOfAccountCode", typeof(string));
                dtb.Columns.Add("ChartOfAccountName", typeof(string));
                dtb.Columns.Add("Jan", typeof(decimal));
                dtb.Columns.Add("Feb", typeof(decimal));
                dtb.Columns.Add("Mar", typeof(decimal));
                dtb.Columns.Add("Apr", typeof(decimal));
                dtb.Columns.Add("May", typeof(decimal));
                dtb.Columns.Add("Jun", typeof(decimal));
                dtb.Columns.Add("Jul", typeof(decimal));
                dtb.Columns.Add("Aug", typeof(decimal));
                dtb.Columns.Add("Sep", typeof(decimal));
                dtb.Columns.Add("Oct", typeof(decimal));
                dtb.Columns.Add("Nov", typeof(decimal));
                dtb.Columns.Add("Dec", typeof(decimal));

                foreach (GridDataItem item in grdItemTransactionItem.MasterTableView.Items)
                {
                    var dr = dtb.NewRow();
                    dr["ChartOfAccountCode"] = item.Cells[2].Text.Trim();
                    dr["ChartOfAccountName"] = item.Cells[3].Text.Trim();
                    dr["Jan"] = (item.FindControl("txtLimit01") as RadNumericTextBox).Value;
                    dr["Feb"] = (item.FindControl("txtLimit02") as RadNumericTextBox).Value;
                    dr["Mar"] = (item.FindControl("txtLimit03") as RadNumericTextBox).Value;
                    dr["Apr"] = (item.FindControl("txtLimit04") as RadNumericTextBox).Value;
                    dr["May"] = (item.FindControl("txtLimit05") as RadNumericTextBox).Value;
                    dr["Jun"] = (item.FindControl("txtLimit06") as RadNumericTextBox).Value;
                    dr["Jul"] = (item.FindControl("txtLimit07") as RadNumericTextBox).Value;
                    dr["Aug"] = (item.FindControl("txtLimit08") as RadNumericTextBox).Value;
                    dr["Sep"] = (item.FindControl("txtLimit09") as RadNumericTextBox).Value;
                    dr["Oct"] = (item.FindControl("txtLimit10") as RadNumericTextBox).Value;
                    dr["Nov"] = (item.FindControl("txtLimit11") as RadNumericTextBox).Value;
                    dr["Dec"] = (item.FindControl("txtLimit12") as RadNumericTextBox).Value;
                    dtb.Rows.Add(dr);
                }
                if (dtb.Rows.Count > 0)
                {
                    //Common.CreateExcelFile.CreateExcelDocument(dtb, string.Format("budget{0}_{1}.xls", cboYear.SelectedValue, ""), this.Response);
                    //Common.CreateExcelFile.CreateExcelDocument(dtb, "budget.xls", this.Response);

                    Session["ExportBudget"] = dtb;
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Export", "ExportToExcel();", true);

                    string script = string.Format("function f(){{openDialog('{0}', {1}, {2}, {3});Sys.Application.remove_load(f);}}Sys.Application.add_load(f);",
                                     "ExportToExcelDialog.aspx",
                                     "true",
                                     400,
                                     400);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Export", script, true);

                }

            }
            catch (Exception ex)
            {
                ShowInformationHeader(ex.Message);
            }
        }

        protected void btnUpload_Click(object sender, ImageClickEventArgs e)
        {
            
        }

        private void UploadFile() {
            var fileName = Session["budgetImport"].ToString();
            if (string.IsNullOrEmpty(fileName)) return;

            try
            {
                var table = Common.ExcelUtil.LoadFirstSheetToDataTable(fileName);

                if (table != null && table.Rows.Count > 0)
                {
                    var coaColl = new ChartOfAccountsCollection();
                    coaColl.LoadAll();

                    foreach (System.Data.DataRow dr in table.Rows)
                    {
                        var coa = coaColl.Where(c => c.ChartOfAccountCode.Trim() == dr["ChartOfAccountCode"].ToString().Trim()).FirstOrDefault();
                        if (coa != null) {
                            var item = grdItemTransactionItem.MasterTableView.FindItemByKeyValue("ChartOfAccountID", coa.ChartOfAccountId);
                            if (item != null)
                            {
                                item.Cells[2].Text = coa.ChartOfAccountCode;
                                item.Cells[3].Text = coa.ChartOfAccountName;//dr["ChartOfAccountName"].ToString();
                                (item.FindControl("txtLimit01") as RadNumericTextBox).Value = System.Convert.ToDouble(dr["Jan"]);
                                (item.FindControl("txtLimit02") as RadNumericTextBox).Value = System.Convert.ToDouble(dr["Feb"]);
                                (item.FindControl("txtLimit03") as RadNumericTextBox).Value = System.Convert.ToDouble(dr["Mar"]);
                                (item.FindControl("txtLimit04") as RadNumericTextBox).Value = System.Convert.ToDouble(dr["Apr"]);
                                (item.FindControl("txtLimit05") as RadNumericTextBox).Value = System.Convert.ToDouble(dr["May"]);
                                (item.FindControl("txtLimit06") as RadNumericTextBox).Value = System.Convert.ToDouble(dr["Jun"]);
                                (item.FindControl("txtLimit07") as RadNumericTextBox).Value = System.Convert.ToDouble(dr["Jul"]);
                                (item.FindControl("txtLimit08") as RadNumericTextBox).Value = System.Convert.ToDouble(dr["Aug"]);
                                (item.FindControl("txtLimit09") as RadNumericTextBox).Value = System.Convert.ToDouble(dr["Sep"]);
                                (item.FindControl("txtLimit10") as RadNumericTextBox).Value = System.Convert.ToDouble(dr["Oct"]);
                                (item.FindControl("txtLimit11") as RadNumericTextBox).Value = System.Convert.ToDouble(dr["Nov"]);
                                (item.FindControl("txtLimit12") as RadNumericTextBox).Value = System.Convert.ToDouble(dr["Dec"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowInformationHeader(ex.Message);
            }
            finally {
                File.Delete(fileName);
                Session["budgetImport"] = null;
            }
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler source, string argument)
        {
            base.RaisePostBackEvent(source, argument);

            if (string.IsNullOrEmpty(argument) || !(source is RadGrid))
                return;

            if (argument.Contains("uploaded")) 
            {
                UploadFile();
            }
        }
    }
}