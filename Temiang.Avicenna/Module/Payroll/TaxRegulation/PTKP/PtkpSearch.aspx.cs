using System;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.TaxRegulation
{
    public partial class PtkpSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.Ptkp; //TODO: Isi ProgramID

            if (!IsPostBack)
                StandardReference.InitializeIncludeSpace(cboSRTaxStatus, AppEnum.StandardReference.TaxStatus);
        }

        public override bool OnButtonOkClicked()
        {
            var query = new PtkpQuery("a");
            var tax = new AppStandardReferenceItemQuery("b");

            query.Select(
                        query.PtkpID,
                        query.ValidFrom,
                        query.ValidTo,
                        query.SRTaxStatus,
                        tax.ItemName.As("TaxStatusName"),
                        query.Amount,
                        query.LastUpdateDateTime,
                        query.LastUpdateByUserID
                    );
            query.InnerJoin(tax).On
                    (
                        query.SRTaxStatus == tax.ItemID &
                        tax.StandardReferenceID == "TaxStatus"
                    );

            if (txtValidFrom.SelectedDate != null)
                query.Where(query.ValidFrom == txtValidFrom.SelectedDate.Value.Date);
            if (txtValidTo.SelectedDate != null)
                query.Where(query.ValidTo == txtValidTo.SelectedDate.Value.Date);
            if (!string.IsNullOrEmpty(cboSRTaxStatus.SelectedValue))
                query.Where(query.SRTaxStatus == cboSRTaxStatus.SelectedValue);

            query.OrderBy(query.ValidFrom.Descending, query.PtkpID.Ascending);
            
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
