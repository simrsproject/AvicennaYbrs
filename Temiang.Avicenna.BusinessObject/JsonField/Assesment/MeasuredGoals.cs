using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class MeasuredGoals : BaseJsonField
    {
        public List<MeasuredGoal> Items { get; set; }
    }
    public class MeasuredGoal
    {
        public int No { get; set; }
        public string Description { get; set; }
        public string IterationQty { get; set; }
        public string IterationInterval { get; set; }
        public string IterationTimeType { get; set; }
    }
}
