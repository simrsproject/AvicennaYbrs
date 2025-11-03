using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;
using Telerik.Windows.Documents.Spreadsheet.Model.DataValidation;
using Telerik.Reporting;
using System.Collections.Generic;
using NCalc;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee
{
    public partial class RemunDetail : BasePageDetail
    {

        private ServiceFeeRsucdrCollection sfrDetail
        {
            get {
                if (!IsPostBack) {
                    var sfrColl = new ServiceFeeRsucdrCollection();

                    var sfrs = new ServiceFeeRemunRsucdr();
                    if (sfrs.LoadByRemunNo(txtRemunNo.Text)) {
                        sfrColl.LoadByRemunID(sfrs.RemunID ?? 0);
                    }

                    Session["sfrDetail"] = sfrColl;
                }

                if(Session["sfrDetail"] == null) Session["sfrDetail"] = new ServiceFeeRsucdrCollection();

                return (ServiceFeeRsucdrCollection)Session["sfrDetail"];
            }
            set
            {
                Session["sfrDetail"] = value;
            }
        }

        private ServiceFeeRemunRsucdrInvoicesCollection sfrInvoices
        {
            get
            {
                if (!IsPostBack)
                {
                    var sfriColl = new ServiceFeeRemunRsucdrInvoicesCollection();

                    var sfr = new ServiceFeeRemunRsucdr();
                    if (!sfr.LoadByRemunNo(txtRemunNo.Text)) {
                        sfr.AddNew();
                        sfr.RemunID = 0;
                    }

                    var sfi = new ServiceFeeRemunRsucdrInvoicesQuery("sfi");
                        var iv = new InvoicesQuery("iv");
                        var ivi = new InvoicesItemQuery("ivi");
                        sfi.InnerJoin(ivi).On(sfi.InvoiceNo == ivi.InvoiceNo)
                            .Where(sfi.RemunID == sfr.RemunID)
                            .GroupBy(
                                sfi.RemunID, sfi.InvoiceNo, 
                                sfi.CreateByUserID, sfi.CreateDateTime, 
                                sfi.LastUpdateByUserID, sfi.LastUpdateDateTime
                            ).Select(
                                sfi.RemunID, sfi.InvoiceNo, 
                                sfi.CreateByUserID, sfi.CreateDateTime, 
                                sfi.LastUpdateByUserID, sfi.LastUpdateDateTime, 
                                ivi.Amount.Sum().As("refToIVI_Amount")
                            );
                    sfriColl.Load(sfi);
                    

                    Session["sfrInvoices"] = sfriColl;
                }

                if (Session["sfrInvoices"] == null) Session["sfrInvoices"] = new ServiceFeeRemunRsucdrInvoicesCollection();

                return (ServiceFeeRemunRsucdrInvoicesCollection)Session["sfrInvoices"];
            }
            set
            {
                Session["sfrInvoices"] = value;
            }
        }

        private ServiceFeeRemunRsucdrDeductionsCollection sfrDeductions
        {
            get
            {
                if (!IsPostBack)
                {
                    var sfrdColl = new ServiceFeeRemunRsucdrDeductionsCollection();

                    if (!string.IsNullOrEmpty(txtRemunNo.Text))
                    {
                        var sfr = new ServiceFeeRemunRsucdr();
                        if (!sfr.LoadByRemunNo(txtRemunNo.Text))
                            return sfrdColl;

                        sfrdColl.Query.Where(sfrdColl.Query.RemunID == sfr.RemunID);
                        sfrdColl.LoadAll();
                    }

                    var rDeducColl = new AppStandardReferenceItemCollection();
                    rDeducColl.Query.Where(rDeducColl.Query.StandardReferenceID == "RemunDeduction", rDeducColl.Query.IsActive == true);
                    rDeducColl.LoadAll();
                    foreach (var rDeduc in rDeducColl) {
                        var std = sfrdColl.Where(s => s.SRRemunDeduction == rDeduc.ItemID).FirstOrDefault();
                        if (std == null)
                        {
                            // blm ada
                            std = sfrdColl.AddNew();
                            std.SRRemunDeduction = rDeduc.ItemID;
                            std.Amount = 0;
                        }
                        std.DeductionName = rDeduc.ItemName;
                        std.Formula = rDeduc.CustomField;
                    }

                    Session["sfrDeductions"] = sfrdColl;
                }

                if (Session["sfrDeductions"] == null) Session["sfrDeductions"] = new ServiceFeeRemunRsucdrDeductionsCollection();

                return (ServiceFeeRemunRsucdrDeductionsCollection)Session["sfrDeductions"];
            }
            set
            {
                Session["sfrDeductions"] = value;
            }
        }

        private AppAutoNumberLast GetAutoNumber()
        {
            if (string.IsNullOrEmpty(cboMonth.SelectedValue)) return null;
            if (string.IsNullOrEmpty(txtYear.Text)) return null;

            var iMonth = System.Convert.ToInt32(cboMonth.SelectedValue);
            var iYear = System.Convert.ToInt32(txtYear.Text);

            var dDate = new DateTime(iYear, iMonth, 1);

            var transCode = new AppAutoNumberTransactionCode();
            if (!transCode.LoadByPrimaryKey(BusinessObject.Reference.TransactionCode.RemunerationByIdi))
                return null;

            return Common.Helper.GetNewAutoNumber(dDate, transCode.SRAutoNumber);
        }
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            //UrlPageSearch = "RemunSearch.aspx";
            UrlPageList = string.Format("RemunList.aspx");

            IsUsingBeforeVoidValidation = true;

            ProgramID = AppConstant.Program.ParamedicFeeRemunerationByIDI;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                ParamedicFeeRemunList.PopulateMonth(cboMonth);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            //ajax.AddAjaxSetting(cboFromServiceUnitID, txtTransactionNo);
            //ajax.AddAjaxSetting(cboFromServiceUnitID, cboSRItemType);

            //ajax.AddAjaxSetting(grdItemTransactionItem, grdItemTransactionItem);
            //ajax.AddAjaxSetting(grdItemTransactionItem, cboFromServiceUnitID);
            //ajax.AddAjaxSetting(grdItemTransactionItem, cboSRItemType);
        }

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
            var rem = new BusinessObject.ServiceFeeRemunRsucdr();
            if (!rem.LoadByRemunNo(txtRemunNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (rem.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            if (rem.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return;
            }

            rem.IsApproved = true;
            rem.ApprovedDateTime = DateTime.Now;
            rem.ApprovedByUserID = AppSession.UserLogin.UserID;
            rem.LastUpdateDateTime = DateTime.Now;
            rem.LastUpdateByUserID = AppSession.UserLogin.UserID;
            using (var trans = new esTransactionScope())
            {
                rem.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            // cek budgetnya sudah approve atau belum
            var rem = new BusinessObject.ServiceFeeRemunRsucdr();
            if (!rem.LoadByRemunNo(txtRemunNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (!(rem.IsApproved ?? false))
            {
                args.MessageText = AppConstant.Message.RecordHasNotApproved;
                args.IsCancel = true;
                return;
            }
            if (rem.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }


            rem.IsApproved = false;
            rem.LastUpdateDateTime = DateTime.Now;
            rem.LastUpdateByUserID = AppSession.UserLogin.UserID;

            using (var trans = new esTransactionScope())
            {
                rem.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            // cek budgetnya sudah approve atau belum
            var rem = new BusinessObject.ServiceFeeRemunRsucdr();
            if (!rem.LoadByRemunNo(txtRemunNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            if (rem.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            rem.IsVoid = true;
            rem.VoidDateTime = DateTime.Now;
            rem.VoidByUserID = AppSession.UserLogin.UserID;
            rem.LastUpdateByUserID = AppSession.UserLogin.UserID;
            rem.LastUpdateDateTime = DateTime.Now;

            var fees = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            fees.Query.Where(fees.Query.RemunByIdiID == rem.RemunID);
            fees.LoadAll();
            foreach (var fee in fees)
            {
                // reset
                fee.RemunByIdiID = null;
            }

            using (var trans = new esTransactionScope())
            {
                fees.Save();

                rem.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            // gak bisa lagi unvoid karena link ke tabel jasmed sudah dikosongkan -->  fee.RemunByIdiID = null;
            args.MessageText = "Unvoid is disabled";
            args.IsCancel = true;
            return;

            // cek budgetnya sudah approve atau belum
            var rem = new BusinessObject.ServiceFeeRemunRsucdr();
            if (!rem.LoadByRemunNo(txtRemunNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (!(rem.IsVoid ?? false))
            {
                args.MessageText = "This data has not been voided";
                args.IsCancel = true;
                return;
            }

            var dtime = DateTime.Now;

            rem.IsVoid = false;
            rem.LastUpdateDateTime = dtime;
            rem.LastUpdateByUserID = AppSession.UserLogin.UserID;

            using (var trans = new esTransactionScope())
            {
                rem.Save();

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
            OnPopulateEntryControl(new ServiceFeeRemunRsucdr());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            ServiceFeeRemunRsucdr entity = new ServiceFeeRemunRsucdr();
            if (entity.LoadByRemunNo(txtRemunNo.Text))
            {
                if (entity.IsApproved ?? false)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                }
                else
                {
                    var invs = new ServiceFeeRemunRsucdrInvoicesCollection();
                    invs.Query.Where(invs.Query.RemunID == entity.RemunID);
                    invs.LoadAll();
                    invs.MarkAllAsDeleted();
                    
                    var decs = new ServiceFeeRemunRsucdrDeductionsCollection();
                    decs.Query.Where(decs.Query.RemunID == entity.RemunID);
                    decs.LoadAll();
                    decs.MarkAllAsDeleted();

                    var sfees = new ServiceFeeRsucdrCollection();
                    sfees.Query.Where(sfees.Query.RemunID == entity.RemunID);
                    sfees.LoadAll();
                    sfees.MarkAllAsDeleted();

                    entity.MarkAsDeleted();
                    using (esTransactionScope trans = new esTransactionScope())
                    {
                        sfees.Save();
                        invs.Save();
                        decs.Save();
                        entity.Save();
                        
                        trans.Complete();
                    }

                    sfrDeductions = null;
                    sfrInvoices = null;
                    sfrDetail = null;

                    grdDeductions.Rebind();
                    grdInvoices.Rebind();
                    grdSummary.Rebind();
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            // validate save
            if (!ValidateHeader(args))
            {
                return;
            }
            //if (feeRemunDetail.Rows.Count == 0)
            //{
            //    args.MessageText = "Detail can not be empty";
            //    args.IsCancel = true;
            //    return;
            //}
            //if (feeRemunDetailTransaction.Rows.Count == 0)
            //{
            //    args.MessageText = "Detail transsaction can not be empty";
            //    args.IsCancel = true;
            //    return;
            //}

            string ret = SaveEntity();
            if (ret != string.Empty)
            {
                args.MessageText = ret;
                args.IsCancel = true;
                return;
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            // validate save
            if (!ValidateHeader(args)) {
                return;
            }

            string ret = SaveEntity();
            if (ret != string.Empty) {
                args.MessageText = ret;
                args.IsCancel = true;
                return;
            }
        }

        private bool ValidateHeader(ValidateArgs args) {
            if (string.IsNullOrEmpty(cboMonth.SelectedValue))
            {
                args.MessageText = "Invalid month";
                args.IsCancel = true;
                return false;
            }
            if (!Helper.IsNumeric(cboMonth.SelectedValue))
            {
                args.MessageText = "Invalid month";
                args.IsCancel = true;
                return false;
            }
            if (string.IsNullOrEmpty(txtYear.Text))
            {
                args.MessageText = "Invalid year";
                args.IsCancel = true;
                return false;
            }
            if (!Helper.IsNumeric(txtYear.Text))
            {
                args.MessageText = "Invalid year";
                args.IsCancel = true;
                return false;
            }

            if (sfrInvoices.Count < 1) {
                args.MessageText = "Invoice can not be empty";
                args.IsCancel = true;
                return false;
            }

            return true;
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
            auditLogFilter.PrimaryKeyData = string.Format("RemunNo='{0}'", txtRemunNo.Text.Trim());
            auditLogFilter.TableName = "ServiceFeeRemunRsucdr";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_RemunNo", txtRemunNo.Text);
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuEdit()
        {
            return txtRemunNo.Text != string.Empty;
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
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdInvoices.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            grdInvoices.Rebind();

            grdDeductions.Columns[0].Visible = (newVal != AppEnum.DataMode.Read);
            grdDeductions.Rebind();

            btnCalculateBudget.Enabled = isVisible;
            btnExportExcel.Enabled = newVal == AppEnum.DataMode.Read;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            BusinessObject.ServiceFeeRemunRsucdr entity = new BusinessObject.ServiceFeeRemunRsucdr();
            if (parameters.Length > 0)
            {
                String RemunNo = (String)parameters[0];

                entity.LoadByRemunNo(RemunNo);
            }
            else
            {
                if (!txtRemunNo.Text.Equals(string.Empty))
                {
                    entity.LoadByRemunNo(txtRemunNo.Text);
                }
                else
                {
                    entity.LoadByRemunNo(txtRemunNo.Text);
                }
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            BusinessObject.ServiceFeeRemunRsucdr rem = (BusinessObject.ServiceFeeRemunRsucdr)entity;

            txtRemunNo.Text = rem.RemunNo;

            cboMonth.SelectedValue = rem.PeriodMonth.ToString();
            txtYear.Text = rem.PeriodYear.ToString();

            chkIsVoid.Checked = rem.IsVoid ?? false;
            chkIsApproved.Checked = rem.IsApproved ?? false;
            txtNotes.Text = rem.Notes;

            txtInvoiceAmount.Value = System.Convert.ToDouble(rem.InvoiceAmount ?? 0);
            txtBudgetPercentage.Value = System.Convert.ToDouble(rem.BudgetPercentage ?? 0);
            txtBudgetAllocation.Value = System.Convert.ToDouble(rem.BudgetAllocation ?? 0);
            txtTotalDeductions.Value = System.Convert.ToDouble(rem.DeductionAmount ?? 0);
            txtBudget.Value = System.Convert.ToDouble(rem.BudgetAmount ?? 0);

            txtTotalMedicIGD.Value = System.Convert.ToDouble(rem.TotalFeeMedisIgd ?? 0);

            //RefreshGridSummary(rem.RemunID);
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(BusinessObject.ServiceFeeRemunRsucdr entity)
        {
            entity.RemunNo = txtRemunNo.Text;
            
            entity.PeriodMonth = System.Convert.ToInt32(cboMonth.SelectedValue);
            entity.PeriodYear = System.Convert.ToInt32(txtYear.Text);
            entity.IsBPJS = true;

            entity.Notes = txtNotes.Text;
            entity.IsApproved = false;
            entity.IsVoid = false;

            entity.InvoiceAmount = System.Convert.ToDecimal(txtInvoiceAmount.Value ?? 0);
            entity.BudgetPercentage = System.Convert.ToDecimal(txtBudgetPercentage.Value ?? 0);
            entity.BudgetAllocation = System.Convert.ToDecimal(txtBudgetAllocation.Value ?? 0);
            entity.DeductionAmount = System.Convert.ToDecimal(txtTotalDeductions.Value ?? 0);
            entity.BudgetAmount = System.Convert.ToDecimal(txtBudget.Value ?? 0);

            entity.TotalFeeDirektur = System.Convert.ToDecimal(txtTotalDirector.Value ?? 0);
            entity.TotalFeeStruktural = System.Convert.ToDecimal(txtTotalStaff.Value ?? 0);
            entity.TotalFeeMedis = System.Convert.ToDecimal(txtTotalMedic.Value ?? 0);
            entity.TotalFeeMedisIgd = System.Convert.ToDecimal(txtTotalMedicIGD.Value ?? 0);
            entity.TotalFeeUnit = System.Convert.ToDecimal(txtTotalUnit.Value ?? 0);
            entity.TotalFeePemerataan = System.Convert.ToDecimal(txtTotalEquality.Value ?? 0);

            if (entity.es.IsAdded)
            {
                entity.CreateByUserID = AppSession.UserLogin.UserID;
                entity.CreateDateTime = DateTime.Now;// DateTime.Now;
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
            var IsNew = string.IsNullOrEmpty(txtRemunNo.Text);

            ServiceFeeRemunRsucdr entity = new ServiceFeeRemunRsucdr();
            
            if (IsNew) { 
                entity.AddNew();
            }
            else {
                entity.LoadByRemunNo(txtRemunNo.Text);
            }

            SetEntityValue(entity);

            using (esTransactionScope trans = new esTransactionScope())
            {
                if(IsNew)
                {
                    var autono = GetAutoNumber();
                    if(autono == null) {
                        return "Generating transaction number failed";
                    }
                    txtRemunNo.Text = autono.LastCompleteNumber;
                    entity.RemunNo = txtRemunNo.Text;
                    autono.Save();
                }

                entity.Save();

                foreach (var sfri in sfrInvoices)
                {
                    if (sfri.es.IsAdded)
                    {
                        sfri.RemunID = entity.RemunID;

                        sfri.CreateByUserID = AppSession.UserLogin.UserID;
                        sfri.CreateDateTime = DateTime.Now;// DateTime.Now;
                        sfri.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        sfri.LastUpdateDateTime = DateTime.Now;// DateTime.Now;
                    }
                    //Last Update Status
                    if (sfri.es.IsModified)
                    {
                        sfri.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        sfri.LastUpdateDateTime = DateTime.Now;// DateTime.Now;
                    }
                }
                sfrInvoices.Save();

                foreach (var sfrd in sfrDeductions)
                {
                    if (sfrd.es.IsAdded)
                    {
                        sfrd.RemunID = entity.RemunID;

                        sfrd.CreateByUserID = AppSession.UserLogin.UserID;
                        sfrd.CreateDateTime = DateTime.Now;// DateTime.Now;
                        sfrd.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        sfrd.LastUpdateDateTime = DateTime.Now;// DateTime.Now;
                    }
                    //Last Update Status
                    if (sfrd.es.IsModified)
                    {
                        sfrd.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        sfrd.LastUpdateDateTime = DateTime.Now;// DateTime.Now;
                    }
                }
                sfrDeductions.Save();

                if (!IsNew) {
                    var sfrOldColl = new ServiceFeeRsucdrCollection();
                    sfrOldColl.LoadByRemunID(entity.RemunID ?? 0);
                    sfrOldColl.MarkAllAsDeleted();
                    sfrOldColl.Save();
                }

                foreach (var sfr in sfrDetail)
                {
                    if (sfr.es.IsAdded)
                    {
                        sfr.RemunID = entity.RemunID;

                        sfr.CreateByUserID = AppSession.UserLogin.UserID;
                        sfr.CreateDateTime = DateTime.Now;// DateTime.Now;
                        sfr.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        sfr.LastUpdateDateTime = DateTime.Now;// DateTime.Now;
                    }
                    //Last Update Status
                    if (sfr.es.IsModified)
                    {
                        sfr.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        sfr.LastUpdateDateTime = DateTime.Now;// DateTime.Now;
                    }
                }
                sfrDetail.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            return string.Empty;
        }

        private void MoveRecord(bool isNextRecord)
        {
            BusinessObject.ServiceFeeRemunRsucdrQuery que = new BusinessObject.ServiceFeeRemunRsucdrQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.RemunNo > txtRemunNo.Text
                    );
                que.OrderBy(que.RemunNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.RemunNo < txtRemunNo.Text
                    );
                que.OrderBy(que.RemunNo.Descending);
            }

            BusinessObject.ServiceFeeRemunRsucdr entity = new BusinessObject.ServiceFeeRemunRsucdr();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function
       
        #endregion

        protected void grdInvoices_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdInvoices.DataSource = sfrInvoices;
        }

        protected void grdDeductions_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdDeductions.DataSource = sfrDeductions;
        }

        protected void grdInvoices_InsertCommand(object sender, GridCommandEventArgs e)
        {
            RemunDetailItemInvoice userControl = (RemunDetailItemInvoice)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                foreach (var inv in userControl.InvoicesAmount) {
                    if (!sfrInvoices.Where(s => s.InvoiceNo == inv.Key).Any()) {
                        var entity = sfrInvoices.AddNew();
                        entity.InvoiceNo = inv.Key;
                        entity.Amount = inv.Value;
                    }
                }
            }

            //Stay in insert mode
            //e.Canceled = true;
            grdInvoices.Rebind();
        }

        protected void grdInvoices_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            string InvNo = (item.OwnerTableView.DataKeyValues[item.ItemIndex][ServiceFeeRemunRsucdrInvoicesMetadata.ColumnNames.InvoiceNo]).ToString();

            var entity = sfrInvoices.Where(s => s.InvoiceNo == InvNo).FirstOrDefault();
            if (entity != null) entity.MarkAsDeleted();
        }

        protected void btnCalculateBudget_Click(object sender, EventArgs e)
        {
            // budget allocation
            txtInvoiceAmount.Value = System.Convert.ToDouble(sfrInvoices.Sum(s => s.Amount));
            txtBudgetPercentage.Value = System.Convert.ToDouble(AppSession.Parameter.RemunBudgedPercentage);
            txtBudgetAllocation.Value = Math.Round((txtInvoiceAmount.Value ?? 0) / 100 * (txtBudgetPercentage.Value ?? 0), 2, MidpointRounding.ToEven);

            // deduction
            foreach (var dec in sfrDeductions) {
                if ((!string.IsNullOrEmpty(dec.Formula)) && (dec.Formula != "0")) {
                    var expr = new Expression(dec.Formula.Replace("@", ""), EvaluateOptions.IgnoreCase);
                    if (dec.Formula.Contains("Invoice")) {
                        expr.Parameters.Add("Invoice", txtInvoiceAmount.Value);
                    }
                    if (dec.Formula.Contains("Pagu"))
                    {
                        expr.Parameters.Add("Pagu", txtBudgetAllocation.Value);
                    }
                    dec.Amount = Math.Round(System.Convert.ToDecimal(expr.Evaluate()), 2, MidpointRounding.ToEven);
                }
            }

            txtTotalDeductions.Value = System.Convert.ToDouble(sfrDeductions.Sum(s => s.Amount ?? 0));
            txtBudget.Value = txtBudgetAllocation.Value - txtTotalDeductions.Value;

            grdDeductions.Rebind();

            CalculateDetail();

            grdSummary.Rebind();
        }

        private void CalculateDetail() {
            var sfrColl = new ServiceFeeRsucdrCollection();
            var query = sfrColl.BaseQuery();
            query.Where(query.RemunID == 0); // dummy, jangan dibuang karena ini sangat diperlukan demi keamana negara
            sfrColl.Load(query);

            decimal bUgd = 0;

            if (sfrInvoices.Any())
            {
                var sfq = new ServiceFeeQuery("sf");
                var feeq = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
                var iviq = new InvoicesItemQuery("ivi");
                var ivq = new InvoicesQuery("iv");
                var parq = new ParamedicQuery("par");
                var tc = new TransChargesQuery("tc");

                sfq.InnerJoin(feeq).On(sfq.TransactionNo == feeq.TransactionNo && sfq.SequenceNo == feeq.SequenceNo && sfq.TariffComponentID == feeq.TariffComponentID)
                    .InnerJoin(iviq).On(feeq.RegistrationNoMergeTo == iviq.RegistrationNo)
                    .InnerJoin(ivq).On(iviq.InvoiceNo == ivq.InvoiceNo)
                    .InnerJoin(parq).On(feeq.ParamedicID == parq.ParamedicID)
                    .InnerJoin(tc).On(sfq.TransactionNo == tc.TransactionNo)
                    .Where(
                        ivq.InvoiceNo.In(sfrInvoices.Select(s => s.InvoiceNo)),
                        ivq.IsApproved == true, ivq.IsVoid == false
                    ).Select(
                        sfq,
                        feeq.ParamedicID.As("refTo_ParamedicID"),
                        parq.ParamedicName.As("refTo_ParamedicName"),
                        tc.ToServiceUnitID.As("refTo_ToServiceUnitID"),
                        feeq.Qty.As("refTo_Qty")
                    );
                sfq.es.Distinct = true;

                var sfColl = new ServiceFeeCollection();
                sfColl.Load(sfq);

                // kosongkan yang dokter IGD, cari dokter umum
                foreach (var sf in sfColl)
                {
                    sf.IsIGD = false;
                }

                var parColl = new ParamedicCollection();
                parColl.Query.Where(parColl.Query.SRParamedicRL1 == "020"/*Umum*/);
                parColl.LoadAll();

                var suIgds = AppParameter.GetParameterValue(AppParameter.ParameterItem.ServiceUnitIdIgdForRemun).Split(';');

                var sfIgds = sfColl.Where(s => parColl.Select(p =>
                    p.ParamedicID).Contains(s.ParamedicID) &&
                    suIgds.Contains(s.ToServiceUnitID));

                foreach (var sfIgd in sfIgds) {
                    //sfIgd.FeeMedis = 0;
                    sfIgd.IsIGD = true;
                }
                // end of kosongkan yang dokter IGD

                decimal tDir = sfColl.Sum(sf => sf.FeeDirektur ?? 0);
                decimal tStr = sfColl.Sum(sf => sf.FeeStruktural ?? 0);
                decimal tMed = sfColl.Where(sf => sf.IsIGD == false).Sum(sf => sf.FeeMedis ?? 0);
                decimal tUn = sfColl.Sum(sf => sf.FeeUnit ?? 0);
                decimal tPmr = sfColl.Sum(sf => sf.FeePemerataan ?? 0);
                decimal tTotal = tDir + tStr + tMed + tUn + tPmr;

                decimal budget = System.Convert.ToDecimal(txtBudget.Value ?? 0);

                decimal bDir = Math.Round(tDir / tTotal * budget, MidpointRounding.ToEven);
                decimal bStr = Math.Round(tStr / tTotal * budget, MidpointRounding.ToEven);
                decimal bMed = Math.Round(tMed / tTotal * budget, MidpointRounding.ToEven);
                decimal bUn = Math.Round(tUn / tTotal * budget, MidpointRounding.ToEven);
                decimal bPmr = Math.Round(tPmr / tTotal * budget, MidpointRounding.ToEven);
                decimal bTotal = bDir + bStr + bMed + bUn + bPmr;

                if (bTotal != budget) bPmr += (budget - bTotal);
                bTotal = bDir + bStr + bMed + bUn + bPmr;

                bUgd = Math.Round(bMed * 1 / 100, 2, MidpointRounding.ToEven);
                bMed = bMed - bUgd;

                foreach (var sf in sfColl)
                {
                    var sfr = sfrColl.AddNew();
                    sfr.TransactionNo = sf.TransactionNo;
                    sfr.SequenceNo = sf.SequenceNo;
                    sfr.TariffComponentID = sf.TariffComponentID;
                    sfr.RegistrationNo = sf.RegistrationNo;

                    sfr.FeeDirektur = Math.Round((sf.FeeDirektur ?? 0) / tDir * bDir, 2, MidpointRounding.ToEven);
                    sfr.FeeStruktural = Math.Round((sf.FeeStruktural ?? 0) / tStr * bStr, 2, MidpointRounding.ToEven);
                    if (sf.IsIGD)
                    {
                        sfr.FeeMedis = 0;
                    }
                    else {
                        sfr.FeeMedis = Math.Round((sf.FeeMedis ?? 0) / tMed * bMed, 2, MidpointRounding.ToEven);
                    }
                    sfr.FeeUnit = Math.Round((sf.FeeUnit ?? 0) / tUn * bUn, 2, MidpointRounding.ToEven);
                    sfr.FeePemerataan = Math.Round((sf.FeePemerataan ?? 0) / tPmr * bPmr, 2, MidpointRounding.ToEven);

                    sfr.IsParamedicFeeRemun = sf.IsParamedicFeeRemun;
                    sfr.IsParamedicEmergency = sf.IsIGD;
                    sfr.Ctr = Math.Abs(sf.FeeMedis ?? 0) > 0 ? (sf.Qty > 0 ? 1 : -1) : 0;

                    sfr.CreateByUserID = AppSession.UserLogin.UserID;
                    sfr.CreateDateTime = DateTime.Now;
                    sfr.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    sfr.LastUpdateDateTime = DateTime.Now;

                    sfr.ParamedicID = sf.ParamedicID;
                    sfr.ParamedicName = sf.ParamedicName;
                }
                // sisa
                
                decimal fDir = sfrColl.Sum(sf => sf.FeeDirektur ?? 0);
                decimal fStr = sfrColl.Sum(sf => sf.FeeStruktural ?? 0);
                decimal fMed = sfrColl.Sum(sf => sf.FeeMedis ?? 0);
                decimal fUn = sfrColl.Sum(sf => sf.FeeUnit ?? 0);
                decimal fPmr = sfrColl.Sum(sf => sf.FeePemerataan ?? 0);

                if (fDir != bDir) sfrColl.Where(sfr => (sfr.FeeDirektur ?? 0) != 0).Last().FeeDirektur += (bDir - fDir);
                if (fStr != bStr) sfrColl.Where(sfr => (sfr.FeeStruktural ?? 0) != 0).Last().FeeStruktural += (bStr - fStr);
                if (fMed != bMed) sfrColl.Where(sfr => (sfr.FeeMedis ?? 0) != 0).Last().FeeMedis += (bMed - fMed);
                if (fUn != bUn) sfrColl.Where(sfr => (sfr.FeeUnit ?? 0) != 0).Last().FeeUnit += (bUn - fUn);
                if (fPmr != bPmr) sfrColl.Where(sfr => (sfr.FeePemerataan ?? 0) != 0).Last().FeePemerataan += (bPmr - fPmr);

                #region Proporsi IGD
                var sfrIgds = sfrColl.Where(sfr => sfr.IsParamedicEmergency ?? false);
                var sfrCount = sfrIgds.Sum(s => s.Ctr);
                if (sfrCount > 0) {
                    foreach (var sfrIgd in sfrIgds)
                    {
                        sfrIgd.FeeMedis = Math.Round(bUgd / sfrCount * sfrIgd.Ctr, 2, MidpointRounding.ToEven);
                    }

                    var sumPopIGD = sfrIgds.Sum(m => m.FeeMedis ?? 0);

                    if (bUgd != sumPopIGD) {
                        var sisa = bUgd - sumPopIGD;
                        if (Math.Abs(sisa) > 50)
                        {
                            throw new Exception("Invalid rounding value, the value is too high");
                        }
                        else {
                            sfrIgds.Last().FeeMedis += sisa;
                        }
                    }
                }
                #endregion
            }

            sfrDetail = sfrColl;

            txtTotalMedicIGD.Value = System.Convert.ToDouble(bUgd);
        }

        protected void grdDeductions_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            RemunDetailItemDeduction ctl = (RemunDetailItemDeduction)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (ctl == null) return;

            var dec = sfrDeductions.Where(s => s.SRRemunDeduction == ctl.SRRemunDeduction).FirstOrDefault();
            if (dec != null) {
                dec.Amount = ctl.Amount;
            }

            //grdDeductions.Rebind();
            btnCalculateBudget_Click(btnCalculateBudget, new EventArgs());
        }

        protected void grdDeductions_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = ((GridDataItem)e.Item);
                var dec = ((ServiceFeeRemunRsucdrDeductions)e.Item.DataItem);
                if (!string.IsNullOrEmpty(dec.Formula) && dec.Formula != "0") {
                    item["colEdit"].Controls[0].Visible = false;
                } 
            }
        }

        protected void grdSummary_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var dtSum = sfrDetail.GroupBy(sf => new { sf.ParamedicID, sf.ParamedicName })
                .Select(sf => new
                {
                    sf.Key.ParamedicID,
                    sf.Key.ParamedicName,
                    FeeMedis = sf.Sum(s => s.FeeMedis ?? 0)
                });
            grdSummary.DataSource = dtSum;

            txtTotalDirector.Value = System.Convert.ToDouble(sfrDetail.Sum(s => s.FeeDirektur ?? 0));
            txtTotalStaff.Value = System.Convert.ToDouble(sfrDetail.Sum(s => s.FeeStruktural ?? 0));
            txtTotalMedic.Value = System.Convert.ToDouble(sfrDetail.Sum(s => s.FeeMedis ?? 0));
            txtTotalUnit.Value = System.Convert.ToDouble(sfrDetail.Sum(s => s.FeeUnit ?? 0));
            txtTotalEquality.Value = System.Convert.ToDouble(sfrDetail.Sum(s => s.FeePemerataan ?? 0));

            txtGrandTotal.Value = txtTotalDirector.Value + txtTotalStaff.Value + txtTotalMedic.Value + txtTotalUnit.Value + txtTotalEquality.Value;
        }
    }
}