using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Tariff
{
    public partial class UpdateDiscountAndVariableStatusList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;
        protected void Page_Init(object sender, EventArgs e)
        {
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.TariffUpdateDiscountAndVariableStatus;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithItemTypeService(cboSRItemType);
                ComboBox.SelectedValue(cboSRItemType, BusinessObject.Reference.ItemType.Service);
            }
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = ItemServices;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                grd.DataSource = dataSource;
            }
        }

        private DataTable ItemServices
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboItemGroup.SelectedValue) && string.IsNullOrEmpty(cboSRItemType.SelectedValue) &&
                   string.IsNullOrEmpty(txtItemName.Text) && string.IsNullOrEmpty(cboItemID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Item")) return null;

                var query = new ItemQuery("a");
                var qgroup = new ItemGroupQuery("b");
                
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.InnerJoin(qgroup).On(query.ItemGroupID == qgroup.ItemGroupID);
                query.Select
                    (
                        query.ItemID,
                        query.ItemName,
                        query.ItemGroupID,
                        qgroup.ItemGroupName
                    );
                query.Where(query.SRItemType == cboSRItemType.SelectedValue, query.IsActive == true);
                if (!string.IsNullOrEmpty(cboItemGroup.SelectedValue))
                    query.Where(query.ItemGroupID == cboItemGroup.SelectedValue);
                
                if (!(string.IsNullOrEmpty(txtItemName.Text)))
                {
                    var searchLike = "%" + txtItemName.Text.Trim() + "%";
                    query.Where(
                        query.Or(
                            query.ItemID.Like(searchLike),
                            query.ItemName.Like(searchLike)
                            )
                        );
                }
                if (!string.IsNullOrEmpty(cboItemID.SelectedValue))
                    query.Where(query.ItemID == cboItemID.SelectedValue);

                query.OrderBy(query.ItemID.Ascending);

                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (!(source is RadGrid))
                return;

            if (eventArgument == "rebind")
                grdList.Rebind();
        }

        protected void cboSRItemType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboItemGroup.Items.Clear();
            cboItemGroup.SelectedValue = string.Empty;
            cboItemGroup.Text = string.Empty;
            cboItemID.Items.Clear();
            cboItemID.SelectedValue = string.Empty;
            cboItemID.Text = string.Empty;
            txtItemName.Text = string.Empty;
        }

        protected void cboItemGroup_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ItemGroupItemsRequested((RadComboBox)sender, e.Text, cboSRItemType.SelectedValue);
        }
        protected void cboItemGroup_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ItemGroupItemDataBound(e);
        }
        protected void cboItemGroup_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboItemID.Items.Clear();
            cboItemID.SelectedValue = string.Empty;
            cboItemID.Text = string.Empty;
            txtItemName.Text = string.Empty;
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"] + " [" + ((DataRowView)e.Item.DataItem)["ItemID"] + "]";
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var tbl = PopulateItem(e.Text);
            cboItemID.DataSource = tbl.Rows.Count == 0 ? PopulateItem(e.Text) : tbl;
            cboItemID.DataBind();
        }

        private DataTable PopulateItem(string parameter)
        {
            string searchTextContain = string.Format("%{0}%", parameter);
            var query = new ItemQuery("a");

            query.es.Top = 20;
            query.es.Distinct = true;
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );

            query.Where(
                    query.Or(
                            query.ItemName.Like(searchTextContain),
                            query.ItemID.Like(searchTextContain)
                        ),
                    query.IsActive == true,
                    query.SRItemType == cboSRItemType.SelectedValue);
            if (!string.IsNullOrEmpty(cboItemGroup.SelectedValue))
                query.Where(query.ItemGroupID == cboItemGroup.SelectedValue);
            query.OrderBy(query.ItemName.Ascending);

            var tbl = query.LoadDataTable();

            return tbl;
        }
    }
}
