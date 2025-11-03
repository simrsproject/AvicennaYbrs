using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ItemIdiDetailItem : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            var qProc = new ProcedureQuery();
            qProc.Where(qProc.ProcedureID == (String)DataBinder.Eval(DataItem, ItemIdiProcedureMetadata.ColumnNames.ProcedureID));
            var tab = qProc.LoadDataTable();
            cboProcedureID.DataSource = tab;
            cboProcedureID.DataBind();
            cboProcedureID.SelectedValue = (String)DataBinder.Eval(DataItem, ItemIdiProcedureMetadata.ColumnNames.ProcedureID);
            txtProcedureText.Text = tab.Rows[0]["ProcedureName"].ToString();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ItemIdiProcedureCollection)Session["collItemIdiProcedure"];

                string id = cboProcedureID.SelectedValue;
                bool isExist = false;
                foreach (ItemIdiProcedure item in coll)
                {
                    if (item.ProcedureID.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Procedure ID: {0} has exist", id);
                }
            }
        }

        #region Properties for return entry value

        public String ProcedureID
        {
            get { return cboProcedureID.SelectedValue; }
        }

        public String ProcedureName
        {
            get { return txtProcedureText.Text; }
        }

        #endregion

        protected void cboProcedureID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            ProcedureQuery query = new ProcedureQuery();

            if (AppSession.Parameter.HealthcareInitial == "RSMP")
                query.Where
                (
                    query.Or
                    (
                        query.ProcedureName.Like(searchTextContain),
                        query.ProcedureID.Equal(e.Text)
                    )
                );
            else
                query.Where
                (
                    query.Or
                    (
                        query.ProcedureName.Like(searchTextContain),
                        query.ProcedureID.Like(searchTextContain)
                    )
                );
            query.OrderBy(query.ProcedureName.Ascending);
            query.es.Top = 50;

            cboProcedureID.DataSource = query.LoadDataTable();
            cboProcedureID.DataBind();
        }
        protected void cboProcedureID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ProcedureID"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ProcedureID"].ToString();
        }
        protected void cboProcedureID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value))
            {
                txtProcedureText.Text = string.Empty;
                return;
            }
            var proc = new Procedure();
            proc.LoadByPrimaryKey(e.Value);
            txtProcedureText.Text = proc.ProcedureName;
        }
    }
}