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
    public partial class SmfItemServiceDetail : BaseUserControl
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
            ComboBox.PopulateWithOneItem(cboItemID, (String)DataBinder.Eval(DataItem, SmfItemServiceMetadata.ColumnNames.ItemID));

            chkIsVisible.Checked = (bool)DataBinder.Eval(DataItem, SmfItemServiceMetadata.ColumnNames.IsVisible);
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        private DataTable LoadItem(string searchText)
        {
            string searchTextContain = string.Format("%{0}%", searchText);
            var query = new ItemQuery("a");
            query.es.Top = 30;
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );
            query.Where(
                query.SRItemType.NotIn(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen), 
                query.IsActive == true,
                query.Or(
                    query.ItemName.Like(searchTextContain),
                    query.ItemID.Like(searchTextContain)
                    )
                );
            query.OrderBy(query.ItemName.Ascending);

            return query.LoadDataTable();
        }

        protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            DataTable tbl = LoadItem(e.Text);
            cboItemID.DataSource = tbl;
            cboItemID.DataBind();
        }




        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            SmfItemServiceCollection coll;

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                coll = (SmfItemServiceCollection)Session["collSmfItemService"];

                string id = cboItemID.SelectedValue;
                bool isExist = false;
                foreach (SmfItemService item in coll)
                {
                    if (item.ItemID.Equals(id))
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

        public String ItemID
        {
            get { return cboItemID.SelectedValue; }
        }

        public String ItemName
        {
            get { return cboItemID.Text; }
        }

        public bool IsVisible
        {
            get { return chkIsVisible.Checked; }
        }
        #endregion

    }
}
