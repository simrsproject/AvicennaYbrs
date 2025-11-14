using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.EmrIp
{
    public partial class EmrIpDetail : BasePage
    {
        //private int _deadlineEditable = AppParameter.GetParameterValue(AppParameter.ParameterItem.DeadlineMedicalRecordEditableAfterDischarge).ToInt();
        //private int _deadlineAddable = AppParameter.GetParameterValue(AppParameter.ParameterItem.DeadlineMedicalRecordAddableAfterDischarge).ToInt();
        protected string ParamedicFirstTransChargesItemIds = AppSession.Parameter.GetParameterValueString(AppParameter.ParameterItem.ParamedicFirstTransChargesItemIds);
        protected string GetAbortAddMessage()
        {
            //if (_deadlineAddable > 0)
            //{
            //    if (!IsDischargeDateTimeLessThan(_deadlineAddable, RegistrationCurrent))`
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


        #region Refer From Info
        public string ReferFromRegistrationNo
        {
            get
            {

                //return Request.QueryString["fregno"];

                // Baca ulangdari Registration krn ada fitur bisa merubah FromRgistrationNo
                return RegistrationCurrent.FromRegistrationNo;
            }
        }
        protected string ReferFromServiceUnitID
        {
            get
            {
                if (ViewState["ReferFromServiceUnitID"] == null || string.IsNullOrEmpty(ViewState["ReferFromServiceUnitID"].ToString()))
                {
                    PopulateReferFromInfo();
                }
                return ViewState["ReferFromServiceUnitID"].ToString();
            }
        }
        protected string ReferFromSRAssessmentType
        {
            get
            {
                if (ViewState["ReferFromSRAssessmentType"] == null || string.IsNullOrEmpty(ViewState["ReferFromSRAssessmentType"].ToString()))
                {
                    PopulateReferFromInfo();
                }
                return ViewState["ReferFromSRAssessmentType"].ToString();
            }
        }
        protected string ReferFromRegistrationInfoMedicID
        {
            get
            {
                if (ViewState["ReferFromRegistrationInfoMedicID"] == null || string.IsNullOrEmpty(ViewState["ReferFromRegistrationInfoMedicID"].ToString()))
                {
                    PopulateReferFromInfo();
                }
                return ViewState["ReferFromRegistrationInfoMedicID"].ToString();
            }
        }
        protected bool ReferFromIsInitialAssessment
        {
            get
            {
                if (ViewState["ReferFromIsInitialAssessment"] == null)
                {
                    PopulateReferFromInfo();
                }
                return Convert.ToBoolean(ViewState["ReferFromIsInitialAssessment"]);
            }
        }
        private void PopulateReferFromInfo()
        {
            ViewState["ReferFromServiceUnitID"] = string.Empty;
            ViewState["ReferFromSRAssessmentType"] = string.Empty;
            ViewState["ReferFromRegistrationInfoMedicID"] = string.Empty;
            ViewState["ReferFromIsInitialAssessment"] = false;

            if (string.IsNullOrEmpty(ReferFromRegistrationNo))
                return;

            var que = new RegistrationInfoMedicQuery("a");
            var pa = new PatientAssessmentQuery("pa");

            // Refer from Registration
            que.InnerJoin(pa).On(que.RegistrationInfoMedicID == pa.RegistrationInfoMedicID);
            que.Where(que.RegistrationNo == ReferFromRegistrationNo);
            que.OrderBy(que.RegistrationInfoMedicID.Descending);
            que.es.Top = 1;

            var reg = new RegistrationQuery("x");
            que.InnerJoin(reg).On(que.RegistrationNo == reg.RegistrationNo);
            que.Select(que.ServiceUnitID, pa.SRAssessmentType, que.RegistrationInfoMedicID, que.ParamedicID, pa.IsInitialAssessment);

            var dtb = que.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                var row = dtb.Rows[0];
                ViewState["ReferFromServiceUnitID"] = row["serviceUnitID"];
                ViewState["ReferFromSRAssessmentType"] = row["SRAssessmentType"];
                ViewState["ReferFromRegistrationInfoMedicID"] = row["RegistrationInfoMedicID"];
                ViewState["ReferFromIsInitialAssessment"] = row["IsInitialAssessment"];
            }
        }
        #endregion


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

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;
            if (!IsPostBack)
            {
                PopulateToolbarHaisMonitoring();
                PopulateToolbarPrint();
            }

        }

        private void PopulateToolbarHaisMonitoring()
        {
            var tbarHais = (RadToolBarDropDown)tbMenu.Items[8];

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
            // Isi Related Program type Report
            var tbarPrint = (RadToolBarDropDown)tbMenu.Items[tbMenu.Items.Count - 2];

            var qPrg = new AppProgramQuery("a");
            var qRel = new AppProgramRelatedQuery("b");
            qRel.InnerJoin(qPrg).On(qRel.RelatedProgramID == qPrg.ProgramID);

            qRel.Where(qRel.Or(qRel.ProgramID == AppConstant.Program.EpisodeAndHistory, qRel.ProgramID == AppConstant.Program.ElectronicMedicalRecord),
                qPrg.ProgramType.In("RPT", "XML", "RSLIP"));

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
            PopulateRegistrationInfo();

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

                PopulateFilterIntegratedNotes();

                // Progress Notes
                var tbItem = tbarIntegratedNote.FindItemByText("Progress Notes");
                tbItem.Enabled = IsUserInParamedicTeam();

                // Medical Discharge Summary hanya bisa diakses oleh dokter team dg status dpjp dan Sharing (RSMM)
                //tbItem = tbarIntegratedNote.FindItemByText("Medical Discharge Summary");
                tbItem = tbarIntegratedNote.FindItemByValue("ResumeMedis");
                if (AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor)
                {
                    if (AppParameter.IsYes(AppParameter.ParameterItem.IsAllPhysicianAllowEditMedicalDischarge))
                        tbItem.Enabled = IsUserInParamedicTeam();
                    else
                        tbItem.Enabled = IsUserParamedicDpjpOrSharing();
                }
                else
                    tbItem.Enabled = false;

                // Check Medication yg sudah mau habis dalam (MedicationWillOutOfBalanceInDay) hari
                if (IsUserInParamedicTeam())
                {
                    var medInMinQtyMsg = MedicationReceive.BalanceInZeroQty(RegistrationNo, ReferFromRegistrationNo)
                        .Trim();
                    if (!string.IsNullOrWhiteSpace(medInMinQtyMsg))
                    {
                        medInMinQtyMsg = medInMinQtyMsg.Replace("\"", "\\\"");
                        Helper.RegisterStartupScript(this.Page, "medInMinQty",
                            string.Concat("setTimeout(function(){radalert(\"", medInMinQtyMsg,
                                "\", 800, 400, \"This Medication Item will soon run out in ", AppSession.Parameter.GetParameterValue(AppParameter.ParameterItem.MedicationWillOutOfBalanceInDay), " days\");}, 500);"));

                    }
                }
                var reg = new Registration();
                reg.LoadByPrimaryKey(RegistrationNo);
                //if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsBillingEmrAddButtonEnabled) == "Yes")
                //{
                //    lnkUddItem.Enabled = reg.IsHoldTransactionEntry == false ? !IsReadOnly : false;
                //}
                //else
                //{
                //    lnkUddItem.Enabled = true;
                //}
                lnkUddItem.Enabled = AppParameter.GetParameterValue(AppParameter.ParameterItem.IsBillingEmrAddButtonEnabled) == "Yes" ? reg.IsHoldTransactionEntry == false : true;

            }
            // Selalu Populate toolbar Assessment
            PopulateMenuAssessmentAdd();

            //"UDD" 
            PopulateMenuUDD();

            // Medical Hist
            medicalHistCtl.IsNewPatient = IsNewPatient;
            medicalHistCtl.IsClosed = IsClosed;
            medicalHistCtl.IsUserAddAble = IsUserAddAble;
            medicalHistCtl.IsUserEditAble = IsUserEditAble;
            medicalHistCtl.ReferFromRegistrationNo = ReferFromRegistrationNo;

            // Diganti dg Medication History
            if (!IsPostBack)
            {
                txtFromDate.SelectedDate = RegistrationCurrent.RegistrationDate;
                medicationHistCtl.SetDataSource(MedicationReceiveDataTable(), txtFromDate.SelectedDate);
            }

            // Exam Order
            examOrderHistCtl.IsNewPatient = IsNewPatient;
            examOrderHistCtl.IsClosed = IsClosed;
            examOrderHistCtl.IsUserAddAble = IsUserAddAble;
            examOrderHistCtl.IsUserEditAble = IsUserEditAble;
            examOrderHistCtl.ReferFromRegistrationNo = ReferFromRegistrationNo;

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

        bool? _isUserInParamedicTeam = null;
        protected bool IsUserInParamedicTeam()
        {
            if (AppSession.UserLogin.SRUserType != AppUser.UserType.Doctor || string.IsNullOrWhiteSpace(AppSession.UserLogin.ParamedicID))
                return false;

            if (_isUserInParamedicTeam == null)
                _isUserInParamedicTeam = IsUserInParamedicTeam(RegistrationCurrent);

            return _isUserInParamedicTeam ?? false;
        }
        protected void PopulateMenuAssessmentAdd()
        {
            // List Asesmen untuk rawat inap diambil dari setingan di SMF dan sekarang hanya 1 tipe saja
            // Form ini khusus untuk entry rawat inap

            var tbarItemAdd = (RadToolBarDropDown)tbarIntegratedNote.FindItemByText("Assessment");

            tbarItemAdd.Enabled = true;
            tbarItemAdd.Buttons.Clear();

            if (!IsUserEntryAssessment())
            {
                tbarItemAdd.Enabled = false;
                return;
            }

            var smf = new Smf();
            smf.LoadByPrimaryKey(RegistrationCurrent.SmfID);
            var assessmentType = smf.SRAssessmentType;

            if (string.IsNullOrEmpty(assessmentType))
            {
                return;
            }

            // Add Default Assessment
            var stdi = new AppStandardReferenceItem();
            stdi.LoadByPrimaryKey("AssessmentType", assessmentType);

            var btn = new RadToolBarButton(string.Format("Add {0}", stdi.ItemName))
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

        private void PopulateMenuUDD()
        {
            //<Buttons>
            //    <telerik:RadToolBarButton runat="server" Text="Service Unit Kardex" Value="MedicationHist" />
            //    <telerik:RadToolBarButton runat="server" Text="Drug Maintenance & Review" Value="udd_maintenance" />
            //    <telerik:RadToolBarButton IsSeparator="true" />
            //    <telerik:RadToolBarButton runat="server" Text="Drug Acceptance from Prescription" Value="drugfrom_prescription" />
            //    <telerik:RadToolBarButton runat="server" Text="Drug Acceptance from Patient" Value="drugfrom_patient" />
            //    <telerik:RadToolBarButton runat="server" Text="Drug Acceptance from Service Unit Tansaction" Value="drugfrom_serviceunit" />
            //    <telerik:RadToolBarButton IsSeparator="true" />
            //    <telerik:RadToolBarButton runat="server" Text="Admission Drug Reconciliation" Value="adm_recon" />
            //    <telerik:RadToolBarButton runat="server" Text="Transfer Drug Reconciliation" Value="trf_recon" />
            //    <telerik:RadToolBarButton runat="server" Text="Discharge Drug Reconciliation" Value="dcg_recon" />
            //    <telerik:RadToolBarButton IsSeparator="true" />
            //    <telerik:RadToolBarButton runat="server" Text="Medication Setup Status" Value="udd_setup" />
            //    <telerik:RadToolBarButton runat="server" Text="Medication Verification Status" Value="udd_verification" />
            //    <telerik:RadToolBarButton runat="server" Text="Medication Realization Status" Value="udd_realization" />
            //</Buttons>

            var tbUdd = (RadToolBarDropDown)tbMenu.FindItemByText("UDD");

            tbUdd.Buttons.Clear();

            var tbtn = new RadToolBarButton("Service Unit Kardex")
            {
                Value = "MedicationHist"
            };
            tbUdd.Buttons.Add(tbtn);


            tbtn = new RadToolBarButton("Drug Maintenance & Review")
            {
                Value = "udd_maintenance",
                Enabled = IsUserInParamedicTeam()
            };
            tbUdd.Buttons.Add(tbtn);

            tbtn = new RadToolBarButton { IsSeparator = true };
            tbUdd.Buttons.Add(tbtn);

            tbtn = new RadToolBarButton("Drug Acceptance from Patient")
            {
                Value = "drugfrom_patient"
            };
            tbUdd.Buttons.Add(tbtn);

            tbtn = new RadToolBarButton("Drug Acceptance from Service Unit Tansaction")
            {
                Value = "drugfrom_serviceunit",
                Enabled = IsUserEditAble
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
                Enabled = IsUserEditAble
            };
            tbUdd.Buttons.Add(tbtn);

            tbtn = new RadToolBarButton("Discharge Drug Reconciliation")
            {
                Value = "dcg_recon",
                Enabled = IsUserEditAble
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
                Enabled = IsUserEditAble
            };
            tbUdd.Buttons.Add(tbtn);

            tbtn = new RadToolBarButton("Medication Handovers")
            {
                Value = "udd_handovers",
                Enabled = IsUserEditAble
            };
            tbUdd.Buttons.Add(tbtn);

            tbtn = new RadToolBarButton("Medication Verification Status")
            {
                Value = "udd_verification",
                Enabled = IsUserEditAble
            };
            tbUdd.Buttons.Add(tbtn);

            tbtn = new RadToolBarButton("Medication Realization Status")
            {
                Value = "udd_realization",
                Enabled = IsUserEditAble
            };
            tbUdd.Buttons.Add(tbtn);
        }

        protected void PopulateFilterIntegratedNotes()
        {
            var tbiFilter = (RadToolBarDropDown)tbarIntegratedNote.FindItemByText("Filter Entry By");

            tbiFilter.Buttons.Clear();

            var stdiColl = new AppStandardReferenceItemCollection();
            stdiColl.LoadByStandardReferenceID("UserType");

            var btnAll = new RadToolBarButton("Show All")
            {
                Value = "filter_"
            };
            tbiFilter.Buttons.Add(btnAll);
            foreach (AppStandardReferenceItem item in stdiColl)
            {
                var btn = new RadToolBarButton(item.ItemName)
                {
                    Value = string.Format("filter_{0}", item.ItemID)
                };
                tbiFilter.Buttons.Add(btn);
            }
        }
        private void PopulateFromReg(string fromRegistrationNo)
        {
            if (string.IsNullOrEmpty(fromRegistrationNo))
            {
                lblFromRegistrationNo.Text = string.Empty;
                lblFromPhysician.Text = string.Empty;
                lblFromServiceUnit.Text = string.Empty;
                return;
            }

            var fromReg = new Registration();
            fromReg.LoadByPrimaryKey(fromRegistrationNo);

            lblFromRegistrationNo.Text = fromRegistrationNo;

            lblFromPhysician.Text = Paramedic.GetParamedicName(fromReg.ParamedicID);

            lblFromServiceUnit.Text = ServiceUnit.GetServiceUnitName(fromReg.ServiceUnitID);
        }

        private string ParamedicTeamHtml(string registrationNo, string paramedicID)
        {
            // Paramedic
            var medic = new Paramedic();
            var qrparteam = new ParamedicTeamQuery("pt");
            qrparteam.Where(qrparteam.RegistrationNo == registrationNo);
            qrparteam.es.Top = 1;
            if ((new ParamedicTeam().Load(qrparteam)))
            {
                var dpjpStatus = AppSession.Parameter.GetParameterValueString(AppParameter.ParameterItem.ParamedicTeamStatusDpjpID);
                qrparteam = new ParamedicTeamQuery("pt");
                qrparteam.Where(qrparteam.RegistrationNo == registrationNo);
                qrparteam.Select(string.Format("<CASE WHEN pt.SRParamedicTeamStatus = '{0}' THEN 1 ELSE 2 END as SortStat>", dpjpStatus),
                    qrparteam.ParamedicID, qrparteam.SRParamedicTeamStatus);
                qrparteam.OrderBy("<1>", qrparteam.StartDate.Descending);

                var dtb = qrparteam.LoadDataTable();

                var isDpjpExist = false;
                var strBld = new StringBuilder();
                var i = 1;
                var paramedicIds = string.Empty;
                strBld.Append("<table>");
                foreach (DataRow row in dtb.Rows)
                {
                    var parId = row["ParamedicID"].ToString();

                    //Skip jika ParamedicID sudah ada
                    if (paramedicIds.Contains(parId)) continue;

                    var qrPar = new ParamedicQuery("p");
                    qrPar.Select(qrPar.ParamedicID, qrPar.ParamedicName);
                    qrPar.Where(qrPar.ParamedicID == parId);
                    medic = new Paramedic();
                    if (medic.Load(qrPar))
                    {
                        paramedicIds = string.Concat(paramedicIds, ";", parId);
                        if (!isDpjpExist && 1.Equals(row["SortStat"]))
                        {
                            // Hanya 1 dpjp yg dibold
                            strBld.AppendFormat(
                                "<tr><td style=\"width:5px;vertical-align: top;\"><b>{0}.</b></td><td><b>{1}</b></td></tr>",
                                i, medic.ParamedicName);
                            isDpjpExist = true;
                        }
                        else
                            strBld.AppendFormat("<tr><td style=\"width:5px;vertical-align: top;\">{0}.</td><td>{1}</td></tr>", i, medic.ParamedicName);
                        i++;
                    }
                }
                strBld.Append("</table>");
                return strBld.ToString();
            }

            return string.Empty;
        }
        private void PopulateRegistrationInfo()
        {
            var reg = RegistrationCurrent;
            PopulateFromReg(reg.FromRegistrationNo);

            var pat = new Patient();
            pat.LoadByPrimaryKey(PatientID);

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

            lblChronicDisease.Text = Patient.ChronicDisease(PatientID);
            divChronicDisease.Visible = !string.IsNullOrEmpty(lblChronicDisease.Text);

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
                stRiskColor.Query.Select(stRiskColor.Query.ReferenceID);
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
            divClinicalPathway.Visible = AppSession.Parameter.ClinicalPathwayRegistrationType.Contains(RegistrationType);

            lblPhysicianTeam.Text = ParamedicTeamHtml(RegistrationNo, RegistrationCurrent.ParamedicID);

            /**
             * Last Ranap Date
             */
            DateTime? latestRanapDate = LatestInpatientRegistrationDate();
            lblTglRanap.Text = latestRanapDate?.ToString(AppConstant.DisplayFormat.DateShortMonth) ?? "-";

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);
            lblGuarantor.Text = grr.GuarantorName;
            trBpjsSepNo.Visible = grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeBPJS;
            if (trBpjsSepNo.Visible)
                lblBpjsSepNo.Text = reg.BpjsSepNo;

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

            PopulatePatientAllergy();
            PopulateEpisodeDiagnose();
            PopulateImmunizationHistory();
            PopulatePatientDialysis();
            //litPlafond.Text = Temiang.Avicenna.Module.RADT.Cpoe.EmrList.PlafondProgress(RegistrationNo, true); // Diganti update via webservice

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
            litWorkDiagnosis.Text = RegistrationInfoMedicDiagnose.DiagnoseSummaryHtml(RegistrationNo);
            litInitialDiagnose.Text = RegistrationCurrent.InitialDiagnose;
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

        #region Status Show All History
        protected void chkShowAll_OnLoad(object sender, EventArgs e)
        {
            // Valuenya harus diset anual krn berada di Command template 
            var chk = ((CheckBox)sender);
            chk.Checked = Convert.ToBoolean(ViewState[chk.ID]);
        }

        protected void chkShowAll_OnCheckedChanged(object sender, EventArgs e)
        {
            var chk = ((CheckBox)sender);
            ViewState[chk.ID] = chk.Checked;
            switch (chk.ID)
            {
                case "chkShowAllRegistrationInfoMedic":
                    //episodeHistCtl.DataBind();
                    break;
            }

        }

        #endregion

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
        }


        private void PrintIntegratedNoteAndImplementation(PrintJobParameterCollection jobParameters)
        {
            // Print from Integrated Note
            var jobParameter = jobParameters.AddNew();
            jobParameter.Name = "p_RegistrationNo";
            jobParameter.ValueString = RegistrationNo;

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = "SLP.10.0008";

        }

        private void PrintIntegratedNoteSoap(PrintJobParameterCollection jobParameters, string keyValue)
        {
            // Print from Integrated Note
            var jobParameter = jobParameters.AddNew();
            jobParameter.Name = "registrationNo";
            jobParameter.ValueString = keyValue;

            var jobParameter2 = jobParameters.AddNew();
            jobParameter2.Name = "SequenceNo";
            jobParameter2.ValueString = "";

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = AppConstant.Report.SOAP;

        }

        private void PrintEpisodeSoap(string keyValue, PrintJobParameterCollection jobParameters)
        {
            var keyValues = keyValue.Split('_');
            if (keyValues.Length > 0)
            {
                var jobParameter = jobParameters.AddNew();
                jobParameter.Name = "registrationNo";
                jobParameter.ValueString = keyValues[0];

                var jobParameter2 = jobParameters.AddNew();
                jobParameter2.Name = "SequenceNo";
                jobParameter2.ValueString = keyValues[1];

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.SOAP;

            }
        }

        protected void grdAssessment_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            // Call from NestedViewTemplate
            if (!(e.CommandSource is LinkButton)) return;
            var selectedKey = ((LinkButton)(e.CommandSource)).CommandArgument;
            if (string.IsNullOrEmpty(selectedKey)) return;


            var keys = selectedKey.Split('_');
            if (keys[1] == "True") // From Askep
            {
                var entity = new NursingDiagnosaTransDT();
                if (entity.LoadByPrimaryKey(keys[0].ToInt()))
                {
                    var messageOfDeadlineEditedOver = Helper.MessageOfDeadlineEditedOver(entity.CreateDateTime.Value);
                    if (!string.IsNullOrWhiteSpace(messageOfDeadlineEditedOver))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "invalid", string.Format("alert('{0}');", messageOfDeadlineEditedOver), true);
                        return;
                    }

                    // Check void reg status utk proses unvoid
                    if (entity.IsDeleted == true)
                    {
                        var ntd = new NursingTransHD();
                        ntd.LoadByPrimaryKey(entity.TransactionNo);

                        var reg = new Registration();
                        reg.LoadByPrimaryKey(ntd.RegistrationNo);
                        if (reg.IsVoid ?? false)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "invalid", string.Format("alert('{0}');", "Registration has void, cannot unvoid this data"), true);
                            return;
                        }
                    }

                    entity.IsDeleted = !(entity.IsDeleted ?? false); ;
                    entity.Save();
                }
            }
            else
            {
                var entity = new RegistrationInfoMedic();
                var rimid = keys[0];
                if (entity.LoadByPrimaryKey(rimid))
                {
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

                    entity.IsDeleted = !(entity.IsDeleted ?? false); ;
                    entity.Save();

                    var pa = new PatientAssessment();
                    if (pa.LoadByPrimaryKey(rimid))
                    {
                        pa.IsDeleted = entity.IsDeleted;
                        pa.Save();
                    }

                    var epds = new RegistrationInfoMedicDiagnoseCollection();
                    epds.Query.Where(epds.Query.RegistrationInfoMedicID == rimid);
                    if (epds.LoadAll())
                    {
                        foreach (var epd in epds)
                        {
                            epd.IsVoid = entity.IsDeleted;
                        }
                        epds.Save();
                    }
                }
            }


            // Refresh
            grdAssessment.DataSource = null;
            grdAssessment.Rebind();

        }

        public static DataTable RegistrationInfoMedicDataTable(string registrationType, string registrationNo, List<string> registrationNoList, string patientID, string filterEntry)
        {
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
                // Filter dari table Registration dan juga dari RegistrationInfoMedic akan lebih efisien query nya langsung terfilter jumlah recordnya masing2 (Handono 2024-11-19)
                if (registrationNoList.Count > 1)
                    que.Where(reg.RegistrationNo.In(registrationNoList), que.RegistrationNo.In(registrationNoList));
                else
                    que.Where(reg.RegistrationNo == registrationNoList[0], que.RegistrationNo == registrationNoList[0]);
            }
            else
            {

                //List<string> patientRelateds = Patient.PatientRelateds(patientID);
                //if (patientRelateds.Count == 1)
                //    que.Where(reg.PatientID == patientID);
                //else
                //    que.Where(reg.PatientID.In(patientRelateds));

                // Non Inpatient ambil 5 registrasi terakhir (Handono 2022-09)
                var regCount = AppSession.Parameter.GetParameterValue(AppParameter.ParameterItem.EmrHistoryRegistrationCount).ToInt();
                var lastRegNos = Patient.Last.RegistrationNos(patientID, regCount, registrationNo);

                // Filter dari table Registration dan juga dari RegistrationInfoMedic akan lebih efisien query nya langsung terfilter jumlah recordnya masing2 (Handono 2024-11-19)
                if (lastRegNos.Count == 1)
                    que.Where(reg.RegistrationNo == lastRegNos[0], que.RegistrationNo == lastRegNos[0]);
                else
                    que.Where(reg.RegistrationNo.In(lastRegNos), que.RegistrationNo.In(lastRegNos));


            }

            if (!string.IsNullOrWhiteSpace(filterEntry))
                que.Where(que.SRUserType == filterEntry);

            que.OrderBy(que.RegistrationInfoMedicID.Descending);
            que.Select(que.RegistrationInfoMedicID, que.RegistrationNo, que.ParamedicID, que.SRMedicalNotesInputType, que.DateTimeInfo, que.Info1, que.Info2, que.Info3, que.Info4, que.Info5,
                que.IsApproved.Coalesce("CONVERT(bit,0)").As("IsApproved"), que.ApprovedDatetime, que.ApprovedByUserID, que.CreatedDateTime, que.CreatedByUserID, que.IsDeleted.Coalesce("CONVERT(bit,0)").As("IsDeleted"), que.ServiceUnitID,
                que.AttendingNotes,
                "<COALESCE(pa.SRAssessmentType,a.ReferenceType) as SRAssessmentType>",
                pa.IsInitialAssessment, "<CAST(0 as bit) as IsFromAskep>", stdi.ItemName.Coalesce("''").As("AssessmentTypeName"),
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
                var regCount = AppSession.Parameter.GetParameterValue(AppParameter.ParameterItem.EmrHistoryRegistrationCount).ToInt();
                var lastRegNos = Patient.Last.RegistrationNos(patientID, regCount, registrationNo);
                if (lastRegNos.Count == 1)
                    nsdt.Where(regqr.RegistrationNo == lastRegNos[0]);
                else
                    nsdt.Where(regqr.RegistrationNo.In(lastRegNos));
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

            QuestionCollection questions = new QuestionCollection();

            var i = -1;
            foreach (DataRow rim in sortedDt.Rows)
            {
                i++;
                var newRow = dtb.NewRow();
                newRow["RegistrationInfoMedicID"] = rim["RegistrationInfoMedicID"];
                newRow["SRMedicalNotesInputType"] = rim["SRMedicalNotesInputType"];
                newRow["DateTimeInfo"] = rim["DateTimeInfo"];
                newRow["CreatedByUserID"] = rim["CreatedByUserID"];
                newRow["RegistrationNo"] = rim["RegistrationNo"];
                newRow["IsDeleted"] = rim["IsDeleted"] == DBNull.Value ? false : rim["IsDeleted"];
                newRow["SRAssessmentType"] = rim["SRAssessmentType"];
                newRow["AssessmentTypeName"] = rim["AssessmentTypeName"];
                newRow["IsInitialAssessment"] = rim["IsInitialAssessment"];
                newRow["ParamedicID"] = rim["ParamedicID"];
                newRow["ServiceUnitID"] = rim["ServiceUnitID"];
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

                if (rim["ServiceUnitID"] != DBNull.Value && !string.IsNullOrEmpty(rim["ServiceUnitID"].ToString()))
                {
                    if (!serviceUnitID.Equals(rim["ServiceUnitID"]))
                    {
                        serviceUnitID = rim["ServiceUnitID"].ToString();
                        var su = suColl.Where(s => s.ServiceUnitID == serviceUnitID).FirstOrDefault();
                        if (su == null)
                        {
                            su = new ServiceUnit();
                            su.Query.Select(su.Query.ServiceUnitID, su.Query.ServiceUnitName);
                            su.LoadByPrimaryKey(serviceUnitID);
                            suColl.AttachEntity(su);
                        }
                        serviceUnitName = su.ServiceUnitName;
                    }
                }
                else
                {
                    serviceUnitID = string.Empty;
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
                            info1 = AuditLogLinkMenu(rim, "Info1", info1);

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

                            info2 = AuditLogLinkMenu(rim, "Info2", info2);

                            sbNote.AppendFormat(
                                "<tr><td class='label' valign='top' style='font-weight: bold;width:{2}px; padding-left:2px'>{0}:</td><td valign='top'>{1}</td></tr>",
                                label.Substring(1, 1), info2, col1Width);
                            sbNote.AppendLine();

                            // Info3
                            var info3 = ReplaceWitBreakLineHTML(rim, "Info3");
                            info3 = AuditLogLinkMenu(rim, "Info3", info3);

                            sbNote.AppendFormat(
                                "<tr><td class='label' valign='top' style='font-weight: bold;width:{2}px; padding-left:2px'>{0}:</td><td valign='top'>{1}</td></tr>",
                                label.Substring(2, 1), info3, col1Width);

                            sbNote.AppendLine();

                            // Planning
                            string planning = FormatToHtml(rim["Info4"]);
                            planning = AuditLogLinkMenu(rim, "Info4", planning);

                            if (medicalNotesInputType == "SOAP" && isFromAssessment)
                            {
                                // Dari asesmen tambah hist resepnya di Planning
                                if (rim["PrescriptionCurrentDay"] != DBNull.Value ||
                                    !string.IsNullOrEmpty(rim["PrescriptionCurrentDay"].ToString()))
                                    planning = string.Format("{0}<br/><br/>{1}", planning,
                                        FormatToHtml(rim["PrescriptionCurrentDay"]));
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
                                {
                                    if (rim["PrescriptionCurrentDay"] != DBNull.Value &&
                                         !string.IsNullOrWhiteSpace(rim["PrescriptionCurrentDay"].ToString().Replace("\r\n", "")))
                                    {
                                        if (string.IsNullOrEmpty(ppaInstruction))
                                            ppaInstruction = FormatToHtml(rim["PrescriptionCurrentDay"]);
                                        else
                                            ppaInstruction = string.Format("{0}<br/><br/>{1}",
                                                ppaInstruction,
                                                FormatToHtml(rim["PrescriptionCurrentDay"]));
                                    }
                                }

                                if (!string.IsNullOrWhiteSpace(ppaInstruction))
                                {
                                    // "I" jangan dimunculkan jika kosong karena akan jadi pertanyaan team akreditasi (Handono 23-12-08 req by RSI)
                                    ppaInstruction = AuditLogLinkMenu(rim, "PpaInstruction", ppaInstruction);
                                    sbNote.AppendFormat(
                                        "<tr><td class='label' valign='top' style='font-weight: bold;width:{1}px; padding-left:2px'>I:</td><td valign='top'>{0}</td></tr>",
                                        ppaInstruction, col1Width);
                                }

                                var info = ReplaceWitBreakLineHTML(rim, "Info5");
                                if (!string.IsNullOrWhiteSpace(info))
                                {
                                    // "E" jangan dimunculkan jika kosong karena akan jadi pertanyaan team akreditasi (Handono 23-12-08 req by RSI)
                                    info = AuditLogLinkMenu(rim, "Info5", info);
                                    sbNote.AppendFormat(
                                        "<tr><td class='label' valign='top' style='font-weight: bold;width:{2}px;padding-left:2px'>{0}:</td><td valign='top'>{1}</td></tr>",
                                        "E", info, col1Width);
                                    sbNote.AppendLine();
                                }

                                info = ReplaceWitBreakLineHTML(rim, "SubmitBy");
                                if (!string.IsNullOrWhiteSpace(info))
                                {
                                    sbNote.AppendFormat(
                                        "<tr><td class='label' valign='top' style='font-weight: bold;width:{2}px; padding-left:2px'>{0}:</td><td valign='top'>{1}</td></tr>",
                                        "Submit By", info, col1Width);
                                    sbNote.AppendLine();
                                }

                                info = ReplaceWitBreakLineHTML(rim, "ReceiveBy");
                                if (!string.IsNullOrWhiteSpace(info))
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
                                info = AuditLogLinkMenu(rim, "PpaInstruction", info);
                                sbNote.AppendFormat(
                                    "<tr><td class='label' valign='top' style='font-weight: bold;width:{1}px; padding-left:2px'>I:</td><td valign='top'>{0}</td></tr>",
                                     info, col1Width);
                                sbNote.AppendLine();

                                info = ReplaceWitBreakLineHTML(rim, "Info5");
                                info = AuditLogLinkMenu(rim, "Info5", info);
                                sbNote.AppendFormat("<tr><td class='label' valign='top' style='font-weight: bold;width:{1}px; padding-left:2px'>TBAK:</td><td valign='top'>{0}</td></tr>", info, col1Width);
                                sbNote.AppendLine();

                                info = ReplaceWitBreakLineHTML(rim, "ReceiveBy");
                                sbNote.AppendFormat("<tr><td class='label' valign='top' style='font-weight: bold;width:{1}px; padding-left:2px'>ReceiveBy:</td><td valign='top'>{0}</td></tr>", info, col1Width);
                                sbNote.AppendLine();
                            }
                            else if (medicalNotesInputType == "ADIME")
                            {
                                var info = ReplaceWitBreakLineHTML(rim, "Info5");
                                info = AuditLogLinkMenu(rim, "Info5", info);
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
                            var consultType = string.Empty;
                            var answerMenu = string.Empty;
                            var csl = new ParamedicConsultRefer();

                            if (csl.LoadByPrimaryKey(rim["ReferenceNo"].ToString()))
                            {
                                if (csl.ToParamedicID == AppSession.UserLogin.ParamedicID)
                                    answerMenu = string.Format(" <a href=\"javascript:void(0);\" onclick=\"javascript:entryParamedicConsultAnswer('{0}','{1}')\"><img src='{2}/Images/Toolbar/edit16.png'/></a>",
                                        rim["ReferenceNo"], rim["RegistrationNo"], Helper.UrlRoot());

                                if (!string.IsNullOrWhiteSpace(csl.SRParamedicConsultType))
                                {
                                    var sri = new AppStandardReferenceItem();
                                    if (sri.LoadByPrimaryKey("ParamedicConsultType", csl.SRParamedicConsultType))
                                        consultType = sri.ItemName;
                                }
                            }

                            sbNote.AppendFormat(
                                "<tr><td colspan='2' style='font-weight: bold;padding-left:2px;'>{1} to : {0}</td></tr>",
                                rim["Info1"], medicalNotesInputType == "REF" ? "Refer" : "Consult");


                            sbNote.AppendFormat("<tr><td colspan='2' style='padding-left:2px;'>Type : {0}</td></tr>", consultType);

                            sbNote.AppendFormat("<tr><td colspan='2' style='padding-left:2px;'>Notes :</td></tr>");
                            var info2 = AuditLogLinkMenu(rim, "Info2", FormatToHtml(rim["Info2"]));
                            sbNote.AppendFormat("<tr><td width:10px;'>&nbsp;</td><td>{0}</td></tr>", info2);
                            sbNote.AppendLine();

                            sbNote.AppendFormat(
                                "<tr><td colspan='2' style='padding-left:2px;'>Action / Examination / Treatment :</td></tr>");

                            var info3 = AuditLogLinkMenu(rim, "Info3", FormatToHtml(rim["Info3"]));
                            sbNote.AppendFormat("<tr><td  width:10px;'>&nbsp;</td><td>{0}</td></tr>", info3);
                            sbNote.AppendLine();

                            sbNote.AppendFormat("<tr><td colspan='2' style='padding-left:2px;'>{0}Answer :</td></tr>", answerMenu);
                            var aswr = AuditLogLinkMenu(rim, "Answer", FormatToHtml(csl.Answer));

                            sbNote.AppendFormat("<tr><td width:10px'>&nbsp;</td><td>{0}</td></tr>",
                                aswr);
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
                                // Generate summary untuk data lama yg tidak disimpan di asesmen field
                                // Data summary PPA Notes respond tempate disimpan di kolom Objective per update app tgl 07 ds 20023 (Handono)
                                if (rim["Info2"] == DBNull.Value || string.IsNullOrWhiteSpace(rim["Info2"].ToString()))
                                {
                                    var phrlColl = new PatientHealthRecordLineCollection();
                                    if (phrlColl.LoadByTransactionNoRegNoOfTemplateEntry(rim["ReferenceToPhrNo"].ToString(), rim["RegistrationNo"].ToString()))
                                    {
                                        rim["Info2"] = rim["Info2"] + " " +
                                                       RADT.Emr.NursingImplementationEntry.ParsePhrlRespond(phrlColl, questions);
                                    }
                                }
                            }

                            var info2 = ReplaceWitBreakLineHTML(rim, "Info2");


                            if (!string.IsNullOrEmpty(info2))
                            {
                                //0003994 daniel
                                if (AppSession.Parameter.HealthcareInitial == "RSI")
                                {
                                    sbNote.AppendFormat(
"<tr><td class='label' valign='top' style='font-weight: bold; width:10px;padding-left:2px'>{0}:</td><td valign='top'>{1}{2}</td></tr>",
"Respond", info2,
(rim["TemplateID"].ToString() == string.Empty || rim["TemplateID"].ToString() == "0")
    ? ""
   : string.Format(
" <a href=\"javascript:void(0);\" onclick=\"javascript:OpenTableRespond('{0}')\"><sub><img src='{2}/Images/Toolbar/views16.png'/></sub></a> | " +
"<a href=\"javascript:void(0);\" onclick=\"javascript:printPreviewTemplatePpaNotes('{0}', '{1}')\"><sub><img src='{2}/Images/Toolbar/print16.png'/></sub></a>",
rim["TemplateID"].ToString(),
rim["RegistrationNo"].ToString(),
Helper.UrlRoot()
)
);
                                    sbNote.AppendLine();
                                }
                                else
                                {

                                    sbNote.AppendFormat(
                                        "<tr><td class='label' valign='top' style='font-weight: bold; width:10px;padding-left:2px'>{0}:</td><td valign='top'>{1}{2}</td></tr>",
                                        "Respond", info2,
                                        (rim["TemplateID"].ToString() == string.Empty || rim["TemplateID"].ToString() == "0")
                                            ? ""
                                            : string.Format(" <a href=\"javascript:void(0);\" onclick=\"javascript:OpenTableRespond('{0}')\"><sub><img src='{1}/Images/Toolbar/views16.png'/></sub></a>",
                                              rim["TemplateID"].ToString(), Helper.UrlRoot()));
                                    sbNote.AppendLine();

                                }
                                //0003994 daniel

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

            return dtb;
        }

        #region Link menu auditLog
        //private static StringBuilder StrbResponseScripts;
        //protected override void OnLoadComplete(EventArgs e)
        //{
        //    base.OnLoadComplete(e);
        //    if (IsPostBack)
        //        AjaxManager.ResponseScripts.Add(StrbResponseScripts.ToString());
        //    //else
        //    //    StrbResponseScripts = new StringBuilder();
        //}

        private static string AuditLogLinkMenu(DataRow rim, string fieldName, string prevText)
        {
            return prevText; //YBRS tidak mau dimunculkan icon edit history dengan alasan bukan untuk konsumsi umum (Handono 231209)

            ////// Remove end <br />
            ////prevText = prevText.TrimEnd(new Char[] { ' ', '>', '/', 'r', 'b', '<' });

            ////var keyId = string.Empty;
            ////if ("REF_CON".Contains(rim["SRMedicalNotesInputType"].ToString()) && fieldName == "Answer")
            ////    keyId = rim["ReferenceNo"].ToString();
            ////else
            ////    keyId = rim["RegistrationInfoMedicID"].ToString();


            ////// Update link menu later via webservice
            ////var strbLink = new StringBuilder();
            ////var spanId = string.Format("aulcppt_{0}_{1}", keyId.ToString().Replace("/", "_"), fieldName);
            ////strbLink.AppendFormat("<span id=\"{0}\"></span>", spanId);
            ////strbLink.Append(" <script type=\"text/javascript\">");

            ////var script = string.Format("UpdateStateAuditLogCppt(\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\");", spanId, rim["SRMedicalNotesInputType"].ToString(), rim["SRAssessmentType"].ToString(), keyId, fieldName);
            ////strbLink.Append(script);
            ////strbLink.Append("</script>");

            ////// Tampung script untuk diregistrasi pada AjaxManager.ResponseScripts karena AJAX update tidak akan menjalankan script yg ada di page
            //////StrbResponseScripts.AppendLine(script);

            ////return string.Concat(prevText, strbLink.ToString());
        }
        #endregion

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


        private static string GetParamedicName(string paramedicID, string defaultParamedicName)
        {
            var paramedic = new Paramedic();
            var paramedicName = paramedic.LoadByPrimaryKey(paramedicID)
                ? paramedic.ParamedicName
                : defaultParamedicName;
            return paramedicName;
        }

        protected void grdAssessment_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var filterEntry = string.Empty;
            if (!string.IsNullOrWhiteSpace(hdnFilterIntegratedNotes.Value))
            {
                filterEntry = hdnFilterIntegratedNotes.Value;
            }
            grdAssessment.DataSource = null;
            grdAssessment.DataSource = RegistrationInfoMedicDataTable(RegistrationType, RegistrationNo, MergeRegistrations, PatientID, filterEntry);
            PopulateEpisodeDiagnose();
            PopulatePatientAllergy();
            PopulateImmunizationHistory();
            PopulatePatientDialysis();

        }
        #endregion

        #region PHR
        public static DataTable PatientHeathRecordDataTable(List<string> registrationNoList)
        {
            // Display semua PHR krn untuk keperluan list history
            var query = new PatientHealthRecordQuery("phr");
            var form = new QuestionFormQuery("f");
            var userQr = new AppUserQuery("usr");
            var reg = new RegistrationQuery("x");
            var par = new ParamedicQuery("y");
            var unit = new ServiceUnitQuery("z");

            query.InnerJoin(form).On(query.QuestionFormID == form.QuestionFormID);
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.LeftJoin(par).On(reg.ParamedicID == par.ParamedicID);
            query.LeftJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
            query.LeftJoin(userQr).On(query.CreateByUserID == userQr.UserID);

            //if (string.IsNullOrEmpty(ReferFromRegistrationNo))
            //    query.Where(query.RegistrationNo == RegistrationNo);
            //else
            //    query.Where(query.Or(query.RegistrationNo == ReferFromRegistrationNo, query.RegistrationNo == RegistrationNo));

            if (registrationNoList.Count > 1)
                query.Where(query.RegistrationNo.In(registrationNoList));
            else
                query.Where(query.RegistrationNo == registrationNoList[0]);

            query.Where(query.Or(form.SRQuestionFormType.IsNull(), form.SRQuestionFormType != QuestionForm.QuestionFormType.PatientLetter));
            query.Select(
                query.TransactionNo,
                reg.RegistrationNo,
                par.ParamedicName,
                unit.ServiceUnitName,
                unit.ServiceUnitID, // Untuk keperluan hak akses
                query.QuestionFormID,
                form.QuestionFormName,
                @"<CAST(CONVERT(VARCHAR(10), phr.RecordDate, 112) + ' ' + phr.RecordTime AS DATETIME) AS RecordDateTime>",
                userQr.UserName.As("CreatedByUserName"),
                query.IsComplete,
                query.ReferenceNo,
                form.RmNO,
                query.CreateByUserID,
                query.IsApproved,
                form.IsSharingEdit
                );

            return query.LoadDataTable();
        }

        //protected void grdPhr_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        //{
        //    grdPhr.DataSource = PatientHeathRecordDataTable(RelatedRegistrations);
        //    grdVitalSign.Rebind();

        //}
        //protected object PhrEditLink(GridItem container)
        //{
        //    var isEditAble = (Eval("IsApproved") == DBNull.Value || false.Equals(Eval("IsApproved"))) && (Eval("IsSharingEdit") != DBNull.Value && true.Equals(Eval("IsSharingEdit")) || AppSession.UserLogin.UserID.Equals(Eval("CreateByUserID")));
        //    return string.Format(
        //        "<a href=\"#\" onclick=\"entryPhr('{4}', '{0}','{1}','{2}','{3}'); return false;\"><img src=\"../../../Images/Toolbar/{5}16.png\" border=\"0\" /></a>",
        //        Eval("TransactionNo"), Eval("RegistrationNo"), Eval("QuestionFormID"), Eval("ServiceUnitID"),
        //        isEditAble ? "edit" : "view",
        //        isEditAble ? "edit" : "views");
        //}
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
                case "imunization":
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


        private bool CheckAccess
        {
            get
            {
                if (!IsUserEditAble)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "unauthorized", "alert('Unauthorized access');", true);
                    return false;
                }
                return true;
            }
        }


        #region PatientImage
        private void PopulatePatientImage(string patientID)
        {
            // Patient Photo
            imgPatientPhoto.ImageUrl = string.Empty;

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


        // Dipindah ke User Control
        //#region Question Form
        //protected void grdVitalSign_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        //{
        //    grdVitalSign.DataSource = VitalSign.VitalSignLastValue(RegistrationNo, MergeRegistrations, true, DateTime.Now);
        //}

        //private DataTable QuestionFormDatatable(string serviceUnitID, string patientID, string registrationNo)
        //{
        //    var isNewPatient = true;
        //    // Check status new patient at clinic
        //    // Lebih tepat jika PatientAssessment baru 1 di Service Unit terpilih tapi akan bermasalah jika assessment dihapus
        //    var regColl = new RegistrationCollection();
        //    var regQuery = new RegistrationQuery();
        //    regQuery.Where(regQuery.PatientID == patientID, regQuery.ServiceUnitID == serviceUnitID, regQuery.RegistrationNo < registrationNo, regQuery.IsVoid == false);
        //    regQuery.es.Top = 1;
        //    if (regColl.Load(regQuery))
        //    {
        //        isNewPatient = regColl.Count == 0;
        //    }

        //    var query = new QuestionFormQuery("a");
        //    var suQr = new QuestionFormInServiceUnitQuery("s");

        //    query.InnerJoin(suQr).On(query.QuestionFormID == suQr.QuestionFormID && suQr.ServiceUnitID == serviceUnitID);
        //    if (isNewPatient)
        //        query.Where(query.IsActive == true && query.IsInitialAssessment == true);
        //    else
        //        query.Where(query.IsActive == true && query.IsContinuedAssessment == true);

        //    // Berdasarkan Form Type
        //    query.Where(query.Or(query.SRQuestionFormType.IsNull(), query.SRQuestionFormType == string.Empty,
        //        query.SRQuestionFormType == QuestionForm.QuestionFormType.PatientTransfer,
        //        query.SRQuestionFormType == QuestionForm.QuestionFormType.General));

        //    // Berdasarkan tipe user
        //    query.Where(query.Or(query.RestrictionUserType.IsNull(), query.RestrictionUserType == string.Empty,
        //        query.RestrictionUserType.Like("%" + AppSession.UserLogin.SRUserType + "%")));

        //    query.Select(string.Format("<'{0}' as registrationNo>", registrationNo),
        //        query.QuestionFormID,
        //        query.QuestionFormName,
        //        query.IsSingleEntry.Coalesce("0").As("IsSingleEntry"), @"<CAST(1 AS BIT) AS IsNewEnable>");

        //    query.OrderBy(query.QuestionFormName.Ascending);

        //    var dtb = query.LoadDataTable();
        //    foreach (DataRow row in dtb.Rows)
        //    {
        //        if (Convert.ToBoolean(row["IsSingleEntry"]))
        //        {
        //            var phr = new PatientHealthRecordCollection();
        //            phr.Query.Where(phr.Query.RegistrationNo == registrationNo,
        //                phr.Query.QuestionFormID == row["QuestionFormID"].ToString());
        //            phr.LoadAll();
        //            row["IsNewEnable"] = phr.Count == 0;
        //        }
        //    }
        //    dtb.AcceptChanges();

        //    return dtb;
        //}

        //#endregion

        public static string IntegratedNoteScript(GridItem gridItem)
        {

            var script = string.Empty;
            //if (true.Equals(DataBinder.Eval(gridItem.DataItem, "IsApproved")))
            //    script = string.Format(
            //        "<div style='background-image: url({1}/Images/Verified32.png);background-size: 80px 40px; background-repeat: no-repeat; background-position: center;'>{0}</div>",
            //        DataBinder.Eval(gridItem.DataItem, "SOAP"), Helper.UrlRoot());
            //else
            script = Convert.ToString(DataBinder.Eval(gridItem.DataItem, "SOAP"));

            //Dokter: Putih, 
            //Perawat: Biru, 
            //Farmasi: Kuning, 
            //Nutrition: Hijau
            //Rehabmedis: Merah

            var backgroundNotApprove = "#E7F1FF";
            //(Dokter-Putih, Perawat-Biru, Farmasi-Kuning, Gizi-Hijau, Fisiotrapi-Merah)
            var createdByUserType = DataBinder.Eval(gridItem.DataItem, "SRUserType");
            var isApproved = true.Equals(DataBinder.Eval(gridItem.DataItem, "IsApproved"));
            if (AppUser.UserType.Doctor.Equals(createdByUserType))
            {
                // Untuk entrian yg dilakukan oleh dokter dpjp (dokter penanggung jawab pasien)
                if (DataBinder.Eval(gridItem.DataItem, "IsCreatedByUserDpjp") != DBNull.Value && DataBinder.Eval(gridItem.DataItem, "IsCreatedByUserDpjp").ToBoolean())
                    script = string.Format("<div style='color: #000081;'>{0}</div>", script);
            }
            else if (AppUser.UserType.Nurse.Equals(createdByUserType))
            {
                script = string.Format("<div style='background-color: {1}'>{0}</div>", script, isApproved ? "#B2E1E2" : backgroundNotApprove);
            }
            else if (AppUser.UserType.Pharmacy.Equals(createdByUserType))
            {
                script = string.Format("<div style='background-color: {1}'>{0}</div>", script, isApproved ? "#FCFC99" : backgroundNotApprove);
            }
            else if (AppUser.UserType.Nutrition.Equals(createdByUserType))
            {
                script = string.Format("<div style='background-color: {1}'>{0}</div>", script, isApproved ? "#D2FEA6" : backgroundNotApprove);
            }
            else if (AppUser.UserType.Rehabilitation.Equals(createdByUserType))
            {
                script = string.Format("<div style='background-color: {1}'>{0}</div>", script, isApproved ? "#FFB7B7" : backgroundNotApprove);
            }
            else if (AppUser.UserType.Physiotherapy.Equals(createdByUserType))
            {
                script = string.Format("<div style='background-color: {1}'>{0}</div>", script, isApproved ? "#FBECBE" : backgroundNotApprove);
            }
            else
            {
                if (!isApproved)
                    script = string.Format("<div style='background-color: {1}'>{0}</div>", script, backgroundNotApprove);
            }


            return script;
        }

        public static string AdditionalNoteScript(GridItem gridItem)
        {
            if (!string.Empty.Equals(DataBinder.Eval(gridItem.DataItem, "AssessmentTypeName")))
            {
                var pa = new PatientAssessment();
                if (pa.LoadByPrimaryKey(DataBinder.Eval(gridItem.DataItem, "RegistrationInfoMedicID").ToString()))
                {
                    if (!string.IsNullOrWhiteSpace(pa.AdditionalNotes))
                    {
                        return string.Format("<br /><fieldset><legend>Additional Notes</legend>{0}</fieldset>", pa.AdditionalNotes);
                    }
                }

            }
            return string.Empty;
        }
        #region User Access
        protected bool IsUserCanNotAdd()
        {
            // Step 1 Cek bisa tambah
            if (this.IsUserAddAble.Equals(false)) return true;

            // Ste 2 Cek user paramedic
            if (string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID))
            {
                // User selain dokter bisa tambah record dan hak aksesnya diset di entriannya page yg dipanggil
                return false;
            }

            if (AppSession.UserLogin.ParamedicID.Equals(ParamedicID))
                return false;


            return !IsUserInParamedicTeam();
        }

        protected bool IsUserEntryAssessment()
        {
            if (this.IsUserAddAble.Equals(false)) return false;

            if (AppSession.Parameter.IsByPassEmrUserTypeRestriction) return true;

            // Hanya dokter team dan dokter jaga
            return (AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor && IsUserInParamedicTeam());
        }

        protected bool IsUserEntryOperatingNotes()
        {
            if (this.IsUserEditAble.Equals(false)) return false;

            // Hanya dokter team
            return (AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor && IsUserInParamedicTeam());
        }

        protected bool IsUserEntryReferConsulToSpecialist()
        {
            // Hanya dokter utama
            return (AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor && IsUserParamedicDpjp());
        }

        protected bool IsUserEntryDischarge()
        {
            // Perawat dan dokter utama
            return AppSession.UserLogin.SRUserType == AppUser.UserType.Nurse || (AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor && IsUserParamedicDpjp());
        }
        protected bool IsUserEntrySurgical()
        {
            // Perawat dan dokter team
            return AppSession.UserLogin.SRUserType == AppUser.UserType.Nurse || IsUserInParamedicTeam();
        }
        #endregion

        #region Integrated Note Column Menu


        public static bool IntegratedNoteDeleteable(GridItem container)
        {
            var retVal = "SOAP;SBAR;ADIME;Notes".Contains(DataBinder.Eval(container.DataItem, "SRMedicalNotesInputType").ToString())
                         //&& string.IsNullOrWhiteSpace(DataBinder.Eval(container.DataItem, "AssessmentTypeName").ToString())
                         && false.Equals(DataBinder.Eval(container.DataItem, "IsDeleted"))
                         && false.Equals(DataBinder.Eval(container.DataItem, "IsApproved"))
                          && DataBinder.Eval(container.DataItem, "CreatedByUserID").Equals(AppSession.UserLogin.UserID)
                          && !DataBinder.Eval(container.DataItem, "SRNursingDiagnosaLevel").Equals("40");
            return retVal;
        }
        public static bool IntegratedNoteUnDeleteable(GridItem container)
        {
            var retVal = "SOAP;SBAR;ADIME;Notes".Contains(DataBinder.Eval(container.DataItem, "SRMedicalNotesInputType").ToString())
                         && true.Equals(DataBinder.Eval(container.DataItem, "IsDeleted"))
                          && DataBinder.Eval(container.DataItem, "CreatedByUserID").Equals(AppSession.UserLogin.UserID)
                          && !DataBinder.Eval(container.DataItem, "SRNursingDiagnosaLevel").Equals("40");
            return retVal;
        }

        private static string IntegratedNotePrintLink(GridItem container)
        {
            if ("SOAP;MDS;".Contains(DataBinder.Eval(container.DataItem, "SRMedicalNotesInputType").ToString())
                && DataBinder.Eval(container.DataItem, "IsFromAskep").Equals(false)
                && (false.Equals(DataBinder.Eval(container.DataItem, "IsDeleted"))))
                return string.Format(
                    "<a href=\"#\" onclick=\"javascript:printPreviewIntegratedNotes('{0}'); return false;\"><img src=\"{1}/Images/Toolbar/print16.png\"  alt=\"Print Integrates Notes\" /></a><br /><br />",
                    DataBinder.Eval(container.DataItem, "RegistrationInfoMedicID"), Helper.UrlRoot());
            return string.Empty;
        }

        public static string IntegratedNoteVerifPrintEditLink(GridItem container, bool isUserParamedicDpjp)
        {
            if (DataBinder.Eval(container.DataItem, "IsDeleted") != DBNull.Value &&
                true.Equals(DataBinder.Eval(container.DataItem, "IsDeleted")))
                return string.Empty;

            // Yg bisa memverifikasi adalah
            // DPJP : Semua CPPT (SOAP;SBAR;ADIME;Notes;Handover Patient)
            // Dokter Team : CPPT yg hanya ditujukan utk dokter tsb
            //var isAccessVerif = !string.IsNullOrWhiteSpace(AppSession.UserLogin.ParamedicID) && AppSession.UserLogin.SRUserType == UserLogin.UserType.Doctor &&
            //    "SOAP;SBAR;ADIME;Notes;Handover Patient".Contains(DataBinder
            //        .Eval(container.DataItem, "SRMedicalNotesInputType").ToString())
            //    && string.IsNullOrWhiteSpace(DataBinder.Eval(container.DataItem, "AssessmentTypeName").ToString())
            //    && !true.Equals(DataBinder.Eval(container.DataItem, "IsDeleted"))
            //    && !true.Equals(DataBinder.Eval(container.DataItem, "IsApproved"))
            //    && (isUserParamedicDpjp || AppSession.UserLogin.ParamedicID.Equals(DataBinder.Eval(container.DataItem, "ParamedicID")));

            // #TK-634: Handono (230902) 
            // Verifikasi CPPT: SOAP;SBAR;ADIME;Notes;Handover Patient
            // Yg bisa memverifikasi adalah
            // DPJP : CPPT yg hanya ditujukan utk dokter tsb + tidak didefinisikan ParamedicID nya
            // Selain DPJP : CPPT yg hanya ditujukan utk dokter tsb
            var cpptParamedicID = DataBinder.Eval(container.DataItem, "ParamedicID") == null ? string.Empty : DataBinder.Eval(container.DataItem, "ParamedicID").ToString();
            var isAccessVerif = false;
            if (!true.Equals(DataBinder.Eval(container.DataItem, "IsDeleted")) && !true.Equals(DataBinder.Eval(container.DataItem, "IsApproved")))
            {
                if (string.IsNullOrWhiteSpace(DataBinder.Eval(container.DataItem, "AssessmentTypeName").ToString()) && "SOAP;SBAR;ADIME;Notes;Handover Patient".Contains(DataBinder.Eval(container.DataItem, "SRMedicalNotesInputType").ToString()))
                {
                    if (!string.IsNullOrWhiteSpace(AppSession.UserLogin.ParamedicID) && (AppSession.UserLogin.SRUserType == UserLogin.UserType.Doctor))
                    {
                        isAccessVerif = ((isUserParamedicDpjp && string.IsNullOrWhiteSpace(cpptParamedicID)) || AppSession.UserLogin.ParamedicID.Equals(cpptParamedicID));
                    }
                }
            }

            var verifLink = !isAccessVerif
                ? string.Empty
                : string.Format(
                    "<a style=\"cursor:pointer;\" onclick=\"javascript:openCpptVerification('{0}_{1}','{2}','{3}')\"><img style=\"border: 0px; vertical-align: middle;\" src=\"{4}/Images/Toolbar/post16.png\" alt=\"\"/></a><br /><br />",
                    DataBinder.Eval(container.DataItem, "RegistrationInfoMedicID"),
                    DataBinder.Eval(container.DataItem, "IsFromAskep"),
                    DataBinder.Eval(container.DataItem, "ParamedicID"), isUserParamedicDpjp, Helper.UrlRoot());


            // Hanya user yg membuat yg bisa mengedit
            var isEditable = (DataBinder.Eval(container.DataItem, "IsApproved") == DBNull.Value ||
                              false.Equals(DataBinder.Eval(container.DataItem, "IsApproved")))
                             && DataBinder.Eval(container.DataItem, "CreatedByUserID")
                                 .Equals(AppSession.UserLogin.UserID);

            var icon = isEditable ? "edit16.png" : "views16.png";
            var mode = isEditable ? "edit" : "view";

            var editLink = string.Format(
                "<a href=\"#\" onclick=\"javascript:entryAssessment('{7}', '{0}', '{1}','{2}','{3}','{4}','{8}','{9}','{10}','{11}'); return false;\"><img src=\"{5}/Images/Toolbar/{6}\"  alt=\"{7}\" /></a><br /><br />",
                DataBinder.Eval(container.DataItem, "RegistrationNo"),
                DataBinder.Eval(container.DataItem, "ServiceUnitID"),
                DataBinder.Eval(container.DataItem, "SRAssessmentType"),
                DataBinder.Eval(container.DataItem, "RegistrationInfoMedicID"),
                true.Equals(DataBinder.Eval(container.DataItem, "IsInitialAssessment")),
                Helper.UrlRoot(), icon, mode,
                DataBinder.Eval(container.DataItem, "SRMedicalNotesInputType"),
                DataBinder.Eval(container.DataItem, "ReferenceNo"),
                DataBinder.Eval(container.DataItem, "FromRegistrationNo"),
                DataBinder.Eval(container.DataItem, "ParamedicID")
            );

            var printLink = IntegratedNotePrintLink(container);

            var link = string.Format("{0}{1}{2}", verifLink,
                editLink,
                printLink);
            return link;
        }

        #endregion

        protected void grdAssessment_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            //TODO: Custom filter grid belum diteruskan
            if (e.Item is GridFilteringItem)
            {
                var cboSRUserType = (RadComboBox)e.Item.FindControl("cboSRUserType");
                StandardReference.InitializeIncludeSpace(cboSRUserType, AppEnum.StandardReference.UserType);
            }
        }


        #region Medication History

        private DataTable MedicationReceiveDataTable()
        {
            var query = new MedicationReceiveQuery("a");
            var reg = new RegistrationQuery("r");
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);

            var cm = new ConsumeMethodQuery("cm");
            query.LeftJoin(cm).On(query.SRConsumeMethod == cm.SRConsumeMethod);

            var mc = new AppStandardReferenceItemQuery("mc");
            query.LeftJoin(mc).On(query.SRMedicationConsume == mc.ItemID &&
                                  mc.StandardReferenceID == "MedicationConsume");

            var item = new ItemProductMedicQuery("im");
            if (chkIsAntibiotic.Checked)
            {
                // Add filter just antibiotik
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.Where(item.IsAntibiotic == true);
            }
            else
                query.LeftJoin(item).On(query.ItemID == item.ItemID);

            query.Select
            (
                query,
                reg.PatientID,
                cm.SRConsumeMethodName,
                item.IsAntibiotic,
                mc.ItemName.As("SRMedicationConsumeName")

            );

            if (chkIsIncStopped.Checked == false)
            {
                query.Where(query.IsContinue == true);
            }
            if (chkIsIncVoided.Checked == false)
            {
                query.Where(query.Or(query.IsVoid == false, query.IsVoid.IsNull()));
            }

            if (chkIsOnlyUsed.Checked == true)
            {
                // Add filter just consumed
                var qrUsed = new MedicationReceiveUsedQuery("usd");
                qrUsed.Select(qrUsed.MedicationReceiveNo);
                qrUsed.es.Distinct = true;
                qrUsed.Where(qrUsed.ScheduleDateTime >= txtFromDate.SelectedDate.Value.Date);

                query.Where(query.MedicationReceiveNo.In(qrUsed));
            }

            // Tampilkan 1 episodenya
            query.Where(query.RegistrationNo.In(MergeRegistrations));
            query.OrderBy(query.RefTransactionNo.Ascending, query.RefSequenceNo.Ascending, query.ItemDescription.Ascending);

            var dtb = query.LoadDataTable();

            return dtb;
        }


        protected void btnMedHistRefresh_Click(object sender, ImageClickEventArgs e)
        {
            medicationHistCtl.SetDataSource(MedicationReceiveDataTable(), txtFromDate.SelectedDate);
        }

        protected void btnMedHistStartFromRegistration_Click(object sender, EventArgs e)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(!string.IsNullOrEmpty(ReferFromRegistrationNo) ? ReferFromRegistrationNo : RegistrationNo);
            txtFromDate.SelectedDate = reg.RegistrationDate;

            medicationHistCtl.SetDataSource(MedicationReceiveDataTable(), txtFromDate.SelectedDate);
        }


        protected void btnMedHistStartFromLast3Day_Click(object sender, EventArgs e)
        {
            txtFromDate.SelectedDate = DateTime.Today.AddDays(-3);
            medicationHistCtl.SetDataSource(MedicationReceiveDataTable(), txtFromDate.SelectedDate);
        }

        protected void btnMedHistStartFromCurrentDay_Click(object sender, EventArgs e)
        {
            txtFromDate.SelectedDate = DateTime.Today;
            medicationHistCtl.SetDataSource(MedicationReceiveDataTable(), txtFromDate.SelectedDate);
        }

        protected void btnMedHistStartFromLast1Day_Click(object sender, EventArgs e)
        {
            txtFromDate.SelectedDate = DateTime.Today.AddDays(-1);
            medicationHistCtl.SetDataSource(MedicationReceiveDataTable(), txtFromDate.SelectedDate);
        }
        #endregion


    }
}
