using System;
using System.Collections.Generic;
using System.Globalization;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.AssetManagement.Process
{
    public partial class ClosingList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "ClosingSearch.aspx";
            UrlPageDetail = "ClosingDetail.aspx";

            this.WindowSearch.Height = 400;
            ProgramID = AppConstant.Program.ASSET_CLOSING;

            this.grdList.SortCommand += new GridSortCommandEventHandler(grdList_SortCommand);
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
            string ivd = dataItem.GetDataKeyValue(AssetPostingStatusMetadata.ColumnNames.PostingId).ToString();

            string url = string.Format("{0}?md={1}&ivd={2}", UrlPageDetail, mode, ivd);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            this.GenerateGrid();
        }

        protected void GenerateGrid()
        {
            if (!this.IsPostBack)
            {
                GridSortExpression sortExpr = new GridSortExpression();
                sortExpr.FieldName = AssetPostingStatusMetadata.ColumnNames.PostingId;
                sortExpr.SortOrder = GridSortOrder.Descending;

                grdList.MasterTableView.SortExpressions.AddSortExpression(sortExpr);
                grdList.MasterTableView.SortExpressions.AllowNaturalSort = false;
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (GridSortExpression e in grdList.MasterTableView.SortExpressions)
            {
                sb.AppendFormat("{0}^{1}", this.grdList.MasterTableView.SortExpressions[0].FieldName, this.grdList.MasterTableView.SortExpressions[0].SortOrder);
                sb.Append(",");
            }

            List<GridItem> items = new List<GridItem>();
            foreach (AssetPostingStatus entity in AssetPostingStatus.Get())
                items.Add(new GridItem(entity));

            grdList.DataSource = items;
        }

        protected void grdList_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
        {
            if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
            {
                GridSortExpression sortExpr = new GridSortExpression();
                sortExpr.FieldName = e.SortExpression;
                sortExpr.SortOrder = e.NewSortOrder;

                grdList.MasterTableView.SortExpressions.Clear();
                grdList.MasterTableView.SortExpressions.AddSortExpression(sortExpr);

                grdList.Rebind();
            }
        }

        protected class GridItem
        {
            // Fields
            private readonly AssetPostingStatus Entity;

            // Methods
            public GridItem(AssetPostingStatus entity)
            {
                this.Entity = entity;
            }

            public int PostingId
            {
                get
                {
                    return (int)this.Entity.PostingId;
                }
            }
            public string StatusImage
            {
                get
                {
                    if (this.Entity.IsEnabled.Value)
                    {
                        return "ClosedStatus.gif";
                    }
                    else
                    {
                        return "OpenStatus.gif";
                    }
                }
            }
            public string Month
            {
                get
                {
                    int m;
                    if (int.TryParse(this.Entity.Month, out m))
                        return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m);
                    else
                        return "-";
                }
            }
            public string Year
            {
                get
                {
                    return this.Entity.Year;
                }
            }
            public string Periode
            {
                get
                {
                    return string.Format("{0} - {1}", this.Month, this.Year);
                }
            }
            public bool IsClosed
            {
                get
                {
                    return this.Entity.IsEnabled.Value;
                }
            }
            public string EditedBy
            {
                get
                {
                    return this.Entity.LastUpdateByUserID;
                }
            }
            public DateTime? DateEdited
            {
                get
                {
                    return this.Entity.LastUpdateDateTime;
                }
            }
        }
    }
}