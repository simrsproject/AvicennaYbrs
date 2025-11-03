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
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Vml;
using System.IO;
using System.Net;
using System.Configuration;
using System.Web;

namespace Temiang.Avicenna.Module.Charges
{
    /// <summary>
    /// Entry Prescription Sales and Realization Prescription Order
    /// Use by ProgramID:
    /// - AppConstant.Program.PrescriptionUddIpr;
    /// - AppConstant.Program.PrescriptionSalesOpr
    /// - AppConstant.Program.PrescriptionSales;
    /// - AppConstant.Program.PrescriptionRealizationOpr
    /// - AppConstant.Program.PrescriptionRealization
    /// - AppConstant.Program.PrescriptionSalesPos
    /// </summary>
    /// Create By: SCI Team
    /// Modified:
    /// -- 25-Nov-2023 Handono --
    /// - UI: Optimize Previouse Buy Info Tooltip lambat untuk kasus data history yg besar 
    ///       -> dirubah menggunakan RadToolTip yg datanya diload via webservice
    /// - UI: Prescription Template menambah beban saat load dan memenuhi layar muka
    ///       -> diganti menggunakan popup window
    public partial class PrescriptionSalesDetail : BasePageDetail
    {
        private bool _isContainHighAlert = false;
        private AppAutoNumberLast _autoNumber;
        private AppAutoNumberLast _autoNumberReg;

        #region Page Event & Initialize

        protected string UniqueID()
        {
            // this should do
            return Request.QueryString["regno"];
        }

        private bool IsFromVerif
        {
            get
            {
                return (Request.QueryString["ver"] == "1");
            }
        }

        private bool IsUddMode
        {
            get
            {
                return "udd".Equals(Request.QueryString["type"]);
            }
        }

        private bool IsRealizationMode
        {
            get
            {
                return "realization".Equals(Request.QueryString["type"]);
            }
        }

        private bool Is23DaysMode
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["refno"]) ? false : true;
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["fromemr"] == "1")
                IsSingleRecordMode = true;

            // Url Search & List
            UrlPageSearch = "PrescriptionSalesSearch.aspx";

            if (AppSession.Parameter.IsNeedVoidReasonOnPrescriptionSales)
            {
                IsUsingBeforeVoidValidation = true;
                IsUsingBeforeUnapprovalValidation = true;
            }

            if (IsUddMode)
            {
                UrlPageList = "PrescriptionUddList.aspx?type=udd";

                //ProgramID = Request.QueryString["rt"] == "opr"
                //                ? AppConstant.Program.PrescriptionUddOpr
                //                : AppConstant.Program.PrescriptionUddIpr;
                ProgramID = AppConstant.Program.PrescriptionUddIpr;
            }
            else if (Request.QueryString["type"] == "sales")
            {
                if (IsFromVerif)
                {
                    UrlPageList = "../PrescriptionVerification/PrescriptionVerificationList.aspx";

                    ProgramID = Request.QueryString["rt"] == "opr"
                                    ? AppConstant.Program.PrescriptionSalesOpr
                                    : AppConstant.Program.PrescriptionSales;
                }
                else
                {
                    if (string.IsNullOrEmpty(Request.QueryString["mode"]) || Request.QueryString["mode"] != "pos")
                    {
                        UrlPageList = "PrescriptionSalesList.aspx?type=sales&rt=" + Request.QueryString["rt"] + "&unit=" + Request.QueryString["unit"] + "&loc=" + Request.QueryString["loc"];

                        ProgramID = Request.QueryString["rt"] == "opr"
                                        ? AppConstant.Program.PrescriptionSalesOpr
                                        : AppConstant.Program.PrescriptionSales;
                    }
                    else
                    {
                        UrlPageList = "PrescriptionSalesPosList.aspx?type=sales&rt=" + Request.QueryString["rt"] + "&unit=" + Request.QueryString["unit"] + "&loc=" + Request.QueryString["loc"];
                        ProgramID = AppConstant.Program.PrescriptionSalesPos;
                    }
                }

                pnlOrderNote.Visible = false;
            }
            else
            {
                UrlPageList = "PrescriptionOrderHandlingList.aspx?type=realization&rt=" + Request.QueryString["rt"] + "&unit=" + Request.QueryString["unit"] + "&loc=" + Request.QueryString["loc"];
                ProgramID = Request.QueryString["rt"] == "opr" ? AppConstant.Program.PrescriptionRealizationOpr : AppConstant.Program.PrescriptionRealization;
            }

            tabs.Tabs[0].Visible = string.IsNullOrEmpty(Request.QueryString["mode"]) || Request.QueryString["mode"] != "pos";


            tabs.Tabs[1].Visible = string.IsNullOrEmpty(Request.QueryString["mode"]);
            tabs.Tabs[2].Visible = !string.IsNullOrEmpty(Request.QueryString["mode"]) && Request.QueryString["mode"] == "pos";
            rfvParamedicID.Visible = string.IsNullOrEmpty(Request.QueryString["mode"]) || Request.QueryString["mode"] != "pos";
            trPrescriptionReview.Visible = AppSession.Parameter.IsPrescriptionReviewActived;

            grdTransPrescriptionItem.Visible = tabs.Tabs[0].Visible;
            grdTransPrescriptionItem2.Visible = tabs.Tabs[2].Visible;

            if (tabs.Tabs[2].Visible)
            {
                tabs.Tabs[2].Selected = true;
                mpg1.SelectedIndex = 2;
            }

            if (!IsPostBack)
            {
                hdnPageId.Value = PageID;
                TransPrescriptionItems = null;

                if (Helper.IsApotekOnlineIntegration)
                {
                    pnlApol.Visible = true;
                    trBpjApol.Visible = true;
                    trPRBApol.Visible = true;
                    btnCariPasienSep.Enabled = false;
                    txtRefAsalSJP.Enabled = false;
                    txtPoliRSP.Enabled = false;
                    cboJnsRsp.Enabled = false;
                    txtNoResep.Enabled = false;
                    txtKdDokter.Enabled = false;
                    cboIterasi.Enabled = false;
                }

                //Guarantor
                var guarQ = new GuarantorQuery("a");
                guarQ.Where(guarQ.IsActive == true);

                var guarDtb = guarQ.LoadDataTable();

                cboGuarantorID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (DataRow row in guarDtb.Rows)
                {
                    cboGuarantorID.Items.Add(new RadComboBoxItem(row["GuarantorName"].ToString(),
                        row["GuarantorID"].ToString()));
                }

                if (string.IsNullOrEmpty(Request.QueryString["mode"]))
                {
                    //CollapsePanel1.IsCollapsed = "true";
                    cboPatientID.Visible = false;
                    btnNewPatient.Visible = false;
                    cboGuarantorID.Enabled = false;
                    trIs23Days.Visible = false;
                    pnlSplitBill.Visible = false;
                }
                else
                {
                    if (Request.QueryString["mode"] == "direct")
                    {
                        if (Request.QueryString["rt"] == "opr")
                        {
                            trIs23Days.Visible = true;
                            pnlSplitBill.Visible = false;
                            txtBpjsSepNo.ReadOnly = false;
                        }
                        else if (Request.QueryString["rt"] == "ipr")
                        {
                            trIs23Days.Visible = false;
                            pnlSplitBill.Visible = AppSession.Parameter.IsPrescriptionSplitBillActived;
                            txtBpjsSepNo.ReadOnly = false;
                        }

                        if (Request.QueryString["fromemr"] == "1")
                        {
                            cboPatientID.Visible = false;
                            btnNewPatient.Visible = false;
                            txtBpjsSepNo.ReadOnly = false;
                        }
                        else
                        {
                            trPatientName.Visible = false;
                            txtSalutation.Visible = false;
                            txtPatientName.Visible = false;
                            txtGender.Visible = false;
                            btnNewPatient.Visible = Request.QueryString["rt"] == "opr";
                            txtBpjsSepNo.ReadOnly = false;
                        }
                        pnlRoom.Visible = false;
                        pnlBedID.Visible = false;
                        pnlClassID.Visible = false;
                        cboGuarantorID.Enabled = true;
                        //trPrescriptionTemplate.Visible = AppSession.Parameter.IsVisibleTemplateForDirectPrescription; // pindah ke aspx (Handono 231125)
                    }
                    else
                    {
                        trIs23Days.Visible = false;
                        pnlSplitBill.Visible = false;
                        //trPrescriptionTemplate.Visible = false;
                        if (Request.QueryString["mode"] == "otc")
                        {
                            rfvParamedicID.Visible = false;
                        }
                        cboPatientID.Visible = false;
                        btnNewPatient.Visible = false;

                        PopulatePatientInformation(AppSession.Parameter.OTCPrescriptionPatientID);
                        PopulateGuarantorFromPatientInformation(AppSession.Parameter.OTCPrescriptionPatientID);
                        cboGuarantorID.Enabled = false;
                        if (Request.QueryString["mode"] == "pos")
                        {
                            lblAdditionalNote.Text = "Customer Name";
                            txtAdditionalNote.Text = txtPatientName.Text;
                        }
                    }
                    string unitId = Request.QueryString["rt"] == "opr"
                                        ? (Request.QueryString["mode"] == "pos"
                                               ? AppSession.Parameter.ServiceUnitPharmacyIdPos
                                               : AppSession.Parameter.ServiceUnitPharmacyIdOpr)
                                        : AppSession.Parameter.ServiceUnitPharmacyID;
                    var unit = new ServiceUnit();
                    if (unit.LoadByPrimaryKey(unitId))
                    {
                        txtServiceUnitID.Text = unit.ServiceUnitID;
                        lblServiceUnitName.Text = unit.ServiceUnitName;
                    }
                }

                //Service Unit
                if (IsUddMode)
                {
                    ComboBox.PopulateWithOneServiceUnit(cboServiceUnitID, Request.QueryString["unit"]);
                    ComboBox.PopulateWithOneLocation(cboLocationID, Request.QueryString["loc"]);

                }
                else
                {
                    ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, TransactionCode.Prescription, true);
                    if (AppSession.Parameter.IsServiceUnitPrescriptionSalesDefaultEmpty)
                        cboServiceUnitID.SelectedValue = string.Empty;
                    else
                        ComboBox.SelectedValue(cboServiceUnitID, Request.QueryString["rt"].ToUpper() == AppConstant.RegistrationType.OutPatient.ToUpper() ? AppSession.Parameter.ServiceUnitPharmacyIdOpr : AppSession.Parameter.ServiceUnitPharmacyID);

                    if (!string.IsNullOrEmpty(Request.QueryString["unit"]))
                    {
                        ComboBox.SelectedValue(cboServiceUnitID, Request.QueryString["unit"]);
                    }

                    ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, cboServiceUnitID.SelectedValue);

                    if (!string.IsNullOrEmpty(Request.QueryString["loc"]))
                    {
                        ComboBox.SelectedValue(cboLocationID, Request.QueryString["loc"]);
                    }

                    SetPrescUnitAndLocationFromGuar();
                }

                // UDD Mode tidak boleh diganti Service Unit dan Lokasinya karena berdasar pada Unit & Lokasi source Udd Item nya
                cboServiceUnitID.Enabled = !IsUddMode;
                cboLocationID.Enabled = !IsUddMode;


                //Paramedic
                if (IsUddMode && Request.QueryString["md"] == "new") // Request.QueryString["md"] == "new" -> codingan gak bagus tapi terpaksa
                {
                    ComboBox.PopulateWithOneParamedic(cboParamedicID, Request.QueryString["parid"]);
                }
                else
                {
                    var query = new ParamedicQuery("a");
                    query.Where
                        (
                            query.IsActive == true,
                            query.IsAvailable == true
                        );
                    var dtb = query.LoadDataTable();

                    cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    foreach (DataRow row in dtb.Rows)
                    {
                        cboParamedicID.Items.Add(new RadComboBoxItem(row["ParamedicName"].ToString(), row["ParamedicID"].ToString()));
                    }
                }
                cboParamedicID.Enabled = !IsUddMode;

                StandardReference.InitializeIncludeSpace(cboGuarSRRelationship, AppEnum.StandardReference.Relationship);
                pnlEmployeeInfo.Visible = AppSession.Parameter.IsUsingHumanResourcesModul &&
                                          Request.QueryString["mode"] == "direct";

                //trUnitDose.Visible = Request.QueryString["rt"] == "ipr";

                StandardReference.InitializeIncludeSpace(cboSRFloor, AppEnum.StandardReference.Floor);

                pnlAdditionalOpr.Visible = (Request.QueryString["rt"] == "opr");
                trExecutionDate.Visible = AppSession.Parameter.IsDisplayExecutionDateOnPrescriptionSales;
            }

            // Additional Toolbar item 
            // Diremark dulu khawatir mempersulit implementasi
            //if (AppSession.Parameter.HealthcareInitial.Equals("RSTJ"))
            //{
            //    // Add menu status
            //    var tbiSplit = new RadToolBarButton();
            //    tbiSplit.IsSeparator = true;
            //    ToolBarMenuData.Items.Add(tbiSplit);

            //    var tbi = new RadToolBarButton();
            //    tbi.Text = "In Progress";
            //    tbi.Value = "inprogress";
            //    ToolBarMenuData.Items.Add(tbi);

            //    var tbi2 = new RadToolBarButton();
            //    tbi2.Text = "Complete";
            //    tbi2.Value = "complete";
            //    ToolBarMenuData.Items.Add(tbi2);

            //    var tbi3 = new RadToolBarButton();
            //    tbi3.Text = "Deliver";
            //    tbi3.Value = "deliver";
            //    ToolBarMenuData.Items.Add(tbi3);
            //}
        }

        #region Additional Toolbar menu item
        public override string OnGetAdditionalMenuScript()
        {
            var script = @"
case ""inprogress"":
    if (!window.confirm('This prescription is in progress?'))
    {
         args.set_cancel(true);
    }
    break; 
case ""ready"":
    if (!window.confirm('This prescription is ready?'))
    {
         args.set_cancel(true);
    }
    break;
case ""deliver"":
    if (!window.confirm('This prescription is deliver?'))
    {
         args.set_cancel(true);
    }
    break;

";

            return script;
        }
        protected override void OnToolBarMenuDataAdditionalButtonClick(ValidateArgs args, string value)
        {
            UpdatePrescriptionStatus(txtPrescriptionNo.Text, value);
        }
        #endregion
        public static void UpdatePrescriptionStatus(string prescriptionNo, string stat)
        {
            var taskId = "4";
            var registrationNo = string.Empty;
            switch (stat)
            {
                case "inprogress":
                    {
                        taskId = "4";
                        var tp = new TransPrescription();
                        if (tp.LoadByPrimaryKey(prescriptionNo))
                        {
                            if (tp.InProgressDateTime == null) // Jangan di override
                            {
                                tp.InProgressDateTime = DateTime.Now;
                                tp.InProgressByUserID = AppSession.UserLogin.UserID;
                            }
                            tp.IsProceedByPharmacist = true;
                            tp.Save();

                            registrationNo = tp.RegistrationNo;
                        }
                        break;
                    }
                case "complete":
                    {
                        taskId = "6";
                        var tp = new TransPrescription();
                        if (tp.LoadByPrimaryKey(prescriptionNo))
                        {
                            tp.CompleteDateTime = DateTime.Now;
                            tp.CompleteByUserID = AppSession.UserLogin.UserID;
                            tp.Save();

                            registrationNo = tp.RegistrationNo;
                        }
                        break;
                    }
                case "deliver":
                    {
                        taskId = "7";
                        var tp = new TransPrescription();
                        if (tp.LoadByPrimaryKey(prescriptionNo))
                        {
                            tp.DeliverDateTime = DateTime.Now;
                            tp.DeliverByUserID = AppSession.UserLogin.UserID;
                            tp.Save();

                            registrationNo = tp.RegistrationNo;
                        }
                        break;
                    }
            }


            //Hanya untuk Tarakan
            if (AppSession.Parameter.HealthcareInitial.Equals("RSTJ"))
            {
                //TaskID 4: Mulai disiapkan
                //TaskID 6: Selesai disiapkan
                //TaskID 7: Selesai diberikan ke Pasien

                var aolTask = new AppointmentOnlineTask();
                if (!aolTask.LoadByPrimaryKey(registrationNo, taskId))
                {
                    aolTask = new AppointmentOnlineTask();
                    aolTask.AppointmentNo = registrationNo;
                    aolTask.TaskId = taskId;
                    aolTask.Timestamp = (new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds()).ToString();
                    aolTask.LastUpdatedDate = DateTime.Now;
                    aolTask.IsSended = false;
                    aolTask.IsError = false;
                    aolTask.Attempt = 0;
                    aolTask.Save();
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            string eventTarget = Request.Params.Get("__EVENTTARGET");
            if (eventTarget == "ShowNewPatient")
            {
                string passedArgument = Request.Params.Get("__EVENTARGUMENT");
                PopulatePatientInformation(passedArgument);
                PopulateGuarantorFromPatientInformation(passedArgument);
            }

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["mode"]) && (Request.QueryString["mode"] == "direct" || Request.QueryString["mode"] == "pos")) return;
                if (IsUddMode || Is23DaysMode)
                {
                    PopulateSoap();
                    return;
                }

                if (DataModeCurrent == AppEnum.DataMode.New)
                {
                    var cstext1 = new StringBuilder();
                    cstext1.Append(@"<script type=text/javascript>
function OpenAddNewRecordGrid(){ 
    __doPostBack('ctl00$ContentPlaceHolder1$grdTransPrescriptionItem$ctl00$ctl02$ctl00$AddNewRecordButton',''); 
    Sys.Application.remove_load(OpenAddNewRecordGrid);
} 

Sys.Application.add_load(OpenAddNewRecordGrid); 
</script>");

                    Page.ClientScript.RegisterStartupScript(GetType(), "OpenAddNewRecordGrid", cstext1.ToString());
                }

                grdLabHist.DataSource = GetLabHistTable;
                grdLabHist.DataBind();

                // load queue type

                LoadQueType();

                if (DataModeCurrent == AppEnum.DataMode.Edit)
                {
                    chkIsSplitBill.Enabled = false;
                    chkIsCash.Enabled = false;
                }
            }

            PopulateSoap();
        }

        private void LoadQueType()
        {
            if (!string.IsNullOrEmpty(AppSession.Parameter.PrescriptionQueueStdiItemID))
            {
                if (cboQueueType.Items.Count == 0)
                {
                    var qpStds = AppSession.Parameter.PrescriptionQueueStdiItemID.Split(';');
                    var stdiColl = new AppStandardReferenceItemCollection();
                    stdiColl.LoadByStdRefGroups("KioskQueueType", qpStds);
                    if (stdiColl.Count == 0)
                    {
                        pnlQueType.Visible = false;
                    }
                    else
                    {
                        cboQueueType.Items.Add("");
                        foreach (var stdi in stdiColl)
                        {
                            cboQueueType.Items.Add(new RadComboBoxItem(stdi.ItemName, stdi.ItemID));
                        }
                    }
                }
            }
            else
            {
                pnlQueType.Visible = false;
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            ToolBarMenuSearch.Visible = false;

            //Additional Toolbar
            //var tp = new TransPrescription();
            //if (tp.LoadByPrimaryKey(txtPrescriptionNo.Text))
            //{
            //    // Disable tombol status
            //    ToolBarMenuData.FindItemByValue("inprogress").Enabled = !ToolBarMenuSave.Visible && tp.InProgressDateTime == null;
            //    ToolBarMenuData.FindItemByValue("ready").Enabled = !ToolBarMenuSave.Visible && tp.CompleteDateTime == null;
            //    ToolBarMenuData.FindItemByValue("deliver").Enabled = !ToolBarMenuSave.Visible && tp.DeliverDateTime == null;
            //}

            //var appParam = new AppParameter();
            //if (appParam.LoadByPrimaryKey("PrescriptionEnableCustomEtiquette"))
            //{
            //    grdTransPrescriptionItem.Columns[grdTransPrescriptionItem.Columns.Count - 3].Visible = !OnGetStatusMenuApproval() & (appParam.ParameterValue == "1");
            //}
            //else
            //{
            //    grdTransPrescriptionItem.Columns[grdTransPrescriptionItem.Columns.Count - 3].Visible = false;
            //}
        }

        private void PopulateRegistrationInformation(string registrationNo)
        {
            trInsuranceID.Visible = false;

            if (string.IsNullOrEmpty(registrationNo))
                return;

            var registration = new Registration();
            if (registration.LoadByPrimaryKey(registrationNo))
            {
                var patient = new Patient();
                if (patient.LoadByPrimaryKey(registration.PatientID))
                {
                    txtMedicalNo.Text = patient.MedicalNo;
                    var std = new AppStandardReferenceItem();
                    txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.SRSalutation) ? std.ItemName : string.Empty;
                    txtPatientName.Text = patient.PatientName;
                    txtGender.Text = patient.Sex;

                    optSexFemale.Checked = patient.Sex.Equals("F");
                    optSexMale.Checked = patient.Sex.Equals("M");
                    if (patient.Sex.Equals("F"))
                        optSexMale.Enabled = false;
                    else
                        optSexFemale.Enabled = false;

                    txtPlaceDOB.Text = string.Format("{0}, {1}", patient.CityOfBirth, Convert.ToDateTime(patient.DateOfBirth).ToString("dd-MMM-yyyy"));
                    txtAgeDay.Value = Helper.GetAgeInDay(patient.DateOfBirth.Value);
                    txtAgeMonth.Value = Helper.GetAgeInMonth(patient.DateOfBirth.Value);
                    txtAgeYear.Value = Helper.GetAgeInYear(patient.DateOfBirth.Value);

                    txtGuarantorCardNo.Text = patient.GuarantorCardNo;

                    cboPatientID.Items.Clear();
                    cboPatientID.Items.Add(new RadComboBoxItem(txtPatientName.Text, patient.PatientID));
                    cboPatientID.SelectedValue = patient.PatientID;

                    PopulatePregnantStatus(patient.PatientID);
                    PopulatePatientImage(patient.PatientID);
                }
                else
                {
                    cboPatientID.Items.Clear();
                    cboPatientID.Items.Add(new RadComboBoxItem("Unknown", registration.PatientID));
                    cboPatientID.SelectedValue = patient.PatientID;
                }
                //var patQ = new PatientQuery();
                //patQ.Select(patQ.PatientID, patQ.PatientName, patQ.DateOfBirth, patQ.MedicalNo, patQ.Address);
                //patQ.Where(patQ.PatientID == registration.PatientID);
                //DataTable patTbl = patQ.LoadDataTable();
                //cboPatientID.DataSource = patTbl;
                //cboPatientID.DataBind();
                //cboPatientID.SelectedValue = registration.PatientID;
                //cboPatientID.Text = patTbl.Rows[0]["PatientName"].ToString();

                txtServiceUnitID.Text = registration.ServiceUnitID;

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(txtServiceUnitID.Text);
                lblServiceUnitName.Text = unit.ServiceUnitName;
                txtRoomID.Text = registration.RoomID;

                var room = new ServiceRoom();
                room.LoadByPrimaryKey(txtRoomID.Text);
                lblRoomName.Text = room.RoomName;

                txtBedID.Text = registration.BedID;

                txtClassID.Text = registration.ChargeClassID;
                var cls = new Class();
                cls.LoadByPrimaryKey(txtClassID.Text);
                lblClassName.Text = cls.ClassName;

                if (!IsUddMode) // Mode UDD sudah diset pada saat Init yg dikirim dari form list UDD outstanding
                {
                    cboParamedicID.SelectedValue = AppSession.Parameter.IsPhysicianPrescriptionSalesDefaultEmpty ? string.Empty : registration.ParamedicID;
                }
                cboGuarantorID.SelectedValue = registration.GuarantorID;

                var guar = new Guarantor();
                guar.LoadByPrimaryKey(registration.GuarantorID);
                trBpjsSepNo.Visible = guar.SRGuarantorType == AppSession.Parameter.GuarantorTypeBPJS;
                txtBpjsSepNo.Text = registration.BpjsSepNo;

                if (!string.IsNullOrEmpty(registration.InsuranceID))
                {
                    trInsuranceID.Visible = true;
                }

                txtInsuranceID.Text = registration.InsuranceID;

                if (registration.PersonID != null)
                {
                    var empq = new PersonalInfoQuery();
                    empq.Where(empq.PersonID == registration.PersonID);
                    cboEmployeeID.DataSource = empq.LoadDataTable();
                    cboEmployeeID.DataBind();
                    cboEmployeeID.SelectedValue = registration.PersonID.ToString();
                }

                if (guar.SRGuarantorType == AppSession.Parameter.GuarantorTypeEmployee)
                {
                    var emp = new PersonalInfo();
                    if (emp.LoadByPrimaryKey(Convert.ToInt32(registration.PersonID)))
                    {
                        cboEmployeeID.Enabled = !AppSession.Parameter.IsRADTLinkToHumanResourcesModul;
                        cboGuarSRRelationship.Enabled = !AppSession.Parameter.IsRADTLinkToHumanResourcesModul;
                    }
                    else
                    {
                        string searchTextContain = string.Format("%{0}%", cboGuarantorID.SelectedValue);
                        var pars = new AppParameterCollection();
                        pars.Query.Where(pars.Query.ParameterID == "DependentsOfEmployeesGuarantorID",
                                         pars.Query.ParameterValue.Like(searchTextContain));
                        pars.LoadAll();
                        if (pars.Count > 0)
                        {
                            cboEmployeeID.Enabled = true;
                            cboGuarSRRelationship.Enabled = true;
                        }
                        else
                        {
                            cboEmployeeID.Enabled = !AppSession.Parameter.IsRADTLinkToHumanResourcesModul;
                            cboGuarSRRelationship.Enabled = !AppSession.Parameter.IsRADTLinkToHumanResourcesModul;
                        }
                    }
                }
                cboGuarSRRelationship.SelectedValue = registration.SREmployeeRelationship;

                cboSRFloor.SelectedValue = room.SRFloor;

                if (registration.SRRegistrationType != AppConstant.RegistrationType.InPatient)
                {
                    var pat = new Patient();
                    if (pat.LoadByPrimaryKey(registration.PatientID))
                    {
                        //var phoneNo = ((pat.PhoneNo.Trim() + " " + pat.MobilePhoneNo).Trim()).Trim();
                        var phoneNo = pat.PhoneNo.Trim();
                        txtNoTelp.Text = phoneNo.Length > 50 ? phoneNo.Substring(0, 10) : phoneNo;
                        txtMobilePhoneNo.Text = pat.MobilePhoneNo;

                        var addr = (pat.StreetName.Trim() + " " + pat.District.Trim() + " " + pat.County.Trim()).Trim();
                        txtFullAddress.Text = addr.Length > 200 ? addr.Substring(0, 10) : addr;
                    }
                }

                if (string.IsNullOrEmpty(Request.QueryString["mode"]))
                {
                    CollapsePanel1.Title = txtPatientName.Text +
                                            " [" + txtMedicalNo.Text + "] [" + txtRegistrationNo.Text + "] " +
                                            " - " + patient.DateOfBirth.Value.ToShortDateString() + " - " +
                                            (optSexMale.Checked ? "M [" : "F [") +
                                            cboParamedicID.Text + "] " +
                                            lblServiceUnitName.Text + ", " +
                                            lblRoomName.Text + ", " +
                                            txtBedID.Text + ", " +
                                            cboGuarantorID.Text;
                }
                else
                {
                    CollapsePanel1.Title = "Patient Information";
                }

                var sepapol = new BpjsSEP();
                if (sepapol.LoadByPrimaryKey(txtBpjsSepNo.Text))
                {
                    if (sepapol.TanggalSEP.HasValue)
                    {
                        txtTglSep.SelectedDate = sepapol.TanggalSEP.Value;
                    }
                }

                if (Helper.IsApotekOnlineIntegration)
                {
                    var apol = new BpjsApol();
                    apol.Query.Where(apol.Query.REFASALSJP == txtBpjsSepNo.Text, apol.Query.PrescriptionNo == txtPrescriptionNo.Text);
                    if (apol.Query.Load())
                    {
                        txtRefAsalSJP.Text = apol.REFASALSJP;
                        txtPoliRSP.Text = apol.POLIRSP;
                        cboJnsRsp.SelectedValue = apol.KDJNSOBAT.ToString();
                        txtNoResep.Text = apol.NORESEP;
                        txtKdDokter.Text = apol.KDDOKTER;
                        cboIterasi.SelectedValue = apol.ITERASI.ToString();
                        txtTglRsp.SelectedDate = apol.TGLRSP;

                        //if (apol.TGLSJP != null)
                        //    txtTglSep.SelectedDate = apol.TGLSJP;

                        var apoldet = new BpjsApolDetailCollection();
                        apoldet.Query.Where(
                            apoldet.Query.PrescriptionNo == apol.PrescriptionNo,
                            apoldet.Query.BpjsApolID == apol.ID,
                            apoldet.Query.MetadataCode != "200"
                        );

                        if (apoldet.Query.Load())
                        {
                            lblApolDtl.Text = $"Detail APOL ditemukan: {apoldet.Count} item Belum Terkirim";
                        }

                        if (apol.MetadataCode == "200")
                        {
                            btnCreateAPOL.Enabled = false;
                        }

                        lblCreateApolResult.Text = apol.MetadataMessage;
                        lblCreateApolResult.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblCreateApolResult.Text = "Data APOL tidak ditemukan.";
                        lblCreateApolResult.ForeColor = System.Drawing.Color.Red;

                        txtRefAsalSJP.Text = "";
                        txtPoliRSP.Text = "";
                        cboJnsRsp.ClearSelection();
                        txtNoResep.Text = "";
                        txtKdDokter.Text = "";
                        cboIterasi.ClearSelection();
                    }
                }
            

            #region height, weight, allergies
            decimal h = 0, w = 0;
                string alergies = string.Empty, hl = "Cm", wl = "Kg";
                var registrationNoList = Helper.MergeBilling.GetMergeRegistration(registrationNo);

                //tinggi badan
                var phrq = new PatientHealthRecordLineQuery("a");
                var qq = new QuestionQuery("b");
                phrq.InnerJoin(qq).On(qq.QuestionID == phrq.QuestionID);
                phrq.Where(phrq.RegistrationNo == registrationNo, qq.VitalSignID == "HEIGHT", phrq.QuestionAnswerNum > 0);
                //phrq.Where(phrq.RegistrationNo.In(registrationNoList), qq.VitalSignID == "HEIGHT", phrq.QuestionAnswerNum > 0);
                phrq.OrderBy(phrq.LastUpdateDateTime.Descending);
                phrq.es.Top = 1;

                var phrcoll = new PatientHealthRecordLineCollection();
                phrcoll.Load(phrq);
                foreach (var p in phrcoll)
                {
                    h = p.QuestionAnswerNum ?? 0;
                    if (!string.IsNullOrEmpty(p.QuestionAnswerSuffix))
                        hl = p.QuestionAnswerSuffix;
                }

                //berat badan
                phrq = new PatientHealthRecordLineQuery("a");
                qq = new QuestionQuery("b");
                phrq.InnerJoin(qq).On(qq.QuestionID == phrq.QuestionID);
                phrq.Where(phrq.RegistrationNo == registrationNo, qq.VitalSignID.In("WEIGHT", "Wg1", "BABYWG"), phrq.QuestionAnswerNum > 0);
                //phrq.Where(phrq.RegistrationNo.In(registrationNoList), qq.VitalSignID.In("WEIGHT", "Wg1", "BABYWG"), phrq.QuestionAnswerNum > 0);
                phrq.OrderBy(phrq.LastUpdateDateTime.Descending);
                phrq.es.Top = 1;

                phrcoll = new PatientHealthRecordLineCollection();
                phrcoll.Load(phrq);
                foreach (var p in phrcoll)
                {
                    w = p.QuestionAnswerNum ?? 0;
                    if (!string.IsNullOrEmpty(p.QuestionAnswerSuffix))
                        wl = p.QuestionAnswerSuffix;
                }

                txtHeight.Text = string.Format("{0:n0}", h);
                lblHeight.Text = hl;
                txtWeight.Text = string.Format("{0:n0}", w);
                lblWeight.Text = wl;

                //alergi
                var pacoll = new PatientAllergyCollection();
                pacoll.Query.Where(pacoll.Query.AllergyGroup == AppSession.Parameter.DrugAllergenGroupID, pacoll.Query.PatientID == registration.PatientID);
                pacoll.Query.OrderBy(pacoll.Query.AllergenName.Ascending);
                pacoll.LoadAll();
                foreach (var pa in pacoll)
                {
                    var descAndReaction = pa.DescAndReaction.Replace("{", "(").Replace("}", ")");
                    if (alergies == string.Empty)
                        alergies += string.Format(@"{0}", descAndReaction);
                    else
                        alergies += string.Format(@"{0} {1}", ", ", descAndReaction);
                }

                txtAllergies.Text = alergies;

                #endregion

                if (string.IsNullOrEmpty(Request.QueryString["emrregno"]) && !string.IsNullOrEmpty(registration.FromRegistrationNo))
                {
                    var regq2 = new RegistrationQuery("a");
                    var patq2 = new PatientQuery("b");
                    var unitq2 = new ServiceUnitQuery("c");
                    var roomq2 = new ServiceRoomQuery("d");

                    regq2.Select(regq2.RegistrationNo, patq2.PatientName, patq2.MedicalNo, regq2.PatientID, unitq2.ServiceUnitName,
                               roomq2.RoomName, regq2.BedID, regq2.GuarantorID);

                    regq2.InnerJoin(patq2).On(patq2.PatientID == regq2.PatientID);
                    regq2.LeftJoin(unitq2).On(unitq2.ServiceUnitID == regq2.ServiceUnitID);
                    regq2.LeftJoin(roomq2).On(roomq2.RoomID == regq2.RoomID);

                    regq2.Where(regq2.RegistrationNo == registration.FromRegistrationNo);

                    DataTable dtb = regq2.LoadDataTable();
                    cboFromRegistrationNo.DataSource = dtb;
                    cboFromRegistrationNo.DataBind();
                    cboFromRegistrationNo.SelectedValue = registration.FromRegistrationNo;
                }
                else
                {
                    cboFromRegistrationNo.Items.Clear();
                    cboFromRegistrationNo.Text = string.Empty;
                }

                lblRegistrationInfo2.Text = RegistrationInfoSumary.GetDocumentCheckListCountRemains(registrationNo);
            }
        }

        private void PopulatePatientInformation(string patientID)
        {
            if (string.IsNullOrEmpty(patientID))
                return;

            var patient = new Patient();
            if (patient.LoadByPrimaryKey(patientID))
            {
                cboPatientID.SelectedValue = patient.PatientID;
                cboPatientID.Text = patient.PatientName;
                txtMedicalNo.Text = patient.MedicalNo;
                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.SRSalutation) ? std.ItemName : string.Empty;
                txtPatientName.Text = patient.PatientName;
                txtGender.Text = patient.Sex;

                optSexFemale.Checked = patient.Sex.Equals("F");
                optSexMale.Checked = patient.Sex.Equals("M");
                if (patient.Sex.Equals("F"))
                    optSexMale.Enabled = false;
                else
                    optSexFemale.Enabled = false;

                txtPlaceDOB.Text = string.Format("{0}, {1}", patient.CityOfBirth, Convert.ToDateTime(patient.DateOfBirth).ToString("dd-MMM-yyyy"));
                txtAgeDay.Value = Helper.GetAgeInDay(patient.DateOfBirth.Value);
                txtAgeMonth.Value = Helper.GetAgeInMonth(patient.DateOfBirth.Value);
                txtAgeYear.Value = Helper.GetAgeInYear(patient.DateOfBirth.Value);

                txtGuarantorCardNo.Text = patient.GuarantorCardNo;

                txtNoTelp.Text = patient.PhoneNo.Trim();
                txtMobilePhoneNo.Text = patient.MobilePhoneNo;
                txtFullAddress.Text = ((patient.StreetName + " " + patient.District).Trim() + " " + patient.County).Trim();

                //alergi
                var alergies = string.Empty;
                var pacoll = new PatientAllergyCollection();
                pacoll.Query.Where(pacoll.Query.PatientID == patientID);
                pacoll.Query.OrderBy(pacoll.Query.AllergenName.Ascending);
                pacoll.LoadAll();
                foreach (var pa in pacoll)
                {
                    var descAndReaction = pa.DescAndReaction.Replace("{", "(").Replace("}", ")");
                    if (alergies == string.Empty)
                        alergies += string.Format(@"{0}", descAndReaction);
                    else
                        alergies += string.Format(@"{0} {1}", ", ", descAndReaction);
                }

                txtAllergies.Text = alergies;

                PopulatePatientImage(patient.PatientID);
                PopulatePregnantStatus(patient.PatientID);
            }
        }

        private void PopulatePregnantStatus(string patientID)
        {
            if (!pnlOrderNote.Visible) return;

            var isPregnant = PatientField.GetValueBool(patientID, AppField.FieldNameEnum.IsPregnant) ?? false;
            var fdolm = PatientField.GetValueDateTime(patientID, AppField.FieldNameEnum.Fdolm);
            var isBreastFeeding = PatientField.GetValueBool(patientID, AppField.FieldNameEnum.IsBreastFeeding) ?? false;
            var isBreastfeedingL6M = PatientField.GetValueBool(patientID, AppField.FieldNameEnum.IsBreastFeedingL6M) ?? false;
            var isBreastfeedingM6M = PatientField.GetValueBool(patientID, AppField.FieldNameEnum.IsBreastFeedingM6M) ?? false;
            pnlPregnantStat.Visible = fdolm != null || isPregnant || isBreastFeeding || isBreastfeedingL6M || isBreastfeedingM6M;

            if (pnlPregnantStat.Visible)
            {
                if (fdolm != null)
                {
                    txtFdolm.SelectedDate = fdolm.Value;
                    txtEstBirthDate.SelectedDate = EstBirthDate(fdolm.Value);
                    PopulatePregnantAgeInfo(txtFdolm.SelectedDate.Value);
                }
                chkIsPregnant.Checked = isPregnant || !string.IsNullOrEmpty(txtPregnantAge.Text);

                chkIsBreastfeeding.Checked = isBreastFeeding || isBreastfeedingL6M || isBreastfeedingM6M;
                chkIsBreastfeedingL6M.Checked = isBreastfeedingL6M;
                chkIsBreastfeedingM6M.Checked = isBreastfeedingM6M;
            }
        }
        private void PopulatePregnantAgeInfo(DateTime? fdlom)
        {
            if (fdlom != null)
            {
                var ageInDays = (DateTime.Today - fdlom.Value).TotalDays;
                var week = Math.Floor((ageInDays / 7)).ToInt();
                var day = (ageInDays % 7);

                if (week > 0)
                    txtPregnantAge.Text = string.Concat(txtPregnantAge.Text, week, " weeks ");

                if (day > 0)
                    txtPregnantAge.Text = string.Concat(txtPregnantAge.Text, day, " days ");
            }
        }

        private DateTime EstBirthDate(DateTime fdlom)
        {
            if (fdlom.Month <= 3) // Jan s/d Maret
                return (new DateTime(fdlom.Year, fdlom.Month + 9, fdlom.Day)).AddDays(7);
            else
                return (new DateTime(fdlom.Year + 1, fdlom.Month - 3, fdlom.Day)).AddDays(7);
        }

        private void PopulateGuarantorFromPatientInformation(string patientId)
        {
            if (string.IsNullOrEmpty(patientId))
                return;

            var patient = new Patient();
            if (patient.LoadByPrimaryKey(patientId))
            {
                cboGuarantorID.SelectedValue = patient.GuarantorID;

                if (patient.PersonID != null)
                {
                    var empq = new PersonalInfoQuery();
                    empq.Where(empq.PersonID == patient.PersonID);
                    cboEmployeeID.DataSource = empq.LoadDataTable();
                    cboEmployeeID.DataBind();
                    cboEmployeeID.SelectedValue = patient.PersonID.ToString();
                }

                var guar = new Guarantor();
                guar.LoadByPrimaryKey(cboGuarantorID.SelectedValue);

                if (guar.SRGuarantorType == AppSession.Parameter.GuarantorTypeEmployee)
                {
                    var emp = new PersonalInfo();
                    if (emp.LoadByPrimaryKey(Convert.ToInt32(patient.PersonID)))
                    {
                        cboEmployeeID.Enabled = !AppSession.Parameter.IsRADTLinkToHumanResourcesModul;
                        cboGuarSRRelationship.Enabled = !AppSession.Parameter.IsRADTLinkToHumanResourcesModul;
                    }
                    else
                    {
                        string searchTextContain = string.Format("%{0}%", cboGuarantorID.SelectedValue);
                        var pars = new AppParameterCollection();
                        pars.Query.Where(pars.Query.ParameterID == "DependentsOfEmployeesGuarantorID",
                                         pars.Query.ParameterValue.Like(searchTextContain));
                        pars.LoadAll();
                        if (pars.Count > 0)
                        {
                            cboEmployeeID.Enabled = true;
                            cboGuarSRRelationship.Enabled = true;
                        }
                        else
                        {
                            cboEmployeeID.Enabled = !AppSession.Parameter.IsRADTLinkToHumanResourcesModul;
                            cboGuarSRRelationship.Enabled = !AppSession.Parameter.IsRADTLinkToHumanResourcesModul;
                        }
                    }
                    cboGuarSRRelationship.SelectedValue = patient.SREmployeeRelationship;
                }
                else
                    cboGuarantorID.SelectedValue = AppSession.Parameter.SelfGuarantor;
            }
            else
            {
                cboGuarantorID.SelectedValue = AppSession.Parameter.SelfGuarantor;
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdTransPrescriptionItem, grdTransPrescriptionItem);
            ajax.AddAjaxSetting(grdTransPrescriptionItem, grdTransPrescriptionItem2);
            ajax.AddAjaxSetting(grdTransPrescriptionItem, txtPatientAmount);
            ajax.AddAjaxSetting(grdTransPrescriptionItem, txtGuarantorAmount);
            ajax.AddAjaxSetting(grdTransPrescriptionItem, cboServiceUnitID);
            ajax.AddAjaxSetting(grdTransPrescriptionItem, cboLocationID);
            ajax.AddAjaxSetting(grdTransPrescriptionItem, cboGuarantorID);

            ajax.AddAjaxSetting(grdTransPrescriptionItem2, grdTransPrescriptionItem2);
            ajax.AddAjaxSetting(grdTransPrescriptionItem2, grdTransPrescriptionItem);
            ajax.AddAjaxSetting(grdTransPrescriptionItem2, cboServiceUnitID);
            ajax.AddAjaxSetting(grdTransPrescriptionItem2, cboLocationID);

            ajax.AddAjaxSetting(AjaxManager, txtPatientAmount);
            ajax.AddAjaxSetting(AjaxManager, txtGuarantorAmount);

            ajax.AddAjaxSetting(cboGuarantorID, cboGuarantorID);
            ajax.AddAjaxSetting(cboGuarantorID, cboEmployeeID);
            ajax.AddAjaxSetting(cboGuarantorID, cboGuarSRRelationship);

            ajax.AddAjaxSetting(cboServiceUnitID, cboServiceUnitID);
            ajax.AddAjaxSetting(cboServiceUnitID, cboLocationID);
            ajax.AddAjaxSetting(cboServiceUnitID, hdnServiceUnitID);
            ajax.AddAjaxSetting(cboServiceUnitID, hdnLocationID);

            ajax.AddAjaxSetting(cboLocationID, hdnLocationID);

            ajax.AddAjaxSetting(AjaxManager, cboEmployeeID);
            ajax.AddAjaxSetting(AjaxManager, cboGuarSRRelationship);

            if (trIs23Days.Visible)
            {
                ajax.AddAjaxSetting(chkIs23Days, chkIs23Days);
                ajax.AddAjaxSetting(chkIs23Days, cboGuarantorID);
            }
            if (pnlSplitBill.Visible)
            {
                ajax.AddAjaxSetting(chkIsSplitBill, chkIsSplitBill);
                ajax.AddAjaxSetting(chkIsSplitBill, chkIsCash);
                ajax.AddAjaxSetting(chkIsSplitBill, cboGuarantorID);

                ajax.AddAjaxSetting(chkIsCash, chkIsCash);
                ajax.AddAjaxSetting(chkIsCash, chkIsSplitBill);
                ajax.AddAjaxSetting(chkIsCash, cboGuarantorID);
                ajax.AddAjaxSetting(chkIsCash, cboGuarantorID);

                ajax.AddAjaxSetting(cboFromRegistrationNo, cboFromRegistrationNo);
                ajax.AddAjaxSetting(cboFromRegistrationNo, cboGuarantorID);
                ajax.AddAjaxSetting(cboFromRegistrationNo, cboParamedicID);
                ajax.AddAjaxSetting(cboFromRegistrationNo, txtBpjsSepNo);
            }
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new TransPrescription());

            trPayment.Visible = false;

            //if (!IsPostBack)
            //{
            if (AppSession.Parameter.IsServiceUnitPrescriptionSalesDefaultEmpty)
            {
                cboServiceUnitID.SelectedValue = string.Empty;
            }
            else
            {
                if (cboServiceUnitID.Items.Count == 2)
                    cboServiceUnitID.SelectedIndex = 1;
                else
                {
                    var reg = new Registration();
                    if (reg.LoadByPrimaryKey(Request.QueryString["regno"]))
                    {
                        var su = new ServiceUnit();
                        if (su.LoadByPrimaryKey(reg.ServiceUnitID))
                        {
                            if (!string.IsNullOrEmpty(su.ServiceUnitPharmacyID))
                                cboServiceUnitID.SelectedValue = su.ServiceUnitPharmacyID;
                            else
                                cboServiceUnitID.SelectedValue = Request.QueryString["rt"] == "opr"
                                                                     ? (!string.IsNullOrEmpty(Request.QueryString["mode"]) && Request.QueryString["mode"] == "por")
                                                                           ? AppSession.Parameter.ServiceUnitPharmacyIdPos
                                                                           : AppSession.Parameter.ServiceUnitPharmacyIdOpr
                                                                     : AppSession.Parameter.ServiceUnitPharmacyID;
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, cboServiceUnitID.SelectedValue);
                cboLocationID.SelectedIndex = 1;
            }
            else
            {
                cboLocationID.Items.Clear();
                cboLocationID.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["unit"]))
            {
                cboServiceUnitID.SelectedValue = Request.QueryString["unit"];
                if (!string.IsNullOrEmpty(Request.QueryString["loc"]))
                {
                    cboLocationID.SelectedValue = Request.QueryString["loc"];
                }
            }

            txtRegistrationNo.Text = Request.QueryString["regno"];

            SetPrescUnitAndLocationFromGuar();

            txtPrescriptionDate.SelectedDate = DateTime.Today;
            txtExecutionDate.SelectedDate = DateTime.Today;
            txtPrescriptionNo.Text = GetNewPrescriptionNo(chkUnitDose.Checked);

            cboServiceUnitID.AutoPostBack = true;
            if (!string.IsNullOrEmpty(Request.QueryString["mode"]))
            {
                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    txtRegistrationNo.Text = GetNewRegistrationNo();
                chkIsDirect.Checked = true;
                pnlRoom.Visible = false;
                pnlBedID.Visible = false;
                pnlClassID.Visible = false;
            }
            else
            {
                chkIsDirect.Checked = false;
                pnlRoom.Visible = true;
                pnlBedID.Visible = true;
                pnlClassID.Visible = true;
            }

            if (Request.QueryString["md"] == "new")
            {
                if (Request.QueryString["mode"] == "direct" && Request.QueryString["fromemr"] == "1")
                {
                    // Jika dipanggil dari EMR dg info sumber regno utk mengisi default patient name etc
                    PopulateRegistrationInformation(Request.QueryString["emrregno"]);
                }
                else
                    PopulateRegistrationInformation(txtRegistrationNo.Text);
            }

            lblRegistrationInfo2.Text = RegistrationInfoSumary.GetDocumentCheckListCountRemains(txtRegistrationNo.Text);

            hdnIsApproved.Value = false.ToString();
            hdnIsVoid.Value = false.ToString();
            hdnIsFromSOAP.Value = false.ToString();

            if (Helper.IsApotekOnlineIntegration)
            {
                btnCariPasienSep.Enabled = true;
                txtRefAsalSJP.Enabled = true;
                txtPoliRSP.Enabled = true;
                cboJnsRsp.Enabled = true;
                txtNoResep.Enabled = true;
                txtKdDokter.Enabled = true;
                cboIterasi.Enabled = true;
            }
            //}
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            // TODO: Terdapat codingan delete Prescription apakah dibolehkan ? (Handono 230318)
            var entity = new TransPrescription();
            if (entity.LoadByPrimaryKey(txtPrescriptionNo.Text))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (TransPrescriptionItems.Count == 0)
            {
                args.MessageText = "Detail transaction is not defined.";
                args.IsCancel = true;
                return;
            }

            if (pnlQueType.Visible && string.IsNullOrEmpty(cboQueueType.SelectedValue))
            {
                if (Request.QueryString["rt"] != "ipr" || (Request.QueryString["rt"] == "ipr" && AppSession.Parameter.IsPrescriptionQueueForInpatient))
                {
                    args.MessageText = "Queue type can not be empty.";
                    args.IsCancel = true;
                    return;
                }
            }

            if (AppSession.Parameter.IsUsingHumanResourcesModul & Request.QueryString["mode"] == "direct")
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
                                args.MessageText = "Employee ID required. Please contact HRD to define that required.";
                                args.IsCancel = true;
                                return;
                            case "2":
                                args.MessageText = "Employee ID required.";
                                args.IsCancel = true;
                                return;
                        }
                    }
                    else
                    {
                        var emp = new PersonalInfo();
                        if (!emp.LoadByPrimaryKey(Convert.ToInt32(cboEmployeeID.SelectedValue)))
                        {
                            args.MessageText = "Employee ID is not registered.";
                            args.IsCancel = true;
                            return;
                        }
                    }

                }
            }
            if (!string.IsNullOrEmpty(Request.QueryString["mode"]))
            {
                if (Request.QueryString["mode"] == "direct")
                {
                    var pat = new Patient();
                    if (!pat.LoadByPrimaryKey(cboPatientID.SelectedValue))
                    {
                        args.MessageText = "Invalid Patient Name. Patient ID does not exist.";
                        args.IsCancel = true;
                        return;
                    }
                }

                var g = new Guarantor();
                if (!g.LoadByPrimaryKey(cboGuarantorID.SelectedValue))
                {
                    args.MessageText = "Invalid Guarantor Name. Guarantor ID does not exist.";
                    args.IsCancel = true;
                    return;
                }
            }

            if (IsUddMode)
            {
                //filter pasien yg udd-nya sudah diproses per hari ini
                //var uddTx = new TransPrescriptionQuery("tx");
                //uddTx.Select(uddTx.PrescriptionNo);
                //uddTx.Where(uddTx.PrescriptionDate.Date() == txtPrescriptionDate.SelectedDate.Value.Date,
                //    uddTx.RegistrationNo == txtRegistrationNo.Text,
                //    uddTx.LocationID == cboLocationID.SelectedValue,
                //    uddTx.ParamedicID == cboParamedicID.SelectedValue,
                //    uddTx.IsUnitDosePrescription == true);

                //if (AppSession.Parameter.IsFilterPrescUddListOnlyWithValidTx)
                //    uddTx.Where(uddTx.IsVoid == false);
                //DataTable uddDtb = uddTx.LoadDataTable();
                //if (uddDtb.Rows.Count > 0)
                //{
                //    args.MessageText = "There is already a processed UDD Prescription for this patient with No. " + uddDtb.Rows[0]["PrescriptionNo"].ToString();
                //    args.IsCancel = true;
                //    return;
                //}

                var collItems = TransPrescriptionItems;
                var listItems = new List<string>();
                foreach (var item in collItems)
                {
                    var itemID = item.ItemID;
                    var itemName = item.ItemName;
                    if (!string.IsNullOrWhiteSpace(item.ItemInterventionID))
                    {
                        itemID = item.ItemInterventionID;
                        itemName = item.ItemInterventionName;
                    }

                    // Cek jika bukan bagian dari UDD Item maka skip
                    var uddItem = new UddItem();
                    uddItem.Query.Where(uddItem.Query.RegistrationNo == RegistrationNo, uddItem.Query.LocationID == cboLocationID.SelectedValue, uddItem.Query.ItemID == itemID);
                    uddItem.Query.es.Top = 1;
                    if (!uddItem.Query.Load()) continue; //Skip

                    // Cek di presciption hari ini
                    var tp = new TransPrescriptionQuery("tp");
                    var tpi = new TransPrescriptionItemQuery("tpi");
                    tp.InnerJoin(tpi).On(tp.PrescriptionNo == tpi.PrescriptionNo);

                    tp.Select(tp.PrescriptionNo);
                    tp.es.Top = 1;
                    tp.Where(tp.PrescriptionDate.Date() == txtPrescriptionDate.SelectedDate.Value.Date,
                        tp.RegistrationNo == RegistrationNo,
                        tp.LocationID == cboLocationID.SelectedValue,
                        tp.ParamedicID == cboParamedicID.SelectedValue,
                        tp.IsUnitDosePrescription == true,
                        tpi.ItemID == itemID,
                        tpi.SRConsumeMethod == item.SRConsumeMethod,
                        tpi.ConsumeQty == item.ConsumeQty,
                        tpi.SRConsumeUnit == item.SRConsumeUnit
                        );

                    if (AppSession.Parameter.IsFilterPrescUddListOnlyWithValidTx)
                        tp.Where(tp.IsVoid == false);

                    var txTp = new TransPrescription();
                    if (txTp.Load(tp) && !string.IsNullOrWhiteSpace(txTp.PrescriptionNo))
                    {
                        args.MessageText = string.Format("Therapy {0} {1} @{2} {3} is already a processed UDD Prescription for this patient with No. {4}", itemName, item.SRConsumeMethodName, item.ConsumeQty, item.SRConsumeUnit, txTp.PrescriptionNo);
                        args.IsCancel = true;
                        return;
                    }
                }

                //AB yg sudah lewat hari ke 7 tidak bisa diresepkan lagi kecuali sudah dibuatkan RASPRAJA 
                if (AppParameter.IsYes(AppParameter.ParameterItem.IsRasproEnable) && AppParameter.IsYes(AppParameter.ParameterItem.IsAntibioticRestriction))
                {
                    var antibioticMaxConsumeDay = AppParameter.GetParameterValue(AppParameter.ParameterItem.AntibioticMaxConsumeDay).ToInt();
                    foreach (var item in TransPrescriptionItems)
                    {
                        var ipm = new ItemProductMedic();
                        if (ipm.LoadByPrimaryKey(item.ItemID) && (ipm.IsAntibiotic ?? false))
                        {
                            var dayNo = MedicationReceiveUsed.ConsumedDay(RegistrationNo, item.ItemID, item.SRConsumeMethod, item.ConsumeQty, item.SRConsumeUnit);
                            if (dayNo >= antibioticMaxConsumeDay)
                            {
                                // Check Raspraja
                                var rri = new RegistrationRasproItem();
                                rri.Query.Where(rri.Query.RegistrationNo == RegistrationNo,
                                    rri.Query.ItemID == item.ItemID,
                                    rri.Query.SRConsumeMethod == item.SRConsumeMethod,
                                    rri.Query.ConsumeQty == item.ConsumeQty,
                                    rri.Query.SRConsumeUnit == item.SRConsumeUnit,
                                    rri.Query.StopDateTime.IsNull(),
                                    rri.Query.RasprajaSeqNo > 0);
                                rri.Query.es.Top = 1;
                                rri.Query.OrderBy(rri.Query.RasproSeqNo.Descending);
                                if (!rri.Query.Load())
                                {
                                    var drug = new Item();
                                    drug.LoadByPrimaryKey(item.ItemID);

                                    args.MessageText = String.Format("Item: {0} in day {1}, please contact Paramedic for create RASPRAJA Form first", item.ItemName, dayNo);
                                    args.IsCancel = true;
                                    return;
                                }
                            }
                        }
                    }
                }

            }
            if (pnlSplitBill.Visible && !chkIsSplitBill.Checked && !chkIsCash.Checked)
            {
                args.MessageText = "Split Bill or Cash must be on the checklist.";
                args.IsCancel = true;
                return;
            }

            var entity = new TransPrescription();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (TransPrescriptionItems.Count == 0)
            {
                args.MessageText = "Detail transaction is not defined.";
                args.IsCancel = true;
                return;
            }

            if (pnlQueType.Visible && string.IsNullOrEmpty(cboQueueType.SelectedValue))
            {
                if (Request.QueryString["rt"] != "ipr" || (Request.QueryString["rt"] == "ipr" && AppSession.Parameter.IsPrescriptionQueueForInpatient))
                {
                    args.MessageText = "Queue type can not be empty.";
                    args.IsCancel = true;
                    return;
                }
            }

            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);
            if (reg.IsHoldTransactionEntry ?? false)
            {
                args.MessageText = "Registration locked.";
                args.IsCancel = true;
                return;
            }

            if (reg.IsClosed ?? false)
            {
                args.MessageText = "Registration closed.";
                args.IsCancel = true;
                return;
            }

            bool isValid = true;
            string msg = string.Empty;
            if (AppSession.Parameter.IsUsingHumanResourcesModul & Request.QueryString["mode"] == "direct")
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
                                isValid = false;
                                msg = "Employee ID required. Please contact HRD to define that required.";
                                break;
                            case "2":
                                isValid = false;
                                msg = "Employee ID required.";
                                break;
                        }
                    }
                    else
                    {
                        var emp = new PersonalInfo();
                        if (!emp.LoadByPrimaryKey(Convert.ToInt32(cboEmployeeID.SelectedValue)))
                        {
                            isValid = false;
                            msg = "Employee ID is not registered.";
                        }
                    }

                }
            }
            if (Request.QueryString["mode"] == "direct")
            {
                var pat = new Patient();
                if (!pat.LoadByPrimaryKey(cboPatientID.SelectedValue))
                {
                    isValid = false;
                    msg = "Invalid Patient Name. Patient ID does not exist.";
                }
            }
            if (pnlSplitBill.Visible && !chkIsSplitBill.Checked && !chkIsCash.Checked)
            {
                isValid = false;
                msg = "Split Bill or Cash must be on the checklist.";
            }
            if (!isValid)
            {
                args.MessageText = msg;
                args.IsCancel = true;
                return;
            }

            var entity = new TransPrescription();
            if (entity.LoadByPrimaryKey(txtPrescriptionNo.Text))
            {
                // Kurangi dulu MedicationReceive (UDD) untuk resep rawat inap & Emergency untuk tipe Home Prescription
                // dan tambahkan lagi setelah selesai proses save prescription
                if (entity.IsForTakeItHome ?? false)
                    AddReduceMedicationQty(entity.PrescriptionNo, reg.RegistrationNo, reg.SRRegistrationType, false);

                SetEntityValue(entity);
                SaveEntity(entity);

                // Tambahkan lagi qty MedicationReceive (UDD) untuk resep rawat inap & Emergency untuk tipe Home Prescription
                if (entity.IsForTakeItHome ?? false)
                    AddReduceMedicationQty(entity.PrescriptionNo, reg.RegistrationNo, reg.SRRegistrationType, true);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
        }

        private static void AddReduceMedicationQty(string prescriptionNo, string registrationNo, string regType, bool isAdd)
        {
            if (regType == AppConstant.RegistrationType.InPatient ||
                regType == AppConstant.RegistrationType.EmergencyPatient)
            {
                MedicationReceive.ImportFromPrescriptionBaseOnTherapy(prescriptionNo,
                    registrationNo,
                    null, !isAdd);
            }
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
            auditLogFilter.PrimaryKeyData = string.Format("PrescriptionNo='{0}'", txtPrescriptionNo.Text.Trim());
            auditLogFilter.TableName = "TransPrescription";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            switch (programID)
            {
                case AppConstant.Report.RSSA_PrescriptionSlip:
                    printJobParameters.AddNew("p_HealthcareID", AppSession.Parameter.HealthcareID);
                    printJobParameters.AddNew("p_PrescriptionNo", txtPrescriptionNo.Text);
                    break;
                case AppConstant.Report.RSSA_PrescriptionReceiptSlip:
                    printJobParameters.AddNew("p_PrescriptionNo", txtPrescriptionNo.Text);
                    printJobParameters.AddNew("p_Label", string.Empty);
                    printJobParameters.AddNew("p_UserID", AppSession.UserLogin.UserID);

                    var tp = new TransPrescription();
                    if (tp.LoadByPrimaryKey(txtPrescriptionNo.Text))
                    {
                        tp.IsPrinted = true;
                        tp.Save();
                    }

                    break;
                case AppConstant.Report.PrescriptionReceiptHandoverSlip:
                    printJobParameters.AddNew("p_PrescriptionNo", txtPrescriptionNo.Text);
                    printJobParameters.AddNew("p_Label", string.Empty);
                    printJobParameters.AddNew("p_UserID", AppSession.UserLogin.UserID);
                    break;
                case AppConstant.Report.PrescriptionEtiket:
                    printJobParameters.AddNew("p_PrescriptionNo", txtPrescriptionNo.Text);
                    printJobParameters.AddNew("p_SequenceNo", string.Empty);
                    printJobParameters.AddNew("p_Label", "1");

                    if (!(OnGetStatusMenuApproval() ?? true))
                    {

                        //var tp2 = new TransPrescription();
                        //if (tp2.LoadByPrimaryKey(txtPrescriptionNo.Text))
                        //{
                        //    tp2.IsProceedByPharmacist = true;
                        //    tp2.Save();
                        //}

                        // Dipindah ke UpdatePrescriptionStatus (Handono 2206)
                        UpdatePrescriptionStatus(txtPrescriptionNo.Text, "inprogress");
                    }

                    break;
                case AppConstant.Report.PrescriptionEtiketLr:
                    printJobParameters.AddNew("p_PrescriptionNo", txtPrescriptionNo.Text);
                    printJobParameters.AddNew("p_SequenceNo", string.Empty);
                    printJobParameters.AddNew("p_Label", "2");

                    if (!(OnGetStatusMenuApproval() ?? true))
                    {
                        //var tp2 = new TransPrescription();
                        //if (tp2.LoadByPrimaryKey(txtPrescriptionNo.Text))
                        //{
                        //    tp2.IsProceedByPharmacist = true;
                        //    tp2.Save();
                        //}

                        // Dipindah ke UpdatePrescriptionStatus (Handono 2206)
                        UpdatePrescriptionStatus(txtPrescriptionNo.Text, "inprogress");

                    }

                    break;
                case AppConstant.Report.PrescriptionQueSlip:
                    printJobParameters.AddNew("p_PrescriptionNo", txtPrescriptionNo.Text);
                    printJobParameters.AddNew("p_Label", string.Empty);
                    break;
                case AppConstant.Report.PrescriptionOrderSlip:
                    printJobParameters.AddNew("p_HealthcareID", AppSession.Parameter.HealthcareID);
                    printJobParameters.AddNew("p_PrescriptionNo", txtPrescriptionNo.Text);

                    //var tp3 = new TransPrescription();
                    //if (tp3.LoadByPrimaryKey(txtPrescriptionNo.Text))
                    //{
                    //    tp3.IsPrinted = true;
                    //    tp3.Save();
                    //}

                    break;
                default:
                    printJobParameters.AddNew("p_PrescriptionNo", txtPrescriptionNo.Text);
                    printJobParameters.AddNew("p_SequenceNo", string.Empty);
                    printJobParameters.AddNew("p_Label", string.Empty);
                    printJobParameters.AddNew("p_UserID", AppSession.UserLogin.UserID);
                    break;
            }
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !(hdnIsApproved.Value.ToBoolean());
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(hdnIsVoid.Value.ToBoolean());
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemTransPrescriptionItem(oldVal, newVal);
            if (!string.IsNullOrEmpty(Request.QueryString["mode"]) && Request.QueryString["mode"] == "pos")
                trPayment.Visible = hdnIsApproved.Value.ToBoolean();
            else
                trPayment.Visible = false;

            btnPrescHighAlert.Enabled = _isContainHighAlert && newVal == AppEnum.DataMode.Read;
            btnPrescReview.Enabled = newVal == AppEnum.DataMode.Read;
            btnDrugReview.Enabled = newVal == AppEnum.DataMode.Read;
            btnFollowUp.Enabled = newVal == AppEnum.DataMode.Read;
            btnPrescProgress.Enabled = newVal == AppEnum.DataMode.Read;
            btnPrescEducation.Enabled = newVal == AppEnum.DataMode.Read;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new TransPrescription();
            if (parameters.Length > 0)
            {
                var prescriptionNo = (String)parameters[0];
                if (Is23DaysMode)
                {
                    var xq = new TransPrescriptionQuery();
                    xq.Where(xq.ReferenceNo == prescriptionNo, xq.IsPrescriptionReturn == false, xq.Is23Days == true);
                    var x = new TransPrescription();
                    x.Load(xq);
                    prescriptionNo = x.PrescriptionNo;
                }

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(prescriptionNo);
            }
            else
                entity.LoadByPrimaryKey(txtPrescriptionNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var pres = (TransPrescription)entity;

            txtPrescriptionNo.Text = pres.PrescriptionNo;
            txtPrescriptionDate.SelectedDate = pres.PrescriptionDate;
            txtExecutionDate.SelectedDate = pres.ExecutionDate ?? pres.PrescriptionDate;

            txtRegistrationNo.Text = pres.RegistrationNo;

            cboServiceUnitID.SelectedValue = Request.QueryString["rt"] == "opr"
                                                 ? (pres.ServiceUnitID ??
                                                    AppSession.Parameter.ServiceUnitPharmacyIdOpr)
                                                 : (pres.ServiceUnitID ??
                                                    AppSession.Parameter.ServiceUnitPharmacyID);

            if (!string.IsNullOrEmpty(Request.QueryString["unit"]))
            {
                cboServiceUnitID.SelectedValue = Request.QueryString["unit"];
                if (!string.IsNullOrEmpty(Request.QueryString["loc"]))
                {
                    cboLocationID.SelectedValue = Request.QueryString["loc"];
                }
            }

            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, cboServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(pres.LocationID))
                    cboLocationID.SelectedValue = pres.LocationID;
                else cboLocationID.SelectedIndex = 1;
            }

            chkIsDirect.Checked = pres.IsDirect ?? false;

            if (DataModeCurrent != AppEnum.DataMode.New)
            {
                PopulateRegistrationInformation(txtRegistrationNo.Text);
                btnNewPatient.Visible = false;
                txtSalutation.Visible = true;
                txtPatientName.Visible = true;
                txtGender.Visible = true;
                cboPatientID.Visible = false;
            }

            if (!string.IsNullOrEmpty(pres.SRFloor))
            {
                cboSRFloor.SelectedValue = pres.SRFloor;
            }
            if (!string.IsNullOrEmpty(pres.AdditionalNote))
            {
                txtAdditionalNote.Text = pres.AdditionalNote;
            }
            if (!string.IsNullOrEmpty(pres.NoTelp))
            {
                txtNoTelp.Text = pres.NoTelp;
            }
            if (!string.IsNullOrEmpty(pres.FullAddress))
            {
                txtFullAddress.Text = pres.FullAddress;
            }
            if (!string.IsNullOrEmpty(pres.FromServiceUnitID))
            {
                txtServiceUnitID.Text = pres.FromServiceUnitID;
                var unit = new ServiceUnit();
                if (unit.LoadByPrimaryKey(txtServiceUnitID.Text))
                    lblServiceUnitName.Text = unit.ServiceUnitName;
            }
            if (!string.IsNullOrEmpty(pres.FromRoomID))
            {
                txtRoomID.Text = pres.FromRoomID;
                var room = new ServiceRoom();
                if (room.LoadByPrimaryKey(txtRoomID.Text))
                    lblRoomName.Text = room.RoomName;
            }
            if (!string.IsNullOrEmpty(pres.FromBedID))
            {
                txtBedID.Text = pres.FromBedID;
            }
            if (!string.IsNullOrEmpty(pres.ClassID))
            {
                txtClassID.Text = pres.ClassID;
                var cls = new Class();
                if (cls.LoadByPrimaryKey(txtClassID.Text))
                    lblClassName.Text = cls.ClassName;
            }


            if (!string.IsNullOrEmpty(pres.ParamedicID))
                cboParamedicID.SelectedValue = pres.ParamedicID;
            //if (Request.QueryString["md"] != "new")
            //    cboParamedicID.SelectedValue = transPrescription.ParamedicID;

            // Ujicoba ganti Hidden Field krn ada kasus di pc tertentu setelah approv statusnya Approved tapi di DB tidak (Handono)
            //ViewState["IsApproved"] = pres.IsApproval ?? false;
            //ViewState["IsVoid"] = pres.IsVoid ?? false;
            //ViewState["IsFromSOAP"] = pres.IsFromSOAP ?? false;
            hdnIsApproved.Value = (pres.IsApproval ?? false).ToString();
            hdnIsVoid.Value = (pres.IsVoid ?? false).ToString();
            hdnIsFromSOAP.Value = (pres.IsFromSOAP ?? false).ToString();

            txtPrescriptionText.Text = pres.str.Note;

            //Display Data Detail
            PopulateTransPrescriptionItemGrid();

            pnlRoom.Visible = !chkIsDirect.Checked;
            pnlBedID.Visible = !chkIsDirect.Checked;
            pnlClassID.Visible = !chkIsDirect.Checked;

            decimal tpatient = 0;
            decimal tguarantor = 0;

            var cccoll = new CostCalculationCollection();
            cccoll.Query.Where(cccoll.Query.RegistrationNo == txtRegistrationNo.Text &&
                     cccoll.Query.TransactionNo == txtPrescriptionNo.Text);
            cccoll.LoadAll();
            foreach (var item in cccoll)
            {
                tpatient += item.PatientAmount ?? 0;
                tguarantor += item.GuarantorAmount ?? 0;
            }

            txtPatientAmount.Value = Convert.ToDouble(tpatient);
            txtGuarantorAmount.Value = Convert.ToDouble(tguarantor);

            //chkUnitDose.Checked = pres.IsUnitDosePrescription ?? false;
            chkIs23Days.Checked = pres.Is23Days ?? false;
            chkIsSplitBill.Checked = pres.IsSplitBill ?? false;
            chkIsCash.Checked = pres.IsCash ?? false;
            txtQtyR.Value = pres.QtyR ?? 0;

            if (!string.IsNullOrEmpty(pres.PrescriptionNo))
                PopulateReviewInfo(pres);

            LoadQueType();
            var itm = cboQueueType.FindItemByValue(pres.SRKioskQueueType);
            if (itm != null)
            {
                cboQueueType.SelectedValue = pres.SRKioskQueueType;
            }
            else
            {
                cboQueueType.SelectedValue = "";
            }
            txtQueueNo.Text = pres.KioskQueueNo;

            if ((pres.IsDirect ?? false) && (Request.QueryString["rt"] == "ipr"))
            {
                pnlSplitBill.Visible = AppSession.Parameter.IsPrescriptionSplitBillActived;
            }

            hdnServiceUnitID.Value = cboServiceUnitID.SelectedValue;
            hdnLocationID.Value = cboLocationID.SelectedValue;

            if (!Is23DaysMode)
            {
                var xq = new TransPrescriptionQuery();
                xq.Where(xq.ReferenceNo == txtPrescriptionNo.Text, xq.IsApproval == false, xq.IsVoid == false, xq.IsPrescriptionReturn == false, xq.Is23Days == true);
                DataTable dbx = xq.LoadDataTable();
                trGoTo23DaysPrescription.Visible = dbx.Rows.Count > 0;
            }
            else
                trGoTo23DaysPrescription.Visible = false;
        }

        private void PopulateReviewInfo(TransPrescription pres)
        {
            // Prescription Review
            if (!string.IsNullOrEmpty(pres.ReviewByUserID))
            {
                var prev = new TransPrescriptionReview();
                prev.Query.Where(prev.Query.PrescriptionNo == pres.PrescriptionNo);
                prev.Query.es.Top = 1;
                prev.Query.OrderBy(prev.Query.LastUpdateByUserID.Ascending);
                if (prev.Query.Load())
                {
                    if (!string.IsNullOrEmpty(prev.PrescriptionReviewByUserID))
                        lblReviewByUserName.Text = string.Format("{0} @{1}", AppUser.GetUserName(prev.PrescriptionReviewByUserID), prev.PrescriptionReviewDateTime.Value.ToString(AppConstant.DisplayFormat.DateTimeSecond));
                    if (!string.IsNullOrEmpty(prev.DrugReviewByUserID))
                        lblDrugReviewByUserName.Text = string.Format("{0} @{1}", AppUser.GetUserName(prev.DrugReviewByUserID), prev.DrugReviewDateTime.Value.ToString(AppConstant.DisplayFormat.DateTimeSecond));
                    if (!string.IsNullOrEmpty(prev.NoteByUserID))
                        lblFollowUpByUserName.Text = string.Format("{0} @{1}", AppUser.GetUserName(prev.NoteByUserID), prev.NoteDateTime.Value.ToString(AppConstant.DisplayFormat.DateTimeSecond));
                }
            }

            // Progress
            var progress = string.Empty;
            var stdi = new AppStandardReferenceItemQuery("stdi");
            var prgs = new TransPrescriptionProgressQuery("a");
            stdi.InnerJoin(prgs)
                .On(stdi.ItemID == prgs.SRPrescriptionProgress && stdi.StandardReferenceID == "PrescriptionProgress");
            stdi.Select(stdi.ItemName,
                "<CONVERT(BIT,CASE WHEN a.SRPrescriptionProgress IS NULL THEN 0 ELSE 1 END) as IsExist>");
            stdi.OrderBy(stdi.LineNumber.Ascending);
            stdi.Where(prgs.PrescriptionNo == pres.PrescriptionNo);

            var dtbProgress = stdi.LoadDataTable();
            foreach (DataRow row in dtbProgress.Rows)
            {
                if (true.Equals(row["IsExist"]))
                    progress = string.Concat(progress, ", ", row["ItemName"]);
            }
            if (!string.IsNullOrEmpty(progress))
            {
                progress = progress.Substring(2);
            }
            lblProgress.Text = progress;

            // High Alert
            var halt = new TransPrescriptionHighAlert();
            halt.Query.Where(halt.Query.PrescriptionNo == pres.PrescriptionNo);
            halt.Query.es.Top = 1;
            halt.Query.OrderBy(halt.Query.LastUpdateByUserID.Ascending);
            if (halt.Query.Load())
                lblHighAlert.Text = string.Format("{0} @{1}", AppUser.GetUserName(halt.LastUpdateByUserID), halt.LastUpdateDateTime.Value.ToString(AppConstant.DisplayFormat.DateTimeSecond));


            // Education
            if (pres.PatientEducationSeqNo > 0)
            {
                var edu = new PatientEducation();
                if (edu.LoadByPrimaryKey(pres.RegistrationNo, pres.PatientEducationSeqNo ?? 0))
                {
                    lblEducationByUserName.Text = string.Format("{0} @{1}", AppUser.GetUserName(edu.EducationByUserID),
                        edu.EducationDateTime.Value.ToString(AppConstant.DisplayFormat.DateTime));
                }

            }
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new TransPrescription();
            if (!entity.LoadByPrimaryKey(txtPrescriptionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (string.IsNullOrEmpty(entity.LocationID))
            {
                args.MessageText = "Location required.";
                args.IsCancel = true;
                return;
            }
            if (entity.IsApproval ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }
            //if (entity.IsUnitDosePrescription ?? false)
            //{
            //    args.MessageText = "Unit dose prescription is no need to approve.";
            //    args.IsCancel = true;
            //    return;
            //}

            var reg = new Registration();
            reg.LoadByPrimaryKey(entity.RegistrationNo);
            if (reg.IsHoldTransactionEntry ?? false)
            {
                args.MessageText = "Transaction is locked.";
                args.IsCancel = true;
                return;
            }
            if (reg.IsClosed ?? false)
            {
                args.MessageText = "Registration has closed.";
                args.IsCancel = true;
                return;
            }

            var mb = new MergeBilling();
            if (mb.LoadByPrimaryKey(reg.RegistrationNo) && !string.IsNullOrEmpty(mb.FromRegistrationNo))
            {
                var fromReg = new Registration();
                if (fromReg.LoadByPrimaryKey(mb.FromRegistrationNo))
                {
                    if (fromReg.IsHoldTransactionEntry ?? false)
                    {
                        args.MessageText = "Parent Transaction is locked.";
                        args.IsCancel = true;
                        return;
                    }

                    if (fromReg.IsClosed ?? false)
                    {
                        args.MessageText = "Parent Registration has closed.";
                        args.IsCancel = true;
                        return;
                    }
                }
            }

            //if ((r.IsHoldTransactionEntry ?? false) || (r.IsClosed ?? false))
            //{
            //    var mb = new MergeBilling();
            //    if (mb.LoadByPrimaryKey(r.RegistrationNo) && !string.IsNullOrEmpty(mb.FromRegistrationNo))
            //    {
            //        var r2 = new Registration();
            //        if (r2.LoadByPrimaryKey(mb.FromRegistrationNo))
            //        {
            //            if (r2.IsHoldTransactionEntry ?? false)
            //            {
            //                args.MessageText = "Registration locked.";
            //                args.IsCancel = true;
            //                return;
            //            }

            //            if (r2.IsClosed ?? false)
            //            {
            //                args.MessageText = "Registration closed.";
            //                args.IsCancel = true;
            //                return;
            //            }
            //        }
            //        else
            //        {
            //            if (r.IsHoldTransactionEntry ?? false)
            //            {
            //                args.MessageText = "Registration locked.";
            //                args.IsCancel = true;
            //                return;
            //            }

            //            if (r.IsClosed ?? false)
            //            {
            //                args.MessageText = "Registration closed.";
            //                args.IsCancel = true;
            //                return;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (r.IsHoldTransactionEntry ?? false)
            //        {
            //            args.MessageText = "Registration locked.";
            //            args.IsCancel = true;
            //            return;
            //        }

            //        if (r.IsClosed ?? false)
            //        {
            //            args.MessageText = "Registration closed.";
            //            args.IsCancel = true;
            //            return;
            //        }
            //    }
            //}

            if (pnlQueType.Visible && string.IsNullOrEmpty(cboQueueType.SelectedValue))
            {
                if (Request.QueryString["rt"] != "ipr" || (Request.QueryString["rt"] == "ipr" && AppSession.Parameter.IsPrescriptionQueueForInpatient))
                {
                    args.MessageText = "Queue type can not be empty.";
                    args.IsCancel = true;
                    return;
                }
            }

            //if ((r.SRRegistrationType == AppConstant.RegistrationType.OutPatient || r.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient) &&
            //    !string.IsNullOrEmpty(Request.QueryString["mode"]) &&
            //    AppSession.Parameter.IsValidateDiagnosisOnRealizationOrderOp &&
            //    string.IsNullOrEmpty(txtDiagnose.Text))
            //{
            //    args.MessageText = "Diagnosis required.";
            //    args.IsCancel = true;
            //    return;
            //}

            ////if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH" && entity.ServiceUnitID == AppSession.Parameter.ServiceUnitPharmacyIdOpr)
            //if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH" && pnlAdditionalOpr.Visible)
            //{
            //    var patient = new Patient();
            //    patient.LoadByPrimaryKey(r.PatientID);
            //    if (txtNoTelp.Text == (patient.PhoneNo.Trim() + " " + patient.MobilePhoneNo).Trim())
            //    {
            //        args.MessageText = "Validate phone number needed.";
            //        args.IsCancel = true;
            //        return;
            //    }
            //}

            if (Helper.GuarantorBpjsCasemix.Contains(cboGuarantorID.SelectedValue))
            {
                reg = new Registration();
                reg.LoadByPrimaryKey(txtRegistrationNo.Text);
                if (AppSession.Parameter.CasemixValidationRegistrationType.Contains(reg.SRRegistrationType))
                {
                    if (TransPrescriptionItems.Any(t => !(t.IsVoid ?? false) && !(t.IsCasemixApproved ?? false) && t.ResultQty > 0))
                    {
                        var tpi = TransPrescriptionItems.Where(t => !(t.IsVoid ?? false) && !(t.IsCasemixApproved ?? false) && t.ResultQty > 0).Take(1).SingleOrDefault();
                        args.MessageText = "Some item(s) need validation by Casemix : " + tpi.GetColumn("refToItem_ItemName").ToString();
                        args.IsCancel = true;
                        return;
                    }
                }
            }

            try
            {
                ApprovPrescription(entity, args);

                // Tambah MedicationReceive (UDD) untuk resep rawat inap & Emergency untuk tipe selain Home Prescription
                // Tipe Home Prescription prosesnya sudah langsung pada saat Save
                if (!(entity.IsForTakeItHome ?? false))
                {
                    entity = new TransPrescription();
                    if (entity.LoadByPrimaryKey(txtPrescriptionNo.Text) &&
                        entity.IsApproval == true) // Approve berhasil disave
                    {
                        AddReduceMedicationQty(entity.PrescriptionNo, reg.RegistrationNo, reg.SRRegistrationType, true);
                    }
                }

                if (!string.IsNullOrEmpty(Request.QueryString["mode"]) && Request.QueryString["mode"] == "pos")
                    trPayment.Visible = true;
                else
                    trPayment.Visible = false;

                if (Helper.IsApotekOnlineIntegration)
                {
                    // buat nampung item gagal
                    var apolErrors = new System.Collections.Generic.List<string>();

                    try
                    {
                        var hdr = new TransPrescription();
                        if (!hdr.LoadByPrimaryKey(txtPrescriptionNo.Text) || !(hdr.IsApproval ?? false))
                            goto END_APOL;

                        // ambil header APOL
                        var apol = new BpjsApol();
                        apol.Query.Where(apol.Query.NosepKunjungan == txtBpjsSepNo.Text);
                        if (!apol.Query.Load() || string.IsNullOrWhiteSpace(Convert.ToString(apol.NOAPOTIK)) || string.IsNullOrWhiteSpace(Convert.ToString(apol.NORESEP)))
                        {
                            ShowInformationHeader("APOL: NOSJP/NORESEP belum tersedia. Kirim ditunda.");
                            goto END_APOL;
                        }
                        var nosjp = Convert.ToString(apol.NOAPOTIK).Trim();
                        var noresep = Convert.ToString(apol.NORESEP).Trim();

                        // kirim per item
                        foreach (var tpi in TransPrescriptionItems.Where(z => !(z.IsVoid ?? false)))
                        {
                            int ToInt(object o)
                            {
                                decimal d;
                                return decimal.TryParse(Convert.ToString(o),
                                        System.Globalization.NumberStyles.Any,
                                        System.Globalization.CultureInfo.InvariantCulture,
                                        out d)
                                    ? (int)Math.Round(d, 0, MidpointRounding.AwayFromZero)
                                    : 0;
                            }
                            if (ToInt(tpi.TakenQty) <= 0) continue; // skip qty 0

                            // cek sudah sukses sebelumnya
                            var detChk = new BpjsApolDetail();
                            detChk.Query.Where(detChk.Query.PrescriptionNo == hdr.PrescriptionNo, detChk.Query.SequenceNo == tpi.SequenceNo);
                            if (detChk.Query.Load() && (detChk.MetadataCode ?? "") == "200") continue;

                            // cek mapping
                            var map = new ItemBridging();
                            map.Query.Where(
                                map.Query.ItemID == (string.IsNullOrWhiteSpace(tpi.ItemInterventionID) ? tpi.ItemID : tpi.ItemInterventionID),
                                map.Query.SRBridgingType == AppEnum.BridgingType.APOTEKONLINE.ToString()
                            );
                            if (!map.Query.Load())
                            {
                                var d0 = new BpjsApolDetail();
                                d0.Query.Where(d0.Query.PrescriptionNo == hdr.PrescriptionNo, d0.Query.SequenceNo == tpi.SequenceNo);
                                var ex0 = d0.Query.Load();
                                if (!ex0) d0.AddNew();
                                d0.PrescriptionNo = hdr.PrescriptionNo;
                                d0.SequenceNo = tpi.SequenceNo;
                                d0.ParentNo = tpi.ParentNo;
                                d0.BpjsApolID = apol.ID;
                                d0.NOSJP = nosjp;
                                d0.NORESEP = noresep;
                                d0.MetadataCode = "PENDING-NOMAP";
                                d0.MetadataMessage = $"APOL: Mapping belum ada untuk ItemID '{tpi.ItemID}'.";
                                d0.LastUpdateDateTime = DateTime.Now;
                                d0.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                d0.Save();

                                apolErrors.Add($"Seq {tpi.SequenceNo}: mapping belum ada");
                                continue;
                            }

                            int signa2 = 0;
                            var cm = new ConsumeMethod();
                            cm.Query.Where(cm.Query.SRConsumeMethod == tpi.SRConsumeMethod);
                            if (cm.Query.Load()) signa2 = cm.IterationQty ?? 0;

                            // jns robit racikan: "R.0{NN}"
                            var seqStr = (Convert.ToString(tpi.SequenceNo) ?? "").Trim();
                            var m = System.Text.RegularExpressions.Regex.Match(seqStr, @"\d{1,2}(?!.*\d)");
                            string jnsrobt;
                            if (tpi.IsCompound == true)
                            {
                                var seq = Convert.ToString(tpi.SequenceNo) ?? "";
                                var lastDigit = seq.Reverse().FirstOrDefault(char.IsDigit);
                                jnsrobt = (lastDigit != default(char)) ? ("R.0" + lastDigit) : "R.01"; // fallback aman
                            }
                            else
                            {
                                jnsrobt = "N";
                            }

                            //payload prep
                            var kdobt = map.BridgingID; var nmobat = map.BridgingName;
                            var signa1 = ToInt(tpi.ConsumeQty);
                            var permintaan = ToInt(tpi.PrescriptionQty); // racikan
                            var jmlobt = ToInt(tpi.TakenQty);
                            var jho = ToInt(tpi.DaysOfUsage);
                            var catatan = Convert.ToString(tpi.Notes);

                            var svc = new Temiang.Avicenna.Common.BPJS.Apotek.Service();
                            string metaCode = null, metaMsg = null;

                            try
                            {
                                if (tpi.IsCompound == true)
                                {
                                    var req = new Temiang.Avicenna.Common.BPJS.Apotek.Obat.Racikan.Request.Root
                                    {
                                        Nosjp = nosjp,
                                        Noresep = noresep,
                                        Jnsrobt = jnsrobt,
                                        Kdobt = kdobt,
                                        Nmobat = nmobat,
                                        Signa1obt = signa1.ToString(),
                                        Signa2obt = signa2.ToString(),
                                        Permintaan = permintaan.ToString(),
                                        Jmlobt = jmlobt.ToString(),
                                        Jho = jho.ToString(),
                                        Catkhsobt = catatan
                                    };
                                    Temiang.Avicenna.Common.BPJS.Apotek.Obat.Racikan.Response resp = null;
                                    Exception last = null;
                                    for (int i = 0; i < 2; i++)
                                    {
                                        try { resp = svc.InsertRacikan(req); last = null; break; }
                                        catch (System.Net.WebException ex) { last = ex; System.Threading.Thread.Sleep(400 * (i + 1)); }
                                    }
                                    if (last != null) throw last;
                                    metaCode = (resp?.Code ?? "").Trim();
                                    metaMsg = (resp?.Message ?? "").Trim();
                                }
                                else
                                {
                                    var req = new Temiang.Avicenna.Common.BPJS.Apotek.Obat.NonRacikan.Request.Root
                                    {
                                        Nosjp = nosjp,
                                        Noresep = noresep,
                                        Kdobt = kdobt,
                                        Nmobat = nmobat,
                                        Signa1obt = signa1.ToString(),
                                        Signa2obt = signa2.ToString(),
                                        Jmlobt = jmlobt.ToString(),
                                        Jho = jho.ToString(),
                                        Catkhsobt = catatan
                                    };
                                    Temiang.Avicenna.Common.BPJS.Apotek.Obat.NonRacikan.Response resp = null;
                                    Exception last = null;
                                    for (int i = 0; i < 2; i++)
                                    {
                                        try { resp = svc.InsertNonRacikan(req); last = null; break; }
                                        catch (System.Net.WebException ex) { last = ex; System.Threading.Thread.Sleep(400 * (i + 1)); }
                                    }
                                    if (last != null) throw last;
                                    metaCode = (resp?.Code ?? "").Trim();
                                    metaMsg = (resp?.Message ?? "").Trim();
                                }
                            }
                            catch (System.Net.WebException wex)
                            {
                                var http = wex.Response as System.Net.HttpWebResponse;
                                var sc = http != null ? (int)http.StatusCode : 0;
                                metaCode = sc.ToString();
                                metaMsg = sc == 504 ? "Gateway Timeout (504). Item tersimpan" : $"HTTP Error {sc}. Item tersimpan";
                            }
                            catch (Exception ex)
                            {
                                metaCode = "Catch";
                                metaMsg = ex.Message;
                            }

                            var det = new BpjsApolDetail();
                            det.Query.Where(det.Query.PrescriptionNo == hdr.PrescriptionNo, det.Query.SequenceNo == tpi.SequenceNo);
                            var exists = det.Query.Load();
                            if (!exists) det.AddNew();

                            det.PrescriptionNo = hdr.PrescriptionNo;
                            det.SequenceNo = tpi.SequenceNo;
                            det.ParentNo = tpi.ParentNo;
                            det.NOSJP = nosjp;
                            det.NORESEP = noresep;
                            det.KDOBT = kdobt;
                            det.NMOBAT = nmobat;
                            det.SIGNA1OBT = signa1;
                            det.SIGNA2OBT = signa2;
                            det.JMLOBT = jmlobt;
                            det.JHO = jho;
                            det.CATKHSOBT = catatan;
                            if (tpi.IsCompound == true) { det.JNSROBT = jnsrobt; det.PERMINTAAN = permintaan; }

                            det.MetadataCode = metaCode ?? "";
                            det.MetadataMessage = metaMsg ?? "";
                            det.LastUpdateDateTime = DateTime.Now;
                            det.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            det.Save();

                            if (!string.Equals(metaCode, "200", StringComparison.OrdinalIgnoreCase))
                            {
                                var shortMsg = string.IsNullOrWhiteSpace(metaMsg) ? "Tidak diketahui" : metaMsg;
                                apolErrors.Add($"Seq {tpi.SequenceNo} ({kdobt}): {shortMsg}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        this.LogError(ex);
                        ShowInformationHeader($"APOL: {ex.Message}");
                    }

                END_APOL:;

                    if (apolErrors.Count > 0)
                    {
                        var toShow = string.Join("\\n", apolErrors.Take(6));
                        ScriptManager.RegisterStartupScript(
                            this,
                            this.GetType(),
                            "apolErr",
                            $"alert('Ada item APOL yang gagal / bukan 200:\\n{toShow}\\nCek log / tabel detail untuk lengkapnya.');",
                            true
                        );

                        ShowInformationHeader("Sebagian item APOL gagal dikirim. Silakan cek BpjsApolDetail.");
                    }
                }
            }
            catch (Exception e)
            {
                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "",
                    UrlAddress = "APROV-APOL",
                    Params = "",
                    Response = e.Message,
                    Totalms = 0
                };
                log.Save();
                throw e;
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var reason = args.ReasonText;

            var entity = new TransPrescription();
            if (!entity.LoadByPrimaryKey(txtPrescriptionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            if (entity.IsApproval == false)
            {
                args.MessageText = AppConstant.Message.RecordHasNotApproved;
                args.IsCancel = true;
                return;
            }

            var prescReturn = new TransPrescriptionCollection();
            prescReturn.Query.Where(prescReturn.Query.ReferenceNo == txtPrescriptionNo.Text,
                                    prescReturn.Query.IsPrescriptionReturn == true, prescReturn.Query.IsVoid == false);
            prescReturn.LoadAll();
            if (prescReturn.Count > 0)
            {
                args.MessageText = "This transaction has prescription return.";
                args.IsCancel = true;
                return;
            }

            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH" && entity.ApprovalDateTime.Value.AddMinutes(1) > (new DateTime()).NowAtSqlServer())
            {
                args.MessageText = "This data can not be unapproval before passing 1 minute.";
                args.IsCancel = true;
                return;
            }

            if (entity.ApprovalDateTime.Value.Date != (new DateTime()).NowAtSqlServer().Date && !this.IsPowerUser)
            {
                args.MessageText = "Transaction is expired. Please contact your supervision.";
                args.IsCancel = true;
                return;
            }

            //if (entity.IsUnitDosePrescription ?? false)
            //{
            //    args.MessageText = "Use prescription return for unit dose prescription.";
            //    args.IsCancel = true;
            //    return;
            //}

            var reg = new Registration();
            reg.LoadByPrimaryKey(entity.RegistrationNo);
            if (reg.IsHoldTransactionEntry ?? false)
            {
                args.MessageText = "Registration locked.";
                args.IsCancel = true;
                return;
            }

            if (reg.IsClosed ?? false)
            {
                args.MessageText = "Registration closed.";
                args.IsCancel = true;
                return;
            }

            var pycoll = new TransPaymentItemOrderCollection();
            pycoll.Query.Where(
                pycoll.Query.TransactionNo == entity.PrescriptionNo,
                pycoll.Query.IsPaymentProceed == true,
                pycoll.Query.IsPaymentReturned == false
                );
            pycoll.LoadAll();

            if (pycoll.Count > 0)
            {
                args.MessageText = "Prescreption already paid. Un-Approval is not allowed.";
                args.IsCancel = true;
                return;
            }

            var ibcoll = new CostCalculationCollection();
            ibcoll.Query.Where(
                ibcoll.Query.TransactionNo == entity.PrescriptionNo,
                ibcoll.Query.IntermBillNo.IsNotNull()
                );
            ibcoll.LoadAll();

            if (ibcoll.Count > 0)
            {
                args.MessageText = "Prescreption already on interm bill. Un-Approval is not allowed.";
                args.IsCancel = true;
                return;
            }

            var loc = new Location();
            if (loc.LoadByPrimaryKey(entity.LocationID) && loc.IsHoldForTransaction == true)
            {
                args.MessageText = "Location: " + loc.LocationName + " in Hold For Transaction status. Transaction is not allowed.";
                args.IsCancel = true;
                return;
            }

            SetUnApproval(true, args, reason);
            trPayment.Visible = false;

            // Kurangi MedicationReceive (UDD) untuk resep rawat inap & Emergency untuk tipe selain Home Prescription
            // Tipe Home Prescription prosesnya sudah langsung pada saat Save
            if (!(entity.IsForTakeItHome ?? false))
            {
                entity = new TransPrescription();
                if (entity.LoadByPrimaryKey(txtPrescriptionNo.Text) &&
                    entity.IsApproval == false) // UnApprove berhasil disave
                {
                    AddReduceMedicationQty(entity.PrescriptionNo, reg.RegistrationNo, reg.SRRegistrationType, false);
                }
            }
        }


        private void SetUnApproval(bool isApproval, ValidateArgs args, string voidReason)
        {
            var prc = new TransPrescription();
            prc.LoadByPrimaryKey(txtPrescriptionNo.Text);

            var reg = new Registration();
            reg.LoadByPrimaryKey(prc.RegistrationNo);
            if (reg.IsHoldTransactionEntry ?? false)
            {
                args.MessageText = "Transaction is locked.";
                args.IsCancel = true;
                return;
            }

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);

            using (var trans = new esTransactionScope())
            {
                if (AppSession.Parameter.IsPrescriptionUnApprovalCreateNewNumber)
                {
                    //header
                    prc.MarkAllColumnsAsDirty(DataRowState.Added);

                    prc.ReferenceNo = prc.PrescriptionNo;

                    //var autoNumber = Helper.GetNewAutoNumber(txtPrescriptionDate.SelectedDate.Value.Date, AppEnum.AutoNumber.PrescriptionNo);

                    /*--- format penomoran disamakan dg menu Prescription Return ---*/
                    AppAutoNumberLast autoNumber;
                    if (AppSession.Parameter.IsPrescriptionReturnNoFormatBasedOnRegType)
                        autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date,
                                                             reg.SRRegistrationType == AppConstant.RegistrationType.InPatient
                                                                 ? AppEnum.AutoNumber.PrescRetIpNo
                                                                 : AppEnum.AutoNumber.PrescRetOpNo);
                    else
                        autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.PrescriptionNo);

                    //var autoNumber = Helper.GetNewAutoNumber(txtPrescriptionDate.SelectedDate.Value.Date,
                    //                                                  Request.QueryString["rt"] == "opr"
                    //                                                      ? AppEnum.AutoNumber.PrescRetOpNo
                    //                                                      : AppEnum.AutoNumber.PrescRetIpNo);

                    /*-------*/

                    prc.PrescriptionNo = autoNumber.LastCompleteNumber;
                    autoNumber.Save();

                    prc.IsPrescriptionReturn = true;
                    prc.IsApproval = false;
                    prc.IsBillProceed = false;
                    prc.IsVoid = true;
                    prc.VoidReason = voidReason;
                    prc.ApprovalDateTime = (new DateTime()).NowAtSqlServer();
                    prc.ApprovedByUserID = AppSession.UserLogin.UserID;
                    prc.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    prc.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    prc.FloorSeqNo = null;
                    prc.IsUnapproved = true;
                    prc.UnapprovedByUserID = AppSession.UserLogin.UserID;
                    prc.UnapprovedDateTime = (new DateTime()).NowAtSqlServer();

                    prc.Save();

                    var prcReturns = new TransPrescriptionItemCollection();
                    var cstReturn = new CostCalculationCollection();

                    foreach (var detail in TransPrescriptionItems)
                    {
                        var r = new TransPrescriptionItem();
                        r.LoadByPrimaryKey(detail.PrescriptionNo, detail.SequenceNo);
                        r.MarkAllColumnsAsDirty(DataRowState.Added);

                        r.PrescriptionNo = prc.PrescriptionNo;

                        decimal qty = 0 - (detail.ResultQty ?? 0);

                        r.PrescriptionQty = qty;
                        r.TakenQty = qty;
                        r.DiscountAmount = r.DiscountAmount;

                        if (Math.Abs(qty) < r.ResultQty) r.RecipeAmount = 0;

                        r.ResultQty = qty;
                        r.LineAmount = 0 - r.LineAmount;
                        r.IsApprove = false;
                        //r.IsVoid = false;
                        r.IsBillProceed = false;
                        r.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        r.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                        r.ItemQtyInString = qty.ToString();
                        r.EmbalaceQty = qty.ToString();

                        prcReturns.AttachEntity(r);

                        var presc = new TransPrescriptionItem();
                        presc.LoadByPrimaryKey(prc.ReferenceNo, detail.SequenceNo);

                        if (!AppSession.Parameter.IsUsingIntermBill)
                        {
                            var calcQUery = new CostCalculationQuery();
                            calcQUery.Where(
                                calcQUery.TransactionNo == prc.ReferenceNo,
                                calcQUery.SequenceNo == detail.SequenceNo
                                );

                            var calc = new CostCalculation();
                            calc.Load(calcQUery);

                            var cost = cstReturn.AddNew();
                            cost.RegistrationNo = prc.RegistrationNo;
                            cost.TransactionNo = prc.PrescriptionNo;
                            cost.SequenceNo = r.SequenceNo;
                            cost.ItemID = string.IsNullOrEmpty(r.ItemInterventionID) ? r.ItemID : r.ItemInterventionID;
                            cost.PatientAmount = 0 - calc.PatientAmount;
                            cost.GuarantorAmount = 0 - calc.GuarantorAmount;
                            cost.DiscountAmount = 0 - calc.DiscountAmount;
                            cost.ParamedicAmount = 0;
                            cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            cost.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        }
                    }
                    prcReturns.Save();

                    // stock calculation
                    var chargesBalances = new ItemBalanceCollection();
                    var chargesDetailBalances = new ItemBalanceDetailCollection();
                    var chargesDetailBalanceEds = new ItemBalanceDetailEdCollection();
                    var chargesMovements = new ItemMovementCollection();

                    ItemBalance.PrepareItemBalancesForReturn(prc.PrescriptionNo, prcReturns, BusinessObject.Reference.TransactionCode.PrescriptionReturn,
                        prc.ServiceUnitID, prc.LocationID, AppSession.UserLogin.UserID, isApproval, ref chargesBalances, ref chargesDetailBalances,
                        ref chargesMovements, ref chargesDetailBalanceEds, AppSession.Parameter.IsEnabledStockWithEdControl);

                    foreach (var prcReturn in prcReturns)
                    {
                        prcReturn.IsVoid = true;
                    }
                    prcReturns.Save();

                    if (!AppSession.Parameter.IsUsingIntermBill)
                    {
                        cstReturn.Save();
                    }
                    else
                    {
                        var ccColl = new CostCalculationCollection();
                        ccColl.Query.Where(ccColl.Query.TransactionNo == prc.ReferenceNo);
                        if (ccColl.LoadAll())
                        {
                            ccColl.MarkAllAsDeleted();
                        }
                        ccColl.Save();
                    }

                    if (chargesBalances != null)
                        chargesBalances.Save();
                    if (chargesDetailBalances != null)
                        chargesDetailBalances.Save();
                    if (chargesDetailBalanceEds != null)
                        chargesDetailBalanceEds.Save();
                    if (chargesMovements != null)
                        chargesMovements.Save();

                    /* Automatic Journal Testing Start */
                    var unit = new ServiceUnit();
                    unit.LoadByPrimaryKey(prc.ServiceUnitID);
                    //if (AppSession.Parameter.IsUsingIntermBill != "Yes")
                    //{
                    //    int? journalId = JournalTransactions.AddNewPrescriptionReturnJournal(prc, reg, unit, cstReturn, "RS", AppSession.UserLogin.UserID, 0);
                    //}
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                    {
                        if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                        {
                            JournalTransactions.AddNewPatientIncomeAccrual(BusinessObject.JournalType.Income, prc.PrescriptionNo, AppSession.UserLogin.UserID, 0);
                        }
                        else
                        {
                            var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');
                            if (type.Contains(reg.SRRegistrationType))
                            {
                                var isClosingPeriod = PostingStatus.IsPeriodeClosed(prc.PrescriptionDate.Value.Date);
                                if (isClosingPeriod)
                                {
                                    args.MessageText = "Financial statements for period: " +
                                                       string.Format("{0:MMMM-yyyy}", prc.PrescriptionDate.Value.Date) +
                                                       " have been closed. Please contact the authorities.";
                                    args.IsCancel = true;
                                    return;
                                }

                                int? journalId = JournalTransactions.AddNewPrescriptionReturnJournalTemporaryNetto(prc, reg, unit,
                                    cstReturn, "RS", AppSession.UserLogin.UserID, 0);
                            }
                        }
                    }
                    //else if (AppSession.Parameter.IsUsingIntermBill != "Yes")
                    //{
                    //    var isClosingPeriod = PostingStatus.IsPeriodeClosed(prc.PrescriptionDate.Value.Date);
                    //    if (isClosingPeriod)
                    //    {
                    //        args.MessageText = "Financial statements for period: " +
                    //                           string.Format("{0:MMMM-yyyy}", prc.PrescriptionDate.Value.Date) +
                    //                           " have been closed. Please contact the authorities.";
                    //        args.IsCancel = true;
                    //        return;
                    //    }

                    //    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsPemisahanCOAUangRacikan) == "1")
                    //    {
                    //        int? journalId = JournalTransactions.AddNewPrescriptionReturnJournalWithSeparationPersonalizedRecipeMoney(prc, reg,
                    //            unit, cstReturn, "RS", AppSession.UserLogin.UserID, 0);
                    //    }
                    //    else
                    //    {
                    //        int? journalId = JournalTransactions.AddNewPrescriptionReturnJournal(prc, reg,
                    //            unit, cstReturn , "RS", AppSession.UserLogin.UserID, 0);
                    //    }
                    //}
                    /* Automatic Journal Testing End */

                    //original trans
                    {
                        var orihd = new TransPrescription();
                        orihd.LoadByPrimaryKey(txtPrescriptionNo.Text);
                        orihd.IsApproval = false;
                        orihd.IsBillProceed = false;
                        orihd.IsVoid = true;
                        orihd.IsUnapproved = true;
                        orihd.UnapprovedByUserID = AppSession.UserLogin.UserID;
                        orihd.UnapprovedDateTime = (new DateTime()).NowAtSqlServer();
                        orihd.Save();

                        foreach (var oridt in TransPrescriptionItems)
                        {
                            oridt.IsApprove = false;
                            oridt.IsVoid = true;
                            oridt.IsBillProceed = false;
                        }
                        TransPrescriptionItems.Save();
                    }

                    if (AppSession.Parameter.IsAutoCreateNewPrescriptionTxOnUnapproval)
                    {
                        //original trans move to new trans
                        {
                            var hd = new TransPrescription();
                            hd.LoadByPrimaryKey(txtPrescriptionNo.Text);
                            hd.MarkAllColumnsAsDirty(DataRowState.Added);

                            //autoNumber = Helper.GetNewAutoNumber(txtPrescriptionDate.SelectedDate.Value.Date, AppEnum.AutoNumber.PrescriptionNo);
                            autoNumber = Helper.GetNewAutoNumber(txtPrescriptionDate.SelectedDate.Value.Date,
                                                                          Request.QueryString["rt"] == "opr"
                                                                              ? AppEnum.AutoNumber.PrescOpNo
                                                                              : AppEnum.AutoNumber.PrescIpNo);

                            hd.PrescriptionNo = autoNumber.LastCompleteNumber;
                            autoNumber.Save();

                            hd.IsApproval = false;
                            hd.IsBillProceed = false;
                            hd.IsVoid = false;
                            hd.str.ApprovalDateTime = string.Empty;
                            hd.ApprovedByUserID = null;
                            hd.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            hd.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            hd.IsUnapproved = null;
                            hd.UnapprovedByUserID = null;
                            hd.str.UnapprovedDateTime = string.Empty;
                            hd.str.CompleteDateTime = string.Empty;
                            hd.str.DeliverDateTime = string.Empty;

                            hd.Save();

                            var dt = new TransPrescriptionItemCollection();
                            dt.Query.Where(dt.Query.PrescriptionNo == txtPrescriptionNo.Text);
                            dt.LoadAll();

                            foreach (var d in dt)
                            {
                                d.MarkAllColumnsAsDirty(DataRowState.Added);

                                d.PrescriptionNo = hd.PrescriptionNo;

                                d.IsApprove = false;
                                d.IsVoid = false;
                                d.IsBillProceed = false;


                                //hitung price ulang, antisipasi u/ yg pake rule margin
                                decimal resultQty = d.ResultQty ?? 0;
                                var itemMedic = new ItemProductMedic();
                                if (itemMedic.LoadByPrimaryKey(string.IsNullOrEmpty(d.ItemInterventionID) ? d.ItemID : d.ItemInterventionID))
                                {
                                    if (!(itemMedic.IsActualDeduct ?? false))
                                        resultQty = Math.Ceiling(d.ResultQty ?? 0);
                                }

                                if ((d.IsCompound ?? false) && AppSession.Parameter.RecipeMarginValueCompound != 0)
                                {
                                    d.Price = (decimal)Helper.Tariff.GetItemTariffNonMargin(grr.SRTariffType, txtPrescriptionDate.SelectedDate.Value.Date,
                                                                                                         reg.ChargeClassID,
                                                                                                         string.IsNullOrEmpty(d.ItemInterventionID) ? d.ItemID : d.ItemInterventionID,
                                                                                                         (d.IsCompound ?? false),
                                                                                                         d.SRDosageUnit);
                                    d.Price += Convert.ToDecimal(AppSession.Parameter.RecipeMarginValueCompound / 100) * d.Price;
                                }
                                else
                                {
                                    d.Price = (decimal)Helper.Tariff.GetItemTariff(grr.SRTariffType, txtPrescriptionDate.SelectedDate.Value.Date, reg.ChargeClassID,
                                        string.IsNullOrEmpty(d.ItemInterventionID) ? d.ItemID : d.ItemInterventionID, (d.IsCompound ?? false), d.SRDosageUnit, reg.GuarantorID, reg.SRRegistrationType);
                                }

                                /* DiscountAmount di-nol-kan krn akan dihitung ulang pada saat proses approved */
                                //d.DiscountAmount += resultQty * d.AutoProcessCalculation;
                                //if (d.DiscountAmount <= 1) d.DiscountAmount = 0;
                                d.DiscountAmount = 0;
                                d.AutoProcessCalculation = 0;

                                d.LineAmount = d.LineAmount = ((resultQty * d.Price) - d.DiscountAmount) + d.EmbalaceAmount + (d.SweetenerAmount ?? 0) + d.RecipeAmount;
                                d.LineAmount = Helper.Rounding(d.LineAmount ?? 0, AppEnum.RoundingType.Prescription);

                                if (Helper.GuarantorBpjsCasemix.Contains(reg.GuarantorID) && AppSession.Parameter.CasemixValidationRegistrationType.Contains(reg.SRRegistrationType))
                                {
                                    var itemId = (string.IsNullOrWhiteSpace(d.ItemInterventionID) ? d.ItemID : d.ItemInterventionID);
                                    d.IsCasemixApproved = Helper.IsCasemixApproved(itemId, resultQty, reg.RegistrationNo, d.PrescriptionNo, reg.GuarantorID, true);
                                }
                                else
                                    d.IsCasemixApproved = true;

                                d.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                d.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            }

                            dt.Save();

                            txtPrescriptionNo.Text = hd.PrescriptionNo;
                        }
                    }
                }
                else
                {
                    var prcReturns = new TransPrescriptionItemCollection();
                    var cstReturn = new CostCalculationCollection();

                    foreach (var detail in TransPrescriptionItems)
                    {
                        var r = new TransPrescriptionItem();
                        r.LoadByPrimaryKey(detail.PrescriptionNo, detail.SequenceNo);
                        r.MarkAllColumnsAsDirty(DataRowState.Added);

                        decimal qty = 0 - (detail.ResultQty ?? 0);

                        r.PrescriptionQty = qty;
                        r.TakenQty = qty;
                        r.ResultQty = qty;

                        r.IsApprove = false;
                        r.IsVoid = false;
                        r.IsBillProceed = false;
                        r.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        r.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                        r.ItemQtyInString = qty.ToString();
                        r.EmbalaceQty = qty.ToString();

                        prcReturns.AttachEntity(r);


                        var calcQUery = new CostCalculationQuery();
                        calcQUery.Where(
                            calcQUery.TransactionNo == prc.PrescriptionNo,
                            calcQUery.SequenceNo == detail.SequenceNo
                            );

                        var calc = new CostCalculation();
                        calc.Load(calcQUery);

                        var cost = cstReturn.AddNew();
                        cost.RegistrationNo = prc.RegistrationNo;
                        cost.TransactionNo = prc.PrescriptionNo;
                        cost.SequenceNo = r.SequenceNo;
                        cost.ItemID = string.IsNullOrEmpty(r.ItemInterventionID) ? r.ItemID : r.ItemInterventionID;
                        cost.PatientAmount = 0 - calc.PatientAmount;
                        cost.GuarantorAmount = 0 - calc.GuarantorAmount;
                        cost.DiscountAmount = 0 - calc.DiscountAmount;
                        cost.ParamedicAmount = 0;
                        cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        cost.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }

                    // stock calculation
                    var chargesBalances = new ItemBalanceCollection();
                    var chargesDetailBalances = new ItemBalanceDetailCollection();
                    var chargesDetailBalanceEds = new ItemBalanceDetailEdCollection();
                    var chargesMovements = new ItemMovementCollection();

                    ItemBalance.PrepareItemBalancesForReturn2(prc.PrescriptionNo, prcReturns, BusinessObject.Reference.TransactionCode.PrescriptionReturn,
                        prc.ServiceUnitID, prc.LocationID, AppSession.UserLogin.UserID, isApproval, ref chargesBalances, ref chargesDetailBalances,
                        ref chargesMovements, ref chargesDetailBalanceEds, AppSession.Parameter.IsEnabledStockWithEdControl);

                    var ccColl = new CostCalculationCollection();
                    ccColl.Query.Where(ccColl.Query.TransactionNo == prc.PrescriptionNo);
                    if (ccColl.LoadAll())
                    {
                        ccColl.MarkAllAsDeleted();
                    }
                    ccColl.Save();

                    if (chargesBalances != null)
                        chargesBalances.Save();
                    if (chargesDetailBalances != null)
                        chargesDetailBalances.Save();
                    if (chargesDetailBalanceEds != null)
                        chargesDetailBalanceEds.Save();
                    if (chargesMovements != null)
                        chargesMovements.Save();

                    /* Automatic Journal Testing Start */
                    var unit = new ServiceUnit();
                    unit.LoadByPrimaryKey(prc.ServiceUnitID);

                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                    {
                        if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                        {
                            JournalTransactions.AddNewPatientIncomeAccrual(BusinessObject.JournalType.Income, prc.PrescriptionNo, AppSession.UserLogin.UserID, 0);
                        }
                        else
                        {
                            var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');
                            if (type.Contains(reg.SRRegistrationType))
                            {
                                var isClosingPeriod = PostingStatus.IsPeriodeClosed(prc.PrescriptionDate.Value.Date);
                                if (isClosingPeriod)
                                {
                                    args.MessageText = "Financial statements for period: " +
                                                       string.Format("{0:MMMM-yyyy}", prc.PrescriptionDate.Value.Date) +
                                                       " have been closed. Please contact the authorities.";
                                    args.IsCancel = true;
                                    return;
                                }

                                int? journalId = JournalTransactions.AddNewPrescriptionReturnJournalTemporaryNetto(prc, reg, unit,
                                    cstReturn, "RS", AppSession.UserLogin.UserID, 0);
                            }
                        }
                    }

                    //original trans
                    {
                        var orihd = new TransPrescription();
                        orihd.LoadByPrimaryKey(txtPrescriptionNo.Text);
                        orihd.IsApproval = false;
                        orihd.IsBillProceed = false;

                        orihd.IsUnapproved = true;
                        orihd.UnapprovedByUserID = AppSession.UserLogin.UserID;
                        orihd.UnapprovedDateTime = (new DateTime()).NowAtSqlServer();
                        orihd.Save();

                        foreach (var oridt in TransPrescriptionItems)
                        {
                            oridt.IsApprove = false;
                            oridt.IsBillProceed = false;
                        }
                        TransPrescriptionItems.Save();
                    }
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void ApprovPrescription(TransPrescription entity, ValidateArgs args)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(entity.RegistrationNo);

            //if (reg.IsHoldTransactionEntry ?? false)
            //{
            //    args.MessageText = "Transaction is locked.";
            //    args.IsCancel = true;
            //    return;
            //}

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(entity.ServiceUnitID);
            if (string.IsNullOrEmpty(entity.LocationID))
                entity.LocationID = unit.GetMainLocationId(entity.ServiceUnitID);

            // generate queue no
            if (!string.IsNullOrEmpty(entity.SRKioskQueueType))
            {
                var que = Temiang.Avicenna.WebService.KioskQueue.QueueAdd((new DateTime()).NowAtSqlServer(), entity.SRKioskQueueType, AppSession.UserLogin.UserID, unit.IsShowOnKiosk ?? true, AppSession.Parameter.IsAutoKioskQueueStatusSkippedForPrescription);
                entity.KioskQueueNo = que.KioskQueueNo;
                txtQueueNo.Text = entity.KioskQueueNo;
            }

            // urutan save :
            // 1. item movement
            // 2. item balance
            // nb : pastikan proses ini selesai lalu lanjut ke insert transaksi

            var sukses12 = false;

            using (var trans = new esTransactionScope())
            {
                var chargesBalances = new ItemBalanceCollection();
                var chargesDetailBalances = new ItemBalanceDetailCollection();
                var chargesDetailBalanceEds = new ItemBalanceDetailEdCollection();
                var chargesMovements = new ItemMovementCollection();

                string itemZeroCostPrice, itemNoStock;

                var collTransPrescriptionItems = new TransPrescriptionItemCollection();
                collTransPrescriptionItems.Query.Where(collTransPrescriptionItems.Query.PrescriptionNo == entity.PrescriptionNo);
                collTransPrescriptionItems.LoadAll();

                ItemBalance.UpdateCostPrice(collTransPrescriptionItems, out itemZeroCostPrice);
                if (!string.IsNullOrEmpty(itemZeroCostPrice))
                {
                    args.MessageText = "Zero cost price of item : " + itemZeroCostPrice;
                    args.IsCancel = true;
                    return;
                }

                ItemBalance.PrepareItemBalances(txtPrescriptionNo.Text, collTransPrescriptionItems, BusinessObject.Reference.TransactionCode.Prescription, entity.ServiceUnitID,
                   entity.LocationID, AppSession.UserLogin.UserID, true, ref chargesBalances, ref chargesDetailBalances, ref chargesMovements, ref chargesDetailBalanceEds,
                   AppSession.Parameter.IsEnabledStockWithEdControl, out itemNoStock);

                if (!string.IsNullOrEmpty(itemNoStock))
                {
                    var x = GetItemWithInsufficientBalance(txtPrescriptionNo.Text, entity.LocationID);
                    if (!string.IsNullOrEmpty(x))
                    {
                        args.MessageText = "Insufficient balance of item : " + x;
                        args.IsCancel = true;
                        return;
                    }

                    args.MessageText = "Insufficient balance of item : " + itemNoStock;
                    args.IsCancel = true;
                    return;
                }

                if (chargesBalances != null) chargesBalances.Save();
                if (chargesDetailBalances != null) chargesDetailBalances.Save();
                if (chargesDetailBalanceEds != null) chargesDetailBalanceEds.Save();
                if (chargesMovements != null) chargesMovements.Save();

                trans.Complete();

                sukses12 = true;
            }

            var isHasQty23Days = false;
            if (sukses12)
            {
                using (var trans = new esTransactionScope())
                {
                    CostCalculations = null;

                    var loc = new Location();
                    if (loc.LoadByPrimaryKey(entity.LocationID) && loc.IsHoldForTransaction == true)
                    {
                        args.MessageText = "Location: " + loc.LocationName + " in Hold For Transaction status. Transaction is not allowed.";
                        args.IsCancel = true;
                        return;
                    }

                    var grrID = reg.GuarantorID;
                    if (grrID == AppSession.Parameter.SelfGuarantor)
                    {
                        var pat = new Patient();
                        pat.LoadByPrimaryKey(reg.PatientID);
                        if (!string.IsNullOrEmpty(pat.MemberID)) grrID = pat.MemberID;
                    }

                    var collTransPrescriptionItems = new TransPrescriptionItemCollection();
                    collTransPrescriptionItems.Query.Where(collTransPrescriptionItems.Query.PrescriptionNo == entity.PrescriptionNo);
                    collTransPrescriptionItems.LoadAll();

                    var ItemID = collTransPrescriptionItems.Where(t => !(t.IsVoid ?? false)).Select(t => new { ItemID = string.IsNullOrEmpty(t.ItemInterventionID) ? t.ItemID : t.ItemInterventionID });
                    var tblCovered = Helper.GetCoveredItems(reg.RegistrationNo, grrID, ItemID.Select(t => t.ItemID).ToArray(), txtPrescriptionDate.SelectedDate.Value, true);

                    var rtype = "NR";

                    foreach (var detail in collTransPrescriptionItems.Where(t => !(t.IsVoid ?? false)))
                    {
                        if (isHasQty23Days == false && (detail.Qty23Days ?? 0) > 0)
                            isHasQty23Days = true;

                        if (detail.IsCompound == true) rtype = "R";

                        //Pembulatan qty
                        decimal resultQty = detail.ResultQty ?? 0;

                        var rowCovered = tblCovered.AsEnumerable().SingleOrDefault(t => t.Field<string>("ItemID") == (string.IsNullOrEmpty(detail.ItemInterventionID) ? detail.ItemID : detail.ItemInterventionID) &&
                                                                                        t.Field<bool>("IsInclude"));

                        detail.IsApprove = true;
                        detail.IsBillProceed = true;

                        // lolos validasi untuk fornas, default value
                        //detail.IsCasemixApproved = true;
                        //detail.CasemixApprovedByUserID = AppSession.UserLogin.UserID;
                        //detail.CasemixApprovedDateTime = (new DateTime()).NowAtSqlServer();

                        decimal rPrice = 0;
                        if (resultQty != 0)
                        {
                            rPrice = ((detail.EmbalaceAmount ?? 0) + (detail.SweetenerAmount ?? 0) + (detail.RecipeAmount ?? 0));
                        }
                        bool IsIncR = false;

                        if (rowCovered != null)
                        {
                            if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
                            {
                                // sementara berlaku cuma untuk rule diskon, pusing gw lihat coding gaya bar-bar kyk gini
                                // --> lu aja pusing apa lg gw... 
                                IsIncR = (AppSession.Parameter.IsPrescriptionDiscountIncludeR);
                                if ((bool)rowCovered["IsValueInPercent"])
                                {
                                    decimal _lineAmt = Convert.ToDecimal((resultQty * detail.Price) + (IsIncR ? rPrice : 0));
                                    if (AppSession.Parameter.IsPrescriptionDiscountAfterRounding)
                                        _lineAmt = Helper.Rounding(_lineAmt, AppEnum.RoundingType.Prescription);

                                    detail.DiscountAmount = Convert.ToDecimal(_lineAmt) * ((decimal)rowCovered["AmountValue"] / 100);
                                    detail.AutoProcessCalculation = 0 - (((IsIncR ? detail.Price + (rPrice / (resultQty == 0 ? 1 : resultQty)) : detail.Price)) * ((decimal)rowCovered["AmountValue"] / 100));
                                }
                                else
                                {
                                    detail.DiscountAmount = resultQty * (decimal)rowCovered["AmountValue"];
                                    detail.AutoProcessCalculation = 0 - (decimal)rowCovered["AmountValue"];
                                }

                                if (detail.DiscountAmount > detail.LineAmount)
                                    detail.DiscountAmount = detail.LineAmount;
                            }
                            else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeMargin))
                            {
                                if (detail.AutoProcessCalculation != 0)
                                {
                                    detail.Price -= detail.AutoProcessCalculation;
                                    detail.AutoProcessCalculation = 0;
                                }

                                if ((bool)rowCovered["IsValueInPercent"])
                                {
                                    detail.AutoProcessCalculation = ((decimal)rowCovered["AmountValue"] / 100) * detail.Price;
                                    detail.Price += ((decimal)rowCovered["AmountValue"] / 100) * detail.Price;
                                }
                                else
                                {
                                    detail.Price += (decimal)rowCovered["AmountValue"];
                                    detail.AutoProcessCalculation = (decimal)rowCovered["AmountValue"];
                                }
                            }
                        }

                        detail.LineAmount = (resultQty * detail.Price) + rPrice;

                        if (AppSession.Parameter.IsPrescriptionDiscountAfterRounding)
                            detail.LineAmount = Helper.Rounding((detail.LineAmount ?? 0), AppEnum.RoundingType.Prescription) - detail.DiscountAmount;
                        else
                            detail.LineAmount = Helper.Rounding((detail.LineAmount ?? 0) - (detail.DiscountAmount ?? 0), AppEnum.RoundingType.Prescription);

                        detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        detail.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                        //post
                        decimal recipeAmount = 0;
                        recipeAmount = (detail.EmbalaceAmount ?? 0) + (detail.SweetenerAmount ?? 0) + (detail.RecipeAmount ?? 0);
                        var calc = new Helper.CostCalculation(grrID, reg.IsGlobalPlafond ?? false,
                                   string.IsNullOrEmpty(detail.ItemInterventionID) ? detail.ItemID : detail.ItemInterventionID, Math.Abs(detail.LineAmount ?? 0),
                                   tblCovered, Math.Abs(resultQty), detail.Price ?? 0, recipeAmount, detail.DiscountAmount ?? 0);

                        //cost calculations
                        // sering terjadi duplicate primary key
                        CostCalculation cost = new CostCalculation();
                        cost.Query.Where(
                            cost.Query.RegistrationNo.Equal(entity.RegistrationNo),
                            cost.Query.TransactionNo.Equal(detail.PrescriptionNo),
                            cost.Query.SequenceNo.Equal(detail.SequenceNo));
                        if (cost.Load(cost.Query))
                        {

                        }
                        else
                        {
                            cost = CostCalculations.AddNew();
                        }

                        cost.RegistrationNo = entity.RegistrationNo;
                        cost.TransactionNo = detail.PrescriptionNo;
                        cost.SequenceNo = detail.SequenceNo;
                        cost.ItemID = string.IsNullOrEmpty(detail.ItemInterventionID) ? detail.ItemID : detail.ItemInterventionID;
                        cost.PatientAmount = calc.PatientAmount;
                        cost.GuarantorAmount = calc.GuarantorAmount;
                        if (reg.SRBussinesMethod == AppSession.Parameter.BusinessMethodFlavon)
                        {
                            cost.GuarantorAmount = calc.GuarantorAmount + calc.PatientAmount;
                            cost.PatientAmount = 0;
                        }
                        cost.DiscountAmount = detail.DiscountAmount;
                        cost.ParamedicAmount = 0;
                        cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        cost.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }

                    foreach (var item in collTransPrescriptionItems.Where(i => !(i.IsPendingDelivery ?? false)))
                    {
                        item.IsApprove = true;
                        item.IsBillProceed = true;
                    }

                    entity.IsApproval = true;
                    entity.ApprovalDateTime = (new DateTime()).NowAtSqlServer();
                    entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                    entity.IsBillProceed = true;

                    if (entity.ServiceUnitID == AppSession.Parameter.ServiceUnitPharmacyIdOpr && !string.IsNullOrEmpty(entity.SRFloor))
                    {
                        if (!(entity.IsPrescriptionReturn ?? false))
                        {
                            var shiftId = Registration.GetShiftID();
                            var floorsn = new TransPrescriptionFloorSeqNo();
                            if (!floorsn.LoadByPrimaryKey((new DateTime()).NowAtSqlServer().Date, entity.SRFloor, entity.ServiceUnitID, shiftId, rtype))
                            {
                                floorsn.AddNew();
                                floorsn.PrescriptionDate = (new DateTime()).NowAtSqlServer().Date;
                                floorsn.SRFloor = entity.SRFloor;
                                floorsn.ServiceUnitID = entity.ServiceUnitID;
                                floorsn.ShiftID = shiftId;
                                floorsn.Rtype = rtype;
                                floorsn.SeqNo = 1;
                            }
                            else floorsn.SeqNo += 1;

                            floorsn.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            floorsn.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            floorsn.Save();

                            entity.FloorSeqNo = floorsn.SeqNo;
                            entity.Rtype = floorsn.Rtype;
                        }
                    }
                    if (AppSession.Parameter.IsAutoPrintEtiquette)
                        entity.IsProceedByPharmacist = true;

                    entity.Save();
                    collTransPrescriptionItems.Save();
                    CostCalculations.Save();

                    /* Automatic Journal Testing Start */
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                    {
                        if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                        {
                            JournalTransactions.AddNewPatientIncomeAccrual(BusinessObject.JournalType.Income, entity.PrescriptionNo, AppSession.UserLogin.UserID, 0);
                        }
                        else
                        {
                            var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');
                            if (type.Contains(reg.SRRegistrationType))
                            {
                                var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.PrescriptionDate.Value.Date);
                                if (isClosingPeriod)
                                {
                                    args.MessageText = "Financial statements for period: " +
                                                       string.Format("{0:MMMM-yyyy}", entity.PrescriptionDate.Value.Date) +
                                                       " have been closed. Please contact the authorities.";
                                    args.IsCancel = true;
                                    return;
                                }

                                int? journalId = JournalTransactions.AddNewPrescriptionJournalTemporaryNetto(entity, reg, unit, CostCalculations, "RS", AppSession.UserLogin.UserID, 0);
                            }
                        }
                    }
                    /* Automatic Journal Testing End */
                    if (Helper.IsBpjsAntrolIntegration)
                    {
                        try
                        {
                            if (!string.IsNullOrWhiteSpace(reg.AppointmentNo) && reg.SRRegistrationType != AppConstant.RegistrationType.InPatient)
                            {
                                {
                                    var log = new WebServiceAPILog();
                                    log.DateRequest = DateTime.Now;
                                    log.IPAddress = string.Empty;
                                    log.UrlAddress = "PrescriptionSalesDetail";
                                    log.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                    {
                                        Kodebooking = reg.AppointmentNo,
                                        Taskid = 5,
                                        Waktu = Convert.ToInt64(DateTimeOffset.Now.AddMinutes(-2).ToUnixTimeMilliseconds())
                                    });

                                    var svc = new Common.BPJS.Antrian.Service();
                                    var response = svc.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                    {
                                        Kodebooking = reg.AppointmentNo,
                                        Taskid = 5,
                                        Waktu = Convert.ToInt64(DateTimeOffset.Now.AddMinutes(-2).ToUnixTimeMilliseconds())
                                    });

                                    if (!response.Metadata.IsAntrolValid)
                                    {
                                        log = new WebServiceAPILog();
                                        log.DateRequest = DateTime.Now;
                                        log.IPAddress = string.Empty;
                                        log.UrlAddress = "PrescriptionSalesDetail";
                                        log.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                        {
                                            Kodebooking = reg.AppointmentNo,
                                            Taskid = 4,
                                            Waktu = Convert.ToInt64(DateTimeOffset.Now.AddMinutes(-10).ToUnixTimeMilliseconds())
                                        });

                                        svc = new Common.BPJS.Antrian.Service();
                                        response = svc.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                        {
                                            Kodebooking = reg.AppointmentNo,
                                            Taskid = 4,
                                            Waktu = Convert.ToInt64(DateTimeOffset.Now.AddMinutes(-10).ToUnixTimeMilliseconds())
                                        });

                                        if (response.Metadata.IsAntrolValid)
                                        {
                                            log = new WebServiceAPILog();
                                            log.DateRequest = DateTime.Now;
                                            log.IPAddress = string.Empty;
                                            log.UrlAddress = "PrescriptionSalesDetail";
                                            log.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                            {
                                                Kodebooking = reg.AppointmentNo,
                                                Taskid = 5,
                                                Waktu = Convert.ToInt64(DateTimeOffset.Now.AddMinutes(-2).ToUnixTimeMilliseconds())
                                            });

                                            svc = new Common.BPJS.Antrian.Service();
                                            response = svc.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                            {
                                                Kodebooking = reg.AppointmentNo,
                                                Taskid = 5,
                                                Waktu = Convert.ToInt64(DateTimeOffset.Now.AddMinutes(-2).ToUnixTimeMilliseconds())
                                            });
                                        }
                                    }

                                    log.Response = JsonConvert.SerializeObject(response);
                                    log.Save();
                                }
                                {
                                    var jenisResep = "non racikan";
                                    var std = new AppStandardReferenceItem();
                                    if (std.LoadByPrimaryKey(AppEnum.StandardReference.KioskQueueFar.ToString(), entity.SRKioskQueueType))
                                    {
                                        if (!string.IsNullOrWhiteSpace(std.Note)) jenisResep = std.Note;
                                    }

                                    var log = new WebServiceAPILog();
                                    log.DateRequest = DateTime.Now;
                                    log.IPAddress = string.Empty;
                                    log.UrlAddress = "PrescriptionSalesDetail";
                                    log.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Farmasi.Request.Root()
                                    {
                                        Kodebooking = reg.AppointmentNo,
                                        Jenisresep = jenisResep,
                                        Nomorantrean = entity.KioskQueueNo,
                                        Keterangan = string.Empty
                                    });

                                    var svc = new Common.BPJS.Antrian.Service();
                                    var response = svc.TambahAntrianFarmasi(new Common.BPJS.Antrian.Farmasi.Request.Root()
                                    {
                                        Kodebooking = reg.AppointmentNo,
                                        Jenisresep = jenisResep,
                                        Nomorantrean = entity.KioskQueueNo,
                                        Keterangan = string.Empty
                                    });

                                    log.Response = JsonConvert.SerializeObject(response);
                                    log.Save();
                                }
                                {
                                    var log = new WebServiceAPILog();
                                    log.DateRequest = DateTime.Now;
                                    log.IPAddress = string.Empty;
                                    log.UrlAddress = "PrescriptionSalesDetail";
                                    log.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                    {
                                        Kodebooking = reg.AppointmentNo,
                                        Taskid = 6,
                                        Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds())
                                    });

                                    var svc = new Common.BPJS.Antrian.Service();
                                    var response = svc.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                    {
                                        Kodebooking = reg.AppointmentNo,
                                        Taskid = 6,
                                        Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds())
                                    });

                                    log.Response = JsonConvert.SerializeObject(response);
                                    log.Save();
                                }
                            }
                        }
                        catch (Exception e)
                        {

                        }
                    }

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                // create resep 23hari
                if (isHasQty23Days)
                {
                    Create23DaysPrescription();
                }

                // auto approve etiket
                if (AppSession.Parameter.IsAutoPrintEtiquette)
                {
                    bool isEtiketDalam = false;
                    bool isEtiketLuar = false;

                    foreach (var tpi in TransPrescriptionItems)
                    {
                        if (tpi.IsRFlag ?? false && tpi.TakenQty != 0)
                        {
                            var itemId = string.IsNullOrEmpty(tpi.ItemInterventionID) ? tpi.ItemID : tpi.ItemInterventionID;
                            var ipm = new ItemProductMedic();
                            if (ipm.LoadByPrimaryKey(itemId))
                            {
                                if (ipm.SRDrugLabelType == "1")
                                {
                                    isEtiketDalam = true;
                                    //// obat dalam / putih
                                    //var parWhiteEtiquette = new PrintJobParameterCollection();
                                    //parWhiteEtiquette.AddNew("p_PrescriptionNo", tpi.PrescriptionNo, null, null);
                                    //parWhiteEtiquette.AddNew("p_SequenceNo", tpi.SequenceNo, null, null);
                                    //parWhiteEtiquette.AddNew("p_Label", "1", null, null);
                                    //PrintManager.CreatePrintJob(AppConstant.Report.PrescriptionEtiket, parWhiteEtiquette);
                                }
                                else if (ipm.SRDrugLabelType == "2")
                                {
                                    // obat luar / biru
                                    if (!string.IsNullOrEmpty(AppSession.Parameter.RegistrationTypeOuterEtiquettePrintRestrictions) && AppSession.Parameter.RegistrationTypeOuterEtiquettePrintRestrictions.Contains(reg.SRRegistrationType))
                                    {
                                        isEtiketLuar = true;
                                        //var parBlueEtiquette = new PrintJobParameterCollection();
                                        //parBlueEtiquette.AddNew("p_PrescriptionNo", tpi.PrescriptionNo, null, null);
                                        //parBlueEtiquette.AddNew("p_SequenceNo", tpi.SequenceNo, null, null);
                                        //parBlueEtiquette.AddNew("p_Label", "2", null, null);
                                        //PrintManager.CreatePrintJob(AppConstant.Report.PrescriptionEtiketLr, parBlueEtiquette);
                                    }
                                }
                            }
                        }
                    }
                    if (!IsUddMode) // UDD tidak disini print nya tapi di medication Setup menu (base on RSI) (Handono 2022 07)
                    {
                        if (isEtiketDalam)
                        {
                            // obat dalam / putih
                            var parWhiteEtiquette = new PrintJobParameterCollection();
                            parWhiteEtiquette.AddNew("p_PrescriptionNo", entity.PrescriptionNo, null, null);
                            parWhiteEtiquette.AddNew("p_SequenceNo", string.Empty, null, null);
                            parWhiteEtiquette.AddNew("p_Label", "1", null, null);
                            PrintManager.CreatePrintJob(AppConstant.Report.PrescriptionEtiket, parWhiteEtiquette);
                        }
                        if (isEtiketLuar)
                        {
                            // obat luar / biru
                            var parBlueEtiquette = new PrintJobParameterCollection();
                            parBlueEtiquette.AddNew("p_PrescriptionNo", entity.PrescriptionNo, null, null);
                            parBlueEtiquette.AddNew("p_SequenceNo", string.Empty, null, null);
                            parBlueEtiquette.AddNew("p_Label", "2", null, null);
                            PrintManager.CreatePrintJob(AppConstant.Report.PrescriptionEtiketLr, parBlueEtiquette);
                        }
                    }
                }
                if (AppSession.Parameter.IsAutoPrintPrescriptionReceipt)
                {
                    var parPrescription = new PrintJobParameterCollection();
                    parPrescription.AddNew("p_PrescriptionNo", entity.PrescriptionNo, null, null);
                    parPrescription.AddNew("p_Label", string.Empty, null, null);
                    parPrescription.AddNew("p_UserID", AppSession.UserLogin.UserID, null, null);

                    PrintManager.CreatePrintJob(AppConstant.Report.RSSA_PrescriptionReceiptSlip, parPrescription);
                }
            }

            if (Helper.IsBpjsAntrolIntegration)
            {

            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var reason = args.ReasonText;

            var entity = new TransPrescription();
            if (!entity.LoadByPrimaryKey(txtPrescriptionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            //if (entity.IsUnitDosePrescription ?? false)
            //{
            //    var presc = new TransPrescriptionCollection();
            //    presc.Query.Where(presc.Query.IsUnitDosePrescription == true,
            //                      presc.Query.ReferenceNo == entity.PrescriptionNo, presc.Query.IsVoid == false);
            //    presc.LoadAll();
            //    if (presc.Count > 0)
            //    {
            //        args.MessageText = "This data can't be void. Existing unit dose realization for the selected data.";
            //        args.IsCancel = true;
            //        return;
            //    }
            //}
            entity.VoidReason = reason;

            SetVoid(entity, true);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            var entity = new TransPrescription();
            if (!entity.LoadByPrimaryKey(txtPrescriptionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private void SetVoid(TransPrescription entity, bool isVoid)
        {
            //header
            entity.IsVoid = isVoid;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            //detail
            //foreach (TransPrescriptionItem item in TransPrescriptionItems)
            //{
            //    item.IsVoid = isVoid;
            //    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
            //    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            //}
            var collTransPrescriptionItems = new TransPrescriptionItemCollection();
            collTransPrescriptionItems.Query.Where(collTransPrescriptionItems.Query.PrescriptionNo == entity.PrescriptionNo);
            collTransPrescriptionItems.LoadAll();
            foreach (TransPrescriptionItem item in collTransPrescriptionItems)
            {
                item.IsVoid = isVoid;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                //TransPrescriptionItems.Save();
                collTransPrescriptionItems.Save();

                var reg = new Registration();
                reg.LoadByPrimaryKey(txtRegistrationNo.Text);

                if (!string.IsNullOrEmpty(Request.QueryString["mode"]))
                {
                    reg.IsVoid = true;
                    reg.IsClosed = true;
                    reg.Save();
                }

                // Update MedicationReceive (UDD) untuk resep rawat inap & Emergency untuk tipe Home Prescription
                // krn sudah langsung diimport saat dokter save order (Handono 231101)
                if (entity.IsForTakeItHome ?? false)
                    AddReduceMedicationQty(entity.PrescriptionNo, reg.RegistrationNo, reg.SRRegistrationType, !isVoid);

                //Commit if success, Rollback if failed
                trans.Complete();
            }

        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new TransPrescription();
            if (entity.LoadByPrimaryKey(txtPrescriptionNo.Text))
            {
                if (!IsApprovedOrVoid(entity, args))
                    return;
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        private bool IsApprovedOrVoid(TransPrescription entity, ValidateArgs args)
        {
            if (entity.IsApproval != null && entity.IsApproval.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            if (entity.IsVoid != null && entity.IsVoid.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(TransPrescription entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtPrescriptionNo.Text = GetNewPrescriptionNo(chkUnitDose.Checked);
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }

            entity.PrescriptionNo = txtPrescriptionNo.Text;
            entity.PrescriptionDate = txtPrescriptionDate.SelectedDate;
            entity.ExecutionDate = txtExecutionDate.SelectedDate;

            if (string.IsNullOrEmpty(Request.QueryString["mode"]))
                entity.RegistrationNo = txtRegistrationNo.Text;
            //else
            //    entity.RegistrationNo = GetNewRegistrationNo();

            entity.ServiceUnitID = cboServiceUnitID.SelectedValue;
            entity.LocationID = cboLocationID.SelectedValue;

            if (string.IsNullOrEmpty(Request.QueryString["mode"]))
            {
                //var reg = new Registration();
                //reg.LoadByPrimaryKey(txtRegistrationNo.Text);
                //entity.ClassID = reg.ChargeClassID;
                entity.ClassID = txtClassID.Text;
            }
            else
                entity.ClassID = AppSession.Parameter.DefaultTariffClass;

            entity.ParamedicID = cboParamedicID.SelectedValue;
            entity.Note = txtPrescriptionText.Text;
            entity.IsPrescriptionReturn = false;
            entity.ReferenceNo = string.Empty;
            entity.IsDirect = chkIsDirect.Checked;
            entity.IsUnitDosePrescription = IsUddMode;
            //if (trUnitDose.Visible)
            //{
            //    entity.IsUnitDosePrescription = chkUnitDose.Checked;
            //    entity.IsClosed = true;
            //}
            entity.OrderNo = string.Empty;

            entity.AdditionalNote = txtAdditionalNote.Text;
            entity.NoTelp = txtNoTelp.Text;
            entity.FullAddress = txtFullAddress.Text;
            entity.SRFloor = cboSRFloor.SelectedValue;
            entity.FromServiceUnitID = txtServiceUnitID.Text;
            entity.FromRoomID = txtRoomID.Text;
            entity.FromBedID = txtBedID.Text;
            entity.IsPos = !string.IsNullOrEmpty(Request.QueryString["mode"]) && Request.QueryString["mode"] == "pos";
            entity.SRKioskQueueType = cboQueueType.SelectedValue;
            entity.Is23Days = chkIs23Days.Checked;
            entity.IsSplitBill = chkIsSplitBill.Checked;
            entity.IsCash = chkIsCash.Checked;

            if (entity.es.IsAdded)
                entity.IsFromSOAP = false;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                if (entity.es.IsAdded)
                {
                    entity.CreatedByUserID = AppSession.UserLogin.UserID;
                    entity.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                }
            }

            foreach (TransPrescriptionItem item in TransPrescriptionItems)
            {
                item.PrescriptionNo = entity.PrescriptionNo;

                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    if (item.es.IsAdded)
                    {
                        item.CreatedByUserID = AppSession.UserLogin.UserID;
                        item.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                    }
                }
            }
        }

        private void SaveEntity(TransPrescription entity)
        {
            using (var trans = new esTransactionScope())
            {
                //autonumber has been saved on SetEntity
                //if (entity.es.IsAdded)
                //    _autoNumber.Save();

                if (!string.IsNullOrEmpty(Request.QueryString["mode"]) && entity.es.IsAdded)
                {
                    var reg = new Registration();
                    txtRegistrationNo.Text = GetNewRegistrationNo();
                    reg.RegistrationNo = txtRegistrationNo.Text;
                    reg.SRRegistrationType = (Request.QueryString["mode"] == "direct" && Request.QueryString["rt"] == "ipr" && pnlSplitBill.Visible) ? AppConstant.RegistrationType.InPatient : AppConstant.RegistrationType.OutPatient;
                    reg.PatientID = Request.QueryString["mode"] == "direct" ? cboPatientID.SelectedValue : AppSession.Parameter.OTCPrescriptionPatientID;
                    reg.GuarantorID = cboGuarantorID.SelectedValue;
                    reg.ClassID = AppSession.Parameter.OutPatientClassID;
                    reg.RegistrationDate = txtPrescriptionDate.SelectedDate;
                    reg.RegistrationTime = (new DateTime()).NowAtSqlServer().ToString("HH:mm");
                    reg.AgeInYear = Convert.ToByte(txtAgeYear.Value);
                    reg.AgeInMonth = Convert.ToByte(txtAgeMonth.Value);
                    reg.AgeInDay = Convert.ToByte(txtAgeDay.Value);
                    reg.SRShift = Registration.GetShiftID();
                    reg.DepartmentID = AppParameter.GetParameterValue(AppParameter.ParameterItem.PharmacyDepartmentID);
                    reg.ServiceUnitID = txtServiceUnitID.Text;
                    reg.ChargeClassID = AppSession.Parameter.OutPatientClassID;
                    reg.CoverageClassID = AppSession.Parameter.OutPatientClassID;
                    reg.str.ParamedicID = cboParamedicID.SelectedValue;
                    reg.IsRoomIn = false;
                    reg.IsFromDispensary = true;
                    reg.LastCreateUserID = AppSession.UserLogin.UserID;
                    reg.LastCreateDateTime = (new DateTime()).NowAtSqlServer();

                    //Guarantor Detail Info
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
                    reg.FromRegistrationNo = cboFromRegistrationNo.SelectedValue;

                    //// modif validasi no bpjs sep
                    reg.BpjsSepNo = txtBpjsSepNo.Text;

                    //Last Update Status
                    reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    reg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    reg.Save();

                    var mrg = new MergeBilling();
                    mrg.RegistrationNo = reg.RegistrationNo;
                    mrg.FromRegistrationNo = string.Empty;

                    //Last Update Status
                    mrg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    mrg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    mrg.Save();

                    entity.RegistrationNo = reg.RegistrationNo;

                    _autoNumberReg.Save();

                    entity.RegistrationNo = reg.RegistrationNo;
                }

                if (entity.es.IsAdded)
                {
                    //if (Helper.IsBpjsAntrolIntegration)
                    //{
                    //    try
                    //    {
                    //        var reg = new Registration();
                    //        reg.LoadByPrimaryKey(entity.RegistrationNo);

                    //        if (!string.IsNullOrWhiteSpace(reg.AppointmentNo) && reg.SRRegistrationType != AppConstant.RegistrationType.InPatient)
                    //        {
                    //            var log = new WebServiceAPILog();
                    //            log.DateRequest = DateTime.Now;
                    //            log.IPAddress = string.Empty;
                    //            log.UrlAddress = "PrescriptionSalesDetail";
                    //            log.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                    //            {
                    //                Kodebooking = reg.AppointmentNo,
                    //                Taskid = 5,
                    //                Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds())
                    //            });

                    //            var svc = new Common.BPJS.Antrian.Service();
                    //            var response = svc.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                    //            {
                    //                Kodebooking = reg.AppointmentNo,
                    //                Taskid = 5,
                    //                Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds()),
                    //                Jenisresep = "Non Racikan"
                    //            });

                    //            log.Response = JsonConvert.SerializeObject(response);
                    //            log.Save();

                    //            log = new WebServiceAPILog();
                    //            log.DateRequest = DateTime.Now;
                    //            log.IPAddress = string.Empty;
                    //            log.UrlAddress = "PrescriptionSalesDetail";
                    //            log.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Farmasi.Request.Root()
                    //            {
                    //                Kodebooking = reg.AppointmentNo
                    //            });

                    //            svc = new Common.BPJS.Antrian.Service();
                    //            var antrian = svc.TambahAntrianFarmasi(new Common.BPJS.Antrian.Farmasi.Request.Root()
                    //            {
                    //                Kodebooking = reg.AppointmentNo
                    //            });

                    //            log.Response = JsonConvert.SerializeObject(response);
                    //            log.Save();
                    //        }
                    //    }
                    //    catch (Exception e)
                    //    {

                    //    }
                    //}

                    // Save info AB RASPRO
                    if (IsUddMode)
                    {
                        // Set RasproSeqNo from last raspro form for Medication Raspro Info
                        var uddItem = new UddItem();
                        uddItem.Query.Where(uddItem.Query.RegistrationNo == entity.RegistrationNo,
                            uddItem.Query.LocationID == Request.QueryString["loc"],
                            uddItem.Query.IsStop == false,
                            uddItem.Query.RasproSeqNo > 0);
                        uddItem.Query.es.Top = 1;
                        uddItem.Query.OrderBy(uddItem.Query.RasproSeqNo.Descending);

                        if (uddItem.Query.Load())
                            entity.RasproSeqNo = uddItem.RasproSeqNo;
                        else
                            entity.RasproSeqNo = 0;
                    }
                }

                entity.Save();

                TransPrescriptionItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new TransPrescriptionQuery("a");
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.PrescriptionNo > txtPrescriptionNo.Text,
                        que.IsPrescriptionReturn == false
                    );

                if (IsUddMode)
                    que.Where(que.IsUnitDosePrescription == true);
                else
                    que.Where(que.IsFromSOAP == (Request.QueryString["type"] == "realization"), que.IsUnitDosePrescription == false);

                if (!string.IsNullOrEmpty(Request.QueryString["mode"]) && Request.QueryString["mode"] == "pos")
                    que.Where(que.IsPos.IsNotNull(), que.IsPos == true);
                else
                    que.Where(que.Or(que.IsPos.IsNull(), que.IsPos == false));

                if (Request.QueryString["type"] == "realization" && AppSession.Parameter.IsPrescOrderHandlingBasedOnDispensary)
                {
                    if (Request.QueryString["rt"] == "ipr")
                        que.Where(que.ServiceUnitID == AppSession.Parameter.ServiceUnitPharmacyID);
                    else
                        que.Where(que.ServiceUnitID != AppSession.Parameter.ServiceUnitPharmacyID);
                }
                else
                {
                    var reg = new RegistrationQuery("b");
                    que.InnerJoin(reg).On(que.RegistrationNo == reg.RegistrationNo);
                    if (Request.QueryString["rt"] == "opr")
                        que.Where(reg.SRRegistrationType != AppConstant.RegistrationType.InPatient);
                    else
                        que.Where(reg.SRRegistrationType == AppConstant.RegistrationType.InPatient);
                }

                if (!AppSession.Parameter.IsMoveRecordOnPrescSalesIncludeVoid)
                    que.Where(que.IsVoid == false);

                que.OrderBy(que.PrescriptionNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.PrescriptionNo < txtPrescriptionNo.Text,
                        que.IsPrescriptionReturn == false

                    );
                if (IsUddMode)
                    que.Where(que.IsUnitDosePrescription == true);
                else
                    que.Where(que.IsFromSOAP == (Request.QueryString["type"] == "realization"), que.IsUnitDosePrescription == false);

                if (!string.IsNullOrEmpty(Request.QueryString["mode"]) && Request.QueryString["mode"] == "pos")
                    que.Where(que.IsPos.IsNotNull(), que.IsPos == true);
                else
                    que.Where(que.Or(que.IsPos.IsNull(), que.IsPos == false));

                if (Request.QueryString["type"] == "realization" && AppSession.Parameter.IsPrescOrderHandlingBasedOnDispensary)
                {
                    if (Request.QueryString["rt"] == "ipr")
                        que.Where(que.ServiceUnitID == AppSession.Parameter.ServiceUnitPharmacyID);
                    else
                        que.Where(que.ServiceUnitID != AppSession.Parameter.ServiceUnitPharmacyID);
                }
                else
                {
                    var reg = new RegistrationQuery("b");
                    que.InnerJoin(reg).On(que.RegistrationNo == reg.RegistrationNo);
                    if (Request.QueryString["rt"] == "opr")
                        que.Where(reg.SRRegistrationType != AppConstant.RegistrationType.InPatient);
                    else
                        que.Where(reg.SRRegistrationType == AppConstant.RegistrationType.InPatient);
                }

                if (!AppSession.Parameter.IsMoveRecordOnPrescSalesIncludeVoid)
                    que.Where(que.IsVoid == false);

                que.OrderBy(que.PrescriptionNo.Descending);
            }

            var entity = new TransPrescription();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        private string GetNewPrescriptionNo(bool isUnitDose)
        {
            if (isUnitDose)
                _autoNumber = Helper.GetNewAutoNumber(txtPrescriptionDate.SelectedDate.Value.Date, AppEnum.AutoNumber.UnitDoseNo);

            else
                _autoNumber = Helper.GetNewAutoNumber(txtPrescriptionDate.SelectedDate.Value.Date,
                                                  Request.QueryString["rt"] == "opr"
                                                      ? AppEnum.AutoNumber.PrescOpNo
                                                      : AppEnum.AutoNumber.PrescIpNo);

            return _autoNumber.LastCompleteNumber;
        }

        private string GetNewRegistrationNo()
        {
            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(cboServiceUnitID.SelectedValue);
            _autoNumberReg = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, BusinessObject.Reference.TransactionCode.Registration,
                unit.DepartmentID);
            return _autoNumberReg.LastCompleteNumber;
        }

        #region Record Detail Method Function TransPrescriptionItem

        private void RefreshCommandItemTransPrescriptionItem(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdTransPrescriptionItem.Columns[0].Visible = isVisible;
            grdTransPrescriptionItem2.Columns[0].Visible = isVisible;

            grdTransPrescriptionItem.Columns.FindByUniqueName("Etiquette").Visible = !isVisible;
            grdTransPrescriptionItem2.Columns.FindByUniqueName("Etiquette").Visible = !isVisible;

            grdTransPrescriptionItem.Columns.FindByUniqueName("Text").Visible = !isVisible;
            grdTransPrescriptionItem2.Columns.FindByUniqueName("Text").Visible = !isVisible;

            grdTransPrescriptionItem.Columns[grdTransPrescriptionItem.Columns.Count - 1].Visible = isVisible && Request.QueryString["type"] != "udd";
            grdTransPrescriptionItem2.Columns[grdTransPrescriptionItem2.Columns.Count - 1].Visible = isVisible && Request.QueryString["type"] != "udd";
            //grdTransPrescriptionItem.Columns[grdTransPrescriptionItem.Columns.Count - 2].Visible = OnGetStatusMenuVoid();
            //grdTransPrescriptionItem.Columns[grdTransPrescriptionItem.Columns.Count - 3].Visible = !OnGetStatusMenuApproval();

            //grdTransPrescriptionItem.MasterTableView.CommandItemDisplay = isVisible && Request.QueryString["type"] != "udd" ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //grdTransPrescriptionItem2.MasterTableView.CommandItemDisplay = isVisible && Request.QueryString["type"] != "udd" ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            // UDD tetap bisa menambah item lain, untuk kasus menambah bahan pendukung
            grdTransPrescriptionItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            grdTransPrescriptionItem2.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;


            if (oldVal != AppEnum.DataMode.Read) TransPrescriptionItems = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
            {
                grdTransPrescriptionItem.Rebind();
                grdTransPrescriptionItem2.Rebind();
            }
            else
            {
                if (isVisible)
                {
                    grdTransPrescriptionItem.Rebind();
                    grdTransPrescriptionItem2.Rebind();
                }
            }
        }

        private TransPrescriptionItemCollection TransPrescriptionItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collTransPrescriptionItem" + UniqueID() + hdnPageId.Value];
                    if (obj != null)
                        return ((TransPrescriptionItemCollection)(obj));
                }

                TransPrescriptionItemCollection coll;
                if (string.IsNullOrWhiteSpace(Request.QueryString["pno"]) && IsUddMode && DataModeCurrent == AppEnum.DataMode.New)
                    // New prescription with load record from UDD
                    coll = LoadFromUddItem();
                else
                    coll = LoadFromTransactionHist();

                Session["collTransPrescriptionItem" + UniqueID() + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collTransPrescriptionItem" + UniqueID() + hdnPageId.Value] = value; }
        }

        #region LoadFromUdd
        private TransPrescriptionItemCollection LoadFromUddItem()
        {
            var transactionDate = DateTime.Today;
            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regno"]);

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);

            // Load untuk ambil schemanya saja
            var coll = new TransPrescriptionItemCollection();
            var query = new TransPrescriptionItemQuery("a");
            query.Select
    (
        query,
        "<'' as refToItem_ItemName>",
        "<'' as refToItem_ItemInterventionName>",
        "<'' as refToTransPrescriptionItem_Total>",
        "<(a.ParentNo + a.SequenceNo) as ORDERKEY>",
        "<'' as refToEmbalace_EmbalaceLabel>",
        "<'' as refToConsumeMethod_SRConsumeMethodName>",
        "<'' as refToConsumeUnit_SRConsumeUnitName>",
        "<'0' as refToIpm_IsHam>",
        "<'' as refToItem_FornasRestrictionNotes>",
        "<'' as refTo_CombinedNotes>"
                );
            query.Where(query.PrescriptionNo == "1");
            coll.Load(query);

            //Load UDD Template Outstanding
            var uddItemQ = new UddItemQuery("udi");

            // Exist
            var tp = new TransPrescriptionQuery("tp");
            var tpi = new TransPrescriptionItemQuery("tpi");
            tp.Select(tpi.ItemID);
            tp.es.Top = 1;
            tp.InnerJoin(tpi).On(tp.PrescriptionNo == tpi.PrescriptionNo);
            tp.Where(tp.RegistrationNo == RegistrationNo,
                tp.PrescriptionDate.Date() == DateTime.Now.Date,
                tp.IsUnitDosePrescription == true,
                tpi.ItemID == uddItemQ.ItemID,
                tp.LocationID == uddItemQ.LocationID,
                tpi.SRConsumeMethod == uddItemQ.SRConsumeMethod,
                tpi.ConsumeQty == uddItemQ.ConsumeQty,
                tpi.SRConsumeUnit == uddItemQ.SRConsumeUnit
                );
            if (AppSession.Parameter.IsFilterPrescUddListOnlyWithValidTx)
                tp.Where(tp.IsVoid != true);


            uddItemQ.Where(uddItemQ.RegistrationNo == Request.QueryString["regno"],
                uddItemQ.LocationID == Request.QueryString["loc"],
                uddItemQ.ParamedicID == Request.QueryString["parid"],
                uddItemQ.IsStop == false,
                uddItemQ.ItemID.NotIn(tp)
                );

            uddItemQ.OrderBy(uddItemQ.SequenceNo.Ascending);


            var uddItems = new UddItemCollection();
            uddItems.Load(uddItemQ);

            // Add to TransPrescriptionItemCollection
            foreach (UddItem uddItem in uddItems)
            {
                var prescItem = coll.AddNew();

                prescItem.PrescriptionNo = txtPrescriptionNo.Text;
                prescItem.SequenceNo = uddItem.SequenceNo;
                prescItem.ParentNo = uddItem.ParentNo;
                prescItem.IsRFlag = string.IsNullOrEmpty(uddItem.ParentNo);
                prescItem.IsCompound = uddItem.IsCompound;
                prescItem.ItemID = uddItem.ItemID;

                var item = new Item();
                item.LoadByPrimaryKey(prescItem.ItemID);
                prescItem.ItemName = item.ItemName;

                prescItem.ItemInterventionID = string.Empty;
                prescItem.ItemInterventionName = string.Empty;
                prescItem.SRItemUnit = uddItem.SRItemUnit;
                prescItem.SRDosageUnit = uddItem.SRDosageUnit;


                // Cost Price
                decimal costPrice = 0;
                var fornasRestrictionNotes = string.Empty;
                if (item.SRItemType == ItemType.Medical)
                {
                    var ipm = new ItemProductMedic();
                    ipm.LoadByPrimaryKey(prescItem.ItemID);
                    costPrice = ipm.CostPrice ?? 0;
                    fornasRestrictionNotes = string.IsNullOrEmpty(ipm.FornasRestrictionNotes) ? string.Empty : ipm.FornasRestrictionNotes;
                }
                else if (item.SRItemType == ItemType.NonMedical)
                {
                    var ipn = new ItemProductNonMedic();
                    ipn.LoadByPrimaryKey(prescItem.ItemID);

                    costPrice = ipn.CostPrice ?? 0;
                }
                else
                {
                    var ipk = new ItemKitchen();
                    ipk.LoadByPrimaryKey(prescItem.ItemID);

                    costPrice = ipk.CostPrice ?? 0;
                }

                prescItem.CostPrice = costPrice;
                prescItem.FornasRestrictionNotes = fornasRestrictionNotes;
                prescItem.CombinedNotes = string.Empty;

                //Price
                decimal price = 0;
                var isCompound = (uddItem.IsCompound ?? false);
                if (isCompound && AppSession.Parameter.RecipeMarginValueCompound != 0)
                {
                    price = Helper.Tariff.GetItemTariffNonMargin(grr.SRTariffType, transactionDate,
                                                                                         reg.ChargeClassID, prescItem.ItemID, isCompound,
                                                                                        prescItem.SRItemUnit);
                    price += (AppSession.Parameter.RecipeMarginValueCompound / 100) * price;
                }
                else
                {
                    price = Helper.Tariff.GetItemTariff(grr.SRTariffType, transactionDate, reg.ChargeClassID,
                        prescItem.ItemID, isCompound, prescItem.SRItemUnit, reg.GuarantorID, reg.SRRegistrationType);
                }
                prescItem.Price = price;
                prescItem.InitialPrice = prescItem.Price;

                double recipeAmount;
                var recipe = new TransPrescription();
                if (uddItem.IsCompound ?? false)
                    recipeAmount = recipe.RecipeAmount(Request.QueryString["mode"], reg.GuarantorID, uddItem.ItemID, 1, uddItem.ParentNo, "1", true);
                //recipeAmount = PrescriptionSalesItemDetail.RecipeForCompound(Request.QueryString["mode"], uddItem.ParentNo, "1");
                else
                    recipeAmount = recipe.RecipeAmount(Request.QueryString["mode"], reg.GuarantorID, uddItem.ItemID, 1, uddItem.ParentNo, "1", false);
                //recipeAmount = PrescriptionSalesItemDetail.RecipeForNonCompound(Request.QueryString["mode"], uddItem.ItemID, 1);

                prescItem.RecipeAmount = Convert.ToDecimal(recipeAmount);
                prescItem.DiscountAmount = 0;
                prescItem.Total = ((prescItem.ResultQty ?? 0) * ((prescItem.Price ?? 0) - (prescItem.DiscountAmount ?? 0))) + (prescItem.RecipeAmount ?? 0) + (prescItem.EmbalaceAmount ?? 0);
                prescItem.EmbalaceID = uddItem.EmbalaceID;

                decimal embalaceAmount;
                if (string.IsNullOrEmpty(uddItem.EmbalaceID))
                    embalaceAmount = 0;
                else if ((uddItem.IsCompound ?? false) && !string.IsNullOrEmpty(uddItem.ParentNo))
                    embalaceAmount = 0;
                else
                {
                    var emb = new Embalace();
                    emb.LoadByPrimaryKey(uddItem.EmbalaceID);
                    embalaceAmount = emb.EmbalaceFeeAmount ?? 0;
                }
                prescItem.EmbalaceAmount = embalaceAmount;


                prescItem.SRDiscountReason = string.Empty;
                prescItem.Notes = uddItem.Notes;
                prescItem.SRConsumeMethod = uddItem.SRConsumeMethod;
                prescItem.DosageQty = uddItem.DosageQty;
                prescItem.EmbalaceQty = uddItem.EmbalaceQty;
                prescItem.EmbalaceLabel = string.Empty; // uddItem.EmbalaceLabel;
                prescItem.IterText = string.Empty;
                prescItem.ConsumeQty = uddItem.ConsumeQty;
                prescItem.SRConsumeUnit = uddItem.SRConsumeUnit;
                prescItem.Acpcdc = uddItem.AcPcDc;

                //// Check Consume Method
                //var cm = new ConsumeMethod();
                //var qtyPerDay = 1;
                //if (cm.LoadByPrimaryKey(uddItem.SRConsumeMethod) && cm.IterationQty > 0)
                //    qtyPerDay = cm.IterationQty ?? 1;

                //prescItem.TakenQty = qtyPerDay;
                //prescItem.ResultQty = qtyPerDay; 
                //prescItem.PrescriptionQty = qtyPerDay;

                // Ambil qty yg dientry dokter
                prescItem.ItemQtyInString = uddItem.ItemQtyInString;
                prescItem.ResultQty = uddItem.PrescriptionQty;
                prescItem.TakenQty = uddItem.PrescriptionQty;
                prescItem.PrescriptionQty = uddItem.PrescriptionQty;


                var cm = new ConsumeMethod();
                cm.LoadByPrimaryKey(uddItem.SRConsumeMethod);
                prescItem.SRConsumeMethodName = cm.SygnaText + " (" + cm.SRConsumeMethodName + ")"; prescItem.OrderText = string.Empty;


                var lineAmt = prescItem.ResultQty * prescItem.Price + prescItem.RecipeAmount + prescItem.EmbalaceAmount;
                lineAmt = Helper.Rounding(Convert.ToDecimal(lineAmt), AppEnum.RoundingType.Prescription);

                prescItem.LineAmount = lineAmt;

                //--- default value ---
                prescItem.IsUsingDosageUnit = false;
                prescItem.FrequencyOfDosing = 0;
                prescItem.DosingPeriod = string.Empty;
                prescItem.NumberOfDosage = 0;
                prescItem.DurationOfDosing = 0;
                prescItem.SRMedicationRoute = string.Empty;
                prescItem.ConsumeMethod = string.Empty;
                prescItem.IsCalcPercentage = false;
                prescItem.IsUseSweetener = false;
                prescItem.SweetenerAmount = 0;
                prescItem.IsPendingDelivery = false;
                prescItem.DaysOfUsage = 0;
                prescItem.Qty23Days = 0;

                if (Helper.GuarantorBpjsCasemix.Contains(reg.GuarantorID) && AppSession.Parameter.CasemixValidationRegistrationType.Contains(reg.SRRegistrationType))
                    prescItem.IsCasemixApproved = Helper.IsCasemixApproved(prescItem.ItemID, prescItem.ResultQty ?? 0, reg.RegistrationNo, prescItem.PrescriptionNo, reg.GuarantorID, true);
                else
                    prescItem.IsCasemixApproved = true;
            }
            return coll;
        }

        #endregion
        private TransPrescriptionItemCollection LoadFromTransactionHist()
        {
            var coll = new TransPrescriptionItemCollection();
            //coll.CreateColumnsForBinding("Total2");

            var query = new TransPrescriptionItemQuery("a");
            var qItem = new ItemQuery("b");
            var qItemI = new ItemQuery("c");

            var emb = new EmbalaceQuery("x");
            var cons = new ConsumeMethodQuery("cm");
            var consunit = new AppStandardReferenceItemQuery("cu");
            var gconsunit = new AppStandardReferenceItemQuery("gcu");
            var acdcpc = new AppStandardReferenceItemQuery("acdcpc");
            var ipm = new ItemProductMedicQuery("ipm");
            var ipnm = new ItemProductMedicQuery("ipnm");

            var total = new esQueryItem(query, "Total", esSystemType.Decimal);
            total = query.ResultQty * (query.Price - query.DiscountAmount);

            query.Select
                (
                    query,
                    qItem.ItemName.As("refToItem_ItemName"),
                    qItemI.ItemName.As("refToItem_ItemInterventionName"),
                    total.As("refToTransPrescriptionItem_Total"),
                    "<(a.ParentNo + a.SequenceNo) as ORDERKEY>",
                    emb.EmbalaceLabel.As("refToEmbalace_EmbalaceLabel"),
                    "<COALESCE(cm.SRConsumeMethodName,'') + ' ' + COALESCE(acdcpc.ItemName,'') as refToConsumeMethod_SRConsumeMethodName>",
                    @"<ISNULL(cu.ItemName, gcu.ItemName) AS 'refToConsumeUnit_SRConsumeUnitName'>",
                    //ipm.IsHam.Coalesce("0").As("refToIpm_IsHam"), --> u/ type boolean gak bisa pake sintaks ini, gak dapat datanya
                    @"<ISNULL(ipm.IsHam, 0) AS 'refToIpm_IsHam'>",
                    @"<ISNULL(ipm.FornasRestrictionNotes, '') AS 'refToItem_FornasRestrictionNotes'>",
                    @"<CASE WHEN ISNULL(a.CasemixNotes, '') = '' THEN '' ELSE ' (Casemix: ' + a.CasemixNotes +')' END AS 'refTo_CombinedNotes'>"
                );
            query.InnerJoin(qItem).On(query.ItemID == qItem.ItemID);
            query.LeftJoin(qItemI).On(query.ItemInterventionID == qItemI.ItemID);
            query.LeftJoin(emb).On(query.EmbalaceID == emb.EmbalaceID);
            query.LeftJoin(cons).On(query.SRConsumeMethod == cons.SRConsumeMethod);
            query.LeftJoin(consunit).On(query.SRConsumeUnit == consunit.ItemID &&
                                        consunit.StandardReferenceID == AppEnum.StandardReference.DosageUnit);
            query.LeftJoin(gconsunit).On(query.SRConsumeUnit == gconsunit.ItemID &&
                                        gconsunit.StandardReferenceID == AppEnum.StandardReference.GlobalConsumeUnit);
            query.LeftJoin(acdcpc).On(query.Acpcdc == acdcpc.ItemID &&
                                        acdcpc.StandardReferenceID == AppEnum.StandardReference.MedicationConsume);
            query.LeftJoin(ipm).On(query.ItemID == ipm.ItemID);
            query.LeftJoin(ipnm).On(query.ItemID == ipnm.ItemID);

            query.Where(query.PrescriptionNo == txtPrescriptionNo.Text);
            query.OrderBy("ORDERKEY", esOrderByDirection.Ascending);
            coll.Load(query);

            // Utk tombol High ALert
            foreach (var item in coll.Where(i => i.IsHam == true))
            {
                _isContainHighAlert = true;
                break;
            }
            return coll;
        }
        private void PopulateTransPrescriptionItemGrid()
        {
            //Display Data Detail
            TransPrescriptionItems = null; //Reset Record Detail
            grdTransPrescriptionItem.DataSource = TransPrescriptionItems; //Requery
            grdTransPrescriptionItem.MasterTableView.IsItemInserted = false;
            grdTransPrescriptionItem.MasterTableView.ClearEditItems();
            grdTransPrescriptionItem.DataBind();

            grdTransPrescriptionItem2.DataSource = TransPrescriptionItems; //Requery
            grdTransPrescriptionItem2.MasterTableView.IsItemInserted = false;
            grdTransPrescriptionItem2.MasterTableView.ClearEditItems();
            grdTransPrescriptionItem2.DataBind();

            CostCalculations = null;
            CostCalculations.LoadAll();
        }

        protected void grdTransPrescriptionItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;
            if (!grd.Visible) return; //Method dipanggil dari 2 grid yg berbeda shg harus dicek jika visible maka baru dijalankan (Handono 231123)

            grd.DataSource = TransPrescriptionItems;

            if (Request.QueryString["mode"] == "direct")
            {
                cboGuarantorID.Enabled = !TransPrescriptionItems.Any();
                cboPatientID.Enabled = !TransPrescriptionItems.Any();
            }
        }

        private List<string> _patiendIds = null;
        private List<string> PatientIds
        {
            get
            {
                if (_patiendIds == null)
                    _patiendIds = Patient.PatientRelateds(cboPatientID.SelectedValue);
                return _patiendIds;
            }
        }
        protected void grdTransPrescriptionItem_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (!((RadGrid)sender).Visible) return; //Method dipanggil dari 2 grid yg berbeda shg harus dicek jika visible maka baru dijalankan (Handono 231123)
            if (e.Item is GridDataItem)
            {

                if (AppSession.Parameter.IsPrescriptionLoadLastBought)
                {
                    var dataItem = e.Item as GridDataItem;
                    TransPrescriptionItem tpi = (TransPrescriptionItem)e.Item.DataItem;

                    //var prev = PrescriptionSalesItemDetail.CekPrevBuy(PatientIds,
                    //    txtPrescriptionNo.Text, tpi.ItemID, tpi.ItemName, tpi.ItemInterventionID, tpi.ItemInterventionName);

                    //if (!string.IsNullOrEmpty(prev.ItemName))
                    //{
                    //    dataItem["TemplateItemName"].ForeColor = prev.Color;
                    //    var tooltip = string.Format("Previous Transaction @ {3} {5}Item: {0} {5}Qty: {1} {2} {4}",
                    //        prev.ItemName, prev.Qty, prev.SRItemUnit, prev.Date,
                    //        prev.DaysOfUsage > 0 ? ("for " + prev.DaysOfUsage.ToString() + " day(s)") : "",
                    //        Environment.NewLine);
                    //    dataItem["TemplateItemName"].ToolTip = tooltip;
                    //}

                    // Karena program diatas jika datanya besar akan menghambat proses loading sehingga
                    // diganti menggunakan RadToolTipManager yg menggunakan webservice untuk load datanya (Handono 231124)
                    var val = string.Format("{0}|{1}|{2}|{3}|{4}|{5}", string.Join(",", PatientIds), txtPrescriptionNo.Text, tpi.ItemID, tpi.ItemName, tpi.ItemInterventionID, tpi.ItemInterventionName);
                    toolTipMgr.TargetControls.Add(dataItem["TemplateItemName"].ClientID, val, true);
                }

            }
        }

        protected void grdTransPrescriptionItem_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (!((RadGrid)sender).Visible) return; //Method dipanggil dari 2 grid yg berbeda shg harus dicek jika visible maka baru dijalankan (Handono 231123)

            if (e.Item is GridDataItem)
            {
                TransPrescriptionItem item = TransPrescriptionItems[e.Item.DataSetIndex];
                if (item != null)
                {
                    if (item.IsVoid ?? false)
                    {
                        for (int i = 0; i < e.Item.Cells.Count; i++)
                        {
                            if (i > 0 && i < e.Item.Cells.Count)
                                e.Item.Cells[i].Font.Strikeout = true;
                        }
                    }
                }
            }
        }

        protected void grdTransPrescriptionItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            var sequenceNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][TransPrescriptionItemMetadata.ColumnNames.SequenceNo]);
            var entity = FindTransPrescriptionItem(sequenceNo);
            if (entity != null)
            {
                SetEntityValue(entity, e);

                if ((entity.IsCompound ?? false) && string.IsNullOrEmpty(entity.ParentNo))
                {
                    foreach (var row in TransPrescriptionItems.Where(row => (row.IsCompound ?? false) &&
                                                                            (row.ParentNo == entity.SequenceNo)))
                    {
                        row.EmbalaceID = entity.EmbalaceID;
                        row.SRConsumeMethod = entity.SRConsumeMethod;
                        row.PrescriptionQty = entity.PrescriptionQty;
                        row.ItemQtyInString = entity.ItemQtyInString;
                        row.EmbalaceQty = entity.EmbalaceQty;

                        var qty = Convert.ToDecimal(new Fraction(row.ItemQtyInString));
                        var dosing = Convert.ToDecimal(new Fraction(row.DosageQty));

                        var item = new ItemProductMedic();
                        if (item.LoadByPrimaryKey(row.ItemInterventionID == string.Empty ? row.ItemID : row.ItemInterventionID))
                        {
                            if (item.SRItemUnit == row.SRDosageUnit)
                            {
                                row.TakenQty = dosing * qty;
                                row.ResultQty = dosing * qty;
                            }
                            else
                            {
                                if (item.SRDosageUnit == row.SRDosageUnit)
                                {
                                    row.TakenQty = (dosing / item.Dosage) * qty;
                                    row.ResultQty = (dosing / item.Dosage) * qty;
                                }
                                else
                                {
                                    var detail = new ItemProductDosageDetailCollection();
                                    detail.Query.Where(detail.Query.ItemID == item.ItemID);
                                    detail.LoadAll();

                                    var dosage = detail.SingleOrDefault(d => d.SRDosageUnit == row.SRDosageUnit);
                                    if (dosage != null)
                                    {
                                        row.TakenQty = (dosing / dosage.Dosage) * qty;
                                        row.ResultQty = (dosing / dosage.Dosage) * qty;
                                    }
                                }
                            }
                        }
                        else
                        {
                            row.TakenQty = qty;
                            row.ResultQty = qty;
                        }

                        if (!(item.IsActualDeduct ?? false))
                        {
                            var x = (row.TakenQty ?? 0) - Math.Floor(row.TakenQty ?? 0);

                            var deduct = new ItemProductDeductionDetail();
                            deduct.Query.Where(string.Format("<{0} BETWEEN MinAmount AND MaxAmount>", x));
                            if (deduct.Query.Load())
                            {
                                row.TakenQty = decimal.Truncate(Convert.ToDecimal(row.TakenQty ?? 0)) + deduct.DeductionAmount;
                                row.ResultQty = decimal.Truncate(Convert.ToDecimal(row.ResultQty ?? 0)) + deduct.DeductionAmount;
                            }
                        }

                        var lineAmt = (row.ResultQty * (row.Price + -row.DiscountAmount)) + row.RecipeAmount;
                        if (lineAmt <= 0)
                            lineAmt = (row.ResultQty * row.Price) + row.RecipeAmount;
                        row.LineAmount = Helper.Rounding(Convert.ToDecimal(lineAmt), AppEnum.RoundingType.Prescription);
                        row.Total = row.LineAmount ?? 0;
                        //row.LineAmount = (row.ResultQty * (row.Price - row.DiscountAmount)) + row.RecipeAmount;
                        //row.Total = Convert.ToDecimal((row.ResultQty * (row.Price - row.DiscountAmount)) + row.RecipeAmount);

                        row.ConsumeQty = entity.ConsumeQty;
                    }
                }
            }
        }

        protected void grdTransPrescriptionItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            var sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][TransPrescriptionItemMetadata.ColumnNames.SequenceNo]);

            if (sequenceNo.Contains("d"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid", "alert('Invalid delete to system');", true);
                return;
            }


            // --- APOL delete kalau item ini sudah pernah dikirim (code == 200) ---
            if (Helper.IsApotekOnlineIntegration)
            {
                try
                {
                    // header APOL
                    var apol = new BpjsApol();
                    apol.Query.Where(
                        apol.Query.PrescriptionNo == txtPrescriptionNo.Text,
                        apol.Query.REFASALSJP == txtBpjsSepNo.Text
                    );

                    if (apol.Query.Load()
                        && !string.IsNullOrWhiteSpace(Convert.ToString(apol.NOAPOTIK))
                        && !string.IsNullOrWhiteSpace(Convert.ToString(apol.NORESEP)))
                    {
                        // detail APOL
                        var det = new BpjsApolDetail();
                        det.Query.Where(
                            det.Query.PrescriptionNo == apol.PrescriptionNo,
                            det.Query.SequenceNo == sequenceNo
                        );

                        if (det.Query.Load())
                        {
                            var meta = (det.MetadataCode ?? "").Trim();
                            
                            if (string.Equals(meta, "200", StringComparison.OrdinalIgnoreCase))
                            {
                                var kdobt = (det.KDOBT ?? "").Trim();
                                if (!string.IsNullOrEmpty(kdobt))
                                {
                                    // tipe obat
                                    var jns = (det.JNSROBT ?? "").Trim();
                                    // kalau racikan: "R.{NN/SeqNo}", kalau non: "N"
                                    var tipeObat = jns.StartsWith("R.", StringComparison.OrdinalIgnoreCase) ? "NSROBT" : "N";

                                    var svc = new Temiang.Avicenna.Common.BPJS.Apotek.Service();
                                    var req = new Temiang.Avicenna.Common.BPJS.Apotek.PelayananObat.HapusPelayananObat.Request.Root
                                    {
                                        Nosepapotek = Convert.ToString(apol.NOAPOTIK).Trim(),
                                        Noresep = Convert.ToString(apol.NORESEP).Trim(),
                                        Kodeobat = kdobt,
                                        Tipeobat = tipeObat
                                    };

                                    try
                                    {
                                        var resp = svc.HapusPelayananObat(req);
                                        var code = (resp?.Metadata?.Code ?? "").ToString();
                                        var msg = resp?.Metadata?.Message ?? "";

                                        if (!string.Equals(code, "200", StringComparison.OrdinalIgnoreCase))
                                        {
                                            // API validasi, misal udah diverif - alert
                                            det.MetadataCode = $"DEL-{code}";
                                            det.MetadataMessage = string.IsNullOrWhiteSpace(msg) ? "Gagal hapus di APOL." : msg;
                                            det.LastUpdateDateTime = DateTime.Now;
                                            det.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                            det.Save();

                                            ScriptManager.RegisterStartupScript(
                                                this, GetType(),
                                                "apolDelNok_" + Guid.NewGuid().ToString("N"),
                                                $"alert('APOL: {System.Web.HttpUtility.JavaScriptStringEncode(det.MetadataMessage)}');",
                                                true
                                            );
                                            return;
                                        }

                                        det.MetadataCode = "DEL-200";
                                        det.MetadataMessage = string.IsNullOrWhiteSpace(msg) ? "Deleted" : msg;
                                        det.LastUpdateDateTime = DateTime.Now;
                                        det.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                        det.Save();
                                    }
                                    catch (System.Net.WebException wex)
                                    {
                                        var http = wex.Response as System.Net.HttpWebResponse;
                                        var sc = http != null ? (int)http.StatusCode : 0;

                                        det.MetadataCode = $"DEL-{sc}";
                                        det.MetadataMessage = sc == 504
                                            ? "Gateway Timeout (504) saat hapus obat di APOL"
                                            : $"HTTP Error {sc} saat hapus obat di APOL";
                                        det.LastUpdateDateTime = DateTime.Now;
                                        det.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                        det.Save();

                                        ScriptManager.RegisterStartupScript(
                                            this, GetType(),
                                            "apolDelErr_" + Guid.NewGuid().ToString("N"),
                                            $"alert('APOL: {System.Web.HttpUtility.JavaScriptStringEncode(det.MetadataMessage)}');",
                                            true
                                        );
                                        return;
                                    }
                                    catch (Exception ex)
                                    {
                                        det.MetadataCode = "DEL-CATCH";
                                        det.MetadataMessage = ex.Message;
                                        det.LastUpdateDateTime = DateTime.Now;
                                        det.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                        det.Save();

                                        ScriptManager.RegisterStartupScript(
                                            this, GetType(),
                                            "apolDelEx_" + Guid.NewGuid().ToString("N"),
                                            $"alert('APOL: {System.Web.HttpUtility.JavaScriptStringEncode(ex.Message)}');",
                                            true
                                        );
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                det.MetadataCode = "DEL-LOCAL";
                                det.MetadataMessage = "Dihapus lokal (belum pernah terkirim ke APOL).";
                                det.LastUpdateDateTime = DateTime.Now;
                                det.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                det.Save();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    var log = new WebServiceAPILog
                    {
                        DateRequest = DateTime.Now,
                        IPAddress = "",
                        UrlAddress = "DEL-DET-APOL",
                        Params = "",
                        Response = ex.Message,
                        Totalms = 0
                    };
                    log.Save();
                }
            }

            var entity = FindTransPrescriptionItem(sequenceNo);
            if (entity != null)
                entity.MarkAsDeleted();

            cboServiceUnitID.Enabled = !TransPrescriptionItems.Any() || (IsRealizationMode && AppSession.Parameter.IsEnabledDispensaryOnPrescriptionOrderRealization);
            cboLocationID.Enabled = !TransPrescriptionItems.Any() || (IsRealizationMode && AppSession.Parameter.IsEnabledDispensaryOnPrescriptionOrderRealization);
            if (Request.QueryString["mode"] == "direct")
            {
                cboGuarantorID.Enabled = !TransPrescriptionItems.Any();
                cboPatientID.Enabled = !TransPrescriptionItems.Any();
            }

        }

        protected void grdTransPrescriptionItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = TransPrescriptionItems.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdTransPrescriptionItem.Rebind();
            grdTransPrescriptionItem2.Rebind();

            cboServiceUnitID.Enabled = !TransPrescriptionItems.Any() || (IsRealizationMode && AppSession.Parameter.IsEnabledDispensaryOnPrescriptionOrderRealization);
            cboLocationID.Enabled = !TransPrescriptionItems.Any() || (IsRealizationMode && AppSession.Parameter.IsEnabledDispensaryOnPrescriptionOrderRealization);
            if (Request.QueryString["mode"] == "direct")
            {
                cboGuarantorID.Enabled = !TransPrescriptionItems.Any();
                cboPatientID.Enabled = !TransPrescriptionItems.Any();
            }
        }

        private TransPrescriptionItem FindTransPrescriptionItem(String sequenceNo)
        {
            var coll = TransPrescriptionItems;
            TransPrescriptionItem retEntity = null;
            foreach (var rec in coll)
            {
                if (rec.SequenceNo.Equals(sequenceNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(TransPrescriptionItem entity, GridCommandEventArgs e)
        {
            var userControl = (PrescriptionSalesItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.PrescriptionNo = txtPrescriptionNo.Text;
                entity.SequenceNo = userControl.SequenceNo;
                entity.ParentNo = userControl.ParentNo;
                entity.IsRFlag = string.IsNullOrEmpty(userControl.ParentNo);
                entity.IsCompound = userControl.IsCompound;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.ItemInterventionID = userControl.ItemInterventionID;
                entity.ItemInterventionName = userControl.ItemInterventionName;
                entity.SRItemUnit = userControl.ItemUnit;
                entity.SRDosageUnit = userControl.SRDosageUnit;
                entity.PrescriptionQty = userControl.PrescriptionQty;
                entity.TakenQty = userControl.TakenQty;
                entity.ResultQty = userControl.ResultQty;
                entity.CostPrice = userControl.CostPrice;
                entity.InitialPrice = userControl.InitialPrice;
                entity.Price = userControl.Price;
                entity.RecipeAmount = userControl.RecipeAmount;
                entity.DiscountAmount = userControl.DiscountAmount;
                entity.Total = ((entity.ResultQty ?? 0) * ((entity.Price ?? 0) - (entity.DiscountAmount ?? 0))) + (entity.RecipeAmount ?? 0) + (entity.EmbalaceAmount ?? 0);
                entity.EmbalaceID = userControl.EmbalaceID;
                entity.EmbalaceAmount = userControl.EmbalaceAmount;
                entity.LineAmount = userControl.LineAmount;
                entity.SRDiscountReason = userControl.SRDiscountReason;
                entity.Notes = userControl.Notes;
                entity.SRConsumeMethod = userControl.SRConsumeMethod;
                entity.DosageQty = userControl.NumberOfDosage;
                entity.EmbalaceQty = userControl.EmbalaceQty;
                entity.EmbalaceLabel = userControl.EmbalaceLabel;
                entity.SRConsumeMethodName = string.Format("{0} {1}", userControl.SRConsumeMethodName, userControl.AcPcDcName);
                entity.OrderText = userControl.Order;
                entity.IterText = userControl.Iter;
                entity.ConsumeQty = userControl.QtyOfDosage;
                entity.SRConsumeUnit = userControl.SRConsumeUnit;
                entity.Acpcdc = userControl.AcPcDc;

                //--- default value ---
                entity.ItemQtyInString = userControl.ItemQtyInString;
                entity.IsUsingDosageUnit = false;
                entity.FrequencyOfDosing = 0;
                entity.DosingPeriod = string.Empty;
                entity.NumberOfDosage = 0;
                entity.DurationOfDosing = 0;
                entity.SRMedicationRoute = string.Empty;
                entity.ConsumeMethod = string.Empty;
                entity.IsCalcPercentage = false;
                entity.IsUseSweetener = false;
                entity.SweetenerAmount = 0;
                entity.IsPendingDelivery = userControl.IsPendingDelivery;
                entity.DaysOfUsage = userControl.DaysOfUsage.ToInt();
                entity.SRInterventionReason = userControl.SRInterventionReason;
                entity.IsCasemixApproved = userControl.IsCasemixApproved;
                entity.Qty23Days = userControl.Qty23Days;
                entity.FornasRestrictionNotes = userControl.FornasRestrictionNotes;
                entity.CombinedNotes = !string.IsNullOrEmpty(userControl.CasemixNotes) ? userControl.Notes + " (Casemix: " + userControl.CasemixNotes + ")" : userControl.Notes;

                var reg = new Registration();
                reg.LoadByPrimaryKey(txtRegistrationNo.Text);

                // cek kalo qty takennya nol, gak perlu ke casemix
                // kalo sx sudah di approved sama casemix, maka update-an selanjutnya tetap melihat status pertama dari casemix
                // tambah status UDD, Presc. Order, Sales Handling

                if (Helper.GuarantorBpjsCasemix.Contains(reg.GuarantorID) && AppSession.Parameter.CasemixValidationRegistrationType.Contains(reg.SRRegistrationType))
                {
                    entity.IsVoid = userControl.IsVoid;
                    if ((entity.IsCasemixApproved == false && entity.IsVoid == false) || (entity.IsCasemixApproved == true && string.IsNullOrEmpty(userControl.CasemixApprovedByUserID)))
                    {
                        if (entity.ResultQty == 0)
                            entity.IsCasemixApproved = true;
                        else
                        {
                            var itemId = (string.IsNullOrWhiteSpace(entity.ItemInterventionID) ? entity.ItemID : entity.ItemInterventionID);
                            entity.IsCasemixApproved = Helper.IsCasemixApproved(itemId, entity.ResultQty ?? 0, reg.RegistrationNo, entity.PrescriptionNo, reg.GuarantorID, true);
                        }
                    }
                }
                else
                    entity.IsCasemixApproved = true;

                if (Helper.IsApotekOnlineIntegration)
                {
                    string nosjp = null, noresep = null;
                    var apol = new BpjsApol();
                    apol.Query.Where(apol.Query.NosepKunjungan == txtBpjsSepNo.Text, apol.Query.PrescriptionNo == txtPrescriptionNo.Text);
                    if (apol.Query.Load())
                    {
                        nosjp = Convert.ToString(apol.NOAPOTIK)?.Trim();
                        noresep = Convert.ToString(apol.NORESEP)?.Trim();
                    }

                    string kdobt = null, nmobat = null;
                    var map = new ItemBridging();
                    map.Query.Where(
                        map.Query.ItemID == userControl.ItemID,
                        map.Query.SRBridgingType == AppEnum.BridgingType.APOTEKONLINE.ToString()
                    );
                    if (map.Query.Load()) { kdobt = map.BridgingID; nmobat = map.BridgingName; }

                    int signa2 = 0;
                    var cm = new ConsumeMethod();
                    cm.Query.Where(cm.Query.SRConsumeMethod == userControl.SRConsumeMethod);
                    if (cm.Query.Load()) signa2 = cm.IterationQty ?? 0;

                    // jns robit racikan: "R.{NN}" -> R.01
                    var seqStr = (Convert.ToString(userControl.SequenceNo) ?? "").Trim();
                    var m = System.Text.RegularExpressions.Regex.Match(seqStr, @"\d{1,2}(?!.*\d)");
                    string jnsrobt;
                    if (userControl.IsCompound == true)
                    {
                        var seq = Convert.ToString(userControl.SequenceNo) ?? "";
                        var lastDigit = seq.Reverse().FirstOrDefault(char.IsDigit);
                        jnsrobt = (lastDigit != default(char)) ? ("R.0" + lastDigit) : "R.01"; // fallback aman
                    }
                    else
                    {
                        jnsrobt = "N";
                    }
                    int ToInt(object o) { int v; return int.TryParse(Convert.ToString(o), out v) ? v : 0; }

                    // save detail (PENDING)
                    var det = new BpjsApolDetail();
                    det.Query.Where(det.Query.PrescriptionNo == txtPrescriptionNo.Text, det.Query.SequenceNo == userControl.SequenceNo);
                    var exists = det.Query.Load();
                    if (!exists) det.AddNew();

                    det.PrescriptionNo = txtPrescriptionNo.Text;
                    det.SequenceNo = userControl.SequenceNo;
                    det.ParentNo = userControl.ParentNo;
                    det.NOSJP = nosjp;
                    det.NORESEP = noresep;
                    det.JNSROBT = jnsrobt;
                    det.KDOBT = kdobt;
                    det.NMOBAT = nmobat;
                    det.SIGNA1OBT = ToInt(userControl.QtyOfDosage);
                    det.SIGNA2OBT = signa2;
                    det.PERMINTAAN = ToInt(userControl.PrescriptionQty); // penting utk racikan
                    det.JMLOBT = ToInt(userControl.TakenQty);
                    det.JHO = ToInt(userControl.DaysOfUsage);
                    det.CATKHSOBT = Convert.ToString(userControl.Notes);

                    det.MetadataCode = "PENDING";
                    det.MetadataMessage = "Menunggu Approval";
                    det.LastUpdateDateTime = DateTime.Now;
                    det.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    det.Save();
                }

            }
        }

        #endregion

        private string[] GuarantorBPJS
        {
            get
            {
                if (ViewState["bpjs"] != null) return (string[])ViewState["bpjs"];
                var grr = new GuarantorBridgingCollection();
                grr.Query.es.Distinct = true;
                grr.Query.Where(grr.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(), AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString()));
                if (grr.Query.Load()) ViewState["bpjs"] = grr.Select(g => g.GuarantorID).ToArray();
                else ViewState["bpjs"] = new string[] { string.Empty };

                return (string[])ViewState["bpjs"];
            }
        }

        protected string GetItemName(object isRFlag, object itemName)
        {
            if (isRFlag.Equals(true))
                return itemName.ToString();
            return "&nbsp;&nbsp;&nbsp;" + itemName.ToString();
        }

        protected override void OnMenuEditClick()
        {
            if (txtPrescriptionNo.Text == string.Empty)
            {
                OnPopulateEntryControl(new TransPrescription());

                txtRegistrationNo.Text = (string)ViewState["regno"];
                txtPrescriptionDate.SelectedDate = DateTime.Today;
                txtExecutionDate.SelectedDate = DateTime.Today;
                txtPrescriptionNo.Text = GetNewPrescriptionNo(chkUnitDose.Checked);

                if (Request.QueryString["md"] == "new")
                    PopulateRegistrationInformation(txtRegistrationNo.Text);

                hdnIsApproved.Value = false.ToString();
                hdnIsVoid.Value = false.ToString();
                hdnIsFromSOAP.Value = false.ToString();
            }

            if (hdnIsFromSOAP.Value.ToBoolean() && AppSession.Parameter.IsServiceUnitPrescriptionSalesDefaultEmpty)
            {
                cboServiceUnitID.Enabled = true;
                cboLocationID.Enabled = true;
            }
            else
            {
                cboServiceUnitID.Enabled = !TransPrescriptionItems.Any() || (IsRealizationMode && AppSession.Parameter.IsEnabledDispensaryOnPrescriptionOrderRealization);
                cboLocationID.Enabled = !TransPrescriptionItems.Any() || (IsRealizationMode && AppSession.Parameter.IsEnabledDispensaryOnPrescriptionOrderRealization);
            }
            if (Request.QueryString["mode"] == "direct")
                cboGuarantorID.Enabled = !TransPrescriptionItems.Any();
            else
                cboGuarantorID.Enabled = false;

            if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "realization")
                cboParamedicID.Enabled = false;

            if (Helper.IsApotekOnlineIntegration)
            {
                btnCariPasienSep.Enabled = true;
                txtRefAsalSJP.Enabled = true;
                txtPoliRSP.Enabled = true;
                cboJnsRsp.Enabled = true;
                txtNoResep.Enabled = true;
                txtKdDokter.Enabled = true;
                cboIterasi.Enabled = true;
            }

        }

        private CostCalculationCollection CostCalculations
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = ViewState["collCostCalculation" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                        return ((CostCalculationCollection)(obj));
                }

                var coll = new CostCalculationCollection();

                coll.Query.Where
                    (
                        coll.Query.TransactionNo == txtPrescriptionNo.Text,
                        coll.Query.RegistrationNo == txtRegistrationNo.Text
                    );
                coll.LoadAll();

                ViewState["collCostCalculation" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { ViewState["collCostCalculation" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        protected void cboPatientID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulatePatientInformation(e.Value);
            if (AppSession.Parameter.HealthcareInitialAppsVersion != "RSCH")
            {
                PopulateGuarantorFromPatientInformation(e.Value);
            }
        }

        protected void cboPatientID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var dtbPatient = (new PatientCollection()).PatientRegisterAble(e.Text, string.Empty, string.Empty, string.Empty, 30);
            cboPatientID.DataSource = dtbPatient;
            cboPatientID.DataBind();
        }

        protected void cboPatientID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PatientName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PatientID"].ToString();
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                if (!string.IsNullOrEmpty(Request.QueryString["mode"]))
                    txtRegistrationNo.Text = GetNewRegistrationNo();
            }
            else
                txtRegistrationNo.Text = string.Empty;
            ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, e.Value);
            cboLocationID.SelectedIndex = cboLocationID.Items.Count > 1 ? 1 : 0;


            // Untuk keperluan lookup item via webservice krn javascript tidak bisa ambil value dari combobox yg di disable
            hdnServiceUnitID.Value = e.Value;
            hdnLocationID.Value = cboLocationID.SelectedValue;
        }

        protected void cboLocationID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            hdnLocationID.Value = e.Value;
        }

        protected void cboGuarantorID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var grr = new Guarantor();
            grr.LoadByPrimaryKey(e.Value);

            cboEmployeeID.Items.Clear();
            cboEmployeeID.Text = string.Empty;
            cboGuarSRRelationship.SelectedValue = string.Empty;
            cboGuarSRRelationship.Text = string.Empty;

            string patId = Request.QueryString["mode"] == "direct"
                               ? cboPatientID.SelectedValue
                               : AppSession.Parameter.OTCPrescriptionPatientID;

            if (grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeEmployee)
            {
                var pat = new Patient();
                pat.LoadByPrimaryKey(patId);

                if (pat.PersonID != null)
                {
                    var empq = new PersonalInfoQuery();
                    empq.Where(empq.PersonID == pat.PersonID);
                    cboEmployeeID.DataSource = empq.LoadDataTable();
                    cboEmployeeID.DataBind();
                    cboEmployeeID.SelectedValue = pat.PersonID.ToString();
                }

                cboGuarSRRelationship.SelectedValue = pat.SREmployeeRelationship;

                cboEmployeeID.Enabled = !AppSession.Parameter.IsRADTLinkToHumanResourcesModul;
                cboGuarSRRelationship.Enabled = !AppSession.Parameter.IsRADTLinkToHumanResourcesModul;
            }
            else
            {
                string searchTextContain = string.Format("%{0}%", cboGuarantorID.SelectedValue);
                var pars = new AppParameterCollection();
                pars.Query.Where(pars.Query.ParameterID == "DependentsOfEmployeesGuarantorID",
                                 pars.Query.ParameterValue.Like(searchTextContain));
                pars.LoadAll();
                if (pars.Count > 0)
                {
                    cboEmployeeID.Enabled = true;
                    cboGuarSRRelationship.Enabled = true;
                }
                else
                {
                    cboEmployeeID.Enabled = !AppSession.Parameter.IsRADTLinkToHumanResourcesModul;
                    cboGuarSRRelationship.Enabled = !AppSession.Parameter.IsRADTLinkToHumanResourcesModul;
                }
            }
        }

        protected void cboEmployeeID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " +
                          ((DataRowView)e.Item.DataItem)["FirstName"].ToString() + " " +
                          ((DataRowView)e.Item.DataItem)["MiddleName"].ToString() + " " +
                          ((DataRowView)e.Item.DataItem)["LastName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void chkUnitDose_CheckedChanged(object sender, EventArgs e)
        {
            grdTransPrescriptionItem.Rebind();
            grdTransPrescriptionItem2.Rebind();

        }

        protected void chkIs23Days_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIs23Days.Checked)
            {
                var p = new Patient();
                if (p.LoadByPrimaryKey(cboPatientID.SelectedValue))
                    cboGuarantorID.SelectedValue = p.GuarantorID;
            }
            else
                PopulateGuarantorFromPatientInformation(cboPatientID.SelectedValue);
        }

        protected void chkIsSplitBill_CheckedChanged(object sender, EventArgs e)
        {
            chkIsCash.Checked = false;
            if (chkIsSplitBill.Checked)
            {
                var p = new Patient();
                if (p.LoadByPrimaryKey(cboPatientID.SelectedValue))
                    cboGuarantorID.SelectedValue = p.GuarantorID;
            }
            else
                PopulateGuarantorFromPatientInformation(cboPatientID.SelectedValue);
        }

        protected void chkIsCash_CheckedChanged(object sender, EventArgs e)
        {
            chkIsSplitBill.Checked = false;
            PopulateGuarantorFromPatientInformation(cboPatientID.SelectedValue);
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

        protected string GetItemWithInsufficientBalance(string prescriptionNo, string locationId)
        {
            var retVal = string.Empty;

            var query = new TransPrescriptionItemQuery("a");
            var ib = new ItemBalanceQuery("b");
            var i = new ItemQuery("c");
            query.InnerJoin(ib).On(query.ItemID == ib.ItemID && ib.LocationID == locationId);
            query.InnerJoin(i).On(query.ItemID == i.ItemID);
            query.Where(query.PrescriptionNo == prescriptionNo, query.ResultQty > ib.Balance,
                query.ItemInterventionID == string.Empty,
                query.Or(query.IsPendingDelivery.IsNull(), query.IsPendingDelivery == false)
                );
            query.Select(i.ItemName);
            DataTable dtb = query.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    if (retVal == string.Empty)
                        retVal = row["ItemName"].ToString();
                    else
                        retVal += ", " + row["ItemName"].ToString();
                }
            }
            // intervention
            var queryi = new TransPrescriptionItemQuery("a");
            var ibi = new ItemBalanceQuery("b");
            var ii = new ItemQuery("c");
            queryi.InnerJoin(ibi).On(queryi.ItemInterventionID == ibi.ItemID && ibi.LocationID == locationId);
            queryi.InnerJoin(ii).On(queryi.ItemInterventionID == ii.ItemID);
            queryi.Where(queryi.PrescriptionNo == prescriptionNo, queryi.ResultQty > ibi.Balance,
                queryi.ItemInterventionID != string.Empty,
                query.Or(query.IsPendingDelivery.IsNull(), query.IsPendingDelivery == false)
                );
            queryi.Select(ii.ItemName);
            DataTable dtbi = queryi.LoadDataTable();
            if (dtbi.Rows.Count > 0)
            {
                foreach (DataRow row in dtbi.Rows)
                {
                    if (retVal == string.Empty)
                        retVal = row["ItemName"].ToString();
                    else
                        retVal += ", " + row["ItemName"].ToString();
                }
            }

            return retVal;
        }

        private DataTable GetLabHistTable
        {
            get
            {
                if (ViewState["LabHistTable" + Request.UserHostName + hdnPageId.Value] == null)
                {
                    var dtb = new DataTable();
                    dtb.Columns.Add(new DataColumn("OrderLabNo", typeof(string)));
                    dtb.Columns.Add(new DataColumn("LabOrderCode", typeof(string)));
                    dtb.Columns.Add(new DataColumn("LabOrderSummary", typeof(string)));
                    dtb.Columns.Add(new DataColumn("Result", typeof(string)));
                    dtb.Columns.Add(new DataColumn("StandarValue", typeof(string)));
                    dtb.Columns.Add(new DataColumn("OrderLabTglOrder", typeof(DateTime)));
                    ViewState["LabHistTable" + Request.UserHostName + hdnPageId.Value] = dtb;
                    return dtb;
                }
                return (DataTable)ViewState["LabHistTable" + Request.UserHostName + hdnPageId.Value];
            }
        }

        private DataTable GetLabHistOrder
        {
            get
            {
                var dtb = GetLabHistTable;
                if (dtb.Rows.Count > 0) dtb.Rows.Clear();

                if (!AppSession.Parameter.IsUsingHisInterop)
                {
                    var result = new TransChargesItemCollection();
                    return result.LaboratoryResultByRegistrationNo(txtRegistrationNo.Text);
                }

                var hd = new TransChargesQuery("a");
                var reg = new RegistrationQuery("b");

                switch (AppSession.Parameter.HisInteropConfigName)
                {
                    case AppConstant.HIS_INTEROP.RSCH_LIS_INTEROP_CONNECTION_NAME:
                        var hasil = new BusinessObject.Interop.RSCH.VwHasilPasienQuery("hp");
                        hasil.InnerJoin(hd).On(hasil.OrderLabNo == hd.TransactionNo);
                        hasil.InnerJoin(reg).On(hd.RegistrationNo == reg.RegistrationNo);

                        //--start--
                        hasil.Select(
                            hasil.OrderLabNo,
                            hasil.CheckupResultFractionCode.As("LabOrderCode"),
                            hasil.CheckupResultFractionName.As("LabOrderSummary"),
                            "<CASE WHEN hp.WithinRange = '' THEN '<font color=''red''>' + hp.OutRange + '</font>' ELSE WithinRange END AS Result>",
                            (hasil.StandarValue + " " + hasil.Satuan).As("StandarValue"),
                            hasil.OrderLabTglOrder
                            );
                        hasil.Where(reg.RegistrationNo == txtRegistrationNo.Text);
                        hasil.OrderBy(hasil.OrderLabNo.Ascending, hasil.Seq.Ascending);
                        //--end--
                        return hasil.LoadDataTable();

                    case AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME:
                        var dtbVansLab = new DataTable();
                        dtbVansLab.Columns.Add(new DataColumn("OrderLabNo", typeof(string)));
                        dtbVansLab.Columns.Add(new DataColumn("LabOrderSummary", typeof(string)));
                        dtbVansLab.Columns.Add(new DataColumn("Result", typeof(string)));
                        dtbVansLab.Columns.Add(new DataColumn("StandarValue", typeof(string)));
                        dtbVansLab.Columns.Add(new DataColumn("OrderLabTglOrder", typeof(DateTime)));

                        var qrhsl = new VwHasilPasienVanslabQuery("hsl");
                        var qitm = new ItemLaboratoryQuery("itm");
                        qrhsl.LeftJoin(qitm).On(qitm.ItemID == qrhsl.KodeTest);
                        qrhsl.Where(qrhsl.RegistrationNo == txtRegistrationNo.Text,
                                    qrhsl.Or(qitm.IsConfidential == false, qitm.IsConfidential.IsNull()));
                        qrhsl.OrderBy(qrhsl.TransactionNo.Descending, qrhsl.NoUrut.Ascending);
                        var vw = new VwHasilPasienVanslabCollection();

                        if (vw.Load(qrhsl))
                        {
                            foreach (var entity in vw)
                            {
                                var vhpv = dtbVansLab.NewRow();
                                vhpv["OrderLabNo"] = entity.TransactionNo;
                                vhpv["LabOrderSummary"] = entity.NamaPemeriksaan;
                                vhpv["Result"] = (entity.Hasil + ' ' + entity.Unit);
                                vhpv["StandarValue"] = entity.StandardValue + ' ' + entity.Unit;
                                vhpv["OrderLabTglOrder"] = DBNull.Value;

                                dtbVansLab.Rows.Add(vhpv);
                            }
                        }
                        else
                        {
                            var vhpv = dtbVansLab.NewRow();
                            vhpv["OrderLabNo"] = string.Empty;
                            vhpv["LabOrderSummary"] = string.Format("Record data is not available from {0}", AppSession.Parameter.LisInterop);
                            vhpv["Result"] = string.Empty;
                            vhpv["StandarValue"] = string.Empty;
                            vhpv["OrderLabTglOrder"] = DBNull.Value;

                            dtbVansLab.Rows.Add(vhpv);
                        }

                        return dtbVansLab;

                    default: //AppConstant.HIS_INTEROP.SYSMEX_LIS_INTEROP_CONNECTION_NAME:
                        var hasil2 = new BusinessObject.Interop.SYSMEX.VwHasilPasienQuery("hp");

                        hasil2.InnerJoin(hd).On(hasil2.OrderLabNo == hd.TransactionNo);
                        hasil2.InnerJoin(reg).On(hd.RegistrationNo == reg.RegistrationNo);

                        hasil2.Where(reg.RegistrationNo == txtRegistrationNo.Text);

                        //--start--
                        hasil2.OrderBy(hasil2.OrderLabTglOrder.Descending, hasil2.OrderLabNo.Ascending, hasil2.TestGroup.Ascending,
                            hasil2.OrderTestid.Ascending, hasil2.DispSeq.Ascending);
                        //--end--
                        return hasil2.LoadDataTable();

                }
            }
        }

        protected void grdLabHist_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Reload")
            {
                grdLabHist.DataSource = GetLabHistOrder;
                grdLabHist.DataBind();
            }

        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (eventArgument.Contains("addwithbarcode"))
            {
                var barcode = eventArgument.Split('|')[1];
                if (AddItemDetailWithBarcode(barcode))
                {
                    grdTransPrescriptionItem2.Rebind();
                }
                var txtBarcode = (RadTextBox)source;
                txtBarcode.Text = string.Empty;
                txtBarcode.Focus();
            }
        }

        #region Barcode entry
        protected virtual void txtBarcodeEntry_OnTextChanged(object sender, System.EventArgs e)
        {
            var txtBarcodeEntry = (RadTextBox)sender;
            if (txtBarcodeEntry.Text == string.Empty)
                return;

            if (AddItemDetailWithBarcode(txtBarcodeEntry.Text))
            {
                grdTransPrescriptionItem2.Rebind();
            }

            txtBarcodeEntry.Text = string.Empty;
            txtBarcodeEntry.Focus();
        }

        private bool AddItemDetailWithBarcode(string barcode)
        {
            //Check hanya untuk type item 11 Medical & 21 Non Medical
            var item = new Item();
            if (!item.LoadByBarcode(barcode))
            {
                // Barcode bisa sbg ItemID
                if (!item.LoadByPrimaryKey(barcode))
                    return false;
            }

            var itemId = item.ItemID;
            var itemName = item.ItemName;
            var srItemUnit = string.Empty;
            decimal costPrice = 0;
            var srConsumeMethod = string.Empty;
            bool isItemInventory = false;

            if (item.SRItemType == ItemType.Medical)
            {
                var ipm = new ItemProductMedic();
                ipm.LoadByPrimaryKey(itemId);
                isItemInventory = ipm.IsInventoryItem ?? false;
                srItemUnit = ipm.SRItemUnit;
                costPrice = ipm.CostPrice ?? 0;
                srConsumeMethod = ipm.SRConsumeMethod;
            }
            else if (item.SRItemType == ItemType.NonMedical)
            {
                var ipnm = new ItemProductNonMedic();
                ipnm.LoadByPrimaryKey(itemId);
                isItemInventory = ipnm.IsInventoryItem ?? false;
                srItemUnit = ipnm.SRItemUnit;
                costPrice = ipnm.CostPrice ?? 0;
            }
            else if (item.SRItemType == ItemType.Kitchen)
            {
                var ik = new ItemKitchen();
                ik.LoadByPrimaryKey(itemId);
                isItemInventory = ik.IsInventoryItem ?? false;
                srItemUnit = ik.SRItemUnit;
                costPrice = ik.CostPrice ?? 0;
            }

            var bal = new ItemBalance();
            if (!bal.LoadByPrimaryKey(cboLocationID.SelectedValue, itemId))
                return false;

            if (bal.Balance < 0 && isItemInventory)
                return false;

            string sequenceNo,
                   embalaceId,
                   srDiscountReason,
                   notes,
                   dosageQty,
                   embalaceQty,
                   embalaceLabel,
                   srConsumeMethodName,
                   orderText,
                   iterText,
                   srConsumeUnit,
                   itemQtyInString;
            decimal? takenQty, resultQty, initialPrice, price, recipeAmount, discountAmount,
                embalaceAmount, lineAmount, numberOfDosage;

            //Check bila sudah ada maka tambah di qty nya saja
            var entity = TransPrescriptionItems.FirstOrDefault(rec => rec.ItemID.Equals(itemId) && rec.ParentNo.Equals(string.Empty));

            if (entity != null)
            {
                sequenceNo = entity.SequenceNo;
                itemQtyInString = Convert.ToString(Convert.ToInt16(entity.ItemQtyInString) + 1);
                takenQty = entity.TakenQty + 1;
                resultQty = entity.ResultQty + 1;
                costPrice = entity.CostPrice ?? 0;
                initialPrice = entity.InitialPrice ?? 0;
                price = entity.Price ?? 0;
                recipeAmount = entity.RecipeAmount ?? 0;
                discountAmount = entity.DiscountAmount ?? 0;
                embalaceAmount = entity.EmbalaceAmount ?? 0;
                embalaceId = entity.EmbalaceID;
                embalaceLabel = entity.EmbalaceLabel;
                embalaceQty = entity.EmbalaceQty;
                lineAmount = (resultQty * price) + recipeAmount;
                srDiscountReason = entity.SRDiscountReason;
                notes = entity.Notes;
                srConsumeMethod = entity.SRConsumeMethod;
                srConsumeMethodName = entity.SRConsumeMethodName;
                numberOfDosage = entity.NumberOfDosage;
                orderText = entity.OrderText;
                iterText = entity.IterText;
                dosageQty = itemQtyInString;
                srConsumeUnit = entity.SRConsumeUnit;
            }
            else
            {
                if (TransPrescriptionItems.Count == 0)
                    sequenceNo = "e01";
                else
                    sequenceNo = "e" + string.Format("{0:00}", int.Parse(TransPrescriptionItems[TransPrescriptionItems.Count - 1].SequenceNo.Substring(1, 2)) + 1);

                itemQtyInString = "1";
                takenQty = 1;
                resultQty = 1;
                initialPrice =
                    Helper.Tariff.GetItemTariffNonMargin(AppSession.Parameter.DefaultTariffType, DateTime.Now.Date,
                                                         AppSession.Parameter.DefaultTariffClass, itemId, false,
                                                         srItemUnit);

                price = Helper.Tariff.GetItemTariff(AppSession.Parameter.DefaultTariffType, DateTime.Now.Date,
                                                                     AppSession.Parameter.DefaultTariffClass,
                                                                     itemId, false, srItemUnit,
                                                                     cboGuarantorID.SelectedValue, Request.QueryString["mode"]);

                recipeAmount = Convert.ToDecimal(AppParameter.GetParameterValue(AppParameter.ParameterItem.RecipeMarginValueNonCompound));
                discountAmount = 0;
                embalaceAmount = 0;
                embalaceId = string.Empty;
                embalaceLabel = string.Empty;
                embalaceQty = "0";
                lineAmount =
                    Convert.ToDecimal(Helper.Rounding(Convert.ToDecimal(resultQty * price + recipeAmount),
                                                      AppEnum.RoundingType.Prescription));

                srDiscountReason = string.Empty;
                notes = string.Empty;
                if (string.IsNullOrEmpty(srConsumeMethod))
                    srConsumeMethod = AppSession.Parameter.DefaultConsumeMethod;

                var cm = new ConsumeMethod();
                srConsumeMethodName = cm.LoadByPrimaryKey(srConsumeMethod) ? cm.SRConsumeMethodName : string.Empty;
                numberOfDosage = 0;
                orderText = string.Empty;
                iterText = string.Empty;
                dosageQty = "1";
                srConsumeUnit = AppSession.Parameter.DefaultDosageUnit;

                entity = TransPrescriptionItems.AddNew();
            }

            entity.PrescriptionNo = txtPrescriptionNo.Text;
            entity.SequenceNo = sequenceNo;
            entity.ParentNo = string.Empty;
            entity.IsRFlag = true;
            entity.IsCompound = false;
            entity.ItemID = itemId;
            entity.ItemName = itemName;
            entity.ItemInterventionID = string.Empty;
            entity.ItemInterventionName = string.Empty;
            entity.SRItemUnit = srItemUnit;
            entity.SRDosageUnit = srItemUnit;
            entity.PrescriptionQty = Convert.ToDecimal(new Fraction(itemQtyInString));
            entity.TakenQty = takenQty;
            entity.ResultQty = resultQty;
            entity.CostPrice = costPrice;
            entity.InitialPrice = initialPrice;
            entity.Price = price;
            entity.RecipeAmount = recipeAmount;
            entity.DiscountAmount = discountAmount;
            entity.EmbalaceID = embalaceId;
            entity.EmbalaceLabel = embalaceLabel;
            entity.EmbalaceQty = embalaceQty;
            entity.EmbalaceAmount = embalaceAmount;
            entity.Total = ((entity.ResultQty ?? 0) * ((entity.Price ?? 0) - (entity.DiscountAmount ?? 0))) + (entity.RecipeAmount ?? 0) + (entity.EmbalaceAmount ?? 0);
            entity.LineAmount = lineAmount;
            entity.SRDiscountReason = srDiscountReason;
            entity.Notes = notes;
            entity.SRConsumeMethod = srConsumeMethod;
            entity.SRConsumeMethodName = srConsumeMethodName;
            entity.DosageQty = numberOfDosage.ToString();
            entity.OrderText = orderText;
            entity.IterText = iterText;
            entity.ConsumeQty = "1";
            entity.SRConsumeUnit = srConsumeUnit;

            //--- default value ---
            entity.ItemQtyInString = itemQtyInString;
            entity.IsUsingDosageUnit = false;
            entity.FrequencyOfDosing = 0;
            entity.DosingPeriod = string.Empty;
            entity.NumberOfDosage = 0;
            entity.DurationOfDosing = 0;
            entity.Acpcdc = string.Empty;
            entity.SRMedicationRoute = string.Empty;
            entity.ConsumeMethod = string.Empty;
            entity.IsCalcPercentage = false;
            entity.IsUseSweetener = false;
            entity.SweetenerAmount = 0;
            entity.IsPendingDelivery = false;
            entity.DaysOfUsage = 0;
            entity.Qty23Days = 0;

            var reg = new Registration();
            if (reg.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                if (Helper.GuarantorBpjsCasemix.Contains(reg.GuarantorID) && AppSession.Parameter.CasemixValidationRegistrationType.Contains(reg.SRRegistrationType))
                    entity.IsCasemixApproved = Helper.IsCasemixApproved(itemId, entity.ResultQty ?? 0, reg.RegistrationNo, entity.PrescriptionNo, reg.GuarantorID, true);
                else
                    entity.IsCasemixApproved = true;
            }
            else
                entity.IsCasemixApproved = true;

            return true;
        }
        #endregion

        #region PatientImage
        private void PopulatePatientImage(string patientID)
        {
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
                    imgPatientPhoto.ImageUrl = txtGender.Text == "M" ? "~/Images/Asset/Patient/ManVector.png" : (txtGender.Text == "F" ? "~/Images/Asset/Patient/WomanVector.png" : "~/Images/Asset/Patient/HumanVector.png");
                }
            }
            else
                imgPatientPhoto.ImageUrl = txtGender.Text == "M" ? "~/Images/Asset/Patient/ManVector.png" : (txtGender.Text == "F" ? "~/Images/Asset/Patient/WomanVector.png" : "~/Images/Asset/Patient/HumanVector.png");

        }
        #endregion

        public void SetPrescUnitAndLocationFromGuar()
        {
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                var guar = new Guarantor();
                if (guar.LoadByPrimaryKey(reg.GuarantorID))
                {
                    string suid = "", locid = "";
                    switch (reg.SRRegistrationType.ToUpper())
                    {
                        case "IPR":
                            {
                                if (!string.IsNullOrEmpty(guar.PrescriptionServiceUnitIdIPR) &&
                                    !string.IsNullOrEmpty(guar.PrescriptionLocationIdIPR))
                                {
                                    suid = guar.PrescriptionServiceUnitIdIPR;
                                    locid = guar.PrescriptionLocationIdIPR;
                                }
                                break;
                            }
                        case "EMR":
                            {
                                if (!string.IsNullOrEmpty(guar.PrescriptionServiceUnitIdEMR) &&
                                    !string.IsNullOrEmpty(guar.PrescriptionLocationIdEMR))
                                {
                                    suid = guar.PrescriptionServiceUnitIdEMR;
                                    locid = guar.PrescriptionLocationIdEMR;
                                }
                                break;
                            }
                        default:
                            {
                                if (!string.IsNullOrEmpty(guar.PrescriptionServiceUnitIdOPR) &&
                                    !string.IsNullOrEmpty(guar.PrescriptionLocationIdOPR))
                                {
                                    suid = guar.PrescriptionServiceUnitIdOPR;
                                    locid = guar.PrescriptionLocationIdOPR;
                                }
                                break;
                            }
                    }
                    if (!string.IsNullOrEmpty(suid))
                    {
                        ComboBox.SelectedValue(cboServiceUnitID, suid);
                        cboServiceUnitID_SelectedIndexChanged(cboServiceUnitID,
                            new RadComboBoxSelectedIndexChangedEventArgs(
                                cboServiceUnitID.Text, cboServiceUnitID.Text,
                                cboServiceUnitID.SelectedValue, cboServiceUnitID.SelectedValue));
                        ComboBox.SelectedValue(cboLocationID, locid);
                    }
                }
            }
        }

        #region cboFormRegistrationNo
        protected void cboFromRegistrationNo_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var reg = new RegistrationQuery("a");
            var pat = new PatientQuery("b");
            var unit = new ServiceUnitQuery("c");
            var room = new ServiceRoomQuery("d");
            var bed = new BedQuery("e");

            reg.es.Top = 5;
            reg.Select(
                reg.RegistrationNo,
                reg.BedID,
                pat.PatientID,
                pat.MedicalNo,
                pat.PatientName,
                unit.ServiceUnitName,
                room.RoomName
                );
            reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            reg.LeftJoin(room).On(reg.RoomID == room.RoomID);
            reg.LeftJoin(bed).On(reg.RegistrationNo == bed.RegistrationNo);
            reg.Where(
                reg.PatientID == cboPatientID.SelectedValue,
                reg.IsClosed == false,
                reg.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                reg.IsHoldTransactionEntry == false,
                reg.IsClosed == false,
                reg.IsVoid == false
                );

            if (e.Text.Trim().Contains(" "))
            {
                var searchs = e.Text.Trim().Split(' ');
                foreach (var search in searchs)
                {
                    var searchLike = "%" + search + "%";
                    reg.Where(
                        reg.Or(
                            reg.RegistrationNo.Like(searchLike),
                            //pat.PatientID.Like(searchLike),
                            pat.FirstName.Like(searchLike),
                            pat.LastName.Like(searchLike),
                            pat.MiddleName.Like(searchLike),
                            pat.MedicalNo.Like(searchLike)
                            )
                        );
                }
            }
            else
            {
                string searchTextContain = string.Format("%{0}%", e.Text);
                reg.Where(
                    reg.Or(
                        reg.RegistrationNo.Like(searchTextContain),
                        //pat.PatientID.Like(searchTextContain),
                        pat.MedicalNo.Like(searchTextContain),
                        pat.FirstName.Like(searchTextContain),
                        pat.MiddleName.Like(searchTextContain),
                        pat.LastName.Like(searchTextContain)
                        )
                );
            }
            reg.OrderBy(reg.RegistrationDate.Descending);

            cboFromRegistrationNo.DataSource = reg.LoadDataTable();
            cboFromRegistrationNo.DataBind();
        }

        protected void cboFromRegistrationNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["RegistrationNo"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["RegistrationNo"].ToString();
        }

        protected void cboFromRegistrationNo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                var reg = new Registration();
                if (reg.LoadByPrimaryKey(e.Value))
                {
                    cboParamedicID.SelectedValue = reg.ParamedicID;
                    if (chkIsSplitBill.Checked)
                    {
                        cboGuarantorID.SelectedValue = reg.GuarantorID;
                        var guar = new Guarantor();
                        guar.LoadByPrimaryKey(reg.GuarantorID);
                        trBpjsSepNo.Visible = guar.SRGuarantorType == AppSession.Parameter.GuarantorTypeBPJS;
                        txtBpjsSepNo.Text = reg.BpjsSepNo;
                    }
                    else
                    {
                        PopulateGuarantorFromPatientInformation(cboPatientID.SelectedValue);
                        txtBpjsSepNo.Text = string.Empty;
                    }
                }
                else
                {
                    cboParamedicID.SelectedValue = string.Empty;
                    cboParamedicID.Text = string.Empty;
                    PopulateGuarantorFromPatientInformation(cboPatientID.SelectedValue);
                    txtBpjsSepNo.Text = string.Empty;
                }
            }
            else
            {
                cboParamedicID.SelectedValue = string.Empty;
                cboParamedicID.Text = string.Empty;
                PopulateGuarantorFromPatientInformation(cboPatientID.SelectedValue);
                txtBpjsSepNo.Text = string.Empty;
            }
        }
        #endregion

        private void Create23DaysPrescription()
        {
            var prc = new TransPrescription();
            prc.LoadByPrimaryKey(txtPrescriptionNo.Text);

            var regOld = new Registration();
            regOld.LoadByPrimaryKey(prc.RegistrationNo);

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(regOld.GuarantorID);

            using (var trans = new esTransactionScope())
            {
                {
                    var reg = new Registration();
                    string newRegNo = GetNewRegistrationNo();
                    _autoNumberReg.Save();

                    reg.RegistrationNo = newRegNo;
                    reg.SRRegistrationType = AppConstant.RegistrationType.OutPatient;
                    reg.PatientID = cboPatientID.SelectedValue;
                    //reg.GuarantorID = cboGuarantorID.SelectedValue;
                    reg.ClassID = AppSession.Parameter.OutPatientClassID;
                    reg.RegistrationDate = txtPrescriptionDate.SelectedDate;
                    reg.RegistrationTime = (new DateTime()).NowAtSqlServer().ToString("HH:mm");
                    reg.AgeInYear = Convert.ToByte(txtAgeYear.Value);
                    reg.AgeInMonth = Convert.ToByte(txtAgeMonth.Value);
                    reg.AgeInDay = Convert.ToByte(txtAgeDay.Value);
                    reg.SRShift = Registration.GetShiftID();
                    reg.DepartmentID = AppParameter.GetParameterValue(AppParameter.ParameterItem.PharmacyDepartmentID);
                    reg.ServiceUnitID = txtServiceUnitID.Text;
                    reg.ChargeClassID = AppSession.Parameter.OutPatientClassID;
                    reg.CoverageClassID = AppSession.Parameter.OutPatientClassID;
                    reg.str.ParamedicID = cboParamedicID.SelectedValue;
                    reg.IsRoomIn = false;
                    reg.IsFromDispensary = true;
                    reg.LastCreateUserID = AppSession.UserLogin.UserID;
                    reg.LastCreateDateTime = (new DateTime()).NowAtSqlServer();
                    reg.BpjsSepNo = regOld.BpjsSepNo;

                    if (!string.IsNullOrEmpty(AppParameter.GetParameterValue(AppParameter.ParameterItem.Is23DaysPrescriptionUseChronicGuarantor)))
                        reg.GuarantorID = AppParameter.GetParameterValue(AppParameter.ParameterItem.Is23DaysPrescriptionUseChronicGuarantor);
                    else
                        reg.GuarantorID = cboGuarantorID.SelectedValue;

                    //Guarantor Detail Info
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
                    reg.FromRegistrationNo = string.Empty;

                    //Last Update Status
                    reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    reg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    reg.Save();

                    var mrg = new MergeBilling();
                    mrg.RegistrationNo = reg.RegistrationNo;
                    mrg.FromRegistrationNo = string.Empty;

                    //Last Update Status
                    mrg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    mrg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    mrg.Save();

                    var hd = new TransPrescription();
                    hd.LoadByPrimaryKey(txtPrescriptionNo.Text);
                    hd.MarkAllColumnsAsDirty(DataRowState.Added);

                    var autoNumber = Helper.GetNewAutoNumber(txtPrescriptionDate.SelectedDate.Value.Date,
                                                                  Request.QueryString["rt"] == "opr"
                                                                      ? AppEnum.AutoNumber.PrescOpNo
                                                                      : AppEnum.AutoNumber.PrescIpNo);

                    hd.PrescriptionNo = autoNumber.LastCompleteNumber;
                    autoNumber.Save();

                    hd.RegistrationNo = reg.RegistrationNo;
                    hd.IsApproval = false;
                    hd.IsBillProceed = false;
                    hd.IsVoid = false;
                    hd.str.ApprovalDateTime = string.Empty;
                    hd.ApprovedByUserID = null;
                    hd.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    hd.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    hd.IsUnapproved = null;
                    hd.UnapprovedByUserID = null;
                    hd.str.UnapprovedDateTime = string.Empty;
                    hd.str.CompleteDateTime = string.Empty;
                    hd.str.DeliverDateTime = string.Empty;
                    hd.IsFromSOAP = false;
                    hd.IsDirect = true;
                    hd.Is23Days = true;
                    hd.IsProceedByPharmacist = false;
                    hd.ReferenceNo = txtPrescriptionNo.Text;

                    hd.Save();

                    var dt = new TransPrescriptionItemCollection();
                    dt.Query.Where(dt.Query.PrescriptionNo == txtPrescriptionNo.Text, dt.Query.Qty23Days > 0);
                    dt.LoadAll();

                    foreach (var d in dt)
                    {
                        d.MarkAllColumnsAsDirty(DataRowState.Added);

                        d.PrescriptionNo = hd.PrescriptionNo;

                        d.IsApprove = false;
                        d.IsVoid = false;
                        d.IsBillProceed = false;

                        decimal resultQty = d.Qty23Days ?? 0;

                        var itemMedic = new ItemProductMedic();
                        if (itemMedic.LoadByPrimaryKey(string.IsNullOrEmpty(d.ItemInterventionID) ? d.ItemID : d.ItemInterventionID))
                        {
                            if (!(itemMedic.IsActualDeduct ?? false))
                                resultQty = Math.Ceiling(d.Qty23Days ?? 0);
                        }
                        d.TakenQty = resultQty;
                        d.ResultQty = resultQty;
                        d.ItemQtyInString = resultQty.ToString();
                        d.PrescriptionQty = resultQty;
                        d.DosageQty = resultQty.ToString();
                        d.Qty23Days = 0;

                        //hitung price ulang, antisipasi u/ yg pake rule margin
                        if ((d.IsCompound ?? false) && AppSession.Parameter.RecipeMarginValueCompound != 0)
                        {
                            d.Price = (decimal)Helper.Tariff.GetItemTariffNonMargin(grr.SRTariffType, txtPrescriptionDate.SelectedDate.Value.Date,
                                                                                                 reg.ChargeClassID,
                                                                                                 string.IsNullOrEmpty(d.ItemInterventionID) ? d.ItemID : d.ItemInterventionID,
                                                                                                 (d.IsCompound ?? false),
                                                                                                 d.SRDosageUnit);
                            d.Price += Convert.ToDecimal(AppSession.Parameter.RecipeMarginValueCompound / 100) * d.Price;
                        }
                        else
                        {
                            d.Price = (decimal)Helper.Tariff.GetItemTariff(grr.SRTariffType, txtPrescriptionDate.SelectedDate.Value.Date, reg.ChargeClassID,
                                string.IsNullOrEmpty(d.ItemInterventionID) ? d.ItemID : d.ItemInterventionID, (d.IsCompound ?? false), d.SRDosageUnit, reg.GuarantorID, reg.SRRegistrationType);
                        }

                        /* DiscountAmount di-nol-kan krn akan dihitung ulang pada saat Approved */
                        //d.DiscountAmount += resultQty * d.AutoProcessCalculation;
                        //if (d.DiscountAmount <= 1) d.DiscountAmount = 0;
                        d.DiscountAmount = 0;
                        d.AutoProcessCalculation = 0;

                        d.LineAmount = d.LineAmount = ((resultQty * d.Price) - d.DiscountAmount) + d.EmbalaceAmount + (d.SweetenerAmount ?? 0) + d.RecipeAmount;
                        d.LineAmount = Helper.Rounding(d.LineAmount ?? 0, AppEnum.RoundingType.Prescription);

                        if (Helper.GuarantorBpjsCasemix.Contains(reg.GuarantorID) && AppSession.Parameter.CasemixValidationRegistrationType.Contains(reg.SRRegistrationType))
                        {
                            var itemId = (string.IsNullOrWhiteSpace(d.ItemInterventionID) ? d.ItemID : d.ItemInterventionID);
                            d.IsCasemixApproved = Helper.IsCasemixApproved(itemId, d.ResultQty ?? 0, reg.RegistrationNo, d.PrescriptionNo, reg.GuarantorID, true);
                        }
                        else
                            d.IsCasemixApproved = true;

                        d.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        d.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    }

                    dt.Save();
                }


                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }


        private void PopulateSoap()
        {
            if (!pnlOrderNote.Visible) return;
            if (string.IsNullOrWhiteSpace(RegistrationNo)) return; // Untuk yg tidak pakai RegistrationNo seperti OTC

            //var regNos = Registration.RelatedRegistrations(RegistrationNo);
            //var rimColl = new RegistrationInfoMedicCollection();
            //rimColl.Query.Where(
            //    rimColl.Query.RegistrationNo.In(regNos),
            //    rimColl.Query.SRMedicalNotesInputType == "SOAP",
            //    rimColl.Query.Or(rimColl.Query.IsDeleted.IsNull(), rimColl.Query.IsDeleted == false)
            //    );
            //rimColl.Query.OrderBy(rimColl.Query.DateTimeInfo.Descending);
            //rimColl.LoadAll();

            //var strb = new StringBuilder();
            //strb.AppendLine("<table width=\"100%\"><tr><td width=\"25%\" class=\"rgHeader\">Subjective</td><td width=\"25%\" class=\"rgHeader\">Objective</td><td width=\"25%\" class=\"rgHeader\">Assessmnet</td><td width=\"25%\" class=\"rgHeader\">Planning</td></tr></table>");
            //strb.AppendLine("<div style=\"overflow: auto;width:100%; height: 140px;\"><table width=\"100%\">");

            //var i = 0;
            //foreach (var rim in rimColl)
            //{
            //    if (!string.IsNullOrWhiteSpace(string.Concat(rim.Info1, rim.Info2, rim.Info3, rim.Info4)))
            //    {
            //        i++;
            //        var className = i % 2 == 0 ? "rgAltRow" : "rgRow";
            //        strb.AppendLine("<tr>");
            //        strb.AppendFormat("<td valign=\"top\" class=\"{2}\" width=\"25%\"><b>{0}</b><br />{1}<br /></td>", rim.DateTimeInfo.Value.ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute), rim.Info1.Trim().Replace(System.Environment.NewLine, "<br />"), className);
            //        strb.AppendFormat("<td valign=\"top\" class=\"{2}\" width=\"25%\"><b>{0}</b><br />{1}</td>", rim.DateTimeInfo.Value.ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute), rim.Info2.Trim().Replace(System.Environment.NewLine, "<br />"), className);
            //        strb.AppendFormat("<td valign=\"top\" class=\"{2}\" width=\"25%\"><b>{0}</b><br />{1}</td>", rim.DateTimeInfo.Value.ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute), rim.Info3.Trim().Replace(System.Environment.NewLine, "<br />"), className);
            //        strb.AppendFormat("<td valign=\"top\" class=\"{2}\" width=\"25%\"><b>{0}</b><br />{1}</td>", rim.DateTimeInfo.Value.ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute), rim.Info4.Trim().Replace(System.Environment.NewLine, "<br />"), className);
            //        strb.AppendLine("</tr>");
            //    }
            //}
            //strb.AppendLine("</table></div>");
            //litSoap.Text = strb.ToString();

            //db:20231125 - Display SOAP history using SoapInfoCtl
            soapInfoCtl.PopulateSoap(RegistrationNo);
        }

        #region APOL
        //protected void btnSaveApolDtl_Click(object sender, EventArgs e)
        //{
        //}
        protected void btnCariHistory_Click(object sender, EventArgs e)
        {
            lblInfo.Text = "";

            try
            {
                if (!string.IsNullOrWhiteSpace(txtNoKaHist.Text) && !txtPeriode1.IsEmpty && !txtPeriode2.IsEmpty)
                {
                    var svc = new Common.BPJS.Apotek.Service();
                    var riwayat = svc.GetRiwayatPelayanan(txtPeriode1.SelectedDate.Value.Date, txtPeriode2.SelectedDate.Value.Date, txtNoKaHist.Text);
                    if (riwayat.MetaData.IsValid && riwayat.Response?.List?.Histories != null)
                    {
                        var allHistories = riwayat.Response.List.Histories;

                        grdListHist.DataSource = allHistories;
                        grdListHist.DataBind();
                    }
                    else
                    {
                        lblInfo.Text = $"{riwayat.MetaData.Code} - {riwayat.MetaData.Message}";
                        grdListHist.DataSource = null;
                        grdListHist.DataBind();
                    }

                    grdListHist.DataBind();
                }
                else
                {
                    lblInfo.Text = "Mohon lengkapi No Kartu dan Periode.";
                    grdListHist.DataSource = null;
                    grdListHist.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblInfo.Text = "Terjadi kesalahan: " + ex.Message;
                grdListHist.DataSource = null;
                grdListHist.DataBind();
            }
        }

        protected void btnCariPasienSep_Click(object sender, EventArgs e)
        {
            try
            {
                string nosep = txtBpjsSepNo.Text;

                if (nosep.Length != 19)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('SEP Number Must 19 Characters!');", true);
                    return;
                }

                var svc = new Common.BPJS.Apotek.Service();
                var response = svc.GetSep(nosep);

                if (response.MetaData.IsApolValid)
                {
                    chkPRB.Checked = response.Response.Flagprb == "1";
                    txtRefAsalSJP.Text = response.Response.NoSep;
                    txtPoliRSP.Text = response.Response.Poli;
                    txtKdDokter.Text = response.Response.Kodedokter;

                    //var parmedbrid = new ParamedicBridging();
                    //parmedbrid.Query.Where(parmedbrid.Query.BridgingID == response.Response.Kodedokter);
                    //if (parmedbrid.Query.Load())
                    //{
                    //    cboDktrApol.SelectedValue = parmedbrid.ParamedicID;
                    //}
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('Error: {response.MetaData.Code} - {response.MetaData.Message}');", true);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        protected void btnCreateHouseApol_Click(object sender, EventArgs e)
        {
            try
            {
                var requestData = new Common.BPJS.Apotek.Resep.SimpanResep.Request
                {
                    TGLSJP = txtTglRsp.SelectedDate.Value.ToString("yyyy-MM-dd"),
                    REFASALSJP = txtRefAsalSJP.Text,
                    POLIRSP = txtPoliRSP.Text,
                    KDJNSOBAT = cboJnsRsp.SelectedValue,
                    NORESEP = txtNoResep.Text,
                    IDUSERSJP = AppSession.UserLogin.UserID,
                    TGLRSP = txtPrescriptionDate.SelectedDate.Value.ToString("yyyy-MM-dd"),
                    TGLPELRSP = txtPrescriptionDate.SelectedDate.Value.ToString("yyyy-MM-dd"),
                    KdDokter = txtKdDokter.Text,
                    Iterasi = cboIterasi.SelectedValue
                };

                // cari header dulu
                var bpjsapol = new BpjsApol();
                bpjsapol.Query.Where(
                    bpjsapol.Query.PrescriptionNo == txtPrescriptionNo.Text,
                    bpjsapol.Query.RegistrationNo == txtRegistrationNo.Text
                );
                bool exists = bpjsapol.Query.Load();

                // case 1: header ADA dan statusnya ORDER → update + kirim
                if (exists && (bpjsapol.MetadataCode ?? "").ToUpper() == "ORDER")
                {
                    var svc = new Common.BPJS.Apotek.Service();
                    var result = svc.SimpanResep(requestData);

                    if (result != null && result.MetaData != null)
                    {
                        lblCreateApolResult.Text = $"[Code: {result.MetaData.Code}] {result.MetaData.Message}";
                        lblCreateApolResult.ForeColor = System.Drawing.Color.Green;

                        if (result.MetaData.IsValid && result.ResponseData != null)
                        {
                            var response = result.ResponseData;

                            // update header yang sudah ada
                            bpjsapol.TGLSJP = DateTime.Parse(requestData.TGLSJP);
                            bpjsapol.REFASALSJP = requestData.REFASALSJP;
                            bpjsapol.POLIRSP = requestData.POLIRSP;
                            bpjsapol.KDJNSOBAT = Convert.ToByte(response.KdJnsObat);
                            bpjsapol.NORESEP = response.NoResep;
                            bpjsapol.IDUSERSJP = requestData.IDUSERSJP;
                            bpjsapol.TGLRSP = DateTime.Parse(requestData.TGLRSP);
                            bpjsapol.TGLPELRSP = DateTime.Parse(requestData.TGLPELRSP);
                            bpjsapol.KDDOKTER = requestData.KdDokter;
                            bpjsapol.ITERASI = Convert.ToByte(requestData.Iterasi);

                            // dari response
                            bpjsapol.NosepKunjungan = response.NoSepKunjungan;
                            bpjsapol.NOKARTU = response.NoKartu;
                            bpjsapol.NAMA = txtPatientName.Text;
                            bpjsapol.FASKESASAL = response.FaskesAsal;
                            bpjsapol.NOAPOTIK = response.NoApotik;   // NOSJP
                            bpjsapol.TGLRESEP = DateTime.Parse(response.TglResep);
                            bpjsapol.TGLENTRY = DateTime.Parse(response.TglEntry);

                            // update status
                            bpjsapol.MetadataCode = result.MetaData.Code;
                            bpjsapol.MetadataMessage = result.MetaData.Message;
                            bpjsapol.LastUpdateDateTime = DateTime.Now;
                            bpjsapol.LastUpdateByUserID = AppSession.UserLogin.UserID;

                            bpjsapol.Save();
                        }
                    }
                    else
                    {
                        lblCreateApolResult.Text = "No metadata returned from server.";
                        lblCreateApolResult.ForeColor = System.Drawing.Color.Red;
                    }

                    return;
                }

                // case 2: header BELUM ADA → buat baru + kirim
                if (!exists)
                {
                    // buat dulu
                    var apol = new BpjsApol
                    {
                        // dari request
                        TGLSJP = DateTime.Parse(requestData.TGLSJP),
                        REFASALSJP = requestData.REFASALSJP,
                        POLIRSP = requestData.POLIRSP,
                        KDJNSOBAT = Convert.ToByte(requestData.KDJNSOBAT),
                        NORESEP = requestData.NORESEP,
                        IDUSERSJP = requestData.IDUSERSJP,
                        TGLRSP = DateTime.Parse(requestData.TGLRSP),
                        TGLPELRSP = DateTime.Parse(requestData.TGLPELRSP),
                        KDDOKTER = requestData.KdDokter,
                        ITERASI = Convert.ToByte(requestData.Iterasi),

                        RegistrationNo = txtRegistrationNo.Text,
                        PrescriptionNo = txtPrescriptionNo.Text,
                        LastUpdateDateTime = DateTime.Now,
                        LastUpdateByUserID = AppSession.UserLogin.UserID,
                        MetadataCode = "ORDER",
                        MetadataMessage = "Header dibuat sebelum kirim."
                    };
                    apol.Save();

                    // kirim
                    var svc = new Common.BPJS.Apotek.Service();
                    var result = svc.SimpanResep(requestData);

                    if (result != null && result.MetaData != null)
                    {
                        lblCreateApolResult.Text = $"[Code: {result.MetaData.Code}] {result.MetaData.Message}";
                        lblCreateApolResult.ForeColor = System.Drawing.Color.Green;

                        if (result.MetaData.IsValid && result.ResponseData != null)
                        {
                            var response = result.ResponseData;

                            apol.NosepKunjungan = response.NoSepKunjungan;
                            apol.NOKARTU = response.NoKartu;
                            apol.NAMA = txtPatientName.Text;
                            apol.FASKESASAL = response.FaskesAsal;
                            apol.NOAPOTIK = response.NoApotik;
                            apol.TGLRESEP = DateTime.Parse(response.TglResep);
                            apol.TGLENTRY = DateTime.Parse(response.TglEntry);
                            apol.MetadataCode = result.MetaData.Code;
                            apol.MetadataMessage = result.MetaData.Message;
                            apol.LastUpdateDateTime = DateTime.Now;
                            apol.LastUpdateByUserID = AppSession.UserLogin.UserID;

                            apol.Save();
                        }
                    }
                    else
                    {
                        lblCreateApolResult.Text = "No metadata returned from server.";
                        lblCreateApolResult.ForeColor = System.Drawing.Color.Red;
                    }

                    return;
                }

                // case 3: header ADA tapi statusnya BUKAN ORDER (misal sudah 200) → jangan kirim ulang
                lblCreateApolResult.Text = $"[Code: {bpjsapol.MetadataCode}] {bpjsapol.MetadataMessage}";
                lblCreateApolResult.ForeColor = System.Drawing.Color.Orange;
            }
            catch (Exception ex)
            {
                lblCreateApolResult.Text = $"Error: {ex.Message}";
                lblCreateApolResult.ForeColor = System.Drawing.Color.Red;
                HandleException(ex);
            }
        }


        private void HandleException(Exception ex)
        {
            string errorMessage = ex.Message;

            var serverError = ex.InnerException as WebException;
            if (serverError != null)
            {
                var response = serverError.Response as HttpWebResponse;
                if (response != null && response.StatusCode != HttpStatusCode.OK)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        errorMessage = reader.ReadToEnd();
                    }
                }
            }
            Response.Write(errorMessage);
        }
        #endregion
    }
}
