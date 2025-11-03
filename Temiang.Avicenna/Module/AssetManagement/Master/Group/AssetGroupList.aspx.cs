using System;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;


namespace Temiang.Avicenna.Module.AssetManagement.Master
{
    public partial class AssetGroupList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "AssetGroupSearch.aspx";
            UrlPageDetail = "AssetGroupDetail.aspx";

            WindowSearch.Height = 170;

            ProgramID = AppConstant.Program.ASSET_GROUP;
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(AssetGroupMetadata.ColumnNames.AssetGroupId).ToString();
            Page.Response.Redirect("AssetGroupDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            this.GenerateGrid();
        }

        private void GenerateGrid()
        {
            var assetGroupId = string.Empty;
            var groupName = string.Empty;

            // set search value here..
            if (Session[SessionNameForQuery] != null)
            {
                var sv = (AssetGroupSearch.SearchValue)Session[SessionNameForQuery];
                assetGroupId = sv.AssetGroupId;
                groupName = sv.GroupName;
            }

            if (!this.IsPostBack)
            {
                var sortExpr = new GridSortExpression
                                   {
                                       FieldName = AssetGroupMetadata.ColumnNames.AssetGroupId,
                                       SortOrder = GridSortOrder.Ascending
                                   };

                grdList.MasterTableView.SortExpressions.AddSortExpression(sortExpr);
                grdList.MasterTableView.SortExpressions.AllowNaturalSort = false;
            }

            var sb = new System.Text.StringBuilder();
            foreach (GridSortExpression e in grdList.MasterTableView.SortExpressions)
            {
                sb.AppendFormat("{0}^{1}", this.grdList.MasterTableView.SortExpressions[0].FieldName, this.grdList.MasterTableView.SortExpressions[0].SortOrder);
                sb.Append(",");
            }

            int totalCount = AssetGroup.TotalCount(assetGroupId, groupName);
            grdList.VirtualItemCount = totalCount;

            var en = AssetGroup.GetAllWithPaging(this.grdList.CurrentPageIndex, this.grdList.PageSize,
                assetGroupId, groupName,
                sb.ToString());

            var list = en.Select(entity => new GridDataSource(entity)).ToList();
            this.grdList.DataSource = list;
        }

        private class GridDataSource
        {
            private AssetGroup entity;

            public GridDataSource(AssetGroup entity)
            {
                this.entity = entity;
            }

            public string AssetGroupId
            {
                get { return this.entity.AssetGroupId; }
            }

            public string Description
            {
                get { return this.entity.Description; }
            }

            public string GroupName
            {
                get { return this.entity.GroupName; }
            }

            public string ChartOfAccountName
            {
                get { return string.Format("{0} - {1}", this.entity.ChartOfAccountCode, this.entity.ChartOfAccountName); }
            }

            public string CoaAssetDepreciation
            {
                get { return string.Format("{0} - {1}", this.entity.CoaAssetDepreciationCode, this.entity.CoaAssetDepreciationName); }
            }

            public string CoaCostOfDepreciation
            {
                get { return string.Format("{0} - {1}", this.entity.CoaCostOfDepreciationCode, this.entity.CoaCostOfDepreciationName); }
            }

            public string CoaCostOfDestruction
            {
                get { return string.Format("{0} - {1}", this.entity.CoaCostOfDestructionCode, this.entity.CoaCostOfDestructionName); }
            }

            public bool IsActive
            {
                get { return this.entity.IsActive ?? false; }
            }
        }
    }
}
