using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.AssetManagement
{
    public partial class PreventiveMaintenanceScheduleList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;
        protected void Page_Init(object sender, EventArgs e)
        {
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.AssetPreventiveMaintenanceSchedule;

            if (!IsPostBack)
            {
                var months = DateTimeFormatInfo.InvariantInfo.MonthNames.Where(m => !string.IsNullOrEmpty(m))
                                                                        .Select((m, i) => new
                                                                        {
                                                                            Month = m,
                                                                            Value = (i + 2) - 1
                                                                        });

                foreach (var m in months)
                {
                    cboPeriodMonth.Items.Add(new RadComboBoxItem(m.Month, m.Value.ToString()));
                }

                cboPeriodMonth.SelectedValue = DateTime.Now.Date.Month.ToString();
                txtPeriodYear.Text = DateTime.Now.Date.Year.ToString();

                ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, TransactionCode.AssetWorkOrderRealization, true);
                if (!string.IsNullOrEmpty(Request.QueryString["su"]))
                    cboToServiceUnitID.SelectedValue = Request.QueryString["su"];
                else
                    cboToServiceUnitID.Text = string.Empty;

                var query = new ServiceUnitQuery();
                query.Select(query.ServiceUnitID, query.ServiceUnitName);
                query.OrderBy(query.ServiceUnitName.Ascending);
                query.Where(query.IsActive == true);

                DataTable dtb = query.LoadDataTable();

                cboFromServiceUnitID.Items.Clear();
                cboFromServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (DataRow item in dtb.Rows)
                {
                    cboFromServiceUnitID.Items.Add(new RadComboBoxItem(item["ServiceUnitName"].ToString(), item["ServiceUnitID"].ToString()));
                }
            }
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
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
            query.Where(query.Or(query.ItemID.Like(searchTextContain), 
                query.AssetName.Like(searchTextContain),
                                 query.SerialNumber.Like(searchTextContain)));
            if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
                query.Where(query.MaintenanceServiceUnitID == cboToServiceUnitID.SelectedValue);
            if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                query.Where(query.ServiceUnitID == cboFromServiceUnitID.SelectedValue);

            query.OrderBy(query.AssetName.Ascending);
            DataTable dtb = query.LoadDataTable();
            cboAssetID.DataSource = dtb;
            cboAssetID.DataBind();
        }

        private DataTable PreventiveMaintenanceSchedules()
        {
            var isEmptyFilter = string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboAssetID.SelectedValue);
            if (!ValidateSearch(isEmptyFilter, "Preventive Maintenance")) return null;


            var query = new AssetPreventiveMaintenanceScheduleQuery("a");
            var qasset = new AssetQuery("b");
            var qunit = new ServiceUnitQuery("c");
            var qroom = new ServiceRoomQuery("d");
            var qtunit = new ServiceUnitQuery("e");
            var usr = new AppUserServiceUnitQuery("f");

            query.Select
                (
                query.ScheduleDate,
                query.AssetID,
                qasset.AssetName,
                qasset.BrandName,
                qasset.SerialNumber,
                @"<CASE WHEN ISNULL(d.RoomName, '') = '' THEN c.ServiceUnitName ELSE c.ServiceUnitName + ' - ' + d.RoomName END AS AssetLocationName>",
                qasset.MaintenanceServiceUnitID,
                qasset.ServiceUnitID,
                qtunit.ServiceUnitName.As("MaintenanceServiceUnitName"),
                query.IsProcessed,
                query.IsVoid
                );
            query.InnerJoin(qasset).On(query.AssetID == qasset.AssetID);
            query.InnerJoin(qunit).On(qasset.ServiceUnitID == qunit.ServiceUnitID);
            query.LeftJoin(qroom).On(qasset.AssetLocationID == qroom.RoomID);
            query.InnerJoin(qtunit).On(qasset.MaintenanceServiceUnitID == qtunit.ServiceUnitID);
            query.InnerJoin(usr).On(qasset.MaintenanceServiceUnitID == usr.ServiceUnitID &&
                                    usr.UserID == AppSession.UserLogin.UserID);

            query.Where(string.Format("<MONTH(a.ScheduleDate) = {0}>", cboPeriodMonth.SelectedValue));
            query.Where(string.Format("<YEAR(a.ScheduleDate) = {0}>", txtPeriodYear.Text));
            query.Where(qasset.MaintenanceServiceUnitID == cboToServiceUnitID.SelectedValue);

            if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                query.Where(qasset.ServiceUnitID == cboFromServiceUnitID.SelectedValue);
            if (!string.IsNullOrEmpty(cboAssetID.SelectedValue))
                query.Where(query.AssetID == cboAssetID.SelectedValue);

            query.OrderBy(query.ScheduleDate.Ascending, qasset.ServiceUnitID.Ascending, query.AssetID.Ascending);

            query.es.Top = AppSession.Parameter.MaxResultRecord;

            DataTable tbl = query.LoadDataTable();
            return tbl;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = PreventiveMaintenanceSchedules();
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }            
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument == "generate")
            {
                pnlInfo.Visible = false;
                
                if (string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = "Maintenace Unit required.";
                    grdList.Rebind();
                    return;
                }
                
                var msg1 = string.Empty;
                var msg2 = string.Empty;

                var assetColl = new AssetCollection();
                var assetQ = new AssetQuery("a");
                var usrQ = new AppUserServiceUnitQuery("b");
                assetQ.InnerJoin(usrQ).On(assetQ.MaintenanceServiceUnitID == usrQ.ServiceUnitID &&
                                          usrQ.UserID == AppSession.UserLogin.UserID);
                assetQ.Where(assetQ.SRAssetsStatus == AppSession.Parameter.AssetsStatusActive, assetQ.MaintenanceInterval > 0);
                if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
                    assetQ.Where(assetQ.MaintenanceServiceUnitID == cboToServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                    assetQ.Where(assetQ.ServiceUnitID == cboFromServiceUnitID.SelectedValue);
                assetQ.OrderBy(assetQ.AssetID.Ascending);
                assetColl.Load(assetQ);

                var periodColl = new AssetPreventiveMaintenanceSchedulePeriodCollection();
                var periodDateColl = new AssetPreventiveMaintenanceSchedulePeriodDateCollection();
                var pmsColl = new AssetPreventiveMaintenanceScheduleCollection();

                DateTime fromDate = Convert.ToDateTime(cboPeriodMonth.SelectedValue + "/1/" + txtPeriodYear.Text);
                DateTime toDate =
                    Convert.ToDateTime(cboPeriodMonth.SelectedValue + "/1/" + txtPeriodYear.Text).Date.AddMonths(1).
                        AddDays(-1);

                foreach (var asset in assetColl)
                {
                    var pmsNextColl = new AssetPreventiveMaintenanceSchedulePeriodDateCollection();
                    pmsNextColl.Query.Where(pmsNextColl.Query.AssetID == asset.AssetID,
                                            pmsNextColl.Query.PeriodDate.Date() > fromDate.Date);
                    pmsNextColl.LoadAll();
                    if (pmsNextColl.Count == 0)
                    {
                        var period = new AssetPreventiveMaintenanceSchedulePeriod();
                        if (!period.LoadByPrimaryKey(asset.AssetID, txtPeriodYear.Text))
                        {
                            var p = periodColl.AddNew();
                            p.AssetID = asset.AssetID;
                            p.PeriodYear = txtPeriodYear.Text;
                            p.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            p.LastUpdateDateTime = DateTime.Now;
                        }

                        var periodDate = new AssetPreventiveMaintenanceSchedulePeriodDate();
                        if (periodDate.LoadByPrimaryKey(asset.AssetID, txtPeriodYear.Text, fromDate.Date))
                        {
                            if (msg2 == string.Empty)
                            {
                                msg1 = "Schedule for this period already exist for : " + asset.AssetName + " (SN: " +
                                       asset.SerialNumber + ")";
                            }
                        }
                        else
                        {
                            var p = periodDateColl.AddNew();
                            p.AssetID = asset.AssetID;
                            p.PeriodYear = txtPeriodYear.Text;
                            p.PeriodDate = fromDate.Date;
                            p.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            p.LastUpdateDateTime = DateTime.Now;

                            var interval = asset.MaintenanceInterval ?? 0;
                            var intervalIn = asset.MaintenanceIntervalIn;
                           
                            if (intervalIn == "d")
                            {
                                DateTime lastMaintenance;
                                var scheduleQ = new AssetPreventiveMaintenanceScheduleQuery();
                                scheduleQ.Where(scheduleQ.AssetID == asset.AssetID, scheduleQ.ScheduleDate < fromDate);
                                scheduleQ.es.Top = 1;
                                scheduleQ.OrderBy(scheduleQ.ScheduleDate.Descending);
                                if (scheduleQ.LoadDataTable().Rows.Count > 0)
                                {
                                    var schedule = new AssetPreventiveMaintenanceSchedule();
                                    schedule.Load(scheduleQ);
                                    lastMaintenance = schedule.ScheduleDate ?? fromDate.Date.AddDays(interval * (-1));
                                }
                                else
                                    lastMaintenance = asset.LastMaintenanceDate ?? fromDate.Date.AddDays(interval * (-1));

                                var startDate = lastMaintenance;
                                var endDate = toDate;

                                for (DateTime lDate = startDate; lDate <= endDate; lDate = lDate.AddDays(interval))
                                {
                                    if (lDate >= fromDate.Date)
                                    {
                                        var holidayQ = new HolidayScheduleQuery();
                                        holidayQ.Where(holidayQ.HolidayDate == lDate);
                                        if (holidayQ.LoadDataTable().Rows.Count > 0)
                                        {
                                            var sDate = lDate.AddDays(1);
                                            var eDate = lDate.AddDays(7);
                                            for (DateTime s = sDate; s <= eDate; s = s.AddDays(1))
                                            {
                                                holidayQ = new HolidayScheduleQuery();
                                                holidayQ.Select(holidayQ.HolidayDate);
                                                holidayQ.Where(holidayQ.HolidayDate == s);
                                                if (holidayQ.LoadDataTable().Rows.Count == 0)
                                                {
                                                    lDate = s;
                                                    break;
                                                }
                                            }
                                        }
                                        
                                        var pms = pmsColl.AddNew();
                                        pms.AssetID = asset.AssetID;
                                        pms.ScheduleDate = lDate;
                                        pms.PeriodYear = txtPeriodYear.Text;
                                        pms.PeriodDate = fromDate.Date;
                                        pms.LastUpdateDateTime = DateTime.Now;
                                        pms.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    }
                                }
                            }
                            else if (intervalIn == "m")
                            {
                                DateTime lastMaintenance;
                                var scheduleQ = new AssetPreventiveMaintenanceScheduleQuery();
                                scheduleQ.Where(scheduleQ.AssetID == asset.AssetID, scheduleQ.ScheduleDate < fromDate);
                                scheduleQ.es.Top = 1;
                                scheduleQ.OrderBy(scheduleQ.ScheduleDate.Descending);
                                if (scheduleQ.LoadDataTable().Rows.Count > 0)
                                {
                                    var schedule = new AssetPreventiveMaintenanceSchedule();
                                    schedule.Load(scheduleQ);
                                    lastMaintenance = schedule.ScheduleDate ?? fromDate.Date.AddMonths(interval * (-1));
                                }
                                else
                                    lastMaintenance = asset.LastMaintenanceDate ?? fromDate.Date.AddMonths(interval * (-1));

                                var startDate = lastMaintenance;
                                var endDate = toDate;

                                for (DateTime lDate = startDate; lDate <= endDate; lDate = lDate.AddMonths(interval))
                                {
                                    if (lDate >= fromDate.Date)
                                    {
                                        var holidayQ = new HolidayScheduleQuery();
                                        holidayQ.Where(holidayQ.HolidayDate == lDate);
                                        if (holidayQ.LoadDataTable().Rows.Count > 0)
                                        {
                                            var sDate = lDate.AddDays(1);
                                            var eDate = lDate.AddDays(7);
                                            for (DateTime s = sDate; s <= eDate; s = s.AddDays(1))
                                            {
                                                holidayQ = new HolidayScheduleQuery();
                                                holidayQ.Select(holidayQ.HolidayDate);
                                                holidayQ.Where(holidayQ.HolidayDate == s);
                                                if (holidayQ.LoadDataTable().Rows.Count == 0)
                                                {
                                                    lDate = s;
                                                    break;
                                                }
                                            }
                                        }

                                        var pms = pmsColl.AddNew();
                                        pms.AssetID = asset.AssetID;
                                        pms.ScheduleDate = lDate;
                                        pms.PeriodYear = txtPeriodYear.Text;
                                        pms.PeriodDate = fromDate.Date;
                                        pms.LastUpdateDateTime = DateTime.Now;
                                        pms.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    }
                                }
                            }
                            else if (intervalIn == "y")
                            {
                                DateTime lastMaintenance;
                                var scheduleQ = new AssetPreventiveMaintenanceScheduleQuery();
                                scheduleQ.Where(scheduleQ.AssetID == asset.AssetID, scheduleQ.ScheduleDate < fromDate);
                                scheduleQ.es.Top = 1;
                                scheduleQ.OrderBy(scheduleQ.ScheduleDate.Descending);
                                if (scheduleQ.LoadDataTable().Rows.Count > 0)
                                {
                                    var schedule = new AssetPreventiveMaintenanceSchedule();
                                    schedule.Load(scheduleQ);
                                    lastMaintenance = schedule.ScheduleDate ?? fromDate.Date.AddYears(interval * (-1));
                                }
                                else
                                    lastMaintenance = asset.LastMaintenanceDate ?? fromDate.Date.AddYears(interval * (-1));

                                var startDate = lastMaintenance;
                                var endDate = toDate;

                                for (DateTime lDate = startDate; lDate <= endDate; lDate = lDate.AddYears(interval))
                                {
                                    if (lDate >= fromDate.Date)
                                    {
                                        var holidayQ = new HolidayScheduleQuery();
                                        holidayQ.Where(holidayQ.HolidayDate == lDate);
                                        if (holidayQ.LoadDataTable().Rows.Count > 0)
                                        {
                                            var sDate = lDate.AddDays(1);
                                            var eDate = lDate.AddDays(7);
                                            for (DateTime s = sDate; s <= eDate; s = s.AddDays(1))
                                            {
                                                holidayQ = new HolidayScheduleQuery();
                                                holidayQ.Select(holidayQ.HolidayDate);
                                                holidayQ.Where(holidayQ.HolidayDate == s);
                                                if (holidayQ.LoadDataTable().Rows.Count == 0)
                                                {
                                                    lDate = s;
                                                    break;
                                                }
                                            }
                                        }

                                        var pms = pmsColl.AddNew();
                                        pms.AssetID = asset.AssetID;
                                        pms.ScheduleDate = lDate;
                                        pms.PeriodYear = txtPeriodYear.Text;
                                        pms.PeriodDate = fromDate.Date;
                                        pms.LastUpdateDateTime = DateTime.Now;
                                        pms.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (msg1 == string.Empty)
                            msg1 = "Schedule for next period already exist for : " + asset.AssetName + " (SN: " +
                                   asset.SerialNumber + ")";
                        else
                            msg1 += ", " + asset.AssetName + " (SN: " + asset.SerialNumber + ")";
                    }
                }

                using (var trans = new esTransactionScope())
                {
                    periodColl.Save();
                    periodDateColl.Save();
                    pmsColl.Save();

                    trans.Complete();
                }

                pnlInfo.Visible = true;
                if (msg1.Length == 0 && msg2.Length == 0)
                    lblInfo.Text = "Generate schedule done.";
                else
                    lblInfo.Text = (msg1.Length == 0 ? "" : msg1 + ". ") + (msg2.Length == 0 ? "" : msg2 + ". ") + "Generate schedule for other asset done.";

                grdList.Rebind();
            }
            else if (eventArgument == "delete")
            {
                pnlInfo.Visible = false;

                var pmsColl = new AssetPreventiveMaintenanceScheduleCollection();
                var pmsQ = new AssetPreventiveMaintenanceScheduleQuery("a");
                var assetQ = new AssetQuery("b");
                var usrQ = new AppUserServiceUnitQuery("c");
                pmsQ.InnerJoin(assetQ).On(pmsQ.AssetID == assetQ.AssetID);
                pmsQ.InnerJoin(usrQ).On(assetQ.MaintenanceServiceUnitID == usrQ.ServiceUnitID &&
                                          usrQ.UserID == AppSession.UserLogin.UserID);
                pmsQ.Where(string.Format("<MONTH(a.ScheduleDate) = {0}>", cboPeriodMonth.SelectedValue));
                pmsQ.Where(string.Format("<YEAR(a.ScheduleDate) = {0}>", txtPeriodYear.Text));
                pmsQ.Where(pmsQ.IsProcessed == true);
                if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
                    pmsQ.Where(assetQ.MaintenanceServiceUnitID == cboToServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                    pmsQ.Where(assetQ.ServiceUnitID == cboFromServiceUnitID.SelectedValue);
                pmsColl.Load(pmsQ);

                if (pmsColl.Count > 0)
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = "Schedule has been processed. Deletion can not be done.";
                    grdList.Rebind();
                }
                else
                {
                    var pmspColl = new AssetPreventiveMaintenanceSchedulePeriodDateCollection();
                    var pmspQ = new AssetPreventiveMaintenanceSchedulePeriodDateQuery("x");
                    pmsQ = new AssetPreventiveMaintenanceScheduleQuery("a");
                    assetQ = new AssetQuery("b");
                    usrQ = new AppUserServiceUnitQuery("c");
                    pmspQ.InnerJoin(pmsQ).On(pmspQ.AssetID == pmsQ.AssetID && pmspQ.PeriodDate == pmsQ.PeriodDate);
                    pmsQ.InnerJoin(assetQ).On(pmsQ.AssetID == assetQ.AssetID);
                    pmsQ.InnerJoin(usrQ).On(assetQ.MaintenanceServiceUnitID == usrQ.ServiceUnitID &&
                                              usrQ.UserID == AppSession.UserLogin.UserID);
                    pmsQ.Where(string.Format("<MONTH(a.ScheduleDate) = {0}>", cboPeriodMonth.SelectedValue));
                    pmsQ.Where(string.Format("<YEAR(a.ScheduleDate) = {0}>", txtPeriodYear.Text));
                    if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
                        pmsQ.Where(assetQ.MaintenanceServiceUnitID == cboToServiceUnitID.SelectedValue);
                    if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                        pmsQ.Where(assetQ.ServiceUnitID == cboFromServiceUnitID.SelectedValue);
                    pmspQ.Select(pmspQ);
                    pmspQ.es.Distinct = true;
                    pmspColl.Load(pmspQ);
                    pmspColl.MarkAllAsDeleted();

                    pmsColl = new AssetPreventiveMaintenanceScheduleCollection();
                    pmsQ = new AssetPreventiveMaintenanceScheduleQuery("a");
                    assetQ = new AssetQuery("b");
                    usrQ = new AppUserServiceUnitQuery("c");
                    pmsQ.InnerJoin(assetQ).On(pmsQ.AssetID == assetQ.AssetID);
                    pmsQ.InnerJoin(usrQ).On(assetQ.MaintenanceServiceUnitID == usrQ.ServiceUnitID &&
                                              usrQ.UserID == AppSession.UserLogin.UserID);
                    pmsQ.Where(string.Format("<MONTH(a.ScheduleDate) = {0}>", cboPeriodMonth.SelectedValue));
                    pmsQ.Where(string.Format("<YEAR(a.ScheduleDate) = {0}>", txtPeriodYear.Text));
                    if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
                        pmsQ.Where(assetQ.MaintenanceServiceUnitID == cboToServiceUnitID.SelectedValue);
                    if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                        pmsQ.Where(assetQ.ServiceUnitID == cboFromServiceUnitID.SelectedValue);
                    pmsColl.Load(pmsQ);
                    pmsColl.MarkAllAsDeleted();

                    using (var trans = new esTransactionScope())
                    {
                        pmspColl.Save();
                        pmsColl.Save();

                        trans.Complete();
                    }

                    pnlInfo.Visible = true;
                    lblInfo.Text = "Delete schedule done.";
                    grdList.Rebind();
                }
            }
            else if (eventArgument == "print")
            {
                pnlInfo.Visible = false;

                var jobParameters = new PrintJobParameterCollection();

                var jobParameterMonth = jobParameters.AddNew();
                jobParameterMonth.Name = "p_PeriodMonth";
                jobParameterMonth.ValueString = cboPeriodMonth.SelectedValue;

                var jobParameterYear = jobParameters.AddNew();
                jobParameterYear.Name = "p_PeriodYear";
                jobParameterYear.ValueString = txtPeriodYear.Text;

                var jobParameterToUnit = jobParameters.AddNew();
                jobParameterToUnit.Name = "p_ToServiceUnit";
                jobParameterToUnit.ValueString = cboToServiceUnitID.SelectedValue;

                var jobParameterFromUnit = jobParameters.AddNew();
                jobParameterFromUnit.Name = "p_ServiceUnitID";
                jobParameterFromUnit.ValueString = cboFromServiceUnitID.SelectedValue;

                var jobParameterAssetId = jobParameters.AddNew();
                jobParameterAssetId.Name = "p_AssetID";
                jobParameterAssetId.ValueString = cboAssetID.SelectedValue;

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.PreventiveMaintenanceSchedule;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                "oWnd.Show();" +
                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
            else if (eventArgument.Contains("|"))
            {
                pnlInfo.Visible = false;
                var param = eventArgument.Split('|');
                var assetId = param[1];
                var date = Convert.ToDateTime(param[2]);

                var pms = new AssetPreventiveMaintenanceSchedule();
                if (pms.LoadByPrimaryKey(assetId, date))
                {
                    pms.IsVoid = true;
                    pms.VoidByUserID = AppSession.UserLogin.UserID;
                    pms.VoidDateTime = DateTime.Now;
                    pms.Save();

                    grdList.Rebind();
                }
            }
        }
    }
}
