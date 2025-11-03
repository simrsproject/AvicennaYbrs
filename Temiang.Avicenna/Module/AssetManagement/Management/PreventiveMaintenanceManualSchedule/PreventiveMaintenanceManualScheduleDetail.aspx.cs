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

namespace Temiang.Avicenna.Module.AssetManagement
{
    public partial class PreventiveMaintenanceManualScheduleDetail : BasePageDetail
    {
        private void SetEntityValue(AssetPreventiveMaintenanceSchedulePeriod entity, AssetPreventiveMaintenanceScheduleCollection collDetail)
        {
            entity.PeriodYear = cboPeriodYear.SelectedValue;
            entity.AssetID = cboAssetID.SelectedValue;
            
            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            collDetail.Query.Where
                (
                    collDetail.Query.PeriodYear == cboPeriodYear.SelectedValue, 
                    collDetail.Query.AssetID == cboAssetID.SelectedValue
                );
            collDetail.LoadAll();

            DataTable dtbEdited = AssetPreventiveMaintenanceSchedules;
            string assetId = cboAssetID.SelectedValue;
            string year = cboPeriodYear.SelectedValue;

            //New and Updated
            foreach (DataRow row in dtbEdited.Rows)
            {
                DateTime date = (row.RowState == DataRowState.Added) ? Convert.ToDateTime(row["ScheduleDate"]) : Convert.ToDateTime(row["ScheduleDate", DataRowVersion.Original]);
                AssetPreventiveMaintenanceSchedule item = collDetail.FirstOrDefault(itemSearch => itemSearch.AssetID == assetId && itemSearch.PeriodYear == year && itemSearch.ScheduleDate == date);

                if (row.RowState == DataRowState.Deleted)
                {
                    if (item != null)
                        item.MarkAsDeleted();

                    continue;
                }

                if (item == null)
                {
                    item = collDetail.AddNew();
                    item.AssetID = cboAssetID.SelectedValue;
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
            var que = new AssetPreventiveMaintenanceSchedulePeriodQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.PeriodYear == cboPeriodYear.SelectedValue & que.AssetID > cboAssetID.SelectedValue);
                que.OrderBy(que.PeriodYear.Ascending);
            }
            else
            {
                que.Where(que.PeriodYear == cboPeriodYear.SelectedValue & que.AssetID < cboAssetID.SelectedValue);
                que.OrderBy(que.PeriodYear.Descending);
            }

            var entity = new AssetPreventiveMaintenanceSchedulePeriod();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "PreventiveMaintenanceManualScheduleSearch.aspx";
            UrlPageList = "PreventiveMaintenanceManualScheduleList.aspx";

            ProgramID = AppConstant.Program.AssetPreventiveMaintenanceManualSchedule;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                int year = DateTime.Now.Year;
                for (int i = 0; i < 15; i++)
                {
                    cboPeriodYear.Items.Add(new RadComboBoxItem((year - 10 + i).ToString(), (year - 10 + i).ToString()));
                }

                ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, BusinessObject.Reference.TransactionCode.AssetWorkOrder, false);
                ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, BusinessObject.Reference.TransactionCode.AssetWorkOrderRealization, true);

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
            var entity = new AssetPreventiveMaintenanceSchedulePeriod();
            if (parameters.Length > 0)
            {
                String periodYear = (String)parameters[1];
                String assetId = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(assetId, periodYear);
            }
            else
            {
                entity.LoadByPrimaryKey(cboAssetID.SelectedValue, cboPeriodYear.SelectedValue);
            }

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var pmScheduleHd = (AssetPreventiveMaintenanceSchedulePeriod)entity;

            cboPeriodYear.SelectedValue = pmScheduleHd.PeriodYear;

            if (!string.IsNullOrEmpty(pmScheduleHd.AssetID))
            {
                var assetQ = new AssetQuery("a");
                var funitQ = new ServiceUnitQuery("b");
                var tunitQ = new ServiceUnitQuery("c");
                assetQ.Select(assetQ.AssetID, assetQ.AssetName, assetQ.SerialNumber, funitQ.ServiceUnitName,
                             tunitQ.ServiceUnitName.As("MaintenanceServiceUnitName"));
                assetQ.InnerJoin(funitQ).On(assetQ.ServiceUnitID == funitQ.ServiceUnitID);
                assetQ.InnerJoin(tunitQ).On(assetQ.MaintenanceServiceUnitID == tunitQ.ServiceUnitID);
                assetQ.Where(assetQ.AssetID == pmScheduleHd.AssetID);

                DataTable dtb = assetQ.LoadDataTable();

                cboAssetID.DataSource = dtb;
                cboAssetID.DataBind();
                cboAssetID.SelectedValue = pmScheduleHd.AssetID;

                GetAssetInfo(pmScheduleHd.AssetID, true);
            }
            else
            {
                cboAssetID.Items.Clear();
                cboAssetID.SelectedValue = string.Empty;
                cboAssetID.Text = string.Empty;
                txtBrandName.Text = string.Empty;
                txtSerialNo.Text = string.Empty;
            }
            
            SetRangeDateSchedule(Convert.ToInt16(pmScheduleHd.PeriodYear));

            //Reset
            AssetPreventiveMaintenanceSchedules = null;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            var rec = new AssetPreventiveMaintenanceSchedulePeriod();
            rec.PeriodYear = DateTime.Now.Year.ToString();
            OnPopulateEntryControl(rec);
        }

        protected override void OnMenuEditClick()
        {
            cboAssetID.Enabled = false;
            cboPeriodYear.Enabled = false;
            cboToServiceUnitID.Enabled = false;
            cboFromServiceUnitID.Enabled = false;
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
            auditLogFilter.PrimaryKeyData = string.Format("AssetID='{0}' AND PeriodYear='{1}'", cboPeriodYear.SelectedValue, cboAssetID.SelectedValue);
            auditLogFilter.TableName = "AssetPreventiveMaintenanceSchedulePeriod";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            btnChangeSchedule.Enabled = (newVal == AppEnum.DataMode.Edit && cboPeriodYear.Text != string.Empty && cboAssetID.SelectedValue != string.Empty);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var sch = new AssetPreventiveMaintenanceScheduleCollection();
            sch.Query.Where(sch.Query.AssetID == cboAssetID.SelectedValue,
                            sch.Query.PeriodYear == cboPeriodYear.SelectedValue, sch.Query.IsProcessed == true);
            sch.LoadAll();
            if (sch.Count > 0)
            {
                args.MessageText = "There is already schedule that is processed. Data can not be deleted.";
                args.IsCancel = true;
                return;
            }

            var entity = new AssetPreventiveMaintenanceSchedulePeriod();
            entity.LoadByPrimaryKey(cboAssetID.SelectedValue, cboPeriodYear.SelectedValue);
            entity.MarkAsDeleted();

            var collHd = new AssetPreventiveMaintenanceSchedulePeriodDateCollection();
            collHd.Query.Where(collHd.Query.AssetID == cboAssetID.SelectedValue,
                               collHd.Query.PeriodYear == cboPeriodYear.SelectedValue);
            collHd.LoadAll();
            collHd.MarkAllAsDeleted();

            var collDt = new AssetPreventiveMaintenanceScheduleCollection();
            collDt.Query.Where(
                collDt.Query.AssetID == cboAssetID.SelectedValue,
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
            var collDetail = new AssetPreventiveMaintenanceScheduleCollection();
            var entity = new AssetPreventiveMaintenanceSchedulePeriod();

            entity.AddNew();
            SetEntityValue(entity, collDetail);
            SaveEntity(entity, collDetail);
        }

        private void SaveEntity(AssetPreventiveMaintenanceSchedulePeriod entity, AssetPreventiveMaintenanceScheduleCollection collDetail)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                //Save Header
                entity.Save();

                //Save Detail
                collDetail.Save();

                var collHeader = new AssetPreventiveMaintenanceSchedulePeriodDateCollection();
                collHeader.Query.Where(collHeader.Query.AssetID == entity.AssetID,
                                       collHeader.Query.PeriodYear == entity.PeriodYear);
                collHeader.LoadAll();
                collHeader.MarkAllAsDeleted();

                var headers =
                    collDetail.Select(
                        item => new {a = item.AssetID, y = item.PeriodYear, m = item.PeriodDate});

                foreach (var group in (from g in headers
                                       group g by new
                                       {
                                           g.a,
                                           g.y,
                                           g.m
                                       }
                                           into grp
                                           orderby grp.Key.m
                                           select new
                                           {
                                               AssetId = grp.Key.a,
                                               PeriodYear = grp.Key.y,
                                               PeriodDate = grp.Key.m
                                           }))
                {
                    var hd = collHeader.AddNew();
                    hd.AssetID = group.AssetId;
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
            var entity = new AssetPreventiveMaintenanceSchedulePeriod();
            if (entity.LoadByPrimaryKey(cboAssetID.SelectedValue, cboPeriodYear.SelectedValue))
            {
                var collDetail = new AssetPreventiveMaintenanceScheduleCollection();
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
            DataRow drw = AssetPreventiveMaintenanceSchedules.Rows.Find(currentDate);
            if (drw != null)
            {
                currentCell.BackColor = Convert.ToBoolean(drw["IsProcessed"]) ? Color.DodgerBlue : Color.ForestGreen;
                
                var toolTip = Convert.ToBoolean(drw["IsProcessed"]) ? "Processed" : "Outstanding";

                currentCell.ToolTip = toolTip;
            }
        }

        private DataTable AssetPreventiveMaintenanceSchedules
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["dtbAssetPreventiveMaintenanceSchedule"];
                    if (obj != null)
                        return ((DataTable)(obj));
                }

                var query = new AssetPreventiveMaintenanceScheduleQuery();

                query.Where
                    (
                        query.AssetID == cboAssetID.SelectedValue,
                        query.PeriodYear == cboPeriodYear.SelectedValue,
                        query.IsVoid == false
                    );

                DataTable dtb = query.LoadDataTable();
                dtb.PrimaryKey = new DataColumn[] 
                    { 
                        dtb.Columns["ScheduleDate"] 
                    };

                Session["dtbAssetPreventiveMaintenanceSchedule"] = dtb;
                return dtb;
            }
            set { Session["dtbAssetPreventiveMaintenanceSchedule"] = value; }
        }

        #endregion

        protected void cboPeriodYear_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRangeDateSchedule(Convert.ToInt16(cboPeriodYear.SelectedValue));
        }

        protected void cvAssetID_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (DataModeCurrent != AppEnum.DataMode.New || DataModeCurrent == AppEnum.DataMode.Read)
                return;

            //Check exist
            var rec = new AssetPreventiveMaintenanceSchedulePeriod();
            if (rec.LoadByPrimaryKey(cboAssetID.SelectedValue, cboPeriodYear.SelectedValue))
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

        protected void cboAssetID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["AssetName"] + " (" + ((DataRowView)e.Item.DataItem)["AssetID"] + ")";
            e.Item.Value = ((DataRowView)e.Item.DataItem)["AssetID"].ToString();
        }

        protected void cboAssetID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AssetQuery("a");
            var funitQ = new ServiceUnitQuery("b");
            var tunitQ = new ServiceUnitQuery("c");
            var usrQ = new AppUserServiceUnitQuery("d");

            query.es.Top = 20;

            query.Select(query.AssetID, query.AssetName, query.SerialNumber, funitQ.ServiceUnitName,
                         tunitQ.ServiceUnitName.As("MaintenanceServiceUnitName"));
            query.InnerJoin(funitQ).On(query.ServiceUnitID == funitQ.ServiceUnitID);
            query.InnerJoin(tunitQ).On(query.MaintenanceServiceUnitID == tunitQ.ServiceUnitID);
            query.InnerJoin(usrQ).On(query.MaintenanceServiceUnitID == usrQ.ServiceUnitID &&
                                    usrQ.UserID == AppSession.UserLogin.UserID);

            query.Where(query.AssetName.Like(searchTextContain), query.MaintenanceInterval == 0);
            if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
                query.Where(query.MaintenanceServiceUnitID == cboToServiceUnitID.SelectedValue);
            if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                query.Where(query.ServiceUnitID == cboFromServiceUnitID.SelectedValue);

            query.OrderBy(query.AssetName.Ascending);
            DataTable dtb = query.LoadDataTable();
            cboAssetID.DataSource = dtb;
            cboAssetID.DataBind();
        }

        protected void cboAssetID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            GetAssetInfo(e.Value, false);
            cvAssetID.Validate();
        }

        private void GetAssetInfo(string assetId, bool isOpen)
        {
            var a = new BusinessObject.Asset();
            if (a.LoadByPrimaryKey(assetId))
            {
                txtBrandName.Text = a.BrandName;
                txtSerialNo.Text = a.SerialNumber;
                if (isOpen)
                {
                    cboFromServiceUnitID.SelectedValue = a.ServiceUnitID;
                    cboToServiceUnitID.SelectedValue = a.MaintenanceServiceUnitID;
                }
            }
            else
            {
                txtBrandName.Text = string.Empty;
                txtSerialNo.Text = string.Empty;
            }
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboAssetID.Items.Clear();
            cboAssetID.SelectedValue = string.Empty;
            cboAssetID.Text = string.Empty;
        }
    }
}
