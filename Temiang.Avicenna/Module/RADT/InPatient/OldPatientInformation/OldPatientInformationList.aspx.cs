using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using Telerik.Reporting;
using System.Collections.Generic;
using System.Linq;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class OldPatientInformationList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.OldPatientInformation;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboRegistrationType, AppEnum.StandardReference.RegistrationType);

                var coll = new ParamedicCollection();
                coll.Query.Where(coll.Query.IsActive == true);
                coll.LoadAll();

                cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Paramedic entity in coll)
                {
                    cboParamedicID.Items.Add(new RadComboBoxItem(entity.ParamedicName, entity.ParamedicID));
                }

                cboStatus.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                cboStatus.Items.Add(new RadComboBoxItem("Patients are still treated", "1"));
                cboStatus.Items.Add(new RadComboBoxItem("Patients was out", "2"));
                cboStatus.Items.Add(new RadComboBoxItem("Open", "3"));
                cboStatus.Items.Add(new RadComboBoxItem("Closed", "4"));
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

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit) 
            {
                grd.DataSource = new String[] { };
                return;
            }
            
            var dataSource = Patients;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        private DataTable Patients
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(txtMedicalNoSearch.Text) && string.IsNullOrEmpty(txtPatientNameSearch.Text) && txtDateOfBirth.IsEmpty && string.IsNullOrEmpty(txtPhoneNo.Text) && 
                    string.IsNullOrEmpty(txtAddress.Text) && string.IsNullOrEmpty(txtParentSpouseName.Text) && txtRegistrationDate1.IsEmpty && txtRegistrationDate2.IsEmpty && string.IsNullOrEmpty(cboRegistrationType.SelectedValue) &&
                    string.IsNullOrEmpty(cboParamedicID.SelectedValue) && string.IsNullOrEmpty(cboStatus.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Patient")) return null;

                var query = new PatientQuery("a");
                var sal = new AppStandardReferenceItemQuery("sal");
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & query.SRSalutation == sal.ItemID);

                query.Select
                    (
                        query.PatientID,
                        query.MedicalNo.Coalesce("''"),
                        query.PatientName.Coalesce("''"),
                        query.Address.Coalesce("''"),
                        query.PhoneNo,
                        query.MobilePhoneNo,
                        query.DateOfBirth,
                        query.Sex,
                        @"<'' AS AgeInString>",
                        sal.ItemName.As("SalutationName"),
                        query.IsAlive,
                        query.DeceasedDateTime
                    );
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.es.Distinct = true;

                if (!(string.IsNullOrEmpty(txtMedicalNoSearch.Text)))
                {
                    string searchMedNo = Helper.EscapeQuery(txtMedicalNoSearch.Text);
                    if (AppSession.Parameter.IsMedicalNoContainStrip)
                        query.Where(
                            query.Or(
                                query.MedicalNo == searchMedNo,
                                query.OldMedicalNo == searchMedNo,
                                string.Format("< OR REPLACE(a.MedicalNo, '-', '') LIKE '%{0}%'>", searchMedNo),
                                string.Format("< OR REPLACE(a.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchMedNo)
                                )
                            );
                    else
                        query.Where(
                            query.Or(
                                query.MedicalNo == searchMedNo,
                                query.OldMedicalNo == searchMedNo,
                                string.Format("< OR a.MedicalNo LIKE '%{0}%'>", searchMedNo),
                                string.Format("< OR a.OldMedicalNo LIKE '%{0}%'>", searchMedNo)
                                )
                            );
                }

                if (!(string.IsNullOrEmpty(txtPatientNameSearch.Text)))
                {
                    if (txtPatientNameSearch.Text.Trim().Contains(" "))
                    {
                        var searchs = Helper.EscapeQuery(txtPatientNameSearch.Text).Trim().Split(' ');
                        foreach (var search in searchs)
                        {
                            var searchLike = "%" + search + "%";
                            query.Where(query.Or(query.FirstName.Like(searchLike), query.LastName.Like(searchLike),
                                                 query.MiddleName.Like(searchLike)));
                        }
                    }
                    else
                    {
                        var searchLike = "%" + Helper.EscapeQuery(txtPatientNameSearch.Text).Trim() + "%";
                        query.Where(query.Or(query.FirstName.Like(searchLike), query.LastName.Like(searchLike),
                                             query.MiddleName.Like(searchLike)));
                    }
                }

                if (!string.IsNullOrEmpty(txtAddress.Text))
                {
                    if (txtAddress.Text.Trim().Contains(" "))
                    {
                        var searchs = Helper.EscapeQuery(txtAddress.Text).Trim().Split(' ');
                        foreach (var search in searchs)
                        {
                            var searchLike = "%" + search + "%";
                            query.Where(
                                query.Or(
                                    query.StreetName.Like(searchLike),
                                    query.City.Like(searchLike),
                                    query.County.Like(searchLike),
                                    query.ZipCode.Like(searchLike)
                                    )
                                );
                        }
                    }
                    else
                    {
                        var searchLike = "%" + Helper.EscapeQuery(txtAddress.Text).Trim() + "%";
                        query.Where(
                            query.Or(
                                    query.StreetName.Like(searchLike),
                                    query.City.Like(searchLike),
                                    query.County.Like(searchLike),
                                    query.ZipCode.Like(searchLike)
                                )
                            );
                    }
                }
                if (!string.IsNullOrEmpty(txtParentSpouseName.Text))
                {
                    var searchLike = "%" + txtParentSpouseName.Text.Trim() + "%";
                    query.Where(query.ParentSpouseName.Like(searchLike));
                }
                if (!(txtDateOfBirth.IsEmpty))
                    query.Where(query.DateOfBirth == txtDateOfBirth.SelectedDate);
                if (!(string.IsNullOrEmpty(txtPhoneNo.Text)))
                {
                    var searchLike = "%" + txtPhoneNo.Text.Trim() + "%";
                    query.Where(
                            query.Or(
                                    query.PhoneNo.Like(searchLike),
                                    query.MobilePhoneNo.Like(searchLike)
                                )
                            );
                }
                if (!txtRegistrationDate1.IsEmpty || cboRegistrationType.SelectedValue != string.Empty || cboParamedicID.SelectedValue != string.Empty || !string.IsNullOrEmpty(cboStatus.SelectedValue))
                {
                    var regQ = new RegistrationQuery("b");
                    query.InnerJoin(regQ).On(query.PatientID == regQ.PatientID & regQ.IsFromDispensary == false & regQ.IsNonPatient == false);

                    if (!txtRegistrationDate1.IsEmpty && !txtRegistrationDate2.IsEmpty)
                        query.Where(regQ.RegistrationDate.Between(txtRegistrationDate1.SelectedDate, txtRegistrationDate2.SelectedDate));
                    if (cboRegistrationType.SelectedValue != string.Empty)
                        query.Where(regQ.SRRegistrationType == cboRegistrationType.SelectedValue);
                    if (cboParamedicID.SelectedValue != string.Empty)
                        query.Where(regQ.ParamedicID == cboParamedicID.SelectedValue);
                    if (!string.IsNullOrEmpty(cboStatus.SelectedValue))
                    {
                        if (cboStatus.SelectedValue == "1")
                            query.Where(regQ.DischargeDate.IsNull(), regQ.SRRegistrationType == AppConstant.RegistrationType.InPatient);
                        else if (cboStatus.SelectedValue == "2")
                            query.Where(regQ.Or(regQ.DischargeDate.IsNotNull(), regQ.SRRegistrationType != AppConstant.RegistrationType.InPatient));
                        else if (cboStatus.SelectedValue == "3")
                            query.Where(regQ.IsClosed == false);
                        else if (cboStatus.SelectedValue == "4")
                            query.Where(regQ.IsClosed == true);
                    }
                }
                
                query.Where(query.IsActive == true, query.IsNonPatient == false);
                query.OrderBy(query.MedicalNo.Coalesce("''").Ascending);

                DataTable dtb = query.LoadDataTable();

                foreach (DataRow row in dtb.Rows)
                {
                    var deceasedDate = Convert.ToDateTime(row["DeceasedDateTime"] == DBNull.Value ? DateTime.Now : row["DeceasedDateTime"]);
                    var ageInYear = row["IsAlive"].ToBoolean() == true ? Helper.GetAgeInYear(Convert.ToDateTime(row["DateOfBirth"])) : Helper.GetAgeInYear(Convert.ToDateTime(row["DateOfBirth"]), deceasedDate);
                    var ageInMonth = row["IsAlive"].ToBoolean() == true ? Helper.GetAgeInMonth(Convert.ToDateTime(row["DateOfBirth"])) : Helper.GetAgeInMonth(Convert.ToDateTime(row["DateOfBirth"]), deceasedDate);
                    var ageInDay = row["IsAlive"].ToBoolean() == true ? Helper.GetAgeInDay(Convert.ToDateTime(row["DateOfBirth"])) : Helper.GetAgeInDay(Convert.ToDateTime(row["DateOfBirth"]), deceasedDate);

                    if (ageInYear > 0)
                        row["AgeInString"] = ageInYear.ToString() + " y";
                    else if (ageInMonth > 0)
                        row["AgeInString"] = ageInMonth.ToString() + " m";
                    else
                        row["AgeInString"] = ageInDay.ToString() + " d";
                }

                dtb.AcceptChanges();

                return dtb;
            }
        }

        protected void btnSearchPatient_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.DataSource = Patients;
            grdList.CurrentPageIndex = 0;
            grdList.Rebind();
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var query = new RegistrationQuery("a");
            var suQ = new ServiceUnitQuery("b");
            var parQ = new ParamedicQuery("c");
            var dmQ = new AppStandardReferenceItemQuery("d");
            var dcQ = new AppStandardReferenceItemQuery("e");
            var mbQ = new MergeBillingQuery("f");
            var bQ = new BedQuery("g");
            var srQ = new ServiceRoomQuery("h");


            query.InnerJoin(suQ).On(query.ServiceUnitID == suQ.ServiceUnitID);
            query.InnerJoin(parQ).On(query.ParamedicID == parQ.ParamedicID);
            query.LeftJoin(dmQ).On(query.SRDischargeMethod == dmQ.ItemID && dmQ.StandardReferenceID == "DischargeMethod");
            query.LeftJoin(dcQ).On(query.SRDischargeCondition == dcQ.ItemID &&
                                   dcQ.StandardReferenceID == "DischargeCondition");
            query.LeftJoin(mbQ).On(query.RegistrationNo == mbQ.RegistrationNo);
            query.LeftJoin(bQ).On(bQ.BedID == query.BedID && bQ.RegistrationNo == query.RegistrationNo && bQ.SRBedStatus == AppSession.Parameter.BedStatusPending.ToString());
            query.LeftJoin(srQ).On(srQ.RoomID == query.RoomID);


            query.es.Top = 30;
            query.Select
            (
                query.PatientID,
                query.RegistrationNo,
                @"<CAST(CONVERT(VARCHAR(10), a.RegistrationDate, 112) + ' ' + a.RegistrationTime AS DATETIME) AS RegistrationDateTime>",
                parQ.ParamedicName,
                suQ.ServiceUnitName,
                srQ.RoomName,
                query.BedID,
                @"<CAST(CONVERT(VARCHAR(10), a.DischargeDate, 112) + ' ' + a.DischargeTime AS DATETIME) AS DischargeDate>",
                dmQ.ItemName.As("DischargeMethod"),
                dcQ.ItemName.As("DischargeCondition"),
                @"<CASE WHEN ISNULL(a.DeathCertificateNo, '') = '' THEN a.DischargeNotes ELSE a.DischargeNotes + ' (DCN : ' + a.DeathCertificateNo + ')' END AS DischargeNotes>",
                //query.DischargeNotes,
                query.IsClosed,
                query.IsHoldTransactionEntry,
                query.IsConsul,
                mbQ.FromRegistrationNo,
                query.IsVoid, 
                query.Notes,
                @"<CASE WHEN g.BedID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END IsBedStatusPending>"
            );

            var patids = new List<string>();
            patids.Add(e.DetailTableView.ParentItem.GetDataKeyValue("PatientID").ToString());
            var prColl = new PatientRelatedCollection();
            prColl.Query.Where(prColl.Query.PatientID == patids[0]);
            if (prColl.LoadAll()) {
                patids.AddRange(prColl.Select(x => x.RelatedPatientID));
            }

            query.Where(query.PatientID.In(patids),
                        query.IsFromDispensary == false, query.IsNonPatient == false);

            if (!txtRegistrationDate1.IsEmpty && !txtRegistrationDate2.IsEmpty)
                query.Where(query.RegistrationDate.Between(txtRegistrationDate1.SelectedDate, txtRegistrationDate2.SelectedDate));
            if (cboRegistrationType.SelectedValue != string.Empty)
                query.Where(query.SRRegistrationType == cboRegistrationType.SelectedValue);
            if (cboParamedicID.SelectedValue != string.Empty)
                query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
            if (!string.IsNullOrEmpty(cboStatus.SelectedValue))
            {
                if (cboStatus.SelectedValue == "1")
                    query.Where(query.DischargeDate.IsNull(), query.SRRegistrationType == AppConstant.RegistrationType.InPatient);
                else if (cboStatus.SelectedValue == "2")
                    query.Where(query.Or(query.DischargeDate.IsNotNull(),
                                        query.SRRegistrationType != AppConstant.RegistrationType.InPatient));
                else if (cboStatus.SelectedValue == "3")
                    query.Where(query.IsClosed == false);
                else if (cboStatus.SelectedValue == "4")
                    query.Where(query.IsClosed == true);
            }

            query.OrderBy(query.RegistrationDate.Descending, query.RegistrationTime.Descending);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }
    }
}
