using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Master.HRBase
{
    public partial class ProfessionSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.Profession;

            if (!IsPostBack)
                StandardReference.InitializeIncludeSpace(cboSRProfessionGroup, AppEnum.StandardReference.ProfessionGroup);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
            }
        }

        public override bool OnButtonOkClicked()
        {
            OnButtonOkClick();

            return true;
        }

        private void OnButtonOkClick()
        {
            var query = new AppStandardReferenceItemQuery("a");
            var gr = new AppStandardReferenceItemQuery("b");
            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(
                query.ItemID,
                query.ItemName,
                gr.ItemName.As("ProfessionGroupName"),
                query.IsActive,
                query.LastUpdateDateTime,
                query.LastUpdateByUserID
                );
            query.LeftJoin(gr).On(gr.StandardReferenceID == AppEnum.StandardReference.ProfessionGroup && gr.ItemID == query.ReferenceID);
            query.Where(query.StandardReferenceID == AppEnum.StandardReference.ProfessionType);
            
            if (!string.IsNullOrEmpty(txtProfessionType.Text))
            {
                if (cboFilterProfessionType.SelectedIndex == 1)
                    query.Where(query.ItemName == txtProfessionType.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtProfessionType.Text);
                    query.Where(query.ItemName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboSRProfessionGroup.SelectedValue))
            {
                 query.Where(query.ReferenceID == cboSRProfessionGroup.SelectedValue);
            }

            query.OrderBy(query.ItemID.Ascending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset
        }
    }
}