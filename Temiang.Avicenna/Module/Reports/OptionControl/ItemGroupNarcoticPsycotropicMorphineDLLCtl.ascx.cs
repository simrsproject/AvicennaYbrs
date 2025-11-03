using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class ItemGroupNarcoticPsycotropicMorphineDLLCtl : BaseOptionCtl 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                rbtNarcoticPhsychotropicPrecusorOot.SelectedValue = "0";

        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_ItemGroupID", rbtNarcoticPhsychotropicPrecusorOot.SelectedValue);

            //Retun List
            return parameters;
        }

        public override string ParameterCaption
        {
            get { return lblCaption1.Text; }
            set { lblCaption1.Text = value; }
        }
        public override string ReportSubTitle
        {
            get
            {
                return string.Format("Item Group : {0} [{1}]", rbtNarcoticPhsychotropicPrecusorOot.Text, rbtNarcoticPhsychotropicPrecusorOot.SelectedValue); 
            }
        }
    }
}