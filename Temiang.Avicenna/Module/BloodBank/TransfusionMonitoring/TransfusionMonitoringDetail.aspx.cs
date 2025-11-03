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

namespace Temiang.Avicenna.Module.BloodBank
{
    public partial class TransfusionMonitoringDetail : BasePageDetail
    {
        private string RegNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }

        private string BagNo
        {
            get
            {
                return Request.QueryString["bagno"];
            }
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "##";
            UrlPageList = "TransfusionMonitoringList.aspx";

            ProgramID = AppConstant.Program.BloodBankTransfusionMonitoring;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRBloodType, AppEnum.StandardReference.BloodType);
                StandardReference.InitializeIncludeSpace(cboSRBloodGroupReceived, AppEnum.StandardReference.BloodGroup);
                StandardReference.InitializeIncludeSpace(cboSRBloodSource, AppEnum.StandardReference.BloodSource);
                StandardReference.InitializeIncludeSpace(cboSRBloodSourceFrom, AppEnum.StandardReference.BloodSourceFrom);

                PopulateRegistrationInformation(RegNo);

                string lblCaption = AppSession.Parameter.LblCaptionCheckMarkForTransfusionMonitoring.ToString();
                if (lblCaption.Contains(","))
                {
                    var param = lblCaption.Split(',');

                    string lblCaption0 = param[0].IsInteger() ? param[0] + "'" : param[0];
                    string lblCaption1 = param[1].IsInteger() ? param[1] + "'" : param[1];
                    string lblCaption2 = param[2].IsInteger() ? param[2] + "'" : param[2];
                    string lblCaption3 = param[3].IsInteger() ? param[3] + "'" : param[3];
                    string lblCaption4 = param[4].IsInteger() ? param[4] + "'" : param[4];
                    string lblCaption5 = param[5].IsInteger() ? param[5] + "'" : param[5];
                    string lblCaption6 = param[6].IsInteger() ? param[6] + "'" : param[6];
                    string lblCaption7 = param[7].IsInteger() ? param[7] + "'" : param[7];
                    string lblCaption8 = param[8].IsInteger() ? param[8] + "'" : param[8];
                    string lblCaption9 = param[9].IsInteger() ? param[9] + "'" : param[9];

                    lblCaptionAction0.Text = lblCaption0;
                    lblCaptionAction1.Text = lblCaption1;
                    lblCaptionAction2.Text = lblCaption2;
                    lblCaptionAction3.Text = lblCaption3;
                    lblCaptionAction4.Text = lblCaption4;
                    lblCaptionAction5.Text = lblCaption5;
                    lblCaptionAction6.Text = lblCaption6;
                    lblCaptionAction7.Text = lblCaption7;
                    lblCaptionAction8.Text = lblCaption8;
                    lblCaptionAction9.Text = lblCaption9;

                    lblCaptionReaction0.Text = lblCaption0;
                    lblCaptionReaction1.Text = lblCaption1;
                    lblCaptionReaction2.Text = lblCaption2;
                    lblCaptionReaction3.Text = lblCaption3;
                    lblCaptionReaction4.Text = lblCaption4;
                    lblCaptionReaction5.Text = lblCaption5;
                    lblCaptionReaction6.Text = lblCaption6;
                    lblCaptionReaction7.Text = lblCaption7;
                    lblCaptionReaction8.Text = lblCaption8;
                    lblCaptionReaction9.Text = lblCaption9;

                    lblCaptionVitalSigns0.Text = lblCaption0;
                    lblCaptionVitalSigns1.Text = lblCaption1;
                    lblCaptionVitalSigns2.Text = lblCaption2;
                    lblCaptionVitalSigns3.Text = lblCaption3;
                    lblCaptionVitalSigns4.Text = lblCaption4;
                    lblCaptionVitalSigns5.Text = lblCaption5;
                    lblCaptionVitalSigns6.Text = lblCaption6;
                    lblCaptionVitalSigns7.Text = lblCaption7;
                    lblCaptionVitalSigns8.Text = lblCaption8;
                    lblCaptionVitalSigns9.Text = lblCaption9;
                }
                else
                {
                    lblCaptionAction1.Text = AppSession.Parameter.FirstTimeCheckMarkForTransfusionMonitoring + "'";
                    lblCaptionReaction1.Text = AppSession.Parameter.FirstTimeCheckMarkForTransfusionMonitoring + "'";
                    lblCaptionVitalSigns1.Text = AppSession.Parameter.FirstTimeCheckMarkForTransfusionMonitoring + "'";
                }
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            ToolBarMenuSearch.Visible = false;
            ToolBarMenuAdd.Visible = false;
        }

        private void PopulateRegistrationInformation(string registrationNo)
        {
            var registration = new Registration();
            if (registration.LoadByPrimaryKey(registrationNo))
            {
                txtRegistrationNo.Text = registrationNo;
                var patient = new Patient();
                if (patient.LoadByPrimaryKey(registration.PatientID))
                {
                    txtMedicalNo.Text = patient.MedicalNo;
                    txtPatientName.Text = patient.PatientName;
                    var std = new AppStandardReferenceItem();
                    txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.SRSalutation) ? std.ItemName : string.Empty;
                    txtGender.Text = patient.Sex;
                    txtPlaceDOB.Text = string.Format("{0}, {1}", patient.CityOfBirth, Convert.ToDateTime(patient.DateOfBirth).ToString("dd-MMM-yyyy"));
                    txtAgeDay.Value = Helper.GetAgeInDay(patient.DateOfBirth.Value);
                    txtAgeMonth.Value = Helper.GetAgeInMonth(patient.DateOfBirth.Value);
                    txtAgeYear.Value = Helper.GetAgeInYear(patient.DateOfBirth.Value);

                    cboSRBloodType.SelectedValue = patient.SRBloodType;
                    rblBloodRhesus.SelectedValue = (patient.BloodRhesus == "-" ? "1" : "0");
                }
                else
                {
                    txtMedicalNo.Text = string.Empty;
                    txtPatientName.Text = string.Empty;
                    txtSalutation.Text = string.Empty;
                    txtGender.Text = string.Empty;
                    txtPlaceDOB.Text = string.Empty;

                    txtAgeDay.Value = 0;
                    txtAgeMonth.Value = 0;
                    txtAgeYear.Value = 0;

                    cboSRBloodType.SelectedValue = string.Empty;
                    cboSRBloodType.Text = string.Empty;
                    rblBloodRhesus.SelectedValue = "0";
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
                txtPatientName.Text = string.Empty;
                txtSalutation.Text = string.Empty;
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

                cboSRBloodType.SelectedValue = string.Empty;
                cboSRBloodType.Text = string.Empty;
                rblBloodRhesus.SelectedValue = "0";
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(chkIsReCheck, chkIsReCheck);
            ajax.AddAjaxSetting(chkIsReCheck, txtReCheckDateTime);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (chkIsReCheck.Checked && txtReCheckDateTime.IsEmpty)
            {
                args.MessageText = "Re-Check Date required.";
                args.IsCancel = true;
                return;
            }

            var entity = new BloodBankTransaction();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                var entityItem = new BloodBankTransactionItem();
                if (entityItem.LoadByPrimaryKey(txtTransactionNo.Text, txtBagNo.Text))
                {
                    SetEntityValue(entity, entityItem);
                    SaveEntity(entity, entityItem);
                }
                else
                {
                    args.MessageText = AppConstant.Message.RecordNotExist;
                    args.IsCancel = true;
                }
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
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text.Trim());
            auditLogFilter.TableName = "BloodBankTransaction";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
            printJobParameters.AddNew("p_UserID", AppSession.UserLogin.UserID);
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new BloodBankTransaction();
            if (parameters.Length > 0)
            {
                var transNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(transNo);
            }
            else
                entity.LoadByPrimaryKey(txtTransactionNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var bb = (BloodBankTransaction)entity;
            txtTransactionNo.Text = bb.TransactionNo;
            txtTransctionDate.SelectedDate = bb.TransactionDate;
            txtRegistrationNo.Text = bb.RegistrationNo;
            PopulateRegistrationInformation(txtRegistrationNo.Text);
            txtDiagnose.Text = bb.Diagnose;
            txtReason.Text = bb.Reason;

            //detail
            txtBagNo.Text = BagNo;
            var bbi = new BloodBankTransactionItem();
            bbi.LoadByPrimaryKey(txtTransactionNo.Text, txtBagNo.Text);

            cboSRBloodGroupReceived.SelectedValue = bbi.SRBloodGroupReceived;
            cboSRBloodSource.SelectedValue = bbi.SRBloodSource;
            cboSRBloodSourceFrom.SelectedValue = bbi.SRBloodSourceFrom;
            txtBloodBagTemperature.Value = Convert.ToDouble(bbi.BloodBagTemperature);

            var bagno = new BloodBagNo();
            if (bagno.LoadByPrimaryKey(txtBagNo.Text))
            {
                txtVolumeBag.Value = Convert.ToDouble(bagno.VolumeBag);
                txtExpiredDateDateTime.SelectedDate = bagno.ExpiredDateTime;
            }
            else
            {
                txtVolumeBag.Value = 0;
                txtExpiredDateDateTime.Clear();
            }

            rblIsScreeningLabelPassedPmi.SelectedValue = bbi.IsScreeningLabelPassedPmi ?? false ? "1" : "0";
            rblIsBloodTypeCompatibility.SelectedValue = bbi.IsBloodTypeCompatibility ?? false ? "1" : "0";
            rblIsLabelDonorsMatchesWithPatientsForm.SelectedValue =
                bbi.IsLabelDonorsMatchesWithPatientsForm ?? false ? "1" : "0";
            rblIsScreeningLabelPassedBd.SelectedValue = bbi.IsScreeningLabelPassedBd ?? false ? "1" : "0";

            rblIsCrossMatchingSuitability.SelectedValue = bbi.IsCrossMatchingSuitability ?? false ? "1" : "0";
            if (!string.IsNullOrEmpty(bbi.CrossmatchCompatibleMajor.Trim()))
                rblCrossmatchCompatibleMajor.SelectedValue = bbi.CrossmatchCompatibleMajor;

            if (!string.IsNullOrEmpty(bbi.CrossmatchCompatibleMinor.Trim()))
                rblCrossmatchCompatibleMinor.SelectedValue = bbi.CrossmatchCompatibleMinor;

            if (!string.IsNullOrEmpty(bbi.CrossmatchCompatibleAutoControl.Trim()))
                rblCrossmatchCompatibleAutoControl.SelectedValue = bbi.CrossmatchCompatibleAutoControl;

            txtCrossmatchCompatibleMinorLevel.Value = Convert.ToDouble(bbi.CrossmatchCompatibleMinorLevel);
            txtCrossmatchCompatibleAutoControlLevel.Value = Convert.ToDouble(bbi.CrossmatchCompatibleAutoControlLevel);

            #region Re-Check
            chkIsHiv.Checked = bbi.IsHiv ?? false;
            chkIsVbrl.Checked = bbi.IsVbrl ?? false;
            chkIsHbsAg.Checked = bbi.IsHbsAg ?? false;
            chkIsHcv.Checked = bbi.IsHcv ?? false;

            chkIsReCheck.Checked = bbi.IsReCheck ?? false;
            object expiredDate = bbi.ReCheckDateTime;
            if (expiredDate != null)
                txtReCheckDateTime.SelectedDate = bbi.ReCheckDateTime;
            else
                txtReCheckDateTime.Clear();

            if (bbi.IsReCheckExpiredDate != null)
                rblIsReCheckExpiredDate.SelectedValue = bbi.IsReCheckExpiredDate ?? false ? "1" : "0";
            else rblIsReCheckExpiredDate.SelectedValue = string.Empty;
            if (bbi.IsReCheckLeak != null)
                rblIsReCheckLeak.SelectedValue = bbi.IsReCheckLeak ?? false ? "1" : "0";
            else rblIsReCheckLeak.SelectedValue = string.Empty;
            if (bbi.IsReCheckHemolysis != null)
                rblIsReCheckHemolysis.SelectedValue = bbi.IsReCheckHemolysis ?? false ? "1" : "0";
            else rblIsReCheckHemolysis.SelectedValue = string.Empty;
            if (bbi.IsReCheckClotting != null)
                rblIsReCheckClotting.SelectedValue = bbi.IsReCheckClotting ?? false ? "1" : "0";
            else rblIsReCheckClotting.SelectedValue = string.Empty;
            if (bbi.IsReCheckBloodTypeCompatibility != null)
                rblIsReCheckBloodTypeCompatibility.SelectedValue = bbi.IsReCheckBloodTypeCompatibility ?? true ? "1" : "0";
            else rblIsReCheckBloodTypeCompatibility.SelectedValue = string.Empty;
            txtReCheckOfficer.Text = bbi.ReCheckOfficer;
            txtReCheckOfficer2.Text = bbi.ReCheckOfficer2;
            txtNotes.Text = bbi.Notes;
            #endregion

            #region Vital Signs
            txtBpPre.Text = bbi.BpPre;
            txtBp10.Text = bbi.Bp10;
            txtBp30.Text = bbi.Bp30;
            txtBp60.Text = bbi.Bp60;
            txtBp120.Text = bbi.Bp120;
            txtBp180.Text = bbi.Bp180;
            txtBp240.Text = bbi.Bp240;
            txtBpPost.Text = bbi.BpPost;
            txtBpPost2.Text = bbi.BpPost2;
            txtBpPost3.Text = bbi.BpPost3;
            txtBp31.Text = bbi.Bp31;
            txtBp32.Text = bbi.Bp32;
            txtBp33.Text = bbi.Bp33;
            txtBp34.Text = bbi.Bp34;
            txtBpPost4.Text = bbi.BpPost4;

            txtPulsePre.Value = Convert.ToDouble(bbi.PulsePre);
            txtPulse10.Value = Convert.ToDouble(bbi.Pulse10);
            txtPulse30.Value = Convert.ToDouble(bbi.Pulse30);
            txtPulse60.Value = Convert.ToDouble(bbi.Pulse60);
            txtPulse120.Value = Convert.ToDouble(bbi.Pulse120);
            txtPulse180.Value = Convert.ToDouble(bbi.Pulse180);
            txtPulse240.Value = Convert.ToDouble(bbi.Pulse240);
            txtPulsePost.Value = Convert.ToDouble(bbi.PulsePost);
            txtPulsePost2.Value = Convert.ToDouble(bbi.PulsePost2);
            txtPulsePost3.Value = Convert.ToDouble(bbi.PulsePost3);
            txtPulse31.Value = Convert.ToDouble(bbi.Pulse31);
            txtPulse32.Value = Convert.ToDouble(bbi.Pulse32);
            txtPulse33.Value = Convert.ToDouble(bbi.Pulse33);
            txtPulse34.Value = Convert.ToDouble(bbi.Pulse34);
            txtPulsePost4.Value = Convert.ToDouble(bbi.PulsePost4);

            txtTemperaturePre.Value = Convert.ToDouble(bbi.TemperaturePre);
            txtTemperature10.Value = Convert.ToDouble(bbi.Temperature10);
            txtTemperature30.Value = Convert.ToDouble(bbi.Temperature30);
            txtTemperature60.Value = Convert.ToDouble(bbi.Temperature60);
            txtTemperature120.Value = Convert.ToDouble(bbi.Temperature120);
            txtTemperature180.Value = Convert.ToDouble(bbi.Temperature180);
            txtTemperature240.Value = Convert.ToDouble(bbi.Temperature240);
            txtTemperaturePost.Value = Convert.ToDouble(bbi.TemperaturePost);
            txtTemperaturePost2.Value = Convert.ToDouble(bbi.TemperaturePost2);
            txtTemperaturePost3.Value = Convert.ToDouble(bbi.TemperaturePost3);
            txtTemperature31.Value = Convert.ToDouble(bbi.Temperature31);
            txtTemperature32.Value = Convert.ToDouble(bbi.Temperature32);
            txtTemperature33.Value = Convert.ToDouble(bbi.Temperature33);
            txtTemperature34.Value = Convert.ToDouble(bbi.Temperature34);
            txtTemperaturePost4.Value = Convert.ToDouble(bbi.TemperaturePost4);

            txtRespiratoryPre.Value = Convert.ToDouble(bbi.RespiratoryPre);
            txtRespiratory10.Value = Convert.ToDouble(bbi.Respiratory10);
            txtRespiratory30.Value = Convert.ToDouble(bbi.Respiratory30);
            txtRespiratory60.Value = Convert.ToDouble(bbi.Respiratory60);
            txtRespiratory120.Value = Convert.ToDouble(bbi.Respiratory120);
            txtRespiratory180.Value = Convert.ToDouble(bbi.Respiratory180);
            txtRespiratory240.Value = Convert.ToDouble(bbi.Respiratory240);
            txtRespiratoryPost.Value = Convert.ToDouble(bbi.RespiratoryPost);
            txtRespiratoryPost2.Value = Convert.ToDouble(bbi.RespiratoryPost2);
            txtRespiratoryPost3.Value = Convert.ToDouble(bbi.RespiratoryPost3);
            txtRespiratory31.Value = Convert.ToDouble(bbi.Respiratory31);
            txtRespiratory32.Value = Convert.ToDouble(bbi.Respiratory32);
            txtRespiratory33.Value = Convert.ToDouble(bbi.Respiratory33);
            txtRespiratory34.Value = Convert.ToDouble(bbi.Respiratory34);
            txtRespiratoryPost4.Value = Convert.ToDouble(bbi.RespiratoryPost4);
            #endregion

            #region Reaction
            if (bbi.IsFeverPre != null)
                rblIsFeverPre.SelectedValue = bbi.IsFeverPre ?? false ? "1" : "0";
            if (bbi.IsFever10 != null)
                rblIsFever10.SelectedValue = bbi.IsFever10 ?? false ? "1" : "0";
            if (bbi.IsFever30 != null)
                rblIsFever30.SelectedValue = bbi.IsFever30 ?? false ? "1" : "0";
            if (bbi.IsFever60 != null)
                rblIsFever60.SelectedValue = bbi.IsFever60 ?? false ? "1" : "0";
            if (bbi.IsFever120 != null)
                rblIsFever120.SelectedValue = bbi.IsFever120 ?? false ? "1" : "0";
            if (bbi.IsFever180 != null)
                rblIsFever180.SelectedValue = bbi.IsFever180 ?? false ? "1" : "0";
            if (bbi.IsFever240 != null)
                rblIsFever240.SelectedValue = bbi.IsFever240 ?? false ? "1" : "0";
            if (bbi.IsFeverPost != null)
                rblIsFeverPost.SelectedValue = bbi.IsFeverPost ?? false ? "1" : "0";
            if (bbi.IsFeverPost2 != null)
                rblIsFeverPost2.SelectedValue = bbi.IsFeverPost2 ?? false ? "1" : "0";
            if (bbi.IsFeverPost3 != null)
                rblIsFeverPost3.SelectedValue = bbi.IsFeverPost3 ?? false ? "1" : "0";
            if (bbi.IsFeverPost4 != null)
                rblIsFeverPost4.SelectedValue = bbi.IsFeverPost4 ?? false ? "1" : "0";
            if (bbi.IsFeverPost5 != null)
                rblIsFeverPost5.SelectedValue = bbi.IsFeverPost5 ?? false ? "1" : "0";
            if (bbi.IsFeverPost6 != null)
                rblIsFeverPost6.SelectedValue = bbi.IsFeverPost6 ?? false ? "1" : "0";
            if (bbi.IsFeverPost7 != null)
                rblIsFeverPost7.SelectedValue = bbi.IsFeverPost7 ?? false ? "1" : "0";
            if (bbi.IsFeverPost8 != null)
                rblIsFeverPost8.SelectedValue = bbi.IsFeverPost8 ?? false ? "1" : "0";

            if (bbi.IsShiveringPre != null)
                rblIsShiveringPre.SelectedValue = bbi.IsShiveringPre ?? false ? "1" : "0";
            if (bbi.IsShivering10 != null)
                rblIsShivering10.SelectedValue = bbi.IsShivering10 ?? false ? "1" : "0";
            if (bbi.IsShivering30 != null)
                rblIsShivering30.SelectedValue = bbi.IsShivering30 ?? false ? "1" : "0";
            if (bbi.IsShivering60 != null)
                rblIsShivering60.SelectedValue = bbi.IsShivering60 ?? false ? "1" : "0";
            if (bbi.IsShivering120 != null)
                rblIsShivering120.SelectedValue = bbi.IsShivering120 ?? false ? "1" : "0";
            if (bbi.IsShivering180 != null)
                rblIsShivering180.SelectedValue = bbi.IsShivering180 ?? false ? "1" : "0";
            if (bbi.IsShivering240 != null)
                rblIsShivering240.SelectedValue = bbi.IsShivering240 ?? false ? "1" : "0";
            if (bbi.IsShiveringPost != null)
                rblIsShiveringPost.SelectedValue = bbi.IsShiveringPost ?? false ? "1" : "0";
            if (bbi.IsShiveringPost2 != null)
                rblIsShiveringPost2.SelectedValue = bbi.IsShiveringPost2 ?? false ? "1" : "0";
            if (bbi.IsShiveringPost3 != null)
                rblIsShiveringPost3.SelectedValue = bbi.IsShiveringPost3 ?? false ? "1" : "0";
            if (bbi.IsShiveringPost4 != null)
                rblIsShiveringPost4.SelectedValue = bbi.IsShiveringPost4 ?? false ? "1" : "0";
            if (bbi.IsShiveringPost5 != null)
                rblIsShiveringPost5.SelectedValue = bbi.IsShiveringPost5 ?? false ? "1" : "0";
            if (bbi.IsShiveringPost6 != null)
                rblIsShiveringPost6.SelectedValue = bbi.IsShiveringPost6 ?? false ? "1" : "0";
            if (bbi.IsShiveringPost7 != null)
                rblIsShiveringPost7.SelectedValue = bbi.IsShiveringPost7 ?? false ? "1" : "0";
            if (bbi.IsShiveringPost8 != null)
                rblIsShiveringPost8.SelectedValue = bbi.IsShiveringPost8 ?? false ? "1" : "0";

            if (bbi.IsSobPre != null)
                rblIsSobPre.SelectedValue = bbi.IsSobPre ?? false ? "1" : "0";
            if (bbi.IsSob10 != null)
                rblIsSob10.SelectedValue = bbi.IsSob10 ?? false ? "1" : "0";
            if (bbi.IsSob30 != null)
                rblIsSob30.SelectedValue = bbi.IsSob30 ?? false ? "1" : "0";
            if (bbi.IsSob60 != null)
                rblIsSob60.SelectedValue = bbi.IsSob60 ?? false ? "1" : "0";
            if (bbi.IsSob120 != null)
                rblIsSob120.SelectedValue = bbi.IsSob120 ?? false ? "1" : "0";
            if (bbi.IsSob180 != null)
                rblIsSob180.SelectedValue = bbi.IsSob180 ?? false ? "1" : "0";
            if (bbi.IsSob240 != null)
                rblIsSob240.SelectedValue = bbi.IsSob240 ?? false ? "1" : "0";
            if (bbi.IsSobPost != null)
                rblIsSobPost.SelectedValue = bbi.IsSobPost ?? false ? "1" : "0";
            if (bbi.IsSobPost2 != null)
                rblIsSobPost2.SelectedValue = bbi.IsSobPost2 ?? false ? "1" : "0";
            if (bbi.IsSobPost3 != null)
                rblIsSobPost3.SelectedValue = bbi.IsSobPost3 ?? false ? "1" : "0";
            if (bbi.IsSobPost4 != null)
                rblIsSobPost4.SelectedValue = bbi.IsSobPost4 ?? false ? "1" : "0";
            if (bbi.IsSobPost5 != null)
                rblIsSobPost5.SelectedValue = bbi.IsSobPost5 ?? false ? "1" : "0";
            if (bbi.IsSobPost6 != null)
                rblIsSobPost6.SelectedValue = bbi.IsSobPost6 ?? false ? "1" : "0";
            if (bbi.IsSobPost7 != null)
                rblIsSobPost7.SelectedValue = bbi.IsSobPost7 ?? false ? "1" : "0";
            if (bbi.IsSobPost8 != null)
                rblIsSobPost8.SelectedValue = bbi.IsSobPost8 ?? false ? "1" : "0";

            if (bbi.IsPainfulPre != null)
                rblIsPainfulPre.SelectedValue = bbi.IsPainfulPre ?? false ? "1" : "0";
            if (bbi.IsPainful10 != null)
                rblIsPainful10.SelectedValue = bbi.IsPainful10 ?? false ? "1" : "0";
            if (bbi.IsPainful30 != null)
                rblIsPainful30.SelectedValue = bbi.IsPainful30 ?? false ? "1" : "0";
            if (bbi.IsPainful60 != null)
                rblIsPainful60.SelectedValue = bbi.IsPainful60 ?? false ? "1" : "0";
            if (bbi.IsPainful120 != null)
                rblIsPainful120.SelectedValue = bbi.IsPainful120 ?? false ? "1" : "0";
            if (bbi.IsPainful180 != null)
                rblIsPainful180.SelectedValue = bbi.IsPainful180 ?? false ? "1" : "0";
            if (bbi.IsPainful240 != null)
                rblIsPainful240.SelectedValue = bbi.IsPainful240 ?? false ? "1" : "0";
            if (bbi.IsPainfulPost != null)
                rblIsPainfulPost.SelectedValue = bbi.IsPainfulPost ?? false ? "1" : "0";
            if (bbi.IsPainfulPost2 != null)
                rblIsPainfulPost2.SelectedValue = bbi.IsPainfulPost2 ?? false ? "1" : "0";
            if (bbi.IsPainfulPost3 != null)
                rblIsPainfulPost3.SelectedValue = bbi.IsPainfulPost3 ?? false ? "1" : "0";
            if (bbi.IsPainfulPost4 != null)
                rblIsPainfulPost4.SelectedValue = bbi.IsPainfulPost4 ?? false ? "1" : "0";
            if (bbi.IsPainfulPost5 != null)
                rblIsPainfulPost5.SelectedValue = bbi.IsPainfulPost5 ?? false ? "1" : "0";
            if (bbi.IsPainfulPost6 != null)
                rblIsPainfulPost6.SelectedValue = bbi.IsPainfulPost6 ?? false ? "1" : "0";
            if (bbi.IsPainfulPost7 != null)
                rblIsPainfulPost7.SelectedValue = bbi.IsPainfulPost7 ?? false ? "1" : "0";
            if (bbi.IsPainfulPost8 != null)
                rblIsPainfulPost8.SelectedValue = bbi.IsPainfulPost8 ?? false ? "1" : "0";

            if (bbi.IsNauseaPre != null)
                rblIsNauseaPre.SelectedValue = bbi.IsNauseaPre ?? false ? "1" : "0";
            if (bbi.IsNausea10 != null)
                rblIsNausea10.SelectedValue = bbi.IsNausea10 ?? false ? "1" : "0";
            if (bbi.IsNausea30 != null)
                rblIsNausea30.SelectedValue = bbi.IsNausea30 ?? false ? "1" : "0";
            if (bbi.IsNausea60 != null)
                rblIsNausea60.SelectedValue = bbi.IsNausea60 ?? false ? "1" : "0";
            if (bbi.IsNausea120 != null)
                rblIsNausea120.SelectedValue = bbi.IsNausea120 ?? false ? "1" : "0";
            if (bbi.IsNausea180 != null)
                rblIsNausea180.SelectedValue = bbi.IsNausea180 ?? false ? "1" : "0";
            if (bbi.IsNausea240 != null)
                rblIsNausea240.SelectedValue = bbi.IsNausea240 ?? false ? "1" : "0";
            if (bbi.IsNauseaPost != null)
                rblIsNauseaPost.SelectedValue = bbi.IsNauseaPost ?? false ? "1" : "0";
            if (bbi.IsNauseaPost2 != null)
                rblIsNauseaPost2.SelectedValue = bbi.IsNauseaPost2 ?? false ? "1" : "0";
            if (bbi.IsNauseaPost3 != null)
                rblIsNauseaPost3.SelectedValue = bbi.IsNauseaPost3 ?? false ? "1" : "0";
            if (bbi.IsNauseaPost4 != null)
                rblIsNauseaPost4.SelectedValue = bbi.IsNauseaPost4 ?? false ? "1" : "0";
            if (bbi.IsNauseaPost5 != null)
                rblIsNauseaPost5.SelectedValue = bbi.IsNauseaPost5 ?? false ? "1" : "0";
            if (bbi.IsNauseaPost6 != null)
                rblIsNauseaPost6.SelectedValue = bbi.IsNauseaPost6 ?? false ? "1" : "0";
            if (bbi.IsNauseaPost7 != null)
                rblIsNauseaPost7.SelectedValue = bbi.IsNauseaPost7 ?? false ? "1" : "0";
            if (bbi.IsNauseaPost8 != null)
                rblIsNauseaPost8.SelectedValue = bbi.IsNauseaPost8 ?? false ? "1" : "0";

            if (bbi.IsBleedingPre != null)
                rblIsBleedingPre.SelectedValue = bbi.IsBleedingPre ?? false ? "1" : "0";
            if (bbi.IsBleeding10 != null)
                rblIsBleeding10.SelectedValue = bbi.IsBleeding10 ?? false ? "1" : "0";
            if (bbi.IsBleeding30 != null)
                rblIsBleeding30.SelectedValue = bbi.IsBleeding30 ?? false ? "1" : "0";
            if (bbi.IsBleeding60 != null)
                rblIsBleeding60.SelectedValue = bbi.IsBleeding60 ?? false ? "1" : "0";
            if (bbi.IsBleeding120 != null)
                rblIsBleeding120.SelectedValue = bbi.IsBleeding120 ?? false ? "1" : "0";
            if (bbi.IsBleeding180 != null)
                rblIsBleeding180.SelectedValue = bbi.IsBleeding180 ?? false ? "1" : "0";
            if (bbi.IsBleeding240 != null)
                rblIsBleeding240.SelectedValue = bbi.IsBleeding240 ?? false ? "1" : "0";
            if (bbi.IsBleedingPost != null)
                rblIsBleedingPost.SelectedValue = bbi.IsBleedingPost ?? false ? "1" : "0";
            if (bbi.IsBleedingPost2 != null)
                rblIsBleedingPost2.SelectedValue = bbi.IsBleedingPost2 ?? false ? "1" : "0";
            if (bbi.IsBleedingPost3 != null)
                rblIsBleedingPost3.SelectedValue = bbi.IsBleedingPost3 ?? false ? "1" : "0";
            if (bbi.IsBleedingPost4 != null)
                rblIsBleedingPost4.SelectedValue = bbi.IsBleedingPost4 ?? false ? "1" : "0";
            if (bbi.IsBleedingPost5 != null)
                rblIsBleedingPost5.SelectedValue = bbi.IsBleedingPost5 ?? false ? "1" : "0";
            if (bbi.IsBleedingPost6 != null)
                rblIsBleedingPost6.SelectedValue = bbi.IsBleedingPost6 ?? false ? "1" : "0";
            if (bbi.IsBleedingPost7 != null)
                rblIsBleedingPost7.SelectedValue = bbi.IsBleedingPost7 ?? false ? "1" : "0";
            if (bbi.IsBleedingPost8 != null)
                rblIsBleedingPost8.SelectedValue = bbi.IsBleedingPost8 ?? false ? "1" : "0";

            if (bbi.IsHypotensionPre != null)
                rblIsHypotensionPre.SelectedValue = bbi.IsHypotensionPre ?? false ? "1" : "0";
            if (bbi.IsHypotension10 != null)
                rblIsHypotension10.SelectedValue = bbi.IsHypotension10 ?? false ? "1" : "0";
            if (bbi.IsHypotension30 != null)
                rblIsHypotension30.SelectedValue = bbi.IsHypotension30 ?? false ? "1" : "0";
            if (bbi.IsHypotension60 != null)
                rblIsHypotension60.SelectedValue = bbi.IsHypotension60 ?? false ? "1" : "0";
            if (bbi.IsHypotension120 != null)
                rblIsHypotension120.SelectedValue = bbi.IsHypotension120 ?? false ? "1" : "0";
            if (bbi.IsHypotension180 != null)
                rblIsHypotension180.SelectedValue = bbi.IsHypotension180 ?? false ? "1" : "0";
            if (bbi.IsHypotension240 != null)
                rblIsHypotension240.SelectedValue = bbi.IsHypotension240 ?? false ? "1" : "0";
            if (bbi.IsHypotensionPost != null)
                rblIsHypotensionPost.SelectedValue = bbi.IsHypotensionPost ?? false ? "1" : "0";
            if (bbi.IsHypotensionPost2 != null)
                rblIsHypotensionPost2.SelectedValue = bbi.IsHypotensionPost2 ?? false ? "1" : "0";
            if (bbi.IsHypotensionPost3 != null)
                rblIsHypotensionPost3.SelectedValue = bbi.IsHypotensionPost3 ?? false ? "1" : "0";
            if (bbi.IsHypotensionPost4 != null)
                rblIsHypotensionPost4.SelectedValue = bbi.IsHypotensionPost4 ?? false ? "1" : "0";
            if (bbi.IsHypotensionPost5 != null)
                rblIsHypotensionPost5.SelectedValue = bbi.IsHypotensionPost5 ?? false ? "1" : "0";
            if (bbi.IsHypotensionPost6 != null)
                rblIsHypotensionPost6.SelectedValue = bbi.IsHypotensionPost6 ?? false ? "1" : "0";
            if (bbi.IsHypotensionPost7 != null)
                rblIsHypotensionPost7.SelectedValue = bbi.IsHypotensionPost7 ?? false ? "1" : "0";
            if (bbi.IsHypotensionPost8 != null)
                rblIsHypotensionPost8.SelectedValue = bbi.IsHypotensionPost8 ?? false ? "1" : "0";

            if (bbi.IsShockPre != null)
                rblIsShockPre.SelectedValue = bbi.IsShockPre ?? false ? "1" : "0";
            if (bbi.IsShock10 != null)
                rblIsShock10.SelectedValue = bbi.IsShock10 ?? false ? "1" : "0";
            if (bbi.IsShock30 != null)
                rblIsShock30.SelectedValue = bbi.IsShock30 ?? false ? "1" : "0";
            if (bbi.IsShock60 != null)
                rblIsShock60.SelectedValue = bbi.IsShock60 ?? false ? "1" : "0";
            if (bbi.IsShock120 != null)
                rblIsShock120.SelectedValue = bbi.IsShock120 ?? false ? "1" : "0";
            if (bbi.IsShock180 != null)
                rblIsShock180.SelectedValue = bbi.IsShock180 ?? false ? "1" : "0";
            if (bbi.IsShock240 != null)
                rblIsShock240.SelectedValue = bbi.IsShock240 ?? false ? "1" : "0";
            if (bbi.IsShockPost != null)
                rblIsShockPost.SelectedValue = bbi.IsShockPost ?? false ? "1" : "0";
            if (bbi.IsShockPost2 != null)
                rblIsShockPost2.SelectedValue = bbi.IsShockPost2 ?? false ? "1" : "0";
            if (bbi.IsShockPost3 != null)
                rblIsShockPost3.SelectedValue = bbi.IsShockPost3 ?? false ? "1" : "0";
            if (bbi.IsShockPost4 != null)
                rblIsShockPost4.SelectedValue = bbi.IsShockPost4 ?? false ? "1" : "0";
            if (bbi.IsShockPost5 != null)
                rblIsShockPost5.SelectedValue = bbi.IsShockPost5 ?? false ? "1" : "0";
            if (bbi.IsShockPost6 != null)
                rblIsShockPost6.SelectedValue = bbi.IsShockPost6 ?? false ? "1" : "0";
            if (bbi.IsShockPost7 != null)
                rblIsShockPost7.SelectedValue = bbi.IsShockPost7 ?? false ? "1" : "0";
            if (bbi.IsShockPost8 != null)
                rblIsShockPost8.SelectedValue = bbi.IsShockPost8 ?? false ? "1" : "0";

            if (bbi.IsUrticariaPre != null)
                rblIsUrticariaPre.SelectedValue = bbi.IsUrticariaPre ?? false ? "1" : "0";
            if (bbi.IsUrticaria10 != null)
                rblIsUrticaria10.SelectedValue = bbi.IsUrticaria10 ?? false ? "1" : "0";
            if (bbi.IsUrticaria30 != null)
                rblIsUrticaria30.SelectedValue = bbi.IsUrticaria30 ?? false ? "1" : "0";
            if (bbi.IsUrticaria60 != null)
                rblIsUrticaria60.SelectedValue = bbi.IsUrticaria60 ?? false ? "1" : "0";
            if (bbi.IsUrticaria120 != null)
                rblIsUrticaria120.SelectedValue = bbi.IsUrticaria120 ?? false ? "1" : "0";
            if (bbi.IsUrticaria180 != null)
                rblIsUrticaria180.SelectedValue = bbi.IsUrticaria180 ?? false ? "1" : "0";
            if (bbi.IsUrticaria240 != null)
                rblIsUrticaria240.SelectedValue = bbi.IsUrticaria240 ?? false ? "1" : "0";
            if (bbi.IsUrticariaPost != null)
                rblIsUrticariaPost.SelectedValue = bbi.IsUrticariaPost ?? false ? "1" : "0";
            if (bbi.IsUrticariaPost2 != null)
                rblIsUrticariaPost2.SelectedValue = bbi.IsUrticariaPost2 ?? false ? "1" : "0";
            if (bbi.IsUrticariaPost3 != null)
                rblIsUrticariaPost3.SelectedValue = bbi.IsUrticariaPost3 ?? false ? "1" : "0";
            if (bbi.IsUrticariaPost4 != null)
                rblIsUrticariaPost4.SelectedValue = bbi.IsUrticariaPost4 ?? false ? "1" : "0";
            if (bbi.IsUrticariaPost5 != null)
                rblIsUrticariaPost5.SelectedValue = bbi.IsUrticariaPost5 ?? false ? "1" : "0";
            if (bbi.IsUrticariaPost6 != null)
                rblIsUrticariaPost6.SelectedValue = bbi.IsUrticariaPost6 ?? false ? "1" : "0";
            if (bbi.IsUrticariaPost7 != null)
                rblIsUrticariaPost7.SelectedValue = bbi.IsUrticariaPost7 ?? false ? "1" : "0";
            if (bbi.IsUrticariaPost8 != null)
                rblIsUrticariaPost8.SelectedValue = bbi.IsUrticariaPost8 ?? false ? "1" : "0";

            if (bbi.IsRashPre != null)
                rblIsRashPre.SelectedValue = bbi.IsRashPre ?? false ? "1" : "0";
            if (bbi.IsRash10 != null)
                rblIsRash10.SelectedValue = bbi.IsRash10 ?? false ? "1" : "0";
            if (bbi.IsRash30 != null)
                rblIsRash30.SelectedValue = bbi.IsRash30 ?? false ? "1" : "0";
            if (bbi.IsRash60 != null)
                rblIsRash60.SelectedValue = bbi.IsRash60 ?? false ? "1" : "0";
            if (bbi.IsRash120 != null)
                rblIsRash120.SelectedValue = bbi.IsRash120 ?? false ? "1" : "0";
            if (bbi.IsRash180 != null)
                rblIsRash180.SelectedValue = bbi.IsRash180 ?? false ? "1" : "0";
            if (bbi.IsRash240 != null)
                rblIsRash240.SelectedValue = bbi.IsRash240 ?? false ? "1" : "0";
            if (bbi.IsRashPost != null)
                rblIsRashPost.SelectedValue = bbi.IsRashPost ?? false ? "1" : "0";
            if (bbi.IsRashPost2 != null)
                rblIsRashPost2.SelectedValue = bbi.IsRashPost2 ?? false ? "1" : "0";
            if (bbi.IsRashPost3 != null)
                rblIsRashPost3.SelectedValue = bbi.IsRashPost3 ?? false ? "1" : "0";
            if (bbi.IsRashPost4 != null)
                rblIsRashPost4.SelectedValue = bbi.IsRashPost4 ?? false ? "1" : "0";
            if (bbi.IsRashPost5 != null)
                rblIsRashPost5.SelectedValue = bbi.IsRashPost5 ?? false ? "1" : "0";
            if (bbi.IsRashPost6 != null)
                rblIsRashPost6.SelectedValue = bbi.IsRashPost6 ?? false ? "1" : "0";
            if (bbi.IsRashPost7 != null)
                rblIsRashPost7.SelectedValue = bbi.IsRashPost7 ?? false ? "1" : "0";
            if (bbi.IsRashPost8 != null)
                rblIsRashPost8.SelectedValue = bbi.IsRashPost8 ?? false ? "1" : "0";

            if (bbi.IsPruritusPre != null)
                rblIsPruritusPre.SelectedValue = bbi.IsPruritusPre ?? false ? "1" : "0";
            if (bbi.IsPruritus10 != null)
                rblIsPruritus10.SelectedValue = bbi.IsPruritus10 ?? false ? "1" : "0";
            if (bbi.IsPruritus30 != null)
                rblIsPruritus30.SelectedValue = bbi.IsPruritus30 ?? false ? "1" : "0";
            if (bbi.IsPruritus60 != null)
                rblIsPruritus60.SelectedValue = bbi.IsPruritus60 ?? false ? "1" : "0";
            if (bbi.IsPruritus120 != null)
                rblIsPruritus120.SelectedValue = bbi.IsPruritus120 ?? false ? "1" : "0";
            if (bbi.IsPruritus180 != null)
                rblIsPruritus180.SelectedValue = bbi.IsPruritus180 ?? false ? "1" : "0";
            if (bbi.IsPruritus240 != null)
                rblIsPruritus240.SelectedValue = bbi.IsPruritus240 ?? false ? "1" : "0";
            if (bbi.IsPruritusPost != null)
                rblIsPruritusPost.SelectedValue = bbi.IsPruritusPost ?? false ? "1" : "0";
            if (bbi.IsPruritusPost2 != null)
                rblIsPruritusPost2.SelectedValue = bbi.IsPruritusPost2 ?? false ? "1" : "0";
            if (bbi.IsPruritusPost3 != null)
                rblIsPruritusPost3.SelectedValue = bbi.IsPruritusPost3 ?? false ? "1" : "0";
            if (bbi.IsPruritusPost4 != null)
                rblIsPruritusPost4.SelectedValue = bbi.IsPruritusPost4 ?? false ? "1" : "0";
            if (bbi.IsPruritusPost5 != null)
                rblIsPruritusPost5.SelectedValue = bbi.IsPruritusPost5 ?? false ? "1" : "0";
            if (bbi.IsPruritusPost6 != null)
                rblIsPruritusPost6.SelectedValue = bbi.IsPruritusPost6 ?? false ? "1" : "0";
            if (bbi.IsPruritusPost7 != null)
                rblIsPruritusPost7.SelectedValue = bbi.IsPruritusPost7 ?? false ? "1" : "0";
            if (bbi.IsPruritusPost8 != null)
                rblIsPruritusPost8.SelectedValue = bbi.IsPruritusPost8 ?? false ? "1" : "0";

            if (bbi.IsAnxiousPre != null)
                rblIsAnxiousPre.SelectedValue = bbi.IsAnxiousPre ?? false ? "1" : "0";
            if (bbi.IsAnxious10 != null)
                rblIsAnxious10.SelectedValue = bbi.IsAnxious10 ?? false ? "1" : "0";
            if (bbi.IsAnxious30 != null)
                rblIsAnxious30.SelectedValue = bbi.IsAnxious30 ?? false ? "1" : "0";
            if (bbi.IsAnxious60 != null)
                rblIsAnxious60.SelectedValue = bbi.IsAnxious60 ?? false ? "1" : "0";
            if (bbi.IsAnxious120 != null)
                rblIsAnxious120.SelectedValue = bbi.IsAnxious120 ?? false ? "1" : "0";
            if (bbi.IsAnxious180 != null)
                rblIsAnxious180.SelectedValue = bbi.IsAnxious180 ?? false ? "1" : "0";
            if (bbi.IsAnxious240 != null)
                rblIsAnxious240.SelectedValue = bbi.IsAnxious240 ?? false ? "1" : "0";
            if (bbi.IsAnxiousPost != null)
                rblIsAnxiousPost.SelectedValue = bbi.IsAnxiousPost ?? false ? "1" : "0";
            if (bbi.IsAnxiousPost2 != null)
                rblIsAnxiousPost2.SelectedValue = bbi.IsAnxiousPost2 ?? false ? "1" : "0";
            if (bbi.IsAnxiousPost3 != null)
                rblIsAnxiousPost3.SelectedValue = bbi.IsAnxiousPost3 ?? false ? "1" : "0";
            if (bbi.IsAnxiousPost4 != null)
                rblIsAnxiousPost4.SelectedValue = bbi.IsAnxiousPost4 ?? false ? "1" : "0";
            if (bbi.IsAnxiousPost5 != null)
                rblIsAnxiousPost5.SelectedValue = bbi.IsAnxiousPost5 ?? false ? "1" : "0";
            if (bbi.IsAnxiousPost6 != null)
                rblIsAnxiousPost6.SelectedValue = bbi.IsAnxiousPost6 ?? false ? "1" : "0";
            if (bbi.IsAnxiousPost7 != null)
                rblIsAnxiousPost7.SelectedValue = bbi.IsAnxiousPost7 ?? false ? "1" : "0";
            if (bbi.IsAnxiousPost8 != null)
                rblIsAnxiousPost8.SelectedValue = bbi.IsAnxiousPost8 ?? false ? "1" : "0";

            if (bbi.IsWeakPre != null)
                rblIsWeakPre.SelectedValue = bbi.IsWeakPre ?? false ? "1" : "0";
            if (bbi.IsWeak10 != null)
                rblIsWeak10.SelectedValue = bbi.IsWeak10 ?? false ? "1" : "0";
            if (bbi.IsWeak30 != null)
                rblIsWeak30.SelectedValue = bbi.IsWeak30 ?? false ? "1" : "0";
            if (bbi.IsWeak60 != null)
                rblIsWeak60.SelectedValue = bbi.IsWeak60 ?? false ? "1" : "0";
            if (bbi.IsWeak120 != null)
                rblIsWeak120.SelectedValue = bbi.IsWeak120 ?? false ? "1" : "0";
            if (bbi.IsWeak180 != null)
                rblIsWeak180.SelectedValue = bbi.IsWeak180 ?? false ? "1" : "0";
            if (bbi.IsWeak240 != null)
                rblIsWeak240.SelectedValue = bbi.IsWeak240 ?? false ? "1" : "0";
            if (bbi.IsWeakPost != null)
                rblIsWeakPost.SelectedValue = bbi.IsWeakPost ?? false ? "1" : "0";
            if (bbi.IsWeakPost2 != null)
                rblIsWeakPost2.SelectedValue = bbi.IsWeakPost2 ?? false ? "1" : "0";
            if (bbi.IsWeakPost3 != null)
                rblIsWeakPost3.SelectedValue = bbi.IsWeakPost3 ?? false ? "1" : "0";
            if (bbi.IsWeakPost4 != null)
                rblIsWeakPost4.SelectedValue = bbi.IsWeakPost4 ?? false ? "1" : "0";
            if (bbi.IsWeakPost5 != null)
                rblIsWeakPost5.SelectedValue = bbi.IsWeakPost5 ?? false ? "1" : "0";
            if (bbi.IsWeakPost6 != null)
                rblIsWeakPost6.SelectedValue = bbi.IsWeakPost6 ?? false ? "1" : "0";
            if (bbi.IsWeakPost7 != null)
                rblIsWeakPost7.SelectedValue = bbi.IsWeakPost7 ?? false ? "1" : "0";
            if (bbi.IsWeakPost8 != null)
                rblIsWeakPost8.SelectedValue = bbi.IsWeakPost8 ?? false ? "1" : "0";


            if (bbi.IsPalpitationsPre != null)
                rblIsPalpitationsPre.SelectedValue = bbi.IsPalpitationsPre ?? false ? "1" : "0";
            if (bbi.IsPalpitations10 != null)
                rblIsPalpitations10.SelectedValue = bbi.IsPalpitations10 ?? false ? "1" : "0";
            if (bbi.IsPalpitations30 != null)
                rblIsPalpitations30.SelectedValue = bbi.IsPalpitations30 ?? false ? "1" : "0";
            if (bbi.IsPalpitations60 != null)
                rblIsPalpitations60.SelectedValue = bbi.IsPalpitations60 ?? false ? "1" : "0";
            if (bbi.IsPalpitations120 != null)
                rblIsPalpitations120.SelectedValue = bbi.IsPalpitations120 ?? false ? "1" : "0";
            if (bbi.IsPalpitations180 != null)
                rblIsPalpitations180.SelectedValue = bbi.IsPalpitations180 ?? false ? "1" : "0";
            if (bbi.IsPalpitations240 != null)
                rblIsPalpitations240.SelectedValue = bbi.IsPalpitations240 ?? false ? "1" : "0";
            if (bbi.IsPalpitationsPost != null)
                rblIsPalpitationsPost.SelectedValue = bbi.IsPalpitationsPost ?? false ? "1" : "0";
            if (bbi.IsPalpitationsPost2 != null)
                rblIsPalpitationsPost2.SelectedValue = bbi.IsPalpitationsPost2 ?? false ? "1" : "0";
            if (bbi.IsPalpitationsPost3 != null)
                rblIsPalpitationsPost3.SelectedValue = bbi.IsPalpitationsPost3 ?? false ? "1" : "0";
            if (bbi.IsPalpitationsPost4 != null)
                rblIsPalpitationsPost4.SelectedValue = bbi.IsPalpitationsPost4 ?? false ? "1" : "0";
            if (bbi.IsPalpitationsPost5 != null)
                rblIsPalpitationsPost5.SelectedValue = bbi.IsPalpitationsPost5 ?? false ? "1" : "0";
            if (bbi.IsPalpitationsPost6 != null)
                rblIsPalpitationsPost6.SelectedValue = bbi.IsPalpitationsPost6 ?? false ? "1" : "0";
            if (bbi.IsPalpitationsPost7 != null)
                rblIsPalpitationsPost7.SelectedValue = bbi.IsPalpitationsPost7 ?? false ? "1" : "0";
            if (bbi.IsPalpitationsPost8 != null)
                rblIsPalpitationsPost8.SelectedValue = bbi.IsPalpitationsPost8 ?? false ? "1" : "0";

            if (bbi.IsMildDyspneaPre != null)
                rblIsMildDyspneaPre.SelectedValue = bbi.IsMildDyspneaPre ?? false ? "1" : "0";
            if (bbi.IsMildDyspnea10 != null)
                rblIsMildDyspnea10.SelectedValue = bbi.IsMildDyspnea10 ?? false ? "1" : "0";
            if (bbi.IsMildDyspnea30 != null)
                rblIsMildDyspnea30.SelectedValue = bbi.IsMildDyspnea30 ?? false ? "1" : "0";
            if (bbi.IsMildDyspnea60 != null)
                rblIsMildDyspnea60.SelectedValue = bbi.IsMildDyspnea60 ?? false ? "1" : "0";
            if (bbi.IsMildDyspnea120 != null)
                rblIsMildDyspnea120.SelectedValue = bbi.IsMildDyspnea120 ?? false ? "1" : "0";
            if (bbi.IsMildDyspnea180 != null)
                rblIsMildDyspnea180.SelectedValue = bbi.IsMildDyspnea180 ?? false ? "1" : "0";
            if (bbi.IsMildDyspnea240 != null)
                rblIsMildDyspnea240.SelectedValue = bbi.IsMildDyspnea240 ?? false ? "1" : "0";
            if (bbi.IsMildDyspneaPost != null)
                rblIsMildDyspneaPost.SelectedValue = bbi.IsMildDyspneaPost ?? false ? "1" : "0";
            if (bbi.IsMildDyspneaPost2 != null)
                rblIsMildDyspneaPost2.SelectedValue = bbi.IsMildDyspneaPost2 ?? false ? "1" : "0";
            if (bbi.IsMildDyspneaPost3 != null)
                rblIsMildDyspneaPost3.SelectedValue = bbi.IsMildDyspneaPost3 ?? false ? "1" : "0";
            if (bbi.IsMildDyspneaPost4 != null)
                rblIsMildDyspneaPost4.SelectedValue = bbi.IsMildDyspneaPost4 ?? false ? "1" : "0";
            if (bbi.IsMildDyspneaPost5 != null)
                rblIsMildDyspneaPost5.SelectedValue = bbi.IsMildDyspneaPost5 ?? false ? "1" : "0";
            if (bbi.IsMildDyspneaPost6 != null)
                rblIsMildDyspneaPost6.SelectedValue = bbi.IsMildDyspneaPost6 ?? false ? "1" : "0";
            if (bbi.IsMildDyspneaPost7 != null)
                rblIsMildDyspneaPost7.SelectedValue = bbi.IsMildDyspneaPost7 ?? false ? "1" : "0";
            if (bbi.IsMildDyspneaPost8 != null)
                rblIsMildDyspneaPost8.SelectedValue = bbi.IsMildDyspneaPost8 ?? false ? "1" : "0";

            if (bbi.IsHeadachePre != null)
                rblIsHeadachePre.SelectedValue = bbi.IsHeadachePre ?? false ? "1" : "0";
            if (bbi.IsHeadache10 != null)
                rblIsHeadache10.SelectedValue = bbi.IsHeadache10 ?? false ? "1" : "0";
            if (bbi.IsHeadache30 != null)
                rblIsHeadache30.SelectedValue = bbi.IsHeadache30 ?? false ? "1" : "0";
            if (bbi.IsHeadache60 != null)
                rblIsHeadache60.SelectedValue = bbi.IsHeadache60 ?? false ? "1" : "0";
            if (bbi.IsHeadache120 != null)
                rblIsHeadache120.SelectedValue = bbi.IsHeadache120 ?? false ? "1" : "0";
            if (bbi.IsHeadache180 != null)
                rblIsHeadache180.SelectedValue = bbi.IsHeadache180 ?? false ? "1" : "0";
            if (bbi.IsHeadache240 != null)
                rblIsHeadache240.SelectedValue = bbi.IsHeadache240 ?? false ? "1" : "0";
            if (bbi.IsHeadachePost != null)
                rblIsHeadachePost.SelectedValue = bbi.IsHeadachePost ?? false ? "1" : "0";
            if (bbi.IsHeadachePost2 != null)
                rblIsHeadachePost2.SelectedValue = bbi.IsHeadachePost2 ?? false ? "1" : "0";
            if (bbi.IsHeadachePost3 != null)
                rblIsHeadachePost3.SelectedValue = bbi.IsHeadachePost3 ?? false ? "1" : "0";
            if (bbi.IsHeadachePost4 != null)
                rblIsHeadachePost4.SelectedValue = bbi.IsHeadachePost4 ?? false ? "1" : "0";
            if (bbi.IsHeadachePost5 != null)
                rblIsHeadachePost5.SelectedValue = bbi.IsHeadachePost5 ?? false ? "1" : "0";
            if (bbi.IsHeadachePost6 != null)
                rblIsHeadachePost6.SelectedValue = bbi.IsHeadachePost6 ?? false ? "1" : "0";
            if (bbi.IsHeadachePost7 != null)
                rblIsHeadachePost7.SelectedValue = bbi.IsHeadachePost7 ?? false ? "1" : "0";
            if (bbi.IsHeadachePost8 != null)
                rblIsHeadachePost8.SelectedValue = bbi.IsHeadachePost8 ?? false ? "1" : "0";

            if (bbi.IsRednessOnTheSkinPre != null)
                rblIsRednessOnTheSkinPre.SelectedValue = bbi.IsRednessOnTheSkinPre ?? false ? "1" : "0";
            if (bbi.IsRednessOnTheSkin10 != null)
                rblIsRednessOnTheSkin10.SelectedValue = bbi.IsRednessOnTheSkin10 ?? false ? "1" : "0";
            if (bbi.IsRednessOnTheSkin30 != null)
                rblIsRednessOnTheSkin30.SelectedValue = bbi.IsRednessOnTheSkin30 ?? false ? "1" : "0";
            if (bbi.IsRednessOnTheSkin60 != null)
                rblIsRednessOnTheSkin60.SelectedValue = bbi.IsRednessOnTheSkin60 ?? false ? "1" : "0";
            if (bbi.IsRednessOnTheSkin120 != null)
                rblIsRednessOnTheSkin120.SelectedValue = bbi.IsRednessOnTheSkin120 ?? false ? "1" : "0";
            if (bbi.IsRednessOnTheSkin180 != null)
                rblIsRednessOnTheSkin180.SelectedValue = bbi.IsRednessOnTheSkin180 ?? false ? "1" : "0";
            if (bbi.IsRednessOnTheSkin240 != null)
                rblIsRednessOnTheSkin240.SelectedValue = bbi.IsRednessOnTheSkin240 ?? false ? "1" : "0";
            if (bbi.IsRednessOnTheSkinPost != null)
                rblIsRednessOnTheSkinPost.SelectedValue = bbi.IsRednessOnTheSkinPost ?? false ? "1" : "0";
            if (bbi.IsRednessOnTheSkinPost2 != null)
                rblIsRednessOnTheSkinPost2.SelectedValue = bbi.IsRednessOnTheSkinPost2 ?? false ? "1" : "0";
            if (bbi.IsRednessOnTheSkinPost3 != null)
                rblIsRednessOnTheSkinPost3.SelectedValue = bbi.IsRednessOnTheSkinPost3 ?? false ? "1" : "0";
            if (bbi.IsRednessOnTheSkinPost4 != null)
                rblIsRednessOnTheSkinPost4.SelectedValue = bbi.IsRednessOnTheSkinPost4 ?? false ? "1" : "0";
            if (bbi.IsRednessOnTheSkinPost5 != null)
                rblIsRednessOnTheSkinPost5.SelectedValue = bbi.IsRednessOnTheSkinPost5 ?? false ? "1" : "0";
            if (bbi.IsRednessOnTheSkinPost6 != null)
                rblIsRednessOnTheSkinPost6.SelectedValue = bbi.IsRednessOnTheSkinPost6 ?? false ? "1" : "0";
            if (bbi.IsRednessOnTheSkinPost7 != null)
                rblIsRednessOnTheSkinPost7.SelectedValue = bbi.IsRednessOnTheSkinPost7 ?? false ? "1" : "0";
            if (bbi.IsRednessOnTheSkinPost8 != null)
                rblIsRednessOnTheSkinPost8.SelectedValue = bbi.IsRednessOnTheSkinPost8 ?? false ? "1" : "0";

            if (bbi.IsTachycardiaPre != null)
                rblIsTachycardiaPre.SelectedValue = bbi.IsTachycardiaPre ?? false ? "1" : "0";
            if (bbi.IsTachycardia10 != null)
                rblIsTachycardia10.SelectedValue = bbi.IsTachycardia10 ?? false ? "1" : "0";
            if (bbi.IsTachycardia30 != null)
                rblIsTachycardia30.SelectedValue = bbi.IsTachycardia30 ?? false ? "1" : "0";
            if (bbi.IsTachycardia60 != null)
                rblIsTachycardia60.SelectedValue = bbi.IsTachycardia60 ?? false ? "1" : "0";
            if (bbi.IsTachycardia120 != null)
                rblIsTachycardia120.SelectedValue = bbi.IsTachycardia120 ?? false ? "1" : "0";
            if (bbi.IsTachycardia180 != null)
                rblIsTachycardia180.SelectedValue = bbi.IsTachycardia180 ?? false ? "1" : "0";
            if (bbi.IsTachycardia240 != null)
                rblIsTachycardia240.SelectedValue = bbi.IsTachycardia240 ?? false ? "1" : "0";
            if (bbi.IsTachycardiaPost != null)
                rblIsTachycardiaPost.SelectedValue = bbi.IsTachycardiaPost ?? false ? "1" : "0";
            if (bbi.IsTachycardiaPost2 != null)
                rblIsTachycardiaPost2.SelectedValue = bbi.IsTachycardiaPost2 ?? false ? "1" : "0";
            if (bbi.IsTachycardiaPost3 != null)
                rblIsTachycardiaPost3.SelectedValue = bbi.IsTachycardiaPost3 ?? false ? "1" : "0";
            if (bbi.IsTachycardiaPost4 != null)
                rblIsTachycardiaPost4.SelectedValue = bbi.IsTachycardiaPost4 ?? false ? "1" : "0";
            if (bbi.IsTachycardiaPost5 != null)
                rblIsTachycardiaPost5.SelectedValue = bbi.IsTachycardiaPost5 ?? false ? "1" : "0";
            if (bbi.IsTachycardiaPost6 != null)
                rblIsTachycardiaPost6.SelectedValue = bbi.IsTachycardiaPost6 ?? false ? "1" : "0";
            if (bbi.IsTachycardiaPost7 != null)
                rblIsTachycardiaPost7.SelectedValue = bbi.IsTachycardiaPost7 ?? false ? "1" : "0";
            if (bbi.IsTachycardiaPost8 != null)
                rblIsTachycardiaPost8.SelectedValue = bbi.IsTachycardiaPost8 ?? false ? "1" : "0";


            if (bbi.IsMuscleStiffnessPre != null)
                rblIsMuscleStiffnessPre.SelectedValue = bbi.IsMuscleStiffnessPre ?? false ? "1" : "0";
            if (bbi.IsMuscleStiffness10 != null)
                rblIsMuscleStiffness10.SelectedValue = bbi.IsMuscleStiffness10 ?? false ? "1" : "0";
            if (bbi.IsMuscleStiffness30 != null)
                rblIsMuscleStiffness30.SelectedValue = bbi.IsMuscleStiffness30 ?? false ? "1" : "0";
            if (bbi.IsMuscleStiffness60 != null)
                rblIsMuscleStiffness60.SelectedValue = bbi.IsMuscleStiffness60 ?? false ? "1" : "0";
            if (bbi.IsMuscleStiffness120 != null)
                rblIsMuscleStiffness120.SelectedValue = bbi.IsMuscleStiffness120 ?? false ? "1" : "0";
            if (bbi.IsMuscleStiffness180 != null)
                rblIsMuscleStiffness180.SelectedValue = bbi.IsMuscleStiffness180 ?? false ? "1" : "0";
            if (bbi.IsMuscleStiffness240 != null)
                rblIsMuscleStiffness240.SelectedValue = bbi.IsMuscleStiffness240 ?? false ? "1" : "0";
            if (bbi.IsMuscleStiffnessPost != null)
                rblIsMuscleStiffnessPost.SelectedValue = bbi.IsMuscleStiffnessPost ?? false ? "1" : "0";
            if (bbi.IsMuscleStiffnessPost2 != null)
                rblIsMuscleStiffnessPost2.SelectedValue = bbi.IsMuscleStiffnessPost2 ?? false ? "1" : "0";
            if (bbi.IsMuscleStiffnessPost3 != null)
                rblIsMuscleStiffnessPost3.SelectedValue = bbi.IsMuscleStiffnessPost3 ?? false ? "1" : "0";
            if (bbi.IsMuscleStiffnessPost4 != null)
                rblIsMuscleStiffnessPost4.SelectedValue = bbi.IsMuscleStiffnessPost4 ?? false ? "1" : "0";
            if (bbi.IsMuscleStiffnessPost5 != null)
                rblIsMuscleStiffnessPost5.SelectedValue = bbi.IsMuscleStiffnessPost5 ?? false ? "1" : "0";
            if (bbi.IsMuscleStiffnessPost6 != null)
                rblIsMuscleStiffnessPost6.SelectedValue = bbi.IsMuscleStiffnessPost6 ?? false ? "1" : "0";
            if (bbi.IsMuscleStiffnessPost7 != null)
                rblIsMuscleStiffnessPost7.SelectedValue = bbi.IsMuscleStiffnessPost7 ?? false ? "1" : "0";
            if (bbi.IsMuscleStiffnessPost8 != null)
                rblIsMuscleStiffnessPost8.SelectedValue = bbi.IsMuscleStiffnessPost8 ?? false ? "1" : "0";

            txtOtherReactionPre.Text = bbi.OtherReactionPre;
            txtOtherReaction10.Text = bbi.OtherReaction10;
            txtOtherReaction30.Text = bbi.OtherReaction30;
            txtOtherReaction60.Text = bbi.OtherReaction60;
            txtOtherReaction120.Text = bbi.OtherReaction120;
            txtOtherReaction180.Text = bbi.OtherReaction180;
            txtOtherReaction240.Text = bbi.OtherReaction240;
            txtOtherReactionPost.Text = bbi.OtherReactionPost;
            txtOtherReactionPost2.Text = bbi.OtherReactionPost2;
            txtOtherReactionPost3.Text = bbi.OtherReactionPost3;
            txtOtherReactionPost4.Text = bbi.OtherReactionPost4;
            txtOtherReactionPost5.Text = bbi.OtherReactionPost5;
            txtOtherReactionPost6.Text = bbi.OtherReactionPost6;
            txtOtherReactionPost7.Text = bbi.OtherReactionPost7;
            txtOtherReactionPost8.Text = bbi.OtherReactionPost8;
            #endregion

            #region Action
            txtHemoglobinPre.Value = Convert.ToDouble(bbi.HemoglobinPre);
            txtHemoglobin10.Value = Convert.ToDouble(bbi.Hemoglobin10);
            txtHemoglobin30.Value = Convert.ToDouble(bbi.Hemoglobin30);
            txtHemoglobin60.Value = Convert.ToDouble(bbi.Hemoglobin60);
            txtHemoglobin120.Value = Convert.ToDouble(bbi.Hemoglobin120);
            txtHemoglobin180.Value = Convert.ToDouble(bbi.Hemoglobin180);
            txtHemoglobin240.Value = Convert.ToDouble(bbi.Hemoglobin240);
            txtHemoglobinPost.Value = Convert.ToDouble(bbi.HemoglobinPost);
            txtHemoglobinPost2.Value = Convert.ToDouble(bbi.HemoglobinPost2);
            txtHemoglobinPost3.Value = Convert.ToDouble(bbi.HemoglobinPost3);

            txtHematocritPre.Value = Convert.ToDouble(bbi.HematocritPre);
            txtHematocrit10.Value = Convert.ToDouble(bbi.Hematocrit10);
            txtHematocrit30.Value = Convert.ToDouble(bbi.Hematocrit30);
            txtHematocrit60.Value = Convert.ToDouble(bbi.Hematocrit60);
            txtHematocrit120.Value = Convert.ToDouble(bbi.Hematocrit120);
            txtHematocrit180.Value = Convert.ToDouble(bbi.Hematocrit180);
            txtHematocrit240.Value = Convert.ToDouble(bbi.Hematocrit240);
            txtHematocritPost.Value = Convert.ToDouble(bbi.HematocritPost);
            txtHematocritPost2.Value = Convert.ToDouble(bbi.HematocritPost2);
            txtHematocritPost3.Value = Convert.ToDouble(bbi.HematocritPost3);

            txtPlateletPre.Value = Convert.ToDouble(bbi.PlateletPre);
            txtPlatelet10.Value = Convert.ToDouble(bbi.Platelet10);
            txtPlatelet30.Value = Convert.ToDouble(bbi.Platelet30);
            txtPlatelet60.Value = Convert.ToDouble(bbi.Platelet60);
            txtPlatelet120.Value = Convert.ToDouble(bbi.Platelet120);
            txtPlatelet180.Value = Convert.ToDouble(bbi.Platelet180);
            txtPlatelet240.Value = Convert.ToDouble(bbi.Platelet240);
            txtPlateletPost.Value = Convert.ToDouble(bbi.PlateletPost);
            txtPlateletPost2.Value = Convert.ToDouble(bbi.PlateletPost2);
            txtPlateletPost3.Value = Convert.ToDouble(bbi.PlateletPost3);

            txtActionPre.Text = bbi.ActionPre;
            txtAction10.Text = bbi.Action10;
            txtAction30.Text = bbi.Action30;
            txtAction60.Text = bbi.Action60;
            txtAction120.Text = bbi.Action120;
            txtAction180.Text = bbi.Action180;
            txtAction240.Text = bbi.Action240;
            txtActionPost.Text = bbi.ActionPost;
            txtActionPost2.Text = bbi.ActionPost2;
            txtActionPost3.Text = bbi.ActionPost3;

            if (string.IsNullOrEmpty(bbi.TransfusedOfficerStartBy))
                txtTransfusionStartDateTime.SelectedDate = (new DateTime()).NowAtSqlServer();
            else
                txtTransfusionStartDateTime.SelectedDate = bbi.TransfusionStartDateTime ?? (new DateTime()).NowAtSqlServer();
            if (string.IsNullOrEmpty(bbi.TransfusedOfficerEndBy))
                txtTransfusionEndDateTime.SelectedDate = (new DateTime()).NowAtSqlServer();
            else
                txtTransfusionEndDateTime.SelectedDate = bbi.TransfusionEndDateTime ?? (new DateTime()).NowAtSqlServer();
            txtQtyTransfusion.Value = Convert.ToDouble(bbi.QtyTransfusion ?? 1);
            txtTransfusedOfficerStartBy.Text = bbi.TransfusedOfficerStartBy;
            txtTransfusedOfficerEndBy.Text = bbi.TransfusedOfficerEndBy;
            #endregion
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(BloodBankTransaction entity, BloodBankTransactionItem entityItem)
        {
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entityItem.SRBloodBagStatus = "3";
            entityItem.BloodBagStatusName = "Transfused";

            #region Re-Check
            entityItem.IsHiv = chkIsHiv.Checked;
            entityItem.IsVbrl = chkIsVbrl.Checked;
            entityItem.IsHbsAg = chkIsHbsAg.Checked;
            entityItem.IsHcv = chkIsHcv.Checked;

            entityItem.IsReCheck = chkIsReCheck.Checked;

            if (txtReCheckDateTime.IsEmpty)
                entityItem.str.ReCheckDateTime = string.Empty;
            else
                entityItem.ReCheckDateTime = txtReCheckDateTime.SelectedDate;

            if (!string.IsNullOrEmpty(rblIsReCheckExpiredDate.SelectedValue))
                entityItem.IsReCheckExpiredDate = rblIsReCheckExpiredDate.SelectedValue == "1";
            else entityItem.IsReCheckExpiredDate = null;
            if (!string.IsNullOrEmpty(rblIsReCheckLeak.SelectedValue))
                entityItem.IsReCheckLeak = rblIsReCheckLeak.SelectedValue == "1";
            else entityItem.IsReCheckLeak = null;
            if (!string.IsNullOrEmpty(rblIsReCheckHemolysis.SelectedValue))
                entityItem.IsReCheckHemolysis = rblIsReCheckHemolysis.SelectedValue == "1";
            else entityItem.IsReCheckHemolysis = null;
            if (!string.IsNullOrEmpty(rblIsReCheckClotting.SelectedValue))
                entityItem.IsReCheckClotting = rblIsReCheckClotting.SelectedValue == "1";
            else entityItem.IsReCheckClotting = null;
            if (!string.IsNullOrEmpty(rblIsReCheckBloodTypeCompatibility.SelectedValue))
                entityItem.IsReCheckBloodTypeCompatibility = rblIsReCheckBloodTypeCompatibility.SelectedValue == "1";
            else entityItem.IsReCheckBloodTypeCompatibility = null;
            entityItem.ReCheckOfficer = txtReCheckOfficer.Text;
            entityItem.ReCheckOfficer2 = txtReCheckOfficer2.Text;
            entityItem.Notes = txtNotes.Text;
            #endregion

            #region Vital Signs
            entityItem.BpPre = txtBpPre.Text;
            entityItem.Bp10 = txtBp10.Text;
            entityItem.Bp30 = txtBp30.Text;
            entityItem.Bp60 = txtBp60.Text;
            entityItem.Bp120 = txtBp120.Text;
            entityItem.Bp180 = txtBp180.Text;
            entityItem.Bp240 = txtBp240.Text;
            entityItem.BpPost = txtBpPost.Text;
            entityItem.BpPost2 = txtBpPost2.Text;
            entityItem.BpPost3 = txtBpPost3.Text;
            entityItem.Bp31 = txtBp31.Text;
            entityItem.Bp32 = txtBp32.Text;
            entityItem.Bp33 = txtBp33.Text;
            entityItem.Bp34 = txtBp34.Text;
            entityItem.BpPost4 = txtBpPost4.Text;


            entityItem.PulsePre = Convert.ToDecimal(txtPulsePre.Value);
            entityItem.Pulse10 = Convert.ToDecimal(txtPulse10.Value);
            entityItem.Pulse30 = Convert.ToDecimal(txtPulse30.Value);
            entityItem.Pulse60 = Convert.ToDecimal(txtPulse60.Value);
            entityItem.Pulse120 = Convert.ToDecimal(txtPulse120.Value);
            entityItem.Pulse180 = Convert.ToDecimal(txtPulse180.Value);
            entityItem.Pulse240 = Convert.ToDecimal(txtPulse240.Value);
            entityItem.PulsePost = Convert.ToDecimal(txtPulsePost.Value);
            entityItem.PulsePost2 = Convert.ToDecimal(txtPulsePost2.Value);
            entityItem.PulsePost3 = Convert.ToDecimal(txtPulsePost3.Value);
            entityItem.Pulse31 = Convert.ToDecimal(txtPulse31.Value);
            entityItem.Pulse32 = Convert.ToDecimal(txtPulse32.Value);
            entityItem.Pulse33 = Convert.ToDecimal(txtPulse33.Value);
            entityItem.Pulse34 = Convert.ToDecimal(txtPulse34.Value);
            entityItem.PulsePost4 = Convert.ToDecimal(txtPulsePost4.Value);

            entityItem.TemperaturePre = Convert.ToDecimal(txtTemperaturePre.Value);
            entityItem.Temperature10 = Convert.ToDecimal(txtTemperature10.Value);
            entityItem.Temperature30 = Convert.ToDecimal(txtTemperature30.Value);
            entityItem.Temperature60 = Convert.ToDecimal(txtTemperature60.Value);
            entityItem.Temperature120 = Convert.ToDecimal(txtTemperature120.Value);
            entityItem.Temperature180 = Convert.ToDecimal(txtTemperature180.Value);
            entityItem.Temperature240 = Convert.ToDecimal(txtTemperature240.Value);
            entityItem.TemperaturePost = Convert.ToDecimal(txtTemperaturePost.Value);
            entityItem.TemperaturePost2 = Convert.ToDecimal(txtTemperaturePost2.Value);
            entityItem.TemperaturePost3 = Convert.ToDecimal(txtTemperaturePost3.Value);
            entityItem.Temperature31 = Convert.ToDecimal(txtTemperature31.Value);
            entityItem.Temperature32 = Convert.ToDecimal(txtTemperature32.Value);
            entityItem.Temperature33 = Convert.ToDecimal(txtTemperature33.Value);
            entityItem.Temperature34 = Convert.ToDecimal(txtTemperature34.Value);
            entityItem.TemperaturePost4 = Convert.ToDecimal(txtTemperaturePost4.Value);

            entityItem.RespiratoryPre = Convert.ToDecimal(txtRespiratoryPre.Value);
            entityItem.Respiratory10 = Convert.ToDecimal(txtRespiratory10.Value);
            entityItem.Respiratory30 = Convert.ToDecimal(txtRespiratory30.Value);
            entityItem.Respiratory60 = Convert.ToDecimal(txtRespiratory60.Value);
            entityItem.Respiratory120 = Convert.ToDecimal(txtRespiratory120.Value);
            entityItem.Respiratory180 = Convert.ToDecimal(txtRespiratory180.Value);
            entityItem.Respiratory240 = Convert.ToDecimal(txtRespiratory240.Value);
            entityItem.RespiratoryPost = Convert.ToDecimal(txtRespiratoryPost.Value);
            entityItem.RespiratoryPost2 = Convert.ToDecimal(txtRespiratoryPost2.Value);
            entityItem.RespiratoryPost3 = Convert.ToDecimal(txtRespiratoryPost3.Value);
            entityItem.Respiratory31 = Convert.ToDecimal(txtRespiratory31.Value);
            entityItem.Respiratory32 = Convert.ToDecimal(txtRespiratory32.Value);
            entityItem.Respiratory33 = Convert.ToDecimal(txtRespiratory33.Value);
            entityItem.Respiratory34 = Convert.ToDecimal(txtRespiratory34.Value);
            entityItem.RespiratoryPost4 = Convert.ToDecimal(txtRespiratoryPost4.Value);
            #endregion

            #region Reaction
            if (!string.IsNullOrEmpty(rblIsFeverPre.SelectedValue))
                entityItem.IsFeverPre = rblIsFeverPre.SelectedValue == "1";
            else entityItem.IsFeverPre = null;
            if (!string.IsNullOrEmpty(rblIsFever10.SelectedValue))
                entityItem.IsFever10 = rblIsFever10.SelectedValue == "1";
            else entityItem.IsFever10 = null;
            if (!string.IsNullOrEmpty(rblIsFever30.SelectedValue))
                entityItem.IsFever30 = rblIsFever30.SelectedValue == "1";
            else entityItem.IsFever30 = null;
            if (!string.IsNullOrEmpty(rblIsFever60.SelectedValue))
                entityItem.IsFever60 = rblIsFever60.SelectedValue == "1";
            else entityItem.IsFever60 = null;
            if (!string.IsNullOrEmpty(rblIsFever120.SelectedValue))
                entityItem.IsFever120 = rblIsFever120.SelectedValue == "1";
            else entityItem.IsFever120 = null;
            if (!string.IsNullOrEmpty(rblIsFever180.SelectedValue))
                entityItem.IsFever180 = rblIsFever180.SelectedValue == "1";
            else entityItem.IsFever180 = null;
            if (!string.IsNullOrEmpty(rblIsFever240.SelectedValue))
                entityItem.IsFever240 = rblIsFever240.SelectedValue == "1";
            else entityItem.IsFever240 = null;
            if (!string.IsNullOrEmpty(rblIsFeverPost.SelectedValue))
                entityItem.IsFeverPost = rblIsFeverPost.SelectedValue == "1";
            else entityItem.IsFeverPost = null;
            if (!string.IsNullOrEmpty(rblIsFeverPost2.SelectedValue))
                entityItem.IsFeverPost2 = rblIsFeverPost2.SelectedValue == "1";
            else entityItem.IsFeverPost2 = null;
            if (!string.IsNullOrEmpty(rblIsFeverPost3.SelectedValue))
                entityItem.IsFeverPost3 = rblIsFeverPost3.SelectedValue == "1";
            else entityItem.IsFeverPost3 = null;
            if (!string.IsNullOrEmpty(rblIsFeverPost4.SelectedValue))
                entityItem.IsFeverPost4 = rblIsFeverPost4.SelectedValue == "1";
            else entityItem.IsFeverPost4 = null;
            if (!string.IsNullOrEmpty(rblIsFeverPost5.SelectedValue))
                entityItem.IsFeverPost5 = rblIsFeverPost5.SelectedValue == "1";
            else entityItem.IsFeverPost5 = null;
            if (!string.IsNullOrEmpty(rblIsFeverPost6.SelectedValue))
                entityItem.IsFeverPost6 = rblIsFeverPost6.SelectedValue == "1";
            else entityItem.IsFeverPost6 = null;
            if (!string.IsNullOrEmpty(rblIsFeverPost7.SelectedValue))
                entityItem.IsFeverPost7 = rblIsFeverPost7.SelectedValue == "1";
            else entityItem.IsFeverPost7 = null;
            if (!string.IsNullOrEmpty(rblIsFeverPost8.SelectedValue))
                entityItem.IsFeverPost8 = rblIsFeverPost8.SelectedValue == "1";
            else entityItem.IsFeverPost8 = null;

            if (!string.IsNullOrEmpty(rblIsShiveringPre.SelectedValue))
                entityItem.IsShiveringPre = rblIsShiveringPre.SelectedValue == "1";
            else entityItem.IsShiveringPre = null;
            if (!string.IsNullOrEmpty(rblIsShivering10.SelectedValue))
                entityItem.IsShivering10 = rblIsShivering10.SelectedValue == "1";
            else entityItem.IsShivering10 = null;
            if (!string.IsNullOrEmpty(rblIsShivering30.SelectedValue))
                entityItem.IsShivering30 = rblIsShivering30.SelectedValue == "1";
            else entityItem.IsShivering30 = null;
            if (!string.IsNullOrEmpty(rblIsShivering60.SelectedValue))
                entityItem.IsShivering60 = rblIsShivering60.SelectedValue == "1";
            else entityItem.IsShivering60 = null;
            if (!string.IsNullOrEmpty(rblIsShivering120.SelectedValue))
                entityItem.IsShivering120 = rblIsShivering120.SelectedValue == "1";
            else entityItem.IsShivering120 = null;
            if (!string.IsNullOrEmpty(rblIsShivering180.SelectedValue))
                entityItem.IsShivering180 = rblIsShivering180.SelectedValue == "1";
            else entityItem.IsShivering180 = null;
            if (!string.IsNullOrEmpty(rblIsShivering240.SelectedValue))
                entityItem.IsShivering240 = rblIsShivering240.SelectedValue == "1";
            else entityItem.IsShivering240 = null;
            if (!string.IsNullOrEmpty(rblIsShiveringPost.SelectedValue))
                entityItem.IsShiveringPost = rblIsShiveringPost.SelectedValue == "1";
            else entityItem.IsShiveringPost = null;
            if (!string.IsNullOrEmpty(rblIsShiveringPost2.SelectedValue))
                entityItem.IsShiveringPost2 = rblIsShiveringPost2.SelectedValue == "1";
            else entityItem.IsShiveringPost2 = null;
            if (!string.IsNullOrEmpty(rblIsShiveringPost3.SelectedValue))
                entityItem.IsShiveringPost3 = rblIsShiveringPost3.SelectedValue == "1";
            else entityItem.IsShiveringPost3 = null;
            if (!string.IsNullOrEmpty(rblIsShiveringPost4.SelectedValue))
                entityItem.IsShiveringPost4 = rblIsShiveringPost4.SelectedValue == "1";
            else entityItem.IsShiveringPost4 = null;
            if (!string.IsNullOrEmpty(rblIsShiveringPost5.SelectedValue))
                entityItem.IsShiveringPost5 = rblIsShiveringPost5.SelectedValue == "1";
            else entityItem.IsShiveringPost5 = null;
            if (!string.IsNullOrEmpty(rblIsShiveringPost6.SelectedValue))
                entityItem.IsShiveringPost6 = rblIsShiveringPost6.SelectedValue == "1";
            else entityItem.IsShiveringPost6 = null;
            if (!string.IsNullOrEmpty(rblIsShiveringPost7.SelectedValue))
                entityItem.IsShiveringPost7 = rblIsShiveringPost7.SelectedValue == "1";
            else entityItem.IsShiveringPost7 = null;
            if (!string.IsNullOrEmpty(rblIsShiveringPost8.SelectedValue))
                entityItem.IsShiveringPost8 = rblIsShiveringPost8.SelectedValue == "1";
            else entityItem.IsShiveringPost8 = null;

            if (!string.IsNullOrEmpty(rblIsSobPre.SelectedValue))
                entityItem.IsSobPre = rblIsSobPre.SelectedValue == "1";
            else entityItem.IsSobPre = null;
            if (!string.IsNullOrEmpty(rblIsSob10.SelectedValue))
                entityItem.IsSob10 = rblIsSob10.SelectedValue == "1";
            else entityItem.IsSob10 = null;
            if (!string.IsNullOrEmpty(rblIsSob30.SelectedValue))
                entityItem.IsSob30 = rblIsSob30.SelectedValue == "1";
            else entityItem.IsSob30 = null;
            if (!string.IsNullOrEmpty(rblIsSob60.SelectedValue))
                entityItem.IsSob60 = rblIsSob60.SelectedValue == "1";
            else entityItem.IsSob60 = null;
            if (!string.IsNullOrEmpty(rblIsSob120.SelectedValue))
                entityItem.IsSob120 = rblIsSob120.SelectedValue == "1";
            else entityItem.IsSob120 = null;
            if (!string.IsNullOrEmpty(rblIsSob180.SelectedValue))
                entityItem.IsSob180 = rblIsSob180.SelectedValue == "1";
            else entityItem.IsSob180 = null;
            if (!string.IsNullOrEmpty(rblIsSob240.SelectedValue))
                entityItem.IsSob240 = rblIsSob240.SelectedValue == "1";
            else entityItem.IsSob240 = null;
            if (!string.IsNullOrEmpty(rblIsSobPost.SelectedValue))
                entityItem.IsSobPost = rblIsSobPost.SelectedValue == "1";
            else entityItem.IsSobPost = null;
            if (!string.IsNullOrEmpty(rblIsSobPost2.SelectedValue))
                entityItem.IsSobPost2 = rblIsSobPost2.SelectedValue == "1";
            else entityItem.IsSobPost2 = null;
            if (!string.IsNullOrEmpty(rblIsSobPost3.SelectedValue))
                entityItem.IsSobPost3 = rblIsSobPost3.SelectedValue == "1";
            else entityItem.IsSobPost3 = null;
            if (!string.IsNullOrEmpty(rblIsSobPost4.SelectedValue))
                entityItem.IsSobPost4 = rblIsSobPost4.SelectedValue == "1";
            else entityItem.IsSobPost4 = null;
            if (!string.IsNullOrEmpty(rblIsSobPost5.SelectedValue))
                entityItem.IsSobPost5 = rblIsSobPost5.SelectedValue == "1";
            else entityItem.IsSobPost5 = null;
            if (!string.IsNullOrEmpty(rblIsSobPost6.SelectedValue))
                entityItem.IsSobPost6 = rblIsSobPost6.SelectedValue == "1";
            else entityItem.IsSobPost6 = null;
            if (!string.IsNullOrEmpty(rblIsSobPost7.SelectedValue))
                entityItem.IsSobPost7 = rblIsSobPost7.SelectedValue == "1";
            else entityItem.IsSobPost7 = null;
            if (!string.IsNullOrEmpty(rblIsSobPost8.SelectedValue))
                entityItem.IsSobPost8 = rblIsSobPost8.SelectedValue == "1";
            else entityItem.IsSobPost8 = null;

            if (!string.IsNullOrEmpty(rblIsPainfulPre.SelectedValue))
                entityItem.IsPainfulPre = rblIsPainfulPre.SelectedValue == "1";
            else entityItem.IsPainfulPre = null;
            if (!string.IsNullOrEmpty(rblIsPainful10.SelectedValue))
                entityItem.IsPainful10 = rblIsPainful10.SelectedValue == "1";
            else entityItem.IsPainful10 = null;
            if (!string.IsNullOrEmpty(rblIsPainful30.SelectedValue))
                entityItem.IsPainful30 = rblIsPainful30.SelectedValue == "1";
            else entityItem.IsPainful30 = null;
            if (!string.IsNullOrEmpty(rblIsPainful60.SelectedValue))
                entityItem.IsPainful60 = rblIsPainful60.SelectedValue == "1";
            else entityItem.IsPainful60 = null;
            if (!string.IsNullOrEmpty(rblIsPainful120.SelectedValue))
                entityItem.IsPainful120 = rblIsPainful120.SelectedValue == "1";
            else entityItem.IsPainful120 = null;
            if (!string.IsNullOrEmpty(rblIsPainful180.SelectedValue))
                entityItem.IsPainful180 = rblIsPainful180.SelectedValue == "1";
            else entityItem.IsPainful180 = null;
            if (!string.IsNullOrEmpty(rblIsPainful240.SelectedValue))
                entityItem.IsPainful240 = rblIsPainful240.SelectedValue == "1";
            else entityItem.IsPainful240 = null;
            if (!string.IsNullOrEmpty(rblIsPainfulPost.SelectedValue))
                entityItem.IsPainfulPost = rblIsPainfulPost.SelectedValue == "1";
            else entityItem.IsPainfulPost = null;
            if (!string.IsNullOrEmpty(rblIsPainfulPost2.SelectedValue))
                entityItem.IsPainfulPost2 = rblIsPainfulPost2.SelectedValue == "1";
            else entityItem.IsPainfulPost2 = null;
            if (!string.IsNullOrEmpty(rblIsPainfulPost3.SelectedValue))
                entityItem.IsPainfulPost3 = rblIsPainfulPost3.SelectedValue == "1";
            else entityItem.IsPainfulPost3 = null;
            if (!string.IsNullOrEmpty(rblIsPainfulPost4.SelectedValue))
                entityItem.IsPainfulPost4 = rblIsPainfulPost4.SelectedValue == "1";
            else entityItem.IsPainfulPost4 = null;
            if (!string.IsNullOrEmpty(rblIsPainfulPost5.SelectedValue))
                entityItem.IsPainfulPost5 = rblIsPainfulPost5.SelectedValue == "1";
            else entityItem.IsPainfulPost5 = null;
            if (!string.IsNullOrEmpty(rblIsPainfulPost6.SelectedValue))
                entityItem.IsPainfulPost6 = rblIsPainfulPost6.SelectedValue == "1";
            else entityItem.IsPainfulPost6 = null;
            if (!string.IsNullOrEmpty(rblIsPainfulPost7.SelectedValue))
                entityItem.IsPainfulPost7 = rblIsPainfulPost7.SelectedValue == "1";
            else entityItem.IsPainfulPost7 = null;
            if (!string.IsNullOrEmpty(rblIsPainfulPost8.SelectedValue))
                entityItem.IsPainfulPost8 = rblIsPainfulPost8.SelectedValue == "1";
            else entityItem.IsPainfulPost8 = null;

            if (!string.IsNullOrEmpty(rblIsNauseaPre.SelectedValue))
                entityItem.IsNauseaPre = rblIsNauseaPre.SelectedValue == "1";
            else entityItem.IsNauseaPre = null;
            if (!string.IsNullOrEmpty(rblIsNausea10.SelectedValue))
                entityItem.IsNausea10 = rblIsNausea10.SelectedValue == "1";
            else entityItem.IsNausea10 = null;
            if (!string.IsNullOrEmpty(rblIsNausea30.SelectedValue))
                entityItem.IsNausea30 = rblIsNausea30.SelectedValue == "1";
            else entityItem.IsNausea30 = null;
            if (!string.IsNullOrEmpty(rblIsNausea60.SelectedValue))
                entityItem.IsNausea60 = rblIsNausea60.SelectedValue == "1";
            else entityItem.IsNausea60 = null;
            if (!string.IsNullOrEmpty(rblIsNausea120.SelectedValue))
                entityItem.IsNausea120 = rblIsNausea120.SelectedValue == "1";
            else entityItem.IsNausea120 = null;
            if (!string.IsNullOrEmpty(rblIsNausea180.SelectedValue))
                entityItem.IsNausea180 = rblIsNausea180.SelectedValue == "1";
            else entityItem.IsNausea180 = null;
            if (!string.IsNullOrEmpty(rblIsNausea240.SelectedValue))
                entityItem.IsNausea240 = rblIsNausea240.SelectedValue == "1";
            else entityItem.IsNausea240 = null;
            if (!string.IsNullOrEmpty(rblIsNauseaPost.SelectedValue))
                entityItem.IsNauseaPost = rblIsNauseaPost.SelectedValue == "1";
            else entityItem.IsNauseaPost = null;
            if (!string.IsNullOrEmpty(rblIsNauseaPost2.SelectedValue))
                entityItem.IsNauseaPost2 = rblIsNauseaPost2.SelectedValue == "1";
            else entityItem.IsNauseaPost2 = null;
            if (!string.IsNullOrEmpty(rblIsNauseaPost3.SelectedValue))
                entityItem.IsNauseaPost3 = rblIsNauseaPost3.SelectedValue == "1";
            else entityItem.IsNauseaPost3 = null;
            if (!string.IsNullOrEmpty(rblIsNauseaPost4.SelectedValue))
                entityItem.IsNauseaPost4 = rblIsNauseaPost4.SelectedValue == "1";
            else entityItem.IsNauseaPost4 = null;
            if (!string.IsNullOrEmpty(rblIsNauseaPost5.SelectedValue))
                entityItem.IsNauseaPost5 = rblIsNauseaPost5.SelectedValue == "1";
            else entityItem.IsNauseaPost5 = null;
            if (!string.IsNullOrEmpty(rblIsNauseaPost6.SelectedValue))
                entityItem.IsNauseaPost6 = rblIsNauseaPost6.SelectedValue == "1";
            else entityItem.IsNauseaPost6 = null;
            if (!string.IsNullOrEmpty(rblIsNauseaPost7.SelectedValue))
                entityItem.IsNauseaPost7 = rblIsNauseaPost7.SelectedValue == "1";
            else entityItem.IsNauseaPost7 = null;
            if (!string.IsNullOrEmpty(rblIsNauseaPost8.SelectedValue))
                entityItem.IsNauseaPost8 = rblIsNauseaPost8.SelectedValue == "1";
            else entityItem.IsNauseaPost8 = null;

            if (!string.IsNullOrEmpty(rblIsBleedingPre.SelectedValue))
                entityItem.IsBleedingPre = rblIsBleedingPre.SelectedValue == "1";
            else entityItem.IsBleedingPre = null;
            if (!string.IsNullOrEmpty(rblIsBleeding10.SelectedValue))
                entityItem.IsBleeding10 = rblIsBleeding10.SelectedValue == "1";
            else entityItem.IsBleeding10 = null;
            if (!string.IsNullOrEmpty(rblIsBleeding30.SelectedValue))
                entityItem.IsBleeding30 = rblIsBleeding30.SelectedValue == "1";
            else entityItem.IsBleeding30 = null;
            if (!string.IsNullOrEmpty(rblIsBleeding60.SelectedValue))
                entityItem.IsBleeding60 = rblIsBleeding60.SelectedValue == "1";
            else entityItem.IsBleeding60 = null;
            if (!string.IsNullOrEmpty(rblIsBleeding120.SelectedValue))
                entityItem.IsBleeding120 = rblIsBleeding120.SelectedValue == "1";
            else entityItem.IsBleeding120 = null;
            if (!string.IsNullOrEmpty(rblIsBleeding180.SelectedValue))
                entityItem.IsBleeding180 = rblIsBleeding180.SelectedValue == "1";
            else entityItem.IsBleeding180 = null;
            if (!string.IsNullOrEmpty(rblIsBleeding240.SelectedValue))
                entityItem.IsBleeding240 = rblIsBleeding240.SelectedValue == "1";
            else entityItem.IsBleeding240 = null;
            if (!string.IsNullOrEmpty(rblIsBleedingPost.SelectedValue))
                entityItem.IsBleedingPost = rblIsBleedingPost.SelectedValue == "1";
            else entityItem.IsBleedingPost = null;
            if (!string.IsNullOrEmpty(rblIsBleedingPost2.SelectedValue))
                entityItem.IsBleedingPost2 = rblIsBleedingPost2.SelectedValue == "1";
            else entityItem.IsBleedingPost2 = null;
            if (!string.IsNullOrEmpty(rblIsBleedingPost3.SelectedValue))
                entityItem.IsBleedingPost3 = rblIsBleedingPost3.SelectedValue == "1";
            else entityItem.IsBleedingPost3 = null;
            if (!string.IsNullOrEmpty(rblIsBleedingPost4.SelectedValue))
                entityItem.IsBleedingPost4 = rblIsBleedingPost4.SelectedValue == "1";
            else entityItem.IsBleedingPost4 = null;
            if (!string.IsNullOrEmpty(rblIsBleedingPost5.SelectedValue))
                entityItem.IsBleedingPost5 = rblIsBleedingPost5.SelectedValue == "1";
            else entityItem.IsBleedingPost5 = null;
            if (!string.IsNullOrEmpty(rblIsBleedingPost6.SelectedValue))
                entityItem.IsBleedingPost6 = rblIsBleedingPost6.SelectedValue == "1";
            else entityItem.IsBleedingPost6 = null;
            if (!string.IsNullOrEmpty(rblIsBleedingPost7.SelectedValue))
                entityItem.IsBleedingPost7 = rblIsBleedingPost7.SelectedValue == "1";
            else entityItem.IsBleedingPost7 = null;
            if (!string.IsNullOrEmpty(rblIsBleedingPost8.SelectedValue))
                entityItem.IsBleedingPost8 = rblIsBleedingPost8.SelectedValue == "1";
            else entityItem.IsBleedingPost8 = null;

            if (!string.IsNullOrEmpty(rblIsHypotensionPre.SelectedValue))
                entityItem.IsHypotensionPre = rblIsHypotensionPre.SelectedValue == "1";
            else entityItem.IsHypotensionPre = null;
            if (!string.IsNullOrEmpty(rblIsHypotension10.SelectedValue))
                entityItem.IsHypotension10 = rblIsHypotension10.SelectedValue == "1";
            else entityItem.IsHypotension10 = null;
            if (!string.IsNullOrEmpty(rblIsHypotension30.SelectedValue))
                entityItem.IsHypotension30 = rblIsHypotension30.SelectedValue == "1";
            else entityItem.IsHypotension30 = null;
            if (!string.IsNullOrEmpty(rblIsHypotension60.SelectedValue))
                entityItem.IsHypotension60 = rblIsHypotension60.SelectedValue == "1";
            else entityItem.IsHypotension60 = null;
            if (!string.IsNullOrEmpty(rblIsHypotension120.SelectedValue))
                entityItem.IsHypotension120 = rblIsHypotension120.SelectedValue == "1";
            else entityItem.IsHypotension120 = null;
            if (!string.IsNullOrEmpty(rblIsHypotension180.SelectedValue))
                entityItem.IsHypotension180 = rblIsHypotension180.SelectedValue == "1";
            else entityItem.IsHypotension180 = null;
            if (!string.IsNullOrEmpty(rblIsHypotension240.SelectedValue))
                entityItem.IsHypotension240 = rblIsHypotension240.SelectedValue == "1";
            else entityItem.IsHypotension240 = null;
            if (!string.IsNullOrEmpty(rblIsHypotensionPost.SelectedValue))
                entityItem.IsHypotensionPost = rblIsHypotensionPost.SelectedValue == "1";
            else entityItem.IsHypotensionPost = null;
            if (!string.IsNullOrEmpty(rblIsHypotensionPost2.SelectedValue))
                entityItem.IsHypotensionPost2 = rblIsHypotensionPost2.SelectedValue == "1";
            else entityItem.IsHypotensionPost2 = null;
            if (!string.IsNullOrEmpty(rblIsHypotensionPost3.SelectedValue))
                entityItem.IsHypotensionPost3 = rblIsHypotensionPost3.SelectedValue == "1";
            else entityItem.IsHypotensionPost3 = null;
            if (!string.IsNullOrEmpty(rblIsHypotensionPost4.SelectedValue))
                entityItem.IsHypotensionPost4 = rblIsHypotensionPost4.SelectedValue == "1";
            else entityItem.IsHypotensionPost4 = null;
            if (!string.IsNullOrEmpty(rblIsHypotensionPost5.SelectedValue))
                entityItem.IsHypotensionPost5 = rblIsHypotensionPost5.SelectedValue == "1";
            else entityItem.IsHypotensionPost5 = null;
            if (!string.IsNullOrEmpty(rblIsHypotensionPost6.SelectedValue))
                entityItem.IsHypotensionPost6 = rblIsHypotensionPost6.SelectedValue == "1";
            else entityItem.IsHypotensionPost6 = null;
            if (!string.IsNullOrEmpty(rblIsHypotensionPost7.SelectedValue))
                entityItem.IsHypotensionPost7 = rblIsHypotensionPost7.SelectedValue == "1";
            else entityItem.IsHypotensionPost7 = null;
            if (!string.IsNullOrEmpty(rblIsHypotensionPost8.SelectedValue))
                entityItem.IsHypotensionPost8 = rblIsHypotensionPost8.SelectedValue == "1";
            else entityItem.IsHypotensionPost8 = null;

            if (!string.IsNullOrEmpty(rblIsShockPre.SelectedValue))
                entityItem.IsShockPre = rblIsShockPre.SelectedValue == "1";
            else entityItem.IsShockPre = null;
            if (!string.IsNullOrEmpty(rblIsShock10.SelectedValue))
                entityItem.IsShock10 = rblIsShock10.SelectedValue == "1";
            else entityItem.IsShock10 = null;
            if (!string.IsNullOrEmpty(rblIsShock30.SelectedValue))
                entityItem.IsShock30 = rblIsShock30.SelectedValue == "1";
            else entityItem.IsShock30 = null;
            if (!string.IsNullOrEmpty(rblIsShock60.SelectedValue))
                entityItem.IsShock60 = rblIsShock60.SelectedValue == "1";
            else entityItem.IsShock60 = null;
            if (!string.IsNullOrEmpty(rblIsShock120.SelectedValue))
                entityItem.IsShock120 = rblIsShock120.SelectedValue == "1";
            else entityItem.IsShock120 = null;
            if (!string.IsNullOrEmpty(rblIsShock180.SelectedValue))
                entityItem.IsShock180 = rblIsShock180.SelectedValue == "1";
            else entityItem.IsShock180 = null;
            if (!string.IsNullOrEmpty(rblIsShock240.SelectedValue))
                entityItem.IsShock240 = rblIsShock240.SelectedValue == "1";
            else entityItem.IsShock240 = null;
            if (!string.IsNullOrEmpty(rblIsShockPost.SelectedValue))
                entityItem.IsShockPost = rblIsShockPost.SelectedValue == "1";
            else entityItem.IsShockPost = null;
            if (!string.IsNullOrEmpty(rblIsShockPost2.SelectedValue))
                entityItem.IsShockPost2 = rblIsShockPost2.SelectedValue == "1";
            else entityItem.IsShockPost2 = null;
            if (!string.IsNullOrEmpty(rblIsShockPost3.SelectedValue))
                entityItem.IsShockPost3 = rblIsShockPost3.SelectedValue == "1";
            else entityItem.IsShockPost3 = null;
            if (!string.IsNullOrEmpty(rblIsShockPost4.SelectedValue))
                entityItem.IsShockPost4 = rblIsShockPost4.SelectedValue == "1";
            else entityItem.IsShockPost4 = null;
            if (!string.IsNullOrEmpty(rblIsShockPost5.SelectedValue))
                entityItem.IsShockPost5 = rblIsShockPost5.SelectedValue == "1";
            else entityItem.IsShockPost5 = null;
            if (!string.IsNullOrEmpty(rblIsShockPost6.SelectedValue))
                entityItem.IsShockPost6 = rblIsShockPost6.SelectedValue == "1";
            else entityItem.IsShockPost6 = null;
            if (!string.IsNullOrEmpty(rblIsShockPost7.SelectedValue))
                entityItem.IsShockPost7 = rblIsShockPost7.SelectedValue == "1";
            else entityItem.IsShockPost7 = null;
            if (!string.IsNullOrEmpty(rblIsShockPost8.SelectedValue))
                entityItem.IsShockPost8 = rblIsShockPost8.SelectedValue == "1";
            else entityItem.IsShockPost8 = null;

            if (!string.IsNullOrEmpty(rblIsUrticariaPre.SelectedValue))
                entityItem.IsUrticariaPre = rblIsUrticariaPre.SelectedValue == "1";
            else entityItem.IsUrticariaPre = null;
            if (!string.IsNullOrEmpty(rblIsUrticaria10.SelectedValue))
                entityItem.IsUrticaria10 = rblIsUrticaria10.SelectedValue == "1";
            else entityItem.IsUrticaria10 = null;
            if (!string.IsNullOrEmpty(rblIsUrticaria30.SelectedValue))
                entityItem.IsUrticaria30 = rblIsUrticaria30.SelectedValue == "1";
            else entityItem.IsUrticaria30 = null;
            if (!string.IsNullOrEmpty(rblIsUrticaria60.SelectedValue))
                entityItem.IsUrticaria60 = rblIsUrticaria60.SelectedValue == "1";
            else entityItem.IsUrticaria60 = null;
            if (!string.IsNullOrEmpty(rblIsUrticaria120.SelectedValue))
                entityItem.IsUrticaria120 = rblIsUrticaria120.SelectedValue == "1";
            else entityItem.IsUrticaria120 = null;
            if (!string.IsNullOrEmpty(rblIsUrticaria180.SelectedValue))
                entityItem.IsUrticaria180 = rblIsUrticaria180.SelectedValue == "1";
            else entityItem.IsUrticaria180 = null;
            if (!string.IsNullOrEmpty(rblIsUrticaria240.SelectedValue))
                entityItem.IsUrticaria240 = rblIsUrticaria240.SelectedValue == "1";
            else entityItem.IsUrticaria240 = null;
            if (!string.IsNullOrEmpty(rblIsUrticariaPost.SelectedValue))
                entityItem.IsUrticariaPost = rblIsUrticariaPost.SelectedValue == "1";
            else entityItem.IsUrticariaPost = null;
            if (!string.IsNullOrEmpty(rblIsUrticariaPost2.SelectedValue))
                entityItem.IsUrticariaPost2 = rblIsUrticariaPost2.SelectedValue == "1";
            else entityItem.IsUrticariaPost2 = null;
            if (!string.IsNullOrEmpty(rblIsUrticariaPost3.SelectedValue))
                entityItem.IsUrticariaPost3 = rblIsUrticariaPost3.SelectedValue == "1";
            else entityItem.IsUrticariaPost3 = null;
            if (!string.IsNullOrEmpty(rblIsUrticariaPost4.SelectedValue))
                entityItem.IsUrticariaPost4 = rblIsUrticariaPost4.SelectedValue == "1";
            else entityItem.IsUrticariaPost4 = null;
            if (!string.IsNullOrEmpty(rblIsUrticariaPost5.SelectedValue))
                entityItem.IsUrticariaPost5 = rblIsUrticariaPost5.SelectedValue == "1";
            else entityItem.IsUrticariaPost5 = null;
            if (!string.IsNullOrEmpty(rblIsUrticariaPost6.SelectedValue))
                entityItem.IsUrticariaPost6 = rblIsUrticariaPost6.SelectedValue == "1";
            else entityItem.IsUrticariaPost6 = null;
            if (!string.IsNullOrEmpty(rblIsUrticariaPost7.SelectedValue))
                entityItem.IsUrticariaPost7 = rblIsUrticariaPost7.SelectedValue == "1";
            else entityItem.IsUrticariaPost7 = null;
            if (!string.IsNullOrEmpty(rblIsUrticariaPost8.SelectedValue))
                entityItem.IsUrticariaPost8 = rblIsUrticariaPost8.SelectedValue == "1";
            else entityItem.IsUrticariaPost8 = null;

            if (!string.IsNullOrEmpty(rblIsRashPre.SelectedValue))
                entityItem.IsRashPre = rblIsRashPre.SelectedValue == "1";
            else entityItem.IsRashPre = null;
            if (!string.IsNullOrEmpty(rblIsRash10.SelectedValue))
                entityItem.IsRash10 = rblIsRash10.SelectedValue == "1";
            else entityItem.IsRash10 = null;
            if (!string.IsNullOrEmpty(rblIsRash30.SelectedValue))
                entityItem.IsRash30 = rblIsRash30.SelectedValue == "1";
            else entityItem.IsRash30 = null;
            if (!string.IsNullOrEmpty(rblIsRash60.SelectedValue))
                entityItem.IsRash60 = rblIsRash60.SelectedValue == "1";
            else entityItem.IsRash60 = null;
            if (!string.IsNullOrEmpty(rblIsRash120.SelectedValue))
                entityItem.IsRash120 = rblIsRash120.SelectedValue == "1";
            else entityItem.IsRash120 = null;
            if (!string.IsNullOrEmpty(rblIsRash180.SelectedValue))
                entityItem.IsRash180 = rblIsRash180.SelectedValue == "1";
            else entityItem.IsRash180 = null;
            if (!string.IsNullOrEmpty(rblIsRash240.SelectedValue))
                entityItem.IsRash240 = rblIsRash240.SelectedValue == "1";
            else entityItem.IsRash240 = null;
            if (!string.IsNullOrEmpty(rblIsRashPost.SelectedValue))
                entityItem.IsRashPost = rblIsRashPost.SelectedValue == "1";
            else entityItem.IsRashPost = null;
            if (!string.IsNullOrEmpty(rblIsRashPost2.SelectedValue))
                entityItem.IsRashPost2 = rblIsRashPost2.SelectedValue == "1";
            else entityItem.IsRashPost2 = null;
            if (!string.IsNullOrEmpty(rblIsRashPost3.SelectedValue))
                entityItem.IsRashPost3 = rblIsRashPost3.SelectedValue == "1";
            else entityItem.IsRashPost3 = null;
            if (!string.IsNullOrEmpty(rblIsRashPost4.SelectedValue))
                entityItem.IsRashPost4 = rblIsRashPost4.SelectedValue == "1";
            else entityItem.IsRashPost4 = null;
            if (!string.IsNullOrEmpty(rblIsRashPost5.SelectedValue))
                entityItem.IsRashPost5 = rblIsRashPost5.SelectedValue == "1";
            else entityItem.IsRashPost5 = null;
            if (!string.IsNullOrEmpty(rblIsRashPost6.SelectedValue))
                entityItem.IsRashPost6 = rblIsRashPost6.SelectedValue == "1";
            else entityItem.IsRashPost6 = null;
            if (!string.IsNullOrEmpty(rblIsRashPost7.SelectedValue))
                entityItem.IsRashPost7 = rblIsRashPost7.SelectedValue == "1";
            else entityItem.IsRashPost7 = null;
            if (!string.IsNullOrEmpty(rblIsRashPost7.SelectedValue))
                entityItem.IsRashPost7 = rblIsRashPost7.SelectedValue == "1";
            else entityItem.IsRashPost7 = null;

            if (!string.IsNullOrEmpty(rblIsPruritusPre.SelectedValue))
                entityItem.IsPruritusPre = rblIsPruritusPre.SelectedValue == "1";
            else entityItem.IsPruritusPre = null;
            if (!string.IsNullOrEmpty(rblIsPruritus10.SelectedValue))
                entityItem.IsPruritus10 = rblIsPruritus10.SelectedValue == "1";
            else entityItem.IsPruritus10 = null;
            if (!string.IsNullOrEmpty(rblIsPruritus30.SelectedValue))
                entityItem.IsPruritus30 = rblIsPruritus30.SelectedValue == "1";
            else entityItem.IsPruritus30 = null;
            if (!string.IsNullOrEmpty(rblIsPruritus60.SelectedValue))
                entityItem.IsPruritus60 = rblIsPruritus60.SelectedValue == "1";
            else entityItem.IsPruritus60 = null;
            if (!string.IsNullOrEmpty(rblIsPruritus120.SelectedValue))
                entityItem.IsPruritus120 = rblIsPruritus120.SelectedValue == "1";
            else entityItem.IsPruritus120 = null;
            if (!string.IsNullOrEmpty(rblIsPruritus180.SelectedValue))
                entityItem.IsPruritus180 = rblIsPruritus180.SelectedValue == "1";
            else entityItem.IsPruritus180 = null;
            if (!string.IsNullOrEmpty(rblIsPruritus240.SelectedValue))
                entityItem.IsPruritus240 = rblIsPruritus240.SelectedValue == "1";
            else entityItem.IsPruritus240 = null;
            if (!string.IsNullOrEmpty(rblIsPruritusPost.SelectedValue))
                entityItem.IsPruritusPost = rblIsPruritusPost.SelectedValue == "1";
            else entityItem.IsPruritusPost = null;
            if (!string.IsNullOrEmpty(rblIsPruritusPost2.SelectedValue))
                entityItem.IsPruritusPost2 = rblIsPruritusPost2.SelectedValue == "1";
            else entityItem.IsPruritusPost2 = null;
            if (!string.IsNullOrEmpty(rblIsPruritusPost3.SelectedValue))
                entityItem.IsPruritusPost3 = rblIsPruritusPost3.SelectedValue == "1";
            else entityItem.IsPruritusPost3 = null;
            if (!string.IsNullOrEmpty(rblIsPruritusPost4.SelectedValue))
                entityItem.IsPruritusPost4 = rblIsPruritusPost4.SelectedValue == "1";
            else entityItem.IsPruritusPost4 = null;
            if (!string.IsNullOrEmpty(rblIsPruritusPost5.SelectedValue))
                entityItem.IsPruritusPost5 = rblIsPruritusPost5.SelectedValue == "1";
            else entityItem.IsPruritusPost5 = null;
            if (!string.IsNullOrEmpty(rblIsPruritusPost6.SelectedValue))
                entityItem.IsPruritusPost6 = rblIsPruritusPost6.SelectedValue == "1";
            else entityItem.IsPruritusPost6 = null;
            if (!string.IsNullOrEmpty(rblIsPruritusPost7.SelectedValue))
                entityItem.IsPruritusPost7 = rblIsPruritusPost7.SelectedValue == "1";
            else entityItem.IsPruritusPost7 = null;
            if (!string.IsNullOrEmpty(rblIsPruritusPost8.SelectedValue))
                entityItem.IsPruritusPost8 = rblIsPruritusPost8.SelectedValue == "1";
            else entityItem.IsPruritusPost8 = null;

            if (!string.IsNullOrEmpty(rblIsAnxiousPre.SelectedValue))
                entityItem.IsAnxiousPre = rblIsAnxiousPre.SelectedValue == "1";
            else entityItem.IsAnxiousPre = null;
            if (!string.IsNullOrEmpty(rblIsAnxious10.SelectedValue))
                entityItem.IsAnxious10 = rblIsAnxious10.SelectedValue == "1";
            else entityItem.IsAnxious10 = null;
            if (!string.IsNullOrEmpty(rblIsAnxious30.SelectedValue))
                entityItem.IsAnxious30 = rblIsAnxious30.SelectedValue == "1";
            else entityItem.IsAnxious30 = null;
            if (!string.IsNullOrEmpty(rblIsAnxious60.SelectedValue))
                entityItem.IsAnxious60 = rblIsAnxious60.SelectedValue == "1";
            else entityItem.IsAnxious60 = null;
            if (!string.IsNullOrEmpty(rblIsAnxious120.SelectedValue))
                entityItem.IsAnxious120 = rblIsAnxious120.SelectedValue == "1";
            else entityItem.IsAnxious120 = null;
            if (!string.IsNullOrEmpty(rblIsAnxious180.SelectedValue))
                entityItem.IsAnxious180 = rblIsAnxious180.SelectedValue == "1";
            else entityItem.IsAnxious180 = null;
            if (!string.IsNullOrEmpty(rblIsAnxious240.SelectedValue))
                entityItem.IsAnxious240 = rblIsAnxious240.SelectedValue == "1";
            else entityItem.IsAnxious240 = null;
            if (!string.IsNullOrEmpty(rblIsAnxiousPost.SelectedValue))
                entityItem.IsAnxiousPost = rblIsAnxiousPost.SelectedValue == "1";
            else entityItem.IsAnxiousPost = null;
            if (!string.IsNullOrEmpty(rblIsAnxiousPost2.SelectedValue))
                entityItem.IsAnxiousPost2 = rblIsAnxiousPost2.SelectedValue == "1";
            else entityItem.IsAnxiousPost2 = null;
            if (!string.IsNullOrEmpty(rblIsAnxiousPost3.SelectedValue))
                entityItem.IsAnxiousPost3 = rblIsAnxiousPost3.SelectedValue == "1";
            else entityItem.IsAnxiousPost3 = null;
            if (!string.IsNullOrEmpty(rblIsAnxiousPost4.SelectedValue))
                entityItem.IsAnxiousPost4 = rblIsAnxiousPost4.SelectedValue == "1";
            else entityItem.IsAnxiousPost4 = null;
            if (!string.IsNullOrEmpty(rblIsAnxiousPost5.SelectedValue))
                entityItem.IsAnxiousPost5 = rblIsAnxiousPost5.SelectedValue == "1";
            else entityItem.IsAnxiousPost5 = null;
            if (!string.IsNullOrEmpty(rblIsAnxiousPost6.SelectedValue))
                entityItem.IsAnxiousPost6 = rblIsAnxiousPost6.SelectedValue == "1";
            else entityItem.IsAnxiousPost6 = null;
            if (!string.IsNullOrEmpty(rblIsAnxiousPost7.SelectedValue))
                entityItem.IsAnxiousPost7 = rblIsAnxiousPost7.SelectedValue == "1";
            else entityItem.IsAnxiousPost7 = null;
            if (!string.IsNullOrEmpty(rblIsAnxiousPost8.SelectedValue))
                entityItem.IsAnxiousPost8 = rblIsAnxiousPost8.SelectedValue == "1";
            else entityItem.IsAnxiousPost8 = null;

            if (!string.IsNullOrEmpty(rblIsWeakPre.SelectedValue))
                entityItem.IsWeakPre = rblIsWeakPre.SelectedValue == "1";
            else entityItem.IsWeakPre = null;
            if (!string.IsNullOrEmpty(rblIsWeak10.SelectedValue))
                entityItem.IsWeak10 = rblIsWeak10.SelectedValue == "1";
            else entityItem.IsWeak10 = null;
            if (!string.IsNullOrEmpty(rblIsWeak30.SelectedValue))
                entityItem.IsWeak30 = rblIsWeak30.SelectedValue == "1";
            else entityItem.IsWeak30 = null;
            if (!string.IsNullOrEmpty(rblIsWeak60.SelectedValue))
                entityItem.IsWeak60 = rblIsWeak60.SelectedValue == "1";
            else entityItem.IsWeak60 = null;
            if (!string.IsNullOrEmpty(rblIsWeak120.SelectedValue))
                entityItem.IsWeak120 = rblIsWeak120.SelectedValue == "1";
            else entityItem.IsWeak120 = null;
            if (!string.IsNullOrEmpty(rblIsWeak180.SelectedValue))
                entityItem.IsWeak180 = rblIsWeak180.SelectedValue == "1";
            else entityItem.IsWeak180 = null;
            if (!string.IsNullOrEmpty(rblIsWeak240.SelectedValue))
                entityItem.IsWeak240 = rblIsWeak240.SelectedValue == "1";
            else entityItem.IsWeak240 = null;
            if (!string.IsNullOrEmpty(rblIsWeakPost.SelectedValue))
                entityItem.IsWeakPost = rblIsWeakPost.SelectedValue == "1";
            else entityItem.IsWeakPost = null;
            if (!string.IsNullOrEmpty(rblIsWeakPost2.SelectedValue))
                entityItem.IsWeakPost2 = rblIsWeakPost2.SelectedValue == "1";
            else entityItem.IsWeakPost2 = null;
            if (!string.IsNullOrEmpty(rblIsWeakPost3.SelectedValue))
                entityItem.IsWeakPost3 = rblIsWeakPost3.SelectedValue == "1";
            else entityItem.IsWeakPost3 = null;
            if (!string.IsNullOrEmpty(rblIsWeakPost4.SelectedValue))
                entityItem.IsWeakPost4 = rblIsWeakPost4.SelectedValue == "1";
            else entityItem.IsWeakPost4 = null;
            if (!string.IsNullOrEmpty(rblIsWeakPost5.SelectedValue))
                entityItem.IsWeakPost5 = rblIsWeakPost5.SelectedValue == "1";
            else entityItem.IsWeakPost5 = null;
            if (!string.IsNullOrEmpty(rblIsWeakPost6.SelectedValue))
                entityItem.IsWeakPost6 = rblIsWeakPost6.SelectedValue == "1";
            else entityItem.IsWeakPost6 = null;
            if (!string.IsNullOrEmpty(rblIsWeakPost7.SelectedValue))
                entityItem.IsWeakPost7 = rblIsWeakPost7.SelectedValue == "1";
            else entityItem.IsWeakPost7 = null;
            if (!string.IsNullOrEmpty(rblIsWeakPost7.SelectedValue))
                entityItem.IsWeakPost3 = rblIsWeakPost7.SelectedValue == "1";
            else entityItem.IsWeakPost7 = null;

            if (!string.IsNullOrEmpty(rblIsPalpitationsPre.SelectedValue))
                entityItem.IsPalpitationsPre = rblIsPalpitationsPre.SelectedValue == "1";
            else entityItem.IsPalpitationsPre = null;
            if (!string.IsNullOrEmpty(rblIsPalpitations10.SelectedValue))
                entityItem.IsPalpitations10 = rblIsPalpitations10.SelectedValue == "1";
            else entityItem.IsPalpitations10 = null;
            if (!string.IsNullOrEmpty(rblIsPalpitations30.SelectedValue))
                entityItem.IsPalpitations30 = rblIsPalpitations30.SelectedValue == "1";
            else entityItem.IsPalpitations30 = null;
            if (!string.IsNullOrEmpty(rblIsPalpitations60.SelectedValue))
                entityItem.IsPalpitations60 = rblIsPalpitations60.SelectedValue == "1";
            else entityItem.IsPalpitations60 = null;
            if (!string.IsNullOrEmpty(rblIsPalpitations120.SelectedValue))
                entityItem.IsPalpitations120 = rblIsPalpitations120.SelectedValue == "1";
            else entityItem.IsPalpitations120 = null;
            if (!string.IsNullOrEmpty(rblIsPalpitations180.SelectedValue))
                entityItem.IsPalpitations180 = rblIsPalpitations180.SelectedValue == "1";
            else entityItem.IsPalpitations180 = null;
            if (!string.IsNullOrEmpty(rblIsPalpitations240.SelectedValue))
                entityItem.IsPalpitations240 = rblIsPalpitations240.SelectedValue == "1";
            else entityItem.IsPalpitations240 = null;
            if (!string.IsNullOrEmpty(rblIsPalpitationsPost.SelectedValue))
                entityItem.IsPalpitationsPost = rblIsPalpitationsPost.SelectedValue == "1";
            else entityItem.IsPalpitationsPost = null;
            if (!string.IsNullOrEmpty(rblIsPalpitationsPost2.SelectedValue))
                entityItem.IsPalpitationsPost2 = rblIsPalpitationsPost2.SelectedValue == "1";
            else entityItem.IsPalpitationsPost2 = null;
            if (!string.IsNullOrEmpty(rblIsPalpitationsPost3.SelectedValue))
                entityItem.IsPalpitationsPost3 = rblIsPalpitationsPost3.SelectedValue == "1";
            else entityItem.IsPalpitationsPost3 = null;
            if (!string.IsNullOrEmpty(rblIsPalpitationsPost4.SelectedValue))
                entityItem.IsPalpitationsPost4 = rblIsPalpitationsPost4.SelectedValue == "1";
            else entityItem.IsPalpitationsPost4 = null;
            if (!string.IsNullOrEmpty(rblIsPalpitationsPost5.SelectedValue))
                entityItem.IsPalpitationsPost5 = rblIsPalpitationsPost5.SelectedValue == "1";
            else entityItem.IsPalpitationsPost5 = null;
            if (!string.IsNullOrEmpty(rblIsPalpitationsPost6.SelectedValue))
                entityItem.IsPalpitationsPost6 = rblIsPalpitationsPost6.SelectedValue == "1";
            else entityItem.IsPalpitationsPost6 = null;
            if (!string.IsNullOrEmpty(rblIsPalpitationsPost7.SelectedValue))
                entityItem.IsPalpitationsPost7 = rblIsPalpitationsPost7.SelectedValue == "1";
            else entityItem.IsPalpitationsPost7 = null;
            if (!string.IsNullOrEmpty(rblIsPalpitationsPost8.SelectedValue))
                entityItem.IsPalpitationsPost8 = rblIsPalpitationsPost8.SelectedValue == "1";
            else entityItem.IsPalpitationsPost8 = null;


            if (!string.IsNullOrEmpty(rblIsMildDyspneaPre.SelectedValue))
                entityItem.IsMildDyspneaPre = rblIsMildDyspneaPre.SelectedValue == "1";
            else entityItem.IsMildDyspneaPre = null;
            if (!string.IsNullOrEmpty(rblIsMildDyspnea10.SelectedValue))
                entityItem.IsMildDyspnea10 = rblIsMildDyspnea10.SelectedValue == "1";
            else entityItem.IsMildDyspnea10 = null;
            if (!string.IsNullOrEmpty(rblIsMildDyspnea30.SelectedValue))
                entityItem.IsMildDyspnea30 = rblIsMildDyspnea30.SelectedValue == "1";
            else entityItem.IsMildDyspnea30 = null;
            if (!string.IsNullOrEmpty(rblIsMildDyspnea60.SelectedValue))
                entityItem.IsMildDyspnea60 = rblIsMildDyspnea60.SelectedValue == "1";
            else entityItem.IsMildDyspnea60 = null;
            if (!string.IsNullOrEmpty(rblIsMildDyspnea120.SelectedValue))
                entityItem.IsMildDyspnea120 = rblIsMildDyspnea120.SelectedValue == "1";
            else entityItem.IsMildDyspnea120 = null;
            if (!string.IsNullOrEmpty(rblIsMildDyspnea180.SelectedValue))
                entityItem.IsMildDyspnea180 = rblIsMildDyspnea180.SelectedValue == "1";
            else entityItem.IsMildDyspnea180 = null;
            if (!string.IsNullOrEmpty(rblIsMildDyspnea240.SelectedValue))
                entityItem.IsMildDyspnea240 = rblIsMildDyspnea240.SelectedValue == "1";
            else entityItem.IsMildDyspnea240 = null;
            if (!string.IsNullOrEmpty(rblIsMildDyspneaPost.SelectedValue))
                entityItem.IsMildDyspneaPost = rblIsMildDyspneaPost.SelectedValue == "1";
            else entityItem.IsMildDyspneaPost = null;
            if (!string.IsNullOrEmpty(rblIsMildDyspneaPost2.SelectedValue))
                entityItem.IsMildDyspneaPost2 = rblIsMildDyspneaPost2.SelectedValue == "1";
            else entityItem.IsMildDyspneaPost2 = null;
            if (!string.IsNullOrEmpty(rblIsMildDyspneaPost3.SelectedValue))
                entityItem.IsMildDyspneaPost3 = rblIsMildDyspneaPost3.SelectedValue == "1";
            else entityItem.IsMildDyspneaPost3 = null;
            if (!string.IsNullOrEmpty(rblIsMildDyspneaPost4.SelectedValue))
                entityItem.IsMildDyspneaPost4 = rblIsMildDyspneaPost4.SelectedValue == "1";
            else entityItem.IsMildDyspneaPost4 = null;
            if (!string.IsNullOrEmpty(rblIsMildDyspneaPost5.SelectedValue))
                entityItem.IsMildDyspneaPost5 = rblIsMildDyspneaPost5.SelectedValue == "1";
            else entityItem.IsMildDyspneaPost5 = null;
            if (!string.IsNullOrEmpty(rblIsMildDyspneaPost6.SelectedValue))
                entityItem.IsMildDyspneaPost6 = rblIsMildDyspneaPost6.SelectedValue == "1";
            else entityItem.IsMildDyspneaPost6 = null;
            if (!string.IsNullOrEmpty(rblIsMildDyspneaPost7.SelectedValue))
                entityItem.IsMildDyspneaPost7 = rblIsMildDyspneaPost7.SelectedValue == "1";
            else entityItem.IsMildDyspneaPost7 = null;
            if (!string.IsNullOrEmpty(rblIsMildDyspneaPost8.SelectedValue))
                entityItem.IsMildDyspneaPost8 = rblIsMildDyspneaPost8.SelectedValue == "1";
            else entityItem.IsMildDyspneaPost8 = null;

            if (!string.IsNullOrEmpty(rblIsHeadachePre.SelectedValue))
                entityItem.IsHeadachePre = rblIsHeadachePre.SelectedValue == "1";
            else entityItem.IsHeadachePre = null;
            if (!string.IsNullOrEmpty(rblIsHeadache10.SelectedValue))
                entityItem.IsHeadache10 = rblIsHeadache10.SelectedValue == "1";
            else entityItem.IsHeadache10 = null;
            if (!string.IsNullOrEmpty(rblIsHeadache30.SelectedValue))
                entityItem.IsHeadache30 = rblIsHeadache30.SelectedValue == "1";
            else entityItem.IsHeadache30 = null;
            if (!string.IsNullOrEmpty(rblIsHeadache60.SelectedValue))
                entityItem.IsHeadache60 = rblIsHeadache60.SelectedValue == "1";
            else entityItem.IsHeadache60 = null;
            if (!string.IsNullOrEmpty(rblIsHeadache120.SelectedValue))
                entityItem.IsHeadache120 = rblIsHeadache120.SelectedValue == "1";
            else entityItem.IsHeadache120 = null;
            if (!string.IsNullOrEmpty(rblIsHeadache180.SelectedValue))
                entityItem.IsHeadache180 = rblIsHeadache180.SelectedValue == "1";
            else entityItem.IsHeadache180 = null;
            if (!string.IsNullOrEmpty(rblIsHeadache240.SelectedValue))
                entityItem.IsHeadache240 = rblIsHeadache240.SelectedValue == "1";
            else entityItem.IsHeadache240 = null;
            if (!string.IsNullOrEmpty(rblIsHeadachePost.SelectedValue))
                entityItem.IsHeadachePost = rblIsHeadachePost.SelectedValue == "1";
            else entityItem.IsHeadachePost = null;
            if (!string.IsNullOrEmpty(rblIsHeadachePost2.SelectedValue))
                entityItem.IsHeadachePost2 = rblIsHeadachePost2.SelectedValue == "1";
            else entityItem.IsHeadachePost2 = null;
            if (!string.IsNullOrEmpty(rblIsHeadachePost3.SelectedValue))
                entityItem.IsHeadachePost3 = rblIsHeadachePost3.SelectedValue == "1";
            else entityItem.IsHeadachePost3 = null;
            if (!string.IsNullOrEmpty(rblIsHeadachePost4.SelectedValue))
                entityItem.IsHeadachePost4 = rblIsHeadachePost4.SelectedValue == "1";
            else entityItem.IsHeadachePost4 = null;
            if (!string.IsNullOrEmpty(rblIsHeadachePost5.SelectedValue))
                entityItem.IsHeadachePost5 = rblIsHeadachePost5.SelectedValue == "1";
            else entityItem.IsHeadachePost5 = null;
            if (!string.IsNullOrEmpty(rblIsHeadachePost6.SelectedValue))
                entityItem.IsHeadachePost6 = rblIsHeadachePost6.SelectedValue == "1";
            else entityItem.IsHeadachePost6 = null;
            if (!string.IsNullOrEmpty(rblIsHeadachePost7.SelectedValue))
                entityItem.IsHeadachePost7 = rblIsHeadachePost7.SelectedValue == "1";
            else entityItem.IsHeadachePost7 = null;
            if (!string.IsNullOrEmpty(rblIsHeadachePost8.SelectedValue))
                entityItem.IsHeadachePost8 = rblIsHeadachePost8.SelectedValue == "1";
            else entityItem.IsHeadachePost8 = null;

            if (!string.IsNullOrEmpty(rblIsRednessOnTheSkinPre.SelectedValue))
                entityItem.IsRednessOnTheSkinPre = rblIsRednessOnTheSkinPre.SelectedValue == "1";
            else entityItem.IsRednessOnTheSkinPre = null;
            if (!string.IsNullOrEmpty(rblIsRednessOnTheSkin10.SelectedValue))
                entityItem.IsRednessOnTheSkin10 = rblIsRednessOnTheSkin10.SelectedValue == "1";
            else entityItem.IsRednessOnTheSkin10 = null;
            if (!string.IsNullOrEmpty(rblIsRednessOnTheSkin30.SelectedValue))
                entityItem.IsRednessOnTheSkin30 = rblIsRednessOnTheSkin30.SelectedValue == "1";
            else entityItem.IsRednessOnTheSkin30 = null;
            if (!string.IsNullOrEmpty(rblIsRednessOnTheSkin60.SelectedValue))
                entityItem.IsRednessOnTheSkin60 = rblIsRednessOnTheSkin60.SelectedValue == "1";
            else entityItem.IsRednessOnTheSkin60 = null;
            if (!string.IsNullOrEmpty(rblIsRednessOnTheSkin120.SelectedValue))
                entityItem.IsRednessOnTheSkin120 = rblIsRednessOnTheSkin120.SelectedValue == "1";
            else entityItem.IsRednessOnTheSkin120 = null;
            if (!string.IsNullOrEmpty(rblIsRednessOnTheSkin180.SelectedValue))
                entityItem.IsRednessOnTheSkin180 = rblIsRednessOnTheSkin180.SelectedValue == "1";
            else entityItem.IsRednessOnTheSkin180 = null;
            if (!string.IsNullOrEmpty(rblIsRednessOnTheSkin240.SelectedValue))
                entityItem.IsRednessOnTheSkin240 = rblIsRednessOnTheSkin240.SelectedValue == "1";
            else entityItem.IsRednessOnTheSkin240 = null;
            if (!string.IsNullOrEmpty(rblIsRednessOnTheSkinPost.SelectedValue))
                entityItem.IsRednessOnTheSkinPost = rblIsRednessOnTheSkinPost.SelectedValue == "1";
            else entityItem.IsRednessOnTheSkinPost = null;
            if (!string.IsNullOrEmpty(rblIsRednessOnTheSkinPost2.SelectedValue))
                entityItem.IsRednessOnTheSkinPost2 = rblIsRednessOnTheSkinPost2.SelectedValue == "1";
            else entityItem.IsRednessOnTheSkinPost2 = null;
            if (!string.IsNullOrEmpty(rblIsRednessOnTheSkinPost3.SelectedValue))
                entityItem.IsRednessOnTheSkinPost3 = rblIsRednessOnTheSkinPost3.SelectedValue == "1";
            else entityItem.IsRednessOnTheSkinPost3 = null;
            if (!string.IsNullOrEmpty(rblIsRednessOnTheSkinPost4.SelectedValue))
                entityItem.IsRednessOnTheSkinPost4 = rblIsRednessOnTheSkinPost4.SelectedValue == "1";
            else entityItem.IsRednessOnTheSkinPost4 = null;
            if (!string.IsNullOrEmpty(rblIsRednessOnTheSkinPost5.SelectedValue))
                entityItem.IsRednessOnTheSkinPost5 = rblIsRednessOnTheSkinPost5.SelectedValue == "1";
            else entityItem.IsRednessOnTheSkinPost5 = null;
            if (!string.IsNullOrEmpty(rblIsRednessOnTheSkinPost6.SelectedValue))
                entityItem.IsRednessOnTheSkinPost6 = rblIsRednessOnTheSkinPost6.SelectedValue == "1";
            else entityItem.IsRednessOnTheSkinPost6 = null;
            if (!string.IsNullOrEmpty(rblIsRednessOnTheSkinPost7.SelectedValue))
                entityItem.IsRednessOnTheSkinPost7 = rblIsRednessOnTheSkinPost7.SelectedValue == "1";
            else entityItem.IsRednessOnTheSkinPost7 = null;
            if (!string.IsNullOrEmpty(rblIsRednessOnTheSkinPost8.SelectedValue))
                entityItem.IsRednessOnTheSkinPost8 = rblIsRednessOnTheSkinPost8.SelectedValue == "1";
            else entityItem.IsRednessOnTheSkinPost8 = null;

            if (!string.IsNullOrEmpty(rblIsTachycardiaPre.SelectedValue))
                entityItem.IsTachycardiaPre = rblIsTachycardiaPre.SelectedValue == "1";
            else entityItem.IsTachycardiaPre = null;
            if (!string.IsNullOrEmpty(rblIsTachycardia10.SelectedValue))
                entityItem.IsTachycardia10 = rblIsTachycardia10.SelectedValue == "1";
            else entityItem.IsTachycardia10 = null;
            if (!string.IsNullOrEmpty(rblIsTachycardia30.SelectedValue))
                entityItem.IsTachycardia30 = rblIsTachycardia30.SelectedValue == "1";
            else entityItem.IsTachycardia30 = null;
            if (!string.IsNullOrEmpty(rblIsTachycardia60.SelectedValue))
                entityItem.IsTachycardia60 = rblIsTachycardia60.SelectedValue == "1";
            else entityItem.IsTachycardia60 = null;
            if (!string.IsNullOrEmpty(rblIsTachycardia120.SelectedValue))
                entityItem.IsTachycardia120 = rblIsTachycardia120.SelectedValue == "1";
            else entityItem.IsTachycardia120 = null;
            if (!string.IsNullOrEmpty(rblIsTachycardia180.SelectedValue))
                entityItem.IsTachycardia180 = rblIsTachycardia180.SelectedValue == "1";
            else entityItem.IsTachycardia180 = null;
            if (!string.IsNullOrEmpty(rblIsTachycardia240.SelectedValue))
                entityItem.IsTachycardia240 = rblIsTachycardia240.SelectedValue == "1";
            else entityItem.IsTachycardia240 = null;
            if (!string.IsNullOrEmpty(rblIsTachycardiaPost.SelectedValue))
                entityItem.IsTachycardiaPost = rblIsTachycardiaPost.SelectedValue == "1";
            else entityItem.IsTachycardiaPost = null;
            if (!string.IsNullOrEmpty(rblIsTachycardiaPost2.SelectedValue))
                entityItem.IsTachycardiaPost2 = rblIsTachycardiaPost2.SelectedValue == "1";
            else entityItem.IsTachycardiaPost2 = null;
            if (!string.IsNullOrEmpty(rblIsTachycardiaPost3.SelectedValue))
                entityItem.IsTachycardiaPost3 = rblIsTachycardiaPost3.SelectedValue == "1";
            else entityItem.IsTachycardiaPost3 = null;
            if (!string.IsNullOrEmpty(rblIsTachycardiaPost4.SelectedValue))
                entityItem.IsTachycardiaPost4 = rblIsTachycardiaPost4.SelectedValue == "1";
            else entityItem.IsTachycardiaPost4 = null;
            if (!string.IsNullOrEmpty(rblIsTachycardiaPost5.SelectedValue))
                entityItem.IsTachycardiaPost5 = rblIsTachycardiaPost5.SelectedValue == "1";
            else entityItem.IsTachycardiaPost5 = null;
            if (!string.IsNullOrEmpty(rblIsTachycardiaPost6.SelectedValue))
                entityItem.IsTachycardiaPost6 = rblIsTachycardiaPost6.SelectedValue == "1";
            else entityItem.IsTachycardiaPost6 = null;
            if (!string.IsNullOrEmpty(rblIsTachycardiaPost7.SelectedValue))
                entityItem.IsTachycardiaPost7 = rblIsTachycardiaPost7.SelectedValue == "1";
            else entityItem.IsTachycardiaPost7 = null;
            if (!string.IsNullOrEmpty(rblIsTachycardiaPost8.SelectedValue))
                entityItem.IsTachycardiaPost8 = rblIsTachycardiaPost8.SelectedValue == "1";
            else entityItem.IsTachycardiaPost8 = null;

            if (!string.IsNullOrEmpty(rblIsMuscleStiffnessPre.SelectedValue))
                entityItem.IsMuscleStiffnessPre = rblIsMuscleStiffnessPre.SelectedValue == "1";
            else entityItem.IsMuscleStiffnessPre = null;
            if (!string.IsNullOrEmpty(rblIsMuscleStiffness10.SelectedValue))
                entityItem.IsMuscleStiffness10 = rblIsMuscleStiffness10.SelectedValue == "1";
            else entityItem.IsMuscleStiffness10 = null;
            if (!string.IsNullOrEmpty(rblIsMuscleStiffness30.SelectedValue))
                entityItem.IsMuscleStiffness30 = rblIsMuscleStiffness30.SelectedValue == "1";
            else entityItem.IsMuscleStiffness30 = null;
            if (!string.IsNullOrEmpty(rblIsMuscleStiffness60.SelectedValue))
                entityItem.IsMuscleStiffness60 = rblIsMuscleStiffness60.SelectedValue == "1";
            else entityItem.IsMuscleStiffness60 = null;
            if (!string.IsNullOrEmpty(rblIsMuscleStiffness120.SelectedValue))
                entityItem.IsMuscleStiffness120 = rblIsMuscleStiffness120.SelectedValue == "1";
            else entityItem.IsMuscleStiffness120 = null;
            if (!string.IsNullOrEmpty(rblIsMuscleStiffness180.SelectedValue))
                entityItem.IsMuscleStiffness180 = rblIsMuscleStiffness180.SelectedValue == "1";
            else entityItem.IsMuscleStiffness180 = null;
            if (!string.IsNullOrEmpty(rblIsMuscleStiffness240.SelectedValue))
                entityItem.IsMuscleStiffness240 = rblIsMuscleStiffness240.SelectedValue == "1";
            else entityItem.IsMuscleStiffness240 = null;
            if (!string.IsNullOrEmpty(rblIsMuscleStiffnessPost.SelectedValue))
                entityItem.IsMuscleStiffnessPost = rblIsMuscleStiffnessPost.SelectedValue == "1";
            else entityItem.IsMuscleStiffnessPost = null;
            if (!string.IsNullOrEmpty(rblIsMuscleStiffnessPost2.SelectedValue))
                entityItem.IsMuscleStiffnessPost2 = rblIsMuscleStiffnessPost2.SelectedValue == "1";
            else entityItem.IsMuscleStiffnessPost2 = null;
            if (!string.IsNullOrEmpty(rblIsMuscleStiffnessPost3.SelectedValue))
                entityItem.IsMuscleStiffnessPost3 = rblIsMuscleStiffnessPost3.SelectedValue == "1";
            else entityItem.IsMuscleStiffnessPost3 = null;
            if (!string.IsNullOrEmpty(rblIsMuscleStiffnessPost4.SelectedValue))
                entityItem.IsMuscleStiffnessPost4 = rblIsMuscleStiffnessPost4.SelectedValue == "1";
            else entityItem.IsMuscleStiffnessPost4 = null;
            if (!string.IsNullOrEmpty(rblIsMuscleStiffnessPost5.SelectedValue))
                entityItem.IsMuscleStiffnessPost5 = rblIsMuscleStiffnessPost5.SelectedValue == "1";
            else entityItem.IsMuscleStiffnessPost5 = null;
            if (!string.IsNullOrEmpty(rblIsMuscleStiffnessPost6.SelectedValue))
                entityItem.IsMuscleStiffnessPost6 = rblIsMuscleStiffnessPost6.SelectedValue == "1";
            else entityItem.IsMuscleStiffnessPost3 = null;
            if (!string.IsNullOrEmpty(rblIsMuscleStiffnessPost7.SelectedValue))
                entityItem.IsMuscleStiffnessPost7 = rblIsMuscleStiffnessPost7.SelectedValue == "1";
            else entityItem.IsMuscleStiffnessPost7 = null;
            if (!string.IsNullOrEmpty(rblIsMuscleStiffnessPost8.SelectedValue))
                entityItem.IsMuscleStiffnessPost8 = rblIsMuscleStiffnessPost8.SelectedValue == "1";
            else entityItem.IsMuscleStiffnessPost8 = null;

            entityItem.OtherReactionPre = txtOtherReactionPre.Text;
            entityItem.OtherReaction10 = txtOtherReaction10.Text;
            entityItem.OtherReaction30 = txtOtherReaction30.Text;
            entityItem.OtherReaction60 = txtOtherReaction60.Text;
            entityItem.OtherReaction120 = txtOtherReaction120.Text;
            entityItem.OtherReaction180 = txtOtherReaction180.Text;
            entityItem.OtherReaction240 = txtOtherReaction240.Text;
            entityItem.OtherReactionPost = txtOtherReactionPost.Text;
            entityItem.OtherReactionPost2 = txtOtherReactionPost2.Text;
            entityItem.OtherReactionPost3 = txtOtherReactionPost3.Text;
            entityItem.OtherReactionPost4 = txtOtherReactionPost4.Text;
            entityItem.OtherReactionPost5 = txtOtherReactionPost5.Text;
            entityItem.OtherReactionPost6 = txtOtherReactionPost6.Text;
            entityItem.OtherReactionPost7 = txtOtherReactionPost7.Text;
            entityItem.OtherReactionPost8 = txtOtherReactionPost8.Text;
            #endregion

            #region Action
            entityItem.HemoglobinPre = Convert.ToDecimal(txtHemoglobinPre.Value);
            entityItem.Hemoglobin10 = Convert.ToDecimal(txtHemoglobin10.Value);
            entityItem.Hemoglobin30 = Convert.ToDecimal(txtHemoglobin30.Value);
            entityItem.Hemoglobin60 = Convert.ToDecimal(txtHemoglobin60.Value);
            entityItem.Hemoglobin120 = Convert.ToDecimal(txtHemoglobin120.Value);
            entityItem.Hemoglobin180 = Convert.ToDecimal(txtHemoglobin180.Value);
            entityItem.Hemoglobin240 = Convert.ToDecimal(txtHemoglobin240.Value);
            entityItem.HemoglobinPost = Convert.ToDecimal(txtHemoglobinPost.Value);
            entityItem.HemoglobinPost2 = Convert.ToDecimal(txtHemoglobinPost2.Value);
            entityItem.HemoglobinPost3 = Convert.ToDecimal(txtHemoglobinPost3.Value);

            entityItem.HematocritPre = Convert.ToDecimal(txtHematocritPre.Value);
            entityItem.Hematocrit10 = Convert.ToDecimal(txtHematocrit10.Value);
            entityItem.Hematocrit30 = Convert.ToDecimal(txtHematocrit30.Value);
            entityItem.Hematocrit60 = Convert.ToDecimal(txtHematocrit60.Value);
            entityItem.Hematocrit120 = Convert.ToDecimal(txtHematocrit120.Value);
            entityItem.Hematocrit180 = Convert.ToDecimal(txtHematocrit180.Value);
            entityItem.Hematocrit240 = Convert.ToDecimal(txtHematocrit240.Value);
            entityItem.HematocritPost = Convert.ToDecimal(txtHematocritPost.Value);
            entityItem.HematocritPost2 = Convert.ToDecimal(txtHematocritPost2.Value);
            entityItem.HematocritPost3 = Convert.ToDecimal(txtHematocritPost3.Value);

            entityItem.PlateletPre = Convert.ToDecimal(txtPlateletPre.Value);
            entityItem.Platelet10 = Convert.ToDecimal(txtPlatelet10.Value);
            entityItem.Platelet30 = Convert.ToDecimal(txtPlatelet30.Value);
            entityItem.Platelet60 = Convert.ToDecimal(txtPlatelet60.Value);
            entityItem.Platelet120 = Convert.ToDecimal(txtPlatelet120.Value);
            entityItem.Platelet180 = Convert.ToDecimal(txtPlatelet180.Value);
            entityItem.Platelet240 = Convert.ToDecimal(txtPlatelet240.Value);
            entityItem.PlateletPost = Convert.ToDecimal(txtPlateletPost.Value);
            entityItem.PlateletPost2 = Convert.ToDecimal(txtPlateletPost2.Value);
            entityItem.PlateletPost3 = Convert.ToDecimal(txtPlateletPost3.Value);

            entityItem.ActionPre = txtActionPre.Text;
            entityItem.Action10 = txtAction10.Text;
            entityItem.Action30 = txtAction30.Text;
            entityItem.Action60 = txtAction60.Text;
            entityItem.Action120 = txtAction120.Text;
            entityItem.Action180 = txtAction180.Text;
            entityItem.Action240 = txtAction240.Text;
            entityItem.ActionPost = txtActionPost.Text;
            entityItem.ActionPost2 = txtActionPost2.Text;
            entityItem.ActionPost3 = txtActionPost3.Text;

            entityItem.TransfusionStartDateTime = txtTransfusionStartDateTime.SelectedDate;
            entityItem.TransfusionEndDateTime = txtTransfusionEndDateTime.SelectedDate;
            entityItem.TransfusedOfficerStartBy = txtTransfusedOfficerStartBy.Text;
            entityItem.TransfusedOfficerEndBy = txtTransfusedOfficerEndBy.Text;
            #endregion

            entityItem.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entityItem.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        }

        private void SaveEntity(BloodBankTransaction entity, BloodBankTransactionItem entityItem)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();

                if (!AppSession.Parameter.IsAutoTransfusionBillProceedOnBloodDistribution)
                {
                    if (entityItem.IsTransfusionBillProceed == null || entityItem.IsTransfusionBillProceed == false)
                    {
                        entityItem.IsTransfusionBillProceed = true;

                        var it = new AppStandardReferenceItem();
                        if (it.LoadByPrimaryKey(AppEnum.StandardReference.BloodGroup.ToString(),
                            entity.SRBloodGroupRequest) && !string.IsNullOrEmpty(it.ReferenceID))
                        {
                            var itemId = it.ReferenceID;

                            var i = new Item();
                            if (i.LoadByPrimaryKey(itemId))
                            {
                                Temiang.Avicenna.WebService.BillingChargeService.BillingProcess(entity.RegistrationNo, i.ItemID, string.Format("{0:000}", 1), 1, "ds", true);
                            }
                        }
                    }
                }

                entityItem.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new BloodBankTransactionQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.TransactionNo > txtTransactionNo.Text, que.IsApproved == true
                    );
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.TransactionNo < txtTransactionNo.Text, que.IsApproved == true
                    );
                que.OrderBy(que.TransactionNo.Descending);
            }

            var entity = new BloodBankTransaction();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        protected void chkIsReCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsReCheck.Checked)
                txtReCheckDateTime.SelectedDate = (new DateTime()).NowAtSqlServer();
            else
            {
                txtReCheckDateTime.Clear();
                rfvIsReCheckExpiredDate.Visible = false;
                rfvIsReCheckLeak.Visible = false;
                rfvIsReCheckHemolysis.Visible = false;
                rfvIsReCheckClotting.Visible = false;
                rfvIsReCheckBloodTypeCompatibility.Visible = false;
                rfvReCheckOfficer.Visible = false;
            }
        }
    }
}
