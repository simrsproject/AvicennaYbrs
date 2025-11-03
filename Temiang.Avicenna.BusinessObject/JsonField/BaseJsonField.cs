using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField
{
    public class BaseJsonField
    {
        public string JsonSource
        {
            get { return this.GetType().FullName; }
        }
    }
}
