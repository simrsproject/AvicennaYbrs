using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator.v2
{
    public partial class RiskManagementDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 300;
            // Url Search & List
            UrlPageSearch = "RiskManagementSearch.aspx";
            UrlPageList = "RiskManagementList.aspx";

            ProgramID = AppConstant.Program.RiskManagement;

            //StandardReference Initialize
            if (!IsPostBack)
            {

            }
        }

        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new RiskManagement());
            PopulateNewNo();
            txtPeriodYear.Value = Convert.ToDouble(DateTime.Now.Year);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new RiskManagement();
            if (entity.LoadByPrimaryKey(txtRiskManagementNo.Text))
            {
                if (!IsApproved(entity, args))
                    return;

                entity.MarkAsDeleted();
                RiskManagementItems.MarkAllAsDeleted();

                SaveEntity(entity);
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                args.MessageText = "Service Unit required.";
                args.IsCancel = true;
                return;
            }

            var entity = new RiskManagement();
            entity.AddNew();
            PopulateNewNo();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                args.MessageText = "Service Unit required.";
                args.IsCancel = true;
                return;
            }

            var entity = new RiskManagement();
            if (entity.LoadByPrimaryKey(txtRiskManagementNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
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
            auditLogFilter.PrimaryKeyData = string.Format("RiskManagementNo='{0}'", txtRiskManagementNo.Text.Trim());
            auditLogFilter.TableName = "RiskManagement";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("transNo", txtRiskManagementNo.Text);
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            using (var trans = new esTransactionScope())
            {
                var entity = new RiskManagement();
                entity.LoadByPrimaryKey(txtRiskManagementNo.Text);

                entity.IsApproved = true;
                entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                entity.ApprovedByUserID = AppSession.UserLogin.UserID;

                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                entity.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new RiskManagement();
            entity.LoadByPrimaryKey(txtRiskManagementNo.Text);

            using (var trans = new esTransactionScope())
            {
                entity.IsApproved = false;
                entity.ApprovedDateTime = null;
                entity.ApprovedByUserID = null;

                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                entity.Save();

                trans.Complete();
            }
        }

        #endregion

        #region ToolBar Menu Support
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new RiskManagement();
            if (entity.LoadByPrimaryKey(txtRiskManagementNo.Text))
            {
                if (!IsApproved(entity, args))
                    return;
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        private bool IsApproved(RiskManagement entity, ValidateArgs args)
        {
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuDelete.Enabled = !chkIsApproved.Checked;
        }

        public override bool OnGetStatusMenuEdit()
        {
            return txtRiskManagementNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !chkIsApproved.Checked;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandRiskManagementItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new RiskManagement();
            if (parameters.Length > 0)
            {
                String id = (String)parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(id);
            }
            else
                entity.LoadByPrimaryKey(txtRiskManagementNo.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var pi = (RiskManagement)entity;
            txtRiskManagementNo.Text = pi.RiskManagementNo;
            txtPeriodYear.Value = Convert.ToDouble(pi.PeriodYear);
            if (!string.IsNullOrEmpty(pi.ServiceUnitID))
            {
                var su = new ServiceUnitQuery();
                su.Where(su.ServiceUnitID == pi.ServiceUnitID);
                cboServiceUnitID.DataSource = su.LoadDataTable();
                cboServiceUnitID.DataBind();
                cboServiceUnitID.SelectedValue = pi.ServiceUnitID;
            }
            else
            {
                cboServiceUnitID.Items.Clear();
                cboServiceUnitID.SelectedValue = string.Empty;
                cboServiceUnitID.Text = string.Empty;
            }
            chkIsApproved.Checked = pi.IsApproved ?? false;

            PopulateRiskManagementItemGrid();
        }
        #endregion

        #region Private Method Standard
        private void SetEntityValue(RiskManagement entity)
        {
            entity.RiskManagementNo = txtRiskManagementNo.Text;
            entity.PeriodYear = Convert.ToInt16(txtPeriodYear.Value);
            entity.ServiceUnitID = cboServiceUnitID.SelectedValue;
            
            //Last Update Status
            if (entity.es.IsAdded)
            {
                entity.CreatedByUserID = AppSession.UserLogin.UserID;
                entity.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
            else if (entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (var item in RiskManagementItems)
            {
                item.RiskManagementNo = txtRiskManagementNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(RiskManagement entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                RiskManagementItems.Save();

                if (DataModeCurrent == AppEnum.DataMode.New)
                    _autoNumber.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new RiskManagementQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.RiskManagementNo > txtRiskManagementNo.Text);
                que.OrderBy(que.RiskManagementNo.Ascending);
            }
            else
            {
                que.Where(que.RiskManagementNo < txtRiskManagementNo.Text);
                que.OrderBy(que.RiskManagementNo.Descending);
            }
            var entity = new RiskManagement();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Override Method & Function

        private void PopulateNewNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New)
                return;

            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.RiskManagement);
            txtRiskManagementNo.Text = _autoNumber.LastCompleteNumber;
        }
        #endregion

        #region ComboBox ServiceUnitID

        protected void cboServiceUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ServiceUnitQuery("a");
            var usr = new AppUserServiceUnitQuery("b");
            query.InnerJoin(usr).On(usr.ServiceUnitID == query.ServiceUnitID && usr.UserID == AppSession.UserLogin.UserID);
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            query.Where
                (
                    query.Or
                        (
                            query.ServiceUnitID.Like(searchTextContain),
                            query.ServiceUnitName.Like(searchTextContain)
                        ), 
                    query.IsActive==true
                );
            query.es.Top = 20;

            cboServiceUnitID.DataSource = query.LoadDataTable();
            cboServiceUnitID.DataBind();
        }

        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        #endregion

        #region Record Detail Method Function of Patient Incident Related Unit

        private RiskManagementItemCollection RiskManagementItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collRiskManagementItem" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((RiskManagementItemCollection)(obj));
                    }
                }

                var coll = new RiskManagementItemCollection();
                var query = new RiskManagementItemQuery("a");
                var category = new AppStandardReferenceItemQuery("b");
                var impact = new AppStandardReferenceItemQuery("c");
                var probability = new AppStandardReferenceItemQuery("d");
                var bands = new AppStandardReferenceItemQuery("e");
                var controlling = new AppStandardReferenceItemQuery("f");

                query.Select
                    (
                        query,
                        category.ItemName.As("refToAppStandardReferenceItem_RiskManagementCategory"),
                        impact.ItemName.As("refToAppStandardReferenceItem_RiskManagementImpact"),
                        probability.ItemName.As("refToAppStandardReferenceItem_RiskManagementProbability"),
                        bands.ItemName.As("refToAppStandardReferenceItem_RiskManagementBands"),
                        bands.ReferenceID.As("refToAppStandardReferenceItem_RiskManagementBandsColor"),
                        controlling.ItemName.As("refToAppStandardReferenceItem_RiskManagementControlling")

                    );
                query.InnerJoin(category).On(category.StandardReferenceID == AppEnum.StandardReference.RiskManagementCategory.ToString() && category.ItemID == query.SRRiskManagementCategory);
                query.InnerJoin(impact).On(impact.StandardReferenceID == AppEnum.StandardReference.RiskManagementImpact.ToString() && impact.ItemID == query.SRRiskManagementImpact);
                query.InnerJoin(probability).On(probability.StandardReferenceID == AppEnum.StandardReference.RiskManagementProbability.ToString() && probability.ItemID == query.SRRiskManagementProbability);
                query.InnerJoin(bands).On(bands.StandardReferenceID == AppEnum.StandardReference.RiskManagementBands.ToString() && bands.ItemID == query.SRRiskManagementBands);
                query.InnerJoin(controlling).On(controlling.StandardReferenceID == AppEnum.StandardReference.RiskManagementControlling.ToString() && controlling.ItemID == query.SRRiskManagementControlling);
                query.OrderBy(query.SRRiskManagementCategory.Ascending, query.SequenceNo.Ascending);

                query.Where(query.RiskManagementNo == txtRiskManagementNo.Text);
                coll.Load(query);

                Session["collRiskManagementItem" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collRiskManagementItem" + Request.UserHostName] = value;
            }
        }

        private void RefreshCommandRiskManagementItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdRiskManagementItem.Columns[0].Visible = isVisible;
            grdRiskManagementItem.Columns[grdRiskManagementItem.Columns.Count - 1].Visible = isVisible;

            grdRiskManagementItem.MasterTableView.CommandItemDisplay = isVisible
                                                                             ? GridCommandItemDisplay.Top
                                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdRiskManagementItem.Rebind();
        }

        private void PopulateRiskManagementItemGrid()
        {
            //Display Data Detail
            RiskManagementItems = null; //Reset Record Detail
            grdRiskManagementItem.DataSource = RiskManagementItems; //Requery
            grdRiskManagementItem.MasterTableView.IsItemInserted = false;
            grdRiskManagementItem.MasterTableView.ClearEditItems();
            grdRiskManagementItem.DataBind();
        }

        private RiskManagementItem FindRiskManagementItem(String seqNo)
        {
            RiskManagementItemCollection coll = RiskManagementItems;
            RiskManagementItem retEntity = null;
            foreach (RiskManagementItem rec in coll)
            {
                if (rec.SequenceNo.Equals(seqNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdRiskManagementItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRiskManagementItem.DataSource = RiskManagementItems;
        }

        protected void grdRiskManagementItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String seqNo =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][RiskManagementItemMetadata.ColumnNames.SequenceNo]);
            RiskManagementItem entity = FindRiskManagementItem(seqNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdRiskManagementItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String seqNo =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][RiskManagementItemMetadata.ColumnNames.SequenceNo]);
            RiskManagementItem entity = FindRiskManagementItem(seqNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdRiskManagementItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            RiskManagementItem entity = RiskManagementItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdRiskManagementItem.Rebind();
        }

        private void SetEntityValue(RiskManagementItem entity, GridCommandEventArgs e)
        {
            var userControl = (RiskManagementItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SequenceNo = userControl.SequenceNo;
                entity.SRRiskManagementCategory = userControl.SRRiskManagementCategory;
                entity.RiskManagementCategoryName = userControl.RiskManagementCategoryName;
                entity.RiskManagementDescription = userControl.RiskManagementDescription;
                entity.SRRiskManagementImpact = userControl.SRRiskManagementImpact;
                entity.RiskManagementImpactName = userControl.RiskManagementImpactName;
                entity.ImpactScore = userControl.ImpactScore;
                entity.SRRiskManagementProbability = userControl.SRRiskManagementProbability;
                entity.RiskManagementProbabilityName = userControl.RiskManagementProbabilityName;
                entity.ProbabilityScore = userControl.ProbabilityScore;
                entity.RiskScore = userControl.RiskScore;
                entity.SRRiskManagementBands = userControl.SRRiskManagementBands;
                entity.RiskManagementBandsName = userControl.RiskManagementBandsName;
                entity.RiskManagementBandsColor = userControl.RiskManagementBandsColor;
                entity.SRRiskManagementControlling = userControl.SRRiskManagementControlling;
                entity.RiskManagementControllingName = userControl.RiskManagementControllingName;
                entity.ControllingScore = userControl.ControllingScore;
                entity.TotalScore = userControl.TotalScore;
                entity.RiskRating = userControl.RiskRating;
                entity.Reason = userControl.Reason;
                entity.Action = userControl.Action;
                entity.Pic = userControl.Pic;
            }
        }

        #endregion
    }
}