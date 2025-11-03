using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.ControlPanel.Admin
{
    public partial class UserGroupDetail : BasePageDetail
    {
        private void SetEntityValue(AppUserGroup entity, AppUserGroupProgramCollection collDetail)
        {
            entity.UserGroupID = txtUserGroupID.Text;
            entity.UserGroupName = txtUserGroupName.Text;
            entity.IsEditAble = chkIsEditAble.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //UserGroupProgram
            collDetail.Query.Where(collDetail.Query.UserGroupID == txtUserGroupID.Text);
            collDetail.LoadAll();

            IList<ProgramItem> listSelected = ProgramSelecteds;

            //Removed Item
            foreach (AppUserGroupProgram program in collDetail)
                if (GetProgramItem(listSelected, program.ProgramID) == null)
                    program.MarkAsDeleted();

            //New and Updated
            foreach (GridDataItem dataItem in grdSelected.MasterTableView.Items)
            {
                AppUserGroupProgram ugItem;
                string programID = dataItem.GetDataKeyValue("ProgramID").ToString();
                ugItem = collDetail.FindByPrimaryKey(txtUserGroupID.Text, programID);
                if (ugItem == null)
                {
                    ugItem = collDetail.AddNew();
                    ugItem.UserGroupID = txtUserGroupID.Text;
                    ugItem.ProgramID = programID;
                }
                ugItem.IsUserGroupAddAble = ((CheckBox)dataItem.FindControl("chkIsAddAble")).Checked;
                ugItem.IsUserGroupEditAble = ((CheckBox)dataItem.FindControl("chkIsEditAble")).Checked;
                ugItem.IsUserGroupDeleteAble = ((CheckBox)dataItem.FindControl("chkIsDeleteAble")).Checked;
                ugItem.IsUserGroupApprovalAble = ((CheckBox)dataItem.FindControl("chkIsApprovalAble")).Checked;
                ugItem.IsUserGroupUnApprovalAble = ((CheckBox)dataItem.FindControl("chkIsUnApprovalAble")).Checked;
                ugItem.IsUserGroupVoidAble = ((CheckBox)dataItem.FindControl("chkIsVoidAble")).Checked;
                ugItem.IsUserGroupUnVoidAble = ((CheckBox)dataItem.FindControl("chkIsUnVoidAble")).Checked;
                ugItem.IsUserGroupExportAble = ((CheckBox)dataItem.FindControl("chkIsExportAble")).Checked;
                ugItem.IsUserGroupCrossUnitAble = ((CheckBox)dataItem.FindControl("chkIsCrossUnitAble")).Checked;
                ugItem.IsUserGroupPowerUserAble = ((CheckBox)dataItem.FindControl("chkIsPowerUserAble")).Checked;

                if (ugItem.es.IsAdded || ugItem.es.IsModified)
                {
                    ugItem.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    ugItem.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void RefreshDataSourceGridDetail()
        {
            //Requery
            ProgramSelections = null;
            grdSelection.DataSource = ProgramSelections;
            grdSelection.DataBind();

            ProgramSelecteds = null;
            grdSelected.DataSource = ProgramSelecteds;
            grdSelected.DataBind();
        }

        private void MoveRecord(bool isNextRecord)
        {
            AppUserGroupQuery que = new AppUserGroupQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.UserGroupID > txtUserGroupID.Text);
                que.OrderBy(que.UserGroupID.Ascending);
            }
            else
            {
                que.Where(que.UserGroupID < txtUserGroupID.Text);
                que.OrderBy(que.UserGroupID.Descending);
            }
            AppUserGroup entity = new AppUserGroup();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            AppUserGroup entity = new AppUserGroup();
            if (parameters.Length > 0)
            {
                string userGroupID = parameters[0];
                if (!string.IsNullOrEmpty(userGroupID))
                    entity.LoadByPrimaryKey(userGroupID);
            }
            else
                entity.LoadByPrimaryKey(txtUserGroupID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            AppUserGroup ug = (AppUserGroup)entity;
            txtUserGroupID.Text = ug.UserGroupID;
            txtUserGroupName.Text = ug.UserGroupName;
            chkIsEditAble.Checked = ug.IsEditAble ?? false;
            cboModule.Text = string.Empty;
            cboProgramType.Text = string.Empty;

            //Display Data Detail
            RefreshDataSourceGridDetail();
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            //Proses dipindah ke OnDataModeChanged, 
            //karena ada proses Rebind grid yg hanya akan di jalankan Rebind yg pertama kali saja
            //sedangkan di OnDataModeChanged harus ada proses rebind untuk show hide kolom pertama
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            grdSelected.Columns[0].Visible = (newVal != AppEnum.DataMode.Read);
            grdSelection.Columns[0].Visible = (newVal != AppEnum.DataMode.Read);

            switch (newVal)
            {
                case AppEnum.DataMode.Edit:
                    RefreshDataSourceGridDetail();
                    break;
                case AppEnum.DataMode.New:
                    OnPopulateEntryControl(new AppUserGroup());
                    break;
                case AppEnum.DataMode.Read:
                    RefreshDataSourceGridDetail();
                    break;
                default:
                    grdSelected.Rebind();
                    grdSelection.Rebind();
                    break;
            }

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "UserGroupSearch.aspx";
            UrlPageList = "UserGroupList.aspx";

            ProgramID = AppConstant.Program.UserGroup;

            if (!IsPostBack)
            {
                ProgramSelections = null;
                ProgramSelecteds = null;

                AppProgramCollection collModule = new AppProgramCollection();
                AppProgramQuery p1 = new AppProgramQuery("p1");
                AppProgramQuery p2 = new AppProgramQuery("p2");
                p1.InnerJoin(p2).On(p1.ProgramID == p2.TopLevelProgramID);
                p1.Select(p1.ProgramName);
                p1.Where(p2.IsProgram == true, p2.IsDiscontinue == false, p2.IsVisible == true);
                p1.GroupBy(p1.ProgramName, p1.RowIndex);
                p1.OrderBy(p1.RowIndex.Ascending);
                collModule.Load(p1);

                cboModule.Items.Add(new RadComboBoxItem(string.Empty));
                foreach (AppProgram module in collModule)
                {
                    cboModule.Items.Add(new RadComboBoxItem(module.ProgramName));
                }

                AppProgramCollection collType = new AppProgramCollection();
                collType.Query.Select(collType.Query.ProgramType);
                collType.Query.Where(collType.Query.IsProgram == true, collType.Query.IsDiscontinue == false, collType.Query.IsVisible == true);
                collType.Query.es.Distinct = true;
                collType.LoadAll();

                cboProgramType.Items.Add(new RadComboBoxItem(string.Empty));
                foreach (AppProgram type in collType)
                {
                    cboProgramType.Items.Add(new RadComboBoxItem(type.ProgramType));
                }
            }
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = "UserGroupID='" + txtUserGroupID.Text.Trim() + "'";
            auditLogFilter.TableName = "AppUserGroup";
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var collUser = new AppUserUserGroupCollection();
            collUser.Query.Where(collUser.Query.UserGroupID == txtUserGroupID.Text);
            collUser.LoadAll();

            if (collUser.Count > 0)
            {
                args.MessageText = "This data cannot be deleted because it is being used by another program.";
                args.IsCancel = true;
                return;
            }

            collUser.MarkAllAsDeleted();

            var entity = new AppUserGroup();
            entity.LoadByPrimaryKey(txtUserGroupID.Text);
            entity.MarkAsDeleted();

            //UserGroupProgram
            var collDetail = new AppUserGroupProgramCollection();
            collDetail.Query.Where(collDetail.Query.UserGroupID == txtUserGroupID.Text);
            collDetail.LoadAll();
            collDetail.MarkAllAsDeleted();

            //Save
            using (esTransactionScope trans = new esTransactionScope())
            {
                //Save Detail
                collDetail.Save();
                collUser.Save();

                //Save Header
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new AppUserGroup();
            if (entity.LoadByPrimaryKey(txtUserGroupID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            var collDetail = new AppUserGroupProgramCollection();
            entity = new AppUserGroup();
            entity.AddNew();
            SetEntityValue(entity, collDetail);
            SaveEntity(entity, collDetail, true);
        }

        private void SaveEntity(AppUserGroup entity, AppUserGroupProgramCollection collDetail, bool isNew)
        {
            var collRpt = new AppUserGroupProgramCollection();

            if (isNew)
            {
                var prg = new AppProgramCollection();
                prg.Query.Where(prg.Query.ProgramID.In("09", "09.01", "Pvt"));
                prg.LoadAll();

                foreach (var item in prg)
                {
                    var augp = collRpt.AddNew();
                    augp.UserGroupID = entity.UserGroupID;
                    augp.ProgramID = item.ProgramID;
                    augp.IsUserGroupAddAble = false;
                    augp.IsUserGroupEditAble = false;
                    augp.IsUserGroupDeleteAble = false;
                    augp.IsUserGroupApprovalAble = false;
                    augp.IsUserGroupUnApprovalAble = false;
                    augp.IsUserGroupVoidAble = false;
                    augp.IsUserGroupUnVoidAble = false;
                    augp.IsUserGroupExportAble = false;
                    augp.IsUserGroupCrossUnitAble = false;
                    augp.IsUserGroupPowerUserAble = false;
                    augp.LastUpdateDateTime = DateTime.Now;
                    augp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }
            }

            using (esTransactionScope trans = new esTransactionScope())
            {
                //Save Header
                entity.Save();

                //Save Detail
                collDetail.Save();

                if (isNew)
                    collRpt.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuEditClick()
        {
            txtUserGroupID.Enabled = false;
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new AppUserGroup();
            if (entity.LoadByPrimaryKey(txtUserGroupID.Text))
            {
                var collDetail = new AppUserGroupProgramCollection();

                SetEntityValue(entity, collDetail);
                SaveEntity(entity, collDetail, false);
            }
        }

        #endregion

        #region Selection Program

        protected IList<ProgramItem> ProgramSelections
        {
            get
            {
                object obj = Session["ProgramSelections"];
                if (obj != null)
                    return (IList<ProgramItem>)obj;

                AppProgramCollection coll = new AppProgramCollection();
                AppUserGroupProgramQuery qa = new AppUserGroupProgramQuery("a");
                AppProgramQuery qb = new AppProgramQuery("b");
                AppProgramQuery qc = new AppProgramQuery("c");
                AppProgramQuery qd = new AppProgramQuery("d");
                qa.Where
                    (
                        qa.UserGroupID == txtUserGroupID.Text,
                        qa.ProgramID == qb.ProgramID
                    );
                qb.InnerJoin(qc).On(qb.TopLevelProgramID == qc.ProgramID);
                qb.InnerJoin(qd).On(qb.ParentProgramID == qd.ProgramID);
                qb.Select
                    (
                        qb.ProgramID,
                        qb.ProgramName,
                        qb.ProgramType,
                        qc.ProgramName.As("ModuleName"),
                        qb.IsProgramAddAble,
                        qb.IsProgramDeleteAble,
                        qb.IsProgramEditAble,
                        qb.IsProgramApprovalAble,
                        qb.IsProgramUnApprovalAble,
                        qb.IsProgramVoidAble,
                        qb.IsProgramUnVoidAble,
                        qb.IsProgramExportAble,
                        qb.IsProgramCrossUnitAble,
                        qb.IsProgramPowerUserAble
                    );
                qb.Where(qb.IsProgram == true, qb.NotExists(qa), qb.IsDiscontinue == false, qb.IsVisible == true);
                if (cboModule.Text != string.Empty)
                    qb.Where(qc.ProgramName == cboModule.Text);
                if (cboProgramType.Text != string.Empty)
                    qb.Where(qb.ProgramType == cboProgramType.Text);
                qb.OrderBy(qb.ProgramType.Ascending, qc.RowIndex.Ascending, qd.RowIndex.Ascending, qb.RowIndex.Ascending);

                coll.Load(qb);

                IList<ProgramItem> listProgram = new List<ProgramItem>();
                foreach (AppProgram item in coll)
                {
                    ProgramItem programItem = new ProgramItem();
                    programItem.ProgramID = item.ProgramID;
                    programItem.ProgramName = item.GetColumn("ProgramName").ToString();
                    programItem.ProgramType = item.GetColumn("ProgramType").ToString();
                    programItem.ModuleName = item.GetColumn("ModuleName").ToString();

                    programItem.IsAddAble = item.IsProgramAddAble ?? false;
                    programItem.IsEditAble = item.IsProgramEditAble ?? false;
                    programItem.IsDeleteAble = item.IsProgramDeleteAble ?? false;
                    programItem.IsApprovalAble = item.IsProgramApprovalAble ?? false;
                    programItem.IsUnApprovalAble = item.IsProgramUnApprovalAble ?? false;
                    programItem.IsVoidAble = item.IsProgramVoidAble ?? false;
                    programItem.IsUnVoidAble = item.IsProgramUnVoidAble ?? false;
                    programItem.IsCrossUnitAble = item.IsProgramCrossUnitAble ?? false;
                    programItem.IsPowerUserAble = item.IsProgramPowerUserAble ?? false;

                    programItem.IsProgramAddAble = item.IsProgramAddAble ?? false;
                    programItem.IsProgramDeleteAble = item.IsProgramDeleteAble ?? false;
                    programItem.IsProgramEditAble = item.IsProgramEditAble ?? false;
                    programItem.IsProgramApprovalAble = item.IsProgramApprovalAble ?? false;
                    programItem.IsProgramUnApprovalAble = item.IsProgramUnApprovalAble ?? false;
                    programItem.IsProgramVoidAble = item.IsProgramVoidAble ?? false;
                    programItem.IsProgramUnVoidAble = item.IsProgramUnVoidAble ?? false;
                    programItem.IsProgramExportAble = item.IsProgramExportAble ?? false;
                    programItem.IsProgramCrossUnitAble = item.IsProgramCrossUnitAble ?? false;
                    programItem.IsProgramPowerUserAble = item.IsProgramPowerUserAble ?? false;

                    listProgram.Add(programItem);
                }
                Session["ProgramSelections"] = listProgram;

                return listProgram;
            }
            set
            {
                if (value == null)
                    Session.Remove("ProgramSelections");
                else
                    Session["ProgramSelections"] = value;
            }
        }

        protected IList<ProgramItem> ProgramSelecteds
        {
            get
            {
                object obj = Session["ProgramSelecteds"];
                if (obj != null)
                    return (IList<ProgramItem>)obj;

                AppUserGroupProgramCollection coll = new AppUserGroupProgramCollection();
                AppUserGroupProgramQuery qa = new AppUserGroupProgramQuery("a");
                AppProgramQuery qb = new AppProgramQuery("b");
                AppProgramQuery qc = new AppProgramQuery("c");
                AppProgramQuery qd = new AppProgramQuery("d");
                qa.InnerJoin(qb).On(qa.ProgramID == qb.ProgramID);
                qa.InnerJoin(qc).On(qb.TopLevelProgramID == qc.ProgramID);
                qa.InnerJoin(qd).On(qb.ParentProgramID == qd.ProgramID);
                qa.Where
                    (
                        qa.UserGroupID == txtUserGroupID.Text,
                        qb.IsProgram == true
                    );
                qa.Select
                    (
                        qa.ProgramID,
                        qb.ProgramName,
                        qb.ProgramType,
                        qc.ProgramName.As("ModuleName"),

                        qa.IsUserGroupAddAble,
                        qa.IsUserGroupEditAble,
                        qa.IsUserGroupApprovalAble,
                        qa.IsUserGroupUnApprovalAble,
                        qa.IsUserGroupVoidAble,
                        qa.IsUserGroupUnVoidAble,
                        qa.IsUserGroupDeleteAble,
                        qa.IsUserGroupExportAble,
                        qa.IsUserGroupCrossUnitAble,
                        qa.IsUserGroupPowerUserAble,

                        qb.IsProgramAddAble,
                        qb.IsProgramEditAble,
                        qb.IsProgramDeleteAble,
                        qb.IsProgramApprovalAble,
                        qb.IsProgramUnApprovalAble,
                        qb.IsProgramVoidAble,
                        qb.IsProgramUnVoidAble,
                        qb.IsProgramExportAble,
                        qb.IsProgramCrossUnitAble,
                        qb.IsProgramPowerUserAble
                    );
                qa.OrderBy(qb.ProgramType.Ascending, qc.RowIndex.Ascending, qd.RowIndex.Ascending, qb.RowIndex.Ascending);
                coll.Load(qa);

                IList<ProgramItem> listProgram = new List<ProgramItem>();
                foreach (AppUserGroupProgram item in coll)
                {
                    ProgramItem programItem = new ProgramItem();
                    programItem.ProgramID = item.ProgramID;
                    programItem.ProgramName = item.GetColumn("ProgramName").ToString();
                    programItem.ProgramType = item.GetColumn("ProgramType").ToString();
                    programItem.ModuleName = item.GetColumn("ModuleName").ToString();

                    programItem.IsAddAble = item.IsUserGroupAddAble ?? false;
                    programItem.IsEditAble = item.IsUserGroupEditAble ?? false;
                    programItem.IsDeleteAble = item.IsUserGroupDeleteAble ?? false;
                    programItem.IsApprovalAble = item.IsUserGroupApprovalAble ?? false;
                    programItem.IsUnApprovalAble = item.IsUserGroupUnApprovalAble ?? false;
                    programItem.IsVoidAble = item.IsUserGroupVoidAble ?? false;
                    programItem.IsUnVoidAble = item.IsUserGroupUnVoidAble ?? false;
                    programItem.IsExportAble = item.IsUserGroupExportAble ?? false;
                    programItem.IsCrossUnitAble = item.IsUserGroupCrossUnitAble ?? false;
                    programItem.IsPowerUserAble = item.IsUserGroupPowerUserAble ?? false;

                    programItem.IsProgramAddAble = item.GetColumn("IsProgramAddAble") == DBNull.Value ? false : Convert.ToBoolean(item.GetColumn("IsProgramAddAble"));
                    programItem.IsProgramDeleteAble = item.GetColumn("IsProgramDeleteAble") == DBNull.Value ? false : Convert.ToBoolean(item.GetColumn("IsProgramDeleteAble"));
                    programItem.IsProgramEditAble = item.GetColumn("IsProgramEditAble") == DBNull.Value ? false : Convert.ToBoolean(item.GetColumn("IsProgramEditAble"));
                    programItem.IsProgramApprovalAble = item.GetColumn("IsProgramApprovalAble") == DBNull.Value ? false : Convert.ToBoolean(item.GetColumn("IsProgramApprovalAble"));
                    programItem.IsProgramUnApprovalAble = item.GetColumn("IsProgramUnApprovalAble") == DBNull.Value ? false : Convert.ToBoolean(item.GetColumn("IsProgramUnApprovalAble"));
                    programItem.IsProgramVoidAble = item.GetColumn("IsProgramVoidAble") == DBNull.Value ? false : Convert.ToBoolean(item.GetColumn("IsProgramVoidAble"));
                    programItem.IsProgramUnVoidAble = item.GetColumn("IsProgramUnVoidAble") == DBNull.Value ? false : Convert.ToBoolean(item.GetColumn("IsProgramUnVoidAble"));
                    programItem.IsProgramExportAble = item.GetColumn("IsProgramExportAble") == DBNull.Value ? false : Convert.ToBoolean(item.GetColumn("IsProgramExportAble"));
                    programItem.IsProgramCrossUnitAble = item.GetColumn("IsProgramCrossUnitAble") == DBNull.Value ? false : Convert.ToBoolean(item.GetColumn("IsProgramCrossUnitAble"));
                    programItem.IsProgramPowerUserAble = item.GetColumn("IsProgramPowerUserAble") == DBNull.Value ? false : Convert.ToBoolean(item.GetColumn("IsProgramPowerUserAble"));

                    listProgram.Add(programItem);
                }

                Session["ProgramSelecteds"] = listProgram;
                return listProgram;
            }
            set
            {
                if (value == null)
                    Session.Remove("ProgramSelecteds");
                else
                    Session["ProgramSelecteds"] = value;
            }
        }

        protected void grdSelection_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdSelection.DataSource = ProgramSelections;
        }

        protected void grdSelected_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdSelected.DataSource = ProgramSelecteds;
        }

        protected void grdSelection_RowDrop(object sender, GridDragDropEventArgs e)
        {
            if (string.IsNullOrEmpty(e.HtmlElement))
            {
                if (e.DraggedItems[0].OwnerGridID == grdSelection.ClientID)
                {
                    // items are drag from selection to selected grid 
                    if ((e.DestDataItem == null && ProgramSelecteds.Count == 0) ||
                        e.DestDataItem != null &&
                        e.DestDataItem.OwnerGridID == grdSelected.ClientID)
                        MoveProgramItem(e, ProgramSelections, ProgramSelecteds);
                }
            }
        }

        protected void grdSelected_RowDrop(object sender, GridDragDropEventArgs e)
        {
            if (string.IsNullOrEmpty(e.HtmlElement))
            {
                if (e.DraggedItems[0].OwnerGridID == grdSelected.ClientID)
                {
                    // items are drag from selectied to selection grid 
                    if ((e.DestDataItem == null && ProgramSelections.Count == 0) ||
                        e.DestDataItem != null &&
                        e.DestDataItem.OwnerGridID == grdSelection.ClientID)
                        MoveProgramItem(e, ProgramSelecteds, ProgramSelections);
                }
            }
        }

        private void MoveProgramItem(GridDragDropEventArgs e, IList<ProgramItem> sourceList, IList<ProgramItem> destinationList)
        {
            int destinationIndex = -1;
            if (e.DestDataItem != null)
            {
                ProgramItem order = GetProgramItem(destinationList, (string)e.DestDataItem.GetDataKeyValue("ProgramID"));
                destinationIndex = (order != null) ? destinationList.IndexOf(order) : -1;
            }

            foreach (GridDataItem draggedItem in e.DraggedItems)
            {
                ProgramItem tmpProgram = GetProgramItem(sourceList, (string)draggedItem.GetDataKeyValue("ProgramID"));
                if (tmpProgram != null)
                {
                    if (destinationIndex > -1)
                        destinationList.Insert(destinationIndex, tmpProgram);
                    else
                    {
                        destinationList.Add(tmpProgram);
                    }

                    sourceList.Remove(tmpProgram);
                }
            }

            grdSelection.Rebind();
            grdSelected.Rebind();
        }

        private static ProgramItem GetProgramItem(IEnumerable<ProgramItem> programItemToSearchIn, string programID)
        {
            foreach (ProgramItem prog in programItemToSearchIn)
            {
                if (prog.ProgramID == programID)
                    return prog;
            }
            return null;
        }

        #endregion

        protected class ProgramItem
        {
            private string _programID;
            private string _programName;
            private string _programType;
            private string _moduleName;

            private bool _isAddAble;
            private bool _isEditAble;
            private bool _isDeleteAble;
            private bool _isApprovalAble;
            private bool _isUnApprovalAble;
            private bool _isVoidAble;
            private bool _isUnVoidAble;
            private bool _isExportAble;
            private bool _isCrossUnitAble;
            private bool _isPowerUserAble;

            private bool _isProgramAddAble;
            private bool _isProgramDeleteAble;
            private bool _isProgramEditAble;
            private bool _isProgramApprovalAble;
            private bool _isProgramUnApprovalAble;
            private bool _isProgramVoidAble;
            private bool _isProgramUnVoidAble;
            private bool _isProgramExportAble;
            private bool _isProgramCrossUnitAble;
            private bool _isProgramPowerUserAble;

            public string ProgramID
            {
                get { return _programID; }
                set { _programID = value; }
            }
            public string ProgramName
            {
                get { return _programName; }
                set { _programName = value; }
            }
            public string ProgramType
            {
                get { return _programType; }
                set { _programType = value; }
            }
            public string ModuleName
            {
                get { return _moduleName; }
                set { _moduleName = value; }
            }

            public bool IsAddAble
            {
                get { return _isAddAble; }
                set { _isAddAble = value; }
            }
            public bool IsEditAble
            {
                get { return _isEditAble; }
                set { _isEditAble = value; }
            }
            public bool IsDeleteAble
            {
                get { return _isDeleteAble; }
                set { _isDeleteAble = value; }
            }
            public bool IsApprovalAble
            {
                get { return _isApprovalAble; }
                set { _isApprovalAble = value; }
            }
            public bool IsUnApprovalAble
            {
                get { return _isUnApprovalAble; }
                set { _isUnApprovalAble = value; }
            }
            public bool IsVoidAble
            {
                get { return _isVoidAble; }
                set { _isVoidAble = value; }
            }
            public bool IsUnVoidAble
            {
                get { return _isUnVoidAble; }
                set { _isUnVoidAble = value; }
            }
            public bool IsExportAble
            {
                get { return _isExportAble; }
                set { _isExportAble = value; }
            }
            public bool IsCrossUnitAble
            {
                get { return _isCrossUnitAble; }
                set { _isCrossUnitAble = value; }
            }
            public bool IsPowerUserAble
            {
                get { return _isPowerUserAble; }
                set { _isPowerUserAble = value; }
            }

            public bool IsProgramAddAble
            {
                get { return _isProgramAddAble; }
                set { _isProgramAddAble = value; }
            }
            public bool IsProgramEditAble
            {
                get { return _isProgramEditAble; }
                set { _isProgramEditAble = value; }
            }
            public bool IsProgramDeleteAble
            {
                get { return _isProgramDeleteAble; }
                set { _isProgramDeleteAble = value; }
            }
            public bool IsProgramApprovalAble
            {
                get { return _isProgramApprovalAble; }
                set { _isProgramApprovalAble = value; }
            }
            public bool IsProgramUnApprovalAble
            {
                get { return _isProgramUnApprovalAble; }
                set { _isProgramUnApprovalAble = value; }
            }
            public bool IsProgramVoidAble
            {
                get { return _isProgramVoidAble; }
                set { _isProgramVoidAble = value; }
            }
            public bool IsProgramUnVoidAble
            {
                get { return _isProgramUnVoidAble; }
                set { _isProgramUnVoidAble = value; }
            }
            public bool IsProgramExportAble
            {
                get { return _isProgramExportAble; }
                set { _isProgramExportAble = value; }
            }
            public bool IsProgramCrossUnitAble
            {
                get { return _isProgramCrossUnitAble; }
                set { _isProgramCrossUnitAble = value; }
            }
            public bool IsProgramPowerUserAble
            {
                get { return _isProgramPowerUserAble; }
                set { _isProgramPowerUserAble = value; }
            }
        }

        protected void grdSelected_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
                e.Item.PreRender += new EventHandler(grdSelected_ItemPreRender);
        }

        private void grdSelected_ItemPreRender(object sender, EventArgs e)
        {
            GridDataItem dataItem = sender as GridDataItem;
            if (dataItem == null) return;

            (dataItem["IsAddAble"].FindControl("chkIsAddAble") as CheckBox).Visible = dataItem["IsProgramAddAble"].Text.Equals("True");
            (dataItem["IsEditAble"].FindControl("chkIsEditAble") as CheckBox).Visible = dataItem["IsProgramEditAble"].Text.Equals("True");
            (dataItem["IsDeleteAble"].FindControl("chkIsDeleteAble") as CheckBox).Visible = dataItem["IsProgramDeleteAble"].Text.Equals("True");
            (dataItem["IsApprovalAble"].FindControl("chkIsApprovalAble") as CheckBox).Visible = dataItem["IsProgramApprovalAble"].Text.Equals("True");
            (dataItem["IsUnApprovalAble"].FindControl("chkIsUnApprovalAble") as CheckBox).Visible = dataItem["IsProgramUnApprovalAble"].Text.Equals("True");
            (dataItem["IsVoidAble"].FindControl("chkIsVoidAble") as CheckBox).Visible = dataItem["IsProgramVoidAble"].Text.Equals("True");
            (dataItem["IsUnVoidAble"].FindControl("chkIsUnVoidAble") as CheckBox).Visible = dataItem["IsProgramUnVoidAble"].Text.Equals("True");
            (dataItem["IsExportAble"].FindControl("chkIsExportAble") as CheckBox).Visible = dataItem["IsProgramExportAble"].Text.Equals("True");
            (dataItem["IsCrossUnitAble"].FindControl("chkIsCrossUnitAble") as CheckBox).Visible = dataItem["IsProgramCrossUnitAble"].Text.Equals("True");
            (dataItem["IsPowerUserAble"].FindControl("chkIsPowerUserAble") as CheckBox).Visible = dataItem["IsProgramPowerUserAble"].Text.Equals("True");
        }
        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Session["ProgramSelections"] = null;
            grdSelection.Rebind();
        }
    }
}