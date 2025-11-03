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
    public partial class ProductAccountCtl : BaseOptionCtl
    {
        protected void cboProductAccountID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ProductAccountItemDataBound(e);
        }

        protected void cboProductAccountID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ProductAccountItemsRequested((RadComboBox)o, e.Text, string.Empty);
        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_ProductAccountID", cboProductAccountID.SelectedValue);

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
                return string.Format("Product Account : {0}", cboProductAccountID.SelectedValue);
            }
        }

    }
}