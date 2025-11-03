using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class FeasibilityTestSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CssdFeasibilityTest;

            if (!IsPostBack)
            {
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new CssdFeasibilityTestQuery("a");
            query.Select
                (
                    query.FeasibilityTestNo,
                    query.FeasibilityTestDate,
                    query.FeasibilityTestTime,
                    query.Notes,
                    query.IsApproved,
                    query.IsVoid
                );
            

            if (!string.IsNullOrEmpty(txtFeasibilityTestNo.Text))
            {
                if (cboFilterFeasibilityTestNo.SelectedIndex == 1)
                    query.Where(query.FeasibilityTestNo == txtFeasibilityTestNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtFeasibilityTestNo.Text);
                    query.Where(query.FeasibilityTestNo.Like(searchTextContain));
                }
            }
            if (!txtFeasibilityTestDate.IsEmpty)
                query.Where(query.FeasibilityTestDate == txtFeasibilityTestDate.SelectedDate);

            query.OrderBy(query.FeasibilityTestDate.Descending, query.FeasibilityTestNo.Descending);
            query.es.Top = AppSession.Parameter.MaxResultRecord;

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}