using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ServiceUnitVisitTypeDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            PopUpSearch.InitializeOnButtonClick(AppEnum.PopUpSearch.VisitType,txtVisitTypeID);
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                return;
            }
            ViewState["IsNewRecord"] = false;
            txtVisitTypeID.Enabled = false;
            txtVisitTypeID.ShowButton = false;
			txtVisitTypeID.Text = (String)DataBinder.Eval(DataItem, ServiceUnitVisitTypeMetadata.ColumnNames.VisitTypeID);		
			txtVisitDuration.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ServiceUnitVisitTypeMetadata.ColumnNames.VisitDuration));
    	    PopulateVisitTypeName(false);
        }
		protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                BasePage basePage = (BasePage)this.Page;
                ServiceUnitVisitTypeCollection coll =
                    (ServiceUnitVisitTypeCollection)Session["collServiceUnitVisitType_" + basePage.PageID];
				
                string id = txtVisitTypeID.Text;
                bool isExist = false;
                foreach (ServiceUnitVisitType item in coll)
                {
                    if (item.VisitTypeID.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator) source).ErrorMessage = string.Format("ID: {0} has exist", id);
                }
            }
        }
		
		#region Properties for return entry value
	 	public String VisitTypeID
        {
            get { return txtVisitTypeID.Text;}	 
	 	}
        public String VisitTypeName
        {
            get { return lblVisitTypeName.Text; }
        } 
	 	public Byte VisitDuration
        {
            get { return Convert.ToByte(txtVisitDuration.Value);}	 
	 	} 
        #endregion
		#region Method & Event TextChanged
        protected void txtVisitTypeID_TextChanged(object sender, EventArgs e)
        {
            PopulateVisitTypeName(true);
        }

        private void PopulateVisitTypeName(bool isResetIdIfNotExist)
        {
			if (txtVisitTypeID.Text == string.Empty)
			{
				lblVisitTypeName.Text = string.Empty;
				return;
			}
            VisitType entity = new VisitType();
            if (entity.LoadByPrimaryKey(txtVisitTypeID.Text))
                lblVisitTypeName.Text = entity.VisitTypeName;
            else
            {
                lblVisitTypeName.Text = string.Empty;
                if (isResetIdIfNotExist)
                    txtVisitTypeID.Text = string.Empty;
            }
        }		
		
		
		
		#endregion		
    }
}
