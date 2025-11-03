using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    /// <summary>
    /// Registration Info List
    ///
    /// Modified By: Handono (Timika 13 Nov 2019)
    /// Note:
    /// Asalnya pakai MasterPageDialog tetapi event insertnya tidak jalan dan belum diketahui penyebabnya
    /// Sehingga dirubah tidak menggunakan master page
    /// </summary>
    public partial class RegistrationInfoList : BasePage
    {
        private AppAutoNumberLast _autoNumber;


        private string RegistrationNo
        {
            get { return Page.Request.QueryString["regNo"]; }
        }

        private string RegistrationInfo
        {
            get { return Page.Request.QueryString["lblRegistrationInfo"]; }
        }

        private string PatientID
        {
            get
            {
                var r = new Registration();
                if (r.LoadByPrimaryKey(Page.Request.QueryString["regNo"]))
                    return r.PatientID;

                return string.Empty;
            }
        }

        private string PatientInfo
        {
            get { return Page.Request.QueryString["lblPatientInfo"]; }
        }



        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


        }


        #region Private Method
        private void SaveEntity(BusinessObject.RegistrationInfo entity, string mode)
        {
            var regInfoCount = new RegistrationInfoSumary();
            if (!regInfoCount.LoadByPrimaryKey(entity.RegistrationNo))
            {
                regInfoCount.AddNew();
                regInfoCount.RegistrationNo = entity.RegistrationNo;
                regInfoCount.NoteCount = 0;
                regInfoCount.NoteMedicalCount = 0;
                regInfoCount.CreatedByUserID = AppSession.UserLogin.UserID;
                regInfoCount.CreatedDateTime = (new DateTime()).NowAtSqlServer();
            }
            regInfoCount.LastUpdateByUserID = AppSession.UserLogin.UserID;
            regInfoCount.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            using (esTransactionScope trans = new esTransactionScope())
            {
                if (mode == "new")
                {
                    _autoNumber.Save();
                    regInfoCount.NoteCount += 1;
                }
                else if (mode == "del")
                {
                    entity.MarkAsDeleted();
                    regInfoCount.NoteCount -= 1;
                }

                entity.Save();
                regInfoCount.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void SaveEntity2(BusinessObject.PatientInfo entity2, string mode)
        {

            var PatInfoCount = new PatientInfoSumary();
            if (!PatInfoCount.LoadByPrimaryKey(entity2.PatientID))
            {
                PatInfoCount.AddNew();
                PatInfoCount.PatientID = entity2.PatientID;
                PatInfoCount.NoteCount = 0;
                PatInfoCount.CreatedByUserID = AppSession.UserLogin.UserID;
                PatInfoCount.CreatedDateTime = (new DateTime()).NowAtSqlServer();
            }
            PatInfoCount.LastUpdateByUserID = AppSession.UserLogin.UserID;
            PatInfoCount.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            using (esTransactionScope trans = new esTransactionScope())
            {
                if (mode == "new")
                {
                    _autoNumber.Save();
                    PatInfoCount.NoteCount += 1;
                }
                else if (mode == "del")
                {
                    entity2.MarkAsDeleted();
                    PatInfoCount.NoteCount -= 1;
                }

                entity2.Save();
                PatInfoCount.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }
        #endregion

        protected void grdRegistrationInfo_ItemInserted(object source, GridInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.ExceptionHandled = true;
                SetMessage("Data cannot be inserted. Reason: " + e.Exception.Message);
            }
            else
            {
                SetMessage("New data is inserted!");
                ((RadGrid)source).Rebind();
            }
        }
        protected void grdRegistrationInfo_ItemUpdated(object source, GridUpdatedEventArgs e)
        {
            GridEditableItem item = (GridEditableItem)e.Item;
            String id = item.GetDataKeyValue("RegistrationInfoID").ToString();

            if (e.Exception != null)
            {
                e.KeepInEditMode = true;
                e.ExceptionHandled = true;
                SetMessage("Data with ID: " + id + " cannot be updated. Reason: " + e.Exception.Message);
            }
            else
            {
                SetMessage("Data with ID: " + id + " is updated!");
                ((RadGrid)source).Rebind();
            }
        }
        protected void grdRegistrationInfo_ItemDeleted(object source, GridDeletedEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.Item;
            String id = dataItem.GetDataKeyValue("RegistrationInfoID").ToString();

            if (e.Exception != null)
            {
                e.ExceptionHandled = true;
                SetMessage("Data with ID: " + id + " cannot be deleted. Reason: " + e.Exception.Message);
            }
            else
            {
                SetMessage("Data with ID: " + id + " is deleted!");
                ((RadGrid)source).Rebind();
            }
        }

        protected void grdRegistrationInfo_DataBound(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(gridMessage))
            {
                DisplayMessage(gridMessage, (RadGrid)sender);
            }
        }
        protected void grdRegistrationInfo_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack)
            {
                grdRegistrationInfo.DataSource = string.Empty;
                grdRegistrationInfo.Columns[0].Visible = false;
                grdRegistrationInfo.Columns[grdRegistrationInfo.Columns.Count - 1].Visible = false;
            }
            var query = new RegistrationInfoQuery("i");
            var qUser = new AppUserQuery("u");
            query.LeftJoin(qUser).On(query.CreatedByUserID == qUser.UserID);
            query.Where(query.RegistrationNo == RegistrationNo);
            query.OrderBy(query.CreatedDateTime.Descending);

            grdRegistrationInfo.DataSource = query.LoadDataTable();
        }

        protected void grdPatientInfo_ItemInserted(object source, GridInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.ExceptionHandled = true;
                SetMessage("Data cannot be inserted. Reason: " + e.Exception.Message);
            }
            else
            {
                SetMessage("New data is inserted!");
                ((RadGrid)source).Rebind();
            }
        }
        protected void grdPatientInfo_ItemUpdated(object source, GridUpdatedEventArgs e)
        {
            GridEditableItem item = (GridEditableItem)e.Item;
            String id = item.GetDataKeyValue("PatientInfoID").ToString();

            if (e.Exception != null)
            {
                e.KeepInEditMode = true;
                e.ExceptionHandled = true;
                SetMessage("Data with ID: " + id + " cannot be updated. Reason: " + e.Exception.Message);
            }
            else
            {
                SetMessage("Data with ID: " + id + " is updated!");
                ((RadGrid)source).Rebind();
            }
        }
        protected void grdPatientInfo_ItemDeleted(object source, GridDeletedEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.Item;
            String id = dataItem.GetDataKeyValue("PatientInfoID").ToString();

            if (e.Exception != null)
            {
                e.ExceptionHandled = true;
                SetMessage("Data with ID: " + id + " cannot be deleted. Reason: " + e.Exception.Message);
            }
            else
            {
                SetMessage("Data with ID: " + id + " is deleted!");
                ((RadGrid)source).Rebind();
            }
        }

        protected void grdPatientInfo_DataBound(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(gridMessage))
            {
                DisplayMessage(gridMessage, (RadGrid)sender);
            }
        }
        protected void grdPatientInfo_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {

            if (!IsPostBack)
            {
                grdPatientInfo.DataSource = string.Empty;
                grdPatientInfo.Columns[0].Visible = false;
                grdPatientInfo.Columns[grdPatientInfo.Columns.Count - 1].Visible = false;
            }
            var query = new PatientInfoQuery("i");
            var qUser = new AppUserQuery("u");
            query.LeftJoin(qUser).On(query.CreatedByUserID == qUser.UserID);
            query.Where(query.PatientID == PatientID);
            query.OrderBy(query.CreatedDateTime.Descending);

            grdPatientInfo.DataSource = query.LoadDataTable();
        }


        private void DisplayMessage(string text, RadGrid rg)
        {
            rg.Controls.Add(new LiteralControl(string.Format("<span style='color:red'>{0}</span>", text)));
        }
        private void SetMessage(string message)
        {
            gridMessage = message;
        }

        private string gridMessage = null;

        private object GetColVal(GridColumn column, GridEditManager editMan)
        {
            if (column is IGridEditableColumn)
            {
                IGridEditableColumn editableCol = (column as IGridEditableColumn);
                if (editableCol.IsEditable)
                {
                    IGridColumnEditor editor = editMan.GetColumnEditor(editableCol);

                    string editorText = "unknown";
                    object editorValue = null;

                    if (editor is GridTextColumnEditor)
                    {
                        editorText = (editor as GridTextColumnEditor).Text;
                        editorValue = (editor as GridTextColumnEditor).Text;
                    }

                    if (editor is GridBoolColumnEditor)
                    {
                        editorText = (editor as GridBoolColumnEditor).Value.ToString();
                        editorValue = (editor as GridBoolColumnEditor).Value;
                    }

                    if (editor is GridDropDownColumnEditor)
                    {
                        editorText = (editor as GridDropDownColumnEditor).SelectedText + "; " +
                         (editor as GridDropDownColumnEditor).SelectedValue;
                        editorValue = (editor as GridDropDownColumnEditor).SelectedValue;
                    }

                    return editorValue;

                }
            }

            return null;
        }
        protected void grdRegistrationInfo_InsertCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            GridEditManager editMan = editedItem.EditManager;

            GridColumn cInformation = e.Item.OwnerTableView.Columns[1];

            BusinessObject.RegistrationInfo entity = new BusinessObject.RegistrationInfo();
            entity.AddNew();

            _autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.RegistrationInfoNo);

            entity.RegistrationInfoID = _autoNumber.LastCompleteNumber;
            entity.RegistrationNo = RegistrationNo;
            entity.Information = GetColVal(cInformation, editMan).ToString();

            entity.CreatedByUserID = AppSession.UserLogin.UserID;
            entity.CreatedDateTime = (new DateTime()).NowAtSqlServer();
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            SaveEntity(entity, "new");

            // set datasource to null will fire needdatasource when u call rebind. strange isn't it?
            ((RadGrid)source).DataSource = null;
            ((RadGrid)source).Rebind();

            SetMessage("New data with ID " + entity.RegistrationInfoID + " is inserted!");

            UpdateParentOnStartup();
        }
        protected void grdRegistrationInfo_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            GridEditManager editMan = editedItem.EditManager;

            GridColumn cInformation = e.Item.OwnerTableView.Columns[1];

            string ID = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["RegistrationInfoID"].ToString();

            BusinessObject.RegistrationInfo entity = new BusinessObject.RegistrationInfo();
            entity.LoadByPrimaryKey(ID);
            if (entity.CreatedByUserID != AppSession.UserLogin.UserID)
            {
                SetMessage("You have no right to update this record!");
            }
            else
            {
                entity.Information = GetColVal(cInformation, editMan).ToString();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                SaveEntity(entity, "update");

                // set datasource to null will fire needdatasource when u call rebind. strange isn't it?
                ((RadGrid)source).DataSource = null;
                ((RadGrid)source).Rebind();

                SetMessage("Data with ID: " + ID + " is updated!");

                UpdateParentOnStartup();
            }
        }
        protected void grdRegistrationInfo_DeleteCommand(object source, GridCommandEventArgs e)
        {
            string ID = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["RegistrationInfoID"].ToString();

            // delete entity
            BusinessObject.RegistrationInfo entity = new BusinessObject.RegistrationInfo();
            entity.LoadByPrimaryKey(ID);
            if (entity.CreatedByUserID != AppSession.UserLogin.UserID)
            {
                SetMessage("You have no right to remove this record!");
            }
            else
            {
                SaveEntity(entity, "del");

                // set datasource to null will fire needdatasource when u call rebind. strange isn't it?
                ((RadGrid)source).DataSource = null;
                ((RadGrid)source).Rebind();

                SetMessage("Data with ID: " + ID + " is deleted!");

                UpdateParentOnStartup();
            }
        }

        protected void grdPatientInfo_InsertCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            GridEditManager editMan = editedItem.EditManager;

            GridColumn cInformation = e.Item.OwnerTableView.Columns[1];

            BusinessObject.PatientInfo entity2 = new BusinessObject.PatientInfo();
            entity2.AddNew();

            _autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.PatientInfoNo);

            entity2.PatientInfoID = _autoNumber.LastCompleteNumber;
            entity2.PatientID = PatientID;
            entity2.Information = GetColVal(cInformation, editMan).ToString();

            entity2.CreatedByUserID = AppSession.UserLogin.UserID;
            entity2.CreatedDateTime = (new DateTime()).NowAtSqlServer();
            entity2.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity2.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            SaveEntity2(entity2, "new");

            // set datasource to null will fire needdatasource when u call rebind. strange isn't it?
            ((RadGrid)source).DataSource = null;
            ((RadGrid)source).Rebind();

            SetMessage("New data with ID " + entity2.PatientInfoID + " is inserted!");

            UpdateParentOnStartup();
        }
        protected void grdPatientInfo_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            GridEditManager editMan = editedItem.EditManager;

            GridColumn cInformation = e.Item.OwnerTableView.Columns[1];

            string ID = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["PatientInfoID"].ToString();

            BusinessObject.PatientInfo entity2 = new BusinessObject.PatientInfo();
            entity2.LoadByPrimaryKey(ID);
            if (entity2.CreatedByUserID != AppSession.UserLogin.UserID)
            {
                SetMessage("You have no right to update this record!");
            }
            else
            {
                entity2.Information = GetColVal(cInformation, editMan).ToString();
                entity2.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity2.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                SaveEntity2(entity2, "update");

                // set datasource to null will fire needdatasource when u call rebind. strange isn't it?
                ((RadGrid)source).DataSource = null;
                ((RadGrid)source).Rebind();

                SetMessage("Data with ID: " + ID + " is updated!");

                UpdateParentOnStartup();
            }
        }
        protected void grdPatientInfo_DeleteCommand(object source, GridCommandEventArgs e)
        {
            string ID = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PatientInfoID"].ToString();

            // delete entity
            BusinessObject.PatientInfo entity2 = new BusinessObject.PatientInfo();
            entity2.LoadByPrimaryKey(ID);
            if (entity2.CreatedByUserID != AppSession.UserLogin.UserID)
            {
                SetMessage("You have no right to remove this record!");
            }
            else
            {
                SaveEntity2(entity2, "del");

                // set datasource to null will fire needdatasource when u call rebind. strange isn't it?
                ((RadGrid)source).DataSource = null;
                ((RadGrid)source).Rebind();

                SetMessage("Data with ID: " + ID + " is deleted!");

                UpdateParentOnStartup();
            }
        }

        private void UpdateParentOnStartup()
        {
            var iCount = grdRegistrationInfo.Items.Count + grdPatientInfo.Items.Count;

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "UpdateRegInfo", "UpdateInformationCount('" + RegistrationInfo + "', '" + iCount.ToString() + "');", true);

            //var iCount2 = grdPatientInfo.Items.Count;
            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "UpdateRegInfo", "UpdateInformationCount('" + PatientInfo + "', '" + iCount.ToString() + "');", true);
        }
    }

}