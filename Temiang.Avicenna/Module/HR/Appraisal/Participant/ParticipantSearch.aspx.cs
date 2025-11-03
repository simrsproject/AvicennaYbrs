using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Appraisal
{
    public partial class ParticipantSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AppraisalParticipant;

            if (!IsPostBack)
            {
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new AppraisalParticipantQuery("a");
            var type = new AppStandardReferenceItemQuery("b");
            var quarter = new AppStandardReferenceItemQuery("c");
            query.Select(query, type.ItemName.As("AppraisalType"), quarter.ItemName.As("QuarterPeriod"));
            query.LeftJoin(type).On(type.StandardReferenceID == AppEnum.StandardReference.AppraisalType.ToString() && type.ItemID == query.SRAppraisalType);
            query.LeftJoin(quarter).On(quarter.StandardReferenceID == AppEnum.StandardReference.QuarterPeriod.ToString() && quarter.ItemID == query.SRQuarterPeriod);
            query.Where(query.IsScoringRecapitulation == true);

            if (!string.IsNullOrEmpty(txtParticipantName.Text))
            {
                if (cboFilterParticipantName.SelectedIndex == 1)
                    query.Where(query.ParticipantName == txtParticipantName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtParticipantName.Text);
                    query.Where(query.ParticipantName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtPeriodYear.Text))
            {
                query.Where(query.PeriodYear == txtPeriodYear.Text);
            }

            query.OrderBy(query.PeriodYear.Ascending, query.ParticipantID.Ascending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}