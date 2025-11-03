using System;
using System.Collections.Generic;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class EditGuarantorDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            var btkOk = (Button)Helper.FindControlRecursive(Master, "btnOk");

            string regType = Request.QueryString["rt"];
            if (string.IsNullOrEmpty(regType))
                regType = AppConstant.RegistrationType.ClusterPatient;

            switch (regType)
            {
                case AppConstant.RegistrationType.InPatient:
                    ProgramID = AppConstant.Program.Admitting;
                    btkOk.Visible = this.IsUserAddAble || this.IsUserEditAble;
                    break;
                case AppConstant.RegistrationType.OutPatient:
                    ProgramID = AppConstant.Program.OutPatientRegistration;
                    btkOk.Visible = this.IsUserAddAble || this.IsUserEditAble;
                    break;
                case AppConstant.RegistrationType.ClusterPatient:
                    ProgramID = AppConstant.Program.ClusterPatientRegistration;
                    btkOk.Visible = this.IsUserAddAble || this.IsUserEditAble;
                    break;
                case AppConstant.RegistrationType.EmergencyPatient:
                    ProgramID = AppConstant.Program.EmergencyPatientRegistration;
                    btkOk.Visible = this.IsUserAddAble || this.IsUserEditAble;
                    break;
                case AppConstant.RegistrationType.MedicalCheckUp:
                    ProgramID = AppConstant.Program.HealthScreeningRegistration;
                    btkOk.Visible = this.IsUserAddAble || this.IsUserEditAble;
                    break;
            }

            if (!IsPostBack)
            {
                trGuarantorHeader.Visible = (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH");

                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regNo"]);

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                txtRegistrationNo.Text = reg.RegistrationNo;
                txtMedicalNo.Text = pat.MedicalNo;
                txtPatientName.Text = pat.PatientName;

                optSexMale.Checked = (pat.Sex == "M");
                optSexMale.Enabled = (pat.Sex == "M");
                optSexFemale.Checked = (pat.Sex == "F");
                optSexFemale.Enabled = (pat.Sex == "F");

                var room = new ServiceRoom();
                room.LoadByPrimaryKey(reg.RoomID);
                txtRoom.Text = room.RoomName;
                txtBed.Text = reg.BedID;

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(reg.ServiceUnitID);
                txtServiceUnitName.Text = unit.ServiceUnitName;

                var par = new Paramedic();
                par.LoadByPrimaryKey(reg.ParamedicID);
                txtPhysicianName.Text = par.ParamedicName;

                //Title = "Edit Guarantor for " + reg.RegistrationNo + " - " + pat.PatientName + " [" + pat.MedicalNo + "]";

                var query = new GuarantorQuery();
                query.Where(query.GuarantorID == reg.GuarantorID);

                cboGuarantorID.DataSource = query.LoadDataTable();
                cboGuarantorID.DataBind();
                cboGuarantorID.SelectedValue = reg.GuarantorID;

                var g = new Guarantor();
                g.LoadByPrimaryKey(reg.GuarantorID);

                query = new GuarantorQuery();
                query.Where(query.GuarantorID == g.GuarantorHeaderID);

                cboGuarantorGroupID.DataSource = query.LoadDataTable();
                cboGuarantorGroupID.DataBind();
                cboGuarantorGroupID.SelectedValue = g.GuarantorHeaderID;

                StandardReference.InitializeIncludeSpace(cboSRBusinessMethod, AppEnum.StandardReference.BusinessMethod);
                StandardReference.InitializeIncludeSpace(cboGuarSRRelationship, AppEnum.StandardReference.Relationship);

                var std = new AppStandardReferenceItem();
                std.LoadByPrimaryKey(AppEnum.StandardReference.BusinessMethod.ToString(), reg.SRBussinesMethod);
                cboSRBusinessMethod.SelectedValue = std.ItemID;

                txtPlafonValue.ReadOnly = cboSRBusinessMethod.SelectedValue != AppSession.Parameter.BusinessMethodFlavon;
                txtPlafonValue.Value = Convert.ToDouble(reg.PlavonAmount);

                chkIsPlavonTypeGlobal.Enabled = cboSRBusinessMethod.SelectedValue == AppSession.Parameter.BusinessMethodFlavon;
                chkIsPlavonTypeGlobal.Checked = reg.IsGlobalPlafond ?? false;

                var guar = new Guarantor();
                guar.LoadByPrimaryKey(reg.GuarantorID);

                if (reg.PersonID != null)
                {
                    var empq = new PersonalInfoQuery();
                    empq.Where(empq.PersonID == reg.PersonID);
                    cboEmployeeID.DataSource = empq.LoadDataTable();
                    cboEmployeeID.DataBind();
                    cboEmployeeID.SelectedValue = reg.PersonID.ToString();
                }

                cboEmployeeID.Enabled = (AppSession.Parameter.IsUsingHumanResourcesModul);
                cboGuarSRRelationship.Enabled = (AppSession.Parameter.IsUsingHumanResourcesModul);

                if (guar.SRGuarantorType == AppSession.Parameter.GuarantorTypeEmployee)
                {
                    var emp = new PersonalInfo();
                    if (emp.LoadByPrimaryKey(Convert.ToInt32(reg.PersonID)))
                    {
                        if (AppSession.Parameter.IsRADTLinkToHumanResourcesModul)
                        {
                            cboEmployeeID.Enabled = false;
                            cboGuarSRRelationship.Enabled = false;
                        }
                    }
                    else
                    {
                        string searchTextContain = string.Format("%{0}%", cboGuarantorID.SelectedValue);
                        var pars = new AppParameterCollection();
                        pars.Query.Where(pars.Query.ParameterID == "DependentsOfEmployeesGuarantorID",
                                         pars.Query.ParameterValue.Like(searchTextContain));
                        pars.LoadAll();
                        if (pars.Count <= 0 && AppSession.Parameter.IsRADTLinkToHumanResourcesModul)
                        {
                            cboEmployeeID.Enabled = false;
                            cboGuarSRRelationship.Enabled = false;
                        }
                    }
                }
                txtInsuranceID.Text = reg.InsuranceID;
                txtBpjsSepNo.Text = reg.BpjsSepNo;
                cboGuarSRRelationship.SelectedValue = reg.SREmployeeRelationship;
                txtGuarIDCardNo.Text = reg.GuarantorCardNo;

                pnlEmployeeInfo.Visible = AppSession.Parameter.IsUsingHumanResourcesModul;

                RegistrationGuarantors = null;
                grdRegistrationGuarantor.Rebind();
            }

        }

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);

            if (AppSession.Parameter.IsUsingHumanResourcesModul)
            {
                RadAjaxManagerProxy1.AjaxSettings.AddAjaxSetting(cboGuarantorID, cboEmployeeID);
                RadAjaxManagerProxy1.AjaxSettings.AddAjaxSetting(cboGuarantorID, cboGuarSRRelationship);
                RadAjaxManagerProxy1.AjaxSettings.AddAjaxSetting(cboGuarantorGroupID, cboEmployeeID);
                RadAjaxManagerProxy1.AjaxSettings.AddAjaxSetting(cboGuarantorGroupID, cboGuarSRRelationship);
            }
        }

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        protected void cboGuarantorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new GuarantorQuery();
            query.es.Top = 30;
            query.Where
                (
                    query.GuarantorName.Like(searchTextContain),
                    query.SRGuarantorType != AppSession.Parameter.GuarantorTypeMemberID,
                    query.IsActive == true
                );
            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
                query.Where(query.GuarantorHeaderID == cboGuarantorGroupID.SelectedValue);

            query.OrderBy(query.GuarantorName.Ascending);

            cboGuarantorID.DataSource = query.LoadDataTable();
            cboGuarantorID.DataBind();
        }

        protected void cboGuarantorID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var grr = new Guarantor();
            grr.LoadByPrimaryKey(e.Value);
            cboSRBusinessMethod.SelectedValue = grr.SRBusinessMethod;
            chkIsPlavonTypeGlobal.Checked = grr.IsGlobalPlafond ?? false;

            txtPlafonValue.ReadOnly = grr.SRBusinessMethod != AppSession.Parameter.BusinessMethodFlavon;
            txtPlafonValue.Value = Convert.ToDouble(grr.AmountValue ?? 0);

            chkIsPlavonTypeGlobal.Enabled = grr.SRBusinessMethod == AppSession.Parameter.BusinessMethodFlavon;

            cboEmployeeID.Items.Clear();
            cboEmployeeID.Text = string.Empty;
            cboGuarSRRelationship.SelectedValue = string.Empty;
            cboGuarSRRelationship.Text = string.Empty;

            if (grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeEmployee)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regNo"]);
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                if (pat.PersonID != null)
                {
                    var empq = new PersonalInfoQuery();
                    empq.Where(empq.PersonID == pat.PersonID);
                    cboEmployeeID.DataSource = empq.LoadDataTable();
                    cboEmployeeID.DataBind();
                    cboEmployeeID.SelectedValue = pat.PersonID.ToString();
                }

                cboGuarSRRelationship.SelectedValue = pat.SREmployeeRelationship;

                if (AppSession.Parameter.IsRADTLinkToHumanResourcesModul)
                {
                    cboEmployeeID.Enabled = false;
                    cboGuarSRRelationship.Enabled = false;
                }
            }
            else
            {
                string searchTextContain = string.Format("%{0}%", cboGuarantorID.SelectedValue);
                var pars = new AppParameterCollection();
                pars.Query.Where(pars.Query.ParameterID == "DependentsOfEmployeesGuarantorID",
                                 pars.Query.ParameterValue.Like(searchTextContain));
                pars.LoadAll();
                if (pars.Count <= 0 && AppSession.Parameter.IsRADTLinkToHumanResourcesModul)
                {
                    cboEmployeeID.Enabled = false;
                    cboGuarSRRelationship.Enabled = false;
                }
            }
        }

        protected void cboGuarantorGroupID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new GuarantorQuery("a");
            var querydt = new GuarantorQuery("b");
            query.InnerJoin(querydt).On(query.GuarantorHeaderID == querydt.GuarantorID);
            query.Select(query.GuarantorHeaderID.As("GuarantorID"), querydt.GuarantorName);
            query.es.Top = 30;
            query.es.Distinct = true;
            query.Where
                (
                    querydt.GuarantorName.Like(searchTextContain),
                    query.IsActive == true,
                    query.SRGuarantorType != AppSession.Parameter.GuarantorTypeMemberID
                );
            query.OrderBy(querydt.GuarantorName.Ascending);

            cboGuarantorGroupID.DataSource = query.LoadDataTable();
            cboGuarantorGroupID.DataBind();
        }

        protected void cboGuarantorGroupID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboGuarantorID.Items.Clear();
            cboGuarantorID.Text = string.Empty;
            cboEmployeeID.Items.Clear();
            cboEmployeeID.Text = string.Empty;
            cboGuarSRRelationship.SelectedValue = string.Empty;
            cboGuarSRRelationship.Text = string.Empty;
            cboSRBusinessMethod.SelectedValue = string.Empty;
            cboSRBusinessMethod.Text = string.Empty;
            txtPlafonValue.Value = 0;
            chkIsPlavonTypeGlobal.Checked = false;
        }

        protected void cboSRBusinessMethod_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txtPlafonValue.ReadOnly = e.Value != AppSession.Parameter.BusinessMethodFlavon;
            txtPlafonValue.Value = 0;

            chkIsPlavonTypeGlobal.Enabled = e.Value == AppSession.Parameter.BusinessMethodFlavon;
            chkIsPlavonTypeGlobal.Checked = e.Value == AppSession.Parameter.BusinessMethodFlavon;
        }

        protected void cboEmployeeID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " +
                          ((DataRowView)e.Item.DataItem)["FirstName"].ToString() + " " +
                          ((DataRowView)e.Item.DataItem)["MiddleName"].ToString() + " " +
                          ((DataRowView)e.Item.DataItem)["LastName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboEmployeeID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new PersonalInfoQuery();
            query.es.Top = 15;
            query.Select(query.PersonID, query.EmployeeNumber, query.FirstName, query.MiddleName, query.LastName);
            query.Where
                (
                    query.Or(query.EmployeeNumber == e.Text,
                    query.FirstName.Like(searchTextContain))
                );
            query.OrderBy(query.EmployeeNumber.Ascending);

            cboEmployeeID.DataSource = query.LoadDataTable();
            cboEmployeeID.DataBind();
        }

        protected void grdHistoryUpdateGuarantor_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource = RegistrationGuarantorHistorys();
        }

        private DataTable RegistrationGuarantorHistorys()
        {
            var query = new RegistrationGuarantorHistoryQuery("a");
            var fromGuarQ = new GuarantorQuery("b");
            var toGuarQ = new GuarantorQuery("c");

            query.Select(
                query.RegistrationNo,
                query.FromGuarantorID,
                query.ToGuarantorID,
                query.LastUpdateDateTime,
                query.LastUpdateByUserID,
                fromGuarQ.GuarantorName.As("FromGuarantorName"),
                toGuarQ.GuarantorName.As("ToGuarantorName")
                );
            query.InnerJoin(fromGuarQ).On(query.FromGuarantorID == fromGuarQ.GuarantorID);
            query.InnerJoin(toGuarQ).On(query.ToGuarantorID == toGuarQ.GuarantorID);
            query.Where(query.RegistrationNo == Request.QueryString["regNo"]);
            query.OrderBy(query.LastUpdateDateTime.Ascending);

            var dtb = query.LoadDataTable();

            return dtb;
        }

        private IEnumerable<string> MergeRegistrationList()
        {
            if (ViewState["MergeRegistration" + Request.UserHostName] == null)
                ViewState["MergeRegistration" + Request.UserHostName] = Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regNo"]);

            return (string[])ViewState["MergeRegistration" + Request.UserHostName];
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind:'";
        }

        public override bool OnButtonOkClicked()
        {
            if (!Page.IsValid)
                return false;

            if (string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
            {
                ShowInformationHeader("Guarantor required.");
                return false;
            }

            //guarantor
            var grr = new Guarantor();
            if (!grr.LoadByPrimaryKey(cboGuarantorID.SelectedValue))
            {
                ShowInformationHeader("Guarantor is not found.");
                return false;
            }

            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regNo"]);

            bool updateGuarantor = reg.GuarantorID != cboGuarantorID.SelectedValue;

            if (updateGuarantor && !string.IsNullOrEmpty(reg.MembershipNo))
            {
                var x = BusinessObject.MembershipDetail.EmployeeRefferalRewardPoints(reg.MembershipNo, reg.RegistrationNo, reg.RegistrationDate ?? (new DateTime()).NowAtSqlServer(),
                        reg.GuarantorID, AppSession.Parameter.GuarantorTypeSelf, AppSession.Parameter.RewardPointsForPatientGeneral, AppSession.Parameter.RewardPointsForPatientGuarantee,
                        AppSession.UserLogin.UserID, false, cboGuarantorID.SelectedValue, string.Empty);
            }

            var hist = new RegistrationGuarantorHistory();
            hist.AddNew();
            hist.RegistrationNo = reg.RegistrationNo;
            hist.FromGuarantorID = reg.GuarantorID;
            hist.ToGuarantorID = cboGuarantorID.SelectedValue;
            hist.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            hist.LastUpdateByUserID = AppSession.UserLogin.UserID;

            if (grr.IsActive == false)
            {
                ShowInformationHeader("Guarantor is not active. Please select another Guarantor.");
                return false;
            }

            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
            {
                var guarhd = new Guarantor();
                guarhd.LoadByPrimaryKey(grr.GuarantorHeaderID);

                if (guarhd.IsActive == false)
                {
                    ShowInformationHeader("Guarantor Group is not active. Please select another Guarantor Group.");
                    return false;
                }
                if (reg.RegistrationDate > guarhd.ContractEnd)
                {
                    ShowInformationHeader("The contract period for selected Guarantor Group is over. Please select another Guarantor Group.");
                    return false;
                }
            }

            if (AppSession.Parameter.ValidateGuarantorContractPeriode == "Yes")
            {
                if (reg.RegistrationDate > grr.ContractEnd)
                {
                    ShowInformationHeader("The contract period for selected Guarantor is over. Please select another Guarantor.");
                    return false;
                }
            }

            if (grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeCompany && AppSession.Parameter.ValidateGuarantorCardNo == "Yes")
            {
                if (string.IsNullOrEmpty(txtGuarIDCardNo.Text))
                {
                    ShowInformationHeader("Guarantor Card No required.");
                    return false;
                }
            }

            if (grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeInsurance && AppSession.Parameter.ValidateInsuranceID == "Yes")
            {
                if (string.IsNullOrEmpty(txtInsuranceID.Text))
                {
                    ShowInformationHeader("Insurance ID required.");
                    return false;
                }
            }

            if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
            {
                if (grr.IsCoverInpatient == false)
                {
                    ShowInformationHeader("Guarantor is not cover Inpatient or Emergency. Please select another Guarantor.");
                    return false;
                }
            }
            else
            {
                if (grr.IsCoverOutpatient == true)
                {
                    var notCovereds = new GuarantorServiceUnitRuleCollection();
                    notCovereds.Query.Where(notCovereds.Query.GuarantorID == cboGuarantorID.SelectedValue,
                                             notCovereds.Query.ServiceUnitID == reg.ServiceUnitID,
                                             notCovereds.Query.IsCovered == false);
                    notCovereds.LoadAll();
                    if (notCovereds.Count > 0)
                    {
                        var unit = new ServiceUnit();
                        unit.LoadByPrimaryKey(reg.ServiceUnitID);
                        ShowInformationHeader("Guarantor is not cover Outpatient - Unit: " + unit.ServiceUnitName +
                                              ". Please select another Guarantor.");
                        return false;
                    }
                }
                else
                {
                    var covereds = new GuarantorServiceUnitRuleCollection();
                    covereds.Query.Where(covereds.Query.GuarantorID == cboGuarantorID.SelectedValue,
                                             covereds.Query.ServiceUnitID == reg.ServiceUnitID,
                                             covereds.Query.IsCovered == true);
                    covereds.LoadAll();
                    if (covereds.Count == 0)
                    {
                        var unit = new ServiceUnit();
                        unit.LoadByPrimaryKey(reg.ServiceUnitID);
                        ShowInformationHeader("Guarantor is not cover Outpatient - Unit: " + unit.ServiceUnitName +
                                              ". Please select another Guarantor.");
                        return false;
                    }
                }
            }

            if (AppSession.Parameter.IsUsingHumanResourcesModul)
            {
                string isEmployeeIdRequeredType;
                var guar = new Guarantor();
                guar.LoadByPrimaryKey(cboGuarantorID.SelectedValue);
                if (guar.SRGuarantorType == AppSession.Parameter.GuarantorTypeEmployee)
                    isEmployeeIdRequeredType = "1";
                else
                {
                    string searchTextContain = string.Format("%{0}%", cboGuarantorID.SelectedValue);
                    var apps = new AppParameterCollection();
                    apps.Query.Where(apps.Query.ParameterID == "DependentsOfEmployeesGuarantorID",
                                    apps.Query.ParameterValue.Like(searchTextContain));
                    apps.LoadAll();
                    isEmployeeIdRequeredType = apps.Count > 0 ? "2" : "0";
                }

                if (isEmployeeIdRequeredType != "0")
                {
                    if (string.IsNullOrEmpty(cboEmployeeID.SelectedValue))
                    {
                        switch (isEmployeeIdRequeredType)
                        {
                            case "1":
                                ShowInformationHeader("Employee ID required. Please contact HRD to define that required.");
                                break;
                            case "2":
                                ShowInformationHeader("Employee ID required.");
                                break;
                        }
                        return false;
                    }
                    var emp = new PersonalInfo();
                    if (!emp.LoadByPrimaryKey(Convert.ToInt32(cboEmployeeID.SelectedValue)))
                    {
                        ShowInformationHeader("Employee ID is not registered.");
                        return false;
                    }
                }
            }

            reg.GuarantorID = cboGuarantorID.SelectedValue;
            reg.SRBussinesMethod = cboSRBusinessMethod.SelectedValue;
            reg.PlavonAmount = Convert.ToDecimal(txtPlafonValue.Value);
            reg.IsGlobalPlafond = chkIsPlavonTypeGlobal.Checked;
            reg.SREmployeeRelationship = cboGuarSRRelationship.SelectedValue;

            if (!string.IsNullOrEmpty(cboEmployeeID.SelectedValue))
            {
                var pInfo = new PersonalInfo();
                if (pInfo.LoadByPrimaryKey(Convert.ToInt32(cboEmployeeID.SelectedValue)))
                {
                    reg.PersonID = Convert.ToInt32(cboEmployeeID.SelectedValue);
                    reg.EmployeeNumber = pInfo.EmployeeNumber;
                }
                else
                {
                    reg.PersonID = null;
                    reg.EmployeeNumber = null;
                }
            }
            else
            {
                reg.PersonID = null;
                reg.EmployeeNumber = null;
            }

            reg.InsuranceID = txtInsuranceID.Text;
            reg.GuarantorCardNo = txtGuarIDCardNo.Text;
            reg.BpjsSepNo = txtBpjsSepNo.Text;
            reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
            reg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            var pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);
            pat.GuarantorID = cboGuarantorID.SelectedValue;
            pat.GuarantorCardNo = txtGuarIDCardNo.Text;
            pat.LastUpdateByUserID = AppSession.UserLogin.UserID;
            pat.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (var guar in RegistrationGuarantors)
            {
                guar.RegistrationNo = reg.RegistrationNo;
                guar.LastUpdateByUserID = AppSession.UserLogin.UserID;
                guar.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            using (var trans = new esTransactionScope())
            {
                reg.Save();
                pat.Save();
                if (updateGuarantor)
                    hist.Save();

                var merge = MergeRegistrationList();
                foreach (var mrg in from s in merge let mrg = new Registration() where mrg.LoadByPrimaryKey(s) select mrg)
                {
                    mrg.GuarantorID = cboGuarantorID.SelectedValue;
                    mrg.PersonID = reg.PersonID;
                    mrg.EmployeeNumber = reg.EmployeeNumber;
                    mrg.Save();
                }

                RegistrationGuarantors.Save();

                trans.Complete();
            }

            return true;
        }

        #region Record Detail Method Function - Registration Guarantor
        private RegistrationGuarantorCollection RegistrationGuarantors
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collRegistrationGuarantor" + Request.UserHostName];
                    if (obj != null)
                        return ((RegistrationGuarantorCollection)(obj));
                }

                var coll = new RegistrationGuarantorCollection();
                var query = new RegistrationGuarantorQuery("a");
                var gq = new GuarantorQuery("b");

                query.Select
                    (
                        query,
                        gq.GuarantorName.As("refToGuarantor_GuarantorName")
                    );

                query.InnerJoin(gq).On(query.GuarantorID == gq.GuarantorID);
                query.Where(query.RegistrationNo == txtRegistrationNo.Text);

                query.OrderBy(query.GuarantorID, esOrderByDirection.Ascending);

                coll.Load(query);

                Session["collRegistrationGuarantor" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collRegistrationGuarantor" + Request.UserHostName] = value; }
        }

        protected void grdRegistrationGuarantor_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRegistrationGuarantor.DataSource = RegistrationGuarantors;
        }

        protected void grdRegistrationGuarantor_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;
            String id =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        RegistrationGuarantorMetadata.ColumnNames.GuarantorID]);

            var tp = new TransPaymentCollection();
            tp.Query.Where(tp.Query.RegistrationNo == txtRegistrationNo.Text, tp.Query.GuarantorID == id,
                           tp.Query.IsVoid == false);
            tp.LoadAll();
            if (tp.Count > 0)
                return;

            RegistrationGuarantor entity = FindItemGuarantorGrid(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdRegistrationGuarantor_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;
            String id =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][RegistrationGuarantorMetadata.ColumnNames.GuarantorID]);
            var tp = new TransPaymentCollection();
            tp.Query.Where(tp.Query.RegistrationNo == txtRegistrationNo.Text, tp.Query.GuarantorID == id,
                           tp.Query.IsVoid == false);
            tp.LoadAll();
            if (tp.Count > 0)
                return;

            RegistrationGuarantor entity = FindItemGuarantorGrid(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdRegistrationGuarantor_InsertCommand(object source, GridCommandEventArgs e)
        {
            RegistrationGuarantor entity = RegistrationGuarantors.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdRegistrationGuarantor.Rebind();
        }

        private void SetEntityValue(RegistrationGuarantor entity, GridCommandEventArgs e)
        {
            var userControl = (RegistrationGuarantorDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.GuarantorID = userControl.GuarantorID;
                entity.GuarantorName = userControl.GuarantorName;
                entity.PlafondAmount = userControl.PlafondAmount;
                entity.Notes = userControl.Notes;
            }
        }

        private RegistrationGuarantor FindItemGuarantorGrid(string guarantorId)
        {
            RegistrationGuarantorCollection coll = RegistrationGuarantors;
            RegistrationGuarantor retval = null;
            foreach (RegistrationGuarantor rec in coll)
            {
                if (rec.GuarantorID.Equals(guarantorId))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }
        #endregion
    }
}
