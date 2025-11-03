using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;

namespace Temiang.Avicenna.Module.Nutrient.Initialization
{
    public partial class UnitClassMealSetMenuSettingDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.UnitClassMenuSetting;

            if (!IsPostBack)
            {
                txtServiceUnitID.Text = Request.QueryString["unitId"];
                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(txtServiceUnitID.Text);
                lblServiceUnitName.Text = unit.ServiceUnitName;

                txtClassID.Text = Request.QueryString["classId"];
                var cls = new Class();
                cls.LoadByPrimaryKey(txtClassID.Text);
                lblClassName.Text = cls.ClassName;
            }
        }

        protected void grdMealSet_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var coll = new AppStandardReferenceItemCollection();
            coll.Query.Where(coll.Query.StandardReferenceID == AppEnum.StandardReference.MealSet);
            coll.Query.OrderBy(coll.Query.ItemID.Ascending);
            coll.LoadAll();

            foreach (var rec in coll)
            {
                var entity = ((ServiceUnitClassMealSetMenuSettingCollection)Session["collServiceUnitClassMealSetMenuSetting"]).FindByPrimaryKey(txtServiceUnitID.Text, txtClassID.Text, rec.ItemID);
                if (entity == null)
                {
                    entity = ((ServiceUnitClassMealSetMenuSettingCollection)Session["collServiceUnitClassMealSetMenuSetting"]).AddNew();
                    entity.ServiceUnitID = txtServiceUnitID.Text;
                    entity.ClassID = txtClassID.Text;
                    entity.SRMealSet = rec.ItemID;
                    entity.MealSet = rec.ItemName;
                    entity.IsOptional = false;
                    entity.LastUpdateDateTime = DateTime.Now;
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }
            }

            grdMealSet.DataSource = ((ServiceUnitClassMealSetMenuSettingCollection)Session["collServiceUnitClassMealSetMenuSetting"]).Where(c => c.ClassID == txtClassID.Text);
        }

        public override bool OnButtonOkClicked()
        {
            Validate();
            if (!IsValid)
                return false;

            foreach (GridDataItem dataItem in grdMealSet.MasterTableView.Items)
            {
                bool isOptional = ((CheckBox)dataItem.FindControl("chkIsOptional")).Checked;

                var comp = ((ServiceUnitClassMealSetMenuSettingCollection)Session["collServiceUnitClassMealSetMenuSetting"]).FindByPrimaryKey(txtServiceUnitID.Text, txtClassID.Text, dataItem["SRMealSet"].Text);
                if (comp == null)
                {
                    comp = ((ServiceUnitClassMealSetMenuSettingCollection)Session["collServiceUnitClassMealSetMenuSetting"]).AddNew();
                    comp.ServiceUnitID = txtServiceUnitID.Text;
                    comp.ClassID = txtClassID.Text;
                    comp.SRMealSet = dataItem["SRMealSet"].Text;
                }
                comp.IsOptional = isOptional;
                comp.LastUpdateDateTime = DateTime.Now;
                comp.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }

            return true;
        }
    }
}
