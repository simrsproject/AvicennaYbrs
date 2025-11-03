using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class ServiceUnitCorrectionList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;
        protected void Page_Init(object sender, EventArgs e)
        {
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            if (Request.QueryString["disch"] == "0")
                ProgramID = AppConstant.Program.ServiceUnitTransactionCorrection;
            else if (Request.QueryString["disch"] == "1")
                ProgramID = AppConstant.Program.ServiceUnitTransactionCorrectionVerification;
            else
                ProgramID = AppConstant.Program.ServiceUnitTransactionCorrectionVerificationAncillary;

            if (!IsPostBack)
            {
                //service unit
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");

                if (Request.QueryString["resp"] == "0" && Request.QueryString["disch"] == "0")
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                    query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                }

                query.Where(
                    query.IsActive == true,
                    query.SRRegistrationType.In(
                            AppConstant.RegistrationType.EmergencyPatient,
                            AppConstant.RegistrationType.InPatient,
                            AppConstant.RegistrationType.OutPatient,
                            AppConstant.RegistrationType.MedicalCheckUp
                        )
                    );
                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                //paramedic
                var param = new ParamedicCollection();
                param.Query.Where
                    (
                        param.Query.IsActive == true,
                        param.Query.IsAvailable == true
                    );
                param.Query.OrderBy(param.Query.ParamedicName.Ascending);
                param.LoadAll();

                cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Paramedic entity in param)
                {
                    cboParamedicID.Items.Add(new RadComboBoxItem(entity.ParamedicName, entity.ParamedicID));
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;
            if (!IsPostBack && !IsListLoadRecordOnInit) {
                grd.DataSource = new String[] { };
                return; 
            }
            
            var dataSource = TransChargess;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        private DataTable TransChargess
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(cboParamedicID.SelectedValue) &&
                    string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Patient")) return null;

                DataTable dtb = TransChargesOutpatient; //digabung untuk op, er, mcu
                dtb.Merge(Request.QueryString["disch"] == "0" ? TransChargesInpatient : TransChargesInpatientAll);
                dtb.Merge(TransChargesDiagnostic);
                dtb.Merge(TransChargesOperatingTheater);
                
                return dtb.DefaultView.ToTable(true, "RegistrationDate", "ServiceUnitID", "ParamedicID", "ParamedicName", "RegistrationNo",
                    "MedicalNo", "PatientName", "Sex", "GuarantorName", "Group", "IsConsul", "BedID", "SalutationName");
            }
        }

        private DataTable TransChargesInpatient
        {
            get
            {
                var query = new RegistrationQuery("a");
                var patQ = new PatientQuery("b");
                var guarQ = new GuarantorQuery("c");
                var parQ = new ParamedicQuery("d");
                var unitQ = new ServiceUnitQuery("e");
                var sal = new AppStandardReferenceItemQuery("sal");

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unitQ.ServiceUnitName;

                query.es.Top = 50;
                query.es.Distinct = true;

                query.Select
                    (
                        query.RegistrationDate,
                        query.ServiceUnitID,
                        query.ParamedicID,
                        parQ.ParamedicName,
                        query.RegistrationNo,
                        patQ.MedicalNo,
                        patQ.PatientName,
                        patQ.Sex,
                        guarQ.GuarantorName,
                        group.As("Group"),
                        query.IsConsul,
                        query.BedID,
                        sal.ItemName.As("SalutationName")
                    );

                query.InnerJoin(patQ).On(query.PatientID == patQ.PatientID);
                query.InnerJoin(guarQ).On(query.GuarantorID == guarQ.GuarantorID);
                query.InnerJoin(parQ).On(query.ParamedicID == parQ.ParamedicID);
                query.InnerJoin(unitQ).On(query.ServiceUnitID == unitQ.ServiceUnitID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patQ.SRSalutation == sal.ItemID);

                if (Request.QueryString["resp"] == "0" && Request.QueryString["disch"] == "0")
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                    query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                }
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                    query.Where(
                        query.Or(
                            query.RegistrationNo == searchReg,
                            patQ.ReverseMedicalNo.Like(reverseMedNoSearch),
                            patQ.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                            )
                        );



                    //query.Where(
                    //    query.Or(
                    //        query.RegistrationNo == searchReg,
                    //        patQ.MedicalNo == searchReg,
                    //        patQ.OldMedicalNo == searchReg,
                    //        string.Format("< OR REPLACE(b.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //        string.Format("< OR REPLACE(b.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //        )
                    //    );
                    //Helper.AddFilterMedNoOrRegNoOrPatName(query, patQ, searchReg, "registration");
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where(patQ.FullName.Like(searchPatient));
                    //query.Where(string.Format("<RTRIM(b.FirstName+' '+b.MiddleName)+' '+b.LastName LIKE '{0}'>", searchPatient));
                }

                query.Where
                    (
                        query.IsClosed == false,
                        query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                        query.IsHoldTransactionEntry == false,
                        query.IsVoid == false
                    );
                query.Where(query.DischargeDate.IsNull());
                query.OrderBy
                    (
                        query.RegistrationNo.Ascending
                    );

                DataTable dtbl = query.LoadDataTable();

                foreach (DataRow row in dtbl.Rows)
                {
                    var bed = new Bed();
                    if (bed.LoadByPrimaryKey(row["BedID"].ToString()))
                    {
                        if (bed.IsNeedConfirmation == true & bed.SRBedStatus == AppSession.Parameter.BedStatusPending)
                            row.Delete();
                    }
                }
                dtbl.AcceptChanges();

                return dtbl;
            }
        }

        private DataTable TransChargesInpatientAll
        {
            get
            {
                var query = new RegistrationQuery("a");
                var patQ = new PatientQuery("b");
                var guarQ = new GuarantorQuery("c");
                var parQ = new ParamedicQuery("d");
                var unitQ = new ServiceUnitQuery("e");
                var sal = new AppStandardReferenceItemQuery("sal");

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unitQ.ServiceUnitName;

                query.es.Top = 50;
                query.es.Distinct = true;

                query.Select
                    (
                        query.RegistrationDate,
                        query.ServiceUnitID,
                        query.ParamedicID,
                        parQ.ParamedicName,
                        query.RegistrationNo,
                        patQ.MedicalNo,
                        patQ.PatientName,
                        patQ.Sex,
                        guarQ.GuarantorName,
                        group.As("Group"),
                        query.IsConsul,
                        query.BedID,
                        sal.ItemName.As("SalutationName")
                    );

                query.InnerJoin(patQ).On(query.PatientID == patQ.PatientID);
                query.InnerJoin(guarQ).On(query.GuarantorID == guarQ.GuarantorID);
                query.InnerJoin(parQ).On(query.ParamedicID == parQ.ParamedicID);
                query.InnerJoin(unitQ).On(query.ServiceUnitID == unitQ.ServiceUnitID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patQ.SRSalutation == sal.ItemID);

                if (Request.QueryString["resp"] == "0" && Request.QueryString["disch"] == "0")
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                    query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                }
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                    query.Where(
                        query.Or(
                            query.RegistrationNo == searchReg,
                            patQ.ReverseMedicalNo.Like(reverseMedNoSearch),
                            patQ.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                            )
                        );



                    //query.Where(
                    //    query.Or(
                    //        query.RegistrationNo == searchReg,
                    //        patQ.MedicalNo == searchReg,
                    //        patQ.OldMedicalNo == searchReg,
                    //        string.Format("< OR REPLACE(b.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //        string.Format("< OR REPLACE(b.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //        )
                    //    );
                    //Helper.AddFilterMedNoOrRegNoOrPatName(query, patQ, searchReg, "registration");
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where(patQ.FullName.Like(searchPatient));
                    //query.Where(string.Format("<RTRIM(b.FirstName+' '+b.MiddleName)+' '+b.LastName LIKE '{0}'>", searchPatient));
                }

                query.Where
                    (
                        query.IsClosed == false,
                        query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                        query.IsHoldTransactionEntry == false,
                        query.IsVoid == false
                    );

                query.OrderBy
                    (
                        query.RegistrationNo.Ascending
                    );

                DataTable dtbl = query.LoadDataTable();

                return dtbl;
            }
        }

        private DataTable TransChargesOutpatient
        {
            get
            {
                var query = new RegistrationQuery("a");
                var patQ = new PatientQuery("b");
                var guarQ = new GuarantorQuery("c");
                var parQ = new ParamedicQuery("d");
                var unitQ = new ServiceUnitQuery("e");
                var sal = new AppStandardReferenceItemQuery("sal");

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unitQ.ServiceUnitName;

                query.es.Top = 50;
                query.es.Distinct = true;

                query.Select
                    (
                        query.RegistrationDate,
                        query.ServiceUnitID,
                        query.ParamedicID,
                        parQ.ParamedicName,
                        query.RegistrationNo,
                        patQ.MedicalNo,
                        patQ.PatientName,
                        patQ.Sex,
                        guarQ.GuarantorName,
                        group.As("Group"),
                        query.IsConsul,
                        "<'' AS BedID>",
                        sal.ItemName.As("SalutationName")
                    );

                query.InnerJoin(patQ).On(query.PatientID == patQ.PatientID);
                query.InnerJoin(guarQ).On(query.GuarantorID == guarQ.GuarantorID);
                query.LeftJoin(parQ).On(query.ParamedicID == parQ.ParamedicID);
                query.InnerJoin(unitQ).On(query.ServiceUnitID == unitQ.ServiceUnitID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patQ.SRSalutation == sal.ItemID);

                if (Request.QueryString["resp"] == "0" && Request.QueryString["disch"] == "0")
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                    query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                }
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                    query.Where(
                        query.Or(
                            query.RegistrationNo == searchReg,
                            patQ.ReverseMedicalNo.Like(reverseMedNoSearch),
                            patQ.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                            )
                        );


                    //query.Where(
                    //    query.Or(
                    //        query.RegistrationNo == searchReg,
                    //        patQ.MedicalNo == searchReg,
                    //        patQ.OldMedicalNo == searchReg,
                    //        string.Format("< OR REPLACE(b.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //        string.Format("< OR REPLACE(b.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //        )
                    //    );
                    //Helper.AddFilterMedNoOrRegNoOrPatName(query, patQ, searchReg, "registration");
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where(patQ.FullName.Like(searchPatient));
                    //query.Where(string.Format("<RTRIM(b.FirstName+' '+b.MiddleName)+' '+b.LastName LIKE '{0}'>", searchPatient));
                }

                query.Where
                    (
                        query.IsClosed == false,
                        query.SRRegistrationType != AppConstant.RegistrationType.InPatient,
                        query.IsHoldTransactionEntry == false,
                        query.IsVoid == false
                    );

                query.OrderBy
                    (
                        query.RegistrationNo.Ascending
                    );

                return query.LoadDataTable();
            }
        }

        private DataTable TransChargesDiagnostic
        {
            get
            {
                var query = new RegistrationQuery("a");
                var transQ = new TransChargesQuery("b");
                var patQ = new PatientQuery("c");
                var guarQ = new GuarantorQuery("d");
                var parQ = new ParamedicQuery("e");
                var unitQ = new ServiceUnitQuery("f");
                var sutcQ = new ServiceUnitTransactionCodeQuery("g");
                var sal = new AppStandardReferenceItemQuery("sal");

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unitQ.ServiceUnitName;

                query.es.Top = 50;
                query.es.Distinct = true;

                query.Select
                    (
                    //roomQ.RoomName,
                        query.RegistrationDate,
                        "<0 AS QueNo>",
                        transQ.ToServiceUnitID.As("ServiceUnitID"),
                        query.ParamedicID,
                        parQ.ParamedicName,
                        query.RegistrationNo,
                        patQ.MedicalNo,
                        patQ.PatientName,
                        patQ.Sex,
                        guarQ.GuarantorName,
                        group.As("Group"),
                        query.IsConsul,
                        "<'' AS BedID>",
                        sal.ItemName.As("SalutationName")
                    );

                query.InnerJoin(transQ).On(query.RegistrationNo == transQ.RegistrationNo);
                query.InnerJoin(patQ).On(query.PatientID == patQ.PatientID);
                query.InnerJoin(guarQ).On(query.GuarantorID == guarQ.GuarantorID);
                query.InnerJoin(parQ).On(query.ParamedicID == parQ.ParamedicID);
                query.InnerJoin(unitQ).On(transQ.ToServiceUnitID == unitQ.ServiceUnitID);
                query.InnerJoin(sutcQ).On(transQ.ToServiceUnitID == sutcQ.ServiceUnitID && sutcQ.SRTransactionCode == "005");
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patQ.SRSalutation == sal.ItemID);

                if (Request.QueryString["resp"] == "0" && Request.QueryString["disch"] == "0")
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.LeftJoin(qusr).On(transQ.ToServiceUnitID == qusr.ServiceUnitID);
                    query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                }
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(transQ.ToServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                    query.Where(
                        query.Or(
                            query.RegistrationNo == searchReg,
                            patQ.ReverseMedicalNo.Like(reverseMedNoSearch),
                            patQ.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                            )
                        );


                    //query.Where(
                    //    query.Or(
                    //        query.RegistrationNo == searchReg,
                    //        patQ.MedicalNo == searchReg,
                    //        patQ.OldMedicalNo == searchReg,
                    //        string.Format("< OR REPLACE(c.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //        string.Format("< OR REPLACE(c.OldMedicalNo, '-', '') = '%{0}%'>", searchReg)
                    //        )
                    //    );
                    //Helper.AddFilterMedNoOrRegNoOrPatName(query, patQ, searchReg, "registration");
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where(patQ.FullName.Like(searchPatient));
                    //query.Where(string.Format("<RTRIM(c.FirstName+' '+c.MiddleName)+' '+c.LastName LIKE '{0}'>", searchPatient));
                }

                query.Where
                    (
                        query.IsClosed == false,
                        query.IsHoldTransactionEntry == false,
                        transQ.IsOrder == true,
                        transQ.IsApproved == true,
                        query.IsVoid == false,
                        transQ.IsPackage == false,
                        transQ.PackageReferenceNo.IsNull()
                    );

                if (Request.QueryString["disch"] == "0")
                {
                    query.Where(query.Or(
                        query.SRRegistrationType != AppConstant.RegistrationType.InPatient,
                        query.DischargeDate.IsNull()));
                }

                query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.RegistrationNo.Ascending
                    );

                return query.LoadDataTable();
            }
        }

        private DataTable TransChargesOperatingTheater
        {
            get
            {
                var query = new ServiceUnitBookingQuery("a");
                var regQ = new RegistrationQuery("b");
                var patQ = new PatientQuery("c");
                var guarQ = new GuarantorQuery("d");
                var parQ = new ParamedicQuery("e");
                var unitQ = new ServiceUnitQuery("f");
                var sal = new AppStandardReferenceItemQuery("sal");

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unitQ.ServiceUnitName;

                query.es.Top = 50;
                query.es.Distinct = true;

                query.Select
                    (
                        regQ.RegistrationDate,
                        query.ServiceUnitID,
                        regQ.ParamedicID,
                        parQ.ParamedicName,
                        query.RegistrationNo,
                        patQ.MedicalNo,
                        patQ.PatientName,
                        patQ.Sex,
                        guarQ.GuarantorName,
                        group.As("Group"),
                        regQ.IsConsul,
                        "<'' AS BedID>",
                        sal.ItemName.As("SalutationName")
                    );

                query.InnerJoin(regQ).On(query.RegistrationNo == regQ.RegistrationNo);
                query.InnerJoin(patQ).On(regQ.PatientID == patQ.PatientID);
                query.InnerJoin(guarQ).On(regQ.GuarantorID == guarQ.GuarantorID);
                query.InnerJoin(parQ).On(regQ.ParamedicID == parQ.ParamedicID);
                query.InnerJoin(unitQ).On(query.ServiceUnitID == unitQ.ServiceUnitID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patQ.SRSalutation == sal.ItemID);

                if (Request.QueryString["resp"] == "0" && Request.QueryString["disch"] == "0")
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                    query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                }
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(regQ.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                    query.Where(
                        query.Or(
                            query.RegistrationNo == searchReg,
                            patQ.ReverseMedicalNo.Like(reverseMedNoSearch),
                            patQ.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                            )
                        );


                    //if (AppSession.Parameter.IsMedicalNoContainStrip)
                    //    query.Where(
                    //        query.Or(
                    //            query.RegistrationNo == searchReg,
                    //            patQ.MedicalNo == searchReg,
                    //            patQ.OldMedicalNo == searchReg,
                    //            string.Format("< OR REPLACE(c.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //            string.Format("< OR REPLACE(c.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //            )
                    //        );
                    //else
                    //    query.Where(
                    //        query.Or(
                    //            query.RegistrationNo == searchReg,
                    //            patQ.MedicalNo == searchReg,
                    //            patQ.OldMedicalNo == searchReg,
                    //            string.Format("< OR MedicalNo LIKE '%{0}%'>", searchReg),
                    //            string.Format("< OR OldMedicalNo LIKE '%{0}%'>", searchReg)
                    //            )
                    //        );
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where(patQ.FullName.Like(searchPatient));
                    //query.Where(string.Format("<RTRIM(c.FirstName+' '+c.MiddleName)+' '+c.LastName LIKE '{0}'>", searchPatient));
                }

                query.Where
                    (
                        regQ.IsClosed == false,
                        regQ.IsHoldTransactionEntry == false,
                        query.IsApproved == true,
                        regQ.DischargeDate.IsNull(),
                        regQ.IsVoid == false
                    );

                query.OrderBy
                    (
                        regQ.ParamedicID.Ascending,
                        query.RegistrationNo.Ascending
                    );

                return query.LoadDataTable();
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.DataSource = TransChargess;
            grdList.DataBind();
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var query = new TransChargesQuery("a");
            var reg = new RegistrationQuery("b");
            var patient = new PatientQuery("c");
            var unit = new ServiceUnitQuery("d");
            var usrunit = new AppUserServiceUnitQuery("e");
            var tcItem = new TransChargesItemQuery("f");
            var item = new ItemQuery("g");

            query.Select
                (
                    query.TransactionNo,
                    query.ReferenceNo,
                    query.TransactionDate,
                    unit.ServiceUnitName,
                    query.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName,
                    query.IsAutoBillTransaction,
                    query.IsApproved,
                    query.IsVoid,
                    query.IsCorrection,
                    query.IsBillProceed,
                    item.ItemName,
                    query.LastUpdateByUserID,
                    tcItem.ChargeQuantity,
                    tcItem.Price,
                    tcItem.ParamedicCollectionName,
                    tcItem.CitoAmount,
                    tcItem.DiscountAmount,
                    query.LastUpdateByUserID
                );

            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            query.InnerJoin(unit).On(query.ToServiceUnitID == unit.ServiceUnitID);
            if (Request.QueryString["disch"] != "1")
                query.InnerJoin(usrunit).On(query.ToServiceUnitID == usrunit.ServiceUnitID &&
                                        usrunit.UserID == AppSession.UserLogin.UserID);
            query.InnerJoin(tcItem).On(query.TransactionNo == tcItem.TransactionNo);
            query.InnerJoin(item).On(tcItem.ItemID == item.ItemID);


            query.Where
                (
                    //query.RegistrationNo == e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString(),
                    query.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString())),
                    query.IsCorrection == true
                );

            query.OrderBy(query.TransactionNo.Ascending, tcItem.SequenceNo.Ascending);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }
    }
}
