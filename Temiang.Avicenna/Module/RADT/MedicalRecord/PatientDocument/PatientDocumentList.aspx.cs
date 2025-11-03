using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using static Temiang.Avicenna.Common.SirsDinkes.Eis.Json.KetersediaanBed.Get.Response;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class PatientDocumentList : BasePage
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

            ProgramID = AppConstant.Program.PatientDocument;

            if (!IsPostBack)
            {
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
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
        }

        private DataTable Patients
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(txtMedicalNoSearch.Text) && string.IsNullOrEmpty(txtRegistrationNoSearch.Text) && string.IsNullOrEmpty(txtPatientNameSearch.Text) && txtDateOfBirth.IsEmpty && string.IsNullOrEmpty(txtPhoneNo.Text) && string.IsNullOrEmpty(txtAddress.Text) && string.IsNullOrEmpty(txtParentSpouseName.Text);
                if (!ValidateSearch(isEmptyFilter, "Patient Document")) return null;

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
                        query.OldMedicalNo.Coalesce("''")
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

                if (!(string.IsNullOrEmpty(txtRegistrationNoSearch.Text)))
                {
                    var reg = new Registration();
                    if (reg.LoadByPrimaryKey(txtRegistrationNoSearch.Text))
                    {
                        query.Where(query.PatientID == reg.PatientID);
                    }
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
                
                query.Where(query.IsActive == true, query.IsNonPatient == false);
                query.OrderBy(query.MedicalNo.Coalesce("''").Ascending);

                DataTable dtb = query.LoadDataTable();

                foreach (DataRow row in dtb.Rows)
                {
                    var ageInYear = Helper.GetAgeInYear(Convert.ToDateTime(row["DateOfBirth"]));
                    var ageInMonth = Helper.GetAgeInMonth(Convert.ToDateTime(row["DateOfBirth"]));
                    var ageInDay = Helper.GetAgeInDay(Convert.ToDateTime(row["DateOfBirth"]));

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

    }
}
