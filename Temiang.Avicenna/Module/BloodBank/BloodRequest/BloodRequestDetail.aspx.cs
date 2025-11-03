using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.BloodBank
{
    public partial class BloodRequestDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private string RegNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "##";
            UrlPageList = "BloodRequestList.aspx";

            ProgramID = AppConstant.Program.BloodBankRequest;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRBloodType, AppEnum.StandardReference.BloodType);
                StandardReference.InitializeIncludeSpace(cboSRBloodGroupRequest, AppEnum.StandardReference.BloodGroup);

                PopulateRegistrationInformation(RegNo);
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            ToolBarMenuSearch.Visible = false;
            ToolBarMenuAdd.Visible = false;
        }

        private void PopulateRegistrationInformation(string registrationNo)
        {
            var registration = new Registration();
            if (registration.LoadByPrimaryKey(registrationNo))
            {
                txtRegistrationNo.Text = registrationNo;
                var patient = new Patient();
                if (patient.LoadByPrimaryKey(registration.PatientID))
                {
                    txtMedicalNo.Text = patient.MedicalNo;
                    var std = new AppStandardReferenceItem();
                    txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.SRSalutation) ? std.ItemName : string.Empty;
                    txtPatientName.Text = patient.PatientName;
                    txtGender.Text = patient.Sex;
                    txtPlaceDOB.Text = string.Format("{0}, {1}", patient.CityOfBirth, Convert.ToDateTime(patient.DateOfBirth).ToString("dd-MMM-yyyy"));
                    txtAgeDay.Value = Helper.GetAgeInDay(patient.DateOfBirth.Value);
                    txtAgeMonth.Value = Helper.GetAgeInMonth(patient.DateOfBirth.Value);
                    txtAgeYear.Value = Helper.GetAgeInYear(patient.DateOfBirth.Value);

                    cboSRBloodType.SelectedValue = patient.SRBloodType;
                    rblBloodRhesus.SelectedValue = (patient.BloodRhesus == "-" ? "1" : "0");
                }
                else
                {
                    txtMedicalNo.Text = string.Empty;
                    txtSalutation.Text = string.Empty;
                    txtPatientName.Text = string.Empty;
                    txtGender.Text = string.Empty;
                    txtPlaceDOB.Text = string.Empty;
                    txtAgeDay.Value = 0;
                    txtAgeMonth.Value = 0;
                    txtAgeYear.Value = 0;

                    cboSRBloodType.SelectedValue = string.Empty;
                    cboSRBloodType.Text = string.Empty;
                    rblBloodRhesus.SelectedValue = "0";
                }

                txtParamedicID.Text = registration.ParamedicID;
                var par = new Paramedic();
                par.LoadByPrimaryKey(txtParamedicID.Text);
                lblParamedicName.Text = par.ParamedicName;

                txtServiceUnitID.Text = registration.ServiceUnitID;
                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(txtServiceUnitID.Text);
                lblServiceUnitName.Text = unit.ServiceUnitName;
                txtRoomID.Text = registration.RoomID;

                var room = new ServiceRoom();
                room.LoadByPrimaryKey(txtRoomID.Text);
                lblRoomName.Text = room.RoomName;

                txtBedID.Text = registration.BedID;

                txtGuarantorID.Text = registration.GuarantorID;
                var guar = new Guarantor();
                guar.LoadByPrimaryKey(txtGuarantorID.Text);
                lblGuarantorName.Text = guar.GuarantorName;
            }
            else
            {
                txtMedicalNo.Text = string.Empty;
                txtSalutation.Text = string.Empty;
                txtPatientName.Text = string.Empty;
                txtGender.Text = string.Empty;
                txtPlaceDOB.Text = string.Empty;

                txtAgeDay.Value = 0;
                txtAgeMonth.Value = 0;
                txtAgeYear.Value = 0;

                txtParamedicID.Text = string.Empty;
                lblParamedicName.Text = string.Empty;

                txtServiceUnitID.Text = string.Empty;
                lblServiceUnitName.Text = string.Empty;
                txtRoomID.Text = string.Empty;
                lblRoomName.Text = string.Empty;
                txtBedID.Text = registration.BedID;
                txtGuarantorID.Text = registration.GuarantorID;
                lblGuarantorName.Text = string.Empty;

                cboSRBloodType.SelectedValue = string.Empty;
                cboSRBloodType.Text = string.Empty;
                rblBloodRhesus.SelectedValue = "0";
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(cboSRBloodGroupRequest, cboSRBloodGroupRequest);
            ajax.AddAjaxSetting(cboSRBloodGroupRequest, txtVolumeBag);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new BloodBankTransaction());

            PopulateRegistrationInformation(string.IsNullOrEmpty(txtRegistrationNo.Text)
                                                    ? RegNo
                                                    : txtRegistrationNo.Text);

            txtTransactionNo.Text = GetNewTransactionNo();
            txtTransctionDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtRequestDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtRequestTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");
            txtQtyBagRequest.Value = 1;
            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new BloodBankTransaction();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                entity.MarkAsDeleted();

                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (txtQtyBagRequest.Value <= 0)
            {
                args.MessageText = "Qty Bag must be greater than zero.";
                args.IsCancel = true;
                return;
            }

            var entity = new BloodBankTransaction();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (txtQtyBagRequest.Value <= 0)
            {
                args.MessageText = "Qty Bag must be greater than zero.";
                args.IsCancel = true;
                return;
            }

            var entity = new BloodBankTransaction();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
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
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text.Trim());
            auditLogFilter.TableName = "BloodBankTransaction";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
            printJobParameters.AddNew("p_UserID", AppSession.UserLogin.UserID);
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return ViewState["IsApproved"] == null ? false : !(bool)ViewState["IsApproved"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {

        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new BloodBankTransaction();
            if (parameters.Length > 0)
            {
                var transNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(transNo);
            }
            else
                entity.LoadByPrimaryKey(txtTransactionNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var bb = (BloodBankTransaction)entity;

            txtTransactionNo.Text = bb.TransactionNo;
            txtTransctionDate.SelectedDate = bb.TransactionDate;
            txtRegistrationNo.Text = bb.RegistrationNo;
            PopulateRegistrationInformation(txtRegistrationNo.Text);

            txtRequestDate.SelectedDate = bb.RequestDate;
            txtRequestTime.Text = bb.RequestTime;
            txtBloodBankNo.Text = bb.BloodBankNo;
            txtPdutNo.Text = bb.PdutNo;
            cboSRBloodGroupRequest.SelectedValue = bb.SRBloodGroupRequest;
            txtHbResultValue.Value = Convert.ToDouble(bb.HbResultValue);
            txtQtyBagRequest.Value = Convert.ToDouble(bb.QtyBagRequest);
            txtVolumeBag.Value = Convert.ToDouble(bb.VolumeBag);
            if (!string.IsNullOrEmpty(bb.Diagnose))
                txtDiagnose.Text = bb.Diagnose;
            else
            {
                var rim = new RegistrationInfoMedicQuery();
                rim.Select(rim.Info3);
                rim.Where(rim.RegistrationNo == txtRegistrationNo.Text,
                                rim.SRMedicalNotesInputType == "SOAP");
                rim.OrderBy(rim.CreatedDateTime.Descending);
                rim.es.Top = 1;
                DataTable rimdt = rim.LoadDataTable();
                txtDiagnose.Text = rimdt.Rows.Count > 0 ? rimdt.Rows[0]["Info3"].ToString() : string.Empty;
            }
            txtReason.Text = bb.Reason;

            if (!string.IsNullOrEmpty(bb.OfficerByUserID))
            {
                var usr = new AppUserQuery();
                usr.Where(usr.UserID == bb.OfficerByUserID);
                cboOfficerByUserID.DataSource = usr.LoadDataTable();
                cboOfficerByUserID.DataBind();
                cboOfficerByUserID.SelectedValue = bb.OfficerByUserID;
            }
            else
            {
                var usr = new AppUserQuery();
                usr.Where(usr.UserID == AppSession.UserLogin.UserID);
                cboOfficerByUserID.DataSource = usr.LoadDataTable();
                cboOfficerByUserID.DataBind();
                cboOfficerByUserID.SelectedValue = AppSession.UserLogin.UserID; 
            }

            ViewState["IsApproved"] = bb.IsApproved ?? false;
            ViewState["IsVoid"] = bb.IsVoid ?? false;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new BloodBankTransaction();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, true, args);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new BloodBankTransaction();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            if (entity.IsApproved == false)
            {
                args.MessageText = AppConstant.Message.RecordHasNotApproved;
                args.IsCancel = true;
                return;
            }

            var detail = new BloodBankTransactionItemCollection();
            detail.Query.Where(detail.Query.TransactionNo == txtTransactionNo.Text);
            detail.LoadAll();
            if (detail.Count > 0)
            {
                args.MessageText = "Data has been processed.";
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, false, args);
        }

        private void SetApproval(BloodBankTransaction entity, bool isApproval, ValidateArgs args)
        {
            entity.IsApproved = isApproval;
            if (!AppSession.Parameter.IsNeedBloodSample)
                entity.IsBloodSampleGiven = true;

            entity.QtyBagCasemixAppr = entity.QtyBagRequest;

            if (isApproval)
            {
                var reg = new Registration();
                if (reg.LoadByPrimaryKey(entity.RegistrationNo) && Helper.GuarantorBpjsCasemix.Contains(reg.GuarantorID))
                {
                    if (AppSession.Parameter.CasemixValidationRegistrationType.Contains(reg.SRRegistrationType))
                    {
                        if (entity.QtyBagRequest > AppSession.Parameter.MaximumQtyBloodBagRequestPassedCasemix)
                            entity.IsValidatedByCasemix = false;
                        else
                            entity.IsValidatedByCasemix = true;
                    }
                    else
                        entity.IsValidatedByCasemix = true;
                }
                else
                    entity.IsValidatedByCasemix = true;
            }
            else
                entity.IsValidatedByCasemix = null;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            entity.Save();
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new BloodBankTransaction();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, true);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            var entity = new BloodBankTransaction();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private void SetVoid(BloodBankTransaction entity, bool isVoid)
        {
            //header
            entity.IsVoid = isVoid;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.Save();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(BloodBankTransaction entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtTransactionNo.Text = GetNewTransactionNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionDate = txtTransctionDate.SelectedDate;
            entity.RequestDate = txtRequestDate.SelectedDate;
            entity.RequestTime = txtRequestTime.TextWithLiterals;
            entity.BloodBankNo = txtBloodBankNo.Text;
            entity.PdutNo = txtPdutNo.Text;
            entity.RegistrationNo = txtRegistrationNo.Text;
            entity.SRBloodGroupRequest = cboSRBloodGroupRequest.SelectedValue;
            entity.HbResultValue = Convert.ToDecimal(txtHbResultValue.Value);
            entity.QtyBagRequest = Convert.ToInt16(txtQtyBagRequest.Value);
            entity.VolumeBag = Convert.ToInt16(txtVolumeBag.Value);
            entity.Diagnose = txtDiagnose.Text;
            entity.Reason = txtReason.Text;
            entity.OfficerByUserID = cboOfficerByUserID.SelectedValue;
            entity.IsBloodSampleGiven = !AppSession.Parameter.IsNeedBloodSample;
            
            entity.IsApproved = false;
            entity.IsVoid = false;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        }

        private void SaveEntity(BloodBankTransaction entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();

                var reg = new Registration();
                reg.LoadByPrimaryKey(entity.RegistrationNo);

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                pat.SRBloodType = cboSRBloodType.SelectedValue;
                pat.BloodRhesus = rblBloodRhesus.SelectedItem == null ? "+" : rblBloodRhesus.SelectedItem.Text;
                pat.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new BloodBankTransactionQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.TransactionNo > txtTransactionNo.Text
                    );
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.TransactionNo < txtTransactionNo.Text
                    );
                que.OrderBy(que.TransactionNo.Descending);
            }

            var entity = new BloodBankTransaction();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        protected void cboOfficerByUserID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.UserByUnitItemRequested((RadComboBox)sender, AppSession.Parameter.ServiceUnitBloodBankID, e.Text);
        }

        protected void cboOfficerByUserID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.UserItemDataBound(e);
        }

        protected void cboSRBloodGroupRequest_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var app = new AppStandardReferenceItem();
            if (app.LoadByPrimaryKey(AppEnum.StandardReference.BloodGroup.ToString(), e.Value) && !string.IsNullOrEmpty(app.Note))
            {
                try
                {
                    txtVolumeBag.Value = Convert.ToDouble(app.Note.ToInt());
                }
                catch (Exception)
                {
                    txtVolumeBag.Value = 0;
                }
            }
            else
                txtVolumeBag.Value = 0;
        }

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.BloodBankTxNo);
            return _autoNumber.LastCompleteNumber;
        }
    }
}
