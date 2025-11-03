using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.RADT.Master.ParamedicScheduleAnc
{
    public partial class ParamedicScheduleAncDetail : BasePageDetail
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "##";
            UrlPageList = "ParamedicScheduleAncList.aspx";
            ProgramID = AppConstant.Program.ParamedicScheduleAnc;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                var unit = new ServiceUnitQuery("a");
                var tcode = new ServiceUnitTransactionCodeQuery("c");
                unit.InnerJoin(tcode).On(tcode.ServiceUnitID == unit.ServiceUnitID && tcode.SRTransactionCode == BusinessObject.Reference.TransactionCode.JobOrder.ToString());
                unit.Where(unit.SRRegistrationType == AppConstant.RegistrationType.OutPatient, unit.IsActive == true);

                var coll = new ServiceUnitCollection();
                coll.Query.OrderBy(coll.Query.ServiceUnitName.Ascending);
                coll.Load(unit);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var entity in coll)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
        }

        private void SetEntityValue(ParamedicScheduleDate entity)
        {
            entity.ServiceUnitID = cboServiceUnitID.SelectedValue;
            entity.ParamedicID = cboParamedicID.SelectedValue;
            entity.PeriodYear = txtScheduleDate.SelectedDate.Value.Year.ToString();
            entity.PeriodMonth = string.Format("{0:00}", txtScheduleDate.SelectedDate.Value.Month);
            entity.ScheduleDate = txtScheduleDate.SelectedDate;
            entity.OperationalTimeID = string.Empty;
            entity.AddQuota = 0;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //Update Key
            var coll = ParamedicScheduleDateItems;
            foreach (ParamedicScheduleDateItem item in coll)
            {
                item.ServiceUnitID = cboServiceUnitID.SelectedValue;
                item.ParamedicID = cboParamedicID.SelectedValue;
                item.ScheduleDate = txtScheduleDate.SelectedDate;

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ParamedicScheduleDateQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ServiceUnitID == cboServiceUnitID.SelectedValue, que.ParamedicID == cboParamedicID.SelectedValue, que.ScheduleDate > txtScheduleDate.SelectedDate);
                que.OrderBy(que.ScheduleDate.Ascending);
            }
            else
            {
                que.Where(que.ServiceUnitID == cboServiceUnitID.SelectedValue, que.ParamedicID == cboParamedicID.SelectedValue, que.ScheduleDate < txtScheduleDate.SelectedDate);
                que.OrderBy(que.ScheduleDate.Descending);
            }
            var entity = new ParamedicScheduleDate();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ParamedicScheduleDate();
            if (parameters.Length > 0)
            {
                String unitId = parameters[0];
                string parId = parameters[1]; 
                DateTime sdate = Convert.ToDateTime(parameters[2]);
                string pyear = sdate.Year.ToString();
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(unitId, parId, pyear, sdate);
            }
            else
                entity.LoadByPrimaryKey(cboServiceUnitID.SelectedValue, 
                    cboParamedicID.SelectedValue, 
                    txtScheduleDate.SelectedDate.Value.Year.ToString(), 
                    txtScheduleDate.SelectedDate ?? DateTime.Now);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var sd = (ParamedicScheduleDate)entity;
            cboServiceUnitID.SelectedValue = sd.ServiceUnitID;
            cboParamedicID.SelectedValue = sd.ParamedicID;
            if (!string.IsNullOrEmpty(sd.ParamedicID))
            {
                var parq = new ParamedicQuery();
                parq.Where(parq.ParamedicID == sd.ParamedicID);
                cboParamedicID.DataSource = parq.LoadDataTable();
                cboParamedicID.DataBind();
                cboParamedicID.SelectedValue = sd.ParamedicID;
            }
            else
            {
                cboParamedicID.Items.Clear();
                cboParamedicID.SelectedValue = string.Empty;
                cboParamedicID.Text = string.Empty;
            }

            txtScheduleDate.SelectedDate = sd.ScheduleDate;

            //Display Data Detail
            PopulateGridDetail();
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ParamedicScheduleDate());
            cboServiceUnitID.SelectedValue = string.Empty;
            cboServiceUnitID.Text = string.Empty;
            txtScheduleDate.SelectedDate = null;
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
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            cboServiceUnitID.Enabled = !(newVal != AppEnum.DataMode.New);
            cboParamedicID.Enabled = !(newVal != AppEnum.DataMode.New);
            txtScheduleDate.Enabled = !(newVal != AppEnum.DataMode.New);
            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ParamedicScheduleDate();
            if (entity.LoadByPrimaryKey(cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue, txtScheduleDate.SelectedDate.Value.Year.ToString(), txtScheduleDate.SelectedDate ?? DateTime.Now))
            {
                var details = new ParamedicScheduleDateItemCollection();
                details.Query.Where(details.Query.ServiceUnitID == cboServiceUnitID.SelectedValue, details.Query.ParamedicID == cboParamedicID.SelectedValue, details.Query.ScheduleDate == txtScheduleDate.SelectedDate);
                details.LoadAll();
                details.MarkAllAsDeleted();

                entity.MarkAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    details.Save();

                    entity.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                args.MessageText = "Service Unit required.";
                args.IsCancel = true;
                return;
            }
            if (string.IsNullOrEmpty(cboParamedicID.SelectedValue))
            {
                args.MessageText = "Physician required.";
                args.IsCancel = true;
                return;
            }
            if (txtScheduleDate.IsEmpty)
            {
                args.MessageText = "Schedule Date required.";
                args.IsCancel = true;
                return;
            }

            var psd = new ParamedicScheduleDate();
            if (psd.LoadByPrimaryKey(cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue, txtScheduleDate.SelectedDate.Value.Year.ToString(), txtScheduleDate.SelectedDate ?? DateTime.Now))
            {
                args.MessageText = "Data with selected Service Unit, Physician and Schedule Date already exists.";
                args.IsCancel = true;
                return;
            }
            var entity = new ParamedicScheduleDate();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(ParamedicScheduleDate entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                ParamedicScheduleDateItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                args.MessageText = "Service Unit required.";
                args.IsCancel = true;
                return;
            }
            if (string.IsNullOrEmpty(cboParamedicID.SelectedValue))
            {
                args.MessageText = "Physician required.";
                args.IsCancel = true;
                return;
            }
            if (txtScheduleDate.IsEmpty)
            {
                args.MessageText = "Schedule Date required.";
                args.IsCancel = true;
                return;
            }

            var entity = new ParamedicScheduleDate();
            if (entity.LoadByPrimaryKey(cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue, txtScheduleDate.SelectedDate.Value.Year.ToString(), txtScheduleDate.SelectedDate ?? DateTime.Now))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
        }

        #endregion

        #region Record Detail Method Function

        private ParamedicScheduleDateItemCollection ParamedicScheduleDateItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collParamedicScheduleDateItem"];
                    if (obj != null)
                        return ((ParamedicScheduleDateItemCollection)(obj));
                }

                var coll = new ParamedicScheduleDateItemCollection();
                var query = new ParamedicScheduleDateItemQuery("a");
                var otq = new OperationalTimeQuery("b");

                query.Select
                    (
                        query,
                        otq.OperationalTimeName.As("refToOperationalTime_OperationalTimeName"),
                        otq.OperationalTimeBackcolor.As("refToOperationalTime_OperationalTimeBackcolor")
                    );
                query.InnerJoin(otq).On(query.OperationalTimeID == otq.OperationalTimeID);
                if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    query.Where(query.ServiceUnitID == "000");
                else
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue, query.ParamedicID == cboParamedicID.SelectedValue, query.ScheduleDate == txtScheduleDate.SelectedDate);
                query.OrderBy(query.OperationalTimeID.Ascending);
                coll.Load(query);

                Session["collParamedicScheduleDateItem"] = coll;
                return coll;
            }
            set { Session["collParamedicScheduleDateItem"] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                ParamedicScheduleDateItems = null;

            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            ParamedicScheduleDateItems = null; //Reset Record Detail
            grdItem.DataSource = ParamedicScheduleDateItems;
            grdItem.DataBind();
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = ParamedicScheduleDateItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            String otId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ParamedicScheduleDateItemMetadata.ColumnNames.OperationalTimeID]);
            ParamedicScheduleDateItem entity = FindItemGrid(otId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String otId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ParamedicScheduleDateItemMetadata.ColumnNames.OperationalTimeID]);
            ParamedicScheduleDateItem entity = FindItemGrid(otId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            ParamedicScheduleDateItem entity = ParamedicScheduleDateItems.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(ParamedicScheduleDateItem entity, GridCommandEventArgs e)
        {
            var userControl = (ParamedicScheduleAncItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.OperationalTimeID = userControl.OperationalTimeID;
                entity.OperationalTimeName = userControl.OperationalTimeName;

                var ot = new OperationalTime();
                if (ot.LoadByPrimaryKey(entity.OperationalTimeID))
                    entity.OperationalTimeBackcolor = ot.OperationalTimeBackcolor;
                else entity.OperationalTimeBackcolor = "#FFFFFF";

                entity.IsIpr = userControl.IsIpr;
                entity.IsOpr = userControl.IsOpr;
                entity.IsEmr = userControl.IsEmr;
            }
        }

        private ParamedicScheduleDateItem FindItemGrid(string otId)
        {
            ParamedicScheduleDateItemCollection coll = ParamedicScheduleDateItems;
            ParamedicScheduleDateItem retval = null;
            foreach (ParamedicScheduleDateItem rec in coll)
            {
                if (rec.OperationalTimeID.Equals(otId))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        #endregion
        
        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (e.Value == string.Empty)
                cboParamedicID.Items.Clear();
        }

        protected void cboParamedicID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            ParamedicQuery query = new ParamedicQuery("a");
            ServiceUnitParamedicQuery serviceUnitParamedic = new ServiceUnitParamedicQuery("b");
            query.Where
                (
                    query.Or
                        (
                             query.ParamedicName.Like(searchTextContain),
                             query.ParamedicID.Like(searchTextContain)
                        ),
                        serviceUnitParamedic.ServiceUnitID == cboServiceUnitID.SelectedValue
                );
            query.InnerJoin(serviceUnitParamedic).On(query.ParamedicID == serviceUnitParamedic.ParamedicID);
            query.OrderBy(query.ParamedicName.Ascending);
            query.es.Top = 10;
            DataTable dtb = query.LoadDataTable();

            cboParamedicID.DataSource = dtb;
            cboParamedicID.DataBind();

        }
        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }
    }
}