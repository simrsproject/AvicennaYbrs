using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ReasonForTreatmentDetailDescDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            //PopUpSearch.InitializeOnButtonClick(AppEnum.PopUpSearch.Diagnose, txtDiagnoseID);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

            //    chkIsActive.Checked = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtReasonsForTreatmentDescID.Text = (String)DataBinder.Eval(DataItem, ReasonsForTreatmentDescMetadata.ColumnNames.ReasonsForTreatmentDescID);
            txtReasonsForTreatmentDescName.Text = (String)DataBinder.Eval(DataItem, ReasonsForTreatmentDescMetadata.ColumnNames.ReasonsForTreatmentDescName);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ReasonsForTreatmentDescCollection)Session["collReasonsForTreatmentDesc"];

                string rftID = ((RadTextBox)Helper.FindControlRecursive(Page, "txtReasonsForTreatmentID")).Text;
                string rftdID = txtReasonsForTreatmentDescID.Text;
                bool isExist = false;
                foreach (ReasonsForTreatmentDesc item in coll)
                {
                    if (item.ReasonsForTreatmentDescID.Equals(rftdID) && item.ReasonsForTreatmentID.Equals(rftID))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", rftdID);
                }
            }
        }

        #region Properties for return entry value

        public String ReasonsForTreatmentDescID
        {
            get { return txtReasonsForTreatmentDescID.Text; }
        }

        public String ReasonsForTreatmentDescName
        {
            get { return txtReasonsForTreatmentDescName.Text; }
        }

        #endregion
    }
}