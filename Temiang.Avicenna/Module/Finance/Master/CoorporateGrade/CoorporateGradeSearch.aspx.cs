using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class CoorporateGradeSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.CoorporateGrade;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new CoorporateGradeQuery("a");
            query.Select(query.CoorporateGradeID,
                         query.CoorporateGradeLevel,
                         query.CoorporateGradeMin,
                         query.CoorporateGradeMax,
                         query.CoorporateGradeInterval,
                         query.CreatedDateTime,
                         query.CreatedByUserID,
                         query.LastUpdateDateTime,
                         query.LastUpdateByUserID);
            
            if (!string.IsNullOrEmpty(txtCoorporateGradeLevel.Text))
            {
                query.Where(query.CoorporateGradeLevel == txtCoorporateGradeLevel.Value);
            }
            
            query.OrderBy(query.CoorporateGradeLevel.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}