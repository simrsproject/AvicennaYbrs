using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class AssetByServiceUnitAndRoomCtl : BaseOptionCtl
    {
        #region ComboBox

        protected void cboServiceUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ServiceUnitItemsRequested((RadComboBox)sender, e.Text);
        }
        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ServiceUnitItemDataBound(e);
        }
        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboRoomID.Items.Clear();
            cboRoomID.Text = string.Empty;
            PopulateRoomList(cboServiceUnitID.SelectedValue);

            cboAssetID.Items.Clear();
            cboAssetID.SelectedValue = string.Empty;
            cboAssetID.Text = string.Empty;
        }

        protected void cboRoomID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboAssetID.Items.Clear();
            cboAssetID.SelectedValue = string.Empty;
            cboAssetID.Text = string.Empty;
        }

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
            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
            if (!string.IsNullOrEmpty(cboRoomID.SelectedValue))
                query.Where(query.AssetLocationID == cboRoomID.SelectedValue);

            query.OrderBy(query.AssetName.Ascending);
            DataTable dtb = query.LoadDataTable();
            cboAssetID.DataSource = dtb;
            cboAssetID.DataBind();
        }
        #endregion

        #region Populate List
        private void PopulateRoomList(string serviceUnitId)
        {
            if (serviceUnitId != string.Empty)
            {
                var sr = new ServiceRoomCollection();
                sr.Query.Where(sr.Query.ServiceUnitID == serviceUnitId);
                sr.Query.Where(sr.Query.IsActive == true);

                sr.LoadAll();

                cboRoomID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceRoom entity in sr)
                {
                    cboRoomID.Items.Add(new RadComboBoxItem(entity.RoomName, entity.RoomID));
                }
            }
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_ServiceUnitID", cboServiceUnitID.SelectedValue);
            parameters.AddNew("p_RoomID", cboRoomID.SelectedValue);
            parameters.AddNew("p_AssetID", cboAssetID.SelectedValue);

            //Retun List
            return parameters;
        }
    }
}