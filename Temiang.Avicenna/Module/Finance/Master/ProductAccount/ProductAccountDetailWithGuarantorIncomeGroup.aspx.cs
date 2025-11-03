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
    public partial class ProductAccountDetailWithGuarantorIncomeGroup : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ProductAccountSearch.aspx";
            UrlPageList = "ProductAccountList.aspx";
			
			ProgramID = AppConstant.Program.PRODUCTACCOUNT ; //TODO: Isi ProgramID

			//StandardReference Initialize
			if (!IsPostBack)
            {
                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
            }
			
			//PopUp Search
			if (!IsCallback)
			{
				
			}
        }

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
            //OnDataModeChanged(DataModeCurrent, DataModeCurrent);
            if (IsPostBack)
            {
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ProductAccount());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            ProductAccount entity = new ProductAccount();
            if (entity.LoadByPrimaryKey(txtProductAccountID.Text))
            {
                var pagColl = new ProductAccountGuarantorGroupCollection();
                pagColl.Query.Where(pagColl.Query.ProductAccountID == entity.ProductAccountID);
                pagColl.LoadAll();

                entity.MarkAsDeleted();
                pagColl.MarkAllAsDeleted();
                
                SaveEntity(entity, pagColl);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            ProductAccount entity = new ProductAccount();
            if (entity.LoadByPrimaryKey(txtProductAccountID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new ProductAccount();
            entity.AddNew();
            var pagColl = new ProductAccountGuarantorGroupCollection();
            SetEntityValue(entity, pagColl);
            SaveEntity(entity, pagColl);

            gridMapping.Rebind();
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            ProductAccount entity = new ProductAccount();
            if (entity.LoadByPrimaryKey(txtProductAccountID.Text))
            {
                var pagColl = new ProductAccountGuarantorGroupCollection();
                pagColl.Query.Where(pagColl.Query.ProductAccountID == entity.ProductAccountID);
                pagColl.LoadAll();

                SetEntityValue(entity, pagColl);
                SaveEntity(entity, pagColl);

                gridMapping.Rebind();
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
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
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("ProductAccountID='{0}'", txtProductAccountID.Text.Trim());
            auditLogFilter.TableName = "ProductAccount";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtProductAccountID.Enabled = (newVal == AppEnum.DataMode.New);
            SetControlEnabled(newVal == AppEnum.DataMode.Edit);
        }

        private void SetControlEnabled(bool Enabled)
        {
            foreach (GridDataItem r in gridMapping.MasterTableView.Items)
            {
                (r.FindControl("cboCOAIncome") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboSlIncome") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboCOAIncomeIPR") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboSlIncomeIPR") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboCOAIncomeIGD") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboSlIncomeIGD") as RadComboBox).Enabled = Enabled;

                (r.FindControl("cboCOAAcrual") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboSlAcrual") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboCOAAcrualIPR") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboSlAcrualIPR") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboCOAAcrualIGD") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboSlAcrualIGD") as RadComboBox).Enabled = Enabled;

                (r.FindControl("cboCOADiscount") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboSlDiscount") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboCOADiscountIPR") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboSlDiscountIPR") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboCOADiscountIGD") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboSlDiscountIGD") as RadComboBox).Enabled = Enabled;

                (r.FindControl("cboCOAInventory") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboSlInventory") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboCOAInventoryIPR") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboSlInventoryIPR") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboCOAInventoryIGD") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboSlInventoryIGD") as RadComboBox).Enabled = Enabled;

                (r.FindControl("cboCOACOGS") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboSlCOGS") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboCOACOGSIPR") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboSlCOGSIPR") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboCOACOGSIGD") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboSlCOGSIGD") as RadComboBox).Enabled = Enabled;

                (r.FindControl("cboCOACOGSOPRTemp") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboSlCOGSOPRTemp") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboCOACOGSIPRTemp") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboSlCOGSIPRTemp") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboCOACOGSIGDTemp") as RadComboBox).Enabled = Enabled;
                (r.FindControl("cboSlCOGSIGDTemp") as RadComboBox).Enabled = Enabled;

            }
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            ProductAccount entity = new ProductAccount();
            if (parameters.Length > 0)
            {
                String productAccountID = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(productAccountID);
            }
            else
            {
                entity.LoadByPrimaryKey(txtProductAccountID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            ProductAccount productAccount = (ProductAccount)entity;
            txtProductAccountID.Text = productAccount.ProductAccountID;
            txtProductAccountName.Text = productAccount.ProductAccountName;
            cboSRItemType.SelectedValue = productAccount.SRItemType;
            chkIsActive.Checked = productAccount.IsActive ?? false;
            chkIsPpnOpr.Checked = productAccount.IsPpnOpr ?? false;
            chkIsPpnEmr.Checked = productAccount.IsPpnEmr ?? false;

            if (txtProductAccountID.Text != string.Empty)
            {
            }
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


        #endregion

        #region Private Method Standard

        class CoaSl {
            public int COAID;
            public int SlID;
        }

        private CoaSl GetCOASl(GridDataItem gdi, string cboIdentifier)
        { 
            var cs = new CoaSl();
            cs.COAID = 0;
            cs.SlID = 0;
            int.TryParse((gdi.FindControl("cboCOA" + cboIdentifier) as RadComboBox).SelectedValue, out cs.COAID);
            int.TryParse((gdi.FindControl("cboSl" + cboIdentifier) as RadComboBox).SelectedValue, out cs.SlID);
            return cs;
        }

        private void SetEntityValue(ProductAccount entity, ProductAccountGuarantorGroupCollection pagColl)
        {
            entity.ProductAccountID = txtProductAccountID.Text;
            entity.ProductAccountName = txtProductAccountName.Text;
            entity.SRItemType = cboSRItemType.SelectedValue;
            entity.IsActive = chkIsActive.Checked;
            entity.IsPpnOpr = chkIsPpnOpr.Checked;
            entity.IsPpnEmr = chkIsPpnEmr.Checked;

            //// OUT PATIENT
            //int chartOfAccountIdIncome = 0;
            //int subLegderIdIncome = 0;
            //int.TryParse(cboChartOfAccountIdIncome.SelectedValue, out chartOfAccountIdIncome);
            //int.TryParse(cboSubledgerIdIncome.SelectedValue, out subLegderIdIncome);
            //entity.ChartOfAccountIdIncome = chartOfAccountIdIncome;
            //entity.SubledgerIdIncome = subLegderIdIncome;

            foreach (GridDataItem gdi in gridMapping.MasterTableView.Items)
            {
                var pags = pagColl.Where(x => x.SRGuarantorIncomeGroup == gdi.GetDataKeyValue("ItemID").ToString());
                ProductAccountGuarantorGroup pag;
                if (pags.Count() == 0)
                {
                    pag = pagColl.AddNew();
                    pag.ProductAccountID = entity.ProductAccountID;
                    pag.SRGuarantorIncomeGroup = gdi.GetDataKeyValue("ItemID").ToString();
                }
                else {
                    pag = pags.First();
                }

                var cs = GetCOASl(gdi, "Income");
                pag.ChartOfAccountIdIncome = cs.COAID;
                pag.SubledgerIdIncome = cs.SlID;
                cs = GetCOASl(gdi, "IncomeIPR");
                pag.ChartOfAccountIdIncomeIP = cs.COAID;
                pag.SubledgerIdIncomeIP = cs.SlID;
                cs = GetCOASl(gdi, "IncomeIGD");
                pag.ChartOfAccountIdIncomeIGD = cs.COAID;
                pag.SubledgerIdIncomeIGD = cs.SlID;

                cs = GetCOASl(gdi, "Acrual");
                pag.ChartOfAccountIdAcrual = cs.COAID;
                pag.SubledgerIdAcrual = cs.SlID;
                cs = GetCOASl(gdi, "AcrualIPR");
                pag.ChartOfAccountIdAcrualIP = cs.COAID;
                pag.SubledgerIdAcrualIP = cs.SlID;
                cs = GetCOASl(gdi, "AcrualIGD");
                pag.ChartOfAccountIdAcrualIGD = cs.COAID;
                pag.SubledgerIdAcrualIGD = cs.SlID;

                cs = GetCOASl(gdi, "Discount");
                pag.ChartOfAccountIdDiscount = cs.COAID;
                pag.SubledgerIdDiscount = cs.SlID;
                cs = GetCOASl(gdi, "DiscountIPR");
                pag.ChartOfAccountIdDiscountIP = cs.COAID;
                pag.SubledgerIdDiscountIP = cs.SlID;
                cs = GetCOASl(gdi, "DiscountIGD");
                pag.ChartOfAccountIdDiscountIGD = cs.COAID;
                pag.SubledgerIdDiscountIGD = cs.SlID;

                cs = GetCOASl(gdi, "Inventory");
                pag.ChartOfAccountIdInventory = cs.COAID;
                pag.SubledgerIdInventory = cs.SlID;
                cs = GetCOASl(gdi, "InventoryIPR");
                pag.ChartOfAccountIdInventoryIP = cs.COAID;
                pag.SubledgerIdInventoryIP = cs.SlID;
                cs = GetCOASl(gdi, "InventoryIGD");
                pag.ChartOfAccountIdInventoryIGD = cs.COAID;
                pag.SubledgerIdInventoryIGD = cs.SlID;

                cs = GetCOASl(gdi, "COGS");
                pag.ChartOfAccountIdCOGS = cs.COAID;
                pag.SubledgerIdCOGS = cs.SlID;
                cs = GetCOASl(gdi, "COGSIPR");
                pag.ChartOfAccountIdCOGSIP = cs.COAID;
                pag.SubledgerIdCOGSIP = cs.SlID;
                cs = GetCOASl(gdi, "COGSIGD");
                pag.ChartOfAccountIdCOGSIGD = cs.COAID;
                pag.SubledgerIdCOGSIGD = cs.SlID;

                cs = GetCOASl(gdi, "COGSOPRTemp");
                pag.ChartOfAccountIdCOGSOPTemp = cs.COAID;
                pag.SubledgerIdCOGSOPTemp = cs.SlID;
                cs = GetCOASl(gdi, "COGSIPRTemp");
                pag.ChartOfAccountIdCOGSIPTemp = cs.COAID;
                pag.SubledgerIdCOGSIPTemp = cs.SlID;
                cs = GetCOASl(gdi, "COGSIGDTemp");
                pag.ChartOfAccountIdCOGSIGDTemp = cs.COAID;
                pag.SubledgerIdCOGSIGDTemp = cs.SlID;
            }
            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(ProductAccount entity, ProductAccountGuarantorGroupCollection pagColl)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                pagColl.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            ProductAccountQuery que = new ProductAccountQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ProductAccountID > txtProductAccountID.Text);
                que.OrderBy(que.ProductAccountID.Ascending);
            }
            else
            {
                que.Where(que.ProductAccountID < txtProductAccountID.Text);
                que.OrderBy(que.ProductAccountID.Descending);
            }
            ProductAccount entity = new ProductAccount();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged COA & SL
        protected void cboCOA_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var cboCoa = (RadComboBox)o;
            var cboSlName = cboCoa.UniqueID.Replace("COA", "Sl");
            var cblSL = FindControl(cboSlName) as RadComboBox;

            cblSL.Items.Clear();
            cblSL.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboCoa.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboCoa.Items.Clear();
                cboCoa.Text = string.Empty;
                return;
            }
        }

        protected void cboCOA_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        protected void cboCOA_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var cboCOA = sender as RadComboBox;
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
            cboCOA.DataSource = dtb;
            cboCOA.DataBind();
        }

        protected void cboSl_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }

        protected void cboSl_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            var cboSl = (RadComboBox)sender;
            var cboCOAName = cboSl.UniqueID.Replace("Sl", "COA");
            var cblCOA = FindControl(cboCOAName) as RadComboBox;

            int groupID;
            if (cblCOA.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cblCOA.SelectedValue));
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
            cboSl.DataSource = dtb;
            cboSl.DataBind();
        }
        #endregion

        protected void gridMapping_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            gridMapping.DataSource = PAG();
        }

        protected void gridMapping_DataBound(object o, EventArgs e) {
            SetControlEnabled(DataModeCurrent == AppEnum.DataMode.Edit);
        }

        protected void gridMapping_ItemDataBound(object o, GridItemEventArgs e) {
            if (e.Item is GridDataItem)
            {
                var gdi = e.Item as GridDataItem;
                var data = gdi.DataItem as AppStandardReferenceItem;

                var pag = new ProductAccountGuarantorGroup();
                pag.Query.Where(pag.Query.ProductAccountID == txtProductAccountID.Text,
                    pag.Query.SRGuarantorIncomeGroup == data.ItemID);
                if (pag.Load(pag.Query))
                {
                    SetVal(gdi, pag.ChartOfAccountIdIncome, pag.SubledgerIdIncome, "Income");
                    SetVal(gdi, pag.ChartOfAccountIdIncomeIP, pag.SubledgerIdIncomeIP, "IncomeIPR");
                    SetVal(gdi, pag.ChartOfAccountIdIncomeIGD, pag.SubledgerIdIncomeIGD, "IncomeIGD");

                    SetVal(gdi, pag.ChartOfAccountIdAcrual, pag.SubledgerIdAcrual, "Acrual");
                    SetVal(gdi, pag.ChartOfAccountIdAcrualIP, pag.SubledgerIdAcrualIP, "AcrualIPR");
                    SetVal(gdi, pag.ChartOfAccountIdAcrualIGD, pag.SubledgerIdAcrualIGD, "AcrualIGD");

                    SetVal(gdi, pag.ChartOfAccountIdDiscount, pag.SubledgerIdDiscount, "Discount");
                    SetVal(gdi, pag.ChartOfAccountIdDiscountIP, pag.SubledgerIdDiscountIP, "DiscountIPR");
                    SetVal(gdi, pag.ChartOfAccountIdDiscountIGD, pag.SubledgerIdDiscountIGD, "DiscountIGD");

                    SetVal(gdi, pag.ChartOfAccountIdInventory, pag.SubledgerIdInventory, "Inventory");
                    SetVal(gdi, pag.ChartOfAccountIdInventoryIP, pag.SubledgerIdInventoryIP, "InventoryIPR");
                    SetVal(gdi, pag.ChartOfAccountIdInventoryIGD, pag.SubledgerIdInventoryIGD, "InventoryIGD");

                    SetVal(gdi, pag.ChartOfAccountIdCOGS, pag.SubledgerIdCOGS, "COGS");
                    SetVal(gdi, pag.ChartOfAccountIdCOGSIP, pag.SubledgerIdCOGSIP, "COGSIPR");
                    SetVal(gdi, pag.ChartOfAccountIdCOGSIGD, pag.SubledgerIdCOGSIGD, "COGSIGD");

                    SetVal(gdi, pag.ChartOfAccountIdCOGSOPTemp, pag.SubledgerIdCOGSOPTemp, "COGSOPRTemp");
                    SetVal(gdi, pag.ChartOfAccountIdCOGSIPTemp, pag.SubledgerIdCOGSIPTemp, "COGSIPRTemp");
                    SetVal(gdi, pag.ChartOfAccountIdCOGSIGDTemp, pag.SubledgerIdCOGSIGDTemp, "COGSIGDTemp");
                }

                var trCOA = gdi.FindControl("trChartOfAccountIdCOGSTemp") as System.Web.UI.HtmlControls.HtmlTableRow;
                trCOA.Visible = (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No");

                var trSl = gdi.FindControl("trSubledgerIdCOGSTemp") as System.Web.UI.HtmlControls.HtmlTableRow;
                trSl.Visible = (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No");
                
            
            }
        }

        private void SetVal(GridDataItem gdi, int? COAID, int? SlID, string cboIdentifier)
        {
            if ((COAID ?? 0) != 0)
            {
                var coa = new ChartOfAccounts();
                if (coa.LoadByPrimaryKey(COAID.Value))
                {
                    var cboCOA = gdi.FindControl("cboCOA" + cboIdentifier) as RadComboBox;
                    cboCOA_ItemsRequested(cboCOA, new RadComboBoxItemsRequestedEventArgs() { Text = coa.ChartOfAccountCode });
                    cboCOA.SelectedValue = coa.ChartOfAccountId.ToString();
                }
            }
            if ((SlID ?? 0) != 0)
            {
                var sl = new SubLedgers();
                if (sl.LoadByPrimaryKey(SlID.Value))
                {
                    var cboSl = gdi.FindControl("cboSl" + cboIdentifier) as RadComboBox;
                    cboSl_ItemsRequested(cboSl, new RadComboBoxItemsRequestedEventArgs() { Text = sl.Description });
                    var cboItem = cboSl.Items.FindItemByValue(sl.SubLedgerId.ToString());
                    if (cboItem != null) cboItem.Selected = true;
                    //cboSl.SelectedValue = sl.SubLedgerId.ToString();
                }
            }
        }

        private AppStandardReferenceItemCollection PAG()
        {
            var gg = new AppStandardReferenceItemCollection();
            gg.Query.Where(gg.Query.StandardReferenceID == "GuarantorIncomeGroup");
            gg.LoadAll();
            return gg;
        }
    }
}
