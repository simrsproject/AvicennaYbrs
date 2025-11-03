using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class StandardSalaryDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "StandardSalarySearch.aspx";
            UrlPageList = "StandardSalaryList.aspx";

            ProgramID = AppConstant.Program.StandardSalary; //TODO: Isi ProgramID
            txtStandardSalaryID.Text = "1";

            //StandardReference Initialize
            if (!IsPostBack)
            {
            }

            //PopUp Search
            if (!IsCallback)
            {

            }
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
            OnPopulateEntryControl(new StandardSalary());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            StandardSalary entity = new StandardSalary();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtStandardSalaryID.Text)))
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
            StandardSalary entity = new StandardSalary();
            entity = new StandardSalary();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            StandardSalary entity = new StandardSalary();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtStandardSalaryID.Text)))
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
            auditLogFilter.PrimaryKeyData = string.Format("StandardSalaryID='{0}'", txtStandardSalaryID.Text.Trim());
            auditLogFilter.TableName = "StandardSalary";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(Common.AppEnum.DataMode oldVal, Common.AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtStandardSalaryID.Enabled = (newVal == Common.AppEnum.DataMode.New);
            RefreshCommandItemStandardSalaryFaktor(newVal);
            cboPositionGradeID.Enabled= (newVal == Common.AppEnum.DataMode.New);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            StandardSalary entity = new StandardSalary();
            if (parameters.Length > 0)
            {
                string standardSalaryID = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(standardSalaryID));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtStandardSalaryID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            StandardSalary standardSalary = (StandardSalary)entity;
            txtStandardSalaryID.Value = Convert.ToDouble(standardSalary.StandardSalaryID);

            if (!string.IsNullOrEmpty(standardSalary.PositionGradeID.ToString()))
            {
                var pgQuery = new PositionGradeQuery();
                pgQuery.Where(pgQuery.PositionGradeID == Convert.ToInt32(standardSalary.PositionGradeID));
                cboPositionGradeID.DataSource = pgQuery.LoadDataTable();
                cboPositionGradeID.DataBind();
                cboPositionGradeID.SelectedValue = standardSalary.PositionGradeID.ToString();

                var pg = new PositionGrade();
                pg.LoadByPrimaryKey(Convert.ToInt32(standardSalary.PositionGradeID));
                cboPositionGradeID.Text = pg.PositionGradeName;
            }

            txtValidFrom.SelectedDate = standardSalary.ValidFrom;
            txtValidTo.SelectedDate = standardSalary.ValidTo;

            //Display Data Detail
            PopulateStandardSalaryFaktorGrid();
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(StandardSalary entity)
        {
            entity.StandardSalaryID = Convert.ToInt32(txtStandardSalaryID.Value);
            entity.PositionGradeID = Convert.ToInt32(cboPositionGradeID.SelectedValue);
            entity.ValidFrom = txtValidFrom.SelectedDate;
            entity.ValidTo = txtValidTo.SelectedDate;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //--> Standard Salary Faktor
            foreach (StandardSalaryFaktor faktor in StandardSalaryFaktors)
            {
                faktor.StandardSalaryID = Convert.ToInt32(txtStandardSalaryID.Text);
                //Last Update Status
                if (faktor.es.IsAdded || faktor.es.IsModified)
                {
                    faktor.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    faktor.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void SaveEntity(StandardSalary entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                StandardSalaryFaktors.Save();

                //Commit if success, Rollback if failed
                trans.Complete();

            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            StandardSalaryQuery que = new StandardSalaryQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.StandardSalaryID > txtStandardSalaryID.Text);
                que.OrderBy(que.StandardSalaryID.Ascending);
            }
            else
            {
                que.Where(que.StandardSalaryID < txtStandardSalaryID.Text);
                que.OrderBy(que.StandardSalaryID.Descending);
            }
            StandardSalary entity = new StandardSalary();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged

        #endregion

        #region ComboBox Function

        protected void cboPositionGradeID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            PositionGradeQuery query = new PositionGradeQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.PositionGradeID,
                    query.PositionGradeCode,
                    query.PositionGradeName
                );
            query.Where
                (
                    query.Or
                        (
                            query.PositionGradeID.Like(searchTextContain),
                            query.PositionGradeName.Like(searchTextContain)
                        )
                );

            cboPositionGradeID.DataSource = query.LoadDataTable();
            cboPositionGradeID.DataBind();
        }

        protected void cboPositionGradeID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PositionGradeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PositionGradeID"].ToString();
        }

        #endregion ComboBox Function

        #region Record Detail Method Function StandardSalaryFaktor
        //TODO: Isi ulang field untuk relasi child ke parent nya di method SetEntity
        //TODO: Tambahkan perintah StandardSalaryFaktors.Save(); di method SaveEntity
        //TODO: Panggil method RefreshCommandItemGrid dari OnDataModeChanged
        private void RefreshCommandItemStandardSalaryFaktor(Common.AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != Common.AppEnum.DataMode.Read);
            grdStandardSalaryFaktor.Columns[0].Visible = isVisible;
            grdStandardSalaryFaktor.Columns[grdStandardSalaryFaktor.Columns.Count - 1].Visible = isVisible;

            grdStandardSalaryFaktor.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdStandardSalaryFaktor.Rebind();
        }

        private StandardSalaryFaktorCollection StandardSalaryFaktors
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collStandardSalaryFaktor"];
                    if (obj != null)
                    {
                        return ((StandardSalaryFaktorCollection)(obj));
                    }
                }

                StandardSalaryFaktorCollection coll = new StandardSalaryFaktorCollection();
                StandardSalaryFaktorQuery query = new StandardSalaryFaktorQuery();

                query.Select
                    (
                       query.StandardSalaryFaktorID,
                       query.StandardSalaryID,
                       query.GradeServiceYear,
                       query.AmountSalary,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.Where(query.StandardSalaryID == txtStandardSalaryID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.GradeServiceYear.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collStandardSalaryFaktor"] = coll;
                return coll;
            }
            set { Session["collStandardSalaryFaktor"] = value; }
        }

        private void PopulateStandardSalaryFaktorGrid()
        {
            //Display Data Detail
            StandardSalaryFaktors = null; //Reset Record Detail
            grdStandardSalaryFaktor.DataSource = StandardSalaryFaktors; //Requery
            grdStandardSalaryFaktor.MasterTableView.IsItemInserted = false;
            grdStandardSalaryFaktor.MasterTableView.ClearEditItems();
            grdStandardSalaryFaktor.DataBind();
        }

        protected void grdStandardSalaryFaktor_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdStandardSalaryFaktor.DataSource = StandardSalaryFaktors;
        }

        protected void grdStandardSalaryFaktor_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 standardSalaryFaktorID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][StandardSalaryFaktorMetadata.ColumnNames.StandardSalaryFaktorID]);
            StandardSalaryFaktor entity = FindStandardSalaryFaktor(standardSalaryFaktorID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdStandardSalaryFaktor_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 standardSalaryFaktorID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][StandardSalaryFaktorMetadata.ColumnNames.StandardSalaryFaktorID]);
            StandardSalaryFaktor entity = FindStandardSalaryFaktor(standardSalaryFaktorID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdStandardSalaryFaktor_InsertCommand(object source, GridCommandEventArgs e)
        {
            StandardSalaryFaktor entity = StandardSalaryFaktors.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdStandardSalaryFaktor.Rebind();
        }
        private StandardSalaryFaktor FindStandardSalaryFaktor(Int32 standardSalaryFaktorID)
        {
            StandardSalaryFaktorCollection coll = StandardSalaryFaktors;
            StandardSalaryFaktor retEntity = null;
            foreach (StandardSalaryFaktor rec in coll)
            {
                if (rec.StandardSalaryFaktorID.Equals(standardSalaryFaktorID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(StandardSalaryFaktor entity, GridCommandEventArgs e)
        {
            StandardSalaryFaktorDetail userControl = (StandardSalaryFaktorDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.StandardSalaryFaktorID = userControl.StandardSalaryFaktorID;
                entity.GradeServiceYear = userControl.GradeServiceYear;
                entity.AmountSalary = userControl.AmountSalary;

            }
        }

        #endregion


    }
}
