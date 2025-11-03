using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class HighRiskCriterias : BaseJsonField
    {
        public List<HighRiskCriteria> Items { get; set; }
    }
    public class HighRiskCriteria
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}
