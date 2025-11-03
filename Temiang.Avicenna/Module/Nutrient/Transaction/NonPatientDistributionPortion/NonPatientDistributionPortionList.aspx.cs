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
    public partial class NonPatientDistributionPortionList : BasePage
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

            ProgramID = AppConstant.Program.NonPatientCustomerDistributionPortion;

            if (!IsPostBack)
            {
                //service unit
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");

                query.Where(
                    query.SRRegistrationType.In(AppConstant.RegistrationType.InPatient,
                        AppConstant.RegistrationType.EmergencyPatient, AppConstant.RegistrationType.OutPatient,
                        AppConstant.RegistrationType.MedicalCheckUp), query.IsActive == true);

                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                txtOrderDate.SelectedDate = (new DateTime()).NowAtSqlServer();
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
                var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) 
                    && string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Distribution Non Patient")) return null;

                var query = new MealOrderNonPatientQuery("a");
                var reg = new RegistrationQuery("b");
                var patient = new PatientQuery("c");
                var bed = new BedQuery("d");
                var room = new ServiceRoomQuery("e");
                var unit = new ServiceUnitQuery("f");
                var sal = new AppStandardReferenceItemQuery("sal");

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        query.TransactionNo,
                        query.TransactionDate,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        room.RoomName,
                        reg.BedID,
                        reg.ServiceUnitID,
                        unit.ServiceUnitName,
                        query.IsDistributed,
                        @"<CAST(b.AgeInYear AS VARCHAR) + 'y ' + CAST(b.AgeInMonth AS VARCHAR) + 'm ' + CAST(b.AgeInDay AS VARCHAR) + 'd' AS Age>",
                        sal.ItemName.As("SalutationName"),
                        @"<'' AS OrderedMenu>"
                    );

                query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                query.InnerJoin(bed).On(reg.BedID == bed.BedID);
                query.LeftJoin(room).On(reg.RoomID == room.RoomID);
                query.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    query.Where(reg.ServiceUnitID == cboServiceUnitID.SelectedValue);

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

                if (!chkIncludeDistributed.Checked)
                    query.Where(query.IsDistributed == false);

                query.Where(query.IsApproved == true, query.TransactionDate.Date() == txtOrderDate.SelectedDate.Value.Date,
                            reg.IsClosed == false, reg.SRDischargeMethod == string.Empty);

                query.OrderBy(unit.ServiceUnitName.Ascending, query.TransactionNo.Ascending);

                DataTable dtb = query.LoadDataTable();

                foreach (DataRow row in dtb.Rows)
                {
                    var item = new MealOrderNonPatientItemQuery("a");
                    var food = new FoodQuery("b");
                    item.InnerJoin(food).On(food.FoodID == item.FoodID);
                    item.Where(item.TransactionNo == row["TransactionNo"].ToString());
                    item.OrderBy(item.SequenceNo.Ascending);
                    item.Select(food.FoodName, item.Qty);
                    DataTable dtbItem = item.LoadDataTable();
                    if (dtbItem.Rows.Count > 0)
                    {
                        var orderedMenu = string.Empty;
                        foreach (DataRow i in dtbItem.Rows)
                        {
                            if (orderedMenu == string.Empty)
                                orderedMenu = i["FoodName"].ToString() + " (" + Convert.ToInt16(i["Qty"]).ToString()+")";
                            else
                                orderedMenu += "; " + i["FoodName"].ToString() + " (" + Convert.ToInt16(i["Qty"]).ToString() + ")";
                        }

                        row["OrderedMenu"] = orderedMenu;
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

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            grdList.Rebind();
        }

        protected void chkIncludeDistributed_CheckedChanged(object sender, EventArgs e)
        {
            grdList.Rebind();
        }

        private void Process(string transNo, string regNo)
        {
            var query = new MealOrderNonPatientItemQuery("a");
            var food = new FoodQuery("b");
            var item = new ItemQuery("c");
            query.InnerJoin(food).On(food.FoodID == query.FoodID);
            query.InnerJoin(item).On(item.ItemID == food.ItemID);
            query.Where(query.TransactionNo == transNo);
            query.Select(food.ItemID, query.Qty.Sum().As("Qty"));
            query.GroupBy(food.ItemID);
            DataTable dtb = query.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(regNo);

                var guarantorId = reg.GuarantorID;
                if (guarantorId == AppSession.Parameter.SelfGuarantor)
                {
                    var pat = new Patient();
                    pat.LoadByPrimaryKey(reg.PatientID);
                    if (!string.IsNullOrEmpty(pat.MemberID))
                        guarantorId = pat.MemberID;
                }

                var grr = new Guarantor();
                grr.LoadByPrimaryKey(guarantorId);

                var idx = 1;
                foreach (DataRow row in dtb.Rows)
                {
                    using (var trans = new esTransactionScope())
                    {
                        Temiang.Avicenna.WebService.BillingChargeService.BillingProcess(regNo, row["ItemID"].ToString(), string.Format("{0:000}", idx), Convert.ToDecimal(row["Qty"]), "tr", true);

                        trans.Complete();
                    }

                    idx += 1;
                }
            }
        }

        private void Print(string reportName, string tranNo)
        {
            var jobParameters = new PrintJobParameterCollection();

            var parTransactionNo = jobParameters.AddNew();
            parTransactionNo.Name = "p_TransactionNo";
            parTransactionNo.ValueString = tranNo;
           
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

                default:
                    if (eventArgument.Contains("distributed|"))
                    {
                        var param = eventArgument.Split('|');
                        string transNo = param[1];
                        var mo = new MealOrderNonPatient();
                        if (mo.LoadByPrimaryKey(transNo))
                        {
                            Process(transNo, mo.RegistrationNo);

                            mo.IsDistributed = true;
                            mo.DistributedByUserID = AppSession.UserLogin.UserID;
                            mo.DistributedDateTime = (new DateTime()).NowAtSqlServer();
                            mo.Save();
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
                Print(AppConstant.Report.DistributionOptionalMenuSlip, e.CommandArgument.ToString());
            }
            else if (e.CommandName == "distributed")
            {
                var transNo = e.CommandArgument.ToString();
                var mo = new MealOrderNonPatient();
                if (mo.LoadByPrimaryKey(transNo))
                {
                    Process(transNo, mo.RegistrationNo);

                    mo.IsDistributed = true;
                    mo.DistributedByUserID = AppSession.UserLogin.UserID;
                    mo.DistributedDateTime = (new DateTime()).NowAtSqlServer();
                }
                grdList.Rebind();
            }
        }
    }
}