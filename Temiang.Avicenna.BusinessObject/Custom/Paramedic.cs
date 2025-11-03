using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class Paramedic
    {
        private string _ScheduleText;
        public string ScheduleText {
            get { return _ScheduleText; }
            set { _ScheduleText = value; }
        }
    }
}
