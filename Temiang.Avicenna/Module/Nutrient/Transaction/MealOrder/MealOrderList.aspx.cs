using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;
using System.Drawing;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class MealOrderList : BasePage
    {
        protected string IsAdditional
        {
            get { return (string)ViewState["_add"]; }
            set { ViewState["_add"] = value; }
        }

        private string MsgEmptyMenu
        {
            get { return (string)ViewState["_msgEmptyMenu"]; }
            set { ViewState["_msgEmptyMenu"] = value; }
        }

        private string MsgEmptyMainFood
        {
            get { return (string)ViewState["_msgEmptyMainFood"]; }
            set { ViewState["_msgEmptyMainFood"] = value; }
        }

        private bool _isHideEmptySearchMessage = false;
        protected void Page_Init(object sender, EventArgs e)
        {
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            switch (Request.QueryString["add"])
            {
                case "0":
                    ProgramID = AppConstant.Program.MealOrder;
                    break;
                case "1":
                    ProgramID = AppConstant.Program.AdditionalMealOrder;
                    break;
            }

            IsAdditional = Request.QueryString["add"];

            if (!IsPostBack)
            {
                txtOrderDate.Enabled = IsAdditional == "1" || this.IsUserCrossUnitAble;

                //service unit
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");

                if (!this.IsUserCrossUnitAble)
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                    query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                }

                query.Where(
                    query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                    query.IsActive == true
                    );

                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                if (!string.IsNullOrEmpty(Request.QueryString["unitid"]))
                    cboServiceUnitID.SelectedValue = Request.QueryString["unitid"];

                txtOrderDate.SelectedDate = Convert.ToDateTime("2000-01-01 00:00:00.000");
                var initQ = new MealOrderDateInitQuery();
                initQ.OrderBy(initQ.MealOrderDate.Descending);
                initQ.es.Top = 1;
                var initColl = new MealOrderDateInitCollection();
                initColl.Load(initQ);
                foreach (var item in initColl)
                {
                    txtOrderDate.SelectedDate = item.MealOrderDate;
                }

                if (Request.QueryString["add"] == "1")
                {
                    RadToolBar2.Items[0].Visible = false;
                    RadToolBar2.Items[1].Visible = false;
                    RadToolBar2.Items[3].Visible = false;
                    RadToolBar2.Items[4].Visible = false;
                    RadToolBar2.Items[5].Visible = false;
                }
                else
                {
                    RadToolBar2.Items[0].Visible = this.IsUserAddAble;
                    RadToolBar2.Items[3].Visible = this.IsUserApproveAble;
                    RadToolBar2.Items[4].Visible = this.IsUserUnApproveAble;
                    RadToolBar2.Items[5].Visible = this.IsUserVoidAble;

                    grdList.Columns[0].Visible = false;
                }
            }
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = MealOrders;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }           
        }

        private DataTable MealOrders
        {
            get
            {
                var isEmptyFilter = txtOrderDate.IsEmpty && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Meal Order")) return null;

                var query = new RegistrationQuery("a");
                var patient = new PatientQuery("b");
                var unit = new ServiceUnitQuery("c");
                var room = new ServiceRoomQuery("d");
                var medic = new ParamedicQuery("e");
                var grr = new GuarantorQuery("f");

                var mealOrder = new MealOrderQuery("h");
                var dietPat = new DietPatientItemQuery("i");
                var diet = new DietQuery("j");
                var menu = new MenuQuery("k");
                var cls = new ClassQuery("l");
                var bd = new BedQuery("m");
                var sal = new AppStandardReferenceItemQuery("sal");

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        room.RoomName,
                        query.RegistrationDate,
                        query.ServiceUnitID,
                        unit.ServiceUnitName,
                        query.RoomID,
                        query.ParamedicID,
                        medic.ParamedicName,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        grr.GuarantorName,
                        query.BedID,
                        query.ChargeClassID.As("ClassID"),
                        cls.ClassName,
                        sal.ItemName.As("SalutationName"),

                        mealOrder.OrderNo,
                        mealOrder.OrderDate,
                        mealOrder.EffectiveDate,
                        mealOrder.FastingTime,
                        mealOrder.DietPatientNo,
                        mealOrder.DietID,
                        diet.DietName,
                        @"<ISNULL(k.MenuName, '') AS 'MenuName'>",
                        @"<CAST(0 AS BIT) AS 'IsFastingMorning'>",
                        @"<CAST(0 AS BIT) AS 'IsFastingDay'>",
                        @"<CAST(0 AS BIT) AS 'IsFastingNight'>",
                        mealOrder.IsAdditional,
                        mealOrder.IsApproved,
                        mealOrder.IsVoid,
                        @"<CASE WHEN h.OrderNo IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsViewVisible'>",
                        @"<CAST(a.AgeInYear AS VARCHAR) + 'y ' + CAST(a.AgeInMonth AS VARCHAR) + 'm ' + CAST(a.AgeInDay AS VARCHAR) + 'd' AS Age>",
                        @"<ISNULL(h.ServiceUnitID, a.ServiceUnitID) AS 'ServiceUnitOrderID'>",
                        @"<ISNULL(h.BedID, a.BedID) AS 'BedOrderID'>",
                        @"<ISNULL(h.ClassID, a.ChargeClassID) AS 'ClassOrderID'>"
                    );

                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                query.InnerJoin(cls).On(query.ChargeClassID == cls.ClassID);
                if (!this.IsUserCrossUnitAble)
                {
                    var qusr = new AppUserServiceUnitQuery("g");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID && qusr.UserID == AppSession.UserLogin.UserID);
                }

                if (!txtOrderDate.IsEmpty)
                    query.LeftJoin(mealOrder).On(query.RegistrationNo == mealOrder.RegistrationNo &&
                                                 mealOrder.EffectiveDate == txtOrderDate.SelectedDate &&
                                                 mealOrder.IsVoid == false);
                else
                {
                    DateTime od = Convert.ToDateTime("1900-01-01 00:00:00");
                    query.InnerJoin(mealOrder).On(query.RegistrationNo == mealOrder.RegistrationNo &&
                                                 mealOrder.EffectiveDate == od &&
                                                 mealOrder.IsVoid == false);
                }
                    
                query.LeftJoin(dietPat).On(mealOrder.DietPatientNo == dietPat.TransactionNo & mealOrder.DietID == dietPat.DietID);
                query.LeftJoin(diet).On(dietPat.DietID == diet.DietID);
                query.LeftJoin(menu).On(mealOrder.MenuID == menu.MenuID);
                query.InnerJoin(bd).On(query.BedID == bd.BedID & query.RegistrationNo == bd.RegistrationNo);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

                query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);

                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    //query.Where(
                    //    query.Or(
                    //        query.RegistrationNo == searchReg,
                    //        patient.MedicalNo == searchReg,
                    //        patient.OldMedicalNo == searchReg,
                    //        string.Format("< OR REPLACE(b.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //        string.Format("< OR REPLACE(b.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //        )
                    //    );
                    Helper.AddFilterMedNoOrRegNoOrPatName(query, patient, searchReg, "registration");
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) LIKE '{0}'>", searchPatient)
                        );
                }

                query.Where
                    (
                        query.IsClosed == false,
                        query.DischargeDate.IsNull(),
                        query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                        query.IsVoid == false,
                        bd.SRBedStatus == AppSession.Parameter.BedStatusOccupied
                    );
                if (Request.QueryString["add"] == "1")
                    query.Where(query.Or(mealOrder.RegistrationNo.IsNull(), mealOrder.IsAdditional == true));
                else
                    query.Where(query.Or(mealOrder.RegistrationNo.IsNull(), mealOrder.IsAdditional == false));

                query.OrderBy(unit.ServiceUnitName.Ascending, query.BedID.Ascending);

                DataTable dtb = query.LoadDataTable();

                foreach (DataRow d in dtb.Rows)
                {
                    if (d["DietName"].ToString().Length == 0)
                    {
                        var dpcoll = new DietPatientCollection();
                        var dpq = new DietPatientQuery();
                        if (!txtOrderDate.IsEmpty)
                        {
                            dpq.Where(dpq.RegistrationNo == d["RegistrationNo"].ToString(),
                                dpq.EffectiveStartDate.Date() <= txtOrderDate.SelectedDate,
                                dpq.IsVoid == false);
                        }
                        else
                        {
                            DateTime od = Convert.ToDateTime("1900-01-01 00:00:00");
                            dpq.Where(dpq.RegistrationNo == d["RegistrationNo"].ToString(),
                                dpq.EffectiveStartDate.Date() <= od,
                                dpq.IsVoid == false);
                        }
                        dpq.OrderBy(dpq.EffectiveStartDate.Descending, dpq.EffectiveStartTime.Descending, dpq.TransactionNo.Descending);
                        dpq.es.Top = 1;
                        dpcoll.Load(dpq);
                        if (dpcoll.Count > 0)
                        {
                            string dietName = string.Empty;
                            var i = 0;

                            foreach (var dp in dpcoll)
                            {
                                var dpicoll = new DietPatientItemCollection();
                                var dpiq = new DietPatientItemQuery("a");
                                var dmq = new DietMenuQuery("b");
                                dpiq.InnerJoin(dmq).On(dmq.DietID == dpiq.DietID && dmq.FormOfFood == dp.FormOfFood);
                                dpiq.Where(dpiq.TransactionNo == dp.TransactionNo);
                                dpicoll.Load(dpiq);

                                foreach (var dpi in dpicoll)
                                {
                                    var di = new Diet();
                                    if (di.LoadByPrimaryKey(dpi.DietID))
                                    {
                                        if (i > 0)
                                            dietName += ", ";

                                        dietName += di.DietName;
                                    }

                                    i += 1;
                                }
                            }
                            d["DietName"] = dietName;
                        }
                    }
                    if (d["DietName"].ToString().Length == 0)
                        d.Delete();
                    else
                    {
                        int ttl = d["FastingTime"].ToString().Length;
                        int idx = 0;
                        while (idx < ttl)
                        {
                            string parseChar = d["FastingTime"].ToString().Substring(idx, 1);
                            if (parseChar != ";")
                            {
                                switch (parseChar)
                                {
                                    case "1":
                                        d["IsFastingMorning"] = true;
                                        break;
                                    case "2":
                                        d["IsFastingDay"] = true;
                                        break;
                                    case "3":
                                        d["IsFastingNight"] = true;
                                        break;
                                }
                            }
                            idx += 1;
                        }

                        if (!string.IsNullOrEmpty(d["DietPatientNo"].ToString()) && !string.IsNullOrEmpty(d["DietID"].ToString()))
                        {
                            var comps = new DietComplicationPatientCollection();
                            comps.Query.Where(comps.Query.TransactionNo == d["DietPatientNo"].ToString(),
                                              comps.Query.DietID == d["DietID"].ToString());
                            comps.LoadAll();

                            var dietCompName = string.Empty;
                            var i = 0;

                            foreach (var comp in comps)
                            {
                                var dc = new Diet();
                                if (dc.LoadByPrimaryKey(comp.DietComplicationID))
                                {
                                    if (i > 0)
                                        dietCompName += ", ";

                                    dietCompName += dc.DietName;
                                }

                                i += 1;
                            }

                            if (!string.IsNullOrEmpty(dietCompName))
                                d["DietName"] = d["DietName"] + " (with Comp: " + dietCompName + ")";
                        }
                    }
                }

                dtb.AcceptChanges();

                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            pnlInfo.Visible = false;
            if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && txtOrderDate.IsEmpty)
            {
                pnlInfo.Visible = true;
                lblInfo.Text = "Service Unit and Order To Date required.";
                return;
            }

            if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                pnlInfo.Visible = true;
                lblInfo.Text = "Service Unit required.";
                return;
            }

            if (txtOrderDate.IsEmpty)
            {
                pnlInfo.Visible = true;
                lblInfo.Text = "Order To Date required.";
                return;
            }

            grdList.Rebind();
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            pnlInfo.Visible = false;
            if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && txtOrderDate.IsEmpty)
            {
                pnlInfo.Visible = true;
                lblInfo.Text = "Service Unit and Order To Date required.";
                return;
            }

            if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                pnlInfo.Visible = true;
                lblInfo.Text = "Service Unit required.";
                return;
            }

            if (txtOrderDate.IsEmpty)
            {
                pnlInfo.Visible = true;
                lblInfo.Text = "Order To Date required.";
                return;
            }

            grdList.Rebind();
        }

        private void Process()
        {
            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                string regNo = dataItem.GetDataKeyValue("RegistrationNo").ToString();
                var dpColl = new DietPatientCollection();
                var dpQ = new DietPatientQuery();
                dpQ.Where(dpQ.RegistrationNo == regNo,
                          dpQ.EffectiveStartDate.Date() <= txtOrderDate.SelectedDate, dpQ.IsVoid == false);
                dpQ.OrderBy(dpQ.EffectiveStartDate.Descending, dpQ.EffectiveStartTime.Descending, dpQ.TransactionNo.Descending);
                dpQ.es.Top = 1;
                dpColl.Load(dpQ);

                foreach (var dp in dpColl)
                {
                    var dpic = new DietPatientItemCollection();
                    dpic.Query.Where(dpic.Query.TransactionNo == dp.TransactionNo);
                    dpic.LoadAll();

                    foreach (var dpi in dpic)
                    {
                        InsertToMealOrder(regNo, dp.TransactionNo, dpi.DietID, dp.FormOfFood);
                    }
                }
            }
        }

        private void InsertToMealOrder(string regNo, string transNo, string dietId, string formOfFood)
        {
            var coll = new MealOrderCollection();
            coll.Query.Where(coll.Query.EffectiveDate == txtOrderDate.SelectedDate, coll.Query.RegistrationNo == regNo,
                             coll.Query.IsVoid == false);
            coll.LoadAll();
            if (coll.Count == 0)
            {
                using (var trans = new esTransactionScope())
                {
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(regNo);

                    var mealOrder = new MealOrder();
                    var number = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.MealOrderNo);
                    mealOrder.OrderNo = number.LastCompleteNumber;

                    //--number--
                    number.LastCompleteNumber = mealOrder.OrderNo;

                    //--header--
                    mealOrder.OrderDate = (new DateTime()).NowAtSqlServer().Date;
                    mealOrder.EffectiveDate = txtOrderDate.SelectedDate;
                    mealOrder.RegistrationNo = regNo;
                    mealOrder.ServiceUnitID = reg.ServiceUnitID;
                    mealOrder.ClassID = reg.ChargeClassID;
                    mealOrder.BedID = reg.BedID;

                    //--start diet patient--
                    mealOrder.DietPatientNo = transNo;
                    mealOrder.DietID = dietId;

                    var diet = new DietMenu();
                    if (diet.LoadByPrimaryKey(mealOrder.DietID, formOfFood))
                        mealOrder.MenuID = diet.MenuID;
                    else
                        mealOrder.MenuID = string.Empty;

                    var menuSettingQ = new MenuSettingQuery();
                    menuSettingQ.es.Top = 1;
                    menuSettingQ.Where(menuSettingQ.StartingDate <= txtOrderDate.SelectedDate, menuSettingQ.IsExtra == false);
                    menuSettingQ.OrderBy(menuSettingQ.StartingDate.Descending);

                    var menuSetting = new MenuSetting();
                    if (menuSetting.Load(menuSettingQ))
                    {
                        string seqNo;
                        //settingan dipindah ke table MenuVersion
                        //if (AppSession.Parameter.IsMealOrderUsingThe10Plus1Rule)
                        //{
                        //    int day = txtOrderDate.SelectedDate.Value.Date.Day;
                        //    if (day == 31)
                        //        seqNo = "11";
                        //    else
                        //    {
                        //        int i = (day % 10);
                        //        if (i == 0)
                        //            i = 10;
                        //        seqNo = i < 10 ? "0" + string.Format("{0}", i) : string.Format("{0}", i);
                        //    }
                        //}
                        //else
                        {
                            var menuVersion = new MenuVersion();
                            menuVersion.LoadByPrimaryKey(menuSetting.VersionID);
                            var cycle = (menuVersion.Cycle ?? 1);

                            if (menuVersion.IsPlusOneRule ?? false)
                            {
                                int day = txtOrderDate.SelectedDate.Value.Date.Day;
                                
                                if (day == 31)
                                    seqNo = string.Format("{0:00}", cycle + 1);
                                else
                                {
                                    int i = (day % cycle);
                                    if (i == 0)
                                        i = cycle;
                                    seqNo = string.Format("{0:00}", i);
                                }
                            }
                            else
                            {
                                int diff = (txtOrderDate.SelectedDate.Value.Date.Subtract(menuSetting.StartingDate.Value.Date)).Days;
                                int i = (diff % cycle) + Convert.ToInt32(menuSetting.SeqNo);

                                if (i > cycle)
                                    i -= cycle;
                                seqNo = string.Format("{0:00}", i);
                            }
                        }

                        mealOrder.VersionID = menuSetting.VersionID;
                        mealOrder.SeqNo = seqNo;

                        if (Request.QueryString["add"] == "0")
                        {
                            var menuItemQ = new MenuItemQuery();
                            menuItemQ.es.Top = 1;
                            menuItemQ.Where(menuItemQ.MenuID == mealOrder.MenuID,
                                            menuItemQ.VersionID == menuSetting.VersionID, menuItemQ.SeqNo == seqNo,
                                            menuItemQ.ClassID == reg.ChargeClassID, menuItemQ.IsActive == true);
                            menuItemQ.OrderBy(menuItemQ.LastUpdateDateTime.Descending);

                            var menuItem = new MenuItem();
                            mealOrder.MenuItemID = menuItem.Load(menuItemQ) ? menuItem.MenuItemID : string.Empty;

                            if (mealOrder.MenuItemID == string.Empty)
                            {
                                var c = new Class();
                                c.LoadByPrimaryKey(reg.ChargeClassID);
                                var m = new Menu();
                                m.LoadByPrimaryKey(mealOrder.MenuID);

                                if (MsgEmptyMenu.Length == 0)
                                    MsgEmptyMenu = "Master MENU ITEM has not been configured to Menu: " + m.MenuName + "-" + mealOrder.MenuID + " [Class: " + c.ClassName + ", Version: " + mealOrder.VersionID + "-" + mealOrder.SeqNo + "]";
                                else
                                    MsgEmptyMenu += "; " + m.MenuName + "-" + mealOrder.MenuID + "[Class: " + c.ClassName + ", Version: " + mealOrder.VersionID + "-" + mealOrder.SeqNo + "]";
                            }
                        }
                        else
                        {
                            var isUsedClassMenuStandard = AppSession.Parameter.IsAdditionalMealOrderUsedClassMenuStandard;

                            var menuItemQ = new MenuItemQuery();
                            menuItemQ.es.Top = 1;
                            menuItemQ.Where(menuItemQ.MenuID == mealOrder.MenuID,
                                            menuItemQ.VersionID == menuSetting.VersionID, menuItemQ.SeqNo == seqNo,
                                            menuItemQ.IsActive == true);
                            if (isUsedClassMenuStandard)
                                menuItemQ.Where(menuItemQ.ClassID == AppSession.Parameter.DefaultClassMenuStandard);
                            else
                                menuItemQ.Where(menuItemQ.ClassID == reg.ChargeClassID);
                            menuItemQ.OrderBy(menuItemQ.LastUpdateDateTime.Descending);

                            var menuItem = new MenuItem();
                            mealOrder.MenuItemID = menuItem.Load(menuItemQ) ? menuItem.MenuItemID : string.Empty;

                            if (mealOrder.MenuItemID == string.Empty)
                            {
                                var className = "Default class for additional meal order";
                                var c = new Class();
                                if (c.LoadByPrimaryKey(AppSession.Parameter.DefaultClassMenuStandard))
                                    className = c.ClassName;

                                var m = new Menu();
                                m.LoadByPrimaryKey(mealOrder.MenuID);

                                if (MsgEmptyMenu.Length == 0)
                                    MsgEmptyMenu = "Master MENU ITEM has not been configured to Menu: " + m.MenuName + "-" + mealOrder.MenuID + " [Class: " + className + ", Version: " + mealOrder.VersionID + "-" + mealOrder.SeqNo + "]";
                                else
                                    MsgEmptyMenu += "; " + m.MenuName + "-" + mealOrder.MenuID + "[Class: " + className + ", Version: " + mealOrder.VersionID + "-" + mealOrder.SeqNo + "]";
                            }
                        }
                    }
                    else
                    {
                        mealOrder.MenuItemID = string.Empty;
                        mealOrder.VersionID = string.Empty;
                        mealOrder.SeqNo = string.Empty;

                        MsgEmptyMenu = "MENU INITIALIZATION is not be set.";
                    }

                    //--end diet patient--

                    if (!string.IsNullOrEmpty(mealOrder.MenuItemID))
                    {
                        number.Save();

                        mealOrder.FastingTime = string.Empty;
                        mealOrder.IsAdditional = (Request.QueryString["add"] == "1");
                        mealOrder.IsApproved = false;
                        mealOrder.IsVoid = false;
                        mealOrder.IsVerified = false;
                        mealOrder.IsOpr = false;
                        mealOrder.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        mealOrder.LastUpdateByUserID = AppSession.UserLogin.UserID;

                        mealOrder.Save();

                        //--detail--
                        if (!string.IsNullOrEmpty(mealOrder.MenuItemID))
                        {
                            var mealOrderItemColl = new MealOrderItemCollection();
                            var mealOrderItemSettingColl = new MealOrderItemSettingCollection();

                            var initMenuClassMealSetColl = new ClassMealSetMenuSettingCollection();
                            initMenuClassMealSetColl.Query.Where(
                                initMenuClassMealSetColl.Query.ClassID == mealOrder.ClassID);
                            initMenuClassMealSetColl.LoadAll();
                            if (initMenuClassMealSetColl.Count > 0)
                            {
                                foreach (var x in initMenuClassMealSetColl)
                                {
                                    var mealOrderItemSetting = mealOrderItemSettingColl.AddNew();
                                    mealOrderItemSetting.OrderNo = mealOrder.OrderNo;
                                    mealOrderItemSetting.SRMealSet = x.SRMealSet;
                                    mealOrderItemSetting.IsOptional = x.IsOptional;
                                    mealOrderItemSetting.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                    mealOrderItemSetting.LastUpdateByUserID = AppSession.UserLogin.UserID;

                                    var menuItemFoodColl = new MenuItemFoodCollection();
                                    menuItemFoodColl.Query.Where(
                                        menuItemFoodColl.Query.MenuItemID == mealOrder.MenuItemID,
                                        menuItemFoodColl.Query.IsStandard == true,
                                        menuItemFoodColl.Query.SRMealSet == x.SRMealSet);
                                    menuItemFoodColl.LoadAll();

                                    foreach (var menuItemFood in menuItemFoodColl)
                                    {
                                        var mealOrderItem = mealOrderItemColl.AddNew();

                                        mealOrderItem.OrderNo = mealOrder.OrderNo;
                                        mealOrderItem.SRMealSet = menuItemFood.SRMealSet;
                                        mealOrderItem.FoodID = menuItemFood.FoodID;
                                        mealOrderItem.IsOptional = false;
                                        mealOrderItem.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                        mealOrderItem.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    }
                                }

                                mealOrderItemSettingColl.Save();
                            }
                            else
                            {
                                var initMenuClass = new ClassMenuSetting();
                                bool isOptional = false;
                                if (initMenuClass.LoadByPrimaryKey(mealOrder.ClassID))
                                    isOptional = initMenuClass.IsOptional ?? false;

                                var mealSetColl = new AppStandardReferenceItemCollection();
                                mealSetColl.Query.Where(
                                    mealSetColl.Query.StandardReferenceID == AppEnum.StandardReference.MealSet,
                                    mealSetColl.Query.IsActive == true);
                                mealSetColl.LoadAll();

                                foreach (var mealSet in mealSetColl)
                                {
                                    var mealOrderItemSetting = mealOrderItemSettingColl.AddNew();
                                    mealOrderItemSetting.OrderNo = mealOrder.OrderNo;
                                    mealOrderItemSetting.SRMealSet = mealSet.ItemID;
                                    mealOrderItemSetting.IsOptional = isOptional;
                                    mealOrderItemSetting.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                    mealOrderItemSetting.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                }

                                mealOrderItemSettingColl.Save();

                                var menuItemFoodColl = new MenuItemFoodCollection();
                                menuItemFoodColl.Query.Where(menuItemFoodColl.Query.MenuItemID == mealOrder.MenuItemID,
                                                             menuItemFoodColl.Query.IsStandard == true);
                                menuItemFoodColl.LoadAll();

                                foreach (var menuItemFood in menuItemFoodColl)
                                {
                                    var mealOrderItem = mealOrderItemColl.AddNew();

                                    mealOrderItem.OrderNo = mealOrder.OrderNo;
                                    mealOrderItem.SRMealSet = menuItemFood.SRMealSet;
                                    mealOrderItem.FoodID = menuItemFood.FoodID;
                                    mealOrderItem.IsOptional = false;
                                    mealOrderItem.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                    mealOrderItem.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                }
                            }

                            if (AppSession.Parameter.IsUseStandardMealMenuForAllClass)
                            {
                                //-- menu pilihan ambil dari hari sebelumnya (dg kondisi diet & bentuk makannya sama)
                                var moiBeforeQ = new MealOrderItemQuery("a");
                                var moBeforeQ = new MealOrderQuery("b");
                                var dpBeforeQ = new DietPatientQuery("c");
                                moiBeforeQ.InnerJoin(moBeforeQ).On(moiBeforeQ.OrderNo == moBeforeQ.OrderNo);
                                moiBeforeQ.InnerJoin(dpBeforeQ).On(moBeforeQ.DietPatientNo == dpBeforeQ.TransactionNo);
                                moiBeforeQ.Where
                                    (
                                        moBeforeQ.RegistrationNo == mealOrder.RegistrationNo,
                                        moBeforeQ.EffectiveDate == mealOrder.EffectiveDate.Value.AddDays(-1),
                                        moBeforeQ.IsApproved == 1,
                                        moBeforeQ.DietID == mealOrder.DietID,
                                        dpBeforeQ.FormOfFood == formOfFood,
                                        moiBeforeQ.IsOptional == true
                                    );
                                moiBeforeQ.Select(moiBeforeQ.SRMealSet, moiBeforeQ.FoodID);
                                moiBeforeQ.es.Distinct = true;
                                DataTable moiBeforeDtb = moiBeforeQ.LoadDataTable();
                                if (moiBeforeDtb.Rows.Count > 0)
                                {
                                    foreach (DataRow row in moiBeforeDtb.Rows)
                                    {
                                        var mealOrderItem = mealOrderItemColl.AddNew();

                                        mealOrderItem.OrderNo = mealOrder.OrderNo;
                                        mealOrderItem.SRMealSet = row["SRMealSet"].ToString();
                                        mealOrderItem.FoodID = row["FoodID"].ToString();
                                        mealOrderItem.IsOptional = true;
                                        mealOrderItem.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                        mealOrderItem.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    }
                                }
                            }

                            mealOrderItemColl.Save();
                        }
                    }

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
        }

        private void InsertToMealOrderPlanAndLiquid(string orderNo)
        {
            var mealOrder = new MealOrder();
            mealOrder.LoadByPrimaryKey(orderNo);

            var dp = new DietPatient();
            dp.LoadByPrimaryKey(mealOrder.DietPatientNo);
            var formOfFood = dp.FormOfFood;

            var dpi = new DietPatientItem();
            dpi.LoadByPrimaryKey(mealOrder.DietPatientNo, mealOrder.DietID);

            using (var trans = new esTransactionScope())
            {
                #region order liquid & blenderize
                if (formOfFood == "1" || formOfFood == "7")
                {
                    var dlQ = new DietLiquidPatientQuery();
                    dlQ.es.Top = 1;
                    dlQ.Where(dlQ.RegistrationNo == mealOrder.RegistrationNo,
                              dlQ.EffectiveStartDate <= txtOrderDate.SelectedDate.Value.Date, dlQ.IsVoid == false);
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
                            lq.OrderNo = orderNo;
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
                    dlQ.Where(dlQ.RegistrationNo == mealOrder.RegistrationNo,
                              dlQ.EffectiveStartDate <= txtOrderDate.SelectedDate.Value.Date, dlQ.IsVoid == false);
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
                            lq.OrderNo = orderNo;
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

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void Print(string reportName, string isOptional)
        {
            var jobParameters = new PrintJobParameterCollection();

            var parServiceUnit = jobParameters.AddNew();
            parServiceUnit.Name = "p_ServiceUnitID";
            parServiceUnit.ValueString = cboServiceUnitID.SelectedValue;

            var parOrderDate = jobParameters.AddNew();
            parOrderDate.Name = "p_OrderDate";
            parOrderDate.ValueDateTime = txtOrderDate.SelectedDate ?? Convert.ToDateTime("1900-01-01 00:00:00");

            var parIsOptional = jobParameters.AddNew();
            parIsOptional.Name = "p_IsOptional";
            parIsOptional.ValueString = isOptional;

            var parIsAdditional = jobParameters.AddNew();
            parIsAdditional.Name = "p_IsAdditional";
            parIsAdditional.ValueString = IsAdditional;

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = reportName;

            string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                            "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                            "oWnd.Show();" +
                            "oWnd.Maximize();";
            RadAjaxPanel1.ResponseScripts.Add(script);
        }

        private void Approval(bool isApproved)
        {
            var coll = new MealOrderCollection();
            coll.Query.Where(coll.Query.ServiceUnitID == cboServiceUnitID.SelectedValue,
                             coll.Query.EffectiveDate == txtOrderDate.SelectedDate, coll.Query.IsVoid == false,
                             coll.Query.IsApproved == !isApproved);
            if (Request.QueryString["add"] == "0")
                coll.Query.Where(coll.Query.IsAdditional == false);
            else
                coll.Query.Where(coll.Query.IsAdditional == true);

            coll.LoadAll();

            if (isApproved)
            {
                foreach (var mo in coll)
                {
                    var msg = string.Empty;
                    if (Request.QueryString["add"] == "0" && AppSession.Parameter.IsMealOrderValidationForIncompleteItem)
                        msg = MsgEmptyFood(mo.OrderNo);

                    if (msg.Length > 0)
                    {
                        if (MsgEmptyMainFood.Length == 0)
                            MsgEmptyMainFood = "Meal order incomplete for order no.: " + msg;
                        else
                            MsgEmptyMainFood += "; " + msg;
                    }
                    else
                    {
                        mo.IsApproved = true;
                        mo.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        mo.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    }
                }
            }
            else
            {
                if (AppSession.Parameter.IsUsingMealOrderVerification)
                {
                    foreach (var mo in coll)
                    {
                        var dpc = new DistributionPortionCollection();
                        dpc.Query.Where(dpc.Query.OrderNo == mo.OrderNo);
                        dpc.LoadAll();

                        if (dpc.Count == 0)
                        {
                            mo.IsApproved = false;
                            mo.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            mo.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        }
                    }
                }
                else
                {
                    foreach (var mo in coll)
                    {
                        if (mo.IsVerified == false)
                        {
                            mo.IsApproved = false;
                            mo.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            mo.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        }
                    }
                }

            }
            using (var trans = new esTransactionScope())
            {
                coll.Save();
                trans.Complete();
            }

            //-- meal order plan & liquid
            if (isApproved)
            {
                foreach (var mo in coll)
                {
                    InsertToMealOrderPlanAndLiquid(mo.OrderNo);
                }
            }
            else
            {
                foreach (var mo in coll)
                {
                    var lqColl = new MealOrderItemLiquidCollection();
                    lqColl.Query.Where(lqColl.Query.OrderNo == mo.OrderNo);
                    lqColl.LoadAll();
                    lqColl.MarkAllAsDeleted();
                    lqColl.Save();
                }
            }
        }

        private string MsgEmptyFood(string transNo)
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
                    query.OrderNo == transNo
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
            if (retValue.Length != 0)
                retValue = transNo + "(" + retValue + ")";
            return retValue;
        }

        private void Void()
        {
            var coll = new MealOrderCollection();
            coll.Query.Where(coll.Query.ServiceUnitID == cboServiceUnitID.SelectedValue,
                             coll.Query.EffectiveDate == txtOrderDate.SelectedDate,
                             coll.Query.IsVoid == false, coll.Query.IsApproved == false);
            if (Request.QueryString["add"] == "0")
                coll.Query.Where(coll.Query.IsAdditional == false);
            else
                coll.Query.Where(coll.Query.IsAdditional == true);
            coll.LoadAll();

            foreach (var mo in coll)
            {
                mo.IsVoid = true;
                mo.LastUpdateByUserID = AppSession.UserLogin.UserID;
                mo.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            using (var trans = new esTransactionScope())
            {
                coll.Save();
                trans.Complete();
            }
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        {
            if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && txtOrderDate.IsEmpty)
            {
                pnlInfo.Visible = true;
                lblInfo.Text = "Service Unit and Order To Date required.";
                grdList.Rebind();
                return;
            }

            if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                pnlInfo.Visible = true;
                lblInfo.Text = "Service Unit required.";
                grdList.Rebind();
                return;
            }

            if (txtOrderDate.IsEmpty)
            {
                pnlInfo.Visible = true;
                lblInfo.Text = "Order To Date required.";
                grdList.Rebind();
                return;
            }

            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;

            pnlInfo.Visible = false;

            switch (eventArgument)
            {
                case "rebind":
                    grdList.Rebind();

                    break;

                case "process":
                    pnlInfo.Visible = false;
                    Validate();
                    if (!IsValid)
                    {
                        pnlInfo.Visible = true;
                        lblInfo.Text = "Data is not valid.";
                        grdList.Rebind();
                        return;
                    }

                    

                    MsgEmptyMenu = string.Empty;
                    Process();

                    if (MsgEmptyMenu.Length > 0)
                    {
                        pnlInfo.Visible = true;
                        lblInfo.Text = MsgEmptyMenu + ". Please contact the administrator.";
                        grdList.Rebind();
                        return;
                    }

                    grdList.Rebind();

                    break;

                case "printo":
                    if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) || txtOrderDate.IsEmpty)
                        return;

                    Print(AppConstant.Report.MealOrderOptionalMenuSlip, "1");

                    break;

                case "prints":
                    if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) || txtOrderDate.IsEmpty)
                        return;

                    Print(AppConstant.Report.MealOrderStandardMenuSlip, "0");

                    break;

                case "approved":
                    pnlInfo.Visible = false;
                    Validate();
                    if (!IsValid)
                    {
                        pnlInfo.Visible = true;
                        lblInfo.Text = "Data is not valid.";
                        grdList.Rebind();
                        return;
                    }

                    if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) || txtOrderDate.IsEmpty)
                    {
                        pnlInfo.Visible = true;
                        lblInfo.Text = "Service Unit and Order To Date required.";
                        grdList.Rebind();
                        return;
                    }

                    MsgEmptyMainFood = string.Empty;
                    Approval(true);

                    if (MsgEmptyMainFood.Length > 0)
                    {
                        pnlInfo.Visible = true;
                        lblInfo.Text = MsgEmptyMainFood + ". Please check back your meal order.";
                        grdList.Rebind();
                        return;
                    }
                    grdList.Rebind();

                    break;

                case "unapproved":
                    pnlInfo.Visible = false;
                    Validate();
                    if (!IsValid)
                    {
                        pnlInfo.Visible = true;
                        lblInfo.Text = "Data is not valid.";
                        grdList.Rebind();
                        return;
                    }

                    if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) || txtOrderDate.IsEmpty)
                    {
                        pnlInfo.Visible = true;
                        lblInfo.Text = "Service Unit and Order To Date required.";
                        grdList.Rebind();
                        return;
                    }

                    Approval(false);
                    grdList.Rebind();

                    break;

                case "void":
                    pnlInfo.Visible = false;
                    Validate();
                    if (!IsValid)
                    {
                        pnlInfo.Visible = true;
                        lblInfo.Text = "Data is not valid.";
                        grdList.Rebind();
                        return;
                    }

                    if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) || txtOrderDate.IsEmpty)
                    {
                        pnlInfo.Visible = true;
                        lblInfo.Text = "Service Unit and Order To Date required.";
                        grdList.Rebind();
                        return;
                    }

                    Void();
                    grdList.Rebind();

                    break;
            }
            if (eventArgument.Contains("|"))
            {
                var param = eventArgument.Split('|');

                var mo = new MealOrder();
                if (mo.LoadByPrimaryKey(param[1]))
                {
                    mo.IsVoid = true;
                    mo.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    mo.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    mo.Save();
                    grdList.Rebind();
                }
            }
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "neworder")
            {
                pnlInfo.Visible = false;

                MsgEmptyMenu = string.Empty;

                var regNo = e.CommandArgument.ToString();

                var dpcoll = new DietPatientCollection();
                var dpq = new DietPatientQuery();
                dpq.Where(dpq.RegistrationNo == regNo, dpq.EffectiveStartDate.Date() <= txtOrderDate.SelectedDate,
                          dpq.IsVoid == false);
                dpq.OrderBy(dpq.EffectiveStartDate.Descending, dpq.EffectiveStartTime.Descending);
                dpq.es.Top = 1;
                dpcoll.Load(dpq);

                if (dpcoll.Count > 0)
                {
                    foreach (var dp in dpcoll)
                    {
                        var dpic = new DietPatientItemCollection();
                        var dpiq = new DietPatientItemQuery("a");
                        var dmq = new DietMenuQuery("b");
                        dpiq.InnerJoin(dmq).On(dmq.DietID == dpiq.DietID && dmq.FormOfFood == dp.FormOfFood);
                        dpiq.Where(dpiq.TransactionNo == dp.TransactionNo);
                        dpic.Load(dpiq);

                        foreach (var dpi in dpic)
                        {
                            InsertToMealOrder(regNo, dp.TransactionNo, dpi.DietID, dp.FormOfFood);
                        }
                    }

                    if (MsgEmptyMenu.Length > 0)
                    {
                        pnlInfo.Visible = true;
                        lblInfo.Text = MsgEmptyMenu + ". Please contact the administrator.";
                        grdList.Rebind();
                        return;
                    }
                }
                else
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = "Diet Patients for selected patient required.";
                }

                grdList.Rebind();
            }
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var tooltip = string.Empty;
                var dataItem = e.Item as GridDataItem;
                if (dataItem.OwnerTableView.Name == "master")
                {
                    if (dataItem["ServiceUnitID"].Text != dataItem["ServiceUnitOrderID"].Text)
                    {
                        // Beri warna merah jika CoverageClassID berbeda dg ChargeClassID
                        dataItem.ForeColor = Color.Red;
                        dataItem.Font.Bold = true;
                        tooltip = "Patient service unit is different from the meal order service unit.";
                    }
                    if (dataItem["ClassID"].Text != dataItem["ClassOrderID"].Text)
                    {
                        dataItem.ForeColor = Color.Red;
                        dataItem.Font.Bold = true;
                        dataItem.Font.Italic = true;
                        tooltip = tooltip == string.Empty ? "Patient class is different from the meal order class." : "Patient service unit & class is different from the meal order service unit & class.";
                    }

                    dataItem.ToolTip = tooltip;
                }
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string orderno = dataItem.GetDataKeyValue("OrderNo").ToString();

            //Load record
            var query = new MealOrderQuery("a");
            var su = new ServiceUnitQuery("b");
            var bed = new BedQuery("c");
            var sr = new ServiceRoomQuery("d");
            var cl = new ClassQuery("e");

            query.InnerJoin(su).On(query.ServiceUnitID == su.ServiceUnitID);
            query.InnerJoin(bed).On(query.BedID == bed.BedID);
            query.InnerJoin(sr).On(bed.RoomID == sr.RoomID);
            query.InnerJoin(cl).On(query.ClassID == cl.ClassID);

            query.Select(
                query.OrderNo, su.ServiceUnitName, sr.RoomName, query.BedID, cl.ClassName);
            query.Where(query.OrderNo == orderno);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }
    }
}
