using System;
using System.Drawing;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class OperationalTimeDetail : BasePageDetail
    {
        private void SetEntityValue(OperationalTime entity)
        {
            entity.OperationalTimeID = txtOperationalTimeID.Text;
            entity.OperationalTimeName = txtOperationalTimeName.Text;
            entity.StartTime1 = GetHourMinute(txtStartTime1.SelectedDate);
            entity.EndTime1 = GetHourMinute(txtEndTime1.SelectedDate);
            entity.StartTime2 = GetHourMinute(txtStartTime2.SelectedDate);
            entity.EndTime2 = GetHourMinute(txtEndTime2.SelectedDate);
            entity.StartTime3 = GetHourMinute(txtStartTime3.SelectedDate);
            entity.EndTime3 = GetHourMinute(txtEndTime3.SelectedDate);
            entity.StartTime4 = GetHourMinute(txtStartTime4.SelectedDate);
            entity.EndTime4 = GetHourMinute(txtEndTime4.SelectedDate);
            entity.StartTime5 = GetHourMinute(txtStartTime5.SelectedDate);
            entity.EndTime5 = GetHourMinute(txtEndTime5.SelectedDate);
            entity.OperationalTimeBackcolor = ColorTranslator.ToHtml(txtOperationalTimeBackcolor.SelectedColor);

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private string GetHourMinute(DateTime? dateTime)
        {
            if (dateTime == null) return "";
            return dateTime.Value.ToString("HH:mm");
        }

        private void MoveRecord(bool isNextRecord)
        {
            OperationalTimeQuery que = new OperationalTimeQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.OperationalTimeID > txtOperationalTimeID.Text);
                que.OrderBy(que.OperationalTimeID.Ascending);
            }
            else
            {
                que.Where(que.OperationalTimeID < txtOperationalTimeID.Text);
                que.OrderBy(que.OperationalTimeID.Descending);
            }
            OperationalTime entity = new OperationalTime();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            OperationalTime entity = new OperationalTime();
            if (parameters.Length > 0)
            {
                String operationalTimeID = (String)parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(operationalTimeID);
            }
            else
                entity.LoadByPrimaryKey(txtOperationalTimeID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            OperationalTime operationalTime = (OperationalTime)entity;
            txtOperationalTimeID.Text = operationalTime.OperationalTimeID;
            txtOperationalTimeName.Text = operationalTime.OperationalTimeName;
            txtStartTime1.SelectedDate = GetForDisplayTime(operationalTime.StartTime1);
            txtEndTime1.SelectedDate = GetForDisplayTime(operationalTime.EndTime1);
            txtStartTime2.SelectedDate = GetForDisplayTime(operationalTime.StartTime2);
            txtEndTime2.SelectedDate = GetForDisplayTime(operationalTime.EndTime2);
            txtStartTime3.SelectedDate = GetForDisplayTime(operationalTime.StartTime3);
            txtEndTime3.SelectedDate = GetForDisplayTime(operationalTime.EndTime3);
            txtStartTime4.SelectedDate = GetForDisplayTime(operationalTime.StartTime4);
            txtEndTime4.SelectedDate = GetForDisplayTime(operationalTime.EndTime4);
            txtStartTime5.SelectedDate = GetForDisplayTime(operationalTime.StartTime5);
            txtEndTime5.SelectedDate = GetForDisplayTime(operationalTime.EndTime5);
            txtOperationalTimeBackcolor.SelectedColor = ColorTranslator.FromHtml(operationalTime.OperationalTimeBackcolor);
        }

        private DateTime? GetForDisplayTime(string time)
        {
            if (string.IsNullOrEmpty(time)) return null;
            if (time.Trim() == "") return null;

            string[] times = time.Split(':');
            return new DateTime(2009, 1, 1, Convert.ToInt32(times[0]), Convert.ToInt32(times[1]), 0);
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new OperationalTime());
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
            auditLogFilter.PrimaryKeyData = string.Format("OperationalTimeID='{0}'", txtOperationalTimeID.Text.Trim());
            auditLogFilter.TableName = "OperationalTime";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtOperationalTimeID.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "OperationalTimeSearch.aspx";
            UrlPageList = "OperationalTimeList.aspx";

            ProgramID = AppConstant.Program.OperationalTime;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            OperationalTime entity = new OperationalTime();
            entity.LoadByPrimaryKey(txtOperationalTimeID.Text);
            entity.MarkAsDeleted();
            SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            OperationalTime entity = new OperationalTime();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(OperationalTime entity)
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
            OperationalTime entity = new OperationalTime();
            if (entity.LoadByPrimaryKey(txtOperationalTimeID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion
    }
}
