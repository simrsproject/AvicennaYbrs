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
    public partial class RemunDetailNonBPJS : BasePageDetail
    {

        private ServiceFeeCollection sfDetail
        {
            get {
                if (!IsPostBack) {
                    var sfrColl = new ServiceFeeCollection();

                    var sfrs = new ServiceFeeRemunRsucdr();
                    if (sfrs.LoadByRemunNo(txtRemunNo.Text)) {
                        sfrColl.LoadByRemunID(sfrs.RemunID ?? 0);
                    }

                    Session["sfDetail"] = sfrColl;
                }

                if(Session["sfDetail"] == null) Session["sfDetail"] = new ServiceFeeCollection();

                return (ServiceFeeCollection)Session["sfDetail"];
            }
            set
            {
                Session["sfDetail"] = value;
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
                    var sfees = new ServiceFeeCollection();
                    sfees.Query.Where(sfees.Query.RemunID == entity.RemunID);
                    sfees.LoadAll();
                    sfees.MarkAllAsDeleted();

                    entity.MarkAsDeleted();
                    using (esTransactionScope trans = new esTransactionScope())
                    {
                        sfees.Save();
                        entity.Save();
                        
                        trans.Complete();
                    }

                    sfDetail = null;

                    grdSummary1.Rebind();
                    grdSummary2.Rebind();
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

            rdpDateFrom.SelectedDate = rem.DischargeDateFrom;
            rdpDateTo.SelectedDate = rem.DischargeDateTo;

            //RefreshGridSummary(rem.RemunID);
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(BusinessObject.ServiceFeeRemunRsucdr entity)
        {
            entity.RemunNo = txtRemunNo.Text;
            
            entity.PeriodMonth = System.Convert.ToInt32(cboMonth.SelectedValue);
            entity.PeriodYear = System.Convert.ToInt32(txtYear.Text);
            entity.IsBPJS = false;
            entity.Notes = txtNotes.Text;

            entity.DischargeDateFrom = rdpDateFrom.SelectedDate.Value;
            entity.DischargeDateTo = rdpDateTo.SelectedDate.Value;

            entity.IsApproved = false;
            entity.IsVoid = false;

            entity.TotalFeeDirektur = System.Convert.ToDecimal(txtTotalDirector.Value ?? 0);
            entity.TotalFeeStruktural = System.Convert.ToDecimal(txtTotalStaff.Value ?? 0);
            entity.TotalFeeMedis = System.Convert.ToDecimal(txtTotalMedic.Value ?? 0);
            entity.TotalFeeUnit = System.Convert.ToDecimal(txtTotalUnit.Value ?? 0);
            entity.TotalFeePemerataan = System.Convert.ToDecimal(txtTotalEquality.Value ?? 0);
            entity.TotalFeeMedisIgd = 0;

            entity.InvoiceAmount =0;
            entity.BudgetPercentage = 0;
            entity.BudgetAllocation = 0;
            entity.DeductionAmount = 0;
            
            entity.BudgetAmount = (entity.TotalFeeDirektur ?? 0) + 
                (entity.TotalFeeStruktural ?? 0) + 
                (entity.TotalFeeMedis ?? 0) + 
                (entity.TotalFeeUnit ?? 0) + 
                (entity.TotalFeePemerataan ?? 0);

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

                foreach (var sf in sfDetail)
                {
                    sf.RemunID = entity.RemunID;
                    if (sf.es.IsModified)
                    {
                        sf.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        sf.LastUpdateDateTime = DateTime.Now;// DateTime.Now;
                    }
                }
                sfDetail.Save();

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

        protected void btnCalculateBudget_Click(object sender, EventArgs e)
        {
            if (!rdpDateFrom.SelectedDate.HasValue)
            {
                this.ShowInformationHeader("Invalid date from");
                return;
            }
            if (!rdpDateTo.SelectedDate.HasValue)
            {
                this.ShowInformationHeader("Invalid date to");
                return;
            }
            if (rdpDateFrom.SelectedDate.Value > rdpDateTo.SelectedDate.Value) {
                this.ShowInformationHeader("Date to must be greater than date from");
                return;
            }
            CalculateDetail();

            grdSummary1.Rebind();
            grdSummary2.Rebind();
        }

        private void CalculateDetail() {
            var sfrColl = new ServiceFeeRsucdrCollection();
            var query = sfrColl.BaseQuery();
            query.Where(query.RemunID == 0); // dummy, jangan dibuang karena ini sangat diperlukan demi keamana negara
            sfrColl.Load(query);

            var sfq = new ServiceFeeQuery("sf");
            var feeq = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
            var parq = new ParamedicQuery("par");
            var tc = new TransChargesQuery("tc");

            sfq.InnerJoin(feeq).On(sfq.TransactionNo == feeq.TransactionNo && sfq.SequenceNo == feeq.SequenceNo && sfq.TariffComponentID == feeq.TariffComponentID)
                .InnerJoin(parq).On(feeq.ParamedicID == parq.ParamedicID)
                .InnerJoin(tc).On(sfq.TransactionNo == tc.TransactionNo)
                .Where(
                    feeq.DischargeDateMergeTo.Between(rdpDateFrom.SelectedDate.Value, rdpDateTo.SelectedDate.Value),
                    sfq.IsParamedicFeeRemun == false,
                    sfq.RemunID.IsNull()
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

            sfDetail = sfColl;
        }

        protected void grdSummary_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var dtSum = sfDetail.GroupBy(sf => new { sf.ParamedicID, sf.ParamedicName })
                .Select(sf => new
                {
                    sf.Key.ParamedicID,
                    sf.Key.ParamedicName,
                    FeeMedis = sf.Sum(s => s.FeeMedis ?? 0)
                });
            var grd = (RadGrid)sender;
            var count = dtSum.Count();
            var mod = count % 2;
            if (grd.ID == "grdSummary1")
            {
                grd.DataSource = dtSum.OrderBy(d => d.ParamedicID).Take(count / 2);
            }
            else {
                grd.DataSource = dtSum.OrderBy(d => d.ParamedicID).Skip(count / 2).Take(count / 2 + mod);
            }

            txtTotalDirector.Value = System.Convert.ToDouble(sfDetail.Sum(s => s.FeeDirektur ?? 0));
            txtTotalStaff.Value = System.Convert.ToDouble(sfDetail.Sum(s => s.FeeStruktural ?? 0));
            txtTotalMedic.Value = System.Convert.ToDouble(sfDetail.Sum(s => s.FeeMedis ?? 0));
            txtTotalUnit.Value = System.Convert.ToDouble(sfDetail.Sum(s => s.FeeUnit ?? 0));
            txtTotalEquality.Value = System.Convert.ToDouble(sfDetail.Sum(s => s.FeePemerataan ?? 0));

            txtGrandTotal.Value = txtTotalDirector.Value + txtTotalStaff.Value + txtTotalMedic.Value + txtTotalUnit.Value + txtTotalEquality.Value;
        }
    }
}