using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.Module.HR.EmployeeHR;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Payroll
{
    public partial class MealAttendanceDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "MealAttendanceSearch.aspx";
            UrlPageList = "MealAttendanceList.aspx";

            //ProgramID = AppConstant.Program.MealAttendance;
        }

        protected override void OnMenuNewClick()
        {
            ViewState["id"] = 0;

            txtOpenDate.SelectedDate = DateTime.Now.Date;
            txtOpenTime.Text = DateTime.Now.ToString("HH:mm");
            txtCloseDate.SelectedDate = DateTime.Now.Date;
            txtCloseTime.Text = DateTime.Now.ToString("HH:mm");
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new MealAttendance();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new MealAttendance();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(ViewState["id"].ToString())))
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
            auditLogFilter.PrimaryKeyData = string.Format("MealAttendanceID='{0}'", ViewState["id"].ToString());
            auditLogFilter.TableName = "MealAttendance";
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new MealAttendance();
            if (parameters.Length > 0)
            {
                if (!parameters[0].Equals(string.Empty)) entity.LoadByPrimaryKey(int.Parse(parameters[0]));
            }
            else entity.LoadByPrimaryKey(Convert.ToInt32(ViewState["id"].ToString()));

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var wh = (MealAttendance)entity;
            if (wh == null)
            {
                ViewState["id"] = 0;
                return;
            }
            ViewState["id"] = wh.MealAttendanceID;

            txtOpenDate.SelectedDate = wh.OpenDatetime.Value.Date;
            txtOpenTime.Text = wh.OpenDatetime.Value.ToString("HH:mm");
            if (wh.CloseDatetime != null)
            {
                txtCloseDate.SelectedDate = wh.CloseDatetime.Value.Date;
                txtCloseTime.Text = wh.CloseDatetime.Value.ToString("HH:mm");
            }
            cboStatusID.SelectedValue = wh.Status.ToString();
            txtNotes.Text = wh.Notes;

            PopulateMealAttendanceDetailGrid();
        }

        private void SetEntityValue(MealAttendance entity)
        {
            if (DateTime.TryParseExact(
                txtOpenDate.SelectedDate.Value.ToString("MM/dd/yyyy") + " " + txtOpenTime.TextWithLiterals, "MM/dd/yyyy HH:mm", null,
                DateTimeStyles.None, out var parsedOpen))
            {
                entity.OpenDatetime = parsedOpen;
            }
            if (txtCloseDate.IsEmpty)
            {
                entity.str.CloseDatetime = string.Empty;
            }
            else
            {
                if (DateTime.TryParseExact(
                    txtCloseDate.SelectedDate.Value.ToString("MM/dd/yyyy") + " " + txtCloseTime.TextWithLiterals, "MM/dd/yyyy HH:mm", null,
                    DateTimeStyles.None, out var parsed))
                {
                    entity.CloseDatetime = parsed;
                }
            }
            entity.Status = byte.Parse(cboStatusID.SelectedValue);
            entity.Notes = txtNotes.Text;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(MealAttendance entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();

                foreach (var mealAttendance in MealAttendances)
                {
                    mealAttendance.MealAttendanceID = entity.MealAttendanceID;
                    if (mealAttendance.es.IsAdded || mealAttendance.es.IsModified)
                    {
                        mealAttendance.LastUpdateUserID = AppSession.UserLogin.UserID;
                        mealAttendance.LastUpdateDateTime = DateTime.Now;
                    }
                }

                MealAttendances.Save();

                //Commit if success, Rollback if failed
                trans.Complete();

                ViewState["id"] = entity.MealAttendanceID;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new MealAttendanceQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.MealAttendanceID > (int)ViewState["id"]);
                que.OrderBy(que.MealAttendanceID.Ascending);
            }
            else
            {
                que.Where(que.MealAttendanceID < (int)ViewState["id"]);
                que.OrderBy(que.MealAttendanceID.Descending);
            }

            var entity = new MealAttendance();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemMealAttendanceDetail(newVal);
        }

        private void RefreshCommandItemMealAttendanceDetail(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPersonalAddress.Columns[0].Visible = isVisible;
            grdPersonalAddress.Columns[grdPersonalAddress.Columns.Count - 1].Visible = isVisible;

            grdPersonalAddress.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdPersonalAddress.Rebind();
        }

        private MealAttendanceDetailCollection MealAttendances
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collMealAttendanceDetailCollection"];
                    if (obj != null)
                    {
                        return ((MealAttendanceDetailCollection)(obj));
                    }
                }

                MealAttendanceDetailCollection coll = new MealAttendanceDetailCollection();
                MealAttendanceDetailQuery query = new MealAttendanceDetailQuery("b");
                VwEmployeeTableQuery emp = new VwEmployeeTableQuery("c");
                ServiceUnitQuery unit = new ServiceUnitQuery("d");

                query.Select
                    (
                       query,
                       emp.EmployeeName.As("refToVwEmployeeTable_EmployeeName"),
                       unit.ServiceUnitName.As("refToVwEmployeeTable_ServiceUnitName")
                    );

                query.InnerJoin(emp).On(query.PersonID == emp.PersonID);
                query.LeftJoin(unit).On(emp.ServiceUnitID == unit.ServiceUnitID);
                query.Where(query.MealAttendanceID == int.Parse(ViewState["id"].ToString())); //TODO: Betulkan parameternya
                query.OrderBy(emp.ServiceUnitID.Ascending, emp.EmployeeName.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collMealAttendanceDetailCollection"] = coll;
                return coll;
            }
            set { Session["collMealAttendanceDetailCollection"] = value; }
        }

        private void PopulateMealAttendanceDetailGrid()
        {
            //Display Data Detail
            MealAttendances = null; //Reset Record Detail
            grdPersonalAddress.DataSource = MealAttendances; //Requery
            grdPersonalAddress.MasterTableView.IsItemInserted = false;
            grdPersonalAddress.MasterTableView.ClearEditItems();
            grdPersonalAddress.DataBind();
        }

        protected void grdPersonalAddress_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPersonalAddress.DataSource = MealAttendances;
        }

        protected void grdPersonalAddress_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][MealAttendanceDetailMetadata.ColumnNames.MealAttendanceDetailID]);
            BusinessObject.MealAttendanceDetail entity = FindMealAttendanceDetail(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPersonalAddress_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][MealAttendanceDetailMetadata.ColumnNames.MealAttendanceDetailID]);
            BusinessObject.MealAttendanceDetail entity = FindMealAttendanceDetail(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPersonalAddress_InsertCommand(object source, GridCommandEventArgs e)
        {
            BusinessObject.MealAttendanceDetail entity = MealAttendances.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPersonalAddress.Rebind();
        }

        private BusinessObject.MealAttendanceDetail FindMealAttendanceDetail(Int32 id)
        {
            MealAttendanceDetailCollection coll = MealAttendances;
            BusinessObject.MealAttendanceDetail retEntity = null;
            foreach (BusinessObject.MealAttendanceDetail rec in coll)
            {
                if (rec.MealAttendanceID.ToString().Equals(id.ToString()))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(BusinessObject.MealAttendanceDetail entity, GridCommandEventArgs e)
        {
            MealAttendanceItem userControl = (MealAttendanceItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.MealAttendanceID = ViewState["id"].ToInt();
                entity.Datetime = userControl.Datetime;
                entity.PersonID = userControl.PersonID;
                entity.EmployeeName = userControl.EmployeeName;
                entity.ServiceUnitName = userControl.ServiceUnitName;
            }
        }

    }
}