using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ParamedicBridging
    {
        public string BridgingTypeName
        {
            get { return GetColumn("refToAppStandardReferenceItem_ItemName").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_ItemName", value); }
        }

        public string SpesialisticName
        {
            get { return GetColumn("refToParamedicBridging_SpesialisticName").ToString(); }
            set { SetColumn("refToParamedicBridging_SpesialisticName", value); }
        }
    }
}
