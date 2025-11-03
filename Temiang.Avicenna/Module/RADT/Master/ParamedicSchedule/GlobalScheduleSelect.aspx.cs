using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.DynamicQuery;
using System.Globalization;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ParamedicGlobalScheduleSelect : BasePageDialog
    {
        private string errorMsg;

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ParamedicSchedule;

            if (!IsPostBack)
            {
                cboMonth.Items.Add(new RadComboBoxItem("January", "01"));
                cboMonth.Items.Add(new RadComboBoxItem("February", "02"));
                cboMonth.Items.Add(new RadComboBoxItem("March", "03"));
                cboMonth.Items.Add(new RadComboBoxItem("April", "04"));
                cboMonth.Items.Add(new RadComboBoxItem("May", "05"));
                cboMonth.Items.Add(new RadComboBoxItem("June", "06"));
                cboMonth.Items.Add(new RadComboBoxItem("July", "07"));
                cboMonth.Items.Add(new RadComboBoxItem("August", "08"));
                cboMonth.Items.Add(new RadComboBoxItem("September", "09"));
                cboMonth.Items.Add(new RadComboBoxItem("October", "10"));
                cboMonth.Items.Add(new RadComboBoxItem("November", "11"));
                cboMonth.Items.Add(new RadComboBoxItem("December", "12"));

                if (DateTime.Now.ToString("MM") == "12")
                    cboMonth.SelectedValue = DateTime.Now.ToString("MM");
                else
                    cboMonth.SelectedValue = DateTime.Now.AddMonths(1).ToString("MM");

                txtYear.Text = Request.QueryString["year"];
            }
        }

        #region Record Detail Method Function ParamedicGlobalSchedule
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
                query.Where(query.ParamedicID == Request.QueryString["parId"], query.ServiceUnitID == Request.QueryString["unitId"]);
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
                entity.Save();
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
                //entity.Save();
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

        private void SetEntityValue(ParamedicGlobalSchedule entity, GridCommandEventArgs e)
        {
            var userControl = (ParamedicGlobalScheduleDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ParamedicID = Request.QueryString["parId"];
                entity.ServiceUnitID = Request.QueryString["unitId"];
                entity.DayOfWeek = userControl.DayOfWeek;
                entity.DayOfWeekName = userControl.DayOfWeekName;
                entity.OperationalTimeID = userControl.OperationalTimeID;
                entity.OperationalTimeName = userControl.OperationalTimeName;

                var ot = new OperationalTime();
                if (ot.LoadByPrimaryKey(entity.OperationalTimeID))
                    entity.OperationalTimeBackcolor = ot.OperationalTimeBackcolor;
                else entity.OperationalTimeBackcolor = "#FFFFFF";

                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }
        #endregion

        private void InsertGlobalSchedule()
        {
            if (string.IsNullOrEmpty(cboMonth.SelectedValue))
            {
                errorMsg = "Month is required.";
                return;
            }

            if (ParamedicGlobalSchedules.Count == 0)
            {
                errorMsg = "Global Schedule Template has not been set.";
                return;
            }

            DateTime sDate = new DateTime(Request.QueryString["year"].ToInt(), cboMonth.SelectedValue.ToInt(), 1);
            DateTime eDate = sDate.AddMonths(1).AddDays(-1);

            var psdColl = new ParamedicScheduleDateCollection();
            psdColl.Query.Where(psdColl.Query.ServiceUnitID == Request.QueryString["unitId"], 
                psdColl.Query.ParamedicID == Request.QueryString["parId"], 
                psdColl.Query.PeriodYear == Request.QueryString["year"],
                psdColl.Query.ScheduleDate >= sDate, psdColl.Query.ScheduleDate <= eDate,
                psdColl.Query.OperationalTimeID != string.Empty
                );
            psdColl.LoadAll();
            if (psdColl.Count > 0)
            {
                errorMsg = "The schedule has been set for the selected month.";
                return;
            }

            ParamedicGlobalSchedule.InsertFromGlobalSchedule(Request.QueryString["unitId"], 
                Request.QueryString["parId"], Request.QueryString["year"], cboMonth.SelectedValue, AppSession.UserLogin.UserID);

            errorMsg = string.Empty;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            InsertGlobalSchedule();
            if (!string.IsNullOrEmpty(errorMsg))
            {
                ShowInformationHeader(errorMsg);
                return false;
            }

            return true;
        }
    }
}