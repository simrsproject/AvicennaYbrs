using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class PatientBlacklistList : BasePage
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

            ProgramID = AppConstant.Program.PatientBlacklist;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
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

            if (!(source is RadGrid)) return;

            if (eventArgument.Contains("process"))
            {
                var pat = new Patient();
                pat.LoadByPrimaryKey(eventArgument.Split('|')[1]);
                pat.IsBlackList = false;
                pat.Save();

                var hist = new PatientBlackListHistory();
                hist.AddNew();
                hist.PatientID = eventArgument.Split('|')[1];
                hist.IsBlackList = false;
                hist.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                hist.LastUpdateByUserID = AppSession.UserLogin.UserID;
                hist.Notes = string.Empty;
                hist.Save();

                pnlInfo.Visible = false;
                grdList.Rebind();
            }
            else
            {
                if (eventArgument == "rebind")
                    grdList.Rebind();
            }
        }

        private DataTable Patients
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(txtMedicalNo.Text) && string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Patient")) return null;

                var query = new PatientQuery("a");
                var sal = new AppStandardReferenceItemQuery("sal");
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
                        query.IsBlackList,
                        sal.ItemName.As("SalutationName")
                    );
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & query.SRSalutation == sal.ItemID);

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.es.Distinct = true;
                if (!(string.IsNullOrEmpty(txtMedicalNo.Text)))
                {
                    string searchMedNo = Helper.EscapeQuery(txtMedicalNo.Text);
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
                if (!(string.IsNullOrEmpty(txtPatientName.Text)))
                {
                    if (txtPatientName.Text.Trim().Contains(" "))
                    {
                        var searchs = Helper.EscapeQuery(txtPatientName.Text).Trim().Split(' ');
                        foreach (var search in searchs)
                        {
                            var searchLike = "%" + search + "%";
                            query.Where(
                                query.Or(
                                    query.FirstName.Like(searchLike),
                                    query.LastName.Like(searchLike),
                                    query.MiddleName.Like(searchLike)
                                    )
                                );
                        }
                    }
                    else
                    {
                        var searchLike = "%" + Helper.EscapeQuery(txtPatientName.Text).Trim() + "%";
                        query.Where(
                            query.Or(
                                query.FirstName.Like(searchLike),
                                query.LastName.Like(searchLike),
                                query.MiddleName.Like(searchLike)
                                )
                            );
                    }
                }
                
                query.Where(query.IsActive == true, query.IsNonPatient == false);

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

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit) {
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

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string patientId = dataItem.GetDataKeyValue("PatientID").ToString();

            var query = new PatientBlackListHistoryQuery("a");
            var usr = new AppUserQuery("b");
            query.InnerJoin(usr).On(query.LastUpdateByUserID == usr.UserID);
            query.Select
                (
                    query.PatientID,
                    query.IsBlackList,
                    query.LastUpdateDateTime,
                    query.LastUpdateByUserID,
                    usr.UserName,
                    query.Notes
                );

            query.Where(query.PatientID == patientId);
            query.OrderBy(query.LastUpdateDateTime.Descending);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            pnlInfo.Visible = false;
            grdList.Rebind();
        }
    }
}
