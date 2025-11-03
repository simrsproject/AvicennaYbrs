using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.ControlPanel.PrinterManagement
{
    public partial class UserHostPrinterDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "UserHostPrinterSearch.aspx";
            UrlPageList = "UserHostPrinterList.aspx";
			
			ProgramID = AppConstant.Program.UserHostPrinter; 

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
            OnPopulateEntryControl(new UserHostPrinter());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            UserHostPrinter entity = new UserHostPrinter();
            if (entity.LoadByPrimaryKey(txtUserHostName.Text))
            {
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
            UserHostPrinter entity = new UserHostPrinter();
            if (entity.LoadByPrimaryKey(txtUserHostName.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new UserHostPrinter();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            UserHostPrinter entity = new UserHostPrinter();
            if (entity.LoadByPrimaryKey(txtUserHostName.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("UserHostName='{0}'", txtUserHostName.Text.Trim());
            auditLogFilter.TableName = "UserHostPrinter";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(Temiang.Avicenna.Common.AppEnum.DataMode oldVal, Temiang.Avicenna.Common.AppEnum.DataMode newVal)
        {
            txtUserHostName.Enabled = (newVal == Temiang.Avicenna.Common.AppEnum.DataMode.New);
            RefreshCommandItemUserHostPrinterOther(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            UserHostPrinter entity = new UserHostPrinter();
            if (parameters.Length > 0)
            {
                String userHostName = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(userHostName);
            }
            else
            {
                entity.LoadByPrimaryKey(txtUserHostName.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            UserHostPrinter userHostPrinter = (UserHostPrinter)entity;
            txtUserHostName.Text = userHostPrinter.UserHostName;
            txtDescription.Text = userHostPrinter.Description;
            if (userHostPrinter.PrinterID != null)
            {
                //var printer = new Printer();
                //printer.LoadByPrimaryKey(userHostPrinter.PrinterID);
                //cboPrinterID.SelectedValue = printer.PrinterID;
                //cboPrinterID.Text = printer.PrinterName;

                PrinterQuery query = new PrinterQuery();
                query.Where(query.PrinterID == userHostPrinter.PrinterID);
                query.Select(query.PrinterID, query.PrinterName);
                cboPrinterID.DataSource = query.LoadDataTable();
                cboPrinterID.DataBind();
                ComboBox.SelectedValue(cboPrinterID, userHostPrinter.PrinterID);
            }
            else
            {
                cboPrinterID.Items.Clear();
                cboPrinterID.Text = string.Empty;
            }

            //Display Data Detail
            PopulateUserHostPrinterOtherGrid();
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(UserHostPrinter entity)
        {
            entity.UserHostName = txtUserHostName.Text;
            entity.Description = txtDescription.Text;
            entity.PrinterID = cboPrinterID.SelectedValue;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            foreach (UserHostPrinterOther item in UserHostPrinterOthers)
            {
                item.UserHostName = txtUserHostName.Text;

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void SaveEntity(UserHostPrinter entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                UserHostPrinterOthers.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            UserHostPrinterQuery que = new UserHostPrinterQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.UserHostName > txtUserHostName.Text);
                que.OrderBy(que.UserHostName.Ascending);
            }
            else
            {
                que.Where(que.UserHostName < txtUserHostName.Text);
                que.OrderBy(que.UserHostName.Descending);
            }
            UserHostPrinter entity = new UserHostPrinter();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion
        protected void cboPrinterID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            PrinterQuery query = new PrinterQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.PrinterID,
                    query.PrinterName
                );
            query.Where
                (
                    query.Or
                        (
                            query.PrinterID.Like(searchTextContain),
                            query.PrinterName.Like(searchTextContain)
                        )
                );

            cboPrinterID.DataSource = query.LoadDataTable();
            cboPrinterID.DataBind();
        }

        protected void cboPrinterID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PrinterName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PrinterID"].ToString();
        }

        #region Record Detail Method Function UserHostPrinterOther
        private void RefreshCommandItemUserHostPrinterOther(Temiang.Avicenna.Common.AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != Temiang.Avicenna.Common.AppEnum.DataMode.Read);
            grdUserHostPrinterOther.Columns[0].Visible = isVisible;
            grdUserHostPrinterOther.Columns[grdUserHostPrinterOther.Columns.Count - 1].Visible = isVisible;

            grdUserHostPrinterOther.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdUserHostPrinterOther.Rebind();
        }

        private UserHostPrinterOtherCollection UserHostPrinterOthers
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collUserHostPrinterOther"];
                    if (obj != null)
                    {
                        return ((UserHostPrinterOtherCollection)(obj));
                    }
                }

                UserHostPrinterOtherCollection coll = new UserHostPrinterOtherCollection();
                UserHostPrinterOtherQuery query = new UserHostPrinterOtherQuery("a");
                AppProgramQuery programQuery = new AppProgramQuery("b");
                query.InnerJoin(programQuery).On(query.ProgramID == programQuery.ProgramID);
                PrinterQuery printerQuery = new PrinterQuery("c");
                query.InnerJoin(printerQuery).On(query.PrinterID == printerQuery.PrinterID);
                query.Select(query.SelectAll(), printerQuery.PrinterName.As("refToPrinter_PrinterName"), programQuery.ProgramName.As("refToProgram_ProgramName"));
                query.Where(query.UserHostName == txtUserHostName.Text); 
                query.OrderBy(query.ProgramID.Ascending); 
                coll.Load(query);

                Session["collUserHostPrinterOther"] = coll;
                return coll;
            }
            set { Session["collUserHostPrinterOther"] = value; }
        }

        private void PopulateUserHostPrinterOtherGrid()
        {
            //Display Data Detail
            UserHostPrinterOthers = null; //Reset Record Detail
            grdUserHostPrinterOther.DataSource = UserHostPrinterOthers; //Requery
            grdUserHostPrinterOther.MasterTableView.IsItemInserted = false;
            grdUserHostPrinterOther.MasterTableView.ClearEditItems();
            grdUserHostPrinterOther.DataBind();
        }

        protected void grdUserHostPrinterOther_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdUserHostPrinterOther.DataSource = UserHostPrinterOthers;
        }

        protected void grdUserHostPrinterOther_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String programID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][UserHostPrinterOtherMetadata.ColumnNames.ProgramID]);
            UserHostPrinterOther entity = FindUserHostPrinterOther(programID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdUserHostPrinterOther_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String programID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][UserHostPrinterOtherMetadata.ColumnNames.ProgramID]);
            UserHostPrinterOther entity = FindUserHostPrinterOther(programID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdUserHostPrinterOther_InsertCommand(object source, GridCommandEventArgs e)
        {
            UserHostPrinterOther entity = UserHostPrinterOthers.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdUserHostPrinterOther.Rebind();
        }
        private UserHostPrinterOther FindUserHostPrinterOther(String programID)
        {
            UserHostPrinterOtherCollection coll = UserHostPrinterOthers;
            UserHostPrinterOther retEntity = null;
            foreach (UserHostPrinterOther rec in coll)
            {
                if (rec.ProgramID.Equals(programID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(UserHostPrinterOther entity, GridCommandEventArgs e)
        {
            UserHostPrinterOtherDetail userControl = (UserHostPrinterOtherDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.UserHostName = txtUserHostName.Text;
                entity.ProgramID = userControl.ProgramID;
                entity.PrinterID = userControl.PrinterID;
                entity.ProgramName = userControl.ProgramName;
                entity.PrinterName = userControl.PrinterName;
            }
        }

        #endregion


    }
}
