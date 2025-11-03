using System;
using System.Collections.Generic;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class NursingDiagnoseNameCtl : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        #region ComboBox NursingDiagnoseID
        protected void cboNursingDiagnoseName_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.NursingDiagnoseItemsRequested((RadComboBox)sender, e.Text);
        }
        protected void cboNursingDiagnoseName_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.NursingDiagnoseItemDataBound(e);
        }
        
        #endregion
        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_NursingDiagnoseID", cboNursingDiagnoseName.SelectedValue);

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
                return string.Format("Nursing Diagnose : {0}", cboNursingDiagnoseName.SelectedValue);
            }
        }

    }
}