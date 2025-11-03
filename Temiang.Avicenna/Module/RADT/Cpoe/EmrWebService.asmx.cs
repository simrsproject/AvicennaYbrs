using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.Module.RADT.Emr.MainContent;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    /// <summary>
    /// Summary description for EmrWebService
    /// </summary>
    ///

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class EmrWebService : System.Web.Services.WebService
    {
        #region Print Parameter
        [WebMethod(EnableSession = true)]
        public void PopulatePrintParameter(string programID, string registrationNo)
        {
            PrintDialog.PopulatePrintParameter(programID, registrationNo);
        }

        #region Print Integrated Notes
        [WebMethod(EnableSession = true)]
        public void PopulatePrintParameterIntegratedNoteRekap(string registrationNo, string entryBy)
        {
            PrintDialog.PopulatePrintParameter(AppConstant.Report.IntegratedNotesRekap, registrationNo);
        }

        [WebMethod(EnableSession = true)]
        public void PopulatePrintParameterIntegratedNotes(string parameterValue)
        {
            if (parameterValue.Contains("_"))
            {
                // Print from EpisodeSoape
                PrintEpisodeSoap(parameterValue);
            }
            else
            {
                // Check is Initial Assessment
                var asses = new PatientAssessment();
                asses.Query.Where(asses.Query.RegistrationInfoMedicID == parameterValue);
                if (asses.Load(asses.Query))
                {
                    PrintAssessment(asses);
                }
                else
                {
                    PrintIntegratedNote(parameterValue);
                }
            }
        }



        //0003994 daniel
        [WebMethod(EnableSession = true)]
        public void PopulatePrintParameterTemplatePpaNotes(string registrationNo, string templateId)
        {
            var jobParameters = new PrintJobParameterCollection();
            var jobParameter = jobParameters.AddNew();
            jobParameter.Name = "p_RegistrationNo";
            jobParameter.ValueString = registrationNo;

            var jobParameter2 = jobParameters.AddNew();
            jobParameter2.Name = "p_TemplateID";
            jobParameter2.ValueString = templateId;

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = AppConstant.Report.TemplatePpaNotes;
        }
        //0003994 daniel
        private void PrintAssessment(PatientAssessment asses)
        {
            var jobParameters = new PrintJobParameterCollection();

            var asmType = new AppSRAssessmentType();
            asmType.LoadByPrimaryKey(asses.SRAssessmentType);
            AppSession.PrintJobReportID = asmType.ReportProgramID;

            var mtx = new AppControlEntryMatrix();
            var entryType = string.Format("ASSESSMENT-{0}", asses.SRAssessmentType);
            mtx.Query.Where(mtx.Query.Or(mtx.Query.HealthcareInitialAppsVersion == AppSession.Parameter.HealthcareInitialAppsVersion, mtx.Query.HealthcareInitialAppsVersion == "DEFAULT"),
                mtx.Query.EntryType == entryType, mtx.Query.IsVisible == true);
            mtx.Query.es.Top = 1;

            // Cek jika ada matrix berarti initial atau lanjutannya diabaikan
            if (!mtx.Query.Load())
            {
                // Override for previouse assessment (Old Code) ( RS Muhamadiyah Palembang )
                if (!asses.IsInitialAssessment ?? false)
                {
                    // Program lama
                    switch (asses.SRAssessmentType)
                    {
                        case "DENTS": // 2. asesmen gigi
                            AppSession.PrintJobReportID = AppConstant.Report.ContinuedAssessment.Dentis;
                            break;
                        case "NURSE": // 10. asesmen Kebidanan
                            AppSession.PrintJobReportID = AppConstant.Report.ContinuedAssessment.Nursing;
                            break;
                        case "PKAND": // 10. asesmen Penyakit Kandungan
                            AppSession.PrintJobReportID = AppConstant.Report.ContinuedAssessment.Gynecology;
                            break;
                        default:
                            AppSession.PrintJobReportID = AppConstant.Report.ContinuedAssessment.General;
                            break;
                    }

                }
                jobParameters.AddNew("PatientID", asses.PatientID);
                jobParameters.AddNew("SRAssessmentType", asses.SRAssessmentType);
                jobParameters.AddNew("RegistrationNo", asses.RegistrationNo);
                jobParameters.AddNew("RegistrationInfoMedicID", asses.RegistrationInfoMedicID);
                AppSession.PrintJobParameters = jobParameters;

            }
            else
            {
                // Yg pakai matrix pakai XML dg webservice
                jobParameters.AddNew("accessKey", "123");
                jobParameters.AddNew("registrationInfoMedicID", asses.RegistrationInfoMedicID);
                AppSession.PrintJobParameters = jobParameters;
            }
        }

        private void PrintIntegratedNote(string keyValue)
        {
            var jobParameters = new PrintJobParameterCollection();

            var rim = new RegistrationInfoMedic();
            rim.LoadByPrimaryKey(keyValue);
            switch (rim.SRMedicalNotesInputType)
            {
                case "MDS":
                    {
                        // Print from Medical Discharge Summary
                        var jobParameter = jobParameters.AddNew();
                        jobParameter.Name = "p_RegistrationNo";
                        jobParameter.ValueString = rim.RegistrationNo;

                        AppSession.PrintJobParameters = jobParameters;

                        var reg = new Registration();
                        reg.LoadByPrimaryKey(rim.RegistrationNo);
                        switch (reg.SRRegistrationType)
                        {
                            case "OPR":
                                AppSession.PrintJobReportID = "SLP.01.0085";
                                break;
                            case "EMR":
                                AppSession.PrintJobReportID = "SLP.01.0077";
                                break;
                            default:
                                AppSession.PrintJobReportID = "SLP.01.0089";
                                break;
                        }

                        break;
                    }

                default:
                    {
                        // Print from Integrated Note
                        var jobParameter = jobParameters.AddNew();
                        jobParameter.Name = "RegistrationNo";
                        jobParameter.ValueString = keyValue;

                        var jobParameter2 = jobParameters.AddNew();
                        jobParameter2.Name = "SequenceNo";
                        jobParameter2.ValueString = "";

                        AppSession.PrintJobParameters = jobParameters;
                        AppSession.PrintJobReportID = AppConstant.Report.SOAP;
                        break;
                    }
            }



        }

        private void PrintEpisodeSoap(string keyValue)
        {
            var keyValues = keyValue.Split('_');
            if (keyValues.Length > 0)
            {
                var jobParameters = new PrintJobParameterCollection();

                var jobParameter = jobParameters.AddNew();
                jobParameter.Name = "RegistrationNo";
                jobParameter.ValueString = keyValues[0];

                var jobParameter2 = jobParameters.AddNew();
                jobParameter2.Name = "SequenceNo";
                jobParameter2.ValueString = keyValues[1];

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.SOAP;

            }
        }


        #endregion

        [WebMethod(EnableSession = true)]
        public void PopulatePrintParameterDischarge(string registrationNo)
        {

            var dd = new RegistrationDischargeDetail();
            if (dd.LoadByPrimaryKey(registrationNo))
            {
                var jobParameters = new PrintJobParameterCollection();

                var jobParameter = jobParameters.AddNew();
                jobParameter.Name = "RegistrationNo";
                jobParameter.ValueString = registrationNo;

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.InpatientAdmissionLetter;

            }
        }

        [WebMethod(EnableSession = true)]
        public void PopulatePrintQuestionForm(string transactionNo, string registrationNo, string questionFormID)
        {
            var jobParameters = new PrintJobParameterCollection();
            jobParameters.AddNew("p_RegistrationNo", registrationNo);
            jobParameters.AddNew("p_QuestionFormID", questionFormID);
            jobParameters.AddNew("p_TransactionNo", transactionNo);

            AppSession.PrintJobParameters = jobParameters;

            var form = new QuestionForm();
            form.LoadByPrimaryKey(questionFormID);
            AppSession.PrintJobReportID = form.ReportProgramID;
        }

        [WebMethod(EnableSession = true)]
        public void PopulatePrintParameterNosocomial(string registrationNo, int monitoringNo, string userName)
        {
            var jobParameters = new PrintJobParameterCollection();
            jobParameters.AddNew("p_RegistrationNo", registrationNo);
            jobParameters.AddNew("p_MonitoringNo", monitoringNo);
            jobParameters.AddNew("p_UserName", userName);

            AppSession.PrintJobParameters = jobParameters;

            var nm = new NosocomialMonitoring();
            nm.LoadByPrimaryKey(registrationNo, monitoringNo);
            switch (nm.MonitoringType)
            {
                case "BDR":
                    AppSession.PrintJobReportID = "FRM.HAIS.BDR";
                    break;
                case "INF":
                    AppSession.PrintJobReportID = "FRM.HAIS.INF";
                    break;
                case "CAT":
                    AppSession.PrintJobReportID = "FRM.HAIS.CAT";
                    break;
                case "ETT":
                    AppSession.PrintJobReportID = "FRM.HAIS.ETT";
                    break;
                case "SUR":
                    AppSession.PrintJobReportID = "FRM.HAIS.SUR";
                    break;
                case "NGT":
                    break;

            }
        }

        [WebMethod(EnableSession = true)]
        public void PopulatePrintServiceUnitBooking(string bookingNo)
        {
            var jobParameters = new PrintJobParameterCollection();
            var jobParameter = jobParameters.AddNew();
            jobParameter.Name = "p_BookingNo";
            jobParameter.ValueString = bookingNo;

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = AppConstant.Report.ServiceUnitBooking;
        }

        [WebMethod(EnableSession = true)]
        public void PopulatePrintOperatingNotes(string bookingNo, string seqNo)
        {
            var jobParameters = new PrintJobParameterCollection();
            var jobParameter = jobParameters.AddNew();
            jobParameter.Name = "p_BookingNo";
            jobParameter.ValueString = bookingNo;

            var jobParameter2 = jobParameters.AddNew();
            jobParameter2.Name = "p_OpNotesSeqNo";
            jobParameter2.ValueString = seqNo;

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = AppConstant.Report.OperatingNotesRpt;
        }

        [WebMethod(EnableSession = true)]
        public void PopulatePrintAnesthesistNotes(string bookingNo)
        {
            var jobParameters = new PrintJobParameterCollection();
            var jobParameter = jobParameters.AddNew();
            jobParameter.Name = "p_BookingNo";
            jobParameter.ValueString = bookingNo;

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = AppConstant.Report.AnesthesistNotesRpt;
        }

        [WebMethod(EnableSession = true)]
        public void PopulatePrintRadiologyResult(string transactionNo, string sequenceNo)
        {
            var jobParameters = new PrintJobParameterCollection();
            var jpTrNo = jobParameters.AddNew();
            jpTrNo.Name = "p_TransactionNo";
            jpTrNo.ValueString = transactionNo;

            var jpSeqNo = jobParameters.AddNew();
            jpSeqNo.Name = "p_SequenceNo";
            jpSeqNo.ValueString = sequenceNo;

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = AppConstant.Report.RadiologyResult;
        }

        [WebMethod(EnableSession = true)]
        public void PopulatePrintExamOrderOtherResult(string transactionNo, string sequenceNo)
        {
            var jobParameters = new PrintJobParameterCollection();
            var jpTrNo = jobParameters.AddNew();
            jpTrNo.Name = "p_TransactionNo";
            jpTrNo.ValueString = transactionNo;

            var jpSeqNo = jobParameters.AddNew();
            jpSeqNo.Name = "p_SequenceNo";
            jpSeqNo.ValueString = sequenceNo;

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = AppSession.Parameter.ProgramIdPrintExamOrderOtherResult;
        }

        [WebMethod(EnableSession = true)]
        public void PopulatePrintPaResult(string resultNo, string reportId)
        {
            var jobParameters = new PrintJobParameterCollection();
            var jpTrNo = jobParameters.AddNew();
            jpTrNo.Name = "p_ResultNo";
            jpTrNo.ValueString = resultNo;

            var jpSeqNo = jobParameters.AddNew();
            jpSeqNo.Name = "p_UserID";
            jpSeqNo.ValueString = AppSession.UserLogin.UserID;

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = reportId;
        }

        [WebMethod(EnableSession = true)]
        public void PopulatePrintParameterCarePlan(string programID, string registrationNo, string transactionNo)
        {
            var jobParameters = new PrintJobParameterCollection();
            if (programID == AppConstant.Report.NursingCareNotes)
                jobParameters.AddNew("p_RegistrationNo", registrationNo);
            else
                jobParameters.AddNew("p_TransactionNo", transactionNo);

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = programID;
        }
        #endregion

        [WebMethod]
        public string ServiceUnitList(string registrationType, string userType, string userID)
        {
            var units = new ServiceUnitCollection();
            var query = new ServiceUnitQuery("a");


            if (string.IsNullOrEmpty(registrationType))
            {
                // ServiceUnit OK SubQuery
                var srOK = new ServiceRoomQuery("sr");
                srOK.Select(srOK.ServiceUnitID);
                srOK.Where(srOK.IsOperatingRoom == true, srOK.IsShowOnBookingOT == true,
                    srOK.ServiceUnitID == query.ServiceUnitID);

                query.Where(query.Or(
                        query.SRRegistrationType.In(
                            AppConstant.RegistrationType.InPatient,
                            AppConstant.RegistrationType.EmergencyPatient,
                            AppConstant.RegistrationType.OutPatient,
                            AppConstant.RegistrationType.MedicalCheckUp
                        ), query.ServiceUnitID.In(srOK)),
                    query.IsActive == true
                );
            }
            else
                query.Where(
                    query.SRRegistrationType == registrationType,
                    query.IsActive == true
                );

            if (userType == AppUser.UserType.Nurse)
            {
                // Yg lainnya bisa lintas serviceUnit
                var ausu = new AppUserServiceUnitQuery("ausu");
                query.InnerJoin(ausu).On(query.ServiceUnitID == ausu.ServiceUnitID &&
                                         ausu.UserID == userID);
            }

            query.OrderBy(units.Query.ServiceUnitName.Ascending);
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            units.Load(query);
            var retval = string.Empty;
            foreach (ServiceUnit su in units)
            {
                retval = string.Concat(retval, string.Format("{0}_{1}|", su.ServiceUnitID, su.ServiceUnitName));
            }
            return retval;
        }


        [WebMethod(EnableSession = true)]
        public string AddExamOrderAbortStatus(string registrationNo)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);

            if (reg.IsClosed ?? false)
                return "Registration has closed, can't add Exam Order";

            if (reg.IsLockVerifiedBilling ?? false)
                return "Registration has Lock Verified Billing, can't add Exam Order";

            if (AppParameter.IsYes(AppParameter.ParameterItem.IsBillingEmrAddButtonEnabled) && reg.IsHoldTransactionEntry == true)
                return "This Registration has been Lock verified billing, can't add Exam Order";

            // Check Asesmen / SOAP
            if ((AppParameter.IsYes(AppParameter.ParameterItem.IsExamOrderIprMustAssessmentFirst) && reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
    || (AppParameter.IsYes(AppParameter.ParameterItem.IsExamOrderOprMustAssessmentFirst) && reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient)
    || (AppParameter.IsYes(AppParameter.ParameterItem.IsExamOrderEmrMustAssessmentFirst) && reg.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient)
    )
            {
                var soap = new RegistrationInfoMedic();
                soap.Query.Where(soap.Query.RegistrationNo == registrationNo);
                soap.Query.es.Top = 1;
                if (!soap.Query.Load())
                    return "Please entry assessment first";

            }

            if (AppSession.UserLogin.SRUserType == AppUser.UserType.Nurse)
                return string.Empty;

            if (!BasePage.IsUserInParamedicTeam(registrationNo, true, reg.ServiceUnitID, reg.SRRegistrationType))
                return "Sorry you are not a team of paramedics of this patient, you cannot add add Exam Order this patient";

            return string.Empty;
        }


        [WebMethod(EnableSession = true)]
        public string AddParamedicTransChargesStatus(string registrationNo, string paramedicID)
        {
            if (string.IsNullOrWhiteSpace(paramedicID))
                return "hide";

            // Cek apakah sudah dibuat transaksi TransCharges untuk dokter bersangkutan
            var ptc = new ParamedicTransCharges();
            if (ptc.LoadByPrimaryKey(registrationNo, paramedicID))
            {
                return "hide";
            }
            return "show";
        }


        [WebMethod(EnableSession = true)]
        public string TakeOverRegistration(string registrationNo)
        {
            try
            {
                var reg = new Registration();
                if (reg.LoadByPrimaryKey(registrationNo))
                {
                    Helper.RegistrationOpenClose.EditPhysician(reg, AppSession.UserLogin.ParamedicID, AppSession.UserLogin.ParamedicName, "", "", "", "");
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
            return string.Empty;
        }

        //[WebMethod(EnableSession = true)]
        //public string SaveReconImageSign(string registrationNo, string reconSeqNo, string signImage)
        //{
        //    var recon = new MedicationRecon();
        //    if (!recon.LoadByPrimaryKey(registrationNo, reconSeqNo.ToInt()))
        //        return "Recon not exist";

        //    // Set Sign Value
        //    if (!string.IsNullOrWhiteSpace(signImage))
        //    {
        //        try
        //        {
        //            var imgHelper = new ImageHelper();
        //            var resized = imgHelper.ResizeImage(imgHelper.ToImage(signImage), new System.Drawing.Size(332, 185));
        //            recon.SignImage = imgHelper.ToByteArray(resized, ImageFormat.Png);
        //            recon.Save();
        //        }
        //        catch (Exception ex)
        //        {
        //            return ex.Message;
        //        }

        //    }
        //    return "OK";
        //}

        #region Status untuk EMR List

        [WebMethod]
        public string UpdateStateEmrList(string statType, string regNo, string fromRegNo, string regType, string patID, string dob, string parID)
        {
            switch (statType)
            {
                case "ews":
                    return EwsScoreLevelHtml(regNo, fromRegNo, regType, patID, dob);
                case "presc":
                    return PrescriptionProgress(regNo, regType);
                case "pathway":
                    return RegistrationPathwayStatuslHtml(regNo);
                case "soap":
                    return SoapEntryStatuslHtml(regNo, parID, regType);
                case "lab":
                    return ExamOrderLabProgress(regNo, regType);
                case "rad":
                    return ExamOrderRadProgress(regNo, regType);
                case "plafond":
                case "plafondt":
                case "plafamt":
                    return PlafondProgress(regNo, statType);
                case "billamt":
                    return BillingAmtInfo(regNo);
                case "riskcol":
                    return RiskColorHtml(regNo);
            }

            return string.Empty;
        }

        private string RegistrationPathwayStatuslHtml(string regNo)
        {
            var rpQr = new RegistrationPathwayQuery("a");
            rpQr.es.Top = 1;
            rpQr.es.WithNoLock = true;

            rpQr.Where(rpQr.RegistrationNo == regNo, rpQr.PathwayID != string.Empty);

            var rp = new RegistrationPathway();
            if (rp.Load(rpQr) && !string.IsNullOrEmpty(rp.PathwayStatus))
            {
                return (rp.PathwayStatus == "A" ? string.Format("<img src='{0}/Images/Toolbar/post_green_16.png'/>", Helper.UrlRoot()) : (rp.PathwayStatus == "F" ? string.Format("<img src='{0}/Images/Toolbar/cancel16.png'/>", Helper.UrlRoot()) : string.Format("<img src='{0}/Images/Toolbar/row_delete16_d.png'/>", Helper.UrlRoot())));
            }

            return string.Empty;
        }

        private string EwsScoreLevelHtml(string regNo, string fromRegNo, string regType, string patID,
            string dob)
        {
            if (!regType.Equals(AppConstant.RegistrationType.InPatient))
                return string.Empty;

            // EWS for InPatient
            string ewsLastTotalScoreValue = string.Empty;
            string ewsLastTotalLevelColor = string.Empty;
            string statType = string.Empty;
            DateTime vitalSignRecordDate = DateTime.Today;
            bool isExist = VitalSign.LastEwsTotalScore(regNo, fromRegNo, Convert.ToDateTime(dob),
                ref ewsLastTotalScoreValue, ref ewsLastTotalLevelColor, ref vitalSignRecordDate, DateTime.Now, ref statType);

            if (isExist)
            {
                return string.Format(
                    "<div style='font-weight: bold;text-align: center;background-color: {0};width: 100%; padding: 1px'><a href=\"#\" onclick=\"javascript:openVitalSignChartEws('{2}','{3}','{4}','{5}','{6}'); return false;\">{1}</a></div>",
                    ewsLastTotalLevelColor,
                    ewsLastTotalScoreValue,
                    patID,
                    regNo,
                    fromRegNo,
                    vitalSignRecordDate,
                    statType);
            }

            return string.Empty;
        }

        private string SoapEntryStatuslHtml(string regNo, string parID, string regType)
        {

            // Cek di Integrated Note
            var rimQr = new RegistrationInfoMedicQuery();
            rimQr.es.Top = 1;
            rimQr.es.WithNoLock = true;

            rimQr.Where(rimQr.RegistrationNo == regNo,
                rimQr.Or(rimQr.IsDeleted.IsNull(), rimQr.IsDeleted == false), rimQr.SRMedicalNotesInputType == "SOAP", rimQr.Info1 != string.Empty);

            if (regType == AppConstant.RegistrationType.InPatient)
            {
                // Untuk in patient cek hari ini apakah sudah diisi soapnya
                rimQr.Where(rimQr.ParamedicID == parID, rimQr.DateTimeInfo > DateTime.Today);
            }

            var rim = new RegistrationInfoMedic();
            if (rim.Load(rimQr))
            {
                return string.Format("<img src='{0}/Images/Toolbar/post_green_16.png'/>", Helper.UrlRoot());
            }
            return string.Empty;
        }

        private string PrescriptionProgress(string regNo, string regType)
        {
            if (regType != AppConstant.RegistrationType.InPatient) return string.Empty;

            var pQr = new TransPrescriptionQuery("tp");

            // Return Prescription
            var subQr = new TransPrescriptionQuery("tp");
            subQr.Where(subQr.RegistrationNo == regNo, subQr.ReferenceNo == pQr.PrescriptionNo, subQr.IsPrescriptionReturn == true, subQr.IsApproval == true);
            subQr.Select(subQr.ReferenceNo);

            // Hitung jumlah resep
            pQr.Where(pQr.RegistrationNo == regNo, pQr.IsPrescriptionReturn == false, pQr.Or(pQr.IsVoid.IsNull(), pQr.IsVoid == false));
            pQr.Where(pQr.PrescriptionNo.NotIn(subQr));
            pQr.Select(pQr.PrescriptionNo.Count());
            pQr.es.WithNoLock = true;
            var dtb = pQr.LoadDataTable();
            if (dtb.Rows[0][0].ToInt() == 0) return string.Empty;

            // Hitung jumlah yg sudah komplit dan belum dideliver
            pQr = new TransPrescriptionQuery("tp");
            pQr.Where(pQr.RegistrationNo == regNo, pQr.CompleteDateTime.IsNotNull(), pQr.IsPrescriptionReturn == false, pQr.DeliverDateTime.IsNull(), pQr.Or(pQr.IsVoid.IsNull(), pQr.IsVoid == false));
            pQr.Where(pQr.PrescriptionNo.NotIn(subQr));
            pQr.Select(pQr.PrescriptionNo.Count());
            pQr.es.WithNoLock = true;
            dtb = pQr.LoadDataTable();
            var completeCount = dtb.Rows[0][0].ToInt();


            // Hitung jumlah resp yg belum diproses
            pQr = new TransPrescriptionQuery("tp");
            pQr.Where(pQr.RegistrationNo == regNo, pQr.CompleteDateTime.IsNull(), pQr.IsPrescriptionReturn == false, pQr.Or(pQr.IsVoid.IsNull(), pQr.IsVoid == false));
            pQr.Select(pQr.PrescriptionNo.Count());
            pQr.Where(pQr.PrescriptionNo.NotIn(subQr));
            pQr.es.WithNoLock = true;
            dtb = pQr.LoadDataTable();
            var notCompleteCount = dtb.Rows[0][0].ToInt();

            var retVal = string.Empty;

            if (notCompleteCount > 0 && completeCount == 0)
                retVal = string.Format(@"<div style='font-weight: bold;color: white;text-align: center;background:gray;width: 100%; padding: 1px'>{0}</div>", notCompleteCount);

            else if (notCompleteCount > 0 && completeCount > 0)
                retVal = string.Format(@"<div style='font-weight: bold;color: white;text-align: center;background:orange;width: 100%; padding: 1px'>{0}</div>", completeCount);

            else if (notCompleteCount == 0 && completeCount > 0)
                retVal = string.Format(@"<div style='font-weight: bold;color: white;text-align: center;background:green;width: 100%; padding: 1px'>{0}</div>", completeCount);

            else
                retVal = "<div style='font-weight: bold;color: green;text-align: center;background:green;width: 100%; padding: 1px'>&nbsp;</div>";

            return string.Format("<a href=\"#\" onclick=\"openPendingPrescription('{0}','{1}'); return false;\">{2}</a>", regNo, regType, retVal);
        }

        private string ExamOrderLabProgress(string regNo, string regType)
        {
            var tc = new TransChargesQuery("b");
            tc.Where(tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID, tc.RegistrationNo == regNo);
            tc.Select(tc.TransactionNo);
            tc.es.WithNoLock = true;
            var dtbLab = tc.LoadDataTable();

            if (dtbLab.Rows.Count == 0) return string.Empty;

            var fractionCount = 0;
            var resultCount = 0;

            foreach (DataRow testLab in dtbLab.Rows)
            {
                var labResult = LaboratoryResult(testLab[0].ToString(), regNo);
                foreach (DataRow row in labResult.Rows)
                {
                    if (row["IsFraction"].ToBoolean() == true)
                    {
                        fractionCount++;
                        var result = row["Result"].ToString();
                        if (!string.IsNullOrWhiteSpace(result) && result.ToLower() != "menyusul")
                            resultCount++;
                    }
                }
            }


            if (fractionCount == 0) return "<div style='background:gray;width: 100%; padding: 1px'>&nbsp;</div>";

            return string.Format(@"<div style='background:{0};color: white;text-align: center;width: 100%; padding: 1px'>{1}/{2}</div>", fractionCount == resultCount ? "green" : (resultCount == 0 ? "gray" : "orange"), resultCount, fractionCount);
        }
        private string ExamOrderRadProgress(string regNo, string regType)
        {
            var tc = new TransChargesQuery("b");
            tc.Where(tc.Or(tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID, tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID2), tc.RegistrationNo == regNo);

            tc.Select(tc.TransactionNo);
            tc.es.Top = 1;
            tc.OrderBy(tc.TransactionDate.Descending, tc.TransactionNo.Descending);
            tc.es.WithNoLock = true;
            var dtbLab = tc.LoadDataTable();

            if (dtbLab.Rows.Count == 0) return string.Empty;

            var result = new TestResultQuery("r");
            var tci = new TransChargesItemQuery("tci");
            result.InnerJoin(tci).On(result.ItemID == tci.ItemID & tci.TransactionNo == dtbLab.Rows[0][0].ToString());

            result.Where(result.TransactionNo == dtbLab.Rows[0][0].ToString());
            result.Select(result.TransactionNo);
            result.es.WithNoLock = true;
            var radResult = result.LoadDataTable();
            return string.Format(@"<div style='background:{0};width: 100%; padding: 1px'>&nbsp;</div>", radResult.Rows.Count > 0 ? "green" : "gray");
        }

        private DataTable LaboratoryResult(string transactionNo, string registrationNo)
        {
            if (AppSession.Parameter.IsUsingHisInterop)
            {
                DataTable dtbResult;
                switch (AppSession.Parameter.LisInterop)
                {
                    case "SYSMEX":
                        dtbResult = ExamOrderHistCtl.LabHistOrderResultFromSysmex(transactionNo);
                        return dtbResult;
                    case "RSCH":
                        dtbResult = ExamOrderHistCtl.LabHistOrderResultFromRSCH(transactionNo);
                        return dtbResult;
                    case "WYNAKOM":
                        dtbResult = ExamOrderHistCtl.LabHistOrderResultFromWynakom(transactionNo);
                        return dtbResult;
                    case "LINK_LIS":
                        //sementara masih development
                        dtbResult = new DataTable();
                        break;
                    case "VANSLITE":
                        dtbResult = ExamOrderHistCtl.LabHistOrderResultFromVanslite(transactionNo);
                        return dtbResult;
                    case "ELIMS":
                        dtbResult = ExamOrderHistCtl.LabHistOrderResultFromElims(transactionNo);
                        return dtbResult;
                    default:
                        dtbResult = ExamOrderHistCtl.LabHistOrderResultFromVanslab(transactionNo);
                        return dtbResult;
                }
            }
            return ExamOrderHistCtl.LabHistOrderResultFromManualEntry(transactionNo, registrationNo);

        }

        private string RiskColorHtml(string regNo)
        {
            var reg = new Registration();
            if (!reg.LoadByPrimaryKey(regNo))
                return string.Empty;

            var color = string.Empty;

            if (string.IsNullOrEmpty(reg.SRPatientRiskColor))
                color = "Gray";
            else
            {
                var prColor = new AppStandardReferenceItem();
                prColor.LoadByPrimaryKey("PatientRiskColor", reg.SRPatientRiskColor);
                color = prColor.ReferenceID;
            }

            var divId = string.Concat("riskcol", regNo.Replace("/", "_")); // div updated
            var result = string.Format(
                "<a href=\"#\" onclick=\"openPatientRiskColorDialog('{0}','{2}'); return false;\">{1}</a>",
                regNo,
                (string.IsNullOrWhiteSpace(reg.SRPatientRiskColor) || (reg.SRPatientRiskColor.Equals("0")))
                    ? "<div style=\"background-color: gray; width: 38px; height: 18px;\"></div>"
                    : "<div style=\"background-color: " + color + "; width: 38px; height: 18px;\"></div>", divId);

            return result;
        }


        #region Plafond Progress

        private string BillingAmtInfo(string regNo)
        {
            decimal tpatient = 0;
            decimal tguarantor = 0;
            decimal totalPlafond = 0;

            var regnos = Helper.MergeBilling.GetMergeRegistration(regNo);

            var reg = new Registration();
            var rqr = new RegistrationQuery();
            rqr.Select(rqr.IsGlobalPlafond);
            rqr.Where(rqr.RegistrationNo == regNo);
            reg.Load(rqr);

            Helper.CostCalculation.GetBillingTotalStatus(regnos, 0, out tpatient, out tguarantor,
                new Guarantor(), reg.IsGlobalPlafond ?? false);

            return string.Format("{0:N2}", tpatient + tguarantor);
        }

        private string PlafondProgress(string regNo, string mode)
        {
            if (!AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsEmrListShowPlafondProgress))
                return String.Empty;

            switch (mode)
            {
                case "plafamt": // plafond amt
                    {
                        var reg = new Registration();
                        var rqr = new RegistrationQuery();
                        rqr.Select(rqr.SRRegistrationType, rqr.PlavonAmount, rqr.GuarantorID, rqr.ApproximatePlafondAmount,
                            rqr.SRBussinesMethod, rqr.IsGlobalPlafond);
                        rqr.Where(rqr.RegistrationNo == regNo);
                        reg.Load(rqr);

                        if (!GuarantorBpjs.Contains(reg.GuarantorID))
                            return "-";

                        decimal cobPlafond = AdditionalPlafond(regNo);
                        var totalPlafond = TotalPlafond(reg.SRRegistrationType, reg.PlavonAmount, reg.GuarantorID,
                            reg.ApproximatePlafondAmount, cobPlafond);
                        return string.Format("{0:N2}", totalPlafond);
                    }
                case "plafond": //progress bar
                case "plafondt": //text mode
                    {
                        decimal tpatient = 0;
                        decimal tguarantor = 0;
                        decimal totalPlafond = 0;

                        var usedInPercent =
                            PlafondValueUsedInPercent(regNo, ref tguarantor, ref tpatient, ref totalPlafond);
                        if (usedInPercent == 0) return string.Empty;

                        if (mode == "plafond")
                            return string.Format(
                                @"<div title='G: [{3:N2}] P: [{4:N2}] F: [{5:N2}]' style='background:black;width: 100%; padding: 1px'>
                            <div style='background:{0};color:Black;width: {1}%'>{2}</div>
                        </div>",
                                usedInPercent > 100 ? "red" : usedInPercent > 75 ? "yellow" : "green",
                                usedInPercent > 100 ? 100 : usedInPercent,
                                usedInPercent > 300 ? ">300%" : string.Format("{0:n2}%", usedInPercent),
                                tguarantor,
                                tpatient,
                                totalPlafond == 1 ? 0 : totalPlafond);

                        else if (mode == "plafondt")
                            return string.Format(@"<table style='font-weight:bold;'>
                            <tr>
                                <td style='width:70px;'>Plafond</td><td>&nbsp;:&nbsp;</td>
                                <td style='width:70px;text-align:right;'>{0:n}</td>
                            </tr>
                            <tr>
                                <td>Billing</td><td>&nbsp;:&nbsp;</td>
                                <td style='width:70px;text-align:right;'>{1:n}</td>
                            </tr>
                        </table>", totalPlafond == 1 ? 0 : totalPlafond, tguarantor + tpatient);

                        return string.Empty;

                    }
            }
            return string.Empty;
        }

        private decimal PlafondValueUsedInPercent(string regno, ref decimal tguarantor, ref decimal tpatient, ref decimal totalPlafond)
        {
            var reg = new Registration();
            var rqr = new RegistrationQuery();
            rqr.Select(rqr.SRRegistrationType, rqr.PlavonAmount, rqr.GuarantorID, rqr.ApproximatePlafondAmount, rqr.SRBussinesMethod, rqr.IsGlobalPlafond);
            rqr.Where(rqr.RegistrationNo == regno);
            reg.Load(rqr);

            if (!GuarantorBpjs.Contains(reg.GuarantorID))
                return 0;

            decimal cobPlafond = AdditionalPlafond(regno);
            totalPlafond = TotalPlafond(reg.SRRegistrationType, reg.PlavonAmount, reg.GuarantorID, reg.ApproximatePlafondAmount, cobPlafond);
            if (totalPlafond == 0)
                totalPlafond = 1;

            var regnos = Helper.MergeBilling.GetMergeRegistration(regno);

            var guarantor = new Guarantor();
            guarantor.LoadByPrimaryKey(reg.GuarantorID);

            Helper.CostCalculation.GetBillingTotalStatus(regnos, 0, out tpatient, out tguarantor,
                                                  guarantor, reg.IsGlobalPlafond ?? false);

            var totalRemain = tguarantor + tpatient;

            var plafonUsedPercent = (totalRemain / totalPlafond) * (decimal)100;
            return plafonUsedPercent;
        }

        private decimal AdditionalPlafond(string regno)
        {
            decimal cobPlafond = 0;
            var cob = new RegistrationGuarantorCollection();
            cob.Query.Where(cob.Query.RegistrationNo == regno);
            cob.LoadAll();
            cobPlafond = cob.Sum(c => (c.PlafondAmount ?? 0));
            return cobPlafond;
        }
        private decimal TotalPlafond(string srRegistrationType, decimal? plavonAmount, string guarantorID, decimal? approximatePlafondAmount, decimal additionalPlafond)
        {
            // approximatePlafondAmount adalah plafond yg diupdate dari inacbgs

            if (srRegistrationType == AppConstant.RegistrationType.InPatient)
            {
                decimal plafondAmt = plavonAmount ?? 0;
                if (GuarantorBpjs.Contains(guarantorID) && plafondAmt == 0)
                    plafondAmt = (decimal)(approximatePlafondAmount == null ? 0 : approximatePlafondAmount);

                return plafondAmt + additionalPlafond;
            }
            else
            {
                // Ambil dari parameter / Inacbg download amt
                var parNonInPatientBpjsPlafond = AppParameter.GetParameterValue(AppParameter.ParameterItem.NonInPatientBpjsPlafond).ToDecimal();
                return parNonInPatientBpjsPlafond > 0 ? parNonInPatientBpjsPlafond : (approximatePlafondAmount ?? 0);
            }
        }
        private static string[] _guarantorBpjs = null;
        private static string[] GuarantorBpjs
        {
            get
            {
                if (_guarantorBpjs != null) return _guarantorBpjs;
                var grr = new GuarantorBridgingCollection();
                grr.Query.es.Distinct = true;
                grr.Query.Where(grr.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(),
                                                            AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString(),
                                                            AppEnum.BridgingType.BPJS_PASIEN_UMUM.ToString()));
                if (grr.Query.Load()) _guarantorBpjs = grr.Select(g => g.GuarantorID).ToArray();
                else _guarantorBpjs = new string[] { string.Empty };

                return _guarantorBpjs;
            }
            set
            {
                _guarantorBpjs = value;
            }
        }
        #endregion

        #endregion

        #region EMR Detail DataSource
        private string FilterValue(List<GridFilterExpression> filterExpression, string filterName)
        {
            var foundFilter = filterExpression.FirstOrDefault(filter => filter.FieldName == filterName);
            if (foundFilter != null)
                return foundFilter.FieldValue;

            return string.Empty;
        }

        [WebMethod(EnableSession = true)]
        public Telerik.Web.UI.GridBindingData VitalSignLastValueAndCount(int startRowIndex, int maximumRows, List<GridSortExpression> sortExpression, List<GridFilterExpression> filterExpression)
        {
            var registrationNo = FilterValue(filterExpression, "RegistrationNo");
            if (string.IsNullOrWhiteSpace(registrationNo)) return null;

            var urlRoot = FilterValue(filterExpression, "UrlRoot");

            var vitalSignDateTime = DateTime.Now;
            var dateTime = FilterValue(filterExpression, "VitalSignDateTime");
            if (!string.IsNullOrWhiteSpace(dateTime))
                vitalSignDateTime = Convert.ToDateTime(dateTime);

            var mergeRegs = Registration.RelatedRegistrations(registrationNo);
            var dtb = VitalSign.VitalSignLastValue(registrationNo, mergeRegs, true, vitalSignDateTime);

            dtb.Columns.Add("UrlRoot", typeof(string));
            foreach (DataRow row in dtb.Rows)
            {
                row["UrlRoot"] = urlRoot;
            }

            var columns = dtb.Columns.Cast<System.Data.DataColumn>();
            var data = dtb.AsEnumerable()
                .Select(r => columns.Select(c => new { Column = c.ColumnName, Value = r[c] })
                    .ToDictionary(i => i.Column, i => i.Value != System.DBNull.Value ? i.Value : null))
                .Skip(startRowIndex).Take(maximumRows)
                .ToList<object>();


            return new Telerik.Web.UI.GridBindingData() { Data = data, Count = dtb.Rows.Count };
        }

        [WebMethod(EnableSession = true)]
        public Telerik.Web.UI.GridBindingData RegistrationInfoMedic(int startRowIndex, int maximumRows, List<GridSortExpression> sortExpression, List<GridFilterExpression> filterExpression)
        {
            //(string registrationType, string registrationNo, List<string> registrationNoList, string patientID, string filterEntry)

            var registrationType = FilterValue(filterExpression, "registrationType");
            var registrationNo = FilterValue(filterExpression, "registrationNo");
            if (string.IsNullOrWhiteSpace(registrationNo)) return null;

            var patientID = FilterValue(filterExpression, "patientID");
            var filterEntry = FilterValue(filterExpression, "filterEntry");
            var registrationNoList = Registration.RelatedRegistrations(registrationNo);

            // RegistrationInfoMedic
            var que = new RegistrationInfoMedicQuery("a");
            var pa = new PatientAssessmentQuery("pa");
            que.LeftJoin(pa).On(que.RegistrationInfoMedicID == pa.RegistrationInfoMedicID);

            var stdi = new AppStandardReferenceItemQuery("stdi");
            que.LeftJoin(stdi).On(pa.SRAssessmentType == stdi.ItemID && stdi.StandardReferenceID == "AssessmentType");

            var reg = new RegistrationQuery("x");
            que.InnerJoin(reg).On(que.RegistrationNo == reg.RegistrationNo);

            if (registrationType == AppConstant.RegistrationType.InPatient)
            {

                if (registrationNoList.Count > 1)
                    que.Where(que.RegistrationNo.In(registrationNoList));
                else
                    que.Where(que.RegistrationNo == registrationNoList[0]);
            }
            else
            {

                //List<string> patientRelateds = Patient.PatientRelateds(patientID);
                //if (patientRelateds.Count == 1)
                //    que.Where(reg.PatientID == patientID);
                //else
                //    que.Where(reg.PatientID.In(patientRelateds));

                // Non Inpatient ambil 5 registrasi terakhir (Handono 2022-09)
                var regCount = AppParameter.GetParameterValue(AppParameter.ParameterItem.EmrHistoryRegistrationCount).ToInt();
                var lastRegNos = Patient.Last.RegistrationNos(patientID, regCount, registrationNo);
                if (lastRegNos.Count == 1)
                    que.Where(que.RegistrationNo == lastRegNos[0]);
                else
                    que.Where(que.RegistrationNo.In(lastRegNos));
            }

            if (!string.IsNullOrWhiteSpace(filterEntry))
                que.Where(que.SRUserType == filterEntry);

            que.OrderBy(que.RegistrationInfoMedicID.Descending);
            que.Select(que.RegistrationInfoMedicID, que.RegistrationNo, que.ParamedicID, que.SRMedicalNotesInputType, que.DateTimeInfo, que.Info1, que.Info2, que.Info3, que.Info4, que.Info5,
                que.IsApproved.Coalesce("CONVERT(bit,0)").As("IsApproved"), que.ApprovedDatetime, que.ApprovedByUserID, que.CreatedDateTime, que.CreatedByUserID, que.IsDeleted.Coalesce("CONVERT(bit,0)").As("IsDeleted"), que.ServiceUnitID,
                que.AttendingNotes, pa.SRAssessmentType, pa.IsInitialAssessment, "<CAST(0 as bit) as IsFromAskep>", stdi.ItemName.Coalesce("''").As("AssessmentTypeName"),
                que.SRUserType, que.PpaInstruction, "<'' as ReferenceToPhrNo>", que.IsCreatedByUserDpjp, que.PrescriptionCurrentDay,
                "<'' as SubmitBy>", que.ReceiveBy, "<'' as TemplateID>", que.DpjpNotes,
                "<'' as SRNursingDiagnosaLevel>", "<'' as SRNursingCarePlanning>", que.ReferenceNo, reg.FromRegistrationNo);


            var dtbRim = que.LoadDataTable();

            // Nursing Notes
            var nsdt = new NursingDiagnosaTransDTQuery("a");
            var nshd = new NursingTransHDQuery("b");
            nsdt.InnerJoin(nshd).On(nsdt.TransactionNo == nshd.TransactionNo);

            var regqr = new RegistrationQuery("x");
            nsdt.InnerJoin(regqr).On(nshd.RegistrationNo == regqr.RegistrationNo);

            //SubQuery ambil service unit saat entry
            //var patTransHist = new PatientTransferHistoryQuery("pth");
            //patTransHist.es.Top = 1;
            //patTransHist.Where(patTransHist.RegistrationNo == nshd.RegistrationNo, patTransHist.DateOfEntry <= nsdt.CreateDateTime, patTransHist.TimeOfEntry <= nsdt.CreateDateTime);
            //patTransHist.Select(patTransHist.ServiceUnitID);


            if (registrationType == AppConstant.RegistrationType.InPatient)
            {
                if (registrationNoList.Count > 1)
                    nsdt.Where(nshd.RegistrationNo.In(registrationNoList));
                else
                    nsdt.Where(nshd.RegistrationNo == registrationNoList[0]);

            }
            else
            {
                //List<string> patientRelateds = Patient.PatientRelateds(patientID);

                //if (patientRelateds.Count == 1)
                //    nsdt.Where(regqr.PatientID == patientID);
                //else
                //    nsdt.Where(regqr.PatientID.In(patientRelateds));

                // Non Inpatient ambil 5 registrasi terakhir (Handono 2022-09)
                var regCount = AppParameter.GetParameterValue(AppParameter.ParameterItem.EmrHistoryRegistrationCount).ToInt();
                var lastRegNos = Patient.Last.RegistrationNos(patientID, regCount, registrationNo);
                if (lastRegNos.Count == 1)
                    nsdt.Where(nshd.RegistrationNo == lastRegNos[0]);
                else
                    nsdt.Where(nshd.RegistrationNo.In(lastRegNos));
            }

            if (!string.IsNullOrWhiteSpace(filterEntry))
                nsdt.Where(nsdt.SRUserType == filterEntry);

            nsdt.Where(nshd.Or(nsdt.SRNursingDiagnosaLevel == "31", nsdt.SRNursingDiagnosaLevel == "40"))
                .Select(
                    nsdt.ID.Cast(esCastType.String).As("RegistrationInfoMedicID"),
                    nshd.RegistrationNo,
                    nsdt.ParamedicID,
                    "<CASE WHEN NursingDiagnosaName='S B A R' THEN 'SBAR' WHEN NursingDiagnosaName='ADIME' THEN 'ADIME' WHEN NursingDiagnosaName = 'S O A P' OR SRNursingDiagnosaLevel = '40' THEN 'SOAP' WHEN NursingDiagnosaName = 'Handover Patient' THEN NursingDiagnosaName  ELSE 'Notes' END as SRMedicalNotesInputType>",
                    nsdt.ExecuteDateTime.As("DateTimeInfo"),
                    "<CASE WHEN NursingDiagnosaName='S B A R' OR NursingDiagnosaName = 'ADIME' OR NursingDiagnosaName = 'S O A P' OR NursingDiagnosaName = 'Handover Patient' OR SRNursingDiagnosaLevel = '40' THEN S ELSE NursingDiagnosaName END as Info1>",
                    "<CASE WHEN NursingDiagnosaName='S B A R' OR NursingDiagnosaName = 'ADIME' OR NursingDiagnosaName = 'S O A P' OR NursingDiagnosaName = 'Handover Patient' OR SRNursingDiagnosaLevel = '40' THEN O ELSE Respond END as Info2>",
                    nsdt.A.As("Info3"),
                    nsdt.P.As("Info4"),
                    nsdt.Info5,
                    nsdt.IsApproved.Coalesce("CONVERT(bit,0)").As("IsApproved"),
                    nsdt.ApprovedDatetime,
                    nsdt.ApprovedByUserID,
                    nsdt.CreateDateTime.As("CreatedDateTime"),
                    nsdt.CreateByUserID.As("CreatedByUserID"),
                    nsdt.IsDeleted.Coalesce("CONVERT(bit,0)").As("IsDeleted"),
                    @"<COALESCE((
           SELECT TOP 1 pth.[ServiceUnitID]
           FROM   [PatientTransferHistory] pth
           WHERE  pth.[RegistrationNo] = b.[RegistrationNo]
                  AND CAST(pth.[DateOfEntry] AS DATETIME) + CAST(pth.[TimeOfEntry] AS DATETIME) <= a.[ExecuteDateTime]
ORDER BY pth.[DateOfEntry] DESC,pth.[TimeOfEntry] DESC),x.ServiceUnitID) AS ServiceUnitID>",
                    //patTransHist.As("ServiceUnitID"),
                    //regqr.ServiceUnitID,
                    "<'' as AttendingNotes>",
                    "<'NurseNotes' as SRAssessmentType>",
                    "<CAST(0 as bit) as IsInitialAssessment>",
                    "<CAST(1 as bit) as IsFromAskep>",
                    "<'' as AssessmentTypeName>",
                    nsdt.SRUserType, nsdt.PpaInstruction, nsdt.ReferenceToPhrNo, "<CAST(0 as bit) as IsCreatedByUserDpjp>",
                    nsdt.PrescriptionCurrentDay, nsdt.SubmitBy, nsdt.ReceiveBy, "<CAST(ISNULL(a.TemplateID,0) as varchar(10)) as TemplateID>", nsdt.DpjpNotes,
                    nsdt.SRNursingDiagnosaLevel, nsdt.SRNursingCarePlanning, "<'' as ReferenceNo>", regqr.FromRegistrationNo
                );

            var dtbAskep = nsdt.LoadDataTable();
            // get data intervensi yang diteruskan
            foreach (var r in dtbAskep.AsEnumerable()
                .Where(dr => dr.Field<string>("SRNursingDiagnosaLevel") == "40"/*evaluasi*/
                    & dr.Field<string>("SRNursingCarePlanning") != "01"/*tidak distop*/))
            {
                var nicEval = NursingDiagnosaTransDT.DetailEvaluationByEvaluationIdHtml(Convert.ToInt64(r["RegistrationInfoMedicID"]));
                nicEval = nicEval.Replace("[BASEURL]", Helper.UrlRoot());
                // if adime
                if (r["SRMedicalNotesInputType"].ToString() == "ADIME")
                {
                    r["Info3"] = r["Info3"].ToString() + nicEval;
                }
                else
                {
                    r["Info4"] = r["Info4"].ToString() + nicEval;
                }

            }

            dtbRim.Merge(dtbAskep);

            var dtb = new DataTable();
            dtb.Columns.Add(new DataColumn("RegistrationInfoMedicID", typeof(string)));
            dtb.Columns.Add(new DataColumn("SRMedicalNotesInputType", typeof(string)));
            dtb.Columns.Add(new DataColumn("SOAP", typeof(string)));
            dtb.Columns.Add(new DataColumn("Footer", typeof(string)));
            dtb.Columns.Add(new DataColumn("DateTimeInfo", typeof(DateTime)));
            dtb.Columns.Add(new DataColumn("DateTimeInfoStr", typeof(string)));
            dtb.Columns.Add(new DataColumn("CreatedByUserID", typeof(string)));
            dtb.Columns.Add(new DataColumn("IsDeleted", typeof(bool)));
            dtb.Columns.Add(new DataColumn("registrationNo", typeof(string)));
            dtb.Columns.Add(new DataColumn("SRAssessmentType", typeof(string)));
            dtb.Columns.Add(new DataColumn("AssessmentTypeName", typeof(string)));
            dtb.Columns.Add(new DataColumn("ParamedicID", typeof(string)));
            dtb.Columns.Add(new DataColumn("ParamedicName", typeof(string)));
            dtb.Columns.Add(new DataColumn("IsInitialAssessment", typeof(bool)));
            dtb.Columns.Add(new DataColumn("ServiceUnitID", typeof(string)));
            dtb.Columns.Add(new DataColumn("ServiceUnitName", typeof(string)));
            dtb.Columns.Add(new DataColumn("IsApproved", typeof(bool)));
            dtb.Columns.Add(new DataColumn("IsFromAskep", typeof(bool)));
            dtb.Columns.Add(new DataColumn("IsCreatedByUserDpjp", typeof(bool)));
            dtb.Columns.Add(new DataColumn("ApprovedDateTime", typeof(DateTime)));
            dtb.Columns.Add(new DataColumn("SRUserType", typeof(string)));
            dtb.Columns.Add(new DataColumn("PpaInstruction", typeof(string)));
            dtb.Columns.Add(new DataColumn("DpJpNotes", typeof(string)));
            dtb.Columns.Add(new DataColumn("SRNursingDiagnosaLevel", typeof(string)));
            dtb.Columns.Add(new DataColumn("ReferenceNo", typeof(string)));
            dtb.Columns.Add(new DataColumn("FromRegistrationNo", typeof(string)));
            dtb.Columns.Add(new DataColumn("CreatedByUserName", typeof(string)));

            var dv = dtbRim.DefaultView;
            dv.Sort = "DateTimeInfo DESC";
            var sortedDt = dv.ToTable();

            var serviceUnitID = string.Empty;
            var serviceUnitName = string.Empty;

            var suColl = new ServiceUnitCollection(); // Untuk pengisian ServiceUnitName

            // TuneUp pengambilan user name (Handono 230326)
            var dvCreateBy = new DataView(dtbRim).ToTable(true, new[] { "CreatedByUserID" });
            var listCreatedBy = new List<string>();
            foreach (DataRow dvr in dvCreateBy.Rows)
            {
                listCreatedBy.Add(dvr[0].ToString());
            }

            DataTable dtbCreateBy;
            if (listCreatedBy.Count > 0)
            {
                var usr = new AppUserQuery("usr");
                usr.Select(usr.UserID, usr.UserName);
                usr.Where(usr.UserID.In(listCreatedBy));
                dtbCreateBy = usr.LoadDataTable();
            }
            else
            {
                dtbCreateBy = new DataTable();
                dtbCreateBy.Columns.Add("UserID", typeof(string));
                dtbCreateBy.Columns.Add("UserName", typeof(string));
            }
            dtbCreateBy.PrimaryKey = new DataColumn[] { dtbCreateBy.Columns["UserID"] };

            // TuneUp pengambilan paramedic name (Handono 230326)
            var dvParamedic = new DataView(dtbRim).ToTable(true, new[] { "ParamedicID" });
            var listParamedic = new List<string>();
            foreach (DataRow dvr in dvParamedic.Rows)
            {
                listParamedic.Add(dvr[0].ToString());
            }

            DataTable dtbParamedic;
            if (listCreatedBy.Count > 0)
            {
                var par = new ParamedicQuery("par");
                par.Select(par.ParamedicID, par.ParamedicName);
                par.Where(par.ParamedicID.In(listParamedic));
                dtbParamedic = par.LoadDataTable();
            }
            else
            {
                dtbParamedic = new DataTable();
                dtbParamedic.Columns.Add("ParamedicID", typeof(string));
                dtbParamedic.Columns.Add("ParamedicName", typeof(string));
            }
            dtbParamedic.PrimaryKey = new DataColumn[] { dtbParamedic.Columns["ParamedicID"] };
            // -------------

            foreach (DataRow rim in sortedDt.Rows)
            {
                var newRow = dtb.NewRow();
                newRow["RegistrationInfoMedicID"] = rim["RegistrationInfoMedicID"];
                newRow["SRMedicalNotesInputType"] = rim["SRMedicalNotesInputType"];
                newRow["DateTimeInfo"] = rim["DateTimeInfo"];
                newRow["CreatedByUserID"] = rim["CreatedByUserID"];
                newRow["registrationNo"] = rim["registrationNo"];
                newRow["IsDeleted"] = rim["IsDeleted"] == DBNull.Value ? false : rim["IsDeleted"];
                newRow["SRAssessmentType"] = rim["SRAssessmentType"];
                newRow["AssessmentTypeName"] = rim["AssessmentTypeName"];
                newRow["IsInitialAssessment"] = rim["IsInitialAssessment"];
                newRow["ParamedicID"] = rim["ParamedicID"];
                newRow["ServiceUnitID"] = rim["serviceUnitID"];
                newRow["IsApproved"] = rim["IsApproved"];
                newRow["IsFromAskep"] = rim["IsFromAskep"];
                newRow["IsCreatedByUserDpjp"] = rim["IsCreatedByUserDpjp"];
                newRow["ApprovedDateTime"] = rim["ApprovedDateTime"];
                newRow["SRUserType"] = rim["SRUserType"];
                newRow["PpaInstruction"] = rim["PpaInstruction"];
                newRow["DpJpNotes"] = rim["DpJpNotes"];
                newRow["SRNursingDiagnosaLevel"] = rim["SRNursingDiagnosaLevel"];
                newRow["ReferenceNo"] = rim["ReferenceNo"];
                newRow["FromRegistrationNo"] = rim["FromRegistrationNo"];

                if (rim["serviceUnitID"] != DBNull.Value && !string.IsNullOrEmpty(rim["ServiceUnitID"].ToString()))
                {
                    if (serviceUnitID != (string)rim["ServiceUnitID"])
                    {
                        serviceUnitID = rim["ServiceUnitID"].ToString();
                        var su = suColl.Where(s => s.ServiceUnitID == serviceUnitID).FirstOrDefault();
                        if (su == null)
                        {
                            su = new ServiceUnit();
                            su.LoadByPrimaryKey(serviceUnitID);
                            su.Query.Select(su.Query.ServiceUnitID, su.Query.ServiceUnitName);
                            suColl.AttachEntity(su);
                        }
                        serviceUnitName = su.ServiceUnitName;
                    }
                }
                else
                {
                    serviceUnitName = string.Empty;
                }

                newRow["ServiceUnitName"] = serviceUnitName;

                // TuneUp (Handono 230326)
                if (rim["ParamedicID"] != DBNull.Value && !string.IsNullOrEmpty(rim["ParamedicID"].ToString()))
                {
                    //newRow["ParamedicName"] = Paramedic.GetParamedicName(rim["ParamedicID"].ToString());

                    // TuneUp (Handono 230326)
                    var rcb = dtbParamedic.Rows.Find(rim["ParamedicID"]);
                    if (rcb != null && rcb["ParamedicName"] != DBNull.Value)
                        newRow["ParamedicName"] = rcb["ParamedicName"].ToString();
                }


                if (rim["CreatedByUserID"] != DBNull.Value && !string.IsNullOrEmpty(rim["CreatedByUserID"].ToString()))
                {
                    //newRow["CreatedByUserName"] = AppUser.GetUserName(rim["CreatedByUserID"].ToString());

                    // TuneUp (Handono 230326)
                    var rcb = dtbCreateBy.Rows.Find(rim["CreatedByUserID"]);
                    if (rcb != null && rcb["UserName"] != DBNull.Value)
                        newRow["CreatedByUserName"] = rcb["UserName"].ToString();
                }



                newRow["DateTimeInfoStr"] = string.Format("{0} {1}", Convert.ToDateTime(rim["DateTimeInfo"]).ToString(AppConstant.DisplayFormat.Date), Convert.ToDateTime(rim["DateTimeInfo"]).ToString("HH:mm"));

                var sbNote = new StringBuilder();
                sbNote = new StringBuilder();

                if (rim["IsDeleted"].ToBoolean() == true)
                    sbNote.AppendLine("<table style=\"width:100%;text-decoration:line-through;\">");
                else
                    sbNote.AppendLine("<table style=\"width:100%\">");

                var medicalNotesInputType = rim["SRMedicalNotesInputType"].ToString();
                switch (medicalNotesInputType)
                {
                    case "SBAR":
                    case "SOAP":
                    case "ADIME":
                    case "Handover Patient":
                        {
                            //var col1Width = medicalNotesInputType == "Handover Patient" ? 50 : 10;
                            var col1Width = (string.IsNullOrEmpty(rim["SubmitBy"].ToString()) && string.IsNullOrEmpty(rim["ReceiveBy"].ToString())) ? 10 : 50;
                            var label = medicalNotesInputType;

                            if (medicalNotesInputType == "Handover Patient")
                            {
                                label = "SOAP";
                            }

                            var info1 = ReplaceWitBreakLineHTML(rim, "Info1");
                            sbNote.AppendFormat(
                                "<tr><td class='label' valign='top' style='font-weight: bold;width:{2}px; padding-left:2px'>{0}:</td><td valign='top'>{1}</td></tr>",
                                label.Substring(0, 1), info1, col1Width);
                            sbNote.AppendLine();

                            var info2 = ReplaceWitBreakLineHTML(rim, "Info2");
                            var isFromAssessment = rim["SRAssessmentType"] != DBNull.Value &&
                                                   !string.IsNullOrEmpty(rim["SRAssessmentType"].ToString()) &&
                                                   !"NurseNotes".Equals(rim["SRAssessmentType"]);
                            if (medicalNotesInputType == "SOAP")
                            {
                                if (isFromAssessment)
                                {
                                    var rimid = rim["RegistrationInfoMedicID"];
                                    var qr = new RegistrationInfoMedicBodyDiagramQuery("a");
                                    qr.Select(qr.RegistrationInfoMedicID);
                                    qr.es.Top = 1;
                                    qr.es.WithNoLock = true;

                                    qr.Where(qr.RegistrationInfoMedicID == rimid, qr.IsDeleted == false);

                                    var ent = new RegistrationInfoMedicBodyDiagram();
                                    if (ent.Load(qr))
                                    {
                                        // Tambah info status Localist Image
                                        info2 = string.Concat(info2, string.Format("<a href='#' style=\"cursor: pointer;\" onclick='javascript:openLocalist(\"{0}\"); event.cancelBubble = true;if(event.stopPropagation) event.stopPropagation();'><img src='{1}/Images/Toolbar/anatomy16.png'/></a>", rimid, Helper.UrlRoot()));
                                    }
                                }
                            }

                            sbNote.AppendFormat(
                                "<tr><td class='label' valign='top' style='font-weight: bold;width:{2}px; padding-left:2px'>{0}:</td><td valign='top'>{1}</td></tr>",
                                label.Substring(1, 1), info2, col1Width);
                            sbNote.AppendLine();

                            var info3 = ReplaceWitBreakLineHTML(rim, "Info3");



                            sbNote.AppendFormat(
                                "<tr><td class='label' valign='top' style='font-weight: bold;width:{2}px; padding-left:2px'>{0}:</td><td valign='top'>{1}</td></tr>",
                                label.Substring(2, 1), info3, col1Width);

                            sbNote.AppendLine();

                            // Planning
                            string planning = FormatToHtml(rim["Info4"]);

                            if (medicalNotesInputType == "SOAP")
                            {
                                if (isFromAssessment)
                                {
                                    // Dari asesmen tambah hist resepnya di Planning
                                    if (rim["PrescriptionCurrentDay"] != DBNull.Value ||
                                        !string.IsNullOrEmpty(rim["PrescriptionCurrentDay"].ToString()))
                                        planning = string.Format("{0}<br/><br/>{1}", FormatToHtml(rim["Info4"]),
                                            FormatToHtml(rim["PrescriptionCurrentDay"]));
                                    else
                                        planning = FormatToHtml(rim["Info4"]);


                                }
                            }

                            sbNote.AppendFormat(
                                "<tr><td class='label' valign='top' style='font-weight: bold; width:{2}px;padding-left:2px'>{0}:</td><td valign='top'>{1}</td></tr>",
                                label.Substring(3, 1), planning, col1Width);

                            // PPA Instruction, hanya dimunculkan jika Soap InPatient yg bukan dari asesmen
                            if (medicalNotesInputType == "SOAP" && !isFromAssessment)
                            {
                                var ppaInstruction = FormatToHtml(rim["PpaInstruction"]);
                                // Tambahkan histori prescription jika bukan dari Nursing Notes
                                if (!"NurseNotes".Equals(rim["SRAssessmentType"]))
                                    if (!isFromAssessment &&
                                        (rim["PrescriptionCurrentDay"] != DBNull.Value ||
                                         !string.IsNullOrEmpty(rim["PrescriptionCurrentDay"].ToString())))
                                        ppaInstruction = string.Format("{0}<br/><br/>{1}",
                                            FormatToHtml(rim["PpaInstruction"]),
                                            FormatToHtml(rim["PrescriptionCurrentDay"]));

                                if (!string.IsNullOrEmpty(ppaInstruction))
                                {
                                    sbNote.AppendFormat(
                                        "<tr><td class='label' valign='top' style='font-weight: bold;width:{1}px; padding-left:2px'>I:</td><td valign='top'>{0}</td></tr>",
                                        ppaInstruction, col1Width);
                                }

                                var info = ReplaceWitBreakLineHTML(rim, "Info5");
                                if (!string.IsNullOrEmpty(info))
                                {
                                    sbNote.AppendFormat(
                                        "<tr><td class='label' valign='top' style='font-weight: bold;width:{2}px;padding-left:2px'>{0}:</td><td valign='top'>{1}</td></tr>",
                                        "E", info, col1Width);
                                    sbNote.AppendLine();
                                }

                                info = ReplaceWitBreakLineHTML(rim, "SubmitBy");
                                if (!string.IsNullOrEmpty(info))
                                {
                                    sbNote.AppendFormat(
                                        "<tr><td class='label' valign='top' style='font-weight: bold;width:{2}px; padding-left:2px'>{0}:</td><td valign='top'>{1}</td></tr>",
                                        "Submit By", info, col1Width);
                                    sbNote.AppendLine();
                                }

                                info = ReplaceWitBreakLineHTML(rim, "ReceiveBy");
                                if (!string.IsNullOrEmpty(info))
                                {
                                    sbNote.AppendFormat(
                                        "<tr><td class='label' valign='top' style='font-weight: bold;width:{2}px; padding-left:2px'>{0}:</td><td valign='top'>{1}</td></tr>",
                                        "Receive By", info, col1Width);
                                    sbNote.AppendLine();
                                }
                            }
                            else if (medicalNotesInputType == "Handover Patient")
                            {
                                var info = ReplaceWitBreakLineHTML(rim, "PpaInstruction");
                                sbNote.AppendFormat(
                                    "<tr><td class='label' valign='top' style='font-weight: bold;width:{1}px; padding-left:2px'>I:</td><td valign='top'>{0}</td></tr>",
                                     info, col1Width);
                                sbNote.AppendLine();

                                info = ReplaceWitBreakLineHTML(rim, "SubmitBy");
                                sbNote.AppendFormat(
                                    "<tr><td class='label' valign='top' style='font-weight: bold;width:{2}px; padding-left:2px'>{0}:</td><td valign='top'>{1}</td></tr>",
                                    "Submit By", info, col1Width);
                                sbNote.AppendLine();

                                info = ReplaceWitBreakLineHTML(rim, "ReceiveBy");
                                sbNote.AppendFormat(
                                    "<tr><td class='label' valign='top' style='font-weight: bold;width:{2}px; padding-left:2px'>{0}:</td><td valign='top'>{1}</td></tr>",
                                    "Receive By", info, col1Width);
                                sbNote.AppendLine();
                            }
                            else if (medicalNotesInputType == "SBAR")
                            {
                                var info = ReplaceWitBreakLineHTML(rim, "PpaInstruction");
                                sbNote.AppendFormat(
                                    "<tr><td class='label' valign='top' style='font-weight: bold;width:{1}px; padding-left:2px'>I:</td><td valign='top'>{0}</td></tr>",
                                     info, col1Width);
                                sbNote.AppendLine();

                                info = ReplaceWitBreakLineHTML(rim, "Info5");
                                sbNote.AppendFormat("<tr><td class='label' valign='top' style='font-weight: bold;width:{1}px; padding-left:2px'>BAK:</td><td valign='top'>{0}</td></tr>", info, col1Width);
                                sbNote.AppendLine();

                                info = ReplaceWitBreakLineHTML(rim, "ReceiveBy");
                                sbNote.AppendFormat("<tr><td class='label' valign='top' style='font-weight: bold;width:{1}px; padding-left:2px'>ReceiveBy:</td><td valign='top'>{0}</td></tr>", info, col1Width);
                                sbNote.AppendLine();
                            }
                            else if (medicalNotesInputType == "ADIME")
                            {
                                var info = ReplaceWitBreakLineHTML(rim, "Info5");
                                sbNote.AppendFormat(
                                    "<tr><td class='label' valign='top' style='font-weight: bold;width:{2}px;padding-left:2px'>{0}:</td><td valign='top'>{1}</td></tr>",
                                    "E", info, col1Width);
                                sbNote.AppendLine();
                            }

                            break;
                        }
                    case "REF":
                    // Refer / Consul to Klinik
                    case "CON":
                        {
                            sbNote.AppendFormat(
                                "<tr><td colspan='2' style='font-weight: bold;padding-left:2px;'>{1} to : {0}</td></tr>",
                                rim["Info1"], medicalNotesInputType == "REF" ? "Refer" : "Consult");

                            sbNote.AppendFormat("<tr><td colspan='2' style='padding-left:2px;'>Notes :</td></tr>");
                            sbNote.AppendFormat("<tr><td width:10px;'>&nbsp;</td><td>{0}</td></tr>",
                                FormatToHtml(rim["Info2"]));
                            sbNote.AppendLine();

                            sbNote.AppendFormat(
                                "<tr><td colspan='2' style='padding-left:2px;'>Action / Examination / Treatment :</td></tr>");
                            sbNote.AppendFormat("<tr><td  width:10px;'>&nbsp;</td><td>{0}</td></tr>",
                                FormatToHtml(rim["Info3"]));
                            sbNote.AppendLine();

                            var answerMenu = string.Empty;
                            var csl = new ParamedicConsultRefer();
                            if (csl.LoadByPrimaryKey(rim["ReferenceNo"].ToString()) && csl.ToParamedicID == AppSession.UserLogin.ParamedicID)
                                answerMenu = string.Format(" <a href=\"javascript:void(0);\" onclick=\"javascript:entryParamedicConsultAnswer('{0}','{1}')\"><img src='{2}/Images/Toolbar/edit16.png'/></a>",
                                    rim["ReferenceNo"], rim["RegistrationNo"], Helper.UrlRoot());

                            sbNote.AppendFormat("<tr><td colspan='2' style='padding-left:2px;'>{0}Answer :</td></tr>", answerMenu);
                            sbNote.AppendFormat("<tr><td width:10px'>&nbsp;</td><td>{0}</td></tr>",
                                FormatToHtml(csl.Answer));
                            sbNote.AppendLine();
                            break;
                        }
                    default:
                        {
                            var info1 = Regex.Replace(rim["Info1"] == DBNull.Value ? String.Empty : rim["Info1"].ToString(),
                                @"\r\n?|\n", "<br />");
                            sbNote.AppendFormat("<tr><td colspan='2' style='padding-left:2px;'>{0}</td></tr>", info1);

                            if (!string.IsNullOrEmpty(rim["ReferenceToPhrNo"].ToString()))
                            {
                                var phrlColl = new PatientHealthRecordLineCollection();
                                if (phrlColl.LoadByTransactionNoRegNoOfTemplateEntry(rim["ReferenceToPhrNo"].ToString(), rim["RegistrationNo"].ToString()))
                                {
                                    rim["Info2"] = rim["Info2"] + " " +
                                                   RADT.Emr.NursingImplementationEntry.parsePhrlRespond(phrlColl);
                                }
                            }

                            var info2 = ReplaceWitBreakLineHTML(rim, "Info2");
                            if (!string.IsNullOrEmpty(info2))
                            {
                                sbNote.AppendFormat(
                                    "<tr><td class='label' valign='top' style='font-weight: bold; width:10px;padding-left:2px'>{0}:</td><td valign='top'>{1}{2}</td></tr>",
                                    "Respond", info2,
                                    (rim["TemplateID"].ToString() == string.Empty || rim["TemplateID"].ToString() == "0")
                                        ? ""
                                        : string.Format(" <a href=\"javascript:void(0);\" onclick=\"javascript:OpenTableRespond('{0}')\"><img src='{1}/Images/Toolbar/views16.png'/></a>",
                                          rim["TemplateID"].ToString(), Helper.UrlRoot()));
                                sbNote.AppendLine();
                            }

                            break;

                        }
                }

                if (rim["AttendingNotes"] != DBNull.Value && rim["AttendingNotes"].ToString() != string.Empty)
                {
                    sbNote.AppendFormat(
                        "<tr><td class='label' valign='top' style='font-weight: bold; width:10px;padding-left:2px'>N:</td><td valign='top'>{0}</td></tr>",
                        rim["AttendingNotes"]);
                }


                if (rim["DpjpNotes"] != DBNull.Value && !string.IsNullOrWhiteSpace(rim["DpjpNotes"].ToString()))
                {
                    sbNote.AppendFormat("<tr><td colspan='2' style='horiz-align: right;'>* DPJP Note: {0}</td></tr>", rim["DpjpNotes"]);
                }

                if (rim["ApprovedDateTime"] != DBNull.Value)
                {
                    var intNotesVerifLabel = AppSession.Parameter.IntNotesVerifLabel;
                    var IntNotesVerifLabelReview = AppSession.Parameter.IntNotesVerifLabelReview;
                    var label = rim["paramedicID"] == DBNull.Value ? IntNotesVerifLabelReview : intNotesVerifLabel;
                    var approvedDate = string.Format("<img src='{1}/Images/Verified16.png'/>&nbsp;{4}: {0} By: {2} ({3})",
                        Convert.ToDateTime(rim["ApprovedDateTime"])
                            .ToString(AppConstant.DisplayFormat.LongDatePattern), Helper.UrlRoot(),
                        AppUser.GetParamedicName(rim["ApprovedByUserID"].ToString()), rim["ApprovedByUserID"], label);

                    sbNote.AppendFormat(
                        "<tr><td colspan='2' style='horiz-align: right;'>{0}</td></tr>", approvedDate);
                }

                //sbNote.AppendLine("<tr><td colspan='2' >&nbsp;</td></tr>"); <-- line ini buat apa ya? di remarks dl ya supaya tidak ada baris kosong di tampilan assessment
                sbNote.AppendLine("</table>");

                newRow["SOAP"] = sbNote.ToString();


                // Add Row
                dtb.Rows.Add(newRow);
            }

            var columns = dtb.Columns.Cast<System.Data.DataColumn>();
            var data = dtb.AsEnumerable()
                .Select(r => columns.Select(c => new { Column = c.ColumnName, Value = r[c] })
                    .ToDictionary(i => i.Column, i => i.Value != System.DBNull.Value ? i.Value : null))
                .Skip(startRowIndex).Take(maximumRows)
                .ToList<object>();


            return new Telerik.Web.UI.GridBindingData() { Data = data, Count = dtb.Rows.Count };
        }
        private static string FormatToHtml(object value)
        {
            return Regex.Replace(value == null || value == DBNull.Value ? String.Empty : value.ToString(), @"\r\n?|\n", "<br />");
        }
        private static string ReplaceWitBreakLineHTML(DataRow row, string fieldName)
        {
            if (row[fieldName] == DBNull.Value) return string.Empty;
            return ReplaceWitBreakLineHTML(row[fieldName].ToString());
        }
        private static string ReplaceWitBreakLineHTML(string text)
        {
            return Regex.Replace(text, @"\r\n?|\n", "<br />");
        }


        //[WebMethod(EnableSession = true)]
        //public Telerik.Web.UI.GridBindingData GetDataAndCount(int startRowIndex, int maximumRows, List<GridSortExpression> sortExpression, List<GridFilterExpression> filterExpression)
        //{
        //    var table = (new AppUserQuery("au")).LoadDataTable();

        //    var columns = table.Columns.Cast<System.Data.DataColumn>();

        //    var data = table.AsEnumerable()
        //        .Select(r => columns.Select(c => new { Column = c.ColumnName, Value = r[c] })
        //            .ToDictionary(i => i.Column, i => i.Value != System.DBNull.Value ? i.Value : null))
        //        .Skip(startRowIndex).Take(maximumRows)
        //        .ToList<object>();

        //    return new Telerik.Web.UI.GridBindingData() { Data = data, Count = table.Rows.Count };
        //}

        //public System.Data.DataTable GetDataTable(string query)
        //{
        //    var connString = System.Configuration.ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ConnectionString;
        //    var conn = new System.Data.SqlClient.SqlConnection(connString);
        //    var adapter = new System.Data.SqlClient.SqlDataAdapter();
        //    adapter.SelectCommand = new System.Data.SqlClient.SqlCommand(query, conn);

        //    var table = new System.Data.DataTable();

        //    conn.Open();
        //    try
        //    {
        //        adapter.Fill(table);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }

        //    return table;
        //}
        #endregion


        [WebMethod(EnableSession = true)]
        public string AddProgressNotesAbortStatus(string registrationNo)
        {
            var pass = new PatientAssessment();
            var passq = new PatientAssessmentQuery();
            passq.Select(passq.RegistrationNo);
            passq.Where(passq.RegistrationNo == registrationNo,
                passq.Or(passq.IsDeleted.IsNull(), passq.IsDeleted == false));
            passq.es.Top = 1;

            if (!pass.Load(passq))
                return "Create Progress Notes / PPA Notes not allowed before assessment, please create assessment first";
            return string.Empty;
        }

        #region menu PHR
        private DataTable QuestionFormDatatable(string searchText, string serviceUnitID, string registrationNo, bool isFirstRegInServiceUnit, string userType)
        {
            var query = new QuestionFormQuery("a");
            var suQr = new QuestionFormInServiceUnitQuery("s");

            query.InnerJoin(suQr)
                .On(query.QuestionFormID == suQr.QuestionFormID && suQr.ServiceUnitID == serviceUnitID);
            if (isFirstRegInServiceUnit)
                query.Where(query.IsActive == true && query.IsInitialAssessment == true);
            else
                query.Where(query.IsActive == true && query.IsContinuedAssessment == true);

            // Berdasarkan Form Type
            query.Where(query.Or(query.SRQuestionFormType.IsNull(), query.SRQuestionFormType == string.Empty,
                query.SRQuestionFormType == QuestionForm.QuestionFormType.PatientTransfer,
                query.SRQuestionFormType == QuestionForm.QuestionFormType.PraRegistration,
                query.SRQuestionFormType == QuestionForm.QuestionFormType.General));

            // Berdasarkan tipe user
            query.Where(query.Or(query.RestrictionUserType.IsNull(), query.RestrictionUserType == string.Empty,
                query.RestrictionUserType.Like("%" + userType + "%")));

            if (!string.IsNullOrWhiteSpace(searchText))
                query.Where(
                    query.QuestionFormID.Like(string.Format("{0}%", searchText))
                    || query.QuestionFormName.Like(string.Format("%{0}%", searchText)));

            query.Select(query.QuestionFormID,
                query.QuestionFormName,
                query.IsSingleEntry.Coalesce("0").As("IsSingleEntry"));

            query.OrderBy(query.QuestionFormName.Ascending);

            var dtb = query.LoadDataTable();

            var singleEntries = (from DataRow row in dtb.Rows where Convert.ToBoolean(row["IsSingleEntry"]) select row["QuestionFormID"].ToString()).ToList();

            if (singleEntries.Count > 0)
            {
                var qr = new PatientHealthRecordQuery("phr");
                qr.Select(qr.QuestionFormID); // Just for check
                qr.es.Distinct = true;
                qr.Where(qr.RegistrationNo == registrationNo, qr.QuestionFormID.In(singleEntries));
                qr.OrderBy(qr.QuestionFormID.Ascending);
                var dtbExistForm = qr.LoadDataTable();
                if (dtbExistForm.Rows.Count > 0)
                {
                    foreach (DataRow row in dtb.Rows)
                    {
                        if (Convert.ToBoolean(row["IsSingleEntry"]))
                        {
                            foreach (DataRow rowEf in dtbExistForm.Rows)
                            {
                                if (rowEf["QuestionFormID"].Equals(row["QuestionFormID"]))
                                {
                                    row.Delete();
                                    break;
                                }
                            }

                        }
                    }
                }
            }

            dtb.AcceptChanges();

            return dtb;
        }



        [WebMethod]
        public RadComboBoxData PhrMenuAdd(RadComboBoxContext context)
        {
            IDictionary<string, object> contextDictionary = (IDictionary<string, object>)context;
            var serviceUnitID = (string)contextDictionary["ServiceUnitID"];
            var registrationNo = (string)contextDictionary["RegistrationNo"];
            var isFirstRegInServiceUnit = "true".Equals(contextDictionary["IsFirstRegInServiceUnit"]);
            var userType = (string)contextDictionary["UserType"];

            List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
            RadComboBoxData comboData = new RadComboBoxData();
            try
            {
                var data = QuestionFormDatatable(context.Text, serviceUnitID, registrationNo, isFirstRegInServiceUnit, userType);

                int itemsPerRequest = 10;
                int itemOffset = context.NumberOfItems;
                int endOffset = itemOffset + itemsPerRequest;
                if (endOffset > data.Rows.Count)
                {
                    endOffset = data.Rows.Count;
                }
                if (endOffset == data.Rows.Count)
                {
                    comboData.EndOfItems = true;
                }
                else
                {
                    comboData.EndOfItems = false;
                }
                result = new List<RadComboBoxItemData>(endOffset - itemOffset);
                for (int i = itemOffset; i < endOffset; i++)
                {
                    RadComboBoxItemData itemData = new RadComboBoxItemData();
                    itemData.Text = data.Rows[i]["QuestionFormName"].ToString().Trim();
                    itemData.Value = data.Rows[i]["QuestionFormID"].ToString().Trim();
                    result.Add(itemData);
                }

                if (data.Rows.Count > 0)
                {
                    comboData.Message = String.Format("Records <b>1</b>-<b>{0}</b> out of <b>{1}+</b>",
                        endOffset.ToString(), data.Rows.Count.ToString());
                }
                else
                {
                    comboData.Message = "No matches";
                }
            }
            catch (Exception e)
            {
                comboData.Message = e.Message;
            }

            comboData.Items = result.ToArray();
            return comboData;
        }

        #endregion

    }
}
