//TODO: Field SRPurchaseUnit di table CustomerItem jangan dipakai, nanti dihapus

using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Linq;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class CustomerDetail : BasePageDetail
    {
        private void SetEntityValue(Customer entity)
        {
            entity.CustomerID = txtCustomerID.Text.ToUpper();
            entity.CustomerName = txtCustomerName.Text;         
            entity.ContactPerson = txtContactPerson.Text;          
            entity.IsActive = chkIsActive.Checked;           
            entity.StreetName = ctlAddress.StreetName;
            entity.ZipCode = ctlAddress.ZipCode;
            entity.District = ctlAddress.District;
            entity.County = ctlAddress.County;
            entity.City = ctlAddress.City;
            entity.State = ctlAddress.State;
            entity.PhoneNo = ctlAddress.PhoneNo;
            entity.FaxNo = ctlAddress.FaxNo;
            entity.MobilePhoneNo = ctlAddress.MobilePhoneNo;
            entity.Email = ctlAddress.Email;
            entity.SalesMarginPercentage =Convert.ToDecimal(txtSalesMarginPercentage.Value ?? 0);

            int chartOfAccountIdAR = 0;
            int subLedgerIdAR = 0;
            int.TryParse(cboChartOfAccountIdAR.SelectedValue, out chartOfAccountIdAR);
            int.TryParse(cboSubledgerIdAR.SelectedValue, out subLedgerIdAR);
            entity.ChartOfAccountIdAR = chartOfAccountIdAR;
            entity.SubledgerIdAR = subLedgerIdAR;

            int chartOfAccountIdARTemporary = 0;
            int subLedgerIdARTemporary = 0;
            int.TryParse(cboChartOfAccountIdARTemporary.SelectedValue, out chartOfAccountIdARTemporary);
            int.TryParse(cboSubledgerIdARTemporary.SelectedValue, out subLedgerIdARTemporary);
            entity.ChartOfAccountIdARTemporary = chartOfAccountIdARTemporary;
            entity.SubledgerIdARTemporary = subLedgerIdARTemporary;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //foreach (CustomerItem item in CustomerItems)
            //{
            //    item.CustomerID = txtCustomerID.Text.ToUpper();

            //    //Last Update Status
            //    if (item.es.IsAdded || item.es.IsModified)
            //    {
            //        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
            //        item.LastUpdateDateTime = DateTime.Now;
            //    }
            //}
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new CustomerQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.CustomerID > txtCustomerID.Text);
                que.OrderBy(que.CustomerID.Ascending);
            }
            else
            {
                que.Where(que.CustomerID < txtCustomerID.Text);
                que.OrderBy(que.CustomerID.Descending);
            }

            var entity = new Customer();
            entity.Load(que);

            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new Customer();
            if (parameters.Length > 0)
            {
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(parameters[0]);
            }
            else
                entity.LoadByPrimaryKey(txtCustomerID.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var customer = (Customer)entity;
            txtCustomerID.Text = customer.CustomerID;
            txtCustomerName.Text = customer.CustomerName;        
            txtContactPerson.Text = customer.ContactPerson;
            txtSalesMarginPercentage.Value = Convert.ToDouble(customer.SalesMarginPercentage ?? 0);
            chkIsActive.Checked = customer.IsActive ?? false;
            ctlAddress.StreetName = customer.StreetName;

            ZipCodeQuery zip = new ZipCodeQuery();
            zip.Where(zip.ZipCode == customer.str.ZipCode);

            ctlAddress.ZipCodeCombo.DataSource = zip.LoadDataTable();
            ctlAddress.ZipCodeCombo.DataBind();

            ctlAddress.District = customer.District;
            ctlAddress.County = customer.County;
            ctlAddress.City = customer.City;
            ctlAddress.State = customer.State;
            ctlAddress.PhoneNo = customer.PhoneNo;
            ctlAddress.FaxNo = customer.FaxNo;
            ctlAddress.MobilePhoneNo = customer.MobilePhoneNo;
            ctlAddress.Email = customer.Email;

            if (txtCustomerID.Text != string.Empty)
            {
                int coaAR = (customer.ChartOfAccountIdAR.HasValue ? customer.ChartOfAccountIdAR.Value : 0);
                int slAR = (customer.SubledgerIdAR.HasValue ? customer.SubledgerIdAR.Value : 0);
                int coaARTemporary = (customer.ChartOfAccountIdARTemporary.HasValue ? customer.ChartOfAccountIdARTemporary.Value : 0);
                int slARTemporary = (customer.SubledgerIdARTemporary.HasValue ? customer.SubledgerIdARTemporary.Value : 0);

                if (coaAR != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdAR, coaAR);
                    if (slAR != 0)
                        PopulateCboSubLedger(cboSubledgerIdAR, slAR);
                    else
                        ClearCombobox(cboSubledgerIdAR);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdAR);
                    ClearCombobox(cboSubledgerIdAR);
                }

                if (coaARTemporary != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdARTemporary, coaARTemporary);
                    if (slAR != 0)
                        PopulateCboSubLedger(cboSubledgerIdARTemporary, slAR);
                    else
                        ClearCombobox(cboSubledgerIdARTemporary);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdARTemporary);
                    ClearCombobox(cboSubledgerIdARTemporary);
                }
            }
            else
            {
                ClearCombobox(cboChartOfAccountIdAR);
                ClearCombobox(cboChartOfAccountIdARTemporary);

                cboChartOfAccountIdAR.Items.Clear();
                cboSubledgerIdAR.Items.Clear();
                cboChartOfAccountIdAR.Text = string.Empty;
                cboSubledgerIdAR.Text = string.Empty;
                cboChartOfAccountIdARTemporary.Items.Clear();
                cboSubledgerIdARTemporary.Items.Clear();
                cboChartOfAccountIdARTemporary.Text = string.Empty;
                cboSubledgerIdARTemporary.Text = string.Empty;
            }

           // PopulateGridDetail();
        }

        private void PopulateCboChartOfAccount(RadComboBox comboBox, int coaId)
        {
            ChartOfAccountsQuery coaQ = new ChartOfAccountsQuery();
            coaQ.Select(coaQ.ChartOfAccountId, coaQ.ChartOfAccountCode, coaQ.ChartOfAccountName);
            coaQ.Where(coaQ.ChartOfAccountId == coaId);
            DataTable dtbCoa = coaQ.LoadDataTable();
            comboBox.DataSource = dtbCoa;
            comboBox.DataBind();
            comboBox.SelectedValue = coaId.ToString();
        }

        private void PopulateCboSubLedger(RadComboBox comboBox, int subLedgerID)
        {
            SubLedgersQuery slQ = new SubLedgersQuery();
            slQ.Select(slQ.SubLedgerId, slQ.SubLedgerName, slQ.Description);
            slQ.Where(slQ.SubLedgerId == subLedgerID);
            DataTable dtbSl = slQ.LoadDataTable();
            comboBox.DataSource = dtbSl;
            comboBox.DataBind();
            comboBox.SelectedValue = subLedgerID.ToString();
        }

        private void ClearCombobox(RadComboBox comboBox)
        {
            comboBox.Items.Clear();
            comboBox.Text = string.Empty;
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Customer());

            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsCreateCustomerIdAutomatic) == "Yes")
                txtCustomerID.Text = GetCustomerId();

            chkIsActive.Checked = true;
            ctlAddress.ZipCodeCombo.Text = string.Empty;
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("CustomerID='{0}'", txtCustomerID.Text.Trim());
            auditLogFilter.TableName = "Customer";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtCustomerID.Enabled = (newVal == AppEnum.DataMode.New);
            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsCreateCustomerIdAutomatic) == "Yes")
                txtCustomerID.Enabled = false;

           // RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "CustomerSearch.aspx";
            UrlPageList = "CustomerList.aspx";

            WindowSearch.Height = 200;

            ProgramID = AppConstant.Program.CUSTOMER;

            if (!IsPostBack)
            {
                //Untuk detail entry dari grid, lakukan dgn cara 
                // 1. Register fungsi untuk memanggil popup window
                // 2. Set Button pd RadtextBox di control entry dgn fungsi PopUpSearch.InitializeOnButtonClick();
               // PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.ItemProductMedical, Page);

            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new Customer();
            if (entity.LoadByPrimaryKey(txtCustomerID.Text))
            {
                entity.MarkAsDeleted();
                using (var trans = new esTransactionScope())
                {
                  //  CustomerItems.Save();
                    entity.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            
            var entity = new Customer();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(Customer entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
               // CustomerItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
           
            var entity = new Customer();
            if (entity.LoadByPrimaryKey(txtCustomerID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion

        #region Record Detail Method Function

        //private CustomerItemCollection CustomerItems
        //{
        //    get
        //    {
        //        if (IsPostBack)
        //        {
        //            object obj = Session["collCustomerItem"];
        //            if (obj != null)
        //                return ((CustomerItemCollection)(obj));
        //        }

        //        var coll = new CustomerItemCollection();
        //        var query = new CustomerItemQuery("a");

        //        var iq = new ItemQuery("b");
        //        query.InnerJoin(iq).On(query.ItemID == iq.ItemID);

        //        ItemProductMedicQuery prodmedQ = new ItemProductMedicQuery("p");
        //        query.InnerJoin(prodmedQ).On(query.ItemID == prodmedQ.ItemID);

        //        query.Where(query.CustomerID == txtCustomerID.Text);

        //        query.Select
        //            (
        //                query.CustomerID,
        //                query.ItemID,
        //                iq.ItemName.As("refToItem_ItemName"),
        //                query.PurchaseDiscount1,
        //                query.PurchaseDiscount2,
        //                prodmedQ.SRPurchaseUnit,
        //                query.PriceInPurchaseUnit,
        //                query.IsActive,
        //                query.LastUpdateDateTime,
        //                query.LastUpdateByUserID
        //            );

        //        query.OrderBy(query.ItemID.Ascending);

        //        coll.Load(query);

        //        Session["collCustomerItem"] = coll;
        //        return coll;
        //    }
        //    set { Session["collCustomerItem"] = value; }
        //}

        //private void RefreshCommandItemGrid(DataMode oldVal, DataMode newVal)
        //{
        //    //Toogle grid command
        //    bool isVisible = (newVal != DataMode.Read);
        //    grdCustomerItem.Columns[0].Visible = isVisible;
        //    grdCustomerItem.Columns[grdCustomerItem.Columns.Count - 1].Visible = isVisible;

        //    grdCustomerItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

        //    //Reset Detail
        //    if (oldVal != DataMode.Read)
        //       // CustomerItems = null;

        //    //Perbaharui tampilan dan data
        //    grdCustomerItem.Rebind();
        //}

        //private void PopulateGridDetail()
        //{
        //    //Display Data Detail
        //   // CustomerItems = null; //Reset Record Detail
        //   // grdCustomerItem.DataSource = CustomerItems;
        //    grdCustomerItem.MasterTableView.IsItemInserted = false;
        //    grdCustomerItem.MasterTableView.ClearEditItems();
        //    grdCustomerItem.DataBind();
        //}

        //protected void grdCustomerItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        //{
        //    grdCustomerItem.DataSource = CustomerItems;
        //}

        //protected void grdCustomerItem_UpdateCommand(object source, GridCommandEventArgs e)
        //{
        //    var editedItem = e.Item as GridEditableItem;
        //    String itemID =
        //        Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][CustomerItemMetadata.ColumnNames.ItemID]);
        //    var entity = FindItemGrid(itemID);
        //    if (entity != null)
        //        SetEntityValue(entity, e);
        //}

        //protected void grdCustomerItem_DeleteCommand(object source, GridCommandEventArgs e)
        //{
        //    var item = e.Item as GridDataItem;
        //    String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][CustomerItemMetadata.ColumnNames.ItemID]);
        //    var entity = FindItemGrid(itemID);
        //    if (entity != null)
        //        entity.MarkAsDeleted();
        //}

        //protected void grdCustomerItem_InsertCommand(object source, GridCommandEventArgs e)
        //{
        //    var entity = CustomerItems.AddNew();
        //    SetEntityValue(entity, e);

        //    e.Canceled = true;
        //    grdCustomerItem.Rebind();
        //}

        //private void SetEntityValue(CustomerItem entity, GridCommandEventArgs e)
        //{
        //    var userControl = (CustomerItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
        //    if (userControl != null)
        //    {
        //        entity.ItemID = userControl.ItemID;
        //        entity.ItemName = userControl.ItemName;
        //        entity.PurchaseDiscount1 = userControl.PurchaseDiscount1;
        //        entity.PurchaseDiscount2 = userControl.PurchaseDiscount2;
        //        entity.SRPurchaseUnit = userControl.SRPurchaseUnit;
        //        entity.PriceInPurchaseUnit = userControl.PriceInPurchaseUnit;
        //        entity.IsActive = userControl.IsActive;
        //    }
        //}

        //private CustomerItem FindItemGrid(string itemID)
        //{
        //    var coll = CustomerItems;
        //    CustomerItem retval = null;
        //    foreach (CustomerItem rec in coll)
        //    {
        //        if (rec.ItemID.Equals(itemID))
        //        {
        //            retval = rec;
        //            break;
        //        }
        //    }
        //    return retval;
        //}

        #endregion

        #region Combobox

        protected void cboChartOfAccountIdAR_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
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
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdAR.DataSource = dtb;
            cboChartOfAccountIdAR.DataBind();
        }

        protected void cboChartOfAccountIdAR_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        protected void cboChartOfAccountIdAR_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdAR.Items.Clear();
            cboSubledgerIdAR.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdAR.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdAR.Items.Clear();
                cboChartOfAccountIdAR.Text = string.Empty;
                return;
            }
        }

        protected void cboSubledgerIdAR_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdAR.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdAR.SelectedValue));
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
            cboSubledgerIdAR.DataSource = dtb;
            cboSubledgerIdAR.DataBind();
        }

        protected void cboSubledgerIdAR_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }

        protected void cboChartOfAccountIdARTemporary_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
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
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdARTemporary.DataSource = dtb;
            cboChartOfAccountIdARTemporary.DataBind();
        }

        protected void cboChartOfAccountIdARTemporary_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        protected void cboChartOfAccountIdARTemporary_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdARTemporary.Items.Clear();
            cboSubledgerIdARTemporary.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdARTemporary.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdARTemporary.Items.Clear();
                cboChartOfAccountIdARTemporary.Text = string.Empty;
                return;
            }
        }

        protected void cboSubledgerIdARTemporary_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdARTemporary.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdARTemporary.SelectedValue));
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
            cboSubledgerIdARTemporary.DataSource = dtb;
            cboSubledgerIdARTemporary.DataBind();
        }

        protected void cboSubledgerIdARTemporary_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }

        #endregion

        private string GetCustomerId()
        {
            var query = new CustomerQuery();
            query.es.Top = 1;
            query.Select(query.CustomerID);
            query.OrderBy(query.CustomerID.Descending);

            var supp = new Customer();
            supp.Load(query);

            string suppId;
            if (supp.CustomerID != null)
            {
                var previx = supp.CustomerID.Where(m => !char.IsNumber(m)).Aggregate(string.Empty, (current, m) => current + (m));
                var number = supp.CustomerID.Where(m => char.IsNumber(m)).Aggregate(string.Empty, (current, m) => current + (m));
                int x = (int.Parse(number) + 1);
                if (x < 10)
                    suppId = "000" + x.ToString();
                else
                {
                    if (x < 100)
                        suppId = "00" + x.ToString();
                    else
                    {
                        if (x < 1000)
                            suppId = "0" + x.ToString();
                        else
                            suppId = x.ToString();
                    }
                }

                if (suppId.Length > number.Length)
                    suppId = suppId.Remove(0, suppId.Length - number.Length);

                suppId = previx + suppId;
            }
            else
                suppId = "C0001";
            return suppId;
        }
    }
}