using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ServiceUnitItemServiceDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            chkIsAllowEditByUserVerificated.Checked = true;
            chkIsVisible.Checked = true;
            trIdiCode.Visible = false;// !AppSession.Application.IsHisMode; --> pindah ke ItemIdiItemSmf

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                chkIsAllowEditByUserVerificated.Checked = true;
                chkIsVisible.Checked = true;
                return;
            }
            ViewState["IsNewRecord"] = false;
            ComboBox.PopulateWithOneItem(cboItemID, (String)DataBinder.Eval(DataItem, ServiceUnitItemServiceMetadata.ColumnNames.ItemID));

            var idiCode = (String)DataBinder.Eval(DataItem, ServiceUnitItemServiceMetadata.ColumnNames.IdiCode);
            if (!string.IsNullOrEmpty(idiCode))
            {
                var idi = new ItemIdiQuery();
                idi.Where(idi.IdiCode == idiCode);
                cboIdiCode.DataSource = idi.LoadDataTable();
                cboIdiCode.DataBind();
                cboIdiCode.SelectedValue = idiCode;
            }

            chkIsAllowEditByUserVerificated.Checked = (bool)DataBinder.Eval(DataItem, ServiceUnitItemServiceMetadata.ColumnNames.IsAllowEditByUserVerificated);
            chkIsVisible.Checked = (bool)DataBinder.Eval(DataItem, ServiceUnitItemServiceMetadata.ColumnNames.IsVisible);
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        private DataTable LoadItem(string searchText)
        {
            string searchTextContain = string.Format("%{0}%", searchText);
            var query = new ItemQuery("a");
            query.es.Top = 30;
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );
            query.Where(
                query.SRItemType.NotIn(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen), 
                query.IsActive == true,
                query.Or(
                    query.ItemName.Like(searchTextContain),
                    query.ItemID.Like(searchTextContain)
                    )
                );
            query.OrderBy(query.ItemName.Ascending);

            return query.LoadDataTable();
        }

        protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            DataTable tbl = LoadItem(e.Text);
            cboItemID.DataSource = tbl;
            cboItemID.DataBind();
        }

        protected void cboIdiCode_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["IdiName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["IdiCode"].ToString();
        }

        protected void cboIdiCode_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ItemIdiQuery("a");
            query.es.Top = 30;
            query.Select
                (
                    query.IdiCode,
                    query.IdiName
                );
            query.Where(
                query.Or(
                    query.IdiName.Like(searchTextContain),
                    query.IdiCode.Like(searchTextContain)
                    )
                );
            query.OrderBy(query.IdiName.Ascending);
            cboIdiCode.DataSource = query.LoadDataTable();
            cboIdiCode.DataBind();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            ServiceUnitItemServiceCollection coll;

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                coll = (ServiceUnitItemServiceCollection)Session["collServiceUnitItemService"];

                string id = cboItemID.SelectedValue;
                bool isExist = false;
                foreach (ServiceUnitItemService item in coll)
                {
                    if (item.ItemID.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item ID: {0} has exist", id);
                }
            }
        }

        #region Properties for return entry value

        public String ItemID
        {
            get { return cboItemID.SelectedValue; }
        }

        public String ItemName
        {
            get { return cboItemID.Text; }
        }

        public String IdiCode
        {
            get { return cboIdiCode.SelectedValue; }
        }

        public String IdiName
        {
            get { return cboIdiCode.Text; }
        }

        public String ChartOfAccountId
        {
            get { return  cboChartOfAccountId.SelectedValue; }
        }

        public String SubLedgerId
        {
            get { return cboSubledgerId.SelectedValue; }
        }

        public bool IsAllowEditByUserVerificated
        {
            get { return chkIsAllowEditByUserVerificated.Checked; }
        }

        public bool IsVisible
        {
            get { return chkIsVisible.Checked; }
        }
        #endregion

        #region Method & Event TextChanged

        //protected void txtItemServiceID_TextChanged(object sender, EventArgs e)
        //{
        //    PopulateItemServiceName(true);
        //}

        //private void PopulateItemServiceName(bool isResetIdIfNotExist)
        //{
        //    if (txtItemID.Text == string.Empty)
        //    {
        //        lblItemName.Text = string.Empty;
        //        return;
        //    }
        //    Item entity = new Item();
        //    if (entity.LoadByPrimaryKey(txtItemID.Text))
        //        lblItemName.Text = entity.ItemName;
        //    else
        //    {
        //        lblItemName.Text = string.Empty;
        //        if (isResetIdIfNotExist)
        //            txtItemID.Text = string.Empty;
        //    }

        //}

        protected void cboChartOfAccountId_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerId.Items.Clear();
            cboSubledgerId.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountId.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountId.Items.Clear();
                cboChartOfAccountId.Text = string.Empty;
                return;
            }
        }

        #endregion

        #region ComboBox ChartOfAccountId
        protected void cboChartOfAccountId_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountId.DataSource = dtb;
            cboChartOfAccountId.DataBind();
        }

        protected void cboChartOfAccountId_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        #endregion

        #region ComboBox SubledgerId
        protected void cboSubledgerId_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountId.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountId.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerId.DataSource = dtb;
            cboSubledgerId.DataBind();
        }

        protected void cboSubledgerId_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion      
    }
}
