using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Finance.Tariff
{
    public partial class ItemTariffRequestProcessDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumberLast;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ItemTariffRequestProcessSearch.aspx";
            UrlPageList = "ItemTariffRequestProcessList.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.ItemTariffRequestProcess;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboFromSRTariffType, AppEnum.StandardReference.TariffType);
                StandardReference.InitializeIncludeSpace(cboToSRTariffType, AppEnum.StandardReference.TariffType);

                //Custom Item Type
                cboSRItemType.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                cboSRItemType.Items.Add(new RadComboBoxItem("Service", BusinessObject.Reference.ItemType.Service));
                cboSRItemType.Items.Add(new RadComboBoxItem("Laboratory", BusinessObject.Reference.ItemType.Laboratory));
                cboSRItemType.Items.Add(new RadComboBoxItem("Radiology", BusinessObject.Reference.ItemType.Radiology));
                cboSRItemType.Items.Add(new RadComboBoxItem("Package", BusinessObject.Reference.ItemType.Package));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(cboSRItemType, cboSRItemType);
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

            var entity = new ItemTariffRequestProcess();
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

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ItemTariffRequestProcess());
            txtTariffRequestDate.SelectedDate = DateTime.Now;
            txtStartingDate.SelectedDate = DateTime.Now;
            txtFromDate.SelectedDate = DateTime.Now;

            PopulateNewRequestNo();
        }

        protected override void OnMenuEditClick()
        {
            PopulateGridDetail();
        }

        private void PopulateNewRequestNo()
        {
            _autoNumberLast = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.TariffRequestProcessNo);
            txtTariffRequestNo.Text = _autoNumberLast.LastCompleteNumber;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ItemTariffRequestProcess();
            if (entity.LoadByPrimaryKey(txtTariffRequestNo.Text))
            {
                entity.MarkAsDeleted();

                var coll = new ItemTariffRequestProcessItemCompCollection();
                string tariffRequestNo = txtTariffRequestNo.Text;
                coll.Query.Where(coll.Query.TariffRequestNo == tariffRequestNo);
                coll.LoadAll();
                coll.MarkAllAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    coll.Save();
                    entity.Save();
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
            var entity = new ItemTariffRequestProcess();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new ItemTariffRequestProcess();
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
            var entity = new ItemTariffRequestProcess();
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

            using (esTransactionScope trans = new esTransactionScope())
            {
                ItemTariff.InsertFromProcess(entity.TariffRequestNo, AppSession.UserLogin.UserID);

                entity.Save();
                trans.Complete();
            }

            txtApprovedDate.Text = entity.ApprovedDate.Value.ToString(AppConstant.DisplayFormat.Date);
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new ItemTariffRequestProcess();
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
                ItemTariff.InsertFromImport(entity.TariffRequestNo, AppSession.UserLogin.UserID);

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
            auditLogFilter.TableName = "ItemTariffRequestProcess";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemGrid(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ItemTariffRequestProcess();
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
            var itemTariffRequest = (ItemTariffRequestProcess)entity;
            txtTariffRequestNo.Text = itemTariffRequest.TariffRequestNo;
            txtTariffRequestDate.SelectedDate = itemTariffRequest.TariffRequestDate;
            cboFromSRTariffType.SelectedValue = itemTariffRequest.FromSRTariffType;
            cboToSRTariffType.SelectedValue = itemTariffRequest.ToSRTariffType;
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
            txtFromDate.SelectedDate = itemTariffRequest.FromDate;
            txtRoundingValue.Value = Convert.ToDouble(itemTariffRequest.RoundingValue);
            chkIsRoundingDown.Checked = itemTariffRequest.IsRoundingDown ?? false;
            chkIsApproved.Checked = itemTariffRequest.IsApproved ?? false;
            txtApprovedDate.Text = itemTariffRequest.ApprovedDate == null
                                       ? string.Empty
                                       : itemTariffRequest.ApprovedDate.Value.ToString(AppConstant.DisplayFormat.Date);

            chkIsVoid.Checked = itemTariffRequest.IsVoid ?? false;
            txtVoidDate.SelectedDate = itemTariffRequest.VoidDate;

            txtNotes.Text = itemTariffRequest.Notes;

            //Display Data Detail
            PopulateGridDetail();
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

        private void SetEntityValue(esItemTariffRequestProcess entity)
        {
            entity.TariffRequestNo = txtTariffRequestNo.Text;
            entity.TariffRequestDate = DateTime.Now; 
            entity.FromSRTariffType = cboFromSRTariffType.SelectedValue;
            entity.ToSRTariffType = cboToSRTariffType.SelectedValue;
            entity.SRItemType = cboSRItemType.SelectedValue;
            entity.ItemGroupID = cboItemGroup.SelectedValue;
            entity.StartingDate = txtStartingDate.SelectedDate;
            entity.Notes = txtNotes.Text;
            entity.FromDate = txtFromDate.SelectedDate;
            entity.RoundingValue = Convert.ToDecimal(txtRoundingValue.Value);
            entity.IsRoundingDown = chkIsRoundingDown.Checked;
            entity.IsApproved = false;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(ItemTariffRequestProcess entity)
        {
            var itemComps = new ItemTariffRequestProcessItemCompCollection();
            itemComps.Query.Where(itemComps.Query.TariffRequestNo == entity.TariffRequestNo);
            itemComps.LoadAll();

            foreach (GridDataItem dataItem in grdItemComp.MasterTableView.Items)
            {
                string tariffCompId = dataItem.GetDataKeyValue("TariffComponentID").ToString();
                double amountValue = ((RadNumericTextBox)dataItem.FindControl("txtAmountValue")).Value ?? 0;
                bool isValueInPercent = ((CheckBox)dataItem.FindControl("chkIsValueInPercent")).Checked;

                bool isExist = false;
                foreach (ItemTariffRequestProcessItemComp row in itemComps)
                {
                    if (row.TariffComponentID.Equals(tariffCompId))
                    {
                        isExist = true;
                        row.AmountValue = Convert.ToDecimal(amountValue);
                        row.IsValueInPercent = isValueInPercent;
                        row.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        row.LastUpdateDateTime = DateTime.Now;
                        break;
                    }
                }
                //Add
                if (!isExist)
                {
                    ItemTariffRequestProcessItemComp row = itemComps.AddNew();
                    row.TariffRequestNo = entity.TariffRequestNo;
                    row.TariffComponentID = tariffCompId;
                    row.AmountValue = Convert.ToDecimal(amountValue);
                    row.IsValueInPercent = isValueInPercent;
                    row.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    row.LastUpdateDateTime = DateTime.Now;
                }
            }

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                itemComps.Save();
                
                //AutoNumberLast
                if (DataModeCurrent == AppEnum.DataMode.New)
                    _autoNumberLast.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ItemTariffRequestProcessQuery();
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
            var entity = new ItemTariffRequestProcess();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function
        private void PopulateGridDetail()
        {
            //Display Data Detail
            grdItemComp.DataSource = GetItemTariffRequestProcessItemComps();
            grdItemComp.DataBind();
        }

        protected void grdItemComp_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemComp.DataSource = GetItemTariffRequestProcessItemComps();
        }

        private DataTable GetItemTariffRequestProcessItemComps()
        {
            var query = new ItemTariffRequestProcessItemCompQuery("a");
            var qrComp = new TariffComponentQuery("b");
            if (this.DataModeCurrent == AppEnum.DataMode.Read)
            {
                query.InnerJoin(qrComp).On(query.TariffComponentID == qrComp.TariffComponentID);
                query.Where(query.TariffRequestNo == txtTariffRequestNo.Text);
            }
            else
            {
                query.RightJoin(qrComp).On(query.TariffComponentID == qrComp.TariffComponentID & query.TariffRequestNo == txtTariffRequestNo.Text);
            }
            query.OrderBy(qrComp.TariffComponentID.Ascending);
            query.Select
            (
                qrComp.TariffComponentID.As("TariffComponentID"),
                qrComp.TariffComponentName.As("TariffComponentName"),
                "<ISNULL(a.AmountValue, 0) as AmountValue>",
                "<ISNULL(a.IsValueInPercent, 1) as IsValueInPercent>"
            );
            DataTable dtb = query.LoadDataTable();
            return dtb;
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            //grdItemComp.Columns[0].Visible = isVisible;

            //Perbaharui tampilan dan data
            grdItemComp.Rebind();
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