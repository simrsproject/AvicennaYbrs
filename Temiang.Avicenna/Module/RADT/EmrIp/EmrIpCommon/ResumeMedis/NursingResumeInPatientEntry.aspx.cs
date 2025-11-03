using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data.SqlTypes;
using System.Text;
using System.Web.Services;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject.JsonField;
using Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl;
using Temiang.Dal.Interfaces;
using DateTime = System.DateTime;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class NursingResumeInPatientEntry : BasePageDialogEntry
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;
            ProgramReferenceID = "NURES";

            // Program Fiture
            IsSingleRecordMode = false; //Save then close
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = false;

            ToolBar.EditVisible = true;
            ToolBar.AddVisible = false;
            // -------------------

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Nursing Resume of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ", Reg No: " + RegistrationNo + ")";
                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }


        }


        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var ent = new MedicalDischargeSummaryByNurse();
            if (!ent.LoadByPrimaryKey(RegistrationNo)) return;

            txtTemp.Text = ent.Temp;
            txtPulse.Text = ent.Pulse;
            txtRespiratory.Text = ent.Respiratory;
            txtBloodPress.Text = ent.BloodPress;

            SetSelectedValue(lboxDiet, ent.DietType);
            SetSelectedValue(lboxDiet2, ent.DietType);
            txtDietLiquidLimitNote.Text = ent.DietLiquidLimitNote;
            txtSpecialDietNote.Text = ent.SpecialDietNote;

            optDefecateType.SelectedValue = ent.DefecateType;
            optUrinateType.SelectedValue = ent.UrinateType;
            txtCatheterLastDate.SelectedDate = ent.CatheterLastDate;

            optUterineType.SelectedValue = ent.UterineType;
            txtUterineHeight.Value = ent.UterineHeight;

            optVulvaType.SelectedValue = ent.VulvaType;
            optPerinealWoundType.SelectedValue = ent.PerinealWoundType;

            optLocheaType.SelectedValue = ent.LocheaType;
            txtLocheaColor.Text = ent.LocheaColor;
            txtLocheaSmell.Text = ent.LocheaSmell;

            optOperationWoundType.SelectedValue = ent.OperationWoundType;
            optTransferMobilizationType.SelectedValue = ent.TransferMobilizationType;
            txtPartiallyAssisted.Text = ent.PartiallyAssisted;

            optAssistToolType.SelectedValue = ent.AssistToolType;

            //TODO: Education apakah mau dimasukan ke integrate education
            SetSelectedValue(lboxEducation, ent.Education);
            ent.EducationOthers = txtEducationOthers.Text;

            txtTreatmentDiag01.Text = ent.TreatmentDiag01;
            txtTreatmentDiag02.Text = ent.TreatmentDiag02;
            txtTreatmentDiag03.Text = ent.TreatmentDiag03;
            txtTreatmentDiag04.Text = ent.TreatmentDiag04;

            txtDischargeDiag01.Text = ent.DischargeDiag01;
            txtDischargeDiag02.Text = ent.DischargeDiag02;
            txtDischargeDiag03.Text = ent.DischargeDiag03;
            txtDischargeDiag04.Text = ent.DischargeDiag04;

            txtDrugsTaken.Text = ent.DrugsTaken;
            txtPossibleEffect.Text = ent.PossibleEffect;
            txtHospitalRefer.Text = ent.HospitalRefer;

            txtLabResultSheet.Value = ent.LabResultSheet;
            txtXRaysSheet.Value = ent.XRaysSheet;
            txtCTScanSheet.Value = ent.CTScanSheet;
            txtMriMraSheet.Value = ent.MriMraSheet;
            txtUsgResultSheet.Value = ent.UsgResultSheet;

            optCertIllnes.SelectedValue = ent.IsCertIllnes == true ? "1" : "0";
            optInsLetter.SelectedValue = ent.IsInsLetter == true ? "1" : "0";
            optDischSummaryLetter.SelectedValue = ent.IsDischSummaryLetter == true ? "1" : "0";
            optBabyBook.SelectedValue = ent.IsBabyBook == true ? "1" : "0";
            optBabyBloodType.SelectedValue = ent.IsBabyBloodType == true ? "1" : "0";
            optCertBirth.SelectedValue = ent.IsCertBirth == true ? "1" : "0";

            txtHandedBy.Text = ent.HandedBy;
            txtOtherLetter.Text = ent.OtherLetter;


            //if (!string.IsNullOrEmpty(ent.ControlPlan))
            //{
            //    var planCount = 0;
            //    var controlPlans = JsonConvert.DeserializeObject<ControlPlan>(ent.ControlPlan);
            //    if (controlPlans != null && controlPlans.Items != null)
            //        planCount = controlPlans.Items.Count;

            //    if (planCount > 0)
            //    {
            //        txtControlPlanDateTime01.SelectedDate = controlPlans.Items[0].ControlPlanDateTime;
            //        cboParamedicName01.Text = controlPlans.Items[0].ParamedicName;
            //        txtSpecialtyName01.Text = controlPlans.Items[0].SpecialtyName;
            //    }

            //    if (planCount > 1)
            //    {
            //        txtControlPlanDateTime02.SelectedDate = controlPlans.Items[1].ControlPlanDateTime;
            //        cboParamedicName02.Text = controlPlans.Items[1].ParamedicName;
            //        txtSpecialtyName02.Text = controlPlans.Items[1].SpecialtyName;
            //    }

            //    if (planCount > 2)
            //    {
            //        txtControlPlanDateTime03.SelectedDate = controlPlans.Items[2].ControlPlanDateTime;
            //        cboParamedicName03.Text = controlPlans.Items[2].ParamedicName;
            //        txtSpecialtyName03.Text = controlPlans.Items[2].SpecialtyName;
            //    }

            //    if (planCount > 3)
            //    {
            //        txtControlPlanDateTime04.SelectedDate = controlPlans.Items[3].ControlPlanDateTime;
            //        cboParamedicName04.Text = controlPlans.Items[3].ParamedicName;
            //        txtSpecialtyName04.Text = controlPlans.Items[3].SpecialtyName;
            //    }

            //    if (planCount > 4)
            //    {
            //        txtControlPlanDateTime05.SelectedDate = controlPlans.Items[4].ControlPlanDateTime;
            //        cboParamedicName05.Text = controlPlans.Items[4].ParamedicName;
            //        txtSpecialtyName05.Text = controlPlans.Items[4].SpecialtyName;
            //    }
            //}


            controlPlanCtl.Populate(ent.ControlPlan);
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }
        protected override void OnMenuNewClick()
        {
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            SaveMedicalResume(args);
        }


        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            SaveMedicalResume(args);
        }

        protected override void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
        {


        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
        }

        protected override void OnMenuEditClick()
        {
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuNewClick(ValidateArgs args)
        {

        }
        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        protected override void OnMenuRejournalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            return string.Empty;
        }
        public override string OnGetScriptToolBarSaveClicking()
        {
            return string.Empty;
        }
        public override bool OnGetStatusMenuEdit()
        {
            return true;
        }

        public override bool OnGetStatusMenuDelete()
        {
            return true;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return true;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return true;
        }

        protected override void OnInitializeAjaxManager(RadAjaxManager ajaxManager)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion


        private void SetSelectedValue(RadListBox listBox, string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return;
            foreach (RadListBoxItem item in listBox.Items)
            {
                item.Checked = value.Contains(item.Value + "|");
            }
        }

        private string GetSelectedValue(RadListBox listBox)
        {
            StringBuilder sb = new StringBuilder();
            var collection = listBox.CheckedItems;
            foreach (RadListBoxItem item in collection)
            {
                sb.Append(item.Value + "|");
            }

            return sb.ToString();
        }

        private void SaveMedicalResume(ValidateArgs args)
        {
            var ent = new MedicalDischargeSummaryByNurse();
            if (!ent.LoadByPrimaryKey(RegistrationNo))
            {
                ent.RegistrationNo = RegistrationNo;
            }

            ent.Temp = txtTemp.Text;
            ent.Pulse = txtPulse.Text;
            ent.Respiratory = txtRespiratory.Text;
            ent.BloodPress = txtBloodPress.Text;

            ent.DietType = GetSelectedValue(lboxDiet) + "|" + GetSelectedValue(lboxDiet2);
            ent.DietLiquidLimitNote = txtDietLiquidLimitNote.Text;
            ent.SpecialDietNote = txtSpecialDietNote.Text;

            ent.DefecateType = optDefecateType.SelectedValue;
            ent.UrinateType = optUrinateType.SelectedValue;
            ent.CatheterLastDate = txtCatheterLastDate.SelectedDate;

            ent.UterineType = optUterineType.SelectedValue;
            ent.UterineHeight = txtUterineHeight.Value.ToInt();

            ent.VulvaType = optVulvaType.SelectedValue;
            ent.PerinealWoundType = optPerinealWoundType.SelectedValue;

            ent.LocheaType = optLocheaType.SelectedValue;
            ent.LocheaColor = txtLocheaColor.Text;
            ent.LocheaSmell = txtLocheaSmell.Text;

            ent.OperationWoundType = optOperationWoundType.SelectedValue;
            ent.TransferMobilizationType = optTransferMobilizationType.SelectedValue;
            ent.PartiallyAssisted = txtPartiallyAssisted.Text;

            ent.AssistToolType = optAssistToolType.SelectedValue;

            ent.Education = GetSelectedValue(lboxEducation);
            ent.EducationOthers = txtEducationOthers.Text;

            ent.TreatmentDiag01 = txtTreatmentDiag01.Text;
            ent.TreatmentDiag02 = txtTreatmentDiag02.Text;
            ent.TreatmentDiag03 = txtTreatmentDiag03.Text;
            ent.TreatmentDiag04 = txtTreatmentDiag04.Text;

            ent.DischargeDiag01 = txtDischargeDiag01.Text;
            ent.DischargeDiag02 = txtDischargeDiag02.Text;
            ent.DischargeDiag03 = txtDischargeDiag03.Text;
            ent.DischargeDiag04 = txtDischargeDiag04.Text;

            ent.DrugsTaken = txtDrugsTaken.Text;
            ent.PossibleEffect = txtPossibleEffect.Text;
            ent.HospitalRefer = txtHospitalRefer.Text;

            ent.LabResultSheet = txtLabResultSheet.Value.ToInt();
            ent.XRaysSheet = txtXRaysSheet.Value.ToInt();
            ent.CTScanSheet = txtCTScanSheet.Value.ToInt();
            ent.MriMraSheet = txtMriMraSheet.Value.ToInt();
            ent.UsgResultSheet = txtUsgResultSheet.Value.ToInt();

            ent.IsCertIllnes = optCertIllnes.SelectedValue == "1";
            ent.IsInsLetter = optInsLetter.SelectedValue == "1";
            ent.IsDischSummaryLetter = optDischSummaryLetter.SelectedValue == "1";
            ent.IsBabyBook = optBabyBook.SelectedValue == "1";
            ent.IsBabyBloodType = optBabyBloodType.SelectedValue == "1";
            ent.IsCertBirth = optCertBirth.SelectedValue == "1";


            ent.HandedBy = txtHandedBy.Text;
            ent.OtherLetter = txtOtherLetter.Text;

            //var itemPlans = new List<ControlPlanItem>();

            //if (!txtControlPlanDateTime01.IsEmpty)
            //    itemPlans.Add(new ControlPlanItem
            //    {
            //        ControlPlanDateTime = txtControlPlanDateTime01.SelectedDate.Value,
            //        ParamedicName = cboParamedicName01.Text,
            //        SpecialtyName = txtSpecialtyName01.Text
            //    });

            //if (!txtControlPlanDateTime02.IsEmpty)
            //    itemPlans.Add(new ControlPlanItem
            //    {
            //        ControlPlanDateTime = txtControlPlanDateTime02.SelectedDate.Value,
            //        ParamedicName = cboParamedicName02.Text,
            //        SpecialtyName = txtSpecialtyName02.Text
            //    });

            //if (!txtControlPlanDateTime03.IsEmpty)
            //    itemPlans.Add(new ControlPlanItem
            //    {
            //        ControlPlanDateTime = txtControlPlanDateTime03.SelectedDate.Value,
            //        ParamedicName = cboParamedicName03.Text,
            //        SpecialtyName = txtSpecialtyName03.Text
            //    });

            //if (!txtControlPlanDateTime04.IsEmpty)
            //    itemPlans.Add(new ControlPlanItem
            //    {
            //        ControlPlanDateTime = txtControlPlanDateTime04.SelectedDate.Value,
            //        ParamedicName = cboParamedicName04.Text,
            //        SpecialtyName = txtSpecialtyName04.Text
            //    });

            //if (!txtControlPlanDateTime05.IsEmpty)
            //    itemPlans.Add(new ControlPlanItem
            //    {
            //        ControlPlanDateTime = txtControlPlanDateTime05.SelectedDate.Value,
            //        ParamedicName = cboParamedicName05.Text,
            //        SpecialtyName = txtSpecialtyName05.Text
            //    });

            //var oplan = new ControlPlan();
            //oplan.Items = itemPlans;

            //ent.ControlPlan = JsonConvert.SerializeObject(oplan);



            //SetPlanControl(args, ent);
            var appointmentNos = SetPlanControl(args, ent);
            if (args.IsCancel == false)
            {
                using (var trans = new esTransactionScope())
                {
                    ent.Save();

                    //db:20240511 - cancel all appointment yg nomornya gak dipake di control plan
                    if (appointmentNos.Length > 0)
                    {
                        var appts = new BusinessObject.AppointmentCollection();
                        appts.Query.Where(appts.Query.AppointmentNo.NotIn(appointmentNos.Split(';')),
                            appts.Query.PatientID == PatientID,
                            appts.Query.AppointmentDate > DateTime.Today,
                            appts.Query.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusOpen,
                            appts.Query.FromRegistrationNoMds == RegistrationNo,
                            appts.Query.SRAppoinmentType == AppSession.Parameter.AppointmentTypeControlPlan);
                        appts.LoadAll();
                        foreach (var a in appts)
                        {
                            a.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusCancel;
                            a.Notes = "Cancel by system";
                        }
                        appts.Save();
                    }

                    trans.Complete();
                }
            }
        }

        private string SetPlanControl(ValidateArgs args, MedicalDischargeSummaryByNurse dsByNurse)
        {
            var oplan = controlPlanCtl.GetControlPlan();

            // Save in appointment
            var pat = new Patient();
            pat.LoadByPrimaryKey(PatientID);

            var appointmentNos = string.Empty;

            foreach (Temiang.Avicenna.BusinessObject.JsonField.ControlPlanItem planItem in oplan.Items)
            {
                if (planItem.ControlPlanDateTime > DateTime.Today
                    && !string.IsNullOrEmpty(planItem.ServiceUnitID)
                    && !string.IsNullOrEmpty(planItem.ParamedicID))
                {
                    var appointmentNo = planItem.AppointmentNo;
                    if (!string.IsNullOrEmpty(appointmentNo))
                    {
                        //db:20241105 - query data appointment berdasarkan no appointment
                        var appt = new BusinessObject.Appointment();
                        var apptq = new AppointmentQuery();
                        //apptq.Where(apptq.AppointmentNo == appointmentNo, appt.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel); - apip:20241114
                        apptq.Where(apptq.AppointmentNo == appointmentNo, apptq.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel);
                        if (appt.Load(apptq))
                        {
                            //db:20241105 - cek data ServiceUnitID, ParamedicID & ControlPlanDateTime. kalo tidak sama dg di data di control plan, cancel no appointment, u/ kemudian create no appoinment baru
                            if (appt.ServiceUnitID != planItem.ServiceUnitID || appt.ParamedicID != planItem.ParamedicID || appt.PatientID != pat.PatientID || appt.AppointmentDate != planItem.ControlPlanDateTime.Date)
                            {
                                appointmentNo = string.Empty;
                            }
                            else
                            {
                                if (appointmentNos == string.Empty)
                                    appointmentNos = appointmentNo;
                                else
                                    appointmentNos = ";" + appointmentNo;
                            }
                        }
                        else
                            appointmentNo = string.Empty;
                    }
                    else
                    {
                        //db:20241105 - cek apakah sudah ada data appointment yg ter-create sesuai ServiceUnitID, ParamedicID & ControlPlanDateTime (dari action save & edit)
                        var appt = new BusinessObject.Appointment();
                        var apptq = new AppointmentQuery();
                        apptq.Where(apptq.ServiceUnitID == planItem.ServiceUnitID, apptq.ParamedicID == planItem.ParamedicID, apptq.PatientID == pat.PatientID,
                            apptq.AppointmentDate == planItem.ControlPlanDateTime.Date, apptq.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel,
                            apptq.SRAppoinmentType == AppSession.Parameter.AppointmentTypeControlPlan);
                        apptq.Select(apptq.AppointmentNo, apptq.AppointmentQue, apptq.AppointmentTime);
                        if (appt.Load(apptq))
                        {
                            appointmentNo = appt.AppointmentNo;

                            planItem.AppointmentTime = appt.AppointmentTime;
                            planItem.AppointmentQue = appt.AppointmentQue;
                            planItem.AppointmentNo = appointmentNo;

                            if (appointmentNos == string.Empty)
                                appointmentNos = appt.AppointmentNo;
                            else
                                appointmentNos = ";" + appt.AppointmentNo;
                        }
                        else
                        {
                            appointmentNo = string.Empty;
                        }
                    }

                    if (string.IsNullOrEmpty(appointmentNo))
                    {
                        var qSchedule = new ParamedicScheduleDate();
                        if (qSchedule.LoadByPrimaryKey(planItem.ServiceUnitID, planItem.ParamedicID, planItem.ControlPlanDateTime.Year.ToString(), planItem.ControlPlanDateTime.Date))
                        {
                            try
                            {
                                // Parameter fromRegistrationNo diisi null supaya tidak terjadi merge billing di reg dari appt nya (Handono 231110 req by Imel)
                                var slot = Temiang.Avicenna.WebService.V1_1.AppointmentWS.AppointmentPostRanapSetEntityValue(string.Empty, planItem.ServiceUnitID, planItem.ParamedicID,
                                    planItem.ControlPlanDateTime.Date.ToShortDateString(), "AUTO", string.Empty,
                                    PatientID, pat.FirstName, pat.MiddleName, pat.LastName, pat.DateOfBirth.Value.Date.ToShortDateString(), pat.CityOfBirth, pat.Sex,
                                    pat.StreetName, pat.District, pat.City, pat.County, pat.State, pat.ZipCode,
                                    pat.PhoneNo, pat.Email, pat.Ssn, pat.GuarantorID, pat.Notes, AppSession.Parameter.AppointmentStatusOpen,
                                    pat.MobilePhoneNo, "", "", 0, AppSession.UserLogin.UserID, AppSession.Parameter.AppointmentTypeControlPlan, null, RegistrationNo);

                                planItem.AppointmentTime = slot["AppointmentTime"].ToString();
                                planItem.AppointmentQue = slot["AppointmentQue"].ToInt();
                                planItem.AppointmentNo = slot["AppointmentNo"].ToString();

                                if (appointmentNos == string.Empty)
                                    appointmentNos = planItem.AppointmentNo;
                                else
                                    appointmentNos = ";" + planItem.AppointmentNo;
                            }
                            catch (Exception ex)
                            {
                                args.MessageText = ex.Message;
                                args.IsCancel = true;
                            }
                        }
                        else
                        {
                            var qSlot = new ServiceUnitParamedic();
                            if (qSlot.LoadByPrimaryKey(planItem.ServiceUnitID, planItem.ParamedicID) && qSlot.IsUsingQue == true)
                            {
                                try
                                {
                                    // Parameter fromRegistrationNo diisi null supaya tidak terjadi merge billing di reg dari appt nya (Handono 231110 req by Imel)
                                    var slot = Temiang.Avicenna.WebService.V1_1.AppointmentWS.AppointmentPostRanapSetEntityValue(string.Empty, planItem.ServiceUnitID, planItem.ParamedicID,
                                        planItem.ControlPlanDateTime.Date.ToShortDateString(), "AUTO", string.Empty,
                                        PatientID, pat.FirstName, pat.MiddleName, pat.LastName, pat.DateOfBirth.Value.Date.ToShortDateString(), pat.CityOfBirth, pat.Sex,
                                        pat.StreetName, pat.District, pat.City, pat.County, pat.State, pat.ZipCode,
                                        pat.PhoneNo, pat.Email, pat.Ssn, pat.GuarantorID, pat.Notes, AppSession.Parameter.AppointmentStatusOpen,
                                        pat.MobilePhoneNo, "", "", 0, AppSession.UserLogin.UserID, AppSession.Parameter.AppointmentTypeControlPlan, null, RegistrationNo);

                                    planItem.AppointmentTime = slot["AppointmentTime"].ToString();
                                    planItem.AppointmentQue = slot["AppointmentQue"].ToInt();
                                    planItem.AppointmentNo = slot["AppointmentNo"].ToString();

                                    if (appointmentNos == string.Empty)
                                        appointmentNos = planItem.AppointmentNo;
                                    else
                                        appointmentNos = ";" + planItem.AppointmentNo;
                                }
                                catch (Exception ex)
                                {
                                    args.MessageText = ex.Message;
                                    args.IsCancel = true;
                                }
                            }
                        }
                    }
                }
            }

            dsByNurse.ControlPlan = JsonConvert.SerializeObject(oplan);

            return appointmentNos;
        }

        //private void SetPlanControl_bak(ValidateArgs args, MedicalDischargeSummaryByNurse dsByNurse)
        //{
        //    var oplan = controlPlanCtl.GetControlPlan();

        //    // Save in appointment
        //    var pat = new Patient();
        //    pat.LoadByPrimaryKey(PatientID);
        //    foreach (Temiang.Avicenna.BusinessObject.JsonField.ControlPlanItem planItem in oplan.Items)
        //    {
        //        if (planItem.ControlPlanDateTime > DateTime.Today
        //            && !string.IsNullOrEmpty(planItem.ServiceUnitID)
        //            && !string.IsNullOrEmpty(planItem.ParamedicID)
        //            && string.IsNullOrEmpty(planItem.AppointmentNo))
        //        {
        //            var qSchedule = new ParamedicScheduleDate();
        //            if (qSchedule.LoadByPrimaryKey(planItem.ServiceUnitID, planItem.ParamedicID, planItem.ControlPlanDateTime.Year.ToString(), planItem.ControlPlanDateTime.Date))
        //            {
        //                try
        //                {
        //                    // Parameter fromRegistrationNo diisi null supaya tidak terjadi merge billing di reg dari appt nya (Handono 231110 req by Imel)
        //                    var slot = Temiang.Avicenna.WebService.V1_1.AppointmentWS.AppointmentPostRanapSetEntityValue(string.Empty, planItem.ServiceUnitID, planItem.ParamedicID,
        //                        planItem.ControlPlanDateTime.Date.ToShortDateString(), "AUTO", string.Empty,
        //                        PatientID, pat.FirstName, pat.MiddleName, pat.LastName, pat.DateOfBirth.Value.Date.ToShortDateString(), pat.CityOfBirth, pat.Sex,
        //                        pat.StreetName, pat.District, pat.City, pat.County, pat.State, pat.ZipCode,
        //                        pat.PhoneNo, pat.Email, pat.Ssn, pat.GuarantorID, pat.Notes, AppSession.Parameter.AppointmentStatusOpen,
        //                        pat.MobilePhoneNo, "", "", 0, AppSession.UserLogin.UserID, AppSession.Parameter.AppointmentTypeControlPlan, null);

        //                    planItem.AppointmentTime = slot["AppointmentTime"].ToString();
        //                    planItem.AppointmentQue = slot["AppointmentQue"].ToInt();
        //                    planItem.AppointmentNo = slot["AppointmentNo"].ToString();
        //                }
        //                catch (Exception ex)
        //                {
        //                    args.MessageText = ex.Message;
        //                    args.IsCancel = true;
        //                }
        //            }
        //            else
        //            {
        //                var qSlot = new ServiceUnitParamedic();
        //                if (qSlot.LoadByPrimaryKey(planItem.ServiceUnitID, planItem.ParamedicID) && qSlot.IsUsingQue == true)
        //                {
        //                    try
        //                    {
        //                        // Parameter fromRegistrationNo diisi null supaya tidak terjadi merge billing di reg dari appt nya (Handono 231110 req by Imel)
        //                        var slot = Temiang.Avicenna.WebService.V1_1.AppointmentWS.AppointmentPostRanapSetEntityValue(string.Empty, planItem.ServiceUnitID, planItem.ParamedicID,
        //                            planItem.ControlPlanDateTime.Date.ToShortDateString(), "AUTO", string.Empty,
        //                            PatientID, pat.FirstName, pat.MiddleName, pat.LastName, pat.DateOfBirth.Value.Date.ToShortDateString(), pat.CityOfBirth, pat.Sex,
        //                            pat.StreetName, pat.District, pat.City, pat.County, pat.State, pat.ZipCode,
        //                            pat.PhoneNo, pat.Email, pat.Ssn, pat.GuarantorID, pat.Notes, AppSession.Parameter.AppointmentStatusOpen,
        //                            pat.MobilePhoneNo, "", "", 0, AppSession.UserLogin.UserID, AppSession.Parameter.AppointmentTypeControlPlan, null);

        //                        planItem.AppointmentTime = slot["AppointmentTime"].ToString();
        //                        planItem.AppointmentQue = slot["AppointmentQue"].ToInt();
        //                        planItem.AppointmentNo = slot["AppointmentNo"].ToString();
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        args.MessageText = ex.Message;
        //                        args.IsCancel = true;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    dsByNurse.ControlPlan = JsonConvert.SerializeObject(oplan);

        //}

    }
}
