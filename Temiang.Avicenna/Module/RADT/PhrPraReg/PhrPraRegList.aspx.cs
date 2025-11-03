using System.Data.SqlClient;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using Temiang.Avicenna.Common;
using System.Data;
using System.Data.Linq;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Web;
using System;
using System.Linq;
using System.Web.UI;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class PhrPraRegList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.HealthRecordPraReg;


            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }
        }
        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (!(source is RadGrid)) return;
            var grd = (RadGrid)source;

            var args = eventArgument.Split(':');
            switch (args[0])
            {
                case "rebind":
                    {
                        break;
                    }
            }
            grdList.Rebind();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                RestoreValueFromCookie();
            }
        }
        protected void btnSearchPatient_Click(object sender, ImageClickEventArgs e)
        {
            grdList.CurrentPageIndex = 0;
            grdList.Rebind();
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
        private DataTable GetPatients()
        {

            var isEmptyFilter = string.IsNullOrEmpty(txtPatientSearch.Text) && txtDateOfBirth.IsEmpty &&
                                string.IsNullOrEmpty(txtPhoneNo.Text) && string.IsNullOrEmpty(txtAddress.Text);
            if (!ValidateSearch(isEmptyFilter, "Patient"))
                return null;

            var searchPatient = Helper.EscapeQuery(txtPatientSearch.Text.Trim());
            string dateOfBirth = txtDateOfBirth.IsEmpty
                ? string.Empty
                : txtDateOfBirth.SelectedDate.Value.ToShortDateString();
            string phoneNo = Helper.EscapeQuery(txtPhoneNo.Text);
            string address = Helper.EscapeQuery(txtAddress.Text);
            byte isValidateByZipCode = IsValidateByZipCode;

            var qb = new HealthcareQuery("b");
            var qr = new PatientQuery("a");
            var qc = new AppStandardReferenceItemQuery("c");
            qr.LeftJoin(qb).On(qr.HealthcareID == qb.HealthcareID);
            qr.LeftJoin(qc).On(qc.StandardReferenceID == "Salutation" && qc.ItemID == qr.SRSalutation);

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
                qb.HealthcareName,
                qr.IsBlackList,
                qr.IsNotPaidOff,
                qr.IsAlive,
                qc.ItemName.As("Salutation"),
                qr.ZipCode.Coalesce("''"), string.Format("<CAST({0} AS BIT) AS IsValidateByZipCode>",
                isValidateByZipCode), qr.IsStoredToLokadok, qr.IsActive
                );
            qr.Where(//qr.IsActive == 1, 
                qr.IsNonPatient == false);

            if (!string.IsNullOrEmpty(dateOfBirth))
                qr.Where(qr.DateOfBirth == dateOfBirth);

            if (!string.IsNullOrEmpty(phoneNo))
                qr.Where(
                    qr.Or(
                        qr.PhoneNo == phoneNo,
                        qr.MobilePhoneNo == phoneNo
                        )
                    );

            if (searchPatient != string.Empty)
            {
                //string searchPatient = "%" + txtPatientName.Text + "%";

                string sNumber = searchPatient.Replace("-", "").Replace("/", "");
                int n;
                bool isNumeric = int.TryParse(sNumber, out n);
                if (isNumeric)
                {
                    // for fast search: numeric is medical no
                    qr.Where(qr.Or(
                        string.Format("<REPLACE(a.MedicalNo, '-', '') LIKE '%{0}%'>", sNumber),
                        string.Format("< OR REPLACE(a.OldMedicalNo, '-', '') LIKE '%{0}%'>", sNumber)));
                }
                else
                {
                    string searchTextContain = string.Format("%{0}%", searchPatient);
                    qr.Where(
                            qr.Or(
                                qr.MedicalNo.Like(searchTextContain),
                                qr.OldMedicalNo.Like(searchTextContain),
                                string.Format("< OR LTRIM(RTRIM(LTRIM(a.FirstName + ' ' + a.MiddleName)) + ' ' + a.LastName) LIKE '%{0}%'>", searchPatient),
                                string.Format("< OR REPLACE(a.MedicalNo, '-', '') LIKE '%{0}%'>", searchPatient),
                                string.Format("< OR REPLACE(a.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchPatient)
                            )
                        );
                }
            }

            if (!string.IsNullOrEmpty(address))
            {
                if (address.Trim().Contains(" "))
                {
                    var searchs = address.Split(' ');
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
                    var searchLike = "%" + address + "%";
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

            qr.OrderBy(searchPatient != string.Empty ? qr.FirstName.Ascending : qr.MedicalNo.Descending);

            return qr.LoadDataTable();
        }
        private byte IsValidateByZipCode
        {
            get
            {
                var app = AppSession.Parameter.TablePatientFieldValidation;
                if (string.IsNullOrEmpty(app)) return 0;
                if (app.Contains("ZipCode")) return 1;
                return 0;
            }
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                return;
            }
            grdList.DataSource = GetPatients();
        }


        private string _patientID = string.Empty;
        protected void grdList_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridDataItem)
            //    _patientID = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PatientID"]);

            //if (e.Item is GridNestedViewItem)
            //{
            //    var grd = (RadGrid)e.Item.FindControl("grdForm");
            //    grd.InitializeCultureGrid();
            //    grd.DataSource = PreRegForms(_patientID);
            //    grd.Rebind();

            //    _patientID = string.Empty;
            //}
        }
        private DataTable PreRegForms(string patientID)
        {
            var form = new QuestionFormQuery("a");
            var phr = new PatientHealthRecordQuery("b");
            form.LeftJoin(phr).On(form.QuestionFormID == phr.QuestionFormID & phr.RegistrationNo == patientID); //RegistrationNo diisi PatientID utk QuestionFormType.PraRegistration 

            form.Where(form.IsActive == true, form.SRQuestionFormType == QuestionForm.QuestionFormType.PraRegistration);
            form.Select("<'" + patientID + "' as PatientID>", form.QuestionFormID, form.RmNO, form.QuestionFormName,
                phr.TransactionNo, phr.CreateDateTime, phr.CreateByUserID,
                phr.ServiceUnitID, phr.RegistrationNo, phr.IsApproved, phr.ApprovedDatetime, phr.ApprovedByUserID,
                form.RestrictionUserType, form.IsSingleEntry);

            return form.LoadDataTable();
        }


        protected void grdList_ItemCreated(object sender, GridItemEventArgs e)
        {
            var gridNestedViewItem = e.Item as GridNestedViewItem;
            if (gridNestedViewItem != null)
            {
                e.Item.FindControl("pnlNestedView").Visible = (gridNestedViewItem).ParentItem.Expanded;
            }
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            var isVisible = false;
            if (e.CommandName == RadGrid.ExpandCollapseCommandName && e.Item is GridDataItem)
            {
                isVisible = !e.Item.Expanded;
                ((GridDataItem)e.Item).ChildItem.FindControl("pnlNestedView").Visible = isVisible;

                _patientID = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PatientID"]);
            }

            if (isVisible)
            {
                var grd = (RadGrid)((GridDataItem)e.Item).ChildItem.FindControl("grdForm");
                grd.InitializeCultureGrid();
                grd.DataSource = PreRegForms(_patientID);
                grd.Rebind();

                _patientID = string.Empty;
            }
        }

        protected string AddPhrLink(GridItem container)
        {
            if (Eval("IsSingleEntry") != DBNull.Value && true.Equals(Eval("IsSingleEntry"))) return string.Empty;
            if (Eval("TransactionNo") == DBNull.Value) return string.Empty;
            if (!Eval("RestrictionUserType").ToString().Contains(AppSession.UserLogin.SRUserType)) return string.Empty;

            var newLink = string.Format(
                "<a href=\"#\" onclick=\"entryPhrPraReg('new', '{0}','{1}','{2}','{3}','{4}','{6}'); return false;\"><img src=\"{5}/Images/Toolbar/insert16.png\" border=\"0\" /></a>&nbsp;&nbsp;",
                string.Empty, string.Empty, Eval("QuestionFormID"), string.Empty, Eval("PatientID"), Helper.UrlRoot(), container.OwnerGridID);

            return newLink;
        }
        protected string PhrLink(GridItem container)
        {
            var retval = string.Empty;

            var newDisableLink = string.Format("<img src=\"{0}/Images/Toolbar/new16_d.png\" />", Helper.UrlRoot());

            var viewLink = PhrViewLink(container, "icon");

            if (Eval("RestrictionUserType") == null || string.IsNullOrWhiteSpace(Eval("RestrictionUserType").ToString()) || Eval("RestrictionUserType").ToString().Contains(AppSession.UserLogin.SRUserType))
            {
                var newLink = string.Format(
                    "<a href=\"#\" onclick=\"entryPhrPraReg('new', '{0}','{1}','{2}','{3}','{4}','{6}'); return false;\"><img src=\"{5}/Images/Toolbar/new16.png\" border=\"0\" /></a>",
                    string.Empty, string.Empty, Eval("QuestionFormID"), string.Empty, Eval("PatientID"), Helper.UrlRoot(), container.OwnerGridID);

                if (Eval("TransactionNo") == DBNull.Value || string.IsNullOrEmpty(Eval("TransactionNo").ToString()))
                    retval = this.IsUserAddAble ? newLink : newDisableLink;
                else
                    retval = viewLink;
            }
            else
            {
                if (Eval("TransactionNo") == DBNull.Value || string.IsNullOrEmpty(Eval("TransactionNo").ToString()))
                    retval = newDisableLink;
                else
                    retval = viewLink;
            }


            return retval;
        }
        protected string PhrViewLink(GridItem container, string type)
        {
            string caption;
            if (type == "icon")
                caption = string.Format("<img src=\"{0}/Images/Toolbar/views16.png\" border=\"0\" />",
                    Helper.UrlRoot());
            else
                caption = Eval("TransactionNo").ToString();

            var viewLink = string.Format(
                "<a href=\"#\" onclick=\"entryPhrPraReg('view', '{0}','{1}','{2}','{3}','{4}','{6}'); return false;\">{5}</a>",
                Eval("TransactionNo"), string.Empty, Eval("QuestionFormID"),
                string.Empty,
                Eval("PatientID"), caption, container.OwnerGridID);
            return viewLink;
        }
    }
}
