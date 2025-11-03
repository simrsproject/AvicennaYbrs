using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ParamedicOtherTypeDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRParamedicType, AppEnum.StandardReference.ParamedicType);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                
                return;
            }
            ViewState["IsNewRecord"] = false;

            cboSRParamedicType.SelectedValue = (string)DataBinder.Eval(DataItem, ParamedicOtherTypeMetadata.ColumnNames.SRParamedicType);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ParamedicOtherTypeCollection)Session["collParamedicOtherType"];

                string id = cboSRParamedicType.SelectedValue;
                bool isExist = false;
                foreach (var item in coll)
                {
                    if (item.SRParamedicType.Equals(cboSRParamedicType.SelectedValue))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Physician Type : {0} already exist", cboSRParamedicType.Text);
                }
            }
        }

        #region Properties for return entry value

        public String SRParamedicType
        {
            get { return cboSRParamedicType.SelectedValue; }
        }

        public String ParamedicTypeName
        {
            get { return cboSRParamedicType.Text; }
        }

        #endregion
    }
}