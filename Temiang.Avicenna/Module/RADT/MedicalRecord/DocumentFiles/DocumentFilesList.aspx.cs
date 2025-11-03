using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class DocumentFilesList : BasePageList
    {
        private string ForDocumentChecklist
        {
            get
            {
                if (string.IsNullOrEmpty(Request.QueryString["dc"]))
                    return "0";
                return Request.QueryString["dc"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "DocumentFilesSearch.aspx?";
            UrlPageDetail = "DocumentFilesDetail.aspx?dc=" + ForDocumentChecklist;

            ProgramID = ForDocumentChecklist == "0" ? AppConstant.Program.DocumentFiles : AppConstant.Program.GuarantorDocumentFiles;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;

            if (!IsPostBack)
            {
                grdList.Columns.FindByUniqueName("IsQuality").Visible = ForDocumentChecklist == "0";
                grdList.Columns.FindByUniqueName("IsLegible").Visible = ForDocumentChecklist == "0";
                grdList.Columns.FindByUniqueName("IsSign").Visible = ForDocumentChecklist == "0";
                grdList.Columns.FindByUniqueName("IsUsedForAnalysis").Visible = ForDocumentChecklist == "0";
                grdList.Columns.FindByUniqueName("File").Visible = ForDocumentChecklist == "0";
                grdList.Columns.FindByUniqueName("DocumentType").Visible = ForDocumentChecklist == "0";
                grdList.Columns.FindByUniqueName("AssessmentType").Visible = ForDocumentChecklist == "0";
                grdList.Columns.FindByUniqueName("HaisMonitoring").Visible = ForDocumentChecklist == "0";
                grdList.Columns.FindByUniqueName("QuestionFormName").Visible = ForDocumentChecklist == "0";
            }
        }
        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"gotoAddUrl('{0}'); args.set_cancel(true);", ForDocumentChecklist);
            return script;
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
            string id = dataItem.GetDataKeyValue(DocumentFilesMetadata.ColumnNames.DocumentFilesID).ToString();
            Page.Response.Redirect("DocumentFilesDetail.aspx?md=" + mode + "&id=" + id + "&dc=" + ForDocumentChecklist, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = DocumentFiless;
        }

        private DataTable DocumentFiless
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                DocumentFilesQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (DocumentFilesQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new DocumentFilesQuery("a");
                    var prg = new AppProgramQuery("b");
                    var qf = new QuestionFormQuery("c");
                    var dt = new AppStandardReferenceItemQuery("d");
                    var astp = new AppStandardReferenceItemQuery("e");
                    var hais = new AppStandardReferenceItemQuery("f");

                    query.Select(
                        query.DocumentFilesID,
                        query.DocumentName,
                        query.DocumentNumber,
                        query.FileTemplateName,
                        query.IsQuality,
                        query.IsLegible,
                        query.IsSign,
                        query.IsActive,
                        query.IsUsedForAnalysis,
                        query.IsUsedForGuarantorChecklist,
                        query.LastUpdateDateTime,
                        query.LastUpdateByUserID,
                        (prg.ProgramID + " - " + prg.ProgramName).As("ProgramName"),
                        (query.QuestionFormID + " - " + qf.QuestionFormName).As("QuestionFormName"),
                        dt.ItemName.As("DocumentType"),
                        astp.ItemName.As("AssessmentType"),
                        hais.ItemName.As("HaisMonitoring")
                        );
                    query.LeftJoin(prg).On(query.ProgramID == prg.ProgramID);
                    query.LeftJoin(qf).On(qf.QuestionFormID == query.QuestionFormID);
                    query.LeftJoin(dt).On(dt.StandardReferenceID == "DocumentFileType" && dt.ItemID == query.SRDocumentFileType);
                    query.LeftJoin(astp).On(astp.StandardReferenceID == "AssessmentType" && astp.ItemID == query.SRAssessmentType);
                    query.LeftJoin(hais).On(hais.StandardReferenceID == "HaisMonitoring" && hais.ItemID == query.SRHaisMonitoring);

                    if (ForDocumentChecklist == "0")
                        query.Where(query.IsUsedForGuarantorChecklist == false);
                    else
                        query.Where(query.IsUsedForGuarantorChecklist == true);

                    query.es.Top = AppSession.Parameter.MaxResultRecord;

                    //Quick Search
                    ApplyQuickSearch(query, "DocumentName", "DocumentNumber");

                    query.OrderBy(query.DocumentNumber.Ascending);
                }
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

