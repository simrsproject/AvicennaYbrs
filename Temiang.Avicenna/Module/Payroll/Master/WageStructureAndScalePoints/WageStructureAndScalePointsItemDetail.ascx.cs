using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class WageStructureAndScalePointsItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }


        private RadComboBox CboSRWageStructureAndScaleType
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboSRWageStructureAndScaleType"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                txtWageStructureAndScaleItemID.Text = "1";
                return;
            }

            ViewState["IsNewRecord"] = false;
            cboSRWageStructureAndScaleItem.Enabled = false;

            txtWageStructureAndScaleItemID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, WageStructureAndScaleItemMetadata.ColumnNames.WageStructureAndScaleItemID));

            var itemId = Convert.ToString(DataBinder.Eval(DataItem, WageStructureAndScaleItemMetadata.ColumnNames.SRWageStructureAndScaleItem));
            var q = new AppStandardReferenceItemQuery();
            q.Where(q.StandardReferenceID == AppEnum.StandardReference.WageStructureAndScaleItem.ToString(), q.ItemID == itemId);
            cboSRWageStructureAndScaleItem.DataSource = q.LoadDataTable();
            cboSRWageStructureAndScaleItem.DataBind();
            
            cboSRWageStructureAndScaleItem.SelectedValue = itemId;
            txtPoints.Value = Convert.ToDouble(DataBinder.Eval(DataItem, WageStructureAndScaleItemMetadata.ColumnNames.Points));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(cboSRWageStructureAndScaleItem.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Item Name required.";
                return;
            }
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (WageStructureAndScaleItemCollection)Session["collWageStructureAndScaleItem"];

                string id = cboSRWageStructureAndScaleItem.SelectedValue;
                bool isExist = false;
                foreach (WageStructureAndScaleItem item in coll)
                {
                    if (item.SRWageStructureAndScaleItem.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Code: {0} has exist", id);
                }
            }
        }

        #region Properties for return entry value
        public Int32 WageStructureAndScaleItemID
        {
            get { return Convert.ToInt32(txtWageStructureAndScaleItemID.Text); }
        }
        public string SRWageStructureAndScaleItem
        {
            get { return cboSRWageStructureAndScaleItem.SelectedValue; }
        }
        public string WageStructureAndScaleItemName
        {
            get { return cboSRWageStructureAndScaleItem.Text; }
        }
        public Decimal Points
        {
            get { return Convert.ToDecimal(txtPoints.Value); }
        }
        #endregion

        protected void cboSRWageStructureAndScaleItem_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery("a");
            query.Where(query.StandardReferenceID == AppEnum.StandardReference.WageStructureAndScaleItem.ToString(),
                query.ItemName.Like(searchTextContain), 
                query.ReferenceID == CboSRWageStructureAndScaleType.SelectedValue);

            query.Select(query.ItemID, query.ItemName);
            DataTable dtb = query.LoadDataTable();
            cboSRWageStructureAndScaleItem.DataSource = dtb;
            cboSRWageStructureAndScaleItem.DataBind();
        }
        
        protected void cboSRWageStructureAndScaleItem_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }
    }
}