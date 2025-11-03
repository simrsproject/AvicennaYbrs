using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class JournalGroupDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "JournalGroupSearch.aspx";
            UrlPageList = "JournalGroupList.aspx";

            WindowSearch.Height = 200;

            ProgramID = AppConstant.Program.JournalModuleGroup;

            if (!IsPostBack)
            {
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("General", JournalType.General));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("Income", JournalType.Income));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("Prescription", JournalType.Prescription));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("PrescriptionReturn", JournalType.PrescriptionReturn));
                //listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("SpectaclePrescription", JournalType.SpectaclePrescription));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("PaymentReturn", JournalType.PaymentReturn));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("DownPaymentReturn", JournalType.DownPaymentReturn));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("DownPayment", JournalType.DownPayment));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("Payment", JournalType.Payment));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("ARPayment", JournalType.ARPayment));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("AR", JournalType.AR));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("ARCreating", JournalType.ARCreating));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("CashTransaction", JournalType.CashTransaction));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("PurchaseOrderReceived", JournalType.PurchaseOrderReceived));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("PurchaseOrderReturned", JournalType.PurchaseOrderReturned));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("ConsignmentReceived", JournalType.ConsignmentReceived));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("ConsignmentReturned", JournalType.ConsignmentReturned));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("ConsignmentInvoicing", JournalType.ConsignmentInvoicing));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("Distribution", JournalType.Distribution));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("DistributionConfirmed", JournalType.DistributionConfirmed));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("InventoryIssue", JournalType.InventoryIssue));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("AP", JournalType.AP));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("APPayment", JournalType.APPayment));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("PhysicianPayment", JournalType.PhysicianPayment));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("PhysicianFeeVerification", JournalType.PhysicianFeeVerification));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("FixAsset", JournalType.FixAsset));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("SalesToBranch", JournalType.SalesToBranch));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("GuarantorDeposit", JournalType.GuarantorDeposit));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("InventoryStockAdjustment", JournalType.InventoryStockAdjustment));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("InventoryStockOpname", JournalType.InventoryStockOpname));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("InventoryProduction", JournalType.InventoryProduction));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("PatientReceivable", JournalType.PatientReceivable));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("CashierCorrection", JournalType.CashierCorrection));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("Sales", JournalType.Sales));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("SalesReturn", JournalType.SalesReturn));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("ARCustomer", JournalType.ARCustomer));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("ARCustomerPayment", JournalType.ARCustomerPayment));

                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("AssetAuction", JournalType.AssetAuction));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("AssetDestruction", JournalType.AssetDestruction));
                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("AssetMovement", JournalType.AssetMovement));

                listJournalCode.Items.Add(new Telerik.Web.UI.RadListBoxItem("Income Summary", "99"));
            }
        }

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            ViewState["id"] = 0;

            OnPopulateEntryControl(new JournalGroup());

            chkIsActive.Checked = true;

            foreach (Telerik.Web.UI.RadListBoxItem item in listJournalCode.Items)
            {
                if (item.Checkable && item.Checked) item.Checked = false;
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new JournalGroup();
            if (entity.LoadByPrimaryKey(ViewState["id"].ToInt()))
            {
                JournalGroupDetailCollection detail = new JournalGroupDetailCollection();
                detail.Query.Where(detail.Query.JournalGroupID == ViewState["id"].ToInt());
                detail.LoadAll();
                detail.MarkAllAsDeleted();
                entity.MarkAsDeleted();

                using (var trans = new esTransactionScope())
                {
                    entity.Save();
                    detail.Save();
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
            var entity = new JournalGroup();
            if (entity.LoadByPrimaryKey(ViewState["id"].ToInt()))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new JournalGroup();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new JournalGroup();
            if (entity.LoadByPrimaryKey(ViewState["id"].ToInt()))
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
            auditLogFilter.PrimaryKeyData = string.Format("JournalGroupID='{0}'", ViewState["id"].ToString());
            auditLogFilter.TableName = "JournalGroup";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new JournalGroup();
            if (parameters.Length > 0)
            {
                int id = parameters[0].ToInt();

                if (!parameters[0].Equals(string.Empty)) entity.LoadByPrimaryKey(id);
            }
            else
            {
                entity.LoadByPrimaryKey(ViewState["id"].ToInt());
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(Temiang.Dal.Core.esEntity entity)
        {
            var group = (JournalGroup)entity;

            ViewState["id"] = group.JournalGroupID ?? 0;
            txtGroupName.Text = group.JournalGroupName;
            txtNotes.Text = group.Notes;
            chkIsActive.Checked = group.IsActive ?? false;

            JournalGroupDetails = null;
            var coll = JournalGroupDetails;

            foreach (var c in coll)
            {
                var item = listJournalCode.Items.Where(i => i.Value == c.JournalJournalCode).SingleOrDefault();
                if (item == null) continue;
                item.Checked = true;
            }

            PopulateItemJournalGroupUserGrid();
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemJournalGroupUser(newVal);
        }
        #endregion

        #region Private Method Standard
        private void SetEntityValue(JournalGroup entity)
        {
            entity.JournalGroupID = ViewState["id"].ToInt();
            entity.JournalGroupName = txtGroupName.Text;
            entity.Notes = txtNotes.Text;
            entity.IsActive = chkIsActive.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(JournalGroup entity)
        {
            using (var trans = new Temiang.Dal.Interfaces.esTransactionScope())
            {
                entity.Save();

                ViewState["id"] = entity.JournalGroupID;

                var codes = JournalGroupDetails;

                foreach (Telerik.Web.UI.RadListBoxItem item in listJournalCode.Items)
                {
                    var code = codes.Where(d => d.JournalJournalCode == item.Value).SingleOrDefault();

                    BusinessObject.JournalGroupDetail detail;
                    if (code == null)
                    {
                        if (item.Checked)
                        {
                            detail = codes.AddNew();
                            detail.JournalGroupID = entity.JournalGroupID;
                            detail.JournalJournalCode = item.Value;
                            detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            detail.LastUpdateDateTime = DateTime.Now;
                        }
                    }
                    else
                    {
                        if (!item.Checked) code.MarkAsDeleted();
                    }
                }

                codes.Save();

                foreach (var user in JournalGroupUsers)
                {
                    user.JournalGroupID = entity.JournalGroupID;

                    if (user.es.IsAdded || user.es.IsModified)
                    {
                        user.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        user.LastUpdateDateTime = DateTime.Now;
                    }
                }

                JournalGroupUsers.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new JournalGroupQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.JournalGroupID > ViewState["id"].ToInt());
                que.OrderBy(que.JournalGroupID.Ascending);
            }
            else
            {
                que.Where(que.JournalGroupID < ViewState["id"].ToInt());
                que.OrderBy(que.JournalGroupID.Descending);
            }

            var entity = new JournalGroup();
            entity.Load(que);

            OnPopulateEntryControl(entity);
        }
        #endregion

        private JournalGroupDetailCollection JournalGroupDetails
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collJournalGroupDetail"];
                    if (obj != null) return ((JournalGroupDetailCollection)(obj));
                }

                var query = new JournalGroupDetailQuery("a");
                query.Where(query.JournalGroupID == ViewState["id"].ToInt());
                query.OrderBy(query.JournalDetailID.Ascending);

                var coll = new JournalGroupDetailCollection();
                coll.Load(query);
                Session["collJournalGroupDetail"] = coll;
                return coll;
            }
            set { Session["collJournalGroupDetail"] = value; }
        }

        #region Record Detail Method Function JournalGroupUser

        private JournalGroupUserCollection JournalGroupUsers
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collJournalGroupUser"];
                    if (obj != null) return ((JournalGroupUserCollection)(obj));
                }

                JournalGroupUserCollection coll = new JournalGroupUserCollection();

                JournalGroupUserQuery query = new JournalGroupUserQuery("a");
                AppUserQuery asri = new AppUserQuery("b");

                query.Select(query, asri.UserName.As("refToAppUser_UserName"));
                query.InnerJoin(asri).On(query.UserID == asri.UserID);
                query.Where(query.JournalGroupID == ViewState["id"].ToInt());
                coll.Load(query);

                Session["collJournalGroupUser"] = coll;
                return coll;
            }
            set
            {
                Session["collJournalGroupUser"] = value;
            }
        }

        private void RefreshCommandItemJournalGroupUser(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdUserAccess.Columns[0].Visible = isVisible;
            grdUserAccess.Columns[grdUserAccess.Columns.Count - 1].Visible = isVisible;

            grdUserAccess.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdUserAccess.Rebind();
        }

        private void PopulateItemJournalGroupUserGrid()
        {
            //Display Data Detail
            JournalGroupUsers = null; //Reset Record Detail
            grdUserAccess.DataSource = JournalGroupUsers; //Requery
            grdUserAccess.MasterTableView.IsItemInserted = false;
            grdUserAccess.MasterTableView.ClearEditItems();
            grdUserAccess.DataBind();
        }

        protected void grdUserAccess_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdUserAccess.DataSource = JournalGroupUsers;
        }

        protected void grdUserAccess_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            int type = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][JournalGroupUserMetadata.ColumnNames.JournalUserID]);

            var entity = FindItemBridging(type);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdUserAccess_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            var type = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][JournalGroupUserMetadata.ColumnNames.JournalUserID]);

            var entity = FindItemBridging(type);
            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdUserAccess_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = JournalGroupUsers.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdUserAccess.Rebind();
        }

        private JournalGroupUser FindItemBridging(int type)
        {
            var coll = JournalGroupUsers;
            return coll.FirstOrDefault(rec => rec.JournalUserID.Equals(type));
        }

        private void SetEntityValue(JournalGroupUser entity, GridCommandEventArgs e)
        {
            JournalGroupUserItem userControl = (JournalGroupUserItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.JournalUserID = userControl.JournalUserID;
                entity.JournalGroupID = 0;
                entity.UserID = userControl.UserID;
                entity.UserName = userControl.UserName;
            }
        }

        #endregion
    }
}
