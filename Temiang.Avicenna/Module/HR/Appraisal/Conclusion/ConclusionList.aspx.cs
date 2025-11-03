using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Appraisal.Conclusion
{
    public partial class ConclusionList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "##";
            UrlPageDetail = "ConclusionDetail.aspx";

            ProgramID = AppConstant.Program.AppraisalConclusion;
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
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
            string id = dataItem.GetDataKeyValue(AppraisalConclusionMetadata.ColumnNames.ConclusionID).ToString();
            Page.Response.Redirect("ConclusionDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = AppraisalConclusions;
        }

        private DataTable AppraisalConclusions
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                AppraisalConclusionQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (AppraisalConclusionQuery)Session[SessionNameForQuery];
                else
                {
                    query = new AppraisalConclusionQuery("a");
                    query.Select
                        (
                            query.ConclusionID,
                            query.ConclusionName,
                            query.MinValue,
                            query.MaxValue
                        );
                    query.OrderBy(query.MinValue.Ascending);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}