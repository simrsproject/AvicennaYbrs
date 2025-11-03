using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Master.HRBase
{
    public partial class EducationLevelSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.EducationLevel;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
            }
        }

        public override bool OnButtonOkClicked()
        {
            OnButtonOkClick();

            return true;
        }

        private void OnButtonOkClick()
        {
            var query = new AppStandardReferenceItemQuery();
            query.Select(
                            query.ItemID,
                            query.ItemName,
                            query.IsActive,
                            query.LastUpdateDateTime,
                            query.LastUpdateByUserID
                        );
            query.Where(query.StandardReferenceID == AppEnum.StandardReference.FieldLabor);

            if (!string.IsNullOrEmpty(txtTypeOfLabor.Text))
            {
                if (cboFilterTypeOfLabor.SelectedIndex == 1)
                    query.Where(query.ItemName == txtTypeOfLabor.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtTypeOfLabor.Text);
                    query.Where(query.ItemName.Like(searchTextContain));
                }
            }

            query.OrderBy(query.ItemID.Ascending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset
        }
    }
}