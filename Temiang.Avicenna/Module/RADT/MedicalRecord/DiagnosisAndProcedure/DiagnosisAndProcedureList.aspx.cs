using System;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class DiagnosisAndProcedureList : BasePage
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

            ProgramID = AppConstant.Program.PatientDiagnosisAndProcedureEntry;

            if (!IsPostBack)
            {
                //service unit
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");

                query.Where(
                    query.SRRegistrationType.In(
                        AppConstant.RegistrationType.EmergencyPatient,
                        AppConstant.RegistrationType.InPatient,
                        AppConstant.RegistrationType.OutPatient,
                        AppConstant.RegistrationType.MedicalCheckUp
                        ),
                    query.IsActive == true
                    );

                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                PopulatePhysician();

                if (AppSession.Parameter.IsDiagAndProcListFilterParameter)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["pmid"]))
                        cboParamedicID.SelectedValue = Request.QueryString["pmid"];

                    if (!string.IsNullOrEmpty(Request.QueryString["cid"]))
                        cboServiceUnitID.SelectedValue = Request.QueryString["cid"].Trim();
                    else
                    {
                        var usr = new AppUser();
                        usr.LoadByPrimaryKey(AppSession.UserLogin.UserID);
                        if (!string.IsNullOrEmpty(usr.ServiceUnitID))
                        {
                            var usrcoll = new AppUserServiceUnitCollection();
                            usrcoll.Query.Where(
                                usrcoll.Query.UserID == AppSession.UserLogin.UserID &&
                                usrcoll.Query.ServiceUnitID == usr.ServiceUnitID
                                );
                            usrcoll.LoadAll();

                            if (usrcoll.Count > 0)
                                cboServiceUnitID.SelectedValue = usr.ServiceUnitID;
                        }
                    }
                }

                cboStatus.Items.Add(new RadComboBoxItem("", ""));
                cboStatus.Items.Add(new RadComboBoxItem("Diagnosis - Fill up", "1"));
                cboStatus.Items.Add(new RadComboBoxItem("Diagnosis - Empty", "2"));
                cboStatus.Items.Add(new RadComboBoxItem("Procedure - Fill up", "3"));
                cboStatus.Items.Add(new RadComboBoxItem("Procedure - Empty", "4"));
                cboStatus.Items.Add(new RadComboBoxItem("SOAP - Fill up", "5"));
                cboStatus.Items.Add(new RadComboBoxItem("SOAP - Empty", "6"));
                cboStatus.Items.Add(new RadComboBoxItem("Code ICD X - Empty", "7"));

                if (!string.IsNullOrEmpty(Request.QueryString["pst"]))
                    cboStatus.SelectedValue = Request.QueryString["pst"].Trim();

                if (AppSession.Parameter.DayLimitDefaultDiagAndProcList >= 0)
                {
                    txtFromDate.SelectedDate = DateTime.Today.AddDays(-1 * AppSession.Parameter.DayLimitDefaultDiagAndProcList);
                    txtToDate.SelectedDate = DateTime.Today;
                }
            }
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

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            if (AppSession.Parameter.IsDiagAndProcListRestoreValueFromCookie)
                if (!IsPostBack) RestoreValueFromCookie();
        }

        private void PopulatePhysician()
        {
            var serviceUnitId = cboServiceUnitID.SelectedValue;
            cboParamedicID.Items.Clear();

            var qPar = new ParamedicQuery("a");

            var su = new ServiceUnit();
            if (serviceUnitId != string.Empty)
            {
                if (su.LoadByPrimaryKey(serviceUnitId))
                {
                    if (su.SRRegistrationType == AppConstant.RegistrationType.OutPatient)
                    {
                        var qSUP = new ServiceUnitParamedicQuery("b");
                        qPar.InnerJoin(qSUP).On(qPar.ParamedicID == qSUP.ParamedicID);
                        qPar.Where(qSUP.ServiceUnitID == su.ServiceUnitID);
                    }
                }
            }

            qPar.Select(qPar.ParamedicID, qPar.ParamedicName);
            qPar.OrderBy(qPar.ParamedicName.Ascending);
            DataTable dtb = qPar.LoadDataTable();
            cboParamedicID.Items.Add(new RadComboBoxItem("", ""));
            foreach (DataRow row in dtb.Rows)
            {
                cboParamedicID.Items.Add(new RadComboBoxItem(row["ParamedicName"].ToString(), row["ParamedicID"].ToString().Trim()));
            }
        }

        protected void grdPatient_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit) {
                grd.DataSource = new String[] { };
                return; 
            }
            
            var dataSource = Registrations;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB")
                {
                    dataSource.DefaultView.Sort = "RegistrationDate desc, RegistrationTime desc";
                    dataSource = dataSource.DefaultView.ToTable();

                    grd.Columns[1].HeaderText = "Disc. Date";
                }

                grd.DataSource = dataSource;
            }

            //var dtb = Registrations;

            //if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB")
            //{
            //    dtb.DefaultView.Sort = "RegistrationDate desc, RegistrationTime desc";
            //    dtb = dtb.DefaultView.ToTable();

            //    grdPatient.Columns[1].HeaderText = "Disc. Date";
            //}

            //grdPatient.DataSource = dtb;
        }

        private DataTable Registrations
        {
            get
            {
                var isEmptyFilter = txtFromDate.IsEmpty && txtToDate.IsEmpty && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) &&
                    string.IsNullOrEmpty(cboParamedicID.SelectedValue) && string.IsNullOrEmpty(txtMedicalNo.Text) && string.IsNullOrEmpty(txtPatientName.Text) && string.IsNullOrEmpty(cboGuarantorID.SelectedValue) && 
                    string.IsNullOrEmpty(cboStatus.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Registration")) return null;

                var qr = new RegistrationQuery("r");

                var qp = new PatientQuery("p");
                qr.InnerJoin(qp).On(qr.PatientID == qp.PatientID);

                var qm = new ParamedicQuery("m");
                qr.LeftJoin(qm).On(qr.ParamedicID == qm.ParamedicID);

                var unit = new ServiceUnitQuery("s");
                qr.LeftJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);

                var room = new ServiceRoomQuery("d");
                qr.LeftJoin(room).On(qr.RoomID == room.RoomID);

                var cq = new ClassQuery("e");
                qr.LeftJoin(cq).On(qr.ClassID == cq.ClassID);

                var grt = new GuarantorQuery("g");
                qr.LeftJoin(grt).On(qr.GuarantorID == grt.GuarantorID);

                var asr = new AppStandardReferenceItemQuery("h");
                qr.LeftJoin(asr).On(asr.StandardReferenceID == "title" & qp.SRSalutation == asr.ItemID);

                qr.Where
                (
                    qr.IsFromDispensary == false,
                    qr.IsNonPatient == false,
                    qr.IsVoid == false
                );

                bool isMaxResultRecord = true;
                bool isFilter = false;
                if (!txtFromDate.IsEmpty && !txtToDate.IsEmpty)
                {
                    qr.Where(qr.Or(
                        qr.And(qr.SRRegistrationType == "IPR",
                            qr.DischargeDate.Between(txtFromDate.SelectedDate, txtToDate.SelectedDate)),
                        qr.And(qr.SRRegistrationType != "IPR",
                            qr.RegistrationDate.Between(txtFromDate.SelectedDate, txtToDate.SelectedDate))));
                    isMaxResultRecord = false;
                    isFilter = true;
                }
                if (txtRegistrationNo.Text != string.Empty)
                {
                    qr.Where(qr.RegistrationNo == Helper.EscapeQuery(txtRegistrationNo.Text));
                    isMaxResultRecord = false;
                    isFilter = true;
                }
                if (txtMedicalNo.Text != string.Empty)
                {
                    string searchMedNo = Helper.EscapeQuery(txtMedicalNo.Text);
                    if (AppSession.Parameter.IsMedicalNoContainStrip)
                        qr.Where(
                            qr.Or(
                                qp.MedicalNo == searchMedNo,
                                string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchMedNo)
                                )
                            );
                    else
                        qr.Where(
                            qr.Or(
                                qp.MedicalNo == searchMedNo,
                                string.Format("< OR p.MedicalNo LIKE '%{0}%'>", searchMedNo)
                                )
                            );
                    isMaxResultRecord = false;
                    isFilter = true;
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    qr.Where
                        (
                         string.Format("<RTRIM(p.FirstName+' '+p.MiddleName)+' '+p.LastName LIKE '{0}'>", searchPatient)
                        );
                    isMaxResultRecord = false;
                    isFilter = true;
                }
                if (cboServiceUnitID.SelectedValue != string.Empty)
                {
                    qr.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);
                    isFilter = true;
                }
                if (cboParamedicID.SelectedValue != string.Empty)
                {
                    qr.Where(qr.ParamedicID == cboParamedicID.SelectedValue);
                    isFilter = true;
                }
                if (!string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
                {
                    qr.Where(qr.GuarantorID == cboGuarantorID.SelectedValue);
                    isFilter = true;
                }
                if (!string.IsNullOrEmpty(cboStatus.SelectedValue))
                {
                    switch (cboStatus.SelectedValue)
                    {
                        case "1":
                            qr.Where(@"<((SELECT COUNT(*) FROM EpisodeDiagnose ed
                                          WHERE ed.RegistrationNo = r.RegistrationNo AND ed.IsVoid = 0 AND ed.DiagnoseID <> '') > 0)>");

                            break;
                        case "2":
                            qr.Where(@"<((SELECT COUNT(*) FROM EpisodeDiagnose ed
                                          WHERE ed.RegistrationNo = r.RegistrationNo AND ed.IsVoid = 0 AND ed.DiagnoseID <> '') = 0)>");
                            break;
                        case "3":
                            qr.Where(@"<((SELECT COUNT(*) FROM EpisodeProcedure ep
                                          WHERE ep.RegistrationNo = r.RegistrationNo AND ep.IsVoid = 0 AND ep.ProcedureID <> '') > 0)>");
                            break;
                        case "4":
                            qr.Where(@"<((SELECT COUNT(*) FROM EpisodeProcedure ep
                                          WHERE ep.RegistrationNo = r.RegistrationNo AND ep.IsVoid = 0 AND ep.ProcedureID <> '') = 0) 
                                    AND ((SELECT COUNT(*) FROM ServiceUnitBooking AS sub
                                          WHERE sub.RegistrationNo = r.RegistrationNo AND sub.IsApproved = 1) > 0)>");
                            break;
                        case "5":
                            qr.Where(@"<((SELECT COUNT(*) FROM RegistrationInfoMedic rim
                                          WHERE rim.RegistrationNo = r.RegistrationNo AND rim.SRMedicalNotesInputType = 'SOAP') > 0)>");
                            break;
                        case "6":
                            qr.Where(@"<((SELECT COUNT(*) FROM RegistrationInfoMedic rim
                                          WHERE rim.RegistrationNo = r.RegistrationNo AND rim.SRMedicalNotesInputType = 'SOAP') = 0)>");
                            break;
                        case "7":
                            qr.Where(@"<((SELECT COUNT(*) FROM EpisodeDiagnose ed
                                          WHERE ed.RegistrationNo = r.RegistrationNo AND ed.IsVoid = 0 AND ed.DiagnoseID <> '') = 0)>");
                            break;
                    }
                    isFilter = true;
                }
                if (txtFromDate.IsEmpty && txtToDate.IsEmpty && !isFilter)
                {
                    DateTime date = Convert.ToDateTime("1/1/1910");
                    qr.Where(qr.RegistrationDate.Between(date, date));
                }

                if (isMaxResultRecord)
                    qr.es.Top = AppSession.Parameter.MaxResultRecord;

                if (AppSession.Parameter.IsDisplayRegDateTimeUseCreateDate)
                {
                    qr.Select(
                            "<CAST(r.LastCreateDateTime as datetime) as RegistrationDate>",
                            "<CAST(convert(char(5), r.LastCreateDateTime, 108) as varchar(5)) as RegistrationTime>"
                            );
                }
                else
                {
                    qr.Select(
                        "<CAST(r.RegistrationDate as datetime) + r.RegistrationTime as RegistrationDate>",
                        "<CAST(r.RegistrationTime as varchar(5)) as RegistrationTime>"
                        );
                }

                qr.Select
                   (
                       qr.PatientID,
                       qr.RegistrationNo,
                       qr.IsClosed,
                       qr.IsVoid,
                       qr.IsFromDispensary,
                       qp.MedicalNo,
                       asr.ItemName.As("SalutationName"),
                       qp.PatientName,
                       qp.Sex,
                       qp.DateOfBirth,
                       qm.ParamedicName,
                       qr.ServiceUnitID,
                       unit.ServiceUnitName,
                       room.RoomName,
                       cq.ClassName,
                       qr.IsConsul,
                       qr.SRRegistrationType,
                       qr.IsNewPatient,
                       qr.LastCreateUserID,
                       @"<CAST(CASE WHEN (SELECT COUNT(*)
                                          FROM RegistrationInfoMedic rim
                                          WHERE rim.RegistrationNo = r.RegistrationNo AND rim.SRMedicalNotesInputType = 'SOAP') > 0 THEN 1 
				                    ELSE 0 END AS BIT) AS IsSoape>",
                       @"<CAST(CASE WHEN (SELECT COUNT(*)
                                          FROM EpisodeDiagnose ed
                                          WHERE ed.RegistrationNo = r.RegistrationNo AND ed.IsVoid = 0 AND ed.DiagnoseID <> '') > 0 THEN 1 
				                    ELSE 0 END AS BIT) AS IsDiagnosis>",
                       @"<CAST(CASE WHEN (SELECT COUNT(*)
                                          FROM EpisodeProcedure ep
                                          WHERE ep.RegistrationNo = r.RegistrationNo AND ep.IsVoid = 0 AND ep.ProcedureID <> '') > 0 THEN 1 
				                    ELSE 0 END AS BIT) AS IsProcedure>",
                        grt.GuarantorName
                   );

                if (AppSession.Parameter.IsDisplayRegDateTimeUseCreateDate)
                {
                    if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB")
                    {
                        qr.OrderBy(qr.LastCreateDateTime.Descending, qr.RegistrationNo.Descending);
                    }
                    else
                    {
                        qr.OrderBy(qr.LastCreateDateTime.Descending,
                            qm.ParamedicName.Ascending,
                            qr.RegistrationNo.Ascending);
                    }
                }
                else
                {
                    qr.OrderBy(qr.RegistrationDate.Descending, qm.ParamedicName.Ascending, qr.RegistrationNo.Ascending);
                }

                DataTable dtb = qr.LoadDataTable();

                return dtb;
            }
        }

        protected void btnSearchPatient_Click(object sender, ImageClickEventArgs e)
        {
            if (AppSession.Parameter.IsDiagAndProcListRestoreValueFromCookie)
                SaveValueToCookie();

            grdPatient.Rebind();
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
                    query.IsActive == true
                );

            query.OrderBy(query.GuarantorName.Ascending);

            cboGuarantorID.DataSource = query.LoadDataTable();
            cboGuarantorID.DataBind();
        }
    }
}