using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;


namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class IncentivePositionSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.IncentivePosition;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
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
            var gr = new AppStandardReferenceItemQuery("b");
            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(
                query.ItemID,
                query.ItemName,
                gr.ItemName.As("ProfessionPositionGroupName"),
                query.NumericValue,
                query.IsActive,
                query.LastUpdateDateTime,
                query.LastUpdateByUserID
                );
            query.LeftJoin(gr).On(gr.StandardReferenceID == AppEnum.StandardReference.IncentivePositionGroup && gr.ItemID == query.ReferenceID);
            query.Where(query.StandardReferenceID == AppEnum.StandardReference.IncentivePosition);

            if (!string.IsNullOrEmpty(txtIncentivePosition.Text))
            {
                if (cboFilterIncentivePosition.SelectedIndex == 1)
                    query.Where(query.ItemName == txtIncentivePosition.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtIncentivePosition.Text);
                    query.Where(query.ItemName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtIncentivePositionGroup.Text))
            {
                if (cboFilterIncentivePositionGroup.SelectedIndex == 1)
                    query.Where(gr.ItemName == txtIncentivePositionGroup.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtIncentivePositionGroup.Text);
                    query.Where(gr.ItemName.Like(searchTextContain));
                }
            }

            query.OrderBy(query.ItemID.Ascending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset
        }
    }
}