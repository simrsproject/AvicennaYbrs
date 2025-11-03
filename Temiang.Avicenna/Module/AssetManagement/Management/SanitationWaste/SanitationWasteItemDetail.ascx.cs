using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.AssetManagement.Management
{
    public partial class SanitationWasteItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRWasteType, AppEnum.StandardReference.WasteType);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;
            
            cboSRWasteType.SelectedValue = (String)DataBinder.Eval(DataItem, SanitationWasteTransItemMetadata.ColumnNames.SRWasteType);
            txtQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, SanitationWasteTransItemMetadata.ColumnNames.Qty));
            hdnReferenceNo.Value = (String)DataBinder.Eval(DataItem, SanitationWasteTransItemMetadata.ColumnNames.ReferenceNo);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(cboSRWasteType.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Waste Type required.");
                return;
            }

            if (txtQty.Value <= 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Quantity Must Bigger than 0.");
                return;
            }

            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (SanitationWasteTransItemCollection)Session["collSanitationWasteTransItem" + Request.UserHostName];

                bool isExist = coll.Any(i => (i.SRWasteType.Equals(cboSRWasteType.SelectedValue)));

                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Waste Type : {0} has exist.", cboSRWasteType.Text);
                    return;
                }

            }
        }

        #region Properties for return entry value
        public String SRWasteType
        {
            get { return cboSRWasteType.SelectedValue; }
        }
        public String WasteTypeName
        {
            get { return cboSRWasteType.Text; }
        }
        public Decimal Qty
        {
            get { return Convert.ToDecimal(txtQty.Value); }
        }
        public String ReferenceNo
        {
            get { return hdnReferenceNo.Value; }
        }
        #endregion
    }
}