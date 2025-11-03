using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ContributoryFactorsClassificationFrameworkItemDetail : BaseUserControl
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

                return;
            }
            ViewState["IsNewRecord"] = false;

            txtFactorItemID.Text = (String)DataBinder.Eval(DataItem, ContributoryFactorsClassificationFrameworkItemMetadata.ColumnNames.FactorItemID);
            txtFactorItemName.Text = (String)DataBinder.Eval(DataItem, ContributoryFactorsClassificationFrameworkItemMetadata.ColumnNames.FactorItemName);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ContributoryFactorsClassificationFrameworkItemCollection)Session["collContributoryFactorsClassificationFrameworkItem"];

                string factorItemId = txtFactorItemID.Text;
                bool isExist = false;
                foreach (var item in coll)
                {
                    if (item.FactorItemID.Equals(factorItemId))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Factor Item ID : {0} already exist", factorItemId);
                }
            }
        }

        #region Properties for return entry value

        public String FactorItemID
        {
            get { return txtFactorItemID.Text; }
        }

        public String FactorItemName
        {
            get { return txtFactorItemName.Text; }
        }

        #endregion
    }
}