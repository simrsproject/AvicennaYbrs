using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.AssetManagement.Master
{
    public partial class CopyAssetList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.aid = '" + grdList.SelectedValue + "'";
        }

        private DataTable Assets
        {
            get
            {
                var query = new AssetQuery("a");
                var qgroup = new AssetGroupQuery("b");
                var qunit = new ServiceUnitQuery("c");
                var qroom = new ServiceRoomQuery("d");

                query.InnerJoin(qgroup).On(query.AssetGroupID == qgroup.AssetGroupId);
                query.InnerJoin(qunit).On(query.ServiceUnitID == qunit.ServiceUnitID);
                query.LeftJoin(qroom).On(query.AssetLocationID == qroom.RoomID);

                if (!string.IsNullOrEmpty(cboAssetGroup.SelectedValue))
                    query.Where(query.AssetGroupID == cboAssetGroup.SelectedValue);
                if (!string.IsNullOrEmpty(txtAssetName.Text))
                {
                    string searchTextContain = string.Format("%{0}%", txtAssetName.Text);
                    query.Where(query.AssetName.Like(searchTextContain));
                }
                if (!string.IsNullOrEmpty(txtPONumber.Text))
                {
                    string searchTextContain = string.Format("%{0}%", txtPONumber.Text);
                    query.Where(query.AssetName.Like(searchTextContain));
                }
                query.Select
                    (
                        query.AssetID,
                        query.AssetName,
                        qgroup.GroupName,
                        qunit.ServiceUnitName,
                        qroom.RoomName
                    );
                query.OrderBy(query.AssetID.Ascending);

                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Assets;
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void cboAssetGroup_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AssetGroupQuery();
            query.Select(query.AssetGroupId, query.GroupName);
            query.es.Top = 20;
            query.Where
                (
                    query.GroupName.Like(searchTextContain),
                    query.IsActive == true
                );
            query.OrderBy(query.AssetGroupId.Ascending);

            cboAssetGroup.DataSource = query.LoadDataTable();
            cboAssetGroup.DataBind();
        }

        protected void cboAssetGroup_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["AssetGroupId"] + " - " +((DataRowView)e.Item.DataItem)["GroupName"];
            e.Item.Value = ((DataRowView)e.Item.DataItem)["AssetGroupId"].ToString();
        }
    }
}
