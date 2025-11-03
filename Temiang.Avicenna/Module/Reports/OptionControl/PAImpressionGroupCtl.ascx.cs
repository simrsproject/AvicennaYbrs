using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class PAImpressionGroupCtl : BaseOptionCtl
    {
        #region ComboBox
        protected void cboPAImpressionGroup_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ImpressionGroupItemsRequested((RadComboBox)sender, e.Text);
        }
        protected void cboPAImpressionGroup_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ImpressionGroupItemDataBound(e);
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_ImpressionGroupID", cboPAImpressionGroup.SelectedValue);

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
                return string.Format("Item : {0}", cboPAImpressionGroup.SelectedValue);
            }
        }
    }
}