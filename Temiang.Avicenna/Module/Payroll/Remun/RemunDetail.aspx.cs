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

namespace Temiang.Avicenna.Module.Payroll.Remun
{
    public partial class RemunDetail : BasePageDetail
    {
        private AppAutoNumberLast GetAutoNumber()
        {
            if (string.IsNullOrEmpty(txtYear.Text)) return null;
            if (string.IsNullOrEmpty(cboMonth.SelectedValue)) return null;

            var transCode = new AppAutoNumberTransactionCode();
            if (!transCode.LoadByPrimaryKey(BusinessObject.Reference.TransactionCode.RemunerationEmp))
                return null;

            int yr = System.Convert.ToInt32(txtYear.Text);
            int mnth = System.Convert.ToInt32(cboMonth.SelectedValue);

            var date = new DateTime(yr, mnth, 1);

            return Helper.GetNewAutoNumber(date, transCode.SRAutoNumber);
        }
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "RemunSearch.aspx";
            UrlPageList = string.Format("RemunList.aspx");

            IsUsingBeforeVoidValidation = true;

            ProgramID = AppConstant.Program.EmployeeRemun;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                RemunList.PopulateMonth(cboMonth);
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
            var rem = new BusinessObject.EmployeeRemun();
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
            var rem = new BusinessObject.EmployeeRemun();
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
            var rem = new BusinessObject.EmployeeRemun();
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

            using (var trans = new esTransactionScope())
            {
                rem.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            args.MessageText = "Unvoid is disabled";
            args.IsCancel = true;
            return;
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
            OnPopulateEntryControl(new EmployeeRemun());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            EmployeeRemun entity = new EmployeeRemun();
            if (entity.LoadByRemunNo(txtRemunNo.Text))
            {
                if (entity.IsApproved ?? false)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                }
                else
                {
                    var dti = new EmployeeRemunDetailCollection();
                    dti.Query.Where(dti.Query.RemunID == entity.RemunID);
                    dti.LoadAll();

                    dti.MarkAllAsDeleted();
                    entity.MarkAsDeleted();

                    using (esTransactionScope trans = new esTransactionScope())
                    {
                        dti.Save();
                        entity.Save();

                        trans.Complete();
                    }
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
            if (string.IsNullOrEmpty(txtYear.Text))
            {
                args.MessageText = "Invalid Year";
                args.IsCancel = true;
                return;
            }
            if (!Avicenna.Common.Helper.IsNumeric(txtYear.Text)) {
                args.MessageText = "Invalid Year";
                args.IsCancel = true;
                return;
            }
            if (string.IsNullOrEmpty(cboMonth.SelectedValue))
            {
                args.MessageText = "Invalid month";
                args.IsCancel = true;
                return;
            }
            if (grdDetail.MasterTableView.Items.Count == 0)
            {
                args.MessageText = "Detail can not be empty";
                args.IsCancel = true;
                return;
            }
            
            string ret = SaveEntity(true);
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
            if (string.IsNullOrEmpty(txtYear.Text))
            {
                args.MessageText = "Invalid Year";
                args.IsCancel = true;
                return;
            }
            if (!Avicenna.Common.Helper.IsNumeric(txtYear.Text))
            {
                args.MessageText = "Invalid Year";
                args.IsCancel = true;
                return;
            }
            if (string.IsNullOrEmpty(cboMonth.SelectedValue))
            {
                args.MessageText = "Invalid month";
                args.IsCancel = true;
                return;
            }
            if (grdDetail.MasterTableView.Items.Count == 0) {
                args.MessageText = "Detail can not be empty";
                args.IsCancel = true;
                return;
            }

            string ret = SaveEntity(false);
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
            auditLogFilter.PrimaryKeyData = string.Format("RemunNo='{0}'", txtRemunNo.Text.Trim());
            auditLogFilter.TableName = "EmployeeRemun";
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
            //grdRemun.Rebind();
            btnCalculate.Enabled = (newVal != AppEnum.DataMode.Read);
            //btnExportExcel.Enabled = (newVal == AppEnum.DataMode.Read);
            //btnExportExcelDetail.Enabled = (newVal == AppEnum.DataMode.Read);
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
            BusinessObject.EmployeeRemun entity = new BusinessObject.EmployeeRemun();
            if (parameters.Length > 0)
            {
                int remunid = System.Convert.ToInt32(parameters[0]);

                entity.LoadByPrimaryKey(remunid);
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
            BusinessObject.EmployeeRemun rem = (BusinessObject.EmployeeRemun)entity;

            txtRemunNo.Text = rem.RemunNo;

            txtYear.Text = rem.PeriodYear.ToString();
            cboMonth.SelectedValue = rem.PeriodMonth.ToString();

            chkIsVoid.Checked = rem.IsVoid ?? false;
            chkIsApproved.Checked = rem.IsApproved ?? false;
            txtNotes.Text = rem.Notes;
            txtFundAllocPosition.Value = System.Convert.ToDouble(rem.FundAllocPosition ?? 0);
            txtFundAllocIncentive.Value = System.Convert.ToDouble(rem.FundAllocInsetif ?? 0);
            txtKursPosition.Value = System.Convert.ToDouble(rem.KursPosition ?? 0);
            txtKursInsentif.Value = System.Convert.ToDouble(rem.KursInsentif ?? 0);

            LoadGrid(rem);

            //RefreshGridSummary(dtFee, rem.RemunID);
        }

        private void LoadGrid(EmployeeRemun entity) 
        {
            int id = -1;
            if (entity.RemunID.HasValue)
                id = entity.RemunID.Value;

            var dtb = (new EmployeeRemunCollection()).GetRemunByID(id);

            var cgColl = new CoorporateGradeCollection();
            cgColl.LoadAll();

            var dsSum = cgColl.Select(c => new {
                c.CoorporateGradeLevel,
                c.CoorporateGradeCoefficient,
                CoorporateGradeValueSum = dtb.AsEnumerable()
                    .Where(f => System.Convert.ToInt16(f["CoorporateGradeLevel"]) == c.CoorporateGradeLevel)
                    .Sum(f => System.Convert.ToInt32(f["CoorporateGradeValue"])),
                EmployeeCount = dtb.AsEnumerable()
                    .Where(f => System.Convert.ToInt16(f["CoorporateGradeLevel"]) == c.CoorporateGradeLevel)
                    .Count()
            });

            grdSummary.DataSource = dsSum;
            grdSummary.DataBind();

            //RefreshGridSummary(dtb, new int?());
            grdDetail.DataSource = dtb;
            grdDetail.DataBind();
        }

        #endregion

        #region Private Method Standard

        private string SaveEntity(bool IsNew)
        {
            EmployeeRemun entity = new EmployeeRemun();
            var detailColl = new EmployeeRemunDetailCollection();

            if (IsNew) { 
                entity.AddNew();
            }
            else {
                entity.LoadByRemunNo(txtRemunNo.Text);
                detailColl.Query.Where(detailColl.Query.RemunID == entity.RemunID);
                detailColl.LoadAll();
            }

            entity.PeriodYear = System.Convert.ToInt32(txtYear.Text);
            entity.PeriodMonth = System.Convert.ToInt32(cboMonth.SelectedValue);

            entity.FundAllocPosition = System.Convert.ToDecimal(txtFundAllocPosition.Value);
            entity.FundAllocInsetif = System.Convert.ToDecimal(txtFundAllocIncentive.Value);
            entity.KursPosition = System.Convert.ToDecimal(txtKursPosition.Value);
            entity.KursInsentif = System.Convert.ToDecimal(txtKursInsentif.Value);
            entity.Notes = txtNotes.Text;
            entity.IsApproved = false;
            entity.IsVoid = false;

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

                if (!IsNew)
                {
                    detailColl.MarkAllAsDeleted();
                    detailColl.Save();
                }

                foreach (GridDataItem gdi in grdDetail.MasterTableView.Items)
                {
                    var dc = detailColl.AddNew();
                    dc.RemunID = entity.RemunID;
                    dc.PersonID = System.Convert.ToInt32(gdi.GetDataKeyValue("PersonID"));
                    if ((gdi.GetDataKeyValue("PositionID") is DBNull)) {
                        dc.PositionID = null;
                    }
                    else {
                        dc.PositionID = System.Convert.ToInt32(gdi.GetDataKeyValue("PositionID"));
                    }
                    dc.SREmployeeStatus = "";
                    dc.CoorporateGradeLevel = System.Convert.ToInt32(gdi["CoorporateGradeLevel"].Text);
                    dc.CoorporateGradeValue = System.Convert.ToInt32((gdi.FindControl("txtCoorporateGradeValue") as RadNumericTextBox).Value);
                    dc.CoorporateGradeCoefficient = System.Convert.ToDecimal(gdi["CoorporateGradeCoefficient"].Text);

                    dc.PositionFeeValue = System.Convert.ToDecimal((gdi.FindControl("txtPositionFeeValue") as RadNumericTextBox).Value);
                    dc.InsentifFeeGrossValue = dc.CoorporateGradeValue * System.Convert.ToDecimal(txtKursInsentif.Value ?? 0) * dc.CoorporateGradeCoefficient;
                    dc.RenkinIndex = System.Convert.ToDecimal(gdi["Index"].Text);
                    dc.InsentifFeeValue = System.Convert.ToDecimal((gdi.FindControl("txtInsentifFeeValue") as RadNumericTextBox).Value);

                    dc.CreateByUserID = AppSession.UserLogin.UserID;
                    dc.CreateDateTime = DateTime.Now;
                    dc.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    dc.LastUpdateDateTime = DateTime.Now;
                }
                detailColl.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            return string.Empty;
        }

        private void MoveRecord(bool isNextRecord)
        {
            BusinessObject.EmployeeRemunQuery que = new BusinessObject.EmployeeRemunQuery();
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

            BusinessObject.EmployeeRemun entity = new BusinessObject.EmployeeRemun();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function
       
        private void RefreshGridSummary(DataTable dtRemun, int? RemunID) {

            

            //decimal fundAllocation = txtFundAllocProcedure.Value.ToDecimal();
            //lblFundAllocation.Text = fundAllocation.ToString("n2");
            //decimal tAllocated = dtSum.AsEnumerable().Sum(r => System.Convert.ToDecimal(r["ProcedureFeeValue"]));
            //lblTotalAllocated.Text = tAllocated.ToString("n2");
            //lblDifference.Text = (fundAllocation - tAllocated).ToString("n2");
        }
        #endregion

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtYear.Text) || (!Helper.IsNumeric(txtYear.Text)))
            {
                ShowInformationHeader("Invalid Year");
                return;
            }
            if (string.IsNullOrEmpty(cboMonth.SelectedValue))
            {
                ShowInformationHeader("Invalid Month");
                return;
            }

            var remColl = new EmployeeRemunCollection();
            remColl.Query.Where(
                remColl.Query.PeriodYear == txtYear.Text,
                remColl.Query.PeriodMonth == cboMonth.SelectedValue,
                remColl.Query.RemunNo != txtRemunNo.Text);
            if (remColl.LoadAll()) {
                ShowInformationHeader("Selected period has exsist");
                return;
            }

            //var empColl = new VwEmployeeTableCollection();
            //empColl.LoadAll();

            var date = new DateTime(System.Convert.ToInt32(txtYear.Text), System.Convert.ToInt32(cboMonth.SelectedValue), 1);
            date = date.AddMonths(1);
            date = date.AddDays(-1);

            var dtb = (new EmployeeRemunCollection()).GetNewRemun(date);

            var cgColl = new CoorporateGradeCollection();
            cgColl.LoadAll();

            var dsSum = cgColl.Select(c => new {
                c.CoorporateGradeLevel,
                c.CoorporateGradeCoefficient,
                CoorporateGradeValueSum = dtb.AsEnumerable()
                    .Where(f => System.Convert.ToInt16(f["CoorporateGradeLevel"]) == c.CoorporateGradeLevel)
                    .Sum(f => System.Convert.ToInt32(f["CoorporateGradeValue"])),
                EmployeeCount = dtb.AsEnumerable()
                    .Where(f => System.Convert.ToInt16(f["CoorporateGradeLevel"]) == c.CoorporateGradeLevel)
                    .Count()
            });

            grdSummary.DataSource = dsSum;
            grdSummary.DataBind();

            var CoorporateGradeValueSum = dsSum.Sum(x => x.CoorporateGradeValueSum);
            if (CoorporateGradeValueSum == 0)
            {
                txtKursPosition.Value = 0;
                txtKursInsentif.Value = 0;
            }
            else {
                txtKursPosition.Value = txtFundAllocPosition.Value / dsSum.Sum(x => x.CoorporateGradeValueSum);
                txtKursInsentif.Value = txtFundAllocIncentive.Value / System.Convert.ToDouble(dsSum.Sum(x => x.CoorporateGradeValueSum * x.CoorporateGradeCoefficient));
            }
            

            dtb.Columns.Remove("rn");

            foreach (System.Data.DataRow row in dtb.Rows) {
                row["PositionFeeValue"] = System.Convert.ToDouble(row["CoorporateGradeValue"]) * (txtKursPosition.Value ?? 0);
                row["InsentifFeeValue"] = System.Convert.ToDouble(row["CoorporateGradeValue"]) * (txtKursInsentif.Value ?? 0) * System.Convert.ToDouble(row["CoorporateGradeCoefficient"]);
            }

            //RefreshGridSummary(dtb, new int?());
            grdDetail.DataSource = dtb;
            grdDetail.DataBind();

            

            ShowInformationHeader("Calculate complete");
        }

        protected void grdDetail_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                //item["CoefficientSummary"].ToolTip = "Summary of coefficient (click on Physician name to show detail coefficient)";
                //item["ProcedureFeeValue"].ToolTip = "Coorporate Grade Value * Coefficient * P1";
                //item["Total"].ToolTip = "Position Fee + Insentif Fee + Procedure Fee";
            }
        }
    }
}