using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Globalization;
using Telerik.Web.UI.Calendar;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class PayrollPeriodDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "PayrollPeriodSearch.aspx";
            UrlPageList = "PayrollPeriodList.aspx";

            ProgramID = AppConstant.Program.PayrollPeriod; //TODO: Isi ProgramID
            this.WindowSearch.Height = 400;

            txtPayrollPeriodID.Text = "1";

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRPaySequent, AppEnum.StandardReference.PaySequent);
                trMoslemThr.Visible = false;
            }

            //PopUp Search
            if (!IsCallback)
            {

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new PayrollPeriod());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            PayrollPeriod entity = new PayrollPeriod();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtPayrollPeriodID.Text)))
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
            var coll = new PayrollPeriodCollection();
            coll.Query.Where(coll.Query.PayrollPeriodCode.Trim() == txtPayrollPeriodCode.Text.Trim());
            coll.LoadAll();
            if (coll.Count > 0)
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            var entity = new PayrollPeriod();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new PayrollPeriod();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtPayrollPeriodID.Text)))
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
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("PayrollPeriodID='{0}'", txtPayrollPeriodID.Text.Trim());
            auditLogFilter.TableName = "PayrollPeriod";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtPayrollPeriodID.Enabled = (newVal == AppEnum.DataMode.New);

        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            PayrollPeriod entity = new PayrollPeriod();
            if (parameters.Length > 0)
            {
                string payrollPeriodID = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(payrollPeriodID));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtPayrollPeriodID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            PayrollPeriod payrollPeriod = (PayrollPeriod)entity;
            txtPayrollPeriodID.Value = Convert.ToDouble(payrollPeriod.PayrollPeriodID);
            txtPayrollPeriodCode.Text = payrollPeriod.PayrollPeriodCode;
            txtPayrollPeriodName.Text = payrollPeriod.PayrollPeriodName;
            cboSRPaySequent.SelectedValue = payrollPeriod.SRPaySequent;
            txtStartDate.SelectedDate = payrollPeriod.StartDate;
            txtEndDate.SelectedDate = payrollPeriod.EndDate;
            txtPayDate.SelectedDate = payrollPeriod.PayDate;
            txtSPTYear.Value = Convert.ToDouble(payrollPeriod.SPTYear);
            txtSPTMonth.Value = Convert.ToDouble(payrollPeriod.SPTMonth);
            chkIsMoslemTHR.Checked = payrollPeriod.IsMoslemTHR ?? false;

            //Display Data Detail
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(PayrollPeriod entity)
        {
            entity.PayrollPeriodID = Convert.ToInt32(txtPayrollPeriodID.Value);
            entity.PayrollPeriodCode = txtSPTYear.Value + string.Format("{0:00}", txtSPTMonth.Value) + cboSRPaySequent.SelectedValue;//txtPayrollPeriodCode.Text;
            entity.PayrollPeriodName = DateTimeFormatInfo.InvariantInfo.GetMonthName(Convert.ToInt32(txtSPTMonth.Value)) + " " + txtSPTYear.Value + " - " + cboSRPaySequent.Text;
            entity.SRPaySequent = cboSRPaySequent.SelectedValue;
            entity.StartDate = txtStartDate.SelectedDate;
            entity.EndDate = txtEndDate.SelectedDate;
            entity.PayDate = txtPayDate.SelectedDate;
            entity.SPTYear = Convert.ToInt32(txtSPTYear.Value);
            entity.SPTMonth = Convert.ToInt32(txtSPTMonth.Value);
            entity.IsMoslemTHR = chkIsMoslemTHR.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(PayrollPeriod entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                if (DataModeCurrent == AppEnum.DataMode.New)
                {
                    var closing = new ClosingWageTransaction
                    {
                        PayrollPeriodID = entity.PayrollPeriodID,
                        IsClosed = false,
                        LastUpdateByUserID = AppSession.UserLogin.UserID,
                        LastUpdateDateTime = DateTime.Now
                    };
                    closing.Save();
                }

                //Commit if success, Rollback if failed
                trans.Complete();

                txtPayrollPeriodID.Text = entity.PayrollPeriodID.ToString();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            PayrollPeriodQuery que = new PayrollPeriodQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.PayrollPeriodID > txtPayrollPeriodID.Text);
                que.OrderBy(que.PayrollPeriodID.Ascending);
            }
            else
            {
                que.Where(que.PayrollPeriodID < txtPayrollPeriodID.Text);
                que.OrderBy(que.PayrollPeriodID.Descending);
            }
            PayrollPeriod entity = new PayrollPeriod();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged

        #endregion

        protected void txtStartDate_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            txtSPTMonth.Value = txtStartDate.SelectedDate.Value.Month;
            txtSPTYear.Value = txtStartDate.SelectedDate.Value.Year;
        }
    }
}
