using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class SmfSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.Smf;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new SmfQuery("a");
            var stdFeeType = new AppStandardReferenceItemQuery("b");
            var stdAssType = new AppStandardReferenceItemQuery("c");

            query.LeftJoin(stdFeeType).On(query.SRParamedicFeeCaseType == stdFeeType.ItemID &&
                                          stdFeeType.StandardReferenceID ==
                                          AppEnum.StandardReference.ParamedicFeeCaseType);
            query.LeftJoin(stdAssType).On(query.SRParamedicFeeCaseType == stdAssType.ItemID &&
                                          stdAssType.StandardReferenceID ==
                                          AppEnum.StandardReference.AssessmentType);

            query.Select
                (
                    query.SmfID,
                    query.SmfName,
                    stdFeeType.ItemName.As("ParamedicFeeCaseTypeName"),
                    stdAssType.ItemName.As("AssessmentTypeName")
                );

            if (!string.IsNullOrEmpty(txtSmfID.Text))
            {
                if (cboFilterSmfID.SelectedIndex == 1)
                    query.Where(query.SmfID == txtSmfID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtSmfID.Text);
                    query.Where(query.SmfID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtSmfName.Text))
            {
                if (cboFilterSmfName.SelectedIndex == 1)
                    query.Where(query.SmfName == txtSmfName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtSmfName.Text);
                    query.Where(query.SmfName.Like(searchTextContain));
                }
            }
            query.OrderBy(query.SmfID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
