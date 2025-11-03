using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class WageStructureAndScaleWorkGroupSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.WageStructureAndScaleWorkGroup;
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
            var query = new AppStandardReferenceItemQuery("a");
            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(
                query.ItemID,
                query.ItemName,
                query.NumericValue,
                query.IsActive,
                query.LastUpdateDateTime,
                query.LastUpdateByUserID
                );
            query.Where(query.StandardReferenceID == AppEnum.StandardReference.EmployeeWorkGroup);

            if (!string.IsNullOrEmpty(txtItemID.Text))
            {
                if (cboFilterItemName.SelectedIndex == 1)
                    query.Where(query.ItemID == txtItemID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtItemID.Text);
                    query.Where(query.ItemID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtItemName.Text))
            {
                if (cboFilterItemName.SelectedIndex == 1)
                    query.Where(query.ItemName == txtItemName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtItemName.Text);
                    query.Where(query.ItemName.Like(searchTextContain));
                }
            }

            query.OrderBy(query.ItemID.Ascending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset
        }
    }
}