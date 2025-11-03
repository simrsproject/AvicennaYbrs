using System;
using System.Collections.Generic;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Linq;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class RiskGradingMtxDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "RiskGradingMtxSearch.aspx";
            UrlPageList = "RiskGradingMtxList.aspx";

            ProgramID = AppConstant.Program.RiskGradingMtx;

            if (!IsPostBack)
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
            OnPopulateEntryControl(new AppStandardReferenceItem());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            SetEntityValue();
            SaveEntity();
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new AppStandardReferenceItem();
            if (entity.LoadByPrimaryKey(AppEnum.StandardReference.ClinicalImpact.ToString(), txtItemID.Text))
            {
                SetEntityValue();
                SaveEntity();
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
            //auditLogFilter.PrimaryKeyData = string.Format("ItemIDID='{0}'", txtItemID.Text.Trim());
            //auditLogFilter.TableName = "AppStandardReferenceItem";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemRiskGrading(newVal);
            txtItemID.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new AppStandardReferenceItem();
            if (parameters.Length > 0)
            {
                String itemId = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(AppEnum.StandardReference.ClinicalImpact.ToString(), itemId);
            }
            else
            {
                entity.LoadByPrimaryKey(AppEnum.StandardReference.ClinicalImpact.ToString(), txtItemID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var std = (AppStandardReferenceItem)entity;
            txtItemID.Text = std.ItemID;
            txtItemName.Text = std.ItemName;
            
            PopulateRiskGradingGrid();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue()
        {
            foreach (var item in RiskGradingMtxs)
            {
                item.SRClinicalImpact = txtItemID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity()
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                RiskGradingMtxs.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new AppStandardReferenceItemQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ItemID > txtItemID.Text);
                que.OrderBy(que.ItemID.Ascending);
            }
            else
            {
                que.Where(que.ItemID < txtItemID.Text);
                que.OrderBy(que.ItemID.Descending);
            }

            var entity = new AppStandardReferenceItem();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function of Incident Type Component
        private RiskGradingMtxCollection RiskGradingMtxs
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collRiskGradingMtx"];
                    if (obj != null)
                    {
                        return ((RiskGradingMtxCollection)(obj));
                    }
                }

                var coll = new RiskGradingMtxCollection();
                var query = new RiskGradingMtxQuery("a");
                var ref1 = new AppStandardReferenceItemQuery("b");
                var ref2 = new AppStandardReferenceItemQuery("c");
                var rg = new RiskGradingQuery("d");
                query.Select
                    (
                        query,
                        ref1.ItemName.As("refToAppStandardReferenceItem_IncidentProbabilityFrequency"),
                        ref2.ItemName.As("refToAppStandardReferenceItem_IncidentFollowUp"),
                        rg.RiskGradingName.As("refToRiskGrading_RiskGradingName")
                    );
                query.InnerJoin(ref1).On(query.SRIncidentProbabilityFrequency == ref1.ItemID &&
                                         ref1.StandardReferenceID ==
                                         AppEnum.StandardReference.IncidentProbabilityFrequency.ToString());
                query.InnerJoin(ref2).On(query.SRIncidentFollowUp == ref2.ItemID &&
                                         ref2.StandardReferenceID ==
                                         AppEnum.StandardReference.IncidentFollowUp.ToString());
                query.InnerJoin(rg).On(query.RiskGradingID == rg.RiskGradingID);
                query.Where(query.SRClinicalImpact == txtItemID.Text);
                coll.Load(query);

                Session["collRiskGradingMtx"] = coll;
                return coll;
            }
            set
            {
                Session["collRiskGradingMtx"] = value;
            }
        }

        private void RefreshCommandItemRiskGrading(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdGrading.Columns[0].Visible = isVisible;
            grdGrading.Columns[grdGrading.Columns.Count - 1].Visible = isVisible;

            grdGrading.MasterTableView.CommandItemDisplay = isVisible
                                                                             ? GridCommandItemDisplay.Top
                                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdGrading.Rebind();
        }

        private void PopulateRiskGradingGrid()
        {
            //Display Data Detail
            RiskGradingMtxs = null; //Reset Record Detail
            grdGrading.DataSource = RiskGradingMtxs; //Requery
            grdGrading.MasterTableView.IsItemInserted = false;
            grdGrading.MasterTableView.ClearEditItems();
            grdGrading.DataBind();
        }

        private RiskGradingMtx FindRiskGrading(String probalityId)
        {
            RiskGradingMtxCollection coll = RiskGradingMtxs;
            RiskGradingMtx retEntity = null;
            foreach (RiskGradingMtx rec in coll)
            {
                if (rec.SRIncidentProbabilityFrequency.Equals(probalityId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdGrading_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdGrading.DataSource = RiskGradingMtxs;
        }

        protected void grdGrading_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String probalityId =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][RiskGradingMtxMetadata.ColumnNames.SRIncidentProbabilityFrequency]);
            RiskGradingMtx entity = FindRiskGrading(probalityId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdGrading_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String probalityId =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][RiskGradingMtxMetadata.ColumnNames.SRIncidentProbabilityFrequency]);
            RiskGradingMtx entity = FindRiskGrading(probalityId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdGrading_InsertCommand(object source, GridCommandEventArgs e)
        {
            RiskGradingMtx entity = RiskGradingMtxs.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdGrading.Rebind();
        }

        private void SetEntityValue(RiskGradingMtx entity, GridCommandEventArgs e)
        {
            var userControl = (RiskGradingMtxItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SRIncidentProbabilityFrequency = userControl.SRIncidentProbabilityFrequency;
                entity.IncidentProbabilityFrequency = userControl.IncidentProbabilityFrequency;
                entity.SRIncidentFollowUp = userControl.SRIncidentFollowUp;
                entity.IncidentFollowUp = userControl.IncidentFollowUp;
                entity.RiskGradingID = userControl.RiskGradingID;
                entity.RiskGradingName = userControl.RiskGradingName;
            }
        }
        #endregion
    }
}
