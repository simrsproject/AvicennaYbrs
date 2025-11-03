using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ServiceRoomDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ServiceRoomSearch.aspx";
            UrlPageList = "ServiceRoomList.aspx";

            this.WindowSearch.Height = 400;
            ProgramID = AppConstant.Program.ServiceRoom;

            //PopUp Search
            if (!IsCallback)
            {
                PopUpSearch.Initialize(AppEnum.PopUpSearch.ServiceUnit, Page, txtServiceUnitID);
                PopUpSearch.Initialize(AppEnum.PopUpSearch.ItemService, Page, txtItemID);
                PopUpSearch.Initialize(AppEnum.PopUpSearch.ItemService, Page, txtItemBookedID);
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.Class, Page);

                StandardReference.InitializeIncludeSpace(cboSRFloor, AppEnum.StandardReference.Floor);
                StandardReference.InitializeIncludeSpace(cboSRGenderType, AppEnum.StandardReference.GenderType);

                chkIsBpjs.Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH";
                trItemBooked.Visible = AppSession.Parameter.IsBookingBedCharged;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdBed, grdBed);
            ajax.AddAjaxSetting(grdImages, grdImages);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ServiceRoom());
            SetEnabled(chkIsOperatingRoom.Checked, true);
            chkIsActive.Checked = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            ServiceRoom entity = new ServiceRoom();
            if (entity.LoadByPrimaryKey(txtRoomID.Text))
            {
                var regs = new RegistrationCollection();
                regs.Query.Where(regs.Query.RoomID == entity.RoomID);
                regs.LoadAll();
                if (regs.Count > 0)
                {
                    args.MessageText = AppConstant.Message.RecordHasUsed;
                    args.IsCancel = true;
                    return;
                }

                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            ServiceRoom entity = new ServiceRoom();
            if (entity.LoadByPrimaryKey(txtRoomID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new ServiceRoom();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            ServiceRoom entity = new ServiceRoom();
            if (entity.LoadByPrimaryKey(txtRoomID.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("RoomID='{0}'", txtRoomID.Text.Trim());
            auditLogFilter.TableName = "ServiceRoom";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtRoomID.ReadOnly = (newVal != AppEnum.DataMode.New);

            RefreshCommandItemBed(newVal);
            RefreshCommandItemAutoBill(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            ServiceRoom entity = new ServiceRoom();
            if (parameters.Length > 0)
            {
                String roomID = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(roomID);
            }
            else
            {
                entity.LoadByPrimaryKey(txtRoomID.Text);
            }

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var serviceRoom = (ServiceRoom)entity;
            txtRoomID.Text = serviceRoom.RoomID;
            txtRoomName.Text = serviceRoom.RoomName;

            txtServiceUnitID.Text = serviceRoom.ServiceUnitID;
            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(txtServiceUnitID.Text);

            grdBed.Enabled = (unit.DepartmentID == AppSession.Parameter.InPatientDepartmentID);

            PopulateServiceUnitName(false);

            txtItemID.Text = serviceRoom.ItemID;
            txtItemBookedID.Text = serviceRoom.ItemBookedID;
            PopulateItemName(false);
            PopulateItemBookedName(false);

            var par = new ParamedicQuery("a");
            par.Select(
                par.ParamedicID,
                par.ParamedicName
                );
            par.Where(par.ParamedicID == serviceRoom.str.ParamedicID1);
            cboParamedicID1.DataSource = par.LoadDataTable();
            cboParamedicID1.DataBind();
            cboParamedicID1.SelectedValue = serviceRoom.ParamedicID1;

            par = new ParamedicQuery("a");
            par.Select(
                par.ParamedicID,
                par.ParamedicName
                );
            par.Where(par.ParamedicID == serviceRoom.str.ParamedicID2);
            cboParamedicID2.DataSource = par.LoadDataTable();
            cboParamedicID2.DataBind();
            cboParamedicID2.SelectedValue = serviceRoom.ParamedicID2;

            txtNotes.Text = serviceRoom.Notes;
            chkIsOperatingRoom.Checked = serviceRoom.IsOperatingRoom ?? false;
            chkIsShowOnBookingOT.Checked = serviceRoom.IsShowOnBookingOT ?? false;
            chkIsResetPrice.Checked = serviceRoom.IsResetPrice ?? false;
            chkIsActive.Checked = serviceRoom.IsActive ?? false;
            txtNumberOfBeds.Value = serviceRoom.NumberOfBeds;
            txtTariffDiscountForRoomIn.Value = Convert.ToDouble(serviceRoom.TariffDiscountForRoomIn);
            cboSRFloor.SelectedValue = serviceRoom.SRFloor;
            chkIsBpjs.Checked = serviceRoom.IsBpjs ?? false;
            cboSRGenderType.SelectedValue = serviceRoom.SRGenderType;
            chkIsIsolationRoom.Checked = serviceRoom.IsIsolationRoom ?? false;
            chkIsNegativePressureRoom.Checked = serviceRoom.IsNegativePressureRoom ?? false;
            chkIsPandemicRoom.Checked = serviceRoom.IsPandemicRoom ?? false;
            chkIsVentilator.Checked = serviceRoom.IsVentilator ?? false;

            PopulateBedGrid();
            PopulatePhotoGrid();
            PopulateAutoBillItemGrid();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(ServiceRoom entity)
        {
            entity.RoomID = txtRoomID.Text;
            entity.ServiceUnitID = txtServiceUnitID.Text;
            entity.RoomName = txtRoomName.Text;
            entity.ItemID = txtItemID.Text;
            entity.ItemBookedID = txtItemBookedID.Text;
            entity.ParamedicID1 = cboParamedicID1.SelectedValue;
            entity.ParamedicID2 = cboParamedicID2.SelectedValue;
            entity.Notes = txtNotes.Text;
            entity.IsOperatingRoom = chkIsOperatingRoom.Checked;
            entity.IsShowOnBookingOT = chkIsShowOnBookingOT.Checked;
            entity.IsResetPrice = chkIsResetPrice.Checked;
            entity.IsActive = chkIsActive.Checked;
            entity.NumberOfBeds = Convert.ToInt16(txtNumberOfBeds.Value);
            entity.TariffDiscountForRoomIn = Convert.ToDecimal(txtTariffDiscountForRoomIn.Value);
            entity.SRFloor = cboSRFloor.SelectedValue;
            entity.IsBpjs = chkIsBpjs.Checked;
            entity.SRGenderType = cboSRGenderType.SelectedValue;
            entity.IsIsolationRoom = chkIsIsolationRoom.Checked;
            entity.IsNegativePressureRoom = chkIsNegativePressureRoom.Checked;
            entity.IsPandemicRoom = chkIsPandemicRoom.Checked;
            entity.IsVentilator = chkIsVentilator.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            foreach (Bed bed in Beds)
            {
                bed.BedStatusUpdatedBy = AppSession.UserLogin.UserID;
                bed.LastUpdateByUserID = AppSession.UserLogin.UserID;
                bed.LastUpdateDateTime = DateTime.Now;
            }

            foreach (ServiceRoomAutoBillItem abi in ServiceRoomAutoBillItems)
            {
                abi.LastUpdateByUserID = AppSession.UserLogin.UserID;
                abi.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(ServiceRoom entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                Beds.Save();
                foreach (ServiceRoomImages Photo in Photos)
                {
                    if (Photo.es.IsAdded)
                    {
                        Photo.RoomID = entity.RoomID;
                    }
                }
                Photos.Save();
                ServiceRoomAutoBillItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            ServiceRoomQuery que = new ServiceRoomQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.RoomID > txtRoomID.Text);
                que.OrderBy(que.RoomID.Ascending);
            }
            else
            {
                que.Where(que.RoomID < txtRoomID.Text);
                que.OrderBy(que.RoomID.Descending);
            }
            ServiceRoom entity = new ServiceRoom();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region Method & Event TextChanged

        protected void txtServiceUnitID_TextChanged(object sender, EventArgs e)
        {
            PopulateServiceUnitName(true);
        }

        private void PopulateServiceUnitName(bool isResetIdIfNotExist)
        {
            if (txtServiceUnitID.Text == string.Empty)
            {
                lblServiceUnitName.Text = string.Empty;
                grdBed.Enabled = false;
                return;
            }
            ServiceUnit entity = new ServiceUnit();
            if (entity.LoadByPrimaryKey(txtServiceUnitID.Text))
            {
                lblServiceUnitName.Text = entity.ServiceUnitName;
                grdBed.Enabled = (entity.SRRegistrationType == AppConstant.RegistrationType.InPatient);
            }
            else
            {
                lblServiceUnitName.Text = string.Empty;
                grdBed.Enabled = false;
                if (isResetIdIfNotExist)
                    txtServiceUnitID.Text = string.Empty;
            }
        }

        protected void txtItemID_TextChanged(object sender, EventArgs e)
        {
            PopulateItemName(true);
        }

        private void PopulateItemName(bool isResetIdIfNotExist)
        {
            if (txtItemID.Text == string.Empty)
            {
                lblItemName.Text = string.Empty;
                return;
            }
            Item entity = new Item();
            if (entity.LoadByPrimaryKey(txtItemID.Text))
                lblItemName.Text = entity.ItemName;
            else
            {
                lblItemName.Text = string.Empty;
                if (isResetIdIfNotExist)
                    txtItemID.Text = string.Empty;
            }
        }

        protected void txtItemBookedID_TextChanged(object sender, EventArgs e)
        {
            PopulateItemBookedName(true);
        }

        private void PopulateItemBookedName(bool isResetIdIfNotExist)
        {
            if (txtItemBookedID.Text == string.Empty)
            {
                lblItemBookedName.Text = string.Empty;
                return;
            }
            Item entity = new Item();
            if (entity.LoadByPrimaryKey(txtItemBookedID.Text))
                lblItemBookedName.Text = entity.ItemName;
            else
            {
                lblItemBookedName.Text = string.Empty;
                if (isResetIdIfNotExist)
                    txtItemBookedID.Text = string.Empty;
            }
        }

        protected void chkIsOperatingRoom_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled(chkIsOperatingRoom.Checked, true);
        }

        private void SetEnabled(bool isEnabled, bool isResetValue)
        {
            chkIsShowOnBookingOT.Enabled = isEnabled;
            chkIsResetPrice.Enabled = isEnabled;
            if (isResetValue)
            {
                chkIsShowOnBookingOT.Checked = isEnabled;
                chkIsResetPrice.Checked = false;
            }
        }
        #endregion

        #region Record Detail Method Function Bed

        private void RefreshCommandItemBed(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdBed.Columns[0].Visible = isVisible;
            grdBed.Columns[grdBed.Columns.Count - 1].Visible = isVisible;

            grdBed.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdBed.Rebind();

            if (isVisible)
                SetEnabled(chkIsOperatingRoom.Checked, false);
            else
            {
                chkIsShowOnBookingOT.Enabled = false;
                chkIsResetPrice.Enabled = false;
            }

            grdImages.Columns[0].Visible = false;
            grdImages.Columns[grdImages.Columns.Count - 1].Visible = isVisible;

            grdImages.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            grdImages.Rebind();
        }

        private ServiceRoomImagesCollection Photos
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPhotos"];
                    if (obj != null)
                    {
                        return ((ServiceRoomImagesCollection)(obj));
                    }
                }

                var coll = new ServiceRoomImagesCollection();
                coll.Query.Where(coll.Query.RoomID == txtRoomID.Text);
                coll.LoadAll();

                Session["collPhotos"] = coll;
                return coll;
            }
            set { Session["collPhotos"] = value; }
        }

        private BedCollection Beds
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collBed"];
                    if (obj != null)
                    {
                        return ((BedCollection)(obj));
                    }
                }

                var coll = new BedCollection();
                var query = new BedQuery("a");
                var classQuery = new ClassQuery("b");
                var chargeclassQuery = new ClassQuery("c");

                query.Select
                    (
                        query,
                        classQuery.ClassName.As("refToClass_ClassName"),
                        chargeclassQuery.ClassName.As("refToClass_DefaultChargeClassName")
                    );
                query.InnerJoin(classQuery).On(query.ClassID == classQuery.ClassID);
                query.InnerJoin(chargeclassQuery).On(query.DefaultChargeClassID == chargeclassQuery.ClassID);
                query.Where(query.RoomID == txtRoomID.Text);
                query.OrderBy
                    (
                        query.ClassID.Ascending,
                        query.BedID.Ascending
                    );
                coll.Load(query);

                Session["collBed"] = coll;
                return coll;
            }
            set { Session["collBed"] = value; }
        }

        private void PopulateBedGrid()
        {
            //Display Data Detail
            Beds = null; //Reset Record Detail
            grdBed.DataSource = Beds; //Requery
            grdBed.MasterTableView.IsItemInserted = false;
            grdBed.MasterTableView.ClearEditItems();
            grdBed.DataBind();
        }

        private void PopulatePhotoGrid()
        {
            //Display Data Detail
            Photos = null; //Reset Record Detail
            grdImages.DataSource = Photos; //Requery
            grdImages.MasterTableView.IsItemInserted = false;
            grdImages.MasterTableView.ClearEditItems();
            grdImages.DataBind();
        }

        protected void grdBed_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdBed.DataSource = Beds;
        }

        protected void grdBed_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String bedID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][BedMetadata.ColumnNames.BedID]);
            String classID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][BedMetadata.ColumnNames.ClassID]);

            Bed entity = FindBed(bedID, classID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdBed_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String bedID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][BedMetadata.ColumnNames.BedID]);
            String classID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][BedMetadata.ColumnNames.ClassID]);

            Bed entity = FindBed(bedID, classID);
            if (entity != null)
            {
                if (string.IsNullOrEmpty(entity.RegistrationNo))
                    entity.IsActive = false;
            }
                
        }

        protected void grdBed_InsertCommand(object source, GridCommandEventArgs e)
        {
            Bed entity = Beds.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdBed.Rebind();
        }

        private Bed FindBed(String bedID, String classID)
        {
            BedCollection coll = Beds;
            return coll.FirstOrDefault(rec => rec.BedID.Equals(bedID) && rec.ClassID.Equals(classID));
        }

        private void SetEntityValue(Bed entity, GridCommandEventArgs e)
        {
            BedDetail userControl = (BedDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.BedID = userControl.BedID;
                entity.RoomID = txtRoomID.Text;
                entity.RegistrationNo = userControl.RegistrationNo; //string.Empty;
                entity.ClassID = userControl.ClassID;
                entity.ClassName = userControl.ClassName;
                entity.DefaultChargeClassID = userControl.DefaultChargeClassID;
                entity.DefaultChargeClassName = userControl.DefaultChargeClassName;
                entity.SRBedStatus = userControl.SRBedStatus; //AppSession.Parameter.BedStatusUnoccupied;
                entity.IsTemporary = userControl.IsTemporary;
                entity.IsActive = userControl.IsActive;
                entity.IsNeedConfirmation = userControl.IsNeedConfirmation;
                entity.IsVisibleTo3rdParty = userControl.IsSharedTo3rdParty;
                entity.Notes = userControl.Notes;
            }
        }

        #endregion

        #region Record Detail Method Function Auto Bill Item
        private void RefreshCommandItemAutoBill(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdAutoBillItem.Columns[0].Visible = isVisible;
            grdAutoBillItem.Columns[grdAutoBillItem.Columns.Count - 1].Visible = isVisible;

            grdAutoBillItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdAutoBillItem.Rebind();
        }

        private ServiceRoomAutoBillItemCollection ServiceRoomAutoBillItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collServiceRoomAutoBillItem"];
                    if (obj != null)
                    {
                        return ((ServiceRoomAutoBillItemCollection)(obj));
                    }
                }

                var coll = new ServiceRoomAutoBillItemCollection();
                var query = new ServiceRoomAutoBillItemQuery("a");
                var iQuery = new ItemQuery("b");
                var isQuery = new ItemServiceQuery("c");
                var imQuery = new ItemProductMedicQuery("d");
                var inmQuery = new ItemProductNonMedicQuery("e");
                var ikQuery = new ItemKitchenQuery("f");

                query.Select
                    (
                        query,
                        iQuery.ItemName.As("refToItem_ItemName"),
                        @"<ISNULL(c.SRItemUnit, ISNULL(d.SRItemUnit, ISNULL(e.SRItemUnit, ISNULL(f.SRItemUnit, 'X')))) AS 'refToItem_ItemUnit'>"
                    );
                query.InnerJoin(iQuery).On(iQuery.ItemID == query.ItemID);
                query.LeftJoin(isQuery).On(isQuery.ItemID == query.ItemID);
                query.LeftJoin(imQuery).On(imQuery.ItemID == query.ItemID);
                query.LeftJoin(inmQuery).On(inmQuery.ItemID == query.ItemID);
                query.LeftJoin(ikQuery).On(ikQuery.ItemID == query.ItemID);

                query.Where(query.RoomID == txtRoomID.Text);
                query.OrderBy
                    (
                        query.ItemID.Ascending
                    );
                coll.Load(query);

                Session["collServiceRoomAutoBillItem"] = coll;
                return coll;
            }
            set { Session["collServiceRoomAutoBillItem"] = value; }
        }

        private void PopulateAutoBillItemGrid()
        {
            //Display Data Detail
            ServiceRoomAutoBillItems = null; //Reset Record Detail
            grdAutoBillItem.DataSource = ServiceRoomAutoBillItems; //Requery
            grdAutoBillItem.MasterTableView.IsItemInserted = false;
            grdAutoBillItem.MasterTableView.ClearEditItems();
            grdAutoBillItem.DataBind();
        }

        protected void grdAutoBillItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdAutoBillItem.DataSource = ServiceRoomAutoBillItems;
        }

        protected void grdAutoBillItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ServiceRoomAutoBillItemMetadata.ColumnNames.ItemID]);

            ServiceRoomAutoBillItem entity = FindAutoBillItem(itemId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdAutoBillItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String itemId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ServiceRoomAutoBillItemMetadata.ColumnNames.ItemID]);

            ServiceRoomAutoBillItem entity = FindAutoBillItem(itemId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdAutoBillItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            ServiceRoomAutoBillItem entity = ServiceRoomAutoBillItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdAutoBillItem.Rebind();
        }

        private ServiceRoomAutoBillItem FindAutoBillItem(String itemId)
        {
            ServiceRoomAutoBillItemCollection coll = ServiceRoomAutoBillItems;
            return coll.FirstOrDefault(rec => rec.ItemID.Equals(itemId));
        }

        private void SetEntityValue(ServiceRoomAutoBillItem entity, GridCommandEventArgs e)
        {
            var userControl = (ServiceRoomAutoBillItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.RoomID = txtRoomID.Text;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.Quantity = userControl.Quantity;
                entity.ItemUnit = userControl.ItemUnit;
            }
        }
        #endregion

        #region ComboBox ParamedicID

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ParamedicQuery();
            query.es.Top = 20;
            query.Select(
                query.ParamedicID,
                query.ParamedicName
                );
            query.Where(
                query.ParamedicName.Like(searchTextContain),
                query.IsActive == true
                );

            (o as RadComboBox).DataSource = query.LoadDataTable();
            (o as RadComboBox).DataBind();
        }

        #endregion

        protected void grdBed_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                Bed item = Beds[e.Item.DataSetIndex];
                if (item != null)
                {
                    if (!item.IsActive.Value)
                    {
                        for (int i = 0; i < e.Item.Cells.Count; i++)
                        {
                            if (i > 0 && i < e.Item.Cells.Count)
                                e.Item.Cells[i].Font.Strikeout = true;
                        }
                    }
                }
            }
        }

        protected void grdImages_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdImages.DataSource = Photos;
        }


        protected void grdImages_ItemDataBound(object sender, GridItemEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item != null)
            {
                (item["Photo"].FindControl("Photo") as RadBinaryImage).DataValue = ((ServiceRoomImages)e.Item.DataItem).Photo;
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (eventArgument.Contains("AfterUpload"))
            {
                grdImages.Rebind();
            }
        }
        protected void grdImages_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String roomID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ServiceRoomImagesMetadata.ColumnNames.RoomID]);
            int seqNo = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][ServiceRoomImagesMetadata.ColumnNames.SeqNo]);

            ServiceRoomImages entity = Photos.FirstOrDefault(p => /*p.RoomID.Equals(roomID) &&*/ p.SeqNo.Equals(seqNo));
            if (entity != null)
                entity.MarkAsDeleted();
            grdImages.Rebind();
        }
    }
}
