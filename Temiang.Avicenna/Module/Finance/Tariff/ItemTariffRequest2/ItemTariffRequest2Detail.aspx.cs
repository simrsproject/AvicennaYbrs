using System;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Tariff
{
    public partial class ItemTariffRequest2Detail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumberLast;

        private string getPageID
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ItemTariffRequest2Search.aspx?type=" + getPageID;
            UrlPageList = "ItemTariffRequest2List.aspx?type=" + getPageID;

            this.WindowSearch.Height = 400;

            ProgramID = getPageID == "" ? AppConstant.Program.ItemServiceTariffRequest2 :
                 (getPageID == "import" ? AppConstant.Program.ItemServiceTariffRequestImport : AppConstant.Program.ItemServiceTariffRequestImportNew);

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRTariffType, AppEnum.StandardReference.TariffType);

                //Custom Item Type
                cboSRItemType.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                cboSRItemType.Items.Add(new RadComboBoxItem("Service", BusinessObject.Reference.ItemType.Service));
                cboSRItemType.Items.Add(new RadComboBoxItem("Laboratory", BusinessObject.Reference.ItemType.Laboratory));
                cboSRItemType.Items.Add(new RadComboBoxItem("Radiology", BusinessObject.Reference.ItemType.Radiology));
                //cboSRItemType.Items.Add(new RadComboBoxItem("Diagnostic", BusinessObject.Reference.ItemType.Diagnostic));
                cboSRItemType.Items.Add(new RadComboBoxItem("Package", BusinessObject.Reference.ItemType.Package));

                if (getPageID == "")
                {
                    RadTabStrip1.Tabs[1].Visible = false;
                    trImportFromDate.Visible = false;
                    rfvImportFromDate.Visible = false;
                    rfvItemGroup.Visible = false;
                }
                else
                {
                    RadTabStrip1.Tabs[0].Visible = false;
                    RadMultiPage1.SelectedIndex = 1;
                    if (getPageID == "import")
                    {
                        trImportFromDate.Visible = true;
                        rfvImportFromDate.Visible = true;
                        rfvItemGroup.Visible = false;
                    }
                    else
                    {
                        trImportFromDate.Visible = false;
                        rfvImportFromDate.Visible = false;
                        rfvItemGroup.Visible = true;

                        grdImportResults.Columns[4].Visible = false;
                        grdImportResults.Columns[5].Visible = false;
                    }
                }
            }

            //PopUp Search
            if (!IsCallback)
            {
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.ItemService, Page);
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.ItemDiagnostic, Page);
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.ItemPackage, Page);
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.ItemLaboratory, Page);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (DataModeCurrent != AppEnum.DataMode.Read)
            {
                //Item type bisa dirubah bila item detail belum dipilih
                cboSRItemType.Enabled = ItemTariffRequest2Items.Count == 0;
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItemTariffRequestItem, grdItemTariffRequestItem);
            ajax.AddAjaxSetting(grdItemTariffRequestItem, cboSRItemType);
            ajax.AddAjaxSetting(grdItemTariffRequestItem, cboItemGroup);
            
            ajax.AddAjaxSetting(grdImportResults, grdImportResults);
            ajax.AddAjaxSetting(cboSRItemType, cboItemGroup);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            if (txtTariffRequestNo.Text.Trim() == string.Empty)
            {
                args.MessageText = AppConstant.Message.RecordCanNotEdited;
                args.IsCancel = true;
                return;
            }

            var entity = new ItemTariffRequest2();
            if (!entity.LoadByPrimaryKey(txtTariffRequestNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            if (entity.IsApproved != null && entity.IsApproved.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved + AppConstant.Message.RecordCanNotEdited;
                args.IsCancel = true;
                return;
            }
        }

        protected override void OnMenuEditClick()
        {
            cboSRItemType.Enabled = ItemTariffRequest2Items.Count == 0;
            cboItemGroup.Enabled = ItemTariffRequest2Items.Count == 0;
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ItemTariffRequest2());
            txtTariffRequestDate.SelectedDate = DateTime.Now;
            cboSRTariffType.SelectedValue = string.Empty;
            cboSRTariffType.Text = string.Empty;
            cboSRItemType.SelectedValue = string.Empty;
            cboSRItemType.Text = string.Empty;
            txtStartingDate.SelectedDate = DateTime.Now;
            txtImportFromDate.SelectedDate = DateTime.Now;
            chkIsImport.Checked = getPageID != "";
            chkIsNew.Checked = getPageID == "inew";

            PopulateNewRequestNo();
        }

        private void PopulateNewRequestNo()
        {
            _autoNumberLast = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.TariffRequestNo);
            txtTariffRequestNo.Text = _autoNumberLast.LastCompleteNumber;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ItemTariffRequest2();
            if (entity.LoadByPrimaryKey(txtTariffRequestNo.Text))
            {
                entity.MarkAsDeleted();

                var coll = new ItemTariffRequest2ItemCollection();
                string tariffRequestNo = txtTariffRequestNo.Text;
                coll.Query.Where(coll.Query.TariffRequestNo == tariffRequestNo);
                coll.LoadAll();
                coll.MarkAllAsDeleted();

                //ItemTariffComponent
                var collItemComp = new ItemTariffRequest2ItemCompCollection();
                collItemComp.Query.Where(collItemComp.Query.TariffRequestNo == txtTariffRequestNo.Text);
                collItemComp.LoadAll();
                collItemComp.MarkAllAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    coll.Save();
                    entity.Save();
                    collItemComp.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            PopulateNewRequestNo();
            var entity = new ItemTariffRequest2();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new ItemTariffRequest2();
            if (entity.LoadByPrimaryKey(txtTariffRequestNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                var entity = new ItemTariffRequest2();
                if (!entity.LoadByPrimaryKey(txtTariffRequestNo.Text))
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
                entity.IsApproved = true;
                entity.ApprovedDate = DateTime.Now.Date;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;

                if ((entity.IsImport ?? false) == false)
                {
                    //Insert to ItemTariff
                    var collItem = new ItemTariffRequest2ItemCollection();
                    string tariffRequestNo = txtTariffRequestNo.Text;
                    collItem.Query.Where(collItem.Query.TariffRequestNo == tariffRequestNo);
                    collItem.LoadAll();

                    foreach (ItemTariffRequest2Item item in collItem)
                    {
                        var isNotNew = false;

                        var tariffUpdateHistory = new ItemTariffUpdateHistory();
                        var tariff = new ItemTariff();
                        if (tariff.LoadByPrimaryKey(entity.SRTariffType, item.ItemID, item.ClassID,
                                                    entity.StartingDate.Value.Date))
                        {
                            isNotNew = true;

                            tariffUpdateHistory.AddNew();
                            tariffUpdateHistory.RequestNo = tariffRequestNo;
                            tariffUpdateHistory.SRTariffType = entity.SRTariffType;
                            tariffUpdateHistory.ItemID = item.ItemID.Trim();
                            tariffUpdateHistory.ClassID = item.ClassID;
                            tariffUpdateHistory.StartingDate = entity.StartingDate;
                            tariffUpdateHistory.Price = tariff.Price;
                            tariffUpdateHistory.ToPrice = item.Price;
                            tariffUpdateHistory.DiscPercentage = tariff.DiscPercentage;
                            tariffUpdateHistory.ToDiscPercentage = item.DiscPercentage;

                            tariffUpdateHistory.IsAllowDiscount = tariff.IsAllowDiscount;
                            tariffUpdateHistory.IsAllowVariable = tariff.IsAllowVariable;
                            tariffUpdateHistory.IsAllowCito = tariff.IsAllowCito;
                            tariffUpdateHistory.IsCitoInPercent = tariff.IsCitoInPercent;
                            tariffUpdateHistory.CitoValue = tariff.CitoValue;
                            tariffUpdateHistory.IsCitoFromStandardReference = tariff.IsCitoFromStandardReference;

                            tariffUpdateHistory.LastUpdateDateTime = DateTime.Now;
                            tariffUpdateHistory.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        }
                        else
                            tariff.AddNew();

                        tariff.SRTariffType = entity.SRTariffType;
                        tariff.ItemID = item.ItemID;
                        tariff.ClassID = item.ClassID;
                        tariff.StartingDate = entity.StartingDate;
                        tariff.Price = item.Price;
                        tariff.ReferenceNo = txtTariffRequestNo.Text;
                        tariff.ReferenceTransactionCode = BusinessObject.Reference.TransactionCode.ItemTariffRequest;

                        var i = new Item();
                        i.LoadByPrimaryKey(item.ItemID);
                        switch (i.SRItemType)
                        {
                            case BusinessObject.Reference.ItemType.Service:
                                var s = new ItemService();
                                s.LoadByPrimaryKey(i.ItemID);
                                tariff.IsAllowDiscount = s.IsAllowDiscount;
                                tariff.IsAllowVariable = s.IsAllowVariable;
                                tariff.IsAllowCito = s.IsAllowCito;
                                tariff.IsAdminCalculation = s.IsAdminCalculation;
                                tariff.IsCitoFromStandardReference = s.IsCitoFromStandardReference;
                                if (isNotNew)
                                {
                                    tariffUpdateHistory.ToIsAllowDiscount = s.IsAllowDiscount;
                                    tariffUpdateHistory.ToIsAllowVariable = s.IsAllowVariable;
                                    tariffUpdateHistory.ToIsAllowCito = s.IsAllowCito;
                                    tariffUpdateHistory.ToIsCitoFromStandardReference = s.IsCitoFromStandardReference;
                                }
                                break;
                            case BusinessObject.Reference.ItemType.Diagnostic:
                                var d = new ItemDiagnostic();
                                d.LoadByPrimaryKey(i.ItemID);
                                tariff.IsAllowDiscount = d.IsAllowDiscount;
                                tariff.IsAllowVariable = d.IsAllowVariable;
                                tariff.IsAllowCito = d.IsAllowCito;
                                tariff.IsAdminCalculation = d.IsAdminCalculation;
                                tariff.IsCitoFromStandardReference = false;
                                if (isNotNew)
                                {
                                    tariffUpdateHistory.ToIsAllowDiscount = d.IsAllowDiscount;
                                    tariffUpdateHistory.ToIsAllowVariable = d.IsAllowVariable;
                                    tariffUpdateHistory.ToIsAllowCito = d.IsAllowCito;
                                    tariffUpdateHistory.ToIsCitoFromStandardReference = false;
                                }
                                break;
                            case BusinessObject.Reference.ItemType.Laboratory:
                                var l = new ItemLaboratory();
                                l.LoadByPrimaryKey(i.ItemID);
                                tariff.IsAllowDiscount = l.IsAllowDiscount;
                                tariff.IsAllowVariable = l.IsAllowVariable;
                                tariff.IsAllowCito = l.IsAllowCito;
                                tariff.IsAdminCalculation = l.IsAdminCalculation;
                                tariff.IsCitoFromStandardReference = l.IsCitoFromStandardReference;
                                if (isNotNew)
                                {
                                    tariffUpdateHistory.ToIsAllowDiscount = l.IsAllowDiscount;
                                    tariffUpdateHistory.ToIsAllowVariable = l.IsAllowVariable;
                                    tariffUpdateHistory.ToIsAllowCito = l.IsAllowCito;
                                    tariffUpdateHistory.ToIsCitoFromStandardReference = l.IsCitoFromStandardReference;
                                }
                                break;
                            case BusinessObject.Reference.ItemType.Package:
                                tariff.IsAllowDiscount = false;
                                tariff.IsAllowVariable = false;
                                tariff.IsAllowCito = false;
                                tariff.IsAdminCalculation = true;
                                tariff.IsCitoFromStandardReference = false;
                                if (isNotNew)
                                {
                                    tariffUpdateHistory.ToIsAllowDiscount = false;
                                    tariffUpdateHistory.ToIsAllowVariable = false;
                                    tariffUpdateHistory.ToIsAllowCito = false;
                                    tariffUpdateHistory.ToIsCitoFromStandardReference = false;
                                }
                                break;
                            case BusinessObject.Reference.ItemType.Radiology:
                                var r = new ItemRadiology();
                                r.LoadByPrimaryKey(i.ItemID);
                                tariff.IsAllowDiscount = r.IsAllowDiscount;
                                tariff.IsAllowVariable = r.IsAllowVariable;
                                tariff.IsAllowCito = r.IsAllowCito;
                                tariff.IsAdminCalculation = r.IsAdminCalculation;
                                tariff.IsCitoFromStandardReference = r.IsCitoFromStandardReference;
                                if (isNotNew)
                                {
                                    tariffUpdateHistory.ToIsAllowDiscount = r.IsAllowDiscount;
                                    tariffUpdateHistory.ToIsAllowVariable = r.IsAllowVariable;
                                    tariffUpdateHistory.ToIsAllowCito = r.IsAllowCito;
                                    tariffUpdateHistory.ToIsCitoFromStandardReference = r.IsCitoFromStandardReference;
                                }
                                break;
                        }
                        tariff.IsCitoInPercent = tariff.IsAllowCito == true ? item.IsCitoInPercent : false;
                        tariff.CitoValue = tariff.IsAllowCito == true ? item.CitoValue : 0;
                        if (isNotNew)
                        {
                            tariffUpdateHistory.ToIsCitoInPercent = tariff.IsCitoInPercent;
                            tariffUpdateHistory.ToCitoValue = tariff.CitoValue;
                        }

                        var isCompAllowVariable = false;
                        var isCompAllowDiscount = false;

                        //ItemTariffComponent
                        var collItemComp = new ItemTariffRequest2ItemCompCollection();
                        collItemComp.Query.Where(collItemComp.Query.TariffRequestNo == txtTariffRequestNo.Text, collItemComp.Query.ItemID == item.ItemID, collItemComp.Query.ClassID == item.ClassID);
                        collItemComp.LoadAll();

                        var compQty = 0;
                        var compDelQty = 0;

                        foreach (ItemTariffRequest2ItemComp itemComp in collItemComp)
                        {
                            compQty++;
                            var isDeleted = (itemComp.Price == 0 && itemComp.IsAllowVariable == false);
                            var tariffCompUpdateHistory = new ItemTariffComponentUpdateHistory();
                            var tariffComp = new ItemTariffComponent();
                            if (tariffComp.LoadByPrimaryKey(entity.SRTariffType, itemComp.ItemID, itemComp.ClassID,
                                                        entity.StartingDate.Value.Date, itemComp.TariffComponentID))
                            {
                                tariffCompUpdateHistory.AddNew();
                                tariffCompUpdateHistory.RequestNo = tariffRequestNo;
                                tariffCompUpdateHistory.SRTariffType = entity.SRTariffType;
                                tariffCompUpdateHistory.ItemID = itemComp.ItemID.Trim();
                                tariffCompUpdateHistory.ClassID = itemComp.ClassID;
                                tariffCompUpdateHistory.StartingDate = entity.StartingDate;
                                tariffCompUpdateHistory.TariffComponentID = itemComp.TariffComponentID;
                                tariffCompUpdateHistory.Price = tariffComp.Price;
                                tariffCompUpdateHistory.ToPrice = itemComp.Price;
                                tariffCompUpdateHistory.IsAllowDiscount = tariffComp.IsAllowDiscount;
                                tariffCompUpdateHistory.ToIsAllowDiscount = itemComp.IsAllowDiscount;
                                tariffCompUpdateHistory.IsAllowVariable = tariffComp.IsAllowVariable;
                                tariffCompUpdateHistory.ToIsAllowVariable = itemComp.IsAllowVariable;

                                tariffCompUpdateHistory.LastUpdateDateTime = DateTime.Now;
                                tariffCompUpdateHistory.LastUpdateByUserID = AppSession.UserLogin.UserID;

                                tariffCompUpdateHistory.Save();

                                if (isDeleted)
                                {
                                    compDelQty++;
                                    tariffComp.MarkAsDeleted();
                                    tariffComp.Save();
                                }
                                else
                                {
                                    tariffComp.SRTariffType = entity.SRTariffType;
                                    tariffComp.ItemID = itemComp.ItemID;
                                    tariffComp.ClassID = itemComp.ClassID;
                                    tariffComp.StartingDate = entity.StartingDate;
                                    tariffComp.TariffComponentID = itemComp.TariffComponentID;
                                    tariffComp.Price = itemComp.Price;
                                    tariffComp.IsAllowDiscount = itemComp.IsAllowDiscount;
                                    tariffComp.IsAllowVariable = itemComp.IsAllowVariable;
                                    tariffComp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    tariffComp.LastUpdateDateTime = DateTime.Now;
                                    tariffComp.ReferenceNo = txtTariffRequestNo.Text;

                                    if (tariffComp.IsAllowDiscount ?? false)
                                        isCompAllowDiscount = true;
                                    if (tariffComp.IsAllowVariable ?? false)
                                        isCompAllowVariable = true;

                                    //--save
                                    tariffComp.Save();
                                }
                            }
                            else
                            {
                                if (!isDeleted)
                                {
                                    tariffComp.AddNew();
                                    tariffComp.SRTariffType = entity.SRTariffType;
                                    tariffComp.ItemID = itemComp.ItemID;
                                    tariffComp.ClassID = itemComp.ClassID;
                                    tariffComp.StartingDate = entity.StartingDate;
                                    tariffComp.TariffComponentID = itemComp.TariffComponentID;
                                    tariffComp.Price = itemComp.Price;
                                    tariffComp.IsAllowDiscount = itemComp.IsAllowDiscount;
                                    tariffComp.IsAllowVariable = itemComp.IsAllowVariable;
                                    tariffComp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    tariffComp.LastUpdateDateTime = DateTime.Now;
                                    tariffComp.ReferenceNo = txtTariffRequestNo.Text;

                                    if (tariffComp.IsAllowDiscount ?? false)
                                        isCompAllowDiscount = true;
                                    if (tariffComp.IsAllowVariable ?? false)
                                        isCompAllowVariable = true;

                                    //--save
                                    tariffComp.Save();
                                }
                            }
                        }

                        tariff.IsAllowDiscount = isCompAllowDiscount;
                        tariff.IsAllowVariable = isCompAllowVariable;
                        tariff.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        tariff.LastUpdateDateTime = DateTime.Now;

                        if (compQty - compDelQty > 0)
                            tariff.Save();

                        if (isNotNew)
                            tariffUpdateHistory.Save();
                    }
                }
                else
                {
                    //Import
                    if (entity.IsNew == false)
                        ItemTariff.InsertFromImport(entity.TariffRequestNo, AppSession.UserLogin.UserID);
                    else
                        ItemTariff.InsertFromImportNew(entity.TariffRequestNo, AppSession.UserLogin.UserID);
                }

                entity.Save();

                trans.Complete();

                txtApprovedDate.Text = entity.ApprovedDate.Value.ToString(AppConstant.DisplayFormat.Date);
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new ItemTariffRequest2();
            if (!entity.LoadByPrimaryKey(txtTariffRequestNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (!(entity.IsApproved ?? false))
            {
                args.MessageText = AppConstant.Message.RecordHasNotApproved;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }
            if (entity.StartingDate.Value.Date < DateTime.Now.Date)
            {
                args.MessageText = "Tariff changes have taken effect. Un-approved is not allowed.";
                args.IsCancel = true;
                return;
            }

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.IsApproved = false;
                entity.IsVoid = true;
                entity.VoidDate = DateTime.Now;
                entity.VoidByUserID = AppSession.UserLogin.UserID;

                var tariffComps = new ItemTariffComponentCollection();
                tariffComps.Query.Where(tariffComps.Query.ReferenceNo == entity.TariffRequestNo);
                tariffComps.LoadAll();
                tariffComps.MarkAllAsDeleted();

                var tariffs = new ItemTariffCollection();
                tariffs.Query.Where(tariffs.Query.ReferenceNo == entity.TariffRequestNo);
                tariffs.LoadAll();
                tariffs.MarkAllAsDeleted();

                tariffComps.Save();
                tariffs.Save();
                entity.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new ItemTariffRequest2();
            if (!entity.LoadByPrimaryKey(txtTariffRequestNo.Text))
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
            entity.IsVoid = true;
            entity.VoidDate = DateTime.Now.Date;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                trans.Complete();
            }
            txtVoidDate.SelectedDate = entity.VoidDate;
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
            auditLogFilter.PrimaryKeyData = string.Format("TariffRequestNo='{0}'", txtTariffRequestNo.Text.Trim());
            auditLogFilter.TableName = "ItemTariffRequest2";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemGrid(newVal);
            txtFilterItemID.ReadOnly = false;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ItemTariffRequest2();
            if (parameters.Length > 0)
            {
                String tariffRequestNo = parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(tariffRequestNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtTariffRequestNo.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var itemTariffRequest = (ItemTariffRequest2)entity;
            txtTariffRequestNo.Text = itemTariffRequest.TariffRequestNo;
            txtTariffRequestDate.SelectedDate = itemTariffRequest.TariffRequestDate;
            cboSRTariffType.SelectedValue = itemTariffRequest.SRTariffType;
            cboSRItemType.SelectedValue = itemTariffRequest.SRItemType;
            if (!string.IsNullOrEmpty(itemTariffRequest.ItemGroupID))
            {
                var ig = new ItemGroupQuery();
                ig.Where(ig.ItemGroupID == itemTariffRequest.ItemGroupID);
                cboItemGroup.DataSource = ig.LoadDataTable();
                cboItemGroup.DataBind();
                cboItemGroup.SelectedValue = itemTariffRequest.ItemGroupID;
            }
            else
            {
                cboItemGroup.Items.Clear();
                cboItemGroup.SelectedValue = string.Empty;
                cboItemGroup.Text = string.Empty;
            }

            txtStartingDate.SelectedDate = itemTariffRequest.StartingDate;
            chkIsApproved.Checked = itemTariffRequest.IsApproved ?? false;
            txtApprovedDate.Text = itemTariffRequest.ApprovedDate == null
                                       ? string.Empty
                                       : itemTariffRequest.ApprovedDate.Value.ToString(AppConstant.DisplayFormat.Date);

            chkIsVoid.Checked = itemTariffRequest.IsVoid ?? false;
            txtVoidDate.SelectedDate = itemTariffRequest.VoidDate;

            txtNotes.Text = itemTariffRequest.Notes;
            chkIsImport.Checked = itemTariffRequest.IsImport ?? false;
            txtImportFromDate.SelectedDate = itemTariffRequest.ImportFromDate;
            chkIsNew.Checked = itemTariffRequest.IsNew ?? false;

            //Display Data Detail
            PopulateGridDetail();

            //Reset supaya reload waktu dipaakai
            ItemTariffRequest2ItemComps = null;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !chkIsApproved.Checked;
        }
        public override bool OnGetStatusMenuVoid()
        {
            return !chkIsVoid.Checked;
        }
        public override bool OnGetStatusMenuEdit()
        {
            return txtTariffRequestNo.Text != string.Empty;
        }
        #endregion

        #region Private Method Standard

        private void SetEntityValue(esItemTariffRequest2 entity)
        {
            entity.TariffRequestNo = txtTariffRequestNo.Text;
            entity.TariffRequestDate = DateTime.Now; //txtTariffRequestDate.SelectedDate;
            entity.SRTariffType = cboSRTariffType.SelectedValue;
            entity.SRItemType = cboSRItemType.SelectedValue;
            entity.StartingDate = txtStartingDate.SelectedDate;
            entity.Notes = txtNotes.Text;
            entity.IsApproved = false;
            entity.IsImport = chkIsImport.Checked;
            entity.ItemGroupID = cboItemGroup.SelectedValue;
            if (getPageID != "import")
                entity.ImportFromDate = null;
            else
                entity.ImportFromDate = txtImportFromDate.SelectedDate;
            entity.IsNew = chkIsNew.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //Detail Item
            ItemTariffRequest2ItemCollection coll = ItemTariffRequest2Items;
            foreach (ItemTariffRequest2Item item in coll)
            {
                item.TariffRequestNo = txtTariffRequestNo.Text;
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }

            ItemTariffRequest2ItemCompCollection coll2 = ItemTariffRequest2ItemComps;
            foreach (ItemTariffRequest2ItemComp item in coll2)
            {
                #region old
                //if (item.Price == 0 && item.IsAllowVariable == false)
                //    item.MarkAsDeleted();
                //else
                //{
                //    item.TariffRequestNo = txtTariffRequestNo.Text;
                //    //Last Update Status
                //    if (item.es.IsAdded || item.es.IsModified)
                //    {
                //        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                //        item.LastUpdateDateTime = DateTime.Now;
                //    }
                //}
                #endregion

                //-- semua data yg di request tetap disimpan, nanti validasi pada saat approved
                item.TariffRequestNo = txtTariffRequestNo.Text;
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void SaveEntity(esEntity entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                ItemTariffRequest2Items.Save();
                ItemTariffRequest2ItemComps.Save();
                //AutoNumberLast
                if (DataModeCurrent == AppEnum.DataMode.New)
                    _autoNumberLast.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ItemTariffRequest2Query();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.TariffRequestNo > txtTariffRequestNo.Text);
                que.OrderBy(que.TariffRequestNo.Ascending);
            }
            else
            {
                que.Where(que.TariffRequestNo < txtTariffRequestNo.Text);
                que.OrderBy(que.TariffRequestNo.Descending);
            }
            if (getPageID == "")
                que.Where(que.IsImport == false);
            else if (getPageID == "import")
                que.Where(que.IsImport == true, que.IsNew == false);
            else que.Where(que.IsImport == true, que.IsNew == true);

            var entity = new ItemTariffRequest2();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function
        private ItemTariffRequest2ItemCompCollection ItemTariffRequest2ItemComps
        {
            get
            {
                object obj = Session["ItemTariffRequest2ItemComps" + Request.UserHostName];
                if (obj != null)
                {
                    return ((ItemTariffRequest2ItemCompCollection)(obj));
                }
                var qa = new ItemTariffRequest2ItemCompQuery("a");
                var qb = new TariffComponentQuery("b");

                qa.InnerJoin(qb).On(qa.TariffComponentID == qb.TariffComponentID);
                qa.Select(qa.SelectAllExcept(), qb.TariffComponentName.As("refToTariffComponent_TariffComponentName"));
                qa.Where(qa.TariffRequestNo == txtTariffRequestNo.Text);
                var coll = new ItemTariffRequest2ItemCompCollection();
                coll.Load(qa);
                Session["ItemTariffRequest2ItemComps" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["ItemTariffRequest2ItemComps" + Request.UserHostName] = value; }
        }
        private ItemTariffRequest2ItemCollection ItemTariffRequest2Items
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemTariffRequest2Item" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((ItemTariffRequest2ItemCollection)(obj));
                    }
                }

                var coll = new ItemTariffRequest2ItemCollection();
                var query = new ItemTariffRequest2ItemQuery("a");
                var itemQuery = new ItemQuery("b");
                var classQuery = new ClassQuery("c");
                query.InnerJoin(itemQuery).On(query.ItemID == itemQuery.ItemID);
                query.InnerJoin(classQuery).On(query.ClassID == classQuery.ClassID);
                query.Select(query.TariffRequestNo,
                             query.ClassID,
                             query.ItemID,
                             query.Price,
                             query.IsCitoInPercent,
                             query.CitoValue,
                             query.IsCitoFromStandardReference,
                             query.LastUpdateDateTime,
                             query.LastUpdateByUserID,
                             itemQuery.ItemName.As("refToItem_ItemName"),
                             classQuery.ClassName.As("refToClass_ClassName"));
                string tariffRequestNo = txtTariffRequestNo.Text;

                query.Where(query.TariffRequestNo == tariffRequestNo);
                query.OrderBy(query.ItemID.Ascending, query.ClassID.Ascending);
                coll.Load(query);

                Session["collItemTariffRequest2Item" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collItemTariffRequest2Item" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemTariffRequestItem.Columns[0].Visible = isVisible;
            grdItemTariffRequestItem.Columns[grdItemTariffRequestItem.Columns.Count - 1].Visible = isVisible;
            grdItemTariffRequestItem.Columns[grdItemTariffRequestItem.Columns.Count - 2].Visible = !isVisible;

            grdItemTariffRequestItem.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            grdItemTariffRequestItem.Rebind();
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            ItemTariffRequest2Items = null; //Reset Record Detail
            ItemTariffRequest2ItemComps = null;

            grdItemTariffRequestItem.DataSource = ItemTariffRequest2Items;
            ItemTariffRequest2ItemCompCollection comp = ItemTariffRequest2ItemComps;

            grdItemTariffRequestItem.MasterTableView.IsItemInserted = false;
            grdItemTariffRequestItem.MasterTableView.ClearEditItems();
            grdItemTariffRequestItem.DataBind();

            ImportResults = null; //Reset Record Detail
            grdImportResults.DataSource = ImportResults;
            grdImportResults.DataBind();
        }

        protected void grdItemTariffRequestItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemTariffRequestItem.DataSource = ItemTariffRequest2Items;
            ItemTariffRequest2ItemCompCollection comp = ItemTariffRequest2ItemComps;
        }

        protected void grdItemTariffRequestItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemID =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        ItemTariffRequest2ItemMetadata.ColumnNames.ItemID]);
            String classID =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        ItemTariffRequest2ItemMetadata.ColumnNames.ClassID]);
            ItemTariffRequest2Item entity = FindItemTariffRequestItem(classID, itemID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItemTariffRequestItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String itemID =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemTariffRequest2ItemMetadata.ColumnNames.ItemID]);
            String classID =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemTariffRequest2ItemMetadata.ColumnNames.ClassID]);
            ItemTariffRequest2Item entity = FindItemTariffRequestItem(classID, itemID);
            if (entity != null)
                entity.MarkAsDeleted();

            //Remove in Component Tariff
            ItemTariffRequest2ItemCompCollection compCollection = ItemTariffRequest2ItemComps;
            foreach (ItemTariffRequest2ItemComp comp in compCollection)
            {
                if (comp.ItemID.Equals(itemID) & comp.ClassID.Equals(classID))
                    comp.MarkAsDeleted();
            }
        }

        protected void grdItemTariffRequestItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            ItemTariffRequest2Item entity = ItemTariffRequest2Items.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItemTariffRequestItem.Rebind();
        }

        private ItemTariffRequest2Item FindItemTariffRequestItem(String classID, String itemID)
        {
            ItemTariffRequest2ItemCollection coll = ItemTariffRequest2Items;
            ItemTariffRequest2Item retEntity = null;
            foreach (ItemTariffRequest2Item rec in coll)
            {
                if (!(rec.ItemID.Equals(itemID) & rec.ClassID.Equals(classID))) continue;
                retEntity = rec;
                break;
            }
            return retEntity;
        }

        private void SetEntityValue(ItemTariffRequest2Item entity, GridCommandEventArgs e)
        {
            var userControl =
                (ItemTariffRequest2ItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl == null) return;
            entity.ClassID = userControl.ClassID;
            entity.ClassName = userControl.ClassName;
            entity.ItemID = userControl.ItemID;
            entity.ItemName = userControl.ItemName;
            entity.CitoValue = userControl.CitoValue;
            entity.IsCitoInPercent = userControl.IsCitoInPercent;
            entity.IsCitoFromStandardReference = userControl.IsCitoFromStandardReference;

            decimal price = 0;
            foreach (GridDataItem dataItem in userControl.GridDataItemCollection)
            {
                RadNumericTextBox txtComponentPrice = (dataItem["Price"].FindControl("txtComponentPrice") as RadNumericTextBox);
                CheckBox chkIsAllowDiscount = (dataItem["IsAllowDiscount"].FindControl("chkIsAllowDiscount") as CheckBox);
                CheckBox chkIsAllowVariable = (dataItem["IsAllowVariable"].FindControl("chkIsAllowVariable") as CheckBox);

                string tariffComponentID = dataItem.GetDataKeyValue("TariffComponentID").ToString();
                ItemTariffRequest2ItemCompCollection coll = ItemTariffRequest2ItemComps;
                ItemTariffRequest2ItemComp itemComp = GetItemTariffRequest2ItemComp(entity.ClassID, entity.ItemID, tariffComponentID);
                if (itemComp == null)
                    itemComp = ItemTariffRequest2ItemComps.AddNew();

                itemComp.ClassID = entity.ClassID;
                itemComp.ItemID = entity.ItemID;
                itemComp.TariffComponentID = tariffComponentID;
                itemComp.Price = (decimal?)(txtComponentPrice.Value);
                itemComp.IsAllowDiscount = chkIsAllowDiscount.Checked;
                itemComp.IsAllowVariable = chkIsAllowVariable.Checked;

                price += itemComp.Price ?? 0;
            }
            //Update jika proce detil per komponen
            entity.Price = price;
        }
        private ItemTariffRequest2ItemComp GetItemTariffRequest2ItemComp(string classID, string itemID, string tariffComponentID)
        {
            ItemTariffRequest2ItemCompCollection coll = ItemTariffRequest2ItemComps;
            foreach (ItemTariffRequest2ItemComp itemComp in coll)
            {
                if (itemComp.ClassID.Equals(classID) && itemComp.ItemID.Equals(itemID) && itemComp.TariffComponentID.Equals(tariffComponentID))
                {
                    return itemComp;
                }
            }
            return null;
        }

        #endregion

        #region Record Detail Method Function - Import Results

        private ItemTariffRequestItemToImportCollection ImportResults
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemTariffRequestItemToImport"];
                    if (obj != null)
                        return ((ItemTariffRequestItemToImportCollection)(obj));
                }

                var coll = new ItemTariffRequestItemToImportCollection();
                var query = new ItemTariffRequestItemToImportQuery("a");
                var cq = new ClassQuery("c");
                var tcq = new TariffComponentQuery("d");

                query.Select
                    (
                        query,
                        cq.ClassName.As("refToClass_ItemName"),
                        tcq.TariffComponentName.As("refToTariffComponent_TariffComponentName")
                        );
                query.InnerJoin(cq).On(cq.ClassID == query.ClassID);
                query.InnerJoin(tcq).On(tcq.TariffComponentID == query.TariffComponentID);
                query.Where(query.ReferenceNo == txtTariffRequestNo.Text);

                query.OrderBy(query.ItemID.Ascending, query.ClassID.Ascending, query.TariffComponentID.Ascending);

                coll.Load(query);

                Session["collItemTariffRequestItemToImport"] = coll;
                return coll;
            }
            set { Session["collItemTariffRequestItemToImport"] = value; }
        }


        protected void grdImportResults_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (txtFilterItemID.Text.Trim() != string.Empty)
            {
                var ds = from d in ImportResults
                         where d.ItemName.ToLower().Contains(txtFilterItemID.Text.ToLower()) || d.ItemID.ToLower().Contains(txtFilterItemID.Text.ToLower())
                         select d;
                grdImportResults.DataSource = ds;
            }
            else
            {
                grdImportResults.DataSource = ImportResults;
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdImportResults.CurrentPageIndex = 0;
            grdImportResults.Rebind();
        }
        #endregion

        #region Combox
        protected void cboSRItemType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboItemGroup.Items.Clear();
            cboItemGroup.Text = string.Empty;
        }

        protected void cboItemGroup_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ItemGroupItemsRequested((RadComboBox)sender, e.Text, cboSRItemType.SelectedValue);
        }

        protected void cboItemGroup_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ItemGroupItemDataBound(e);
        }
        #endregion
    }
}
