using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class WageStructureAndScaleSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.WageStructureAndScale;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRWageStructureAndScaleType, AppEnum.StandardReference.WageStructureAndScaleType, false);
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
            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(
                query.StandardReferenceID,
                query.ItemID,
                query.ItemName,
                query.Note,
                query.NumericValue,
                query.LastUpdateDateTime,
                query.LastUpdateByUserID
                );
            query.Where(query.StandardReferenceID == AppEnum.StandardReference.WageStructureAndScaleType.ToString());
            if (!string.IsNullOrEmpty(cboSRWageStructureAndScaleType.SelectedValue))
            {
                query.Where(query.ItemID == cboSRWageStructureAndScaleType.SelectedValue);
            }

            query.OrderBy(query.ItemID.Ascending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset
        }
    }
}