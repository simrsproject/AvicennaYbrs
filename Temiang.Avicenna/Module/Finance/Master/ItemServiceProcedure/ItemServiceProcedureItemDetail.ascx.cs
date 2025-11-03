using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ItemServiceProcedureItemDetail : BaseUserControl
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
            cboItemID.Enabled = false;
            PopulateCboItemID(cboItemID, (String)DataBinder.Eval(DataItem, ItemServiceProcedureMetadata.ColumnNames.ItemID), false);
        }

        #region ComboBox ServiceUnitID
        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboItemID((RadComboBox)sender, e.Text, true);
        }

        private void PopulateCboItemID(RadComboBox comboBox, string textSearch, bool isNew)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new ItemQuery("a");
            var itmq = new ItemServiceQuery("b");
            query.InnerJoin(itmq).On(itmq.ItemID == query.ItemID);
            query.Where(query.SRItemType == BusinessObject.Reference.ItemType.Service);

            if (isNew)
                query.Where(
                    query.ItemName.Like(searchTextContain),
                    query.IsActive == true);
            else
                query.Where(
                    query.ItemID == textSearch);

            query.Select(query.ItemID, query.ItemName);

            query.es.Top = 30;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["ItemID"].ToString();
            }
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        #endregion

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ItemServiceProcedureCollection)Session["collItemServiceProcedure"];

                bool isExist = false;
                foreach (ItemServiceProcedure row in coll)
                {
                    if (row.ItemID.Equals(cboItemID.SelectedValue))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item: {0} has exist", cboItemID.Text);
                }
            }
        }

        #region Properties for return entry value
        public String ItemID
        {
            get { return cboItemID.SelectedValue; }
        }
        public String ItemName
        {
            get { return cboItemID.Text; }
        }

        #endregion
    }
}