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

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class PpiInfectionDetail : BaseUserControl
    {
        public object DataItem { get; set; }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRInfectionType, AppEnum.StandardReference.InfectionType);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }

            ViewState["IsNewRecord"] = false;
            cboSRInfectionType.Enabled = false;

            cboSRInfectionType.SelectedValue = (String)DataBinder.Eval(DataItem, PpiInfectionMetadata.ColumnNames.SRInfectionType);
            txtDaysTo.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PpiInfectionMetadata.ColumnNames.DaysTo));
            txtCultures.Text = (String)DataBinder.Eval(DataItem, PpiInfectionMetadata.ColumnNames.Cultures);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (PpiInfectionCollection)Session["collPpiInfection"];

                string id = cboSRInfectionType.SelectedValue;
                bool isExist = false;

                foreach (PpiInfection item in coll)
                {
                    if (item.SRInfectionType.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage =
                        string.Format("Infection : {0} already exist", cboSRInfectionType.Text);
                }
            }
        }

        #region Properties for return entry value

        public String SRInfectionType
        {
            get { return cboSRInfectionType.SelectedValue; }
        }

        public String InfectionTypeName
        {
            get { return cboSRInfectionType.Text; }
        }

        public Int16 DaysTo
        {
            get { return Convert.ToInt16(txtDaysTo.Value); }
        }

        public String Cultures
        {
            get { return txtCultures.Text; }
        }

        #endregion
    }
}