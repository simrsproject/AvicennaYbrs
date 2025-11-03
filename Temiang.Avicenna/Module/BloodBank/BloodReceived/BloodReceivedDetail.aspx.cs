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
    public partial class BloodReceivedDetail : BasePageDetail
    {
        private string RegNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }

        private bool IsBypassBloodCrossMatching
        {
            get
            {
                var bloodGroup = Request.QueryString["bg"].ToString();
                var bg = new AppStandardReferenceItem();
                var isBypass = false;
                if (bg.LoadByPrimaryKey(AppEnum.StandardReference.BloodGroup.ToString(), bloodGroup))
                {
                    isBypass = string.IsNullOrEmpty(bg.CustomField) || bg.CustomField == "0";
                }
                
                return (AppSession.Parameter.IsBypassBloodCrossMatching || isBypass);
            }
        }

        private bool IsReturnable
        {
            get
            {
                var bloodGroup = Request.QueryString["bg"].ToString();
                var bg = new AppStandardReferenceItem();
                var isReturnable = false;
                if (bg.LoadByPrimaryKey(AppEnum.StandardReference.BloodGroup.ToString(), bloodGroup))
                {
                    isReturnable = bg.CustomField2 == "1";
                }

                return isReturnable;
            }
        }

        #region Page Event & Initialize

        private string FormType
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "##";
            UrlPageList = "BloodReceivedList.aspx?type=" + FormType;

            ProgramID = FormType == string.Empty ? AppConstant.Program.BloodBankReceived : AppConstant.Program.BloodBankReturn;

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
                    txtPatientName.Text = patient.PatientName;
                    var std = new AppStandardReferenceItem();
                    txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.SRSalutation) ? std.ItemName : string.Empty;
                    txtGender.Text = patient.Sex;
                    txtPlaceDOB.Text = string.Format("{0}, {1}", patient.CityOfBirth, Convert.ToDateTime(patient.DateOfBirth).ToString("dd-MMM-yyyy"));

                    txtAgeDay.Value = Helper.GetAgeInDay(patient.DateOfBirth.Value);
                    txtAgeMonth.Value = Helper.GetAgeInMonth(patient.DateOfBirth.Value);
                    txtAgeYear.Value = Helper.GetAgeInYear(patient.DateOfBirth.Value);

                    cboSRBloodType.SelectedValue = patient.SRBloodType;
                    rblBloodRhesus.SelectedValue = (patient.BloodRhesus == "+" ? "0" : "1");
                }
                else
                {
                    txtMedicalNo.Text = string.Empty;
                    txtPatientName.Text = string.Empty;
                    txtSalutation.Text = string.Empty;
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

                if (Helper.GuarantorBpjsCasemix.Contains(registration.GuarantorID))
                {
                    if (AppSession.Parameter.CasemixValidationRegistrationType.Contains(registration.SRRegistrationType))
                        pnlValidatedByCasemix.Visible = true;
                    else
                        pnlValidatedByCasemix.Visible = false;
                }
                else
                    pnlValidatedByCasemix.Visible = false;
            }
            else
            {
                txtMedicalNo.Text = string.Empty;
                txtPatientName.Text = string.Empty;
                txtSalutation.Text = string.Empty;
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

                pnlValidatedByCasemix.Visible = false;
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItem, grdItem);
            ajax.AddAjaxSetting(grdItem, cboSRBloodType);
            ajax.AddAjaxSetting(grdItem, rblBloodRhesus);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new BloodBankTransaction();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                if (!IsValidatedByCasemix(entity, args))
                    return;
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        private bool IsValidatedByCasemix(BloodBankTransaction entity, ValidateArgs args)
        {
            if (!(entity.IsValidatedByCasemix ?? true))
            {
                args.MessageText = "Need validation by Casemix";
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        protected override void OnMenuNewClick()
        {
            //OnPopulateEntryControl(new BloodBankTransaction());

            //PopulateRegistrationInformation(string.IsNullOrEmpty(txtRegistrationNo.Text)
            //                                        ? RegNo
            //                                        : txtRegistrationNo.Text);

            //txtTransactionNo.Text = GetNewTransactionNo();
            //txtTransctionDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            //txtRequestDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            //txtRequestTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");

            //ViewState["IsApproved"] = false;
            //ViewState["IsVoid"] = false;
        }

        protected override void OnMenuEditClick()
        {
            if (!IsBypassBloodCrossMatching)
            {
                cboSRBloodType.Enabled = false;
                rblBloodRhesus.Enabled = false;
            }
            else
            {
                cboSRBloodType.Enabled = BloodBankTransactionItems.Count == 0;
                rblBloodRhesus.Enabled = BloodBankTransactionItems.Count == 0;
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            //var entity = new BloodBankTransaction();
            //if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            //{
            //    entity.MarkAsDeleted();

            //    SaveEntity(entity);
            //}
            //else
            //    args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            //var entity = new BloodBankTransaction();
            //entity.AddNew();

            //SetEntityValue(entity);
            //SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            int maxQty = txtQtyBagRequest.Value.ToInt();
            if (pnlValidatedByCasemix.Visible)
                maxQty = txtQtyBagCasemixAppr.Value.ToInt();
            
            var count = BloodBankTransactionItems.Count(item => item.IsVoid == false && item.IsProceedToTransfusion == true);
            if (count > maxQty)
            {
                args.MessageText = "Defining detail bag exceeds the number of requests.";
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

        //public override bool? OnGetStatusMenuApproval()
        //{
        //    return ViewState["IsApproved"] == null ? false : !(bool)ViewState["IsApproved"];
        //}

        //public override bool OnGetStatusMenuVoid()
        //{
        //    return !(bool)ViewState["IsVoid"];
        //}

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItem(newVal);
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
            txtDiagnose.Text = bb.Diagnose;
            txtReason.Text = bb.Reason;
            txtOfficerByUserID.Text = bb.OfficerByUserID;
            if (!string.IsNullOrEmpty(txtOfficerByUserID.Text))
            {
                var usr = new AppUser();
                usr.LoadByPrimaryKey(bb.OfficerByUserID);
                txtOfficerByUserName.Text = usr.UserName;
            }
            else
                txtOfficerByUserName.Text = AppSession.UserLogin.UserName;
            chkIsValidatedByCasemix.Checked = bb.IsValidatedByCasemix ?? true;
            txtCasemixNotes.Text = bb.CasemixNotes;
            txtQtyBagCasemixAppr.Value = Convert.ToDouble(bb.QtyBagCasemixAppr);

            PopulateItemGrid();

            //ViewState["IsApproved"] = bb.IsApproved ?? false;
            //ViewState["IsVoid"] = bb.IsVoid ?? false;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            //var entity = new BloodBankTransaction();
            //if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            //{
            //    args.MessageText = AppConstant.Message.RecordNotExist;
            //    args.IsCancel = true;
            //    return;
            //}
            //if (entity.IsApproved ?? false)
            //{
            //    args.MessageText = AppConstant.Message.RecordHasApproved;
            //    args.IsCancel = true;
            //    return;
            //}
            //if (entity.IsVoid ?? false)
            //{
            //    args.MessageText = AppConstant.Message.RecordHasVoided;
            //    args.IsCancel = true;
            //    return;
            //}

            //SetApproval(entity, true, args);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            //var entity = new BloodBankTransaction();
            //if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            //{
            //    args.MessageText = AppConstant.Message.RecordNotExist;
            //    args.IsCancel = true;
            //    return;
            //}

            //if (entity.IsVoid ?? false)
            //{
            //    args.MessageText = AppConstant.Message.RecordHasVoided;
            //    args.IsCancel = true;
            //    return;
            //}

            //if (entity.IsApproved == false)
            //{
            //    args.MessageText = AppConstant.Message.RecordHasNotApproved;
            //    args.IsCancel = true;
            //    return;
            //}

            //SetApproval(entity, false, args);
        }

        private void SetApproval(BloodBankTransaction entity, bool isApproval, ValidateArgs args)
        {
            entity.IsApproved = isApproval;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.Save();
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            //var entity = new BloodBankTransaction();
            //if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            //{
            //    args.MessageText = AppConstant.Message.RecordNotExist;
            //    args.IsCancel = true;
            //    return;
            //}
            //if (entity.IsVoid ?? false)
            //{
            //    args.MessageText = AppConstant.Message.RecordHasVoided;
            //    args.IsCancel = true;
            //    return;
            //}

            //SetVoid(entity, true);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            //var entity = new BloodBankTransaction();
            //if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            //{
            //    args.MessageText = AppConstant.Message.RecordNotExist;
            //    args.IsCancel = true;
            //    return;
            //}

            //SetVoid(entity, false);
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
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (var item in BloodBankTransactionItems)
            {
                item.TransactionNo = txtTransactionNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(BloodBankTransaction entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();

                if (IsBypassBloodCrossMatching)
                {
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(entity.RegistrationNo);

                    var pat = new Patient();
                    pat.LoadByPrimaryKey(reg.PatientID);
                    pat.SRBloodType = cboSRBloodType.SelectedValue;
                    pat.BloodRhesus = rblBloodRhesus.SelectedItem == null ? "+" : rblBloodRhesus.SelectedItem.Text;
                    pat.Save();
                }

                if (BloodBankTransactionItems.Count > 0)
                {
                    var bagnos = (BloodBankTransactionItems.Select(i => i.BagNo)).Distinct();

                    var bloodBalances = new BloodBalanceCollection();
                    bloodBalances.Query.Where
                    (
                        bloodBalances.Query.BagNo.In(bagnos)
                    );
                    bloodBalances.LoadAll();
                    
                    var bloodBagNos = new BloodBagNoCollection();
                    bloodBagNos.Query.Where(bloodBagNos.Query.BagNo.In(bagnos));
                    bloodBagNos.LoadAll();

                    var idx = 1;
                    foreach (var item in BloodBankTransactionItems)
                    {
                        if (item.IsVoid == false)
                        {
                            if (item.IsProceedToTransfusion == true)
                            {
                                var balance =
                                    bloodBalances.SingleOrDefault(
                                        ib =>
                                            ib.SRBloodSource == item.SRBloodSource &&
                                            ib.SRBloodSourceFrom == item.SRBloodSourceFrom && ib.BagNo == item.BagNo &&
                                            ib.Balance > 0);
                                if (balance != null)
                                {
                                    balance.Balance -= 1;
                                    balance.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                    balance.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                }


                                if (AppSession.Parameter.IsAutoTransfusionBillProceedOnBloodDistribution)
                                {
                                    if (item.IsTransfusionBillProceed == null || item.IsTransfusionBillProceed == false)
                                    {
                                        item.IsTransfusionBillProceed = true;

                                        var it = new AppStandardReferenceItem();
                                        if (it.LoadByPrimaryKey(AppEnum.StandardReference.BloodGroup.ToString(),
                                            entity.SRBloodGroupRequest) && !string.IsNullOrEmpty(it.ReferenceID))
                                        {
                                            var itemId = it.ReferenceID;

                                            var i = new Item();
                                            if (i.LoadByPrimaryKey(itemId))
                                            {
                                                Temiang.Avicenna.WebService.BillingChargeService.BillingProcess(entity.RegistrationNo, i.ItemID, string.Format("{0:000}", idx), 1, "ds", true);
                                            }
                                        }
                                    }
                                }

                            }
                        }
                        else
                        {
                            if (item.SRBloodBagStatus == "2")
                            {
                                var balance =
                                    bloodBalances.SingleOrDefault(
                                        ib =>
                                            ib.SRBloodSource == item.SRBloodSource &&
                                            ib.SRBloodSourceFrom == item.SRBloodSourceFrom && ib.BagNo == item.BagNo &&
                                            ib.Balance == 0);
                                if (balance != null)
                                {
                                    balance.Balance += 1;
                                    balance.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                    balance.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                }

                                var bagno = bloodBagNos.SingleOrDefault(bn => bn.BagNo == item.BagNo);
                                if (bagno != null)
                                {
                                    bagno.IsCrossMatching = false;
                                    bagno.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                    bagno.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                }
                            }
                        }

                        idx += 1;
                    }
                    BloodBankTransactionItems.Save();
                    bloodBalances.Save();
                    bloodBagNos.Save();
                }

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
                        que.TransactionNo > txtTransactionNo.Text, que.IsApproved == true
                    );
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.TransactionNo < txtTransactionNo.Text, que.IsApproved == true
                    );
                que.OrderBy(que.TransactionNo.Descending);
            }

            var entity = new BloodBankTransaction();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function of Blood Bank Transaction Item

        private BloodBankTransactionItemCollection BloodBankTransactionItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collBloodBankTransactionItemReceived" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((BloodBankTransactionItemCollection)(obj));
                    }
                }

                var coll = new BloodBankTransactionItemCollection();
                var query = new BloodBankTransactionItemQuery("a");
                var bn = new BloodBagNoQuery("bn");
                var bg = new AppStandardReferenceItemQuery("b");
                var usr = new AppUserQuery("c");
                var bs = new AppStandardReferenceItemQuery("d");

                query.Select
                    (
                        query,
                        bn.VolumeBag.Coalesce("0").As("refBloodBagNo_VolumeBag"),
                        bn.ExpiredDateTime.As("refBloodBagNo_ExpiredDateTime"),
                        bg.ItemName.As("refToAppStandardReferenceItem_ItemName"),
                        usr.UserName.As("refToAppUser_ExaminerByUserName"),
                        bs.ItemName.As("refToAppStandardReferenceItem_BloodBagStatus")
                    );
                query.InnerJoin(bn).On(bn.BagNo == query.BagNo);
                query.InnerJoin(bg).On(bg.ItemID == query.SRBloodGroupReceived && bg.StandardReferenceID == AppEnum.StandardReference.BloodGroup);
                query.LeftJoin(usr).On(usr.UserID == query.ExaminerByUserID);
                query.LeftJoin(bs).On(bs.ItemID == query.SRBloodBagStatus && bs.StandardReferenceID == AppEnum.StandardReference.BloodBagStatus);
                query.Where(query.TransactionNo == txtTransactionNo.Text, query.IsCrossmatchingPassed == true);
                coll.Load(query);

                Session["collBloodBankTransactionItemReceived" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collBloodBankTransactionItemReceived" + Request.UserHostName] = value;
            }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);

            if (FormType == "ret")
            {
                grdItem.Columns[0].Visible = false;
                grdItem.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
            }
            else
            {
                grdItem.Columns[0].Visible = isVisible;
                grdItem.MasterTableView.CommandItemDisplay = IsBypassBloodCrossMatching && isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            }
            
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = IsReturnable && isVisible;

            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            BloodBankTransactionItems = null; //Reset Record Detail
            grdItem.DataSource = BloodBankTransactionItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private BloodBankTransactionItem FindItem(String bagNo)
        {
            BloodBankTransactionItemCollection coll = BloodBankTransactionItems;
            BloodBankTransactionItem retEntity = null;
            foreach (BloodBankTransactionItem rec in coll)
            {
                if (rec.BagNo.Equals(bagNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = BloodBankTransactionItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String bagNo =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][BloodBankTransactionItemMetadata.ColumnNames.BagNo]);
            BloodBankTransactionItem entity = FindItem(bagNo);
            if (entity != null && entity.IsVoid == false && entity.TransfusionStartDateTime == null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String bagNo =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][BloodBankTransactionItemMetadata.ColumnNames.BagNo]);
            BloodBankTransactionItem entity = FindItem(bagNo);
            if (entity != null && entity.IsVoid == false && entity.TransfusionStartDateTime == null)
                entity.IsVoid = true;

            if (!IsBypassBloodCrossMatching)
            {
                cboSRBloodType.Enabled = false;
                rblBloodRhesus.Enabled = false;
            }
            else
            {
                cboSRBloodType.Enabled = BloodBankTransactionItems.Count == 0;
                rblBloodRhesus.Enabled = BloodBankTransactionItems.Count == 0;
            }
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            BloodBankTransactionItem entity = BloodBankTransactionItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(BloodBankTransactionItem entity, GridCommandEventArgs e)
        {
            var userControl = (BloodReceivedItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.BagNo = userControl.BagNo;
                entity.ReceivedDate = userControl.ReceivedDate;
                entity.ReceivedTime = userControl.ReceivedTime;
                entity.SRBloodGroupReceived = userControl.SrBloodGroupReceived;
                entity.BloodGroupReceived = userControl.BloodGroupReceived;
                entity.SRBloodBagStatus = "2";
                entity.BloodBagStatusName = "Distributed";
                entity.IsScreeningLabelPassedPmi = userControl.IsScreeningLabelPassedPmi;
                entity.IsExpiredDate = userControl.IsExpiredDate;
                entity.IsLeak = userControl.IsLeak;
                entity.IsHemolysis = userControl.IsHemolysis;
                entity.IsCrossMatchingSuitability = userControl.IsCrossMatchingSuitability;
                entity.CrossmatchCompatibleMajor = userControl.CrossmatchCompatibleMajor;
                entity.CrossmatchCompatibleMinor = userControl.CrossmatchCompatibleMinor;
                entity.CrossmatchCompatibleMinorLevel = userControl.CrossmatchCompatibleMinorLevel;
                entity.CrossmatchCompatibleAutoControl = userControl.CrossmatchCompatibleAutoControl;
                entity.CrossmatchCompatibleAutoControlLevel = userControl.CrossmatchCompatibleAutoControlLevel;
                entity.CrossmatchCompatibleDctControl = userControl.CrossmatchCompatibleDctControl;
                entity.CrossmatchCompatibleDctControlLevel = userControl.CrossmatchCompatibleDctControlLevel;
                entity.IsClotting = userControl.IsClotting;
                entity.IsBloodTypeCompatibility = userControl.IsBloodTypeCompatibility;
                entity.IsLabelDonorsMatchesWithPatientsForm = userControl.IsLabelDonorsMatchesWithPatientsForm;
                entity.IsScreeningLabelPassedBd = userControl.IsScreeningLabelPassedBd;
                entity.ExaminerByUserID = userControl.ExaminerByUserId;
                entity.UnitOfficerByUserID = userControl.ReceivedByUserID;
                entity.UnitOfficer = userControl.UnitOfficer;
                entity.SRBloodSource = userControl.SrBloodSource;
                entity.SRBloodSourceFrom = userControl.SrBloodSourceFrom;
                entity.BloodBagTemperature = userControl.BloodBagTemperature;
                entity.BloodBagNotes = userControl.BloodBagNotes;
                entity.IsVoid = userControl.IsVoid;
                entity.IsScreening1 = userControl.IsScreening1;
                entity.IsScreening2 = userControl.IsScreening2;
                entity.IsScreening3 = userControl.IsScreening3;

                bool isBypass = false;
                var bg = new AppStandardReferenceItem();
                if (bg.LoadByPrimaryKey(AppEnum.StandardReference.BloodGroup.ToString(), cboSRBloodGroupRequest.SelectedValue))
                {
                    isBypass = bg.CustomField == "0";
                }

                if (IsBypassBloodCrossMatching)
                    entity.IsCrossmatchingPassed = true;

                if (entity.IsLeak == false && entity.IsHemolysis == false && entity.IsClotting == false && entity.IsBloodTypeCompatibility == true && entity.IsLabelDonorsMatchesWithPatientsForm == true && entity.IsCrossmatchingPassed == true)
                    entity.IsProceedToTransfusion = true;
                else
                    entity.IsProceedToTransfusion = false;

                var bn = new BloodBagNo();
                if (bn.LoadByPrimaryKey(entity.BagNo))
                {
                    entity.VolumeBag = bn.VolumeBag ?? 0;
                    entity.ExpiredDateTime = bn.ExpiredDateTime;
                }
                else
                {
                    entity.VolumeBag = 0;
                    entity.ExpiredDateTime = null;
                }
            }
        }

        protected void grdItem_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                BloodBankTransactionItem item = BloodBankTransactionItems[e.Item.DataSetIndex];
                if (item != null)
                {
                    if (item.IsVoid ?? false)
                    {
                        for (int i = 0; i < e.Item.Cells.Count; i++)
                        {
                            if (i > 0 && i < e.Item.Cells.Count)
                                e.Item.Cells[i].Font.Strikeout = true;
                        }
                    }
                }
            }
        }

        #endregion
    }
}
