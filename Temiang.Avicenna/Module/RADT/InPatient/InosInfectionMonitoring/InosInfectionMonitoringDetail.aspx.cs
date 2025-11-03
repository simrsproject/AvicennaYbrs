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
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class InosInfectionMonitoringDetail : BasePageDetail
    {
        #region Page Event & Initialize
        private string RegNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "##";
            UrlPageList = "InosInfectionMonitoringList.aspx";

            ProgramID = AppConstant.Program.INOSInfectionMonitoring;

            if (!IsPostBack)
            {
                PopulateRegistrationInformation(RegNo);
                txtMonitoringDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            ToolBarMenuSearch.Visible = false;
            ToolBarMenuDelete.Visible = false;
            ToolBarMenuAdd.Visible = false;
            ToolBarMenuMoveNext.Visible = false;
            ToolBarMenuMovePrev.Visible = false;
        }

        private void PopulateRegistrationInformation(string registrationNo)
        {
            var registration = new Registration();
            if (registration.LoadByPrimaryKey(registrationNo))
            {
                txtRegistrationNo.Text = registrationNo;
                txtRegistrationDate.SelectedDate = registration.RegistrationDate;

                var x = registration.DischargeDate != null ? registration.DischargeDate.Value.Date : (new DateTime()).NowAtSqlServer().Date;
                var y = registration.RegistrationDate.Value.Date;
                txtLengthOfStay.Value = (x - y).TotalDays == 0 ? 1 : (x - y).TotalDays + 1;

                var patient = new Patient();
                if (patient.LoadByPrimaryKey(registration.PatientID))
                {
                    txtMedicalNo.Text = patient.MedicalNo;
                    var std = new AppStandardReferenceItem();
                    txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.SRSalutation) ? std.ItemName : string.Empty;
                    txtPatientName.Text = patient.PatientName;
                    txtGender.Text = patient.Sex;
                    txtPlaceDOB.Text = string.Format("{0}, {1}", patient.CityOfBirth, Convert.ToDateTime(patient.DateOfBirth).ToString("dd-MMM-yyyy"));
                    txtAgeDay.Value = Helper.GetAgeInDay(patient.DateOfBirth.Value);
                    txtAgeMonth.Value = Helper.GetAgeInMonth(patient.DateOfBirth.Value);
                    txtAgeYear.Value = Helper.GetAgeInYear(patient.DateOfBirth.Value);
                }
                else
                {
                    txtMedicalNo.Text = string.Empty;
                    txtSalutation.Text = string.Empty;
                    txtPatientName.Text = string.Empty;
                    txtGender.Text = string.Empty;
                    txtPlaceDOB.Text = string.Empty;
                    txtAgeDay.Value = 0;
                    txtAgeMonth.Value = 0;
                    txtAgeYear.Value = 0;
                }

                txtParamedicID.Text = registration.ParamedicID;
                var par = new Paramedic();
                par.LoadByPrimaryKey(txtParamedicID.Text);
                lblParamedicName.Text = par.ParamedicName;

                txtServiceUnitID.Text = registration.ServiceUnitID;
                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(txtServiceUnitID.Text);
                lblServiceUnitName.Text = unit.ServiceUnitName;
                txtRoomID.Text = registration.RoomID;

                var room = new ServiceRoom();
                room.LoadByPrimaryKey(txtRoomID.Text);
                lblRoomName.Text = room.RoomName;

                txtBedID.Text = registration.BedID;

                txtGuarantorID.Text = registration.GuarantorID;
                var guar = new Guarantor();
                guar.LoadByPrimaryKey(txtGuarantorID.Text);
                lblGuarantorName.Text = guar.GuarantorName;
            }
            else
            {
                txtMedicalNo.Text = string.Empty;
                txtSalutation.Text = string.Empty;
                txtPatientName.Text = string.Empty;
                txtGender.Text = string.Empty;
                txtPlaceDOB.Text = string.Empty;

                txtAgeDay.Value = 0;
                txtAgeMonth.Value = 0;
                txtAgeYear.Value = 0;

                txtParamedicID.Text = string.Empty;
                lblParamedicName.Text = string.Empty;

                txtServiceUnitID.Text = string.Empty;
                lblServiceUnitName.Text = string.Empty;
                txtRoomID.Text = string.Empty;
                lblRoomName.Text = string.Empty;
                txtBedID.Text = registration.BedID;
                txtGuarantorID.Text = registration.GuarantorID;
                lblGuarantorName.Text = string.Empty;
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            PopulateRegistrationInformation(string.IsNullOrEmpty(txtRegistrationNo.Text)
                                                    ? RegNo
                                                    : txtRegistrationNo.Text);
            txtMonitoringID.Value = 0;
            txtMonitoringDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            ViewState["IsVoid"] = false;
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
            var coll = new INOSInfectionMonitoringCollection();
            coll.Query.Where(coll.Query.RegistrationNo == txtRegistrationNo.Text, coll.Query.MonitoringDate == txtMonitoringDate.SelectedDate);
            coll.LoadAll();
            if (coll.Count > 0)
            {
                args.MessageText = "There is already data with the selected date.";
                args.IsCancel = true;
                return;
            }

            var entity = new INOSInfectionMonitoring();
            entity.AddNew();

            SetEntityValue(entity);
            entity.BedID = txtBedID.Text;
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new INOSInfectionMonitoring();
            if (entity.LoadByPrimaryKey(Convert.ToInt64(txtMonitoringID.Value)))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
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
            //auditLogFilter.PrimaryKeyData = string.Format("MonitoringID='{0}'", txtMonitoringID.Value.ToString().Trim());
            //auditLogFilter.TableName = "INOSInfectionMonitoring";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_MonitoringID", txtMonitoringID.Value.ToString());
            printJobParameters.AddNew("p_UserID", AppSession.UserLogin.UserID);
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {

        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new INOSInfectionMonitoring();
            if (parameters.Length > 0)
            {
                var id = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt64(id));
            }
            else
                entity.LoadByPrimaryKey(Convert.ToInt64(txtMonitoringID.Value));

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var iim = (INOSInfectionMonitoring)entity;

            txtMonitoringID.Value = iim.MonitoringID;
            txtRegistrationNo.Text = iim.RegistrationNo;
            PopulateRegistrationInformation(txtRegistrationNo.Text);

            txtMonitoringDate.SelectedDate = iim.MonitoringDate;
            chkIsMechanicalVentilator.Checked = iim.IsMechanicalVentilator ?? false;
            chkIsInpatient.Checked = iim.IsInpatient ?? false;
            chkIsUrineCatheter.Checked = iim.IsUrineCatheter ?? false;
            chkIsSurgery.Checked = iim.IsSurgery ?? false;
            chkIsCentralVeinLine.Checked = iim.IsCentralVeinLine ?? false;
            chkIsIntraVeinLine.Checked = iim.IsIntraVeinLine ?? false;
            chkIsTotalCare.Checked = iim.IsTotalCare ?? false;
            chkIsAntibioticDrugs.Checked = iim.IsAntibioticDrugs ?? false;
            chkIsVAP.Checked = iim.IsVAP ?? false;
            chkIsHAP.Checked = iim.IsHAP ?? false;
            chkIsISK.Checked = iim.IsISK ?? false;
            chkIsILO.Checked = iim.IsILO ?? false;
            chkIsIADP.Checked = iim.IsIADP ?? false;
            chkIsPhlebitis.Checked = iim.IsPhlebitis ?? false;
            chkIsDecubitus.Checked = iim.IsDecubitus ?? false;

            ViewState["IsVoid"] = iim.IsVoid ?? false;
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new INOSInfectionMonitoring();
            if (!entity.LoadByPrimaryKey(Convert.ToInt64(txtMonitoringID.Value)))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, true);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            var entity = new INOSInfectionMonitoring();
            if (!entity.LoadByPrimaryKey(Convert.ToInt64(txtMonitoringID.Value)))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private void SetVoid(INOSInfectionMonitoring entity, bool isVoid)
        {
            //header
            entity.IsVoid = isVoid;
            entity.VoidDateTime = (new DateTime()).NowAtSqlServer();
            entity.VoidByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

            entity.Save();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(INOSInfectionMonitoring entity)
        {
            entity.MonitoringID = Convert.ToInt64(txtMonitoringID.Value);
            entity.RegistrationNo = txtRegistrationNo.Text;
            entity.MonitoringDate = txtMonitoringDate.SelectedDate;
            entity.IsMechanicalVentilator = chkIsMechanicalVentilator.Checked;
            entity.IsInpatient = chkIsInpatient.Checked;
            entity.IsUrineCatheter = chkIsUrineCatheter.Checked;
            entity.IsSurgery = chkIsSurgery.Checked;
            entity.IsCentralVeinLine = chkIsCentralVeinLine.Checked;
            entity.IsIntraVeinLine = chkIsIntraVeinLine.Checked;
            entity.IsTotalCare = chkIsTotalCare.Checked;
            entity.IsAntibioticDrugs = chkIsAntibioticDrugs.Checked;
            entity.IsVAP = chkIsVAP.Checked;
            entity.IsHAP = chkIsHAP.Checked;
            entity.IsISK = chkIsISK.Checked;
            entity.IsILO = chkIsILO.Checked;
            entity.IsIADP = chkIsIADP.Checked;
            entity.IsPhlebitis = chkIsPhlebitis.Checked;
            entity.IsDecubitus = chkIsDecubitus.Checked;

            entity.IsVoid = false;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        }

        private void SaveEntity(INOSInfectionMonitoring entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();

                txtMonitoringID.Value = entity.MonitoringID;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new INOSInfectionMonitoringQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.MonitoringID == txtMonitoringID.Value
                    );
                que.OrderBy(que.MonitoringID.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.MonitoringID == txtMonitoringID.Value
                    );
                que.OrderBy(que.MonitoringID.Descending);
            }

            var entity = new INOSInfectionMonitoring();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion
    }
}