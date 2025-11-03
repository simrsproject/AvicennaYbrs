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
    public partial class AtePatientsControlList : BasePage
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

            ProgramID = AppConstant.Program.AtePatientsControl;

            if (!IsPostBack)
            {
                //service unit
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");

                if (!AppSession.Parameter.IsControlEatingPatientByNutritionists)
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

                txtOrderDate.SelectedDate = (new DateTime()).NowAtSqlServer();
                StandardReference.Initialize(cboSRMealSet, AppEnum.StandardReference.MealSet, true);
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
                if (!ValidateSearch(isEmptyFilter, "Control Eating")) return null;

                var query = new MealOrderQuery("a");
                var reg = new RegistrationQuery("b");
                var patient = new PatientQuery("c");
                var bed = new BedQuery("d");
                var room = new ServiceRoomQuery("e");
                var unit = new ServiceUnitQuery("f");
                var diet = new DietQuery("h");
                var menu = new MenuQuery("i");
                var sal = new AppStandardReferenceItemQuery("sal");
                var distpor = new DistributionPortionQuery("dp");

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
                        unit.ServiceUnitID,
                        query.FastingTime,
                        diet.DietName,
                        menu.MenuName,
                        @"<CAST(0 AS BIT) AS 'IsControl'>",
                        @"<CAST(b.AgeInYear AS VARCHAR) + 'y ' + CAST(b.AgeInMonth AS VARCHAR) + 'm ' + CAST(b.AgeInDay AS VARCHAR) + 'd' AS Age>",
                        sal.ItemName.As("SalutationName")
                    );

                query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                query.InnerJoin(bed).On(reg.BedID == bed.BedID);
                query.LeftJoin(room).On(reg.RoomID == room.RoomID);
                query.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
                query.InnerJoin(diet).On(query.DietID == diet.DietID);
                query.InnerJoin(menu).On(query.MenuID == menu.MenuID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
                query.InnerJoin(distpor).On(query.OrderNo == distpor.OrderNo &&
                                            distpor.SRMealSet == cboSRMealSet.SelectedValue && distpor.IsVoid == false);
                if (!AppSession.Parameter.IsControlEatingPatientByNutritionists)
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID && qusr.UserID == AppSession.UserLogin.UserID);
                }
                
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    if (AppSession.Parameter.IsMedicalNoContainStrip)
                        query.Where(
                            query.Or(
                                patient.MedicalNo == searchReg,
                                patient.OldMedicalNo == searchReg,
                                query.RegistrationNo == searchReg,
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

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unit.ServiceUnitName;

                query.Select(group.As("Group"));
                query.Where(query.IsApproved == true, query.EffectiveDate == txtOrderDate.SelectedDate, query.IsOpr == false,
                            reg.IsClosed == false, reg.DischargeDate.IsNull());
                query.OrderBy(query.BedID.Ascending);

                DataTable dtb = query.LoadDataTable();

                foreach (DataRow d in dtb.Rows)
                {
                    var coll = new AtePatientsControlCollection();
                    coll.Query.Where(coll.Query.OrderNo == d["OrderNo"].ToString(),
                                     coll.Query.SRMealSet == cboSRMealSet.SelectedValue);
                    coll.LoadAll();
                    d["IsControl"] = coll.Count > 0;

                    if (!string.IsNullOrEmpty(d["FastingTime"].ToString()))
                    {
                        bool isFasting = false;
                        switch (cboSRMealSet.SelectedValue)
                        {
                            case "01": //pagi
                                isFasting = d["FastingTime"].ToString().Contains("1");
                                break;
                            case "02": //siang
                                isFasting = d["FastingTime"].ToString().Contains("2");
                                break;
                            case "03": //sore
                                isFasting = d["FastingTime"].ToString().Contains("3");
                                break;
                        }
                        if (isFasting)
                            d.Delete();
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
            }
        }
    }
}
