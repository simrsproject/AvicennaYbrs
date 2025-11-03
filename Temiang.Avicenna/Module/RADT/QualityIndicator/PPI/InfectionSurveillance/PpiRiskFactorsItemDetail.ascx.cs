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
    public partial class PpiRiskFactorsItemDetail : BaseUserControl
    {
        public object DataItem { get; set; }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRSignsOfInfection, AppEnum.StandardReference.SignsOfInfection);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }

            ViewState["IsNewRecord"] = false;
            txtDateOfInfection.Enabled = false;
            cboSRSignsOfInfection.Enabled = false;
            
            object dateOfInfection = DataBinder.Eval(DataItem, PpiRiskFactorsItemMetadata.ColumnNames.DateOfInfection);
            if (dateOfInfection != null)
                txtDateOfInfection.SelectedDate = (DateTime)DataBinder.Eval(DataItem, PpiRiskFactorsItemMetadata.ColumnNames.DateOfInfection);
            else
                txtDateOfInfection.Clear();

            cboSRSignsOfInfection.SelectedValue = (String)DataBinder.Eval(DataItem, PpiRiskFactorsItemMetadata.ColumnNames.SRSignsOfInfection);
            txtNotes.Text = (String)DataBinder.Eval(DataItem, PpiRiskFactorsItemMetadata.ColumnNames.Notes);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (PpiRiskFactorsItemCollection)Session["collPpiRiskFactorsItem"];

                DateTime date = txtDateOfInfection.SelectedDate ?? (new DateTime()).NowAtSqlServer();
                string id = cboSRSignsOfInfection.SelectedValue;
                bool isExist = false;

                foreach (PpiRiskFactorsItem item in coll)
                {
                    if (item.DateOfInfection.Equals(date) && item.SRSignsOfInfection.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage =
                        string.Format("Date : {0} with Sign Of Infection : {1} already exist", date.ToString("dd-MMM-yyyy"), cboSRSignsOfInfection.Text);
                }
            }
        }

        #region Properties for return entry value
        public DateTime? DateOfInfection
        {
            get { return txtDateOfInfection.SelectedDate; }
        }

        public String SRSignsOfInfection
        {
            get { return cboSRSignsOfInfection.SelectedValue; }
        }

        public String SignsOfInfectionName
        {
            get { return cboSRSignsOfInfection.Text; }
        }

        public String Notes
        {
            get { return txtNotes.Text; }
        }

        #endregion
    }
}