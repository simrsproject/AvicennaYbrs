using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using System.Data;

namespace Temiang.Avicenna.Module.Nutrient.Master
{
    public partial class MenuItemSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = Request.QueryString["ext"] == "1" ? AppConstant.Program.MenuExtraItem : AppConstant.Program.MenuItem;
            
            if (!IsPostBack)
            {
                ComboBox.PopulateWithInpatientClassTariff(cboClassID);

                var mvcoll = new MenuVersionCollection();
                mvcoll.Query.Where(mvcoll.Query.IsActive == true);
                if (Request.QueryString["ext"] == "0")
                    mvcoll.Query.Where(mvcoll.Query.IsExtra == false);
                else
                    mvcoll.Query.Where(mvcoll.Query.IsExtra == true);
                mvcoll.Query.OrderBy(mvcoll.Query.VersionID.Descending);
                mvcoll.LoadAll();

                cboVersionID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var item in mvcoll)
                {
                    cboVersionID.Items.Add(new RadComboBoxItem(item.VersionName, item.VersionID));
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
            var query = new MenuItemQuery("a");
            var menu = new MenuQuery("b");
            var version = new MenuVersionQuery("c");
            var cls = new ClassQuery("d");
            
            query.InnerJoin(menu).On(query.MenuID == menu.MenuID);
            query.InnerJoin(version).On(query.VersionID == version.VersionID);
            query.InnerJoin(cls).On(query.ClassID == cls.ClassID);
            
            query.Select
                (
                    query.MenuItemID,
                    query.MenuItemName,
                    menu.MenuName,
                    version.VersionName,
                    query.SeqNo,
                    query.ClassID,
                    cls.ClassName,
                    query.IsActive
                );
            if (!string.IsNullOrEmpty(txtMenuItemID.Text))
            {
                if (cboFilterMenuItemID.SelectedIndex == 1)
                    query.Where(query.MenuItemID == txtMenuItemID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtMenuItemID.Text);
                    query.Where(query.MenuItemID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtMenuItemName.Text))
            {
                if (cboFilterMenuItemName.SelectedIndex == 1)
                    query.Where(query.MenuItemName == txtMenuItemName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtMenuItemName.Text);
                    query.Where(query.MenuItemName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboMenuID.SelectedValue))
                query.Where(menu.MenuID == cboMenuID.SelectedValue);
            
            if (!string.IsNullOrEmpty(cboClassID.SelectedValue))
                query.Where(query.ClassID == cboClassID.SelectedValue);

            if (!string.IsNullOrEmpty(cboVersionID.SelectedValue))
                query.Where(query.VersionID == cboVersionID.SelectedValue);

            if (Request.QueryString["ext"] == "1")
                query.Where(menu.IsExtra == true);
            else
                query.Where(menu.IsExtra == false);

            query.OrderBy(query.MenuID.Ascending, query.MenuItemID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }

        protected void cboMenuID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new MenuQuery("a");
            query.Where(query.Or(query.MenuID == e.Text, query.MenuName.Like(searchTextContain)));
            query.Where(query.IsActive == true);
            if (Request.QueryString["ext"] == "0")
                query.Where(query.IsExtra == false);
            else
                query.Where(query.IsExtra == true);

            query.Select(query.MenuID, query.MenuName);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboMenuID.DataSource = dtb;
            cboMenuID.DataBind();
        }

        protected void cboMenuID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["MenuName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["MenuID"].ToString();
        }
    }
}
