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

namespace Temiang.Avicenna.Module.AssetManagement.Management
{
    public partial class SanitationMaintenanceActivityProcessList : BasePage
    {
        private AppAutoNumberLast _autoNumber, _autoNumberWo;

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.SanitationMaintenanceActivityProcess;

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

                ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, TransactionCode.AssetWorkOrder, false);
                ComboBox.PopulateWorkTradeItemList(cboSRWorkTradeItem, AppSession.Parameter.WorkTradeSanitation, true);

                txtTransactionDate.SelectedDate = DateTime.Now;
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

        private DataTable MaintenanceSchedules()
        {
            var query = new SanitationMaintenanceActivityScheduleQuery("a");
            var qwti = new AppStandardReferenceItemQuery("b");
            var qunit = new ServiceUnitQuery("c");
            var qsma = new SanitationMaintenanceActivityQuery("sma");

            query.Select
                (
                query.ScheduleDate,
                query.SRWorkTradeItem,
                qwti.ItemName.As("WorkTradeItemName"),
                query.ServiceUnitID,
                qunit.ServiceUnitName
                );
            query.InnerJoin(qwti).On(qwti.StandardReferenceID == "WorkTradeItem" && qwti.ItemID == query.SRWorkTradeItem);
            query.InnerJoin(qunit).On(qunit.ServiceUnitID == query.ServiceUnitID);
            query.LeftJoin(qsma).On(qsma.SRWorkTradeItem == query.SRWorkTradeItem && qsma.ServiceUnitID == query.ServiceUnitID &&
                                   qsma.TargetDate == query.ScheduleDate && qsma.IsVoid == false);

            query.Where(string.Format("<MONTH(a.ScheduleDate) = {0}>", cboPeriodMonth.SelectedValue));
            query.Where(string.Format("<YEAR(a.ScheduleDate) = {0}>", txtPeriodYear.Text));

            query.Where(query.IsVoid == false,
                        query.IsProcessed == false, 
                        qsma.TransactionNo.IsNull());

            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
            if (!string.IsNullOrEmpty(cboSRWorkTradeItem.SelectedValue))
                query.Where(query.SRWorkTradeItem == cboSRWorkTradeItem.SelectedValue);

            query.OrderBy(query.ScheduleDate.Ascending, query.ServiceUnitID.Ascending, query.SRWorkTradeItem.Ascending);

            var tbl = query.LoadDataTable();
            return tbl;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = MaintenanceSchedules();
        }

        private DataTable SanitationMaintenanceActivitys
        {
            get
            {
                var query = new SanitationMaintenanceActivityQuery("a");
                var wti = new AppStandardReferenceItemQuery("b");
                var unit = new ServiceUnitQuery("c");

                query.InnerJoin(wti).On(wti.StandardReferenceID == "WorkTradeItem" && wti.ItemID == query.SRWorkTradeItem);
                query.InnerJoin(unit).On(unit.ServiceUnitID == query.ServiceUnitID);

                query.Select(
                       query.TransactionNo,
                       query.TransactionDate,
                       query.SRWorkTradeItem,
                       wti.ItemName.As("WorkTradeItemName"),
                       query.ServiceUnitID,
                       unit.ServiceUnitName,
                       query.TargetDate,
                       query.IsApproved,
                       query.IsVoid,
                       @"<CASE WHEN ISNULL(a.IsApproved, 0) = 0 AND ISNULL(a.IsVoid, 0) = 0 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS 'IsEnabled'>"
                       );
                query.Where(string.Format("<MONTH(a.TargetDate) = {0}>", cboPeriodMonth.SelectedValue));
                query.Where(string.Format("<YEAR(a.TargetDate) = {0}>", txtPeriodYear.Text));
                if (!txtTransactionDate.IsEmpty)
                    query.Where(query.TransactionDate == txtTransactionDate.SelectedDate);
                if (!txtTargetDate.IsEmpty)
                    query.Where(query.TargetDate == txtTargetDate.SelectedDate);
                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(cboSRWorkTradeItem.SelectedValue))
                    query.Where(query.SRWorkTradeItem == cboSRWorkTradeItem.SelectedValue);

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.OrderBy(query.TransactionDate.Descending, query.TransactionNo.Descending);

                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }

        protected void grdListTaskList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdListTaskList.DataSource = SanitationMaintenanceActivitys;
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
            //foreach (CheckBox chkBox in grdListTaskList.MasterTableView.Items.Cast<GridDataItem>().Select(dataItem => (CheckBox)dataItem.FindControl("detailChkbox")).Where(chkBox => chkBox.Visible))
            //{
            //    var chkApproved = (CheckBox)i.FindControl("approvedChkbox");
            //    var chkVoid = (CheckBox)i.FindControl("voidChkbox");
            //    chkBox.Checked = ((CheckBox)sender).Checked;
            //}

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
                                                                                      SRWorkTradeItem = dataItem["SRWorkTradeItem"].Text,
                                                                                      ServiceUnitID = dataItem["ServiceUnitID"].Text,
                                                                                      TargetDate = Convert.ToDateTime(dataItem["ScheduleDate"].Text)
                                                                                  });
                    foreach (var item in items)
                    {
                        var su = new ServiceUnit();
                        su.LoadByPrimaryKey(item.ServiceUnitID);

                        _autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, TransactionCode.SanitationMaintenanceActivity, su.DepartmentID);

                        var entity = new BusinessObject.SanitationMaintenanceActivity();

                        entity.TransactionNo = _autoNumber.LastCompleteNumber;
                        _autoNumber.Save();

                        entity.TransactionDate = DateTime.Now.Date;
                        entity.SRWorkTradeItem = item.SRWorkTradeItem;
                        entity.ServiceUnitID = item.ServiceUnitID;
                        entity.TargetDate = item.TargetDate;
                        entity.IsApproved = false;
                        entity.IsVoid = false;
                        entity.LastUpdateDateTime = DateTime.Now;
                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                        var pms = new SanitationMaintenanceActivitySchedule();
                        pms.LoadByPrimaryKey(item.SRWorkTradeItem, item.ServiceUnitID, item.TargetDate);
                        pms.IsProcessed = true;

                        entity.Save();
                        pms.Save();

                        transNos = string.IsNullOrEmpty(transNos) ? entity.TransactionNo : transNos + ", " + entity.TransactionNo;
                    }

                    trans.Complete();
                }

                pnlInfo.Visible = true;

                if (!string.IsNullOrEmpty(transNos))
                    lblInfo.Text = "Generate Sanitation Maintenance Activity Succeed with No. : " + transNos;
                else
                    lblInfo.Text = "No items are selected for generate Sanitation Maintenance Activity.";

                grdList.Rebind();
                grdListTaskList.Rebind();
            }
            else if (eventArgument == "approved")
            {
                var items = grdListTaskList.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                                                                                  .Select(dataItem => new
                                                                                  {
                                                                                      TransactionNo = dataItem["TransactionNo"].Text
                                                                                  });
                foreach (var item in items)
                {
                    using (var trans = new esTransactionScope())
                    {
                        var entity = new BusinessObject.SanitationMaintenanceActivity();
                        entity.LoadByPrimaryKey(item.TransactionNo);

                        entity.IsApproved = true;
                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        entity.LastUpdateDateTime = DateTime.Now;

                        var su = new ServiceUnit();
                        su.LoadByPrimaryKey(entity.ServiceUnitID);

                        _autoNumberWo = Helper.GetNewAutoNumber(entity.TargetDate.Value.Date, TransactionCode.SanitationWorkOrder, su.DepartmentID);

                        var wo = new AssetWorkOrder();
                        wo.AddNew();
                        wo.OrderNo = _autoNumberWo.LastCompleteNumber;
                        _autoNumberWo.Save();

                        wo.OrderDate = entity.TargetDate.Value.Date;
                        wo.FromServiceUnitID = entity.ServiceUnitID;
                        wo.ToServiceUnitID = AppSession.Parameter.ServiceUnitSanitationId;
                        wo.AssetID = string.Empty;
                        wo.ProblemDescription = "Maintenance Routine";
                        wo.SRWorkStatus = AppSession.Parameter.WorkStatusOpen;
                        wo.SRWorkType = AppSession.Parameter.WorkTypePreventive;
                        wo.SRWorkPriority = AppSession.Parameter.WorkPriorityRoutine;
                        wo.SRWorkTrade = AppSession.Parameter.WorkTradeSanitation;
                        wo.SRWorkTradeItem = entity.SRWorkTradeItem;
                        wo.RequiredDate = entity.TargetDate;
                        wo.RequestByUserID = AppSession.UserLogin.UserID;
                        wo.IsVoid = false;
                        wo.IsApproved = true;
                        wo.ApprovedDateTime = DateTime.Now;
                        wo.IsProceed = false;
                        wo.IsPreventiveMaintenance = true;
                        wo.PMNo = entity.TransactionNo;
                        wo.IsSanitation = true;
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
                        var entity = new BusinessObject.SanitationMaintenanceActivity();
                        entity.LoadByPrimaryKey(param[1]);

                        entity.IsApproved = true;
                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        entity.LastUpdateDateTime = DateTime.Now;

                        var su = new ServiceUnit();
                        su.LoadByPrimaryKey(entity.ServiceUnitID);

                        _autoNumberWo = Helper.GetNewAutoNumber(entity.TargetDate.Value.Date, TransactionCode.SanitationWorkOrder, su.DepartmentID);

                        var wo = new AssetWorkOrder();
                        wo.AddNew();
                        wo.OrderNo = _autoNumberWo.LastCompleteNumber;
                        _autoNumberWo.Save();

                        wo.OrderDate = entity.TargetDate.Value.Date;
                        wo.FromServiceUnitID = entity.ServiceUnitID;
                        wo.ToServiceUnitID = AppSession.Parameter.ServiceUnitSanitationId;
                        wo.AssetID = string.Empty;
                        wo.ProblemDescription = "Maintenance Routine";
                        wo.SRWorkStatus = AppSession.Parameter.WorkStatusOpen;
                        wo.SRWorkType = AppSession.Parameter.WorkTypePreventive;
                        wo.SRWorkPriority = AppSession.Parameter.WorkPriorityRoutine;
                        wo.SRWorkTrade = AppSession.Parameter.WorkTradeSanitation;
                        wo.SRWorkTradeItem = entity.SRWorkTradeItem;
                        wo.RequiredDate = entity.TargetDate;
                        wo.RequestByUserID = AppSession.UserLogin.UserID;
                        wo.IsVoid = false;
                        wo.IsApproved = true;
                        wo.ApprovedDateTime = DateTime.Now;
                        wo.IsProceed = false;
                        wo.IsPreventiveMaintenance = true;
                        wo.PMNo = entity.TransactionNo;
                        wo.IsSanitation = true;
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
                    var entity = new BusinessObject.SanitationMaintenanceActivity();
                    entity.LoadByPrimaryKey(param[1]);

                    entity.IsVoid = true;
                    entity.VoidByUserID = AppSession.UserLogin.UserID;
                    entity.VoidDateTime = DateTime.Now;

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
    }
}