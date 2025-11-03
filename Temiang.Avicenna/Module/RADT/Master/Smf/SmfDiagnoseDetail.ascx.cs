using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class SmfDiagnoseDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            chkIsVisible.Checked = true;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                chkIsVisible.Checked = true;
                return;
            }
            ViewState["IsNewRecord"] = false;
            ComboBox.PopulateWithOneDiagnose(cboDiagnoseID, (String)DataBinder.Eval(DataItem, SmfDiagnoseMetadata.ColumnNames.DiagnoseID));

            chkIsVisible.Checked = (bool)DataBinder.Eval(DataItem, SmfDiagnoseMetadata.ColumnNames.IsVisible);
        }

        protected void cboDiagnoseID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["DiagnoseName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["DiagnoseID"].ToString();
        }

        private DataTable LoadItem(string searchText)
        {
            string searchTextContain = string.Format("%{0}%", searchText);

            var query = new DiagnoseQuery("a");
            query.es.Top = 30;
            query.Select
                (
                    query.DiagnoseID,
                    query.DiagnoseName
                );
            query.Where(
                query.IsActive == true,
                query.Or(
                    query.DiagnoseName.Like(searchTextContain),
                    query.DiagnoseID.Like(searchTextContain)
                    )
                );
            query.OrderBy(query.DiagnoseName.Ascending);

            return query.LoadDataTable();
        }

        protected void cboDiagnoseID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            DataTable tbl = LoadItem(e.Text);
            cboDiagnoseID.DataSource = tbl;
            cboDiagnoseID.DataBind();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            SmfDiagnoseCollection coll;

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                coll = (SmfDiagnoseCollection)Session["collSmfDiagnose"];

                string id = cboDiagnoseID.SelectedValue;
                bool isExist = false;
                foreach (SmfDiagnose item in coll)
                {
                    if (item.DiagnoseID.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item ID: {0} has exist", id);
                }
            }
        }

        #region Properties for return entry value

        public String DiagnoseID
        {
            get { return cboDiagnoseID.SelectedValue; }
        }

        public String DiagnoseName
        {
            get { return cboDiagnoseID.Text; }
        }

        public bool IsVisible
        {
            get { return chkIsVisible.Checked; }
        }
        #endregion

    }
}
