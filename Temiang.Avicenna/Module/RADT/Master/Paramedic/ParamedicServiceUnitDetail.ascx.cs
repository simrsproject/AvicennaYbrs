using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ParamedicServiceUnitDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        public RadTextBox TxtParamedicID
        {
            get
            {
                return (RadTextBox)Helper.FindControlRecursive(Page, "txtParamedicID");
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            PopUpSearch.InitializeOnButtonClick(AppEnum.PopUpSearch.ServiceUnit, txtServiceUnitID);
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtServiceUnitID.Text = (String)DataBinder.Eval(DataItem, ServiceUnitParamedicMetadata.ColumnNames.ServiceUnitID);
            PopulateServiceUnitName(false);

            //PopulateRoomList();
            ComboBox.PopulateWithRoom(cbRoomDefault, (String)DataBinder.Eval(DataItem, ServiceUnitParamedicMetadata.ColumnNames.ServiceUnitID));
            cbRoomDefault.SelectedValue = (String)DataBinder.Eval(DataItem, ServiceUnitParamedicMetadata.ColumnNames.DefaultRoomID);
            chkIsUsingQue.Checked = (bool)DataBinder.Eval(DataItem, ServiceUnitParamedicMetadata.ColumnNames.IsUsingQue);
            chkIsAcceptBPJS.Checked = (bool)DataBinder.Eval(DataItem, ServiceUnitParamedicMetadata.ColumnNames.IsAcceptBPJS);
            chkIsAcceptNonBPJS.Checked = (bool)DataBinder.Eval(DataItem, ServiceUnitParamedicMetadata.ColumnNames.IsAcceptNonBPJS);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ServiceUnitParamedicCollection)Session["collServiceUnitParamedic"];

                string id = txtServiceUnitID.Text;
                bool isExist = false;
                foreach (ServiceUnitParamedic item in coll)
                {
                    if (item.ServiceUnitID.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Service Unit ID: {0} has exist", id);
                }
            }
        }

        #region Properties for return entry value
        public String ServiceUnitID
        {
            get { return txtServiceUnitID.Text; }
        }
        public String ServiceUnitName
        {
            get { return lblServiceUnitName.Text; }
        }
        public String DefaultRoomID
        {
            get { return cbRoomDefault.SelectedValue; }
        }
        public String DefaultRoomName
        {
            get { return cbRoomDefault.SelectedItem.Text; }
        }
        public Boolean IsUsingQue
        {
            get { return chkIsUsingQue.Checked; }
        }
        public Boolean IsAcceptBPJS
        {
            get { return chkIsAcceptBPJS.Checked; }
        }
        public Boolean IsAcceptNonBPJS
        {
            get { return chkIsAcceptNonBPJS.Checked; }
        }
        #endregion

        #region Method & Event TextChanged
        protected void txtServiceUnitID_TextChanged(object sender, EventArgs e)
        {
            PopulateServiceUnitName(true);
            ComboBox.PopulateWithRoom(cbRoomDefault, txtServiceUnitID.Text);
        }

        private void PopulateServiceUnitName(bool isResetIdIfNotExist)
        {
            if (txtServiceUnitID.Text == string.Empty)
            {
                lblServiceUnitName.Text = string.Empty;
                return;
            }
            var entity = new ServiceUnit();
            if (entity.LoadByPrimaryKey(txtServiceUnitID.Text))
                lblServiceUnitName.Text = entity.ServiceUnitName;
            else
            {
                lblServiceUnitName.Text = string.Empty;
                if (isResetIdIfNotExist)
                    txtServiceUnitID.Text = string.Empty;
            }
        }
        #endregion
    }
}