using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ItemServiceProcedureSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.ItemServiceProcedure;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new AppStandardReferenceItemQuery("a");
            query.Select(query);
            query.Where(query.StandardReferenceID == AppEnum.StandardReference.Procedure.ToString());

            if (!string.IsNullOrEmpty(txtItemID.Text))
            {
                if (cboFilterItemID.SelectedIndex == 1)
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

            return true;
        }
    }
}