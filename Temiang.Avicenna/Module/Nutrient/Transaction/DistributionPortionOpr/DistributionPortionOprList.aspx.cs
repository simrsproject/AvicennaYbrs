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
    public partial class DistributionPortionOprList : BasePage
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

            ProgramID = AppConstant.Program.DistributionPortionOutpatient;

            if (!IsPostBack)
            {
                //service unit
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");

                query.Where(
                    query.SRRegistrationType.In(AppConstant.RegistrationType.OutPatient, AppConstant.RegistrationType.EmergencyPatient, AppConstant.RegistrationType.MedicalCheckUp),
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
                    grdList.Columns[grdList.Columns.Count - 1].Visible = true;
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
                var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Distribution Portion Outpatient")) return null;

                var query = new MealOrderQuery("a");
                var reg = new RegistrationQuery("b");
                var patient = new PatientQuery("c");
                var unit = new ServiceUnitQuery("f");
                var distPortion = new DistributionPortionQuery("j");
                var sal = new AppStandardReferenceItemQuery("sal");
                var guar = new GuarantorQuery("g");
                var par = new ParamedicQuery("p");

                query.Select
                    (
                        query.OrderNo,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        reg.ServiceUnitID,
                        unit.ServiceUnitName,
                        @"<CASE WHEN j.OrderNo IS NOT NULL AND j.IsVoid = 0 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS 'IsDistributed'>",
                        @"<ISNULL(j.IsVoid, 0) AS 'IsVoid'>",
                        @"<CAST(b.AgeInYear AS VARCHAR) + 'y ' + CAST(b.AgeInMonth AS VARCHAR) + 'm ' + CAST(b.AgeInDay AS VARCHAR) + 'd' AS Age>",
                        sal.ItemName.As("SalutationName"),
                        guar.GuarantorName,
                        par.ParamedicName
                    );

                query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                query.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(distPortion).On(query.OrderNo == distPortion.OrderNo &&
                                               distPortion.SRMealSet == cboSRMealSet.SelectedValue);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
                query.InnerJoin(guar).On(guar.GuarantorID == reg.GuarantorID);
                query.InnerJoin(par).On(par.ParamedicID == reg.ParamedicID);

                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    query.Where(reg.ServiceUnitID == cboServiceUnitID.SelectedValue);

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

                query.Where(query.IsApproved == true, query.EffectiveDate == txtOrderDate.SelectedDate, query.IsOpr == true,
                            reg.IsClosed == false);

                query.OrderBy(unit.ServiceUnitName.Ascending);

                DataTable dtb = query.LoadDataTable();

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

                    var entity = dpColl.AddNew();
                    entity.OrderNo = orderNo;
                    entity.SRMealSet = cboSRMealSet.SelectedValue;
                    entity.IsVoid = false;
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }

                dpColl.Save();
                trans.Complete();
            }
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

                    if (string.IsNullOrEmpty(cboSRMealSet.SelectedValue) || txtOrderDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                        return;

                    Process();

                    grdList.Rebind();

                    break;

                case "print":
                    if (txtOrderDate.SelectedDate.ToString().Trim().Equals(string.Empty) || string.IsNullOrEmpty(cboSRMealSet.SelectedValue))
                        return;

                    Print(AppConstant.Report.DistributionPortionOprSlip, "1", string.Empty);

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
                            {
                                entity.AddNew();

                                entity.OrderNo = orderNo;
                                entity.SRMealSet = cboSRMealSet.SelectedValue;
                                entity.IsVoid = false;
                                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                                entity.Save();
                            }
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
                Print(AppConstant.Report.DistributionPortionOprSlip, "0", e.CommandArgument.ToString());
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
    }
}