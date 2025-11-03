using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class PayrollPeriodSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PayrollPeriod; //TODO: Isi ProgramID
        }

        public override bool OnButtonOkClicked()
        {
            AppStandardReferenceItemQuery sequent = new AppStandardReferenceItemQuery("b"); 
            var query = new PayrollPeriodQuery("a");
            query.Select(
                sequent.ItemName.As("PaySequentName"),
                query
            );
            query.InnerJoin(sequent).On
                   (
                       query.SRPaySequent == sequent.ItemID &
                       sequent.StandardReferenceID == "PaySequent"
                   );
            query.OrderBy(query.PayrollPeriodCode.Descending, query.SRPaySequent.Ascending);
            if (!string.IsNullOrEmpty(txtPayrollPeriodCode.Text))
            {
                if (cboFilterPayrollPeriodCode.SelectedIndex == 1)
                    query.Where(query.PayrollPeriodName == txtPayrollPeriodCode.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtPayrollPeriodCode.Text);
                    query.Where(query.PayrollPeriodName.Like(searchTextContain));
                }
            }
           
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
