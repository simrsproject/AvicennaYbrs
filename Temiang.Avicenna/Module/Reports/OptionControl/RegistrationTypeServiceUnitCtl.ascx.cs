using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class RegistrationTypeServiceUnitCtl : BaseOptionCtl
    {
        #region ComboBox
        protected void cboRegistrationType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboServiceUnitID.Items.Clear();
            cboServiceUnitID.Text = string.Empty;
        }
        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ServiceUnitItemDataBound(e);
        }
        protected void cboServiceUnitID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ServiceUnitQuery("a");
            query.Where
                (
                query.Or
                        (
                            query.ServiceUnitID == e.Text,
                            query.ServiceUnitName.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );
            if (cboRegistrationType.SelectedValue == "IPR")
                query.Where(query.SRRegistrationType == "IPR");
            else if (cboRegistrationType.SelectedValue == "OP")
                query.Where(query.SRRegistrationType != "IPR", query.SRRegistrationType != string.Empty);

            query.es.Top = 20;
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            DataTable dtb = query.LoadDataTable();

            cboServiceUnitID.DataSource = dtb;
            cboServiceUnitID.DataBind();
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_RegistrationType", cboRegistrationType.SelectedValue);
            parameters.AddNew("p_ServiceUnitID", cboServiceUnitID.SelectedValue);

            //Retun List
            return parameters;
        }
    }
}