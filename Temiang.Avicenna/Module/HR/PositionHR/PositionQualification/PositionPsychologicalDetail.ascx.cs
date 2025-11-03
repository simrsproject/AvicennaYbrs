using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.PositionInformation
{
    public partial class PositionPsychologicalDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRPsychological, AppEnum.StandardReference.Psychological);
            StandardReference.InitializeIncludeSpace(cboSROperandType, AppEnum.StandardReference.OperandType);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtPositionPsychologicalID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtPositionPsychologicalID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PositionPsychologicalMetadata.ColumnNames.PositionPsychologicalID));
            txtPositionID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PositionPsychologicalMetadata.ColumnNames.PositionID));
            cboSRPsychological.SelectedValue = (String)DataBinder.Eval(DataItem, PositionPsychologicalMetadata.ColumnNames.SRPsychological);
            cboSROperandType.SelectedValue = (String)DataBinder.Eval(DataItem, PositionPsychologicalMetadata.ColumnNames.SROperandType);
            txtPsychologicalValue.Text = (String)DataBinder.Eval(DataItem, PositionPsychologicalMetadata.ColumnNames.PsychologicalValue);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                PositionPsychologicalCollection coll =
                    (PositionPsychologicalCollection)Session["collPositionPsychological"];

                //TODO: Betulkan cara pengecekannya
                string id = txtPositionID.Text;
                bool isExist = false;
                foreach (PositionPsychological item in coll)
                {
                    if (item.PositionID.Equals(id))
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
        public Int32 PositionPsychologicalID
        {
            get { return Convert.ToInt32(txtPositionPsychologicalID.Text); }
        }
        
        public String SRPsychological
        {
            get { return cboSRPsychological.SelectedValue; }
        }
        public String PsychologicalName
        {
            get { return cboSRPsychological.Text; }
        }
        public String PsychologicalValue
        {
            get { return txtPsychologicalValue.Text; }
        }
        public String SROperandType
        {
            get { return cboSROperandType.SelectedValue; }
        }        
        public String OperandTypeName
        {
            get { return cboSROperandType.Text; }
        }
        #endregion
        #region Method & Event TextChanged
        
        #endregion
    }
}
