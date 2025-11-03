using System;
using System.Data;
using System.Web.UI;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class MedicalRecordHistoryList : BasePage
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

            ProgramID = AppConstant.Program.MedicalRecordHistory;
            IsShowValueFromCookie = true;
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

        private DataTable Patients
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(txtMedicalNo.Text) && string.IsNullOrEmpty(txtPatientSearch.Text) && txtBirthDate.IsEmpty && string.IsNullOrEmpty(txtAddress.Text);
                if (!ValidateSearch(isEmptyFilter, "Patient")) return null;

                var query = new PatientQuery("a");
                var sal = new AppStandardReferenceItemQuery("sal");
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & query.SRSalutation == sal.ItemID);

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        query.PatientID,
                        query.MedicalNo,
                        query.PatientName,
                        query.Sex,
                        query.DateOfBirth,
                        query.Address,
                        query.OldMedicalNo,
                        sal.ItemName.As("SalutationName"),
                        query.IsAlive
                    );

                query.Where(query.IsActive == true, query.IsNonPatient == false);

                if (!txtMedicalNo.Text.Trim().Equals(string.Empty))
                {
                    string searchPatient = "%" + txtMedicalNo.Text.Trim().Replace("'", "''") + "%";
                    if (AppSession.Parameter.IsMedicalNoContainStrip)
                        query.Where(string.Format("<REPLACE(a.MedicalNo,'-','') LIKE REPLACE('{0}','-','') OR REPLACE(a.OldMedicalNo,'-','') LIKE REPLACE('{0}','-','')>", searchPatient));
                    else
                        query.Where(string.Format("<a.MedicalNo LIKE '%{0}%' OR a.OldMedicalNo LIKE '%{0}%'>", searchPatient));
                }

                if (!txtPatientSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchPatient = "%" + txtPatientSearch.Text.Trim().Replace("'", "''") + "%";
                    query.Where
                        (
                            string.Format("<RTRIM(a.FirstName+' '+a.MiddleName)+' '+a.LastName LIKE '{0}'>", searchPatient)
                        );
                }

                if (!txtBirthDate.IsEmpty)
                    query.Where(query.DateOfBirth == txtBirthDate.SelectedDate.Value.Date);
                if (!txtAddress.Text.Trim().Equals(string.Empty))
                {
                    char[] c = { ' ' };
                    string[] sSearch = txtAddress.Text.Trim().Split(c);

                    string sQuery = string.Empty;
                    foreach (var s in sSearch)
                    {
                        if (sQuery != string.Empty) sQuery += " AND ";
                        sQuery += string.Format(" RTRIM(ISNULL(StreetName,'')) + RTRIM(' ' + ISNULL(City,'')) + RTRIM(' ' + ISNULL(County,'')) + RTRIM(' ' + ISNULL(ZipCode,'')) LIKE '%{0}%' ", s);
                    }
                    sQuery = " ( " + sQuery + " ) ";
                    query.Where(sQuery);
                }

                query.OrderBy(query.PatientID.Descending);

                return query.LoadDataTable();
            }
        }

        protected void btnSearchPatient_Click(object sender, ImageClickEventArgs e)
        {
            grdPatient.Rebind();
        }

        protected void grdPatient_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
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
    }
}