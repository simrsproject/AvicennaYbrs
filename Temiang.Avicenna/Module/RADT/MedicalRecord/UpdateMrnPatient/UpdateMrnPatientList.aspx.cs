using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class UpdateMrnPatientList : BasePage
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

            ProgramID = AppConstant.Program.UpdateMrnPatient;

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

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (!(source is RadGrid))
                return;

            if (eventArgument == "rebind")
                grdPatient.Rebind();
        }

        private DataTable Patients
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(txtPatientSearch.Text) && txtDateOfBirth.IsEmpty && string.IsNullOrEmpty(txtAddress.Text) && string.IsNullOrEmpty(txtPhoneNo.Text);
                if (!ValidateSearch(isEmptyFilter, "Patient")) return null;

                var qr = new PatientQuery("a");
                var qc = new AppStandardReferenceItemQuery("c");
                var qu = new AppUserQuery("u");
                qr.LeftJoin(qc).On(qc.StandardReferenceID == "Salutation" && qc.ItemID == qr.SRSalutation);
                qr.LeftJoin(qu).On(qu.UserID == qr.CreatedByUserID);

                qr.es.Top = AppSession.Parameter.MaxResultRecord;
                qr.Select(
                    qr.PatientID,
                    qr.MedicalNo,
                    qr.OldMedicalNo,
                    qr.PatientName,
                    qr.Sex,
                    qr.Address,
                    qr.PhoneNo,
                    qr.MobilePhoneNo,
                    qr.DateOfBirth,
                    qc.ItemName.As("Salutation"),
                    qr.ZipCode.Coalesce("''"), 
                    qr.IsActive,
                    qr.CreatedDateTime,
                    qu.UserName.As("CreatedByUserName")
                    );

                qr.Where(qr.IsActive == true, qr.IsNonPatient == false);

                if (!txtDateOfBirth.IsEmpty)
                    qr.Where(qr.DateOfBirth == txtDateOfBirth.SelectedDate);

                if (!string.IsNullOrEmpty(txtPhoneNo.Text))
                    qr.Where(
                        qr.Or(
                            qr.PhoneNo == txtPhoneNo.Text,
                            qr.MobilePhoneNo == txtPhoneNo.Text
                            )
                        );

                if (txtPatientSearch.Text != string.Empty)
                {
                    string sNumber = txtPatientSearch.Text.Replace("-", "").Replace("/", "");
                    int n;
                    bool isNumeric = int.TryParse(sNumber, out n);
                    if (isNumeric)
                    {
                        if (AppSession.Parameter.IsMedicalNoContainStrip)
                            // for fast search: numeric is medical no
                            qr.Where(qr.Or(
                                string.Format("<REPLACE(a.MedicalNo, '-', '') LIKE '%{0}%'>", sNumber),
                                string.Format("< OR REPLACE(a.OldMedicalNo, '-', '') LIKE '%{0}%'>", sNumber)));
                        else
                            qr.Where(qr.Or(
                                string.Format("<a.MedicalNo LIKE '%{0}%'>", sNumber),
                                string.Format("< OR a.OldMedicalNo LIKE '%{0}%'>", sNumber)));
                    }
                    else
                    {
                        string searchTextContain = string.Format("%{0}%", txtPatientSearch.Text);
                        if (AppSession.Parameter.IsMedicalNoContainStrip)
                            qr.Where(
                                qr.Or(
                                    qr.PatientID == txtPatientSearch.Text,
                                    qr.MedicalNo.Like(searchTextContain),
                                    qr.OldMedicalNo.Like(searchTextContain),
                                    string.Format("< OR RTRIM(LTRIM(RTRIM(LTRIM(a.FirstName + ' ' + a.MiddleName)) + ' ' + a.LastName)) LIKE '%{0}%'>", txtPatientSearch.Text),
                                    string.Format("< OR REPLACE(a.MedicalNo, '-', '') LIKE '%{0}%'>", txtPatientSearch.Text),
                                    string.Format("< OR REPLACE(a.OldMedicalNo, '-', '') LIKE '%{0}%'>", txtPatientSearch.Text)
                                )
                            );
                        else
                            qr.Where(
                                qr.Or(
                                    qr.PatientID == txtPatientSearch.Text,
                                    qr.MedicalNo.Like(searchTextContain),
                                    qr.OldMedicalNo.Like(searchTextContain),
                                    string.Format("< OR RTRIM(LTRIM(RTRIM(LTRIM(a.FirstName + ' ' + a.MiddleName)) + ' ' + a.LastName)) LIKE '%{0}%'>", txtPatientSearch.Text),
                                    string.Format("< OR a.MedicalNo LIKE '%{0}%'>", txtPatientSearch.Text),
                                    string.Format("< OR a.OldMedicalNo LIKE '%{0}%'>", txtPatientSearch.Text)
                                )
                            );
                    }
                }

                if (!string.IsNullOrEmpty(txtAddress.Text))
                {
                    if (txtAddress.Text.Trim().Contains(" "))
                    {
                        var searchs = txtAddress.Text.Split(' ');
                        foreach (var searchLike in searchs.Select(search => "%" + search + "%"))
                        {
                            qr.Where(
                                qr.Or(
                                    qr.StreetName.Like(searchLike),
                                    qr.City.Like(searchLike),
                                    qr.County.Like(searchLike),
                                    qr.ZipCode.Like(searchLike)
                                    )
                                );
                        }
                    }
                    else
                    {
                        var searchLike = "%" + txtAddress.Text + "%";
                        qr.Where(
                            qr.Or(
                                    qr.StreetName.Like(searchLike),
                                    qr.City.Like(searchLike),
                                    qr.County.Like(searchLike),
                                    qr.ZipCode.Like(searchLike)
                                )
                            );
                    }
                }

                qr.OrderBy(txtPatientSearch.Text != string.Empty ? qr.FirstName.Ascending : qr.MedicalNo.Descending);

                return qr.LoadDataTable();
            }
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

        protected void grdPatient_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            //Load record
            var hist = new PatientMRNNameHistoryQuery("a");
            var usr = new AppUserQuery("b");
            
            hist.InnerJoin(usr).On(usr.UserID == hist.UpdateByUserID);

            hist.Select(hist, usr.UserName.As("UpdateByUserName"));
            hist.Where(hist.PatientID == e.DetailTableView.ParentItem.GetDataKeyValue("PatientID").ToString());
            hist.OrderBy(hist.UpdateDateTime.Ascending);

            //Apply
            e.DetailTableView.DataSource = hist.LoadDataTable();
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdPatient.Rebind();
        }
    }
}