using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.AssetManagement.Master
{
    public partial class HolidayScheduleDetail : BasePageDetail
    {
        private void SetEntityValue(HolidayScheduleCollection collDetail)
        {
            collDetail.Query.Where(collDetail.Query.PeriodYear == cboPeriodYear.SelectedValue);
            collDetail.LoadAll();
            collDetail.MarkAllAsDeleted();
            collDetail.Save();

            //DataTable dtbEdited = HolidaySchedules;

            string year = cboPeriodYear.SelectedValue;

            //New and Updated
            //foreach (DataRow row in dtbEdited.Rows)
            //{
            //    DateTime date = (row.RowState == DataRowState.Added) ? Convert.ToDateTime(row["HolidayDate"]) : Convert.ToDateTime(row["HolidayDate", DataRowVersion.Original]);
            //    HolidaySchedule item = collDetail.FirstOrDefault(itemSearch => itemSearch.PeriodYear == year && itemSearch.HolidayDate == date);

            //    if (row.RowState == DataRowState.Deleted)
            //    {
            //        if (item != null) item.MarkAsDeleted();
            //        continue;
            //    }

            //    if (item == null)
            //    {
            //        item = collDetail.AddNew();
            //        item.PeriodYear = cboPeriodYear.SelectedValue;
            //        item.HolidayDate = Convert.ToDateTime(row["HolidayDate"]);
            //    }

            //    //Last Update Status
            //    if (item.es.IsAdded || item.es.IsModified)
            //    {
            //        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
            //        item.LastUpdateDateTime = DateTime.Now;
            //    }
            //}

            foreach (RadDate date in cldSchedule.SelectedDates)
            {
                HolidaySchedule item = collDetail.FirstOrDefault(itemSearch => itemSearch.PeriodYear == year && itemSearch.HolidayDate == date.Date);

                if (item == null)
                {
                    item = collDetail.AddNew();
                    item.PeriodYear = cboPeriodYear.SelectedValue;
                    item.HolidayDate = date.Date;
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
        }

        #region Override Method & Function

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "HolidayScheduleSearch.aspx";
            UrlPageList = "HolidayScheduleList.aspx";

            ProgramID = AppConstant.Program.AssetHolidaySchedule;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                //int year = DateTime.Now.Year;

                //var hs = new HolidaySchedule();
                //hs.Query.es.Top = 1;
                //hs.Query.es.Distinct = true;
                //hs.Query.Select(hs.Query.PeriodYear.Max());
                //hs.Query.OrderBy(hs.Query.PeriodYear.Descending);
                //if (hs.Query.Load()) year = string.IsNullOrWhiteSpace(hs.PeriodYear) ? year : hs.PeriodYear.ToInt() + 1;

                //for (int i = year; i < (year + 6); i++)
                //{
                //    cboPeriodYear.Items.Add(new RadComboBoxItem(i.ToString(), i.ToString()));
                //}

                //var hsc = new HolidayScheduleCollection();
                //hsc.Query.es.Distinct = true;
                //hsc.Query.Select(hsc.Query.PeriodYear);
                //hsc.Query.OrderBy(hsc.Query.PeriodYear.Ascending);
                //if (hsc.Query.Load())
                //{
                //    cboCopyYear.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

                //    foreach (var item in hsc)
                //    {
                //        cboCopyYear.Items.Add(new RadComboBoxItem(item.PeriodYear.ToString(), item.PeriodYear.ToString()));
                //    }
                //}
            }

            //AjaxPanel.AjaxRequest += new RadAjaxControl.AjaxRequestDelegate(AjaxPanel_AjaxRequest);
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
            var entity = new HolidaySchedule();
            if (parameters.Length > 0)
            {
                String periodYear = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                {
                    var q = new HolidayScheduleQuery();
                    q.Where(q.PeriodYear == periodYear);
                    q.es.Top = 1;
                    entity.Load(q);
                }
            }
            else
            {
                var q = new HolidayScheduleQuery();
                q.Where(q.PeriodYear == cboPeriodYear.SelectedValue);
                q.es.Top = 1;
                entity.Load(q);
            }

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var pmScheduleHd = (HolidaySchedule)entity;

            if (!string.IsNullOrWhiteSpace(pmScheduleHd.PeriodYear))
            {
                cboPeriodYear.Items.Add(new RadComboBoxItem(pmScheduleHd.PeriodYear, pmScheduleHd.PeriodYear));
                cboPeriodYear.SelectedValue = pmScheduleHd.PeriodYear;
            }

            SetRangeDateSchedule(string.IsNullOrWhiteSpace(pmScheduleHd.PeriodYear) ? DateTime.Now.Year.ToInt() : pmScheduleHd.PeriodYear.ToInt());

            //Reset
            //HolidaySchedules = null;

            if (string.IsNullOrWhiteSpace(pmScheduleHd.PeriodYear)) return;

            var hs = new HolidayScheduleCollection();
            hs.Query.Where(hs.Query.PeriodYear == cboPeriodYear.SelectedValue);
            hs.Query.OrderBy(hs.Query.HolidayDate.Ascending);
            if (hs.Query.Load())
            {
                foreach (var item in hs)
                {
                    cldSchedule.SelectedDates.Add(new RadDate(cboPeriodYear.SelectedValue.ToInt(), item.HolidayDate.Value.Month, item.HolidayDate.Value.Day));
                }
            }
        }

        //protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        //{
        //}

        protected override void OnMenuNewClick()
        {
            int year = DateTime.Now.Year;

            var hs = new HolidaySchedule();
            hs.Query.es.Top = 1;
            hs.Query.es.Distinct = true;
            hs.Query.Select(hs.Query.PeriodYear.Max());
            hs.Query.OrderBy(hs.Query.PeriodYear.Descending);
            if (hs.Query.Load()) year = string.IsNullOrWhiteSpace(hs.PeriodYear) ? year : hs.PeriodYear.ToInt() + 1;

            for (int i = year; i < (year + 6); i++)
            {
                cboPeriodYear.Items.Add(new RadComboBoxItem(i.ToString(), i.ToString()));
            }

            var hsc = new HolidayScheduleCollection();
            hsc.Query.es.Distinct = true;
            hsc.Query.Select(hsc.Query.PeriodYear);
            hsc.Query.OrderBy(hsc.Query.PeriodYear.Ascending);
            if (hsc.Query.Load())
            {
                cboCopyYear.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

                foreach (var item in hsc)
                {
                    cboCopyYear.Items.Add(new RadComboBoxItem(item.PeriodYear.ToString(), item.PeriodYear.ToString()));
                }
            }

            //var rec = new HolidaySchedule();
            //rec.PeriodYear = DateTime.Now.Year.ToString();
            OnPopulateEntryControl(new HolidaySchedule());
        }

        protected override void OnMenuEditClick()
        {
            cboPeriodYear.Enabled = false;
        }

        private void SetRangeDateSchedule(int year)
        {
            if (year == 0) return;

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

        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            btnChangeSchedule.Enabled = ((newVal == AppEnum.DataMode.Edit && cboPeriodYear.Text != string.Empty) || newVal == AppEnum.DataMode.New);
            trNew.Visible = newVal == AppEnum.DataMode.New && cboCopyYear.Items.Any();
            //cldSchedule.Enabled = newVal != AppEnum.DataMode.Read;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var collDt = new HolidayScheduleCollection();
            collDt.Query.Where(collDt.Query.PeriodYear == cboPeriodYear.SelectedValue);
            collDt.LoadAll();
            collDt.MarkAllAsDeleted();

            using (esTransactionScope trans = new esTransactionScope())
            {
                //Delete detil
                collDt.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var collDetail = new HolidayScheduleCollection();

            SetEntityValue(collDetail);
            SaveEntity(collDetail);
        }

        private void SaveEntity(HolidayScheduleCollection collDetail)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                //Save Detail
                collDetail.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var collDetail = new HolidayScheduleCollection();
            SetEntityValue(collDetail);
            SaveEntity(collDetail);
        }

        #endregion

        #region Calendar

        //protected void CustomizeDay(object sender, Telerik.Web.UI.Calendar.DayRenderEventArgs e)
        //{
        //    //Jika text kosong
        //    if (e.Cell.Text == "&#160;") return;

        //    TableCell currentCell = e.Cell;
        //    DateTime currentDate = e.Day.Date;
        //    DataRow drw = HolidaySchedules.Rows.Find(currentDate);
        //    if (drw != null)
        //    {
        //        currentCell.BackColor = Color.Red;

        //        var toolTip = "Holiday";

        //        currentCell.ToolTip = toolTip;
        //    }
        //}

        //private DataTable HolidaySchedules
        //{
        //    get
        //    {
        //        if (IsPostBack)
        //        {
        //            object obj = Session["dtbAssetHolidaySchedule"];
        //            if (obj != null) return ((DataTable)(obj));
        //        }

        //        var query = new HolidayScheduleQuery();

        //        query.Where(query.PeriodYear == cboPeriodYear.SelectedValue);

        //        DataTable dtb = query.LoadDataTable();
        //        dtb.PrimaryKey = new DataColumn[]
        //        {
        //            dtb.Columns["HolidayDate"]
        //        };

        //        Session["dtbAssetHolidaySchedule"] = dtb;
        //        return dtb;
        //    }
        //    set { Session["dtbAssetHolidaySchedule"] = value; }
        //}

        #endregion

        protected void cboPeriodYear_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRangeDateSchedule(cboPeriodYear.SelectedValue.ToInt());
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            trNew.Visible = DataModeCurrent == AppEnum.DataMode.New && cboCopyYear.Items.Any();
        }

        protected void btnCopyFrom_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cboPeriodYear.SelectedValue)) return;
            if (string.IsNullOrWhiteSpace(cboCopyYear.SelectedValue)) return;

            var hs = new HolidayScheduleCollection();
            hs.Query.Where(hs.Query.PeriodYear == cboCopyYear.SelectedValue);
            hs.Query.OrderBy(hs.Query.HolidayDate.Ascending);
            if (hs.Query.Load())
            {
                foreach (var item in hs)
                {
                    cldSchedule.SelectedDates.Add(new RadDate(cboPeriodYear.SelectedValue.ToInt(), item.HolidayDate.Value.Month, item.HolidayDate.Value.Day));
                }
            }
        }
    }
}