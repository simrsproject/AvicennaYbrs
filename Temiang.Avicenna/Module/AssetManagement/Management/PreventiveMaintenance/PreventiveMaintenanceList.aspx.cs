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
    public partial class PreventiveMaintenanceList : BasePage
    {
        private AppAutoNumberLast _autoNumber, _autoNumberWo;
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

            ProgramID = AppConstant.Program.AssetPreventiveMaintenance;
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

                if (string.IsNullOrEmpty(Request.QueryString["su"]))
                    cboToServiceUnitID.Text = string.Empty;
                else
                    cboToServiceUnitID.SelectedValue = Request.QueryString["su"];

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

                txtPMDate.SelectedDate = DateTime.Now;
                //txtTargetDate.SelectedDate = DateTime.Now;
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
            grdListTaskList.Rebind();
        }

        protected void btnFilterPm_Click(object sender, ImageClickEventArgs e)
        {
            grdListTaskList.Rebind();
        }

        private void PopulateWorkTrade()
        {
            if (ViewState["worktrade"] != null)
                return;

            var query = new AppStandardReferenceItemQuery("a");
            query.Select(query.ItemID, query.ItemName);
            query.Where(query.IsActive == true, query.StandardReferenceID == AppEnum.StandardReference.WorkTrade, query.ItemID != AppSession.Parameter.WorkTradeSanitation);

            ViewState["worktrade"] = query.LoadDataTable();
        }

        protected void grdList_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
                e.Item.PreRender += grdList_ItemPreRender;
        }

        private void grdList_ItemPreRender(object sender, EventArgs e)
        {
            var dataItem = sender as GridDataItem;
            if (dataItem == null)
                return;

            var worktrade = (dataItem["AssetID"].FindControl("cboSRWorkTrade") as RadComboBox);

            if (!worktrade.Items.Any())
            {
                worktrade.Items.Clear();
                worktrade.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

                if (ViewState["worktrade"] == null) PopulateWorkTrade();

                var table = ((DataTable)ViewState["worktrade"]);
                foreach (DataRow row in table.Rows)
                {
                    worktrade.Items.Add(new RadComboBoxItem((string)row["ItemName"], (string)row["ItemID"]));
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

        private DataTable MaintenanceSchedules()
        {
            var isEmptyFilter = string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue)
                && string.IsNullOrEmpty(cboAssetID.SelectedValue);
            if (!ValidateSearch(isEmptyFilter, "Preventive Schedule")) return null;

            var query = new AssetPreventiveMaintenanceScheduleQuery("a");
            var qasset = new AssetQuery("b");
            var qunit = new ServiceUnitQuery("c");
            var qroom = new ServiceRoomQuery("d");
            var qtunit = new ServiceUnitQuery("e");
            var usr = new AppUserServiceUnitQuery("f");
            var qpm = new AssetPreventiveMaintenanceQuery("pm");

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
                qtunit.ServiceUnitName.As("MaintenanceServiceUnitName")
                );
            query.InnerJoin(qasset).On(query.AssetID == qasset.AssetID);
            query.InnerJoin(qunit).On(qasset.ServiceUnitID == qunit.ServiceUnitID);
            query.LeftJoin(qroom).On(qasset.AssetLocationID == qroom.RoomID);
            query.InnerJoin(qtunit).On(qasset.MaintenanceServiceUnitID == qtunit.ServiceUnitID);
            query.InnerJoin(usr).On(qasset.MaintenanceServiceUnitID == usr.ServiceUnitID &&
                                    usr.UserID == AppSession.UserLogin.UserID);
            query.LeftJoin(qpm).On(query.AssetID == qpm.AssetID && qasset.MaintenanceServiceUnitID == qpm.ServiceUnitID &&
                                   query.ScheduleDate == qpm.TargetDate && qpm.IsVoid == false);

            query.Where(string.Format("<MONTH(a.ScheduleDate) = {0}>", cboPeriodMonth.SelectedValue));
            query.Where(string.Format("<YEAR(a.ScheduleDate) = {0}>", txtPeriodYear.Text));

            query.Where(qpm.PMNo.IsNull(), 
                        query.IsVoid == false, 
                        query.IsProcessed == false);

            if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
                query.Where(qasset.MaintenanceServiceUnitID == cboToServiceUnitID.SelectedValue);
            if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                query.Where(qasset.ServiceUnitID == cboFromServiceUnitID.SelectedValue);
            if (!string.IsNullOrEmpty(cboAssetID.SelectedValue))
                query.Where(query.AssetID == cboAssetID.SelectedValue);

            query.OrderBy(query.ScheduleDate.Ascending, qasset.ServiceUnitID.Ascending, query.AssetID.Ascending);

            var tbl = query.LoadDataTable();
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
            var dataSource = MaintenanceSchedules();
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }

            //grdList.DataSource = MaintenanceSchedules();
        }

        private DataTable PreventiveMaintenances
        {
            get
            {
                var isEmptyFilter = txtPMDate.IsEmpty && txtTargetDate.IsEmpty && string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue)
                    && string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Preventive Schedule")) return null;

                var query = new AssetPreventiveMaintenanceQuery("a");
                var asset = new AssetQuery("b");
                var unit = new ServiceUnitQuery("c");
                var room = new ServiceRoomQuery("d");
                var unitm = new ServiceUnitQuery("e");
                var std = new AppStandardReferenceItemQuery("f");
                var user = new AppUserServiceUnitQuery("u");

                query.InnerJoin(asset).On(query.AssetID == asset.AssetID);
                query.InnerJoin(unit).On(asset.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(room).On(asset.AssetLocationID == room.RoomID);
                query.InnerJoin(unitm).On(query.ServiceUnitID == unitm.ServiceUnitID);
                query.LeftJoin(std).On(query.SRWorkTrade == std.ItemID &&
                                        std.StandardReferenceID == AppEnum.StandardReference.WorkTrade);
                query.InnerJoin(user).On(query.ServiceUnitID == user.ServiceUnitID &&
                                         user.UserID == AppSession.UserLogin.UserID);

                query.Select(
                       query.PMNo,
                       query.PMDate,
                       asset.AssetName,
                       asset.BrandName,
                       asset.SerialNumber,
                       @"<CASE WHEN ISNULL(d.RoomName, '') = '' THEN c.ServiceUnitName ELSE c.ServiceUnitName + ' - ' + d.RoomName END AS AssetLocationName>",
                       unitm.ServiceUnitName,
                       query.ServiceUnitID,
                       query.AssetID,
                       query.SRWorkTrade,
                       std.ItemName.As("WorkTrade"),
                       query.TargetDate,
                       query.IsApproved,
                       query.IsVoid,
                       @"<CASE WHEN ISNULL(a.IsApproved, 0) = 0 AND ISNULL(a.IsVoid, 0) = 0 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS 'IsEnabled'>",
                       "<'PreventiveMaintenanceDetail.aspx?md=view&id=' + a.PMNo + '&su=' + a.ServiceUnitID AS PmUrl>"
                       );
                query.Where(string.Format("<MONTH(a.TargetDate) = {0}>", cboPeriodMonth.SelectedValue));
                query.Where(string.Format("<YEAR(a.TargetDate) = {0}>", txtPeriodYear.Text));
                if (!txtPMDate.IsEmpty)
                    query.Where(query.PMDate == txtPMDate.SelectedDate);
                if (!txtTargetDate.IsEmpty)
                    query.Where(query.TargetDate == txtTargetDate.SelectedDate);
                if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
                    query.Where(query.ServiceUnitID == cboToServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                    query.Where(asset.ServiceUnitID == cboFromServiceUnitID.SelectedValue);


                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.OrderBy(query.PMDate.Descending, query.PMNo.Descending);

                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }

        protected void grdListTaskList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdListTaskList.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = PreventiveMaintenances;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }

        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            foreach (CheckBox chkBox in grdList.MasterTableView.Items.Cast<GridDataItem>().Select(dataItem => (CheckBox)dataItem.FindControl("detailChkbox")).Where(chkBox => chkBox.Visible))
            {
                chkBox.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void ToggleSelectedStateApproved(object sender, EventArgs e)
        {
            foreach (GridDataItem i in grdListTaskList.MasterTableView.Items)
            {
                var chk = (CheckBox)i.FindControl("detailChkbox");
                var chkApproved = (CheckBox)i.FindControl("approvedChkbox");
                var chkVoid = (CheckBox)i.FindControl("voidChkbox");

                chk.Checked = ((CheckBox)sender).Checked && !chkApproved.Checked && !chkVoid.Checked;
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

                var transNos = string.Empty;

                using (var trans = new esTransactionScope())
                {
                    var items = grdList.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                                                                                  .Select(dataItem => new
                                                                                  {
                                                                                      SRWorkTrade = ((RadComboBox)dataItem.FindControl("cboSRWorkTrade")).SelectedValue,
                                                                                      AssetID = dataItem["AssetID"].Text,
                                                                                      ServiceUnitID = dataItem["ServiceUnitID"].Text,
                                                                                      MaintenanceServiceUnitID = dataItem["MaintenanceServiceUnitID"].Text,
                                                                                      TargetDate = Convert.ToDateTime(dataItem["ScheduleDate"].Text)
                                                                                  });
                    foreach (var item in items)
                    {
                        var su = new ServiceUnit();
                        su.LoadByPrimaryKey(item.ServiceUnitID);

                        _autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, TransactionCode.AssetPM, su.DepartmentID);

                        var entity = new AssetPreventiveMaintenance();

                        entity.PMNo = _autoNumber.LastCompleteNumber;
                        _autoNumber.Save();

                        entity.PMDate = DateTime.Now.Date;
                        entity.ServiceUnitID = item.MaintenanceServiceUnitID;
                        entity.AssetID = item.AssetID;
                        entity.SRWorkTrade = item.SRWorkTrade;
                        entity.TargetDate = item.TargetDate;
                        entity.IsApproved = false;
                        entity.IsVoid = false;
                        entity.LastUpdateDateTime = DateTime.Now;
                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                        var pms = new AssetPreventiveMaintenanceSchedule();
                        pms.LoadByPrimaryKey(item.AssetID, item.TargetDate);
                        pms.IsProcessed = true;
                        
                        entity.Save();
                        pms.Save();

                        transNos = string.IsNullOrEmpty(transNos) ? entity.PMNo : transNos + ", " + entity.PMNo;
                    }

                    trans.Complete();
                }

                pnlInfo.Visible = true;

                if (!string.IsNullOrEmpty(transNos))
                    lblInfo.Text = "Generate Preventive Maintenance Succeed with No. : " + transNos;
                else
                    lblInfo.Text = "No items are selected for generate Preventive Maintenance.";

                grdList.Rebind();
                grdListTaskList.Rebind();
            }
            else if (eventArgument == "approved")
            {
                var items = grdListTaskList.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                                                                                  .Select(dataItem => new
                                                                                  {
                                                                                      PMNo = dataItem["PMNo"].Text
                                                                                  });
                foreach (var item in items)
                {
                    using (var trans = new esTransactionScope())
                    {
                        var entity = new AssetPreventiveMaintenance();
                        entity.LoadByPrimaryKey(item.PMNo);

                        entity.IsApproved = true;
                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        entity.LastUpdateDateTime = DateTime.Now;

                        var asset = new BusinessObject.Asset();
                        asset.LoadByPrimaryKey(entity.AssetID);

                        var su = new ServiceUnit();
                        su.LoadByPrimaryKey(asset.ServiceUnitID);

                        _autoNumberWo = Helper.GetNewAutoNumber(entity.TargetDate.Value.Date, TransactionCode.AssetWorkOrder, su.DepartmentID);

                        var wo = new AssetWorkOrder();
                        wo.AddNew();
                        wo.OrderNo = _autoNumberWo.LastCompleteNumber;
                        _autoNumberWo.Save();

                        wo.OrderDate = entity.TargetDate.Value.Date;
                        wo.FromServiceUnitID = asset.ServiceUnitID;
                        wo.ToServiceUnitID = entity.ServiceUnitID;
                        wo.AssetID = entity.AssetID;
                        wo.ProblemDescription = "Maintenance Routine";
                        wo.SRWorkStatus = AppSession.Parameter.WorkStatusOpen;
                        wo.SRWorkType = AppSession.Parameter.WorkTypePreventive;
                        wo.SRWorkPriority = AppSession.Parameter.WorkPriorityRoutine;
                        wo.SRWorkTrade = entity.SRWorkTrade;
                        wo.SRWorkTradeItem = string.Empty;
                        wo.RequiredDate = entity.TargetDate;
                        wo.RequestByUserID = AppSession.UserLogin.UserID;
                        wo.IsVoid = false;
                        wo.IsApproved = true;
                        wo.ApprovedDateTime = DateTime.Now;
                        wo.IsProceed = false;
                        wo.IsPreventiveMaintenance = true;
                        wo.PMNo = entity.PMNo;
                        wo.IsSanitation = false;
                        wo.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        wo.LastUpdateDateTime = DateTime.Now;

                        entity.Save();
                        wo.Save();

                        trans.Complete();
                    }
                }
                grdListTaskList.Rebind();
            }
            else if (eventArgument.Contains("|"))
            {
                var param = eventArgument.Split('|');
                if (param[0] == "approved")
                {
                    using (var trans = new esTransactionScope())
                    {
                        var entity = new AssetPreventiveMaintenance();
                        entity.LoadByPrimaryKey(param[1]);

                        entity.IsApproved = true;
                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        entity.LastUpdateDateTime = DateTime.Now;

                        var asset = new BusinessObject.Asset();
                        asset.LoadByPrimaryKey(entity.AssetID);

                        var su = new ServiceUnit();
                        su.LoadByPrimaryKey(asset.ServiceUnitID);

                        _autoNumberWo = Helper.GetNewAutoNumber(entity.TargetDate.Value.Date, TransactionCode.AssetWorkOrder, su.DepartmentID);

                        var wo = new AssetWorkOrder();
                        wo.AddNew();
                        wo.OrderNo = _autoNumberWo.LastCompleteNumber;
                        _autoNumberWo.Save();

                        wo.OrderDate = entity.TargetDate.Value.Date;
                        wo.FromServiceUnitID = asset.ServiceUnitID;
                        wo.ToServiceUnitID = entity.ServiceUnitID;
                        wo.AssetID = entity.AssetID;
                        wo.ProblemDescription = "Maintenance Routine";
                        wo.SRWorkStatus = AppSession.Parameter.WorkStatusOpen;
                        wo.SRWorkType = AppSession.Parameter.WorkTypePreventive;
                        wo.SRWorkPriority = AppSession.Parameter.WorkPriorityRoutine;
                        wo.SRWorkTrade = entity.SRWorkTrade;
                        wo.SRWorkTradeItem = string.Empty;
                        wo.RequiredDate = entity.TargetDate;
                        wo.RequestByUserID = AppSession.UserLogin.UserID;
                        wo.IsVoid = false;
                        wo.IsApproved = true;
                        wo.ApprovedDateTime = DateTime.Now;
                        wo.IsProceed = false;
                        wo.IsPreventiveMaintenance = true;
                        wo.PMNo = entity.PMNo;
                        wo.IsSanitation = false;
                        wo.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        wo.LastUpdateDateTime = DateTime.Now;

                        entity.Save();
                        wo.Save();

                        trans.Complete();
                    }

                    grdListTaskList.Rebind();
                }
                else if (param[0] == "void")
                {
                    var entity = new AssetPreventiveMaintenance();
                    entity.LoadByPrimaryKey(param[1]);

                    entity.IsVoid = true;
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = DateTime.Now;

                    entity.Save();
                    grdListTaskList.Rebind();
                }
                else if (param[0] == "print")
                {
                    var qwo = new AssetWorkOrderQuery();
                    qwo.Where(qwo.PMNo == param[1]);
                    var wo = new AssetWorkOrder();
                    wo.Load(qwo);

                    var orderNo = string.Empty;
                    if (wo != null)
                        orderNo = wo.OrderNo;

                    var jobParameters = new PrintJobParameterCollection();

                    var jobParameter = jobParameters.AddNew();
                    jobParameter.Name = "p_OrderNo";
                    jobParameter.ValueString = orderNo;

                    AppSession.PrintJobParameters = jobParameters;
                    AppSession.PrintJobReportID = AppConstant.Report.AssetWorkOrder;

                    string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                    "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                    "oWnd.Show();" +
                    "oWnd.Maximize();";
                    RadAjaxPanel1.ResponseScripts.Add(script);
                }
            }
        }

        #region cboAssetID
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
        #endregion
    }
}
