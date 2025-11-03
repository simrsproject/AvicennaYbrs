using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class CasemixDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CasemixProcedureGroup;

            UrlPageSearch = "CasemixSearch.aspx";
            UrlPageList = "CasemixList.aspx";

            if (!IsCallback)
            {
                //For Grid Detail
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.Item, Page);
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.ItemGroup, Page);
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = PathwayItems;
        }

        private CasemixPathwayItemCollection PathwayItems
        {
            get
            {
                string sessName = "collCasemixPathwayItemCollection";
                if (IsPostBack)
                {
                    object obj = Session[sessName];
                    if (obj != null) return ((CasemixPathwayItemCollection)(obj));
                }

                var coll = new CasemixPathwayItemCollection();

                var item = new CasemixPathwayItemQuery("a");
                var pathway = new CasemixPathwayQuery("b");
                var it = new ItemQuery("c");
                var ig = new ItemGroupQuery("d");

                item.Select(item, 
                    it.ItemName.As("refTo_Item_ItemName"),
                    ig.ItemGroupName.As("refTo_ItemGroup_ItemGroupName")
                    );
                item.InnerJoin(pathway).On(item.PathwayID == pathway.PathwayID);
                item.LeftJoin(it).On(item.ItemID == it.ItemID);
                item.LeftJoin(ig).On(it.ItemGroupID == ig.ItemGroupID);
                item.Where(item.PathwayID == txtPathwayID.Text);
                item.OrderBy(ig.ItemGroupName.Ascending, it.ItemName.Ascending);

                coll.Load(item);

                Session[sessName] = coll;
                return coll;
            }
            set
            {
                string sessName = "collCasemixPathwayItemCollection";
                Session[sessName] = value;
            }
        }

        private void RefreshCommandItemPathwayItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdList.Columns[0].Visible = isVisible;
            grdList.Columns[grdList.Columns.Count - 1].Visible = isVisible;

            grdList.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdList.Rebind();
        }

        private void PopulatePathwayItemGrid()
        {
            //Display Data Detail
            PathwayItems = null; //Reset Record Detail
            grdList.DataSource = PathwayItems; //Requery
            grdList.MasterTableView.IsItemInserted = false;
            grdList.MasterTableView.ClearEditItems();
            grdList.DataBind();
        }

        protected void grdList_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            var pathwayID = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][CasemixPathwayItemMetadata.ColumnNames.PathwayID].ToString();
            var itemID = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][CasemixPathwayItemMetadata.ColumnNames.ItemID].ToString();

            var entity = FindPathwayItem(pathwayID, itemID);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdList_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            var pathwayID = item.OwnerTableView.DataKeyValues[item.ItemIndex][CasemixPathwayItemMetadata.ColumnNames.PathwayID].ToString();
            var itemID = item.OwnerTableView.DataKeyValues[item.ItemIndex][CasemixPathwayItemMetadata.ColumnNames.ItemID].ToString();
            var entity = FindPathwayItem(pathwayID, itemID);
            if (entity != null)
            {
                entity.MarkAsDeleted();
            }
        }

        protected void grdList_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = PathwayItems.AddNew();
            if (!string.IsNullOrEmpty(txtPathwayID.Text)) entity.PathwayID = txtPathwayID.Text;

            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdList.Rebind();
        }

        private CasemixPathwayItem FindPathwayItem(string pathwayID, string itemID)
        {
            var coll = PathwayItems;
            return coll.FirstOrDefault(rec => rec.PathwayID == pathwayID && rec.ItemID == itemID);
        }

        private void SetEntityValue(CasemixPathwayItem entity, GridCommandEventArgs e)
        {
            var userControl = (CasemixItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.AssesmentName;
                var i = new Item();
                i.LoadByPrimaryKey(entity.ItemID);
                var ig = new ItemGroup();
                ig.LoadByPrimaryKey(i.ItemGroupID);
                entity.ItemGroupName = ig.ItemGroupName;
                entity.Notes = userControl.Notes;
                entity.IsActive = userControl.IsActive;
            }
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtPathwayID.ReadOnly = (newVal != AppEnum.DataMode.New);

            RefreshCommandItemPathwayItem(newVal);
            RefreshCommandItemPathwayDiagnoseItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new CasemixPathway();
            if (parameters.Length > 0)
            {
                if (!parameters[0].Equals(string.Empty)) entity.LoadByPrimaryKey((String)parameters[0]);
                else entity.LoadByPrimaryKey(txtPathwayID.Text);
            }
            else entity.LoadByPrimaryKey(txtPathwayID.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var pathway = (CasemixPathway)entity;

            txtPathwayID.Text = pathway.PathwayID;
            txtPathwayName.Text = pathway.PathwayName;

            txtALOS.Value = pathway.Alos;
            txtClass1.Value = Convert.ToDouble(pathway.CoverageValue);
            txtNotes.Text = pathway.Notes;
            chkIsActive.Checked = pathway.IsActive ?? false;

            PopulatePathwayItemGrid();
            PopulatePathwayDiagnoseItemGrid();
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new CasemixPathwayQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.PathwayID > txtPathwayID.Text);
                que.OrderBy(que.PathwayID.Ascending);
            }
            else
            {
                que.Where(que.PathwayID < txtPathwayID.Text);
                que.OrderBy(que.PathwayID.Descending);
            }
            var entity = new CasemixPathway();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        protected override void OnMenuNewClick()
        {
            txtALOS.Value = 10;
            txtClass1.Value = 0;
            chkIsActive.Checked = true;
        }

        private void SetEntityValue(CasemixPathway p)
        {
            p.PathwayID = txtPathwayID.Text;
            p.PathwayName = txtPathwayName.Text;
            p.CoverageValue = Convert.ToDecimal(txtClass1.Value);
            p.Alos = Convert.ToInt32(txtALOS.Value);
            p.Notes = txtNotes.Text;
            p.IsActive = chkIsActive.Checked;
            p.LastUpdateByUserID = AppSession.UserLogin.UserID;
            p.LastUpdateDateTime = DateTime.Now;

            foreach (var e in PathwayDiagnoseItems)
            {
                e.PathwayID = p.PathwayID;
                e.LastUpdateByUserID = AppSession.UserLogin.UserID;
                e.LastUpdateDateTime = DateTime.Now;
            }

            foreach (var e in PathwayItems)
            {
                e.PathwayID = p.PathwayID;
                e.LastUpdateByUserID = AppSession.UserLogin.UserID;
                e.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(CasemixPathway entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();

                PathwayItems.Save();

                PathwayDiagnoseItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuEditClick()
        {

        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new CasemixPathway();
            if (entity.LoadByPrimaryKey(txtPathwayID.Text))
            {
                entity.MarkAsDeleted();

                PathwayDiagnoseItems.MarkAllAsDeleted();
                PathwayItems.MarkAllAsDeleted();
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new CasemixPathway();
            if (entity.LoadByPrimaryKey(txtPathwayID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new CasemixPathway();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new CasemixPathway();
            if (entity.LoadByPrimaryKey(txtPathwayID.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("CasemixPathwayID='{0}'", txtPathwayID.Text.Trim());
            auditLogFilter.TableName = "CasemixPathway";
        }

        protected void grdDiagnose_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDiagnose.DataSource = PathwayDiagnoseItems;
        }

        private CasemixPathwayDiagnoseItemCollection PathwayDiagnoseItems
        {
            get
            {
                string sessName = "collPathwayDiagnoseItemCollection";
                if (IsPostBack)
                {
                    object obj = Session[sessName];
                    if (obj != null) return ((CasemixPathwayDiagnoseItemCollection)(obj));
                }

                var coll = new CasemixPathwayDiagnoseItemCollection();

                var item = new CasemixPathwayDiagnoseItemQuery("a");
                var diag = new DiagnoseQuery("b");

                item.Select(
                    item,
                    diag.DiagnoseName.As("refToDiagnose_DiagnoseName")
                    );
                item.InnerJoin(diag).On(item.DiagnoseID == diag.DiagnoseID);
                item.Where(item.PathwayID == txtPathwayID.Text);

                coll.Load(item);

                Session[sessName] = coll;
                return coll;
            }
            set
            {
                string sessName = "collCasemixPathwayDiagnoseItemCollection";
                Session[sessName] = value;
            }
        }

        private void RefreshCommandItemPathwayDiagnoseItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdDiagnose.Columns[0].Visible = isVisible;
            grdDiagnose.Columns[grdDiagnose.Columns.Count - 1].Visible = isVisible;

            grdDiagnose.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdDiagnose.Rebind();
        }

        private void PopulatePathwayDiagnoseItemGrid()
        {
            //Display Data Detail
            PathwayDiagnoseItems = null; //Reset Record Detail
            grdDiagnose.DataSource = PathwayDiagnoseItems; //Requery
            grdDiagnose.MasterTableView.IsItemInserted = false;
            grdDiagnose.MasterTableView.ClearEditItems();
            grdDiagnose.DataBind();
        }

        protected void grdDiagnose_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            var diagnoseID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][CasemixPathwayDiagnoseItemMetadata.ColumnNames.DiagnoseID]);

            var entity = FindPathwayDiagnoseItem(diagnoseID);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdDiagnose_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            var diagnoseID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][CasemixPathwayDiagnoseItemMetadata.ColumnNames.DiagnoseID]);

            var entity = FindPathwayDiagnoseItem(diagnoseID);
            if (entity != null)
            {
                entity.MarkAsDeleted();
            }
        }

        protected void grdDiagnose_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = PathwayDiagnoseItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdDiagnose.Rebind();
        }

        private CasemixPathwayDiagnoseItem FindPathwayDiagnoseItem(string diagnoseID)
        {
            var coll = PathwayDiagnoseItems;
            return coll.FirstOrDefault(rec => rec.DiagnoseID.Equals(diagnoseID));
        }

        private void SetEntityValue(CasemixPathwayDiagnoseItem entity, GridCommandEventArgs e)
        {
            var userControl = (ClinicalPathwayDiagnoseItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.PathwayID = txtPathwayID.Text;
                entity.DiagnoseID = userControl.DiagnoseID;
                entity.DiagnoseName = userControl.DiagnoseName;
            }
        }
    }
}