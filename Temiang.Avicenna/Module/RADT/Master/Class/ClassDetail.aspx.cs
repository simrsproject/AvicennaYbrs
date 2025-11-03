using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;
using System.Linq;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ClassDetail : BasePageDetail
    {
        private void SetEntityValue(Class entity)
        {
            entity.ClassID = txtClassID.Text;
            entity.ClassName = txtClassName.Text;
            entity.ShortName = txtShortName.Text;
            entity.SRClassRL = cboSRClassRL.SelectedValue;
            entity.MarginPercentage = Convert.ToDecimal(txtMarginPercentage.Value);
            entity.Margin2Percentage = Convert.ToDecimal(txtMargin2Percentage.Value);
            entity.DepositAmount = Convert.ToDecimal(txtDepositAmount.Value);
            entity.IsInPatientClass = chkIsInPatientClass.Checked;
            entity.IsActive = chkIsActive.Checked;
            entity.IsTariffClass = chkIsTariffClass.Checked;
            entity.ClassSeq = Convert.ToInt16(txtClassSeq.Value);
            entity.BpjsClassID = cboBpjsClassID.SelectedValue;

            //Last Update Status
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;

            foreach (var item in ClassBridgings)
            {
                item.ClassID = txtClassID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            ClassQuery que = new ClassQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ClassID > txtClassID.Text);
                que.OrderBy(que.ClassID.Ascending);
            }
            else
            {
                que.Where(que.ClassID < txtClassID.Text);
                que.OrderBy(que.ClassID.Descending);
            }
            Class entity = new Class();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            Class entity = new Class();
            if (parameters.Length > 0)
            {
                String classID = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(classID);
            }
            else
            {
                entity.LoadByPrimaryKey(txtClassID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            Class classes = (Class)entity;
            txtClassID.Text = classes.ClassID;
            txtClassName.Text = classes.ClassName;
            txtShortName.Text = classes.ShortName;
            cboSRClassRL.SelectedValue = classes.SRClassRL;
            txtMarginPercentage.Value = Convert.ToDouble(classes.MarginPercentage);
            txtMargin2Percentage.Value = Convert.ToDouble(classes.Margin2Percentage);
            txtDepositAmount.Value = Convert.ToDouble(classes.DepositAmount);
            chkIsInPatientClass.Checked = classes.IsInPatientClass ?? false;
            chkIsActive.Checked = classes.IsActive ?? false;
            chkIsTariffClass.Checked = classes.IsTariffClass ?? false;
            txtClassSeq.Value = Convert.ToDouble(classes.ClassSeq);
            cboBpjsClassID.SelectedValue = classes.BpjsClassID;

            PopulateClassBirdgingGrid();
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Class());
            chkIsTariffClass.Checked = true;
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
            auditLogFilter.PrimaryKeyData = string.Format("ClassID='{0}'", txtClassID.Text.Trim());
            auditLogFilter.TableName = "Class";
        }

        protected override void OnDataModeChanged(Temiang.Avicenna.Common.AppEnum.DataMode oldVal, Temiang.Avicenna.Common.AppEnum.DataMode newVal)
        {
            txtClassID.Enabled = (newVal == Temiang.Avicenna.Common.AppEnum.DataMode.New);
            pnlUpdate.Visible = (newVal == Temiang.Avicenna.Common.AppEnum.DataMode.Read);

            RefreshCommandItemClassBridging(newVal);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "ClassSearch.aspx";
            UrlPageList = "ClassList.aspx";

            ProgramID = AppConstant.Program.ServiceClass;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRClassRL, AppEnum.StandardReference.ClassRL);
                StandardReference.InitializeIncludeSpace(cboBpjsClassID, AppEnum.StandardReference.BpjsClassID);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            Class entity = new Class();
            entity.LoadByPrimaryKey(txtClassID.Text);
            entity.MarkAsDeleted();
            SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new Class();
            if (entity.LoadByPrimaryKey(txtClassID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new Class();

            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(Class entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();


                ClassBridgings.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Class entity = new Class();
            if (entity.LoadByPrimaryKey(txtClassID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            var margins = new ItemProductMedicMarginDetailCollection();
            margins.Query.Where(margins.Query.ClassID == txtClassID.Text);
            margins.LoadAll();
            margins.MarkAllAsDeleted();

            var items = new ItemProductMedicCollection();
            items.LoadAll();
            foreach (var item in items)
            {
                var margin = margins.AddNew();
                margin.ItemID = item.ItemID;
                margin.ClassID = txtClassID.Text;
                margin.AmountPercentage = Convert.ToDecimal(txtMarginPercentage.Value);
                margin.LastUpdateByUserID = AppSession.UserLogin.UserID;
                margin.LastUpdateDateTime = DateTime.Now;
            }
            margins.Save();
        }

        #region Record Detail Method Function ClassBridging

        private ClassBridgingCollection ClassBridgings
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collClassBridging"];
                    if (obj != null) return ((ClassBridgingCollection)(obj));
                }

                ClassBridgingCollection coll = new ClassBridgingCollection();

                ClassBridgingQuery query = new ClassBridgingQuery("a");
                AppStandardReferenceItemQuery asri = new AppStandardReferenceItemQuery("b");

                query.Select(query, asri.ItemName.As("refToAppStandardReferenceItem_ItemName"));
                query.InnerJoin(asri).On(query.SRBridgingType == asri.ItemID && asri.StandardReferenceID == AppEnum.StandardReference.BridgingType.ToString());
                query.Where(query.ClassID == txtClassID.Text);
                coll.Load(query);

                Session["collClassBridging"] = coll;
                return coll;
            }
            set
            {
                Session["collClassBridging"] = value;
            }
        }

        private void RefreshCommandItemClassBridging(Temiang.Avicenna.Common.AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdAliasName.Columns[0].Visible = isVisible;
            grdAliasName.Columns[grdAliasName.Columns.Count - 1].Visible = isVisible;

            grdAliasName.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdAliasName.Rebind();
        }

        private void PopulateClassBirdgingGrid()
        {
            //Display Data Detail
            ClassBridgings = null; //Reset Record Detail
            grdAliasName.DataSource = ClassBridgings; //Requery
            grdAliasName.MasterTableView.IsItemInserted = false;
            grdAliasName.MasterTableView.ClearEditItems();
            grdAliasName.DataBind();
        }

        protected void grdAliasName_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdAliasName.DataSource = ClassBridgings;
        }

        protected void grdAliasName_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String type = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ClassBridgingMetadata.ColumnNames.SRBridgingType]);
            String id = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ClassBridgingMetadata.ColumnNames.BridgingID]);

            var entity = FindClassBridging(type, id);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdAliasName_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String type = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ClassBridgingMetadata.ColumnNames.SRBridgingType]);
            String id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ClassBridgingMetadata.ColumnNames.BridgingID]);

            var entity = FindClassBridging(type, id);
            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdAliasName_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = ClassBridgings.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdAliasName.Rebind();
        }

        private ClassBridging FindClassBridging(String type, string id)
        {
            var coll = ClassBridgings;
            return coll.FirstOrDefault(rec => rec.SRBridgingType.Equals(type) && rec.BridgingID.Equals(id));
        }

        private void SetEntityValue(ClassBridging entity, GridCommandEventArgs e)
        {
            ClassAliasDetail userControl = (ClassAliasDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ClassID = txtClassID.Text;
                entity.SRBridgingType = userControl.BridgingType;
                entity.BridgingTypeName = userControl.BridgingTypeName;
                entity.BridgingID = userControl.BridgingID;
                entity.BridgingName = userControl.BridgingName;
                entity.IsActive = userControl.IsActive;
            }
        }

        #endregion
    }
}