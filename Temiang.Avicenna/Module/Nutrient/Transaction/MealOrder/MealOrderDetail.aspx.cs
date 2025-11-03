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
    public partial class MealOrderDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List 
            UrlPageSearch = "#";
            UrlPageList = "MealOrderList.aspx?unitid=" + Request.QueryString["unitid"] + "&add=" + Request.QueryString["add"];
            
            switch (Request.QueryString["add"])
            {
                case "0":
                    ProgramID = AppConstant.Program.MealOrder;
                    break;
                case "1":
                    ProgramID = AppConstant.Program.AdditionalMealOrder;
                    break;
            }

            if (!IsPostBack)
            {
                var dietColl = new DietCollection();
                dietColl.LoadAll();
                cboDietID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var item in dietColl)
                {
                    cboDietID.Items.Add(new RadComboBoxItem(item.DietName, item.DietID));
                }

                StandardReference.Initialize(cboSRMealSet, AppEnum.StandardReference.MealSet, true);

                MealOrderItems = null;
            }

        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItem, grdItem);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            //printJobParameters.AddNew("TransactionNo", txtTransactionNo.Text);
        }

        protected override void OnMenuNewClick()
        {
        }

        protected override void OnMenuEditClick()
        {
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new MealOrder();
            if (entity.LoadByPrimaryKey(txtOrderNo.Text))
            {
                if (!IsApprovedOrVoid(entity, args))
                    return;
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {

        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new MealOrder();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new MealOrder();
            if (entity.LoadByPrimaryKey(txtOrderNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
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
            auditLogFilter.PrimaryKeyData = string.Format("OrderNo='{0}'", txtOrderNo.Text.Trim());
            auditLogFilter.TableName = "MealOrder";
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new MealOrder();
            if (!entity.LoadByPrimaryKey(txtOrderNo.Text))
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

            var msg = string.Empty;
            if (Request.QueryString["add"] == "0" && AppSession.Parameter.IsMealOrderValidationForIncompleteItem)
                msg = MsgEmptyFood();

            if (msg.Length > 0)
            {
                args.MessageText = "Meal order incomplete for set: " + msg + ".";
                args.IsCancel = true;
                return;
            }

            SetApproved(entity, true);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new MealOrder();
            if (!entity.LoadByPrimaryKey(txtOrderNo.Text))
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
            if (AppSession.Parameter.IsUsingMealOrderVerification)
            {
                if (entity.IsVerified ?? false)
                {
                    args.MessageText = "This data has been verified.";
                    args.IsCancel = true;
                    return;
                }
            }

            SetApproved(entity, false);
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new MealOrder();
            if (!entity.LoadByPrimaryKey(txtOrderNo.Text))
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

            SetVoid(entity, true);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
        }

        private bool IsApprovedOrVoid(MealOrder entity, ValidateArgs args)
        {
            if (entity.IsApproved.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            if (entity.IsVoid.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }
            return true;
        }

        private void SetApproved(MealOrder entity, bool isApproved)
        {
            using (var trans = new esTransactionScope())
            {
                //header
                entity.IsApproved = isApproved;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                entity.Save();

                #region insert to meal order plan & liquid
                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSA")
                {
                    if (isApproved)
                    {
                        #region order plan

                        var dp = new DietPatient();
                        dp.LoadByPrimaryKey(entity.DietPatientNo);
                        var formOfFood = dp.FormOfFood;

                        var dpi = new DietPatientItem();
                        dpi.LoadByPrimaryKey(entity.DietPatientNo, entity.DietID);

                        var menuVersionId = string.Empty;
                        var menuSeqNo = string.Empty;

                        var menuSettingQ = new MenuSettingQuery();
                        menuSettingQ.es.Top = 1;
                        menuSettingQ.Where(menuSettingQ.StartingDate <= entity.EffectiveDate.Value.Date.AddDays(1), menuSettingQ.IsExtra == false);
                        menuSettingQ.OrderBy(menuSettingQ.StartingDate.Descending);

                        var menuSetting = new MenuSetting();
                        if (menuSetting.Load(menuSettingQ))
                        {
                            var menuVersion = new MenuVersion();
                            menuVersion.LoadByPrimaryKey(menuSetting.VersionID);
                            var cycle = menuVersion.Cycle ?? 1;

                            int diff = (entity.EffectiveDate.Value.Date.AddDays(1).Subtract(menuSetting.StartingDate.Value.Date)).Days;
                            int i = (diff % cycle) + Convert.ToInt32(menuSetting.SeqNo);

                            if (i > cycle)
                                i = i - cycle;

                            var seqNo = i < 10 ? "0" + string.Format("{0}", i) : string.Format("{0}", i);

                            menuVersionId = menuSetting.VersionID;
                            menuSeqNo = seqNo;
                        }

                        var menuItemQ = new MenuItemQuery();
                        menuItemQ.es.Top = 1;
                        menuItemQ.Where(menuItemQ.MenuID == entity.MenuID,
                                        menuItemQ.VersionID == menuVersionId, menuItemQ.SeqNo == menuSeqNo,
                                        menuItemQ.ClassID == entity.ClassID, menuItemQ.IsActive == true);
                        menuItemQ.OrderBy(menuItemQ.LastUpdateDateTime.Descending);

                        var menuItem = new MenuItem();
                        var menuItemId = menuItem.Load(menuItemQ) ? menuItem.MenuItemID : string.Empty;

                        if (!string.IsNullOrEmpty(menuItemId))
                        {
                            var planColl = new MealOrderItemPlanCollection();

                            var mealSetColl = new AppStandardReferenceItemCollection();
                            mealSetColl.Query.Where(
                                mealSetColl.Query.StandardReferenceID == AppEnum.StandardReference.MealSet,
                                mealSetColl.Query.IsActive == true);
                            mealSetColl.LoadAll();

                            var menuItemFoodColl = new MenuItemFoodCollection();
                            var menuItemFoodQ = new MenuItemFoodQuery("a");
                            var foodQ = new FoodQuery("b");
                            menuItemFoodQ.InnerJoin(foodQ).On(menuItemFoodQ.FoodID == foodQ.FoodID);
                            menuItemFoodQ.Where(menuItemFoodQ.MenuItemID == menuItemId,
                                                menuItemFoodQ.IsStandard == true);
                            if (formOfFood == "1" || formOfFood == "7" || formOfFood == "9")
                            {
                                menuItemFoodQ.Where(
                                    menuItemFoodQ.FoodID.NotIn(AppSession.Parameter.LiquidFoodId,
                                                               AppSession.Parameter.BlenderizedFoodId),
                                    foodQ.SRFoodGroup1 == "VII");
                            }

                            menuItemFoodColl.Load(menuItemFoodQ);

                            foreach (var menuItemFood in menuItemFoodColl)
                            {
                                var plan = planColl.AddNew();

                                plan.OrderNo = entity.OrderNo;
                                plan.OrderToDate = txtOrderDate.SelectedDate.Value.Date.AddDays(1);
                                plan.SRMealSet = menuItemFood.SRMealSet;
                                plan.FoodID = menuItemFood.FoodID;
                                plan.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                plan.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            }

                            planColl.Save();
                        }
                        #endregion

                        #region order liquid
                        if (formOfFood == "1" || formOfFood == "7" || formOfFood == "2" || formOfFood == "3" || formOfFood == "9")
                        {
                            var dlQ = new DietLiquidPatientQuery();
                            dlQ.es.Top = 1;
                            dlQ.Where(dlQ.RegistrationNo == txtRegistrationNo.Text,
                                      dlQ.EffectiveStartDate <= txtOrderDate.SelectedDate.Value.Date,
                                      dlQ.IsVoid == false);
                            dlQ.OrderBy(dlQ.EffectiveStartDate.Descending, dlQ.EffectiveStartTime.Descending);

                            var dl = new DietLiquidPatient();
                            if (dl.Load(dlQ))
                            {
                                var lqColl = new MealOrderItemLiquidCollection();

                                var dlts = new DietLiquidPatientTimeCollection();
                                dlts.Query.Where(dlts.Query.TransactionNo == dl.TransactionNo);
                                dlts.LoadAll();

                                foreach (var dlt in dlts)
                                {
                                    var lq = lqColl.AddNew();
                                    lq.OrderNo = entity.OrderNo;
                                    lq.MealTime = dlt.DietTime;
                                    lq.FoodID = dlt.FoodID;
                                    lq.DietLiquidTransNo = dlt.TransactionNo;
                                    lq.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                    lq.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                }

                                lqColl.Save();
                            }
                        }
                        else if (dpi.ExtraQty.ToInt() > 0)
                        {
                            var dlQ = new DietLiquidPatientQuery();
                            dlQ.es.Top = 1;
                            dlQ.Where(dlQ.RegistrationNo == txtRegistrationNo.Text,
                                      dlQ.EffectiveStartDate <= txtOrderDate.SelectedDate.Value.Date,
                                      dlQ.IsVoid == false);
                            dlQ.OrderBy(dlQ.EffectiveStartDate.Descending, dlQ.EffectiveStartTime.Descending);

                            var dl = new DietLiquidPatient();
                            if (dl.Load(dlQ))
                            {
                                var lqColl = new MealOrderItemLiquidCollection();

                                var dlts = new DietLiquidPatientTimeCollection();
                                dlts.Query.Where(dlts.Query.TransactionNo == dl.TransactionNo, dlts.Query.FoodID != string.Empty);
                                dlts.LoadAll();

                                foreach (var dlt in dlts)
                                {
                                    var lq = lqColl.AddNew();
                                    lq.OrderNo = entity.OrderNo;
                                    lq.MealTime = dlt.DietTime;
                                    lq.FoodID = dlt.FoodID;
                                    lq.DietLiquidTransNo = dlt.TransactionNo;
                                    lq.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                    lq.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                }

                                lqColl.Save();
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        var planColl = new MealOrderItemPlanCollection();
                        planColl.Query.Where(planColl.Query.OrderNo == entity.OrderNo);
                        planColl.LoadAll();
                        planColl.MarkAllAsDeleted();
                        planColl.Save();

                        var lqColl = new MealOrderItemLiquidCollection();
                        lqColl.Query.Where(lqColl.Query.OrderNo == entity.OrderNo);
                        lqColl.LoadAll();
                        lqColl.MarkAllAsDeleted();
                        lqColl.Save();
                    }
                }
                else
                {
                    if (isApproved)
                    {
                        var dp = new DietPatient();
                        dp.LoadByPrimaryKey(entity.DietPatientNo);
                        var formOfFood = dp.FormOfFood;

                        var dpi = new DietPatientItem();
                        dpi.LoadByPrimaryKey(entity.DietPatientNo, entity.DietID);
                        
                        #region order liquid
                        if (formOfFood == "1" || formOfFood == "7")
                        {
                            var dlQ = new DietLiquidPatientQuery();
                            dlQ.es.Top = 1;
                            dlQ.Where(dlQ.RegistrationNo == txtRegistrationNo.Text,
                                      dlQ.EffectiveStartDate <= txtOrderDate.SelectedDate.Value.Date,
                                      dlQ.IsVoid == false);
                            dlQ.OrderBy(dlQ.EffectiveStartDate.Descending, dlQ.EffectiveStartTime.Descending);

                            var dl = new DietLiquidPatient();
                            if (dl.Load(dlQ))
                            {
                                var lqColl = new MealOrderItemLiquidCollection();

                                var dlts = new DietLiquidPatientTimeCollection();
                                dlts.Query.Where(dlts.Query.TransactionNo == dl.TransactionNo);
                                dlts.LoadAll();

                                foreach (var dlt in dlts)
                                {
                                    var lq = lqColl.AddNew();
                                    lq.OrderNo = entity.OrderNo;
                                    lq.MealTime = dlt.DietTime;
                                    lq.FoodID = dlt.FoodID;
                                    lq.DietLiquidTransNo = dlt.TransactionNo;
                                    lq.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                    lq.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                }

                                lqColl.Save();
                            }
                        }
                        else if (dpi.ExtraQty.ToInt() > 0)
                        {
                            var dlQ = new DietLiquidPatientQuery();
                            dlQ.es.Top = 1;
                            dlQ.Where(dlQ.RegistrationNo == txtRegistrationNo.Text,
                                      dlQ.EffectiveStartDate <= txtOrderDate.SelectedDate.Value.Date,
                                      dlQ.IsVoid == false);
                            dlQ.OrderBy(dlQ.EffectiveStartDate.Descending, dlQ.EffectiveStartTime.Descending);

                            var dl = new DietLiquidPatient();
                            if (dl.Load(dlQ))
                            {
                                var lqColl = new MealOrderItemLiquidCollection();

                                var dlts = new DietLiquidPatientTimeCollection();
                                dlts.Query.Where(dlts.Query.TransactionNo == dl.TransactionNo, dlts.Query.FoodID != string.Empty);
                                dlts.LoadAll();

                                foreach (var dlt in dlts)
                                {
                                    var lq = lqColl.AddNew();
                                    lq.OrderNo = entity.OrderNo;
                                    lq.MealTime = dlt.DietTime;
                                    lq.FoodID = dlt.FoodID;
                                    lq.DietLiquidTransNo = dlt.TransactionNo;
                                    lq.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                    lq.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                }

                                lqColl.Save();
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        var lqColl = new MealOrderItemLiquidCollection();
                        lqColl.Query.Where(lqColl.Query.OrderNo == entity.OrderNo);
                        lqColl.LoadAll();
                        lqColl.MarkAllAsDeleted();
                        lqColl.Save();
                    }
                }

                #endregion

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void SetVoid(MealOrder entity, bool isVoid)
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
            return txtOrderNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !(bool)ViewState["IsApproved"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemMealOrder(oldVal, newVal);
            cboSRMealSet.Enabled = newVal == AppEnum.DataMode.Read;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new MealOrder();
            if (parameters.Length > 0)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                    entity.LoadByPrimaryKey(Request.QueryString["id"]);
            }
            else
                entity.LoadByPrimaryKey(txtOrderNo.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var mealOrder = (MealOrder)entity;
            txtOrderNo.Text = mealOrder.OrderNo;
            txtOrderDate.SelectedDate = mealOrder.OrderDate;
            txtEffectiveDate.SelectedDate = mealOrder.EffectiveDate;
            txtRegistrationNo.Text = mealOrder.RegistrationNo;

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
            }

            string su = string.Empty;
            string sr = string.Empty;
            var unit = new ServiceUnit();
            if (unit.LoadByPrimaryKey(mealOrder.ServiceUnitID))
                su = unit.ServiceUnitName;
            var bed = new Bed();
            if (bed.LoadByPrimaryKey(mealOrder.BedID))
            {
                var room = new ServiceRoom();
                room.LoadByPrimaryKey(bed.RoomID);
                sr = room.RoomName;
            }
            txtUnitRoomBed.Text = su;//su + " (Room : " + sr + ", Bed : " + mealOrder.BedID + ")";
            txtRoomBed.Text = sr + " / " + mealOrder.BedID;
            txtUnitID.Text = mealOrder.ServiceUnitID;
            txtBedID.Text = mealOrder.BedID;
            txtClassID.Text = mealOrder.ClassID;
            var c = new Class();
            if (c.LoadByPrimaryKey(txtClassID.Text))
                lblClassName.Text = c.ClassName;

            txtDietPatientNo.Text = mealOrder.DietPatientNo;
            cboDietID.SelectedValue = mealOrder.DietID;
            txtMenuID.Text = mealOrder.MenuID;
            txtMenuItemID.Text = mealOrder.MenuItemID;

            //-- diet patient --
            var dietPatient = new DietPatient();
            dietPatient.LoadByPrimaryKey(txtDietPatientNo.Text);

            GetDietInformation(cboDietID.SelectedValue, txtMenuID.Text);
            txtDiagnose.Text = dietPatient.Diagnose;
            txtHeight.Value = Convert.ToDouble(dietPatient.Height);
            txtWeight.Value = Convert.ToDouble(dietPatient.Weight);
            txtBodyMassIndex.Value = Convert.ToDouble(dietPatient.BodyMassIndex);
            txtNotes.Text = dietPatient.Notes;
            txtMuac.Value = Convert.ToDouble(dietPatient.Muac);
            txtUlna.Value = Convert.ToDouble(dietPatient.Ulna);

            var dietPatientItem = new DietPatientItem();
            dietPatientItem.LoadByPrimaryKey(txtDietPatientNo.Text, cboDietID.SelectedValue);

            cboFormOfFood.SelectedValue = dietPatient.FormOfFood;
            txtCalorie.Value = Convert.ToDouble(dietPatientItem.Calorie);
            txtProtein.Value = Convert.ToDouble(dietPatientItem.Protein);
            txtFat.Value = Convert.ToDouble(dietPatientItem.Fat);
            txtCarbohydrate.Value = Convert.ToDouble(dietPatientItem.Carbohydrate);
            txtSalt.Value = Convert.ToDouble(dietPatientItem.Salt);
            txtFiber.Value = Convert.ToDouble(dietPatientItem.Fiber);
            txtDietNote.Text = dietPatient.Notes;
            //-- end diet patient --

            //-- allergies
            string alergies = string.Empty;
            var pacoll = new PatientAllergyCollection();
            pacoll.Query.Where(pacoll.Query.AllergyGroup == AppSession.Parameter.FoodAllergenGroupID, pacoll.Query.PatientID == reg.str.PatientID);
            pacoll.Query.OrderBy(pacoll.Query.AllergenName.Ascending);
            pacoll.LoadAll();
            foreach (var pa in pacoll)
            {
                var descAndReaction = pa.DescAndReaction.Replace("{", "(").Replace("}", ")");
                if (alergies == string.Empty)
                    alergies += string.Format(@"{0}", descAndReaction);
                else
                    alergies += string.Format(@"{0} {1}", ", ", descAndReaction);
            }

            txtAllergies.Text = alergies;
            //-- end

            chkIsFastingMornig.Checked = false;
            chkIsFastingDay.Checked = false;
            chkIsFastingNight.Checked = false;

            int ttl = mealOrder.FastingTime == null ? 0 : mealOrder.FastingTime.Length;
            int idx = 0;
            while (idx < ttl)
            {
                string parseChar = mealOrder.FastingTime.Substring(idx, 1);
                if (parseChar != ";")
                {
                    switch (parseChar)
                    {
                        case "1":
                            chkIsFastingMornig.Checked = true;
                            break;
                        case "2":
                            chkIsFastingDay.Checked = true;
                            break;
                        case "3":
                            chkIsFastingNight.Checked = true;
                            break;
                    }
                }
                idx += 1;
            }

            if (string.IsNullOrEmpty(txtSRMealSet.Text))
                cboSRMealSet.SelectedValue = MealSet.Breakfast;

            ViewState["IsVoid"] = mealOrder.IsVoid ?? false;
            ViewState["IsApproved"] = mealOrder.IsApproved ?? false;

            //Display Data Detail
            PopulateMealOrderItemGrid();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(MealOrder entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtOrderNo.Text = GetNewTransactionNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }

            entity.OrderNo = txtOrderNo.Text;
            entity.OrderDate = txtOrderDate.SelectedDate;
            entity.EffectiveDate = txtEffectiveDate.SelectedDate;
            entity.RegistrationNo = txtRegistrationNo.Text;
            entity.ServiceUnitID = txtUnitID.Text;
            entity.ClassID = txtClassID.Text;
            entity.BedID = txtBedID.Text;
            entity.DietPatientNo = txtDietPatientNo.Text;
            entity.DietID = cboDietID.SelectedValue;
            entity.MenuID = txtMenuID.Text;
            entity.MenuItemID = txtMenuItemID.Text;

            string charToParse = string.Empty;
            if (chkIsFastingMornig.Checked)
                charToParse += "1;";
            if (chkIsFastingDay.Checked)
                charToParse += "2;";
            if (chkIsFastingNight.Checked)
                charToParse += "3;";
            entity.FastingTime = charToParse;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            //Last Update Status Detail
            foreach (var item in MealOrderItems)
            {
                item.OrderNo = entity.OrderNo;
                item.SRMealSet = cboSRMealSet.SelectedValue;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(MealOrder entity)
        {
            using (var trans = new esTransactionScope())
            {
                //autonumber has been saved on SetEntity
                //if (DataModeCurrent == DataMode.New)
                //    _autoNumber.Save();

                entity.Save();
                MealOrderItems.Save();

                if (AppSession.Parameter.IsUseStandardMealMenuForAllClass)
                {
                    var mealset = new AppStandardReferenceItemCollection();
                    mealset.Query.Where(
                        mealset.Query.StandardReferenceID == AppEnum.StandardReference.MealSet.ToString(),
                        mealset.Query.ItemID != cboSRMealSet.SelectedValue);
                    mealset.LoadAll();

                    foreach (var ms in mealset)
                    {
                        var moi = new MealOrderItemCollection();
                        moi.Query.Where(moi.Query.OrderNo == entity.OrderNo, moi.Query.SRMealSet == ms.ItemID, moi.Query.IsOptional == true);
                        moi.LoadAll();
                        if (moi.Count == 0)
                        {
                            foreach (var x in MealOrderItems)
                            {
                                if (x.IsOptional == true)
                                {
                                    var dt = new MealOrderItem();
                                    if (!dt.LoadByPrimaryKey(entity.OrderNo, ms.ItemID, x.FoodID))
                                    {
                                        dt.AddNew();
                                        dt.OrderNo = entity.OrderNo;
                                        dt.SRMealSet = ms.ItemID;
                                        dt.FoodID = x.FoodID;
                                        dt.IsOptional = x.IsOptional;
                                        dt.LastUpdateDateTime = DateTime.Now;
                                        dt.LastUpdateByUserID = AppSession.UserLogin.UserID;

                                        dt.Save();
                                    }
                                }
                            }
                        }
                    }
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new MealOrderQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.OrderNo > txtOrderNo.Text, que.OrderDate.Date() == txtOrderDate.SelectedDate.Value.Date, que.ServiceUnitID == txtUnitID.Text, que.IsOpr == false);
                que.OrderBy(que.OrderNo.Ascending);
            }
            else
            {
                que.Where(que.OrderNo < txtOrderNo.Text, que.OrderDate.Date() == txtOrderDate.SelectedDate.Value.Date, que.ServiceUnitID == txtUnitID.Text, que.IsOpr == false);
                que.OrderBy(que.OrderNo.Descending);
            }

            var entity = new MealOrder();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer(), AppEnum.AutoNumber.MealOrderNo);
            return _autoNumber.LastCompleteNumber;
        }

        #endregion

        #region Record Detail Method Function MealOrderItem

        private MealOrderItemCollection MealOrderItems
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collMealOrderItem" + Request.UserHostName];
                    if (obj != null)
                        return ((MealOrderItemCollection)(obj));
                }

                var coll = new MealOrderItemCollection();
                var query = new MealOrderItemQuery("a");
                var foodQ = new FoodQuery("b");
                var stdQ = new AppStandardReferenceItemQuery("c");

                query.Select
                    (
                        query,
                        foodQ.FoodName.As("refToFood_FoodName"),
                        foodQ.SRFoodGroup1.As("refToFood_FoodGroupID"),
                        stdQ.ItemName.As("refToFood_FoodGroupName")
                        );

                query.InnerJoin(foodQ).On(query.FoodID == foodQ.FoodID);
                query.InnerJoin(stdQ).On(foodQ.SRFoodGroup1 == stdQ.ItemID &
                                         stdQ.StandardReferenceID == AppEnum.StandardReference.FoodGroup1);
                query.Where(query.OrderNo == txtOrderNo.Text, query.SRMealSet == cboSRMealSet.SelectedValue);

                query.OrderBy(foodQ.SRFoodGroup1.Ascending, query.FoodID.Ascending);
                coll.Load(query);

                Session["collMealOrderItem" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collMealOrderItem" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemMealOrder(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);

            var mos = new MealOrderItemSetting();
            mos.LoadByPrimaryKey(txtOrderNo.Text, cboSRMealSet.SelectedValue);
            if (mos.IsOptional == true)
            {
                grdItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
                grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;
            }
            else
            {
                grdItem.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                grdItem.Columns[grdItem.Columns.Count - 1].Visible = false;
            }

            if (oldVal != AppEnum.DataMode.Read)
                MealOrderItems = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdItem.Rebind();
        }

        private void PopulateMealOrderItemGrid()
        {
            //Display Data Detail
            MealOrderItems = null; //Reset Record Detail
            grdItem.DataSource = MealOrderItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = MealOrderItems;
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            var foodId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][MealOrderItemMetadata.ColumnNames.FoodID]);
            var entity = FindMealOrderItem(foodId, Request.QueryString["add"] == "1");
            if (entity != null)
                entity.MarkAsDeleted();
        }

        private MealOrderItem FindMealOrderItem(String foodId, bool isAdditional)
        {
            if (!isAdditional)
                return MealOrderItems.FirstOrDefault(rec => rec.FoodID.Equals(foodId) && rec.IsOptional.Equals(true));
            return MealOrderItems.FirstOrDefault(rec => rec.FoodID.Equals(foodId));
        }

        #endregion

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
        }

        protected void cboDietID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new DietQuery("a");
            query.Where(query.Or(query.DietID == e.Text, query.DietName.Like(searchTextContain)));
            query.Select(query.DietID, query.DietName);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboDietID.DataSource = dtb;
            cboDietID.DataBind();
        }

        protected void cboDietID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["DietName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["DietID"].ToString();
        }

        private void GetDietInformation(string dietId, string menuId)
        {
            var diet = new Diet();
            if (diet.LoadByPrimaryKey(dietId))
            {
                var menu = new Menu();
                menu.LoadByPrimaryKey(menuId);
                txtMenu.Text = menu.MenuName;
                txtCalorieInterval.Value = Convert.ToDouble(diet.CalorieInterval);
                txtCalorieMin.Value = Convert.ToDouble(diet.CalorieMin);
                txtCalorieMax.Value = Convert.ToDouble(diet.CalorieMax);
                txtProteinInterval.Value = Convert.ToDouble(diet.ProteinInterval);
                txtProteinMin.Value = Convert.ToDouble(diet.ProteinMin);
                txtProteinMax.Value = Convert.ToDouble(diet.ProteinMax);
                txtFatInterval.Value = Convert.ToDouble(diet.FatInterval);
                txtFatMin.Value = Convert.ToDouble(diet.FatMin);
                txtFatMax.Value = Convert.ToDouble(diet.FatMax);
                txtCarbohydrateInterval.Value = Convert.ToDouble(diet.CarbohydrateInterval);
                txtCarbohydrateMin.Value = Convert.ToDouble(diet.CarbohydrateMin);
                txtCarbohydrateMax.Value = Convert.ToDouble(diet.CarbohydrateMax);
                txtSaltInterval.Value = Convert.ToDouble(diet.SaltInterval);
                txtSaltMin.Value = Convert.ToDouble(diet.SaltMin);
                txtSaltMax.Value = Convert.ToDouble(diet.SaltMax);
                txtFiberInterval.Value = Convert.ToDouble(diet.FiberInterval);
                txtFiberMin.Value = Convert.ToDouble(diet.FiberMin);
                txtFiberMax.Value = Convert.ToDouble(diet.FiberMax);

                ComboBox.PopulateFormOfFood(cboFormOfFood, dietId);
            }
            else
            {
                txtMenu.Text = string.Empty;
                txtCalorieInterval.Value = 0;
                txtCalorieMin.Value = 0;
                txtCalorieMax.Value = 0;
                txtProteinInterval.Value = 0;
                txtProteinMin.Value = 0;
                txtProteinMax.Value = 0;
                txtFatInterval.Value = 0;
                txtFatMin.Value = 0;
                txtFatMax.Value = 0;
                txtCarbohydrateInterval.Value = 0;
                txtCarbohydrateMin.Value = 0;
                txtCarbohydrateMax.Value = 0;
                txtSaltInterval.Value = 0;
                txtSaltMin.Value = 0;
                txtSaltMax.Value = 0;
                txtFiberInterval.Value = 0;
                txtFiberMin.Value = 0;
                txtFiberMax.Value = 0;
                txtCalorie.Value = 0;
                txtProtein.Value = 0;
                txtFat.Value = 0;
                txtCarbohydrate.Value = 0;
                txtSalt.Value = 0;
                txtFiber.Value = 0;
            }
        }

        protected void cboSRMealSet_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txtSRMealSet.Text = cboSRMealSet.SelectedValue;
            MealOrderItems = null;
            grdItem.Rebind();
        }

        private string MsgEmptyFood()
        {
            var retValue = string.Empty;
            var query = new MealOrderQuery("a");
            query.Select
                (
                    query.OrderNo,
                    @"<ISNULL((SELECT TOP 1 f.FoodName 
                            FROM MealOrderItem moi 
                            INNER JOIN Food f ON f.FoodID = moi.FoodID 
                                AND moi.SRMealSet = '01' 
                            INNER JOIN (SELECT asri.ItemID, asri.ReferenceID FROM AppStandardReferenceItem AS asri WHERE asri.StandardReferenceID = 'FoodGroup1') std ON std.ItemID = f.SRFoodGroup1
                            WHERE moi.OrderNo = a.OrderNo AND std.ReferenceID = 'MAIN'), '-') AS Breakfast>",
                    @"<ISNULL((SELECT TOP 1 f.FoodName 
                            FROM MealOrderItem moi 
                            INNER JOIN Food f ON f.FoodID = moi.FoodID 
                                AND moi.SRMealSet = '02' 
                            INNER JOIN (SELECT asri.ItemID, asri.ReferenceID FROM AppStandardReferenceItem AS asri WHERE asri.StandardReferenceID = 'FoodGroup1') std ON std.ItemID = f.SRFoodGroup1
                            WHERE moi.OrderNo = a.OrderNo AND std.ReferenceID = 'MAIN'), '-') Lunch>",
                    @"<ISNULL((SELECT TOP 1 f.FoodName 
                            FROM MealOrderItem moi 
                            INNER JOIN Food f ON f.FoodID = moi.FoodID 
                                AND moi.SRMealSet = '03' 
                            INNER JOIN (SELECT asri.ItemID, asri.ReferenceID FROM AppStandardReferenceItem AS asri WHERE asri.StandardReferenceID = 'FoodGroup1') std ON std.ItemID = f.SRFoodGroup1
                            WHERE moi.OrderNo = a.OrderNo AND std.ReferenceID = 'MAIN'), '-') AS Dinner>"
                );

            query.Where
                (
                    query.OrderNo == txtOrderNo.Text
                );

            DataTable dtb = query.LoadDataTable();

            foreach (DataRow row in dtb.Rows)
            {
                if (row["Breakfast"].ToString() == "-")
                {
                    if (retValue.Length == 0)
                        retValue = "Breakfast";
                    else
                        retValue += ", Breakfast";
                }
                if (row["Lunch"].ToString() == "-")
                {
                    if (retValue.Length == 0)
                        retValue = "Lunch";
                    else
                        retValue += ", Lunch";
                }
                if (row["Dinner"].ToString() == "-")
                {
                    if (retValue.Length == 0)
                        retValue = "Dinner";
                    else
                        retValue += ", Dinner";
                }
            }

            return retValue;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;

            if (eventArgument.Contains("rebind"))
            {
                var val = eventArgument.Split('|');
                txtSRMealSet.Text = val[1];

                grdItem.Rebind();
            }
            else if (eventArgument == "clearlist")
            {
                foreach (MealOrderItem item in MealOrderItems)
                {
                    if (item.IsOptional == true)
                        item.MarkAsDeleted();
                }
                grdItem.Rebind();
            }
        }
    }
}
