using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class DirectPrescriptionReturnList : BasePageList
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

            ProgramID = AppConstant.Program.DirectPrescriptionReturn;
            UrlPageDetail = "DirectPrescriptionReturnDetail.aspx";
            UrlPageSearch = "#";

            if (!IsPostBack)
                txtRegistrationDate.SelectedDate = DateTime.Now.Date;
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            ToolBarMenuSearch.Visible = false;
            ToolBarMenuEdit.Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue("PrescriptionNo").ToString();
            Page.Response.Redirect(UrlPageDetail + "?md=" + mode + "&id=" + id, true);
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

        protected void grdRegistration_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = Registrations;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
                if (!e.IsFromDetailTable)
                grd.DataSource = dataSource;

        }

        private DataTable Registrations
        {
            get
            {
                var isEmptyFilter = txtRegistrationDate.IsEmpty && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtMedicalNo.Text) && string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Direct Prescription Return")) return null;

                var query = new RegistrationQuery("a");
                var trans = new TransPrescriptionQuery("b");
                var pat = new PatientQuery("c");
                var sal = new AppStandardReferenceItemQuery("sal");

                query.Select
                    (
                        query.RegistrationNo,
                        query.RegistrationDate,
                        trans.PrescriptionNo,
                        pat.MedicalNo,
                        pat.PatientName,
                        pat.Sex,
                        pat.DateOfBirth,
                        trans.IsApproval,
                        trans.IsVoid,
                        sal.ItemName.As("SalutationName")
                    );
                query.InnerJoin(pat).On(query.PatientID == pat.PatientID);
                query.InnerJoin(trans).On(query.RegistrationNo == trans.RegistrationNo);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & pat.SRSalutation == sal.ItemID);

                if (!txtRegistrationDate.IsEmpty)
                    query.Where(query.RegistrationDate == txtRegistrationDate.SelectedDate);
                if (txtRegistrationNo.Text != string.Empty)
                    query.Where(query.RegistrationNo == Helper.EscapeQuery(txtRegistrationNo.Text));
                if (txtMedicalNo.Text != string.Empty)
                {
                    string searchMedNo = Helper.EscapeQuery(txtMedicalNo.Text);
                    var reverseMedNoSearch = string.Format("{0}%", searchMedNo.Replace("-", "").Reverse());

                    query.Where(
                        query.Or(
                            pat.ReverseMedicalNo.Like(reverseMedNoSearch),
                            pat.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                            )
                        );

                    //if (AppSession.Parameter.IsMedicalNoContainStrip)
                    //    query.Where(
                    //        query.Or(
                    //            pat.MedicalNo == searchMedNo,
                    //            pat.OldMedicalNo == searchMedNo,
                    //            string.Format("< OR REPLACE(c.MedicalNo, '-', '') LIKE '%{0}%'>", searchMedNo),
                    //            string.Format("< OR REPLACE(c.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchMedNo)
                    //            )
                    //        );
                    //else
                    //    query.Where(
                    //        query.Or(
                    //            pat.MedicalNo == searchMedNo,
                    //            pat.OldMedicalNo == searchMedNo,
                    //            string.Format("< OR c.MedicalNo LIKE '%{0}%'>", searchMedNo),
                    //            string.Format("< OR c.OldMedicalNo LIKE '%{0}%'>", searchMedNo)
                    //            )
                    //        );
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where(pat.FullName.Like(searchPatient));
                    //query.Where
                    //    (
                    //    string.Format("<LTRIM(RTRIM(LTRIM(c.FirstName + ' ' + c.MiddleName)) + ' ' + c.LastName) LIKE '{0}'>", searchPatient)
                    //    );
                }
                
                query.Where
                    (
                        //query.ServiceUnitID == AppSession.Parameter.ServiceUnitPharmacyID,
                        query.IsDirectPrescriptionReturn == true,
                        trans.IsPrescriptionReturn == true
                    );
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.OrderBy(query.RegistrationDate.Descending, query.RegistrationTime.Descending);

                return query.LoadDataTable();
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdRegistration.Rebind();
        }
    }
}
