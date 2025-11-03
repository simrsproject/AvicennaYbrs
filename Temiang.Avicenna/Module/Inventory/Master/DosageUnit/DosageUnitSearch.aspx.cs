using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory
{
    public partial class DosageUnitSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.DosageUnit;
        }


        public override bool OnButtonOkClicked()
        {
            var query = new AppStandardReferenceItemQuery();
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
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
