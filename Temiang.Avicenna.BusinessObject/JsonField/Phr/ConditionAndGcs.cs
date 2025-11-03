using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;

namespace Temiang.Avicenna.BusinessObject.JsonField.Phr
{
    /// <summary>
    /// Glasgow Coma Scale
    /// </summary>
    public class ConditionAndGcs : Gcs
    {
        public string Condition { get; set; }
    }
}
