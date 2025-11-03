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

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class NonPatientMealOrderDetail : BasePageDetail
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
            UrlPageList = "NonPatientMealOrderList.aspx";

            ProgramID = AppConstant.Program.NonPatientCustomerMealOrder;

            if (!IsPostBack)
            {
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
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new MealOrderNonPatient());

            PopulateRegistrationInformation(string.IsNullOrEmpty(txtRegistrationNo.Text)
                                                    ? RegNo
                                                    : txtRegistrationNo.Text);

            txtTransactionNo.Text = GetNewTransactionNo();
            txtTransctionDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new MealOrderNonPatient();
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
            var entity = new MealOrderNonPatient();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new MealOrderNonPatient();
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
            auditLogFilter.TableName = "MealOrderNonPatient";
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
            RefreshCommandItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new MealOrderNonPatient();
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
            var mo = (MealOrderNonPatient)entity;

            txtTransactionNo.Text = mo.TransactionNo;
            txtTransctionDate.SelectedDate = mo.TransactionDate;
            txtRegistrationNo.Text = mo.RegistrationNo;
            PopulateRegistrationInformation(txtRegistrationNo.Text);
            PopulateItemGrid();

            ViewState["IsApproved"] = mo.IsApproved ?? false;
            ViewState["IsVoid"] = mo.IsVoid ?? false;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new MealOrderNonPatient();
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
            var entity = new MealOrderNonPatient();
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
            if (entity.IsDistributed == true)
            {
                args.MessageText = "Data has been processed.";
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, false, args);
        }

        private void SetApproval(MealOrderNonPatient entity, bool isApproval, ValidateArgs args)
        {
            entity.IsApproved = isApproval;
            if (isApproval)
            {
                entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                entity.ApprovedByUserID = AppSession.UserLogin.UserID;
            }
            else {
                entity.str.ApprovedDateTime = string.Empty;
                entity.ApprovedByUserID = null;
            }
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.Save();
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new MealOrderNonPatient();
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
            var entity = new MealOrderNonPatient();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private void SetVoid(MealOrderNonPatient entity, bool isVoid)
        {
            //header
            entity.IsVoid = isVoid;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.Save();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(MealOrderNonPatient entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtTransactionNo.Text = GetNewTransactionNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionDate = txtTransctionDate.SelectedDate;
            entity.RegistrationNo = txtRegistrationNo.Text;
            entity.IsApproved = false;
            entity.IsVoid = false;
            entity.IsDistributed = false;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (var item in MealOrderNonPatientItems)
            {
                item.TransactionNo = entity.TransactionNo;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(MealOrderNonPatient entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                MealOrderNonPatientItems.Save();
                

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

        #region Record Detail Method Function of Meal Order Non Patient Item

        private MealOrderNonPatientItemCollection MealOrderNonPatientItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collMealOrderNonPatientItem" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((MealOrderNonPatientItemCollection)(obj));
                    }
                }

                var coll = new MealOrderNonPatientItemCollection();
                var query = new MealOrderNonPatientItemQuery("a");
                var fq = new FoodQuery("b");
                query.Select
                    (
                        query,
                        fq.FoodName.As("refToFood_FoodName")

                    );
                query.InnerJoin(fq).On(fq.FoodID == query.FoodID);
                query.Where(query.TransactionNo == txtTransactionNo.Text);
                coll.Load(query);

                Session["collMealOrderNonPatientItem" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collMealOrderNonPatientItem" + Request.UserHostName] = value;
            }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 2].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible
                                                                             ? GridCommandItemDisplay.Top
                                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            MealOrderNonPatientItems = null; //Reset Record Detail
            grdItem.DataSource = MealOrderNonPatientItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private MealOrderNonPatientItem FindItem(String seqNo)
        {
            MealOrderNonPatientItemCollection coll = MealOrderNonPatientItems;
            MealOrderNonPatientItem retEntity = null;
            foreach (MealOrderNonPatientItem rec in coll)
            {
                if (rec.SequenceNo.Equals(seqNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = MealOrderNonPatientItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String seqNo =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][MealOrderNonPatientItemMetadata.ColumnNames.SequenceNo]);
            MealOrderNonPatientItem entity = FindItem(seqNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String seqNo =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][MealOrderNonPatientItemMetadata.ColumnNames.SequenceNo]);
            MealOrderNonPatientItem entity = FindItem(seqNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            MealOrderNonPatientItem entity = MealOrderNonPatientItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(MealOrderNonPatientItem entity, GridCommandEventArgs e)
        {
            var userControl = (NonPatientMealOrderItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SequenceNo = userControl.SequenceNo;
                entity.FoodID = userControl.FoodID;
                entity.FoodName = userControl.FoodName;
                entity.Qty = userControl.Qty;
            }
        }

        #endregion
        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.NonPatientMealOrderNo);

            return _autoNumber.LastCompleteNumber;
        }
    }
}