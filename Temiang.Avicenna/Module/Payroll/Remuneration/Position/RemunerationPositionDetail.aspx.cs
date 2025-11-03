using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.RemunerationPosition
{
    public partial class RemunerationPositionDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "RemunerationPositionSearch.aspx";
            UrlPageList = "RemunerationPositionList.aspx";

            this.WindowSearch.Height = 400;
            ProgramID = AppConstant.Program.EmployeeRemunerationPosition;

            if (IsPostBack) return;

            StandardReference.InitializeIncludeSpace(cboSREmployeeWorkGroup, AppEnum.StandardReference.EmployeeWorkGroup);
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(cboSREmployeeWorkGroup, cboSREmployeeWorkGroup);
            ajax.AddAjaxSetting(cboSREmployeeWorkGroup, cboSREmployeeWorkSubGroup);
            ajax.AddAjaxSetting(cboSREmployeeWorkGroup, cboSREmployeeJobPosition);
            ajax.AddAjaxSetting(cboSREmployeeWorkSubGroup, cboSREmployeeWorkSubGroup);
            ajax.AddAjaxSetting(cboSREmployeeWorkSubGroup, cboSREmployeeJobPosition);

            ajax.AddAjaxSetting(grdItem, grdItem);
            ajax.AddAjaxSetting(grdItem, txtBasePoint);
            ajax.AddAjaxSetting(grdItem, txtPoints);
            ajax.AddAjaxSetting(grdItem, cboPositionGradeID);

            ajax.AddAjaxSetting(txtPoints, txtPoints);
            ajax.AddAjaxSetting(txtPoints, cboPositionGradeID);
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
        }

        protected override void OnMenuEditClick()
        {
            cboPersonID.Enabled = false;
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new EmployeeWageStructureAndScalePosition());

            txtWageStructureAndScalePositionID.Value = -1;
            cboSREmployeeWorkGroup.SelectedValue = string.Empty;
            cboSREmployeeWorkGroup.Text = string.Empty;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new EmployeeWageStructureAndScalePosition();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtWageStructureAndScalePositionID.Text)))
            {
                if (!IsApproved(entity, args))
                    return;

                var items = new EmployeeWageStructureAndScalePositionItemCollection();
                items.Query.Where(items.Query.WageStructureAndScalePositionID == Convert.ToInt32(txtWageStructureAndScalePositionID.Text));
                items.LoadAll();
                items.MarkAllAsDeleted();

                entity.MarkAsDeleted();
                using (var trans = new esTransactionScope())
                {
                    items.Save();
                    entity.Save();
                    trans.Complete();
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var coll = new EmployeeWageStructureAndScalePositionCollection();
            coll.Query.Where(coll.Query.PersonID == cboPersonID.SelectedValue.ToInt(),
                coll.Query.ValidFrom == txtValidFrom.SelectedDate,
                coll.Query.Or(coll.Query.IsVoid.IsNull(), coll.Query.IsVoid == false));
            coll.LoadAll();
            if (coll.Count > 0)
            {
                args.MessageText = "Record with this Employee Name and Valid From has registered.";
                args.IsCancel = true;
                return;
            }
                
            var entity = new EmployeeWageStructureAndScalePosition();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var coll = new EmployeeWageStructureAndScalePositionCollection();
            coll.Query.Where(coll.Query.WageStructureAndScalePositionID != txtWageStructureAndScalePositionID.Text.ToInt(), 
                coll.Query.PersonID == cboPersonID.SelectedValue.ToInt(),
                coll.Query.ValidFrom == txtValidFrom.SelectedDate,
                coll.Query.Or(coll.Query.IsVoid.IsNull(), coll.Query.IsVoid == false));
            coll.LoadAll();
            if (coll.Count > 0)
            {
                args.MessageText = "Record with this Employee Name and Valid From has registered.";
                args.IsCancel = true;
                return;
            }

            var entity = new EmployeeWageStructureAndScalePosition();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtWageStructureAndScalePositionID.Text)))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
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

        private bool IsApprovedOrVoid(EmployeeWageStructureAndScalePosition entity, ValidateArgs args)
        {
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }

            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        private bool IsApproved(EmployeeWageStructureAndScalePosition entity, ValidateArgs args)
        {
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("WageStructureAndScalePositionID='{0}'", txtWageStructureAndScalePositionID.Text.Trim());
            auditLogFilter.TableName = "EmployeeWageStructureAndScalePosition";
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new EmployeeWageStructureAndScalePosition();
            if (entity.LoadByPrimaryKey(txtWageStructureAndScalePositionID.Text.ToInt()))
            {
                if (!IsApprovedOrVoid(entity, args))
                    return;

                using (var trans = new esTransactionScope())
                {
                    entity.IsApproved = true;
                    entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                    entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                    var remun = new EmployeeWageStructureAndScale();
                    remun.AddNew();
                    remun.PersonID = entity.PersonID;
                    remun.ValidFrom = entity.ValidFrom;
                    remun.WageStructureAndScalePositionID = entity.WageStructureAndScalePositionID;
                    remun.Points = entity.Points;
                    remun.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    remun.LastUpdateByUserID = AppSession.UserLogin.UserID;

                    var epgColl = new EmployeePositionGradeCollection();
                    epgColl.Query.Where(epgColl.Query.PersonID == entity.PersonID, epgColl.Query.ValidFrom == entity.ValidFrom);
                    epgColl.LoadAll();
                    if (epgColl.Count == 0)
                    {
                        var epg = new EmployeePositionGrade();
                        epg.PersonID = entity.PersonID;
                        epg.SREducationLevel = string.Empty;
                        epg.ValidFrom = entity.ValidFrom;
                        epg.PositionGradeID = entity.PositionGradeID;
                        epg.GradeYear = 0;
                        epg.SRDecreeType = "02";
                        epg.DecreeNo = string.Empty;
                        epg.PositionName = string.Empty;
                        epg.NextProposalDate = entity.ValidFrom.Value.AddYears(4);
                        epg.NextPositionGradeID = entity.PositionGradeID;
                        epg.NextGradeYear = 0;
                        epg.SRDecreeTypeNext = "02";
                        epg.NextPositionName = string.Empty;
                        epg.SRDp3 = string.Empty;
                        epg.Notes = string.Empty;
	                    epg.LastUpdateDateTime= (new DateTime()).NowAtSqlServer();
                        epg.LastUpdateByUserID= AppSession.UserLogin.UserID;
                        epg.SalaryScaleID = -1;
                        epg.NextSalaryScaleID = -1;

                        epg.Save();
                    }

                    remun.Save();
                    entity.Save();
                    
                    trans.Complete();
                }
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new EmployeeWageStructureAndScalePosition();
            if (!entity.LoadByPrimaryKey(txtWageStructureAndScalePositionID.Text.ToInt()))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }
            entity.IsVoid = true;
            entity.VoidByUserID = AppSession.UserLogin.UserID;
            entity.VoidDateTime = (new DateTime()).NowAtSqlServer();
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.Save();
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new EmployeeWageStructureAndScalePosition();
            if (entity.LoadByPrimaryKey(txtWageStructureAndScalePositionID.Text.ToInt()))
            {
                if (!IsApprovedOrVoid(entity, args))
                    return;
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !chkIsApproved.Checked;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !chkIsVoid.Checked;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            RefreshCommandItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new EmployeeWageStructureAndScalePosition();
            if (parameters.Length > 0)
            {
                string id = (string)parameters[0];
                if (!parameters[0].Equals(string.Empty)) entity.LoadByPrimaryKey(Convert.ToInt32(id));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtWageStructureAndScalePositionID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var position = (EmployeeWageStructureAndScalePosition)entity;
            txtWageStructureAndScalePositionID.Value = position.WageStructureAndScalePositionID;
            if (!string.IsNullOrEmpty(position.PersonID.ToString()))
            {
                var personal = new VwEmployeeTableQuery();
                personal.Where(personal.PersonID == Convert.ToInt32(position.PersonID));
                var dtb = personal.LoadDataTable();
                cboPersonID.DataSource = dtb;
                cboPersonID.DataBind();
                cboPersonID.SelectedValue = position.PersonID.ToString();
                if (!string.IsNullOrEmpty(cboPersonID.SelectedValue))
                    cboPersonID.Text = dtb.Rows[0]["EmployeeNumber"].ToString() + " - " + dtb.Rows[0]["EmployeeName"].ToString();
            }
            else
            {
                cboPersonID.Items.Clear();
                cboPersonID.Text = string.Empty;
            }
            txtValidFrom.SelectedDate = position.ValidFrom;
            cboSREmployeeWorkGroup.SelectedValue = position.SREmployeeWorkGroup;
            if (!string.IsNullOrEmpty(position.SREmployeeWorkSubGroup))
            {
                var query = new AppStandardReferenceItemQuery();
                query.Where(query.StandardReferenceID == AppEnum.StandardReference.EmployeeWorkSubGroup.ToString(), query.ItemID == position.SREmployeeWorkSubGroup);
                cboSREmployeeWorkSubGroup.DataSource = query.LoadDataTable();
                cboSREmployeeWorkSubGroup.DataBind();
                cboSREmployeeWorkSubGroup.SelectedValue = position.SREmployeeWorkSubGroup.ToString();
            }
            else
            {
                cboSREmployeeWorkSubGroup.Items.Clear();
                cboSREmployeeWorkSubGroup.Text = string.Empty;
            }
            if (!string.IsNullOrEmpty(position.SREmployeeJobPosition))
            {
                var query = new AppStandardReferenceItemQuery();
                query.Where(query.StandardReferenceID == AppEnum.StandardReference.EmployeeJobPosition.ToString(), query.ItemID == position.SREmployeeJobPosition);
                cboSREmployeeJobPosition.DataSource = query.LoadDataTable();
                cboSREmployeeJobPosition.DataBind();
                cboSREmployeeJobPosition.SelectedValue = position.SREmployeeJobPosition.ToString();
            }
            else
            {
                cboSREmployeeJobPosition.Items.Clear();
                cboSREmployeeJobPosition.Text = string.Empty;
            }
            txtBasePoint.Value = Convert.ToDouble(position.BasePoint);
            txtPoints.Value = Convert.ToDouble(position.Points);
            if (position.PositionGradeID.HasValue && position.PositionGradeID != -1)
                PopulatecboPositionGradeID(cboPositionGradeID, position.PositionGradeID.ToInt(), 0);
            else
            {
                cboPositionGradeID.Items.Clear();
                cboPositionGradeID.SelectedValue = string.Empty;
                cboPositionGradeID.Text = string.Empty;
            }
            chkIsApproved.Checked = position.IsApproved ?? false;
            chkIsVoid.Checked = position.IsVoid ?? false;

            PopulateGrid();
        }

        private void SetEntityValue(EmployeeWageStructureAndScalePosition position)
        {
            position.WageStructureAndScalePositionID = Convert.ToInt32(txtWageStructureAndScalePositionID.Value);
            position.PersonID = cboPersonID.SelectedValue.ToInt();
            position.ValidFrom = txtValidFrom.SelectedDate;
            position.SREmployeeWorkGroup = cboSREmployeeWorkGroup.SelectedValue;
            position.SREmployeeWorkSubGroup = cboSREmployeeWorkSubGroup.SelectedValue;
            position.SREmployeeJobPosition = cboSREmployeeJobPosition.SelectedValue;
            position.BasePoint = Convert.ToDecimal(txtBasePoint.Value);
            position.Points = Convert.ToDecimal(txtPoints.Value);
            position.PositionGradeID = string.IsNullOrEmpty(cboPositionGradeID.SelectedValue) ? -1 : cboPositionGradeID.SelectedValue.ToInt();

            //Last Update Status
            if (position.es.IsAdded || position.es.IsModified)
            {
                position.LastUpdateByUserID = AppSession.UserLogin.UserID;
                position.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(EmployeeWageStructureAndScalePosition entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();

                //--> SalaryComponentRuleDefinition
                foreach (EmployeeWageStructureAndScalePositionItem item in EmployeeWageStructureAndScalePositionItems)
                {
                    item.WageStructureAndScalePositionID = entity.WageStructureAndScalePositionID;
                    //Last Update Status
                    if (item.es.IsAdded || item.es.IsModified)
                    {
                        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        item.LastUpdateDateTime = DateTime.Now;
                    }
                }


                EmployeeWageStructureAndScalePositionItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();

                txtWageStructureAndScalePositionID.Value = entity.WageStructureAndScalePositionID;
            }
        }

        private void CalculatePoints()
        {
            if (EmployeeWageStructureAndScalePositionItems.Count > 0)
            {
                decimal? points = 0;
                
                foreach (EmployeeWageStructureAndScalePositionItem item in EmployeeWageStructureAndScalePositionItems)
                {
                    points += (item.Points);
                }

                txtBasePoint.Value = Convert.ToDouble(points);
                txtPoints.Value = Convert.ToDouble(points);

                PopulatecboPositionGradeID(cboPositionGradeID, -1, Convert.ToDecimal(txtPoints.Value));
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new EmployeeWageStructureAndScalePositionQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.WageStructureAndScalePositionID > txtWageStructureAndScalePositionID.Text.ToInt());
                que.OrderBy(que.WageStructureAndScalePositionID.Ascending);
            }
            else
            {
                que.Where(que.WageStructureAndScalePositionID < txtWageStructureAndScalePositionID.Text.ToInt());
                que.OrderBy(que.WageStructureAndScalePositionID.Descending);
            }

            var entity = new EmployeeWageStructureAndScalePosition();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private EmployeeWageStructureAndScalePositionItemCollection EmployeeWageStructureAndScalePositionItems
        {
            get
            {
                object obj = Session["collEmployeeWageStructureAndScalePositionItem" + Request.UserHostName];
                if (obj != null)
                    return ((EmployeeWageStructureAndScalePositionItemCollection)(obj));

                var coll = new EmployeeWageStructureAndScalePositionItemCollection();

                var query = new EmployeeWageStructureAndScalePositionItemQuery("a");
                var type = new AppStandardReferenceItemQuery("b");
                var scale = new WageStructureAndScaleQuery("c");
                var scaleitm = new WageStructureAndScaleItemQuery("d");
                var scaleitmdef = new AppStandardReferenceItemQuery("e");

                query.Select
                    (
                       query,
                       type.ItemName.As("refToStdRef_WageStructureAndScaleType"),
                       scale.WageStructureAndScaleName.As("refToWageStructureAndScale_WageStructureAndScaleName"),
                       scaleitmdef.ItemName.As("refToStdRef_WageStructureAndScaleItem")
                    );

                query.InnerJoin(type).On(type.StandardReferenceID == AppEnum.StandardReference.WageStructureAndScaleType.ToString() && type.ItemID == query.SRWageStructureAndScaleType);
                query.InnerJoin(scale).On(scale.WageStructureAndScaleID == query.WageStructureAndScaleID);
                query.InnerJoin(scaleitm).On(scaleitm.WageStructureAndScaleItemID == query.WageStructureAndScaleItemID);
                query.InnerJoin(scaleitmdef).On(scaleitmdef.StandardReferenceID == AppEnum.StandardReference.WageStructureAndScaleItem.ToString() && scaleitmdef.ItemID == scaleitm.SRWageStructureAndScaleItem);

                query.Where(query.WageStructureAndScalePositionID == Convert.ToInt32(txtWageStructureAndScalePositionID.Value == null ? -1 : txtWageStructureAndScalePositionID.Value)); 
                query.OrderBy(query.SRWageStructureAndScaleType.Ascending); 

                coll.Load(query);

                Session["collEmployeeWageStructureAndScalePositionItem" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collEmployeeWageStructureAndScalePositionItem" + Request.UserHostName] = value; }
        }

        private void PopulateGrid()
        {
            //Display Data Detail
            EmployeeWageStructureAndScalePositionItems = null; //Reset Record Detail
            grdItem.DataSource = EmployeeWageStructureAndScalePositionItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = EmployeeWageStructureAndScalePositionItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.WageStructureAndScalePositionItemID]);

            EmployeeWageStructureAndScalePositionItem entity = FindItem(id);
            if (entity != null) SetEntityValue(entity, e);

            CalculatePoints();
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.WageStructureAndScalePositionItemID]);

            EmployeeWageStructureAndScalePositionItem entity = FindItem(id);
            if (entity != null) entity.MarkAsDeleted();

            CalculatePoints();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeWageStructureAndScalePositionItem entity = EmployeeWageStructureAndScalePositionItems.AddNew();
            SetEntityValue(entity, e);

            CalculatePoints();

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private EmployeeWageStructureAndScalePositionItem FindItem(Int32 id)
        {
            EmployeeWageStructureAndScalePositionItemCollection coll = EmployeeWageStructureAndScalePositionItems;
            EmployeeWageStructureAndScalePositionItem retEntity = null;
            foreach (EmployeeWageStructureAndScalePositionItem rec in coll)
            {
                if (rec.WageStructureAndScalePositionItemID.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(EmployeeWageStructureAndScalePositionItem entity, GridCommandEventArgs e)
        {
            var userControl = (RemunerationPositionItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.WageStructureAndScalePositionItemID = userControl.WageStructureAndScalePositionItemID;
                entity.SRWageStructureAndScaleType = userControl.SRWageStructureAndScaleType;
                entity.WageStructureAndScaleTypeName = userControl.WageStructureAndScaleTypeName;
                entity.WageStructureAndScaleID = userControl.WageStructureAndScaleID;
                entity.WageStructureAndScaleName = userControl.WageStructureAndScaleName;
                entity.WageStructureAndScaleItemID = userControl.WageStructureAndScaleItemID;
                entity.WageStructureAndScaleItemName = userControl.WageStructureAndScaleItemName;
                var load = new AppStandardReferenceItem();
                if (load.LoadByPrimaryKey(AppEnum.StandardReference.WageStructureAndScaleType.ToString(), entity.SRWageStructureAndScaleType))
                {
                    entity.LoadPoint = load.NumericValue;
                }
                else entity.LoadPoint = 0;
                entity.BasePoint = userControl.Points;
                entity.Points = entity.BasePoint * entity.LoadPoint / 100;
            }
        }

        #region Combobox
        protected void cboPersonID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new VwEmployeeTableQuery("a");
            query.es.Top = 20;
            query.es.Distinct = true;
            query.Select
                (
                    query.PersonID,
                    query.EmployeeNumber,
                    query.EmployeeName
                );
            query.Where
                (
                    query.Or
                        (
                            query.EmployeeNumber.Like(searchTextContain),
                            query.EmployeeName.Like(searchTextContain)
                        )
                );

            cboPersonID.DataSource = query.LoadDataTable();
            cboPersonID.DataBind();
        }

        protected void cboPersonID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboSREmployeeWorkGroup_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSREmployeeWorkSubGroup.Items.Clear();
            cboSREmployeeWorkSubGroup.Text = string.Empty;
            cboSREmployeeWorkSubGroup.SelectedValue = string.Empty;
            cboSREmployeeJobPosition.Items.Clear();
            cboSREmployeeJobPosition.Text = string.Empty;
            cboSREmployeeJobPosition.SelectedValue = string.Empty;
        }

        protected void cboSREmployeeWorkSubGroup_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery("a");
            query.Where(query.StandardReferenceID == AppEnum.StandardReference.EmployeeWorkSubGroup.ToString(),
                query.ItemName.Like(searchTextContain), query.ReferenceID == cboSREmployeeWorkGroup.SelectedValue);

            query.Select(query.ItemID, query.ItemName);
            DataTable dtb = query.LoadDataTable();
            cboSREmployeeWorkSubGroup.DataSource = dtb;
            cboSREmployeeWorkSubGroup.DataBind();
        }

        protected void cboSREmployeeWorkSubGroup_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSREmployeeWorkSubGroup_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSREmployeeJobPosition.Items.Clear();
            cboSREmployeeJobPosition.Text = string.Empty;
            cboSREmployeeJobPosition.SelectedValue = string.Empty;
        }

        protected void cboSREmployeeJobPosition_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery("a");
            query.Where(query.StandardReferenceID == AppEnum.StandardReference.EmployeeJobPosition.ToString(),
                query.ItemName.Like(searchTextContain), query.ReferenceID == cboSREmployeeWorkSubGroup.SelectedValue);

            query.Select(query.ItemID, query.ItemName);
            DataTable dtb = query.LoadDataTable();
            cboSREmployeeJobPosition.DataSource = dtb;
            cboSREmployeeJobPosition.DataBind();
        }

        protected void cboSREmployeeJobPosition_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboPositionGradeID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboPositionGradeID((RadComboBox)sender, e.Text);
        }

        private void PopulatecboPositionGradeID(RadComboBox comboBox, string textSearch)
        {
            var srEmploymentType = "-1";
            var empQ = new VwEmployeeTableQuery();
            empQ.Where(empQ.PersonID == cboPersonID.SelectedValue.ToInt());
            var emp = new VwEmployeeTable();
            emp.Load(empQ);
            if (emp != null)
                srEmploymentType = emp.SREmploymentType;

            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new PositionGradeQuery("a");
            var et = new AppStandardReferenceItemQuery("b");

            query.LeftJoin(et).On(et.StandardReferenceID == "EmploymentType" && et.ItemID == query.SREmploymentType);

            query.Where(query.PositionGradeName.Like(searchTextContain));
            if (srEmploymentType != "-1")
                query.Where(query.Or(query.SREmploymentType == srEmploymentType, query.SREmploymentType.IsNull(), query.SREmploymentType == string.Empty));

            query.Select(
                query.PositionGradeID,
                query.PositionGradeCode,
                query.PositionGradeName,
                query.RankName,
                et.ItemName.As("EmploymentTypeName")
                );

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["PositionGradeID"].ToString();
            }
        }
        
        private void PopulatecboPositionGradeID(RadComboBox comboBox, int positionGradeId, decimal points)
        {
            var query = new PositionGradeQuery("a");
            var et = new AppStandardReferenceItemQuery("b");
            query.LeftJoin(et).On(et.StandardReferenceID == "EmploymentType" && et.ItemID == query.SREmploymentType);
            if (points == 0)
                query.Where(query.PositionGradeID == positionGradeId);
            else
                query.Where(query.LowerLimit <= points, query.UpperLimit >= points);

            query.Select(
                query.PositionGradeID,
                query.PositionGradeCode,
                query.PositionGradeName,
                query.RankName,
                et.ItemName.As("EmploymentTypeName")
                );
            

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["PositionGradeID"].ToString();
                comboBox.Text = dtb.Rows[0]["PositionGradeName"].ToString();
            }
            else
            {
                comboBox.SelectedValue = string.Empty;
                comboBox.Text = string.Empty;
            }
        }
        
        protected void cboPositionGradeID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PositionGradeCode"].ToString().Trim() + " (" + ((DataRowView)e.Item.DataItem)["RankName"].ToString() + ")";
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PositionGradeID"].ToString();
        }

        protected void txtPoints_TextChanged(object sender, EventArgs e)
        {
            PopulatecboPositionGradeID(cboPositionGradeID, -1, Convert.ToDecimal(txtPoints.Value));
        }
        #endregion
    }
}