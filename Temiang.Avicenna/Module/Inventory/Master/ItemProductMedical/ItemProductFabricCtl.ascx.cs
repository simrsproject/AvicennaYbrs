using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ItemProductFabricCtl : BaseUserControl
    {
        private RadComboBox CboFabricId
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboFabricID"); }
        }

        public object DataItem { get; set; }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            var query = new FabricQuery("a");
            query.Select(query.FabricID, query.FabricName);
            query.Where(query.FabricID == DataBinder.Eval(DataItem, ItemProductFabricMetadata.ColumnNames.FabricID).ToString());
            var dtb = query.LoadDataTable();

            cboFabricID.DataSource = dtb;
            cboFabricID.DataBind();

            cboFabricID.SelectedValue = DataBinder.Eval(DataItem, ItemProductFabricMetadata.ColumnNames.FabricID).ToString();
        }

        protected void cboFabricID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new FabricQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.FabricID,
                    query.FabricName
                );
            query.Where
                (
                    query.FabricID != CboFabricId.SelectedValue,
                    query.FabricName.Like(searchTextContain),
                    query.IsActive == true
                );

            cboFabricID.DataSource = query.LoadDataTable();
            cboFabricID.DataBind();
        }

        protected void cboFabricID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["FabricName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["FabricID"].ToString();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (BusinessObject.ItemProductFabricCollection)Session["collItemProductFabric"];

                var id = cboFabricID.SelectedValue;
                var isExist = coll.Any(row => row.FabricID.Equals(id));
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Factory: {0} has exist", cboFabricID.Text);
                }
            }
        }

        public String FabricID
        {
            get { return cboFabricID.SelectedValue; }
        }

        public String FabricName
        {
            get { return cboFabricID.Text; }
        }
    }
}