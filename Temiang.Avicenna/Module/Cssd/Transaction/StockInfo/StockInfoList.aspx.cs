using System;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class StockInfoList : BasePage
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

            ProgramID = AppConstant.Program.CssdStockInformation;

            if (!IsPostBack)
            {
                var query = new ServiceUnitQuery("l");
                query.Where(query.ServiceUnitID == AppSession.Parameter.ServiceUnitCssdID);
                cboServiceUnitID.DataSource = query.LoadDataTable();
                cboServiceUnitID.DataBind();
                cboServiceUnitID.SelectedValue = AppSession.Parameter.ServiceUnitCssdID;
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
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdItemBalance.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = CssdItemBalances;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }            
        }

        private DataTable CssdItemBalances
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboItemID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Stock Information")) return null;

                var balanceQ = new CssdItemBalanceQuery("a");
                var itemQ = new ItemQuery("b");
                balanceQ.InnerJoin(itemQ).On(balanceQ.ItemID == itemQ.ItemID);

                var unitQ = new ServiceUnitQuery("c");
                balanceQ.InnerJoin(unitQ).On(balanceQ.ServiceUnitID == unitQ.ServiceUnitID);

                var itemProductQ = new VwItemProductMedicNonMedicQuery("d");
                balanceQ.LeftJoin(itemProductQ).On(balanceQ.ItemID == itemProductQ.ItemID);

                balanceQ.Where(itemQ.IsActive == true, itemQ.IsNeedToBeSterilized == true);

                var isTopMaxRecord = true;
                if (cboServiceUnitID.SelectedValue != string.Empty)
                {
                    balanceQ.Where(balanceQ.ServiceUnitID == cboServiceUnitID.SelectedValue);
                    isTopMaxRecord = false;
                }
                if (!string.IsNullOrEmpty(cboItemID.SelectedValue))
                {
                    balanceQ.Where(itemQ.ItemID == cboItemID.SelectedValue);
                    isTopMaxRecord = false;
                }
                    
                balanceQ.Select
                (
                    balanceQ.ServiceUnitID,
                    unitQ.ServiceUnitName,
                    balanceQ.ItemID,
                    itemQ.ItemName,
                    itemProductQ.SRItemUnit,
                    balanceQ.Balance,
                    balanceQ.BalanceReceived,
                    balanceQ.BalanceDeconImmersion,
                    balanceQ.BalanceDeconAbstersion,
                    balanceQ.BalanceDeconDrying,
                    balanceQ.BalanceFeasibilityTest,
                    balanceQ.BalancePackaging,
                    balanceQ.BalanceUltrasound,
                    balanceQ.BalanceSterilization,
                    balanceQ.BalanceDistribution,
                    balanceQ.BalanceReturned
                );

                if (isTopMaxRecord)
                    balanceQ.es.Top = AppSession.Parameter.MaxResultRecord;

                balanceQ.OrderBy(itemQ.ItemName.Ascending);

                return balanceQ.LoadDataTable();
            }
        }

        protected void cboServiceUnitID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ServiceUnitQuery("l");
            query.es.Top = 10;
            query.Select
                (
                    query.ServiceUnitID,
                    query.ServiceUnitName
                );
            query.Where
                (
                    query.ServiceUnitName.Like(searchTextContain),
                    query.IsActive == true
                );

            cboServiceUnitID.DataSource = query.LoadDataTable();
            cboServiceUnitID.DataBind();
        }

        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
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
                    query.IsNeedToBeSterilized == true);
            query.OrderBy(query.ItemName.Ascending);

            var tbl = query.LoadDataTable();

            return tbl;
        }
    }
}