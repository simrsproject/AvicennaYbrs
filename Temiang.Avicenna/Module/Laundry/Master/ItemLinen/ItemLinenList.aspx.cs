using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Laundry.Master
{
    public partial class ItemLinenList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.ItemLinen;
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

        protected void grdListItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdListItem.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = LaundryItems;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }           
        }

        private DataTable LaundryItems
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(txtItemName.Text) && string.IsNullOrEmpty(cboItemID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Laundry Items")) return null;

                var query = new ItemQuery("a");
                var ipnm = new ItemProductNonMedicQuery("b");
                query.InnerJoin(ipnm).On(ipnm.ItemID == query.ItemID && ipnm.IsNeedToBeLaundered == true);
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.Select(query.ItemID,
                                query.ItemName,
                                ipnm.Weight.Coalesce("0"),
                                query.Notes,
                                query.IsActive);

                if (txtItemName.Text != string.Empty)
                {
                    string search = txtItemName.Text.Trim();
                    query.Where
                        (
                            query.Or
                            (
                                query.ItemID.Like("%" + search + "%"),
                                query.ItemName.Like("%" + search + "%")
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

        protected void grdListBundle_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdListBundle.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = ItemLinens;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        private DataTable ItemLinens
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(txtBundleName.Text) && string.IsNullOrEmpty(cboBundleID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Linen Bundle")) return null;

                var query = new ItemLinenQuery("a");
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.Select(query.ItemID,
                                query.ItemName,
                                query.Notes,
                                query.IsActive);

                if (txtBundleName.Text != string.Empty)
                {
                    string search = txtBundleName.Text.Trim();
                    query.Where
                        (
                            query.Or
                            (
                                query.ItemID.Like("%" + search + "%"),
                                query.ItemName.Like("%" + search + "%")
                            )
                        );
                }

                if (!string.IsNullOrEmpty(cboBundleID.SelectedValue))
                    query.Where(query.ItemID == cboBundleID.SelectedValue);


                query.OrderBy(query.ItemID.Ascending);

                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdListItem.Rebind();
        }

        protected void btnSearch2_Click(object sender, ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdListBundle.Rebind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;

            if (eventArgument == "rebind")
            {
                grdListItem.Rebind();
            }
        }

        #region Combobox
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
            var ipnm = new ItemProductNonMedicQuery("b");
            query.InnerJoin(ipnm).On(ipnm.ItemID == query.ItemID);

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
                    ipnm.IsNeedToBeLaundered == true);
            query.OrderBy(query.ItemName.Ascending);

            var tbl = query.LoadDataTable();

            return tbl;
        }

        protected void cboBundleID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"] + " [" + ((DataRowView)e.Item.DataItem)["ItemID"] + "]";
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboBundleID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var tbl = PopulateBundle(e.Text);
            cboBundleID.DataSource = tbl.Rows.Count == 0 ? PopulateBundle(e.Text) : tbl;
            cboBundleID.DataBind();
        }

        private DataTable PopulateBundle(string parameter)
        {
            string searchTextContain = string.Format("%{0}%", parameter);
            var query = new ItemLinenQuery("a");

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
                        ));
            query.OrderBy(query.ItemName.Ascending);

            var tbl = query.LoadDataTable();

            return tbl;
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }
    }
}