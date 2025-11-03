using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.AssetManagement.Management
{
    public partial class AssetAuctionDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "##";
            UrlPageList = "AssetAuctionList.aspx";

            ProgramID = AppConstant.Program.AssetAuction;

            if (!IsPostBack)
            {
                var ptColl = new PaymentTypeCollection();
                ptColl.Query.Where(ptColl.Query.IsAssetAuctionPayment == true);
                ptColl.LoadAll();
                cboSRPaymentType.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var pt in ptColl)
                {
                    cboSRPaymentType.Items.Add(new RadComboBoxItem(pt.PaymentTypeName, pt.SRPaymentTypeID));
                }

                var bankColl = new BankCollection();
                bankColl.Query.Where(bankColl.Query.IsActive == true, bankColl.Query.IsAssetAuctionPayment == true);
                bankColl.LoadAll();
                cboBankID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var bank in bankColl)
                {
                    cboBankID.Items.Add(new RadComboBoxItem(bank.BankName, bank.BankID));
                }
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            ToolBarMenuSearch.Visible = false;
            ToolBarMenuAdd.Visible = false;

            cboAssetID.Enabled = false;
            cboFromServiceUnit.Enabled = false;
            cboFromLocation.Enabled = false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        private void PopulateDetailFromDisposed(int refNo)
        {
            var disposed = new AssetStatusHistory();
            disposed.LoadByPrimaryKey(refNo);

            var assetq = new AssetQuery("a");
            var funitq = new ServiceUnitQuery("b");
            var tunitq = new ServiceUnitQuery("c");
            assetq.InnerJoin(funitq).On(assetq.ServiceUnitID == funitq.ServiceUnitID);
            assetq.LeftJoin(tunitq).On(assetq.MaintenanceServiceUnitID == tunitq.ServiceUnitID);
            assetq.Select(assetq.AssetID, assetq.AssetName, assetq.SerialNumber, funitq.ServiceUnitName,
                          tunitq.ServiceUnitName.As("MaintenanceServiceUnitName"));
            assetq.Where(assetq.AssetID == disposed.AssetID);
            cboAssetID.DataSource = assetq.LoadDataTable();
            cboAssetID.DataBind();
            cboAssetID.SelectedValue = disposed.AssetID;

            chkIsFixedAssetFrom.Checked = disposed.IsFixedAssetTo ?? false;
            txtDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtNotes.Text = string.Empty;

            if (chkIsFixedAssetFrom.Checked)
            {
                var adpQ = new AssetDepreciationPostQuery("adp");
                adpQ.Where(adpQ.AssetID == disposed.AssetID);
                adpQ.Where(@"<CAST(adp.[Month] AS INT) = MONTH(GETDATE()) AND CAST(adp.[Year] AS INT) = YEAR(GETDATE())>");
                DataTable dtb = adpQ.LoadDataTable();
                txtCurrentValue.Value = dtb.Rows.Count > 0 ? (Convert.ToDouble(dtb.Rows[0]["CurrentAmount"]) - Convert.ToDouble(dtb.Rows[0]["DepreciationAmount"])) : 0;
                txtDepreciationAccValue.Value = dtb.Rows.Count > 0 ? Convert.ToDouble(dtb.Rows[0]["AccumulationAmount"]) : 0;
            }
            else
            {
                txtCurrentValue.Value = 0;
                txtDepreciationAccValue.Value = 0;
            }

            #region Asset Info
            var asset = new Asset();
            asset.LoadByPrimaryKey(disposed.AssetID);

            var unitq = new ServiceUnitQuery();
            unitq.Where(unitq.ServiceUnitID == asset.ServiceUnitID);
            cboFromServiceUnit.DataSource = unitq.LoadDataTable();
            cboFromServiceUnit.DataBind();
            cboFromServiceUnit.SelectedValue = asset.ServiceUnitID;

            if (string.IsNullOrEmpty(asset.AssetLocationID))
            {
                PopulateRoomList(asset.ServiceUnitID, false, cboFromLocation);
                cboFromLocation.SelectedValue = asset.AssetLocationID;
            }
            else
            {
                cboFromLocation.Items.Clear();
                cboFromLocation.Text = string.Empty;
            }

            txtBrandName.Text = asset.BrandName;
            txtSerialNumber.Text = asset.SerialNumber;
            var g = AssetGroup.Get(asset.AssetGroupID);
            txtAssetGroup.Text = string.Format("{0} - {1}", asset.AssetGroupID, g.GroupName);
            txtPurchaseDate2.Text = asset.PurchasedDate.Value.ToString("dd-MMMM-yyyy");
            #endregion
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new AssetStatusHistory());

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuEditClick()
        {
            if (cboSRPaymentType.SelectedValue == AppSession.Parameter.PaymentTypeCredit)
                cboSRPaymentMethod.Enabled = false;
            else
                cboSRPaymentMethod.Enabled = true;

            if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodCash)
                cboBankID.Enabled = false;
            else
                cboBankID.Enabled = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            //var entity = new CssdSterileItemsReceived();
            //if (entity.LoadByPrimaryKey(txtReceivedNo.Text))
            //{
            //    entity.MarkAsDeleted();

            //    SaveEntity(entity);
            //}
            //else
            //    args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboAssetID.SelectedValue))
            {
                args.MessageText = "Asset is required.";
                args.IsCancel = true;
                return;
            }
            if (txtSalesPrice.Value <= 0)
            {
                args.MessageText = "Price is required.";
                args.IsCancel = true;
                return;
            }
            if (cboSRPaymentMethod.SelectedValue == AppSession.Parameter.PaymentMethodTransfer && string.IsNullOrEmpty(cboBankID.SelectedValue))
            {
                args.MessageText = "Bank is required.";
                args.IsCancel = true;
                return;
            }

            var entity = new AssetStatusHistory();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (txtSalesPrice.Value <= 0)
            {
                args.MessageText = "Price is required.";
                args.IsCancel = true;
                return;
            }

            var entity = new AssetStatusHistory();
            if (entity.LoadByPrimaryKey(txtSeqNo.Text.ToInt()))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
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
            auditLogFilter.PrimaryKeyData = string.Format("SeqNo='{0}'", txtSeqNo.Text.Trim());
            auditLogFilter.TableName = "AssetStatusHistory";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_SeqNo", txtSeqNo.Text);
            printJobParameters.AddNew("p_UserID", AppSession.UserLogin.UserID);
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return ViewState["IsApproved"] == null ? false : !(bool)ViewState["IsApproved"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new AssetStatusHistory();
            if (parameters.Length > 0)
            {
                var id = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(id.ToInt());
            }
            else
                entity.LoadByPrimaryKey(txtSeqNo.Text.ToInt());

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var auc = (AssetStatusHistory)entity;

            txtSeqNo.Text = auc.SeqNo.ToString();
            txtDate.SelectedDate = auc.TransactionDate;

            if (!string.IsNullOrEmpty(auc.AssetID))
            {
                var assetq = new AssetQuery("a");
                var funitq = new ServiceUnitQuery("b");
                var tunitq = new ServiceUnitQuery("c");
                assetq.InnerJoin(funitq).On(assetq.ServiceUnitID == funitq.ServiceUnitID);
                assetq.LeftJoin(tunitq).On(assetq.MaintenanceServiceUnitID == tunitq.ServiceUnitID);
                assetq.Select(assetq.AssetID, assetq.AssetName, assetq.SerialNumber, funitq.ServiceUnitName,
                              tunitq.ServiceUnitName.As("MaintenanceServiceUnitName"));
                assetq.Where(assetq.AssetID == auc.AssetID);
                cboAssetID.DataSource = assetq.LoadDataTable();
                cboAssetID.DataBind();
                cboAssetID.SelectedValue = auc.AssetID;

                #region Asset Info
                var asset = new Asset();
                asset.LoadByPrimaryKey(auc.AssetID);

                var unitq = new ServiceUnitQuery();
                unitq.Where(unitq.ServiceUnitID == asset.ServiceUnitID);
                cboFromServiceUnit.DataSource = unitq.LoadDataTable();
                cboFromServiceUnit.DataBind();
                cboFromServiceUnit.SelectedValue = asset.ServiceUnitID;

                if (string.IsNullOrEmpty(asset.AssetLocationID))
                {
                    PopulateRoomList(asset.ServiceUnitID, false, cboFromLocation);
                    cboFromLocation.SelectedValue = asset.AssetLocationID;
                }
                else
                {
                    cboFromLocation.Items.Clear();
                    cboFromLocation.SelectedValue = string.Empty;
                    cboFromLocation.Text = string.Empty;
                }

                txtBrandName.Text = asset.BrandName;
                txtSerialNumber.Text = asset.SerialNumber;
                var g = AssetGroup.Get(asset.AssetGroupID);
                txtAssetGroup.Text = string.Format("{0} - {1}", asset.AssetGroupID, g.GroupName);
                txtPurchaseDate2.Text = asset.PurchasedDate.Value.ToString("dd-MMMM-yyyy");
                #endregion
            }
            else
            {
                cboAssetID.Items.Clear();
                cboAssetID.SelectedValue = string.Empty;
                cboAssetID.Text = string.Empty;

                cboFromServiceUnit.Items.Clear();
                cboFromServiceUnit.SelectedValue = string.Empty;
                cboFromServiceUnit.Text = string.Empty;

                cboFromLocation.Items.Clear();
                cboFromLocation.SelectedValue = string.Empty;
                cboFromLocation.Text = string.Empty;

                txtBrandName.Text = string.Empty;
                txtSerialNumber.Text = string.Empty;
                txtAssetGroup.Text = string.Empty;
                txtPurchaseDate2.Text = string.Empty;
            }

            chkIsFixedAssetFrom.Checked = auc.IsFixedAssetFrom ?? false;
            txtDate.SelectedDate = auc.TransactionDate;
            txtNotes.Text = auc.Notes;
            txtCurrentValue.Value = Convert.ToDouble(auc.CurrentValue);
            txtBuyersName.Text = auc.BuyersName;
            txtBuyersAddress.Text = auc.BuyersAddress;
            txtBuyersPhoneNo.Text = auc.BuyersPhoneNo;
            txtBuyersTaxRegister.Text = auc.BuyersTaxRegister;

            txtDepreciationAccValue.Value = Convert.ToDouble(auc.DepreciationAccValue);
            txtSalesPrice.Value = Convert.ToDouble(auc.SalesPrice);
            rblTaxStatus.SelectedIndex = auc.TaxStatus == "1" ? 0 : (auc.TaxStatus == "0" ? 1 : 2);
            txtTaxPercentage.Value = Convert.ToDouble(auc.TaxPercentage);
            txtTaxAmount.Value = Convert.ToDouble(auc.TaxAmount);
            txtTotal.Value = txtSalesPrice.Value + txtTaxAmount.Value;
            cboSRPaymentType.SelectedValue = auc.SRPaymentType;

            cboSRPaymentMethod.Items.Clear();
            cboSRPaymentMethod.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            var pmColl = new PaymentMethodCollection();
            pmColl.Query.Where(pmColl.Query.SRPaymentTypeID == cboSRPaymentType.SelectedValue);
            pmColl.LoadAll();
            foreach (var pm in pmColl)
            {
                cboSRPaymentMethod.Items.Add(new RadComboBoxItem(pm.PaymentMethodName, pm.SRPaymentMethodID));
            }
            cboSRPaymentMethod.SelectedValue = auc.SRPaymentMethod;
            
            cboBankID.SelectedValue = auc.BankID;

            if (Request.QueryString["refNo"] != "-1" && DataModeCurrent == AppEnum.DataMode.New)
            {
                PopulateDetailFromDisposed(Request.QueryString["refNo"].ToInt());
            }

            ViewState["IsApproved"] = auc.IsApproved ?? false;
            ViewState["IsVoid"] = auc.IsVoid ?? false;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new AssetStatusHistory();
            if (!entity.LoadByPrimaryKey(txtSeqNo.Text.ToInt()))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return;
            }

            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            bool isAutoJournal = false;
            if (AppSession.Parameter.acc_IsAutoJournalAssetAuction)
            {
                var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.TransactionDate ?? DateTime.Now);
                if (isClosingPeriod)
                {
                    args.MessageText = "Financial statements for period: " +
                                       string.Format("{0:MMMM-yyyy}", entity.TransactionDate ?? DateTime.Now) +
                                       " have been closed. Please contact the authorities.";
                    args.IsCancel = true;
                    return;
                }
                isAutoJournal = true;
            }

            SetApproval(entity, args, isAutoJournal);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
        }

        private void SetApproval(AssetStatusHistory entity, ValidateArgs args, bool isAutoJournal)
        {
            using (var trans = new esTransactionScope())
            {
                entity.IsApproved = true;
                entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();

                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.Save();

                var asset = new Asset();
                if (asset.LoadByPrimaryKey(entity.AssetID))
                {
                    asset.SRAssetsStatus = AppSession.Parameter.AssetsStatusSold;
                    asset.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    asset.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    asset.Save();                
                }

                /* Automatic Journal Start */
                if (isAutoJournal)
                {
                    int? journalId = JournalTransactions.AddNewAssetAuctionJournal(entity, asset, AppSession.UserLogin.UserID, 0);
                }

                /* Automatic Journal End */

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new AssetStatusHistory();
            if (!entity.LoadByPrimaryKey(txtSeqNo.Text.ToInt()))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity);
        }

        private void SetVoid(AssetStatusHistory entity)
        {
            //header
            entity.IsVoid = true;
            entity.VoidByUserID = AppSession.UserLogin.UserID;
            entity.VoidDateTime = (new DateTime()).NowAtSqlServer();

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.Save();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(AssetStatusHistory entity)
        {
            entity.TransactionDate = txtDate.SelectedDate;
            entity.AssetID = cboAssetID.SelectedValue;
            entity.SRAssetsStatusFrom = AppSession.Parameter.AssetsStatusDisposed;
            entity.SRAssetsStatusTo = AppSession.Parameter.AssetsStatusSold;
            entity.IsFixedAssetFrom = chkIsFixedAssetFrom.Checked;
            entity.IsFixedAssetTo = chkIsFixedAssetFrom.Checked;
            entity.CurrentValue = Convert.ToDecimal(txtCurrentValue.Value);
            entity.Notes = txtNotes.Text;

            entity.BuyersName = txtBuyersName.Text;
            entity.BuyersAddress = txtBuyersAddress.Text;
            entity.BuyersPhoneNo = txtBuyersPhoneNo.Text;
            entity.BuyersTaxRegister = txtBuyersTaxRegister.Text;

            entity.DepreciationAccValue = Convert.ToDecimal(txtDepreciationAccValue.Value);
            entity.SalesPrice = Convert.ToDecimal(txtSalesPrice.Value);
            entity.TaxStatus= rblTaxStatus.SelectedIndex == 0 ? "1" : (rblTaxStatus.SelectedIndex == 1 ? "0" : "2");
            entity.TaxPercentage = Convert.ToDecimal(txtTaxPercentage.Value);
            entity.TaxAmount = Convert.ToDecimal(txtTaxAmount.Value);
            entity.SRPaymentType = cboSRPaymentType.SelectedValue;
            entity.SRPaymentMethod = cboSRPaymentMethod.SelectedValue;
            entity.BankID = cboBankID.SelectedValue;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        }

        private void SaveEntity(AssetStatusHistory entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();

                txtSeqNo.Text = entity.SeqNo.ToString();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new AssetStatusHistoryQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.SeqNo > txtSeqNo.Text.ToInt()
                    );
                que.OrderBy(que.SeqNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.SeqNo < txtSeqNo.Text.ToInt()
                    );
                que.OrderBy(que.SeqNo.Descending);
            }
            que.Where(que.SRAssetsStatusTo == AppSession.Parameter.AssetsStatusSold);

            var entity = new AssetStatusHistory();
            if (entity.Load(que))
            {
                OnPopulateEntryControl(entity);
            }
        }

        #endregion

        #region Combobox
        protected void cboFromServiceUnit_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new ServiceUnitQuery("a");
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            query.es.Top = 20;
            query.Where
                (
                    query.ServiceUnitName.Like(searchText),
                    query.IsActive == true
                );
            query.OrderBy(query.ServiceUnitID.Ascending);

            cboFromServiceUnit.DataSource = query.LoadDataTable();
            cboFromServiceUnit.DataBind();
        }

        protected void cboServiceUnit_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitID"] + @" - " + ((DataRowView)e.Item.DataItem)["ServiceUnitName"];
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void cboAssetID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new AssetQuery("a");
            var fromunit = new ServiceUnitQuery("b");
            var tounit = new ServiceUnitQuery("c");
            query.InnerJoin(fromunit).On(query.ServiceUnitID == fromunit.ServiceUnitID);
            query.LeftJoin(tounit).On(query.MaintenanceServiceUnitID == tounit.ServiceUnitID);
            query.es.Top = 20;
            query.Select(query.AssetID, query.AssetName, query.SerialNumber, fromunit.ServiceUnitName,
                         tounit.ServiceUnitName.As("MaintenanceServiceUnitName"));
            query.Where
                (
                    query.Or(query.AssetName.Like(searchText), query.SerialNumber.Like(searchText)),
                    query.ServiceUnitID == cboFromServiceUnit.SelectedValue
                );

            query.Where(query.SRAssetsStatus == AppSession.Parameter.AssetsStatusDisposed);

            if (!string.IsNullOrEmpty(cboFromLocation.SelectedValue))
            {
                query.Where(query.AssetLocationID == cboFromLocation.SelectedValue);
            }
            query.OrderBy(query.AssetName.Ascending);
            DataTable dtb = query.LoadDataTable();
            cboAssetID.DataSource = dtb;
            cboAssetID.DataBind();
        }

        protected void cboAssetID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["AssetName"] + " (" + ((DataRowView)e.Item.DataItem)["AssetID"] + ")";
            e.Item.Value = ((DataRowView)e.Item.DataItem)["AssetID"].ToString();
        }

        #endregion

        #region Populate List
        private void PopulateRoomList(string serviceUnitId, bool isNew, RadComboBox comboBox)
        {
            if (serviceUnitId != string.Empty)
            {
                var sr = new ServiceRoomCollection();
                sr.Query.Where(sr.Query.ServiceUnitID == serviceUnitId);

                if (isNew)
                    sr.Query.Where(sr.Query.IsActive == true);

                sr.LoadAll();

                comboBox.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceRoom entity in sr)
                {
                    comboBox.Items.Add(new RadComboBoxItem(entity.RoomName, entity.RoomID));
                }
            }
        }
        #endregion

        protected void txtSalesPrice_TextChanged(object sender, EventArgs e)
        {
            CalculateTax();
        }

        protected void rblTaxStatus_OnTextChanged(object sender, EventArgs e)
        {
            if (rblTaxStatus.SelectedIndex == 0 || rblTaxStatus.SelectedIndex == 1)
                txtTaxPercentage.Value = AppSession.Parameter.Ppn;
            else
                txtTaxPercentage.Value = 0;

            CalculateTax();
        }

        private void CalculateTax()
        {
            if (txtTaxPercentage.Value == 0 || rblTaxStatus.SelectedIndex == 1)
                txtTaxAmount.Value = 0.00;
            else
            {
                txtTaxAmount.Value = ((txtSalesPrice.Value * txtTaxPercentage.Value) / Convert.ToDouble(100));
            }
            txtTotal.Value = txtSalesPrice.Value + txtTaxAmount.Value;
        }

        protected void cboSRPaymentType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRPaymentMethod.Items.Clear();
            cboSRPaymentMethod.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            var pmColl = new PaymentMethodCollection();
            pmColl.Query.Where(pmColl.Query.SRPaymentTypeID == e.Value);
            pmColl.LoadAll();

            foreach (var pm in pmColl)
            {
                cboSRPaymentMethod.Items.Add(new RadComboBoxItem(pm.PaymentMethodName, pm.SRPaymentMethodID));
            }
            
            if (e.Value == AppSession.Parameter.PaymentTypeCredit)
            {
                cboSRPaymentMethod.Enabled = false;
                cboBankID.Enabled = false;
            }
            else
            {
                cboSRPaymentMethod.Enabled = true;
                cboBankID.Enabled = true;
            }
        }

        protected void cboSRPaymentMethod_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (e.Value == AppSession.Parameter.PaymentMethodCash)
            {
                cboBankID.Enabled = false;
            }
            else
            {
                cboBankID.Enabled = true;
            }
        }
    }
}