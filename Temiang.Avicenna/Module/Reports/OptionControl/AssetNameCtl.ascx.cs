using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class AssetNameCtl : BaseOptionCtl
    {
        #region ComboBox
        protected void cboAssetName_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["AssetName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["AssetName"].ToString();
        }

        protected void cboAssetName_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AssetQuery("a");
            
            query.es.Top = 20;
            query.es.Distinct = true;

            query.Select(query.AssetName);
            query.Where(query.AssetName.Like(searchTextContain));

            query.OrderBy(query.AssetName.Ascending);
            DataTable dtb = query.LoadDataTable();
            cboAssetName.DataSource = dtb;
            cboAssetName.DataBind();
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_AssetName", cboAssetName.SelectedValue);

            //Retun List
            return parameters;
        }
    }
}