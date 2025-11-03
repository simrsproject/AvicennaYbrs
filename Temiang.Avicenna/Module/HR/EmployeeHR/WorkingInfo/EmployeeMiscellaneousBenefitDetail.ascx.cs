using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class EmployeeMiscellaneousBenefitDetail : BaseUserControl
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
            StandardReference.InitializeIncludeSpace(cboSRMiscellaneousBenefit, AppEnum.StandardReference.MiscellaneousBenefit);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtEmployeeMiscellaneousBenefitID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtEmployeeMiscellaneousBenefitID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeMiscellaneousBenefitMetadata.ColumnNames.EmployeeMiscellaneousBenefitID));
            cboSRMiscellaneousBenefit.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeeMiscellaneousBenefitMetadata.ColumnNames.SRMiscellaneousBenefit);
            txtValidFrom.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeMiscellaneousBenefitMetadata.ColumnNames.ValidFrom);
            txtValidTo.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeMiscellaneousBenefitMetadata.ColumnNames.ValidTo);
            txtNote.Text = (String)DataBinder.Eval(DataItem, EmployeeMiscellaneousBenefitMetadata.ColumnNames.Note);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                EmployeeMiscellaneousBenefitCollection coll =
                    (EmployeeMiscellaneousBenefitCollection)Session["collEmployeeMiscellaneousBenefit" + Request.UserHostName + PageId];

                //TODO: Betulkan cara pengecekannya
                string id = cboSRMiscellaneousBenefit.SelectedValue;
                bool isExist = false;
                foreach (EmployeeMiscellaneousBenefit item in coll)
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
                }
            }
        }

        #region Properties for return entry value
        public Int32 EmployeeMiscellaneousBenefitID
        {
            get { return Convert.ToInt32(txtEmployeeMiscellaneousBenefitID.Text); }
        }
        public String SRMiscellaneousBenefit
        {
            get { return cboSRMiscellaneousBenefit.SelectedValue; }
        }
        public String MiscellaneousBenefitName
        {
            get { return cboSRMiscellaneousBenefit.Text; }
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
        #endregion
        #region Method & Event TextChanged

        #endregion
    }
}
