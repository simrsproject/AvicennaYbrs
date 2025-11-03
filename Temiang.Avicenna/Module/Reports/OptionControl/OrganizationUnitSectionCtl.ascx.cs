using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;


namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class OrganizationUnitSectionCtl : BaseOptionCtl
    {
        #region ComboBox
        protected void cboOrganizationUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.OrganizationUnitSectionItemsRequested((RadComboBox)sender, e.Text);
        }
        protected void cboOrganizationUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.OrganizationUnitItemDataBound(e);
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_OrganizationUnitID", string.IsNullOrEmpty(cboOrganizationUnitID.SelectedValue) ? "-1" : cboOrganizationUnitID.SelectedValue);

            //Retun List
            return parameters;
        }

        public override string ParameterCaption
        {
            get { return lblCaption.Text; }
            set { lblCaption.Text = value; }
        }
        public override string ReportSubTitle
        {
            get
            {
                return string.Format("Organization Unit : {0}", cboOrganizationUnitID.SelectedValue);
            }
        }
    }
}