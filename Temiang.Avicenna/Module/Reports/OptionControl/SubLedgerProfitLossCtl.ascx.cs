using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class SubLedgerProfitLossCtl : BaseOptionCtl
    {
        #region ComboBox
        protected void cboSubLedger_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.SublegderItemsRequested((RadComboBox)sender, e.Text);
        }
        protected void cboSubLedger_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.SubledgerItemDataBound(e);
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_SubLUnit", cboSubledger.SelectedValue);
            
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
                return string.Format("Subledger Unit : {0}", cboSubledger.SelectedValue);
            }
        }
    }
}