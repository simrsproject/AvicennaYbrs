using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Linq;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ApprovalRangeDetail : BasePageDetail
    {
        private void SetEntityValue(ApprovalRange entity)
        {
            entity.ApprovalRangeID = txtApprovalRangeID.Text.ToUpper();
            entity.AmountFrom = Convert.ToDecimal(txtAmountFrom.Value);
            entity.SRItemType = cboSRItemType.SelectedValue;
            entity.ItemGroupID = cboItemGroupID.SelectedValue;
            entity.TransactionCode = cboTransactionCode.SelectedValue;
            var highestApprovalLevel = 0;
            foreach (ApprovalRangeUser item in ApprovalRangeUsers)
            {
                if (highestApprovalLevel < item.ApprovalLevel)
                    highestApprovalLevel = item.ApprovalLevel.ToInt();

                item.ApprovalRangeID = txtApprovalRangeID.Text;

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }
            entity.ApprovalLevelFinal = highestApprovalLevel;
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ApprovalRangeQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ApprovalRangeID > txtApprovalRangeID.Text);
                que.OrderBy(que.ApprovalRangeID.Ascending);
            }
            else
            {
                que.Where(que.ApprovalRangeID < txtApprovalRangeID.Text);
                que.OrderBy(que.ApprovalRangeID.Descending);
            }

            var entity = new ApprovalRange();
            entity.Load(que);

            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ApprovalRange();
            if (parameters.Length > 0)
            {
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(parameters[0]);
            }
            else
                entity.LoadByPrimaryKey(txtApprovalRangeID.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var apprRange = (ApprovalRange)entity;
            txtApprovalRangeID.Text = apprRange.ApprovalRangeID;
            txtAmountFrom.Value = Convert.ToDouble (apprRange.AmountFrom);
            ComboBox.SelectedValue(cboSRItemType, apprRange.SRItemType);
            ComboBox.SelectedValue(cboTransactionCode, apprRange.TransactionCode);
            ComboBox.PopulateWithOneItemGroup(cboItemGroupID, apprRange.ItemGroupID);
            PopulateGridDetail();
        }


        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ApprovalRange());
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
            auditLogFilter.PrimaryKeyData = string.Format("ApprovalRangeID='{0}'", txtApprovalRangeID.Text.Trim());
            auditLogFilter.TableName = "ApprovalRange";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "ApprovalRangeSearch.aspx";
            UrlPageList = "ApprovalRangeList.aspx";

            WindowSearch.Height = 200;

            ProgramID = AppConstant.Program.ApprovalRange;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
                ComboBox.PopulateWithTransactionCodeWithApprovalLevel(cboTransactionCode);
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ApprovalRange();
            if (entity.LoadByPrimaryKey(txtApprovalRangeID.Text))
            {
                using (var trans = new esTransactionScope())
                {
                    ApprovalRangeUsers.MarkAllAsDeleted();
                    ApprovalRangeUsers.Save();

                    entity.MarkAsDeleted();
                    entity.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new ApprovalRange();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(ApprovalRange entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                ApprovalRangeUsers.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new ApprovalRange();
            if (entity.LoadByPrimaryKey(txtApprovalRangeID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        protected override void OnInitializeAjaxManager(RadAjaxManager ajaxManager)
        {
            ajaxManager.AjaxSettings.AddAjaxSetting(cboSRItemType,cboItemGroupID);
            ajaxManager.AjaxSettings.AddAjaxSetting(grdApprovalRangeUser, grdApprovalRangeUser);
        }

        #endregion

        #region Record Detail Method Function

        private ApprovalRangeUserCollection ApprovalRangeUsers
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collApprovalRangeUser"];
                    if (obj != null)
                        return ((ApprovalRangeUserCollection)(obj));
                }

                var coll = new ApprovalRangeUserCollection();
                var query = new ApprovalRangeUserQuery("a");

                var uq = new AppUserQuery("b");
                query.InnerJoin(uq).On(query.UserID == uq.UserID);

                query.Where(query.ApprovalRangeID == txtApprovalRangeID.Text);

                query.Select
                    (
                        query,
                        uq.UserName.As("refToAppUser_UserName")
                    );

                query.OrderBy(query.ApprovalLevel.Ascending);

                coll.Load(query);

                Session["collApprovalRangeUser"] = coll;
                return coll;
            }
            set { Session["collApprovalRangeUser"] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdApprovalRangeUser.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                ApprovalRangeUsers = null;

            //Perbaharui tampilan dan data
            grdApprovalRangeUser.Rebind();
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            ApprovalRangeUsers = null; //Reset Record Detail
            grdApprovalRangeUser.DataSource = ApprovalRangeUsers;
            grdApprovalRangeUser.MasterTableView.IsItemInserted = false;
            grdApprovalRangeUser.MasterTableView.ClearEditItems();
            grdApprovalRangeUser.DataBind();
        }

        protected void grdApprovalRangeUser_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdApprovalRangeUser.DataSource = ApprovalRangeUsers;
        }

        protected void grdApprovalRangeUser_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            var userID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ApprovalRangeUserMetadata.ColumnNames.UserID]);
            var apprLevel = Convert.ToInt16(item.OwnerTableView.DataKeyValues[item.ItemIndex][ApprovalRangeUserMetadata.ColumnNames.ApprovalLevel]);
            var entity = FindItemGrid(userID, apprLevel);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdApprovalRangeUser_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = ApprovalRangeUsers.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdApprovalRangeUser.Rebind();
        }

        private void SetEntityValue(ApprovalRangeUser entity, GridCommandEventArgs e)
        {
            var userControl = (ApprovalRangeUserDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.UserID = userControl.UserID;
                entity.UserName = userControl.UserName;
                entity.ApprovalLevel = userControl.ApprovalLevel;
            }
        }

        private ApprovalRangeUser FindItemGrid(string userID, int apprLevel)
        {
            var coll = ApprovalRangeUsers;
            ApprovalRangeUser retval = null;
            foreach (ApprovalRangeUser rec in coll)
            {
                if (rec.UserID.Equals(userID) && rec.ApprovalLevel == apprLevel)
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        #endregion

        protected void cboItemGroupID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            ItemGroupQuery query = new ItemGroupQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.ItemGroupID,
                    query.ItemGroupName
                );
            query.Where
                (
                    query.Or
                        (
                            query.ItemGroupID.Like(searchText),
                            query.ItemGroupName.Like(searchText)
                        ),
                        query.IsActive == true,
                        query.SRItemType == cboSRItemType.SelectedValue
                );

            cboItemGroupID.DataSource = query.LoadDataTable();
            cboItemGroupID.DataBind();
        }

        protected void cboItemGroupID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemGroupName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemGroupID"].ToString();
        }

        protected void cboSRItemType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboItemGroupID.Text = string.Empty;
            cboItemGroupID.SelectedValue = null;
        }
    }
}