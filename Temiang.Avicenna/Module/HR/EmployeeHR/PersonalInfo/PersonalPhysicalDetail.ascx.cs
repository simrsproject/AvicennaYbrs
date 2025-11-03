using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class PersonalPhysicalDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private string PageId
        {
            get { return ((HiddenField)Helper.FindControlRecursive(Page, "hdnPageId")).Value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRPhysicalCharacteristic, AppEnum.StandardReference.PhysicalCharacteristic);
            StandardReference.InitializeIncludeSpace(cboSRMeasurementCode, AppEnum.StandardReference.MeasurementCode);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtPersonalPhysicalID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtPersonalPhysicalID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PersonalPhysicalMetadata.ColumnNames.PersonalPhysicalID));
            cboSRPhysicalCharacteristic.SelectedValue = (String)DataBinder.Eval(DataItem, PersonalPhysicalMetadata.ColumnNames.SRPhysicalCharacteristic);
            txtPhysicalValue.Text = (String)DataBinder.Eval(DataItem, PersonalPhysicalMetadata.ColumnNames.PhysicalValue);
            cboSRMeasurementCode.SelectedValue = (String)DataBinder.Eval(DataItem, PersonalPhysicalMetadata.ColumnNames.SRMeasurementCode);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                PersonalPhysicalCollection coll =
                    (PersonalPhysicalCollection)Session["collPersonalPhysical" + Request.UserHostName + PageId];

                //TODO: Betulkan cara pengecekannya
                string id = txtPersonalPhysicalID.Text;
                bool isExist = false;
                foreach (PersonalPhysical item in coll)
                {
                    if (item.PersonalPhysicalID.Equals(id))
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
        public Int32 PersonalPhysicalID
        {
            get { return Convert.ToInt32(txtPersonalPhysicalID.Text); }
        }
        public String SRPhysicalCharacteristic
        {
            get { return cboSRPhysicalCharacteristic.SelectedValue; }
        }
        public String PhysicalCharacteristicName
        {
            get { return cboSRPhysicalCharacteristic.Text; }
        }
        public String PhysicalValue
        {
            get { return txtPhysicalValue.Text; }
        }
        public String SRMeasurementCode
        {
            get { return cboSRMeasurementCode.SelectedValue; }
        }
        public String MeasurementCodeName
        {
            get { return cboSRMeasurementCode.Text; }
        }
        #endregion
        #region Method & Event TextChanged

        #endregion
    }
}
