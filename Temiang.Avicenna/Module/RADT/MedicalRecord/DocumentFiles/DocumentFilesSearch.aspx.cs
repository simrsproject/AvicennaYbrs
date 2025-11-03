using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class DocumentFilesSearch : BasePageDialog
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
            ProgramID = AppConstant.Program.DocumentFiles;//TODO: Isi ProgramID

            ProgramID = ForDocumentChecklist == "0"
                            ? AppConstant.Program.DocumentFiles
                            : AppConstant.Program.GuarantorDocumentFiles;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new DocumentFilesQuery("a");
            var prg = new AppProgramQuery("b");
            var qf = new QuestionFormQuery("c");

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
                (query.QuestionFormID + " - " + qf.QuestionFormName).As("QuestionFormName")
                );
            query.LeftJoin(prg).On(query.ProgramID == prg.ProgramID);
            query.LeftJoin(qf).On(qf.QuestionFormID == query.QuestionFormID);

            if (ForDocumentChecklist == "0")
                query.Where(query.IsUsedForGuarantorChecklist == false);
            else
                query.Where(query.IsUsedForGuarantorChecklist == true);
            if (!string.IsNullOrEmpty(txtDocumentName.Text))
            {
                if (cboFilterDocumentName.SelectedIndex == 1)
                    query.Where(query.DocumentName == txtDocumentName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtDocumentName.Text);
                    query.Where(query.DocumentName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtDocumentNumber.Text))
            {
                if (cboFilterDocumentNumber.SelectedIndex == 1)
                    query.Where(query.DocumentNumber == txtDocumentNumber.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtDocumentNumber.Text);
                    query.Where(query.DocumentNumber.Like(searchTextContain));
                }
            }
            query.OrderBy(query.DocumentNumber.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
