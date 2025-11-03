using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class MealOrderVerificationList : BasePage
    {
        private int _total;

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.MealOrderVerification;

            if (!IsPostBack)
            {
                //service unit
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");
                var qusr = new AppUserServiceUnitQuery("u");
                query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                query.Where(qusr.UserID == AppSession.UserLogin.UserID);

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
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = MealOrders;
            txtTotal.Text = _total.ToString();
        }

        private DataTable MealOrders
        {
            get
            {
                var query = new MealOrderQuery("a");
                var reg = new RegistrationQuery("b");
                var patient = new PatientQuery("c");
                var unit = new ServiceUnitQuery("d");
                var bed = new BedQuery("e");
                var room = new ServiceRoomQuery("f");
                var cls = new ClassQuery("g");
                var dietPat = new DietPatientQuery("h");
                var dietPatItem = new DietPatientItemQuery("i");
                var diet = new DietQuery("j");
                var qusr = new AppUserServiceUnitQuery("k");
                var fof = new AppStandardReferenceItemQuery("l");

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        unit.ServiceUnitName,
                        query.OrderNo,
                        query.RegistrationNo,
                        reg.BedID,
                        cls.ClassName,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        dietPat.Diagnose,
                        diet.DietName,
                        fof.ItemName.As("FormOfFood"),
                        @"<ISNULL((SELECT TOP 1 f.FoodName 
                            FROM MealOrderItem moi 
                            INNER JOIN Food f ON f.FoodID = moi.FoodID 
                                AND moi.SRMealSet = '01' 
                            WHERE moi.OrderNo = a.OrderNo AND f.SRFoodGroup1 IN ('I', 'VIII')), '-') AS Breakfast>",
                        @"<ISNULL((SELECT TOP 1 f.FoodName 
                            FROM MealOrderItem moi 
                            INNER JOIN Food f ON f.FoodID = moi.FoodID 
                                AND moi.SRMealSet = '02' 
                            WHERE moi.OrderNo = a.OrderNo AND f.SRFoodGroup1 IN ('I', 'VIII')), '-') Lunch>",
                        @"<ISNULL((SELECT TOP 1 f.FoodName 
                            FROM MealOrderItem moi 
                            INNER JOIN Food f ON f.FoodID = moi.FoodID 
                                AND moi.SRMealSet = '03' 
                            WHERE moi.OrderNo = a.OrderNo AND f.SRFoodGroup1 IN ('I', 'VIII')), '-') AS Dinner>",
                        dietPatItem.ExtraQty,
                        dietPat.Notes,
                        query.IsVerified,
                        query.DietPatientNo,
                        @"<CASE WHEN b.DischargeDate IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsDischarge>",
                        reg.DischargeDate
                    );

                query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                query.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
                query.InnerJoin(bed).On(reg.BedID == bed.BedID);
                query.LeftJoin(room).On(bed.RoomID == room.RoomID);
                query.InnerJoin(cls).On(reg.ChargeClassID == cls.ClassID);
                query.InnerJoin(dietPat).On(query.DietPatientNo == dietPat.TransactionNo);
                query.InnerJoin(dietPatItem).On(query.DietPatientNo == dietPatItem.TransactionNo & query.DietID == dietPatItem.DietID);
                query.InnerJoin(diet).On(dietPatItem.DietID == diet.DietID);
                query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID && qusr.UserID == AppSession.UserLogin.UserID);
                query.InnerJoin(fof).On(dietPat.FormOfFood == fof.ItemID &&
                                    fof.StandardReferenceID == AppEnum.StandardReference.FormOfFood);

                query.Where
                    (
                        query.EffectiveDate == txtOrderDate.SelectedDate,
                        query.IsApproved == true,
                        reg.ServiceUnitID == cboServiceUnitID.SelectedValue
                    );

                query.OrderBy(reg.BedID.Ascending);

                DataTable dtb = query.LoadDataTable();
                _total = dtb.Rows.Count;

                foreach (DataRow d in dtb.Rows)
                {
                    var x = false;
                    if (Convert.ToBoolean(d["IsDischarge"]))
                    {
                        if (Convert.ToDateTime(d["DischargeDate"]).Date <= txtOrderDate.SelectedDate.Value.Date)
                        {
                            d.Delete();
                            _total = _total - 1;
                        }
                        else
                            x = true;
                    }
                    else
                        x = true;

                    if (x)
                    {
                        var dcomps = new DietComplicationPatientCollection();
                        dcomps.Query.Where(dcomps.Query.TransactionNo == d["DietPatientNo"]);
                        dcomps.LoadAll();

                        var dietCompName = string.Empty;
                        var i = 0;

                        foreach (var dc in dcomps)
                        {
                            var di = new Diet();
                            if (di.LoadByPrimaryKey(dc.DietComplicationID))
                            {
                                if (i > 0)
                                    dietCompName += ", ";

                                dietCompName += di.DietName;
                            }

                            i += 1;
                        }

                        if (!string.IsNullOrEmpty(dietCompName))
                            d["DietName"] = d["DietName"] + " (with Comp: " + dietCompName + ")";
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

        private void Approval(bool isApproved)
        {
            var coll = new MealOrderCollection();
            var query = new MealOrderQuery("a");
            var reg = new RegistrationQuery("b");
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.Where(reg.ServiceUnitID == cboServiceUnitID.SelectedValue,
                        query.EffectiveDate == txtOrderDate.SelectedDate, query.IsApproved == true,
                        query.IsVerified == !isApproved);
            coll.Load(query);

            if (isApproved)
            {
                foreach (var mo in coll)
                {
                    mo.IsVerified = true;
                    mo.VerifiedByUserID = AppSession.UserLogin.UserID;
                    mo.VerifiedDateTime = (new DateTime()).NowAtSqlServer();
                    mo.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    mo.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }
            else
            {
                foreach (var mo in coll)
                {
                    var dpc = new DistributionPortionCollection();
                    dpc.Query.Where(dpc.Query.OrderNo == mo.OrderNo);
                    dpc.LoadAll();

                    if (dpc.Count == 0)
                    {
                        mo.IsVerified = false;
                        mo.VerifiedByUserID = null;
                        mo.str.VerifiedDateTime = string.Empty;
                        mo.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        mo.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    }
                }
            }
            using (var trans = new esTransactionScope())
            {
                coll.Save();
                trans.Complete();
            }
        }

        private void Print(string reportName)
        {
            var jobParameters = new PrintJobParameterCollection();

            var parFromDate = jobParameters.AddNew();
            parFromDate.Name = "p_Date";
            parFromDate.ValueDateTime = txtOrderDate.SelectedDate ?? Convert.ToDateTime("1900-01-01 00:00:00");

            var parServiceUnit = jobParameters.AddNew();
            parServiceUnit.Name = "p_ServiceUnitID";
            parServiceUnit.ValueString = cboServiceUnitID.SelectedValue;

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

                    if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) || txtOrderDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                    {
                        pnlInfo.Visible = true;
                        lblInfo.Text = "Service Unit and Order To Date required.";
                        grdList.Rebind();
                        return;
                    }

                    Approval(true);
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

                    if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) || txtOrderDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                    {
                        pnlInfo.Visible = true;
                        lblInfo.Text = "Service Unit and Order To Date required.";
                        grdList.Rebind();
                        return;
                    }

                    if (IsDistributed())
                    {
                        pnlInfo.Visible = true;
                        lblInfo.Text = "Meal Order already distributed.";
                        grdList.Rebind();
                        return;
                    }

                    Approval(false);
                    grdList.Rebind();

                    break;

                case "print":
                    if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) || txtOrderDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                        return;

                    Print(AppConstant.Report.LetterOrderFoodRpt);

                    break;
            }
        }

        private bool IsDistributed()
        {
            var retValue = false;

            var dist = new DistributionPortionQuery("a");
            var mo = new MealOrderQuery("b");
            dist.InnerJoin(mo).On(dist.OrderNo == mo.OrderNo);
            dist.Where(mo.EffectiveDate.Date() == txtOrderDate.SelectedDate.Value.Date,
                       mo.ServiceUnitID == cboServiceUnitID.SelectedValue);
            if (dist.LoadDataTable().Rows.Count > 0)
                retValue = true;

            return retValue;
        }
    }
}
