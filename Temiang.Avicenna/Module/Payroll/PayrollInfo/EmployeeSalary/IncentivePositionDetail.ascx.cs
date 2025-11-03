using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.PayrollInfo
{
    public partial class IncentivePositionDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private string PageId
        {
            get { return ((HiddenField)Helper.FindControlRecursive(Page, "hdnPageId")).Value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRIncentiveServiceUnitGroup, AppEnum.StandardReference.IncentiveServiceUnitGroup);
            StandardReference.InitializeIncludeSpace(cboSRIncentivePositionGroup, AppEnum.StandardReference.IncentivePositionGroup);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                
                txtIncentivePositionID.Text = "1";
                txtIncentivePositionPoints.Value = 0;
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtIncentivePositionID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeIncentivePositionMetadata.ColumnNames.IncentivePositionID));
            txtValidFrom.SelectedDate = Convert.ToDateTime(DataBinder.Eval(DataItem, EmployeeIncentivePositionMetadata.ColumnNames.ValidFrom));
            cboSRIncentiveServiceUnitGroup.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeeIncentivePositionMetadata.ColumnNames.SRIncentiveServiceUnitGroup);
            cboSRIncentivePositionGroup.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeeIncentivePositionMetadata.ColumnNames.SRIncentivePositionGroup);
            PopulateCboSRIncentivePosition(cboSRIncentivePosition, "", (String)DataBinder.Eval(DataItem, EmployeeIncentivePositionMetadata.ColumnNames.SRIncentivePosition));
            txtIncentivePositionPoints.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeIncentivePositionMetadata.ColumnNames.IncentivePositionPoints));
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            //if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll =
                    (EmployeeIncentivePositionCollection)Session["collEmployeeIncentivePosition" + Request.UserHostName + PageId];

                //TODO: Betulkan cara pengecekannya
                DateTime validFrom = txtValidFrom.SelectedDate ?? DateTime.Now;
                DateTime validTo = txtValidTo.SelectedDate ?? DateTime.Now;
                bool isExist = false;
                foreach (EmployeeIncentivePosition item in coll)
                {
                    if (item.SRIncentiveServiceUnitGroup.Equals(cboSRIncentiveServiceUnitGroup.SelectedValue) && item.ValidFrom.Equals(validFrom) && item.ValidTo.Equals(validTo))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Service unit group {2} with period {0} to {1} has exist", validFrom, validTo, cboSRIncentiveServiceUnitGroup.Text);
                    return;
                }
            }


        }

        #region Properties for return entry value
        public Int32 IncentivePositionID
        {
            get { return Convert.ToInt32(txtIncentivePositionID.Text); }
        }
        public string SRIncentiveServiceUnitGroup
        {
            get { return cboSRIncentiveServiceUnitGroup.SelectedValue; }
        }
        public string IncentiveServiceUnitGroupName
        {
            get { return cboSRIncentiveServiceUnitGroup.Text; }
        }
        public string SRIncentivePositionGroup
        {
            get { return cboSRIncentivePositionGroup.SelectedValue; }
        }
        public string IncentivePositionGroupName
        {
            get { return cboSRIncentivePositionGroup.Text; }
        }
        public string SRIncentivePosition
        {
            get { return cboSRIncentivePosition.SelectedValue; }
        }
        public string IncentivePositionName
        {
            get { return cboSRIncentivePosition.Text; }
        }
        public Decimal IncentivePositionPoints
        {
            get { return Convert.ToDecimal(txtIncentivePositionPoints.Value); }
        }
        public DateTime ValidFrom
        {
            get { return Convert.ToDateTime(txtValidFrom.SelectedDate); }
        }
        public DateTime ValidTo
        {
            get { return Convert.ToDateTime(txtValidTo.SelectedDate); }
        }

        #endregion

        #region Method & Event TextChanged

        #endregion

        #region ComboBox ItemID
        protected void cboSRIncentivePosition_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboSRIncentivePosition((RadComboBox)sender, e.Text, "");
        }
        private void PopulateCboSRIncentivePosition(RadComboBox comboBox, string textSearch, string itemId)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new AppStandardReferenceItemQuery();
            if (itemId == "")
                query.Where(query.StandardReferenceID == AppEnum.StandardReference.IncentivePosition,
                    query.ItemName.Like(searchTextContain),
                    query.IsActive == true, query.ReferenceID == cboSRIncentivePositionGroup.SelectedValue);
            else
                query.Where(query.StandardReferenceID == "IncentivePosition",
                query.ItemID == itemId);

            query.Select(query.ItemID, query.ItemName, query.NumericValue);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["ItemID"].ToString();
            }
        }

        protected void cboSRIncentivePosition_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRIncentivePosition_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                var p = new AppStandardReferenceItem();
                if (p.LoadByPrimaryKey("IncentivePosition", e.Value))
                    txtIncentivePositionPoints.Value = Convert.ToDouble(p.NumericValue);
                else
                    txtIncentivePositionPoints.Value = 0;
            }
            else
            {
                txtIncentivePositionPoints.Value = 0;
            }
        }

        protected void cboSRIncentivePositionGroup_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRIncentivePosition.Items.Clear();
            cboSRIncentivePosition.Text = string.Empty;
            cboSRIncentivePosition.SelectedValue = string.Empty;
            txtIncentivePositionPoints.Value = 0;
        }

        #endregion
    }
}