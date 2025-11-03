using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Nutrient.Initialization
{
    public partial class LiquidFoodSettingSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.LiquidFoodSetting;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new AppStandardReferenceQuery();
            query.Select
                        (
                            query.StandardReferenceID,
                            query.StandardReferenceName
                        );
            query.Where(query.StandardReferenceID.In("LQ-Unit", "LQ-Class"));
            

            if (!string.IsNullOrEmpty(txtStandardReferenceID.Text))
            {
                if (cboFilterStandardReferenceID.SelectedIndex == 1)
                    query.Where(query.StandardReferenceID == txtStandardReferenceID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtStandardReferenceID.Text);
                    query.Where(query.StandardReferenceID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtStandardReferenceName.Text))
            {
                if (cboFilterStandardReferenceName.SelectedIndex == 1)
                    query.Where(query.StandardReferenceName == txtStandardReferenceName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtStandardReferenceName.Text);
                    query.Where(query.StandardReferenceName.Like(searchTextContain));
                }
            }

            query.OrderBy(query.StandardReferenceID.Ascending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
