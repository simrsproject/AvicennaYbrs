using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField
{
    public class ControlPlan : BaseJsonField
    {
        public List<ControlPlanItem> Items { get; set; }
    }
    public class ControlPlanItem
    {
        public DateTime ControlPlanDateTime { get; set; }
        public string ParamedicName { get; set; }
        public string ParamedicID { get; set; }
        public string ServiceUnitID { get; set; }
        public string SpecialtyName { get; set; }
        public string AppointmentNo { get; set; }
        public int? AppointmentQue { get; set; }
        public string AppointmentTime { get; set; }
    }

    public class ControlPlanExt : BaseJsonField
    {
        public List<ControlPlanItemExt> Items { get; set; }
    }
    public class ControlPlanItemExt: ControlPlanItem
    {
        public string ServiceUnitName { get; set; }

    }
}
