using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.NursingCare.Master
{
    public partial class NursingAssessmentQuestionList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            UrlPageDetail = "NursingAssessmentQuestionDetail.aspx";
            UrlPageSearch = "NursingAssessmentQuestionSearch.aspx";
            ProgramID = AppConstant.Program.NursingAssessmentQuestion;

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
            string id = dataItem.GetDataKeyValue(QuestionMetadata.ColumnNames.QuestionID).ToString();
            Page.Response.Redirect(string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id), true);
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = ((GridDataItem)e.Item);
                var x = ((DataRowView)item.DataItem).Row["SRAnswerType"].ToString();
                var x3 = ((DataRowView)item.DataItem).Row["SRNadAnswerType"].ToString();
                var QID = ((DataRowView)item.DataItem).Row["QuestionID"].ToString();
                
                var xDO = AppSession.Parameter.NursingAssessmentDO;
                var xDS = AppSession.Parameter.NursingAssessmentDS;
                //CheckBox chkCorrected = item["Corrected"].Controls[0] as CheckBox;
                //if (chkCorrected.Checked)
                if (!x.Equals(x3) && !x3.Equals(string.Empty))
                {
                    item.ForeColor = System.Drawing.Color.Red;
                    item.Font.Bold = true;
                }
            }
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = NursingQuestionDataTable;
        }
        private DataTable NursingQuestionDataTable
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                QuestionQuery query;
                NursingAssessmentDiagnosaQuery qad;
                QuestionQuery qEquiv;

                if (Session[SessionNameForQuery] != null)
                {
                    query = (QuestionQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new QuestionQuery("a");
                    qEquiv = new QuestionQuery("qEquiv");

                    query.LeftJoin(qEquiv).On(query.EquivalentQuestionID == qEquiv.QuestionID)
                        .Where(query.NursingDisplayAs.Coalesce("''") != string.Empty)
                        .Select
                        (
                            query.QuestionID,
                            query.QuestionText,
                            query.NursingDisplayAs,
                            query.SRAnswerType,
                            "<ISNULL((select top 1 SRAnswerType from NursingAssessmentDiagnosa nad where nad.QuestionID = a.QuestionID and nad.SRAnswerType <> a.SRAnswerType),'') SRNadAnswerType>",
                            query.IsActive,
                            qEquiv.QuestionID.As("equivQuestionID"),
                            qEquiv.QuestionText.As("equivQuestionText"),
                            qEquiv.SRAnswerType.As("equivSRAnswerType")
                        );

                    query.es.Distinct = true;

                    //Quick Search
                    ApplyQuickSearch(query, "QuestionText", "QuestionID");

                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
            set { this.Session[SessionNameForList] = value; }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            NursingQuestionDataTable = null;
            grdList.Rebind();
        }
    }
}
