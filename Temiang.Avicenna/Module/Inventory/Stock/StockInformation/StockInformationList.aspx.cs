using System;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Stock
{
    public partial class StockInformationList : BasePage
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

            ProgramID = AppConstant.Program.StockInformation;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);

                ComboBox.SelectedValue(cboSRItemType, BusinessObject.Reference.ItemType.Medical);

                chkHasPendingBalance.Visible= (!AppSession.Parameter.IsDistributionAutoConfirm);
                grdItemBalance.Columns.FindByUniqueName("Booking").Visible = (!AppSession.Parameter.IsDistributionAutoConfirm);

                grdItemBalance.Columns.FindByUniqueName("EdList").Visible = !AppSession.Parameter.IsEnabledStockWithEdControl;
                grdItemBalance.Columns.FindByUniqueName("NewEdList").Visible = AppSession.Parameter.IsEnabledStockWithEdControl;

                pnlApproachingExpiration.Visible = AppSession.Parameter.IsEnabledStockWithEdControl;
            }
        }

        protected void btnSearchItemID_Click(object sender, ImageClickEventArgs e)
        {
            grdItemBalance.Rebind();
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

        protected void grdItemBalance_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = ItemBalances;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;

        }

        protected void grdItemBalance_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;

            //Load record
            if (e.DetailTableView.Name.Equals("grdItemBalanceDetail"))
            {
                DataTable dtb;

                if (!AppSession.Parameter.IsEnabledStockWithEdControl)
                {
                    var balanceQ = new ItemBalanceDetailQuery("a");

                    balanceQ.Select(
                        balanceQ.BalanceDate,
                        balanceQ.ReferenceNo,
                        balanceQ.Balance
                    );
                    balanceQ.Where(balanceQ.ItemID == dataItem.GetDataKeyValue("ItemID").ToString(),
                                   balanceQ.LocationID == dataItem.GetDataKeyValue("LocationID").ToString(),
                                   balanceQ.Balance > 0);

                    balanceQ.OrderBy(balanceQ.BalanceDate.Ascending);

                    dtb = balanceQ.LoadDataTable();
                }
                else
                {
                    dtb = null;
                }
                    
                e.DetailTableView.DataSource = dtb;
            }
            else
            {
                DataTable dtb;

                if (AppSession.Parameter.IsEnabledStockWithEdControl)
                {
                    var balanceQ = new ItemBalanceDetailEdQuery("a");

                    balanceQ.Select(
                        balanceQ.ExpiredDate,
                        balanceQ.BatchNumber,
                        balanceQ.Balance
                    );
                    balanceQ.Where(balanceQ.ItemID == dataItem.GetDataKeyValue("ItemID").ToString(),
                                   balanceQ.LocationID == dataItem.GetDataKeyValue("LocationID").ToString(),
                                   balanceQ.Balance > 0);

                    balanceQ.OrderBy(balanceQ.ExpiredDate.Ascending, balanceQ.CreatedDateTime.Ascending);

                    dtb = balanceQ.LoadDataTable();
                }
                else
                {
                    dtb = null;
                }
                    
                e.DetailTableView.DataSource = dtb;
            }
        }

        private DataTable ItemBalances
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboSRItemType.SelectedValue) && string.IsNullOrEmpty(cboLocationID.SelectedValue) && chkBelowMinimum.Checked == false && chkOnlyInStock.Checked == false && chkHasPendingBalance.Checked == false && chkIsApproachingExpiration.Checked == false && txtExpiredDateFrom.IsEmpty && txtExpiredDateTo.IsEmpty && string.IsNullOrEmpty(cboItemGroupID.SelectedValue) && string.IsNullOrEmpty(txtItemName.Text) && string.IsNullOrEmpty(cboItemID.SelectedValue) && string.IsNullOrEmpty(cboZatActiveID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Stock Information")) return null;

                var balanceQ = new ItemBalanceQuery("a");
                var itemQ = new ItemQuery("b");
                balanceQ.InnerJoin(itemQ).On(balanceQ.ItemID == itemQ.ItemID);

                var locationQ = new LocationQuery("c");
                balanceQ.InnerJoin(locationQ).On(balanceQ.LocationID == locationQ.LocationID);

                var itemProductMedicQ = new ItemProductMedicQuery("d");
                balanceQ.LeftJoin(itemProductMedicQ).On(balanceQ.ItemID == itemProductMedicQ.ItemID);

                var itemProductNonMedicQ = new ItemProductNonMedicQuery("e");
                balanceQ.LeftJoin(itemProductNonMedicQ).On(balanceQ.ItemID == itemProductNonMedicQ.ItemID);

                var itemKitchenQ = new ItemKitchenQuery("f");
                balanceQ.LeftJoin(itemKitchenQ).On(balanceQ.ItemID == itemKitchenQ.ItemID);

                var asri = new AppStandardReferenceItemQuery("asri");
                balanceQ.LeftJoin(asri).On(balanceQ.SRItemBin == asri.ItemID && asri.StandardReferenceID == AppEnum.StandardReference.ItemBin.ToString());

                balanceQ.Where(itemQ.SRItemType == cboSRItemType.SelectedValue, itemQ.IsActive == true);

                // tambah filter akses ke service unit
                if (!this.IsUserCrossUnitAble)
                {
                    var su = new ServiceUnitQuery("su");
                    var qusr = new AppUserServiceUnitQuery("u");
                    var sul = new ServiceUnitLocationQuery("sul");
                    balanceQ.InnerJoin(sul).On(balanceQ.LocationID == sul.LocationID);
                    balanceQ.InnerJoin(su).On(sul.ServiceUnitID == su.ServiceUnitID);
                    balanceQ.InnerJoin(qusr).On(su.ServiceUnitID == qusr.ServiceUnitID);
                    balanceQ.Where(qusr.UserID == AppSession.UserLogin.UserID);
                }
                //

                if (cboLocationID.SelectedValue != string.Empty)
                    balanceQ.Where(balanceQ.LocationID == cboLocationID.SelectedValue);

                if (txtItemName.Text != string.Empty)
                {
                    string search = txtItemName.Text.Trim();
                    balanceQ.Where
                        (
                            balanceQ.Or
                            (
                                itemQ.ItemID.Like("%" + search + "%"),
                                itemQ.ItemName.Like("%" + search + "%")
                            )
                        );
                }

                balanceQ.Select
                (
                    balanceQ.LocationID,
                    balanceQ.ItemID,
                    itemQ.ItemName,
                    locationQ.LocationName,
                    balanceQ.Balance,
                    balanceQ.Booking,
                    balanceQ.Minimum,
                    balanceQ.Maximum,
                    asri.ItemName.As("ItemBin")
                );

                switch (cboSRItemType.SelectedValue)
                {
                    case BusinessObject.Reference.ItemType.Medical:
                        balanceQ.Select(itemProductMedicQ.SRItemUnit, itemProductMedicQ.IsControlExpired);
                        break;
                    case BusinessObject.Reference.ItemType.NonMedical:
                        balanceQ.Select(itemProductNonMedicQ.SRItemUnit, itemProductNonMedicQ.IsControlExpired);
                        break;
                    case BusinessObject.Reference.ItemType.Kitchen:
                        balanceQ.Select(itemKitchenQ.SRItemUnit, itemKitchenQ.IsControlExpired);
                        break;
                }

                if (!string.IsNullOrEmpty(cboItemGroupID.SelectedValue))
                    balanceQ.Where(itemQ.ItemGroupID == cboItemGroupID.SelectedValue);

                if (!string.IsNullOrEmpty(cboItemID.SelectedValue))
                    balanceQ.Where(itemQ.ItemID == cboItemID.SelectedValue);

                //Nurul - Tambah filter Generic ambil dari table Zat Active
                if (!string.IsNullOrEmpty(cboZatActiveID.SelectedValue))
                {
                    var itemProductMedicZatActiveQ = new ItemProductMedicZatActiveQuery("ipmza");
                    var zatActiveQ = new ZatActiveQuery("zaq");
                    balanceQ.LeftJoin(itemProductMedicZatActiveQ).On(balanceQ.ItemID == itemProductMedicZatActiveQ.ItemID)
                        .LeftJoin(zatActiveQ).On(zatActiveQ.ZatActiveID == itemProductMedicZatActiveQ.ZatActiveID);

                    balanceQ.Where(zatActiveQ.ZatActiveID == cboZatActiveID.SelectedValue);
                }

                if (chkBelowMinimum.Checked)
                    balanceQ.Where(balanceQ.Balance < balanceQ.Minimum);
                if (chkOnlyInStock.Checked)
                    balanceQ.Where(balanceQ.Balance > 0);
                if (chkHasPendingBalance.Checked)
                    balanceQ.Where(balanceQ.Booking > 0);

                bool isTopMaxRecord = string.IsNullOrEmpty(cboLocationID.SelectedValue);
                if (isTopMaxRecord && !string.IsNullOrEmpty(cboItemGroupID.SelectedValue))
                    isTopMaxRecord = false;
                if (isTopMaxRecord && !string.IsNullOrEmpty(txtItemName.Text))
                    isTopMaxRecord = false;
                if (isTopMaxRecord && !string.IsNullOrEmpty(cboItemID.SelectedValue))
                    isTopMaxRecord = false;

                if (chkIsApproachingExpiration.Checked && !txtExpiredDateFrom.IsEmpty && !txtExpiredDateTo.IsEmpty)
                {
                    var edQ = new ItemBalanceDetailEdQuery("ed");
                    balanceQ.InnerJoin(edQ).On(edQ.LocationID == balanceQ.LocationID && edQ.ItemID == balanceQ.ItemID);
                    balanceQ.Where(edQ.ExpiredDate >= txtExpiredDateFrom.SelectedDate && edQ.ExpiredDate <= txtExpiredDateTo.SelectedDate && edQ.Balance > 0);
                }

                if (isTopMaxRecord)
                    balanceQ.es.Top = AppSession.Parameter.MaxResultRecord;

                balanceQ.es.Distinct = true;
                balanceQ.OrderBy(itemQ.ItemName.Ascending);

                return balanceQ.LoadDataTable();
            }
        }

        protected void cboLocationID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new LocationQuery("l");
            query.es.Top = 10;
            query.Select
                (
                    query.LocationID,
                    query.LocationName
                );
            query.Where
                (
                    query.LocationName.Like(searchTextContain),
                    query.IsActive == true
                );

            // tambah filter akses ke service unit
            if (!this.IsUserCrossUnitAble)
            {
                var sul = new ServiceUnitLocationQuery("sul");
                var qusr = new AppUserServiceUnitQuery("u");
                query.InnerJoin(sul).On(query.LocationID == sul.LocationID);
                query.InnerJoin(qusr).On(sul.ServiceUnitID == qusr.ServiceUnitID);
                query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                query.es.Distinct = true;
            }
            //

            cboLocationID.DataSource = query.LoadDataTable();
            cboLocationID.DataBind();
        }

        protected void cboLocationID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["LocationName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["LocationID"].ToString();
        }

        protected void chkBelowMinimum_CheckedChanged(object sender, EventArgs e)
        {
            grdItemBalance.Rebind();
        }

        protected void cboSRItemType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboItemGroupID.Items.Clear();
            cboItemGroupID.Text = string.Empty;
            cboItemID.Items.Clear();
            cboItemID.Text = string.Empty;
            txtItemName.Text = string.Empty;
        }

        protected void cboItemGroupID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboItemID.Items.Clear();
            cboItemID.Text = string.Empty;
            txtItemName.Text = string.Empty;
        }

        protected void cboItemGroupID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ItemGroupItemsRequested((RadComboBox)sender, e.Text, cboSRItemType.SelectedValue);
        }
        protected void cboItemGroupID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ItemGroupItemDataBound(e);
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
                    query.SRItemType == cboSRItemType.SelectedValue) ;
            if (!string.IsNullOrEmpty(cboItemGroupID.SelectedValue))
                query.Where(query.ItemGroupID == cboItemGroupID.SelectedValue);
            query.OrderBy(query.ItemName.Ascending);

            var tbl = query.LoadDataTable();

            return tbl;
        }

        protected void cboZatActiveID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ZatActiveName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ZatActiveID"].ToString();
        }

        protected void cboZatActiveID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            DataTable tbl = LoadItem(e.Text);
            cboZatActiveID.DataSource = tbl;
            cboZatActiveID.DataBind();
        }

        private DataTable LoadItem(string searchText)
        {
            string searchTextContain = string.Format("%{0}%", searchText);
            var query = new ZatActiveQuery("a");
            query.es.Top = 20;
            query.Select
                (
                    query.ZatActiveID,
                    query.ZatActiveName
                );
            query.Where(
                query.IsActive == true,
                query.Or(
                    query.ZatActiveName.Like(searchTextContain),
                    query.ZatActiveID.Like(searchTextContain)
                    )
                );
            query.OrderBy(query.ZatActiveName.Ascending);

            return query.LoadDataTable();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            grdItemBalance.MasterTableView.ExportToExcel();
        }
    }
}
