using System;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ItemProductMedicalIndicationDetail : BaseUserControl
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
            ComboBox.PopulateWithOneIndication(cboIndicationID, (String)DataBinder.Eval(DataItem, ItemProductMedicIndicationMetadata.ColumnNames.IndicationID));
        }

        protected void cboIndicationID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["IndicationName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["IndicationID"].ToString();
        }

        protected void cboIndicationID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            DataTable tbl = LoadItem(e.Text);
            cboIndicationID.DataSource = tbl;
            cboIndicationID.DataBind();
        }

        private DataTable LoadItem(string searchText)
        {
            string searchTextContain = string.Format("%{0}%", searchText);
            var query = new IndicationQuery("a");
            query.es.Top = 30;
            query.Select
                (
                    query.IndicationID,
                    query.IndicationName
                );
            query.Where(
                query.IsActive == true,
                query.Or(
                    query.IndicationName.Like(searchTextContain),
                    query.IndicationID.Like(searchTextContain)
                    )
                );
            query.OrderBy(query.IndicationName.Ascending);

            return query.LoadDataTable();
        }

        #region Properties for return entry value

        public String IndicationID
        {
            get { return cboIndicationID.SelectedValue; }
        }

        public String IndicationName
        {
            get { return cboIndicationID.Text; }
        }

        #endregion

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                ItemProductMedicIndicationCollection coll = (ItemProductMedicIndicationCollection)Session["collItemProductMedicIndicationCollection"];

                string IndicationID = cboIndicationID.SelectedValue;
                bool isExist = false;
                foreach (ItemProductMedicIndication item in coll)
                {
                    if (item.IndicationID.Equals(IndicationID))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item ID: {0} has exist", IndicationID);
                }
            }
        }
    }
}
