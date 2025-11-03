using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.TrainingHR.Master.TrainingType
{
    public partial class TrainingTypeSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.EmployeeTrainingType;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new AppStandardReferenceItemQuery("a");
            query.Select
                (
                    query.ItemID,
                    query.ItemName,
                    query.IsActive
                );
            query.Where(query.StandardReferenceID == AppEnum.StandardReference.ActivityType.ToString());

            if (!string.IsNullOrEmpty(txtItemID.Text))
            {
                if (cboFilterItemID.SelectedIndex == 1)
                    query.Where(query.ItemID == txtItemID.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtItemID.Text);
                    query.Where(query.ItemID.Like(searchText));
                }
            }
            if (!string.IsNullOrEmpty(txtItemName.Text))
            {
                if (cboFilterItemName.SelectedIndex == 1)
                    query.Where(query.ItemName == txtItemName.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtItemName.Text);
                    query.Where(query.ItemName.Like(searchText));
                }
            }
            query.OrderBy(query.ItemID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}