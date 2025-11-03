using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Nutrient.Initialization
{
    public partial class UnitClassMenuExtraSettingSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.UnitClassMenuSetting;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new ServiceUnitQuery("a");
            var std = new AppStandardReferenceItemQuery("b");
            var x = new ServiceUnitClassMenuExtraSettingQuery("c");
            var c = new ClassQuery("d");
            var m = new MenuQuery("e");
            query.InnerJoin(std).On(query.ServiceUnitID == std.ItemID &&
                                    std.StandardReferenceID == AppEnum.StandardReference.UnitForExtraMealOrder);
            query.LeftJoin(x).On(query.ServiceUnitID == x.ServiceUnitID);
            query.LeftJoin(c).On(x.ClassID == c.ClassID);
            query.LeftJoin(m).On(x.MenuID == m.MenuID);
            query.Select
                (
                    query.ServiceUnitID,
                    query.ServiceUnitName,
                    c.ClassName,
                    m.MenuName
                );
            query.Where(query.IsActive == true);
            if (!string.IsNullOrEmpty(txtServiceUnitID.Text))
            {
                if (cboFilterServiceUnitID.SelectedIndex == 1)
                    query.Where(query.ServiceUnitID == txtServiceUnitID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtServiceUnitID.Text);
                    query.Where(query.ServiceUnitID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtServiceUnitName.Text))
            {
                if (cboFilterServiceUnitName.SelectedIndex == 1)
                    query.Where(query.ServiceUnitName == txtServiceUnitName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtServiceUnitName.Text);
                    query.Where(query.ServiceUnitName.Like(searchTextContain));
                }
            }

            query.OrderBy(query.ServiceUnitID.Ascending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
