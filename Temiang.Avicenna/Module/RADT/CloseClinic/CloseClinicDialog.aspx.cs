using System;
using System.Linq;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace Temiang.Avicenna.Module.RADT.CloseClinic
{
    public partial class CloseClinicDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var usr = new AppUser();
                usr.LoadByPrimaryKey(AppSession.UserLogin.UserID);

                txtScheduleDate.SelectedDate = DateTime.Now;
                txtParamedicID.Text = usr.ParamedicID;
                txtPeriodYear.Text = DateTime.Now.Year.ToString();

                var query = new ServiceUnitQuery("a");
                var scheduleq = new ParamedicScheduleDateQuery("b");
                query.InnerJoin(scheduleq).On(scheduleq.ServiceUnitID == query.ServiceUnitID);
                query.Where(scheduleq.PeriodYear == txtPeriodYear.Text, scheduleq.ScheduleDate.Date() == txtScheduleDate.SelectedDate.Value.Date, scheduleq.ParamedicID == txtParamedicID.Text);
                var coll = new ServiceUnitCollection();
                coll.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                var i = 0;
                foreach (var c in coll)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(c.ServiceUnitName, c.ServiceUnitID));
                    if (i == 0)
                        cboServiceUnitID.SelectedValue = c.ServiceUnitID;
                    i += 1;
                }

                LoadOperationalTime(cboServiceUnitID.SelectedValue);
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonCancel.Visible = false;
                ButtonOk.Visible = false;
            }
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadOperationalTime(e.Value);
        }

        private void LoadOperationalTime(string unitId)
        {
            var schedules = new ParamedicScheduleDate();
            if (schedules.LoadByPrimaryKey(unitId, txtParamedicID.Text, txtPeriodYear.Text, txtScheduleDate.SelectedDate.Value.Date))
            {
                var ot = new OperationalTime();
                ot.LoadByPrimaryKey(schedules.OperationalTimeID);

                txtStartTime1.Text = ot.StartTime1;
                txtEndTime1.Text = ot.EndTime1;
                lblStatusTime1.Text = string.IsNullOrEmpty(txtStartTime1.Text.Trim()) ? string.Empty : ((schedules.IsClosedTime1 ?? false) ? "CLOSED" : "OPEN");
                lblStatusTime1.ForeColor = string.IsNullOrEmpty(txtStartTime1.Text.Trim()) ? Color.White : ((schedules.IsClosedTime1 ?? false) ? Color.Red : Color.Green);
                btnClosedTime1.Visible = !(schedules.IsClosedTime1 ?? false) && !string.IsNullOrEmpty(txtStartTime1.Text.Trim());
                btnOpenTime1.Visible = (schedules.IsClosedTime1 ?? false) && !string.IsNullOrEmpty(txtStartTime1.Text.Trim());

                txtStartTime2.Text = ot.StartTime2;
                txtEndTime2.Text = ot.EndTime2;
                lblStatusTime2.Text = string.IsNullOrEmpty(txtStartTime2.Text.Trim()) ? string.Empty : ((schedules.IsClosedTime2 ?? false) ? "CLOSED" : "OPEN");
                lblStatusTime2.ForeColor = string.IsNullOrEmpty(txtStartTime2.Text.Trim()) ? Color.White : ((schedules.IsClosedTime2 ?? false) ? Color.Red : Color.Green);
                btnClosedTime2.Visible = !(schedules.IsClosedTime2 ?? false) && !string.IsNullOrEmpty(txtStartTime2.Text.Trim());
                btnOpenTime2.Visible = (schedules.IsClosedTime2 ?? false) && !string.IsNullOrEmpty(txtStartTime2.Text.Trim());

                txtStartTime3.Text = ot.StartTime3;
                txtEndTime3.Text = ot.EndTime3;
                lblStatusTime3.Text = string.IsNullOrEmpty(txtStartTime3.Text.Trim()) ? string.Empty : ((schedules.IsClosedTime3 ?? false) ? "CLOSED" : "OPEN");
                lblStatusTime3.ForeColor = string.IsNullOrEmpty(txtStartTime3.Text.Trim()) ? Color.White : ((schedules.IsClosedTime3 ?? false) ? Color.Red : Color.Green);
                btnClosedTime3.Visible = !(schedules.IsClosedTime3 ?? false) && !string.IsNullOrEmpty(txtStartTime3.Text.Trim());
                btnOpenTime3.Visible = (schedules.IsClosedTime3 ?? false) && !string.IsNullOrEmpty(txtStartTime3.Text.Trim());

                txtStartTime4.Text = ot.StartTime4;
                txtEndTime4.Text = ot.EndTime4;
                lblStatusTime4.Text = string.IsNullOrEmpty(txtStartTime4.Text.Trim()) ? string.Empty : ((schedules.IsClosedTime4 ?? false) ? "CLOSED" : "OPEN");
                lblStatusTime4.ForeColor = string.IsNullOrEmpty(txtStartTime4.Text.Trim()) ? Color.White : ((schedules.IsClosedTime4 ?? false) ? Color.Red : Color.Green);
                btnClosedTime4.Visible = !(schedules.IsClosedTime4 ?? false) && !string.IsNullOrEmpty(txtStartTime4.Text.Trim());
                btnOpenTime4.Visible = (schedules.IsClosedTime4 ?? false) && !string.IsNullOrEmpty(txtStartTime4.Text.Trim());

                txtStartTime5.Text = ot.StartTime5;
                txtEndTime5.Text = ot.EndTime5;
                lblStatusTime5.Text = string.IsNullOrEmpty(txtStartTime5.Text.Trim()) ? string.Empty : ((schedules.IsClosedTime5 ?? false) ? "CLOSED" : "OPEN");
                lblStatusTime5.ForeColor = string.IsNullOrEmpty(txtStartTime5.Text.Trim()) ? Color.White : ((schedules.IsClosedTime5 ?? false) ? Color.Red : Color.Green);
                btnClosedTime5.Visible = !(schedules.IsClosedTime5 ?? false) && !string.IsNullOrEmpty(txtStartTime5.Text.Trim());
                btnOpenTime5.Visible = (schedules.IsClosedTime5 ?? false) && !string.IsNullOrEmpty(txtStartTime5.Text.Trim());
            }
            else
            {
                txtStartTime1.Text = string.Empty;
                txtEndTime1.Text = string.Empty;
                lblStatusTime1.Text = string.Empty;
                btnClosedTime1.Visible = false;
                btnOpenTime1.Visible = false;

                txtStartTime2.Text = string.Empty;
                txtEndTime2.Text = string.Empty;
                lblStatusTime2.Text = string.Empty;
                btnClosedTime2.Visible = false;
                btnOpenTime2.Visible = false;

                txtStartTime3.Text = string.Empty;
                txtEndTime3.Text = string.Empty;
                lblStatusTime3.Text = string.Empty;
                btnClosedTime3.Visible = false;
                btnOpenTime3.Visible = false;

                txtStartTime4.Text = string.Empty;
                txtEndTime4.Text = string.Empty;
                lblStatusTime4.Text = string.Empty;
                btnClosedTime4.Visible = false;
                btnOpenTime4.Visible = false;

                txtStartTime5.Text = string.Empty;
                txtEndTime5.Text = string.Empty;
                lblStatusTime5.Text = string.Empty;
                btnClosedTime5.Visible = false;
                btnOpenTime5.Visible = false;
            }
        }

        private void Closed(string time, bool isClosed)
        {
            var schedule = new ParamedicScheduleDate();
            if (schedule.LoadByPrimaryKey(cboServiceUnitID.SelectedValue, txtParamedicID.Text, txtPeriodYear.Text, txtScheduleDate.SelectedDate.Value.Date))
            {
                switch (time)
                {
                    case "1":
                        schedule.IsClosedTime1 = isClosed;
                        schedule.ClosedDateTime1 = (new DateTime()).NowAtSqlServer();
                        schedule.ClosedTime1ByUserID = AppSession.UserLogin.UserID;

                        break;
                    case "2":
                        schedule.IsClosedTime2 = isClosed;
                        schedule.ClosedDateTime2 = (new DateTime()).NowAtSqlServer();
                        schedule.ClosedTime2ByUserID = AppSession.UserLogin.UserID;

                        break;
                    case "3":
                        schedule.IsClosedTime3 = isClosed;
                        schedule.ClosedDateTime3 = (new DateTime()).NowAtSqlServer();
                        schedule.ClosedTime3ByUserID = AppSession.UserLogin.UserID;

                        break;
                    case "4":
                        schedule.IsClosedTime4 = isClosed;
                        schedule.ClosedDateTime4 = (new DateTime()).NowAtSqlServer();
                        schedule.ClosedTime4ByUserID = AppSession.UserLogin.UserID;

                        break;
                    case "5":
                        schedule.IsClosedTime5 = isClosed;
                        schedule.ClosedDateTime5 = (new DateTime()).NowAtSqlServer();
                        schedule.ClosedTime5ByUserID = AppSession.UserLogin.UserID;

                        break;
                }
                schedule.LastUpdateByUserID = AppSession.UserLogin.UserID;
                schedule.LastUpdateDateTime= (new DateTime()).NowAtSqlServer();
                schedule.Save();

                LoadOperationalTime(cboServiceUnitID.SelectedValue);
            }
        }
        protected void btnClosedTime1_Click(object sender, ImageClickEventArgs e)
        {
            Closed("1", true);
        }

        protected void btnClosedTime2_Click(object sender, ImageClickEventArgs e)
        {
            Closed("2", true);
        }

        protected void btnClosedTime3_Click(object sender, ImageClickEventArgs e)
        {
            Closed("3", true);
        }

        protected void btnClosedTime4_Click(object sender, ImageClickEventArgs e)
        {
            Closed("4", true);
        }

        protected void btnClosedTime5_Click(object sender, ImageClickEventArgs e)
        {
            Closed("5", true);
        }

        protected void btnOpenTime1_Click(object sender, ImageClickEventArgs e)
        {
            Closed("1", false);
        }

        protected void btnOpenTime2_Click(object sender, ImageClickEventArgs e)
        {
            Closed("2", false);
        }

        protected void btnOpenTime3_Click(object sender, ImageClickEventArgs e)
        {
            Closed("3", false);
        }

        protected void btnOpenTime4_Click(object sender, ImageClickEventArgs e)
        {
            Closed("4", false);
        }

        protected void btnOpenTime5_Click(object sender, ImageClickEventArgs e)
        {
            Closed("5", false);
        }

        public override bool OnButtonOkClicked()
        {
            if (!Page.IsValid)
                return false;

            return true;
        }
    }
}