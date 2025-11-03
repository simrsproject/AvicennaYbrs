using System;
using System.Data;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar.Collections;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.AssetManagement
{
    public partial class ScheduleDateSelect : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int year = Convert.ToInt16(Page.Request.QueryString[0]);
                cldSchedule.RangeMinDate = new DateTime(year, 1, 1);
                cldSchedule.RangeMaxDate = new DateTime(year, 12, 31);
                cldSchedule.FocusedDate = new DateTime(year, 1, 1);
            }
        }
        
        public override bool OnButtonOkClicked()
        {
            DateTimeCollection selectedDates = cldSchedule.SelectedDates;
            var dtb = (DataTable)Session["dtbAssetPreventiveMaintenanceSchedule"];
            
            foreach (RadDate date in selectedDates)
            {
                DataRow row = dtb.Rows.Find(date.Date);
                if (row == null)
                {
                    row = dtb.NewRow();
                    row["ScheduleDate"] = date.Date;
                    row["IsProcessed"] = false;
                    dtb.Rows.Add(row);
                }
                else if (chkIsVoid.Checked && Convert.ToBoolean(row["IsProcessed"]) == false)
                    row.Delete();
            }
            return true;
        }
    }
}
