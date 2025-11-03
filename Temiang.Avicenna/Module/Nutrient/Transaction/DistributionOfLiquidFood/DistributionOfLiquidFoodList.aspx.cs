using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class DistributionOfLiquidFoodList : BasePage
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

            ProgramID = AppConstant.Program.DistributionOfLiquidFood;

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

                txtOrderDate.SelectedDate = (new DateTime()).NowAtSqlServer();
                StandardReference.InitializeIncludeSpace(cboDietTime, AppEnum.StandardReference.DietLiquidTime);
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
                if (!ValidateSearch(isEmptyFilter, "Distribution Liquid Food")) return null;

                var query = new MealOrderQuery("a");
                var reg = new RegistrationQuery("b");
                var patient = new PatientQuery("c");
                var bed = new BedQuery("d");
                var room = new ServiceRoomQuery("e");
                var unit = new ServiceUnitQuery("f");
                var diet = new DietQuery("g");
                var menu = new MenuQuery("h");
                var moil = new MealOrderItemLiquidQuery("i");
                var food = new FoodQuery("j");
                var sal = new AppStandardReferenceItemQuery("sal");

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        query.OrderNo,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        room.RoomName,
                        query.BedID,
                        query.ServiceUnitID,
                        unit.ServiceUnitName,
                        query.FastingTime,
                        diet.DietName,
                        food.FoodName,
                        moil.IsDistributed,
                        moil.IsVoidDistributed.As("IsVoid"),
                        @"<CAST(b.AgeInYear AS VARCHAR) + 'y ' + CAST(b.AgeInMonth AS VARCHAR) + 'm ' + CAST(b.AgeInDay AS VARCHAR) + 'd' AS Age>",
                        sal.ItemName.As("SalutationName")
                    );

                query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                query.InnerJoin(bed).On(query.BedID == bed.BedID);
                query.LeftJoin(room).On(bed.RoomID == room.RoomID);
                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                query.InnerJoin(diet).On(query.DietID == diet.DietID);
                query.InnerJoin(menu).On(query.MenuID == menu.MenuID);
                query.InnerJoin(moil).On(query.OrderNo == moil.OrderNo && moil.MealTime == cboDietTime.SelectedValue);
                query.InnerJoin(food).On(moil.FoodID == food.FoodID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

                query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue, reg.DischargeDate.IsNull());

                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    query.Where(
                        query.Or(
                            query.RegistrationNo == searchReg,
                            patient.MedicalNo == searchReg,
                            patient.OldMedicalNo == searchReg,
                            string.Format("< OR REPLACE(c.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                            string.Format("< OR REPLACE(c.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
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

                query.Where(query.IsApproved == true, query.EffectiveDate == txtOrderDate.SelectedDate);
                if (AppSession.Parameter.IsUsingMealOrderVerification)
                    query.Where(query.IsVerified == true);

                query.OrderBy(unit.ServiceUnitName.Ascending, query.BedID.Ascending);

                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void cboDietTime_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            grdList.Rebind();
        }

        private void Process()
        {
            using (var trans = new esTransactionScope())
            {
                foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
                {
                    string orderNo = dataItem.GetDataKeyValue("OrderNo").ToString();

                    var moil = new MealOrderItemLiquid();
                    if (moil.LoadByPrimaryKey(cboDietTime.SelectedValue, orderNo))
                    {
                        moil.IsDistributed = true;
                        moil.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        moil.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        moil.Save();
                    }
                }
                trans.Complete();
            }
        }

        private void PrintLiquid(string reportName, string orderNo)
        {
            var jobParameters = new PrintJobParameterCollection();

            var parServiceUnit = jobParameters.AddNew();
            parServiceUnit.Name = "p_ServiceUnitID";
            parServiceUnit.ValueString = cboServiceUnitID.SelectedValue;

            var parOrderDate = jobParameters.AddNew();
            parOrderDate.Name = "p_OrderDate";
            parOrderDate.ValueDateTime = txtOrderDate.SelectedDate ?? Convert.ToDateTime("1900-01-01 00:00:00");

            var parTime = jobParameters.AddNew();
            parTime.Name = "p_OrderTime";
            parTime.ValueString = cboDietTime.SelectedValue;

            var parOrderNo = jobParameters.AddNew();
            parOrderNo.Name = "p_OrderNo";
            parOrderNo.ValueString = orderNo;

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

                    if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) || txtOrderDate.SelectedDate.ToString().Trim().Equals(string.Empty) || string.IsNullOrEmpty(cboDietTime.SelectedValue))
                        return;

                    Process();

                    grdList.Rebind();

                    break;

                case "print":
                    if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) || txtOrderDate.SelectedDate.ToString().Trim().Equals(string.Empty) || string.IsNullOrEmpty(cboDietTime.SelectedValue))
                        return;

                    PrintLiquid(AppConstant.Report.DistributionLiquidMenuSlip, string.Empty);

                    break;

                default:

                    if (eventArgument.Contains("|"))
                    {
                        var param = eventArgument.Split('|');
                        string orderNo = param[1];
                        var mealOrder = new MealOrder();
                        if (mealOrder.LoadByPrimaryKey(orderNo))
                        {
                            var entity = new MealOrderItemLiquid();
                            if (entity.LoadByPrimaryKey(cboDietTime.SelectedValue, orderNo))
                            {
                                entity.IsVoidDistributed = true;
                                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            }

                            entity.Save();
                        }

                        grdList.Rebind();
                    }

                    break;
            }
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "distributed")
            {
                var orderNo = e.CommandArgument.ToString();
                var mealOrder = new MealOrder();
                if (mealOrder.LoadByPrimaryKey(orderNo))
                {
                    var entity = new MealOrderItemLiquid();
                    if (entity.LoadByPrimaryKey(cboDietTime.SelectedValue, orderNo))
                    {
                        entity.IsDistributed = true;
                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                        entity.Save();
                    }
                }

                grdList.Rebind();
            }
            else if (e.CommandName == "print")
            {
                PrintLiquid(AppConstant.Report.DistributionLiquidMenuSlip, e.CommandArgument.ToString());
            }
        }
    }
}
