using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemBridging
    {
        public string BridgingTypeName
        {
            get { return GetColumn("refToAppStandardReferenceItem_ItemName").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_ItemName", value); }
        }

        public string ConnectionName
        {
            get { return GetColumn("refToAppStandardReferenceItem_BridgingTypeReferenceID").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_BridgingTypeReferenceID", value); }
        }
    }
}
