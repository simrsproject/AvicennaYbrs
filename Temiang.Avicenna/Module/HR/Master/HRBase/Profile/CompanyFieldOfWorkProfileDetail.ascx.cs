using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Master
{
    public partial class CompanyFieldOfWorkProfileDetail : BaseUserControl
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

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtCompanyFieldOfWorkProfileID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtCompanyFieldOfWorkProfileID.Value = Convert.ToInt32(DataBinder.Eval(DataItem, CompanyFieldOfWorkProfileMetadata.ColumnNames.CompanyFieldOfWorkProfileID));
            txtCompanyFieldOfWorkProfileCode.Text = (String)DataBinder.Eval(DataItem, CompanyFieldOfWorkProfileMetadata.ColumnNames.CompanyFieldOfWorkProfileCode);
            txtCompanyFieldOfWorkProfileName.Text = (String)DataBinder.Eval(DataItem, CompanyFieldOfWorkProfileMetadata.ColumnNames.CompanyFieldOfWorkProfileName);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                CompanyFieldOfWorkProfileCollection coll =
                    (CompanyFieldOfWorkProfileCollection)Session["collCompanyFieldOfWorkProfile"];

                //TODO: Betulkan cara pengecekannya
                string id = txtCompanyFieldOfWorkProfileCode.Text;
                bool isExist = false;
                foreach (CompanyFieldOfWorkProfile item in coll)
                {
                    if (item.CompanyLaborProfileID.Equals(id))
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
        public Int32 CompanyFieldOfWorkProfileID
        {
            get { return Convert.ToInt32(txtCompanyFieldOfWorkProfileID.Text); }
        }
        public String CompanyFieldOfWorkProfileCode
        {
            get { return txtCompanyFieldOfWorkProfileCode.Text; }
        }
        public String CompanyFieldOfWorkProfileName
        {
            get { return txtCompanyFieldOfWorkProfileName.Text; }
        }
        #endregion
        #region Method & Event TextChanged

        #endregion
    }
}
