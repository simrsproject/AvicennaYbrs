using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class InPatientTypeCtl : BaseOptionCtl
    {

        protected void Page_Init(object sender, EventArgs e)
        {

            //StandardReference Initialize
            if (!IsPostBack)
            {
                cboInPatientType.Items.Add(new RadComboBoxItem("All", "0"));
                cboInPatientType.Items.Add(new RadComboBoxItem("Sudah Pulang", "1"));
                cboInPatientType.Items.Add(new RadComboBoxItem("Masih Dirawat", "2"));
 
            }
        }
        
        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_InPatientType", cboInPatientType.SelectedValue);

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