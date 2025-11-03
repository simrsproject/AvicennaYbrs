using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ItemGroupSearch : BasePageDialog
    {
        private string FormType
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            switch (FormType)
            {
                case "":
                case "service":
                    ProgramID = AppConstant.Program.GroupItem;
                    break;

                case "product":
                    ProgramID = AppConstant.Program.GroupItemProduct;
                    break;
            }

            if (!IsPostBack)
            {
                switch (FormType)
                {
                    case "":
                        StandardReference.InitializeIncludeSpace(cboSRItemType, AppEnum.StandardReference.ItemType);
                        break;

                    case "service":
                        StandardReference.InitializeIncludeSpace(cboSRItemType, AppEnum.StandardReference.ItemType, "Service");

                        break;
                    case "product":
                        StandardReference.InitializeIncludeSpace(cboSRItemType, AppEnum.StandardReference.ItemType, "Product");
                        break;
                }
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool  OnButtonOkClicked()
        {
            var query = new ItemGroupQuery("a");
            var it = new AppStandardReferenceItemQuery("b");
            query.InnerJoin(it).On(query.SRItemType == it.ItemID && it.StandardReferenceID == "ItemType");
            query.Select(query.ItemGroupID, query.ItemGroupName, query.Initial,
                        @"<a.SRItemType + ' - ' + b.ItemName AS 'ItemType'>",
                        it.ItemName, query.IsActive, query.CssClass);
            if (FormType == "service")
                query.Where(query.SRItemType.In("01", "31", "41", "61"));
            else if (FormType == "product")
                query.Where(query.SRItemType.In("11", "21", "81"));

            if (!string.IsNullOrEmpty(cboSRItemType.SelectedValue))
            {
                query.Where(query.SRItemType == cboSRItemType.SelectedValue);
            }
            if (!string.IsNullOrEmpty(txtItemGroupID.Text))
            {
                if (cboFilterItemGroupID.SelectedIndex == 1)
                    query.Where(query.ItemGroupID == txtItemGroupID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtItemGroupID.Text);
                    query.Where(query.ItemGroupID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtItemGroupName.Text))
            {
                if (cboFilterItemGroupName.SelectedIndex == 1)
                    query.Where(query.ItemGroupName == txtItemGroupName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtItemGroupName.Text);
                    query.Where(query.ItemGroupName.Like(searchTextContain));
                }
            }
            query.OrderBy(query.ItemGroupID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
