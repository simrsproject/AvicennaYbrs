using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Nutrient.Initialization
{
    public partial class MealOrderDateInitializationDialog : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.MealOrderDateInitialization;

            if (!IsPostBack)
                txtMealOrderDate.SelectedDate = DateTime.Now;
        }

        protected void btnSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (txtMealOrderDate.SelectedDate.Value < DateTime.Now.Date)
            {
                pnlInfo.Visible = true;
                lblInfo.Text = "Meal Order Date can't be less than today.";
            }
            else
            {
                var entity = new MealOrderDateInit();
                if (!entity.LoadByPrimaryKey(txtMealOrderDate.SelectedDate.Value))
                    entity.AddNew();
                entity.MealOrderDate = txtMealOrderDate.SelectedDate;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.Save();

                pnlInfo.Visible = true;
                lblInfo.Text = "Data has been saved.";
            }
        }
    }
}
