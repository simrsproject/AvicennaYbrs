using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class ThrScheduleDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "ThrScheduleSearch.aspx";
            UrlPageList = "ThrScheduleList.aspx";

            ProgramID = AppConstant.Program.ThrSchedule;

            txtCounterID.Text = "1";
            if (!IsPostBack)
            { 
            }
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ThrSchedule());

            txtCounterID.Text = "1";
            //cboPayrollPeriodID.Items.Clear();
            //cboPayrollPeriodID.Text = string.Empty;
            //cboPayrollPeriodID.SelectedValue = string.Empty;
        }

        protected override void OnMenuEditClick()
        {
            cboPayrollPeriodID.Enabled = false;
        }
        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ThrSchedule();
            entity.LoadByPrimaryKey(Convert.ToInt32(txtCounterID.Text));
            entity.MarkAsDeleted();

            using (var trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var query = new ThrScheduleQuery();
            query.Where(
                query.PayrollPeriodID == Convert.ToInt32(cboPayrollPeriodID.SelectedValue)
                );

            var entity = new ThrSchedule();
            if (entity.Load(query))
            {
                args.MessageText = "Payroll Period : " + cboPayrollPeriodID.Text + " has exist.";
                args.IsCancel = true;
                return;
            }

            entity = new ThrSchedule();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new ThrSchedule();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtCounterID.Text)))
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
            auditLogFilter.PrimaryKeyData = string.Format("CounterID='{0}'", txtCounterID.Text);
            auditLogFilter.TableName = "ThrSchedule";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ThrSchedule();
            if (parameters.Length > 0)
            {
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(int.Parse(parameters[0]));
            }
            else
                entity.LoadByPrimaryKey(Convert.ToInt32(txtCounterID.Text));

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var thrs = (ThrSchedule)entity;

            txtCounterID.Value = Convert.ToDouble(thrs.CounterID);

            if (thrs.PayrollPeriodID != null)
            {
                var query = new PayrollPeriodQuery();
                query.Where(query.PayrollPeriodID == thrs.PayrollPeriodID);
                cboPayrollPeriodID.DataSource = query.LoadDataTable();
                cboPayrollPeriodID.DataBind();
                cboPayrollPeriodID.SelectedValue = thrs.PayrollPeriodID.ToString();
                var pp = new PayrollPeriod();
                pp.Load(query);
                cboPayrollPeriodID.Text = pp.PayrollPeriodName;
            }
            else
            {
                cboPayrollPeriodID.Items.Clear();
                cboPayrollPeriodID.Text = string.Empty;
                cboPayrollPeriodID.SelectedValue = string.Empty;
            }
            txtPayrollPeriodName.Text = thrs.PayrollPeriodName;
            txtPayDate.SelectedDate = thrs.PayDate;
            txtSPTYear.Value = Convert.ToDouble(thrs.SPTYear);

            //Display Data Detail
            PopulateItemGrid();
        }

        private void SetEntityValue(ThrSchedule entity)
        {
            entity.CounterID = Convert.ToInt32(txtCounterID.Value);
            entity.PayrollPeriodID = Convert.ToInt32(cboPayrollPeriodID.SelectedValue);
            entity.PayrollPeriodName = "THR - " + cboPayrollPeriodID.Text.Replace("- Monthly", "").Trim();
            entity.PayDate = txtPayDate.SelectedDate;
            entity.SPTYear = Convert.ToInt32(txtSPTYear.Value);

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(ThrSchedule entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();

                if (DataModeCurrent == AppEnum.DataMode.New)
                {
                    var closing = new ClosingThrTransaction
                    {
                        PayrollPeriodID = entity.PayrollPeriodID,
                        IsClosed = false,
                        LastUpdateByUserID = AppSession.UserLogin.UserID,
                        LastUpdateDateTime = DateTime.Now
                    };
                    closing.Save();
                }

                foreach (var i in ThrScheduleItems)
                {
                    i.CounterID = entity.CounterID;
                    i.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    i.LastUpdateDateTime = DateTime.Now;
                }
                ThrScheduleItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();

                txtCounterID.Text = entity.CounterID.ToString();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ThrScheduleQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.CounterID > txtCounterID.Text.ToInt());
                que.OrderBy(que.CounterID.Ascending);
            }
            else
            {
                que.Where(que.CounterID < txtCounterID.Text.ToInt());
                que.OrderBy(que.CounterID.Descending);
            }

            var entity = new ThrSchedule();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        protected void cboPayrollPeriodID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.PayrollPeriodItemsRequested((RadComboBox)o, e.Text);
        }

        protected void cboPayrollPeriodID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.PayrollPeriodItemDataBound(e);
        }

        protected void cboPayrollPeriodID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var pp = new PayrollPeriod();
            if (pp.LoadByPrimaryKey(e.Value.ToInt()))
            {
                txtSPTYear.Value = Convert.ToDouble(pp.SPTYear);
                txtPayDate.SelectedDate = pp.PayDate;
            }
            else 
                txtSPTYear.Value = DateTime.Now.Year;
        }

        #region Record Detail Method Function ThrPeriodItem
        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdDetail.Columns[grdDetail.Columns.Count - 1].Visible = isVisible;

            grdDetail.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdDetail.Rebind();
        }

        private ThrScheduleItemCollection ThrScheduleItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collThrScheduleItem"];
                    if (obj != null)
                    {
                        return ((ThrScheduleItemCollection)(obj));
                    }
                }

                var coll = new ThrScheduleItemCollection();
                var query = new ThrScheduleItemQuery("a");
                var qr = new AppStandardReferenceItemQuery("b");

                query.Select
                    (
                       query,
                       qr.ItemName.As("refToReligionName")
                    );

                query.InnerJoin(qr).On
                        (
                            query.SRReligion == qr.ItemID &
                            qr.StandardReferenceID == AppEnum.StandardReference.Religion
                        );

                query.Where(query.CounterID == txtCounterID.Text);
                query.OrderBy(query.SRReligion.Ascending);

                coll.Load(query);
                Session["collThrScheduleItem"] = coll;
                return coll;
            }
            set { Session["collThrScheduleItem"] = value; }
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            ThrScheduleItems = null; //Reset Record Detail
            grdDetail.DataSource = ThrScheduleItems; //Requery
            grdDetail.MasterTableView.IsItemInserted = false;
            grdDetail.MasterTableView.ClearEditItems();
            grdDetail.DataBind();
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDetail.DataSource = ThrScheduleItems;
        }

        protected void grdDetail_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ThrScheduleItemMetadata.ColumnNames.SRReligion]);
            ThrScheduleItem entity = FindItem(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdDetail_InsertCommand(object source, GridCommandEventArgs e)
        {
            ThrScheduleItem entity = ThrScheduleItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdDetail.Rebind();
        }
        private ThrScheduleItem FindItem(string id)
        {
            var coll = ThrScheduleItems;
            ThrScheduleItem retEntity = null;
            foreach (ThrScheduleItem rec in coll)
            {
                if (rec.SRReligion.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(ThrScheduleItem entity, GridCommandEventArgs e)
        {
            var userControl = (ThrScheduleItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.CounterItemID = userControl.CounterItemID;
                entity.SRReligion = userControl.SRReligion;
                entity.ReligionName = userControl.ReligionName;
            }
        }

        #endregion
    }
}
