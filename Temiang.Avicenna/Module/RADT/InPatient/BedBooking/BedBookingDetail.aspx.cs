using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class BedBookingDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.BedBooking;

            if (!IsPostBack)
            {

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtBedNo.Text = Request.QueryString["bedno"];
                var bed = new Bed();
                bed.LoadByPrimaryKey(txtBedNo.Text);

                var sr = new ServiceRoom();
                sr.LoadByPrimaryKey(bed.RoomID);

                var su = new ServiceUnit();
                su.LoadByPrimaryKey(sr.ServiceUnitID);

                txtServiceUnitID.Text = su.ServiceUnitName;
                txtRoom.Text = sr.RoomName;
            }
        }

        public override bool OnButtonOkClicked()
        {
            Validate();

            if (!IsValid)
                return false;

            //update
            var entity = new Bed();
            entity.LoadByPrimaryKey(Request.QueryString["bedno"]);

            entity.RegistrationNo = cboRegistrationNo.SelectedValue;
            entity.SRBedStatus = AppParameter.GetParameterValue(AppParameter.ParameterItem.BedStatusBooked);
            entity.BedStatusUpdatedBy = AppSession.UserLogin.UserID;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.BookingDateTime = (new DateTime()).NowAtSqlServer();

            var bedManag = new BedManagement();
            bedManag.AddNew();
            bedManag.BedID = Request.QueryString["bedno"];
            bedManag.TransactionDate = (new DateTime()).NowAtSqlServer().Date;
            bedManag.ReservationNo = string.Empty;
            bedManag.RegistrationNo = cboRegistrationNo.SelectedValue;
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(cboRegistrationNo.SelectedValue))
            {
                bedManag.PatientID = reg.PatientID;
                bedManag.RegistrationBedID = reg.BedID;

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                bedManag.FirstName = pat.FirstName;
                bedManag.MiddleName = pat.MiddleName;
                bedManag.LastName = pat.LastName;
                bedManag.StreetName = pat.StreetName;
                bedManag.District = pat.District;
                bedManag.City = pat.City;
                bedManag.County = pat.County;
                bedManag.State = pat.State;
                bedManag.ZipCode = pat.ZipCode;
                bedManag.PhoneNo = pat.PhoneNo;
                bedManag.MobilePhoneNo = pat.MobilePhoneNo;
                bedManag.FaxNo = pat.FaxNo;
                bedManag.Email = pat.Email;
            }
            
            bedManag.SRBedStatus = AppSession.Parameter.BedStatusBooked;
            bedManag.CreatedDateTime = (new DateTime()).NowAtSqlServer();
            bedManag.CreatedByUserID = AppSession.UserLogin.UserID;
            bedManag.IsReleased = false;
            bedManag.IsVoid = false;
            bedManag.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            bedManag.LastUpdateByUserID = AppSession.UserLogin.UserID;

            using (esTransactionScope trans = new esTransactionScope())
            {
                if (AppSession.Parameter.IsBookingBedCharged)
                    entity.Save();

                bedManag.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        protected void cboRegistrationNo_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var reg = new RegistrationQuery("a");
            var pat = new PatientQuery("b");
            var unit = new ServiceUnitQuery("c");
            var room = new ServiceRoomQuery("d");
            var bed = new BedQuery("e");

            reg.es.Top = 5;
            reg.Select(
                reg.RegistrationNo,
                reg.BedID,
                pat.PatientID,
                pat.MedicalNo,
                pat.PatientName,
                unit.ServiceUnitName,
                room.RoomName
                );
            reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            reg.InnerJoin(room).On(reg.RoomID == room.RoomID);
            reg.InnerJoin(bed).On(bed.BedID == reg.BedID && bed.RegistrationNo == reg.RegistrationNo);
            reg.Where(
                reg.IsClosed == false,
                reg.IsVoid == false,
                reg.IsConsul == false,
                reg.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                reg.DischargeDate.IsNull(),
                bed.SRBedStatus.In(AppSession.Parameter.BedStatusOccupied.ToString(), AppSession.Parameter.BedStatusPending.ToString())
                );

            if (e.Text.Trim().Contains(" "))
            {
                var searchs = e.Text.Trim().Split(' ');
                foreach (var search in searchs)
                {
                    var searchLike = "%" + search + "%";
                    reg.Where(
                        reg.Or(
                            pat.PatientID.Like(searchLike),
                            pat.FirstName.Like(searchLike),
                            pat.LastName.Like(searchLike),
                            pat.MiddleName.Like(searchLike),
                            pat.MedicalNo.Like(searchLike),
                            reg.RegistrationNo.Like(searchLike)
                            )
                        );
                }
            }
            else
            {
                string searchText = string.Format("%{0}%", e.Text);
                reg.Where(
                    reg.Or(
                        pat.PatientID.Like(searchText),
                        pat.MedicalNo.Like(searchText),
                        pat.FirstName.Like(searchText),
                        pat.MiddleName.Like(searchText),
                        pat.LastName.Like(searchText),
                        reg.RegistrationNo.Like(searchText)
                        )
                );
            }

            cboRegistrationNo.DataSource = reg.LoadDataTable();
            cboRegistrationNo.DataBind();
        }

        protected void cboRegistrationNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["RegistrationNo"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["RegistrationNo"].ToString();
        }

        protected void cboRegistrationNo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(cboRegistrationNo.SelectedValue))
            {
                txtPatientID.Text = reg.PatientID;

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                txtMedicalNo.Text = pat.MedicalNo;

                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;
                txtPatientName.Text = pat.PatientName;
                txtGender.Text = pat.Sex;
                txtPlaceDOB.Text = string.Format("{0}, {1}", pat.CityOfBirth, Convert.ToDateTime(pat.DateOfBirth).ToString("dd-MMM-yyyy"));

                var su = new ServiceUnit();
                su.LoadByPrimaryKey(reg.ServiceUnitID);
                txtServiceUnit.Text = su.ServiceUnitName;

                var sr = new ServiceRoom();
                sr.LoadByPrimaryKey(reg.RoomID);
                txtRoomBed.Text = sr.RoomName + " - " + reg.BedID;

                var par = new Paramedic();
                par.LoadByPrimaryKey(reg.ParamedicID);
                txtParamedic.Text = par.ParamedicName;
            }
            else
            {
                cboRegistrationNo.SelectedValue = string.Empty;
                cboRegistrationNo.Text = string.Empty;
                txtPatientID.Text = string.Empty;
                txtMedicalNo.Text = string.Empty;
                txtSalutation.Text = string.Empty;
                txtPatientName.Text = string.Empty;
                txtGender.Text = string.Empty;
                txtPlaceDOB.Text = string.Empty;
                txtServiceUnit.Text = string.Empty;
                txtRoomBed.Text = string.Empty;
                txtParamedic.Text = string.Empty;
            }
        }
    }
}
