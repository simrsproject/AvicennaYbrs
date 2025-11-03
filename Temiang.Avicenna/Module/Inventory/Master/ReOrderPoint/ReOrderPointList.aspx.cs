using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ReOrderPointList : BasePage
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

            ProgramID = AppConstant.Program.ReOrderPoint;

            if (!IsPostBack)
            {
                var locs = new LocationCollection();
                var locq = new LocationQuery("a");
                var suq = new ServiceUnitLocationQuery("b");
                locq.InnerJoin(suq).On(locq.LocationID == suq.LocationID);

                if (!this.IsUserCrossUnitAble)
                {
                    var usr = new AppUserServiceUnitQuery("c");
                    locq.InnerJoin(usr).On(suq.ServiceUnitID == usr.ServiceUnitID &&
                                           usr.UserID == AppSession.UserLogin.UserID);
                }

                locq.Where(locq.IsActive == true);
                locq.Select(locq.LocationID, locq.LocationName);
                locq.es.Distinct = true;

                locs.Load(locq);
                
                cboLocationID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Location item in locs)
                {
                    cboLocationID.Items.Add(new RadComboBoxItem(item.LocationName, item.LocationID));
                }

                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
                ComboBox.SelectedValue(cboSRItemType, BusinessObject.Reference.ItemType.Medical);
                grdList.Columns.FindByUniqueName("Booking").Visible = !AppSession.Parameter.IsDistributionAutoConfirm;
            }
        }

        protected void cboLocationID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ComboBox.PopulateWithItemBinByLocation(cboSrItemBin, cboLocationID.SelectedValue);
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

            var dataSource = ItemBalances;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
        }

        private DataTable ItemBalances
        {
            get
            {
                var isEmptyFilter =  string.IsNullOrEmpty(cboLocationID.SelectedValue) && string.IsNullOrEmpty(cboSRItemType.SelectedValue) && chkZeroMinMax.Checked == false && string.IsNullOrEmpty(txtItemName.Text) && string.IsNullOrEmpty(cboItemID.SelectedValue) && string.IsNullOrEmpty(cboSrItemBin.SelectedValue) && chkEmptyItemBin.Checked == false;
                if (!ValidateSearch(isEmptyFilter, "Re-Order Point")) return null;

                var query = new ItemBalanceQuery("a");
                var item = new ItemQuery("b");
                var bin = new AppStandardReferenceItemQuery("s");
                
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        query.ItemID,
                        item.ItemName,
                        query.Minimum,
                        query.Maximum,
                        query.Balance,
                        query.Booking,
                        bin.ItemName.As("ItemBin"),
                        query.LocationID,
                        @"<CASE WHEN (a.Balance + a.Booking) = 0 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS 'IsAllowDelete'>"
                    );

                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.LeftJoin(bin).On(query.SRItemBin == bin.ItemID &
                                       bin.StandardReferenceID == AppEnum.StandardReference.ItemBin);
                
                if (cboSRItemType.SelectedValue == BusinessObject.Reference.ItemType.Medical)
                {
                    var m = new ItemProductMedicQuery("c");
                    query.InnerJoin(m).On(query.ItemID == m.ItemID);
                    query.Select(m.SRItemUnit);
                }
                else if (cboSRItemType.SelectedValue == BusinessObject.Reference.ItemType.NonMedical)
                {
                    var nm = new ItemProductNonMedicQuery("c");
                    query.InnerJoin(nm).On(query.ItemID == nm.ItemID);
                    query.Select(nm.SRItemUnit);
                }
                else if (cboSRItemType.SelectedValue == BusinessObject.Reference.ItemType.Kitchen)
                {
                    var k = new ItemKitchenQuery("c");
                    query.InnerJoin(k).On(query.ItemID == k.ItemID);
                    query.Select(k.SRItemUnit);
                }
                query.Where(query.LocationID == cboLocationID.SelectedValue,
                            item.SRItemType == cboSRItemType.SelectedValue);
                if (chkZeroMinMax.Checked)
                    query.Where(query.Minimum == 0, query.Maximum == 0);
                if (!(string.IsNullOrEmpty(txtItemName.Text)))
                {
                    var searchLike = "%" + txtItemName.Text.Trim() + "%";
                    query.Where(
                        item.Or(
                            item.ItemID.Like(searchLike),
                            item.ItemName.Like(searchLike)
                            )
                        );
                }
                if (!string.IsNullOrEmpty(cboItemID.SelectedValue))
                    query.Where(query.ItemID == cboItemID.SelectedValue);
                if (!string.IsNullOrEmpty(cboSrItemBin.SelectedValue)) {
                    query.Where(query.SRItemBin == cboSrItemBin.SelectedValue);
                }
                if (chkEmptyItemBin.Checked) {
                    query.Where(query.SRItemBin.Coalesce("''") == string.Empty);
                }

                query.OrderBy(query.ItemID.Ascending);

                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void chkZeroMinMax_CheckedChanged(object sender, EventArgs e)
        {
            grdList.Rebind();
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
            query.OrderBy(query.ItemName.Ascending);

            var tbl = query.LoadDataTable();

            return tbl;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (!(source is RadGrid))
                return;

            if (eventArgument == "rebind")
                grdList.Rebind();

            else if (eventArgument.Contains("delete"))
            {
                var par = eventArgument.Split('|')[1];
                var locId = par.Split('!')[0];
                var itemId = par.Split('!')[1];

                var balance = new ItemBalance();
                balance.LoadByPrimaryKey(locId, itemId);

                if (balance.Balance + balance.Booking <= 0)
                {
                    balance.MarkAsDeleted();

                    var balanceDetail = new ItemBalanceDetailCollection();
                    balanceDetail.Query.Where(balanceDetail.Query.LocationID == locId, balanceDetail.Query.ItemID == itemId);
                    balanceDetail.LoadAll();
                    balanceDetail.MarkAllAsDeleted();

                    using (esTransactionScope trans = new esTransactionScope())
                    {
                        balance.Save();
                        balanceDetail.Save();

                        //Commit if success, Rollback if failed
                        trans.Complete();
                    }

                    grdList.Rebind();
                }
            }
        }
    }
}
