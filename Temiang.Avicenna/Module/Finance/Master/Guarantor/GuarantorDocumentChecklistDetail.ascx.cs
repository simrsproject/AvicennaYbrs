using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class GuarantorDocumentChecklistDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRRegistrationType, AppEnum.StandardReference.RegistrationType);
            StandardReference.InitializeIncludeSpace(cboSRDocumentChecklist, AppEnum.StandardReference.DocumentChecklist);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;
            cboSRRegistrationType.Enabled = false;

            cboSRRegistrationType.SelectedValue = (String)DataBinder.Eval(DataItem, GuarantorDocumentChecklistMetadata.ColumnNames.SRRegistrationType);
            cboSRDocumentChecklist.SelectedValue = (String)DataBinder.Eval(DataItem, GuarantorDocumentChecklistMetadata.ColumnNames.SRDocumentChecklist);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (GuarantorDocumentChecklistCollection)Session["collGuarantorDocumentChecklist"];

                bool isExist = false;
                foreach (var item in coll)
                {
                    if (item.SRRegistrationType.Equals(cboSRRegistrationType.SelectedValue))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Registration Type: {0} already exist", cboSRRegistrationType.Text);
                }
            }
        }

        public String SRRegistrationType
        {
            get { return cboSRRegistrationType.SelectedValue; }
        }

        public String RegistrationType
        {
            get { return cboSRRegistrationType.Text; }
        }

        public String SRDocumentChecklist
        {
            get { return cboSRDocumentChecklist.SelectedValue; }
        }

        public String DocumentChecklistName
        {
            get { return cboSRDocumentChecklist.Text; }
        }
    }
}