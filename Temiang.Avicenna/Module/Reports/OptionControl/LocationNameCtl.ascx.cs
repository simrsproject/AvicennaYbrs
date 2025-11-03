using System;
using System.Collections.Generic;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class LocationNameCtl : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        #region ComboBox LocationID
        protected void cboLocationName_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.LocationItemsRequested((RadComboBox)sender, e.Text);
        }
        protected void cboLocationName_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.LocationItemDataBound(e);
        }
        
        #endregion
        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            var locId = string.IsNullOrWhiteSpace(cboLocationName.Text)? string.Empty:cboLocationName.SelectedValue;
            parameters.AddNew("p_LocationID", locId);

            //Retun List
            return parameters;
        }

        //public override string ParameterCaption
        //{
        //    get { return lblParameterCaption.Text; }
        //    set { lblParameterCaption.Text = value; }
        //}
        public override string ReportSubTitle
        {
            get
            {
                return string.Format("Location : {0}", cboLocationName.SelectedValue);
            }
        }

    }
}