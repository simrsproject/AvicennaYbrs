using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Module.RADT.EmrIp;
using System.Configuration;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class EmrDetail : BasePage
    {
        //private int _deadlineAddable = AppParameter.GetParameterValue(AppParameter.ParameterItem.DeadlineMedicalRecordAddableAfterDischarge).ToInt();
        protected string ParamedicFirstTransChargesItemIds = AppSession.Parameter.GetParameterValueString(AppParameter.ParameterItem.ParamedicFirstTransChargesItemIds);
        protected string GetAbortAddMessage()
        {
            //if (_deadlineAddable > 0)
            //{
            //    if (!IsDischargeDateTimeLessThan(_deadlineAddable, RegistrationCurrent))
            //    {

            //        var par = AppParameter.GetParameter(AppParameter.ParameterItem.DeadlineMedicalRecordAddableAfterDischarge);
            //        return string.Format(par.Message, par.ParameterValue);
            //    }
            //}

            var args = new ValidateArgs();
            MedicalRecordAddableValidate(args, RegistrationCurrent); // Ambil message dari method ini
            if (args.IsCancel)
                return args.MessageText;

            return string.Empty;
        }

        #region Properties
        private List<string> _patientRelateds;
        protected List<string> PatientRelateds
        {
            get
            {
                if (_patientRelateds == null)
                {
                    _patientRelateds = Patient.PatientRelateds(PatientID);
                }

                return _patientRelateds;
            }
        }
        protected string RegistrationType
        {
            get
            {
                return Request.QueryString["rt"];
            }
        }

        public bool IsClosed
        {
            get { return Convert.ToBoolean(ViewState["_mc_isclosed"]); }
            set { ViewState["_mc_isclosed"] = value; }
        }

        protected string ServiceUnitID
        {
            get
            {
                return Request.QueryString["unit"];
            }
        }
        protected string RoomID
        {
            get
            {
                return Request.QueryString["room"];
            }
        }

        protected string PatientName
        {
            get { return Convert.ToString(ViewState["patname"]); }
            set { ViewState["patname"] = value; }
        }

        /// <summary>
        /// InitialAssessment == true if new in clinic
        /// </summary>
        protected bool IsNewPatient
        {
            get { return Convert.ToBoolean(ViewState["isnp"]); }
            set { ViewState["isnp"] = value; }
        }

        public string ReferFromRegistrationNo
        {
            get
            {
                return Request.QueryString["fregno"];
            }
        }

        public string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }

        public string ParamedicID
        {
            get
            {
                return Request.QueryString["parid"];
            }
        }

        private Patient _patientCurrent;
        private Patient PatientCurrent
        {
            get
            {
                if (_patientCurrent == null)
                {
                    _patientCurrent = new Patient();
                    _patientCurrent.LoadByPrimaryKey(RegistrationCurrent.PatientID);
                }

                return _patientCurrent;
            }
        }

        private string _lastRegNo = null;
        private string LastRegistrationNo
        {
            get
            {
                if (_lastRegNo == null)
                {
                    var lastReg = PatientCurrent.LastRegistration();
                    if (lastReg != null)
                        _lastRegNo = lastReg.RegistrationNo;
                    else
                        _lastRegNo = string.Empty;
                }

                return _lastRegNo;
            }

        }
        private Registration _regCurr;
        private Registration RegistrationCurrent
        {
            get
            {
                if (_regCurr == null)
                {
                    _regCurr = new Registration();
                    _regCurr.LoadByPrimaryKey(RegistrationNo);
                }

                return _regCurr;
            }
        }

        private bool? _isMedicalDisSumModeRichText = null;
        protected bool IsMedicalDisSumModeRichText
        {
            get
            {
                if (_isMedicalDisSumModeRichText == null)
                {
                    var mds = new MedicalDischargeSummary();
                    if (mds.LoadByPrimaryKey(RegistrationNo) && !(mds.IsRichTextMode ?? false))
                        _isMedicalDisSumModeRichText = false;
                    else
                        _isMedicalDisSumModeRichText = true;
                }
                return _isMedicalDisSumModeRichText ?? false;
            }
        }

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;
            if (!IsPostBack)
            {
                PopulateToolbarHaisMonitoring();
                PopulateToolbarPrint();

                // Hide / Unhide ClinicalPathway ToolbarItem
                foreach (RadToolBarItem tItem in tbMenu.Items)
                {
                    if (tItem is RadToolBarDropDown) continue;
                    if (tItem.Value != "ClinicalPathway") continue;
                    //tItem.Visible = AppSession.Parameter.CasemixValidationRegistrationType.Contains(RegistrationType);
                    tItem.Visible = AppSession.Parameter.ClinicalPathwayRegistrationType.Contains(RegistrationType);
                }
            }
        }

        private void PopulateToolbarHaisMonitoring()
        {
            var tbarHais = (RadToolBarDropDown)tbMenu.Items[10];

            var std = new AppStandardReferenceItemQuery();
            std.Where(std.StandardReferenceID == "HaisMonitoring");
            std.Select(std.Note.As("Value"));
            if (AppSession.Parameter.HaisMonitoringProgramName == "NAT")
                std.Select(std.CustomField.As("ProgramName"));
            else if (AppSession.Parameter.HaisMonitoringProgramName == "INT")
                std.Select(std.CustomField2.As("ProgramName"));
            else
                std.Select(std.ItemName.As("ProgramName"));
            std.OrderBy(std.ItemID.Ascending);

            var dtb = std.LoadDataTable();

            if (dtb.Rows.Count > 0)
            {
                foreach (var btn in from DataRow row in dtb.Rows
                                    select new RadToolBarButton(row["ProgramName"].ToString())
                                    {
                                        Value = string.Format("{0}", row["Value"])
                                    })
                {
                    tbarHais.Buttons.Add(btn);
                }
            }
        }

        private void PopulateToolbarPrint()
        {
            // Dipindah ke Page_Init (Handono 2022 08)
            //foreach (RadToolBarItem tItem in tbMenu.Items)
            //{
            //    if (tItem is RadToolBarDropDown) continue;
            //    if (tItem.Value != "ClinicalPathway") continue;
            //    tItem.Visible = AppSession.Parameter.CasemixValidationRegistrationType.Contains(RegistrationType);
            //}

            // Isi Related Program type Report
            var tbarPrint = (RadToolBarDropDown)tbMenu.Items[tbMenu.Items.Count - 2];

            var qPrg = new AppProgramQuery("a");
            var qRel = new AppProgramRelatedQuery("b");
            qRel.InnerJoin(qPrg).On(qRel.RelatedProgramID == qPrg.ProgramID);

            qRel.Where(qRel.Or(qRel.ProgramID == AppConstant.Program.EpisodeAndHistory, qRel.ProgramID == AppConstant.Program.ElectronicMedicalRecord),
                qRel.Or(qPrg.ProgramType.In("RPT", "XML"), qPrg.ProgramType == "RSLIP"));

            qRel.Select(qRel.RelatedProgramID, qPrg.ProgramName);
            qRel.es.Distinct = true;
            var dtb = qRel.LoadDataTable();

            if (dtb.Rows.Count > 0)
            {
                foreach (var btn in from DataRow row in dtb.Rows
                                    select new RadToolBarButton(row["ProgramName"].ToString())
                                    {
                                        Value = string.Format("rpt_{0}", row["RelatedProgramID"])
                                    })
                {
                    tbarPrint.Buttons.Add(btn);
                }
            }

            tbarPrint.Enabled = tbarPrint.Buttons.Count > 0; //Print
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ajaxManagerProxy.AjaxSettings.AddAjaxSetting(grdAssessment, medicalHistCtl.GridDiagAndPrescription);
            if (!IsPostBack)
            {
                IsNewPatient = true;

                // Check sudah ada registrasi di Service Unit jika baru 1 berarti pasien baru
                // Lebih tepat jika PatientAssessment baru 1 di Service Unit terpilih tapi akan bermasalah jika assessment dihapus
                var regColl = new RegistrationCollection();
                var regQuery = new RegistrationQuery();
                regQuery.Where(regQuery.PatientID == PatientID, regQuery.ServiceUnitID == ServiceUnitID, regQuery.RegistrationNo < RegistrationNo, regQuery.IsVoid == false);
                regQuery.es.Top = 1;
                if (regColl.Load(regQuery))
                {
                    IsNewPatient = regColl.Count == 0;
                }
                PopulateRegistrationInfo();

            }

            // Selalu Populate toolbar Assessment
            PopulateAssessmentMenuAdd();

            var isEmergencyReg =
                RegistrationCurrent.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient;

            //"UDD" 
            PopulateMenuUDD(isEmergencyReg);

            // Karena semua registrasi non IP bisa direfer ke IP maka entrian HAIS tetap bisa diakses (Handono 2303 RSYS)
            //var hais = tbMenu.FindItemByText("HAIs Monitoring");
            //hais.Enabled = isEmergencyReg;

            // Progress Notes
            var prgnotes = tbarAssessment.FindItemByText("Progress Notes");
            prgnotes.Enabled = isEmergencyReg && IsUserInParamedicTeam(RegistrationCurrent);

            // Resuem medis
            var tbMds = tbarAssessment.FindItemByValue("ResumeMedis");
            if (AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor)
            {
                if (AppParameter.IsYes(AppParameter.ParameterItem.IsAllPhysicianAllowEditMedicalDischarge))
                    tbMds.Enabled = IsUserInParamedicTeam();
                else
                    tbMds.Enabled = IsUserParamedicDpjpOrSharing();
            }
            else
                tbMds.Enabled = false;

            //medicalHistCtl
            medicalHistCtl.IsNewPatient = IsNewPatient;
            medicalHistCtl.IsClosed = IsClosed;
            medicalHistCtl.IsUserAddAble = IsUserAddAble;
            medicalHistCtl.IsUserEditAble = IsUserEditAble;

            // Exam Order
            examOrderHistCtl.IsNewPatient = IsNewPatient;
            examOrderHistCtl.IsClosed = IsClosed;
            examOrderHistCtl.IsUserAddAble = IsUserAddAble;
            examOrderHistCtl.IsUserEditAble = IsUserEditAble;

            surgicalHistCtl.IsNewPatient = IsNewPatient;
            surgicalHistCtl.IsClosed = IsClosed;
            surgicalHistCtl.IsUserAddAble = IsUserAddAble;
            surgicalHistCtl.IsUserEditAble = IsUserEditAble;
            surgicalHistCtl.ReferFromRegistrationNo = ReferFromRegistrationNo;

            healthRecordHistCtl.IsUserAddAble = IsUserAddAble;
            healthRecordHistCtl.IsUserEditAble = IsUserEditAble;

            nursingCareCtl.IsUserAddAble = IsUserAddAble;
            nursingCareCtl.IsUserEditAble = IsUserEditAble;
        }

        private void PopulateMenuUDD(bool isEmergencyReg)
        {
            var tbUdd = (RadToolBarDropDown)tbMenu.FindItemByText("UDD");
            tbUdd.Buttons.Clear();

            var tbtn = new RadToolBarButton("Service Unit Kardex")
            {
                Value = "MedicationHist",
                Enabled = isEmergencyReg
            };
            tbUdd.Buttons.Add(tbtn);


            tbtn = new RadToolBarButton("Drug Maintenance & Review")
            {
                Value = "udd_maintenance",
                Enabled = IsUserEditAble && IsUserInParamedicTeam(RegistrationCurrent)
            };
            tbUdd.Buttons.Add(tbtn);

            tbtn = new RadToolBarButton { IsSeparator = true };
            tbUdd.Buttons.Add(tbtn);

            tbtn = new RadToolBarButton("Drug Acceptance from Patient")
            {
                Value = "drugfrom_patient",
                Enabled = IsUserEditAble
            };
            tbUdd.Buttons.Add(tbtn);

            tbtn = new RadToolBarButton("Drug Acceptance from Service Unit Tansaction")
            {
                Value = "drugfrom_serviceunit",
                Enabled = IsUserEditAble && isEmergencyReg
            };
            tbUdd.Buttons.Add(tbtn);

            tbtn = new RadToolBarButton
            {
                IsSeparator = true
            };
            tbUdd.Buttons.Add(tbtn);

            tbtn = new RadToolBarButton("Admission Drug Reconciliation")
            {
                Value = "adm_recon",
                Enabled = IsUserEditAble
            };
            tbUdd.Buttons.Add(tbtn);

            tbtn = new RadToolBarButton("Transfer Drug Reconciliation")
            {
                Value = "trf_recon",
                Enabled = IsUserEditAble && isEmergencyReg
            };
            tbUdd.Buttons.Add(tbtn);

            tbtn = new RadToolBarButton("Discharge Drug Reconciliation")
            {
                Value = "dcg_recon",
                Enabled = IsUserEditAble && isEmergencyReg
            };
            tbUdd.Buttons.Add(tbtn);
            tbtn = new RadToolBarButton
            {
                IsSeparator = true
            };
            tbUdd.Buttons.Add(tbtn);

            tbtn = new RadToolBarButton("Medication Setup Status")
            {
                Value = "udd_setup",
                Enabled = IsUserEditAble && isEmergencyReg
            };
            tbUdd.Buttons.Add(tbtn);

            tbtn = new RadToolBarButton("Medication Verification Status")
            {
                Value = "udd_verification",
                Enabled = IsUserEditAble && isEmergencyReg
            };
            tbUdd.Buttons.Add(tbtn);

            tbtn = new RadToolBarButton("Medication Realization Status")
            {
                Value = "udd_realization",
                Enabled = IsUserEditAble && isEmergencyReg
            };
            tbUdd.Buttons.Add(tbtn);
        }


        protected void PopulateAssessmentMenuAdd()
        {
            // List Asesmen untuk selain rawat inap diambil dari ServiceUnit dan ditambah dari ServiceUnitAssessmentType
            // Form ini khusus untuk entry selain rawat inap

            var tbarItemAdd = (RadToolBarDropDown)tbarAssessment.Items[0];
            tbarItemAdd.Enabled = true;
            tbarItemAdd.Buttons.Clear();

            if (!IsUserEntryAssessment())
            {
                tbarItemAdd.Enabled = false;
                return;
            }

            var su = new ServiceUnit();
            su.LoadByPrimaryKey(RegistrationCurrent.ServiceUnitID);
            var assessmentType = su.SRAssessmentType;

            // Add Default Assessment
            var stdi = new AppStandardReferenceItem();
            if (!string.IsNullOrEmpty(assessmentType))
            {
                stdi.LoadByPrimaryKey("AssessmentType", assessmentType);

                var btn = new RadToolBarButton(stdi.ItemName)
                {
                    Value = string.Format("addasses_{0}", assessmentType)
                };

                // IsSingleEntry
                var astp = new AppSRAssessmentType();
                if (astp.LoadByPrimaryKey(assessmentType) && astp.IsSingleEntry == true)
                {
                    var pass = new PatientAssessment();
                    var passq = new PatientAssessmentQuery();
                    passq.Where(passq.RegistrationNo == RegistrationNo, passq.SRAssessmentType == assessmentType, passq.Or(passq.IsDeleted.IsNull(), passq.IsDeleted == false));
                    passq.es.Top = 1;

                    if (pass.Load(passq))
                    {
                        btn.Enabled = false;
                    }
                }
                tbarItemAdd.Buttons.Add(btn);
            }


            // Add other Assessment
            var astype = new ServiceUnitAssessmentTypeQuery();
            astype.Where(astype.ServiceUnitID == ServiceUnitID);
            var dtbOth = astype.LoadDataTable();
            if (dtbOth != null && dtbOth.Rows != null && dtbOth.Rows.Count > 0)
            {
                foreach (DataRow row in dtbOth.Rows)
                {
                    if (!assessmentType.Equals(row["SRAssessmentType"]))
                    {
                        stdi = new AppStandardReferenceItem();
                        if (!stdi.LoadByPrimaryKey("AssessmentType", row["SRAssessmentType"].ToString()))
                            continue;

                        var btn = new RadToolBarButton(stdi.ItemName)
                        {
                            Value = string.Format("addasses_{0}", row["SRAssessmentType"])
                        };

                        // IsSingleEntry
                        var astp = new AppSRAssessmentType();
                        if (astp.LoadByPrimaryKey(row["SRAssessmentType"].ToString()) && astp.IsSingleEntry == true)
                        {

                            var pass = new PatientAssessment();
                            var passq = new PatientAssessmentQuery();
                            passq.Where(passq.RegistrationNo == RegistrationNo,
                                passq.SRAssessmentType == row["SRAssessmentType"]);
                            passq.es.Top = 1;

                            if (pass.Load(passq))
                            {
                                btn.Enabled = false;
                            }
                        }

                        tbarItemAdd.Buttons.Add(btn);
                    }
                }
            }

            // Default SOAP Entry // Diremark masih errror
            //if (tbarItemAdd.Buttons.Count == 0)
            //{
            //    var btnSoap = new RadToolBarButton("SOAP")
            //    {
            //        Value = string.Format("addasses_{0}", "SOAP")
            //    };
            //    tbarItemAdd.Buttons.Add(btnSoap);
            //}
        }

        private void ShowPrintPreview()
        {
            var url = Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx");
            Helper.ShowRadWindowAfterPostback(winPrintPreview, url, "preview", true);
        }

        private void PopulateRegistrationInfo()
        {
            var reg = RegistrationCurrent;

            var pat = PatientCurrent;

            PatientName = pat.PatientName;
            IsClosed = reg.IsClosed ?? false;

            lblMedicalNo.Text = pat.MedicalNo;
            imgRip.ImageUrl = (pat.IsAlive ?? false) ? string.Empty : "~/Images/Rip16.png";
            lblRegistrationNo.Text = reg.RegistrationNo;
            lblRegistrationDate.Text = Convert.ToDateTime(reg.RegistrationDate).ToString(AppConstant.DisplayFormat.DateShortMonth);

            var stdGender = new AppStandardReferenceItem();
            if (stdGender.LoadByPrimaryKey(AppEnum.StandardReference.GenderType.ToString(), pat.Sex))
                lblGender.Text = stdGender.ItemName;
            else
                lblGender.Text = string.Empty;

            lblDateOfBirth.Text = (pat.IsAlive ?? true) ? string.Format("{0} ({1}y {2}m {3}d)", (pat.DateOfBirth ?? new DateTime()).ToString(AppConstant.DisplayFormat.DateShortMonth),
                Helper.GetAgeInYear(pat.DateOfBirth ?? DateTime.Now.Date), Helper.GetAgeInMonth(pat.DateOfBirth ?? DateTime.Now.Date), Helper.GetAgeInDay(pat.DateOfBirth ?? DateTime.Now.Date)) : string.Format("{0} ({1}y {2}m {3}d)", (pat.DateOfBirth ?? new DateTime()).ToString(AppConstant.DisplayFormat.DateShortMonth),
                Helper.GetAgeInYear(pat.DateOfBirth ?? DateTime.Now.Date, pat.DeceasedDateTime ?? DateTime.Now.Date), Helper.GetAgeInMonth(pat.DateOfBirth ?? DateTime.Now.Date, pat.DeceasedDateTime ?? DateTime.Now.Date), Helper.GetAgeInDay(pat.DateOfBirth ?? DateTime.Now.Date, pat.DeceasedDateTime ?? DateTime.Now.Date));

            lblPhysician.Text = ParamedicName(RegistrationNo, RegistrationCurrent.ParamedicID);

            /**
             * Last Ranap Date
             */
            DateTime? latestRanapDate = LatestInpatientRegistrationDate();
            lblTglRanap.Text = latestRanapDate?.ToString(AppConstant.DisplayFormat.DateShortMonth) ?? "-";

            hdnGuarantorCardNo.Value = reg.GuarantorCardNo;

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);
            lblGuarantor.Text = grr.GuarantorName;
            trBpjsSepNo.Visible = grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeBPJS;
            if (trBpjsSepNo.Visible)
            {
                lblBpjsSepNo.Text = reg.BpjsSepNo;
                // imel 11 Feb 2025 untuk pengambilan data saya modif sbagai berikut, tanpa melihat apakah sudah data di BPJSSEP
                if (Helper.IsBpjsIcareIntegration)
                {
                    if (!string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID))
                    {
                        var pb = new ParamedicBridging();
                        pb.Query.Where(pb.Query.ParamedicID == AppSession.UserLogin.ParamedicID && pb.Query.SRBridgingType == AppEnum.BridgingType.BPJS.ToString());
                        if (pb.Query.Load() && reg.ParamedicID == pb.ParamedicID)
                        {
                            hdnPhysicianID.Value = pb.BridgingID;
                            tbMenu.Items.FindItemByValue("icare").Visible = true;
                        }
                    }
                } // ending imel

                //if (!string.IsNullOrWhiteSpace(reg.BpjsSepNo))
                //{
                //    var bs = new BpjsSEP();
                //    bs.Query.es.Top = 1;
                //    bs.Query.OrderBy(bs.Query.TanggalSEP.Descending);
                //    if (bs.LoadByPrimaryKey(reg.BpjsSepNo))
                //    {
                //        hdnPhysicianID.Value = bs.KodeDpjpPelayanan;
                //        if (Helper.IsBpjsIcareIntegration)
                //        {
                //            if (!string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID))
                //            {
                //                var pb = new ParamedicBridging();
                //                pb.Query.Where(pb.Query.ParamedicID == AppSession.UserLogin.ParamedicID && pb.Query.SRBridgingType == AppEnum.BridgingType.BPJS.ToString());
                //                if (pb.Query.Load() && pb.BridgingID == bs.KodeDpjpPelayanan)
                //                {
                //                    tbMenu.Items.FindItemByValue("icare").Visible = true;
                //                }
                //            }
                //        }
                //    }
            }
            if (trBpjsSepNo.Visible && !string.IsNullOrWhiteSpace(reg.BpjsSepNo))
            {
                var bsq = new BpjsSEPQuery("bsq");
                bsq.Where(bsq.NoSEP == reg.BpjsSepNo);
                bsq.es.Top = 1;
                bsq.Load();
                var bs = new BpjsSEP();
                bs.Query.Select(bs.Query.TanggalRujukan);
                bs.Load(bsq);
                trTglRujukan.Visible = !string.IsNullOrWhiteSpace(bs.TanggalRujukan.ToString());
                if (trTglRujukan.Visible)
                    lblTglRujukan.Text = bs.TanggalRujukan.Value.AddDays(90).ToString("yyyy-MM-dd");
            }
            else
                trTglRujukan.Visible = false;


            trCovClass.Visible = !string.IsNullOrWhiteSpace(reg.CoverageClassID);
            if (trCovClass.Visible)
            {
                var covClass = new Class();
                if (covClass.LoadByPrimaryKey(reg.CoverageClassID))
                    lblCovClass.Text = covClass.ClassName;
            }

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(ServiceUnitID);
            lblServiceUnit.Text = unit.ServiceUnitName;

            lblChronicDisease.Text = Patient.ChronicDisease(PatientID);
            if (string.IsNullOrEmpty(lblChronicDisease.Text))
                divChronicDisease.Style["Display"] = "none";
            else
                divChronicDisease.Attributes.Remove("Display");

            if (!(string.IsNullOrEmpty(reg.SRPatientRiskStatus) || reg.SRPatientRiskStatus == "X"))
            {
                var stRisk = new AppStandardReferenceItem();
                if (stRisk.LoadByPrimaryKey("PatientRiskStatus", reg.str.SRPatientRiskStatus))
                {
                    lblPatientRiskStatus.Text = stRisk.ItemName;
                    divPatientRiskStatus.Visible = true;

                    if (reg.SRPatientRiskStatus == "0")
                        divPatientRiskStatus.Style.Add("background-color", "green");
                    else if (reg.SRPatientRiskStatus == "1")
                    {
                        divPatientRiskStatus.Style.Add("background-color", "yellow");
                        divPatientRiskStatus.Style.Add("color", "darkgrey");
                    }
                }
                else
                {
                    lblPatientRiskStatus.Text = string.Empty;
                    divPatientRiskStatus.Visible = false;
                }
            }
            else
            {
                lblPatientRiskStatus.Text = string.Empty;
                divPatientRiskStatus.Visible = false;
            }

            if (!string.IsNullOrEmpty(reg.SRPatientRiskColor))
            {
                var stRiskColor = new AppStandardReferenceItem();
                if (stRiskColor.LoadByPrimaryKey("PatientRiskColor", reg.str.SRPatientRiskColor))
                {
                    lblPatientRiskColor.Text = stRiskColor.ItemName;
                    divPatientRiskColor.Visible = true;
                    divPatientRiskColor.Style.Add("background-color", stRiskColor.ReferenceID);
                }
                else
                {
                    lblPatientRiskColor.Text = string.Empty;
                    divPatientRiskColor.Visible = false;
                }
            }

            lblClinicalPathway.Text = Registration.GetRegistrationPathwayName(RegistrationNo);
            if (!AppSession.Parameter.ClinicalPathwayRegistrationType.Contains(RegistrationType))
                divClinicalPathway.Style["Display"] = "none";
            else
                divClinicalPathway.Attributes.Remove("Display");

            PopulatePatientAllergy();
            PopulateEpisodeDiagnose();
            PopulateImmunizationHistory();
            PopulatePatientDialysis();

            //DIpindah ke webservice update (Handono 230329)
            //litPlafond.Text = Temiang.Avicenna.Module.RADT.Cpoe.EmrList.PlafondProgress(RegistrationNo, true);

            PopulatePatientImage(PatientID);
        }

        #region latest ranap
        private DateTime? LatestInpatientRegistrationDate()
        {
            var pars = new esParameters();
            pars.Add("MedicalNo", PatientCurrent.MedicalNo);
            var dtLatestRanap = BusinessObject.Common.Utils.LoadDataTableFromStoreProcedure("sp_GetLatestIPRRegistration", pars, 0);
            if (dtLatestRanap.Rows.Count > 0)
            {
                DataRow row = dtLatestRanap.Rows[0];
                DateTime latestRegistration = DateTime.Parse(row["RegistrationDate"].ToString());
                return latestRegistration;
            }
            return null;
        }

        #endregion

        private string ParamedicName(string registrationNo, string paramedicID)
        {
            // Paramedic
            var medic = new Paramedic();
            var qrparteam = new ParamedicTeamQuery("pt");
            qrparteam.Where(qrparteam.RegistrationNo == registrationNo);
            qrparteam.es.Top = 1;
            if ((new ParamedicTeam().Load(qrparteam)))
            {
                var parteams = new ParamedicTeamCollection();
                parteams.Query.Where(parteams.Query.RegistrationNo == registrationNo);
                parteams.Query.OrderBy(parteams.Query.SRParamedicTeamStatus.Ascending);
                parteams.LoadAll();
                var strBld = new StringBuilder();
                var i = 1;
                foreach (var parteam in parteams)
                {
                    medic = new Paramedic();
                    if (medic.LoadByPrimaryKey(parteam.ParamedicID))
                    {
                        strBld.AppendFormat("{0}. {1}<br />", i, medic.ParamedicName);
                        i++;
                    }
                }
                return strBld.ToString();
            }

            // Tampilkan DPJP nya
            medic.LoadByPrimaryKey(paramedicID);
            return medic.ParamedicName;
        }

        private void PopulatePatientAllergy()
        {
            var paQ = new PatientAllergyQuery("a");
            var asriQ = new AppStandardReferenceItemQuery("b");
            paQ.LeftJoin(asriQ).On(paQ.AllergyGroup == asriQ.StandardReferenceID
                && asriQ.StandardReferenceID == paQ.AllergyGroup
                && asriQ.ItemName == paQ.SRAllergyCategory);
            paQ.Select(paQ.AllergenName, paQ.Allergen, paQ.DescAndReaction, paQ.AllergyGroup, paQ.SRAnaphylaxis, asriQ.ItemName);
            paQ.Where(paQ.PatientID == PatientID);
            var dtbPatientAllergy = paQ.LoadDataTable();

            var sb = new StringBuilder();
            sb.AppendLine("<table style='width:100%'>");
            foreach (DataRow dataRow in dtbPatientAllergy.Rows)
            {
                var allergyGroup = dataRow["AllergyGroup"] != DBNull.Value ? dataRow["AllergyGroup"].ToString() : string.Empty;
                var ItemName = dataRow["Allergen"] != DBNull.Value ? dataRow["Allergen"].ToString() : string.Empty;

                var asri = new AppStandardReferenceItem();
                asri.LoadByPrimaryKey(allergyGroup, ItemName);

                if (asri.ReferenceID == AppEnum.StandardReference.PatientHealthRecord.ToString())
                {
                    sb.AppendLine("<tr>");
                    sb.AppendFormat("<td style='width:100px;font-weight: bold;'>{0}</td>", dataRow["AllergenName"]);
                    sb.AppendLine("<td style='width:2px'>:</td>");
                    sb.AppendFormat("<td style='color: red;'>{0}</td>", dataRow["DescAndReaction"]);
                    sb.AppendLine("</tr>");
                }
            }
            sb.AppendLine("</table>");

            if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["SatuSehatOrganizationID"]))
            {
                paQ.Select(paQ.AllergenName, paQ.DescAndReaction, paQ.SRAllergyCategory, paQ.AllergyGroup, paQ.SRAnaphylaxis);
                paQ.Where(paQ.PatientID == PatientID);
                var dtb = paQ.LoadDataTable();

                var refQ = new AppStandardReferenceItemQuery("b");
                refQ.Select(refQ.ItemID, refQ.ItemName);
                refQ.Where(refQ.StandardReferenceID == "AllergyCategory", refQ.ItemID == "AllergyCategory-002");
                var dtbReferenceItem = refQ.LoadDataTable();

                var query =
                    from allergy in dtb.AsEnumerable()
                    join reference in dtbReferenceItem.AsEnumerable()
                    on allergy["SRAllergyCategory"] equals reference["ItemName"]
                    select new
                    {
                        AllergyCategory = reference["ItemName"].ToString(),
                        AllergenName = allergy["AllergenName"].ToString(),
                        AllergenDesc = allergy["DescAndReaction"] != DBNull.Value ? allergy["DescAndReaction"].ToString() : string.Empty,
                    };

                sb.AppendLine("<table style='width:100%;'>");
                foreach (var row in query)
                {
                    sb.AppendLine("<tr>");
                    sb.AppendLine("<td style='width:100px; font-weight: bold; vertical-align: top;'>Medication</td>");
                    sb.AppendLine("<td style='width:2px; vertical-align: top;'>:</td>");
                    sb.AppendFormat("<td style='color: red; word-wrap: break-word; display: inline-block; width: calc(100% - 102px);'>{0}</td>", row.AllergenName);
                    sb.AppendLine("</tr>");
                    sb.AppendLine("<tr>");
                    sb.AppendFormat("<td style='width:100px; font-weight: bold;'>Description</td>");
                    sb.AppendLine("<td style='width:2px'>:</td>");
                    sb.AppendFormat("<td style='color: red;'>{0}</td>", row.AllergenDesc);
                    sb.AppendLine("</tr>");
                }
                sb.AppendLine("</table>");
            }

            litPatientAllergy.Text = sb.ToString();
        }

        private void PopulateEpisodeDiagnose()
        {
            cpnDiagnosis.Title = "Diagnosis";
            litDiagnosis.Text = EpisodeDiagnose.DiagnoseSummaryHtml(RegistrationNo);
        }

        private void PopulateImmunizationHistory()
        {
            var piQ = new PatientImmunizationQuery("a");
            var imQ = new ImmunizationQuery("b");
            piQ.InnerJoin(imQ).On(imQ.ImmunizationID == piQ.ImmunizationID);
            piQ.Select(piQ.ImmunizationNo, imQ.ImmunizationName, piQ.ImmunizationDate);
            piQ.Where(piQ.PatientID == PatientID);
            piQ.OrderBy(imQ.IndexNo.Ascending, piQ.ImmunizationNo.Ascending);
            var dtb = piQ.LoadDataTable();
            var sb = new StringBuilder();
            sb.AppendLine("<table style='width:100%'>");
            foreach (DataRow dataRow in dtb.Rows)
            {
                sb.AppendLine("<tr>");
                sb.AppendFormat("<td style='width:100px;font-weight: bold;'>{0} {1}</td><td style='width:5px'>:</td><td>{2}</td>", dataRow["ImmunizationName"], dataRow["ImmunizationNo"], dataRow["ImmunizationDate"] == DBNull.Value ? string.Empty : ((DateTime)dataRow["ImmunizationDate"]).ToString("MMMM yyyy"));
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("</table>");
            litPatientImunization.Text = sb.ToString();
        }

        private void PopulatePatientDialysis()
        {
            var pd = new PatientDialysis();
            if (pd.LoadByPrimaryKey(PatientID))
            {
                var sb1 = new StringBuilder();
                sb1.AppendLine("<table style='width:100%'>");
                AppendRow(sb1, "Initial Diagnosis", pd.InitialDiagnosis);
                AppendRow(sb1, "Referring Hospital", pd.RefferingHospital);
                AppendRow(sb1, "Referring Physician", pd.RefferingPhysician);
                sb1.AppendLine("</table>");
                litPatientDialysisGeneralInfo.Text = sb1.ToString();

                var sb2 = new StringBuilder();
                sb2.AppendLine("<table style='width:100%'>");
                AppendRowTwoValue(sb2, "Hb", pd.Hb, pd.HbDate?.ToString(AppConstant.DisplayFormat.DateShortMonth));
                AppendRowTwoValue(sb2, "Urea", pd.Urea, pd.UreaDate?.ToString(AppConstant.DisplayFormat.DateShortMonth));
                AppendRowTwoValue(sb2, "Creatinine", pd.Creatinine, pd.CreatinineDate?.ToString(AppConstant.DisplayFormat.DateShortMonth));
                AppendRowTwoValue(sb2, "HBsAg", pd.HBsAg, pd.HBsAgDate?.ToString(AppConstant.DisplayFormat.DateShortMonth));
                AppendRowTwoValue(sb2, "Anti HCV", pd.AntiHCV, pd.AntiHCVDate?.ToString(AppConstant.DisplayFormat.DateShortMonth));
                AppendRowTwoValue(sb2, "Anti HIV", pd.AntiHIV, pd.AntiHIVDate?.ToString(AppConstant.DisplayFormat.DateShortMonth));
                AppendRowTwoValue(sb2, "Kidney Ultrasound", pd.KidneyUltrasound, pd.KidneyUltrasoundDate?.ToString(AppConstant.DisplayFormat.DateShortMonth));
                AppendRowTwoValue(sb2, "ECHO", pd.ECHO, pd.ECHODate?.ToString(AppConstant.DisplayFormat.DateShortMonth));
                sb2.AppendLine("</table>");
                litPatientDialysisExamination.Text = sb2.ToString();

                var sb3 = new StringBuilder();
                sb3.AppendLine("<table style='width:100%'>");
                AppendRow(sb3, "First Hemodialysis Date", pd.FirstHDDate?.ToString(AppConstant.DisplayFormat.DateShortMonth));
                AppendRow(sb3, "Date of transfer to HD ", pd.TransferHDDate?.ToString(AppConstant.DisplayFormat.DateShortMonth));
                sb3.AppendLine("</table>");
                litPatientDialysisHemodialisa.Text = sb3.ToString();

                var sb4 = new StringBuilder();
                sb4.AppendLine("<table style='width:100%'>");
                AppendRow(sb4, "First Peritoneal Dialysis Date", pd.FirstPDDate?.ToString(AppConstant.DisplayFormat.DateShortMonth));
                AppendRow(sb4, "Date of transfer to PD", pd.TransferPDDate?.ToString(AppConstant.DisplayFormat.DateShortMonth));
                sb4.AppendLine("</table>");
                litPatientDialysisPeritoneal.Text = sb4.ToString();

                var sb5 = new StringBuilder();
                sb5.AppendLine("<table style='width:100%'>");
                AppendRow(sb5, "Kidney Transplant Date", pd.KidneyTransplantDate?.ToString(AppConstant.DisplayFormat.DateShortMonth));
                sb5.AppendLine("</table>");
                litPatientDialysisKidney.Text = sb5.ToString();
            }
        }

        private void AppendRowTwoValue(StringBuilder sb, string label, string value1, string value2)
        {
            if (!string.IsNullOrEmpty(value1) || !string.IsNullOrEmpty(value2))
            {
                sb.AppendLine("<tr>");
                sb.AppendFormat("<td style='width:100px;font-weight: bold;'>{0}</td><td style='width:5px'>:</td><td>{1} ({2})</td>", label, value1, value2);
                sb.AppendLine("</tr>");
            }
        }

        private void AppendRow(StringBuilder sb, string label, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                sb.AppendLine("<tr>");
                sb.AppendFormat("<td style='width:100px;font-weight: bold;'>{0}</td><td style='width:5px'>:</td><td>{1}</td>", label, value);
                sb.AppendLine("</tr>");
            }
        }

        #region Integrated Notes History Grid
        protected void grdAssessment_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridGroupHeaderItem)
            {
                // Menghilangkan control group expand collapse
                (e.Item as GridGroupHeaderItem).Cells[0].Controls.Clear();
            }
            else if (e.Item is GridNestedViewItem)
            {
                (e.Item as GridNestedViewItem).Cells[0].Controls.Clear();
            }
        }
        protected void grdAssessment_ItemCommand(object source, GridCommandEventArgs e)
        {
            // Call from NestedViewTemplate
            if (!(e.CommandSource is LinkButton)) return;
            var selectedKey = ((LinkButton)(e.CommandSource)).CommandArgument;
            if (string.IsNullOrEmpty(selectedKey)) return;
            //if (e.CommandName == "Print")
            //{
            //    PrintAssessment(selectedKey);
            //}
            //else if (e.CommandName == "Approve")
            //{
            //    EmrIpDetail.ApproveIntegratedNote(selectedKey, RegistrationNo, ReferFromRegistrationNo);

            //    // Refresh
            //    grdAssessment.DataSource = null;
            //    grdAssessment.Rebind();
            //}
        }

        //private void PrintAssessment(string keyValue)
        //{
        //    var jobParameters = new PrintJobParameterCollection();
        //    if (string.IsNullOrEmpty(AssessmentType))
        //    {
        //        // Print from EpisodeSoape
        //        // PrintEpisodeSoap(keyValue, jobParameters);
        //        PrintIntegratedNoteSoap(jobParameters, keyValue);
        //    }
        //    else
        //    {
        //        // Check is Assessment
        //        var asses = new PatientAssessment();
        //        asses.Query.Where(asses.Query.RegistrationInfoMedicID == keyValue);
        //        if (asses.Load(asses.Query))
        //        {
        //            PrintAssessment(jobParameters, asses);
        //        }
        //        else
        //        {
        //            PrintIntegratedNoteSoap(jobParameters, keyValue);
        //        }
        //    }
        //    if (jobParameters.Count > 0)
        //        ShowPrintPreview();

        //}

        //private void PrintAssessment(PrintJobParameterCollection jobParameters, PatientAssessment asses)
        //{

        //    if (asses.IsInitialAssessment ?? false)
        //    {
        //        switch (asses.SRAssessmentType)
        //        {
        //            case "SURGI": // 1. asesmen bedah
        //                AppSession.PrintJobReportID = AppConstant.Report.InitialAssessment.OutPatient.Surgery;
        //                break;
        //            case "DENTS": // 2. asesmen gigi
        //                AppSession.PrintJobReportID = AppConstant.Report.InitialAssessment.OutPatient.Dentis;
        //                break;
        //            case "EYE": // 3. asesmen mata
        //                AppSession.PrintJobReportID = AppConstant.Report.InitialAssessment.OutPatient.Eye;
        //                break;
        //            case "PSYCY": // 4. asesmen Psikiatri
        //                AppSession.PrintJobReportID = AppConstant.Report.InitialAssessment.OutPatient.Psychiatry;
        //                break;
        //            case "KID": // 5. Asessmen Awal Rawat Inap Anak
        //                AppSession.PrintJobReportID = AppConstant.Report.InitialAssessment.OutPatient.Kid;
        //                break;
        //            case "HEART": // 6. asesmen jantung
        //                AppSession.PrintJobReportID = AppConstant.Report.InitialAssessment.OutPatient.Heart;
        //                break;
        //            case "INTER": //"INTERNAL": // 7. asesmen rajal PD  etc
        //                AppSession.PrintJobReportID = AppConstant.Report.InitialAssessment.OutPatient.Internal;
        //                break;
        //            case "LUNG": // 8. asesmen paru
        //                AppSession.PrintJobReportID = AppConstant.Report.InitialAssessment.OutPatient.Lung;
        //                break;
        //            case "THT": // 9. asesmen THT
        //                AppSession.PrintJobReportID = AppConstant.Report.InitialAssessment.OutPatient.Tht;
        //                break;
        //            case "NURSE": // 10. asesmen Kandungan
        //                AppSession.PrintJobReportID = AppConstant.Report.InitialAssessment.OutPatient.Nursing;
        //                break;
        //            case "PKAND": // 10. asesmen Kandungan
        //                AppSession.PrintJobReportID = AppConstant.Report.InitialAssessment.OutPatient.Gynecology;
        //                break;
        //            case "NEURO": //"NEUROLOGI": // 11. asesmen syaraf
        //                AppSession.PrintJobReportID = AppConstant.Report.InitialAssessment.OutPatient.Neurology;
        //                break;
        //            case "SKIN": // 12. asesmen Kulit fix
        //                AppSession.PrintJobReportID = AppConstant.Report.InitialAssessment.OutPatient.Skin;
        //                break;
        //            case "GENRL":
        //                AppSession.PrintJobReportID = AppConstant.Report.InitialAssessment.OutPatient.General;
        //                break;
        //            case "PSYCO":
        //                AppSession.PrintJobReportID = AppConstant.Report.InitialAssessment.OutPatient.Psychology;
        //                break;
        //            case "NUTRI":
        //                AppSession.PrintJobReportID = AppConstant.Report.InitialAssessment.OutPatient.Nutrient;
        //                break;
        //            case "REHAB":
        //                AppSession.PrintJobReportID = AppConstant.Report.InitialAssessment.OutPatient.Rehabilitation;
        //                break;
        //            case "IGD":
        //                AppSession.PrintJobReportID = AppConstant.Report.InitialAssessment.OutPatient.Igd;
        //                break;
        //        }
        //    }
        //    else
        //        switch (asses.SRAssessmentType)
        //        {
        //            case "DENTS": // 2. asesmen gigi
        //                AppSession.PrintJobReportID = AppConstant.Report.ContinuedAssessment.Dentis;
        //                break;
        //            case "NURSE": // 10. asesmen Kebidanan
        //                AppSession.PrintJobReportID = AppConstant.Report.ContinuedAssessment.Nursing;
        //                break;
        //            case "PKAND": // 10. asesmen Penyakit Kandungan
        //                AppSession.PrintJobReportID = AppConstant.Report.ContinuedAssessment.Gynecology;
        //                break;
        //            case "GENRL":
        //                AppSession.PrintJobReportID = AppConstant.Report.ContinuedAssessment.General;
        //                break;
        //            default:
        //                AppSession.PrintJobReportID = AppConstant.Report.ContinuedAssessment.General;
        //                break;
        //        }

        //    jobParameters.AddNew("PatientID", asses.PatientID);
        //    jobParameters.AddNew("SRAssessmentType", asses.SRAssessmentType);
        //    jobParameters.AddNew("RegistrationNo", asses.RegistrationNo);
        //    jobParameters.AddNew("RegistrationInfoMedicID", asses.RegistrationInfoMedicID);
        //    AppSession.PrintJobParameters = jobParameters;

        //}

        //private void PrintIntegratedNoteSoap(PrintJobParameterCollection jobParameters, string keyValue)
        //{
        //    // Print from Integrated Note
        //    var jobParameter = jobParameters.AddNew();
        //    jobParameter.Name = "RegistrationNo";
        //    jobParameter.ValueString = keyValue;

        //    var jobParameter2 = jobParameters.AddNew();
        //    jobParameter2.Name = "SequenceNo";
        //    jobParameter2.ValueString = "";

        //    AppSession.PrintJobParameters = jobParameters;
        //    AppSession.PrintJobReportID = AppConstant.Report.SOAP;

        //}

        //private void PrintEpisodeSoap(string keyValue, PrintJobParameterCollection jobParameters)
        //{
        //    var keyValues = keyValue.Split('_');
        //    if (keyValues.Length > 0)
        //    {
        //        var jobParameter = jobParameters.AddNew();
        //        jobParameter.Name = "RegistrationNo";
        //        jobParameter.ValueString = keyValues[0];

        //        var jobParameter2 = jobParameters.AddNew();
        //        jobParameter2.Name = "SequenceNo";
        //        jobParameter2.ValueString = keyValues[1];

        //        AppSession.PrintJobParameters = jobParameters;
        //        AppSession.PrintJobReportID = AppConstant.Report.SOAP;

        //    }
        //}

        protected void grdAssessment_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            // Void / Unvoid
            // Call from NestedViewTemplate
            if (!(e.CommandSource is LinkButton)) return;
            var selectedKey = ((LinkButton)(e.CommandSource)).CommandArgument;
            if (string.IsNullOrEmpty(selectedKey)) return;

            var entity = new RegistrationInfoMedic();
            var keys = selectedKey.Split('_');
            if (entity.LoadByPrimaryKey(keys[0]))
            {
                // Check edit expired
                var messageOfDeadlineEditedOver = Helper.MessageOfDeadlineEditedOver(entity.DateTimeInfo.Value);
                if (!string.IsNullOrWhiteSpace(messageOfDeadlineEditedOver))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "invalid", string.Format("alert('{0}');", messageOfDeadlineEditedOver), true);
                    return;
                }

                // Check void reg status utk proses unvoid
                if (entity.IsDeleted == true)
                {
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(entity.RegistrationNo);
                    if (reg.IsVoid ?? false)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "invalid", string.Format("alert('{0}');", "Registration has void, cannot unvoid this data"), true);
                        return;
                    }
                }

                entity.IsDeleted = !(entity.IsDeleted ?? false);
                entity.Save();

                var pa = new PatientAssessment();
                if (pa.LoadByPrimaryKey(keys[0]))
                {
                    var epds = new EpisodeDiagnoseCollection();
                    epds.Query.Where(epds.Query.RegistrationNo == pa.RegistrationNo);
                    if (epds.LoadAll())
                    {
                        foreach (var epd in epds)
                        {
                            epd.IsVoid = entity.IsDeleted;
                        }
                        epds.Save();
                    }

                    pa.IsDeleted = entity.IsDeleted;
                    pa.Save();
                }

            }

            // Refresh
            grdAssessment.DataSource = null;
            grdAssessment.Rebind();

        }

        protected void grdAssessment_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdAssessment.DataSource = null;
            grdAssessment.DataSource = EmrIpDetail.RegistrationInfoMedicDataTable(RegistrationType, RegistrationNo, MergeRegistrations, PatientID, string.Empty);
            PopulateEpisodeDiagnose();
            PopulatePatientAllergy();
            PopulateImmunizationHistory();
            PopulatePatientDialysis();
        }
        #endregion

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            switch (eventArgument)
            {
                case "allergy":
                    PopulatePatientAllergy();
                    break;
                case "diagnose":
                    PopulateEpisodeDiagnose();
                    break;
                case "immunization":
                    PopulateImmunizationHistory();
                    break;
                case "dialysis":
                    PopulatePatientDialysis();
                    break;
            }

            // Save Photo
            if (sourceControl is Button && (sourceControl as Button).UniqueID == btnSavePhoto.UniqueID)
            {
                // Sumber data image tersimpan di session
                RegistrationDetail.SavePatientImage(PatientID, hdnImgData.Value);

                //Load
                //CaptureImageFile = null; // Reset
                PopulatePatientImage(PatientID);
            }
        }



        #region PatientImage
        private void PopulatePatientImage(string patientID)
        {
            // Patient Photo
            imgPatientPhoto.ImageUrl = string.Empty;
            //if (!string.IsNullOrEmpty(CaptureImageFile))
            //{
            //    // Load form webcam capture
            //    var capturedImageFileArgs = CaptureImageFile.Split('|');
            //    var capturedImageFile = capturedImageFileArgs[0];
            //    if (Convert.ToBoolean(capturedImageFileArgs[2]) == true)
            //    {
            //        var imgByteArr = ImageHelper.LoadImageToArray(capturedImageFile);
            //        if (imgByteArr != null)
            //        {
            //            imgPatientPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}",
            //                Convert.ToBase64String(imgByteArr));
            //            return;
            //        }
            //    }
            //}

            // Load from database
            var patientImg = new PatientImage();
            if (patientImg.LoadByPrimaryKey(patientID))
            {
                // Show Image
                if (patientImg.Photo != null)
                {
                    imgPatientPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}",
                        Convert.ToBase64String(patientImg.Photo));
                }
                else
                {
                    imgPatientPhoto.ImageUrl = lblGender.Text == "Male" ? "~/Images/Asset/Patient/ManVector.png" : (lblGender.Text == "Female" ? "~/Images/Asset/Patient/WomanVector.png" : "~/Images/Asset/Patient/HumanVector.png");
                }
            }
            else
                imgPatientPhoto.ImageUrl = lblGender.Text == "Male" ? "~/Images/Asset/Patient/ManVector.png" : (lblGender.Text == "Female" ? "~/Images/Asset/Patient/WomanVector.png" : "~/Images/Asset/Patient/HumanVector.png");

        }

        #endregion

        #region Question Form
        //protected void grdVitalSign_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        //{
        //    grdVitalSign.DataSource = VitalSign.VitalSignLastValue(RegistrationNo, String.Empty, false, DateTime.Now);
        //}

        #endregion

        #region User Access

        protected bool IsUserEntryAssessment()
        {
            if (this.IsUserAddAble.Equals(false)) return false;

            if (AppSession.Parameter.IsByPassEmrUserTypeRestriction) return true;

            // Hanya dokter team
            if (AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor && IsUserInParamedicTeam())
                return true;

            return false;
        }
        protected bool IsUserInParamedicTeam()
        {
            return IsUserInParamedicTeam(RegistrationCurrent);
        }

        protected bool IsUserEntryReferConsulToSpecialist()
        {
            // Hanya dokter utama
            return (AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor && IsUserParamedicDpjp());
        }

        protected bool IsUserEntryDischarge()
        {
            // Perawat dan dokter utama
            //return AppSession.UserLogin.SRUserType == AppUser.UserType.Nurse || IsUserParamedicDpjp();

            //Perawat dan dokter 
            return AppSession.UserLogin.SRUserType == AppUser.UserType.Nurse || AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor;
        }
        #endregion


        #region PastMedicalHist
        private const string PastMedHist = "PastMedHist";
        private const string FamilyMedHist = "FamilyMedHist";

        protected void grdPastMedicalHist_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (string.IsNullOrEmpty(PatientID)) return;

            if (!IsPostBack || Session["dtbPmh"] == null || !Equals(PatientID, Session["dtbPmh_id"]))
            {
                Session["dtbPmh"] = MedicalHistDataTable();
                Session["dtbPmh_id"] = PatientID;
            }

            grdPastMedicalHist.DataSource = Session["dtbPmh"];
        }
        protected void grdPastMedicalHist_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "refresh")
            {
                Session["dtbPmh"] = null;
                grdPastMedicalHist.DataSource = null;
                grdPastMedicalHist.Rebind();
            }
        }
        private DataTable MedicalHistDataTable()
        {
            var dtb = new DataTable();
            dtb.Columns.Add("GroupName", typeof(System.String));
            dtb.Columns.Add("PastMedical", typeof(System.String));

            var pmh = PastMedicalHistDataTable();
            foreach (DataRow row in pmh.Rows)
            {
                var newRow = dtb.NewRow();
                newRow["GroupName"] = "Past Medical History";
                newRow["PastMedical"] = string.Format("{0}: {1}", row["ItemName"], row["Notes"]);
                dtb.Rows.Add(newRow);
            }

            var fmh = FamilyMedicalHistDataTable();
            foreach (DataRow row in fmh.Rows)
            {
                var newRow = dtb.NewRow();
                newRow["GroupName"] = "Family Medical History";
                newRow["PastMedical"] = string.Format("{0}: {1}", row["ItemName"], row["Notes"]);
                dtb.Rows.Add(newRow);
            }

            //Past Surgery
            var surgicalHist = new PastSurgicalHistory();
            if (surgicalHist.LoadByPrimaryKey(PatientID))
            {
                var newRow = dtb.NewRow();
                newRow["GroupName"] = "Past Surgical History";
                newRow["PastMedical"] = surgicalHist.SurgicalHistory;
                dtb.Rows.Add(newRow);
            }


            //Surgery Transaction
            var dtbSurgery = EpisodeProcedures();
            foreach (DataRow surgery in dtbSurgery.Rows)
            {
                var newRow = dtb.NewRow();
                newRow["GroupName"] = "Surgical History";
                newRow["PastMedical"] = string.Format("{0} {1} [{2}]", Convert.ToDateTime(surgery["ProcedureDate"]).ToString(AppConstant.DisplayFormat.Date), surgery["ProcedureName"], surgery["ParamedicName"]);
                dtb.Rows.Add(newRow);
            }
            return dtb;
        }

        private DataTable PastMedicalHistDataTable()
        {
            var que = new PastMedicalHistoryQuery("a");
            var qrSri = new AppStandardReferenceItemQuery("sri");
            que.LeftJoin(qrSri)
                .On(que.SRMedicalDisease == qrSri.ItemID && qrSri.StandardReferenceID == PastMedHist);
            que.Where(que.PatientID == PatientID);
            que.Select(que.SRMedicalDisease, qrSri.ItemName, que.Notes);

            var dtbPast = que.LoadDataTable();
            return dtbPast;
        }

        private DataTable EpisodeProcedures()
        {
            var query = new EpisodeProcedureQuery("a");
            var reg = new RegistrationQuery("r");
            var param = new ParamedicQuery("b");
            var proc = new ProcedureQuery("c");

            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(param).On(query.ParamedicID == param.ParamedicID);
            query.InnerJoin(proc).On(query.ProcedureID == proc.ProcedureID);

            query.Select
                 (
                     query.ProcedureDate,
                     query.ProcedureID,
                     param.ParamedicName,
                     proc.ProcedureName
                 );

            if (PatientRelateds.Count == 1)
                query.Where(reg.PatientID == PatientID);
            else
                query.Where(reg.PatientID.In(PatientRelateds));


            query.Where(reg.IsVoid == false, query.IsFromOperatingRoom == true, query.IsVoid == false);
            query.OrderBy(query.SequenceNo.Ascending);

            var dtb = query.LoadDataTable();

            return dtb;
        }


        protected void grdPastMedicalHist_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridGroupHeaderItem)
            {
                // Menghilangkan control group expand collapse
                (e.Item as GridGroupHeaderItem).Cells[0].Controls.Clear();
            }
        }

        private DataTable FamilyMedicalHistDataTable()
        {
            var que = new FamilyMedicalHistoryQuery("a");
            var qrSri = new AppStandardReferenceItemQuery("sri");
            que.LeftJoin(qrSri)
                .On(que.SRMedicalDisease == qrSri.ItemID && qrSri.StandardReferenceID == FamilyMedHist);
            que.Where(que.PatientID == PatientID);
            que.Select(qrSri.ItemName, que.Notes);
            return que.LoadDataTable();
        }
        #endregion
    }
}
