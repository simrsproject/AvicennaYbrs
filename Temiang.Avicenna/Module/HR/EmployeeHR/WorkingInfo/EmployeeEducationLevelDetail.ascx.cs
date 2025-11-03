using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class EmployeeEducationLevelDetail : BaseUserControl
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
            StandardReference.InitializeIncludeSpace(cboSREducationLevel, AppEnum.StandardReference.EducationLevel);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtEmployeeEducationLevelID.Text = "1";
                txtValidTo.SelectedDate = Convert.ToDateTime("1/1/2100");
                
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtEmployeeEducationLevelID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeEducationLevelMetadata.ColumnNames.EmployeeEducationLevelID));
            cboSREducationLevel.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeeEducationLevelMetadata.ColumnNames.SREducationLevel);
            txtValidFrom.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeEducationLevelMetadata.ColumnNames.ValidFrom);
            txtValidTo.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeEducationLevelMetadata.ColumnNames.ValidTo);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                EmployeeEducationLevelCollection coll = (EmployeeEducationLevelCollection)Session["collEmployeeEducationLevel" + Request.UserHostName + PageId];

                bool isExist = false;
                bool a = false;
                bool b = false;
                bool c = false;
                DateTime sDate = txtValidFrom.SelectedDate ?? (new DateTime()).NowAtSqlServer();
                DateTime eDate = txtValidTo.SelectedDate ?? (new DateTime()).NowAtSqlServer().AddYears(1);

                foreach (EmployeeEducationLevel item in coll)
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

                    //if (item.ValidFrom < DateTime.Now && item.ValidTo > DateTime.Now)
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
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Valid Education Level Period has exist.");
                }
            }
        }

        #region Properties for return entry value
        public Int32 EmployeeEducationLevelID
        {
            get { return Convert.ToInt32(txtEmployeeEducationLevelID.Text); }
        }
        public String SREducationLevel
        {
            get { return cboSREducationLevel.SelectedValue; }
        }
        public String EducationLevelName
        {
            get { return cboSREducationLevel.Text; }
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
    }
}