using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.PositionInformation
{
    public partial class PositionEmploymentCompanyDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRRequirement, AppEnum.StandardReference.HRLevelRequirement);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtPositionEmploymentCompanyID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtPositionEmploymentCompanyID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PositionEmploymentCompanyMetadata.ColumnNames.PositionEmploymentCompanyID));
            cboSRRequirement.SelectedValue = (String)DataBinder.Eval(DataItem, PositionEmploymentCompanyMetadata.ColumnNames.SRRequirement);
            txtYearOfService.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PositionEmploymentCompanyMetadata.ColumnNames.YearOfService));
            PopulatecboPositionGradeID(cboPositionGradeID, (String)DataBinder.Eval(DataItem, "PositionGradeName"));
            txtNotes.Text = (String)DataBinder.Eval(DataItem, PositionEmploymentCompanyMetadata.ColumnNames.Notes);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                PositionEmploymentCompanyCollection coll =
                    (PositionEmploymentCompanyCollection)Session["collPositionEmploymentCompany"];

                //TODO: Betulkan cara pengecekannya
                string id = txtPositionEmploymentCompanyID.Text;
                bool isExist = false;
                foreach (PositionEmploymentCompany item in coll)
                {
                    if (item.PositionID.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", id);
                }
            }
        }

        #region Properties for return entry value
        public Int32 PositionEmploymentCompanyID
        {
            get { return Convert.ToInt32(txtPositionEmploymentCompanyID.Text); }
        }
        public String SRRequirement
        {
            get { return cboSRRequirement.SelectedValue; }
        }

        public String HRRequirementName
        {
            get { return cboSRRequirement.Text; }
        }

        public Int32 YearOfService
        {
            get { return Convert.ToInt32(txtYearOfService.Text); }
        }
        public Int32 PositionGradeID
        {
            get { return Convert.ToInt32(cboPositionGradeID.SelectedValue); }
        }

        public String PositionGradeName
        {
            get { return cboPositionGradeID.Text; }
        }

        public String Notes
        {
            get { return txtNotes.Text; }
        }
        #endregion

        #region Method & Event TextChanged
        
        #endregion

        #region ComboBox Function
        private void PopulatecboPositionGradeID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            PositionGradeQuery query = new PositionGradeQuery("a");
            PositionEmploymentCompanyQuery employment = new PositionEmploymentCompanyQuery("b");
            query.LeftJoin(employment).On(query.PositionGradeID == employment.PositionGradeID);

            query.Where(query.PositionGradeName.Like(searchTextContain));

            query.Select(query.PositionGradeID, query.PositionGradeCode, query.PositionGradeName);

            query.es.Top = 10;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["PositionGradeID"].ToString();
            }
        }

        protected void cboPositionGradeID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboPositionGradeID((RadComboBox)sender, e.Text);
        }

        protected void cboPositionGradeID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PositionGradeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PositionGradeID"].ToString();
        }
        
        #endregion ComboBox Function
    }
}
