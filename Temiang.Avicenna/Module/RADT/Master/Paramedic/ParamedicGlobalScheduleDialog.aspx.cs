using System;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Globalization;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ParamedicGlobalScheduleDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.Paramedic;

            if (!IsPostBack)
            {
                LoadData();
                (Helper.FindControlRecursive(this, "btnOk") as Button).Text = "Close";
                (Helper.FindControlRecursive(this, "btnCancel") as Button).Visible = false;
            }
        }

        private void LoadData()
        {
            txtParamedicID.Text = Request.QueryString["parId"];
            var par = new Paramedic();
            if (par.LoadByPrimaryKey(txtParamedicID.Text))
                lblParamedicName.Text = par.ParamedicName;
            else lblParamedicName.Text = string.Empty;
            txtServiceUnitID.Text = Request.QueryString["unitId"];
            var unit = new ServiceUnit();
            if (unit.LoadByPrimaryKey(txtServiceUnitID.Text))
                lblServiceUnitName.Text = unit.ServiceUnitName;
            else lblServiceUnitName.Text = string.Empty;
        }

        private ParamedicGlobalScheduleCollection ParamedicGlobalSchedules
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collParamedicGlobalSchedule"];
                    if (obj != null)
                    {
                        return ((ParamedicGlobalScheduleCollection)(obj));
                    }
                }

                var coll = new ParamedicGlobalScheduleCollection();
                var query = new ParamedicGlobalScheduleQuery("a");
                var otq = new OperationalTimeQuery("b");
                query.InnerJoin(otq).On(otq.OperationalTimeID == query.OperationalTimeID);
                query.Select
                    (
                        query,
                        "<'' AS refToParamedicGlobalSchedule_DayOfWeekName>",
                        otq.OperationalTimeName.As("refToOperationalTime_OperationalTimeName"),
                        otq.OperationalTimeBackcolor.As("refToOperationalTime_OperationalTimeBackcolor")
                    );
                query.Where(query.ParamedicID == txtParamedicID.Text, query.ServiceUnitID == txtServiceUnitID.Text);
                query.OrderBy(query.DayOfWeek.Ascending);
                coll.Load(query);

                var culture = new CultureInfo(AppSession.UserLogin.SRLanguage);
                var days = culture.DateTimeFormat.DayNames;

                foreach (var entity in coll)
                {
                    entity.DayOfWeekName = days[(entity.DayOfWeek ?? 0) - 1];
                }

                Session["collParamedicGlobalSchedule"] = coll;
                return coll;
            }
            set
            {
                Session["collParamedicGlobalSchedule"] = value;
            }
        }

        protected void grdGlobalSchedule_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdGlobalSchedule.DataSource = ParamedicGlobalSchedules;
        }

        protected void grdGlobalSchedule_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            int id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ParamedicGlobalScheduleMetadata.ColumnNames.DayOfWeek]);

            var entity = FindParamedicGlobalSchedule(id);
            if (entity != null)
            {
                SetEntityValue(entity, e);
                ParamedicGlobalSchedules.Save();
            }
        }

        protected void grdGlobalSchedule_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            int id =
                Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][ParamedicGlobalScheduleMetadata.ColumnNames.DayOfWeek]);
            ParamedicGlobalSchedule entity = FindParamedicGlobalSchedule(id);
            if (entity != null)
            {
                entity.MarkAsDeleted();
                ParamedicGlobalSchedules.Save();
            }
                
        }

        protected void grdGlobalSchedule_InsertCommand(object source, GridCommandEventArgs e)
        {
            ParamedicGlobalSchedule entity = ParamedicGlobalSchedules.AddNew();
            SetEntityValue(entity, e);
            ParamedicGlobalSchedules.Save();

            //Stay in insert mode
            e.Canceled = true;
            grdGlobalSchedule.Rebind();
        }

        private ParamedicGlobalSchedule FindParamedicGlobalSchedule(int id)
        {
            ParamedicGlobalScheduleCollection coll = ParamedicGlobalSchedules;
            ParamedicGlobalSchedule retEntity = null;
            foreach (ParamedicGlobalSchedule rec in coll)
            {
                if (rec.DayOfWeek.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(ParamedicGlobalSchedule entity, GridCommandEventArgs e)
        {
            var userControl = (ParamedicGlobalScheduleDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ParamedicID = txtParamedicID.Text;
                entity.ServiceUnitID = txtServiceUnitID.Text;
                entity.DayOfWeek = userControl.DayOfWeek;
                entity.DayOfWeekName = userControl.DayOfWeekName;
                entity.OperationalTimeID = userControl.OperationalTimeID;
                entity.OperationalTimeName = userControl.OperationalTimeName;

                var ot = new OperationalTime();
                if (ot.LoadByPrimaryKey(entity.OperationalTimeID))
                    entity.OperationalTimeBackcolor = ot.OperationalTimeBackcolor;
                else entity.OperationalTimeBackcolor = "#FFFFFF";
            }
        }
    }
}