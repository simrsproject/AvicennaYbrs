using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Charges.Dispensary.PrescriptionSales
{
    public partial class PrescriptionSalesPosSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PrescriptionSalesPos;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new TransPrescriptionQuery("a");
            var unit = new ServiceUnitQuery("b");
            query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
            query.Select
                (
                    query.PrescriptionNo,
                    query.PrescriptionDate,
                    query.AdditionalNote,
                    query.ServiceUnitID,
                    unit.ServiceUnitName,
                    query.IsApproval,
                    query.IsVoid
                );
            query.Where(query.IsPrescriptionReturn == false, query.IsPos.IsNotNull(), query.IsPos == true);
            query.OrderBy(query.PrescriptionNo.Descending);

            if (!txtPrescriptionDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.PrescriptionDate == txtPrescriptionDate.SelectedDate);
            }
            if (!txtPrescriptionNo.Text.Trim().Equals(string.Empty))
            {
                if (cboFilterPrescriptionNo.SelectedIndex == 1)
                    query.Where(query.PrescriptionNo == txtPrescriptionNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtPrescriptionNo.Text);
                    query.Where(query.PrescriptionNo.Like(searchTextContain));
                }
            }
            if (!txtAdditionalNotes.Text.Trim().Equals(string.Empty))
            {
                if (cboFilterAdditionalNotes.SelectedIndex == 1)
                    query.Where(query.AdditionalNote == txtAdditionalNotes.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtAdditionalNotes.Text);
                    query.Where(query.AdditionalNote.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
