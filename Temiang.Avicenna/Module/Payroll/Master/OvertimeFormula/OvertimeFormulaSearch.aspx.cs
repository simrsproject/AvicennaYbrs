using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class OvertimeFormulaSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.OvertimeFormula;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new OvertimeQuery("a");
            var sc = new SalaryComponentQuery("b");

            query.Select(
                query.OvertimeID,
                query.OvertimeName,
                query.SalaryComponentID,
                sc.SalaryComponentName,
                query.Notes,
                query.ValidFrom,
                query.ValidTo,
                query.LastUpdateDateTime,
                query.LastUpdateByUserID
                    );
            query.InnerJoin(sc).On
                (
                    query.SalaryComponentID == sc.SalaryComponentID
                );
            if (!string.IsNullOrEmpty(txtOvertimeName.Text))
            {
                if (cboFilteOvertimeName.SelectedIndex == 1)
                    query.Where(query.OvertimeName == txtOvertimeName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtOvertimeName.Text);
                    query.Where(query.OvertimeName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList);

            return true;
        }
    }
}