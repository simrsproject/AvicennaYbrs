using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;


namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class ProductionOfGoodsDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber, _autoNumberCssdReceivedNo, _autoNumberCssdItemNo;

        private void PopulateNewTransactionNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New)
                return;
            if (cboServiceUnitID.SelectedValue == string.Empty)
                return;

            var serv = new ServiceUnit();
            if (serv.LoadByPrimaryKey(cboServiceUnitID.SelectedValue))
            {
                _autoNumber = Helper.GetNewAutoNumber(txtProductionDate.SelectedDate.Value.Date, TransactionCode.ProductionOfGoods, serv.DepartmentID);
                txtProductionNo.Text = _autoNumber.LastCompleteNumber;
            }
        }

        private string GetNewCssdReceivedNo()
        {
            _autoNumberCssdReceivedNo = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.CssdReceivedNo);

            return _autoNumberCssdReceivedNo.LastCompleteNumber;
        }

        private string GetNewCssdItemNo()
        {
            _autoNumberCssdItemNo = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.CssdItemNo);

            return _autoNumberCssdItemNo.LastCompleteNumber;
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ProductionOfGoodsSearch.aspx";
            UrlPageList = "ProductionOfGoodsList.aspx";

            ProgramID = AppConstant.Program.ProductionOfGoods;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");
                query.Where(query.IsActive == true);

                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);

                cboToServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboToServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(cboServiceUnitID, cboServiceUnitID);
            ajax.AddAjaxSetting(cboServiceUnitID, txtProductionNo);
            ajax.AddAjaxSetting(cboServiceUnitID, cboLocationID);
            ajax.AddAjaxSetting(cboServiceUnitID, cboFormulaID);
            ajax.AddAjaxSetting(cboServiceUnitID, cboToServiceUnitID);

            ajax.AddAjaxSetting(grdItem, grdItem);
            ajax.AddAjaxSetting(grdItem, cboServiceUnitID);
            ajax.AddAjaxSetting(grdItem, cboLocationID);
            ajax.AddAjaxSetting(grdItem, cboFormulaID);
            ajax.AddAjaxSetting(grdItem, txtPrice);
            ajax.AddAjaxSetting(grdItem, txtCostAmount);
            ajax.AddAjaxSetting(grdItem, txtQty);

            ajax.AddAjaxSetting(cboFormulaID, grdItem);
            ajax.AddAjaxSetting(cboFormulaID, txtItemProductionName);
            ajax.AddAjaxSetting(cboFormulaID, chkIsControlExpired);
            ajax.AddAjaxSetting(cboFormulaID, txtQty);
            ajax.AddAjaxSetting(cboFormulaID, txtPrice);
            ajax.AddAjaxSetting(cboFormulaID, txtCostAmount);

            ajax.AddAjaxSetting(txtQty, grdItem);
            ajax.AddAjaxSetting(txtQty, txtPrice);
            ajax.AddAjaxSetting(txtQty, txtCostAmount);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuEditClick()
        {
            string serviceUnitID = cboServiceUnitID.SelectedValue;
            ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, TransactionCode.ProductionOfGoods, true);
            cboServiceUnitID.SelectedValue = serviceUnitID;

            cboServiceUnitID.Enabled = ProductionOfGoodsItems.Count == 0;
            cboLocationID.Enabled = ProductionOfGoodsItems.Count == 0;
            cboFormulaID.Enabled = ProductionOfGoodsItems.Count == 0;
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new ProductionOfGoods();
            if (entity.LoadByPrimaryKey(txtProductionNo.Text))
            {
                if (!IsApprovedOrVoid(entity, args))
                    return;
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            string result = ProductionOfGoods.IsItemMinusProcess(txtProductionNo.Text, ProductionOfGoodsItems);
            if (result != string.Empty)
            {
                args.MessageText = result;
                args.IsCancel = true;
                return;
            }

            string itemZeroCostPrice;
            ProductionOfGoods.UpdateCostPrice(ProductionOfGoodsItems, out itemZeroCostPrice);
            if (!string.IsNullOrEmpty(itemZeroCostPrice))
            {
                args.MessageText = "Zero cost price of item : " + itemZeroCostPrice;
                args.IsCancel = true;
                return;
            }

            var loc = new Location();
            if (loc.LoadByPrimaryKey(cboLocationID.SelectedValue) && loc.IsHoldForTransaction == true)
            {
                args.MessageText = "Location: " + loc.LocationName + " in Hold For Transaction status. Transaction is not allowed.";
                args.IsCancel = true;
                return;
            }

            var productionOfGoodsItemColl = new ProductionOfGoodsItemCollection();
            productionOfGoodsItemColl.Query.Where(productionOfGoodsItemColl.Query.ProductionNo == txtProductionNo.Text);
            productionOfGoodsItemColl.LoadAll();

            result = (new ProductionOfGoods()).Approve(txtProductionNo.Text, productionOfGoodsItemColl, AppSession.UserLogin.UserID, AppSession.Parameter.TaxPercentage, AppSession.Parameter.IsEnabledStockWithEdControl);
            if (result != string.Empty)
            {
                args.MessageText = result;
                args.IsCancel = true;
                return;
            }

            #region process to sterilisasi
            if (AppSession.Parameter.IsProductionOfGoodsAutoCssdReceived)
            {
                var entity = new ProductionOfGoods();
                entity.LoadByPrimaryKey(txtProductionNo.Text);

                var pf = new ProductionFormula();
                if (pf.LoadByPrimaryKey(entity.FormulaID))
                {
                    var i = new Item();
                    if (i.LoadByPrimaryKey(pf.ItemID) && i.IsNeedToBeSterilized == true)
                    {
                        var srItemUnit = string.Empty;

                        switch (i.SRItemType)
                        {
                            case ItemType.Medical:
                                var ipm = new ItemProductMedic();
                                ipm.LoadByPrimaryKey(i.ItemID);
                                srItemUnit = ipm.SRItemUnit;
                                break;
                            case ItemType.NonMedical:
                                var ipnm = new ItemProductNonMedic();
                                ipnm.LoadByPrimaryKey(i.ItemID);
                                srItemUnit = ipnm.SRItemUnit;
                                break;
                            case ItemType.Kitchen:
                                var ik = new ItemKitchen();
                                ik.LoadByPrimaryKey(i.ItemID);
                                srItemUnit = ik.SRItemUnit;
                                break;
                        }

                        using (var trans = new esTransactionScope())
                        {
                            var hd = new CssdSterileItemsReceived();
                            hd.AddNew();

                            hd.ReceivedNo = GetNewCssdReceivedNo();
                            _autoNumberCssdReceivedNo.Save();
                            
                            hd.ReceivedDate = (new DateTime()).NowAtSqlServer().Date;// DateTime.Now.Date;
                            hd.ReceivedTime = (new DateTime()).NowAtSqlServer().ToString("HH:mm");// DateTime.Now.ToString("HH:mm");
                            hd.FromServiceUnitID = cboToServiceUnitID.SelectedValue;
                            hd.SenderByID = AppSession.Parameter.CssdSenderBySelf;
                            hd.SenderBy = AppSession.UserLogin.UserName;
                            hd.ReceivedByUserID = AppSession.UserLogin.UserID;
                            hd.IsFromProductionOfGoods = true;
                            hd.ProductionNo = txtProductionNo.Text;
                            hd.IsApproved = true;
                            hd.ApprovedDateTime = (new DateTime()).NowAtSqlServer();// DateTime.Now;
                            hd.ApprovedByUserID = AppSession.UserLogin.UserID;
                            hd.IsVoid = false;
                            hd.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();// DateTime.Now;
                            hd.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            hd.Save();

                            var dt = new CssdSterileItemsReceivedItem();
                            dt.AddNew();
                            dt.ReceivedNo = hd.ReceivedNo;
                            dt.ReceivedSeqNo = "001";
                            dt.ItemID = pf.ItemID;
                            dt.SRCssdItemUnit = srItemUnit;
                            dt.Qty = entity.Qty * pf.Qty;
                            dt.Notes = string.Empty;
                            dt.CssdItemNo = GetNewCssdItemNo();
                            _autoNumberCssdItemNo.Save();
                            if (txtExpiredDate.IsEmpty)
                                dt.str.ExpiredDate = string.Empty;
                            else
                                dt.ExpiredDate = txtExpiredDate.SelectedDate;
                            dt.ReuseTo = 0;
                            dt.IsNeedUltrasound = false;
                            dt.IsDtt = false;
                            dt.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();// DateTime.Now;
                            dt.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            dt.Save();

                            var dtDetails = new CssdSterileItemsReceivedItemDetailCollection();
                            var items = new CssdItemDetailCollection();
                            items.Query.Where(items.Query.ItemID == dt.ItemID);
                            items.LoadAll();

                            foreach (var d in items)
                            {
                                var det = dtDetails.AddNew();
                                det.ReceivedNo = dt.ReceivedNo;
                                det.ReceivedSeqNo = dt.ReceivedSeqNo;
                                det.ItemID = dt.ItemID;
                                det.ItemDetailID = d.ItemDetailID;

                                var item = new Item();
                                if (item.LoadByPrimaryKey(det.ItemDetailID))
                                    det.ItemName = item.ItemName;
                                else det.ItemName = string.Empty;

                                det.Qty = dt.Qty * d.Qty;
                                det.QtyReceived = det.Qty;

                                det.IsBrokenInstrument = false;
                                det.QtyReplacements = 0;

                                var itemDetQ = new VwItemProductMedicNonMedicQuery();
                                itemDetQ.Where(itemDetQ.ItemID == det.ItemDetailID);
                                var itemDet = new VwItemProductMedicNonMedic();
                                itemDet.Load(itemDetQ);

                                det.SRItemUnit = itemDet.SRItemUnit;
                            }

                            var dtoColl = new CssdSterileItemsReceivedItemCollection();
                            var pfoColl = new ProductionFormulaOtherItemCollection();
                            pfoColl.Query.Where(pfoColl.Query.FormulaID == entity.FormulaID);
                            pfoColl.LoadAll();
                            var x = 1;
                            foreach (var pfo in pfoColl)
                            {
                                i = new Item();
                                if (i.LoadByPrimaryKey(pfo.ItemID) && i.IsNeedToBeSterilized == true)
                                {
                                    x++;

                                    switch (i.SRItemType)
                                    {
                                        case ItemType.Medical:
                                            var ipm = new ItemProductMedic();
                                            ipm.LoadByPrimaryKey(i.ItemID);
                                            srItemUnit = ipm.SRItemUnit;
                                            break;
                                        case ItemType.NonMedical:
                                            var ipnm = new ItemProductNonMedic();
                                            ipnm.LoadByPrimaryKey(i.ItemID);
                                            srItemUnit = ipnm.SRItemUnit;
                                            break;
                                        case ItemType.Kitchen:
                                            var ik = new ItemKitchen();
                                            ik.LoadByPrimaryKey(i.ItemID);
                                            srItemUnit = ik.SRItemUnit;
                                            break;
                                    }

                                    var dto = dtoColl.AddNew();
                                    dto.ReceivedNo = hd.ReceivedNo;
                                    dto.ReceivedSeqNo = string.Format("{0:000}", x);
                                    dto.ItemID = pfo.ItemID;
                                    dto.SRCssdItemUnit = srItemUnit;
                                    dto.Qty = entity.Qty * pfo.Qty;
                                    dto.Notes = string.Empty;
                                    dto.CssdItemNo = GetNewCssdItemNo();
                                    _autoNumberCssdItemNo.Save();
                                    if (txtExpiredDate.IsEmpty)
                                        dto.str.ExpiredDate = string.Empty;
                                    else
                                        dto.ExpiredDate = txtExpiredDate.SelectedDate;
                                    dto.ReuseTo = 0;
                                    dto.IsNeedUltrasound = false;
                                    dto.IsDtt = false;
                                    dto.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();// DateTime.Now;
                                    dto.LastUpdateByUserID = AppSession.UserLogin.UserID;

                                    items = new CssdItemDetailCollection();
                                    items.Query.Where(items.Query.ItemID == dto.ItemID);
                                    items.LoadAll();

                                    foreach (var d in items)
                                    {
                                        var det = dtDetails.AddNew();
                                        det.ReceivedNo = dto.ReceivedNo;
                                        det.ReceivedSeqNo = dto.ReceivedSeqNo;
                                        det.ItemID = dto.ItemID;
                                        det.ItemDetailID = d.ItemDetailID;

                                        var item = new Item();
                                        if (item.LoadByPrimaryKey(det.ItemDetailID))
                                            det.ItemName = item.ItemName;
                                        else det.ItemName = string.Empty;

                                        det.Qty = dto.Qty * d.Qty;
                                        det.QtyReceived = det.Qty;

                                        det.IsBrokenInstrument = false;
                                        det.QtyReplacements = 0;

                                        var itemDetQ = new VwItemProductMedicNonMedicQuery();
                                        itemDetQ.Where(itemDetQ.ItemID == det.ItemDetailID);
                                        var itemDet = new VwItemProductMedicNonMedic();
                                        itemDet.Load(itemDetQ);

                                        det.SRItemUnit = itemDet.SRItemUnit;
                                    }
                                }
                            }
                            dtoColl.Save();
                            dtDetails.Save();

                            trans.Complete();
                        }
                    }
                }
            }
            #endregion
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            (new ProductionOfGoods()).Void(txtProductionNo.Text, AppSession.UserLogin.UserID);
        }

        private bool IsApprovedOrVoid(ProductionOfGoods entity, ValidateArgs args)
        {
            if (entity.IsApproved != null && entity.IsApproved.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            if (entity.IsVoid != null && entity.IsVoid.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ProductionOfGoods());
            ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, TransactionCode.ProductionOfGoods, true);
            txtProductionDate.SelectedDate = (new DateTime()).NowAtSqlServer();// DateTime.Now;
            txtItemProductionName.Text = string.Empty;
            chkIsControlExpired.Checked = false;
            cboLocationID.Items.Clear();
            cboLocationID.Text = string.Empty;
            PopulateNewTransactionNo();
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ProductionOfGoods();
            if (entity.LoadByPrimaryKey(txtProductionNo.Text))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (chkIsControlExpired.Checked && AppSession.Parameter.IsEnabledStockWithEdControl)
            {
                if (string.IsNullOrWhiteSpace(txtBatchNumber.Text))
                {
                    args.MessageText = "Batch Number is required.";
                    args.IsCancel = true;
                    return;
                }
                if (txtExpiredDate.IsEmpty)
                {
                    args.MessageText = "Expired Date is required.";
                    args.IsCancel = true;
                    return;
                }
            }

            PopulateNewTransactionNo();
            // save autonumber immediately to decrease time gap between create and save
            _autoNumber.Save();

            var entity = new ProductionOfGoods();
            if (entity.LoadByPrimaryKey(txtProductionNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new ProductionOfGoods();
            entity.AddNew();
            SetEntityValue(entity);
            if (ProductionOfGoodsItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (chkIsControlExpired.Checked && AppSession.Parameter.IsEnabledStockWithEdControl)
            {
                if (string.IsNullOrWhiteSpace(txtBatchNumber.Text))
                {
                    args.MessageText = "Batch Number is required.";
                    args.IsCancel = true;
                    return;
                }
                if (txtExpiredDate.IsEmpty)
                {
                    args.MessageText = "Expired Date is required.";
                    args.IsCancel = true;
                    return;
                }
            }

            var entity = new ProductionOfGoods();
            if (entity.LoadByPrimaryKey(txtProductionNo.Text))
            {
                SetEntityValue(entity);
                if (ProductionOfGoodsItems.Count == 0)
                {
                    args.MessageText = AppConstant.Message.RecordDetailEmpty;
                    args.IsCancel = true;
                    return;
                }
                SaveEntity(entity);
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
            auditLogFilter.PrimaryKeyData = string.Format("ProductionNo='{0}'", txtProductionNo.Text.Trim());
            auditLogFilter.TableName = "ProductionOfGoods";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_ProductionNo", txtProductionNo.Text);
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuEdit()
        {
            return txtProductionNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !chkIsApproved.Checked;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !chkIsVoid.Checked;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ProductionOfGoods();
            if (parameters.Length > 0)
            {
                String prodNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(prodNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtProductionNo.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var pog = (ProductionOfGoods)entity;
            txtProductionNo.Text = pog.ProductionNo;
            txtProductionDate.SelectedDate = pog.ProductionDate;
            ComboBox.PopulateWithOneServiceUnit(cboServiceUnitID, pog.ServiceUnitID ?? string.Empty);
            if (!string.IsNullOrEmpty(pog.ServiceUnitID))
            {
                ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, pog.ServiceUnitID);
                if (!string.IsNullOrEmpty(pog.LocationID))
                    cboLocationID.SelectedValue = pog.LocationID;
                else cboLocationID.SelectedIndex = 1;
            }
            
            if (!string.IsNullOrEmpty(pog.ToServiceUnitID))
                cboToServiceUnitID.SelectedValue = pog.ToServiceUnitID;
            else
            {
                cboToServiceUnitID.SelectedValue = string.Empty;
                cboServiceUnitID.Text = string.Empty;
                cboLocationID.Items.Clear();
                cboLocationID.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(pog.FormulaID))
            {
                var query = new ProductionFormulaQuery("a");

                query.Select(query.FormulaID, query.FormulaName);
                query.Where(query.FormulaID == pog.FormulaID);

                DataTable dtb = query.LoadDataTable();
                cboFormulaID.DataSource = dtb;
                cboFormulaID.DataBind();
                cboFormulaID.SelectedValue = pog.FormulaID;
                cboFormulaID.Text = dtb.Rows[0]["FormulaName"] + " (" + dtb.Rows[0]["FormulaID"] + ")";
            }
            else
            {
                cboFormulaID.Items.Clear();
                cboFormulaID.Text = string.Empty;
            }

            txtQty.Value = Convert.ToDouble(pog.Qty);
            txtPrice.Value = Convert.ToDouble(pog.Price - pog.CostAmount);
            txtCostAmount.Value = Convert.ToDouble(pog.CostAmount);
            txtExpiredDate.SelectedDate = pog.ExpiredDate;
            txtBatchNumber.Text = pog.BatchNumber;
            chkIsApproved.Checked = pog.IsApproved ?? false;
            chkIsVoid.Checked = pog.IsVoid ?? false;
            txtNotes.Text = pog.Notes;


            //Display Data Detail
            PopulateUnitItem();
            PopulateGridDetail();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(ProductionOfGoods entity)
        {
            entity.ProductionNo = txtProductionNo.Text;
            entity.ProductionDate = txtProductionDate.SelectedDate;
            entity.ServiceUnitID = cboServiceUnitID.SelectedValue;
            entity.LocationID = cboLocationID.SelectedValue;
            entity.FormulaID = cboFormulaID.SelectedValue;
            entity.Qty = Convert.ToDecimal(txtQty.Value);
            entity.CostAmount = Convert.ToDecimal(txtCostAmount.Value);
            entity.Price = Convert.ToDecimal(txtPrice.Value + txtCostAmount.Value);

            if (txtExpiredDate.SelectedDate != null && Convert.ToDateTime(txtExpiredDate.SelectedDate).Year > 1900)
                entity.ExpiredDate = txtExpiredDate.SelectedDate;
            else
                entity.str.ExpiredDate = string.Empty;
            entity.BatchNumber = txtBatchNumber.Text;

            entity.Notes = txtNotes.Text;
            entity.ToServiceUnitID = cboToServiceUnitID.SelectedValue;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();// DateTime.Now;
            }

            //Update Detil
            foreach (ProductionOfGoodsItem item in ProductionOfGoodsItems)
            {
                item.ProductionNo = txtProductionNo.Text;

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();// DateTime.Now;
                }
            }

        }

        private void SaveEntity(ProductionOfGoods entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                ProductionOfGoodsItems.Save();

                //autonumber has been saved on SetEntity
                //if (DataModeCurrent == AppEnum.DataMode.New)
                //    _autoNumber.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ProductionOfGoodsQuery("a");
            var qusr = new AppUserServiceUnitQuery("u");
            que.InnerJoin(qusr).On(que.ServiceUnitID == qusr.ServiceUnitID &&
                                         qusr.UserID == AppSession.UserLogin.UserID);

            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.ProductionNo > txtProductionNo.Text
                    );
                que.OrderBy(que.ProductionNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.ProductionNo < txtProductionNo.Text
                    );
                que.OrderBy(que.ProductionNo.Descending);
            }

            var entity = new ProductionOfGoods();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function

        private ProductionOfGoodsItemCollection ProductionOfGoodsItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collProductionOfGoodsItem" + Request.UserHostName];
                    if (obj != null)
                        return ((ProductionOfGoodsItemCollection)(obj));
                }

                var coll = new ProductionOfGoodsItemCollection();
                var query = new ProductionOfGoodsItemQuery("a");

                var iq = new ItemQuery("b");
                var ibq = new ItemBalanceQuery("c");

                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
                query.LeftJoin(ibq).On(ibq.LocationID == cboLocationID.SelectedValue && ibq.ItemID == query.ItemID);

                query.Where(query.ProductionNo == txtProductionNo.Text);
                query.OrderBy(query.ItemID.Ascending);

                query.Select
                    (
                        query,
                        iq.ItemName.As("refToItem_ItemName"),
                        @"<ISNULL(c.Balance, 0) AS 'refToItemBalance_Balance'>"
                    );

                coll.Load(query);
                Session["collProductionOfGoodsItem" + Request.UserHostName] = coll;

                return coll;
            }
            set { Session["collProductionOfGoodsItem" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            grdItem.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                ProductionOfGoodsItems = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdItem.Rebind();
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            ProductionOfGoodsItems = null; //Reset Record Detail
            grdItem.DataSource = ProductionOfGoodsItems;
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = ProductionOfGoodsItems;
        }

        #endregion

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateNewTransactionNo();
            cboToServiceUnitID.SelectedValue = e.Value;
            ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, e.Value);
            cboLocationID.SelectedIndex = 1;
        }

        protected void cboFormulaID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txtQty.Value = 1;

            PopulateUnitItem();
            FillGridItem();
            CalculateCostPrice();

            grdItem.DataSource = ProductionOfGoodsItems;
            grdItem.DataBind();
        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            FillGridItem();
            CalculateCostPrice();

            grdItem.DataSource = ProductionOfGoodsItems;
            grdItem.DataBind();
        }

        private void PopulateUnitItem()
        {
            if (!string.IsNullOrEmpty(cboFormulaID.SelectedValue))
            {
                string itemUnit = string.Empty;
                bool isControlExpired = false;
                var pf = new ProductionFormula();
                pf.LoadByPrimaryKey(cboFormulaID.SelectedValue);

                var i = new Item();
                if (i.LoadByPrimaryKey(pf.ItemID))
                {
                    if (i.SRItemType == ItemType.Medical)
                    {
                        var x = new ItemProductMedic();
                        x.LoadByPrimaryKey(pf.ItemID);
                        itemUnit = x.SRItemUnit;
                        isControlExpired = x.IsControlExpired ?? false;
                    }
                    else if (i.SRItemType == ItemType.NonMedical)
                    {
                        var y = new ItemProductNonMedic();
                        y.LoadByPrimaryKey(pf.ItemID);
                        itemUnit = y.SRItemUnit;
                        isControlExpired = y.IsControlExpired ?? false;
                    }
                    else
                    {
                        var z = new ItemKitchen();
                        z.LoadByPrimaryKey(pf.ItemID);
                        itemUnit = z.SRItemUnit;
                        isControlExpired = z.IsControlExpired ?? false;
                    }
                    txtItemProductionName.Text = i.ItemName + " @" + string.Format("{0:n0}", (pf.Qty)) + " " + itemUnit;
                }
                else
                    txtItemProductionName.Text = string.Empty;
                
                chkIsControlExpired.Checked = isControlExpired;
            }
        }

        private void CalculateCostPrice()
        {
            decimal? total = 0;

            if (ProductionOfGoodsItems.Count > 0)
            {
                total = ProductionOfGoodsItems.Where(item => item.IsConsumables == true).Aggregate(total, (current, item) => current + (item.Qty*item.PriceInBaseUnit));
            }

            var pf = new ProductionFormula();
            pf.LoadByPrimaryKey(cboFormulaID.SelectedValue);

            txtPrice.Value = Convert.ToDouble(total) / (txtQty.Value * Convert.ToDouble(pf.Qty));

            if (pf.IsCostInPercentage == true)
                txtCostAmount.Value = (Convert.ToDouble(total) / (txtQty.Value * Convert.ToDouble(pf.Qty))) *
                                      Convert.ToDouble(pf.CostAmount) / 100;
            else
                txtCostAmount.Value = Convert.ToDouble(pf.CostAmount);
        }

        private void FillGridItem()
        {
            ProductionOfGoodsItems = null;

            if (ProductionOfGoodsItems.Count > 0)
            {
                foreach (var entity in ProductionOfGoodsItems)
                {
                    var pf = new ProductionFormulaItem();
                    pf.LoadByPrimaryKey(cboFormulaID.SelectedValue, entity.ItemID);

                    entity.Qty = pf.Qty * Convert.ToDecimal(txtQty.Value);
                    entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();// DateTime.Now;
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }
            }
            else
            {
                var pfColl = new ProductionFormulaItemCollection();
                pfColl.Query.Where(pfColl.Query.FormulaID == cboFormulaID.SelectedValue);
                pfColl.LoadAll();

                foreach (var item in pfColl)
                {
                    var entity = ProductionOfGoodsItems.AddNew();
                    entity.ProductionNo = txtProductionNo.Text;
                    entity.ItemID = item.ItemID;

                    var i = new Item();
                    i.LoadByPrimaryKey(entity.ItemID);

                    if (i.SRItemType == ItemType.Medical)
                    {
                        var x = new ItemProductMedic();
                        x.LoadByPrimaryKey(entity.ItemID);
                        entity.SRItemUnit = item.SRItemUnit;
                        entity.CostPrice = x.CostPrice;
                        entity.PriceInBaseUnit = x.PriceInBaseUnit;
                    }
                    else if (i.SRItemType == ItemType.NonMedical)
                    {
                        var y = new ItemProductNonMedic();
                        y.LoadByPrimaryKey(entity.ItemID);
                        entity.SRItemUnit = item.SRItemUnit;
                        entity.CostPrice = y.CostPrice;
                        entity.PriceInBaseUnit = y.PriceInBaseUnit;
                    }
                    else
                    {
                        var z = new ItemKitchen();
                        z.LoadByPrimaryKey(entity.ItemID);
                        entity.SRItemUnit = item.SRItemUnit;
                        entity.CostPrice = z.CostPrice;
                        entity.PriceInBaseUnit = z.PriceInBaseUnit;
                    }
                    entity.ItemName = i.ItemName;
                    entity.Qty = item.Qty * Convert.ToDecimal(txtQty.Value);
                    entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();// DateTime.Now;
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.IsConsumables = item.IsConsumables;

                    var bal = new ItemBalance();
                    if (bal.LoadByPrimaryKey(cboLocationID.SelectedValue, entity.ItemID))
                        entity.Balance = bal.Balance;
                    else entity.Balance = 0;
                }
            }

            Session["collProductionOfGoodsItem" + Request.UserHostName] = ProductionOfGoodsItems;
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (!(sourceControl is RadGrid) || string.IsNullOrEmpty(eventArgument))
                return;
        }

        protected void cboFormulaID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            bool itemMedic = false, itemNonMedic = false, itemKitchen = false;
            var it = new ServiceUnitTransactionCode();
            if (it.LoadByPrimaryKey(cboServiceUnitID.SelectedValue, TransactionCode.ProductionOfGoods))
            {
                itemMedic = it.IsItemProductMedic ?? false;
                itemNonMedic = it.IsItemProductNonMedic ?? false;
                itemKitchen = it.IsItemKitchen ?? false;
            }

            var dtb = FormulaItem(e.Text, itemMedic, itemNonMedic, itemKitchen);
            
            cboFormulaID.DataSource = dtb;
            cboFormulaID.DataBind();
        }

        protected void cboFormulaID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["FormulaName"] + " (" + ((DataRowView)e.Item.DataItem)["FormulaID"] + ")";
            e.Item.Value = ((DataRowView)e.Item.DataItem)["FormulaID"].ToString();
        }

        private DataTable FormulaItem(string parameter, bool isMedic, bool isNonMedic, bool isKitchen)
        {
            DataTable tbl = null;

            try
            {
                DataTable tempTbl = null;
                string searchTextContain = string.Format("%{0}%", parameter);
                if (isMedic)
                {
                    var query = new ProductionFormulaQuery("a");
                    var itemQ = new ItemQuery("b");
                    var balq = new ItemBalanceQuery("c");

                    query.InnerJoin(itemQ).On(query.ItemID == itemQ.ItemID & itemQ.SRItemType == ItemType.Medical);
                    query.InnerJoin(balq).On(query.ItemID == balq.ItemID &
                                             balq.LocationID == cboLocationID.SelectedValue);
                    query.es.Top = 10;
                    query.Select
                        (
                            query.FormulaID,
                            query.FormulaName,
                            itemQ.ItemName,
                            itemQ.SRItemType
                        );
                    query.Where
                        (
                            query.Or
                                (
                                    query.FormulaID.Like(searchTextContain),
                                    query.FormulaName.Like(searchTextContain)
                                ),
                                itemQ.IsActive == true,
                                query.IsActive == true

                        );

                    tempTbl = query.LoadDataTable();
                }

                if (isNonMedic & (tempTbl == null || tempTbl.Rows.Count < 10))
                {
                    var query = new ProductionFormulaQuery("a");
                    var itemQ = new ItemQuery("b");
                    var balq = new ItemBalanceQuery("c");

                    query.InnerJoin(itemQ).On(query.ItemID == itemQ.ItemID & itemQ.SRItemType == ItemType.NonMedical);
                    query.InnerJoin(balq).On(query.ItemID == balq.ItemID &
                                             balq.LocationID == cboLocationID.SelectedValue);
                    query.es.Top = 10;
                    query.Select
                        (
                            query.FormulaID,
                            query.FormulaName,
                            itemQ.ItemName,
                            itemQ.SRItemType
                        );
                    query.Where
                        (
                            query.Or
                                (
                                    query.FormulaID.Like(searchTextContain),
                                    query.FormulaName.Like(searchTextContain)
                                ),
                                itemQ.IsActive == true,
                                query.IsActive == true

                        );
                    if (tempTbl == null)
                        tempTbl = query.LoadDataTable();
                    else 
                        tempTbl.Merge(query.LoadDataTable());
                }

                if (isKitchen & (tempTbl == null || tempTbl.Rows.Count < 10))
                {
                    var query = new ProductionFormulaQuery("a");
                    var itemQ = new ItemQuery("b");
                    var balq = new ItemBalanceQuery("c");

                    query.InnerJoin(itemQ).On(query.ItemID == itemQ.ItemID & itemQ.SRItemType == ItemType.Kitchen);
                    query.InnerJoin(balq).On(query.ItemID == balq.ItemID &
                                             balq.LocationID == cboLocationID.SelectedValue);
                    query.es.Top = 10;
                    query.Select
                        (
                            query.FormulaID,
                            query.FormulaName,
                            itemQ.ItemName,
                            itemQ.SRItemType
                        );
                    query.Where
                        (
                            query.Or
                                (
                                    query.FormulaID.Like(searchTextContain),
                                    query.FormulaName.Like(searchTextContain)
                                ),
                                itemQ.IsActive == true,
                                query.IsActive == true

                        );

                    if (tempTbl == null)
                        tempTbl = query.LoadDataTable();
                    else
                        tempTbl.Merge(query.LoadDataTable());
                }

                // Sort
                var dataSorted = tempTbl.Select("", "FormulaName ASC").CopyToDataTable();

                String item2 = string.Empty;

                foreach (DataRow row in dataSorted.Rows)
                {
                    var item1 = (string)row["FormulaID"];
                    if (item1 != item2)
                        item2 = (string)row["FormulaID"];
                    else
                        row.Delete();
                }

                dataSorted.AcceptChanges();
                tbl = dataSorted;
            }
            catch
            {
            }

            return tbl;
        }
    }
}
