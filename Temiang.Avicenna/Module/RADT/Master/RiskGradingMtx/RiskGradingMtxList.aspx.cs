using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class RiskGradingMtxList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "RiskGradingMtxSearch.aspx";
            UrlPageDetail = "RiskGradingMtxDetail.aspx";

            ProgramID = AppConstant.Program.RiskGradingMtx;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
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
            string id = dataItem.GetDataKeyValue(AppStandardReferenceItemMetadata.ColumnNames.ItemID).ToString();
            Page.Response.Redirect("RiskGradingMtxDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = RiskGradingMtxs;
        }

        private DataTable RiskGradingMtxs
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                AppStandardReferenceItemQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (AppStandardReferenceItemQuery)Session[SessionNameForQuery];
                else
                {
                    query = new AppStandardReferenceItemQuery();
                    query.Where(query.StandardReferenceID == AppEnum.StandardReference.ClinicalImpact);
                    query.OrderBy(query.ItemID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "ItemName", "ItemID");
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string id = dataItem.GetDataKeyValue("ItemID").ToString();

            //Load record
            var query = new RiskGradingMtxQuery("a");
            var ref1 = new AppStandardReferenceItemQuery("b");
            var ref2 = new AppStandardReferenceItemQuery("c");
            var rg = new RiskGradingQuery("d");
            query.Select
                (
                    query,
                    ref1.ItemName.As("IncidentProbabilityFrequency"),
                    ref2.ItemName.As("IncidentFollowUp"),
                    rg.RiskGradingName,
                    rg.RiskGradingColor
                );
            query.InnerJoin(ref1).On(query.SRIncidentProbabilityFrequency == ref1.ItemID &&
                                     ref1.StandardReferenceID ==
                                     AppEnum.StandardReference.IncidentProbabilityFrequency.ToString());
            query.InnerJoin(ref2).On(query.SRIncidentFollowUp == ref2.ItemID &&
                                     ref2.StandardReferenceID ==
                                     AppEnum.StandardReference.IncidentFollowUp.ToString());
            query.InnerJoin(rg).On(query.RiskGradingID == rg.RiskGradingID);
            query.Where(query.SRClinicalImpact == id);
            query.OrderBy(query.SRIncidentProbabilityFrequency.Ascending, query.SRIncidentFollowUp.Ascending);

            //Apply
            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        public System.Drawing.Color GetColorOfGradingColor(object GradingColor)
        {
            System.Drawing.Color color = System.Drawing.Color.White;
            switch (GradingColor.ToString())
            {
                case "Blue":
                    {
                        color = System.Drawing.Color.Blue;
                        break;
                    }
                case "Green":
                    {
                        color = System.Drawing.Color.Green;
                        break;
                    }
                case "Yellow":
                    {
                        color = System.Drawing.Color.Yellow;
                        break;
                    }
                case "Red":
                    {
                        color = System.Drawing.Color.Red;
                        break;
                    }
            }

            return color;
        }
    }
}
