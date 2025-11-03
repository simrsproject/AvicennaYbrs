using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class EmployeeDisciplinaryDetail : BaseUserControl
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
            StandardReference.InitializeIncludeSpace(cboSRWarningLevel, AppEnum.StandardReference.WarningLevel);
            StandardReference.InitializeIncludeSpace(cboSRViolationDegree, AppEnum.StandardReference.ViolationDegree);
            StandardReference.InitializeIncludeSpace(cboSRViolationType, AppEnum.StandardReference.ViolationType);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtEmployeeDisciplinaryID.Text="1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtEmployeeDisciplinaryID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeDisciplinaryMetadata.ColumnNames.EmployeeDisciplinaryID));
            cboSRWarningLevel.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeeDisciplinaryMetadata.ColumnNames.SRWarningLevel);
            txtIncidentDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeDisciplinaryMetadata.ColumnNames.IncidentDate);
            txtDateIssue.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeDisciplinaryMetadata.ColumnNames.DateIssue);
            txtViolation.Text = (String)DataBinder.Eval(DataItem, EmployeeDisciplinaryMetadata.ColumnNames.Violation);
            txtEffectViolation.Text = (String)DataBinder.Eval(DataItem, EmployeeDisciplinaryMetadata.ColumnNames.EffectViolation);
            txtAdviceGiven.Text = (String)DataBinder.Eval(DataItem, EmployeeDisciplinaryMetadata.ColumnNames.AdviceGiven);
            txtSanctionGiven.Text = (String)DataBinder.Eval(DataItem, EmployeeDisciplinaryMetadata.ColumnNames.SanctionGiven);
            cboSRViolationDegree.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeeDisciplinaryMetadata.ColumnNames.SRViolationDegree);
            cboSRViolationType.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeeDisciplinaryMetadata.ColumnNames.SRViolationType);
            txtNote.Text = (String)DataBinder.Eval(DataItem, EmployeeDisciplinaryMetadata.ColumnNames.Note);
            txtEffectiveDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeDisciplinaryMetadata.ColumnNames.EffectiveDate);
            txtValidUntil.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeDisciplinaryMetadata.ColumnNames.ValidUntil);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            //if (ViewState["IsNewRecord"].Equals(true))
            //{
            //    EmployeeDisciplinaryCollection coll =
            //        (EmployeeDisciplinaryCollection)Session["collEmployeeDisciplinary" + Request.UserHostName + PageId];

            //    //TODO: Betulkan cara pengecekannya
            //    string id = txtEmployeeDisciplinaryID.Text;
            //    bool isExist = false;
            //    foreach (EmployeeDisciplinary item in coll)
            //    {
            //        if (item.PersonID.Equals(id))
            //        {
            //            isExist = true;
            //            break;
            //        }
            //    }
            //    if (isExist)
            //    {
            //        args.IsValid = false;
            //        ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", id);
            //    }
            //}
        }

        #region Properties for return entry value
        public Int32 EmployeeDisciplinaryID
        {
            get { return Convert.ToInt32(txtEmployeeDisciplinaryID.Text); }
        }
        public String SRWarningLevel
        {
            get { return cboSRWarningLevel.SelectedValue; }
        }
        public String WarningLevelName
        {
            get { return cboSRWarningLevel.Text; }
        }
        public DateTime IncidentDate
        {
            get { return Convert.ToDateTime(txtIncidentDate.SelectedDate); }
        }
        public DateTime DateIssue
        {
            get { return Convert.ToDateTime(txtDateIssue.SelectedDate); }
        }
        public String Violation
        {
            get { return txtViolation.Text; }
        }
        public String EffectViolation
        {
            get { return txtEffectViolation.Text; }
        }
        public String AdviceGiven
        {
            get { return txtAdviceGiven.Text; }
        }
        public String SanctionGiven
        {
            get { return txtSanctionGiven.Text; }
        }
        public String SRViolationDegree
        {
            get { return cboSRViolationDegree.SelectedValue; }
        }
        public String ViolationDegreeName
        {
            get { return cboSRViolationDegree.Text; }
        }
        public String SRViolationType
        {
            get { return cboSRViolationType.SelectedValue; }
        }
        public String ViolationTypeName
        {
            get { return cboSRViolationType.Text; }
        }
        public String Note
        {
            get { return txtNote.Text; }
        }
        public DateTime EffectiveDate
        {
            get { return Convert.ToDateTime(txtEffectiveDate.SelectedDate); }
        }
        public DateTime ValidUntil
        {
            get { return Convert.ToDateTime(txtValidUntil.SelectedDate); }
        }
        #endregion

        #region Method & Event TextChanged

        #endregion
    }
}
