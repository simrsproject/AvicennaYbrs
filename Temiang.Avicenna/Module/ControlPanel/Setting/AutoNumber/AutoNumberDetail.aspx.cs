using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.ControlPanel.Setting
{
    public partial class AppAutoNumberDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "AutoNumberSearch.aspx";
            UrlPageList = "AutoNumberList.aspx";

            ProgramID = AppConstant.Program.AutoNumbering;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.Initialize(cboSRAutoNumber, AppEnum.StandardReference.AutoNumber);
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
            OnPopulateEntryControl(new AppAutoNumber());
            chkIsUsedYearToDateOrder.Checked = true;
        }
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            AppAutoNumber entity = new AppAutoNumber();
            if (!entity.LoadByPrimaryKey(cboSRAutoNumber.SelectedValue, txtEffectiveDate.SelectedDate.Value))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
        }
        protected override void OnMenuEditClick()
        {
            // Hanya bisa dirubah bila belum dipakai di transaksi dan dalam modus bukan read
            bool enable = grdAppAutoNumberLast.Items.Count == 0;
            txtPrefik.Enabled = enable;
            txtSeparatorAfterPrefik.Enabled = enable;
            chkIsUsedDepartment.Enabled = enable;
            txtSeparatorAfterDept.Enabled = enable;
            chkIsUsedYear.Enabled = enable;
            txtYearDigit.Enabled = enable;
            txtSeparatorAfterYear.Enabled = enable;
            chkIsUsedMonth.Enabled = enable;
            chkIsMonthInRomawi.Enabled = enable;
            txtSeparatorAfterMonth.Enabled = enable;
            chkIsUsedDay.Enabled = enable;
            txtSeparatorAfterDay.Enabled = enable;
            txtNumberLength.Enabled = enable;
            txtNumberGroupLength.Enabled = enable;
            txtNumberGroupSeparator.Enabled = enable;
            txtSeparatorAfterNumber.Enabled = enable;
            chkIsUsedYearToDateOrder.Enabled = enable;
        }
        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            //string autoNumberID = cboSRAutoNumber.SelectedValue;
            //DateTime efDate = txtEffectiveDate.SelectedDate == null ? DateTime.Now : txtEffectiveDate.SelectedDate.Value;

            //AppAutoNumber entity = new AppAutoNumber();
            //entity.LoadByPrimaryKey(autoNumberID, efDate);
            //entity.MarkAsDeleted();

            ////Detil
            //AppAutoNumberLastCollection coll = new AppAutoNumberLastCollection();
            //coll.Query.Where(coll.Query.SRAutoNumber == autoNumberID, coll.Query.EffectiveDate == efDate);
            //coll.LoadAll();
            //coll.MarkAllAsDeleted();
            //using (esTransactionScope trans = new esTransactionScope())
            //{
            //    coll.Save();
            //    entity.Save();

            //    //Commit if success, Rollback if failed
            //    trans.Complete();
            //}
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            AppAutoNumber entity = new AppAutoNumber();
            if (entity.LoadByPrimaryKey(cboSRAutoNumber.SelectedValue, txtEffectiveDate.SelectedDate.Value))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new AppAutoNumber();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            AppAutoNumber entity = new AppAutoNumber();
            if (entity.LoadByPrimaryKey(cboSRAutoNumber.SelectedValue, txtEffectiveDate.SelectedDate.Value))
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
            auditLogFilter.PrimaryKeyData = string.Format("SRAutoNumber='{0}' AND EffectiveDate='{1}'", cboSRAutoNumber.SelectedValue, txtEffectiveDate.SelectedDate.Value.Date);
            auditLogFilter.TableName = "AppAutoNumber";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            cboSRAutoNumber.Enabled = (newVal == AppEnum.DataMode.New);
            txtEffectiveDate.Enabled = (newVal == AppEnum.DataMode.New);

            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            AppAutoNumber entity = new AppAutoNumber();
            if (parameters.Length > 0)
            {
                String sRAutoNumber = (String)parameters[0];
                DateTime effectiveDate = Convert.ToDateTime(parameters[1]);

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(sRAutoNumber, effectiveDate);
            }
            else
            {
                entity.LoadByPrimaryKey(cboSRAutoNumber.Text, txtEffectiveDate.SelectedDate.Value);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            AppAutoNumber appAutoNumber = (AppAutoNumber)entity;
            cboSRAutoNumber.SelectedValue = appAutoNumber.SRAutoNumber;
            txtEffectiveDate.SelectedDate = appAutoNumber.EffectiveDate;
            txtPrefik.Text = appAutoNumber.Prefik;
            txtSeparatorAfterPrefik.Text = appAutoNumber.SeparatorAfterPrefik;
            chkIsUsedDepartment.Checked = appAutoNumber.IsUsedDepartment ?? false;
            txtSeparatorAfterDept.Text = appAutoNumber.SeparatorAfterDept;
            chkIsUsedYear.Checked = appAutoNumber.IsUsedYear ?? false;
            txtYearDigit.Value = Convert.ToDouble(appAutoNumber.YearDigit);
            txtSeparatorAfterYear.Text = appAutoNumber.SeparatorAfterYear;
            chkIsUsedMonth.Checked = appAutoNumber.IsUsedMonth ?? false;
            chkIsMonthInRomawi.Checked = appAutoNumber.IsMonthInRomawi ?? false;
            txtSeparatorAfterMonth.Text = appAutoNumber.SeparatorAfterMonth;
            chkIsUsedDay.Checked = appAutoNumber.IsUsedDay ?? false;
            txtSeparatorAfterDay.Text = appAutoNumber.SeparatorAfterDay;
            txtNumberLength.Value = Convert.ToDouble(appAutoNumber.NumberLength);
            txtNumberGroupLength.Value = Convert.ToDouble(appAutoNumber.NumberGroupLength);
            txtNumberGroupSeparator.Text = appAutoNumber.NumberGroupSeparator;
            txtSeparatorAfterNumber.Text = appAutoNumber.SeparatorAfterNumber;
            chkIsUsedYearToDateOrder.Checked = appAutoNumber.IsUsedYearToDateOrder ?? false;

            //Display Data Detail
            PopulateGridDetail();
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(AppAutoNumber entity)
        {
            entity.SRAutoNumber = cboSRAutoNumber.SelectedValue;
            entity.EffectiveDate = txtEffectiveDate.SelectedDate.Value.Date;
            entity.Prefik = txtPrefik.Text;
            entity.SeparatorAfterPrefik = txtSeparatorAfterPrefik.Text;
            entity.IsUsedDepartment = chkIsUsedDepartment.Checked;
            entity.SeparatorAfterDept = txtSeparatorAfterDept.Text;
            entity.IsUsedYear = chkIsUsedYear.Checked;
            entity.YearDigit = Convert.ToByte(txtYearDigit.Value);
            entity.SeparatorAfterYear = txtSeparatorAfterYear.Text;
            entity.IsUsedMonth = chkIsUsedMonth.Checked;
            entity.IsMonthInRomawi = chkIsMonthInRomawi.Checked;
            entity.SeparatorAfterMonth = txtSeparatorAfterMonth.Text;
            entity.IsUsedDay = chkIsUsedDay.Checked;
            entity.SeparatorAfterDay = txtSeparatorAfterDay.Text;
            entity.NumberLength = Convert.ToByte(txtNumberLength.Value);
            entity.NumberGroupLength = Convert.ToByte(txtNumberGroupLength.Value);
            entity.NumberGroupSeparator = txtNumberGroupSeparator.Text;
            entity.SeparatorAfterNumber = txtSeparatorAfterNumber.Text;
            entity.IsUsedYearToDateOrder = chkIsUsedYearToDateOrder.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(AppAutoNumber entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                AppAutoNumberLasts.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            AppAutoNumberQuery que = new AppAutoNumberQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.SRAutoNumber > cboSRAutoNumber.SelectedValue);
                que.OrderBy(que.SRAutoNumber.Ascending);
            }
            else
            {
                que.Where(que.SRAutoNumber < cboSRAutoNumber.SelectedValue);
                que.OrderBy(que.SRAutoNumber.Descending);
            }
            AppAutoNumber entity = new AppAutoNumber();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Record Detail Method Function
        private AppAutoNumberLastCollection AppAutoNumberLasts
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collAppAutoNumberLast"];
                    if (obj != null)
                    {
                        return ((AppAutoNumberLastCollection)(obj));
                    }
                }

                AppAutoNumberLastCollection coll = new AppAutoNumberLastCollection();
                AppAutoNumberLastQuery query = new AppAutoNumberLastQuery();

                string autoNumberID = cboSRAutoNumber.SelectedValue;
                DateTime efDate = txtEffectiveDate.SelectedDate == null ? DateTime.Now : txtEffectiveDate.SelectedDate.Value;
                query.Where(query.SRAutoNumber == autoNumberID, query.EffectiveDate == efDate);
                query.OrderBy(query.YearNo.Descending, query.MonthNo.Descending, query.DayNo.Descending);
                coll.Load(query);

                Session["collAppAutoNumberLast"] = coll;
                return coll;
            }
            set { Session["collAppAutoNumberLast"] = value; }
        }

        private void PopulateGridDetail()
        {
            //Reset Record Detail
            AppAutoNumberLasts = null;

            //Requery Record Detail
            grdAppAutoNumberLast.DataSource = AppAutoNumberLasts;
            grdAppAutoNumberLast.DataBind();
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdAppAutoNumberLast.Columns[0].Visible = isVisible;
            grdAppAutoNumberLast.Rebind();
        }
        protected void grdAppAutoNumberLast_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdAppAutoNumberLast.DataSource = AppAutoNumberLasts;
        }

        protected void grdAppAutoNumberLast_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem==null) return;
            string dept = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][AppAutoNumberLastMetadata.ColumnNames.DepartmentInitial]);
            int yearNo = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][AppAutoNumberLastMetadata.ColumnNames.YearNo]);
            int monthNo = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][AppAutoNumberLastMetadata.ColumnNames.MonthNo]);
            int dayNo = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][AppAutoNumberLastMetadata.ColumnNames.DayNo]);

            AppAutoNumberLast entity = FindAppAutoNumberLast(dept, yearNo, monthNo, dayNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }
        private AppAutoNumberLast FindAppAutoNumberLast(string dept, int yearNo, int monthNo, int dayNo)
        {
            AppAutoNumberLastCollection coll = AppAutoNumberLasts;
            AppAutoNumberLast retEntity = null;
            foreach (AppAutoNumberLast rec in coll)
            {
                if (rec.DepartmentInitial.Equals(dept) && rec.YearNo.Equals(yearNo) && rec.MonthNo.Equals(monthNo) && rec.DayNo.Equals(dayNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(AppAutoNumberLast entity, GridCommandEventArgs e)
        {
            AppAutoNumberLastDetail userControl = (AppAutoNumberLastDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.str.DepartmentInitial = userControl.DepartmentInitial;
                entity.str.YearNo = userControl.YearNo;
                entity.str.MonthNo = userControl.MonthNo;
                entity.str.DayNo = userControl.DayNo;
                entity.str.LastNumber = userControl.LastNumber;
                entity.LastCompleteNumber = userControl.LastCompleteNumber;
            }
        }

        #endregion
    }
}
