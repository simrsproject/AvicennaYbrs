using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class TariffComponentSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.TariffComponent;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new TariffComponentQuery("a");
            var type = new AppStandardReferenceItemQuery("b");
            query.InnerJoin(type).On(query.SRTariffComponentType == type.ItemID &&
                                     type.StandardReferenceID == AppEnum.StandardReference.TariffComponentType);

            query.Select
                (
                query.TariffComponentID,
                query.TariffComponentName,
                type.ItemName.As("TariffComponentType"),
                query.Notes,
                query.IsTariffParamedic,
                query.IsIncludeInTaxCalc
                );

            if (!string.IsNullOrEmpty(txtTariffComponentID.Text))
            {
                if (cboFilterTariffComponentID.SelectedIndex == 1)
                    query.Where(query.TariffComponentID == txtTariffComponentID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtTariffComponentID.Text);
                    query.Where(query.TariffComponentID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtTariffComponentName.Text))
            {
                if (cboFilterTariffComponentName.SelectedIndex == 1)
                    query.Where(query.TariffComponentName == txtTariffComponentName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtTariffComponentName.Text);
                    query.Where(query.TariffComponentName.Like(searchTextContain));
                }
            }
            query.OrderBy(query.TariffComponentID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
