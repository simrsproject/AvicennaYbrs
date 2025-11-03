using System;
using System.Collections.Generic;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class DiagnoseNameCtl : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        #region ComboBox DiagnoseID
        protected void cboDiagnoseName_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.DiagnoseItemsRequested((RadComboBox)sender, e.Text);
        }
        protected void cboDiagnoseName_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.DiagnoseItemDataBound(e);
        }
        
        #endregion
        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_DiagnoseID", cboDiagnoseName.SelectedValue);

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
                return string.Format("Diagnose : {0}", cboDiagnoseName.SelectedValue);
            }
        }

    }
}