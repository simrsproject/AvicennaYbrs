using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class FabricDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private string GetNewId()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.FabricId);

            return _autoNumber.LastCompleteNumber;
        }

        private void SetEntityValue(Fabric entity)
        {
            if (entity.es.IsAdded && AppParameter.GetParameterValue(AppParameter.ParameterItem.IsCreateFabricIdAutomatic) == "Yes")
            {
                txtFabricID.Text = GetNewId();
                _autoNumber.Save();
            }

            entity.FabricID = txtFabricID.Text;
            entity.FabricName = txtFabricName.Text;
            entity.ShortName = txtShortName.Text;
            entity.ContractNumber = txtContractNumber.Text;
            entity.ContractStart = txtContractStart.SelectedDate;
            entity.ContractEnd = txtContractEnd.SelectedDate;
            entity.ContractSummary = txtContractSummary.Text;
            entity.ContactPerson = txtContactPerson.Text;
            entity.IsPKP = chkIsPKP.Checked;
            entity.TaxRegistrationNo = txtTaxRegistrationNo.Text;
            entity.IsActive = chkIsActive.Checked;
            entity.StreetName = AddressCtl1.StreetName;
            entity.District = AddressCtl1.District;
            entity.City = AddressCtl1.City;
            entity.County = AddressCtl1.County;
            entity.State = AddressCtl1.State;
            entity.ZipCode = AddressCtl1.ZipCode;
            entity.PhoneNo = AddressCtl1.PhoneNo;
            entity.FaxNo = AddressCtl1.FaxNo;
            entity.Email = AddressCtl1.Email;
            entity.MobilePhoneNo = AddressCtl1.MobilePhoneNo;

            if (entity.es.IsAdded || entity.es.IsModified)
            {
                //Last Update Status
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            foreach (SupplierFabric supp in SupplierFabrics)
            {
                supp.FabricID = txtFabricID.Text;

                //Last Update Status
                if (supp.es.IsAdded || supp.es.IsModified)
                {
                    supp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    supp.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            FabricQuery que = new FabricQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.FabricID > txtFabricID.Text);
                que.OrderBy(que.FabricID.Ascending);
            }
            else
            {
                que.Where(que.FabricID < txtFabricID.Text);
                que.OrderBy(que.FabricID.Descending);
            }
            Fabric entity = new Fabric();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            Fabric entity = new Fabric();
            if (parameters.Length > 0)
            {
                String fabricID = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(fabricID);
            }
            else
                entity.LoadByPrimaryKey(txtFabricID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            Fabric fabric = (Fabric) entity;
            txtFabricID.Text = fabric.FabricID;
            txtFabricName.Text = fabric.FabricName;
            txtShortName.Text = fabric.ShortName;
            txtContractNumber.Text = fabric.ContractNumber;
            txtContractStart.SelectedDate = fabric.ContractStart;
            txtContractEnd.SelectedDate = fabric.ContractEnd;
            txtContractSummary.Text = fabric.ContractSummary;
            txtContactPerson.Text = fabric.ContactPerson;
            chkIsPKP.Checked = fabric.IsPKP ?? false;
            txtTaxRegistrationNo.Text = fabric.TaxRegistrationNo;
            chkIsActive.Checked = fabric.IsActive ?? false;
            AddressCtl1.StreetName = fabric.StreetName;
            AddressCtl1.District = fabric.District;
            AddressCtl1.City = fabric.City;
            AddressCtl1.County = fabric.County;
            AddressCtl1.State = fabric.State;
            AddressCtl1.ZipCode = fabric.ZipCode;
            AddressCtl1.PhoneNo = fabric.PhoneNo;
            AddressCtl1.FaxNo = fabric.FaxNo;
            AddressCtl1.Email = fabric.Email;
            AddressCtl1.MobilePhoneNo = fabric.MobilePhoneNo;

            PopulateGridDetail();
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Fabric());

            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsCreateFabricIdAutomatic) == "Yes")
                txtFabricID.Text = GetNewId();
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
            auditLogFilter.PrimaryKeyData = string.Format("FabricID='{0}'", txtFabricID.Text.Trim());
            auditLogFilter.TableName = "Fabric";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtFabricID.ReadOnly = (newVal != AppEnum.DataMode.New) || AppParameter.GetParameterValue(AppParameter.ParameterItem.IsCreateFabricIdAutomatic) == "Yes";

            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "FabricSearch.aspx";
            UrlPageList = "FabricList.aspx";

            ProgramID = AppConstant.Program.Fabric;

            if (!IsPostBack)
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.Supplier, Page);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            Fabric entity = new Fabric();
            if (entity.LoadByPrimaryKey(txtFabricID.Text))
            {
                entity.MarkAsDeleted();

                SupplierFabricCollection coll = new SupplierFabricCollection();
                coll.Query.Where(coll.Query.FabricID == txtFabricID.Text);
                coll.LoadAll();
                coll.MarkAllAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    coll.Save();
                    entity.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }


        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new Fabric();
            if (entity.LoadByPrimaryKey(txtFabricID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            
            entity = new Fabric();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(Fabric entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                SupplierFabrics.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Fabric entity = new Fabric();
            if (entity.LoadByPrimaryKey(txtFabricID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion

        #region Record Detail Method Function

        private SupplierFabricCollection SupplierFabrics
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collSupplierFabric"];
                    if (obj != null)
                        return ((SupplierFabricCollection) (obj));
                }

                SupplierFabricCollection coll = new SupplierFabricCollection();
                SupplierFabricQuery query = new SupplierFabricQuery("a");

                SupplierQuery sq = new SupplierQuery("b");
                query.InnerJoin(sq).On(query.SupplierID == sq.SupplierID);

                query.Where(query.FabricID == txtFabricID.Text);

                query.Select
                    (
                    query.FabricID,
                    query.SupplierID,
                    sq.SupplierName.As("refToSupplier_SupplierName"),
                    query.IsActive,
                    query.LastUpdateByUserID,
                    query.LastUpdateDateTime
                    );

                query.OrderBy
                    (
                    query.SupplierID.Ascending
                    );
                coll.Load(query);

                Session["collSupplierFabric"] = coll;
                return coll;
            }
            set { Session["collSupplierFabric"] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdSupplierFabric.Columns[0].Visible = isVisible;
            grdSupplierFabric.Columns[grdSupplierFabric.Columns.Count - 1].Visible = isVisible;

            grdSupplierFabric.MasterTableView.CommandItemDisplay = isVisible
                                                                       ? GridCommandItemDisplay.Top
                                                                       : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                SupplierFabrics = null;

            //Perbaharui tampilan dan data
            grdSupplierFabric.Rebind();
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            SupplierFabrics = null; //Reset Record Detail
            grdSupplierFabric.DataSource = SupplierFabrics;
            grdSupplierFabric.DataBind();
        }

        protected void grdSupplierFabric_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdSupplierFabric.DataSource = SupplierFabrics;
        }

        protected void grdSupplierFabric_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            String supplierID =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        SupplierFabricMetadata.ColumnNames.SupplierID]);
            SupplierFabric entity = FindItemGrid(supplierID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdSupplierFabric_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String supplierID =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][SupplierFabricMetadata.ColumnNames.SupplierID]);
            SupplierFabric entity = FindItemGrid(supplierID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdSupplierFabric_InsertCommand(object source, GridCommandEventArgs e)
        {
            SupplierFabric entity = SupplierFabrics.AddNew();
            SetEntityValue(entity, e);
        }

        private void SetEntityValue(SupplierFabric entity, GridCommandEventArgs e)
        {
            SupplierFabricDetail userControl =
                (SupplierFabricDetail) e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SupplierID = userControl.SupplierID;
                entity.SupplierName = userControl.SupplierName;
                entity.IsActive = userControl.IsActive;
            }
        }

        private SupplierFabric FindItemGrid(string supplierID)
        {
            SupplierFabricCollection coll = SupplierFabrics;
            SupplierFabric retval = null;
            foreach (SupplierFabric rec in coll)
            {
                if (rec.SupplierID.Equals(supplierID))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        #endregion
    }
}