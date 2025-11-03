using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class DepartmentCtl : BaseOptionCtl
    {
        #region ComboBox 
        protected void cboDepartmentID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.DepartmentItemsRequested((RadComboBox)sender, e.Text);
        }
        protected void cboDepartmentID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.DepartmentItemDataBound(e);
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_DepartmentID", cboDepartmentID.SelectedValue);

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
                return string.Format("Departement : {0}", cboDepartmentID.SelectedValue);
            }
        }
    }
}