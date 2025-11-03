using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Laundry.Master
{
    public partial class ItemLinenSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.ItemLinen;
            
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new ItemLinenQuery("a");
            query.Select(query.ItemID,
                            query.ItemName,
                            query.Notes,
                            query.IsActive);
            var isEsTop = true;

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
            query.OrderBy(query.ItemID.Ascending);

            if (isEsTop)
                query.es.Top = AppSession.Parameter.MaxResultRecord;

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();
            return true;
        }
    }
}