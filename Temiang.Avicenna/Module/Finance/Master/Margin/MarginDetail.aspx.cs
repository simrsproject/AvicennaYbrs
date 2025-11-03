using System;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class MarginDetail : BasePageDetail
    {
        #region Page Event
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "MarginSearch.aspx";
            UrlPageList = "MarginList.aspx";

            ProgramID = AppConstant.Program.MARGIN;

            if (!IsPostBack)
            {
                var cls = ItemProductMarginClassValues;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion

        private void SetEntityValue(ItemProductMargin entity)
        {
            entity.MarginID = txtMarginID.Text;
            entity.MarginName = txtMarginName.Text;
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
            ItemProductMarginQuery que = new ItemProductMarginQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.MarginID > txtMarginID.Text);
                que.OrderBy(que.MarginID.Ascending);
            }
            else
            {
                que.Where(que.MarginID < txtMarginID.Text);
                que.OrderBy(que.MarginID.Descending);
            }
            ItemProductMargin entity = new ItemProductMargin();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            ItemProductMargin entity = new ItemProductMargin();
            if (parameters.Length > 0)
            {
                String marginID = (String)parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(marginID);
            }
            else
                entity.LoadByPrimaryKey(txtMarginID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            ItemProductMargin itemProductMarginHd = (ItemProductMargin)entity;
            txtMarginID.Text = itemProductMarginHd.MarginID;
            txtMarginName.Text = itemProductMarginHd.MarginName;
            chkIsActive.Checked = itemProductMarginHd.IsActive ?? false;

            //Display Data Detail
            PopulateGridDetail();
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ItemProductMargin());
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
            auditLogFilter.PrimaryKeyData = string.Format("MarginID='{0}'", txtMarginID.Text.Trim());
            auditLogFilter.TableName = "ItemProductMargin";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtMarginID.Enabled = (newVal == AppEnum.DataMode.New);
            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            ItemProductMargin entity = new ItemProductMargin();
            entity.LoadByPrimaryKey(txtMarginID.Text);
            entity.MarkAsDeleted();

            ItemProductMarginValueCollection collDt = new ItemProductMarginValueCollection();
            collDt.Query.Where(collDt.Query.MarginID == txtMarginID.Text);
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
            ItemProductMargin entity = new ItemProductMargin();
            if (entity.LoadByPrimaryKey(txtMarginID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new ItemProductMargin();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(ItemProductMargin entity)
        {
            foreach (ItemProductMarginValue item in ItemProductMarginValues)
                item.MarginID = entity.MarginID;

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                ItemProductMarginValues.Save();
                ItemProductMarginClassValues.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            ItemProductMargin entity = new ItemProductMargin();
            if (entity.LoadByPrimaryKey(txtMarginID.Text))
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
            grdItemProductMarginValue.Columns[0].Visible = isVisible;
            grdItemProductMarginValue.Columns[grdItemProductMarginValue.Columns.Count - 1].Visible = isVisible;

            grdItemProductMarginValue.MasterTableView.CommandItemDisplay = isVisible
                ? GridCommandItemDisplay.Top
                : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                ItemProductMarginValues = null;

            //Perbaharui tampilan dan data
            grdItemProductMarginValue.Rebind();
        }

        private ItemProductMarginValueCollection ItemProductMarginValues
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemProductMarginValue"];
                    if (obj != null)
                        return ((ItemProductMarginValueCollection)(obj));
                }

                ItemProductMarginValueCollection coll = new ItemProductMarginValueCollection();
                ItemProductMarginValueQuery query = new ItemProductMarginValueQuery();

                string marginID = txtMarginID.Text;

                query.Where(query.MarginID == marginID);
                query.OrderBy(query.SequenceNo.Ascending);
                coll.Load(query);

                Session["collItemProductMarginValue"] = coll;
                return coll;
            }
            set
            {
                Session["collItemProductMarginValue"] = value;
            }
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            ItemProductMarginValues = null; //Reset Record Detail
            grdItemProductMarginValue.DataSource = ItemProductMarginValues;
            grdItemProductMarginValue.DataBind();

            ItemProductMarginClassValues = null;
            var cls = ItemProductMarginClassValues;
        }

        protected void grdItemProductMarginValue_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemProductMarginValue.DataSource = ItemProductMarginValues;
        }

        protected void grdItemProductMarginValue_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String sequenceNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ItemProductMarginValueMetadata.ColumnNames.SequenceNo]);
            ItemProductMarginValue entity = FindItemProductMarginValue(sequenceNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItemProductMarginValue_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemProductMarginValueMetadata.ColumnNames.SequenceNo]);
            ItemProductMarginValue entity = FindItemProductMarginValue(sequenceNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItemProductMarginValue_InsertCommand(object source, GridCommandEventArgs e)
        {
            ItemProductMarginValue entity = ItemProductMarginValues.AddNew();
            SetEntityValue(entity, e);
        }

        private ItemProductMarginValue FindItemProductMarginValue(String sequenceNo)
        {
            ItemProductMarginValueCollection coll = ItemProductMarginValues;
            ItemProductMarginValue retEntity = null;
            foreach (ItemProductMarginValue rec in coll)
            {
                if (rec.SequenceNo.Equals(sequenceNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(ItemProductMarginValue entity, GridCommandEventArgs e)
        {
            ItemProductMarginValueDetail userControl = (ItemProductMarginValueDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.MarginID = txtMarginID.Text;
                //if (grdItemProductMarginValue.Items.Count == 0) entity.SequenceNo = "1";
                //else 
                entity.SequenceNo = userControl.SequenceNo;
                entity.StartingValue = userControl.StartingValue;
                entity.EndingValue = userControl.EndingValue;
                entity.MarginPercentage = userControl.MarginPercentage;
                entity.IsGlobalWithoutVAT = userControl.IsGlobalWithoutVAT;
                entity.IsMinusDiscount = userControl.IsMinusDiscount;

                entity.InpatientMarginPercentage = userControl.InpatientMarginPercentage;
                entity.IsIpWithoutVAT = userControl.IsIpWithoutVAT;
                entity.OutpatientMarginPercentage = userControl.OutpatientMarginPercentage;
                entity.IsOpWithoutVAT = userControl.IsOpWithoutVAT;
                entity.OTCMarginPercentage = userControl.OTCMarginPercentage;
                entity.IsOtcWithoutVAT = userControl.IsOtcWithoutVAT;
                entity.EmergencyMarginPercentage = userControl.EmergencyMarginPercentage;
                entity.IsEmWithoutVAT = userControl.IsEmWithoutVAT;

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                //inpatient detailed
                foreach (GridDataItem dataItem in userControl.GridItemProductMarginValue)
                {
                    var classID = dataItem.KeyValues;
                    var value = ((RadNumericTextBox)dataItem.FindControl("txtValue")).Value;
                    //if (value == 0) continue;
                    //var cls = ItemProductMarginClassValues.FindByPrimaryKey(dataItem.GetDataKeyValue("ClassID").ToString(), entity.MarginID, entity.SequenceNo);
                    var cls = ItemProductMarginClassValues.SingleOrDefault(i => i.MarginID == entity.MarginID && i.SequenceNo == entity.SequenceNo && i.ClassID == dataItem.GetDataKeyValue("ClassID").ToString());
                    if (cls == null)
                    {
                        cls = ItemProductMarginClassValues.AddNew();
                        cls.MarginID = entity.MarginID;
                        cls.SequenceNo = entity.SequenceNo;
                        cls.ClassID = dataItem.GetDataKeyValue("ClassID").ToString();
                    }
                    cls.MarginValuePercentage = Convert.ToDecimal(value);
                    cls.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    cls.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        #endregion

        private ItemProductMarginClassValueCollection ItemProductMarginClassValues
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemProductMarginClassValue"];
                    if (obj != null) return ((ItemProductMarginClassValueCollection)(obj));
                }

                var coll = new ItemProductMarginClassValueCollection();

                var query = new ItemProductMarginClassValueQuery("a");
                var cls = new ClassQuery("b");

                string marginID = txtMarginID.Text;

                query.Select(query, cls.ClassName.As("refToClass_ClassName"));
                query.RightJoin(cls).On(query.ClassID == cls.ClassID && cls.IsInPatientClass == true && cls.IsActive == true);
                query.Where(query.MarginID == marginID);
                query.OrderBy(query.SequenceNo.Ascending);
                coll.Load(query);

                Session["collItemProductMarginClassValue"] = coll;
                return coll;
            }
            set
            {
                Session["collItemProductMarginClassValue"] = value;
            }
        }

        protected void grdItemProductMarginValue_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string id = dataItem.GetDataKeyValue("MarginID").ToString();
            string sequence = dataItem.GetDataKeyValue("SequenceNo").ToString();

            //Apply
            e.DetailTableView.DataSource = ItemProductMarginClassValues.Where(i => i.MarginID == id && i.SequenceNo == sequence);
        }

    }
}
