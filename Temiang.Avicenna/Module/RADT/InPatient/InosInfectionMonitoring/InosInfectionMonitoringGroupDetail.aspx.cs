using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class InosInfectionMonitoringGroupDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "##";
            UrlPageList = "InosInfectionMonitoringList.aspx";

            ProgramID = AppConstant.Program.INOSInfectionMonitoring;

            if (!IsPostBack)
            {
                txtMonitoringDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            ToolBarMenuSearch.Visible = false;
            ToolBarMenuDelete.Visible = false;
            ToolBarMenuAdd.Visible = false;
            //ToolBarMenuEdit.Visible = false;
            ToolBarMenuMoveNext.Visible = false;
            ToolBarMenuMovePrev.Visible = false;
            ToolBarMenuVoid.Visible = false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ServiceRoom());

            txtMonitoringDate.SelectedDate = (new DateTime()).NowAtSqlServer();
        }

        protected override void OnMenuEditClick()
        {
            txtMonitoringDate.Enabled = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            SaveEntity();
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            SaveEntity();
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

        #endregion

        #region ToolBar Menu Support

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshGrid();
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ServiceRoom();
            if (parameters.Length > 0)
            {
                var id = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(id);
            }
            else
                entity.LoadByPrimaryKey(txtRoomID.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var room = (ServiceRoom)entity;

            txtRoomID.Text = room.RoomID;
            lblRoomName.Text = room.RoomName;
            txtServiceUnitID.Text = room.ServiceUnitID;
            var unit = new ServiceUnit();
            if (unit.LoadByPrimaryKey(txtServiceUnitID.Text))
                lblServiceUnitName.Text = unit.ServiceUnitName;
            else lblServiceUnitName.Text = string.Empty;

            if (IsPostBack)
            {
                RefreshGrid();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
        }

        #endregion

        #region Private Method Standard

        private void SaveEntity()
        {
            using (var trans = new esTransactionScope())
            {
                foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
                {
                    string regNo = dataItem.GetDataKeyValue("RegistrationNo").ToString();
                    var nestedView = dataItem.ChildItem;
                    bool isMechanicalVentilator = ((CheckBox)nestedView.FindControl("chkIsMechanicalVentilator")).Checked;
                    bool isInpatient = ((CheckBox)nestedView.FindControl("chkIsInpatient")).Checked;
                    bool isUrineCatheter = ((CheckBox)nestedView.FindControl("chkIsUrineCatheter")).Checked;
                    bool isSurgery = ((CheckBox)nestedView.FindControl("chkIsSurgery")).Checked;
                    bool isCentralVeinLine = ((CheckBox)nestedView.FindControl("chkIsCentralVeinLine")).Checked;
                    bool isIntraVeinLine = ((CheckBox)nestedView.FindControl("chkIsIntraVeinLine")).Checked;
                    bool isTotalCare = ((CheckBox)nestedView.FindControl("chkIsTotalCare")).Checked;
                    bool isAntibioticDrugs = ((CheckBox)nestedView.FindControl("chkIsAntibioticDrugs")).Checked;
                    bool isVAP = ((CheckBox)nestedView.FindControl("chkIsVAP")).Checked;
                    bool isHAP = ((CheckBox)nestedView.FindControl("chkIsHAP")).Checked;
                    bool isISK = ((CheckBox)nestedView.FindControl("chkIsISK")).Checked;
                    bool isILO = ((CheckBox)nestedView.FindControl("chkIsILO")).Checked;
                    bool isIADP = ((CheckBox)nestedView.FindControl("chkIsIADP")).Checked;
                    bool isPhlebitis = ((CheckBox)nestedView.FindControl("chkIsPhlebitis")).Checked;
                    bool isDecubitus = ((CheckBox)nestedView.FindControl("chkIsDecubitus")).Checked;

                    bool isVoid = ((CheckBox)nestedView.FindControl("chkIsVoid")).Checked;

                    if (!isVoid)
                    {
                        var entity = new INOSInfectionMonitoring();
                        entity.Query.Where(entity.Query.RegistrationNo == regNo, entity.Query.MonitoringDate == txtMonitoringDate.SelectedDate);
                        if (!entity.Query.Load())
                        {
                            entity = new INOSInfectionMonitoring();

                            var reg = new Registration();
                            reg.LoadByPrimaryKey(regNo);
                            entity.BedID = reg.BedID;
                        }

                        entity.RegistrationNo = regNo;
                        entity.MonitoringDate = txtMonitoringDate.SelectedDate;
                        entity.IsMechanicalVentilator = isMechanicalVentilator;
                        entity.IsInpatient = isInpatient;
                        entity.IsUrineCatheter = isUrineCatheter;
                        entity.IsSurgery = isSurgery;
                        entity.IsCentralVeinLine = isCentralVeinLine;
                        entity.IsIntraVeinLine = isIntraVeinLine;
                        entity.IsTotalCare = isTotalCare;
                        entity.IsAntibioticDrugs = isAntibioticDrugs;
                        entity.IsVAP = isVAP;
                        entity.IsHAP = isHAP;
                        entity.IsISK = isISK;
                        entity.IsILO = isILO;
                        entity.IsIADP = isIADP;
                        entity.IsPhlebitis = isPhlebitis;
                        entity.IsDecubitus = isDecubitus;

                        entity.IsVoid = isVoid;

                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        entity.LastUpdateDateTime = DateTime.Now;

                        entity.Save();
                    }
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ServiceRoomQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.RoomID == txtRoomID.Text
                    );
                que.OrderBy(que.RoomID.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.RoomID == txtRoomID.Text
                    );
                que.OrderBy(que.RoomID.Descending);
            }

            var entity = new ServiceRoom();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region detil

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Registrations;
        }

        private DataTable Registrations
        {
            get
            {
                object obj = this.Session["INOSInfectionMonitorings" + Request.UserHostName];
                if (obj != null)
                    return ((DataTable)(obj));

                var query = new RegistrationQuery("a");
                var patient = new PatientQuery("b");
                var par = new ParamedicQuery("c");
                var guar = new GuarantorQuery("d");
                var sal = new AppStandardReferenceItemQuery("e");
                var inos = new INOSInfectionMonitoringQuery("aa");

                query.Select
                    (
                        query.RegistrationNo,
                        query.RegistrationDate,
                        query.RegistrationTime,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        query.RoomID,
                        query.BedID,
                        query.ParamedicID,
                        par.ParamedicName,
                        query.GuarantorID,
                        guar.GuarantorName,
                        sal.ItemName.As("SalutationName"),
                        @"<DATEDIFF(DAY, a.RegistrationDate, GETDATE())+1 AS Los>",

                        @"<ISNULL(aa.IsMechanicalVentilator, 0) AS IsMechanicalVentilator>",
                        @"<ISNULL(aa.IsInpatient, 0) AS IsInpatient>",
                        @"<ISNULL(aa.IsUrineCatheter, 0) AS IsUrineCatheter>",
                        @"<ISNULL(aa.IsSurgery, 0) AS IsSurgery>",
                        @"<ISNULL(aa.IsCentralVeinLine, 0) AS IsCentralVeinLine>",
                        @"<ISNULL(aa.IsIntraVeinLine, 0) AS IsIntraVeinLine>",
                        @"<ISNULL(aa.IsTotalCare, 0) AS IsTotalCare>",
                        @"<ISNULL(aa.IsAntibioticDrugs, 0) AS IsAntibioticDrugs>",
                        @"<ISNULL(aa.IsVAP, 0) AS IsVAP>",
                        @"<ISNULL(aa.IsHAP, 0) AS IsHAP>",
                        @"<ISNULL(aa.IsISK, 0) AS IsISK>",
                        @"<ISNULL(aa.IsILO, 0) AS IsILO>",
                        @"<ISNULL(aa.IsIADP, 0) AS IsIADP>",
                        @"<ISNULL(aa.IsPhlebitis, 0) AS IsPhlebitis>",
                        @"<ISNULL(aa.IsDecubitus, 0) AS IsDecubitus>",
                        @"<ISNULL(aa.IsVoid, 0) AS IsVoid>",

                        @"<CASE WHEN ISNULL(aa.IsVoid, 0) = 0 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsEnabled>",
                        @"<CASE WHEN ISNULL(aa.IsVoid, 0) = 0 THEN '' ELSE '-VOID-' END AS CaptionVoid>"
                    );

                query.InnerJoin(patient).On(patient.PatientID == query.PatientID);
                query.LeftJoin(par).On(par.ParamedicID == query.ParamedicID);
                query.LeftJoin(guar).On(guar.GuarantorID == query.GuarantorID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" && sal.ItemID == patient.SRSalutation);
                query.LeftJoin(inos).On(inos.RegistrationNo == query.RegistrationNo && inos.MonitoringDate == txtMonitoringDate.SelectedDate);
                query.Where
                    (
                        query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                        query.RoomID == txtRoomID.Text,
                        query.IsVoid == false,
                        query.IsClosed == false,
                        query.Or(query.DischargeDate.IsNull(), query.SRDischargeMethod == string.Empty)
                    );

                query.OrderBy(query.RegistrationNo.Ascending);

                DataTable dtb = query.LoadDataTable();

                if (DataModeCurrent == AppEnum.DataMode.Read)
                {
                    foreach (DataRow row in dtb.Rows)
                    {
                        row["IsEnabled"] = false;
                    }
                    dtb.AcceptChanges();
                }

                Session["INOSInfectionMonitorings" + Request.UserHostName] = dtb;
                return dtb;
            }
        }

        private void RefreshGrid()
        {
            Session["INOSInfectionMonitorings" + Request.UserHostName] = null;
            grdList.Rebind();
        }
        #endregion
    }
}