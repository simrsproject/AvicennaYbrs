using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ZatActiveInteractionDetail : BaseUserControl
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
            cboInteractionZatActiveID.Enabled = false;
            ComboBox.PopulateWithOneZatActive(cboInteractionZatActiveID, (String)DataBinder.Eval(DataItem, "InteractionZatActiveID"));

            txtInteraction.Text =
                (string)DataBinder.Eval(DataItem, ZatActiveInteractionMetadata.ColumnNames.Interaction);
        }

        #region ComboBox InteractionZatActiveID
        protected void cboInteractionZatActiveID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboInteractionZatActiveID((RadComboBox)sender, e.Text);
        }

        private void PopulateCboInteractionZatActiveID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new ZatActiveQuery("a");
            query.Where(query.ZatActiveName.Like(searchTextContain));
            query.Select(query.ZatActiveID, query.ZatActiveName);

            query.es.Top = 10;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["ZatActiveID"].ToString();
            }
        }

        protected void cboInteractionZatActiveID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ZatActiveName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ZatActiveID"].ToString();
        }

        #endregion

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ZatActiveInteractionCollection)Session["collZatActiveInteraction"];

                string id = cboInteractionZatActiveID.SelectedValue;
                bool isExist = false;
                foreach (ZatActiveInteraction row in coll)
                {
                    if (row.InteractionZatActiveID.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Interaction Zat Active: {0} has exist", id);
                }
            }
        }

        #region Properties for return entry value
        public String InteractionZatActiveID
        {
            get { return cboInteractionZatActiveID.SelectedValue; }
        }
        public String InteractionZatActiveName
        {
            get { return cboInteractionZatActiveID.Text; }
        }
        public string Interaction
        {
            get { return txtInteraction.Text; }
        }
        #endregion
    }
}