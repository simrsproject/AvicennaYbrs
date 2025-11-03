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
    public partial class MealOrderChangeList : BasePage
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

            switch (Request.QueryString["type"])
            {
                case "":
                    ProgramID = AppConstant.Program.MealOrderChange;
                    break;
                case "dc":
                    ProgramID = AppConstant.Program.MealOrderChangeDc;
                    break;
                case "sc":
                    ProgramID = AppConstant.Program.MealOrderChangeSc;
                    break;
            }

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
                    cboServiceUnitID.SelectedValue = Request.QueryString["unitid"];

                txtOrderDate.SelectedDate = DateTime.Now;
                StandardReference.Initialize(cboSRMealSet, AppEnum.StandardReference.MealSet, true);

                if (!string.IsNullOrEmpty(Request.QueryString["mealset"]))
                    cboSRMealSet.SelectedValue = Request.QueryString["mealset"];
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
                if (!ValidateSearch(isEmptyFilter, "Meal Order Change")) return null;

                var query = new MealOrderQuery("a");
                var reg = new RegistrationQuery("b");
                var patient = new PatientQuery("c");
                var bed = new BedQuery("d");
                var room = new ServiceRoomQuery("e");
                var unit = new ServiceUnitQuery("f");
                var diet = new DietQuery("g");
                var menu = new MenuQuery("h");
                var dp = new DietPatientQuery("i");
                var distPortion = new DistributionPortionQuery("j");
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
                        menu.MenuName,
                        diet.DietName,
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
                query.InnerJoin(dp).On(dp.TransactionNo == query.DietPatientNo);
                query.LeftJoin(distPortion).On(query.OrderNo == distPortion.OrderNo &&
                                               distPortion.SRMealSet == cboSRMealSet.SelectedValue);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

                query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue, reg.DischargeDate.IsNull(), distPortion.OrderNo.IsNull());

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
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(c.FirstName + ' ' + c.MiddleName)) + ' ' + c.LastName) LIKE '{0}'>", searchPatient)
                        );
                }

                query.Where(query.IsApproved == true, query.EffectiveDate == txtOrderDate.SelectedDate);
                if (AppSession.Parameter.IsUsingMealOrderVerification)
                    query.Where(query.IsVerified == true);
                if (Request.QueryString["type"] == "sc")
                    query.Where(dp.IsSpecialCondition == true);

                query.OrderBy(unit.ServiceUnitName.Ascending, query.BedID.Ascending);

                DataTable dtb = query.LoadDataTable();

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

        protected void cboSRMealSet_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            grdList.Rebind();
        }
    }
}
