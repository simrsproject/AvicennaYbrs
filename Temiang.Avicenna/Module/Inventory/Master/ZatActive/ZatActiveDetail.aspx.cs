using System;
using System.Collections.Generic;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Linq;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ZatActiveDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ZatActiveSearch.aspx";
            UrlPageList = "ZatActiveList.aspx";

            ProgramID = AppConstant.Program.ZatActive;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {

        }

        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuEditClick()
        {
            StandardReference.InitializeIncludeSpace(cboSRZatActiveGroup, AppEnum.StandardReference.ZatActiveGroup);
        }
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ZatActive());
            txtZatActiveID.Text = NewZatActiveID();
            StandardReference.InitializeIncludeSpace(cboSRZatActiveGroup, AppEnum.StandardReference.ZatActiveGroup);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            ZatActive entity = new ZatActive();
            if (entity.LoadByPrimaryKey(txtZatActiveID.Text))
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
            var entity = new ZatActive();
            if (entity.LoadByPrimaryKey(txtZatActiveID.Text))
                txtZatActiveID.Text = NewZatActiveID();

            //if (entity.LoadByPrimaryKey(txtZatActiveID.Text))
            //{
            //    args.MessageText = AppConstant.Message.DuplicateKey;
            //    args.IsCancel = true;
            //    return;
            //}

            entity = new ZatActive();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new ZatActive();
            if (entity.LoadByPrimaryKey(txtZatActiveID.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("ZatActiveID='{0}'", txtZatActiveID.Text.Trim());
            auditLogFilter.TableName = "ZatActive";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtZatActiveID.Enabled = (newVal == AppEnum.DataMode.New);
            RefreshCommandItemZatActiveInteraction(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ZatActive();
            if (parameters.Length > 0)
            {
                String ZatActiveID = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(ZatActiveID);
            }
            else
            {
                entity.LoadByPrimaryKey(txtZatActiveID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var za = (ZatActive)entity;
            txtZatActiveID.Text = za.ZatActiveID;
            txtZatActiveName.Text = za.ZatActiveName;
            txtDddOral.Value = za.DddOral.ToDouble();
            txtDddParenteral.Value = za.DddParenteral.ToDouble();
            chkIsActive.Checked = za.IsActive.HasValue ? za.IsActive.Value : true;
            StandardReference.InitializeWithOneRow(cboSRZatActiveGroup, AppEnum.StandardReference.ZatActiveGroup, za.SRZatActiveGroup);
        
            PopulateZatActiveInteractionGrid();
            }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(ZatActive entity)
        {
            entity.ZatActiveID = txtZatActiveID.Text;
            entity.ZatActiveName = txtZatActiveName.Text;
            entity.IsActive = chkIsActive.Checked;
            entity.DddOral = txtDddOral.Value.ToDecimal();
            entity.DddParenteral = txtDddParenteral.Value.ToDecimal();
            entity.SRZatActiveGroup = cboSRZatActiveGroup.SelectedValue;

            if (entity.es.IsAdded)
            {
                entity.InsertByUserID = AppSession.UserLogin.UserID;
                entity.InsertDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
            else if (entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(ZatActive entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                ZatActiveInteractions.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            ZatActiveQuery que = new ZatActiveQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ZatActiveID > txtZatActiveID.Text);
                que.OrderBy(que.ZatActiveID.Ascending);
            }
            else
            {
                que.Where(que.ZatActiveID < txtZatActiveID.Text);
                que.OrderBy(que.ZatActiveID.Descending);
            }

            ZatActive entity = new ZatActive();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        private string NewZatActiveID()
        {
            var query = new ZatActiveQuery();
            query.es.Top = 1;
            query.Select(query.ZatActiveID);
            query.OrderBy(query.ZatActiveID.Descending);

            var za = new ZatActive();
            za.Load(query);

            int x;
            if (za.ZatActiveID != null)
            {
                var number = za.ZatActiveID.Where(m => char.IsNumber(m)).Aggregate(string.Empty, (current, m) => current + (m));
                x = (int.Parse(number) + 1);
            }
            else
                x = 1;
            return string.Format("{0:00000}", x);
        }

        #region Record Detail Method Function ZatActiveInteraction

        private ZatActiveInteractionCollection ZatActiveInteractions
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collZatActiveInteraction"];
                    if (obj != null)
                    {
                        return ((ZatActiveInteractionCollection)(obj));
                    }
                }


                ZatActiveInteractionCollection coll = new ZatActiveInteractionCollection();
                ZatActiveInteractionQuery query = new ZatActiveInteractionQuery("a");
                ZatActiveQuery za = new ZatActiveQuery("b");


                query.InnerJoin(za).On(query.InteractionZatActiveID == za.ZatActiveID);
                query.Select
                    (
                        query,
                        za.ZatActiveName.As("refZatActive_ZatActiveName")
                    );

                query.Where(query.ZatActiveID == txtZatActiveID.Text);
                query.OrderBy(za.ZatActiveName.Ascending);
                coll.Load(query);

                Session["collZatActiveInteraction"] = coll;
                return coll;
            }
            set { Session["collZatActiveInteraction"] = value; }
        }

        private void RefreshCommandItemZatActiveInteraction(AppEnum.DataMode newVal)
        {

            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdZatActiveInteraction.Columns[0].Visible = isVisible; //Edit
            grdZatActiveInteraction.Columns[grdZatActiveInteraction.Columns.Count - 2].Visible = isVisible; //delete
            grdZatActiveInteraction.MasterTableView.CommandItemDisplay = isVisible
                                                                               ? GridCommandItemDisplay.Top
                                                                               : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdZatActiveInteraction.Rebind();

        }

        private void PopulateZatActiveInteractionGrid()
        {
            //Display Data Detail
            ZatActiveInteractions = null; //Reset Record Detail
            grdZatActiveInteraction.DataSource = ZatActiveInteractions; //Requery
            grdZatActiveInteraction.MasterTableView.IsItemInserted = false;
            grdZatActiveInteraction.MasterTableView.ClearEditItems();
            grdZatActiveInteraction.DataBind();
        }

        protected void grdZatActiveInteraction_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdZatActiveInteraction.DataSource = ZatActiveInteractions;
        }

        protected void grdZatActiveInteraction_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String interactionZatActiveID =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        ZatActiveInteractionMetadata.ColumnNames.InteractionZatActiveID]);
            ZatActiveInteraction entity = FindZatActiveInteraction(interactionZatActiveID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdZatActiveInteraction_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String interactionZatActiveID =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][ZatActiveInteractionMetadata.ColumnNames.InteractionZatActiveID]);
            ZatActiveInteraction entity = FindZatActiveInteraction(interactionZatActiveID);

            if (entity != null)
            {
                entity.MarkAsDeleted();
            }

        }

        protected void grdZatActiveInteraction_InsertCommand(object source, GridCommandEventArgs e)
        {
            ZatActiveInteraction entity = ZatActiveInteractions.AddNew();
            SetEntityValue(entity, e);
            //Stay in insert mode

            e.Canceled = true;
            grdZatActiveInteraction.Rebind();
        }

        private ZatActiveInteraction FindZatActiveInteraction(String interactionZatActiveID)
        {
            ZatActiveInteractionCollection coll = ZatActiveInteractions;
            return coll.FirstOrDefault(rec => rec.InteractionZatActiveID.Equals(interactionZatActiveID));
        }


        private void SetEntityValue(ZatActiveInteraction entity, GridCommandEventArgs e)
        {
            ZatActiveInteractionDetail userControl = (ZatActiveInteractionDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ZatActiveID = txtZatActiveID.Text;
                entity.InteractionZatActiveID = userControl.InteractionZatActiveID;
                entity.InteractionZatActiveName = userControl.InteractionZatActiveName;
                entity.Interaction = userControl.Interaction;

            }
        }

        #endregion

    }
}
