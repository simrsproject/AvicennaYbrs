using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections;

using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class ReservationDetail : BasePageDialog
    {
        private bool _isNewRecord;
        public AppAutoNumberLast _autoNumberLastReservation;

        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.Admitting; //TODO: Isi ProgramID
            var btkOk = (Button)Helper.FindControlRecursive(Master, "btnOk");
            btkOk.Visible = this.IsUserAddAble || this.IsUserEditAble;
            StandardReference.Initialize(cboSRReservationStatus, AppEnum.StandardReference.AppointmentStatus);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _isNewRecord = Page.Request.QueryString["md"] == "new";

            if (!IsPostBack)
            {
                if (_isNewRecord)
                    InitializeNewReservation();
                else
                    PopulateEntryControl(Page.Request.QueryString["reservationtNo"]);
                trSRAppointmentStatus.Visible = true;
            }

        }

        private void InitializeNewReservation()
        {
            _autoNumberLastReservation = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.ReservationID);
            txtReservationNo.Text = _autoNumberLastReservation.LastCompleteNumber;

        }

        private void PopulateEntryControl(string reservationNo)
        {
            //Load record
            Reservation reservation = new Reservation();
            if (reservation.LoadByPrimaryKey(reservationNo))
            {
                //Populate Control
                txtReservationNo.Text = reservation.ReservationNo;

                PatientQuery query = new PatientQuery();
                query.Select(
                        query.PatientID,
                        query.MedicalNo,
                        query.PatientName,
                        query.Sex,
                        query.DateOfBirth,
                        query.Address
                    );
                query.Where(query.PatientID == reservation.str.PatientID);

                var dtb = query.LoadDataTable();

                cboPatientID.DataSource = dtb;
                cboPatientID.DataBind();
                cboPatientID.SelectedValue = reservation.PatientID;

                txtReservationDate.SelectedDate = reservation.ReservationDate;
                cboSRReservationStatus.SelectedValue = reservation.SRReservationStatus;

                ServiceUnitQuery suQuery = new ServiceUnitQuery();
                suQuery.Where(suQuery.ServiceUnitID == reservation.ServiceUnitID);
                cboServiceUnitID.DataSource = suQuery.LoadDataTable();
                cboServiceUnitID.DataBind();
                cboServiceUnitID.SelectedValue = suQuery.ServiceUnitID;


                BedQuery bedQuery = new BedQuery();
                bedQuery.Where(bedQuery.BedID == reservation.BedID);
                cboBedID.DataSource = bedQuery.LoadDataTable();
                cboBedID.DataBind();
                cboBedID.SelectedValue = bedQuery.BedID;

                txtClassID.Text = reservation.ClassID;
                txtNotes.Text = reservation.Notes;



            }
        }

        #endregion

        #region Toolbar Menu Event

        private bool IsDuplicateReservationNo()
        {
            ReservationQuery query = new ReservationQuery();
            query.es.Top = 1;
            query.Select(query.ReservationNo, query.PatientID);
            query.Where(query.ReservationDate == txtReservationDate.SelectedDate && query.BedID == cboBedID.SelectedValue);
            DataTable dtb = query.LoadDataTable();
            if (dtb.Rows.Count > 0 && !dtb.Rows[0]["ReservationNo"].Equals(txtReservationNo.Text))
            {
                this.ShowInformationHeader(
                    string.Format("Reservation No {0} has been used by another patient, please change to other No",
                                  txtReservationNo.Text));
                return true;
            }
            return false;
        }

        private bool SaveNew()
        {
            if (txtReservationNo.Text != string.Empty)
            {
                if (IsDuplicateReservationNo())
                    return false;
            }

            Reservation entity = new Reservation();
            entity.AddNew();

            SetEntityValue(entity);
            var coll = new ReservationCollection();

            SaveEntity(entity);

            return true;
        }


        private bool SaveEdit()
        {
            if (txtReservationNo.Text != string.Empty)
            {
                if (IsDuplicateReservationNo())
                    return false;
            }

            Reservation entity = new Reservation();
            if (entity.LoadByPrimaryKey(txtReservationNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }

            return true;
        }

        public override bool OnButtonOkClicked()
        {

            if (!Page.IsValid)
                return false;

            var retval = _isNewRecord ? SaveNew() : SaveEdit();

            return retval;
        }

        #endregion

        #region Private Method Standard


        private void SetEntityValue(Reservation entity)
        {
            entity.ReservationNo = txtReservationNo.Text;
            entity.ReservationDate = txtReservationDate.SelectedDate;
            entity.SRReservationStatus = cboSRReservationStatus.SelectedValue;
            entity.PatientID = cboPatientID.SelectedValue;
            entity.ServiceUnitID = cboServiceUnitID.SelectedValue;
            entity.DepartmentID = AppConstant.RegistrationType.InPatient;
            entity.RoomID = cboRoomID.SelectedValue;
            entity.ClassID = txtClassID.Text;
            entity.BedID = cboBedID.SelectedValue;
            entity.Notes = txtNotes.Text;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(Reservation entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                //AutoNumber
                if (_isNewRecord)
                {
                    if (_autoNumberLastReservation != null)
                        _autoNumberLastReservation.Save();
                }

                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        #endregion

        #region Method & Event TextChanged

        //private void PopulateRoomList(string serviceUnitID)
        //{
        //    cboRoomID.Items.Clear();
        //    if (serviceUnitID != string.Empty)
        //    {
        //        ServiceRoomQuery query = new ServiceRoomQuery("a");
        //        query.Where(query.ServiceUnitID == serviceUnitID);
        //        query.Select(query.RoomID, query.RoomName);
        //        query.OrderBy(query.RoomName.Ascending);
        //        DataTable dtb = query.LoadDataTable();

        //        cboRoomID.Items.Add(new RadComboBoxItem("", ""));
        //        foreach (DataRow row in dtb.Rows)
        //        {
        //            cboRoomID.Items.Add(new RadComboBoxItem(row["RoomName"].ToString(), row["RoomID"].ToString()));
        //        }
        //    }
        //}

        protected void cboBedID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(cboBedID.SelectedValue))
            {
                txtClassID.Text = string.Empty;
                lblClassName_NT.Text = string.Empty;
                return;
            }

            Bed bed = new Bed();
            if (bed.LoadByPrimaryKey(cboBedID.SelectedValue))
            {
                txtClassID.Text = bed.ClassID;
                Class cl = new Class();
                cl.LoadByPrimaryKey(txtClassID.Text);
                lblClassName_NT.Text = cl.ClassName;

                ClassCollection coll = new ClassCollection();
                coll.Query.Where
                    (
                        coll.Query.IsActive == true,
                        coll.Query.IsInPatientClass == true
                    );
                coll.Query.OrderBy(coll.Query.ClassName, esOrderByDirection.Ascending);
                coll.LoadAll();
            }
        }

        #endregion Method & Event TextChanged

        #region ComboBox Function
        protected void cboPatientID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var dtbPatient = (new PatientCollection()).PatientRegisterAble(e.Text, string.Empty, string.Empty, string.Empty, 0);
            cboPatientID.DataSource = dtbPatient;
            cboPatientID.DataBind();
        }

        protected void cboPatientID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["MedicalNo"].ToString() + "-" + ((DataRowView)e.Item.DataItem)["PatientName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PatientID"].ToString();
        }

        protected void cboServiceUnitID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            ServiceUnitQuery query = new ServiceUnitQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.ServiceUnitID,
                    query.ServiceUnitName
                );
            query.Where
                (
                  query.ServiceUnitName.Like(searchTextContain) &&
                  query.SRRegistrationType == AppConstant.RegistrationType.InPatient
                );

            cboServiceUnitID.DataSource = query.LoadDataTable();
            cboServiceUnitID.DataBind();
        }

        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void cboRoomID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            string ServiceUnitID = cboServiceUnitID.SelectedValue;
            ServiceRoomQuery query = new ServiceRoomQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.RoomID,
                    query.RoomName
                );
            query.Where
                (
                  query.RoomName.Like(searchTextContain) &&
                  query.ServiceUnitID == ServiceUnitID,
                  query.IsActive == true
                );

            cboRoomID.DataSource = query.LoadDataTable();
            cboRoomID.DataBind();
        }

        protected void cboRoomID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["RoomName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["RoomID"].ToString();
        }

        protected void cboBedID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            BedQuery query = new BedQuery("a");
            RegistrationQuery reg = new RegistrationQuery("b");
            PatientQuery pat = new PatientQuery("c");

            query.es.Top = 10;
            query.Select
                (
                    query.BedID,
                    reg.RegistrationNo,
                    pat.PatientName,
                    pat.Sex
                );
            query.LeftJoin(reg).On
                (
                    query.BedID == reg.BedID &&
                    reg.SRRegistrationType == AppConstant.RegistrationType.InPatient &&
                    reg.IsClosed == false,
                    query.IsActive == true
                );
            query.LeftJoin(pat).On(reg.PatientID == pat.PatientID);
            query.Where
                (
                    query.RoomID == cboRoomID.SelectedValue,
                    query.BedID.Like(searchTextContain)
                );

            query.OrderBy(query.BedID.Ascending);

            cboBedID.DataSource = query.LoadDataTable();
            cboBedID.DataBind();
        }

        protected void cboBedID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["BedID"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["BedID"].ToString();
        }

        #endregion ComboBox Function

    }
}
