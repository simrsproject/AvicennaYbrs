using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class AssetCtl : BaseOptionCtl
    {
        #region ComboBox
        protected void cboAssetID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["AssetName"] + " (" + ((DataRowView)e.Item.DataItem)["AssetID"] + ")";
            e.Item.Value = ((DataRowView)e.Item.DataItem)["AssetID"].ToString();
        }

        protected void cboAssetID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AssetQuery("a");
            var funitQ = new ServiceUnitQuery("b");
            var tunitQ = new ServiceUnitQuery("c");
            var usrQ = new AppUserServiceUnitQuery("d");

            query.es.Top = 20;

            query.Select(query.AssetID, query.AssetName, query.SerialNumber, funitQ.ServiceUnitName,
                         tunitQ.ServiceUnitName.As("MaintenanceServiceUnitName"));
            query.InnerJoin(funitQ).On(query.ServiceUnitID == funitQ.ServiceUnitID);
            query.InnerJoin(tunitQ).On(query.MaintenanceServiceUnitID == tunitQ.ServiceUnitID);
            query.InnerJoin(usrQ).On(query.MaintenanceServiceUnitID == usrQ.ServiceUnitID &&
                                    usrQ.UserID == AppSession.UserLogin.UserID);
            query.Where(query.Or(query.AssetName.Like(searchTextContain),
                                 query.SerialNumber.Like(searchTextContain)));

            query.OrderBy(query.AssetName.Ascending);
            DataTable dtb = query.LoadDataTable();
            cboAssetID.DataSource = dtb;
            cboAssetID.DataBind();
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_AssetID", cboAssetID.SelectedValue);

            //Retun List
            return parameters;
        }
    }
}