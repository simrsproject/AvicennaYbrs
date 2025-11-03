using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class AdditionalMealOrderSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ExtraMealOrder;

            if (!IsPostBack)
            {
                var menuColl = new MenuCollection();
                menuColl.Query.Where(menuColl.Query.IsExtra == true);
                menuColl.LoadAll();
                cboMenuID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var item in menuColl)
                {
                    cboMenuID.Items.Add(new RadComboBoxItem(item.MenuName, item.MenuID));
                }

                StandardReference.InitializeIncludeSpace(cboSRMealSet, AppEnum.StandardReference.MealSet);
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new AddMealOrderQuery("a");
            var menu = new MenuQuery("b");
            var set = new AppStandardReferenceItemQuery("c");
            query.InnerJoin(menu).On(query.MenuID == menu.MenuID);
            query.InnerJoin(set).On(query.SRMealSet == set.ItemID &&
                                    set.StandardReferenceID == AppEnum.StandardReference.MealSet);
            query.Select
                (
                    query.OrderNo,
                    query.OrderDate,
                    query.EffectiveDate,
                    query.MenuID,
                    menu.MenuName,
                    set.ItemName.As("MealSet"),
                    query.Qty,
                    query.Notes,
                    query.IsApproved,
                    query.IsVoid
                );
            query.OrderBy(query.OrderNo.Descending);

            if (!string.IsNullOrEmpty(txtOrderNo.Text))
            {
                if (cboFilterOrderNo.SelectedIndex == 1)
                    query.Where(query.OrderNo == txtOrderNo.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtOrderNo.Text);
                    query.Where(query.OrderNo.Like(searchText));
                }
            }
            if (!txtOrderDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                query.Where(query.OrderDate == txtOrderDate.SelectedDate);
            if (!txtEffectiveDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                query.Where(query.EffectiveDate == txtEffectiveDate.SelectedDate);
            if (!string.IsNullOrEmpty(cboMenuID.SelectedValue))
                query.Where(query.MenuID == cboMenuID.SelectedValue);
            if (!string.IsNullOrEmpty(cboSRMealSet.SelectedValue))
                query.Where(query.SRMealSet == cboSRMealSet.SelectedValue);
            
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
