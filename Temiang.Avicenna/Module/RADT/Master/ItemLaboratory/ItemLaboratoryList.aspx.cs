using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ItemLaboratoryList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            UrlPageSearch = "ItemLaboratorySearch.aspx";
            UrlPageDetail = "ItemLaboratoryDetail.aspx";

            ProgramID = AppConstant.Program.LaboratoryItem;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
        }

        public override void OnMenuEditClick(Telerik.Web.UI.GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(Telerik.Web.UI.GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(ItemMetadata.ColumnNames.ItemID).ToString();
            Page.Response.Redirect("ItemLaboratoryDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ItemLaboratorys;
        }

        private DataTable ItemLaboratorys
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ItemQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (ItemQuery)Session[SessionNameForQuery];
                else
                {
                    ItemLaboratoryQuery laboratoryQuery = new ItemLaboratoryQuery("b");
                    ItemGroupQuery grp = new ItemGroupQuery("c");
                    query = new ItemQuery("a");
                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Where(query.SRItemType == BusinessObject.Reference.ItemType.Laboratory);
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
                    query.OrderBy(query.ItemGroupID.Ascending, query.ItemID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "ItemName", "ItemID");

                    var profile = new ItemLaboratoryProfileCollection();
                    profile.LoadAll();

                    if (profile.Count > 0) query.Where(query.ItemID.NotIn(profile.Select(p => p.DetailItemID)));
                }

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }

        private DataTable ItemLaboratoryProfiles(string parentItemID)
        {
            var profile = new ItemLaboratoryProfileQuery("a");
            var item = new ItemQuery("b");
            var lab = new ItemLaboratoryQuery("c");

            profile.Select(
                profile.ParentItemID,
                profile.DetailItemID,
                item.ItemName.As("DetailItemName"),
                profile.DisplaySequence
                );
            profile.InnerJoin(item).On(profile.DetailItemID == item.ItemID);
            profile.InnerJoin(lab).On(profile.DetailItemID == lab.ItemID);
            profile.Where(profile.ParentItemID == parentItemID);
            return profile.LoadDataTable();
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            switch (e.DetailTableView.Name)
            {
                case "detail":
                    e.DetailTableView.DataSource = ItemLaboratoryProfiles(e.DetailTableView.ParentItem.GetDataKeyValue("ItemID").ToString());
                    break;
                case "detail2":
                    e.DetailTableView.DataSource = ItemLaboratoryProfiles(e.DetailTableView.ParentItem.GetDataKeyValue("DetailItemID").ToString());
                    break;
                case "detail3":
                    e.DetailTableView.DataSource = ItemLaboratoryProfiles(e.DetailTableView.ParentItem.GetDataKeyValue("DetailItemID").ToString());
                    break;
            }
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "order")
            {
                var args = e.CommandArgument.ToString().Split('|');

                var profiles = new ItemLaboratoryProfileCollection();
                profiles.Query.Where(profiles.Query.ParentItemID == args[0]);
                profiles.Query.Load();

                var profile1 = profiles.FindByPrimaryKey(args[0], args[1]);
                if (profile1.DisplaySequence == 0) return;
                else
                {
                    var profile2 = profiles.Where(p => p.DisplaySequence == profile1.DisplaySequence - 1).Take(1).SingleOrDefault();
                    if (profile2 != null) profile2.DisplaySequence += 1;

                    profile1.DisplaySequence -= 1;

                    profiles.Save();

                    e.Item.OwnerTableView.Rebind();
                }
            }
        }
    }
}