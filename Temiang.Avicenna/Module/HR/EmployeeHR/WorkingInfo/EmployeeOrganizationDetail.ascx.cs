using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class EmployeeOrganizationDetail : BaseUserControl
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
            trSubDivision.Visible = AppSession.Parameter.IsUsingFourLevelOrganizationUnit;

            StandardReference.InitializeIncludeSpace(cboSROrganizationLevelType, AppEnum.StandardReference.OrganizationLevelType);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtEmployeeOrganizationID.Text = "1";
                txtValidTo.SelectedDate = Convert.ToDateTime("1/1/2100");

                chkIsActive.Checked = true;

                return;
            }
            ViewState["IsNewRecord"] = false;
            if ((int)DataBinder.Eval(DataItem, EmployeeOrganizationMetadata.ColumnNames.OrganizationID) > 0)
            {
                PopulateCboOrganizationUnitID(cboOrganizationUnitID, (int)DataBinder.Eval(DataItem, EmployeeOrganizationMetadata.ColumnNames.OrganizationID));
                cboOrganizationUnitID.SelectedValue = DataBinder.Eval(DataItem, EmployeeOrganizationMetadata.ColumnNames.OrganizationID).ToString();
            }
            if ((int)DataBinder.Eval(DataItem, EmployeeOrganizationMetadata.ColumnNames.SubOrganizationID) > 0)
            {
                PopulateCboSubOrganizationUnitID(cboSubOrganizationUnitID, (int)DataBinder.Eval(DataItem, EmployeeOrganizationMetadata.ColumnNames.SubOrganizationID));
                cboSubOrganizationUnitID.SelectedValue = DataBinder.Eval(DataItem, EmployeeOrganizationMetadata.ColumnNames.SubOrganizationID).ToString();
            }
            if ((int)DataBinder.Eval(DataItem, EmployeeOrganizationMetadata.ColumnNames.SubDivisonID) > 0)
            {
                PopulateCboSubDivisonID(cboSubDivisonID, (int)DataBinder.Eval(DataItem, EmployeeOrganizationMetadata.ColumnNames.SubDivisonID));
                cboSubDivisonID.SelectedValue = DataBinder.Eval(DataItem, EmployeeOrganizationMetadata.ColumnNames.SubDivisonID).ToString();
            }
            var unitId = (string)DataBinder.Eval(DataItem, EmployeeOrganizationMetadata.ColumnNames.ServiceUnitID);
            if (!string.IsNullOrEmpty(unitId))
            {
                PopulatecboUnit2(cboUnit, (string)DataBinder.Eval(DataItem, EmployeeOrganizationMetadata.ColumnNames.ServiceUnitID));
                cboUnit.SelectedValue = DataBinder.Eval(DataItem, EmployeeOrganizationMetadata.ColumnNames.ServiceUnitID).ToString();
            }

            txtValidFrom.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeOrganizationMetadata.ColumnNames.ValidFrom);
            txtValidTo.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeOrganizationMetadata.ColumnNames.ValidTo);
            chkIsActive.Checked = (bool)DataBinder.Eval(DataItem, EmployeeOrganizationMetadata.ColumnNames.IsActive);
            cboSROrganizationLevelType.SelectedValue = (string)DataBinder.Eval(DataItem, EmployeeOrganizationMetadata.ColumnNames.SROrganizationLevelType);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(cboOrganizationUnitID.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Department required.");
                return;
            }

            if (!trSubDivision.Visible && string.IsNullOrEmpty(cboUnit.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Section / Service Unit required.");
                return;
            }

            if (trSubDivision.Visible && string.IsNullOrEmpty(cboSubDivisonID.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Sub Division required.");
                return;
            }

            //Check duplicate key
            if (cboSROrganizationLevelType.SelectedValue == "001")
            {
                //var coll = (EmployeeOrganizationCollection)Session["collEmployeeOrganization" + Request.UserHostName + PageId];

                //if (ViewState["IsNewRecord"].Equals(true))
                //{
                //    if (coll.Any(c => c.SROrganizationLevelType == cboSROrganizationLevelType.SelectedValue))
                //    {
                //        args.IsValid = false;
                //        ((CustomValidator)source).ErrorMessage = string.Format("Organization Level Type : Main has exist.");
                //        return;
                //    }
                //}
                //else
                //{
                //    if (coll.Any(c => c.SROrganizationLevelType == cboSROrganizationLevelType.SelectedValue && c.EmployeeOrganizationID != txtEmployeeOrganizationID.Text.ToInt()))
                //    {
                //        args.IsValid = false;
                //        ((CustomValidator)source).ErrorMessage = string.Format("Organization Level Type : Main has exist.");
                //        return;
                //    }
                //}

                if (ViewState["IsNewRecord"].Equals(true))
                {
                    var coll = (EmployeeOrganizationCollection)Session["collEmployeeOrganization" + Request.UserHostName + PageId];

                    bool isExist = false;
                    bool a = false;
                    bool b = false;
                    bool c = false;
                    DateTime sDate = txtValidFrom.SelectedDate ?? (new DateTime()).NowAtSqlServer();
                    DateTime eDate = txtValidTo.SelectedDate ?? (new DateTime()).NowAtSqlServer().AddYears(1);

                    foreach (EmployeeOrganization item in coll)
                    {
                        DateTime eDateExists = item.ValidTo ?? eDate;
                        //tgl akhir input = tgl akhir exist, kondisi tgl akhir null
                        if (eDate == eDateExists)
                        {
                            a = true;
                            break;
                        }
                        //tgl awal input <= tgl akhir exist 
                        if (sDate <= eDateExists)
                        {
                            b = true;
                            break;
                        }
                        //tgl akhir input <= tgl akhir exist
                        if (eDate <= eDateExists)
                        {
                            c = true;
                            break;
                        }

 
                    }
                    if (a)
                    {
                        args.IsValid = false;
                        ((CustomValidator)source).ErrorMessage = string.Format("Invalid Valid To. There is already the same data as the input.");
                        return;
                    }
                    if (b)
                    {
                        args.IsValid = false;
                        ((CustomValidator)source).ErrorMessage = string.Format("Invalid Valid From. There is Valid To which is greater than the Valid From that was input.");
                        return;
                    }
                    if (c)
                    {
                        args.IsValid = false;
                        ((CustomValidator)source).ErrorMessage = string.Format("Invalid Valid To. There is Valid To which is greater than the Valid To that was input.");
                        return;
                    }
                }
            }
        }

        #region Properties for return entry value
        public Int32 EmployeeOrganizationID
        {
            get { return Convert.ToInt32(txtEmployeeOrganizationID.Text); }
        }

        public Int32 OrganizationID
        {
            get { return Convert.ToInt32(cboOrganizationUnitID.SelectedValue); }
        }

        public string OrganizationName
        {
            get { return cboOrganizationUnitID.Text; }
        }

        public Int32 SubOrganizationID
        {
            get { return string.IsNullOrEmpty(cboSubOrganizationUnitID.SelectedValue) ? 0 : Convert.ToInt32(cboSubOrganizationUnitID.SelectedValue); }
        }

        public string SubOrganizationName
        {
            get { return cboSubOrganizationUnitID.Text; }
        }

        public Int32 SubDivisonID
        {
            get { return string.IsNullOrEmpty(cboSubDivisonID.SelectedValue) ? 0 : Convert.ToInt32(cboSubDivisonID.SelectedValue); }
        }

        public string SubDivisionName
        {
            get { return cboSubDivisonID.Text; }
        }

        public string ServiceUnitID
        {
            get { return cboUnit.SelectedValue; }
        }

        public string ServiceUnitName
        {
            get { return cboUnit.Text; }
        }

        public DateTime ValidFrom
        {
            get { return Convert.ToDateTime(txtValidFrom.SelectedDate); }
        }

        public DateTime ValidTo
        {
            get { return Convert.ToDateTime(txtValidTo.SelectedDate); }
        }

        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }

        public string SROrganizationLevelTypeID
        {
            get { return cboSROrganizationLevelType.SelectedValue; }
        }

        public string SROrganizationLevelTypeName
        {
            get { return cboSROrganizationLevelType.Text; }
        }

        #endregion

        #region Method & Event TextChanged

        #endregion

        #region ComboBox
        protected void cboOrganizationUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboOrganizationUnitID((RadComboBox)sender, e.Text);
        }
        private void PopulateCboOrganizationUnitID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new OrganizationUnitQuery();

            query.Where(
                query.OrganizationUnitName.Like(searchTextContain));

            query.Select(query.OrganizationUnitID, query.OrganizationUnitCode, query.OrganizationUnitName);
            query.Where(query.SROrganizationLevel == "3");
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }
        private void PopulateCboOrganizationUnitID(RadComboBox comboBox, int textSearch) // open
        {
            var query = new OrganizationUnitQuery();

            query.Where(query.OrganizationUnitID == textSearch);

            query.Select(query.OrganizationUnitID, query.OrganizationUnitCode, query.OrganizationUnitName);
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();

        }
        protected void cboOrganizationUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["OrganizationUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["OrganizationUnitID"].ToString();
        }
        protected void cboOrganizationUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubOrganizationUnitID.Items.Clear();
            cboSubOrganizationUnitID.Text = string.Empty;
            cboSubDivisonID.Items.Clear();
            cboSubDivisonID.Text = string.Empty;
            cboUnit.Items.Clear();
            cboUnit.Text = string.Empty;
        }

        protected void cboSubOrganizationUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboSubOrganizationUnitID((RadComboBox)sender, e.Text);
        }
        private void PopulateCboSubOrganizationUnitID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new OrganizationUnitQuery();

            query.Where(query.OrganizationUnitName.Like(searchTextContain));
            query.Select(query.OrganizationUnitID.As("SubOrganizationID"), query.OrganizationUnitName.As("SubOrganizationName"));
            query.Where(query.SROrganizationLevel == "2", query.ParentOrganizationUnitID == cboOrganizationUnitID.SelectedValue.ToInt());
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }
        private void PopulateCboSubOrganizationUnitID(RadComboBox comboBox, int textSearch) //open
        {
            var query = new OrganizationUnitQuery();

            query.Where(query.OrganizationUnitID == textSearch);

            query.Select(query.OrganizationUnitID.As("SubOrganizationID"),
                         query.OrganizationUnitName.As("SubOrganizationName"));
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }
        protected void cboSubOrganizationUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubOrganizationName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubOrganizationID"].ToString();
        }
        protected void cboSubOrganizationUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboUnit.Items.Clear();
            cboUnit.Text = string.Empty;
            cboSubDivisonID.Items.Clear();
            cboSubDivisonID.Text = string.Empty;
        }

        protected void cboSubDivisonID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboSubDivisonID((RadComboBox)sender, e.Text);
        }
        private void PopulateCboSubDivisonID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new OrganizationUnitQuery();

            query.Where(query.OrganizationUnitName.Like(searchTextContain));
            query.Select(query.OrganizationUnitID.As("SubDivisionID"), query.OrganizationUnitName.As("SubDivisionName"));
            query.Where(query.SROrganizationLevel == "1",
                query.Or(query.ParentOrganizationUnitID == cboSubOrganizationUnitID.SelectedValue.ToInt(), query.ParentOrganizationUnitID == cboOrganizationUnitID.SelectedValue.ToInt())
                );
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }
        private void PopulateCboSubDivisonID(RadComboBox comboBox, int textSearch) //open
        {
            var query = new OrganizationUnitQuery();

            query.Where(query.OrganizationUnitID == textSearch);

            query.Select(query.OrganizationUnitID.As("SubDivisionID"),
                         query.OrganizationUnitName.As("SubDivisionName"));
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }
        protected void cboSubDivisonID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubDivisionName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubDivisionID"].ToString();
        }
        protected void cboSubDivisonID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboUnit.Items.Clear();
            cboUnit.Text = string.Empty;
        }

        protected void cboUnit_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboUnit((RadComboBox)sender, e.Text);
        }
        private void PopulatecboUnit(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            DataTable dtb;

            var query = new OrganizationUnitQuery("a");
            var sub = new OrganizationUnitQuery("b");
            query.LeftJoin(sub).On(sub.OrganizationUnitID == query.ParentOrganizationUnitID);
            query.Select(query.OrganizationUnitID.As("ServiceUnitID"), query.OrganizationUnitName.As("ServiceUnitName"));
            query.Where(query.OrganizationUnitName.Like(searchTextContain), query.SROrganizationLevel == "0",
                query.Or(
                    query.ParentOrganizationUnitID == cboSubDivisonID.SelectedValue.ToInt(),
                    query.ParentOrganizationUnitID == cboSubOrganizationUnitID.SelectedValue.ToInt(),
                    query.ParentOrganizationUnitID == cboOrganizationUnitID.SelectedValue.ToInt()));
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }
        private void PopulatecboUnit2(RadComboBox comboBox, string textSearch)
        {
            DataTable dtb;

            var query = new OrganizationUnitQuery();

            query.Where(query.OrganizationUnitID == textSearch);

            query.Select(query.OrganizationUnitID.As("ServiceUnitID"),
                         query.OrganizationUnitName.As("ServiceUnitName"));
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }
        protected void cboUnit_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }
        #endregion
    }
}
