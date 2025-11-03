using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.PositionInformation
{
    public partial class PositionAuthorityDetail : BaseUserControl
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
                ////misal --> chkIsActive.Checked = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtAuthorityName.Text = (String)DataBinder.Eval(DataItem, PositionAuthorityMetadata.ColumnNames.AuthorityName);
            txtDescription.Text = (String)DataBinder.Eval(DataItem, PositionAuthorityMetadata.ColumnNames.Description);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                PositionAuthorityCollection coll =
                    (PositionAuthorityCollection)Session["collPositionAuthority"];

                //TODO: Betulkan cara pengecekannya
                string authority = txtAuthorityName.Text;
                bool isExist = false;
                foreach (PositionAuthority item in coll)
                {
                    if (item.AuthorityName.Equals(authority))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Authority: {0} has exist", authority);
                }
            }
        }

        #region Properties for return entry value

        public String AuthorityName
        {
            get { return txtAuthorityName.Text; }
        }
        public String Description
        {
            get { return txtDescription.Text; }
        }
        #endregion
        #region Method & Event TextChanged

        #endregion
    }
}