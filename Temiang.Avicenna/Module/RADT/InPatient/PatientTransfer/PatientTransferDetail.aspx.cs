using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Configuration;

namespace Temiang.Avicenna.Module.RADT.InPatients
{
    public partial class PatientTransferDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private string GetNewTransferNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, BusinessObject.Reference.TransactionCode.PatientTransfer,
                AppSession.Parameter.InPatientDepartmentID);

            var dept = new Department();
            dept.LoadByPrimaryKey(AppSession.Parameter.InPatientDepartmentID);

            _autoNumber.DepartmentInitial = dept.Initial;
            return _autoNumber.LastCompleteNumber;
        }

        private void SetEntityValue(PatientTransfer entity)
        {
            entity.TransferNo = txtTransferNo.Text;
            entity.RegistrationNo = txtRegistrationNo.Text;
            entity.DepartmentID = AppSession.Parameter.InPatientDepartmentID;
            entity.TransactionCode = BusinessObject.Reference.TransactionCode.PatientTransfer;
            entity.TransferDate = txtTransferDate.SelectedDate;
            entity.TransferTime = txtTransferTime.TextWithLiterals;
            entity.FromServiceUnitID = txtFromServiceUnitID.Text;
            entity.FromClassID = txtFromClassID.Text;
            entity.FromRoomID = txtFromRoomID.Text;
            entity.FromBedID = txtFromBedID.Text;
            entity.FromChargeClassID = txtFromChargeClassID.Text;
            entity.FromSpecialtyID = cboFromSpecialtyID.SelectedValue;
            entity.ToServiceUnitID = cboServiceUnitID.SelectedValue;
            entity.ToClassID = cboClassID.SelectedValue;
            entity.ToRoomID = cboRoomID.SelectedValue;
            entity.ToBedID = cboBedID.SelectedValue;
            entity.ToChargeClassID = cboChargeClassID.SelectedValue;
            entity.ToSpecialtyID = cboToSpecialityID.SelectedValue;
            entity.IsRoomInFrom = chkIsRoomInFrom.Checked;
            entity.IsRoomInTo = chkIsRoomInTo.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new PatientTransferQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.TransferNo > txtTransferNo.Text,
                        que.IsVoid == false
                    );
                que.OrderBy(que.TransferNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.TransferNo < txtTransferNo.Text,
                        que.IsVoid == false
                    );
                que.OrderBy(que.TransferNo.Descending);
            }
            var entity = new PatientTransfer();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new PatientTransfer();
            if (parameters.Length > 0)
            {
                String transferNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(transferNo);
            }
            else
                entity.LoadByPrimaryKey(txtTransferNo.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var patientTransfer = (PatientTransfer)entity;
            txtTransferNo.Text = patientTransfer.TransferNo;
            txtRegistrationNo.Text = patientTransfer.RegistrationNo;
            if (patientTransfer.TransferDate.HasValue)
            {
                txtTransferDate.SelectedDate = patientTransfer.TransferDate.Value.Date;
                txtTransferTime.Text = patientTransfer.TransferTime;
            }

            ViewState["IsApprove"] = patientTransfer.IsApprove ?? false;
            ViewState["IsVoid"] = patientTransfer.IsVoid ?? false;

            var reg = new Registration();

            if (reg.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                //var guar = new Guarantor();
                //if (guar.LoadByPrimaryKey(reg.GuarantorID))
                //    txtGuarantorName.Text = guar.GuarantorName;
                //else
                //    txtGuarantorName.Text = string.Empty;

                var cls = new Class();
                if (cls.LoadByPrimaryKey(reg.CoverageClassID))
                    txtCoverageClassName.Text = cls.ClassName;
                else
                    txtCoverageClassName.Text = string.Empty;

                txtPhysicianID.Text = reg.ParamedicID;
                var param = new Paramedic();
                param.LoadByPrimaryKey(txtPhysicianID.Text);
                lblPhysicianName.Text = param.ParamedicName;

                chkIsNewBornInfant.Checked = reg.IsNewBornInfant ?? false;

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.str.PatientID);
                
                txtMedicalNo.Text = pat.MedicalNo;
                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;
                txtPatientName.Text = pat.PatientName;
                txtGender.Text = pat.Sex;
                txtPlaceDOB.Text = string.Format("{0}, {1}", pat.CityOfBirth, Convert.ToDateTime(pat.DateOfBirth).ToString("dd-MMM-yyyy"));

                PopulatePatientImage(pat.PatientID);

                //to
                //var coll = new ClassCollection();
                //coll.Query.Where
                //    (
                //        coll.Query.IsActive == true,
                //        coll.Query.IsInPatientClass == true
                //    );
                //coll.Query.OrderBy(coll.Query.ClassID.Ascending);
                //coll.LoadAll();

                //cboClassID.Items.Clear();
                //cboChargeClassID.Items.Clear();
                //cboFilterClassID.Items.Clear();
                //cboClassID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                //cboChargeClassID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                //cboFilterClassID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                //foreach (Class cl in coll)
                //{
                //    cboClassID.Items.Add(new RadComboBoxItem(cl.ClassName, cl.ClassID));
                //    cboChargeClassID.Items.Add(new RadComboBoxItem(cl.ClassName, cl.ClassID));
                //    cboFilterClassID.Items.Add(new RadComboBoxItem(cl.ClassName, cl.ClassID));
                //}
                cboClassID.SelectedValue = patientTransfer.ToClassID;
                cboChargeClassID.SelectedValue = patientTransfer.ToChargeClassID;
                if (trFilterToClass.Visible)
                    cboFilterClassID.SelectedValue = patientTransfer.ToClassID;
                else
                    cboFilterClassID.SelectedValue = string.Empty;

                PopulateServiceUnitList(cboFilterClassID.SelectedValue);    
                cboServiceUnitID.SelectedValue = patientTransfer.ToServiceUnitID;

                PopulateRoomList(cboServiceUnitID.SelectedValue, string.Empty);
                cboRoomID.SelectedValue = patientTransfer.ToRoomID;

                var query = new BedQuery("a");
                query.Select
                    (
                        query.BedID,
                        "<'' AS RegistrationNo>",
                        "<'' AS PatientName>",
                        "<'' AS Sex>"
                    );

                query.Where
                    (
                        query.RoomID == cboRoomID.SelectedValue,
                        query.BedID == patientTransfer.str.ToBedID
                    );

                cboBedID.DataSource = query.LoadDataTable();
                cboBedID.DataBind();
                cboBedID.SelectedValue = patientTransfer.ToBedID;
            }

            cboToSpecialityID.SelectedValue = patientTransfer.ToSpecialtyID;
            chkIsRoomInTo.Checked = patientTransfer.IsRoomInTo ?? false;

            //from
            txtFromServiceUnitID.Text = patientTransfer.FromServiceUnitID;
            var unit = new ServiceUnit();
            if (unit.LoadByPrimaryKey(txtFromServiceUnitID.Text))
                lblFromServiceUnitName.Text = unit.ServiceUnitName;
            else
                lblFromServiceUnitName.Text = string.Empty;

            txtFromClassID.Text = patientTransfer.FromClassID;
            var c = new Class();
            if (c.LoadByPrimaryKey(txtFromClassID.Text))
                lblFromClassName.Text = c.ClassName;
            else
                lblFromClassName.Text = string.Empty;

            txtFromRoomID.Text = patientTransfer.FromRoomID;
            var room = new ServiceRoom();
            if (room.LoadByPrimaryKey(txtFromRoomID.Text))
                lblFromRoomName.Text = room.RoomName;
            else
                lblFromRoomName.Text = string.Empty;

            txtFromBedID.Text = patientTransfer.FromBedID;

            txtFromChargeClassID.Text = patientTransfer.FromChargeClassID;
            c = new Class();
            if (c.LoadByPrimaryKey(txtFromChargeClassID.Text))
                lblFromChargeClassName.Text = c.ClassName;
            else
                lblFromChargeClassName.Text = string.Empty;

            cboFromSpecialtyID.SelectedValue = patientTransfer.FromSpecialtyID;
            chkIsRoomInFrom.Checked = patientTransfer.IsRoomInFrom ?? false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new PatientTransfer());

            txtTransferNo.Text = GetNewTransferNo();
            txtTransferDate.SelectedDate = (new DateTime()).NowAtSqlServer().Date;
            txtTransferTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");

            txtRegistrationNo.Text = Request.QueryString["regno"];
            PopulateRegistrationNo(txtRegistrationNo.Text);
            chkIsRoomInTo.Enabled = chkIsNewBornInfant.Checked;
            cboClassID.Enabled = false;

            ViewState["IsApprove"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuEditClick()
        {
            chkIsRoomInTo.Enabled = chkIsNewBornInfant.Checked;
            cboClassID.Enabled = false;
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("TransferNo='{0}'", txtTransferNo.Text.Trim());
            auditLogFilter.TableName = "PatientTransfer";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "PatientTransferSearch.aspx?fp=dt&regno=" + Request.QueryString["regno"];
            UrlPageList = "PatientTransferRegistrationList.aspx";

            ProgramID = AppConstant.Program.PatientTransfer;

            WindowSearch.Height = 275;

            if (!IsPostBack)
            {
                PopulateServiceUnitList(string.Empty);
                PopulateClassList();

                ComboBox.PopulateWithSmf(cboFromSpecialtyID);
                ComboBox.PopulateWithSmf(cboToSpecialityID);

                trRoomInFrom.Visible = AppSession.Parameter.IsUsingRoomingIn;
                trRoomInTo.Visible = AppSession.Parameter.IsUsingRoomingIn;
                chkIsNewBornInfant.Visible = AppSession.Parameter.IsUsingRoomingIn;

                trFilterFromClass.Visible = AppSession.Parameter.IsPatientTransferUsingFilterToClass;
                trFilterToClass.Visible = AppSession.Parameter.IsPatientTransferUsingFilterToClass;
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var bed = new Bed();
            bed.LoadByPrimaryKey(txtFromBedID.Text);
            if (bed.RegistrationNo != string.Empty)
            {
                args.MessageText = AppConstant.Message.BedAlreadyRegistered;
                args.IsCancel = true;
                return;
            }

            var transfer = new PatientTransfer();
            transfer.LoadByPrimaryKey(txtTransferNo.Text);
            transfer.IsVoid = true;

            var from = new Bed();
            from.LoadByPrimaryKey(transfer.FromBedID);
            from.RegistrationNo = transfer.RegistrationNo;
            from.SRBedStatus = AppSession.Parameter.BedStatusOccupied;
            from.LastUpdateByUserID = AppSession.UserLogin.UserID;
            from.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            var to = new Bed();
            to.LoadByPrimaryKey(transfer.ToBedID);
            to.RegistrationNo = string.Empty;
            to.SRBedStatus = AppSession.Parameter.BedStatusUnoccupied;
            to.LastUpdateByUserID = AppSession.UserLogin.UserID;
            to.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            var reg = new Registration();
            reg.LoadByPrimaryKey(transfer.RegistrationNo);
            reg.RoomID = transfer.FromRoomID;
            reg.BedID = transfer.FromBedID;
            reg.ClassID = transfer.FromClassID;
            reg.ChargeClassID = transfer.FromChargeClassID;

            using (esTransactionScope trans = new esTransactionScope())
            {
                transfer.Save();
                from.Save();
                to.Save();
                reg.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var unit = new ServiceUnit();
            if (!unit.LoadByPrimaryKey(cboServiceUnitID.SelectedValue))
            {
                args.MessageText = "Invalid To Service Unit.";
                args.IsCancel = true;
                return;
            }

            var room = new ServiceRoom();
            if (room.LoadByPrimaryKey(cboRoomID.SelectedValue))
            {
                if (!string.IsNullOrEmpty(room.SRGenderType))
                {
                    if (room.SRGenderType != txtGender.Text)
                    {
                        string gender = room.SRGenderType == "M" ? "Male" : "Female";
                        args.MessageText = "Room: " + cboRoomID.Text + " specifically for the " + gender + " gender.";
                        args.IsCancel = true;
                        return;
                    }
                }
            }
            else
            {
                args.MessageText = "Invalid To Room.";
                args.IsCancel = true;
                return;
            }

            var bed = new Bed();
            if (!bed.LoadByPrimaryKey(cboBedID.SelectedValue))
            {
                args.MessageText = "Invalid To Bed No.";
                args.IsCancel = true;
                return;
            }

            if (!chkIsRoomInTo.Checked)
            {
                if (!string.IsNullOrEmpty(bed.RegistrationNo))
                {
                    if (bed.RegistrationNo != txtRegistrationNo.Text)
                    {
                        args.MessageText = "The intended bed is already registered to another patient.";
                        args.IsCancel = true;
                        return;
                    }
                }
                if (bed.SRBedStatus == AppSession.Parameter.BedStatusCleaning)
                {
                    args.MessageText = "The intended bed is being cleaned. Please select another available bed.";
                    args.IsCancel = true;
                    return;
                }
                if (bed.IsRoomIn == true)
                {
                    args.MessageText = "The intended bed has patient room in. Please select another available bed.";
                    args.IsCancel = true;
                    return;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(bed.RegistrationNo))
                {
                    args.MessageText = "The intended bed is empty. Room In is not allow to this bed.";
                    args.IsCancel = true;
                    return;
                }
                if (bed.SRBedStatus == AppSession.Parameter.BedStatusBooked)
                {
                    args.MessageText = "The intended bed is already booked. Room In is not allow to this bed.";
                    args.IsCancel = true;
                    return;
                }
            }

            var c = new Class();
            if (!c.LoadByPrimaryKey(cboClassID.SelectedValue))
            {
                args.MessageText = "Invalid To Class.";
                args.IsCancel = true;
                return;
            }

            c = new Class();
            if (!c.LoadByPrimaryKey(cboChargeClassID.SelectedValue))
            {
                args.MessageText = "Invalid To Charge Class.";
                args.IsCancel = true;
                return;
            }

            if (!(c.IsTariffClass ?? false))
            {
                args.MessageText = "Invalid Charge Class. Please select another class.";
                args.IsCancel = true;
                return;
            }

            var smf = new Smf();
            if (!smf.LoadByPrimaryKey(cboToSpecialityID.SelectedValue))
            {
                args.MessageText = "Invalid To SMF.";
                args.IsCancel = true;
                return;
            }

            txtTransferNo.Text = GetNewTransferNo();

            var entity = new PatientTransfer();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(PatientTransfer entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                if (entity.es.IsAdded)
                    _autoNumber.Save();

                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var unit = new ServiceUnit();
            if (!unit.LoadByPrimaryKey(cboServiceUnitID.SelectedValue))
            {
                args.MessageText = "Invalid To Service Unit.";
                args.IsCancel = true;
                return;
            }

            var room = new ServiceRoom();
            if (room.LoadByPrimaryKey(cboRoomID.SelectedValue))
            {
                if (!string.IsNullOrEmpty(room.SRGenderType))
                {
                    if (room.SRGenderType != txtGender.Text)
                    {
                        string gender = room.SRGenderType == "M" ? "Male" : "Female";
                        args.MessageText = "Room: " + cboRoomID.Text + " specifically for the " + gender + " gender.";
                        args.IsCancel = true;
                        return;
                    }
                }
            }
            else
            {
                args.MessageText = "Invalid To Room.";
                args.IsCancel = true;
                return;
            }

            var bed = new Bed();
            if (!bed.LoadByPrimaryKey(cboBedID.SelectedValue))
            {
                args.MessageText = "Invalid To Bed No.";
                args.IsCancel = true;
                return;
            }

            if (!chkIsRoomInTo.Checked)
            {
                if (!string.IsNullOrEmpty(bed.RegistrationNo))
                {
                    if (bed.RegistrationNo != txtRegistrationNo.Text)
                    {
                        args.MessageText = "Bed destination is already registered to another patient.";
                        args.IsCancel = true;
                        return;
                    }
                }
                if (bed.IsRoomIn == true)
                {
                    args.MessageText = "Bed destination has patient room in. Please select another available bed.";
                    args.IsCancel = true;
                    return;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(bed.RegistrationNo))
                {
                    args.MessageText = "Bed destination is empty. Room In is not allow to this bed.";
                    args.IsCancel = true;
                    return;
                }
                if (bed.SRBedStatus == AppSession.Parameter.BedStatusBooked)
                {
                    args.MessageText = "Bed destination is already booked. Room In is not allow to this bed.";
                    args.IsCancel = true;
                    return;
                }
            }

            var c = new Class();
            if (!c.LoadByPrimaryKey(cboClassID.SelectedValue))
            {
                args.MessageText = "Invalid To Class.";
                args.IsCancel = true;
                return;
            }

            c = new Class();
            if (!c.LoadByPrimaryKey(cboChargeClassID.SelectedValue))
            {
                args.MessageText = "Invalid To Charge Class.";
                args.IsCancel = true;
                return;
            }

            if (!(c.IsTariffClass ?? false))
            {
                args.MessageText = "Invalid Charge Class. Please select another class.";
                args.IsCancel = true;
                return;
            }

            var smf = new Smf();
            if (!smf.LoadByPrimaryKey(cboToSpecialityID.SelectedValue))
            {
                args.MessageText = "Invalid To SMF.";
                args.IsCancel = true;
                return;
            }

            var entity = new PatientTransfer();
            if (entity.LoadByPrimaryKey(txtTransferNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !(bool)ViewState["IsApprove"];
        }

        private void PopulateRegistrationNo(string registrationNo)
        {
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(registrationNo))
            {
                txtPhysicianID.Text = reg.ParamedicID;
                var param = new Paramedic();
                param.LoadByPrimaryKey(txtPhysicianID.Text);
                lblPhysicianName.Text = param.ParamedicName;

                chkIsNewBornInfant.Checked = reg.IsNewBornInfant ?? false;

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                txtMedicalNo.Text = pat.MedicalNo;
                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;
                txtPatientName.Text = pat.PatientName;
                txtGender.Text = pat.Sex;
                txtPlaceDOB.Text = string.Format("{0}, {1}", pat.CityOfBirth, Convert.ToDateTime(pat.DateOfBirth).ToString("dd-MMM-yyyy"));

                PopulatePatientImage(pat.PatientID);

                var gua = new Guarantor();
                gua.LoadByPrimaryKey(reg.GuarantorID);
                txtGuarantorName.Text = gua.GuarantorName;

                var cls = new Class();
                cls.LoadByPrimaryKey(reg.CoverageClassID);
                txtCoverageClassName.Text = cls.ClassName;

                txtFromServiceUnitID.Text = reg.ServiceUnitID;
                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(reg.ServiceUnitID);
                lblFromServiceUnitName.Text = unit.ServiceUnitName;

                txtFromRoomID.Text = reg.RoomID;
                var room = new ServiceRoom();
                room.LoadByPrimaryKey(reg.RoomID);
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

                cboFromSpecialtyID.SelectedValue = reg.SmfID;

                chkIsRoomInFrom.Checked = reg.IsRoomIn ?? false;

                cboToSpecialityID.SelectedValue = cboFromSpecialtyID.SelectedValue;
            }
        }

        private void PopulateClassList()
        {
            var coll = new ClassCollection();
            coll.Query.Where
                (
                    coll.Query.IsActive == true,
                    coll.Query.IsInPatientClass == true
                );
            coll.Query.OrderBy(coll.Query.ClassID.Ascending);
            coll.LoadAll();

            cboFilterClassID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            cboClassID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            cboChargeClassID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            
            foreach (Class c in coll)
            {
                cboFilterClassID.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
                cboClassID.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
                cboChargeClassID.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
            }
        }

        protected void cboFilterClassID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboServiceUnitID.Items.Clear();
            cboServiceUnitID.SelectedValue = string.Empty;
            cboServiceUnitID.Text = string.Empty;
            PopulateServiceUnitList(e.Value);
            
            cboRoomID.Items.Clear();
            cboRoomID.Text = string.Empty;
            cboBedID.Items.Clear();
            cboBedID.Text = string.Empty;
            //cboClassID.Items.Clear();
            //cboChargeClassID.Items.Clear();

            //var clsColl = new ClassCollection();
            //clsColl.Query.Where
            //    (
            //        clsColl.Query.IsActive == true,
            //        clsColl.Query.IsInPatientClass == true
            //    );
            //clsColl.Query.OrderBy(clsColl.Query.ClassID.Ascending);
            //clsColl.LoadAll();

            //cboClassID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            //cboChargeClassID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            //foreach (Class c in clsColl)
            //{
            //    cboClassID.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
            //    cboChargeClassID.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
            //}
            cboClassID.SelectedValue = cboFilterClassID.SelectedValue;
            cboChargeClassID.SelectedValue = cboFilterClassID.SelectedValue;

            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                PopulateRoomList(cboServiceUnitID.SelectedValue, e.Value);
            }
        }

        private void PopulateServiceUnitList(string filterClassId)
        {
            var coll = new ServiceUnitCollection();
            var su = new ServiceUnitQuery("su");

            su.Where(su.IsActive == true, su.SRRegistrationType == AppConstant.RegistrationType.InPatient)
                .OrderBy(su.ServiceUnitName.Ascending)
                .Select(su);

            var ro = new ServiceRoomQuery("ro");
            var bd = new BedQuery("bd");
            var usr = new AppUserServiceUnitQuery("usr");

            su.InnerJoin(ro).On(su.ServiceUnitID == ro.ServiceUnitID)
            .InnerJoin(bd).On(ro.RoomID == bd.RoomID)
            .LeftJoin(usr).On(usr.UserID == AppSession.UserLogin.UserID && usr.ServiceUnitID == su.ServiceUnitID)
            .Where(ro.IsActive == true, bd.IsActive == true);

            if (!AppSession.Parameter.IsUsingRoomingIn)
                su.Where(su.Or(bd.RegistrationNo == string.Empty, usr.ServiceUnitID.IsNotNull()));
            
            if (!string.IsNullOrEmpty(filterClassId))
                su.Where(bd.ClassID ==filterClassId);

            su.es.Distinct = true;
            coll.Load(su);

            cboServiceUnitID.Items.Clear();
            cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (ServiceUnit item in coll)
            {
                cboServiceUnitID.Items.Add(new RadComboBoxItem(item.ServiceUnitName, item.ServiceUnitID));
            }
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboRoomID.Items.Clear();
            cboRoomID.Text = string.Empty;
            cboBedID.Items.Clear();
            cboBedID.Text = string.Empty;
            //cboClassID.Items.Clear();
            //cboChargeClassID.Items.Clear();

            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                PopulateRoomList(cboServiceUnitID.SelectedValue, cboFilterClassID.SelectedValue);
            }
        }

        private void PopulateRoomList(string serviceUnitID, string filterClassId)
        {
            cboRoomID.Items.Clear();
            if (serviceUnitID != string.Empty)
            {
                var query = new ServiceRoomQuery("a");
                
                query.Where(query.ServiceUnitID == serviceUnitID, query.IsActive == true);
                query.Select(query.RoomID, query.RoomName);
                query.OrderBy(query.RoomName.Ascending);

                var bd = new BedQuery("b");
                query.InnerJoin(bd).On(query.RoomID == bd.RoomID)
                    .Where(bd.IsActive == true);

                if (!AppSession.Parameter.IsUsingRoomingIn)
                {
                    query.Where(query.Or(bd.RegistrationNo == string.Empty, bd.RegistrationNo == txtRegistrationNo.Text));
                }
                if (!string.IsNullOrEmpty(filterClassId))
                    query.Where(bd.ClassID == filterClassId);

                query.es.Distinct = true;

                DataTable dtb = query.LoadDataTable();

                cboRoomID.Items.Add(new RadComboBoxItem("", ""));
                foreach (DataRow row in dtb.Rows)
                {
                    cboRoomID.Items.Add(new RadComboBoxItem(row["RoomName"].ToString(), row["RoomID"].ToString()));
                }
            }
        }

        protected void cboRoomID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboBedID.Items.Clear();
            cboBedID.Text = string.Empty;
            //cboClassID.Items.Clear();
            //cboChargeClassID.Items.Clear();
        }

        protected void cboBedID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //if (string.IsNullOrEmpty(cboBedID.SelectedValue))
            //{
            //    cboClassID.Items.Clear();
            //    cboChargeClassID.Items.Clear();
            //    return;
            //}

            var bed = new Bed();
            if (bed.LoadByPrimaryKey(cboBedID.SelectedValue))
            {
                //var coll = new ClassCollection();
                //coll.Query.Where
                //    (
                //        coll.Query.IsActive == true,
                //        coll.Query.IsInPatientClass == true
                //    );
                //coll.Query.OrderBy(coll.Query.ClassID.Ascending);
                //coll.LoadAll();

                //cboClassID.Items.Clear();
                //cboChargeClassID.Items.Clear();
                //cboClassID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                //cboChargeClassID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                //foreach (Class c in coll)
                //{
                //    cboClassID.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
                //    cboChargeClassID.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
                //}
                cboClassID.SelectedValue = bed.ClassID;
                cboChargeClassID.SelectedValue = bed.DefaultChargeClassID;

                if (string.IsNullOrEmpty(cboRoomID.SelectedValue))
                {
                    PopulateRoomList(cboServiceUnitID.SelectedValue, cboFilterClassID.SelectedValue);
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

            query.es.Top = 50;
            query.Select
                (
                    query.BedID,
                    reg.RegistrationNo,
                    pat.PatientName,
                    pat.Sex, 
                    query.SRBedStatus
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
            if (!string.IsNullOrEmpty(cboFilterClassID.SelectedValue))
                query.Where(query.ClassID == cboFilterClassID.SelectedValue);

            query.OrderBy(query.BedID.Ascending);

            DataTable dtb = query.LoadDataTable();

            foreach (DataRow row in dtb.Rows)
            {
                if ((string)row["SRBedStatus"] == AppParameter.GetParameterValue(AppParameter.ParameterItem.BedStatusBooked))
                {
                    row["PatientName"] = "Booked by: " + row["PatientName"];
                }
                else if ((string)row["SRBedStatus"] == AppParameter.GetParameterValue(AppParameter.ParameterItem.BedStatusCleaning))
                {
                    row["PatientName"] = "Cleaning";
                }
            }

            dtb.AcceptChanges();

            cboBedID.DataSource = dtb;
            cboBedID.DataBind();
        }

        protected void cboBedID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["BedID"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["BedID"].ToString();
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var bed = new Bed();
            bed.LoadByPrimaryKey(cboBedID.SelectedValue);
            if (!chkIsRoomInTo.Checked)
            {
                if (!string.IsNullOrEmpty(bed.RegistrationNo))
                {
                    if (bed.RegistrationNo != txtRegistrationNo.Text)
                    {
                        args.MessageText = "Bed destination is already registered to another patient.";
                        args.IsCancel = true;
                        return;
                    }
                }
                if (bed.SRBedStatus == AppSession.Parameter.BedStatusCleaning)
                {
                    args.MessageText = "Bed destination is being cleaned.";
                    args.IsCancel = true;
                    return;
                }
                if (bed.IsRoomIn == true)
                {
                    args.MessageText = "Bed destination has patient room in. Please select another available bed.";
                    args.IsCancel = true;
                    return;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(bed.RegistrationNo))
                {
                    args.MessageText = "Bed destination is empty. Room In is not allow to this bed.";
                    args.IsCancel = true;
                    return;
                }
                if (bed.SRBedStatus == AppSession.Parameter.BedStatusBooked)
                {
                    args.MessageText = "Bed destination is already booked. Room In is not allow to this bed.";
                    args.IsCancel = true;
                    return;
                }
            }

            var transfer = new PatientTransfer();
            transfer.LoadByPrimaryKey(txtTransferNo.Text);

            SetApproval(transfer, true, args);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var transfer = new PatientTransfer();
            transfer.LoadByPrimaryKey(txtTransferNo.Text);

            SetApproval(transfer, false, args);
        }

        private void SetApproval(PatientTransfer transfer, bool isApprove, ValidateArgs args)
        {
            //update bed status
            Bed bedTo = new Bed(), bedFrom = new Bed();
            bedTo.LoadByPrimaryKey(transfer.ToBedID);
            bedFrom.LoadByPrimaryKey(transfer.FromBedID);

            var reg = new Registration();
            reg.LoadByPrimaryKey(transfer.RegistrationNo);

            if (isApprove)
            {
                if (Helper.IsInhealthIntegration)
                {
                    var grr = new Guarantor();
                    grr.LoadByPrimaryKey(reg.GuarantorID);

                    var cls = new ClassBridging();
                    cls.Query.Where(cls.Query.SRBridgingType == AppEnum.BridgingType.Inhealth.ToString(), cls.Query.ClassID == transfer.ToChargeClassID);
                    cls.Query.Load();

                    var room = new ServiceRoom();
                    room.LoadByPrimaryKey(transfer.ToRoomID);

                    var item = new ItemBridging();
                    item.Query.Where(item.Query.SRBridgingType == AppEnum.BridgingType.Inhealth.ToString(), item.Query.ItemID == room.ItemID);
                    if (!item.Query.Load())
                    {
                        var i = new Item();
                        if (i.LoadByPrimaryKey(item.ItemID))
                        {
                            args.MessageText = string.Format("{0} is not mapped to Inhealth item code", i.ItemName);
                            args.IsCancel = true;
                            return;
                        }
                    }

                    var sjp = new InhealthSJP();
                    if (sjp.LoadByPrimaryKey(reg.BpjsSepNo))
                    {
                        var isd = new BusinessObject.InhealthSJPDetail();
                        isd.Query.Where(isd.Query.Nosjp == reg.BpjsSepNo, isd.Query.Idsjp == sjp.Idsjp);
                        if (!isd.Query.Load())
                        {

                        }

                        var svc = new WebService.WSDL.Inhealth.InHealthWebService();
                        var response1 = svc.UpdateTanggalPulang(ConfigurationManager.AppSettings["InhealthHospitalToken"], sjp.Idsjp.ToInt(), reg.BpjsSepNo,
                            Convert.ToDateTime(isd.Tanggalmasuk.Value.ToString("yyyy-MM-dd")), Convert.ToDateTime(transfer.TransferDate.Value.ToString("yyyy-MM-dd")),
                            ConfigurationManager.AppSettings["InhealthHospitalID"]);
                        if (response1.ERRORCODE != "00")
                        {
                            args.MessageText = String.Format("Inhealth server error (HTTP {0}: {1}).", response1.ERRORCODE, response1.ERRORDESC);
                            args.IsCancel = true;
                            return;
                        }
                        else
                        {
                            isd.Tanggalkeluar = transfer.TransferDate.Value.Date;
                            isd.Save();
                        }

                        svc = new WebService.WSDL.Inhealth.InHealthWebService();
                        var response = svc.SimpanRuangRawat(ConfigurationManager.AppSettings["InhealthHospitalToken"], reg.BpjsSepNo,
                            Convert.ToDateTime(transfer.TransferDate.Value.ToString("yyyy-MM-dd")), ConfigurationManager.AppSettings["InhealthHospitalID"], cls.BridgingID,
                            item.BridgingID, 1);
                        if (response.ERRORCODE != "00")
                        {
                            args.MessageText = String.Format("Inhealth server error (HTTP {0}: {1}).", response.ERRORCODE, response.ERRORDESC);
                            args.IsCancel = true;
                            return;
                        }
                        else
                        {
                            sjp.Idsjp = response.ID.ToString();
                            sjp.Save();

                            isd = new BusinessObject.InhealthSJPDetail()
                            {
                                Nosjp = response.NOSJP,
                                Idsjp = response.ID.ToString(),
                                Tanggalmasuk = transfer.TransferDate.Value.Date,
                                Kodejenpelruangrawat = response.KDJENPEL
                            };
                            isd.Save();
                        }
                    }
                }

                if (reg.BedID != transfer.FromBedID)
                {
                    args.MessageText = "Patient has moved to another bed.";
                    args.IsCancel = true;
                    return;
                }

                if (transfer.IsRoomInTo == false)
                {
                    if (bedTo.SRBedStatus == AppSession.Parameter.BedStatusCleaning)
                    {
                        args.MessageText = "Bed destination is being cleaned.";
                        args.IsCancel = true;
                        return;
                    }

                    if (!string.IsNullOrEmpty(bedTo.RegistrationNo) || bedTo.SRBedStatus != AppSession.Parameter.BedStatusUnoccupied)
                    {
                        var bed = new Bed();
                        bed.LoadByPrimaryKey(bedTo.BedID);
                        if (bed.RegistrationNo != transfer.RegistrationNo)
                        {
                            args.MessageText = "Bed destination is already registered to another patient.";
                            args.IsCancel = true;
                            return;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(bedTo.RegistrationNo) || bedTo.SRBedStatus == AppSession.Parameter.BedStatusUnoccupied)
                    {
                        args.MessageText = "Bed destination is empty. Room In is not allow to this bed.";
                        args.IsCancel = true;
                        return;
                    }
                }
            }
            else
            {
                var bed = new Bed();
                bed.LoadByPrimaryKey(transfer.ToBedID);
                if (bed.IsNeedConfirmation == true & transfer.IsValidated == true)
                {
                    if (transfer.IsValidated == true)
                    {
                        args.MessageText = "Patient transfer is already validated (check in confirmed). Un-Approval is not allowed.";
                        args.IsCancel = true;
                        return;
                    }
                }
                else
                {
                    //cek apakah ada transfer yg lain setelah itu
                    var pt = new PatientTransferQuery();
                    pt.Select(pt.TransferNo);
                    pt.Where(pt.RegistrationNo == transfer.RegistrationNo, pt.IsVoid == false, pt.TransferNo > transfer.TransferNo);
                    pt.OrderBy(pt.TransferNo.Descending);
                    pt.es.Top = 1;
                    DataTable ptDt = pt.LoadDataTable();
                    var transferNo = ptDt.Rows.Count > 0 ? ptDt.Rows[0]["TransferNo"].ToString() : string.Empty;
                    if (!string.IsNullOrEmpty(transferNo))
                    {
                        args.MessageText = "Patient is already transfered with transfer no." + transferNo + ". Un-Approval is not allowed.";
                        args.IsCancel = true;
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(bedFrom.RegistrationNo))
                {
                    args.MessageText = "Bed origin is already registered to another patient. Un-Approval is not allowed.";
                    args.IsCancel = true;
                    return;
                }
                if (bedFrom.SRBedStatus != AppSession.Parameter.BedStatusUnoccupied)
                {
                    args.MessageText = "Bed origin status is not empty. Un-Approval is not allowed.";
                    args.IsCancel = true;
                    return;
                }
            }

            using (esTransactionScope trans = new esTransactionScope())
            {
                //update patient transfer
                transfer.IsApprove = isApprove;
                transfer.IsVoid = !isApprove;
                transfer.LastUpdateByUserID = AppSession.UserLogin.UserID;
                transfer.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                bedTo = new Bed();
                bedTo.LoadByPrimaryKey(transfer.ToBedID);
                bedFrom = new Bed();
                bedFrom.LoadByPrimaryKey(transfer.FromBedID);

                var bshFrom = new BedStatusHistory();
                var bshTo = new BedStatusHistory();

                //update bed status
                if (!(transfer.IsRoomInTo ?? false))
                {
                    bedTo.RegistrationNo = isApprove ? transfer.RegistrationNo : string.Empty;
                    bedTo.SRBedStatus = isApprove
                                            ? (bedTo.IsNeedConfirmation ?? false
                                                   ? AppSession.Parameter.BedStatusPending
                                                   : AppSession.Parameter.BedStatusOccupied)
                                            : AppSession.Parameter.BedStatusUnoccupied;

                    if (isApprove)
                    {
                        bshTo.AddNew();
                        bshTo.BedID = transfer.ToBedID;
                        bshTo.SRBedStatusFrom = AppSession.Parameter.BedStatusUnoccupied;
                        bshTo.SRBedStatusTo = (bedTo.IsNeedConfirmation ?? false) ? AppSession.Parameter.BedStatusPending : AppSession.Parameter.BedStatusOccupied;
                        bshTo.RegistrationNo = transfer.RegistrationNo;
                        bshTo.TransferNo = transfer.TransferNo;
                        bshTo.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        bshTo.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                    else
                    {
                        var bshToQuery = new BedStatusHistoryQuery();
                        bshToQuery.Where(
                            bshToQuery.BedID == transfer.ToBedID,
                            bshToQuery.SRBedStatusFrom == AppSession.Parameter.BedStatusUnoccupied,
                            bshToQuery.SRBedStatusTo == ((bedTo.IsNeedConfirmation ?? false) ? AppSession.Parameter.BedStatusPending : AppSession.Parameter.BedStatusOccupied),
                            bshToQuery.RegistrationNo == transfer.RegistrationNo,
                            bshToQuery.TransferNo == transfer.TransferNo);
                        bshToQuery.es.Top = 1;
                        bshToQuery.OrderBy(bshToQuery.LastUpdateDateTime.Descending);
                        bshTo.Load(bshToQuery);
                        bshTo.MarkAsDeleted();
                    }
                }
                else
                    bshTo = null;

                bedTo.IsRoomIn = transfer.IsRoomInTo;
                bedTo.LastUpdateByUserID = AppSession.UserLogin.UserID;
                bedTo.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                if (!(transfer.IsRoomInFrom ?? false))
                {
                    bedFrom.RegistrationNo = isApprove ? string.Empty : transfer.RegistrationNo;
                    bedFrom.SRBedStatus = isApprove
                                              ? (AppSession.Parameter.IsBedNeedCleanedProcess
                                                     ? AppSession.Parameter.BedStatusCleaning
                                                     : AppSession.Parameter.BedStatusUnoccupied)
                                              : AppSession.Parameter.BedStatusOccupied;

                    bedFrom.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    bedFrom.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    if (isApprove)
                    {
                        bshFrom.AddNew();
                        bshFrom.BedID = transfer.FromBedID;
                        bshFrom.SRBedStatusFrom = AppSession.Parameter.BedStatusOccupied;
                        bshFrom.SRBedStatusTo = (AppSession.Parameter.IsBedNeedCleanedProcess
                                                     ? AppSession.Parameter.BedStatusCleaning
                                                     : AppSession.Parameter.BedStatusUnoccupied);
                        bshFrom.RegistrationNo = transfer.RegistrationNo;
                        bshFrom.TransferNo = transfer.TransferNo;
                        bshFrom.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        bshFrom.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                    else
                    {
                        var bshFromQuery = new BedStatusHistoryQuery();
                        bshFromQuery.Where(
                            bshFromQuery.BedID == transfer.FromBedID,
                            bshFromQuery.SRBedStatusFrom == AppSession.Parameter.BedStatusOccupied,
                            bshFromQuery.SRBedStatusTo == (AppSession.Parameter.IsBedNeedCleanedProcess
                                                     ? AppSession.Parameter.BedStatusCleaning
                                                     : AppSession.Parameter.BedStatusUnoccupied),
                            bshFromQuery.RegistrationNo == transfer.RegistrationNo,
                            bshFromQuery.TransferNo == transfer.TransferNo);
                        bshFromQuery.es.Top = 1;
                        bshFromQuery.OrderBy(bshFromQuery.LastUpdateDateTime.Descending);
                        bshFrom.Load(bshFromQuery);
                        bshFrom.MarkAsDeleted();
                    }
                }
                else
                    bshFrom = null;
                 
                //update bed room in
                var briFColl = new BedRoomInCollection();
                var briT = new BedRoomIn();

                if (chkIsRoomInFrom.Checked)
                {
                    var bri = new BedRoomInCollection();
                    bri.Query.Where(bri.Query.BedID == transfer.FromBedID, bri.Query.DateOfExit.IsNull(),
                                    bri.Query.IsVoid == false, bri.Query.RegistrationNo != transfer.RegistrationNo);
                    bri.LoadAll();

                    bedFrom.IsRoomIn = bri.Count > 0;
                }
                
                //update registration
                reg.ServiceUnitID = isApprove ? transfer.ToServiceUnitID : transfer.FromServiceUnitID;
                reg.RoomID = isApprove ? transfer.ToRoomID : transfer.FromRoomID;
                reg.BedID = isApprove ? transfer.ToBedID : transfer.FromBedID;
                reg.ClassID = isApprove ? transfer.ToClassID : transfer.FromClassID;
                reg.ChargeClassID = isApprove ? transfer.ToChargeClassID : transfer.FromChargeClassID;
                if (reg.GuarantorID == AppSession.Parameter.SelfGuarantor)
                    reg.CoverageClassID = reg.ChargeClassID;
                reg.SmfID = isApprove ? transfer.ToSpecialtyID : transfer.FromSpecialtyID;
                reg.IsRoomIn = transfer.IsRoomInTo;

                reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                reg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                var thuColl = new PatientTransferHistoryCollection();
                var thi = new PatientTransferHistory();
                if (isApprove)
                {
                    //update PatientTransferHistory before
                    var thuQuery = new PatientTransferHistoryQuery();
                    thuQuery.Where(thuQuery.RegistrationNo == transfer.RegistrationNo);
                    thuQuery.es.Top = 1;
                    thuQuery.OrderBy(thuQuery.TransferNo.Descending);
                    thuColl.Load(thuQuery);

                    foreach (var item in thuColl)
                    {
                        item.DateOfExit = transfer.TransferDate;
                        item.TimeOfExit = transfer.TransferTime;
                        item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }

                    thi.AddNew();
                    thi.RegistrationNo = transfer.RegistrationNo;
                    thi.TransferNo = transfer.TransferNo;
                    thi.ServiceUnitID = transfer.ToServiceUnitID;
                    thi.ClassID = transfer.ToClassID;
                    thi.RoomID = transfer.ToRoomID;
                    thi.BedID = transfer.ToBedID;
                    thi.ChargeClassID = transfer.ToChargeClassID;
                    thi.DateOfEntry = transfer.TransferDate;
                    thi.TimeOfEntry = transfer.TransferTime;
                    thi.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    thi.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    thi.SmfID = transfer.ToSpecialtyID;
                    thi.FromServiceUnitID = transfer.FromServiceUnitID;
                    thi.FromClassID = transfer.FromClassID;
                    thi.FromRoomID = transfer.FromRoomID;
                    thi.FromBedID = transfer.FromBedID;
                    thi.FromChargeClassID = transfer.FromChargeClassID;
                }
                else
                {
                    //update PatientTransferHistory before
                    var thuQuery = new PatientTransferHistoryQuery();
                    thuQuery.Where(thuQuery.RegistrationNo == transfer.RegistrationNo, thuQuery.TransferNo != transfer.TransferNo);
                    thuQuery.es.Top = 1;
                    thuQuery.OrderBy(thuQuery.TransferNo.Descending);
                    thuColl.Load(thuQuery);

                    foreach (var item in thuColl)
                    {
                        item.DateOfExit = null;
                        item.TimeOfExit = null;
                        item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }

                    if (thi.LoadByPrimaryKey(transfer.RegistrationNo, transfer.TransferNo))
                        thi.MarkAsDeleted();
                }

                //update BedRoomIn before
                if (chkIsRoomInFrom.Checked)
                {
                    briFColl.Query.Where(briFColl.Query.BedID == transfer.FromBedID, //briFColl.Query.DateOfExit.IsNull(),
                                    briFColl.Query.IsVoid == false, briFColl.Query.RegistrationNo == transfer.RegistrationNo);
                    briFColl.LoadAll();

                    foreach (var item in briFColl)
                    {
                        item.DateOfExit = isApprove ? transfer.TransferDate : null;
                        item.TimeOfExit = isApprove ? transfer.TransferTime : null;
                        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();   
                    }
                }
                else
                    briFColl = null;

                //insert BedRoomIn now
                if (chkIsRoomInTo.Checked)
                {
                    if (isApprove)
                    {
                        briT.AddNew();
                        briT.BedID = cboBedID.SelectedValue;
                        briT.RegistrationNo = transfer.RegistrationNo;
                        briT.DateOfEntry = transfer.TransferDate;
                        briT.TimeOfEntry = transfer.TransferTime;
                        briT.IsVoid = false;
                        briT.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        briT.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        briT.SRBedStatus = (bedTo.IsNeedConfirmation ?? false
                                                ? AppSession.Parameter.BedStatusPending
                                                : AppSession.Parameter.BedStatusOccupied);
                    }
                    else
                    {
                        if (briT.LoadByPrimaryKey(cboBedID.SelectedValue, transfer.RegistrationNo,
                                              transfer.TransferDate ?? DateTime.Now.Date, transfer.TransferTime))
                            briT.MarkAsDeleted();
                    }
                }
                else
                    briT = null;

                //save
                transfer.Save();
                if (transfer.ToBedID != transfer.FromBedID)
                    bedFrom.Save();
                bedTo.Save();
                reg.Save();
                thuColl.Save();
                thi.Save();
                if (briFColl != null)
                    briFColl.Save();
                if (briT != null)
                    briT.Save();
                if (bshFrom != null)
                    bshFrom.Save();
                if (bshTo != null)
                    bshTo.Save();

                var bedmanag = new BedManagementCollection();
                if (isApprove)
                {
                    bedmanag.Query.Where(bedmanag.Query.BedID == transfer.ToBedID,
                                     bedmanag.Query.RegistrationNo == transfer.RegistrationNo,
                                     bedmanag.Query.IsReleased == false,
                                     bedmanag.Query.IsVoid == false);
                    bedmanag.LoadAll();
                    foreach (var b in bedmanag)
                    {
                        b.IsReleased = true;
                        b.ReleasedDateTime = (new DateTime()).NowAtSqlServer();
                        b.ReleasedByUserID = AppSession.UserLogin.UserID;
                        b.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        b.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                }
                else
                {
                    bedmanag.Query.Where(bedmanag.Query.BedID == transfer.ToBedID,
                                     bedmanag.Query.RegistrationNo == transfer.RegistrationNo,
                                     bedmanag.Query.IsReleased == true,
                                     bedmanag.Query.IsVoid == false);
                    bedmanag.LoadAll();
                    foreach (var b in bedmanag)
                    {
                        b.IsReleased = false;
                        b.ReleasedDateTime = null;
                        b.ReleasedByUserID = null;
                        b.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        b.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                }
                bedmanag.Save();



                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var transfer = new PatientTransfer();
            transfer.LoadByPrimaryKey(txtTransferNo.Text);
            SetVoid(transfer, true);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            base.OnMenuUnVoidClick(args);
        }

        private void SetVoid(PatientTransfer transfer, bool isVoid)
        {
            //update patient transfer
            transfer.IsVoid = isVoid;
            transfer.LastUpdateByUserID = AppSession.UserLogin.UserID;
            transfer.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            using (esTransactionScope trans = new esTransactionScope())
            {
                transfer.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
            ToolBarMenuEdit.Enabled = !(bool)ViewState["IsApprove"] && !(bool)ViewState["IsVoid"];
        }

        #region PatientImage
        private void PopulatePatientImage(string patientID)
        {
            // Load from database
            var patientImg = new PatientImage();
            if (patientImg.LoadByPrimaryKey(patientID))
            {
                // Show Image
                if (patientImg.Photo != null)
                {
                    imgPatientPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}",
                        Convert.ToBase64String(patientImg.Photo));
                }
                else
                {
                    imgPatientPhoto.ImageUrl = txtGender.Text == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
                }
            }
            else
                imgPatientPhoto.ImageUrl = txtGender.Text == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";

        }
        #endregion

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            switch (programID)
            {
                case AppConstant.Report.PatientTransfer:
                    printJobParameters.AddNew("TransferNo", txtTransferNo.Text);
                    break;
                case AppConstant.Report.PersyaratanTurunKls:
                    printJobParameters.AddNew("TransferNo", txtTransferNo.Text);
                    break;
                default:
                    printJobParameters.AddNew("TransferNo", txtTransferNo.Text);
                    break;
            }
        }
    }
}