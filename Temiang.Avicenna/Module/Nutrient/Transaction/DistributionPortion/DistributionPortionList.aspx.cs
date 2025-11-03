using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class DistributionPortionList : BasePage
    {
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

            ProgramID = AppConstant.Program.DistributionPortion;

            if (!IsPostBack)
            {
                //service unit
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");
                
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
                    cboServiceUnitID.SelectedValue = Request.QueryString["unitid"].ToString();

                txtOrderDate.SelectedDate = (new DateTime()).NowAtSqlServer();
                StandardReference.Initialize(cboSRMealSet, AppEnum.StandardReference.MealSet, true);
                if (!string.IsNullOrEmpty(Request.QueryString["mealset"]))
                    cboSRMealSet.SelectedValue = Request.QueryString["mealset"].ToString();

                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSA")
                {
                    RadToolBar2.Items[2].Visible = false;
                }

                grdList.Columns.FindByUniqueName("CheckBoxTemplateColumn").Visible = AppSession.Parameter.IsCheckallDistributedPrint;
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
                var isEmptyFilter = string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Distribution Portion")) return null;

                var query = new MealOrderQuery("a");
                var reg = new RegistrationQuery("b");
                var patient = new PatientQuery("c");
                var bed = new BedQuery("d");
                var room = new ServiceRoomQuery("e");
                var unit = new ServiceUnitQuery("f");
                var diet = new DietQuery("g");
                var menu = new MenuQuery("h");
                var distPortion = new DistributionPortionQuery("j");
                var sal = new AppStandardReferenceItemQuery("sal");
                var atectl = new AtePatientsControlQuery("k");

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        query.OrderNo,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        room.RoomName,
                        reg.BedID,
                        reg.ServiceUnitID,
                        unit.ServiceUnitName,
                        query.FastingTime,
                        menu.MenuName,
                        query.DietID,
                        diet.DietName,
                        @"<'' AS DietComplicationName>",
                        @"<CASE WHEN j.OrderNo IS NOT NULL AND j.IsVoid = 0 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS 'IsDistributed'>",
                        @"<ISNULL(j.IsVoid, 0) AS 'IsVoid'>",
                        @"<CAST(b.AgeInYear AS VARCHAR) + 'y ' + CAST(b.AgeInMonth AS VARCHAR) + 'm ' + CAST(b.AgeInDay AS VARCHAR) + 'd' AS Age>",
                        sal.ItemName.As("SalutationName"), 
                        query.FastingTime,
                        query.DietPatientNo,
                        @"<CAST(0 AS BIT) AS IsDietChanged>",
                        @"<CAST(0 AS BIT) AS IsBeenProcessed>",
                        @"<CASE WHEN j.IsInvalid IS NOT NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS 'IsCheckInvalid'>",
                        @"<CASE WHEN k.OrderNo IS NOT NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS 'IsCheckAteCtl'>"
                    );

                query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                query.InnerJoin(bed).On(reg.BedID == bed.BedID);
                query.LeftJoin(room).On(reg.RoomID == room.RoomID);
                query.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
                query.InnerJoin(diet).On(query.DietID == diet.DietID);
                query.InnerJoin(menu).On(query.MenuID == menu.MenuID);
                query.LeftJoin(distPortion).On(query.OrderNo == distPortion.OrderNo &&
                                               distPortion.SRMealSet == cboSRMealSet.SelectedValue);
                query.LeftJoin(atectl).On(atectl.OrderNo == query.OrderNo && atectl.SRMealSet == cboSRMealSet.SelectedValue);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    if (AppSession.Parameter.IsMedicalNoContainStrip)
                        query.Where(
                            query.Or(
                                query.RegistrationNo == searchReg,
                                patient.MedicalNo == searchReg,
                                patient.OldMedicalNo == searchReg,
                                string.Format("< OR REPLACE(c.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                                string.Format("< OR REPLACE(c.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                                )
                            );
                    else
                        query.Where(
                            query.Or(
                                query.RegistrationNo == searchReg,
                                patient.MedicalNo == searchReg,
                                patient.OldMedicalNo == searchReg,
                                string.Format("< OR c.MedicalNo LIKE '%{0}%'>", searchReg),
                                string.Format("< OR c.OldMedicalNo LIKE '%{0}%'>", searchReg)
                                )
                            );
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + txtPatientName.Text + "%";
                    query.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(c.FirstName + ' ' + c.MiddleName)) + ' ' + c.LastName) LIKE '{0}'>", searchPatient)
                        );
                }

                query.Where(reg.ServiceUnitID == cboServiceUnitID.SelectedValue, reg.DischargeDate.IsNull());
                query.Where(query.IsApproved == true, query.EffectiveDate == txtOrderDate.SelectedDate, query.IsOpr == false,
                            reg.IsClosed == false, reg.DischargeDate.IsNull());
                if (AppSession.Parameter.IsUsingMealOrderVerification)
                    query.Where(query.IsVerified == true);

                query.OrderBy(unit.ServiceUnitName.Ascending, query.BedID.Ascending);

                DataTable dtb = query.LoadDataTable();

                foreach (DataRow row in dtb.Rows)
                {
                    bool isFasting = false;
                    if (!string.IsNullOrEmpty(row["FastingTime"].ToString()))
                    {
                        
                        switch (cboSRMealSet.SelectedValue)
                        {
                            case "01": //pagi
                                isFasting = row["FastingTime"].ToString().Contains("1");
                                break;
                            case "02": //siang
                                isFasting = row["FastingTime"].ToString().Contains("2");
                                break;
                            case "03": //sore
                                isFasting = row["FastingTime"].ToString().Contains("3");
                                break;
                        }
                        
                    }
                    if (isFasting)
                        row.Delete();
                    else
                    {
                        var dpcoll = new DietPatientCollection();
                        var dpq = new DietPatientQuery();
                        dpq.Where(dpq.RegistrationNo == row["RegistrationNo"].ToString(),
                            dpq.EffectiveStartDate.Date() <= txtOrderDate.SelectedDate,
                            dpq.IsVoid == false);
                        dpq.OrderBy(dpq.EffectiveStartDate.Descending, dpq.EffectiveStartTime.Descending, dpq.TransactionNo.Descending);
                        dpq.es.Top = 1;
                        dpcoll.Load(dpq);
                        if (dpcoll.Count > 0)
                        {
                            string dietName = string.Empty;
                            var i = 0;

                            foreach (var dp in dpcoll)
                            {
                                var oldDiet = row["DietName"].ToString();
                                if (dp.TransactionNo == row["DietPatientNo"].ToString()) continue;
                                row["IsDietChanged"] = true;

                                var dpicoll = new DietPatientItemCollection();
                                dpicoll.Query.Where(dpicoll.Query.TransactionNo == dp.TransactionNo);
                                dpicoll.LoadAll();
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
                                row["DietName"] = dietName + " [Prev. Diet: " + oldDiet + "]";
                            }
                        }

                        var moicoll = new MealOrderItemCollection();
                        moicoll.Query.Where(moicoll.Query.OrderNo == row["OrderNo"].ToString(), moicoll.Query.SRMealSet == cboSRMealSet.SelectedValue);
                        moicoll.Query.es.Top = 1;
                        moicoll.LoadAll();

                        var dietDetailId = string.Empty;
                        var menuDetailName = string.Empty;
                        foreach (var moi in moicoll)
                        {
                            if (!string.IsNullOrEmpty(moi.DietID))
                                dietDetailId = moi.DietID;
                            if (!string.IsNullOrEmpty(moi.MenuID))
                            {
                                var m = new BusinessObject.Menu();
                                if (m.LoadByPrimaryKey(moi.MenuID))
                                    menuDetailName = m.MenuName;
                            }
                        }

                        if (dietDetailId != string.Empty)
                            row["IsBeenProcessed"] = true;
                        if (menuDetailName != string.Empty)
                        {
                            var oldMenu = row["MenuName"].ToString();
                            row["MenuName"] = menuDetailName+ " [Prev. Menu: " + oldMenu + "]"; ;
                        }

                        var dcpq = new DietComplicationPatientQuery("dc");
                        var dq = new DietQuery("d");
                        dcpq.InnerJoin(dq).On(dq.DietID == dcpq.DietComplicationID);
                        dcpq.Where(dcpq.TransactionNo == row["DietPatientNo"].ToString(), dcpq.DietID == row["DietID"].ToString());
                        dcpq.Select(dcpq.DietComplicationID, dq.DietName);
                        var dcpdtb = dcpq.LoadDataTable();

                        foreach (DataRow r in dcpdtb.Rows)
                        {
                            if (row["DietComplicationName"].ToString() == string.Empty)
                            {
                                row["DietComplicationName"] = r["DietName"].ToString();
                            }
                            else {
                                row["DietComplicationName"] = row["DietComplicationName"].ToString() + ", " + r["DietName"].ToString();
                            }
                        }
                    }
                }

                dtb.AcceptChanges();

                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void cboSRMealSet_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            grdList.Rebind();
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            grdList.Rebind();
        }

        private void Process()
        {
            var dpColl = new DistributionPortionCollection();

            using (var trans = new esTransactionScope())
            {
                foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
                {
                    string orderNo = dataItem.GetDataKeyValue("OrderNo").ToString();

                    var dp = new DistributionPortion();
                    if (!dp.LoadByPrimaryKey(orderNo, cboSRMealSet.SelectedValue))
                    {
                        var entity = dpColl.AddNew();
                        entity.OrderNo = orderNo;
                        entity.SRMealSet = cboSRMealSet.SelectedValue;
                        entity.IsVoid = false;
                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    }
                }

                dpColl.Save();
                trans.Complete();
            }
        }


        private string[] OrderList()
        {
            int i = grdList.MasterTableView.Items.Cast<GridDataItem>().Count(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked);
            var arr = new string[i];

            var idx = 0;
            foreach (GridDataItem dataItem in grdList.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked))
            {
                arr.SetValue(dataItem.GetDataKeyValue("OrderNo"), idx);
                idx++;
            }

            return arr;
        }

        private void Print(string reportName, string isOptional, string orderNo)
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

            var parSrMealSet = jobParameters.AddNew();
            parSrMealSet.Name = "p_SRMealSet";
            parSrMealSet.ValueString = cboSRMealSet.SelectedValue;


            if (!AppSession.Parameter.IsCheckallDistributedPrint)
            {
                var parOrderNo = jobParameters.AddNew();
                parOrderNo.Name = "p_OrderNo";
                parOrderNo.ValueString = orderNo;
            }
            else
            {
                var jobParameter = jobParameters.AddNew();
                jobParameter.Name = "p_OrderNo";
                jobParameter.ValueString = string.Empty;


                string[] OrderNoList = OrderList();
                foreach (var str in OrderNoList)
                {
                    jobParameter.ValueString += str + ",";
                }

                if (jobParameter.ValueString != string.Empty)
                    jobParameter.ValueString = jobParameter.ValueString.Substring(0, jobParameter.ValueString.Length - 1);
            }


            //jobParameters.AddNew("p_OrderNo", param);


            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = reportName;

            string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                            "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                            "oWnd.Show();" +
                            "oWnd.Maximize();";
            RadAjaxPanel1.ResponseScripts.Add(script);
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;

            switch (eventArgument)
            {
                case "rebind":
                    grdList.Rebind();

                    break;

                case "process":
                    Validate();
                    if (!IsValid)
                        return;

                    if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) || txtOrderDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                        return;

                    Process();

                    grdList.Rebind();

                    break;

                case "printo":
                    if (AppSession.Parameter.HealthcareInitialAppsVersion != "RSMM")
                    {
                        if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) || txtOrderDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                            return;
                    }
                    else if (txtOrderDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                        return;

                    Print(AppConstant.Report.DistributionOptionalMenuSlip, "1", string.Empty);

                    break;

                case "prints":
                    if (AppSession.Parameter.HealthcareInitialAppsVersion != "RSMM")
                    {
                        if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) || txtOrderDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                            return;
                    }
                    else if (txtOrderDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                        return; 

                    Print(AppConstant.Report.DistributionStandardMenuSlip, "0", string.Empty);

                    break;

                default:
                    if (eventArgument.Contains("distributed|"))
                    {
                        var param = eventArgument.Split('|');
                        string orderNo = param[1];
                        var mealOrder = new MealOrder();
                        if (mealOrder.LoadByPrimaryKey(orderNo))
                        {
                            var entity = new DistributionPortion();
                            if (!entity.LoadByPrimaryKey(orderNo, cboSRMealSet.SelectedValue))
                                entity.AddNew();
                            
                            entity.OrderNo = orderNo;
                            entity.SRMealSet = cboSRMealSet.SelectedValue;
                            entity.IsVoid = false;
                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            entity.Save();
                        }

                        grdList.Rebind();
                    }
                    else if (eventArgument.Contains("void|"))
                    {
                        var param = eventArgument.Split('|');
                        string orderNo = param[1];
                        var mealOrder = new MealOrder();
                        if (mealOrder.LoadByPrimaryKey(orderNo))
                        {
                            var entity = new DistributionPortion();
                            if (!entity.LoadByPrimaryKey(orderNo, cboSRMealSet.SelectedValue))
                                entity.AddNew();
                           
                            entity.OrderNo = orderNo;
                            entity.SRMealSet = cboSRMealSet.SelectedValue;
                            entity.IsVoid = true;
                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                            entity.Save();
                        }

                        grdList.Rebind();
                    }

                    break;
            }
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "print")
            {
                Print(AppConstant.Report.DistributionOptionalMenuSlip, "0", e.CommandArgument.ToString());
            }
            else if (e.CommandName == "distributed")
            {
                var orderNo = e.CommandArgument.ToString();
                var mealOrder = new MealOrder();
                if (mealOrder.LoadByPrimaryKey(orderNo))
                {
                    var entity = new DistributionPortion();
                    if (!entity.LoadByPrimaryKey(orderNo, cboSRMealSet.SelectedValue))
                        entity.AddNew();

                    entity.OrderNo = orderNo;
                    entity.SRMealSet = cboSRMealSet.SelectedValue;
                    entity.IsVoid = false;
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    entity.Save();
                }

                grdList.Rebind();
            }
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            bool selected = ((CheckBox)sender).Checked;
            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }
    }
}
