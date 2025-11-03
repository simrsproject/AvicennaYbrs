using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using System.Data;

namespace Temiang.Avicenna.Module.AssetManagement.Master
{
    public partial class SanitationActivityResultTemplateSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.SanitationActivityResultTemplate;

            if (!IsPostBack)
            {
                var sr = new AppStandardReferenceItemCollection();
                sr.Query.Where(sr.Query.StandardReferenceID == "WorkTradeItem", sr.Query.ReferenceID == AppSession.Parameter.WorkTradeSanitation);
                sr.Query.Where(sr.Query.IsActive == true);
                sr.LoadAll();

                cboSRWorkTradeItem.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (AppStandardReferenceItem entity in sr)
                {
                    cboSRWorkTradeItem.Items.Add(new RadComboBoxItem(entity.ItemName, entity.ItemID));
                }
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new SanitationActivityResultTemplateQuery("a");
            var wti = new AppStandardReferenceItemQuery("b");
            query.LeftJoin(wti).On(wti.StandardReferenceID == "WorkTradeItem" && wti.ItemID == query.SRWorkTradeItem && wti.ReferenceID == AppSession.Parameter.WorkTradeSanitation);
            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(
                            query.SanitationActivityResultID,
                            query.SRWorkTradeItem,
                            wti.ItemName.As("WorkTradeItemName"),
                            query.ResultTemplateName,
                            query.Result.Substring(100).As("TestResult")
                        );

            if (!string.IsNullOrEmpty(txtResultTemplateName.Text))
            {
                if (cboFilterResultTemplateName.SelectedIndex == 1)
                    query.Where(query.ResultTemplateName == txtResultTemplateName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtResultTemplateName.Text);
                    query.Where(query.ResultTemplateName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboSRWorkTradeItem.SelectedValue))
                query.Where(query.SRWorkTradeItem == cboSRWorkTradeItem.SelectedValue);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}