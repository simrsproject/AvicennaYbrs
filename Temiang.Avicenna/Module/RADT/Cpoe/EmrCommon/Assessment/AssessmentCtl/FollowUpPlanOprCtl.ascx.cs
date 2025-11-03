using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Telerik.Web.UI.Barcode;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    /// <summary>
    /// Control Rencana Tindak Lanjut
    /// Dipakai di RSMM dan RS Selanjutnya
    /// Created : 2019-10 by Handono
    /// </summary>
    public partial class FollowUpPlanOprCtl : BaseAssessmentCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            var reg = new Registration(); //kode ini dipindah ke OnInit agar terbaca sat create new //https://sciptaintegrasi.com/mantis/view.php?id=2911
            reg.LoadByPrimaryKey(RegistrationNo);
            // Control Plan
            var plan = new MedicalDischargeSummaryByNurse();
            if (plan.LoadByPrimaryKey(RegistrationNo))
            {
                controlPlanCtl.Populate(plan.ControlPlan);
            }
            else
            {
                var oplan = controlPlanCtl.GetControlPlan();
                controlPlanCtl.PopulatePlanItem(new BusinessObject.JsonField.ControlPlan()
                {
                    Items = new List<BusinessObject.JsonField.ControlPlanItem>()
                    {
                        new BusinessObject.JsonField.ControlPlanItem()
                        {
                            ServiceUnitID = reg.ServiceUnitID,
                            ParamedicID = reg.ParamedicID
                        }
                    }
                });
            }
            //if (IsCallFromCaseMix)
            //    plan.Query.es.QuerySource = "MedicalDischargeSummaryCmx"; // ControlPlan untuk casemix disimpan di table MedicalDischargeSummaryCmx
        }

        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);

            var medsum = new MedicalDischargeSummary();
            if (medsum.LoadByPrimaryKey(RegistrationNo))
            {
                if (!IsEdited)
                {
                    ComboBox.PopulateWithOneStandardReference(cboSRDischargeCondition, AppEnum.StandardReference.DischargeCondition.ToString(), medsum.SRDischargeCondition);
                    ComboBox.PopulateWithOneStandardReference(cboSRDischargeMethod, AppEnum.StandardReference.DischargeMethod.ToString(), medsum.SRDischargeMethod);
                }
                else
                {
                    ComboBox.SelectedValue(cboSRDischargeCondition, medsum.SRDischargeCondition);
                    ComboBox.SelectedValue(cboSRDischargeMethod, medsum.SRDischargeMethod);
                }
            }
            
            rbFupINP.Checked = assessment.FollowUpPlanType == "INP";
            rbFupRHS.Checked = assessment.FollowUpPlanType == "RHS";
            rbFupRPK.Checked = assessment.FollowUpPlanType == "RPK";
            rbFupRFD.Checked = assessment.FollowUpPlanType == "RFD";
            rbFupRDT.Checked = assessment.FollowUpPlanType == "RDT";
            rbFupRHC.Checked = assessment.FollowUpPlanType == "RHC";
            rbFupRJT.Checked = assessment.FollowUpPlanType == "RJT";
            rbFupSUR.Checked = assessment.FollowUpPlanType == "SUR";


            txtRoom.Text = assessment.RoomInPatient;
            txtDayEst.Text = assessment.EstimatedDayInPatient.ToString();
            //txtDPjpInPatient.Text = assessment.DpjpInPatient;

            if (!string.IsNullOrEmpty(assessment.DpjpInPatientID))
                ComboBox.PopulateWithOneParamedic(cboDPjpInPatientID, assessment.DpjpInPatientID);
            else if (!string.IsNullOrEmpty(assessment.DpjpInPatient)) // Free text paramedic name
                    cboDPjpInPatientID.Text = assessment.DpjpInPatient;

            optIsInPatientGuide.SelectedValue = assessment.IsInPatientGuide==true ? "1" : "0";
            txtReferToHospital.Text = assessment.ReferToHospital;
            txtReferToFamilyDoctor.Text = assessment.ReferToFamilyDoctor;
            txtInPatientRejectReason.Text = assessment.InPatientRejectReason;
            txtReferReason.Text = assessment.ReferReason;

            if (assessment.SurgicalDateTime == null)
                txtSurgicalDateTime.Clear();
            else
                txtSurgicalDateTime.SelectedDate = assessment.SurgicalDateTime;

        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment,
            RegistrationInfoMedic rim)
        {
            if (rbFupINP.Checked) assessment.FollowUpPlanType = "INP";
            else if (rbFupRHS.Checked) assessment.FollowUpPlanType = "RHS";
            else if (rbFupRPK.Checked) assessment.FollowUpPlanType = "RPK";
            else if (rbFupRFD.Checked) assessment.FollowUpPlanType = "RFD";
            else if (rbFupRDT.Checked) assessment.FollowUpPlanType = "RDT";
            else if (rbFupRHC.Checked) assessment.FollowUpPlanType = "RHC";
            else if (rbFupSUR.Checked) assessment.FollowUpPlanType = "SUR";
            else if (rbFupRJT.Checked) assessment.FollowUpPlanType = "RJT";

            assessment.RoomInPatient = txtRoom.Text;
            assessment.EstimatedDayInPatient = txtDayEst.Text.ToInt();
            //assessment.DpjpInPatient = txtDPjpInPatient.Text;

            //Reset
            assessment.str.DpjpInPatientID = string.Empty;
            assessment.str.DpjpInPatient = string.Empty;

            //Set value
            if (!string.IsNullOrWhiteSpace(cboDPjpInPatientID.SelectedValue))
            {
                assessment.DpjpInPatientID = cboDPjpInPatientID.SelectedValue;
            }

            if (!string.IsNullOrWhiteSpace(cboDPjpInPatientID.Text))
            {
                assessment.DpjpInPatient = cboDPjpInPatientID.Text;
            }

            assessment.IsInPatientGuide = optIsInPatientGuide.SelectedValue=="1";
            assessment.ReferToHospital = txtReferToHospital.Text;
            assessment.ReferToFamilyDoctor = txtReferToFamilyDoctor.Text;
            assessment.InPatientRejectReason = txtInPatientRejectReason.Text;
            assessment.ReferReason = txtReferReason.Text;

            if (txtSurgicalDateTime.IsEmpty)
                assessment.str.SurgicalDateTime = string.Empty;
            else
                assessment.SurgicalDateTime = txtSurgicalDateTime.SelectedDate;

            var medsum = new MedicalDischargeSummary();
            if (!medsum.LoadByPrimaryKey(RegistrationNo))
            {
                medsum = new MedicalDischargeSummary();
                medsum.RegistrationNo = RegistrationNo;
            }
            medsum.SRDischargeCondition = cboSRDischargeCondition.SelectedValue;
            medsum.SRDischargeMethod = cboSRDischargeMethod.SelectedValue;
            medsum.Save();

            SavePlanControl(args);
        }

        protected override void OnDataModeChanged(bool isEdited)
        {
            if (isEdited)
            {
                // Selanjutkan akan dijalankan OnPopulate jadi jangan ditimpa
                var stdRef = AppParameter.GetParameterValue(AppParameter.ParameterItem.RefDischargeConditionForPresentStatus);
                if (string.IsNullOrWhiteSpace(stdRef))
                    stdRef = AppConstant.RegistrationType.InPatient;

                StandardReference.InitializeIncludeSpace(cboSRDischargeCondition, AppEnum.StandardReference.DischargeCondition, stdRef);
                StandardReference.InitializeIncludeSpace(cboSRDischargeMethod, AppEnum.StandardReference.DischargeMethod, RegistrationType);
                ComboBox.SelectedValue(cboSRDischargeMethod,
                RegistrationType == AppConstant.RegistrationType.EmergencyPatient
                    ? AppParameter.GetParameterValue(AppParameter.ParameterItem.DischargeMethodDefaultEmr)
                    : AppParameter.GetParameterValue(AppParameter.ParameterItem.DischargeMethodDefaultOpr));

            }
        }

        private void SavePlanControl(ValidateArgs args)
        {
            var oplan = controlPlanCtl.GetControlPlan();

            //if (IsCallFromCaseMix)
            //{
            //    var esplan = new MedicalDischargeSummaryCmx();
            //    if (!esplan.LoadByPrimaryKey(RegistrationNo))
            //    {
            //        if (oplan.Items.Count > 0)
            //        {
            //            esplan.RegistrationNo = RegistrationNo;
            //            esplan.ControlPlan = JsonConvert.SerializeObject(oplan);
            //            esplan.Save();
            //        }
            //    }
            //    else
            //    {
            //        esplan.ControlPlan = JsonConvert.SerializeObject(oplan);
            //        esplan.Save();
            //    }
            //    return; // Untuk casemix tidak di link ke yg lain
            //}
            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);

            var nosep = string.Empty;

            if (!string.IsNullOrWhiteSpace(reg.BpjsSepNo))
            {
                var bpjs = new BpjsSEPCollection();
                bpjs.Query.es.Top = 1;
                bpjs.Query.Where(bpjs.Query.NoSEP == reg.BpjsSepNo);
                bpjs.Query.OrderBy(bpjs.Query.LastUpdateDateTime.Descending);
                if (bpjs.Query.Load())
                //if (bpjs.NoSEP != null) //imel 23 sept 2023
                {
                    nosep = bpjs.First().NoSEP;
                }
            }

            //if (bpjs.LoadByPrimaryKey(reg.BpjsSepNo)) nosep = reg.BpjsSepNo;

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
                        var apptq = new AppointmentQuery();
                        apptq.Where(apptq.AppointmentNo == appointmentNo, apptq.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel);

                        var appt = new BusinessObject.Appointment();
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
                        var apptq = new AppointmentQuery();
                        apptq.Where(apptq.ServiceUnitID == planItem.ServiceUnitID, apptq.ParamedicID == planItem.ParamedicID, apptq.PatientID == pat.PatientID,
                            apptq.AppointmentDate == planItem.ControlPlanDateTime.Date, apptq.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel,
                            apptq.SRAppoinmentType == AppSession.Parameter.AppointmentTypeControlPlan);
                        apptq.Select(apptq.AppointmentNo, apptq.AppointmentQue, apptq.AppointmentTime);

                        var appt = new BusinessObject.Appointment();
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
                                var slot = Temiang.Avicenna.WebService.V1_1.AppointmentWS.AppointmentSetEntityValue(string.Empty, planItem.ServiceUnitID, planItem.ParamedicID,
                                    planItem.ControlPlanDateTime.Date.ToShortDateString(), "AUTO", string.Empty,
                                    PatientID, pat.FirstName, pat.MiddleName, pat.LastName, pat.DateOfBirth.Value.Date.ToShortDateString(), pat.CityOfBirth, pat.Sex,
                                    pat.StreetName, pat.District, pat.City, pat.County, pat.State, pat.ZipCode,
                                    pat.PhoneNo, pat.Email, pat.Ssn, pat.GuarantorID, nosep, AppSession.Parameter.AppointmentStatusOpen,
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
                                    var slot = Temiang.Avicenna.WebService.V1_1.AppointmentWS.AppointmentSetEntityValue(string.Empty, planItem.ServiceUnitID, planItem.ParamedicID,
                                        planItem.ControlPlanDateTime.Date.ToShortDateString(), "AUTO", string.Empty,
                                        PatientID, pat.FirstName, pat.MiddleName, pat.LastName, pat.DateOfBirth.Value.Date.ToShortDateString(), pat.CityOfBirth, pat.Sex,
                                        pat.StreetName, pat.District, pat.City, pat.County, pat.State, pat.ZipCode,
                                        pat.PhoneNo, pat.Email, pat.Ssn, pat.GuarantorID, nosep, AppSession.Parameter.AppointmentStatusOpen,
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

            var ent = new MedicalDischargeSummaryByNurse();
            if (!ent.LoadByPrimaryKey(RegistrationNo))
            {
                if (oplan.Items.Count > 0)
                {
                    ent.RegistrationNo = RegistrationNo;
                    ent.ControlPlan = JsonConvert.SerializeObject(oplan);
                    ent.Save();
                }
            }
            else
            {
                ent.ControlPlan = JsonConvert.SerializeObject(oplan);
                ent.Save();
            }

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
        }
        #endregion
    }
}