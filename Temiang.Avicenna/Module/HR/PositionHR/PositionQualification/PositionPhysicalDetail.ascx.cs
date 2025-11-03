using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.PositionInformation
{
    public partial class PositionPhysicalDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRPhysicalCharacteristic, AppEnum.StandardReference.PhysicalCharacteristic);
            StandardReference.InitializeIncludeSpace(cboSROperandType, AppEnum.StandardReference.OperandType);
            StandardReference.InitializeIncludeSpace(cboSRMeasurementCode, AppEnum.StandardReference.MeasurementCode);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtPositionPhysicalID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtPositionPhysicalID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PositionPhysicalMetadata.ColumnNames.PositionPhysicalID));
            txtPositionID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PositionPhysicalMetadata.ColumnNames.PositionID));
            cboSRPhysicalCharacteristic.SelectedValue = (String)DataBinder.Eval(DataItem, PositionPhysicalMetadata.ColumnNames.SRPhysicalCharacteristic);
            cboSROperandType.SelectedValue = (String)DataBinder.Eval(DataItem, PositionPhysicalMetadata.ColumnNames.SROperandType);
            txtPhysicalValue.Text = (String)DataBinder.Eval(DataItem, PositionPhysicalMetadata.ColumnNames.PhysicalValue);
            cboSRMeasurementCode.SelectedValue = (String)DataBinder.Eval(DataItem, PositionPhysicalMetadata.ColumnNames.SRMeasurementCode);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                PositionPhysicalCollection coll =
                    (PositionPhysicalCollection)Session["collPositionPhysical"];

                //TODO: Betulkan cara pengecekannya
                string id = txtPositionID.Text;
                bool isExist = false;
                foreach (PositionPhysical item in coll)
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
        public Int32 PositionPhysicalID
        {
            get { return Convert.ToInt32(txtPositionPhysicalID.Text); }
        }
        public Int32 PositionID
        {
            get { return Convert.ToInt32(txtPositionID.Text); }
        }
        public String SRPhysicalCharacteristic
        {
            get { return cboSRPhysicalCharacteristic.SelectedValue; }
        }
        public String PhysicalCharacteristicName
        {
            get { return cboSRPhysicalCharacteristic.Text; }
        }
        public String OperandTypeName
        {
            get { return cboSROperandType.Text; }
        }
        public String SROperandType
        {
            get { return cboSROperandType.SelectedValue; }
        }
        public String PhysicalValue
        {
            get { return txtPhysicalValue.Text; }
        }
        public String SRMeasurementCode
        {
            get { return cboSRMeasurementCode.SelectedValue; }
        }
        public String MeasurementName
        {
            get { return cboSRMeasurementCode.Text; }
        }
        #endregion
        #region Method & Event TextChanged
        
        #endregion
    }
}
