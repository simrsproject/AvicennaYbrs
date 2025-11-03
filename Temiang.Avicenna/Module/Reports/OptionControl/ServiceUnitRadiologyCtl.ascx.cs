using System;
using System.Collections.Generic;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class ServiceUnitRadiologyCtl : BaseOptionCtl
    {
        #region ComboBox
        protected void cboServiceUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
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
                    query.IsActive == true,
                    query.SRServiceUnitGroup == "RAD"
                );

            query.es.Top = 20;
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            DataTable dtb = query.LoadDataTable();

            cboServiceUnitID.DataSource = dtb;
            cboServiceUnitID.DataBind();
        }
        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ServiceUnitItemDataBound(e);
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_ServiceUnitID", cboServiceUnitID.SelectedValue);

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
                return string.Format("To Service Unit : {0}", cboServiceUnitID.SelectedValue);
            }
        }
    }
}