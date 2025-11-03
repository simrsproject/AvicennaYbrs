using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Appraisal
{
    public partial class ParticipantList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AppraisalParticipant;

            UrlPageSearch = "ParticipantSearch.aspx";
            UrlPageDetail = "ParticipantDetail.aspx";

            WindowSearch.Height = 400;
            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;

            if (!IsPostBack)
            { 
            }
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
            string id = dataItem.GetDataKeyValue(AppraisalParticipantMetadata.ColumnNames.ParticipantID).ToString();
            Page.Response.Redirect("ParticipantDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = AppraisalParticipants;
        }

        private DataTable AppraisalParticipants
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                AppraisalParticipantQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (AppraisalParticipantQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new AppraisalParticipantQuery("a");
                    var type = new AppStandardReferenceItemQuery("b");
                    var quarter = new AppStandardReferenceItemQuery("c");
                    query.Select(query, type.ItemName.As("AppraisalType"), quarter.ItemName.As("QuarterPeriod"));
                    query.LeftJoin(type).On(type.StandardReferenceID == AppEnum.StandardReference.AppraisalType.ToString() && type.ItemID == query.SRAppraisalType);
                    query.LeftJoin(quarter).On(quarter.StandardReferenceID == AppEnum.StandardReference.QuarterPeriod.ToString() && quarter.ItemID == query.SRQuarterPeriod);
                    query.Where(query.IsScoringRecapitulation == true);
                    query.OrderBy(query.PeriodYear.Ascending, query.ParticipantID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "ParticipantName", "PeriodYear");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}