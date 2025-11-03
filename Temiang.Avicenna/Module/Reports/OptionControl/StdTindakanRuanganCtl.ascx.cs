using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class StdTindakanRuanganCtl : BaseOptionCtl
    {

        #region ComboBox
        protected void cboTindakanRuangan_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.StandardReferenceItemsRequested((RadComboBox)sender, "RptTindakanRuangan", e.Text);
        }

        protected void cboTindakanRuangan_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.StandardReferenceItemDataBound(e);
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_ItemID", cboTindakanRuangan.SelectedValue);

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
                return string.Format("Tindakan Ruangan : {0}", cboTindakanRuangan.SelectedValue);
            }
        }
    }
}