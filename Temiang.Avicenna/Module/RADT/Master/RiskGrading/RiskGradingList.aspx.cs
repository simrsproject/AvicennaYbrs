using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class RiskGradingList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "RiskGradingSearch.aspx";
            UrlPageDetail = "RiskGradingDetail.aspx";

            ProgramID = AppConstant.Program.RiskGrading;

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
            string id = dataItem.GetDataKeyValue(RiskGradingMetadata.ColumnNames.RiskGradingID).ToString();
            Page.Response.Redirect("RiskGradingDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = RiskGradings;
        }

        private DataTable RiskGradings
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                RiskGradingQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (RiskGradingQuery)Session[SessionNameForQuery];
                else
                {
                    query = new RiskGradingQuery();
                    query.OrderBy(query.RiskGradingID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "RiskGradingName", "RiskGradingID");
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
            query.Select
                (
                    query,
                    ref1.ItemName.As("IncidentProbabilityFrequency"),
                    ref2.ItemName.As("IncidentFollowUp")
                );
            query.InnerJoin(ref1).On(query.SRIncidentProbabilityFrequency == ref1.ItemID &&
                                     ref1.StandardReferenceID ==
                                     AppEnum.StandardReference.IncidentProbabilityFrequency);
            query.InnerJoin(ref2).On(query.SRIncidentProbabilityFrequency == ref2.ItemID &&
                                     ref2.StandardReferenceID ==
                                     AppEnum.StandardReference.IncidentFollowUp);
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
