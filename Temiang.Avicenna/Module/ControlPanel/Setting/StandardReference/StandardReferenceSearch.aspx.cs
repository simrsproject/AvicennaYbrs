using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ControlPanel.Setting
{
    public partial class StandardReferenceSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.StandardReference;
        }

        public override bool  OnButtonOkClicked()
        {
            var query = new AppStandardReferenceQuery("a");
            query.Select(query);

            if (!string.IsNullOrEmpty(txtStandardReferenceID.Text))
            {
                if (cboFilterStandardReferenceID.SelectedIndex == 1)
                    query.Where(query.StandardReferenceID == txtStandardReferenceID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtStandardReferenceID.Text);
                    query.Where(query.StandardReferenceID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtStandardReferenceName.Text))
            {
                if (cboFilterStandardReferenceName.SelectedIndex == 1)
                    query.Where(query.StandardReferenceName == txtStandardReferenceName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtStandardReferenceName.Text);
                    query.Where(query.StandardReferenceName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtStandardReferenceGroup.Text))
            {
                if (cboStandardReferenceGroup.SelectedIndex == 1)
                    query.Where(query.StandardReferenceGroup == txtStandardReferenceGroup.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtStandardReferenceGroup.Text);
                    query.Where(query.StandardReferenceGroup.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtItemID.Text) || !string.IsNullOrEmpty(txtItemName.Text))
            {
                var queryItem = new AppStandardReferenceItemQuery("b");
                if (!string.IsNullOrEmpty(txtItemID.Text))
                {
                    if (cboFilterItemID.SelectedIndex == 1)
                        query.Where(queryItem.ItemID == txtItemID.Text);
                    else
                    {
                        string searchTextContain = string.Format("%{0}%", txtItemID.Text);
                        query.Where(queryItem.ItemID.Like(searchTextContain));
                    }
                }
                if (!string.IsNullOrEmpty(txtItemName.Text))
                {
                    if (cboFilterItemName.SelectedIndex == 1)
                        query.Where(queryItem.ItemName == txtItemName.Text);
                    else
                    {
                        string searchTextContain = string.Format("%{0}%", txtItemName.Text);
                        query.Where(queryItem.ItemName.Like(searchTextContain));
                    }
                }

                //Join
                query.InnerJoin(queryItem).On(query.StandardReferenceID == queryItem.StandardReferenceID);
                query.es.Distinct = true;
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}