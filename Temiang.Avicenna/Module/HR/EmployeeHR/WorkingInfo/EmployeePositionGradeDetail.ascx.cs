using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class EmployeePositionGradeDetail : BaseUserControl
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

        private RadNumericTextBox TxtPersonId
        {
            get
            {
                return (RadNumericTextBox)Helper.FindControlRecursive(Page, "txtPersonID");
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSREducationLevel, AppEnum.StandardReference.EducationLevel);
            StandardReference.InitializeIncludeSpace(cboSRDecreeType, AppEnum.StandardReference.DecreeType);
            StandardReference.InitializeIncludeSpace(cboSRDecreeTypeNext, AppEnum.StandardReference.DecreeType);
            StandardReference.InitializeIncludeSpace(cboSRDp3, AppEnum.StandardReference.Dp3);

            trSalaryScale.Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP";
            trNextSalaryScale.Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP";
            trDp3.Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSSA";

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtEmployeePositionGradeID.Text = "1";

                var emps = new EmployeePositionGradeCollection();
                var emp = new EmployeePositionGradeQuery();
                emp.Where(emp.PersonID == TxtPersonId.Value.ToInt());
                emp.OrderBy(emp.ValidFrom.Descending);
                emp.es.Top = 1;
                emps.Load(emp);
                foreach (var x in emps)
                {
                    txtValidFrom.SelectedDate = x.NextProposalDate ?? DateTime.Now;
                    PopulatecboPositionGradeID(cboPositionGradeID, x.NextPositionGradeID.ToInt());
                    txtGradeYear.Value = Convert.ToDouble(x.NextGradeYear);
                    cboSRDecreeType.SelectedValue = x.SRDecreeTypeNext;
                    txtPositionName.Text = x.NextPositionName;
                    txtNotes.Text = x.Notes;
                }

                return;
            }
            ViewState["IsNewRecord"] = false;

            txtEmployeePositionGradeID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeePositionGradeMetadata.ColumnNames.EmployeePositionGradeID));
            cboSREducationLevel.SelectedValue = (string)DataBinder.Eval(DataItem, EmployeePositionGradeMetadata.ColumnNames.SREducationLevel);
            txtValidFrom.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeePositionGradeMetadata.ColumnNames.ValidFrom);
            PopulatecboPositionGradeID(cboPositionGradeID, (int)DataBinder.Eval(DataItem, EmployeePositionGradeMetadata.ColumnNames.PositionGradeID));
            txtGradeYear.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeePositionGradeMetadata.ColumnNames.GradeYear));
            cboSRDecreeType.SelectedValue = (string)DataBinder.Eval(DataItem, EmployeePositionGradeMetadata.ColumnNames.SRDecreeType);
            txtDecreeNo.Text = (string)DataBinder.Eval(DataItem, EmployeePositionGradeMetadata.ColumnNames.DecreeNo);

            int salaryScaleId = -1;
            int.TryParse(Convert.ToString(DataBinder.Eval(DataItem, EmployeePositionGradeMetadata.ColumnNames.SalaryScaleID)), out salaryScaleId);
            PopulateCboSalaryScaleID(cboSalaryScaleID, salaryScaleId);

            object nextProposalDate = DataBinder.Eval(DataItem, EmployeePositionGradeMetadata.ColumnNames.NextProposalDate);
            if (nextProposalDate != null)
                txtNextProposalDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeePositionGradeMetadata.ColumnNames.NextProposalDate);
            else
                txtNextProposalDate.Clear();

            PopulatecboPositionGradeID(cboNextPositionGradeID, (int)DataBinder.Eval(DataItem, EmployeePositionGradeMetadata.ColumnNames.NextPositionGradeID));
            txtNextGradeYear.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeePositionGradeMetadata.ColumnNames.NextGradeYear));
            cboSRDecreeTypeNext.SelectedValue = (string)DataBinder.Eval(DataItem, EmployeePositionGradeMetadata.ColumnNames.SRDecreeTypeNext);

            int nextSalaryScaleId = -1;
            int.TryParse(Convert.ToString(DataBinder.Eval(DataItem, EmployeePositionGradeMetadata.ColumnNames.NextSalaryScaleID)), out nextSalaryScaleId);
            PopulateCboSalaryScaleID(cboNextSalaryScaleID, nextSalaryScaleId);

            cboSRDp3.SelectedValue = (string)DataBinder.Eval(DataItem, EmployeePositionGradeMetadata.ColumnNames.SRDp3);
            txtNotes.Text = (string)DataBinder.Eval(DataItem, EmployeePositionGradeMetadata.ColumnNames.Notes);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
        }

        #region Properties for return entry value
        public Int32 EmployeePositionGradeID
        {
            get { return Convert.ToInt32(txtEmployeePositionGradeID.Text); }
        }
        public DateTime ValidFrom
        {
            get { return Convert.ToDateTime(txtValidFrom.SelectedDate); }
        }
        public string SREducationLevel
        {
            get { return cboSREducationLevel.SelectedValue; }
        }
        public string EducationLevelName
        {
            get { return cboSREducationLevel.Text; }
        }
        public Int32 PositionGradeID
        {
            get { return Convert.ToInt32(cboPositionGradeID.SelectedValue); }
        }
        public string PositionGradeName
        {
            get { return cboPositionGradeID.Text; }
        }
        public Int32 GradeYear
        {
            get { return Convert.ToInt32(txtGradeYear.Value); }
        }
        public string PositionName
        {
            get { return txtPositionName.Text; }
        }
        public string SRDecreeType
        {
            get { return cboSRDecreeType.SelectedValue; }
        }
        public string DecreeTypeName
        {
            get { return cboSRDecreeType.Text; }
        }
        public string DecreeNo
        {
            get { return txtDecreeNo.Text; }
        }
        public DateTime? NextProposalDate
        {
            get { return Convert.ToDateTime(txtNextProposalDate.SelectedDate); }
        }
        public Int32 NextPositionGradeID
        {
            get { return Convert.ToInt32(cboNextPositionGradeID.SelectedValue); }
        }
        public string NextPositionGradeName
        {
            get { return cboNextPositionGradeID.Text; }
        }
        public Int32? NextGradeYear
        {
            get { return Convert.ToInt32(txtNextGradeYear.Value); }
        }
        public string SRDecreeTypeNext
        {
            get { return cboSRDecreeTypeNext.SelectedValue; }
        }
        public string DecreeTypeNameNext
        {
            get { return cboSRDecreeTypeNext.Text; }
        }
        public string NextPositionName
        {
            get { return txtNextPositionName.Text; }
        }
        public string SRDp3
        {
            get { return cboSRDp3.SelectedValue; }
        }
        public string Dp3Name
        {
            get { return cboSRDp3.Text; }
        }
        public string Notes
        {
            get { return txtNotes.Text; }
        }

        public Int32 SalaryScaleID
        {
            get 
            {
                int salaryScaleId = -1;
                int.TryParse(cboSalaryScaleID.SelectedValue, out salaryScaleId);

                return salaryScaleId; 
            }
        }
        public string SalaryScaleName
        {
            get { return cboSalaryScaleID.Text; }
        }
        public Int32 NextSalaryScaleID
        {
            get 
            {
                int salaryScaleId = -1;
                int.TryParse(cboNextSalaryScaleID.SelectedValue, out salaryScaleId);

                return salaryScaleId;
            }
        }
        public string NextSalaryScaleName
        {
            get { return cboNextSalaryScaleID.Text; }
        }

        #endregion

        #region Method & Event TextChanged
        #endregion

        #region ComboBox
        protected void cboPositionGradeID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboPositionGradeID((RadComboBox)sender, e.Text);
        }
        private void PopulatecboPositionGradeID(RadComboBox comboBox, string textSearch)
        {
            var srEmploymentType = "-1";
            var empQ = new VwEmployeeTableQuery();
            empQ.Where(empQ.PersonID == TxtPersonId.Text.ToInt());
            var emp = new VwEmployeeTable();
            emp.Load(empQ);
            if (emp != null)
                srEmploymentType = emp.SREmploymentType;

            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new PositionGradeQuery("a");
            var et = new AppStandardReferenceItemQuery("b");

            query.LeftJoin(et).On(et.StandardReferenceID == "EmploymentType" && et.ItemID == query.SREmploymentType);

            query.Where(query.PositionGradeName.Like(searchTextContain));
            if (srEmploymentType != "-1")
                query.Where(query.Or(query.SREmploymentType == srEmploymentType, query.SREmploymentType.IsNull(), query.SREmploymentType == string.Empty));

            query.Select(
                query.PositionGradeID, 
                query.PositionGradeCode, 
                query.PositionGradeName, 
                query.RankName, 
                et.ItemName.As("EmploymentTypeName")
                );

            query.es.Top = 30;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["PositionGradeID"].ToString();
            }
        }
        private void PopulatecboPositionGradeID(RadComboBox comboBox, int positionGradeId)
        {
            var query = new PositionGradeQuery("a");
            var et = new AppStandardReferenceItemQuery("b");

            query.LeftJoin(et).On(et.StandardReferenceID == "EmploymentType" && et.ItemID == query.SREmploymentType);

            query.Where(query.PositionGradeID == positionGradeId);

            query.Select(
                query.PositionGradeID, 
                query.PositionGradeCode, 
                query.PositionGradeName, 
                query.RankName,
                et.ItemName.As("EmploymentTypeName")
                );

            query.es.Top = 30;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["PositionGradeID"].ToString();
            }
        }
        protected void cboPositionGradeID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PositionGradeCode"].ToString().Trim() + " (" + ((DataRowView)e.Item.DataItem)["RankName"].ToString() + ")";
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PositionGradeID"].ToString();
        }

        protected void cboPositionGradeID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSalaryScaleID.Items.Clear();
            cboSalaryScaleID.Text = string.Empty;
            cboSalaryScaleID.SelectedValue = string.Empty;
        }
        protected void cboNextPositionGradeID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboNextSalaryScaleID.Items.Clear();
            cboNextSalaryScaleID.Text = string.Empty;
            cboNextSalaryScaleID.SelectedValue = string.Empty;
        }

        protected void cboSalaryScaleID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboSalaryScaleID((RadComboBox)sender, e.Text, false);
        }

        protected void cboNextSalaryScaleID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboSalaryScaleID((RadComboBox)sender, e.Text, true);
        }

        private void PopulateCboSalaryScaleID(RadComboBox comboBox, string textSearch, bool isNext)
        {
            var srEmploymentType = "-1";
            var srProfessionGroup = "";
            var srEducationGroup = "";
            var empQ = new VwEmployeeTableQuery();
            empQ.Where(empQ.PersonID == TxtPersonId.Text.ToInt());
            var emp = new VwEmployeeTable();
            emp.Load(empQ);
            if (emp != null)
            {
                srEmploymentType = emp.SREmploymentType;
                srProfessionGroup = emp.SRProfessionGroup;
                srEducationGroup = emp.SREducationGroup;
            }

            var query = new SalaryScaleQuery("a");
            var empType = new AppStandardReferenceItemQuery("b");
            var pGroup = new AppStandardReferenceItemQuery("c");
            var eduGroup = new AppStandardReferenceItemQuery("d");

            query.InnerJoin(empType).On(empType.StandardReferenceID == AppEnum.StandardReference.EmploymentType && empType.ItemID == query.SREmploymentType);
            query.InnerJoin(pGroup).On(pGroup.StandardReferenceID == AppEnum.StandardReference.ProfessionGroup && pGroup.ItemID == query.SRProfessionGroup);
            query.InnerJoin(eduGroup).On(eduGroup.StandardReferenceID == AppEnum.StandardReference.EducationGroup && eduGroup.ItemID == query.SREducationGroup);

            string searchTextContain = string.Format("%{0}%", textSearch);
            query.Where(query.Or(query.SalaryScaleCode.Like(searchTextContain), 
                query.SalaryScaleName.Like(searchTextContain)));
            if (!isNext)
                query.Where(query.PositionGradeID == cboPositionGradeID.SelectedValue.ToInt());
            else
                query.Where(query.PositionGradeID == cboNextPositionGradeID.SelectedValue.ToInt());

            if (srEmploymentType != "-1")
                query.Where(query.SREmploymentType == srEmploymentType);
            if (srProfessionGroup != "")
                query.Where(query.SRProfessionGroup == srProfessionGroup);
            if (srEducationGroup != "")
                query.Where(query.SREducationGroup == srEducationGroup);

            query.Select(
                query.SalaryScaleID,
                query.SalaryScaleCode,
                query.SalaryScaleName,
                empType.ItemName.As("EmploymentTypeName"),
                pGroup.ItemName.As("ProfessionGroupName"),
                eduGroup.ItemName.As("EducationGroup"), 
                query.Notes
                );

            query.es.Top = 30;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["SalaryScaleID"].ToString();
            }
        }
        private void PopulateCboSalaryScaleID(RadComboBox comboBox, int salaryScaleId)
        {
            var query = new SalaryScaleQuery("a");
            var empType = new AppStandardReferenceItemQuery("b");
            var pGroup = new AppStandardReferenceItemQuery("c");
            var eduGroup = new AppStandardReferenceItemQuery("d");

            query.InnerJoin(empType).On(empType.StandardReferenceID == AppEnum.StandardReference.EmploymentType && empType.ItemID == query.SREmploymentType);
            query.InnerJoin(pGroup).On(pGroup.StandardReferenceID == AppEnum.StandardReference.ProfessionGroup && pGroup.ItemID == query.SRProfessionGroup);
            query.InnerJoin(eduGroup).On(eduGroup.StandardReferenceID == AppEnum.StandardReference.EducationGroup && eduGroup.ItemID == query.SREducationGroup);

            query.Where(query.SalaryScaleID == salaryScaleId);
            
            query.Select(
                query.SalaryScaleID,
                query.SalaryScaleCode,
                query.SalaryScaleName,
                empType.ItemName.As("EmploymentTypeName"),
                pGroup.ItemName.As("ProfessionGroupName"),
                eduGroup.ItemName.As("EducationGroup"),
                query.Notes
                );

            query.es.Top = 30;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["SalaryScaleID"].ToString();
            }
        }

        protected void cboSalaryScaleID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SalaryScaleName"].ToString().Trim();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SalaryScaleID"].ToString();
        }
        #endregion
    }
}