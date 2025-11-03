using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class BpjsPackageDetail : BasePageDetail
    {
        #region Page Event
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "BpjsPackageSearch.aspx";
            UrlPageList = "BpjsPackageList.aspx";

            ProgramID = AppConstant.Program.BpjsPackage;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion

        private void SetEntityValue(BpjsPackage entity)
        {
            entity.PackageID = txtPackageID.Text;
            entity.PackageName = txtPackageName.Text;
            entity.IsActive = chkIsActive.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new BpjsPackageQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.PackageID > txtPackageID.Text);
                que.OrderBy(que.PackageID.Ascending);
            }
            else
            {
                que.Where(que.PackageID < txtPackageID.Text);
                que.OrderBy(que.PackageID.Descending);
            }
            var entity = new BpjsPackage();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new BpjsPackage();
            if (parameters.Length > 0)
            {
                String packageId = (String)parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(packageId);
            }
            else
                entity.LoadByPrimaryKey(txtPackageID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var bpjsPackage = (BpjsPackage)entity;
            txtPackageID.Text = bpjsPackage.PackageID;
            txtPackageName.Text = bpjsPackage.PackageName;
            chkIsActive.Checked = bpjsPackage.IsActive ?? false;

            //Display Data Detail
            PopulateGridDetail();
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new BpjsPackage());
            chkIsActive.Checked = true;
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
            auditLogFilter.PrimaryKeyData = string.Format("PackageID='{0}'", txtPackageID.Text.Trim());
            auditLogFilter.TableName = "BpjsPackage";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtPackageID.Enabled = (newVal == AppEnum.DataMode.New);
            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new BpjsPackage();
            entity.LoadByPrimaryKey(txtPackageID.Text);
            entity.MarkAsDeleted();

            var collDt = new BpjsPackageTariffCollection();
            collDt.Query.Where(collDt.Query.PackageID == txtPackageID.Text);
            collDt.MarkAllAsDeleted();

            using (esTransactionScope trans = new esTransactionScope())
            {
                collDt.Save();
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new BpjsPackage();
            if (entity.LoadByPrimaryKey(txtPackageID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new BpjsPackage();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(BpjsPackage entity)
        {
            foreach (BpjsPackageTariff item in BpjsPackageTariffs)
                item.PackageID = entity.PackageID;

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                BpjsPackageTariffs.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new BpjsPackage();
            if (entity.LoadByPrimaryKey(txtPackageID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion

        #region Record Detail Method Function

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                BpjsPackageTariffs = null;

            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private BpjsPackageTariffCollection BpjsPackageTariffs
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collBpjsPackageTariff"];
                    if (obj != null)
                        return ((BpjsPackageTariffCollection)(obj));
                }

                var coll = new BpjsPackageTariffCollection();
                var query = new BpjsPackageTariffQuery("a");
                var cls = new AppStandardReferenceItemQuery("b");
                query.InnerJoin(cls).On(query.ClassID == cls.ItemID && cls.StandardReferenceID == "ClassRL");

                string packageId = txtPackageID.Text;
                query.Select(query, cls.ItemName.As("refToClass_ClassName"));
                query.Where(query.PackageID == packageId);
                query.OrderBy(query.StartingDate.Ascending, query.ClassID.Ascending);
                coll.Load(query);

                Session["collBpjsPackageTariff"] = coll;
                return coll;
            }
            set
            {
                Session["collBpjsPackageTariff"] = value;
            }
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            BpjsPackageTariffs = null; //Reset Record Detail
            grdItem.DataSource = BpjsPackageTariffs;
            grdItem.DataBind();
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = BpjsPackageTariffs;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            DateTime startingDate = Convert.ToDateTime(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][BpjsPackageTariffMetadata.ColumnNames.StartingDate]);
            String clsId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][BpjsPackageTariffMetadata.ColumnNames.ClassID]);
            BpjsPackageTariff entity = FindBpjsPackageTariff(startingDate, clsId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            DateTime startingDate = Convert.ToDateTime(item.OwnerTableView.DataKeyValues[item.ItemIndex][BpjsPackageTariffMetadata.ColumnNames.StartingDate]);
            String clsId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][BpjsPackageTariffMetadata.ColumnNames.ClassID]);
            BpjsPackageTariff entity = FindBpjsPackageTariff(startingDate, clsId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            BpjsPackageTariff entity = BpjsPackageTariffs.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdItem.Rebind();
        }

        private BpjsPackageTariff FindBpjsPackageTariff(DateTime startingDate, string clsId)
        {
            BpjsPackageTariffCollection coll = BpjsPackageTariffs;
            BpjsPackageTariff retEntity = null;
            foreach (BpjsPackageTariff rec in coll)
            {
                if (rec.StartingDate.Equals(startingDate) && rec.ClassID.Equals(clsId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(BpjsPackageTariff entity, GridCommandEventArgs e)
        {
            var userControl = (BpjsPackageTariffDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.PackageID = txtPackageID.Text;
                entity.StartingDate = userControl.StartingDate;
                entity.ClassID = userControl.ClassId;
                entity.ClassName = userControl.ClassName;
                entity.Price = userControl.Price;

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        #endregion
    }
}