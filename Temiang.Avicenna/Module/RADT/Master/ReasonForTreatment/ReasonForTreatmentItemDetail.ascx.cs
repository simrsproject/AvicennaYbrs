using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ReasonForTreatmentItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            PopUpSearch.InitializeOnButtonClick(AppEnum.PopUpSearch.Diagnose, txtDiagnoseID);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                chkIsActive.Checked = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtReasonsForTreatmentID.Text = (String)DataBinder.Eval(DataItem, ReasonsForTreatmentMetadata.ColumnNames.ReasonsForTreatmentID);
            txtReasonsForTreatmentName.Text = (String)DataBinder.Eval(DataItem, ReasonsForTreatmentMetadata.ColumnNames.ReasonsForTreatmentName);
            txtDiagnoseID.Text = (String)DataBinder.Eval(DataItem, ReasonsForTreatmentMetadata.ColumnNames.DiagnoseID);
            if (!string.IsNullOrEmpty(txtDiagnoseID.Text))
            {
                var d = new Diagnose();
                txtDiagnoseName.Text = d.LoadByPrimaryKey(txtDiagnoseID.Text) ? d.DiagnoseName : string.Empty;
            }
            chkIsActive.Checked = (Boolean)DataBinder.Eval(DataItem, ReasonsForTreatmentMetadata.ColumnNames.IsActive);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ReasonsForTreatmentCollection)Session["collReasonsForTreatment"];

                string itemId = txtReasonsForTreatmentID.Text;
                bool isExist = false;
                foreach (ReasonsForTreatment item in coll)
                {
                    if (item.ReasonsForTreatmentID.Equals(itemId))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", itemId);
                }
            }
        }

        #region Properties for return entry value

        public String ReasonsForTreatmentID
        {
            get { return txtReasonsForTreatmentID.Text; }
        }

        public String ReasonsForTreatmentName
        {
            get { return txtReasonsForTreatmentName.Text; }
        }

        public String DiagnoseID
        {
            get { return txtDiagnoseID.Text; }
        }

        public String DiagnoseName
        {
            get { return txtDiagnoseName.Text; }
        }

        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }

        #endregion

        protected void txtDiagnoseID_TextChanged(object sender, EventArgs e)
        {
            var d = new Diagnose();
            if (d.LoadByPrimaryKey(txtDiagnoseID.Text))
                txtDiagnoseName.Text = d.DiagnoseName;
            else
            {
                txtDiagnoseID.Text = string.Empty;
                txtDiagnoseName.Text = string.Empty;
            }
        }
    }
}