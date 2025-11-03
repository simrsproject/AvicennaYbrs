using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Globalization;
using System.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class SmfDetail : BasePageDetail
    {
        private void SetEntityValue(Smf entity)
        {
            entity.SmfID = txtSmfID.Text;
            entity.SmfName = txtSmfName.Text;
            entity.SRParamedicFeeCaseType = cboSRParamedicFeeCaseType.SelectedValue;
            entity.SRAssessmentType = cboSRAssessmentType.SelectedValue;
            entity.SmfBackcolor = ColorTranslator.ToHtml(txtSmfBackcolor.SelectedColor);

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            SmfQuery que = new SmfQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.SmfID > txtSmfID.Text);
                que.OrderBy(que.SmfID.Ascending);
            }
            else
            {
                que.Where(que.SmfID < txtSmfID.Text);
                que.OrderBy(que.SmfID.Descending);
            }
            Smf entity = new Smf();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            Smf entity = new Smf();
            if (parameters.Length > 0)
            {
                String smfID = (String)parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(smfID);
            }
            else
                entity.LoadByPrimaryKey(txtSmfID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var smf = (Smf)entity;
            txtSmfID.Text = smf.SmfID;
            txtSmfName.Text = smf.SmfName;
            cboSRParamedicFeeCaseType.SelectedValue = smf.SRParamedicFeeCaseType;
            cboSRAssessmentType.SelectedValue = smf.SRAssessmentType;
            txtSmfBackcolor.SelectedColor = ColorTranslator.FromHtml(smf.SmfBackcolor);

            PopulateSmfItemServiceGrid();
            PopulateSmfDiagnoseGrid();
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Smf());
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
            auditLogFilter.PrimaryKeyData = string.Format("SmfID='{0}'", txtSmfID.Text.Trim());
            auditLogFilter.TableName = "Smf";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtSmfID.Enabled = (newVal == AppEnum.DataMode.New);
            RefreshCommandItemSmfItemService(newVal);
            RefreshCommandItemSmfDiagnose(newVal);
            txtFilterItemService.ReadOnly = false;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "SmfSearch.aspx";
            UrlPageList = "SmfList.aspx";

            ProgramID = AppConstant.Program.Smf;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRParamedicFeeCaseType, AppEnum.StandardReference.ParamedicFeeCaseType);
                StandardReference.InitializeIncludeSpace(cboSRAssessmentType, AppEnum.StandardReference.AssessmentType);
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new Smf();
            entity.LoadByPrimaryKey(txtSmfID.Text);
            entity.MarkAsDeleted();
            SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new Smf();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(Smf entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                SmfItemServices.Save();
                SmfDiagnoses.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new Smf();
            if (entity.LoadByPrimaryKey(txtSmfID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion


        #region Record Detail Method Function SmfItemService
        protected void btnFilterItemService_Click(object sender, ImageClickEventArgs e)
        {

            grdSmfItemService.CurrentPageIndex = 0;
            grdSmfItemService.Rebind();
        }
        private SmfItemServiceCollection SmfItemServices
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collSmfItemService"];
                    if (obj != null)
                    {
                        return ((SmfItemServiceCollection)(obj));
                    }
                }


                SmfItemServiceCollection coll = new SmfItemServiceCollection();
                SmfItemServiceQuery query = new SmfItemServiceQuery("a");
                ItemQuery pq = new ItemQuery("b");

                query.InnerJoin(pq).On(query.ItemID == pq.ItemID);
                query.Select
                    (
                        query,
                        pq.ItemName.As("refToItem_ItemName")
                    );

                string SmfID = txtSmfID.Text;
                query.Where(query.SmfID == SmfID);
                query.OrderBy(query.ItemID.Ascending);
                coll.Load(query);

                Session["collSmfItemService"] = coll;
                return coll;
            }
            set { Session["collSmfItemService"] = value; }
        }

        private void RefreshCommandItemSmfItemService(AppEnum.DataMode newVal)
        {

            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdSmfItemService.Columns[0].Visible = isVisible;
            grdSmfItemService.Columns[grdSmfItemService.Columns.Count - 1].Visible = isVisible;


            grdSmfItemService.MasterTableView.CommandItemDisplay = isVisible
                                                                               ? GridCommandItemDisplay.Top
                                                                               : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdSmfItemService.Rebind();


        }

        private void PopulateSmfItemServiceGrid()
        {
            //Display Data Detail
            SmfItemServices = null; //Reset Record Detail
            grdSmfItemService.DataSource = SmfItemServices; //Requery
            grdSmfItemService.MasterTableView.IsItemInserted = false;
            grdSmfItemService.MasterTableView.ClearEditItems();
            grdSmfItemService.DataBind();
        }

        protected void grdSmfItemService_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (txtFilterItemService.Text.Trim() != string.Empty)
            {
                var ds = from d in SmfItemServices
                         where d.ItemName.ToLower().Contains(txtFilterItemService.Text.ToLower()) || d.ItemID.ToLower().Contains(txtFilterItemService.Text.ToLower())
                         select d;
                grdSmfItemService.DataSource = ds;
            }
            else
            {
                grdSmfItemService.DataSource = SmfItemServices;
            }
        }

        protected void grdSmfItemService_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String visitTypeID =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        SmfItemServiceMetadata.ColumnNames.ItemID]);
            SmfItemService entity = FindSmfItemService(visitTypeID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdSmfItemService_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            var id =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][SmfItemServiceMetadata.ColumnNames.ItemID]);
            SmfItemService entity = FindSmfItemService(id);

            if (entity != null)
            {
                entity.MarkAsDeleted();
            }

        }

        protected void grdSmfItemService_InsertCommand(object source, GridCommandEventArgs e)
        {
            SmfItemService entity = SmfItemServices.AddNew();
            SetEntityValue(entity, e);
            e.Canceled = true;
            grdSmfItemService.Rebind();
        }

        private SmfItemService FindSmfItemService(String visitTypeID)
        {
            SmfItemServiceCollection coll = SmfItemServices;
            return coll.FirstOrDefault(rec => rec.ItemID.Equals(visitTypeID));
        }



        private void SetEntityValue(SmfItemService entity, GridCommandEventArgs e)
        {
            SmfItemServiceDetail userControl = (SmfItemServiceDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SmfID = txtSmfID.Text;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.IsVisible = userControl.IsVisible;
            }
        }




        #endregion

        #region Record Detail Method Function SmfDiagnose
        protected void btnFilterDiagnose_Click(object sender, ImageClickEventArgs e)
        {

            grdSmfDiagnose.CurrentPageIndex = 0;
            grdSmfDiagnose.Rebind();
        }
        private SmfDiagnoseCollection SmfDiagnoses
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collSmfDiagnose"];
                    if (obj != null)
                    {
                        return ((SmfDiagnoseCollection)(obj));
                    }
                }


                SmfDiagnoseCollection coll = new SmfDiagnoseCollection();
                SmfDiagnoseQuery query = new SmfDiagnoseQuery("a");
                DiagnoseQuery pq = new DiagnoseQuery("b");

                query.InnerJoin(pq).On(query.DiagnoseID == pq.DiagnoseID);
                query.Select
                    (
                        query,
                        pq.DiagnoseName.As("refToDiagnose_DiagnoseName")
                    );

                string SmfID = txtSmfID.Text;
                query.Where(query.SmfID == SmfID);
                query.OrderBy(query.DiagnoseID.Ascending);
                coll.Load(query);

                Session["collSmfDiagnose"] = coll;
                return coll;
            }
            set { Session["collSmfDiagnose"] = value; }
        }

        private void RefreshCommandItemSmfDiagnose(AppEnum.DataMode newVal)
        {

            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdSmfDiagnose.Columns[0].Visible = isVisible;
            grdSmfDiagnose.Columns[grdSmfDiagnose.Columns.Count - 1].Visible = isVisible;


            grdSmfDiagnose.MasterTableView.CommandItemDisplay = isVisible
                                                                               ? GridCommandItemDisplay.Top
                                                                               : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdSmfDiagnose.Rebind();


        }

        private void PopulateSmfDiagnoseGrid()
        {
            //Display Data Detail
            SmfDiagnoses = null; //Reset Record Detail
            grdSmfDiagnose.DataSource = SmfDiagnoses; //Requery
            grdSmfDiagnose.MasterTableView.IsItemInserted = false;
            grdSmfDiagnose.MasterTableView.ClearEditItems();
            grdSmfDiagnose.DataBind();
        }

        protected void grdSmfDiagnose_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (txtFilterDiagnose.Text.Trim() != string.Empty)
            {
                var ds = from d in SmfDiagnoses
                         where d.DiagnoseName.ToLower().Contains(txtFilterDiagnose.Text.ToLower()) || d.DiagnoseID.ToLower().Contains(txtFilterDiagnose.Text.ToLower())
                         select d;
                grdSmfDiagnose.DataSource = ds;
            }
            else
            {
                grdSmfDiagnose.DataSource = SmfDiagnoses;
            }
        }

        protected void grdSmfDiagnose_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String visitTypeID =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        SmfDiagnoseMetadata.ColumnNames.DiagnoseID]);
            SmfDiagnose entity = FindSmfDiagnose(visitTypeID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdSmfDiagnose_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            var id =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][SmfDiagnoseMetadata.ColumnNames.DiagnoseID]);
            SmfDiagnose entity = FindSmfDiagnose(id);

            if (entity != null)
            {
                entity.MarkAsDeleted();
            }

        }

        protected void grdSmfDiagnose_InsertCommand(object source, GridCommandEventArgs e)
        {
            SmfDiagnose entity = SmfDiagnoses.AddNew();
            SetEntityValue(entity, e);
            e.Canceled = true;
            grdSmfDiagnose.Rebind();
        }

        private SmfDiagnose FindSmfDiagnose(String visitTypeID)
        {
            SmfDiagnoseCollection coll = SmfDiagnoses;
            return coll.FirstOrDefault(rec => rec.DiagnoseID.Equals(visitTypeID));
        }



        private void SetEntityValue(SmfDiagnose entity, GridCommandEventArgs e)
        {
            SmfDiagnoseDetail userControl = (SmfDiagnoseDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SmfID = txtSmfID.Text;
                entity.DiagnoseID = userControl.DiagnoseID;
                entity.DiagnoseName = userControl.DiagnoseName;
                entity.IsVisible = userControl.IsVisible;
            }
        }




        #endregion

    }
}
