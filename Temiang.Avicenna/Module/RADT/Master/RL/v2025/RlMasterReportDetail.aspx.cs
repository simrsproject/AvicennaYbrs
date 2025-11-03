using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.RADT.Master.v2025
{
    public partial class RlMasterReportDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "RlMasterReportSearch.aspx";
            UrlPageList = "RlMasterReportList.aspx";

            ProgramID = AppConstant.Program.RlMasterReportV2; //TODO: Isi ProgramID
            txtRlMasterReportID.Text = "1";
            //StandardReference Initialize
            if (!IsPostBack)
            {
            }

            //PopUp Search
            if (!IsCallback)
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
            OnPopulateEntryControl(new RlMasterReportV2025());
            chkIsActive.Checked = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            RlMasterReportV2025 entity = new RlMasterReportV2025();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtRlMasterReportID.Text)))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            RlMasterReportV2025 entity = new RlMasterReportV2025();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            RlMasterReportV2025 entity = new RlMasterReportV2025();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtRlMasterReportID.Text)))
            {
                SetEntityValue(entity);
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
            auditLogFilter.PrimaryKeyData = string.Format("RlMasterReportID='{0}'", txtRlMasterReportID.Text.Trim());
            auditLogFilter.TableName = "RlMasterReportV2025";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtRlMasterReportID.Enabled = (newVal == AppEnum.DataMode.New);

            RefreshCommandItemRlMasterReportItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            RlMasterReportV2025 entity = new RlMasterReportV2025();
            if (parameters.Length > 0)
            {
                string rlMasterReportID = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(rlMasterReportID));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtRlMasterReportID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            RlMasterReportV2025 rlMasterReport = (RlMasterReportV2025)entity;
            txtRlMasterReportID.Value = Convert.ToDouble(rlMasterReport.RlMasterReportID);
            txtRlMasterReportNo.Text = rlMasterReport.RlMasterReportNo;
            txtRlMasterReportName.Text = rlMasterReport.RlMasterReportName;
            chkIsActive.Checked = rlMasterReport.IsActive ?? false;
            txtNotes.Text = rlMasterReport.Notes;

            //Display Data Detail
            PopulateRlMasterReportItemGrid();
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(RlMasterReportV2025 entity)
        {
            entity.RlMasterReportID = Convert.ToInt32(txtRlMasterReportID.Value);
            entity.RlMasterReportNo = txtRlMasterReportNo.Text;
            entity.RlMasterReportName = txtRlMasterReportName.Text;
            entity.IsActive = chkIsActive.Checked;
            entity.Notes = txtNotes.Text;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            foreach (RlMasterReportItemV2025 item in RlMasterReportItems)
            {
                item.RlMasterReportID = Convert.ToInt32(txtRlMasterReportID.Text);
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void SaveEntity(RlMasterReportV2025 entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                RlMasterReportItems.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            RlMasterReportV2025Query que = new RlMasterReportV2025Query();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.RlMasterReportID > txtRlMasterReportID.Text);
                que.OrderBy(que.RlMasterReportID.Ascending);
            }
            else
            {
                que.Where(que.RlMasterReportID < txtRlMasterReportID.Text);
                que.OrderBy(que.RlMasterReportID.Descending);
            }
            RlMasterReportV2025 entity = new RlMasterReportV2025();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged

        #endregion

        #region Record Detail Method Function RlMasterReportItem

        private void RefreshCommandItemRlMasterReportItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdRlMasterReportItem.Columns[0].Visible = isVisible;
            //grdRlMasterReportItem.Columns[grdRlMasterReportItem.Columns.Count - 1].Visible = isVisible;

            grdRlMasterReportItem.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdRlMasterReportItem.Rebind();
        }

        private RlMasterReportItemV2025Collection RlMasterReportItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collRlMasterReportItemV2025"];
                    if (obj != null)
                    {
                        return ((RlMasterReportItemV2025Collection)(obj));
                    }
                }

                var coll = new RlMasterReportItemV2025Collection();
                var rlType = new SmfQuery("c");
                var header = new RlMasterReportV2025Query("b");
                var query = new RlMasterReportItemV2025Query("a");

                query.Select
                    (
                       query.RlMasterReportItemID,
                       query.RlMasterReportID,
                       query.RlMasterReportItemNo,
                       query.RlMasterReportItemCode,
                       query.RlMasterReportItemName,
                       query.SRParamedicRL1,
                       rlType.SmfName.As("refToSmf_SmfName"),
                       query.IsActive,
                       query.ParameterValue,
                       query.LastUpdateDateTime,
                       query.LastUpdateByUserID

                    );

                query.InnerJoin(header).On(query.RlMasterReportID == header.RlMasterReportID);
                query.LeftJoin(rlType).On(query.SRParamedicRL1 == rlType.SmfID);

                query.Where(query.RlMasterReportID == txtRlMasterReportID.Text);
                query.OrderBy(query.RlMasterReportItemNo.Ascending);

                coll.Load(query);

                Session["collRlMasterReportItemV2025"] = coll;
                return coll;
            }
            set { Session["collRlMasterReportItemV2025"] = value; }
        }

        private void PopulateRlMasterReportItemGrid()
        {
            //Display Data Detail
            RlMasterReportItems = null; //Reset Record Detail
            grdRlMasterReportItem.DataSource = RlMasterReportItems; //Requery
            grdRlMasterReportItem.MasterTableView.IsItemInserted = false;
            grdRlMasterReportItem.MasterTableView.ClearEditItems();
            grdRlMasterReportItem.DataBind();
        }

        protected void grdRlMasterReportItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            switch (txtRlMasterReportID.Text)
            {
                case "0":
                    break;
                default:
                    grdRlMasterReportItem.DataSource = RlMasterReportItems;
                    break;
            }
        }

        protected void grdRlMasterReportItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 rlMasterReportItemID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][RlMasterReportItemV2025Metadata.ColumnNames.RlMasterReportItemID]);
            RlMasterReportItemV2025 entity = FindRlMasterReportItem(rlMasterReportItemID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdRlMasterReportItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 rlMasterReportItemID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][RlMasterReportItemV2025Metadata.ColumnNames.RlMasterReportItemID]);
            RlMasterReportItemV2025 entity = FindRlMasterReportItem(rlMasterReportItemID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdRlMasterReportItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            RlMasterReportItemV2025 entity = RlMasterReportItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdRlMasterReportItem.Rebind();
        }
        private RlMasterReportItemV2025 FindRlMasterReportItem(Int32 rlMasterReportItemID)
        {
            RlMasterReportItemV2025Collection coll = RlMasterReportItems;
            RlMasterReportItemV2025 retEntity = null;
            foreach (RlMasterReportItemV2025 rec in coll)
            {
                if (rec.RlMasterReportItemID.Equals(rlMasterReportItemID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(RlMasterReportItemV2025 entity, GridCommandEventArgs e)
        {
            RlMasterReportItemDetail userControl = (RlMasterReportItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.RlMasterReportItemID = userControl.RlMasterReportItemID;
                entity.RlMasterReportItemNo = userControl.RlMasterReportItemNo;
                entity.RlMasterReportItemCode = userControl.RlMasterReportItemCode;
                entity.RlMasterReportItemName = userControl.RlMasterReportItemName;
                entity.IsActive = userControl.IsActive;
                entity.SRParamedicRL1 = userControl.SRParamedicRL1;
                entity.ParamedicRL1Name = userControl.SRParamedicRL1Name;
                entity.ParameterValue = userControl.ParameterValue;

            }
        }

        #endregion

    }
}
