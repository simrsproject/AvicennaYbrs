using System;
using System.Data;
using System.Linq;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar.Collections;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class OperationalTimeSelect : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int year = Convert.ToInt16(Page.Request.QueryString[0]);
                cldSchedule.RangeMinDate = new DateTime(year, 1, 1);
                cldSchedule.RangeMaxDate = new DateTime(year, 12, 31);
                cldSchedule.FocusedDate = new DateTime(year, 1, 1);

                if (grdOperationalTime.MasterTableView.Items.Count > 0)
                    grdOperationalTime.MasterTableView.Items[0].Selected = true;

                var unit = new ServiceUnit();
                if (unit.LoadByPrimaryKey(Request.QueryString["unitId"].ToString()) && unit.IsUsingJobOrder == true)
                    trRegType.Visible = true;
                else
                    trRegType.Visible = false;

                chkIsIpr.Checked = true;
                chkIsOpr.Checked = true;
                chkIsEmr.Checked = true;
            }
        }

        protected void grdOperationalTime_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //Session sudah diload di page Detail
            ((RadGrid)source).DataSource = OperationalTimes;
        }
        private DataTable OperationalTimes
        {
            get
            {
                object obj = this.Session["collOperationalTime"];
                if (obj != null)
                    return ((DataTable)(obj));

                OperationalTimeQuery query;
                if (Session["queOperationalTime"] != null)
                    query = (OperationalTimeQuery)Session["queOperationalTime"];
                else
                    query = new OperationalTimeQuery();

                query.Select
                    (
                        query.OperationalTimeID,
                        query.OperationalTimeName,
                        query.OperationalTimeBackcolor,
                        (query.StartTime1 + "-" + query.EndTime1).As("Time1"),
                        (query.StartTime2 + "-" + query.EndTime2).As("Time2"),
                        (query.StartTime3 + "-" + query.EndTime3).As("Time3"),
                        (query.StartTime4 + "-" + query.EndTime4).As("Time4"),
                        (query.StartTime5 + "-" + query.EndTime5).As("Time5")
                    );
                DataTable dtb = query.LoadDataTable();
                DataRow newRow = dtb.NewRow();
                newRow["OperationalTimeID"] = "";
                newRow["OperationalTimeName"] = "Remove Schedule";
                dtb.Rows.Add(newRow);

                dtb.PrimaryKey = new DataColumn[] { dtb.Columns["OperationalTimeID"] };
                this.Session["collOperationalTime"] = dtb;
                return dtb;
            }
        }
        public override bool OnButtonOkClicked()
        {
            if (grdOperationalTime.SelectedValue == null) return false;

            DateTimeCollection selectedDates = cldSchedule.SelectedDates;
            DataTable dtb = (DataTable)Session["dtbParamedicScheduleDate"];
            string operationalTimeID = grdOperationalTime.SelectedValue.ToString();
            foreach (RadDate date in selectedDates)
            {
                DataRow row = dtb.Rows.Find(date.Date);
                if (row == null)
                {
                    row = dtb.NewRow();
                    row["ScheduleDate"] = date.Date;
                    row["IsIpr"] = chkIsIpr.Checked;
                    row["IsOpr"] = chkIsOpr.Checked;
                    row["IsEmr"] = chkIsEmr.Checked;
                    row["AddQuota"] = 0;
                    dtb.Rows.Add(row);
                }
                else if (operationalTimeID.Equals(string.Empty))
                {
                    row.Delete();
                    continue;
                } else if(row["OperationalTimeID"].ToString() != operationalTimeID)
                {
                    // cek appointment
                    var aptColl = new BusinessObject.AppointmentCollection();
                    var qr = aptColl.Query;
                    qr.Where(qr.ServiceUnitID == Request.QueryString["unitId"].ToString(),
                        qr.ParamedicID == Request.QueryString["parId"].ToString(),
                        qr.AppointmentDate == date.Date,
                        qr.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel);
                    if (aptColl.LoadAll()) {
                        // tidak boleh edit schedule karena sudah ada appointment
                        // gak boleh editnya hanya jika jam start berubah sehingga slot antrian jadi kacauuu
                        var dtoColl = new OperationalTimeCollection();
                        dtoColl.Query.Where(dtoColl.Query.OperationalTimeID.In(row["OperationalTimeID"].ToString(), operationalTimeID));
                        if (dtoColl.LoadAll()) { 
                            var dto1 = dtoColl.Where(d => d.OperationalTimeID == row["OperationalTimeID"].ToString()).FirstOrDefault();
                            var dto2 = dtoColl.Where(d => d.OperationalTimeID == operationalTimeID).FirstOrDefault();
                            if (dto1 != null && dto2 != null) {
                                if (dto1.StartTime1 != dto2.StartTime1) {
                                    this.ShowMessage("There are currently appointments available on the schedule, Operational time can not be changed");
                                    return false;
                                }
                            }
                        }
                    }
                }
                
                row["OperationalTimeID"] = operationalTimeID;
            }
            return true;
        }
    }
}
