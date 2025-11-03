using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class ClassCtl : BaseOptionCtl
    {

        #region ComboBox 
        protected void cboClass_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ClassItemsRequested((RadComboBox)sender, e.Text);
        }
        protected void cboClassID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ClassItemDataBound(e);
            
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_ClassID", cboClassID.SelectedValue);

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
                return string.Format("Class : {0}", cboClassID.SelectedValue);
            }
        }

    }
}