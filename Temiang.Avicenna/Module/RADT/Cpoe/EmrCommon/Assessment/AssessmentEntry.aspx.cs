using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.CustomControl;
using Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class AssessmentEntry : BasePageDialogEntry
    {
        #region Properties

        public string PatientName
        {
            get
            {
                return ViewState["p_name"].ToString();
            }
            set { ViewState["p_name"] = value; }
        }
        public string MedicalNo
        {
            get
            {
                return ViewState["p_medNo"].ToString();
            }
            set { ViewState["p_medNo"] = value; }
        }
        public string Sex
        {
            get
            {
                return ViewState["p_sex"].ToString();
            }
            set { ViewState["p_sex"] = value; }
        }
        public string PatientAge
        {
            get
            {
                return ViewState["p_age"].ToString();
            }
            set { ViewState["p_age"] = value; }
        }

        private string AssessmentType
        {
            get
            {
                return Request.QueryString["astp"];
            }
        }
        private string RegistrationType
        {
            get
            {
                return Request.QueryString["rt"];
            }
        }
        public string RegistrationDate
        {
            get
            {
                return ViewState["p_regDate"].ToString();
            }
            set { ViewState["p_regDate"] = value; }

        }
        public string RegistrationTime
        {
            get
            {
                return ViewState["p_regTime"].ToString();
            }
            set { ViewState["p_regTime"] = value; }

        }

        public bool IsClosed
        {
            get { return Convert.ToBoolean(ViewState["p_isClosed"]); }
            set { ViewState["p_isClosed"] = value; }

        }

        public bool IsContinuedAssessment
        {
            get
            {
                // Jika tipe Initial Assessment dan record baru, cek apakah sudah ada kalau sudah ada rubah sbg ContinuedAssessment
                if (string.IsNullOrWhiteSpace(hdnIsContinuedAssessment.Value))
                {
                    if (Request.QueryString["mod"] == "new") // ambil dari query string krn dipakai pada init event 
                    {
                        if (AssessmentType == "CONT")
                            hdnIsContinuedAssessment.Value = "1"; // Utk versi manual menentukan asesmen awal atau ulangan
                        else
                        {
                            // Untuk versi otomatis menentukan  asesmen awal atau ulangan (masih rancu) (dipakai di Muhammadiyah PLB)
                            var pass = new PatientAssessment();
                            var passq = new PatientAssessmentQuery();
                            passq.Where(passq.PatientID == PatientID, passq.ServiceUnitID == ServiceUnitID,
                                passq.SRAssessmentType == AssessmentType);
                            passq.es.Top = 1;

                            if (pass.Load(passq) && !string.IsNullOrWhiteSpace(pass.RegistrationInfoMedicID))
                                hdnIsContinuedAssessment.Value = "true";
                            else
                                hdnIsContinuedAssessment.Value = Request.QueryString["iscontinued"];
                        }
                    }
                    else
                    {
                        hdnIsContinuedAssessment.Value = Request.QueryString["iscontinued"];
                    }

                }

                return hdnIsContinuedAssessment.Value == "1";
            }
        }
        protected string PageCaption { get; set; }

        #endregion

        #region override method

        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var asses = new PatientAssessment();
            if (asses.LoadByPrimaryKey(hdnRegistrationInfoMedicID.Value))
            {
                PopulatePhoto(asses.Photo);

                lblHandleTime.Text = Convert.ToDateTime(asses.AssessmentDateTime).ToString("HH:mm");
                hdnAssessmentDateTime.Value = Convert.ToDateTime(asses.AssessmentDateTime).ToString();
                var rim = new RegistrationInfoMedic();
                if (rim.LoadByPrimaryKey(hdnRegistrationInfoMedicID.Value))
                {
                    foreach (BaseAssessmentCtl ctl in _assessmentCtlList)
                    {
                        ctl.PopulateEntryControl(asses, rim);
                    }
                }

                edtOtherNotes.Content = asses.AdditionalNotes;

                if (divPhysicianSign.Visible == true)
                    PopulateSignImage(rbiParSign, hdnParSign, asses.SignImg);

                if (divPatientSign.Visible == true)
                    PopulateSignImage(rbiPatSign, hdnPatSign, asses.PatientSignImg);
            }
        }
        private void PopulateSignImage(RadBinaryImage rbImage, HiddenField hdnImage, Byte[] val)
        {
            rbImage.DataValue = val;
            if (val == null)
                hdnImage.Value = String.Empty;
            else
            {
                var mstream = new System.IO.MemoryStream(val);
                var img = new Telerik.Web.UI.ImageEditor.EditableImage(mstream);
                var imgHelper = new ImageHelper();
                hdnImage.Value = imgHelper.ToBase64String(img.Image, System.Drawing.Imaging.ImageFormat.Png);
            }
        }
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Sign
            var isEnable = newVal != AppEnum.DataMode.Read;
            btnParSign.Enabled = isEnable;
            btnPatSign.Enabled = isEnable;

            //Generated Control
            foreach (BaseAssessmentCtl ctl in _assessmentCtlList)
            {
                ctl.DataModeChanged(newVal != AppEnum.DataMode.Read);
            }
        }


        protected override void OnMenuNewClick()
        {
            lblHandleTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm", CultureInfo.InvariantCulture);
            hdnAssessmentDateTime.Value = DateTime.Now.ToString();

            foreach (BaseAssessmentCtl ctl in _assessmentCtlList)
            {
                ctl.OnMenuNewClick();
            }
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            //if (!string.IsNullOrWhiteSpace(hdnRegistrationInfoMedicID.Value)) // Tanda sudah berubah ke mode Edit
            //{
            //    // Cek jika mode new sudah di Save oleh tombol Save And Continue maka jalankan OnMenuSaveEditClick
            //    OnMenuSaveEditClick(args);
            //    return;
            //}

            if ((RegistrationType == AppConstant.RegistrationType.OutPatient || RegistrationType == AppConstant.RegistrationType.EmergencyPatient) && string.IsNullOrWhiteSpace(hdnParSign.Value) && AppParameter.GetParameterValue(AppParameter.ParameterItem.IsSignMandatoryOnAssessmentEntry) == "Yes")
            {
                args.MessageText = "Sign is required.";
                args.IsCancel = true;
                return;
            }

            var autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.RegInfoMedicNo);
            var newRimId = autoNumber.LastCompleteNumber;
            autoNumber.Save(); // Save untuk menghindari tersalip user lain

            var dateTime = Convert.ToDateTime(hdnAssessmentDateTime.Value);
            var asses = new PatientAssessment
            {
                PatientID = PatientID,
                SRAssessmentType = AssessmentType,
                RegistrationNo = RegistrationNo,
                ServiceUnitID = ServiceUnitID,
                RegistrationInfoMedicID = newRimId,
                AssessmentDateTime = dateTime,
                IsAutoAnamnesis = true
            };

            using (var trans = new esTransactionScope())
            {
                var rim = CreateNewSoapRegistrationInfoMedic(dateTime, newRimId);
                foreach (BaseAssessmentCtl ctl in _assessmentCtlList)
                {
                    ctl.RegistrationInfoMedicID = rim.RegistrationInfoMedicID;
                    ctl.SetEntityValue(args, asses, rim);

                    if (args.IsCancel)
                        return;
                }
                asses.IsInitialAssessment = !IsContinuedAssessment;

                // Cek jika PhysicalExam bukan tipe JSON maka masukan isi PhysicalExam
                if (!string.IsNullOrWhiteSpace(asses.PhysicalExam) && !asses.PhysicalExam.IsValidJson())
                    if (!string.IsNullOrWhiteSpace(rim.Info2))
                        rim.Info2 = string.Concat(rim.Info2, Environment.NewLine, asses.PhysicalExam);
                    else
                        rim.Info2 = asses.PhysicalExam;

                // Cek juga untuk OtherExam 
                if (!string.IsNullOrEmpty(asses.OtherExam))
                {
                    if (!string.IsNullOrWhiteSpace(rim.Info2))
                        rim.Info2 = string.Concat(rim.Info2, Environment.NewLine, "Pemeriksaan Penunjang: ",
                            asses.OtherExam);
                    else
                        rim.Info2 = string.Concat("Pemeriksaan Penunjang: ", asses.OtherExam);
                }

                rim.PopulatePrescriptionCurrentDay(rim.ParamedicID, RegistrationNo, FromRegistrationNo);

                asses.Photo = GetPhotoValue();
                asses.AdditionalNotes = edtOtherNotes.Content;

                SetSignValue(asses);

                asses.Save();
                rim.Save();

                trans.Complete();

                hdnRegistrationInfoMedicID.Value = newRimId;
            }

            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsAssessmentAutoSaveMds) == "Yes" && (RegistrationType == AppConstant.RegistrationType.OutPatient || RegistrationType == AppConstant.RegistrationType.EmergencyPatient))
                SaveMedicalDischargeSummary(asses);

            // UpdateSoapSubjective harus stelah proses save Asesmen supaya data history sudah tersimpan
            UpdateSoapSubjective(newRimId);


            UpdateTaskIdAntrol(RegistrationNo);

            //if (!string.IsNullOrEmpty(args.MessageText))
            //{
            //    args.IsCancel = true;
            //    AppSession.LastErrorException = new Exception(args.MessageText);
            //    var url = Page.ResolveUrl("~/ErrorPage.aspx");
            //    Helper.ShowRadWindowAfterPostback(winDialog, url, "preview", true);
            //}
        }

        private void SetSignValue(PatientAssessment asses)
        {
            var imgHelper = new ImageHelper();
            if (!string.IsNullOrWhiteSpace(hdnParSign.Value))
            {
                var resized = imgHelper.ResizeImage(imgHelper.ToImage(hdnParSign.Value), new System.Drawing.Size(332, 185));
                asses.SignImg = imgHelper.ToByteArray(resized, System.Drawing.Imaging.ImageFormat.Png);
            }
            else
                asses.SignImg = null;

            if (!string.IsNullOrWhiteSpace(hdnPatSign.Value))
            {
                var resized = imgHelper.ResizeImage(imgHelper.ToImage(hdnPatSign.Value), new System.Drawing.Size(332, 185));
                asses.PatientSignImg = imgHelper.ToByteArray(resized, System.Drawing.Imaging.ImageFormat.Png);
            }
            else
                asses.PatientSignImg = null;

            if (asses.PatientEducationSeqNo > 0)
            {
                var patEdu = new PatientEducation();
                if (patEdu.LoadByPrimaryKey(asses.RegistrationNo, asses.PatientEducationSeqNo ?? 0))
                {
                    patEdu.FmSign = asses.PatientSignImg;
                    patEdu.PsSign = asses.SignImg; // Educator;
                    patEdu.Save();
                }
            }
        }

        private byte[] GetPhotoValue()
        {
            if (!string.IsNullOrWhiteSpace(hdnImgData.Value))
            {
                // Contoh data 
                //  - dari JCrop  -> data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD...
                //  - dari CropIt -> data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAA...

                var imgHelper = new ImageHelper();
                var dataImage = imgHelper.ConvertBase64StringToImage(hdnImgData.Value.Split(',')[1]);
                var sizeImage = AppSession.Parameter.GetParameterValue(AppParameter.ParameterItem.AssessmentPhotoSize).ToInt();
                if (sizeImage == 0)
                    sizeImage = 400;
                var resizedImg = imgHelper.ResizeImage(dataImage, new System.Drawing.Size(sizeImage, sizeImage), true, System.Drawing.Drawing2D.InterpolationMode.Default);
                var compressedImg = imgHelper.CompressImageToArray(resizedImg, 100); // 115KB from 14KB 
                if (compressedImg != null)
                {
                    return compressedImg;
                }
            }
            return null;
        }
        private void UpdateSoapSubjective(string registrationInfoMedicID)
        {
            var rim = new RegistrationInfoMedic();
            if (rim.LoadByPrimaryKey(registrationInfoMedicID))
            {
                var asses = new PatientAssessment();
                asses.LoadByPrimaryKey(registrationInfoMedicID);

                var sbSbjPast = new StringBuilder();
                var sbSbjCurr = new StringBuilder();
                var sbSbjSoss = new StringBuilder();

                if (!asses.IsAutoAnamnesis ?? false)
                {
                    sbSbjCurr.AppendFormat("Alloanamnesis, dari: {0}", asses.AllowAnamnesisSource);
                    sbSbjCurr.AppendLine(string.Empty);
                }

                var reg = new Registration(); // jangan pakai RegistrationCurrent krn Complaint bisa belum tersave disini
                reg.LoadByPrimaryKey(RegistrationNo);

                if (!string.IsNullOrEmpty(reg.Complaint))
                {
                    sbSbjCurr.AppendLine("Keluhan utama:");
                    sbSbjCurr.AppendLine(reg.Complaint);
                    sbSbjCurr.AppendLine(string.Empty);
                }
                if (!string.IsNullOrEmpty(asses.Hpi))
                {
                    sbSbjCurr.AppendLine("Riwayat penyakit skrg:");
                    sbSbjCurr.AppendLine(asses.Hpi);
                    sbSbjCurr.AppendLine(string.Empty);
                }
                if (!string.IsNullOrEmpty(asses.Medikamentosa))
                {
                    sbSbjCurr.AppendLine("Riwayat pengobatan:");
                    sbSbjCurr.AppendLine(asses.Medikamentosa);
                    sbSbjCurr.AppendLine(string.Empty);
                }
                if (!string.IsNullOrEmpty(asses.AnamnesisNotes))
                {
                    sbSbjCurr.AppendLine("Lain-lain:");
                    sbSbjCurr.AppendLine(asses.AnamnesisNotes);
                    sbSbjCurr.AppendLine(string.Empty);
                }

                if ("IPKAN,PKAND".Contains(AssessmentType))
                {
                    if (asses.Fdolm != null)
                    {
                        sbSbjCurr.AppendFormat("HPHT: {0}, PHL: {1}",
                            asses.Fdolm.Value.ToString(AppConstant.DisplayFormat.DateShortMonth),
                            EstBirthDate(asses.Fdolm.Value).ToString(AppConstant.DisplayFormat.DateShortMonth));
                        sbSbjCurr.AppendLine(string.Empty);
                    }
                }

                //Riwayat Penyakit Dahulu
                var pastMedicalHistory = Patient.PastMedicalHistory(PatientID, true);
                if (!string.IsNullOrEmpty(pastMedicalHistory))
                {
                    sbSbjPast.AppendLine("Riwayat Penyakit Dahulu:");
                    sbSbjPast.AppendLine(pastMedicalHistory);
                    sbSbjPast.AppendLine(string.Empty);
                }

                var surgicalHist = new PastSurgicalHistory();
                surgicalHist.LoadByPrimaryKey(PatientID);
                if (!string.IsNullOrEmpty(surgicalHist.SurgicalHistory))
                {
                    sbSbjPast.AppendLine("Riwayat Pembedahan:");
                    sbSbjPast.AppendLine(surgicalHist.SurgicalHistory);
                    sbSbjPast.AppendLine(string.Empty);
                }

                //Riwayat Penyakit Dalam Keluarga
                var famMed = FamilyMedicalHistory.ToString(PatientID);
                if (!string.IsNullOrEmpty(famMed))
                {
                    sbSbjPast.AppendLine("Riwayat Penyakit Dalam Keluarga:");
                    sbSbjPast.AppendLine(famMed);
                    sbSbjPast.AppendLine(string.Empty);
                }

                //Riwayat Transfusion terdahulu
                var tfhist = new PastTransfusionHistory();
                tfhist.LoadByPrimaryKey(PatientID);
                if (!string.IsNullOrEmpty(tfhist.Year) || !string.IsNullOrEmpty(tfhist.AllergicReaction))
                {
                    sbSbjCurr.AppendFormat("Riwayat Transfusion: {0} {1}", tfhist.Year, tfhist.AllergicReaction);
                    sbSbjPast.AppendLine(string.Empty);
                }

                //Riwayat Job
                if (!string.IsNullOrEmpty(asses.JobHistNotes))
                {
                    sbSbjCurr.AppendFormat("Riwayat Job : {0} ", asses.JobHistNotes);
                    sbSbjPast.AppendLine(string.Empty);
                }

                //RSSTJ : tambah psiko-sosio di integrated notes (Fajri - 2023/10/26)
                var paQuest = new PatientAssessmentQuestField();
                if (paQuest.LoadByPrimaryKey(registrationInfoMedicID, "APSI.SOS"))
                {
                    var qaSkrn = JsonConvert.DeserializeObject<QuestionGroupAnswerValue>(paQuest.QuestionAnswer);
                    var answerValue1 = qaSkrn.QuestionAnswerValues.FirstOrDefault(x => x.QuestionID == "APSI.SOS01");
                    if (answerValue1 != null)
                    {
                        sbSbjSoss.AppendFormat("Status Mental: {0}", answerValue1.QuestionAnswerText);
                        sbSbjSoss.AppendLine(string.Empty);
                    }

                    var answerValue2 = qaSkrn.QuestionAnswerValues.FirstOrDefault(x => x.QuestionID == "APSI.SOS02");
                    if (answerValue1 != null)
                        sbSbjSoss.AppendFormat("Perilaku Kekerasan: {0}", answerValue2.QuestionAnswerText);
                }

                rim.Info1 = sbSbjCurr.AppendLine(sbSbjPast.AppendLine(sbSbjSoss.ToString()).ToString()).ToString();
                rim.Info1 = !string.IsNullOrWhiteSpace(rim.Info1) ? string.Concat(rim.Info1.TrimEnd(), Environment.NewLine, rim.Info1Entry) : rim.Info1Entry;
                rim.Save();
            }

        }

        private DateTime EstBirthDate(DateTime fdlom)
        {
            if (fdlom.Month <= 3) // Jan s/d Maret
                return (new DateTime(fdlom.Year, fdlom.Month + 9, fdlom.Day)).AddDays(7);
            else
                return (new DateTime(fdlom.Year + 1, fdlom.Month - 3, fdlom.Day)).AddDays(7);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var asses = new PatientAssessment();
            if (asses.LoadByPrimaryKey(hdnRegistrationInfoMedicID.Value))
            {
                using (var trans = new esTransactionScope())
                {
                    var rim = new RegistrationInfoMedic();
                    if (rim.LoadByPrimaryKey(hdnRegistrationInfoMedicID.Value))
                    {
                        rim.Info2 = string.Empty; // Reset
                        foreach (BaseAssessmentCtl ctl in _assessmentCtlList)
                        {
                            ctl.SetEntityValue(args, asses, rim);
                            if (args.IsCancel)
                                return;
                        }
                        asses.IsInitialAssessment = !IsContinuedAssessment;

                        //// Cek jika PhysicalExam bukan tipe JSON maka masukan isi PhysicalExam
                        //if (!asses.PhysicalExam.IsValidJson())
                        //    rim.Info2 = asses.PhysicalExam;

                        //// Tambah dg OtherExam 
                        //if (!string.IsNullOrEmpty(asses.OtherExam))
                        //    rim.Info2 = string.Concat(rim.Info2, Environment.NewLine, "Pemeriksaan Penunjang: ",
                        //        asses.OtherExam);

                        // Cek jika PhysicalExam bukan tipe JSON maka masukan isi PhysicalExam
                        if (!string.IsNullOrWhiteSpace(asses.PhysicalExam) && !asses.PhysicalExam.IsValidJson())
                            if (!string.IsNullOrWhiteSpace(rim.Info2))
                                rim.Info2 = string.Concat(rim.Info2, Environment.NewLine, asses.PhysicalExam);
                            else
                                rim.Info2 = asses.PhysicalExam;

                        // Cek juga untuk OtherExam 
                        if (!string.IsNullOrEmpty(asses.OtherExam))
                        {
                            if (!string.IsNullOrWhiteSpace(rim.Info2))
                                rim.Info2 = string.Concat(rim.Info2, Environment.NewLine, "Pemeriksaan Penunjang: ",
                                    asses.OtherExam);
                            else
                                rim.Info2 = string.Concat("Pemeriksaan Penunjang: ", asses.OtherExam);
                        }

                        rim.PopulatePrescriptionCurrentDay(rim.ParamedicID, RegistrationNo, FromRegistrationNo);

                        asses.Photo = GetPhotoValue();
                        asses.AdditionalNotes = edtOtherNotes.Content;

                        SetSignValue(asses);

                        asses.Save();
                        rim.Save();

                        trans.Complete();

                    }
                }

                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsAssessmentAutoSaveMds) == "Yes" && (RegistrationType == AppConstant.RegistrationType.OutPatient || RegistrationType == AppConstant.RegistrationType.EmergencyPatient))
                    SaveMedicalDischargeSummary(asses);
            }

            // SaveSoapSubjective harus stelah proses save Asesmen supaya data history sudah tersimpan
            UpdateSoapSubjective(hdnRegistrationInfoMedicID.Value);

            //var reg = new Registration();
            //reg.LoadByPrimaryKey(RegistrationNo);
            //UpdateTaskIdAntrol(reg);

            //if (!string.IsNullOrEmpty(args.MessageText))
            //{
            //    args.IsCancel = true;
            //    AppSession.LastErrorException = new Exception(args.MessageText);
            //    var url = Page.ResolveUrl("~/ErrorPage.aspx");
            //    Helper.ShowRadWindowAfterPostback(winDialog, url, "preview", true);

            //}
        }


        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
        }

        protected override void OnMenuEditClick()
        {
        }

        protected override void OnBeforeMenuNewClick(ValidateArgs args)
        {
            // Check IsSingleEntry
            var astp = new AppSRAssessmentType();
            if (astp.LoadByPrimaryKey(AssessmentType) && astp.IsSingleEntry == true)
            {
                var pass = new PatientAssessment();
                var passq = new PatientAssessmentQuery();
                passq.Where(passq.RegistrationNo == RegistrationNo, passq.SRAssessmentType == AssessmentType, passq.Or(passq.IsDeleted.IsNull(), passq.IsDeleted == false));
                passq.es.Top = 1;

                if (pass.Load(passq))
                {
                    args.IsCancel = true;
                    args.MessageText = "This Assessment type is singgle entry and already registered";
                    return;
                }
            }
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            return string.Empty;
        }

        protected override void OnInitializeAjaxManager(RadAjaxManager ajaxManager)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(ToolBarMenuData, divEntry);

            var pnlInfo = (Panel)Helper.FindControlRecursive(Master, "fw_PanelInfo");
            //var lblInfo = (Label)Helper.FindControlRecursive(Master, "fw_LabelInfo");

            ajax.AddAjaxSetting(ToolBarMenuData, pnlInfo);

            // AutoSave & SaveAndEdit update
            var fw_btnAutoSave = (Button)Helper.FindControlRecursive(Master, "fw_btnAutoSave");
            ajax.AddAjaxSetting(fw_btnAutoSave, hdnRegistrationInfoMedicID);

        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return string.Format("oArg.rimid='{0}'", hdnRegistrationInfoMedicID.Value);
        }

        //public override void OnMenuSaveAndEditClick(ValidateArgs args)
        //{
        //    if (string.IsNullOrWhiteSpace(hdnRegistrationInfoMedicID.Value))
        //    {
        //        OnMenuSaveNewClick(args);
        //        if (!args.IsCancel && string.IsNullOrWhiteSpace(args.MessageText))
        //            args.MessageText = "New assessmen has saved";
        //    }
        //    else
        //    {
        //        OnMenuSaveEditClick(args);
        //        if (!args.IsCancel && string.IsNullOrWhiteSpace(args.MessageText))
        //            args.MessageText = "Assessmen has saved";
        //    }
        //}
        #endregion

        private List<BaseAssessmentCtl> _assessmentCtlList = new List<BaseAssessmentCtl>();

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            var isMcuAssessment = false;
            var astp = new AppSRAssessmentType();
            if (astp.LoadByPrimaryKey(AssessmentType))
            {
                isMcuAssessment = astp.SRRegistrationType == AppConstant.RegistrationType.MedicalCheckUp;
            }

            #region Caption
            //switch (AssessmentType)
            //{
            //    case "SURGI": //"SURGICAL": // 1. asesmen bedah
            //    case "ISURG":
            //        PageCaption = AssessmentType == "SURGI" ? "Outpatient Surgery" : "Inpatient Surgery";
            //        break;
            //    case "DENTS": //"DENTIS": // 2. asesmen gigi
            //        PageCaption = "Outpatient Dental";
            //        break;
            //    case "EYE": // 3. asesmen mata
            //    case "IEYE":
            //        PageCaption = AssessmentType == "EYE" ? "Outpatient Eye" : "InPatient Eye";
            //        break;
            //    case "PSYCY": //"PSYCHIATRY": // 4. asesmen Psikiatri
            //    case "IPSYI":
            //        PageCaption = AssessmentType == "PSYCY" ? "Outpatient Psychiatry" : "Inpatient Psychiatry";
            //        break;
            //    case "KID": // 5. Asessmen Awal Rawat Inap Anak
            //    case "IKID":
            //        PageCaption = AssessmentType == "KID" ? "Outpatient Children" : "Inpatient Children";

            //        break;
            //    case "HEART": // 6. asesmen jantung
            //    case "IHERT":
            //        PageCaption = AssessmentType == "HEART" ? "Outpatient Cardiac & Vascular" : "Inpatient Cardiac & Vascular";
            //        break;
            //    case "INTER": //"INTERNAL": // 7. asesmen rajal PD  etc
            //    case "IINTR":
            //        PageCaption = AssessmentType == "INTER" ? "Outpatient Internal Disease" : "Inpatient Internal Disease";
            //        break;
            //    case "LUNG": // 8. asesmen paru
            //    case "ILUNG":
            //        PageCaption = AssessmentType == "LUNG" ? "Outpatient Pulmonary" : "Inpatient Pulmonary";
            //        break;
            //    case "THT": // 9. asesmen THT
            //    case "ITHT":
            //        PageCaption = AssessmentType == "THT" ? "Outpatient Throat Nose and Ear" : "Inpatient Throat Nose and Ear";
            //        break;
            //    case "NURSE": // "NURSING": // 10. asesmen Kandungan
            //    case "INURS":
            //    case "PKAND": // Gynecology 
            //    case "IPKAN":
            //        if (AssessmentType == "INURS" || AssessmentType == "IPKAN")
            //        {
            //            PageCaption = AssessmentType == "NURSE" ? "Inpatient Obstetrics" : "Inpatient Gynecology";
            //        }
            //        else
            //        {
            //            PageCaption = AssessmentType == "NURSE" ? "Outpatient Obstetrics" : "Outpatient Gynecology";
            //        }

            //        break;
            //    case "NEURO": //"NEUROLOGI": // 11. asesmen syaraf
            //    case "INEUR":
            //        PageCaption = AssessmentType == "NEURO" ? "Outpatient Neurology" : "Inpatient Neurology";
            //        break;
            //    case "SKIN": // 12. asesmen Kulit fix
            //    case "ISKIN": // 12. asesmen Kulit In Patient
            //        PageCaption = AssessmentType == "SKIN" ? "Outpatient Skin" : "Inpatient Skin";
            //        break;
            //    case "PSYCO":
            //        PageCaption = "Outpatient Psychology";
            //        break;
            //    case "NUTRI":
            //        PageCaption = "Outpatient Nutrient";
            //        break;
            //    case "REHAB":
            //        PageCaption = "Medic Rehabilitation";
            //        break;
            //    case "IGD":
            //        PageCaption = "IGD";
            //        break;
            //    case "GENRL":
            //        PageCaption = "General Clinic";
            //        break;
            //    case "EECP":
            //        PageCaption = "EECP";
            //        break;
            //    case "OBESY":
            //        PageCaption = "Obesytas";
            //        break;
            //    case "MDENT":
            //        PageCaption = "MCU - Dentis";
            //        isMcuAssessment = true;
            //        break;
            //    case "MCAR":
            //        PageCaption = "MCU - Cardiology";
            //        isMcuAssessment = true;
            //        break;
            //    case "MNEU":
            //        PageCaption = "MCU - Neurology";
            //        isMcuAssessment = true;
            //        break;
            //    case "MTHT":
            //        PageCaption = "MCU - Throat Nose and Ear";
            //        isMcuAssessment = true;
            //        break;
            //    case "MOBGY":
            //        PageCaption = "MCU - Obstetrics and Gynecology";
            //        isMcuAssessment = true;
            //        break;
            //    case "MDERM":
            //        PageCaption = "MCU - Dermato - Venereology";
            //        isMcuAssessment = true;
            //        break;
            //    case "MSPIR":
            //        PageCaption = "MCU - Spirometry";
            //        isMcuAssessment = true;
            //        break;
            //    case "MINTR":
            //        PageCaption = "MCU - Internal Disease";
            //        isMcuAssessment = true;
            //        break;
            //    case "MEYE":
            //        PageCaption = "MCU - Eye";
            //        isMcuAssessment = true;
            //        break;
            //    case "MSURM":
            //        PageCaption = "MCU - Surgery (Male)";
            //        isMcuAssessment = true;
            //        break;
            //    case "MSURF":
            //        PageCaption = "MCU - Surgery (Female)";
            //        isMcuAssessment = true;
            //        break;
            //    default:
            //        var stdi = new AppStandardReferenceItem();
            //        if (stdi.LoadByPrimaryKey("AssessmentType", AssessmentType))
            //        {
            //            PageCaption = stdi.ItemName;
            //        }
            //        break;
            //}
            #endregion

            var stdi = new AppStandardReferenceItem();
            if (stdi.LoadByPrimaryKey("AssessmentType", AssessmentType))
            {
                Title = stdi.ItemName;
            }

            // Cek jika ada seting di matrix
            var mtxColl = LoadControlEntryMatrix(AppSession.Parameter.HealthcareInitialAppsVersion);
            if (mtxColl.Count > 0)
                _assessmentCtlList = LoadControlListFromMatrix(mtxColl);
            else
            {
                // Check Default
                mtxColl = LoadControlEntryMatrix("DEFAULT");
                _assessmentCtlList = mtxColl.Count > 0
                    ? LoadControlListFromMatrix(mtxColl)
                    : LoadControlList(isMcuAssessment);
            }

            //Refer Answer
            if (!isMcuAssessment)
            {
                // Jawab consul harus oleh dokter bersangkutan jadi amannya pakai AppSession.UserLogin.ParamedicID
                var consult =
                    ParamedicConsultRefer.LastConsultReferTo(MergeRegistrations, AppSession.UserLogin.ParamedicID);
                if (consult != null)
                {
                    _assessmentCtlList.Add((BaseAssessmentCtl)Page.LoadControl(
                        "~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/ConsultReferAnswerCtl.ascx"));
                }
            }

            var isPeExist = false;

            // Add control to contentEntry
            var i = 1;
            foreach (BaseAssessmentCtl ctl in _assessmentCtlList)
            {
                var cpnl = InitializedAssessmentControl(i, ctl);
                if (ctl.EntryGroup == BaseAssessmentCtl.EntryGroupEnum.Anamnesis)
                {
                    anamnesisEntry.Controls.Add(cpnl);

                }
                else if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsEmrHideDivPeEntryOnAssessmentEntry) == "No")  //RSSTJ : Collapse semua control kecuali anamnesis (Fajri - 2023/10/30)
                {
                    if (ctl.EntryGroup == BaseAssessmentCtl.EntryGroupEnum.PhysicalExam)
                    {
                        if (ctl.Column == BaseAssessmentCtl.ColumnEnum.Left)
                        {
                            peLeft.Controls.Add(cpnl);
                        }
                        else
                        {
                            peRight.Controls.Add(cpnl);
                        }

                        isPeExist = true;
                    }
                    else
                    {
                        contentEntry.Controls.Add(cpnl);
                    }
                }
                else
                {
                    contentEntry.Controls.Add(cpnl);
                }
                i++;
            }

            peEntry.Visible = isPeExist;
        }

        private Control InitializedAssessmentControl(int i, BaseAssessmentCtl ctl)
        {
            if (string.IsNullOrEmpty(ctl.Description))
                return ctl;

            var cpnl = new CollapsePanel();
            cpnl.ID = string.Format("cp_{0}", i);

            cpnl.Title = ctl.Description;

            cpnl.Controls.Add(ctl);
            cpnl.IsCollapsed = ctl.IsPanelCollapse.ToString().ToLower();
            return cpnl;
        }

        private AppControlEntryMatrixCollection LoadControlEntryMatrix(string healthcareInitialAppsVersion)
        {
            var qr = new AppControlEntryMatrixQuery("a");
            var qrRef = new AppControlQuery("b");
            qr.InnerJoin(qrRef).On(qr.ControlID == qrRef.ControlID);
            qr.SelectAll();
            qr.Select(qrRef.ControlUrl.As("refToAppControl_ControlUrl"), qrRef.Description.As("refToAppControl_Description"));

            var entryType = string.Format("ASSESSMENT-{0}", AssessmentType);
            qr.Where(qr.HealthcareInitialAppsVersion == healthcareInitialAppsVersion,
                qr.EntryType == entryType, qr.IsVisible == true);
            qr.OrderBy(qr.IndexNo.Ascending);

            var mtxColl = new AppControlEntryMatrixCollection();
            mtxColl.Load(qr);
            return mtxColl;
        }

        private List<BaseAssessmentCtl> LoadControlListFromMatrix(AppControlEntryMatrixCollection mtxColl)
        {
            var assessmentCtlList = new List<BaseAssessmentCtl>();
            foreach (var item in mtxColl)
            {
                // Untuk control yg list entriannya dari table Question
                if (item.ControlID == "AssessmentQuestFieldCtl")
                {
                    if (!string.IsNullOrWhiteSpace(item.ReferenceValue))
                    {
                        var qg = new QuestionGroup();
                        if (qg.LoadByPrimaryKey(item.ReferenceValue))
                            item.Description = qg.QuestionGroupName;
                    }

                    if (string.IsNullOrWhiteSpace(item.Description))
                        continue; // Skip jika belum didefinisikan QuestionGroupID nya
                }


                var ctl = (BaseAssessmentCtl)Page.LoadControl(item.ControlUrl);
                if (!string.IsNullOrWhiteSpace(item.ReferenceValue))
                    ctl.ReferenceValue = item.ReferenceValue;

                ctl.Description = item.Description;
                ctl.IsPanelCollapse = item.IsPanelCollapse ?? false;
                assessmentCtlList.Add(ctl);
            }

            return assessmentCtlList;
        }

        // Hardcode control list (dipakai di Muhammadiyah Palembang)
        private List<BaseAssessmentCtl> LoadControlList(bool isMcuAssessment)
        {
            PageCaption = string.Format("{0} Medical Assessment of {1}",
                isMcuAssessment ? string.Empty : IsContinuedAssessment ? "Continued" : "Initial", PageCaption);

            var assessmentCtlList = new List<BaseAssessmentCtl>();
            var baseInitialPhsycalExamCtlUrl = "~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/Initial/";
            var defPhsycalExamCtlUrl = string.Empty;
            var phsycalExamInitialCtlUrl = string.Empty;
            var phsycalExamContinuedCtlUrl = string.Empty;
            var isUseChildBirtHist = false;
            var isUseMedicalHist = true;
            var isUseLocalist = false;
            var isUseEducation = false;
            var isUseDiagnoseTherapy = true;
            var isUseOdontogram = false;
            var isUseAncillaryExam = false;
            var isFollowUpPlan = false;
            var isUseImunizationHist = false;
            var isUseBirthFoodGrowthHist = false;

            var pastMedicalHistRefId = string.Empty;
            var familyMedHistRefId = string.Empty;
            // Control for Initial Assessment
            switch (AssessmentType)
            {
                case "SURGI": //"SURGICAL": // 1. asesmen bedah
                case "ISURG":
                    isUseAncillaryExam = AssessmentType == "SURGI";
                    defPhsycalExamCtlUrl = "Surgical/SurgicalPeCtl.ascx";
                    isUseLocalist = true;
                    isUseEducation = AssessmentType == "SURGI";
                    break;
                case "DENTS": //"DENTIS": // 2. asesmen gigi
                    defPhsycalExamCtlUrl = "Dentis/DentisPeCtl.ascx";
                    isUseEducation = true;
                    isUseOdontogram = true;
                    break;
                case "EYE": // 3. asesmen mata
                case "IEYE":
                    isUseAncillaryExam = AssessmentType == "IEYE";
                    defPhsycalExamCtlUrl = "Eye/EyePeCtl.ascx";
                    isUseLocalist = true;
                    isUseEducation = AssessmentType == "EYE";
                    break;
                case "PSYCY": //"PSYCHIATRY": // 4. asesmen Psikiatri
                case "IPSYI":
                case "PSYCO":
                    isUseAncillaryExam = false; //sudah di hardcode di PsychiatryPeCtl.ascx
                    defPhsycalExamCtlUrl = "Psychiatry/PsychiatryPeCtl.ascx";
                    isUseEducation = AssessmentType == "PSYCY";
                    break;
                case "KID": // 5. Asessmen Awal Rawat Inap Anak
                case "IKID":
                    isUseAncillaryExam = AssessmentType == "IKID";
                    defPhsycalExamCtlUrl = "Kid/KidPeCtl.ascx";
                    isUseLocalist = true;
                    isUseImunizationHist = true;
                    isUseBirthFoodGrowthHist = true;
                    pastMedicalHistRefId = "KID";
                    familyMedHistRefId = string.Empty;

                    break;
                case "HEART": // 6. asesmen jantung
                case "IHERT":
                    isUseAncillaryExam = AssessmentType == "IHERT" || AssessmentType == "HEART";
                    defPhsycalExamCtlUrl = "Heart/HeartPeCtl.ascx";
                    isUseEducation = AssessmentType == "HEART";
                    break;
                case "INTER": //"INTERNAL": // 7. asesmen rajal PD  etc
                case "IINTR":
                    isUseAncillaryExam = AssessmentType == "IINTR";
                    defPhsycalExamCtlUrl = "Internal/InternalPeCtl.ascx";
                    break;
                case "LUNG": // 8. asesmen paru
                case "ILUNG":
                    defPhsycalExamCtlUrl = "Lung/LungPeCtl.ascx";
                    isUseEducation = AssessmentType == "LUNG";
                    break;
                case "THT": // 9. asesmen THT
                case "ITHT":
                    isUseAncillaryExam = AssessmentType == "ITHT";
                    defPhsycalExamCtlUrl = "Tht/ThtPeCtl.ascx";
                    //defPhsycalExamCtlUrl = "Tht/ThtPe2Ctl.ascx";
                    isUseLocalist = true;
                    break;
                case "NURSE": // "NURSING": // 10. asesmen Kandungan
                case "INURS":
                case "PKAND": // Gynecology 
                case "IPKAN":
                    if (AssessmentType == "INURS" || AssessmentType == "IPKAN")
                    {
                        isUseAncillaryExam = true;
                    }
                    else
                    {
                        isUseEducation = true;
                    }

                    defPhsycalExamCtlUrl = "Nursing/NursingPeCtl.ascx";
                    isUseLocalist = true;
                    isUseChildBirtHist = true;
                    break;
                case "NEURO": //"NEUROLOGI": // 11. asesmen syaraf
                case "INEUR":
                    defPhsycalExamCtlUrl = "Neurology/NeurologyPeCtl.ascx"; // v1 Muhamadiyah
                    //defPhsycalExamCtlUrl = "Neurology/NeurologyPeCtl2.ascx"; // Cibinong
                    isUseEducation = AssessmentType == "NEURO";
                    break;
                case "SKIN": // 12. asesmen Kulit fix
                case "ISKIN": // 12. asesmen Kulit In Patient
                    isUseAncillaryExam = AssessmentType == "ISKIN";
                    defPhsycalExamCtlUrl = "Skin/SkinPeCtl.ascx";
                    isUseLocalist = true;
                    isUseEducation = AssessmentType == "SKIN";
                    break;
                //case "PSYCO": /*deby: disamakan dg 4. asesmen Psikiatri krn form belum ada.. */
                //    defPhsycalExamCtlUrl = "Psychology/PsychologyPeCtl.ascx";
                //    break;
                case "NUTRI":
                    defPhsycalExamCtlUrl = "Nutrient/NutrientPeCtl.ascx";
                    break;
                case "REHAB":
                    defPhsycalExamCtlUrl = "Rehabilitation/RehabilitationPeCtl.ascx";
                    break;
                case "IGD":
                    defPhsycalExamCtlUrl = "Igd/IgdEsiPeCtl.ascx"; // RS MP
                    isUseLocalist = true;
                    break;
                case "GENRL":
                    defPhsycalExamCtlUrl = "General/GeneralPeCtl.ascx";
                    break;
                case "EECP":
                    defPhsycalExamCtlUrl = "Eecp/EecpPeCtl.ascx";
                    break;
                case "OBESY":
                    defPhsycalExamCtlUrl = "Obesy/ObesyPeCtl.ascx";
                    pastMedicalHistRefId = "OBESY";
                    familyMedHistRefId = string.Empty;
                    break;
                case "MDENT":
                    defPhsycalExamCtlUrl = "Dentis/DentisMcuCtl.ascx";
                    isMcuAssessment = true;
                    break;
                case "MCAR":
                    defPhsycalExamCtlUrl = "Cardiology/CardiologyMcuCtl.ascx";
                    isMcuAssessment = true;
                    break;
                case "MNEU":
                    defPhsycalExamCtlUrl = "Neurology/NeurologyMcuCtl.ascx";
                    isMcuAssessment = true;
                    break;
                case "MTHT":
                    defPhsycalExamCtlUrl = "Tht/ThtMcuCtl.ascx";
                    isMcuAssessment = true;
                    break;
                case "MOBGY":
                    defPhsycalExamCtlUrl = "Obgyn/ObgynMcuCtl.ascx";
                    isMcuAssessment = true;
                    break;
                case "MDERM":
                    defPhsycalExamCtlUrl = "Derma/DermaMcuCtl.ascx";
                    isMcuAssessment = true;
                    break;
                case "MSPIR":
                    defPhsycalExamCtlUrl = "Spiro/SpiroMcuCtl.ascx";
                    isMcuAssessment = true;
                    break;
                case "MINTR":
                    defPhsycalExamCtlUrl = "Interna/InternaMcuCtl.ascx";
                    isMcuAssessment = true;
                    break;
                case "MEYE":
                    defPhsycalExamCtlUrl = "Eye/EyeMcuCtl.ascx";
                    isMcuAssessment = true;
                    break;
                case "MSURM":
                    defPhsycalExamCtlUrl = "SurgeryMale/SurgeryMaleMcuCtl.ascx";
                    isMcuAssessment = true;
                    break;
                case "MSURF":
                    defPhsycalExamCtlUrl = "SurgeryFemale/SurgeryFemaleMcuCtl.ascx";
                    isMcuAssessment = true;
                    break;
            }

            // Override with custom field
            var at = new AppStandardReferenceItem();
            if (at.LoadByPrimaryKey("AssessmentType", AssessmentType))
            {
                try
                {
                    var assessmentTypeSetting = JsonConvert.DeserializeObject<AssessmentType>(at.CustomField);
                    isUseMedicalHist = assessmentTypeSetting.IsUseMedicalHist ?? isUseMedicalHist;
                    isUseLocalist = assessmentTypeSetting.IsUseLocalist ?? isUseLocalist;
                    isUseEducation = assessmentTypeSetting.IsUseEducation ?? isUseEducation;
                    isUseDiagnoseTherapy = assessmentTypeSetting.IsUseDiagnoseTherapy ?? isUseDiagnoseTherapy;
                    isUseOdontogram = assessmentTypeSetting.IsUseOdontogram ?? isUseOdontogram;
                    isUseChildBirtHist = assessmentTypeSetting.IsUseChildBirtHist ?? false;
                    isUseImunizationHist = assessmentTypeSetting.IsUseImunizationHist ?? false;
                    isUseBirthFoodGrowthHist = assessmentTypeSetting.IsUseBirthFoodGrowthHist ?? false;
                    phsycalExamInitialCtlUrl = assessmentTypeSetting.PhsycalExamInitialCtlUrl;
                    phsycalExamContinuedCtlUrl = assessmentTypeSetting.PhsycalExamContinuedCtlUrl;
                    PageCaption = assessmentTypeSetting.PageCaption ?? PageCaption;
                    pastMedicalHistRefId = assessmentTypeSetting.PastMedicalHistRefId ?? pastMedicalHistRefId;
                    familyMedHistRefId = assessmentTypeSetting.FamilyMedHistRefId ?? familyMedHistRefId;
                    isUseAncillaryExam = assessmentTypeSetting.IsUseAncillaryExam ?? isUseAncillaryExam;
                    isFollowUpPlan = assessmentTypeSetting.IsFollowUpPlan ?? isFollowUpPlan;
                }
                catch (Exception)
                {
                    // Nothing
                }
            }

            // Localist Statusdibuat selalu ada
            isUseLocalist = true;

            // Override if Continued Assessment
            if (!isMcuAssessment && IsContinuedAssessment)
            {
                if (AssessmentType != "DENTS") // Untuk gigi sama dgn asesmen awal
                {
                    baseInitialPhsycalExamCtlUrl = "~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/Continued/";
                    defPhsycalExamCtlUrl = "ContinuedCtl.ascx";
                }
            }


            // General
            assessmentCtlList.Add(
                (BaseAssessmentCtl)Page.LoadControl("~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/GeneralCtl.ascx"));

            //Child Birth History
            if (!IsContinuedAssessment && isUseChildBirtHist)
            {
                assessmentCtlList.Add(
                    (BaseAssessmentCtl)Page.LoadControl(
                        "~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/PatientChildBirthHistCtl.ascx"));
            }

            // Medical History
            // MCU dan Continue asesmen tidak menampilkan Medical Hist
            if (!isMcuAssessment && !IsContinuedAssessment && isUseMedicalHist)
            {
                var ctl =
                    (BaseAssessmentCtl)
                    Page.LoadControl("~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/MedHistCtl.ascx");
                ctl.PastMedicalHistRefId = pastMedicalHistRefId;
                ctl.FamilyMedHistRefId = familyMedHistRefId;
                assessmentCtlList.Add(ctl);
            }


            if (!IsContinuedAssessment && isUseBirthFoodGrowthHist)
            {
                assessmentCtlList.Add((BaseAssessmentCtl)Page.LoadControl(
                    "~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/Initial/Kid/BirthFoodGrowthHistCtl.ascx"));
            }

            // Immunization
            if (!IsContinuedAssessment && isUseImunizationHist)
            {
                assessmentCtlList.Add((BaseAssessmentCtl)Page.LoadControl(
                    "~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/Initial/Kid/ImunizationHistCtl.ascx"));
            }

            // Physical Exam
            if (isMcuAssessment)
            {
                assessmentCtlList.Add(
                    (BaseAssessmentCtl)Page.LoadControl("~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/Mcu/" +
                                                         defPhsycalExamCtlUrl));
            }
            else
            {
                if ((IsContinuedAssessment && string.IsNullOrEmpty(phsycalExamContinuedCtlUrl)) ||
                    (!IsContinuedAssessment && string.IsNullOrEmpty(phsycalExamInitialCtlUrl)))
                {
                    assessmentCtlList.Add(
                        (BaseAssessmentCtl)Page.LoadControl(baseInitialPhsycalExamCtlUrl + defPhsycalExamCtlUrl));
                }
                else if ((IsContinuedAssessment && !string.IsNullOrEmpty(phsycalExamContinuedCtlUrl)) ||
                         (!IsContinuedAssessment && !string.IsNullOrEmpty(phsycalExamInitialCtlUrl)))
                {
                    assessmentCtlList.Add((BaseAssessmentCtl)Page.LoadControl(IsContinuedAssessment
                        ? phsycalExamContinuedCtlUrl
                        : phsycalExamInitialCtlUrl));
                }
            }

            if (isUseLocalist)
            {
                assessmentCtlList.Add(
                    (BaseAssessmentCtl)Page.LoadControl(
                        "~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/LocalistCtl.ascx"));
            }

            // Anchillary Exam
            if (!IsContinuedAssessment && isUseAncillaryExam)
            {
                assessmentCtlList.Add(
                    (BaseAssessmentCtl)
                    Page.LoadControl("~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/AncillaryExamCtl.ascx"));
            }


            // Diagnose & Therapy
            if (isUseDiagnoseTherapy)
            {
                //assessmentCtlList.Add(
                //    (BaseAssessmentCtl)
                //        Page.LoadControl("~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/DiagnoseTherapyCtl.ascx"));

                if (RegistrationType == AppConstant.RegistrationType.InPatient)
                    assessmentCtlList.Add(
                        (BaseAssessmentCtl)
                        Page.LoadControl("~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/WorkDiagnoseCtl.ascx"));
                else
                    assessmentCtlList.Add(
                        (BaseAssessmentCtl)
                        Page.LoadControl("~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/DiagnoseCtl.ascx"));

                assessmentCtlList.Add(
                    (BaseAssessmentCtl)
                    Page.LoadControl("~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/PlanActionCtl.ascx"));
            }

            if (RegistrationType != AppConstant.RegistrationType.InPatient && isFollowUpPlan)
            {
                assessmentCtlList.Add(
                    (BaseAssessmentCtl)
                    Page.LoadControl("~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/FollowUpPlanCtl.ascx"));
            }

            // Education
            if (!isMcuAssessment && !IsContinuedAssessment && isUseEducation)
            {
                assessmentCtlList.Add(
                    (BaseAssessmentCtl)Page.LoadControl(
                        "~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/EducationCtl.ascx"));
            }

            // Odontogram
            if (!isMcuAssessment && isUseOdontogram)
            {
                assessmentCtlList.Add((BaseAssessmentCtl)Page.LoadControl(
                    "~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/Initial/Dentis/OdontogramCtl.ascx"));
            }



            return assessmentCtlList;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            // Program Fiture
            IsSingleRecordMode = true; //Save then close
            IsMedicalRecordEntry = true; //Activate deadline edit & add
            ToolBar.AutoSaveVisible = true;
            ToolBar.SaveAndEditVisible = true;
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = false;
            ToolBar.AddVisible = false;
            // -------------------

            // Hardcode security dilayar depan ..kalu mode view berarti hanya bisa view saja
            if (Request.QueryString["mod"] == "view")
            {
                ToolBar.EditVisible = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Simpan informasi RegistrationInfoMedicID yg sedang diedit
                hdnRegistrationInfoMedicID.Value = Request.QueryString["rimid"];

                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {

                    PatientName = pat.PatientName;
                    MedicalNo = pat.MedicalNo;
                    Sex = pat.Sex;
                }

                var reg = RegistrationCurrent;

                PatientAge = string.Format("{0}Y {1}M {2}D", reg.AgeInYear, reg.AgeInMonth, reg.AgeInDay);
                RegistrationDate = string.Format("{0}", Convert.ToDateTime(reg.RegistrationDate).ToString(AppConstant.DisplayFormat.Date));
                RegistrationTime = string.Format("{0}", reg.RegistrationTime);
                IsClosed = reg.IsClosed ?? false;

                divPatientSign.Visible = AppParameter.IsYes(AppParameter.ParameterItem.IsAssessmentFamilyOrPatientSign);
                divPhysicianSign.Visible = AppParameter.IsYes(AppParameter.ParameterItem.IsAssessmentPhysicianSign);

                PopulateRegistrationInfo();
            }
        }

        private string ServiceUnitID
        {
            get
            {
                return Request.QueryString["unit"];
            }
        }
        private RegistrationInfoMedic CreateNewSoapRegistrationInfoMedic(DateTime dateTime, string rimid)
        {
            var ent = new RegistrationInfoMedic();
            ent.RegistrationInfoMedicID = rimid;

            ent.RegistrationNo = RegistrationNo;
            ent.ParamedicID = ParamedicID;
            ent.SRMedicalNotesInputType = "SOAP"; //Hardcode
            ent.ServiceUnitID = ServiceUnitID;

            ent.Info1 = string.Empty;
            ent.Info2 = string.Empty;
            ent.Info3 = string.Empty;
            ent.Info4 = string.Empty;
            ent.DateTimeInfo = dateTime;

            ent.AttendingNotes = string.Empty;
            ent.IsInformConcern = false;
            ent.Save();
            return ent;
        }

        #region Save Medical Discharge Summary
        private void SaveMedicalDischargeSummary(PatientAssessment asses)
        {
            var medsum = new MedicalDischargeSummary();

            if (!medsum.LoadByPrimaryKey(RegistrationNo))
            {
                medsum = new MedicalDischargeSummary();
                medsum.RegistrationNo = RegistrationNo;
            }

            var reg = RegistrationCurrent;
            if (reg == null)
                return;

            var par = new Paramedic();
            par.LoadByPrimaryKey(reg.ParamedicID);

            var timeNow = (new DateTime()).NowAtSqlServer();
            medsum.DischargeDate = timeNow.Date;
            medsum.DischargeTime = timeNow.ToString("HH:mm");
            medsum.Medications = PrescriptionHist(MergeRegistrations);
            medsum.PhysicalExam = ResumeMedisRichTextInPatientEntry.PhysicalExaminationHist(RegistrationNo, FromRegistrationNo);
            medsum.ChiefComplaint = asses.ChiefComplaint;
            medsum.HistOfPresentIllness = asses.Hpi;
            medsum.PastMedicalHistory = Patient.PastMedicalHistory(PatientID, true);
            medsum.SuggestionFollowUp = asses.Therapy;
            medsum.ParamedicID = reg.ParamedicID;
            medsum.ParamedicName = par.ParamedicName;
            medsum.IsRichTextMode = true; // Utk membedakan layar entry dan cetakan krn data yg lama bisa masalah dalam konversinya jika dibuat mode richtext
            medsum.DocumentDate = reg.RegistrationDate;
            medsum.PpaSign = asses.SignImg;
            medsum.Save();

            SaveRegistrationInfoMedicMDS(reg.RegistrationNo, reg.ServiceUnitID);
            SaveDiagnoseFromEpisodeDiagnose(reg.RegistrationNo);
            SaveProcedureFromEpisodeProcedure(reg.RegistrationNo);

            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsAssessmentAutoSaveMdsCasemixWithGuarantorDoc) == "Yes")
            {
                // Copy ulang ke MDS Casemix selama Mds Casemix belum di approve
                var mdsCmx = new MedicalDischargeSummaryCmx();
                if (!mdsCmx.LoadByPrimaryKey(RegistrationNo) || !(mdsCmx.IsApproved ?? false))
                    CopyToMdsCaseMix();

                // Save to SEP Doc source code copy from ReportViewer
                if (AppParameter.IsYes(AppParameter.ParameterItem.IsAutoSaveMdsDpjpToSepFolderAfterSave))
                {
                    try
                    {
                        SaveMdsToSepFolder();
                    }
                    catch (Exception ex)
                    {

                        // Nothing do
                    }
                }
            }
        }

        #region Copy to MDS CaseMix 
        private void CopyToMdsCaseMix()
        {
            using (var trans = new esTransactionScope())
            {
                var medsum = new MedicalDischargeSummary();
                if (medsum.LoadByPrimaryKey(RegistrationNo))
                {
                    var controlPlan = string.Empty;
                    var ent = new MedicalDischargeSummaryByNurse();
                    if (ent.LoadByPrimaryKey(RegistrationNo))
                    {
                        controlPlan = ent.ControlPlan;
                    }

                    CopyToMdsCaseMix(medsum, controlPlan);

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
        }
        private void CopyToMdsCaseMix(MedicalDischargeSummary medsum, string controlPlan)
        {
            //1. Copy MedicalDischargeSummary
            var medsumCmx = new MedicalDischargeSummaryCmx();
            if (!medsumCmx.LoadByPrimaryKey(medsum.RegistrationNo))
            {
                medsumCmx = new MedicalDischargeSummaryCmx();
            }

            foreach (esColumnMetadata col in medsum.es.Meta.Columns)
            {
                medsumCmx.SetColumn(col.Name, medsum.GetColumn(col.Name));
            }

            medsumCmx.ControlPlan = controlPlan;
            medsumCmx.Save();

            //2. Copy MedicalDischargeSummaryProcedure
            var procs = new MedicalDischargeSummaryProcedureCollection();
            procs.Query.Where(procs.Query.RegistrationNo == medsum.RegistrationNo);
            procs.Query.Load();

            var procCmxs = new MedicalDischargeSummaryProcedureCmxCollection();
            procCmxs.Query.Where(procCmxs.Query.RegistrationNo == medsum.RegistrationNo);
            procCmxs.Query.Load();
            procCmxs.MarkAllAsDeleted();
            procCmxs.Save();

            foreach (var proc in procs)
            {
                var procCmx = procCmxs.AddNew();
                foreach (esColumnMetadata col in proc.es.Meta.Columns)
                {
                    procCmx.SetColumn(col.Name, proc.GetColumn(col.Name));
                }
            }
            procCmxs.Save();

            //3. Copy MedicalDischargeSummaryDiagnose
            var diags = new MedicalDischargeSummaryDiagnoseCollection();
            diags.Query.Where(diags.Query.RegistrationNo == medsum.RegistrationNo);
            diags.Query.Load();

            var diagCmxs = new MedicalDischargeSummaryDiagnoseCmxCollection();
            diagCmxs.Query.Where(diagCmxs.Query.RegistrationNo == medsum.RegistrationNo);
            diagCmxs.Query.Load();
            diagCmxs.MarkAllAsDeleted();
            diagCmxs.Save();

            foreach (var diag in diags)
            {
                var diagCmx = diagCmxs.AddNew();
                foreach (esColumnMetadata col in diag.es.Meta.Columns)
                {
                    diagCmx.SetColumn(col.Name, diag.GetColumn(col.Name));
                }
            }
            diagCmxs.Save();

            //4. Copy MedicalDischargeSummaryBodyDiagram
            var bds = new MedicalDischargeSummaryBodyDiagramCollection();
            bds.Query.Where(bds.Query.RegistrationNo == medsum.RegistrationNo);
            bds.Query.Load();

            var bdCmxs = new MedicalDischargeSummaryBodyDiagramCmxCollection();
            bdCmxs.Query.Where(bdCmxs.Query.RegistrationNo == medsum.RegistrationNo);
            bdCmxs.Query.Load();
            bdCmxs.MarkAllAsDeleted();
            bdCmxs.Save();

            foreach (var bd in bds)
            {
                var bdCmx = bdCmxs.AddNew();
                foreach (esColumnMetadata col in bd.es.Meta.Columns)
                {
                    bdCmx.SetColumn(col.Name, bd.GetColumn(col.Name));
                }
            }
            bdCmxs.Save();

            //5. Copy ReferExternal
            var refExCmx = new ReferExternalCmx();
            if (refExCmx.LoadByPrimaryKey(RegistrationNo))
            {
                refExCmx.MarkAsDeleted();
                refExCmx.Save();
            }
            var refEx = new ReferExternal();
            if (refEx.LoadByPrimaryKey(medsum.RegistrationNo))
            {
                refExCmx = new ReferExternalCmx();
                foreach (esColumnMetadata col in refEx.es.Meta.Columns)
                {
                    refExCmx.SetColumn(col.Name, refEx.GetColumn(col.Name));
                }
                refExCmx.Save();
            }

            //6. Copy Home prescription
            var hps = new MedicationReceiveCollection();
            hps.Query.Where(hps.Query.RegistrationNo == medsum.RegistrationNo, hps.Query.IsBroughtHome == true);
            hps.Query.Load();

            var hpCmxs = new MedicalDischargeSummaryPrescHomeCmxCollection();
            hpCmxs.Query.Where(hpCmxs.Query.RegistrationNo == medsum.RegistrationNo);
            hpCmxs.Query.Load();
            hpCmxs.MarkAllAsDeleted();
            hpCmxs.Save();

            foreach (var hp in hps)
            {
                var hpCmx = hpCmxs.AddNew();
                foreach (esColumnMetadata col in hp.es.Meta.Columns)
                {
                    hpCmx.SetColumn(col.Name, hp.GetColumn(col.Name));
                }
            }
            hpCmxs.Save();
        }

        #endregion

        private void SaveMdsToSepFolder()
        {
            var isSaveMdsToSepFolder = false;
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(RegistrationNo))
            {
                var gr = new Guarantor();
                gr.LoadByPrimaryKey(reg.GuarantorID);
                var isBpjsPatient = gr.SRGuarantorType.Equals(AppParameter.GetParameterValue(AppParameter.ParameterItem.GuarantorTypeBpjs));
                if (isBpjsPatient && !string.IsNullOrEmpty(reg.BpjsSepNo))
                {
                    isSaveMdsToSepFolder = true;
                }
                else
                {
                    isSaveMdsToSepFolder = !gr.SRGuarantorType.Equals(AppParameter.GetParameterValue(AppParameter.ParameterItem.GuarantorTypeSelf)); ;
                }
            }

            if (isSaveMdsToSepFolder)
            {
                // Save cetakan MDS
                var printJobParameters = new PrintJobParameterCollection();
                printJobParameters.AddNew("p_RegistrationNo", RegistrationNo);
                printJobParameters.AddNew("p_IsForCasemix", "0");
                var path = Module.Reports.ReportViewer.SaveFileToGuarantorDocument(AppSession.Parameter.HealthcareInitial, "SLP.01.0089b", printJobParameters); //Resume Medis Rawat Inap
            }
        }

        private void SaveRegistrationInfoMedicMDS(string refNo, string serviceUnitID)
        {
            var ent = new RegistrationInfoMedic();
            var qr = new RegistrationInfoMedicQuery();
            qr.Where(qr.RegistrationNo == RegistrationNo, qr.SRMedicalNotesInputType == "MDS");
            qr.es.Top = 1;

            ent.Load(qr);

            if (string.IsNullOrEmpty(ent.RegistrationInfoMedicID))
            {
                var autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.RegInfoMedicNo);
                ent.RegistrationInfoMedicID = autoNumber.LastCompleteNumber;
                autoNumber.Save();

                ent.RegistrationNo = RegistrationNo;

                ent.SRMedicalNotesInputType = "MDS";
                ent.ServiceUnitID = serviceUnitID;
                ent.ParamedicID = ParamedicID;
            }

            ent.Info1 = string.Format("Medical Discharge Summary");
            ent.Info2 = string.Empty;
            ent.Info3 = string.Empty;
            ent.Info4 = string.Empty;
            ent.IsPRMRJ = true;
            var date = DateTime.Now;
            ent.DateTimeInfo = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0);
            ent.ReferenceNo = refNo;
            ent.ReferenceType = "MDS";
            ent.Save();
        }

        private MedicalDischargeSummaryDiagnose CreateNewMdsDiagnose(MedicalDischargeSummaryDiagnoseCollection mdsDiags)
        {
            var ed = mdsDiags.AddNew();
            ed.DiagnoseID = string.Empty;

            var lastSeqNo = string.Empty;
            foreach (var item in mdsDiags)
            {
                if (!string.IsNullOrEmpty(item.SequenceNo))
                    lastSeqNo = item.SequenceNo;
            }

            var newSeqNo = (lastSeqNo == null || string.IsNullOrEmpty(lastSeqNo))
                ? "001"
                : string.Format("{0:000}", int.Parse(lastSeqNo) + 1);
            ed.SequenceNo = newSeqNo;
            if (ed.SequenceNo == "001")
                ed.SRDiagnoseType = AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain);
            else
                ed.SRDiagnoseType = string.Empty;

            ed.RegistrationNo = RegistrationNo;
            ed.ParamedicID = AppSession.UserLogin.ParamedicID;
            ed.IsVoid = false;
            ed.IsOldCase = false;

            return ed;
        }

        private void SaveDiagnoseFromEpisodeDiagnose(string registrationNo)
        {
            var mdsDiags = new MedicalDischargeSummaryDiagnoseCollection();
            mdsDiags.Query.Where(mdsDiags.Query.RegistrationNo == registrationNo);
            mdsDiags.LoadAll();
            mdsDiags.MarkAllAsDeleted();
            mdsDiags.Save();

            // Import last 1 Main diagnose
            var epDiags = new EpisodeDiagnoseCollection();
            var epDiagQr = new EpisodeDiagnoseQuery("ed");
            epDiagQr.Where(epDiagQr.RegistrationNo == registrationNo, epDiagQr.IsVoid == false,
                epDiagQr.SRDiagnoseType == AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            epDiagQr.Select(epDiagQr.SRDiagnoseType, epDiagQr.DiagnoseID, epDiagQr.DiagnosisText, epDiagQr.ExternalCauseID, epDiagQr.IsOldCase, epDiagQr.DiagnoseSynonym);
            epDiagQr.OrderBy(epDiagQr.CreateDateTime.Descending);
            epDiagQr.es.Top = 1;
            epDiags.Load(epDiagQr);

            var mainSeqNo = string.Empty;
            if (epDiags.Count > 0)
            {
                var epdiag = epDiags[0];

                var diag = CreateNewMdsDiagnose(mdsDiags);
                diag.SRDiagnoseType = epdiag.SRDiagnoseType;
                diag.DiagnoseID = epdiag.DiagnoseID;
                diag.DiagnosisText = epdiag.DiagnosisText;
                diag.ExternalCauseID = epdiag.ExternalCauseID;
                diag.IsOldCase = epdiag.IsOldCase;
                diag.DiagnoseType =
                    StandardReference.GetItemName(AppEnum.StandardReference.DiagnoseType, epdiag.SRDiagnoseType);
                diag.DiagnoseSynonym = epdiag.DiagnoseSynonym;

                mainSeqNo = epdiag.SequenceNo;
            }

            // Other diagnose
            epDiags = new EpisodeDiagnoseCollection();
            epDiagQr = new EpisodeDiagnoseQuery("wd");
            epDiagQr.Where(epDiagQr.RegistrationNo == registrationNo,
                epDiagQr.IsVoid == false,
                epDiagQr.SRDiagnoseType != AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            if (!string.IsNullOrWhiteSpace(mainSeqNo))
                epDiagQr.Where(epDiagQr.SequenceNo != mainSeqNo);
            epDiagQr.OrderBy(epDiagQr.SRDiagnoseType.Ascending, epDiagQr.DiagnoseID.Ascending, epDiagQr.ExternalCauseID.Ascending);
            epDiagQr.es.Distinct = true;
            epDiagQr.Select(epDiagQr.SRDiagnoseType, epDiagQr.DiagnoseID, epDiagQr.DiagnosisText, epDiagQr.ExternalCauseID, epDiagQr.IsOldCase, epDiagQr.DiagnoseSynonym);
            epDiags.Load(epDiagQr);
            if (epDiags.Count > 0)
            {
                foreach (var epDiag in epDiags)
                {
                    var ed = CreateNewMdsDiagnose(mdsDiags);
                    ed.SRDiagnoseType = epDiag.SRDiagnoseType;
                    ed.DiagnoseID = epDiag.DiagnoseID;
                    ed.DiagnosisText = epDiag.DiagnosisText;
                    ed.ExternalCauseID = epDiag.ExternalCauseID;
                    ed.IsOldCase = epDiag.IsOldCase;
                    ed.DiagnoseType =
                        StandardReference.GetItemName(AppEnum.StandardReference.DiagnoseType, epDiag.SRDiagnoseType);
                    ed.DiagnoseSynonym = epDiag.DiagnoseSynonym;
                }
            }

            mdsDiags.Save();
        }

        private MedicalDischargeSummaryProcedure CreateNewMdsProcedure(MedicalDischargeSummaryProcedureCollection dischargeProc)
        {
            var ed = dischargeProc.AddNew();
            ed.ProcedureID = string.Empty;

            var lastSeqNo = string.Empty;
            foreach (var item in dischargeProc)
            {
                if (!string.IsNullOrEmpty(item.SequenceNo))
                    lastSeqNo = item.SequenceNo;
            }

            var newSeqNo = (lastSeqNo == null || string.IsNullOrEmpty(lastSeqNo))
                ? "001"
                : string.Format("{0:000}", int.Parse(lastSeqNo) + 1);
            ed.SequenceNo = newSeqNo;
            ed.RegistrationNo = RegistrationNo;
            ed.ParamedicID = AppSession.UserLogin.ParamedicID;
            ed.IsVoid = false;

            return ed;
        }

        public void SaveProcedureFromEpisodeProcedure(string registrationNo)
        {
            var mdsProc = new MedicalDischargeSummaryProcedureCollection();
            mdsProc.Query.Where(mdsProc.Query.RegistrationNo == registrationNo);
            mdsProc.LoadAll();
            mdsProc.MarkAllAsDeleted();
            mdsProc.Save();

            // Import last All Procedures
            var epProcs = new EpisodeProcedureCollection();
            epProcs.Query.Where(epProcs.Query.RegistrationNo == registrationNo,
                epProcs.Query.IsVoid == false);
            epProcs.Query.OrderBy(epProcs.Query.ProcedureDate.Ascending);
            epProcs.LoadAll();
            if (epProcs.Count > 0)
            {
                foreach (var proc in epProcs)
                {
                    var ed = CreateNewMdsProcedure(mdsProc);

                    ed.ParamedicID = proc.ParamedicID;

                    // Override ambil ParamedicID dari yg membuat laporan Operasi
                    if (proc.BookingNo != null && proc.OpNotesSeqNo != null)
                    {
                        var note = new ServiceUnitBookingOperatingNotes();
                        if (note.LoadByPrimaryKey(proc.BookingNo, proc.OpNotesSeqNo))
                            ed.ParamedicID = note.ParamedicID;
                    }

                    ed.ProcedureID = proc.ProcedureID;
                    ed.ProcedureName = proc.ProcedureName;
                    ed.ProcedureSynonym = proc.ProcedureSynonym;
                }
            }

            mdsProc.Save();
        }

        private static string PrescriptionHist(List<string> mergeRegistrations)
        {
            if (AppParameter.IsYes(AppParameter.ParameterItem.IsMedicalDischargeSummaryPrescJustItemName))
                return PrescriptionHistJustItemName(mergeRegistrations);
            else
                return PrescriptionHistWithConsumeMethodInfo(mergeRegistrations);
        }

        private static string PrescriptionHistJustItemName(List<string> mergeRegistrations)
        {
            var query = new TransPrescriptionItemQuery("a");
            var qrPresc = new TransPrescriptionQuery("b");
            query.InnerJoin(qrPresc).On(query.PrescriptionNo == qrPresc.PrescriptionNo);

            var qrItem = new ItemQuery("i");
            query.InnerJoin(qrItem).On(query.ItemID == qrItem.ItemID);

            var itemProduct = new ItemProductMedicQuery("ip");
            query.InnerJoin(itemProduct).On(query.ItemID == itemProduct.ItemID);


            var itemIntervention = new ItemQuery("int");
            query.LeftJoin(itemIntervention).On(query.ItemInterventionID == itemIntervention.ItemID);

            var itemProductInt = new ItemProductMedicQuery("ipi");
            query.LeftJoin(itemProductInt).On(query.ItemInterventionID == itemProductInt.ItemID);


            query.Select(
                "<COALESCE(int.ItemName, i.ItemName) as ItemName>"
            );
            query.es.Distinct = true;
            query.Where(qrPresc.RegistrationNo.In(mergeRegistrations));
            // Hanya tipe medication
            query.Where(query.Or(itemProductInt.IsMedication == true, query.And(itemProductInt.IsMedication.IsNull(), itemProduct.IsMedication == true)));

            var dtbPresc = query.LoadDataTable();
            var strb = new StringBuilder();
            strb.AppendLine("<ul>");
            foreach (DataRow row in dtbPresc.Rows)
            {
                strb.AppendFormat("<li> {0}</li>", row["ItemName"]);
            }

            strb.AppendLine("</ul>");
            var prescriptionHist = strb.ToString();
            return prescriptionHist;
        }

        private static string PrescriptionHistWithConsumeMethodInfo(List<string> mergeRegistrations)
        {
            // Obat patent
            var query = PrescriptionItemNameList(mergeRegistrations, false);
            var dtbPresc = query.LoadDataTable();
            var strb = new StringBuilder();
            strb.AppendLine("<ul>");
            foreach (DataRow row in dtbPresc.Rows)
            {
                strb.AppendFormat("<li> {0} ({1} {2} {3})</li>",
                    row["ItemName"], row["SRConsumeMethodName"], row["ConsumeQty"], row["SRConsumeUnit"]);
            }

            // Obat Racikan
            query = PrescriptionItemNameList(mergeRegistrations, true);
            dtbPresc = query.LoadDataTable();
            foreach (DataRow row in dtbPresc.Rows)
            {
                var consumeMethod = string.Format("{0} {1} {2}", row["SRConsumeMethodName"], row["ConsumeQty"], row["SRConsumeUnit"]);
                var itemDescription = PrescriptionItemCompound(row["PrescriptionNo"].ToString(), row["SequenceNo"].ToString(), consumeMethod);
                strb.AppendFormat("{0}", itemDescription);
            }
            strb.AppendLine("</ul>");
            var prescriptionHist = strb.ToString();
            return prescriptionHist;
        }

        private static TransPrescriptionItemQuery PrescriptionItemNameList(List<string> mergeRegistrations, bool isCompound)
        {
            //Prescription History, yg diambil hanya daftar obat dan consume methodnya
            var query = new TransPrescriptionItemQuery("a");
            var qrPresc = new TransPrescriptionQuery("b");
            query.InnerJoin(qrPresc).On(query.PrescriptionNo == qrPresc.PrescriptionNo);

            var qrItem = new ItemQuery("i");
            query.InnerJoin(qrItem).On(query.ItemID == qrItem.ItemID);

            var itemProduct = new ItemProductMedicQuery("ip");
            query.InnerJoin(itemProduct).On(query.ItemID == itemProduct.ItemID);

            var itemIntervention = new ItemQuery("int");
            query.LeftJoin(itemIntervention).On(query.ItemInterventionID == itemIntervention.ItemID);

            var itemProductInt = new ItemProductMedicQuery("ipi");
            query.LeftJoin(itemProductInt).On(query.ItemInterventionID == itemProductInt.ItemID);


            var consume = new ConsumeMethodQuery("e");
            query.LeftJoin(consume).On(query.SRConsumeMethod == consume.SRConsumeMethod);


            query.Select(
                "<COALESCE(int.ItemName, i.ItemName) as ItemName>",
                consume.SRConsumeMethodName,
                query.ConsumeQty,
                query.SRConsumeUnit,
                query.IsCompound

            );

            if (isCompound)
            {
                query.Select(query.ParentNo,
                    query.SequenceNo, query.PrescriptionNo);
                query.Where(query.Or(query.ParentNo.IsNull(), query.ParentNo == string.Empty));
            }
            else
            {
                query.Select("<'' as ParentNo>",
                    "<'' as SequenceNo>",
                    "<'' as PrescriptionNo>");
            }
            query.OrderBy("ItemName", esOrderByDirection.Ascending);
            query.es.Distinct = true;
            //if (!string.IsNullOrEmpty(fromRegistrationNo))
            //    query.Where(query.Or(qrPresc.RegistrationNo == registrationNo,
            //        qrPresc.RegistrationNo == fromRegistrationNo));
            //else
            //    query.Where(qrPresc.RegistrationNo == registrationNo);

            query.Where(qrPresc.RegistrationNo.In(mergeRegistrations));
            // Hanya tipe medication
            query.Where(query.Or(itemProductInt.IsMedication == true, query.And(itemProductInt.IsMedication.IsNull(), itemProduct.IsMedication == true)));

            query.Where(query.IsCompound == isCompound);
            return query;
        }

        private static string PrescriptionItemCompound(string prescriptionNo, string sequenceNo, string consumeMethod)
        {
            // Racikan
            var query = new TransPrescriptionItemQuery("a");
            var qItem = new ItemQuery("b");
            var qItemMedic = new ItemProductMedicQuery("im");
            var qItemIntervention = new ItemQuery("c");

            query.InnerJoin(qItem).On(query.ItemID == qItem.ItemID);
            query.InnerJoin(qItemMedic).On(query.ItemID == qItemMedic.ItemID);
            query.LeftJoin(qItemIntervention).On(query.ItemInterventionID == qItemIntervention.ItemID);

            query.Select
            (
                query.ItemInterventionID, query.ParentNo, query.IsRFlag,
                qItem.ItemName, query.SRDosageUnit, query.DosageQty,
                qItemIntervention.ItemName.Coalesce("''").As("ItemNameIntervention")
            );

            query.Where(query.PrescriptionNo == prescriptionNo, query.Or(query.SequenceNo == sequenceNo, query.ParentNo == sequenceNo));
            query.OrderBy(query.SequenceNo.Ascending);

            var dtb = query.LoadDataTable();
            var sbItem = new StringBuilder();
            foreach (DataRow row in dtb.Rows)
            {
                var itemName = row["ItemName"].ToString();
                if (row["ItemInterventionID"] != DBNull.Value &&
                    !string.IsNullOrEmpty(row["ItemInterventionID"].ToString()))
                {
                    itemName = row["ItemNameIntervention"].ToString();
                }

                if (row["ParentNo"] != DBNull.Value && string.IsNullOrEmpty(row["ParentNo"].ToString()))
                {
                    //Header
                    sbItem = new StringBuilder();
                    sbItem.AppendFormat("<li>{0} @{1} {2} ({3}){4}</li>", itemName, row["DosageQty"], row["SRDosageUnit"], consumeMethod, Environment.NewLine);
                    sbItem.AppendLine("<ul>");
                }
                else
                {
                    sbItem.AppendFormat("<li> {0} @{1} {2}{3}</li>", itemName, row["DosageQty"], row["SRDosageUnit"], Environment.NewLine);

                }
            }
            sbItem.AppendLine("</ul>");
            return sbItem.ToString();
        }
        #endregion

        private void PopulateRegistrationInfo()
        {
            var reg = RegistrationCurrent;

            var pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);

            lblPatientName.Text = pat.PatientName;

            lblMedicalNo.Text = pat.MedicalNo;
            lblRegistrationDate.Text = string.Format("{0} / {1}", Convert.ToDateTime(reg.RegistrationDate).ToString(AppConstant.DisplayFormat.DateShortMonth), reg.RegistrationTime);
            lblRegistrationNo.Text = reg.RegistrationNo;
            lblGender.Text = pat.Sex == "M" ? "Male" : "Female";
            lblDateOfBirth.Text = string.Format("{0} ({1}y {2}m {3}d)", (pat.DateOfBirth ?? new DateTime()).ToString(AppConstant.DisplayFormat.DateShortMonth),
                reg.AgeInYear, reg.AgeInMonth, reg.AgeInDay);
            lblEmployeeJobTitleName.Text = pat.EmployeeJobTitleName;

            var stdi = new AppStandardReferenceItem();
            if (stdi.LoadByPrimaryKey("Education", pat.SREducation))
            {
                lblSREducation.Text = stdi.ItemName;
            }


            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);
            lblGuarantor.Text = grr.GuarantorName;

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(RegistrationCurrent.ServiceUnitID);
            lblServiceUnit.Text = unit.ServiceUnitName;

            if (!string.IsNullOrWhiteSpace(reg.RoomID))
            {
                var room = new ServiceRoom();
                room.LoadByPrimaryKey(reg.RoomID);
                lblRoom.Text = room.RoomName;
            }

            PopulatePhoto(null);
        }


        private void PopulatePhoto(byte[] photo)
        {
            var patient = new Patient();
            patient.LoadByPrimaryKey(Request.QueryString["patid"]);

            // Show image from last capture
            if (!string.IsNullOrWhiteSpace(hdnImgData.Value))
            {
                imgPatientPhoto.ImageUrl = hdnImgData.Value;
                return;
            }

            // Show Image from DB
            if (photo != null)
                imgPatientPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(photo));
            else if (patient.Sex == "M")
                imgPatientPhoto.ImageUrl = "~/Images/Asset/Patient/ManVector.png";
            else if (patient.Sex == "F")
                imgPatientPhoto.ImageUrl = "~/Images/Asset/Patient/WomanVector.png";

        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            if (!IsPostBack)
            {
                if (Helper.IsBpjsAntrolIntegration)
                {
                    ViewState["task4_emr"] = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds());
                }
            }
        }

        private void UpdateTaskIdAntrol(string registrationNo)
        {
            if (Helper.IsBpjsAntrolIntegration)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(registrationNo);

                try
                {
                    if (!string.IsNullOrWhiteSpace(reg.AppointmentNo) && reg.SRRegistrationType != AppConstant.RegistrationType.InPatient)
                    {
                        var log = new WebServiceAPILog();
                        log.DateRequest = DateTime.Now;
                        log.IPAddress = string.Empty;
                        log.UrlAddress = "AssesmentEntry";
                        log.Params = JsonConvert.SerializeObject(new Temiang.Avicenna.Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                        {
                            Kodebooking = reg.AppointmentNo,
                            Taskid = 4,
                            Waktu = Convert.ToInt64(ViewState["task4_emr"])
                        });

                        var svc = new Temiang.Avicenna.Common.BPJS.Antrian.Service();
                        var response = svc.UpdateWaktuAntrian(new Temiang.Avicenna.Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                        {
                            Kodebooking = reg.AppointmentNo,
                            Taskid = 4,
                            Waktu = Convert.ToInt64(ViewState["task4_emr"])
                        });

                        log.Response = JsonConvert.SerializeObject(response);
                        log.Save();

                        var time = (new DateTime()).NowAtSqlServer();
                        reg.IsConfirmedAttendance = true;
                        reg.ConfirmedAttendanceByUserID = AppSession.UserLogin.UserID;
                        reg.ConfirmedAttendanceDateTime = time;
                        reg.Save();
                    }
                }
                catch (Exception ex)
                {
                    var log = new WebServiceAPILog
                    {
                        DateRequest = DateTime.Now,
                        IPAddress = "",
                        UrlAddress = "AssesmentEntry",
                        Params = JsonConvert.SerializeObject(new Temiang.Avicenna.Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                        {
                            Kodebooking = reg.AppointmentNo,
                            Taskid = 4,
                            Waktu = ViewState["task4_emr"] == null ? 0 : Convert.ToInt64(ViewState["task4_emr"])
                        }),
                        Response = JsonConvert.SerializeObject(new
                        {
                            ex.Source,
                            ex.Message,
                            ex.StackTrace,
                            InnerException = ex.InnerException == null ? null : new
                            {
                                ex.Source,
                                ex.Message,
                                ex.StackTrace
                            }
                        }),
                        Totalms = 0
                    };
                    log.Save();
                }

                try
                {
                    if (!string.IsNullOrWhiteSpace(reg.AppointmentNo))
                    {
                        var log = new WebServiceAPILog();
                        log.DateRequest = DateTime.Now;
                        log.IPAddress = string.Empty;
                        log.UrlAddress = "AssesmentEntry";
                        log.Params = JsonConvert.SerializeObject(new Temiang.Avicenna.Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                        {
                            Kodebooking = reg.AppointmentNo,
                            Taskid = 5,
                            Waktu = Convert.ToInt64(DateTimeOffset.Now.AddMinutes(6).ToUnixTimeMilliseconds())
                        });

                        var svc = new Temiang.Avicenna.Common.BPJS.Antrian.Service();
                        var response = svc.UpdateWaktuAntrian(new Temiang.Avicenna.Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                        {
                            Kodebooking = reg.AppointmentNo,
                            Taskid = 5,
                            Waktu = Convert.ToInt64(DateTimeOffset.Now.AddMinutes(6).ToUnixTimeMilliseconds())
                        });

                        log.Response = JsonConvert.SerializeObject(response);
                        log.Save();

                        var time = (new DateTime()).NowAtSqlServer();
                        reg.IsFinishedAttendance = true;
                        reg.FinishedAttendanceByUserID = AppSession.UserLogin.UserID;
                        reg.FinishedAttendanceDateTime = time;
                        reg.Save();
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}