using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ItemServiceSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.ServiceItem;

            if (!IsPostBack)
            {
                var group = new ItemGroupCollection();
                group.Query.Where(group.Query.IsActive == true, group.Query.SRItemType == ItemType.Service);
                group.Query.OrderBy(group.Query.ItemGroupID.Ascending);
                group.LoadAll();

                cboItemGroupID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ItemGroup entity in group)
                {
                    cboItemGroupID.Items.Add(new RadComboBoxItem(entity.ItemGroupName, entity.ItemGroupID));
                }
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new ItemQuery("a");
            var serv = new ItemServiceQuery("b");
            var grp = new ItemGroupQuery("c");
            query.InnerJoin(serv).On(query.ItemID == serv.ItemID);
            query.InnerJoin(grp).On(query.ItemGroupID == grp.ItemGroupID);
            
            query.Select(query.ItemID,
                            grp.ItemGroupName,
                            query.ItemName,
                            query.IsActive,
                            query.Notes,
                            serv.SRItemUnit);
            query.Where(query.SRItemType == ItemType.Service);

            var isEsTop = true;

            if (!string.IsNullOrEmpty(cboItemGroupID.SelectedValue))
            {
                query.Where(grp.ItemGroupID == cboItemGroupID.SelectedValue);
                isEsTop = false;
            }
            
            if (!string.IsNullOrEmpty(txtItemID.Text))
            {
                if (cboFilterItemID.SelectedIndex == 1)
                    query.Where(query.ItemID == txtItemID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtItemID.Text);
                    query.Where(query.ItemID.Like(searchTextContain));
                }
                isEsTop = false;
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
                isEsTop = false;
            }
            query.OrderBy(query.ItemGroupID.Ascending, query.ItemID.Ascending);

            if (isEsTop)
                query.es.Top = AppSession.Parameter.MaxResultRecord;

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();
            return true;
        }

    }
}
