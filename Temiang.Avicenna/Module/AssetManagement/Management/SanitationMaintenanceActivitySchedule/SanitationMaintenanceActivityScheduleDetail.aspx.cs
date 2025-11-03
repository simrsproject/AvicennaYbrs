using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.AssetManagement.Management
{
    public partial class SanitationMaintenanceActivityScheduleDetail : BasePageDetail
    {
        private void SetEntityValue(SanitationMaintenanceActivitySchedulePeriod entity, SanitationMaintenanceActivityScheduleCollection collDetail)
        {
            entity.SRWorkTradeItem = cboSRWorkTradeItem.SelectedValue;
            entity.ServiceUnitID = cboServiceUnitID.SelectedValue;
            entity.PeriodYear = cboPeriodYear.SelectedValue;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            collDetail.Query.Where
                (
                    collDetail.Query.SRWorkTradeItem == cboSRWorkTradeItem.SelectedValue,
                    collDetail.Query.ServiceUnitID == cboServiceUnitID.SelectedValue,
                    collDetail.Query.PeriodYear == cboPeriodYear.SelectedValue
                );
            collDetail.LoadAll();

            DataTable dtbEdited = SanitationMaintenanceActivitySchedules;
            string srWorkTradeItem = cboSRWorkTradeItem.SelectedValue;
            string serviceUnitId = cboServiceUnitID.SelectedValue;
            string year = cboPeriodYear.SelectedValue;

            //New and Updated
            foreach (DataRow row in dtbEdited.Rows)
            {
                DateTime date = (row.RowState == DataRowState.Added) ? Convert.ToDateTime(row["ScheduleDate"]) : Convert.ToDateTime(row["ScheduleDate", DataRowVersion.Original]);
                SanitationMaintenanceActivitySchedule item = collDetail.FirstOrDefault(itemSearch => itemSearch.SRWorkTradeItem == srWorkTradeItem && itemSearch.ServiceUnitID == serviceUnitId && itemSearch.PeriodYear == year && itemSearch.ScheduleDate == date);

                if (row.RowState == DataRowState.Deleted)
                {
                    if (item != null)
                        item.MarkAsDeleted();

                    continue;
                }

                if (item == null)
                {
                    item = collDetail.AddNew();
                    item.SRWorkTradeItem = cboSRWorkTradeItem.SelectedValue;
                    item.ServiceUnitID = cboServiceUnitID.SelectedValue;
                    item.ScheduleDate = Convert.ToDateTime(row["ScheduleDate"]);
                    item.PeriodYear = cboPeriodYear.SelectedValue;
                    item.PeriodDate = new DateTime(item.ScheduleDate.Value.Year, item.ScheduleDate.Value.Month, 1, 0, 0, 0);
                }

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new SanitationMaintenanceActivitySchedulePeriodQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.PeriodYear == cboPeriodYear.SelectedValue & que.ServiceUnitID == cboServiceUnitID.SelectedValue & que.SRWorkTradeItem > cboSRWorkTradeItem.SelectedValue);
                que.OrderBy(que.PeriodYear.Ascending);
            }
            else
            {
                que.Where(que.PeriodYear == cboPeriodYear.SelectedValue & que.ServiceUnitID == cboServiceUnitID.SelectedValue & que.SRWorkTradeItem < cboSRWorkTradeItem.SelectedValue);
                que.OrderBy(que.PeriodYear.Descending);
            }

            var entity = new SanitationMaintenanceActivitySchedulePeriod();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "SanitationMaintenanceActivityScheduleSearch.aspx";
            UrlPageList = "SanitationMaintenanceActivityScheduleList.aspx";

            ProgramID = AppConstant.Program.SanitationMaintenanceActivitySchedule;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                int year = DateTime.Now.Year;
                for (int i = 0; i < 15; i++)
                {
                    cboPeriodYear.Items.Add(new RadComboBoxItem((year - 10 + i).ToString(), (year - 10 + i).ToString()));
                }

                ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, BusinessObject.Reference.TransactionCode.AssetWorkOrder, false);
                ComboBox.PopulateWorkTradeItemList(cboSRWorkTradeItem, AppSession.Parameter.WorkTradeSanitation, true);
            }

            AjaxPanel.AjaxRequest += new RadAjaxControl.AjaxRequestDelegate(AjaxPanel_AjaxRequest);
        }

        /// <summary>
        /// Dijalankan dari javascript var ajxPanel=$find("<%= AjaxPanel.ClientID %>");ajxPanel.ajaxRequest('parameternya');
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AjaxPanel_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "cldSchedule")
            {
                //Not todo just callback for refresh calendar
            }
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new SanitationMaintenanceActivitySchedulePeriod();
            if (parameters.Length > 0)
            {
                String srWorkTradeItem = (String)parameters[0];
                String serviceUnitId = (String)parameters[1];
                String periodYear = (String)parameters[2];
               

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(srWorkTradeItem, serviceUnitId, periodYear);
            }
            else
            {
                entity.LoadByPrimaryKey(cboSRWorkTradeItem.SelectedValue, cboServiceUnitID.SelectedValue, cboPeriodYear.SelectedValue);
            }

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var scheduleHd = (SanitationMaintenanceActivitySchedulePeriod)entity;

            cboPeriodYear.SelectedValue = scheduleHd.PeriodYear;
            cboServiceUnitID.SelectedValue = scheduleHd.ServiceUnitID;
            cboSRWorkTradeItem.SelectedValue = scheduleHd.SRWorkTradeItem;

            SetRangeDateSchedule(Convert.ToInt16(scheduleHd.PeriodYear));

            //Reset
            SanitationMaintenanceActivitySchedules = null;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            var rec = new SanitationMaintenanceActivitySchedulePeriod();
            rec.PeriodYear = DateTime.Now.Year.ToString();
            OnPopulateEntryControl(rec);
        }

        protected override void OnMenuEditClick()
        {
            cboPeriodYear.Enabled = false;
            cboServiceUnitID.Enabled = false;
            cboSRWorkTradeItem.Enabled = false;
        }

        private void SetRangeDateSchedule(int year)
        {
            if (year == 0)
                return;

            cldSchedule.RangeMinDate = new DateTime(year, 1, 1);
            cldSchedule.RangeMaxDate = new DateTime(year, 12, 31);
            cldSchedule.FocusedDate = new DateTime(year, 1, 1);
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
            auditLogFilter.PrimaryKeyData = string.Format("SRWorkTradeItem='{0}' AND ServiceUnitID='{1}' AND PeriodYear='{2}'", cboSRWorkTradeItem.SelectedValue, cboServiceUnitID.SelectedValue, cboPeriodYear.SelectedValue);
            auditLogFilter.TableName = "SanitationMaintenanceActivitySchedulePeriod";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            btnChangeSchedule.Enabled = (newVal == AppEnum.DataMode.Edit && cboPeriodYear.Text != string.Empty && cboSRWorkTradeItem.SelectedValue != string.Empty);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var sch = new SanitationMaintenanceActivityScheduleCollection();
            sch.Query.Where(sch.Query.SRWorkTradeItem == cboSRWorkTradeItem.SelectedValue,
                            sch.Query.ServiceUnitID == cboServiceUnitID.SelectedValue,
                            sch.Query.PeriodYear == cboPeriodYear.SelectedValue, sch.Query.IsProcessed == true);
            sch.LoadAll();
            if (sch.Count > 0)
            {
                args.MessageText = "There is already schedule that is processed. Data can not be deleted.";
                args.IsCancel = true;
                return;
            }

            var entity = new SanitationMaintenanceActivitySchedulePeriod();
            entity.LoadByPrimaryKey(cboSRWorkTradeItem.SelectedValue, cboServiceUnitID.SelectedValue, cboPeriodYear.SelectedValue);
            entity.MarkAsDeleted();

            var collHd = new SanitationMaintenanceActivitySchedulePeriodDateCollection();
            collHd.Query.Where(collHd.Query.SRWorkTradeItem == cboSRWorkTradeItem.SelectedValue,
                               collHd.Query.ServiceUnitID == cboServiceUnitID.SelectedValue,
                               collHd.Query.PeriodYear == cboPeriodYear.SelectedValue);
            collHd.LoadAll();
            collHd.MarkAllAsDeleted();

            var collDt = new SanitationMaintenanceActivityScheduleCollection();
            collDt.Query.Where(
                collDt.Query.SRWorkTradeItem == cboSRWorkTradeItem.SelectedValue,
                collDt.Query.ServiceUnitID == cboServiceUnitID.SelectedValue,
                collDt.Query.PeriodYear == cboPeriodYear.SelectedValue
                );
            collDt.LoadAll();
            collDt.MarkAllAsDeleted();

            using (esTransactionScope trans = new esTransactionScope())
            {
                //Delete detil
                collDt.Save();

                //Delete Header
                collHd.Save();
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var collDetail = new SanitationMaintenanceActivityScheduleCollection();
            var entity = new SanitationMaintenanceActivitySchedulePeriod();

            entity.AddNew();
            SetEntityValue(entity, collDetail);
            SaveEntity(entity, collDetail);
        }

        private void SaveEntity(SanitationMaintenanceActivitySchedulePeriod entity, SanitationMaintenanceActivityScheduleCollection collDetail)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                //Save Header
                entity.Save();

                //Save Detail
                collDetail.Save();

                var collHeader = new SanitationMaintenanceActivitySchedulePeriodDateCollection();
                collHeader.Query.Where(collHeader.Query.SRWorkTradeItem == entity.SRWorkTradeItem,
                                       collHeader.Query.ServiceUnitID == entity.ServiceUnitID,
                                       collHeader.Query.PeriodYear == entity.PeriodYear);
                collHeader.LoadAll();
                collHeader.MarkAllAsDeleted();

                var headers =
                    collDetail.Select(
                        item => new { a = item.SRWorkTradeItem, u = item.ServiceUnitID, y = item.PeriodYear, m = item.PeriodDate });

                foreach (var group in (from g in headers
                                       group g by new
                                       {
                                           g.a,
                                           g.u,
                                           g.y,
                                           g.m
                                       }
                                           into grp
                                       orderby grp.Key.m
                                       select new
                                       {
                                           SRWorkTradeItem = grp.Key.a,
                                           ServiceUnitId = grp.Key.u,
                                           PeriodYear = grp.Key.y,
                                           PeriodDate = grp.Key.m
                                       }))
                {
                    var hd = collHeader.AddNew();
                    hd.SRWorkTradeItem = group.SRWorkTradeItem;
                    hd.ServiceUnitID = group.ServiceUnitId;
                    hd.PeriodYear = group.PeriodYear;
                    hd.PeriodDate = group.PeriodDate;
                    hd.LastUpdateDateTime = DateTime.Now;
                    hd.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }
                collHeader.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new SanitationMaintenanceActivitySchedulePeriod();
            if (entity.LoadByPrimaryKey(cboSRWorkTradeItem.SelectedValue, cboServiceUnitID.SelectedValue, cboPeriodYear.SelectedValue))
            {
                var collDetail = new SanitationMaintenanceActivityScheduleCollection();
                SetEntityValue(entity, collDetail);
                SaveEntity(entity, collDetail);
            }
        }

        #endregion

        #region Calendar

        protected void CustomizeDay(object sender, Telerik.Web.UI.Calendar.DayRenderEventArgs e)
        {
            //Jika text kosong
            if (e.Cell.Text == "&#160;") return;

            TableCell currentCell = e.Cell;
            DateTime currentDate = e.Day.Date;
            DataRow drw = SanitationMaintenanceActivitySchedules.Rows.Find(currentDate);
            if (drw != null)
            {
                currentCell.BackColor = Convert.ToBoolean(drw["IsProcessed"]) ? Color.DodgerBlue : Color.ForestGreen;

                var toolTip = Convert.ToBoolean(drw["IsProcessed"]) ? "Processed" : "Outstanding";

                currentCell.ToolTip = toolTip;
            }
        }

        private DataTable SanitationMaintenanceActivitySchedules
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["dtbSanitationMaintenanceActivitySchedule"];
                    if (obj != null)
                        return ((DataTable)(obj));
                }

                var query = new SanitationMaintenanceActivityScheduleQuery();

                query.Where
                    (
                        query.SRWorkTradeItem == cboSRWorkTradeItem.SelectedValue,
                        query.ServiceUnitID == cboServiceUnitID.SelectedValue,
                        query.PeriodYear == cboPeriodYear.SelectedValue,
                        query.IsVoid == false
                    );

                DataTable dtb = query.LoadDataTable();
                dtb.PrimaryKey = new DataColumn[]
                    {
                        dtb.Columns["ScheduleDate"]
                    };

                Session["dtbSanitationMaintenanceActivitySchedule"] = dtb;
                return dtb;
            }
            set { Session["dtbSanitationMaintenanceActivitySchedule"] = value; }
        }

        #endregion

        protected void cboPeriodYear_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRangeDateSchedule(Convert.ToInt16(cboPeriodYear.SelectedValue));
        }

        protected void cvSRWorkTradeItem_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (DataModeCurrent != AppEnum.DataMode.New || DataModeCurrent == AppEnum.DataMode.Read)
                return;

            //Check exist
            var rec = new SanitationMaintenanceActivitySchedulePeriod();
            if (rec.LoadByPrimaryKey(cboSRWorkTradeItem.SelectedValue, cboServiceUnitID.SelectedValue, cboPeriodYear.SelectedValue))
            {
                args.IsValid = false;
                btnChangeSchedule.Enabled = false;
            }
            else
            {
                args.IsValid = true;
                btnChangeSchedule.Enabled = true;
            }
        }

        protected void cboSRWorkTradeItem_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cvSRWorkTradeItem.Validate();
        }
    }
}