using System;
using System.Collections.Generic;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class ProcedureNameCtl : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        #region ComboBox ProcedureID
        protected void cboProcedureName_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ProcedureItemsRequested((RadComboBox)sender, e.Text);
        }
        protected void cboProcedureName_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ProcedureItemDataBound(e);
        }
        
        #endregion
        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_ProcedureID", cboProcedureName.SelectedValue);

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
                return string.Format("Procedure : {0}", cboProcedureName.SelectedValue);
            }
        }

    }
}