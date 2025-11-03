using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class PersonalContactDetail : BaseUserControl
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
            StandardReference.InitializeIncludeSpace(cboSRContactType, AppEnum.StandardReference.ContactType);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtPersonalContactID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtPersonalContactID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PersonalContactMetadata.ColumnNames.PersonalContactID));
            cboSRContactType.SelectedValue = (String)DataBinder.Eval(DataItem, PersonalContactMetadata.ColumnNames.SRContactType);
            txtContactValue.Text = (String)DataBinder.Eval(DataItem, PersonalContactMetadata.ColumnNames.ContactValue);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            //if (ViewState["IsNewRecord"].Equals(true))
            //{
            //    PersonalContactCollection coll =
            //        (PersonalContactCollection)Session["collPersonalContact" + Request.UserHostName + PageId];

            //    //TODO: Betulkan cara pengecekannya
            //    string id = txtPersonalContactID.Text;
            //    bool isExist = false;
            //    foreach (PersonalContact item in coll)
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
        public Int32 PersonalContactID
        {
            get { return Convert.ToInt32(txtPersonalContactID.Text); }
        }
        public String SRContactType
        {
            get { return cboSRContactType.SelectedValue; }
        }
        public String ContactTypeName
        {
            get { return cboSRContactType.Text; }
        }
        public String ContactValue
        {
            get { return txtContactValue.Text; }
        }
        #endregion
        #region Method & Event TextChanged

        #endregion
    }
}
