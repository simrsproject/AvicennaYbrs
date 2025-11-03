using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using System.Data;

namespace Temiang.Avicenna.Module.HR.K3RS
{
    public partial class FormsSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.K3RS_Form;

            if (!IsPostBack)
            {
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new K3rsFormQuery("a");
            var template = new K3rsFormTemplateQuery("b");
            query.InnerJoin(template).On(template.TemplateID == query.TemplateID);
            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(
                    query.TransactionNo,
                    query.TransactionDate,
                    query.TemplateID,
                    template.TemplateName,
                    query.Notes,
                    query.Result.Substring(100).As("Result"));

            if (!string.IsNullOrEmpty(txtTransactionNo.Text))
            {
                if (cboFilterTemplateName.SelectedIndex == 1)
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtTransactionNo.Text);
                    query.Where(query.TransactionNo.Like(searchTextContain));
                }
            }
            if (!txtFromDate.SelectedDate.ToString().Trim().Equals(string.Empty) && !txtToDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                query.Where(query.TransactionDate >= txtFromDate.SelectedDate, query.TransactionDate < txtToDate.SelectedDate.Value.AddDays(1));
            if (!string.IsNullOrEmpty(txtTemplateName.Text))
            {
                if (cboFilterTemplateName.SelectedIndex == 1)
                    query.Where(template.TemplateName == txtTemplateName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtTemplateName.Text);
                    query.Where(template.TemplateName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtNotes.Text))
            {
                if (cboFilterTemplateName.SelectedIndex == 1)
                    query.Where(query.Notes == txtNotes.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtNotes.Text);
                    query.Where(query.Notes.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}