using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Payroll
{
    public partial class WorkingScheduleInterventionDetail : BasePageDetail
    {
        private string RoleType
        {
            get
            {
                return !string.IsNullOrEmpty(Request.QueryString["role"]) ? Request.QueryString["role"] : string.Empty;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "WorkingScheduleInterventionSearch.aspx?role=" + RoleType;
            UrlPageList = "WorkingScheduleInterventionList.aspx?role=" + RoleType;

            ProgramID = RoleType == "admin" ? AppConstant.Program.WorkingScheduleInterventionAdmin : AppConstant.Program.WorkingScheduleIntervention;

            WindowSearch.Height = 300;
        }

        protected override void OnMenuNewClick()
        {
            ViewState["id"] = -1;

            rbtEntryType.SelectedValue = "1";
            cboEmployeeName.SelectedValue = string.Empty;
            cboDayFrom.SelectedValue = string.Empty;
            cboDayTo.SelectedValue = string.Empty;
            cboWorkingHour.SelectedValue = string.Empty;

            var user = new AppUser();
            user.LoadByPrimaryKey(AppSession.UserLogin.UserID);

            var emp = new VwEmployeeTable();
            emp.Query.Where(emp.Query.PersonID == user.PersonID);
            emp.Query.Load();

            var empOrgs = new EmployeeOrganizationCollection();
            empOrgs.Query.Where(empOrgs.Query.PersonID == emp.PersonID && empOrgs.Query.ValidTo.Date() > DateTime.Now.Date);
            if (empOrgs.Query.Load())
            {
                var orgs = new OrganizationUnitCollection();
                orgs.Query.es.Distinct = true;
                //orgs.Query.Where(orgs.Query.OrganizationUnitID == emp.ServiceUnitID);
                if (RoleType == "user")
                    orgs.Query.Where(orgs.Query.OrganizationUnitID.In(empOrgs.Select(e => e.ServiceUnitID)));
                orgs.Query.Where(orgs.Query.IsActive == true);
                orgs.Query.OrderBy(orgs.Query.OrganizationUnitName.Ascending);
                orgs.Query.Load();

                if (orgs.Count > 1) cboOrganizationUnit.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
                foreach (var org in orgs)
                {
                    cboOrganizationUnit.Items.Add(new Telerik.Web.UI.RadComboBoxItem(org.OrganizationUnitName, org.OrganizationUnitID.ToString()));
                }
                if (orgs.Count == 1)
                {
                    cboOrganizationUnit.SelectedIndex = 0;
                    cboOrganizationUnit_SelectedIndexChanged(null, new Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, cboOrganizationUnit.SelectedValue, string.Empty));
                }
            }

            //var whou = new WorkingHourOrganizationUnitCollection();
            //whou.Query.Where(whou.Query.OrganizationUnitID == emp.ServiceUnitID);
            //if (whou.Query.Load())
            //{
            //    var whs = new WorkingHourCollection();
            //    whs.Query.Where(whs.Query.WorkingHourID.In(whou.Select(w => w.WorkingHourID)), whs.Query.IsActive == true);
            //    whs.Query.OrderBy(whs.Query.WorkingHourName.Ascending);
            //    whs.Query.Load();

            //    cboWorkingHour.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
            //    //cboWorkingHour.Items.Add(new Telerik.Web.UI.RadComboBoxItem("No Schedule", "-1"));
            //    foreach (var wh in whs)
            //    {
            //        cboWorkingHour.Items.Add(new Telerik.Web.UI.RadComboBoxItem(wh.WorkingHourName + " (" + wh.StartTime + " to " + wh.EndTime + ")", wh.WorkingHourID.ToString()));
            //    }
            //}

            grdWorkingScheduleDetail.Rebind();
        }

        protected override void OnMenuEditClick()
        {
            rbtEntryType.SelectedValue = "1";
            cboEmployeeName.SelectedValue = string.Empty;
            cboDayFrom.SelectedValue = string.Empty;
            cboDayTo.SelectedValue = string.Empty;
            cboWorkingHour.SelectedValue = string.Empty;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (WorkingScheduleDetails.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new WorkingSchduleIntervention();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (WorkingScheduleDetails.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new WorkingSchduleIntervention();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(ViewState["id"].ToString())))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
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
            auditLogFilter.PrimaryKeyData = string.Format("WorkingSchduleInterventionID='{0}'", ViewState["id"].ToString());
            auditLogFilter.TableName = "WorkingSchduleIntervention";
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new WorkingSchduleIntervention();
            if (parameters.Length > 0)
            {
                if (!string.IsNullOrWhiteSpace(parameters[0])) entity.LoadByPrimaryKey(int.Parse(parameters[0]));
            }
            else entity.LoadByPrimaryKey(Convert.ToInt32(ViewState["id"].ToString()));

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var wh = (WorkingSchduleIntervention)entity;
            if (wh == null)
            {
                ViewState["id"] = -1;
                return;
            }
            ViewState["id"] = wh.WorkingSchduleInterventionID == null ? -1 : wh.WorkingSchduleInterventionID;
            ViewState["IsApproved"] = wh.IsApproved ?? false;
            ViewState["IsVoid"] = wh.IsVoid ?? false;

            if ((int)ViewState["id"] == -1) return;

            var ws = new WorkingSchedule();
            ws.LoadByPrimaryKey(wh.WorkingScheduleID ?? 0);

            var orgs = new OrganizationUnitCollection();
            orgs.Query.es.Distinct = true;
            orgs.Query.Where(orgs.Query.IsActive == true);
            orgs.Query.OrderBy(orgs.Query.OrganizationUnitName.Ascending);
            orgs.Query.Load();

            if (orgs.Count > 1) cboOrganizationUnit.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
            foreach (var org in orgs)
            {
                cboOrganizationUnit.Items.Add(new Telerik.Web.UI.RadComboBoxItem(org.OrganizationUnitName, org.OrganizationUnitID.ToString()));
            }

            cboOrganizationUnit.SelectedValue = ws.OrganizationUnitID.ToString();
            cboOrganizationUnit_SelectedIndexChanged(null, new Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, ws.OrganizationUnitID.ToString(), string.Empty));
            cboPayrollPeriod.SelectedValue = ws.PayrollPeriodID.ToString();
            cboPayrollPeriod_SelectedIndexChanged(null, new Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, ws.PayrollPeriodID.ToString(), string.Empty));
            cboWorkingSchedule.SelectedValue = wh.WorkingScheduleID.ToString();
            cboWorkingSchedule_SelectedIndexChanged(null, new Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, ws.WorkingScheduleID.ToString(), string.Empty));

            //var whou = new WorkingHourOrganizationUnitCollection();
            //whou.Query.Where(whou.Query.OrganizationUnitID == ws.OrganizationUnitID);
            //if (whou.Query.Load())
            //{
            //    var whs = new WorkingHourCollection();
            //    whs.Query.Where(whs.Query.WorkingHourID.In(whou.Select(w => w.WorkingHourID)), whs.Query.IsActive == true);
            //    whs.Query.OrderBy(whs.Query.WorkingHourName.Ascending);
            //    whs.Query.Load();

            //    cboWorkingHour.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
            //    //cboWorkingHour.Items.Add(new Telerik.Web.UI.RadComboBoxItem("No Schedule", "-1"));
            //    foreach (var hour in whs)
            //    {
            //        cboWorkingHour.Items.Add(new Telerik.Web.UI.RadComboBoxItem(hour.WorkingHourName + " (" + hour.StartTime + " to " + hour.EndTime + ")", hour.WorkingHourID.ToString()));
            //    }
            //}
            txtNotes.Text = wh.Notes;

            grdWorkingScheduleDetail.Rebind();
        }

        private void SetEntityValue(WorkingSchduleIntervention entity)
        {
            entity.WorkingScheduleID = cboWorkingSchedule.SelectedValue.ToInt();
            entity.Notes = txtNotes.Text;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.IsApproved = false;
                entity.IsVoid = false;
                entity.LastUpdateUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;

                foreach (var wsd in WorkingScheduleDetails)
                {
                    //wsd.WorkingSchduleInterventionID = entity.WorkingSchduleInterventionID;
                    wsd.LastUpdateUserID = AppSession.UserLogin.UserID;
                    wsd.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void SaveEntity(WorkingSchduleIntervention entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();

                foreach (var wsd in WorkingScheduleDetails)
                {
                    wsd.WorkingSchduleInterventionID = entity.WorkingSchduleInterventionID;

                    //    if (wsd.WorkingHourIDDay1 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay1 == wsd.WorkingHourIDDay1);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay1 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay2 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay2 == wsd.WorkingHourIDDay2);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay2 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay3 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay3 == wsd.WorkingHourIDDay3);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay3 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay4 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay4 == wsd.WorkingHourIDDay4);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay4 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay5 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay5 == wsd.WorkingHourIDDay5);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay5 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay6 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay6 == wsd.WorkingHourIDDay6);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay6 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay7 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay7 == wsd.WorkingHourIDDay7);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay7 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay8 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay8 == wsd.WorkingHourIDDay8);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay8 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay9 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay9 == wsd.WorkingHourIDDay9);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay9 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay10 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay10 == wsd.WorkingHourIDDay10);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay10 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay11 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay11 == wsd.WorkingHourIDDay11);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay11 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay12 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay12 == wsd.WorkingHourIDDay12);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay12 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay13 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay13 == wsd.WorkingHourIDDay13);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay13 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay14 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay14 == wsd.WorkingHourIDDay14);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay14 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay15 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay15 == wsd.WorkingHourIDDay15);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay15 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay16 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay16 == wsd.WorkingHourIDDay16);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay16 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay17 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay17 == wsd.WorkingHourIDDay17);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay17 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay18 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay18 == wsd.WorkingHourIDDay18);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay18 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay19 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay19 == wsd.WorkingHourIDDay19);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay19 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay20 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay20 == wsd.WorkingHourIDDay20);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay20 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay21 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay21 == wsd.WorkingHourIDDay21);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay21 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay22 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay22 == wsd.WorkingHourIDDay22);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay22 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay23 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay23 == wsd.WorkingHourIDDay23);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay23 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay24 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay24 == wsd.WorkingHourIDDay24);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay24 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay25 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay25 == wsd.WorkingHourIDDay25);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay25 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay26 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay26 == wsd.WorkingHourIDDay26);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay26 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay27 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay27 == wsd.WorkingHourIDDay27);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay27 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay28 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay28 == wsd.WorkingHourIDDay28);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay28 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay29 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay29 == wsd.WorkingHourIDDay29);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay29 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay30 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay30 == wsd.WorkingHourIDDay30);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay30 = null;
                    //        }
                    //    }
                    //    if (wsd.WorkingHourIDDay31 != null)
                    //    {
                    //        var wsod = new BusinessObject.WorkingScheduleDetail();
                    //        wsod.Query.Where(wsod.Query.WorkingScheduleID == entity.WorkingScheduleID, wsod.Query.PersonID == wsd.PersonID, wsod.Query.WorkingHourIDDay31 == wsd.WorkingHourIDDay31);
                    //        if (wsod.Query.Load())
                    //        {
                    //            wsd.WorkingHourIDDay31 = null;
                    //        }
                    //    }

                    //    wsd.LastUpdateUserID = AppSession.UserLogin.UserID;
                    //    wsd.LastUpdateDateTime = DateTime.Now;
                }
                WorkingScheduleDetails.Save();

                //Commit if success, Rollback if failed
                trans.Complete();

                ViewState["id"] = entity.WorkingSchduleInterventionID;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new WorkingSchduleInterventionQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.WorkingScheduleID > (int)ViewState["id"]);
                que.OrderBy(que.WorkingScheduleID.Ascending);
            }
            else
            {
                que.Where(que.WorkingScheduleID < (int)ViewState["id"]);
                que.OrderBy(que.WorkingScheduleID.Descending);
            }

            var entity = new WorkingSchduleIntervention();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        protected void cboOrganizationUnit_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Value)) return;

            var wsc = new WorkingScheduleCollection();
            wsc.Query.Where(wsc.Query.OrganizationUnitID == e.Value.ToInt() && wsc.Query.IsApproved == true && wsc.Query.IsVoid == false);
            wsc.Query.Load();

            if (wsc.Any())
            {
                var periods = new PayrollPeriodCollection();
                periods.Query.Where(periods.Query.PayrollPeriodID.In(wsc.Select(w => w.PayrollPeriodID)));
                periods.Query.OrderBy(periods.Query.SPTYear.Descending, periods.Query.SPTMonth.Ascending);
                periods.Query.Load();

                cboPayrollPeriod.Items.Clear();
                cboPayrollPeriod.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
                foreach (var period in periods)
                {
                    cboPayrollPeriod.Items.Add(new Telerik.Web.UI.RadComboBoxItem(period.PayrollPeriodName, period.PayrollPeriodID.ToString()));
                }
            }

            var whou = new WorkingHourOrganizationUnitCollection();
            whou.Query.Where(whou.Query.OrganizationUnitID == e.Value.ToInt());
            if (whou.Query.Load())
            {
                var whs = new WorkingHourCollection();
                whs.Query.Where(whs.Query.WorkingHourID.In(whou.Select(w => w.WorkingHourID)), whs.Query.IsActive == true);
                whs.Query.OrderBy(whs.Query.WorkingHourName.Ascending);
                whs.Query.Load();

                cboWorkingHour.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
                foreach (var wh in whs)
                {
                    cboWorkingHour.Items.Add(new Telerik.Web.UI.RadComboBoxItem(wh.WorkingHourName + " (" + wh.StartTime + " to " + wh.EndTime + ")", wh.WorkingHourID.ToString()));
                }
            }
        }

        private HolidayScheduleCollection HolidaySchedules
        {
            get
            {
                if (string.IsNullOrEmpty(cboPayrollPeriod.SelectedValue)) return new HolidayScheduleCollection();

                var period = new PayrollPeriod();
                period.LoadByPrimaryKey(cboPayrollPeriod.SelectedValue.ToInt());

                var sch = new HolidayScheduleCollection();
                sch.Query.Where($"<month(HolidayDate) = {period.SPTMonth} and year(HolidayDate) = {period.SPTYear}>");
                sch.Query.Load();
                return sch;
            }
        }

        private WorkingSchduleInterventionDetailCollection WorkingScheduleDetails
        {
            get
            {
                if (ViewState["workingScheduleDetailInterventions"] != null)
                {
                    return (WorkingSchduleInterventionDetailCollection)ViewState["workingScheduleDetailInterventions"];
                }
                else
                {
                    var period = new PayrollPeriod();
                    period.LoadByPrimaryKey(cboPayrollPeriod.SelectedValue.ToInt());

                    var day = DateTime.DaysInMonth(period.SPTYear ?? -1, period.SPTMonth ?? -1);

                    var wsd = new WorkingSchduleInterventionDetailQuery("a");
                    var emp = new VwEmployeeTableQuery("b");
                    var whq1 = new WorkingHourQuery("c1");
                    var whq2 = new WorkingHourQuery("c2");
                    var whq3 = new WorkingHourQuery("c3");
                    var whq4 = new WorkingHourQuery("c4");
                    var whq5 = new WorkingHourQuery("c5");
                    var whq6 = new WorkingHourQuery("c6");
                    var whq7 = new WorkingHourQuery("c7");
                    var whq8 = new WorkingHourQuery("c8");
                    var whq9 = new WorkingHourQuery("c9");
                    var whq10 = new WorkingHourQuery("c10");
                    var whq11 = new WorkingHourQuery("c11");
                    var whq12 = new WorkingHourQuery("c12");
                    var whq13 = new WorkingHourQuery("c13");
                    var whq14 = new WorkingHourQuery("c14");
                    var whq15 = new WorkingHourQuery("c15");
                    var whq16 = new WorkingHourQuery("c16");
                    var whq17 = new WorkingHourQuery("c17");
                    var whq18 = new WorkingHourQuery("c18");
                    var whq19 = new WorkingHourQuery("c19");
                    var whq20 = new WorkingHourQuery("c20");
                    var whq21 = new WorkingHourQuery("c21");
                    var whq22 = new WorkingHourQuery("c22");
                    var whq23 = new WorkingHourQuery("c23");
                    var whq24 = new WorkingHourQuery("c24");
                    var whq25 = new WorkingHourQuery("c25");
                    var whq26 = new WorkingHourQuery("c26");
                    var whq27 = new WorkingHourQuery("c27");
                    var whq28 = new WorkingHourQuery("c28");
                    var whq29 = new WorkingHourQuery("c29");
                    var whq30 = new WorkingHourQuery("c30");
                    var whq31 = new WorkingHourQuery("c31");

                    wsd.Select(
                        wsd,
                        emp.EmployeeNumber.As("refToVwEmployeeTable_EmployeeNumber"),
                        emp.EmployeeName.As("refToVwEmployeeTable_EmployeeName"),
                        whq1.WorkingHourName.As("refToWorkingHour1_WorkingHourName"),
                        whq2.WorkingHourName.As("refToWorkingHour2_WorkingHourName"),
                        whq3.WorkingHourName.As("refToWorkingHour3_WorkingHourName"),
                        whq4.WorkingHourName.As("refToWorkingHour4_WorkingHourName"),
                        whq5.WorkingHourName.As("refToWorkingHour5_WorkingHourName"),
                        whq6.WorkingHourName.As("refToWorkingHour6_WorkingHourName"),
                        whq7.WorkingHourName.As("refToWorkingHour7_WorkingHourName"),
                        whq8.WorkingHourName.As("refToWorkingHour8_WorkingHourName"),
                        whq9.WorkingHourName.As("refToWorkingHour9_WorkingHourName"),
                        whq10.WorkingHourName.As("refToWorkingHour10_WorkingHourName"),
                        whq11.WorkingHourName.As("refToWorkingHour11_WorkingHourName"),
                        whq12.WorkingHourName.As("refToWorkingHour12_WorkingHourName"),
                        whq13.WorkingHourName.As("refToWorkingHour13_WorkingHourName"),
                        whq14.WorkingHourName.As("refToWorkingHour14_WorkingHourName"),
                        whq15.WorkingHourName.As("refToWorkingHour15_WorkingHourName"),
                        whq16.WorkingHourName.As("refToWorkingHour16_WorkingHourName"),
                        whq17.WorkingHourName.As("refToWorkingHour17_WorkingHourName"),
                        whq18.WorkingHourName.As("refToWorkingHour18_WorkingHourName"),
                        whq19.WorkingHourName.As("refToWorkingHour19_WorkingHourName"),
                        whq20.WorkingHourName.As("refToWorkingHour20_WorkingHourName"),
                        whq21.WorkingHourName.As("refToWorkingHour21_WorkingHourName"),
                        whq22.WorkingHourName.As("refToWorkingHour22_WorkingHourName"),
                        whq23.WorkingHourName.As("refToWorkingHour23_WorkingHourName"),
                        whq24.WorkingHourName.As("refToWorkingHour24_WorkingHourName"),
                        whq25.WorkingHourName.As("refToWorkingHour25_WorkingHourName"),
                        whq26.WorkingHourName.As("refToWorkingHour26_WorkingHourName"),
                        whq27.WorkingHourName.As("refToWorkingHour27_WorkingHourName"),
                        whq28.WorkingHourName.As("refToWorkingHour28_WorkingHourName"),
                        whq29.WorkingHourName.As("refToWorkingHour29_WorkingHourName"),
                        whq30.WorkingHourName.As("refToWorkingHour30_WorkingHourName"),
                        whq31.WorkingHourName.As("refToWorkingHour31_WorkingHourName"),
                        "<'' AS refToWorkingDayName1_WorkingDayName>",
                        "<'' AS refToWorkingDayName2_WorkingDayName>",
                        "<'' AS refToWorkingDayName3_WorkingDayName>",
                        "<'' AS refToWorkingDayName4_WorkingDayName>",
                        "<'' AS refToWorkingDayName5_WorkingDayName>",
                        "<'' AS refToWorkingDayName6_WorkingDayName>",
                        "<'' AS refToWorkingDayName7_WorkingDayName>",
                        "<'' AS refToWorkingDayName8_WorkingDayName>",
                        "<'' AS refToWorkingDayName9_WorkingDayName>",
                        "<'' AS refToWorkingDayName10_WorkingDayName>",
                        "<'' AS refToWorkingDayName11_WorkingDayName>",
                        "<'' AS refToWorkingDayName12_WorkingDayName>",
                        "<'' AS refToWorkingDayName13_WorkingDayName>",
                        "<'' AS refToWorkingDayName14_WorkingDayName>",
                        "<'' AS refToWorkingDayName15_WorkingDayName>",
                        "<'' AS refToWorkingDayName16_WorkingDayName>",
                        "<'' AS refToWorkingDayName17_WorkingDayName>",
                        "<'' AS refToWorkingDayName18_WorkingDayName>",
                        "<'' AS refToWorkingDayName19_WorkingDayName>",
                        "<'' AS refToWorkingDayName20_WorkingDayName>",
                        "<'' AS refToWorkingDayName21_WorkingDayName>",
                        "<'' AS refToWorkingDayName22_WorkingDayName>",
                        "<'' AS refToWorkingDayName23_WorkingDayName>",
                        "<'' AS refToWorkingDayName24_WorkingDayName>",
                        "<'' AS refToWorkingDayName25_WorkingDayName>",
                        "<'' AS refToWorkingDayName26_WorkingDayName>",
                        "<'' AS refToWorkingDayName27_WorkingDayName>",
                        "<'' AS refToWorkingDayName28_WorkingDayName>",
                        "<'' AS refToWorkingDayName29_WorkingDayName>",
                        "<'' AS refToWorkingDayName30_WorkingDayName>",
                        "<'' AS refToWorkingDayName31_WorkingDayName>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday1_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday2_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday3_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday4_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday5_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday6_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday7_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday8_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday9_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday10_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday11_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday12_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday13_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday14_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday15_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday16_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday17_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday18_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday19_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday20_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday21_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday22_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday23_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday24_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday25_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday26_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday27_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday28_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday29_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday30_WorkingDayHoliday>",
                        "<cast(0 as bit) AS refToWorkingDayHoliday31_WorkingDayHoliday>"
                    );
                    wsd.InnerJoin(emp).On(wsd.PersonID == emp.PersonID);
                    wsd.LeftJoin(whq1).On(wsd.WorkingHourIDDay1 == whq1.WorkingHourID);
                    wsd.LeftJoin(whq2).On(wsd.WorkingHourIDDay2 == whq2.WorkingHourID);
                    wsd.LeftJoin(whq3).On(wsd.WorkingHourIDDay3 == whq3.WorkingHourID);
                    wsd.LeftJoin(whq4).On(wsd.WorkingHourIDDay4 == whq4.WorkingHourID);
                    wsd.LeftJoin(whq5).On(wsd.WorkingHourIDDay5 == whq5.WorkingHourID);
                    wsd.LeftJoin(whq6).On(wsd.WorkingHourIDDay6 == whq6.WorkingHourID);
                    wsd.LeftJoin(whq7).On(wsd.WorkingHourIDDay7 == whq7.WorkingHourID);
                    wsd.LeftJoin(whq8).On(wsd.WorkingHourIDDay8 == whq8.WorkingHourID);
                    wsd.LeftJoin(whq9).On(wsd.WorkingHourIDDay9 == whq9.WorkingHourID);
                    wsd.LeftJoin(whq10).On(wsd.WorkingHourIDDay10 == whq10.WorkingHourID);
                    wsd.LeftJoin(whq11).On(wsd.WorkingHourIDDay11 == whq11.WorkingHourID);
                    wsd.LeftJoin(whq12).On(wsd.WorkingHourIDDay12 == whq12.WorkingHourID);
                    wsd.LeftJoin(whq13).On(wsd.WorkingHourIDDay13 == whq13.WorkingHourID);
                    wsd.LeftJoin(whq14).On(wsd.WorkingHourIDDay14 == whq14.WorkingHourID);
                    wsd.LeftJoin(whq15).On(wsd.WorkingHourIDDay15 == whq15.WorkingHourID);
                    wsd.LeftJoin(whq16).On(wsd.WorkingHourIDDay16 == whq16.WorkingHourID);
                    wsd.LeftJoin(whq17).On(wsd.WorkingHourIDDay17 == whq17.WorkingHourID);
                    wsd.LeftJoin(whq18).On(wsd.WorkingHourIDDay18 == whq18.WorkingHourID);
                    wsd.LeftJoin(whq19).On(wsd.WorkingHourIDDay19 == whq19.WorkingHourID);
                    wsd.LeftJoin(whq20).On(wsd.WorkingHourIDDay20 == whq20.WorkingHourID);
                    wsd.LeftJoin(whq21).On(wsd.WorkingHourIDDay21 == whq21.WorkingHourID);
                    wsd.LeftJoin(whq22).On(wsd.WorkingHourIDDay22 == whq22.WorkingHourID);
                    wsd.LeftJoin(whq23).On(wsd.WorkingHourIDDay23 == whq23.WorkingHourID);
                    wsd.LeftJoin(whq24).On(wsd.WorkingHourIDDay24 == whq24.WorkingHourID);
                    wsd.LeftJoin(whq25).On(wsd.WorkingHourIDDay25 == whq25.WorkingHourID);
                    wsd.LeftJoin(whq26).On(wsd.WorkingHourIDDay26 == whq26.WorkingHourID);
                    wsd.LeftJoin(whq27).On(wsd.WorkingHourIDDay27 == whq27.WorkingHourID);
                    wsd.LeftJoin(whq28).On(wsd.WorkingHourIDDay28 == whq28.WorkingHourID);
                    wsd.LeftJoin(whq29).On(wsd.WorkingHourIDDay29 == whq29.WorkingHourID);
                    wsd.LeftJoin(whq30).On(wsd.WorkingHourIDDay30 == whq30.WorkingHourID);
                    wsd.LeftJoin(whq31).On(wsd.WorkingHourIDDay31 == whq31.WorkingHourID);
                    wsd.Where(wsd.WorkingSchduleInterventionID == (int)ViewState["id"]);
                    wsd.OrderBy(emp.EmployeeNumber.Ascending);

                    var coll = new WorkingSchduleInterventionDetailCollection();
                    coll.Load(wsd);

                    foreach (var row in coll)
                    {
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName1))
                        {
                            row.WorkingDayName1 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 1).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 1)) row.WorkingDayHoliday1 = true;
                        }
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName2))
                        {
                            row.WorkingDayName2 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 2).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 2)) row.WorkingDayHoliday2 = true;
                        }
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName3))
                        {
                            row.WorkingDayName3 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 3).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 3)) row.WorkingDayHoliday3 = true;
                        }
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName4))
                        {
                            row.WorkingDayName4 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 4).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 4)) row.WorkingDayHoliday4 = true;
                        }
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName5))
                        {
                            row.WorkingDayName5 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 5).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 5)) row.WorkingDayHoliday5 = true;
                        }
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName6))
                        {
                            row.WorkingDayName6 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 6).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 6)) row.WorkingDayHoliday6 = true;
                        }
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName7))
                        {
                            row.WorkingDayName7 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 7).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 7)) row.WorkingDayHoliday7 = true;
                        }
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName8))
                        {
                            row.WorkingDayName8 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 8).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 8)) row.WorkingDayHoliday8 = true;
                        }
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName9))
                        {
                            row.WorkingDayName9 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 9).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 9)) row.WorkingDayHoliday9 = true;
                        }
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName10))
                        {
                            row.WorkingDayName10 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 10).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 10)) row.WorkingDayHoliday10 = true;
                        }
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName11))
                        {
                            row.WorkingDayName11 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 11).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 11)) row.WorkingDayHoliday11 = true;
                        }
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName12))
                        {
                            row.WorkingDayName12 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 12).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 12)) row.WorkingDayHoliday12 = true;
                        }
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName13))
                        {
                            row.WorkingDayName13 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 13).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 13)) row.WorkingDayHoliday13 = true;
                        }
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName14))
                        {
                            row.WorkingDayName14 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 14).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 14)) row.WorkingDayHoliday14 = true;
                        }
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName15))
                        {
                            row.WorkingDayName15 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 15).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 15)) row.WorkingDayHoliday15 = true;
                        }
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName16))
                        {
                            row.WorkingDayName16 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 16).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 16)) row.WorkingDayHoliday16 = true;
                        }
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName17))
                        {
                            row.WorkingDayName17 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 17).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 17)) row.WorkingDayHoliday17 = true;
                        }
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName18))
                        {
                            row.WorkingDayName18 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 18).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 18)) row.WorkingDayHoliday18 = true;
                        }
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName19))
                        {
                            row.WorkingDayName19 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 19).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 19)) row.WorkingDayHoliday19 = true;
                        }
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName20))
                        {
                            row.WorkingDayName20 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 20).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 20)) row.WorkingDayHoliday20 = true;
                        }
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName21))
                        {
                            row.WorkingDayName21 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 21).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 21)) row.WorkingDayHoliday21 = true;
                        }
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName22))
                        {
                            row.WorkingDayName22 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 22).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 22)) row.WorkingDayHoliday22 = true;
                        }
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName23))
                        {
                            row.WorkingDayName23 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 23).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 23)) row.WorkingDayHoliday23 = true;
                        }
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName24))
                        {
                            row.WorkingDayName24 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 24).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 24)) row.WorkingDayHoliday24 = true;
                        }
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName25))
                        {
                            row.WorkingDayName25 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 25).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 25)) row.WorkingDayHoliday25 = true;
                        }
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName26))
                        {
                            row.WorkingDayName26 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 26).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 26)) row.WorkingDayHoliday26 = true;
                        }
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName27))
                        {
                            row.WorkingDayName27 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 27).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 27)) row.WorkingDayHoliday27 = true;
                        }
                        if (string.IsNullOrWhiteSpace(row.WorkingDayName28))
                        {
                            row.WorkingDayName28 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 28).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 28)) row.WorkingDayHoliday28 = true;
                        }
                        if (day >= 29)
                            if (string.IsNullOrWhiteSpace(row.WorkingDayName29))
                            {
                                row.WorkingDayName29 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 29).DayOfWeek.ToString();
                                if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 29)) row.WorkingDayHoliday29 = true;
                            }
                        if (day >= 30)
                            if (string.IsNullOrWhiteSpace(row.WorkingDayName30))
                            {
                                row.WorkingDayName30 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 30).DayOfWeek.ToString();
                                if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 30)) row.WorkingDayHoliday30 = true;
                            }
                        if (day >= 31)
                            if (string.IsNullOrWhiteSpace(row.WorkingDayName31))
                            {
                                row.WorkingDayName31 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 31).DayOfWeek.ToString();
                                if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 31)) row.WorkingDayHoliday31 = true;
                            }
                    }

                    ViewState["workingScheduleDetailInterventions"] = coll;
                    return coll;
                }
            }
            set
            {

            }
        }

        protected void grdWorkingScheduleDetail_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdWorkingScheduleDetail.DataSource = WorkingScheduleDetails;
        }

        protected void grdWorkingScheduleDetail_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void grdWorkingScheduleDetail_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void grdWorkingScheduleDetail_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cboWorkingSchedule.SelectedValue)) return;
            if (rbtEntryType.SelectedValue == "1" && string.IsNullOrEmpty(cboEmployeeName.SelectedValue)) return;
            if (string.IsNullOrEmpty(cboDayFrom.SelectedValue)) return;
            if (string.IsNullOrEmpty(cboDayTo.SelectedValue)) return;
            if (cboDayFrom.Text.ToInt() > cboDayTo.Text.ToInt()) return;
            if (cboWorkingHour.SelectedValue == string.Empty) return;

            var emps = new List<string>();
            if (rbtEntryType.SelectedValue == "0")
            {
                foreach (var item in cboEmployeeName.Items.Where(i => !string.IsNullOrEmpty(i.Value)))
                {
                    emps.Add(item.Value);
                }
            }
            else
            {
                emps.Add(cboEmployeeName.SelectedValue);
            }

            var period = new PayrollPeriod();
            period.LoadByPrimaryKey(cboPayrollPeriod.SelectedValue.ToInt());

            var day = DateTime.DaysInMonth(period.SPTYear ?? -1, period.SPTMonth ?? -1);

            foreach (var value in emps)
            {
                var wsd = WorkingScheduleDetails.SingleOrDefault(w => w.WorkingSchduleInterventionID == (int)ViewState["id"] && w.PersonID == value.ToInt());
                if (wsd == null) continue;

                var from = cboDayFrom.Text.ToInt();
                while (from <= cboDayTo.Text.ToInt())
                {
                    var item = cboDayTo.Items.Single(c => c.Text == from.ToString());
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay1 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay1 = null;
                            wsd.WorkingHourName1 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay1 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName1 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName1 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 1).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 1)) wsd.WorkingDayHoliday1 = true;
                    }
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay2 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay2 = null;
                            wsd.WorkingHourName2 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay2 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName2 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName2 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 2).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 2)) wsd.WorkingDayHoliday2 = true;
                    }
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay3 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay3 = null;
                            wsd.WorkingHourName3 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay3 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName3 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName3 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 3).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 3)) wsd.WorkingDayHoliday3 = true;
                    }
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay4 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay4 = null;
                            wsd.WorkingHourName4 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay4 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName4 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName4 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 4).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 4)) wsd.WorkingDayHoliday4 = true;
                    }
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay5 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay5 = null;
                            wsd.WorkingHourName5 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay5 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName5 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName5 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 5).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 5)) wsd.WorkingDayHoliday5 = true;
                    }
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay6 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay6 = null;
                            wsd.WorkingHourName6 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay6 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName6 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName6 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 6).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 6)) wsd.WorkingDayHoliday6 = true;
                    }
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay7 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay7 = null;
                            wsd.WorkingHourName7 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay7 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName7 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName7 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 7).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 7)) wsd.WorkingDayHoliday7 = true;
                    }
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay8 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay8 = null;
                            wsd.WorkingHourName8 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay8 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName8 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName8 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 8).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 8)) wsd.WorkingDayHoliday8 = true;
                    }
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay9 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay9 = null;
                            wsd.WorkingHourName9 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay9 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName9 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName9 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 9).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 9)) wsd.WorkingDayHoliday9 = true;
                    }
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay10 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay10 = null;
                            wsd.WorkingHourName10 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay10 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName10 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName10 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 10).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 10)) wsd.WorkingDayHoliday10 = true;
                    }
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay11 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay11 = null;
                            wsd.WorkingHourName11 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay11 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName11 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName11 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 11).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 11)) wsd.WorkingDayHoliday11 = true;
                    }
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay12 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay12 = null;
                            wsd.WorkingHourName12 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay12 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName12 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName12 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 12).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 12)) wsd.WorkingDayHoliday12 = true;
                    }
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay13 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay13 = null;
                            wsd.WorkingHourName13 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay13 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName13 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName13 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 13).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 13)) wsd.WorkingDayHoliday13 = true;
                    }
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay14 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay14 = null;
                            wsd.WorkingHourName14 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay14 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName14 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName14 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 14).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 14)) wsd.WorkingDayHoliday14 = true;
                    }
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay15 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay15 = null;
                            wsd.WorkingHourName15 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay15 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName15 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName15 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 15).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 15)) wsd.WorkingDayHoliday15 = true;
                    }
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay16 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay16 = null;
                            wsd.WorkingHourName16 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay16 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName16 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName16 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 16).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 16)) wsd.WorkingDayHoliday16 = true;
                    }
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay17 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay17 = null;
                            wsd.WorkingHourName17 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay17 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName17 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName17 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 17).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 17)) wsd.WorkingDayHoliday17 = true;
                    }
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay18 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay18 = null;
                            wsd.WorkingHourName18 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay18 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName18 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName18 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 18).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 18)) wsd.WorkingDayHoliday18 = true;
                    }
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay19 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay19 = null;
                            wsd.WorkingHourName19 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay19 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName19 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName19 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 19).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 19)) wsd.WorkingDayHoliday19 = true;
                    }
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay20 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay20 = null;
                            wsd.WorkingHourName20 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay20 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName20 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName20 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 20).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 20)) wsd.WorkingDayHoliday20 = true;
                    }
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay21 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay21 = null;
                            wsd.WorkingHourName21 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay21 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName21 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName21 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 21).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 21)) wsd.WorkingDayHoliday21 = true;
                    }
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay22 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay22 = null;
                            wsd.WorkingHourName22 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay22 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName22 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName22 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 22).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 22)) wsd.WorkingDayHoliday22 = true;
                    }
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay23 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay23 = null;
                            wsd.WorkingHourName23 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay23 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName23 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName23 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 23).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 23)) wsd.WorkingDayHoliday23 = true;
                    }
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay24 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay24 = null;
                            wsd.WorkingHourName24 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay24 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName24 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName24 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 24).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 24)) wsd.WorkingDayHoliday24 = true;
                    }
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay25 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay25 = null;
                            wsd.WorkingHourName25 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay25 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName25 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName25 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 25).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 25)) wsd.WorkingDayHoliday25 = true;
                    }
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay26 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay26 = null;
                            wsd.WorkingHourName26 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay26 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName26 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName26 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 26).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 26)) wsd.WorkingDayHoliday26 = true;
                    }
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay27 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay27 = null;
                            wsd.WorkingHourName27 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay27 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName27 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName27 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 27).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 27)) wsd.WorkingDayHoliday27 = true;
                    }
                    if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay28 == item.Value)
                    {
                        if (cboWorkingHour.SelectedValue == "-1")
                        {
                            wsd.WorkingHourIDDay28 = null;
                            wsd.WorkingHourName29 = string.Empty;
                        }
                        else
                        {
                            wsd.WorkingHourIDDay28 = cboWorkingHour.SelectedValue.ToInt();
                            wsd.WorkingHourName28 = cboWorkingHour.Text;
                        }
                        wsd.WorkingDayName28 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 28).DayOfWeek.ToString();
                        if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 28)) wsd.WorkingDayHoliday28 = true;
                    }
                    if (day >= 29)
                        if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay29 == item.Value)
                        {
                            if (cboWorkingHour.SelectedValue == "-1")
                            {
                                wsd.WorkingHourIDDay29 = null;
                                wsd.WorkingHourName29 = string.Empty;
                            }
                            else
                            {
                                wsd.WorkingHourIDDay29 = cboWorkingHour.SelectedValue.ToInt();
                                wsd.WorkingHourName29 = cboWorkingHour.Text;
                            }
                            wsd.WorkingDayName29 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 29).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 29)) wsd.WorkingDayHoliday29 = true;
                        }
                    if (day >= 30)
                        if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay30 == item.Value)
                        {
                            if (cboWorkingHour.SelectedValue == "-1")
                            {
                                wsd.WorkingHourIDDay30 = null;
                                wsd.WorkingHourName30 = string.Empty;
                            }
                            else
                            {
                                wsd.WorkingHourIDDay30 = cboWorkingHour.SelectedValue.ToInt();
                                wsd.WorkingHourName30 = cboWorkingHour.Text;
                            }
                            wsd.WorkingDayName30 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 30).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 30)) wsd.WorkingDayHoliday30 = true;
                        }
                    if (day >= 31)
                        if (WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay31 == item.Value)
                        {
                            if (cboWorkingHour.SelectedValue == "-1")
                            {
                                wsd.WorkingHourIDDay31 = null;
                                wsd.WorkingHourName31 = string.Empty;
                            }
                            else
                            {
                                wsd.WorkingHourIDDay31 = cboWorkingHour.SelectedValue.ToInt();
                                wsd.WorkingHourName31 = cboWorkingHour.Text;
                            }
                            wsd.WorkingDayName31 = new DateTime(period.SPTYear.ToInt(), period.SPTMonth.ToInt(), 31).DayOfWeek.ToString();
                            if (HolidaySchedules.Any(h => h.HolidayDate?.Day == 31)) wsd.WorkingDayHoliday31 = true;
                        }
                    from++;
                }
            }

            cboDayFrom.SelectedValue = string.Empty;
            cboDayTo.SelectedValue = string.Empty;
            cboWorkingHour.SelectedValue = string.Empty;

            grdWorkingScheduleDetail.Rebind();
        }

        protected void rbtEntryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtEntryType.SelectedValue == "0")
            {
                cboEmployeeName.Enabled = false;
            }
            else
            {
                cboEmployeeName.Enabled = true;
            }
            cboEmployeeName.SelectedValue = string.Empty;
            cboEmployeeName.Text = string.Empty;
        }

        protected void cboWorkingSchedule_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Value)) return;
            cboEmployeeName.Items.Clear();

            var wsd = new WorkingScheduleDetailCollection();
            wsd.Query.Where(wsd.Query.WorkingScheduleID == e.Value.ToInt());
            wsd.Query.Load();

            if (wsd.Any())
            {
                var emps = new VwEmployeeTableCollection();
                emps.Query.Where(emps.Query.PersonID.In(wsd.Select(w => w.PersonID)));
                emps.Query.Load();

                cboEmployeeName.Items.Clear();
                cboEmployeeName.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
                foreach (var emp in emps)
                {
                    cboEmployeeName.Items.Add(new Telerik.Web.UI.RadComboBoxItem(emp.EmployeeName, emp.PersonID.ToString()));
                }
            }
        }

        protected void cboPayrollPeriod_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Value)) return;

            cboWorkingSchedule.Items.Clear();

            var wsc = new WorkingScheduleCollection();
            wsc.Query.Where(wsc.Query.OrganizationUnitID == cboOrganizationUnit.SelectedValue.ToInt() && wsc.Query.PayrollPeriodID == e.Value.ToInt() && wsc.Query.IsApproved == true && wsc.Query.IsVoid == false);
            wsc.Query.OrderBy(wsc.Query.WorkingScheduleID.Ascending);
            wsc.Query.Load();

            cboWorkingSchedule.Items.Clear();
            foreach (var period in wsc)
            {
                var user = new AppUser();
                user.LoadByPrimaryKey(period.LastUpdateUserID);

                cboWorkingSchedule.Items.Add(new Telerik.Web.UI.RadComboBoxItem($"Created by : {user.UserName} ({period.WorkingScheduleID})", period.WorkingScheduleID.ToString()));
            }

            var payroll = new PayrollPeriod();
            payroll.Query.Where(payroll.Query.PayrollPeriodID == e.Value.ToInt());
            payroll.Query.Load();

            //int index = RoleType == "admin" ? 1 : DateTime.Now.Day + 1;
            int index = 1;
            int days = DateTime.DaysInMonth(payroll.SPTYear ?? 0, payroll.SPTMonth ?? 0);

            cboDayFrom.Items.Clear();
            cboDayTo.Items.Clear();
            cboDayFrom.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
            cboDayTo.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));

            while (index <= days)
            {
                cboDayFrom.Items.Add(new Telerik.Web.UI.RadComboBoxItem(index.ToString(), "WorkingHourIDDay" + index.ToString()));
                cboDayTo.Items.Add(new Telerik.Web.UI.RadComboBoxItem(index.ToString(), "WorkingHourIDDay" + index.ToString()));
                index++;
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cboWorkingSchedule.SelectedValue)) return;
            if (rbtEntryType.SelectedValue == "1" && string.IsNullOrEmpty(cboEmployeeName.SelectedValue)) return;

            var emps = new List<string>();
            if (rbtEntryType.SelectedValue == "0")
            {
                foreach (var item in cboEmployeeName.Items.Where(i => !string.IsNullOrEmpty(i.Value)))
                {
                    emps.Add(item.Value);
                }
            }
            else
            {
                emps.Add(cboEmployeeName.SelectedValue);
            }

            foreach (var value in emps)
            {
                var wsd = WorkingScheduleDetails.SingleOrDefault(w => w.WorkingSchduleInterventionID == (int)ViewState["id"] && w.PersonID == value.ToInt());

                if (wsd == null) wsd = WorkingScheduleDetails.AddNew();
                wsd.WorkingSchduleInterventionID = (int)ViewState["id"];
                wsd.PersonID = value.ToInt();

                var emp = new VwEmployeeTable();
                emp.Query.Where(emp.Query.PersonID == wsd.PersonID);
                emp.Query.Load();

                wsd.EmployeeNumber = emp.EmployeeNumber;
                wsd.EmployeeName = emp.EmployeeName;

                //var wsdo = new BusinessObject.WorkingScheduleDetail();
                //wsdo.Query.Where(wsdo.Query.WorkingScheduleID == cboWorkingSchedule.SelectedValue.ToInt() && wsdo.Query.PersonID == value.ToInt());
                //wsdo.Query.Load();

                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay1 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay1)
                //{
                //    wsd.WorkingHourIDDay1 = wsdo.WorkingHourIDDay1;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay1 ?? 0);
                //    wsd.WorkingHourName1 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay2 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay2)
                //{
                //    wsd.WorkingHourIDDay2 = wsdo.WorkingHourIDDay2;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay2 ?? 0);
                //    wsd.WorkingHourName2 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay3 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay3)
                //{
                //    wsd.WorkingHourIDDay3 = wsdo.WorkingHourIDDay3;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay3 ?? 0);
                //    wsd.WorkingHourName3 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay4 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay4)
                //{
                //    wsd.WorkingHourIDDay4 = wsdo.WorkingHourIDDay4;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay4 ?? 0);
                //    wsd.WorkingHourName4 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay5 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay5)
                //{
                //    wsd.WorkingHourIDDay5 = wsdo.WorkingHourIDDay5;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay5 ?? 0);
                //    wsd.WorkingHourName5 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay6 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay6)
                //{
                //    wsd.WorkingHourIDDay6 = wsdo.WorkingHourIDDay6;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay6 ?? 0);
                //    wsd.WorkingHourName6 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay7 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay7)
                //{
                //    wsd.WorkingHourIDDay7 = wsdo.WorkingHourIDDay7;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay7 ?? 0);
                //    wsd.WorkingHourName7 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay8 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay8)
                //{
                //    wsd.WorkingHourIDDay8 = wsdo.WorkingHourIDDay8;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay8 ?? 0);
                //    wsd.WorkingHourName8 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay9 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay9)
                //{
                //    wsd.WorkingHourIDDay9 = wsdo.WorkingHourIDDay9;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay9 ?? 0);
                //    wsd.WorkingHourName9 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay10 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay10)
                //{
                //    wsd.WorkingHourIDDay10 = wsdo.WorkingHourIDDay10;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay10 ?? 0);
                //    wsd.WorkingHourName10 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay11 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay11)
                //{
                //    wsd.WorkingHourIDDay11 = wsdo.WorkingHourIDDay11;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay11 ?? 0);
                //    wsd.WorkingHourName11 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay12 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay12)
                //{
                //    wsd.WorkingHourIDDay12 = wsdo.WorkingHourIDDay12;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay12 ?? 0);
                //    wsd.WorkingHourName12 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay13 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay13)
                //{
                //    wsd.WorkingHourIDDay13 = wsdo.WorkingHourIDDay13;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay13 ?? 0);
                //    wsd.WorkingHourName13 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay14 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay14)
                //{
                //    wsd.WorkingHourIDDay14 = wsdo.WorkingHourIDDay14;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay14 ?? 0);
                //    wsd.WorkingHourName14 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay15 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay15)
                //{
                //    wsd.WorkingHourIDDay15 = wsdo.WorkingHourIDDay15;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay15 ?? 0);
                //    wsd.WorkingHourName15 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay16 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay16)
                //{
                //    wsd.WorkingHourIDDay16 = wsdo.WorkingHourIDDay16;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay16 ?? 0);
                //    wsd.WorkingHourName16 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay17 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay17)
                //{
                //    wsd.WorkingHourIDDay17 = wsdo.WorkingHourIDDay17;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay17 ?? 0);
                //    wsd.WorkingHourName17 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay18 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay18)
                //{
                //    wsd.WorkingHourIDDay18 = wsdo.WorkingHourIDDay18;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay18 ?? 0);
                //    wsd.WorkingHourName18 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay19 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay19)
                //{
                //    wsd.WorkingHourIDDay19 = wsdo.WorkingHourIDDay19;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay19 ?? 0);
                //    wsd.WorkingHourName19 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay20 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay20)
                //{
                //    wsd.WorkingHourIDDay20 = wsdo.WorkingHourIDDay20;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay20 ?? 0);
                //    wsd.WorkingHourName20 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay21 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay21)
                //{
                //    wsd.WorkingHourIDDay21 = wsdo.WorkingHourIDDay21;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay21 ?? 0);
                //    wsd.WorkingHourName21 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay22 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay22)
                //{
                //    wsd.WorkingHourIDDay22 = wsdo.WorkingHourIDDay22;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay22 ?? 0);
                //    wsd.WorkingHourName22 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay23 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay23)
                //{
                //    wsd.WorkingHourIDDay23 = wsdo.WorkingHourIDDay23;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay23 ?? 0);
                //    wsd.WorkingHourName23 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay24 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay24)
                //{
                //    wsd.WorkingHourIDDay24 = wsdo.WorkingHourIDDay24;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay24 ?? 0);
                //    wsd.WorkingHourName24 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay25 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay25)
                //{
                //    wsd.WorkingHourIDDay25 = wsdo.WorkingHourIDDay25;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay25 ?? 0);
                //    wsd.WorkingHourName25 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay26 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay26)
                //{
                //    wsd.WorkingHourIDDay26 = wsdo.WorkingHourIDDay26;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay26 ?? 0);
                //    wsd.WorkingHourName26 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay27 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay27)
                //{
                //    wsd.WorkingHourIDDay27 = wsdo.WorkingHourIDDay27;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay27 ?? 0);
                //    wsd.WorkingHourName27 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay28 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay28)
                //{
                //    wsd.WorkingHourIDDay28 = wsdo.WorkingHourIDDay28;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay28 ?? 0);
                //    wsd.WorkingHourName28 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay29 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay29)
                //{
                //    wsd.WorkingHourIDDay29 = wsdo.WorkingHourIDDay29;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay29 ?? 0);
                //    wsd.WorkingHourName29 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay30 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay30)
                //{
                //    wsd.WorkingHourIDDay30 = wsdo.WorkingHourIDDay30;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay30 ?? 0);
                //    wsd.WorkingHourName30 = wh.WorkingHourName;
                //}
                //if (WorkingScheduleDetailMetadata.ColumnNames.WorkingHourIDDay31 == WorkingSchduleInterventionDetailMetadata.ColumnNames.WorkingHourIDDay31)
                //{
                //    wsd.WorkingHourIDDay31 = wsdo.WorkingHourIDDay31;
                //    var wh = new WorkingHour();
                //    wh.LoadByPrimaryKey(wsdo.WorkingHourIDDay31 ?? 0);
                //    wsd.WorkingHourName31 = wh.WorkingHourName;
                //}
            }


            cboDayFrom.SelectedValue = string.Empty;
            cboDayTo.SelectedValue = string.Empty;
            cboWorkingHour.SelectedValue = string.Empty;

            grdWorkingScheduleDetail.Rebind();

        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new WorkingSchduleIntervention();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(ViewState["id"].ToString())))
            {
                if (RoleType != "admin")
                {
                    var ws = new WorkingSchedule();
                    ws.LoadByPrimaryKey(entity.WorkingScheduleID ?? -1);

                    var period = new PayrollPeriod();
                    period.LoadByPrimaryKey(ws.PayrollPeriodID ?? -1);

                    var periodDate = new DateTime(period.SPTYear ?? 0, period.SPTMonth ?? 0, 1);
                    var nowDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    if (nowDate != periodDate)
                    {
                        args.MessageText = "Period schedule is closed";
                        args.IsCancel = true;
                        return;
                    }
                }

                entity.IsApproved = true;
                entity.IsVoid = false;
                entity.LastUpdateUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.Save();
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new WorkingSchduleIntervention();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(ViewState["id"].ToString())))
            {
                if (RoleType != "admin")
                {
                    var ws = new WorkingSchedule();
                    ws.LoadByPrimaryKey(entity.WorkingScheduleID ?? -1);

                    var period = new PayrollPeriod();
                    period.LoadByPrimaryKey(ws.PayrollPeriodID ?? -1);

                    var periodDate = new DateTime(period.SPTYear ?? 0, period.SPTMonth ?? 0, 1);
                    var nowDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    if (nowDate != periodDate)
                    {
                        args.MessageText = "Period schedule is closed";
                        args.IsCancel = true;
                        return;
                    }
                }

                entity.IsApproved = false;
                entity.IsVoid = false;
                entity.LastUpdateUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.Save();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new WorkingSchduleIntervention();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(ViewState["id"].ToString())))
            {
                entity.IsApproved = false;
                entity.IsVoid = true;
                entity.LastUpdateUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.Save();
            }
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !(bool)ViewState["IsApproved"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new WorkingSchduleIntervention();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(ViewState["id"].ToString())))
            {
                if (!IsApprovedOrVoid(entity, args))
                    return;
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        private bool IsApprovedOrVoid(WorkingSchduleIntervention entity, ValidateArgs args)
        {
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            btnInsert.Enabled = isVisible;
            btnLoad.Enabled = isVisible;
        }
    }
}