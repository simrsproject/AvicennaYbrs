using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ImageTemplateSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.ImageTemplate;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new ImageTemplateQuery();
            if (!string.IsNullOrEmpty(txtImageTemplateID.Text))
            {
                if (cboFilterImageTemplateID.SelectedIndex == 1)
                    query.Where(query.ImageTemplateID == txtImageTemplateID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtImageTemplateID.Text);
                    query.Where(query.ImageTemplateID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtImageTemplateName.Text))
            {
                if (cboFilterImageTemplateName.SelectedIndex == 1)
                    query.Where(query.ImageTemplateName == txtImageTemplateName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtImageTemplateName.Text);
                    query.Where(query.ImageTemplateName.Like(searchTextContain));
                }
            }
            query.OrderBy(query.ImageTemplateID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
