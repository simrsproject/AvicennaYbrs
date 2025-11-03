using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class UpdateMrnRegistrationDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.UpdateMrnRegistration;
            //this.Title = "Update MRN For:";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtRegistrationNo.Text = Request.QueryString["regno"];
                var reg = new Registration();
                reg.LoadByPrimaryKey(txtRegistrationNo.Text);

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                txtMedicalNo.Text = pat.MedicalNo;
                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;
                txtPatientName.Text = pat.PatientName;
                txtGender.Text = pat.Sex;
                txtPlaceDOB.Text = string.Format("{0}, {1}", pat.CityOfBirth, Convert.ToDateTime(pat.DateOfBirth).ToString("dd-MMM-yyyy"));
                txtAddress.Text = pat.Address;

                txtRegistrationDateTime.SelectedDate = DateTime.Parse(reg.RegistrationDate.Value.ToShortDateString() + " " + reg.RegistrationTime);

                var su = new ServiceUnit();
                su.LoadByPrimaryKey(reg.ServiceUnitID);
                txtServiceUnit.Text = su.ServiceUnitName;

                var sr = new ServiceRoom();
                sr.LoadByPrimaryKey(reg.RoomID);
                txtRoomBed.Text = sr.RoomName + " - " + reg.BedID;

                if (!string.IsNullOrEmpty(reg.ParamedicID))
                {
                    var par = new Paramedic();
                    par.LoadByPrimaryKey(reg.ParamedicID);
                    txtParamedic.Text = par.ParamedicName;
                }
                else txtParamedic.Text = string.Empty;
            }
        }

        public override bool OnButtonOkClicked()
        {
            Validate();

            if (!IsValid)
                return false;

            var entity = new Registration();
            entity.LoadByPrimaryKey(txtRegistrationNo.Text);
            var patIdBefore = entity.PatientID;
            entity.PatientID = cboPatientID.SelectedValue;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            var bookings = new ServiceUnitBookingCollection();
            bookings.Query.Where(bookings.Query.RegistrationNo == txtRegistrationNo.Text);
            bookings.LoadAll();
            foreach (var b in bookings)
            {
                b.PatientID = cboPatientID.SelectedValue;
                b.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                b.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }

            var hist = new RegistrationMRNHistory();
            hist.AddNew();
            hist.RegistrationNo = txtRegistrationNo.Text;
            hist.UpdateDateTime = (new DateTime()).NowAtSqlServer();
            hist.UpdateByUserID = AppSession.UserLogin.UserID;
            hist.FromPatientID = patIdBefore;
            hist.ToPatientID = cboPatientID.SelectedValue;

            using (var trans = new esTransactionScope())
            {
                entity.Save();
                if (bookings.Count > 0)
                    bookings.Save();
                hist.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        protected void cboPatientID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var pat = new PatientQuery();
            
            pat.es.Top = 5;
            pat.Select(
                pat.PatientID,
                pat.MedicalNo,
                pat.PatientName
                );
            pat.Where(pat.IsActive == true, pat.MedicalNo != txtMedicalNo.Text, pat.MedicalNo != string.Empty,
                      pat.IsNonPatient == false);

            if (e.Text.Trim().Contains(" "))
            {
                var searchs = e.Text.Trim().Split(' ');
                foreach (var search in searchs)
                {
                    var searchLike = "%" + search + "%";
                    pat.Where(
                        pat.Or(
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
                pat.Where(
                       pat.Or(
                           pat.MedicalNo.Like(searchTextContain),
                           pat.FirstName.Like(searchTextContain),
                           pat.MiddleName.Like(searchTextContain),
                           pat.LastName.Like(searchTextContain)
                           )
                   );
            }

            cboPatientID.DataSource = pat.LoadDataTable();
            cboPatientID.DataBind();
        }

        protected void cboPatientID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["MedicalNo"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PatientID"].ToString();
        }

        protected void cboPatientID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var pat = new Patient();
            if (pat.LoadByPrimaryKey(cboPatientID.SelectedValue))
            {
                var std = new AppStandardReferenceItem();
                txtSalutation2.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;
                txtPatientName2.Text = pat.PatientName;
                txtGender2.Text = pat.Sex;
                txtPlaceDOB2.Text = string.Format("{0}, {1}", pat.CityOfBirth, Convert.ToDateTime(pat.DateOfBirth).ToString("dd-MMM-yyyy"));
                txtAddress2.Text = pat.Address;
            }
            else
            {
                txtSalutation2.Text = string.Empty;
                txtPatientName2.Text = string.Empty;
                txtGender2.Text = string.Empty;
                txtPlaceDOB2.Text = string.Empty;
                txtAddress2.Text = string.Empty;
            }
        }
    }
}
