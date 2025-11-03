using System;
using System.Collections.Generic;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ChartOfAccountSearch : BasePageDialog
    {
        public class SearchValue
        {
            public string ChartOfAccountCode;
            public string ChartOfAccountName;
            public string AccountLevel;
            public string GeneralAccount;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboAccountLevel, AppEnum.StandardReference.AcctLevel);
            ProgramID = AppConstant.Program.CHART_OF_ACCOUNT;
            //if (Session[SessionNameForQuery] != null)
            //{
            //    ChartOfAccountSearch.SearchValue sv = (ChartOfAccountSearch.SearchValue)Session[SessionNameForQuery];
            //    txtAccountID.Text = sv.ChartOfAccountCode;
            //    txtAccountName.Text = sv.ChartOfAccountName;
            //    cboAccountLevel.SelectedValue = sv.AccountLevel;
            //    txtGeneralAccount.Text = sv.GeneralAccount;
            //}
        }

        public override bool OnButtonOkClicked()
        {
            //var sv = new SearchValue();

            //if (!string.IsNullOrEmpty(txtAccountID.Text))
            //{
            //    sv.ChartOfAccountCode = txtAccountID.Text;
            //}
            //if (!string.IsNullOrEmpty(txtAccountName.Text))
            //{
            //    sv.ChartOfAccountName = txtAccountName.Text;
            //}
            //if (!string.IsNullOrEmpty(cboAccountLevel.Text))
            //{
            //    int aLevel = 0;
            //    if (int.TryParse(cboAccountLevel.SelectedValue, out aLevel))
            //        if (aLevel != 0)
            //            sv.AccountLevel = aLevel.ToString();
            //}
            //if (!string.IsNullOrEmpty(txtGeneralAccount.Text))
            //{
            //    sv.GeneralAccount = txtGeneralAccount.Text;
            //}

            ChartOfAccountsQuery query = new ChartOfAccountsQuery("a");

            if (!string.IsNullOrEmpty(txtAccountID.Text))
            {
                query.Where(query.ChartOfAccountCode == txtAccountID.Text);
            }
            if (!string.IsNullOrEmpty(txtAccountName.Text))
            {
                query.Where(query.ChartOfAccountName.Like("%" + txtAccountName.Text + "%"));
            }
            if (!string.IsNullOrEmpty(cboAccountLevel.Text))
            {
                int aLevel = 0;
                if (int.TryParse(cboAccountLevel.SelectedValue, out aLevel))
                    if (aLevel != 0) query.Where(query.AccountLevel == aLevel.ToString());
            }
            if (!string.IsNullOrEmpty(txtGeneralAccount.Text))
            {
               query.Where(query.GeneralAccount == txtGeneralAccount.Text);
            }
            query.Where(query.IsActive == true);
            query.OrderBy(query.ChartOfAccountCode.Ascending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
