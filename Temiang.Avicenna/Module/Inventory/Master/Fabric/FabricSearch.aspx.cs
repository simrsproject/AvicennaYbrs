using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class FabricSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.Fabric;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool  OnButtonOkClicked()
        {
            var query = new FabricQuery();
            if (!string.IsNullOrEmpty(txtFabricID.Text))
            {
                if (cboFilterFabricID.SelectedIndex == 1)
                    query.Where(query.FabricID == txtFabricID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtFabricID.Text);
                    query.Where(query.FabricID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtFabricName.Text))
            {
                if (cboFilterFabricName.SelectedIndex == 1)
                    query.Where(query.FabricName == txtFabricName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtFabricName.Text);
                    query.Where(query.FabricName.Like(searchTextContain));
                }
            }
            query.OrderBy(query.FabricID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
