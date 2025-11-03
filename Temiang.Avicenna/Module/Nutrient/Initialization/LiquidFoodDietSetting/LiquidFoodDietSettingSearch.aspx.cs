using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Nutrient.Initialization
{
    public partial class LiquidFoodDietSettingSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.LiquidFoodDietSetting;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new DietQuery();
            query.Select
                        (
                            query.DietID,
                            query.DietName
                        );

            if (!string.IsNullOrEmpty(txtDietID.Text))
            {
                if (cboFilterDietID.SelectedIndex == 1)
                    query.Where(query.DietID == txtDietID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtDietID.Text);
                    query.Where(query.DietID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtDietName.Text))
            {
                if (cboFilterDietName.SelectedIndex == 1)
                    query.Where(query.DietName == txtDietName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtDietName.Text);
                    query.Where(query.DietName.Like(searchTextContain));
                }
            }

            query.OrderBy(query.DietID.Ascending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
