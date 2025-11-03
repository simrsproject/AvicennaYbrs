using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class WageStructureAndScalePointsSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.WageStructureAndScalePoints;
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
            var query = new WageStructureAndScaleQuery("a");
            var type = new AppStandardReferenceItemQuery("b");
            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(
                query.WageStructureAndScaleID,
                query.SRWageStructureAndScaleType,
                type.ItemName.As("WageStructureAndScaleTypeName"),
                query.WageStructureAndScaleCode,
                query.WageStructureAndScaleName,
                query.LastUpdateDateTime,
                query.LastUpdateByUserID
                );
            query.InnerJoin(type).On(type.StandardReferenceID == AppEnum.StandardReference.WageStructureAndScaleType && type.ItemID == query.SRWageStructureAndScaleType);


            if (!string.IsNullOrEmpty(cboSRWageStructureAndScaleType.SelectedValue))
            {
                query.Where(query.SRWageStructureAndScaleType == cboSRWageStructureAndScaleType.SelectedValue);
            }
            if (!string.IsNullOrEmpty(txtWageStructureAndScaleName.Text))
            {
                if (cboFilterWageStructureAndScaleName.SelectedIndex == 1)
                    query.Where(query.WageStructureAndScaleName == txtWageStructureAndScaleName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtWageStructureAndScaleName.Text);
                    query.Where(query.WageStructureAndScaleName.Like(searchTextContain));
                }
            }

            query.OrderBy(query.SRWageStructureAndScaleType.Ascending, query.WageStructureAndScaleCode.Ascending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset
        }
    }
}