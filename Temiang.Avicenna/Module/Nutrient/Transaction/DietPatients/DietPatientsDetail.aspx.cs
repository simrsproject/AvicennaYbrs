using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class DietPatientsDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List 
            UrlPageSearch = "#";
            UrlPageList = "DietPatientsList.aspx?uid=" + Request.QueryString["uid"];
            ProgramID = AppConstant.Program.DietPatients;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboFormOfFood, AppEnum.StandardReference.FormOfFood);

                DietPatientItems = null;
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdDietPatientItem, grdDietPatientItem);
            ajax.AddAjaxSetting(grdDietPatientItem, cboFormOfFood);
        }

        #endregion

        #region Toolbar Menu Event
        
        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new DietPatient());

            txtEffectiveStartDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtEffectiveStartTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");
            txtEffectiveEndDate.Clear();
            txtEffectiveEndTime.Text = "00:00";
            txtTransactionNo.Text = GetNewTransactionNo();
            txtRegistrationNo.Text = Request.QueryString["regno"];

            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            var par = new Paramedic();
            par.LoadByPrimaryKey(reg.ParamedicID);
            txtPhysicianName.Text = par.ParamedicName;

            var patient = new Patient();
            patient.LoadByPrimaryKey(reg.PatientID);
            txtMedicalNo.Text = patient.MedicalNo;
            txtPatientName.Text = patient.PatientName;
            txtGender.Text = patient.Sex;
            var std = new AppStandardReferenceItem();
            txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.SRSalutation) ? std.ItemName : string.Empty;
            txtPlaceDOB.Text = string.Format("{0}, {1}", patient.CityOfBirth, Convert.ToDateTime(patient.DateOfBirth).ToString("dd-MMM-yyyy"));
            txtAgeInYear.Text = Convert.ToString(reg.AgeInYear);
            txtAgeInMonth.Text = Convert.ToString(reg.AgeInMonth);
            txtAgeInDay.Text = Convert.ToString(reg.AgeInDay);
            
            string su = string.Empty;
            string sr = string.Empty;
            var unit = new ServiceUnit();
            if (unit.LoadByPrimaryKey(reg.ServiceUnitID))
                su = unit.ServiceUnitName;
            var room = new ServiceRoom();
            if (room.LoadByPrimaryKey(reg.RoomID))
                sr = room.RoomName;

            txtUnitRoomBed.Text = su + " (Room : " + sr + ", Bed : " + reg.BedID + ")";

            txtClassID.Text = reg.ChargeClassID;
            var c = new Class();
            c.LoadByPrimaryKey(txtClassID.Text);
            lblClassName.Text = c.ClassName;

            #region height, weight, body mask
            decimal h = 0, w = 0, bm = 0;

            //tinggi badan
            var phrcoll = new PatientHealthRecordLineCollection();
            phrcoll.Query.Where(phrcoll.Query.RegistrationNo == reg.RegistrationNo,
                            phrcoll.Query.QuestionID.In(AppSession.Parameter.QuestionIdForHeight), phrcoll.Query.QuestionAnswerNum > 0);
            phrcoll.Query.OrderBy(phrcoll.Query.LastUpdateDateTime.Descending);
            phrcoll.Query.es.Top = 1;
            phrcoll.LoadAll();
            foreach (var p in phrcoll)
            {
                h = p.QuestionAnswerNum ?? 0;
            }

            //berat badan
            phrcoll = new PatientHealthRecordLineCollection();
            phrcoll.Query.Where(phrcoll.Query.RegistrationNo == reg.RegistrationNo,
                        phrcoll.Query.QuestionID.In(AppSession.Parameter.QuestionIdForWeight), phrcoll.Query.QuestionAnswerNum > 0);
            phrcoll.Query.OrderBy(phrcoll.Query.LastUpdateDateTime.Descending);
            phrcoll.Query.es.Top = 1;
            phrcoll.LoadAll();
            foreach (var p in phrcoll)
            {
                w = p.QuestionAnswerNum ?? 0;
            }

            //body mask
            phrcoll = new PatientHealthRecordLineCollection();
            phrcoll.Query.Where(phrcoll.Query.RegistrationNo == reg.RegistrationNo,
                        phrcoll.Query.QuestionID.In(AppSession.Parameter.QuestionIdBodyMassIndex), phrcoll.Query.QuestionAnswerNum > 0);
            phrcoll.Query.OrderBy(phrcoll.Query.LastUpdateDateTime.Descending);
            phrcoll.Query.es.Top = 1;
            phrcoll.LoadAll();
            foreach (var p in phrcoll)
            {
                bm = p.QuestionAnswerNum ?? 0;
            }

            txtHeight.Value = Convert.ToDouble(h);
            txtWeight.Value = Convert.ToDouble(w);
            txtBodyMassIndex.Value = Convert.ToDouble(bm);

            #endregion

            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuEditClick()
        {
            cboFormOfFood.Enabled = !(DietPatientItems.Count > 0);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            txtTransactionNo.Text = GetNewTransactionNo();
            _autoNumber.Save();

            var entity = new DietPatient();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            
            if (DietPatientItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSA" && string.IsNullOrEmpty(txtDiagnose.Text))
            {
                args.MessageText = "Diagnose required.";
                args.IsCancel = true;
                return;
            }

            entity = new DietPatient();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new DietPatient();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                if (DietPatientItems.Count == 0)
                {
                    args.MessageText = AppConstant.Message.RecordDetailEmpty;
                    args.IsCancel = true;
                    return;
                }

                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSA" && string.IsNullOrEmpty(txtDiagnose.Text))
                {
                    args.MessageText = "Diagnose required.";
                    args.IsCancel = true;
                    return;
                }

                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
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
            auditLogFilter.TableName = "DietPatient";
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {

        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new DietPatient();
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
        }

        private void SetVoid(DietPatient entity, bool isVoid)
        {
            //header
            entity.IsVoid = isVoid;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            using (var trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuEdit()
        {
            return txtTransactionNo.Text != string.Empty;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemDietPatientItem(oldVal, newVal);

            txtEffectiveStartDate.Enabled = (newVal == AppEnum.DataMode.New);
            txtEffectiveStartTime.ReadOnly = (newVal != AppEnum.DataMode.New);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new DietPatient();
            if (parameters.Length > 0)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                    entity.LoadByPrimaryKey(Request.QueryString["id"]);
            }
            else
                entity.LoadByPrimaryKey(txtTransactionNo.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var dietPatient = (DietPatient)entity;
            txtTransactionNo.Text = dietPatient.TransactionNo;
            txtRegistrationNo.Text = dietPatient.RegistrationNo;
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.str.PatientID);
                txtMedicalNo.Text = pat.MedicalNo;
                txtPatientName.Text = pat.PatientName;
                txtGender.Text = pat.Sex;
                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;
                txtPlaceDOB.Text = string.Format("{0}, {1}", pat.CityOfBirth, Convert.ToDateTime(pat.DateOfBirth).ToString("dd-MMM-yyyy"));
                txtAgeInYear.Text = Convert.ToString(reg.AgeInYear);
                txtAgeInMonth.Text = Convert.ToString(reg.AgeInMonth);
                txtAgeInDay.Text = Convert.ToString(reg.AgeInDay);

                var par = new Paramedic();
                par.LoadByPrimaryKey(reg.ParamedicID);
                txtPhysicianName.Text = par.ParamedicName;

                string su = string.Empty;
                string sr = string.Empty;
                var unit = new ServiceUnit();
                if (unit.LoadByPrimaryKey(reg.ServiceUnitID))
                    su = unit.ServiceUnitName;
                var room = new ServiceRoom();
                if (room.LoadByPrimaryKey(reg.RoomID))
                    sr = room.RoomName;

                txtUnitRoomBed.Text = su + " (Room : " + sr + ", Bed : " + reg.BedID + ")";

                txtClassID.Text = reg.ClassID;
                var c = new Class();
                if (c.LoadByPrimaryKey(txtClassID.Text))
                    lblClassName.Text = c.ClassName;
            }

            if (dietPatient.EffectiveStartDate.HasValue)
            {
                txtEffectiveStartDate.SelectedDate = dietPatient.EffectiveStartDate.Value.Date;
                txtEffectiveStartTime.Text = dietPatient.EffectiveStartTime;
            }

            if (dietPatient.EffectiveEndDate.HasValue)
            {
                txtEffectiveEndDate.SelectedDate = dietPatient.EffectiveEndDate.Value.Date;
                txtEffectiveEndTime.Text = dietPatient.EffectiveEndTime;
            }

            txtDiagnose.Text = dietPatient.Diagnose;
            txtHeight.Value = Convert.ToDouble(dietPatient.Height);
            txtWeight.Value = Convert.ToDouble(dietPatient.Weight);
            txtBodyMassIndex.Value = Convert.ToDouble(dietPatient.BodyMassIndex);
            
            txtNotes.Text = dietPatient.Notes;
            cboFormOfFood.SelectedValue = dietPatient.FormOfFood;
            chkIsSpecialCondition.Checked = dietPatient.IsSpecialCondition ?? false;
            txtMuac.Value = Convert.ToDouble(dietPatient.Muac);
            txtUlna.Value = Convert.ToDouble(dietPatient.Ulna);
            
            ViewState["IsVoid"] = dietPatient.IsVoid ?? false;

            //Display Data Detail
            PopulateDietPatientItemGrid();

            var x = DietComplicationPatients;
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(DietPatient entity)
        {
            entity.TransactionNo = txtTransactionNo.Text;
            entity.RegistrationNo = txtRegistrationNo.Text;
            entity.EffectiveStartDate = txtEffectiveStartDate.SelectedDate;
            entity.EffectiveStartTime = txtEffectiveStartTime.TextWithLiterals;

            if (!txtEffectiveEndDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                entity.EffectiveEndDate = txtEffectiveEndDate.SelectedDate;
                entity.EffectiveEndTime = txtEffectiveEndTime.TextWithLiterals;
            }
            else
            {
                entity.EffectiveEndDate = null;
                entity.EffectiveEndTime = null;
            }

            entity.Height = Convert.ToDecimal(txtHeight.Value);
            entity.Weight = Convert.ToDecimal(txtWeight.Value);
            entity.BodyMassIndex = Convert.ToDecimal(txtBodyMassIndex.Value);
            entity.Diagnose = txtDiagnose.Text;
            entity.Notes = txtNotes.Text;
            entity.FormOfFood = cboFormOfFood.SelectedValue;
            entity.IsSpecialCondition = chkIsSpecialCondition.Checked;
            entity.Muac = Convert.ToDecimal(txtMuac.Value);
            entity.Ulna = Convert.ToDecimal(txtUlna.Value);
            entity.IsVoid = false;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            //Last Update Status Detail
            foreach (var item in DietPatientItems)
            {
                item.TransactionNo = entity.TransactionNo;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(DietPatient entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                DietPatientItems.Save();
                DietComplicationPatients.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new DietPatientQuery();
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

            var entity = new DietPatient();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer(), AppEnum.AutoNumber.DietPatientNo);
            return _autoNumber.LastCompleteNumber;
        }

        #endregion

        #region Record Detail Method Function DietPatientItem

        private DietPatientItemCollection DietPatientItems
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collDietPatientItem" + Request.UserHostName];
                    if (obj != null)
                        return ((DietPatientItemCollection)(obj));
                }

                var coll = new DietPatientItemCollection();
                var query = new DietPatientItemQuery("a");
                var hdQ = new DietPatientQuery("aa");
                var dietQ = new DietQuery("b");
                var dietMenuQ = new DietMenuQuery("bb");
                var menuQ = new MenuQuery("c");

                query.Select
                    (
                        query,
                        dietQ.DietName.As("refToDiet_DietName"),
                        menuQ.MenuName.As("refToMenu_MenuName")
                    );
                query.InnerJoin(hdQ).On(query.TransactionNo == hdQ.TransactionNo);
                query.InnerJoin(dietQ).On(query.DietID == dietQ.DietID);
                query.InnerJoin(dietMenuQ).On(query.DietID == dietMenuQ.DietID && dietMenuQ.FormOfFood == hdQ.FormOfFood);
                query.InnerJoin(menuQ).On(dietMenuQ.MenuID == menuQ.MenuID);
                query.Where(query.TransactionNo == txtTransactionNo.Text);

                query.OrderBy(query.DietID.Ascending);
                coll.Load(query);

                Session["collDietPatientItem" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collDietPatientItem" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemDietPatientItem(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdDietPatientItem.Columns[0].Visible = isVisible;
            grdDietPatientItem.Columns[grdDietPatientItem.Columns.Count - 1].Visible = isVisible;
            grdDietPatientItem.Columns[grdDietPatientItem.Columns.Count - 2].Visible = isVisible;

            grdDietPatientItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            if (oldVal != AppEnum.DataMode.Read)
                DietPatientItems = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdDietPatientItem.Rebind();
        }

        private void PopulateDietPatientItemGrid()
        {
            //Display Data Detail
            DietPatientItems = null; //Reset Record Detail
            grdDietPatientItem.DataSource = DietPatientItems; //Requery
            grdDietPatientItem.MasterTableView.IsItemInserted = false;
            grdDietPatientItem.MasterTableView.ClearEditItems();
            grdDietPatientItem.DataBind();
        }

        protected void grdDietPatientItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDietPatientItem.DataSource = DietPatientItems;
        }

        protected void grdDietPatientItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            var dietId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][DietPatientItemMetadata.ColumnNames.DietID]);
            var entity = FindDietPatientItem(dietId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdDietPatientItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            var dietId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][DietPatientItemMetadata.ColumnNames.DietID]);
            var entity = FindDietPatientItem(dietId);
            if (entity != null)
                entity.MarkAsDeleted();

            DietComplicationPatientCollection dietCompColl = DietComplicationPatients;
            foreach (DietComplicationPatient dietComp in dietCompColl.Where(dietComp => dietComp.DietID.Equals(dietId)))
            {
                dietComp.MarkAsDeleted();
            }

            cboFormOfFood.Enabled = DietPatientItems.Count == 0;
        }

        protected void grdDietPatientItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = DietPatientItems.AddNew();
            SetEntityValue(entity, e);

            //grid not close first
            //if (AppSession.Parameter.HealthcareInitialAppsVersion != "RSSA")
            //    e.Canceled = true;

            grdDietPatientItem.Rebind();
        }

        private DietPatientItem FindDietPatientItem(String dietId)
        {
            return DietPatientItems.FirstOrDefault(rec => rec.DietID.Equals(dietId));
        }

        private void SetEntityValue(DietPatientItem entity, GridCommandEventArgs e)
        {
            var userControl = (DietPatientsItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.TransactionNo = txtTransactionNo.Text;
                entity.DietID = userControl.DietID;
                entity.DietName = userControl.DietName;
                entity.Calorie = userControl.Calorie;
                entity.Protein = userControl.Protein;
                entity.Fat = userControl.Fat;
                entity.Carbohydrate = userControl.Carbohydrate;
                entity.Salt = userControl.Salt;
                entity.Fiber = userControl.Fiber;
                entity.ExtraQty = userControl.ExtraQty;
                entity.LiquidTime = userControl.LiquidTime;
                entity.Notes = userControl.Notes;

                var d = new DietMenu();
                if (d.LoadByPrimaryKey(entity.DietID, cboFormOfFood.SelectedValue))
                {
                    var m = new Menu();
                    if (m.LoadByPrimaryKey(d.MenuID))
                        entity.MenuName = m.MenuName;
                }
            }
        }

        #endregion

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
        }

        private DietComplicationPatientCollection DietComplicationPatients
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collDietComplicationPatient" + Request.UserHostName];
                    if (obj != null)
                        return ((DietComplicationPatientCollection)(obj));
                }
                var coll = new DietComplicationPatientCollection();

                var query = new DietComplicationPatientQuery("a");
                var d = new DietQuery("b");
                query.Select
                    (
                        query,
                        d.DietName.As("refToDiet_DietName")
                    );
                query.InnerJoin(d).On(query.DietComplicationID == d.DietID);
                query.Where(query.TransactionNo == txtTransactionNo.Text);

                coll.Load(query);

                Session["collDietComplicationPatient" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collDietComplicationPatient" + Request.UserHostName] = value;
            }
        }

        protected void txtHeight_TextChanged(object sender, EventArgs e)
        {
            if (txtHeight.Value > 0)
            {
                txtBodyMassIndex.Value = txtWeight.Value / ((txtHeight.Value / 100) * (txtHeight.Value / 100));
            }
        }
    }
}
