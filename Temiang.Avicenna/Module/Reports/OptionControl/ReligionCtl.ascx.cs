using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;


namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class ReligionCtl : BaseOptionCtl
    {
        #region ComboBox
        protected void cboReligion_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.StandardReferenceItemsRequested((RadComboBox)sender, AppEnum.StandardReference.Religion.ToString(), e.Text);
        }

        protected void cboReligion_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.StandardReferenceItemDataBound(e);
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_Religion", cboReligion.SelectedValue);

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
                return string.Format("Religion : {0}", cboReligion.SelectedValue);
            }
        }
    }
}