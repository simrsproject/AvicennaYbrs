using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Master
{
    public partial class CompanyEducationProfileDetail : BaseUserControl
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
                txtCompanyEducationProfileID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtCompanyEducationProfileID.Value = Convert.ToInt32(DataBinder.Eval(DataItem, CompanyEducationProfileMetadata.ColumnNames.CompanyEducationProfileID));
            txtCompanyEducationProfileCode.Text = (String)DataBinder.Eval(DataItem, CompanyEducationProfileMetadata.ColumnNames.CompanyEducationProfileCode);
            txtCompanyEducationProfileName.Text = (String)DataBinder.Eval(DataItem, CompanyEducationProfileMetadata.ColumnNames.CompanyEducationProfileName);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                CompanyEducationProfileCollection coll =
                    (CompanyEducationProfileCollection)Session["collCompanyEducationProfile"];

                //TODO: Betulkan cara pengecekannya
                string id = txtCompanyEducationProfileCode.Text;
                bool isExist = false;
                foreach (CompanyEducationProfile item in coll)
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
        public Int32 CompanyEducationProfileID
        {
            get { return Convert.ToInt32(txtCompanyEducationProfileID.Text); }
        }
        public String CompanyEducationProfileCode
        {
            get { return txtCompanyEducationProfileCode.Text; }
        }
        public String CompanyEducationProfileName
        {
            get { return txtCompanyEducationProfileName.Text; }
        }
        #endregion
        #region Method & Event TextChanged

        #endregion
    }
}
