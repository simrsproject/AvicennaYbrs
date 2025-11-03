using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class PatientTypeCtl : BaseOptionCtl
    {

        protected void Page_Init(object sender, EventArgs e)
        {

            //StandardReference Initialize
            if (!IsPostBack)
            {
                cboPatientType.Items.Add(new RadComboBoxItem("Umum", "0"));
                cboPatientType.Items.Add(new RadComboBoxItem("Jaminan", "1"));
                //ComboBox.SelectedValue(cboPatientType, BusinessObject.Reference.ItemType.Medical);
            }
        }
        
        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_IsGeneralPatient", cboPatientType.SelectedValue);

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