using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ConsumeMethodDetail : BasePageDetail
    {
        private void SetEntityValue(ConsumeMethod entity)
        {
            entity.SRConsumeMethod = txtSRConsumeMethod.Text;
            entity.SRConsumeMethodName = txtSRConsumeMethodName.Text;
            entity.TimeSequence = txtTimeSequence.Text;
            entity.SygnaText = txtSygnaText.Text;
            entity.LineNumber = txtLineNumber.Value.ToInt();
            entity.IterationQty = Convert.ToInt32(txtIterationQty.Value);
            entity.IterationInInterval = rbtIterationInInterval.SelectedValue;
            entity.Time01 = GetHourMinute(txtTime01.SelectedDate);
            entity.Time02 = GetHourMinute(txtTime02.SelectedDate);
            entity.Time03 = GetHourMinute(txtTime03.SelectedDate);
            entity.Time04 = GetHourMinute(txtTime04.SelectedDate);
            entity.Time05 = GetHourMinute(txtTime05.SelectedDate);
            entity.Time06 = GetHourMinute(txtTime06.SelectedDate);
            entity.Time07 = GetHourMinute(txtTime07.SelectedDate);
            entity.Time08 = GetHourMinute(txtTime08.SelectedDate);
            entity.Time09 = GetHourMinute(txtTime09.SelectedDate);
            entity.Time10 = GetHourMinute(txtTime10.SelectedDate);
            entity.IsActive = chkIsActive.Checked;
            
            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastCreateByUserID = AppSession.UserLogin.UserID;
                entity.LastCreateDateTime = DateTime.Now;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ConsumeMethodQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.SRConsumeMethod > txtSRConsumeMethod.Text);
                que.OrderBy(que.SRConsumeMethod.Ascending);
            }
            else
            {
                que.Where(que.SRConsumeMethod < txtSRConsumeMethod.Text);
                que.OrderBy(que.SRConsumeMethod.Descending);
            }
            var entity = new ConsumeMethod();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ConsumeMethod();
            if (parameters.Length > 0)
            {
                String id = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(id);
            }
            else
                entity.LoadByPrimaryKey(txtSRConsumeMethod.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var d = (ConsumeMethod)entity;
            txtSRConsumeMethod.Text = d.SRConsumeMethod;
            txtSRConsumeMethodName.Text = d.SRConsumeMethodName;
            txtTimeSequence.Text = d.TimeSequence;
            txtSygnaText.Text = d.SygnaText;
            txtLineNumber.Value = d.LineNumber.ToInt();
            txtIterationQty.Value = Convert.ToDouble(d.IterationQty);
            rbtIterationInInterval.SelectedValue = d.IterationInInterval;
            txtTime01.SelectedDate = GetForDisplayTime(d.Time01);
            txtTime02.SelectedDate = GetForDisplayTime(d.Time02);
            txtTime03.SelectedDate = GetForDisplayTime(d.Time03);
            txtTime04.SelectedDate = GetForDisplayTime(d.Time04);
            txtTime05.SelectedDate = GetForDisplayTime(d.Time05);
            txtTime06.SelectedDate = GetForDisplayTime(d.Time06);
            txtTime07.SelectedDate = GetForDisplayTime(d.Time07);
            txtTime08.SelectedDate = GetForDisplayTime(d.Time08);
            txtTime09.SelectedDate = GetForDisplayTime(d.Time09);
            txtTime10.SelectedDate = GetForDisplayTime(d.Time10);
            chkIsActive.Checked = d.IsActive ?? false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ConsumeMethod());
            chkIsActive.Checked = true;
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
            auditLogFilter.PrimaryKeyData = string.Format("SRConsumeMethod='{0}'", txtSRConsumeMethod.Text.Trim());
            auditLogFilter.TableName = "ConsumeMethod";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtSRConsumeMethod.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "ConsumeMethodSearch.aspx";
            UrlPageList = "ConsumeMethodList.aspx";

            ProgramID = AppConstant.Program.ConsumeMethod;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new ConsumeMethod();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(ConsumeMethod entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new ConsumeMethod();
            if (entity.LoadByPrimaryKey(txtSRConsumeMethod.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion

        private string GetHourMinute(DateTime? dateTime)
        {
            if (dateTime == null) return "";
            return dateTime.Value.ToString("HH:mm");
        }

        private DateTime? GetForDisplayTime(string time)
        {
            if (string.IsNullOrEmpty(time)) return null;
            if (time.Trim() == "") return null;

            string[] times = time.Split(':');
            return new DateTime(2009, 1, 1, Convert.ToInt32(times[0]), Convert.ToInt32(times[1]), 0);
        }

    }
}
