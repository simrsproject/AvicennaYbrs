using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ServiceUnitParamedicDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        public RadTextBox TxtServiceUnit
        {
            get {
                return (RadTextBox)Helper.FindControlRecursive(Page, "txtServiceUnitID");
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            PopUpSearch.InitializeOnButtonClick(AppEnum.PopUpSearch.Paramedic,txtParamedicID);
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                ComboBox.PopulateWithRoom(cbRoomDefault, TxtServiceUnit.Text);
                return;
            }
            ViewState["IsNewRecord"] = false;
            
            txtParamedicID.Text = (String)DataBinder.Eval(DataItem, ServiceUnitParamedicMetadata.ColumnNames.ParamedicID);		
    	    PopulateParamedicName(false);

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
                ServiceUnitParamedicCollection coll =
                    (ServiceUnitParamedicCollection) Session["collServiceUnitParamedic"];
				
                string id = txtParamedicID.Text;
                bool isExist = false;
                foreach (ServiceUnitParamedic item in coll)
                {
                    if (item.ParamedicID.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator) source).ErrorMessage = string.Format("Physician ID: {0} has exist", id);
                }
            }
        }
		
		#region Properties for return entry value
	 	public String ParamedicID
        {
            get { return txtParamedicID.Text;}	 
	 	}
        public String ParamedicName
        {
            get { return lblParamedicName.Text; }
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
        protected void txtParamedicID_TextChanged(object sender, EventArgs e)
        {
            PopulateParamedicName(true);
        }

        private void PopulateParamedicName(bool isResetIdIfNotExist)
        {
			if (txtParamedicID.Text == string.Empty)
			{
				lblParamedicName.Text = string.Empty;
				return;
			}
            Paramedic entity = new Paramedic();
            if (entity.LoadByPrimaryKey(txtParamedicID.Text))
                lblParamedicName.Text = entity.ParamedicName;
            else
            {
                lblParamedicName.Text = string.Empty;
                if (isResetIdIfNotExist)
                    txtParamedicID.Text = string.Empty;
            }
        }		
		#endregion
    }
}
