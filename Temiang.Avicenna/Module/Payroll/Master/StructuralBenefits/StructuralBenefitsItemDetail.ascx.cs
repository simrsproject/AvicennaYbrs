using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class StructuralBenefitsItemDetail : BaseUserControl
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

            cboPositionID.Enabled = false;
            txtValidFrom.Enabled = false;
            PopulatecboPositionID(cboPositionID, (int)DataBinder.Eval(DataItem, StructuralBenefitsMetadata.ColumnNames.PositionID));
            txtValidFrom.SelectedDate = (DateTime)DataBinder.Eval(DataItem, StructuralBenefitsMetadata.ColumnNames.ValidFrom);
            txtAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, StructuralBenefitsMetadata.ColumnNames.Amount));
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll =
                    (StructuralBenefitsCollection)Session["collStructuralBenefits"];

                bool isExist = false;
                foreach (StructuralBenefits item in coll)
                {
                    if (item.PositionID == cboPositionID.SelectedValue.ToInt() && item.ValidFrom.Value.Date == txtValidFrom.SelectedDate.Value)
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("{0} with Valid From {1} has exist.", cboPositionID.Text, txtValidFrom.SelectedDate.ToString());
                }
            }
        }

        #region Properties for return entry value

        public Int32 PositionID
        {
            get { return Convert.ToInt32(cboPositionID.SelectedValue); }
        }
        public string PositionName
        {
            get { return cboPositionID.Text; }
        }
        public DateTime ValidFrom
        {
            get { return Convert.ToDateTime(txtValidFrom.SelectedDate); }
        }
        public Decimal Amount
        {
            get { return Convert.ToDecimal(txtAmount.Value); }
        }

        #endregion

        #region Method & Event TextChanged

        #endregion

        #region ComboBox
        protected void cboPositionID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboPositionID((RadComboBox)sender, e.Text);
        }
        private void PopulatecboPositionID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new PositionQuery();

            query.Where(
                query.PositionName.Like(searchTextContain));

            query.Select(query.PositionID, query.PositionCode, query.PositionName);

            query.es.Top = 30;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["PositionID"].ToString();
            }
        }
        private void PopulatecboPositionID(RadComboBox comboBox, int positionId)
        {
            var query = new PositionQuery();

            query.Where(query.PositionID == positionId);

            query.Select(query.PositionID, query.PositionCode, query.PositionName);

            query.es.Top = 30;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["PositionID"].ToString();
            }
        }

        protected void cboPositionID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PositionCode"].ToString() + " " + ((DataRowView)e.Item.DataItem)["PositionName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PositionID"].ToString();
        }
        #endregion
    }
}