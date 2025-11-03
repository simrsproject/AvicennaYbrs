using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class EmployeeRL4Detail : BaseUserControl
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
            StandardReference.InitializeIncludeSpace(cboSRRL4Status, AppEnum.StandardReference.RL4Status);
            StandardReference.InitializeIncludeSpace(cboSRRL4Type, AppEnum.StandardReference.RL4Type);
            StandardReference.InitializeIncludeSpace(cboSRMedisType, AppEnum.StandardReference.RL4MedisType);
            StandardReference.InitializeIncludeSpace(cboSREducationLevel, AppEnum.StandardReference.EducationLevel);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtEmployeeRL4ID.Text = "1";
                chkIsActive.Checked = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtEmployeeRL4ID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeRL4Metadata.ColumnNames.EmployeeRL4ID));
            //PopulatecboCompanyEducationProfileID(cboCompanyEducationProfileID, (String)DataBinder.Eval(DataItem, "CompanyEducationProfileName"));
            //PopulatecboCompanyFieldOfWorkProfileID(cboCompanyFieldOfWorkProfileID, (String)DataBinder.Eval(DataItem, "CompanyFieldOfWorkProfileName"));            
            cboSRRL4Status.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeeRL4Metadata.ColumnNames.SRRL4Status);
            cboSRRL4Type.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeeRL4Metadata.ColumnNames.SRRL4Type);
            //cboSRMedisType.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeeRL4Metadata.ColumnNames.SRMedisType);
            //cboSREducationLevel.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeeRL4Metadata.ColumnNames.SREducationLevel);
            //PopulatecboRL4EducationID(cboRL4EducationID, (String)DataBinder.Eval(DataItem, "PositionName"));
            chkIsActive.Checked = (bool)DataBinder.Eval(DataItem, EmployeeRL4Metadata.ColumnNames.IsActive);
            txtValidFrom.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeRL4Metadata.ColumnNames.ValidFrom);
            txtValidTo.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeRL4Metadata.ColumnNames.ValidTo);

            PopulateCboSRRL4ProfessionType(cboSRRL4ProfessionType, (String)DataBinder.Eval(DataItem, "SRRL4ProfessionType"), false);
            PopulateCboSRRL4EducationLevel(cboSRRL4EducationLevel, (String)DataBinder.Eval(DataItem, "SRRL4EducationLevel"), false);
            PopulateCboSRRL4EducationMajor(cboSRRL4EducationMajor, (String)DataBinder.Eval(DataItem, "SRRL4EducationMajor"), false);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                EmployeeRL4Collection coll =
                    (EmployeeRL4Collection)Session["collEmployeeRL4" + Request.UserHostName + PageId];

                //TODO: Betulkan cara pengecekannya
                string id = txtEmployeeRL4ID.Text;
                bool isExist = false;
                foreach (EmployeeRL4 item in coll)
                {
                    if (item.PersonID.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", id);
                    return;
                }
            }
            if (string.IsNullOrEmpty(cboSRRL4Status.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Invalid Labor Status.";
                return;
            }
            if (string.IsNullOrEmpty(cboSRRL4Type.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Invalid RL4 Type.";
                return;
            }
            if (string.IsNullOrEmpty(cboSRRL4ProfessionType.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Invalid Profession Type";
                return;
            }
            if (string.IsNullOrEmpty(cboSRRL4EducationLevel.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Invalid Education Level";
                return;
            }
            if (string.IsNullOrEmpty(cboSRRL4EducationMajor.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Invalid Major.";
                return;
            }
        }

        #region Properties for return entry value
        public Int32 EmployeeRL4ID
        {
            get { return Convert.ToInt32(txtEmployeeRL4ID.Text); }
        }
        public Int32 CompanyEducationProfileID
        {
            get { return string.IsNullOrEmpty(cboCompanyEducationProfileID.SelectedValue) ? -1 : Convert.ToInt32(cboCompanyEducationProfileID.SelectedValue); }
        }
        public string CompanyEducationProfileName
        {
            get { return cboCompanyEducationProfileID.Text; }
        }
        public Int32 CompanyFieldOfWorkProfileID
        {
            get { return string.IsNullOrEmpty(cboCompanyFieldOfWorkProfileID.SelectedValue) ? -1 : Convert.ToInt32(cboCompanyFieldOfWorkProfileID.SelectedValue); }
        }
        public string CompanyFieldOfWorkProfileName
        {
            get { return cboCompanyFieldOfWorkProfileID.Text; }
        }
        public String SRRL4Status
        {
            get { return cboSRRL4Status.SelectedValue; }
        }
        public String RL4StatusName
        {
            get { return cboSRRL4Status.Text; }
        }
        public String SRRL4Type
        {
            get { return cboSRRL4Type.SelectedValue; }
        }
        public String RL4TypeName
        {
            get { return cboSRRL4Type.Text; }
        }
        public String SRMedisType
        {
            get { return cboSRMedisType.SelectedValue; }
        }
        public String MedisTypeName
        {
            get { return cboSRMedisType.Text; }
        }
        public String SREducationLevel
        {
            get { return cboSREducationLevel.SelectedValue; }
        }
        public String EducationLevelName
        {
            get { return cboSREducationLevel.Text; }
        }
        public Int32 RL4EducationID
        {
            get { return string.IsNullOrEmpty(cboCompanyEducationProfileID.SelectedValue) ? -1 : Convert.ToInt32(cboCompanyEducationProfileID.SelectedValue); }
        }
        public string RL4EducationName
        {
            get { return cboCompanyEducationProfileID.Text; }
        }
        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }
        public DateTime ValidFrom
        {
            get { return Convert.ToDateTime(txtValidFrom.SelectedDate); }
        }
        public DateTime ValidTo
        {
            get { return Convert.ToDateTime(txtValidTo.SelectedDate); }
        }

        public string SRRL4ProfessionType
        {
            get { return cboSRRL4ProfessionType.SelectedValue; }
        }
        public string RL4ProfessionTypeName
        {
            get { return cboSRRL4ProfessionType.Text; }
        }
        public string SRRL4EducationLevel
        {
            get { return cboSRRL4EducationLevel.SelectedValue; }
        }
        public string RL4EducationLevelName
        {
            get { return cboSRRL4EducationLevel.Text; }
        }
        public string SRRL4EducationMajor
        {
            get { return cboSRRL4EducationMajor.SelectedValue; }
        }
        public string RL4EducationMajorName
        {
            get { return cboSRRL4EducationMajor.Text; }
        }


        #endregion

        #region Method & Event TextChanged
        protected void cboSRRL4Type_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRRL4ProfessionType.Items.Clear();
            cboSRRL4ProfessionType.Text = string.Empty;
            cboSRRL4EducationLevel.Items.Clear();
            cboSRRL4EducationLevel.Text = string.Empty;
            cboSRRL4EducationMajor.Items.Clear();
            cboSRRL4EducationMajor.Text = string.Empty;
        }
        protected void cboSRRL4ProfessionType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRRL4EducationLevel.Items.Clear();
            cboSRRL4EducationLevel.Text = string.Empty;
            cboSRRL4EducationMajor.Items.Clear();
            cboSRRL4EducationMajor.Text = string.Empty;
        }
        protected void cboSRRL4EducationLevel_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRRL4EducationMajor.Items.Clear();
            cboSRRL4EducationMajor.Text = string.Empty;
        }
        #endregion

        #region ComboBox ItemID

        //Education
        protected void cboCompanyEducationProfileID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboCompanyEducationProfileID((RadComboBox)sender, e.Text);
        }
        private void PopulatecboCompanyEducationProfileID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            CompanyEducationProfileQuery query = new CompanyEducationProfileQuery("a");
            EmployeeRL4Query employee = new EmployeeRL4Query("b");
            query.LeftJoin(employee).On(query.CompanyEducationProfileID == employee.CompanyEducationProfileID);

            query.Where(
                query.CompanyEducationProfileName.Like(searchTextContain));

            query.Select(query.CompanyEducationProfileID, query.CompanyEducationProfileCode, query.CompanyEducationProfileName);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["CompanyEducationProfileID"].ToString();
            }
        }
        protected void cboCompanyEducationProfileID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["CompanyEducationProfileName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["CompanyEducationProfileID"].ToString();
        }

        // Field Of Work
        protected void cboCompanyFieldOfWorkProfileID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboCompanyFieldOfWorkProfileID((RadComboBox)sender, e.Text);
        }
        private void PopulatecboCompanyFieldOfWorkProfileID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            CompanyFieldOfWorkProfileQuery query = new CompanyFieldOfWorkProfileQuery("a");
            EmployeeRL4Query employee = new EmployeeRL4Query("b");
            query.LeftJoin(employee).On(query.CompanyFieldOfWorkProfileID == employee.CompanyFieldOfWorkProfileID);

            query.Where(
                query.CompanyFieldOfWorkProfileName.Like(searchTextContain));

            query.Select(query.CompanyFieldOfWorkProfileID, query.CompanyFieldOfWorkProfileCode, query.CompanyFieldOfWorkProfileName);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["CompanyFieldOfWorkProfileID"].ToString();
            }
        }
        protected void cboCompanyFieldOfWorkProfileID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["CompanyFieldOfWorkProfileName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["CompanyFieldOfWorkProfileID"].ToString();
        }

        // RL4 Education
        protected void cboRL4EducationID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboRL4EducationID((RadComboBox)sender, e.Text);
        }
        private void PopulatecboRL4EducationID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            RL4EducationQuery query = new RL4EducationQuery("a");
            EmployeeRL4Query employee = new EmployeeRL4Query("b");
            query.LeftJoin(employee).On(query.RL4EducationID == employee.RL4EducationID);

            query.Where(
                query.RL4EducationName.Like(searchTextContain));

            query.Select(query.RL4EducationID, query.RL4EducationCode, query.RL4EducationName);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["RL4EducationID"].ToString();
            }
        }
        protected void cboRL4EducationID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["RL4EducationName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["RL4EducationID"].ToString();
        }

        //-------
        // RL4 Education
        protected void cboSRRL4ProfessionType_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboSRRL4ProfessionType((RadComboBox)sender, e.Text, true);
        }
        private void PopulateCboSRRL4ProfessionType(RadComboBox comboBox, string textSearch, bool isNew)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new AppStandardReferenceItemQuery("a");

            if (isNew)
            {
                query.Where(
                    query.StandardReferenceID == AppEnum.StandardReference.RL4ProfessionType.ToString(),
                    query.ItemName.Like(searchTextContain),
                    query.ReferenceID == cboSRRL4Type.SelectedValue);
            }
            else
            {
                query.Where(
                    query.StandardReferenceID == AppEnum.StandardReference.RL4ProfessionType.ToString(),
                    query.ItemID == textSearch,
                    query.ReferenceID == cboSRRL4Type.SelectedValue);
            }

            query.Select(query.ItemID, query.ItemName);
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }
        protected void cboSRRL4ProfessionType_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRRL4EducationLevel_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboSRRL4EducationLevel((RadComboBox)sender, e.Text, true);
        }
        private void PopulateCboSRRL4EducationLevel(RadComboBox comboBox, string textSearch, bool isNew)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new AppStandardReferenceItemQuery("a");
            if (isNew)
            {
                query.Where(
                    query.StandardReferenceID == AppEnum.StandardReference.RL4EducationLevel.ToString(),
                    query.ItemName.Like(searchTextContain),
                    query.ReferenceID == cboSRRL4ProfessionType.SelectedValue,
                    query.CustomField == cboSRRL4Type.SelectedValue);
            }
            else
            {
                query.Where(
                    query.StandardReferenceID == AppEnum.StandardReference.RL4EducationLevel.ToString(),
                    query.ItemID == textSearch,
                    query.ReferenceID == cboSRRL4ProfessionType.SelectedValue,
                    query.CustomField == cboSRRL4Type.SelectedValue);
            }

            query.Select(query.ItemID, query.ItemName);
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }
        protected void cboSRRL4EducationLevel_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRRL4EducationMajor_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboSRRL4EducationMajor((RadComboBox)sender, e.Text, true);
        }
        private void PopulateCboSRRL4EducationMajor(RadComboBox comboBox, string textSearch, bool isNew)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new AppStandardReferenceItemQuery("a");
            if (isNew)
            {
                query.Where(
                    query.StandardReferenceID == AppEnum.StandardReference.RL4EducationMajor.ToString(),
                    query.ItemName.Like(searchTextContain),
                    query.ReferenceID == cboSRRL4EducationLevel.SelectedValue,
                    query.CustomField == cboSRRL4Type.SelectedValue,
                    query.CustomField2 == cboSRRL4ProfessionType.SelectedValue);
            }
            else
            {
                query.Where(
                    query.StandardReferenceID == AppEnum.StandardReference.RL4EducationMajor.ToString(),
                    query.ItemID == textSearch,
                    query.ReferenceID == cboSRRL4EducationLevel.SelectedValue,
                    query.CustomField == cboSRRL4Type.SelectedValue,
                    query.CustomField2 == cboSRRL4ProfessionType.SelectedValue);
            }

            query.Select(query.ItemID, query.ItemName);
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }
        protected void cboSRRL4EducationMajor_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }
        #endregion
    }
}
