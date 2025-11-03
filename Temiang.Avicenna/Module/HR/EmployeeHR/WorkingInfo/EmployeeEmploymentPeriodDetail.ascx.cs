using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Linq;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class EmployeeEmploymentPeriodDetail : BaseUserControl
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

        private RadTextBox TxtEmployeeNumber
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtEmployeeNumber"); }
        }

        private RadDatePicker TxtJoinDate
        {
            get
            { return (RadDatePicker)Helper.FindControlRecursive(Page, "txtJoinDate"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSREmploymentCategory, AppEnum.StandardReference.EmploymentCategory);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                txtEmployeeEmploymentPeriodID.Text = "1";

                StandardReference.InitializeIncludeSpace(cboSREmploymentType, AppEnum.StandardReference.EmploymentType, "0".Split(','));

                var coll = (EmployeeEmploymentPeriodCollection)Session["collEmployeeEmploymentPeriod" + Request.UserHostName + PageId];
                if (coll.Count != 0)
                {
                    var validToBefore = (coll.OrderByDescending(c => c.ValidTo).Select(c => c.ValidTo)).Take(1);
                    DateTime? validTo = validToBefore.Single().Value.AddDays(1);

                    txtValidFrom.SelectedDate = validTo;
                }
                else
                    txtValidFrom.SelectedDate = TxtJoinDate.SelectedDate;

                txtValidTo.SelectedDate = Convert.ToDateTime("1/1/2100");

                if (string.IsNullOrEmpty(Request.QueryString["status"]))
                {
                    txtEmployeeNo.Text = TxtEmployeeNumber.Text;
                    txtEmployeeNo.ReadOnly = true;
                }

                return;
            }
            ViewState["IsNewRecord"] = false;

            StandardReference.InitializeIncludeSpace(cboSREmploymentType, AppEnum.StandardReference.EmploymentType);

            txtEmployeeEmploymentPeriodID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeEmploymentPeriodMetadata.ColumnNames.EmployeeEmploymentPeriodID));
            if (DataBinder.Eval(DataItem, EmployeeEmploymentPeriodMetadata.ColumnNames.SREmploymentType) != null)
                cboSREmploymentType.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeeEmploymentPeriodMetadata.ColumnNames.SREmploymentType);
            if (DataBinder.Eval(DataItem, EmployeeEmploymentPeriodMetadata.ColumnNames.SREmploymentCategory) != null)
                cboSREmploymentCategory.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeeEmploymentPeriodMetadata.ColumnNames.SREmploymentCategory);
            txtValidFrom.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeEmploymentPeriodMetadata.ColumnNames.ValidFrom);
            txtValidTo.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeEmploymentPeriodMetadata.ColumnNames.ValidTo);
            txtNote.Text = (String)DataBinder.Eval(DataItem, EmployeeEmploymentPeriodMetadata.ColumnNames.Note);
            txtEmployeeNo.Text = (String)DataBinder.Eval(DataItem, EmployeeEmploymentPeriodMetadata.ColumnNames.EmployeeNumber);

            if (DataBinder.Eval(DataItem, EmployeeEmploymentPeriodMetadata.ColumnNames.RecruitmentPlanID) != null)
            {
                var query = new RecruitmentPlanQuery();
                query.es.Top = 20;
                query.Where(query.RecruitmentPlanID == DataBinder.Eval(DataItem, EmployeeEmploymentPeriodMetadata.ColumnNames.RecruitmentPlanID).ToStringDefaultEmpty());
                DataTable tab = query.LoadDataTable();
                cboRecruitmentPlanID.DataSource = tab;
                cboRecruitmentPlanID.DataBind();
                try
                {
                    if (tab.Rows.Count > 0)
                        cboRecruitmentPlanID.SelectedValue = DataBinder.Eval(DataItem, EmployeeEmploymentPeriodMetadata.ColumnNames.RecruitmentPlanID).ToStringDefaultEmpty();
                }
                catch
                { }
            }

            if (cboSREmploymentType.SelectedValue == "0")
            {
                cboSREmploymentType.Enabled = false;
                lblEmployeeNo.Text = "Applicant No";
            }
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                EmployeeEmploymentPeriodCollection coll = (EmployeeEmploymentPeriodCollection)Session["collEmployeeEmploymentPeriod" + Request.UserHostName + PageId];

                //bool isExist = false;
                bool a = false;
                bool b = false;
                bool c = false;
                DateTime sDate = txtValidFrom.SelectedDate ?? (new DateTime()).NowAtSqlServer();
                DateTime eDate = txtValidTo.SelectedDate ?? (new DateTime()).NowAtSqlServer().AddYears(1);

                foreach (EmployeeEmploymentPeriod item in coll)
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

                    //if (item.SREmploymentType == cboSREmploymentType.SelectedValue)
                    //{
                    //    isExist = true;
                    //    break;
                    //}
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
                //if (isExist)
                //{
                //    args.IsValid = false;
                //    ((CustomValidator)source).ErrorMessage = string.Format("Employment Type: {0} has exist", cboSREmploymentType.Text);
                //}

                if (cboSREmploymentType.SelectedValue != "0" && string.IsNullOrEmpty(txtEmployeeNo.Text))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Employee No required.");
                    return;
                }
            }
        }

        #region Properties for return entry value
        public Int32 EmployeeEmploymentPeriodID
        {
            get { return Convert.ToInt32(txtEmployeeEmploymentPeriodID.Text); }
        }
        public String SREmploymentType
        {
            get { return cboSREmploymentType.SelectedValue; }
        }
        public String EmploymentTypeName
        {
            get { return cboSREmploymentType.Text; }
        }
        public String SREmploymentCategory
        {
            get { return cboSREmploymentCategory.SelectedValue; }
        }
        public String EmploymentCategoryName
        {
            get { return cboSREmploymentCategory.Text; }
        }
        public DateTime ValidFrom
        {
            get { return Convert.ToDateTime(txtValidFrom.SelectedDate); }
        }
        public DateTime ValidTo
        {
            get { return Convert.ToDateTime(txtValidTo.SelectedDate); }
        }
        public String Note
        {
            get { return txtNote.Text; }
        }

        public Int32 RecruitmentPlanID
        {
            get { return string.IsNullOrEmpty(cboRecruitmentPlanID.SelectedValue) ? -1 : Convert.ToInt32(cboRecruitmentPlanID.SelectedValue); }
        }
        public String RecruitmentPlanName
        {
            get { return cboRecruitmentPlanID.Text; }
        }
        public String EmployeeNumber
        {
            get { return txtEmployeeNo.Text; }
        }

        #endregion

        #region Method & Event TextChanged

        #endregion

        protected void cboRecruitmentPlanID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            RecruitmentPlanQuery query = new RecruitmentPlanQuery();
            query.es.Top = 20;
            query.Where(query.RecruitmentPlanName.Like(searchTextContain), query.ValidFrom <= DateTime.Now, query.ValidTo >= DateTime.Now);
            cboRecruitmentPlanID.DataSource = query.LoadDataTable();
            cboRecruitmentPlanID.DataBind();
        }

        protected void cboRecruitmentPlanID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["RecruitmentPlanName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["RecruitmentPlanID"].ToString();
        }
    }
}
