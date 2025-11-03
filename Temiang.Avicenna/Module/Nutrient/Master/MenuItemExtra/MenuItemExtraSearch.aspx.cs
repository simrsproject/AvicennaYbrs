using System;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Nutrient.Master
{
    public partial class MenuItemExtraSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.MenuExtraItemFood;

            if (!IsPostBack)
            {
                var menuColl = new MenuCollection();
                menuColl.Query.Where(menuColl.Query.IsActive == true, menuColl.Query.IsExtra == true);
                menuColl.LoadAll();
                cboMenuID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var item in menuColl)
                {
                    cboMenuID.Items.Add(new RadComboBoxItem(item.MenuName, item.MenuID));
                }

                StandardReference.Initialize(cboSRMealSet, AppEnum.StandardReference.MealSet);
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new MenuItemExtraQuery("a");
            var menuQ = new MenuQuery("b");
            var msQ = new AppStandardReferenceItemQuery("c");
            query.InnerJoin(menuQ).On(query.MenuID == menuQ.MenuID);
            query.InnerJoin(msQ).On(query.SRMealSet == msQ.ItemID &&
                                    msQ.StandardReferenceID == AppEnum.StandardReference.MealSet);
            query.Select
                (
                    query.SeqNo,
                    query.MenuID,
                    menuQ.MenuName,
                    query.StartingDate,
                    msQ.ItemName.As("MealSet")
                );
            query.OrderBy(query.SeqNo.Ascending);

            if (!string.IsNullOrEmpty(cboMenuID.SelectedValue))
            {
                query.Where(query.MenuID == cboMenuID.SelectedValue);
            }
            if (!txtStartingDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.StartingDate == txtStartingDate.SelectedDate);
            }
            if (!string.IsNullOrEmpty(cboSRMealSet.SelectedValue))
            {
                query.Where(query.SRMealSet == cboSRMealSet.SelectedValue);
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
