using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class EditBedNoDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.Admitting;

            if (!IsPostBack)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regNo"]);

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                txtRegistrationNo.Text = reg.RegistrationNo;
                txtMedicalNo.Text = pat.MedicalNo;
                txtPatientName.Text = pat.PatientName;

                optSexMale.Checked = (pat.Sex == "M");
                optSexMale.Enabled = (pat.Sex == "M");
                optSexFemale.Checked = (pat.Sex == "F");
                optSexFemale.Enabled = (pat.Sex == "F");

                var room = new ServiceRoom();
                room.LoadByPrimaryKey(reg.RoomID);
                txtRoom.Text = room.RoomName;
                txtBed.Text = reg.BedID;

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(reg.ServiceUnitID);
                txtServiceUnitName.Text = unit.ServiceUnitName;

                var param = new Paramedic();
                param.LoadByPrimaryKey(reg.ParamedicID);
                txtPhysicianName.Text = param.ParamedicName;

                //Title = "Edit Bed for " + reg.RegistrationNo + " - " + pat.PatientName + " [" + pat.MedicalNo + "]";

                //var coll = new ServiceUnitCollection();
                //coll.Query.Where(coll.Query.DepartmentID == AppSession.Parameter.InPatientDepartmentID & coll.Query.IsActive == true);
                //coll.Query.OrderBy(coll.Query.ServiceUnitName.Ascending);
                //coll.LoadAll();
                //cboServiceUnitID.Items.Clear();
                //cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                //foreach (ServiceUnit item in coll)
                //{
                //    cboServiceUnitID.Items.Add(new RadComboBoxItem(item.ServiceUnitName, item.ServiceUnitID));
                //}

                txtFromServiceUnitID.Text = reg.ServiceUnitID;
                lblFromServiceUnitName.Text = unit.ServiceUnitName;

                txtFromRoomID.Text = reg.RoomID;
                lblFromRoomName.Text = room.RoomName;

                txtFromBedID.Text = reg.BedID;

                txtFromClassID.Text = reg.ClassID;
                var cl = new Class();
                cl.LoadByPrimaryKey(reg.ClassID);
                lblFromClassName.Text = cl.ClassName;

                txtFromChargeClassID.Text = reg.ChargeClassID;
                cl = new Class();
                cl.LoadByPrimaryKey(reg.ChargeClassID);
                lblFromChargeClassName.Text = cl.ClassName;
            }
            var btkOk = (Button)Helper.FindControlRecursive(Master, "btnOk");
            btkOk.Visible = this.IsUserAddAble || this.IsUserEditAble;
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            //{
            //    PopulateRoomList(cboServiceUnitID.SelectedValue);

            //    cboBedID.Items.Clear();
            //    cboBedID.Text = string.Empty;
            //    cboClassID.Items.Clear();
            //    cboChargeClassID.Items.Clear();
            //}
            //else
            //{
            //    cboBedID.Items.Clear();
            //    cboBedID.Text = string.Empty;
            //    cboClassID.Items.Clear();
            //    cboChargeClassID.Items.Clear();
            //}
            cboRoomID.Items.Clear();
            cboRoomID.SelectedValue = string.Empty;
            cboRoomID.Text = string.Empty;
            cboBedID.Items.Clear();
            cboBedID.SelectedValue = string.Empty;
            cboBedID.Text = string.Empty;
            cboClassID.Items.Clear();
            cboClassID.SelectedValue = string.Empty;
            cboClassID.Text = string.Empty;
            cboChargeClassID.Items.Clear();
            cboChargeClassID.SelectedValue = string.Empty;
            cboChargeClassID.Text = string.Empty;
        }

        protected void cboServiceUnitID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ServiceUnitQuery("a");
            query.es.Top = 10;
            query.Select
                (
                    query.ServiceUnitID,
                    query.ServiceUnitName
                );

            query.Where
                (
                    query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                    query.Or
                        (
                            query.ServiceUnitID.Like(searchTextContain),
                            query.ServiceUnitName.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );

            query.OrderBy(query.ServiceUnitName.Ascending);

            cboServiceUnitID.DataSource = query.LoadDataTable();
            cboServiceUnitID.DataBind();
        }

        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        private void PopulateRoomList(string serviceUnitID)
        {
            cboRoomID.Items.Clear();
            if (serviceUnitID != string.Empty)
            {
                var query = new ServiceRoomQuery("a");
                query.Where(query.ServiceUnitID == serviceUnitID);
                query.Select(query.RoomID, query.RoomName);
                query.OrderBy(query.RoomName.Ascending);
                DataTable dtb = query.LoadDataTable();

                cboRoomID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (DataRow row in dtb.Rows)
                {
                    cboRoomID.Items.Add(new RadComboBoxItem(row["RoomName"].ToString(), row["RoomID"].ToString()));
                }
            }
        }

        protected void cboRoomID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboBedID.Items.Clear();
            cboBedID.SelectedValue = string.Empty;
            cboBedID.Text = string.Empty;
            cboClassID.Items.Clear();
            cboClassID.SelectedValue = string.Empty;
            cboClassID.Text = string.Empty;
            cboChargeClassID.Items.Clear();
            cboChargeClassID.SelectedValue = string.Empty;
            cboChargeClassID.Text = string.Empty;
        }

        protected void cboRoomID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ServiceRoomQuery("a");
            query.es.Top = 10;
            query.Select
                (
                    query.RoomID,
                    query.RoomName
                );

            query.Where
                (
                    query.ServiceUnitID == cboServiceUnitID.SelectedValue,
                    query.Or
                        (
                            query.RoomID.Like(searchTextContain),
                            query.RoomName.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );

            query.OrderBy(query.RoomID.Ascending);

            cboRoomID.DataSource = query.LoadDataTable();
            cboRoomID.DataBind();
        }

        protected void cboRoomID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["RoomName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["RoomID"].ToString();
        }

        protected void cboBedID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(cboBedID.SelectedValue))
            {
                cboClassID.Items.Clear();
                cboClassID.SelectedValue = string.Empty;
                cboClassID.Text = string.Empty;
                cboChargeClassID.Items.Clear();
                cboChargeClassID.SelectedValue = string.Empty;
                cboChargeClassID.Text = string.Empty;
                return;
            }

            var bed = new Bed();
            if (bed.LoadByPrimaryKey(e.Value))
            {
                var coll = new ClassCollection();
                coll.Query.Where
                    (
                        coll.Query.IsActive == true,
                        coll.Query.IsInPatientClass == true
                    );
                coll.Query.OrderBy(coll.Query.ClassName.Ascending);
                coll.LoadAll();

                cboClassID.Items.Clear();
                cboChargeClassID.Items.Clear();
                cboClassID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                cboChargeClassID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Class c in coll)
                {
                    cboClassID.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
                    cboChargeClassID.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
                }
                cboClassID.SelectedValue = bed.ClassID;
                cboChargeClassID.SelectedValue = bed.DefaultChargeClassID;

                if (string.IsNullOrEmpty(cboRoomID.SelectedValue))
                {
                    //PopulateRoomList(cboServiceUnitID.SelectedValue);
                    var roomq = new ServiceRoomQuery();
                    roomq.Where(roomq.RoomID == bed.RoomID);
                    cboRoomID.DataSource = roomq.LoadDataTable();
                    cboRoomID.DataBind();

                    cboRoomID.SelectedValue = bed.RoomID;
                }
            }
        }

        protected void cboBedID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new BedQuery("a");
            var reg = new RegistrationQuery("b");
            var pat = new PatientQuery("c");

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
                    query.RegistrationNo == reg.RegistrationNo
                );
            query.LeftJoin(pat).On(reg.PatientID == pat.PatientID);

            query.Where
                (
                    query.BedID.Like(searchTextContain),
                    query.IsActive == true
                );

            if (!string.IsNullOrEmpty(cboRoomID.SelectedValue))
                query.Where(query.RoomID == cboRoomID.SelectedValue);
            else
            {
                var roomQ = new ServiceRoomQuery("d");
                query.InnerJoin(roomQ).On(query.RoomID == roomQ.RoomID);
                query.Where(roomQ.ServiceUnitID == cboServiceUnitID.SelectedValue);
            }

            query.OrderBy(query.BedID.Ascending);

            cboBedID.DataSource = query.LoadDataTable();
            cboBedID.DataBind();
        }

        protected void cboBedID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["BedID"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["BedID"].ToString();
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind:'";
        }

        public override bool OnButtonOkClicked()
        {
            if (!Page.IsValid)
                return false;

            if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                ShowInformationHeader("To Service Unit required.");
                return false;
            }

            if (string.IsNullOrEmpty(cboRoomID.SelectedValue))
            {
                ShowInformationHeader("To Room required.");
                return false;
            }

            if (string.IsNullOrEmpty(cboBedID.SelectedValue))
            {
                ShowInformationHeader("To Bed required.");
                return false;
            }

            if (string.IsNullOrEmpty(cboClassID.SelectedValue))
            {
                ShowInformationHeader("To Class required.");
                return false;
            }

            if (string.IsNullOrEmpty(cboChargeClassID.SelectedValue))
            {
                ShowInformationHeader("To Charge Class required.");
                return false;
            }

            var room = new ServiceRoom();
            if (room.LoadByPrimaryKey(cboRoomID.SelectedValue))
            {
                if (!string.IsNullOrEmpty(room.SRGenderType))
                {
                    string patSex = optSexMale.Checked ? "M" : "F";

                    if (room.SRGenderType != patSex)
                    {
                        string gender = room.SRGenderType == "M" ? "Male" : "Female";
                        ShowInformationHeader("Room: " + cboRoomID.Text + " specifically for the " + gender + " gender.");
                        return false;
                    }
                }
            }

            var r = new Registration();
            r.LoadByPrimaryKey(Request.QueryString["regNo"]);

            if (r.BedID != txtFromBedID.Text)
            {
                ShowInformationHeader("Patient has moved to another bed. Edit bed can't be proceed.");
                return false;
            }

            //bed
            var bed = new Bed();
            bed.LoadByPrimaryKey(txtFromBedID.Text);
            if (bed.IsNeedConfirmation == true && bed.SRBedStatus == AppSession.Parameter.BedStatusOccupied)
            {
                ShowInformationHeader("Patient is already validated (check in confirmed). Edit bed can't be proceed.");
                return false;
            }

            //patient transfer
            var transfer = new PatientTransferCollection();
            transfer.Query.Where
                (
                    transfer.Query.RegistrationNo == Request.QueryString["regNo"],
                    transfer.Query.IsApprove == true
                );
            transfer.LoadAll();

            if (transfer.Count > 0)
            {
                ShowInformationHeader("Patient already have transfer transaction. Edit bed can't be proceed.");
                return false;
            }

            //transaction
            var triQ = new TransChargesItemQuery("a");
            var trQ = new TransChargesQuery("b");

            triQ.Select
                    (triQ.ItemID,
                    triQ.ChargeQuantity.Sum()
                    );
            triQ.InnerJoin(trQ).On(triQ.TransactionNo == trQ.TransactionNo);
            triQ.Where(
                             trQ.RegistrationNo == Request.QueryString["regNo"],
                             trQ.IsAutoBillTransaction == false,
                             triQ.IsVoid == false
                             );
            triQ.GroupBy(triQ.ItemID);
            triQ.Having(triQ.ChargeQuantity.Sum() != 0);
            DataTable dtb = triQ.LoadDataTable();
            if (dtb.Rows.Count > 1)
            {
                ShowInformationHeader("Patient already have service unit transaction. Edit bed can't be proceed.");
                return false;
            }

            //prescription
            var prescription = new TransPrescriptionCollection();
            prescription.Query.Where
                (
                    prescription.Query.RegistrationNo == Request.QueryString["regNo"],
                    prescription.Query.IsVoid == false
                );
            prescription.LoadAll();
            if (prescription.Count > 0)
            {
                ShowInformationHeader("Patient already have prescription transaction. Edit bed can't be proceed.");
                return false;
            }

            //payment
            var payment = new TransPaymentCollection();
            payment.Query.Where
                (
                    payment.Query.TransactionCode == "016",
                    payment.Query.RegistrationNo == Request.QueryString["regNo"],
                    payment.Query.IsVoid == false
                );
            payment.LoadAll();

            if (payment.Count > 0)
            {
                ShowInformationHeader("Patient already have payment transaction. Edit bed can't be proceed.");
                return false;
            }

            var isRoomIn = r.IsRoomIn ?? false;

            var bedTo = new Bed();
            bedTo.LoadByPrimaryKey(cboBedID.SelectedValue);

            bool isSamePatient = r.RegistrationNo == bedTo.RegistrationNo;

            if (isRoomIn)
            {
                if (string.IsNullOrEmpty(bedTo.RegistrationNo) || bedTo.SRBedStatus == AppSession.Parameter.BedStatusUnoccupied)
                {
                    ShowInformationHeader("Bed is empty. Room In is not allow to this bed.");
                    return false;
                }
                if (bedTo.SRBedStatus == AppSession.Parameter.BedStatusBooked)
                {
                    ShowInformationHeader("Bed is already booked. Room In is not allow to this bed.");
                    return false;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(bedTo.RegistrationNo) || bedTo.SRBedStatus != AppSession.Parameter.BedStatusUnoccupied)
                {
                    if (!string.IsNullOrEmpty(bedTo.RegistrationNo) && !isSamePatient)
                    {
                        ShowInformationHeader("Bed is already registered to other patient. Please select another available bed.");
                        return false;
                    }
                    if (bedTo.SRBedStatus == AppSession.Parameter.BedStatusCleaning)
                    {
                        ShowInformationHeader("Bed is being cleaned. Please select another available bed.");
                        return false;
                    }
                }
                if (bedTo.IsRoomIn == true)
                {
                    ShowInformationHeader("Bed has patient room in. Please select another available bed.");
                    return false;
                }
            }

            using (esTransactionScope trans = new esTransactionScope())
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regNo"]);

                //update registration
                reg.ServiceUnitID = cboServiceUnitID.SelectedValue;
                reg.RoomID = cboRoomID.SelectedValue;
                reg.BedID = cboBedID.SelectedValue;
                reg.ClassID = cboClassID.SelectedValue;
                reg.ChargeClassID = cboChargeClassID.SelectedValue;
                if (reg.GuarantorID == AppSession.Parameter.SelfGuarantor)
                    reg.CoverageClassID = reg.ChargeClassID;

                reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                reg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                Bed bedF = new Bed(), bedT = new Bed();
                var bshOld = new BedStatusHistory();
                var bshNew = new BedStatusHistory();

                BedRoomIn briF = new BedRoomIn(), briT = new BedRoomIn();
                var bedmanagColl = new BedManagementCollection();

                bedF.LoadByPrimaryKey(txtFromBedID.Text);
                bedT.LoadByPrimaryKey(cboBedID.SelectedValue);

                if (!isRoomIn)
                {
                    bedF.RegistrationNo = string.Empty;
                    bedF.SRBedStatus = AppSession.Parameter.BedStatusUnoccupied;
                    bedF.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    bedF.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    bedT.RegistrationNo = reg.RegistrationNo;
                    bedT.SRBedStatus = bedT.IsNeedConfirmation == true ? AppSession.Parameter.BedStatusPending : AppSession.Parameter.BedStatusOccupied;
                    bedT.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    bedT.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    briF = null;
                    briT = null;

                    var bshOldQuery = new BedStatusHistoryQuery();
                    bshOldQuery.Where(
                        bshOldQuery.BedID == txtFromBedID.Text, 
                        bshOldQuery.SRBedStatusFrom == AppSession.Parameter.BedStatusUnoccupied, 
                        bshOldQuery.SRBedStatusTo == (bedF.IsNeedConfirmation == true ? AppSession.Parameter.BedStatusPending : AppSession.Parameter.BedStatusOccupied), 
                        bshOldQuery.RegistrationNo == reg.RegistrationNo,
                        bshOldQuery.TransferNo == string.Empty);
                    bshOldQuery.es.Top = 1;
                    bshOldQuery.OrderBy(bshOldQuery.LastUpdateDateTime.Descending);
                    bshOld.Load(bshOldQuery);
                    if (bshOld != null)
                        bshOld.MarkAsDeleted();

                    bshNew.AddNew();
                    bshNew.BedID = cboBedID.SelectedValue;
                    bshNew.SRBedStatusFrom = AppSession.Parameter.BedStatusUnoccupied;
                    bshNew.SRBedStatusTo = (bedT.IsNeedConfirmation ?? false) ? AppSession.Parameter.BedStatusPending : AppSession.Parameter.BedStatusOccupied;
                    bshNew.RegistrationNo = reg.RegistrationNo;
                    bshNew.TransferNo = string.Empty;
                    bshNew.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    bshNew.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }
                else
                {
                    var birColl = new BedRoomInCollection();
                    birColl.Query.Where(birColl.Query.BedID == txtFromBedID.Text,
                                        birColl.Query.DateOfExit.IsNotNull(), birColl.Query.IsVoid == false);
                    birColl.LoadAll();

                    bedF.IsRoomIn = birColl.Count > 0;
                    bedF.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    bedF.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    bedT.IsRoomIn = true;
                    bedT.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    bedT.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    briF.LoadByPrimaryKey(txtFromBedID.Text, reg.RegistrationNo,
                                          reg.RegistrationDate ?? (new DateTime()).NowAtSqlServer(), reg.RegistrationTime);
                    briF.MarkAsDeleted();

                    briT.AddNew();
                    briT.BedID = cboBedID.SelectedValue;
                    briT.RegistrationNo = reg.RegistrationNo;
                    briT.DateOfEntry = reg.RegistrationDate;
                    briT.TimeOfEntry = reg.RegistrationTime;
                    briT.IsVoid = reg.IsVoid;
                    briT.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    briT.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    bshOld = null;
                    bshNew = null;
                }

                if (!isSamePatient) bedmanagColl = null;
                else if (bedT.IsNeedConfirmation == false)
                {
                    bedmanagColl.Query.Where(bedmanagColl.Query.BedID == cboBedID.SelectedValue, bedmanagColl.Query.RegistrationNo == reg.RegistrationNo, bedmanagColl.Query.RegistrationBedID == txtFromBedID.Text);
                    bedmanagColl.LoadAll();
                    foreach (var bm in bedmanagColl)
                    {
                        bm.IsReleased = true;
                        bm.ReleasedByUserID = AppSession.UserLogin.UserID;
                        bm.ReleasedDateTime = (new DateTime()).NowAtSqlServer();
                    }
                }

                var thuColl = new PatientTransferHistoryCollection();
                var thuQuery = new PatientTransferHistoryQuery();
                thuQuery.Where(thuQuery.RegistrationNo == reg.RegistrationNo, thuQuery.TransferNo == string.Empty);
                thuQuery.es.Top = 1;
                thuQuery.OrderBy(thuQuery.TransferNo.Descending);
                thuColl.Load(thuQuery);

                foreach (var item in thuColl)
                {
                    item.ServiceUnitID = reg.ServiceUnitID;
                    item.RoomID = reg.RoomID;
                    item.BedID = reg.BedID;
                    item.ClassID = reg.ClassID;
                    item.ChargeClassID = reg.ChargeClassID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }

                reg.Save();
                if (bedT != null)
                    bedT.Save();
                if (bedF != null)
                    bedF.Save();
                if (briF != null)
                    briF.Save();
                if (briT != null)
                    briT.Save();
                if (bedmanagColl != null)
                    bedmanagColl.Save();
                if (bshOld != null)
                    bshOld.Save();
                if (bshNew != null)
                    bshNew.Save();

                thuColl.Save();

                trans.Complete();
            }

            return true;
        }
    }
}
