using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class StandardSalarySearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.StandardSalary; //TODO: Isi ProgramID
        }

        public override bool OnButtonOkClicked()
        {
            var grade = new PositionGradeQuery("b");
            var query = new StandardSalaryQuery("a");

            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(
                            query.StandardSalaryID,
                            query.PositionGradeID,
                            grade.PositionGradeCode,
                            grade.PositionGradeName,
                            query.ValidFrom,
                            query.ValidTo,
                            query.LastUpdateDateTime,
                            query.LastUpdateByUserID
                        );
            query.InnerJoin(grade).On
                   (
                       query.PositionGradeID == grade.PositionGradeID
                   );
            query.OrderBy(query.ValidFrom.Descending, query.PositionGradeID.Ascending);


            if (!string.IsNullOrEmpty(txtPositionGradeName.Text))
            {
                if (cboFilterPositionGradeName.SelectedIndex == 1)
                    query.Where(grade.PositionGradeName == txtPositionGradeName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtPositionGradeName.Text);
                    query.Where(grade.PositionGradeName.Like(searchTextContain));
                }
            }

            if (txtValidFrom.SelectedDate != null)
            {
                query.Where(query.ValidFrom == txtValidFrom.SelectedDate.Value.Date);
            }
            if (txtValidTo.SelectedDate != null)
            {
                query.Where(query.ValidTo == txtValidTo.SelectedDate.Value.Date);
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
