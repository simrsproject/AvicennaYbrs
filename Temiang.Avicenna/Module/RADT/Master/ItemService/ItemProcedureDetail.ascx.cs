using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ItemProcedureDetail : BaseUserControl
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
            cboSRProcedure.Enabled = false;
            PopulateCboSRProcedure(cboSRProcedure, (String)DataBinder.Eval(DataItem, ItemServiceProcedureMetadata.ColumnNames.SRProcedure), false);
        }

        #region ComboBox ServiceUnitID
        protected void cboSRProcedure_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboSRProcedure((RadComboBox)sender, e.Text, true);
        }

        private void PopulateCboSRProcedure(RadComboBox comboBox, string textSearch, bool isNew)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new AppStandardReferenceItemQuery("a");
            query.Where(query.StandardReferenceID == AppEnum.StandardReference.Procedure.ToString());
            if (isNew)
                query.Where(
                    query.ItemName.Like(searchTextContain),
                    query.IsActive == true);
            else
                query.Where(
                    query.ItemID == textSearch);

            query.Select(query.ItemID, query.ItemName);

            query.es.Top = 10;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["ItemID"].ToString();
            }
        }

        protected void cboSRProcedure_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
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
                    if (row.SRProcedure.Equals(cboSRProcedure.SelectedValue))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Procedure: {0} has exist", cboSRProcedure.Text);
                }
            }
        }

        #region Properties for return entry value
        public String SRProcedure
        {
            get { return cboSRProcedure.SelectedValue; }
        }
        public String ProcedureName
        {
            get { return cboSRProcedure.Text; }
        }
        
        #endregion
    }
}