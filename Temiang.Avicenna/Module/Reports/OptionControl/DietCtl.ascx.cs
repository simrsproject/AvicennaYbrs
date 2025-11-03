using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class DietCtl : BaseOptionCtl
    {
        #region ComboBox
        protected void cboDietID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.DietsRequested((RadComboBox)sender, e.Text, string.Empty, false);
        }

        protected void cboDietID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.DietItemDataBound(e);
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_DietID", cboDietID.SelectedValue);

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
                return string.Format("Diet : {0}", cboDietID.SelectedValue);
            }
        }
    }
}