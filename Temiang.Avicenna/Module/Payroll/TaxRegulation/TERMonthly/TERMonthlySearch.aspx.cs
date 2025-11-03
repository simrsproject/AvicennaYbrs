using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.TaxRegulation
{
    public partial class TERMonthlySearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.TERMonthly; //TODO: Isi ProgramID

            if (!IsPostBack)
                StandardReference.InitializeIncludeSpace(cboSRTaxStatus, AppEnum.StandardReference.TaxStatus, "TAX");
        }

        public override bool OnButtonOkClicked()
        {
            var query = new TERMonthlyQuery("a");
            var ts = new AppStandardReferenceItemQuery("b");

            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select
                (
                    query.TERMonthlyID,
                    query.ValidFrom,
                    query.SRTaxStatus,
                    ts.ItemName.As("TaxStatusName"),
                    query.LastUpdateDateTime,
                    query.LastUpdateByUserID
                );
            query.InnerJoin(ts).On(ts.StandardReferenceID == AppEnum.StandardReference.TaxStatus.ToString() && ts.ItemID == query.SRTaxStatus);
            query.OrderBy(query.ValidFrom.Descending, query.SRTaxStatus.Ascending);

            if (txtValidFrom.SelectedDate != null)
            {
                query.Where(query.ValidFrom == txtValidFrom.SelectedDate.Value.Date);
            }

            if (!string.IsNullOrEmpty(cboSRTaxStatus.SelectedValue))
            {
                query.Where(query.SRTaxStatus == cboSRTaxStatus.SelectedValue);
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}