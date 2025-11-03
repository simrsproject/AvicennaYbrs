using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.RADT.OutPatient
{
    public partial class DetailOpRegistrationAndDischargeDetail : BasePageDialog
    {
        private AppAutoNumberLast _autoNumber;

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.DetailRegistrationOpDischarge;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRDischargeCondition, AppEnum.StandardReference.DischargeCondition, AppConstant.RegistrationType.OutPatient);
                StandardReference.InitializeIncludeSpace(cboSRDischargeMethod, AppEnum.StandardReference.DischargeMethod, AppConstant.RegistrationType.OutPatient);
                StandardReference.InitializeIncludeSpace(cboCovidStatus, AppEnum.StandardReference.CovidStatus);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtRegistrationNo.Text = Request.QueryString["regno"];
                var reg = new Registration();
                reg.LoadByPrimaryKey(txtRegistrationNo.Text);
                txtRegistrationDate.SelectedDate = reg.RegistrationDate;
                txtRegistrationTime.Text = reg.RegistrationTime;

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                txtMedicalNo.Text = pat.MedicalNo;
                txtPatientName.Text = pat.PatientName;
                hdnPatientID.Value = reg.PatientID;

                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;
                txtGender.Text = pat.Sex;
                txtPlaceDOB.Text = string.Format("{0}, {1}", pat.CityOfBirth, Convert.ToDateTime(pat.DateOfBirth).ToString("dd-MMM-yyyy"));

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(reg.ServiceUnitID);
                txtServiceUnitName.Text = unit.ServiceUnitName;

                if (!string.IsNullOrEmpty(reg.EmrDiagnoseID))
                {
                    var query = new EmergencyDiagnoseQuery();
                    query.Where(query.EmrDiagnoseID == reg.EmrDiagnoseID);
                    cboEmrDiagnoseID.DataSource = query.LoadDataTable();
                    cboEmrDiagnoseID.DataBind();

                    cboEmrDiagnoseID.SelectedValue = reg.EmrDiagnoseID;
                }
                txtInitialDiagnose.Text = reg.InitialDiagnose;
                if (reg.DischargeDate == null)
                {
                    txtDischargeDate.SelectedDate = reg.RegistrationDate;
                    txtDischargeTime.Text = reg.RegistrationTime;
                }
                else
                {
                    txtDischargeDate.SelectedDate = reg.DischargeDate;
                    txtDischargeTime.Text = reg.DischargeTime;    
                }
                
                txtDischargeMedicalNotes.Text = reg.DischargeMedicalNotes;
                txtDischargeNotes.Text = reg.DischargeNotes;
                cboSRDischargeCondition.SelectedValue = reg.SRDischargeCondition;
                cboSRDischargeMethod.SelectedValue = reg.SRDischargeMethod;
                txtDeathCertificateNo.Text = reg.DeathCertificateNo;

                chkIsOldCase.Checked = reg.IsOldCase ?? false;
                chkIsDHF.Checked = reg.IsDHF ?? false;
                chkIsEKG.Checked = reg.IsEKG ?? false;
                txtReferTo.Text = reg.ReferTo;

                cboCovidStatus.SelectedValue = reg.SRCovidStatus == null ? string.Empty : reg.SRCovidStatus.ToString();
            }
        }

        public override bool OnButtonOkClicked()
        {
            Validate();

            if (!IsValid)
                return false;

            using (esTransactionScope trans = new esTransactionScope())
            {
                var entity = new Registration();
                entity.LoadByPrimaryKey(txtRegistrationNo.Text);

                entity.SRCovidStatus = string.IsNullOrEmpty(cboCovidStatus.SelectedValue) ? Convert.ToByte(0) : byte.Parse(cboCovidStatus.SelectedValue);
                entity.DischargeDate = txtDischargeDate.SelectedDate;
                entity.DischargeTime = txtDischargeTime.TextWithLiterals;
                entity.DischargeMedicalNotes = txtDischargeMedicalNotes.Text;
                entity.DischargeNotes = txtDischargeNotes.Text;
                entity.SRDischargeCondition = cboSRDischargeCondition.SelectedValue;
                entity.SRDischargeMethod = cboSRDischargeMethod.SelectedValue;
                entity.DischargeOperatorID = AppSession.UserLogin.UserID;

                entity.EmrDiagnoseID = cboEmrDiagnoseID.SelectedValue;
                entity.InitialDiagnose = txtInitialDiagnose.Text;
                entity.IsOldCase = chkIsOldCase.Checked;
                entity.IsDHF = chkIsDHF.Checked;
                entity.IsEKG = chkIsEKG.Checked;
                entity.ReferTo = txtReferTo.Text;

                var std = new AppStandardReferenceItem();
                if (std.LoadByPrimaryKey(AppEnum.StandardReference.DischargeCondition.ToString(), entity.SRDischargeCondition) && std.Note == "+")
                {
                    if (string.IsNullOrEmpty(txtDeathCertificateNo.Text))
                    {
                        txtDeathCertificateNo.Text = GetNewDeathCertificateNo();
                        _autoNumber.LastCompleteNumber = txtDeathCertificateNo.Text;
                        _autoNumber.Save();
                    }

                    entity.DeathCertificateNo = txtDeathCertificateNo.Text;
                }
                else
                    entity.DeathCertificateNo = string.Empty;

                //insert diagnosa Detail Registration
                if (!string.IsNullOrEmpty(entity.InitialDiagnose) && AppSession.Parameter.HealthcareInitialAppsVersion == "RSCDR")
                {
                    var entity2 = new EpisodeDiagnose();
                    if (!entity2.LoadByPrimaryKey(entity.RegistrationNo, "000"))
                    {
                        entity2.AddNew();
                        entity2.RegistrationNo = entity.RegistrationNo;
                        entity2.SequenceNo = "000"; 
                        entity2.DiagnoseID = "";
                        entity2.SRDiagnoseType = AppSession.Parameter.DiagnoseTypeInitial; //"DiagnoseType-006";
                        entity2.DiagnosisText = "";
                        entity2.MorphologyID = "";
                        entity2.ParamedicID = entity.ParamedicID;
                        entity2.IsAcuteDisease = false;
                        entity2.IsChronicDisease = false;
                        entity2.IsOldCase = false;
                        entity2.IsConfirmed = false;
                        entity2.IsVoid = false;
                        entity2.Notes = entity.InitialDiagnose; //?
                        entity2.ExternalCauseID = "";
                        entity2.CreateByUserID = AppSession.UserLogin.UserID;
                        entity2.CreateDateTime = (new DateTime()).NowAtSqlServer();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(entity2.DiagnoseID) && entity2.IsConfirmed == false)
                        {
                            entity2.Notes = entity.InitialDiagnose;
                        }
                    }
                    entity2.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    entity2.LastUpdateByUserID = AppSession.UserLogin.UserID;

                    entity2.Save();
                }

                entity.Save();

                bool isAllowAppointment = (cboSRDischargeCondition.SelectedValue != AppSession.Parameter.DischargeConditionDie && cboSRDischargeCondition.SelectedValue != AppSession.Parameter.DischargeConditionDieLessThen48 && cboSRDischargeCondition.SelectedValue != AppSession.Parameter.DischargeConditionDieMoreThen48);
                if (isAllowAppointment)
                {
                    var pat = new Patient();
                    pat.LoadByPrimaryKey(entity.PatientID);

                    foreach (var appt in PatientDischargeAppointments)
                    {
                        if (appt.IsProcessed == false)
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(appt.QueNo))
                                {
                                    string value = appt.QueNo.Split('-')[1].Substring(1);
                                    DateTime.TryParse(value, out DateTime dt);

                                    var apt = Temiang.Avicenna.WebService.V1_1.AppointmentWS.AppointmentSetEntityValue(string.Empty, appt.ServiceUnitID, appt.ParamedicID,
                                        appt.AppointmentDate.Value.ToShortDateString(), dt.ToString("HH:mm"), string.Empty,
                                        entity.PatientID, pat.FirstName, pat.MiddleName, pat.LastName, pat.DateOfBirth.Value.Date.ToShortDateString(), pat.CityOfBirth, pat.Sex,
                                        pat.StreetName, pat.District, pat.City, pat.County, pat.State, pat.ZipCode,
                                        pat.PhoneNo, pat.Email, pat.Ssn, pat.GuarantorID, appt.Notes, AppSession.Parameter.AppointmentStatusOpen,
                                        pat.MobilePhoneNo, pat.GuarantorCardNo, "", 0, AppSession.UserLogin.UserID, AppSession.Parameter.AppointmentTypeControlPlan, txtRegistrationNo.Text);
                                }
                                else
                                {
                                    var apt = Temiang.Avicenna.WebService.V1_1.AppointmentWS.AppointmentSetEntityValue(string.Empty, appt.ServiceUnitID, appt.ParamedicID,
                                        appt.AppointmentDate.Value.Date.ToShortDateString(), string.Empty, string.Empty,
                                        entity.PatientID, pat.FirstName, pat.MiddleName, pat.LastName, pat.DateOfBirth.Value.Date.ToShortDateString(), pat.CityOfBirth, pat.Sex,
                                        pat.StreetName, pat.District, pat.City, pat.County, pat.State, pat.ZipCode,
                                        pat.PhoneNo, pat.Email, pat.Ssn, pat.GuarantorID, appt.Notes, AppSession.Parameter.AppointmentStatusOpen,
                                        pat.MobilePhoneNo, pat.GuarantorCardNo, "", 0, AppSession.UserLogin.UserID, AppSession.Parameter.AppointmentTypeControlPlan, txtRegistrationNo.Text);
                                }
                            }
                            catch (Exception ex)
                            {
                                ShowInformationHeader(ex.Message);
                                return false;
                            }

                            appt.IsProcessed = true;
                        }
                    }

                    PatientDischargeAppointments.Save();
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        protected void cboEmrDiagnoseID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmrDiagnoseName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["EmrDiagnoseID"].ToString();
        }

        protected void cboEmrDiagnoseID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new EmergencyDiagnoseQuery();
            query.es.Top = 20;
            query.Where
                (
                    query.EmrDiagnoseName.Like(searchTextContain),
                    query.IsActive == true
                );
            query.OrderBy(query.EmrDiagnoseName.Ascending);

            cboEmrDiagnoseID.DataSource = query.LoadDataTable();
            cboEmrDiagnoseID.DataBind();
        }

        private string GetNewDeathCertificateNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer(), AppEnum.AutoNumber.DeathCertificateNo);
            return _autoNumber.LastCompleteNumber;
        }

        #region Appointment

        private PatientDischargeAppointmentCollection PatientDischargeAppointments
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPatientDischargeAppointment" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((PatientDischargeAppointmentCollection)(obj));
                    }
                }

                var coll = new PatientDischargeAppointmentCollection();
                var query = new PatientDischargeAppointmentQuery("a");
                var suq = new ServiceUnitQuery("b");
                var parq = new ParamedicQuery("c");
                var roomq = new ServiceRoomQuery("d");

                query.InnerJoin(suq).On(suq.ServiceUnitID == query.ServiceUnitID);
                query.InnerJoin(parq).On(parq.ParamedicID == query.ParamedicID);
                query.InnerJoin(roomq).On(roomq.RoomID == query.RoomID);
                query.Select
                    (
                        query,
                        suq.ServiceUnitName.As("refToServiceUnit_ServiceUnitName"),
                        parq.ParamedicName.As("refToParamedic_ParamedicName"),
                        roomq.RoomName.As("refToServiceRoom_RoomName")
                    );
                query.Where(query.RegistrationNo == txtRegistrationNo.Text);
                coll.Load(query);

                Session["collPatientDischargeAppointment" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collPatientDischargeAppointment" + Request.UserHostName] = value;
            }
        }

        protected void grdAppt_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdAppt.DataSource = PatientDischargeAppointments;
        }

        protected void grdAppt_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String parId =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        PatientDischargeAppointmentMetadata.ColumnNames.ParamedicID]);
            PatientDischargeAppointment entity = FindItem(parId);
            if (entity != null)
                SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                PatientDischargeAppointments.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected void grdAppt_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String parId =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][
                        PatientDischargeAppointmentMetadata.ColumnNames.ParamedicID]);
            PatientDischargeAppointment entity = FindItem(parId);
            if (entity != null && entity.IsProcessed == false)
            {
                entity.MarkAsDeleted();
                PatientDischargeAppointments.Save();
            }
        }

        protected void grdAppt_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = PatientDischargeAppointments.AddNew();
            SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                PatientDischargeAppointments.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            ////Stay in insert mode
            //e.Canceled = true;
            e.Canceled = false;
            grdAppt.Rebind();
        }

        private PatientDischargeAppointment FindItem(String paramedicId)
        {
            PatientDischargeAppointmentCollection coll = PatientDischargeAppointments;
            PatientDischargeAppointment retEntity = null;
            foreach (PatientDischargeAppointment rec in coll)
            {
                if (rec.ParamedicID.Equals(paramedicId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(PatientDischargeAppointment entity, GridCommandEventArgs e)
        {
            var userControl = (DetailOpRegistrationAndDischargeApptItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.RegistrationNo = txtRegistrationNo.Text;
                entity.AppointmentDate = userControl.AppointmentDate;

                entity.ParamedicID = userControl.ParamedicID;
                entity.ParamedicName = userControl.ParamedicName;
                entity.ServiceUnitID = userControl.ServiceUnitID;
                entity.ServiceUnitName = userControl.ServiceUnitName;
                entity.RoomID = userControl.RoomID;
                entity.RoomName = userControl.RoomName;
                entity.QueNo = userControl.QueNo;

                if (!string.IsNullOrEmpty(entity.QueNo))
                {
                    string value = entity.QueNo.Split('-')[1].Substring(1);
                    DateTime.TryParse(value, out DateTime dt);
                    entity.AppointmentTime = dt.ToString("HH:mm");
                }
                else
                {
                    entity.AppointmentTime = string.Empty;
                }
                entity.Notes = userControl.Notes;
                entity.IsProcessed = false;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        #endregion
    }
}
