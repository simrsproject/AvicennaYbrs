using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class OKPatientTypeCtl : BaseOptionCtl
    {

        protected void Page_Init(object sender, EventArgs e)
        {

            //StandardReference Initialize
            if (!IsPostBack)
            {
                cboOKPatientType.Items.Add(new RadComboBoxItem("Semua", "0"));
                cboOKPatientType.Items.Add(new RadComboBoxItem("Rawat Inap", "1"));
                cboOKPatientType.Items.Add(new RadComboBoxItem("One Day Care", "2"));
 
            }
        }
        
        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_OKPatientType", cboOKPatientType.SelectedValue);

            //Retun List
            return parameters;
        }

        //public override string ParameterCaption
        //{
        //    get { return lblCaption.Text; }
        //    set { lblCaption.Text = value; }
        //}

    }
}