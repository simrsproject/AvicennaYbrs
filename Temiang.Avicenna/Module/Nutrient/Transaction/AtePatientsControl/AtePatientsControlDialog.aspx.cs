using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class AtePatientsControlDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AtePatientsControl;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboCarbohydrate, AppEnum.StandardReference.EatingPatientStatus, false);
                StandardReference.InitializeIncludeSpace(cboVegetableSideDish, AppEnum.StandardReference.EatingPatientStatus, false);
                StandardReference.InitializeIncludeSpace(cboAnimalSideDish, AppEnum.StandardReference.EatingPatientStatus, false);
                StandardReference.InitializeIncludeSpace(cboVegetable, AppEnum.StandardReference.EatingPatientStatus, false);
                StandardReference.InitializeIncludeSpace(cboFruit, AppEnum.StandardReference.EatingPatientStatus, false);
                StandardReference.InitializeIncludeSpace(cboBeverage, AppEnum.StandardReference.EatingPatientStatus, false);
                StandardReference.InitializeIncludeSpace(cboSREatingPatientStatusReason, AppEnum.StandardReference.EatingPatientStatusReason, false);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtOrderNo.Text = Request.QueryString["orderno"];

                //-- header --
                var mealOrder = new MealOrder();
                mealOrder.LoadByPrimaryKey(txtOrderNo.Text);

                txtRegistrationNo.Text = mealOrder.RegistrationNo;
                var reg = new Registration();
                if (reg.LoadByPrimaryKey(txtRegistrationNo.Text))
                {
                    var pat = new Patient();
                    pat.LoadByPrimaryKey(reg.str.PatientID);
                    txtMedicalNo.Text = pat.MedicalNo;
                    txtPatientName.Text = pat.PatientName;
                    txtGender.Text = pat.Sex;
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
                txtUnitRoomBed.Text = su + " (Room : " + sr + ", Bed : " + mealOrder.BedID + ")";
                txtClassID.Text = mealOrder.ClassID;
                var c = new Class();
                if (c.LoadByPrimaryKey(txtClassID.Text))
                    lblClassName.Text = c.ClassName;

                //-- detail --
                var entity = new AtePatientsControl();
                if (entity.LoadByPrimaryKey(txtOrderNo.Text, Request.QueryString["set"]))
                {
                    txtControlDate.SelectedDate = entity.ControlDate;
                    cboCarbohydrate.SelectedValue = entity.Carbohydrate;
                    cboVegetableSideDish.SelectedValue = entity.VegetableSideDish;
                    cboAnimalSideDish.SelectedValue = entity.AnimalSideDish;
                    cboVegetable.SelectedValue = entity.Vegetable;
                    cboFruit.SelectedValue = entity.Fruit;
                    cboBeverage.SelectedValue = entity.Beverage;
                    cboSREatingPatientStatusReason.SelectedValue = entity.SREatingPatientStatusReason;
                    txtNotes.Text = entity.Notes;
                }
                else
                    txtControlDate.SelectedDate = (new DateTime()).NowAtSqlServer();

                var ms = new AppStandardReferenceItem();
                if (ms.LoadByPrimaryKey(AppEnum.StandardReference.MealSet.ToString(), Request.QueryString["set"]))
                    txtMealSet.Text = ms.ItemName;
            }
        }

        public override bool OnButtonOkClicked()
        {
            Validate();

            if (!IsValid)
                return false;

            using (var trans = new esTransactionScope())
            {
                var entity = new AtePatientsControl();
                if (!entity.LoadByPrimaryKey(txtOrderNo.Text, Request.QueryString["set"]))
                    entity.AddNew();

                entity.OrderNo = txtOrderNo.Text;
                entity.SRMealSet = Request.QueryString["set"];
                entity.ControlDate = txtControlDate.SelectedDate;
                entity.Carbohydrate = cboCarbohydrate.SelectedValue;
                entity.VegetableSideDish = cboVegetableSideDish.SelectedValue;
                entity.AnimalSideDish = cboAnimalSideDish.SelectedValue;
                entity.Vegetable = cboVegetable.SelectedValue;
                entity.Fruit = cboFruit.SelectedValue;
                entity.Beverage = cboBeverage.SelectedValue;
                entity.SREatingPatientStatusReason = cboSREatingPatientStatusReason.SelectedValue;
                entity.Notes = txtNotes.Text;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                entity.Save();

                trans.Complete();
            }

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.command = 'rebind'";
        }
    }
}
