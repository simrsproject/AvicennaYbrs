using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class RegistrationTypeCtl2 : BaseOptionCtl
    {

        //#region ComboBox 
        //protected void cboRegistrationType_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        //{
        //    ComboBox.StandardReferenceItemsRequested((RadComboBox)sender, "RegistrationType",e.Text);
        //}
        //protected void cboRegistrationType_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        //{
        //    ComboBox.StandardReferenceItemDataBound(e);
        //}
        //#endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_RegistrationType", cboRegistrationType.SelectedValue);

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
                return string.Format("Registration Type : {0}", cboRegistrationType.SelectedValue);
            }
        }

    }
}