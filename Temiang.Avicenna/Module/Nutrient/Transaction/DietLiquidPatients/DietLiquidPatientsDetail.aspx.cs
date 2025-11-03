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
    public partial class DietLiquidPatientsDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List 
            UrlPageSearch = "#";
            UrlPageList = "DietLiquidPatientsList.aspx?uid=" + Request.QueryString["uid"];
            ProgramID = AppConstant.Program.DietLiquidPatients;

            if (!IsPostBack)
                DietLiquidPatientTimes = null;

        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdDietTime, grdDietTime);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new DietLiquidPatient());

            txtEffectiveStartDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtEffectiveStartTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");
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

            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuEditClick()
        {
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {

        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new DietLiquidPatient();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            txtTransactionNo.Text = GetNewTransactionNo();
            _autoNumber.Save();

            entity = new DietLiquidPatient();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);

            RefreshCommandItem(AppEnum.DataMode.Read, AppEnum.DataMode.Read);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new DietLiquidPatient();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                if (DietLiquidPatientTimes.Count == 0)
                {
                    args.MessageText = AppConstant.Message.RecordDetailEmpty;
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
            var entity = new DietLiquidPatient();
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

        private void SetVoid(DietLiquidPatient entity, bool isVoid)
        {
            //header
            entity.IsVoid = isVoid;
            entity.VoidByUserID = AppSession.UserLogin.UserID;
            entity.VoidDateTime = (new DateTime()).NowAtSqlServer();
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
            RefreshCommandItem(oldVal, newVal);

            txtEffectiveStartDate.Enabled = (newVal == AppEnum.DataMode.New);
            txtEffectiveStartTime.ReadOnly = (newVal != AppEnum.DataMode.New);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new DietLiquidPatient();
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
            var dlp = (DietLiquidPatient)entity;
            txtTransactionNo.Text = dlp.TransactionNo;
            txtRegistrationNo.Text = dlp.RegistrationNo;
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

            if (dlp.EffectiveStartDate.HasValue)
            {
                txtEffectiveStartDate.SelectedDate = dlp.EffectiveStartDate.Value.Date;
                txtEffectiveStartTime.Text = dlp.EffectiveStartTime;
            }
            
            txtNotes.Text = dlp.Notes;

            ViewState["IsVoid"] = dlp.IsVoid ?? false;

            //information
            var dpQ = new DietPatientQuery();
            dpQ.Where(dpQ.RegistrationNo == txtRegistrationNo.Text);
            dpQ.Select(dpQ.Diagnose, dpQ.BodyMassIndex, dpQ.Weight, dpQ.Height);
            dpQ.Where(dpQ.IsVoid == false, dpQ.EffectiveStartDate.Date() <= (new DateTime()).NowAtSqlServer().Date);
            dpQ.es.Top = 1;
            dpQ.OrderBy(dpQ.EffectiveStartDate.Descending, dpQ.EffectiveStartTime.Descending);
            DataTable dpDt = dpQ.LoadDataTable();
            if (dpDt.Rows.Count > 0)
            {
                txtDiagnose.Text = dpDt.Rows[0]["Diagnose"].ToString();
                txtHeight.Value = Convert.ToDouble(dpDt.Rows[0]["Height"]);
                txtWeight.Value = Convert.ToDouble(dpDt.Rows[0]["Weight"]);
                txtBodyMassIndex.Value = Convert.ToDouble(dpDt.Rows[0]["BodyMassIndex"]);
            }
            else
            {
                txtDiagnose.Text = string.Empty;
                txtHeight.Value = 0;
                txtWeight.Value = 0;
                txtBodyMassIndex.Value = 0; 
            }

            //Display Data Detail
            PopulateItemGrid();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(DietLiquidPatient entity)
        {
            entity.TransactionNo = txtTransactionNo.Text;
            entity.RegistrationNo = txtRegistrationNo.Text;
            entity.EffectiveStartDate = txtEffectiveStartDate.SelectedDate;
            entity.EffectiveStartTime = txtEffectiveStartTime.TextWithLiterals;
            entity.Notes = txtNotes.Text;
            entity.IsVoid = false;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            //Last Update Status Detail
            foreach (var item in DietLiquidPatientTimes)
            {
                item.TransactionNo = entity.TransactionNo;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(DietLiquidPatient entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();

                if (DataModeCurrent == AppEnum.DataMode.New)
                {
                    var query = new DietLiquidPatientQuery();
                    query.Where(query.RegistrationNo == entity.RegistrationNo, query.TransactionNo != entity.TransactionNo, query.IsVoid == false);
                    query.OrderBy(query.TransactionNo.Descending);
                    query.es.Top = 1;

                    var dlp = new DietLiquidPatient();
                    if (dlp.Load(query))
                    {
                        //load data dari diet sebelumnya
                        var dlpts = new DietLiquidPatientTimeCollection();
                        var dlpis = new DietLiquidPatientItemCollection();

                        var dlptBefores = new DietLiquidPatientTimeCollection();
                        dlptBefores.Query.Where(dlptBefores.Query.TransactionNo == dlp.TransactionNo);
                        dlptBefores.LoadAll();

                        foreach (var detail in dlptBefores)
                        {
                            detail.MarkAllColumnsAsDirty(DataRowState.Added);

                            detail.TransactionNo = entity.TransactionNo;
                            detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            detail.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                            dlpts.AttachEntity(detail);
                        }

                        var dlpiBefores = new DietLiquidPatientItemCollection();
                        dlpiBefores.Query.Where(dlpiBefores.Query.TransactionNo == dlp.TransactionNo);
                        dlpiBefores.LoadAll();

                        foreach (var detail in dlpiBefores)
                        {
                            detail.MarkAllColumnsAsDirty(DataRowState.Added);

                            detail.TransactionNo = entity.TransactionNo;
                            detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            detail.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                            dlpis.AttachEntity(detail);
                        }

                        dlpts.Save();
                        dlpis.Save();
                    }
                    else
                    {
                        //load data dari master
                        var reg = new Registration();
                        reg.LoadByPrimaryKey(entity.RegistrationNo);

                        var isAdult = reg.AgeInYear.ToInt() >= 17;

                        var pat = new Patient();
                        pat.LoadByPrimaryKey(reg.PatientID);

                        var dpiQ = new DietPatientItemQuery("a");
                        var dpQ = new DietPatientQuery("b");
                        dpiQ.Select(dpiQ.DietID, dpQ.FormOfFood, dpiQ.ExtraQty, dpiQ.LiquidTime);
                        dpiQ.InnerJoin(dpQ).On(dpiQ.TransactionNo == dpQ.TransactionNo);
                        dpiQ.Where(dpQ.RegistrationNo == entity.RegistrationNo,
                                  dpQ.EffectiveStartDate.Date() <= (new DateTime()).NowAtSqlServer().Date, dpQ.IsVoid == false);
                        dpiQ.OrderBy(dpQ.EffectiveStartDate.Descending, dpQ.EffectiveStartTime.Descending);
                        dpiQ.es.Top = 1;
                        DataTable dpidtb = dpiQ.LoadDataTable();
                        if (dpidtb.Rows.Count > 0)
                        {
                            #region dlpt & dlpi
                            var formOfFood = dpidtb.Rows[0]["FormOfFood"].ToString();
                            if (formOfFood == "1" || formOfFood == "7" || formOfFood == "9")
                            {
                                var dlpts = new DietLiquidPatientTimeCollection();
                                var dlpis = new DietLiquidPatientItemCollection();

                                var time = new AppStandardReferenceItemCollection();
                                time.Query.Where(time.Query.StandardReferenceID == "DietLiquidTime");
                                time.LoadAll();

                                if (formOfFood == "1" || formOfFood == "7")
                                {
                                    //-- cair
                                    //--- cek LQ-Unit, kalo gak ketemu baru cek di LQ-Class
                                    string stdId, stdItemId;
                                    var lq = new AppStandardReferenceItem();
                                    if (lq.LoadByPrimaryKey("LQ-Unit", reg.ServiceUnitID))
                                    {
                                        stdId = "LQ-Unit";
                                        stdItemId = reg.ServiceUnitID;
                                    }
                                    else
                                    {
                                        stdId = "LQ-Class";
                                        stdItemId = reg.ClassID;
                                    }

                                    foreach (var t in time)
                                    {
                                        //--- cek food setting :
                                        //--- 1. LiquidFoodDietTimeGender
                                        //--- 2. LiquidFoodDietTime
                                        //--- 3. LiquidFoodDiet
                                        //--- 4. LiquidFoodTimeGender
                                        //--- 5. LiquidFoodTime
                                        //--- 6. AppStandardReferenceItem
                                        var foodId = string.Empty;

                                        var lqdg = new LiquidFoodDietTimeGender();
                                        if (lqdg.LoadByPrimaryKey(dpidtb.Rows[0]["DietID"].ToString(), pat.Sex, t.ItemID))
                                        {
                                            foodId = isAdult ? lqdg.FoodID : lqdg.ChildrenFoodID;
                                        }
                                        else
                                        {
                                            var lqdt = new LiquidFoodDietTime();
                                            if (lqdt.LoadByPrimaryKey(dpidtb.Rows[0]["DietID"].ToString(), t.ItemID))
                                            {
                                                foodId = isAdult ? lqdt.FoodID : lqdt.ChildrenFoodID;
                                            }
                                            else
                                            {
                                                var lqd = new LiquidFoodDiet();
                                                if (lqd.LoadByPrimaryKey(dpidtb.Rows[0]["DietID"].ToString()))
                                                {
                                                    foodId = isAdult ? lqd.FoodID : lqd.ChildrenFoodID;
                                                }
                                                else
                                                {
                                                    var lqg = new LiquidFoodTimeGender();
                                                    if (lqg.LoadByPrimaryKey(pat.Sex, stdItemId, stdId, t.ItemID))
                                                    {
                                                        foodId = isAdult ? lqg.FoodID : lqg.ChildrenFoodID;
                                                    }
                                                    else
                                                    {
                                                        var lqt = new LiquidFoodTime();
                                                        if (lqt.LoadByPrimaryKey(stdItemId, stdId, t.ItemID))
                                                        {
                                                            foodId = isAdult ? lqt.FoodID : lqt.ChildrenFoodID;
                                                        }
                                                        else
                                                        {
                                                            var lqi = new AppStandardReferenceItem();
                                                            if (lqi.LoadByPrimaryKey(stdId, stdItemId))
                                                                foodId = isAdult ? lqi.ReferenceID : lqi.Note;
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(foodId))
                                        {
                                            var dlpt = dlpts.AddNew();
                                            dlpt.TransactionNo = entity.TransactionNo;
                                            dlpt.DietTime = t.ItemID;
                                            dlpt.FoodID = foodId;

                                            var food = new Food();
                                            dlpt.Measure = food.LoadByPrimaryKey(dlpt.FoodID)
                                                               ? food.Weight.ToString() + " " + food.SRItemUnit
                                                               : string.Empty;

                                            dlpt.AmountOfWater = 0;
                                            dlpt.Etc = string.Empty;
                                            dlpt.Total = 0;
                                            dlpt.Notes = string.Empty;
                                            dlpt.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                            dlpt.LastUpdateByUserID = AppSession.UserLogin.UserID;

                                            var foodItems = new FoodItemCollection();
                                            foodItems.Query.Where(foodItems.Query.FoodID == dlpt.FoodID);
                                            foodItems.LoadAll();
                                            foreach (var foodItem in foodItems)
                                            {
                                                var dlpi = dlpis.AddNew();
                                                dlpi.TransactionNo = entity.TransactionNo;
                                                dlpi.DietTime = dlpt.DietTime;
                                                dlpi.ItemID = foodItem.ItemID;
                                                dlpi.Qty = foodItem.Qty;
                                                dlpi.SRItemUnit = foodItem.SRItemUnit;
                                                dlpi.Notes = string.Empty;
                                                dlpi.FoodID = dlpt.FoodID;
                                                dlpi.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                                dlpi.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //-- blenderize
                                    var foodId = AppSession.Parameter.BlenderizedFoodId;
                                    foreach (var t in time)
                                    {
                                        if (!string.IsNullOrEmpty(foodId))
                                        {
                                            var dlpt = dlpts.AddNew();
                                            dlpt.TransactionNo = entity.TransactionNo;
                                            dlpt.DietTime = t.ItemID;
                                            dlpt.FoodID = foodId;

                                            var food = new Food();
                                            dlpt.Measure = food.LoadByPrimaryKey(dlpt.FoodID)
                                                               ? food.Weight.ToString() + " " + food.SRItemUnit
                                                               : string.Empty;

                                            dlpt.AmountOfWater = 0;
                                            dlpt.Etc = string.Empty;
                                            dlpt.Total = 0;
                                            dlpt.Notes = string.Empty;
                                            dlpt.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                            dlpt.LastUpdateByUserID = AppSession.UserLogin.UserID;

                                            var foodItems = new FoodItemCollection();
                                            foodItems.Query.Where(foodItems.Query.FoodID == dlpt.FoodID);
                                            foodItems.LoadAll();
                                            foreach (var foodItem in foodItems)
                                            {
                                                var dlpi = dlpis.AddNew();
                                                dlpi.TransactionNo = entity.TransactionNo;
                                                dlpi.DietTime = dlpt.DietTime;
                                                dlpi.ItemID = foodItem.ItemID;
                                                dlpi.Qty = foodItem.Qty;
                                                dlpi.SRItemUnit = foodItem.SRItemUnit;
                                                dlpi.Notes = string.Empty;
                                                dlpi.FoodID = dlpt.FoodID;
                                                dlpi.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                                dlpi.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                            }
                                        }
                                    }
                                }

                                dlpts.Save();
                                dlpis.Save();
                            }
                            else if (Convert.ToInt32(dpidtb.Rows[0]["FormOfFood"]) > 0)
                            {
                                var liquidTime = dpidtb.Rows[0]["LiquidTime"].ToString();

                                var dlpts = new DietLiquidPatientTimeCollection();
                                var dlpis = new DietLiquidPatientItemCollection();

                                var time = new AppStandardReferenceItemCollection();
                                time.Query.Where(time.Query.StandardReferenceID == "DietLiquidTime");
                                time.LoadAll();

                                //--- cek LQ-Unit, kalo gak ketemu baru cek di LQ-Class
                                string stdId, stdItemId;
                                var lq = new AppStandardReferenceItem();
                                if (lq.LoadByPrimaryKey("LQ-Unit", reg.ServiceUnitID))
                                {
                                    stdId = "LQ-Unit";
                                    stdItemId = reg.ServiceUnitID;
                                }
                                else
                                {
                                    stdId = "LQ-Class";
                                    stdItemId = reg.ClassID;
                                }

                                foreach (var t in time)
                                {
                                    var foodId = string.Empty;
                                    if (liquidTime.Contains(t.ItemID))
                                    {
                                        //--- cek food setting :
                                        //--- 1. LiquidFoodDietTimeGender
                                        //--- 2. LiquidFoodDietTime
                                        //--- 3. LiquidFoodDiet
                                        //--- 4. LiquidFoodTimeGender
                                        //--- 5. LiquidFoodTime
                                        //--- 6. AppStandardReferenceItem
                                        var lqdg = new LiquidFoodDietTimeGender();
                                        if (lqdg.LoadByPrimaryKey(dpidtb.Rows[0]["DietID"].ToString(), pat.Sex, t.ItemID))
                                        {
                                            foodId = isAdult ? lqdg.FoodID : lqdg.ChildrenFoodID;
                                        }
                                        else
                                        {
                                            var lqdt = new LiquidFoodDietTime();
                                            if (lqdt.LoadByPrimaryKey(dpidtb.Rows[0]["DietID"].ToString(), t.ItemID))
                                            {
                                                foodId = isAdult ? lqdt.FoodID : lqdt.ChildrenFoodID;
                                            }
                                            else
                                            {
                                                var lqd = new LiquidFoodDiet();
                                                if (lqd.LoadByPrimaryKey(dpidtb.Rows[0]["DietID"].ToString()))
                                                {
                                                    foodId = isAdult ? lqd.FoodID : lqd.ChildrenFoodID;
                                                }
                                                else
                                                {
                                                    var lqg = new LiquidFoodTimeGender();
                                                    if (lqg.LoadByPrimaryKey(pat.Sex, stdItemId, stdId, t.ItemID))
                                                    {
                                                        foodId = isAdult ? lqg.FoodID : lqg.ChildrenFoodID;
                                                    }
                                                    else
                                                    {
                                                        var lqt = new LiquidFoodTime();
                                                        if (lqt.LoadByPrimaryKey(stdItemId, stdId, t.ItemID))
                                                        {
                                                            foodId = isAdult ? lqt.FoodID : lqt.ChildrenFoodID;
                                                        }
                                                        else
                                                        {
                                                            var lqi = new AppStandardReferenceItem();
                                                            if (lqi.LoadByPrimaryKey(stdId, stdItemId))
                                                                foodId = isAdult ? lqi.ReferenceID : lqi.Note;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    var dlpt = dlpts.AddNew();
                                    dlpt.TransactionNo = entity.TransactionNo;
                                    dlpt.DietTime = t.ItemID;
                                    dlpt.FoodID = foodId;

                                    var food = new Food();
                                    dlpt.Measure = food.LoadByPrimaryKey(dlpt.FoodID)
                                                       ? food.Weight.ToString() + " " + food.SRItemUnit
                                                       : string.Empty;

                                    dlpt.AmountOfWater = 0;
                                    dlpt.Etc = string.Empty;
                                    dlpt.Total = 0;
                                    dlpt.Notes = string.Empty;
                                    dlpt.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                    dlpt.LastUpdateByUserID = AppSession.UserLogin.UserID;

                                    if (!string.IsNullOrEmpty(foodId))
                                    {
                                        var foodItems = new FoodItemCollection();
                                        foodItems.Query.Where(foodItems.Query.FoodID == dlpt.FoodID);
                                        foodItems.LoadAll();
                                        foreach (var foodItem in foodItems)
                                        {
                                            var dlpi = dlpis.AddNew();
                                            dlpi.TransactionNo = entity.TransactionNo;
                                            dlpi.DietTime = dlpt.DietTime;
                                            dlpi.ItemID = foodItem.ItemID;
                                            dlpi.Qty = foodItem.Qty;
                                            dlpi.SRItemUnit = foodItem.SRItemUnit;
                                            dlpi.Notes = string.Empty;
                                            dlpi.FoodID = dlpt.FoodID;
                                            dlpi.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                            dlpi.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                        }
                                    }
                                }
                                dlpts.Save();
                                dlpis.Save();
                            }

                            #endregion
                        }
                    }
                }
                else
                    DietLiquidPatientTimes.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new DietLiquidPatientQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.TransactionNo > txtTransactionNo.Text, que.RegistrationNo == txtRegistrationNo.Text
                    );
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.TransactionNo < txtTransactionNo.Text, que.RegistrationNo == txtRegistrationNo.Text
                    );
                que.OrderBy(que.TransactionNo.Descending);
            }

            var entity = new DietLiquidPatient();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer(), AppEnum.AutoNumber.DietLiquidNo);
            return _autoNumber.LastCompleteNumber;
        }

        #endregion

        #region Record Detail Method Function DietLiquidPatientTime

        private DietLiquidPatientTimeCollection DietLiquidPatientTimes
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collDietLiquidPatientTime" + Request.UserHostName];
                    if (obj != null)
                        return ((DietLiquidPatientTimeCollection)(obj));
                }

                var coll = new DietLiquidPatientTimeCollection();
                var query = new DietLiquidPatientTimeQuery("a");
                var foodq = new FoodQuery("b");
                query.Select
                    (
                        query,
                        foodq.FoodName.As("refToFood_FoodName")
                    );

                query.LeftJoin(foodq).On(query.FoodID == foodq.FoodID);
                query.Where(query.TransactionNo == txtTransactionNo.Text);

                query.OrderBy(query.DietTime.Ascending);
                coll.Load(query);

                Session["collDietLiquidPatientTime" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collDietLiquidPatientTime" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItem(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdDietTime.Columns[0].Visible = isVisible;
            grdDietTime.Columns[grdDietTime.Columns.Count - 1].Visible = isVisible;

            if (oldVal != AppEnum.DataMode.Read)
                DietLiquidPatientTimes = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdDietTime.Rebind();
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            DietLiquidPatientTimes = null; //Reset Record Detail
            grdDietTime.DataSource = DietLiquidPatientTimes; //Requery
            grdDietTime.MasterTableView.IsItemInserted = false;
            grdDietTime.MasterTableView.ClearEditItems();
            grdDietTime.DataBind();
        }

        protected void grdDietTime_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDietTime.DataSource = DietLiquidPatientTimes;
        }

        protected void grdDietTime_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            var time = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][DietLiquidPatientTimeMetadata.ColumnNames.DietTime]);
            var foodId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][DietLiquidPatientTimeMetadata.ColumnNames.FoodID]);
            var entity = FindDietLiquidPatientTime(time);
            if (entity != null)
                SetEntityValue(entity, e);

            var dlpis = new DietLiquidPatientItemCollection();
            dlpis.Query.Where(dlpis.Query.TransactionNo == txtTransactionNo.Text, dlpis.Query.DietTime == time,
                              dlpis.Query.FoodID == foodId);
            dlpis.LoadAll();
            if (dlpis.Count == 0)
            {
                dlpis = new DietLiquidPatientItemCollection();
                dlpis.Query.Where(dlpis.Query.TransactionNo == txtTransactionNo.Text, dlpis.Query.DietTime == time);
                dlpis.LoadAll();
                dlpis.MarkAllAsDeleted();
                dlpis.Save();

                dlpis = new DietLiquidPatientItemCollection();
                var foodItems = new FoodItemCollection();
                foodItems.Query.Where(foodItems.Query.FoodID == foodId);
                foodItems.LoadAll();
                foreach (var foodItem in foodItems)
                {
                    var dlpi = dlpis.AddNew();
                    dlpi.TransactionNo = txtTransactionNo.Text;
                    dlpi.DietTime = time;
                    dlpi.ItemID = foodItem.ItemID;
                    dlpi.Qty = foodItem.Qty;
                    dlpi.SRItemUnit = foodItem.SRItemUnit;
                    dlpi.Notes = string.Empty;
                    dlpi.FoodID = foodId;
                    dlpi.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    dlpi.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }
                dlpis.Save();
            }
        }

        private DietLiquidPatientTime FindDietLiquidPatientTime(String time)
        {
            return DietLiquidPatientTimes.FirstOrDefault(rec => rec.DietTime.Equals(time));
        }

        private void SetEntityValue(DietLiquidPatientTime entity, GridCommandEventArgs e)
        {
            var userControl = (DietLiquidPatientsTimeDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.TransactionNo = txtTransactionNo.Text;
                entity.FoodID = userControl.FoodID;
                entity.FoodName = userControl.FoodName;
                entity.Measure = userControl.Measure;
                entity.AmountOfWater = userControl.AmountOfWater;
                entity.Etc = userControl.Etc;
                entity.Total = userControl.Total;
                entity.Notes = userControl.Notes;
            }
        }

        #endregion

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
        }

    }
}
