using System;
using System.Collections.Generic;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Linq;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ItemIdiDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ItemIDI;

            // Url Search & List
            UrlPageSearch = "ItemIdiSearch.aspx";
            UrlPageList = "ItemIdiList.aspx";

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
            OnPopulateEntryControl(new ItemIdi());
            txtIdiCode.ReadOnly = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            txtIdiCode.Text = Helper.GetItemIdiCode();

            var entity = new ItemIdi();
            if (entity.LoadByPrimaryKey(txtIdiCode.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new ItemIdi();
            entity.AddNew();

            string msg = ValidateNewItemSmf();
            if (string.IsNullOrEmpty(msg))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = msg;
            }

            
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new ItemIdi();
            if (entity.LoadByPrimaryKey(txtIdiCode.Text))
            {
                string msg = ValidateNewItemSmf();
                if (string.IsNullOrEmpty(msg))
                {
                    SetEntityValue(entity);
                    SaveEntity(entity);
                }
                else {
                    args.MessageText = msg;
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        private string ValidateNewItemSmf() {
            var msg = string.Empty;
            if (ItemIdiItemSmfs.Count > 0)
            {
                var collAll = new ItemIdiItemSmfCollection();
                var idismf = new ItemIdiItemSmfQuery("idismf");
                var item = new ItemQuery("item");
                var smf = new SmfQuery("smf");
                var idi = new ItemIdiQuery("idi");

                idismf.InnerJoin(item).On(idismf.ItemID == item.ItemID)
                    .InnerJoin(smf).On(idismf.SmfID == smf.SmfID)
                    .InnerJoin(idi).On(idismf.IdiCode == idi.IdiCode)
                    .Select(idismf, item.ItemName.As("refToItem_ItemName"), smf.SmfName.As("refToSmf_SmfName"), idi.IdiName.As("refToItemIdi_IdiName"));

                if (collAll.Load(idismf))
                {
                    foreach (var imf in ItemIdiItemSmfs.Where(i => i.es.IsAdded == true)) {
                        var imfExistings = collAll.Where(c => c.ItemID == imf.ItemID && c.SmfID == imf.SmfID);
                        foreach (var imfExisting in imfExistings) {
                            msg += string.Format("Item {0}-{1} with smf {2} has already been mapped to idi {3}-{4}{5}",
                                imf.ItemID, imf.ItemName, imf.SmfName, imfExisting.IdiCode, imfExisting.IdiName, Environment.NewLine);
                        }
                    }
                }
            }
            return msg;
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
            auditLogFilter.PrimaryKeyData = string.Format("IdiCode='{0}'", txtIdiCode.Text.Trim());
            auditLogFilter.TableName = "ItemIdi";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtIdiCode.ReadOnly = true;
            RefreshCommandItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ItemIdi();
            if (parameters.Length > 0)
            {
                String id = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(id);
            }
            else
            {
                entity.LoadByPrimaryKey(txtIdiCode.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var idi = (ItemIdi)entity;
            txtIdiCode.Text= idi.IdiCode;
            txtIdiName.Text= idi.IdiName;
            txtIcd9Cm.Text = idi.Icd9Cm;
            txtF_1.Value= Convert.ToDouble(idi.F1);
            txtF_2_1.Value= Convert.ToDouble(idi.F21);
            txtF_2_2.Value= Convert.ToDouble(idi.F22);
            txtF_2_3.Value= Convert.ToDouble(idi.F23);
            txtF_3.Value= Convert.ToDouble(idi.F3);
            txtF_4.Value= Convert.ToDouble(idi.F4);
            txtRvu.Value= Convert.ToDouble(idi.Rvu);
            txtPrice.Value= Convert.ToDouble(idi.Price);
            txtSpecialist.Text = idi.Specialist;

            PopulateItemGrid();
            PopulateItemSmfGrid();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(ItemIdi entity)
        {
            entity.IdiCode = txtIdiCode.Text;
            entity.IdiName = txtIdiName.Text;
            entity.Icd9Cm = txtIcd9Cm.Text;
            entity.F1 = Convert.ToDecimal(txtF_1.Value);
            entity.F21 = Convert.ToDecimal(txtF_2_1.Value);
            entity.F22 = Convert.ToDecimal(txtF_2_2.Value);
            entity.F23 = Convert.ToDecimal(txtF_2_3.Value);
            entity.F3 = Convert.ToDecimal(txtF_3.Value);
            entity.F4 = Convert.ToDecimal(txtF_4.Value);
            entity.Rvu = Convert.ToDecimal(txtRvu.Value);
            entity.Price = Convert.ToDecimal(txtPrice.Value);
            entity.Specialist = txtSpecialist.Text;
            entity.LastUpdateDateTime = DateTime.Now;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

            foreach (var item in ItemIdiProcedures)
            {
                item.IdiCode = txtIdiCode.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }

            foreach (var item in ItemIdiItemSmfs) {
                item.IdiCode = entity.IdiCode;
                if (item.es.IsAdded) {
                    item.CreateDateTime = DateTime.Now;
                    item.CreateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }
                if (item.es.IsModified) {
                    item.LastUpdateDateTime = DateTime.Now;
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }
            }
        }

        private void SaveEntity(ItemIdi entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                ItemIdiProcedures.Save();
                ItemIdiItemSmfs.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ItemIdiQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.IdiCode > txtIdiCode.Text);
                que.OrderBy(que.IdiCode.Ascending);
            }
            else
            {
                que.Where(que.IdiCode < txtIdiCode.Text);
                que.OrderBy(que.IdiCode.Descending);
            }

            var entity = new ItemIdi();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function of Procedure
        private ItemIdiProcedureCollection ItemIdiProcedures
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemIdiProcedure"];
                    if (obj != null)
                    {
                        return ((ItemIdiProcedureCollection)(obj));
                    }
                }

                var coll = new ItemIdiProcedureCollection();
                var query = new ItemIdiProcedureQuery("a");
                var proc = new ProcedureQuery("b");
                query.InnerJoin(proc).On(proc.ProcedureID == query.ProcedureID);
                query.Select
                    (
                        query, proc.ProcedureName.As("refToProcedure_ProcedureName")
                    );
                query.Where(query.IdiCode == txtIdiCode.Text);
                coll.Load(query);

                Session["collItemIdiProcedure"] = coll;
                return coll;
            }
            set
            {
                Session["collItemIdiProcedure"] = value;
            }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;
            grdItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdItem.Rebind();


            grdIdiItemSmf.Columns[grdIdiItemSmf.Columns.Count - 1].Visible = isVisible;
            grdIdiItemSmf.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdIdiItemSmf.Rebind();
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            ItemIdiProcedures = null; //Reset Record Detail
            grdItem.DataSource = ItemIdiProcedures; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private ItemIdiProcedure FindItem(String id)
        {
            ItemIdiProcedureCollection coll = ItemIdiProcedures;
            ItemIdiProcedure retEntity = null;
            foreach (ItemIdiProcedure rec in coll)
            {
                if (rec.ProcedureID.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = ItemIdiProcedures;
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String id =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemIdiProcedureMetadata.ColumnNames.ProcedureID]);
            ItemIdiProcedure entity = FindItem(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            ItemIdiProcedure entity = ItemIdiProcedures.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(ItemIdiProcedure entity, GridCommandEventArgs e)
        {
            var userControl = (ItemIdiDetailItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ProcedureID = userControl.ProcedureID;
                entity.ProcedureName = userControl.ProcedureName;
            }
        }
        #endregion

        #region Record Detail Method Function of Item SMF
        private ItemIdiItemSmfCollection ItemIdiItemSmfs
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemIdiItemSmf"];
                    if (obj != null)
                    {
                        return ((ItemIdiItemSmfCollection)(obj));
                    }
                }

                var coll = new ItemIdiItemSmfCollection();
                var query = new ItemIdiItemSmfQuery("a");
                var item = new ItemQuery("b");
                var smf = new SmfQuery("c");
                query.InnerJoin(item).On(query.ItemID == item.ItemID)
                    .InnerJoin(smf).On(query.SmfID == smf.SmfID)
                    .Select
                    (
                        query, 
                        item.ItemName.As("refToItem_ItemName"),
                        smf.SmfName.As("refToSmf_SmfName")
                    );
                query.Where(query.IdiCode == txtIdiCode.Text);
                coll.Load(query);

                Session["collItemIdiItemSmf"] = coll;
                return coll;
            }
            set
            {
                Session["collItemIdiItemSmf"] = value;
            }
        }

        private void PopulateItemSmfGrid()
        {
            //Display Data Detail
            ItemIdiItemSmfs = null; //Reset Record Detail
            grdIdiItemSmf.Rebind();
        }

        private ItemIdiItemSmf FindItemSmf(string ItemID, string SmfID)
        {
            return ItemIdiItemSmfs.Where(x => x.ItemID == ItemID && x.SmfID == SmfID).FirstOrDefault();
        }

        protected void grdIdiItemSmf_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdIdiItemSmf.DataSource = ItemIdiItemSmfs;
        }

        protected void grdIdiItemSmf_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            string itemid = item.GetDataKeyValue(ItemIdiItemSmfMetadata.ColumnNames.ItemID).ToString();
            string smfid = item.GetDataKeyValue(ItemIdiItemSmfMetadata.ColumnNames.SmfID).ToString();

            ItemIdiItemSmf entity = FindItemSmf(itemid, smfid);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdIdiItemSmf_InsertCommand(object source, GridCommandEventArgs e)
        {
            ItemIdiItemSmf entity = ItemIdiItemSmfs.AddNew();
            SetItemSmfValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdIdiItemSmf.Rebind();
        }

        private void SetItemSmfValue(ItemIdiItemSmf entity, GridCommandEventArgs e)
        {
            var userControl = (ItemIdiDetailItemSmf)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.SmfID = userControl.SmfID;
                entity.SmfName = userControl.SmfName;
            }
        }
        #endregion

    }
}