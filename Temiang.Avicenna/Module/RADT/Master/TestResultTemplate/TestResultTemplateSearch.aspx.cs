using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using System.Data;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class TestResultTemplateSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.TestResultTemplate;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new TestResultTemplateQuery("a");
            var paramedicQuery = new ParamedicQuery("b");
            var itemQuery = new ItemQuery("c");
            
            query.LeftJoin(paramedicQuery).On(query.ParamedicID == paramedicQuery.ParamedicID);
            query.LeftJoin(itemQuery).On(query.ItemID == itemQuery.ItemID);
            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(
                            query.TestResultTemplateID,
                            query.ParamedicID,
                            paramedicQuery.ParamedicName,
                            query.ItemID,
                            itemQuery.ItemName,
                            query.TestResultTemplateName
                        );

            if (!string.IsNullOrEmpty(txtTestResultName.Text))
            {
                if (cboFilterTestResultName.SelectedIndex == 1)
                    query.Where(query.TestResultTemplateName == txtTestResultName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtTestResultName.Text);
                    query.Where(query.TestResultTemplateName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
                query.Where(query.ParamedicID == cboParamedicID.SelectedValue);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ParamedicQuery("a");

            query.es.Top = 15;
            query.Select
                (
                    query.ParamedicID,
                    query.ParamedicName
                );
            query.Where
                (
                    query.Or
                    (
                       query.ParamedicID.Like(searchTextContain),
                       query.ParamedicName.Like(searchTextContain)
                    ),
                    query.IsActive == true

                );
            query.OrderBy(query.ParamedicID.Ascending);

            cboParamedicID.DataSource = query.LoadDataTable();
            cboParamedicID.DataBind();
        }
    }
}
