using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class AssetGroupCtl : BaseOptionCtl
    {
        #region ComboBox
        protected void cboAssetGroupID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GroupName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["AssetGroupId"].ToString();
        }

        protected void cboAssetGroupID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AssetGroupQuery("a");

            query.es.Top = 20;
            query.es.Distinct = true;

            query.Select(query.AssetGroupId, query.GroupName);
            query.Where(query.GroupName.Like(searchTextContain));

            query.OrderBy(query.GroupName.Ascending);
            DataTable dtb = query.LoadDataTable();
            cboAssetGroupID.DataSource = dtb;
            cboAssetGroupID.DataBind();
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_AssetGroupId", cboAssetGroupID.SelectedValue);

            //Retun List
            return parameters;
        }
    }
}