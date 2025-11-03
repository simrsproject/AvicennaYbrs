using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ItemLaboratorySearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.LaboratoryItem;

            if (!IsPostBack)
            {
                var group = new ItemGroupCollection();
                group.Query.Where(group.Query.IsActive == true, group.Query.SRItemType == ItemType.Laboratory);
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
            var laboratoryQuery = new ItemLaboratoryQuery("b");
            var grp = new ItemGroupQuery("c");
            query.LeftJoin(laboratoryQuery).On(query.ItemID == laboratoryQuery.ItemID);
            query.LeftJoin(grp).On(query.ItemGroupID == grp.ItemGroupID);

            query.Select(
                        query.ItemID,
                        query.ItemGroupID,
                        grp.ItemGroupName,
                        query.ItemName,
                        query.IsActive,
                        laboratoryQuery.ReportRLID,
                        laboratoryQuery.IsAdminCalculation,
                        laboratoryQuery.IsAllowVariable,
                        laboratoryQuery.IsAllowCito,
                        laboratoryQuery.IsAllowDiscount,
                        laboratoryQuery.IsAssetUtilization,
                        query.Notes
                    );

            query.Where(query.SRItemType == ItemType.Laboratory);

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
            
            query.OrderBy(query.ItemID.Ascending);

            //var profile = new ItemLaboratoryProfileCollection();
            //profile.LoadAll();

            //if (profile.Count > 0) query.Where(query.ItemID.NotIn(profile.Select(p => p.DetailItemID)));

            if (isEsTop)
                query.es.Top = AppSession.Parameter.MaxResultRecord;

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
