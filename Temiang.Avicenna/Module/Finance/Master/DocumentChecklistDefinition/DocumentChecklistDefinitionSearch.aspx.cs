using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class DocumentChecklistDefinitionSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.GuarantorDocumentChecklistDefinition; //TODO: Isi ProgramID
        }

        public override bool OnButtonOkClicked()
        {
            var query = new AppStandardReferenceItemQuery("a");
            query.Select(
                query.StandardReferenceID,
                query.ItemID,
                query.ItemName,
                query.IsActive,
                query.LastUpdateDateTime,
                query.LastUpdateByUserID
                );
            query.Where(query.StandardReferenceID == AppEnum.StandardReference.DocumentChecklist);
            
            if (!string.IsNullOrEmpty(txtItemName.Text))
            {
                if (cboFilterItemName.SelectedIndex == 1)
                    query.Where(query.ItemName == txtItemName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtItemName.Text);
                    query.Where(query.ItemName.Like(searchTextContain));
                }
            }

            query.OrderBy(query.ItemID.Ascending);
            
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
