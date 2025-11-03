using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class QuestionFormList : BasePageList
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ToolBarMenuQuickSearch.Enabled = true;
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            UrlPageDetail = "QuestionFormDetail.aspx";
            UrlPageSearch = "QuestionFormSearch.aspx";
            ProgramID = AppConstant.Program.QuestionForm;

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
            string id = dataItem.GetDataKeyValue(QuestionFormMetadata.ColumnNames.QuestionFormID).ToString();
            Page.Response.Redirect(string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id), true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = QuestionFormDataTable;
        }
        private DataTable QuestionFormDataTable
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                QuestionFormQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (QuestionFormQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new QuestionFormQuery("a");
                    query.Select
                        (
                            query.QuestionFormID,
                            query.QuestionFormName,
                            query.RmNO,
                            query.IsSingleEntry,
                            query.RestrictionUserType,
                            query.IsSharingEdit,
                            query.IsUsingApproval,
                            query.IsAskepForm,
                            query.IsModeMapping
                        );
                    query.Where(query.IsActive == true);

                    query.es.Top = AppSession.Parameter.MaxResultRecord;

                    //Quick Search
                    ApplyQuickSearch(query, "QuestionFormName", "QuestionFormID");
                }
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
            set { this.Session[SessionNameForList] = value; }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            QuestionFormDataTable = null;
            grdList.Rebind();
        }
    }
}
