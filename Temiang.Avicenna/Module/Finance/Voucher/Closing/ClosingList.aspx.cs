using System;
using System.Collections.Generic;
using System.Globalization;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Voucher.Closing
{
    public partial class ClosingList : BasePageList
    {
        public bool GetUserUnapprovable() {
            return base.IsUserUnApproveAble;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "ClosingSearch.aspx";
            UrlPageDetail = "ClosingDetail.aspx";

            this.WindowSearch.Height = 400;
            ProgramID = AppConstant.Program.CLOSING_PERIODE;

            this.grdList.SortCommand += new GridSortCommandEventHandler(grdList_SortCommand);
            ToolBarMenuSearch.Visible = false;
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
            string ivd = dataItem.GetDataKeyValue(PostingStatusMetadata.ColumnNames.PostingId).ToString();

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
                sortExpr.FieldName = PostingStatusMetadata.ColumnNames.PostingId;
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
            foreach (PostingStatus entity in PostingStatus.Get())
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

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);
            if (!(sourceControl is RadGrid))
                return;
            if (eventArgument.Contains("openPosting"))
            {
                var param = eventArgument.Split('|');
                var ps = new PostingStatus();
                if (ps.LoadByPrimaryKey(Convert.ToInt32(param[1]))) {
                    ps.IsEnabled = false;
                    ps.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    ps.LastUpdateDateTime = DateTime.Now;
                    ps.Save();

                    grdList.Rebind();
                }
            }
        }

        protected class GridItem
        {
            // Fields
            private readonly PostingStatus Entity;

            // Methods
            public GridItem(PostingStatus entity)
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
            public DateTime DateEdited
            {
                get
                {
                    return this.Entity.LastUpdateDateTime.Value;
                }
            }
            public string JournalGroupName
            {
                get
                {
                    return this.Entity.GetColumn("JournalGroupName").ToString();
                }
            }
        }
    }
}