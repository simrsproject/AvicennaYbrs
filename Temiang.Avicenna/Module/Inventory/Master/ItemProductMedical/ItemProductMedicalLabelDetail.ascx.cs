using System;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ItemProductMedicalLabelDetail : BaseUserControl
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
            ComboBox.PopulateWithOneLabel(cboLabelID, (String)DataBinder.Eval(DataItem, ItemProductMedicLabelMetadata.ColumnNames.LabelID));
        }

        protected void cboLabelID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["LabelName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["LabelID"].ToString();
        }

        protected void cboLabelID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            DataTable tbl = LoadItem(e.Text);
            cboLabelID.DataSource = tbl;
            cboLabelID.DataBind();
        }

        private DataTable LoadItem(string searchText)
        {
            string searchTextContain = string.Format("%{0}%", searchText);
            var query = new LabellQuery("a");
            query.es.Top = 30;
            query.Select
                (
                    query.LabelID,
                    query.LabelName
                );
            query.Where(
                query.IsActive == true,
                query.Or(
                    query.LabelName.Like(searchTextContain),
                    query.LabelID.Like(searchTextContain)
                    )
                );
            query.OrderBy(query.LabelName.Ascending);

            return query.LoadDataTable();
        }

        #region Properties for return entry value

        public String LabelID
        {
            get { return cboLabelID.SelectedValue; }
        }

        public String LabelName
        {
            get { return cboLabelID.Text; }
        }

        #endregion

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                ItemProductMedicLabelCollection coll = (ItemProductMedicLabelCollection)Session["collItemProductMedicLabelCollection"];

                string LabelID = cboLabelID.SelectedValue;
                bool isExist = false;
                foreach (ItemProductMedicLabel item in coll)
                {
                    if (item.LabelID.Equals(LabelID))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item ID: {0} has exist", LabelID);
                }
            }
        }
    }
}
