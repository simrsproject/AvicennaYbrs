using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class LocationTemplateDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "LocationTemplateSearch.aspx";
            UrlPageList = "LocationTemplateList.aspx";

            ProgramID = AppConstant.Program.LocationTemplate;

            //StandardReference Initialize
            if (!IsPostBack)
            {
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItemMedic, grdItemMedic);
            ajax.AddAjaxSetting(grdItemNonMedic, grdItemNonMedic);
            ajax.AddAjaxSetting(grdItemKitchen, grdItemKitchen);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new LocationTemplate());
            chkIsActive.Checked = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new LocationTemplate();
            entity.LoadByPrimaryKey(txtTemplateNo.Text);
            entity.MarkAsDeleted();
            SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            txtTemplateNo.Text = GetTemplateNo();

            var entity = new LocationTemplate();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new LocationTemplate();
            if (entity.LoadByPrimaryKey(txtTemplateNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
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
            auditLogFilter.PrimaryKeyData = string.Format("TemplateNo='{0}'", txtTemplateNo.Text.Trim());
            auditLogFilter.TableName = "LocationTemplate";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_TemplateNo", txtTemplateNo.Text);
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemMedic(newVal);
            RefreshCommandItemNonMedic(newVal);
            RefreshCommandItemKitchen(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new LocationTemplate();
            if (parameters.Length > 0)
            {
                String id = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(id);
            }
            else
            {
                entity.LoadByPrimaryKey(txtTemplateNo.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var template = (LocationTemplate)entity;
            txtTemplateNo.Text = template.TemplateNo;
            txtTemplateName.Text = template.TemplateName;
            chkIsActive.Checked = template.IsActive ?? false;

            if (!string.IsNullOrEmpty(template.LocationID))
            {
                var location = new LocationQuery();
                location.Where(location.LocationID == template.LocationID);
                cboLocationID.DataSource = location.LoadDataTable();
                cboLocationID.DataBind();

                cboLocationID.SelectedValue = template.LocationID;
            }
            else
            {
                cboLocationID.Items.Clear();
                cboLocationID.Text = string.Empty;
            }

            PopulateTemplateItemMedicGrid();
            PopulateTemplateItemNonMedicGrid();
            PopulateTemplateItemKitchenGrid();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(LocationTemplate entity)
        {
            entity.TemplateNo = txtTemplateNo.Text;
            entity.TemplateName = txtTemplateName.Text;
            entity.LocationID = cboLocationID.SelectedValue;
            entity.IsActive = chkIsActive.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            //Update Detil
            foreach (LocationTemplateItem item in TemplateItemMedics)
            {
                item.TemplateNo = txtTemplateNo.Text;
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }

            foreach (LocationTemplateItem item in TemplateItemNonMedics)
            {
                item.TemplateNo = txtTemplateNo.Text;
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }

            foreach (LocationTemplateItem item in TemplateItemKitchens)
            {
                item.TemplateNo = txtTemplateNo.Text;
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }
        }

        private void SaveEntity(LocationTemplate entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                TemplateItemMedics.Save();
                TemplateItemNonMedics.Save();
                TemplateItemKitchens.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new LocationTemplateQuery("a");
            var location = new LocationQuery("b");
            var unitLocation = new ServiceUnitLocationQuery("c");
            var usrUnit = new AppUserServiceUnitQuery("d");

            que.InnerJoin(location).On(location.LocationID == que.LocationID);
            que.InnerJoin(unitLocation).On(unitLocation.LocationID == que.LocationID);
            que.InnerJoin(usrUnit).On(usrUnit.UserID == AppSession.UserLogin.UserID &&
                                        usrUnit.ServiceUnitID == unitLocation.ServiceUnitID);

            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.TemplateNo > txtTemplateNo.Text);
                que.OrderBy(que.TemplateNo.Ascending);
            }
            else
            {
                que.Where(que.TemplateNo < txtTemplateNo.Text);
                que.OrderBy(que.TemplateNo.Descending);
            }
            var entity = new LocationTemplate();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function Item Medic
        private void RefreshCommandItemMedic(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemMedic.Columns[0].Visible = isVisible;
            grdItemMedic.Columns[grdItemMedic.Columns.Count - 1].Visible = isVisible;

            grdItemMedic.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdItemMedic.Rebind();
        }

        private LocationTemplateItemCollection TemplateItemMedics
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collTemplateItemMedic"];
                    if (obj != null)
                    {
                        return ((LocationTemplateItemCollection)(obj));
                    }
                }

                var coll = new LocationTemplateItemCollection();

                var query = new LocationTemplateItemQuery("a");
                var qrItemMed = new ItemProductMedicQuery("b");
                var qrRef = new AppStandardReferenceItemQuery("c");
                var qrItem = new ItemQuery("d");

                query.InnerJoin(qrItemMed).On(query.ItemID == qrItemMed.ItemID);
                query.LeftJoin(qrRef).On(qrItemMed.SRItemUnit == qrRef.ItemID & qrRef.StandardReferenceID == "ItemUnit");
                query.InnerJoin(qrItem).On(query.ItemID == qrItem.ItemID);

                query.Where(query.TemplateNo == txtTemplateNo.Text);

                query.Select(query, qrItem.ItemName.As("refToItem_ItemName"), qrRef.ItemName.As("refToSRI_ItemUnit"));

                query.OrderBy(query.ItemID.Ascending);
                coll.Load(query);

                Session["collTemplateItemMedic"] = coll;
                return coll;
            }
            set { Session["collTemplateItemMedic"] = value; }
        }

        private void PopulateTemplateItemMedicGrid()
        {
            //Display Data Detail
            TemplateItemMedics = null; //Reset Record Detail
            grdItemMedic.DataSource = TemplateItemMedics; //Requery
            grdItemMedic.MasterTableView.IsItemInserted = false;
            grdItemMedic.MasterTableView.ClearEditItems();
            grdItemMedic.DataBind();
        }

        protected void grdItemMedic_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (txtFilterItemMedic.Text.Trim() != string.Empty)
            {
                var ds = from d in TemplateItemMedics
                         where d.ItemName.ToLower().Contains(txtFilterItemMedic.Text.ToLower()) || d.ItemID.ToLower().Contains(txtFilterItemMedic.Text.ToLower())
                    select d;
                grdItemMedic.DataSource = ds;
            }
            else
            {
                grdItemMedic.DataSource = TemplateItemMedics;
            }
        }

        protected void grdItemMedic_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][LocationTemplateItemMetadata.ColumnNames.ItemID]);
            LocationTemplateItem entity = FindTemplateItemMedic(itemID);
            if (entity != null)
                SetEntityValueItemMedic(entity, e);
        }

        protected void grdItemMedic_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][LocationTemplateItemMetadata.ColumnNames.ItemID]);
            var entity = FindTemplateItemMedic(itemID);
            if (entity != null)
            {
                entity.MarkAsDeleted();
            }
        }

        protected void grdItemMedic_InsertCommand(object source, GridCommandEventArgs e)
        {
            LocationTemplateItem entity = TemplateItemMedics.AddNew();
            SetEntityValueItemMedic(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItemMedic.Rebind();
        }

        private LocationTemplateItem FindTemplateItemMedic(String itemID)
        {
            LocationTemplateItemCollection coll = TemplateItemMedics;
            LocationTemplateItem retEntity = null;
            foreach (LocationTemplateItem rec in coll)
            {
                if (rec.ItemID.Equals(itemID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValueItemMedic(LocationTemplateItem entity, GridCommandEventArgs e)
        {
            var userControl = (LocationTemplateItemMedicDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.SRItemUnitName = userControl.SRItemUnitName;
            }
        }

        #endregion

        #region Record Detail Method Function Item Non Medic
        private void RefreshCommandItemNonMedic(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemNonMedic.Columns[0].Visible = isVisible;
            grdItemNonMedic.Columns[grdItemNonMedic.Columns.Count - 1].Visible = isVisible;

            grdItemNonMedic.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdItemNonMedic.Rebind();
        }

        private LocationTemplateItemCollection TemplateItemNonMedics
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collTemplateItemNonMedic"];
                    if (obj != null)
                    {
                        return ((LocationTemplateItemCollection)(obj));
                    }
                }

                var coll = new LocationTemplateItemCollection();

                var query = new LocationTemplateItemQuery("a");
                var qrItemMed = new ItemProductNonMedicQuery("b");
                var qrRef = new AppStandardReferenceItemQuery("c");
                var qrItem = new ItemQuery("d");

                query.InnerJoin(qrItemMed).On(query.ItemID == qrItemMed.ItemID);
                query.LeftJoin(qrRef).On(qrItemMed.SRItemUnit == qrRef.ItemID & qrRef.StandardReferenceID == "ItemUnit");
                query.InnerJoin(qrItem).On(query.ItemID == qrItem.ItemID);

                query.Where(query.TemplateNo == txtTemplateNo.Text);

                query.Select(query, qrItem.ItemName.As("refToItem_ItemName"), qrRef.ItemName.As("refToSRI_ItemUnit"));

                query.OrderBy(query.ItemID.Ascending);
                coll.Load(query);

                Session["collTemplateItemNonMedic"] = coll;
                return coll;
            }
            set { Session["collTemplateItemNonMedic"] = value; }
        }

        private void PopulateTemplateItemNonMedicGrid()
        {
            //Display Data Detail
            TemplateItemNonMedics = null; //Reset Record Detail
            grdItemNonMedic.DataSource = TemplateItemNonMedics; //Requery
            grdItemNonMedic.MasterTableView.IsItemInserted = false;
            grdItemNonMedic.MasterTableView.ClearEditItems();
            grdItemNonMedic.DataBind();
        }

        protected void grdItemNonMedic_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (txtFilterItemNonMedic.Text.Trim() != string.Empty)
            {
                var ds = from d in TemplateItemNonMedics
                         where d.ItemName.ToLower().Contains(txtFilterItemNonMedic.Text.ToLower()) || d.ItemID.ToLower().Contains(txtFilterItemNonMedic.Text.ToLower())
                    select d;
                grdItemNonMedic.DataSource = ds;
            }
            else
            {
                grdItemNonMedic.DataSource = TemplateItemNonMedics;
            }
        }

        protected void grdItemNonMedic_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][LocationTemplateItemMetadata.ColumnNames.ItemID]);
            var entity = FindTemplateItemNonMedic(itemID);

            if (entity != null)
                SetEntityValueItemNonMedic(entity, e);
        }

        protected void grdItemNonMedic_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][LocationTemplateItemMetadata.ColumnNames.ItemID]);
            var entity = FindTemplateItemNonMedic(itemID);
            if (entity != null)
            {
                entity.MarkAsDeleted();
            }
        }

        protected void grdItemNonMedic_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = TemplateItemNonMedics.AddNew();
            SetEntityValueItemNonMedic(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItemNonMedic.Rebind();
        }

        private LocationTemplateItem FindTemplateItemNonMedic(String itemID)
        {
            var coll = TemplateItemNonMedics;
            LocationTemplateItem retEntity = null;
            foreach (LocationTemplateItem rec in coll)
            {
                if (rec.ItemID.Equals(itemID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValueItemNonMedic(LocationTemplateItem entity, GridCommandEventArgs e)
        {
            var userControl = (LocationTemplateItemNonMedicDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.SRItemUnitName = userControl.SRItemUnitName;
            }
        }

        #endregion

        #region Record Detail Method Function Item Kitchen
        private void RefreshCommandItemKitchen(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemKitchen.Columns[0].Visible = isVisible;
            grdItemKitchen.Columns[grdItemKitchen.Columns.Count - 1].Visible = isVisible;

            grdItemKitchen.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdItemKitchen.Rebind();
        }

        private LocationTemplateItemCollection TemplateItemKitchens
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collTemplateItemKitchen"];
                    if (obj != null)
                    {
                        return ((LocationTemplateItemCollection)(obj));
                    }
                }

                var coll = new LocationTemplateItemCollection();

                var query = new LocationTemplateItemQuery("a");
                var qrItemMed = new ItemKitchenQuery("b");
                var qrRef = new AppStandardReferenceItemQuery("c");
                var qrItem = new ItemQuery("d");

                query.InnerJoin(qrItemMed).On(query.ItemID == qrItemMed.ItemID);
                query.LeftJoin(qrRef).On(qrItemMed.SRItemUnit == qrRef.ItemID & qrRef.StandardReferenceID == "ItemUnit");
                query.InnerJoin(qrItem).On(query.ItemID == qrItem.ItemID);

                query.Where(query.TemplateNo == txtTemplateNo.Text);

                query.Select(query, qrItem.ItemName.As("refToItem_ItemName"), qrRef.ItemName.As("refToSRI_ItemUnit"));

                query.OrderBy(query.ItemID.Ascending);
                coll.Load(query);

                Session["collTemplateItemKitchen"] = coll;
                return coll;
            }
            set { Session["collTemplateItemKitchen"] = value; }
        }

        private void PopulateTemplateItemKitchenGrid()
        {
            //Display Data Detail
            TemplateItemKitchens = null; //Reset Record Detail
            grdItemKitchen.DataSource = TemplateItemKitchens; //Requery
            grdItemKitchen.MasterTableView.IsItemInserted = false;
            grdItemKitchen.MasterTableView.ClearEditItems();
            grdItemKitchen.DataBind();
        }

        protected void grdItemKitchen_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (txtFilterItemKitchen.Text.Trim() != string.Empty)
            {
                var ds = from d in TemplateItemKitchens
                         where d.ItemName.ToLower().Contains(txtFilterItemKitchen.Text.ToLower()) || d.ItemID.ToLower().Contains(txtFilterItemKitchen.Text.ToLower())
                    select d;
                grdItemKitchen.DataSource = ds;
            }
            else
            {
                grdItemKitchen.DataSource = TemplateItemKitchens;
            }
        }

        protected void grdItemKitchen_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][LocationTemplateItemMetadata.ColumnNames.ItemID]);
            var entity = FindTemplateItemKitchen(itemID);

            if (entity != null)
                SetEntityValueItemKitchen(entity, e);
        }

        protected void grdItemKitchen_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][LocationTemplateItemMetadata.ColumnNames.ItemID]);
            var entity = FindTemplateItemKitchen(itemID);
            if (entity != null)
            {
                entity.MarkAsDeleted();
            }
        }

        protected void grdItemKitchen_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = TemplateItemKitchens.AddNew();
            SetEntityValueItemKitchen(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItemKitchen.Rebind();
        }

        private LocationTemplateItem FindTemplateItemKitchen(String itemID)
        {
            var coll = TemplateItemKitchens;
            LocationTemplateItem retEntity = null;
            foreach (LocationTemplateItem rec in coll)
            {
                if (rec.ItemID.Equals(itemID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValueItemKitchen(LocationTemplateItem entity, GridCommandEventArgs e)
        {
            var userControl = (LocationTemplateItemKitchenDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.SRItemUnitName = userControl.SRItemUnitName;
            }
        }

        #endregion

        #region ComboBox
        protected void cboLocationID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new LocationQuery("a");
            var unitLocation = new ServiceUnitLocationQuery("b");
            var usrUnit = new AppUserServiceUnitQuery("c");
            query.InnerJoin(unitLocation).On(unitLocation.LocationID == query.LocationID);
            query.InnerJoin(usrUnit).On(usrUnit.UserID == AppSession.UserLogin.UserID &&
                                        usrUnit.ServiceUnitID == unitLocation.ServiceUnitID);
            query.Select(query.LocationID, query.LocationName);
            query.Where(
                query.Or(query.LocationName.Like(searchTextContain), query.LocationID.Like(searchTextContain)));

            query.es.Top = 30;
            DataTable dtb = query.LoadDataTable();
            cboLocationID.DataSource = dtb;
            cboLocationID.DataBind();
        }

        protected void cboLocationID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["LocationName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["LocationID"].ToString();
        }

        #endregion

        private static string GetTemplateNo()
        {
            var query = new LocationTemplateQuery("a");
            query.es.Top = 1;
            query.Select(query.TemplateNo);
            query.OrderBy(query.TemplateNo.Descending);

            var template = new LocationTemplate();
            template.Load(query);

            string iId;
            if (template.TemplateNo != null)
            {
                int x = (int.Parse(template.TemplateNo.Substring(2, template.TemplateNo.Length - 2)) + 1);
                iId = "T" + "-" + string.Format("{0:0000}", x);
            }
            else
                iId = "T" + "-" + "0001";

            return iId;
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            switch (mpgDetail.SelectedIndex)
            {
                case 0:
                {
                    grdItemMedic.CurrentPageIndex = 0;
                    grdItemMedic.Rebind();
                    break;
                }
                case 1:
                {
                    grdItemNonMedic.CurrentPageIndex = 0;
                    grdItemNonMedic.Rebind();
                    break;
                }
                case 2:
                {
                    grdItemKitchen.CurrentPageIndex = 0;
                    grdItemKitchen.Rebind();
                    break;
                }
            }
        }
    }
}