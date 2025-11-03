using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
namespace Temiang.Avicenna.Module.RADT.Emergency
{
    public partial class DetailRegistrationAndDischargeDetail : BasePageDialog
    {
        private AppAutoNumberLast _autoNumber;

        private bool? _isEmrDiagnoseFreeText = null;
        private bool IsEmrDiagnoseFreeText
        {
            get
            {
                if (_isEmrDiagnoseFreeText == null)
                    _isEmrDiagnoseFreeText = AppParameter.IsYes(AppParameter.ParameterItem.IsEmrDiagnoseFreeText);

                return _isEmrDiagnoseFreeText ?? false;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.DetailRegistrationEmrDischarge;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRPatientInCondition, AppEnum.StandardReference.PatientInCondition);
                StandardReference.InitializeIncludeSpace(cboSRPatientInType, AppEnum.StandardReference.PatientInType, AppConstant.RegistrationType.EmergencyPatient);
                StandardReference.InitializeIncludeSpace(cboSRERCaseType, AppEnum.StandardReference.ERCaseType);
                StandardReference.InitializeIncludeSpace(cboSRVisitReason, AppEnum.StandardReference.VisitReason);
                StandardReference.InitializeIncludeSpace(cboSRTriage, AppEnum.StandardReference.Triage);
                StandardReference.InitializeIncludeSpace(cboSRDischargeCondition, AppEnum.StandardReference.DischargeCondition, AppConstant.RegistrationType.EmergencyPatient);
                StandardReference.InitializeIncludeSpace(cboSRDischargeMethod, AppEnum.StandardReference.DischargeMethod, AppConstant.RegistrationType.EmergencyPatient);
                StandardReference.InitializeIncludeSpace(cboSRReferralGroup, AppEnum.StandardReference.ReferralGroup);
                StandardReference.InitializeIncludeSpace(cboSRCrashSite, AppEnum.StandardReference.CrashSite);
                StandardReference.InitializeIncludeSpace(cboCovidStatus, AppEnum.StandardReference.CovidStatus);

                cboEmrDiagnoseID.Visible = !IsEmrDiagnoseFreeText;
                txtEmrDiagnoseID.Visible = IsEmrDiagnoseFreeText;

                rfvDischargeDate.Visible = AppSession.Parameter.IsDischargeDateOnEmrMandatory;
                rfvSRDischargeMethod.Visible = rfvDischargeDate.Visible;
                rfvSRDischargeCondition.Visible = rfvDischargeDate.Visible;
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
                txtServiceUnitID.Text = reg.ServiceUnitID;

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                txtPatientID.Text = reg.PatientID;
                txtMedicalNo.Text = pat.MedicalNo;
                txtPatientName.Text = pat.PatientName;

                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;
                txtGender.Text = pat.Sex;
                txtPlaceDOB.Text = string.Format("{0}, {1}", pat.CityOfBirth, Convert.ToDateTime(pat.DateOfBirth).ToString("dd-MMM-yyyy"));

                txtFirstResponDate.SelectedDate = reg.FirstResponDate;
                txtFirstResponTime.Text = reg.FirstResponTime;
                txtPhysicianResponDate.SelectedDate = reg.PhysicianResponDate;
                txtPhysicianResponTime.Text = reg.PhysicianResponTime;

                cboSRPatientInCondition.SelectedValue = reg.SRPatientInCondition;
                cboSRPatientInType.SelectedValue = reg.SRPatientInType;
                cboSRERCaseType.SelectedValue = reg.SRERCaseType;
                cboSRVisitReason.SelectedValue = reg.SRVisitReason;

                if (!string.IsNullOrEmpty(reg.SRVisitReason))
                {
                    PopulatecboReasonForTreatmentList(reg.SRVisitReason);
                    if (!string.IsNullOrEmpty(reg.ReasonsForTreatmentID))
                    {
                        cboReasonForTreatmentID.SelectedValue = reg.ReasonsForTreatmentID;
                        PopulatecboReasonForTreatmentDescList(reg.SRVisitReason, reg.ReasonsForTreatmentID);

                        if (!string.IsNullOrEmpty(reg.ReasonsForTreatmentDescID))
                        {
                            cboReasonsForTreatmentDescID.SelectedValue = reg.ReasonsForTreatmentDescID;
                        }
                    }
                }

                cboSRTriage.SelectedValue = reg.SRTriage;

                if (IsEmrDiagnoseFreeText)
                {
                    txtEmrDiagnoseID.Text = reg.EmrDiagnoseID;
                }
                else if (!string.IsNullOrEmpty(reg.EmrDiagnoseID))
                {
                    var query = new EmergencyDiagnoseQuery();
                    query.Where(query.EmrDiagnoseID == reg.EmrDiagnoseID);
                    cboEmrDiagnoseID.DataSource = query.LoadDataTable();
                    cboEmrDiagnoseID.DataBind();

                    cboEmrDiagnoseID.SelectedValue = reg.EmrDiagnoseID;
                }

                cboSRReferralGroup.SelectedValue = reg.SRReferralGroup;
                if (!string.IsNullOrEmpty(reg.ReferralID))
                {
                    var query = new ReferralQuery();
                    query.Where(query.ReferralID == reg.ReferralID);
                    cboReferralID.DataSource = query.LoadDataTable();
                    cboReferralID.DataBind();

                    cboReferralID.SelectedValue = reg.ReferralID;
                }

                txtReferralName.Text = reg.ReferralName;
                ReadOnlyReferralName();

                txtDischargeDate.SelectedDate = reg.DischargeDate ?? (new DateTime()).NowAtSqlServer().Date;
                txtDischargeTime.Text = string.IsNullOrEmpty(reg.DischargeTime) ? (new DateTime()).NowAtSqlServer().ToString("HH:mm") : reg.DischargeTime;
                txtDischargeMedicalNotes.Text = reg.DischargeMedicalNotes;
                txtDischargeNotes.Text = reg.DischargeNotes;
                cboSRDischargeCondition.SelectedValue = reg.SRDischargeCondition;
                cboSRDischargeMethod.SelectedValue = reg.SRDischargeMethod;
                txtDeathCertificateNo.Text = reg.DeathCertificateNo;

                chkIsOldCase.Checked = reg.IsOldCase ?? false;
                chkIsObservation.Checked = reg.IsObservation ?? false;
                chkIsDHF.Checked = reg.IsDHF ?? false;
                chkIsEKG.Checked = reg.IsEKG ?? false;
                txtCauseOfAccident.Text = reg.CauseOfAccident;
                cboSRCrashSite.SelectedValue = reg.SRCrashSite;
                txtCrashSiteDetail.Text = reg.CrashSiteDetail;
                txtReferTo.Text = reg.ReferTo;

                cboCovidStatus.SelectedValue = reg.SRCovidStatus == null ? string.Empty : reg.SRCovidStatus.ToString();

                PopulateTreatmentForAnimalBitesGrid();
            }
        }

        public override bool OnButtonOkClicked()
        {
            Validate();

            if (!IsValid)
                return false;

            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
            {
                if (string.IsNullOrEmpty(cboSRPatientInCondition.SelectedValue))
                {
                    ShowInformationHeader("Patient Initial Condition is required.");
                    return false;
                }
            }

            if (!AppSession.Parameter.IsDischargeDateOnEmrMandatory && !txtDischargeDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                if (string.IsNullOrEmpty(cboSRDischargeMethod.SelectedValue))
                {
                    ShowInformationHeader("Discharge Method is required.");
                    return false;
                }

                if (string.IsNullOrEmpty(cboSRDischargeCondition.SelectedValue))
                {
                    ShowInformationHeader("Discharge Condition is required.");
                    return false;
                }
            }

            using (esTransactionScope trans = new esTransactionScope())
            {
                var entity = new Registration();
                entity.LoadByPrimaryKey(txtRegistrationNo.Text);

                entity.SRCovidStatus = string.IsNullOrEmpty(cboCovidStatus.SelectedValue) ? Convert.ToByte(0) : byte.Parse(cboCovidStatus.SelectedValue);
                entity.SRReferralGroup = cboSRReferralGroup.SelectedValue;
                entity.ReferralID = cboReferralID.SelectedValue;
                entity.ReferralName = txtReferralName.Text;

                if (!txtDischargeDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                {
                    entity.DischargeDate = txtDischargeDate.SelectedDate;
                    entity.DischargeTime = txtDischargeTime.TextWithLiterals;

                    entity.DischargeMedicalNotes = txtDischargeMedicalNotes.Text;
                    entity.DischargeNotes = txtDischargeNotes.Text;
                    entity.SRDischargeCondition = cboSRDischargeCondition.SelectedValue;
                    entity.SRDischargeMethod = cboSRDischargeMethod.SelectedValue;
                    entity.DischargeOperatorID = AppSession.UserLogin.UserID;

                    if (AppSession.Parameter.IsCloseRegOnDischargeEmr)
                        entity.IsClosed = true;
                }

                entity.SRPatientInCondition = cboSRPatientInCondition.SelectedValue;
                entity.SRPatientInType = cboSRPatientInType.SelectedValue;
                entity.SRERCaseType = cboSRERCaseType.SelectedValue;
                entity.SRVisitReason = cboSRVisitReason.SelectedValue;
                entity.ReasonsForTreatmentID = cboReasonForTreatmentID.SelectedValue;
                entity.ReasonsForTreatmentDescID = cboReasonsForTreatmentDescID.SelectedValue;
                entity.SRTriage = cboSRTriage.SelectedValue;
                entity.EmrDiagnoseID = IsEmrDiagnoseFreeText
                                           ? txtEmrDiagnoseID.Text
                                           : cboEmrDiagnoseID.SelectedValue;
                entity.InitialDiagnose = IsEmrDiagnoseFreeText
                                           ? txtEmrDiagnoseID.Text
                                           : cboEmrDiagnoseID.Text;
                entity.IsOldCase = chkIsOldCase.Checked;
                entity.IsObservation = chkIsObservation.Checked;
                entity.IsDHF = chkIsDHF.Checked;
                entity.IsEKG = chkIsEKG.Checked;
                entity.CauseOfAccident = txtCauseOfAccident.Text;
                entity.SRCrashSite = cboSRCrashSite.SelectedValue;
                entity.CrashSiteDetail = txtCrashSiteDetail.Text;
                entity.ReferTo = txtReferTo.Text;

                entity.FirstResponDate = txtFirstResponDate.SelectedDate;
                entity.FirstResponTime = txtFirstResponTime.TextWithLiterals;
                entity.PhysicianResponDate = txtPhysicianResponDate.SelectedDate;
                entity.PhysicianResponTime = txtPhysicianResponTime.TextWithLiterals;

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

                //update patient
                var patient = new Patient();
                patient.LoadByPrimaryKey(entity.PatientID);
                var isUpdatePatient = false;
                if (entity.SRDischargeCondition == AppSession.Parameter.DischargeConditionDieLessThen48 || entity.SRDischargeCondition == AppSession.Parameter.DischargeConditionDieMoreThen48 || entity.SRDischargeCondition == AppSession.Parameter.DischargeConditionDie)
                {
                    isUpdatePatient = true;

                    patient.IsAlive = false;
                    patient.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    patient.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }

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
                if (isUpdatePatient)
                    patient.Save();

                TreatmentForAnimalBitess.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        //protected void cboReasonForTreatmentID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        //{
        //    e.Item.Text = ((DataRowView)e.Item.DataItem)["ReasonsForTreatmentName"].ToString();
        //    e.Item.Value = ((DataRowView)e.Item.DataItem)["ReasonsForTreatmentID"].ToString();
        //}

        //protected void cboReasonForTreatmentID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        //{
        //    var query = new ReasonsForTreatmentQuery();
        //    query.es.Top = 20;
        //    query.Where
        //        (
        //            query.ReasonsForTreatmentName.Like(string.Format(."%{0}%", e.Text)),
        //            query.IsActive == true,
        //            query.SRReasonVisit == cboSRVisitReason.SelectedValue
        //        );
        //    query.OrderBy(query.ReasonsForTreatmentName.Ascending);

        //    cboReasonForTreatmentID.DataSource = query.LoadDataTable();
        //    cboReasonForTreatmentID.DataBind();
        //}

        //protected void cboReasonsForTreatmentDescID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        //{
        //    e.Item.Text = ((DataRowView)e.Item.DataItem)["ReasonsForTreatmentDescName"].ToString();
        //    e.Item.Value = ((DataRowView)e.Item.DataItem)["ReasonsForTreatmentDescID"].ToString();
        //}

        //protected void cboReasonsForTreatmentDescID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        //{
        //    var query = new ReasonsForTreatmentDescQuery();
        //    query.es.Top = 20;
        //    query.Where
        //        (
        //            query.ReasonsForTreatmentDescName.Like(string.Format(."%{0}%", e.Text)),
        //            query.ReasonsForTreatmentID == cboReasonForTreatmentID.SelectedValue,
        //            query.SRReasonVisit == cboSRVisitReason.SelectedValue
        //        );
        //    query.OrderBy(query.ReasonsForTreatmentDescName.Ascending);

        //    cboReasonsForTreatmentDescID.DataSource = query.LoadDataTable();
        //    cboReasonsForTreatmentDescID.DataBind();
        //}

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

        protected void cboReferralID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ReferralName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ReferralID"].ToString();
        }

        protected void cboReferralID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ReferralQuery();
            query.es.Top = 30;
            query.Where
                (
                    query.ReferralName.Like(searchTextContain),
                    query.IsActive == true, query.SRReferralGroup == cboSRReferralGroup.SelectedValue
                );
            query.OrderBy(query.ReferralName.Ascending);

            cboReferralID.DataSource = query.LoadDataTable();
            cboReferralID.DataBind();
        }

        protected void cboReferralID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ReadOnlyReferralName();
            txtReferralName.Text = string.Empty;
            if (string.IsNullOrEmpty(cboSRReferralGroup.SelectedValue))
            {
                var r = new Referral();
                r.LoadByPrimaryKey(e.Value);
                cboSRReferralGroup.SelectedValue = r.SRReferralGroup;
            }
        }

        protected void cboSRReferralGroup_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboReferralID.Items.Clear();
            cboReferralID.SelectedValue = string.Empty;
            cboReferralID.Text = string.Empty;
            txtReferralName.ReadOnly = false;
            txtReferralName.Text = string.Empty;
        }

        private void ReadOnlyReferralName()
        {
            var referral = new Referral();
            if (referral.LoadByPrimaryKey(cboReferralID.SelectedValue))
            {
                var std = new AppStandardReferenceItem();
                std.LoadByPrimaryKey("ReferralGroup", referral.SRReferralGroup);
                txtReferralName.ReadOnly = (std.ReferenceID == "JM");
            }
            else
                txtReferralName.ReadOnly = false;
        }

        protected void cboSRVisitReason_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulatecboReasonForTreatmentList(e.Value);
            cboReasonForTreatmentID.Text = string.Empty;
            cboReasonForTreatmentID.SelectedValue = string.Empty;

            cboReasonsForTreatmentDescID.Items.Clear();
            cboReasonsForTreatmentDescID.Text = string.Empty;
            cboReasonsForTreatmentDescID.SelectedValue = string.Empty;
        }

        protected void cboReasonForTreatmentID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulatecboReasonForTreatmentDescList(cboSRVisitReason.SelectedValue, e.Value);
            cboReasonsForTreatmentDescID.Text = string.Empty;
            cboReasonsForTreatmentDescID.SelectedValue = string.Empty;
        }

        private void PopulatecboReasonForTreatmentList(string srReasonVisit)
        {
            cboReasonForTreatmentID.Items.Clear();

            var coll = new ReasonsForTreatmentCollection();
            coll.Query.Where(coll.Query.SRReasonVisit == srReasonVisit, coll.Query.IsActive == true);
            coll.LoadAll();

            cboReasonForTreatmentID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (ReasonsForTreatment entity in coll)
            {
                cboReasonForTreatmentID.Items.Add(new RadComboBoxItem(entity.ReasonsForTreatmentName, entity.ReasonsForTreatmentID));
            }
        }

        private void PopulatecboReasonForTreatmentDescList(string srReasonVisit, string reasonsForTreatmentId)
        {
            cboReasonsForTreatmentDescID.Items.Clear();

            var coll = new ReasonsForTreatmentDescCollection();
            coll.Query.Where(coll.Query.SRReasonVisit == srReasonVisit, coll.Query.ReasonsForTreatmentID == reasonsForTreatmentId);
            coll.LoadAll();

            cboReasonsForTreatmentDescID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (ReasonsForTreatmentDesc entity in coll)
            {
                cboReasonsForTreatmentDescID.Items.Add(new RadComboBoxItem(entity.ReasonsForTreatmentDescName, entity.ReasonsForTreatmentDescID));
            }
        }

        #region Treatment For Animal Bites

        private void PopulateTreatmentForAnimalBitesGrid()
        {
            //Display Data Detail
            TreatmentForAnimalBitess = null; //Reset Record Detail
            grdTreatmentForAnimalBites.DataSource = TreatmentForAnimalBitess; //Requery
            grdTreatmentForAnimalBites.MasterTableView.IsItemInserted = false;
            grdTreatmentForAnimalBites.MasterTableView.ClearEditItems();
            grdTreatmentForAnimalBites.DataBind();
        }

        protected void grdTreatmentForAnimalBites_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdTreatmentForAnimalBites.DataSource = TreatmentForAnimalBitess;
        }

        protected void grdTreatmentForAnimalBites_InsertCommand(object source, GridCommandEventArgs e)
        {
            TreatmentForAnimalBites entity = TreatmentForAnimalBitess.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdTreatmentForAnimalBites.Rebind();
        }

        protected void grdTreatmentForAnimalBites_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String itemId =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][TreatmentForAnimalBitesMetadata.ColumnNames.SRTreatmentForAnimalBites]);
            TreatmentForAnimalBites entity = FindItem(itemId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        private TreatmentForAnimalBites FindItem(String itemId)
        {
            TreatmentForAnimalBitesCollection coll = TreatmentForAnimalBitess;
            TreatmentForAnimalBites retEntity = null;
            foreach (TreatmentForAnimalBites rec in coll)
            {
                if (rec.SRTreatmentForAnimalBites.Equals(itemId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(TreatmentForAnimalBites entity, GridCommandEventArgs e)
        {
            var userControl = (TreatmentForAnimalBitesItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.RegistrationNo = txtRegistrationNo.Text;
                entity.SRTreatmentForAnimalBites = userControl.SRTreatmentForAnimalBites;
                entity.TreatmentForAnimalBitesName = userControl.TreatmentForAnimalBitesName;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private TreatmentForAnimalBitesCollection TreatmentForAnimalBitess
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collTreatmentForAnimalBites"];
                    if (obj != null)
                        return ((TreatmentForAnimalBitesCollection)(obj));
                }

                var coll = new TreatmentForAnimalBitesCollection();
                var query = new TreatmentForAnimalBitesQuery("a");
                var stdQ = new AppStandardReferenceItemQuery("b");

                query.InnerJoin(stdQ).On(query.SRTreatmentForAnimalBites == stdQ.ItemID &&
                                         stdQ.StandardReferenceID ==
                                         AppEnum.StandardReference.TreatmentForAnimalBites.ToString());

                query.Where(query.RegistrationNo == txtRegistrationNo.Text);
                query.OrderBy(query.SRTreatmentForAnimalBites.Descending);

                query.Select
                    (
                        query,
                        stdQ.ItemName.As("refToAppStandardReferenceItem_TreatmentForAnimalBites")
                    );

                coll.Load(query);
                Session["collTreatmentForAnimalBites"] = coll;

                return coll;
            }
            set { Session["collTreatmentForAnimalBites"] = value; }
        }

        #endregion

        #region Patient Health Record & Document
        protected void grdPHR_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPHR.DataSource = SelectPhrForm(txtServiceUnitID.Text, txtRegistrationNo.Text);
        }

        protected void grdPHR2_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPHR2.DataSource = SelectPhrFormHistory(txtServiceUnitID.Text, txtPatientID.Text);
        }

        protected void grdPHR_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (!e.CommandArgument.ToString().Contains("_")) return;

            var arg = e.CommandArgument.ToString().Split('|');
            var pars = arg[1].Split('_');

            if (arg[0] == "print")
            {
                var jobParameters = new PrintJobParameterCollection();
                jobParameters.AddNew("p_RegistrationNo", pars[0]);
                jobParameters.AddNew("p_QuestionFormID", pars[1]);
                jobParameters.AddNew("p_TransactionNo", pars[2]);

                AppSession.PrintJobParameters = jobParameters;

                var form = new QuestionForm();
                form.LoadByPrimaryKey(pars[1]);
                AppSession.PrintJobReportID = form.ReportProgramID;

                string script = @"var oWnd = $find('" + winPrint.ClientID + @"');oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + @"');
                                oWnd.Show(); oWnd.Maximize();";

                radAjaxPanel.ResponseScripts.Add(script);
            }
            else if (arg[0] == "delete")
            {
                using (var trans = new esTransactionScope())
                {
                    var dt = new PatientHealthRecordLineCollection();
                    dt.Query.Where(
                        dt.Query.TransactionNo == pars[2],
                        dt.Query.RegistrationNo == pars[0],
                        dt.Query.QuestionFormID == pars[1]
                        );
                    dt.Query.Load();
                    dt.MarkAllAsDeleted();
                    dt.Save();

                    var hd = new PatientHealthRecord();
                    hd.LoadByPrimaryKey(pars[1], pars[0], pars[2]);
                    hd.MarkAsDeleted();
                    hd.Save();

                    trans.Complete();
                }
                grdPHR2.Rebind();
            }
        }

        public static DataTable SelectPhrForm(string serviceUnitID, string registrationNo)
        {
            var query = new QuestionFormQuery("a");
            var suQr = new QuestionFormInServiceUnitQuery("s");

            query.InnerJoin(suQr).On(query.QuestionFormID == suQr.QuestionFormID && suQr.ServiceUnitID == serviceUnitID);

            query.Where(query.IsActive == true);

            query.Select(string.Format("<'{0}' as RegistrationNo>", registrationNo),
                query.QuestionFormID,
                @"<CASE WHEN a.QuestionFormID = 'MEDHIS' then 1
                        WHEN a.QuestionFormID = 'PHYEXAM' THEN 2
                        WHEN a.QuestionFormID = 'SUMMARY' THEN 3
                        ELSE 4 END AS formID>",
                query.QuestionFormName,
                query.IsMCUForm
                );

            return query.LoadDataTable();
        }

        public static DataTable SelectPhrFormHistory(string serviceUnitID, string patientID)
        {
            var query = new QuestionFormQuery("a");
            var suQr = new QuestionFormInServiceUnitQuery("s");
            var phrQr = new PatientHealthRecordQuery("b");
            var empQr = new EmployeeQuery("e");
            var reg = new RegistrationQuery("x");
            var par = new ParamedicQuery("y");
            var unit = new ServiceUnitQuery("z");

            query.InnerJoin(suQr).On(query.QuestionFormID == suQr.QuestionFormID && suQr.ServiceUnitID == serviceUnitID);
            query.InnerJoin(phrQr).On(phrQr.QuestionFormID == query.QuestionFormID);
            query.InnerJoin(reg).On(phrQr.RegistrationNo == reg.RegistrationNo && reg.PatientID == patientID);
            query.LeftJoin(par).On(reg.ParamedicID == par.ParamedicID);
            query.LeftJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            query.LeftJoin(empQr).On(phrQr.EmployeeID == empQr.EmployeeID);

            query.Where(
                query.IsActive == true
                );

            query.Select(
                phrQr.TransactionNo,
                reg.RegistrationNo,
                par.ParamedicName,
                unit.ServiceUnitName,
                query.QuestionFormID,
                @"<CASE WHEN a.QuestionFormID = 'MEDHIS' then 1
                        WHEN a.QuestionFormID = 'PHYEXAM' THEN 2
                        WHEN a.QuestionFormID = 'SUMMARY' THEN 3
                        ELSE 4 END AS formID>",
                query.QuestionFormName,
                query.IsMCUForm,
                phrQr.RecordDate,
                phrQr.EmployeeID,
                empQr.EmployeeName,
                phrQr.IsComplete,
                phrQr.ReferenceNo
                );

            return query.LoadDataTable();
        }

        //private static DataTable DocumentTemplate
        //{
        //    get
        //    {
        //        var obj = HttpContext.Current.Session["DocumentTemplate"];
        //        if (obj == null)
        //            return null;
        //        return (DataTable)obj;
        //    }
        //    set { HttpContext.Current.Session["DocumentTemplate"] = value; }
        //}

        //public static DataTable SelectDocumentTemplate()
        //{
        //    if (DocumentTemplate == null)
        //    {
        //        var qr = new DocumentFilesQuery();
        //        qr.Select(
        //            qr.DocumentNumber,
        //            qr.DocumentName,
        //            qr.DocumentFilesID,
        //            qr.DocumentName,
        //            qr.FileTemplateName
        //            );
        //        qr.Where(
        //            qr.IsActive == true &&
        //            qr.FileTemplateName != string.Empty
        //            );
        //        DocumentTemplate = qr.LoadDataTable();
        //    }
        //    return DocumentTemplate;
        //}

        protected void grdTransaction_Init(object sender, EventArgs e)
        {
            InitializeCultureGrid((RadGrid)sender);
        }


        #endregion

        private string GetNewDeathCertificateNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer(), AppEnum.AutoNumber.DeathCertificateNo);
            return _autoNumber.LastCompleteNumber;
        }

        protected void cboSRPatientInType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!AppSession.Parameter.IsMandatoryEmrRegDetail)
            {
                if (cboSRPatientInType.SelectedValue == AppSession.Parameter.PatientInTypeTrueEmergency)
                {
                    rfvSRPatientInCondition.Visible = true;
                    rfvSRERCaseType.Visible = true;
                    rfvSRTriage.Visible = true;
                    rfvcSRVisitReason.Visible = true;
                    rfvReasonForTreatmentID.Visible = true;
                    rfvFirstResponDate.Visible = true;
                    rfvPhysicianResponDate.Visible = true;
                    //rfvDischargeDate.Visible = true;
                    //rfvSRDischargeMethod.Visible = true;
                    //rfvSRDischargeCondition.Visible = true;
                }
                else
                {
                    rfvSRPatientInCondition.Visible = false;
                    rfvSRERCaseType.Visible = false;
                    rfvSRTriage.Visible = false;
                    rfvcSRVisitReason.Visible = false;
                    rfvReasonForTreatmentID.Visible = false;
                    rfvFirstResponDate.Visible = false;
                    rfvPhysicianResponDate.Visible = false;
                    //rfvDischargeDate.Visible = false;
                    //rfvSRDischargeMethod.Visible = false;
                    //rfvSRDischargeCondition.Visible = false;
                }
            }
        }
    }
}
