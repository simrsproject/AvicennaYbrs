using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.RemunerationPosition
{
    public partial class RemunerationPositionItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRWageStructureAndScaleType, AppEnum.StandardReference.WageStructureAndScaleType, false);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                txtWageStructureAndScalePositionItemID.Text = "1";
                return;
            }

            cboSRWageStructureAndScaleType.Enabled = false;

            ViewState["IsNewRecord"] = false;

            txtWageStructureAndScalePositionItemID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.WageStructureAndScalePositionItemID));
            cboSRWageStructureAndScaleType.SelectedValue = Convert.ToString(DataBinder.Eval(DataItem, EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.SRWageStructureAndScaleType));

            var id = Convert.ToString(DataBinder.Eval(DataItem, EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.WageStructureAndScaleID));
            var idQ = new WageStructureAndScaleQuery();
            idQ.Where(idQ.WageStructureAndScaleID == id.ToInt());
            cboWageStructureAndScaleID.DataSource = idQ.LoadDataTable();
            cboWageStructureAndScaleID.DataBind();
            cboWageStructureAndScaleID.SelectedValue = id;

            var itemId = Convert.ToString(DataBinder.Eval(DataItem, EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.WageStructureAndScaleItemID));
            var itemIdQ = new WageStructureAndScaleItemQuery("a");
            var std = new AppStandardReferenceItemQuery("b");
            itemIdQ.InnerJoin(std).On(std.StandardReferenceID == AppEnum.StandardReference.WageStructureAndScaleItem.ToString() && std.ItemID == itemIdQ.SRWageStructureAndScaleItem);
            itemIdQ.Where(itemIdQ.WageStructureAndScaleItemID == itemId.ToInt());
            itemIdQ.Select(itemIdQ.WageStructureAndScaleItemID, itemIdQ.SRWageStructureAndScaleItem, std.ItemName.As("WageStructureAndScaleItemName"));
            cboWageStructureAndScaleItemID.DataSource = itemIdQ.LoadDataTable();
            cboWageStructureAndScaleItemID.DataBind();
            cboWageStructureAndScaleItemID.SelectedValue = itemId;

            txtPoints.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.Points));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(cboSRWageStructureAndScaleType.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Invalid Type.";
                return;
            }
            if (string.IsNullOrEmpty(cboWageStructureAndScaleID.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Wage Structure And Scale.";
                return;
            }
            if (string.IsNullOrEmpty(cboWageStructureAndScaleItemID.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Wage Structure And Scale Item.";
                return;
            }
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (EmployeeWageStructureAndScalePositionItemCollection)Session["collEmployeeWageStructureAndScalePositionItem" + Request.UserHostName];

                string id = cboSRWageStructureAndScaleType.SelectedValue;
                bool isExist = false;
                foreach (EmployeeWageStructureAndScalePositionItem item in coll)
                {
                    if (item.SRWageStructureAndScaleType.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Type: {0} has exist", cboSRWageStructureAndScaleType.Text);
                }
            }
        }

        #region Properties for return entry value
        public Int32 WageStructureAndScalePositionItemID
        {
            get { return Convert.ToInt32(txtWageStructureAndScalePositionItemID.Text); }
        }
        public string SRWageStructureAndScaleType
        {
            get { return cboSRWageStructureAndScaleType.SelectedValue; }
        }
        public string WageStructureAndScaleTypeName
        {
            get { return cboSRWageStructureAndScaleType.Text; }
        }
        public Int32 WageStructureAndScaleID
        {
            get { return Convert.ToInt32(cboWageStructureAndScaleID.SelectedValue); }
        }
        public string WageStructureAndScaleName
        {
            get { return cboWageStructureAndScaleID.Text; }
        }
        public Int32 WageStructureAndScaleItemID
        {
            get { return Convert.ToInt32(cboWageStructureAndScaleItemID.SelectedValue); }
        }
        public string WageStructureAndScaleItemName
        {
            get { return cboWageStructureAndScaleItemID.Text; }
        }
        public Decimal Points
        {
            get { return Convert.ToDecimal(txtPoints.Value); }
        }
        #endregion

        #region ComboBox
        protected void cboSRWageStructureAndScaleType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboWageStructureAndScaleID.Items.Clear();
            cboWageStructureAndScaleID.Text = string.Empty;
            cboWageStructureAndScaleID.SelectedValue = string.Empty;
            cboWageStructureAndScaleItemID.Items.Clear();
            cboWageStructureAndScaleItemID.Text = string.Empty;
            cboWageStructureAndScaleItemID.SelectedValue = string.Empty;
            txtPoints.Value = 0;
        }

        protected void cboWageStructureAndScaleID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new WageStructureAndScaleQuery("a");
            query.Where(query.SRWageStructureAndScaleType == cboSRWageStructureAndScaleType.SelectedValue,
                query.WageStructureAndScaleName.Like(searchTextContain));

            query.Select(query.WageStructureAndScaleID, query.WageStructureAndScaleCode, query.WageStructureAndScaleName);
            DataTable dtb = query.LoadDataTable();
            cboWageStructureAndScaleID.DataSource = dtb;
            cboWageStructureAndScaleID.DataBind();
        }

        protected void cboWageStructureAndScaleID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["WageStructureAndScaleName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["WageStructureAndScaleID"].ToString();
        }

        protected void cboWageStructureAndScaleID_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboWageStructureAndScaleItemID.Items.Clear();
            cboWageStructureAndScaleItemID.Text = string.Empty;
            cboWageStructureAndScaleItemID.SelectedValue = string.Empty;
            txtPoints.Value = 0;
        }

        protected void cboWageStructureAndScaleItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new WageStructureAndScaleItemQuery("a");
            var std = new AppStandardReferenceItemQuery("b");
            query.InnerJoin(std).On(std.StandardReferenceID == AppEnum.StandardReference.WageStructureAndScaleItem.ToString() && std.ItemID == query.SRWageStructureAndScaleItem);
            query.Where(query.WageStructureAndScaleID == cboWageStructureAndScaleID.SelectedValue.ToInt(),
                std.ItemName.Like(searchTextContain));

            query.Select(query.WageStructureAndScaleItemID, query.SRWageStructureAndScaleItem, std.ItemName.As("WageStructureAndScaleItemName"));
            DataTable dtb = query.LoadDataTable();
            cboWageStructureAndScaleItemID.DataSource = dtb;
            cboWageStructureAndScaleItemID.DataBind();
        }

        protected void cboWageStructureAndScaleItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["WageStructureAndScaleItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["WageStructureAndScaleItemID"].ToString();
        }

        protected void cboWageStructureAndScaleItemID_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(cboWageStructureAndScaleItemID.SelectedValue))
            {
                txtPoints.Value = 0;
                return;
            }

            var p = new WageStructureAndScaleItem();
            if (p.LoadByPrimaryKey(cboWageStructureAndScaleItemID.SelectedValue.ToInt()))
                txtPoints.Value = Convert.ToDouble(p.Points);
            else
                txtPoints.Value = 0;
        }

        #endregion





    }
}