using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ReasonForTreatmentDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ReasonForTreatmentSearch.aspx";
            UrlPageList = "ReasonForTreatmentList.aspx";

            ProgramID = AppConstant.Program.ReasonForTreatment; 

            if (!IsPostBack)
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.Diagnose, this.Page);
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
            var entity = new AppStandardReferenceItem();
            if (entity.LoadByPrimaryKey(AppEnum.StandardReference.VisitReason.ToString(), txtItemID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            
            entity = new AppStandardReferenceItem();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new AppStandardReferenceItem();
            if (entity.LoadByPrimaryKey(AppEnum.StandardReference.VisitReason.ToString(), txtItemID.Text))
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
            //auditLogFilter.PrimaryKeyData = string.Format("ItemID='{0}'", txtItemID.Text.Trim());
            //auditLogFilter.TableName = "AppStandardReferenceItem";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new AppStandardReferenceItem();
            if (parameters.Length > 0)
            {
                string itemId = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(AppEnum.StandardReference.VisitReason.ToString(), itemId);
            }
            else
            {
                entity.LoadByPrimaryKey(AppEnum.StandardReference.VisitReason.ToString(), txtItemID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var appStd = (AppStandardReferenceItem)entity;
            txtItemID.Text = appStd.ItemID;
            txtItemName.Text = appStd.ItemName;
            chkIsActive.Checked = appStd.IsActive ?? false;

            //Display Data Detail
            PopulateItemGrid();
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(AppStandardReferenceItem entity)
        {
            entity.StandardReferenceID = AppEnum.StandardReference.VisitReason.ToString();
            entity.ItemID = txtItemID.Text;
            entity.ItemName = txtItemName.Text;

            foreach (ReasonsForTreatment item in ReasonsForTreatments)
            {
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }

            foreach (ReasonsForTreatmentDesc item in ReasonsForTreatmentDesc)
            {
                //item.SRReasonVisit = txtItemID.Text;
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void SaveEntity(AppStandardReferenceItem entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                ReasonsForTreatments.Save();
                //Commit if success, Rollback if failed
                ReasonsForTreatmentDesc.Save();
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new AppStandardReferenceItemQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ItemID > txtItemID.Text, que.StandardReferenceID == AppEnum.StandardReference.VisitReason);
                que.OrderBy(que.ItemID.Ascending);
            }
            else
            {
                que.Where(que.ItemID < txtItemID.Text, que.StandardReferenceID == AppEnum.StandardReference.VisitReason);
                que.OrderBy(que.ItemID.Descending);
            }
            var entity = new AppStandardReferenceItem();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged

        #endregion

        #region Record Detail Method Function

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 2].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible
                                                             ? GridCommandItemDisplay.Top
                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private ReasonsForTreatmentCollection ReasonsForTreatments
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collReasonsForTreatment"];
                    if (obj != null)
                    {
                        return ((ReasonsForTreatmentCollection)(obj));
                    }
                }

                var coll = new ReasonsForTreatmentCollection();
                var query = new ReasonsForTreatmentQuery("a");
                var diagQ = new DiagnoseQuery("b");

                query.Select
                    (
                       query.SRReasonVisit,
                       query.ReasonsForTreatmentID,
                       query.ReasonsForTreatmentName,
                       query.DiagnoseID,
                       diagQ.DiagnoseName.As("refToDiagnose_DiagnoseName"),
                       query.IsActive,
                       query.LastUpdateDateTime,
                       query.LastUpdateByUserID
                    );

                query.LeftJoin(diagQ).On(query.DiagnoseID == diagQ.DiagnoseID);

                query.Where(query.SRReasonVisit == txtItemID.Text);
                query.OrderBy(query.ReasonsForTreatmentID.Ascending);

                coll.Load(query);

                // --- force load desc
                var x = ReasonsForTreatmentDesc;

                Session["collReasonsForTreatment"] = coll;
                return coll;
            }
            set { Session["collReasonsForTreatment"] = value; }
        }

        private ReasonsForTreatmentDescCollection ReasonsForTreatmentDesc
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collReasonsForTreatmentDesc"];
                    if (obj != null)
                    {
                        return ((ReasonsForTreatmentDescCollection)(obj));
                    }
                }

                var coll = new ReasonsForTreatmentDescCollection();
                var query = new ReasonsForTreatmentDescQuery("a");

                query.Where(query.SRReasonVisit == txtItemID.Text);
                query.OrderBy(query.ReasonsForTreatmentID.Ascending);
                query.OrderBy(query.ReasonsForTreatmentDescName.Ascending);

                coll.Load(query);

                Session["collReasonsForTreatmentDesc"] = coll;
                return coll;
            }
            set { Session["collReasonsForTreatmentDesc"] = value; }
        }

        private void PopulateItemGrid()
        {
            ReasonsForTreatments = null;
            grdItem.DataSource = ReasonsForTreatments;
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = ReasonsForTreatments;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            string itemId = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ReasonsForTreatmentMetadata.ColumnNames.ReasonsForTreatmentID].ToString();
            ReasonsForTreatment entity = FindItem(itemId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            string itemId = item.OwnerTableView.DataKeyValues[item.ItemIndex][ReasonsForTreatmentMetadata.ColumnNames.ReasonsForTreatmentID].ToString();
            ReasonsForTreatment entity = FindItem(itemId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            ReasonsForTreatment entity = ReasonsForTreatments.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private ReasonsForTreatment FindItem(string itemId)
        {
            var coll = ReasonsForTreatments;
            ReasonsForTreatment retEntity = null;
            foreach (ReasonsForTreatment rec in coll)
            {
                if (rec.ReasonsForTreatmentID.Equals(itemId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(ReasonsForTreatment entity, GridCommandEventArgs e)
        {
            var userControl = (ReasonForTreatmentItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ReasonsForTreatmentID = userControl.ReasonsForTreatmentID;
                entity.ReasonsForTreatmentName = userControl.ReasonsForTreatmentName;
                entity.DiagnoseID = userControl.DiagnoseID;
                entity.DiagnoseName = userControl.DiagnoseName;
                entity.IsActive = userControl.IsActive;
                entity.SRReasonVisit = txtItemID.Text;
            }
        }

        #endregion
    }
}
