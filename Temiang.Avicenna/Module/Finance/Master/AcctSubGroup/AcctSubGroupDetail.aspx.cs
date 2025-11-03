using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.Module.Finance.Master.AcctSubGroup;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class AcctSubGroupDetail : BasePageDetail
    {
        protected int SubLedgerGroupID;

        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "AcctSubGroupSearch.aspx";
            UrlPageList = "AcctSubGroupList.aspx";

            ProgramID = AppConstant.Program.ACCOUNT_SUB_GROUP;

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
            string id = Request.QueryString["id"];
            int.TryParse(id, out this.SubLedgerGroupID);

            grdSubLedgers.Columns.FindByUniqueName("IsDirectCost").Visible = (id == AppSession.Parameter.SubLedgerGroupIdServiceUnit) && AppSession.Parameter.acc_IsJournalPayrollWithDirectIndirectCost;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new SubLedgerGroups());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            SubLedgerGroups entity = SubLedgerGroups.Get(txtAcctSubGroupID.Text) ;
            if (entity != null)
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
            SubLedgerGroups entity = SubLedgerGroups.Get(txtAcctSubGroupID.Text);
            if (entity != null)
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new SubLedgerGroups();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            SubLedgerGroups entity =  SubLedgerGroups.Get(txtAcctSubGroupID.Text);
            if (entity != null)
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
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("GroupCode='{1}'", txtAcctSubGroupID.Text.Trim());
            auditLogFilter.TableName = "SubLedgerGroups";
            
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtAcctSubGroupID.Enabled = (newVal == AppEnum.DataMode.New);

            RefreshCommandItemGrid(oldVal, newVal);

        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            SubLedgerGroups entity = new SubLedgerGroups();
            if (parameters.Length > 0)
            {
                String AcctSubGroupID = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity = SubLedgerGroups.Get(Convert.ToInt32(AcctSubGroupID));
            }
            else
            {
                entity = SubLedgerGroups.Get(txtAcctSubGroupID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity e)
        {
            SubLedgerGroups entity = (SubLedgerGroups)e;
            txtAcctSubGroupID.Text = entity.GroupCode;
            txtAcctSubGroupName.Text = entity.GroupName;
            txtDescription.Text = entity.Description;
            PopulateGridDetail();
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(SubLedgerGroups entity)
        {
            entity.GroupCode = txtAcctSubGroupID.Text;
            entity.GroupName = txtAcctSubGroupName.Text;
            entity.Description = txtDescription.Text;

            entity.LastUpdateDateTime = DateTime.Now;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

            if (entity.es.IsAdded)
            {
                entity.DateCreated = DateTime.Now;
                entity.CreatedBy = entity.LastUpdateByUserID;
            }
        }

        private void SaveEntity(SubLedgerGroups entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                //Commit if success, Rollback if failed
                SubLedgers.Save();

                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            SubLedgerGroupsQuery que = new SubLedgerGroupsQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.GroupCode > txtAcctSubGroupID.Text);
                que.OrderBy(que.GroupCode.Ascending);
            }
            else
            {
                que.Where(que.GroupCode < txtAcctSubGroupID.Text);
                que.OrderBy(que.GroupCode.Descending);
            }
            SubLedgerGroups entity = new SubLedgerGroups();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged
        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdSubLedgers.Columns[0].Visible = isVisible;
            grdSubLedgers.Columns[grdSubLedgers.Columns.Count - 2].Visible = isVisible;

            grdSubLedgers.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                SubLedgers = null;

            //Perbaharui tampilan dan data
            grdSubLedgers.Rebind();
        }

        protected void grdSubLedgers_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdSubLedgers.DataSource = SubLedgers;
        }

        protected void grdSubLedgers_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            string name = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][SubLedgersMetadata.ColumnNames.SubLedgerName]);
            SubLedgers entity = FindSubLedger(name);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdSubLedgers_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            string name = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][SubLedgersMetadata.ColumnNames.SubLedgerName]);
            SubLedgers entity = FindSubLedger(name);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        private SubLedgers FindSubLedger(string name)
        {
            SubLedgersCollection coll = SubLedgers;
            SubLedgers retEntity = null;
            foreach (SubLedgers rec in coll)
            {
                if (rec.SubLedgerName.Equals(name))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdSubLedgers_InsertCommand(object source, GridCommandEventArgs e)
        {
            SubLedgers entity = SubLedgers.AddNew();
            SetEntityValue(entity, e);
        }

        private void SetEntityValue(SubLedgers entity, GridCommandEventArgs e)
        {
            SubLedgerDetail userControl = (SubLedgerDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                SubLedgerGroups group = SubLedgerGroups.Get(txtAcctSubGroupID.Text);
                if (group != null)
                {
                    entity.GroupId = group.SubLedgerGroupId;
                    entity.SubLedgerName = userControl.SubLedgerName;
                    entity.Description = userControl.Description;
                    entity.DateCreated = entity.LastUpdateDateTime = DateTime.Now;
                    entity.CreatedBy = entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                    if (userControl.HoSubLedgerId == 0)
                    {
                        entity.str.HoSubLedgerId = string.Empty;
                        entity.str.HoDescription = string.Empty;
                    }
                    else
                    {
                        entity.HoSubLedgerId = userControl.HoSubLedgerId;
                        entity.HoDescription = userControl.HoDescription;
                    }
                    entity.IsDirectCost = userControl.IsDirectCost;
                }
            }
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            SubLedgers = null; //Reset Record Detail
            grdSubLedgers.DataSource = SubLedgers;
            grdSubLedgers.DataBind();
        }

        private SubLedgersCollection SubLedgers
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collSubLedgers"];
                    if (obj != null)
                    {
                        return ((SubLedgersCollection)(obj));
                    }
                }

                SubLedgersCollection coll = new SubLedgersCollection();
                SubLedgersQuery query = new SubLedgersQuery();
                SubLedgerGroups group = SubLedgerGroups.Get(txtAcctSubGroupID.Text);
                if (group != null)
                {
                    query.Where(query.GroupId == group.SubLedgerGroupId);
                    query.OrderBy(query.SubLedgerName.Ascending);
                    coll.Load(query);

                    Session["collSubLedgers"] = coll;
                }
                return coll;
            }
            set { Session["collSubLedgers"] = value; }
        }
        #endregion
    }
}
